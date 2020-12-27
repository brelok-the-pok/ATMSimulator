namespace AtmTerminal
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.AccountButton = new System.Windows.Forms.Button();
            this.GetMoneyButton = new System.Windows.Forms.Button();
            this.TransferButton = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.cardBox = new System.Windows.Forms.ComboBox();
            this.helpLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // AccountButton
            // 
            this.AccountButton.Enabled = false;
            this.AccountButton.Location = new System.Drawing.Point(12, 12);
            this.AccountButton.Name = "AccountButton";
            this.AccountButton.Size = new System.Drawing.Size(251, 68);
            this.AccountButton.TabIndex = 0;
            this.AccountButton.Text = "Выписка по счёту";
            this.AccountButton.UseVisualStyleBackColor = true;
            this.AccountButton.Click += new System.EventHandler(this.AccountButton_Click);
            // 
            // GetMoneyButton
            // 
            this.GetMoneyButton.Enabled = false;
            this.GetMoneyButton.Location = new System.Drawing.Point(12, 120);
            this.GetMoneyButton.Name = "GetMoneyButton";
            this.GetMoneyButton.Size = new System.Drawing.Size(251, 68);
            this.GetMoneyButton.TabIndex = 1;
            this.GetMoneyButton.Text = "Снять со счёта";
            this.GetMoneyButton.UseVisualStyleBackColor = true;
            this.GetMoneyButton.Click += new System.EventHandler(this.GetMoneyButton_Click);
            // 
            // TransferButton
            // 
            this.TransferButton.Enabled = false;
            this.TransferButton.Location = new System.Drawing.Point(12, 220);
            this.TransferButton.Name = "TransferButton";
            this.TransferButton.Size = new System.Drawing.Size(251, 68);
            this.TransferButton.TabIndex = 2;
            this.TransferButton.Text = "Перевод на другой счёт";
            this.TransferButton.UseVisualStyleBackColor = true;
            this.TransferButton.Click += new System.EventHandler(this.TransferButton_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Enabled = false;
            this.checkBox1.Location = new System.Drawing.Point(12, 294);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(112, 17);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "Карта вставлена";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
            // 
            // cardBox
            // 
            this.cardBox.FormattingEnabled = true;
            this.cardBox.Items.AddRange(new object[] {
            "1111 1111 1111 1111",
            "2222 2222 2222 2222",
            "3333 3333 3333 3333",
            "4444 4444 4444 4444"});
            this.cardBox.Location = new System.Drawing.Point(12, 317);
            this.cardBox.Name = "cardBox";
            this.cardBox.Size = new System.Drawing.Size(251, 21);
            this.cardBox.TabIndex = 4;
            this.cardBox.Text = "Выберите карту из списка";
            this.cardBox.SelectedIndexChanged += new System.EventHandler(this.cardBox_SelectedIndexChanged);
            // 
            // helpLabel
            // 
            this.helpLabel.AutoSize = true;
            this.helpLabel.ForeColor = System.Drawing.Color.Red;
            this.helpLabel.Location = new System.Drawing.Point(12, 351);
            this.helpLabel.Name = "helpLabel";
            this.helpLabel.Size = new System.Drawing.Size(186, 13);
            this.helpLabel.TabIndex = 5;
            this.helpLabel.Text = "Сначала выберите карту из списка";
            this.helpLabel.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 373);
            this.Controls.Add(this.helpLabel);
            this.Controls.Add(this.cardBox);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.TransferButton);
            this.Controls.Add(this.GetMoneyButton);
            this.Controls.Add(this.AccountButton);
            this.Name = "Form1";
            this.Text = "Терминал";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AccountButton;
        private System.Windows.Forms.Button GetMoneyButton;
        private System.Windows.Forms.Button TransferButton;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox cardBox;
        private System.Windows.Forms.Label helpLabel;
    }
}

