using Customization;
using PX.Data;
using PX.Data.BQL.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAPPA
{
    public partial class KAPCustomizationPlugin : CustomizationPlugin
    {

        public override void UpdateDatabase()
        {
            PXTrace.WriteInformation("testig_Kappa_123");
            WriteLog($"testig_Kappa_start");
            ExecuteKappaDatabaseInitializer();
            WriteLog($"testig_Kappa_end");
        }

        public void ExecuteKappaDatabaseInitializer()
        {
            PXTrace.WriteInformation("testig_Kappa_ExecuteKappaDatabaseInitializer");
        }
    }
}
