using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHDLGenerator.Models
{
    class DataPath
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string ArchName { get; set; }

        public List<Component> Components { get; set; }

        public List<Port> Ports { get; set; }

    }
}
