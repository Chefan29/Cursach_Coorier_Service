using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace cursovaya
{
    public partial class CourierChooseForm : Form
    {
        public int CourierNumber;
        public CourierChooseForm()
        {
            InitializeComponent();
        }
        public bool Autorization()
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBase.mdb;";
            OleDbConnection dbConnection = new OleDbConnection(connectionString);
            dbConnection.Open();
            OleDbCommand cmd = new OleDbCommand("SELECT Авторизация.Id, Авторизация.Login, Авторизация.Password FROM Авторизация WHERE (((Авторизация.Admin)=False));", dbConnection);
            List<int> listId = new List<int>();
            List<string> listLogin = new List<string>();
            List<string> listPassword = new List<string>();
            var m = cmd.ExecuteReader();
            while (m.Read())
            {
                listId.Add((int)m[0]);
                listLogin.Add((string)m[1]);
                listPassword.Add((string)m[2]);
            }
            m.Close();
            dbConnection.Close();
            bool trueLogin = false;
            int indexOfTrueLogin = -1;
            int i = -1;
            foreach (string login in listLogin)
            {
                ++i;

                if (textBox1.Text == login)
                {
                    trueLogin = true;
                    indexOfTrueLogin = i; break;
                }
            }
            if (trueLogin)
            {
                if (listPassword[indexOfTrueLogin] == md5.hashPassword(textBox2.Text))
                {
                    CourierNumber = listId[indexOfTrueLogin];
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
        private void NextButton_Click(object sender, EventArgs e)
        {
            if (Autorization())
            {
                bool Noticed = false;

                string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBase.mdb;";
                OleDbConnection dbConnection = new OleDbConnection(connectionString);
                dbConnection.Open();
                OleDbCommand cmd = new OleDbCommand($"SELECT Авторизация.Noticed FROM Авторизация WHERE (((Авторизация.Id)={CourierNumber}));", dbConnection);
                var m = cmd.ExecuteReader();
                while (m.Read())
                {
                    Noticed = Convert.ToBoolean(m[0]);
                }
                m.Close();
                dbConnection.Close();
                if (!Noticed)
                {
                    MessageBox.Show("Появились новые заказы!!!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    OleDbConnection dbConnection1 = new OleDbConnection(connectionString);
                    dbConnection1.Open();
                    OleDbCommand cmd1 = new OleDbCommand($"UPDATE Авторизация SET Авторизация.Noticed = True WHERE (((Авторизация.Id)={CourierNumber}));", dbConnection1);
                    cmd1.ExecuteNonQuery();
                    dbConnection.Close();
                }

                ClientForm clientForm = new ClientForm(CourierNumber);
                clientForm.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Вы ввели неправильный логин или пароль!!!");
            }

        }
    }
}
