using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHDLGenerator.Models;

namespace VHDLGenerator.Templates
{
    public partial class DataPathTemplate
    {

        public DataPathTemplate(DataPathModel data)
        {
            this.Name = data.Name;
            this.ArchName = data.ArchName;
            this.Ports = data.Ports;
            this.Components = data.Components;
            this.Signals = data.Signals;
        }

        public string Name { get; set; }

        public string ArchName { get; set; }

        public List<PortModel> Ports { get; set; }

        public List<ComponentModel> Components { get; set; }

        public List<SignalModel> Signals { get; set; }

        public List<string> MainPortsData
        {
            get{return PortTranslation(this.Ports);}
        }

        public List<string> SignalData
        {
            get{ return SignalTranslation(this.Signals); }
        }

        private List<string> PortTranslation(List<PortModel> ports)
        {
            List<string> templist = new List<string>();

            if (ports.Count != 0)
            {
                foreach (PortModel port in ports)
                {
                    string temp = "";
                    if (port.Bus == true)
                    {
                        temp = $"{port.Name} : {port.Direction} STD_LOGIC_VECTOR({port.MSB} downto {port.LSB})";
                    }
                    else
                    {
                        temp = $"{port.Name} : {port.Direction} STD_LOGIC";
                    }

                    if (ports.First() == port)
                    {
                        templist.Add(temp + ";");
                    }
                    else if(ports.Last() == port)
                    {
                        templist.Add("\t" + temp + ");");
                    }
                    else
                    {
                        templist.Add("\t" +temp + ";");
                    }
                }
            }

            return templist;
        }

        private List<string> SignalTranslation(List<SignalModel> signals)
        {
            List<string> templist = new List<string>();

            if(signals.Count != 0)
            {
                foreach(SignalModel sig in signals)
                {
                    string tempsig = string.Empty;
                    if (sig.Bus == true)
                    {
                        tempsig = $"signal {sig.Name} : STD_LOGIC_VECTOR({sig.MSB} downto {sig.LSB});";
                    }
                    else
                    {
                        tempsig = $"signal {sig.Name} : STD_LOGIC;";
                    }
                    templist.Add(tempsig);
                }
            }

            return templist;
        }
    }
}
