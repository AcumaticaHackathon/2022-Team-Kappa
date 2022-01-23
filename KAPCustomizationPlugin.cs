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
            var kappaType = Type.GetType(typeName);
            //var test = typeof(KAPSampleDac);
            WriteLog($"Processing Type:{typeName}");

            var tableName = kappaType.Name;

            var script = $"Create Table tableName (";

            foreach (var t in kappaType.GetProperties())
            {
                string feildName = null;
                int feildSize = 0;
                string feildType = null;
                PXTrace.WriteInformation(t.ToString());
                WriteLog($"Processing {typeName}.{t.Name}");
                foreach (var att in t.GetCustomAttributes())
                {
                    if (att is PXDBStringAttribute)
                      {
                        feildName = ((PXDBStringAttribute)att).FieldName;
                        feildSize = ((PXDBStringAttribute)att).Length;
                        //feildType = ((PXDBStringAttribute)att).;
                    }
                    WriteLog($"Processing {typeName}.{t.Name} Attribute:{att.ToString()}");
                }
            }

            var testSql = @"
Create Table Test123 
(
	column1 varchar(255),
	column2 varchar(255)
)
";

            var tsqlToDeterminIfFeildExists = @"
Select count(*) 
from sys.all_columns C
inner join sys.tables T on T.object_id = C.object_id
Where T.name = 'test123'
and C.name = 'column1'
";


            //ystem.Data.ParameterDirection direction = new System.Data.ParameterDirection();
            //PXDatabase.Execute("sp_executesql", new PXSPParameter("test",);

        }

        private bool DelegateToSearchCriteria(MemberInfo m, object filterCriteria)
        {
            return true;
        }
    }
}
