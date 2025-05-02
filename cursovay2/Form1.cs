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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ClientChooseButton_Click(object sender, EventArgs e)
        {
            CourierChooseForm form = new CourierChooseForm();
            form.Show();
            //ClientForm form = new ClientForm();
            //form.Show();
        }

        private void AdminChooseButton_Click(object sender, EventArgs e)
        {
            AdminVerificationForm form = new AdminVerificationForm();  
            form.Show();
        }
    }
}
