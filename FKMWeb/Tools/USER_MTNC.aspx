<%@ Page Title="USER MAINTENANCE" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="false"
    CodeFile="USER_MTNC.aspx.cs" Inherits="Tools_USER_MTNC" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="HeaderContent1" runat="server" ContentPlaceHolderID="ContenHeader">
    <table width="100%">
        <tr>
            <td align="center" style="font-size: large;">
                USER MAINTENANCE
                <hr />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table>
        <tr>
            <td>
                <asp:Label ID="LBL_USR_REFSRL" runat="server" Text="SELECT"></asp:Label>
            </td>
            <td>
                <cc1:ComboBox ID="CMB_USR_REFSRL" runat="server" DataTextField="USR_NAME" DataValueField="USR_USERID"
                    DropDownStyle="DropDownList" AutoCompleteMode="SuggestAppend" Width="300px" AutoPostBack="true">
                </cc1:ComboBox>
            </td>
            <td rowspan="20" valign="top">
                <div style="overflow-x: auto; overflow-y: auto; height: 400PX; width: 800PX;">
                    <%--  <asp:Button ID="Button1" runat="server" Text="DOWNLOAD REPORT" />--%>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="USR_USERID"
                        RowStyle-Wrap="false" HeaderStyle-BackColor="#0071ac" HeaderStyle-ForeColor="WhiteSmoke"
                        RowStyle-ForeColor="Black"   HeaderStyle- 
                        RowStyle-Font-Size="Small" RowStyle-Height="25px" RowStyle-Font-Bold="true">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkRow" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowSelectButton="true" />
                            <asp:BoundField DataField="USR_USERID" HeaderText="USERID" SortExpression="USR_USERID"
                                ItemStyle-Width="60Px" />
                            <asp:BoundField DataField="USR_NAME" HeaderText="NAME" SortExpression="USR_NAME"
                                ItemStyle-Width="120Px" />
                            <asp:BoundField DataField="USR_EMAILID" HeaderText="EMAIL ID" SortExpression="USR_EMAILID"
                                ItemStyle-Width="120Px" />
                            <%--  <asp:BoundField DataField="USR_TELEPHONE" HeaderText="TELEPHONE" SortExpression="USR_TELEPHONE"
                                ItemStyle-Width="60Px" />--%>
                            <asp:BoundField DataField="USR_STATUS" HeaderText="STATUS" SortExpression="USR_STATUS"
                                ItemStyle-Width="60Px" />
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_USR_USERID" runat="server" Text="USER ID"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TXT_USR_USERID" runat="server" MaxLength="10" Width="200px" AutoPostBack="true"
                    onchange="rmvqoutes(this)"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_USR_NAME" runat="server" Text="NAME"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TXT_USR_NAME" runat="server" TextMode="MultiLine" Rows="2" MaxLength="100"
                    Width="200px" onchange="rmvqoutes(this)"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_USR_CIVILID" runat="server" Text="CIVIL ID"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TXT_USR_CIVILID" runat="server" MaxLength="12" Width="200px" onchange="rmvqoutes(this)"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_USR_MENUGROUP" runat="server" Text="MENU GROUP"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TXT_USR_MENUGROUP" runat="server" MaxLength="10" Width="200px" onchange="rmvqoutes(this)"></asp:TextBox>
            </td>
        </tr>
        <%--  <tr>
            <td>
                <asp:Label ID="LBL_USR_OPN_BALANCE" runat="server" Text="OPENING AMOUNT" Visible="false"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TXT_USR_OPN_BALANCE" runat="server" Width="200px"  Visible="false"  onchange="numformat(this)" ></asp:TextBox>
                 
                        
            </td>
        </tr>--%>
        <%--  <tr>
            <td>
                <asp:Label ID="LBL_USR_BALANCE_AMT" runat="server" Text="BALANCE AMOUNT"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TXT_USR_BALANCE_AMT" runat="server"  Width="200px"  onchange="numformat(this)" ></asp:TextBox>
                             
            </td>
        </tr>--%>
        <tr>
            <td>
                <asp:Label ID="LBL_USR_EMAILID" runat="server" Text="EMAIL ID"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TXT_USR_EMAILID" runat="server" TextMode="MultiLine" Rows="3" MaxLength="100"
                    Width="200px" onchange="rmvqoutes(this)"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_USR_ADDRESS" runat="server" Text="ADDRESS" Visible="false"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TXT_USR_ADDRESS" runat="server" TextMode="MultiLine" Rows="4" Width="200px"
                    Visible="false" onchange="rmvqoutes(this)"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_USR_TELEPHONE" runat="server" Text="TELEPHONE" Visible="false"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TXT_USR_TELEPHONE" runat="server" MaxLength="10" Width="200px" Visible="false"
                    onchange="rmvqoutes(this)"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_USR_LST_ACCESS_DT" runat="server" Text="LAST ACCESSED ON"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TXT_USR_LST_ACCESS_DT" runat="server" MaxLength="10" Width="200px"
                    ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_USR_LST_ACS_TIME" runat="server" Text="LAST ACCESSED AT"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TXT_USR_LST_ACS_TIME" runat="server" MaxLength="10" Width="200px"
                    ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_USR_STATUS" runat="server" Text="STATUS"></asp:Label>
            </td>
            <td>
                <cc1:ComboBox ID="CMB_USR_STATUS" runat="server" DropDownStyle="DropDownList"  
                      AutoCompleteMode="SuggestAppend" Width="200px">
                    <asp:ListItem Selected="true" Text="NORMAL" Value="A" />
                    <asp:ListItem Text="INACTIVE" Value="I" />
                    <asp:ListItem Text="SEARCH OPS" Value="S" />
                </cc1:ComboBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="BTN_ADD" runat="server" Text="SAVE" />
                <asp:Button ID="BTN_UPDT" runat="server" Text="UPDATE" />
            </td>
            <td>
                <asp:Button ID="BTN_CLEAR" runat="server" Text="CLEAR" />
            </td>
        </tr>
    </table>
</asp:Content>
