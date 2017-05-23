using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class MainPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["USER_LOGGED"] == null)
            {
                Session.Abandon();
                Response.Redirect("~/Account/Login.aspx");
            }
            //If Not IsNothing(Session("USER_MENU_CLR")) Then
            //    NavigationMenu.StaticMenuItemStyle.ForeColor = Session("USER_MENU_CLR")
            //End If
            //If Session("FKM_QUOTES") Is Nothing Then
            //    Call Getquotes()
            //Else
            //    LBL_QUOTE.Text = Session("FKM_QUOTES")
            //End If
            USER_NAME.Text = (string)Session["USER_NAME"];
            DESIGNATION.Text = (string)Session["USER_DESIGNATION"];
            EMPLOYEE_ID.Text = (string)Session["USER_EID"];
            user.ImageUrl = (string)Session["USER_PIC"];
            Image2.ImageUrl = (string)Session["USER_PIC"];
            if (Session["FKM_QUOTES"] == null)
            {
                Getquotes();
                LBL_QUOTE.Text = Session["FKM_QUOTES"].ToString();
            }
            else
            {
              LBL_QUOTE.Text = Session["FKM_QUOTES"].ToString();
            }

        }
    }
    protected void Getquotes()
    {
        fkminvcom dbo = new fkminvcom();
        String RETRVQRY = "SELECT * FROM FKM_REFRNC WHERE RF_FEILDTYPE  = 'QUOTE' ORDER BY NEWID()";
        String marquee2 = "         <marquee scrollamount='3' direction='right' width='40'>&gt;&gt;&gt;</marquee>  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  ";
        String marquee1 = "         <marquee scrollamount='3' width='40'>&lt;&lt;&lt;</marquee>   ";
        DataTable dtinfo = dbo.SelTable(RETRVQRY);
        foreach (DataRow dr in dtinfo.Rows)
        {
            //foreach (DataColumn column in dtinfo.Columns)
            //{
            //    Console.WriteLine(row[column]);
            //}
            Session["FKM_QUOTES"] = Session["FKM_QUOTES"] + marquee1 + dr["RF_DESCRP"].ToString().Trim() + marquee2;
        }

    }

    public void searchBtn_Click(object sender, System.EventArgs e) //Handles Button1.Click
    {
        string search_text = search.Text.ToString().ToUpper().Trim();
        Response.Redirect("~/Home/Home.aspx?SRCHTXT=" + search_text + " ", false);
    }
    public void btnLogout_Click(object sender, System.EventArgs e) //Handles Button1.Click
    {
        Session.Abandon();
        Response.Redirect("~/Account/Login.aspx");
    }
}
