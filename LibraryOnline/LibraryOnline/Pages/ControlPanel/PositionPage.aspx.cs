using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace LibraryOnline.Pages.ControlPanel
{
    public partial class PositionPage : System.Web.UI.Page
    {
        private string QR = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            QR = DBConnection.qrPosition;
            if (!IsPostBack)
            {
                gvFill(QR);
            }
        }
        private void gvFill(string qr)
        {
            sdsPosition.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsPosition.SelectCommand = qr;
            sdsPosition.DataSourceMode = SqlDataSourceMode.DataReader;
            gvPosition.DataSource = sdsPosition;
            gvPosition.DataBind();
        }
        //Сортировка записей в таблице
        protected void gvPosition_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection sortDirection = SortDirection.Ascending;
            string strField = string.Empty;
            switch (e.SortExpression)
            {
                case ("ID"):
                    e.SortExpression = "[ID_Position]";
                    break;
                case ("Название должности"):
                    e.SortExpression = "[Position_Name]";
                    break;
                case ("Pay"):
                    e.SortExpression = "[Зарплата]";
                    break;
            }
            sortGridView(gvPosition, e, out sortDirection, out strField);
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
            tbPositionName.Text = string.Empty;
            tbPay.Text = string.Empty;
        }
        //Добавление записей
        protected void btInsert_Click(object sender, EventArgs e)
        {
            DataProcedure procedure = new DataProcedure();
            procedure.PositionInsert(tbPositionName.Text.ToString(), Convert.ToDecimal(tbPay.Text.ToString()));
            Cleaner();
            gvFill(QR);
        }
        protected void gvPosition_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvPosition, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Нажмите, чтобы выбрать запись";
            }
        }
        //Выбор записи
        protected void gvPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvPosition.Rows)
            {
                if (row.RowIndex == gvPosition.SelectedIndex)
                {
                    row.ToolTip = string.Empty;
                }
                else
                {
                    row.ToolTip = "Нажмите, чтобы выбрать запись";
                }
            }
            GridViewRow rows = gvPosition.SelectedRow;
            DBConnection.idRecord = Convert.ToInt32(rows.Cells[1].Text.ToString());
            tbPositionName.Text = rows.Cells[2].Text.ToString();
            tbPay.Text = rows.Cells[3].Text.ToString();
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
        //Обновление записей
        protected void btUpdate_Click(object sender, EventArgs e)
        {
            DataProcedure procedure = new DataProcedure();
            procedure.PositionUpdate(DBConnection.idRecord, tbPositionName.Text.ToString(), Convert.ToDecimal(tbPay.Text.ToString()));
            Cleaner();
            gvFill(QR);
            btUpdate.Visible = false;
            SelectedMessage.Visible = false;
            DBConnection.idRecord = 0;
        }
        //Удаление строки
        protected void gvPosition_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Index = Convert.ToInt32(e.RowIndex);
            DataProcedure procedure = new DataProcedure();
            GridViewRow rows = gvPosition.SelectedRow;
            DBConnection.idRecord = Convert.ToInt32(gvPosition.Rows[Index].Cells[1].Text.ToString());
            procedure.PositionDelete(DBConnection.idRecord);
            gvFill(QR);
        }
        //Фильтрация
        protected void btFilter_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                string newQR = QR + "where [ID_Position] like '%" + tbSearch.Text + "%' or [Position_Name] like '%" + tbSearch.Text + "%'" +
                    "or [Pay] like '%" + tbSearch.Text + "%'";
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
                foreach (GridViewRow row in gvPosition.Rows)
                {
                    if (row.Cells[1].Text.Equals(tbSearch.Text) ||
                        row.Cells[2].Text.Equals(tbSearch.Text) ||
                        row.Cells[3].Text.Equals(tbSearch.Text) ||
                        row.Cells[4].Text.Equals(tbSearch.Text))
                        row.BackColor = ColorTranslator.FromHtml("#a1f2be");
                    else
                        row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                }
                btCancel.Visible = true;
            }
        }
    }
}