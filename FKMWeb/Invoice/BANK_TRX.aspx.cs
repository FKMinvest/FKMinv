using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class Invoice_BANK_TRX : Page
{
    fkminvcom dbo = new fkminvcom();
    // Visual C# .NET requires that you override the OnInit function,
    // adding a new delegate for the Page_Load event.

    override protected void OnInit(System.EventArgs e)
    {
        this.Load += new System.EventHandler(this.Page_Load);
        base.OnInit(e);
        CMB_BTRX_REFSRL.SelectedIndexChanged += new System.EventHandler(CMB_BTRX_REFSRL_SelectedIndexChanged);
        CMB_FKM_CD.SelectedIndexChanged += new System.EventHandler(CMB_FKM_CD_SelectedIndexChanged);
        CMB_BTRX_BANK_NAME.SelectedIndexChanged += new System.EventHandler(CMB_BTRX_BANK_NAME_SelectedIndexChanged);

        GridView1.SelectedIndexChanged += new System.EventHandler(GridView1_SelectedIndexChanged);
        // GridView1.RowDeleting += new System.Web.UI.WebControls.GridViewDeleteEventHandler(Gridview1_RowDeleting);
        //GridView1.RowDataBound += new System.Web.UI.WebControls.GridViewRowEventHandler(GridView1_RowDataBound); 
        CHBX_EDIT.CheckedChanged += new System.EventHandler(CHBX_EDIT_CheckedChanged);
        Button1.Click += new System.EventHandler(Button1_Click);

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
                CMB_BTRXTYP.SelectedIndex = CMB_BTRXTYP.Items.IndexOf(CMB_BTRXTYP.Items.FindByValue(INCCODE));
                LBL_CAPTION.Text = CMB_BTRXTYP.SelectedItem.Text;
            }


            CLEAR_FIELDS();
            INIT_FKMCD();
            INIT_BANK_TRX_SRL();
            INIT_BANK_NAME();


            if ((Request.QueryString.Get("REFSRL")) != null)
            {
                String REFCODE = Request.QueryString.Get("REFSRL");
                GET_REFSRL_DETAILS(ref REFCODE);
            }


        }
    }

    protected void INIT_FKMCD()
    {
        // 'Dim COMP_CD As String = Session("USER_COMP_CD")
        String RETRVQRY = "SELECT DISTINCT FKM_SRL ,FKM_SRL + '-' + FKM_PROJNAME AS FKM_PROJNAME  FROM INVEST_INFO  ORDER BY FKM_SRL";
        DataTable dtinfo = dbo.SelTable(RETRVQRY);
        CMB_FKM_CD.DataSource = dtinfo;
        CMB_FKM_CD.DataBind();
        CMB_FKM_CD.SelectedIndex = -1;

    }

    protected void INIT_BANK_TRX_SRL()
    {
        String CODE = CMB_BTRXTYP.SelectedValue;
        //' Dim COMP_CD As String = Session("USER_COMP_CD")
        String RETRVQRY = "SELECT DISTINCT BTRX_REFSRL  FROM BANK_TRX  WHERE BTRX_TYPE  LIKE '" + CODE + "%'  ORDER BY BTRX_REFSRL";
        //'BTRX_TYPE IN ('B','I','H','P') 
        DataTable dtinfo = dbo.SelTable(RETRVQRY);
        CMB_BTRX_REFSRL.DataSource = dtinfo;
        CMB_BTRX_REFSRL.DataBind();
        CMB_BTRX_REFSRL.SelectedIndex = -1;

    }

    protected void CLEAR_FIELDS()
{ 

        if( CHBX_EDIT.Checked == true ){

            CMB_FKM_CD.SelectedIndex = -1;
            TXT_BTRX_REFDESC.Text = "";
            TXT_BTRX_ISSDT.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
            CMB_BTRX_BANK_NAME.SelectedIndex = -1;
            TXT_BTRX_BANK_ACC.Text = "";
            CMB_BTRXCDTYP.SelectedIndex = 0;
            TXT_BTRX_VALUEDT.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
            TXT_BTRX_VALUEAMT.Text = "";
            TXT_BTRX_VALUEAMT_OLD.Text = "";
            TXT_BTRX_NARR.Text = "";
            CMB_BTRX_CURRCD.SelectedIndex = 0;
            CMB_BTRX_STATUS.SelectedIndex = 0;

            TXT_BTRX_REFSRL.Text = NEXT_REC_SRL();
            TXT_BTRX_REFSRL.Visible = false;
            BTN_ADD.Visible = false;
            CMB_BTRX_BANK_NAME.Enabled = true;
            CMB_BTRX_REFSRL.SelectedIndex = -1;
            CMB_BTRX_REFSRL.Visible = true;
            BTN_UPDT.Visible = true;
        }else{
            CMB_FKM_CD.SelectedIndex = -1;
            TXT_BTRX_REFDESC.Text = "";
            TXT_BTRX_ISSDT.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
            CMB_BTRX_BANK_NAME.SelectedIndex = -1;
            TXT_BTRX_BANK_ACC.Text = "";
            CMB_BTRXCDTYP.SelectedIndex = 0;
            TXT_BTRX_VALUEDT.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
            TXT_BTRX_VALUEAMT.Text = "";
            TXT_BTRX_VALUEAMT_OLD.Text = "";
            TXT_BTRX_NARR.Text = "";
            CMB_BTRX_CURRCD.SelectedIndex = 0;
            CMB_BTRX_STATUS.SelectedIndex = 0;

            TXT_BTRX_REFSRL.Text = NEXT_REC_SRL();
            TXT_BTRX_REFSRL.Visible = true;
            BTN_ADD.Visible = true;
            CMB_BTRX_BANK_NAME.Enabled = true;
            CMB_BTRX_REFSRL.SelectedIndex = -1;
            CMB_BTRX_REFSRL.Visible = false;
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
    

     protected void  BTN_ADD_Click(object sender, System.EventArgs e)  // Handles BTN_ADD.Click
     {
       // 'if( CMB_FKM_CD.SelectedIndex < 0 ){
       // '    mbox("PLEASE SELECT THE PROJECT NAME !!")
       // '}
        string USR   = User.Identity.Name.ToUpper();
        string UPDDT  = System.DateTime.Today.ToString("dd/MM/yyyy");
        string UPDTIME   = System.DateTime.Now.ToString("hh:mm:ss");
        string CODE  = TXT_BTRX_REFSRL.Text.ToUpper();
        //string COMP_CD = Session("USER_COMP_CD")
        string SRL  = NEXT_REC_SRL();
       try{
            string RETRVQRY   = "SELECT * FROM BANK_TRX WHERE BTRX_REFSRL = '" + CODE + "' ";
            if( dbo.RecExist(RETRVQRY) == false ){
                //'Dim CREDIT_BALAMT As Decimal = 0
                    decimal BANK_BALAMT  = 0;
                    string BANK_NAME   = "";
                   // ' Dim CREDIT_NAME As string = ""
                    string UPD_BANK_QRY   = "";
                    //' Dim UPD_CREDIT_QRY As string = ""

                    string STR_TEMP  = "0";
                    if( TXT_BTRX_VALUEAMT.Text.Trim().Length > 0 ){
                        STR_TEMP = TXT_BTRX_VALUEAMT.Text.Trim();
                    }
                    Decimal BTRX_VALUEAMT   = decimal.Parse(STR_TEMP);

                    STR_TEMP = "0";
                    if( TXT_BTRX_VALUEAMT_OLD.Text.Trim().Length > 0 ){
                        STR_TEMP = TXT_BTRX_VALUEAMT_OLD.Text.Trim();
                    }
                    decimal BTRX_VALUEAMT_OLD = decimal.Parse(STR_TEMP);


                    if( CMB_BTRXCDTYP.SelectedIndex == 0 ){
                        //BANK_NAME = CMB_BTRX_BANK_NAME.SelectedItem.Text.ToUpper();
                           BANK_NAME = CMB_BTRX_BANK_NAME.SelectedValue;
                        BANK_BALAMT = GET_BANK_BALANCE( CMB_BTRX_BANK_NAME.SelectedValue );

                        BANK_BALAMT = BANK_BALAMT - BTRX_VALUEAMT + BTRX_VALUEAMT_OLD;

                        UPD_BANK_QRY = "UPDATE BANK_INFO SET BNK_BALANCE_AMT = '" + BANK_BALAMT + "', BNK_LST_DEBIT_DT = '" + UPDDT +
                                               "', BNK_UPDBY = '" + USR + "', BNK_UPDDATE = '" + UPDDT + "', BNK_UPDTIME = '" + UPDTIME + "' " +
                                               " WHERE BNK_REFSRL= '" + CMB_BTRX_BANK_NAME.SelectedValue + "' ";
                    }

                    if( CMB_BTRXCDTYP.SelectedIndex == 1 ){
                       // BANK_NAME = CMB_BTRX_BANK_NAME.SelectedItem.Text.ToUpper();
                           BANK_NAME = CMB_BTRX_BANK_NAME.SelectedValue;
                        BANK_BALAMT = GET_BANK_BALANCE(CMB_BTRX_BANK_NAME.SelectedValue);

                        BANK_BALAMT = BANK_BALAMT + BTRX_VALUEAMT - BTRX_VALUEAMT_OLD;

                        UPD_BANK_QRY = "UPDATE BANK_INFO SET BNK_BALANCE_AMT = '" + BANK_BALAMT + "', BNK_LST_CREDIT_DT = '" + UPDDT +
                                               "', BNK_UPDBY = '" + USR + "', BNK_UPDDATE = '" + UPDDT + "', BNK_UPDTIME = '" + UPDTIME + "' " +
                                               " WHERE BNK_REFSRL= '" + CMB_BTRX_BANK_NAME.SelectedValue + "' ";
                    }

                    if( CMB_BTRX_STATUS.SelectedIndex == 1 ){
                       // 'BANK_NAME = ""
                        BANK_BALAMT = GET_BANK_BALANCE(CMB_BTRX_BANK_NAME.SelectedValue);
                        if( CMB_BTRXCDTYP.SelectedIndex == 1 ){
                            BANK_BALAMT = BANK_BALAMT - BTRX_VALUEAMT_OLD;
                        }else{
                            BANK_BALAMT = BANK_BALAMT + BTRX_VALUEAMT_OLD;
                        }
                        BTRX_VALUEAMT = 0;
                        UPD_BANK_QRY = "UPDATE BANK_INFO SET BNK_BALANCE_AMT = '" + BANK_BALAMT + "', BNK_LST_CREDIT_DT = '" + UPDDT +
                                               "', BNK_UPDBY = '" + USR + "', BNK_UPDDATE = '" + UPDDT + "', BNK_UPDTIME = '" + UPDTIME + "' " +
                                               " WHERE BNK_REFSRL= '" + CMB_BTRX_BANK_NAME.SelectedValue + "' ";
                    }

                    string INSQRY   = "INSERT INTO BANK_TRX VALUES('','" + SRL + "','" + TXT_BTRX_ISSDT.Text.ToUpper() + "','" + TXT_BTRX_VALUEDT.Text.ToUpper() +
                                                   "','" + CMB_FKM_CD.SelectedValue.ToUpper()  + "','" + CMB_BTRXCDTYP.SelectedValue.ToUpper() + "','" + CMB_BTRXTYP.SelectedValue.ToUpper() +
                                                   "','" + CMB_LEDGER.Text.ToUpper() + "','" + TXT_BTRX_REFDESC.Text.ToUpper() + "','" + TXT_BTRX_NARR.Text.ToUpper() +
                                                   "','" + CMB_BTRX_CURRCD.SelectedValue.ToUpper() + "','" + BTRX_VALUEAMT +
                                                   "','" + BANK_NAME + "','" + TXT_BTRX_BANK_ACC.Text.ToUpper() + "','" + BANK_BALAMT +
                                                   "','" + CMB_BTRX_STATUS.SelectedValue + "' ,'" + USR + "','" + UPDDT + "','" + USR + "','" + UPDDT + "','" + UPDTIME + "')";


                    if( dbo.Transact(INSQRY, UPD_BANK_QRY, "", "", "") == false ){
                        mbox("ERROR !!");
                    }else{
                       // 'GridView1.DataBind()a
                        mbox("CODE : '" + SRL + "'      UPLOADED SUCCESSFULLY !!");
                        BTN_CLEAR_Click(sender, e);
                        INIT_BANK_TRX_SRL();
                    }  
            }else{
               // 'TXT_SRL.Text = NEXT_REC_SRL()
                mbox("PLEASE TRY AGAIN .. ALREADY FILE EXIST FOR : " + SRL + "!!");

            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }
     }

     protected void BTN_UPDT_Click(object sender, System.EventArgs e)// Handles BTN_UPDT.Click
     {
         string USR = User.Identity.Name.ToUpper();
         string UPDDT = System.DateTime.Today.ToString("dd/MM/yyyy");
         string UPDTIME = System.DateTime.Now.ToString("hh:mm:ss");
         string CODE = CMB_BTRX_REFSRL.SelectedValue.Trim();
         //   // 'Dim SRL As string = "0"
         try
         {
             string RETRVQRY = "SELECT * FROM BANK_TRX WHERE BTRX_REFSRL = '" + CODE + "' ";
             if (dbo.RecExist(RETRVQRY) == true)
             {
                 //'Dim CREDIT_BALAMT As Decimal = 0
                 decimal BANK_BALAMT = 0;
                 string BANK_NAME = "";
                 // ' Dim CREDIT_NAME As string = ""
                 string UPD_BANK_QRY = "";
                 //' Dim UPD_CREDIT_QRY As string = ""true


                 string STR_TEMP = "0";
                 if (TXT_BTRX_VALUEAMT.Text.Trim().Length > 0)
                 {
                     STR_TEMP = TXT_BTRX_VALUEAMT.Text.Trim();
                 }
                 Decimal BTRX_VALUEAMT = decimal.Parse(STR_TEMP);

                 STR_TEMP = "0";
                 if (TXT_BTRX_VALUEAMT_OLD.Text.Trim().Length > 0)
                 {
                     STR_TEMP = TXT_BTRX_VALUEAMT_OLD.Text.Trim();
                 }
                 decimal BTRX_VALUEAMT_OLD = decimal.Parse(STR_TEMP);



                 if (CMB_BTRXCDTYP.SelectedIndex == 0)
                 {
                     //BANK_NAME = CMB_BTRX_BANK_NAME.SelectedItem.Text.ToUpper();
                          BANK_NAME = CMB_BTRX_BANK_NAME.SelectedValue;
                     BANK_BALAMT = GET_BANK_BALANCE(CMB_BTRX_BANK_NAME.SelectedValue);

                     BANK_BALAMT = BANK_BALAMT - BTRX_VALUEAMT + BTRX_VALUEAMT_OLD;

                     UPD_BANK_QRY = "UPDATE BANK_INFO SET BNK_BALANCE_AMT = '" + BANK_BALAMT + "', BNK_LST_DEBIT_DT = '" + UPDDT +
                                            "', BNK_UPDBY = '" + USR + "', BNK_UPDDATE = '" + UPDDT + "', BNK_UPDTIME = '" + UPDTIME + "' " +
                                            " WHERE BNK_REFSRL= '" + CMB_BTRX_BANK_NAME.SelectedValue + "' ";
                 }

                 if (CMB_BTRXCDTYP.SelectedIndex == 1)
                 {
                    // BANK_NAME = CMB_BTRX_BANK_NAME.SelectedItem.Text.ToUpper();
                           BANK_NAME = CMB_BTRX_BANK_NAME.SelectedValue;
                     BANK_BALAMT = GET_BANK_BALANCE(CMB_BTRX_BANK_NAME.SelectedValue);

                     BANK_BALAMT = BANK_BALAMT + BTRX_VALUEAMT - BTRX_VALUEAMT_OLD;

                     UPD_BANK_QRY = "UPDATE BANK_INFO SET BNK_BALANCE_AMT = '" + BANK_BALAMT + "', BNK_LST_CREDIT_DT = '" + UPDDT +
                                            "', BNK_UPDBY = '" + USR + "', BNK_UPDDATE = '" + UPDDT + "', BNK_UPDTIME = '" + UPDTIME + "' " +
                                            " WHERE BNK_REFSRL= '" + CMB_BTRX_BANK_NAME.SelectedValue + "' ";
                 }


                 if (CMB_BTRX_STATUS.SelectedIndex == 1)
                 {
                     // 'BANK_NAME = ""
                     BANK_BALAMT = GET_BANK_BALANCE(CMB_BTRX_BANK_NAME.SelectedValue);
                     if (CMB_BTRXCDTYP.SelectedIndex == 1)
                     {
                         BANK_BALAMT = BANK_BALAMT - BTRX_VALUEAMT_OLD;
                         UPD_BANK_QRY = "UPDATE BANK_INFO SET BNK_BALANCE_AMT = '" + BANK_BALAMT + "', BNK_LST_DEBIT_DT = '" + UPDDT +
                                                "', BNK_UPDBY = '" + USR + "', BNK_UPDDATE = '" + UPDDT + "', BNK_UPDTIME = '" + UPDTIME + "' " +
                                                " WHERE BNK_REFSRL= '" + CMB_BTRX_BANK_NAME.SelectedValue + "' ";
                     }
                     else
                     {
                         BANK_BALAMT = BANK_BALAMT + BTRX_VALUEAMT_OLD;
                         UPD_BANK_QRY = "UPDATE BANK_INFO SET BNK_BALANCE_AMT = '" + BANK_BALAMT + "', BNK_LST_CREDIT_DT = '" + UPDDT +
                                                "', BNK_UPDBY = '" + USR + "', BNK_UPDDATE = '" + UPDDT + "', BNK_UPDTIME = '" + UPDTIME + "' " +
                                                " WHERE BNK_REFSRL= '" + CMB_BTRX_BANK_NAME.SelectedValue + "' ";
                     }
                     BTRX_VALUEAMT = 0;
                 }

                 string UPDQRY = "UPDATE BANK_TRX SET BTRX_ISSDT = '" + TXT_BTRX_ISSDT.Text.ToUpper() + "',BTRX_FKMREF = '" + CMB_FKM_CD.SelectedValue.ToUpper() +
                                        "', BTRX_VALUEDT  = '" + TXT_BTRX_VALUEDT.Text.ToUpper() + "', BTRX_LEDGER = '" + CMB_LEDGER.Text.ToUpper() + "',BTRX_REFDESC = '" + TXT_BTRX_REFDESC.Text.ToUpper() +
                                        "', BTRX_NARR = '" + TXT_BTRX_NARR.Text.ToUpper() + "',BTRX_CURRCD = '" + CMB_BTRX_CURRCD.SelectedValue.ToUpper() +
                                        "', BTRX_VALUEAMT = '" + BTRX_VALUEAMT + "', BTRX_BAL_AMT = '" + BANK_BALAMT +
                                        "', BTRX_BANK_NAME = '" + BANK_NAME + "',BTRX_BANK_ACC = '" + TXT_BTRX_BANK_ACC.Text.ToUpper() +
                                        "', BTRX_STATUS = '" + CMB_BTRX_STATUS.SelectedValue + "', BTRX_UPDBY = '" + USR + "', BTRX_UPDDATE = '" + UPDDT + "', BTRX_UPDTIME = '" + UPDTIME + "' " +
                                        " WHERE BTRX_REFSRL= '" + CODE + "'";


                 if (dbo.Transact(UPDQRY, UPD_BANK_QRY, "", "", "") == false)
                 {
                     mbox("ERROR !!");
                 }
                 else
                 {
                     // 'GridView1.DataBind()a
                     mbox("CODE : '" + CODE + "'      UPLOADED SUCCESSFULLY !!");
                     CLEAR_FIELDS();
                 }
             }
             //    else{
             //   // 'TXT_SRL.Text = NEXT_REC_SRL()
             //    mbox("PLEASE TRY AGAIN .. ALREADY FILE EXIST FOR : " + CODE + "!!");

             //}
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
            String RETRVQRY = "SELECT MAX (BTRX_REFSRL) FROM BANK_TRX  WHERE BTRX_REFSRL LIKE '" + System.DateTime.Today.Year + "%' ";
            DTINVINF = dbo.SelTable(RETRVQRY);
            if (DTINVINF.Rows.Count > 0)
            {
                if (DTINVINF.Rows[0][0].ToString() != null)
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


    protected void GET_REFSRL_DETAILS(ref String CODE)
{
        String CODE1 = CMB_BTRX_REFSRL.SelectedValue;
        CHBX_EDIT.Checked = true;

        CLEAR_FIELDS();


        try{

            String RETRVQRY   = "SELECT * FROM BANK_TRX WHERE BTRX_REFSRL = '" + CODE + "' ";
            DataTable dtinfo   = dbo.SelTable(RETRVQRY);
            if( dtinfo.Rows.Count == 1 ){


                CMB_BTRXTYP.SelectedIndex = CMB_BTRXTYP.Items.IndexOf(CMB_BTRXTYP.Items.FindByValue(dtinfo.Rows[0]["BTRX_TYPE"].ToString().Trim()));
                LBL_CAPTION.Text = CMB_BTRXTYP.SelectedItem.Text;

                  INIT_BANK_TRX_SRL();
                  INIT_BANK_NAME();

                CMB_BTRX_REFSRL.SelectedIndex = CMB_BTRX_REFSRL.Items.IndexOf(CMB_BTRX_REFSRL.Items.FindByValue(CODE));

                CMB_FKM_CD.SelectedIndex = CMB_FKM_CD.Items.IndexOf(CMB_FKM_CD.Items.FindByValue(dtinfo.Rows[0]["BTRX_FKMREF"].ToString()));
                CMB_LEDGER.SelectedIndex = CMB_LEDGER.Items.IndexOf(CMB_LEDGER.Items.FindByValue(dtinfo.Rows[0]["BTRX_LEDGER"].ToString()));
                TXT_BTRX_REFDESC.Text = dtinfo.Rows[0]["BTRX_REFDESC"].ToString().Trim();
                TXT_BTRX_ISSDT.Text = dtinfo.Rows[0]["BTRX_ISSDT"].ToString().Trim();
                CMB_BTRXCDTYP.SelectedIndex = CMB_BTRXCDTYP.Items.IndexOf(CMB_BTRXCDTYP.Items.FindByValue(dtinfo.Rows[0]["BTRX_CD_TYPE"].ToString().Trim()));

                for(int I = 0 ; I <= CMB_BTRX_BANK_NAME.Items.Count - 1 ; I++)
                {
                    if( dtinfo.Rows[0]["BTRX_BANK_NAME"].ToString().Trim() == CMB_BTRX_BANK_NAME.Items[I].Value.Trim() ){
                        CMB_BTRX_BANK_NAME.SelectedIndex = I;
                    }
            }
            //' CMB_BTRX_BANK_NAME.SelectedIndex = CMB_BTRX_BANK_NAME.Items.IndexOf(CMB_BTRX_BANK_NAME.Items.FindByText(dtinfo.Rows[0]["BTRX_BANK_NAME").ToString))
                TXT_BTRX_BANK_ACC.Text = dtinfo.Rows[0]["BTRX_BANK_ACC"].ToString().Trim();
              //  'CMB_BTRX_CREDIT_NAME.SelectedIndex = CMB_BTRX_CREDIT_NAME.Items.IndexOf(CMB_BTRX_CREDIT_NAME.Items.FindByText(dtinfo.Rows[0]["BTRX_CREDIT_NAME").ToString))
                ///'TXT_BTRX_CREDIT_ACC.Text = dtinfo.Rows[0]["BTRX_CREDIT_ACC"].ToString().Trim()
                CMB_BTRX_CURRCD.DataBind();
                CMB_BTRX_CURRCD.SelectedIndex = CMB_BTRX_CURRCD.Items.IndexOf(CMB_BTRX_CURRCD.Items.FindByValue(dtinfo.Rows[0]["BTRX_CURRCD"].ToString().Trim()));
                TXT_BTRX_VALUEDT.Text = dtinfo.Rows[0]["BTRX_VALUEDT"].ToString().Trim();
                TXT_BTRX_VALUEAMT.Text = String.Format("{0:#,##0.00}", dtinfo.Rows[0]["BTRX_VALUEAMT"]);// ' dtinfo.Rows[0]["BTRX_VALUEAMT"].ToString().Trim()
                TXT_BTRX_VALUEAMT_OLD.Text = String.Format("{0:#,##0.00}", dtinfo.Rows[0]["BTRX_VALUEAMT"]);// 'dtinfo.Rows[0]["BTRX_VALUEAMT"].ToString().Trim()
                TXT_BTRX_NARR.Text = dtinfo.Rows[0]["BTRX_NARR"].ToString().Trim();

                CMB_BTRX_STATUS.SelectedIndex = CMB_BTRX_STATUS.Items.IndexOf(CMB_BTRX_STATUS.Items.FindByValue(dtinfo.Rows[0]["BTRX_STATUS"].ToString().Trim()));

                TXT_BTRX_REFSRL.Text = CODE;

            }else{
               CLEAR_FIELDS();
              //  'BTN_CLEAR_Click(sender, e)
                mbox("ERROR !!");
            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }

  }

    protected void CMB_BTRX_REFSRL_SelectedIndexChanged(object sender, System.EventArgs e)
    { // Handles CMB_BTRX_REFSRL.SelectedIndexChanged

        String CODE = CMB_BTRX_REFSRL.SelectedValue;
        //'Dim SRL As String = "0"
        GET_REFSRL_DETAILS(ref  CODE);
        //Call BTN_CLEAR_Click(sender, e)
        //Try

        //    Dim RETRVQRY As String = "SELECT * FROM BANK_TRX WHERE BTRX_REFSRL = '" + CODE + "' "
        //    Dim dtinfo As DataTable = dbo.SelTable(RETRVQRY)
        //    if( dtinfo.Rows.Count = 1 ){
        //        '    CMB_BTRXTYP.SelectedIndex = CMB_BTRXTYP.Items.IndexOf(CMB_BTRXTYP.Items.FindByValue(dtinfo.Rows[0]["BTRX_TYPE"].ToString().Trim()))
        //        CMB_BTRX_REFSRL.SelectedIndex = CMB_BTRX_REFSRL.Items.IndexOf(CMB_BTRX_REFSRL.Items.FindByValue(CODE))
        //        CMB_FKM_CD.SelectedIndex = CMB_FKM_CD.Items.IndexOf(CMB_FKM_CD.Items.FindByValue(dtinfo.Rows[0]["BTRX_FKMREF").ToString))
        //        TXT_BTRX_REFDESC.Text = dtinfo.Rows[0]["BTRX_REFDESC"].ToString().Trim()
        //        TXT_BTRX_ISSDT.Text = dtinfo.Rows[0]["BTRX_ISSDT"].ToString().Trim()
        //        CMB_BTRXCDTYP.SelectedIndex = CMB_BTRXCDTYP.Items.IndexOf(CMB_BTRXCDTYP.Items.FindByValue(dtinfo.Rows[0]["BTRX_CD_TYPE"].ToString().Trim()))

        //        For I = 0 To CMB_BTRX_BANK_NAME.Items.Count - 1
        //            if( dtinfo.Rows[0]["BTRX_BANK_NAME"].ToString().Trim() = CMB_BTRX_BANK_NAME.Items(I).Value.Trim ){
        //                CMB_BTRX_BANK_NAME.SelectedIndex = I
        //            }
        //        Next
        //        ' CMB_BTRX_BANK_NAME.SelectedIndex = CMB_BTRX_BANK_NAME.Items.IndexOf(CMB_BTRX_BANK_NAME.Items.FindByText(dtinfo.Rows[0]["BTRX_BANK_NAME").ToString))

        //        TXT_BTRX_BANK_ACC.Text = dtinfo.Rows[0]["BTRX_BANK_ACC"].ToString().Trim()
        //        'CMB_BTRX_CREDIT_NAME.SelectedIndex = CMB_BTRX_CREDIT_NAME.Items.IndexOf(CMB_BTRX_CREDIT_NAME.Items.FindByText(dtinfo.Rows[0]["BTRX_CREDIT_NAME").ToString))
        //        'TXT_BTRX_CREDIT_ACC.Text = dtinfo.Rows[0]["BTRX_CREDIT_ACC"].ToString().Trim()
        //        CMB_BTRX_CURRCD.DataBind()
        //        CMB_BTRX_CURRCD.SelectedIndex = CMB_BTRX_CURRCD.Items.IndexOf(CMB_BTRX_CURRCD.Items.FindByValue(dtinfo.Rows[0]["BTRX_CURRCD"].ToString().Trim()))
        //        TXT_BTRX_VALUEDT.Text = dtinfo.Rows[0]["BTRX_VALUEDT"].ToString().Trim()
        //        TXT_BTRX_VALUEAMT.Text = String.Format("{0:#,##0.00}", dtinfo.Rows[0]["BTRX_VALUEAMT")) ' dtinfo.Rows[0]["BTRX_VALUEAMT"].ToString().Trim()
        //        TXT_BTRX_VALUEAMT_OLD.Text = String.Format("{0:#,##0.00}", dtinfo.Rows[0]["BTRX_VALUEAMT")) 'dtinfo.Rows[0]["BTRX_VALUEAMT"].ToString().Trim()
        //        TXT_BTRX_NARR.Text = dtinfo.Rows[0]["BTRX_NARR"].ToString().Trim()

        //        CMB_BTRX_STATUS.SelectedIndex = CMB_BTRX_STATUS.Items.IndexOf(CMB_BTRX_STATUS.Items.FindByValue(dtinfo.Rows[0]["BTRX_STATUS"].ToString().Trim()))

        //        TXT_BTRX_REFSRL.Text = CODE

        //    }else{
        //        Call CLEAR_FIELDS()
        //        'BTN_CLEAR_Click(sender, e)
        //        mbox("ERROR !!")
        //    }
        //Catch EX As Exception
        //    mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8))
        //End Try
    }


    protected void CMB_FKM_CD_SelectedIndexChanged(object sender, System.EventArgs e)
    { // Handles CMB_FKM_CD.SelectedIndexChanged
        String CODE = CMB_FKM_CD.SelectedValue;
        try
        {
            // 'Dim COMP_CD As String = Session("USER_COMP_CD")
            String RETRVQRY = "SELECT * FROM INVEST_INFO WHERE FKM_SRL = '" + CODE + "'  ";
            DataTable dtinfo = dbo.SelTable(RETRVQRY);
            if (dtinfo.Rows.Count == 1)
            {
                CMB_BTRX_CURRCD.SelectedIndex = CMB_BTRX_CURRCD.Items.IndexOf(CMB_BTRX_CURRCD.Items.FindByValue(dtinfo.Rows[0]["FKM_CURR"].ToString().Trim()));
            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }
    }

    protected void CMB_BTRX_BANK_NAME_SelectedIndexChanged(object sender, System.EventArgs e)
    { // Handles CMB_BTRX_BANK_NAME.SelectedIndexChanged
        String CODE = CMB_BTRX_BANK_NAME.SelectedValue;
        try
        {
            //  'Dim COMP_CD As String = Session("USER_COMP_CD")
            String RETRVQRY = "SELECT * FROM BANK_INFO WHERE   BNK_REFSRL = '" + CODE + "' ";
            DataTable dtinfo = dbo.SelTable(RETRVQRY);
            if (dtinfo.Rows.Count == 1)
            {

                if (dtinfo.Rows[0]["BNK_IBAN_NO"].ToString().Trim().Length > 0)
                {
                    TXT_BTRX_BANK_ACC.Text = dtinfo.Rows[0]["BNK_IBAN_NO"].ToString().Trim();
                }
                else
                {
                    TXT_BTRX_BANK_ACC.Text = dtinfo.Rows[0]["BNK_ACCOUNT_NO"].ToString().Trim();
                }
                CMB_BTRX_CURRCD.SelectedIndex = CMB_BTRX_CURRCD.Items.IndexOf(CMB_BTRX_CURRCD.Items.FindByValue(dtinfo.Rows[0]["BNK_CURRCD"].ToString().Trim()));

            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }
    }

    protected void INIT_BANK_NAME()
{
        String CODE  = CMB_BTRXTYP.SelectedValue.Substring(0, 1);
        //'Dim COMP_CD As String = Session("USER_COMP_CD")
        String RETRVQRY   = "SELECT DISTINCT BNK_REFSRL,  BNK_NAME  + ' | ' + BNK_ACCOUNT_NO as BNK_NAME   FROM BANK_INFO  WHERE BNK_TYPE IN ('" + CODE + "')  ORDER BY BNK_REFSRL";
        DataTable dtinfo  = dbo.SelTable(RETRVQRY);



        CMB_BTRX_BANK_NAME.DataSource = dtinfo;
        CMB_BTRX_BANK_NAME.DataBind();

        CMB_BTRX_BANK_NAME.SelectedIndex = CMB_BTRX_BANK_NAME.Items.Count - 1;

    }


    protected Decimal GET_BANK_BALANCE(String BNK_CODE)
    {
        Decimal BNK_BALANCE_AMT = 0;
        try
        {// 'Dim COMP_CD As String = Session("USER_COMP_CD")
            String RETRVQRY = "SELECT * FROM BANK_INFO WHERE   BNK_REFSRL = '" + BNK_CODE + "' ";
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

    protected void GridView1_SelectedIndexChanged(object sender, System.EventArgs e)// Handles CMB_BTRX_REFSRL.SelectedIndexChanged
    {

        if (GridView1.SelectedDataKey[0].ToString().Trim().Length != 0)
        {
            string CODE = (string)GridView1.SelectedDataKey[0];
            GET_REFSRL_DETAILS(ref  CODE);
        }
    }

    protected void GET_GRID_INFO()
    {
        String BTRXTYPE = CMB_BTRXTYP.SelectedValue;
        String CURRCD = CMB_BTRX_CURRCD.SelectedValue;
        String FKMREF = "";
        String BNK_CODE = "";
        String FRMDT = "";
        String TODT = "";

        try
        {
            //  ' Dim COMP_CD As String = Session("USER_COMP_CD")
            String RETRVQRY = "SELECT BANK_TRX.*,BNK_NAME,BNK_ACCOUNT_NO FROM BANK_TRX,BANK_INFO WHERE BTRX_TYPE LIKE '" + BTRXTYPE + "'   " +
                                     FKMREF + BNK_CODE + FRMDT + TODT + "   AND BTRX_STATUS IN ('V') AND BNK_REFSRL = BTRX_BANK_NAME ORDER BY BTRX_REFSRL DESC";
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
                }
                GridView1.DataSource = dtinfo;
                GridView1.DataBind();
            }
            else
            {

                GridView1.DataSource = null;
                GridView1.DataBind();
            }
            Session["BANKTRX_dtINFO_REPORT"] = dtinfo;
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }
    }

    protected void GetSelectedRecords()
    {
        DataTable dt = new DataTable();

        DataTable dtinfo  = (DataTable)Session["BANKTRX_dtINFO_REPORT"];
        dt = dtinfo.Clone();
        dt.Rows.Clear();
       foreach (GridViewRow row in GridView1.Rows){
            if( row.RowType == DataControlRowType.DataRow ){
                CheckBox chkRow   = (CheckBox)row.Cells[0].FindControl("chkRow");
                if( chkRow.Checked ){
                    String BTRX_REFSRL  = row.Cells[2].Text.Replace("+nbsp;", "");
DataRow[] DR   = dtinfo.Select(" BTRX_REFSRL = '" + BTRX_REFSRL + "'    ");
                    if( DR.Length > 0 ){
                        dt.ImportRow(DR[0]);
                    }
                }
            }
       }

        ReportDocument rpt = new ReportDocument();
        String rname = Server.MapPath("~/Reports/BANKTRX_RPT.rpt");
        String dwnldfname   = Server.MapPath("~/ReportsGenerate/BANKTRX_RPT.PDF");
        //'  Dim dt As DataTable = TryCast(Session("NAVRPT_dt_NAVRPT_REPORT"), DataTable)

        if( dt.Rows.Count > 0 ){
            rpt.Load(rname);
            rpt.SetDataSource(dt);

            rpt.PrintOptions.PaperSize = PaperSize.PaperA4;
            rpt.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
            rpt.ExportToDisk(ExportFormatType.PortableDocFormat, dwnldfname);
            rpt.Close();
            rpt.Dispose();


            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("content-disposition", "attachment;filename =" + "PETTYTRX_RPT.PDF");
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
        dbo.ErrorLog(USR + " : BANK_TRX.aspx : " + val, Server.MapPath("~\\Logs\\ErrorLog"));
    }

}
