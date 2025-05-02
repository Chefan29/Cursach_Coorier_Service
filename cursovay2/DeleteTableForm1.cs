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
    public partial class DeleteTableForm1 : Form
    {
        public DeleteTableForm1()
        {
            InitializeComponent();
            Renew();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("Выберите таблицу, которую хотите удалить!");
            }
            else if (comboBox1.Text == "Рестораны" || comboBox1.Text == "Клиенты" || comboBox1.Text == "Курьеры" || comboBox1.Text == "Заказы")
            {
                    MessageBox.Show("Эту таблицу нельзя удалить, она основа приложения!");
            }
            else
            {
                string keyName = "";
                string relationsName ="";
                string WhatRelate = "";
                List<string> Names = new List<string>();
                List<string> Keys = new List<string>();
                string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBase.mdb;";
                OleDbConnection dbConnection = new OleDbConnection(connectionString);
                dbConnection.Open();
                OleDbCommand cmd = new OleDbCommand($"SELECT Ключи.Имя, Ключи.Ключ, Ключи.[Имя связи], Ключи.[С чем связано] FROM Ключи WHERE (((Ключи.Имя)='{comboBox1.Text}'));", dbConnection);
                var m = cmd.ExecuteReader();

                while (m.Read())
                {
                    keyName = (string)m[1];
                    relationsName = (string)m[2];
                    WhatRelate = (string)m[3];
                }
                m.Close();
                dbConnection.Close();

                OleDbConnection dbConnection1 = new OleDbConnection(connectionString);
                dbConnection.Open();
                OleDbCommand cmd1 = new OleDbCommand($"ALTER TABLE [{WhatRelate}] DROP CONSTRAINT [{relationsName}];", dbConnection);
                cmd1.ExecuteNonQuery();
                OleDbCommand cmd2 = new OleDbCommand($"ALTER TABLE [{WhatRelate}] DROP COLUMN [{keyName}];", dbConnection);
                cmd2.ExecuteNonQuery();
                OleDbCommand cmd3 = new OleDbCommand($"DROP TABLE [{comboBox1.Text}];", dbConnection);
                cmd3.ExecuteNonQuery();
                OleDbCommand cmd4 = new OleDbCommand($"DELETE FROM [Ключи] WHERE Имя = '{comboBox1.Text}';", dbConnection);
                cmd4.ExecuteNonQuery();
                dbConnection1.Close();

                MessageBox.Show($"Вы удалили связь {relationsName} и столбец {keyName} с таблицей {WhatRelate}");
                this.Close();
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
