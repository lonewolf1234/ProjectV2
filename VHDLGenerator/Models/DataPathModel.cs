using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHDLGenerator.Models
{
    class DataPathModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string ArchName { get; set; }
        
        public List<PortModel> Ports { get; set; }

        public List<ComponentModel> Components { get; set; }

        public List<SignalModel> Signals { get; set; }

    }
}
