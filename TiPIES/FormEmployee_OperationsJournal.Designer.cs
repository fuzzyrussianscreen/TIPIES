namespace TiPIES
{
    partial class FormEmployee_OperationsJournal
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEmployee_OperationsJournal));
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxEmployeePersonnelNumber = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxTypeAccounting = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel8 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxSum = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButtonAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonChange = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label = new System.Windows.Forms.Label();
            this.textBoxNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxDate = new System.Windows.Forms.TextBox();
            this.textBoxMonth = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxTotal = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.toolStripComboBoxIdUnit = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = null;
            this.bindingNavigator1.CountItem = null;
            this.bindingNavigator1.DeleteItem = null;
            this.bindingNavigator1.Dock = System.Windows.Forms.DockStyle.Right;
            this.bindingNavigator1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripComboBoxEmployeePersonnelNumber,
            this.toolStripLabel6,
            this.toolStripComboBoxTypeAccounting,
            this.toolStripLabel8,
            this.toolStripTextBoxSum,
            this.toolStripButtonAdd,
            this.toolStripButtonChange,
            this.toolStripButtonDelete});
            this.bindingNavigator1.Location = new System.Drawing.Point(1071, 0);
            this.bindingNavigator1.MoveFirstItem = null;
            this.bindingNavigator1.MoveLastItem = null;
            this.bindingNavigator1.MoveNextItem = null;
            this.bindingNavigator1.MovePreviousItem = null;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = null;
            this.bindingNavigator1.Size = new System.Drawing.Size(140, 486);
            this.bindingNavigator1.TabIndex = 3;
            this.bindingNavigator1.Text = "bindingNavigator1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(137, 20);
            this.toolStripLabel1.Text = "Табельный номер";
            // 
            // toolStripComboBoxEmployeePersonnelNumber
            // 
            this.toolStripComboBoxEmployeePersonnelNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxEmployeePersonnelNumber.Name = "toolStripComboBoxEmployeePersonnelNumber";
            this.toolStripComboBoxEmployeePersonnelNumber.Size = new System.Drawing.Size(135, 28);
            // 
            // toolStripLabel6
            // 
            this.toolStripLabel6.Name = "toolStripLabel6";
            this.toolStripLabel6.Size = new System.Drawing.Size(137, 20);
            this.toolStripLabel6.Text = "Вид расчёта";
            // 
            // toolStripComboBoxTypeAccounting
            // 
            this.toolStripComboBoxTypeAccounting.Name = "toolStripComboBoxTypeAccounting";
            this.toolStripComboBoxTypeAccounting.Size = new System.Drawing.Size(135, 28);
            // 
            // toolStripLabel8
            // 
            this.toolStripLabel8.Name = "toolStripLabel8";
            this.toolStripLabel8.Size = new System.Drawing.Size(137, 20);
            this.toolStripLabel8.Text = "Сумма";
            // 
            // toolStripTextBoxSum
            // 
            this.toolStripTextBoxSum.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBoxSum.Name = "toolStripTextBoxSum";
            this.toolStripTextBoxSum.Size = new System.Drawing.Size(135, 27);
            // 
            // toolStripButtonAdd
            // 
            this.toolStripButtonAdd.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAdd.Image")));
            this.toolStripButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAdd.Name = "toolStripButtonAdd";
            this.toolStripButtonAdd.Size = new System.Drawing.Size(137, 24);
            this.toolStripButtonAdd.Text = "Добавить";
            this.toolStripButtonAdd.Click += new System.EventHandler(this.ToolStripButtonAdd_Click);
            // 
            // toolStripButtonChange
            // 
            this.toolStripButtonChange.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonChange.Image")));
            this.toolStripButtonChange.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonChange.Name = "toolStripButtonChange";
            this.toolStripButtonChange.Size = new System.Drawing.Size(137, 24);
            this.toolStripButtonChange.Text = "Изменить";
            this.toolStripButtonChange.Click += new System.EventHandler(this.ToolStripButtonChange_Click);
            // 
            // toolStripButtonDelete
            // 
            this.toolStripButtonDelete.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDelete.Image")));
            this.toolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDelete.Name = "toolStripButtonDelete";
            this.toolStripButtonDelete.Size = new System.Drawing.Size(137, 24);
            this.toolStripButtonDelete.Text = "Удалить";
            this.toolStripButtonDelete.Click += new System.EventHandler(this.ToolStripButtonDelete_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 104);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(1033, 297);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView1_RowHeaderMouseClick);
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(24, 15);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(93, 17);
            this.label.TabIndex = 5;
            this.label.Text = "Операция №";
            // 
            // textBoxNumber
            // 
            this.textBoxNumber.Enabled = false;
            this.textBoxNumber.Location = new System.Drawing.Point(142, 12);
            this.textBoxNumber.Name = "textBoxNumber";
            this.textBoxNumber.Size = new System.Drawing.Size(79, 22);
            this.textBoxNumber.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Дата";
            // 
            // textBoxDate
            // 
            this.textBoxDate.Location = new System.Drawing.Point(142, 43);
            this.textBoxDate.Name = "textBoxDate";
            this.textBoxDate.Size = new System.Drawing.Size(79, 22);
            this.textBoxDate.TabIndex = 8;
            // 
            // textBoxMonth
            // 
            this.textBoxMonth.Location = new System.Drawing.Point(142, 76);
            this.textBoxMonth.Name = "textBoxMonth";
            this.textBoxMonth.Size = new System.Drawing.Size(79, 22);
            this.textBoxMonth.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "Месяц расчёта";
            // 
            // textBoxTotal
            // 
            this.textBoxTotal.Enabled = false;
            this.textBoxTotal.Location = new System.Drawing.Point(142, 416);
            this.textBoxTotal.Name = "textBoxTotal";
            this.textBoxTotal.Size = new System.Drawing.Size(79, 22);
            this.textBoxTotal.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 416);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Итого";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(880, 416);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(154, 45);
            this.buttonSave.TabIndex = 13;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(227, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 17);
            this.label4.TabIndex = 14;
            this.label4.Text = "дд.мм.гг";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(227, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 17);
            this.label5.TabIndex = 15;
            this.label5.Text = "мм.гг";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(310, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(140, 17);
            this.label6.TabIndex = 16;
            this.label6.Text = "Код подразделения";
            // 
            // toolStripComboBoxIdUnit
            // 
            this.toolStripComboBoxIdUnit.FormattingEnabled = true;
            this.toolStripComboBoxIdUnit.Location = new System.Drawing.Point(322, 50);
            this.toolStripComboBoxIdUnit.Name = "toolStripComboBoxIdUnit";
            this.toolStripComboBoxIdUnit.Size = new System.Drawing.Size(165, 24);
            this.toolStripComboBoxIdUnit.TabIndex = 17;
            this.toolStripComboBoxIdUnit.SelectedIndexChanged += new System.EventHandler(this.ToolStripComboBoxIdUnit_SelectedIndexChanged);
            // 
            // FormEmployee_OperationsJournal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1211, 486);
            this.Controls.Add(this.toolStripComboBoxIdUnit);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxTotal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxMonth);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxNumber);
            this.Controls.Add(this.label);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.bindingNavigator1);
            this.Name = "FormEmployee_OperationsJournal";
            this.Text = "Добавление операции";
            this.Load += new System.EventHandler(this.OperationsJournal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxEmployeePersonnelNumber;
        private System.Windows.Forms.ToolStripLabel toolStripLabel6;
        private System.Windows.Forms.ToolStripLabel toolStripLabel8;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxSum;
        private System.Windows.Forms.ToolStripButton toolStripButtonAdd;
        private System.Windows.Forms.ToolStripButton toolStripButtonChange;
        private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxTypeAccounting;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.TextBox textBoxNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxDate;
        private System.Windows.Forms.TextBox textBoxMonth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxTotal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox toolStripComboBoxIdUnit;
    }
}