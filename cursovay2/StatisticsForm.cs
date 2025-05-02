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
    public partial class StatisticsForm : Form
    {
        public StatisticsForm()
        {
            InitializeComponent();
            Renew();
            ListsViewsInitialize();
        }
        public void ListsViewsInitialize()
        {
            listView1.Columns.Add("Id курьера");
            listView1.Columns.Add("Кол-во заказов");
            listView1.Columns.Add("ФИО Курьера");
            listView1.Columns.Add("Местоположение");
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            listView2.Columns.Add("Id ресторана");
            listView2.Columns.Add("Кол-во заказов");
            listView2.Columns.Add("Название");
            listView2.Columns.Add("Адрес");
            listView2.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }/*Инициализация листвью*/
        public void Renew()
        {
            listView1.Items.Clear();
            listView2.Items.Clear();


            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBase.mdb;";
            OleDbConnection dbConnection = new OleDbConnection(connectionString);
            dbConnection.Open();

            #region Обновление таблицы самых трудоспособных курьеров
            OleDbCommand cmd = new OleDbCommand($"SELECT Заказы.[id курьера], Count(*) AS [Сколько заказов], Курьеры.ФИО, Курьеры.Местоположение " +
                    $"FROM Курьеры INNER JOIN Заказы ON Курьеры.Id = Заказы.[id курьера] " +
                    $"GROUP BY Заказы.[id курьера], Курьеры.ФИО, Курьеры.Местоположение " +
                    $"ORDER BY Count(*) DESC;", dbConnection);


            var m = cmd.ExecuteReader();

            while (m.Read())
            {
                ListViewItem listViewItem = new ListViewItem($"{m[0]}");
                listViewItem.SubItems.Add($"{m[1]}");
                listViewItem.SubItems.Add($"{m[2]}");
                listViewItem.SubItems.Add($"{m[3]}");
                listView1.Items.Add(listViewItem);
            }

            m.Close();
            #endregion

            #region Обновление таблицы самых популярных ресторанов
            OleDbCommand cmd1 = new OleDbCommand("SELECT Заказы.[Id ресторана], Count(*) AS [Сколько раз заказали], Рестораны.Название, Рестораны.Адрес " +
                "FROM Рестораны INNER JOIN Заказы ON Рестораны.Id = Заказы.[Id ресторана] " +
                "GROUP BY Заказы.[Id ресторана], Рестораны.Название, Рестораны.Адрес " +
                "ORDER BY Count(*) DESC;", dbConnection);

            var m1 = cmd1.ExecuteReader();

            while (m1.Read())
            {
                    ListViewItem listViewItem = new ListViewItem($"{m1[0]}");
                    listViewItem.SubItems.Add($"{m1[1]}");
                    listViewItem.SubItems.Add($"{m1[2]}");
                    listViewItem.SubItems.Add($"{m1[3]}");
                    listView2.Items.Add(listViewItem);
            }
            #endregion
            dbConnection.Close();
        }

        private void CloseFormButton_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
    }
    }
