using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHDLGenerator.Models;

namespace VHDLGenerator.DataModels
{
    class ComponentData
    {
        private ComponentModel Component = new ComponentModel();
        private List<PortModel> Ports = new List<PortModel>();
        private PortModel Port = new PortModel();
        private bool AddPort { get; set; }

        public ComponentData()
        {

        }

        // Main Component Properties
        public string EntityNameTxt
        {
            get { return this.Component.Name; }
            set { this.Component.Name = value; }
        }

        public string ArchNameTxt
        {
            get {return this.Component.ArchName; }
            set {this.Component.ArchName = value; }
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
                    Ports.Add(Port);
                    Port.Clear();
                }
            }
        }

        // Finish Button Selected
        public bool FinishedSel
        {
            get { return this.FinishedSel; }
            set
            {
                this.FinishedSel = value;
                if(FinishedSel == true)
                {
                    Component.Ports = Ports;
                }
            }
        }

        public bool CancelSel
        {
            get { return this.AddPort; }
            set { this.AddPort = value; }
        }
    }
}
