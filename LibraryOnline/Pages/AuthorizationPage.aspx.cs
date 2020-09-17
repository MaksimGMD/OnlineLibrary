using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;


namespace LibraryOnline.Pages
{
    public partial class AuthorizationPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btRegistration_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistrationPage.aspx");
        }
        protected void btEnter_Click(object sender, EventArgs e)
        {
            DBConnection connection = new DBConnection();
            connection.Authorization(tbLogin.Text, tbPassword.Text);
            switch(DBConnection.idUser)
            {
                case (0):
                    tbLogin.BackColor = ColorTranslator.FromHtml("#FEA7A7");
                    tbPassword.BackColor = ColorTranslator.FromHtml("#FEA7A7");
                    lbAuthError.Visible = true;
                    break;
                default:
                    switch(connection.userRole(DBConnection.idUser))
                    {
                        //Читатель
                        case ("1"):
                            Response.Redirect("Users/MainPage.aspx");
                            break;
                        //Библиотекарь
                        case ("2"):
                            Response.Redirect("ControlPanel/OrdersPage.aspx");
                            break;
                        //Сотрудник отдела кадро
                        case ("3"):
                            Response.Redirect("ControlPanel/EmployeeAccPage.aspx");
                            break;
                        //Администратор
                        case ("4"):
                            Response.Redirect("ControlPanel/UsersPage.aspx");
                            break;

                    }
                    break;
            }
        }
    }
}