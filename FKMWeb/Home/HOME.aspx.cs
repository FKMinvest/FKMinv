using System.IO;
using System.Data;
using System.Web.UI;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Web.UI.WebControls;

public partial class MAIN_HOME : Page
{
    fkminvcom dbo = new fkminvcom();
    override protected void OnInit(System.EventArgs e)
    {
        this.Load += new System.EventHandler(this.Page_Load);
        base.OnInit(e);
        TXT_SRCH.TextChanged += new System.EventHandler(TXT_SRCH_TextChanged); 
        BTNSRCH.Click += new System.EventHandler(BTNSRCH_Click);
        GridView1.RowDataBound += new System.Web.UI.WebControls.GridViewRowEventHandler(GridView1_RowDataBound);
        GridView2.RowDataBound += new System.Web.UI.WebControls.GridViewRowEventHandler(GridView2_RowDataBound);
        GridView3.RowDataBound += new System.Web.UI.WebControls.GridViewRowEventHandler(GridView3_RowDataBound);


        Button1.Click += new System.EventHandler(Button1_Click);
        Button1b.Click += new System.EventHandler(Button1b_Click);
        Button1_pdf.Click += new System.EventHandler(Button1_pdf_Click);
        Button2.Click += new System.EventHandler(Button2_Click);
        Button3.Click += new System.EventHandler(Button3_Click); 
    }

    public void Page_Load(object sender, System.EventArgs e)
    {
        if (Session["USER_LOGGED"] == null)
        {
            Session.Abandon();
            Response.Redirect("~/Account/Login.aspx");
        }


        if (!IsPostBack)
        {
            TXT_SRCH.Text = "";
            Button1.Visible = false;
            Button1b.Visible = false;
            Button1_pdf.Visible = false;
            Button2.Visible = false;
            GridView1.Visible = false;
            GridView1.DataSource = null;
            GridView1.DataBind();
            Button3.Visible = false;
            GridView2.Visible = false;
            GridView2.DataSource = null;
            GridView2.DataBind();

            string SRCHTXT = Request.QueryString.Get("SRCHTXT");
            if (SRCHTXT != null && SRCHTXT.Length > 0)
            {
                TXT_SRCH.Text = SRCHTXT.ToUpper();
                BTNSRCH_Click(sender ,e);
            }
        }
    }
    public void TXT_SRCH_TextChanged(object sender, System.EventArgs e)
    {
        BTNSRCH_Click(sender, e);
    }
    public void BTNSRCH_Click(object sender, System.EventArgs e) //Handles BTNSRCH.Click
    {
        Session["HOME_dt_invest_info"] = null;
        Session["HOME_dt_NAVRPT_info"] = null;
        Session["HOME_dt_NAVRPT_REPORT"] = null;
        Session["HOME_dt_LQDLIST_REPORT"] = null;
        Button1.Visible = false;
        Button1b.Visible = false;
        Button1_pdf.Visible = false;
        GridView1.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();

        Button2.Visible = false;
        GridView2.Visible = false;
        GridView2.DataSource = null;
        GridView2.DataBind();

        Button3.Visible = false;
        GridView3.Visible = false;
        GridView3.DataSource = null;
        GridView3.DataBind();

        string COMP_CD = Session["USER_COMP_CD"].ToString();
        char TBL = 'I';// invest info
        string RETRVQRY = BUILD_QRY(COMP_CD, TXT_SRCH.Text.Trim(), ref TBL);

        string REFRNC_QRY = "SELECT RF_CODE,RF_DESCRP,RF_FEILDTYPE FROM FKM_REFRNC ORDER BY RF_CODE  ";
        DataTable DT_REFRNC = dbo.SelTable(REFRNC_QRY);

        string[] srch_KEY_ARR = TXT_SRCH.Text.Split(' ');

        foreach (string CELL in srch_KEY_ARR)
        {
            string srchtxt = CELL.ToUpper();
            if ((srchtxt == "FKM") && (srchtxt.Length == 3))
            {
                COMP_CD = "001";
            }
            else if ((srchtxt == "FENK") && (srchtxt.Length == 4))
            {
                COMP_CD = "002";
            }
            else if ((srchtxt == "PERSONAL") && (srchtxt.Length == 8))
            {
                COMP_CD = "003";
            }
            else if ((srchtxt == "TECHSQUARE") && (srchtxt.Length == 10))
            {
                COMP_CD = "004";
            }
            else if ((srchtxt == "ALL") && (srchtxt.Length == 3))
            {
                COMP_CD = "%";
            }
            else
            {
                COMP_CD = "%"; //'  COMP_CD = Session("USER_COMP_CD");
            }

            Session["HOME_COMP_CD"] = COMP_CD;
        }



        if (RETRVQRY != "")
        {
            if (TBL == 'I')
            {

                REFINE_QRY_INV(RETRVQRY);
                // GridView1.Columns.Item(1).Visible = false;
                GridView1.Visible = true;
                GridView1.DataBind();
            }
            else if (TBL == 'N')
            {

                REFINE_QRY_NAV(RETRVQRY);
                REFINE_QRY_TO_GRID_NAV(RETRVQRY);
                // ' GridView1.Columns.Item(1).Visible = False
                GridView2.Visible = true;
                GridView2.DataBind();
            }
            else if (TBL == 'L')
            {

                string YR_CLMN = "%";
                string CRNC_CLMN = "%";

                foreach (string CELL in srch_KEY_ARR)
                {
                    if (dbo.IsAllDigits(CELL))
                    {
                        YR_CLMN = CELL;
                    }
                    DataRow[] DR_RFCD = DT_REFRNC.Select(" RF_CODE  = '" + CELL + "' ");
                    if (DR_RFCD.Length > 0)
                    {
                        CRNC_CLMN = DR_RFCD[0]["RF_DESCRP"].ToString().Trim();
                    }

                }

                REFINE_QRY_LQD("%", CRNC_CLMN, YR_CLMN); ;

                // ' GridView1.Columns.Item(1).Visible = False
                GridView3.Visible = true;
                GridView3.DataBind();
            }


        }

    }

    public string BUILD_QRY(string COMP_CD, string SEARCH_TEXT, ref char TBL)
    {

        string srchtxt, srch_KEY;
        string SRCHVL_CD, SRCHVL_DESC, SRCHVL_FIELD;
        TBL = 'I';  //' inves info            
        srchtxt = SEARCH_TEXT.ToUpper();
        if ((srchtxt == "FKM") && (srchtxt.Length == 3))
        {
            COMP_CD = "001";
        }
        else if ((srchtxt == "FENK") && (srchtxt.Length == 4))
        {
            COMP_CD = "002";
        }
        else if ((srchtxt == "PERSONAL") && (srchtxt.Length == 8))
        {
            COMP_CD = "003";
        }
        else if ((srchtxt == "TECHSQUARE") && (srchtxt.Length == 10))
        {
            COMP_CD = "004";
        }
        else if ((srchtxt == "ALL") && (srchtxt.Length == 3))
        {
            COMP_CD = "%";
        }
        else
        {
            COMP_CD = "%"; //'  COMP_CD = Session("USER_COMP_CD");
        }

        Session["HOME_COMP_CD"] = COMP_CD;

        string sSQLQry = "";
        string sSQLcurrqry = "";
        string sSQLSting = " SELECT * FROM INVEST_INFO  ";
        string sSQLStingSTS = " AND FKM_STATUS IN ('Y') AND COMP_CD LIKE '" + COMP_CD + "'  ";
        string sSQLStingF = "";

        if (SEARCH_TEXT.Trim().Length == 0)
        {
            return "";
        }


        string REFRNC_QRY = "SELECT RF_CODE,RF_DESCRP,RF_FEILDTYPE FROM FKM_REFRNC ORDER BY RF_CODE  ";
        DataTable DT_REFRNC = dbo.SelTable(REFRNC_QRY);

        srchtxt = SEARCH_TEXT.ToUpper();
        srch_KEY = SEARCH_TEXT.ToUpper();//  ' FOR VLOOKUP
        SRCHVL_FIELD = "";
        string srchchar;

        string[] srch_KEY_ARR = srch_KEY.Split(' ');

        if (srch_KEY_ARR.Length == 1)
        {
            SRCHVL_CD = "";
            SRCHVL_DESC = "";
            SRCHVL_FIELD = "#";

            int SpaceLoc = srchtxt.IndexOf("%", 0);

            if (dbo.IsAllDigits(srchtxt))
            {
                SpaceLoc = 99;
            }
            else if (SpaceLoc == 0 && dbo.IsAllDigits(srchtxt.Substring(0, 1)))
            {
                SpaceLoc = srchtxt.IndexOf("-", 0);
            }
            srchchar = srchtxt.Substring(0, 1);
            if ((srchchar == "M" || srchchar == "P" || srchtxt == "FKM" || srchtxt == "ALL") && srchtxt.Length == 3)
            {
                SRCHVL_CD = srchtxt;
                SRCHVL_DESC = srchtxt;
            }
            else if ((srchtxt == "FENK") && srchtxt.Length == 4)
            {
                SRCHVL_CD = srchtxt;
                SRCHVL_DESC = srchtxt;
            }
            else if ((srchtxt == "PERSONAL") && srchtxt.Length == 8)
            {
                SRCHVL_CD = srchtxt;
                SRCHVL_DESC = srchtxt;
            }
            else if ((srchtxt == "TECHSQUARE") && srchtxt.Length == 10)
            {
                SRCHVL_CD = srchtxt;
                SRCHVL_DESC = srchtxt;
            }

            else if ((srchtxt == "LQD") && srchtxt.Length == 3)
            {
                TBL = 'L';//' LIQUIDATED info
                return srchtxt;
            }
            else if (srchtxt.Trim().Length == 7)
            {
                if (GET_NAV_QUARTERCD(ref srchtxt) == true)
                {
                    TBL = 'N';  //' NAV info
                    return srchtxt;
                }
                SRCHVL_CD = srchtxt;
            }
            else if (SpaceLoc > 0)
            {
                SRCHVL_CD = "%";
                SRCHVL_DESC = srchtxt;
            }
            else
            {
                DataRow[] DR_RFCD = DT_REFRNC.Select(" RF_CODE  = '" + srchtxt + "' ");

                if (DR_RFCD.Length > 0)
                {
                    SRCHVL_CD = DR_RFCD[0]["RF_CODE"].ToString().Trim();
                    SRCHVL_DESC = DR_RFCD[0]["RF_DESCRP"].ToString().Trim();
                    SRCHVL_FIELD = DR_RFCD[0]["RF_FEILDTYPE"].ToString().Trim();
                    //'if SRCHVL_FIELD = "#" ){
                    //'    SRCHVL_FIELD = ""
                    //'End if
                }
                else
                {
                    //' DR_RFCD = DT_REFRNC.Select(" RF_DESCRP  = '" + srchtxt + "' ")
                    SRCHVL_CD = srchtxt;
                    SRCHVL_DESC = srchtxt;
                }
            }


            srchchar = SRCHVL_CD.Substring(0, 1);
            string yrs;

            if (srchchar == "P" && SRCHVL_CD.Length == 3)
            {

                yrs = SRCHVL_CD.Substring(1, 2);
                yrs = "20" + yrs;
                sSQLQry = sSQLQry + " WHERE  ( SUBstring(FKM_PRTCPDATE, 7, 4)  =  '" + yrs + "' )";
            }
            else if (srchchar == "M" && SRCHVL_CD.Length == 3)
            {

                yrs = SRCHVL_CD.Substring(1, 2);
                yrs = "20" + yrs;

                //'if (ActiveCell.Column = 11 Or ActiveCell.Column = 12 Or ActiveCell.Column = 13 Or ActiveCell.Column = 14 Or ActiveCell.Column = 15 Or ActiveCell.Column = 16 Or ActiveCell.Column = 22 Or ActiveCell.Column = 23 Or ActiveCell.Column = 24) And ActiveCell.Row = 9 ){
                sSQLQry = sSQLQry + " WHERE  ( SUBstring( FKM_MTRTDATE, 7, 4)  =  '" + yrs + "' )";
            }
            else if (srchchar == "%")
            {
                if (SpaceLoc == 99)
                {
                    sSQLQry = sSQLQry + " WHERE  ( FKM_ROI  = " + SRCHVL_DESC + " )";
                }
                else
                {
                    SpaceLoc = SRCHVL_DESC.IndexOf("-", 0);
                    if (SpaceLoc > 0)
                    {
                        decimal DEC1 = decimal.Parse(SRCHVL_DESC.Substring(0, SpaceLoc - 1));
                        decimal DEC2 = 0;
                        int LST = SRCHVL_DESC.IndexOf("%", 0);
                        if (LST == 0)
                        {
                            LST = SRCHVL_DESC.Length;
                            DEC2 = decimal.Parse(SRCHVL_DESC.Substring(SpaceLoc, (LST - SpaceLoc)));
                        }
                        else
                        {
                            DEC2 = decimal.Parse(SRCHVL_DESC.Substring(SpaceLoc, (LST - SpaceLoc - 1)));
                        }
                        sSQLQry = sSQLQry + " WHERE  ( FKM_ROI  BETWEEN " + DEC1 + " AND " + DEC2 + ")";
                        //  ' sSQLQry = sSQLQry + " OR ( FKM_COMMCAP  BETWEEN " + DEC1 + " AND " + DEC2 + ")"
                    }

                }
            }
            else if (srchtxt == "FKM" || srchtxt == "FENK" || srchtxt == "PERSONAL" || srchtxt == "TECHSQUARE" || srchtxt == "ALL")
            {
                sSQLQry = sSQLQry + " WHERE  FKM_SRL <> '' ";
            }
            else
            {
                if (SRCHVL_FIELD.Length > 0)
                {
                    sSQLQry = " WHERE (  " + SRCHVL_FIELD + " like '%" + SRCHVL_DESC + "%' )";
                    //' " + SRCHVL_FIELD + " like '%" + SRCHVL_CD + "%' OR 
                    //'if SRCHVL_FIELD = "COUPON" ){
                    //'    sSQLQry = " WHERE (  " + SRCHVL_FIELD + " " +  srchtxt + ")"
                    //'  } else {

                    if (SRCHVL_FIELD == "#")
                    {
                        sSQLQry = " WHERE (  FKM_SRL like '%" + SRCHVL_CD + "%' ";
                        sSQLQry = sSQLQry + " OR UPPER(FKM_PROJNAME) like '%" + SRCHVL_CD + "%' ";
                        sSQLQry = sSQLQry + " OR  FKM_HOLDNAME like '%" + SRCHVL_CD + "%' ";
                        sSQLQry = sSQLQry + " OR  FKM_LOCATION like '%" + SRCHVL_CD + "%' ";
                        sSQLQry = sSQLQry + " OR  FKM_OPPRBY like '%" + SRCHVL_CD + "%' ";
                        sSQLQry = sSQLQry + " OR  FKM_INVCOMP like '%" + SRCHVL_CD + "%' ";
                        sSQLQry = sSQLQry + " OR  FKM_PRVTEQT like '%" + SRCHVL_CD + "%' ";
                        sSQLQry = sSQLQry + " OR  FKM_BANK like '%" + SRCHVL_CD + "%' ";
                        sSQLQry = sSQLQry + " OR  FKM_YEILDPRD like '%" + SRCHVL_CD + "%' ";
                        sSQLQry = sSQLQry + " OR  FKM_INCGEN like '%" + SRCHVL_CD + "%' ";
                        sSQLQry = sSQLQry + " OR  FKM_CURR like '%" + SRCHVL_CD + "%' ";
                        sSQLQry = sSQLQry + " OR  FKM_STATUS like '%" + SRCHVL_CD + "%' )";
                    }
                    else if (SRCHVL_FIELD == "FKM_YEILDPRD")
                    {

                        SRCHVL_DESC = SRCHVL_DESC.Substring(0, 1);

                        sSQLQry = " WHERE (  " + SRCHVL_FIELD + " like '%" + SRCHVL_DESC + "%' )";
                    }
                    else if (SRCHVL_FIELD == "FKM_INCGEN")
                    {
                        if (SRCHVL_DESC == "INCOME GENERATING")
                        {
                            SRCHVL_DESC = "Y";
                        }
                        else if (SRCHVL_DESC == "NON-INCOME GENERATING")
                        {
                            SRCHVL_DESC = "N";
                        }

                        sSQLQry = " WHERE (  " + SRCHVL_FIELD + " like '%" + SRCHVL_DESC + "%' )";
                    }
                    else if (SRCHVL_FIELD == "FKM_PRVTEQT")
                    {
                        if (SRCHVL_DESC == "PRIVATE EQUITY")
                        {
                            SRCHVL_DESC = "Y";
                        }
                        else if (SRCHVL_DESC == "NON-PRIVATE EQUITY")
                        {
                            SRCHVL_DESC = "N";
                        }

                        sSQLQry = " WHERE (  " + SRCHVL_FIELD + " like '%" + SRCHVL_DESC + "%' )";
                    }
                    else if (SRCHVL_FIELD == "FKM_STATUS")
                    {

                        if (SRCHVL_DESC == "ACTIVE")
                        {
                            SRCHVL_DESC = "Y";
                        }
                        else if (SRCHVL_DESC == "INACTIVE")
                        {
                            SRCHVL_DESC = "N";
                            sSQLStingSTS = " AND FKM_STATUS IN ('N') AND COMP_CD LIKE '" + COMP_CD + "' ";
                        }
                        else if (SRCHVL_DESC == "OFF THE RECORD")
                        {
                            SRCHVL_DESC = "O";
                            sSQLStingSTS = " AND FKM_STATUS IN ('O') AND COMP_CD LIKE '" + COMP_CD + "' ";
                        }

                        sSQLQry = " WHERE (  UPPER(" + SRCHVL_FIELD + ") like '%" + SRCHVL_DESC + "%' )";
                    }
                }

            }
            sSQLStingF = sSQLSting + sSQLQry + sSQLStingSTS;
        }
        else
        {
            //' Return ""
            //'MUTIPLE CRETERIA YET TO DO 
            sSQLSting = " SELECT * FROM INVEST_INFO  ";

            //'UNION OF ALL CRITERIA
            //' Dim sSQL_MULTIPLE_QRY As string = "WHERE  FKM_SRL <> '' "

            //'ANY AMONG CRITERIA
            string sSQL_MULTIPLE_QRY = "WHERE  FKM_SRL = '' ";
            string OPRT = " OR ";

            if (chbx_and_or.Checked == true)
            {
                sSQL_MULTIPLE_QRY = "WHERE  FKM_SRL <> '' ";
                OPRT = " AND ";
            }

            string srchtxt1 = srch_KEY_ARR[0];
            if ((srchtxt1 == "LQD") && srchtxt1.Length == 3)
            {
                TBL = 'L';//' LIQUIDATED info
                return srchtxt1;
            }
            else if (srchtxt1.Trim().Length == 7)
            {
                if (GET_NAV_QUARTERCD(ref srchtxt) == true)
                {
                    TBL = 'N';  //' NAV info
                    return srchtxt1;
                }
            }
            string NULL_SQL_QRY = " FKM_SRL = '' ";
            foreach (string CELL in srch_KEY_ARR)
            {
                string MC_QRY = "";
                srchtxt = CELL.ToUpper();
                if ((srchtxt == "FKM") && (srchtxt.Length == 3))
                {
                    COMP_CD = "001";
                }
                else if ((srchtxt == "FENK") && (srchtxt.Length == 4))
                {
                    COMP_CD = "002";
                }
                else if ((srchtxt == "PERSONAL") && (srchtxt.Length == 8))
                {
                    COMP_CD = "003";
                }
                else if ((srchtxt == "TECHSQUARE") && (srchtxt.Length == 10))
                {
                    COMP_CD = "004";
                }
                else if ((srchtxt == "ALL") && (srchtxt.Length == 3))
                {
                    COMP_CD = "%";
                }
                else
                {
                    MC_QRY = MULTI_CRITERIA(CELL, "OR", ref DT_REFRNC);
                }

                //' MC_QRY = MULTI_CRITERIA(CELL, "OR", DT_REFRNC)

                if (MC_QRY.Length > 0)
                {
                    //'sSQL_MULTIPLE_QRY = sSQL_MULTIPLE_QRY + " AND (" + NULL_SQL_QRY + MC_QRY + ") ";
                    sSQL_MULTIPLE_QRY = sSQL_MULTIPLE_QRY + OPRT + " (" + NULL_SQL_QRY + MC_QRY + ") ";
                }
                if (CELL == "BOTH")
                {
                    sSQLStingSTS = " AND FKM_STATUS IN ('Y','N') AND COMP_CD LIKE '" + COMP_CD + "'  ";
                }
                else
                {
                    sSQLStingSTS = " AND FKM_STATUS IN ('Y') AND COMP_CD LIKE '" + COMP_CD + "'  ";
                }
            }

            if (sSQL_MULTIPLE_QRY.Length > 0)
            {

                sSQLStingF = sSQLSting + sSQL_MULTIPLE_QRY + sSQLStingSTS;
            }

        }

        return sSQLStingF;
    }


    public string MULTI_CRITERIA(string SEARCH_TEXT, string OPRT, ref DataTable DT_REFRNC)
    {
        string srchtxt, srch_KEY;
        string SRCHVL_CD, SRCHVL_DESC, SRCHVL_FIELD;

        string sSQLQry = "";

        if (SEARCH_TEXT.Trim().Length == 0)
        {
            return "";
        }

        //'Dim REFRNC_QRY As string = "SELECT RF_CODE,RF_DESCRP,RF_FEILDTYPE FROM FKM_REFRNC ORDER BY RF_CODE  "
        //'Dim DT_REFRNC As DataTable = dbo.SelTable(REFRNC_QRY)


        srchtxt = SEARCH_TEXT.ToUpper();
        srch_KEY = SEARCH_TEXT.ToUpper();//  ' FOR VLOOKUP
        SRCHVL_FIELD = "";
        string srchchar;

        srchchar = srchtxt.Substring(0, 1);

        string[] srch_KEY_ARR = srch_KEY.Split(' ');


        SRCHVL_CD = "";
        SRCHVL_DESC = "";
        SRCHVL_FIELD = "#";

        int SpaceLoc = srchtxt.IndexOf("%", 0);


        if (dbo.IsAllDigits(srchtxt))
        {
            SpaceLoc = 99;
        }
        else if (SpaceLoc == 0 && dbo.IsAllDigits(srchtxt.Substring(0, 1)))
        {
            SpaceLoc = srchtxt.IndexOf("-", 0);
        }
        srchchar = srchtxt.Substring(0, 1);
        if ((srchchar == "M" || srchchar == "P") && srchtxt.Length == 3)
        {
            SRCHVL_CD = srchtxt;
            SRCHVL_DESC = srchtxt;
        }
        else if (SpaceLoc > 0)
        {
            SRCHVL_CD = "%";
            SRCHVL_DESC = srchtxt;
        }

        else
        {
            DataRow[] DR_RFCD = DT_REFRNC.Select(" RF_CODE  = '" + srchtxt + "' ");

            if (DR_RFCD.Length > 0)
            {
                SRCHVL_CD = DR_RFCD[0]["RF_CODE"].ToString().Trim();
                SRCHVL_DESC = DR_RFCD[0]["RF_DESCRP"].ToString().Trim();
                SRCHVL_FIELD = DR_RFCD[0]["RF_FEILDTYPE"].ToString().Trim();

            }
            else
            {
                //' DR_RFCD = DT_REFRNC.Select(" RF_DESCRP  = '" + srchtxt + "' ")
                SRCHVL_CD = srchtxt;
                SRCHVL_DESC = srchtxt;
            }
        }


        srchchar = SRCHVL_CD.Substring(0, 1);
        string yrs;

        if (srchchar == "P" && SRCHVL_CD.Length == 3)
        {

            yrs = SRCHVL_CD.Substring(1, 2);
            yrs = "20" + yrs;
            sSQLQry = sSQLQry + " " + OPRT + " ( SUBstring(FKM_PRTCPDATE, 7, 4)  =  '" + yrs + "' )";
        }
        else if (srchchar == "M" && SRCHVL_CD.Length == 3)
        {

            yrs = SRCHVL_CD.Substring(1, 2);
            yrs = "20" + yrs;

            //'if (ActiveCell.Column = 11 Or ActiveCell.Column = 12 Or ActiveCell.Column = 13 Or ActiveCell.Column = 14 Or ActiveCell.Column = 15 Or ActiveCell.Column = 16 Or ActiveCell.Column = 22 Or ActiveCell.Column = 23 Or ActiveCell.Column = 24) And ActiveCell.Row = 9 ){
            sSQLQry = sSQLQry + " " + OPRT + "  ( SUBstring( FKM_MTRTDATE, 7, 4)  =  '" + yrs + "' )";
        }
        else if (srchchar == "%")
        {
            if (SpaceLoc == 99)
            {
                sSQLQry = sSQLQry + " " + OPRT + "  (   FKM_ROI  = " + SRCHVL_DESC + " )";
            }
            else
            {
                SpaceLoc = SRCHVL_DESC.IndexOf("-", 0);
                if (SpaceLoc > 0)
                {
                    decimal DEC1 = decimal.Parse(SRCHVL_DESC.Substring(0, SpaceLoc - 1));
                    decimal DEC2 = 0;
                    int LST = SRCHVL_DESC.IndexOf("%", 0);
                    if (LST == 0)
                    {
                        LST = SRCHVL_DESC.Length;
                        DEC2 = decimal.Parse(SRCHVL_DESC.Substring(SpaceLoc, (LST - SpaceLoc)));
                    }
                    else
                    {
                        DEC2 = decimal.Parse(SRCHVL_DESC.Substring(SpaceLoc, (LST - SpaceLoc - 1)));
                    }
                    sSQLQry = sSQLQry + " " + OPRT + "  ( FKM_ROI  BETWEEN " + DEC1 + " AND " + DEC2 + ")";
                    //  ' sSQLQry = sSQLQry + " OR ( FKM_COMMCAP  BETWEEN " + DEC1 + " AND " + DEC2 + ")"
                }

            }
        }
        else
        {
            if (SRCHVL_FIELD.Length > 0)
            {
                // sSQLQry = " WHERE (  " + SRCHVL_FIELD + " like '%" + SRCHVL_DESC + "%' )"
                //' " + SRCHVL_FIELD + " like '%" + SRCHVL_CD + "%' OR 
                //'if SRCHVL_FIELD = "COUPON" ){
                //'    sSQLQry = " WHERE (  " + SRCHVL_FIELD + " " +  srchtxt + ")"
                //'  } else {

                if (SRCHVL_FIELD == "#")
                {
                    sSQLQry = sSQLQry + " " + OPRT + " (  FKM_SRL like '%" + SRCHVL_CD + "%' ";
                    sSQLQry = sSQLQry + " OR UPPER(FKM_PROJNAME) like '%" + SRCHVL_CD + "%' ";
                    sSQLQry = sSQLQry + " OR  FKM_HOLDNAME like '%" + SRCHVL_CD + "%' ";
                    sSQLQry = sSQLQry + " OR  FKM_LOCATION like '%" + SRCHVL_CD + "%' ";
                    sSQLQry = sSQLQry + " OR  FKM_OPPRBY like '%" + SRCHVL_CD + "%' ";
                    sSQLQry = sSQLQry + " OR  FKM_INVCOMP like '%" + SRCHVL_CD + "%' ";
                    sSQLQry = sSQLQry + " OR  FKM_PRVTEQT like '%" + SRCHVL_CD + "%' ";
                    sSQLQry = sSQLQry + " OR  FKM_BANK like '%" + SRCHVL_CD + "%' ";
                    sSQLQry = sSQLQry + " OR  FKM_YEILDPRD like '%" + SRCHVL_CD + "%' ";
                    sSQLQry = sSQLQry + " OR  FKM_INCGEN like '%" + SRCHVL_CD + "%' ";
                    sSQLQry = sSQLQry + " OR  FKM_CURR like '%" + SRCHVL_CD + "%' ";
                    sSQLQry = sSQLQry + " OR  FKM_STATUS like '%" + SRCHVL_CD + "%' )";
                }
                else if (SRCHVL_FIELD == "FKM_YEILDPRD")
                {

                    SRCHVL_DESC = SRCHVL_DESC.Substring(0, 1);

                    sSQLQry = sSQLQry + " " + OPRT + " (  " + SRCHVL_FIELD + " like '%" + SRCHVL_DESC + "%' )";
                }
                else if (SRCHVL_FIELD == "FKM_INCGEN")
                {
                    if (SRCHVL_DESC == "INCOME GENERATING")
                    {
                        SRCHVL_DESC = "Y";
                    }
                    else if (SRCHVL_DESC == "NON-INCOME GENERATING")
                    {
                        SRCHVL_DESC = "N";
                    }

                    sSQLQry = sSQLQry + " " + OPRT + " (   " + SRCHVL_FIELD + " like '%" + SRCHVL_DESC + "%' )";
                }
                else if (SRCHVL_FIELD == "FKM_PRVTEQT")
                {
                    if (SRCHVL_DESC == "PRIVATE EQUITY")
                    {
                        SRCHVL_DESC = "Y";
                    }
                    else if (SRCHVL_DESC == "NON-PRIVATE EQUITY")
                    {
                        SRCHVL_DESC = "N";
                    }

                    sSQLQry = sSQLQry + " " + OPRT + " (   " + SRCHVL_FIELD + " like '%" + SRCHVL_DESC + "%' )";
                }
                else if (SRCHVL_FIELD == "FKM_STATUS")
                {
                    if (SRCHVL_DESC == "ACTIVE")
                    {
                        SRCHVL_DESC = "Y";
                    }
                    else if (SRCHVL_DESC == "INACTIVE")
                    {
                        SRCHVL_DESC = "N";
                    }
                    else if (SRCHVL_DESC == "OFF THE RECORD")
                    {
                        SRCHVL_DESC = "O";
                    }


                    sSQLQry = sSQLQry + " " + OPRT + " (  " + SRCHVL_FIELD + " like '%" + SRCHVL_DESC + "%' )";
                }
                else
                {
                    sSQLQry = sSQLQry + " " + OPRT + " (  " + SRCHVL_FIELD + " like '%" + SRCHVL_DESC + "%' )";
                }

            }

        }

        return sSQLQry;

    }
    public void REFINE_QRY_INV(string QRY)
    {
        //'  Dim RETRVQRY As string = "SELECT * FROM INVEST_INFO "
        DataTable DTINVINFO = dbo.SelTable(QRY);

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

            Session["HOME_dt_invest_info"] = null;
            if (dt_invest_info.Rows.Count > 0)
            {
                Button1.Visible = true;
                Button1b.Visible = true;
                Button1_pdf.Visible = false;
                Session["HOME_dt_invest_info"] = dt_invest_info;
                GridView1.DataSource = dt_invest_info;
                GridView1.DataBind();
            }
        }
    }
    public void REFINE_ROW_INV_DETAILS(ref DataTable DTINVINFO, ref DataTable dt_invest_info, string CURR, string CURRSYMB)
    {
        //'  Dim RETRVQRY As string = "SELECT * FROM INVEST_INFO "
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

    public void GridView1_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    //Handles GridView1.RowDataBound
    {
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
                e.Row.Cells[3].ForeColor = (System.Drawing.Color)dbo.GET_FRM_COMP_CD(drval2, 3); 
            }
        }

    }
    public void Button1_Click(object sender, System.EventArgs e) //Handles Button1.Click
    {

        ReportDocument rpt = new ReportDocument();
        string rname = Server.MapPath("~/Reports/HOME_GENRPT.rpt");
        string dwnldfname = Server.MapPath("~/ReportsGenerate/gendwnload.xls");
        DataTable dt = (DataTable)Session["HOME_dt_invest_info"];

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
            Response.AddHeader("content-disposition", "attachment;filename =" + "gendwnload.xls");
            Response.ContentType = "application/ms-excel";
            Response.TransmitFile(dwnldfname);

        }
    }
    public void Button1b_Click(object sender, System.EventArgs e) //Handles Button1.Click
    {

        ReportDocument rpt = new ReportDocument();
        string rname = Server.MapPath("~/Reports/HOME_GENRPT2.rpt");
        string dwnldfname = Server.MapPath("~/ReportsGenerate/sumdwnload .xls");
        DataTable dt = (DataTable)Session["HOME_dt_invest_info"];

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
            Response.AddHeader("content-disposition", "attachment;filename =" + "sumdwnload.xls");
            Response.ContentType = "application/ms-excel";
            Response.TransmitFile(dwnldfname);

        }
    }
    public void Button1_pdf_Click(object sender, System.EventArgs e) //Handles Button1.Click
    {

        ReportDocument rpt = new ReportDocument();
        string rname = Server.MapPath("~/Reports/HOME_GENRPT.rpt");
        string dwnldfname = Server.MapPath("~/ReportsGenerate/gendwnload.pdf");
        DataTable dt = (DataTable)Session["HOME_dt_invest_info"];

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
            Response.AddHeader("content-disposition", "attachment;filename =" + "gendwnload.pdf");
            Response.ContentType = "application/pdf";
            Response.TransmitFile(dwnldfname);

        }
    }

    public bool GET_NAV_QUARTERCD(ref string srchtxt)
    {
        string mnth = srchtxt.Substring(0, 3);
        string yr = srchtxt.Substring(3, 4);
        string cd = "";//'srchtxt.Substring(3, 4)
        bool flg = false;// 'srchtxt.Substring(3, 4)

        switch (mnth)
        {
            case "MAR":
                {
                    cd = yr + "03";
                    flg = true;
                    break;
                }
            case "JUN":
                {
                    cd = yr + "06";
                    flg = true;
                    break;
                }
            case "SEP":
                {
                    cd = yr + "09";
                    flg = true;
                    break;
                }
            case "DEC":
                {
                    cd = yr + "12";
                    flg = true;
                    break;
                }
            default:
                {
                    cd = srchtxt;
                    flg = false;
                    break;
                }
        }
        srchtxt = cd;
        //'if Today > CDate("30/09/" + Today.Year) ){
        //'    DT.Rows.Add("DECEMBER - " + (Today.Year), (Today.Year) + "12")
        //'End if

        //'if Today > CDate("30/06/" + Today.Year) ){
        //'    DT.Rows.Add("SEPTEMBER - " + (Today.Year), (Today.Year) + "09")
        //'End if

        //'if Today > CDate("31/03/" + Today.Year) ){
        //'    DT.Rows.Add("JUNE - " + (Today.Year), (Today.Year) + "06")
        //'End if

        //'if Today >= CDate("01/01/" + Today.Year) ){
        //'    DT.Rows.Add("MARCH - " + (Today.Year), (Today.Year) + "03")
        //'End if

        //'Dim YEAR As Integer = 2015
        //'For I = 1 To (Today.Year - YEAR)

        //'    DT.Rows.Add("DECEMBER - " + (Today.Year - I), (Today.Year - I) + "12")
        //'    DT.Rows.Add("SEPTEMBER - " + (Today.Year - I), (Today.Year - I) + "09")
        //'    DT.Rows.Add("JUNE - " + (Today.Year - I), (Today.Year - I) + "06")
        //'    DT.Rows.Add("MARCH - " + (Today.Year - I), (Today.Year - I) + "03")

        //'Next

        return flg;
    }

    public void REFINE_QRY_NAV(string QUARTERCD)
    {
        string COMP_CD = (string)Session["HOME_COMP_CD"];
        string RETRVQRY = "SELECT * FROM NAV_INFO WHERE FKM_STATUS = 'Y'  AND  FKM_ISSDATE LIKE '" + QUARTERCD + "' AND COMP_CD LIKE '" + COMP_CD + "' ";
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
            DTINVINFO.Columns.Add("FKM_CURR_SMBL", typeof(string));
            DTINVINFO.Columns.Add("FOOTER", typeof(string));
            DTINVINFO.Columns.Add("HEADER1", typeof(string));
            DTINVINFO.Columns.Add("FKM_REG", typeof(string));
            DTINVINFO.Columns.Add("LOGO", typeof(byte[]));




            DataTable dt_NAVRPT_info = DTINVINFO.Clone();
            dt_NAVRPT_info.Rows.Clear();

            REFINE_ROW_NAV_DETAILS(ref DTINVINFO, ref dt_NAVRPT_info, "US DOLLAR", "$");
            REFINE_ROW_NAV_DETAILS(ref DTINVINFO, ref dt_NAVRPT_info, "UK POUND", "£");
            REFINE_ROW_NAV_DETAILS(ref DTINVINFO, ref dt_NAVRPT_info, "EURO", "€");


            Session["HOME_dt_NAVRPT_REPORT"] = null;
            if (dt_NAVRPT_info.Rows.Count > 0)
            {
                Session["HOME_dt_NAVRPT_REPORT"] = dt_NAVRPT_info;
                //'GridView1.DataSource = dt_NAVRPT_info
                //'GridView1.DataBind()

            }
        }
    }

    public void REFINE_ROW_NAV_DETAILS(ref DataTable DTINVINFO, ref DataTable dt_NAVRPT_info, string CURR, string CURRSYMB)
    {
        // session.  .LCID = 2057;

        DataRow[] DR_USD = DTINVINFO.Select("FKM_CURR  = '" + CURR + "' ");

        if (DR_USD.Length > 0)
        {
            for (int I = 0; I <= DR_USD.Length - 1; I++)
            {
                //'DR_TOT_USD["COMP_CD") = DR_USD[I]["COMP_CD")

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


                //'if DR_USD[I]["FKM_PRVTEQT").ToString.Trim = "Y" ){
                //'    DR_USD[I]["FKM_PRVTEQT") = "PRIVATE EQUITY"
                //'  } else {if DR_USD[I]["FKM_PRVTEQT").ToString.Trim = "N" ){
                //'    DR_USD[I]["FKM_PRVTEQT") = "NON PRIVATE EQUITY"
                //'End if

                if (DR_USD[I]["FKM_INCGEN"].ToString().Trim() == "Y")
                {
                    DR_USD[I]["FKM_INCGEN"] = "INCOME GENERATING ";
                }
                else if (DR_USD[I]["FKM_INCGEN"].ToString().Trim() == "N")
                {
                    DR_USD[I]["FKM_INCGEN"] = "NON INCOME GENERATING ";
                }

                //'if DR_USD[I]["FKM_YEILDPRD").ToString.Trim = "M" ){
                //'    DR_USD[I]["FKM_YEILDPRD") = "MONTHLY"
                //'  } else {if DR_USD[I]["FKM_YEILDPRD").ToString.Trim = "Q" ){
                //'    DR_USD[I]["FKM_YEILDPRD") = "QUARTERLY"
                //'  } else {if DR_USD[I]["FKM_YEILDPRD").ToString.Trim = "S" ){
                //'    DR_USD[I]["FKM_YEILDPRD") = "SEMI ANNUAL"
                //'End if


                //'if Not IsDBNull(DR_USD[I]["FKM_MTRTDATE"].ToString()) ){
                //'    Dim DTE As string = DR_USD[I]["FKM_MTRTDATE").ToString.Substring(3, 2) + "/" + DR_USD[I]["FKM_MTRTDATE").ToString.Substring(0, 2) + "/" + DR_USD[I]["FKM_MTRTDATE").ToString.Substring(6, 4)
                //'    DR_USD[I]["FKM_MTRTDATE") = CDate(DTE)
                //'End if

                if (DR_USD[I]["FKM_REMX_NAV"].ToString() != null)
                {
                    if (DR_USD[I]["FKM_REMX_NAV"].ToString().Trim().Length > 0)
                    {
                        DR_USD[I]["FKM_REMX"] = DR_USD[I]["FKM_REMX_NAV"];
                    }
                    else
                    {
                        DR_USD[I]["FKM_REMX"] = ". ";
                    }
                }
                else
                {
                    DR_USD[I]["FKM_REMX"] = ". ";
                }
                //'if Not IsDBNull(DR_USD[I]["COMP_CD"].ToString()) ){
                //'    if DR_USD[I]["COMP_CD") = "001" ){
                //'        DR_USD[I]["FKM_REG") = "FKMINVEST"
                //'      } else {if DR_USD[I]["COMP_CD") = "002" ){
                //'        DR_USD[I]["FKM_REG") = "FENKINVEST"
                //'      } else {if DR_USD[I]["COMP_CD") = "003" ){
                //'        DR_USD[I]["FKM_REG") = "PERSONAL"
                //'   }
                //'End if

                DR_USD[I]["FKM_REG"] = (string)dbo.GET_FRM_COMP_CD(DR_USD[I]["COMP_CD"].ToString(), 1);
                DR_USD[I]["LOGO"] = dbo.ReadFile(Server.MapPath((string)dbo.GET_FRM_COMP_CD(DR_USD[I]["COMP_CD"].ToString(), 2)));

                DR_USD[I]["FKM_CURR_SMBL"] = CURRSYMB;
                DR_USD[I]["FOOTER"] = User.Identity.Name.ToUpper();
                DR_USD[I]["HEADER1"] = TXT_SRCH.Text.ToUpper();

                dt_NAVRPT_info.ImportRow(DR_USD[I]);
            }


        }
    }
    public void REFINE_QRY_TO_GRID_MAIN(string QUARTERCD, string CURR, string CURRSYMB, string INCGEN, string INVGRP)
    {

        string COMP_CD = (string)Session["HOME_COMP_CD"];
        string RETRVQRY = "SELECT COMP_CD,FKM_SRL ,FKM_PROJNAME,FKM_PRTCPDATE ,FKM_MTRTDATE,FKM_REMX_NAV AS FKM_REMX , " +
                                    "FKM_CURR, FKM_INCGEN,FKM_INVGRP, " +
                                 " FKM_COMMCAP ,   FKM_COMMCAP2 ," +
                                 " FKM_INVAMT, FKM_CAPPD, " +
                                 "  FKM_CAPUNPD , FKM_CAPRFND," +
                                 " FKM_EXPNS, FKM_ROI, " +
                                 "  FKM_BOOKVAL, FKM_MONYCAL," +
                                 " FKM_QRTYCAL, FKM_SMANYCAL, " +
                                 "  FKM_ANLYCAL, FKM_SCNINCP," +
                                 " FKM_ANLINCMCY, FKM_ANLRLZD, " +
                                 "  FKM_ACTLINCMRU, FKM_UNRLYLD," +
                                 " FKM_DVDND, FKM_VALVTN, " +
                                 "  FKM_CAPGN, FKM_UNRLDVD," +
                                 " FKM_UNRLDVDCAP, FKM_FAIRVAL, " +
                                 "  FKM_NAV, FKM_SALEPRCD" +
                                 " FROM NAV_INFO " +
                                 " WHERE FKM_CURR = '" + CURR + "' AND FKM_INCGEN = '" + INCGEN + "' AND FKM_INVGRP = '" + INVGRP +
                                 "' AND FKM_STATUS = 'Y'  AND  FKM_ISSDATE LIKE '" + QUARTERCD + "'  AND COMP_CD LIKE '" + COMP_CD + "'" +
                                         " ORDER BY FKM_CURR DESC, FKM_INCGEN DESC ,FKM_INVGRP,FKM_SRL ";

        DataTable DTINVINFO = dbo.SelTable(RETRVQRY);

        if (DTINVINFO.Rows.Count > 0)
        {

            //'DTINVINFO.Columns.Add("FKM_SRL", typeof(string))
            //'DTINVINFO.Columns.Add("FKM_PROJNAME", typeof(string))
            //'' DTINVINFO.Columns.Add("FKM_INVGRP", typeof(string));
            //'DTINVINFO.Columns.Add("FKM_PRTCPDATE", typeof(string))
            //'DTINVINFO.Columns.Add("FKM_MTRTDATE", typeof(string))
            //'DTINVINFO.Columns.Add("FKM_REMX", typeof(string))

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
            DTINVINFO.Columns.Add("FKM_CURR_SMBL", typeof(string));
            DTINVINFO.Columns.Add("FOOTER", typeof(string));
            DTINVINFO.Columns.Add("FKM_REG", typeof(string));
            DTINVINFO.Columns.Add("LOGO", typeof(byte[]));



            DataTable dt_NAVRPT_info = DTINVINFO.Clone();
            dt_NAVRPT_info.Rows.Clear();

            REFINE_ROW_GRIDS_NAV(ref DTINVINFO, ref dt_NAVRPT_info, CURR, CURRSYMB, "FKM_CURR", CURR);
            //'Call REFINE_ROW_GRIDS(DTINVINFO, dt_NAVRPT_info, "EURO", "€");

        }

    }
    public void REFINE_QRY_TO_GRID_INVGRP(string QUARTERCD, string CURR, string CURRSYMB, string INCGEN)
    {
        string COMP_CD = (string)Session["HOME_COMP_CD"];
        string RETRVQRY = "SELECT   FKM_CURR, FKM_INCGEN,FKM_INVGRP, " +
                                 " SUM(FKM_COMMCAP) AS FKM_COMMCAP ,SUM(FKM_COMMCAP2)  AS FKM_COMMCAP2 ," +
                                 "SUM(FKM_INVAMT) AS FKM_INVAMT,SUM(FKM_CAPPD) AS FKM_CAPPD, " +
                                 " SUM(FKM_CAPUNPD) AS FKM_CAPUNPD ,SUM(FKM_CAPRFND) AS FKM_CAPRFND," +
                                 "SUM(FKM_EXPNS) AS FKM_EXPNS,AVG(FKM_ROI) AS FKM_ROI, " +
                                 " SUM(FKM_BOOKVAL) AS FKM_BOOKVAL,SUM(FKM_MONYCAL) AS FKM_MONYCAL," +
                                 "SUM(FKM_QRTYCAL) AS FKM_QRTYCAL,SUM(FKM_SMANYCAL) AS FKM_SMANYCAL, " +
                                 " SUM(FKM_ANLYCAL) AS FKM_ANLYCAL,SUM(FKM_SCNINCP) AS FKM_SCNINCP," +
                                 "SUM(FKM_ANLINCMCY) AS FKM_ANLINCMCY,SUM(FKM_ANLRLZD) AS FKM_ANLRLZD, " +
                                 " AVG(FKM_ACTLINCMRU) AS FKM_ACTLINCMRU,SUM(FKM_UNRLYLD) AS FKM_UNRLYLD," +
                                 "SUM(FKM_DVDND) AS FKM_DVDND,SUM(FKM_VALVTN) AS FKM_VALVTN, " +
                                 " SUM(FKM_CAPGN) AS FKM_CAPGN,SUM(FKM_UNRLDVD) AS FKM_UNRLDVD," +
                                 "SUM(FKM_UNRLDVDCAP) AS FKM_UNRLDVDCAP,SUM(FKM_FAIRVAL) AS FKM_FAIRVAL, " +
                                 " AVG(FKM_NAV) AS FKM_NAV,SUM(FKM_SALEPRCD)  AS FKM_SALEPRCD" +
                                 " FROM NAV_INFO " +
                                 " WHERE FKM_CURR = '" + CURR + "' AND FKM_INCGEN = '" + INCGEN + "' AND FKM_STATUS = 'Y'  AND  FKM_ISSDATE LIKE '" + QUARTERCD + "' AND  COMP_CD LIKE '" + COMP_CD + "'" +
                                 " GROUP BY  FKM_CURR , FKM_INCGEN ,FKM_INVGRP " +
                                 " ORDER BY FKM_CURR DESC, FKM_INCGEN DESC ,FKM_INVGRP ";


        DataTable DTINVINFO = dbo.SelTable(RETRVQRY);

        if (DTINVINFO.Rows.Count > 0)
        {

            DTINVINFO.Columns.Add("FKM_SRL", typeof(string));
            DTINVINFO.Columns.Add("FKM_PROJNAME", typeof(string));
            //   ' DTINVINFO.Columns.Add("FKM_INVGRP", typeof(string));
            DTINVINFO.Columns.Add("FKM_PRTCPDATE", typeof(string));
            DTINVINFO.Columns.Add("FKM_MTRTDATE", typeof(string));
            DTINVINFO.Columns.Add("FKM_REMX", typeof(string));

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
            DTINVINFO.Columns.Add("FKM_CURR_SMBL", typeof(string));
            DTINVINFO.Columns.Add("FOOTER", typeof(string));
            DTINVINFO.Columns.Add("FKM_REG", typeof(string));
            DTINVINFO.Columns.Add("LOGO", typeof(byte[]));



            DataTable dt_NAVRPT_info = DTINVINFO.Clone();
            dt_NAVRPT_info.Rows.Clear();
            foreach (DataRow DR in DTINVINFO.Rows)
            {
                REFINE_QRY_TO_GRID_MAIN(QUARTERCD, CURR, CURRSYMB, INCGEN, DR["FKM_INVGRP"].ToString());
                REFINE_ROW_GRIDS_NAV(ref DTINVINFO, ref dt_NAVRPT_info, CURR, CURRSYMB, "FKM_INVGRP", DR["FKM_INVGRP"].ToString());
            }

            //'Call REFINE_ROW_GRIDS(DTINVINFO, dt_NAVRPT_info, CURR, CURRSYMB, "FKM_INCGEN", DR("FKM_INVGRP"].ToString())
            //'Call REFINE_ROW_GRIDS(DTINVINFO, dt_NAVRPT_info, "UK POUND", "£")
            //'Call REFINE_ROW_GRIDS(DTINVINFO, dt_NAVRPT_info, "EURO", "€")


            //'' Session("HOME_dt_NAVRPT_info") = Nothing
            //'if dt_NAVRPT_info.Rows.Count > 0 ){
            //'    Session("HOME_dt_NAVRPT_info") = dt_NAVRPT_info
            //'    'GridView1.DataSource = dt_NAVRPT_info
            //'    'GridView1.DataBind()



            //'End if
        }

    }
    public void REFINE_QRY_TO_GRID_INCGEN(string QUARTERCD, string CURR, string CURRSYMB)
    {
        string COMP_CD = (string)Session["HOME_COMP_CD"];
        string RETRVQRY = "SELECT  FKM_CURR, FKM_INCGEN, " +
                                 " SUM(FKM_COMMCAP) AS FKM_COMMCAP ,SUM(FKM_COMMCAP2)  AS FKM_COMMCAP2 ," +
                                 "SUM(FKM_INVAMT) AS FKM_INVAMT,SUM(FKM_CAPPD) AS FKM_CAPPD, " +
                                 " SUM(FKM_CAPUNPD) AS FKM_CAPUNPD ,SUM(FKM_CAPRFND) AS FKM_CAPRFND," +
                                 "SUM(FKM_EXPNS) AS FKM_EXPNS,AVG(FKM_ROI) AS FKM_ROI, " +
                                 " SUM(FKM_BOOKVAL) AS FKM_BOOKVAL,SUM(FKM_MONYCAL) AS FKM_MONYCAL," +
                                 "SUM(FKM_QRTYCAL) AS FKM_QRTYCAL,SUM(FKM_SMANYCAL) AS FKM_SMANYCAL, " +
                                 " SUM(FKM_ANLYCAL) AS FKM_ANLYCAL,SUM(FKM_SCNINCP) AS FKM_SCNINCP," +
                                 "SUM(FKM_ANLINCMCY) AS FKM_ANLINCMCY,SUM(FKM_ANLRLZD) AS FKM_ANLRLZD, " +
                                 " AVG(FKM_ACTLINCMRU) AS FKM_ACTLINCMRU,SUM(FKM_UNRLYLD) AS FKM_UNRLYLD," +
                                 "SUM(FKM_DVDND) AS FKM_DVDND,SUM(FKM_VALVTN) AS FKM_VALVTN, " +
                                 " SUM(FKM_CAPGN) AS FKM_CAPGN,SUM(FKM_UNRLDVD) AS FKM_UNRLDVD," +
                                 "SUM(FKM_UNRLDVDCAP) AS FKM_UNRLDVDCAP,SUM(FKM_FAIRVAL) AS FKM_FAIRVAL, " +
                                 " AVG(FKM_NAV) AS FKM_NAV,SUM(FKM_SALEPRCD)  AS FKM_SALEPRCD" +
                                 " FROM NAV_INFO " +
                                 " WHERE FKM_CURR = '" + CURR + "' AND FKM_STATUS = 'Y'  AND  FKM_ISSDATE LIKE '" + QUARTERCD + "' AND  COMP_CD LIKE '" + COMP_CD + "' " +
                                 " GROUP BY  FKM_CURR , FKM_INCGEN  " +
                                 " ORDER BY FKM_CURR DESC, FKM_INCGEN DESC  ";

        DataTable DTINVINFO = dbo.SelTable(RETRVQRY);

        if (DTINVINFO.Rows.Count > 0)
        {

            DTINVINFO.Columns.Add("FKM_SRL", typeof(string));
            DTINVINFO.Columns.Add("FKM_PROJNAME", typeof(string));
            DTINVINFO.Columns.Add("FKM_INVGRP", typeof(string));
            DTINVINFO.Columns.Add("FKM_PRTCPDATE", typeof(string));
            DTINVINFO.Columns.Add("FKM_MTRTDATE", typeof(string));
            DTINVINFO.Columns.Add("FKM_REMX", typeof(string));

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
            DTINVINFO.Columns.Add("FKM_CURR_SMBL", typeof(string));
            DTINVINFO.Columns.Add("FOOTER", typeof(string));
            DTINVINFO.Columns.Add("FKM_REG", typeof(string));
            DTINVINFO.Columns.Add("LOGO", typeof(byte[]));



            DataTable dt_NAVRPT_info = DTINVINFO.Clone();
            dt_NAVRPT_info.Rows.Clear();

            REFINE_QRY_TO_GRID_INVGRP(QUARTERCD, CURR, CURRSYMB, "Y");
            REFINE_ROW_GRIDS_NAV(ref DTINVINFO, ref dt_NAVRPT_info, CURR, CURRSYMB, "FKM_INCGEN", "Y");
            REFINE_QRY_TO_GRID_INVGRP(QUARTERCD, CURR, CURRSYMB, "N");
            REFINE_ROW_GRIDS_NAV(ref DTINVINFO, ref dt_NAVRPT_info, CURR, CURRSYMB, "FKM_INCGEN", "N");
            //'Call REFINE_ROW_GRIDS(DTINVINFO, dt_NAVRPT_info, "UK POUND", "£")
            //'Call REFINE_ROW_GRIDS(DTINVINFO, dt_NAVRPT_info, "EURO", "€")


            //''Session("HOME_dt_NAVRPT_info") = Nothing
            //'if dt_NAVRPT_info.Rows.Count > 0 ){
            //'    Session("HOME_dt_NAVRPT_info") = dt_NAVRPT_info
            //'    GridView1.DataSource = dt_NAVRPT_info
            //'    GridView1.DataBind()



            //'End if
        }

    }
    public void REFINE_QRY_TO_GRID_NAV(string QUARTERCD)
    {
        GridView1.DataSource = null;
        GridView1.DataBind();

        string COMP_CD = (string)Session["HOME_COMP_CD"];
        string RETRVQRY = "SELECT  FKM_CURR,  " +
                                 " SUM(FKM_COMMCAP) AS FKM_COMMCAP ,SUM(FKM_COMMCAP2)  AS FKM_COMMCAP2 ," +
                                 "SUM(FKM_INVAMT) AS FKM_INVAMT,SUM(FKM_CAPPD) AS FKM_CAPPD, " +
                                 " SUM(FKM_CAPUNPD) AS FKM_CAPUNPD ,SUM(FKM_CAPRFND) AS FKM_CAPRFND," +
                                 "SUM(FKM_EXPNS) AS FKM_EXPNS,AVG(FKM_ROI) AS FKM_ROI, " +
                                 " SUM(FKM_BOOKVAL) AS FKM_BOOKVAL,SUM(FKM_MONYCAL) AS FKM_MONYCAL," +
                                 "SUM(FKM_QRTYCAL) AS FKM_QRTYCAL,SUM(FKM_SMANYCAL) AS FKM_SMANYCAL, " +
                                 " SUM(FKM_ANLYCAL) AS FKM_ANLYCAL,SUM(FKM_SCNINCP) AS FKM_SCNINCP," +
                                 "SUM(FKM_ANLINCMCY) AS FKM_ANLINCMCY,SUM(FKM_ANLRLZD) AS FKM_ANLRLZD, " +
                                 " AVG(FKM_ACTLINCMRU) AS FKM_ACTLINCMRU,SUM(FKM_UNRLYLD) AS FKM_UNRLYLD," +
                                 "SUM(FKM_DVDND) AS FKM_DVDND,SUM(FKM_VALVTN) AS FKM_VALVTN, " +
                                 " SUM(FKM_CAPGN) AS FKM_CAPGN,SUM(FKM_UNRLDVD) AS FKM_UNRLDVD," +
                                 "SUM(FKM_UNRLDVDCAP) AS FKM_UNRLDVDCAP,SUM(FKM_FAIRVAL) AS FKM_FAIRVAL, " +
                                 " AVG(FKM_NAV) AS FKM_NAV,SUM(FKM_SALEPRCD)  AS FKM_SALEPRCD" +
                                 " FROM NAV_INFO WHERE FKM_STATUS = 'Y'  AND  FKM_ISSDATE LIKE '" + QUARTERCD + "' AND  COMP_CD LIKE '" + COMP_CD + "' " +
                                 " GROUP BY  FKM_CURR   " +
                                 " ORDER BY FKM_CURR DESC  ";

        DataTable DTINVINFO = dbo.SelTable(RETRVQRY);

        if (DTINVINFO.Rows.Count > 0)
        {

            DTINVINFO.Columns.Add("FKM_SRL", typeof(string));
            DTINVINFO.Columns.Add("FKM_PROJNAME", typeof(string));
            DTINVINFO.Columns.Add("FKM_INCGEN", typeof(string));
            DTINVINFO.Columns.Add("FKM_INVGRP", typeof(string));
            DTINVINFO.Columns.Add("FKM_PRTCPDATE", typeof(string));
            DTINVINFO.Columns.Add("FKM_MTRTDATE", typeof(string));
            DTINVINFO.Columns.Add("FKM_REMX", typeof(string));

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
            DTINVINFO.Columns.Add("FKM_CURR_SMBL", typeof(string));
            DTINVINFO.Columns.Add("FOOTER", typeof(string));
            DTINVINFO.Columns.Add("FKM_REG", typeof(string));
            DTINVINFO.Columns.Add("LOGO", typeof(byte[]));



            DataTable dt_NAVRPT_info = DTINVINFO.Clone();
            dt_NAVRPT_info.Rows.Clear();
            REFINE_QRY_TO_GRID_INCGEN(QUARTERCD, "US DOLLAR", "$");
            REFINE_ROW_GRIDS_NAV(ref DTINVINFO, ref dt_NAVRPT_info, "US DOLLAR", "$", "FKM_CURR", "US DOLLAR");
            REFINE_QRY_TO_GRID_INCGEN(QUARTERCD, "UK POUND", "£");
            REFINE_ROW_GRIDS_NAV(ref DTINVINFO, ref dt_NAVRPT_info, "UK POUND", "£", "FKM_CURR", "UK POUND");
            //'Call REFINE_ROW_GRIDS(DTINVINFO, dt_NAVRPT_info, "EURO", "€")


            dt_NAVRPT_info = (DataTable)Session["HOME_dt_NAVRPT_info"];
            if (dt_NAVRPT_info.Rows.Count > 0)
            {
                //' = dt_NAVRPT_info
                Button2.Visible = true;
                GridView2.DataSource = dt_NAVRPT_info;
                GridView2.DataBind();

            }
        }

    }

    public void REFINE_ROW_GRIDS_NAV(ref DataTable DTINVINFO, ref DataTable dt_NAVRPT_info, string CURR, string CURRSYMB, string FLDTYP, string SLCTN)
    {
        Session.LCID = 2057;
        DataTable dt_NAVRPT_GRID = new DataTable();
        dt_NAVRPT_GRID = (DataTable)Session["HOME_dt_NAVRPT_info"];
        if (dt_NAVRPT_GRID == null)
        {
            dt_NAVRPT_GRID = dt_NAVRPT_info.Clone();
        }

        DataRow[] DR_USD = DTINVINFO.Select("FKM_CURR  = '" + CURR + "' AND  " + FLDTYP + "  = '" + SLCTN + "'");
        if (DR_USD.Length > 0)
        {


            for (int I = 0; I <= DR_USD.Length - 1; I++)
            {

                if (decimal.Parse(DR_USD[I]["FKM_COMMCAP"].ToString()) == 0)
                {
                    //' DR_USD[I]["FKM_COMMCAP") = DBNull.Value
                }
                else
                {
                    DR_USD[I]["FKM_COMMCAP_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_COMMCAP"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_COMMCAP2"].ToString()) == 0)
                {
                    //' DR_USD[I]["FKM_COMMCAP2") = DBNull.Value
                }
                else
                {
                    DR_USD[I]["FKM_COMMCAP2_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_COMMCAP2"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_INVAMT"].ToString()) == 0)
                {
                    //' DR_USD[I]["FKM_INVAMT") = DBNull.Value
                }
                else
                {
                    DR_USD[I]["FKM_INVAMT_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_INVAMT"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_CAPPD"].ToString()) == 0)
                {
                    //' DR_USD[I]["FKM_CAPPD") = DBNull.Value
                }
                else
                {
                    DR_USD[I]["FKM_CAPPD_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_CAPPD"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_CAPUNPD"].ToString()) == 0)
                {
                    //' DR_USD[I]["FKM_CAPUNPD") = DBNull.Value
                }
                else
                {
                    DR_USD[I]["FKM_CAPUNPD_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_CAPUNPD"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_EXPNS"].ToString()) == 0)
                {
                    //  'DR_USD[I]["FKM_EXPNS") = DBNull.Value
                }
                else
                {
                    DR_USD[I]["FKM_EXPNS_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_EXPNS"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_ROI"].ToString()) == 0)
                {
                    //' DR_USD[I]["FKM_ROI") = DBNull.Value
                }
                else
                {
                    DR_USD[I]["FKM_ROI_R"] = string.Format("{0:#0.00}", DR_USD[I]["FKM_ROI"].ToString()) + "%";
                }
                if (decimal.Parse(DR_USD[I]["FKM_BOOKVAL"].ToString()) == 0)
                {
                    //' DR_USD[I]["FKM_BOOKVAL") = DBNull.Value
                }
                else
                {
                    DR_USD[I]["FKM_BOOKVAL_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_BOOKVAL"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_MONYCAL"].ToString()) == 0)
                {
                    //'  DR_USD[I]["FKM_MONYCAL") = DBNull.Value
                }
                else
                {
                    DR_USD[I]["FKM_MONYCAL_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_MONYCAL"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_QRTYCAL"].ToString()) == 0)
                {
                    //'  DR_USD[I]["FKM_QRTYCAL") = DBNull.Value
                }
                else
                {
                    DR_USD[I]["FKM_QRTYCAL_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_QRTYCAL"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_SMANYCAL"].ToString()) == 0)
                {
                    //'  DR_USD[I]["FKM_SMANYCAL") = DBNull.Value
                }
                else
                {
                    DR_USD[I]["FKM_SMANYCAL_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_SMANYCAL"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_ANLYCAL"].ToString()) == 0)
                {
                    //' DR_USD[I]["FKM_ANLYCAL") = DBNull.Value
                }
                else
                {
                    DR_USD[I]["FKM_ANLYCAL_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_ANLYCAL"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_SCNINCP"].ToString()) == 0)
                {
                    //'DR_USD[I]["FKM_SCNINCP") = DBNull.Value
                }
                else
                {
                    DR_USD[I]["FKM_SCNINCP_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_SCNINCP"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_ANLINCMCY"].ToString()) == 0)
                {
                    //'  DR_USD[I]["FKM_ANLINCMCY") = DBNull.Value
                }
                else
                {
                    DR_USD[I]["FKM_ANLINCMCY_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_ANLINCMCY"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_ANLRLZD"].ToString()) == 0)
                {
                    //' DR_USD[I]["FKM_ANLRLZD") = DBNull.Value
                }
                else
                {
                    DR_USD[I]["FKM_ANLRLZD_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_ANLRLZD"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_ACTLINCMRU"].ToString()) == 0)
                {
                    //'DR_USD[I]["FKM_ACTLINCMRU") = DBNull.Value
                }
                else
                {
                    DR_USD[I]["FKM_ACTLINCMRU_R"] = string.Format("{0:#0.00}", DR_USD[I]["FKM_ACTLINCMRU"].ToString()) + "%";
                }
                if (decimal.Parse(DR_USD[I]["FKM_UNRLYLD"].ToString()) == 0)
                {
                    //' DR_USD[I]["FKM_UNRLYLD") = DBNull.Value
                }
                else
                {
                    DR_USD[I]["FKM_UNRLYLD_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_UNRLYLD"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_DVDND"].ToString()) == 0)
                {
                    // '  DR_USD[I]["FKM_DVDND") = DBNull.Value
                }
                else
                {
                    DR_USD[I]["FKM_DVDND_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_DVDND"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_VALVTN"].ToString()) == 0)
                {
                    //' DR_USD[I]["FKM_VALVTN") = DBNull.Value
                }
                else
                {
                    DR_USD[I]["FKM_VALVTN_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_VALVTN"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_CAPGN"].ToString()) == 0)
                {
                    //'  DR_USD[I]["FKM_CAPGN") = DBNull.Value
                }
                else
                {
                    DR_USD[I]["FKM_CAPGN_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_CAPGN"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_UNRLDVD"].ToString()) == 0)
                {
                    //'  DR_USD[I]["FKM_UNRLDVD") = DBNull.Value
                }
                else
                {
                    DR_USD[I]["FKM_UNRLDVD_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_UNRLDVD"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_UNRLDVDCAP"].ToString()) == 0)
                {
                    //' DR_USD[I]["FKM_UNRLDVDCAP") = DBNull.Value
                }
                else
                {
                    DR_USD[I]["FKM_UNRLDVDCAP_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_UNRLDVDCAP"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_FAIRVAL"].ToString()) == 0)
                {
                    //'DR_USD[I]["FKM_FAIRVAL") = DBNull.Value
                }
                else
                {
                    DR_USD[I]["FKM_FAIRVAL_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_FAIRVAL"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_NAV"].ToString()) == 0)
                {
                }
                else
                {
                    DR_USD[I]["FKM_NAV_R"] = string.Format("{0:#0.00}", DR_USD[I]["FKM_NAV"]);
                }
                if (decimal.Parse(DR_USD[I]["FKM_SALEPRCD"].ToString()) == 0)
                {
                    //' DR_USD[I]["FKM_SALEPRCD") = DBNull.Value
                }
                else
                {
                    DR_USD[I]["FKM_SALEPRCD_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_SALEPRCD"]);
                }


                //'if DR_USD[I]["FKM_PRVTEQT").ToString.Trim = "Y" ){
                //'    DR_USD[I]["FKM_PRVTEQT") = "PRIVATE EQUITY"
                //'} else {if DR_USD[I]["FKM_PRVTEQT").ToString.Trim = "N" ){
                //'    DR_USD[I]["FKM_PRVTEQT") = "NON PRIVATE EQUITY"
                //'End if

                if (DR_USD[I]["FKM_INCGEN"].ToString().Trim() == "Y")
                {
                    DR_USD[I]["FKM_INCGEN"] = "INCOME GENERATING ";
                }
                else if (DR_USD[I]["FKM_INCGEN"].ToString().Trim() == "N")
                {
                    DR_USD[I]["FKM_INCGEN"] = "NON INCOME GENERATING ";
                }

                //'if DR_USD[I]["FKM_YEILDPRD").ToString.Trim = "M" ){
                //'    DR_USD[I]["FKM_YEILDPRD") = "MONTHLY"
                //'} else {if DR_USD[I]["FKM_YEILDPRD").ToString.Trim = "Q" ){
                //'    DR_USD[I]["FKM_YEILDPRD") = "QUARTERLY"
                //'} else {if DR_USD[I]["FKM_YEILDPRD").ToString.Trim = "S" ){
                //'    DR_USD[I]["FKM_YEILDPRD") = "SEMI ANNUAL"
                //'End if


                //'if Not IsDBNull(DR_USD[I]["FKM_PRTCPDATE"].ToString()) ){
                //'    Dim DTE As string = DR_USD[I]["FKM_PRTCPDATE").ToString.Substring(3, 2) & "/" & DR_USD[I]["FKM_PRTCPDATE").ToString.Substring(0, 2) & "/" & DR_USD[I]["FKM_PRTCPDATE").ToString.Substring(6, 4)
                //'    DR_USD[I]["FKM_PRTCPDATE") = CDate(DTE)
                //'End if
                //'if Not IsDBNull(DR_USD[I]["FKM_MTRTDATE"].ToString()) ){
                //'    Dim DTE As string = DR_USD[I]["FKM_MTRTDATE").ToString.Substring(3, 2) & "/" & DR_USD[I]["FKM_MTRTDATE").ToString.Substring(0, 2) & "/" & DR_USD[I]["FKM_MTRTDATE").ToString.Substring(6, 4)
                //'    DR_USD[I]["FKM_MTRTDATE") = CDate(DTE)
                //'End if


                if ((DR_USD[I]["FKM_PROJNAME"].ToString()) == "")
                {

                    if ((DR_USD[I]["FKM_INVGRP"].ToString()) == "")
                    {
                        DR_USD[I]["FKM_PROJNAME"] = DR_USD[I]["FKM_INCGEN"] + "  " + CURR + " (" + CURRSYMB + ") ";
                    }
                    else
                    {
                        DR_USD[I]["FKM_PROJNAME"] = "  TOTAL     " + DR_USD[I]["FKM_INVGRP"];
                    }

                    if ((DR_USD[I]["FKM_INCGEN"].ToString()) == "")
                    {
                        DR_USD[I]["FKM_PROJNAME"] = CURR + " (" + CURRSYMB + ") ";
                    }
                }
                else
                {

                    if ((DR_USD[I]["COMP_CD"].ToString()) != null)
                    {
                        //'if DR_USD[I]["COMP_CD") = "001" ){
                        //'    DR_USD[I]["FKM_REG") = "FKMINVEST"
                        //'} else {if DR_USD[I]["COMP_CD") = "002" ){
                        //'    DR_USD[I]["FKM_REG") = "FENKINVEST"
                        //'} else {if DR_USD[I]["COMP_CD") = "003" ){
                        //'    DR_USD[I]["FKM_REG") = "PERSONAL"
                        //'End if
                        DR_USD[I]["FKM_REG"] = (string)dbo.GET_FRM_COMP_CD(DR_USD[I]["COMP_CD"].ToString(), 1);
                        DR_USD[I]["LOGO"] = dbo.ReadFile(Server.MapPath((string)dbo.GET_FRM_COMP_CD(DR_USD[I]["COMP_CD"].ToString(), 2)));
                    }


                }

                DR_USD[I]["FKM_CURR_SMBL"] = CURRSYMB;
                DR_USD[I]["FOOTER"] = User.Identity.Name.ToUpper();

                dt_NAVRPT_GRID.ImportRow(DR_USD[I]);
            }


        }


        Session["HOME_dt_NAVRPT_info"] = dt_NAVRPT_GRID;
    }

    public void GridView2_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)// Handles GridView2.RowDataBound
    {
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
                e.Row.Cells[1].ForeColor = (System.Drawing.Color)dbo.GET_FRM_COMP_CD(drval2, 3); 
            } 
        }
    }
    public void Button2_Click(object sender, System.EventArgs e)            // Handles Button2.Click
    {

        ReportDocument rpt = new ReportDocument();
        string rname = Server.MapPath("~/Reports/NAVSUMMRPT1.rpt");
        string dwnldfname = Server.MapPath("~/ReportsGenerate/NAVRPT.xls");
        DataTable dt = (DataTable)Session["HOME_dt_NAVRPT_REPORT"];

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
            Response.AddHeader("content-disposition", "attachment;filename =" + "NAV_REPORT.xls");
            Response.ContentType = "application/ms-excel";
            Response.TransmitFile(dwnldfname);

        }
    }


    public void REFINE_QRY_LQD(string CODE, string CURR, string YYYY)
    {
        Session["HOME_dt_LQDLIST_REPORT"] = null;
        GridView3.DataSource = null;
        GridView3.DataBind();

        string COMP_CD = (string)Session["HOME_COMP_CD"];
        string RETRVQRY = "SELECT * FROM LQD_INFO WHERE FKM_SRL LIKE '" + CODE + "' AND FKM_CURR LIKE '" + CURR +
            "' AND COMP_CD LIKE '" + COMP_CD + "' AND SUBstring(FKM_LQDDATE, 7, 4) LIKE '" + YYYY + "' ORDER BY FKM_SRL   ";

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


            Session["HOME_dt_LQDLIST_REPORT"] = null;
            if (dt_LQDLIST_info.Rows.Count > 0)
            {
                Session["HOME_dt_LQDLIST_REPORT"] = dt_LQDLIST_info;
                Button3.Visible = true;
                GridView3.DataSource = dt_LQDLIST_info;
                GridView3.DataBind();
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

    public void GridView3_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
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
                e.Row.Cells[1].ForeColor = (System.Drawing.Color)dbo.GET_FRM_COMP_CD(drval2, 3); 
            }
        }

    }

    public void Button3_Click(object sender, System.EventArgs e) //Handles Button3.Click 
    {
        ReportDocument rpt = new ReportDocument();
        string rname = Server.MapPath("~/Reports/LQDATED_GENRPT.rpt");
        string dwnldfname = Server.MapPath("~/ReportsGenerate/LQDLIST_REPORT.xls");
        DataTable dt = (DataTable)Session["HOME_dt_LQDLIST_REPORT"];

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
            Response.AddHeader("content-disposition", "attachment;filename =" + "LQDLIST_REPORT.xls");
            Response.ContentType = "application/ms-excel";
            Response.TransmitFile(dwnldfname);

        }

    }

}
