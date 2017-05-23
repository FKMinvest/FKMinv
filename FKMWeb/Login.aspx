<%@ Page Title="FKM LOGIN" Language="C#" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Login" %>

<%--<%@ Page Title="FKM LOGIN" Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb"
    Inherits="Account_Login" %>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head1" runat="server">
    <title>FKMINVEST</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
   <%-- <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>--%>
    <%--<script>
        $(document).ready(function () {
            $(document.getElementById("<%= Image1.ClientID %>")).click(function () {
                $(document.getElementById("<%= Image2.ClientID %>")).fadeOut(350);
                $(document.getElementById("<%= Image1.ClientID %>")).fadeOut(350);
                $(document.getElementById("<%= Image1.ClientID %>")).fadeIn(450);
                $("#login").fadeIn(1500);
                $("#TXT_COMP_CD").val("001");
            });
            $(document.getElementById("<%= Image2.ClientID %>")).click(function () {
                $(document.getElementById("<%= Image2.ClientID %>")).fadeOut(350);
                $(document.getElementById("<%= Image1.ClientID %>")).fadeOut(350);
                $(document.getElementById("<%= Image2.ClientID %>")).fadeIn(450);
                $("#login").fadeIn(1500);
                $("#TXT_COMP_CD").val("002");
            });
        });
</script>--%>
<%--
<script>
             $(document).ready(function () {
                 $(document.getElementById("<%= Image1.ClientID %>")).click(function () {
                     $(document.getElementById("<%= Image3.ClientID %>")).fadeOut(350);
                     $(document.getElementById("<%= Image2.ClientID %>")).fadeOut(350);
                     $(document.getElementById("<%= Image1.ClientID %>")).fadeOut(350);
                     $(document.getElementById("<%= Image1.ClientID %>")).fadeIn(450);
                     $("#login").fadeIn(1500);
                     $("#TXT_COMP_CD").val("001");
                 });
                 $(document.getElementById("<%= Image2.ClientID %>")).click(function () {
                     $(document.getElementById("<%= Image3.ClientID %>")).fadeOut(350);
                     $(document.getElementById("<%= Image2.ClientID %>")).fadeOut(350);
                     $(document.getElementById("<%= Image1.ClientID %>")).fadeOut(350);
                     $(document.getElementById("<%= Image2.ClientID %>")).fadeIn(450);
                     $("#login").fadeIn(1500);
                     $("#TXT_COMP_CD").val("002");
                 });
                 $(document.getElementById("<%= Image3.ClientID %>")).click(function () {
                     $(document.getElementById("<%= Image3.ClientID %>")).fadeOut(350);
                     $(document.getElementById("<%= Image2.ClientID %>")).fadeOut(350);
                     $(document.getElementById("<%= Image1.ClientID %>")).fadeOut(350);
                     $(document.getElementById("<%= Image3.ClientID %>")).fadeIn(450);
                     $("#login").fadeIn(1500);
                     $("#TXT_COMP_CD").val("003");
                 });
             });
</script>--%>
</head>
<body>
    <form id="Form1" runat="server">
    <div class="page" style="box-shadow: 0 0 20px #000;">
    
                    <br />
                    <br />
        <table width="100%">
            <tr>
                <td align="center">
                   <%-- <h2>
                        WELCOME TO FKMINVEST
                    </h2>
                    <hr />--%>
                    <%--<div class="title" style="width:1000px;" >--%>
           <%--<span style=" font-size: 2.5em;     padding: 0px 0px 0px 20px;  font-weight: 700; color: #f9f9f9;" >FKM</span>
                <span style=" font-size: 2.2em;     font-weight: 600; color: #f9f9f9;" >INVEST</span>--%>
                <%--<marquee scrollamount="3"  width="100%" onmousedown="this.stop();" onmouseup="this.start();"
                                        behavior="alternate" >--%>
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/fkmlogo.png" Height="70px" Width="350px" />
                      <%--</marquee>--%>
                      
            <%--</div>--%>
                    <%--   <h2>
        Log In
    </h2>--%>
                </td>
                
                <td align="center"  >
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/images/fenklogo.png" Height="70px" Width="350px"/>
                    <br />
                    <br />
                </td>
               <%-- <td align="center"  >
                        <asp:Image ID="Image3" runat="server" ImageUrl="~/images/perslogo.png" Height="70px" Width="350px"/>
                    <br />
                    <br />
                </td>--%>
            </tr>
            <tr>
                <td colspan="2" align="center">
                 
                    <div style="display:none;">
                    <asp:TextBox  ID="TXT_COMP_CD" runat="server" Text="001"  ></asp:TextBox>
                    </div>
                   
                    <div class="login" id="login">
                    <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="false" 
                     OnAuthenticate="LoginUser_Authenticate"  OnLoggedIn="LoginUser_LoggedIn"> 
                        <LayoutTemplate>
                            <span class="failureNotification">
                                <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                            </span>
                            <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification"
                                ValidationGroup="LoginUserValidationGroup" />
                            <div class="accountInfo">
                                <fieldset class="login">
                                    <legend>LOGIN CREDENTIALS</legend>
                                    <p>
                                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">USERNAME</asp:Label>
                                        <asp:TextBox ID="UserName" runat="server" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                            CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required."
                                            ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                                    </p>
                                    <p>
                                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">PASSWORD</asp:Label>
                                        <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                            CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required."
                                            ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                                    </p>
                                    <p>
                                        <asp:CheckBox ID="RememberMe" runat="server" />
                                        <asp:Label ID="RememberMeLabel"    runat="server" AssociatedControlID="RememberMe" CssClass="inline">KEEP ME LOGGED IN</asp:Label>
                                    </p>
                                </fieldset>
                                <p class="submitButton">
                                    <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" ValidationGroup="LoginUserValidationGroup" />
                                </p>
                            </div>
                        </LayoutTemplate>
                    </asp:Login>
                    </div>
                  
                </td>
            </tr>
        </table>
    </div>
    <div class="footer">
    </div>
    </form>
</body>
</html>
