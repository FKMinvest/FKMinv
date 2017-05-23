using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class Invoice_INVOICE_LIST : Page
{
    fkminvcom dbo = new fkminvcom();
    //Visual C# .NET requires that you override the OnInit function,
    //adding a new delegate for the Page_Load event.

    override protected void OnInit(System.EventArgs e)
    {
        this.Load += new System.EventHandler(this.Page_Load);
        base.OnInit(e);
        CMB_INVCTYP.SelectedIndexChanged += new System.EventHandler(CMB_INVCTYP_SelectedIndexChanged);
        CMB_FKM_CD.SelectedIndexChanged += new System.EventHandler(CMB_FKM_CD_SelectedIndexChanged);
        CMB_INVC_CURRCD.SelectedIndexChanged += new System.EventHandler(CMB_INVC_CURRCD_SelectedIndexChanged);
        CMB_INVC_DEBIT_NAME.SelectedIndexChanged += new System.EventHandler(CMB_INVC_DEBIT_NAME_SelectedIndexChanged);
        TXT_INVC_ISSDT.TextChanged += new System.EventHandler(TXT_INVC_ISSDT_TextChanged);
        TXT_INVC_VALUEDT.TextChanged += new System.EventHandler(TXT_INVC_VALUEDT_TextChanged);
        //GridView1.SelectedIndexChanged += new System.EventHandler(GridView1_SelectedIndexChanged);
        // GridView1.RowDeleting += new System.Web.UI.WebControls.GridViewDeleteEventHandler(Gridview1_RowDeleting);
        //GridView1.RowDataBound += new System.Web.UI.WebControls.GridViewRowEventHandler(GridView1_RowDataBound); 
        // CHBX_EDIT.CheckedChanged += new System.EventHandler(CHBX_EDIT_CheckedChanged);
        //Button1.Click += new System.EventHandler(Button1_Click);

        BTN_CLEAR.Click += new System.EventHandler(BTN_CLEAR_Click);
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {

        if (!IsPostBack && !IsCallback)
        {
            CMB_INVC_DEBIT_NAME.DataBind();

            if ((Request.QueryString.Get("REFSRL")) != null)
            {
                String REFCODE = Request.QueryString.Get("REFSRL");
                GET_REFSRL_DETAILS(ref REFCODE);
            }
            else
            {
                INIT_FKMCD();
                GET_GRID_INFO();
            }
        }
    }


    protected void CLEAR_FIELDS()
    {

        CMB_FKM_CD.SelectedIndex = -1;
        TXT_INVC_ISSDT.Text = "";
        CMB_INVC_DEBIT_NAME.SelectedIndex = -1;
        TXT_INVC_VALUEDT.Text = "";
        CMB_INVC_CURRCD.SelectedIndex = 0;

    }

    protected void BTN_CLEAR_Click(object sender, System.EventArgs e)
    { // Handles BTN_CLEAR.Click
        CLEAR_FIELDS();
        // GET_GRID_INFO()
    }

    protected void INIT_FKMCD()
    {
        String RETRVQRY = "SELECT DISTINCT FKM_SRL ,FKM_SRL + '-' + FKM_PROJNAME AS FKM_PROJNAME  FROM INVEST_INFO ORDER BY FKM_SRL";
        DataTable dtinfo = dbo.SelTable(RETRVQRY);
        CMB_FKM_CD.DataSource = dtinfo;
        CMB_FKM_CD.DataBind();
        CMB_FKM_CD.SelectedIndex = -1;
    }

    protected void GET_REFSRL_DETAILS(ref  string CODE)
    {

        DataTable dt = new DataTable();
        dt.Columns.Add("INVC_REFSRL");
        dt.Columns.Add("INVC_ISSDT");
        dt.Columns.Add("INVC_VALUEDT");
        dt.Columns.Add("INVC_FKMREF");
        dt.Columns.Add("INVC_TYPE");
        dt.Columns.Add("INVC_REFDESC");
        dt.Columns.Add("INVC_NARR");
        dt.Columns.Add("INVC_CURRCD");
        dt.Columns.Add("INVC_VALUEAMT");
        dt.Columns.Add("INVC_DEBIT_NAME");
        dt.Columns.Add("INVC_DEBIT_ACC");
        dt.Columns.Add("INVC_CREDIT_NAME");
        dt.Columns.Add("INVC_CREDIT_ACC");
        dt.Columns.Add("INVC_CREATEDBY");
        dt.Columns.Add("INVC_UPDBY");
        dt.Columns.Add("INVC_UPDDATE");
        dt.Columns.Add("HEADER1");
        dt.Columns.Add("FOOTER1");

        try
        {
            //'Dim COMP_CD As String = Session("USER_COMP_CD")
            String RETRVQRY = "SELECT * FROM INVOICE_INFO WHERE INVC_REFSRL = '" + CODE + "' ";
            DataTable dtinfo = dbo.SelTable(RETRVQRY);
            if (dtinfo.Rows.Count == 1)
            {
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

                dtinfo.Rows[0]["INVC_TYPE_TEXT"] = CMB_INVCTYP.Items.FindByValue(dtinfo.Rows[0]["INVC_TYPE"].ToString().Trim()).Text;
                dtinfo.Rows[0]["FOOTER1"] = User.Identity.Name.ToUpper();
                //   'Dim DTE As DateTime = Date.Parse(dtinfo.Rows[0]["INVC_ISSDT"].ToString().Trim())
                dtinfo.Rows[0]["ISSDT"] = DateTime.Parse(dtinfo.Rows[0]["INVC_ISSDT"].ToString().Trim());
                //  ' dtinfo.Rows[0]["ISSDT"] = Date.Parse(dtinfo.Rows[0]["INVC_ISSDT"].ToString().Trim()).ToShortDateString
                dtinfo.Rows[0]["VALUEDT"] = DateTime.Parse(dtinfo.Rows[0]["INVC_VALUEDT"].ToString().Trim()).ToShortDateString();
                Decimal VAL = Decimal.Parse(dtinfo.Rows[0]["INVC_VALUEAMT"].ToString());

                if (dtinfo.Rows[0]["INVC_CURRCD"].ToString().Trim() == "US DOLLAR")
                {
                    dtinfo.Rows[0]["INVC_CURR"] = "$";
                    // dtinfo.Rows[0]["AMTINWORDS"] = dbo.Amt2Word(VAL, "US DOLLAR", "CENTS", "U");
                }
                else if (dtinfo.Rows[0]["INVC_CURRCD"].ToString().Trim() == "UK POUND")
                {
                    dtinfo.Rows[0]["INVC_CURR"] = "£";
                    // dtinfo.Rows[0]["AMTINWORDS"] = fkminvcom.Amt2Word(VAL, "UK POUND", "PENCE", "U");
                }
                else if (dtinfo.Rows[0]["INVC_CURRCD"].ToString().Trim() == "EURO")
                {
                    dtinfo.Rows[0]["INVC_CURR"] = "€";
                    //dtinfo.Rows[0]["AMTINWORDS"] = fkminvcom.Amt2Word(VAL, "EURO", "CENTS", "U");
                }

                dtinfo.Rows[0]["INVC_DEBIT_NAME"] = CMB_INVC_DEBIT_NAME.Items.FindByValue(dtinfo.Rows[0]["INVC_DEBIT_NAME"].ToString().Trim()).Text;
                dtinfo.Rows[0]["INVC_CREDIT_NAME"] = CMB_INVC_DEBIT_NAME.Items.FindByValue(dtinfo.Rows[0]["INVC_CREDIT_NAME"].ToString().Trim()).Text;

                String REFQRY = "SELECT FKM_SRL ,FKM_PROJNAME,FKM_PRTCPDATE ,FKM_MTRTDATE,FKM_REMX_NAV AS FKM_REMX , " +
                                " FKM_COMMCAP ,   FKM_COMMCAP2 , FKM_ROI, " +
                                " FKM_INVAMT, FKM_ANLYCAL " +
                                " FROM INVEST_INFO " +
                                " WHERE FKM_SRL LIKE '" + dtinfo.Rows[0]["INVC_FKMREF"].ToString().Trim() + "'   ";

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
                //'"FKM_CURR, FKM_INCGEN,FKM_INVGRP, " +
                //'"  FKM_CAPUNPD , FKM_CAPRFND," +
                //'" FKM_EXPNS, FKM_ROI, " +
                //'"  FKM_BOOKVAL, FKM_MONYCAL," +
                //'" FKM_QRTYCAL, FKM_SMANYCAL, " +
                //'"  FKM_ANLYCAL, FKM_SCNINCP," +
                //'" FKM_ANLINCMCY, FKM_ANLRLZD, " +
                //'"  FKM_ACTLINCMRU, FKM_UNRLYLD," +
                //'" FKM_DVDND, FKM_VALVTN, " +
                //'"  FKM_CAPGN, FKM_UNRLDVD," +
                //'" FKM_UNRLDVDCAP, FKM_FAIRVAL, " +
                //'"  FKM_NAV, FKM_SALEPRCD" +
                //'dr("INVC_REFDESC") = dtinfo.Rows[0]["INVC_REFDESC"].ToString().Trim()
                //'dr("INVC_ISSDT") = dtinfo.Rows[0]["INVC_ISSDT"].ToString().Trim()
                //'dr("INVC_DEBIT_NAME") = dtinfo.Rows[0]["INVC_DEBIT_NAME").ToString
                //'dr("INVC_DEBIT_ACC") = dtinfo.Rows[0]["INVC_DEBIT_ACC"].ToString().Trim()
                //'dr("INVC_CREDIT_NAME") = dtinfo.Rows[0]["INVC_CREDIT_NAME").ToString
                //'dr("INVC_CREDIT_ACC") = dtinfo.Rows[0]["INVC_CREDIT_ACC"].ToString().Trim()
                //'dr("INVC_CURRCD") = dtinfo.Rows[0]["INVC_CURRCD"].ToString().Trim()
                //'dr("INVC_VALUEDT") = dtinfo.Rows[0]["INVC_VALUEDT"].ToString().Trim()
                //'dr("INVC_VALUEAMT") = dtinfo.Rows[0]["INVC_VALUEAMT"].ToString().Trim()
                //'dr("INVC_NARR") = dtinfo.Rows[0]["INVC_NARR"].ToString().Trim()


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


                //'End if(

            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }
    }
    protected void GET_GRID_INFO()
    {
        String INVCTYPE = CMB_INVCTYP.SelectedValue;
        String CURRCD = CMB_INVC_CURRCD.SelectedValue;
        String FKMREF = "";
        String BNK_CODE = "";
        String FRMDT = "";
        String TODT = "";

        //  'CMB_INVC_DEBIT_NAME.DataBind()

        if (CMB_FKM_CD.Text.Length > 0)
        {
            FKMREF = " AND INVC_FKMREF LIKE '" + CMB_FKM_CD.SelectedValue + "' ";
        }

        if (CMB_INVC_DEBIT_NAME.Text.Length > 0)
        {
            BNK_CODE = " AND (INVC_DEBIT_NAME LIKE '" + CMB_INVC_DEBIT_NAME.SelectedItem.Value + "' OR INVC_CREDIT_NAME LIKE '" + CMB_INVC_DEBIT_NAME.SelectedItem.Value + "') ";
        }


        if (TXT_INVC_ISSDT.Text.Length > 0 && TXT_INVC_VALUEDT.Text.Length > 0)
        {
            FRMDT = " AND (INVC_ISSDT BETWEEN '" + TXT_INVC_ISSDT.Text + "' AND   '" + TXT_INVC_VALUEDT.Text + "') ";
        }
        else
        {
            if (TXT_INVC_ISSDT.Text.Length > 0)
            {
                FRMDT = " AND INVC_ISSDT = '" + TXT_INVC_ISSDT.Text + "'   ";
            }
            else
            {
            }
            if (TXT_INVC_VALUEDT.Text.Length > 0)
            {
                TODT = " AND INVC_ISSDT = '" + TXT_INVC_VALUEDT.Text + "'   ";
            }
            else
            {
            }
        }


        try
        {
            //' Dim COMP_CD As String = Session("USER_COMP_CD")
            String RETRVQRY = "SELECT * FROM INVOICE_INFO WHERE INVC_TYPE LIKE '" + INVCTYPE + "' AND INVC_CURRCD  LIKE '" + CURRCD + "'  " +
                                     FKMREF + BNK_CODE + FRMDT + TODT;
            DataTable dtinfo = dbo.SelTable(RETRVQRY);
            if (dtinfo.Rows.Count > 0)
            {
                foreach (DataRow DR in dtinfo.Rows)
                {
                    DR["INVC_DEBIT_NAME"] = CMB_INVC_DEBIT_NAME.Items.FindByValue(DR["INVC_DEBIT_NAME"].ToString().Trim()).Text;
                    DR["INVC_CREDIT_NAME"] = CMB_INVC_DEBIT_NAME.Items.FindByValue(DR["INVC_CREDIT_NAME"].ToString().Trim()).Text;
                }
                GridView1.DataSource = dtinfo;
                GridView1.DataBind();
            }
            else
            {

                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }
    }

    protected void CMB_INVCTYP_SelectedIndexChanged(object sender, System.EventArgs e)
    { // Handles CMB_INVCTYP.SelectedIndexChanged
        GET_GRID_INFO();
    }

    protected void CMB_INVC_CURRCD_SelectedIndexChanged(object sender, System.EventArgs e)
    { // Handles CMB_INVC_CURRCD.SelectedIndexChanged
        GET_GRID_INFO();
    }

    protected void CMB_INVC_DEBIT_NAME_SelectedIndexChanged(object sender, System.EventArgs e)
    { // Handles CMB_INVC_DEBIT_NAME.SelectedIndexChanged
        GET_GRID_INFO();
    }

    protected void TXT_INVC_ISSDT_TextChanged(object sender, System.EventArgs e)
    { // Handles TXT_INVC_ISSDT.TextChanged
        GET_GRID_INFO();
    }

    protected void TXT_INVC_VALUEDT_TextChanged(object sender, System.EventArgs e)
    { // Handles TXT_INVC_VALUEDT.TextChanged
        GET_GRID_INFO();
    }

    protected void CMB_FKM_CD_SelectedIndexChanged(object sender, System.EventArgs e)
    { // Handles CMB_FKM_CD.SelectedIndexChanged
        GET_GRID_INFO();
    }

    protected void mbox(string val)
    {
        //string cs   = "c";
        //string cst  = "<script type=""text/javascript"">" + "alert('" + val + "');</" + "script>";
        //Page page    = HttpContext.Current.Handler;
        //page.ClientScript.RegisterStartupScript(Me.typeof, cs, cst);

        string USR = User.Identity.Name.ToUpper();
        dbo.ErrorLog(USR + " : INVOICE_LIST.aspx : " + val, Server.MapPath("~\\Logs\\ErrorLog"));
    }
}
