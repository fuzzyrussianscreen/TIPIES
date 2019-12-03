using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace TiPIES
{
    public partial class FormOperationsJournal : Form
    {
        
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private string sPath = Path.Combine("D:\\ФИСТ ИСЭбд\\Repos\\TIPIES\\TIPIES.db");
        public FormOperationsJournal()
        {
            InitializeComponent();
        }

        private void OperationsJournal_Load(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath +
           ";New=False;Version=3";
            String selectCommand = "Select * from OperationsJournal";
            selectTable(ConnectionString, selectCommand);

        }

        public void selectTable(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new
           SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new
           SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);

            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = ds.Tables[0].ToString();
            connect.Close();
        }

        private void ButtonCreate_Click(object sender, EventArgs e)
        {
            Form formOperationsJournal = new FormEmployee_OperationsJournal(null);
            formOperationsJournal.ShowDialog();
            string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
            String selectCommand = "select * from OperationsJournal";
            refreshForm(ConnectionString, selectCommand);
        }

        public void refreshForm(string ConnectionString, String selectCommand)
        {
            selectTable(ConnectionString, selectCommand);
            dataGridView1.Update();
            dataGridView1.Refresh();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells[0].RowIndex>=0)
            {
                int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
                Form formOperationsJournal = new FormEmployee_OperationsJournal(Convert.ToInt32(dataGridView1[0, CurrentRow].Value.ToString()));
                formOperationsJournal.ShowDialog();
                string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
                String selectCommand = "select * from OperationsJournal";
                refreshForm(ConnectionString, selectCommand);
            }
            else
            {
                MessageBox.Show("Не выбрана операция", "Ошибка");
            }
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells[0].RowIndex >= 0)
            {
                //выбрана строка CurrentRow
                int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
                //получить значение idMOL выбранной строки
                string valueId = dataGridView1[0, CurrentRow].Value.ToString();

                String selectCommand = "delete from Employee_JournalOperation where IdJournalOperations=" + valueId;
                String ConnectionString = @"Data Source=" + sPath +
               ";New=False;Version=3";
                changeValue(ConnectionString, selectCommand);

                selectCommand = "delete from OperationsJournal where Id=" + valueId;
                ConnectionString = @"Data Source=" + sPath +
               ";New=False;Version=3";
                changeValue(ConnectionString, selectCommand);


                //обновление dataGridView1
                selectCommand = "select * from OperationsJournal";
                refreshForm(ConnectionString, selectCommand);
            }
            else
            {
                MessageBox.Show("Не выбрана операция", "Ошибка");
            }
        }

        public void changeValue(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new
           SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteTransaction trans;
            SQLiteCommand cmd = new SQLiteCommand();
            trans = connect.BeginTransaction();
            cmd.Connection = connect;
            cmd.CommandText = selectCommand;
            cmd.ExecuteNonQuery();
            trans.Commit();
            connect.Close();
        }
    }
}
