using System.IO;
using System.Data;
using System.Web.UI;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Web.UI.WebControls;

public partial class MAIN_INV_TRX : Page
{
    fkminvcom dbo = new fkminvcom();

    // Visual C# .NET requires that you override the OnInit function,
    // adding a new delegate for the Page_Load event.

    override protected void OnInit(System.EventArgs e)
    {
        this.Load += new System.EventHandler(this.Page_Load);
        base.OnInit(e);

    }

    public void  Page_Load(object sender, System.EventArgs e)
    {

        if (!IsPostBack)
        {
            //'CMB_FKM_CD.DataBind()
            string FKMCODE = Request.QueryString.Get("FKMCODE");
            //'if( FKMCODE.Length > 0 ){            
            CMB_FKM_CD_Changed(FKMCODE);
            // 'End if(
        }

    }

    //public void CMB_FKM_CD_SelectedIndexChanged(object sender,  System.EventArgs e )// Handles CMB_FKM_CD.SelectedIndexChanged
    //{ 
    //    CMB_FKM_CD_Changed(CMB_FKM_CD.SelectedValue);
    //}


    public void CLEAR()
    {
        TXT_FKM_REF.Text = "";

        //'TXT_PROJNAME.Text = ""
        TXT_PRTCPDATE.Text = "";
        TXT_MTRTDATE.Text = "";
        TXT_BANK.Text = "";
        TXT_YLDPRD.Text = "";
        TXT_CURR.Text = "";
        TXT_COMMCAP.Text = "";
        TXT_COMMCAP2.Text = "";
        TXT_INVAMT.Text = "";

        TXT_OSAMT.Text = "";
        TXT_PAID.Text = "";
        TXT_ROI.Text = "";
        TXT_ACTLRTN.Text = "";
        TXT_ANNLRTN.Text = "";


        Button1.Visible = false;
        Button2.Visible = false;
        DownloadButton.NavigateUrl = "";

        Button2.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
    }

    public void CMB_FKM_CD_Changed(string CODE)
    {
        CLEAR();
        try
        {

            string RETRVQRY = "SELECT * FROM INVEST_INFO WHERE FKM_SRL = '" + CODE + "' ";//'AND COMP_CD = '" + COMP_CD + "'  ";
            DataTable dtinfo = dbo.SelTable(RETRVQRY);
            string CURRSYMB = "$";

            if (dtinfo.Rows.Count == 1)
            {

                if (dtinfo.Rows[0]["FKM_CURR"].ToString().Trim() == "US DOLLAR")
                {
                    CURRSYMB = "$";
                }
                else if (dtinfo.Rows[0]["FKM_CURR"].ToString().Trim() == "UK POUND")
                {
                    CURRSYMB = "£";
                }
                else if (dtinfo.Rows[0]["FKM_CURR"].ToString().Trim() == "EURO")
                {
                    CURRSYMB = "€";
                }
                //' Image1.ImageUrl = (string)dbo.GET_FRM_COMP_CD(dtinfo.Rows[0]["COMP_CD"].ToString().Trim(), 2)
                //'if( dtinfo.Rows[0]["COMP_CD"].ToString().Trim() = "001" ){
                //'    Image1.ImageUrl = "~/images/fkmlogo.png"
                //'else if( dtinfo.Rows[0]["COMP_CD"].ToString().Trim() = "002" ){
                //'    Image1.ImageUrl = "~/images/fenklogo.png"
                //'else if( dtinfo.Rows[0]["COMP_CD"].ToString().Trim() = "003" ){
                //'    Image1.ImageUrl = "~/images/perslogo.png"
                //'End if(


                TXT_FKM_REF.Text = CODE;
                //' CMB_FKM_CD.SelectedIndex = CMB_FKM_CD.Items.IndexOf(CMB_FKM_CD.Items.FindByValue(dtinfo.Rows[0]["FKM_SRL"].ToString().Trim()))
                //'TXT_PROJNAME.Text = dtinfo.Rows[0]["FKM_PROJNAME"].ToString().Trim();
                PROJECTNAME.Text = dtinfo.Rows[0]["FKM_PROJNAME"].ToString().Trim();
                TXT_PRTCPDATE.Text = dtinfo.Rows[0]["FKM_PRTCPDATE"].ToString().Trim();
                TXT_MTRTDATE.Text = dtinfo.Rows[0]["FKM_MTRTDATE"].ToString().Trim();
                TXT_BANK.Text = dtinfo.Rows[0]["FKM_BANK"].ToString().Trim();

                TXT_CURR.Text = dtinfo.Rows[0]["FKM_CURR"].ToString().Trim();


                TXT_COMMCAP.Text = CURRSYMB + " " + string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_COMMCAP"]);
                //'TXT_COMMCAP2.Text = dtinfo.Rows[0]["FKM_COMMCAP2"].ToString().Trim()
                TXT_PAID.Text = CURRSYMB + " " + string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_CAPPD"]);
                TXT_OSAMT.Text = CURRSYMB + " " + string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_CAPUNPD"]);
                TXT_INVAMT.Text = CURRSYMB + " " + string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_INVAMT"]);
                TXT_ROI.Text = dtinfo.Rows[0]["FKM_ROI"].ToString().Trim() + " %";
                TXT_ACTLRTN.Text = CURRSYMB + " " + string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_SCNINCP"]);
                //'   TXT_ANNLRTN.Text = dtinfo.Rows[0]["FKM_SCNINCP"].ToString().Trim()



                string STR_TEMP = dtinfo.Rows[0]["FKM_COMMCAP"].ToString().Trim();
                decimal COMMCAP = decimal.Parse(STR_TEMP);

                STR_TEMP = dtinfo.Rows[0]["FKM_COMMCAP2"].ToString().Trim();
                decimal COMMCAP2 = decimal.Parse(STR_TEMP);

                if (COMMCAP <= 0)
                {
                    COMMCAP = COMMCAP2;
                }

                STR_TEMP = dtinfo.Rows[0]["FKM_ROI"].ToString().Trim();
                decimal ROI = decimal.Parse(STR_TEMP);

                TXT_ANNLRTN.Text = CURRSYMB + " " + string.Format("{0:#,##0.00}", (COMMCAP * (ROI * (decimal)(0.01))));

                if (dtinfo.Rows[0]["FKM_YEILDPRD"].ToString().Trim() == "M")
                {
                    TXT_YLDPRD.Text = "MONTHLY";
                    //'TXT_INCOME.Text = dtinfo.Rows[0]["FKM_MONYCAL"].ToString().Trim()
                }
                else if (dtinfo.Rows[0]["FKM_YEILDPRD"].ToString().Trim() == "Q")
                {
                    TXT_YLDPRD.Text = "QUARTERLY";
                    // 'TXT_INCOME.Text = dtinfo.Rows[0]["FKM_QRTYCAL").ToString().Trim(
                }
                else if (dtinfo.Rows[0]["FKM_YEILDPRD"].ToString().Trim() == "S")
                {
                    TXT_YLDPRD.Text = "SEMI ANNUALY";
                    //  'TXT_INCOME.Text = dtinfo.Rows[0]["FKM_SMANYCAL"].ToString().Trim()
                }

                string sSQLSting = "SELECT  TRX_REFSRL,  TRX_ISSUEDATE, TRX_INVAMT,TRX_EXPNSAMT, TRX_AMT, TRX_ROI, TRX_REMX,TRX_UPDBY " +
                                          " FROM INVEST_TRX WHERE TRX_FKM_SRL = '" + CODE + "' ";//'AND COMP_CD = '" + COMP_CD + "'  ";
                DataTable INV_TRX_dtGRID = dbo.SelTable(sSQLSting);

                if (INV_TRX_dtGRID.Rows.Count > 0)
                {
                    INV_TRX_dtGRID.Columns.Add("COMP_CD", typeof(string));
                    INV_TRX_dtGRID.Columns.Add("FKM_REG", typeof(string));
                    INV_TRX_dtGRID.Columns.Add("LOGO", typeof(byte[]));
                    INV_TRX_dtGRID.Columns.Add("FKM_SRL", typeof(string));
                    INV_TRX_dtGRID.Columns.Add("FKM_PROJNAME", typeof(string));
                    INV_TRX_dtGRID.Columns.Add("FKM_INVCOMP", typeof(string));
                    INV_TRX_dtGRID.Columns.Add("FKM_PRTCPDATE", typeof(string));
                    INV_TRX_dtGRID.Columns.Add("FKM_MTRTDATE", typeof(string));
                    INV_TRX_dtGRID.Columns.Add("FKM_BANK", typeof(string));
                    INV_TRX_dtGRID.Columns.Add("FKM_CURR", typeof(string));

                    INV_TRX_dtGRID.Columns.Add("FKM_COMMCAP", typeof(decimal));
                    INV_TRX_dtGRID.Columns.Add("FKM_COMMCAP2", typeof(decimal));
                    INV_TRX_dtGRID.Columns.Add("FKM_INVAMT", typeof(decimal));
                    INV_TRX_dtGRID.Columns.Add("FKM_CAPPD", typeof(decimal));
                    INV_TRX_dtGRID.Columns.Add("FKM_CAPUNPD", typeof(decimal));
                    INV_TRX_dtGRID.Columns.Add("FKM_CAPRFND", typeof(decimal));
                    INV_TRX_dtGRID.Columns.Add("FKM_EXPNS", typeof(decimal));
                    INV_TRX_dtGRID.Columns.Add("FKM_ROI", typeof(decimal));
                    INV_TRX_dtGRID.Columns.Add("FKM_BOOKVAL", typeof(decimal));

                    INV_TRX_dtGRID.Columns.Add("HEADER1", typeof(string));
                    INV_TRX_dtGRID.Columns.Add("FKM_CURR_SMBL", typeof(string));
                    INV_TRX_dtGRID.Columns.Add("FOOTER", typeof(string));

                    INV_TRX_dtGRID.Columns.Add("TRX_AMT_R");
                    INV_TRX_dtGRID.Columns.Add("TRX_INVAMT_R");
                    INV_TRX_dtGRID.Columns.Add("TRX_EXPNSAMT_R");
                    INV_TRX_dtGRID.Columns.Add("TRX_ROI_R");

                    foreach (DataRow dr in INV_TRX_dtGRID.Rows)
                    {
                        dr["TRX_AMT_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", dr["TRX_AMT"]);
                        dr["TRX_INVAMT_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", dr["TRX_INVAMT"]);
                        dr["TRX_EXPNSAMT_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", dr["TRX_EXPNSAMT"]);

                        dr["TRX_ROI_R"] = dr["TRX_ROI"].ToString().Trim() + " %";


                        dr["COMP_CD"] = dtinfo.Rows[0]["COMP_CD"].ToString().Trim();
                        dr["FKM_SRL"] = CODE;
                        dr["FKM_PROJNAME"] = dtinfo.Rows[0]["FKM_PROJNAME"].ToString().Trim();
                        dr["FKM_INVCOMP"] = dtinfo.Rows[0]["FKM_INVCOMP"].ToString().Trim();
                        dr["FKM_PRTCPDATE"] = dtinfo.Rows[0]["FKM_PRTCPDATE"].ToString().Trim();
                        dr["FKM_MTRTDATE"] = dtinfo.Rows[0]["FKM_MTRTDATE"].ToString().Trim();
                        dr["FKM_BANK"] = dtinfo.Rows[0]["FKM_BANK"].ToString().Trim();
                        dr["FKM_CURR"] = dtinfo.Rows[0]["FKM_CURR"].ToString().Trim();

                        dr["FKM_COMMCAP"] = dtinfo.Rows[0]["FKM_COMMCAP"].ToString().Trim();
                        dr["FKM_COMMCAP2"] = dtinfo.Rows[0]["FKM_COMMCAP2"].ToString().Trim();
                        dr["FKM_INVAMT"] = dtinfo.Rows[0]["FKM_INVAMT"].ToString().Trim();
                        dr["FKM_CAPPD"] = dtinfo.Rows[0]["FKM_CAPPD"].ToString().Trim();
                        dr["FKM_CAPUNPD"] = dtinfo.Rows[0]["FKM_CAPUNPD"].ToString().Trim();
                        dr["FKM_CAPRFND"] = dtinfo.Rows[0]["FKM_CAPRFND"].ToString().Trim();
                        dr["FKM_EXPNS"] = dtinfo.Rows[0]["FKM_EXPNS"].ToString().Trim();
                        dr["FKM_ROI"] = dtinfo.Rows[0]["FKM_ROI"].ToString().Trim();
                        dr["FKM_BOOKVAL"] = dtinfo.Rows[0]["FKM_BOOKVAL"].ToString().Trim();

                        dr["HEADER1"] = "";
                        dr["FKM_CURR_SMBL"] = CURRSYMB;
                        dr["FOOTER"] = User.Identity.Name.ToUpper();

                        dr["FKM_REG"] = (string)dbo.GET_FRM_COMP_CD(dr["COMP_CD"].ToString(), 1);
                        dr["LOGO"] = dbo.ReadFile(Server.MapPath((string)dbo.GET_FRM_COMP_CD(dr["COMP_CD"].ToString(), 2)));
                    }

                    Button1.Visible = true;
                    Session["INV_TRX_dtGRID"] = INV_TRX_dtGRID;
                    GridView1.DataSource = INV_TRX_dtGRID;
                    GridView1.DataBind();
                }
                else
                {
                    Session["INV_TRX_dtGRID"] = null;
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    Button1.Visible = false;

                }


                Button2.Visible = false;
                DownloadButton.NavigateUrl = "";
                string docQRY = "SELECT * FROM FKM_DOC WHERE  DOC_FKM_LNK='" + CODE + "' AND  DOC_TYPE LIKE 'IRR CALC' ";
                DataTable docinfo = dbo.SelTable(docQRY);
                if (docinfo.Rows.Count == 1)
                {
                    string flnm = docinfo.Rows[0]["DOC_SRL"].ToString().Trim() + "." + docinfo.Rows[0]["DOC_EXTN"].ToString().Trim();

                    DownloadButton.NavigateUrl = "~/Documents/" + flnm;
                    Button2.Visible = true;
                }

            }
            else
            {
                mbox("FKM SRL :  '" + CODE + "'  DOESN'T EXIST !!");
            }

        }
        catch (System.Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }
    }



    public void Button1_Click(object sender, System.EventArgs e) //Handles Button1.Click
    {

        ReportDocument rpt = new ReportDocument();
        string rname = Server.MapPath("~/Reports/IRRSUMMRPT.rpt");
        string dwnldfname = Server.MapPath("~/ReportsGenerate/INVdwnload.xls");
        DataTable dt = (DataTable)Session["INV_TRX_dtGRID"];

        if (dt.Rows.Count > 0)
        {
            rpt.Load(rname);
            rpt.SetDataSource(dt);

            //'rpt.PrintOptions.PaperSize = PaperSize.PaperA4
            //'rpt.PrintOptions.PaperOrientation = PaperOrientation.Landscape
            rpt.ExportToDisk(ExportFormatType.Excel, dwnldfname);
            rpt.Close();
            rpt.Dispose();


            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("content-disposition", "attachment;filename =" + "INVdwnload.xls");
            Response.ContentType = "application/ms-excel";
            Response.TransmitFile(dwnldfname);

        }
    }


    public void Button2_Click(object sender, System.EventArgs e) //Handles Button1.Click
    {

        string CODE = Request.QueryString.Get("FKMCODE");
        string dwnldfname = Server.MapPath(DownloadButton.NavigateUrl);

        string docQRY = "SELECT * FROM FKM_DOC WHERE  DOC_FKM_LNK='" + CODE + "' AND  DOC_TYPE LIKE 'IRR CALC' ";
        DataTable docinfo = dbo.SelTable(docQRY);
        if (docinfo.Rows.Count == 1)
        {

            string flnm = docinfo.Rows[0]["DOC_SRL"].ToString().Trim() + "." + docinfo.Rows[0]["DOC_EXTN"].ToString().Trim();

            DownloadButton.NavigateUrl = "~/Documents/" + flnm;

            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("content-disposition", "attachment;filename =" + flnm);
            Response.ContentType = "application/ms-excel";
            Response.TransmitFile(dwnldfname);
        }
    }

    public void mbox(string val)
    {
        //string cs   = "c";
        //string cst  = "<script type=""text/javascript"">" + "alert('" + val + "');</" + "script>";
        //Page page    = HttpContext.Current.Handler;
        //page.ClientScript.RegisterStartupScript(Me.typeof, cs, cst);

        string USR = User.Identity.Name.ToUpper();
        dbo.ErrorLog(USR + " : INV_TRX.aspx : " + val, Server.MapPath("~\\Logs\\ErrorLog"));
    }
}
