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

    }
}
