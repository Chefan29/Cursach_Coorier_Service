using cursovay2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MailKit.Net.Smtp;
using MimeKit;

namespace cursovaya
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
            Renew("все равно что передать", false);
            ListsViewsInitialize();
        }
        public void ListsViewsInitialize()
        {
            listView1.Columns.Add("Id заказа");
            listView1.Columns.Add("Id Ресторана");
            listView1.Columns.Add("Id заказчика");
            listView1.Columns.Add("Выполняется");
            listView1.Columns.Add("Id курьера");
            listView1.Columns.Add("Размер заказа");
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            listView2.Columns.Add("Id курьера");
            listView2.Columns.Add("ФИО курьера");
            listView2.Columns.Add("Местоположение");
            listView2.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listView2.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);

            listView3.Columns.Add("Id клиента");
            listView3.Columns.Add("ФИО клиента");
            listView3.Columns.Add("Местоположение");
            listView3.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listView3.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);

            listView4.Columns.Add("Id Ресторана");
            listView4.Columns.Add("Ресторан");
            listView4.Columns.Add("Местоположение");
            listView4.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listView4.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);
        }/*Инициализация листвью*/
        public void Renew (string command, bool Filtres)
        {
            listView1.Items.Clear();
            listView2.Items.Clear();
            listView3.Items.Clear();
            listView4.Items.Clear();

            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBase.mdb;";
            OleDbConnection dbConnection = new OleDbConnection(connectionString);
            dbConnection.Open();

            #region Обновление таблицы заказов
            OleDbCommand cmd;
            if (Filtres == false)
            {
                cmd = new OleDbCommand($"SELECT Заказы.[Id заказа], Заказы.[Id ресторана], Заказы.[Id заказчика], Заказы.Выполняется, Заказы.[id курьера], Заказы.[Размер заказа] FROM Заказы;", dbConnection);
            }
            else
            {
                cmd = new OleDbCommand(command, dbConnection);
            }
            
            var m = cmd.ExecuteReader();

            while (m.Read())
            {
                ListViewItem listViewItem = new ListViewItem($"{m[0]}");
                listViewItem.SubItems.Add($"{m[1]}");
                listViewItem.SubItems.Add($"{m[2]}");
                listViewItem.SubItems.Add($"{m[3]}");
                listViewItem.SubItems.Add($"{m[4]}");
                listViewItem.SubItems.Add($"{m[5]}");
                listView1.Items.Add(listViewItem);
            }
            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("По вашему запросу ничего не найдено");
            }

            m.Close();
            #endregion

            #region Обновление таблицы курьеров
            OleDbCommand cmd1 = new OleDbCommand("SELECT Курьеры.Id, Курьеры.ФИО, Курьеры.Местоположение FROM Курьеры;", dbConnection);

            var m1 = cmd1.ExecuteReader();

            while (m1.Read())
            {
                if (!m1.IsDBNull(0))
                {
                    ListViewItem listViewItem = new ListViewItem($"{m1[0]}");
                    listViewItem.SubItems.Add($"{m1[1]}");
                    listViewItem.SubItems.Add($"{m1[2]}");
                    listView2.Items.Add(listViewItem);
                }
                else
                {
                    MessageBox.Show("По вашему запросу ничего не найдено");
                }
            }
            #endregion

            #region Обновление таблицы клиентов
            OleDbCommand cmd2 = new OleDbCommand("SELECT Клиенты.Id, Клиенты.ФИО, Клиенты.Местоположение FROM Клиенты;", dbConnection);

            var m2 = cmd2.ExecuteReader();

            while (m2.Read())
            {
                if (!m2.IsDBNull(0))
                {
                    ListViewItem listViewItem = new ListViewItem($"{m2[0]}");
                    listViewItem.SubItems.Add($"{m2[1]}");
                    listViewItem.SubItems.Add($"{m2[2]}");
                    listView3.Items.Add(listViewItem);
                }
                else
                {
                    MessageBox.Show("По вашему запросу ничего не найдено");
                }
            }
            #endregion

            #region Обновление таблицы ресторанов
            OleDbCommand cmd3 = new OleDbCommand("SELECT Рестораны.Id, Рестораны.Название, Рестораны.Адрес FROM Рестораны;", dbConnection);

            var m3 = cmd3.ExecuteReader();

            while (m3.Read())
            {
                if (!m3.IsDBNull(0))
                {
                    ListViewItem listViewItem = new ListViewItem($"{m3[0]}");
                    listViewItem.SubItems.Add($"{m3[1]}");
                    listViewItem.SubItems.Add($"{m3[2]}");
                    listView4.Items.Add(listViewItem);
                }
                else
                {
                    MessageBox.Show("По вашему запросу ничего не найдено");
                }
            }
            #endregion

            dbConnection.Close();
        }

        private void FiltrButton_Click(object sender, EventArgs e)
        {
            bool textbox1 = textBox1.Text != "";
            bool textbox2 = textBox2.Text != "";

            if (textbox1 && textbox2) 
            {
                string cmd0;
                if ((TextBOxCheck(textBox1.Text, "курьера") != -1) && (TextBOxCheck(textBox2.Text, "ресторана") != -1)) 
                { 
                    if ((IdCheck(TextBOxCheck(textBox1.Text, "курьера"), "Курьеры")) && (IdCheck(TextBOxCheck(textBox2.Text, "ресторана"), "Рестораны")))
                    {
                        cmd0 = "SELECT Заказы.[Id заказа], Заказы.[Id ресторана], Заказы.[Id заказчика], Заказы.Выполняется, Заказы.[id курьера], Заказы.[Размер заказа] " +
                            "FROM Заказы " +
                            $"WHERE (((Заказы.[Id ресторана])={TextBOxCheck(textBox2.Text, "ресторана")}) AND ((Заказы.[id курьера])={TextBOxCheck(textBox1.Text, "курьера")}));";
                        listView1.Items.Clear();
                        Renew(cmd0, true);
                    }
                }
            }
            else if (textbox1)
            {
                string cmd0;
                if (TextBOxCheck(textBox1.Text, "курьера") != -1)
                {
                    if (IdCheck(TextBOxCheck(textBox1.Text, "курьера"), "Курьеры"))
                    {
                        cmd0 = "SELECT Заказы.[Id заказа], Заказы.[Id ресторана], Заказы.[Id заказчика], Заказы.Выполняется, Заказы.[id курьера], Заказы.[Размер заказа] " +
                            "FROM Заказы " +
                            $"WHERE ((Заказы.[id курьера])={TextBOxCheck(textBox1.Text, "курьера")});";
                        listView1.Items.Clear();
                        Renew(cmd0, true);
                    }
                }
            }
            else if (textbox2)
            {
                string cmd0;
                if (TextBOxCheck(textBox2.Text, "ресторана") != -1)
                {
                    if (IdCheck(TextBOxCheck(textBox2.Text, "ресторана"), "Рестораны"))
                    {
                        cmd0 = "SELECT Заказы.[Id заказа], Заказы.[Id ресторана], Заказы.[Id заказчика], Заказы.Выполняется, Заказы.[id курьера], Заказы.[Размер заказа] " +
                            "FROM Заказы " +
                            $"WHERE ((Заказы.[Id ресторана])={TextBOxCheck(textBox2.Text, "ресторана")});";
                        listView1.Items.Clear();
                        Renew(cmd0, true);
                    }
                }
            }
            else
            {
                listView1.Items.Clear();
                Renew("разницы нет", false);
            }
        }
        public int TextBOxCheck(string textFromTextBox, string whooseId)
        {
            int Id;
            bool isInt = int.TryParse(textFromTextBox, out Id);
            if (!isInt)
            {
                MessageBox.Show($"Введенный вами id {whooseId} имел не числовой формат!");
                return -1;
            }            
            else
                return Id;
        }
        public bool IdCheck(int id, string tableName)
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBase.mdb;";
            OleDbConnection dbConnection = new OleDbConnection(connectionString);
            dbConnection.Open();
            OleDbCommand cmd = new OleDbCommand($"SELECT Id FROM {tableName}", dbConnection);
            List<int> list = new List<int>();
            var m = cmd.ExecuteReader();
            while (m.Read())
            {
                list.Add((int)m[0]);
            }
            m.Close();
            bool NextStep = false;
            foreach (int i in list)
            {
                if (i == id)
                    NextStep = true;
            }
            if (!NextStep)
            {
                MessageBox.Show($"Такого Id нет в таблице {tableName}");
            }
            dbConnection.Close();
        
            return NextStep;
        }

        private void EditTableButton_Click(object sender, EventArgs e)
        {
            TableChooseForm form = new TableChooseForm(this);
            form.Show();
        }

        private void AddNewTableButton_Click(object sender, EventArgs e)
        {
            AddTableForm2 form = new AddTableForm2();
            form.Show();
        }

        private void DeleteTableButton_Click(object sender, EventArgs e)
        {
            DeleteTableForm1 form = new DeleteTableForm1();
            form.Show();
        }

        private void NoticeButton_Click(object sender, EventArgs e)
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBase.mdb;";
            OleDbConnection dbConnection = new OleDbConnection(connectionString);
            dbConnection.Open();
            OleDbCommand cmd = new OleDbCommand($"UPDATE Авторизация SET Авторизация.Noticed = False WHERE (((Авторизация.Admin)=False));", dbConnection);
            cmd.ExecuteNonQuery();
            dbConnection.Close();
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress("Егор2", "sosy.2006@mail.ru"));
                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = message
                };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.mail.ru", 465, true);
                    await client.AuthenticateAsync("sosy.2006@mail.ru", "w3ZMRHWpQDLJhcCQv0hT");
                    await client.SendAsync(emailMessage);

                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Возникла ошибка при отправке письма: {e.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void EmailButton_Click(object sender, EventArgs e)
        {
            List <string> emails = new List<string>();
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBase.mdb;";
            OleDbConnection dbConnection = new OleDbConnection(connectionString);
            dbConnection.Open();
            OleDbCommand cmd = new OleDbCommand($"SELECT Авторизация.Login FROM Авторизация WHERE (((Авторизация.Admin)=False));", dbConnection);
            var m = cmd.ExecuteReader();
            while (m.Read())
            {
                emails.Add($"{m[0]}");
            }
            m.Close();
            dbConnection.Close();
            foreach (var email in emails)
            {
                await SendEmailAsync(email, "КурьерPro Уведомление", "Внимание, появились новые заказы!");
            }
        }

        private void StatisticsButton_Click(object sender, EventArgs e)
        {
            StatisticsForm form = new StatisticsForm();
            form.Show();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {

        }
    }
}
