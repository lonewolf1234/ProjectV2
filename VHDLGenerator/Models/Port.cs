using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHDLGenerator.Models
{
    class Port
    {
        public Port() { }

        public int ID { get; set; }

        public string Name { get; set; }

        public string Direction { get; set; }

        public bool Bus { get; set; }

        public string MSB { get; set; }

        public string LSB { get; set; }

    }
}
