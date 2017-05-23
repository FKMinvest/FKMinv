<%@ Page Title="HOME SEARCH" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="false"
    CodeFile="HOME.aspx.cs" Inherits="MAIN_HOME" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style4
        {
            width: 100%;
        }
        
        #panel, #flip
        {
            padding: 5px;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="HeaderContent1" runat="server" ContentPlaceHolderID="ContenHeader">
    <script>

        var keyValue
        function PrintSlipClick(element, key) {
            keyValue = key;
            alert('Printing under process : ' + keyValue + ' ...');
        }

        $(document).ready(function () {
            $("#flip").click(function () {
                $("#panel").slideToggle("slow");
            });

            $("#export").click(function () {
                $("#dwnld").slideDown("slow");
            });

            $("#dwnld").click(function () {
                $("#dwnld").slideUp("slow");
            });
        });
    </script>
    <table width="100%" style="height: 800px auto;">
        <tr>
            <td style="font-size: large;">
                <asp:TextBox ID="TXT_SRCH" runat="server" Width="400px" Height="20PX" Font-Bold="true"
                    Font-Size="Large"   AutoPostBack="true"   ></asp:TextBox>
                <asp:Button ID="BTNSRCH" runat="server" Text="SEARCH"  OnClick="BTNSRCH_Click"/>
            </td>
            <td>
                <div id="pnl" style="font-size: medium;">
                    <asp:CheckBox ID="chbx_and_or" Text="UNION" runat="server" />
                    <%-- <asp:CheckBox ID="chbx_hidden" Text="HIDE" Checked="true" runat="server" />--%>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button1" runat="server" Text="EXCEL" ToolTip="Download the Main Report"  />
                        <asp:Button ID="Button1_pdf" runat="server" Text="PDF" ToolTip="Download the Main Report"    />
                        <asp:Button ID="Button2" runat="server" Text="DOWNLOAD" ToolTip="Download the NAV Report"  />
                        <asp:Button ID="Button3" runat="server" Text="DOWNLOAD" ToolTip="Download the Liquidated Report"   />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button1b" runat="server" Text="SUMMARY" ToolTip="Download the Summary Report"    />
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div id="flip" style="text-decoration: 'underline'| inherit;">
                    <hr />
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent"> 
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="FKM_SRL"
            RowStyle-Wrap="false" HeaderStyle-BackColor="#0071ac" HeaderStyle-ForeColor="WhiteSmoke"
            RowStyle-ForeColor="Black"   
            RowStyle-Font-Size="Small" RowStyle-Height="28px" RowStyle-Font-Bold="true"  >
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="FKM_SRL" HeaderText="CODE" DataNavigateUrlFormatString="~/INV_TRX.aspx?FKMCODE={0}"
                    Target="_blank" DataTextField="FKM_SRL" />
                <%--     <asp:BoundField DataField="FKM_SRL" HeaderText="SRL" SortExpression="FKM_SRL" ItemStyle-Width="70Px"
                    ReadOnly="True" />--%>
                <asp:BoundField DataField="FKM_ISSDATE" HeaderText="ISSUE DATE" SortExpression="FKM_ISSDATE"
                    ItemStyle-Width="100Px" />
                <asp:BoundField DataField="FKM_CURR" HeaderText="CURRRENCY" SortExpression="FKM_CURR"
                    ControlStyle-Width="200px" ItemStyle-Width="100Px" />
                <asp:BoundField DataField="FKM_PROJNAME" HeaderText="PROJECT NAME" SortExpression="FKM_PROJNAME"
                    ItemStyle-Width="300Px" />
                <%--<asp:HyperLinkField DataNavigateUrlFields="FKM_SRL,FKM_PROJNAME" HeaderText="PROJECT NAME"
             DataNavigateUrlFormatString="~/INVEST/INVEST_EDIT.aspx?REFSRL={0}" Target="_blank" DataTextField="FKM_PROJNAME" ItemStyle-Width="300Px" />
                <asp:BoundField DataField="FKM_HOLDNAME" HeaderText="HOLDING NAME" SortExpression="FKM_HOLDNAME"
                    ItemStyle-Width="300Px" />--%>
                <asp:BoundField DataField="FKM_LOCATION" HeaderText="LOCATION" SortExpression="FKM_LOCATION"
                    ControlStyle-Width="400px" ItemStyle-Width="100Px" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="FKM_INVCOMP" HeaderText="INVESTMENT CATEGORY" SortExpression="FKM_INVCOMP"
                    ControlStyle-Width="300px" />
                <asp:BoundField DataField="FKM_OPPRBY" HeaderText="OPPORTUNITY OFFERED BY" SortExpression="FKM_OPPRBY"
                    ControlStyle-Width="300px" />
                <asp:BoundField DataField="FKM_BANK" HeaderText="BANK" SortExpression="FKM_BANK"
                    ControlStyle-Width="300px" />
                <asp:BoundField DataField="FKM_PRVTEQT" HeaderText="PRIVATE EQUITY" SortExpression="FKM_PRVTEQT"
                    ControlStyle-Width="300px" />
                <asp:BoundField DataField="FKM_YEILDPRD" HeaderText="YEILD PERIOD" SortExpression="FKM_YEILDPRD"
                    ControlStyle-Width="300px" />
                <asp:BoundField DataField="FKM_PRTCPDATE" HeaderText="PARTICIPATION DATE" SortExpression="FKM_PRTCPDATE"
                    ControlStyle-Width="150px" />
                <asp:BoundField DataField="FKM_MTRTDATE" HeaderText="MATURITY DATE" SortExpression="FKM_MTRTDATE"
                    ControlStyle-Width="150px" />
                <asp:BoundField DataField="FKM_COMMCAP_R" HeaderText="COMMITED CAPITAL" SortExpression="FKM_COMMCAP"
                    ControlStyle-Width="120px" ItemStyle-HorizontalAlign="Right" />
                <%-- <asp:BoundField DataField="FKM_COMMCAP2_R" HeaderText="COMMITTED CAP2" SortExpression="FKM_COMMCAP2"
                    ItemStyle-HorizontalAlign="Right" />--%>
                <asp:BoundField DataField="FKM_INVAMT_R" HeaderText="INVESTMENT" SortExpression="FKM_INVAMT"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_ROI_R" HeaderText="ROI" SortExpression="FKM_ROI" ItemStyle-HorizontalAlign="Right"
                    ControlStyle-Width="70px" />
                <asp:BoundField DataField="FKM_CAPUNPD_R" HeaderText="CAPITAL UNPAID" SortExpression="FKM_CAPUNPD"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_CAPRFND_R" HeaderText="CAPITAL REFUND" SortExpression="FKM_CAPRFND"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_EXPNS_R" HeaderText="EXPENSE" SortExpression="FKM_EXPNS"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_MONYCAL_R" HeaderText="MONTHLY YEILD" SortExpression="FKM_MONYCAL"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_QRTYCAL_R" HeaderText="QUARTERLY YEILD" SortExpression="FKM_QRTYCAL"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_SMANYCAL_R" HeaderText="SEMI ANNUAL YEILD" SortExpression="FKM_SMANYCAL"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_ANLYCAL_R" HeaderText="ANNUAL YEILD" SortExpression="FKM_ANLYCAL"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_SCNINCP_R" HeaderText="SINCE INCEPTIION" SortExpression="FKM_SCNINCP"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_SALEPRCD_R" HeaderText="SALE PROCEED" SortExpression="FKM_SALEPRCD"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_REMX" HeaderText="REMARKS" SortExpression="FKM_REMX" />
                <%-- 
                <asp:BoundField DataField="FKM_INCGEN" HeaderText="INC GEN" SortExpression="FKM_INCGEN" />
                <asp:BoundField DataField="FKM_STATUS" HeaderText="STATUS" SortExpression="FKM_STATUS" />
                <asp:BoundField DataField="FKM_LNKSRL" HeaderText="LNK SRL" SortExpression="FKM_LNKSRL" />
                <asp:BoundField DataField="FKM_CAPPD_R" HeaderText="CAPITAL PAID" SortExpression="FKM_CAPPD"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_VALVTN_R" HeaderText="VALUATION" SortExpression="FKM_VALVTN"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_DVDND_R" HeaderText="DIVIDEND" SortExpression="FKM_DVDND"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_BOOKVAL_R" HeaderText="BOOK VALUE" SortExpression="FKM_BOOKVAL"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_CAPGN_R" HeaderText="CAPITAL GAIN" SortExpression="FKM_CAPGN"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_UNRLCAP_R" HeaderText="UNRL CAPITAL" SortExpression="FKM_UNRLCAP"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_FAIRVAL_R" HeaderText="FAIR VALUE" SortExpression="FKM_FAIRVAL"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_NAV_R" HeaderText="NAV" SortExpression="FKM_NAV" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_REMX" HeaderText="REMARKS" SortExpression="FKM_REMX" />
                <asp:BoundField DataField="FKM_UPDBY" HeaderText="UPD BY" SortExpression="FKM_UPDBY" />--%>
            </Columns>
        </asp:GridView>
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="FKM_SRL"
            RowStyle-Wrap="false" HeaderStyle-BackColor="#0071ac" HeaderStyle-ForeColor="WhiteSmoke"
            RowStyle-ForeColor="Black"    
            RowStyle-Font-Size="Small" RowStyle-Height="25px" RowStyle-Font-Bold="true">
            <Columns>
                <asp:BoundField DataField="FKM_SRL" HeaderText="SRL" SortExpression="FKM_SRL" ItemStyle-Width="70Px"
                    ReadOnly="True" />
                <asp:BoundField DataField="FKM_PROJNAME" HeaderText="PROJECT NAME" SortExpression="FKM_PROJNAME"
                    ItemStyle-Width="300Px" />
                <asp:BoundField DataField="FKM_PRTCPDATE" HeaderText="PARTICIPATION DATE" SortExpression="FKM_PRTCPDATE" />
                <asp:BoundField DataField="FKM_MTRTDATE" HeaderText="LIQUIDATION DATE" SortExpression="FKM_MTRTDATE" />
                <asp:BoundField DataField="FKM_COMMCAP_R" HeaderText="COMMITED CAPITAL" SortExpression="FKM_COMMCAP"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_CAPPD_R" HeaderText="PAID" SortExpression="FKM_CAPPD"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_CAPUNPD_R" HeaderText="OUTSTANDING" SortExpression="FKM_CAPUNPD"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_INVAMT_R" HeaderText="INVESTMENT AMOUNT (COST)" SortExpression="FKM_INVAMT"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_ROI_R" HeaderText="RATE OF INTEREST" SortExpression="FKM_ROI"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_ANLINCMCY_R" HeaderText="ANNUAL INTEREST INCOME ON CURRENT YIELD"
                    SortExpression="FKM_ANLINCMCY" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_SCNINCP_R" HeaderText="ACTUAL RETURNS TILL [ ]" SortExpression="FKM_SCNINCP"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_ANLRLZD_R" HeaderText="ANNUALIZED REALIZED" SortExpression="FKM_ANLRLZD"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_ACTLINCMRU_R" HeaderText="ACTUAL ON INCOME ECEIVED UPTO [ ]"
                    SortExpression="FKM_ACTLINCMRU" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_UNRLYLD_R" HeaderText="UNREALIZED YIELD" SortExpression="FKM_UNRLYLD"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_DVDND_R" HeaderText="DIVIDEND" SortExpression="FKM_DVDND"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_VALVTN_R" HeaderText="VALUATION" SortExpression="FKM_VALVTN"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_CAPGN_R" HeaderText="CAPITAL GAIN (LOSS)" SortExpression="FKM_CAPGN_R"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_UNRLDVD_R" HeaderText="UNREALIZED YIELD / DIVIDEND "
                    SortExpression="FKM_UNRLDVD" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_UNRLDVDCAP_R" HeaderText="UNREALIZED YIELD / DIVIDEND / CAPITAL GAIN (LOSS) "
                    SortExpression="FKM_UNRLDVDCAP" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_FAIRVAL_R" HeaderText="FAIR VALUE" SortExpression="FKM_FAIRVAL"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_NAV_R" HeaderText="NAV" SortExpression="FKM_NAV" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_REMX" HeaderText="REMARKS" SortExpression="FKM_REMX" />
                <asp:BoundField DataField="FKM_INCGEN" HeaderText="INC GEN" SortExpression="FKM_INCGEN" />
                <asp:BoundField DataField="FKM_INVGRP" HeaderText="CATEGORY" SortExpression="FKM_INVGRP" />
                <asp:BoundField DataField="FKM_CURR" HeaderText="CURRRENCY" SortExpression="FKM_CURR"
                    ItemStyle-Width="100Px" />
                <%--   <asp:HyperLinkField DataNavigateUrlFields="FKM_SRL" HeaderText="FKM SRL" 
             DataNavigateUrlFormatString="INV_TRX.aspx?FKMCODE={0}" Target="_blank" DataTextField="FKM_SRL" />--%>
                <%--     <asp:BoundField DataField="FKM_SRL" HeaderText="SRL" SortExpression="FKM_SRL" ItemStyle-Width="70Px"
                    ReadOnly="True" />--%>
                <%--    <asp:BoundField DataField="FKM_ISSDATE" HeaderText="ISSUE DATE" SortExpression="FKM_ISSDATE"
                    ItemStyle-Width="100Px" />--%>
                <%--
                <asp:BoundField DataField="FKM_HOLDNAME" HeaderText="HOLDING NAME" SortExpression="FKM_HOLDNAME"
                    ItemStyle-Width="300Px" />
                <asp:BoundField DataField="FKM_LOCATION" HeaderText="LOCATION" SortExpression="FKM_LOCATION"
                    ItemStyle-Width="100Px" />--%>
                <%--<asp:BoundField DataField="FKM_BANK" HeaderText="BANK" SortExpression="FKM_BANK" />
                <asp:BoundField DataField="FKM_PRVTEQT" HeaderText="CATE EQT" SortExpression="FKM_PRVTEQT" />
                <asp:BoundField DataField="FKM_YEILDPRD" HeaderText="YEILD PRD" SortExpression="FKM_YEILDPRD" />--%>
                <%-- <asp:BoundField DataField="FKM_OPPRBY" HeaderText="OPPORTUNITY OFFERED BY" SortExpression="FKM_OPPRBY" />
                <asp:BoundField DataField="FKM_INCGEN" HeaderText="INC GEN" SortExpression="FKM_INCGEN" />
                <asp:BoundField DataField="FKM_STATUS" HeaderText="STATUS" SortExpression="FKM_STATUS" />
                <asp:BoundField DataField="FKM_LNKSRL" HeaderText="LNK SRL" SortExpression="FKM_LNKSRL" />
                <asp:BoundField DataField="FKM_CAPPD_R" HeaderText="CAPITAL PAID" SortExpression="FKM_CAPPD"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_VALVTN_R" HeaderText="VALUATION" SortExpression="FKM_VALVTN"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_DVDND_R" HeaderText="DIVIDEND" SortExpression="FKM_DVDND"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_BOOKVAL_R" HeaderText="BOOK VALUE" SortExpression="FKM_BOOKVAL"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_CAPGN_R" HeaderText="CAPITAL GAIN" SortExpression="FKM_CAPGN"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_UNRLCAP_R" HeaderText="UNRL CAPITAL" SortExpression="FKM_UNRLCAP"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_FAIRVAL_R" HeaderText="FAIR VALUE" SortExpression="FKM_FAIRVAL"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_NAV_R" HeaderText="NAV" SortExpression="FKM_NAV" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_REMX" HeaderText="REMARKS" SortExpression="FKM_REMX" />
                <asp:BoundField DataField="FKM_UPDBY" HeaderText="UPD BY" SortExpression="FKM_UPDBY" />--%>
            </Columns>
        </asp:GridView>
        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataKeyNames="FKM_SRL"
            RowStyle-Wrap="false" HeaderStyle-BackColor="#0071ac" HeaderStyle-ForeColor="WhiteSmoke"
            RowStyle-ForeColor="Black"   
            RowStyle-Font-Size="Small" RowStyle-Height="25px" RowStyle-Font-Bold="true">
            <Columns>
                <%--  <asp:HyperLinkField DataNavigateUrlFields="FKM_SRL,FKM_ISSDATE" HeaderText="REF SRL"
                    DataNavigateUrlFormatString="NAV_DTL_EDIT.aspx?REFSRL={0}&QTR={1}" Target="_blank"
                    DataTextField="FKM_SRL" />--%>
                <asp:BoundField DataField="FKM_SRL" HeaderText="SRL" SortExpression="FKM_SRL" />
                <asp:BoundField DataField="FKM_PROJNAME" HeaderText="PROJECT NAME" SortExpression="FKM_PROJNAME"
                    ItemStyle-Width="300Px" />
                <asp:BoundField DataField="FKM_PRTCPDATE" HeaderText="PARTICIPATION DATE" SortExpression="FKM_PRTCPDATE" />
                <asp:BoundField DataField="FKM_LQDDATE" HeaderText="LIQUIDATION DATE" SortExpression="FKM_LQDDATE" />
                <asp:BoundField DataField="FKM_ROI_R" HeaderText="RATE OF INTEREST" SortExpression="FKM_ROI"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_PRINCIPAL_R" HeaderText="PRICIPAL" SortExpression="FKM_PRINCIPAL"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_EXPENSE_R" HeaderText="EXPENSE" SortExpression="FKM_EXPENSE"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_TOTCOST_R" HeaderText="TOTAL COST " SortExpression="FKM_TOTCOST"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_INTEREST_R" HeaderText="INTEREST" SortExpression="FKM_INTEREST_R"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_DVDND_R" HeaderText="DIVIDEND" SortExpression="FKM_DVDND"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_CAPGN_R" HeaderText="CAPITAL GAIN" SortExpression="FKM_CAPGN_R"
                    ItemStyle-HorizontalAlign="Right" />
                <%-- <asp:BoundField DataField="FKM_AMOUNT_R" HeaderText="ACTUAL RETURNS TILL [ ]" SortExpression="FKM_AMOUNT"
                    ItemStyle-HorizontalAlign="Right" />--%>
                <asp:BoundField DataField="FKM_REMX" HeaderText="REMARKS" SortExpression="FKM_REMX" />
                <asp:BoundField DataField="FKM_CURR" HeaderText="CURRRENCY" SortExpression="FKM_CURR"
                    ItemStyle-Width="100Px" />
                <%--   <asp:HyperLinkField DataNavigateUrlFields="INVC_REFSRL" HeaderText="DOWNLOAD" 
             DataNavigateUrlFormatString="INVOICE_LIST.aspx?REFSRL={0}" Target="_blank" DataTextField="INVC_REFSRL" />--%>
            </Columns>
        </asp:GridView> 
    <%-- <p>
        To learn more about ASP.NET visit <a href="http://www.asp.net" title="ASP.NET Website">www.asp.net</a>.
    </p>
    <p>
        You can also find <a href="http://go.microsoft.com/fwlink/?LinkID=152368&amp;clcid=0x409"
            title="MSDN ASP.NET Docs">documentation on ASP.NET at MSDN</a>.
    </p>--%>
</asp:Content>
