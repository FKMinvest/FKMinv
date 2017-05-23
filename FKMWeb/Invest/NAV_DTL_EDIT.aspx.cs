using System;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
public partial class INVEST_NAV_DTL_EDIT : Page
{
    fkminvcom dbo = new fkminvcom();
    // Visual C# .NET requires that you override the OnInit function,
    // adding a new delegate for the Page_Load event.

    override protected void OnInit(System.EventArgs e)
    {
        this.Load += new System.EventHandler(this.Page_Load);
        base.OnInit(e);
        CMB_FKM_CD.SelectedIndexChanged += new System.EventHandler(CMB_FKM_CD_SelectedIndexChanged); 
        CMB_NAV_QTR.SelectedIndexChanged += new System.EventHandler(CMB_NAV_QTR_SelectedIndexChanged);
        CMB_FKM_CD_QRY.SelectedIndexChanged += new System.EventHandler(CMB_FKM_CD_QRY_SelectedIndexChanged); 
        CMB_NAV_QTR_QRY.SelectedIndexChanged += new System.EventHandler(CMB_NAV_QTR_QRY_SelectedIndexChanged);
        GridView1.SelectedIndexChanged += new System.EventHandler(GridView1_SelectedIndexChanged);
        GridView1.RowDataBound += new System.Web.UI.WebControls.GridViewRowEventHandler(GridView1_RowDataBound); 
        BTN_ADD.Click += new System.EventHandler(BTN_ADD_Click);
        BTN_UPDT.Click += new System.EventHandler(BTN_UPDT_Click);
        BTN_CLEAR.Click += new System.EventHandler(BTN_CLEAR_Click);
        BTN_PROCESS.Click += new System.EventHandler(BTN_PROCESS_Click);
    }

    public void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {             
            INIT_QUARTER();
            INIT_FKMCD();
            INIT_QUARTER_QRY();
            INIT_FKMCD_QRY();
            CLEAR();
            CMB_FKM_CD.SelectedIndex = -1;
            // ' EDITING
            if ((Request.QueryString.Get("REFSRL")) != null)
            {
                string REFCODE = Request.QueryString.Get("REFSRL");
                string QTR = Request.QueryString.Get("QTR");
                CMB_NAV_QTR.SelectedIndex = CMB_NAV_QTR.Items.IndexOf(CMB_NAV_QTR.Items.FindByValue(QTR));
                CMB_FKM_CD.SelectedIndex = CMB_FKM_CD.Items.IndexOf(CMB_FKM_CD.Items.FindByValue(REFCODE));

                string CODE = CMB_FKM_CD.SelectedValue;
                string QUARTER = CMB_NAV_QTR.SelectedItem.Text;
                string QUARTERCD = CMB_NAV_QTR.Text.Trim();

                CMB_FKM_CD_Changed(CODE, QUARTER);

            }
        }

    }

    public void CLEAR()
    {
        //'CMB_FKM_CD.SelectedIndex = -1
        TXT_PROJNAME.Text = "";
        TXT_HOLDNAME.Text = "";
        TXT_OPPRBY.Text = "";
        TXT_INVCOMP.Text = "";
        CMB_INVGRP.SelectedIndex = -1;
        TXT_PRTCPDATE.Text = "";
        TXT_MTRTDATE.Text = "";
        TXT_LOCATION.Text = "";
        TXT_CURR.Text = "";
        TXT_BANK.Text = "";
        CMB_INCGEN.SelectedIndex = -1;
        CMB_PRVTEQT.SelectedIndex = -1;
        CMB_STATUS.SelectedIndex = -1;
        CMB_FKM_LNK.SelectedIndex = -1;
        CMB_COMP_CD.SelectedIndex = -1;

        TXT_COMMCAP.Text = "";
        TXT_COMMCAP2.Text = "";
        TXT_INVAMT.Text = "";
        TXT_CAPPD.Text = "";
        TXT_CAPUNPD.Text = "";
        TXT_CAPRFND.Text = "";
        TXT_EXPNS.Text = "";
        TXT_ROI.Text = "";
        TXT_BOOKVAL.Text = "";

        CMB_YEILDPRD.SelectedIndex = -1;
        TXT_MONYCAL.Text = "";
        TXT_QRTYCAL.Text = "";
        TXT_SMANYCAL.Text = "";
        TXT_ANLYCAL.Text = "";
        TXT_SCNINCP.Text = "";
        TXT_ANLINCMCY.Text = "";
        TXT_ANLRLZD.Text = "";
        TXT_ACTLINCMRU.Text = "";
        CMB_COMP_PRD.SelectedIndex = 0;
        CMB_CONSIDER_CI.SelectedIndex = 0;
        TXT_VALUATION_NEW.Text = "";
        TXT_UNRLYLD.Text = "";
        TXT_DVDND.Text = "";
        TXT_VALVTN.Text = "";
        TXT_CAPGN.Text = "";
        TXT_UNRLDVD.Text = "";
        TXT_UNRLDVDCAP.Text = "";
        TXT_FAIRVAL.Text = "";
        TXT_NAV.Text = "";
        TXT_SALEPRCD.Text = "";
        TXT_REMX.Text = "";
        BTN_ADD.Visible = true;
        BTN_UPDT.Visible = false;

        REFINE_QRY();
       // GET_GRID_INFO();
    }

    public void BTN_ADD_Click(object sender, System.EventArgs e)// Handles BTN_ADD.Click
    {
        if (CMB_FKM_CD.SelectedIndex < 0)
        {
            mbox("Please select the refrence !!");
            return;
        }


        //'Dim SRL As string = TXT_FKM_CD.Text
        string STR_TEMP = "";
        if (TXT_PROJNAME.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_PROJNAME.Text.Trim();
            //  ' Call ADD_REFRNC(STR_TEMP, "FKM_PROJNAME", "C")
        }
        string PROJNAME = STR_TEMP;

        STR_TEMP = "";
        if (TXT_HOLDNAME.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_HOLDNAME.Text.Trim();
            // 'Call ADD_REFRNC(STR_TEMP, "FKM_HOLDNAME", "C")
        }
        string HOLDNAME = STR_TEMP;

        STR_TEMP = "";
        if (TXT_OPPRBY.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_OPPRBY.Text.Trim();
            // '  Call ADD_REFRNC(STR_TEMP, "FKM_OPPRBY", "C")
        }
        string OPPRBY = STR_TEMP;

        STR_TEMP = "";
        if (TXT_INVCOMP.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_INVCOMP.Text.Trim();
            //'    Call ADD_REFRNC(STR_TEMP, "FKM_INVCOMP", "C")
        }
        string INVCOMP = STR_TEMP;

        STR_TEMP = "";
        if (CMB_INVGRP.Text.Trim().Length > 0)
        {
            STR_TEMP = CMB_INVGRP.Text.Trim();
            //  ' Call ADD_REFRNC(STR_TEMP, "FKM_INVGRP", "C")
        }
        string INVGRP = STR_TEMP;

        string PRTCPDATE = "null";
        if (TXT_PRTCPDATE.Text.Trim().Length > 0)
        {
            //'  Dim PDT As Date = CDate(TXT_PRTCPDATE.Text.Trim())
            PRTCPDATE = "'" + TXT_PRTCPDATE.Text.Trim() + "'";
        }

        string MTRTDATE = "null";
        if (TXT_MTRTDATE.Text.Trim().Length > 0)
        {
            //    ' Dim PDT As Date = CDate(TXT_MTRTDATE.Text.Trim())
            MTRTDATE = "'" + TXT_MTRTDATE.Text.Trim() + "'";
        }

        STR_TEMP = "";
        if (TXT_LOCATION.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_LOCATION.Text.Trim();
            //' Call ADD_REFRNC(STR_TEMP, "FKM_LOCATION", "C")
        }
        string LOCATION = STR_TEMP;

        STR_TEMP = "";
        if (TXT_CURR.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_CURR.Text.Trim();
            //  ' Call ADD_REFRNC(STR_TEMP, "FKM_CURR", "C")
        }
        string CURR = STR_TEMP;

        STR_TEMP = "";
        if (TXT_BANK.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_BANK.Text.Trim();
            //  ' Call ADD_REFRNC(STR_TEMP, "FKM_BANK", "C")
        }
        string BANK = STR_TEMP;


        string YEILDPRD = CMB_YEILDPRD.Text.Trim();
        string INCGEN = CMB_INCGEN.Text.Trim();
        string PRVTEQT = CMB_PRVTEQT.Text.Trim();
        string STATUS = CMB_STATUS.Text.Trim();
        string FKM_LNK = CMB_FKM_LNK.Text.Trim();
        string QUARTER = CMB_NAV_QTR.SelectedItem.Text;
        string QUARTERCD = CMB_NAV_QTR.Text.Trim();


        STR_TEMP = "0";
        if (TXT_COMMCAP.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_COMMCAP.Text.Trim();
        }
        Double COMMCAP = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_COMMCAP2.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_COMMCAP2.Text.Trim();
        }
        Double COMMCAP2 = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_INVAMT.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_INVAMT.Text.Trim();
        }
        Double INVAMT = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_CAPPD.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_CAPPD.Text.Trim();
        }
        Double CAPPD = double.Parse(STR_TEMP);


        STR_TEMP = "0";
        if (TXT_CAPUNPD.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_CAPUNPD.Text.Trim();
        }
        Double CAPUNPD = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_CAPRFND.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_CAPRFND.Text.Trim();
        }
        Double CAPRFND = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_EXPNS.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_EXPNS.Text.Trim();
        }
        Double EXPNS = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_ROI.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_ROI.Text.Trim();
        }
        Double ROI = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_BOOKVAL.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_BOOKVAL.Text.Trim();
        }
        Double BOOKVAL = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_MONYCAL.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_MONYCAL.Text.Trim();
        }
        Double MYCAL = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_QRTYCAL.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_QRTYCAL.Text.Trim();
        }
        Double QYCAL = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_SMANYCAL.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_SMANYCAL.Text.Trim();
        }
        Double SYCAL = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_ANLYCAL.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_ANLYCAL.Text.Trim();
        }
        Double AYCAL = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_SCNINCP.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_SCNINCP.Text.Trim();
        }
        Double SCNINCP = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_ANLINCMCY.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_ANLINCMCY.Text.Trim();
        }
        Double ANLINCMCY = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_ANLRLZD.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_ANLRLZD.Text.Trim();
        }
        Double ANLRLZD = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_ACTLINCMRU.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_ACTLINCMRU.Text.Trim();
        }
        Double ACTLINCMRU = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_UNRLYLD.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_UNRLYLD.Text.Trim();
        }
        Double UNRLYLD = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_DVDND.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_DVDND.Text.Trim();
        }
        Double DVDND = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_VALVTN.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_VALVTN.Text.Trim();
        }
        Double VALVTN = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_CAPGN.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_CAPGN.Text.Trim();
        }
        Double CAPGN = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_UNRLDVD.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_UNRLDVD.Text.Trim();
        }
        Double UNRLDVD = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_UNRLDVDCAP.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_UNRLDVDCAP.Text.Trim();
        }
        Double UNRLDVDCAP = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_FAIRVAL.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_FAIRVAL.Text.Trim();
        }
        Double FAIRVAL = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_NAV.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_NAV.Text.Trim();
        }
        Double NAV = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_SALEPRCD.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_SALEPRCD.Text.Trim();
        }
        Double SALEPRCD = double.Parse(STR_TEMP);

        string USR = User.Identity.Name.ToUpper();
        string UPDDT = System.DateTime.Today.ToString("dd/MM/yyyy");
        string UPDTIME = System.DateTime.Now.ToString("hh:mm:ss");
        string CODE = CMB_FKM_CD.SelectedValue;

        try
        {
            string COMP_CD = CMB_COMP_CD.SelectedItem.Value.Trim();
            string RETRVQRY = "SELECT *  FROM INVEST_INFO WHERE FKM_SRL = '" + CODE + "' AND FKM_HD1 IS NULL  ";//'AND COMP_CD = '" + COMP_CD + "' "


            if (dbo.RecExist(RETRVQRY) == true)
            {
                RETRVQRY = "INSERT  INTO NAV_INFO  SELECT * FROM INVEST_INFO WHERE FKM_SRL = '" + CODE + "' AND FKM_HD1 IS NULL ";

                if (dbo.Transact(RETRVQRY, "", "", "", "") == false)
                {
                    mbox("ERROR !!");
                }
                else
                {

                    string INSQRY = "UPDATE NAV_INFO SET FKM_ISSDATE = '" + QUARTERCD + "',FKM_CURR ='" + CURR + "',FKM_PROJNAME ='" + PROJNAME +
                                           "',FKM_HOLDNAME = '" + HOLDNAME + "',FKM_LOCATION = '" + LOCATION + "',FKM_OPPRBY = '" + OPPRBY + "',FKM_INVCOMP = '" + INVCOMP + "',FKM_INVGRP = '" + INVGRP +
                                           "',FKM_PRTCPDATE = " + PRTCPDATE + ",FKM_MTRTDATE = " + MTRTDATE + ",FKM_PRVTEQT = '" + PRVTEQT + "',FKM_BANK = '" + BANK +
                                           "',FKM_YEILDPRD = '" + YEILDPRD + "',FKM_INCGEN = '" + INCGEN + "',FKM_STATUS = '" + STATUS + "',FKM_LNKSRL = '" + FKM_LNK +
                                           "',FKM_COMMCAP = '" + COMMCAP + "',FKM_COMMCAP2 = '" + COMMCAP2 + "',FKM_INVAMT = '" + INVAMT + "',FKM_CAPPD = '" + CAPPD +
                                           "',FKM_CAPUNPD = '" + CAPUNPD + "',FKM_CAPRFND = '" + CAPRFND + "',FKM_EXPNS = '" + EXPNS + "',FKM_ROI = '" + ROI +
                                           "',FKM_BOOKVAL = '" + BOOKVAL + "',FKM_MONYCAL = '" + MYCAL + "',FKM_QRTYCAL = '" + QYCAL +
                                           "',FKM_SMANYCAL = '" + SYCAL + "',FKM_ANLYCAL = '" + AYCAL + "',FKM_SCNINCP = '" + SCNINCP +
                                           "',FKM_ANLINCMCY = '" + ANLINCMCY + "',FKM_ANLRLZD = '" + ANLRLZD + "',FKM_ACTLINCMRU = '" + ACTLINCMRU +
                                           "',FKM_UNRLYLD = '" + UNRLYLD + "',FKM_DVDND = '" + DVDND + "',FKM_VALVTN = '" + VALVTN + "',FKM_CAPGN = '" + CAPGN +
                                           "',FKM_UNRLDVD = '" + UNRLDVD + "',FKM_UNRLDVDCAP = '" + UNRLDVDCAP + "',FKM_FAIRVAL = '" + FAIRVAL +
                                           "',FKM_NAV = '" + NAV + "',FKM_SALEPRCD = '" + SALEPRCD + "',FKM_REMX_NAV = '" + TXT_REMX.Text +
                                           "',FKM_UPDBY = '" + USR + "',FKM_UPDDATE = '" + UPDDT + "',FKM_UPDTIME = '" + UPDTIME + "' , FKM_HD1 = '" + QUARTER + "'" +
                                           " WHERE  FKM_SRL = '" + CODE + "'  AND FKM_HD1 IS NULL ";//'AND COMP_CD = '" + COMP_CD + "' "

                    if (dbo.Transact(INSQRY, "", "", "", "") == false)
                    {
                        mbox("ERROR !!");
                    }
                    else
                    {
                        if (dbo.sendmail((string)Session["USER_MAIL_ID"], PROJNAME + " INVESTMENT DETAILS ADDED #" + CMB_NAV_QTR.SelectedItem.Text, "Test") == true)
                        {
                            // ' StatusLabel.Text = "Upload status: mail sent!"
                        }
                        else
                        {
                            // ' StatusLabel.Text = "Upload status: The mail could not be sent.!"
                        }
                        BTN_CLEAR_Click(sender, e);
                        CMB_FKM_CD.SelectedIndex = -1;
                        mbox("FKM SRL :  '" + CODE + "'   ADDED SUCCESSFULLY !!");
                    }
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

    public void BTN_UPDT_Click(object sender, System.EventArgs e)// Handles BTN_UPDT.Click
    {

        if (CMB_FKM_CD.SelectedIndex < 0)
        {
            mbox("Please select the refrence !!");
            return;
        }


        //'Dim SRL As string = TXT_FKM_CD.Text
        string STR_TEMP = "";
        if (TXT_PROJNAME.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_PROJNAME.Text.Trim();
            //  ' Call ADD_REFRNC(STR_TEMP, "FKM_PROJNAME", "C")
        }
        string PROJNAME = STR_TEMP;

        STR_TEMP = "";
        if (TXT_HOLDNAME.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_HOLDNAME.Text.Trim();
            // 'Call ADD_REFRNC(STR_TEMP, "FKM_HOLDNAME", "C")
        }
        string HOLDNAME = STR_TEMP;

        STR_TEMP = "";
        if (TXT_OPPRBY.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_OPPRBY.Text.Trim();
            // '  Call ADD_REFRNC(STR_TEMP, "FKM_OPPRBY", "C")
        }
        string OPPRBY = STR_TEMP;

        STR_TEMP = "";
        if (TXT_INVCOMP.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_INVCOMP.Text.Trim();
            //'    Call ADD_REFRNC(STR_TEMP, "FKM_INVCOMP", "C")
        }
        string INVCOMP = STR_TEMP;

        STR_TEMP = "";
        if (CMB_INVGRP.Text.Trim().Length > 0)
        {
            STR_TEMP = CMB_INVGRP.Text.Trim();
            //  ' Call ADD_REFRNC(STR_TEMP, "FKM_INVGRP", "C")
        }
        string INVGRP = STR_TEMP;

        string PRTCPDATE = "null";
        if (TXT_PRTCPDATE.Text.Trim().Length > 0)
        {
            //'  Dim PDT As Date = CDate(TXT_PRTCPDATE.Text.Trim())
            PRTCPDATE = "'" + TXT_PRTCPDATE.Text.Trim() + "'";
        }

        string MTRTDATE = "null";
        if (TXT_MTRTDATE.Text.Trim().Length > 0)
        {
            //    ' Dim PDT As Date = CDate(TXT_MTRTDATE.Text.Trim())
            MTRTDATE = "'" + TXT_MTRTDATE.Text.Trim() + "'";
        }

        STR_TEMP = "";
        if (TXT_LOCATION.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_LOCATION.Text.Trim();
            //' Call ADD_REFRNC(STR_TEMP, "FKM_LOCATION", "C")
        }
        string LOCATION = STR_TEMP;

        STR_TEMP = "";
        if (TXT_CURR.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_CURR.Text.Trim();
            //  ' Call ADD_REFRNC(STR_TEMP, "FKM_CURR", "C")
        }
        string CURR = STR_TEMP;

        STR_TEMP = "";
        if (TXT_BANK.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_BANK.Text.Trim();
            //  ' Call ADD_REFRNC(STR_TEMP, "FKM_BANK", "C")
        }
        string BANK = STR_TEMP;


        string YEILDPRD = CMB_YEILDPRD.Text.Trim();
        string INCGEN = CMB_INCGEN.Text.Trim();
        string PRVTEQT = CMB_PRVTEQT.Text.Trim();
        string STATUS = CMB_STATUS.Text.Trim();
        string FKM_LNK = CMB_FKM_LNK.Text.Trim();
        string QUARTER = CMB_NAV_QTR.SelectedItem.Text;
        string QUARTERCD = CMB_NAV_QTR.Text.Trim();


        STR_TEMP = "0";
        if (TXT_COMMCAP.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_COMMCAP.Text.Trim();
        }
        Double COMMCAP = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_COMMCAP2.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_COMMCAP2.Text.Trim();
        }
        Double COMMCAP2 = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_INVAMT.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_INVAMT.Text.Trim();
        }
        Double INVAMT = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_CAPPD.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_CAPPD.Text.Trim();
        }
        Double CAPPD = double.Parse(STR_TEMP);


        STR_TEMP = "0";
        if (TXT_CAPUNPD.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_CAPUNPD.Text.Trim();
        }
        Double CAPUNPD = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_CAPRFND.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_CAPRFND.Text.Trim();
        }
        Double CAPRFND = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_EXPNS.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_EXPNS.Text.Trim();
        }
        Double EXPNS = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_ROI.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_ROI.Text.Trim();
        }
        Double ROI = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_BOOKVAL.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_BOOKVAL.Text.Trim();
        }
        Double BOOKVAL = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_MONYCAL.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_MONYCAL.Text.Trim();
        }
        Double MYCAL = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_QRTYCAL.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_QRTYCAL.Text.Trim();
        }
        Double QYCAL = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_SMANYCAL.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_SMANYCAL.Text.Trim();
        }
        Double SYCAL = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_ANLYCAL.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_ANLYCAL.Text.Trim();
        }
        Double AYCAL = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_SCNINCP.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_SCNINCP.Text.Trim();
        }
        Double SCNINCP = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_ANLINCMCY.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_ANLINCMCY.Text.Trim();
        }
        Double ANLINCMCY = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_ANLRLZD.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_ANLRLZD.Text.Trim();
        }
        Double ANLRLZD = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_ACTLINCMRU.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_ACTLINCMRU.Text.Trim();
        }
        Double ACTLINCMRU = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_UNRLYLD.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_UNRLYLD.Text.Trim();
        }
        Double UNRLYLD = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_DVDND.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_DVDND.Text.Trim();
        }
        Double DVDND = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_VALVTN.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_VALVTN.Text.Trim();
        }
        Double VALVTN = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_CAPGN.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_CAPGN.Text.Trim();
        }
        Double CAPGN = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_UNRLDVD.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_UNRLDVD.Text.Trim();
        }
        Double UNRLDVD = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_UNRLDVDCAP.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_UNRLDVDCAP.Text.Trim();
        }
        Double UNRLDVDCAP = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_FAIRVAL.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_FAIRVAL.Text.Trim();
        }
        Double FAIRVAL = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_NAV.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_NAV.Text.Trim();
        }
        Double NAV = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_SALEPRCD.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_SALEPRCD.Text.Trim();
        }
        Double SALEPRCD = double.Parse(STR_TEMP);

        string USR = User.Identity.Name.ToUpper();
        string UPDDT = System.DateTime.Today.ToString("dd/MM/yyyy");
        string UPDTIME = System.DateTime.Now.ToString("hh:mm:ss");
        string CODE = CMB_FKM_CD.SelectedValue;

        try
        {
            string COMP_CD = CMB_COMP_CD.SelectedItem.Value.Trim();
            string RETRVQRY = "SELECT *  FROM NAV_INFO WHERE FKM_SRL = '" + CODE + "' AND FKM_HD1 = '" + QUARTER + "' ";//' AND COMP_CD = '" + COMP_CD + "'  "

            if (dbo.RecExist(RETRVQRY) == true)
            {


                string INSQRY = "UPDATE NAV_INFO SET FKM_ISSDATE = '" + QUARTERCD + "',FKM_CURR ='" + CURR + "',FKM_PROJNAME ='" + PROJNAME +
                                       "',FKM_HOLDNAME = '" + HOLDNAME + "',FKM_LOCATION = '" + LOCATION + "',FKM_OPPRBY = '" + OPPRBY +
                                       "',FKM_INVCOMP = '" + INVCOMP + "',FKM_INVGRP = '" + INVGRP +
                                       "',FKM_PRTCPDATE = " + PRTCPDATE + ",FKM_MTRTDATE = " + MTRTDATE + ",FKM_PRVTEQT = '" + PRVTEQT + "',FKM_BANK = '" + BANK +
                                       "',FKM_YEILDPRD = '" + YEILDPRD + "',FKM_INCGEN = '" + INCGEN + "',FKM_STATUS = '" + STATUS + "',FKM_LNKSRL = '" + FKM_LNK +
                                       "',FKM_COMMCAP = '" + COMMCAP + "',FKM_COMMCAP2 = '" + COMMCAP2 + "',FKM_INVAMT = '" + INVAMT + "',FKM_CAPPD = '" + CAPPD +
                                       "',FKM_CAPUNPD = '" + CAPUNPD + "',FKM_CAPRFND = '" + CAPRFND + "',FKM_EXPNS = '" + EXPNS + "',FKM_ROI = '" + ROI +
                                       "',FKM_BOOKVAL = '" + BOOKVAL + "',FKM_MONYCAL = '" + MYCAL + "',FKM_QRTYCAL = '" + QYCAL +
                                       "',FKM_SMANYCAL = '" + SYCAL + "',FKM_ANLYCAL = '" + AYCAL + "',FKM_SCNINCP = '" + SCNINCP +
                                       "',FKM_ANLINCMCY = '" + ANLINCMCY + "',FKM_ANLRLZD = '" + ANLRLZD + "',FKM_ACTLINCMRU = '" + ACTLINCMRU +
                                       "',FKM_UNRLYLD = '" + UNRLYLD + "',FKM_DVDND = '" + DVDND + "',FKM_VALVTN = '" + VALVTN + "',FKM_CAPGN = '" + CAPGN +
                                       "',FKM_UNRLDVD = '" + UNRLDVD + "',FKM_UNRLDVDCAP = '" + UNRLDVDCAP + "',FKM_FAIRVAL = '" + FAIRVAL +
                                       "',FKM_NAV = '" + NAV + "',FKM_SALEPRCD = '" + SALEPRCD + "',FKM_REMX_NAV = '" + TXT_REMX.Text +
                                       "',FKM_UPDBY = '" + USR + "',FKM_UPDDATE = '" + UPDDT + "',FKM_UPDTIME = '" + UPDTIME + "' , FKM_HD1 = '" + QUARTER + "'" +
                                       " WHERE  FKM_SRL = '" + CODE + "'  AND  FKM_ISSDATE = '" + QUARTERCD + "'  ";// ' AND COMP_CD = '" + COMP_CD + "'  "

                if (dbo.Transact(INSQRY, "", "", "", "") == false)
                {
                    mbox("ERROR !!");
                }
                else
                {
                    if (dbo.sendmail((string)Session["USER_MAIL_ID"], PROJNAME + " INVESTMENT DETAILS UPDATED #" + CMB_NAV_QTR.SelectedItem.Text, "Test") == true)
                    {
                        // ' StatusLabel.Text = "Upload status: mail sent!"
                    }
                    else
                    {
                        //' StatusLabel.Text = "Upload status: The mail could not be sent.!"
                    }
                    BTN_CLEAR_Click(sender, e);
                    CMB_FKM_CD.SelectedIndex = -1;
                    mbox("FKM SRL :  '" + CODE + "'   ADDED SUCCESSFULLY !!");
                }
            }
            else
            {
                mbox("FKM SRL :  '" + CODE + "' DOESNOT  EXIST FOR '" + QUARTER + "'  !!");
            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }

    }


    public void BTN_CLEAR_Click(object sender, System.EventArgs e)// Handles BTN_CLEAR.Click
    {
        CLEAR();
    }

    public void CMB_FKM_CD_SelectedIndexChanged(object sender, System.EventArgs e)// Handles CMB_FKM_CD.SelectedIndexChanged
    {
        if (CMB_FKM_CD.SelectedIndex > -1)
        {
            string CODE = CMB_FKM_CD.SelectedValue;
            //   ' Dim QUARTER As string = CMB_NAV_QTR.SelectedItem.Text
            string QUARTER = CMB_NAV_QTR.SelectedValue;
            string QUARTERCD = CMB_NAV_QTR.Text.Trim();
            CMB_FKM_CD_Changed(CODE, QUARTER);
        }

    }

    public void CMB_NAV_QTR_SelectedIndexChanged(object sender, System.EventArgs e)// Handles CMB_NAV_QTR.SelectedIndexChanged
    {
        if (CMB_FKM_CD.SelectedIndex > -1)
        {
            string CODE = CMB_FKM_CD.SelectedValue;
            //   ' Dim QUARTER As string = CMB_NAV_QTR.SelectedItem.Text
            string QUARTER = CMB_NAV_QTR.SelectedValue;
            string QUARTERCD = CMB_NAV_QTR.Text.Trim();
            CMB_FKM_CD_Changed(CODE, QUARTER);
        }
    }

    public void CMB_FKM_CD_Changed(string CODE, string QUARTER)
    {
        //'Dim SRL As string = "0"
        CLEAR();
        try
        {
            //  '  Dim COMP_CD As string = dbo.GET_COMP_CD(CODE)
            string RETRVQRY = "SELECT * FROM NAV_INFO WHERE FKM_SRL = '" + CODE + "' AND  FKM_ISSDATE LIKE '" + QUARTER + "' ";// ' AND COMP_CD = '" + COMP_CD + "' "
            DataTable dtinfo = dbo.SelTable(RETRVQRY);
            if (dtinfo.Rows.Count == 1)
            {
                //'CMB_PROJNAME.SelectedIndex = CMB_PROJNAME.Items.IndexOf(CMB_PROJNAME.Items.FindByValue(dtinfo.Rows[0]["FKM_PROJNAME").ToString))
                //'CMB_HOLDNAME.SelectedIndex = CMB_HOLDNAME.Items.IndexOf(CMB_HOLDNAME.Items.FindByValue(dtinfo.Rows[0]["FKM_HOLDNAME").ToString))
                //'CMB_OPPRBY.SelectedIndex = CMB_OPPRBY.Items.IndexOf(CMB_OPPRBY.Items.FindByValue(dtinfo.Rows[0]["FKM_OPPRBY").ToString))
                //'CMB_INVCOMP.SelectedIndex = CMB_INVCOMP.Items.IndexOf(CMB_INVCOMP.Items.FindByValue(dtinfo.Rows[0]["FKM_INVCOMP").ToString))
                if ((dtinfo.Rows[0]["FKM_INVGRP"]) != null)
                {
                    CMB_INVGRP.SelectedIndex = CMB_INVGRP.Items.IndexOf(CMB_INVGRP.Items.FindByValue(dtinfo.Rows[0]["FKM_INVGRP"].ToString()));
                }
                //'TXT_PRTCPDATE.Text = dtinfo.Rows[0]["FKM_PRTCPDATE"].ToString().Trim()
                //'TXT_MTRTDATE.Text = dtinfo.Rows[0]["FKM_MTRTDATE"].ToString().Trim()
                //'CMB_LOCATION.SelectedIndex = CMB_LOCATION.Items.IndexOf(CMB_LOCATION.Items.FindByValue(dtinfo.Rows[0]["FKM_LOCATION").ToString))
                //'CMB_CURR.SelectedIndex = CMB_CURR.Items.IndexOf(CMB_CURR.Items.FindByValue(dtinfo.Rows[0]["FKM_CURR").ToString))
                //'CMB_BANK.SelectedIndex = CMB_BANK.Items.IndexOf(CMB_BANK.Items.FindByValue(dtinfo.Rows[0]["FKM_BANK").ToString))
                CMB_YEILDPRD.SelectedIndex = CMB_YEILDPRD.Items.IndexOf(CMB_YEILDPRD.Items.FindByValue(dtinfo.Rows[0]["FKM_YEILDPRD"].ToString().Trim()));
                CMB_INCGEN.SelectedIndex = CMB_INCGEN.Items.IndexOf(CMB_INCGEN.Items.FindByValue(dtinfo.Rows[0]["FKM_INCGEN"].ToString().Trim()));
                CMB_PRVTEQT.SelectedIndex = CMB_PRVTEQT.Items.IndexOf(CMB_PRVTEQT.Items.FindByValue(dtinfo.Rows[0]["FKM_PRVTEQT"].ToString().Trim()));
                CMB_STATUS.SelectedIndex = CMB_STATUS.Items.IndexOf(CMB_STATUS.Items.FindByValue(dtinfo.Rows[0]["FKM_STATUS"].ToString().Trim()));
                CMB_FKM_LNK.SelectedIndex = CMB_FKM_LNK.Items.IndexOf(CMB_FKM_LNK.Items.FindByValue(dtinfo.Rows[0]["FKM_LNKSRL"].ToString().Trim()));
                CMB_COMP_CD.SelectedIndex = CMB_COMP_CD.Items.IndexOf(CMB_COMP_CD.Items.FindByValue(dtinfo.Rows[0]["COMP_CD"].ToString().Trim()));

                TXT_PROJNAME.Text = dtinfo.Rows[0]["FKM_PROJNAME"].ToString().Trim();
                TXT_HOLDNAME.Text = dtinfo.Rows[0]["FKM_HOLDNAME"].ToString().Trim();
                TXT_OPPRBY.Text = dtinfo.Rows[0]["FKM_OPPRBY"].ToString().Trim();
                TXT_INVCOMP.Text = dtinfo.Rows[0]["FKM_INVCOMP"].ToString().Trim();
                TXT_PRTCPDATE.Text = dtinfo.Rows[0]["FKM_PRTCPDATE"].ToString().Trim();
                TXT_MTRTDATE.Text = dtinfo.Rows[0]["FKM_MTRTDATE"].ToString().Trim();
                TXT_LOCATION.Text = dtinfo.Rows[0]["FKM_LOCATION"].ToString().Trim();
                TXT_CURR.Text = dtinfo.Rows[0]["FKM_CURR"].ToString().Trim();
                TXT_BANK.Text = dtinfo.Rows[0]["FKM_BANK"].ToString().Trim();
                //'CMB_YEILDPRD.Text = dtinfo.Rows[0]["FKM_YEILDPRD"].ToString().Trim()
                //'CMB_INCGEN.Text = dtinfo.Rows[0]["FKM_INCGEN"].ToString().Trim()
                //'CMB_PRVTEQT.Text = dtinfo.Rows[0]["FKM_PRVTEQT"].ToString().Trim()
                //'CMB_STATUS.Text = dtinfo.Rows[0]["FKM_STATUS"].ToString().Trim()
                //'CMB_FKM_LNK.Text = dtinfo.Rows[0]["FKM_LNKSRL"].ToString().Trim()

                TXT_COMMCAP.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_COMMCAP"]);
                TXT_COMMCAP2.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_COMMCAP2"]);
                TXT_INVAMT.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_INVAMT"]);
                TXT_CAPPD.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_CAPPD"]);
                TXT_CAPUNPD.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_CAPUNPD"]);
                TXT_CAPRFND.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_CAPRFND"]);
                TXT_EXPNS.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_EXPNS"]);
                TXT_ROI.Text = dtinfo.Rows[0]["FKM_ROI"].ToString().Trim();
                TXT_BOOKVAL.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_BOOKVAL"]);

                TXT_MONYCAL.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_MONYCAL"]);
                TXT_QRTYCAL.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_QRTYCAL"]);
                TXT_SMANYCAL.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_SMANYCAL"]);
                TXT_ANLYCAL.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_ANLYCAL"]);
                TXT_SCNINCP.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_SCNINCP"]);
                TXT_ANLINCMCY.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_ANLINCMCY"]);
                TXT_ANLRLZD.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_ANLRLZD"]);
                TXT_ACTLINCMRU.Text = dtinfo.Rows[0]["FKM_ACTLINCMRU"].ToString().Trim();// ' string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_ACTLINCMRU"))

                TXT_UNRLYLD.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_UNRLYLD"]);
                TXT_DVDND.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_DVDND"]);
                TXT_VALVTN.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_VALVTN"]);
                TXT_CAPGN.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_CAPGN"]);
                TXT_UNRLDVD.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_UNRLDVD"]);
                TXT_UNRLDVDCAP.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_UNRLDVDCAP"]);
                TXT_FAIRVAL.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_FAIRVAL"]);
                TXT_NAV.Text = dtinfo.Rows[0]["FKM_NAV"].ToString().Trim();
                TXT_SALEPRCD.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_SALEPRCD"]);
                TXT_REMX.Text = dtinfo.Rows[0]["FKM_REMX_NAV"].ToString().Trim();

                TXT_VALUATION_NEW.Text = string.Format("{0:#,##0}", 0);
                BTN_ADD.Visible = false;
                BTN_UPDT.Visible = true;
            }
            else
            {

                RETRVQRY = "SELECT * FROM INVEST_INFO WHERE FKM_SRL = '" + CODE + "' ";// 'AND COMP_CD = '" + COMP_CD + "' "
                dtinfo = dbo.SelTable(RETRVQRY);
                if (dtinfo.Rows.Count == 1)
                {
                    //'CMB_PROJNAME.SelectedIndex = CMB_PROJNAME.Items.IndexOf(CMB_PROJNAME.Items.FindByValue(dtinfo.Rows[0]["FKM_PROJNAME").ToString))
                    //'CMB_HOLDNAME.SelectedIndex = CMB_HOLDNAME.Items.IndexOf(CMB_HOLDNAME.Items.FindByValue(dtinfo.Rows[0]["FKM_HOLDNAME").ToString))
                    //'CMB_OPPRBY.SelectedIndex = CMB_OPPRBY.Items.IndexOf(CMB_OPPRBY.Items.FindByValue(dtinfo.Rows[0]["FKM_OPPRBY").ToString))
                    //'CMB_INVCOMP.SelectedIndex = CMB_INVCOMP.Items.IndexOf(CMB_INVCOMP.Items.FindByValue(dtinfo.Rows[0]["FKM_INVCOMP").ToString))
                    if (dtinfo.Rows[0]["FKM_INVGRP"] != null)
                    {
                        CMB_INVGRP.SelectedIndex = CMB_INVGRP.Items.IndexOf(CMB_INVGRP.Items.FindByValue(dtinfo.Rows[0]["FKM_INVGRP"].ToString()));
                    }
                    //'TXT_PRTCPDATE.Text = dtinfo.Rows[0]["FKM_PRTCPDATE"].ToString().Trim()
                    //'TXT_MTRTDATE.Text = dtinfo.Rows[0]["FKM_MTRTDATE"].ToString().Trim()
                    //'CMB_LOCATION.SelectedIndex = CMB_LOCATION.Items.IndexOf(CMB_LOCATION.Items.FindByValue(dtinfo.Rows[0]["FKM_LOCATION").ToString))
                    //'CMB_CURR.SelectedIndex = CMB_CURR.Items.IndexOf(CMB_CURR.Items.FindByValue(dtinfo.Rows[0]["FKM_CURR").ToString))
                    //'CMB_BANK.SelectedIndex = CMB_BANK.Items.IndexOf(CMB_BANK.Items.FindByValue(dtinfo.Rows[0]["FKM_BANK").ToString))
                    CMB_YEILDPRD.SelectedIndex = CMB_YEILDPRD.Items.IndexOf(CMB_YEILDPRD.Items.FindByValue(dtinfo.Rows[0]["FKM_YEILDPRD"].ToString().Trim()));
                    CMB_INCGEN.SelectedIndex = CMB_INCGEN.Items.IndexOf(CMB_INCGEN.Items.FindByValue(dtinfo.Rows[0]["FKM_INCGEN"].ToString().Trim()));
                    CMB_PRVTEQT.SelectedIndex = CMB_PRVTEQT.Items.IndexOf(CMB_PRVTEQT.Items.FindByValue(dtinfo.Rows[0]["FKM_PRVTEQT"].ToString().Trim()));
                    CMB_STATUS.SelectedIndex = CMB_STATUS.Items.IndexOf(CMB_STATUS.Items.FindByValue(dtinfo.Rows[0]["FKM_STATUS"].ToString().Trim()));
                    CMB_FKM_LNK.SelectedIndex = CMB_FKM_LNK.Items.IndexOf(CMB_FKM_LNK.Items.FindByValue(dtinfo.Rows[0]["FKM_LNKSRL"].ToString().Trim()));
                    CMB_COMP_CD.SelectedIndex = CMB_COMP_CD.Items.IndexOf(CMB_COMP_CD.Items.FindByValue(dtinfo.Rows[0]["COMP_CD"].ToString().Trim()));

                    TXT_PROJNAME.Text = dtinfo.Rows[0]["FKM_PROJNAME"].ToString().Trim();
                    TXT_HOLDNAME.Text = dtinfo.Rows[0]["FKM_HOLDNAME"].ToString().Trim();
                    TXT_OPPRBY.Text = dtinfo.Rows[0]["FKM_OPPRBY"].ToString().Trim();
                    TXT_INVCOMP.Text = dtinfo.Rows[0]["FKM_INVCOMP"].ToString().Trim();
                    TXT_PRTCPDATE.Text = dtinfo.Rows[0]["FKM_PRTCPDATE"].ToString().Trim();
                    TXT_MTRTDATE.Text = dtinfo.Rows[0]["FKM_MTRTDATE"].ToString().Trim();
                    TXT_LOCATION.Text = dtinfo.Rows[0]["FKM_LOCATION"].ToString().Trim();
                    TXT_CURR.Text = dtinfo.Rows[0]["FKM_CURR"].ToString().Trim();
                    TXT_BANK.Text = dtinfo.Rows[0]["FKM_BANK"].ToString().Trim();
                    //'CMB_YEILDPRD.Text = dtinfo.Rows[0]["FKM_YEILDPRD"].ToString().Trim()
                    //'CMB_INCGEN.Text = dtinfo.Rows[0]["FKM_INCGEN"].ToString().Trim()
                    //'CMB_PRVTEQT.Text = dtinfo.Rows[0]["FKM_PRVTEQT"].ToString().Trim()
                    //'CMB_STATUS.Text = dtinfo.Rows[0]["FKM_STATUS"].ToString().Trim()
                    //'CMB_FKM_LNK.Text = dtinfo.Rows[0]["FKM_LNKSRL"].ToString().Trim()

                    TXT_COMMCAP.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_COMMCAP"]);
                    TXT_COMMCAP2.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_COMMCAP2"]);
                    TXT_INVAMT.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_INVAMT"]);
                    TXT_CAPPD.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_CAPPD"]);
                    TXT_CAPUNPD.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_CAPUNPD"]);
                    TXT_CAPRFND.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_CAPRFND"]);
                    TXT_EXPNS.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_EXPNS"]);
                    TXT_ROI.Text = dtinfo.Rows[0]["FKM_ROI"].ToString().Trim();
                    TXT_BOOKVAL.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_BOOKVAL"]);

                    TXT_MONYCAL.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_MONYCAL"]);
                    TXT_QRTYCAL.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_QRTYCAL"]);
                    TXT_SMANYCAL.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_SMANYCAL"]);
                    TXT_ANLYCAL.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_ANLYCAL"]);
                    TXT_SCNINCP.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_SCNINCP"]);
                    TXT_ANLINCMCY.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_ANLINCMCY"]);
                    TXT_ANLRLZD.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_ANLRLZD"]);
                    TXT_ACTLINCMRU.Text = dtinfo.Rows[0]["FKM_ACTLINCMRU"].ToString().Trim();

                    TXT_UNRLYLD.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_UNRLYLD"]);
                    TXT_DVDND.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_DVDND"]);
                    TXT_VALUATION_NEW.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_VALVTN"]);
                    TXT_CAPGN.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_CAPGN"]);
                    TXT_UNRLDVD.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_UNRLDVD"]);
                    TXT_UNRLDVDCAP.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_UNRLDVDCAP"]);
                    TXT_FAIRVAL.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_FAIRVAL"]);
                    TXT_NAV.Text = dtinfo.Rows[0]["FKM_NAV"].ToString().Trim();
                    TXT_SALEPRCD.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_SALEPRCD"]);
                    TXT_REMX.Text = dtinfo.Rows[0]["FKM_REMX_NAV"].ToString().Trim();

                     TXT_VALVTN.Text = string.Format("{0:#,##0}",0);

                    TXT_LIBORRATE.Text = "1";
                    BTN_ADD.Visible = true;
                    BTN_UPDT.Visible = false;

                    Init_INVEST_TRX(CODE);
                    //Process_INVEST_TRX(CODE);
                }
                else
                {
                    mbox("FKM SRL :  '" + CODE + "'  DOESN'T EXIST !!");
                }
            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }
    }


    protected void Init_INVEST_TRX(string CODE)
    {
        Session["NAV_EDIT_dtINVEST_TRX"] = null; 

        if (CMB_FKM_CD.SelectedIndex < 0)
        {
            return;
        }


        string sSQLSting = "SELECT  TRX_REFSRL, TRX_REFMEMO, TRX_ISSUEDATE, TRX_INVAMT, TRX_AMT, TRX_EXPNSAMT, TRX_ROI, TRX_STATUS, TRX_NOF_DAYS, TRX_REMX, TRX_UPDBY " +
                                  " FROM INVEST_TRX WHERE TRX_FKM_SRL = '" + CODE + "'  ORDER BY     SUBSTRING(TRX_ISSUEDATE, 7, 4) + SUBSTRING(TRX_ISSUEDATE, 4, 2)+ SUBSTRING(TRX_ISSUEDATE, 1, 2) , TRX_ISSMONT   ";
        DataTable dtGRID = dbo.SelTable(sSQLSting);

        if (dtGRID.Rows.Count > 0)
        {
            Session["NAV_EDIT_dtINVEST_TRX"] = dtGRID;
        }

    }

    protected void Process_INVEST_TRX_Month(string CODE)
    {

        DataTable dtINVEST_TRX = (DataTable)Session["NAV_EDIT_dtINVEST_TRX"];
      //  decimal count_invtrx = (decimal)dtINVEST_TRX.Rows.Count;
        decimal total_invtrx = (decimal)dtINVEST_TRX.Compute("Sum(TRX_AMT)", "");
        decimal total_invtrxR = (decimal)dtINVEST_TRX.Compute("Sum(TRX_AMT)", " TRX_STATUS = 'Y' ");

        decimal total_invtrxUNR = 0;
        DataRow[] dtunrealrow = dtINVEST_TRX.Select(" TRX_STATUS = 'N' ");
        if (dtunrealrow.Length > 0)
        {
            total_invtrxUNR = (decimal)dtINVEST_TRX.Compute("Sum(TRX_AMT)", " TRX_STATUS = 'N' ");
        }

        TXT_TOT_INCM.Text = total_invtrx.ToString();
        //TXT_EXIT_CPGN.Text = total_invtrxR.ToString();
        //TXT_ACTRL_INCM.Text = total_invtrxUNR.ToString();
        decimal valv = decimal.Parse(TXT_VALUATION_NEW.Text.ToString());
        decimal invested = decimal.Parse(TXT_INVAMT.Text.ToString());
        decimal comcap = decimal.Parse(TXT_COMMCAP.Text.ToString());
        decimal consdr_amt = comcap;
        if (CMB_CONSIDER_CI.SelectedItem.Value == "I")
        {

            consdr_amt = invested;
        }

        decimal valv_diff = valv - consdr_amt;

       // TXT_EXIT_CPGN.Text = valv_diff.ToString("#,##0");

      //  TXT_ACTRL_INCM.Text = total_invtrxR.ToString("#,##0");

        decimal count_invtrx = dtINVEST_TRX.Select(" TRX_STATUS = 'Y' ").Length;
        decimal ar_percnt = (total_invtrxR / invested) * (12/count_invtrx)*100;
      //  TXT_ANNRL_PERC.Text = ar_percnt.ToString("0.00"); 
        decimal anu_real = (ar_percnt/100) * invested;
      //  TXT_ANNRL_INCM.Text = anu_real.ToString("#,##0");
        TXT_UNRL_INCM.Text = total_invtrxUNR.ToString("#,##0");

        decimal unrl_count = dtINVEST_TRX.Select(" TRX_STATUS = 'N' ").Length;

        decimal UNR_percnt = 0;
        if (unrl_count > 0)
        {
            UNR_percnt = (total_invtrxUNR / consdr_amt) * (12 / unrl_count) * 100;
        }
        TXT_UNRL_PERC.Text = UNR_percnt.ToString("#0.00");

        decimal baseval = decimal.Parse("1");
        if (TXT_LIBORRATE.Text.Length > 0 && decimal.Parse(TXT_LIBORRATE.Text) > 0)
        baseval = decimal.Parse(TXT_LIBORRATE.Text.ToString());

        decimal total_baseUNR = 0;// (decimal)dtINVEST_TRX.Compute("Sum( TRX_AMT-( TRX_AMT * " + baseval + "* (" + unrl_count + "/12)))", " TRX_STATUS = 'N' ");
        decimal i = 1;
        foreach (DataRow dr in dtunrealrow)
        {
            decimal tot_URN_W_Base = decimal.Parse(dr["TRX_AMT"].ToString());
            tot_URN_W_Base = tot_URN_W_Base - (tot_URN_W_Base * (baseval/100) * (i/12));
            total_baseUNR += tot_URN_W_Base;
            i++;
        }
       // TXT_UNRL_DISC_INCM.Text = total_baseUNR.ToString("#,##0");
        decimal total_final = valv - (valv * (baseval / 100) * (unrl_count / 12));

        TXT_VALVTN.Text = total_final.ToString("#,##0");


        decimal roi = decimal.Parse(TXT_ROI.Text.ToString());
        TXT_SCNINCP.Text = total_invtrxR.ToString("#,##0");
        TXT_ANLINCMCY.Text = ((consdr_amt) * (roi / 100)).ToString("#,##0");
        TXT_ANLRLZD.Text = anu_real.ToString("#,##0");
        TXT_ACTLINCMRU.Text = ar_percnt.ToString("#0.00");
        TXT_UNRLYLD.Text = total_baseUNR.ToString("#,##0");
        TXT_CAPGN.Text = valv_diff.ToString("#,##0");
        TXT_UNRLDVD.Text = total_baseUNR.ToString("#,##0");

        TXT_UNRLDVDCAP.Text = (total_baseUNR + valv_diff).ToString("#,##0");
        TXT_FAIRVAL.Text = (invested + total_invtrxR + total_baseUNR + valv_diff).ToString("#,##0");
        TXT_NAV.Text = ((invested + total_invtrxR + total_baseUNR + valv_diff) / invested).ToString("#0.0000");

    }
    protected void Process_INVEST_TRX_day(string CODE)
    {

        DataTable dtINVEST_TRX = (DataTable)Session["NAV_EDIT_dtINVEST_TRX"];
        //  decimal count_invtrx = (decimal)dtINVEST_TRX.Rows.Count;
        decimal total_invtrx = (decimal)dtINVEST_TRX.Compute("Sum(TRX_AMT)", "");
        decimal total_invtrxR = (decimal)dtINVEST_TRX.Compute("Sum(TRX_AMT)", " TRX_STATUS = 'Y' ");

        decimal total_invtrxUNR = 0;
        DataRow[] dtunrealrow = dtINVEST_TRX.Select(" TRX_STATUS = 'N' ");
        if (dtunrealrow.Length > 0)
        {
            total_invtrxUNR = (decimal)dtINVEST_TRX.Compute("Sum(TRX_AMT)", " TRX_STATUS = 'N' ");
        }

        TXT_TOT_INCM.Text = total_invtrx.ToString();
        //TXT_EXIT_CPGN.Text = total_invtrxR.ToString();
        //TXT_ACTRL_INCM.Text = total_invtrxUNR.ToString();
        decimal valv = decimal.Parse(TXT_VALUATION_NEW.Text.ToString());
        decimal invested = decimal.Parse(TXT_INVAMT.Text.ToString());
        decimal comcap = decimal.Parse(TXT_COMMCAP.Text.ToString());
        decimal consdr_amt = comcap;
        if (CMB_CONSIDER_CI.SelectedItem.Value == "I")
        {

            consdr_amt = invested;
        }

        decimal valv_diff = valv - consdr_amt;

        // TXT_EXIT_CPGN.Text = valv_diff.ToString("#,##0");

        //  TXT_ACTRL_INCM.Text = total_invtrxR.ToString("#,##0");

        DateTime inv_dt = DateTime.Parse(TXT_PRTCPDATE.Text);


        //DataRow[] dtunrealrow_Y = dtINVEST_TRX.Select(" TRX_STATUS = 'Y' ");
        //int count_invtrxRL = dtunrealrow_Y.Length;
         
       //DateTime First_invRL_dt = DateTime.Parse(dtunrealrow_Y[0]["TRX_ISSUEDATE"].ToString());
       //DateTime Curr_invRL_dt = DateTime.Parse(dtunrealrow_Y[count_invtrxRL -1]["TRX_ISSUEDATE"].ToString());
       //TimeSpan t = Curr_invRL_dt - First_invRL_dt;
        decimal days_computed_RL = (decimal)dtINVEST_TRX.Compute("Sum(TRX_NOF_DAYS)", " TRX_STATUS = 'Y' ");

       int daysinyear = 365;
       //if ( (System.DateTime.Now.Year % 4) == 0)
       //{
       //    daysinyear = 366;
       //}

       decimal ar_percnt = (total_invtrxR / invested) * ((decimal)daysinyear / (decimal)days_computed_RL) * 100;
        //  TXT_ANNRL_PERC.Text = ar_percnt.ToString("0.00"); 
        decimal anu_real = (ar_percnt / 100) * invested;
        //  TXT_ANNRL_INCM.Text = anu_real.ToString("#,##0");
        TXT_UNRL_INCM.Text = total_invtrxUNR.ToString("#,##0");

        //DataRow[] dtunrealrow_N = dtINVEST_TRX.Select(" TRX_STATUS = 'Y' ");
        //int count_invtrxUNRL = dtunrealrow_N.Length;

        //DateTime First_invUNRL_dt = DateTime.Parse(dtunrealrow_Y[0]["TRX_ISSUEDATE"].ToString());
        //DateTime Curr_invUNRL_dt = DateTime.Parse(dtunrealrow_Y[count_invtrxUNRL - 1]["TRX_ISSUEDATE"].ToString());
        //t = Curr_invUNRL_dt - Curr_invRL_dt;
        decimal days_computed_UNRL = (decimal)dtINVEST_TRX.Compute("Sum(TRX_NOF_DAYS)", " TRX_STATUS = 'N' ");
        decimal UNR_percnt = 0;
        if (days_computed_UNRL > 0)
        {
            UNR_percnt = (total_invtrxUNR / consdr_amt) * ((decimal)daysinyear / (decimal)days_computed_UNRL) * 100;
        }
        TXT_UNRL_PERC.Text = UNR_percnt.ToString("#0.00");

        decimal baseval = decimal.Parse("1");
        if (TXT_LIBORRATE.Text.Length > 0 && decimal.Parse(TXT_LIBORRATE.Text) > 0)
            baseval = decimal.Parse(TXT_LIBORRATE.Text.ToString());

        decimal total_baseUNR = 0;// (decimal)dtINVEST_TRX.Compute("Sum( TRX_AMT-( TRX_AMT * " + baseval + "* (" + unrl_count + "/12)))", " TRX_STATUS = 'N' ");
        //decimal i = 1;
        decimal days_computed_UNRL_new = 0;
        foreach (DataRow dr in dtunrealrow)
        {


             days_computed_UNRL_new += decimal.Parse(dr["TRX_NOF_DAYS"].ToString());

            decimal tot_URN_W_Base = decimal.Parse(dr["TRX_AMT"].ToString());
            tot_URN_W_Base = tot_URN_W_Base - (tot_URN_W_Base * (baseval / 100) * ((decimal)days_computed_UNRL_new / (decimal)daysinyear));
            total_baseUNR += tot_URN_W_Base;
        }
        // TXT_UNRL_DISC_INCM.Text = total_baseUNR.ToString("#,##0");
        decimal total_final = valv - (valv * (baseval / 100) * ((decimal)days_computed_UNRL / (decimal)daysinyear));

        TXT_VALVTN.Text = total_final.ToString("#,##0");


        decimal roi = decimal.Parse(TXT_ROI.Text.ToString());
        TXT_SCNINCP.Text = total_invtrxR.ToString("#,##0");
        TXT_ANLINCMCY.Text = ((consdr_amt) * (roi / 100)).ToString("#,##0");
        TXT_ANLRLZD.Text = anu_real.ToString("#,##0");
        TXT_ACTLINCMRU.Text = ar_percnt.ToString("#0.00");
        TXT_UNRLYLD.Text = total_baseUNR.ToString("#,##0");
        TXT_CAPGN.Text = valv_diff.ToString("#,##0");
        TXT_UNRLDVD.Text = total_baseUNR.ToString("#,##0");

        TXT_UNRLDVDCAP.Text = (total_baseUNR + valv_diff).ToString("#,##0");
        TXT_FAIRVAL.Text = (invested + total_invtrxR + total_baseUNR + valv_diff).ToString("#,##0");
        TXT_NAV.Text = ((invested + total_invtrxR + total_baseUNR + valv_diff) / invested).ToString("#0.0000");

    }

    public void BTN_PROCESS_Click(object sender, System.EventArgs e)// Handles BTN_CLEAR.Click
    {
        if (CMB_COMP_PRD.SelectedItem.Value == "M")
        {
            Process_INVEST_TRX_Month(CMB_FKM_CD.SelectedValue);
        }
        else
        {
            Process_INVEST_TRX_day(CMB_FKM_CD.SelectedValue);
        }
    }

    public void INIT_QUARTER()
    {
        DataTable DT = new DataTable();

        DT.Columns.Add("QUARTER");
        DT.Columns.Add("CD");

        if (System.DateTime.Today > DateTime.Parse("30/09/" + System.DateTime.Today.Year))
        {
            DT.Rows.Add("DECEMBER - " + (System.DateTime.Today.Year), (System.DateTime.Today.Year) + "12");
        }

        if (System.DateTime.Today > DateTime.Parse("30/06/" + System.DateTime.Today.Year))
        {
            DT.Rows.Add("SEPTEMBER - " + (System.DateTime.Today.Year), (System.DateTime.Today.Year) + "09");
        }

        if (System.DateTime.Today > DateTime.Parse("31/03/" + System.DateTime.Today.Year))
        {
            DT.Rows.Add("JUNE - " + (System.DateTime.Today.Year), (System.DateTime.Today.Year) + "06");
        }

        if (System.DateTime.Today >= DateTime.Parse("01/01/" + System.DateTime.Today.Year))
        {
            DT.Rows.Add("MARCH - " + (System.DateTime.Today.Year), (System.DateTime.Today.Year) + "03");
        }

        int YEAR = 2015;
        for (int I = 1; I <= (System.DateTime.Today.Year - YEAR); I++)
        {
            DT.Rows.Add("DECEMBER - " + (System.DateTime.Today.Year - I), (System.DateTime.Today.Year - I) + "12");
            DT.Rows.Add("SEPTEMBER - " + (System.DateTime.Today.Year - I), (System.DateTime.Today.Year - I) + "09");
            DT.Rows.Add("JUNE - " + (System.DateTime.Today.Year - I), (System.DateTime.Today.Year - I) + "06");
            DT.Rows.Add("MARCH - " + (System.DateTime.Today.Year - I), (System.DateTime.Today.Year - I) + "03");

        }

        CMB_NAV_QTR.DataSource = DT;
        CMB_NAV_QTR.DataBind();

    }
    public void INIT_QUARTER_QRY()
    {
        DataTable DT = new DataTable();

        DT.Columns.Add("QUARTER");
        DT.Columns.Add("CD");

        if (System.DateTime.Today > DateTime.Parse("30/09/" + System.DateTime.Today.Year))
        {
            DT.Rows.Add("DECEMBER - " + (System.DateTime.Today.Year), (System.DateTime.Today.Year) + "12");
        }

        if (System.DateTime.Today > DateTime.Parse("30/06/" + System.DateTime.Today.Year))
        {
            DT.Rows.Add("SEPTEMBER - " + (System.DateTime.Today.Year), (System.DateTime.Today.Year) + "09");
        }

        if (System.DateTime.Today > DateTime.Parse("31/03/" + System.DateTime.Today.Year))
        {
            DT.Rows.Add("JUNE - " + (System.DateTime.Today.Year), (System.DateTime.Today.Year) + "06");
        }

        if (System.DateTime.Today >= DateTime.Parse("01/01/" + System.DateTime.Today.Year))
        {
            DT.Rows.Add("MARCH - " + (System.DateTime.Today.Year), (System.DateTime.Today.Year) + "03");
        }

       //'Dim COMP_CD As String = Session("USER_COMP_CD")
        string RETRVQRY = "SELECT DISTINCT  FKM_ISSDATE  FROM NAV_INFO WHERE FKM_ISSDATE NOT LIKE '" + System.DateTime.Today.Year + "%'    ORDER BY FKM_ISSDATE DESC";
        DataTable dtinfo    = dbo.SelTable(RETRVQRY);

       // ' Dim YEAR As Integer = 2015
        foreach(DataRow DR in dtinfo.Rows)// ' (Today.Year - YEAR)
        { string YR   = DR["FKM_ISSDATE"].ToString().Substring(0, 4);
            string MNT   = DR["FKM_ISSDATE"].ToString().Substring(4, 2);
           switch(MNT){
               case "03": DT.Rows.Add("MARCH - " + YR, YR + "03"); break;
               case "06": DT.Rows.Add("JUNE - " + YR, YR + "06"); break;
               case "09": DT.Rows.Add("SEPTEMBER - " + YR, YR + "09"); break;
               case "12": DT.Rows.Add("DECEMBER - " + YR, YR + "12"); break;
        }

            //'DT.Rows.Add("DECEMBER - " & (Today.Year - I), (Today.Year - I) & "12")
            //'DT.Rows.Add("SEPTEMBER - " & (Today.Year - I), (Today.Year - I) & "09")
            //'DT.Rows.Add("JUNE - " & (Today.Year - I), (Today.Year - I) & "06")
            //'DT.Rows.Add("MARCH - " & (Today.Year - I), (Today.Year - I) & "03")
        }
     
         DT.Rows.Add("ALL QUARTER", "%");
        CMB_NAV_QTR_QRY.DataSource = DT;
        CMB_NAV_QTR_QRY.DataBind();
        CMB_NAV_QTR_QRY.SelectedIndex = CMB_NAV_QTR_QRY.Items.Count - 1;
        }
    public void INIT_FKMCD()
    {
        //' Dim COMP_CD As string = Session("USER_COMP_CD")
        string RETRVQRY = "SELECT DISTINCT FKM_SRL ,FKM_SRL + '-' + FKM_PROJNAME AS FKM_PROJNAME  FROM INVEST_INFO  ORDER BY FKM_SRL";
        DataTable dtinfo = dbo.SelTable(RETRVQRY);
        CMB_FKM_CD.DataSource = dtinfo;
        CMB_FKM_CD.DataBind();
        CMB_FKM_CD.SelectedIndex = -1;
         

    }
    public void INIT_FKMCD_QRY()
    {
        //' Dim COMP_CD As string = Session("USER_COMP_CD")
        string RETRVQRY = "SELECT DISTINCT FKM_SRL ,FKM_SRL + '-' + FKM_PROJNAME AS FKM_PROJNAME  FROM INVEST_INFO   ORDER BY FKM_SRL";
        DataTable dtinfo = dbo.SelTable(RETRVQRY);
        

        dtinfo.Rows.Add("%", "ALL PROJECTS");
        CMB_FKM_CD_QRY.DataSource = dtinfo;
        CMB_FKM_CD_QRY.DataBind();
        CMB_FKM_CD_QRY.SelectedIndex = CMB_FKM_CD_QRY.Items.Count - 1;


    }
    //'Protected Sub CMB_FKM_CD_TextChanged(object sender, System.EventArgs e)// Handles CMB_FKM_CD.TextChanged
    //'    Dim CODE As string = CMB_FKM_CD.Text.Trim()
    //'    Call CMB_FKM_CD_Changed(CODE)
    //'}
    
  public void CMB_FKM_CD_QRY_SelectedIndexChanged(object sender, System.EventArgs e)// Handles CMB_FKM_CD.SelectedIndexChanged
  {   if( CMB_FKM_CD_QRY.SelectedIndex > -1 && CMB_NAV_QTR_QRY.SelectedIndex > -1 ){
       
             REFINE_QRY( );
            }
        }

 public void CMB_NAV_QTR_QRY_SelectedIndexChanged(object sender, System.EventArgs e)//CMB_NAV_QTR.SelectedIndexChanged
 { if( CMB_FKM_CD_QRY.SelectedIndex > -1 && CMB_NAV_QTR_QRY.SelectedIndex > -1 ){
           
             REFINE_QRY( );
            }
}
    
    public void REFINE_QRY( )
    {

        String CODE = CMB_FKM_CD_QRY.SelectedValue; 
        String QUARTERCD = CMB_NAV_QTR_QRY.Text.Trim();
        Session["NAVLIST_dt_NAVLIST_REPORT"] = null;
        GridView1.DataSource = null;
        GridView1.DataBind();

       // 'Dim COMP_CD As String = Session("USER_COMP_CD")
        String RETRVQRY  = "SELECT * FROM NAV_INFO WHERE FKM_SRL LIKE '" + CODE + "' AND  FKM_ISSDATE LIKE '" + QUARTERCD + "'   ORDER BY FKM_ISSDATE DESC  ";
        //Dim DTINVINFO As DataTable = dbo.SelTable(RETRVQRY)
        DataTable DTINVINFO = dbo.SelTable(RETRVQRY);

        if (DTINVINFO.Rows.Count > 0)
        {
            DTINVINFO.Columns.Add("FKM_COMMCAP_R", typeof(string));
            DTINVINFO.Columns.Add("FKM_COMMCAP2_R", typeof(string));
            DTINVINFO.Columns.Add("FKM_INVAMT_R", typeof(string));
            DTINVINFO.Columns.Add("FKM_CAPPD_R", typeof(string));
            DTINVINFO.Columns.Add("FKM_CAPUNPD_R", typeof(string));
            DTINVINFO.Columns.Add("FKM_CAPRFND_R", typeof(string));
            DTINVINFO.Columns.Add("FKM_EXPNS_R", typeof(string));
            DTINVINFO.Columns.Add("FKM_ROI_R", typeof(string));
            DTINVINFO.Columns.Add("FKM_BOOKVAL_R", typeof(string));
            DTINVINFO.Columns.Add("FKM_MONYCAL_R", typeof(string));
            DTINVINFO.Columns.Add("FKM_QRTYCAL_R", typeof(string));
            DTINVINFO.Columns.Add("FKM_SMANYCAL_R", typeof(string));
            DTINVINFO.Columns.Add("FKM_ANLYCAL_R", typeof(string));
            DTINVINFO.Columns.Add("FKM_SCNINCP_R", typeof(string));
            DTINVINFO.Columns.Add("FKM_ANLINCMCY_R", typeof(string));
            DTINVINFO.Columns.Add("FKM_ANLRLZD_R", typeof(string));
            DTINVINFO.Columns.Add("FKM_ACTLINCMRU_R", typeof(string));
            DTINVINFO.Columns.Add("FKM_UNRLYLD_R", typeof(string));
            DTINVINFO.Columns.Add("FKM_DVDND_R", typeof(string));
            DTINVINFO.Columns.Add("FKM_VALVTN_R", typeof(string));
            DTINVINFO.Columns.Add("FKM_CAPGN_R", typeof(string));
            DTINVINFO.Columns.Add("FKM_UNRLDVD_R", typeof(string));
            DTINVINFO.Columns.Add("FKM_UNRLDVDCAP_R", typeof(string));
            DTINVINFO.Columns.Add("FKM_FAIRVAL_R", typeof(string));
            DTINVINFO.Columns.Add("FKM_NAV_R", typeof(string));
            DTINVINFO.Columns.Add("FKM_SALEPRCD_R", typeof(string));
            DTINVINFO.Columns.Add("FOOTER", typeof(string));
            DTINVINFO.Columns.Add("FKM_REG", typeof(string));
            DTINVINFO.Columns.Add("LOGO", typeof(string));

            DataTable dt_invest_info = DTINVINFO.Clone();
            dt_invest_info.Rows.Clear();
            REFINE_ROW_INV_DETAILS(ref DTINVINFO, ref dt_invest_info, "US DOLLAR", "$");
            REFINE_ROW_INV_DETAILS(ref DTINVINFO, ref  dt_invest_info, "UK POUND", "£");
            REFINE_ROW_INV_DETAILS(ref DTINVINFO, ref dt_invest_info, "EURO", "€");

            Session["NAVLIST_dt_NAVLIST_REPORT"] = null;
            if (dt_invest_info.Rows.Count > 0)
            { 
                Session["NAVLIST_dt_NAVLIST_REPORT"] = dt_invest_info;
                GridView1.DataSource = dt_invest_info;
                GridView1.DataBind();
            }
        }
    }
    public void REFINE_ROW_INV_DETAILS(ref DataTable DTINVINFO, ref DataTable dt_invest_info, string CURR, string CURRSYMB)
    { 


        DataRow[] DR_USD = DTINVINFO.Select("FKM_CURR  = '" + CURR + "' ");
        DataRow DR_TOT_USD = dt_invest_info.NewRow();
        if (DR_USD.Length > 0)
        {


            //DR_TOT_USD["FKM_CURR"] = CURR;
            //DR_TOT_USD["FKM_PROJNAME"] = "TOTAL PROJECTS: " + DR_USD.Length;
            //DR_TOT_USD["FKM_LOCATION"] = "TOTAL (" + CURR + ")";

            //'DR_TOT_USD["FKM_HOLDNAME") = "."
            //'DR_TOT_USD["FKM_OPPRBY") = "."
            //'DR_TOT_USD["FKM_INVCOMP") = "."
            //''DR_TOT_USD["FKM_PRVTEQT") = "."
            //'DR_TOT_USD["FKM_BANK") = "."
            //''DR_TOT_USD["FKM_YEILDPRD") = "."
            //''DR_TOT_USD["FKM_INCGEN") = "."
            //'DR_TOT_USD["FKM_REMX") = "."

            DR_TOT_USD["FKM_COMMCAP"] = decimal.Parse("0");
            DR_TOT_USD["FKM_COMMCAP2"] = decimal.Parse("0");
            DR_TOT_USD["FKM_INVAMT"] = decimal.Parse("0");
            DR_TOT_USD["FKM_CAPPD"] = decimal.Parse("0");
            DR_TOT_USD["FKM_CAPUNPD"] = decimal.Parse("0");
            DR_TOT_USD["FKM_CAPRFND"] = decimal.Parse("0");
            DR_TOT_USD["FKM_EXPNS"] = decimal.Parse("0");
            DR_TOT_USD["FKM_ROI"] = decimal.Parse("0");
            DR_TOT_USD["FKM_BOOKVAL"] = decimal.Parse("0");
            DR_TOT_USD["FKM_MONYCAL"] = decimal.Parse("0");
            DR_TOT_USD["FKM_QRTYCAL"] = decimal.Parse("0");
            DR_TOT_USD["FKM_SMANYCAL"] = decimal.Parse("0");
            DR_TOT_USD["FKM_ANLYCAL"] = decimal.Parse("0");
            DR_TOT_USD["FKM_SCNINCP"] = decimal.Parse("0");
            DR_TOT_USD["FKM_ANLINCMCY"] = decimal.Parse("0");
            DR_TOT_USD["FKM_ANLRLZD"] = decimal.Parse("0");
            DR_TOT_USD["FKM_ACTLINCMRU"] = decimal.Parse("0");
            DR_TOT_USD["FKM_UNRLYLD"] = decimal.Parse("0");
            DR_TOT_USD["FKM_DVDND"] = decimal.Parse("0");
            DR_TOT_USD["FKM_VALVTN"] = decimal.Parse("0");
            DR_TOT_USD["FKM_CAPGN"] = decimal.Parse("0");
            DR_TOT_USD["FKM_UNRLDVD"] = decimal.Parse("0");
            DR_TOT_USD["FKM_UNRLDVDCAP"] = decimal.Parse("0");
            DR_TOT_USD["FKM_FAIRVAL"] = decimal.Parse("0");
            DR_TOT_USD["FKM_NAV"] = decimal.Parse("0");
            DR_TOT_USD["FKM_SALEPRCD"] = decimal.Parse("0");

            for (int I = 0; I <= DR_USD.Length - 1; I++)
            {
                DR_TOT_USD["COMP_CD"] = DR_USD[I]["COMP_CD"];
                DR_TOT_USD["FKM_COMMCAP"] = decimal.Parse(DR_TOT_USD["FKM_COMMCAP"].ToString()) + decimal.Parse(DR_USD[I]["FKM_COMMCAP"].ToString());
                DR_TOT_USD["FKM_COMMCAP2"] = decimal.Parse(DR_TOT_USD["FKM_COMMCAP2"].ToString()) + decimal.Parse(DR_USD[I]["FKM_COMMCAP2"].ToString());
                DR_TOT_USD["FKM_INVAMT"] = decimal.Parse(DR_TOT_USD["FKM_INVAMT"].ToString()) + decimal.Parse(DR_USD[I]["FKM_INVAMT"].ToString());
                DR_TOT_USD["FKM_CAPPD"] = decimal.Parse(DR_TOT_USD["FKM_CAPPD"].ToString()) + decimal.Parse(DR_USD[I]["FKM_CAPPD"].ToString());
                DR_TOT_USD["FKM_CAPUNPD"] = decimal.Parse(DR_TOT_USD["FKM_CAPUNPD"].ToString()) + decimal.Parse(DR_USD[I]["FKM_CAPUNPD"].ToString());
                DR_TOT_USD["FKM_CAPRFND"] = decimal.Parse(DR_TOT_USD["FKM_CAPRFND"].ToString()) + decimal.Parse(DR_USD[I]["FKM_CAPRFND"].ToString());
                DR_TOT_USD["FKM_EXPNS"] = decimal.Parse(DR_TOT_USD["FKM_EXPNS"].ToString()) + decimal.Parse(DR_USD[I]["FKM_EXPNS"].ToString());
                DR_TOT_USD["FKM_BOOKVAL"] = decimal.Parse(DR_TOT_USD["FKM_BOOKVAL"].ToString()) + decimal.Parse(DR_USD[I]["FKM_BOOKVAL"].ToString());
                DR_TOT_USD["FKM_MONYCAL"] = decimal.Parse(DR_TOT_USD["FKM_MONYCAL"].ToString()) + decimal.Parse(DR_USD[I]["FKM_MONYCAL"].ToString());
                DR_TOT_USD["FKM_QRTYCAL"] = decimal.Parse(DR_TOT_USD["FKM_QRTYCAL"].ToString()) + decimal.Parse(DR_USD[I]["FKM_QRTYCAL"].ToString());
                DR_TOT_USD["FKM_SMANYCAL"] = decimal.Parse(DR_TOT_USD["FKM_SMANYCAL"].ToString()) + decimal.Parse(DR_USD[I]["FKM_SMANYCAL"].ToString());
                DR_TOT_USD["FKM_ANLYCAL"] = decimal.Parse(DR_TOT_USD["FKM_ANLYCAL"].ToString()) + decimal.Parse(DR_USD[I]["FKM_ANLYCAL"].ToString());
                DR_TOT_USD["FKM_SCNINCP"] = decimal.Parse(DR_TOT_USD["FKM_SCNINCP"].ToString()) + decimal.Parse(DR_USD[I]["FKM_SCNINCP"].ToString());
                DR_TOT_USD["FKM_ANLINCMCY"] = decimal.Parse(DR_TOT_USD["FKM_ANLINCMCY"].ToString()) + decimal.Parse(DR_USD[I]["FKM_ANLINCMCY"].ToString());
                DR_TOT_USD["FKM_ANLRLZD"] = decimal.Parse(DR_TOT_USD["FKM_ANLRLZD"].ToString()) + decimal.Parse(DR_USD[I]["FKM_ANLRLZD"].ToString());
                DR_TOT_USD["FKM_ACTLINCMRU"] = decimal.Parse(DR_TOT_USD["FKM_ACTLINCMRU"].ToString()) + decimal.Parse(DR_USD[I]["FKM_ACTLINCMRU"].ToString());
                DR_TOT_USD["FKM_UNRLYLD"] = decimal.Parse(DR_TOT_USD["FKM_UNRLYLD"].ToString()) + decimal.Parse(DR_USD[I]["FKM_UNRLYLD"].ToString());
                DR_TOT_USD["FKM_DVDND"] = decimal.Parse(DR_TOT_USD["FKM_DVDND"].ToString()) + decimal.Parse(DR_USD[I]["FKM_DVDND"].ToString());
                DR_TOT_USD["FKM_VALVTN"] = decimal.Parse(DR_TOT_USD["FKM_VALVTN"].ToString()) + decimal.Parse(DR_USD[I]["FKM_VALVTN"].ToString());
                DR_TOT_USD["FKM_CAPGN"] = decimal.Parse(DR_TOT_USD["FKM_CAPGN"].ToString()) + decimal.Parse(DR_USD[I]["FKM_CAPGN"].ToString());
                DR_TOT_USD["FKM_UNRLDVD"] = decimal.Parse(DR_TOT_USD["FKM_UNRLDVD"].ToString()) + decimal.Parse(DR_USD[I]["FKM_UNRLDVD"].ToString());
                DR_TOT_USD["FKM_UNRLDVDCAP"] = decimal.Parse(DR_TOT_USD["FKM_UNRLDVDCAP"].ToString()) + decimal.Parse(DR_USD[I]["FKM_UNRLDVDCAP"].ToString());
                DR_TOT_USD["FKM_FAIRVAL"] = decimal.Parse(DR_TOT_USD["FKM_FAIRVAL"].ToString()) + decimal.Parse(DR_USD[I]["FKM_FAIRVAL"].ToString());
                DR_TOT_USD["FKM_NAV"] = decimal.Parse(DR_TOT_USD["FKM_NAV"].ToString()) + decimal.Parse(DR_USD[I]["FKM_NAV"].ToString());
                DR_TOT_USD["FKM_SALEPRCD"] = decimal.Parse(DR_TOT_USD["FKM_SALEPRCD"].ToString()) + decimal.Parse(DR_USD[I]["FKM_SALEPRCD"].ToString());

                if (decimal.Parse(DR_USD[I]["FKM_COMMCAP"].ToString()) == 0)
                {
                    DR_USD[I]["FKM_COMMCAP"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_COMMCAP_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_COMMCAP"]);
                }

                if (decimal.Parse(DR_USD[I]["FKM_COMMCAP2"].ToString()) == 0)
                {
                    DR_USD[I]["FKM_COMMCAP2"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_COMMCAP2_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_COMMCAP2"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_INVAMT"].ToString()) == 0)
                {
                    DR_USD[I]["FKM_INVAMT"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_INVAMT_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_INVAMT"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_CAPPD"].ToString()) == 0)
                {
                    DR_USD[I]["FKM_CAPPD"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_CAPPD_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_CAPPD"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_CAPUNPD"].ToString()) == 0)
                {
                    DR_USD[I]["FKM_CAPUNPD"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_CAPUNPD_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_CAPUNPD"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_EXPNS"].ToString()) == 0)
                {
                    DR_USD[I]["FKM_EXPNS"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_EXPNS_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_EXPNS"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_ROI"].ToString()) == 0)
                {
                    DR_USD[I]["FKM_ROI"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_ROI_R"] = string.Format("{0:#0.00}", DR_USD[I]["FKM_ROI"].ToString()) + "%";
                }
                if (decimal.Parse(DR_USD[I]["FKM_BOOKVAL"].ToString()) == 0)
                {
                    DR_USD[I]["FKM_BOOKVAL"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_BOOKVAL_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_BOOKVAL"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_MONYCAL"].ToString()) == 0)
                {
                    DR_USD[I]["FKM_MONYCAL"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_MONYCAL_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_MONYCAL"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_QRTYCAL"].ToString()) == 0)
                {
                    DR_USD[I]["FKM_QRTYCAL"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_QRTYCAL_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_QRTYCAL"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_SMANYCAL"].ToString()) == 0)
                {
                    DR_USD[I]["FKM_SMANYCAL"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_SMANYCAL_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_SMANYCAL"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_ANLYCAL"].ToString()) == 0)
                {
                    DR_USD[I]["FKM_ANLYCAL"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_ANLYCAL_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_ANLYCAL"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_SCNINCP"].ToString()) == 0)
                {
                    DR_USD[I]["FKM_SCNINCP"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_SCNINCP_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_SCNINCP"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_ANLINCMCY"].ToString()) == 0)
                {
                    DR_USD[I]["FKM_ANLINCMCY"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_ANLINCMCY_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_ANLINCMCY"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_ANLRLZD"].ToString()) == 0)
                {
                    DR_USD[I]["FKM_ANLRLZD"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_ANLRLZD_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_ANLRLZD"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_ACTLINCMRU"].ToString()) == 0)
                {
                    DR_USD[I]["FKM_ACTLINCMRU"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_ACTLINCMRU_R"] = string.Format("{0:#0.00}", DR_USD[I]["FKM_ACTLINCMRU"].ToString()) + "%";
                }
                if (decimal.Parse(DR_USD[I]["FKM_UNRLYLD"].ToString()) == 0)
                {
                    DR_USD[I]["FKM_UNRLYLD"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_UNRLYLD_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_UNRLYLD"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_DVDND"].ToString()) == 0)
                {
                    DR_USD[I]["FKM_DVDND"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_DVDND_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_DVDND"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_VALVTN"].ToString()) == 0)
                {
                    DR_USD[I]["FKM_VALVTN"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_VALVTN_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_VALVTN"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_CAPGN"].ToString()) == 0)
                {
                    DR_USD[I]["FKM_CAPGN"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_CAPGN_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_CAPGN"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_UNRLDVD"].ToString()) == 0)
                {
                    DR_USD[I]["FKM_UNRLDVD"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_UNRLDVD_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_UNRLDVD"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_UNRLDVDCAP"].ToString()) == 0)
                {
                    DR_USD[I]["FKM_UNRLDVDCAP"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_UNRLDVDCAP_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_UNRLDVDCAP"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_FAIRVAL"].ToString()) == 0)
                {
                    DR_USD[I]["FKM_FAIRVAL"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_FAIRVAL_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_FAIRVAL"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_NAV"].ToString()) == 0)
                {
                    DR_USD[I]["FKM_NAV"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_NAV_R"] = string.Format("{0:#0.00}", DR_USD[I]["FKM_NAV"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_SALEPRCD"].ToString()) == 0)
                {
                    DR_USD[I]["FKM_SALEPRCD"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_SALEPRCD_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_SALEPRCD"]);
                }


                if (DR_USD[I]["FKM_PRVTEQT"].ToString().Trim() == "Y")
                {
                    DR_USD[I]["FKM_PRVTEQT"] = "PRIVATE EQUITY";
                }
                else if (DR_USD[I]["FKM_PRVTEQT"].ToString().Trim() == "N")
                {
                    DR_USD[I]["FKM_PRVTEQT"] = "NON PRIVATE EQUITY";
                }

                if (DR_USD[I]["FKM_YEILDPRD"].ToString().Trim() == "M")
                {
                    DR_USD[I]["FKM_YEILDPRD"] = "MONTHLY";
                }
                else if (DR_USD[I]["FKM_YEILDPRD"].ToString().Trim() == "Q")
                {
                    DR_USD[I]["FKM_YEILDPRD"] = "QUARTERLY";
                }
                else if (DR_USD[I]["FKM_YEILDPRD"].ToString().Trim() == "S")
                {
                    DR_USD[I]["FKM_YEILDPRD"] = "SEMI ANNUAL";
                }

                //'if DR_USD[I]["FKM_HOLDNAME").ToString.Length = 0 ){
                //'    DR_USD[I]["FKM_HOLDNAME") = "~"
                //'End if
                //'if DR_USD[I]["FKM_OPPRBY").ToString.Length = 0 ){
                //'    DR_USD[I]["FKM_OPPRBY") = "~"
                //'End if
                //'if DR_USD[I]["FKM_INVCOMP").ToString.Length = 0 ){
                //'    DR_USD[I]["FKM_INVCOMP") = "~"
                //'End if
                //'if DR_USD[I]["FKM_BANK").ToString.Length = 0 ){
                //'    DR_USD[I]["FKM_BANK") = "~"
                //'End if
                //'if DR_USD[I]["FKM_REMX").ToString.Length = 0 ){
                //'    DR_USD[I]["FKM_REMX") = "~"
                //'End if


                //if (DR_USD[I]["COMP_CD"].ToString() !=  System.DBNull.Value)
                //{
                //    if (DR_USD[I]["COMP_CD"].ToString() == "001")
                //    {
                //        DR_USD[I]["FKM_REG"] = "FKMINVEST";
                //    }
                //    else if (DR_USD[I]["COMP_CD"].ToString() == "002")
                //    {
                //        DR_USD[I]["FKM_REG"] = "FENKINVEST";
                //    }
                //    else if (DR_USD[I]["COMP_CD"].ToString() == "003")
                //    {
                //        DR_USD[I]["FKM_REG"] = "PERSONAL";
                //    }
                //}

                DR_USD[I]["FKM_REG"] = (string)dbo.GET_FRM_COMP_CD(DR_USD[I]["COMP_CD"].ToString(), 1);
                DR_USD[I]["LOGO"] = dbo.ReadFile( Server.MapPath((string)dbo.GET_FRM_COMP_CD(DR_USD[I]["COMP_CD"].ToString(), 2)));

                DR_USD[I]["FOOTER"] = User.Identity.Name.ToUpper();

                dt_invest_info.ImportRow(DR_USD[I]);
            }


            //if (decimal.Parse(DR_TOT_USD["FKM_COMMCAP"].ToString()) == 0)
            //{
            //    DR_TOT_USD["FKM_COMMCAP"] = System.DBNull.Value;
            //}
            //else
            //{
            //    DR_TOT_USD["FKM_COMMCAP_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_COMMCAP"]);
            //}
            //if (decimal.Parse(DR_TOT_USD["FKM_COMMCAP2"].ToString()) == 0)
            //{
            //    DR_TOT_USD["FKM_COMMCAP2"] = System.DBNull.Value;
            //}
            //else
            //{
            //    DR_TOT_USD["FKM_COMMCAP2_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_COMMCAP2"]);
            //}
            //if (decimal.Parse(DR_TOT_USD["FKM_INVAMT"].ToString()) == 0)
            //{
            //    DR_TOT_USD["FKM_INVAMT"] = System.DBNull.Value;
            //}
            //else
            //{
            //    DR_TOT_USD["FKM_INVAMT_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_INVAMT"]);
            //}
            //if (decimal.Parse(DR_TOT_USD["FKM_CAPPD"].ToString()) == 0)
            //{
            //    DR_TOT_USD["FKM_CAPPD"] = System.DBNull.Value;
            //}
            //else
            //{
            //    DR_TOT_USD["FKM_CAPPD_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_CAPPD"]);
            //}
            //if (decimal.Parse(DR_TOT_USD["FKM_CAPUNPD"].ToString()) == 0)
            //{
            //    DR_TOT_USD["FKM_CAPUNPD"] = System.DBNull.Value;
            //}
            //else
            //{
            //    DR_TOT_USD["FKM_CAPUNPD_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_CAPUNPD"]);
            //}
            //if (decimal.Parse(DR_TOT_USD["FKM_EXPNS"].ToString()) == 0)
            //{
            //    DR_TOT_USD["FKM_EXPNS"] = System.DBNull.Value;
            //}
            //else
            //{
            //    DR_TOT_USD["FKM_EXPNS_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_EXPNS"]);
            //}
            //if (decimal.Parse(DR_TOT_USD["FKM_ROI"].ToString()) == 0)
            //{
            //    DR_TOT_USD["FKM_ROI"] = System.DBNull.Value;
            //}
            //else
            //{
            //    DR_TOT_USD["FKM_ROI_R"] = string.Format("{0:#0.00}", DR_TOT_USD["FKM_ROI"].ToString()) + "%";
            //}
            //if (decimal.Parse(DR_TOT_USD["FKM_BOOKVAL"].ToString()) == 0)
            //{
            //    DR_TOT_USD["FKM_BOOKVAL"] = System.DBNull.Value;
            //}
            //else
            //{
            //    DR_TOT_USD["FKM_BOOKVAL_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_BOOKVAL"]);
            //}
            //if (decimal.Parse(DR_TOT_USD["FKM_MONYCAL"].ToString()) == 0)
            //{
            //    DR_TOT_USD["FKM_MONYCAL"] = System.DBNull.Value;
            //}
            //else
            //{
            //    DR_TOT_USD["FKM_MONYCAL_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_MONYCAL"]);
            //}
            //if (decimal.Parse(DR_TOT_USD["FKM_QRTYCAL"].ToString()) == 0)
            //{
            //    DR_TOT_USD["FKM_QRTYCAL"] = System.DBNull.Value;
            //}
            //else
            //{
            //    DR_TOT_USD["FKM_QRTYCAL_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_QRTYCAL"]);
            //}
            //if (decimal.Parse(DR_TOT_USD["FKM_SMANYCAL"].ToString()) == 0)
            //{
            //    DR_TOT_USD["FKM_SMANYCAL"] = System.DBNull.Value;
            //}
            //else
            //{
            //    DR_TOT_USD["FKM_SMANYCAL_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_SMANYCAL"]);
            //}
            //if (decimal.Parse(DR_TOT_USD["FKM_ANLYCAL"].ToString()) == 0)
            //{
            //    DR_TOT_USD["FKM_ANLYCAL"] = System.DBNull.Value;
            //}
            //else
            //{
            //    DR_TOT_USD["FKM_ANLYCAL_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_ANLYCAL"]);
            //}
            //if (decimal.Parse(DR_TOT_USD["FKM_SCNINCP"].ToString()) == 0)
            //{
            //    DR_TOT_USD["FKM_SCNINCP"] = System.DBNull.Value;
            //}
            //else
            //{
            //    DR_TOT_USD["FKM_SCNINCP_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_SCNINCP"]);
            //}
            //if (decimal.Parse(DR_TOT_USD["FKM_ANLINCMCY"].ToString()) == 0)
            //{
            //    DR_TOT_USD["FKM_ANLINCMCY"] = System.DBNull.Value;
            //}
            //else
            //{
            //    DR_TOT_USD["FKM_ANLINCMCY_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_ANLINCMCY"]);
            //}
            //if (decimal.Parse(DR_TOT_USD["FKM_ANLRLZD"].ToString()) == 0)
            //{
            //    DR_TOT_USD["FKM_ANLRLZD"] = System.DBNull.Value;
            //}
            //else
            //{
            //    DR_TOT_USD["FKM_ANLRLZD_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_ANLRLZD"]);
            //}
            //if (decimal.Parse(DR_TOT_USD["FKM_ACTLINCMRU"].ToString()) == 0)
            //{
            //    DR_TOT_USD["FKM_ACTLINCMRU"] = System.DBNull.Value;
            //}
            //else
            //{
            //    DR_TOT_USD["FKM_ACTLINCMRU_R"] = string.Format("{0:#0.00}", DR_TOT_USD["FKM_ACTLINCMRU"].ToString()) + "%";
            //}
            //if (decimal.Parse(DR_TOT_USD["FKM_UNRLYLD"].ToString()) == 0)
            //{
            //    DR_TOT_USD["FKM_UNRLYLD"] = System.DBNull.Value;
            //}
            //else
            //{
            //    DR_TOT_USD["FKM_UNRLYLD_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_UNRLYLD"]);
            //}
            //if (decimal.Parse(DR_TOT_USD["FKM_DVDND"].ToString()) == 0)
            //{
            //    DR_TOT_USD["FKM_DVDND"] = System.DBNull.Value;
            //}
            //else
            //{
            //    DR_TOT_USD["FKM_DVDND_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_DVDND"]);
            //}
            //if (decimal.Parse(DR_TOT_USD["FKM_VALVTN"].ToString()) == 0)
            //{
            //    DR_TOT_USD["FKM_VALVTN"] = System.DBNull.Value;
            //}
            //else
            //{
            //    DR_TOT_USD["FKM_VALVTN_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_VALVTN"]);
            //}
            //if (decimal.Parse(DR_TOT_USD["FKM_CAPGN"].ToString()) == 0)
            //{
            //    DR_TOT_USD["FKM_CAPGN"] = System.DBNull.Value;
            //}
            //else
            //{
            //    DR_TOT_USD["FKM_CAPGN_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_CAPGN"]);
            //}
            //if (decimal.Parse(DR_TOT_USD["FKM_UNRLDVD"].ToString()) == 0)
            //{
            //    DR_TOT_USD["FKM_UNRLDVD"] = System.DBNull.Value;
            //}
            //else
            //{
            //    DR_TOT_USD["FKM_UNRLDVD_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_UNRLDVD"]);
            //}
            //if (decimal.Parse(DR_TOT_USD["FKM_UNRLDVDCAP"].ToString()) == 0)
            //{
            //    DR_TOT_USD["FKM_UNRLDVDCAP"] = System.DBNull.Value;
            //}
            //else
            //{
            //    DR_TOT_USD["FKM_UNRLDVDCAP_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_UNRLDVDCAP"]);
            //}
            //if (decimal.Parse(DR_TOT_USD["FKM_FAIRVAL"].ToString()) == 0)
            //{
            //    DR_TOT_USD["FKM_FAIRVAL"] = System.DBNull.Value;
            //}
            //else
            //{
            //    DR_TOT_USD["FKM_FAIRVAL_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_FAIRVAL"]);
            //}
            //if (decimal.Parse(DR_TOT_USD["FKM_NAV"].ToString()) == 0)
            //{
            //    DR_TOT_USD["FKM_NAV"] = System.DBNull.Value;
            //}
            //else
            //{
            //    DR_TOT_USD["FKM_NAV_R"] = string.Format("{0:#0.00}", DR_TOT_USD["FKM_NAV"]);
            //}
            //if (decimal.Parse(DR_TOT_USD["FKM_SALEPRCD"].ToString()) == 0)
            //{
            //    DR_TOT_USD["FKM_SALEPRCD"] = System.DBNull.Value;
            //}
            //else
            //{
            //    DR_TOT_USD["FKM_SALEPRCD_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_SALEPRCD"]);
            //}

            //// 'DR_TOT_USD["FKM_REG") = (string)dbo.GET_FRM_COMP_CD(Session("USER_COMP_CD"), 1)
            ////  '  DR_TOT_USD["LOGO") = dbo.ReadFile(Server.MapPath((string)dbo.GET_FRM_COMP_CD(Session("USER_COMP_CD"), 2)))

            //DR_TOT_USD["FOOTER"] = User.Identity.Name.ToUpper();

            //dt_invest_info.Rows.Add(DR_TOT_USD);
        }

    }

    public void GridView1_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)// Handles GridView2.RowDataBound
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //'if IsDBNull(e.Row.DataItem("FKM_SRL"].ToString()) And IsDBNull(e.Row.DataItem("FKM_INCGEN"].ToString()) And IsDBNull(e.Row.DataItem("FKM_INVGRP"].ToString()) ){
            //'    e.Row.BackColor = System.Drawing.Color.Navy
            //'    e.Row.ForeColor = System.Drawing.Color.White
            //'  } else {if IsDBNull(e.Row.DataItem("FKM_SRL"].ToString()) And IsDBNull(e.Row.DataItem("FKM_INVGRP"].ToString()) ){
            //'    e.Row.BackColor = System.Drawing.Color.Indigo
            //'    e.Row.ForeColor = System.Drawing.Color.White
            //'  } else {

            string drval = (string)DataBinder.Eval(e.Row.DataItem, "FKM_SRL").ToString();
            if (drval.ToString().Length == 0)
            {
                e.Row.BackColor = System.Drawing.Color.DarkGray;
                e.Row.ForeColor = System.Drawing.Color.Black;
                e.Row.Font.Bold = true;

            }
            else
            {
                string drval2 = (string)DataBinder.Eval(e.Row.DataItem, "FKM_REG").ToString();
                e.Row.ForeColor = (System.Drawing.Color)dbo.GET_FRM_COMP_CD(drval2, 3); 
            } 
        }
    }

    public void GridView1_SelectedIndexChanged(object sender, System.EventArgs e)// Handles GridView1.SelectedIndexChanged
    {

        if (GridView1.SelectedDataKey[0].ToString().Trim().Length != 0)
        {
            string REFCODE = (string)GridView1.SelectedDataKey[0];
            string QTR = (string)GridView1.SelectedDataKey[1];
            CMB_NAV_QTR.SelectedIndex = CMB_NAV_QTR.Items.IndexOf(CMB_NAV_QTR.Items.FindByValue(QTR.Trim()));
            CMB_FKM_CD.SelectedIndex = CMB_FKM_CD.Items.IndexOf(CMB_FKM_CD.Items.FindByValue(REFCODE));

            string CODE = CMB_FKM_CD.SelectedValue;
            string QUARTER = CMB_NAV_QTR.SelectedItem.Text;
            string QUARTERCD = CMB_NAV_QTR.Text.Trim();
            CMB_FKM_CD_Changed(CODE, QUARTER);
        }
    }

    public void mbox(string val)
    {
        //string cs   = "c";
        //string cst  = "<script type=""text/javascript"">" + "alert('" + val + "');</" + "script>";
        //Page page    = HttpContext.Current.Handler;
        //page.ClientScript.RegisterStartupScript(Me.typeof, cs, cst);

        string USR = User.Identity.Name.ToUpper();
        dbo.ErrorLog(USR + " : NAV_DTL_EDIT.aspx : " + val, Server.MapPath("~\\Logs\\ErrorLog"));
    }
}
