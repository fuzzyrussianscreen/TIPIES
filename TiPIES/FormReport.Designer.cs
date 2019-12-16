namespace TiPIES
{
    partial class FormReport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.comboBoxTypeOperation = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBoxAccount = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxUnit = new System.Windows.Forms.ComboBox();
            this.comboBoxEmployee = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(31, 105);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(850, 369);
            this.dataGridView1.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(93, 496);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(788, 22);
            this.label4.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 496);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Итого";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(31, 12);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(141, 22);
            this.dateTimePicker1.TabIndex = 3;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(215, 12);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(155, 22);
            this.dateTimePicker2.TabIndex = 4;
            // 
            // comboBoxTypeOperation
            // 
            this.comboBoxTypeOperation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTypeOperation.FormattingEnabled = true;
            this.comboBoxTypeOperation.Location = new System.Drawing.Point(410, 10);
            this.comboBoxTypeOperation.Name = "comboBoxTypeOperation";
            this.comboBoxTypeOperation.Size = new System.Drawing.Size(171, 24);
            this.comboBoxTypeOperation.TabIndex = 5;
            this.comboBoxTypeOperation.SelectedIndexChanged += new System.EventHandler(this.ComboBoxTypeOperation_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(721, 59);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 34);
            this.button1.TabIndex = 6;
            this.button1.Text = "Сформировать";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // comboBoxAccount
            // 
            this.comboBoxAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAccount.Enabled = false;
            this.comboBoxAccount.FormattingEnabled = true;
            this.comboBoxAccount.Location = new System.Drawing.Point(31, 75);
            this.comboBoxAccount.Name = "comboBoxAccount";
            this.comboBoxAccount.Size = new System.Drawing.Size(141, 24);
            this.comboBoxAccount.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "Номер счёта";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(212, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Подразделение";
            // 
            // comboBoxUnit
            // 
            this.comboBoxUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUnit.Enabled = false;
            this.comboBoxUnit.FormattingEnabled = true;
            this.comboBoxUnit.Location = new System.Drawing.Point(215, 75);
            this.comboBoxUnit.Name = "comboBoxUnit";
            this.comboBoxUnit.Size = new System.Drawing.Size(141, 24);
            this.comboBoxUnit.TabIndex = 10;
            this.comboBoxUnit.SelectedIndexChanged += new System.EventHandler(this.ComboBoxUnit_SelectedIndexChanged);
            // 
            // comboBoxEmployee
            // 
            this.comboBoxEmployee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEmployee.Enabled = false;
            this.comboBoxEmployee.FormattingEnabled = true;
            this.comboBoxEmployee.Location = new System.Drawing.Point(377, 75);
            this.comboBoxEmployee.Name = "comboBoxEmployee";
            this.comboBoxEmployee.Size = new System.Drawing.Size(141, 24);
            this.comboBoxEmployee.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(374, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 17);
            this.label5.TabIndex = 11;
            this.label5.Text = "Сотрудник";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(685, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(196, 41);
            this.button2.TabIndex = 13;
            this.button2.Text = "Сохранить в ворд";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // FormReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 543);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.comboBoxEmployee);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxUnit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxAccount);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBoxTypeOperation);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FormReport";
            this.Text = "FormReport";
            this.Load += new System.EventHandler(this.FormReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.ComboBox comboBoxTypeOperation;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBoxAccount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxUnit;
        private System.Windows.Forms.ComboBox comboBoxEmployee;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button2;
    }
}