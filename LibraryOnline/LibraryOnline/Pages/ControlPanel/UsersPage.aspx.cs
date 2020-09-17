using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Drawing;

namespace LibraryOnline.Pages.Admin
{
    public partial class UsersPage : System.Web.UI.Page
    {
        private string QR = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            QR = DBConnection.qrUsers;
            if (!IsPostBack)
            {
                gvFill(QR);
                ddlRoleFill();
            }
        }
        private void gvFill(string qr)
        {
            sdsUsers.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsUsers.SelectCommand = qr;
            sdsUsers.DataSourceMode = SqlDataSourceMode.DataReader;
            gvUsers.DataSource = sdsUsers;
            gvUsers.DataBind();
        }
        private void ddlRoleFill()
        {
            sdsRole.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsRole.SelectCommand = DBConnection.qrRoles;
            sdsRole.DataSourceMode = SqlDataSourceMode.DataReader;
            ddlRole.DataSource = sdsRole;
            ddlRole.DataTextField = "Название роли";
            ddlRole.DataValueField = "ID";
            ddlRole.DataBind();
        }

        //Сортировка записей в таблице
        protected void gvUsers_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection sortDirection = SortDirection.Ascending;
            string strField = string.Empty;
            switch (e.SortExpression)
            {
                case ("ID"):
                    e.SortExpression = "[ID_Authorization]";
                    break;
                case ("Логин"):
                    e.SortExpression = "[Login]";
                    break;
                case ("Фамилия"):
                    e.SortExpression = "[Surname]";
                    break;
                case ("Имя"):
                    e.SortExpression = "[Name]";
                    break;
                case ("Номер телефона"):
                    e.SortExpression = "[Phone_Reader]";
                    break;
                case ("Номер билета"):
                    e.SortExpression = "[Ticket_Number]";
                    break;
                case ("Номер паспорта"):
                    e.SortExpression = "[Reader_Passport_Number]";
                    break;
                case ("Серия паспорта"):
                    e.SortExpression = "[Reader_Passport_Series]";
                    break;
                case ("Заблокирован"):
                    e.SortExpression = "[Ban]";
                    break;
            }
            sortGridView(gvUsers, e, out sortDirection, out strField);
            string strDirection = sortDirection
                == SortDirection.Ascending ? "ASC" : "DESC";
            gvFill(QR + " order by " + e.SortExpression + " " + strDirection);
        }
        private void sortGridView(GridView gridView,
         GridViewSortEventArgs e,
         out SortDirection sortDirection,
         out string strSortField)
        {
            strSortField = e.SortExpression;
            sortDirection = e.SortDirection;

            if (gridView.Attributes["CurrentSortField"] != null &&
                gridView.Attributes["CurrentSortDirection"] != null)
            {
                if (strSortField ==
                    gridView.Attributes["CurrentSortField"])
                {
                    if (gridView.Attributes["CurrentSortDirection"]
                        == "ASC")
                    {
                        sortDirection = SortDirection.Descending;
                    }
                    else
                    {
                        sortDirection = SortDirection.Ascending;
                    }
                }
            }
            gridView.Attributes["CurrentSortField"] = strSortField;
            gridView.Attributes["CurrentSortDirection"] =
                (sortDirection == SortDirection.Ascending ? "ASC"
                : "DESC");
        }
        //Очистка полей
        protected void Cleaner()
        {
            tbName.Text = string.Empty;
            tbSurname.Text = string.Empty;
            tbPhone.Text = string.Empty;
            tbPassportNumber.Text = string.Empty;
            tbPassportSeries.Text = string.Empty;
            ddlRole.SelectedIndex = 0;
            tbLogin.Text = string.Empty;
            tbPassword.Text = string.Empty;
            cbBan.Checked = false;

        }
        //Добавление записи
        protected void btInsert_Click(object sender, EventArgs e)
        {
            DBConnection connection = new DBConnection();
            //Проверка уникальности логина
            if (connection.LoginCheck(tbLogin.Text) > 0)
            {
                lblLoginCheck.Visible = true;
            }
            else
            {
                lblLoginCheck.Visible = false;
                bool banState;
                if (cbBan.Checked)
                {
                    banState = true;
                }
                else
                {
                    banState = false;
                }
                DataProcedure procedure = new DataProcedure();
                procedure.UsersInsert(tbLogin.Text.ToString(), tbPassword.ToString(), Convert.ToInt32(ddlRole.SelectedValue), tbSurname.Text.ToString(),
                    tbName.Text.ToString(), tbPhone.Text.ToString(), tbPassportNumber.Text.ToString(), tbPassportSeries.Text.ToString(), banState);
                Cleaner();
                gvFill(QR);

            }
        }
        //Поиск
        protected void btSearch_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                foreach (GridViewRow row in gvUsers.Rows)
                {
                    if (row.Cells[1].Text.Equals(tbSearch.Text) ||
                        row.Cells[2].Text.Equals(tbSearch.Text) ||
                        row.Cells[3].Text.Equals(tbSearch.Text) ||
                        row.Cells[4].Text.Equals(tbSearch.Text) ||
                        row.Cells[5].Text.Equals(tbSearch.Text) ||
                        row.Cells[6].Text.Equals(tbSearch.Text) ||
                        row.Cells[7].Text.Equals(tbSearch.Text) ||
                        row.Cells[8].Text.Equals(tbSearch.Text) ||
                        row.Cells[10].Text.Equals(tbSearch.Text))
                        row.BackColor = ColorTranslator.FromHtml("#a1f2be");
                    else
                        row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                }
                btCancel.Visible = true;
            }
        }
        //Фильтрация
        protected void btFilter_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                string newQR = QR + "where [ID_Authorization] like '%" + tbSearch.Text + "%' or [Login] like '%" + tbSearch.Text + "%' or [Surname] like '%" + tbSearch.Text + "%' or" +
                                "[Name] like '%" + tbSearch.Text + "%' or [Phone_Reader] like '%" + tbSearch.Text + "%' or [Ticket_Number] like '%" + tbSearch.Text + "%' or" +
                                "[Reader_Passport_Number] like '%" + tbSearch.Text + "%' or [Reader_Passport_Series] like '%" + tbSearch.Text + "%' or [Role_Name] like '%" + tbSearch.Text + "%'";
                gvFill(newQR);
                btCancel.Visible = true;
            }
        }
        //Отмена поиска и фильтрации
        protected void btCancel_Click(object sender, EventArgs e)
        {
            tbSearch.Text = "";
            btCancel.Visible = false;
            gvFill(QR);
        }

        protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[9].Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvUsers, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Нажмите, чтобы выбрать запись";
            }
        }
        //Выбор записи
        protected void gvUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvUsers.Rows)
            {
                if (row.RowIndex == gvUsers.SelectedIndex)
                {
                    row.ToolTip = string.Empty;
                }
                else
                {
                    row.ToolTip = "Нажмите, чтобы выбрать запись";
                }
            }
            int selectedRow = gvUsers.SelectedRow.RowIndex;
            GridViewRow rows = gvUsers.SelectedRow;
            DBConnection.idRecord = Convert.ToInt32(rows.Cells[1].Text.ToString());
            tbLogin.Text = rows.Cells[2].Text.ToString();
            tbSurname.Text = rows.Cells[3].Text.ToString();
            tbName.Text = rows.Cells[4].Text.ToString();
            tbPhone.Text = rows.Cells[5].Text.ToString();
            tbPassportNumber.Text = rows.Cells[7].Text.ToString();
            tbPassportSeries.Text = rows.Cells[8].Text.ToString();
            ddlRole.SelectedIndex = Convert.ToInt32(rows.Cells[9].Text.ToString())-1;
            CheckBox checkBox = (CheckBox)gvUsers.Rows[selectedRow].Cells[11].Controls[0];
            cbBan.Checked = checkBox.Checked;
            SelectedMessage.Visible = true;
            lblSelectedRow.Text = "Выбрана строка с ID " + DBConnection.idRecord;
            btUpdate.Visible = true;
            cbPasswordChange.Visible = true;
        }
        //Отменить выбор
        protected void btCanselSelected_Click(object sender, EventArgs e)
        {
            Cleaner();
            btUpdate.Visible = false;
            DBConnection.idRecord = 0;
            SelectedMessage.Visible = false;
            cbPasswordChange.Visible = false;
            gvFill(QR);
        }
        //Обновить данные
        protected void btUpdate_Click(object sender, EventArgs e)
        {
            bool banCheck;
            if (cbBan.Checked)
            {
                banCheck = true;
            }
            else
            {
                banCheck = false;
            }
            DataProcedure procedure = new DataProcedure();
            switch (cbPasswordChange.Checked)
            {
                case (true):
                    RequiredFieldValidator7.Enabled = true;
                    procedure.UsersUpdate(DBConnection.idRecord, tbLogin.Text.ToString(), Convert.ToInt32(ddlRole.SelectedValue), tbSurname.Text.ToString(), tbName.Text.ToString(),
                        tbPhone.Text.ToString(), tbPassportNumber.Text.ToString(), tbPassportSeries.Text.ToString(), banCheck);
                    procedure.UsersUpdatePassword(DBConnection.idRecord, tbPassword.Text.ToString());
                    break;
                case (false):
                    RequiredFieldValidator7.Enabled = false;
                    procedure.UsersUpdate(DBConnection.idRecord, tbLogin.Text.ToString(), Convert.ToInt32(ddlRole.SelectedValue), tbSurname.Text.ToString(), tbName.Text.ToString(),
                        tbPhone.Text.ToString(), tbPassportNumber.Text.ToString(), tbPassportSeries.Text.ToString(), banCheck);
                    break;
            }
            Cleaner();
            gvFill(QR);
            btUpdate.Visible = false;
            SelectedMessage.Visible = false;
            cbPasswordChange.Visible = false;
            DBConnection.idRecord = 0;
        }
        //Удаление записи
        protected void gvUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Index = Convert.ToInt32(e.RowIndex);
            DataProcedure procedure = new DataProcedure();
            GridViewRow rows = gvUsers.SelectedRow;
            DBConnection.idRecord = Convert.ToInt32(gvUsers.Rows[Index].Cells[1].Text.ToString());
            procedure.UsersDelete(DBConnection.idRecord);
            gvFill(QR);
        }
    }
}