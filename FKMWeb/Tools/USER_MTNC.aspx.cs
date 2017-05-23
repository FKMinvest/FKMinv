using System.Data;
using System.IO;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class Tools_USER_MTNC : Page
{
    fkminvcom dbo = new fkminvcom();

    // Visual C# .NET requires that you override the OnInit function,
    // adding a new delegate for the Page_Load event.

    override protected void OnInit(EventArgs e)
    {
        this.Load += new System.EventHandler(this.Page_Load);
        base.OnInit(e);
        CMB_USR_REFSRL.SelectedIndexChanged += new System.EventHandler(CMB_USR_REFSRL_SelectedIndexChanged); 
        //UploadButton.Click += new System.EventHandler(UploadButton_Click1); 
        GridView1.SelectedIndexChanged += new System.EventHandler(GridView1_SelectedIndexChanged);
        // GridView1.RowDeleting += new System.Web.UI.WebControls.GridViewDeleteEventHandler(Gridview1_RowDeleting);  

        BTN_ADD.Click += new System.EventHandler(BTN_ADD_Click);
        BTN_UPDT.Click += new System.EventHandler(BTN_UPDT_Click);
        BTN_CLEAR.Click += new System.EventHandler(BTN_CLEAR_Click);

        //TXT_OPNDATE.TextChanged += new System.EventHandler(TXT_OPNDATE_TextChanged);
        //TXT_OPNTIME.TextChanged += new System.EventHandler(TXT_OPNTIME_TextChanged);
        //TXT_ESTCLSDATE.TextChanged += new System.EventHandler(TXT_ESTCLSDATE_TextChanged);
        //TXT_ESTCLSTIME.TextChanged += new System.EventHandler(TXT_ESTCLSTIME_TextChanged);
        //TXT_CLSDATE.TextChanged += new System.EventHandler(TXT_CLSDATE_TextChanged);
        //TXT_CLSTIME.TextChanged += new System.EventHandler(TXT_CLSTIME_TextChanged);
    }

    public void Page_Load(object sender, System.EventArgs e)
    {

        if((!IsPostBack && !IsCallback))
        {
             
            BTN_CLEAR_Click(sender, e);
             
            INIT_USERID();
            GET_GRID_INFO();
        }
    }


         public void INIT_USERID()
    {
        //'Dim COMP_CD As string = Session("USER_COMP_CD")
        string RETRVQRY = "SELECT DISTINCT USR_USERID , USR_USERID + '-' +  USR_NAME AS USR_NAME FROM USER_INFO ORDER BY USR_NAME ";
        DataTable dtinfo = dbo.SelTable(RETRVQRY);
        dtinfo.Rows.Add("*", "ADD NEW USER");
        CMB_USR_REFSRL.DataSource = dtinfo;
        CMB_USR_REFSRL.DataBind();
        CMB_USR_REFSRL.SelectedIndex = -1;
// where USR_STATUS = 'A'  
    }
    public void BTN_CLEAR_Click(object sender, System.EventArgs e) // Handles BTN_CLEAR.Click
    {
        TXT_USR_NAME.Text = "";
        TXT_USR_USERID.Text = "";
        TXT_USR_MENUGROUP.Text = "";
        TXT_USR_CIVILID.Text = "";
        TXT_USR_ADDRESS.Text = "";
        TXT_USR_TELEPHONE.Text = "";
        TXT_USR_EMAILID.Text = "";
        TXT_USR_LST_ACCESS_DT.Text = "";
        TXT_USR_LST_ACS_TIME.Text = "";
        CMB_USR_STATUS.SelectedIndex = 0;

        BTN_ADD.Visible = true;
        //' CMB_USR_REFSRL.SelectedIndex = -1
        //'CMB_USR_REFSRL.Visible = false
        BTN_UPDT.Visible = false;

       // ' CMB_USR_REFSRL.SelectedIndex = CMB_USR_REFSRL.Items.Count - 1
        GET_GRID_INFO();
    }




    public void BTN_ADD_Click(object sender, System.EventArgs e) // Handles BTN_ADD.Click
    {
        if (CMB_USR_REFSRL.SelectedIndex < 0)
        {
            mbox("PLEASE SELECT THE ADD NEW USER !!");
            return;
        }
        else if (CMB_USR_REFSRL.SelectedIndex == CMB_USR_REFSRL.Items.Count - 1)
        {

            if (TXT_USR_USERID.Text.Trim().Length < 0)
            {
                mbox(" Please enter valid USER ID !!");
                return;
            }

            string USR = User.Identity.Name.ToUpper();
            string UPDDT = System.DateTime.Today.ToString("dd/MM/yyyy");
            string UPDTIME = System.DateTime.Now.ToString("hh:mm:ss");
            string CODE = TXT_USR_USERID.Text.ToUpper();
            try
            {
                string RETRVQRY = "SELECT * FROM USER_INFO WHERE USR_USERID = '" + CODE + "'  ";
                if (dbo.RecExist(RETRVQRY) == false)
                {


                    string INSQRY = "INSERT INTO USER_INFO VALUES('" + TXT_USR_NAME.Text.ToUpper() + "','" + CODE.ToUpper() +
                                                   "','" + CMB_USR_STATUS.SelectedValue + "','" + TXT_USR_MENUGROUP.Text.ToUpper() + "','" + TXT_USR_CIVILID.Text.ToUpper() +
                                                   "',NULL,'" + TXT_USR_ADDRESS.Text.ToUpper() + "','" + TXT_USR_TELEPHONE.Text.ToUpper() +
                                                   "','" + TXT_USR_EMAILID.Text.ToUpper() + "','" + UPDDT + "','" + UPDTIME +
                                                   "','" + USR + "','" + UPDDT + "','" + USR + "','" + UPDDT + "','" + UPDTIME + "')";

                    string PSWDQRY = "INSERT INTO PSWD_INFO VALUES('" + CODE.ToUpper() + "','FKM*123','A','" + System.DateTime.Today.AddDays(1).ToString("dd/MM/yyyy") +
                        "','" + USR + "','" + UPDDT + "','" + USR + "','" + UPDDT + "','" + UPDTIME + "')";

                    if (dbo.Transact(INSQRY, PSWDQRY, "", "", "") == false)
                    {
                        mbox("ERROR !!");
                    }
                    else
                    {
                        //'GridView1.DataBind()
                        mbox("CODE : '" + TXT_USR_NAME.Text.ToUpper() + "'   RECORD SAVED SUCCESSFULLY !!");
                        BTN_CLEAR_Click(sender, e);
                        INIT_USERID();
                    }
                }
                else
                {
                    //'TXT_SRL.Text = NEXT_REC_SRL()
                    mbox(" " + TXT_USR_USERID.Text.ToUpper() + " ALREADY EXIST !! PLEASE TRY AGAIN WITH NEW USER ID ");

                }
            }
            catch (Exception EX)
            {
                mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
            }

        }
    }
    public void BTN_UPDT_Click(object sender, System.EventArgs e) // Handles BTN_UPDT.Click
    {

        string USR = User.Identity.Name.ToUpper();
        string UPDDT = System.DateTime.Today.ToString("dd/MM/yyyy");
        string UPDTIME = System.DateTime.Now.ToString("hh:mm:ss");
        string CODE = CMB_USR_REFSRL.SelectedValue;
        if (CMB_USR_REFSRL.SelectedIndex < 0)
        {
            mbox("PLEASE SELECT THE ADD NEW USER !!");
            return;
        }
        else if (CMB_USR_REFSRL.SelectedIndex == CMB_USR_REFSRL.Items.Count - 1)
        {
            mbox(" PLEASE SELECT THE USER ID YOU NEED TO CHANGE  !!");
            return;
        }
        try
        {
            string RETRVQRY = "SELECT * FROM USER_INFO WHERE USR_USERID = '" + CODE + "'  ";
            if (dbo.RecExist(RETRVQRY) == true)
            {
                string UPDQRY = "UPDATE USER_INFO SET USR_NAME = '" + TXT_USR_NAME.Text.ToUpper() +
                                       "', USR_MENUGROUP  = '" + TXT_USR_MENUGROUP.Text.ToUpper() + "',USR_CIVILID = '" + TXT_USR_CIVILID.Text.ToUpper() +
                                       "', USR_STATUS = '" + CMB_USR_STATUS.SelectedValue +
                                       "', USR_ADDRESS = '" + TXT_USR_ADDRESS.Text.ToUpper() + "',USR_TELEPHONE = '" + TXT_USR_TELEPHONE.Text.ToUpper() +
                                       "', USR_EMAILID = '" + TXT_USR_EMAILID.Text.ToUpper() +
                                       "', USR_UPDBY = '" + USR + "', USR_UPDDATE = '" + UPDDT + "', USR_UPDTIME = '" + UPDTIME + "' " +
                                       " WHERE USR_USERID= '" + CODE + "'   ";

                //                '"', USR_BALANCE_AMT  = '" + USR_BALANCE_AMT + "',USR_OPN_BALANCE = '" + USR_OPN_BALANCE +


                string PSWDQRY = "UPDATE PSWD_INFO " +
                      "SET   PSWD_STATUS = '" + CMB_USR_STATUS.SelectedValue + "'  " +
                      "WHERE PSWD_USERID = '" + CODE + "'  ";

                if (dbo.Transact(UPDQRY, PSWDQRY, "", "", "") == false)
                {
                    mbox("ERROR !!");
                }
                else
                {
                    BTN_CLEAR_Click(sender, e);
                    mbox("CODE : '" + CODE + "'   UPDATED SUCCESSFULLY !!");
                }


            }
            else
            {
                //'TXT_SRL.Text = NEXT_REC_SRL()
                mbox(" " + TXT_USR_USERID.Text.ToUpper() + " NOT EXIST !! PLEASE TRY AGAIN WITH NEW USER ID ");

            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }

    }

    public void CMB_USR_REFSRL_CODE(string CODE)
    {
        //Dim CODE As string = CMB_USR_REFSRL.SelectedValue

        try
        {
            //' Dim COMP_CD As string = Session("USER_COMP_CD")
            string RETRVQRY = "SELECT * FROM USER_INFO WHERE USR_USERID = '" + CODE + "' ";
            DataTable dtinfo = dbo.SelTable(RETRVQRY);
            if (dtinfo.Rows.Count == 1)
            {
                // '  CMB_USR_TYPE.SelectedIndex = CMB_USR_TYPE.Items.IndexOf(CMB_USR_TYPE.Items.FindByValue(dtinfo.Rows[0]["USR_TYPE").ToString))

                            for ( int I = 0 ; I <= CMB_USR_REFSRL.Items.Count - 1 ; I++)
                {
                                if( dtinfo.Rows[0]["USR_USERID"].ToString().Trim() == CMB_USR_REFSRL.Items[I].Value.Trim() ){
                                    CMB_USR_REFSRL.SelectedIndex = I;
                               } 
                }
                CMB_USR_STATUS.SelectedIndex = CMB_USR_STATUS.Items.IndexOf(CMB_USR_STATUS.Items.FindByValue(dtinfo.Rows[0]["USR_STATUS"].ToString().Trim()));
                TXT_USR_USERID.Text = CODE;
                TXT_USR_NAME.Text = dtinfo.Rows[0]["USR_NAME"].ToString().Trim();
                TXT_USR_MENUGROUP.Text = dtinfo.Rows[0]["USR_MENUGROUP"].ToString().Trim();
                TXT_USR_CIVILID.Text = dtinfo.Rows[0]["USR_CIVILID"].ToString().Trim();
                TXT_USR_ADDRESS.Text = dtinfo.Rows[0]["USR_ADDRESS"].ToString().Trim();
                TXT_USR_TELEPHONE.Text = dtinfo.Rows[0]["USR_TELEPHONE"].ToString().Trim();
                TXT_USR_EMAILID.Text = dtinfo.Rows[0]["USR_EMAILID"].ToString().Trim();
                TXT_USR_LST_ACCESS_DT.Text = dtinfo.Rows[0]["USR_LST_ACCESS_DT"].ToString().Trim();
                TXT_USR_LST_ACS_TIME.Text = dtinfo.Rows[0]["USR_LST_ACS_TIME"].ToString().Trim();

                BTN_ADD.Visible = false;
                BTN_UPDT.Visible = true;
            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }
    }

    public void CMB_USR_REFSRL_SelectedIndexChanged(object sender, System.EventArgs e) // Handles CMB_USR_REFSRL.SelectedIndexChanged
    {
            if( CMB_USR_REFSRL.SelectedIndex < 0 ){
            mbox("PLEASE SELECT THE DOCUMENT TYPE !!");
            return;
        } else if ( CMB_USR_REFSRL.SelectedIndex == CMB_USR_REFSRL.Items.Count - 1 ){
            BTN_CLEAR_Click(sender, e);
            return;
       } 
                 string CODE    = CMB_USR_REFSRL.SelectedValue;
   CMB_USR_REFSRL_CODE(CODE);
    }
     

    public void GET_GRID_INFO()
    {

       try{

            string RETRVQRY   = "SELECT * FROM USER_INFO  ";
            //'FKMREF + BNK_CODE + FRMDT + TODT
            DataTable dtinfo = dbo.SelTable(RETRVQRY);
            if( dtinfo.Rows.Count > 0 ){


                GridView1.DataSource = dtinfo;
                GridView1.DataBind();
            }else{

                GridView1.DataSource = null;
                GridView1.DataBind();
           } 
          //  ' Session("BANKMTNC_dtINFO_REPORT") = dtinfo
        
            }
            catch (Exception EX)
            {
                mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
            }
    }



    public void GridView1_SelectedIndexChanged(object sender, System.EventArgs e) // Handles GridView1.SelectedIndexChanged
    {

        if (GridView1.SelectedDataKey[0].ToString().Trim().Length != 0)
        {
            BTN_CLEAR_Click(sender, e);
            string CODE = (string)GridView1.SelectedDataKey[0];
            CMB_USR_REFSRL.DataBind();
            CMB_USR_REFSRL_CODE(CODE);
        }
        //        Dim RETRVQRY As string = "SELECT * FROM USER_INFO WHERE USR_USERID = '" + CODE + "'"
        //        Dim dtinfo As DataTable = dbo.SelTable(RETRVQRY)
        //        if( dtinfo.Rows.Count = 1 ){

        //            For I = 0 To CMB_USR_REFSRL.Items.Count - 1
        //                if( dtinfo.Rows[0]["USR_USERID"].ToString().Trim() = CMB_USR_REFSRL.Items(I).Value.Trim ){
        //                    CMB_USR_REFSRL.SelectedIndex = I
        //               } 
        //            Next
        //            ' CMB_USR_REFSRL.SelectedIndex = CMB_USR_REFSRL.Items.IndexOf(CMB_USR_REFSRL.Items.FindByValue(CODE.Trim))
        //            TXT_USR_NAME.Text = dtinfo.Rows[0]["USR_NAME"].ToString().Trim()
        //            TXT_USR_USERID.Text = dtinfo.Rows[0]["USR_USERID"].ToString().Trim()
        //            TXT_USR_MENUGROUP.Text = dtinfo.Rows[0]["USR_MENUGROUP"].ToString().Trim()
        //            TXT_USR_CIVILID.Text = dtinfo.Rows[0]["USR_CIVILID"].ToString().Trim()
        //            TXT_USR_ADDRESS.Text = dtinfo.Rows[0]["USR_ADDRESS"].ToString().Trim()
        //            TXT_USR_TELEPHONE.Text = dtinfo.Rows[0]["USR_TELEPHONE"].ToString().Trim()
        //            TXT_USR_EMAILID.Text = dtinfo.Rows[0]["USR_EMAILID"].ToString().Trim()
        //            TXT_USR_LST_ACCESS_DT.Text = dtinfo.Rows[0]["USR_LST_ACCESS_DT"].ToString().Trim()
        //            TXT_USR_LST_ACS_TIME.Text = dtinfo.Rows[0]["USR_LST_ACS_TIME"].ToString().Trim()
        //            CMB_USR_STATUS.SelectedIndex = CMB_USR_STATUS.Items.IndexOf(CMB_USR_STATUS.Items.FindByValue(dtinfo.Rows[0]["USR_STATUS"].ToString().Trim()))

        //            BTN_ADD.Visible = false
        //            BTN_UPDT.Visible = true
        //        Else
        //            BTN_CLEAR_Click(sender, e)
        //            mbox("ERROR !!")
        //       } 
        //    Catch EX As Exception
        //        mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8))
        //    End Try
    }
    
    public void mbox(string val)
    {
        //string cs   = "c";
        //string cst  = "<script type=""text/javascript"">" + "alert('" + val + "');</" + "script>";
        //Page page    = HttpContext.Current.Handler;
        //page.ClientScript.RegisterStartupScript(Me.typeof, cs, cst);

        string USR = User.Identity.Name.ToUpper();
        dbo.ErrorLog(USR + " : USER_MTNC.aspx : " + val, Server.MapPath("~\\Logs\\ErrorLog"));
    }
    }
