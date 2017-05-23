using System.Data;
using System.IO;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class Tools_DOC_MTNC : Page
{
    fkminvcom dbo = new fkminvcom();

    // Visual C# .NET requires that you override the OnInit function,
    // adding a new delegate for the Page_Load event.

    override protected void OnInit(EventArgs e)
    {
        this.Load += new System.EventHandler(this.Page_Load);
        base.OnInit(e);
        CMB_DOC_SRL.SelectedIndexChanged += new System.EventHandler(CMB_DOC_SRL_SelectedIndexChanged);
       // CMB_FLDTYP.SelectedIndexChanged += new System.EventHandler(CMB_FLDTYP_SelectedIndexChanged);
        CHBX_EDIT.CheckedChanged += new System.EventHandler(CHBX_EDIT_CheckedChanged);
        GridView1.RowDeleting += new System.Web.UI.WebControls.GridViewDeleteEventHandler(Gridview1_RowDeleting);
        GridView1.SelectedIndexChanged += new System.EventHandler(GridView1_SelectedIndexChanged);
        TXT_SRCH.TextChanged += new System.EventHandler(TXT_SRCH_TextChanged);

        BTN_ADD.Click += new System.EventHandler(BTN_ADD_Click);
        BTN_UPDT.Click += new System.EventHandler(BTN_UPDT_Click);
        BTN_DELETE.Click += new System.EventHandler(BTN_DELETE_Click);
        BTN_CLEAR.Click += new System.EventHandler(BTN_CLEAR_Click);

        //TXT_OPNDATE.TextChanged += new System.EventHandler(TXT_OPNDATE_TextChanged); 
    }

    public void Page_Load(object sender, System.EventArgs e)
    {

        if((!IsPostBack && !IsCallback))
        {

            BTN_CLEAR_Click(sender, e);
           // ' CMB_FKM_CD.SelectedIndex = -1
              INIT_FKMCD();
              INIT_GRID();
            CHBX_EDIT.Checked = false;
            TXT_SRCH.Text = " ";
        }
    }
    
    protected void INIT_FKMCD()
    {
        // ' Dim COMP_CD As String = Session("USER_COMP_CD")
        String RETRVQRY = "SELECT DISTINCT FKM_SRL ,FKM_SRL + '-' + FKM_PROJNAME AS FKM_PROJNAME  FROM INVEST_INFO ORDER BY FKM_SRL";
        DataTable dtinfo = dbo.SelTable(RETRVQRY);
        CMB_FKM_CD.DataSource = dtinfo;
        CMB_FKM_CD.DataBind();
        CMB_FKM_CD.SelectedIndex = -1;
    }

    protected void INIT_DOC_SRL()
    {
        // ' Dim COMP_CD As String = Session("USER_COMP_CD")
        String RETRVQRY = "SELECT DISTINCT DOC_SRL  FROM FKM_DOC  ORDER BY DOC_SRL";
        DataTable dtinfo = dbo.SelTable(RETRVQRY);
        CMB_DOC_SRL.DataSource = dtinfo;
        CMB_DOC_SRL.DataBind();
        CMB_DOC_SRL.SelectedIndex = -1;
    }

    protected void INIT_GRID()
    {
        INIT_DOC_SRL();
        //    'Dim COMP_CD As String = Session("USER_COMP_CD")
        //    'Dim RETRVQRY As String = "SELECT *  FROM FKM_DOC WHERE ( DOC_SRL LIKE '%" + TXT_SRCH.Text.Trim + "%'  OR DOC_TYPE LIKE '%" + CMB_DOCTYP.SelectedValue + "%'  OR DOC_DESC LIKE '%" + TXT_SRCH.Text.Trim +
        //    '                         "%'  OR DOC_FKM_LNK LIKE '%" + CMB_FKM_CD.SelectedValue + "%' OR DOC_TYPE LIKE '%" + TXT_SRCH.Text.Trim + "%'  OR DOC_FKM_LNK LIKE '%" + TXT_SRCH.Text.Trim + "%'  ) ORDER BY DOC_SRL DESC"
        String RETRVQRY = "SELECT *  FROM FKM_DOC WHERE ( DOC_SRL LIKE '%" + TXT_SRCH.Text.Trim() + "%'    OR DOC_DESC LIKE '%" + TXT_SRCH.Text.Trim() +
                                 "%'   OR DOC_TYPE LIKE '%" + TXT_SRCH.Text.Trim() + "%'  OR DOC_FKM_LNK LIKE '%" + TXT_SRCH.Text.Trim() + "%'  ) ORDER BY DOC_SRL DESC";
        DataTable dtinfo = dbo.SelTable(RETRVQRY);

        Session["FKM_DOC_dt_FKM_DOC_info"] = dtinfo;
        GridView1.DataSource = dtinfo;
        GridView1.DataBind();
    }

    protected void BTN_CLEAR_Click(object sender, System.EventArgs e)
    { // Handles BTN_CLEAR.Click
                CLEAR();
    }

    protected void CLEAR()
    {
        if (CHBX_EDIT.Checked == true)
        {
            CMB_DOCTYP.SelectedIndex = -1;
            CMB_FKM_CD.SelectedIndex = -1;
            TXT_DESC.Text = "";
            TXT_REMX.Text = "";
            TXT_SRL.Text = "";
            TXT_SRL.Visible = false;
            BTN_ADD.Visible = false;
            CMB_DOC_SRL.SelectedIndex = -1;
            CMB_DOC_SRL.Visible = true;
            BTN_UPDT.Visible = true;
        }
        else
        {
            CMB_DOCTYP.SelectedIndex = -1;
            CMB_FKM_CD.SelectedIndex = -1;
            TXT_DESC.Text = "";
            TXT_REMX.Text = "";
            TXT_SRL.Text = NEXT_REC_SRL();
            TXT_SRL.Visible = true;
            BTN_ADD.Visible = true;
            CMB_DOC_SRL.SelectedIndex = -1;
            CMB_DOC_SRL.Visible = false;
            BTN_UPDT.Visible = false;

        }

        BTN_DELETE.Visible = false;
        INIT_GRID();
    }

    protected void CHBX_EDIT_CheckedChanged(object sender, System.EventArgs e)
    { // Handles CHBX_EDIT.CheckedChanged
        CLEAR();
    }

    protected void BTN_ADD_Click(object sender, System.EventArgs e)
    { // Handles BTN_ADD.Click
        if (CMB_FKM_CD.SelectedIndex < 0)
        {
            mbox("PLEASE SELECT THE PROJECT NAME !!");
            return;
        }
        if (CMB_DOCTYP.SelectedIndex < 0)
        {
            mbox("PLEASE SELECT THE DOCUMENT TYPE !!");
            return;
        }
        if (FileUploadControl.HasFile)
        {
            String USR = User.Identity.Name.ToUpper();
            String UPDDT = System.DateTime.Today.ToString("dd/MM/yyyy");
            String UPDTIME = System.DateTime.Now.ToString("hh:mm:ss");
            String CODE = TXT_SRL.Text.ToUpper();
            String FKMCODE = CMB_FKM_CD.SelectedItem.Value.ToString().ToUpper();
            String SRL = NEXT_REC_SRL();

            try
            {
                String RETRVQRY = "SELECT * FROM FKM_DOC WHERE DOC_SRL = '" + CODE + "'  ";
                if (dbo.RecExist(RETRVQRY) == false)
                {
                    bool qryflg = true;
                    if (CMB_DOCTYP.SelectedItem.Value.ToString().ToUpper().Trim() == "IRR CALC")
                    {
                        RETRVQRY = "SELECT * FROM FKM_DOC WHERE DOC_FKM_LNK = '" + CMB_FKM_CD.SelectedItem.Value.ToString().ToUpper() + "' AND DOC_TYPE = '" + CMB_DOCTYP.SelectedItem.Value.ToString().ToUpper() + "'  ";
                        if (dbo.RecExist(RETRVQRY) == false)
                        {
                            qryflg = true;
                        }
                        else
                        {
                            //'TXT_SRL.Text = NEXT_REC_SRL()
                            qryflg = false;
                            mbox("PLEASE TRY AGAIN .. ALREADY FILE EXIST FOR " + CMB_FKM_CD.SelectedItem.Value.ToString().ToUpper() + " : " + CMB_DOCTYP.SelectedItem.Value.ToUpper() + "!!");

                        }
                    }

                    if (qryflg == true)
                    {

                        String[] FLNME = FileUploadControl.FileName.Split('.');
                        //'Dim filename As String = Path.GetFileName(FileUploadControl.FileName)
                        String PATHFL = "~/Documents/" + SRL + "." + FLNME[1];
                        FileUploadControl.SaveAs(Server.MapPath("~/Documents/") + SRL + "." + FLNME[1]);
                        StatusLabel.Text = "Upload status: File uploaded!";

                        String INSQRY = "INSERT INTO FKM_DOC VALUES('" + SRL + "','" + FKMCODE + "','" + CMB_DOCTYP.Text.ToString().ToUpper() +
                                               "','" + TXT_DESC.Text.ToUpper() + "','" + PATHFL.ToUpper() + "','" + FLNME[0] + "','" + FLNME[1] + "','" + TXT_REMX.Text.ToUpper() +
                                               "','" + USR + "','" + UPDDT + "','" + UPDTIME + "')";

                        if (dbo.InsRecord(INSQRY) == false)
                        {
                            mbox("ERROR !!");
                        }
                        else
                        {
                            CLEAR();
                            INIT_DOC_SRL();
                            mbox("CODE : '" + CODE + "'      UPLOADED SUCCESSFULLY !!");
                        }

                    }
                }
                else
                {
                    TXT_SRL.Text = NEXT_REC_SRL();
                    mbox("PLEASE TRY AGAIN !!");
                }
            }
            catch (Exception EX)
            {
                mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
            }
        }
        else
        {
            mbox("PLEASE SELECT THE DOCUMENT TO UPLOAD !!");
        } 
    }

    protected String NEXT_REC_SRL() 
    {
        String SRL   = "";
        DataTable DTINVINF = new DataTable();
        try
        {
            String RETRVQRY   = "SELECT MAX (DOC_SRL) FROM FKM_DOC ";
            DTINVINF = dbo.SelTable(RETRVQRY);
            if(  (DTINVINF.Rows[0][0])!=null ){
                Double NSRL  = Double.Parse(DTINVINF.Rows[0][0].ToString());

                SRL = (NSRL + 1).ToString("0000000000");
            }else {
                SRL = "0000000001";
            }
    }
        catch (Exception EX   ){
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
    }
       return SRL;
    }

    protected void UploadButton_Click(object sender, System.EventArgs e)
    { //

        //    if( FileUploadControl.HasFile ){

        //        Try
        //            Dim filename As String = Path.GetFileName(FileUploadControl.FileName)
        //            ' FileUploadControl.SaveAs(Server.MapPath("~/Documents/") + filename)
        //            StatusLabel.Text = "Upload status: File uploaded!"
        //            if( dbo.sendmail("rohith.raghuram@hotmail.com", "fkm test", filename) = true ){
        //                StatusLabel.Text = "Upload status: mail sent!"
        //            }else {
        //                StatusLabel.Text = "Upload status: The mail could not be sent.!"
        //            }
        //            'Try
        //            '    Dim msg As New MailMessage()
        //            '    Dim client As New SmtpClient   '("smtp.gmail.com")

        //            '    'msg.From = New MailAddress("A@GMAIL.COM")
        //            '    msg.To.Add("rohith.raghuram@hotmail.com")
        //            '    msg.Subject = "fkm testing"
        //            '    msg.Body = filename
        //            '    client.EnableSsl = true
        //            '    client.Send(msg)

        //            'Catch ex As SmtpException

        //            '    mbox(ex.Message)
        //            'End Try
        //        Catch ex As Exception
        //            StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message
        //        End Try

        //    }

    }

    protected void BTN_UPDT_Click(object sender, System.EventArgs e)
    { // Handles BTN_UPDT.Click
        String USR = User.Identity.Name.ToUpper();
        String UPDDT = System.DateTime.Today.ToString("dd/MM/yyyy");
        String UPDTIME = System.DateTime.Now.ToString("hh:mm:ss");
        String CODE = CMB_DOC_SRL.SelectedValue.Trim();
        String FKMCODE = CMB_FKM_CD.SelectedItem.Value.ToString().ToUpper();
        //    ' Dim SRL As String = NEXT_REC_SRL()
        try
            {
                String RETRVQRY = "SELECT * FROM FKM_DOC WHERE DOC_SRL = '" + CODE + "'  ";
                if (dbo.RecExist(RETRVQRY) == true)
                {  
                        String UPDQRY  = "";
                        if( FileUploadControl.HasFile == true ){
                             String[] FLNME = FileUploadControl.FileName.Split('.');
                          //  'Dim filename As String = Path.GetFileName(FileUploadControl.FileName)
                             String PATHFL = "~/Documents/" + CODE + "." + FLNME[1];
                        FileUploadControl.SaveAs(Server.MapPath("~/Documents/") + CODE + "." + FLNME[1]);
                            StatusLabel.Text = "Upload status: File uploaded!";

                            UPDQRY = "UPDATE FKM_DOC SET DOC_TYPE = '" + CMB_DOCTYP.SelectedItem.Value.ToString().ToUpper() +
                                                   "', DOC_DESC  = '" + TXT_DESC.Text.ToUpper() + "',DOC_REMX = '" + TXT_REMX.Text.ToUpper() +
                                                   "', DOC_FKM_LNK = '" + CMB_FKM_CD.SelectedItem.Value.ToString().ToUpper() + "',DOC_PATH = '" + PATHFL.ToUpper() +
                                                   "', DOC_FILENAME = '" + FLNME[0] + "',DOC_EXTN = '" + FLNME[1] +
                                                   "',DOC_UPDBY = '" + USR + "',DOC_UPDDATE = '" + UPDDT + "',DOC_UPDTIME = '" + UPDTIME + "' " +
                                                   "WHERE DOC_SRL= '" + CODE + "'";
                        }else {
                            UPDQRY = "UPDATE FKM_DOC SET DOC_TYPE = '" + CMB_DOCTYP.SelectedItem.Value.ToString().ToUpper() +
                                                   "', DOC_DESC  = '" + TXT_DESC.Text.ToUpper() + "',DOC_REMX = '" + TXT_REMX.Text.ToUpper() +
                                                   "', DOC_FKM_LNK = '" + CMB_FKM_CD.SelectedItem.Value.ToString().ToUpper() +
                                                   "',DOC_UPDBY = '" + USR + "',DOC_UPDDATE = '" + UPDDT + "',DOC_UPDTIME = '" + UPDTIME + "' " +
                                                   "WHERE DOC_SRL= '" + CODE + "'";

                        }
                        if( dbo.InsRecord(UPDQRY) == false ){
                            mbox("ERROR !!");
                        }
                      else
                        {
                            CLEAR();
                            INIT_DOC_SRL();
                            mbox("CODE : '" + CODE + "'      UPLOADED SUCCESSFULLY !!");
                        }

                    } 
            }
            catch (Exception EX)
            {
                mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
            }  
    }

    protected void BTN_DELETE_Click(object sender, System.EventArgs e)
    { // Handles BTN_UPDT.Click 
        String CODE = CMB_DOC_SRL.SelectedValue.Trim();  
        try
        {
            String RETRVQRY = "SELECT * FROM FKM_DOC WHERE DOC_SRL = '" + CODE + "'  ";
            if (dbo.RecExist(RETRVQRY) == true)
            {
                String UPDQRY = "";
                
                    UPDQRY = "DELETE FROM FKM_DOC  WHERE DOC_SRL= '" + CODE + "'";

                 
                if (dbo.InsRecord(UPDQRY) == false)
                {
                    mbox("ERROR !!");
                }
                else
                {
                    CLEAR();
                    INIT_DOC_SRL();
                    mbox("CODE : '" + CODE + "'      UPLOADED SUCCESSFULLY !!");
                }

            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        } 
    }
    public void mbox(string val)
    {
        //string cs   = "c";
        //string cst  = "<script type=""text/javascript"">" + "alert('" + val + "');</" + "script>";
        //Page page    = HttpContext.Current.Handler;
        //page.ClientScript.RegisterStartupScript(Me.typeof, cs, cst);

        string USR = User.Identity.Name.ToUpper();
        dbo.ErrorLog(USR + " : DOC_MTNC.aspx : " + val, Server.MapPath("~\\Logs\\ErrorLog"));
    }

    protected void CMB_DOC_SRL_Changed(String CODE)
    {
        try
        {
            CHBX_EDIT.Checked = true;
            CLEAR();
            //String CODE = (string)GridView1.SelectedDataKey[0];

            String RETRVQRY = "SELECT * FROM FKM_DOC WHERE DOC_SRL = '" + CODE + "'  ";
            DataTable dtinfo = dbo.SelTable(RETRVQRY);
            if (dtinfo.Rows.Count == 1)
            {
                CMB_DOC_SRL.SelectedIndex = CMB_DOC_SRL.Items.IndexOf(CMB_DOC_SRL.Items.FindByValue(CODE));
                CMB_DOCTYP.SelectedIndex = CMB_DOCTYP.Items.IndexOf(CMB_DOCTYP.Items.FindByValue(dtinfo.Rows[0]["DOC_TYPE"].ToString()));
                CMB_FKM_CD.SelectedIndex = CMB_FKM_CD.Items.IndexOf(CMB_FKM_CD.Items.FindByValue(dtinfo.Rows[0]["DOC_FKM_LNK"].ToString()));
                TXT_DESC.Text = dtinfo.Rows[0]["DOC_DESC"].ToString().Trim();
                TXT_REMX.Text = dtinfo.Rows[0]["DOC_REMX"].ToString().Trim();
                TXT_SRL.Text = CODE;
            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }

    }

    protected void CMB_DOC_SRL_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        String CODE = CMB_DOC_SRL.SelectedValue;
        CMB_DOC_SRL_Changed(CODE);
    }

    //protected void BTNSRCH_Click(object sender, System.EventArgs e)
    //{
    //    // Handles BTNSRCH.Click
    //    INIT_GRID();
    //}

    protected void TXT_SRCH_TextChanged(object sender, System.EventArgs e)
    {
        INIT_GRID();
    }

    protected void GridView1_SelectedIndexChanged(object sender, System.EventArgs e)
    {        //Handles GridView1.SelectedIndexChanged

        if (GridView1.SelectedDataKey[0].ToString().Trim().Length != 0)
        {
            String CODE = (string)GridView1.SelectedDataKey[0];
            CMB_DOC_SRL_Changed(CODE);
        }
    }

    protected void Gridview1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = Convert.ToInt32(e.RowIndex);
        DataTable dt = Session["FKM_DOC_dt_FKM_DOC_info"] as DataTable;
        string CODE = dt.Rows[index]["DOC_SRL"].ToString();

        CHBX_EDIT.Checked = true;
        CMB_DOC_SRL_Changed(CODE);

        BTN_UPDT.Visible = false;
        BTN_DELETE.Visible = true;
    }

}
