using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHDLGenerator.Models
{
    class Signal
    {
        public Signal () {}

        public string Name { get; set; }

        public bool Bus { get; set; }

        public int MSB { get; set; }

        public int LSB { get; set; }

        public string Destination { get; set; }

        public string Destination_Comp { get; set; }

        public string Source { get; set; }

        public string Source_Comp { get; set; }
    }
}
