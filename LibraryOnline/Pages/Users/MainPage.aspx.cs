using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace LibraryOnline.Pages
{
    public partial class MainPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        //Добавить книгу к заказу
        protected void btInsertOrder_Click(object sender, EventArgs e)
        {

            if (DBConnection.idUser == 0)
            {
                Response.Redirect("../AuthorizationPage.aspx");
            }
            else
            {
                var btn = (Button)sender;
                //Get the repeater selected row
                var item = (RepeaterItem)btn.NamingContainer;
                //FInd the control from row
                var IdValue = ((Label)item.FindControl("lblID")).Text;
                DataProcedure procedure = new DataProcedure();
                procedure.UserOrderInsert(Convert.ToInt32(IdValue), 3, DBConnection.idUser);
            }
        }
    }
}