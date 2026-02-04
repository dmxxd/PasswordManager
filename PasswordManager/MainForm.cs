using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace PasswordManager
{
    public partial class MainForm : Form
    {
        private List<PasswordEntry> passwordEntries = new List<PasswordEntry>();
        private string dataFilePath = "passwords.json";
        private string encryptionKey = "mySecretKey1234567890123456789012"; // 32 символа = 256 бит
        private byte[] salt = Encoding.UTF8.GetBytes("MySalt1234567890"); // Соль для PBKDF2

        public MainForm()
        {
            InitializeComponent();
            LoadData();
            RefreshTreeView();
            RefreshDataGridView();
        }

        private class PasswordEntry
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Category { get; set; }
            public string Login { get; set; }
            public string Password { get; set; }
            public string Notes { get; set; }
            public DateTime Created { get; set; }
            public DateTime Modified { get; set; }
        }

        private byte[] GenerateKey()
        {
            // Используем PBKDF2 для создания безопасного ключа
            using (var deriveBytes = new Rfc2898DeriveBytes(encryptionKey, salt, 10000, HashAlgorithmName.SHA256))
            {
                return deriveBytes.GetBytes(32); // 256 бит
            }
        }

        private string Encrypt(string plainText)
        {
            try
            {
                if (string.IsNullOrEmpty(plainText))
                    return string.Empty;

                byte[] key = GenerateKey();
                using (Aes aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.GenerateIV(); // Генерируем случайный IV каждый раз
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        // Записываем IV в начало потока
                        ms.Write(aes.IV, 0, aes.IV.Length);

                        using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter sw = new StreamWriter(cs))
                            {
                                sw.Write(plainText);
                            }
                        }

                        byte[] encrypted = ms.ToArray();
                        return Convert.ToBase64String(encrypted);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка шифрования: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }

        private string Decrypt(string cipherText)
        {
            try
            {
                if (string.IsNullOrEmpty(cipherText))
                    return string.Empty;

                byte[] fullCipher = Convert.FromBase64String(cipherText);

                if (fullCipher.Length < 16)
                    return string.Empty;

                // Извлекаем IV из начала данных
                byte[] iv = new byte[16];
                byte[] cipherBytes = new byte[fullCipher.Length - 16];

                Array.Copy(fullCipher, 0, iv, 0, iv.Length);
                Array.Copy(fullCipher, iv.Length, cipherBytes, 0, cipherBytes.Length);

                byte[] key = GenerateKey();
                using (Aes aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                    using (MemoryStream ms = new MemoryStream(cipherBytes))
                    {
                        using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader sr = new StreamReader(cs))
                            {
                                return sr.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (FormatException)
            {
                // Неверный формат Base64
                return string.Empty;
            }
            catch (CryptographicException)
            {
                // Ошибка дешифрования (неверный ключ или поврежденные данные)
                return string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка дешифрования: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }

        private void LoadData()
        {
            try
            {
                if (File.Exists(dataFilePath))
                {
                    string encryptedJson = File.ReadAllText(dataFilePath);
                    if (!string.IsNullOrEmpty(encryptedJson))
                    {
                        string decryptedJson = Decrypt(encryptedJson);
                        if (!string.IsNullOrEmpty(decryptedJson))
                        {
                            try
                            {
                                passwordEntries = JsonSerializer.Deserialize<List<PasswordEntry>>(decryptedJson);
                            }
                            catch (JsonException)
                            {
                                // Если не удалось десериализовать, пробуем старый формат
                                try
                                {
                                    // Проверяем, не незашифрованный ли это JSON
                                    if (encryptedJson.Trim().StartsWith("["))
                                    {
                                        passwordEntries = JsonSerializer.Deserialize<List<PasswordEntry>>(encryptedJson);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Файл данных поврежден или создан другой версией программы.", "Ошибка",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        passwordEntries = new List<PasswordEntry>();
                                    }
                                }
                                catch
                                {
                                    MessageBox.Show("Не удалось прочитать файл данных. Создан новый список паролей.", "Ошибка",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    passwordEntries = new List<PasswordEntry>();
                                }
                            }
                        }
                        else
                        {
                            // Пробуем прочитать как незашифрованный файл
                            try
                            {
                                if (encryptedJson.Trim().StartsWith("["))
                                {
                                    passwordEntries = JsonSerializer.Deserialize<List<PasswordEntry>>(encryptedJson);
                                }
                                else
                                {
                                    MessageBox.Show("Не удалось расшифровать файл данных. Создан новый список паролей.", "Ошибка",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    passwordEntries = new List<PasswordEntry>();
                                }
                            }
                            catch
                            {
                                passwordEntries = new List<PasswordEntry>();
                            }
                        }
                    }
                    else
                    {
                        passwordEntries = new List<PasswordEntry>();
                    }
                }
                else
                {
                    passwordEntries = new List<PasswordEntry>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                passwordEntries = new List<PasswordEntry>();
            }
        }

        private void SaveData()
        {
            try
            {
                string json = JsonSerializer.Serialize(passwordEntries,
                    new JsonSerializerOptions { WriteIndented = true });

                string encryptedJson = Encrypt(json);

                if (string.IsNullOrEmpty(encryptedJson))
                {
                    throw new Exception("Шифрование не удалось");
                }

                // Сохраняем с безопасной перезаписью
                SaveDataSafely(encryptedJson);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveDataSafely(string encryptedData)
        {
            string tempFile = Path.GetTempFileName();

            try
            {
                // Записываем во временный файл
                File.WriteAllText(tempFile, encryptedData);

                // Копируем временный файл в целевое расположение
                File.Copy(tempFile, dataFilePath, true);

                // Удаляем временный файл
                File.Delete(tempFile);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Отказано в доступе. Сохраните файл в другом месте.", "Ошибка доступа",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                SaveDataWithDialog(encryptedData);
            }
            catch (IOException ioEx)
            {
                MessageBox.Show($"Ошибка ввода-вывода: {ioEx.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                SaveDataWithDialog(encryptedData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Неожиданная ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveDataWithDialog(string encryptedData)
        {
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "JSON файлы (*.json)|*.json|Все файлы (*.*)|*.*";
                saveDialog.Title = "Сохранить данные в другом месте";
                saveDialog.FileName = dataFilePath;

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.WriteAllText(saveDialog.FileName, encryptedData);
                        dataFilePath = saveDialog.FileName;
                        MessageBox.Show($"Данные сохранены в: {dataFilePath}", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Не удалось сохранить в выбранное место: {ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void RefreshTreeView()
        {
            treeCategories.Nodes.Clear();

            TreeNode allNode = new TreeNode("Все записи");
            allNode.Tag = "ALL";
            treeCategories.Nodes.Add(allNode);

            HashSet<string> categories = new HashSet<string>();
            foreach (var entry in passwordEntries)
            {
                if (!string.IsNullOrEmpty(entry.Category))
                {
                    categories.Add(entry.Category);
                }
            }

            if (passwordEntries.Any(e => string.IsNullOrEmpty(e.Category)))
            {
                TreeNode noCatNode = new TreeNode("Без категории");
                noCatNode.Tag = "";
                treeCategories.Nodes.Add(noCatNode);
            }

            List<string> sortedCategories = categories.ToList();
            sortedCategories.Sort();

            foreach (string category in sortedCategories)
            {
                TreeNode node = new TreeNode(category);
                node.Tag = category;
                treeCategories.Nodes.Add(node);
            }

            if (treeCategories.Nodes.Count > 0)
            {
                treeCategories.SelectedNode = treeCategories.Nodes[0];
            }
        }

        private void RefreshDataGridView()
        {
            gridEntries.Rows.Clear();

            string selectedCategory = treeCategories.SelectedNode?.Tag as string;
            string searchText = txtSearch.Text.ToLower();

            foreach (var entry in passwordEntries)
            {
                if (selectedCategory != "ALL")
                {
                    if (selectedCategory == "" && !string.IsNullOrEmpty(entry.Category))
                        continue;
                    if (selectedCategory != "" && entry.Category != selectedCategory)
                        continue;
                }

                if (!string.IsNullOrEmpty(searchText))
                {
                    bool matches = entry.Name.ToLower().Contains(searchText) ||
                                   entry.Login.ToLower().Contains(searchText) ||
                                   (entry.Category?.ToLower().Contains(searchText) ?? false) ||
                                   (entry.Notes?.ToLower().Contains(searchText) ?? false);
                    if (!matches) continue;
                }

                gridEntries.Rows.Add(
                    entry.Id,
                    entry.Name,
                    entry.Category ?? "Без категории",
                    entry.Login,
                    "••••••••",
                    entry.Notes
                );
            }

            UpdateStatusBar();
        }

        private void UpdateStatusBar()
        {
            int totalCount = passwordEntries.Count;
            int filteredCount = gridEntries.Rows.Count;

            if (totalCount == filteredCount)
                lblStatus.Text = $"Всего записей: {totalCount}";
            else
                lblStatus.Text = $"Показано: {filteredCount} из {totalCount}";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var categories = passwordEntries
                .Select(p => p.Category)
                .Where(c => !string.IsNullOrEmpty(c))
                .Distinct()
                .ToArray();

            using (var dialog = new EntryForm(categories))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var newEntry = new PasswordEntry
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = dialog.EntryName,
                        Category = dialog.Category,
                        Login = dialog.Login,
                        Password = dialog.Password,
                        Notes = dialog.Notes,
                        Created = DateTime.Now,
                        Modified = DateTime.Now
                    };

                    passwordEntries.Add(newEntry);
                    SaveData();
                    RefreshTreeView();
                    RefreshDataGridView();

                    // Выделяем новую запись
                    foreach (DataGridViewRow row in gridEntries.Rows)
                    {
                        if (row.Cells["colId"].Value?.ToString() == newEntry.Id)
                        {
                            row.Selected = true;
                            gridEntries.FirstDisplayedScrollingRowIndex = row.Index;
                            break;
                        }
                    }

                    MessageBox.Show("Запись успешно добавлена", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (gridEntries.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите запись для редактирования", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string entryId = gridEntries.SelectedRows[0].Cells["colId"].Value?.ToString();
            if (string.IsNullOrEmpty(entryId)) return;

            var entry = passwordEntries.Find(item => item.Id == entryId);
            if (entry != null)
            {
                var categories = passwordEntries
                    .Select(p => p.Category)
                    .Where(c => !string.IsNullOrEmpty(c))
                    .Distinct()
                    .ToArray();

                using (var dialog = new EntryForm(categories))
                {
                    dialog.SetEntryName(entry.Name);
                    dialog.SetCategory(entry.Category);
                    dialog.SetLogin(entry.Login);
                    dialog.SetPassword(entry.Password);
                    dialog.SetNotes(entry.Notes);

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        entry.Name = dialog.EntryName;
                        entry.Category = dialog.Category;
                        entry.Login = dialog.Login;
                        entry.Password = dialog.Password;
                        entry.Notes = dialog.Notes;
                        entry.Modified = DateTime.Now;

                        SaveData();
                        RefreshTreeView();
                        RefreshDataGridView();

                        MessageBox.Show("Запись успешно обновлена", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gridEntries.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите запись для удаления", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Вы уверены, что хотите удалить выбранную запись?", "Подтверждение удаления",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string entryId = gridEntries.SelectedRows[0].Cells["colId"].Value?.ToString();
                if (!string.IsNullOrEmpty(entryId))
                {
                    passwordEntries.RemoveAll(item => item.Id == entryId);
                    SaveData();
                    RefreshTreeView();
                    RefreshDataGridView();

                    MessageBox.Show("Запись успешно удалена", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnCopyLogin_Click(object sender, EventArgs e)
        {
            if (gridEntries.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите запись", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string entryId = gridEntries.SelectedRows[0].Cells["colId"].Value?.ToString();
            var entry = passwordEntries.Find(item => item.Id == entryId);

            if (entry != null)
            {
                try
                {
                    Clipboard.SetText(entry.Login);
                    MessageBox.Show("Логин скопирован в буфер обмена", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка копирования: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCopyPass_Click(object sender, EventArgs e)
        {
            if (gridEntries.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите запись", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string entryId = gridEntries.SelectedRows[0].Cells["colId"].Value?.ToString();
            var entry = passwordEntries.Find(item => item.Id == entryId);

            if (entry != null)
            {
                try
                {
                    Clipboard.SetText(entry.Password);
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

        private void btnAddCat_Click(object sender, EventArgs e)
        {
            string categoryName = Microsoft.VisualBasic.Interaction.InputBox(
                "Введите название новой категории:",
                "Добавить категорию",
                "");

            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                bool categoryExists = false;
                foreach (TreeNode node in treeCategories.Nodes)
                {
                    if (node.Tag != null && node.Tag.ToString() != "ALL" && node.Text == categoryName)
                    {
                        categoryExists = true;
                        MessageBox.Show($"Категория '{categoryName}' уже существует", "Информация",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        treeCategories.SelectedNode = node;
                        break;
                    }
                }

                if (!categoryExists)
                {
                    var tempEntry = new PasswordEntry
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Новая категория: " + categoryName,
                        Category = categoryName,
                        Login = "",
                        Password = "",
                        Notes = "Временная запись для отображения категории. Удалите после создания реальных записей.",
                        Created = DateTime.Now,
                        Modified = DateTime.Now
                    };

                    passwordEntries.Add(tempEntry);
                    SaveData();
                    RefreshTreeView();

                    foreach (TreeNode node in treeCategories.Nodes)
                    {
                        if (node.Text == categoryName)
                        {
                            treeCategories.SelectedNode = node;
                            break;
                        }
                    }

                    MessageBox.Show($"Категория '{categoryName}' добавлена. Создана временная запись для отображения категории.\n" +
                                   "Вы можете удалить эту запись после создания реальных записей с этой категорией.",
                                   "Категория добавлена",
                                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void treeCategories_AfterSelect(object sender, TreeViewEventArgs e)
        {
            RefreshDataGridView();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }

        private void gridEntries_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnEdit_Click(sender, e);
            }
        }

        private void gridEntries_SelectionChanged(object sender, EventArgs e)
        {
            bool hasSelection = gridEntries.SelectedRows.Count > 0;
            btnEdit.Enabled = hasSelection;
            btnDelete.Enabled = hasSelection;
            btnCopyLogin.Enabled = hasSelection;
            btnCopyPass.Enabled = hasSelection;
            btnShowPass.Enabled = hasSelection;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveData();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "JSON файлы (*.json)|*.json|Все файлы (*.*)|*.*";
                saveDialog.Title = "Экспорт данных";
                saveDialog.FileName = "passwords_export.json";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var exportData = passwordEntries.Select(entry => new
                        {
                            entry.Name,
                            entry.Category,
                            entry.Login,
                            Password = "******",
                            entry.Notes,
                            entry.Created,
                            entry.Modified
                        }).ToList();

                        string json = JsonSerializer.Serialize(exportData,
                            new JsonSerializerOptions { WriteIndented = true });
                        File.WriteAllText(saveDialog.FileName, json);

                        MessageBox.Show($"Данные успешно экспортированы в файл:\n{saveDialog.FileName}",
                            "Экспорт завершен", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка экспорта: {ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnShowPass_Click(object sender, EventArgs e)
        {
            if (gridEntries.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите запись", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string entryId = gridEntries.SelectedRows[0].Cells["colId"].Value?.ToString();
            var entry = passwordEntries.Find(item => item.Id == entryId);

            if (entry != null)
            {
                DataGridViewRow selectedRow = gridEntries.SelectedRows[0];
                bool isHidden = selectedRow.Cells["colPassword"].Value?.ToString() == "••••••••";

                if (isHidden)
                {
                    selectedRow.Cells["colPassword"].Value = entry.Password;
                    btnShowPass.Text = "Скрыть пароль";
                }
                else
                {
                    selectedRow.Cells["colPassword"].Value = "••••••••";
                    btnShowPass.Text = "Показать пароль";
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            if (treeCategories.Nodes.Count > 0)
            {
                treeCategories.SelectedNode = treeCategories.Nodes[0];
            }
            RefreshDataGridView();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                string backupDir = "Backups";
                if (!Directory.Exists(backupDir))
                {
                    Directory.CreateDirectory(backupDir);
                }

                string backupFile = Path.Combine(backupDir,
                    $"passwords_backup_{DateTime.Now:yyyyMMdd_HHmmss}.json");

                if (File.Exists(dataFilePath))
                {
                    File.Copy(dataFilePath, backupFile, true);
                    MessageBox.Show($"Резервная копия создана:\n{backupFile}", "Резервное копирование",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Файл данных не найден", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка создания резервной копии: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}