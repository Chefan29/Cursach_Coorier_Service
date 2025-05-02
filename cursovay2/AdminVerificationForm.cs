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

namespace cursovaya
{
    public partial class AdminVerificationForm : Form
    {
        public AdminVerificationForm()
        {
            InitializeComponent();
        }

        private void ToAdminForm_Click(object sender, EventArgs e)
        {
            if (Autorization())
            {
                AdminForm form = new AdminForm();
                form.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный Логин или Пароль!!!");
            }
            
        }
        public bool Autorization()
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBase.mdb;";
            OleDbConnection dbConnection = new OleDbConnection(connectionString);
            dbConnection.Open();
            OleDbCommand cmd = new OleDbCommand("SELECT Авторизация.Id, Авторизация.Login, Авторизация.Password FROM Авторизация WHERE (((Авторизация.Admin)=True));", dbConnection);
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
    }
}
