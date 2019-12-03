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
        private string sPath = Path.Combine("D:\\ФИСТ ИСЭбд\\Repos\\TIPIES\\TIPIES.db");

        private int? ID = null;
        public FormEmployee_OperationsJournal(int? ID)
        {
            this.ID = ID;
            InitializeComponent();
        }

        private void OperationsJournal_Load(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
            String selectCommand;

            selectCommand = "Select Id from Unit";
            selectComboBoxIdUnit(ConnectionString, selectCommand);
            selectCommand = "Select Id from TypeAccounting";
            selectComboBoxIdTypeAccounting(ConnectionString, selectCommand);

            ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";

            if (ID == null)
            {
                selectCommand = "select MAX(Id) from OperationsJournal";
                object maxValue = selectValue(ConnectionString, selectCommand);

                if (Convert.ToString(maxValue) == "")
                    maxValue = 0;
                textBoxNumber.Text = (Convert.ToInt32(maxValue) + 1).ToString();
            }
            else
            {
                textBoxNumber.Text = ID.ToString();

                selectCommand = "Select * from OperationsJournal where Id =" + textBoxNumber.Text;
                selectOperation(ConnectionString, selectCommand);

                selectCommand = "Select * from Employee_JournalOperation where IdJournalOperations = " + textBoxNumber.Text;
                selectTable(ConnectionString, selectCommand);
            }

            updateSum();
        }

        public void selectOperation(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new
           SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new
           SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);

            textBoxDate.DataBindings.Add(new Binding("Text", ds.Tables[0], "DateOperation", true));
            textBoxMonth.DataBindings.Add(new Binding("Text", ds.Tables[0], "PeriodOperation", true));

            toolStripComboBoxIdUnit.Text = ds.Tables[0].Rows[0].ItemArray[1].ToString();

            connect.Close();
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

        public void selectComboBoxNumber(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new
           SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new
           SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);

            toolStripComboBoxEmployeePersonnelNumber.ComboBox.Items.Clear();

            foreach ( DataRow row in ds.Tables[0].Rows)
            {
                string[] period = row.ItemArray[1].ToString().Split('-');
                string[] period1 = period[0].Split('.');
                string[] period2 = period[1].Split('.');

                string[] month = textBoxMonth.Text.Split('.');

                if ((Convert.ToInt32(period1[2]) <= Convert.ToInt32(month[1])) && (Convert.ToInt32(month[1]) <= Convert.ToInt32(period2[2])))
                {
                    if ((Convert.ToInt32(period1[2]) < Convert.ToInt32(month[1])) && (Convert.ToInt32(month[1]) < Convert.ToInt32(period2[2])))
                    {
                        if (row.ItemArray[2].ToString() == toolStripComboBoxIdUnit.Text)
                        {
                            toolStripComboBoxEmployeePersonnelNumber.ComboBox.Items.Add(row.ItemArray[0] + " за " + row.ItemArray[1]);
                            toolStripComboBoxEmployeePersonnelNumber.SelectedItem = 1;
                        }
                    }
                    if ((Convert.ToInt32(period1[2]) == Convert.ToInt32(month[1])) && (Convert.ToInt32(period1[1]) <= Convert.ToInt32(month[0])) ||
                    ((Convert.ToInt32(month[1]) == Convert.ToInt32(period2[2]))) && (Convert.ToInt32(month[0]) <= Convert.ToInt32(period2[1])))
                    {
                        if (row.ItemArray[2].ToString() == toolStripComboBoxIdUnit.Text)
                        {
                            toolStripComboBoxEmployeePersonnelNumber.ComboBox.Items.Add(row.ItemArray[0] + " за " + row.ItemArray[1]);
                            toolStripComboBoxEmployeePersonnelNumber.SelectedItem = 1;
                        }
                    }

                }

            }

            connect.Close();
        }

        private void ToolStripComboBoxIdUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (textBoxMonth.Text != "")
            {
                string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
                String selectCommand = "Select EmployeePersonnelNumber, WorkPeriod, IdUnit from Unit_Employee";
                selectComboBoxNumber(ConnectionString, selectCommand);

                if (toolStripComboBoxEmployeePersonnelNumber.Items.Count == 0)
                {
                    MessageBox.Show("Нет сотрудников за этот период", "Ошибка");
                }
            }
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
            toolStripComboBoxIdUnit.DataSource = null;
            toolStripComboBoxIdUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            toolStripComboBoxIdUnit.DisplayMember = "Id";
            toolStripComboBoxIdUnit.ValueMember = "Value";
            toolStripComboBoxIdUnit.DataSource = ds.Tables[0];
            toolStripComboBoxIdUnit.SelectedItem = 1;
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

            selectCommand = "Select * from Employee_JournalOperation where IdJournalOperations = " + textBoxNumber.Text;
            selectTable(ConnectionString, selectCommand);

            dataGridView1.Update();
            dataGridView1.Refresh();

            toolStripTextBoxSum.Clear();

            toolStripComboBoxEmployeePersonnelNumber.SelectedIndex = 1;
            toolStripComboBoxIdUnit.SelectedIndex = 1;
        }

        public void updateSum()
        {
            double total = 0;
            for (int i=0; i< dataGridView1.Rows.Count; i++)
            {
                total = total + Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
            }
            textBoxTotal.Text = total.ToString();

        }

        private void ToolStripButtonDelete_Click(object sender, EventArgs e)
        {
            //выбрана строка CurrentRow
            int CurrentRow = dataGridView1.SelectedCells[0].RowIndex;
            //получить значение idMOL выбранной строки
            string valueId = dataGridView1[0, CurrentRow].Value.ToString();
            String selectCommand = "delete from Employee_JournalOperation where EmployeePersonnelNumber=" + valueId;
            string ConnectionString = @"Data Source=" + sPath +
           ";New=False;Version=3";
            changeValue(ConnectionString, selectCommand);
            //обновление dataGridView1
            selectCommand = "Select * from Employee_JournalOperation where IdJournalOperations = " + textBoxNumber.Text;
            refreshForm(ConnectionString, selectCommand);

            toolStripTextBoxSum.Clear();

            toolStripComboBoxEmployeePersonnelNumber.SelectedIndex = 1;
            toolStripComboBoxTypeAccounting.SelectedIndex = 1;
            toolStripComboBoxIdUnit.SelectedIndex = 1;

            updateSum();
        }

        private void ToolStripButtonAdd_Click(object sender, EventArgs e)
        {
            if ((textBoxDate.Text != "") && (textBoxMonth.Text != ""))
            {
                if (checkDataOperation())
                {
                    saveOperation();
                }
                check("Add");
            }
            else
            {
                MessageBox.Show("Не заполнены поля операции", "Ошибка");
            }
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
                    if ((new Regex(@"^[0-9]$")).Match(toolStripTextBoxSum.Text).Success)
                    {
                        toolStripTextBoxSum.Text = toolStripTextBoxSum.Text + ",00";
                    }
                    if ((new Regex(@"[0-9]\,[0-9]{2}$")).Match(toolStripTextBoxSum.Text).Success)
                    {

                        if (operation == "Add")
                        {
                            if (checkData())
                            {
                                string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
                                //вставка в таблицу MOL
                                string txtSQLQuery = "insert into Employee_JournalOperation (EmployeePersonnelNumber, IdJournalOperations, IdAccounting, Sum) values (" +
                               toolStripComboBoxEmployeePersonnelNumber.Text + ", " + textBoxNumber.Text + ", " + toolStripComboBoxTypeAccounting.Text + ", '" + toolStripTextBoxSum.Text + "')";
                                ExecuteQuery(txtSQLQuery);
                                //обновление dataGridView1
                                String selectCommand = "select * from Employee_JournalOperation";

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
                            string valueId = dataGridView1[1, CurrentRow].Value.ToString();
                            string valueId2 = dataGridView1[0, CurrentRow].Value.ToString();
                            string changeId = toolStripComboBoxIdUnit.Text;
                            string changeSum = toolStripTextBoxSum.Text;

                            //обновление Name
                            string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";

                            String selectCommand = "update Employee_JournalOperation set IdAccounting='" + changeId + "' where IdJournalOperations=" + valueId + " AND " + "EmployeePersonnelNumber=" + valueId2;
                            changeValue(ConnectionString, selectCommand);

                            selectCommand = "update Employee_JournalOperation set Sum='" + changeSum + "' where IdJournalOperations=" + valueId + " AND " + "EmployeePersonnelNumber=" + valueId2;
                            changeValue(ConnectionString, selectCommand);
                            //обновление dataGridView1
                            selectCommand = "select * from Unit_Employee";
                            refreshForm(ConnectionString, selectCommand);
                        }

                        updateSum();

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

        public bool checkData()
        {
            string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
            string selectCommand = "Select * from Employee_JournalOperation where IdJournalOperations = " + textBoxNumber.Text + " AND " + "EmployeePersonnelNumber = " + toolStripComboBoxEmployeePersonnelNumber.Text
                + " AND " + "IdAccounting = " + toolStripComboBoxTypeAccounting.Text;

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

        public bool checkOperationExist()
        {
            string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
            string selectCommand = "Select * from OperationsJournal where UnitId = " + toolStripComboBoxIdUnit.Text + " AND " + "PeriodOperation = '" + textBoxMonth.Text +"'";

            SQLiteConnection connect = new
           SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new
           SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);

            connect.Close();
            if (ds.Tables[0].Rows.Count > 1)
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
            if (checkDataOperation() || (ID != null))
            {
                saveOperation();
            //this.Close();
            }
        }

        public bool checkDataOperation()
        {
            string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
            string selectCommand = "Select * from OperationsJournal where Id = " + textBoxNumber.Text;

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

        public void saveOperation()
        {
            if ((textBoxDate.Text != "") && (textBoxMonth.Text != ""))
            {
                if ((textBoxDate.Text.Length < 9) && (textBoxMonth.Text.Length < 6))
                {
                    if ((new Regex(@"^[0-9]*$")).Match(textBoxTotal.Text).Success)
                    {
                        textBoxTotal.Text = textBoxTotal.Text + ",00";
                    }
                    if ((new Regex(@"\d\,\d{2}$")).Match(textBoxTotal.Text).Success)
                    {
                        if ((new Regex(@"\d{2}\.\d{2}\.\d{2}$")).Match(textBoxDate.Text).Success && (new Regex(@"\d{2}.\d{2}$")).Match(textBoxMonth.Text).Success)
                        {
                            string[] period = textBoxDate.Text.Split('.');
                            string[] period2 = textBoxMonth.Text.Split('.');

                            if (Convert.ToInt32(period[0]) <= 31 && Convert.ToInt32(period[1]) < 13 && Convert.ToInt32(period2[0]) < 13)
                            {
                                if (checkOperationExist())
                                {
                                    string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
                                    //вставка в таблицу
                                    if (ID == null)
                                    {

                                        string txtSQLQuery = "insert into OperationsJournal (Id, UnitId, DateOperation, PeriodOperation, Sum) values (" +
                                       textBoxNumber.Text + ", " + toolStripComboBoxIdUnit.Text + ", '" + textBoxDate.Text + "', '" + textBoxMonth.Text + "', '" + textBoxTotal.Text + "')";
                                        ID = Convert.ToInt32(textBoxNumber.Text);
                                        ExecuteQuery(txtSQLQuery);
                                    }
                                    else
                                    {
                                        String selectCommand = "update OperationsJournal set UnitId='" + toolStripComboBoxIdUnit.Text + "' where Id=" + textBoxNumber.Text;
                                        changeValue(ConnectionString, selectCommand);

                                        selectCommand = "update OperationsJournal set DateOperation='" + textBoxDate.Text + "' where Id=" + textBoxNumber.Text;
                                        changeValue(ConnectionString, selectCommand);

                                        selectCommand = "update OperationsJournal set PeriodOperation='" + textBoxMonth.Text + "' where Id=" + textBoxNumber.Text;
                                        changeValue(ConnectionString, selectCommand);

                                        selectCommand = "update OperationsJournal set Sum='" + textBoxTotal.Text + "' where Id=" + textBoxNumber.Text;
                                        changeValue(ConnectionString, selectCommand);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Операция за этот месяц уже существует", "Ошибка");
                                }

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
                    MessageBox.Show("Слишком длинное наименование", "Ошибка");
                }
            }
            else
            {
                MessageBox.Show("Пустое поле", "Ошибка");
            }
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

            toolStripTextBoxSum.TextBox.DataBindings.Clear();

            toolStripTextBoxSum.TextBox.DataBindings.Add(new Binding("Text", ds.Tables[0], "Sum", true));

            toolStripComboBoxTypeAccounting.Text = ds.Tables[0].Rows[0].ItemArray[2].ToString();
            toolStripComboBoxEmployeePersonnelNumber.Text = ds.Tables[0].Rows[0].ItemArray[1].ToString();

            connect.Close();
        }


        private void DataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string ConnectionString = @"Data Source=" + sPath +
           ";New=False;Version=3";
            string selectCommand = "Select * from Employee_JournalOperation where EmployeePersonnelNumber= " + dataGridView1.SelectedCells[1].Value
                + " AND " + "IdJournalOperations = " + dataGridView1.SelectedCells[0].Value + " AND " + "IdAccounting = " + dataGridView1.SelectedCells[2].Value;
            selectData(ConnectionString, selectCommand);
        }

        
    }
}
