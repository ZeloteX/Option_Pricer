using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Option_Pricer.Objects;

namespace Option_Pricer.DataModels
{
    internal class InputGlobal
    {
        public required List<OptionInfo> Option_infos { get; set; }
    }
}
