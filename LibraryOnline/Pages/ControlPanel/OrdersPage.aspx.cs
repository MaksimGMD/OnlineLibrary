using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data.SqlClient;

namespace LibraryOnline.Pages.ControlPanel
{
    public partial class OrdersPage : System.Web.UI.Page
    {
        private string QR = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            QR = DBConnection.qrOrders_List;
            if (!IsPostBack)
            {
                gvFill(QR);
                ddlBooksFill();
                ddlReadersFill();
            }
        }
        private void gvFill(string qr)
        {
            sdsOrders_List.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsOrders_List.SelectCommand = qr;
            sdsOrders_List.DataSourceMode = SqlDataSourceMode.DataReader;
            gvOrders.DataSource = sdsOrders_List;
            gvOrders.DataBind();
        }
        private void ddlBooksFill()
        {
            sdsBooks.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsBooks.SelectCommand = DBConnection.qrOrderBooks;
            sdsBooks.DataSourceMode = SqlDataSourceMode.DataReader;
            ddlBooks.DataSource = sdsBooks;
            ddlBooks.DataTextField = "Книга";
            ddlBooks.DataValueField = "ID_Book";
            ddlBooks.DataBind();
        }
        private void ddlReadersFill()
        {
            sdsTicketNumber.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsTicketNumber.SelectCommand = DBConnection.qrOrderTicketNumber;
            sdsTicketNumber.DataSourceMode = SqlDataSourceMode.DataReader;
            ddlReaders.DataSource = sdsTicketNumber;
            ddlReaders.DataTextField = "Ticket_Number";
            ddlReaders.DataValueField = "Authorization_ID";
            ddlReaders.DataBind();
        }
        protected void gvOrders_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection sortDirection = SortDirection.Ascending;
            string strField = string.Empty;
            switch (e.SortExpression)
            {
                case ("ID"):
                    e.SortExpression = "[ID]";
                    break;
                case ("Книга"):
                    e.SortExpression = "[Книга]";
                    break;
                case ("Дата выдачи"):
                    e.SortExpression = "[Дата выдачи]";
                    break;
                case ("Дата возврата"):
                    e.SortExpression = "[Дата возврата]";
                    break;
                case ("Статус"):
                    e.SortExpression = "[Статус]";
                    break;
                case ("Номер билета"):
                    e.SortExpression = "[Номер билета]";
                    break;
                case ("Библиотекарь"):
                    e.SortExpression = "[Библиотекарь]";
                    break;

            }
            sortGridView(gvOrders, e, out sortDirection, out strField);
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

        protected void gvOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[10].Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvOrders, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Нажмите, чтобы выбрать запись";
            }
        }

        protected void gvOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvOrders.Rows)
            {
                if (row.RowIndex == gvOrders.SelectedIndex)
                {
                    row.ToolTip = string.Empty;
                }
                else
                {
                    row.ToolTip = "Нажмите, чтобы выбрать запись";
                }
            }
            int selectedRow = gvOrders.SelectedRow.RowIndex;
            GridViewRow rows = gvOrders.SelectedRow;
            DBConnection.idRecord = Convert.ToInt32(rows.Cells[1].Text.ToString());
            ddlBooks.SelectedValue = rows.Cells[2].Text.ToString();
            if(rows.Cells[4].Text.ToString() == "Нет данных")
            {
                tbIssuance_Date.Text = string.Empty;
            }   
            else
            {
                tbIssuance_Date.Text = Convert.ToDateTime(rows.Cells[4].Text.ToString()).ToString("yyyy-MM-dd");
            }
            if (rows.Cells[5].Text.ToString() == "Нет данных")
            {
                tbReturn_Date.Text = string.Empty;
            }
            else
            {
                tbReturn_Date.Text = Convert.ToDateTime(rows.Cells[5].Text.ToString()).ToString("yyyy-MM-dd");
            }
            ddlStatus.SelectedValue = rows.Cells[6].Text.ToString();
            ddlReaders.SelectedValue = rows.Cells[8].Text.ToString();
            SelectedMessage.Visible = true;
            lblSelectedRow.Text = "Выбрана строка с ID " + DBConnection.idRecord;
            btUpdate.Visible = true;
        }
        //Очитска полей
        protected void Cleaner()
        {
            tbIssuance_Date.Text = string.Empty;
            tbReturn_Date.Text = string.Empty;
            ddlReaders.SelectedIndex = 0;
            ddlBooks.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
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
        //Проверка количества книг
        protected string AmountCheck(string IDBook)
        {
            SqlCommand command = new SqlCommand("", DBConnection.connection);
            command.CommandText = "select [Amount] from [Book] where [ID_Book] = '" + IDBook + "'";
            DBConnection.connection.Open();
            string BookAmount = command.ExecuteScalar().ToString();
            DBConnection.connection.Close();
            return BookAmount;
        }

        //Добавление записи
        protected void btInsert_Click(object sender, EventArgs e)
        {
            if(Convert.ToInt32(DBConnection.idUser) == 0)
            {
                Response.Redirect("../AuthorizationPage.aspx");
            }
            else
            {
                if(AmountCheck(ddlBooks.SelectedValue.ToString()) == "0")
                {
                    lbBookAmount.Visible = true;
                }
                else
                {
                    DataProcedure procedure = new DataProcedure();
                    DateTime theDate = DateTime.ParseExact(tbIssuance_Date.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    string dateToInsert = theDate.ToString("dd.MM.yyyy");
                    DateTime theDate2 = DateTime.ParseExact(tbReturn_Date.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    string dateToInsert2 = theDate2.ToString("dd.MM.yyyy");
                    procedure.OrderInsert(Convert.ToInt32(ddlBooks.SelectedValue.ToString()), dateToInsert, dateToInsert2, Convert.ToInt32(ddlStatus.SelectedValue.ToString()),
                        Convert.ToInt32(ddlReaders.SelectedValue.ToString()), DBConnection.idUser);
                    Cleaner();
                    ddlBooksFill();
                    lbBookAmount.Visible = false;
                    gvFill(QR);
                }
            }
        }
        //Обновление записи
        protected void btUpdate_Click(object sender, EventArgs e)
        {
            if (AmountCheck(ddlBooks.SelectedValue.ToString()) == "0" && ddlStatus.SelectedValue.ToString() != "2")
            {
                lbBookAmount.Visible = true;
            }
            else
            {
                DataProcedure procedure = new DataProcedure();
                DateTime theDate = DateTime.ParseExact(tbIssuance_Date.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                string dateToInsert = theDate.ToString("dd.MM.yyyy");
                DateTime theDate2 = DateTime.ParseExact(tbReturn_Date.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                string dateToInsert2 = theDate2.ToString("dd.MM.yyyy");
                procedure.OrderUpdate(DBConnection.idRecord, Convert.ToInt32(ddlBooks.SelectedValue.ToString()), tbIssuance_Date.Text.ToString(), tbReturn_Date.Text.ToString(),
                    Convert.ToInt32(ddlStatus.SelectedValue.ToString()), Convert.ToInt32(ddlReaders.SelectedValue.ToString()));
                Cleaner();
                SelectedMessage.Visible = false;
                lbBookAmount.Visible = false;
                DBConnection.idRecord = 0;
                ddlBooksFill();
                gvFill(QR);
            }
        }
        //Удаление записи
        protected void gvOrders_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Index = Convert.ToInt32(e.RowIndex);
            DataProcedure procedure = new DataProcedure();
            GridViewRow rows = gvOrders.SelectedRow;
            DBConnection.idRecord = Convert.ToInt32(gvOrders.Rows[Index].Cells[1].Text.ToString());
            procedure.OrderDelete(DBConnection.idRecord);
            ddlBooksFill();
            gvFill(QR);
        }
        //Поиск
        protected void btSearch_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                foreach (GridViewRow row in gvOrders.Rows)
                {
                    if (row.Cells[1].Text.Equals(tbSearch.Text) ||
                        row.Cells[3].Text.Equals(tbSearch.Text) ||
                        row.Cells[4].Text.Equals(tbSearch.Text) ||
                        row.Cells[5].Text.Equals(tbSearch.Text) ||
                        row.Cells[7].Text.Equals(tbSearch.Text) ||
                        row.Cells[9].Text.Equals(tbSearch.Text) ||
                        row.Cells[11].Text.Equals(tbSearch.Text))
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
                string newQR = QR + "where [ID] like '%" + tbSearch.Text + "%' or [Книга] like '%" + tbSearch.Text + "%' or [Дата выдачи] like '%" + tbSearch.Text + "%' or" +
                    "[Дата возврата] like '%" + tbSearch.Text + "%' or [Статус] like '%" + tbSearch.Text + "%' or [Номер билета] like '%" + tbSearch.Text + "%' or" +
                    "[Библиотекарь] like '%" + tbSearch.Text + "%'";
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
    }
}