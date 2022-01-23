using Customization;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Data.Maintenance.GI;
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

        //         foreach (var res in result)
        //            {
        //                var giTable = (PXTablesSelectorAttribute.SingleTable)res;
        //                if (giTable.FullName.Contains("DBUpdateTest"))
        //                {
        //                    Type myType = Type.GetType(giTable.FullName);
        //        var sqlQuery = string.Format(@"IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = '{0}')
        //                                        CREATE TABLE[dbo].[{0}](", giTable.Name);
        //                    foreach (var custAtb in myType.CustomAttributes)
        //                    {
        //                        PXTrace.WriteInformation(custAtb.ToString());
        //                    }
        //    var members = myType.FindMembers(
        //        MemberTypes.All,
        //        BindingFlags.Default,
        //        new MemberFilter(DelegateToSearchCriteria),
        //        "*");

        //                    foreach (var t in members)
        //                    {
        //                        PXTrace.WriteInformation($"Name:{t.Name} type:{t.MemberType}");

        //                        sqlQuery = sqlQuery + string.Format("[{0}] [{1}] NOT NULL,", t.Name, t.MemberType);

        //}

        //foreach (var t in myType.GetProperties())
        //{
        //    PXTrace.WriteInformation(t.ToString());
        //}

        //PXTrace.WriteInformation("test");
        //                    //foreach (var atb in test.FindMembers)
        //                    //{
        //                    //    PXTrace.WriteInformation(custAtb.ToString());
        //                    //}
        //                }
        //            }

        public void ExecuteKappaDatabaseInitializer()
        {
            PXTrace.WriteInformation("testig_Kappa_ExecuteKappaDatabaseInitializer");
            GenericInquiryDesigner databaseSchemaInquiry = PXGraph.CreateInstance<GenericInquiryDesigner>();
            var result = PXSelectorAttribute.SelectAll<GITable.name>(databaseSchemaInquiry.Tables.Cache, null);

            foreach (var res in result)
            {
                var giTable = (PXTablesSelectorAttribute.SingleTable)res;
                var kappaType = Type.GetType(giTable.FullName);
                //var test = typeof(KAPSampleDac);
                WriteLog($"Processing Type:{giTable.FullName}");

                var tableName = kappaType.Name;

                Type myType = Type.GetType(giTable.FullName);
                var sqlQuery = string.Format(@"IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = '{0}')
                                                    CREATE TABLE[dbo].[{0}](", giTable.Name);

                foreach (var t in kappaType.GetProperties())
                {
                    string feildName = null;
                    int feildSize = 0;
                    string feildType = null;
                    PXTrace.WriteInformation(t.ToString());
                    WriteLog($"Processing {giTable.FullName}.{t.Name}");
                    foreach (var att in t.GetCustomAttributes())
                    {
                        if (att is PXDBStringAttribute)
                        {
                            feildName = ((PXDBStringAttribute)att).FieldName;
                            feildSize = ((PXDBStringAttribute)att).Length;
                            //feildType = ((PXDBStringAttribute)att).;
                        }
                        WriteLog($"Processing {giTable.FullName}.{t.Name} Attribute:{att.ToString()}");
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
        }

        private bool DelegateToSearchCriteria(MemberInfo m, object filterCriteria)
        {
            return true;
        }
    }
}
