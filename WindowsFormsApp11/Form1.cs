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
            guna2TextBox2.PasswordChar = '*';
            guna2Button1.Click += LoginEvent;
            guna2Button2.Click += RegEvent;
        }
        void LoginEvent(object s, EventArgs e)
        {
            db.ReadAll();

            foreach (User item in User.allUser)
            {
                if (guna2TextBox1.Text == item.username && guna2TextBox2.Text == item.password)
                {
                    game G = new game(item);
                    this.Hide();
                    G.Show();
                    G.FormClosing += (ss, ee) =>
                    {
                        Application.Exit();
                    };
                    break;
                }
            }

        }
        void RegEvent(object s, EventArgs e)
        {
            User user = new User();
            user.username = guna2TextBox1.Text;
            user.password = guna2TextBox2.Text;
            user.points = 0;
            db.InsertOne(user);

        }
    }
}
