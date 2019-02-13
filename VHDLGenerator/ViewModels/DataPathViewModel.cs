using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHDLGenerator.Models;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace VHDLGenerator.ViewModels
{
    class DataPathViewModel : INotifyPropertyChanged , IDataErrorInfo
    {
        #region Property Changed Interface
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion

        private DataPathModel DataPath = new DataPathModel();
        private List<PortModel> Ports = new List<PortModel>();
        private PortModel Port = new PortModel();
        private bool AddPort { get; set; }

        public DataPathViewModel()
        {
            ArchNameTxt = "Behavioural";
            this._BitsEnable = false;
        }

        // Main Component Properties
        public string EntityNameTxt
        {
            get { return this.DataPath.Name; }
            set { this.DataPath.Name = value; }
        }
        public string ArchNameTxt
        {
            get { return this.DataPath.ArchName; }
            set { this.DataPath.ArchName = value; }
        }
        public bool _FinishEnable { get; set; }

        //Port Properties
        public string PortNameTxt
        {
            get { return this.Port.Name; }
            set { this.Port.Name = value; }
        }
        public string DirectionSel
        {
            get { return this.Port.Direction; }
            set { this.Port.Direction = value; }
        } 
        public bool BusSel
        {
            get { return this.Port.Bus; }
            set
            {
                this.Port.Bus = value;
                OnPropertyChanged("BitsEnable");
            }
        }
        public string MsbTxt
        {
            get { return this.Port.MSB; }
            set { this.Port.MSB = value; }
        }
        public string LsbTxt
        {
            get { return this.Port.LSB; }
            set { this.Port.LSB = value; }
        }
        public bool _BitsEnable { get; set; }
       


        //Add Port Selected
        public bool AddPortSel
        {
            get { return this.AddPort; }
            set
            {
                this.AddPort = value;
                if (AddPortSel == true)
                {
                    Ports.Add(GetPortData);
                    DataPath.Ports = Ports;
                    this.AddPort = false;

                    this.Port.Clear();
                    OnPropertyChanged("PortNameTxt");
                    OnPropertyChanged("DirectionSel");
                    OnPropertyChanged("BusSel");
                    OnPropertyChanged("MsbTxt");
                    OnPropertyChanged("LsbTxt");
                }
            }
        }

        //Gets the Port Data entered
        public PortModel GetPortData
        {
            get
            {
                PortModel TempPort = new PortModel
                {
                    Name = PortNameTxt,
                    Direction = DirectionSel,
                    Bus = BusSel,
                    MSB = MsbTxt,
                    LSB = LsbTxt,
                };
                return TempPort;
            }
        }
        public List<PortModel> GetPorts
        {
            get
            {
                return Ports;
            }
        }

        //Contains the list for the directions options
        public List<string> GetDirections
        {
            get
            {
                List<string> Directions = new List<string>
                {
                    "in",
                    "out",
                    "inout"
                };
                return Directions;
            }
        }

        // Gets the Component Constructed from the data entered
        public DataPathModel GetDataPath
        {
            get { return this.DataPath; }
        }

        //finish button enable
        public bool FinishEnable
        {
            get
            {
                if (ErrorCollection.Count == 0)
                {
                    this._FinishEnable = true;
                }
                else
                {
                    this._FinishEnable = false;
                }
                return this._FinishEnable;
            }
            set
            {
                this._FinishEnable = value;

            }
        }

        public bool BitsEnable
        {
            get
            {
                if (BusSel == true)
                {
                    this._BitsEnable = true;
                }
                else
                {
                    this._BitsEnable = false;
                    if(ErrorCollection.ContainsKey("MsbTxt"))
                    {
                        ErrorCollection.Remove("MsbTxt");
                    }
                    if (ErrorCollection.ContainsKey("LsbTxt"))
                    {
                        ErrorCollection.Remove("LsbTxt");
                    }
                    OnPropertyChanged("ErrorCollection");
                }
                return this._BitsEnable;
            }
            set
            {
                this._BitsEnable = value;

            }
        }

      



        #region Validation

        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();
        public List<string> ReservedWords = new List<string>
        {
            "abs", "access","after","alias","all","and","architecture","array","assert","attribute",
            "begin","block","body","buffer","bus",
            "case","component","configuration","constant",
            "disconnect","downto",
            "else","elsif","end","entity","exit",
            "file","for","function",
            "generate","generic","group","guarded",
            "if","impure","in","internal","inout","is",
            "label","library","linkage","loop",
            "map","mod",
            "nand","new","next","nor","not","null",
            "of","on","open","or","others","out",
            "package","port","postponed","procedure","process","pure",
            "range","record","register","reject","rem","report","return","rol","ror",
            "select","severity","signal","shared","sla","sll","sra","srl","subtype",
            "then","to","transport","type",
            "unaffected","units","use",
            "variable",
            "wait","when","while","with",
            "xnor","xor"
        };
        public string Error { get; }

        public string this[string propertyname]
        {
            get
            {
                string result = null;
                switch(propertyname)
                {
                    case "EntityNameTxt":
                        if (string.IsNullOrWhiteSpace(EntityNameTxt))
                            result = "Entity Name cannot be empty";
                        else if (IsBeginWNum(EntityNameTxt))
                            result = "Cannot begin with a number";
                        else if (IsValidName(EntityNameTxt))
                            result = "Not a valid name. Only Letters, Numbers or underscore are allowed";
                        else if (IsReservedWord(EntityNameTxt))
                            result = "This is a Reserved Word";
                        break;

                    case "PortNameTxt":
                        if (string.IsNullOrWhiteSpace(PortNameTxt))
                            result = "Port Name cannot be empty";
                        else if (IsBeginWNum(PortNameTxt))
                            result = "Cannot begin with a number";
                        else if (IsValidName(PortNameTxt))
                            result = "Not a valid name. Only Letters, Numbers or underscore are allowed";
                        else if (IsReservedWord(PortNameTxt))
                            result = "This is a Reserved Word";
                        break;

                    case "DirectionSel":
                        if(!IsDirectionSel(DirectionSel))
                            result = "No Direction Selected";
                        break;

                    case "MsbTxt":
                        if (BitsEnable == true)
                        {
                            if (string.IsNullOrWhiteSpace(MsbTxt))
                                result = "MSB cannot be empty";
                            else if (IsInterger(MsbTxt))
                                result = "Only Integers are allowed";
                            break;
                        }
                        else
                            break;

                    case "LsbTxt":
                        if (BitsEnable == true)
                        {
                            if (string.IsNullOrWhiteSpace(LsbTxt))
                                result = "LSB cannot be empty";
                            else if (IsInterger(LsbTxt))
                                result = "Only Integers are allowed";
                            break;
                        }
                        else
                            break;
                }

                if (ErrorCollection.ContainsKey(propertyname))
                    ErrorCollection[propertyname] = result;
                else if (result != null)
                    ErrorCollection.Add(propertyname, result);

                OnPropertyChanged("ErrorCollection");
                return result;
            }
        }

        public bool IsReservedWord (string word)
        {
            bool result = false;

            foreach (string s in ReservedWords)
            {
                if (s.ToLower() == word.ToLower())
                {
                    result = true;
                }
            }

            return result;
        }

        public bool IsInterger (string word)
        {
            Regex regex = new Regex("[^0-9]+");
            return (regex.IsMatch(word));
        }

        public bool IsValidName (string word)
        {
            Regex regex = new Regex("[^A-Za-z0-9_]+");
            return (regex.IsMatch(word));
        }

        public bool IsBeginWNum (string word)
        {
            Regex regex = new Regex("^[0-9]");
            return (regex.IsMatch(word));
        }

        public bool IsDirectionSel(string word)
        {
            if (word == null)
                return false;
            else
                return true;
        }


        #endregion
    }
}
