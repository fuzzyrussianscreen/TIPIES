using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SQLite;
using System.IO;

namespace TiPIES
{
    public partial class FormEmployee_OperationsJournal : Form
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private string sPath = Path.Combine(Application.StartupPath, "TIPIES.db");
        public FormEmployee_OperationsJournal()
        {
            InitializeComponent();
        }

        private void OperationsJournal_Load(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath +
           ";New=False;Version=3";
            String selectCommand = "Select * from Employee_JournalOperation";
            selectTable(ConnectionString, selectCommand);

            selectCommand = "Select PersonnelNumber from Employee";
            selectComboBoxNumber(ConnectionString, selectCommand);
            selectCommand = "Select Id from Unit";
            selectComboBoxIdUnit(ConnectionString, selectCommand);
            selectCommand = "Select Id from TypeAccounting";
            selectComboBoxIdTypeAccounting(ConnectionString, selectCommand);
        }

        public void selectComboBoxNumber(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new
           SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new
           SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            toolStripComboBoxEmployeePersonnelNumber.ComboBox.DataSource = null;
            toolStripComboBoxEmployeePersonnelNumber.ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            toolStripComboBoxEmployeePersonnelNumber.ComboBox.DisplayMember = "PersonnelNumber";
            toolStripComboBoxEmployeePersonnelNumber.ComboBox.ValueMember = "Value";
            toolStripComboBoxEmployeePersonnelNumber.ComboBox.DataSource = ds.Tables[0];
            toolStripComboBoxEmployeePersonnelNumber.ComboBox.SelectedItem = 1;
            connect.Close();
        }

        public void selectComboBoxIdUnit(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new
           SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new
           SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            toolStripComboBoxIdUnit.ComboBox.DataSource = null;
            toolStripComboBoxIdUnit.ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            toolStripComboBoxIdUnit.ComboBox.DisplayMember = "Id";
            toolStripComboBoxIdUnit.ComboBox.ValueMember = "Value";
            toolStripComboBoxIdUnit.ComboBox.DataSource = ds.Tables[0];
            toolStripComboBoxIdUnit.ComboBox.SelectedItem = 1;
            connect.Close();
        }
        public void selectComboBoxIdTypeAccounting(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new
           SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new
           SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            toolStripComboBoxTypeAccounting.ComboBox.DataSource = null;
            toolStripComboBoxTypeAccounting.ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            toolStripComboBoxTypeAccounting.ComboBox.DisplayMember = "Id";
            toolStripComboBoxTypeAccounting.ComboBox.ValueMember = "Value";
            toolStripComboBoxTypeAccounting.ComboBox.DataSource = ds.Tables[0];
            toolStripComboBoxTypeAccounting.ComboBox.SelectedItem = 1;
            connect.Close();
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

            toolStripTextBoxSum.Clear();

            toolStripComboBoxEmployeePersonnelNumber.SelectedIndex = 1;
            toolStripComboBoxTypeAccounting.SelectedIndex = 1;
            toolStripComboBoxIdUnit.SelectedIndex = 1;
        }

        private void ToolStripButtonDelete_Click(object sender, EventArgs e)
        {
            toolStripTextBoxSum.Clear();

            toolStripComboBoxEmployeePersonnelNumber.SelectedIndex = 1;
            toolStripComboBoxTypeAccounting.SelectedIndex = 1;
            toolStripComboBoxIdUnit.SelectedIndex = 1;
        }

        private void ToolStripButtonAdd_Click(object sender, EventArgs e)
        {
            check("Add");
        }

        private void ToolStripButtonChange_Click(object sender, EventArgs e)
        {
            check("Change");
        }

        private void check(string operation)
        {
            if (toolStripTextBoxSum.Text != "")
            {
                if (toolStripTextBoxSum.Text.Length < 9)
                {
                    if ((new Regex(@"^\d$")).Match(toolStripTextBoxSum.Text).Success)
                    {
                        toolStripTextBoxSum.Text = toolStripTextBoxSum.Text + ",00";
                    }
                    if ((new Regex(@"\d,\d{2}$")).Match(toolStripTextBoxSum.Text).Success)
                    {

                        if (operation == "Add")
                        {
                            if (checkData())
                            {
                                string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
                                //вставка в таблицу MOL
                                string txtSQLQuery = "insert into Employee_JournalOperation (EmployeePersonnelNumber, EmployeePersonnelNumber, Post, Salary, WorkPeriod) values (" +
                               toolStripComboBoxIdUnit.Text + ", " + toolStripComboBoxEmployeePersonnelNumber.Text + ", '" + toolStripTextBoxPost.Text + "', '" + toolStripTextBoxSalary.Text + "', '" + changePeriod + "')";
                                ExecuteQuery(txtSQLQuery);
                                //обновление dataGridView1
                                String selectCommand = "select * from Unit_Employee";

                                refreshForm(ConnectionString, selectCommand);
                            }
                            else
                            {
                                MessageBox.Show("Объект уже существует", "Ошибка");
                            }
                        }
                        else
                        {
                            int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
                            //получить значение Name выбранной строки
                            string valueId = dataGridView1[0, CurrentRow].Value.ToString();
                            string valueId2 = dataGridView1[1, CurrentRow].Value.ToString();
                            string changePost = toolStripTextBoxPost.Text;
                            string changePeriod = toolStripTextBoxPeriod1.Text + "-" + toolStripTextBoxPeriod2.Text;
                            string changeSalary = toolStripTextBoxSalary.Text;

                            //обновление Name
                            string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
                            String selectCommand = "update Unit_Employee set Post='" + changePost + "' where IdUnit=" + valueId + " AND " + "EmployeePersonnelNumber=" + valueId2;
                            changeValue(ConnectionString, selectCommand);

                            selectCommand = "update Unit_Employee set WorkPeriod='" + changePeriod + "' where IdUnit=" + valueId + " AND " + "EmployeePersonnelNumber=" + valueId2;
                            changeValue(ConnectionString, selectCommand);

                            selectCommand = "update Unit_Employee set Salary='" + changeSalary + "' where IdUnit=" + valueId + " AND " + "EmployeePersonnelNumber=" + valueId2;
                            changeValue(ConnectionString, selectCommand);
                            //обновление dataGridView1
                            selectCommand = "select * from Unit_Employee";
                            refreshForm(ConnectionString, selectCommand);
                            toolStripTextBoxPeriod2.Text = "";
                            toolStripTextBoxPost.Text = "";
                            toolStripTextBoxSalary.Text = "";
                            toolStripComboBoxIdUnit.SelectedItem = 1;
                            toolStripComboBoxEmployeePersonnelNumber.SelectedItem = 1;
                        }

                    }
                    else
                    {
                        MessageBox.Show("Неверная сумма", "Ошибка");
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

        public bool checkData()
        {
            string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
            string selectCommand = "Select * from Unit_Employee where IdUnit = " + toolStripComboBoxIdUnit.Text + " AND " + "EmployeePersonnelNumber = " + toolStripComboBoxEmployeePersonnelNumber.Text;

            SQLiteConnection connect = new
           SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new
           SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);

            connect.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if ((toolStripTextBoxPost.Text != "") && (toolStripTextBoxPeriod1.Text != "") && (toolStripTextBoxPeriod2.Text != "") && (toolStripTextBoxSalary.Text != ""))
            {
                if ((toolStripTextBoxPost.Text.Length < 30) && (toolStripTextBoxSalary.Text.Length < 18) && (toolStripTextBoxPeriod1.Text.Length < 9) && (toolStripTextBoxPeriod2.Text.Length < 9))
                {
                    if (!(new Regex(@"[\d\W!#h]")).Match(toolStripTextBoxPost.Text).Success)
                    {
                        if ((new Regex(@"^\d$")).Match(toolStripTextBoxSalary.Text).Success)
                        {
                            toolStripTextBoxSalary.Text = toolStripTextBoxSalary.Text + ",00";
                        }
                        if ((new Regex(@"\d,\d{2}$")).Match(toolStripTextBoxSalary.Text).Success)
                        {
                            if ((new Regex(@"\d{2}.\d{2}.\d{2}$")).Match(toolStripTextBoxPeriod1.Text).Success && (new Regex(@"\d{2}.\d{2}.\d{2}$")).Match(toolStripTextBoxPeriod2.Text).Success)
                            {
                                string[] period = toolStripTextBoxPeriod1.Text.Split('.');
                                string[] period2 = toolStripTextBoxPeriod2.Text.Split('.');

                                if (Convert.ToInt32(period[0]) <= 31 && Convert.ToInt32(period2[0]) <= 31 && Convert.ToInt32(period[1]) < 13
                                    && Convert.ToInt32(period2[1]) < 13)
                                {

                                }
                                else
                                {
                                    MessageBox.Show("Неверные даты", "Ошибка");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Неверные даты", "Ошибка");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Неверная сумма", "Ошибка");
                        }
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
    }
}
