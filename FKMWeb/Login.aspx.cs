using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data; 

public partial class Login : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["USER_LOGGED"] = null;
            Session["USER_COMP_CD"] = null;
            Session["USER_MENU_CLR"] = System.Drawing.Color.SteelBlue;
            Session["USER_LOGO_URL"] = "~/images/fkmlogo.png";
            Session["USER_MAIL_ID"] = "rohith.raghuram@hotmail.com";
        }
    }
     
    protected void LoginUser_Authenticate(Object sender, AuthenticateEventArgs e) //'Handles LoginUser.Authenticate
    {
        fkminvcom dbo = new fkminvcom();
        String qry = "SELECT * from PSWD_INFO where PSWD_USERID = '" + LoginUser.UserName.ToUpper() +
                        "' and PSWD_PASSWORD = '" + LoginUser.Password + "' and  PSWD_STATUS IN ('A','S')";


        DataTable dt = dbo.SelTable(qry);

        if (dt.Rows.Count > 0)
        {
            e.Authenticated = true;
        }
        else
        {
            e.Authenticated = false;
        }

    }
     
     protected void Getquotes()
     {
        Session["FKM_QUOTES"] = "";
        fkminvcom dbo = new fkminvcom();
        String RETRVQRY = "SELECT * FROM FKM_REFRNC WHERE RF_FEILDTYPE  = 'QUOTE' ORDER BY NEWID()";
        String marquee2    = "         <marquee scrollamount='3' direction='right' width='40'>&gt;&gt;&gt;</marquee> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;     ";
        String marquee1   = "         <marquee scrollamount='3' width='40'>&lt;&lt;&lt;</marquee>   ";
        DataTable dtinfo  = dbo.SelTable(RETRVQRY);
        foreach (DataRow dr in dtinfo.Rows)
        {  
            Session["FKM_QUOTES"] = Session["FKM_QUOTES"] + marquee1 + dr["RF_DESCRP"].ToString().Trim() + marquee2;
        }
          
     }

    protected void LoginUser_LoggedIn(Object sender, EventArgs e) //Handles LoginUser.LoggedIn
    {
        Session["USER_COMP_CD"] = TXT_COMP_CD.Text;
        //If Session("USER_COMP_CD") = "001" {
        Session["USER_LOGO_URL"] = "~/images/fkmlogo.png";
            Session["USER_MENU_CLR"] = System.Drawing.Color.SteelBlue;// '"#FF4682B4"
        //ElseIf Session("USER_COMP_CD") = "002" Then
         //   Session("USER_MENU_CLR") = System.Drawing.Color.Crimson  '"#FFDC143C"
        //    Session("USER_LOGO_URL") = "~/images/fenklogo.png"
        //ElseIf Session("USER_COMP_CD") = "003" Then
        //    Session("USER_MENU_CLR") = System.Drawing.Color.DarkViolet  '"#FF9400D3"
        //    Session("USER_LOGO_URL") = "~/images/perslogo.png"
        //End If
        Session["USER_LOGGED"] = LoginUser.UserName.ToUpper();
        Getquotes();

        fkminvcom dbo = new fkminvcom();
        String qry = "SELECT * from  USER_INFO  where USR_USERID = '" + LoginUser.UserName.ToUpper() + "'  ";
        DataTable dt = dbo.SelTable(qry);
        if (dt.Rows.Count > 0){
            if (dt.Rows[0]["USR_STATUS"].ToString() == "S")
        {
            Response.Redirect("~/Search.aspx");
        }
        else
        {
            Session["USER_NAME"] = dt.Rows[0]["USR_NAME"];
            Session["USER_DESIGNATION"] = dt.Rows[0]["USR_EMAILID"];
            Session["USER_EID"] = dt.Rows[0]["USR_USERID"];
            Session["USER_MAIL_ID"] = dt.Rows[0]["USR_EMAILID"];
            Session["USER_PIC"] = "~/Images/"+ LoginUser.UserName.ToUpper().Trim() + ".jpg";
            Response.Redirect("~/Home/Home.aspx");
        }
        }
        //'If LoginUser.UserName.ToUpper = "TESTER" Or LoginUser.UserName.ToUpper = "FKMINVEST" Or LoginUser.UserName.ToUpper = "FENKINVEST" Then
        //'Else
        //'End If
    }
}
