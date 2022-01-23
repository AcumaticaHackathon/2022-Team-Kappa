using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAPPA
{
    [PXCacheName("Sample DAC for team Kappa hackathon project")]
    public class KAPSampleDac : IBqlTable
    {
        public abstract class kapString : BqlString.Field<kapString> { }
        [PXDBString(256, IsUnicode = true)]
        [PXDefault("", PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Kappa String", Enabled = false)]
        public string KapString { get; set; }

        public abstract class kapInt : BqlString.Field<kapInt> { }
        [PXDBInt()]
        [PXDefault("", PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Kappa Integer Sample")]
        public int? KapInt { get; set; }


        public abstract class kapBool : BqlBool.Field<kapBool> { }
        [PXDBBool()]
        [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Kappa Bool Sample")]
        public bool? KapBool { get; set; }


    }
}
