using System;
using System.Data;
using System.IO;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
public partial class INVEST_TRX_EDIT : Page
{
    fkminvcom dbo = new fkminvcom();
    // Visual C# .NET requires that you override the OnInit function,
    // adding a new delegate for the Page_Load event.

    override protected void OnInit(System.EventArgs e)
    {
        this.Load += new System.EventHandler(this.Page_Load);
        base.OnInit(e);
        CMB_FKM_CD.SelectedIndexChanged += new System.EventHandler(CMB_FKM_CD_SelectedIndexChanged);
        UploadButton.Click += new System.EventHandler(UploadButton_Click1);
        CMB_REFSRL.SelectedIndexChanged += new System.EventHandler(CMB_REFSRL_SelectedIndexChanged);
        GridView1.SelectedIndexChanged += new System.EventHandler(GridView1_SelectedIndexChanged);
        CHBX_EDIT.CheckedChanged += new System.EventHandler(CHBX_EDIT_CheckedChanged);
        Button1.Click += new System.EventHandler(Button1_Click);
        Button2.Click += new System.EventHandler(Button2_Click);
        BTN_ADD.Click += new System.EventHandler(BTN_ADD_Click);
        BTN_UPDT.Click += new System.EventHandler(BTN_UPDT_Click);
        BTN_CLEAR.Click += new System.EventHandler(BTN_CLEAR_Click);
    }

    public void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            CLEAR();
            CMB_FKM_CD.SelectedIndex = -1;
            INIT_FKMCD();

            //' EDITING
            if ((Request.QueryString.Get("REFSRL") != null))
            {
                CMB_FKM_CD.DataBind();
                string FKMREFCODE = Request.QueryString.Get("FKMSRL");
                CMB_FKM_CD.SelectedIndex = CMB_FKM_CD.Items.IndexOf(CMB_FKM_CD.Items.FindByValue(FKMREFCODE));
                CMB_REFSRL.DataBind();

                string REFCODE = Request.QueryString.Get("REFSRL");
                CMB_REFSRL.SelectedIndex = CMB_REFSRL.Items.IndexOf(CMB_REFSRL.Items.FindByValue(REFCODE));
                string CODE = CMB_REFSRL.SelectedValue;

               CMB_REFSRL_CHANGED(CODE);
            }
        }
    }

    public void BTN_CLEAR_Click(object sender, System.EventArgs e)// Handles BTN_CLEAR.Click
    { CLEAR(); }


    public void CHBX_EDIT_CheckedChanged(object sender, System.EventArgs e)// Handles CHBX_EDIT.CheckedChanged
    { CLEAR(); }

    public void CLEAR()
    {
        TXT_FKM_REF.Text = "";
        TXT_ISSUEDATE.Text = "";
        TXT_REFMEMO.Text = "";
        TXT_DESCRP.Text = "";
        TXT_TINVAMT.Text = "";
        TXT_EXPENSE_T.Text = "";
        TXT_EXPENSE_Temp.Text = "0";
        TXT_INCOME_temp.Text = "0";
        TXT_INCOME.Text = "";
        TXT_TROI.Text = "";
        // ' TXT_TOTSNCINCP.Text = ""
        TXT_REMX.Text = "";
        CMB_STATUS.SelectedIndex = 0;

        TXT_PROJNAME_M.Text = "";
        TXT_PROJNAME.Text = "";
        //'CMB_HOLDNAME.SelectedIndex = -1
        //'CMB_OPPRBY.SelectedIndex = -1
        //'CMB_INVCOMP.SelectedIndex = -1
        TXT_PRTCPDATE.Text = "";
        TXT_MTRTDATE.Text = "";
        //'CMB_LOCATION.SelectedIndex = -1
        //'CMB_CURR.SelectedIndex = -1
        TXT_BANK.Text = "";
        TXT_YLDPRD.Text = "";
        //'CMB_YEILDPRD.SelectedIndex = 3
        //'CMB_INCGEN.SelectedIndex = 0
        //'CMB_PRVTEQT.SelectedIndex = 0
        //'CMB_STATUS.SelectedIndex = 0
        //'CMB_FKM_LNK.SelectedIndex = -1

        TXT_COMMCAP.Text = "";
        TXT_COMMCAP2.Text = "";
        TXT_INVAMT.Text = "";

        TXT_CAPPD.Text = "";
        TXT_CAPUNPD.Text = "";
        TXT_CAPRFND.Text = "";
        TXT_EXPNS.Text = "";
        TXT_VALVTN.Text = "";
        TXT_ROI.Text = "";

        //'TXT_DVDND.Text = ""
        //'TXT_BOOKVAL.Text = ""
        //'TXT_CAPGN.Text = ""
        //'TXT_UNRLCAP.Text = ""
        //'TXT_FAIRVAL.Text = ""
        //'TXT_NAV.Text = ""

        //'TXT_MONYCAL.Text = ""
        //'TXT_QRTYCAL.Text = ""
        //'TXT_SMANYCAL.Text = ""
        //'TXT_ANLYCAL.Text = ""
        //'TXT_SCNINCP.Text = ""
        //'TXT_SALEPRCD.Text = ""

        //'TXT_DESC.Text = ""
        TXT_NOF_DAYS.Text = "";
        TXT_REMX.Text = "";
        TXT_IRR_FLNM.Text = "";
        Button2.Visible = false;
        DownloadButton.NavigateUrl = "";
        StatusLabel.Text = "";
        CMB_REFSRL.SelectedIndex = -1;

        //'Session("INV_TRX_EDIT_dtGRID") = null
        //'Button1.Visible = false
        //'GridView1.DataSource = null
        //'GridView1.DataBind()

        if (CHBX_EDIT.Checked == true)
        {

            TXT_REFSRL.Text = "";//'NEXT_REC_SRL()
            TXT_REFSRL.Visible = false;
            CMB_REFSRL.SelectedIndex = -1;
            CMB_REFSRL.Visible = true;
            BTN_ADD.Visible = false;
            BTN_UPDT.Visible = true;
        }
        else
        {

            TXT_REFSRL.Text = NEXT_REC_SRL();
            TXT_REFSRL.Visible = true;
            CMB_REFSRL.SelectedIndex = -1;
            CMB_REFSRL.Visible = false;
            BTN_ADD.Visible = true;
            BTN_UPDT.Visible = false;
        }

        // ' Call NEXT_REC_SRL()
    }

    protected void BTN_ADD_Click(object sender, System.EventArgs e) //Handles BTN_ADD.Click
    {
        string STR_TEMP = "";
        if (CMB_FKM_CD.SelectedIndex < 0)
        {
            return;
        }

        string ISSDATE = "null";
        string ISSMON = "null";
        if ((TXT_ISSUEDATE.Text.Trim().Length > 0))
        {
            string PDT = TXT_ISSUEDATE.Text;
            ISSDATE = "'" + TXT_ISSUEDATE.Text + "'";
            ISSMON = "'" + PDT.Substring(6, 4) + PDT.Substring(3, 2) + "'";
        }

        STR_TEMP = "0";
        if (TXT_TINVAMT.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_TINVAMT.Text.Trim();
        }
        Double TINVAMT = double.Parse(STR_TEMP);
        STR_TEMP = "0";
        if (TXT_INCOME_temp.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_INCOME_temp.Text.Trim();
        }
        Double TINCOME_temp = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_INCOME.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_INCOME.Text.Trim();
        }
        Double INCM = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_EXPENSE_T.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_EXPENSE_T.Text.Trim();
        }
        Double EXPENSE = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_EXPENSE_Temp.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_EXPENSE_Temp.Text.Trim();
        }
        Double EXPENSE_temp = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_TROI.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_TROI.Text.Trim();
        }
        Double TROI = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_NOF_DAYS.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_NOF_DAYS.Text.Trim();
        }
        Double NOF_DAYS = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_TOTSNCINCP.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_TOTSNCINCP.Text.Trim();
        }
        double TOTSNCINCP = double.Parse(STR_TEMP);
        double ZERO = 0;


        string USR = User.Identity.Name.ToUpper();
        string UPDDT = System.DateTime.Today.ToString("dd/MM/yyyy");
        string UPDTIME = System.DateTime.Now.ToString("hh:mm:ss");
        string CODE = TXT_REFSRL.Text.ToUpper();

        try
        {
            string RETRVQRY = "SELECT * FROM INVEST_TRX WHERE TRX_REFSRL = '" + CODE + "'";
            string COMP_CD = dbo.GET_COMP_CD(CODE);

            if (dbo.RecExist(RETRVQRY) == false)
            {
                string INSQRY = "INSERT INTO INVEST_TRX VALUES('" + COMP_CD + "','" + CODE + "','" + CMB_FKM_CD.SelectedItem.Value.Trim() + "'," + ISSMON +
                                       ",'" + TXT_DESCRP.Text.Trim() + "','" + TINVAMT + "','" + EXPENSE + "','" + ZERO + "','" + INCM +
                                       "','" + TROI + "','" + NOF_DAYS + "','" + TXT_REMX.Text.Trim() + "','" + CMB_STATUS.SelectedItem.Value.Trim() + "','" + USR +
                                       "','" + UPDDT + "','" + UPDTIME + "'," + ISSDATE + ",'" + TXT_REFMEMO.Text.Trim() + "')";
                //' TOTAL SINCE INCEPTION 
                string fkmQRY = "";
                //'   "UPDATE INVEST_INFO SET FKM_SCNINCP = '" + TOTSNCINCP +
                //'                       "',FKM_UPDBY = '" + USR + "',FKM_UPDDATE = '" + UPDDT + "',FKM_UPDTIME = '" + UPDTIME + "'" +
                //'                       " WHERE  FKM_SRL = '" + CMB_FKM_CD.SelectedItem.Value.Trim + "'"

                if (dbo.Transact(INSQRY, fkmQRY, "", "", "") == false)
                {
                    mbox("ERROR !!");
                }
                else
                {

                    BTN_CLEAR_Click(sender, e);
                    CMB_FKM_CD_SelectedIndexChanged(sender, e);
                    mbox("FKM SRL :  '" + CODE + "'   ADDED SUCCESSFULLY !!");
                }
            }
            else
            {
                mbox("FKM SRL :  '" + CODE + "'  ALREADY EXIST !!");
            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }

    }
    protected void BTN_UPDT_Click(object sender, System.EventArgs e) //Handles BTN_UPDT.Click
    {
        //' Dim SRL As String = TXT_FKM_CD.Text
        string STR_TEMP = "";
        if ((CMB_REFSRL.Text.Trim().Length < 1))
        {
            return;
        }

        string ISSDATE = "null";
        string ISSMON = "null";
        if ((TXT_ISSUEDATE.Text.Trim().Length > 0))
        {
            string PDT = TXT_ISSUEDATE.Text;
            ISSDATE = "'" + TXT_ISSUEDATE.Text + "'";
            ISSMON = "'" + PDT.Substring(6, 4) + PDT.Substring(3, 2) + "'";
        }


        STR_TEMP = "0";
        if (TXT_TINVAMT.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_TINVAMT.Text.Trim();
        }
        Double TINVAMT = double.Parse(STR_TEMP);
        STR_TEMP = "0";
        if (TXT_INCOME_temp.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_INCOME_temp.Text.Trim();
        }
        Double TINCOME_temp = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_INCOME.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_INCOME.Text.Trim();
        }
        Double INCM = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_EXPENSE_T.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_EXPENSE_T.Text.Trim();
        }
        Double EXPENSE = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_EXPENSE_Temp.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_EXPENSE_Temp.Text.Trim();
        }
        Double EXPENSE_temp = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_TROI.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_TROI.Text.Trim();
        }
        Double TROI = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_NOF_DAYS.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_NOF_DAYS.Text.Trim();
        }
        Double NOF_DAYS = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_TOTSNCINCP.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_TOTSNCINCP.Text.Trim();
        }
        double TOTSNCINCP = double.Parse(STR_TEMP);
        double ZERO = 0;



        string USR = User.Identity.Name.ToUpper();
        string UPDDT = System.DateTime.Today.ToString("dd/MM/yyyy");
        string UPDTIME = System.DateTime.Now.ToString("hh:mm:ss");
        string CODE = CMB_REFSRL.Text.ToUpper();

        try
        {
            string RETRVQRY = "SELECT * FROM INVEST_TRX WHERE TRX_REFSRL = '" + CODE + "'";
            ///'  Dim COMP_CD As String = dbo.GET_COMP_CD(CODE)

            if (dbo.RecExist(RETRVQRY) == true)
            {
                string INSQRY = "UPDATE INVEST_TRX SET TRX_ISSMONT = " + ISSMON +
                                       ",TRX_DESCRP = '" + TXT_DESCRP.Text.Trim() + "',TRX_INVAMT = '" + TINVAMT +
                                       "',TRX_AMT = '" + INCM + "',TRX_EXPNSAMT = '" + EXPENSE + "',TRX_STATUS = '" + CMB_STATUS.SelectedItem.Value.Trim() +
                                       "',TRX_ROI = '" + TROI + "',TRX_NOF_DAYS = '" + NOF_DAYS + "',TRX_REMX = '" + TXT_REMX.Text.Trim() +
                                       "',TRX_UPDBY = '" + USR + "',TRX_REFMEMO = '" + TXT_REFMEMO.Text.Trim() +
                                       "',TRX_UPDDATE = '" + UPDDT + "',TRX_UPDTIME= '" + UPDTIME + "',TRX_ISSUEDATE = " + ISSDATE + " " +
                                       " WHERE TRX_REFSRL = '" + CODE + "' ";// 'AND COMP_CD = '" + COMP_CD + "' "

                //'  "',TRX_AMT = '" + TOTAMT +  "',TRX_INSCPAMT = '" + TOTSNCINCP - TINCOME_temp + INCM +

                string fkmQRY = "";
                //"UPDATE INVEST_INFO SET FKM_SCNINCP = '" + (TOTSNCINCP - TINCOME_temp) + INCM +
                // "',FKM_UPDBY = '" + USR + "',FKM_UPDDATE = '" + UPDDT + "',FKM_UPDTIME = '" + UPDTIME + "'" +
                // " WHERE  FKM_SRL = '" + CMB_FKM_CD.SelectedItem.Value.Trim() + "'";

                if (dbo.Transact(INSQRY, fkmQRY, "", "", "") == false)
                {
                    mbox("ERROR !!");
                }
                else
                {
                    BTN_CLEAR_Click(sender, e);
                    CMB_FKM_CD_SelectedIndexChanged(sender, e);
                    mbox("FKM SRL :  '" + CODE + "'   ADDED SUCCESSFULLY !!");
                }
            }
            else
            {
                mbox("FKM SRL :  '" + CODE + "'  ALREADY EXIST !!");
            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }

    }


    protected void CMB_FKM_CD_SelectedIndexChanged(object sender, System.EventArgs e)// Handles CMB_FKM_CD.SelectedIndexChanged
    {
        string CODE = CMB_FKM_CD.SelectedValue;
        CMB_FKM_CD_Changed(CODE);
    }

    protected void CMB_FKM_CD_Changed(string CODE)
    {
        CHBX_EDIT.Checked = true;
        //'Dim SRL As String = "0"
        CLEAR();
        try
        {
            string RETRVQRY = "SELECT * FROM INVEST_INFO WHERE FKM_SRL = '" + CODE + "'";
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

                TXT_FKM_REF.Text = CODE;
                TXT_ISSUEDATE.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
                TXT_PROJNAME_M.Text = dtinfo.Rows[0]["FKM_PROJNAME"].ToString().Trim();
                TXT_PROJNAME.Text = dtinfo.Rows[0]["FKM_PROJNAME"].ToString().Trim();
                //'CMB_HOLDNAME.SelectedIndex = CMB_HOLDNAME.Items.IndexOf(CMB_HOLDNAME.Items.FindByValue(dtinfo.Rows[0]["FKM_HOLDNAME"].ToString().Trim()))
                //'CMB_OPPRBY.SelectedIndex = CMB_OPPRBY.Items.IndexOf(CMB_OPPRBY.Items.FindByValue(dtinfo.Rows[0]["FKM_OPPRBY"].ToString().Trim()))
                //'CMB_INVCOMP.SelectedIndex = CMB_INVCOMP.Items.IndexOf(CMB_INVCOMP.Items.FindByValue(dtinfo.Rows[0]["FKM_INVCOMP"].ToString().Trim()))
                TXT_PRTCPDATE.Text = dtinfo.Rows[0]["FKM_PRTCPDATE"].ToString().Trim();
                TXT_MTRTDATE.Text = dtinfo.Rows[0]["FKM_MTRTDATE"].ToString().Trim();
                //'CMB_LOCATION.SelectedIndex = CMB_LOCATION.Items.IndexOf(CMB_LOCATION.Items.FindByValue(dtinfo.Rows[0]["FKM_LOCATION"].ToString().Trim()))
                //'CMB_CURR.SelectedIndex = CMB_CURR.Items.IndexOf(CMB_CURR.Items.FindByValue(dtinfo.Rows[0]["FKM_CURR"].ToString().Trim()))
                TXT_BANK.Text = dtinfo.Rows[0]["FKM_BANK"].ToString().Trim();
                if (dtinfo.Rows[0]["FKM_YEILDPRD"].ToString().Trim() == "M")
                {
                    TXT_YLDPRD.Text = "MONTHLY";
                    // '  TXT_INCOME.Text = dtinfo.Rows[0]["FKM_MONYCAL"].ToString().Trim()
                }
                else if (dtinfo.Rows[0]["FKM_YEILDPRD"].ToString().Trim() == "Q")
                {
                    TXT_YLDPRD.Text = "QUARTERLY";
                    // ' TXT_INCOME.Text = dtinfo.Rows[0]["FKM_QRTYCAL"].ToString().Trim()
                }
                else if (dtinfo.Rows[0]["FKM_YEILDPRD"].ToString().Trim() == "S")
                {
                    TXT_YLDPRD.Text = "SEMI ANNUALY";
                    //'  TXT_INCOME.Text = dtinfo.Rows[0]["FKM_SMANYCAL"].ToString().Trim()
                }

                //'CMB_INCGEN.SelectedIndex = CMB_INCGEN.Items.IndexOf(CMB_INCGEN.Items.FindByValue(dtinfo.Rows[0]["FKM_INCGEN"].ToString().Trim()))
                //'CMB_PRVTEQT.SelectedIndex = CMB_PRVTEQT.Items.IndexOf(CMB_PRVTEQT.Items.FindByValue(dtinfo.Rows[0]["FKM_PRVTEQT"].ToString().Trim()))
                //'CMB_STATUS.SelectedIndex = CMB_STATUS.Items.IndexOf(CMB_STATUS.Items.FindByValue(dtinfo.Rows[0]["FKM_STATUS"].ToString().Trim()))
                //'CMB_FKM_LNK.SelectedIndex = CMB_FKM_LNK.Items.IndexOf(CMB_FKM_LNK.Items.FindByValue(dtinfo.Rows[0]["FKM_LNKSRL"].ToString().Trim()))

                //'CMB_PROJNAME.Text = dtinfo.Rows[0]["FKM_PROJNAME"].ToString().Trim()
                //'CMB_HOLDNAME.Text = dtinfo.Rows[0]["FKM_HOLDNAME"].ToString().Trim()
                //'CMB_OPPRBY.Text = dtinfo.Rows[0]["FKM_OPPRBY"].ToString().Trim()
                //'CMB_INVCOMP.Text = dtinfo.Rows[0]["FKM_INVCOMP"].ToString().Trim()
                //'TXT_PRTCPDATE.Text = dtinfo.Rows[0]["FKM_PRTCPDATE"].ToString().Trim()
                //'TXT_MTRTDATE.Text = dtinfo.Rows[0]["FKM_MTRTDATE"].ToString().Trim()
                //'CMB_LOCATION.Text = dtinfo.Rows[0]["FKM_LOCATION"].ToString().Trim()
                //'CMB_CURR.Text = dtinfo.Rows[0]["FKM_CURR"].ToString().Trim()
                //'CMB_BANK.Text = dtinfo.Rows[0]["FKM_BANK"].ToString().Trim()
                //'CMB_YEILDPRD.Text = dtinfo.Rows[0]["FKM_YEILDPRD"].ToString().Trim()
                //'CMB_INCGEN.Text = dtinfo.Rows[0]["FKM_INCGEN"].ToString().Trim()
                //'CMB_PRVTEQT.Text = dtinfo.Rows[0]["FKM_PRVTEQT"].ToString().Trim()
                //'CMB_STATUS.Text = dtinfo.Rows[0]["FKM_STATUS"].ToString().Trim()
                //'CMB_FKM_LNK.Text = dtinfo.Rows[0]["FKM_LNKSRL"].ToString().Trim()

                TXT_COMMCAP.Text = CURRSYMB + " " + string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_COMMCAP"]);// 'dtinfo.Rows[0]["FKM_COMMCAP"].ToString().Trim()
                TXT_COMMCAP2.Text = CURRSYMB + " " + string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_COMMCAP2"]);// 'dtinfo.Rows[0]["FKM_COMMCAP2"].ToString().Trim()
                TXT_INVAMT.Text = CURRSYMB + " " + string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_INVAMT"]); //' dtinfo.Rows[0]["FKM_INVAMT"].ToString().Trim()

                //'TXT_ISSUEDATE.Text = System.DateTime.Today.ToShortDateString
                TXT_TINVAMT.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_INVAMT"]);// 'dtinfo.Rows[0]["FKM_INVAMT"].ToString().Trim()


                TXT_CAPPD.Text = CURRSYMB + " " + string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_CAPPD"]);// 'dtinfo.Rows[0]["FKM_CAPPD"].ToString().Trim()
                TXT_CAPUNPD.Text = CURRSYMB + " " + string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_CAPUNPD"]);// 'dtinfo.Rows[0]["FKM_CAPUNPD"].ToString().Trim()
                TXT_CAPRFND.Text = CURRSYMB + " " + string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_CAPRFND"]);// ' dtinfo.Rows[0]["FKM_CAPRFND"].ToString().Trim()
                TXT_EXPNS.Text = CURRSYMB + " " + string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_EXPNS"]);// 'dtinfo.Rows[0]["FKM_EXPNS"].ToString().Trim()
                TXT_VALVTN.Text = CURRSYMB + " " + string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_VALVTN"]);// ' dtinfo.Rows[0]["FKM_VALVTN"].ToString().Trim()
                TXT_ROI.Text = dtinfo.Rows[0]["FKM_ROI"].ToString().Trim() + " %";
                //'TXT_DVDND.Text = dtinfo.Rows[0]["FKM_DVDND"].ToString().Trim()
                //'TXT_BOOKVAL.Text = dtinfo.Rows[0]["FKM_BOOKVAL"].ToString().Trim()
                //'TXT_CAPGN.Text = dtinfo.Rows[0]["FKM_CAPGN"].ToString().Trim()
                //'TXT_UNRLCAP.Text = dtinfo.Rows[0]["FKM_UNRLCAP"].ToString().Trim()
                //'TXT_FAIRVAL.Text = dtinfo.Rows[0]["FKM_FAIRVAL"].ToString().Trim()
                //'TXT_NAV.Text = dtinfo.Rows[0]["FKM_NAV"].ToString().Trim()

                //'TXT_MONYCAL.Text = dtinfo.Rows[0]["FKM_MONYCAL"].ToString().Trim()
                //'TXT_QRTYCAL.Text = dtinfo.Rows[0]["FKM_QRTYCAL"].ToString().Trim()
                //'TXT_SMANYCAL.Text = dtinfo.Rows[0]["FKM_SMANYCAL"].ToString().Trim()
                //'TXT_ANLYCAL.Text = dtinfo.Rows[0]["FKM_ANLYCAL"].ToString().Trim()
                TXT_TOTSNCINCP.Text = dtinfo.Rows[0]["FKM_SCNINCP"].ToString().Trim();
                //'TXT_SALEPRCD.Text = dtinfo.Rows[0]["FKM_SALEPRCD"].ToString().Trim()
                //'TXT_REMX.Text = dtinfo.Rows[0]["FKM_REMX"].ToString().Trim()

                //'Dim CODE As String = CMB_FKM_CD.SelectedValue
                InitGrid();
                DataTable dtGRID = (DataTable)Session["INV_TRX_EDIT_dtGRID"];

                if (dtGRID.Rows.Count > 0)
                {

                    dtGRID.Columns.Add("FKM_SRL", typeof(String));
                    dtGRID.Columns.Add("FKM_PROJNAME", typeof(String));
                    dtGRID.Columns.Add("FKM_INVCOMP", typeof(String));
                    dtGRID.Columns.Add("FKM_PRTCPDATE", typeof(String));
                    dtGRID.Columns.Add("FKM_MTRTDATE", typeof(String));
                    dtGRID.Columns.Add("FKM_BANK", typeof(String));
                    dtGRID.Columns.Add("FKM_CURR", typeof(String));

                    dtGRID.Columns.Add("FKM_COMMCAP", typeof(Decimal));
                    dtGRID.Columns.Add("FKM_COMMCAP2", typeof(Decimal));
                    dtGRID.Columns.Add("FKM_INVAMT", typeof(Decimal));
                    dtGRID.Columns.Add("FKM_CAPPD", typeof(Decimal));
                    dtGRID.Columns.Add("FKM_CAPUNPD", typeof(Decimal));
                    dtGRID.Columns.Add("FKM_CAPRFND", typeof(Decimal));
                    dtGRID.Columns.Add("FKM_EXPNS", typeof(Decimal));
                    dtGRID.Columns.Add("FKM_ROI", typeof(Decimal));
                    dtGRID.Columns.Add("FKM_BOOKVAL", typeof(Decimal));

                    dtGRID.Columns.Add("HEADER1", typeof(String));
                    dtGRID.Columns.Add("FKM_CURR_SMBL", typeof(String));
                    dtGRID.Columns.Add("FOOTER", typeof(String));

                    dtGRID.Columns.Add("TRX_AMT_R");
                    dtGRID.Columns.Add("TRX_INVAMT_R");
                    dtGRID.Columns.Add("TRX_EXPNSAMT_R");
                    dtGRID.Columns.Add("TRX_ROI_R");


                    foreach (DataRow DR in dtGRID.Rows)
                    {
                        DR["TRX_AMT_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR["TRX_AMT"]);
                        DR["TRX_INVAMT_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR["TRX_INVAMT"]);
                        DR["TRX_EXPNSAMT_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR["TRX_EXPNSAMT"]);
                        DR["TRX_ROI_R"] = DR["TRX_ROI"].ToString().Trim() + " %";

                        DR["FKM_SRL"] = CODE;
                        DR["FKM_PROJNAME"] = dtinfo.Rows[0]["FKM_PROJNAME"].ToString().Trim();
                        DR["FKM_INVCOMP"] = dtinfo.Rows[0]["FKM_INVCOMP"].ToString().Trim();
                        DR["FKM_PRTCPDATE"] = dtinfo.Rows[0]["FKM_PRTCPDATE"].ToString().Trim();
                        DR["FKM_MTRTDATE"] = dtinfo.Rows[0]["FKM_MTRTDATE"].ToString().Trim();
                        DR["FKM_BANK"] = dtinfo.Rows[0]["FKM_BANK"].ToString().Trim();
                        DR["FKM_CURR"] = dtinfo.Rows[0]["FKM_CURR"].ToString().Trim();

                        DR["FKM_COMMCAP"] = dtinfo.Rows[0]["FKM_COMMCAP"].ToString().Trim();
                        DR["FKM_COMMCAP2"] = dtinfo.Rows[0]["FKM_COMMCAP2"].ToString().Trim();
                        DR["FKM_INVAMT"] = dtinfo.Rows[0]["FKM_INVAMT"].ToString().Trim();
                        DR["FKM_CAPPD"] = dtinfo.Rows[0]["FKM_CAPPD"].ToString().Trim();
                        DR["FKM_CAPUNPD"] = dtinfo.Rows[0]["FKM_CAPUNPD"].ToString().Trim();
                        DR["FKM_CAPRFND"] = dtinfo.Rows[0]["FKM_CAPRFND"].ToString().Trim();
                        DR["FKM_EXPNS"] = dtinfo.Rows[0]["FKM_EXPNS"].ToString().Trim();
                        DR["FKM_ROI"] = dtinfo.Rows[0]["FKM_ROI"].ToString().Trim();
                        DR["FKM_BOOKVAL"] = dtinfo.Rows[0]["FKM_BOOKVAL"].ToString().Trim();

                        DR["HEADER1"] = "";
                        DR["FKM_CURR_SMBL"] = CURRSYMB;
                        DR["FOOTER"] = User.Identity.Name.ToUpper();
                    }





                    Button1.Visible = true;
                    Session["INV_TRX_EDIT_dtGRID"] = dtGRID;

                    GridView1.DataSource = dtGRID;
                    GridView1.DataBind();
                }

                Button2.Visible = false;
                string docQRY = "SELECT * FROM FKM_DOC WHERE  DOC_FKM_LNK='" + CODE + "' AND  DOC_TYPE LIKE 'IRR CALC' ";
                DataTable docinfo = dbo.SelTable(docQRY);
                if (docinfo.Rows.Count == 1)
                {

                    TXT_IRR_FLNM.Text = docinfo.Rows[0]["DOC_SRL"].ToString().Trim() + "." + docinfo.Rows[0]["DOC_EXTN"].ToString().Trim();

                    DownloadButton.NavigateUrl = "~/Documents/" + TXT_IRR_FLNM.Text;

                    Button2.Visible = true;
                }
            }
            else
            {
                mbox("FKM SRL :  '" + CODE + "'  DOESN'T EXIST !!");
            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }

    }

    protected void CMB_REFSRL_SelectedIndexChanged(object sender, System.EventArgs e)
    {// Handles CMB_REFSRL.SelectedIndexChanged

        string CODE = CMB_REFSRL.SelectedValue;
        CMB_REFSRL_CHANGED(CODE);

    }
    protected void CMB_REFSRL_CHANGED(string CODE)
    {
        //' Dim CODE As String = CMB_REFSRL.SelectedValue
        TXT_ISSUEDATE.Text = "";
        TXT_REFMEMO.Text = "";
        TXT_DESCRP.Text = "";
        TXT_TINVAMT.Text = "";
        TXT_EXPENSE_T.Text = "";
        //'TXT_EXPENSE_Temp.Text = "0"
        TXT_INCOME.Text = "";
        TXT_TROI.Text = "";
        //'TXT_TOTSNCINCP.Text = ""
        TXT_REMX.Text = "";

        try
        {
            //'   Dim COMP_CD As String = dbo.GET_COMP_CD(CODE)
            string RETRVQRY = "SELECT * FROM INVEST_TRX WHERE TRX_REFSRL = '" + CODE + "'";// ' AND  COMP_CD = '" + COMP_CD + "'  ";
            DataTable dtinfo = dbo.SelTable(RETRVQRY);
            if (dtinfo.Rows.Count == 1)
            {

                CMB_REFSRL.SelectedIndex = CMB_REFSRL.Items.IndexOf(CMB_REFSRL.Items.FindByValue(CODE));
                TXT_ISSUEDATE.Text = dtinfo.Rows[0]["TRX_ISSUEDATE"].ToString().Trim();
                TXT_REFMEMO.Text = dtinfo.Rows[0]["TRX_REFMEMO"].ToString().Trim();
                TXT_DESCRP.Text = dtinfo.Rows[0]["TRX_DESCRP"].ToString().Trim();
                TXT_TINVAMT.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["TRX_INVAMT"]);
                TXT_INCOME_temp.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["TRX_AMT"]);
                TXT_INCOME.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["TRX_AMT"]);
                TXT_EXPENSE_T.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["TRX_EXPNSAMT"]);
                TXT_EXPENSE_Temp.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["TRX_EXPNSAMT"]);
                // '  TXT_TOTSNCINCP.Text = dtinfo.Rows[0]["TRX_INSCPAMT"].ToString().Trim();
                TXT_TROI.Text = dtinfo.Rows[0]["TRX_ROI"].ToString().Trim();
                TXT_NOF_DAYS.Text = dtinfo.Rows[0]["TRX_NOF_DAYS"].ToString().Trim();
                TXT_REMX.Text = dtinfo.Rows[0]["TRX_REMX"].ToString().Trim();
                CMB_STATUS.SelectedIndex = CMB_STATUS.Items.IndexOf(CMB_STATUS.Items.FindByValue(dtinfo.Rows[0]["TRX_STATUS"].ToString().Trim()));
            }
            else
            {
                mbox("FKM SRL :  '" + CODE + "'  DOESN'T EXIST !!");
            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }

    }


    protected void UploadButton_Click1(object sender, System.EventArgs e) //Handles UploadButton.Click
    {
        if (FileUploadControl.HasFile)
        {

            string[] FLNME = FileUploadControl.FileName.ToString().Split('.');
            if (FLNME[1].ToUpper() == "XLS" || FLNME[1].ToUpper() == "XLSX")
            {

                try
                {
                    string[] DOC_SRL = TXT_IRR_FLNM.Text.ToString().Split('.');
                    string USR = User.Identity.Name.ToUpper();
                    string UPDDT = System.DateTime.Today.ToString("dd/MM/yyyy");
                    string UPDTIME = System.DateTime.Now.ToString("hh:mm:ss");
                    string CODE = CMB_FKM_CD.SelectedValue;
                    //' Dim COMP_CD As String = dbo.GET_COMP_CD(CODE)


                    String RETRVQRY = "SELECT * FROM FKM_DOC WHERE DOC_SRL = '" + DOC_SRL[0] + "'";
                    if (dbo.RecExist(RETRVQRY) == true)
                    {

                        //'Dim filename As String = Path.GetFileName(FileUploadControl.FileName)
                        String PATHFL = "~/Documents/" + DOC_SRL[0] + "." + FLNME[1];
                        FileUploadControl.SaveAs(Server.MapPath("~/Documents/") + DOC_SRL[0] + "." + FLNME[1]);
                        StatusLabel.Text = "Upload status: File uploaded!";

                        String filename = Path.GetFileName(FileUploadControl.FileName);
                        //' FileUploadControl.SaveAs(Server.MapPath("~/Documents/") + filename)
                        //' StatusLabel.Text = "Upload status: File uploaded!"


                        String UPDQRY = "UPDATE FKM_DOC SET  DOC_PATH = '" + PATHFL.ToUpper() +
                                                   "', DOC_FILENAME = '" + FLNME[0] + "',DOC_EXTN = '" + FLNME[1] +
                                                   "', DOC_UPDBY = '" + USR + "',DOC_UPDDATE = '" + UPDDT + "',DOC_UPDTIME = '" + UPDTIME + "' " +
                                                   "WHERE DOC_SRL= '" + DOC_SRL[0] + "' AND DOC_TYPE LIKE 'IRR CALC' AND DOC_FKM_LNK LIKE '" +
                                                   CMB_FKM_CD.SelectedItem.Value.ToString().ToUpper().Trim() + "' ";//'AND COMP_CD = '" + COMP_CD + "' "

                        if (dbo.InsRecord(UPDQRY) == false)
                        {
                            StatusLabel.Text = "Upload status: File upload failed !";
                        }
                        else
                        {
                            StatusLabel.Text = "Upload status: File uploaded!";
                            TXT_IRR_FLNM.Text = DOC_SRL[0] + "." + FLNME[1];

                            DownloadButton.NavigateUrl = "~/Documents/" + TXT_IRR_FLNM.Text;
                        }

                        //'if( dbo.sendmail("rohith.raghuram@hotmail.com", "fkm test", filename) = true ) {
                        //'    StatusLabel.Text = "Upload status: mail sent!"
                        //'}else{
                        //'    StatusLabel.Text = "Upload status: The mail could not be sent.!"
                        //'}
                    }
                    else
                    {
                        RETRVQRY = "SELECT * FROM FKM_DOC WHERE DOC_FKM_LNK = '" + CMB_FKM_CD.SelectedItem.Value.ToString().ToUpper().Trim() +
                                    "' AND DOC_TYPE LIKE 'IRR CALC'  ";// 'AND COMP_CD = '" + COMP_CD + "' "
                        if (dbo.RecExist(RETRVQRY) == false)
                        {

                            string SRL = NEXT_DOCREC_SRL();

                            //' Dim FLNME As String() = Split(FileUploadControl.FileName, ".")
                            //'Dim filename As String = Path.GetFileName(FileUploadControl.FileName)
                            string PATHFL = "~/Documents/" + SRL + "." + FLNME[1];
                            FileUploadControl.SaveAs(Server.MapPath("~/Documents/") + SRL + "." + FLNME[1]);
                            //'StatusLabel.Text = "Upload status: File uploaded!"

                            string filename = Path.GetFileName(FileUploadControl.FileName);


                            string INSQRY = "INSERT INTO FKM_DOC VALUES('" + SRL + "','" + CMB_FKM_CD.SelectedItem.Value.ToString().ToUpper() +
                                                   "', 'IRR CALC','IRR Calculations ','" + PATHFL.ToUpper() + "','" + FLNME[0] + "','" + FLNME[1] +
                                                   "','','" + USR + "','" + UPDDT + "','" + UPDTIME + "')";

                            if (dbo.InsRecord(INSQRY) == false)
                            {
                                StatusLabel.Text = "Upload status: File upload failed !";
                            }
                            else
                            {
                                StatusLabel.Text = "Upload status: File uploaded!";
                                TXT_IRR_FLNM.Text = SRL + "." + FLNME[1];

                                DownloadButton.NavigateUrl = "~/Documents/" + TXT_IRR_FLNM.Text;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }

            }
            else
            {
                mbox("Please upload Excel file !!! ");
            }

        }
    }

    protected string NEXT_DOCREC_SRL()
    {
        string SRL = "";
        DataTable DTINVINF = new DataTable();
        SRL = "0000000001";
        try
        {
            string RETRVQRY = "SELECT MAX (DOC_SRL) FROM FKM_DOC ";
            DTINVINF = dbo.SelTable(RETRVQRY);
            if (DTINVINF.Rows.Count > 0)
            {
                Double NSRL = double.Parse(DTINVINF.Rows[0][0].ToString());

                SRL = (NSRL + 1).ToString("0000000000");
            }

        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }

        return SRL;
    }

    protected string NEXT_REC_SRL()
    {
        // 'TXT_REFSRL.Text = ""
        string SRL = "";
        DataTable DTINVINF = new DataTable();
        SRL = "0000000001";
        try
        {
            string RETRVQRY = "SELECT MAX (TRX_REFSRL) FROM INVEST_TRX ";
            DTINVINF = dbo.SelTable(RETRVQRY);
            if (DTINVINF.Rows.Count > 0)
            {
                Double NSRL = double.Parse(DTINVINF.Rows[0][0].ToString());

                SRL = (NSRL + 1).ToString("0000000000");
            }

        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }

        return SRL;
    }

    protected void InitGrid()
    {
        Session["INV_TRX_EDIT_dtGRID"] = null;
        Button1.Visible = false;

        if (CMB_FKM_CD.SelectedIndex < 0)
        {
            return;
        }

        string CODE = CMB_FKM_CD.SelectedValue;
        string sSQLSting = "SELECT  TRX_REFSRL, TRX_REFMEMO, TRX_ISSUEDATE, TRX_INVAMT, TRX_AMT, TRX_EXPNSAMT, TRX_ROI,TRX_NOF_DAYS, TRX_STATUS,  TRX_REMX, TRX_UPDBY " +
                                  " FROM INVEST_TRX WHERE TRX_FKM_SRL = '" + CODE + "'  ORDER BY     SUBSTRING(TRX_ISSUEDATE, 7, 4) + SUBSTRING(TRX_ISSUEDATE, 4, 2) + SUBSTRING(TRX_ISSUEDATE, 1, 2) , TRX_ISSMONT   ";
        DataTable dtGRID = dbo.SelTable(sSQLSting);

        if (dtGRID.Rows.Count > 0)
        {
            Button1.Visible = true;
            Session["INV_TRX_EDIT_dtGRID"] = dtGRID;
        }

    }

    protected void Button1_Click(object sender, System.EventArgs e) //Handles Button1.Click
    {
        ReportDocument rpt = new ReportDocument();
        string rname = Server.MapPath("~/Reports/IRRSUMMRPT.rpt");
        string dwnldfname = Server.MapPath("~/ReportsGenerate/IRRSUMMRPT.xls");
        DataTable dt = (DataTable)Session["INV_TRX_EDIT_dtGRID"];

        if (dt.Rows.Count > 0)
        {

            //        foreach(DataRow DR in dt.Rows)
            //{
            //            Dim I As String = 0
            //            I = 2

            //}
            rpt.Load(rname);
            rpt.SetDataSource(dt);

            // 'rpt.PrintOptions.PaperSize = PaperSize.PaperA4
            //'rpt.PrintOptions.PaperOrientation = PaperOrientation.Landscape
            rpt.ExportToDisk(ExportFormatType.Excel, dwnldfname);
            rpt.Close();
            rpt.Dispose();


            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("content-disposition", "attachment;filename =" + "IRRSUMMRPT.xls");
            Response.ContentType = "application/ms-excel";
            Response.TransmitFile(dwnldfname);

        }
    }

    protected void Button2_Click(object sender, System.EventArgs e)// Handles Button2.Click
    {
        string CODE = CMB_FKM_CD.SelectedValue;
        // 'Dim rname As String = Server.MapPath("~/Reports/IRRSUMMRPT.rpt")
        string dwnldfname = Server.MapPath(DownloadButton.NavigateUrl);
        //  '  Dim COMP_CD As String = dbo.GET_COMP_CD(CODE)

        string docQRY = "SELECT * FROM FKM_DOC WHERE  DOC_FKM_LNK='" + CODE + "' AND  DOC_TYPE LIKE 'IRR CALC' ";//' AND COMP_CD = '" + COMP_CD + "' "
        DataTable docinfo = dbo.SelTable(docQRY);
        if (docinfo.Rows.Count == 1)
        {

            TXT_IRR_FLNM.Text = docinfo.Rows[0]["DOC_SRL"].ToString().Trim() + "." + docinfo.Rows[0]["DOC_EXTN"].ToString().Trim();

            DownloadButton.NavigateUrl = "~/Documents/" + TXT_IRR_FLNM.Text;


            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("content-disposition", "attachment;filename =" + TXT_IRR_FLNM.Text);
            Response.ContentType = "application/ms-excel";
            Response.TransmitFile(dwnldfname);

        }


    }

    protected void INIT_FKMCD()
    {
        // 'Dim COMP_CD As String = Session("USER_COMP_CD")  WHERE COMP_CD = '" + COMP_CD + "'
        string RETRVQRY = "SELECT DISTINCT FKM_SRL ,FKM_SRL + '-' + FKM_PROJNAME AS FKM_PROJNAME  FROM INVEST_INFO  ORDER BY FKM_SRL";
        DataTable dtinfo = dbo.SelTable(RETRVQRY);
        CMB_FKM_CD.DataSource = dtinfo;
        CMB_FKM_CD.DataBind();
        CMB_FKM_CD.SelectedIndex = -1;
    }

    protected void GridView1_SelectedIndexChanged(object sender, System.EventArgs e)//Handles GridView1.SelectedIndexChanged
    {

        if (GridView1.SelectedDataKey[0].ToString().Trim().Length != 0)
        {
            string CODE = (string)GridView1.SelectedDataKey[0];
            CHBX_EDIT.Checked = true;
            TXT_REFSRL.Text = "";// 'NEXT_REC_SRL()
            TXT_REFSRL.Visible = false;
            CMB_REFSRL.SelectedIndex = -1;
            CMB_REFSRL.Visible = true;
            BTN_ADD.Visible = false;
            BTN_UPDT.Visible = true;
            CMB_REFSRL.DataBind();
            CMB_REFSRL_CHANGED(CODE);
        }
    }

    public void mbox(string val)
    {
        //string cs   = "c";
        //string cst  = "<script type=""text/javascript"">" + "alert('" + val + "');</" + "script>";
        //Page page    = HttpContext.Current.Handler;
        //page.ClientScript.RegisterStartupScript(Me.typeof, cs, cst);

        string USR = User.Identity.Name.ToUpper();
        dbo.ErrorLog(USR + " : INV_TRX_EDIT.aspx : " + val, Server.MapPath("~\\Logs\\ErrorLog"));
    }
}
