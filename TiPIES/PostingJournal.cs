using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TiPIES
{
    class PostingJournal
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private static string sPath = Path.Combine("D:\\ФИСТ ИСЭбд\\Repos\\TIPIES\\TIPIES.db");

        private string Id = null;
        private string DebitAccount;
        private string CreditAccount;
        private string SubcontoDt1;
        private string SubcontoKt1;
        private string SubcontoDt2;
        private string SubcontoKt2;
        private string Sum = null;
        private string Date;
        private string IdOperationsJournal;
        private string Count;

        string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
        String selectCommand;
        public void addPostingJournal(string IdJournalOperations, string EmployeePersonnelNumber, string IdAccounting, string Sum, string UnitId)
        {
            this.IdOperationsJournal = IdJournalOperations;
            this.Count = "1";

            selectCommand = "select MAX(Id) from PostingJournal";
            string maxValue = selectValue(ConnectionString, selectCommand);
            if (maxValue == "")
                maxValue = "0";
            this.Id = (Convert.ToInt32(maxValue) + 1).ToString();

            selectCommand = "select DateOperation from OperationsJournal where Id=" + IdJournalOperations;
            this.Date = selectValue(ConnectionString, selectCommand);

            selectCommand = "select Type from TypeAccounting where Id=" + IdAccounting;
            string type = selectValue(ConnectionString, selectCommand);

            if (type == "Начисления")
            {
                accrual(IdJournalOperations, EmployeePersonnelNumber, IdAccounting, Sum, UnitId);
            }
            if (type == "Удержание")
            {
                withholding(IdJournalOperations, EmployeePersonnelNumber, IdAccounting, Sum, UnitId);
            }
            if (type == "Выплаты")
            {
                payout(IdJournalOperations, EmployeePersonnelNumber, IdAccounting, Sum, UnitId);
            }
        }
        public void /*Начисление*/ accrual(string IdJournalOperations, string EmployeePersonnelNumber, string IdAccounting, string Sum, string UnitId)
        {
            #region Дебет Начисление

            string IdUnit = UnitId;

            this.Sum = getSum(IdJournalOperations, UnitId, EmployeePersonnelNumber);

            if (this.Sum == null)
            {
                //MessageBox.Show("Невозможно посчитать сумму начисления", "Ошибка");
                Console.WriteLine("Ошибка " + IdJournalOperations + " " + EmployeePersonnelNumber + " " + IdAccounting);
                return;
            }

            selectCommand = "select ExpenseAccount from Unit where Id=" + IdUnit;
            this.DebitAccount = selectValue(ConnectionString, selectCommand);

            selectCommand = "select NumberAccount from ChartAccounts where Id=" + this.DebitAccount;
            this.DebitAccount = selectValue(ConnectionString, selectCommand);

            selectCommand = "select Name from Unit where Id=" + UnitId;
            this.SubcontoDt1 = selectValue(ConnectionString, selectCommand);

            selectCommand = "select Subconto2 from ChartAccounts where NumberAccount=" + this.DebitAccount;
            this.SubcontoDt2 = selectValue(ConnectionString, selectCommand);
            #endregion

            #region Кредит 70
            this.CreditAccount = "" + 70;

            selectCommand = "select FullName from Employee where PersonnelNumber=" + EmployeePersonnelNumber;
            this.SubcontoKt1 = selectValue(ConnectionString, selectCommand);

            selectCommand = "select Name from TypeAccounting where Id=" + IdAccounting;
            this.SubcontoKt2 = selectValue(ConnectionString, selectCommand);
            #endregion

            addRecord();
        }
        public void /*Удержание*/ withholding(string IdJournalOperations, string EmployeePersonnelNumber, string IdAccounting, string Sum, string UnitId)
        {
            #region Дебет 70
            this.DebitAccount = "" + 70;

            selectCommand = "select FullName from Employee where PersonnelNumber=" + EmployeePersonnelNumber;
            this.SubcontoDt1 = selectValue(ConnectionString, selectCommand);

            selectCommand = "select Name from TypeAccounting where Id=" + IdAccounting;
            this.SubcontoDt2 = selectValue(ConnectionString, selectCommand);
            #endregion

            #region Кредит Удержание

            selectCommand = "select Name from TypeAccounting where Id=" + IdAccounting;
            string name = selectValue(ConnectionString, selectCommand);

            if (name == "НДФЛ")
                this.CreditAccount = "" + 68;
            if (name == "Алименты")
                this.CreditAccount = "" + 76;
            if (name == "Недостача")
                this.CreditAccount = "" + 94;

            selectCommand = "select Percent from TypeAccounting where Id=" + IdAccounting;
            double percent = Convert.ToDouble(selectValue(ConnectionString, selectCommand))/100;

            this.Sum = getSum(IdJournalOperations, UnitId, EmployeePersonnelNumber);

            if (this.Sum == null)
            {
                //MessageBox.Show("Невозможно посчитать сумму начисления", "Ошибка");
                Console.WriteLine("Ошибка " + IdJournalOperations + " " + EmployeePersonnelNumber + " " + IdAccounting);
                return;
            }
            else
            {
                this.Sum = (Convert.ToDouble( this.Sum) * percent).ToString();
            }

            selectCommand = "select Subconto1 from ChartAccounts where NumberAccount=" + this.CreditAccount;
            this.SubcontoKt1 = selectValue(ConnectionString, selectCommand);

            if (name == "Алименты")
            {
                selectCommand = "select FullName from Employee where PersonnelNumber=" + EmployeePersonnelNumber;
                this.SubcontoKt1 = selectValue(ConnectionString, selectCommand);
            }

            selectCommand = "select Subconto2 from ChartAccounts where NumberAccount=" + this.CreditAccount;
            this.SubcontoKt2 = selectValue(ConnectionString, selectCommand);
            #endregion

            addRecord();
        }
        public void /*Выплаты*/ payout(string IdJournalOperations, string EmployeePersonnelNumber, string IdAccounting, string Sum, string UnitId)
        {
            #region Дебет 70
            this.DebitAccount = "" + 70;

            selectCommand = "select FullName from Employee where PersonnelNumber=" + EmployeePersonnelNumber;
            this.SubcontoDt1 = selectValue(ConnectionString, selectCommand);

            selectCommand = "select Name from TypeAccounting where Id=" + IdAccounting;
            this.SubcontoDt2 = selectValue(ConnectionString, selectCommand);
            #endregion

            this.Sum = getSum(IdJournalOperations, UnitId, EmployeePersonnelNumber);

            if (this.Sum == null)
            {
                //MessageBox.Show("Невозможно посчитать сумму начисления", "Ошибка");
                Console.WriteLine("Ошибка " + IdJournalOperations + " "+ EmployeePersonnelNumber + " " + IdAccounting);
                return;
            }

            #region Кредит 51
            this.CreditAccount = "" + 51;

            selectCommand = "select Subconto1 from ChartAccounts where NumberAccount=" + this.CreditAccount;
            this.SubcontoKt1 = selectValue(ConnectionString, selectCommand);

            selectCommand = "select Subconto2 from ChartAccounts where NumberAccount=" + this.CreditAccount;
            this.SubcontoKt2 = selectValue(ConnectionString, selectCommand);
            #endregion

            addRecord();
        }

        public void addRecord()
        {
            string txtSQLQuery = "insert into PostingJournal (Id,DebitAccount,CreditAccount,SubcontoDt1,SubcontoKt1,SubcontoDt2,SubcontoKt2,Sum,Date,IdOperationsJournal, Count) values (" +
           Id + ", " + DebitAccount + ", " + CreditAccount + ", '" + SubcontoDt1 + "', '" + SubcontoKt1 + "', '" 
           + SubcontoDt2 + "', '" + SubcontoKt2 + "', '" + Sum + "', '" + Date + "', " + IdOperationsJournal + ", " + Count + ")";
            ExecuteQuery(txtSQLQuery);
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

        public string getSum(string IdJournalOperations, string UnitId, string EmployeePersonnelNumber)
        {
            string Sum = null;

            selectCommand = "select EmployeePersonnelNumber, WorkPeriod, IdUnit, Salary from Unit_Employee where EmployeePersonnelNumber=" + EmployeePersonnelNumber + " AND IdUnit= "+ UnitId;

            SQLiteConnection connect = new
           SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new
           SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            connect.Close();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                string[] period = row.ItemArray[1].ToString().Split('-');
                string[] period1 = period[0].Split('.');
                string[] period2 = period[1].Split('.');

                selectCommand = "select PeriodOperation from OperationsJournal where Id=" + IdJournalOperations;
                string monthOper = selectValue(ConnectionString, selectCommand);

                string[] month = monthOper.Split('.');

                if ((Convert.ToInt32(period1[2]) <= Convert.ToInt32(month[1])) && (Convert.ToInt32(month[1]) <= Convert.ToInt32(period2[2])))
                {
                    if ((Convert.ToInt32(period1[2]) < Convert.ToInt32(month[1])) && (Convert.ToInt32(month[1]) < Convert.ToInt32(period2[2])))
                    {
                        if (row.ItemArray[2].ToString() == UnitId)
                        {
                        }
                            Sum = row.ItemArray[3].ToString();
                    }
                    if ((Convert.ToInt32(period1[2]) == Convert.ToInt32(month[1])) && (Convert.ToInt32(period1[1]) <= Convert.ToInt32(month[0])) ||
                    ((Convert.ToInt32(month[1]) == Convert.ToInt32(period2[2]))) && (Convert.ToInt32(month[0]) <= Convert.ToInt32(period2[1])))
                    {
                        if (row.ItemArray[2].ToString() == UnitId)
                        {
                        }
                            Sum = row.ItemArray[3].ToString();
                    }

                }
            }

            return Sum;
        }

    }
}
