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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace cursovaya
{
    public partial class CouriersEditForm : Form
    {
        public AdminForm AdminForm;
        public CouriersEditForm(AdminForm adminForm)
        {
            InitializeComponent();
            AdminForm = adminForm;
            listView1.Columns.Add("Id курьера");
            listView1.Columns.Add("ФИО");
            listView1.Columns.Add("Местоположение");
            listView1.Columns.Add("Широта");
            listView1.Columns.Add("Долгота");
            Renew();
        }
        public void Renew()
        {
            listView1.Items.Clear();
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBase.mdb;";
            OleDbConnection dbConnection = new OleDbConnection(connectionString);
            dbConnection.Open();
            OleDbCommand cmd;
            cmd = new OleDbCommand($"SELECT Курьеры.Id, Курьеры.ФИО, Курьеры.Местоположение, Координаты.Широта, Координаты.Долгота " +
                $"FROM Координаты INNER JOIN Курьеры ON Координаты.Местоположение = Курьеры.Местоположение;", dbConnection);

            var m = cmd.ExecuteReader();

            while (m.Read())
            {
                ListViewItem listViewItem = new ListViewItem($"{m[0]}");
                listViewItem.SubItems.Add($"{m[1]}");
                listViewItem.SubItems.Add($"{m[2]}");
                listViewItem.SubItems.Add($"{m[3]}");
                listViewItem.SubItems.Add($"{m[4]}");
                listView1.Items.Add(listViewItem);
            }
            m.Close();

            dbConnection.Close();
        }
        public bool IdCheck()
        {
            int courierNumber;
            bool isInt = int.TryParse(textBox1.Text, out courierNumber);
            if (!isInt)
            {
                MessageBox.Show("Введенный вами id курьера не имел числовой формат!");
                return false;
            }
            else
            {
                string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBase.mdb;";
                OleDbConnection dbConnection = new OleDbConnection(connectionString);
                dbConnection.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT Id FROM Курьеры", dbConnection);
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
                    if (i == courierNumber)
                        NextStep = true;
                }
                if (NextStep)
                {
                    dbConnection.Close();
                    return true;
                }
                else
                {
                    MessageBox.Show("Такого курьера не существует!");
                    dbConnection.Close();
                    return false;
                }

            }
        }
        public bool FioCheck()
        {
            if (textBox3.Text == "" || textBox3.Text == " " || textBox3.Text == "  ")
            {
                MessageBox.Show("Вы забыли ввести ФИО");
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool PlaceAvailabilityCheck()
        {
            if (textBox2.Text == "" || textBox2.Text == " " || textBox2.Text == "  ")
            {
                MessageBox.Show("Вы забыли ввести Местоположение");
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool PlaceInDbCheck()
        {

            if (textBox2.Text == "" || textBox2.Text == " " || textBox2.Text == "  ")
            {
                MessageBox.Show("Вы забыли ввести Местоположение");
                return false;
            }
            else
            {
                string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBase.mdb;";
                OleDbConnection dbConnection = new OleDbConnection(connectionString);
                dbConnection.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT [Местоположение] FROM Координаты", dbConnection);
                List<string> list = new List<string>();
                var m = cmd.ExecuteReader();
                while (m.Read())
                {
                    list.Add((string)m[0]);
                }
                m.Close();
                bool NextStep = false;
                foreach (string i in list)
                {
                    if (i == textBox2.Text)
                        NextStep = true;
                }
                if (NextStep)
                {
                    dbConnection.Close();
                    return true;
                }
                else
                {
                    MessageBox.Show("Такого Местоположения не существует в бд! Заполните поля 'Широта' и 'Долгота'!");
                    dbConnection.Close();
                    return false;
                }
            }
            

        }
        public bool WidthCheck()
        {
            double width;
            bool isDouble = Double.TryParse(textBox4.Text, out width);
            if (!isDouble)
            {
                MessageBox.Show("Введенная вами Широта имела не числовой формат!");
                return false;
            }
            else
            {
                if (width > 55.9 &&  width < 56.2)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Введенная вами широта не соответсвует заданным координатам от 55,9 до 56,2 с.ш.!");
                    return false;
                }

            }
        }
        public bool LengthCheck()
        {
            double length;
            bool isDouble = Double.TryParse(textBox5.Text, out length);
            if (!isDouble)
            {
                MessageBox.Show("Введенная вами Долгота имела не числовой формат!");
                return false;
            }
            else
            {
                if (length > 92.6 && length < 92.9)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Введенная вами долгота не соответсвует заданным координатам от 92,6 до 92,9 с.ш.!");
                    return false;
                }

            }
        }
        public bool PlaceContainsInDb()
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBase.mdb;";
            OleDbConnection dbConnection = new OleDbConnection(connectionString);
            dbConnection.Open();
            OleDbCommand cmd = new OleDbCommand("SELECT [Местоположение] FROM Координаты", dbConnection);
            List<string> list = new List<string>();
            var m = cmd.ExecuteReader();
            while (m.Read())
            {
                list.Add((string)m[0]);
            }
            m.Close();
            bool NextStep = false;
            foreach (string i in list)
            {
                if (i == textBox2.Text)
                    NextStep = true;
            }
            if (NextStep)
            {
                dbConnection.Close();
                MessageBox.Show("Такого местоположение уже существует в бд! Поставте галочку!");
                return false;
            }
            else
            {
                dbConnection.Close();
                return true;
            }
        }


        public bool CourierIsUsedInOrdersTable()
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string selected = listView1.SelectedItems[0].Text;
                string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBase.mdb;";
                OleDbConnection dbConnection = new OleDbConnection(connectionString);
                dbConnection.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT [id курьера] FROM Заказы", dbConnection);
                List<string> list = new List<string>();
                var m = cmd.ExecuteReader();
                while (m.Read())
                {
                    list.Add(m[0].ToString());
                }
                m.Close();
                bool NextStep = false;
                foreach (string i in list)
                {
                    if (i == selected)
                        NextStep = true;
                }
                if (NextStep)
                {
                    dbConnection.Close();
                    MessageBox.Show("Эта запись используется в таблице Заказы!!!");
                    return false;
                }
                else
                {
                    dbConnection.Close();
                    return true;
                }
            }
            else
            {
                MessageBox.Show("Вы не выбрали заказ!");
                return false;
            }
        }
        private void AddNewCourierButton_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                bool fioCheck = FioCheck();
                bool placeInDbCheck = PlaceInDbCheck();
                if (fioCheck && placeInDbCheck)
                {
                    string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBase.mdb;";
                    OleDbConnection dbConnection = new OleDbConnection(connectionString);
                    dbConnection.Open();
                    OleDbCommand cmd = new OleDbCommand($"INSERT INTO Курьеры ( ФИО, Местоположение ) VALUES ('{textBox3.Text}', '{textBox2.Text}');", dbConnection);
                    cmd.ExecuteNonQuery();
                    dbConnection.Close();
                }
            }
            else
            {
                bool fioCheck = FioCheck();
                bool placeAvailabilityCheck = PlaceAvailabilityCheck();
                bool placeContains = PlaceContainsInDb();
                bool widthCheck = WidthCheck();
                bool lengthCheck = LengthCheck();
                string width = textBox4.Text.Replace(',', '.');
                string length = textBox5.Text.Replace(',', '.');
                if (fioCheck && placeAvailabilityCheck && placeContains && widthCheck &&  lengthCheck)
                {
                    string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBase.mdb;";
                    OleDbConnection dbConnection = new OleDbConnection(connectionString);
                    dbConnection.Open();
                    OleDbCommand cmd1 = new OleDbCommand($"INSERT INTO Координаты ( Местоположение, Широта, Долгота ) VALUES ('{textBox2.Text}', {width}, {length});", dbConnection);
                    cmd1.ExecuteNonQuery();
                    OleDbCommand cmd = new OleDbCommand($"INSERT INTO Курьеры ( ФИО, Местоположение ) VALUES ('{textBox3.Text}', '{textBox2.Text}');", dbConnection);
                    cmd.ExecuteNonQuery();
                    dbConnection.Close();
                }
            }
            Renew();

        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (CourierIsUsedInOrdersTable())
            {
                int selected = Convert.ToInt32(listView1.SelectedItems[0].Text);
                string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBase.mdb;";
                OleDbConnection dbConnection = new OleDbConnection(connectionString);
                dbConnection.Open();
                OleDbCommand cmd = new OleDbCommand("DELETE Курьеры.[Id] FROM Курьеры " +
                    $"WHERE (((Курьеры.[Id])={selected}));", dbConnection);
                cmd.ExecuteNonQuery();
                dbConnection.Close();
                Renew();
                AdminForm.Renew("frefre", false);
            }
        }

        private void chooseButton_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                textBox1.Text = listView1.SelectedItems[0].SubItems[0].Text;
                textBox3.Text = listView1.SelectedItems[0].SubItems[1].Text;
                textBox2.Text = listView1.SelectedItems[0].SubItems[2].Text;
                textBox4.Text = listView1.SelectedItems[0].SubItems[3].Text;
                textBox5.Text = listView1.SelectedItems[0].SubItems[4].Text;
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                bool idCheck = IdCheck();
                bool couriersIsUsed = CourierIsUsedInOrdersTable();
                bool fioCheck = FioCheck();
                bool placeInDbCheck = PlaceInDbCheck();

                if (idCheck && couriersIsUsed && fioCheck && placeInDbCheck)
                {
                    string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBase.mdb;";
                    OleDbConnection dbConnection = new OleDbConnection(connectionString);
                    dbConnection.Open();
                    OleDbCommand cmd = new OleDbCommand($"UPDATE Курьеры SET Курьеры.ФИО = '{textBox3.Text}', Курьеры.Местоположение = '{textBox2.Text}' WHERE (((Курьеры.Id)={textBox1.Text}));", dbConnection);
                    cmd.ExecuteNonQuery();
                    dbConnection.Close();
                }
            }
            else
            {
                bool idCheck = IdCheck();
                bool couriersIsUsed = CourierIsUsedInOrdersTable();
                bool fioCheck = FioCheck();
                bool placeContains = PlaceContainsInDb();
                bool widthCheck = WidthCheck();
                bool lengthCheck = LengthCheck();
                string width = textBox4.Text.Replace(',', '.');
                string length = textBox5.Text.Replace(',', '.');

                if (idCheck && couriersIsUsed && fioCheck && placeContains && widthCheck && lengthCheck)
                {
                    string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBase.mdb;";
                    OleDbConnection dbConnection = new OleDbConnection(connectionString);
                    dbConnection.Open();
                    OleDbCommand cmd2 = new OleDbCommand($"INSERT INTO Координаты ( Местоположение, Широта, Долгота ) VALUES ('{textBox2.Text}', {width}, {length});", dbConnection);
                    cmd2.ExecuteNonQuery();
                    OleDbCommand cmd = new OleDbCommand($"UPDATE Курьеры SET Курьеры.ФИО = '{textBox3.Text}', Курьеры.Местоположение = '{textBox2.Text}' WHERE (((Курьеры.Id)={textBox1.Text}));", dbConnection);
                    cmd.ExecuteNonQuery();
                    dbConnection.Close();
                }
            }
            Renew();
            AdminForm.Renew("gwerg", false);
        }
    }

}

