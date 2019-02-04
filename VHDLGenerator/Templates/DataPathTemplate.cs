using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHDLGenerator.Models;

namespace VHDLGenerator.Templates
{
    partial class DataPathTemplate : DataPathModel
    {
        public DataPathTemplate(DataPathModel data)
        {
            this.Name = data.Name;
            this.ArchName = data.ArchName;
            this.Ports = data.Ports;
            this.Components = data.Components;
            this.Signals = data.Signals;
        }
    }
}
