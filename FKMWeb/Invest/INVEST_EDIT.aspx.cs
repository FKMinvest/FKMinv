using System;
using System.Data;
using System.IO;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class INVEST_EDIT : Page
{
    fkminvcom dbo = new fkminvcom();
    // Visual C# .NET requires that you override the OnInit function,
    // adding a new delegate for the Page_Load event.

    override protected void OnInit(System.EventArgs e)
    {
        this.Load += new System.EventHandler(this.Page_Load);
        base.OnInit(e);
        CMB_FKM_CD.SelectedIndexChanged += new System.EventHandler(CMB_FKM_CD_SelectedIndexChanged);
        GridView1.SelectedIndexChanged += new System.EventHandler(GridView1_SelectedIndexChanged);
        GridView1.RowDataBound += new System.Web.UI.WebControls.GridViewRowEventHandler(GridView1_RowDataBound); 
        CHBX_EDIT.CheckedChanged += new System.EventHandler(CHBX_EDIT_CheckedChanged); 
        BTN_ADD.Click += new System.EventHandler(BTN_ADD_Click);
        BTN_UPDATE.Click += new System.EventHandler(BTN_UPDATE_Click);
        BTN_CLEAR.Click += new System.EventHandler(BTN_CLEAR_Click);
    }

    public void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            CLEAR();
            CMB_FKM_CD.SelectedIndex = -1;
            INIT_FKMCD();

            // ' EDITING
            if (Request.QueryString.Get("REFSRL") != null)
            {
                CMB_PROJNAME.DataBind();
                CMB_HOLDNAME.DataBind();
                CMB_OPPRBY.DataBind();
                CMB_INVCOMP.DataBind();
                CMB_INVGRP.DataBind();
                CMB_LOCATION.DataBind();
                CMB_CURR.DataBind();
                CMB_BANK.DataBind();

                string REFCODE = Request.QueryString.Get("REFSRL");
                CMB_FKM_CD.SelectedIndex = CMB_FKM_CD.Items.IndexOf(CMB_FKM_CD.Items.FindByValue(REFCODE));
                string CODE = CMB_FKM_CD.SelectedValue;
                CMB_FKM_CD_Changed(CODE);

            }
        }
    }

    protected void CHBX_EDIT_CheckedChanged(object sender, System.EventArgs e)// Handles CHBX_EDIT.CheckedChanged
    {
        CLEAR();
    }
    public void CLEAR()
    {
        if (CHBX_EDIT.Checked == false)
        {
            TXT_FKM_CD.Visible = true;
            BTN_ADD.Visible = true; 
            CMB_FKM_CD.Visible = false;
            BTN_UPDATE.Visible = false;
            TXT_FKM_CD.Text = NEXT_REC_SRL();
        }
        else
        {
            TXT_FKM_CD.Visible = false;
            BTN_ADD.Visible = false;
            CMB_FKM_CD.Visible = true;
            BTN_UPDATE.Visible = true;
            TXT_FKM_CD.Text = CMB_FKM_CD.SelectedValue.Trim();
        }

        //'CMB_FKM_CD.SelectedIndex = -1
        CMB_PROJNAME.SelectedIndex = -1;
        CMB_HOLDNAME.SelectedIndex = -1;
        CMB_OPPRBY.SelectedIndex = -1;
        CMB_INVCOMP.SelectedIndex = -1;
        CMB_INVGRP.SelectedIndex = -1;
        TXT_PRTCPDATE.Text = "";
        TXT_MTRTDATE.Text = "";
        CMB_LOCATION.SelectedIndex = -1;
        CMB_CURR.SelectedIndex = -1;
        CMB_BANK.SelectedIndex = -1;
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
        //'TXT_ANLINCMCY.Text = ""
        //'TXT_ANLRLZD.Text = ""
        //'TXT_ACTLINCMRU.Text = ""

        //'TXT_UNRLYLD.Text = ""
        //'TXT_DVDND.Text = ""
        //'TXT_VALVTN.Text = ""
        //'TXT_CAPGN.Text = ""
        //'TXT_UNRLDVD.Text = ""
        //'TXT_UNRLDVDCAP.Text = ""
        //'TXT_FAIRVAL.Text = ""
        //'TXT_NAV.Text = ""
        TXT_SALEPRCD.Text = "";
        TXT_REMX.Text = "";

        GET_GRID_INFO();
    }
    public void BTN_ADD_Click(object sender, System.EventArgs e)// Handles BTN_ADD.Click
    {
        if ((TXT_FKM_CD.Text.Length < 0))
        {
            mbox("Please select the refrence no !!");
            return;
        }

        string STR_TEMP = "";
        if ((CMB_PROJNAME.Text.Trim().Length > 0))
        {
            STR_TEMP = CMB_PROJNAME.Text;
            ADD_REFRNC(STR_TEMP, "FKM_PROJNAME", "C");
        }
        string PROJNAME = STR_TEMP;

        STR_TEMP = "";
        if ((CMB_HOLDNAME.Text.Trim().Length > 0))
        {
            STR_TEMP = CMB_HOLDNAME.Text;
            ADD_REFRNC(STR_TEMP, "FKM_HOLDNAME", "C");
        }
        string HOLDNAME = STR_TEMP;

        STR_TEMP = "";
        if ((CMB_OPPRBY.Text.Trim().Length > 0))
        {
            STR_TEMP = CMB_OPPRBY.Text;
            ADD_REFRNC(STR_TEMP, "FKM_OPPRBY", "C");
        }
        string OPPRBY = STR_TEMP;

        STR_TEMP = "";
        if ((CMB_INVCOMP.Text.Trim().Length > 0))
        {
            STR_TEMP = CMB_INVCOMP.Text;
            ADD_REFRNC(STR_TEMP, "FKM_INVCOMP", "C");
        }
        string INVCOMP = STR_TEMP;

        STR_TEMP = "";
        if ((CMB_INVGRP.Text.Trim().Length > 0))
        {
            STR_TEMP = CMB_INVGRP.Text;
            //    ' Call ADD_REFRNC(STR_TEMP, "FKM_INVGRP", "C")
        }
        string INVGRP = STR_TEMP;

        string PRTCPDATE = "null";
        if ((TXT_PRTCPDATE.Text.Trim().Length > 0))
        {
            // '  Dim PDT As Date = CDate(TXT_PRTCPDATE.Text.Trim() )
            PRTCPDATE = "'" + TXT_PRTCPDATE.Text.Trim() + "'";
        }

        string MTRTDATE = "null";
        if ((TXT_MTRTDATE.Text.Trim().Length > 0))
        {
            //' Dim PDT As Date = CDate(TXT_MTRTDATE.Text.Trim() )
            MTRTDATE = "'" + TXT_MTRTDATE.Text.Trim() + "'";
        }

        STR_TEMP = "";
        if ((CMB_LOCATION.Text.Trim().Length > 0))
        {
            STR_TEMP = CMB_LOCATION.Text;
            // 'Call ADD_REFRNC(STR_TEMP, "FKM_LOCATION", "C")
        }
        string LOCATION = STR_TEMP;

        STR_TEMP = "";
        if ((CMB_CURR.Text.Trim().Length > 0))
        {
            STR_TEMP = CMB_CURR.Text;
            // 'Call ADD_REFRNC(STR_TEMP, "FKM_CURR", "C")
        }
        string CURR = STR_TEMP;

        STR_TEMP = "";
        if ((CMB_BANK.Text.Trim().Length > 0))
        {
            STR_TEMP = CMB_BANK.Text;
            //   'Call ADD_REFRNC(STR_TEMP, "FKM_BANK", "C")
        }
        string BANK = STR_TEMP;


        string YEILDPRD = CMB_YEILDPRD.Text.Trim();
        string INCGEN = CMB_INCGEN.Text.Trim();
        string PRVTEQT = CMB_PRVTEQT.Text.Trim();
        string STATUS = CMB_STATUS.Text.Trim();
        string FKM_LNK = CMB_FKM_LNK.Text.Trim();


        STR_TEMP = "0";
        if (TXT_COMMCAP.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_COMMCAP.Text.Trim();
        }
        double COMMCAP = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_COMMCAP2.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_COMMCAP2.Text.Trim();
        }
        double COMMCAP2 = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_INVAMT.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_INVAMT.Text.Trim();
        }
        double INVAMT = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_CAPPD.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_CAPPD.Text.Trim();
        }
        double CAPPD = double.Parse(STR_TEMP);


        STR_TEMP = "0";
        if (TXT_CAPUNPD.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_CAPUNPD.Text.Trim();
        }
        double CAPUNPD = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_CAPRFND.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_CAPRFND.Text.Trim();
        }
        double CAPRFND = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_EXPNS.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_EXPNS.Text.Trim();
        }
        double EXPNS = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_ROI.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_ROI.Text.Trim();
        }
        double ROI = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_BOOKVAL.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_BOOKVAL.Text.Trim();
        }
        double BOOKVAL = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_MONYCAL.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_MONYCAL.Text.Trim();
        }
        double MYCAL = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_QRTYCAL.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_QRTYCAL.Text.Trim();
        }
        double QYCAL = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_SMANYCAL.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_SMANYCAL.Text.Trim();
        }
        double SYCAL = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_ANLYCAL.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_ANLYCAL.Text.Trim();
        }
        double AYCAL = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_SCNINCP.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_SCNINCP.Text.Trim();
        }
        double SCNINCP = double.Parse(STR_TEMP);


        STR_TEMP = "0";
        if (TXT_SALEPRCD.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_SALEPRCD.Text.Trim();
        }
        double SALEPRCD = double.Parse(STR_TEMP);

        string USR = User.Identity.Name.ToUpper();
        string UPDDT = System.DateTime.Today.ToString("dd/MM/yyyy");
        string UPDTIME = System.DateTime.Now.ToString("hh:mm:ss");
        string CODE = TXT_FKM_CD.Text;
        string COMP_CD = CMB_COMP_CD.SelectedItem.Value;


        try
        {
            string RETRVQRY = "SELECT * FROM INVEST_INFO WHERE FKM_SRL = '" + CODE + "'   ";

            if (dbo.RecExist(RETRVQRY) == false)
            { 
                string INSQRY = "INSERT INTO INVEST_INFO VALUES('" + COMP_CD + "', '" + CODE + "','" + UPDDT + "','" + CURR + "','" + PROJNAME +
                                       "','" + HOLDNAME + "','" + LOCATION + "','" + OPPRBY + "','" + INVCOMP + "',null," + PRTCPDATE +
                                       "," + MTRTDATE + ",'" + PRVTEQT + "','" + BANK +
                                       "','" + YEILDPRD + "','" + INCGEN + "','" + STATUS + "','" + FKM_LNK +
                                       "','" + COMMCAP + "','" + COMMCAP2 + "','" + INVAMT + "','" + CAPPD +
                                       "','" + CAPUNPD + "','" + CAPRFND + "','" + EXPNS + "','" + ROI +
                                       "','" + BOOKVAL + "','0','0','0','0','0','0','0','0','" + MYCAL +
                                       "','" + QYCAL + "','" + SYCAL + "','" + AYCAL + "','0','" + SCNINCP +
                                       "','0','00.00','0','" + TXT_REMX.Text +
                                       "','','" + USR + "','" + UPDDT + "','" + UPDTIME + "',null)";

                if (dbo.InsRecord(INSQRY) == false)
                {
                    mbox("ERROR !!");
                }
                else
                {
                    if (dbo.sendmail((string)Session["USER_MAIL_ID"], PROJNAME + "- NEW INVESTMENT ADDED ", "Test") == true)
                    {
                        //' StatusLabel.Text = "Upload status: mail sent!"
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


    public void BTN_UPDATE_Click(object sender, System.EventArgs e)// Handles BTN_ADD.Click
    {
        if ((CMB_FKM_CD.SelectedIndex < 0))
        {
            mbox("Please select the refrence !!");
            return;
        }

        //'Dim SRL As string = TXT_FKM_CD.Text
        string STR_TEMP = "";
        if ((CMB_PROJNAME.Text.Trim().Length > 0))
        {
            STR_TEMP = CMB_PROJNAME.Text;
            ADD_REFRNC(STR_TEMP, "FKM_PROJNAME", "C");
        }
        string PROJNAME = STR_TEMP;

        STR_TEMP = "";
        if ((CMB_HOLDNAME.Text.Trim().Length > 0))
        {
            STR_TEMP = CMB_HOLDNAME.Text;
            ADD_REFRNC(STR_TEMP, "FKM_HOLDNAME", "C");
        }
        string HOLDNAME = STR_TEMP;

        STR_TEMP = "";
        if ((CMB_OPPRBY.Text.Trim().Length > 0))
        {
            STR_TEMP = CMB_OPPRBY.Text;
            ADD_REFRNC(STR_TEMP, "FKM_OPPRBY", "C");
        }
        string OPPRBY = STR_TEMP;

        STR_TEMP = "";
        if ((CMB_INVCOMP.Text.Trim().Length > 0))
        {
            STR_TEMP = CMB_INVCOMP.Text;
            ADD_REFRNC(STR_TEMP, "FKM_INVCOMP", "C");
        }
        string INVCOMP = STR_TEMP;

        STR_TEMP = "";
        if ((CMB_INVGRP.Text.Trim().Length > 0))
        {
            STR_TEMP = CMB_INVGRP.Text;
            //    ' Call ADD_REFRNC(STR_TEMP, "FKM_INVGRP", "C")
        }
        string INVGRP = STR_TEMP;

        string PRTCPDATE = "null";
        if ((TXT_PRTCPDATE.Text.Trim().Length > 0))
        {
            // '  Dim PDT As Date = CDate(TXT_PRTCPDATE.Text.Trim() )
            PRTCPDATE = "'" + TXT_PRTCPDATE.Text.Trim() + "'";
        }

        string MTRTDATE = "null";
        if ((TXT_MTRTDATE.Text.Trim().Length > 0))
        {
            //' Dim PDT As Date = CDate(TXT_MTRTDATE.Text.Trim() )
            MTRTDATE = "'" + TXT_MTRTDATE.Text.Trim() + "'";
        }

        STR_TEMP = "";
        if ((CMB_LOCATION.Text.Trim().Length > 0))
        {
            STR_TEMP = CMB_LOCATION.Text;
            // 'Call ADD_REFRNC(STR_TEMP, "FKM_LOCATION", "C")
        }
        string LOCATION = STR_TEMP;

        STR_TEMP = "";
        if ((CMB_CURR.Text.Trim().Length > 0))
        {
            STR_TEMP = CMB_CURR.Text;
            // 'Call ADD_REFRNC(STR_TEMP, "FKM_CURR", "C")
        }
        string CURR = STR_TEMP;

        STR_TEMP = "";
        if ((CMB_BANK.Text.Trim().Length > 0))
        {
            STR_TEMP = CMB_BANK.Text;
            //   'Call ADD_REFRNC(STR_TEMP, "FKM_BANK", "C")
        }
        string BANK = STR_TEMP;


        string YEILDPRD = CMB_YEILDPRD.Text.Trim();
        string INCGEN = CMB_INCGEN.Text.Trim();
        string PRVTEQT = CMB_PRVTEQT.Text.Trim();
        string STATUS = CMB_STATUS.Text.Trim();
        string FKM_LNK = CMB_FKM_LNK.Text.Trim();


        STR_TEMP = "0";
        if (TXT_COMMCAP.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_COMMCAP.Text.Trim();
        }
        double COMMCAP = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_COMMCAP2.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_COMMCAP2.Text.Trim();
        }
        double COMMCAP2 = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_INVAMT.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_INVAMT.Text.Trim();
        }
        double INVAMT = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_CAPPD.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_CAPPD.Text.Trim();
        }
        double CAPPD = double.Parse(STR_TEMP);


        STR_TEMP = "0";
        if (TXT_CAPUNPD.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_CAPUNPD.Text.Trim();
        }
        double CAPUNPD = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_CAPRFND.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_CAPRFND.Text.Trim();
        }
        double CAPRFND = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_EXPNS.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_EXPNS.Text.Trim();
        }
        double EXPNS = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_ROI.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_ROI.Text.Trim();
        }
        double ROI = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_BOOKVAL.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_BOOKVAL.Text.Trim();
        }
        double BOOKVAL = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_MONYCAL.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_MONYCAL.Text.Trim();
        }
        double MYCAL = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_QRTYCAL.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_QRTYCAL.Text.Trim();
        }
        double QYCAL = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_SMANYCAL.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_SMANYCAL.Text.Trim();
        }
        double SYCAL = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_ANLYCAL.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_ANLYCAL.Text.Trim();
        }
        double AYCAL = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_SCNINCP.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_SCNINCP.Text.Trim();
        }
        double SCNINCP = double.Parse(STR_TEMP);


        STR_TEMP = "0";
        if (TXT_SALEPRCD.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_SALEPRCD.Text.Trim();
        }
        double SALEPRCD = double.Parse(STR_TEMP);

        string USR = User.Identity.Name.ToUpper();
        string UPDDT = System.DateTime.Today.ToString("dd/MM/yyyy");
        string UPDTIME = System.DateTime.Now.ToString("hh:mm:ss");
        string CODE = CMB_FKM_CD.SelectedValue;
        string COMP_CD = CMB_COMP_CD.SelectedItem.Value;

        try
        {
            string RETRVQRY = "SELECT * FROM INVEST_INFO WHERE FKM_SRL = '" + CODE + "'   ";

            if (dbo.RecExist(RETRVQRY) == true)
            {
                string INSQRY = "UPDATE INVEST_INFO SET COMP_CD = '" + COMP_CD + "', FKM_ISSDATE = '" + UPDDT + "',FKM_CURR ='" + CURR + "',FKM_PROJNAME ='" + PROJNAME +
                                       "',FKM_HOLDNAME = '" + HOLDNAME + "',FKM_LOCATION = '" + LOCATION + "',FKM_OPPRBY = '" + OPPRBY + "',FKM_INVCOMP = '" + INVCOMP +
                                       "',FKM_PRTCPDATE = " + PRTCPDATE + ",FKM_MTRTDATE = " + MTRTDATE + ",FKM_PRVTEQT = '" + PRVTEQT + "',FKM_BANK = '" + BANK +
                                       "',FKM_YEILDPRD = '" + YEILDPRD + "',FKM_INCGEN = '" + INCGEN + "',FKM_STATUS = '" + STATUS + "',FKM_LNKSRL = '" + FKM_LNK +
                                       "',FKM_COMMCAP = '" + COMMCAP + "',FKM_COMMCAP2 = '" + COMMCAP2 + "',FKM_INVAMT = '" + INVAMT + "',FKM_CAPPD = '" + CAPPD +
                                       "',FKM_CAPUNPD = '" + CAPUNPD + "',FKM_CAPRFND = '" + CAPRFND + "',FKM_EXPNS = '" + EXPNS + "',FKM_ROI = '" + ROI +
                                       "',FKM_BOOKVAL = '" + BOOKVAL + "',FKM_MONYCAL = '" + MYCAL + "',FKM_QRTYCAL = '" + QYCAL + "',FKM_INVGRP = '" + INVGRP +
                                       "',FKM_SMANYCAL = '" + SYCAL + "',FKM_ANLYCAL = '" + AYCAL + "',FKM_SCNINCP = '" + SCNINCP +
                                       "',FKM_SALEPRCD = '" + SALEPRCD + "',FKM_REMX = '" + TXT_REMX.Text +
                                       "',FKM_UPDBY = '" + USR + "',FKM_UPDDATE = '" + UPDDT + "',FKM_UPDTIME = '" + UPDTIME + "'" +
                                       " WHERE  FKM_SRL = '" + CODE + "'   ";

                //'"',FKM_ANLINCMCY = '" + ANLINCMCY + "',FKM_ANLRLZD = '" + ANLRLZD + "',FKM_ACTLINCMRU = '" + ACTLINCMRU +
                //'"',FKM_UNRLYLD = '" + UNRLYLD + "',FKM_DVDND = '" + DVDND + "',FKM_VALVTN = '" + VALVTN + "',FKM_CAPGN = '" + CAPGN +
                //'"',FKM_UNRLDVD = '" + UNRLDVD + "',FKM_UNRLDVDCAP = '" + UNRLDVDCAP + "',FKM_FAIRVAL = '" + FAIRVAL +
                //'"',FKM_NAV = '" + NAV + 
                //'  "',FKM_INVGRP = '" + INVGRP +
                if (dbo.InsRecord(INSQRY) == false)
                {
                    mbox("ERROR !!");
                }
                else
                {
                    if (dbo.sendmail((string)Session["USER_MAIL_ID"], PROJNAME + "- REVISED INVESTMENT DETAILS ", "Test") == true)
                    {
                        //' StatusLabel.Text = "Upload status: mail sent!"
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

    public string NEXT_REC_SRL()
    {
        TXT_FKM_CD.Text = "";
        string fkmsrl = "";
        DataTable DTINVINF = new DataTable();
        try
        {

            string RETRVQRY = "SELECT MAX (FKM_SRL) FROM INVEST_INFO ";
            DTINVINF = dbo.SelTable(RETRVQRY);
            if (DTINVINF.Rows.Count > 0)
            {
                double NSRL = double.Parse(DTINVINF.Rows[0][0].ToString().Substring(3));
                fkmsrl = "FKM" + (NSRL + 1).ToString("0000000");
            }
            else
            {
                fkmsrl = "FKM00000001";
            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }
        return fkmsrl;
    }



    public void BTN_CLEAR_Click(object sender, System.EventArgs e) //Handles BTN_CLEAR.Click
    {
        CLEAR();
    }

    public void CMB_FKM_CD_SelectedIndexChanged(object sender, System.EventArgs e) //Handles CMB_FKM_CD.SelectedIndexChanged
    {
        string CODE = CMB_FKM_CD.SelectedValue;
        CMB_FKM_CD_Changed(CODE);
    }

    public void CMB_FKM_CD_Changed(string CODE)
    {
        CHBX_EDIT.Checked = true;
        CLEAR();
        try
        {
            string RETRVQRY = "SELECT * FROM INVEST_INFO WHERE FKM_SRL = '" + CODE + "'  ";
            DataTable dtinfo = dbo.SelTable(RETRVQRY);
            if (dtinfo.Rows.Count == 1)
            {

                for (int I = 0; I <= CMB_PROJNAME.Items.Count - 1; I++)
                {
                    if (dtinfo.Rows[0]["FKM_PROJNAME"].ToString().Trim() == CMB_PROJNAME.Items[I].Text.Trim())
                    {
                        CMB_PROJNAME.SelectedIndex = I;
                    }
                }
                for (int I = 0; I <= CMB_HOLDNAME.Items.Count - 1; I++)
                {
                    if (dtinfo.Rows[0]["FKM_HOLDNAME"].ToString().Trim() == CMB_HOLDNAME.Items[I].Text.Trim())
                    {
                        CMB_HOLDNAME.SelectedIndex = I;
                    }
                }
                for (int I = 0; I <= CMB_OPPRBY.Items.Count - 1; I++)
                {
                    if (dtinfo.Rows[0]["FKM_OPPRBY"].ToString().Trim() == CMB_OPPRBY.Items[I].Text.Trim())
                    {
                        CMB_OPPRBY.SelectedIndex = I;
                    }
                }
                for (int I = 0; I <= CMB_INVCOMP.Items.Count - 1; I++)
                {
                    if (dtinfo.Rows[0]["FKM_INVCOMP"].ToString().Trim() == CMB_INVCOMP.Items[I].Text.Trim())
                    {
                        CMB_INVCOMP.SelectedIndex = I;
                    }
                }
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
                for (int I = 0; I <= CMB_LOCATION.Items.Count - 1; I++)
                {
                    if (dtinfo.Rows[0]["FKM_LOCATION"].ToString().Trim() == CMB_LOCATION.Items[I].Text.Trim())
                    {
                        CMB_LOCATION.SelectedIndex = I;
                    }
                }
                //' CMB_LOCATION.SelectedIndex = CMB_LOCATION.Items.IndexOf(CMB_LOCATION.Items.(dtinfo.Rows[0]["FKM_LOCATION"].ToString().Trim()))
                CMB_CURR.SelectedIndex = CMB_CURR.Items.IndexOf(CMB_CURR.Items.FindByValue(dtinfo.Rows[0]["FKM_CURR"].ToString().Trim()));
                //' CMB_BANK.SelectedIndex = CMB_BANK.Items.IndexOf(CMB_BANK.Items.FindByValue(dtinfo.Rows[0]["FKM_BANK"].ToString().Trim()))
                for (int I = 0; I <= CMB_BANK.Items.Count - 1; I++)
                {
                    if (dtinfo.Rows[0]["FKM_BANK"].ToString().Trim() == CMB_BANK.Items[I].Text.Trim())
                    {
                        CMB_BANK.SelectedIndex = I;
                    }
                }

                //'For I = 0 To CMB_COMP_CD.Items.Count - 1
                //'    if( dtinfo.Rows[0]["COMP_CD"].ToString().Trim() == CMB_COMP_CD.Items[I].Value.Trim ) {
                //'        CMB_COMP_CD.SelectedIndex = I
                //'   }
                //'Next
                CMB_COMP_CD.SelectedIndex = CMB_COMP_CD.Items.IndexOf(CMB_COMP_CD.Items.FindByValue(dtinfo.Rows[0]["COMP_CD"].ToString().Trim()));

                CMB_YEILDPRD.SelectedIndex = CMB_YEILDPRD.Items.IndexOf(CMB_YEILDPRD.Items.FindByValue(dtinfo.Rows[0]["FKM_YEILDPRD"].ToString().Trim()));
                CMB_INCGEN.SelectedIndex = CMB_INCGEN.Items.IndexOf(CMB_INCGEN.Items.FindByValue(dtinfo.Rows[0]["FKM_INCGEN"].ToString().Trim()));
                CMB_PRVTEQT.SelectedIndex = CMB_PRVTEQT.Items.IndexOf(CMB_PRVTEQT.Items.FindByValue(dtinfo.Rows[0]["FKM_PRVTEQT"].ToString().Trim()));
                CMB_STATUS.SelectedIndex = CMB_STATUS.Items.IndexOf(CMB_STATUS.Items.FindByValue(dtinfo.Rows[0]["FKM_STATUS"].ToString().Trim()));
                CMB_FKM_LNK.SelectedIndex = CMB_FKM_LNK.Items.IndexOf(CMB_FKM_LNK.Items.FindByValue(dtinfo.Rows[0]["FKM_LNKSRL"].ToString().Trim()));

                //'TXT_PROJNAME.Text = dtinfo.Rows[0]["FKM_PROJNAME"].ToString().Trim()
                //'TXT_HOLDNAME.Text = dtinfo.Rows[0]["FKM_HOLDNAME"].ToString().Trim()
                //'TXT_OPPRBY.Text = dtinfo.Rows[0]["FKM_OPPRBY"].ToString().Trim()
                //'TXT_INVCOMP.Text = dtinfo.Rows[0]["FKM_INVCOMP"].ToString().Trim()
                TXT_PRTCPDATE.Text = dtinfo.Rows[0]["FKM_PRTCPDATE"].ToString().Trim();
                TXT_MTRTDATE.Text = dtinfo.Rows[0]["FKM_MTRTDATE"].ToString().Trim();
                //'TXT_LOCATION.Text = dtinfo.Rows[0]["FKM_LOCATION"].ToString().Trim()
                //'TXT_CURR.Text = dtinfo.Rows[0]["FKM_CURR"].ToString().Trim()
                //'TXT_BANK.Text = dtinfo.Rows[0]["FKM_BANK"].ToString().Trim()
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
                //'TXT_ANLINCMCY.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_ANLINCMCY"))
                //'TXT_ANLRLZD.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_ANLRLZD"))
                //'TXT_ACTLINCMRU.Text = dtinfo.Rows[0]["FKM_ACTLINCMRU"].ToString().Trim()

                //'TXT_UNRLYLD.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_UNRLYLD"))
                //'TXT_DVDND.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_DVDND"))
                //'TXT_VALVTN.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_VALVTN"))
                //'TXT_CAPGN.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_CAPGN"))
                //'TXT_UNRLDVD.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_UNRLDVD"))
                //'TXT_UNRLDVDCAP.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_UNRLDVDCAP"))
                //'TXT_FAIRVAL.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_FAIRVAL"))
                //'TXT_NAV.Text = dtinfo.Rows[0]["FKM_NAV"].ToString().Trim()
                TXT_SALEPRCD.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_SALEPRCD"]);
                TXT_REMX.Text = dtinfo.Rows[0]["FKM_REMX"].ToString().Trim();
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

    public void ADD_REFRNC(string CODE, string FLDTYP, string CDTYP)
    {
        if (CODE.Trim().Length < 1)
        {
            return;
        }

        string USR = User.Identity.Name.ToUpper();
        string UPDDT = System.DateTime.Today.ToString("dd/MM/yyyy");
        string UPDTIME = System.DateTime.Now.ToString("hh:mm:ss");
        // ' Dim CODE As string = TXT_CODE.Text.ToUpper
        string SRL = NEXT_RFRNCC_SRL();
        try
        {
            string RETRVQRY = "SELECT * FROM FKM_REFRNC WHERE RF_DESCRP LIKE '%" + CODE + "%' ";

            if (dbo.RecExist(RETRVQRY) == false)
            {
                string INSQRY = "INSERT INTO FKM_REFRNC VALUES('" + SRL + "','" + SRL + "','" + CDTYP +
                                       "','" + CODE.ToUpper() + "','" + FLDTYP.ToUpper() + "','0','','" +
                                       "','" + USR + "','" + UPDDT + "','" + UPDTIME + "')";

                if (dbo.InsRecord(INSQRY) == false)
                {
                    mbox("ERROR !!");
                }
                else
                {
                    //   '  mbox("CODE : '" + CODE + "'   ADDED SUCCESSFULLY !!")
                }

            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }

    }

    public string NEXT_RFRNCC_SRL()
    {
        string SRL = "";
        DataTable DTINVINF = new DataTable();
        try
        {
            string RETRVQRY = "SELECT MAX (RF_SRL) FROM FKM_REFRNC ";
            DTINVINF = dbo.SelTable(RETRVQRY);
            if ( DTINVINF.Rows[0][0].ToString().Length !=0)
            {
                double NSRL = double.Parse(DTINVINF.Rows[0][0].ToString());
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

    //protected void CMB_FKM_CD_TextChanged(object sender, System.EventArgs e)// Handles CMB_FKM_CD.TextChanged
    //{
    //    string CODE = CMB_FKM_CD.Text.Trim();
    //    CMB_FKM_CD_Changed(CODE);
    //}

    //    'Protected Sub CMB_YEILDPRD_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CMB_YEILDPRD.SelectedIndexChanged


    //    '    Dim STR_TEMP As string = "0"
    //    '    if( TXT_COMMCAP.Text.Trim() .Length > 0 ) {
    //    '        STR_TEMP = TXT_COMMCAP.Text.Trim() 
    //    '   }
    //    '    Dim COMMCAP As double = double.Parse(STR_TEMP)

    //    '    STR_TEMP = "0"
    //    '    if( TXT_COMMCAP2.Text.Trim() .Length > 0 ) {
    //    '        STR_TEMP = TXT_COMMCAP2.Text.Trim() 
    //    '   }
    //    '    Dim COMMCAP2 As double = double.Parse(STR_TEMP)

    //    '    if( COMMCAP <= 0 ) {
    //    '        COMMCAP = COMMCAP2
    //    '   }


    //    '    STR_TEMP = "0"
    //    '    if( TXT_ROI.Text.Trim() .Length > 0 ) {
    //    '        STR_TEMP = TXT_ROI.Text.Trim() 
    //    '   }
    //    '    Dim ROI As double = double.Parse(STR_TEMP)

    //    '    TXT_ANLYCAL.Text = 0
    //    '    TXT_MONYCAL.Text = 0
    //    '    TXT_QRTYCAL.Text = 0
    //    '    TXT_SMANYCAL.Text = 0
    //    '    if( CMB_YEILDPRD.SelectedIndex = 1 ) {
    //    '        TXT_MONYCAL.Text = (COMMCAP * (ROI * (0.01))) / 12
    //    '        TXT_ANLYCAL.Text = (COMMCAP * (ROI * (0.01)))
    //    '    }else {if( CMB_YEILDPRD.SelectedIndex = 2 ) {
    //    '        TXT_QRTYCAL.Text = (COMMCAP * (ROI * (0.01))) / 4
    //    '        TXT_ANLYCAL.Text = (COMMCAP * (ROI * (0.01)))
    //    '    }else {if( CMB_YEILDPRD.SelectedIndex = 3 ) {
    //    '        TXT_SMANYCAL.Text = (COMMCAP * (ROI * (0.01))) / 2
    //    '        TXT_ANLYCAL.Text = (COMMCAP * (ROI * (0.01)))
    //    '   }


    //    'End Sub

    protected void INIT_FKMCD()
    {
        //' Dim COMP_CD As string = Session("USER_COMP_CD")
        //'Dim RETRVQRY As string = "SELECT DISTINCT FKM_SRL ,FKM_SRL + '-' + FKM_PROJNAME AS FKM_PROJNAME  FROM INVEST_INFO  WHERE COMP_CD = '" + COMP_CD + "' ORDER BY FKM_SRL"
        string RETRVQRY = "SELECT DISTINCT FKM_SRL ,FKM_SRL + '-' + FKM_PROJNAME AS FKM_PROJNAME  FROM INVEST_INFO  ORDER BY FKM_SRL";
        DataTable dtinfo = dbo.SelTable(RETRVQRY);
        CMB_FKM_CD.DataSource = dtinfo;
        CMB_FKM_CD.DataBind();
        CMB_FKM_CD.SelectedIndex = -1;

        CMB_FKM_LNK.DataSource = dtinfo;
        CMB_FKM_LNK.DataBind();
        CMB_FKM_LNK.SelectedIndex = -1;

    }

    //protected void BTNSRCH_Click(object sender, System.EventArgs e)// Handles CMB_FKM_CD.TextChanged
    //{
    //    GET_GRID_INFO();
    //}
    //protected void GET_GRID_INFO()
    //{
    //    try
    //    {
    //        string RETRVQRY = "SELECT * FROM INVEST_INFO  ";

    //        DataTable dtinfo = dbo.SelTable(RETRVQRY);
    //        if (dtinfo.Rows.Count > 0)
    //        {
    //            dtinfo.Columns.Add("FKM_REG");
    //            foreach (DataRow DR in dtinfo.Rows)
    //            {
    //                DR["FKM_REG"] = (string)dbo.GET_FRM_COMP_CD(DR["COMP_CD"].ToString(), 1);

    //            }

    //            GridView1.DataSource = dtinfo;
    //            GridView1.DataBind();
    //        }
    //        else
    //        {

    //            GridView1.DataSource = null;
    //            GridView1.DataBind();
    //        }
    //        //' Session("BANKMTNC_dtINFO_REPORT") = dtinfo
    //    }
    //    catch (Exception EX)
    //    {
    //        mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
    //    }
    //}
    public void GET_GRID_INFO( )
    {
        try
        { 
            string RETRVQRY = "SELECT * FROM INVEST_INFO ";
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
            DTINVINFO.Columns.Add("FKM_CURRSYMB", typeof(string));
            DTINVINFO.Columns.Add("LOGO", typeof(string));

            DataTable dt_invest_info = DTINVINFO.Clone();
            dt_invest_info.Rows.Clear();
            REFINE_ROW_INV_DETAILS(ref DTINVINFO, ref dt_invest_info, "US DOLLAR", "$");
            REFINE_ROW_INV_DETAILS(ref DTINVINFO, ref  dt_invest_info, "UK POUND", "£");
            REFINE_ROW_INV_DETAILS(ref DTINVINFO, ref dt_invest_info, "EURO", "€");

            Session["EDITINV_dt_invest_info"] = null;
            if (dt_invest_info.Rows.Count > 0)
            {
                Session["EDITINV_dt_invest_info"] = dt_invest_info;
                GridView1.DataSource = dt_invest_info;
                GridView1.DataBind();
            }
        } }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }
    }
    public void REFINE_ROW_INV_DETAILS(ref DataTable DTINVINFO, ref DataTable dt_invest_info, string CURR, string CURRSYMB)
    {   //'  Dim RETRVQRY As string = "SELECT * FROM INVEST_INFO "
        //'Dim DTINVINFO As DataTable = dbo.SelTable(QRY)
        //'if DTINVINFO.Rows.Count > 0 ){
        //'    Dim dt_invest_info As DataTable = DTINVINFO.Clone
        //'    dt_invest_info.Rows.Clear()


        DataRow[] DR_USD = DTINVINFO.Select("FKM_CURR  = '" + CURR + "' ");
        DataRow DR_TOT_USD = dt_invest_info.NewRow();
        if (DR_USD.Length > 0)
        {


            DR_TOT_USD["FKM_CURR"] = CURR;
            DR_TOT_USD["FKM_PROJNAME"] = "TOTAL PROJECTS: " + DR_USD.Length;
            DR_TOT_USD["FKM_LOCATION"] = "TOTAL (" + CURR + ")";

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
            DR_TOT_USD["FKM_CURRSYMB"] = CURRSYMB;
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
                DR_USD[I]["FKM_CURRSYMB"] = CURRSYMB;
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
                DR_USD[I]["LOGO"] = dbo.ReadFile(Server.MapPath((string)dbo.GET_FRM_COMP_CD(DR_USD[I]["COMP_CD"].ToString(), 2)));

                //DR_USD[I]["FOOTER"] = User.Identity.Name.ToUpper();

                DR_USD[I]["FOOTER"] = User.Identity.Name.ToUpper() + "   Printed on : " + System.DateTime.Now.ToShortDateString() + "  " + System.DateTime.Now.ToLongTimeString();
                dt_invest_info.ImportRow(DR_USD[I]);
            }


            if (decimal.Parse(DR_TOT_USD["FKM_COMMCAP"].ToString()) == 0)
            {
                DR_TOT_USD["FKM_COMMCAP"] = System.DBNull.Value;
            }
            else
            {
                DR_TOT_USD["FKM_COMMCAP_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_COMMCAP"]);
            }
            if (decimal.Parse(DR_TOT_USD["FKM_COMMCAP2"].ToString()) == 0)
            {
                DR_TOT_USD["FKM_COMMCAP2"] = System.DBNull.Value;
            }
            else
            {
                DR_TOT_USD["FKM_COMMCAP2_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_COMMCAP2"]);
            }
            if (decimal.Parse(DR_TOT_USD["FKM_INVAMT"].ToString()) == 0)
            {
                DR_TOT_USD["FKM_INVAMT"] = System.DBNull.Value;
            }
            else
            {
                DR_TOT_USD["FKM_INVAMT_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_INVAMT"]);
            }
            if (decimal.Parse(DR_TOT_USD["FKM_CAPPD"].ToString()) == 0)
            {
                DR_TOT_USD["FKM_CAPPD"] = System.DBNull.Value;
            }
            else
            {
                DR_TOT_USD["FKM_CAPPD_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_CAPPD"]);
            }
            if (decimal.Parse(DR_TOT_USD["FKM_CAPUNPD"].ToString()) == 0)
            {
                DR_TOT_USD["FKM_CAPUNPD"] = System.DBNull.Value;
            }
            else
            {
                DR_TOT_USD["FKM_CAPUNPD_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_CAPUNPD"]);
            }
            if (decimal.Parse(DR_TOT_USD["FKM_EXPNS"].ToString()) == 0)
            {
                DR_TOT_USD["FKM_EXPNS"] = System.DBNull.Value;
            }
            else
            {
                DR_TOT_USD["FKM_EXPNS_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_EXPNS"]);
            }
            if (decimal.Parse(DR_TOT_USD["FKM_ROI"].ToString()) == 0)
            {
                DR_TOT_USD["FKM_ROI"] = System.DBNull.Value;
            }
            else
            {
                DR_TOT_USD["FKM_ROI_R"] = string.Format("{0:#0.00}", DR_TOT_USD["FKM_ROI"].ToString()) + "%";
            }
            if (decimal.Parse(DR_TOT_USD["FKM_BOOKVAL"].ToString()) == 0)
            {
                DR_TOT_USD["FKM_BOOKVAL"] = System.DBNull.Value;
            }
            else
            {
                DR_TOT_USD["FKM_BOOKVAL_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_BOOKVAL"]);
            }
            if (decimal.Parse(DR_TOT_USD["FKM_MONYCAL"].ToString()) == 0)
            {
                DR_TOT_USD["FKM_MONYCAL"] = System.DBNull.Value;
            }
            else
            {
                DR_TOT_USD["FKM_MONYCAL_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_MONYCAL"]);
            }
            if (decimal.Parse(DR_TOT_USD["FKM_QRTYCAL"].ToString()) == 0)
            {
                DR_TOT_USD["FKM_QRTYCAL"] = System.DBNull.Value;
            }
            else
            {
                DR_TOT_USD["FKM_QRTYCAL_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_QRTYCAL"]);
            }
            if (decimal.Parse(DR_TOT_USD["FKM_SMANYCAL"].ToString()) == 0)
            {
                DR_TOT_USD["FKM_SMANYCAL"] = System.DBNull.Value;
            }
            else
            {
                DR_TOT_USD["FKM_SMANYCAL_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_SMANYCAL"]);
            }
            if (decimal.Parse(DR_TOT_USD["FKM_ANLYCAL"].ToString()) == 0)
            {
                DR_TOT_USD["FKM_ANLYCAL"] = System.DBNull.Value;
            }
            else
            {
                DR_TOT_USD["FKM_ANLYCAL_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_ANLYCAL"]);
            }
            if (decimal.Parse(DR_TOT_USD["FKM_SCNINCP"].ToString()) == 0)
            {
                DR_TOT_USD["FKM_SCNINCP"] = System.DBNull.Value;
            }
            else
            {
                DR_TOT_USD["FKM_SCNINCP_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_SCNINCP"]);
            }
            if (decimal.Parse(DR_TOT_USD["FKM_ANLINCMCY"].ToString()) == 0)
            {
                DR_TOT_USD["FKM_ANLINCMCY"] = System.DBNull.Value;
            }
            else
            {
                DR_TOT_USD["FKM_ANLINCMCY_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_ANLINCMCY"]);
            }
            if (decimal.Parse(DR_TOT_USD["FKM_ANLRLZD"].ToString()) == 0)
            {
                DR_TOT_USD["FKM_ANLRLZD"] = System.DBNull.Value;
            }
            else
            {
                DR_TOT_USD["FKM_ANLRLZD_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_ANLRLZD"]);
            }
            if (decimal.Parse(DR_TOT_USD["FKM_ACTLINCMRU"].ToString()) == 0)
            {
                DR_TOT_USD["FKM_ACTLINCMRU"] = System.DBNull.Value;
            }
            else
            {
                DR_TOT_USD["FKM_ACTLINCMRU_R"] = string.Format("{0:#0.00}", DR_TOT_USD["FKM_ACTLINCMRU"].ToString()) + "%";
            }
            if (decimal.Parse(DR_TOT_USD["FKM_UNRLYLD"].ToString()) == 0)
            {
                DR_TOT_USD["FKM_UNRLYLD"] = System.DBNull.Value;
            }
            else
            {
                DR_TOT_USD["FKM_UNRLYLD_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_UNRLYLD"]);
            }
            if (decimal.Parse(DR_TOT_USD["FKM_DVDND"].ToString()) == 0)
            {
                DR_TOT_USD["FKM_DVDND"] = System.DBNull.Value;
            }
            else
            {
                DR_TOT_USD["FKM_DVDND_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_DVDND"]);
            }
            if (decimal.Parse(DR_TOT_USD["FKM_VALVTN"].ToString()) == 0)
            {
                DR_TOT_USD["FKM_VALVTN"] = System.DBNull.Value;
            }
            else
            {
                DR_TOT_USD["FKM_VALVTN_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_VALVTN"]);
            }
            if (decimal.Parse(DR_TOT_USD["FKM_CAPGN"].ToString()) == 0)
            {
                DR_TOT_USD["FKM_CAPGN"] = System.DBNull.Value;
            }
            else
            {
                DR_TOT_USD["FKM_CAPGN_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_CAPGN"]);
            }
            if (decimal.Parse(DR_TOT_USD["FKM_UNRLDVD"].ToString()) == 0)
            {
                DR_TOT_USD["FKM_UNRLDVD"] = System.DBNull.Value;
            }
            else
            {
                DR_TOT_USD["FKM_UNRLDVD_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_UNRLDVD"]);
            }
            if (decimal.Parse(DR_TOT_USD["FKM_UNRLDVDCAP"].ToString()) == 0)
            {
                DR_TOT_USD["FKM_UNRLDVDCAP"] = System.DBNull.Value;
            }
            else
            {
                DR_TOT_USD["FKM_UNRLDVDCAP_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_UNRLDVDCAP"]);
            }
            if (decimal.Parse(DR_TOT_USD["FKM_FAIRVAL"].ToString()) == 0)
            {
                DR_TOT_USD["FKM_FAIRVAL"] = System.DBNull.Value;
            }
            else
            {
                DR_TOT_USD["FKM_FAIRVAL_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_FAIRVAL"]);
            }
            if (decimal.Parse(DR_TOT_USD["FKM_NAV"].ToString()) == 0)
            {
                DR_TOT_USD["FKM_NAV"] = System.DBNull.Value;
            }
            else
            {
                DR_TOT_USD["FKM_NAV_R"] = string.Format("{0:#0.00}", DR_TOT_USD["FKM_NAV"]);
            }
            if (decimal.Parse(DR_TOT_USD["FKM_SALEPRCD"].ToString()) == 0)
            {
                DR_TOT_USD["FKM_SALEPRCD"] = System.DBNull.Value;
            }
            else
            {
                DR_TOT_USD["FKM_SALEPRCD_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_SALEPRCD"]);
            }

            // 'DR_TOT_USD["FKM_REG") = (string)dbo.GET_FRM_COMP_CD(Session("USER_COMP_CD"), 1)
            //  '  DR_TOT_USD["LOGO") = dbo.ReadFile(Server.MapPath((string)dbo.GET_FRM_COMP_CD(Session("USER_COMP_CD"), 2)))

            DR_TOT_USD["FOOTER"] = User.Identity.Name.ToUpper();

            dt_invest_info.Rows.Add(DR_TOT_USD);
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

    protected void GridView1_SelectedIndexChanged(object sender, System.EventArgs e)// Handles GridView1.SelectedIndexChanged
    {      //'CMB_FKM_CD.DataBind()
        CMB_PROJNAME.DataBind();
        CMB_HOLDNAME.DataBind();
        CMB_OPPRBY.DataBind();
        CMB_INVCOMP.DataBind();
        CMB_INVGRP.DataBind();
        CMB_LOCATION.DataBind();
        CMB_CURR.DataBind();
        CMB_BANK.DataBind();
        if (GridView1.SelectedDataKey[0].ToString().Trim().Length != 0)
        {
            string REFCODE = (string)GridView1.SelectedDataKey[0];
            CMB_FKM_CD.SelectedIndex = CMB_FKM_CD.Items.IndexOf(CMB_FKM_CD.Items.FindByValue(REFCODE));
            string CODE = CMB_FKM_CD.SelectedValue;
            CMB_FKM_CD_Changed(CODE);
        }
    }
    public void mbox(string val)
    {
        //string cs   = "c";
        //string cst  = "<script type=""text/javascript"">" + "alert('" + val + "');</" + "script>";
        //Page page    = HttpContext.Current.Handler;
        //page.ClientScript.RegisterStartupScript(Me.typeof, cs, cst);

        string USR = User.Identity.Name.ToUpper();
        dbo.ErrorLog(USR + " : INVEST_EDIT.aspx : " + val, Server.MapPath("~\\Logs\\ErrorLog"));
    }
}
