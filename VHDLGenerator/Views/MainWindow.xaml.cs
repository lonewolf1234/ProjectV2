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
using VHDLGenerator.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using VHDLGenerator.Templates;
using VHDLGenerator.ViewModels;

namespace VHDLGenerator.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Main Data Produced by the windows
        /// </summary>
        /// 

        #region
        private DataPathModel DataPath = new DataPathModel();
        private List<ComponentModel> components = new List<ComponentModel>();
        private List<SignalModel> signals = new List<SignalModel>();
        private string DebugPath { get; set; }
        private string NewFolderPath { get; set; }
        private int ID;
        #endregion

        private Point startPoint;
        private Rectangle rect;
        private bool _loaded;
        ///////////////////////////////////////////////////////////////

        public MainWindow()
        {
            InitializeComponent();
            //Data = new MainViewModel(DataPath);
            //this.DataContext = Data;
            _loaded = false;
            ID = 1;

            //Path to the location of the executable
            DebugPath = (string)System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //Path to the location of the executable moved one folder up
            CreateFolder();

            Btn_Component.IsEnabled = false;
            Btn_Signal.IsEnabled = false;
            Btn_Datapath.IsEnabled = true;
        }

        #region Main Code

        private void Btn_Datapath_Click(object sender, RoutedEventArgs e)
        {
            Window_Datapath window_Datapath = new Window_Datapath();

            if (window_Datapath.ShowDialog() == true)
            {
                try
                {
                    DataPath = window_Datapath.GetDataPathModel;

                    Btn_Component.IsEnabled = true;
                    Btn_Signal.IsEnabled = true;
                    Btn_Datapath.IsEnabled = false;

                    //Datapath File Generation
                    GenerateDatapath(DataPath);

                    LoadDataTree();
                    LoadFileTree();

                    ///////////////////////////////////Drawing code
                    RenderDatapath(DataPath);
                    ////////////////////////////////////
                }
                catch (Exception) { }
            }
        }
        private void Btn_Component_Click(object sender, RoutedEventArgs e)
        {
            Window_Component window_Component = new Window_Component();
            ComponentModel model = new ComponentModel();

            if (window_Component.ShowDialog() == true)
            {
                try
                {
                    model = window_Component.GetComponentModel;
                    model.ID = ID.ToString();
                    ID++;

                    components.Add(model);
                    DataPath.Components = components;

                    //Datapath File Generation
                    GenerateDatapath(DataPath);
                    //Component File Generation
                    GenerateComponents(DataPath);

                    LoadDataTree();
                    LoadFileTree();

                    RenderComponent(DataPath);

                    var newDP_ResultJSON = JsonConvert.SerializeObject(DataPath, Formatting.Indented);
                    File.WriteAllText(System.IO.Path.Combine(NewFolderPath, "DatapathJSON.txt"), newDP_ResultJSON);
                }
                catch (Exception) { }
            }
        }
        private void Btn_Signal_Click(object sender, RoutedEventArgs e)
        {
            Window_Signal window_Signal = new Window_Signal(DataPath);
            if (window_Signal.ShowDialog() == true)
            {
                try
                {
                    //signals.Add(JsonConvert.DeserializeObject<SignalModel>(window_Signal.GetSignalJSON));
                    signals.Add(window_Signal.GetSignalModel);
                    DataPath.Signals = signals;

                    //Datapath File Generation
                    GenerateDatapath(DataPath);
                    //Component File Generation
                    //GenerateComponents(DataPath);

                    LoadDataTree();
                    LoadFileTree();

                    //System.IO.File.WriteAllText(System.IO.Path.Combine(DebugPath, "SignalJSON.txt"), window_Signal.GetSignalJSON);
                    var newDP_ResultJSON = JsonConvert.SerializeObject(DataPath, Formatting.Indented);
                    //System.IO.File.WriteAllText(System.IO.Path.Combine(DebugPath, "newDatapathwsJSON.txt"), newDP_ResultJSON);
                    File.WriteAllText(System.IO.Path.Combine(NewFolderPath, "DatapathJSON.txt"), newDP_ResultJSON);
                }
                catch (Exception) { }
            }
        }

        private void GenerateDatapath(DataPathModel Data)
        {
            try
            {
                if (Data != null && Data.Name != null)
                {
                    DataPathTemplate DPTemplate = new DataPathTemplate(Data);
                    String DPText = DPTemplate.TransformText();

                    //string textpath = Data.Name + ".txt";
                    //string newpath = System.IO.Path.Combine(NewFolderPath, textpath);
                    File.WriteAllText(System.IO.Path.Combine(NewFolderPath, Data.Name + ".txt"), DPText);

                    //File.WriteAllText(Data.Name + ".txt", DPText);
                }
            }
            catch (Exception) { }
        }
        public void GenerateComponents(DataPathModel Data)
        {
            if (Data.Components != null)
            {
                foreach (ComponentModel comp in Data.Components)
                {
                    ComponentTemplate CompTemplate = new ComponentTemplate(comp);
                    String CompText = CompTemplate.TransformText();
                    //File.WriteAllText(comp.Name + ".txt", CompText);
                    File.WriteAllText(System.IO.Path.Combine(NewFolderPath, comp.Name + ".txt"), CompText);
                }
            }
        }
        public void LoadDataTree()
        {
            CustomTreeView.Items.Clear();

            TreeViewData maintv = new TreeViewData();

            if (DataPath.Name != null)
            {
                TreeViewData tv = new TreeViewData();
                maintv.Title = DataPath.Name;
            }

            if (DataPath.Ports != null)
            {
                TreeViewData tv = new TreeViewData();
                tv.Title = "Ports";
                foreach (PortModel port in DataPath.Ports)
                {
                    TreeViewData tv1 = new TreeViewData();
                    tv1.Title = port.Name;
                    tv.Items.Add(tv1);
                }
                maintv.Items.Add(tv);
            }

            if (DataPath.Components != null)
            {
                TreeViewData tv = new TreeViewData();
                tv.Title = "Components";
                foreach (ComponentModel comp in DataPath.Components)
                {
                    TreeViewData tv1 = new TreeViewData();
                    tv1.Title = comp.Name;
                    tv.Items.Add(tv1);
                }
                maintv.Items.Add(tv);
            }

            if (DataPath.Signals != null)
            {
                TreeViewData tv = new TreeViewData();
                tv.Title = "Signal";
                maintv.Items.Add(tv);
            }

            CustomTreeView.Items.Add(maintv);
        }
        public void LoadFileTree()
        {
            CodeTreeView.Items.Clear();

            List<string> FileNames = new List<string>(Directory.GetFiles(NewFolderPath));

            foreach (string Path in FileNames)
            {
                string FileName = "";
                Uri uri = new Uri(Path);
                FileName = uri.Segments[uri.Segments.Length - 1];
                TreeViewData tv = new TreeViewData();
                tv.Title = FileName;
                CodeTreeView.Items.Add(tv);
            }

        }
        public void CreateFolder()
        {
            string temp = "";
            string FolderPath = "";
            Uri uri = new Uri((string)System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            temp = uri.Segments[uri.Segments.Length - 1];
            FolderPath = DebugPath.Substring(0, DebugPath.Length - temp.Length - 1);
            string pathString = System.IO.Path.Combine(FolderPath, "GeneratedCode");
            NewFolderPath = pathString;
            //checks to see if the folder "GenerateCode exists"
            if (Directory.Exists(pathString))
            {
                //delets folder and creates a new empty one
                Directory.Delete(pathString, true);
                Directory.CreateDirectory(pathString);
            }
            else
            {
                //creates a new folder if it doesnot exist
                Directory.CreateDirectory(pathString);
            }
        }

        private void Btn_OpenFile_Click(object sender, RoutedEventArgs e)
        {
            if (CodeTreeView.SelectedItem != null)
            {
                var item = CodeTreeView.SelectedItem as TreeViewData;
                var itempath = System.IO.Path.Combine(NewFolderPath, item.Title);
                Process.Start(itempath);
            }

            //MessageBox.Show(item.Title);
        }

        #endregion

        #region
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _loaded = true;
        }

        private void RenderDatapath(DataPathModel _data)
        {

            if (_data.Name != null)
            {
                Rectangle dprect = new Rectangle
                {
                    Stroke = Brushes.Blue,
                    StrokeThickness = 1,
                    Height = canvas.ActualHeight - 100,
                    Width = canvas.ActualWidth - 100
                };

                Point spoint = new Point(50, 50);
                Canvas.SetLeft(dprect, spoint.X);
                Canvas.SetTop(dprect, spoint.Y);
                canvas.Children.Add(dprect);

                if (_data.Ports != null)
                {
                    int incount = 1;
                    int outcount = 1;
                    
                    foreach (PortModel port in _data.Ports)
                    {
                        TextBlock textBlock = new TextBlock() { Text = port.Name, FontSize = 10 };
                        Point point = new Point();
                        if (port.Direction == "in")
                        {
                            point.X = spoint.X + 5;
                            point.Y = spoint.Y + (incount * 10);
                            incount++;
                        }
                        else
                        {

                            point.X = spoint.X + dprect.Width - (textBlock.Text.Length * 5) - 5;
                            point.Y = spoint.Y + (outcount * 10) ;
                            outcount++;
                        }
                        Canvas.SetLeft(textBlock, point.X);
                        Canvas.SetTop(textBlock, point.Y);
                        canvas.Children.Add(textBlock);
                    }
                }
            }
        }

        private void RenderComponent(DataPathModel _data)
        {
            if (_data.Components != null)
            {
                Point point = new Point(50, 50);
                Point StartPoint = new Point() { X = 100, Y = 100 };
                int count1 = 0;
                int count2 = 0;
                int height = 0;

                foreach( PortModel port in _data.Components.Last().Ports)
                {
                    if (port.Direction == "in")
                        count1++;
                    else
                        count2++;
                }

                if (count1 > count2)
                    height = count1;
                else
                    height = count2;

                Rectangle rectComp = new Rectangle()
                {
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                    Height = (height * 10) + 20,
                    Width = 100
                };
                Canvas.SetLeft(rectComp, StartPoint.X);
                Canvas.SetTop(rectComp, StartPoint.Y);
                canvas.Children.Add(rectComp);

                RenderPortText(_data.Components.Last().Ports, StartPoint);
            }
        }

        private void RenderPortText(List<PortModel> _data, Point _point)
        {
            int incount = 1;
            int outcount = 1;
            foreach (PortModel port in _data)
            {
                TextBlock textBlock = new TextBlock() { Text = port.Name, FontSize = 10 };
                Point point = new Point();
                if (port.Direction == "in")
                {
                    point.X = _point.X + 5;
                    point.Y = _point.Y + (incount * 10) ;
                    incount++;
                }
                else
                {
                    point.X = _point.X + rect.Width - (textBlock.Text.Length * 5) - 5;
                    point.Y = _point.Y + (outcount * 10) ;
                    outcount++;
                }
                Canvas.SetLeft(textBlock, point.X);
                Canvas.SetTop(textBlock, point.Y);
                canvas.Children.Add(textBlock);
            }
            #endregion
        }

        public class TreeViewData
        {
            public TreeViewData()
            {
                this.Items = new List<TreeViewData>();
            }
            public string Title { get; set; }
            public List<TreeViewData> Items { get; set; }
        }
    }


}
