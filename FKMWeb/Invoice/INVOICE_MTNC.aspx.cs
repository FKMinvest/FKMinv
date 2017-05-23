using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class Invoice_INVOICE_MTNC : Page
{
    fkminvcom dbo = new fkminvcom();
    // Visual C# .NET requires that you override the OnInit function,
    // adding a new delegate for the Page_Load event.

    override protected void OnInit(System.EventArgs e)
    {
        this.Load += new System.EventHandler(this.Page_Load);
        base.OnInit(e);
        CMB_INVC_REFSRL.SelectedIndexChanged += new System.EventHandler(CMB_INVC_REFSRL_SelectedIndexChanged);
        CMB_FKM_CD.SelectedIndexChanged += new System.EventHandler(CMB_FKM_CD_SelectedIndexChanged);
        CMB_INVC_CREDIT_NAME.SelectedIndexChanged += new System.EventHandler(CMB_INVC_CREDIT_NAME_SelectedIndexChanged);
        CMB_INVC_DEBIT_NAME.SelectedIndexChanged += new System.EventHandler(CMB_INVC_DEBIT_NAME_SelectedIndexChanged);
        GridView1.SelectedIndexChanged += new System.EventHandler(GridView1_SelectedIndexChanged);
        // GridView1.RowDeleting += new System.Web.UI.WebControls.GridViewDeleteEventHandler(Gridview1_RowDeleting);
        //GridView1.RowDataBound += new System.Web.UI.WebControls.GridViewRowEventHandler(GridView1_RowDataBound); 
        CHBX_EDIT.CheckedChanged += new System.EventHandler(CHBX_EDIT_CheckedChanged);
        //Button1.Click += new System.EventHandler(Button1_Click);

        BTN_ADD.Click += new System.EventHandler(BTN_ADD_Click);
        BTN_UPDT.Click += new System.EventHandler(BTN_UPDT_Click);
        //' BTN_DELETE.Click += new System.EventHandler(BTN_DELETE_Click);
        BTN_CLEAR.Click += new System.EventHandler(BTN_CLEAR_Click);
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {

        if (!IsPostBack && !IsCallback)
        {
            if ((Request.QueryString.Get("INCCD")) != null)
            {
                String INCCODE = Request.QueryString.Get("INCCD");
                CMB_INVCTYP.SelectedIndex = CMB_INVCTYP.Items.IndexOf(CMB_INVCTYP.Items.FindByValue(INCCODE));
            }

            CLEAR_FIELDS( );
            INIT_FKMCD();
            INIT_INVC_SRL();
            INIT_BANK_NAME();


            if ((Request.QueryString.Get("REFSRL")) != null)
            {
                String REFCODE = Request.QueryString.Get("REFSRL");
                GET_REFSRL_DETAILS(ref REFCODE);
            }

            LBL_CAPTION.Text = CMB_INVCTYP.SelectedItem.Text;

        }
    }

    protected void INIT_FKMCD()
    {
        //' Dim COMP_CD As string = Session("USER_COMP_CD")
        string RETRVQRY = "SELECT DISTINCT FKM_SRL ,FKM_SRL + '-' + FKM_PROJNAME AS FKM_PROJNAME  FROM INVEST_INFO  ORDER BY FKM_SRL";
        DataTable dtinfo = dbo.SelTable(RETRVQRY);
        CMB_FKM_CD.DataSource = dtinfo;
        CMB_FKM_CD.DataBind();
        CMB_FKM_CD.SelectedIndex = -1;

    }


    protected void INIT_INVC_SRL()
    {
        String CODE = CMB_INVCTYP.SelectedValue;
        //'Dim COMP_CD As String = Session("USER_COMP_CD")
        String RETRVQRY = "SELECT DISTINCT INVC_REFSRL  FROM INVOICE_INFO  WHERE INVC_TYPE  LIKE '" + CODE + "%'  ORDER BY INVC_REFSRL";
        //'BTRX_TYPE IN ('B','I','H','P') 
        DataTable dtinfo = dbo.SelTable(RETRVQRY);
        CMB_INVC_REFSRL.DataSource = dtinfo;
        CMB_INVC_REFSRL.DataBind();
        CMB_INVC_REFSRL.SelectedIndex = -1;

    }

    protected void INIT_BANK_NAME()
    {
        //'Dim CODE As String = CMB_BTRXTYP.SelectedValue.Substring(0, 1)
        //'Dim COMP_CD As String = Session("USER_COMP_CD")
        String RETRVQRY = "SELECT DISTINCT BNK_REFSRL, BNK_NAME  + ' | ' + BNK_ACCOUNT_NO as BNK_NAME  FROM BANK_INFO  WHERE BNK_TYPE IN ('B','I')  ORDER BY BNK_REFSRL";
        DataTable dtinfo = dbo.SelTable(RETRVQRY);

        //'dtinfo.Rows.Add("%", "ALL PROJECTS")

        CMB_INVC_DEBIT_NAME.DataSource = dtinfo;
        CMB_INVC_DEBIT_NAME.DataBind();
        CMB_INVC_DEBIT_NAME.SelectedIndex = CMB_FKM_CD.Items.Count - 1;

        CMB_INVC_CREDIT_NAME.DataSource = dtinfo;
        CMB_INVC_CREDIT_NAME.DataBind();
        CMB_INVC_CREDIT_NAME.SelectedIndex = CMB_FKM_CD.Items.Count - 1;

    }

    protected void CLEAR_FIELDS()
    {
        //   ' CMB_DOCTYP.SelectedIndex = -1

        if (CHBX_EDIT.Checked == true)
        {

            CMB_FKM_CD.SelectedIndex = -1;
            TXT_INVC_REFDESC.Text = "";
            TXT_INVC_ISSDT.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
            CMB_INVC_DEBIT_NAME.SelectedIndex = -1;
            TXT_INVC_DEBIT_ACC.Text = "";
            CMB_INVC_CREDIT_NAME.SelectedIndex = -1;
            TXT_INVC_CREDIT_ACC.Text = "";
            TXT_INVC_VALUEDT.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
            TXT_INVC_VALUEAMT.Text = "";
            TXT_INVC_VALUEAMT_OLD.Text = "";
            TXT_INVC_NARR.Text = "";
            CMB_INVC_CURRCD.SelectedIndex = 0;
            CMB_INVC_STATUS.SelectedIndex = 0;

            TXT_INVC_REFSRL.Text = NEXT_REC_SRL();
            TXT_INVC_REFSRL.Visible = false;
            BTN_ADD.Visible = false;
            CMB_FKM_CD.Enabled = false;
            CMB_INVC_REFSRL.SelectedIndex = -1;
            CMB_INVC_REFSRL.Visible = true;
            BTN_UPDT.Visible = true;
        }
        else
        {
            CMB_FKM_CD.SelectedIndex = -1;
            TXT_INVC_REFDESC.Text = "";
            TXT_INVC_ISSDT.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
            CMB_INVC_DEBIT_NAME.SelectedIndex = -1;
            TXT_INVC_DEBIT_ACC.Text = "";
            CMB_INVC_CREDIT_NAME.SelectedIndex = -1;
            TXT_INVC_CREDIT_ACC.Text = "";
            TXT_INVC_VALUEDT.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
            TXT_INVC_VALUEAMT.Text = "";
            TXT_INVC_VALUEAMT_OLD.Text = "";
            TXT_INVC_NARR.Text = "";
            CMB_INVC_CURRCD.SelectedIndex = 0;
            CMB_INVC_STATUS.SelectedIndex = 0;

            TXT_INVC_REFSRL.Text = NEXT_REC_SRL();
            TXT_INVC_REFSRL.Visible = true;
            BTN_ADD.Visible = true;
            CMB_FKM_CD.Enabled = true;
            CMB_INVC_REFSRL.SelectedIndex = -1;
            CMB_INVC_REFSRL.Visible = false;
            BTN_UPDT.Visible = false;

        }
        GET_GRID_INFO();
    }

    protected void BTN_CLEAR_Click(object sender, System.EventArgs e)
    { // Handles BTN_CLEAR.Click
        CLEAR_FIELDS();
    }

    protected void CHBX_EDIT_CheckedChanged(object sender, System.EventArgs e)
    { // Handles CHBX_EDIT.CheckedChanged
        CLEAR_FIELDS();
    }

    protected void BTN_ADD_Click(object sender, System.EventArgs e)
    { // Handles BTN_ADD.Click
        if (CMB_FKM_CD.SelectedIndex < 0)
        {
            mbox("PLEASE SELECT THE PROJECT NAME !!");
        }
        String USR = User.Identity.Name.ToUpper();
        String UPDDT = System.DateTime.Today.ToString("dd/MM/yyyy");
        String UPDTIME = System.DateTime.Now.ToString("hh:mm:ss");
        String CODE = TXT_INVC_REFSRL.Text.ToUpper();
        String FKMCODE = CMB_FKM_CD.Text.ToUpper();
        String SRL = NEXT_REC_SRL();
        try
        {
            String COMP_CD = dbo.GET_COMP_CD(FKMCODE);
            String RETRVQRY = "SELECT * FROM INVOICE_INFO WHERE INVC_REFSRL = '" + CODE + "' AND  COMP_CD = '" + COMP_CD + "'";
            if (dbo.RecExist(RETRVQRY) == false)
            {

                Decimal CREDIT_BALAMT = 0;
                Decimal DEBIT_BALAMT = 0;
                String DEBIT_NAME = "";
                String CREDIT_NAME = "";
                String UPD_DEBIT_QRY = "";
                String UPD_CREDIT_QRY = "";

                String STR_TEMP = "0";
                if (TXT_INVC_VALUEAMT.Text.Trim().Length > 0)
                {
                    STR_TEMP = TXT_INVC_VALUEAMT.Text.Trim();
                }
                Decimal INVC_VALUEAMT = decimal.Parse(STR_TEMP);

                STR_TEMP = "0";
                if (TXT_INVC_VALUEAMT_OLD.Text.Trim().Length > 0)
                {
                    STR_TEMP = TXT_INVC_VALUEAMT_OLD.Text.Trim();
                }
                Decimal INVC_VALUEAMT_OLD = decimal.Parse(STR_TEMP);


                if (CMB_INVC_DEBIT_NAME.SelectedIndex > -1)
                {
                    //'DEBIT_NAME = CMB_INVC_DEBIT_NAME.SelectedItem.Text.ToUpper
                    DEBIT_NAME = CMB_INVC_DEBIT_NAME.SelectedValue;
                    DEBIT_BALAMT = GET_BANK_BALANCE(ref DEBIT_NAME);

                    //'DEBIT_BALAMT = DEBIT_BALAMT - INVC_VALUEAMT + INVC_VALUEAMT_OLD

                    //'UPD_DEBIT_QRY = "UPDATE BANK_INFO SET BNK_BALANCE_AMT = '" + DEBIT_BALAMT + "', BNK_LST_DEBIT_DT = '" + UPDDT +
                    //'                "', BNK_UPDBY = '" + USR + "', BNK_UPDDATE = '" + UPDDT + "', BNK_UPDTIME = '" + UPDTIME + "' " +
                    //'                       " WHERE BNK_REFSRL= '" + CMB_INVC_DEBIT_NAME.SelectedValue + "'  "
                }

                if (CMB_INVC_CREDIT_NAME.SelectedIndex > -1)
                {
                    // 'CREDIT_NAME = CMB_INVC_CREDIT_NAME.SelectedItem.Text.ToUpper
                    CREDIT_NAME = CMB_INVC_CREDIT_NAME.SelectedValue;
                    CREDIT_BALAMT = GET_BANK_BALANCE(ref CREDIT_NAME);

                    //'CREDIT_BALAMT = CREDIT_BALAMT + INVC_VALUEAMT - INVC_VALUEAMT_OLD

                    //'UPD_CREDIT_QRY = "UPDATE BANK_INFO SET BNK_BALANCE_AMT = '" + CREDIT_BALAMT + "', BNK_LST_CREDIT_DT = '" + UPDDT +
                    //'                       "', BNK_UPDBY = '" + USR + "', BNK_UPDDATE = '" + UPDDT + "', BNK_UPDTIME = '" + UPDTIME + "' " +
                    //'                       " WHERE BNK_REFSRL= '" + CMB_INVC_CREDIT_NAME.SelectedValue + "'  "
                }

                String INSQRY = "INSERT INTO INVOICE_INFO VALUES('" + COMP_CD + "','" + SRL + "','" + TXT_INVC_ISSDT.Text.ToUpper() + "','" + TXT_INVC_VALUEDT.Text.ToUpper() +
                                               "','" + CMB_FKM_CD.Text.ToUpper() + "','" + CMB_INVCTYP.SelectedValue.ToUpper() +
                                               "','" + TXT_INVC_REFDESC.Text.ToUpper() + "','" + TXT_INVC_NARR.Text.ToUpper() +
                                               "','" + CMB_INVC_CURRCD.SelectedValue.ToUpper() + "','" + INVC_VALUEAMT +
                                               "','" + DEBIT_NAME + "','" + TXT_INVC_DEBIT_ACC.Text.ToUpper() +
                                               "','" + INVC_VALUEAMT + "','" + DEBIT_BALAMT +
                                               "','" + CREDIT_NAME + "','" + TXT_INVC_CREDIT_ACC.Text.ToUpper() +
                                               "','" + INVC_VALUEAMT + "','" + CREDIT_BALAMT +
                                               "','" + CMB_INVC_STATUS.SelectedValue + "' ,'" + USR + "','" + UPDDT + "','" + USR + "','" + UPDDT + "','" + UPDTIME + "')";


                if (dbo.Transact(INSQRY, UPD_DEBIT_QRY, UPD_CREDIT_QRY, "", "") == false)
                {
                    mbox("ERROR !!");
                }
                else
                {
                    // 'GridView1.DataBind()
                    mbox("CODE : '" + SRL + "'      UPLOADED SUCCESSFULLY !!");
                    CLEAR_FIELDS();
                    INIT_INVC_SRL();
                }
            }
            else
            {
                //TXT_SRL.Text = NEXT_REC_SRL()
                mbox("PLEASE TRY AGAIN .. ALREADY FILE EXIST FOR : " + SRL + "!!");

            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }

    }

    protected void BTN_UPDT_Click(object sender, System.EventArgs e)
    { // Handles BTN_UPDT.Click
        String USR = User.Identity.Name.ToUpper();
        String UPDDT = System.DateTime.Today.ToString("dd/MM/yyyy");
        String UPDTIME = System.DateTime.Now.ToString("hh:mm:ss");
        String CODE = TXT_INVC_REFSRL.Text.ToUpper();
        String FKMCODE = CMB_FKM_CD.Text.ToUpper();
        try
        {
            String COMP_CD = dbo.GET_COMP_CD(FKMCODE);
            String RETRVQRY = "SELECT * FROM INVOICE_INFO WHERE INVC_REFSRL = '" + CODE + "' AND  COMP_CD = '" + COMP_CD + "'";

            if (dbo.RecExist(RETRVQRY) == true)
            {


                Decimal CREDIT_BALAMT = 0;
                Decimal DEBIT_BALAMT = 0;
                String DEBIT_NAME = "";
                String CREDIT_NAME = "";
                String UPD_DEBIT_QRY = "";
                String UPD_CREDIT_QRY = "";

                String STR_TEMP = "0";
                if (TXT_INVC_VALUEAMT.Text.Trim().Length > 0)
                {
                    STR_TEMP = TXT_INVC_VALUEAMT.Text.Trim();
                }
                Decimal INVC_VALUEAMT = decimal.Parse(STR_TEMP);

                STR_TEMP = "0";
                if (TXT_INVC_VALUEAMT_OLD.Text.Trim().Length > 0)
                {
                    STR_TEMP = TXT_INVC_VALUEAMT_OLD.Text.Trim();
                }
                Decimal INVC_VALUEAMT_OLD = decimal.Parse(STR_TEMP);


                if (CMB_INVC_DEBIT_NAME.SelectedIndex > -1)
                {
                    //'DEBIT_NAME = CMB_INVC_DEBIT_NAME.SelectedItem.Text.ToUpper
                    DEBIT_NAME = CMB_INVC_DEBIT_NAME.SelectedValue;
                    DEBIT_BALAMT = GET_BANK_BALANCE(ref DEBIT_NAME);

                    //'DEBIT_BALAMT = DEBIT_BALAMT - INVC_VALUEAMT + INVC_VALUEAMT_OLD

                    //'UPD_DEBIT_QRY = "UPDATE BANK_INFO SET BNK_BALANCE_AMT = '" + DEBIT_BALAMT + "', BNK_LST_DEBIT_DT = '" + UPDDT +
                    //'                "', BNK_UPDBY = '" + USR + "', BNK_UPDDATE = '" + UPDDT + "', BNK_UPDTIME = '" + UPDTIME + "' " +
                    //'                       " WHERE BNK_REFSRL= '" + CMB_INVC_DEBIT_NAME.SelectedValue + "'  "
                }

                if (CMB_INVC_CREDIT_NAME.SelectedIndex > -1)
                {
                    // 'CREDIT_NAME = CMB_INVC_CREDIT_NAME.SelectedItem.Text.ToUpper
                    CREDIT_NAME = CMB_INVC_CREDIT_NAME.SelectedValue;
                    CREDIT_BALAMT = GET_BANK_BALANCE(ref CREDIT_NAME);

                    //'CREDIT_BALAMT = CREDIT_BALAMT + INVC_VALUEAMT - INVC_VALUEAMT_OLD

                    //'UPD_CREDIT_QRY = "UPDATE BANK_INFO SET BNK_BALANCE_AMT = '" + CREDIT_BALAMT + "', BNK_LST_CREDIT_DT = '" + UPDDT +
                    //'                       "', BNK_UPDBY = '" + USR + "', BNK_UPDDATE = '" + UPDDT + "', BNK_UPDTIME = '" + UPDTIME + "' " +
                    //'                       " WHERE BNK_REFSRL= '" + CMB_INVC_CREDIT_NAME.SelectedValue + "'  "
                }

                String UPDQRY = "UPDATE INVOICE_INFO SET INVC_ISSDT = '" + TXT_INVC_ISSDT.Text.ToUpper() +
                                       "', INVC_VALUEDT  = '" + TXT_INVC_VALUEDT.Text.ToUpper() + "',INVC_REFDESC = '" + TXT_INVC_REFDESC.Text.ToUpper() +
                                       "', INVC_NARR = '" + TXT_INVC_NARR.Text.ToUpper() + "',INVC_CURRCD = '" + CMB_INVC_CURRCD.SelectedValue.ToUpper() +
                                       "', INVC_VALUEAMT = '" + INVC_VALUEAMT + "', INVC_DEBIT_AMT = '" + INVC_VALUEAMT + "', INVC_CREDIT_AMT = '" + INVC_VALUEAMT +
                                       "', INVC_DEBIT_BAL = '" + DEBIT_BALAMT + "', INVC_CREDIT_BAL = '" + CREDIT_BALAMT +
                                       "', INVC_DEBIT_NAME = '" + DEBIT_NAME + "',INVC_DEBIT_ACC = '" + TXT_INVC_DEBIT_ACC.Text.ToUpper() +
                                       "', INVC_CREDIT_NAME = '" + CREDIT_NAME + "',INVC_CREDIT_ACC = '" + TXT_INVC_CREDIT_ACC.Text.ToUpper() +
                                       "', INVC_STATUS = '" + CMB_INVC_STATUS.SelectedValue + "', INVC_UPDBY = '" + USR + "', INVC_UPDDATE = '" + UPDDT + "', INVC_UPDTIME = '" + UPDTIME + "' " +
                                       " WHERE INVC_REFSRL= '" + CODE + "'  AND  COMP_CD = '" + COMP_CD + "'";



                if (dbo.Transact(UPDQRY, UPD_DEBIT_QRY, UPD_CREDIT_QRY, "", "") == false)
                {
                    mbox("ERROR !!");
                }
                else
                {
                    CLEAR_FIELDS();
                    mbox("CODE : '" + CODE + "'   UPDATED SUCCESSFULLY !!");
                }


            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }

    }

    protected String NEXT_REC_SRL()
    {
        String SRL = "";
        DataTable DTINVINF = new DataTable();
        try
        {
            String RETRVQRY = "SELECT MAX (INVC_REFSRL) FROM INVOICE_INFO  WHERE INVC_REFSRL LIKE '" + System.DateTime.Today.Year + "%' ";
            DTINVINF = dbo.SelTable(RETRVQRY);
            if (DTINVINF.Rows.Count > 0)
            {
                if (DTINVINF.Rows[0][0].ToString().Length != 0)
                {
                    Decimal NSRL = decimal.Parse(DTINVINF.Rows[0][0].ToString().Substring(4));

                    SRL = (NSRL + 1).ToString("0000000000");
                    SRL = System.DateTime.Today.Year + SRL;
                }
                else
                {
                    SRL = System.DateTime.Today.Year + "0000000001";
                }
            }
            else
            {
                SRL = System.DateTime.Today.Year + "0000000001";
            }

        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }
        return SRL;
    }

    protected void GET_REFSRL_DETAILS(ref string CODE)
    {
       
        CHBX_EDIT.Checked = true;

        CMB_FKM_CD.SelectedIndex = -1;
        TXT_INVC_REFSRL.Visible = false;
        BTN_ADD.Visible = false;
        CMB_FKM_CD.Enabled = false;
        CMB_INVC_REFSRL.SelectedIndex = -1;
        CMB_INVC_REFSRL.Visible = true;
        BTN_UPDT.Visible = true;


        try
        {
            String RETRVQRY = "SELECT * FROM INVOICE_INFO WHERE INVC_REFSRL = '" + CODE + "'  ";
            DataTable dtinfo = dbo.SelTable(RETRVQRY);
            if (dtinfo.Rows.Count == 1)
            {


                CMB_INVCTYP.SelectedIndex = CMB_INVCTYP.Items.IndexOf(CMB_INVCTYP.Items.FindByValue(dtinfo.Rows[0]["INVC_TYPE"].ToString().Trim()));
                LBL_CAPTION.Text = CMB_INVCTYP.SelectedItem.Text;

                CMB_INVC_REFSRL.DataBind();
                CMB_FKM_CD.DataBind();
                CMB_INVC_DEBIT_NAME.DataBind();
                CMB_INVC_CREDIT_NAME.DataBind();

                CMB_INVC_REFSRL.SelectedIndex = CMB_INVC_REFSRL.Items.IndexOf(CMB_INVC_REFSRL.Items.FindByValue(CODE));

                CMB_FKM_CD.SelectedIndex = CMB_FKM_CD.Items.IndexOf(CMB_FKM_CD.Items.FindByValue(dtinfo.Rows[0]["INVC_FKMREF"].ToString()));
                TXT_INVC_REFDESC.Text = dtinfo.Rows[0]["INVC_REFDESC"].ToString().Trim();
                TXT_INVC_ISSDT.Text = dtinfo.Rows[0]["INVC_ISSDT"].ToString().Trim();
                CMB_INVC_DEBIT_NAME.SelectedIndex = CMB_INVC_DEBIT_NAME.Items.IndexOf(CMB_INVC_DEBIT_NAME.Items.FindByValue(dtinfo.Rows[0]["INVC_DEBIT_NAME"].ToString().Trim()));
                TXT_INVC_DEBIT_ACC.Text = dtinfo.Rows[0]["INVC_DEBIT_ACC"].ToString().Trim();
                CMB_INVC_CREDIT_NAME.SelectedIndex = CMB_INVC_CREDIT_NAME.Items.IndexOf(CMB_INVC_CREDIT_NAME.Items.FindByValue(dtinfo.Rows[0]["INVC_CREDIT_NAME"].ToString().Trim()));
                TXT_INVC_CREDIT_ACC.Text = dtinfo.Rows[0]["INVC_CREDIT_ACC"].ToString().Trim();
                CMB_INVC_CURRCD.DataBind();
                CMB_INVC_CURRCD.SelectedIndex = CMB_INVC_CURRCD.Items.IndexOf(CMB_INVC_CURRCD.Items.FindByValue(dtinfo.Rows[0]["INVC_CURRCD"].ToString().Trim()));
                TXT_INVC_VALUEDT.Text = dtinfo.Rows[0]["INVC_VALUEDT"].ToString().Trim();
                TXT_INVC_VALUEAMT.Text = String.Format("{0:#,##0.00}", dtinfo.Rows[0]["INVC_VALUEAMT"]);   //'dtinfo.Rows[0]["INVC_VALUEAMT"].ToString().Trim()
                TXT_INVC_VALUEAMT_OLD.Text = String.Format("{0:#,##0.00}", dtinfo.Rows[0]["INVC_VALUEAMT"]);// ' dtinfo.Rows[0]["INVC_VALUEAMT"].ToString().Trim()
                TXT_INVC_NARR.Text = dtinfo.Rows[0]["INVC_NARR"].ToString().Trim();

                CMB_INVC_STATUS.SelectedIndex = CMB_INVC_STATUS.Items.IndexOf(CMB_INVC_STATUS.Items.FindByValue(dtinfo.Rows[0]["INVC_STATUS"].ToString().Trim()));

                TXT_INVC_REFSRL.Text = CODE;

            }
            else
            {
                CLEAR_FIELDS();

                mbox("ERROR !!");
            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }
    }

    protected void CMB_INVC_REFSRL_SelectedIndexChanged(object sender, System.EventArgs e)  // Handles CMB_INVC_REFSRL.SelectedIndexChanged
    {
        String CODE = CMB_INVC_REFSRL.SelectedValue;
        GET_REFSRL_DETAILS(ref    CODE);
        //Call BTN_CLEAR_Click(sender, e)
        //Try
        //    'Dim COMP_CD As String = Session("USER_COMP_CD")
        //    Dim RETRVQRY As String = "SELECT * FROM INVOICE_INFO WHERE INVC_REFSRL = '" + CODE + "' "
        //    Dim dtinfo As DataTable = dbo.SelTable(RETRVQRY)
        //    if( dtinfo.Rows.Count = 1 ){
        //        '    CMB_INVCTYP.SelectedIndex = CMB_INVCTYP.Items.IndexOf(CMB_INVCTYP.Items.FindByValue(dtinfo.Rows[0]["INVC_TYPE"].ToString().Trim()))
        //        CMB_INVC_REFSRL.SelectedIndex = CMB_INVC_REFSRL.Items.IndexOf(CMB_INVC_REFSRL.Items.FindByValue(CODE))
        //        CMB_FKM_CD.SelectedIndex = CMB_FKM_CD.Items.IndexOf(CMB_FKM_CD.Items.FindByValue(dtinfo.Rows[0]["INVC_FKMREF").ToString))
        //        TXT_INVC_REFDESC.Text = dtinfo.Rows[0]["INVC_REFDESC"].ToString().Trim()
        //        TXT_INVC_ISSDT.Text = dtinfo.Rows[0]["INVC_ISSDT"].ToString().Trim()
        //        CMB_INVC_DEBIT_NAME.SelectedIndex = CMB_INVC_DEBIT_NAME.Items.IndexOf(CMB_INVC_DEBIT_NAME.Items.FindByValue(dtinfo.Rows[0]["INVC_DEBIT_NAME"].ToString().Trim()))
        //        TXT_INVC_DEBIT_ACC.Text = dtinfo.Rows[0]["INVC_DEBIT_ACC"].ToString().Trim()
        //        CMB_INVC_CREDIT_NAME.SelectedIndex = CMB_INVC_CREDIT_NAME.Items.IndexOf(CMB_INVC_CREDIT_NAME.Items.FindByValue(dtinfo.Rows[0]["INVC_CREDIT_NAME"].ToString().Trim()))
        //        TXT_INVC_CREDIT_ACC.Text = dtinfo.Rows[0]["INVC_CREDIT_ACC"].ToString().Trim()
        //        CMB_INVC_CURRCD.DataBind()
        //        CMB_INVC_CURRCD.SelectedIndex = CMB_INVC_CURRCD.Items.IndexOf(CMB_INVC_CURRCD.Items.FindByValue(dtinfo.Rows[0]["INVC_CURRCD"].ToString().Trim()))
        //        TXT_INVC_VALUEDT.Text = dtinfo.Rows[0]["INVC_VALUEDT"].ToString().Trim()
        //        TXT_INVC_VALUEAMT.Text = String.Format("{0:#,##0.00}", dtinfo.Rows[0]["INVC_VALUEAMT"))   'dtinfo.Rows[0]["INVC_VALUEAMT"].ToString().Trim()
        //        TXT_INVC_VALUEAMT_OLD.Text = String.Format("{0:#,##0.00}", dtinfo.Rows[0]["INVC_VALUEAMT")) ' dtinfo.Rows[0]["INVC_VALUEAMT"].ToString().Trim()
        //        TXT_INVC_NARR.Text = dtinfo.Rows[0]["INVC_NARR"].ToString().Trim()

        //        CMB_INVC_STATUS.SelectedIndex = CMB_INVC_STATUS.Items.IndexOf(CMB_INVC_STATUS.Items.FindByValue(dtinfo.Rows[0]["INVC_STATUS"].ToString().Trim()))

        //        TXT_INVC_REFSRL.Text = CODE

        //  }else{
        //        Call CLEAR_FIELDS()
        //        'BTN_CLEAR_Click(sender, e)
        //        mbox("ERROR !!")
        //  }
        //Catch EX As Exception
        //    mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8))
        //End Try
    }

    protected void CMB_FKM_CD_SelectedIndexChanged(object sender, System.EventArgs e)
    { // Handles CMB_FKM_CD.SelectedIndexChanged
        String CODE = CMB_FKM_CD.SelectedValue;
        try
        {

            //   ' Dim COMP_CD As String = Session("USER_COMP_CD")
            String RETRVQRY = "SELECT * FROM INVOICE_INFO WHERE INVC_REFSRL = '" + CODE + "' ";
            DataTable dtinfo = dbo.SelTable(RETRVQRY);
            if (dtinfo.Rows.Count == 1)
            {
                CMB_INVC_CURRCD.SelectedIndex = CMB_INVC_CURRCD.Items.IndexOf(CMB_INVC_CURRCD.Items.FindByValue(dtinfo.Rows[0]["FKM_CURR"].ToString().Trim()));
            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }
    }

    protected void CMB_INVC_CREDIT_NAME_SelectedIndexChanged(object sender, System.EventArgs e)
    { // Handles CMB_INVC_CREDIT_NAME.SelectedIndexChanged
        String CODE = CMB_INVC_CREDIT_NAME.SelectedValue;
        try
        {
            // 'Dim COMP_CD As String = Session("USER_COMP_CD")
            String RETRVQRY = "SELECT * FROM BANK_INFO WHERE   BNK_REFSRL = '" + CODE + "' ";
            DataTable dtinfo = dbo.SelTable(RETRVQRY);
            if (dtinfo.Rows.Count == 1)
            {

                if (dtinfo.Rows[0]["BNK_IBAN_NO"].ToString().Trim().Length > 0)
                {
                    TXT_INVC_CREDIT_ACC.Text = dtinfo.Rows[0]["BNK_IBAN_NO"].ToString().Trim();
                }
                else
                {
                    TXT_INVC_CREDIT_ACC.Text = dtinfo.Rows[0]["BNK_ACCOUNT_NO"].ToString().Trim();
                }

                CMB_INVC_CURRCD.SelectedIndex = CMB_INVC_CURRCD.Items.IndexOf(CMB_INVC_CURRCD.Items.FindByValue(dtinfo.Rows[0]["BNK_CURRCD"].ToString().Trim()));
            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }
    }

    protected void CMB_INVC_DEBIT_NAME_SelectedIndexChanged(object sender, System.EventArgs e)
    { // Handles CMB_INVC_DEBIT_NAME.SelectedIndexChanged
        String CODE = CMB_INVC_DEBIT_NAME.SelectedValue;
        try
        {
            // 'Dim COMP_CD As String = Session("USER_COMP_CD")
            String RETRVQRY = "SELECT * FROM BANK_INFO WHERE   BNK_REFSRL = '" + CODE + "'  ";
            DataTable dtinfo = dbo.SelTable(RETRVQRY);
            if (dtinfo.Rows.Count == 1)
            {

                if (dtinfo.Rows[0]["BNK_IBAN_NO"].ToString().Trim().Length > 0)
                {
                    TXT_INVC_DEBIT_ACC.Text = dtinfo.Rows[0]["BNK_IBAN_NO"].ToString().Trim();
                }
                else
                {
                    TXT_INVC_DEBIT_ACC.Text = dtinfo.Rows[0]["BNK_ACCOUNT_NO"].ToString().Trim();
                }

                CMB_INVC_CURRCD.SelectedIndex = CMB_INVC_CURRCD.Items.IndexOf(CMB_INVC_CURRCD.Items.FindByValue(dtinfo.Rows[0]["BNK_CURRCD"].ToString().Trim()));
            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }
    }

    protected Decimal GET_BANK_BALANCE(ref string BNK_CODE)
    {
        Decimal BNK_BALANCE_AMT = 0;
        try
        {
            //' Dim COMP_CD As String = Session("USER_COMP_CD")
            String RETRVQRY = "SELECT * FROM BANK_INFO WHERE   BNK_REFSRL = '" + BNK_CODE + "'  ";
            DataTable dtinfo = dbo.SelTable(RETRVQRY);
            if (dtinfo.Rows.Count == 1)
            {
                if (dtinfo.Rows[0]["BNK_BALANCE_AMT"].ToString().Length > 0)
                {
                    BNK_BALANCE_AMT = decimal.Parse(dtinfo.Rows[0]["BNK_BALANCE_AMT"].ToString());
                }
            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }
        return BNK_BALANCE_AMT;
    }


    protected void GET_GRID_INFO()
    {
        String INVCTYPE = CMB_INVCTYP.SelectedValue;
        String CURRCD = "%";//'CMB_INVC_CURRCD.SelectedValue
        String FKMREF = "";
        String BNK_CODE = "";
        String FRMDT = "";
        String TODT = "";

        //'if( CMB_FKM_CD.Text.Length > 0 ){
        //'    FKMREF = " AND INVC_FKMREF LIKE '" + CMB_FKM_CD.SelectedValue + "' "
        //'End if(

        //'if( CMB_INVC_DEBIT_NAME.Text.Length > 0 ){
        //'    BNK_CODE = " AND (INVC_DEBIT_NAME LIKE '" + CMB_INVC_DEBIT_NAME.SelectedItem.Text + "' OR INVC_CREDIT_NAME LIKE '" + CMB_INVC_DEBIT_NAME.SelectedItem.Text + "') "
        //'End if(


        //'if( TXT_INVC_ISSDT.Text.Length > 0 And TXT_INVC_VALUEDT.Text.Length > 0 ){
        //'    FRMDT = " AND (INVC_ISSDT BETWEEN '" + TXT_INVC_ISSDT.Text + "' AND   '" + TXT_INVC_VALUEDT.Text + "') "
        //'Else
        //'    if( TXT_INVC_ISSDT.Text.Length > 0 ){
        //'        FRMDT = " AND INVC_ISSDT = '" + TXT_INVC_ISSDT.Text + "'   "
        //'  }else{
        //'  }
        //'    if( TXT_INVC_VALUEDT.Text.Length > 0 ){
        //'        TODT = " AND INVC_ISSDT = '" + TXT_INVC_VALUEDT.Text + "'   "
        //'  }else{
        //'  }
        //'End if(


        try
        {
            //' Dim COMP_CD As String = Session("USER_COMP_CD")
            String RETRVQRY = "SELECT * FROM INVOICE_INFO WHERE INVC_TYPE LIKE '" + INVCTYPE + "' AND INVC_CURRCD  LIKE '" + CURRCD + "'  " +
                                     FKMREF + BNK_CODE + FRMDT + TODT;
            DataTable dtinfo = dbo.SelTable(RETRVQRY);
            if (dtinfo.Rows.Count > 0)
            {

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

    protected void GridView1_SelectedIndexChanged(object sender, System.EventArgs e)// Handles CMB_BTRX_REFSRL.SelectedIndexChanged
    {

        if (GridView1.SelectedDataKey[0].ToString().Trim().Length != 0)
        {
            string CODE = (string)GridView1.SelectedDataKey[0];
            GET_REFSRL_DETAILS(ref    CODE);
        }
    }

    protected void mbox(string val)
    {
        //string cs   = "c";
        //string cst  = "<script type=""text/javascript"">" + "alert('" + val + "');</" + "script>";
        //Page page    = HttpContext.Current.Handler;
        //page.ClientScript.RegisterStartupScript(Me.typeof, cs, cst);

        string USR = User.Identity.Name.ToUpper();
        dbo.ErrorLog(USR + " : INVOICE_MTNC.aspx : " + val, Server.MapPath("~\\Logs\\ErrorLog"));
    }
}
