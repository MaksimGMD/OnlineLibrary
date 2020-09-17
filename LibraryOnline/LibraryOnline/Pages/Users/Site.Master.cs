using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LibraryOnline
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                DBConnection connection = new DBConnection();
                switch(DBConnection.idUser != 0)
                {
                    case (true):
                        dvIn.Visible = false;
                        dvUserPage.Visible = true;
                        break;
                    case (false):
                        dvIn.Visible = true;
                        dvUserPage.Visible = false;
                        break;
                }
            }
        }
    }
}