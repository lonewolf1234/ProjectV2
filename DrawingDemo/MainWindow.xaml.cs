using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DrawingDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Point startPoint;
        private Rectangle rect;
        private Rectangle myrect;

        public MainWindow()
        {
            InitializeComponent();
        }

        double newheight;
        double newwidth;
       
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            newheight = canvas.ActualHeight;
            newwidth = canvas.ActualWidth;
            //DrawRect();
            Drawrect1();

        }

        public void Drawrect1()
        {
            Rectangle rectangle = new Rectangle();
            
        }

        #region
        //private void DrawRect()
        //{
        //    //var newheight = canvas.ActualHeight ;
        //    //var newwidth = canvas.ActualWidth ;

        //    #region Rectangle Creation
        //    myrect = new Rectangle
        //    {
        //        //Stroke = Brushes.Blue,
        //        //StrokeThickness = 2,
        //        Height = (int)newheight - 50,
        //        Width = (int)newwidth - 150,
        //    };

        //    Canvas.SetLeft(myrect, 75);
        //    Canvas.SetTop(myrect, 25);
        //    canvas.Children.Add(myrect);
        //    #endregion

        //    #region Text creation
        //    TextBlock text = new TextBlock();
        //    text.Text = "Port 1";

        //    var x = 78;
        //    var y = 30;
        //    Canvas.SetLeft(text, x);
        //    Canvas.SetTop(text, y);
        //    canvas.Children.Add(text);
        //    #endregion

        //    #region Text creation
        //    TextBlock text1 = new TextBlock();
        //    text1.Text = "Port 2";

        //    var x1 = 78;
        //    var y1 = 50;
        //    //Point point2 = new Point(x1, y1);
        //    Canvas.SetLeft(text1, x1);
        //    Canvas.SetTop(text1, y1);
        //    canvas.Children.Add(text1);
        //    #endregion

        //    #region Text creation
        //    TextBlock text2 = new TextBlock();
        //    text2.Text = "Port 3";

        //    var x2 = 78;
        //    var y2 = 30;
        //    //Point point3 = new Point(x2,y2);
        //    Canvas.SetRight(text2, x2);
        //    Canvas.SetTop(text2, y2);
        //    canvas.Children.Add(text2);
        //    #endregion


        //}
        #endregion

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(canvas);

            rect = new Rectangle
            {
                Stroke = Brushes.LightBlue,
                StrokeThickness = 2
            };
            Canvas.SetLeft(rect, startPoint.X);
            Canvas.SetTop(rect, startPoint.Y);
            canvas.Children.Add(rect);
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released || rect == null)
                return;

            var pos = e.GetPosition(canvas);

            var x = Math.Min(pos.X, startPoint.X);
            var y = Math.Min(pos.Y, startPoint.Y);

            var w = Math.Max(pos.X, startPoint.X) - x;
            var h = Math.Max(pos.Y, startPoint.Y) - y;

            rect.Width = w;
            rect.Height = h;

            Canvas.SetLeft(rect, x);
            Canvas.SetTop(rect, y);
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            rect = null;
        }
    }
}
