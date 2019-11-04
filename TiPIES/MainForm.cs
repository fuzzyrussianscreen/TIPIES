using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TiPIES
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void ПланСчетовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form formChartsAccount = new FormChartAccounts();
            formChartsAccount.ShowDialog();
        }

        private void ПодразделенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form formUnit = new FormUnit();
            formUnit.ShowDialog();
        }

        private void СотрудникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form formEmployee = new FormEmployee();
            formEmployee.ShowDialog();
        }

        private void ВидыРасчётаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form formTypeAccounting = new FormTypeAccounting();
            formTypeAccounting.ShowDialog();
        }
        

        private void СотрудникиПоПодразделениямToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form formUnit_Employee = new FormUnit_Employee();
            formUnit_Employee.ShowDialog();
        }

        private void ЖурналОперацийToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form formOperationsJournal = new FormOperationsJournal();
            formOperationsJournal.ShowDialog();
        }
    }
}
