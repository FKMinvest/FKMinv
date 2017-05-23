using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class Invoice_PETTY_TRX : Page
{
    fkminvcom dbo = new fkminvcom();
    // Visual C# .NET requires that you override the OnInit function,
    // adding a new delegate for the Page_Load event.

    override protected void OnInit(System.EventArgs e)
    {
        this.Load += new System.EventHandler(this.Page_Load);
        base.OnInit(e);
        CMB_BTRX_REFSRL.SelectedIndexChanged += new System.EventHandler(CMB_BTRX_REFSRL_SelectedIndexChanged);
        CMB_MONTH.SelectedIndexChanged += new System.EventHandler(CMB_MONTH_SelectedIndexChanged);
        //UploadButton.Click += new System.EventHandler(UploadButton_Click1); 
        GridView1.SelectedIndexChanged += new System.EventHandler(GridView1_SelectedIndexChanged);
        GridView1.RowDeleting += new System.Web.UI.WebControls.GridViewDeleteEventHandler(Gridview1_RowDeleting);
        //GridView1.RowDataBound += new System.Web.UI.WebControls.GridViewRowEventHandler(GridView1_RowDataBound); 
        CHBX_EDIT.CheckedChanged += new System.EventHandler(CHBX_EDIT_CheckedChanged);
        Button1.Click += new System.EventHandler(Button1_Click);
         
        BTN_ADD.Click += new System.EventHandler(BTN_ADD_Click);
        BTN_UPDT.Click += new System.EventHandler(BTN_UPDT_Click);
        BTN_DELETE.Click += new System.EventHandler(BTN_DELETE_Click);
        BTN_CLEAR.Click += new System.EventHandler(BTN_CLEAR_Click);
    }

    public void Page_Load(object sender, System.EventArgs e)
    {

        if (!IsPostBack && !IsCallback)
        {
            if (Request.QueryString.Get("INCCD") != null)
            {
                string INCCODE = Request.QueryString.Get("INCCD");
                CMB_BTRXTYP.SelectedIndex = 0;//'CMB_BTRXTYP.Items.IndexOf(CMB_BTRXTYP.Items.FindByValue(INCCODE))
                LBL_CAPTION.Text = CMB_BTRXTYP.SelectedItem.Text;
            }
            INIT_MONTH();
            INIT_BANK_TRX_SRL();
            INIT_BANK_NAME();
            TXT_SRCH.Text = "";
            BTN_CLEAR_Click(sender, e);

            //'Call INIT_FKMCD()
            //'CHBX_EDIT.Checked = false

            if ((Request.QueryString.Get("REFSRL") != null))
            {
                string REFCODE = Request.QueryString.Get("REFSRL");
                GET_REFSRL_DETAILS(ref REFCODE);
            }

           // GET_GRID_INFO();
        }
    }

    public void INIT_BANK_TRX_SRL()
    {
        string CODE = CMB_BTRXTYP.SelectedValue;
        // ' Dim COMP_CD As string = Session("USER_COMP_CD")
        string RETRVQRY = "SELECT DISTINCT BTRX_REFSRL  FROM BANK_TRX  WHERE BTRX_TYPE  LIKE 'PETTY'  ORDER BY BTRX_REFSRL";
        // 'BTRX_TYPE IN ('B','I','H','P') 
        DataTable dtinfo = dbo.SelTable(RETRVQRY);
        CMB_BTRX_REFSRL.DataSource = dtinfo;
        CMB_BTRX_REFSRL.DataBind();
        CMB_BTRX_REFSRL.SelectedIndex = 0;
    }

    public void INIT_MONTH()
    {
        DataTable DT = new DataTable();

        DT.Columns.Add("MONTH");
        DT.Columns.Add("CD");

        // 'Dim COMP_CD As string = Session("USER_COMP_CD")
        string RETRVQRY = "SELECT DISTINCT SUBSTRING(BTRX_VALUEDT, 7, 4) + SUBSTRING(BTRX_VALUEDT, 4, 2) AS CD  FROM BANK_TRX  WHERE BTRX_TYPE  LIKE 'PETTY'  ORDER BY  CD DESC";
        DataTable dtinfo = dbo.SelTable(RETRVQRY);
        // 'dtinfo.Columns.Add("MONTH")
        // ' Dim YEAR As Integer = 2015
        foreach (DataRow DR in dtinfo.Rows)//' (System.DateTime.Today.Year - YEAR)
        {
            string YEAR = DR["CD"].ToString().Substring(0, 4);
            string MNT = DR["CD"].ToString().Substring(4, 2);
            switch (MNT)
            {
                case "01": DT.Rows.Add("JANUARY - " + YEAR, YEAR + "01"); break;
                case "02": DT.Rows.Add("FEBRUARY - " + YEAR, YEAR + "02"); break;
                case "03": DT.Rows.Add("MARCH - " + YEAR, YEAR + "03"); break;
                case "04": DT.Rows.Add("APRIL - " + YEAR, YEAR + "04"); break;
                case "05": DT.Rows.Add("MAY - " + YEAR, YEAR + "05"); break;
                case "06": DT.Rows.Add("JUNE - " + YEAR, YEAR + "06"); break;
                case "07": DT.Rows.Add("JULY - " + YEAR, YEAR + "07"); break;
                case "08": DT.Rows.Add("AUGUST - " + YEAR, YEAR + "08"); break;
                case "09": DT.Rows.Add("SEPTEMBER - " + YEAR, YEAR + "09"); break;
                case "10": DT.Rows.Add("OCTOBER - " + YEAR, YEAR + "10"); break;
                case "11": DT.Rows.Add("NOVEMBER - " + YEAR, YEAR + "11"); break;
                case "12": DT.Rows.Add("DECEMBER - " + YEAR, YEAR + "12"); break;
            }
        }
        DT.Rows.Add("ALL MONTH", "%");

        CMB_MONTH.DataSource = DT;
        CMB_MONTH.DataBind();

        CMB_MONTH.SelectedIndex = CMB_MONTH.Items.Count - 1;

    }

    public void CLEAR_FIELDS()
    {
        // ' CMB_DOCTYP.SelectedIndex = -1

        CMB_LEDGER.DataBind();
        if (CHBX_EDIT.Checked == true)
        {

            //   '  CMB_FKM_CD.SelectedIndex = -1
            TXT_BTRX_REFDESC.Text = "";
            TXT_BTRX_ISSDT.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
            CMB_BTRX_BANK_NAME.SelectedIndex = 0;
            TXT_BTRX_BANK_ACC.Text = "";
            CMB_BTRXCDTYP.SelectedIndex = 0;
            CMB_LEDGER.SelectedIndex = 0;
            TXT_BTRX_VALUEDT.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
            TXT_BTRX_VALUEAMT.Text = "";
            TXT_BTRX_VALUEAMT_OLD.Text = "";
            TXT_BTRX_NARR.Text = "";
            CMB_BTRX_CURRCD.SelectedIndex = 0;
            CMB_BTRX_STATUS.SelectedIndex = 0;

            TXT_BTRX_REFSRL.Text = NEXT_REC_SRL();
            TXT_BTRX_REFSRL.Visible = false;
            BTN_ADD.Visible = false;
            CMB_BTRX_BANK_NAME.Enabled = false;
            CMB_BTRX_REFSRL.SelectedIndex = -1;
            CMB_BTRX_REFSRL.Visible = true;
            BTN_UPDT.Visible = true;
        }
        else
        {
            // ' CMB_FKM_CD.SelectedIndex = -1
            TXT_BTRX_REFDESC.Text = "";
            TXT_BTRX_ISSDT.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
            CMB_BTRX_BANK_NAME.SelectedIndex = 0;
            TXT_BTRX_BANK_ACC.Text = "";
            CMB_BTRXCDTYP.SelectedIndex = 0;
            CMB_LEDGER.SelectedIndex = 0;
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
        BTN_DELETE.Visible = false;
        GET_GRID_INFO();
    }

    protected void BTN_CLEAR_Click(object sender, System.EventArgs e) //Handles BTN_CLEAR.Click
    {
        CLEAR_FIELDS();
    }

    protected void CHBX_EDIT_CheckedChanged(object sender, System.EventArgs e)// Handles CHBX_EDIT.CheckedChanged
    {
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
       // ' Dim COMP_CD As string = Session("USER_COMP_CD")
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
                        BANK_NAME = CMB_BTRX_BANK_NAME.SelectedItem.Text.ToUpper();
                        BANK_BALAMT = GET_BANK_BALANCE( CMB_BTRX_BANK_NAME.SelectedValue );

                        BANK_BALAMT = BANK_BALAMT - BTRX_VALUEAMT + BTRX_VALUEAMT_OLD;

                        UPD_BANK_QRY = "UPDATE BANK_INFO SET BNK_BALANCE_AMT = '" + BANK_BALAMT + "', BNK_LST_DEBIT_DT = '" + UPDDT +
                                               "', BNK_UPDBY = '" + USR + "', BNK_UPDDATE = '" + UPDDT + "', BNK_UPDTIME = '" + UPDTIME + "' " +
                                               " WHERE BNK_REFSRL= '" + CMB_BTRX_BANK_NAME.SelectedValue + "' ";
                    }

                    if( CMB_BTRXCDTYP.SelectedIndex == 1 ){
                        BANK_NAME = CMB_BTRX_BANK_NAME.SelectedItem.Text.ToUpper();
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
                                                   "','','" + CMB_BTRXCDTYP.SelectedValue.ToUpper() + "','" + CMB_BTRXTYP.SelectedValue.ToUpper() +
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
                     BANK_NAME = CMB_BTRX_BANK_NAME.SelectedItem.Text.ToUpper();
                     BANK_BALAMT = GET_BANK_BALANCE(CMB_BTRX_BANK_NAME.SelectedValue);

                     BANK_BALAMT = BANK_BALAMT - BTRX_VALUEAMT + BTRX_VALUEAMT_OLD;

                     UPD_BANK_QRY = "UPDATE BANK_INFO SET BNK_BALANCE_AMT = '" + BANK_BALAMT + "', BNK_LST_DEBIT_DT = '" + UPDDT +
                                            "', BNK_UPDBY = '" + USR + "', BNK_UPDDATE = '" + UPDDT + "', BNK_UPDTIME = '" + UPDTIME + "' " +
                                            " WHERE BNK_REFSRL= '" + CMB_BTRX_BANK_NAME.SelectedValue + "' ";
                 }

                 if (CMB_BTRXCDTYP.SelectedIndex == 1)
                 {
                     BANK_NAME = CMB_BTRX_BANK_NAME.SelectedItem.Text.ToUpper();
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

                 string UPDQRY = "UPDATE BANK_TRX SET BTRX_ISSDT = '" + TXT_BTRX_ISSDT.Text.ToUpper() +
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
     protected void BTN_DELETE_Click(object sender, System.EventArgs e)  // Handles BTN_ADD.Click
     {  string USR = User.Identity.Name.ToUpper();
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
                 //' Dim UPD_CREDIT_QRY As string = ""


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
                     BANK_NAME = CMB_BTRX_BANK_NAME.SelectedItem.Text.ToUpper();
                     BANK_BALAMT = GET_BANK_BALANCE(CMB_BTRX_BANK_NAME.SelectedValue);

                     BANK_BALAMT = BANK_BALAMT + BTRX_VALUEAMT_OLD;//- BTRX_VALUEAMT

                     UPD_BANK_QRY = "UPDATE BANK_INFO SET BNK_BALANCE_AMT = '" + BANK_BALAMT + "', BNK_LST_DEBIT_DT = '" + UPDDT +
                                            "', BNK_UPDBY = '" + USR + "', BNK_UPDDATE = '" + UPDDT + "', BNK_UPDTIME = '" + UPDTIME + "' " +
                                            " WHERE BNK_REFSRL= '" + CMB_BTRX_BANK_NAME.SelectedValue + "' ";
                 }

                 if (CMB_BTRXCDTYP.SelectedIndex == 1)
                 {
                     BANK_NAME = CMB_BTRX_BANK_NAME.SelectedItem.Text.ToUpper();
                     BANK_BALAMT = GET_BANK_BALANCE(CMB_BTRX_BANK_NAME.SelectedValue);

                     BANK_BALAMT = BANK_BALAMT - BTRX_VALUEAMT_OLD; //+ BTRX_VALUEAMT

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
                         BANK_BALAMT = BANK_BALAMT + BTRX_VALUEAMT_OLD;
                         UPD_BANK_QRY = "UPDATE BANK_INFO SET BNK_BALANCE_AMT = '" + BANK_BALAMT + "', BNK_LST_DEBIT_DT = '" + UPDDT +
                                                "', BNK_UPDBY = '" + USR + "', BNK_UPDDATE = '" + UPDDT + "', BNK_UPDTIME = '" + UPDTIME + "' " +
                                                " WHERE BNK_REFSRL= '" + CMB_BTRX_BANK_NAME.SelectedValue + "' ";
                     }
                     else
                     {
                         BANK_BALAMT = BANK_BALAMT - BTRX_VALUEAMT_OLD;
                         UPD_BANK_QRY = "UPDATE BANK_INFO SET BNK_BALANCE_AMT = '" + BANK_BALAMT + "', BNK_LST_CREDIT_DT = '" + UPDDT +
                                                "', BNK_UPDBY = '" + USR + "', BNK_UPDDATE = '" + UPDDT + "', BNK_UPDTIME = '" + UPDTIME + "' " +
                                                " WHERE BNK_REFSRL= '" + CMB_BTRX_BANK_NAME.SelectedValue + "' ";
                     }
                     BTRX_VALUEAMT = 0;
                 }

                 //string UPDQRY = "UPDATE BANK_TRX SET BTRX_ISSDT = '" + TXT_BTRX_ISSDT.Text.ToUpper() +
                 //                       "', BTRX_VALUEDT  = '" + TXT_BTRX_VALUEDT.Text.ToUpper() + "',BTRX_REFDESC = '" + TXT_BTRX_REFDESC.Text.ToUpper() +
                 //                       "', BTRX_NARR = '" + TXT_BTRX_NARR.Text.ToUpper() + "',BTRX_CURRCD = '" + CMB_BTRX_CURRCD.SelectedValue.ToUpper() +
                 //                       "', BTRX_VALUEAMT = '" + BTRX_VALUEAMT + "', BTRX_BAL_AMT = '" + BANK_BALAMT +
                 //                       "', BTRX_BANK_NAME = '" + BANK_NAME + "',BTRX_BANK_ACC = '" + TXT_BTRX_BANK_ACC.Text.ToUpper() +
                 //                       "', BTRX_STATUS = '" + CMB_BTRX_STATUS.SelectedValue + "', BTRX_UPDBY = '" + USR + "', BTRX_UPDDATE = '" + UPDDT + "', BTRX_UPDTIME = '" + UPDTIME + "' " +
                 //                       " WHERE BTRX_REFSRL= '" + CODE + "'";

                 string UPDQRY = "DELETE FROM BANK_TRX " + " WHERE BTRX_REFSRL= '" + CODE + "'";


                 if (dbo.Transact(UPDQRY, UPD_BANK_QRY, "", "", "") == false)
                 {
                     mbox("ERROR !!");
                 }
                 else
                 {
                     // 'GridView1.DataBind()a
                     mbox("CODE : '" + CODE + "'      UPLOADED SUCCESSFULLY !!");
                     BTN_CLEAR_Click(sender, e);
                 }
             }
         }
         catch (Exception EX)
         {
             mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
         }
     
     }

    protected string NEXT_REC_SRL()
    {
        string SRL = "";
        DataTable DTINVINF = new DataTable();
        try
        {
            string RETRVQRY = "SELECT MAX (BTRX_REFSRL) FROM BANK_TRX  WHERE BTRX_REFSRL LIKE '" + System.DateTime.Today.Year + "%' ";
            DTINVINF = dbo.SelTable(RETRVQRY);
            if (DTINVINF.Rows.Count > 0)
            {
                if (DTINVINF.Rows[0][0].ToString().Length !=0)
                {
                    decimal NSRL = decimal.Parse(DTINVINF.Rows[0][0].ToString().Substring(4));

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

    public void GET_REFSRL_DETAILS(ref string CODE)
    {
        // string CODE1   = CMB_BTRX_REFSRL.SelectedValue;
        CHBX_EDIT.Checked = true;

        CLEAR_FIELDS();
        try
        {
            string RETRVQRY = "SELECT * FROM BANK_TRX WHERE BTRX_REFSRL = '" + CODE + "' ";
            DataTable dtinfo = dbo.SelTable(RETRVQRY);
            if (dtinfo.Rows.Count == 1)
            {


                CMB_BTRXTYP.SelectedIndex = 0;// ' CMB_BTRXTYP.Items.IndexOf(CMB_BTRXTYP.Items.FindByValue(dtinfo.Rows[0]("BTRX_TYPE").ToString.Trim))
                LBL_CAPTION.Text = CMB_BTRXTYP.SelectedItem.Text;
                 
                INIT_BANK_TRX_SRL();
                INIT_BANK_NAME();

                CMB_BTRX_REFSRL.SelectedIndex = CMB_BTRX_REFSRL.Items.IndexOf(CMB_BTRX_REFSRL.Items.FindByValue(CODE));

                //' CMB_FKM_CD.SelectedIndex = CMB_FKM_CD.Items.IndexOf(CMB_FKM_CD.Items.FindByValue(dtinfo.Rows[0]("BTRX_FKMREF").ToString))
                CMB_LEDGER.SelectedIndex = CMB_LEDGER.Items.IndexOf(CMB_LEDGER.Items.FindByValue(dtinfo.Rows[0]["BTRX_LEDGER"].ToString()));
                TXT_BTRX_REFDESC.Text = dtinfo.Rows[0]["BTRX_REFDESC"].ToString().Trim();
                TXT_BTRX_ISSDT.Text = dtinfo.Rows[0]["BTRX_ISSDT"].ToString().Trim();
                CMB_BTRXCDTYP.SelectedIndex = CMB_BTRXCDTYP.Items.IndexOf(CMB_BTRXCDTYP.Items.FindByValue(dtinfo.Rows[0]["BTRX_CD_TYPE"].ToString().Trim()));
                CMB_BTRX_BANK_NAME.SelectedIndex = CMB_BTRX_BANK_NAME.Items.IndexOf(CMB_BTRX_BANK_NAME.Items.FindByText(dtinfo.Rows[0]["BTRX_BANK_NAME"].ToString()));
                TXT_BTRX_BANK_ACC.Text = dtinfo.Rows[0]["BTRX_BANK_ACC"].ToString().Trim();
                //'CMB_BTRX_CREDIT_NAME.SelectedIndex = CMB_BTRX_CREDIT_NAME.Items.IndexOf(CMB_BTRX_CREDIT_NAME.Items.FindByText(dtinfo.Rows[0]("BTRX_CREDIT_NAME").ToString))
                //'TXT_BTRX_CREDIT_ACC.Text = dtinfo.Rows[0]("BTRX_CREDIT_ACC").ToString.Trim
                CMB_BTRX_CURRCD.SelectedIndex = CMB_BTRX_CURRCD.Items.IndexOf(CMB_BTRX_CURRCD.Items.FindByValue(dtinfo.Rows[0]["BTRX_CURRCD"].ToString().Trim()));
                TXT_BTRX_VALUEDT.Text = dtinfo.Rows[0]["BTRX_VALUEDT"].ToString().Trim();
                TXT_BTRX_VALUEAMT.Text = string.Format("{0:#,##0.00}", dtinfo.Rows[0]["BTRX_VALUEAMT"]);//' dtinfo.Rows[0]("BTRX_VALUEAMT").ToString.Trim
                TXT_BTRX_VALUEAMT_OLD.Text = string.Format("{0:#,##0.00}", dtinfo.Rows[0]["BTRX_VALUEAMT"]);// 'dtinfo.Rows[0]("BTRX_VALUEAMT").ToString.Trim
                TXT_BTRX_NARR.Text = dtinfo.Rows[0]["BTRX_NARR"].ToString().Trim();

                CMB_BTRX_STATUS.SelectedIndex = CMB_BTRX_STATUS.Items.IndexOf(CMB_BTRX_STATUS.Items.FindByValue(dtinfo.Rows[0]["BTRX_STATUS"].ToString().Trim()));

                TXT_BTRX_REFSRL.Text = CODE;
            }
            else
            {
                CLEAR_FIELDS();
                //'BTN_CLEAR_Click(sender, e)
                mbox("ERROR !!");
            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }
    }

    protected void CMB_BTRX_REFSRL_SelectedIndexChanged(object sender, System.EventArgs e)// Handles CMB_BTRX_REFSRL.SelectedIndexChanged
    {
        string CODE = CMB_BTRX_REFSRL.SelectedValue;
        GET_REFSRL_DETAILS(ref   CODE);

        //CLEAR_FIELDS();
        //try
        //{
        //    string RETRVQRY = "SELECT * FROM BANK_TRX WHERE BTRX_REFSRL = '" + CODE + "' ";
        //    DataTable dtinfo = dbo.SelTable(RETRVQRY);
        //    if (dtinfo.Rows.Count == 1)
        //    {


        //        CMB_BTRXTYP.SelectedIndex = 0;// ' CMB_BTRXTYP.Items.IndexOf(CMB_BTRXTYP.Items.FindByValue(dtinfo.Rows[0]("BTRX_TYPE").ToString.Trim))
        //        LBL_CAPTION.Text = CMB_BTRXTYP.SelectedItem.Text;

        //        INIT_BANK_TRX_SRL();
        //        INIT_BANK_NAME();

        //        CMB_BTRX_REFSRL.SelectedIndex = CMB_BTRX_REFSRL.Items.IndexOf(CMB_BTRX_REFSRL.Items.FindByValue(CODE));

        //        //' CMB_FKM_CD.SelectedIndex = CMB_FKM_CD.Items.IndexOf(CMB_FKM_CD.Items.FindByValue(dtinfo.Rows[0]("BTRX_FKMREF").ToString))
        //        TXT_BTRX_REFDESC.Text = dtinfo.Rows[0]["BTRX_REFDESC"].ToString().Trim();
        //        TXT_BTRX_ISSDT.Text = dtinfo.Rows[0]["BTRX_ISSDT"].ToString().Trim();
        //        CMB_BTRXCDTYP.SelectedIndex = CMB_BTRXCDTYP.Items.IndexOf(CMB_BTRXCDTYP.Items.FindByValue(dtinfo.Rows[0]["BTRX_CD_TYPE"].ToString().Trim()));
        //        CMB_BTRX_BANK_NAME.SelectedIndex = CMB_BTRX_BANK_NAME.Items.IndexOf(CMB_BTRX_BANK_NAME.Items.FindByText(dtinfo.Rows[0]["BTRX_BANK_NAME"].ToString()));
        //        TXT_BTRX_BANK_ACC.Text = dtinfo.Rows[0]["BTRX_BANK_ACC"].ToString().Trim();
        //        //'CMB_BTRX_CREDIT_NAME.SelectedIndex = CMB_BTRX_CREDIT_NAME.Items.IndexOf(CMB_BTRX_CREDIT_NAME.Items.FindByText(dtinfo.Rows[0]("BTRX_CREDIT_NAME").ToString))
        //        //'TXT_BTRX_CREDIT_ACC.Text = dtinfo.Rows[0]("BTRX_CREDIT_ACC").ToString.Trim
        //        CMB_BTRX_CURRCD.SelectedIndex = CMB_BTRX_CURRCD.Items.IndexOf(CMB_BTRX_CURRCD.Items.FindByValue(dtinfo.Rows[0]["BTRX_CURRCD"].ToString().Trim()));
        //        TXT_BTRX_VALUEDT.Text = dtinfo.Rows[0]["BTRX_VALUEDT"].ToString().Trim();
        //        TXT_BTRX_VALUEAMT.Text = string.Format("{0:#,##0.00}", dtinfo.Rows[0]["BTRX_VALUEAMT"]);//' dtinfo.Rows[0]("BTRX_VALUEAMT").ToString.Trim
        //        TXT_BTRX_VALUEAMT_OLD.Text = string.Format("{0:#,##0.00}", dtinfo.Rows[0]["BTRX_VALUEAMT"]);// 'dtinfo.Rows[0]("BTRX_VALUEAMT").ToString.Trim
        //        TXT_BTRX_NARR.Text = dtinfo.Rows[0]["BTRX_NARR"].ToString().Trim();

        //        CMB_BTRX_STATUS.SelectedIndex = CMB_BTRX_STATUS.Items.IndexOf(CMB_BTRX_STATUS.Items.FindByValue(dtinfo.Rows[0]["BTRX_STATUS"].ToString().Trim()));

        //        TXT_BTRX_REFSRL.Text = CODE;
        //    }
        //    else
        //    {
        //        CLEAR_FIELDS();
        //        //'BTN_CLEAR_Click(sender, e)
        //        mbox("ERROR !!");
        //    }
        //}
        //catch (Exception EX)
        //{
        //    mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        //}
    }

    public void mbox(string val)
    {
        //string cs   = "c";
        //string cst  = "<script type=""text/javascript"">" + "alert('" + val + "');</" + "script>";
        //Page page    = HttpContext.Current.Handler;
        //page.ClientScript.RegisterStartupScript(Me.typeof, cs, cst);

        string USR = User.Identity.Name.ToUpper();
        dbo.ErrorLog(USR + " : PETTY_TRX.aspx : " + val, Server.MapPath("~\\Logs\\ErrorLog"));
    }

    protected void INIT_BANK_NAME()
    {
        string CODE = CMB_BTRXTYP.SelectedValue.Substring(0, 1);
        // 'Dim COMP_CD As string = Session("USER_COMP_CD")
        string RETRVQRY = "SELECT DISTINCT BNK_REFSRL, BNK_NAME  FROM BANK_INFO  WHERE BNK_TYPE IN ('" + CODE + "')  ORDER BY BNK_REFSRL";
        DataTable dtinfo = dbo.SelTable(RETRVQRY);


        // 'dtinfo.Rows.Add("%", "ALL PROJECTS")

        CMB_BTRX_BANK_NAME.DataSource = dtinfo;
        CMB_BTRX_BANK_NAME.DataBind();

        CMB_BTRX_BANK_NAME.SelectedIndex = CMB_BTRX_BANK_NAME.Items.Count - 1;
    }

    protected decimal GET_BANK_BALANCE( string BNK_CODE)
    {
        decimal BNK_BALANCE_AMT = 0;
        try
        {
            // 'Dim COMP_CD As string = Session("USER_COMP_CD")
            string RETRVQRY = "SELECT * FROM BANK_INFO WHERE   BNK_REFSRL = '" + BNK_CODE + "' ";
            DataTable dtinfo = dbo.SelTable(RETRVQRY);
            if (dtinfo.Rows.Count == 1)
            {
                if (dtinfo.Rows[0]["BNK_BALANCE_AMT"].ToString().Length > 0)
                {
                    BNK_BALANCE_AMT = (decimal)dtinfo.Rows[0]["BNK_BALANCE_AMT"];
                }
            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }
        return BNK_BALANCE_AMT;
    }

    public void TXTSRCH_TextChanged(object sender, System.EventArgs e)
    {
        GET_GRID_INFO();
    }
    protected void GET_GRID_INFO()
    {
        string BTRXTYPE = CMB_BTRXTYP.SelectedValue;
        string CURRCD = CMB_BTRX_CURRCD.SelectedValue;
       // string FKMREF = "";
        string MNT_CD = CMB_MONTH.SelectedValue;
        //string FRMDT = "";
        //string TODT = "";

        
string searchtxt = TXT_SRCH.Text.ToUpper();


        if (searchtxt.Length > 0)
        {
            searchtxt = " AND (BTRX_NARR LIKE '%" + searchtxt + "%' OR BTRX_LEDGER LIKE '%" + searchtxt + "%') ";
        }


        if (MNT_CD.Length > 0)
        {
            MNT_CD = " AND SUBSTRING(BTRX_VALUEDT, 7, 4) + SUBSTRING(BTRX_VALUEDT, 4, 2) LIKE '" + MNT_CD + "' ";
        }


        try
        {
            // ' Dim COMP_CD As string = Session("USER_COMP_CD")
            string RETRVQRY = "SELECT * FROM BANK_TRX WHERE BTRX_TYPE LIKE '" + BTRXTYPE + "' AND BTRX_CURRCD  LIKE '" + CURRCD + "'  " +
                                     MNT_CD + searchtxt + "  ORDER BY BTRX_REFSRL DESC";
            //'AND BTRX_STATUS IN ('V')
            DataTable dtinfo = dbo.SelTable(RETRVQRY);
            if (dtinfo.Rows.Count > 0)
            {
                dtinfo.Columns.Add("BTRX_VALUEAMTC",typeof(decimal));
                dtinfo.Columns.Add("BTRX_VALUEAMTD", typeof(decimal));
                dtinfo.Columns.Add("STATUS");
                dtinfo.Columns.Add("FOOTER");

                foreach (DataRow DR in dtinfo.Rows)
                {
                    DR["BTRX_VALUEAMTC"] = 0;
                    DR["BTRX_VALUEAMTD"] = 0;
                    if (DR["BTRX_CD_TYPE"].ToString().Substring(0, 1) == "D")
                    {
                        DR["BTRX_VALUEAMTD"] = DR["BTRX_VALUEAMT"];
                    }
                    else if (DR["BTRX_CD_TYPE"].ToString().Substring(0, 1) == "C")
                    {
                        DR["BTRX_VALUEAMTC"] = DR["BTRX_VALUEAMT"];
                        DR["BTRX_VALUEAMT"] = 0;
                    }

                    DR["STATUS"] = CMB_BTRX_STATUS.Items.FindByValue(DR["BTRX_STATUS"].ToString().Trim()).Text;
                    DR["FOOTER"] = User.Identity.Name.ToUpper();
                }

                GridView1.DataSource = dtinfo;
                GridView1.DataBind();
            }
            else
            {

                GridView1.DataSource = null;
                GridView1.DataBind();
            }
            Session["PETTYTRX_dtINFO_REPORT"] = dtinfo;  
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
            GET_REFSRL_DETAILS(ref   CODE);
        }
    }
    protected void GetSelectedRecords()
    {
        DataTable dt = new DataTable();

        DataTable dtinfo = (DataTable)Session["PETTYTRX_dtINFO_REPORT"];
        dt = dtinfo.Clone();
        dt.Rows.Clear();
        foreach (GridViewRow row in GridView1.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (CheckBox)(row.Cells[0].FindControl("chkRow"));
                if (chkRow.Checked)
                {
                    string BTRX_REFSRL = row.Cells[2].Text.Replace("+nbsp;", "");
                    //AND BTRX_CD_TYPE LIKE 'DEBIT' 
                    DataRow[] DR = dtinfo.Select(" BTRX_REFSRL = '" + BTRX_REFSRL + "' AND BTRX_STATUS IN ('V')  ");
                    if (DR.Length > 0)
                    {
                        DR[0]["BTRX_NARR"] = DR[0]["BTRX_LEDGER"];
                        dt.ImportRow(DR[0]);
                    }
                }
            }
        }

        ReportDocument rpt = new ReportDocument();
        string rname = Server.MapPath("~/Reports/PETTYTRX_RPT.rpt");

        string dwnldfname = Server.MapPath("~/ReportsGenerate/PETTYTRX_RPT.PDF");
        // '  Dim dt As DataTable = TryCast(Session("NAVRPT_dt_NAVRPT_REPORT"), DataTable)

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
            Response.AddHeader("content-disposition", "attachment;filename =" + "PETTYTRX_RPT.PDF");
            Response.ContentType = "application/pdf";
            Response.TransmitFile(dwnldfname);

        }
    }

    protected void Button1_Click(object sender, System.EventArgs e) //Handles Button1.Click
    {
        GetSelectedRecords();
    }

    protected void CMB_MONTH_SelectedIndexChanged(object sender, System.EventArgs e) //Handles CMB_MONTH.SelectedIndexChanged
    {
        GET_GRID_INFO();
    }
    //protected void Gridview1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    int CODE = e.RowIndex;
    //    GridView1.DeleteRow(CODE);
    //    //GET_REFSRL_DETAILS(ref   CODE);
    //    GridView1.DataBind();
    //}
    protected void Gridview1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        if (GridView1.SelectedDataKey[0].ToString().Trim().Length != 0)
        {
            int index = Convert.ToInt32(e.RowIndex);
            DataTable dt = Session["PETTYTRX_dtINFO_REPORT"]  as DataTable;
            string CODE = dt.Rows[index]["BTRX_REFSRL"].ToString();
            GET_REFSRL_DETAILS(ref   CODE);

            BTN_UPDT.Visible = false;
            BTN_DELETE.Visible = true;

        }
    }
    //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        string item = e.Row.Cells[2].Text;
    //        Button button = (Button) e.Row.Cells[9].Controls  ;
    //        //foreach (Button button in e.Row.Cells[9].Controls)
    //       // {
    //            if (button.CommandName == "Delete")
    //            {
    //                button.Attributes["onclick"] = "if(!confirm('Do you want to delete " + item + "?')){ return false; };";
    //            }
    //        //}
    //    }
    //}
}
