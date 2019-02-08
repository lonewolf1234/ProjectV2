using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHDLGenerator.Models;
using Newtonsoft.Json;
using System.ComponentModel;

namespace VHDLGenerator.ViewModels
{
    class SignalViewModel : INotifyPropertyChanged
    {
        #region Property Changed Interface
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion

        #region Private Variables
        private SignalModel Signal = new SignalModel();
        private DataPathModel _Datapath = new DataPathModel();
        private List<string> SPorts = new List<string>();
        private List<string> TPorts = new List<string>();
        private bool _GridEnable;
        #endregion

        public SignalViewModel(string datapath)
        {
           _Datapath = JsonConvert.DeserializeObject<DataPathModel>(datapath);
        }

        #region Properties
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
        public bool GridEnable
        {
            get
            {
                if (this.SCompName == _Datapath.Name || this.TCompName == _Datapath.Name)
                {
                    this._GridEnable = false;
                }
                else
                {
                    this._GridEnable = true;
                }
                return this._GridEnable;
            }
            set
            {
                this._GridEnable = value;
                
            }
        }

        //item source - components
        //use as source for the combobox items
        public List<string> ComponentNames
        {
            get
            {
                return GetCompName();
            }
        }
        public SignalModel GetSignal
        {
            get { return this.Signal; }
        }

        //item selected
        //for selected item in source catsx
        public string SCompName
        {
            get { return Signal.Source_Comp; }
            set
            {
                this.Signal.Source_Comp = value;

                this.SPorts = GetPortNames(this.Signal.Source_Comp, "source");
                OnPropertyChanged("SCompPorts");
                OnPropertyChanged("GridEnable");
            }
        }
        //for selected item in traget cat
        public string TCompName
        {
            get { return Signal.Target_Comp; }
            set
            {
                this.Signal.Target_Comp = value;
                this.TPorts = GetPortNames(this.Signal.Target_Comp, "target");
                OnPropertyChanged("TCompPorts");
                OnPropertyChanged("GridEnable");
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
        #endregion

        #region Methods
        private List<string> GetCompName()
        {
            List<string> names = new List<string>();
            try
            {
                foreach(ComponentModel comp in _Datapath.Components)
                {
                    names.Add(comp.Name);
                }
                names.Add(_Datapath.Name);
            }
            catch(Exception) { }
            
            return names;
        }

        private List<string> GetPortNames(string selectedComponent, string filter)
        {

            List<string> names = new List<string>();

            try
            {
                if (_Datapath.Name == selectedComponent)
                {
                    foreach (PortModel port in _Datapath.Ports)
                    {
                        if (filter == "source" && port.Direction == "in")
                        {
                            names.Add(port.Name);
                        }
                        else if (filter == "target" && (port.Direction == "out" || port.Direction == "inout"))
                        {
                            names.Add(port.Name);
                        }
                    }
                }
                else
                {
                    foreach (ComponentModel comp in _Datapath.Components)
                    {
                        if (comp.Name == selectedComponent)
                        {
                            foreach (PortModel port in comp.Ports)
                            {
                                if (filter == "source" && (port.Direction == "out" || port.Direction == "inout"))
                                {
                                    names.Add(port.Name);
                                }
                                else if (filter == "target" && port.Direction == "in")
                                {
                                    names.Add(port.Name);
                                }
                            }
                        }
                    }
                }

                
            }
            catch (Exception) { };
            
            return names;
        }
        #endregion

    }
}
