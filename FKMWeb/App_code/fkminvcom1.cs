using System;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Web;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.UI;
using System.Net;
using System.Net.Mail;
//using System.Web.UI.WebControls;
//using CrystalDecisions.CrystalReports;
//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.Shared;
public class fkminvcom1
{
    OdbcConnection fkmconn = new OdbcConnection("Dsn=FKMDB;");

    public fkminvcom1()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable SelTable(string Odbcstring)
    {
        DataTable dt = new DataTable();
        OdbcDataAdapter da = new OdbcDataAdapter();
        try
        {
            ErrorLog(Odbcstring, "C:\\src\\FKMWeb\\Logs\\OdbcQuery");
            da.SelectCommand = new OdbcCommand(Odbcstring, fkmconn);
            da.Fill(dt);
        }
        catch (Exception err)
        {
            mbox("Error : " + err.Message.ToString());
        }

        return (dt);
    }

    public bool InsRecord(string Odbcstring) // As DataTable\
    {
        return UpdTable(Odbcstring);
    }

    public bool DelRecord(string Odbcstring) // As DataTable\
    {
        return UpdTable(Odbcstring);
    }

    public bool UpdTable(string Odbcstring) // As DataTable\
    {

        OdbcCommand cmd1 = new OdbcCommand(Odbcstring);
        Boolean stat = false;
        try
        {

            ErrorLog(Odbcstring, "C:\\src\\FKMWeb\\Logs\\OdbcQuery");
            cmd1.Connection = fkmconn;
            if (fkmconn.State != ConnectionState.Open)
            {
                fkmconn.Open();
            }
            cmd1.ExecuteNonQuery();
            stat = true;
        }
        catch (Exception err)
        {
            stat = false;
            mbox("Error : " + err.Message.ToString());
        }
        finally
        {
            fkmconn.Close();
        }

        return stat;
    }

    public bool RecExist(string Odbcstring)
    {
        DataTable dt = new DataTable();
        OdbcDataAdapter da = new OdbcDataAdapter();

        try
        {
            ErrorLog(Odbcstring, "C:\\src\\FKMWeb\\Logs\\OdbcQuery");
            da.SelectCommand = new OdbcCommand(Odbcstring, fkmconn);
            da.Fill(dt);
        }
        catch (Exception err)
        {
            mbox("Error : " + err.Message.ToString());
        }

        if (dt.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Transact(string str1, string str2, string str3, string str4, string str5)
    {
        //'Use this function when dbops needs to be done within a single transaction
        //'Presently supports upto 5 db operations
        //'Params expected are the respective query strings for dbops

        OdbcCommand cmd1 = new OdbcCommand(str1);
        OdbcCommand cmd2 = new OdbcCommand(str2);
        OdbcCommand cmd3 = new OdbcCommand(str3);
        OdbcCommand cmd4 = new OdbcCommand(str4);
        OdbcCommand cmd5 = new OdbcCommand(str5);
        OdbcTransaction tran = null;

        cmd1.Connection = fkmconn;
        cmd2.Connection = fkmconn;
        cmd3.Connection = fkmconn;
        cmd4.Connection = fkmconn;
        cmd5.Connection = fkmconn;

        try
        {
            if (fkmconn.State != ConnectionState.Open)
            {
                fkmconn.Open();
            }
            tran = fkmconn.BeginTransaction();
            ErrorLog(str1, "C:\\src\\FKMWeb\\Logs\\OdbcQuery");
            cmd1.Transaction = tran;
            cmd1.ExecuteNonQuery();
            if (str2.Length > 0)
            {
                ErrorLog(str2, "C:\\src\\FKMWeb\\Logs\\OdbcQuery");
                cmd2.Transaction = tran;
                cmd2.ExecuteNonQuery();
            }
            if (str3.Length > 0)
            {
                ErrorLog(str3, "C:\\src\\FKMWeb\\Logs\\OdbcQuery");
                cmd3.Transaction = tran;
                cmd3.ExecuteNonQuery();
            }
            if (str4.Length > 0)
            {
                ErrorLog(str4, "C:\\src\\FKMWeb\\Logs\\OdbcQuery");
                cmd4.Transaction = tran;
                cmd4.ExecuteNonQuery();
            }
            if (str5.Length > 0)
            {
                ErrorLog(str5, "C:\\src\\FKMWeb\\Logs\\OdbcQuery");
                cmd5.Transaction = tran;
                cmd5.ExecuteNonQuery();
            }
            tran.Commit();
        }
        catch (Exception err)
        {
            tran.Rollback();
            // 'MsgBox(err.Message.ToString, MsgBoxStyle.OkOnly, "Error Occured..!!")
            mbox("Error : " + err.Message.ToString());
            return false;
        }
        finally
        {
            fkmconn.Close();
        }

        return true;
    }

    public bool IsAllDigits(string s)
    {
        foreach (char c in s)
        {
            if (!char.IsDigit(c))
                return false;
        }
        return true;
    }

    public string GET_COMP_CD(string FKM_SRL)
    {
        string COMP_CD = "001";
        string RETRVQRY = "SELECT COMP_CD  FROM INVEST_INFO  WHERE FKM_SRL = '" + FKM_SRL + "' ";
        DataTable dtinfo = SelTable(RETRVQRY);
        if (dtinfo.Rows.Count > 0)
        {
            COMP_CD = dtinfo.Rows[0][0].ToString();
        }
        return COMP_CD;
    }

    public object GET_FRM_COMP_CD(string COMP_CD, int TYPE)
    {
        string RTRN_STR = COMP_CD;

        if (TYPE == 1) //  ' NAME
        {
            if (COMP_CD == "001")
            {
                RTRN_STR = "FKMINVEST";
            }
            else if (COMP_CD == "002")
            {
                RTRN_STR = "FENKINVEST";
            }
            else if (COMP_CD == "003")
            {
                RTRN_STR = "PERSONAL";
            }
            else if (COMP_CD == "004")
            {
                RTRN_STR = "TECHSQUARE";
            }
        }

        else if (TYPE == 2) //   ' LOGO
        {
            if (COMP_CD == "001")
            {
                RTRN_STR = "~/images/fkmlogo.png";
            }
            else if (COMP_CD == "002")
            {
                RTRN_STR = "~/images/fenklogo.png";
            }
            else if (COMP_CD == "003")
            {
                RTRN_STR = "~/images/perslogo.png";
            }
            else if (COMP_CD == "004")
            {
                RTRN_STR = "~/images/perslogo.png";
            }
        }
        else if (TYPE == 3) //   ' COLOUR
        {
            if (COMP_CD == "001" || COMP_CD == "FKMINVEST")
            {
               return System.Drawing.Color.SteelBlue; //"#FF4682B4";
            }// 'Drawing.Color.SteelBlue '
            else if (COMP_CD == "002" || COMP_CD == "FENKINVEST")
            {
                return System.Drawing.Color.Crimson; //RTRN_STR = "#FFDC143C";
            }// 'Drawing.Color.Crimson  '
            else if (COMP_CD == "003" || COMP_CD == "PERSONAL")
            {
                return System.Drawing.Color.DarkViolet; //RTRN_STR = "#FF9400D3";
            }// 'Drawing.Color.DarkViolet  '
            else if (COMP_CD == "004" || COMP_CD == "TECHSQUARE")
            {
                return System.Drawing.Color.DarkGray; //RTRN_STR = "#FF9400D3";// 'Drawing.Color.DarkViolet  '
            }
            else
            {
                return System.Drawing.Color.Black; 
            }
        }


        return RTRN_STR;
    }

    public byte[] ReadFile(string sPath)
    {
        byte[] data;

        FileInfo fInfo = new FileInfo(sPath);
        long numBytes = fInfo.Length;

        FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);
        BinaryReader br = new BinaryReader(fStream);
        data = br.ReadBytes((int)numBytes);

        return data;
    }

    public void mbox(string val) // As DataTable\ 
    {
        // string cs     = "c";
        string cst = "<script >" + "alert('" + val + "');</" + "script>";
        //string cst = "<script type=""text/javascript"">" + "alert('" + val + "');</" + "script>";
        // Page page1    =  HttpContext.Current.Handler;
        //Page1.ClientScript.RegisterStartupScript(this.GetType, cs, cst);

        ErrorLog(val, "C:\\src\\FKMWeb\\Logs\\ErrorLog");
    }

    public void ErrorLog(string sErrMsg, string sPathName) // As DataTable\
    {


        //'sLogFormat used to create log files format :
        //' dd/mm/yyyy hh:mm:ss AM/PM ==> Log Message
        string sLogFormat = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> ";
        sErrMsg = sLogFormat + sErrMsg;

        //' "C:\src\FKMWeb\Logs\search"
        //'this variable used to create log filename format "
        //'for example filename : ErrorLogYYYYMMDD        
        string sErrorTime = DateTime.Now.ToString("yyyyMMdd");
        string fileLoc = sPathName + sErrorTime + ".txt";


        if (File.Exists(fileLoc))
        {
            //'Using sw As StreamWriter = new StreamWriter(fileLoc)
            //'    sw.Write("Some sample text for the file")
            //'End Using

            StreamWriter sw = new StreamWriter(fileLoc, true);
            sw.WriteLine(sErrMsg);
            sw.Flush();
            sw.Close();
        }
        else if (!File.Exists(fileLoc))
        {
            FileStream fs;
            fs = File.Create(fileLoc);
            fs.Close();

            StreamWriter sw = new StreamWriter(fileLoc, true);

            sw.WriteLine(sErrMsg);
            sw.Flush();
            sw.Close();
        }

    }

    public bool sendmail(string mailid, string msgsubj, string msgbody)// As Boolean
    {
        try
        {
            MailMessage msg = new MailMessage();
            SmtpClient client = new SmtpClient(); //  '("smtp.gmail.com")

            //'msg.From = New MailAddress("A@GMAIL.COM")
            msg.To.Add(mailid);
            msg.Bcc.Add("rohith.raghuram@hotmail.com");
            msg.Subject = msgsubj;
            msg.Body = msgbody;
            client.EnableSsl = true;
            client.Send(msg); 
        }
        catch (SmtpException ex)
        {
            mbox(ex.Message);
            return false;
        }
        return true;
    }

    public void send_NTF_mail(string ToMail, string msgsubj, string msgbody, string attchmentfile)
    { 
        MailMessage msg = new MailMessage();
        SmtpClient client = new SmtpClient();
        
        //Default port will be 25
        client.Port = 587;
        client.Host = "smtp.gmail.com";
        msg.From = new MailAddress("Notification.fkminvest@gmail.com", "FKM INVEST Support");

        //Create two views, one text, one HTML.
        System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(  msgbody, null, "text/html");
        //Add image to HTML version
        System.Net.Mail.LinkedResource imageResource1 = new System.Net.Mail.LinkedResource(System.Web.HttpContext.Current.Server.MapPath("~/images/fkmlogo_WBG.jpg"), System.Net.Mime.MediaTypeNames.Image.Jpeg);
        imageResource1.ContentId = "FKMHDIImage";
        htmlView.LinkedResources.Add(imageResource1);
        System.Net.Mail.LinkedResource imageResource2 = new System.Net.Mail.LinkedResource(System.Web.HttpContext.Current.Server.MapPath("~/images/fenk_wbg1.jpg"), System.Net.Mime.MediaTypeNames.Image.Jpeg);
        imageResource2.ContentId = "FENKHDIImage";
        htmlView.LinkedResources.Add(imageResource2);

        msg.To.Add(new MailAddress(ToMail));
       // msg.To.Add(new MailAddress("ROHITH.RAGHURAM@HOTMAIL.com"));
        msg.Bcc.Add(new MailAddress("ROHITH.RAGHURAM@HOTMAIL.com"));
        msg.IsBodyHtml = true;
        if (attchmentfile.Length != 0)
        {// more than one file ?? code it later 
            msg.Attachments.Add(new Attachment(attchmentfile));
        }
        client.UseDefaultCredentials = false;
        client.Credentials = new NetworkCredential("Notification.fkminvest@gmail.com", "FKMINV*123");
        client.EnableSsl = true;
        msg.IsBodyHtml = true;
        msg.Subject = msgsubj;

        msg.Body = msgbody.ToString();
try
        {
            client.Send(msg);
        }
        catch (Exception ex)
        {
            mbox("Error : " + ex.ToString());
            return;
        }
        msg.Dispose();
         
    }
}
