﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHDLGenerator.Models
{
    public class DataPathModel
    {
        public DataPathModel() { }

        public string Name { get; set; }

        public string ArchName { get; set; }
        
        public List<PortModel> Ports { get; set; }

        public List<ComponentModel> Components { get; set; }

        public List<SignalModel> Signals { get; set; }

        //this.Name = data.Name;
        //this.ArchName = data.ArchName;
        //this.Ports = data.Ports;
        //this.Components = data.Components;
        //this.Signals = data.Signals;

    }
}
