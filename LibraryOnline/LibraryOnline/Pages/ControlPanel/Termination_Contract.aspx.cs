using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Configuration;
using System.IO;

namespace LibraryOnline.Pages.ControlPanel
{
    public partial class Termination_Contract : System.Web.UI.Page
    {
        private string QR = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            QR = DBConnection.qrTermination_Contract_List;
            if (!IsPostBack)
            {
                gvFill(QR);
                ddlContractFill();
            }
        }
        private void gvFill(string qr)
        {
            sdsContract.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsContract.SelectCommand = qr;
            sdsContract.DataSourceMode = SqlDataSourceMode.DataReader;
            gvTContract.DataSource = sdsContract;
            gvTContract.DataBind();
        }
        private void ddlContractFill()
        {
            sdsContractList.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsContractList.SelectCommand = DBConnection.qrContractList;
            sdsContractList.DataSourceMode = SqlDataSourceMode.DataReader;
            ddlContract.DataSource = sdsContractList;
            ddlContract.DataTextField = "Контракт";
            ddlContract.DataValueField = "Employee_Contract_ID";
            ddlContract.DataBind();
        }

        protected void gvTContract_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection sortDirection = SortDirection.Ascending;
            string strField = string.Empty;
            switch (e.SortExpression)
            {
                case ("ID"):
                    e.SortExpression = "[ID]";
                    break;
                case ("Сотрудник"):
                    e.SortExpression = "[Сотрудник]";
                    break;
                case ("Номер ТД"):
                    e.SortExpression = "[Номер ТД]";
                    break;
                case ("Номер приказа"):
                    e.SortExpression = "[Номер приказа]";
                    break;
                case ("Дата увольнения"):
                    e.SortExpression = "[Дата увольнения]";
                    break;
                case ("Причина"):
                    e.SortExpression = "[Причина]";
                    break;
            }
            sortGridView(gvTContract, e, out sortDirection, out strField);
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

        protected void gvTContract_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[7].Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvTContract, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Нажмите, чтобы выбрать запись";
            }
        }
        //Выбор записи
        protected void gvTContract_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvTContract.Rows)
            {
                if (row.RowIndex == gvTContract.SelectedIndex)
                {
                    row.ToolTip = string.Empty;
                }
                else
                {
                    row.ToolTip = "Нажмите, чтобы выбрать запись";
                }
            }
            GridViewRow rows = gvTContract.SelectedRow;
            DBConnection.idRecord = Convert.ToInt32(rows.Cells[1].Text.ToString());
            tbNumber.Text = rows.Cells[4].Text.ToString();
            tbData.Text = Convert.ToDateTime(rows.Cells[5].Text.ToString()).ToString("yyyy-MM-dd");
            tbReason.Text = rows.Cells[6].Text.ToString();
            ddlContract.SelectedValue = rows.Cells[7].Text.ToString();
            lblSelectedRow.Text = "Выбрана строка с ID " + DBConnection.idRecord;
            btUpdate.Visible = true;
            SelectedMessage.Visible = true;
        }
        //Очитска полей
        protected void Cleaner()
        {
            tbData.Text = string.Empty;
            tbNumber.Text = string.Empty;
            tbReason.Text = string.Empty;
            ddlContract.SelectedIndex = 0;
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

        protected void btInsert_Click(object sender, EventArgs e)
        {
            DateTime theDate = DateTime.ParseExact(tbData.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            string dateToInsert = theDate.ToString("dd.MM.yyyy");
            DataProcedure procedure = new DataProcedure();
            procedure.TerminationContractInsert(dateToInsert, tbReason.Text.ToString(), Convert.ToInt32(ddlContract.SelectedValue.ToString()));
            Cleaner();
            gvFill(QR);
        }

        protected void btUpdate_Click(object sender, EventArgs e)
        {
            DateTime theDate = DateTime.ParseExact(tbData.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            string dateToInsert = theDate.ToString("dd.MM.yyyy");
            DataProcedure procedure = new DataProcedure();
            procedure.TerminationContractUpdate(DBConnection.idRecord,dateToInsert, tbReason.Text.ToString(), Convert.ToInt32(ddlContract.SelectedValue.ToString()));
            Cleaner();
            gvFill(QR);
            btUpdate.Visible = false;
            SelectedMessage.Visible = false;
            DBConnection.idRecord = 0;
        }

        protected void gvTContract_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Index = Convert.ToInt32(e.RowIndex);
            DataProcedure procedure = new DataProcedure();
            GridViewRow rows = gvTContract.SelectedRow;
            DBConnection.idRecord = Convert.ToInt32(gvTContract.Rows[Index].Cells[1].Text.ToString());
            procedure.TerminationContracDelete(DBConnection.idRecord);
            gvFill(QR);
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
                foreach (GridViewRow row in gvTContract.Rows)
                {
                    if (row.Cells[1].Text.Equals(tbSearch.Text) ||
                        row.Cells[2].Text.Equals(tbSearch.Text) ||
                        row.Cells[3].Text.Equals(tbSearch.Text) ||
                        row.Cells[5].Text.Equals(tbSearch.Text) ||
                        row.Cells[6].Text.Equals(tbSearch.Text))
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
                string newQR = QR + "where [ID] like '%" + tbSearch.Text + "%' or [Сотрудник] like '%" + tbSearch.Text + "%' or" +
                    "[Номер ТД] like '%" + tbSearch.Text + "%' or [Номер приказа] like '%" + tbSearch.Text + "%' or [Дата увольнения] like '%" + tbSearch.Text + "%' or" +
                    "[Причина] like '%" + tbSearch.Text + "%'";
                gvFill(newQR);
                btCancel.Visible = true;
            }
        }
        //Экспорт в EXCEL
        protected void btXLS_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.AppendHeader("content-disposition", "attachment; filename=Termination_Contracts.xls");
            Response.ContentType = "appliction/excel";
            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
            gvTContract.RenderControl(htmlTextWriter);
            Response.Write(stringWriter.ToString());
            Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
        }
    }
}