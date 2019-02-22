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
        private DataPathModel DataPath = new DataPathModel();
        private List<ComponentModel> components = new List<ComponentModel>();
        private List<SignalModel> signals = new List<SignalModel>();
        private string DebugPath { get; set; }
        private string FolderPath { get; set; }
        private int ID;
       
        ////////////////////////////////////////////////////////////////

        public MainWindow()
        {
            InitializeComponent();
            //Data = new MainViewModel(DataPath);
            //this.DataContext = Data;

            ID = 1;

            //Path to the location of the executable
            DebugPath = (string)System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //Path to the location of the executable moved one folder up
            string temp = "";
            
            Uri uri = new Uri((string)System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            temp = uri.Segments[uri.Segments.Length - 1];
            FolderPath = DebugPath.Substring(0, DebugPath.Length - temp.Length - 1);
            string pathString = System.IO.Path.Combine(FolderPath, "SubFolder");
            System.IO.Directory.CreateDirectory(pathString);

            Btn_Component.IsEnabled = false;
            Btn_Signal.IsEnabled = false;
            Btn_Datapath.IsEnabled = true;
        }

        private void Btn_Datapath_Click(object sender, RoutedEventArgs e)
        {
            Window_Datapath window_Datapath = new Window_Datapath();
            
            if( window_Datapath.ShowDialog()== true)
            {
                try
                {
                    DataPath = window_Datapath.GetDataPathModel;
                   
                    Btn_Component.IsEnabled = true;
                    Btn_Signal.IsEnabled = true;
                    Btn_Datapath.IsEnabled = false;
                    
                    LoadDataTree();
                    LoadFileTree();

                    //Datapath File Generation
                    GenerateDatapath(DataPath);
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

                    LoadDataTree();
                    LoadFileTree();

                    //Datapath File Generation
                    GenerateDatapath(DataPath);
                    //Component File Generation
                    GenerateComponents(DataPath);

                    var newDP_ResultJSON = JsonConvert.SerializeObject(DataPath, Formatting.Indented);
                    System.IO.File.WriteAllText(System.IO.Path.Combine(DebugPath, "newDatapathJSON.txt"), newDP_ResultJSON);
                }
                catch (Exception) { }
            }
        }
        private void Btn_Signal_Click(object sender, RoutedEventArgs e)
        {
            Window_Signal window_Signal = new Window_Signal(JsonConvert.SerializeObject(DataPath, Formatting.Indented));
            if (window_Signal.ShowDialog() == true)
            {
                try
                {
                    //signals.Add(JsonConvert.DeserializeObject<SignalModel>(window_Signal.GetSignalJSON));
                    signals.Add(window_Signal.GetSignalModel);
                    DataPath.Signals = signals;

                    LoadDataTree();
                    LoadFileTree();

                    //Datapath File Generation
                    GenerateDatapath(DataPath);
                    //Component File Generation
                    GenerateComponents(DataPath);

                    //System.IO.File.WriteAllText(System.IO.Path.Combine(DebugPath, "SignalJSON.txt"), window_Signal.GetSignalJSON);
                    var newDP_ResultJSON = JsonConvert.SerializeObject(DataPath, Formatting.Indented);
                    System.IO.File.WriteAllText(System.IO.Path.Combine(DebugPath, "newDatapathwsJSON.txt"), newDP_ResultJSON);
                }
                catch (Exception) { }
            }
        }

        private void GenerateDatapath(DataPathModel Data)
        {
            if (Data != null && Data.Name != null)
            {
                DataPathTemplate DPTemplate = new DataPathTemplate(Data);
                String DPText = DPTemplate.TransformText();
                File.WriteAllText(DataPath.Name + ".txt", DPText);
            }
        }
        public void GenerateComponents(DataPathModel Data)
        {
            if (Data.Components != null)
            {
                foreach (ComponentModel comp in Data.Components)
                {
                    ComponentTemplate CompTemplate = new ComponentTemplate(comp);
                    String CompText = CompTemplate.TransformText();
                    File.WriteAllText(comp.Name + ".txt", CompText);
                }
            }
        }
        public void LoadDataTree()
        {
            CustomTreeView.Items.Clear();

            TreeViewData1 maintv = new TreeViewData1();

            if (DataPath.Name != null)
            {
                TreeViewData1 tv = new TreeViewData1();
                maintv.Title = DataPath.Name;
            }

            if (DataPath.Ports != null)
            {
                TreeViewData1 tv = new TreeViewData1();
                tv.Title = "Ports";
                foreach (PortModel port in DataPath.Ports)
                {
                    TreeViewData1 tv1 = new TreeViewData1();
                    tv1.Title = port.Name;
                    tv.Items.Add(tv1);
                }
                maintv.Items.Add(tv);
            }

            if (DataPath.Components != null)
            {
                TreeViewData1 tv = new TreeViewData1();
                tv.Title = "Components";
                foreach(ComponentModel comp in DataPath.Components)
                {
                    TreeViewData1 tv1 = new TreeViewData1();
                    tv1.Title = comp.Name;
                    tv.Items.Add(tv1);
                }
                maintv.Items.Add(tv);
            }

            if (DataPath.Signals != null)
            {
                TreeViewData1 tv = new TreeViewData1();
                tv.Title = "Signal";
                maintv.Items.Add(tv);
            }

            CustomTreeView.Items.Add(maintv);
        }
        public void LoadFileTree()
        {

        }
    }

    public class TreeViewData1
    {
        public TreeViewData1()
        {
            this.Items = new List<TreeViewData1>();
        }
        public string Title { get; set; }
        public List<TreeViewData1> Items { get; set; }
    }


}
