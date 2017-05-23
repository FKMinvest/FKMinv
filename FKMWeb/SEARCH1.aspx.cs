using System.IO;
using System.Data;
using System.Web.UI;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Web.UI.WebControls; 

public partial class MAIN_SEARCH : System.Web.UI.MasterPage
{
    fkminvcom dbo = new fkminvcom();
    protected void Page_Load(object sender, System.EventArgs e)
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
            Button2.Visible = false;
            GridView1.Visible = false;
            GridView1.DataSource = null;
            GridView1.DataBind();
            Button3.Visible = false;
            GridView2.Visible = false;
            GridView2.DataSource = null;
            GridView2.DataBind();
        }
    }


    protected void BTNSRCH_Click(object sender, System.EventArgs e) //Handles BTNSRCH.Click
    {
        Session["HOME_dt_invest_info"] = null;
        Session["HOME_dt_NAVRPT_info"] = null;
        Session["HOME_dt_NAVRPT_REPORT"] = null;
        Session["HOME_dt_LQDLIST_REPORT"] = null;
        Button1.Visible = false;
        Button1b.Visible = false;
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
        string RETRVQRY = BUILD_QRY(COMP_CD, TXT_SRCH.Text.Trim(),TBL);

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

                // Call REFINE_QRY_INV(RETRVQRY)
                // GridView1.Columns.Item(1).Visible = false;
                GridView1.Visible = true;
                GridView1.DataBind();
            }
            else if (TBL == 'N')
            {

                // Call REFINE_QRY_NAV(RETRVQRY)
                // Call REFINE_QRY_TO_GRID_NAV(RETRVQRY)
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

                //  Call REFINE_QRY_LQD("%", CRNC_CLMN, YR_CLMN);

                // ' GridView1.Columns.Item(1).Visible = False
                GridView3.Visible = true;
                GridView3.DataBind();
            }


        }

    }

    public string BUILD_QRY(string COMP_CD, string SEARCH_TEXT, char TBL) 
{

        string srchtxt, srch_KEY;
        string SRCHVL_CD, SRCHVL_DESC, SRCHVL_FIELD ;
        TBL = 'I';  //' inves info            
         srchtxt  = SEARCH_TEXT.ToUpper() ;
            if ((srchtxt == "FKM") && (srchtxt.Length == 3))
            {
                COMP_CD = "001";
            }
            else if ((srchtxt == "FENK") && (srchtxt.Length == 4)){
                COMP_CD = "002";}
            else if ((srchtxt == "PERSONAL") && (srchtxt.Length == 8 )){
                COMP_CD = "003";}
            else if ((srchtxt == "TECHSQUARE") && ( srchtxt.Length == 10 )){
                COMP_CD = "004";}
            else if ((srchtxt == "ALL") && (srchtxt.Length == 3 )){
                COMP_CD = "%";}
            else {
                COMP_CD = "%" ; //'  COMP_CD = Session("USER_COMP_CD");
            }

        Session["HOME_COMP_CD"] = COMP_CD;

        string sSQLQry   = "";
        string sSQLcurrqry   = "";
        string sSQLSting   = " SELECT * FROM INVEST_INFO  ";
        string sSQLStingSTS  = " AND FKM_STATUS IN ('Y') AND COMP_CD LIKE '" + COMP_CD + "'  ";
        string sSQLStingF   = "";

        if( SEARCH_TEXT.Trim().Length == 0 )
        {
            return "";
        }
      

        string REFRNC_QRY   = "SELECT RF_CODE,RF_DESCRP,RF_FEILDTYPE FROM FKM_REFRNC ORDER BY RF_CODE  ";
        DataTable DT_REFRNC    = dbo.SelTable(REFRNC_QRY);

        srchtxt =  SEARCH_TEXT.ToUpper() ;
        srch_KEY =  SEARCH_TEXT.ToUpper() ;//  ' FOR VLOOKUP
        SRCHVL_FIELD = "";
        string srchchar  ;

        string[] srch_KEY_ARR  = srch_KEY.Split(' ');

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
                //if (GET_NAV_QUARTERCD(srchtxt) == true) {
                TBL = 'N';  //' NAV info
                return srchtxt;
                //}
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
                    //' DR_RFCD = DT_REFRNC.Select(" RF_DESCRP  = '" & srchtxt & "' ")
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
                        //  ' sSQLQry = sSQLQry + " OR ( FKM_COMMCAP  BETWEEN " & DEC1 & " AND " & DEC2 & ")"
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
                    //' " & SRCHVL_FIELD & " like '%" & SRCHVL_CD & "%' OR 
                    //'if SRCHVL_FIELD = "COUPON" ){
                    //'    sSQLQry = " WHERE (  " & SRCHVL_FIELD + " " +  srchtxt & ")"
                    //'Else

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
                            sSQLStingSTS = " AND FKM_STATUS IN ('N') AND COMP_CD = '" + COMP_CD + "' ";
                        }
                        else if (SRCHVL_DESC == "OFF THE RECORD")
                        {
                            SRCHVL_DESC = "O";
                            sSQLStingSTS = " AND FKM_STATUS IN ('O') AND COMP_CD = '" + COMP_CD + "' ";
                        }

                        sSQLQry = " WHERE (  UPPER(" + SRCHVL_FIELD + ") like '%" + SRCHVL_DESC + "%' )";
                    }
                }

                sSQLStingF = sSQLSting + sSQLQry + sSQLStingSTS;
            }
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
                //if (GET_NAV_QUARTERCD(srchtxt) == true) {
                TBL = 'N';  //' NAV info
                return srchtxt1;
                //} 
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
                    MC_QRY = MULTI_CRITERIA(CELL, "OR", DT_REFRNC);
                }

                //' MC_QRY = MULTI_CRITERIA(CELL, "OR", DT_REFRNC)

                if (MC_QRY.Length > 0)
                {
                    //'sSQL_MULTIPLE_QRY = sSQL_MULTIPLE_QRY + " AND (" + NULL_SQL_QRY + MC_QRY + ") ";
                    sSQL_MULTIPLE_QRY = sSQL_MULTIPLE_QRY + OPRT + " (" + NULL_SQL_QRY + MC_QRY + ") ";
                }
                if (CELL == "BOTH")
                {
                    sSQLStingSTS = " AND FKM_STATUS IN ('Y','N') AND COMP_CD = '" + COMP_CD + "'  ";
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


   public string MULTI_CRITERIA(string SEARCH_TEXT  , string OPRT  , DataTable DT_REFRNC )  

   {
        string srchtxt, srch_KEY  ;
        string SRCHVL_CD, SRCHVL_DESC, SRCHVL_FIELD ;

        string sSQLQry  = "";
    
        if( SEARCH_TEXT.Trim().Length == 0 )
        {
            return "";
        }

        //'Dim REFRNC_QRY As string = "SELECT RF_CODE,RF_DESCRP,RF_FEILDTYPE FROM FKM_REFRNC ORDER BY RF_CODE  "
        //'Dim DT_REFRNC As DataTable = dbo.SelTable(REFRNC_QRY)

       
        srchtxt =  SEARCH_TEXT.ToUpper() ;
        srch_KEY =  SEARCH_TEXT.ToUpper() ;//  ' FOR VLOOKUP
        SRCHVL_FIELD = "";
        string srchchar  ;      
    
        srchchar = srchtxt.Substring(0, 1);

        string[] srch_KEY_ARR  = srch_KEY.Split(' ');


        SRCHVL_CD = "";
        SRCHVL_DESC = "";
        SRCHVL_FIELD = "#";
    
            int SpaceLoc = srchtxt.IndexOf("%",0);

       
            if (dbo.IsAllDigits(srchtxt))
            {
                SpaceLoc = 99;
            }
            else if (SpaceLoc == 0 && dbo.IsAllDigits(srchtxt.Substring(0,1)))
            {
                SpaceLoc =   srchtxt.IndexOf( "-",0);
            }
            srchchar = srchtxt.Substring(0, 1);
            if ((srchchar == "M" || srchchar == "P" ) && srchtxt.Length == 3) 
            {
                SRCHVL_CD = srchtxt;
                SRCHVL_DESC = srchtxt;
            } 
            else if( SpaceLoc > 0)
            {  
                SRCHVL_CD = "%";
                SRCHVL_DESC = srchtxt;
            }
       
            else
            {
                DataRow[] DR_RFCD = DT_REFRNC.Select(" RF_CODE  = '" + srchtxt + "' ");

                if( DR_RFCD.Length > 0) 
                {
                    SRCHVL_CD = DR_RFCD[0]["RF_CODE"].ToString().Trim();
                    SRCHVL_DESC = DR_RFCD[0]["RF_DESCRP"].ToString().Trim();
                    SRCHVL_FIELD = DR_RFCD[0]["RF_FEILDTYPE"].ToString().Trim();
                    
                }
                else
                {
                    //' DR_RFCD = DT_REFRNC.Select(" RF_DESCRP  = '" & srchtxt & "' ")
                    SRCHVL_CD = srchtxt;
                    SRCHVL_DESC = srchtxt;
                }
            }


            srchchar = SRCHVL_CD.Substring (0, 1);
            string yrs ;

            if( srchchar == "P" &&  SRCHVL_CD.Length == 3 )
            {

                yrs = SRCHVL_CD.Substring (1, 2);
                yrs = "20" + yrs;
                sSQLQry = sSQLQry + " " + OPRT + " ( SUBstring(FKM_PRTCPDATE, 7, 4)  =  '" + yrs + "' )";
            }
            else if (srchchar == "M" && SRCHVL_CD.Length == 3) 
            {

                yrs =  SRCHVL_CD.Substring (1, 2);
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
                    SpaceLoc = SRCHVL_DESC.IndexOf("-",0);
                    if (SpaceLoc > 0) 
                    {
                       decimal DEC1  = decimal.Parse(SRCHVL_DESC.Substring(0, SpaceLoc - 1));
                        decimal DEC2  = 0;
                       int LST   =    SRCHVL_DESC.IndexOf("%",0);
                        if (LST == 0) {
                            LST = SRCHVL_DESC.Length;
                            DEC2 = decimal.Parse(SRCHVL_DESC.Substring(SpaceLoc, (LST - SpaceLoc)));}
                        else{
                            DEC2 = decimal.Parse(SRCHVL_DESC.Substring(SpaceLoc, (LST - SpaceLoc - 1)));
                        }
                        sSQLQry = sSQLQry + " " + OPRT + "  ( FKM_ROI  BETWEEN " + DEC1 + " AND " + DEC2 + ")";
                      //  ' sSQLQry = sSQLQry + " OR ( FKM_COMMCAP  BETWEEN " & DEC1 & " AND " & DEC2 & ")"
                    } 
                
                }
            }
            else
            {
                if (SRCHVL_FIELD.Length > 0)
                {
                   // sSQLQry = " WHERE (  " + SRCHVL_FIELD + " like '%" + SRCHVL_DESC + "%' )"
                    //' " & SRCHVL_FIELD & " like '%" & SRCHVL_CD & "%' OR 
                    //'if SRCHVL_FIELD = "COUPON" ){
                    //'    sSQLQry = " WHERE (  " & SRCHVL_FIELD + " " +  srchtxt & ")"
                    //'Else
               
                if (SRCHVL_FIELD == "#" )
                { 
                        sSQLQry = sSQLQry + " " + OPRT + " (  FKM_SRL like '%" + SRCHVL_CD + "%' "
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

                        SRCHVL_DESC = SRCHVL_DESC.Substring(0,1);

                        sSQLQry = sSQLQry + " " + OPRT + " (  " + SRCHVL_FIELD + " like '%" + SRCHVL_DESC + "%' )";
                }
                else if (SRCHVL_FIELD == "FKM_INCGEN")
                {
                        if( SRCHVL_DESC == "INCOME GENERATING") 
                        {
                            SRCHVL_DESC = "Y";
                        }
                        else if (SRCHVL_DESC == "NON-INCOME GENERATING" )
                        {
                            SRCHVL_DESC = "N";
                        }

                        sSQLQry = sSQLQry + " " + OPRT + " (   " + SRCHVL_FIELD + " like '%" + SRCHVL_DESC + "%' )";
                }
                else if( SRCHVL_FIELD == "FKM_PRVTEQT") 
                {                             
                        if( SRCHVL_DESC == "PRIVATE EQUITY") 
                        {
                            SRCHVL_DESC = "Y";
                        }
                        else if (SRCHVL_DESC == "NON-PRIVATE EQUITY" )
                        {
                            SRCHVL_DESC = "N";
                        }

                        sSQLQry = sSQLQry + " " + OPRT + " (   " + SRCHVL_FIELD + " like '%" + SRCHVL_DESC + "%' )";
                }
                else if( SRCHVL_FIELD == "FKM_STATUS")
                {
                        if( SRCHVL_DESC == "ACTIVE") 
                        {
                            SRCHVL_DESC = "Y";
                        }
                        else if (SRCHVL_DESC == "INACTIVE" )
                        {
                            SRCHVL_DESC = "N"; 
                        }
                        else if (SRCHVL_DESC == "OFF THE RECORD" )
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
    public void  REFINE_QRY_INV(string QRY )
    {
        //'  Dim RETRVQRY As string = "SELECT * FROM INVEST_INFO "
        DataTable DTINVINFO   = dbo.SelTable(QRY);

        if (DTINVINFO.Rows.Count > 0 )
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
            DTINVINFO.Columns.Add("LOGO", typeof(string));

            DataTable dt_invest_info    = DTINVINFO.Clone();
            dt_invest_info.Rows.Clear();
              REFINE_ROW_INV_DETAILS(DTINVINFO, dt_invest_info, "US DOLLAR", "$")
            REFINE_ROW_INV_DETAILS(DTINVINFO, dt_invest_info, "UK POUND", "£")
              REFINE_ROW_INV_DETAILS(DTINVINFO, dt_invest_info, "EURO", "€")
            
            Session["HOME_dt_invest_info"] = null;
            if (dt_invest_info.Rows.Count > 0)
            {
                Button1.Visible = true;
                Button1b.Visible = true;
                Session["HOME_dt_invest_info"] = dt_invest_info;
                GridView1.DataSource = dt_invest_info;
                GridView1.DataBind();
            }
        }
    }
    public void  REFINE_ROW_INV_DETAILS(DataTable DTINVINFO   , DataTable dt_invest_info  , string CURR  , string CURRSYMB )
{
        //'  Dim RETRVQRY As string = "SELECT * FROM INVEST_INFO "
        //'Dim DTINVINFO As DataTable = dbo.SelTable(QRY)
        //'if DTINVINFO.Rows.Count > 0 ){
        //'    Dim dt_invest_info As DataTable = DTINVINFO.Clone
        //'    dt_invest_info.Rows.Clear()


        DataRow[] DR_USD   = DTINVINFO.Select("FKM_CURR  = '" + CURR + "' ");
        DataRow DR_TOT_USD = dt_invest_info.NewRow();
        if (DR_USD.Length > 0 ){ 


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
            DR_TOT_USD["FKM_COMMCAP2"] =decimal.Parse("0");
            DR_TOT_USD["FKM_INVAMT"] =decimal.Parse("0");
            DR_TOT_USD["FKM_CAPPD"] =decimal.Parse("0");
            DR_TOT_USD["FKM_CAPUNPD"] =decimal.Parse("0");
            DR_TOT_USD["FKM_CAPRFND"] =decimal.Parse("0");
            DR_TOT_USD["FKM_EXPNS"] =decimal.Parse("0");
            DR_TOT_USD["FKM_ROI"] =decimal.Parse("0");
            DR_TOT_USD["FKM_BOOKVAL"] =decimal.Parse("0");
            DR_TOT_USD["FKM_MONYCAL"] =decimal.Parse("0");
            DR_TOT_USD["FKM_QRTYCAL"] =decimal.Parse("0");
            DR_TOT_USD["FKM_SMANYCAL"] =decimal.Parse("0");
            DR_TOT_USD["FKM_ANLYCAL"] =decimal.Parse("0");
            DR_TOT_USD["FKM_SCNINCP"] =decimal.Parse("0");
            DR_TOT_USD["FKM_ANLINCMCY"] =decimal.Parse("0");
            DR_TOT_USD["FKM_ANLRLZD"] =decimal.Parse("0");
            DR_TOT_USD["FKM_ACTLINCMRU"] =decimal.Parse("0");
            DR_TOT_USD["FKM_UNRLYLD"] =decimal.Parse("0");
            DR_TOT_USD["FKM_DVDND"] =decimal.Parse("0");
            DR_TOT_USD["FKM_VALVTN"] =decimal.Parse("0");
            DR_TOT_USD["FKM_CAPGN"] =decimal.Parse("0");
            DR_TOT_USD["FKM_UNRLDVD"] =decimal.Parse("0");
            DR_TOT_USD["FKM_UNRLDVDCAP"] =decimal.Parse("0");
            DR_TOT_USD["FKM_FAIRVAL"] =decimal.Parse("0");
            DR_TOT_USD["FKM_NAV"] =decimal.Parse("0");
            DR_TOT_USD["FKM_SALEPRCD"] =decimal.Parse("0");

            for(int I = 0 ; I <= DR_USD.Length - 1 ; I++)
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

                if (decimal.Parse(DR_USD[I]["FKM_COMMCAP"].ToString()) == 0)
                {
                    DR_USD[I]["FKM_COMMCAP"] = null;
                }
                else
                {
                    DR_USD[I]["FKM_COMMCAP_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_COMMCAP"].ToString());
                }

                if  (decimal.Parse(DR_USD[I]["FKM_COMMCAP2"].ToString()) == 0) {
                    DR_USD[I]["FKM_COMMCAP2"] = null;
                }
                else
                {
                    DR_USD[I]["FKM_COMMCAP2_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_COMMCAP2"].ToString());
                }
                if (decimal.Parse(DR_USD[I]["FKM_INVAMT"].ToString()) == 0){  
                    DR_USD[I]["FKM_INVAMT"] = null;
                }
                else
                {
                    DR_USD[I]["FKM_INVAMT_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_INVAMT"].ToString());
                }
                if (decimal.Parse(DR_USD[I]["FKM_CAPPD"].ToString()) == 0){  
                    DR_USD[I]["FKM_CAPPD"] =null;
                }
                else
                {
                    DR_USD[I]["FKM_CAPPD_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_CAPPD"].ToString());
                }
                if (decimal.Parse(DR_USD[I]["FKM_CAPUNPD"].ToString()) == 0 ){
                    DR_USD[I]["FKM_CAPUNPD"] =  null;
                }
                else
                {
                    DR_USD[I]["FKM_CAPUNPD_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_CAPUNPD"].ToString());
               }
                if (decimal.Parse(DR_USD[I]["FKM_EXPNS"].ToString())== 0 ){
                    DR_USD[I]["FKM_EXPNS"] = null;
                }
                else
                {
                    DR_USD[I]["FKM_EXPNS_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_EXPNS"].ToString());
               }
                if (decimal.Parse(DR_USD[I]["FKM_ROI"].ToString()) == 0 ){
                    DR_USD[I]["FKM_ROI"] = null;
                }
                else
                {
                    DR_USD[I]["FKM_ROI_R"] = string.Format("{0:#0.00}", DR_USD[I]["FKM_ROI"].ToString()) + "%";
               }
                if (decimal.Parse(DR_USD[I]["FKM_BOOKVAL"].ToString()) == 0 ){
                    DR_USD[I]["FKM_BOOKVAL"] =  null;
                }
                else
                {
                    DR_USD[I]["FKM_BOOKVAL_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_BOOKVAL"].ToString());
               }
                if (decimal.Parse(DR_USD[I]["FKM_MONYCAL"].ToString()) == 0 ){
                    DR_USD[I]["FKM_MONYCAL"] = null;
                }
                else
                {
                    DR_USD[I]["FKM_MONYCAL_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_MONYCAL"].ToString());
               }
                if (decimal.Parse(DR_USD[I]["FKM_QRTYCAL"].ToString()) == 0 ){
                    DR_USD[I]["FKM_QRTYCAL"] =  null;
                }
                else
                {
                    DR_USD[I]["FKM_QRTYCAL_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_QRTYCAL"].ToString());
               }
                if (decimal.Parse(DR_USD[I]["FKM_SMANYCAL"].ToString()) == 0 ){
                    DR_USD[I]["FKM_SMANYCAL"] =  null;
                }
                else
                {
                    DR_USD[I]["FKM_SMANYCAL_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_SMANYCAL"].ToString());
               }
                if (decimal.Parse(DR_USD[I]["FKM_ANLYCAL"].ToString()) == 0 ){
                    DR_USD[I]["FKM_ANLYCAL"] =  null;
                }
                else
                {
                    DR_USD[I]["FKM_ANLYCAL_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_ANLYCAL"].ToString());
               }
                if (decimal.Parse(DR_USD[I]["FKM_SCNINCP"].ToString()) == 0 ){
                    DR_USD[I]["FKM_SCNINCP"] = null;
                }
                else
                {
                    DR_USD[I]["FKM_SCNINCP_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_SCNINCP"].ToString());
               }
                if (decimal.Parse(DR_USD[I]["FKM_ANLINCMCY"].ToString()) == 0 ){
                    DR_USD[I]["FKM_ANLINCMCY"] = null;
                }
                else
                {
                    DR_USD[I]["FKM_ANLINCMCY_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_ANLINCMCY"].ToString());
               }
                if (decimal.Parse(DR_USD[I]["FKM_ANLRLZD"].ToString()) == 0 ){
                    DR_USD[I]["FKM_ANLRLZD"] = null;
                }
                else
                {
                    DR_USD[I]["FKM_ANLRLZD_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_ANLRLZD"].ToString());
               }
                if (decimal.Parse(DR_USD[I]["FKM_ACTLINCMRU"].ToString()) == 0 ){
                    DR_USD[I]["FKM_ACTLINCMRU"] = null;
                }
                else
                {
                    DR_USD[I]["FKM_ACTLINCMRU_R"] = string.Format("{0:#0.00}", DR_USD[I]["FKM_ACTLINCMRU"].ToString()) + "%";
               }
                if( decimal.Parse(DR_USD[I]["FKM_UNRLYLD"].ToString()) == 0 ){
                    DR_USD[I]["FKM_UNRLYLD"] = null;
                }
                else
                {
                    DR_USD[I]["FKM_UNRLYLD_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_UNRLYLD"].ToString());
                }
                if (decimal.Parse(DR_USD[I]["FKM_DVDND"].ToString()) == 0  ){
                    DR_USD[I]["FKM_DVDND"] = null;
                }
                else
                {
                    DR_USD[I]["FKM_DVDND_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_DVDND"].ToString());
                }
                if (decimal.Parse(DR_USD[I]["FKM_VALVTN"].ToString()) == 0){  
                    DR_USD[I]["FKM_VALVTN"] = null;
                }
                else
                {
                    DR_USD[I]["FKM_VALVTN_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_VALVTN"].ToString());
                }
                if (decimal.Parse(DR_USD[I]["FKM_CAPGN"].ToString()) == 0){  
                    DR_USD[I]["FKM_CAPGN"] = null;
                }
                else
                {
                    DR_USD[I]["FKM_CAPGN_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_CAPGN"].ToString());
                }
                if (decimal.Parse(DR_USD[I]["FKM_UNRLDVD"].ToString()) == 0){ 
                    DR_USD[I]["FKM_UNRLDVD"] = null;
                }
                else
                {
                    DR_USD[I]["FKM_UNRLDVD_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_UNRLDVD"].ToString());
                }
                if (decimal.Parse(DR_USD[I]["FKM_UNRLDVDCAP"].ToString()) == 0){
                    DR_USD[I]["FKM_UNRLDVDCAP"] = null;
                }
                else
                {
                    DR_USD[I]["FKM_UNRLDVDCAP_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_UNRLDVDCAP"].ToString());
                }
               if (decimal.Parse(DR_USD[I]["FKM_FAIRVAL"].ToString()) == 0 ){
                    DR_USD[I]["FKM_FAIRVAL"] = null;
                }
                else
                {
                    DR_USD[I]["FKM_FAIRVAL_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_FAIRVAL"].ToString());
                }
                if (decimal.Parse(DR_USD[I]["FKM_NAV"].ToString()) == 0 ){
                    DR_USD[I]["FKM_NAV"] = null;
                }
                else
                {
                    DR_USD[I]["FKM_NAV_R"] = string.Format("{0:#0.00}", DR_USD[I]["FKM_NAV"].ToString());
                }
                if (decimal.Parse(DR_USD[I]["FKM_SALEPRCD"].ToString()) == 0 ){
                    DR_USD[I]["FKM_SALEPRCD"] =null;
                }
                else
                {
                    DR_USD[I]["FKM_SALEPRCD_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_SALEPRCD"].ToString());
                }


                if (DR_USD[I]["FKM_PRVTEQT"].ToString().Trim() == "Y" ){
                    DR_USD[I]["FKM_PRVTEQT"] = "PRIVATE EQUITY";}
                else if (DR_USD[I]["FKM_PRVTEQT"].ToString().Trim() == "N" ){
                    DR_USD[I]["FKM_PRVTEQT"] = "NON PRIVATE EQUITY";
               }

                if (DR_USD[I]["FKM_YEILDPRD"].ToString().Trim() == "M" ){
                    DR_USD[I]["FKM_YEILDPRD"] = "MONTHLY";}
                else if( DR_USD[I]["FKM_YEILDPRD"].ToString().Trim() == "Q" ){
                    DR_USD[I]["FKM_YEILDPRD"] = "QUARTERLY";}
                else if( DR_USD[I]["FKM_YEILDPRD"].ToString().Trim() == "S" ){
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


                //'if Not IsDBNull(DR_USD[I]["COMP_CD"].ToString()) ){
                //'    if DR_USD[I]["COMP_CD") = "001" ){
                //'        DR_USD[I]["FKM_REG") = "FKMINVEST"
                //'    Elseif DR_USD[I]["COMP_CD") = "002" ){
                //'        DR_USD[I]["FKM_REG") = "FENKINVEST"
                //'    Elseif DR_USD[I]["COMP_CD") = "003" ){
                //'        DR_USD[I]["FKM_REG") = "PERSONAL"
                //'   }
                //'End if

                DR_USD[I]["FKM_REG"] = null;// (string)dbo.GET_FRM_COMP_CD(DR_USD[I]["COMP_CD"), 1);
                DR_USD[I]["LOGO"] = null;//dbo.ReadFile(Server.MapPath((string)dbo.GET_FRM_COMP_CD(DR_USD[I]["COMP_CD"), 2)));

                DR_USD[I]["FOOTER"] = "";// User.Identity.Name.ToUpper();

                dt_invest_info.ImportRow(DR_USD[I]);
        }


            if (decimal.Parse(DR_TOT_USD["FKM_COMMCAP"].ToString()) == 0 ){
                DR_TOT_USD["FKM_COMMCAP"] =null;
                }
                else
                {
                DR_TOT_USD["FKM_COMMCAP_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_TOT_USD["FKM_COMMCAP"].ToString());
            }
            if (decimal.Parse(DR_TOT_USD["FKM_COMMCAP2"].ToString()) == 0 ){
                DR_TOT_USD["FKM_COMMCAP2"] =null;
                }
                else
                {
                DR_TOT_USD["FKM_COMMCAP2_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_TOT_USD["FKM_COMMCAP2"].ToString());
            }
            if (decimal.Parse(DR_TOT_USD["FKM_INVAMT"].ToString()) == 0 ){
                DR_TOT_USD["FKM_INVAMT"]=null;
                }
                else
                {
                DR_TOT_USD["FKM_INVAMT_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_TOT_USD["FKM_INVAMT"].ToString());
            }
            if (decimal.Parse(DR_TOT_USD["FKM_CAPPD"].ToString()) == 0 ){
                DR_TOT_USD["FKM_CAPPD"] =null;
                }
                else
                {
                DR_TOT_USD["FKM_CAPPD_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_TOT_USD["FKM_CAPPD"].ToString());
            }
            if (decimal.Parse(DR_TOT_USD["FKM_CAPUNPD"].ToString()) == 0 ){
                DR_TOT_USD["FKM_CAPUNPD"] =null;
                }
                else
                {
                DR_TOT_USD["FKM_CAPUNPD_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_TOT_USD["FKM_CAPUNPD"].ToString());
            }
            if (decimal.Parse(DR_TOT_USD["FKM_EXPNS"].ToString()) == 0 ){
                DR_TOT_USD["FKM_EXPNS"] =null;
                }
                else
                {
                DR_TOT_USD["FKM_EXPNS_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_TOT_USD["FKM_EXPNS"].ToString());
            }
            if (decimal.Parse(DR_TOT_USD["FKM_ROI"].ToString()) == 0 ){
                DR_TOT_USD["FKM_ROI"] =null;
                }
                else
                {
                DR_TOT_USD["FKM_ROI_R"] = string.Format("{0:#0.00}", DR_TOT_USD["FKM_ROI"].ToString()) + "%";
            }
            if (decimal.Parse(DR_TOT_USD["FKM_BOOKVAL"].ToString()) == 0 ){
                DR_TOT_USD["FKM_BOOKVAL"]=null;
                }
                else
                {
                DR_TOT_USD["FKM_BOOKVAL_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_TOT_USD["FKM_BOOKVAL"].ToString());
            }
            if (decimal.Parse(DR_TOT_USD["FKM_MONYCAL"].ToString()) == 0 ){
                DR_TOT_USD["FKM_MONYCAL"]=null;
                }
                else
                {
                DR_TOT_USD["FKM_MONYCAL_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_TOT_USD["FKM_MONYCAL"].ToString());
            }
            if (decimal.Parse(DR_TOT_USD["FKM_QRTYCAL"].ToString()) == 0 ){
                DR_TOT_USD["FKM_QRTYCAL"] =null;
                }
                else
                {
                DR_TOT_USD["FKM_QRTYCAL_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_TOT_USD["FKM_QRTYCAL"].ToString());
            }
            if (decimal.Parse(DR_TOT_USD["FKM_SMANYCAL"].ToString()) == 0 ){
                DR_TOT_USD["FKM_SMANYCAL"] =null;
                }
                else
                {
                DR_TOT_USD["FKM_SMANYCAL_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_TOT_USD["FKM_SMANYCAL"].ToString());
            }
            if (decimal.Parse(DR_TOT_USD["FKM_ANLYCAL"].ToString()) == 0 ){
                DR_TOT_USD["FKM_ANLYCAL"] =null;
                }
                else
                {
                DR_TOT_USD["FKM_ANLYCAL_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_TOT_USD["FKM_ANLYCAL"].ToString());
            }
            if (decimal.Parse(DR_TOT_USD["FKM_SCNINCP"].ToString()) == 0 ){
                DR_TOT_USD["FKM_SCNINCP"] =null;
                }
                else
                {
                DR_TOT_USD["FKM_SCNINCP_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_TOT_USD["FKM_SCNINCP"].ToString());
            }
            if (decimal.Parse(DR_TOT_USD["FKM_ANLINCMCY"].ToString()) == 0 ){
                DR_TOT_USD["FKM_ANLINCMCY"] =null;
                }
                else
                {
                DR_TOT_USD["FKM_ANLINCMCY_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_TOT_USD["FKM_ANLINCMCY"].ToString());
            }
            if (decimal.Parse(DR_TOT_USD["FKM_ANLRLZD"].ToString()) == 0 ){
                DR_TOT_USD["FKM_ANLRLZD"] =null;
                }
                else
                {
                DR_TOT_USD["FKM_ANLRLZD_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_TOT_USD["FKM_ANLRLZD"].ToString());
            }
            if (decimal.Parse(DR_TOT_USD["FKM_ACTLINCMRU"].ToString()) == 0 ){
                DR_TOT_USD["FKM_ACTLINCMRU"] =null;
                }
                else
                {
                DR_TOT_USD["FKM_ACTLINCMRU_R"] = string.Format("{0:#0.00}", DR_TOT_USD["FKM_ACTLINCMRU"].ToString()) + "%";
            }
            if (decimal.Parse(DR_TOT_USD["FKM_UNRLYLD"].ToString()) == 0 ){
                DR_TOT_USD["FKM_UNRLYLD"] =null;
                }
                else
                {
                DR_TOT_USD["FKM_UNRLYLD_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_TOT_USD["FKM_UNRLYLD"].ToString());
            }
            if (decimal.Parse(DR_TOT_USD["FKM_DVDND"].ToString()) == 0 ){
                DR_TOT_USD["FKM_DVDND"] =null;
                }
                else
                {
                DR_TOT_USD["FKM_DVDND_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_TOT_USD["FKM_DVDND"].ToString());
            }
            if (decimal.Parse(DR_TOT_USD["FKM_VALVTN"].ToString()) == 0 ){
                DR_TOT_USD["FKM_VALVTN"]=null;
                }
                else
                {
                DR_TOT_USD["FKM_VALVTN_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_TOT_USD["FKM_VALVTN"].ToString());
            }
            if (decimal.Parse(DR_TOT_USD["FKM_CAPGN"].ToString()) == 0 ){
                DR_TOT_USD["FKM_CAPGN"] =null;
                }
                else
                {
                DR_TOT_USD["FKM_CAPGN_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_TOT_USD["FKM_CAPGN"].ToString());
            }
            if (decimal.Parse(DR_TOT_USD["FKM_UNRLDVD"].ToString()) == 0 ){
                DR_TOT_USD["FKM_UNRLDVD"] =null;
                }
                else
                {
                DR_TOT_USD["FKM_UNRLDVD_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_TOT_USD["FKM_UNRLDVD"].ToString());
            }
            if (decimal.Parse(DR_TOT_USD["FKM_UNRLDVDCAP"].ToString()) == 0 ){
                DR_TOT_USD["FKM_UNRLDVDCAP"] =null;
                }
                else
                {
                DR_TOT_USD["FKM_UNRLDVDCAP_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_TOT_USD["FKM_UNRLDVDCAP"].ToString());
            }
            if (decimal.Parse(DR_TOT_USD["FKM_FAIRVAL"].ToString()) == 0 ){
                DR_TOT_USD["FKM_FAIRVAL"] =null;
                }
                else
                {
                DR_TOT_USD["FKM_FAIRVAL_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_TOT_USD["FKM_FAIRVAL"].ToString());
            }
            if (decimal.Parse(DR_TOT_USD["FKM_NAV"].ToString()) == 0 ){
                DR_TOT_USD["FKM_NAV"] =null;
                }
                else
                {
                DR_TOT_USD["FKM_NAV_R"] = string.Format("{0:#0.00}", DR_TOT_USD["FKM_NAV"].ToString());
            }
            if (decimal.Parse(DR_TOT_USD["FKM_SALEPRCD"].ToString()) == 0 ){
                DR_TOT_USD["FKM_SALEPRCD"] =null;
                }
                else
                {
                DR_TOT_USD["FKM_SALEPRCD_R"] = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_TOT_USD["FKM_SALEPRCD"].ToString());
            }

            //'DR_TOT_USD["FKM_REG") = (string)dbo.GET_FRM_COMP_CD(Session("USER_COMP_CD"), 1)
            //'  DR_TOT_USD["LOGO") = dbo.ReadFile(Server.MapPath((string)dbo.GET_FRM_COMP_CD(Session("USER_COMP_CD"), 2)))

            DR_TOT_USD["FOOTER"] = "";//User.Identity.Name.ToUpper;

            dt_invest_info.Rows.Add(DR_TOT_USD);
        }

        }

   public void   GridView1_RowDataBound(object sender  , System.Web.UI.WebControls.GridViewRowEventArgs e  )
       //Handles GridView1.RowDataBound
   {
       if ( e.Row.RowType == DataControlRowType.DataRow ){
           var drval =  DataBinder.Eval(e.Row.DataItem, "FKM_SRL");
            if(  drval==null) 
           {
                e.Row.BackColor = System.Drawing.Color.DarkGray;
                e.Row.ForeColor = System.Drawing.Color.Black;
                e.Row.Font.Bold = true;
            }
            else
           {
               drval = DataBinder.Eval(e.Row.DataItem, "FKM_REG");
               if (drval == "FENKINVEST")
               {
                    e.Row.ForeColor = System.Drawing.Color.Crimson;}
               else if (drval == "PERSONAL")
               {
                    e.Row.ForeColor = System.Drawing.Color.DarkViolet;
               }
               else if (drval == "TECHSQUARE")
               {
                    e.Row.ForeColor = System.Drawing.Color.DarkGray;
               }
           }
       }

   }
    //Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
    //    Dim rpt As New ReportDocument
    //    Dim rname As string = Server.MapPath("~/Reports/HOME_GENRPT.rpt")
    //    Dim dwnldfname As string = Server.MapPath("~/ReportsGenerate/gendwnload.xls")
    //    Dim dt As DataTable = TryCast(Session("HOME_dt_invest_info"), DataTable)

    //    if dt.Rows.Count > 0 ){
    //        rpt.Load(rname)
    //        rpt.SetDataSource(dt)

    //        'rpt.PrintOptions.PaperSize = PaperSize.PaperA4
    //        'rpt.PrintOptions.PaperOrientation = PaperOrientation.Landscape
    //        rpt.ExportToDisk(ExportFormatType.Excel, dwnldfname)
    //        rpt.Close()
    //        rpt.Dispose()


    //        Response.ClearContent()
    //        Response.ClearHeaders()
    //        Response.AddHeader("content-disposition", "attachment;filename =" & "gendwnload.xls")
    //        Response.ContentType = "application/ms-excel"
    //        Response.TransmitFile(dwnldfname)

    //   }
    //End Sub
    //Protected Sub Button1b_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1b.Click
    //    Dim rpt As New ReportDocument
    //    Dim rname As string = Server.MapPath("~/Reports/HOME_GENRPT2.rpt")
    //    Dim dwnldfname As string = Server.MapPath("~/ReportsGenerate/sumdwnload.xls")
    //    Dim dt As DataTable = TryCast(Session("HOME_dt_invest_info"), DataTable)

    //    if dt.Rows.Count > 0 ){
    //        rpt.Load(rname)
    //        rpt.SetDataSource(dt)

    //        'rpt.PrintOptions.PaperSize = PaperSize.PaperA4
    //        'rpt.PrintOptions.PaperOrientation = PaperOrientation.Landscape
    //        rpt.ExportToDisk(ExportFormatType.Excel, dwnldfname)
    //        rpt.Close()
    //        rpt.Dispose()


    //        Response.ClearContent()
    //        Response.ClearHeaders()
    //        Response.AddHeader("content-disposition", "attachment;filename =" & "gendwnload.xls")
    //        Response.ContentType = "application/ms-excel"
    //        Response.TransmitFile(dwnldfname)

    //   }
    //End Sub

    //Function GET_NAV_QUARTERCD(ByRef srchtxt As string) As Boolean

    //    Dim mnth As string = srchtxt.Substring(0, 3)
    //    Dim yr As string = srchtxt.Substring(3, 4)
    //    Dim cd As string = "" 'srchtxt.Substring(3, 4)
    //    Dim flg As Boolean = False 'srchtxt.Substring(3, 4)
    //    Select Case mnth
    //        Case "MAR" : cd = yr & "03" : flg = True
    //        Case "JUN" : cd = yr & "06" : flg = True
    //        Case "SEP" : cd = yr & "09" : flg = True
    //        Case "DEC" : cd = yr & "12" : flg = True
    //        Case Else : cd = srchtxt : flg = False
    //    End Select
    //    srchtxt = cd
    //    'if Today > CDate("30/09/" & Today.Year) ){
    //    '    DT.Rows.Add("DECEMBER - " & (Today.Year), (Today.Year) & "12")
    //    'End if

    //    'if Today > CDate("30/06/" & Today.Year) ){
    //    '    DT.Rows.Add("SEPTEMBER - " & (Today.Year), (Today.Year) & "09")
    //    'End if

    //    'if Today > CDate("31/03/" & Today.Year) ){
    //    '    DT.Rows.Add("JUNE - " & (Today.Year), (Today.Year) & "06")
    //    'End if

    //    'if Today >= CDate("01/01/" & Today.Year) ){
    //    '    DT.Rows.Add("MARCH - " & (Today.Year), (Today.Year) & "03")
    //    'End if

    //    'Dim YEAR As Integer = 2015
    //    'For I = 1 To (Today.Year - YEAR)

    //    '    DT.Rows.Add("DECEMBER - " & (Today.Year - I), (Today.Year - I) & "12")
    //    '    DT.Rows.Add("SEPTEMBER - " & (Today.Year - I), (Today.Year - I) & "09")
    //    '    DT.Rows.Add("JUNE - " & (Today.Year - I), (Today.Year - I) & "06")
    //    '    DT.Rows.Add("MARCH - " & (Today.Year - I), (Today.Year - I) & "03")

    //    'Next

    //    Return flg
    //End Function
    //Protected Sub REFINE_QRY_NAV(ByVal QUARTERCD As string)

    //    Dim COMP_CD As string = Session("HOME_COMP_CD")
    //    Dim RETRVQRY As string = "SELECT * FROM NAV_INFO WHERE FKM_STATUS = 'Y'  AND  FKM_ISSDATE LIKE '" & QUARTERCD & "' AND COMP_CD LIKE '" & COMP_CD & "' "
    //    Dim DTINVINFO As DataTable = dbo.SelTable(RETRVQRY)

    //    if DTINVINFO.Rows.Count > 0 ){

    //        DTINVINFO.Columns.Add("FKM_COMMCAP_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_COMMCAP2_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_INVAMT_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_CAPPD_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_CAPUNPD_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_CAPRFND_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_EXPNS_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_ROI_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_BOOKVAL_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_MONYCAL_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_QRTYCAL_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_SMANYCAL_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_ANLYCAL_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_SCNINCP_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_ANLINCMCY_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_ANLRLZD_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_ACTLINCMRU_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_UNRLYLD_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_DVDND_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_VALVTN_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_CAPGN_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_UNRLDVD_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_UNRLDVDCAP_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_FAIRVAL_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_NAV_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_SALEPRCD_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_CURR_SMBL", typeof(string))
    //        DTINVINFO.Columns.Add("FOOTER", typeof(string))
    //        DTINVINFO.Columns.Add("HEADER1", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_REG", typeof(string))
    //        DTINVINFO.Columns.Add("LOGO", typeof(Byte()))




    //        Dim dt_NAVRPT_info As DataTable = DTINVINFO.Clone
    //        dt_NAVRPT_info.Rows.Clear()

    //        Call REFINE_ROW_NAV_DETAILS(DTINVINFO, dt_NAVRPT_info, "US DOLLAR", "$")
    //        Call REFINE_ROW_NAV_DETAILS(DTINVINFO, dt_NAVRPT_info, "UK POUND", "£")
    //        Call REFINE_ROW_NAV_DETAILS(DTINVINFO, dt_NAVRPT_info, "EURO", "€")


    //        Session("HOME_dt_NAVRPT_REPORT") = Nothing
    //        if dt_NAVRPT_info.Rows.Count > 0 ){
    //            Session("HOME_dt_NAVRPT_REPORT") = dt_NAVRPT_info
    //            'GridView1.DataSource = dt_NAVRPT_info
    //            'GridView1.DataBind()

    //       }
    //   }


    //End Sub
    //Protected Sub REFINE_ROW_NAV_DETAILS(ByRef DTINVINFO As DataTable, ByRef dt_NAVRPT_info As DataTable, ByVal CURR As string, ByVal CURRSYMB As string)

    //    Session.LCID = 2057

    //    Dim DR_USD As DataRow() = DTINVINFO.Select("FKM_CURR  = '" & CURR & "' ")

    //    if DR_USD.Length > 0 ){
    //        For I = 0 To DR_USD.Length - 1

    //            'DR_TOT_USD["COMP_CD") = DR_USD[I]["COMP_CD")

    //            if decimal.Parse(DR_USD[I]["FKM_COMMCAP"].ToString()) = 0 ){
    //                DR_USD[I]["FKM_COMMCAP") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_COMMCAP_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_COMMCAP"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_COMMCAP2"].ToString()) = 0 ){
    //                DR_USD[I]["FKM_COMMCAP2") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_COMMCAP2_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_COMMCAP2"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_INVAMT"].ToString()) = 0 ){
    //                DR_USD[I]["FKM_INVAMT") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_INVAMT_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_INVAMT"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_CAPPD"].ToString()) = 0 ){
    //                DR_USD[I]["FKM_CAPPD") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_CAPPD_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_CAPPD"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_CAPUNPD"].ToString()) = 0 ){
    //                DR_USD[I]["FKM_CAPUNPD") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_CAPUNPD_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_CAPUNPD"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_EXPNS"].ToString()) = 0 ){
    //                DR_USD[I]["FKM_EXPNS") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_EXPNS_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_EXPNS"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_ROI"].ToString()) = 0 ){
    //                DR_USD[I]["FKM_ROI") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_ROI_R") = string.Format("{0:#0.00}", DR_USD[I]["FKM_ROI"].ToString()) & "%"
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_BOOKVAL"].ToString()) = 0 ){
    //                DR_USD[I]["FKM_BOOKVAL") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_BOOKVAL_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_BOOKVAL"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_MONYCAL"].ToString()) = 0 ){
    //                DR_USD[I]["FKM_MONYCAL") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_MONYCAL_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_MONYCAL"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_QRTYCAL"].ToString()) = 0 ){
    //                DR_USD[I]["FKM_QRTYCAL") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_QRTYCAL_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_QRTYCAL"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_SMANYCAL"].ToString()) = 0 ){
    //                DR_USD[I]["FKM_SMANYCAL") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_SMANYCAL_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_SMANYCAL"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_ANLYCAL"].ToString()) = 0 ){
    //                DR_USD[I]["FKM_ANLYCAL") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_ANLYCAL_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_ANLYCAL"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_SCNINCP"].ToString()) = 0 ){
    //                DR_USD[I]["FKM_SCNINCP") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_SCNINCP_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_SCNINCP"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_ANLINCMCY"].ToString()) = 0 ){
    //                DR_USD[I]["FKM_ANLINCMCY") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_ANLINCMCY_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_ANLINCMCY"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_ANLRLZD"].ToString()) = 0 ){
    //                DR_USD[I]["FKM_ANLRLZD") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_ANLRLZD_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_ANLRLZD"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_ACTLINCMRU"].ToString()) = 0 ){
    //                DR_USD[I]["FKM_ACTLINCMRU") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_ACTLINCMRU_R") = string.Format("{0:#0.00}", DR_USD[I]["FKM_ACTLINCMRU"].ToString()) & "%"
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_UNRLYLD"].ToString()) = 0 ){
    //                DR_USD[I]["FKM_UNRLYLD") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_UNRLYLD_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_UNRLYLD"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_DVDND"].ToString()) = 0 ){
    //                DR_USD[I]["FKM_DVDND") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_DVDND_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_DVDND"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_VALVTN"].ToString()) = 0 ){
    //                DR_USD[I]["FKM_VALVTN") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_VALVTN_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_VALVTN"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_CAPGN"].ToString()) = 0 ){
    //                DR_USD[I]["FKM_CAPGN") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_CAPGN_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_CAPGN"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_UNRLDVD"].ToString()) = 0 ){
    //                DR_USD[I]["FKM_UNRLDVD") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_UNRLDVD_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_UNRLDVD"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_UNRLDVDCAP"].ToString()) = 0 ){
    //                DR_USD[I]["FKM_UNRLDVDCAP") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_UNRLDVDCAP_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_UNRLDVDCAP"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_FAIRVAL"].ToString()) = 0 ){
    //                DR_USD[I]["FKM_FAIRVAL") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_FAIRVAL_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_FAIRVAL"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_NAV"].ToString()) = 0 ){
    //                DR_USD[I]["FKM_NAV") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_NAV_R") = string.Format("{0:#0.00}", DR_USD[I]["FKM_NAV"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_SALEPRCD"].ToString()) = 0 ){
    //                DR_USD[I]["FKM_SALEPRCD") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_SALEPRCD_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_SALEPRCD"].ToString())
    //           }


    //            'if DR_USD[I]["FKM_PRVTEQT").ToString.Trim = "Y" ){
    //            '    DR_USD[I]["FKM_PRVTEQT") = "PRIVATE EQUITY"
    //            'Elseif DR_USD[I]["FKM_PRVTEQT").ToString.Trim = "N" ){
    //            '    DR_USD[I]["FKM_PRVTEQT") = "NON PRIVATE EQUITY"
    //            'End if

    //            if DR_USD[I]["FKM_INCGEN").ToString.Trim = "Y" ){
    //                DR_USD[I]["FKM_INCGEN") = "INCOME GENERATING "
    //            Elseif DR_USD[I]["FKM_INCGEN").ToString.Trim = "N" ){
    //                DR_USD[I]["FKM_INCGEN") = "NON INCOME GENERATING "
    //           }

    //            'if DR_USD[I]["FKM_YEILDPRD").ToString.Trim = "M" ){
    //            '    DR_USD[I]["FKM_YEILDPRD") = "MONTHLY"
    //            'Elseif DR_USD[I]["FKM_YEILDPRD").ToString.Trim = "Q" ){
    //            '    DR_USD[I]["FKM_YEILDPRD") = "QUARTERLY"
    //            'Elseif DR_USD[I]["FKM_YEILDPRD").ToString.Trim = "S" ){
    //            '    DR_USD[I]["FKM_YEILDPRD") = "SEMI ANNUAL"
    //            'End if


    //            'if Not IsDBNull(DR_USD[I]["FKM_MTRTDATE"].ToString()) ){
    //            '    Dim DTE As string = DR_USD[I]["FKM_MTRTDATE").ToString.Substring(3, 2) & "/" & DR_USD[I]["FKM_MTRTDATE").ToString.Substring(0, 2) & "/" & DR_USD[I]["FKM_MTRTDATE").ToString.Substring(6, 4)
    //            '    DR_USD[I]["FKM_MTRTDATE") = CDate(DTE)
    //            'End if
    //            if Not IsDBNull(DR_USD[I]["FKM_REMX_NAV"].ToString()) ){
    //                if DR_USD[I]["FKM_REMX_NAV").ToString.Trim.Length > 0 ){
    //                    DR_USD[I]["FKM_REMX") = DR_USD[I]["FKM_REMX_NAV")
    //                Else
    //                    DR_USD[I]["FKM_REMX") = ". "
    //               }
    //            Else
    //                DR_USD[I]["FKM_REMX") = ". "
    //           }
    //            'if Not IsDBNull(DR_USD[I]["COMP_CD"].ToString()) ){
    //            '    if DR_USD[I]["COMP_CD") = "001" ){
    //            '        DR_USD[I]["FKM_REG") = "FKMINVEST"
    //            '    Elseif DR_USD[I]["COMP_CD") = "002" ){
    //            '        DR_USD[I]["FKM_REG") = "FENKINVEST"
    //            '    Elseif DR_USD[I]["COMP_CD") = "003" ){
    //            '        DR_USD[I]["FKM_REG") = "PERSONAL"
    //            '   }
    //            'End if

    //            DR_USD[I]["FKM_REG") = (string)dbo.GET_FRM_COMP_CD(DR_USD[I]["COMP_CD"), 1)
    //            DR_USD[I]["LOGO") = dbo.ReadFile(Server.MapPath((string)dbo.GET_FRM_COMP_CD(DR_USD[I]["COMP_CD"), 2)))

    //            DR_USD[I]["FKM_CURR_SMBL") = CURRSYMB
    //            DR_USD[I]["FOOTER") = User.Identity.Name.ToUpper
    //            DR_USD[I]["HEADER1") = TXT_SRCH.Text.ToUpper

    //            dt_NAVRPT_info.ImportRow(DR_USD(I))
    //        Next


    //   }

    //End Sub
    //Protected Sub REFINE_QRY_TO_GRID_MAIN(ByVal QUARTERCD As string, ByVal CURR As string, ByVal CURRSYMB As string, ByVal INCGEN As string, ByVal INVGRP As string)

    //    Dim COMP_CD As string = Session("HOME_COMP_CD")
    //    Dim RETRVQRY As string = "SELECT COMP_CD,FKM_SRL ,FKM_PROJNAME,FKM_PRTCPDATE ,FKM_MTRTDATE,FKM_REMX_NAV AS FKM_REMX , " &
    //                                "FKM_CURR, FKM_INCGEN,FKM_INVGRP, " &
    //                             " FKM_COMMCAP ,   FKM_COMMCAP2 ," &
    //                             " FKM_INVAMT, FKM_CAPPD, " &
    //                             "  FKM_CAPUNPD , FKM_CAPRFND," &
    //                             " FKM_EXPNS, FKM_ROI, " &
    //                             "  FKM_BOOKVAL, FKM_MONYCAL," &
    //                             " FKM_QRTYCAL, FKM_SMANYCAL, " &
    //                             "  FKM_ANLYCAL, FKM_SCNINCP," &
    //                             " FKM_ANLINCMCY, FKM_ANLRLZD, " &
    //                             "  FKM_ACTLINCMRU, FKM_UNRLYLD," &
    //                             " FKM_DVDND, FKM_VALVTN, " &
    //                             "  FKM_CAPGN, FKM_UNRLDVD," &
    //                             " FKM_UNRLDVDCAP, FKM_FAIRVAL, " &
    //                             "  FKM_NAV, FKM_SALEPRCD" &
    //                             " FROM NAV_INFO " &
    //                             " WHERE FKM_CURR = '" & CURR & "' AND FKM_INCGEN = '" & INCGEN & "' AND FKM_INVGRP = '" & INVGRP &
    //                             "' AND FKM_STATUS = 'Y'  AND  FKM_ISSDATE LIKE '" & QUARTERCD & "'  AND COMP_CD LIKE '" & COMP_CD & "'" &
    //                                     " ORDER BY FKM_CURR DESC, FKM_INCGEN DESC ,FKM_INVGRP,FKM_SRL "

    //    Dim DTINVINFO As DataTable = dbo.SelTable(RETRVQRY)

    //    if DTINVINFO.Rows.Count > 0 ){

    //        'DTINVINFO.Columns.Add("FKM_SRL", typeof(string))
    //        'DTINVINFO.Columns.Add("FKM_PROJNAME", typeof(string))
    //        '' DTINVINFO.Columns.Add("FKM_INVGRP", typeof(string))
    //        'DTINVINFO.Columns.Add("FKM_PRTCPDATE", typeof(string))
    //        'DTINVINFO.Columns.Add("FKM_MTRTDATE", typeof(string))
    //        'DTINVINFO.Columns.Add("FKM_REMX", typeof(string))

    //        DTINVINFO.Columns.Add("FKM_COMMCAP_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_COMMCAP2_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_INVAMT_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_CAPPD_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_CAPUNPD_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_CAPRFND_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_EXPNS_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_ROI_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_BOOKVAL_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_MONYCAL_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_QRTYCAL_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_SMANYCAL_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_ANLYCAL_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_SCNINCP_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_ANLINCMCY_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_ANLRLZD_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_ACTLINCMRU_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_UNRLYLD_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_DVDND_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_VALVTN_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_CAPGN_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_UNRLDVD_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_UNRLDVDCAP_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_FAIRVAL_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_NAV_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_SALEPRCD_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_CURR_SMBL", typeof(string))
    //        DTINVINFO.Columns.Add("FOOTER", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_REG", typeof(string))
    //        DTINVINFO.Columns.Add("LOGO", typeof(Byte()))



    //        Dim dt_NAVRPT_info As DataTable = DTINVINFO.Clone
    //        dt_NAVRPT_info.Rows.Clear()

    //        Call REFINE_ROW_GRIDS_NAV(DTINVINFO, dt_NAVRPT_info, CURR, CURRSYMB, "FKM_CURR", CURR)
    //        'Call REFINE_ROW_GRIDS(DTINVINFO, dt_NAVRPT_info, "EURO", "€")

    //   }


    //End Sub
    //Protected Sub REFINE_QRY_TO_GRID_INVGRP(ByVal QUARTERCD As string, ByVal CURR As string, ByVal CURRSYMB As string, ByVal INCGEN As string)

    //    Dim COMP_CD As string = Session("HOME_COMP_CD")
    //    Dim RETRVQRY As string = "SELECT   FKM_CURR, FKM_INCGEN,FKM_INVGRP, " &
    //                             " SUM(FKM_COMMCAP) AS FKM_COMMCAP ,SUM(FKM_COMMCAP2)  AS FKM_COMMCAP2 ," &
    //                             "SUM(FKM_INVAMT) AS FKM_INVAMT,SUM(FKM_CAPPD) AS FKM_CAPPD, " &
    //                             " SUM(FKM_CAPUNPD) AS FKM_CAPUNPD ,SUM(FKM_CAPRFND) AS FKM_CAPRFND," &
    //                             "SUM(FKM_EXPNS) AS FKM_EXPNS,AVG(FKM_ROI) AS FKM_ROI, " &
    //                             " SUM(FKM_BOOKVAL) AS FKM_BOOKVAL,SUM(FKM_MONYCAL) AS FKM_MONYCAL," &
    //                             "SUM(FKM_QRTYCAL) AS FKM_QRTYCAL,SUM(FKM_SMANYCAL) AS FKM_SMANYCAL, " &
    //                             " SUM(FKM_ANLYCAL) AS FKM_ANLYCAL,SUM(FKM_SCNINCP) AS FKM_SCNINCP," &
    //                             "SUM(FKM_ANLINCMCY) AS FKM_ANLINCMCY,SUM(FKM_ANLRLZD) AS FKM_ANLRLZD, " &
    //                             " AVG(FKM_ACTLINCMRU) AS FKM_ACTLINCMRU,SUM(FKM_UNRLYLD) AS FKM_UNRLYLD," &
    //                             "SUM(FKM_DVDND) AS FKM_DVDND,SUM(FKM_VALVTN) AS FKM_VALVTN, " &
    //                             " SUM(FKM_CAPGN) AS FKM_CAPGN,SUM(FKM_UNRLDVD) AS FKM_UNRLDVD," &
    //                             "SUM(FKM_UNRLDVDCAP) AS FKM_UNRLDVDCAP,SUM(FKM_FAIRVAL) AS FKM_FAIRVAL, " &
    //                             " AVG(FKM_NAV) AS FKM_NAV,SUM(FKM_SALEPRCD)  AS FKM_SALEPRCD" &
    //                             " FROM NAV_INFO " &
    //                             " WHERE FKM_CURR = '" & CURR & "' AND FKM_INCGEN = '" & INCGEN & "' AND FKM_STATUS = 'Y'  AND  FKM_ISSDATE LIKE '" & QUARTERCD & "' AND  COMP_CD LIKE '" & COMP_CD & "'" &
    //                             " GROUP BY  FKM_CURR , FKM_INCGEN ,FKM_INVGRP " &
    //                             " ORDER BY FKM_CURR DESC, FKM_INCGEN DESC ,FKM_INVGRP "


    //    Dim DTINVINFO As DataTable = dbo.SelTable(RETRVQRY)

    //    if DTINVINFO.Rows.Count > 0 ){

    //        DTINVINFO.Columns.Add("FKM_SRL", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_PROJNAME", typeof(string))
    //        ' DTINVINFO.Columns.Add("FKM_INVGRP", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_PRTCPDATE", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_MTRTDATE", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_REMX", typeof(string))

    //        DTINVINFO.Columns.Add("FKM_COMMCAP_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_COMMCAP2_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_INVAMT_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_CAPPD_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_CAPUNPD_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_CAPRFND_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_EXPNS_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_ROI_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_BOOKVAL_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_MONYCAL_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_QRTYCAL_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_SMANYCAL_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_ANLYCAL_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_SCNINCP_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_ANLINCMCY_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_ANLRLZD_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_ACTLINCMRU_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_UNRLYLD_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_DVDND_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_VALVTN_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_CAPGN_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_UNRLDVD_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_UNRLDVDCAP_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_FAIRVAL_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_NAV_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_SALEPRCD_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_CURR_SMBL", typeof(string))
    //        DTINVINFO.Columns.Add("FOOTER", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_REG", typeof(string))
    //        DTINVINFO.Columns.Add("LOGO", typeof(Byte()))



    //        Dim dt_NAVRPT_info As DataTable = DTINVINFO.Clone
    //        dt_NAVRPT_info.Rows.Clear()
    //        For Each DR In DTINVINFO.Rows
    //            REFINE_QRY_TO_GRID_MAIN(QUARTERCD, CURR, CURRSYMB, INCGEN, DR("FKM_INVGRP"].ToString())
    //            Call REFINE_ROW_GRIDS_NAV(DTINVINFO, dt_NAVRPT_info, CURR, CURRSYMB, "FKM_INVGRP", DR("FKM_INVGRP"].ToString())
    //        Next

    //        'Call REFINE_ROW_GRIDS(DTINVINFO, dt_NAVRPT_info, CURR, CURRSYMB, "FKM_INCGEN", DR("FKM_INVGRP"].ToString())
    //        'Call REFINE_ROW_GRIDS(DTINVINFO, dt_NAVRPT_info, "UK POUND", "£")
    //        'Call REFINE_ROW_GRIDS(DTINVINFO, dt_NAVRPT_info, "EURO", "€")


    //        '' Session("HOME_dt_NAVRPT_info") = Nothing
    //        'if dt_NAVRPT_info.Rows.Count > 0 ){
    //        '    Session("HOME_dt_NAVRPT_info") = dt_NAVRPT_info
    //        '    'GridView1.DataSource = dt_NAVRPT_info
    //        '    'GridView1.DataBind()



    //        'End if
    //   }


    //End Sub
    //Protected Sub REFINE_QRY_TO_GRID_INCGEN(ByVal QUARTERCD As string, ByVal CURR As string, ByVal CURRSYMB As string)

    //    Dim COMP_CD As string = Session("HOME_COMP_CD")
    //    Dim RETRVQRY As string = "SELECT  FKM_CURR, FKM_INCGEN, " &
    //                             " SUM(FKM_COMMCAP) AS FKM_COMMCAP ,SUM(FKM_COMMCAP2)  AS FKM_COMMCAP2 ," &
    //                             "SUM(FKM_INVAMT) AS FKM_INVAMT,SUM(FKM_CAPPD) AS FKM_CAPPD, " &
    //                             " SUM(FKM_CAPUNPD) AS FKM_CAPUNPD ,SUM(FKM_CAPRFND) AS FKM_CAPRFND," &
    //                             "SUM(FKM_EXPNS) AS FKM_EXPNS,AVG(FKM_ROI) AS FKM_ROI, " &
    //                             " SUM(FKM_BOOKVAL) AS FKM_BOOKVAL,SUM(FKM_MONYCAL) AS FKM_MONYCAL," &
    //                             "SUM(FKM_QRTYCAL) AS FKM_QRTYCAL,SUM(FKM_SMANYCAL) AS FKM_SMANYCAL, " &
    //                             " SUM(FKM_ANLYCAL) AS FKM_ANLYCAL,SUM(FKM_SCNINCP) AS FKM_SCNINCP," &
    //                             "SUM(FKM_ANLINCMCY) AS FKM_ANLINCMCY,SUM(FKM_ANLRLZD) AS FKM_ANLRLZD, " &
    //                             " AVG(FKM_ACTLINCMRU) AS FKM_ACTLINCMRU,SUM(FKM_UNRLYLD) AS FKM_UNRLYLD," &
    //                             "SUM(FKM_DVDND) AS FKM_DVDND,SUM(FKM_VALVTN) AS FKM_VALVTN, " &
    //                             " SUM(FKM_CAPGN) AS FKM_CAPGN,SUM(FKM_UNRLDVD) AS FKM_UNRLDVD," &
    //                             "SUM(FKM_UNRLDVDCAP) AS FKM_UNRLDVDCAP,SUM(FKM_FAIRVAL) AS FKM_FAIRVAL, " &
    //                             " AVG(FKM_NAV) AS FKM_NAV,SUM(FKM_SALEPRCD)  AS FKM_SALEPRCD" &
    //                             " FROM NAV_INFO " &
    //                             " WHERE FKM_CURR = '" & CURR & "' AND FKM_STATUS = 'Y'  AND  FKM_ISSDATE LIKE '" & QUARTERCD & "' AND  COMP_CD LIKE '" & COMP_CD & "' " &
    //                             " GROUP BY  FKM_CURR , FKM_INCGEN  " &
    //                             " ORDER BY FKM_CURR DESC, FKM_INCGEN DESC  "

    //    Dim DTINVINFO As DataTable = dbo.SelTable(RETRVQRY)

    //    if DTINVINFO.Rows.Count > 0 ){

    //        DTINVINFO.Columns.Add("FKM_SRL", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_PROJNAME", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_INVGRP", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_PRTCPDATE", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_MTRTDATE", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_REMX", typeof(string))

    //        DTINVINFO.Columns.Add("FKM_COMMCAP_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_COMMCAP2_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_INVAMT_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_CAPPD_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_CAPUNPD_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_CAPRFND_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_EXPNS_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_ROI_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_BOOKVAL_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_MONYCAL_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_QRTYCAL_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_SMANYCAL_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_ANLYCAL_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_SCNINCP_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_ANLINCMCY_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_ANLRLZD_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_ACTLINCMRU_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_UNRLYLD_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_DVDND_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_VALVTN_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_CAPGN_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_UNRLDVD_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_UNRLDVDCAP_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_FAIRVAL_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_NAV_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_SALEPRCD_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_CURR_SMBL", typeof(string))
    //        DTINVINFO.Columns.Add("FOOTER", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_REG", typeof(string))
    //        DTINVINFO.Columns.Add("LOGO", typeof(Byte()))



    //        Dim dt_NAVRPT_info As DataTable = DTINVINFO.Clone
    //        dt_NAVRPT_info.Rows.Clear()

    //        Call REFINE_QRY_TO_GRID_INVGRP(QUARTERCD, CURR, CURRSYMB, "Y")
    //        Call REFINE_ROW_GRIDS_NAV(DTINVINFO, dt_NAVRPT_info, CURR, CURRSYMB, "FKM_INCGEN", "Y")
    //        Call REFINE_QRY_TO_GRID_INVGRP(QUARTERCD, CURR, CURRSYMB, "N")
    //        Call REFINE_ROW_GRIDS_NAV(DTINVINFO, dt_NAVRPT_info, CURR, CURRSYMB, "FKM_INCGEN", "N")
    //        'Call REFINE_ROW_GRIDS(DTINVINFO, dt_NAVRPT_info, "UK POUND", "£")
    //        'Call REFINE_ROW_GRIDS(DTINVINFO, dt_NAVRPT_info, "EURO", "€")


    //        ''Session("HOME_dt_NAVRPT_info") = Nothing
    //        'if dt_NAVRPT_info.Rows.Count > 0 ){
    //        '    Session("HOME_dt_NAVRPT_info") = dt_NAVRPT_info
    //        '    GridView1.DataSource = dt_NAVRPT_info
    //        '    GridView1.DataBind()



    //        'End if
    //   }


    //End Sub
    //Protected Sub REFINE_QRY_TO_GRID_NAV(ByVal QUARTERCD As string)


    //    GridView1.DataSource = Nothing
    //    GridView1.DataBind()

    //    Dim COMP_CD As string = Session("HOME_COMP_CD")
    //    Dim RETRVQRY As string = "SELECT  FKM_CURR,  " &
    //                             " SUM(FKM_COMMCAP) AS FKM_COMMCAP ,SUM(FKM_COMMCAP2)  AS FKM_COMMCAP2 ," &
    //                             "SUM(FKM_INVAMT) AS FKM_INVAMT,SUM(FKM_CAPPD) AS FKM_CAPPD, " &
    //                             " SUM(FKM_CAPUNPD) AS FKM_CAPUNPD ,SUM(FKM_CAPRFND) AS FKM_CAPRFND," &
    //                             "SUM(FKM_EXPNS) AS FKM_EXPNS,AVG(FKM_ROI) AS FKM_ROI, " &
    //                             " SUM(FKM_BOOKVAL) AS FKM_BOOKVAL,SUM(FKM_MONYCAL) AS FKM_MONYCAL," &
    //                             "SUM(FKM_QRTYCAL) AS FKM_QRTYCAL,SUM(FKM_SMANYCAL) AS FKM_SMANYCAL, " &
    //                             " SUM(FKM_ANLYCAL) AS FKM_ANLYCAL,SUM(FKM_SCNINCP) AS FKM_SCNINCP," &
    //                             "SUM(FKM_ANLINCMCY) AS FKM_ANLINCMCY,SUM(FKM_ANLRLZD) AS FKM_ANLRLZD, " &
    //                             " AVG(FKM_ACTLINCMRU) AS FKM_ACTLINCMRU,SUM(FKM_UNRLYLD) AS FKM_UNRLYLD," &
    //                             "SUM(FKM_DVDND) AS FKM_DVDND,SUM(FKM_VALVTN) AS FKM_VALVTN, " &
    //                             " SUM(FKM_CAPGN) AS FKM_CAPGN,SUM(FKM_UNRLDVD) AS FKM_UNRLDVD," &
    //                             "SUM(FKM_UNRLDVDCAP) AS FKM_UNRLDVDCAP,SUM(FKM_FAIRVAL) AS FKM_FAIRVAL, " &
    //                             " AVG(FKM_NAV) AS FKM_NAV,SUM(FKM_SALEPRCD)  AS FKM_SALEPRCD" &
    //                             " FROM NAV_INFO WHERE FKM_STATUS = 'Y'  AND  FKM_ISSDATE LIKE '" & QUARTERCD & "' AND  COMP_CD LIKE '" & COMP_CD & "' " &
    //                             " GROUP BY  FKM_CURR   " &
    //                             " ORDER BY FKM_CURR DESC  "

    //    Dim DTINVINFO As DataTable = dbo.SelTable(RETRVQRY)

    //    if DTINVINFO.Rows.Count > 0 ){

    //        DTINVINFO.Columns.Add("FKM_SRL", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_PROJNAME", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_INCGEN", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_INVGRP", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_PRTCPDATE", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_MTRTDATE", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_REMX", typeof(string))

    //        DTINVINFO.Columns.Add("FKM_COMMCAP_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_COMMCAP2_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_INVAMT_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_CAPPD_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_CAPUNPD_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_CAPRFND_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_EXPNS_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_ROI_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_BOOKVAL_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_MONYCAL_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_QRTYCAL_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_SMANYCAL_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_ANLYCAL_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_SCNINCP_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_ANLINCMCY_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_ANLRLZD_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_ACTLINCMRU_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_UNRLYLD_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_DVDND_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_VALVTN_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_CAPGN_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_UNRLDVD_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_UNRLDVDCAP_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_FAIRVAL_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_NAV_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_SALEPRCD_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_CURR_SMBL", typeof(string))
    //        DTINVINFO.Columns.Add("FOOTER", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_REG", typeof(string))
    //        DTINVINFO.Columns.Add("LOGO", typeof(Byte()))



    //        Dim dt_NAVRPT_info As DataTable = DTINVINFO.Clone
    //        dt_NAVRPT_info.Rows.Clear()
    //        Call REFINE_QRY_TO_GRID_INCGEN(QUARTERCD, "US DOLLAR", "$")
    //        Call REFINE_ROW_GRIDS_NAV(DTINVINFO, dt_NAVRPT_info, "US DOLLAR", "$", "FKM_CURR", "US DOLLAR")
    //        Call REFINE_QRY_TO_GRID_INCGEN(QUARTERCD, "UK POUND", "£")
    //        Call REFINE_ROW_GRIDS_NAV(DTINVINFO, dt_NAVRPT_info, "UK POUND", "£", "FKM_CURR", "UK POUND")
    //        'Call REFINE_ROW_GRIDS(DTINVINFO, dt_NAVRPT_info, "EURO", "€")


    //        dt_NAVRPT_info = Session("HOME_dt_NAVRPT_info")
    //        if dt_NAVRPT_info.Rows.Count > 0 ){
    //            ' = dt_NAVRPT_info
    //            Button2.Visible = True
    //            GridView2.DataSource = dt_NAVRPT_info
    //            GridView2.DataBind()

    //       }
    //   }


    //End Sub

    //Protected Sub REFINE_ROW_GRIDS_NAV(ByRef DTINVINFO As DataTable, ByRef dt_NAVRPT_info As DataTable, ByVal CURR As string, ByVal CURRSYMB As string, ByVal FLDTYP As string, ByVal SLCTN As string)

    //    Session.LCID = 2057
    //    Dim dt_NAVRPT_GRID As New DataTable
    //    dt_NAVRPT_GRID = TryCast(Session("HOME_dt_NAVRPT_info"), DataTable)
    //    if dt_NAVRPT_GRID Is Nothing ){
    //        dt_NAVRPT_GRID = dt_NAVRPT_info.Clone
    //   }

    //    Dim DR_USD As DataRow() = DTINVINFO.Select("FKM_CURR  = '" & CURR & "' AND  " & FLDTYP & "  = '" & SLCTN & "'")
    //    if DR_USD.Length > 0 ){


    //        For I = 0 To DR_USD.Length - 1


    //            if decimal.Parse(DR_USD[I]["FKM_COMMCAP"].ToString()) = 0 ){
    //                ' DR_USD[I]["FKM_COMMCAP") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_COMMCAP_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_COMMCAP"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_COMMCAP2"].ToString()) = 0 ){
    //                'DR_USD[I]["FKM_COMMCAP2") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_COMMCAP2_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_COMMCAP2"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_INVAMT"].ToString()) = 0 ){
    //                ' DR_USD[I]["FKM_INVAMT") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_INVAMT_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_INVAMT"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_CAPPD"].ToString()) = 0 ){
    //                ' DR_USD[I]["FKM_CAPPD") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_CAPPD_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_CAPPD"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_CAPUNPD"].ToString()) = 0 ){
    //                ' DR_USD[I]["FKM_CAPUNPD") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_CAPUNPD_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_CAPUNPD"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_EXPNS"].ToString()) = 0 ){
    //                'DR_USD[I]["FKM_EXPNS") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_EXPNS_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_EXPNS"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_ROI"].ToString()) = 0 ){
    //                ' DR_USD[I]["FKM_ROI") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_ROI_R") = string.Format("{0:#0.00}", DR_USD[I]["FKM_ROI"].ToString()) & "%"
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_BOOKVAL"].ToString()) = 0 ){
    //                ' DR_USD[I]["FKM_BOOKVAL") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_BOOKVAL_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_BOOKVAL"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_MONYCAL"].ToString()) = 0 ){
    //                '  DR_USD[I]["FKM_MONYCAL") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_MONYCAL_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_MONYCAL"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_QRTYCAL"].ToString()) = 0 ){
    //                '  DR_USD[I]["FKM_QRTYCAL") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_QRTYCAL_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_QRTYCAL"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_SMANYCAL"].ToString()) = 0 ){
    //                '  DR_USD[I]["FKM_SMANYCAL") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_SMANYCAL_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_SMANYCAL"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_ANLYCAL"].ToString()) = 0 ){
    //                ' DR_USD[I]["FKM_ANLYCAL") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_ANLYCAL_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_ANLYCAL"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_SCNINCP"].ToString()) = 0 ){
    //                'DR_USD[I]["FKM_SCNINCP") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_SCNINCP_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_SCNINCP"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_ANLINCMCY"].ToString()) = 0 ){
    //                '  DR_USD[I]["FKM_ANLINCMCY") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_ANLINCMCY_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_ANLINCMCY"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_ANLRLZD"].ToString()) = 0 ){
    //                ' DR_USD[I]["FKM_ANLRLZD") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_ANLRLZD_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_ANLRLZD"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_ACTLINCMRU"].ToString()) = 0 ){
    //                'DR_USD[I]["FKM_ACTLINCMRU") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_ACTLINCMRU_R") = string.Format("{0:#0.00}", DR_USD[I]["FKM_ACTLINCMRU"].ToString()) & "%"
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_UNRLYLD"].ToString()) = 0 ){
    //                ' DR_USD[I]["FKM_UNRLYLD") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_UNRLYLD_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_UNRLYLD"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_DVDND"].ToString()) = 0 ){
    //                '  DR_USD[I]["FKM_DVDND") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_DVDND_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_DVDND"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_VALVTN"].ToString()) = 0 ){
    //                ' DR_USD[I]["FKM_VALVTN") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_VALVTN_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_VALVTN"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_CAPGN"].ToString()) = 0 ){
    //                '  DR_USD[I]["FKM_CAPGN") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_CAPGN_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_CAPGN"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_UNRLDVD"].ToString()) = 0 ){
    //                '  DR_USD[I]["FKM_UNRLDVD") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_UNRLDVD_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_UNRLDVD"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_UNRLDVDCAP"].ToString()) = 0 ){
    //                ' DR_USD[I]["FKM_UNRLDVDCAP") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_UNRLDVDCAP_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_UNRLDVDCAP"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_FAIRVAL"].ToString()) = 0 ){
    //                'DR_USD[I]["FKM_FAIRVAL") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_FAIRVAL_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_FAIRVAL"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_NAV"].ToString()) = 0 ){
    //                ' DR_USD[I]["FKM_NAV") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_NAV_R") = string.Format("{0:#0.00}", DR_USD[I]["FKM_NAV"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_SALEPRCD"].ToString()) = 0 ){
    //                ' DR_USD[I]["FKM_SALEPRCD") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_SALEPRCD_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_SALEPRCD"].ToString())
    //           }


    //            'if DR_USD[I]["FKM_PRVTEQT").ToString.Trim = "Y" ){
    //            '    DR_USD[I]["FKM_PRVTEQT") = "PRIVATE EQUITY"
    //            'Elseif DR_USD[I]["FKM_PRVTEQT").ToString.Trim = "N" ){
    //            '    DR_USD[I]["FKM_PRVTEQT") = "NON PRIVATE EQUITY"
    //            'End if

    //            if DR_USD[I]["FKM_INCGEN").ToString.Trim = "Y" ){
    //                DR_USD[I]["FKM_INCGEN") = "INCOME GENERATING "
    //            Elseif DR_USD[I]["FKM_INCGEN").ToString.Trim = "N" ){
    //                DR_USD[I]["FKM_INCGEN") = "NON INCOME GENERATING "
    //           }

    //            'if DR_USD[I]["FKM_YEILDPRD").ToString.Trim = "M" ){
    //            '    DR_USD[I]["FKM_YEILDPRD") = "MONTHLY"
    //            'Elseif DR_USD[I]["FKM_YEILDPRD").ToString.Trim = "Q" ){
    //            '    DR_USD[I]["FKM_YEILDPRD") = "QUARTERLY"
    //            'Elseif DR_USD[I]["FKM_YEILDPRD").ToString.Trim = "S" ){
    //            '    DR_USD[I]["FKM_YEILDPRD") = "SEMI ANNUAL"
    //            'End if


    //            'if Not IsDBNull(DR_USD[I]["FKM_PRTCPDATE"].ToString()) ){
    //            '    Dim DTE As string = DR_USD[I]["FKM_PRTCPDATE").ToString.Substring(3, 2) & "/" & DR_USD[I]["FKM_PRTCPDATE").ToString.Substring(0, 2) & "/" & DR_USD[I]["FKM_PRTCPDATE").ToString.Substring(6, 4)
    //            '    DR_USD[I]["FKM_PRTCPDATE") = CDate(DTE)
    //            'End if
    //            'if Not IsDBNull(DR_USD[I]["FKM_MTRTDATE"].ToString()) ){
    //            '    Dim DTE As string = DR_USD[I]["FKM_MTRTDATE").ToString.Substring(3, 2) & "/" & DR_USD[I]["FKM_MTRTDATE").ToString.Substring(0, 2) & "/" & DR_USD[I]["FKM_MTRTDATE").ToString.Substring(6, 4)
    //            '    DR_USD[I]["FKM_MTRTDATE") = CDate(DTE)
    //            'End if


    //            if IsDBNull(DR_USD[I]["FKM_PROJNAME"].ToString()) ){

    //                if IsDBNull(DR_USD[I]["FKM_INVGRP"].ToString()) ){
    //                    DR_USD[I]["FKM_PROJNAME") = DR_USD[I]["FKM_INCGEN") & "  " & CURR & " (" & CURRSYMB & ") "
    //                Else
    //                    DR_USD[I]["FKM_PROJNAME") = "  TOTAL     " & DR_USD[I]["FKM_INVGRP")
    //               }

    //                if IsDBNull(DR_USD[I]["FKM_INCGEN"].ToString()) ){
    //                    DR_USD[I]["FKM_PROJNAME") = CURR & " (" & CURRSYMB & ") "
    //               }
    //            Else

    //                if Not IsDBNull(DR_USD[I]["COMP_CD"].ToString()) ){
    //                    'if DR_USD[I]["COMP_CD") = "001" ){
    //                    '    DR_USD[I]["FKM_REG") = "FKMINVEST"
    //                    'Elseif DR_USD[I]["COMP_CD") = "002" ){
    //                    '    DR_USD[I]["FKM_REG") = "FENKINVEST"
    //                    'Elseif DR_USD[I]["COMP_CD") = "003" ){
    //                    '    DR_USD[I]["FKM_REG") = "PERSONAL"
    //                    'End if
    //                    DR_USD[I]["FKM_REG") = (string)dbo.GET_FRM_COMP_CD(DR_USD[I]["COMP_CD"), 1)
    //                    DR_USD[I]["LOGO") = dbo.ReadFile(Server.MapPath((string)dbo.GET_FRM_COMP_CD(DR_USD[I]["COMP_CD"), 2)))
    //               }


    //           }

    //            DR_USD[I]["FKM_CURR_SMBL") = CURRSYMB
    //            DR_USD[I]["FOOTER") = User.Identity.Name.ToUpper

    //            dt_NAVRPT_GRID.ImportRow(DR_USD(I))
    //        Next


    //   }


    //    Session("HOME_dt_NAVRPT_info") = dt_NAVRPT_GRID

    //End Sub

    //Protected Sub GridView2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowDataBound
    //    if e.Row.RowType = DataControlRowType.DataRow ){
    //        'if IsDBNull(e.Row.DataItem("FKM_SRL"].ToString()) And IsDBNull(e.Row.DataItem("FKM_INCGEN"].ToString()) And IsDBNull(e.Row.DataItem("FKM_INVGRP"].ToString()) ){
    //        '    e.Row.BackColor = System.Drawing.Color.Navy
    //        '    e.Row.ForeColor = System.Drawing.Color.White
    //        'Elseif IsDBNull(e.Row.DataItem("FKM_SRL"].ToString()) And IsDBNull(e.Row.DataItem("FKM_INVGRP"].ToString()) ){
    //        '    e.Row.BackColor = System.Drawing.Color.Indigo
    //        '    e.Row.ForeColor = System.Drawing.Color.White
    //        'Else

    //        if IsDBNull(e.Row.DataItem("FKM_SRL"].ToString()) ){
    //            e.Row.BackColor = System.Drawing.Color.DarkGray
    //            e.Row.ForeColor = System.Drawing.Color.Black
    //            e.Row.Font.Bold = True

    //        Else
    //            if e.Row.DataItem("FKM_REG") = "FENKINVEST" ){
    //                e.Row.ForeColor = System.Drawing.Color.Crimson
    //            Elseif e.Row.DataItem("FKM_REG") = "PERSONAL" ){
    //                e.Row.ForeColor = System.Drawing.Color.DarkViolet
    //            Elseif e.Row.DataItem("FKM_REG") = "TECHSQUARE" ){
    //                e.Row.ForeColor = System.Drawing.Color.DarkGray
    //           }
    //       }
    //        'Elseif e.Row.RowType = DataControlRowType.Header ){
    //        '    e.Row.BackColor = System.Drawing.Color.DodgerBlue
    //        '    e.Row.ForeColor = System.Drawing.Color.White
    //   }

    //End Sub
    //Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
    //    Dim rpt As New ReportDocument
    //    Dim rname As string = Server.MapPath("~/Reports/NAVSUMMRPT1.rpt")
    //    Dim dwnldfname As string = Server.MapPath("~/ReportsGenerate/NAVRPT.xls")
    //    Dim dt As DataTable = TryCast(Session("HOME_dt_NAVRPT_REPORT"), DataTable)

    //    if dt.Rows.Count > 0 ){
    //        rpt.Load(rname)
    //        rpt.SetDataSource(dt)

    //        'rpt.PrintOptions.PaperSize = PaperSize.PaperA4
    //        'rpt.PrintOptions.PaperOrientation = PaperOrientation.Landscape
    //        rpt.ExportToDisk(ExportFormatType.Excel, dwnldfname)
    //        rpt.Close()
    //        rpt.Dispose()


    //        Response.ClearContent()
    //        Response.ClearHeaders()
    //        Response.AddHeader("content-disposition", "attachment;filename =" & "NAV_REPORT.xls")
    //        Response.ContentType = "application/ms-excel"
    //        Response.TransmitFile(dwnldfname)

    //   }
    //End Sub



    //Protected Sub REFINE_QRY_LQD(ByRef CODE As string, ByRef CURR As string, ByRef YYYY As string)


    //    Session("HOME_dt_LQDLIST_REPORT") = Nothing
    //    GridView3.DataSource = Nothing
    //    GridView3.DataBind()

    //    Dim COMP_CD As string = Session("HOME_COMP_CD")
    //    Dim RETRVQRY As string = "SELECT * FROM LQD_INFO WHERE FKM_SRL LIKE '" & CODE & "' AND FKM_CURR LIKE '" & CURR &
    //        "' AND COMP_CD LIKE '" & COMP_CD & "' AND SUBstring(FKM_LQDDATE, 7, 4) LIKE '" & YYYY & "' ORDER BY FKM_SRL   "

    //    Dim DTINVINFO As DataTable = dbo.SelTable(RETRVQRY)

    //    if DTINVINFO.Rows.Count > 0 ){

    //        DTINVINFO.Columns.Add("FKM_ROI_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_PRINCIPAL_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_EXPENSE_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_TOTCOST_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_INTEREST_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_DVDND_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_CAPGN_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_LQDAMT_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_AMOUNT_R", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_CURR_SMBL", typeof(string))
    //        DTINVINFO.Columns.Add("FOOTER", typeof(string))
    //        DTINVINFO.Columns.Add("FKM_REG", typeof(string))
    //        DTINVINFO.Columns.Add("LOGO", typeof(Byte()))



    //        Dim dt_LQDLIST_info As DataTable = DTINVINFO.Clone
    //        dt_LQDLIST_info.Rows.Clear()

    //        Call REFINE_ROW_LQD_DETAILS(DTINVINFO, dt_LQDLIST_info, "US DOLLAR", "$")
    //        Call REFINE_ROW_LQD_DETAILS(DTINVINFO, dt_LQDLIST_info, "UK POUND", "£")
    //        Call REFINE_ROW_LQD_DETAILS(DTINVINFO, dt_LQDLIST_info, "EURO", "€")


    //        Session("HOME_dt_LQDLIST_REPORT") = Nothing
    //        if dt_LQDLIST_info.Rows.Count > 0 ){
    //            Session("HOME_dt_LQDLIST_REPORT") = dt_LQDLIST_info
    //            Button3.Visible = True
    //            GridView3.DataSource = dt_LQDLIST_info
    //            GridView3.DataBind()

    //       }
    //   }


    //End Sub
    //Protected Sub REFINE_ROW_LQD_DETAILS(ByRef DTINVINFO As DataTable, ByRef dt_invest_info As DataTable, ByVal CURR As string, ByVal CURRSYMB As string)

    //    Dim DR_USD As DataRow() = DTINVINFO.Select("FKM_CURR  = '" & CURR & "' ")
    //    Dim DR_TOT_USD As DataRow = dt_invest_info.NewRow
    //    if DR_USD.Length > 0 ){


    //        DR_TOT_USD["FKM_CURR") = CURR
    //        DR_TOT_USD["FKM_PROJNAME") = "TOTAL PROJECTS: " & DR_USD.Length
    //        DR_TOT_USD["FKM_LQDDATE") = "(" & CURR & ")"

    //        DR_TOT_USD["FKM_ROI") =decimal.Parse("0")
    //        DR_TOT_USD["FKM_PRINCIPAL") =decimal.Parse("0")
    //        DR_TOT_USD["FKM_EXPENSE") =decimal.Parse("0")
    //        DR_TOT_USD["FKM_TOTCOST") =decimal.Parse("0")
    //        DR_TOT_USD["FKM_INTEREST") =decimal.Parse("0")
    //        DR_TOT_USD["FKM_DVDND") =decimal.Parse("0")
    //        DR_TOT_USD["FKM_CAPGN") =decimal.Parse("0")
    //        DR_TOT_USD["FKM_LQDAMT") =decimal.Parse("0")
    //        DR_TOT_USD["FKM_AMOUNT") =decimal.Parse("0")

    //        For I = 0 To DR_USD.Length - 1

    //            DR_TOT_USD["COMP_CD") = DR_USD[I]["COMP_CD")
    //            DR_TOT_USD["FKM_PRINCIPAL") = decimal.Parse(DR_TOT_USD["FKM_PRINCIPAL"].ToString()) + decimal.Parse(DR_USD[I]["FKM_PRINCIPAL"].ToString())
    //            DR_TOT_USD["FKM_EXPENSE") = decimal.Parse(DR_TOT_USD["FKM_EXPENSE"].ToString()) + decimal.Parse(DR_USD[I]["FKM_EXPENSE"].ToString())
    //            DR_TOT_USD["FKM_TOTCOST") = decimal.Parse(DR_TOT_USD["FKM_TOTCOST"].ToString()) + decimal.Parse(DR_USD[I]["FKM_TOTCOST"].ToString())
    //            DR_TOT_USD["FKM_INTEREST") = decimal.Parse(DR_TOT_USD["FKM_INTEREST"].ToString()) + decimal.Parse(DR_USD[I]["FKM_INTEREST"].ToString())
    //            DR_TOT_USD["FKM_DVDND") = decimal.Parse(DR_TOT_USD["FKM_DVDND"].ToString()) + decimal.Parse(DR_USD[I]["FKM_DVDND"].ToString())
    //            DR_TOT_USD["FKM_CAPGN") = decimal.Parse(DR_TOT_USD["FKM_CAPGN"].ToString()) + decimal.Parse(DR_USD[I]["FKM_CAPGN"].ToString())
    //            DR_TOT_USD["FKM_LQDAMT") = decimal.Parse(DR_TOT_USD["FKM_LQDAMT"].ToString()) + decimal.Parse(DR_USD[I]["FKM_LQDAMT"].ToString())
    //            DR_TOT_USD["FKM_AMOUNT") = decimal.Parse(DR_TOT_USD["FKM_AMOUNT"].ToString()) + decimal.Parse(DR_USD[I]["FKM_AMOUNT"].ToString())

    //            if decimal.Parse(DR_USD[I]["FKM_ROI"].ToString()) = 0 ){
    //                DR_USD[I]["FKM_ROI") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_ROI_R") = string.Format("{0:#0.00}", DR_USD[I]["FKM_ROI"].ToString()) & "%"
    //           }

    //            if decimal.Parse(DR_USD[I]["FKM_PRINCIPAL"].ToString()) = 0 ){
    //                DR_USD[I]["FKM_PRINCIPAL") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_PRINCIPAL_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_PRINCIPAL"].ToString())
    //           }

    //            if decimal.Parse(DR_USD[I]["FKM_EXPENSE"].ToString()) = 0 ){
    //                DR_USD[I]["FKM_EXPENSE") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_EXPENSE_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_EXPENSE"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_TOTCOST"].ToString()) = 0 ){
    //                DR_USD[I]["FKM_TOTCOST") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_TOTCOST_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_TOTCOST"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_INTEREST"].ToString()) = 0 ){
    //                DR_USD[I]["FKM_INTEREST") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_INTEREST_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_INTEREST"].ToString())
    //           }

    //            if decimal.Parse(DR_USD[I]["FKM_DVDND"].ToString()) = 0 ){
    //                DR_USD[I]["FKM_DVDND") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_DVDND_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_DVDND"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_CAPGN"].ToString()) = 0 ){
    //                DR_USD[I]["FKM_CAPGN") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_CAPGN_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_CAPGN"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_LQDAMT"].ToString()) = 0 ){
    //                DR_USD[I]["FKM_LQDAMT") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_LQDAMT_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_LQDAMT"].ToString())
    //           }
    //            if decimal.Parse(DR_USD[I]["FKM_AMOUNT"].ToString()) = 0 ){
    //                DR_USD[I]["FKM_AMOUNT") = DBNull.Value
    //            Else
    //                DR_USD[I]["FKM_AMOUNT_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_USD[I]["FKM_AMOUNT"].ToString())
    //           }

    //            'if DR_USD[I]["FKM_PRVTEQT").ToString.Trim = "Y" ){
    //            '    DR_USD[I]["FKM_PRVTEQT") = "PRIVATE EQUITY"
    //            'Elseif DR_USD[I]["FKM_PRVTEQT").ToString.Trim = "N" ){
    //            '    DR_USD[I]["FKM_PRVTEQT") = "NON PRIVATE EQUITY"
    //            'End if

    //            'if DR_USD[I]["FKM_YEILDPRD").ToString.Trim = "M" ){
    //            '    DR_USD[I]["FKM_YEILDPRD") = "MONTHLY"
    //            'Elseif DR_USD[I]["FKM_YEILDPRD").ToString.Trim = "Q" ){
    //            '    DR_USD[I]["FKM_YEILDPRD") = "QUARTERLY"
    //            'Elseif DR_USD[I]["FKM_YEILDPRD").ToString.Trim = "S" ){
    //            '    DR_USD[I]["FKM_YEILDPRD") = "SEMI ANNUAL"
    //            'End if


    //            if Not IsDBNull(DR_USD[I]["COMP_CD"].ToString()) ){
    //                'if DR_USD[I]["COMP_CD") = "001" ){
    //                '    DR_USD[I]["FKM_REG") = "FKMINVEST"
    //                'Elseif DR_USD[I]["COMP_CD") = "002" ){
    //                '    DR_USD[I]["FKM_REG") = "FENKINVEST"
    //                'Elseif DR_USD[I]["COMP_CD") = "003" ){
    //                '    DR_USD[I]["FKM_REG") = "PERSONAL"
    //                'End if
    //                DR_USD[I]["FKM_REG") = (string)dbo.GET_FRM_COMP_CD(DR_USD[I]["COMP_CD"), 1)
    //                DR_USD[I]["LOGO") = dbo.ReadFile(Server.MapPath((string)dbo.GET_FRM_COMP_CD(DR_USD[I]["COMP_CD"), 2)))
    //           }


    //            DR_USD[I]["FOOTER") = User.Identity.Name.ToUpper

    //            dt_invest_info.ImportRow(DR_USD(I))
    //        Next

    //        if decimal.Parse(DR_TOT_USD["FKM_ROI"].ToString()) = 0 ){
    //            DR_TOT_USD["FKM_ROI") = DBNull.Value
    //        Else
    //            DR_TOT_USD["FKM_ROI_R") = string.Format("{0:#0.00}", DR_TOT_USD["FKM_ROI"].ToString()) & "%"
    //       }

    //        if decimal.Parse(DR_TOT_USD["FKM_PRINCIPAL"].ToString()) = 0 ){
    //            DR_TOT_USD["FKM_PRINCIPAL") = DBNull.Value
    //        Else
    //            DR_TOT_USD["FKM_PRINCIPAL_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_TOT_USD["FKM_PRINCIPAL"].ToString())
    //       }

    //        if decimal.Parse(DR_TOT_USD["FKM_EXPENSE"].ToString()) = 0 ){
    //            DR_TOT_USD["FKM_EXPENSE") = DBNull.Value
    //        Else
    //            DR_TOT_USD["FKM_EXPENSE_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_TOT_USD["FKM_EXPENSE"].ToString())
    //       }
    //        if decimal.Parse(DR_TOT_USD["FKM_TOTCOST"].ToString()) = 0 ){
    //            DR_TOT_USD["FKM_TOTCOST") = DBNull.Value
    //        Else
    //            DR_TOT_USD["FKM_TOTCOST_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_TOT_USD["FKM_TOTCOST"].ToString())
    //       }
    //        if decimal.Parse(DR_TOT_USD["FKM_INTEREST"].ToString()) = 0 ){
    //            DR_TOT_USD["FKM_INTEREST") = DBNull.Value
    //        Else
    //            DR_TOT_USD["FKM_INTEREST_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_TOT_USD["FKM_INTEREST"].ToString())
    //       }

    //        if decimal.Parse(DR_TOT_USD["FKM_DVDND"].ToString()) = 0 ){
    //            DR_TOT_USD["FKM_DVDND") = DBNull.Value
    //        Else
    //            DR_TOT_USD["FKM_DVDND_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_TOT_USD["FKM_DVDND"].ToString())
    //       }
    //        if decimal.Parse(DR_TOT_USD["FKM_CAPGN"].ToString()) = 0 ){
    //            DR_TOT_USD["FKM_CAPGN") = DBNull.Value
    //        Else
    //            DR_TOT_USD["FKM_CAPGN_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_TOT_USD["FKM_CAPGN"].ToString())
    //       }
    //        if decimal.Parse(DR_TOT_USD["FKM_LQDAMT"].ToString()) = 0 ){
    //            DR_TOT_USD["FKM_LQDAMT") = DBNull.Value
    //        Else
    //            DR_TOT_USD["FKM_LQDAMT_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_TOT_USD["FKM_LQDAMT"].ToString())
    //       }
    //        if decimal.Parse(DR_TOT_USD["FKM_AMOUNT"].ToString()) = 0 ){
    //            DR_TOT_USD["FKM_AMOUNT") = DBNull.Value
    //        Else
    //            DR_TOT_USD["FKM_AMOUNT_R") = CURRSYMB + " " +  string.Format("{0:#,##0}", DR_TOT_USD["FKM_AMOUNT"].ToString())
    //       }

    //        DR_TOT_USD["FOOTER") = User.Identity.Name.ToUpper

    //        dt_invest_info.Rows.Add(DR_TOT_USD)
    //   }

    //End Sub
    //Protected Sub GridView3_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView3.RowDataBound
    //    if e.Row.RowType = DataControlRowType.DataRow ){
    //        'if IsDBNull(e.Row.DataItem("FKM_SRL"].ToString()) And IsDBNull(e.Row.DataItem("FKM_INCGEN"].ToString()) And IsDBNull(e.Row.DataItem("FKM_INVGRP"].ToString()) ){
    //        '    e.Row.BackColor = System.Drawing.Color.Navy
    //        '    e.Row.ForeColor = System.Drawing.Color.White
    //        'Elseif IsDBNull(e.Row.DataItem("FKM_SRL"].ToString()) And IsDBNull(e.Row.DataItem("FKM_INVGRP"].ToString()) ){
    //        '    e.Row.BackColor = System.Drawing.Color.Indigo
    //        '    e.Row.ForeColor = System.Drawing.Color.White
    //        'Else

    //        if IsDBNull(e.Row.DataItem("FKM_SRL"].ToString()) ){
    //            e.Row.BackColor = System.Drawing.Color.DarkGray
    //            e.Row.ForeColor = System.Drawing.Color.Black
    //            e.Row.Font.Bold = True

    //        Else
    //            if e.Row.DataItem("FKM_REG") = "FENKINVEST" ){
    //                e.Row.ForeColor = System.Drawing.Color.Crimson
    //            Elseif e.Row.DataItem("FKM_REG") = "PERSONAL" ){
    //                e.Row.ForeColor = System.Drawing.Color.DarkViolet
    //            Elseif e.Row.DataItem("FKM_REG") = "TECHSQUARE" ){
    //                e.Row.ForeColor = System.Drawing.Color.DarkGray
    //           }
    //       }
    //        'Elseif e.Row.RowType = DataControlRowType.Header ){
    //        '    e.Row.BackColor = System.Drawing.Color.DodgerBlue
    //        '    e.Row.ForeColor = System.Drawing.Color.White
    //   }

    //End Sub
    //Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
    //    Dim rpt As New ReportDocument
    //    Dim rname As string = Server.MapPath("~/Reports/LQDATED_GENRPT.rpt")
    //    Dim dwnldfname As string = Server.MapPath("~/ReportsGenerate/LQDLIST_REPORT.xls")
    //    Dim dt As DataTable = TryCast(Session("HOME_dt_LQDLIST_REPORT"), DataTable)

    //    if dt.Rows.Count > 0 ){
    //        rpt.Load(rname)
    //        rpt.SetDataSource(dt)

    //        'rpt.PrintOptions.PaperSize = PaperSize.PaperA4
    //        'rpt.PrintOptions.PaperOrientation = PaperOrientation.Landscape
    //        rpt.ExportToDisk(ExportFormatType.Excel, dwnldfname)
    //        rpt.Close()
    //        rpt.Dispose()


    //        Response.ClearContent()
    //        Response.ClearHeaders()
    //        Response.AddHeader("content-disposition", "attachment;filename =" & "LQDLIST_REPORT.xls")
    //        Response.ContentType = "application/ms-excel"
    //        Response.TransmitFile(dwnldfname)

    //   }
    //End Sub


}
