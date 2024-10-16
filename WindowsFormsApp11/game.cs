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
        Random r = new Random();

        public List<PictureBox> cars = new List<PictureBox>();
        Timer carGenerate = new Timer();
        Timer carMovement = new Timer();
        Timer TrainCountDown = new Timer();
        Timer trainattack = new Timer();
        PictureBox gate = new PictureBox();
        public bool IsOpened = true;

        public PictureBox train = new PictureBox();


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

            createshitcar();
            trainshit();
            creategate();
        }
        void creategate()
        {   
            gate.Height = 150;
            gate.Width = 20;
            gate.BackColor = Color.Black;
            Controls.Add(gate);
            gate.Location = new Point(200, (this.Height - gate.Height) / 2);
            IsOpened = false;
            guna2Button1.Click += (s, e) =>
            {
                if (IsOpened == false)
                {
                    gate.Location = new Point(200, (this.Height - gate.Height) / 2);
                    IsOpened = true;
                    guna2Button1.Text = $"Open Gate";
                }
                else if(IsOpened == true)
                {
                    gate.Location = new Point(200, ((this.Height - gate.Height) / 2)-150);
                    IsOpened = false;
                    guna2Button1.Text = $"Close Gate";
                }
                else
                {
                }
            };
        }
        void trainshit()
        {
            train.Height = 200;
            train.Width = 105;
            train.BackColor = Color.DarkRed;
            Controls.Add(train);
            train.Location = new Point((this.Width - train.Width) / 2, this.Top - train.Height);
            TrainCountDown.Interval = r.Next(10000, 30000);

            TrainCountDown.Tick += (s, e) =>
            {
                TrainCountDown.Interval = r.Next(10000, 30000);
                trainattack.Start();
            };
            TrainCountDown.Start();

            trainattack.Interval = 10;
            trainattack.Tick += (s, e) =>
            {
                if (train.Top > this.Height)
                {
                    trainattack.Stop();
                    train.Location = new Point((this.Width - train.Width) / 2, this.Top - train.Height);

                }
                train.Top += 4;

            };
        }
        void createshitcar()
        {
            PictureBox oneCar = new PictureBox();
            oneCar.Width = 80;
            oneCar.Height = 50;
            oneCar.BackColor = Color.Red;
            Controls.Add(oneCar);
            cars.Add(oneCar);
            oneCar.Location = new Point(this.Left - oneCar.Width, (this.Height - oneCar.Height) / 2);
            movecar(oneCar);
            
        }
        void movecar(PictureBox car)
        {
            carMovement.Interval = 25;
            carMovement.Tick += (s, e) =>
            {
                if (cars.Count != 0)
                {
                    for (int i = 0; i < cars.Count; i++)
                    {
                        if (!IsOpened)
                        {
                            cars[i].Left++;
                            
                        }
                        else
                        {
                            if (cars[i].Location.X >= 190)
                            {
                                cars[i].Left++;
                            }
                        }
                        if (cars[i].Location.X == 20)
                        {
                            createshitcar();
                        }

                    }
                }
            };
            carMovement.Start();

        }

    }
}
