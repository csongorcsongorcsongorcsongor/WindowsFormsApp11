using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp11
{
    public partial class Form1 : Form
    {
        dbHandler db;
        public Form1()
        {
            InitializeComponent();
            start();
        }
        void start()
        {
            db = new dbHandler();
            db.ReadAll();
            guna2TextBox2.PasswordChar = '*';
            guna2Button1.Click += LoginEvent;
            guna2Button2.Click += RegEvent;
        }
        void LoginEvent(object s, EventArgs e)
        {
            foreach (User item in User.allUser)
            {
                if (guna2TextBox1.Text == item.username && guna2TextBox2.Text == item.password)
                {
                    MessageBox.Show("Sikeres bejelentkezés");
                    break;
                }
            }
        }
        void RegEvent(object s, EventArgs e)
        {

        }
    }
}
