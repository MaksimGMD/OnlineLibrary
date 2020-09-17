using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LibraryOnline.Pages.Admin
{
    public partial class AdminSite : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DBConnection connection = new DBConnection();
                switch (connection.userRole(DBConnection.idUser))
                {
                    //Библиотекарь
                    case ("2"):
                        navbarDropdown.Text = "Библиотекарь";
                        aUsers.Visible = false;
                        aEmployee.Visible = false;
                        aProfiles.Visible = false;
                        aRoles.Visible = false;
                        dvOut.Visible = true;
                        break;
                    //Сотрудник отдела кадров
                    case ("3"):
                        navbarDropdown.Text = "Сотрудник отдела кадров";
                        aUsers.Visible = false;
                        aBooks.Visible = false;
                        aOrders.Visible = false;
                        dvOut.Visible = true;
                        break;
                    //Администратор
                    case ("4"):
                        navbarDropdown.Text = "Администратор";
                        dvOut.Visible = true;
                        break;
                    default:
                        navbarDropdown.Text = "Авторизоваться";
                        dvIn.Visible = true;
                        break;
                }
            }
        }
        //Выход из учётной записи
        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            Response.Redirect("../AuthorizationPage.aspx");
            DBConnection.idUser = 0;
        }
        //Переход к авторизации
        protected void Unnamed2_Click(object sender, EventArgs e)
        {
            Response.Redirect("../AuthorizationPage.aspx");
        }
    }
}