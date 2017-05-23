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
public partial class SUMMRPT_NAVRPT : Page
{
    fkminvcom dbo = new fkminvcom();
    // Visual C# .NET requires that you override the OnInit function,
    // adding a new delegate for the Page_Load event.

    override protected void OnInit(System.EventArgs e)
    {
        this.Load += new System.EventHandler(this.Page_Load);
        base.OnInit(e);
        CMB_NAV_QTR.SelectedIndexChanged += new System.EventHandler(CMB_NAV_QTR_SelectedIndexChanged);
        GridView1.RowDataBound += new System.Web.UI.WebControls.GridViewRowEventHandler(GridView1_RowDataBound);
        GridView2.RowDataBound += new System.Web.UI.WebControls.GridViewRowEventHandler(GridView2_RowDataBound);
        GridView3.RowDataBound += new System.Web.UI.WebControls.GridViewRowEventHandler(GridView3_RowDataBound);
        Tab1.Click += new System.EventHandler(Tab1_Click);
        Tab2.Click += new System.EventHandler(Tab2_Click);
        Tab3.Click += new System.EventHandler(Tab3_Click);
        Button1.Click += new System.EventHandler(Button1_Click);
        Button2.Click += new System.EventHandler(Button2_Click);
        Button3.Click += new System.EventHandler(Button3_Click);
    }

    public void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {

            if ((Session["USER_LOGGED"]) == null)
            {
                Session.Abandon();
                Response.Redirect("~/Account/Login.aspx");
            }
            INIT_QUARTER();
            Session["NAVRPT_dt_NAVRPT_info"] = null;
            Session["SUMMRPT2_dt_SUMMRPT2_info"] = null;
            REFINE_QRY();
            REFINE_QRY_TO_GRID();
            Tab1_Click(sender, e);
        }

    }

    public void INIT_QUARTER()
    {
        DataTable DT = new DataTable();

        DT.Columns.Add("QUARTER");
        DT.Columns.Add("CD");

        //if (System.DateTime.Today > DateTime.Parse("30/09/" + System.DateTime.Today.Year))
        //{
        //    DT.Rows.Add("DECEMBER - " + (System.DateTime.Today.Year), (System.DateTime.Today.Year) + "12");
        //}

        //if (System.DateTime.Today > DateTime.Parse("30/06/" + System.DateTime.Today.Year))
        //{
        //    DT.Rows.Add("SEPTEMBER - " + (System.DateTime.Today.Year), (System.DateTime.Today.Year) + "09");
        //}

        //if (System.DateTime.Today > DateTime.Parse("31/03/" + System.DateTime.Today.Year))
        //{
        //    DT.Rows.Add("JUNE - " + (System.DateTime.Today.Year), (System.DateTime.Today.Year) + "06");
        //}

        //if (System.DateTime.Today >= DateTime.Parse("01/01/" + System.DateTime.Today.Year))
        //{
        //    DT.Rows.Add("MARCH - " + (System.DateTime.Today.Year), (System.DateTime.Today.Year) + "03");
        //}

        //'Dim COMP_CD As String = Session("USER_COMP_CD") WHERE FKM_ISSDATE NOT LIKE '" + System.DateTime.Today.Year + "%' 
        string RETRVQRY = "SELECT DISTINCT  FKM_ISSDATE  FROM NAV_INFO    ORDER BY FKM_ISSDATE DESC";
        DataTable dtinfo = dbo.SelTable(RETRVQRY);

        // ' Dim YEAR As Integer = 2015
        foreach (DataRow DR in dtinfo.Rows)// ' (Today.Year - YEAR)
        {
            string YR = DR["FKM_ISSDATE"].ToString().Substring(0, 4);
            string MNT = DR["FKM_ISSDATE"].ToString().Substring(4, 2);
            switch (MNT)
            {
                case "03": DT.Rows.Add("MARCH - " + YR, YR + "03"); break;
                case "06": DT.Rows.Add("JUNE - " + YR, YR + "06"); break;
                case "09": DT.Rows.Add("SEPTEMBER - " + YR, YR + "09"); break;
                case "12": DT.Rows.Add("DECEMBER - " + YR, YR + "12"); break;
            }

        }

        DT.Rows.Add("ALL QUARTER", "%");
        CMB_NAV_QTR.DataSource = DT;
        CMB_NAV_QTR.DataBind();
        CMB_NAV_QTR.SelectedIndex = CMB_NAV_QTR.Items.Count - 1;
    }
    protected void CMB_NAV_QTR_SelectedIndexChanged(object sender, System.EventArgs e) // Handles CMB_NAV_QTR.SelectedIndexChanged
    {
        if (CMB_NAV_QTR.SelectedIndex > -1)
        {
            Session["NAVRPT_dt_NAVRPT_info"] = null;
            Session["NAVRPT_dt_NAVRPT_REPORT"] = null;
            REFINE_QRY();
            REFINE_QRY_TO_GRID();
            Tab1_Click(sender, e);
        }
    }

    public void REFINE_QRY()
    {

        // String CODE = CMB_FKM_CD.SelectedValue;
        String QUARTERCD = CMB_NAV_QTR.Text.Trim();
        Session["NAVLIST_dt_NAVLIST_REPORT"] = null;
        GridView1.DataSource = null;
        GridView1.DataBind();

        // 'Dim COMP_CD As String = Session("USER_COMP_CD")
        String RETRVQRY = "SELECT * FROM NAV_INFO WHERE FKM_STATUS = 'Y'  AND FKM_ISSDATE LIKE '" + QUARTERCD + "'  ";// ORDER BY FKM_ISSDATE DESC  ";
        //Dim DTINVINFO As DataTable = dbo.SelTable(RETRVQRY)
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
            DTINVINFO.Columns.Add("FOOTER", typeof(string));
            DTINVINFO.Columns.Add("FKM_REG", typeof(string));
            DTINVINFO.Columns.Add("LOGO", typeof(string));

            DataTable dt_invest_info = DTINVINFO.Clone();
            dt_invest_info.Rows.Clear();
            REFINE_ROW_DETAILS(ref DTINVINFO, ref dt_invest_info, "US DOLLAR", "$");
            REFINE_ROW_DETAILS(ref DTINVINFO, ref  dt_invest_info, "UK POUND", "£");
            REFINE_ROW_DETAILS(ref DTINVINFO, ref dt_invest_info, "EURO", "€");

            Session["NAVRPT_dt_NAVRPT_REPORT"] = null;
            if (dt_invest_info.Rows.Count > 0)
            {
                Session["NAVRPT_dt_NAVRPT_REPORT"] = dt_invest_info;
                //GridView1.DataSource = dt_invest_info;
                //GridView1.DataBind();
            }
        }
    }
    protected void REFINE_ROW_DETAILS(ref DataTable DTINVINFO, ref DataTable dt_NAVRPT_info, string CURR, string CURRSYMB)
    {
        Session.LCID = 2057;

        DataRow[] DR_USD = DTINVINFO.Select("FKM_CURR  = '" + CURR + "' ");

        if (DR_USD.Length > 0)
        {

            for (int I = 0; I <= DR_USD.Length - 1; I++)
            {



                if ((DR_USD[I]["FKM_COMMCAP"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_COMMCAP"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_COMMCAP"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_COMMCAP_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_COMMCAP"]);
                }

                if ((DR_USD[I]["FKM_COMMCAP2"].ToString()) == "" || (Convert.ToDecimal(DR_USD[I]["FKM_COMMCAP2"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_COMMCAP2"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_COMMCAP2_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_COMMCAP2"]);
                }
                if ((DR_USD[I]["FKM_INVAMT"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_INVAMT"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_INVAMT"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_INVAMT_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_INVAMT"]);
                }
                if ((DR_USD[I]["FKM_CAPPD"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_CAPPD"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_CAPPD"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_CAPPD_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_CAPPD"]);
                }
                if ((DR_USD[I]["FKM_CAPUNPD"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_CAPUNPD"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_CAPUNPD"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_CAPUNPD_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_CAPUNPD"]);
                }
                if ((DR_USD[I]["FKM_EXPNS"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_EXPNS"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_EXPNS"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_EXPNS_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_EXPNS"]);
                }
                if ((DR_USD[I]["FKM_ROI"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_ROI"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_ROI"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_ROI_R"] = string.Format("{0:#0.00}", DR_USD[I]["FKM_ROI"].ToString()) + "%";
                }
                if ((DR_USD[I]["FKM_BOOKVAL"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_BOOKVAL"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_BOOKVAL"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_BOOKVAL_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_BOOKVAL"]);
                }
                if ((DR_USD[I]["FKM_MONYCAL"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_MONYCAL"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_MONYCAL"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_MONYCAL_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_MONYCAL"]);
                }
                if ((DR_USD[I]["FKM_QRTYCAL"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_QRTYCAL"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_QRTYCAL"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_QRTYCAL_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_QRTYCAL"]);
                }
                if ((DR_USD[I]["FKM_SMANYCAL"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_SMANYCAL"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_SMANYCAL"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_SMANYCAL_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_SMANYCAL"]);
                }
                if ((DR_USD[I]["FKM_ANLYCAL"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_ANLYCAL"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_ANLYCAL"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_ANLYCAL_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_ANLYCAL"]);
                }
                if ((DR_USD[I]["FKM_SCNINCP"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_SCNINCP"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_SCNINCP"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_SCNINCP_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_SCNINCP"]);
                }
                if ((DR_USD[I]["FKM_ANLINCMCY"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_ANLINCMCY"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_ANLINCMCY"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_ANLINCMCY_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_ANLINCMCY"]);
                }
                if ((DR_USD[I]["FKM_ANLRLZD"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_ANLRLZD"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_ANLRLZD"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_ANLRLZD_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_ANLRLZD"]);
                }
                if ((DR_USD[I]["FKM_ACTLINCMRU"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_ACTLINCMRU"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_ACTLINCMRU"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_ACTLINCMRU_R"] = string.Format("{0:#0.00}", DR_USD[I]["FKM_ACTLINCMRU"].ToString()) + "%";
                }
                if ((DR_USD[I]["FKM_UNRLYLD"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_UNRLYLD"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_UNRLYLD"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_UNRLYLD_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_UNRLYLD"]);
                }
                if ((DR_USD[I]["FKM_DVDND"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_DVDND"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_DVDND"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_DVDND_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_DVDND"]);
                }
                if ((DR_USD[I]["FKM_VALVTN"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_VALVTN"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_VALVTN"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_VALVTN_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_VALVTN"]);
                }
                if ((DR_USD[I]["FKM_CAPGN"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_CAPGN"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_CAPGN"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_CAPGN_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_CAPGN"]);
                }
                if ((DR_USD[I]["FKM_UNRLDVD"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_UNRLDVD"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_UNRLDVD"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_UNRLDVD_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_UNRLDVD"]);
                }
                if ((DR_USD[I]["FKM_UNRLDVDCAP"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_UNRLDVDCAP"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_UNRLDVDCAP"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_UNRLDVDCAP_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_UNRLDVDCAP"]);
                }
                if ((DR_USD[I]["FKM_FAIRVAL"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_FAIRVAL"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_FAIRVAL"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_FAIRVAL_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_FAIRVAL"]);
                }
                if ((DR_USD[I]["FKM_NAV"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_NAV"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_NAV"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_NAV_R"] = string.Format("{0:#0.00}", DR_USD[I]["FKM_NAV"]);
                }
                if ((DR_USD[I]["FKM_SALEPRCD"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_SALEPRCD"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_SALEPRCD"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_SALEPRCD_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_SALEPRCD"]);
                }
                 

                if (DR_USD[I]["FKM_INCGEN"].ToString().Trim() == "Y")
                {
                    DR_USD[I]["FKM_INCGEN"] = "INCOME GENERATING ";
                }
                else if (DR_USD[I]["FKM_INCGEN"].ToString().Trim() == "N")
                {
                    DR_USD[I]["FKM_INCGEN"] = "NON INCOME GENERATING ";
                }

                //if (DR_USD[I]["FKM_PRVTEQT"].ToString().Trim() == "Y")
                //{
                //    DR_USD[I]["FKM_PRVTEQT"] = "PRIVATE EQUITY";
                //}
                //else if (DR_USD[I]["FKM_PRVTEQT"].ToString().Trim() == "N")
                //{
                //    DR_USD[I]["FKM_PRVTEQT"] = "NON PRIVATE EQUITY";
                //}

                //if (DR_USD[I]["FKM_YEILDPRD"].ToString().Trim() == "M")
                //{
                //    DR_USD[I]["FKM_YEILDPRD"] = "MONTHLY";
                //}
                //else if (DR_USD[I]["FKM_YEILDPRD"].ToString().Trim() == "Q")
                //{
                //    DR_USD[I]["FKM_YEILDPRD"] = "QUARTERLY";
                //}
                //else if (DR_USD[I]["FKM_YEILDPRD"].ToString().Trim() == "S")
                //{
                //    DR_USD[I]["FKM_YEILDPRD"] = "SEMI ANNUAL";
                //}

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



                //DR_USD[I]["FKM_REG"] = (string)dbo.GET_FRM_COMP_CD(DR_USD[I]["COMP_CD"].ToString(), 1);
                //DR_USD[I]["LOGO"] = dbo.ReadFile(Server.MapPath((string)dbo.GET_FRM_COMP_CD(DR_USD[I]["COMP_CD"].ToString(), 2)));

                //DR_USD[I]["FOOTER"] = User.Identity.Name.ToUpper();


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
                    else
                    {
                        if ((DR_USD[I]["COMP_CD"].ToString()) != "")
                        {
                            DR_USD[I]["FKM_REG"] = (string)dbo.GET_FRM_COMP_CD(DR_USD[I]["COMP_CD"].ToString(), 1);
                            DR_USD[I]["LOGO"] = dbo.ReadFile(Server.MapPath((string)dbo.GET_FRM_COMP_CD(DR_USD[I]["COMP_CD"].ToString(), 2)));
                        }
                    }
                    DR_USD[I]["FKM_CURR_SMBL"] = CURRSYMB;
                    DR_USD[I]["FOOTER"] = User.Identity.Name.ToUpper();

                }


                dt_NAVRPT_info.ImportRow(DR_USD[I]);
            }


        }
    }
    protected void REFINE_QRY_TO_GRID_MAIN(string QUARTERCD, string CURR, string CURRSYMB, string INCGEN, string INVGRP)
    {
        //' Dim COMP_CD As string = Session("USER_COMP_CD")
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
                                 "' AND FKM_STATUS = 'Y'  AND  FKM_ISSDATE LIKE '" + QUARTERCD + "' " +
                                         " ORDER BY FKM_CURR DESC, FKM_INCGEN DESC ,FKM_INVGRP,FKM_SRL ";

        DataTable DTINVINFO = dbo.SelTable(RETRVQRY);

        if (DTINVINFO.Rows.Count > 0)
        {

            //'DTINVINFO.Columns.Add("FKM_SRL", typeof(string))
            //'DTINVINFO.Columns.Add("FKM_PROJNAME", typeof(string))
            //'' DTINVINFO.Columns.Add("FKM_INVGRP", typeof(string))
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

            REFINE_ROW_GRIDS(ref DTINVINFO, ref dt_NAVRPT_info, CURR, CURRSYMB, "FKM_CURR", CURR);

        }


    }
    protected void REFINE_QRY_TO_GRID_INVGRP(string QUARTERCD, string CURR, string CURRSYMB, string INCGEN)
    {
        //'Dim COMP_CD As string = Session("USER_COMP_CD")
        string RETRVQRY = "SELECT FKM_CURR, FKM_INCGEN,FKM_INVGRP, " +
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
                                  " WHERE FKM_CURR = '" + CURR + "' AND FKM_INCGEN = '" + INCGEN +
                                  "' AND FKM_STATUS = 'Y'  AND  FKM_ISSDATE LIKE '" + QUARTERCD + "' " +
                                  " GROUP BY FKM_CURR , FKM_INCGEN ,FKM_INVGRP " +
                                  " ORDER BY FKM_CURR DESC, FKM_INCGEN DESC ,FKM_INVGRP ";


        DataTable DTINVINFO = dbo.SelTable(RETRVQRY);

        if (DTINVINFO.Rows.Count > 0)
        {

            DTINVINFO.Columns.Add("COMP_CD", typeof(string));
            DTINVINFO.Columns.Add("FKM_SRL", typeof(string));
            DTINVINFO.Columns.Add("FKM_PROJNAME", typeof(string));
            //' DTINVINFO.Columns.Add("FKM_INVGRP", typeof(string))
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
                REFINE_ROW_GRIDS(ref DTINVINFO, ref dt_NAVRPT_info, CURR, CURRSYMB, "FKM_INVGRP", DR["FKM_INVGRP"].ToString());
            }
        }
    }
    protected void REFINE_QRY_TO_GRID_INCGEN(string QUARTERCD, string CURR, string CURRSYMB)
    {
        string RETRVQRY = "SELECT FKM_CURR, FKM_INCGEN, " +
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
                                   " WHERE FKM_CURR = '" + CURR + "' AND FKM_STATUS = 'Y'  AND  FKM_ISSDATE LIKE '" + QUARTERCD + "' " +
                                   " GROUP BY FKM_CURR , FKM_INCGEN  " +
                                   " ORDER BY FKM_CURR DESC, FKM_INCGEN DESC  ";

        DataTable DTINVINFO = dbo.SelTable(RETRVQRY);

        if (DTINVINFO.Rows.Count > 0)
        {

            DTINVINFO.Columns.Add("COMP_CD", typeof(string));
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
            REFINE_ROW_GRIDS(ref DTINVINFO, ref dt_NAVRPT_info, CURR, CURRSYMB, "FKM_INCGEN", "Y");
            REFINE_ROW_GRIDS(ref DTINVINFO, ref dt_NAVRPT_info, CURR, CURRSYMB, "FKM_INCGEN", "Y");
            REFINE_QRY_TO_GRID_INVGRP(QUARTERCD, CURR, CURRSYMB, "N");
            REFINE_ROW_GRIDS(ref DTINVINFO, ref dt_NAVRPT_info, CURR, CURRSYMB, "FKM_INCGEN", "N");
            REFINE_ROW_GRIDS(ref DTINVINFO, ref dt_NAVRPT_info, CURR, CURRSYMB, "FKM_INCGEN", "N");

        }


    }
    protected void REFINE_QRY_TO_GRID()
    {
        GridView1.DataSource = null;
        GridView1.DataBind();

        GridView2.DataSource = null;
        GridView2.DataBind();

        GridView3.DataSource = null;
        GridView3.DataBind();

        //' Dim COMP_CD As string = Session("USER_COMP_CD")
        string QUARTERCD = CMB_NAV_QTR.SelectedValue;
        string RETRVQRY = "SELECT FKM_CURR,  " +
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
                                 " FROM NAV_INFO WHERE FKM_STATUS = 'Y'  AND  FKM_ISSDATE LIKE '" + QUARTERCD + "'   " +
                                 " GROUP BY FKM_CURR   " +
                                 " ORDER BY FKM_CURR DESC  ";

        DataTable DTINVINFO = dbo.SelTable(RETRVQRY);

        if (DTINVINFO.Rows.Count > 0)
        {

            DTINVINFO.Columns.Add("COMP_CD", typeof(string));
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
            REFINE_ROW_GRIDS(ref DTINVINFO, ref dt_NAVRPT_info, "US DOLLAR", "$", "FKM_CURR", "US DOLLAR");
          //  REFINE_ROW_GRIDS(ref DTINVINFO, ref  dt_NAVRPT_info, "US DOLLAR", "$", "FKM_CURR", "US DOLLAR");
            REFINE_QRY_TO_GRID_INCGEN(QUARTERCD, "UK POUND", "£");
            REFINE_ROW_GRIDS(ref DTINVINFO, ref dt_NAVRPT_info, "UK POUND", "£", "FKM_CURR", "UK POUND"); 
            REFINE_QRY_TO_GRID_INCGEN(QUARTERCD, "EURO", "€");
            REFINE_ROW_GRIDS(ref DTINVINFO, ref dt_NAVRPT_info, "EURO", "€", "FKM_CURR", "EURO"); 


            dt_NAVRPT_info = (DataTable)Session["NAVRPT_dt_NAVRPT_info"];
            DataTable dt_SUMMRPT2_info = new DataTable(); //'= Session("SUMMRPT2_dt_SUMMRPT2_info")

            dt_SUMMRPT2_info = dt_NAVRPT_info.Clone();
            dt_SUMMRPT2_info.Rows.Clear();

            if (dt_NAVRPT_info.Rows.Count > 0)
            {
                for (int I = 0; I <= dt_NAVRPT_info.Rows.Count - 1; I++)
                {

                    //  'For Each DR In dt_NAVRPT_info.Rows
                    if ((dt_NAVRPT_info.Rows[I]["FKM_SRL"]) != null)
                    {
                        dt_SUMMRPT2_info.ImportRow(dt_NAVRPT_info.Rows[I]);
                    }
                }

                // ' = dt_NAVRPT_info
                GridView1.DataSource = dt_NAVRPT_info;
                GridView1.DataBind();

                GridView2.DataSource = dt_NAVRPT_info;
                GridView2.DataBind();

                GridView3.DataSource = dt_SUMMRPT2_info;
                GridView3.DataBind();

            }
        }
    }
    protected void REFINE_ROW_GRIDS(ref DataTable DTINVINFO, ref DataTable dt_NAVRPT_info, string CURR, string CURRSYMB, string FLDTYP, string SLCTN)
    {

        Session.LCID = 2057;

        DataTable dt_NAVRPT_GRID = (DataTable)Session["NAVRPT_dt_NAVRPT_info"];

        if (dt_NAVRPT_GRID == null)
        {
            dt_NAVRPT_GRID = dt_NAVRPT_info.Clone();
        }


        DataRow[] DR_USD = DTINVINFO.Select("FKM_CURR  = '" + CURR + "' AND  " + FLDTYP + "  = '" + SLCTN + "'");

        if (DR_USD.Length > 0)
        {

            for (int I = 0; I <= DR_USD.Length - 1; I++)
            {


                if ((DR_USD[I]["FKM_COMMCAP"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_COMMCAP"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_COMMCAP"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_COMMCAP_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_COMMCAP"]);
                }

                if ((DR_USD[I]["FKM_COMMCAP2"].ToString()) == "" || (Convert.ToDecimal(DR_USD[I]["FKM_COMMCAP2"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_COMMCAP2"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_COMMCAP2_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_COMMCAP2"]);
                }
                if ((DR_USD[I]["FKM_INVAMT"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_INVAMT"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_INVAMT"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_INVAMT_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_INVAMT"]);
                }
                if ((DR_USD[I]["FKM_CAPPD"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_CAPPD"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_CAPPD"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_CAPPD_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_CAPPD"]);
                }
                if ((DR_USD[I]["FKM_CAPUNPD"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_CAPUNPD"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_CAPUNPD"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_CAPUNPD_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_CAPUNPD"]);
                }
                if ((DR_USD[I]["FKM_EXPNS"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_EXPNS"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_EXPNS"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_EXPNS_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_EXPNS"]);
                }
                if ((DR_USD[I]["FKM_ROI"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_ROI"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_ROI"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_ROI_R"] = string.Format("{0:#0.00}", DR_USD[I]["FKM_ROI"].ToString()) + "%";
                }
                if ((DR_USD[I]["FKM_BOOKVAL"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_BOOKVAL"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_BOOKVAL"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_BOOKVAL_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_BOOKVAL"]);
                }
                if ((DR_USD[I]["FKM_MONYCAL"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_MONYCAL"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_MONYCAL"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_MONYCAL_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_MONYCAL"]);
                }
                if ((DR_USD[I]["FKM_QRTYCAL"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_QRTYCAL"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_QRTYCAL"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_QRTYCAL_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_QRTYCAL"]);
                }
                if ((DR_USD[I]["FKM_SMANYCAL"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_SMANYCAL"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_SMANYCAL"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_SMANYCAL_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_SMANYCAL"]);
                }
                if ((DR_USD[I]["FKM_ANLYCAL"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_ANLYCAL"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_ANLYCAL"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_ANLYCAL_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_ANLYCAL"]);
                }
                if ((DR_USD[I]["FKM_SCNINCP"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_SCNINCP"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_SCNINCP"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_SCNINCP_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_SCNINCP"]);
                }
                if ((DR_USD[I]["FKM_ANLINCMCY"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_ANLINCMCY"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_ANLINCMCY"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_ANLINCMCY_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_ANLINCMCY"]);
                }
                if ((DR_USD[I]["FKM_ANLRLZD"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_ANLRLZD"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_ANLRLZD"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_ANLRLZD_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_ANLRLZD"]);
                }
                if ((DR_USD[I]["FKM_ACTLINCMRU"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_ACTLINCMRU"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_ACTLINCMRU"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_ACTLINCMRU_R"] = string.Format("{0:#0.00}", DR_USD[I]["FKM_ACTLINCMRU"].ToString()) + "%";
                }
                if ((DR_USD[I]["FKM_UNRLYLD"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_UNRLYLD"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_UNRLYLD"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_UNRLYLD_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_UNRLYLD"]);
                }
                if ((DR_USD[I]["FKM_DVDND"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_DVDND"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_DVDND"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_DVDND_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_DVDND"]);
                }
                if ((DR_USD[I]["FKM_VALVTN"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_VALVTN"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_VALVTN"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_VALVTN_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_VALVTN"]);
                }
                if ((DR_USD[I]["FKM_CAPGN"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_CAPGN"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_CAPGN"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_CAPGN_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_CAPGN"]);
                }
                if ((DR_USD[I]["FKM_UNRLDVD"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_UNRLDVD"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_UNRLDVD"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_UNRLDVD_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_UNRLDVD"]);
                }
                if ((DR_USD[I]["FKM_UNRLDVDCAP"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_UNRLDVDCAP"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_UNRLDVDCAP"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_UNRLDVDCAP_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_UNRLDVDCAP"]);
                }
                if ((DR_USD[I]["FKM_FAIRVAL"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_FAIRVAL"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_FAIRVAL"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_FAIRVAL_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_FAIRVAL"]);
                }
                if ((DR_USD[I]["FKM_NAV"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_NAV"].ToString()) == 0))
                {
                    DR_USD[I]["FKM_NAV"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_NAV_R"] = string.Format("{0:#0.00}", DR_USD[I]["FKM_NAV"]);
                }
                if ((DR_USD[I]["FKM_SALEPRCD"].ToString()) == "" || (decimal.Parse(DR_USD[I]["FKM_SALEPRCD"].ToString()) == 0)) 
                {
                    DR_USD[I]["FKM_SALEPRCD"] = System.DBNull.Value;
                }
                else
                {
                    DR_USD[I]["FKM_SALEPRCD_R"] = CURRSYMB + " " + string.Format("{0:#,##0}", DR_USD[I]["FKM_SALEPRCD"]);
                }

                if (DR_USD[I]["FKM_INCGEN"].ToString().Trim() == "Y")
                {
                    DR_USD[I]["FKM_INCGEN"] = "INCOME GENERATING ";
                }
                else if (DR_USD[I]["FKM_INCGEN"].ToString().Trim() == "N")
                {
                    DR_USD[I]["FKM_INCGEN"] = "NON INCOME GENERATING ";
                }

                //if (DR_USD[I]["FKM_PRVTEQT"].ToString().Trim() == "Y")
                //{
                //    DR_USD[I]["FKM_PRVTEQT"] = "PRIVATE EQUITY";
                //}
                //else if (DR_USD[I]["FKM_PRVTEQT"].ToString().Trim() == "N")
                //{
                //    DR_USD[I]["FKM_PRVTEQT"] = "NON PRIVATE EQUITY";
                //}

                //if (DR_USD[I]["FKM_YEILDPRD"].ToString().Trim() == "M")
                //{
                //    DR_USD[I]["FKM_YEILDPRD"] = "MONTHLY";
                //}
                //else if (DR_USD[I]["FKM_YEILDPRD"].ToString().Trim() == "Q")
                //{
                //    DR_USD[I]["FKM_YEILDPRD"] = "QUARTERLY";
                //}
                //else if (DR_USD[I]["FKM_YEILDPRD"].ToString().Trim() == "S")
                //{
                //    DR_USD[I]["FKM_YEILDPRD"] = "SEMI ANNUAL";
                //}

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



                //DR_USD[I]["FKM_REG"] = (string)dbo.GET_FRM_COMP_CD(DR_USD[I]["COMP_CD"].ToString(), 1);
                //DR_USD[I]["LOGO"] = dbo.ReadFile(Server.MapPath((string)dbo.GET_FRM_COMP_CD(DR_USD[I]["COMP_CD"].ToString(), 2)));

                //DR_USD[I]["FOOTER"] = User.Identity.Name.ToUpper();


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
                    else
                    {
                        if ((DR_USD[I]["COMP_CD"].ToString()) != "")
                        {
                            DR_USD[I]["FKM_REG"] = (string)dbo.GET_FRM_COMP_CD(DR_USD[I]["COMP_CD"].ToString(), 1);
                            DR_USD[I]["LOGO"] = dbo.ReadFile(Server.MapPath((string)dbo.GET_FRM_COMP_CD(DR_USD[I]["COMP_CD"].ToString(), 2)));
                        }
                    }
                    DR_USD[I]["FKM_CURR_SMBL"] = CURRSYMB;
                    DR_USD[I]["FOOTER"] = User.Identity.Name.ToUpper();

                }

                dt_NAVRPT_GRID.ImportRow(DR_USD[I]);

            }


        }

        Session["NAVRPT_dt_NAVRPT_info"] = dt_NAVRPT_GRID;
    }
    public void GridView1_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)// Handles GridView2.RowDataBound
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
                e.Row.ForeColor = (System.Drawing.Color)dbo.GET_FRM_COMP_CD(drval2, 3);
            }
        }
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
                e.Row.ForeColor = (System.Drawing.Color)dbo.GET_FRM_COMP_CD(drval2, 3);
            }
        }
    }
    protected void GridView3_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)// Handles GridView3.RowDataBound
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string drval = (string)DataBinder.Eval(e.Row.DataItem, "FKM_SRL").ToString();
            if ((drval != null))
            {
                e.Row.BackColor = System.Drawing.Color.DarkGray;
                e.Row.ForeColor = System.Drawing.Color.Black;
                e.Row.Font.Bold = true;

            }

        }

    }


    protected void Tab1_Click(object sender, System.EventArgs e) //
    {
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
        Tab3.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;
    }
    protected void Tab2_Click(object sender, System.EventArgs e) //
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Clicked";
        Tab3.CssClass = "Initial";
        MainView.ActiveViewIndex = 1;
    }
    protected void Tab3_Click(object sender, System.EventArgs e) //
    {
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Initial";
        Tab3.CssClass = "Clicked";
        MainView.ActiveViewIndex = 2;
    }

    public void Button1_Click(object sender, System.EventArgs e)            // Handles Button1.Click
    {

        ReportDocument rpt = new ReportDocument();
        string rname = Server.MapPath("~/Reports/NAVSUMMRPT1.rpt");
        string dwnldfname = Server.MapPath("~/ReportsGenerate/NAVRPT.xls");
        DataTable dt = (DataTable)Session["NAVRPT_dt_NAVRPT_REPORT"];

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

    public void Button2_Click(object sender, System.EventArgs e)            // Handles Button2.Click
    {

        ReportDocument rpt = new ReportDocument();
        string rname = Server.MapPath("~/Reports/NAVSUMMRPT2.rpt");
        string dwnldfname = Server.MapPath("~/ReportsGenerate/NAVREPORT2.xls");
        DataTable dt = (DataTable)Session["NAVRPT_dt_NAVRPT_REPORT"];

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
            Response.AddHeader("content-disposition", "attachment;filename =" + "NAVREPORT2.xls");
            Response.ContentType = "application/ms-excel";
            Response.TransmitFile(dwnldfname);

        }
    }
    public void Button3_Click(object sender, System.EventArgs e)            // Handles Button2.Click
    {

        ReportDocument rpt = new ReportDocument();
        string rname = Server.MapPath("~/Reports/NAVSUMMRPT3.rpt");
        string dwnldfname = Server.MapPath("~/ReportsGenerate/NAVREPORT3.xls");
        DataTable dt = (DataTable)Session["NAVRPT_dt_NAVRPT_REPORT"];

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
            Response.AddHeader("content-disposition", "attachment;filename =" + "NAVREPORT3.xls");
            Response.ContentType = "application/ms-excel";
            Response.TransmitFile(dwnldfname);

        }
    }

}
