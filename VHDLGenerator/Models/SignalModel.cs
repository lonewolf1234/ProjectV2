using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHDLGenerator.Models
{
    class SignalModel
    {
        public SignalModel() {}

        public string Name { get; set; }

        public bool Bus { get; set; }

        public int MSB { get; set; }

        public int LSB { get; set; }

        public string Target_port { get; set; }

        public string Target_Comp { get; set; }

        public string Source_port { get; set; }

        public string Source_Comp { get; set; }
    }
}
