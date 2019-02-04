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
        DataPathModel DataPath = new DataPathModel();

        List<ComponentModel> components = new List<ComponentModel>();

        List<SignalModel> signals = new List<SignalModel>();

        public string DebugPath { get; set; }

        ////////////////////////////////////////////////////////////////

        public MainWindow()
        {
            InitializeComponent();

            DebugPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        }

        private void Btn_Datapath_Click(object sender, RoutedEventArgs e)
        {
            TextBlock_test.Text = "Create Datapath Selected";

            Window_Datapath window_Datapath = new Window_Datapath();
            
            if( window_Datapath.ShowDialog()== true)
            {
                #region
                //var InputJSON = window_Datapath.DP_ResultJSON;
                //DataPath = JsonConvert.DeserializeObject<DataPathModel>(InputJSON);

                //Debug.WriteLine(InputJSON);

                //System.IO.File.WriteAllText(System.IO.Path.Combine(DebugPath,"DatapathJSON.txt"), InputJSON);
                #endregion
                try
                {
                    DataPath = JsonConvert.DeserializeObject<DataPathModel>(window_Datapath.GetDataPJSON);
                    System.IO.File.WriteAllText(System.IO.Path.Combine(DebugPath,"DatapathJSON.txt"), window_Datapath.GetDataPJSON);
                }
                catch (Exception) { }

            }
        }

        private void Btn_Component_Click(object sender, RoutedEventArgs e)
        {
            TextBlock_test.Text = "Create Component Selected";
            Window_Component window_Component = new Window_Component();

            if (window_Component.ShowDialog() == true)
            {
                #region Old code using JSON format to transfer data
                //var InputJSON = window_Component.Comp_ResultJSON;

                //ComponentModel tempComponent = new ComponentModel();
                //tempComponent = JsonConvert.DeserializeObject<ComponentModel>(InputJSON);
                //components.Add(tempComponent);
                //DataPath.Components = components;

                //Degugging Stuff for component created
                //Debug.WriteLine(InputJSON);
                //System.IO.File.WriteAllText(System.IO.Path.Combine(DebugPath, "ComponentJSON.txt"), InputJSON);

                ////Degugging stuff for the updated Datapath object with the addition of the component
                //var newDP_ResultJSON = JsonConvert.SerializeObject(DataPath, Formatting.Indented);
                //System.IO.File.WriteAllText(System.IO.Path.Combine(DebugPath, "newDatapathJSON.txt"), newDP_ResultJSON);
                #endregion
                try
                {
                    components.Add(JsonConvert.DeserializeObject<ComponentModel>(window_Component.GetCompJSON));
                    DataPath.Components = components;

                    System.IO.File.WriteAllText(System.IO.Path.Combine(DebugPath, "ComponentJSON.txt"), window_Component.GetCompJSON);
                    var newDP_ResultJSON = JsonConvert.SerializeObject(DataPath, Formatting.Indented);
                    System.IO.File.WriteAllText(System.IO.Path.Combine(DebugPath, "newDatapathJSON.txt"), newDP_ResultJSON);
                }
                catch (Exception) { }
            }
        }

        private void Btn_Signal_Click(object sender, RoutedEventArgs e)
        {
            TextBlock_test.Text = "Create Signal Selected";

            Window_Signal window_Signal = new Window_Signal(JsonConvert.SerializeObject(DataPath, Formatting.Indented));

            if (window_Signal.ShowDialog() == true)
            {
                #region Old code using JSON format to transfer data
                //var InputJSON = window_Component.Comp_ResultJSON;

                //ComponentModel tempComponent = new ComponentModel();
                //tempComponent = JsonConvert.DeserializeObject<ComponentModel>(InputJSON);
                //components.Add(tempComponent);
                //DataPath.Components = components;

                //Degugging Stuff for component created
                //Debug.WriteLine(InputJSON);
                //System.IO.File.WriteAllText(System.IO.Path.Combine(DebugPath, "ComponentJSON.txt"), InputJSON);

                ////Degugging stuff for the updated Datapath object with the addition of the component
                //var newDP_ResultJSON = JsonConvert.SerializeObject(DataPath, Formatting.Indented);
                //System.IO.File.WriteAllText(System.IO.Path.Combine(DebugPath, "newDatapathJSON.txt"), newDP_ResultJSON);
                #endregion
                try
                {
                    signals.Add(JsonConvert.DeserializeObject<SignalModel>(window_Signal.GetSignalJSON));
                    DataPath.Components = components;

                    System.IO.File.WriteAllText(System.IO.Path.Combine(DebugPath, "SignalJSON.txt"), window_Signal.GetSignalJSON);
                    var newDP_ResultJSON = JsonConvert.SerializeObject(DataPath, Formatting.Indented);
                    System.IO.File.WriteAllText(System.IO.Path.Combine(DebugPath, "newDatapathw/sJSON.txt"), newDP_ResultJSON);
                }
                catch (Exception) { }
            }
        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            //GenerateCode(DataPath);
            DataPathTemplate DPTemplate = new DataPathTemplate(DataPath);
            String s = DPTemplate.TransformText();
            File.WriteAllText("GeneratedCode.txt", s);

            TextBlock_test.Text = "Code Generated";
        }

        private void GenerateCode(DataPathModel Data)
        {
            List<string> port = new List<string>();

            if(DataPath != null)
            {
                //Library Code
                string[] Libraries_txt =
                {
                "library IEEE;",
                "use IEEE.STD_LOGIC_1164.ALL;",
                "use IEEE.STD_LOGIC_ARITH.ALL;",
                "use IEEE.STD_LOGIC_UNSIGNED.ALL;",
                "",
            };

                //Entity Begin code
                string EntityBegin_txt = $"entity {DataPath.Name} is";

                //Port Code
                List<string> ports_txt = new List<string>();

                if (DataPath.Ports.Count > 0)
                {

                    foreach (PortModel p in DataPath.Ports)
                    {
                        string vector = "";

                        if (p.Bus == true)
                        {
                            vector = $"_vector({p.MSB} downto {p.LSB})";
                        }

                        string s = $"\t{p.Name} : {p.Direction} std_logic{vector}";


                        if (DataPath.Ports.First() == p)
                        {
                            ports_txt.Add("\tPort(" + s + ";");
                        }
                        else if (DataPath.Ports.Last() == p)
                        {
                            ports_txt.Add("\t" + s + ");");
                        }
                        else
                        {
                            ports_txt.Add("\t" + s + ";");
                        }
                    }
                }
                else
                {
                    ports_txt.Add("\tPort(\n);");
                }

                //Entity End Code
                string EntityEnd_txt = $"end {DataPath.Name};";

                //Behavioral Begin code
                string BehavioralBegin_txt = $"architecture {DataPath.ArchName} of {DataPath.Name} is";

                string Begin_txt = "\nbegin";

                //Component Code


                //Behavioral End Code
                string BehavioralEnd_txt = $"\nend {DataPath.ArchName};";


                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



                using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine(DebugPath, "VHDLFile.txt")))

                {
                    //outputFile.WriteLine(DebugPath);

                    //libraries
                    foreach (string line in Libraries_txt)
                        outputFile.WriteLine(line);

                    //Entity begin
                    outputFile.WriteLine(EntityBegin_txt);

                    //Ports
                    foreach (string line in ports_txt)
                        outputFile.WriteLine(line);

                    //Entity End
                    outputFile.WriteLine(EntityEnd_txt);
                    outputFile.WriteLine("");

                    //Behaviourial Begin
                    outputFile.WriteLine(BehavioralBegin_txt);
                    outputFile.WriteLine(Begin_txt);
                    outputFile.WriteLine("");
                    outputFile.WriteLine("");

                    //Behaviourial End
                    outputFile.WriteLine(BehavioralEnd_txt);
                }
            }

           
        }
        
    }
}
