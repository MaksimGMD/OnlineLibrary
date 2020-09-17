using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services.Description;
using System.Text;
using System.Security.Cryptography;

namespace LibraryOnline.Pages
{
    public partial class RegistrationPage : System.Web.UI.Page
    {
        DataProcedure procedure = new DataProcedure();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //Регистрация для клиента
        protected void btRegistration_Click(object sender, EventArgs e)
        {
            DBConnection connection = new DBConnection();
            //Проверка уникальности логина
            if (connection.LoginCheck(tbLogin.Text) > 0)
            {
                lblLoginCheck.Visible = true;
            }
            else
            {
                if(tbPasswordConfirm.Text.ToString() != tbPassword.Text.ToString())
                {
                    lbPasswordConfirm.Visible = true;
                }
                else
                {
                    lblLoginCheck.Visible = false;
                    lbPasswordConfirm.Visible = false;
                    DataProcedure procedure = new DataProcedure();
                    procedure.UsersInsert(tbLogin.Text.ToString(), tbPassword.Text.ToString(), Convert.ToInt32(1), tbSurname.Text.ToString(),
                    tbName.Text.ToString(), tbPhone.Text.ToString(), tbPassportNumber.Text.ToString(), tbPassportSeries.Text.ToString(), false);
                    Response.Redirect("AuthorizationPage.aspx");
                }
            }
        }
    }
}