using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHDLGenerator.Models;

namespace VHDLGenerator.DataModels
{
    class DataPathData
    {
        private DataPathModel DataPath = new DataPathModel();

        private List<PortModel> Ports = new List<PortModel>();
        private PortModel Port = new PortModel();
        private bool AddPort { get; set; }

        public DataPathData()
        {
            ArchNameTxt = "Behavioural";
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
            set { this.Port.Bus = value; }
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
    }
}
