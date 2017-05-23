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
public partial class INVEST_LQD_DTL_EDIT : Page
{
    fkminvcom dbo = new fkminvcom();
    // Visual C# .NET requires that you override the OnInit function,
    // adding a new delegate for the Page_Load event.

    override protected void OnInit(System.EventArgs e)
    {
        this.Load += new System.EventHandler(this.Page_Load);
        base.OnInit(e);
        CMB_FKM_CD.SelectedIndexChanged += new System.EventHandler(CMB_FKM_CD_SelectedIndexChanged);
        //CMB_NAV_QTR.SelectedIndexChanged += new System.EventHandler(CMB_NAV_QTR_SelectedIndexChanged);
        //CMB_FKM_CD_QRY.SelectedIndexChanged += new System.EventHandler(CMB_FKM_CD_QRY_SelectedIndexChanged);
        //CMB_NAV_QTR_QRY.SelectedIndexChanged += new System.EventHandler(CMB_NAV_QTR_QRY_SelectedIndexChanged);
        GridView1.SelectedIndexChanged += new System.EventHandler(GridView1_SelectedIndexChanged);
        GridView1.RowDataBound += new System.Web.UI.WebControls.GridViewRowEventHandler(GridView1_RowDataBound);
        BTN_ADD.Click += new System.EventHandler(BTN_ADD_Click);
        BTN_UPDT.Click += new System.EventHandler(BTN_UPDT_Click);
        BTN_CLEAR.Click += new System.EventHandler(BTN_CLEAR_Click);
    }

    public void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            INIT_FKMCD();
            CLEAR();
            CMB_FKM_CD.SelectedIndex = -1;
           
            //' EDITING
            if ((Request.QueryString.Get("REFSRL") != null))
            {
                string REFCODE = Request.QueryString.Get("REFSRL");
                string QTR = Request.QueryString.Get("QTR");
                CMB_FKM_CD.SelectedIndex = CMB_FKM_CD.Items.IndexOf(CMB_FKM_CD.Items.FindByValue(REFCODE));

                string CODE = CMB_FKM_CD.SelectedValue;
                 CMB_FKM_CD_Changed(CODE);

            }
        }

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

    protected void CLEAR()
    {
        CMB_FKM_CD.SelectedIndex = -1;
        TXT_PROJNAME.Text = "";
        TXT_PRTCPDATE.Text = "";
        TXT_LQDDATE.Text = "";
        CMB_CURR.SelectedIndex = -1;
        CMB_STATUS.SelectedIndex = -1;
        TXT_ROI.Text = "";
        TXT_PRINCIPAL.Text = "";
        TXT_EXPENSE.Text = "";
        TXT_TOTCOST.Text = "";
        TXT_INTEREST.Text = "";
        TXT_DVDND.Text = "";
        TXT_CAPGN.Text = "";
        TXT_LQDAMT.Text = "";
        TXT_AMOUNT.Text = "";
        TXT_REMX.Text = "";
        BTN_ADD.Visible = true;
        BTN_UPDT.Visible = false;
        REFINE_QRY_LQD();
    }

    protected void BTN_ADD_Click(object sender, System.EventArgs e)
    {
        //Handles BTN_ADD.Click
        if ((CMB_FKM_CD.SelectedIndex < 0))
        {
            mbox("Please select the refrence !!");
            return;
        }

        string STR_TEMP = "";
        if ((TXT_PROJNAME.Text.Trim().Length > 0))
        {
            STR_TEMP = TXT_PROJNAME.Text.Trim();
        }
        string PROJNAME = STR_TEMP;


        string PRTCPDATE = "null";
        if ((TXT_PRTCPDATE.Text.Trim().Length > 0))
        {
            PRTCPDATE = "'" + TXT_PRTCPDATE.Text.Trim() + "'";
        }

        string LQDDATE = "null";
        if ((TXT_LQDDATE.Text.Trim().Length > 0))
        {
            LQDDATE = "'" + TXT_LQDDATE.Text.Trim() + "'";
        }

        STR_TEMP = "";
        if ((CMB_CURR.Text.Trim().Length > 0))
        {
            STR_TEMP = CMB_CURR.Text;
        }
        string CURR = STR_TEMP;

        string STATUS = CMB_STATUS.Text.Trim();

        STR_TEMP = "0";
        if (TXT_ROI.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_ROI.Text.Trim();
        }
        double ROI = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_PRINCIPAL.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_PRINCIPAL.Text.Trim();
        }
        double PRINCIPAL = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_EXPENSE.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_EXPENSE.Text.Trim();
        }
        double EXPENSE = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_TOTCOST.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_TOTCOST.Text.Trim();
        }
        double TOTCOST = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_INTEREST.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_INTEREST.Text.Trim();
        }
        double INTEREST = double.Parse(STR_TEMP);


        STR_TEMP = "0";
        if (TXT_DVDND.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_DVDND.Text.Trim();
        }
        double DVDND = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_CAPGN.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_CAPGN.Text.Trim();
        }
        double CAPGN = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_LQDAMT.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_LQDAMT.Text.Trim();
        }
        double LQDAMT = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_AMOUNT.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_AMOUNT.Text.Trim();
        }
        double AMOUNT = double.Parse(STR_TEMP);



        string USR = User.Identity.Name.ToUpper();
        string UPDDT = System.DateTime.Today.ToString("dd/MM/yyyy");
        string UPDTIME = System.DateTime.Now.ToString("hh:mm:ss");
        string CODE = CMB_FKM_CD.SelectedValue;

        try
        {
            string COMP_CD = dbo.GET_COMP_CD(CODE);
            string RETRVQRY = "SELECT *  FROM INVEST_INFO WHERE FKM_SRL = '" + CODE + "' AND FKM_HD1 IS NULL "; // ' AND COMP_CD = '" + COMP_CD + "' "


            if (dbo.RecExist(RETRVQRY) == true)
            {
                //'RETRVQRY = " INSERT INTO LQD_INFO  " +
                //'           " SELECT COMP_CD,FKM_SRL,FKM_ISSDATE,FKM_CURR,FKM_PROJNAME,FKM_PRTCPDATE,NULL,FKM_STATUS,FKM_ROI, " +
                //'           "        0,0,0,0,0,0,0,0,NULL,NULL,NULL,NULL " +
                //'           " FROM INVEST_INFO WHERE FKM_SRL = '" + CODE + "' AND COMP_CD = '" + COMP_CD + "' "

                //'if(dbo.Transact(RETRVQRY, "", "", "", "") = false ){
                //'    mbox("ERROR !!")
                //'Else
                string INSQRY = " INSERT INTO LQD_INFO VALUES ('" + COMP_CD + "' ,'" + CODE + "','" + System.DateTime.Today.ToString("dd/MM/yyyy") + "', '" + CURR + "', '" + PROJNAME +
                                       "', " + PRTCPDATE + ", " + LQDDATE + ", '" + STATUS + "', '" + ROI +
                                       "', '" + PRINCIPAL + "', '" + EXPENSE + "', '" + TOTCOST + "', '" + INTEREST +
                                       "', '" + DVDND + "', '" + CAPGN + "', '" + LQDAMT + "', '" + AMOUNT +
                                       "' , '" + TXT_REMX.Text + "','" + USR + "', '" + UPDDT + "', '" + UPDTIME + "') ";
                //'Dim INSQRY As string = "UPDATE LQD_INFO SET FKM_ISSDATE = '" + System.DateTime.Today.ToString("dd/MM/yyyy") + "',FKM_CURR ='" + CURR + "',FKM_PROJNAME ='" + PROJNAME +
                //'                       "',FKM_PRTCPDATE = " + PRTCPDATE + ",FKM_LQDDATE = " + LQDDATE + ",FKM_STATUS = '" + STATUS + "',FKM_ROI = '" + ROI +
                //'                       "',FKM_PRINCIPAL = '" + PRINCIPAL + "',FKM_EXPENSE = '" + EXPENSE + "',FKM_TOTCOST = '" + TOTCOST +
                //'                       "',FKM_INTEREST = '" + INTEREST + "',FKM_DVDND = '" + DVDND + "',FKM_CAPGN = '" + CAPGN +
                //'                       "',FKM_LQDAMT = '" + LQDAMT + "',FKM_AMOUNT = '" + AMOUNT + "' ,FKM_REMX  = '" + TXT_REMX.Text +
                //'                       "',FKM_UPDBY = '" + USR + "',FKM_UPDDATE = '" + UPDDT + "',FKM_UPDTIME = '" + UPDTIME + "' " +
                //'                       " WHERE  FKM_SRL = '" + CODE + "'  AND COMP_CD = '" + COMP_CD + "' "

                if (dbo.Transact(INSQRY, "", "", "", "") == false)
                {
                    mbox("ERROR !!");
                }
                else
                {
                    //'if(dbo.sendmail(Session("USER_MAIL_ID"), PROJNAME + " LIQUIDATED DETAILS ADDED #", "Test") = true ){
                    //'    ' StatusLabel.Text = "Upload status: mail sent!"
                    //'Else
                    //'    ' StatusLabel.Text = "Upload status: The mail could not be sent.!"
                    //'End If
                    BTN_CLEAR_Click(sender, e);
                    CMB_FKM_CD.SelectedIndex = -1;
                    mbox("FKM SRL :  '" + CODE + "'   ADDED SUCCESSFULLY !!");
                }
            }
            //'Else
            //'mbox("FKM SRL :  '" + CODE + "'  ALREADY EXIST !!")
            //'End If
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }

    }

    protected void BTN_UPDT_Click(object sender, System.EventArgs e)
    {
        //Handles BTN_UPDT.Click

        if ((CMB_FKM_CD.SelectedIndex < 0))
        {
            mbox("Please select the refrence !!");
            return;
        }

        string STR_TEMP = "";
        if ((TXT_PROJNAME.Text.Trim().Length > 0))
        {
            STR_TEMP = TXT_PROJNAME.Text.Trim();
        }
        string PROJNAME = STR_TEMP;


        string PRTCPDATE = "null";
        if ((TXT_PRTCPDATE.Text.Trim().Length > 0))
        {
            PRTCPDATE = "'" + TXT_PRTCPDATE.Text.Trim() + "'";
        }

        string LQDDATE = "null";
        if ((TXT_LQDDATE.Text.Trim().Length > 0))
        {
            LQDDATE = "'" + TXT_LQDDATE.Text.Trim() + "'";
        }

        STR_TEMP = "";
        if ((CMB_CURR.Text.Trim().Length > 0))
        {
            STR_TEMP = CMB_CURR.Text;
        }
        string CURR = STR_TEMP;

        string STATUS = CMB_STATUS.Text.Trim();

        STR_TEMP = "0";
        if (TXT_ROI.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_ROI.Text.Trim();
        }
        double ROI = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_PRINCIPAL.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_PRINCIPAL.Text.Trim();
        }
        double PRINCIPAL = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_EXPENSE.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_EXPENSE.Text.Trim();
        }
        double EXPENSE = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_TOTCOST.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_TOTCOST.Text.Trim();
        }
        double TOTCOST = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_INTEREST.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_INTEREST.Text.Trim();
        }
        double INTEREST = double.Parse(STR_TEMP);


        STR_TEMP = "0";
        if (TXT_DVDND.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_DVDND.Text.Trim();
        }
        double DVDND = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_CAPGN.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_CAPGN.Text.Trim();
        }
        double CAPGN = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_LQDAMT.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_LQDAMT.Text.Trim();
        }
        double LQDAMT = double.Parse(STR_TEMP);

        STR_TEMP = "0";
        if (TXT_AMOUNT.Text.Trim().Length > 0)
        {
            STR_TEMP = TXT_AMOUNT.Text.Trim();
        }
        double AMOUNT = double.Parse(STR_TEMP);



        string USR = User.Identity.Name.ToUpper();
        string UPDDT = System.DateTime.Today.ToString("dd/MM/yyyy");
        string UPDTIME = System.DateTime.Now.ToString("hh:mm:ss");
        string CODE = CMB_FKM_CD.SelectedValue;

        try
        {
            //'   Dim COMP_CD As string = dbo.GET_COMP_CD(CODE)
            string RETRVQRY = "SELECT *  FROM LQD_INFO WHERE FKM_SRL = '" + CODE + "'  ";//'AND COMP_CD = '" + COMP_CD + "'  "

            if (dbo.RecExist(RETRVQRY) == true)
            {

                string INSQRY = "UPDATE LQD_INFO SET FKM_ISSDATE = '" + System.DateTime.Today.ToString("dd/MM/yyyy") + "',FKM_CURR ='" + CURR + "',FKM_PROJNAME ='" + PROJNAME +
                                       "',FKM_PRTCPDATE = " + PRTCPDATE + ",FKM_LQDDATE = " + LQDDATE + ",FKM_STATUS = '" + STATUS + "',FKM_ROI = '" + ROI +
                                       "',FKM_PRINCIPAL = '" + PRINCIPAL + "',FKM_EXPENSE = '" + EXPENSE + "',FKM_TOTCOST = '" + TOTCOST +
                                       "',FKM_INTEREST = '" + INTEREST + "',FKM_DVDND = '" + DVDND + "',FKM_CAPGN = '" + CAPGN +
                                       "',FKM_LQDAMT = '" + LQDAMT + "',FKM_AMOUNT = '" + AMOUNT + "' ,FKM_REMX  = '" + TXT_REMX.Text +
                                       "',FKM_UPDBY = '" + USR + "',FKM_UPDDATE = '" + UPDDT + "',FKM_UPDTIME = '" + UPDTIME + "' " +
                                       " WHERE  FKM_SRL = '" + CODE + "' ";// ' AND COMP_CD = '" + COMP_CD + "' "

                if (dbo.Transact(INSQRY, "", "", "", "") == false)
                {
                    mbox("ERROR !!");
                }
                else
                {
                    if (dbo.sendmail((string)Session["USER_MAIL_ID"], PROJNAME + " LIQUIDATED DETAILS UPDATED #", "Test") == true)
                    {
                        // ' StatusLabel.Text = "Upload status: mail sent!"
                    }
                    else
                    {
                        //  ' StatusLabel.Text = "Upload status: The mail could not be sent.!"
                    }
                    BTN_CLEAR_Click(sender, e);
                    CMB_FKM_CD.SelectedIndex = -1;
                    mbox("FKM SRL :  '" + CODE + "'   ADDED SUCCESSFULLY !!");
                }
            }
            else
            {
                mbox("FKM SRL :  '" + CODE + "' DOESNOT  EXIST FOR '" + PROJNAME + "'  !!");
            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }

    }

    protected void BTN_CLEAR_Click(object sender, System.EventArgs e)
    {//Handles BTN_CLEAR.Click
        CLEAR();
    }

    protected void CMB_FKM_CD_SelectedIndexChanged(object sender, System.EventArgs e)
    {//Handles CMB_FKM_CD.SelectedIndexChanged
        if (CMB_FKM_CD.SelectedIndex > -1)
        {
            string CODE = CMB_FKM_CD.SelectedValue;
            CMB_FKM_CD_Changed(CODE);
        }

    }

    protected void CMB_FKM_CD_Changed(string CODE)
    { 
        CLEAR();
        try
        {
            // 'Dim COMP_CD As string = dbo.GET_COMP_CD(CODE)
            string RETRVQRY = "SELECT * FROM LQD_INFO WHERE FKM_SRL = '" + CODE + "'";// ' AND  COMP_CD = '" + COMP_CD + "' "
            DataTable dtinfo = dbo.SelTable(RETRVQRY);
            if (dtinfo.Rows.Count == 1)
            {

                CMB_FKM_CD.SelectedIndex = CMB_FKM_CD.Items.IndexOf(CMB_FKM_CD.Items.FindByValue(CODE));
                TXT_PROJNAME.Text = dtinfo.Rows[0]["FKM_PROJNAME"].ToString().Trim();
                TXT_PRTCPDATE.Text = dtinfo.Rows[0]["FKM_PRTCPDATE"].ToString().Trim();
                TXT_LQDDATE.Text = dtinfo.Rows[0]["FKM_LQDDATE"].ToString().Trim();
                CMB_CURR.SelectedIndex = CMB_CURR.Items.IndexOf(CMB_CURR.Items.FindByValue(dtinfo.Rows[0]["FKM_CURR"].ToString().Trim()));
                CMB_STATUS.SelectedIndex = CMB_STATUS.Items.IndexOf(CMB_STATUS.Items.FindByValue(dtinfo.Rows[0]["FKM_STATUS"].ToString().Trim()));

                TXT_ROI.Text = dtinfo.Rows[0]["FKM_ROI"].ToString().Trim();// 'string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_COMMCAP"))
                TXT_PRINCIPAL.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_PRINCIPAL"]);
                TXT_EXPENSE.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_EXPENSE"]);
                TXT_TOTCOST.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_TOTCOST"]);
                TXT_INTEREST.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_INTEREST"]);
                TXT_DVDND.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_DVDND"]);
                TXT_CAPGN.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_CAPGN"]);
                TXT_LQDAMT.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_LQDAMT"]);// ' dtinfo.Rows[0]["FKM_ROI"].ToString().Trim()
                TXT_AMOUNT.Text = string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_AMOUNT"]);
                TXT_REMX.Text = dtinfo.Rows[0]["FKM_REMX"].ToString().Trim();

                BTN_ADD.Visible = false;
                BTN_UPDT.Visible = true;
            }
            else
            {
                RETRVQRY = "SELECT * FROM INVEST_INFO WHERE FKM_SRL = '" + CODE + "' ";// 'AND COMP_CD = '" + COMP_CD + "' "
                dtinfo = dbo.SelTable(RETRVQRY);
                if (dtinfo.Rows.Count == 1)
                {
                    CMB_FKM_CD.SelectedIndex = CMB_FKM_CD.Items.IndexOf(CMB_FKM_CD.Items.FindByValue(CODE));
                    TXT_PROJNAME.Text = dtinfo.Rows[0]["FKM_PROJNAME"].ToString().Trim();
                    TXT_PRTCPDATE.Text = dtinfo.Rows[0]["FKM_PRTCPDATE"].ToString().Trim();
                    TXT_LQDDATE.Text = System.DateTime.Today.ToString("dd/MM/yyyy"); // 'dtinfo.Rows[0]["FKM_LQDDATE"].ToString().Trim()
                    CMB_CURR.SelectedIndex = CMB_CURR.Items.IndexOf(CMB_CURR.Items.FindByValue(dtinfo.Rows[0]["FKM_CURR"].ToString().Trim()));
                    CMB_STATUS.SelectedIndex = CMB_STATUS.Items.IndexOf(CMB_STATUS.Items.FindByValue(dtinfo.Rows[0]["FKM_STATUS"].ToString().Trim()));

                    TXT_ROI.Text = dtinfo.Rows[0]["FKM_ROI"].ToString().Trim();// 'string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_COMMCAP"))
                    TXT_PRINCIPAL.Text = "0";// 'string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_PRINCIPAL"))
                    TXT_EXPENSE.Text = "0"; //'string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_EXPENSE"))
                    TXT_TOTCOST.Text = "0";// 'string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_TOTCOST"))
                    TXT_INTEREST.Text = "0";// 'string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_INTEREST"))
                    TXT_DVDND.Text = "0";//'string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_DVDND"))
                    TXT_CAPGN.Text = "0"; //'string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_CAPGN"))
                    TXT_LQDAMT.Text = "0";///'string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_LQDAMT"))  
                    TXT_AMOUNT.Text = "0"; //'string.Format("{0:#,##0}", dtinfo.Rows[0]["FKM_AMOUNT"))
                    TXT_REMX.Text = "";//' dtinfo.Rows[0]["FKM_REMX"].ToString().Trim()

                    BTN_ADD.Visible = true;
                    BTN_UPDT.Visible = false;
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

    //protected void GET_GRID_INFO()
    //{   try{
    //        string RETRVQRY  = "SELECT * FROM LQD_INFO  ";

    //        DataTable dtinfo    = dbo.SelTable(RETRVQRY);
    //        if(dtinfo.Rows.Count > 0 ){
    //            dtinfo.Columns.Add("FKM_REG");
    //            foreach(DataRow DR in dtinfo.Rows){

    //                DR["FKM_REG"] = (string)dbo.GET_FRM_COMP_CD(DR["COMP_CD"].ToString(), 1);

    //        }

    //            GridView1.DataSource = dtinfo;
    //            GridView1.DataBind();
    //        }else{

    //            GridView1.DataSource = null;
    //            GridView1.DataBind();
    //       }
    //        //    ' Session("BANKMTNC_dtINFO_REPORT") = dtinfo
        
    //    }
    //    catch (Exception EX)
    //    {
    //        mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
    //    }
    //}
    public void REFINE_QRY_LQD()
    {
        Session["LQD_dt_LQDLIST_REPORT"] = null;
        GridView1.DataSource = null;
        GridView1.DataBind();
        //string CODE, string CURR, string YYYY
        string RETRVQRY = "SELECT * FROM LQD_INFO ORDER BY SUBstring(FKM_LQDDATE, 7, 4)  DESC ";
        //string RETRVQRY = "SELECT * FROM LQD_INFO WHERE FKM_SRL LIKE '" + CODE + "' AND FKM_CURR LIKE '" + CURR +
        //    "' AND COMP_CD LIKE '" + COMP_CD + "' AND SUBstring(FKM_LQDDATE, 7, 4) LIKE '" + YYYY + "' ORDER BY FKM_SRL   ";

        DataTable DTINVINFO = dbo.SelTable(RETRVQRY);

        if (DTINVINFO.Rows.Count > 0)
        {

            DTINVINFO.Columns.Add("FKM_ROI_R", typeof(string));
            DTINVINFO.Columns.Add("FKM_PRINCIPAL_R", typeof(string));
            DTINVINFO.Columns.Add("FKM_EXPENSE_R", typeof(string));
            DTINVINFO.Columns.Add("FKM_TOTCOST_R", typeof(string));
            DTINVINFO.Columns.Add("FKM_INTEREST_R", typeof(string));
            DTINVINFO.Columns.Add("FKM_DVDND_R", typeof(string));
            DTINVINFO.Columns.Add("FKM_CAPGN_R", typeof(string));
            DTINVINFO.Columns.Add("FKM_LQDAMT_R", typeof(string));
            DTINVINFO.Columns.Add("FKM_AMOUNT_R", typeof(string));
            DTINVINFO.Columns.Add("FKM_CURR_SMBL", typeof(string));
            DTINVINFO.Columns.Add("FOOTER", typeof(string));
            DTINVINFO.Columns.Add("FKM_REG", typeof(string));
            DTINVINFO.Columns.Add("LOGO", typeof(byte[]));



            DataTable dt_LQDLIST_info = DTINVINFO.Clone();
            dt_LQDLIST_info.Rows.Clear();

            REFINE_ROW_LQD_DETAILS(ref DTINVINFO, ref dt_LQDLIST_info, "US DOLLAR", "$");
            REFINE_ROW_LQD_DETAILS(ref DTINVINFO, ref dt_LQDLIST_info, "UK POUND", "£");
            REFINE_ROW_LQD_DETAILS(ref DTINVINFO, ref dt_LQDLIST_info, "EURO", "€");


            Session["LQD_dt_LQDLIST_REPORT"] = null;
            if (dt_LQDLIST_info.Rows.Count > 0)
            {
                Session["LQD_dt_LQDLIST_REPORT"] = dt_LQDLIST_info;
                
                GridView1.DataSource = dt_LQDLIST_info;
                GridView1.DataBind();
            }
        }

    }
    public void REFINE_ROW_LQD_DETAILS(ref DataTable DTINVINFO, ref DataTable dt_invest_info, string CURR, string CURRSYMB)
    {
        DataRow[] DR_USD = DTINVINFO.Select("FKM_CURR  = '" + CURR + "' ");
        DataRow DR_TOT_USD = dt_invest_info.NewRow();
        if (DR_USD.Length > 0)
        {


            DR_TOT_USD["FKM_CURR"] = CURR;
            DR_TOT_USD["FKM_PROJNAME"] = "TOTAL PROJECTS: " + DR_USD.Length;
            DR_TOT_USD["FKM_LQDDATE"] = "(" + CURR + ")";

            DR_TOT_USD["FKM_ROI"] = decimal.Parse("0");
            DR_TOT_USD["FKM_PRINCIPAL"] = decimal.Parse("0");
            DR_TOT_USD["FKM_EXPENSE"] = decimal.Parse("0");
            DR_TOT_USD["FKM_TOTCOST"] = decimal.Parse("0");
            DR_TOT_USD["FKM_INTEREST"] = decimal.Parse("0");
            DR_TOT_USD["FKM_DVDND"] = decimal.Parse("0");
            DR_TOT_USD["FKM_CAPGN"] = decimal.Parse("0");
            DR_TOT_USD["FKM_LQDAMT"] = decimal.Parse("0");
            DR_TOT_USD["FKM_AMOUNT"] = decimal.Parse("0");

            for (int I = 0; I <= DR_USD.Length - 1; I++)
            {
                DR_TOT_USD["COMP_CD"] = DR_USD[I]["COMP_CD"];
                DR_TOT_USD["FKM_PRINCIPAL"] = decimal.Parse(DR_TOT_USD["FKM_PRINCIPAL"].ToString()) + decimal.Parse(DR_USD[I]["FKM_PRINCIPAL"].ToString());
                DR_TOT_USD["FKM_EXPENSE"] = decimal.Parse(DR_TOT_USD["FKM_EXPENSE"].ToString()) + decimal.Parse(DR_USD[I]["FKM_EXPENSE"].ToString());
                DR_TOT_USD["FKM_TOTCOST"] = decimal.Parse(DR_TOT_USD["FKM_TOTCOST"].ToString()) + decimal.Parse(DR_USD[I]["FKM_TOTCOST"].ToString());
                DR_TOT_USD["FKM_INTEREST"] = decimal.Parse(DR_TOT_USD["FKM_INTEREST"].ToString()) + decimal.Parse(DR_USD[I]["FKM_INTEREST"].ToString());
                DR_TOT_USD["FKM_DVDND"] = decimal.Parse(DR_TOT_USD["FKM_DVDND"].ToString()) + decimal.Parse(DR_USD[I]["FKM_DVDND"].ToString());
                DR_TOT_USD["FKM_CAPGN"] = decimal.Parse(DR_TOT_USD["FKM_CAPGN"].ToString()) + decimal.Parse(DR_USD[I]["FKM_CAPGN"].ToString());
                DR_TOT_USD["FKM_LQDAMT"] = decimal.Parse(DR_TOT_USD["FKM_LQDAMT"].ToString()) + decimal.Parse(DR_USD[I]["FKM_LQDAMT"].ToString());
                DR_TOT_USD["FKM_AMOUNT"] = decimal.Parse(DR_TOT_USD["FKM_AMOUNT"].ToString()) + decimal.Parse(DR_USD[I]["FKM_AMOUNT"].ToString());

                if (decimal.Parse(DR_USD[I]["FKM_ROI"].ToString()) == 0)
                {
                    DR_USD[I]["FKM_ROI"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_ROI_R"] = string.Format("{0:#0.00}", DR_USD[I]["FKM_ROI"].ToString()) + "%";
                }

                if (decimal.Parse(DR_USD[I]["FKM_PRINCIPAL"].ToString()) == 0)
                {
                    DR_USD[I]["FKM_PRINCIPAL"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_PRINCIPAL_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_PRINCIPAL"]);
                }

                if (decimal.Parse(DR_USD[I]["FKM_EXPENSE"].ToString()) == 0)
                {
                    DR_USD[I]["FKM_EXPENSE"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_EXPENSE_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_EXPENSE"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_TOTCOST"].ToString()) == 0)
                {
                    DR_USD[I]["FKM_TOTCOST"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_TOTCOST_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_TOTCOST"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_INTEREST"].ToString()) == 0)
                {
                    DR_USD[I]["FKM_INTEREST"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_INTEREST_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_INTEREST"]);
                }

                if (decimal.Parse(DR_USD[I]["FKM_DVDND"].ToString()) == 0)
                {
                    DR_USD[I]["FKM_DVDND"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_DVDND_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_DVDND"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_CAPGN"].ToString()) == 0)
                {
                    DR_USD[I]["FKM_CAPGN"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_CAPGN_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_CAPGN"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_LQDAMT"].ToString()) == 0)
                {
                    DR_USD[I]["FKM_LQDAMT"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_LQDAMT_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_LQDAMT"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_AMOUNT"].ToString()) == 0)
                {
                    DR_USD[I]["FKM_AMOUNT"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_AMOUNT_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_AMOUNT"]);
                }

                //'if DR_USD[I]["FKM_PRVTEQT").ToString.Trim = "Y" ){
                //'    DR_USD[I]["FKM_PRVTEQT") = "PRIVATE EQUITY"
                //'  } else {if DR_USD[I]["FKM_PRVTEQT").ToString.Trim = "N" ){
                //'    DR_USD[I]["FKM_PRVTEQT") = "NON PRIVATE EQUITY"
                //'End if

                //'if DR_USD[I]["FKM_YEILDPRD").ToString.Trim = "M" ){
                //'    DR_USD[I]["FKM_YEILDPRD") = "MONTHLY"
                //'  } else {if DR_USD[I]["FKM_YEILDPRD").ToString.Trim = "Q" ){
                //'    DR_USD[I]["FKM_YEILDPRD") = "QUARTERLY"
                //'  } else {if DR_USD[I]["FKM_YEILDPRD").ToString.Trim = "S" ){
                //'    DR_USD[I]["FKM_YEILDPRD") = "SEMI ANNUAL"
                //'End if


                if ((DR_USD[I]["COMP_CD"].ToString()) != null)
                {
                    //'if DR_USD[I]["COMP_CD") = "001" ){
                    //'    DR_USD[I]["FKM_REG") = "FKMINVEST"
                    //'  } else {if DR_USD[I]["COMP_CD") = "002" ){
                    //'    DR_USD[I]["FKM_REG") = "FENKINVEST"
                    //'  } else {if DR_USD[I]["COMP_CD") = "003" ){
                    //'    DR_USD[I]["FKM_REG") = "PERSONAL"
                    //'End if
                    DR_USD[I]["FKM_REG"] = (string)dbo.GET_FRM_COMP_CD(DR_USD[I]["COMP_CD"].ToString(), 1);
                    DR_USD[I]["LOGO"] = dbo.ReadFile(Server.MapPath((string)dbo.GET_FRM_COMP_CD(DR_USD[I]["COMP_CD"].ToString(), 2)));
                }


                DR_USD[I]["FOOTER"] = User.Identity.Name.ToUpper();

                dt_invest_info.ImportRow(DR_USD[I]);
            }

            if (decimal.Parse(DR_TOT_USD["FKM_ROI"].ToString()) == 0)
            {
                DR_TOT_USD["FKM_ROI"] = System.DBNull.Value;
            }
            else
            {
                DR_TOT_USD["FKM_ROI_R"] = string.Format("{0:#0.00}", DR_TOT_USD["FKM_ROI"].ToString()) + "%";
            }

            if (decimal.Parse(DR_TOT_USD["FKM_PRINCIPAL"].ToString()) == 0)
            {
                DR_TOT_USD["FKM_PRINCIPAL"] = System.DBNull.Value;
            }
            else
            {
                DR_TOT_USD["FKM_PRINCIPAL_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_PRINCIPAL"]);
            }

            if (decimal.Parse(DR_TOT_USD["FKM_EXPENSE"].ToString()) == 0)
            {
                DR_TOT_USD["FKM_EXPENSE"] = System.DBNull.Value;
            }
            else
            {
                DR_TOT_USD["FKM_EXPENSE_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_EXPENSE"]);
            }
            if (decimal.Parse(DR_TOT_USD["FKM_TOTCOST"].ToString()) == 0)
            {
                DR_TOT_USD["FKM_TOTCOST"] = System.DBNull.Value;
            }
            else
            {
                DR_TOT_USD["FKM_TOTCOST_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_TOTCOST"]);
            }
            if (decimal.Parse(DR_TOT_USD["FKM_INTEREST"].ToString()) == 0)
            {
                DR_TOT_USD["FKM_INTEREST"] = System.DBNull.Value;
            }
            else
            {
                DR_TOT_USD["FKM_INTEREST_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_INTEREST"]);
            }

            if (decimal.Parse(DR_TOT_USD["FKM_DVDND"].ToString()) == 0)
            {
                DR_TOT_USD["FKM_DVDND"] = System.DBNull.Value;
            }
            else
            {
                DR_TOT_USD["FKM_DVDND_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_DVDND"]);
            }
            if (decimal.Parse(DR_TOT_USD["FKM_CAPGN"].ToString()) == 0)
            {
                DR_TOT_USD["FKM_CAPGN"] = System.DBNull.Value;
            }
            else
            {
                DR_TOT_USD["FKM_CAPGN_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_CAPGN"]);
            }
            if (decimal.Parse(DR_TOT_USD["FKM_LQDAMT"].ToString()) == 0)
            {
                DR_TOT_USD["FKM_LQDAMT"] = System.DBNull.Value;
            }
            else
            {
                DR_TOT_USD["FKM_LQDAMT_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_LQDAMT"]);
            }
            if (decimal.Parse(DR_TOT_USD["FKM_AMOUNT"].ToString()) == 0)
            {
                DR_TOT_USD["FKM_AMOUNT"] = System.DBNull.Value;
            }
            else
            {
                DR_TOT_USD["FKM_AMOUNT_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_TOT_USD["FKM_AMOUNT"]);
            }

            DR_TOT_USD["FOOTER"] = User.Identity.Name.ToUpper();

            dt_invest_info.Rows.Add(DR_TOT_USD);
        }
    }

    public void GridView1_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        //Protected Sub GridView3_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView3.RowDataBound
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
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
    protected void GridView1_SelectedIndexChanged(object sender, System.EventArgs e) //Handles GridView1.SelectedIndexChanged
    {

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
        dbo.ErrorLog(USR + " : LQD_DTL_EDIT.aspx : " + val, Server.MapPath("~\\Logs\\ErrorLog"));
    }
}
