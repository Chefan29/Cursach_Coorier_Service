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
    public partial class OrdersEditFormcs : Form
    {
        public AdminForm adminForm;
        public OrdersEditFormcs(AdminForm aadminForm)
        {
            InitializeComponent();
            adminForm = aadminForm;
            listView1.Columns.Add("Id заказа");
            listView1.Columns.Add("Id Ресторана");
            listView1.Columns.Add("Id заказчика");
            listView1.Columns.Add("Выполняется");
            listView1.Columns.Add("Id курьера");
            listView1.Columns.Add("Размер заказа");
            Renew();
        }
        public void Renew()
        {
            listView1.Items.Clear();
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBase.mdb;";
            OleDbConnection dbConnection = new OleDbConnection(connectionString);
            dbConnection.Open();
            OleDbCommand cmd;
            cmd = new OleDbCommand($"SELECT Заказы.[Id заказа], Заказы.[Id ресторана], Заказы.[Id заказчика], Заказы.Выполняется, Заказы.[id курьера], Заказы.[Размер заказа] FROM Заказы;", dbConnection);

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
            m.Close();

            dbConnection.Close();
        }
        private void AddButton_Click(object sender, EventArgs e)
        {
            
            bool restaurantIdCheck = RestaurantIdCheck();
            bool clientsIdCheck = ClientsIdCheck();
            bool inProgressCheck = InProgressCheck();
            bool falseProcess = FalseProcess();
            bool courierIdCheck = CourierIdCheck();
            bool orderSizeCheck = OrderSizeCheck();


            if (restaurantIdCheck && clientsIdCheck && inProgressCheck && falseProcess && courierIdCheck && orderSizeCheck)
            {
                string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBase.mdb;";
                OleDbConnection dbConnection = new OleDbConnection(connectionString);
                dbConnection.Open();
                OleDbCommand cmd = new OleDbCommand("INSERT INTO Заказы ( [Id ресторана], [Id заказчика], Выполняется, [id курьера], [Размер заказа] ) " +
                    $"VALUES ({textBox1.Text}, {textBox2.Text}, {inProgressComboBox.Text}, {textBox4.Text}, '{orderSizeСomboBox.Text}');", dbConnection);
                cmd.ExecuteNonQuery();
                dbConnection.Close();
            }
            else if (restaurantIdCheck && clientsIdCheck && inProgressCheck && !falseProcess && orderSizeCheck)
            {
                string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBase.mdb;";
                OleDbConnection dbConnection = new OleDbConnection(connectionString);
                dbConnection.Open();
                OleDbCommand cmd = new OleDbCommand("INSERT INTO Заказы ( [Id ресторана], [Id заказчика], Выполняется, [Размер заказа] ) " +
                    $"VALUES ({textBox1.Text}, {textBox2.Text}, {inProgressComboBox.Text}, '{orderSizeСomboBox.Text}');", dbConnection);
                cmd.ExecuteNonQuery();
                dbConnection.Close();
            }
            Renew();
            adminForm.Renew("frefre", false);
        }
        #region Проверки
        public bool RestaurantIdCheck()
        {
            int restaurantNumber;
            bool isInt = int.TryParse(textBox1.Text, out restaurantNumber);
            if (!isInt)
            {
                MessageBox.Show("Введенный вами id ресторана не имел числовой формат!");
                return false;
            }
            else
            {
                string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBase.mdb;";
                OleDbConnection dbConnection = new OleDbConnection(connectionString);
                dbConnection.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT Id FROM Рестораны", dbConnection);
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
                    if (i == restaurantNumber)
                        NextStep = true;
                }
                if (NextStep)
                {
                    dbConnection.Close();
                    return true;
                }
                else
                {
                    MessageBox.Show("Такого ресторана не существует!");
                    dbConnection.Close();
                    return false;
                }

            }
        }
        public bool ClientsIdCheck()
        {
            int clientNumber;
            bool isInt = int.TryParse(textBox2.Text, out clientNumber);
            if (!isInt)
            {
                MessageBox.Show("Введенный вами id заказчика не имел числовой формат!");
                return false;
            }
            else
            {
                string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBase.mdb;";
                OleDbConnection dbConnection = new OleDbConnection(connectionString);
                dbConnection.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT Id FROM Клиенты", dbConnection);
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
                    if (i == clientNumber)
                        NextStep = true;
                }
                if (NextStep)
                {
                    dbConnection.Close();
                    return true;
                }
                else
                {
                    MessageBox.Show("Такого заказчика не существует!");
                    dbConnection.Close();
                    return false;
                }

            }
        }
        public bool CourierIdCheck()
        {
            if (inProgressComboBox.Text == "True")
            {
                int courierNumber;
                bool isInt = int.TryParse(textBox4.Text, out courierNumber);
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
            else 
            {
                return true;
            }
        }
        public bool InProgressCheck()
        {
            if (inProgressComboBox.Text != "")
            {
                return true;
            }
            else
            {
                MessageBox.Show("Выберите значение поля 'Выполняется'");
                return false;
            }
        }
        public bool OrderSizeCheck()
        {
            if (orderSizeСomboBox.Text != "")
            {
                return true;
            }
            else
            {
                MessageBox.Show("Выберите значение поля 'Размер заказа'");
                return false;
            }
        }
        public bool FalseProcess()
        { 
            if (inProgressComboBox.Text == "True")
                return true;
            else
                return false;
        }
        public bool OrderIdCheck()
        {
            int OrderNumber;
            bool isInt = int.TryParse(textBox3.Text, out OrderNumber);
            if (!isInt)
            {
                MessageBox.Show("Введенный вами id заказа не имел числовой формат!");
                return false;
            }
            else
            {
                string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBase.mdb;";
                OleDbConnection dbConnection = new OleDbConnection(connectionString);
                dbConnection.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT [Id заказа] FROM Заказы", dbConnection);
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
                    if (i == OrderNumber)
                        NextStep = true;
                }
                if (NextStep)
                {
                    dbConnection.Close();
                    return true;
                }
                else
                {
                    MessageBox.Show("Такого заказа не существует!");
                    dbConnection.Close();
                    return false;
                }

            }
        }

        #endregion


        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int selected = Convert.ToInt32(listView1.SelectedItems[0].Text);
                string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBase.mdb;";
                OleDbConnection dbConnection = new OleDbConnection(connectionString);
                dbConnection.Open();
                OleDbCommand cmd = new OleDbCommand("DELETE Заказы.[Id заказа] FROM Заказы " +
                    $"WHERE (((Заказы.[Id заказа])={selected}));", dbConnection);
                cmd.ExecuteNonQuery();
                dbConnection.Close();
                Renew();
                adminForm.Renew("frefre", false);
            }

        }

        private void ChooseButton_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                textBox3.Text = listView1.SelectedItems[0].SubItems[0].Text;
                textBox1.Text = listView1.SelectedItems[0].SubItems[1].Text;
                textBox2.Text = listView1.SelectedItems[0].SubItems[2].Text;
                inProgressComboBox.Text = listView1.SelectedItems[0].SubItems[3].Text;
                textBox4.Text = listView1.SelectedItems[0].SubItems[4].Text;
                orderSizeСomboBox.Text = listView1.SelectedItems[0].SubItems[5].Text;
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {

            bool orderIdCheck = OrderIdCheck();
            bool restaurantIdCheck = RestaurantIdCheck();
            bool clientsIdCheck = ClientsIdCheck();
            bool inProgressCheck = InProgressCheck();
            bool falseProcess = FalseProcess();
            bool courierIdCheck = CourierIdCheck();
            bool orderSizeCheck = OrderSizeCheck();

            if (orderIdCheck && restaurantIdCheck && clientsIdCheck && inProgressCheck && falseProcess && courierIdCheck && orderSizeCheck)
            {
                string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBase.mdb;";
                OleDbConnection dbConnection = new OleDbConnection(connectionString);
                dbConnection.Open();
                OleDbCommand cmd = new OleDbCommand($"UPDATE Заказы SET Заказы.[Id ресторана] = {textBox1.Text}, Заказы.[Id заказчика] = {textBox2.Text}, Заказы.Выполняется = {inProgressComboBox.Text}, Заказы.[id курьера] = {textBox4.Text}, Заказы.[Размер заказа] = '{orderSizeСomboBox.Text}' WHERE (((Заказы.[Id заказа])={textBox3.Text}));", dbConnection);
                cmd.ExecuteNonQuery();
                dbConnection.Close();
            }
            else if (orderIdCheck && restaurantIdCheck && clientsIdCheck && inProgressCheck && !falseProcess && orderSizeCheck)
            {
                string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBase.mdb;";
                OleDbConnection dbConnection = new OleDbConnection(connectionString);
                dbConnection.Open();
                OleDbCommand cmd = new OleDbCommand($"UPDATE Заказы SET Заказы.[Id ресторана] = {textBox1.Text}, Заказы.[Id заказчика] = {textBox2.Text}, Заказы.Выполняется = {inProgressComboBox.Text}, Заказы.[Размер заказа] = '{orderSizeСomboBox.Text}' WHERE (((Заказы.[Id заказа])={textBox3.Text}));", dbConnection);
                cmd.ExecuteNonQuery();
                dbConnection.Close();
            }

            Renew();
            adminForm.Renew("frefre", false);
        }
    }
}
