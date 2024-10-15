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
    public partial class game : Form
    {
        public User loggedInUser { get; set; }
        public game(User loggedInUser)
        {
            this.loggedInUser = loggedInUser;
            InitializeComponent();
            start();
        }
        void start()
        {
            label1.Text = $"Üdv {loggedInUser.username}! \n Pontjaid száma: {loggedInUser.points}.";
            label1.AutoSize = true;
        }
    }
}
