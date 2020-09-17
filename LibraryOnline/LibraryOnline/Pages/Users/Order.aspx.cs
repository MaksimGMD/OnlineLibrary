using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace LibraryOnline.Pages.Users
{
    public partial class Order : System.Web.UI.Page
    {
        private string QR = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            QR = DBConnection.qrOrder + "and [Authorization_Reader_ID] = '" + DBConnection.idUser + "'";
            if (!IsPostBack)
            {
                gvFill(QR);
            }
        }
        private void gvFill(string qr)
        {
            sdsOrder.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsOrder.SelectCommand = qr;
            sdsOrder.DataSourceMode = SqlDataSourceMode.DataReader;
            gvOrders.DataSource = sdsOrder;
            gvOrders.DataBind();
        }
        //Сортировка записей в таблице
        protected void gvOrders_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection sortDirection = SortDirection.Ascending;
            string strField = string.Empty;
            switch (e.SortExpression)
            {
                case ("Название книги"):
                    e.SortExpression = "[Book_Name]";
                    break;
                case ("Статус"):
                    e.SortExpression = "[Status_Name]";
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

        protected void gvOrders_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Index = Convert.ToInt32(e.RowIndex);
            DataProcedure procedure = new DataProcedure();
            DBConnection.idRecord = Convert.ToInt32(gvOrders.Rows[Index].Cells[1].Text.ToString());
            procedure.OrderDelete(DBConnection.idRecord);
            gvFill(QR);
        }

        protected void gvOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
        }
    }
}