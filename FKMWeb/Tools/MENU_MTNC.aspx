<%@ Page Title="MENU MAINTENANCE" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="false"
    CodeFile="MENU_MTNC.aspx.cs" Inherits="Tools_MENU_MTNC" %>
    
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
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
        .Initial
        {
            display: block;
            padding: 4px 18px 4px 18px;
            float: left;
            color: Black;
            font-weight: bold;
        }
        .Initial:hover
        {
            color: Red;
        }
        .Clicked
        {
            float: left;
            display: block;
            padding: 4px 18px 4px 18px;
            color: Black;
            font-weight: bold;
            color: White;
        }
    </style>
</asp:Content>
<asp:Content ID="HeaderContent1" runat="server" ContentPlaceHolderID="ContenHeader">
    <table width="100%">
        <tr>
            <td align="center" style="font-size: large;">
                MENU MAINTENANCE
                <hr />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table width="100%" align="center">
        <tr>
            <td>
                <asp:Button Text="MENU ITEM" BorderStyle="None" ID="Tab1" CssClass="Initial" runat="server"
                    OnClick="Tab1_Click" />
                <asp:Button Text="MENU ACCESS" BorderStyle="None" ID="Tab2" CssClass="Initial"
                    runat="server" OnClick="Tab2_Click" />
                <asp:Button Text="DISCONTINUED 2" BorderStyle="None" ID="Tab3" CssClass="Initial"
                    runat="server" OnClick="Tab3_Click" />
                <asp:MultiView ID="MainView" runat="server">
                    <asp:View ID="View1" runat="server">
                        <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <%-- <asp:Label ID="LBL_USR_REFSRL" runat="server" Text="SELECT"></asp:Label>--%>
                                            </td>
                                            <td>
                                                <%--<cc1:ComboBox ID="CMB_USR_REFSRL" runat="server" DataTextField="USR_NAME" DataValueField="USR_USERID"
                    DropDownStyle="DropDownList" AutoCompleteMode="SuggestAppend" Width="300px" AutoPostBack="true">
                </cc1:ComboBox>--%>
                                            </td>
                                            <td rowspan="20" valign="top">
                                                <div style="overflow-x: auto; overflow-y: auto; height: 400PX; width: 800PX;">
                                                    <%--  <asp:Button ID="Button1" runat="server" Text="DOWNLOAD REPORT" />--%>
                                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="MI_MENU_ID"
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
                                                            <asp:BoundField DataField="MI_MENU_ID" HeaderText="MI_MENU_ID" SortExpression="MI_MENU_ID"
                                                                ItemStyle-Width="60Px" />
                                                            <asp:BoundField DataField="MI_NAME" HeaderText="MI_NAME" SortExpression="MI_NAME"
                                                                ItemStyle-Width="120Px" />
                                                            <asp:BoundField DataField="MI_PARENT_ID" HeaderText="MI_PARENT_ID" SortExpression="MI_PARENT_ID"
                                                                ItemStyle-Width="120Px" />
                                                            <asp:BoundField DataField="MI_URL" HeaderText="MI_URL" SortExpression="MI_URL" ItemStyle-Width="60Px" />
                                                            <asp:BoundField DataField="MI_SEQ" HeaderText="MI_SEQ" SortExpression="MI_SEQ" ItemStyle-Width="60Px" />
                                                            <asp:BoundField DataField="MI_STATUS" HeaderText="MI_STATUS" SortExpression="MI_STATUS"
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
                                                <asp:Label ID="LBL_MI_NAME" runat="server" Text="NAME"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TXT_MI_NAME" runat="server" TextMode="MultiLine" Rows="2" MaxLength="50"
                                                    Width="200px" onchange="rmvqoutes(this)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LBL_MI_PARENT_ID" runat="server" Text="PARENT ID"></asp:Label>
                                            </td>
                                            <td>
                                                <%--<asp:TextBox ID="TXT_MI_PARENT_ID" runat="server" MaxLength="10" Width="200px" onchange="rmvqoutes(this)"></asp:TextBox>--%>
                                                <cc1:ComboBox ID="TXT_MI_PARENT_ID" runat="server" DataTextField="NAME" DataValueField="MI_MENU_ID"
                                                    DropDownStyle="DropDownList" AutoCompleteMode="SuggestAppend" Width="200px" AutoPostBack="true">
                                                </cc1:ComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LBL_MI_MENU_ID" runat="server" Text="MENU ID"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TXT_MI_MENU_ID" runat="server" MaxLength="10" Width="200px" AutoPostBack="true"
                                                    onchange="rmvqoutes(this)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LBL_MI_URL" runat="server" Text="URL"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TXT_MI_URL" runat="server" MaxLength="50" Width="200px" onchange="rmvqoutes(this)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LBL_MI_SEQ" runat="server" Text="SEQUENCE"></asp:Label>
                                            </td>
                                            <td>
                                                <%-- <asp:TextBox ID="TXT_MI_SEQ" runat="server" TextMode="MultiLine" Rows="4" Width="200px"
                    Visible="false" onchange="rmvqoutes(this)"></asp:TextBox>--%>
                                                <cc1:ComboBox ID="TXT_MI_SEQ" runat="server" DataTextField="USR_NAME" DataValueField="USR_USERID"
                                                    DropDownStyle="DropDownList" AutoCompleteMode="SuggestAppend" Width="200px" AutoPostBack="true">
                                                </cc1:ComboBox>
                                            </td>
                                        </tr>
                                       
                                        <tr>
                                            <td>
                                                <asp:Label ID="LBL_MI_DESC" runat="server" Text="NARRATION"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TXT_MI_DESC" runat="server" TextMode="MultiLine" Rows="5" Width="200px"
                                                    onchange="rmvqoutes(this)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LBL_MI_STATUS" runat="server" Text="STATUS"></asp:Label>
                                            </td>
                                            <td>
                                                <cc1:ComboBox ID="CMB_MI_STATUS" runat="server" DropDownStyle="DropDownList"  
                                                      AutoCompleteMode="SuggestAppend" Width="200px">
                                                    <asp:ListItem Selected="true" Text="ACTIVE" Value="A" />
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
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="View2" runat="server">
                        <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
        <tr>
            <td>
                <asp:Label ID="LBL_USR_REFSRL" runat="server" Text="SELECT"></asp:Label>
            </td>
            <td>
                <cc1:ComboBox ID="CMB_USR_REFSRL" runat="server" DataTextField="USR_NAME" DataValueField="USR_USERID"
                    DropDownStyle="DropDownList" AutoCompleteMode="SuggestAppend" Width="300px" AutoPostBack="true">
                </cc1:ComboBox>
            </td>
                            </tr> <tr>
            <td colspan="5">
                <asp:Menu ID="Menu1" runat="server" Font-Names="Tahoma" Width="100%" Border-BorderColor="White"
                                            BorderTop-BorderWidth="1px" Font-Size="Small" ForeColor="#32298A"
                                            SubMenuItemStyle-BackColor="#d2deec" BackColor="#d2deec" >
                </asp:Menu>
            </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                             
                                                <div style="overflow-x: auto; overflow-y: auto; height: 400PX; width: 800PX;">
                                                    <%--  <asp:Button ID="Button1" runat="server" Text="DOWNLOAD REPORT" />--%>
                                                    <asp:GridView ID="GridView_ALLMENU" runat="server" AutoGenerateColumns="False" DataKeyNames="MI_MENU_ID"
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
                                                            <asp:BoundField DataField="MI_MENU_ID" HeaderText="MI_MENU_ID" SortExpression="MI_MENU_ID"
                                                                ItemStyle-Width="60Px" />
                                                            <asp:BoundField DataField="MI_NAME" HeaderText="MI_NAME" SortExpression="MI_NAME"
                                                                ItemStyle-Width="120Px" />
                                                            <asp:BoundField DataField="MI_PARENT_ID" HeaderText="MI_PARENT_ID" SortExpression="MI_PARENT_ID"
                                                                ItemStyle-Width="120Px" />
                                                            <asp:BoundField DataField="MI_URL" HeaderText="MI_URL" SortExpression="MI_URL" ItemStyle-Width="60Px" />
                                                            <asp:BoundField DataField="MI_SEQ" HeaderText="MI_SEQ" SortExpression="MI_SEQ" ItemStyle-Width="60Px" />
                                                            <asp:BoundField DataField="MI_STATUS" HeaderText="MI_STATUS" SortExpression="MI_STATUS"
                                                                ItemStyle-Width="60Px" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            
                                </td>
                                            <td align="center" valign="middle">
                                                <asp:Button ID="BTN_SUB" runat="server" Text="SUBMIT" />
                                            </td>
                                 <td>
                             
                                                <div style="overflow-x: auto; overflow-y: auto; height: 400PX; width: 800PX;">
                                                    <%--  <asp:Button ID="Button1" runat="server" Text="DOWNLOAD REPORT" />--%>
                                                    <asp:GridView ID="GridView_USERMENU" runat="server" AutoGenerateColumns="False" DataKeyNames="MI_MENU_ID"
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
                                                            <asp:BoundField DataField="MI_MENU_ID" HeaderText="MI_MENU_ID" SortExpression="MI_MENU_ID"
                                                                ItemStyle-Width="60Px" />
                                                            <asp:BoundField DataField="MI_NAME" HeaderText="MI_NAME" SortExpression="MI_NAME"
                                                                ItemStyle-Width="120Px" />
                                                            <asp:BoundField DataField="MI_PARENT_ID" HeaderText="MI_PARENT_ID" SortExpression="MI_PARENT_ID"
                                                                ItemStyle-Width="120Px" />
                                                            <asp:BoundField DataField="MI_URL" HeaderText="MI_URL" SortExpression="MI_URL" ItemStyle-Width="60Px" />
                                                            <asp:BoundField DataField="MI_SEQ" HeaderText="MI_SEQ" SortExpression="MI_SEQ" ItemStyle-Width="60Px" />
                                                            <asp:BoundField DataField="MI_STATUS" HeaderText="MI_STATUS" SortExpression="MI_STATUS"
                                                                ItemStyle-Width="60Px" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="View3" runat="server">
                        <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                            <tr>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
            </td>
        </tr>
    </table>
</asp:Content>
