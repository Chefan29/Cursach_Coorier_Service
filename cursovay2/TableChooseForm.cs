using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cursovaya
{
    public partial class TableChooseForm : Form
    {
        AdminForm adminForm;
        public TableChooseForm(AdminForm aadminForm)
        {
            InitializeComponent();
            adminForm = aadminForm;
        }

        private void NextFormButton_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Заказы")
            {
                OrdersEditFormcs form = new OrdersEditFormcs (adminForm);
                form.Show();
                this.Close();
            }
            else if (comboBox1.Text == "Курьеры")
            {
                CouriersEditForm form = new CouriersEditForm(adminForm);
                form.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Вы не выбрали таблицу!");
            }
        }
    }
}
