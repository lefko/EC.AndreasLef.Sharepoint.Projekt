using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using EC.AndreasLef.Sharepoint.Projekt.Code;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;

namespace EC.AndreasLef.Sharepoint.Projekt.Features.EC.AndreasLef.AddList
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("bb109490-6542-4988-8d6c-1f09a2e825c8")]
    public class ECAndreasLefEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

       

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            try
            {

                //TODO:Should a using statement go here?
                SPWeb web = properties.Feature.Parent as SPWeb;

                // kolla om listan finns
                SPList customers = web.Lists.TryGetList("Customers");
                if (customers != null)
                {
                    //TODO: What should happen here??
                    customers.Delete();
                    //customers.Update();
                    Logger.WriteTrace("Old version of list Customers was removed.");
                }

                Guid listId = web.Lists.Add("Customers",
                    "My Customers List",
                    SPListTemplateType.GenericList);


                SPList myCustomers = web.Lists[listId];
                myCustomers.Fields.Add("CustomerWebsite", SPFieldType.URL, true);
                myCustomers.Fields.Add("CustomerAddress", SPFieldType.Text, true);
                myCustomers.Fields.Add("CustomerNotes", SPFieldType.Text, false);
                myCustomers.EnableVersioning = false;
                myCustomers.OnQuickLaunch = true;
                myCustomers.Update();

                customers = web.Lists.TryGetList("Customers");
                if (customers != null)
                    Logger.WriteTrace("List Customers added successfully");


                //TODO: FRÅGA ZIMMERGREN: Behöver man göara en Update varje gång man lägger till ett Item eller räcker det med ett för hela listanwww

                Logger.WriteTrace("Starting to populate Customers List.");

                SPListItem newCustomer = myCustomers.AddItem();
                newCustomer["Title"] = "Customer 1";
                newCustomer["CustomerWebsite"] = "http://www.foretaget.se";
                newCustomer["CustomerAddress"] = "Lund";
                newCustomer["CustomerNotes"] = "Lite anteckngar ang kunden";
                newCustomer.Update();

                SPListItem newCustomer2 = myCustomers.AddItem();
                newCustomer2["Title"] = "Customer 2";
                newCustomer2["CustomerWebsite"] = "http://www.konkurenten.se";
                newCustomer2["CustomerAddress"] = "Malmö";
                newCustomer2["CustomerNotes"] = "Lite anteckngar ang den andra kunden";
                newCustomer2.Update();

                SPListItem newCustomer3 = myCustomers.AddItem();
                newCustomer3["Title"] = "Customer 3";
                newCustomer3["CustomerWebsite"] = "http://www.enannankonkurent.se";
                newCustomer3["CustomerAddress"] = "Helsingborg";
                newCustomer3["CustomerNotes"] = "Lite anteckngar ang en annan kund";
                newCustomer3.Update();

                SPListItem newCustomer4 = myCustomers.AddItem();
                newCustomer4["Title"] = "Customer 4";
                newCustomer4["CustomerWebsite"] = "http://www.girigakunden.se";
                newCustomer4["CustomerAddress"] = "Stockholm";
                newCustomer4["CustomerNotes"] = "Shit vad vi ska käna pengar här";
                newCustomer4.Update();

                Logger.WriteTrace("Finished Populating Customers List");

            }
            catch (Exception exc)
            {
                
                throw;
            }
            
            


        }


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            SPWeb web = properties.Feature.Parent as SPWeb;

            
            SPList customers = web.Lists.TryGetList("Customers");

            if (customers != null)
            {
                customers.Delete();
                customers.Update();
                
            }
        }


        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}
