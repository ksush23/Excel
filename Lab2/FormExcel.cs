using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2
{
    public partial class FormExcel : Form
    {
        static Class26BasedSys f = new Class26BasedSys();
        static MyHashTable myHash = new MyHashTable();
        static Parser parser = new Parser();

        public FormExcel()
        {
            InitializeComponent();
            int rows = 1;
            int columns = 1;

            dataGridView.AllowUserToAddRows = false;
            InitializeDataGridView(rows, columns, true);
            comboBox.SelectedIndex = 0;
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void FormExcel_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddRowButton_Click(object sender, EventArgs e)
        {
            AddRow();
        }

        private void AddRow()
        {
            ++dataGridView.RowCount;

            for (int i = 0; i < dataGridView.RowCount; ++i)
            {
                dataGridView.Rows[i].HeaderCell.Value = (i).ToString();
            }

            string cell;
            for (int j = 0; j < dataGridView.ColumnCount; j++)
            {
                cell = dataGridView.Columns[j].Index.ToString() + "." + (dataGridView.RowCount - 1).ToString();
                myHash.AddBoth(cell, "", "0");
            }
        }

        private void AddColumnButton_Click(object sender, EventArgs e)
        {
            AddColumn();
        }

        private void AddColumn()
        {
            dataGridView.ColumnCount++;
            Class26BasedSys f = new Class26BasedSys();
            dataGridView.Columns[dataGridView.ColumnCount - 1].Name = f.ToSys(dataGridView.ColumnCount - 1);

            string cell;
            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                cell = (dataGridView.ColumnCount - 1).ToString() + "." + dataGridView.Rows[i].Index.ToString();
                myHash.AddBoth(cell, "", "0");
            }
        }

        private void dataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            string cell = dataGridView.CurrentCell.ColumnIndex.ToString() + "." + dataGridView.CurrentCell.RowIndex.ToString();
            if (myHash.Formulas.Contains(cell))
                textBox.Text = myHash.Formulas[cell].ToString();
        }

        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string cell = dataGridView.CurrentCell.ColumnIndex.ToString() + "." + dataGridView.CurrentCell.RowIndex.ToString();
            try
            {
                string value = "";
                string formula;
            
                string expression = dataGridView.CurrentCell.Value.ToString();
                myHash.AddFormula(cell, expression);

                if (expression[0] >= 'A' && expression[0] <= 'Z')
                {
                    if (IsRecur())
                    {
                        MessageBox.Show("Рекурсія!");
                        parser.str_error = "Resur";
                        myHash.Formulas[cell] = "";
                        myHash.Values[cell] = "0";
                        
                        if (comboBox.SelectedIndex.Equals(0))
                            dataGridView.CurrentCell.Value = "";

                        if (comboBox.SelectedIndex.Equals(1))
                            dataGridView.CurrentCell.Value = "0";

                        return;
                    }
                    else
                    {
                        formula = ToFormula(expression, cell);

                        parser.str_error = "";

                        value = parser.ExpressionStart(formula).ToString();

                        if (parser.str_error != "")
                        {
                            MessageBox.Show(parser.str_error);
                            return;
                        }
                    }
                }

                formula = ToFormula(expression, cell);

                parser.str_error = "";

                value = parser.ExpressionStart(formula).ToString();

                if (parser.str_error.Equals(""))
                {
                    myHash.AddValue(cell, value);
                    textBox.Text = value;

                    if (comboBox.SelectedIndex.Equals(1))
                        dataGridView[dataGridView.CurrentCell.ColumnIndex, dataGridView.CurrentCell.RowIndex].Value = value;

                    if (myHash.References.Contains(cell))
                    {
                        string refer = GetReference(cell);

                        Reference(refer, expression);
                    }
                }
            }

             catch (System.NullReferenceException)
            {
                MessageBox.Show("Посилання на неіснуючу чи порожню клітинку");
            }

            catch (System.FormatException)
            {
                MessageBox.Show("Неправильно введений вираз");
                myHash.Formulas[cell] = "";
            }
            
            catch(System.IndexOutOfRangeException)
            {
                MessageBox.Show("Введений порожній вираз!");
            }
        }

        private void Reference(string refer, string expression)
        {
            string reference = "";
            int j = 0;
            while (refer != "")
            {
                while (refer[j] != ' ')
                {
                    reference += refer[j];
                    j++;

                    if (j == refer.Length)
                        break;
                }

                if (j != refer.Length)
                {
                    if (refer[j] != ' ')
                        refer = refer.Remove(0, j);
                    else
                        refer = refer.Remove(0, j + 1);
                }
                else
                    refer = "";

                string newreference = GetReference(reference);
                if (newreference != "")
                    Reference(newreference, expression);

                string columnIndex = "";
                string rowIndex = "";

                int i = 0;
                while (reference[i] != '.')
                {
                    columnIndex += reference[i];
                    i++;
                }

                i++;
                while (i < reference.Length)
                {
                    rowIndex += reference[i];
                    i++;
                }

                int colIndex = Convert.ToInt32(columnIndex);
                int rIndex = Convert.ToInt32(rowIndex);

                // Формула, яка записана в клітинці, яку ми поміняли
                string formula = ToFormula(expression, "0");
                expression = parser.ExpressionStart(formula).ToString();
                myHash.Values[dataGridView.CurrentCell.ColumnIndex.ToString() + "." + dataGridView.CurrentCell.RowIndex.ToString()] = expression;

                // Формула, записана в клітинці, яку ми повинні поміняти після змін в клітинці, на яку дана посилалась
                string formula1 = ToFormula(myHash.Formulas[reference].ToString(), "0");
                string expression1 = parser.ExpressionStart(formula1).ToString();

                if (parser.str_error.Equals("Порожній вираз!"))
                {
                    myHash.Formulas[reference] = "";
                    if (comboBox.SelectedIndex.Equals(0))
                        dataGridView[colIndex, rIndex].Value = "";
                    textBox.Text = "";

                    if (comboBox.SelectedIndex.Equals(1))
                        dataGridView[colIndex, rIndex].Value = "0";
                }

                myHash.Values[reference] = expression1;

                if (comboBox.SelectedIndex.Equals(1))
                {
                    dataGridView[colIndex, rIndex].Value = expression1;
                }

                j = 0;
                reference = "";
            }
        }

        private void InitializeDataGridView(int rows, int columns, bool ok)
        {
            dataGridView.ColumnCount = columns;
            dataGridView.ColumnHeadersVisible = true;

            //style
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Times new roman", 12, FontStyle.Bold);
            dataGridView.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            // header names
            
            for (int i = 0; i < columns; ++i)
            {
                dataGridView.Columns[i].Name = f.ToSys(i);
            }

            dataGridView.RowCount = rows;
            dataGridView.RowHeadersVisible = true;
            DataGridViewCellStyle rowHeaderStyle = new DataGridViewCellStyle();

            rowHeaderStyle.BackColor = Color.Beige;
            rowHeaderStyle.Font = new Font("Times new roman", 12, FontStyle.Bold);
            for (int i = 0; i < rows; ++i)
            {
                dataGridView.Rows[i].HeaderCell.Value = (i).ToString();
            }
            // populate the rows
            dataGridView.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

            string cell;
            for (int j = 0; j < dataGridView.ColumnCount; j++)
                for (int i = 0; i < dataGridView.RowCount; i++)
                {
                    cell = j.ToString() + "." + i.ToString();
                    if (ok)
                        myHash.AddBoth(cell, "", "0");
                }
        }

        private void DelRowButton_Click(object sender, EventArgs e)
        {
            DelRow();
        }

        private void DelRow()
        {
            string cell;
            for (int j = 0; j < dataGridView.ColumnCount; j++)
            {
                cell = j.ToString() + "." + (dataGridView.RowCount - 1).ToString();

                myHash.Formulas.Remove(cell);
                myHash.Values.Remove(cell);

                if (myHash.References.Contains(cell))
                {
                    MessageBox.Show("Видалення клітинки, на яку є посилання в інших клітинках!");

                    string refer = GetReference(cell);
                    Reference(refer, "");
                    myHash.References.Remove(cell);
                }
            }

            dataGridView.Rows.RemoveAt(dataGridView.RowCount - 1);
        }

        private void DelColumnButton_Click(object sender, EventArgs e)
        {
            DelColumn();
        }

        private void DelColumn()
        {
            string cell;
            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                cell = (dataGridView.ColumnCount - 1).ToString() + "." + i.ToString();
                myHash.Formulas.Remove(cell);
                myHash.Values.Remove(cell);

                if (myHash.References.Contains(cell))
                {
                    MessageBox.Show("Видалення клітинки, на яку є посилання в інших клітинках!");

                    string refer = GetReference(cell);
                    Reference(refer, "");
                    myHash.References.Remove(cell);
                }
            }

            dataGridView.Columns.RemoveAt(dataGridView.ColumnCount - 1);
        }

        public bool IsCorrectExpression(string expression)
        {
            int i = 0;
            int max = expression.Length;

            while (i < max)
            {
                bool ok = (expression[i] >= 'A' && expression[i] <= 'Z') ||
                    (expression[i] >= '0' && expression[i] <= '9') ||
                    (expression[i] == ' ' || expression[i] == '+' || expression[i] == '-'
                    || expression[i] == '*' || expression[i] == '/' || expression[i] == '%'
                    || expression[i] == '|' || expression[i] == ')' || expression[i] == '('
                    || expression[i] == '^');
                if (!ok)
                    return false;

                if (expression[i] >= 'A' && expression[i] <= 'Z')
                {
                    if (i + 1 == max)
                        return false;
                    if (expression[i + 1] < '0' || expression[i + 1] > '9')
                        return false;
                }

                if (expression[i] >= '0' && expression[i] <= '9')
                {
                    if (i + 1 == max)
                        return true;

                    if (expression[i + 1] != '+' && expression[i + 1] != '-'
                        && expression[i + 1] != '*' && expression[i + 1] != '/'
                        && expression[i + 1] != '%' && expression[i + 1] != '|'
                        && expression[i + 1] != '^' && expression[i + 1] != ' ' 
                        && expression[i + 1] < '0' && expression[i + 1] > '9')
                        return false;
                }
                i++;
            }
            return true;
        }

        public string ToFormula(string expression, string cell)
        {
            if (IsCorrectExpression(expression))
            {
                string formula = "";
                int i = 0;
                int max = expression.Length;

                while (i < max)
                {
                    while (expression[i] < 'A' || expression[i] > 'Z')
                    {
                        formula += expression[i];
                        i++;
                        if (i == max)
                            break;
                    }
                    if (i == max)
                        break;

                    string st = "";
                    if (expression[i] >= 'A' && expression[i] <= 'Z' && i < max)
                    {
                        st += expression[i];
                    }

                    i++;
                    while (expression[i] >= '0' && expression[i] <= '9')
                    {
                        st += expression[i];
                        i++;
                        if (i == max)
                            break;
                    }

                    if (st != "")
                    {
                        string cellIndex = AddressAnalizator(st);
                        if (cell != "0")
                            myHash.AddReference(cellIndex, cell);
                        formula += myHash.Values[cellIndex];
                    }
                }

                return formula;
            }
            else
            {
                throw new System.FormatException("Неправильно введений вираз");
            }
        }

        private string AddressAnalizator(string cellValue)
        {
            char cellText = cellValue[0];

            cellValue = cellValue.Remove(0, 1);

            string cellTextIndex = f.FromSys(cellText.ToString()).ToString();
            string cellIndex = cellTextIndex + "." + cellValue;

            return cellIndex;
        }

        private string ToAddress(string cell)
        {
            int i = 0;
            string address = "";

            while (cell[i] != '.')
            {
                address += cell[i];
                i++;
            }
            int toInt = Convert.ToInt32(address);
            address = f.ToSys(toInt);

            while (i < cell.Length)
            {
                address += cell[i];
            }

            return address;
        }

        public bool IsExpression(string text)
        {
            char cellText = text[0];
            if (cellText >= '0' && cellText <= '9' || cellText.Equals('-') || cellText.Equals('+'))
                return false;

            if (cellText >= 'A' && cellText <= 'Z' && text[1] >= '0' && text[1] <= '9')
                return true;

            return false;
        }

        private bool IsRecur()
        {
            string cellName = dataGridView.CurrentCell.ColumnIndex.ToString() + "." + dataGridView.CurrentCell.RowIndex.ToString();
            string cellValue = AddressAnalizator(dataGridView.CurrentCell.Value.ToString());

            if (cellName.Equals(cellValue))
                return true;
            string refer = GetReference(cellName);
            string reference = "";
            
            while (refer != "")
            {
                reference = refer;
                refer = GetReference(refer);
            }

            if (cellValue.Equals(reference))
                return true;

            return false;
        }

        private void dataGridView_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string index;
            if(comboBox.SelectedIndex.Equals(0))
            {
                for (int j = 0; j < dataGridView.ColumnCount; j++)
                    for (int i = 0; i < dataGridView.RowCount; i++)
                    {
                        index = j.ToString() + "." + i.ToString();
                        dataGridView[j, i].Value = myHash.Formulas[index];
                    }
            }
            else
            {
                for (int j = 0; j < dataGridView.ColumnCount; j++)
                    for (int i = 0; i < dataGridView.RowCount; i++)
                    {
                        index = j.ToString() + "." + i.ToString();

                        dataGridView[j, i].Value = myHash.Values[index];
                    }
            }
        }

        private string GetReference(string cell)
        {
            if (myHash.References.Contains(cell))
            {
                return myHash.References[cell].ToString();
            }   

            else
            {
                return "";
            }
        }

        private void HelpButton_Click(object sender, EventArgs e)
        {
            Help();
        }

        private void Help()
        {
            MessageBox.Show("Для обчислення існують два режими: вираз і значення\n" +
                "Вираз обчислює коректно введений арифметичний вираз\n" +
                "Значення візьме значення іншої клітинки (за її назвою, наприклад А0)\n" +
                "Кнопка зберегти збереже задану таблицю в текстовий документ\n" +
                "Кнопка відкрити відкриє останній збережений документ");
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            FileStream fs1 = new FileStream(@"1.txt", FileMode.Create);
            StreamWriter streamWriter1 = new StreamWriter(fs1);

            try
            {
                for (int i = 0; i < dataGridView.RowCount; i++)
                {
                    for (int j = 0; j < dataGridView.ColumnCount; j++)
                    {
                        string cell = j.ToString() + "." + i.ToString();
                        streamWriter1.Write(myHash.Values[cell].ToString() + "#");
                    }

                    streamWriter1.WriteLine();
                }

                streamWriter1.Close();
                fs1.Close();

                FileStream fs2 = new FileStream(@"2.txt", FileMode.Create);
                StreamWriter streamWriter2 = new StreamWriter(fs2);
                for (int i = 0; i < dataGridView.RowCount; i++)
                {
                    for (int j = 0; j < dataGridView.ColumnCount; j++)
                    {
                        string cell = j.ToString() + "." + i.ToString();
                        streamWriter2.Write(myHash.Formulas[cell].ToString() + "#");
                    }

                    streamWriter2.WriteLine();
                }

                streamWriter2.Close();
                fs2.Close();

                FileStream fs3 = new FileStream(@"3.txt", FileMode.Create);
                StreamWriter streamWriter3 = new StreamWriter(fs3);
                for (int i = 0; i < dataGridView.RowCount; i++)
                {
                    for (int j = 0; j < dataGridView.ColumnCount; j++)
                    {
                        string cell = j.ToString() + "." + i.ToString();
                        streamWriter3.Write(GetReference(cell) + "#");
                    }

                    streamWriter3.WriteLine();
                }

                streamWriter3.Close();
                fs3.Close();

                MessageBox.Show("Файл успішно збережений");
            }
            catch
            {
                MessageBox.Show("Помилка при збереженні файла!");
            }
        }

        private void OpenFile_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void Open()
        {
            Stream fs1 = new FileStream(@"1.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr1 = new StreamReader(fs1);

            myHash.Formulas.Clear();
            myHash.Values.Clear();
            myHash.References.Clear();

            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();

            string elem = "";
            string cell;
            int j = 0;
            int rowCount = 0;
            int columnCount = 0;
            int count = 0;

            string line = sr1.ReadLine();
            while (line != null)
            {
                string lineEdit = line;
                while (lineEdit != "")
                {
                    while (lineEdit[j] != '#')
                    {
                        elem += lineEdit[j];
                        j++;

                        if (lineEdit.Length.Equals(j + 1))
                            break;
                    }

                    if (j != lineEdit.Length)
                    {
                        if (lineEdit[j] != '#')
                            lineEdit = lineEdit.Remove(0, j);
                        else
                            lineEdit = lineEdit.Remove(0, j + 1);
                    }

                    cell = columnCount.ToString() + "." + rowCount.ToString();
                    myHash.Values[cell] = elem;

                    columnCount++;
                    j = 0;
                    elem = "";
                }
                line = sr1.ReadLine();
                rowCount++;
                count++;
                columnCount = 0;
            }

            fs1.Close();
            sr1.Close();

            Stream fs2 = new FileStream(@"2.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr2 = new StreamReader(fs2);

            elem = "";
            j = 0;
            rowCount = 0;
            columnCount = 0;

            line = sr2.ReadLine();
            while (line != null)
            {
                string lineEdit = line;
                while (lineEdit != "")
                {
                    while (lineEdit[j] != '#')
                    {
                        elem += lineEdit[j];
                        j++;

                        if (lineEdit.Length.Equals(j + 1))
                            break;
                    }

                    if (j != lineEdit.Length)
                    {
                        if (lineEdit[j] != '#')
                            lineEdit = lineEdit.Remove(0, j);
                        else
                            lineEdit = lineEdit.Remove(0, j + 1);
                    }

                    cell = columnCount.ToString() + "." + rowCount.ToString();
                    myHash.Formulas[cell] = elem;

                    columnCount++;
                    j = 0;
                    elem = "";
                }
                line = sr2.ReadLine();
                rowCount++;
                columnCount = 0;
            }

            fs2.Close();
            sr2.Close();

            Stream fs3 = new FileStream(@"3.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr3 = new StreamReader(fs3);

            elem = "";
            j = 0;
            rowCount = 0;
            columnCount = 0;

            line = sr3.ReadLine();
            while (line != null)
            {
                string lineEdit = line;
                while (lineEdit != "")
                {
                    while (lineEdit[j] != '#')
                    {
                        elem += lineEdit[j];
                        j++;

                        if (lineEdit.Length.Equals(j + 1))
                            break;
                    }

                    if (j != lineEdit.Length)
                    {
                        if (lineEdit[j] != '#')
                            lineEdit = lineEdit.Remove(0, j);
                        else
                            lineEdit = lineEdit.Remove(0, j + 1);
                    }

                    cell = columnCount.ToString() + "." + rowCount.ToString();
                    myHash.References[cell] = elem;

                    columnCount++;
                    j = 0;
                    elem = "";
                }
                line = sr3.ReadLine();
                rowCount++;
                columnCount = 0;
            }

            fs3.Close();
            sr3.Close();

            InitializeDataGridView(rowCount, count, false);

            string index;
            if (comboBox.SelectedIndex.Equals(0))
            {
                for (int k = 0; k < dataGridView.ColumnCount; k++)
                    for (int i = 0; i < dataGridView.RowCount; i++)
                    {
                        index = k.ToString() + "." + i.ToString();
                        dataGridView[k, i].Value = myHash.Formulas[index];
                    }
            }
            else
            {
                for (int k = 0; k < dataGridView.ColumnCount; k++)
                    for (int i = 0; i < dataGridView.RowCount; i++)
                    {
                        index = k.ToString() + "." + i.ToString();

                        dataGridView[k, i].Value = myHash.Values[index];
                    }
            }
        }
    }
}
