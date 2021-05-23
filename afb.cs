using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Drawing;
using System.Windows.Controls;

namespace eindwerk_ontwikkelingomgeving
{
    public class Circle
    {
        public double _x;
        public double _y;
        public double _speedX;
        public double _speedY;

        public double X
        {
            get
            {
                return _x;
            }
            set
            {
                if (value > 800)
                {
                    _x = -50;
                }
                else if (value < -50)
                {
                    _x = 800;
                }
                else
                {
                    _x = value;
                }
                Canvas.SetLeft(Ellps, _x);
            }
        }
        public double Y
        {
            get
            {
                return _y;
            }
            set
            {
                if (value > 365)
                {
                    _y = -50;
                }
                else if (value < -50)
                {
                    _y = 365;
                }
                else
                {
                    _y = value;
                }
                Canvas.SetTop(Ellps, _y);
            }
        }
        public Ellipse Ellps { get; set; }

        public Circle(string imagepath)
        {
            BitmapImage image = new BitmapImage(new Uri(@imagepath, UriKind.Relative));
            ImageBrush tbrush = new ImageBrush(image);

            Ellps = new Ellipse();
            Ellps.Width = 50;
            Ellps.Height = 50;
            Random rand = new Random();
            X = rand.Next(0, 750);
            Y = rand.Next(0, 335);

            _speedX = rand.Next(-4, 4);
            _speedY = rand.Next(-4, 4);

            Ellps.Fill = tbrush;
            if (imagepath == "images\\baland.jpg")
            {
                Ellps.Tag = "fouten bal.";
            }
            else if (imagepath == "images\\balcb.jpg")
            {
                Ellps.Tag = "";
            }
        }

        public void Move()
        {
            X += _speedX;
            Random rand = new Random();
            Y += _speedY;
        }
        public void AddToCanvas(Canvas c)
        {
            c.Children.Add(Ellps);
        }
        public void remove(Canvas c)
        {
            c.Children.Remove(Ellps);
        }
    }
}

