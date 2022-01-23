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
                if (kappaType == null)
                    continue;
                //var test = typeof(KAPSampleDac);
                WriteLog($"Processing Type:{giTable.FullName}");

                var tableName = kappaType.Name;

                Type myType = Type.GetType(giTable.FullName);
                var sqlQuery = string.Format(@"IF NOT EXISTS(SELECT * FROM sys.objects WHERE name = '{0}')
                                                    CREATE TABLE[dbo].[{0}]( [CompanyID] [int] NOT NULL,", giTable.Name);

                var listOfKeys = new List<string>();
                foreach (var t in kappaType.GetProperties())
                {
                    string feildName = null;
                    int feildSize = 0;
                    string feildType = null;
                    sqlQuery = sqlQuery + string.Format("[{0}] {1} NOT NULL,", t.Name, ConvertDBTypes(t.PropertyType.Name));
                    foreach (var att in t.GetCustomAttributes())
                    {
                        bool? isKey = null;
                        if (att.GetType().GetProperty("IsKey") != null)
                        {
                            isKey = ((PX.Data.PXDBFieldAttribute)att)?.IsKey;
                        }
                        //if (att is PXDBStringAttribute)
                        //{
                        //    feildName = ((PXDBStringAttribute)att).FieldName;

                        //    //feildType = ((PXDBStringAttribute)att).;
                        //}
                        if (isKey == true)
                            listOfKeys.Add(t.Name);

                        WriteLog($"Processing {giTable.FullName}.{t.Name} Attribute:{att.ToString()}");
                    }
                }


                sqlQuery = sqlQuery.Trim().TrimEnd(',');
                sqlQuery = sqlQuery + string.Format(" CONSTRAINT [PK_{0}] PRIMARY KEY CLUSTERED ( ", giTable.Name);
                sqlQuery = sqlQuery + "[CompanyID] ASC,";

                if (listOfKeys.Count > 0)
                {

                    foreach (var key in listOfKeys)
                    {
                        sqlQuery = sqlQuery + string.Format("[{0}] ASC,", key);
                    }
                    
                    
                }
                sqlQuery = sqlQuery.Trim().TrimEnd(',');
                sqlQuery = sqlQuery + ")";
                sqlQuery = sqlQuery + ")";

                PXSPParameter parameter = new PXSPInParameter("@DynamicSql", PXDbType.NVarChar, sqlQuery);
                PXDatabase.Execute("sp_executesql", parameter);
                //PXSPParameter paramater = new PXSPParameter();
                //PXDatabase.Execute("sp_executesql", sqlQuery);


                //                var tsqlToDeterminIfFeildExists = @"
                //Select count(*) 
                //from sys.all_columns C
                //inner join sys.tables T on T.object_id = C.object_id
                //Where T.name = 'test123'
                //and C.name = 'column1'
                //";


                //ystem.Data.ParameterDirection direction = new System.Data.ParameterDirection();
                //PXDatabase.Execute("sp_executesql", new PXSPParameter("test",);

            }
        }

        private string ConvertDBTypes(string reflectedType)
        {
            reflectedType = reflectedType.ToLower();
            if (reflectedType == "string")
                return "varchar(255)";
            else if (reflectedType.Contains("int"))
                return "int";
            else if (reflectedType.Contains("bool"))
                return "bit";
            else if (reflectedType.Contains("decimal"))
                return "decimal(19, 6)";
            else if (reflectedType.Contains("guid"))
                return "uniqueidentifier";
            else
                return "DateTime";
        }

        private bool DelegateToSearchCriteria(MemberInfo m, object filterCriteria)
        {
            return true;
        }
    }
}
