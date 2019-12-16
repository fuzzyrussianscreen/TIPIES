using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Office.Interop.Word;
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
    public partial class FormReport : Form
    {

        public SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private DataSet DS = new DataSet();
        private System.Data.DataTable DT = new System.Data.DataTable();
        private static string sPath = Path.Combine("D:\\ФИСТ ИСЭбд\\Repos\\TIPIES\\TIPIES.db");
        string ConnectionString = @"Data Source=" + sPath + ";New=False;Version=3";
        public FormReport()
        {

            InitializeComponent();
        }

        private void FormReport_Load(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "dd.MM.yy";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd.MM.yy";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;

            comboBoxTypeOperation.Items.Add("Оборотно сальдовая");
            comboBoxTypeOperation.Items.Add("Взаиморасчёт");
            comboBoxTypeOperation.Items.Add("Заработная плата");
            comboBoxTypeOperation.SelectedIndex = -1;

        }

        private void ComboBoxTypeOperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxAccount.Enabled = false;
            comboBoxUnit.Enabled = false;
            comboBoxEmployee.Enabled = false;
            dateTimePicker2.Enabled = true;
            String select = "";

            if (comboBoxTypeOperation.SelectedIndex == 0)
            {
                comboBoxAccount.Enabled = true;
                select = "Select NumberAccount, NameAccount from ChartAccounts";
                selectCombo(ConnectionString, select, comboBoxAccount, "NameAccount", "NumberAccount");
                comboBoxAccount.SelectedIndex = 1;
            }
            if (comboBoxTypeOperation.SelectedIndex == 1)
            {
                comboBoxUnit.SelectedIndex = -1;
                select = "Select Name, Id from Unit";
                selectCombo(ConnectionString, select, comboBoxUnit, "Name", "Id");
                comboBoxUnit.Enabled = true;
                comboBoxUnit.SelectedIndex = 1;
            }
            if (comboBoxTypeOperation.SelectedIndex == 2)
            {
                dateTimePicker2.Enabled = false;
            }
        }

        private void ComboBoxUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((comboBoxUnit.SelectedIndex != -1) && (comboBoxUnit.Enabled == true))
            {

                comboBoxEmployee.Enabled = true;
                string select = "Select EmployeePersonnelNumber, FullName, PersonnelNumber from Unit_Employee, Employee where IdUnit ="
                    + comboBoxUnit.SelectedValue + " and EmployeePersonnelNumber = PersonnelNumber";
                selectCombo(ConnectionString, select, comboBoxEmployee, "FullName", "PersonnelNumber");
                comboBoxEmployee.SelectedIndex = -1;
            }
        }

        public void selectCombo(string ConnectionString, String selectCommand, ComboBox comboBox, string displayMember, string valueMember)
        {
            SQLiteConnection connect = new SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            comboBox.DataSource = ds.Tables[0];
            comboBox.DisplayMember = displayMember;
            comboBox.ValueMember = valueMember;
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

        public void selectTable(string ConnectionString, String selectCommand)
        {
            SQLiteConnection connect = new SQLiteConnection(ConnectionString);
            connect.Open();
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(selectCommand, connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = ds.Tables[0].ToString();
            connect.Close();
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            String selectCommand = "";
            string dateFrom = Convert.ToString(dateTimePicker1.Text);
            string dateTo = Convert.ToString(dateTimePicker2.Text);
            string stock = comboBoxUnit.Text.ToString();

            int count = 0;
            double sum = 0;

            if (comboBoxTypeOperation.SelectedIndex == 0)
            {
                string account = comboBoxAccount.SelectedValue  +"";
                string year1 = dateTimePicker1.Value.ToString("dd.mm.yy");
                string year2 = dateTimePicker2.Value.ToString("dd.mm.yy");
                selectCommand = "SELECT OperationsJournal.Id, OperationsJournal.Name, " +
                    "SUM(CASE WHEN (CAST (SUBSTR(PostingJournal.Date, 1, 8) AS TEXT) < '" + year1 + "') AND  DebitAccount = '" + account + "' THEN PostingJournal.Sum ELSE 0 END), " +
                    "SUM(CASE WHEN (CAST (SUBSTR(PostingJournal.Date, 1, 8) AS TEXT) < '" + year1 + "') AND  CreditAccount = '" + account + "' THEN PostingJournal.Sum ELSE 0 END), " +
                    "SUM(CASE WHEN (CAST (SUBSTR(PostingJournal.Date, 1, 8) AS TEXT) >= '" + year1 + "') AND  CAST (SUBSTR(PostingJournal.Date, 1, 8) AS TEXT) <= '" + year2 + "' AND  DebitAccount = '" + account + "' THEN PostingJournal.Sum ELSE 0 END), " +
                    "SUM(CASE WHEN (CAST (SUBSTR(PostingJournal.Date, 1, 8) AS TEXT) >= '" + year1 + "') AND  CAST (SUBSTR(PostingJournal.Date, 1, 8) AS TEXT) <= '" + year2 + "' AND  CreditAccount = '" + account + "' THEN PostingJournal.Sum ELSE 0 END), " +
                    "SUM(CASE WHEN (CAST (SUBSTR(PostingJournal.Date, 1, 8) AS TEXT) <= '" + year2 + "') AND  DebitAccount = '" + account + "' THEN PostingJournal.Sum ELSE 0 END), " +
                    "SUM(CASE WHEN (CAST (SUBSTR(PostingJournal.Date, 1, 8) AS TEXT) <= '" + year2 + "') AND  CreditAccount = '" + account + "' THEN PostingJournal.Sum ELSE 0 END)  " +
                    "FROM PostingJournal, OperationsJournal WHERE PostingJournal.IdOperationsJournal = OperationsJournal.Id GROUP BY OperationsJournal.Id";

                selectTable(ConnectionString, selectCommand);

                dataGridView1.Columns[0].HeaderCell.Value = "Код операции";
                dataGridView1.Columns[1].HeaderCell.Value = "Наименование операции";
                dataGridView1.Columns[2].HeaderCell.Value = "Начальный остаток дебет";
                dataGridView1.Columns[3].HeaderCell.Value = "Начальный остаток кредит";
                dataGridView1.Columns[4].HeaderCell.Value = "Дебетовый оборот";
                dataGridView1.Columns[5].HeaderCell.Value = "Кредитовый оборот";
                dataGridView1.Columns[6].HeaderCell.Value = "Конечный остаток дебет";
                dataGridView1.Columns[7].HeaderCell.Value = "Конечный остаток кредит";

                count = 0;
                sum = 0;
                while (count <= (Convert.ToInt32(dataGridView1.RowCount.ToString()) - 1))
                {
                    sum += Convert.ToDouble(dataGridView1[2, count].Value);
                    count++;
                }
                label4.Text = "" + Convert.ToString(sum);

                count = 0;
                sum = 0;
                while (count <= (Convert.ToInt32(dataGridView1.RowCount.ToString()) - 1))
                {
                    sum += Convert.ToDouble(dataGridView1[3, count].Value);
                    count++;
                }
                label4.Text = label4.Text + " " + Convert.ToString(sum);

                count = 0;
                sum = 0;
                while (count <= (Convert.ToInt32(dataGridView1.RowCount.ToString()) - 1))
                {
                    sum += Convert.ToDouble(dataGridView1[4, count].Value);
                    count++;
                }
                label4.Text = label4.Text + " " + Convert.ToString(sum);

                count = 0;
                sum = 0;
                while (count <= (Convert.ToInt32(dataGridView1.RowCount.ToString()) - 1))
                {
                    sum += Convert.ToDouble(dataGridView1[5, count].Value);
                    count++;
                }
                label4.Text = label4.Text + " " + Convert.ToString(sum);

                count = 0;
                sum = 0;
                while (count <= (Convert.ToInt32(dataGridView1.RowCount.ToString()) - 1))
                {
                    sum += Convert.ToDouble(dataGridView1[6, count].Value);
                    count++;
                }
                label4.Text = label4.Text + " " + Convert.ToString(sum);

                count = 0;
                sum = 0;
                while (count <= (Convert.ToInt32(dataGridView1.RowCount.ToString()) - 1))
                {
                    sum += Convert.ToDouble(dataGridView1[7, count].Value);
                    count++;
                }
                label4.Text = label4.Text + " " + Convert.ToString(sum);
            }
            if (comboBoxTypeOperation.SelectedIndex == 1)
            {
                selectCommand = "SELECT PersonnelNumber, " +
                    "CASE WHEN PostingJournal.DebitAccount LIKE '70' THEN SubcontoDt1 ELSE SubcontoKt1 END AS " +
                    "Name, SUM(CASE WHEN PostingJournal.CreditAccount LIKE '70' THEN PostingJournal.Sum ELSE 0 END), " +
                    "SUM(CASE WHEN PostingJournal.DebitAccount LIKE '70' AND  PostingJournal.CreditAccount NOT LIKE '51' " +
                    "AND  PostingJournal.SubcontoDt2 LIKE 'НДФЛ' THEN PostingJournal.Sum ELSE 0 END), " +
                    "SUM(CASE WHEN PostingJournal.DebitAccount LIKE '70' AND  PostingJournal.CreditAccount NOT LIKE '51' " +
                    "AND  PostingJournal.SubcontoDt2 NOT LIKE 'НДФЛ' THEN PostingJournal.Sum ELSE 0 END), " +
                    "SUM(CASE WHEN PostingJournal.DebitAccount LIKE '70' AND  PostingJournal.CreditAccount LIKE '51' " +
                    "THEN PostingJournal.Sum ELSE 0 END) ";

                if (comboBoxEmployee.SelectedIndex == -1)
                {

                    selectCommand = selectCommand + "FROM PostingJournal, Employee, Unit_Employee WHERE (PostingJournal.SubcontoDt1 = Employee.FullName OR " +
                    "PostingJournal.SubcontoKt1 = Employee.FullName) AND (PersonnelNumber = Unit_Employee.EmployeePersonnelNumber " +
                        "AND IdUnit = '"+ comboBoxUnit.SelectedValue + "')";
                }
                else
                {
                    selectCommand = selectCommand + "FROM PostingJournal, Employee WHERE (PostingJournal.SubcontoDt1 = Employee.FullName OR " +
                    "PostingJournal.SubcontoKt1 = Employee.FullName) AND (PostingJournal.SubcontoDt1 ='" + comboBoxEmployee.Text + "' OR " +
                    "PostingJournal.SubcontoKt1 ='" + comboBoxEmployee.Text + "')";
                }

                selectCommand = selectCommand + " GROUP BY Name";
                selectTable(ConnectionString, selectCommand);
                dataGridView1.Columns[0].HeaderCell.Value = "Табельный номер";
                dataGridView1.Columns[1].HeaderCell.Value = "ФИО";
                dataGridView1.Columns[2].HeaderCell.Value = "Начислено";
                dataGridView1.Columns[3].HeaderCell.Value = "Удержано НДФЛ";
                dataGridView1.Columns[4].HeaderCell.Value = "Удержано другое";
                dataGridView1.Columns[5].HeaderCell.Value = "Выплачено";

                count = 0;
                sum = 0;
                while (count <= (Convert.ToInt32(dataGridView1.RowCount.ToString()) - 1))
                {
                    sum += Convert.ToDouble(dataGridView1[2, count].Value);
                    count++;
                }
                label4.Text = "" + Convert.ToString(sum);

                count = 0;
                sum = 0;
                while (count <= (Convert.ToInt32(dataGridView1.RowCount.ToString()) - 1))
                {
                    sum += Convert.ToDouble(dataGridView1[3, count].Value);
                    count++;
                }
                label4.Text = label4.Text + " " + Convert.ToString(sum);

                count = 0;
                sum = 0;
                while (count <= (Convert.ToInt32(dataGridView1.RowCount.ToString()) - 1))
                {
                    sum += Convert.ToDouble(dataGridView1[4, count].Value);
                    count++;
                }
                label4.Text = label4.Text + " " + Convert.ToString(sum);

                count = 0;
                sum = 0;
                while (count <= (Convert.ToInt32(dataGridView1.RowCount.ToString()) - 1))
                {
                    sum += Convert.ToDouble(dataGridView1[5, count].Value);
                    count++;
                }
                label4.Text = label4.Text + " " + Convert.ToString(sum);
            }
            if (comboBoxTypeOperation.SelectedIndex == 2)
            {

                string year = dateTimePicker1.Value.ToString("yy");
                selectCommand = "SELECT SubcontoDt1, " +
                    "SUM(CASE WHEN SUBSTR(PostingJournal.Date, 4, 5) LIKE '01." + year + "' THEN PostingJournal.Sum ELSE 0 END), " +
                    "SUM(CASE WHEN SUBSTR(PostingJournal.Date, 4, 5) LIKE '02." + year + "' THEN PostingJournal.Sum ELSE 0 END), " +
                    "SUM(CASE WHEN SUBSTR(PostingJournal.Date, 4, 5) LIKE '03." + year + "' THEN PostingJournal.Sum ELSE 0 END), " +
                    "SUM(CASE WHEN SUBSTR(PostingJournal.Date, 4, 5) LIKE '04." + year + "' THEN PostingJournal.Sum ELSE 0 END), " +
                    "SUM(CASE WHEN SUBSTR(PostingJournal.Date, 4, 5) LIKE '05." + year + "' THEN PostingJournal.Sum ELSE 0 END), " +
                    "SUM(CASE WHEN SUBSTR(PostingJournal.Date, 4, 5) LIKE '06." + year + "' THEN PostingJournal.Sum ELSE 0 END), " +
                    "SUM(CASE WHEN SUBSTR(PostingJournal.Date, 4, 5) LIKE '07." + year + "' THEN PostingJournal.Sum ELSE 0 END), " +
                    "SUM(CASE WHEN SUBSTR(PostingJournal.Date, 4, 5) LIKE '08." + year + "' THEN PostingJournal.Sum ELSE 0 END), " +
                    "SUM(CASE WHEN SUBSTR(PostingJournal.Date, 4, 5) LIKE '09." + year + "' THEN PostingJournal.Sum ELSE 0 END), " +
                    "SUM(CASE WHEN SUBSTR(PostingJournal.Date, 4, 5) LIKE '10." + year + "' THEN PostingJournal.Sum ELSE 0 END), " +
                    "SUM(CASE WHEN SUBSTR(PostingJournal.Date, 4, 5) LIKE '11." + year + "' THEN PostingJournal.Sum ELSE 0 END), " +
                    "SUM(CASE WHEN SUBSTR(PostingJournal.Date, 4, 5) LIKE '12." + year + "' THEN PostingJournal.Sum ELSE 0 END), " +
                    "SUM(CASE WHEN SUBSTR(PostingJournal.Date, 7, 5) LIKE '" + year + "' THEN PostingJournal.Sum ELSE 0 END) " +
                    "FROM PostingJournal WHERE SubcontoDt2 = 'Выплата' GROUP BY SubcontoDt1";


                selectTable(ConnectionString, selectCommand);
                dataGridView1.Columns[0].HeaderCell.Value = "Сотрудник";
                dataGridView1.Columns[1].HeaderCell.Value = "Январь";
                dataGridView1.Columns[2].HeaderCell.Value = "Февраль";
                dataGridView1.Columns[3].HeaderCell.Value = "Март";
                dataGridView1.Columns[4].HeaderCell.Value = "Апрель";
                dataGridView1.Columns[5].HeaderCell.Value = "Май";
                dataGridView1.Columns[6].HeaderCell.Value = "Июнь";
                dataGridView1.Columns[7].HeaderCell.Value = "Июль";
                dataGridView1.Columns[8].HeaderCell.Value = "Август";
                dataGridView1.Columns[9].HeaderCell.Value = "Сентябрь";
                dataGridView1.Columns[10].HeaderCell.Value = "Октябрь";
                dataGridView1.Columns[11].HeaderCell.Value = "Ноябрь";
                dataGridView1.Columns[12].HeaderCell.Value = "Декабрь";
                dataGridView1.Columns[13].HeaderCell.Value = "Итого";

                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.Visible = true;
                    bool nulable = false;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (dataGridView1.Rows[row.Index].Cells[column.Index].Value.ToString() == "0")
                        {
                            nulable = true;
                        }
                        else
                        {
                            nulable = false;
                            break;
                        }
                    }
                    if (nulable)
                    {
                        column.Visible = false;
                    }
                }

                count = 0;
                sum = 0;
                while (count <= (Convert.ToInt32(dataGridView1.RowCount.ToString()) - 1))
                {
                    sum += Convert.ToDouble(dataGridView1[13, count].Value);
                    count++;
                }
                label4.Text = "" + Convert.ToString(sum);

            }

        }

        private void buttonPDF_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "pdf|*.pdf"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }

            string FONT_LOCATION = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.TTF"); //определяем В СИСТЕМЕ(чтобы не копировать файл) расположение шрифта arial.ttf
            BaseFont baseFont = BaseFont.CreateFont(FONT_LOCATION, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED); //создаем шрифт
            iTextSharp.text.Font fontParagraph = new iTextSharp.text.Font(baseFont, 17, iTextSharp.text.Font.NORMAL); //регистрируем + можно задать параметры для него(17 - размер, последний параметр - стиль)
            string title = "";
            if (comboBoxTypeOperation.SelectedItem.ToString() == "Поступление материалов")
            {
                title = "Поступление материалов на " + comboBoxUnit.Text.ToString() + " с " + Convert.ToString(dateTimePicker1.Text) + " по " + Convert.ToString(dateTimePicker2.Text) + "\n\n";
            }
            if (comboBoxTypeOperation.SelectedItem.ToString() == "Остатки материалов на складе")
            {
                title = "Остатки материалов на " + comboBoxUnit.Text.ToString() + " на " + Convert.ToString(dateTimePicker2.Text) + "\n\n";
            }
            if (comboBoxTypeOperation.SelectedItem.ToString() == "Отпуск материалов")
            {
                title = "Отпуск материалов в " + comboBoxUnit.Text.ToString() + " с " + Convert.ToString(dateTimePicker1.Text) + " по " + Convert.ToString(dateTimePicker2.Text) + "\n\n";
            }

            var phraseTitle = new Phrase(title,
            new iTextSharp.text.Font(baseFont, 18, iTextSharp.text.Font.BOLD));
            iTextSharp.text.Paragraph paragraph = new
           iTextSharp.text.Paragraph(phraseTitle)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 12
            };

            PdfPTable table = new PdfPTable(dataGridView1.Columns.Count);

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                table.AddCell(new Phrase(dataGridView1.Columns[i].HeaderCell.Value.ToString(), fontParagraph));
            }
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    table.AddCell(new Phrase(dataGridView1.Rows[i].Cells[j].Value.ToString(), fontParagraph));
                }
            }

            var phraseSum = new Phrase(label4.Text.ToString(),
            new iTextSharp.text.Font(baseFont, 16, iTextSharp.text.Font.BOLD));
            iTextSharp.text.Paragraph paragraphSum = new
           iTextSharp.text.Paragraph(phraseSum)
            {
                Alignment = Element.ALIGN_RIGHT - 1,
                SpacingAfter = 12,
            };
            using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
            {
                iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.A2, 10f, 10f, 10f, 0f);
                PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                pdfDoc.Add(paragraph);
                pdfDoc.Add(table);
                pdfDoc.Add(paragraphSum);
                pdfDoc.Close();
                stream.Close();
            }
        }

        public void saveDoc(string FileName)
        {
            var winword = new Microsoft.Office.Interop.Word.Application();
            try
            {
                object missing = System.Reflection.Missing.Value;
                //создаем документ
                Microsoft.Office.Interop.Word.Document document =
                winword.Documents.Add(ref missing, ref missing, ref missing, ref
               missing);
                //получаем ссылку на параграф
                var paragraph = document.Paragraphs.Add(missing);
                var range = paragraph.Range;
                string title = "";
                if (comboBoxTypeOperation.SelectedIndex == 0)
                {
                    title = "Оборотно сальдовая по счёту " + comboBoxAccount.Text + " \"" + comboBoxAccount.SelectedValue + "\" с " + Convert.ToString(dateTimePicker1.Text) + " по " + Convert.ToString(dateTimePicker2.Text) + "";
                }
                if (comboBoxTypeOperation.SelectedIndex == 1)
                {
                    title = "Ведомость взаиморасчёта с сотрудниками " + comboBoxUnit.Text + " " + comboBoxEmployee.Text + " с " + Convert.ToString(dateTimePicker1.Text) + " по " + Convert.ToString(dateTimePicker2.Text) + "";
                }
                if (comboBoxTypeOperation.SelectedIndex == 2)
                {
                    title = "Ведомость выплат за год " + dateTimePicker1.Value.ToString("yyyy") + "";
                }
                //задаем текст
                range.Text = title;
                //задаем настройки шрифта
                var font = range.Font;
                font.Size = 16;
                font.Name = "Times New Roman";
                font.Bold = 1;
                //задаем настройки абзаца
                var paragraphFormat = range.ParagraphFormat;
                paragraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                paragraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
                paragraphFormat.SpaceAfter = 10;
                paragraphFormat.SpaceBefore = 0;
                //добавляем абзац в документ
                range.InsertParagraphAfter();
                //создаем таблицу
                var paragraphTable = document.Paragraphs.Add(Type.Missing);
                var rangeTable = paragraphTable.Range;

                int count = 0;
                for (int i = 0; i < dataGridView1.Columns.Count; ++i)
                {
                    if (dataGridView1.Columns[i].Visible == true)
                    {
                        count++;
                    }
                }
                        var table = document.Tables.Add(rangeTable, dataGridView1.Rows.Count + 1, count, ref
               missing, ref missing);
                font = table.Range.Font;
                font.Size = 14;
                font.Name = "Times New Roman";
                var paragraphTableFormat = table.Range.ParagraphFormat;
                paragraphTableFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
                paragraphTableFormat.SpaceAfter = 0;
                paragraphTableFormat.SpaceBefore = 0;
                count = 0;
                for (int i = 0; i < dataGridView1.Columns.Count; ++i)
                {
                    if (dataGridView1.Columns[i].Visible == true)
                    {
                        table.Cell(1, count + 1).Range.Text = dataGridView1.Columns[i].HeaderCell.Value.ToString();
                        count++;
                    }
                }
                for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                {
                count = 0;
                    for (int j = 0; j < dataGridView1.Columns.Count; ++j)
                    {
                        if (dataGridView1.Columns[j].Visible == true)
                        {
                            table.Cell(i + 2, count + 1).Range.Text = dataGridView1.Rows[i].Cells[j].Value.ToString();
                            count++;
                        }
                    }
                }
                //задаем границы таблицы
                table.Borders.InsideLineStyle = WdLineStyle.wdLineStyleInset;
                table.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;
                paragraph = document.Paragraphs.Add(missing);
                range = paragraph.Range;
                range.Text = label4.Text.ToString();
                font = range.Font;
                font.Size = 12;
                font.Name = "Times New Roman";
                paragraphFormat = range.ParagraphFormat;
                paragraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                paragraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
                paragraphFormat.SpaceAfter = 10;
                paragraphFormat.SpaceBefore = 10;
                range.InsertParagraphAfter();
                //сохраняем
                object fileFormat = WdSaveFormat.wdFormatXMLDocument;
                document.SaveAs(FileName, ref fileFormat, ref missing,
                ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing,
                ref missing);
                document.Close(ref missing, ref missing, ref missing);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                winword.Quit();
            }
        }



        private void buttonSaveXls_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "xls|*.xls"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value.Date >= dateTimePicker2.Value.Date)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания",
               "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "doc|*.doc"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    saveDoc(sfd.FileName);
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }
    }
}
