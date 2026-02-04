namespace PasswordManager
{
    partial class PasswordGeneratorForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.NumericUpDown numLength;
        private System.Windows.Forms.CheckBox cbLowercase;
        private System.Windows.Forms.CheckBox cbUppercase;
        private System.Windows.Forms.CheckBox cbNumbers;
        private System.Windows.Forms.CheckBox cbSymbols;
        private System.Windows.Forms.TextBox txtPreview;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.numLength = new System.Windows.Forms.NumericUpDown();
            this.cbLowercase = new System.Windows.Forms.CheckBox();
            this.cbUppercase = new System.Windows.Forms.CheckBox();
            this.cbNumbers = new System.Windows.Forms.CheckBox();
            this.cbSymbols = new System.Windows.Forms.CheckBox();
            this.txtPreview = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numLength)).BeginInit();
            this.SuspendLayout();
            // 
            // numLength
            // 
            this.numLength.Location = new System.Drawing.Point(30, 58);
            this.numLength.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.numLength.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.numLength.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numLength.Name = "numLength";
            this.numLength.Size = new System.Drawing.Size(200, 31);
            this.numLength.TabIndex = 0;
            this.numLength.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numLength.ValueChanged += new System.EventHandler(this.numLength_ValueChanged);
            // 
            // cbLowercase
            // 
            this.cbLowercase.AutoSize = true;
            this.cbLowercase.Checked = true;
            this.cbLowercase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLowercase.Location = new System.Drawing.Point(30, 146);
            this.cbLowercase.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cbLowercase.Name = "cbLowercase";
            this.cbLowercase.Size = new System.Drawing.Size(259, 29);
            this.cbLowercase.TabIndex = 1;
            this.cbLowercase.Text = "Строчные буквы (a-z)";
            this.cbLowercase.UseVisualStyleBackColor = true;
            this.cbLowercase.CheckedChanged += new System.EventHandler(this.cbType_CheckedChanged);
            // 
            // cbUppercase
            // 
            this.cbUppercase.AutoSize = true;
            this.cbUppercase.Checked = true;
            this.cbUppercase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUppercase.Location = new System.Drawing.Point(30, 183);
            this.cbUppercase.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cbUppercase.Name = "cbUppercase";
            this.cbUppercase.Size = new System.Drawing.Size(271, 29);
            this.cbUppercase.TabIndex = 2;
            this.cbUppercase.Text = "Заглавные буквы (A-Z)";
            this.cbUppercase.UseVisualStyleBackColor = true;
            this.cbUppercase.CheckedChanged += new System.EventHandler(this.cbType_CheckedChanged);
            // 
            // cbNumbers
            // 
            this.cbNumbers.AutoSize = true;
            this.cbNumbers.Checked = true;
            this.cbNumbers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbNumbers.Location = new System.Drawing.Point(30, 231);
            this.cbNumbers.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cbNumbers.Name = "cbNumbers";
            this.cbNumbers.Size = new System.Drawing.Size(167, 29);
            this.cbNumbers.TabIndex = 3;
            this.cbNumbers.Text = "Цифры (0-9)";
            this.cbNumbers.UseVisualStyleBackColor = true;
            this.cbNumbers.CheckedChanged += new System.EventHandler(this.cbType_CheckedChanged);
            // 
            // cbSymbols
            // 
            this.cbSymbols.AutoSize = true;
            this.cbSymbols.Checked = true;
            this.cbSymbols.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSymbols.Location = new System.Drawing.Point(30, 279);
            this.cbSymbols.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cbSymbols.Name = "cbSymbols";
            this.cbSymbols.Size = new System.Drawing.Size(195, 29);
            this.cbSymbols.TabIndex = 4;
            this.cbSymbols.Text = "Символы (!@#)";
            this.cbSymbols.UseVisualStyleBackColor = true;
            this.cbSymbols.CheckedChanged += new System.EventHandler(this.cbType_CheckedChanged);
            // 
            // txtPreview
            // 
            this.txtPreview.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtPreview.Location = new System.Drawing.Point(30, 356);
            this.txtPreview.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtPreview.Name = "txtPreview";
            this.txtPreview.ReadOnly = true;
            this.txtPreview.Size = new System.Drawing.Size(496, 35);
            this.txtPreview.TabIndex = 5;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(250, 58);
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(280, 44);
            this.btnGenerate.TabIndex = 6;
            this.btnGenerate.Text = "Сгенерировать";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(30, 423);
            this.btnApply.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(150, 44);
            this.btnApply.TabIndex = 7;
            this.btnApply.Text = "Применить";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(210, 423);
            this.btnCopy.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(150, 44);
            this.btnCopy.TabIndex = 8;
            this.btnCopy.Text = "Копировать";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(390, 423);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(150, 44);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 25);
            this.label1.TabIndex = 10;
            this.label1.Text = "Длина пароля:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 115);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 25);
            this.label2.TabIndex = 11;
            this.label2.Text = "Использовать:";
            // 
            // PasswordGeneratorForm
            // 
            this.AcceptButton = this.btnApply;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(568, 502);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.txtPreview);
            this.Controls.Add(this.cbSymbols);
            this.Controls.Add(this.cbNumbers);
            this.Controls.Add(this.cbUppercase);
            this.Controls.Add(this.cbLowercase);
            this.Controls.Add(this.numLength);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PasswordGeneratorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Генератор паролей";
            ((System.ComponentModel.ISupportInitialize)(this.numLength)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}