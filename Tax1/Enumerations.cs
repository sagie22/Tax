using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Tax
{
    public class Enumerations
    {
        public enum IndividualTaxType
        {
            [Description("Flat Value")]
            flatValue,
            [Description("Flat Rate")]
            flatRate,
            [Description("Progressive")]
            progressive
        }
    }
}
