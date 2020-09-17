using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Threading.Tasks.Dataflow;
using System.Data;

namespace LibraryOnline.Pages.ControlPanel
{
    public partial class BooksPage : System.Web.UI.Page
    {
        private string QR = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            QR = DBConnection.qrBooks;
            if (!IsPostBack)
            {
                gvFill(QR);
                ddlAuthorFill();
                ddlGenreFill();
            }
        }

        private void gvFill(string qr)
        {
            sdsBooks.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsBooks.SelectCommand = qr;
            sdsBooks.DataSourceMode = SqlDataSourceMode.DataReader;
            gvBooks.DataSource = sdsBooks;
            gvBooks.DataBind();
        }
        //Заполнение данными список авторов
        private void ddlAuthorFill()
        {
            sdsAuthor.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsAuthor.SelectCommand = DBConnection.qrAuthor;
            sdsAuthor.DataSourceMode = SqlDataSourceMode.DataReader;
            ddlAuthor.DataSource = sdsAuthor;
            ddlAuthor.DataTextField = "Автор";
            ddlAuthor.DataValueField = "ID_Author";
            ddlAuthor.DataBind();
        }

        //Заполнение данными список жанров
        private void ddlGenreFill()
        {
            sdsGenre.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsGenre.SelectCommand = DBConnection.qrGenre;
            sdsGenre.DataSourceMode = SqlDataSourceMode.DataReader;
            ddlGenre.DataSource = sdsGenre;
            ddlGenre.DataTextField = "Genre_Name";
            ddlGenre.DataValueField = "ID_Genre";
            ddlGenre.DataBind();
        }
        protected void gvBooks_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection sortDirection = SortDirection.Ascending;
            string strField = string.Empty;
            switch (e.SortExpression)
            {
                case ("ID"):
                    e.SortExpression = "ID_Book";
                    break;
                case ("Название книги"):
                    e.SortExpression = "Book_Name";
                    break;
                case ("Год"):
                    e.SortExpression = "Year";
                    break;
                case ("Количество"):
                    e.SortExpression = "Amount";
                    break;
                case ("Автор"):
                    e.SortExpression = "Автор";
                    break;
                case ("Жанр"):
                    e.SortExpression = "Genre_Name";
                    break;
            }
            sortGridView(gvBooks, e, out sortDirection, out strField);
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

        protected void gvBooks_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[7].Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvBooks, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Нажмите, чтобы выбрать запись";
            }
        }

        protected void gvBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvBooks.Rows)
            {
                if (row.RowIndex == gvBooks.SelectedIndex)
                {
                    row.ToolTip = string.Empty;
                }
                else
                {
                    row.ToolTip = "Нажмите, чтобы выбрать запись";
                }
            }
            int selectedRow = gvBooks.SelectedRow.RowIndex;
            GridViewRow rows = gvBooks.SelectedRow;
            DBConnection.idRecord = Convert.ToInt32(rows.Cells[1].Text.ToString());
            tbBookName.Text = rows.Cells[2].Text.ToString();
            tbYear.Text = rows.Cells[3].Text.ToString();
            tbAmount.Text = rows.Cells[9].Text.ToString();
            ddlAuthor.SelectedValue = rows.Cells[5].Text.ToString();
            ddlGenre.SelectedValue = rows.Cells[7].Text.ToString();
            SelectedMessage.Visible = true;
            lblSelectedRow.Text = "Выбрана строка с ID " + DBConnection.idRecord;
            btUpdate.Visible = true;
        }
        //Очитска полей
        protected void Cleaner()
        {
            tbAmount.Text = string.Empty;
            tbBookName.Text = string.Empty;
            tbYear.Text = string.Empty;
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
            if (flBookImage.PostedFile != null)
            {
                string imgfile = Path.GetFileName(flBookImage.PostedFile.FileName);
                flBookImage.SaveAs(Server.MapPath("../../Content/BooksImage/") + imgfile);
                DataProcedure procedure = new DataProcedure();
                procedure.BookInsert(tbBookName.Text.ToString(), tbYear.Text.ToString(), Convert.ToInt32(tbAmount.Text.ToString()), "../../Content/BooksImage/" + imgfile,
                    Convert.ToInt32(ddlGenre.SelectedValue), Convert.ToInt32(ddlAuthor.SelectedValue));
                Cleaner();
                gvFill(QR);
            }
        }

        protected void btUpdate_Click(object sender, EventArgs e)
        {
            if (flBookImage.PostedFile != null)
            {
                string imgfile = Path.GetFileName(flBookImage.PostedFile.FileName);
                flBookImage.SaveAs(Server.MapPath("../../Content/BooksImage/") + imgfile);
                DataProcedure procedure = new DataProcedure();
                procedure.Book_Update(DBConnection.idRecord, tbBookName.Text.ToString(), tbYear.Text.ToString(), Convert.ToInt32(tbAmount.Text.ToString()),
                    "../../Content/BooksImage/" + imgfile, Convert.ToInt32(ddlGenre.SelectedValue), Convert.ToInt32(ddlAuthor.SelectedValue));
                Cleaner();
                DBConnection.idRecord = 0;
                gvFill(QR);
            }
        }

        protected void gvBooks_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Index = Convert.ToInt32(e.RowIndex);
            DataProcedure procedure = new DataProcedure();
            GridViewRow rows = gvBooks.SelectedRow;
            DBConnection.idRecord = Convert.ToInt32(gvBooks.Rows[Index].Cells[1].Text.ToString());
            procedure.BookDelete(DBConnection.idRecord);
            gvFill(QR);
        }
        //Поиск
        protected void btSearch_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                foreach (GridViewRow row in gvBooks.Rows)
                {
                    if (row.Cells[1].Text.Equals(tbSearch.Text) ||
                        row.Cells[2].Text.Equals(tbSearch.Text) ||
                        row.Cells[3].Text.Equals(tbSearch.Text) ||
                        row.Cells[6].Text.Equals(tbSearch.Text) ||
                        row.Cells[8].Text.Equals(tbSearch.Text) ||
                        row.Cells[9].Text.Equals(tbSearch.Text))
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
                string newQR = QR + "where [ID_Book] like '%" + tbSearch.Text + "%' or [Book_Name] like '%" + tbSearch.Text + "%' or [Book_Name] like '%" + tbSearch.Text + "%' or" +
                    "[Year] like '%" + tbSearch.Text + "%' or [Name] like '%" + tbSearch.Text + "%' or [Surname] like '%" + tbSearch.Text + "%' or [Genre_Name] like '%" + tbSearch.Text + "%' or [Amount] like '%" + tbSearch.Text + "%'";
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