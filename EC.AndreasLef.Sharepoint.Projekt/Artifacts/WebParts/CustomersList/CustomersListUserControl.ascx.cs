using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;

namespace EC.AndreasLef.Sharepoint.Projekt.Artifacts.WebParts.CustomersList
{
    public partial class CustomersListUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                SPWeb web = SPContext.Current.Web;
                    SPQuery query = new SPQuery();
                    //query.ViewFields = "<FieldRef Name='Title' />" +
                    //                    "<FieldRef Name='CustomerWebsite' />" +
                    //                   "<FieldRef Name='CustomerAddress' />";
                    query.Query = "<OrderBy><FieldRef Name='Title' Ascending='False'/></OrderBy>";
                    
                    query.RowLimit = 500;

                    SPList customerList = SPContext.Current.Web.Lists.TryGetList("Customers");

                    if (customerList != null && !Page.IsPostBack)
                    {
                        //Making sure these Fields are added to the Default View of the List.
                        GridView1.AutoGenerateColumns = false;

                        BoundField myTitle = new BoundField();
                        myTitle.DataField = "Title";
                        myTitle.HeaderText = "Kundens Namn";
                        GridView1.Columns.Add(myTitle);

                        BoundField columnCustomerWebsite = new BoundField();
                        columnCustomerWebsite.DataField = "CustomerWebsite";
                        columnCustomerWebsite.HeaderText = "Webbsida";
                        GridView1.Columns.Add(columnCustomerWebsite);

                        BoundField columnCustomerAdress = new BoundField();
                        columnCustomerAdress.DataField = "CustomerAddress";
                        columnCustomerAdress.HeaderText = "Adress";
                        GridView1.Columns.Add(columnCustomerAdress);



                        SPListItemCollection allCustomers = customerList.GetItems(query);

                        GridView1.DataSource = allCustomers.GetDataTable();
                        GridView1.DataBind();

                        //GridView1.RowDataBound += new GridViewRowEventHandler(GridView1_RowDataBound);
                    }
                
            }
            catch (Exception)
            {
                
                throw new Exception();
            }
        }

        //void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
