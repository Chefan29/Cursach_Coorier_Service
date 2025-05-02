using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace cursovay2
{
    public partial class AddTableForm2 : Form
    {
        public string TableName;
        public int FieldsAmount;
        public string KeyField;
        private List<System.Windows.Forms.TextBox> textBoxes = new List<System.Windows.Forms.TextBox>();
        private List<System.Windows.Forms.ComboBox> comboBoxes = new List<System.Windows.Forms.ComboBox>();
        private List<System.Windows.Forms.CheckBox> checkBoxes = new List<System.Windows.Forms.CheckBox>();
        public AddTableForm2()
        {
            InitializeComponent();
            Renew();
        }
        #region Методы проверки начальных полей
        public bool TableNameCheck()
        {
            if (textBox1.Text == "" || textBox1.Text == " " || textBox1.Text == "  ")
            {
                MessageBox.Show("Вы забыли ввести название таблицы!");
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool FieldsAmountCheck()
        {
            int fieldsAmount;
            bool isInt = int.TryParse(textBox2.Text, out fieldsAmount);
            if (!isInt)
            {
                MessageBox.Show("Введенное вами количество полей имело не числовой формат!");
                return false;
            }
            else
            {
                if (fieldsAmount > 5)
                {
                    MessageBox.Show("Вы хотите создать таблицу со слишком большим количеством полей!");
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        #endregion
        public bool NotOneMoreCheckBoxChanged()
        {
            int count = 0;
            for (int i = 0; i < FieldsAmount; i++)
            {
                if (checkBoxes[i].Checked)
                {
                    count++;
                }
            }
            if (count > 1)
            {
                MessageBox.Show($"Вы хотите создать таблицу с {count} ключевыми полями! Так нельзя, оставьте только одно!");
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool MoreThanZeroCHeckBoxesChenged()
        {
            int count = 0;
            for (int i = 0; i < FieldsAmount; i++)
            {
                if (checkBoxes[i].Checked)
                {
                    count++;
                }
            }
            if (count == 1)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Вы пытаетесь создать связанную таблицу с кол-вом полей ≠ 1");
                return false;
            }
        }
        public bool CheckComboBox(System.Windows.Forms.ComboBox comboBox)
        {
            return !string.IsNullOrEmpty(comboBox.Text);
        }

        private void FieldAddButton_Click(object sender, EventArgs e)
        {
            #region Проверка начальных полей
            bool tableNameCheck = TableNameCheck();
            bool fieldsAmountCheck = FieldsAmountCheck();
            #endregion

            if (tableNameCheck && fieldsAmountCheck) //Присвоение начальных полей
            {
                TableName = textBox1.Text;
                FieldsAmount = int.Parse(textBox2.Text);
            }
            for (int i = 0; i < FieldsAmount; i++)
            {
                System.Windows.Forms.TextBox textBox = new System.Windows.Forms.TextBox();
                textBox.Location = new Point(13, 63 + i * 30);
                textBox.Size = new Size(240, 30);
                this.Controls.Add(textBox);
                textBoxes.Add(textBox);

                System.Windows.Forms.ComboBox comboBox = new System.Windows.Forms.ComboBox();
                comboBox.Location = new Point(260, 63 + i * 30);
                comboBox.Size = new Size(60, 30);
                comboBox.Items.Add("string");
                comboBox.Items.Add("int");
                this.Controls.Add(comboBox);
                comboBoxes.Add(comboBox);

                System.Windows.Forms.CheckBox checkBox = new System.Windows.Forms.CheckBox();
                checkBox.Location = new Point(380, 63 + i * 30);
                checkBox.Text = "Ключ";
                this.Controls.Add(checkBox);
                checkBoxes.Add(checkBox);
            }
        }

        private void AddTableButton_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (!string.IsNullOrEmpty(comboBox1.Text))
                {
                    bool isComboBoxesNotUsed = false;
                    //bool notOneMoreCheckBoxChanged = NotOneMoreCheckBoxChanged();
                    bool moreThanZeroCHeckBoxesChenged = MoreThanZeroCHeckBoxesChenged();
                    Random random = new Random();
                    int relations = random.Next(1, 1000);
                    string keysQuery = $"INSERT INTO Ключи ( Имя, Ключ, [Имя связи], [С чем связано]) VALUES ( '{TableName}', ";
                    string alterTAbleQuery = "ALTER TABLE [" + comboBox1.Text + "] ADD COLUMN";
                    string alter1TAbleQuery = $"ALTER TABLE [{comboBox1.Text}] ADD CONSTRAINT {relations} FOREIGN KEY ";
                    string createTableQuery = "CREATE TABLE [" + TableName + "] (";
                    for (int i = 0; i < FieldsAmount; i++)
                    {
                        if (!CheckComboBox(comboBoxes[i]))
                        {
                            isComboBoxesNotUsed = true;
                        }
                    }
                    if ((!isComboBoxesNotUsed) && moreThanZeroCHeckBoxesChenged)
                    {
                        for (int i = 0; i < FieldsAmount; i++)
                        {
                            if (comboBoxes[i].Text == "int")
                            {
                                if (checkBoxes[i].Checked)
                                {
                                    if (i == (FieldsAmount - 1))
                                    {
                                        createTableQuery += $" [{textBoxes[i].Text}] LONG PRIMARY KEY";
                                        alterTAbleQuery += $" [{textBoxes[i].Text}] LONG;";
                                        alter1TAbleQuery += $"([{textBoxes[i].Text}]) REFERENCES [{TableName}] ([{textBoxes[i].Text}]);";
                                        keysQuery += $"'{textBoxes[i].Text}', '{relations}', '{comboBox1.Text}');";
                                    }
                                    else
                                    {
                                        createTableQuery += $" [{textBoxes[i].Text}] LONG PRIMARY KEY,";
                                        alterTAbleQuery += $" [{textBoxes[i].Text}] LONG;";
                                        alter1TAbleQuery += $"([{textBoxes[i].Text}]) REFERENCES [{TableName}] ([{textBoxes[i].Text}]);";
                                        keysQuery += $"'{textBoxes[i].Text}', '{relations}', '{comboBox1.Text}');";
                                    }

                                }
                                else
                                {
                                    if (i == (FieldsAmount - 1))
                                    {
                                        createTableQuery += $" [{textBoxes[i].Text}] LONG";
                                    }
                                    else
                                    {
                                        createTableQuery += $" [{textBoxes[i].Text}] LONG,";
                                    }

                                }

                            }
                            else
                            {
                                if (checkBoxes[i].Checked)
                                {
                                    if (i == (FieldsAmount - 1))
                                    {
                                        createTableQuery += $" [{textBoxes[i].Text}] TEXT(255) PRIMARY KEY";
                                        alterTAbleQuery += $" [{textBoxes[i].Text}] TEXT(255);";
                                        alter1TAbleQuery += $"([{textBoxes[i].Text}]) REFERENCES [{TableName}] ([{textBoxes[i].Text}]);";
                                        keysQuery += $"'{textBoxes[i].Text}', '{relations}', '{comboBox1.Text}');";
                                    }
                                    else
                                    {
                                        createTableQuery += $" [{textBoxes[i].Text}] TEXT(255) PRIMARY KEY,";
                                        alterTAbleQuery += $" [{textBoxes[i].Text}] TEXT(255);";
                                        alter1TAbleQuery += $"([{textBoxes[i].Text}]) REFERENCES [{TableName}] ([{textBoxes[i].Text}]);";
                                        keysQuery += $"'{textBoxes[i].Text}', '{relations}', '{comboBox1.Text}');";
                                    }

                                }
                                else
                                {
                                    if (i == (FieldsAmount - 1))
                                    {
                                        createTableQuery += $" [{textBoxes[i].Text}] TEXT(255)";
                                    }
                                    else
                                    {
                                        createTableQuery += $" [{textBoxes[i].Text}] TEXT(255),";
                                    }

                                }
                            }
                        }
                        createTableQuery += ");";
                        string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBase.mdb;";
                        OleDbConnection dbConnection = new OleDbConnection(connectionString);
                        dbConnection.Open();
                        OleDbCommand cmd1 = new OleDbCommand(createTableQuery, dbConnection);
                        cmd1.ExecuteNonQuery();
                        OleDbCommand cmd2 = new OleDbCommand(alterTAbleQuery, dbConnection);
                        cmd2.ExecuteNonQuery();
                        OleDbCommand cmd3 = new OleDbCommand(alter1TAbleQuery, dbConnection);
                        cmd3.ExecuteNonQuery();
                        OleDbCommand cmd4 = new OleDbCommand(keysQuery, dbConnection);
                        cmd4.ExecuteNonQuery();
                        dbConnection.Close();
                        MessageBox.Show("Вы успешно создали таблицу!");
                        this.Close();
                    }
                    else if ((!isComboBoxesNotUsed))
                    {
                        
                    }
                    else
                    {
                        MessageBox.Show("Заполните все ячейки с типом поля!!");
                    }
                }
                else 
                {
                    MessageBox.Show("Выберите таблицу с которой хотите связать!!!");
                }
            }
            else
            {
                bool isComboBoxesNotUsed = false;
                bool notOneMoreCheckBoxChanged = NotOneMoreCheckBoxChanged();
                string createTableQuery = "CREATE TABLE [" + TableName + "] (";
                for (int i = 0; i < FieldsAmount; i++)
                {
                    if (!CheckComboBox(comboBoxes[i]))
                    {
                        isComboBoxesNotUsed = true;
                    }
                }
                if ((!isComboBoxesNotUsed) && notOneMoreCheckBoxChanged)
                {
                    for (int i = 0; i < FieldsAmount; i++)
                    {
                        if (comboBoxes[i].Text == "int")
                        {
                            if (checkBoxes[i].Checked)
                            {
                                if (i == (FieldsAmount - 1))
                                {
                                    createTableQuery += $" [{textBoxes[i].Text}] LONG PRIMARY KEY";
                                }
                                else
                                {
                                    createTableQuery += $" [{textBoxes[i].Text}] LONG PRIMARY KEY,";
                                }

                            }
                            else
                            {
                                if (i == (FieldsAmount - 1))
                                {
                                    createTableQuery += $" [{textBoxes[i].Text}] LONG";
                                }
                                else
                                {
                                    createTableQuery += $" [{textBoxes[i].Text}] LONG,";
                                }

                            }

                        }
                        else
                        {
                            if (checkBoxes[i].Checked)
                            {
                                if (i == (FieldsAmount - 1))
                                {
                                    createTableQuery += $" [{textBoxes[i].Text}] TEXT(255) PRIMARY KEY";
                                }
                                else
                                {
                                    createTableQuery += $" [{textBoxes[i].Text}] TEXT(255) PRIMARY KEY,";
                                }

                            }
                            else
                            {
                                if (i == (FieldsAmount - 1))
                                {
                                    createTableQuery += $" [{textBoxes[i].Text}] TEXT(255)";
                                }
                                else
                                {
                                    createTableQuery += $" [{textBoxes[i].Text}] TEXT(255),";
                                }

                            }
                        }
                    }
                    createTableQuery += ");";
                    string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBase.mdb;";
                    OleDbConnection dbConnection = new OleDbConnection(connectionString);
                    dbConnection.Open();
                    OleDbCommand cmd1 = new OleDbCommand(createTableQuery, dbConnection);
                    cmd1.ExecuteNonQuery();
                    dbConnection.Close();
                    MessageBox.Show("Вы успешно создали таблицу!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Заполните все ячейки с типом поля!!");
                }
            }
            
            
        }
        public void Renew()
        {
            comboBox1.Items.Clear();
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBase.mdb;";
            OleDbConnection dbConnection = new OleDbConnection(connectionString);
            dbConnection.Open();
            OleDbCommand cmd = new OleDbCommand("SELECT Ключи.Имя FROM Ключи;", dbConnection);
            List<string> list = new List<string>();
            var m = cmd.ExecuteReader();
            while (m.Read())
            {
                list.Add((string)m[0]);
            }
            foreach (string tableName in list)
            {
                comboBox1.Items.Add(tableName);
            }
            dbConnection.Close();
        }
    }
}
