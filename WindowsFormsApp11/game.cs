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
        Timer carMovement = new Timer();
        Timer TrainCountDown = new Timer();
        Timer trainattack = new Timer();
        PictureBox gate = new PictureBox();
        public bool IsOpened = false;

        public PictureBox train = new PictureBox();


        Label trainCountdownLabel = new Label();
        int countdownTime = 0;

        dbHandler db;

        public User loggedInUser { get; set; }
        public game(User loggedInUser)
        {
            db = new dbHandler();
            this.loggedInUser = loggedInUser;
            InitializeComponent();
            start();
        }
        void start()
        {
            label1.Text = $"Üdv {loggedInUser.username}! \n Pontjaid száma: {loggedInUser.points}.";
            label1.AutoSize = true;

           
            trainCountdownLabel.Location = new Point(20, 50); 
            trainCountdownLabel.AutoSize = true;
            Controls.Add(trainCountdownLabel); 

            creategate();
            createshitcar();
            carMovement.Interval = 25;
            carMovement.Tick += MoveAllCars; 
            carMovement.Start();
            trainshit();
        }
        void MoveAllCars(object sender, EventArgs e)
        {
            if (cars.Count != 0)
            {
                for (int i = 0; i < cars.Count; i++)
                {
                    
                    if (IsOpened)
                    {
                        cars[i].Left+=2;
                    }
                    else
                    {
                        
                        if (cars[i].Location.X >= 260)
                        {
                            cars[i].Left+=2;
                        }
                    } 
                    if (cars[i].Bounds.IntersectsWith(train.Bounds))
                    {
                        stopalltimers();
                        MessageBox.Show("baleset", "Rip");
                        break; 
                    }
                  
                    if (cars[i].Left == 24 && i == cars.Count - 1)
                    {
                        createshitcar();
                    }
                    if (cars[i].Left >= this.Width)
                    {
                        cars.Remove(cars[i]);
                        this.Controls.Remove(cars[i]);

                        loggedInUser.points++;
                        updpoints(loggedInUser);
                        label1.Text = $"Üdv {loggedInUser.username}! \n Pontjaid száma: {loggedInUser.points}.";

                    }
                }
            }
        }
        void stopalltimers()
        {
            trainattack.Stop();
            TrainCountDown.Stop();
            carMovement.Stop();
        }
        void creategate()
        {   
            gate.Height = 150;
            gate.Width = 15;
            gate.BackColor = Color.Black;
            Controls.Add(gate);
            gate.Location = new Point(300, ((this.Height - gate.Height) / 2) - 150);
            IsOpened = true;


            guna2Button1.Click += (s, e) =>

            {
                if (IsOpened == false)
                {
                    gate.Location = new Point(300, ((this.Height - gate.Height) / 2) - 150);
                    IsOpened = true; 
                    guna2Button1.Text = $"Close Gate";
                }
                else if (IsOpened == true)
                {
                    gate.Location = new Point(300, (this.Height - gate.Height) / 2);
                    IsOpened = false; 
                    guna2Button1.Text = $"Open Gate";
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
            train.Image = Image.FromFile("hev.png");
            train.SizeMode = PictureBoxSizeMode.StretchImage;
            Controls.Add(train);
            train.Location = new Point((this.Width - train.Width) / 2, this.Top - train.Height);
            log($"{train.Location.X}");
           
            countdownTime = r.Next(10, 30); 
            trainCountdownLabel.Text = $"Vonat érkezik: {countdownTime} másodperc."; 

            TrainCountDown.Interval = 1000; 
            TrainCountDown.Tick += (s, e) =>
            {
                countdownTime--; 

                if (countdownTime <= 0) 
                {
                    trainattack.Start(); 
                }

                trainCountdownLabel.Text = $"Vonat érkezik: {countdownTime} másodperc."; 
            };

            TrainCountDown.Start(); 

            trainattack.Interval = 10;
            trainattack.Tick += (s, e) =>
            {
                train.Top += 4; 

              
                if (train.Top > this.Height + 45)
                {
                    trainattack.Stop(); 
                    train.Top = -200; 
                    log($"{train.Location.Y}");
                    countdownTime = r.Next(10, 30);
                    trainCountdownLabel.Text = $"Vonat érkezik: {countdownTime} másodperc.";
                }
            };
        }
        void updpoints(User userr)
        {

            db.UpdateOne(userr);
        }

        void createshitcar()
        {
            PictureBox oneCar = new PictureBox();
            oneCar.Width = 80;  
            oneCar.Height = 50;
            oneCar.BackColor = Color.Red;
            Controls.Add(oneCar);
            cars.Add(oneCar);

         
            oneCar.Location = new Point(-oneCar.Width, (this.Height - oneCar.Height) / 2);

        }
        void log(string message)
        {
            listBox1.Items.Add($"{message} . {DateTime.Now}");
        }

    }
}
