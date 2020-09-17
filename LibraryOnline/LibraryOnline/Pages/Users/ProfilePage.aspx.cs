using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LibraryOnline.Pages.Users
{
    public partial class ProfilePage : System.Web.UI.Page
    {
        private string QR = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            QR = DBConnection.qrProfile;
            if (!IsPostBack)
            {
                ddlPositionFill();
            }
        }
        private void ddlPositionFill()
        {
            sdsPosition.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsPosition.SelectCommand = DBConnection.qrPosition;
            sdsPosition.DataSourceMode = SqlDataSourceMode.DataReader;
            ddlPosition.DataSource = sdsPosition;
            ddlPosition.DataTextField = "Название должности";
            ddlPosition.DataValueField = "ID";
            ddlPosition.DataBind();
        }

        //Очистка полей
        private void Cleaner()
        {
            tbName.Text = string.Empty;
            tbSurname.Text = string.Empty;
            tbAge.Text = string.Empty;
            tbPhone.Text = string.Empty;
            ddlPosition.SelectedIndex = 1;
            cbEducation.Checked = false;
        }
        protected void btInsert_Click(object sender, EventArgs e)
        {
            DataProcedure procedure = new DataProcedure();
            if (Convert.ToInt32(tbAge.Text) < 18)
            {
                lblAgeCheck.Visible = true;
            }
            else
            {
                lblAgeCheck.Visible = false;
                bool educationCheck;
                if (cbEducation.Checked)
                {
                    educationCheck = true;
                }
                else
                {
                    educationCheck = false;
                }
                procedure.ProfileInsert(tbSurname.Text.ToString(), tbName.Text.ToString(), tbAge.Text.ToString(), tbPhone.Text.ToString(), educationCheck, Convert.ToInt32(ddlPosition.SelectedValue));
                Cleaner();
                InsertMess.Visible = true;
            }
        }
    }
}