using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHDLGenerator.Models;

namespace VHDLGenerator.DataModels
{
    class SignalData
    {
        private SignalModel Signal = new SignalModel();

        public string SigEntityName
        {
            get { return Signal.Name; }
            set { Signal.Name = value; }
        }

    }
}
