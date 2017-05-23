using System.Data;
using System.IO;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class Tools_SAFEV_EDIT : Page
{
    fkminvcom dbo = new fkminvcom();

    // Visual C# .NET requires that you override the OnInit function,
    // adding a new delegate for the Page_Load event.

    override protected void OnInit(EventArgs e)
    {
        this.Load += new System.EventHandler(this.Page_Load);
        base.OnInit(e);
        CMB_CODE.SelectedIndexChanged += new System.EventHandler(CMB_CODE_SelectedIndexChanged);
        // CMB_FLDTYP.SelectedIndexChanged += new System.EventHandler(CMB_FLDTYP_SelectedIndexChanged);
        CHBX_EDIT.CheckedChanged += new System.EventHandler(CHBX_EDIT_CheckedChanged);
        //GridView1.RowDeleting += new System.Web.UI.WebControls.GridViewDeleteEventHandler(Gridview1_RowDeleting);
        GridView1.SelectedIndexChanged += new System.EventHandler(GridView1_SelectedIndexChanged);
        TXT_SRCH.TextChanged += new System.EventHandler(TXT_SRCH_TextChanged);

        BTN_ADD.Click += new System.EventHandler(BTN_ADD_Click);
        BTN_UPDT.Click += new System.EventHandler(BTN_UPDT_Click);
        //BTN_DELETE.Click += new System.EventHandler(BTN_DELETE_Click);
        BTN_CLEAR.Click += new System.EventHandler(BTN_CLEAR_Click);

        //TXT_OPNDATE.TextChanged += new System.EventHandler(TXT_OPNDATE_TextChanged); 
    }

    public void Page_Load(object sender, System.EventArgs e)
    {

        if ((!IsPostBack && !IsCallback))
        {
            BTN_CLEAR_Click(sender, e);
            INIT_FKMCD();

            CHBX_EDIT.Checked = false;
            TXT_SRCH.Text = " ";
            CMB_CODE.DataBind();
            GridView1.DataBind();
        }
    }

    protected void BTN_CLEAR_Click(object sender, System.EventArgs e)
    { // Handles BTN_CLEAR.Click
        CLEAR();
    }

    protected void CLEAR()
    {
        if (CHBX_EDIT.Checked == true)
        {
            CMB_FKM_CD.SelectedIndex = -1;
            TXT_ISSUEDATE.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
            TXT_DESC.Text = "";
            TXT_CABNT.Text = "";
            TXT_SHELF.Text = "";
            TXT_CODE.Text = "";
            TXT_CODE.Visible = false;
            BTN_ADD.Visible = false;
            CMB_CODE.SelectedIndex = -1;
            CMB_CODE.Visible = true;
            BTN_UPDT.Visible = true;
        }
        else
        {
            CMB_FKM_CD.SelectedIndex = -1;
            TXT_ISSUEDATE.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
            TXT_DESC.Text = "";
            TXT_CABNT.Text = "";
            TXT_SHELF.Text = "";
            TXT_CODE.Text = NEXT_REC_SRL();
            TXT_CODE.Visible = true;
            BTN_ADD.Visible = true;
            CMB_CODE.SelectedIndex = -1;
            CMB_CODE.Visible = false;
            BTN_UPDT.Visible = false;

        }

        //  BTN_DELETE.Visible = false;
        INIT_GRID();
    }

    protected void CHBX_EDIT_CheckedChanged(object sender, System.EventArgs e)
    { // Handles CHBX_EDIT.CheckedChanged
        CLEAR();
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

    protected void BTN_ADD_Click(object sender, System.EventArgs e)
    { // Handles BTN_ADD.Click

        if (CMB_FKM_CD.SelectedIndex < 0)
        {
            mbox("PLEASE SELECT THE PROJECT NAME !!");
            return;
        }
        if (FileUploadControl.HasFile)
        {
            String USR = User.Identity.Name.ToUpper();
            String UPDDT = System.DateTime.Today.ToString("dd/MM/yyyy");
            String UPDTIME = System.DateTime.Now.ToString("hh:mm:ss");
            String CODE = TXT_CODE.Text.ToUpper();
            String FKMCODE = CMB_FKM_CD.SelectedItem.Value.ToString().ToUpper();
            String SRL = NEXT_REC_SRL();

            try
            {
                String RETRVQRY = "SELECT SV_SRL FROM SAFE_VAULT,FKM_DOC WHERE SV_SRL = '" + CODE + "' OR  DOC_SRL = '" + CODE + "' ";

                if (dbo.RecExist(RETRVQRY) == false)
                {



                    String[] FLNME = FileUploadControl.FileName.Split('.');
                    //'Dim filename As String = Path.GetFileName(FileUploadControl.FileName)
                    String PATHFL = "~/Documents/" + SRL + "." + FLNME[1];
                    FileUploadControl.SaveAs(Server.MapPath("~/Documents/") + SRL + "." + FLNME[1]);

                    String ISSDATE = "null";
                    if ((TXT_ISSUEDATE.Text.Trim().Length > 0))
                    {
                        String PDT = TXT_ISSUEDATE.Text;
                        ISSDATE = "'" + TXT_ISSUEDATE.Text + "'";
                    }

                    String INSQRY = "INSERT INTO SAFE_VAULT VALUES('" + SRL + "','" + FKMCODE + "','" + TXT_DESC.Text.ToString().ToUpper() +
                                           "', " + ISSDATE + ",'" + TXT_CABNT.Text.ToString().ToUpper() + "','" + TXT_SHELF.Text.ToString().ToUpper() +
                                           "','" + USR + "','" + UPDDT + "','" + UPDTIME + "')";

                    String INSQRYDOC = "INSERT INTO FKM_DOC VALUES('" + SRL + "','" + FKMCODE + "','SAFE DOCS','" + TXT_DESC.Text.ToUpper() +
                                                "','" + PATHFL.ToUpper() + "','" + FLNME[0] + "','" + FLNME[1] + "','" + TXT_DESC.Text.ToUpper() +
                                           "','" + USR + "','" + UPDDT + "','" + UPDTIME + "')";

                    if (dbo.Transact(INSQRY, INSQRYDOC, "", "", "") == false)
                    {
                        mbox("ERROR !!");
                    }
                    else
                    {
                        CLEAR();
                        CHBX_EDIT.Checked = false;
                        mbox("CODE : '" + CODE + "'   ADDED SUCCESSFULLY !!");
                    }
                }
                else
                {

                    TXT_CODE.Text = NEXT_REC_SRL();
                    mbox("CODE : '" + CODE + "'   ALREADY EXIST !!");
                }
            }
            catch (Exception EX)
            {
                mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
            }
        }
    }


    protected String NEXT_REC_SRL()
    {
        String SRL = "";
        DataTable DTINVINF = new DataTable();
        try
        {
            String RETRVQRY = "SELECT MAX (DOC_SRL) FROM FKM_DOC ";
            DTINVINF = dbo.SelTable(RETRVQRY);
            if ((DTINVINF.Rows[0][0]) != null)
            {
                Double NSRL = Double.Parse(DTINVINF.Rows[0][0].ToString());

                SRL = (NSRL + 1).ToString("0000000000");
            }
            else
            {
                SRL = "0000000001";
            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }
        return SRL;
    }

    protected void BTN_UPDT_Click(object sender, System.EventArgs e)
    {

        String USR = User.Identity.Name.ToUpper();
        String UPDDT = System.DateTime.Today.ToString("dd/MM/yyyy");
        String UPDTIME = System.DateTime.Now.ToString("hh:mm:ss");
        String CODE = CMB_CODE.Text.Trim();
        try
        {
            String RETRVQRY = "SELECT SV_SRL FROM SAFE_VAULT,FKM_DOC WHERE SV_SRL = '" + CODE + "' OR  DOC_SRL = '" + CODE + "' ";

            if (dbo.RecExist(RETRVQRY) == true)
            {


                String ISSDATE = "null";
                if ((TXT_ISSUEDATE.Text.Trim().Length > 0))
                {
                    String PDT = TXT_ISSUEDATE.Text;
                    ISSDATE = "'" + TXT_ISSUEDATE.Text + "'";
                }

                String UPDQRY = "UPDATE SAFE_VAULT SET  SV_DESC = '" + TXT_DESC.Text.ToUpper() + "',SV_DATE = " + ISSDATE +
                                       " , SV_FKM_LNK = '" + CMB_FKM_CD.SelectedItem.Value.ToString().ToUpper() +
                                       "', SV_CABNT = '" + TXT_CABNT.Text.ToString().ToUpper() + "',SV_SHELF = '" + TXT_SHELF.Text.ToString().ToUpper() +
                                       "', SV_UPDBY = '" + USR + "',SV_UPDDATE = '" + UPDDT + "',SV_UPDTIME = '" + UPDTIME + "' " +
                                       " WHERE SV_SRL= '" + CODE + "' ";

                String UPDQRYDOC = "";
                if (FileUploadControl.HasFile == true)
                {

                    String[] FLNME = FileUploadControl.FileName.Split('.');
                    //'Dim filename As String = Path.GetFileName(FileUploadControl.FileName)
                    String PATHFL = "~/Documents/" + CODE + "." + FLNME[1];
                    FileUploadControl.SaveAs(Server.MapPath("~/Documents/") + CODE + "." + FLNME[1]);

                    UPDQRYDOC = "UPDATE FKM_DOC SET DOC_TYPE = 'SAFE DOCS', DOC_DESC  = '" + TXT_DESC.Text.ToUpper() + "',DOC_REMX = '" + TXT_DESC.Text.ToUpper() +
                                           "', DOC_FKM_LNK = '" + CMB_FKM_CD.SelectedItem.Value.ToString().ToUpper() + "',DOC_PATH = '" + PATHFL.ToUpper() +
                                           "', DOC_FILENAME = '" + FLNME[0] + "',DOC_EXTN = '" + FLNME[1] +
                                           "',DOC_UPDBY = '" + USR + "',DOC_UPDDATE = '" + UPDDT + "',DOC_UPDTIME = '" + UPDTIME + "' " +
                                           "WHERE DOC_SRL= '" + CODE + "'";
                }
                else
                {
                    UPDQRYDOC = "UPDATE FKM_DOC SET DOC_TYPE = 'SAFE DOCS', DOC_DESC  = '" + TXT_DESC.Text.ToUpper() + "',DOC_REMX = '" + TXT_DESC.Text.ToUpper() +
                                           "', DOC_FKM_LNK = '" + CMB_FKM_CD.SelectedItem.Value.ToString().ToUpper() +
                                           "',DOC_UPDBY = '" + USR + "',DOC_UPDDATE = '" + UPDDT + "',DOC_UPDTIME = '" + UPDTIME + "' " +
                                           "WHERE DOC_SRL= '" + CODE + "'";

                }
                if (dbo.Transact(UPDQRY, UPDQRYDOC, "", "", "") == false)
                {
                    mbox("ERROR !!");
                }
                else
                {
                    CLEAR();
                    CMB_CODE.SelectedIndex = -1;

                    mbox("CODE : '" + CODE + "'   UPDATED SUCCESSFULLY !!");
                }

            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }
    }

    protected void CMB_CODE_Changed(String CODE)
    {
        try
        {
            String RETRVQRY = "SELECT * FROM SAFE_VAULT WHERE SV_SRL = '" + CODE + "'";
            DataTable dtinfo = dbo.SelTable(RETRVQRY);
            if (dtinfo.Rows.Count == 1)
            {
                CMB_FKM_CD.SelectedIndex = CMB_FKM_CD.Items.IndexOf(CMB_FKM_CD.Items.FindByValue(dtinfo.Rows[0]["SV_FKM_LNK"].ToString()));
                TXT_DESC.Text = dtinfo.Rows[0]["SV_DESC"].ToString().Trim();
                TXT_ISSUEDATE.Text = dtinfo.Rows[0]["SV_DATE"].ToString().Trim();
                TXT_CABNT.Text = dtinfo.Rows[0]["SV_CABNT"].ToString().Trim();
                TXT_SHELF.Text = dtinfo.Rows[0]["SV_SHELF"].ToString().Trim();
                TXT_CODE.Text = CODE;
            }
            else
            {
                CLEAR();
                mbox("ERROR !!");
            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }
    }

    protected void CMB_CODE_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        //Handles CMB_CODE.SelectedIndexChanged

        CHBX_EDIT.Checked = true;
        String CODE = CMB_CODE.SelectedValue;

    }

    protected void GridView1_SelectedIndexChanged(object sender, System.EventArgs e)
    {

        if (GridView1.SelectedDataKey[0].ToString().Trim().Length != 0)
        {

            CHBX_EDIT.Checked = true;
            String CODE = (string)GridView1.SelectedDataKey[0];
            CMB_CODE_Changed(CODE);
        }
    }

    protected void TXT_SRCH_TextChanged(object sender, System.EventArgs e)
    {
        INIT_GRID();
    }

    protected void INIT_GRID()
    {
        INIT_FKMCD();
        String RF_TXT = TXT_SRCH.Text.Trim().ToUpper();
        String RETRVQRY = "SELECT SAFE_VAULT.*,FKM_PROJNAME HEADER1,DOC_PATH FROM SAFE_VAULT,INVEST_INFO,FKM_DOC WHERE ((SV_SRL LIKE '%" + RF_TXT +
                "%') OR (SV_DESC LIKE '%" + RF_TXT + "%') OR (SV_DATE LIKE '%" + RF_TXT + "%') OR (SV_UPDBY LIKE '%" + RF_TXT +
                "%')) AND SV_FKM_LNK = FKM_SRL  AND SV_SRL = DOC_SRL order by  SV_SRL ";
        DataTable dtinfo = dbo.SelTable(RETRVQRY);

        Session["SAFE_VAULT_dt_SAFE_VAULT_info"] = dtinfo;
        GridView1.DataSource = dtinfo;
        GridView1.DataBind();
    }

    protected void Button1_Click(object sender, System.EventArgs e)
    {
        ReportDocument rpt = new ReportDocument();
        String rname = Server.MapPath("~/Reports/SAFE_VAULT_RPT.rpt");
        String dwnldfname = Server.MapPath("~/ReportsGenerate/SAFE_VAULT_REP.xls");
        DataTable dt = (DataTable)Session["SAFE_VAULT_dt_SAFE_VAULT_info"];
        rpt.Load(rname);
        rpt.SetDataSource(dt);

        rpt.ExportToDisk(ExportFormatType.Excel, dwnldfname);
        rpt.Close();
        rpt.Dispose();


        Response.ClearContent();
        Response.ClearHeaders();
        Response.AddHeader("content-disposition", "attachment;filename =" + "SAFEVAULTREP.xls");
        Response.ContentType = "application/ms-excel";
        Response.TransmitFile(dwnldfname);

    }

    public void mbox(string val)
    {
        //string cs   = "c";
        //string cst  = "<script type=""text/javascript"">" + "alert('" + val + "');</" + "script>";
        //Page page    = HttpContext.Current.Handler;
        //page.ClientScript.RegisterStartupScript(Me.typeof, cs, cst);

        string USR = User.Identity.Name.ToUpper();
        dbo.ErrorLog(USR + " : SAFEV_EDIT.aspx : " + val, Server.MapPath("~\\Logs\\ErrorLog"));
    }
}