using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class Tools_BANK_MTNC : Page
{
    fkminvcom dbo = new fkminvcom();
    // Visual C# .NET requires that you override the OnInit function,
    // adding a new delegate for the Page_Load event.

    override protected void OnInit(System.EventArgs e)
    {
        this.Load += new System.EventHandler(this.Page_Load);
        base.OnInit(e);
         CMB_BNK_NAME.SelectedIndexChanged += new System.EventHandler(CMB_BNK_NAME_SelectedIndexChanged);
        CMB_BNK_REFSRL.SelectedIndexChanged += new System.EventHandler(CMB_BNK_REFSRL_SelectedIndexChanged);
        //UploadButton.Click += new System.EventHandler(UploadButton_Click1); 
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

    public void Page_Load(object sender, System.EventArgs e)
    {

        if (!IsPostBack && !IsCallback)
        {
            CLEAR_FIELDS();
            CHBX_EDIT.Checked = false;
            INIT_BANK_SRL();

            if (Request.QueryString.Get("REFSRL") != null)
            {
                String REFCODE = Request.QueryString.Get("REFSRL");
                CHBX_EDIT.Checked = true;

                CMB_BNK_REFSRL.SelectedIndex = CMB_BNK_REFSRL.Items.IndexOf(CMB_BNK_REFSRL.Items.FindByValue(REFCODE));
                CMB_BNK_REFSRL_Changed(REFCODE);
            }
        }
    }

    public void INIT_BANK_SRL()
    {

        // ' Dim COMP_CD As string = Session("USER_COMP_CD")
        string RETRVQRY = "SELECT DISTINCT BNK_REFSRL,BNK_REFSRL + '-' + BNK_NAME AS BNK_NAME  FROM BANK_INFO ORDER BY BNK_NAME";
        // 'BTRX_TYPE IN ('B','I','H','P') 
        DataTable dtinfo = dbo.SelTable(RETRVQRY);
        CMB_BNK_REFSRL.DataSource = dtinfo;
        CMB_BNK_REFSRL.DataBind();
        CMB_BNK_REFSRL.SelectedIndex = -1;
    }
    public void CLEAR_FIELDS()
    {
        // ' CMB_DOCTYP.SelectedIndex = -1


        if (CHBX_EDIT.Checked == true)
        {
            CMB_BNK_TYPE.SelectedIndex = 0;
            TXT_BNK_NAME.Text = "";
            // ' TXT_BNK_NAME.ReadOnly = true;
            TXT_BNK_ACC_NO.Text = "";
            TXT_BNK_IBAN_NO.Text = "";
            TXT_BNK_DESC.Text = "";
            TXT_BNK_OPN_BALANCE.Text = "0.00";
            // 'TXT_BNK_OPN_BALANCE.ReadOnly = true;
            CMB_BNK_CURRCD.SelectedIndex = 0;
            TXT_BNK_BALANCE_AMT.Text = "0.00";
            TXT_BNK_ADDRESS.Text = "";
            TXT_BNK_TELEPHONE.Text = "";
            TXT_BNK_WEBSITE.Text = "";
            TXT_BNK_LST_CDT.Text = "";
            TXT_BNK_LST_DDT.Text = "";

            TXT_BNK_REFSRL.Text = "";
            TXT_BNK_REFSRL.Visible = false;
            BTN_ADD.Visible = false;
            CMB_BNK_REFSRL.SelectedIndex = -1;
            CMB_BNK_REFSRL.Visible = true;
            BTN_UPDT.Visible = true;
            //CMB_BNK_NAME.SelectedIndex = -1;
            //CMB_BNK_NAME.DataBind();
        }
        else
        {
            CMB_BNK_TYPE.SelectedIndex = 0;
            TXT_BNK_NAME.Text = "";
            //  '  TXT_BNK_NAME.ReadOnly = false
            TXT_BNK_ACC_NO.Text = "";
            TXT_BNK_IBAN_NO.Text = "";
            TXT_BNK_DESC.Text = "";
            TXT_BNK_OPN_BALANCE.Text = "0.00";
            //'TXT_BNK_OPN_BALANCE.ReadOnly = false;
            CMB_BNK_CURRCD.SelectedIndex = 0;
            TXT_BNK_BALANCE_AMT.Text = "0.00";
            TXT_BNK_ADDRESS.Text = "";
            TXT_BNK_TELEPHONE.Text = "";
            TXT_BNK_WEBSITE.Text = "";
            TXT_BNK_LST_CDT.Text = "";
            TXT_BNK_LST_DDT.Text = "";

            TXT_BNK_REFSRL.Text = NEXT_REC_SRL();
            TXT_BNK_REFSRL.Visible = true;
            BTN_ADD.Visible = true;
            CMB_BNK_REFSRL.SelectedIndex = -1;
            CMB_BNK_REFSRL.Visible = false;
            BTN_UPDT.Visible = false;

            //CMB_BNK_NAME.SelectedIndex = -1;
            //CMB_BNK_NAME.DataBind();
        }
        //  BTN_DELETE.Visible = false;
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


    protected void BTN_ADD_Click(object sender, System.EventArgs e)
    {
        //Handles BTN_ADD.Click

               if( CMB_BNK_TYPE.SelectedIndex < 0 ){
                    mbox("PLEASE SELECT THE DOCUMENT TYPE !!");
                }
                String USR   = User.Identity.Name.ToUpper();
                String UPDDT   = System.DateTime.Today.ToString("dd/MM/yyyy");
                String UPDTIME    = System.DateTime.Now.ToString("hh:mm:ss");
                String CODE   = TXT_BNK_REFSRL.Text.ToUpper();
                String SRL  = NEXT_REC_SRL();
                try
                {
                    //'Dim COMP_CD As String = Session("USER_COMP_CD")
                    String RETRVQRY = "SELECT * FROM BANK_INFO WHERE BNK_REFSRL = '" + CODE + "' AND BNK_NAME = '" + TXT_BNK_NAME.Text.ToUpper() + "' ";
                   if( dbo.RecExist(RETRVQRY) == false ){


                       String STR_TEMP = "0";
                            //'If TXT_BNK_OPN_BALANCE.Text.Trim.Length > 0 ){
                            //'    STR_TEMP = TXT_BNK_OPN_BALANCE.Text.Trim
                            //'}
                          //  Decimal BNK_OPN_BALANCE  = 0 ;//'CDec(STR_TEMP)

                            STR_TEMP = "0";
                           if( TXT_BNK_BALANCE_AMT.Text.Trim().Length > 0 ){
                                STR_TEMP = TXT_BNK_BALANCE_AMT.Text.Trim();
                            }
                            Decimal BNK_BALANCE_AMT   = decimal.Parse(STR_TEMP);

                            String INSQRY   = "INSERT INTO BANK_INFO VALUES('','" + SRL + "','" + TXT_BNK_NAME.Text.ToUpper() + "','" + TXT_BNK_NAME.Text.ToUpper() + "','" + TXT_BNK_IBAN_NO.Text.ToUpper() +
                                                           "','" + TXT_BNK_ACC_NO.Text.ToUpper() + "','" + CMB_BNK_CURRCD.SelectedValue.ToUpper() + "','" + BNK_BALANCE_AMT +
                                                           "','" + BNK_BALANCE_AMT + "','" + TXT_BNK_DESC.Text.ToUpper() +
                                                           "','" + CMB_BNK_TYPE.SelectedValue.ToUpper() + "','" + TXT_BNK_ADDRESS.Text.ToUpper() +
                                                           "','" + TXT_BNK_TELEPHONE.Text.ToUpper() + "','" + TXT_BNK_WEBSITE.Text.ToUpper() +
                                                           "','" + TXT_BNK_LST_CDT.Text.ToUpper() + "','" + TXT_BNK_LST_DDT.Text.ToUpper() +
                                                           "','" + USR + "','" + UPDDT + "','" + USR + "','" + UPDDT + "','" + UPDTIME + "')";

                           if( dbo.InsRecord(INSQRY) == false ){
                                mbox("ERROR !!");
                            }else{
                              //  'GridView1.DataBind()
                                mbox("CODE : '" + TXT_BNK_NAME.Text.ToUpper() + "'      UPLOADED SUCCESSFULLY !!");
                                CLEAR_FIELDS();
                                INIT_BANK_SRL();
                            } 
                    }else{
                      //  'TXT_SRL.Text = NEXT_REC_SRL()
                        mbox("PLEASE TRY AGAIN .. ALREADY FILE EXIST FOR : " + TXT_BNK_NAME.Text.ToUpper() + "!!");

                    }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }

    }

    protected void BTN_UPDT_Click(object sender, System.EventArgs e)
    {
        //Handles BTN_UPDT.Click
           String USR   = User.Identity.Name.ToUpper();
                String UPDDT   = System.DateTime.Today.ToString("dd/MM/yyyy");
                String UPDTIME    = System.DateTime.Now.ToString("hh:mm:ss");
                String CODE = CMB_BNK_REFSRL.SelectedValue;
       
               if( CMB_BNK_TYPE.SelectedIndex < 0 ){
                   mbox("PLEASE SELECT THE DOCUMENT TYPE !!");
                }

                try
        {
                   // ' Dim COMP_CD As String = Session("USER_COMP_CD")
                    String RETRVQRY   = "SELECT * FROM BANK_INFO WHERE BNK_REFSRL = '" + CODE + "' ";

                   if( dbo.RecExist(RETRVQRY) == true ){
                        
                            
                       String STR_TEMP = "0";
                            //'If TXT_BNK_OPN_BALANCE.Text.Trim.Length > 0 ){
                            //'    STR_TEMP = TXT_BNK_OPN_BALANCE.Text.Trim
                            //'}
                            //'Dim BNK_OPN_BALANCE As Double = CDec(STR_TEMP)

                            STR_TEMP = "0";
                           if( TXT_BNK_BALANCE_AMT.Text.Trim().Length > 0 ){
                                STR_TEMP = TXT_BNK_BALANCE_AMT.Text.Trim();
                            }
                            Decimal BNK_BALANCE_AMT   = decimal.Parse(STR_TEMP);

                            String UPDQRY = "UPDATE BANK_INFO SET BNK_IBAN_NO = '" + TXT_BNK_IBAN_NO.Text.ToUpper() +
                                                   "', BNK_ACCOUNT_NO  = '" + TXT_BNK_ACC_NO.Text.ToUpper() + "',BNK_DESC = '" + TXT_BNK_DESC.Text.ToUpper() +
                                                   "', BNK_CURRCD = '" + CMB_BNK_CURRCD.Text.ToUpper() + "' ,  BNK_NAME = '" + TXT_BNK_NAME.Text.ToUpper() +
                                                   "', BNK_ADDRESS = '" + TXT_BNK_ADDRESS.Text.ToUpper() + "',BNK_TELEPHONE = '" + TXT_BNK_TELEPHONE.Text.ToUpper() +
                                                   "', BNK_WEBSITE = '" + TXT_BNK_WEBSITE.Text.ToUpper() + "', BNK_BALANCE_AMT  = '" + BNK_BALANCE_AMT +
                                                   "', BNK_UPDBY = '" + USR + "', BNK_UPDDATE = '" + UPDDT + "', BNK_UPDTIME = '" + UPDTIME + "' " +
                                                   " WHERE BNK_REFSRL= '" + CODE + "'   ";
                            //'
                            //'"', BNK_BALANCE_AMT  = '" + BNK_BALANCE_AMT + "',BNK_OPN_BALANCE = '" + BNK_OPN_BALANCE +


                           if( dbo.InsRecord(UPDQRY) == false ){
                                mbox("ERROR !!");
                            }else{
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

    protected string NEXT_REC_SRL()
    {
        string SRL = "";
        DataTable DTINVINF = new DataTable();
        try
        {
            string RETRVQRY = "SELECT MAX (BNK_REFSRL) FROM BANK_INFO  ";
            DTINVINF = dbo.SelTable(RETRVQRY);
            if (DTINVINF.Rows.Count > 0)
            {
                if (  DTINVINF.Rows[0][0].ToString().Length !=0)
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

    protected void CMB_BNK_REFSRL_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        //Handles CMB_BNK_REFSRL.SelectedIndexChanged

        String CODE = CMB_BNK_REFSRL.SelectedValue;
        CMB_BNK_REFSRL_Changed(CODE);

    }

    protected void GridView1_SelectedIndexChanged(object sender, System.EventArgs e)// Handles CMB_BTRX_REFSRL.SelectedIndexChanged
    {

        if (GridView1.SelectedDataKey[0].ToString().Trim().Length != 0)
        {
            string CODE = (string)GridView1.SelectedDataKey[0];
            CMB_BNK_REFSRL_Changed(CODE);
        }
    }

    protected void CMB_BNK_REFSRL_Changed(String CODE)
    {
        try
        {
            //    ' Dim COMP_CD As String = Session("USER_COMP_CD")
            String RETRVQRY = "SELECT * FROM BANK_INFO WHERE BNK_REFSRL = '" + CODE + "' ";
            DataTable dtinfo = dbo.SelTable(RETRVQRY);
            if (dtinfo.Rows.Count == 1)
            {
                CMB_BNK_CURRCD.DataBind();
                CMB_BNK_TYPE.SelectedIndex = CMB_BNK_TYPE.Items.IndexOf(CMB_BNK_TYPE.Items.FindByValue(dtinfo.Rows[0]["BNK_TYPE"].ToString()));
                CMB_BNK_CURRCD.SelectedIndex = CMB_BNK_CURRCD.Items.IndexOf(CMB_BNK_CURRCD.Items.FindByValue(dtinfo.Rows[0]["BNK_CURRCD"].ToString().Trim()));
                TXT_BNK_REFSRL.Text = CODE;
                TXT_BNK_NAME.Text = dtinfo.Rows[0]["BNK_NAME"].ToString().Trim();
                TXT_BNK_ACC_NO.Text = dtinfo.Rows[0]["BNK_ACCOUNT_NO"].ToString().Trim();
                TXT_BNK_IBAN_NO.Text = dtinfo.Rows[0]["BNK_IBAN_NO"].ToString().Trim();
                TXT_BNK_DESC.Text = dtinfo.Rows[0]["BNK_DESC"].ToString().Trim();
                TXT_BNK_OPN_BALANCE.Text = String.Format("{0:#,##0.00}", dtinfo.Rows[0]["BNK_OPN_BALANCE"]);// 'dtinfo.Rows[0]["BNK_OPN_BALANCE"].ToString().Trim()
                TXT_BNK_BALANCE_AMT.Text = String.Format("{0:#,##0.00}", dtinfo.Rows[0]["BNK_BALANCE_AMT"]);// 'dtinfo.Rows[0]["BNK_BALANCE_AMT"].ToString().Trim()
                TXT_BNK_ADDRESS.Text = dtinfo.Rows[0]["BNK_ADDRESS"].ToString().Trim();
                TXT_BNK_TELEPHONE.Text = dtinfo.Rows[0]["BNK_TELEPHONE"].ToString().Trim();
                TXT_BNK_WEBSITE.Text = dtinfo.Rows[0]["BNK_WEBSITE"].ToString().Trim();
                TXT_BNK_LST_CDT.Text = dtinfo.Rows[0]["BNK_LST_CREDIT_DT"].ToString().Trim();
                TXT_BNK_LST_DDT.Text = dtinfo.Rows[0]["BNK_LST_DEBIT_DT"].ToString().Trim();

            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }
    }


    protected void CMB_BNK_NAME_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        //Handles CMB_BNK_NAME.SelectedIndexChanged

        if (CMB_BNK_NAME.SelectedIndex > -1)
        {
            TXT_BNK_NAME.Text = CMB_BNK_NAME.SelectedItem.Text;
        }
    }

    protected void GET_GRID_INFO()
    {

        try
        {
            //  ' Dim COMP_CD As String = Session("USER_COMP_CD")
            String RETRVQRY = "SELECT BANK_INFO.*,RF_CODE as BNK_CURR_SMBL FROM (BANK_INFO LEFT JOIN FKM_REFRNC ON (RF_DESCRP=BNK_CURRCD AND RF_FEILDTYPE ='BNK_CURR') )";
            //'WHERE BTRX_TYPE LIKE '" + BTRXTYPE + "' AND BTRX_CURRCD  LIKE '" + CURRCD + "'  " +
            // FKMREF + BNK_CODE + FRMDT + TODT;
            DataTable dtinfo = dbo.SelTable(RETRVQRY);
            if (dtinfo.Rows.Count > 0)
            {
                //  'dtinfo.Columns.Add("BTRX_VALUEAMTC")
                dtinfo.Columns.Add("BNK_BALANCE_AMT_R");

                foreach (DataRow DR in dtinfo.Rows)
                {
                    DR["BNK_BALANCE_AMT_R"] = DR["BNK_CURR_SMBL"] + " " + String.Format("{0:#,##0}", DR["BNK_BALANCE_AMT"]);
                    //'}else{If DR("BTRX_CD_TYPE").ToString.Substring(0, 1) = "C" ){
                    //'    DR("BTRX_VALUEAMTC") = DR("BTRX_VALUEAMT")
                    //'}
                }

                GridView1.DataSource = dtinfo;
                GridView1.DataBind();
            }
            else
            {

                GridView1.DataSource = null;
                GridView1.DataBind();
            }
            Session["BANKMTNC_dtINFO_REPORT"] = dtinfo;
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }
    }

    protected void GetSelectedRecords()
    {
        DataTable dt = new DataTable();

        DataTable dtinfo = (DataTable)Session["BANKMTNC_dtINFO_REPORT"];
        dt = dtinfo.Clone();
        dt.Rows.Clear();
        foreach (GridViewRow row in GridView1.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                //'Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkRow"), CheckBox)
                //'If chkRow.Checked ){
                String BNK_REFSRL = row.Cells[2].Text.Replace("+nbsp;", "");

                DataRow[] DR = dtinfo.Select(" BNK_REFSRL = '" + BNK_REFSRL + "'    ");
                if (DR.Length > 0)
                {
                    dt.ImportRow(DR[0]);
                }
                // '}
            }
        }

        //'GridView1.DataSource = dt
        //'GridView1.DataBind()


        ReportDocument rpt = new ReportDocument();
        String rname = Server.MapPath("~/Reports/BANK_INFO_RPT.rpt");
        String dwnldfname = Server.MapPath("~/ReportsGenerate/BANKINFO_RPT.xls");
        // '  Dim dt As DataTable = TryCast(Session("NAVRPT_dt_NAVRPT_REPORT"), DataTable)

        if (dt.Rows.Count > 0)
        {
            rpt.Load(rname);
            rpt.SetDataSource(dt);

            rpt.PrintOptions.PaperSize = PaperSize.PaperA4;
            rpt.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
            rpt.ExportToDisk(ExportFormatType.Excel, dwnldfname);
            rpt.Close();
            rpt.Dispose();


            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("content-disposition", "attachment;filename =" + "BANKINFO_RPT.xls");
            Response.ContentType = "application/ms-excel";
            Response.TransmitFile(dwnldfname);

        }
    }
    protected void Button1_Click(Object sender, System.EventArgs e)
    {
        GetSelectedRecords();
    }
    public void mbox(string val)
    {
        //string cs   = "c";
        //string cst  = "<script type=""text/javascript"">" + "alert('" + val + "');</" + "script>";
        //Page page    = HttpContext.Current.Handler;
        //page.ClientScript.RegisterStartupScript(Me.typeof, cs, cst);

        string USR = User.Identity.Name.ToUpper();
        dbo.ErrorLog(USR + " : BANK_MTNC.aspx : " + val, Server.MapPath("~\\Logs\\ErrorLog"));
    }
}
