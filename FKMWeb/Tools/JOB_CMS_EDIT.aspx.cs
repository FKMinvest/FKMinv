using System;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Net;

public partial class Tools_JOB_CMS_EDIT : Page
{
    fkminvcom dbo = new fkminvcom();

    // Visual C# .NET requires that you override the OnInit function,
    // adding a new delegate for the Page_Load event.

    override protected void OnInit(EventArgs e)
    {
        this.Load += new System.EventHandler(this.Page_Load);
        base.OnInit(e);
        CMB_CODE.SelectedIndexChanged += new System.EventHandler(CMB_CODE_SelectedIndexChanged);
        cmb_Rpt_user.SelectedIndexChanged += new System.EventHandler(cmb_Rpt_user_SelectedIndexChanged);
        cmb_Rpt_sts.SelectedIndexChanged += new System.EventHandler(cmb_Rpt_sts_SelectedIndexChanged);
        //UploadButton.Click += new System.EventHandler(UploadButton_Click1); 
        GridView1.SelectedIndexChanged += new System.EventHandler(GridView1_SelectedIndexChanged);
        GridView1.RowDeleting += new System.Web.UI.WebControls.GridViewDeleteEventHandler(Gridview1_RowDeleting);
        GridView1.RowDataBound += new System.Web.UI.WebControls.GridViewRowEventHandler(GridView1_RowDataBound);
        CHBX_EDIT.CheckedChanged += new System.EventHandler(CHBX_EDIT_CheckedChanged);
        Button1.Click += new System.EventHandler(Button1_Click);

        BTN_ADD.Click += new System.EventHandler(BTN_ADD_Click);
        BTN_UPDT.Click += new System.EventHandler(BTN_UPDT_Click);
        BTN_DELETE.Click += new System.EventHandler(BTN_DELETE_Click);
        BTN_CLEAR.Click += new System.EventHandler(BTN_CLEAR_Click);
        BTN_mail.Click += new System.EventHandler(BTN_mail_Click);

        TXT_OPNDATE.TextChanged += new System.EventHandler(TXT_OPNDATE_TextChanged);
        TXT_OPNTIME.TextChanged += new System.EventHandler(TXT_OPNTIME_TextChanged);
        TXT_ESTCLSDATE.TextChanged += new System.EventHandler(TXT_ESTCLSDATE_TextChanged);
        TXT_ESTCLSTIME.TextChanged += new System.EventHandler(TXT_ESTCLSTIME_TextChanged);
        TXT_CLSDATE.TextChanged += new System.EventHandler(TXT_CLSDATE_TextChanged);
        TXT_CLSTIME.TextChanged += new System.EventHandler(TXT_CLSTIME_TextChanged);
    }

    public void Page_Load(object sender, System.EventArgs e)
    {

        if ((!IsPostBack && !IsCallback))
        {
            BTN_CLEAR_Click(sender, e);
            CHBX_EDIT.Checked = false;

            cmb_Rpt_sts.SelectedIndex = 2;
            //  TXT_SRCH.Text = " ";
            CMB_CODE.DataBind();
            INIT_USERID();
            GET_GRID_INFO();

            //if (User.Identity.Name.ToString().Trim().ToUpper() == "DEV_R")
            //{
            //    GridView1.Columns[14].Visible = true;
            //    //GridView1.Columns.Item(14).Visible = false;
            //}
            //else
            //{
            //    GridView1.Columns[14].Visible = false;
            //    //GridView1.Columns.Item(14).Visible = false;
            //}
        }
    }
    public void INIT_USERID()
    {
        //'Dim COMP_CD As string = Session("USER_COMP_CD")
        string RETRVQRY = "SELECT DISTINCT USR_USERID , USR_USERID + '-' +  USR_NAME AS USR_NAME FROM USER_INFO where USR_STATUS = 'A'  ORDER BY USR_NAME ";
        DataTable dtinfo = dbo.SelTable(RETRVQRY);
        CMB_USR_REFSRL.DataSource = dtinfo;
        CMB_USR_REFSRL.DataBind();
        CMB_USR_REFSRL.SelectedIndex = -1;

        dtinfo.Rows.Add("%", "ALL USER");
        cmb_Rpt_user.DataSource = dtinfo;
        cmb_Rpt_user.DataBind();
        cmb_Rpt_user.SelectedIndex = cmb_Rpt_user.Items.Count - 1;
    }

    public void BTN_CLEAR_Click(object sender, System.EventArgs e) //Handles BTN_CLEAR.Click
    {
        CHBX_EDIT_CheckedChanged(sender, e);
    }

    protected void CHBX_EDIT_CheckedChanged(object sender, System.EventArgs e) //Handles CHBX_EDIT.CheckedChanged
    {
        if (CHBX_EDIT.Checked == true)
        {
            CMB_CDTYP.SelectedIndex = -1;
            CMB_USR_REFSRL.SelectedIndex = -1;
            TXT_DESC.Text = "";
            TXT_OPNDATE.Text = "";
            TXT_OPNTIME.Text = "";
            TXT_ESTCLSDATE.Text = "";
            TXT_ESTCLSTIME.Text = "";
            TXT_ESTDURATION.Text = "";
            TXT_CLSDATE.Text = "";
            TXT_CLSTIME.Text = "";
            TXT_DURATION.Text = "";
            CMB_status.SelectedIndex = 1;
            TXT_REMX.Text = "";

            LBL_CLSDATE.Visible = false;
            TXT_CLSDATE.Visible = false;
            LBL_CLSTIME.Visible = false;
            TXT_CLSTIME.Visible = false;
            LBL_DURATION.Visible = false;
            TXT_DURATION.Visible = false;

            TXT_CODE.Visible = false;
            BTN_ADD.Visible = false;
            CMB_CODE.SelectedIndex = -1;
            CMB_CODE.Visible = true;
            BTN_UPDT.Visible = true;
        }
        else
        {
            CMB_CDTYP.SelectedIndex = -1;
            CMB_USR_REFSRL.SelectedIndex = -1;
            TXT_DESC.Text = "";
            TXT_OPNDATE.Text = "";
            TXT_OPNTIME.Text = "";
            TXT_ESTCLSDATE.Text = "";
            TXT_ESTCLSTIME.Text = "";
            TXT_ESTDURATION.Text = "";
            TXT_CLSDATE.Text = "";
            TXT_CLSTIME.Text = "";
            TXT_DURATION.Text = "";
            CMB_status.SelectedIndex = 1;
            TXT_REMX.Text = "";

            LBL_CLSDATE.Visible = false;
            TXT_CLSDATE.Visible = false;
            LBL_CLSTIME.Visible = false;
            TXT_CLSTIME.Visible = false;
            LBL_DURATION.Visible = false;
            TXT_DURATION.Visible = false;

            TXT_CODE.Text = NEXT_REC_SRL();
            TXT_CODE.Visible = true;
            BTN_ADD.Visible = true;
            CMB_CODE.SelectedIndex = -1;
            CMB_CODE.Visible = false;
            BTN_UPDT.Visible = false;
        }


        TXT_ESTCLSDATE.Enabled = true;
        TXT_ESTCLSTIME.Enabled = true;
        TXT_ESTDURATION.Enabled = true;
        TXT_CLSDATE.Enabled = true;
        TXT_CLSTIME.Enabled = true;
        TXT_DURATION.Enabled = true;
        CMB_status.Enabled = true;

        BTN_DELETE.Visible = false;
        // ' Call GET_GRID_INFO()
    }

    protected void BTN_ADD_Click(object sender, System.EventArgs e) // Handles BTN_ADD.Click
    {
        string USR = User.Identity.Name.ToUpper();
        string UPDDT = System.DateTime.Today.ToString("dd/MM/yyyy");
        string UPDTIME = System.DateTime.Now.ToString("HH:mm:ss");
        string CODE = TXT_CODE.Text.ToUpper().Trim();
        string OPNDT = TXT_OPNDATE.Text;
        DateTime OPNDATE = DateTime.Parse(TXT_OPNDATE.Text);//'CDate(OPNDT.Substring(3, 3) + OPNDT.Substring(0, 3) + OPNDT.Substring(6, 4))
        string OPNTIME = TXT_OPNTIME.Text;
        DateTime OPNTIMEdt = System.DateTime.Now;
        if (OPNTIME.Length > 5)
        {
            OPNTIMEdt = DateTime.Parse(OPNTIME);
        }
        DateTime dteOPNtime = new DateTime(OPNDATE.Year, OPNDATE.Month, OPNDATE.Day, OPNTIMEdt.Hour, OPNTIMEdt.Minute, OPNTIMEdt.Second);

        DateTime ESTCLSDATE = DateTime.Parse(TXT_ESTCLSDATE.Text);//'OPNDT.Substring(3, 3) + OPNDT.Substring(0, 3) + OPNDT.Substring(6, 4)
        string ESTCLSTIME = TXT_ESTCLSTIME.Text;
        DateTime ESTCLSTIMEdt = DateTime.Parse(ESTCLSTIME);
        DateTime dteESTCLStime = new DateTime(ESTCLSDATE.Year, ESTCLSDATE.Month, ESTCLSDATE.Day, ESTCLSTIMEdt.Hour, ESTCLSTIMEdt.Minute, ESTCLSTIMEdt.Second);

        TimeSpan TimeSpan = dteESTCLStime.Subtract(dteOPNtime);

        if (dteOPNtime > dteESTCLStime)
        {

            mbox("Start Time: " + dteOPNtime.ToString() + "  should earlier than Estimated Closing Time: " + dteESTCLStime.ToString() + " Duration Taken : " + TimeSpan.ToString());
            return;
        }

        string SRL = NEXT_REC_SRL();
        try
        {
            string RETRVQRY = "SELECT * FROM JOB_TKN_INFO WHERE JB_SRLNO = '" + SRL + "'";

            if (dbo.RecExist(RETRVQRY) == false)
            {
                string INSQRY = "INSERT INTO JOB_TKN_INFO VALUES('" + SRL + "','" + CMB_USR_REFSRL.Text.ToString().ToUpper() + "','" + CMB_CDTYP.Text.ToString().ToUpper() +
                                       "','" + TXT_DESC.Text.ToUpper() + "','" + dteOPNtime.ToString("dd/MM/yyyy") + "','" + dteOPNtime.ToString("HH:mm:ss") + "','" +
                                       dteESTCLStime.ToString("dd/MM/yyyy") + "',null,'" + dteESTCLStime.ToString("HH:mm:ss") + "',null, null,'" +
                                       CMB_status.SelectedValue.ToString().ToUpper() + "' , '" + USR + "',null,'" + TXT_REMX.Text.ToUpper() + "','" + UPDDT + "','" + UPDDT + "','" + UPDTIME + "')";
                //'Dim INSQRY As string = "INSERT INTO JOB_TKN_INFO VALUES('" + SRL + "','" + CMB_USR_REFSRL.Text.ToString().ToUpper + "','" + CMB_CDTYP.Text.ToString().ToUpper +
                //'                                      "','" + TXT_DESC.Text.ToUpper + "','" + dteOPNtime.ToString + "','" + dteCLStime.ToString + "', '" + TimeSpan.ToString +
                //'                                      "','" + CMB_status.Text.ToString().ToUpper + "' , '" + USR + "','','" + UPDDT + "','" + UPDDT + "','" + UPDTIME + "')"

                if (dbo.InsRecord(INSQRY) == false)
                {
                    mbox("ERROR !!");
                }
                else
                {

                    if (send_mail() == true)
                    {
                        // ' StatusLabel.Text = "Upload status: mail sent!"
                    }
                    else
                    {
                        //' StatusLabel.Text = "Upload status: The mail could not be sent.!"
                    }
                    CHBX_EDIT.Checked = false;
                    BTN_CLEAR_Click(sender, e);
                    CMB_CODE.DataBind();
                    GET_GRID_INFO();
                    mbox("CODE : '" + CODE + "'   ADDED SUCCESSFULLY !!");
                }
            }
            else
            {

                TXT_CODE.Text = NEXT_REC_SRL();
                mbox("CODE : '" + CODE + "'   ALREADY EXIST !!");
            }
        }
        catch (Exception EX)
        {
            //  ' mbox("start : " + dteOPNtime.ToString + "   end : " + dteCLStime.ToString + " duration : " + TimeSpan.ToString)
        }
    }

    protected void BTN_UPDT_Click(object sender, System.EventArgs e) // Handles BTN_UPDT.Click
    {
        //' Dim CODE As string = TXT_DURATION.Text.ToUpper.Trim


        string USR = User.Identity.Name.ToUpper();
        string UPDDT = System.DateTime.Today.ToString("dd/MM/yyyy");
        string UPDTIME = System.DateTime.Now.ToString("HH:mm:ss");
        string CODE = TXT_CODE.Text.ToUpper().Trim();
        string OPNDT = TXT_OPNDATE.Text;
        DateTime OPNDATE = DateTime.Parse(TXT_OPNDATE.Text);//'CDate(OPNDT.Substring(3, 3) + OPNDT.Substring(0, 3) + OPNDT.Substring(6, 4))
        string OPNTIME = TXT_OPNTIME.Text;
        DateTime OPNTIMEdt = DateTime.Parse(OPNTIME);
        DateTime dteOPNtime = new DateTime(OPNDATE.Year, OPNDATE.Month, OPNDATE.Day, OPNTIMEdt.Hour, OPNTIMEdt.Minute, OPNTIMEdt.Second);


        DateTime ESTCLSDATE = DateTime.Parse(TXT_ESTCLSDATE.Text);//'OPNDT.Substring(3, 3) + OPNDT.Substring(0, 3) + OPNDT.Substring(6, 4)
        string ESTCLSTIME = TXT_ESTCLSTIME.Text;
        DateTime ESTCLSTIMEdt = DateTime.Parse(ESTCLSTIME);
        DateTime dteESTCLStime = new DateTime(ESTCLSDATE.Year, ESTCLSDATE.Month, ESTCLSDATE.Day, ESTCLSTIMEdt.Hour, ESTCLSTIMEdt.Minute, ESTCLSTIMEdt.Second);

        try
        {
            string RETRVQRY = "SELECT * FROM JOB_TKN_INFO WHERE JB_SRLNO = '" + CODE + "'";
            string UPDQRY = "";
            if (dbo.RecExist(RETRVQRY) == true)
            {
                if (CMB_status.SelectedValue.ToString() == "C" || CMB_status.SelectedValue.ToString() == "S")
                {
                    //  SET_DT = TXT_CLSDATE.Text;
                    DateTime CLSDATE = DateTime.Parse(TXT_CLSDATE.Text); // 'SET_DT.Substring(3, 3) + SET_DT.Substring(0, 3) + SET_DT.Substring(6, 4)
                    string CLSTIME = TXT_CLSTIME.Text;
                    DateTime CLSTIMEdt = DateTime.Parse(CLSTIME);
                    DateTime dteCLStime = new DateTime(CLSDATE.Year, CLSDATE.Month, CLSDATE.Day, CLSTIMEdt.Hour, CLSTIMEdt.Minute, CLSTIMEdt.Second);

                    TimeSpan TimeSpan = dteCLStime.Subtract(dteOPNtime);

                    if (dteOPNtime > dteCLStime)
                    {
                        mbox("Start Time: " + dteOPNtime.ToString() + "  should earlier than Closing Time: " + dteCLStime.ToString() + " Duration Taken : " + TimeSpan.ToString());
                        return;
                    }

                    UPDQRY = "UPDATE JOB_TKN_INFO SET JB_USERID = '" + CMB_USR_REFSRL.Text.ToString().ToUpper() + "', JB_CODE = '" + CMB_CDTYP.Text.ToString().ToUpper() + "',JB_DESC = '" + TXT_DESC.Text.ToUpper() +
                                           "', JB_OPN_JOB_DT = '" + dteOPNtime.ToString("dd/MM/yyyy") + "',JB_OPN_JOB_TM = '" + dteOPNtime.ToString("HH:mm:ss") + "', JB_EST_CLS_DT = '" + dteESTCLStime.ToString("dd/MM/yyyy") +
                                           "',JB_EST_CLS_TM = '" + dteESTCLStime.ToString("HH:mm:ss") + "', JB_CLS_JOB_DT = '" + dteCLStime.ToString("dd/MM/yyyy") +
                                           "',JB_CLS_JOB_TM = '" + dteCLStime.ToString("HH:mm:ss") + "', JB_DURATION = '" + TimeSpan.ToString() + "',JB_STATUS = '" + CMB_status.SelectedValue.ToString().ToUpper() +
                                           "', JB_CLS_USR_ENTRY = '" + USR + "', JB_REMX ='" + TXT_REMX.Text.ToUpper() + "', JB_UPDDATE = '" + UPDDT + "',JB_UPDTIME = '" + UPDTIME + "' " +
                                           " WHERE JB_SRLNO= '" + CODE + "' ";
                }
                else
                {


                    UPDQRY = "UPDATE JOB_TKN_INFO SET JB_USERID = '" + CMB_USR_REFSRL.Text.ToString().ToUpper() + "', JB_CODE = '" + CMB_CDTYP.Text.ToString().ToUpper() + "',JB_DESC = '" + TXT_DESC.Text.ToUpper() +
                                           "', JB_OPN_JOB_DT = '" + dteOPNtime.ToString("dd/MM/yyyy") + "',JB_OPN_JOB_TM = '" + dteOPNtime.ToString("HH:mm:ss") + "', JB_EST_CLS_DT = '" + dteESTCLStime.ToString("dd/MM/yyyy") +
                                           "',JB_EST_CLS_TM = '" + dteESTCLStime.ToString("HH:mm:ss") +
                                           "', JB_CLS_JOB_DT = null,JB_CLS_JOB_TM = null , JB_DURATION = null, JB_STATUS = '" +
                                           CMB_status.SelectedValue.ToString().ToUpper() + "',JB_REMX ='" + TXT_REMX.Text.ToUpper() + "',JB_UPDDATE = '" + UPDDT + "',JB_UPDTIME = '" + UPDTIME + "' " +
                                           " WHERE JB_SRLNO= '" + CODE + "' ";

                }


                if (dbo.InsRecord(UPDQRY) == false)
                {
                    mbox("ERROR !!");
                }
                else
                {
                    //send_mail();
                    BTN_CLEAR_Click(sender, e);
                    CMB_CODE.SelectedIndex = -1;
                    CMB_CODE.DataBind();
                    GET_GRID_INFO();

                    mbox("CODE : '" + CODE + "'   UPDATED SUCCESSFULLY !!");
                }

            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }

    }

    protected void BTN_DELETE_Click(object sender, System.EventArgs e)  // Handles BTN_ADD.Click
    {
        string CODE = TXT_CODE.Text.ToUpper().Trim();
        try
        {
            string RETRVQRY = "SELECT * FROM JOB_TKN_INFO WHERE JB_SRLNO = '" + CODE + "'";
            string UPDQRY = "";
            if (dbo.RecExist(RETRVQRY) == true)
            {
                UPDQRY = "DELETE FROM JOB_TKN_INFO  WHERE JB_SRLNO= '" + CODE + "' ";


                //if (dbo.InsRecord(UPDQRY) == false)
                //{
                //    mbox("ERROR !!");
                //}
                //else
                //{


                    if (send_mail_task_del() == true)
                    {
                        // ' StatusLabel.Text = "Upload status: mail sent!"
                    }
                    else
                    {
                        //' StatusLabel.Text = "Upload status: The mail could not be sent.!"
                    } 
                    BTN_CLEAR_Click(sender, e);
                    CMB_CODE.SelectedIndex = -1;
                    CMB_CODE.DataBind();
                    GET_GRID_INFO();

                    mbox("CODE : '" + CODE + "'   DELETED SUCCESSFULLY !!");
                //}

            }

        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }


    }

    protected string Duration_Taken()
    {
        if (TXT_CLSDATE.Text.Length > 0 && TXT_CLSTIME.Text.Length > 0)
        {

            TimeSpan TimeSpan = new TimeSpan();
            string CODE = TXT_CODE.Text.ToUpper().Trim();
            string OPNDT = TXT_OPNDATE.Text;
            DateTime OPNDATE = DateTime.Parse(TXT_OPNDATE.Text);
            string OPNTIME = TXT_OPNTIME.Text;
            DateTime OPNTIMEdt = System.DateTime.Now;
            if (OPNTIME.Length > 5)
            {
                OPNTIMEdt = DateTime.Parse(OPNTIME);
            }
            DateTime dteOPNtime = new DateTime(OPNDATE.Year, OPNDATE.Month, OPNDATE.Day, OPNTIMEdt.Hour, OPNTIMEdt.Minute, OPNTIMEdt.Second);

            if (CMB_status.SelectedValue.ToString() == "C" || CMB_status.SelectedValue.ToString() == "S")
            {
                DateTime CLSDATE = DateTime.Parse(TXT_CLSDATE.Text);
                string CLSTIME = TXT_CLSTIME.Text;
                DateTime CLSTIMEdt = DateTime.Parse(CLSTIME);
                DateTime dteCLStime = new DateTime(CLSDATE.Year, CLSDATE.Month, CLSDATE.Day, CLSTIMEdt.Hour, CLSTIMEdt.Minute, CLSTIMEdt.Second);

                TimeSpan = dteCLStime.Subtract(dteOPNtime);

                if (dteOPNtime > dteCLStime)
                {
                    mbox("Start Time: " + dteOPNtime.ToString() + "  should earlier than Closing Time: " + dteCLStime.ToString() + " Duration Taken : " + TimeSpan.ToString());

                    return "";
                }
            }
            return TimeSpan.ToString();
        }

        return "";
    }

    protected string ESTDuration_Taken()
    {
        if (TXT_ESTCLSDATE.Text.Length > 0 && TXT_ESTCLSTIME.Text.Length > 0)
        {

            TimeSpan TimeSpan = new TimeSpan();
            string CODE = TXT_CODE.Text.ToUpper().Trim();
            string OPNDT = TXT_OPNDATE.Text;
            DateTime OPNDATE = DateTime.Parse(TXT_OPNDATE.Text);
            string OPNTIME = TXT_OPNTIME.Text;
            DateTime OPNTIMEdt = System.DateTime.Now;
            if (OPNTIME.Length > 5)
            {
                OPNTIMEdt = DateTime.Parse(OPNTIME);
            }
            DateTime dteOPNtime = new DateTime(OPNDATE.Year, OPNDATE.Month, OPNDATE.Day, OPNTIMEdt.Hour, OPNTIMEdt.Minute, OPNTIMEdt.Second);

            DateTime ESTCLSDATE = DateTime.Parse(TXT_ESTCLSDATE.Text);
            string ESTCLSTIME = TXT_ESTCLSTIME.Text;
            DateTime ESTCLSTIMEdt = DateTime.Parse(ESTCLSTIME);
            DateTime dteESTCLStime = new DateTime(ESTCLSDATE.Year, ESTCLSDATE.Month, ESTCLSDATE.Day, ESTCLSTIMEdt.Hour, ESTCLSTIMEdt.Minute, ESTCLSTIMEdt.Second);

            TimeSpan = dteESTCLStime.Subtract(dteOPNtime);

            if (dteOPNtime > dteESTCLStime)
            {
                mbox("Start Time: " + dteOPNtime.ToString() + "  should earlier than Estimated Closing Time: " + dteESTCLStime.ToString() + " Duration Taken : " + TimeSpan.ToString());

                return "";
            }
            return TimeSpan.ToString();
        }
        return "";
    }

    protected string NEXT_REC_SRL()
    {
        string SRL = "";
        DataTable DTINVINF = new DataTable();
        try
        {
            string RETRVQRY = "SELECT MAX (JB_SRLNO) FROM JOB_TKN_INFO  WHERE JB_SRLNO LIKE '" + System.DateTime.Today.Year + "%' ";
            DTINVINF = dbo.SelTable(RETRVQRY);
            if (DTINVINF.Rows.Count > 0)
            {
                if ( DTINVINF.Rows[0][0].ToString().Length !=0 )
                {
                    decimal NSRL = Convert.ToDecimal(DTINVINF.Rows[0][0].ToString());

                    SRL = (NSRL + 1).ToString("0000000000");
                    //'SRL = SRL
                }
                else
                {
                    SRL = System.DateTime.Today.Year + "000001";
                }
            }
            else
            {
                SRL = System.DateTime.Today.Year + "000001";
            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }
        return SRL;
    }

    protected void CMB_CODE_SelectedIndexChanged(object sender, System.EventArgs e) //Handles CMB_CODE.SelectedIndexChanged
    {
        string CODE = CMB_CODE.SelectedValue;

        BTN_CLEAR_Click(sender, e);
        try
        {
            CHBX_EDIT.Checked = true;
            CHBX_EDIT_CheckedChanged(sender, e);

            string RETRVQRY = "SELECT * FROM JOB_TKN_INFO WHERE JB_SRLNO = '" + CODE + "'";
            DataTable dtinfo = dbo.SelTable(RETRVQRY);
            if (dtinfo.Rows.Count == 1)
            {

                CMB_CODE.SelectedIndex = CMB_CODE.Items.IndexOf(CMB_CODE.Items.FindByValue(CODE));


                TXT_CODE.Text = CODE;
                CMB_CDTYP.SelectedIndex = CMB_CDTYP.Items.IndexOf(CMB_CDTYP.Items.FindByValue(dtinfo.Rows[0]["JB_CODE"].ToString().Trim()));
                CMB_USR_REFSRL.SelectedIndex = CMB_USR_REFSRL.Items.IndexOf(CMB_USR_REFSRL.Items.FindByValue(dtinfo.Rows[0]["JB_USERID"].ToString()));
                TXT_DESC.Text = dtinfo.Rows[0]["JB_DESC"].ToString().Trim();

                DateTime dteOPNtime = DateTime.Parse(dtinfo.Rows[0]["JB_OPN_JOB_DT"].ToString().Trim());
                TXT_OPNDATE.Text = dteOPNtime.ToString("dd/MM/yyyy");
                dteOPNtime = DateTime.Parse(dtinfo.Rows[0]["JB_OPN_JOB_TM"].ToString().Trim());
                TXT_OPNTIME.Text = dteOPNtime.ToString("hh:mm tt");

                DateTime dteESTCLStime = DateTime.Parse(dtinfo.Rows[0]["JB_EST_CLS_DT"].ToString().Trim());
                TXT_ESTCLSDATE.Text = dteESTCLStime.ToString("dd/MM/yyyy");
                dteESTCLStime = DateTime.Parse(dtinfo.Rows[0]["JB_EST_CLS_TM"].ToString().Trim());
                TXT_ESTCLSTIME.Text = dteESTCLStime.ToString("hh:mm tt");
                TXT_ESTDURATION.Text = ESTDuration_Taken();

                CMB_status.SelectedIndex = CMB_status.Items.IndexOf(CMB_status.Items.FindByValue(dtinfo.Rows[0]["JB_STATUS"].ToString().Trim()));
                TXT_REMX.Text = dtinfo.Rows[0]["JB_REMX"].ToString().Trim();
                if (CMB_status.SelectedValue.ToString() == "C" || CMB_status.SelectedValue.ToString() == "S")
                {
                    DateTime dteCLStime = DateTime.Parse(dtinfo.Rows[0]["JB_CLS_JOB_DT"].ToString().Trim());
                    TXT_CLSDATE.Text = dteCLStime.ToString("dd/MM/yyyy");
                    dteCLStime = DateTime.Parse(dtinfo.Rows[0]["JB_CLS_JOB_TM"].ToString().Trim());
                    TXT_CLSTIME.Text = dteCLStime.ToString("hh:mm tt");

                    TXT_DURATION.Text = dtinfo.Rows[0]["JB_DURATION"].ToString().Trim();
                }
                else if (CMB_status.SelectedValue.ToString() == "O")
                {
                    DateTime OPNDATE = DateTime.Parse(TXT_OPNDATE.Text);
                    string OPNTIME = TXT_OPNTIME.Text;
                    DateTime OPNTIMEdt = DateTime.Parse(OPNTIME);
                    dteOPNtime = new DateTime(OPNDATE.Year, OPNDATE.Month, OPNDATE.Day, OPNTIMEdt.Hour, OPNTIMEdt.Minute, OPNTIMEdt.Second);

                    TXT_CLSDATE.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    TXT_CLSTIME.Text = System.DateTime.Now.ToString("hh:mm tt");
                    DateTime CLSDATE = DateTime.Parse(TXT_CLSDATE.Text);
                    string CLSTIME = TXT_CLSTIME.Text;
                    DateTime CLSTIMEdt = DateTime.Parse(CLSTIME);
                    DateTime dteCLStime = new DateTime(CLSDATE.Year, CLSDATE.Month, CLSDATE.Day, CLSTIMEdt.Hour, CLSTIMEdt.Minute, CLSTIMEdt.Second);

                    TimeSpan TimeSpan = dteCLStime.Subtract(dteOPNtime);

                    if (dteOPNtime > dteCLStime)
                    {
                        mbox("Start Time: " + dteOPNtime.ToString() + "  should earlier than Closing Time: " + dteCLStime.ToString() + " Duration Taken : " + TimeSpan.ToString());
                        // ' Exit Sub
                    }
                    TXT_DURATION.Text = TimeSpan.ToString();
                }

                CMB_status.SelectedIndex = 0;
                LBL_CLSDATE.Visible = true;
                TXT_CLSDATE.Visible = true;
                LBL_CLSTIME.Visible = true;
                TXT_CLSTIME.Visible = true;
                LBL_DURATION.Visible = true;
                TXT_DURATION.Visible = true;


                if (Auth_exist("X") == false)
                {
                    TXT_ESTCLSDATE.Enabled = false;
                    TXT_ESTCLSTIME.Enabled = false;
                    TXT_ESTDURATION.Enabled = false;
                    TXT_CLSDATE.Enabled = false;
                    TXT_CLSTIME.Enabled = false;
                    TXT_DURATION.Enabled = false;
                    CMB_status.Enabled = true;
                }
            }
            else
            {
                BTN_CLEAR_Click(sender, e);
                mbox("ERROR !!");
            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, System.EventArgs e) //Handles GridView1.SelectedIndexChanged
    {

        try
        {
            if (GridView1.SelectedDataKey[0].ToString().Trim().Length != 0)
            {
                CHBX_EDIT.Checked = true;
                CHBX_EDIT_CheckedChanged(sender, e);
                string CODE = (string)GridView1.SelectedDataKey[0];

                string RETRVQRY = "SELECT * FROM JOB_TKN_INFO WHERE JB_SRLNO = '" + CODE + "'";
                DataTable dtinfo = dbo.SelTable(RETRVQRY);
                if (dtinfo.Rows.Count == 1)
                {

                    CMB_CODE.SelectedIndex = CMB_CODE.Items.IndexOf(CMB_CODE.Items.FindByValue(CODE));


                    TXT_CODE.Text = CODE;
                    CMB_CDTYP.SelectedIndex = CMB_CDTYP.Items.IndexOf(CMB_CDTYP.Items.FindByValue(dtinfo.Rows[0]["JB_CODE"].ToString().Trim()));
                    CMB_USR_REFSRL.SelectedIndex = CMB_USR_REFSRL.Items.IndexOf(CMB_USR_REFSRL.Items.FindByValue(dtinfo.Rows[0]["JB_USERID"].ToString()));
                    TXT_DESC.Text = dtinfo.Rows[0]["JB_DESC"].ToString().Trim();

                    DateTime dteOPNtime = DateTime.Parse(dtinfo.Rows[0]["JB_OPN_JOB_DT"].ToString().Trim());
                    TXT_OPNDATE.Text = dteOPNtime.ToString("dd/MM/yyyy");
                    dteOPNtime = DateTime.Parse(dtinfo.Rows[0]["JB_OPN_JOB_TM"].ToString().Trim());
                    TXT_OPNTIME.Text = dteOPNtime.ToString("hh:mm tt");

                    DateTime dteESTCLStime = DateTime.Parse(dtinfo.Rows[0]["JB_EST_CLS_DT"].ToString().Trim());
                    TXT_ESTCLSDATE.Text = dteESTCLStime.ToString("dd/MM/yyyy");
                    dteESTCLStime = DateTime.Parse(dtinfo.Rows[0]["JB_EST_CLS_TM"].ToString().Trim());
                    TXT_ESTCLSTIME.Text = dteESTCLStime.ToString("hh:mm tt");
                    TXT_ESTDURATION.Text = ESTDuration_Taken();

                    CMB_status.SelectedIndex = CMB_status.Items.IndexOf(CMB_status.Items.FindByValue(dtinfo.Rows[0]["JB_STATUS"].ToString().Trim()));
                    TXT_REMX.Text = dtinfo.Rows[0]["JB_REMX"].ToString().Trim();
                    if (CMB_status.SelectedValue.ToString() == "C" || CMB_status.SelectedValue.ToString() == "S")
                    {
                        DateTime dteCLStime = DateTime.Parse(dtinfo.Rows[0]["JB_CLS_JOB_DT"].ToString().Trim());
                        TXT_CLSDATE.Text = dteCLStime.ToString("dd/MM/yyyy");
                        dteCLStime = DateTime.Parse(dtinfo.Rows[0]["JB_CLS_JOB_TM"].ToString().Trim());
                        TXT_CLSTIME.Text = dteCLStime.ToString("hh:mm tt");

                        TXT_DURATION.Text = dtinfo.Rows[0]["JB_DURATION"].ToString().Trim();
                    }
                    else if (CMB_status.SelectedValue.ToString() == "O")
                    {
                        DateTime OPNDATE = DateTime.Parse(TXT_OPNDATE.Text);
                        string OPNTIME = TXT_OPNTIME.Text;
                        DateTime OPNTIMEdt = DateTime.Parse(OPNTIME);
                        dteOPNtime = new DateTime(OPNDATE.Year, OPNDATE.Month, OPNDATE.Day, OPNTIMEdt.Hour, OPNTIMEdt.Minute, OPNTIMEdt.Second);

                        TXT_CLSDATE.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                        TXT_CLSTIME.Text = System.DateTime.Now.ToString("hh:mm tt");
                        DateTime CLSDATE = DateTime.Parse(TXT_CLSDATE.Text);
                        string CLSTIME = TXT_CLSTIME.Text;
                        DateTime CLSTIMEdt = DateTime.Parse(CLSTIME);
                        DateTime dteCLStime = new DateTime(CLSDATE.Year, CLSDATE.Month, CLSDATE.Day, CLSTIMEdt.Hour, CLSTIMEdt.Minute, CLSTIMEdt.Second);

                        TimeSpan TimeSpan = dteCLStime.Subtract(dteOPNtime);

                        if (dteOPNtime > dteCLStime)
                        {
                            mbox("Start Time: " + dteOPNtime.ToString() + "  should earlier than Closing Time: " + dteCLStime.ToString() + " Duration Taken : " + TimeSpan.ToString());
                            // ' Exit Sub
                        }
                        TXT_DURATION.Text = TimeSpan.ToString();
                    }

                    CMB_status.SelectedIndex = 0;
                    LBL_CLSDATE.Visible = true;
                    TXT_CLSDATE.Visible = true;
                    LBL_CLSTIME.Visible = true;
                    TXT_CLSTIME.Visible = true;
                    LBL_DURATION.Visible = true;
                    TXT_DURATION.Visible = true;


                    if (Auth_exist("X") == false)
                    {
                        TXT_ESTCLSDATE.Enabled = false;
                        TXT_ESTCLSTIME.Enabled = false;
                        TXT_ESTDURATION.Enabled = false;
                        TXT_CLSDATE.Enabled = false;
                        TXT_CLSTIME.Enabled = false;
                        TXT_DURATION.Enabled = false;
                        CMB_status.Enabled = true;
                    }
                }
                else
                {
                    BTN_CLEAR_Click(sender, e);
                    mbox("ERROR !!");
                }
            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }
    }


    protected void Gridview1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = Convert.ToInt32(e.RowIndex);
        DataTable dt = Session["JOBTKN_INF_dtINFO_REPORT"] as DataTable;
        string CODE = dt.Rows[index]["JB_SRLNO"].ToString();

        try
        {
            CHBX_EDIT.Checked = true;
            CHBX_EDIT_CheckedChanged(sender, e);
            //            string CODE = (string)GridView1.SelectedDataKey[0];

            string RETRVQRY = "SELECT * FROM JOB_TKN_INFO WHERE JB_SRLNO = '" + CODE + "'";
            DataTable dtinfo = dbo.SelTable(RETRVQRY);
            if (dtinfo.Rows.Count == 1)
            {

                CMB_CODE.SelectedIndex = CMB_CODE.Items.IndexOf(CMB_CODE.Items.FindByValue(CODE));


                TXT_CODE.Text = CODE;
                CMB_CDTYP.SelectedIndex = CMB_CDTYP.Items.IndexOf(CMB_CDTYP.Items.FindByValue(dtinfo.Rows[0]["JB_CODE"].ToString().Trim()));
                CMB_USR_REFSRL.SelectedIndex = CMB_USR_REFSRL.Items.IndexOf(CMB_USR_REFSRL.Items.FindByValue(dtinfo.Rows[0]["JB_USERID"].ToString()));
                TXT_DESC.Text = dtinfo.Rows[0]["JB_DESC"].ToString().Trim();

                DateTime dteOPNtime = DateTime.Parse(dtinfo.Rows[0]["JB_OPN_JOB_DT"].ToString().Trim());
                TXT_OPNDATE.Text = dteOPNtime.ToString("dd/MM/yyyy");
                dteOPNtime = DateTime.Parse(dtinfo.Rows[0]["JB_OPN_JOB_TM"].ToString().Trim());
                TXT_OPNTIME.Text = dteOPNtime.ToString("hh:mm tt");

                DateTime dteESTCLStime = DateTime.Parse(dtinfo.Rows[0]["JB_EST_CLS_DT"].ToString().Trim());
                TXT_ESTCLSDATE.Text = dteESTCLStime.ToString("dd/MM/yyyy");
                dteESTCLStime = DateTime.Parse(dtinfo.Rows[0]["JB_EST_CLS_TM"].ToString().Trim());
                TXT_ESTCLSTIME.Text = dteESTCLStime.ToString("hh:mm tt");
                TXT_ESTDURATION.Text = ESTDuration_Taken();

                CMB_status.SelectedIndex = CMB_status.Items.IndexOf(CMB_status.Items.FindByValue(dtinfo.Rows[0]["JB_STATUS"].ToString().Trim()));
                TXT_REMX.Text = dtinfo.Rows[0]["JB_REMX"].ToString().Trim();
                if (CMB_status.SelectedValue.ToString() == "C" || CMB_status.SelectedValue.ToString() == "S")
                {
                    DateTime dteCLStime = DateTime.Parse(dtinfo.Rows[0]["JB_CLS_JOB_DT"].ToString().Trim());
                    TXT_CLSDATE.Text = dteCLStime.ToString("dd/MM/yyyy");
                    dteCLStime = DateTime.Parse(dtinfo.Rows[0]["JB_CLS_JOB_TM"].ToString().Trim());
                    TXT_CLSTIME.Text = dteCLStime.ToString("hh:mm tt");

                    TXT_DURATION.Text = dtinfo.Rows[0]["JB_DURATION"].ToString().Trim();
                }
                else if (CMB_status.SelectedValue.ToString() == "O")
                {
                    DateTime OPNDATE = DateTime.Parse(TXT_OPNDATE.Text);
                    string OPNTIME = TXT_OPNTIME.Text;
                    DateTime OPNTIMEdt = DateTime.Parse(OPNTIME);
                    dteOPNtime = new DateTime(OPNDATE.Year, OPNDATE.Month, OPNDATE.Day, OPNTIMEdt.Hour, OPNTIMEdt.Minute, OPNTIMEdt.Second);

                    TXT_CLSDATE.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    TXT_CLSTIME.Text = System.DateTime.Now.ToString("hh:mm tt");
                    DateTime CLSDATE = DateTime.Parse(TXT_CLSDATE.Text);
                    string CLSTIME = TXT_CLSTIME.Text;
                    DateTime CLSTIMEdt = DateTime.Parse(CLSTIME);
                    DateTime dteCLStime = new DateTime(CLSDATE.Year, CLSDATE.Month, CLSDATE.Day, CLSTIMEdt.Hour, CLSTIMEdt.Minute, CLSTIMEdt.Second);

                    TimeSpan TimeSpan = dteCLStime.Subtract(dteOPNtime);

                    if (dteOPNtime > dteCLStime)
                    {
                        mbox("Start Time: " + dteOPNtime.ToString() + "  should earlier than Closing Time: " + dteCLStime.ToString() + " Duration Taken : " + TimeSpan.ToString());
                        // ' Exit Sub
                    }
                    TXT_DURATION.Text = TimeSpan.ToString();
                }

                CMB_status.SelectedIndex = 0;
                LBL_CLSDATE.Visible = true;
                TXT_CLSDATE.Visible = true;
                LBL_CLSTIME.Visible = true;
                TXT_CLSTIME.Visible = true;
                LBL_DURATION.Visible = true;
                TXT_DURATION.Visible = true;


                if (Auth_exist("X") == false)
                {
                    TXT_ESTCLSDATE.Enabled = false;
                    TXT_ESTCLSTIME.Enabled = false;
                    TXT_ESTDURATION.Enabled = false;
                    TXT_CLSDATE.Enabled = false;
                    TXT_CLSTIME.Enabled = false;
                    TXT_DURATION.Enabled = false;
                    CMB_status.Enabled = true;
                }
            }
            else
            {
                BTN_CLEAR_Click(sender, e);
                mbox("ERROR !!");
            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }

        BTN_UPDT.Visible = false;
        BTN_DELETE.Visible = true;
    }
    protected void GET_GRID_INFO()
    {
        string USR_dt = "";
        string STS = cmb_Rpt_sts.SelectedValue;
        string USR = "%";
        if (cmb_Rpt_user.SelectedIndex > -1)
        {
            USR = cmb_Rpt_user.SelectedValue.Trim();
        }


        try
        {
            //  ' Dim COMP_CD As string = Session("USER_COMP_CD")
            string RETRVQRY = "SELECT  * , SUBstring(JB_OPN_JOB_DT, 7, 4) + SUBstring(JB_OPN_JOB_DT, 4, 2) + SUBstring(JB_OPN_JOB_DT,1, 2) AS OPDT " +
                                    " FROM JOB_TKN_INFO WHERE  JB_STATUS LIKE '" + STS +
                                    "' AND JB_USERID LIKE '" + USR + "' ORDER BY JB_USERID, OPDT DESC  ";
            DataTable dtinfo = dbo.SelTable(RETRVQRY);
            if (dtinfo.Rows.Count > 0)
            {
                dtinfo.Columns.Add("JB_ESTDURATION");
                dtinfo.Columns.Add("JBT_STATUS");

                foreach (DataRow DR in dtinfo.Rows)
                {
                    USR_dt = DR["JB_SRLNO"] + " " + DR["JB_OPN_JOB_DT"];
                    DateTime OPNDATE = DateTime.Parse(DR["JB_OPN_JOB_DT"].ToString());
                    //'DR["JB_OPN_DT") = OPNTIMEdt.ToString("dd/MM/yyyy")
                    DateTime OPNTIMEdt = DateTime.Parse(DR["JB_OPN_JOB_TM"].ToString());
                    DR["JB_OPN_JOB_TM"] = OPNTIMEdt.ToString("hh:mm tt");

                    OPNTIMEdt = DateTime.Parse(DR["JB_OPN_JOB_TM"].ToString());
                    DateTime dteOPNtime = new DateTime(OPNDATE.Year, OPNDATE.Month, OPNDATE.Day, OPNTIMEdt.Hour, OPNTIMEdt.Minute, OPNTIMEdt.Second);

                    DateTime Nowtime = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.Day, System.DateTime.Now.Hour, System.DateTime.Now.Minute, System.DateTime.Now.Second);

                    if (DR["JB_STATUS"].ToString().Trim() == "C" || DR["JB_STATUS"].ToString().Trim() == "S")
                    {

                        DateTime csDATE = DateTime.Parse(DR["JB_CLS_JOB_DT"].ToString());
                        DateTime csTIMEdt = DateTime.Parse(DR["JB_CLS_JOB_TM"].ToString());
                        Nowtime = new DateTime(csDATE.Year, csDATE.Month, csDATE.Day, csTIMEdt.Hour, csTIMEdt.Minute, csTIMEdt.Second);
                    }
                    else
                    {
                        DR["JB_ESTDURATION"] = "DONE";
                    }
                        DateTime ESTCLSDATE = DateTime.Parse(DR["JB_EST_CLS_DT"].ToString());
                        //'DR["JB_CLS_DT"] = CLSTIMEdt.ToString("dd/MM/yyyy")
                        DateTime ESTCLSTIMEdt = DateTime.Parse(DR["JB_EST_CLS_TM"].ToString());
                        DR["JB_EST_CLS_TM"] = ESTCLSTIMEdt.ToString("hh:mm tt");
                        ESTCLSTIMEdt = DateTime.Parse(DR["JB_EST_CLS_TM"].ToString());
                        DateTime dteESTCLStime = new DateTime(ESTCLSDATE.Year, ESTCLSDATE.Month, ESTCLSDATE.Day, ESTCLSTIMEdt.Hour, ESTCLSTIMEdt.Minute, ESTCLSTIMEdt.Second);
                        TimeSpan TimeSpan = new TimeSpan();
                        TimeSpan = dteESTCLStime.Subtract(Nowtime);

                        string[] estDURATION = TimeSpan.ToString().Split('.');

                        if (estDURATION.Length > 1)
                        {

                            DR["JB_ESTDURATION"] = estDURATION[0] + " DAY " + estDURATION[1] + " TIME ";
                            if (Convert.ToInt32(estDURATION[0]) > 1)
                            {
                                DR["JB_ESTDURATION"] = estDURATION[0] + " DAYS " + estDURATION[1] + " TIME ";
                            }
                        }
                        else
                        {
                            DR["JB_ESTDURATION"] = estDURATION[0] + " TIME ";
                        }

                    DR["JBT_STATUS"] = CMB_status.Items.FindByValue(DR["JB_STATUS"].ToString().Trim()).Text;

                    if (DR["JB_STATUS"].ToString().Trim() == "C" || DR["JB_STATUS"].ToString().Trim() == "S")
                    {
                        DateTime CLSTIMEdt = DateTime.Parse(DR["JB_CLS_JOB_DT"].ToString());
                        // 'DR["JB_CLS_DT") = CLSTIMEdt.ToString("dd/MM/yyyy")
                        CLSTIMEdt = DateTime.Parse(DR["JB_CLS_JOB_TM"].ToString());
                        DR["JB_CLS_JOB_TM"] = CLSTIMEdt.ToString("hh:mm tt");

                        string[] DURATION = DR["JB_DURATION"].ToString().Split('.');
                        if (DURATION.Length > 1)
                        {
                            DR["JB_DURATION"] = DURATION[0] + " DAY " + DURATION[1] + " TIME ";
                            if (Convert.ToInt32(DURATION[0]) > 1)
                            {
                                DR["JB_DURATION"] = DURATION[0] + " DAYS " + DURATION[1] + " TIME ";
                            }
                        }
                        else
                        {
                            if (DURATION[0].Length > 1)
                            {
                                DR["JB_DURATION"] = DURATION[0] + " TIME ";
                            }
                        }
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

            Session["JOBTKN_INF_dtINFO_REPORT"] = dtinfo;
        }
        catch (Exception EX)
        {
            mbox(EX.Message + System.DateTime.Today + USR_dt + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }
    }
    public void GridView1_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string JB_USERID = (string)DataBinder.Eval(e.Row.DataItem, "JB_USERID").ToString();
            if (JB_USERID == null)
            {
                e.Row.BackColor = System.Drawing.Color.DarkGray;
                e.Row.ForeColor = System.Drawing.Color.Black;
                e.Row.Font.Bold = true;
            }
            else
            {

                if (JB_USERID.Trim() == "PRIYA")
                {
                    e.Row.ForeColor = System.Drawing.Color.HotPink;
                }
                else if (JB_USERID.Trim() == "MANNAN")
                {
                    e.Row.ForeColor = System.Drawing.Color.DarkSeaGreen;
                }
                else if (JB_USERID.Trim() == "VANESSA")
                {
                    e.Row.ForeColor = System.Drawing.Color.DarkCyan;
                }
            }

            string JB_STS = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "JB_STATUS").ToString());
            if (JB_STS.Substring(0, 1) == "C")
            {
                e.Row.ForeColor = System.Drawing.Color.Black;
            }

            string JB_ESTDURATION = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "JB_ESTDURATION").ToString());
            if (JB_ESTDURATION.Substring(0, 1) == "-")
            {
                e.Row.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    public void Button1_Click(object sender, System.EventArgs e) //Handles Button1.Click
    {

        ReportDocument rpt = new ReportDocument();
        string rname = Server.MapPath("~/Reports/TASK_CNTRLPDF.rpt");
        string dwnldfname = Server.MapPath("~/ReportsGenerate/TASK_CNTRLPDF.pdf");
        DataTable dt = (DataTable)Session["JOBTKN_INF_dtINFO_REPORT"];

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
            Response.AddHeader("content-disposition", "attachment;filename =" + "TASK_CNTRLRPT.pdf");
            Response.ContentType = "application/pdf";
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
        dbo.ErrorLog(USR + " : JOB_CMS_EDIT.aspx : " + val, Server.MapPath("~\\Logs\\ErrorLog"));
    }



    public void TXT_OPNDATE_TextChanged(Object sender, System.EventArgs e)// Handles TXT_OPNDATE.TextChanged
    {
        if (TXT_OPNTIME.Text.Length == 0)
        {
            TXT_OPNTIME.Text = "09:00 AM";
        }
        TXT_ESTDURATION.Text = ESTDuration_Taken();
        if (CMB_status.SelectedValue.ToString() == "C" || CMB_status.SelectedValue.ToString() == "S")
        {
            TXT_DURATION.Text = Duration_Taken();
        }
    }

    public void TXT_OPNTIME_TextChanged(Object sender, System.EventArgs e)// Handles TXT_OPNTIME.TextChanged
    {
        TXT_ESTDURATION.Text = ESTDuration_Taken();
        if (CMB_status.SelectedValue.ToString() == "C" || CMB_status.SelectedValue.ToString() == "S")
        {
            TXT_DURATION.Text = Duration_Taken();
        }
    }


    protected void TXT_ESTCLSDATE_TextChanged(Object sender, System.EventArgs e) //Handles TXT_ESTCLSDATE.TextChanged
    {
        if (TXT_ESTCLSTIME.Text.Length == 0)
        {
            TXT_ESTCLSTIME.Text = "12:00 PM";
        }
        TXT_ESTDURATION.Text = ESTDuration_Taken();
    }

    protected void TXT_ESTCLSTIME_TextChanged(object sender, System.EventArgs e) //Handles TXT_ESTCLSTIME.TextChanged
    {
        TXT_ESTDURATION.Text = ESTDuration_Taken();
    }


    protected void TXT_CLSDATE_TextChanged(object sender, System.EventArgs e) ///Handles TXT_CLSDATE.TextChanged
    {
        if (CMB_status.SelectedValue.ToString() == "C" || CMB_status.SelectedValue.ToString() == "S")
        {
            TXT_DURATION.Text = Duration_Taken();
        }
    }

    protected void TXT_CLSTIME_TextChanged(object sender, System.EventArgs e)// Handles TXT_CLSTIME.TextChanged
    {
        if (CMB_status.SelectedValue.ToString() == "C" || CMB_status.SelectedValue.ToString() == "S")
        {
            TXT_DURATION.Text = Duration_Taken();
        }
    }

    protected bool Auth_exist(string User)
    {
        User = (string)Session["USER_LOGGED"];
        if (User.ToUpper() == "DEV_R" || User.ToUpper() == "PRIYA")
        {
            //'User.ToUpper = "DEV_R" Or
            return true;
        }
        return false;
    }

    protected void cmb_Rpt_user_SelectedIndexChanged(object sender, System.EventArgs e)// Handles cmb_Rpt_user.SelectedIndexChanged
    {
        GET_GRID_INFO();
    }

    protected void cmb_Rpt_sts_SelectedIndexChanged(object sender, System.EventArgs e) //Handles cmb_Rpt_sts.SelectedIndexChanged
    {
        GET_GRID_INFO();
    }

    protected bool send_mail()//String attchmentfile)
    {
        //  String pc   = cmbPC.SelectedItem.Text.ToString.Trim;
        String en = User.Identity.Name.ToLower();
        String ToMail = (String)Session["USER_MAIL_ID"];


        MailMessage msg = new MailMessage();
        SmtpClient client = new SmtpClient();

        // You can specify the host name or ipaddress of your server
        // Default in IIS will be localhost
        // client.Host = "localhost";

        //Default port will be 25
        client.Port = 587;
        client.Host = "smtp.gmail.com";
        msg.From = new MailAddress("Notification.fkminvest@gmail.com", "FKM INVEST Support");

        //String[] tomaillist   = ToMail.Split(',');
        //foreach(string mailid in tomaillist)
        //{
        //    msg.To.Add(new MailAddress(mailid));
        //}
        msg.To.Add(new MailAddress(ToMail));
        //  msg.Bcc.Add(new MailAddress("PRIYA@FKMINVEST.com"));
        msg.IsBodyHtml = true;
        //msg.Attachments.Add(new Attachment(attchmentfile));
        client.UseDefaultCredentials = false;
        client.Credentials = new NetworkCredential("Notification.fkminvest@gmail.com", "FKMINV*123");
        client.EnableSsl = true;
        msg.IsBodyHtml = true;
        msg.Subject = "NEW TASK REQUEST(" + TXT_CODE.Text + ") IS GENERATED";

        StreamReader reader = new StreamReader(Server.MapPath("~/Reports/MAIL_TASK.htm"));
        String myString = reader.ReadToEnd();
        reader.Close();
        reader.Dispose();

        myString = myString.Replace("$JB_SRLNO", TXT_CODE.Text.ToUpper().Trim());
        myString = myString.Replace("$JB_CODE", CMB_CDTYP.Text.ToString().ToUpper());
        myString = myString.Replace("$JB_USERID", CMB_USR_REFSRL.Text.ToString().ToUpper());
        myString = myString.Replace("$JB_DESC", TXT_DESC.Text.ToUpper());
        myString = myString.Replace("$JB_REMX", TXT_REMX.Text.ToUpper());

        myString = myString.Replace("$JB_OPN_JOB_DT", TXT_OPNDATE.Text);
        myString = myString.Replace("$JB_OPN_JOB_TM", TXT_OPNTIME.Text);

        myString = myString.Replace("$JB_EST_CLS_DT", TXT_ESTCLSDATE.Text);
        myString = myString.Replace("$JB_EST_CLS_TM", TXT_ESTCLSTIME.Text);

        string est_DURATION = "";
        string[] estDURATION = TXT_ESTDURATION.Text.ToString().Split('.');

        if (estDURATION.Length > 1)
        {

            est_DURATION = estDURATION[0] + " DAY " + estDURATION[1] + " TIME ";
            if (Convert.ToInt32(estDURATION[0]) > 1)
            {
                est_DURATION = estDURATION[0] + " DAYS " + estDURATION[1] + " TIME ";
            }
        }
        else
        {
            est_DURATION = estDURATION[0] + " TIME ";
        }
        myString = myString.Replace("$JB_ESTDURATION", est_DURATION);

        myString = myString.Replace("$JB_CLS_JOB_DT", TXT_CLSDATE.Text);
        myString = myString.Replace("$JB_CLS_JOB_TM", TXT_CLSTIME.Text);

        string t_DURATION = "";
        string[] tDURATION = TXT_DURATION.Text.ToString().Split('.');

        if (tDURATION.Length > 1)
        {

            t_DURATION = tDURATION[0] + " DAY " + tDURATION[1] + " TIME ";
            if (Convert.ToInt32(estDURATION[0]) > 1)
            {
                t_DURATION = tDURATION[0] + " DAYS " + tDURATION[1] + " TIME ";
            }
        }
        else
        {
            if (tDURATION[0].Length > 1)
            {

                t_DURATION = tDURATION[0] + " TIME ";
            }
        }
        myString = myString.Replace("$JB_DURATION", t_DURATION);

        myString = myString.Replace("$USER", User.Identity.Name.ToLower());

        msg.Body = myString.ToString();


        //'msg.Body = "<html><body><p>Dear " & en & " ,</p><p><b> " & _
        //'          "You have Submitted Claims Data into Al-Ahleia System with Batch ID as " & txt_batch_id.Text & ". " &
        //'          "</p><br /><p><b> Please note that you have recieved a  Medical Batch Data Submission Excel Report attached to  this e-mail.  <b></p>" & _
        //'          "<p> Thank you  <br /> <b> - AIC Support</p></body></html>" & _
        //'          "<p><a href='http://aicweb.Alahleia.com/AICMED/Reports/NPMedClmQry.aspx'> Please Click this Link to view   </a> " & _
        //'          " <P><FONT COLOR=#C0C0C0> *** Please do not reply to this message via e-mail. This address is automated, unattended, and cannot help with questions or requests.</FONT></P><hr/></body></html>"
        try
        {
            client.Send(msg);
        }
        catch (Exception ex)
        {
            mbox("Error : " + ex.ToString());
            return false;
        }
        msg.Dispose();
        return true;
    }

    protected bool send_mail_task_del()//String attchmentfile)
    {
        //  String pc   = cmbPC.SelectedItem.Text.ToString.Trim;
        String en = User.Identity.Name.ToLower();
        String ToMail = (String)Session["USER_MAIL_ID"];


        MailMessage msg = new MailMessage();
        SmtpClient client = new SmtpClient();

        // You can specify the host name or ipaddress of your server
        // Default in IIS will be localhost
        // client.Host = "localhost";

        //Default port will be 25
        client.Port = 587;
        client.Host = "smtp.gmail.com";
        msg.From = new MailAddress("Notification.fkminvest@gmail.com", "FKM INVEST Support");

        //String[] tomaillist   = ToMail.Split(',');
        //foreach(string mailid in tomaillist)
        //{
        //    msg.To.Add(new MailAddress(mailid));
        //}
        msg.To.Add(new MailAddress(ToMail));
        //  msg.Bcc.Add(new MailAddress("PRIYA@FKMINVEST.com"));
        msg.IsBodyHtml = true;
        //msg.Attachments.Add(new Attachment(attchmentfile));
        client.UseDefaultCredentials = false;
        client.Credentials = new NetworkCredential("Notification.fkminvest@gmail.com", "FKMINV*123");
        client.EnableSsl = true;
        msg.IsBodyHtml = true;
        msg.Subject = "TASK DELETE REQUEST(" + TXT_CODE.Text + ") IS GENERATED";

        StreamReader reader = new StreamReader(Server.MapPath("~/Reports/MAIL_TASK_DEL.htm"));
        String myString = reader.ReadToEnd();
        reader.Close();
        reader.Dispose();

        myString = myString.Replace("$JB_SRLNO", TXT_CODE.Text.ToUpper().Trim());
        myString = myString.Replace("$JB_CODE", CMB_CDTYP.Text.ToString().ToUpper());
        myString = myString.Replace("$JB_USERID", CMB_USR_REFSRL.Text.ToString().ToUpper());
        myString = myString.Replace("$JB_DESC", TXT_DESC.Text.ToUpper());
        myString = myString.Replace("$JB_REMX", TXT_REMX.Text.ToUpper());

        myString = myString.Replace("$JB_OPN_JOB_DT", TXT_OPNDATE.Text);
        myString = myString.Replace("$JB_OPN_JOB_TM", TXT_OPNTIME.Text);

        myString = myString.Replace("$JB_EST_CLS_DT", TXT_ESTCLSDATE.Text);
        myString = myString.Replace("$JB_EST_CLS_TM", TXT_ESTCLSTIME.Text);

        string est_DURATION = "";
        string[] estDURATION = TXT_ESTDURATION.Text.ToString().Split('.');

        if (estDURATION.Length > 1)
        {

            est_DURATION = estDURATION[0] + " DAY " + estDURATION[1] + " TIME ";
            if (Convert.ToInt32(estDURATION[0]) > 1)
            {
                est_DURATION = estDURATION[0] + " DAYS " + estDURATION[1] + " TIME ";
            }
        }
        else
        {
            est_DURATION = estDURATION[0] + " TIME ";
        }
        myString = myString.Replace("$JB_ESTDURATION", est_DURATION);

        myString = myString.Replace("$JB_CLS_JOB_DT", TXT_CLSDATE.Text);
        myString = myString.Replace("$JB_CLS_JOB_TM", TXT_CLSTIME.Text);

        string t_DURATION = "";
        string[] tDURATION = TXT_DURATION.Text.ToString().Split('.');

        if (tDURATION.Length > 1)
        {

            t_DURATION = tDURATION[0] + " DAY " + tDURATION[1] + " TIME ";
            if (Convert.ToInt32(estDURATION[0]) > 1)
            {
                t_DURATION = tDURATION[0] + " DAYS " + tDURATION[1] + " TIME ";
            }
        }
        else
        {
            if (tDURATION[0].Length > 1)
            {

                t_DURATION = tDURATION[0] + " TIME ";
            }
        }
        myString = myString.Replace("$JB_DURATION", t_DURATION);

        myString = myString.Replace("$USER", User.Identity.Name.ToLower());

        msg.Body = myString.ToString();


        //'msg.Body = "<html><body><p>Dear " & en & " ,</p><p><b> " & _
        //'          "You have Submitted Claims Data into Al-Ahleia System with Batch ID as " & txt_batch_id.Text & ". " &
        //'          "</p><br /><p><b> Please note that you have recieved a  Medical Batch Data Submission Excel Report attached to  this e-mail.  <b></p>" & _
        //'          "<p> Thank you  <br /> <b> - AIC Support</p></body></html>" & _
        //'          "<p><a href='http://aicweb.Alahleia.com/AICMED/Reports/NPMedClmQry.aspx'> Please Click this Link to view   </a> " & _
        //'          " <P><FONT COLOR=#C0C0C0> *** Please do not reply to this message via e-mail. This address is automated, unattended, and cannot help with questions or requests.</FONT></P><hr/></body></html>"
        try
        {
            client.Send(msg);
        }
        catch (Exception ex)
        {
            mbox("Error : " + ex.ToString());
            return false;
        }
        msg.Dispose();
        return true;
    }
    public void BTN_mail_Click(object sender, System.EventArgs e) //Handles BTN_CLEAR.Click
    {
        SEND_TASK_INFO();
        SEND_LAPSED_TASK_INFO();
    }
    protected void SEND_TASK_INFO()
    {
        string mail_strinG = "";
        string USR_dt = "";
        string STS = "O";// cmb_Rpt_sts.SelectedValue;
        string USR = "%";
        if (cmb_Rpt_user.SelectedIndex > -1)
        {
            USR = cmb_Rpt_user.SelectedValue.Trim();
        }

        string DTE = System.DateTime.Now.ToString("yyyyMMdd");
        string DTETMRW = System.DateTime.Now.AddDays(1).ToString("yyyyMMdd");
        try
        {
            string usrQRY = "SELECT DISTINCT USR_USERID, USR_NAME, USR_EMAILID FROM USER_INFO where USR_STATUS = 'A'  ORDER BY USR_NAME ";
            DataTable dtusrinfo = dbo.SelTable(usrQRY);
            dtusrinfo.Rows.Add("%", "MR. LUIS", "LUIS@FKMINVEST.COM");
            if (dtusrinfo.Rows.Count > 0)
            {
                foreach (DataRow DR1 in dtusrinfo.Rows)
                {
                    mail_strinG = "";
                    string RETRVQRY = "SELECT  * , SUBstring(JB_OPN_JOB_DT, 7, 4) + SUBstring(JB_OPN_JOB_DT, 4, 2) + SUBstring(JB_OPN_JOB_DT,1, 2) AS OPDT," +
                                        " SUBstring(JB_EST_CLS_DT, 7, 4) + SUBstring(JB_EST_CLS_DT, 4, 2) + SUBstring(JB_EST_CLS_DT,1, 2) AS ESTDT, " +
                                        "  SUBstring(JB_CLS_JOB_DT, 7, 4) + SUBstring(JB_CLS_JOB_DT, 4, 2) + SUBstring(JB_CLS_JOB_DT,1, 2) AS CLSDT " +
                                        " FROM JOB_TKN_INFO WHERE  JB_STATUS LIKE '" + STS + "' AND  JB_USERID LIKE '" + DR1["USR_USERID"].ToString().Trim() +
                                        "' AND  (SUBstring(JB_EST_CLS_DT, 7, 4) + SUBstring(JB_EST_CLS_DT, 4, 2) + SUBstring(JB_EST_CLS_DT,1, 2) = '" + DTE +
                                        "' OR SUBstring(JB_EST_CLS_DT, 7, 4) + SUBstring(JB_EST_CLS_DT, 4, 2) + SUBstring(JB_EST_CLS_DT,1, 2) = '" + DTETMRW + "' ) ORDER BY JB_USERID, OPDT DESC  ";
                    DataTable dtinfo = dbo.SelTable(RETRVQRY);
                    if (dtinfo.Rows.Count > 0)
                    {
                        dtinfo.Columns.Add("JB_ESTDURATION");
                        dtinfo.Columns.Add("JBT_STATUS");

                        foreach (DataRow DR in dtinfo.Rows)
                        {
                            USR_dt = DR["JB_SRLNO"] + " " + DR["JB_OPN_JOB_DT"];
                            DateTime OPNDATE = DateTime.Parse(DR["JB_OPN_JOB_DT"].ToString());
                            //'DR["JB_OPN_DT") = OPNTIMEdt.ToString("dd/MM/yyyy")
                            DateTime OPNTIMEdt = DateTime.Parse(DR["JB_OPN_JOB_TM"].ToString());
                            DR["JB_OPN_JOB_TM"] = OPNTIMEdt.ToString("hh:mm tt");

                            OPNTIMEdt = DateTime.Parse(DR["JB_OPN_JOB_TM"].ToString());
                            DateTime dteOPNtime = new DateTime(OPNDATE.Year, OPNDATE.Month, OPNDATE.Day, OPNTIMEdt.Hour, OPNTIMEdt.Minute, OPNTIMEdt.Second);

                            if (DR["JB_STATUS"].ToString().Trim() != "C")
                            {
                                DateTime ESTCLSDATE = DateTime.Parse(DR["JB_EST_CLS_DT"].ToString());
                                //'DR["JB_CLS_DT"] = CLSTIMEdt.ToString("dd/MM/yyyy")
                                DateTime ESTCLSTIMEdt = DateTime.Parse(DR["JB_EST_CLS_TM"].ToString());
                                DR["JB_EST_CLS_TM"] = ESTCLSTIMEdt.ToString("hh:mm tt");
                                ESTCLSTIMEdt = DateTime.Parse(DR["JB_EST_CLS_TM"].ToString());
                                DateTime dteESTCLStime = new DateTime(ESTCLSDATE.Year, ESTCLSDATE.Month, ESTCLSDATE.Day, ESTCLSTIMEdt.Hour, ESTCLSTIMEdt.Minute, ESTCLSTIMEdt.Second);
                                DateTime Nowtime = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.Day, System.DateTime.Now.Hour, System.DateTime.Now.Minute, System.DateTime.Now.Second);
                                TimeSpan TimeSpan = new TimeSpan();
                                TimeSpan = dteESTCLStime.Subtract(Nowtime);

                                string[] estDURATION = TimeSpan.ToString().Split('.');

                                if (estDURATION.Length > 1)
                                {

                                    DR["JB_ESTDURATION"] = estDURATION[0] + " DAY " + estDURATION[1] + " TIME ";
                                    if (Convert.ToInt32(estDURATION[0]) > 1)
                                    {
                                        DR["JB_ESTDURATION"] = estDURATION[0] + " DAYS " + estDURATION[1] + " TIME ";
                                    }
                                }
                                else
                                {
                                    DR["JB_ESTDURATION"] = estDURATION[0] + " TIME ";
                                }
                            }
                            else
                            {
                                DR["JB_ESTDURATION"] = "DONE";
                            }

                            DR["JBT_STATUS"] = CMB_status.Items.FindByValue(DR["JB_STATUS"].ToString().Trim()).Text;

                            if (DR["JB_STATUS"].ToString().Trim() == "C" || DR["JB_STATUS"].ToString().Trim() == "S")
                            {
                                DateTime CLSTIMEdt = DateTime.Parse(DR["JB_CLS_JOB_DT"].ToString());
                                // 'DR["JB_CLS_DT") = CLSTIMEdt.ToString("dd/MM/yyyy")
                                CLSTIMEdt = DateTime.Parse(DR["JB_CLS_JOB_TM"].ToString());
                                DR["JB_CLS_JOB_TM"] = CLSTIMEdt.ToString("hh:mm tt");

                                string[] DURATION = DR["JB_DURATION"].ToString().Split('.');
                                if (DURATION.Length > 1)
                                {
                                    DR["JB_DURATION"] = DURATION[0] + " DAY " + DURATION[1] + " TIME ";
                                    if (Convert.ToInt32(DURATION[0]) > 1)
                                    {
                                        DR["JB_DURATION"] = DURATION[0] + " DAYS " + DURATION[1] + " TIME ";
                                    }
                                    else
                                    {
                                        if (DURATION[0].Length > 1)
                                        {
                                            DR["JB_DURATION"] = DURATION[0] + " TIME ";
                                        }
                                    }
                                }
                            }

                            //mail_strinG = mail_strinG + " <tr> <td> " + DR["JB_SRLNO"] + "</td><td>" + DR["JB_CODE"] + " </td>  <td>" + DR["JB_DESC"] +
                            //                            " </td>  <td>" + DR["JB_USERID"] + " </td>  <td>" + DR["JB_OPN_JOB_DT"] +
                            //                            " </td>  <td>" + DR["JB_OPN_JOB_TM"] + " </td>  <td>" + DR["JB_EST_CLS_DT"] +
                            //                            " </td>  <td>" + DR["JB_EST_CLS_TM"] + " </td>  <td>" + DR["JB_ESTDURATION"] +
                            //                            " </td>  <td>" + DR["JBT_STATUS"] + " </td>  <td>" + DR["JB_CLS_JOB_DT"] +
                            //                            " </td>  <td>" + DR["JB_CLS_JOB_TM"] + " </td>  <td>" + DR["JB_DURATION"] + 
                            //                            " </td> <td>" + DR["JB_REMX"] + " </td> </tr> ";


                            mail_strinG = mail_strinG + " <tr> <td style=' text-align: center;'> " + DR["JB_SRLNO"] + "</td><td style=' text-align: center;'>" + DR["JB_CODE"] +
                                                        " </td>  <td style=' text-align: center;'>" + DR["JB_DESC"] +
                                                        " </td>  <td style=' text-align: center;'>" + DR["JB_USERID"] + " </td>  <td>" + DR["JB_OPN_JOB_DT"] +
                                                        " </td>  <td>" + DR["JB_OPN_JOB_TM"] + " </td>  <td>" + DR["JB_EST_CLS_DT"] +
                                                        " </td>  <td>" + DR["JB_EST_CLS_TM"] + " </td>  <td>" + DR["JB_ESTDURATION"] +
                                                        " </td>  <td  style=' text-align: center;'>" + DR["JBT_STATUS"] + " </td>  <td  style=' text-align: center;'>" + DR["JB_REMX"] + " </td> </tr> ";
                        }

                        StreamReader reader = new StreamReader(Server.MapPath("~/Reports/SEND_NTF_MAIL.htm"));
                        String myString = reader.ReadToEnd();
                        reader.Close();
                        reader.Dispose();
                        myString = myString.Replace("$TASK_CNTRL", mail_strinG);
                        myString = myString.Replace("$USR_NAME", DR1["USR_NAME"].ToString());
                        dbo.send_NTF_mail(DR1["USR_EMAILID"].ToString(), "TASKS ESTIMATED TO CLOSE (" + System.DateTime.Now.ToShortDateString() + "/" + System.DateTime.Now.AddDays(1).ToShortDateString() + ").", myString, "");
                    }
                }
                //GridView1.DataSource = dtinfo;
                //GridView1.DataBind();
            }
            else
            {

                //GridView1.DataSource = null;
                //GridView1.DataBind();
            }
            //Session["JOBTKN_INF_dtINFO_REPORT"] = dtinfo;
        }
        catch (Exception EX)
        {
            mbox(EX.Message + System.DateTime.Today + USR_dt + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }
    }
    protected void SEND_LAPSED_TASK_INFO()
    {
        string mail_strinG = "";
        string USR_dt = "";
        string STS = "O";// cmb_Rpt_sts.SelectedValue;
        string USR = "%";
        if (cmb_Rpt_user.SelectedIndex > -1)
        {
            USR = cmb_Rpt_user.SelectedValue.Trim();
        }

        string DTE = System.DateTime.Now.ToString("yyyyMMdd");
        try
        {
            string usrQRY = "SELECT DISTINCT USR_USERID, USR_NAME, USR_EMAILID FROM USER_INFO where USR_STATUS = 'A'  ORDER BY USR_NAME ";
            DataTable dtusrinfo = dbo.SelTable(usrQRY);
            dtusrinfo.Rows.Add("%", "MR. LUIS", "LUIS@FKMINVEST.COM");
            if (dtusrinfo.Rows.Count > 0)
            {
                foreach (DataRow DR1 in dtusrinfo.Rows)
                {
                    mail_strinG = "";
                    string RETRVQRY = "SELECT  * , SUBstring(JB_OPN_JOB_DT, 7, 4) + SUBstring(JB_OPN_JOB_DT, 4, 2) + SUBstring(JB_OPN_JOB_DT,1, 2) AS OPDT," +
                                        " SUBstring(JB_EST_CLS_DT, 7, 4) + SUBstring(JB_EST_CLS_DT, 4, 2) + SUBstring(JB_EST_CLS_DT,1, 2) AS ESTDT, " +
                                        "  SUBstring(JB_CLS_JOB_DT, 7, 4) + SUBstring(JB_CLS_JOB_DT, 4, 2) + SUBstring(JB_CLS_JOB_DT,1, 2) AS CLSDT " +
                                        " FROM JOB_TKN_INFO WHERE  JB_STATUS LIKE '" + STS + "' AND  JB_USERID LIKE '" + DR1["USR_USERID"].ToString().Trim() +
                                        "' AND  SUBstring(JB_EST_CLS_DT, 7, 4) + SUBstring(JB_EST_CLS_DT, 4, 2) + SUBstring(JB_EST_CLS_DT,1, 2) < '" + DTE + "' ORDER BY JB_USERID, OPDT DESC  ";
                    DataTable dtinfo = dbo.SelTable(RETRVQRY);
                    if (dtinfo.Rows.Count > 0)
                    {
                        dtinfo.Columns.Add("JB_ESTDURATION");
                        dtinfo.Columns.Add("JBT_STATUS");

                        foreach (DataRow DR in dtinfo.Rows)
                        {
                            USR_dt = DR["JB_SRLNO"] + " " + DR["JB_OPN_JOB_DT"];
                            DateTime OPNDATE = DateTime.Parse(DR["JB_OPN_JOB_DT"].ToString());
                            //'DR["JB_OPN_DT") = OPNTIMEdt.ToString("dd/MM/yyyy")
                            DateTime OPNTIMEdt = DateTime.Parse(DR["JB_OPN_JOB_TM"].ToString());
                            DR["JB_OPN_JOB_TM"] = OPNTIMEdt.ToString("hh:mm tt");

                            OPNTIMEdt = DateTime.Parse(DR["JB_OPN_JOB_TM"].ToString());
                            DateTime dteOPNtime = new DateTime(OPNDATE.Year, OPNDATE.Month, OPNDATE.Day, OPNTIMEdt.Hour, OPNTIMEdt.Minute, OPNTIMEdt.Second);

                            if (DR["JB_STATUS"].ToString().Trim() != "C")
                            {
                                DateTime ESTCLSDATE = DateTime.Parse(DR["JB_EST_CLS_DT"].ToString());
                                //'DR["JB_CLS_DT"] = CLSTIMEdt.ToString("dd/MM/yyyy")
                                DateTime ESTCLSTIMEdt = DateTime.Parse(DR["JB_EST_CLS_TM"].ToString());
                                DR["JB_EST_CLS_TM"] = ESTCLSTIMEdt.ToString("hh:mm tt");
                                ESTCLSTIMEdt = DateTime.Parse(DR["JB_EST_CLS_TM"].ToString());
                                DateTime dteESTCLStime = new DateTime(ESTCLSDATE.Year, ESTCLSDATE.Month, ESTCLSDATE.Day, ESTCLSTIMEdt.Hour, ESTCLSTIMEdt.Minute, ESTCLSTIMEdt.Second);
                                DateTime Nowtime = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.Day, System.DateTime.Now.Hour, System.DateTime.Now.Minute, System.DateTime.Now.Second);
                                TimeSpan TimeSpan = new TimeSpan();
                                TimeSpan = dteESTCLStime.Subtract(Nowtime);

                                string[] estDURATION = TimeSpan.ToString().Split('.');

                                if (estDURATION.Length > 1)
                                {

                                    DR["JB_ESTDURATION"] = estDURATION[0] + " DAY " + estDURATION[1] + " TIME ";
                                    if (Convert.ToInt32(estDURATION[0]) > 1)
                                    {
                                        DR["JB_ESTDURATION"] = estDURATION[0] + " DAYS " + estDURATION[1] + " TIME ";
                                    }
                                }
                                else
                                {
                                    DR["JB_ESTDURATION"] = estDURATION[0] + " TIME ";
                                }
                            }
                            else
                            {
                                DR["JB_ESTDURATION"] = "DONE";
                            }

                            DR["JBT_STATUS"] = CMB_status.Items.FindByValue(DR["JB_STATUS"].ToString().Trim()).Text;

                            if (DR["JB_STATUS"].ToString().Trim() == "C" || DR["JB_STATUS"].ToString().Trim() == "S")
                            {
                                DateTime CLSTIMEdt = DateTime.Parse(DR["JB_CLS_JOB_DT"].ToString());
                                // 'DR["JB_CLS_DT") = CLSTIMEdt.ToString("dd/MM/yyyy")
                                CLSTIMEdt = DateTime.Parse(DR["JB_CLS_JOB_TM"].ToString());
                                DR["JB_CLS_JOB_TM"] = CLSTIMEdt.ToString("hh:mm tt");

                                string[] DURATION = DR["JB_DURATION"].ToString().Split('.');
                                if (DURATION.Length > 1)
                                {
                                    DR["JB_DURATION"] = DURATION[0] + " DAY " + DURATION[1] + " TIME ";
                                    if (Convert.ToInt32(DURATION[0]) > 1)
                                    {
                                        DR["JB_DURATION"] = DURATION[0] + " DAYS " + DURATION[1] + " TIME ";
                                    }
                                    else
                                    {
                                        if (DURATION[0].Length > 1)
                                        {
                                            DR["JB_DURATION"] = DURATION[0] + " TIME ";
                                        }
                                    }
                                }
                            }

                            //mail_strinG = mail_strinG + " <tr> <td> " + DR["JB_SRLNO"] + "</td><td>" + DR["JB_CODE"] + " </td>  <td>" + DR["JB_DESC"] +
                            //                            " </td>  <td>" + DR["JB_USERID"] + " </td>  <td>" + DR["JB_OPN_JOB_DT"] +
                            //                            " </td>  <td>" + DR["JB_OPN_JOB_TM"] + " </td>  <td>" + DR["JB_EST_CLS_DT"] +
                            //                            " </td>  <td>" + DR["JB_EST_CLS_TM"] + " </td>  <td>" + DR["JB_ESTDURATION"] +
                            //                            " </td>  <td>" + DR["JBT_STATUS"] + " </td>  <td>" + DR["JB_CLS_JOB_DT"] +
                            //                            " </td>  <td>" + DR["JB_CLS_JOB_TM"] + " </td>  <td>" + DR["JB_DURATION"] + 
                            //                            " </td> <td>" + DR["JB_REMX"] + " </td> </tr> ";


                            mail_strinG = mail_strinG + " <tr> <td style=' text-align: center;'> " + DR["JB_SRLNO"] + "</td><td style=' text-align: center;'>" + DR["JB_CODE"] +
                                                        " </td>  <td style=' text-align: center;'>" + DR["JB_DESC"] +
                                                        " </td>  <td style=' text-align: center;'>" + DR["JB_USERID"] + " </td>  <td>" + DR["JB_OPN_JOB_DT"] +
                                                        " </td>  <td>" + DR["JB_OPN_JOB_TM"] + " </td>  <td>" + DR["JB_EST_CLS_DT"] +
                                                        " </td>  <td>" + DR["JB_EST_CLS_TM"] + " </td>  <td>" + DR["JB_ESTDURATION"] +
                                                        " </td>  <td  style=' text-align: center;'>" + DR["JBT_STATUS"] + " </td>  <td  style=' text-align: center;'>" + DR["JB_REMX"] + " </td> </tr> ";
                        }

                        StreamReader reader = new StreamReader(Server.MapPath("~/Reports/SEND_LPSD_NTF_MAIL.htm"));
                        String myString = reader.ReadToEnd();
                        reader.Close();
                        reader.Dispose();
                        myString = myString.Replace("$TASK_CNTRL", mail_strinG);
                        myString = myString.Replace("$USR_NAME", DR1["USR_NAME"].ToString());
                        dbo.send_NTF_mail(DR1["USR_EMAILID"].ToString(), "TASKS ELAPSED BEYOND DATE " + System.DateTime.Now.ToShortDateString() + ".", myString, "");
                    }
                }
                //GridView1.DataSource = dtinfo;
                //GridView1.DataBind();
            }
            else
            {

                //GridView1.DataSource = null;
                //GridView1.DataBind();
            }
            //Session["JOBTKN_INF_dtINFO_REPORT"] = dtinfo;
        }
        catch (Exception EX)
        {
            mbox(EX.Message + System.DateTime.Today + USR_dt + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }
    }
}