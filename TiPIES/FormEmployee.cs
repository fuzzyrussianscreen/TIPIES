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
using System.Text.RegularExpressions;

namespace TiPIES
{
    public partial class FormEmployee : Form
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private string sPath = Path.Combine(Application.StartupPath, "TIPIES.db");

        public FormEmployee()
        {
            InitializeComponent();
        }

        private void Employee_Load(object sender, System.EventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath +
           ";New=False;Version=3";
            String selectCommand = "Select * from Employee";
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

        private void ToolStripButtonAdd_Click(object sender, EventArgs e)
        {
            if ((toolStripTextBoxFullName.Text != "") && (toolStripTextBoxData.Text != ""))
            {
                if ((toolStripTextBoxFullName.Text.Length < 50) && (toolStripTextBoxData.Text.Length < 50))
                {
                    if (!(new Regex(@"[\d\W!#h]")).Match(toolStripTextBoxFullName.Text).Success)
                    {
                        string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
                        String selectCommand = "select MAX(PersonnelNumber) from Employee";
                        object maxValue = selectValue(ConnectionString, selectCommand);
                        if (Convert.ToString(maxValue) == "")
                            maxValue = 0;
                        //вставка в таблицу MOL
                        string txtSQLQuery = "insert into Employee (PersonnelNumber, FullName, PersonalData) values (" +
                       (Convert.ToInt32(maxValue) + 1) + ", '" + toolStripTextBoxFullName.Text + "', '" + toolStripTextBoxData.Text + "')";
                        ExecuteQuery(txtSQLQuery);
                        //обновление dataGridView1
                        selectCommand = "select * from Employee";
                        refreshForm(ConnectionString, selectCommand);
                        toolStripTextBoxFullName.Text = "";
                        toolStripTextBoxData.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Не буквы в текстовом поле", "Ошибка");
                    }
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
            toolStripTextBoxData.Text = "";
            toolStripTextBoxFullName.Text = "";
        }

        private void ToolStripButtonChange_Click(object sender, EventArgs e)
        {
            if ((toolStripTextBoxFullName.Text != "") && (toolStripTextBoxData.Text != ""))
            {
                if ((toolStripTextBoxFullName.Text.Length < 50) && (toolStripTextBoxData.Text.Length < 50))
                {
                    if (!(new Regex(@"[\d\W!#h]")).Match(toolStripTextBoxFullName.Text).Success)
                    {
                        int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
                    //получить значение Name выбранной строки
                    string valueId = dataGridView1[0, CurrentRow].Value.ToString();
                    string changeName = toolStripTextBoxFullName.Text;
                    string changeDate = toolStripTextBoxData.Text;
                    //обновление Name
                    String selectCommand = "update Employee set FullName='" + changeName + "' where PersonnelNumber = " + valueId;
                    string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
                    changeValue(ConnectionString, selectCommand);
                    selectCommand = "update Employee set PersonalData='" + changeDate + "' where PersonnelNumber = " + valueId;
                    changeValue(ConnectionString, selectCommand);
                    //обновление dataGridView1
                    selectCommand = "select * from Employee";
                    refreshForm(ConnectionString, selectCommand);
                    toolStripTextBoxData.Text = "";
                    toolStripTextBoxFullName.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Не буквы в текстовом поле", "Ошибка");
                    }
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
            String selectCommand = "delete from Employee where PersonnelNumber=" + valueId;
            string ConnectionString = @"Data Source=" + sPath +
           ";New=False;Version=3";
            changeValue(ConnectionString, selectCommand);
            //обновление dataGridView1
            selectCommand = "select * from Employee";
            refreshForm(ConnectionString, selectCommand);
            toolStripTextBoxData.Text = "";
            toolStripTextBoxFullName.Text = "";
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


        public void selectData(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new
           SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new
           SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            toolStripTextBoxData.TextBox.DataBindings.Clear();
            toolStripTextBoxFullName.TextBox.DataBindings.Clear();

            toolStripTextBoxData.TextBox.DataBindings.Add(new Binding("Text", ds.Tables[0], "PersonalData", true));
            toolStripTextBoxFullName.TextBox.DataBindings.Add(new Binding("Text", ds.Tables[0], "FullName", true));
            connect.Close();
        }


        private void DataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath +
           ";New=False;Version=3";
            string selectCommand = "Select * from Employee where PersonnelNumber= " + dataGridView1.SelectedCells[0].Value;
            selectData(ConnectionString, selectCommand);
        }
    }
}
