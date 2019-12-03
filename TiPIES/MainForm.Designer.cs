namespace TiPIES
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.справочникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.планСчетовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.подразделенияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.видыРасчётаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сотрудникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сотрудникиПоПодразделениямToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.журналПроводокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.журналПроводокToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.журналОперацийToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.отчётToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справочникиToolStripMenuItem,
            this.журналПроводокToolStripMenuItem,
            this.отчётToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // справочникиToolStripMenuItem
            // 
            this.справочникиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.планСчетовToolStripMenuItem,
            this.подразделенияToolStripMenuItem,
            this.видыРасчётаToolStripMenuItem,
            this.сотрудникиToolStripMenuItem,
            this.сотрудникиПоПодразделениямToolStripMenuItem});
            this.справочникиToolStripMenuItem.Name = "справочникиToolStripMenuItem";
            this.справочникиToolStripMenuItem.Size = new System.Drawing.Size(117, 24);
            this.справочникиToolStripMenuItem.Text = "Справочники";
            // 
            // планСчетовToolStripMenuItem
            // 
            this.планСчетовToolStripMenuItem.Name = "планСчетовToolStripMenuItem";
            this.планСчетовToolStripMenuItem.Size = new System.Drawing.Size(319, 26);
            this.планСчетовToolStripMenuItem.Text = "План счетов";
            this.планСчетовToolStripMenuItem.Click += new System.EventHandler(this.ПланСчетовToolStripMenuItem_Click);
            // 
            // подразделенияToolStripMenuItem
            // 
            this.подразделенияToolStripMenuItem.Name = "подразделенияToolStripMenuItem";
            this.подразделенияToolStripMenuItem.Size = new System.Drawing.Size(319, 26);
            this.подразделенияToolStripMenuItem.Text = "Подразделения";
            this.подразделенияToolStripMenuItem.Click += new System.EventHandler(this.ПодразделенияToolStripMenuItem_Click);
            // 
            // видыРасчётаToolStripMenuItem
            // 
            this.видыРасчётаToolStripMenuItem.Name = "видыРасчётаToolStripMenuItem";
            this.видыРасчётаToolStripMenuItem.Size = new System.Drawing.Size(319, 26);
            this.видыРасчётаToolStripMenuItem.Text = "Виды расчёта";
            this.видыРасчётаToolStripMenuItem.Click += new System.EventHandler(this.ВидыРасчётаToolStripMenuItem_Click);
            // 
            // сотрудникиToolStripMenuItem
            // 
            this.сотрудникиToolStripMenuItem.Name = "сотрудникиToolStripMenuItem";
            this.сотрудникиToolStripMenuItem.Size = new System.Drawing.Size(319, 26);
            this.сотрудникиToolStripMenuItem.Text = "Сотрудники";
            this.сотрудникиToolStripMenuItem.Click += new System.EventHandler(this.СотрудникиToolStripMenuItem_Click);
            // 
            // сотрудникиПоПодразделениямToolStripMenuItem
            // 
            this.сотрудникиПоПодразделениямToolStripMenuItem.Name = "сотрудникиПоПодразделениямToolStripMenuItem";
            this.сотрудникиПоПодразделениямToolStripMenuItem.Size = new System.Drawing.Size(319, 26);
            this.сотрудникиПоПодразделениямToolStripMenuItem.Text = "Сотрудники по подразделениям";
            this.сотрудникиПоПодразделениямToolStripMenuItem.Click += new System.EventHandler(this.СотрудникиПоПодразделениямToolStripMenuItem_Click);
            // 
            // журналПроводокToolStripMenuItem
            // 
            this.журналПроводокToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.журналПроводокToolStripMenuItem1,
            this.журналОперацийToolStripMenuItem1});
            this.журналПроводокToolStripMenuItem.Name = "журналПроводокToolStripMenuItem";
            this.журналПроводокToolStripMenuItem.Size = new System.Drawing.Size(88, 24);
            this.журналПроводокToolStripMenuItem.Text = "Журналы";
            // 
            // журналПроводокToolStripMenuItem1
            // 
            this.журналПроводокToolStripMenuItem1.Name = "журналПроводокToolStripMenuItem1";
            this.журналПроводокToolStripMenuItem1.Size = new System.Drawing.Size(224, 26);
            this.журналПроводокToolStripMenuItem1.Text = "Журнал проводок";
            this.журналПроводокToolStripMenuItem1.Click += new System.EventHandler(this.ЖурналПроводокToolStripMenuItem1_Click);
            // 
            // журналОперацийToolStripMenuItem1
            // 
            this.журналОперацийToolStripMenuItem1.Name = "журналОперацийToolStripMenuItem1";
            this.журналОперацийToolStripMenuItem1.Size = new System.Drawing.Size(224, 26);
            this.журналОперацийToolStripMenuItem1.Text = "Журнал операций";
            this.журналОперацийToolStripMenuItem1.Click += new System.EventHandler(this.ЖурналОперацийToolStripMenuItem1_Click);
            // 
            // отчётToolStripMenuItem
            // 
            this.отчётToolStripMenuItem.Name = "отчётToolStripMenuItem";
            this.отчётToolStripMenuItem.Size = new System.Drawing.Size(62, 24);
            this.отчётToolStripMenuItem.Text = "Отчёт";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Главное меню";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem справочникиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem планСчетовToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem подразделенияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem видыРасчётаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сотрудникиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem журналПроводокToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отчётToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem журналПроводокToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem журналОперацийToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem сотрудникиПоПодразделениямToolStripMenuItem;
    }
}

