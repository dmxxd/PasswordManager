using System;
using System.Windows.Forms;

namespace PasswordManager
{
    public partial class EntryForm : Form
    {
        public string EntryName => txtName.Text.Trim();
        public string Category => cmbCategory.Text.Trim();
        public string Login => txtLogin.Text.Trim();
        public string Password => txtPassword.Text;
        public string Notes => txtNotes.Text.Trim();

        private string[] existingCategories;

        public EntryForm(string[] categories)
        {
            InitializeComponent();
            existingCategories = categories;
            LoadCategories();
        }

        private void LoadCategories()
        {
            cmbCategory.Items.Clear();
            cmbCategory.Items.Add("");
            if (existingCategories != null)
            {
                cmbCategory.Items.AddRange(existingCategories);
            }
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDown;
        }
        public void SetEntryName(string name) => txtName.Text = name;
        public void SetCategory(string category) => cmbCategory.Text = category;
        public void SetLogin(string login) => txtLogin.Text = login;
        public void SetPassword(string password) => txtPassword.Text = password;
        public void SetNotes(string notes) => txtNotes.Text = notes;

        private void btnShow_Click(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = txtPassword.PasswordChar == '•' ? '\0' : '•';
            btnShow.Text = txtPassword.PasswordChar == '•' ? "Показать" : "Скрыть";
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            using (PasswordGeneratorForm generatorForm = new PasswordGeneratorForm())
            {
                if (generatorForm.ShowDialog(this) == DialogResult.OK)
                {
                    string generatedPassword = generatorForm.GeneratedPassword;
                    if (!string.IsNullOrEmpty(generatedPassword))
                    {
                        txtPassword.Text = generatedPassword;
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите название записи", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Введите пароль", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void EntryForm_Load(object sender, EventArgs e)
        {
            txtName.Focus();
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtLogin.Focus();
            }
        }

        private void txtLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtNotes.Focus();
            }
        }

        private void txtNotes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                if (Control.ModifierKeys == Keys.Shift)
                {
                    txtNotes.AppendText(Environment.NewLine);
                }
                else
                {
                    btnSave.PerformClick();
                }
            }
        }
    }
}