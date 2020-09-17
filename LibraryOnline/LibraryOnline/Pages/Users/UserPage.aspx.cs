using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Data;

namespace LibraryOnline.Pages
{
    public partial class UserPage : System.Web.UI.Page
    {
        private string QR = ""; //Для команд БД
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TbFill();
            }
        }
        DataProcedure procedure = new DataProcedure();
        private void TbFill()
        {
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-2OC8HFJ\\MYGRIT; Initial Catalog=Library;" +
            "Integrated Security=True; Connect Timeout=30; Encrypt=False;" +
            "TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False"))
            {
                DataTable dt = new DataTable();
                connection.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("select [Authorization_ID], [Login], [Password], [Surname], [Name], [Phone_Reader]," +
                    "[Ticket_Number], [Reader_Passport_Number], [Reader_Passport_Series] from [Reader]" +
                    "inner join [Authorization] on[Authorization_ID] = [ID_Authorization] where [Authorization_ID] = '" + DBConnection.idUser + "'", connection);

                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    tbLogin.Text = (myReader["Login"].ToString());
                    tbSurname.Text = (myReader["Surname"].ToString());
                    tbName.Text = (myReader["Name"].ToString());
                    tbPhone.Text = (myReader["Phone_Reader"].ToString());
                    tbPassportNumber.Text = (myReader["Reader_Passport_Number"].ToString());
                    tbPassportSeries.Text = (myReader["Reader_Passport_Series"].ToString());
                    lblTicketNumber.Text = (myReader["Ticket_Number"].ToString());
                    lblTicketSurname.Text = (myReader["Surname"].ToString());
                    lblTicketName.Text = (myReader["Name"].ToString());
                }
                connection.Close();
            }
        }
        //Активация текстовых полей для записи данных
        private void tbActive()
        {
            tbLogin.Enabled = true;
            tbLogin.CssClass = "user-input-active";
            tbPassword.Enabled = true;
            tbPassword.CssClass = "user-input-active";
            tbSurname.Enabled = true;
            tbSurname.CssClass = "user-input-active";
            tbName.Enabled = true;
            tbName.CssClass = "user-input-active";
            tbPhone.Enabled = true;
            tbPhone.CssClass = "user-input-active";
            tbPassportNumber.Enabled = true;
            tbPassportNumber.CssClass = "user-input-active";
            tbPassportSeries.Enabled = true;
            tbPassportSeries.CssClass = "user-input-active";
        }
        //Скрытие текстовых полей
        private void tbDisabled()
        {
            tbLogin.Enabled = false;
            tbLogin.CssClass = "user-input";
            tbPassword.Enabled = false;
            tbPassword.CssClass = "user-input";
            tbSurname.Enabled = false;
            tbSurname.CssClass = "user-input";
            tbName.Enabled = false;
            tbName.CssClass = "user-input";
            tbPhone.Enabled = false;
            tbPhone.CssClass = "user-input";
            tbPassportNumber.Enabled = false;
            tbPassportNumber.CssClass = "user-input";
            tbPassportSeries.Enabled = false;
            tbPassportSeries.CssClass = "user-input";
        }
        //Редактирование личных данных
        protected void btEdit_Click(object sender, EventArgs e)
        {
            tbActive();
            btEdit.Visible = false;
            btSave.Visible = true;
            btCansel.Visible = true;
        }
        //Сохранить изменения
        protected void btSave_Click(object sender, EventArgs e)
        {

                    lblLoginCheck.Visible = false;
                    DataProcedure procedure = new DataProcedure();
                    procedure.UsersUpdate(DBConnection.idUser, tbLogin.Text.ToString(), 1, tbSurname.Text.ToString(),
                        tbName.Text.ToString(), tbPhone.Text.ToString(), tbPassportNumber.Text.ToString(), tbPassportSeries.Text.ToString(), false);
                    procedure.UsersUpdatePassword(DBConnection.idUser, tbPassword.Text.ToString());
            tbPassword.Text = string.Empty;
            tbDisabled();
            btEdit.Visible = true;
            btSave.Visible = false;
            btCansel.Visible = false;
        }
        //Отменить изменения
        protected void btCansel_Click(object sender, EventArgs e)
        {
            tbDisabled();
            btEdit.Visible = true;
            btSave.Visible = false;
            btCansel.Visible = false;
        }
        //Выход из учётной записи
        protected void btExit_Click(object sender, EventArgs e)
        {
            DBConnection.idUser = 0;
            Response.Redirect("MainPage.aspx");
        }
    }
}