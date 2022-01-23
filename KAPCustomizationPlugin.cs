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
            var test = typeof(KAPSampleDac);
            foreach (var custAtb in test.CustomAttributes)
            {
                PXTrace.WriteInformation(custAtb.ToString());
            }
            var members = test.FindMembers(
                MemberTypes.All, 
                BindingFlags.Default,
                new MemberFilter(DelegateToSearchCriteria),
                "*");

            foreach(var t in members)
            {
                PXTrace.WriteInformation($"Name:{t.Name} type:{t.MemberType}");

            }

            foreach(var t in test.GetProperties())
            {
                PXTrace.WriteInformation(t.ToString());
            }

            PXTrace.WriteInformation("test");
            //foreach (var atb in test.FindMembers)
            //{
            //    PXTrace.WriteInformation(custAtb.ToString());
            //}
        }

        private bool DelegateToSearchCriteria(MemberInfo m, object filterCriteria)
        {
            return true;
        }
    }
}
