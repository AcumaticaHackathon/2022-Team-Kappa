using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAPPA
{
    public class KAPSampleDac : IBqlTable
    {
        public abstract class kapString : BqlString.Field<kapString> { }
        /// <summary>
        /// Holds the code used as a primary key in the ClickToPay system
        /// </summary>
        [PXDBString(256, IsUnicode = true)]
        [PXDefault("", PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Kappa String", Enabled = false)]
        public string KapString { get; set; }

        public abstract class kapString2 : BqlString.Field<kapString2> { }
        /// <summary>
        /// Holds the code used as a primary key in the ClickToPay system
        /// </summary>
        [PXDBString(256, IsUnicode = true)]
        [PXDefault("", PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Kappa String2", Enabled = false)]
        public string KapString2 { get; set; }
    }
}
