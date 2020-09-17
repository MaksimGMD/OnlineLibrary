using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Office.Core;

namespace LibraryOnline.Pages
{
    public partial class BooksList : System.Web.UI.Page
    {
        private string QR = ""; //Переменная для команд БД
        protected void Page_Load(object sender, EventArgs e)
        {
            QR = DBConnection.qrBook_List;
            if (!IsPostBack)
            {
                rpFill(QR);
                ddlAuthorFill();
                ddlGenreFill();
                ddlYearFill();
            }
        }
        //Заполнение данными список книг
        private void rpFill(string qr)
        {
            sdsBookList.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsBookList.SelectCommand = qr;
            sdsBookList.DataSourceMode = SqlDataSourceMode.DataReader;
            rpBooksList.DataSource = sdsBookList;
            rpBooksList.DataBind();
        }
        //Заполнение данными список авторов
        private void ddlAuthorFill()
        {
            sdsAuthor.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsAuthor.SelectCommand = DBConnection.qrAuthor;
            sdsAuthor.DataSourceMode = SqlDataSourceMode.DataReader;
            ddlAuthor.DataSource = sdsAuthor;
            ddlAuthor.DataTextField = "Автор";
            ddlAuthor.DataValueField = "Автор";
            ddlAuthor.DataBind();
            ddlAuthor.Items.Insert(0,"Авторы");
            ddlAuthor.Items[0].Value = "%";
        }
        //Заполнение данными список жанров
        private void ddlGenreFill()
        {
            sdsGenre.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsGenre.SelectCommand = DBConnection.qrGenre;
            sdsGenre.DataSourceMode = SqlDataSourceMode.DataReader;
            ddlGenre.DataSource = sdsGenre;
            ddlGenre.DataTextField = "Genre_Name";
            ddlGenre.DataValueField = "Genre_Name";
            ddlGenre.DataBind();
            ddlGenre.Items.Insert(0, "Жанры");
            ddlGenre.Items[0].Value = "%";
        }
        //Заполнение данными список годов выпуска
        private void ddlYearFill()
        {
            sdsYear.ConnectionString = DBConnection.connection.ConnectionString.ToString();
            sdsYear.SelectCommand = DBConnection.qrYear;
            sdsYear.DataSourceMode = SqlDataSourceMode.DataReader;
            ddlYear.DataSource = sdsYear;
            ddlYear.DataTextField = "Year";
            ddlYear.DataValueField = "Year";
            ddlYear.DataBind();
            ddlYear.Items.Insert(0, "Года");
            ddlYear.Items[0].Value = "%";
        }
        //Фильтр книг
        protected void btFilter_Click(object sender, EventArgs e)
        {
            string newQR = QR + "where [Author] like '%" + ddlAuthor.SelectedItem.Value + "%'" + 
                "and [Year] like '%" + ddlYear.SelectedItem.Value + "%' and [Genre_Name] like '%" + ddlGenre.SelectedItem.Value + "%'";
            rpFill(newQR);
        }
        //Отмена фильтрации
        public void btCancel_Click(object sender, EventArgs e)
        {
            ddlAuthor.SelectedIndex = 0;
            ddlGenre.SelectedIndex = 0;
            ddlYear.SelectedIndex = 0;
            rpFill(QR);
        }
        //Поиск книг
        protected void btSearch_Click(object sender, EventArgs e)
        {
            string newQR = QR + "where [Book_Name] like '%" + tbSearch.Text + "%' or [Author] like '%" + tbSearch.Text + "%' or [Genre_Name] like '%" + tbSearch.Text + "%'";
            rpFill(newQR);
        }
        //Добавить книгу к заказу
        protected void btInsertOrder_Click(object sender, EventArgs e)
        {

            if (DBConnection.idUser == 0)
            {
                Response.Redirect("../AuthorizationPage.aspx");
            }
            else
            {
                var btn = (Button)sender;
                //Get the repeater selected row
                var item = (RepeaterItem)btn.NamingContainer;
                //FInd the control from row
                var IdValue = ((Label)item.FindControl("lblID")).Text;
                DataProcedure procedure = new DataProcedure();
                procedure.UserOrderInsert(Convert.ToInt32(IdValue), 3, DBConnection.idUser);
            }
        }
    }
}