using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TiPIES
{
    public partial class FormPostingJournal : Form
    {
        
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private string sPath = Path.Combine("D:\\ФИСТ ИСЭбд\\Repos\\TIPIES\\TIPIES.db");
        public FormPostingJournal()
        {
            InitializeComponent();
        }

        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath +
           ";New=False;Version=3";
            String selectCommand = "Select * from PostingJournal where Date between '" + dateTimePicker1.Value.ToString(dateTimePicker1.CustomFormat.ToString()) + "' and '" + dateTimePicker2.Value.ToString(dateTimePicker2.CustomFormat.ToString()) + "'";
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

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            PostingJournal journal = new PostingJournal();
            string UnitId;

            string ConnectionString = @"Data Source=" + sPath +
           ";New=False;Version=3";

            String selectCommand = "delete from PostingJournal";

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

            selectCommand = "Select IdJournalOperations, EmployeePersonnelNumber, IdAccounting, Sum from Employee_JournalOperation";
            
            SQLiteDataAdapter dataAdapter = new
           SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);

            connect.Close();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                selectCommand = "select UnitId from OperationsJournal where Id=" + row.ItemArray[0].ToString();
                UnitId = selectValue(ConnectionString, selectCommand);

                journal.addPostingJournal(row.ItemArray[0].ToString(), row.ItemArray[1].ToString(), row.ItemArray[2].ToString(), row.ItemArray[3].ToString(), UnitId);
            }
        }

        public string selectValue(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteCommand command = new SQLiteCommand(selectCommand, connect);
            SQLiteDataReader reader = command.ExecuteReader();
            string value = "";
            while (reader.Read())
            {
                value = reader[0].ToString();
            }
            connect.Close(); return value;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath +
           ";New=False;Version=3";
            String selectCommand = "Select * from PostingJournal";
            selectTable(ConnectionString, selectCommand);

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
    }
}
