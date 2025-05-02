using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace cursovaya
{
    public partial class ClientForm : Form
    {
        public int ClientNumber {  get; set; }
        private GMapOverlay markersOverlay = new GMapOverlay("markers"); // Слой для меток

        public ClientForm(int clientNumber)
        {
            InitializeComponent();
            ClientNumber = clientNumber;
            listView1.Columns.Add("Id");
            listView1.Columns.Add("Ресторан");
            listView1.Columns.Add("Адрес Ресторана");
            listView1.Columns.Add("ФИО клиента");
            listView1.Columns.Add("Адрес клиента");
            listView1.Columns.Add("Выполняется вами");
            listView1.Columns.Add("Размер заказа");
            listView1.Items.Clear();
            Renew("тест", false);
        }

        private void Filtres_Click(object sender, EventArgs e)
        {
            bool combBOx = (/*comboBox1 != null ||*/ comboBox1.Text != "");
            bool textBX = (textBox1.Text != "");
            if (combBOx)
            {
                string cmd = "SELECT Заказы.[Id заказа], Рестораны.Название, Рестораны.Адрес, Клиенты.ФИО, Клиенты.Местоположение, Заказы.Выполняется, Заказы.[Размер заказа]" +
                    " FROM Клиенты INNER JOIN (Рестораны INNER JOIN Заказы ON Рестораны.Id = Заказы.[Id ресторана]) ON Клиенты.Id = Заказы.[Id заказчика] " +
                    $"WHERE ((Заказы.[id курьера])={ClientNumber}) AND ((Заказы.Выполняется)=True)  OR  ((Заказы.[Размер заказа])='{comboBox1.Text}') AND ((Заказы.Выполняется)=False) ;";
                listView1.Items.Clear();
                Renew(cmd, true);
            }
            else if (textBX)
            {
                string cmd = "SELECT Заказы.[Id заказа], Рестораны.Название, Рестораны.Адрес, Клиенты.ФИО, Клиенты.Местоположение, Заказы.Выполняется, Заказы.[Размер заказа]" +
                                    "FROM Клиенты INNER JOIN(Рестораны INNER JOIN Заказы ON Рестораны.Id = Заказы.[Id ресторана]) ON Клиенты.Id = Заказы.[Id заказчика]" +
                                        $"WHERE ((Заказы.[id курьера])={ClientNumber}) AND ((Заказы.Выполняется)=True)  OR  ((Рестораны.Адрес) Like '%{textBox1.Text}%') AND ((Заказы.Выполняется)=False) ;";
                listView1.Items.Clear();
                Renew(cmd, true);
            }
            else if (textBX && combBOx)
            {
                string cmd = "SELECT Заказы.[Id заказа], Рестораны.Название, Рестораны.Адрес, Клиенты.ФИО, Клиенты.Местоположение, Заказы.Выполняется, Заказы.[Размер заказа]" +
                                    "FROM Клиенты INNER JOIN(Рестораны INNER JOIN Заказы ON Рестораны.Id = Заказы.[Id ресторана]) ON Клиенты.Id = Заказы.[Id заказчика]" +
                                        $"WHERE ((Заказы.[id курьера])={ClientNumber}) AND ((Заказы.Выполняется)=True)  OR  (((Рестораны.Адрес) Like '%{textBox1.Text}%') AND ((Заказы.Выполняется)=False) AND ((Заказы.[Размер заказа])='{comboBox1.Text}')) ;";
                listView1.Items.Clear();
                Renew(cmd, true);
            }
            else 
            {
                listView1.Items.Clear();
                Renew("без разницы что передать", false); 
            }
            
        }
        private void Renew(string command, bool Filtres) 
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBase.mdb;";
            OleDbConnection dbConnection = new OleDbConnection(connectionString);
            dbConnection.Open();
            OleDbCommand cmd;
            if (Filtres == false) 
            {
                cmd = new OleDbCommand($"SELECT Заказы.[Id заказа], Рестораны.Название, Рестораны.Адрес, Клиенты.ФИО, Клиенты.Местоположение, Заказы.Выполняется, Заказы.[Размер заказа]" +
                " FROM Клиенты INNER JOIN (Рестораны INNER JOIN Заказы ON Рестораны.Id = Заказы.[Id ресторана]) ON Клиенты.Id = Заказы.[Id заказчика] " +
                $"WHERE (((Заказы.[id курьера])={ClientNumber})) OR (((Заказы.Выполняется)=False));", dbConnection);
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
                listViewItem.SubItems.Add($"{m[6]}");
                listView1.Items.Add(listViewItem);
            }
            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("По вашему запросу ничего не найдено");
            }
            m.Close(); 
            
            dbConnection.Close();
        }

        private void AcceptOrderButton_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBase.mdb;";
                OleDbConnection dbConnection = new OleDbConnection(connectionString);
                dbConnection.Open();
                int id = int.Parse(listView1.SelectedItems[0].Text);
                OleDbCommand cmd = new OleDbCommand($"UPDATE Заказы SET Заказы.[id курьера] = {ClientNumber}, Заказы.Выполняется = True WHERE (((Заказы.[Id заказа])={id}));", dbConnection);
                cmd.ExecuteNonQuery();
                dbConnection.Close();
            }
            listView1.Items.Clear();
            Renew("wfe", false);
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache; //выбор подгрузки карты – онлайн или из ресурсов
            gMapControl1.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance; //какой провайдер карт используется (в нашем случае гугл) 
            gMapControl1.MinZoom = 2; //минимальный зум
            gMapControl1.MaxZoom = 16; //максимальный зум
            gMapControl1.Zoom = 16; // какой используется зум при открытии
            gMapControl1.Position = new GMap.NET.PointLatLng(55.9943489195581, 92.7975721657276);// точка в центре карты при открытии (центр России)
            gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter; // как приближает (просто в центр карты или по положению мыши)
            gMapControl1.CanDragMap = true; // перетаскивание карты мышью
            gMapControl1.DragButton = MouseButtons.Left; // какой кнопкой осуществляется перетаскивание
            gMapControl1.ShowCenter = false; //показывать или скрывать красный крестик в центре
            gMapControl1.ShowTileGridLines = false; //показывать или скрывать тайлы
        }

        private void RouteButton_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                markersOverlay.Clear();
                #region ПеременныеДляМеток
                string CourierAddress = "";
                double CourierLength = 0;
                double CourierWidth = 0;
                int selected = Convert.ToInt32(listView1.SelectedItems[0].Text);
                string RestaurantName = "";
                string RestaurantAddress = "";
                double RestaurantLength = 0;
                double RestaurantWidth = 0;
                string ClientAddress = "";
                double ClientLength = 0;
                double ClientWidth = 0;
                #endregion
                
                string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBase.mdb;";
                OleDbConnection dbConnection = new OleDbConnection(connectionString);
                dbConnection.Open();


                OleDbCommand cmd;
                cmd = new OleDbCommand($"SELECT Курьеры.Id, Координаты.Местоположение, Координаты.Долгота, Координаты.Широта " +
                    $"FROM Координаты INNER JOIN Курьеры ON Координаты.Местоположение = Курьеры.Местоположение " +
                    $"WHERE (((Курьеры.Id)={ClientNumber}));", dbConnection);
                var m = cmd.ExecuteReader();

                while (m.Read())
                {
                    CourierAddress += $"{m[1]}";
                    CourierLength = Double.Parse($"{m[2]}");
                    CourierWidth = Double.Parse($"{m[3]}");
                }
                m.Close();

                OleDbCommand cmd1;
                cmd1 = new OleDbCommand($"SELECT Заказы.[Id заказа], Заказы.[Id ресторана], Рестораны.Название, Рестораны.Адрес, Координаты.Долгота, Координаты.Широта " +
                    $"FROM Координаты INNER JOIN (Рестораны INNER JOIN Заказы ON Рестораны.Id = Заказы.[Id ресторана]) ON Координаты.Местоположение = Рестораны.Адрес " +
                    $"WHERE (((Заказы.[Id заказа])={selected}));", dbConnection);
                var m1= cmd1.ExecuteReader();

                while (m1.Read())
                {
                    RestaurantName += $"{m1[2]}";
                    RestaurantAddress += $"{m1[3]}";
                    RestaurantLength = Double.Parse($"{m1[4]}");
                    RestaurantWidth = Double.Parse($"{m1[5]}");
                }
                m1.Close();

                OleDbCommand cmd2;
                cmd2 = new OleDbCommand($"SELECT Заказы.[Id заказа], Заказы.[Id заказчика], Клиенты.Местоположение, Координаты.Долгота, Координаты.Широта " +
                    $"FROM Координаты INNER JOIN (Клиенты INNER JOIN Заказы ON Клиенты.Id = Заказы.[Id заказчика]) ON Координаты.Местоположение = Клиенты.Местоположение " +
                    $"WHERE (((Заказы.[Id заказа])={selected}));", dbConnection);
                var m2 = cmd2.ExecuteReader();

                while (m2.Read())
                {
                    ClientAddress += $"{m2[2]}";
                    ClientLength = Double.Parse($"{m2[3]}");
                    ClientWidth = Double.Parse($"{m2[4]}");
                }
                m2.Close();

                dbConnection.Close();

                // 2. Координаты для меток
                PointLatLng point1 = new PointLatLng(CourierWidth, CourierLength);
                PointLatLng point2 = new PointLatLng(RestaurantWidth, RestaurantLength);
                PointLatLng point3 = new PointLatLng(ClientWidth, ClientLength);


                // 3. Создаем и добавляем метки на карту
                AddMarker(point1, $"Ваше местоположение: {CourierAddress}", GMarkerGoogleType.red);
                AddMarker(point2, $"Ресторан '{RestaurantName}. Адрес: {RestaurantAddress}'", GMarkerGoogleType.green);
                AddMarker(point3, $"Адрес клиента: {ClientAddress}", GMarkerGoogleType.purple);
            }

        }
        private void AddMarker(PointLatLng point, string tooltip, GMarkerGoogleType color)
        {
            GMarkerGoogle marker = new GMarkerGoogle(point, color);
            marker.ToolTipText = tooltip;
            markersOverlay.Markers.Add(marker);
            gMapControl1.Overlays.Add(markersOverlay);
        }

    }
}
