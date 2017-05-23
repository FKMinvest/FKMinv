using System.Data;
using System.IO;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class Tools_REF_EDIT : Page
{
    fkminvcom dbo = new fkminvcom();

    // Visual C# .NET requires that you override the OnInit function,
    // adding a new delegate for the Page_Load event.

    override protected void OnInit(EventArgs e)
    {
        this.Load += new System.EventHandler(this.Page_Load);
        base.OnInit(e);
        CMB_CODE.SelectedIndexChanged += new System.EventHandler(CMB_CODE_SelectedIndexChanged);
        CMB_FLDTYP.SelectedIndexChanged += new System.EventHandler(CMB_FLDTYP_SelectedIndexChanged);
        CHBX_EDIT.CheckedChanged += new System.EventHandler(CHBX_EDIT_CheckedChanged);
        GridView1.RowDeleting += new System.Web.UI.WebControls.GridViewDeleteEventHandler(Gridview1_RowDeleting);
        GridView1.SelectedIndexChanged += new System.EventHandler(GridView1_SelectedIndexChanged);
        TXT_SRCH.TextChanged += new System.EventHandler(TXT_SRCH_TextChanged);

        BTN_ADD.Click += new System.EventHandler(BTN_ADD_Click);
        BTN_UPDT.Click += new System.EventHandler(BTN_UPDT_Click);
        BTN_DELETE.Click += new System.EventHandler(BTN_DELETE_Click);
        BTN_CLEAR.Click += new System.EventHandler(BTN_CLEAR_Click);

        //TXT_OPNDATE.TextChanged += new System.EventHandler(TXT_OPNDATE_TextChanged); 
    }

    public void Page_Load(object sender, System.EventArgs e)
    {

        if ((!IsPostBack && !IsCallback))
        {

            BTN_CLEAR_Click(sender, e);
            CHBX_EDIT.Checked = false;
            TXT_SRCH.Text = " ";
            CMB_CODE.DataBind();
            GridView1.DataBind();
        }
    }

    public void CLEAR()
    {

        if (CHBX_EDIT.Checked == true)
        {
            CMB_CDTYP.SelectedIndex = -1;
            CMB_FLDTYP.SelectedIndex = -1;
            TXT_RF_CODE.Text = "";
            TXT_DESC.Text = "";
            TXT_REMX.Text = "";
            TXT_CODE.Visible = false;
            BTN_ADD.Visible = false;
            CMB_CODE.SelectedIndex = -1;
            CMB_CODE.Visible = true;
            BTN_UPDT.Visible = true;
        }
        else
        {
            CMB_CDTYP.SelectedIndex = -1;
            CMB_FLDTYP.SelectedIndex = -1;
            TXT_RF_CODE.Text = "";
            TXT_DESC.Text = "";
            TXT_REMX.Text = "";
            TXT_CODE.Text = NEXT_REC_SRL();
            TXT_CODE.Visible = true;
            BTN_ADD.Visible = true;
            CMB_CODE.SelectedIndex = -1;
            CMB_CODE.Visible = false;
            BTN_UPDT.Visible = false;

        }
        BTN_DELETE.Visible = false;
        GET_GRID_INFO();
    }

    protected void BTN_CLEAR_Click(object sender, System.EventArgs e)
    {  // Handles BTN_CLEAR.Click
        CLEAR();
    }

    protected void CHBX_EDIT_CheckedChanged(object sender, System.EventArgs e)
    {  // Handles CHBX_EDIT.CheckedChanged
        CLEAR();


    }

    protected void BTN_ADD_Click(object sender, System.EventArgs e)
    {  // Handles BTN_ADD.Click

        if ((CMB_CDTYP.SelectedIndex < 0))
        {
            return;
        }

        String USR = User.Identity.Name.ToUpper();
        String UPDDT = System.DateTime.Today.ToString("dd/MM/yyyy");
        String UPDTIME = System.DateTime.Now.ToString("hh:mm:ss");
        String CODE = TXT_CODE.Text.ToUpper().Trim();
        if (TXT_RF_CODE.Text.Trim().Length > 0)
        {
            CODE = TXT_RF_CODE.Text.ToUpper().Trim();
        }

        String SRL = NEXT_REC_SRL();
        try
        {
            String RETRVQRY = "SELECT * FROM FKM_REFRNC WHERE RF_CODE = '" + CODE + "'";

            if (dbo.RecExist(RETRVQRY) == false)
            {
                String INSQRY = "INSERT INTO FKM_REFRNC VALUES('" + SRL + "','" + CODE + "','" + CMB_CDTYP.Text.ToString().ToUpper() +
                                       "','" + TXT_DESC.Text.ToUpper() + "','" + CMB_FLDTYP.Text.ToString().ToUpper() + "','0','','" + TXT_REMX.Text.ToUpper() +
                                       "','" + USR + "','" + UPDDT + "','" + UPDTIME + "')";

                if (dbo.InsRecord(INSQRY) == false)
                {
                    mbox("ERROR !!");
                }
                else
                {
                    // GridView1.DataBind();
                    CLEAR();
                    CMB_CODE.DataBind();
                    CHBX_EDIT.Checked = false;
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
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }

    }

    protected string NEXT_REC_SRL()
    {
        String SRL = "";
        try
        {
            String RETRVQRY = "SELECT MAX (RF_SRL) FROM FKM_REFRNC ";
            DataTable DTINVINF = dbo.SelTable(RETRVQRY);
            if ((DTINVINF.Rows[0][0]) != null)
            {
                Double NSRL = Double.Parse(DTINVINF.Rows[0][0].ToString());

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

    protected void BTN_UPDT_Click(object sender, System.EventArgs e)
    {  // Handles BTN_UPDT.Click
        String USR = User.Identity.Name.ToUpper();
        String UPDDT = System.DateTime.Today.ToString("dd/MM/yyyy");
        String UPDTIME = System.DateTime.Now.ToString("hh:mm:ss");
        String CODE = CMB_CODE.Text.Trim();
        
        try
        {
            String RETRVQRY = "SELECT * FROM FKM_REFRNC WHERE RF_CODE = '" + CODE + "'";

            if (dbo.RecExist(RETRVQRY) == true)
            {
                String UPDQRY = "UPDATE FKM_REFRNC SET RF_CODE = '" + TXT_RF_CODE.Text.ToUpper() + "', RF_DESCRP = '" + TXT_DESC.Text.ToUpper() +
                                        "',RF_REMX = '" + TXT_REMX.Text.ToUpper() + "', RF_CODETYPE = '" + CMB_CDTYP.Text.ToString().ToUpper() + "',RF_FEILDTYPE = '" + CMB_FLDTYP.Text.ToString().ToUpper() +
                                       "',RF_UPDBY = '" + USR + "',RF_UPDDATE = '" + UPDDT + "',RF_UPDTIME = '" + UPDTIME + "' " +
                                       "WHERE RF_CODE= '" + CODE + "' ";

                if (dbo.InsRecord(UPDQRY) == false)
                {
                    mbox("ERROR !!");
                }
                else
                {
                    CLEAR();
                    CMB_CODE.SelectedIndex = -1;
                    CMB_CODE.DataBind();

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
        String CODE = CMB_CODE.Text.Trim();

        try
        {
            String RETRVQRY = "SELECT * FROM FKM_REFRNC WHERE RF_CODE = '" + CODE + "'";

            if (dbo.RecExist(RETRVQRY) == true)
            {
                String UPDQRY = "DELETE FROM FKM_REFRNC  WHERE RF_CODE= '" + CODE + "' ";

                if (dbo.InsRecord(UPDQRY) == false)
                {
                    mbox("ERROR !!");
                }
                else
                {
                    CLEAR();
                    CMB_CODE.SelectedIndex = -1;
                    CMB_CODE.DataBind();

                    mbox("CODE : '" + CODE + "'   UPDATED SUCCESSFULLY !!");
                }

            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }

    }

    protected void CMB_CODE_Changed(String CODE)
    {//'Dim SRL As String = "0"
        CLEAR();
        try
        {
            String RETRVQRY = "SELECT * FROM FKM_REFRNC WHERE RF_CODE = '" + CODE + "'";
            DataTable dtinfo = dbo.SelTable(RETRVQRY);
            if (dtinfo.Rows.Count == 1)
            {

                CMB_CODE.SelectedIndex = CMB_CODE.Items.IndexOf(CMB_CODE.Items.FindByValue(CODE));

                CMB_CDTYP.SelectedIndex = CMB_CDTYP.Items.IndexOf(CMB_CDTYP.Items.FindByValue(dtinfo.Rows[0]["RF_CODETYPE"].ToString().Trim()));
                CMB_FLDTYP.SelectedIndex = CMB_FLDTYP.Items.IndexOf(CMB_FLDTYP.Items.FindByValue(dtinfo.Rows[0]["RF_FEILDTYPE"].ToString().Trim()));
                TXT_DESC.Text = dtinfo.Rows[0]["RF_DESCRP"].ToString().Trim();
                TXT_REMX.Text = dtinfo.Rows[0]["RF_REMX"].ToString().Trim();
                TXT_RF_CODE.Text = CODE;
            }
            else
            {
                //// CLEAR();
                mbox("ERROR !!");
            }
        }
        catch (Exception EX)
        {
            mbox(EX.Message + " # Error " + EX.StackTrace.Substring(EX.StackTrace.Length - 8, 8));
        }
    }

    protected void CMB_CODE_SelectedIndexChanged(object sender, System.EventArgs e)
    {  // Handles CMB_CODE.SelectedIndexChanged
        msgbox.Text = "";
        String CODE = CMB_CODE.SelectedValue;
        CMB_CODE_Changed(CODE);
    }

    protected void CMB_FLDTYP_SelectedIndexChanged(object sender, System.EventArgs e)
    {  // Handles CMB_FLDTYP.SelectedIndexChanged
        msgbox.Text = "";
        GridView1.DataBind();
    }

    protected void TXT_SRCH_TextChanged(object sender, System.EventArgs e)
    {
        GET_GRID_INFO();
    }

    protected void GET_GRID_INFO()
    {
      
        String RF_TXT   = TXT_SRCH.Text.Trim().ToUpper();
        String RETRVQRY = "SELECT * FROM FKM_REFRNC   WHERE ((RF_CODE LIKE '%" + RF_TXT + "%') OR (RF_CODETYPE LIKE '%" + RF_TXT + "%') OR (RF_DESCRP LIKE '%" + RF_TXT + "%') OR (RF_FEILDTYPE LIKE '%" + RF_TXT + "%') ) order by  RF_FEILDTYPE, RF_CODE ";
        DataTable dt   = dbo.SelTable(RETRVQRY);
         Session["FKM_REFRNC_dt_FKM_REFRNC_info"] = null;
            if (dt.Rows.Count > 0)
            {
                Session["FKM_REFRNC_dt_FKM_REFRNC_info"] = dt;
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
    }
    protected void Button1_Click(object sender, System.EventArgs e)
    { // Handles Button1.Click
        ReportDocument rpt = new ReportDocument();
        String rname = Server.MapPath("~/Reports/REFERENCEREP.rpt");
        String dwnldfname = Server.MapPath("~/ReportsGenerate/REFERENCEREP.xls");
        //String RF_TXT   = TXT_SRCH.Text.Trim().ToUpper();
        //String RETRVQRY    = "SELECT * FROM FKM_REFRNC   WHERE ((RF_CODE LIKE '%" + RF_TXT + "%') OR (RF_CODETYPE LIKE '%" + RF_TXT + "%') OR (RF_DESCRP LIKE '%" + RF_TXT + "%') OR (RF_FEILDTYPE LIKE '%" + RF_TXT + "%') ) order by  RF_FEILDTYPE, RF_CODE "
        DataTable dt = (DataTable)Session["FKM_REFRNC_dt_FKM_REFRNC_info"];//dbo.SelTable(RETRVQRY);  // 'GridView1.DataSource 'TryCast(Session("INV_TRX_dtGRID"), DataTable)

        if (dt.Rows.Count > 0)
        {
            rpt.Load(rname);
            rpt.SetDataSource(dt);

            //'rpt.PrintOptions.PaperSize = PaperSize.PaperA4
            //'rpt.PrintOptions.PaperOrientation = PaperOrientation.Landscape
            rpt.ExportToDisk(ExportFormatType.Excel, dwnldfname);
            rpt.Close();
            rpt.Dispose();


            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("content-disposition", "attachment;filename =" + "REFERENCEREP.xls");
            Response.ContentType = "application/ms-excel";
            Response.TransmitFile(dwnldfname);

        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, System.EventArgs e)
    {

        if (GridView1.SelectedDataKey[0].ToString().Trim().Length != 0)
        {
            CHBX_EDIT.Checked = true;
            String CODE = (String)GridView1.SelectedDataKey[0];

            CMB_CODE_Changed(CODE);
        }
    }

    protected void Gridview1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = Convert.ToInt32(e.RowIndex);
        DataTable dt = Session["FKM_REFRNC_dt_FKM_REFRNC_info"] as DataTable;
        string CODE = dt.Rows[index]["RF_CODE"].ToString();

        CHBX_EDIT.Checked = true;
        CMB_CODE_Changed(CODE);

        BTN_UPDT.Visible = false;
        BTN_DELETE.Visible = true;
    }
    public void mbox(string val)
    {
        //string cs   = "c";
        //string cst  = "<script type=""text/javascript"">" + "alert('" + val + "');</" + "script>";
        //Page page    = HttpContext.Current.Handler;
        //page.ClientScript.RegisterStartupScript(Me.typeof, cs, cst);

        string USR = User.Identity.Name.ToUpper();
        dbo.ErrorLog(USR + " : REF_EDIT.aspx : " + val, Server.MapPath("~\\Logs\\ErrorLog"));
    }

}
