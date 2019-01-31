using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHDLGenerator.Models;
using Newtonsoft.Json;
using System.ComponentModel;

namespace VHDLGenerator.DataModels
{
    class SignalData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private SignalModel Signal = new SignalModel();
        private DataPathModel _Datapath = new DataPathModel();
        private List<string> SPorts = new List<string>();
        private List<string> TPorts = new List<string>();


        public SignalData(string datapath)
        {
           _Datapath = JsonConvert.DeserializeObject<DataPathModel>(datapath);
        }

        public string SigEntityNameTxt
        {
            get { return Signal.Name; }
            set { Signal.Name = value; }
        }
        public bool SigBusCB
        {
            get { return Signal.Bus;}
            set { Signal.Bus = value;}
        }
        public string MsbTxt
        {
            get {return Signal.MSB; }
            set {Signal.MSB = value; }
        }
        public string LsbTxt
        {
            get { return Signal.LSB; }
            set { Signal.LSB = value; }
        }
        //public bool prop { get; set; }

        //public bool PropChanged
        //{
        //    get { return this.prop; }
        //    set
        //    {
        //        this.prop = value;
        //        if(value == true)
        //        {
        //            SCompPorts = GetPortNames(SCompName);
        //            TCompPorts = GetPortNames(TCompName);
        //            this.prop = false;
        //        }
        //    }
        //}

        //item source - components
        //use as source for the combobox items
        public List<string> ComponentNames
        {
            get
            {
                return GetCompName();
            }
        }

        //item selected
        //for selected item in source cat
        public string SCompName
        {
            get { return Signal.Source_Comp; }
            set
            {
                this.Signal.Source_Comp = value;

                this.SPorts = GetPortNames(this.Signal.Source_Comp);
                OnPropertyChanged("SCompPorts");
            }
        }
        //for selected item in traget cat
        public string TCompName
        {
            get { return Signal.Target_Comp; }
            set
            {
                this.Signal.Target_Comp = value;
                this.TPorts = GetPortNames(this.Signal.Target_Comp);
                OnPropertyChanged("TCompPorts");
            }
        }


        //item source - source ports
        public List<string> SCompPorts
        {
            get
            {
                return this.SPorts;
            }
        }

        //item source - target ports
        public List<string> TCompPorts
        {
            get
            {
                return this.TPorts;
            }
        }

        //for selected iten in source cat - port
        public string SCompPortName
        {
            get { return Signal.Source_port; }
            set { this.Signal.Source_port = value; }
        }
        //for selected item in traget cat - port
        public string TCompPortName
        {
            get { return Signal.Target_port; }
            set { this.Signal.Target_port = value; }
        }


        private List<string> GetCompName()
        {
            List<string> names = new List<string>();
            try
            {
                foreach(ComponentModel comp in _Datapath.Components)
                {
                    names.Add(comp.Name);
                }
            }
            catch(Exception) { }
            
            return names;
        }

        private List<string> GetPortNames(string selectedComponent)
        {
            List<string> names = new List<string>();

            try
            {
                foreach (ComponentModel comp in _Datapath.Components)
                {
                    if (comp.Name == selectedComponent)
                    {
                        foreach (PortModel port in comp.Ports)
                        {
                            names.Add(port.Name);
                        }
                    }
                }
            }
            catch (Exception) { };
            
            return names;
        }

        public SignalModel GetSignal
        {
            get { return this.Signal; }
        }
    }
}
