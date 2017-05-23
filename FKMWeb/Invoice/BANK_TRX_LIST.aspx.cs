using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class Invoice_BANK_TRX_LIST : Page
{
    fkminvcom dbo = new fkminvcom();
    // Visual C# .NET requires that you override the OnInit function,
    // adding a new delegate for the Page_Load event.

    override protected void OnInit(System.EventArgs e)
    {
        this.Load += new System.EventHandler(this.Page_Load);
        base.OnInit(e);
        CMB_BTRXTYP.SelectedIndexChanged += new System.EventHandler(CMB_BTRXTYP_SelectedIndexChanged);
        CMB_BTRX_CURRCD.SelectedIndexChanged += new System.EventHandler(CMB_BTRX_CURRCD_SelectedIndexChanged);
        CMB_BTRX_DEBIT_NAME.SelectedIndexChanged += new System.EventHandler(CMB_BTRX_DEBIT_NAME_SelectedIndexChanged);
        TXT_BTRX_ISSDT.TextChanged += new System.EventHandler(TXT_BTRX_ISSDT_TextChanged);
        TXT_BTRX_VALUEDT.TextChanged += new System.EventHandler(TXT_BTRX_VALUEDT_TextChanged);
        CMB_BTRXCDTYP.SelectedIndexChanged += new System.EventHandler(CMB_BTRXCDTYP_SelectedIndexChanged);
        //UploadButton.Click += new System.EventHandler(UploadButton_Click1); 
        //GridView1.SelectedIndexChanged += new System.EventHandler(GridView1_SelectedIndexChanged);
        //GridView1.RowDeleting += new System.Web.UI.WebControls.GridViewDeleteEventHandler(Gridview1_RowDeleting);
        //GridView1.RowDataBound += new System.Web.UI.WebControls.GridViewRowEventHandler(GridView1_RowDataBound); 
        //CHBX_EDIT.CheckedChanged += new System.EventHandler(CHBX_EDIT_CheckedChanged);
        Button1.Click += new System.EventHandler(Button1_Click);

        BTN_CLEAR.Click += new System.EventHandler(BTN_CLEAR_Click);
    }

    public void Page_Load(object sender, System.EventArgs e)
    {

        if (!IsPostBack && !IsCallback)
        {


            if ((Request.QueryString.Get("REFSRL")) != null)
            {
                String REFCODE = Request.QueryString.Get("REFSRL");
                GET_REFSRL_DETAILS(ref REFCODE);
            }
            CLEAR_FIELDS();
            GET_GRID_INFO();
        }
    }

    protected void CLEAR_FIELDS()
    {
        CMB_BTRXCDTYP.SelectedIndex = 0;
        TXT_BTRX_ISSDT.Text = "";
        CMB_BTRX_DEBIT_NAME.SelectedIndex = -1;
        TXT_BTRX_VALUEDT.Text = "";
        CMB_BTRX_CURRCD.SelectedIndex = 0;

    }

    protected void BTN_CLEAR_Click(object sender, System.EventArgs e)
    {//Handles BTN_CLEAR.Click
        CLEAR_FIELDS();
        GET_GRID_INFO();
    }

    protected void GET_REFSRL_DETAILS(ref String CODE)
    {
        try
        {
            //  'Dim COMP_CD As String = Session("USER_COMP_CD")
            String RETRVQRY = "SELECT * FROM BANK_TRX WHERE BTRX_REFSRL = '" + CODE + "' ";
            DataTable dtinfo = dbo.SelTable(RETRVQRY);
            if (dtinfo.Rows.Count == 1)
            {
                dtinfo.Columns.Add("INVC_REFSRL");
                dtinfo.Columns.Add("INVC_ISSDT");
                dtinfo.Columns.Add("INVC_VALUEDT");
                dtinfo.Columns.Add("INVC_FKMREF");
                dtinfo.Columns.Add("INVC_TYPE");
                dtinfo.Columns.Add("INVC_REFDESC");
                dtinfo.Columns.Add("INVC_NARR");
                dtinfo.Columns.Add("INVC_CURRCD");
                dtinfo.Columns.Add("INVC_VALUEAMT");
                dtinfo.Columns.Add("INVC_DEBIT_NAME");
                dtinfo.Columns.Add("INVC_DEBIT_ACC");
                dtinfo.Columns.Add("INVC_CREDIT_NAME");
                dtinfo.Columns.Add("INVC_CREDIT_ACC");
                dtinfo.Columns.Add("ISSDT", typeof(DateTime));
                dtinfo.Columns.Add("VALUEDT", typeof(DateTime));
                dtinfo.Columns.Add("FKM_PROJNAME");
                dtinfo.Columns.Add("FKM_PRTCPDATE", typeof(DateTime));
                dtinfo.Columns.Add("FKM_MTRTDATE", typeof(DateTime));
                dtinfo.Columns.Add("FKM_COMMCAP", typeof(decimal));
                dtinfo.Columns.Add("FKM_INVAMT", typeof(decimal));
                dtinfo.Columns.Add("FKM_ANLYCAL", typeof(decimal));
                dtinfo.Columns.Add("FKM_ROI", typeof(decimal));
                dtinfo.Columns.Add("AMTINWORDS");
                dtinfo.Columns.Add("HEADER1");
                dtinfo.Columns.Add("FOOTER1");
                dtinfo.Columns.Add("INVC_TYPE_TEXT");
                dtinfo.Columns.Add("INVC_CURR");


                dtinfo.Rows[0]["INVC_REFSRL"] = dtinfo.Rows[0]["BTRX_REFSRL"];
                dtinfo.Rows[0]["INVC_FKMREF"] = dtinfo.Rows[0]["BTRX_FKMREF"];
                dtinfo.Rows[0]["INVC_TYPE"] = dtinfo.Rows[0]["BTRX_TYPE"];
                dtinfo.Rows[0]["INVC_REFDESC"] = dtinfo.Rows[0]["BTRX_REFDESC"];
                dtinfo.Rows[0]["INVC_NARR"] = dtinfo.Rows[0]["BTRX_NARR"];
                dtinfo.Rows[0]["INVC_VALUEAMT"] = dtinfo.Rows[0]["BTRX_VALUEAMT"];

                if (dtinfo.Rows[0]["BTRX_CD_TYPE"].ToString().Trim() == "CREDIT")
                {

                    dtinfo.Rows[0]["INVC_CREDIT_NAME"] = dtinfo.Rows[0]["BTRX_BANK_NAME"];
                    dtinfo.Rows[0]["INVC_CREDIT_ACC"] = dtinfo.Rows[0]["BTRX_BANK_ACC"];
                }
                else if (dtinfo.Rows[0]["BTRX_CD_TYPE"].ToString().Trim() == "DEBIT")
                {

                    dtinfo.Rows[0]["INVC_DEBIT_NAME"] = dtinfo.Rows[0]["BTRX_BANK_NAME"];
                    dtinfo.Rows[0]["INVC_DEBIT_ACC"] = dtinfo.Rows[0]["BTRX_BANK_ACC"];
                }

                dtinfo.Rows[0]["INVC_TYPE_TEXT"] = CMB_BTRXTYP.Items.FindByValue(dtinfo.Rows[0]["BTRX_TYPE"].ToString().Trim()).Text;
                dtinfo.Rows[0]["FOOTER1"] = User.Identity.Name.ToUpper();
                dtinfo.Rows[0]["ISSDT"] = DateTime.Parse(dtinfo.Rows[0]["BTRX_ISSDT"].ToString().Trim());
                dtinfo.Rows[0]["VALUEDT"] = DateTime.Parse(dtinfo.Rows[0]["BTRX_VALUEDT"].ToString().Trim()).ToShortDateString();
                Decimal VAL = Decimal.Parse(dtinfo.Rows[0]["BTRX_VALUEAMT"].ToString());

                if (dtinfo.Rows[0]["BTRX_CURRCD"].ToString().Trim() == "US DOLLAR")
                {
                    dtinfo.Rows[0]["INVC_CURR"] = "$";
                    // dtinfo.Rows[0]["AMTINWORDS"] = dbo.Amt2Word(VAL, "US DOLLAR", "CENTS", "U");
                }
                else if (dtinfo.Rows[0]["BTRX_CURRCD"].ToString().Trim() == "UK POUND")
                {
                    dtinfo.Rows[0]["INVC_CURR"] = "£";
                    // dtinfo.Rows[0]["AMTINWORDS"] = fkminvcom.Amt2Word(VAL, "UK POUND", "PENCE", "U");
                }
                else if (dtinfo.Rows[0]["BTRX_CURRCD"].ToString().Trim() == "EURO")
                {
                    dtinfo.Rows[0]["INVC_CURR"] = "€";
                    //dtinfo.Rows[0]["AMTINWORDS"] = fkminvcom.Amt2Word(VAL, "EURO", "CENTS", "U");
                }

                //dtinfo.Rows[0]["INVC_DEBIT_NAME"] = CMB_INVC_DEBIT_NAME.Items.FindByValue(dtinfo.Rows[0]["INVC_DEBIT_NAME"].ToString().Trim()).Text;
                //dtinfo.Rows[0]["INVC_CREDIT_NAME"] = CMB_INVC_DEBIT_NAME.Items.FindByValue(dtinfo.Rows[0]["INVC_CREDIT_NAME"].ToString().Trim()).Text;

                String REFQRY = "SELECT FKM_SRL ,FKM_PROJNAME,FKM_PRTCPDATE ,FKM_MTRTDATE,FKM_REMX_NAV AS FKM_REMX , " +
                                " FKM_COMMCAP ,   FKM_COMMCAP2 , FKM_ROI, " +
                                " FKM_INVAMT, FKM_ANLYCAL " +
                                " FROM INVEST_INFO " +
                                " WHERE FKM_SRL LIKE '" + dtinfo.Rows[0]["BTRX_FKMREF"].ToString().Trim() + "'   ";

                DataTable DTINVINFO = dbo.SelTable(REFQRY);
                if (DTINVINFO.Rows.Count == 1)
                {
                    dtinfo.Rows[0]["FKM_PROJNAME"] = DTINVINFO.Rows[0]["FKM_PROJNAME"].ToString().Trim();
                    dtinfo.Rows[0]["FKM_ROI"] = DTINVINFO.Rows[0]["FKM_ROI"].ToString().Trim();
                    dtinfo.Rows[0]["FKM_ANLYCAL"] = DTINVINFO.Rows[0]["FKM_ANLYCAL"].ToString().Trim();
                    dtinfo.Rows[0]["FKM_PRTCPDATE"] = DateTime.Parse(DTINVINFO.Rows[0]["FKM_PRTCPDATE"].ToString().Trim());

                    if (DTINVINFO.Rows[0]["FKM_MTRTDATE"].ToString().Trim().Length > 0)
                    {
                        dtinfo.Rows[0]["FKM_MTRTDATE"] = DateTime.Parse(DTINVINFO.Rows[0]["FKM_MTRTDATE"].ToString().Trim()).ToShortDateString();
                    }

                    dtinfo.Rows[0]["FKM_COMMCAP"] = DTINVINFO.Rows[0]["FKM_COMMCAP"].ToString().Trim();
                    dtinfo.Rows[0]["FKM_INVAMT"] = DTINVINFO.Rows[0]["FKM_INVAMT"].ToString().Trim();
                }


                ReportDocument rpt = new ReportDocument();
                String rname = Server.MapPath("~/Reports/INVOICETRX_RPT.rpt");
                String dwnldfname = Server.MapPath("~/ReportsGenerate/INVOICETRX_RPT.pdf");
                // ' Dim dt1 As DataTable = TryCast(Session("NAVRPT_dt_NAVRPT_REPORT"), DataTable)

                // 'if( dt.Rows.Count > 0 ){
                rpt.Load(rname);
                rpt.SetDataSource(dtinfo);

                //'rpt.PrintOptions.PaperSize = PaperSize.PaperA4
                //'rpt.PrintOptions.PaperOrientation = PaperOrientation.Landscape
                rpt.ExportToDisk(ExportFormatType.PortableDocFormat, dwnldfname);
                rpt.Close();
                rpt.Dispose();


                Response.ClearContent();
                Response.ClearHeaders();
                Response.AddHeader("content-disposition", "attachment;filename =" + "INVOICETRX_RPT.pdf");
                Response.ContentType = "application/pdf";
                Response.TransmitFile(dwnldfname);


            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }
    }

    protected void GET_GRID_INFO()
    {

        string BTRXTYPE = CMB_BTRXTYP.SelectedValue;
        string CURRCD = CMB_BTRX_CURRCD.SelectedValue;
        string FKMREF = "";
        string BNK_CODE = "";
        string FRMDT = "";
        string TODT = "";

        if (CMB_BTRXCDTYP.Text.Length > 0)
        {
            FKMREF = " AND BTRX_CD_TYPE LIKE '" + CMB_BTRXCDTYP.SelectedValue + "%' ";
        }

        if (CMB_BTRX_DEBIT_NAME.Text.Length > 0)
        {
            BNK_CODE = " AND BTRX_BANK_NAME LIKE '" + CMB_BTRX_DEBIT_NAME.SelectedItem.Text + "'  ";
        }


        if (TXT_BTRX_ISSDT.Text.Length > 0 && TXT_BTRX_VALUEDT.Text.Length > 0)
        {
            FRMDT = " AND (BTRX_ISSDT BETWEEN '" + TXT_BTRX_ISSDT.Text + "' AND   '" + TXT_BTRX_VALUEDT.Text + "') ";
        }
        else
        {
            if (TXT_BTRX_ISSDT.Text.Length > 0)
            {
                FRMDT = " AND BTRX_ISSDT = '" + TXT_BTRX_ISSDT.Text + "'   ";
            }
            else if (TXT_BTRX_VALUEDT.Text.Length > 0)
            {
                TODT = " AND BTRX_ISSDT = '" + TXT_BTRX_VALUEDT.Text + "'   ";
            }
        }


        try
        {
            //' Dim COMP_CD As String = Session("USER_COMP_CD")
            //'Dim RETRVQRY As String = "SELECT * FROM BANK_TRX WHERE BTRX_TYPE LIKE '" + BTRXTYPE + "' AND BTRX_CURRCD  LIKE '" + CURRCD + "'  " +
            //'                         FKMREF + BNK_CODE + FRMDT + TODT
            String RETRVQRY = "SELECT BANK_TRX.*,BNK_NAME,BNK_ACCOUNT_NO FROM BANK_TRX,BANK_INFO WHERE BTRX_TYPE LIKE '" + BTRXTYPE + "'   " +
                                    FKMREF + BNK_CODE + FRMDT + TODT + " AND BNK_REFSRL = BTRX_BANK_NAME ORDER BY BTRX_REFSRL DESC";

            DataTable dtinfo = dbo.SelTable(RETRVQRY);
            if (dtinfo.Rows.Count > 0)
            {
                dtinfo.Columns.Add("BTRX_VALUEAMTC");
                dtinfo.Columns.Add("BTRX_VALUEAMTD");


                foreach (DataRow DR in dtinfo.Rows)
                {
                    if (DR["BTRX_CD_TYPE"].ToString().Substring(0, 1) == "D")
                    {
                        DR["BTRX_VALUEAMTD"] = DR["BTRX_VALUEAMT"];
                    }
                    else if (DR["BTRX_CD_TYPE"].ToString().Substring(0, 1) == "C")
                    {
                        DR["BTRX_VALUEAMTC"] = DR["BTRX_VALUEAMT"];
                    }

                    DR["BTRX_BANK_NAME"] = DR["BNK_NAME"];
                    //DR["STATUS"] = CMB_BTRX_STATUS.Items.FindByValue(DR["BTRX_STATUS"].ToString().Trim()).Text;
                    //DR["FOOTER"] = User.Identity.Name.ToUpper();
                }

                GridView1.DataSource = dtinfo;
                GridView1.DataBind();
            }
            else
            {

                GridView1.DataSource = null;
                GridView1.DataBind();
            }
            Session["BANKLIST_dtINFO_REPORT"] = dtinfo;
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }
    }

    protected void CMB_BTRXTYP_SelectedIndexChanged(object sender, System.EventArgs e)
    { // Handles CMB_BTRXCDTYP.SelectedIndexChanged
        GET_GRID_INFO();
    }

    protected void CMB_BTRX_CURRCD_SelectedIndexChanged(object sender, System.EventArgs e)
    { // Handles CMB_BTRX_CURRCD.SelectedIndexChanged
        GET_GRID_INFO();
    }

    protected void CMB_BTRX_DEBIT_NAME_SelectedIndexChanged(object sender, System.EventArgs e)
    { // Handles CMB_BTRX_DEBIT_NAME.SelectedIndexChanged
        GET_GRID_INFO();
    }

    protected void TXT_BTRX_ISSDT_TextChanged(object sender, System.EventArgs e)
    { // Handles TXT_BTRX_ISSDT.TextChanged
        GET_GRID_INFO();
    }

    protected void TXT_BTRX_VALUEDT_TextChanged(object sender, System.EventArgs e)
    { // Handles TXT_BTRX_VALUEDT.TextChanged
        GET_GRID_INFO();
    }

    protected void CMB_BTRXCDTYP_SelectedIndexChanged(object sender, System.EventArgs e)
    { // Handles CMB_BTRXCDTYP.SelectedIndexChanged
        GET_GRID_INFO();
    }


    protected void GetSelectedRecords()
    {
        DataTable dt = (DataTable)Session["BANKLIST_dtINFO_REPORT"];


        ReportDocument rpt = new ReportDocument();
        String rname = Server.MapPath("~/Reports/BANKTRX_RPT.rpt");
        String dwnldfname = Server.MapPath("~/ReportsGenerate/BANKLIST_RPT.PDF");
        //'  Dim dt As DataTable = TryCast(Session("NAVRPT_dt_NAVRPT_REPORT"), DataTable)

        if (dt.Rows.Count > 0)
        {
            rpt.Load(rname);
            rpt.SetDataSource(dt);

            rpt.PrintOptions.PaperSize = PaperSize.PaperA4;
            rpt.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
            rpt.ExportToDisk(ExportFormatType.PortableDocFormat, dwnldfname);
            rpt.Close();
            rpt.Dispose();


            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("content-disposition", "attachment;filename =" + "BANKLIST_RPT.PDF");
            Response.ContentType = "application/pdf";
            Response.TransmitFile(dwnldfname);

        }
    }
    protected void Button1_Click(object sender, System.EventArgs e)
    { // Handles Button1.Click
        GetSelectedRecords();
    }

    protected void mbox(string val)
    {
        //string cs   = "c";
        //string cst  = "<script type=""text/javascript"">" + "alert('" + val + "');</" + "script>";
        //Page page    = HttpContext.Current.Handler;
        //page.ClientScript.RegisterStartupScript(Me.typeof, cs, cst);

        string USR = User.Identity.Name.ToUpper();
        dbo.ErrorLog(USR + " : BANK_TRX_LIST.aspx : " + val, Server.MapPath("~\\Logs\\ErrorLog"));
    }

}
