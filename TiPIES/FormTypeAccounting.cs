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
    public partial class FormTypeAccounting : Form
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private string sPath = Path.Combine("D:\\ФИСТ ИСЭбд\\Repos\\TIPIES\\TIPIES.db");
        public FormTypeAccounting()
        {
            InitializeComponent();
        }

        private void TypeAccounting_Load(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath +
           ";New=False;Version=3";
            String selectCommand = "Select * from TypeAccounting";
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
            if ((toolStripTextBoxName.Text != "") && (toolStripTextBoxType.Text != "") && (toolStripTextBoxPercent.Text != ""))
            {
                if ((toolStripTextBoxName.Text.Length < 15) && (toolStripTextBoxType.Text.Length < 15))
                {
                    if ((new Regex(@"^\d$")).Match(toolStripTextBoxPercent.Text).Success)
                    {
                        toolStripTextBoxPercent.Text = toolStripTextBoxPercent.Text + ",00";
                    }
                    if ((new Regex(@"\d,\d{2}$")).Match(toolStripTextBoxPercent.Text).Success && Convert.ToDouble(toolStripTextBoxPercent.Text) >= 0 && Convert.ToDouble(toolStripTextBoxPercent.Text) <= 100)
                    {
                        string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
                    String selectCommand = "select MAX(Id) from TypeAccounting";
                    object maxValue = selectValue(ConnectionString, selectCommand);
                    if (Convert.ToString(maxValue) == "")
                        maxValue = 0;
                    //вставка в таблицу MOL
                    string txtSQLQuery = "insert into TypeAccounting (Id, Name, Type, Percent) values (" +
                   (Convert.ToInt32(maxValue) + 1) + ", '" + toolStripTextBoxName.Text + "', '" + toolStripTextBoxType.Text + "', '" + toolStripTextBoxPercent.Text + "')";
                    ExecuteQuery(txtSQLQuery);
                    //обновление dataGridView1
                    selectCommand = "select * from TypeAccounting";
                    refreshForm(ConnectionString, selectCommand);
                    toolStripTextBoxName.Text = "";
                    toolStripTextBoxType.Text = "";
                    toolStripTextBoxPercent.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Не корректный процент", "Ошибка");
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
            toolStripTextBoxType.Text = "";
            toolStripTextBoxName.Text = "";
            toolStripTextBoxPercent.Text = "";
        }

        private void ToolStripButtonChange_Click(object sender, EventArgs e)
        {
            if ((toolStripTextBoxName.Text != "") && (toolStripTextBoxType.Text != "") && (toolStripTextBoxPercent.Text != ""))
            {
                if ((toolStripTextBoxName.Text.Length < 15) && (toolStripTextBoxType.Text.Length < 15))
                {
                    if ((new Regex(@"^\d$")).Match(toolStripTextBoxPercent.Text).Success)
                    {
                        toolStripTextBoxPercent.Text = toolStripTextBoxPercent.Text + ",00";
                    }
                    if ((new Regex(@"\d,\d{2}$")).Match(toolStripTextBoxPercent.Text).Success && Convert.ToDouble(toolStripTextBoxPercent.Text) >= 0 && Convert.ToDouble(toolStripTextBoxPercent.Text) <= 100)
                    {
                        int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
                        //получить значение Name выбранной строки
                        string valueId = dataGridView1[0, CurrentRow].Value.ToString();
                        string changeName = toolStripTextBoxName.Text;
                        string changeType = toolStripTextBoxType.Text;
                        string changePercent = toolStripTextBoxPercent.Text;

                        //обновление Name
                        String selectCommand = "update TypeAccounting set Name='" + changeName + "' where Id = " + valueId;
                        string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
                        changeValue(ConnectionString, selectCommand);

                        selectCommand = "update TypeAccounting set Type='" + changeType + "' where Id = " + valueId;
                        changeValue(ConnectionString, selectCommand);

                        selectCommand = "update TypeAccounting set Percent='" + changePercent + "' where Id = " + valueId;
                        changeValue(ConnectionString, selectCommand);

                        //обновление dataGridView1
                        selectCommand = "select * from TypeAccounting";
                        refreshForm(ConnectionString, selectCommand);
                        toolStripTextBoxType.Text = "";
                        toolStripTextBoxName.Text = "";
                        toolStripTextBoxPercent.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Не корректный процент", "Ошибка");
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
            String selectCommand = "delete from TypeAccounting where Id=" + valueId;
            string ConnectionString = @"Data Source=" + sPath +
           ";New=False;Version=3";
            changeValue(ConnectionString, selectCommand);
            //обновление dataGridView1
            selectCommand = "select * from TypeAccounting";
            refreshForm(ConnectionString, selectCommand);
            toolStripTextBoxType.Text = "";
            toolStripTextBoxName.Text = "";
            toolStripTextBoxPercent.Text = "";
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

        private void DataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath +
           ";New=False;Version=3";
            string selectCommand = "Select * from TypeAccounting where Id= " + dataGridView1.SelectedCells[0].Value;
            selectData(ConnectionString, selectCommand);
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

            toolStripTextBoxType.TextBox.DataBindings.Clear();
            toolStripTextBoxName.TextBox.DataBindings.Clear();
            toolStripTextBoxPercent.TextBox.DataBindings.Clear();

            toolStripTextBoxType.TextBox.DataBindings.Add(new Binding("Text", ds.Tables[0], "Type", true));
            toolStripTextBoxName.TextBox.DataBindings.Add(new Binding("Text", ds.Tables[0], "Name", true));
            toolStripTextBoxPercent.TextBox.DataBindings.Add(new Binding("Text", ds.Tables[0], "Percent", true));

            connect.Close();
        }
    }
}
