using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.DynamicData;

namespace LibraryOnline.Pages
{
    public partial class HistoryPage : System.Web.UI.Page
    {
        private string QR = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            QR = DBConnection.qrHistory + "and [Authorization_Reader_ID] = '" + DBConnection.idUser + "'";
            if (!IsPostBack)
            {
                gvFill(QR);
            }
        }
        
        private void gvFill(string qr)
        {
            sdsHistory.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsHistory.SelectCommand = qr;
            sdsHistory.DataSourceMode = SqlDataSourceMode.DataReader;
            gvHistory.DataSource = sdsHistory;
            gvHistory.DataBind();
        }
        //Сортировка записей в таблице
        protected void gvHistory_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection sortDirection = SortDirection.Ascending;
            string strField = string.Empty;
            switch (e.SortExpression)
            {
                case ("Название книги"):
                    e.SortExpression = "[Book_Name]";
                    break;
                case ("Дата выдачи"):
                    e.SortExpression = "[Issuance_Date]";
                    break;
                case ("Дата возврата"):
                    e.SortExpression = "[Return_Date]";
                    break;
                case ("Статус"):
                    e.SortExpression = "[Status_Name]";
                    break;
            }
            sortGridView(gvHistory, e, out sortDirection, out strField);
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
        //Поиск
        protected void btSearch_Click(object sender, EventArgs e)
        {
            if(tbSearch.Text != "")
            {
                string newQR = QR + "and ([Book_Name] like '%" + tbSearch.Text + "%' or [Issuance_Date] like '%" + tbSearch.Text + "%' or " +
                "[Return_Date] like '%" + tbSearch.Text + "%' or [Status_Name] like '%" + tbSearch.Text + "%')";
                gvFill(newQR);
                btCansel.Visible = true;
            }
        }
        //Отмена поиска
        protected void btCansel_Click(object sender, EventArgs e)
        {
            tbSearch.Text = "";
            gvFill(QR);
            btCansel.Visible = false;
        }
    }
}