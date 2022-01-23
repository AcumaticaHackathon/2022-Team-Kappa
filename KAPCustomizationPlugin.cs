using Customization;
using PX.Data;
using PX.Data.BQL.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            var typeName = "KAPPA.KAPSampleDac";
            var test = Type.GetType(typeName);
            //var test = typeof(KAPSampleDac);
            WriteLog($"Processing Type:{typeName}");

            foreach (var t in test.GetProperties())
            {
                PXTrace.WriteInformation(t.ToString());
                WriteLog($"Processing {typeName}.{t.Name}");
                foreach (var att in t.GetCustomAttributes())
                {
                    WriteLog($"Processing {typeName}.{t.Name} Attribute:{att.ToString()}");
                }
            }

        }

        private bool DelegateToSearchCriteria(MemberInfo m, object filterCriteria)
        {
            return true;
        }
    }
}
