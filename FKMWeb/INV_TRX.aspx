<%@ Page Title="FKM INVEST INFO" Language="C#" AutoEventWireup="false" CodeFile="INV_TRX.aspx.cs"
    Inherits="MAIN_INV_TRX" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head1" runat="server">
    <title>FKM INVEST</title>
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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
        <table width="100%">
            <tr>
                <td align="center" colspan="3">
                    <h1>
                        <strong>
                            <asp:Label ID="PROJECTNAME" runat="server"></asp:Label></strong>
                    </h1>
                    <div id="flip" style="text-decoration: 'underline'| inherit;">
                        <hr />
                    </div>
                    <br />
                    <br />
                    <%--   <h2>
        Log In
    </h2>--%>
                </td>
            </tr>
            <tr>
                <td colspan="3" align="left">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="LBL_FKM_REF" runat="server" Text="CODE"></asp:Label>
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="TXT_FKM_REF" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td rowspan="25" valign="top" align="center" style="width: 650px;">
                                <div style="margin-left: 1px;">
                                    <asp:HyperLink runat="server" ID="DownloadButton" Text="DOWNLOAD SPREADSHEET " NavigateUrl=""
                                        Target="_blank" Visible="false" />
                                </div>
                                <div style="margin-right: 1px;">
                                    <asp:Button ID="Button2" runat="server" Text="DOWNLOAD SPREADSHEET" OnClick="Button2_Click" />
                                    <asp:Button ID="Button1" runat="server" Text="DOWNLOAD REPORT" OnClick="Button1_Click" />
                                </div>
                                <br />
                                <div style="overflow-x: auto; overflow-y: auto; height: 650px; width: 100%; margin: 0 auto;">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="TRX_REFSRL"
                                        RowStyle-Wrap="false" HeaderStyle-BackColor="#0071ac" HeaderStyle-ForeColor="WhiteSmoke"
                                        RowStyle-ForeColor="Black"   HeaderStyle- 
                                        RowStyle-Font-Size="Small" RowStyle-Height="25px" RowStyle-Font-Bold="true" Caption="TRANSACTION DETAILS OVER INVESTMENT"
                                        Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="TRX_REFSRL" HeaderText="REFERENCE" SortExpression="TRX_REFSRL" />
                                            <%--     <asp:BoundField DataField="TRX_REFMEMO" HeaderText="REFERENCE MEMO" SortExpression="TRX_REFMEMO" />--%>
                                            <asp:BoundField DataField="TRX_ISSUEDATE" HeaderText="DATE" SortExpression="TRX_ISSUEDATE" />
                                            <asp:BoundField DataField="TRX_INVAMT_R" HeaderText="INVESTMENT AMOUNT" SortExpression="TRX_INVAMT"
                                                ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="TRX_EXPNSAMT_R" HeaderText="EXPENSE" SortExpression="TRX_EXPNSAMT"
                                                ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="TRX_AMT_R" HeaderText="INCOME" SortExpression="TRX_AMT"
                                                ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="TRX_ROI_R" HeaderText="RATE OF INTEREST" SortExpression="TRX_ROI"
                                                ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="TRX_REMX" HeaderText="REMARKS" SortExpression="TRX_REMX" />
                                            <%--   <asp:BoundField DataField="TRX_UPDBY" HeaderText="UPDATED BY" SortExpression="TRX_UPDBY" />--%>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FKMCONNSTR %>"
                                    SelectCommand="SELECT  [TRX_REFSRL],[TRX_REFMEMO], [TRX_ISSUEDATE], [TRX_INVAMT],[TRX_AMT], [TRX_ROI], [TRX_REMX],[TRX_UPDBY] FROM [INVEST_TRX] WHERE (([TRX_FKM_SRL] = @TRX_FKM_SRL) AND ([TRX_FKM_SRL] = @TRX_FKM_SRL2))">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="TXT_FKM_REF" DefaultValue="0" Name="TRX_FKM_SRL"
                                            PropertyName="Text" Type="String" />
                                        <asp:ControlParameter ControlID="TXT_FKM_REF" DefaultValue="0" Name="TRX_FKM_SRL2"
                                            PropertyName="Text" Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource>--%>
                            </td>
                        </tr>
                        <%--   <tr>
                            <td>
                                <asp:Label ID="LBL_PROJNAME" runat="server" Text="PROJECT NAME"></asp:Label>
                            </td>
                            <td class="style1">
                                <asp:TextBox    ID="TXT_PROJNAME" runat="server" Width="300px" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>--%>
                        <tr>
                            <td>
                                <asp:Label ID="LBL_PRTCPDATE" runat="server" Text="PARTICIPATION&nbsp;DATE"></asp:Label>
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="TXT_PRTCPDATE" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                                <%-- <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TXT_PRTCPDATE" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LBL_MTRTDATE" runat="server" Text="MATURITY&nbsp;DATE"></asp:Label>
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="TXT_MTRTDATE" runat="server" ReadOnly="true" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LBL_BANK" runat="server" Text="BANK"></asp:Label>
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="TXT_BANK" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LBL_YLDPRD" runat="server" Text="YEILD PERIOD"></asp:Label>
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="TXT_YLDPRD" runat="server" ReadOnly="true" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LBL_CURR" runat="server" Text="CURRENCY"></asp:Label>
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="TXT_CURR" runat="server" ReadOnly="true" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td class="style1">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LBL_COMMCAP" runat="server" Text="COMMITTED&nbsp;CAPITAL"></asp:Label>
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="TXT_COMMCAP" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LBL_COMMCAP2" runat="server" Text="COMMITTED&nbsp;CAPITAL2" Visible="false"></asp:Label>
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="TXT_COMMCAP2" runat="server" ReadOnly="true" Width="200px" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LBL_PAID" runat="server" Text="PAID"></asp:Label>
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="TXT_PAID" runat="server" ReadOnly="true" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LBL_OSAMT" runat="server" Text="OUTSTANDING AMOUNT"></asp:Label>
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="TXT_OSAMT" runat="server" ReadOnly="true" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LBL_INVAMT" runat="server" Text="INVEST AMOUNT"></asp:Label>
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="TXT_INVAMT" runat="server" ReadOnly="true" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LBL_ROI" runat="server" Text="RATE OF INTEREST"></asp:Label>
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="TXT_ROI" runat="server" ReadOnly="true" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LBL_ACTLRTN" runat="server" Text="ACTUAL RETURNS TILL DATE"></asp:Label>
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="TXT_ACTLRTN" runat="server" ReadOnly="true" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LBL_ANNLRTN" runat="server" Text="ANNUAL RETURNS"></asp:Label>
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="TXT_ANNLRTN" runat="server" ReadOnly="true" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <a href="#" class="go-top" style="display: none;">Back to top</a>
    <%--   <script src="js/jquery.js" type="text/javascript"></script>
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <script src="js/jquery.backstretch.js" type="text/javascript"></script>
    <script type="text/javascript">
        'use strict';

        /* ========================== */
        /* ::::::: Backstrech ::::::: */
        /* ========================== */
        // You may also attach Backstretch to a block-level element
        //        $.backstretch("img/grey1.png");
        $.backstretch(
        [

            "img/blue1.png",
            "img/grey1.png",
            "img/blue2.png",
            "img/grey2.png",
            "img/blue3.png",
            "img/blue4.png",
            "img/grey1.png",
            "img/blue5.png",
            "img/blue6.png",
            "img/grey2.png"
        ],

        {
            duration: 7500,
            fade: 1500
        }
    );
    </script>--%>
    </form>
</body>
</html>
