using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace LibraryOnline.Pages.ControlPanel
{
    public partial class ProfilePage : System.Web.UI.Page
    {
        private string QR = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            QR = DBConnection.qrProfile;
            if (!IsPostBack)
            {
                gvFill(QR);
                ddlPositionFill();
            }
        }
        private void gvFill(string qr)
        {
            sdsProfile.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsProfile.SelectCommand = qr;
            sdsProfile.DataSourceMode = SqlDataSourceMode.DataReader;
            gvProfile.DataSource = sdsProfile;
            gvProfile.DataBind();
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
        //Сортировка записей в таблице
        protected void gvProfile_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection sortDirection = SortDirection.Ascending;
            string strField = string.Empty;
            switch (e.SortExpression)
            {
                case ("ID"):
                    e.SortExpression = "[ID_Profile]";
                    break;
                case ("Фамилия"):
                    e.SortExpression = "[Surname_Profile]";
                    break;
                case ("Имя"):
                    e.SortExpression = "[Name_Profile]";
                    break;
                case ("Возраст"):
                    e.SortExpression = "[Age]";
                    break;
                case ("Номер телефона"):
                    e.SortExpression = "[Phone]";
                    break;
                case ("Высшее образование"):
                    e.SortExpression = "[Higher_Education]";
                    break;
                case ("Должность"):
                    e.SortExpression = "[Position_Name]";
                    break;
            }
            sortGridView(gvProfile, e, out sortDirection, out strField);
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
            tbAge.Text = string.Empty;
            tbPhone.Text = string.Empty;
            ddlPosition.SelectedIndex = 1;
            cbEducation.Checked = false;
        }
        //Добавление записей
        protected void btInsert_Click(object sender, EventArgs e)
        {
            DataProcedure procedure = new DataProcedure();
            if(Convert.ToInt32(tbAge.Text) < 18)
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
                gvFill(QR);
            }
        }

        protected void gvProfile_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[7].Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvProfile, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Нажмите, чтобы выбрать запись";
            }
        }
        //Выбор записи
        protected void gvProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvProfile.Rows)
            {
                if (row.RowIndex == gvProfile.SelectedIndex)
                {
                    row.ToolTip = string.Empty;
                }
                else
                {
                    row.ToolTip = "Нажмите, чтобы выбрать запись";
                }
            }
            int selectedRow = gvProfile.SelectedRow.RowIndex;
            GridViewRow rows = gvProfile.SelectedRow;
            DBConnection.idRecord = Convert.ToInt32(rows.Cells[1].Text.ToString());
            tbSurname.Text = rows.Cells[2].Text.ToString();
            tbName.Text = rows.Cells[3].Text.ToString();
            tbAge.Text = rows.Cells[4].Text.ToString();
            tbPhone.Text = rows.Cells[5].Text.ToString();
            CheckBox checkBox = (CheckBox)gvProfile.Rows[selectedRow].Cells[6].Controls[0];
            cbEducation.Checked = checkBox.Checked;
            ddlPosition.SelectedIndex = Convert.ToInt32(rows.Cells[7].Text.ToString()) - 1;
            SelectedMessage.Visible = true;
            lblSelectedRow.Text = "Выбрана строка с ID " + DBConnection.idRecord;
            btUpdate.Visible = true;
        }
        //Отменить выбор
        protected void btCanselSelected_Click(object sender, EventArgs e)
        {
            Cleaner();
            btUpdate.Visible = false;
            DBConnection.idRecord = 0;
            SelectedMessage.Visible = false;
            gvFill(QR);
        }
        //Обновление записи
        protected void btUpdate_Click(object sender, EventArgs e)
        {
            bool educationCheck;
            if (cbEducation.Checked)
            {
                educationCheck = true;
            }
            else
            {
                educationCheck = false;
            }
            DataProcedure procedure = new DataProcedure();
            procedure.ProfileUpdate(DBConnection.idRecord, tbSurname.Text.ToString(), tbName.Text.ToString(), tbAge.Text.ToString(), tbPhone.Text.ToString(),
                educationCheck, Convert.ToInt32(ddlPosition.SelectedValue));
            Cleaner();
            gvFill(QR);
            btUpdate.Visible = false;
            SelectedMessage.Visible = false;
            DBConnection.idRecord = 0;
        }
        //Удаление строки
        protected void gvProfile_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Index = Convert.ToInt32(e.RowIndex);
            DataProcedure procedure = new DataProcedure();
            GridViewRow rows = gvProfile.SelectedRow;
            DBConnection.idRecord = Convert.ToInt32(gvProfile.Rows[Index].Cells[1].Text.ToString());
            procedure.ProfileDelete(DBConnection.idRecord);
            gvFill(QR);
        }

        //Фильтрация
        protected void btFilter_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                string newQR = QR + "where [ID_Profile] like '%" + tbSearch.Text + "%' or [Surname_Profile] like '%" + tbSearch.Text + "%' or [Name_Profile] like '%" + tbSearch.Text + "%' or" +
                "[Age] like '%" + tbSearch.Text + "%' or [Phone] like '%" + tbSearch.Text + "%' or [Higher_Education] like '%" + tbSearch.Text + "%' or [Position_Name] like '%" + tbSearch.Text + "%'";
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
        //Поиск
        protected void btSearch_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                foreach (GridViewRow row in gvProfile.Rows)
                {
                    if (row.Cells[1].Text.Equals(tbSearch.Text) ||
                        row.Cells[2].Text.Equals(tbSearch.Text) ||
                        row.Cells[3].Text.Equals(tbSearch.Text) ||
                        row.Cells[4].Text.Equals(tbSearch.Text) ||
                        row.Cells[5].Text.Equals(tbSearch.Text) ||
                        row.Cells[6].Text.Equals(tbSearch.Text) ||
                        row.Cells[7].Text.Equals(tbSearch.Text))
                        row.BackColor = ColorTranslator.FromHtml("#a1f2be");
                    else
                        row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                }
                btCancel.Visible = true;
            }
        }
    }
}