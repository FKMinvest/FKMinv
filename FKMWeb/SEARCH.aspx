<%@ Page Title="SEARCH" Language="C#" AutoEventWireup="false" CodeFile="SEARCH.aspx.cs"
    Inherits="MAIN_SEARCH" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head1" runat="server">
    <title>FKM INVESMENT CO.</title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
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
        
        .go-top
        {
            position: fixed;
            bottom: 2em;
            right: 0px;
            text-decoration: none;
            color: #000000;
            background-color: rgba(235, 235, 235, 0.80);
            font-size: 12px;
            padding: 1em;
            display: none;
        }
        
        .go-top:hover
        {
            background-color: rgba(135, 135, 135, 0.50);
            margin-right: 0px auto 0;
        }
        
        .imgdiv
        {
            margin: 0px;
            width: 350px;
            margin-top: 0px;
            background-size: cover;
            z-index: -1;
            height: 70px;
            background-image: url('images/fkmlogo.png');
        }
    </style>
</head>
<body>
    <form id="Form1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            // Show or hide the sticky footer button
            $(window).scroll(function () {
                if ($(this).scrollTop() > 300) {
                    $('.go-top').fadeIn(400);
                } else {
                    $('.go-top').fadeOut(300);
                }
            });

            setTimeout(changeBackground, 15000);

            // Animate the scroll to top
            $('.go-top').click(function (event) {
                event.preventDefault();

                $('html, body').animate({ scrollTop: 0 }, 300);
            })
        });

        var currentBackground = 0;

        var backgrounds = [];
        backgrounds[0] = 'images/fkmlogo.png';
        backgrounds[1] = 'images/fenklogo.png';
        backgrounds[2] = 'images/fkmlogo.png';
        backgrounds[3] = 'images/fenklogo.png';
        backgrounds[4] = 'images/fkmlogo.png';
        backgrounds[5] = 'images/fenklogo.png';
        backgrounds[6] = 'images/fkmlogo.png';
        backgrounds[7] = 'images/fenklogo.png';


        function changeBackground() {

            currentBackground++;

            if (currentBackground > 7) currentBackground = 0;

            $('#imgdiv').fadeOut(1000, function (event) {

                $('#imgdiv').css({
                    'background-image': "url('" + backgrounds[currentBackground] + "')"
                });
                $('#imgdiv').fadeIn(1000);
            });


            setTimeout(changeBackground, 15000);
        }

        
    </script>
    <div class="main" style="margin-top: 0px; margin-bottom: 10px; width: 97%; margin: 0 auto;
        background-color: White; padding-top: 10px; box-shadow: 10px 10px 15px; height: 95%;">
        <div class="header">
            <div class="title" style="width: 850px; margin-top: 0px; margin-left: 15px;">
                <%--<span style=" font-size: 2.5em;     padding: 0px 0px 0px 20px;  font-weight: 700; color: #f9f9f9;" >FKM</span>
                <span style=" font-size: 2.2em;     font-weight: 600; color: #f9f9f9;" >INVEST</span>--%>
                <%-- <marquee scrollamount="3"  width="100%" onmousedown="this.stop();" onmouseup="this.start();"
                                        behavior="alternate" >--%>
                <div id="imgdiv" runat="server" class="imgdiv">
                </div>
                <%--  <asp:Image ID="Image1" runat="server" ImageUrl="~/fkmlogo9.png" Height="70px" Width="350px" />--%>
                <%--</marquee>--%>
            </div>
            <div class="loginDisplay">
                <asp:LoginView ID="HeadLoginView" runat="server" EnableViewState="false">
                    <AnonymousTemplate>
                        [ <a href="~/Account/Login.aspx" id="HeadLoginStatus" runat="server">Log In</a>
                        ]
                    </AnonymousTemplate>
                    <LoggedInTemplate>
                        Welcome <span class="bold">
                            <asp:LoginName ID="HeadLoginName" runat="server" />
                        </span>! [
                        <asp:LoginStatus ID="HeadLoginStatus" runat="server" LogoutAction="Redirect" LogoutText="Log Out"
                            LogoutPageUrl="~/Account/Login.aspx" />
                        ]
                    </LoggedInTemplate>
                </asp:LoginView>
                <iframe src="http://free.timeanddate.com/clock/i4z8j5et/n123/fn15/ahr/ftb/tt0/tb1"
                    frameborder="0" width="285" height="18"></iframe>
            </div>
        </div>
        <table width="100%" style="height: 100%;">
            <tr>
                <td align="center" rowspan="3">
                    <br />
                    <br />
                </td>
                <td align="center" colspan="3">
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td style="font-size: large;">
                    <asp:TextBox ID="TXT_SRCH" runat="server" Width="400px" Height="20PX" Font-Bold="true"
                        Font-Size="Large" OnTextChanged="TXTSRCH_TextChanged" AutoPostBack="true" ></asp:TextBox>
                    <asp:Button ID="BTNSRCH" runat="server" Text="SEARCH"  OnClick="BTNSRCH_Click" />
                </td>
                <td>
                    <div id="pnl" style="font-size: medium;">
                        <asp:CheckBox ID="chbx_and_or" Text="UNION" runat="server" />
                        <%-- <asp:CheckBox ID="chbx_hidden" Text="HIDE" Checked="true" runat="server" />--%>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button1" runat="server" Text="DOWNLOAD" ToolTip="Download the Main Report" OnClick="Button1_Click"  />
                        <asp:Button ID="Button2" runat="server" Text="DOWNLOAD" ToolTip="Download the NAV Report" OnClick="Button2_Click" />
                        <asp:Button ID="Button3" runat="server" Text="DOWNLOAD" ToolTip="Download the Liquidated Report"  OnClick="Button3_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button1b" runat="server" Text="SUMMARY" ToolTip="Download the Summary Report" OnClick="Button1b_Click"  />
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
        <%-- <div style="overflow-x: auto; overflow-y: auto; height:100%; width: 100%;">--%>
        <div style="overflow-x: auto; overflow-y: auto; height: 98%; width: 100%;">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="FKM_SRL"
                RowStyle-Wrap="false" HeaderStyle-BackColor="#0071ac" HeaderStyle-ForeColor="WhiteSmoke"
                RowStyle-ForeColor="Black"   HeaderStyle- 
                RowStyle-Font-Size="Small" RowStyle-Height="28px" RowStyle-Font-Bold="true"
                 OnRowDataBound="GridView1_RowDataBound"  >
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="FKM_SRL" HeaderText="CODE" DataNavigateUrlFormatString="INV_TRX.aspx?FKMCODE={0}"
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
             DataNavigateUrlFormatString="INVEST/INVEST_EDIT.aspx?REFSRL={0}" Target="_blank" DataTextField="FKM_PROJNAME" ItemStyle-Width="300Px" />
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
                RowStyle-ForeColor="Black"   HeaderStyle- 
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
                RowStyle-ForeColor="Black"   HeaderStyle- 
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
        </div>
        <a href="#" class="go-top" style="display: none;">Back to top</a>
    </div>
    </form>
</body>
</html>
