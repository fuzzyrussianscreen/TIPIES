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
    public partial class FormUnit : Form
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private string sPath = Path.Combine(Application.StartupPath, "TIPIES.db");
        public FormUnit()
        {
            InitializeComponent();
        }

        private void unit_Load(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath +
           ";New=False;Version=3";
            String selectCommand = "Select * from Unit";
            selectTable(ConnectionString, selectCommand);
            selectCommand = "Select NumberAccount from ChartAccounts";
            selectComboBox(ConnectionString, selectCommand);
            
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

        public void selectComboBox(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new
           SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new
           SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);

            toolStripComboBoxAccount.ComboBox.DataSource = null;
            toolStripComboBoxAccount.ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            toolStripComboBoxAccount.ComboBox.DisplayMember = "NumberAccount";
            toolStripComboBoxAccount.ComboBox.ValueMember = "Value";
            toolStripComboBoxAccount.ComboBox.DataSource = ds.Tables[0];
            toolStripComboBoxAccount.ComboBox.SelectedItem = 1;
            connect.Close();
        }

        public void selectData(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new
           SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new
           SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);

            toolStripTextBoxName.TextBox.DataBindings.Clear();
            toolStripComboBoxAccount.ComboBox.Text = ds.Tables[0].Rows[0].ItemArray[2].ToString();

            toolStripTextBoxName.TextBox.DataBindings.Add(new Binding("Text", ds.Tables[0], "Name", true));
            connect.Close();
        }
        private void ToolStripButtonAdd_Click(object sender, EventArgs e)
        {
            if ((toolStripTextBoxName.Text != ""))
            {
                if ((toolStripTextBoxName.Text.Length < 50))
                {
                    string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
                    String selectCommand = "select MAX(Id) from Unit";
                    object maxValue = selectValue(ConnectionString, selectCommand);
                    if (Convert.ToString(maxValue) == "")
                        maxValue = 0;
                    //вставка в таблицу MOL
                    string txtSQLQuery = "insert into Unit (Id, Name, ExpenseAccount) values (" +
                   (Convert.ToInt32(maxValue) + 1) + ", '" + toolStripTextBoxName.Text + "', '" + toolStripComboBoxAccount.Text + "')";
                    ExecuteQuery(txtSQLQuery);
                    //обновление dataGridView1
                    selectCommand = "select * from Unit";
                    refreshForm(ConnectionString, selectCommand);
                    toolStripTextBoxName.Text = "";
                    toolStripComboBoxAccount.Text = "";
                }
                else
                {
                    MessageBox.Show("Слишком длинное наименование", "Ошибка");
                }
            }
            else
            {
                MessageBox.Show("Пустое поле", "Ошибка");
            }
        }
        public object selectValue(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteCommand command = new SQLiteCommand(selectCommand, connect);
            SQLiteDataReader reader = command.ExecuteReader();
            object value = "";
            while (reader.Read())
            {
                value = reader[0];
            }
            connect.Close(); return value;
        }
        private void ExecuteQuery(string txtQuery)
        {
            sql_con = new SQLiteConnection("Data Source=" + sPath +
           ";Version=3;New=False;Compress=True;");
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }
        public void refreshForm(string ConnectionString, String selectCommand)
        {
            selectTable(ConnectionString, selectCommand);
            dataGridView1.Update();
            dataGridView1.Refresh();
            toolStripComboBoxAccount.Text = "";
            toolStripTextBoxName.Text = "";
        }

        private void DataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath +
           ";New=False;Version=3";
            string selectCommand = "Select * from Unit where Id= " + dataGridView1.SelectedCells[0].Value;
            selectData(ConnectionString, selectCommand);
        }

        private void ToolStripButtonChange_Click(object sender, EventArgs e)
        {
            if ((toolStripTextBoxName.Text != ""))
            {
                if ((toolStripTextBoxName.Text.Length < 50))
                {
                    int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
                    //получить значение Name выбранной строки
                    string valueId = dataGridView1[0, CurrentRow].Value.ToString();
                    string changeName = toolStripTextBoxName.Text;
                    string changeAccount = toolStripComboBoxAccount.Text;

                    //обновление Name
                    String selectCommand = "update Unit set Name='" + changeName + "' where Id = " + valueId;
                    string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
                    changeValue(ConnectionString, selectCommand);
                    selectCommand = "update Unit set ExpenseAccount='" + changeAccount + "' where Id = " + valueId;
                    changeValue(ConnectionString, selectCommand);
                    //обновление dataGridView1
                    selectCommand = "select * from Unit";
                    refreshForm(ConnectionString, selectCommand);
                    toolStripComboBoxAccount.Text = "";
                    toolStripTextBoxName.Text = "";
                }
                else
                {
                    MessageBox.Show("Слишком длинное наименование", "Ошибка");
                }
            }
            else
            {
                MessageBox.Show("Пустое поле", "Ошибка");
            }
        }

        private void ToolStripButtonDelete_Click(object sender, EventArgs e)
        {
            //выбрана строка CurrentRow
            int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
            //получить значение idMOL выбранной строки
            string valueId = dataGridView1[0, CurrentRow].Value.ToString();
            String selectCommand = "delete from Unit where Id=" + valueId;
            string ConnectionString = @"Data Source=" + sPath +
           ";New=False;Version=3";
            changeValue(ConnectionString, selectCommand);
            //обновление dataGridView1
            selectCommand = "select * from Unit";
            refreshForm(ConnectionString, selectCommand);
            toolStripComboBoxAccount.Text = "";
            toolStripTextBoxName.Text = "";
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
