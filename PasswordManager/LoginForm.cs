using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasswordManager
{
    public partial class LoginForm : Form
    {
        private const int SALT_SIZE = 16;
        private const int KEY_SIZE = 32;
        private const int IV_SIZE = 16;
        private const int ITERATIONS = 100000;
        private const int ERROR_DELAY = 5000;
        private const int MIN_PASSWORD_LENGTH = 8;

        private readonly string appDataFolder;
        private readonly string databasePath;
        private bool databaseExists;

        public LoginForm()
        {
            InitializeComponent();

            appDataFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "PasswordManager");

            databasePath = Path.Combine(appDataFolder, "passwords.dat");

            CheckDatabaseExists();
        }

        private void CheckDatabaseExists()
        {
            databaseExists = File.Exists(databasePath);
            btnUnlock.Enabled = databaseExists;

            btnCreateNew.Text = databaseExists ?
                "Создать новую базу" :
                "Создать базу";
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtMasterPassword.Focus();
        }

        private void txtMasterPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                if (databaseExists)
                    _ = AuthenticateUser();
                else
                    _ = CreateNewDatabase();
            }
        }

        private void checkBoxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtMasterPassword.PasswordChar = checkBoxShowPassword.Checked ? '\0' : '•';
        }

        private async void btnUnlock_Click(object sender, EventArgs e)
        {
            await AuthenticateUser();
        }

        private async void btnCreateNew_Click(object sender, EventArgs e)
        {
            await CreateNewDatabase();
        }

        private async Task AuthenticateUser()
        {
            if (!ValidatePasswordInput("Введите мастер-пароль для доступа к базе"))
                return;

            SetControlsEnabled(false);

            try
            {
                byte[] encryptedData = File.ReadAllBytes(databasePath);

                if (encryptedData.Length < SALT_SIZE + IV_SIZE)
                    throw new InvalidDataException("Файл базы поврежден");

                byte[] salt = new byte[SALT_SIZE];
                byte[] iv = new byte[IV_SIZE];

                Buffer.BlockCopy(encryptedData, 0, salt, 0, SALT_SIZE);
                Buffer.BlockCopy(encryptedData, SALT_SIZE, iv, 0, IV_SIZE);

                byte[] key;
                using (var pbkdf2 = new Rfc2898DeriveBytes(txtMasterPassword.Text, salt, ITERATIONS))
                    key = pbkdf2.GetBytes(KEY_SIZE);

                if (TryDecryptData(encryptedData, key, iv))
                {
                    AppState.MasterPassword = txtMasterPassword.Text;
                    OpenMainForm();
                }
                else
                {
                    await HandleInvalidPassword();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                CheckDatabaseExists();
            }
            finally
            {
                SetControlsEnabled(true);
                ClearPasswordField();
            }
        }

        private bool TryDecryptData(byte[] encryptedData, byte[] key, byte[] iv)
        {
            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    using (var ms = new MemoryStream())
                    using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        int dataStart = SALT_SIZE + IV_SIZE;
                        cs.Write(encryptedData, dataStart, encryptedData.Length - dataStart);
                        cs.FlushFinalBlock();

                        byte[] decryptedData = ms.ToArray();
                        string json = Encoding.UTF8.GetString(decryptedData);
                        return !string.IsNullOrWhiteSpace(json) &&
                               (json.Contains("{") || json.Contains("["));
                    }
                }
            }
            catch (CryptographicException)
            {
                return false;
            }
        }

        private async Task CreateNewDatabase()
        {
            string action = databaseExists ? "создать новую" : "создать";

            if (!ValidatePasswordInput($"Введите мастер-пароль для {action} базы данных", true))
                return;

            if (databaseExists)
            {
                var result = MessageBox.Show(
                    "ВНИМАНИЕ: Существующая база будет перезаписана!\n\nСоздать новую базу?",
                    "Подтверждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result != DialogResult.Yes)
                    return;
            }

            SetControlsEnabled(false);

            try
            {
                if (!Directory.Exists(appDataFolder))
                    Directory.CreateDirectory(appDataFolder);

                CreateEncryptedDatabase(txtMasterPassword.Text);

                AppState.MasterPassword = txtMasterPassword.Text;

                MessageBox.Show("База данных создана!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                OpenMainForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetControlsEnabled(true);
                ClearPasswordField();
                CheckDatabaseExists();
            }
        }

        private void CreateEncryptedDatabase(string password)
        {
            byte[] salt = GenerateRandomBytes(SALT_SIZE);

            byte[] key;
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, ITERATIONS))
                key = pbkdf2.GetBytes(KEY_SIZE);

            string emptyDatabase = $@"{{
                ""version"": 1,
                ""entries"": [],
                ""categories"": [{{ ""id"": 1, ""name"": ""Без категории"" }}],
                ""created"": ""{DateTime.Now:yyyy-MM-dd HH:mm:ss}"",
                ""modified"": ""{DateTime.Now:yyyy-MM-dd HH:mm:ss}""
            }}";

            byte[] data = Encoding.UTF8.GetBytes(emptyDatabase);

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.GenerateIV();

                using (var ms = new MemoryStream())
                {
                    ms.Write(salt, 0, salt.Length);
                    ms.Write(aes.IV, 0, aes.IV.Length);

                    using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                        cs.Write(data, 0, data.Length);

                    File.WriteAllBytes(databasePath, ms.ToArray());
                }
            }
        }

        private bool ValidatePasswordInput(string message, bool checkLength = false)
        {
            if (string.IsNullOrWhiteSpace(txtMasterPassword.Text))
            {
                MessageBox.Show($"{message}.", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMasterPassword.Focus();
                return false;
            }

            if (checkLength && txtMasterPassword.Text.Length < MIN_PASSWORD_LENGTH)
            {
                MessageBox.Show($"Минимум {MIN_PASSWORD_LENGTH} символов",
                    "Слабый пароль", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMasterPassword.Focus();
                txtMasterPassword.SelectAll();
                return false;
            }

            return true;
        }

        private async Task HandleInvalidPassword()
        {
            SetControlsEnabled(false);

            MessageBox.Show("Неверный мастер-пароль",
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

            await Task.Delay(ERROR_DELAY);

            SetControlsEnabled(true);
        }

        private byte[] GenerateRandomBytes(int length)
        {
            byte[] bytes = new byte[length];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
                rng.GetBytes(bytes);
            return bytes;
        }

        private void SetControlsEnabled(bool enabled)
        {
            txtMasterPassword.Enabled = enabled;
            btnUnlock.Enabled = enabled && databaseExists;
            btnCreateNew.Enabled = enabled;
            checkBoxShowPassword.Enabled = enabled;
            Cursor = enabled ? Cursors.Default : Cursors.WaitCursor;
        }

        private void ClearPasswordField()
        {
            txtMasterPassword.Clear();
            txtMasterPassword.Focus();
        }

        private void OpenMainForm()
        {
            var mainForm = new MainForm();
            mainForm.FormClosed += (s, args) => Close();
            Hide();
            mainForm.Show();
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClearPasswordField();
            AppState.ClearSession();
        }
    }
}

