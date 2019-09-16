namespace Lab2
{
    partial class FormExcel
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.textBox = new System.Windows.Forms.TextBox();
            this.AddRowButton = new System.Windows.Forms.Button();
            this.AddColumnButton = new System.Windows.Forms.Button();
            this.DelRowButton = new System.Windows.Forms.Button();
            this.DelColumnButton = new System.Windows.Forms.Button();
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.HelpButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.OpenFile = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(12, 66);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(776, 317);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView_CellBeginEdit);
            this.dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellContentClick_1);
            this.dataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellEndEdit);
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(161, 30);
            this.textBox.Name = "textBox";
            this.textBox.ReadOnly = true;
            this.textBox.Size = new System.Drawing.Size(100, 20);
            this.textBox.TabIndex = 1;
            this.textBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // AddRowButton
            // 
            this.AddRowButton.Location = new System.Drawing.Point(284, 18);
            this.AddRowButton.Name = "AddRowButton";
            this.AddRowButton.Size = new System.Drawing.Size(75, 42);
            this.AddRowButton.TabIndex = 2;
            this.AddRowButton.Text = "Додати рядок";
            this.AddRowButton.UseVisualStyleBackColor = true;
            this.AddRowButton.Click += new System.EventHandler(this.AddRowButton_Click);
            // 
            // AddColumnButton
            // 
            this.AddColumnButton.Location = new System.Drawing.Point(459, 17);
            this.AddColumnButton.Name = "AddColumnButton";
            this.AddColumnButton.Size = new System.Drawing.Size(75, 42);
            this.AddColumnButton.TabIndex = 3;
            this.AddColumnButton.Text = "Додати стовпчик";
            this.AddColumnButton.UseVisualStyleBackColor = true;
            this.AddColumnButton.Click += new System.EventHandler(this.AddColumnButton_Click);
            // 
            // DelRowButton
            // 
            this.DelRowButton.Location = new System.Drawing.Point(365, 18);
            this.DelRowButton.Name = "DelRowButton";
            this.DelRowButton.Size = new System.Drawing.Size(75, 41);
            this.DelRowButton.TabIndex = 4;
            this.DelRowButton.Text = "Видалити рядок";
            this.DelRowButton.UseVisualStyleBackColor = true;
            this.DelRowButton.Click += new System.EventHandler(this.DelRowButton_Click);
            // 
            // DelColumnButton
            // 
            this.DelColumnButton.Location = new System.Drawing.Point(540, 18);
            this.DelColumnButton.Name = "DelColumnButton";
            this.DelColumnButton.Size = new System.Drawing.Size(75, 42);
            this.DelColumnButton.TabIndex = 5;
            this.DelColumnButton.Text = "Видалити стовпчик";
            this.DelColumnButton.UseVisualStyleBackColor = true;
            this.DelColumnButton.Click += new System.EventHandler(this.DelColumnButton_Click);
            // 
            // comboBox
            // 
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Items.AddRange(new object[] {
            "Вираз",
            "Значення"});
            this.comboBox.Location = new System.Drawing.Point(24, 30);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(121, 21);
            this.comboBox.TabIndex = 6;
            this.comboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // HelpButton
            // 
            this.HelpButton.Location = new System.Drawing.Point(713, 27);
            this.HelpButton.Name = "HelpButton";
            this.HelpButton.Size = new System.Drawing.Size(75, 23);
            this.HelpButton.TabIndex = 7;
            this.HelpButton.Text = "Допомога";
            this.HelpButton.UseVisualStyleBackColor = true;
            this.HelpButton.Click += new System.EventHandler(this.HelpButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(632, 17);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 8;
            this.SaveButton.Text = "Зберегти";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // OpenFile
            // 
            this.OpenFile.Location = new System.Drawing.Point(632, 37);
            this.OpenFile.Name = "OpenFile";
            this.OpenFile.Size = new System.Drawing.Size(75, 23);
            this.OpenFile.TabIndex = 9;
            this.OpenFile.Text = "Відкрити";
            this.OpenFile.UseVisualStyleBackColor = true;
            this.OpenFile.Click += new System.EventHandler(this.OpenFile_Click);
            // 
            // FormExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.OpenFile);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.HelpButton);
            this.Controls.Add(this.comboBox);
            this.Controls.Add(this.DelColumnButton);
            this.Controls.Add(this.DelRowButton);
            this.Controls.Add(this.AddColumnButton);
            this.Controls.Add(this.AddRowButton);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.dataGridView);
            this.Name = "FormExcel";
            this.Text = "ExcelByGavryliukOksana";
            this.Load += new System.EventHandler(this.FormExcel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button AddRowButton;
        private System.Windows.Forms.Button AddColumnButton;
        private System.Windows.Forms.Button DelRowButton;
        private System.Windows.Forms.Button DelColumnButton;
        private System.Windows.Forms.ComboBox comboBox;
        private System.Windows.Forms.Button HelpButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button OpenFile;
    }
}

