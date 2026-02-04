using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PasswordManager
{
    public partial class PasswordGeneratorForm : Form
    {
        public string GeneratedPassword { get; private set; }
        private Random random = new Random();

        public PasswordGeneratorForm()
        {
            InitializeComponent();
            GeneratedPassword = GeneratePassword();
            txtPreview.Text = GeneratedPassword;
        }

        private string GeneratePassword()
        {
            int length = (int)numLength.Value;

            StringBuilder chars = new StringBuilder();
            if (cbLowercase.Checked) chars.Append("abcdefghijklmnopqrstuvwxyz");
            if (cbUppercase.Checked) chars.Append("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            if (cbNumbers.Checked) chars.Append("0123456789");
            if (cbSymbols.Checked) chars.Append("!@#$%^&*()_-+=[]{}|;:,.<>?/");

            if (chars.Length == 0)
            {
                MessageBox.Show("Выберите хотя бы один тип символов", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbLowercase.Checked = true;
                chars.Append("abcdefghijklmnopqrstuvwxyz");
                return string.Empty;
            }

            StringBuilder password = new StringBuilder();

            if (cbLowercase.Checked) password.Append("abcdefghijklmnopqrstuvwxyz"[random.Next(26)]);
            if (cbUppercase.Checked) password.Append("ABCDEFGHIJKLMNOPQRSTUVWXYZ"[random.Next(26)]);
            if (cbNumbers.Checked) password.Append("0123456789"[random.Next(10)]);
            if (cbSymbols.Checked) password.Append("!@#$%^&*()_-+=[]{}|;:,.<>?/"[random.Next("!@#$%^&*()_-+=[]{}|;:,.<>?/".Length)]);

            while (password.Length < length)
            {
                password.Append(chars[random.Next(chars.Length)]);
            }
            return ShuffleString(password.ToString());
        }

        private string ShuffleString(string input)
        {
            char[] array = input.ToCharArray();

            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                char temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }

            return new string(array);
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            GeneratedPassword = GeneratePassword();
            txtPreview.Text = GeneratedPassword;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            GeneratedPassword = txtPreview.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPreview.Text))
            {
                try
                {
                    Clipboard.SetText(txtPreview.Text);
                    MessageBox.Show("Пароль скопирован в буфер обмена", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка копирования: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void numLength_ValueChanged(object sender, EventArgs e)
        {
            GeneratedPassword = GeneratePassword();
            txtPreview.Text = GeneratedPassword;
        }

        private void cbType_CheckedChanged(object sender, EventArgs e)
        {
            GeneratedPassword = GeneratePassword();
            txtPreview.Text = GeneratedPassword;
        }
    }
}