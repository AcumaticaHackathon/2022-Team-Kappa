using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.Objects.SO
{
    // Acuminator disable once PX1016 ExtensionDoesNotDeclareIsActiveMethod extension should be constantly active
    public class KAPSalesOrderMaintExt : PXGraphExtension<SOOrderEntry>
    {
        public PXAction<SOOrder> KAPTest;


        ///<summary>
        ///    This Provides a method for the user to navigate and or reference the ClickToPay link
        ///</summary>
        [PXButton]
        [PXUIField(DisplayName = "Test Kappa")]
        protected void kAPTest()
        {
            PXTrace.WriteInformation("testing kappa button");

            var kapCustomizationPlugin = new KAPPA.KAPCustomizationPlugin();
            kapCustomizationPlugin.ExecuteKappaDatabaseInitializer();

        }
    }
}
