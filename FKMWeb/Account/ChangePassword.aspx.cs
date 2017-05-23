using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Account_ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {

    }
    protected void ChangePassword1_ChangingPassword(object sender, System.Web.UI.WebControls.LoginCancelEventArgs e)// Handles ChangeUserPassword.ChangingPassword
    {
        string qry;
        DataTable dt;
        fkminvcom dbo = new fkminvcom();
        string usrid = "";
        string passwdcurr;
        string passwdnew;
        usrid = ChangeUserPassword.UserName.ToUpper(); //'AicLog.UserName
        passwdnew = ChangeUserPassword.ConfirmNewPassword.ToString();//               'AicLog.Password
        //' = ChangePassword1.NewPassword
        passwdcurr = ChangeUserPassword.CurrentPassword.ToString();

        if (passwdnew.Length > 14 || passwdnew.Length < 6)
        {
            ChangeUserPassword.ChangePasswordFailureText = "Password Max/Min no of character is 14/6";
            //'Response.Redirect("~/Account/AicLogin.aspx?rstpass=TF1")
        }



        qry = "SELECT * from PSWD_INFO where PSWD_USERID = '" + usrid + "' and PSWD_PASSWORD = '" +
        passwdcurr + "' and  PSWD_STATUS = 'A'";

        dt = dbo.SelTable(qry);

        if (dt.Rows.Count > 0)
        {

            qry = "UPDATE PSWD_INFO " +
                  "SET PSWD_PASSWORD = '" + passwdnew + "', PSWD_EXPIRYDATE = '" + System.DateTime.Today.AddDays(100).ToString("dd/MM/yyyy") + "'  " +
                  "WHERE PSWD_USERID = '" + usrid + "'  and  PSWD_STATUS = 'A'  ";

            if (dbo.UpdTable(qry) == true)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            else
            {
                //'ChangePassword1.ChangePasswordFailureText = "Change password failed , Please retry again after some time!!!"
                Response.Redirect("~/Account/ChangePassword.aspx");
                //'Response.Redirect("~/Redirect.aspx?errorCode=WA002")
            }
        }
        else
        {
            //'ChangePassword1.ChangePasswordFailureText = "Change password failed 'DataBase error', Please retry again after some time!!!"
            Response.Redirect("~/Account/ChangePassword.aspx");
            //' Response.Redirect("~/Redirect.aspx?errorCode=WA001")
        }
    }
}
