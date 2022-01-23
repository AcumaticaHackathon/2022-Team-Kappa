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

            

            List<string> tsqlColumns = new List<string>();

            foreach (var t in kappaType.GetProperties())
            {
                string feildName = null;
                int feildSize = 0;
                string feildType = null;
                PXTrace.WriteInformation(t.ToString());
                WriteLog($"Processing {typeName}.{t.Name}");
                feildName = t.Name;
                
                foreach (var att in t.GetCustomAttributes())
                {
                    if (att is PXDBStringAttribute)
                      {
                        feildSize = ((PXDBStringAttribute)att).Length;
                        feildType = $"NVarchar({feildSize})";
                        tsqlColumns.Add($"{feildName} {feildType}");
                    }
                    if (att is PXDBIntAttribute)
                    {
                        feildType = "int";
                        tsqlColumns.Add($"{feildName} {feildType}");
                    }
                    if (att is PXDBIntAttribute)
                    {
                        feildType = "bit";
                        tsqlColumns.Add($"{feildName} {feildType}");
                    }
                    WriteLog($"Processing {typeName}.{t.Name} Attribute:{att}");
                }
            }

            var script = $"Create Table {tableName} ({string.Join(" , ", tsqlColumns.ToArray())})";

            var testSql = @"
Create Table Test123 
(
	column1 varchar(255),
	column2 varchar(255)
)
";

            //            var tsqlToDeterminIfFeildExists = @"
            //Select count(*) 
            //from sys.all_columns C
            //inner join sys.tables T on T.object_id = C.object_id
            //Where T.name = 'test123'
            //and C.name = 'column1'
            //";

            PXSPParameter parameter = new PXSPInParameter("@DynamicSql", PXDbType.NVarChar, testSql);
            PXDatabase.Execute("sp_executesql", parameter);

        }

       
    }
}
