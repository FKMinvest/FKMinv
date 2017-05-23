<%@ Page Title="BANK MAINTENANCE" Language="C#" MasterPageFile="~/MainPage.Master"
    AutoEventWireup="false" CodeFile="BANK_MTNC.aspx.cs" Inherits="Tools_BANK_MTNC" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="HeaderContent1" runat="server" ContentPlaceHolderID="ContenHeader">
    <table width="100%">
        <tr>
            <td align="center" style="font-size: large;">
                BANK MAINTENANCE
                <hr />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table>
        <tr>
            <td valign="top">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_BNK_REFSRL" runat="server" Text="CODE"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_BNK_REFSRL" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                            <cc1:ComboBox ID="CMB_BNK_REFSRL" runat="server" DataTextField="BNK_NAME" DataValueField="BNK_REFSRL"
                                DropDownStyle="DropDownList" AutoCompleteMode="SuggestAppend" Width="400px" AutoPostBack="true">
                            </cc1:ComboBox>
                            <%-- <asp:SqlDataSource ID="CMB_BNK_REFSRL1" runat="server" ConnectionString="<%$ ConnectionStrings:FKMCONNSTR %>"
                    SelectCommand="SELECT DISTINCT [BNK_REFSRL], [BNK_NAME]  FROM [BANK_INFO]  ORDER BY [BNK_REFSRL]">
                </asp:SqlDataSource>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_BNK_TYPE" runat="server" Text="ACCOUNT TYPE"></asp:Label>
                        </td>
                        <td>
                            <cc1:ComboBox ID="CMB_BNK_TYPE" runat="server" DropDownStyle="DropDownList"  
                                  AutoCompleteMode="SuggestAppend" Width="200px">
                                <asp:ListItem Selected="true" Text="BANK" Value="B" />
                                <asp:ListItem Text="INVESTOR" Value="I" />
                                <asp:ListItem Text="HOLDING" Value="H" />
                                <asp:ListItem Text="PETTY CASH" Value="P" />
                            </cc1:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_BNK_NAME" runat="server" Text="BANK NAME"></asp:Label>
                        </td>
                        <td class="style1">
                            <cc1:ComboBox ID="CMB_BNK_NAME" runat="server" Width="200px" DataSourceID="BNK_NAME"
                                DataTextField="BNK_NAME" DataValueField="BNK_NAME" DropDownStyle="DropDown" AutoPostBack="true"
                                    AutoCompleteMode="SuggestAppend">
                            </cc1:ComboBox>
                            <asp:SqlDataSource ID="BNK_NAME" runat="server" ConnectionString="<%$ ConnectionStrings:FKMCONNSTR %>"
                                SelectCommand="SELECT DISTINCT [BNK_NAME] FROM [BANK_INFO]   ORDER BY [BNK_NAME]">
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%--<asp:Label ID="Label1" runat="server" Text="BANK NAME"></asp:Label>--%>
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_BNK_NAME" runat="server" TextMode="MultiLine" Rows="4" MaxLength="80"
                                Width="200px" onchange="rmvqoutes(this)"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_BNK_DESC" runat="server" Text="ACCOUNT NAME"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_BNK_DESC" runat="server" TextMode="MultiLine" Rows="8" Width="200px"
                                onchange="rmvqoutes(this)"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_BNK_ACC_NO" runat="server" Text="ACCOUNT NUMBER"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_BNK_ACC_NO" runat="server" TextMode="MultiLine" Rows="2" MaxLength="50"
                                Width="200px" onchange="rmvqoutes(this)"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_BNK_IBAN_NO" runat="server" Text="IBAN NUMBER"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_BNK_IBAN_NO" runat="server" TextMode="MultiLine" Rows="2" MaxLength="50"
                                Width="200px" onchange="rmvqoutes(this)"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_BNK_CURRCD" runat="server" Text="CURRENCY"></asp:Label>
                        </td>
                        <%--   <td>
                <cc1:ComboBox   ID="CMB_BNK_CURRCD" runat="server" DropDownStyle="Simple"
                        AutoCompleteMode="SuggestAppend"
                    Width="200px" >
                    <asp:ListItem Selected="true" Text="KWD" Value="KWD" />
                    <asp:ListItem Text="US DOLLAR" Value="US DOLLAR" />
                    <asp:ListItem Text="UK POUND" Value="UK POUND" />
                    <asp:ListItem Text="EURO" Value="EURO" />
                </cc1:ComboBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_CURR" runat="server" Text="CURRENCY"></asp:Label>
            </td>--%>
                        <td class="style1">
                            <cc1:ComboBox ID="CMB_BNK_CURRCD" runat="server" Width="200px" DataSourceID="BNK_CURR"
                                DataTextField="RF_DESCRP" DataValueField="RF_DESCRP" DropDownStyle="DropDownList"
                                AutoCompleteMode="SuggestAppend">
                            </cc1:ComboBox>
                            <asp:SqlDataSource ID="BNK_CURR" runat="server" ConnectionString="<%$ ConnectionStrings:FKMCONNSTR %>"
                                SelectCommand="SELECT DISTINCT RTRIM([RF_DESCRP]) AS [RF_DESCRP] FROM [FKM_REFRNC] WHERE ([RF_FEILDTYPE] = @RF_FEILDTYPE) ORDER BY [RF_DESCRP]">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="BNK_CURR" Name="RF_FEILDTYPE" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_BNK_OPN_BALANCE" runat="server" Text="OPENING AMOUNT" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_BNK_OPN_BALANCE" runat="server" Width="200px" Visible="false"
                                onchange="numformat(this)"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_BNK_BALANCE_AMT" runat="server" Text="BALANCE AMOUNT"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_BNK_BALANCE_AMT" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_BNK_ADDRESS" runat="server" Text="ADDRESS"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_BNK_ADDRESS" runat="server" TextMode="MultiLine" Rows="4" Width="200px"
                                onchange="rmvqoutes(this)"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_BNK_TELEPHONE" runat="server" Text="TELEPHONE" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_BNK_TELEPHONE" runat="server" MaxLength="10" Width="200px" Visible="false"
                                onchange="rmvqoutes(this)"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_BNK_WEBSITE" runat="server" Text="BANK URL" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_BNK_WEBSITE" runat="server" TextMode="MultiLine" Rows="3" MaxLength="100"
                                Width="200px" Visible="false" onchange="rmvqoutes(this)"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_BNK_LST_CDT" runat="server" Text="LAST CREDITED ON" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_BNK_LST_CDT" runat="server" MaxLength="10" Width="200px" ReadOnly="true"
                                Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_BNK_LST_DDT" runat="server" Text="LAST DEBITED ON" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_BNK_LST_DDT" runat="server" MaxLength="10" Width="200px" Visible="false"
                                ReadOnly="true"></asp:TextBox>
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
            <td valign="top">
                <table>
                    <tr>
                        <td>
                            <asp:CheckBox ID="CHBX_EDIT" runat="server" Text="EDIT" AutoPostBack="true" Width="80PX" />
                        </td>
                        <td>
                                <asp:Button ID="Button1" runat="server" Text="DOWNLOAD REPORT" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" valign="top" align="right"> 
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="BNK_REFSRL"
                                    RowStyle-Wrap="false" HeaderStyle-BackColor="#0071ac" HeaderStyle-ForeColor="WhiteSmoke"
                                    RowStyle-ForeColor="Black" 
                                    RowStyle-Font-Size="Small" RowStyle-Height="25px" RowStyle-Font-Bold="true">
                                    <Columns>
                <asp:CommandField ShowSelectButton="true" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkRow" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="BNK_REFSRL" HeaderText="REF SRL" SortExpression="BNK_REFSRL"
                                            ItemStyle-Width="60Px" />
                                        <%--  <asp:BoundField DataField="BTRX_ISSDT" HeaderText="ISSUE DATE" 
                SortExpression="BTRX_ISSDT" ItemStyle-Width="100Px" />--%>
                                        <%-- <asp:BoundField DataField="BNK_TYPE" HeaderText="TRX TYPE" 
                SortExpression="BNK_TYPE" ItemStyle-Width="50Px"/>--%>
                                        <%-- <asp:BoundField DataField="BTRX_CD_TYPE" HeaderText="Cr./Dr." 
                SortExpression="BTRX_CD_TYPE" ItemStyle-Width="50Px"/>    --%>
                                        <asp:HyperLinkField DataNavigateUrlFields="BNK_REFSRL,BNK_NAME" HeaderText="BANK_NAME"
                                            DataNavigateUrlFormatString="BANK_MTNC.aspx?REFSRL={0}" Target="_blank" DataTextField="BNK_NAME" />
                                        <asp:BoundField DataField="BNK_ACCOUNT_NO" HeaderText="ACCOUNT" SortExpression="BNK_ACCOUNT_NO"
                                            ItemStyle-Width="60Px" />
                                        <asp:BoundField DataField="BNK_IBAN_NO" HeaderText="IBAN NO" SortExpression="BNK_IBAN_NO"
                                            ItemStyle-Width="60Px" />
                                        <asp:BoundField DataField="BNK_BALANCE_AMT_R" HeaderText="BAL AMT" SortExpression="BNK_BALANCE_AMT_R"
                                            ItemStyle-Width="60Px" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="BNK_DESC" HeaderText="DESCRIPTION" SortExpression="BNK_DESC"
                                            ItemStyle-Width="100Px" />
                                        <%-- <asp:BoundField DataField="BTRX_VALUEAMTD" HeaderText="DEBIT AMT" 
                SortExpression="BTRX_VALUEAMTD" ItemStyle-Width="60Px" />
            <asp:BoundField DataField="BTRX_UPDBY" HeaderText="UPDBY" 
                SortExpression="BTRX_UPDBY" ItemStyle-Width="50Px" />
            <asp:BoundField DataField="BTRX_UPDDATE" HeaderText="UPD DATE" 
                SortExpression="BTRX_UPDDATE" ItemStyle-Width="100Px" />--%>
                                        <%--
             <asp:HyperLinkField DataNavigateUrlFields="BTRX_REFSRL" HeaderText="DOWNLOAD" 
             DataNavigateUrlFormatString="BANK_TRX_LIST.aspx?REFSRL={0}" Target="_blank" DataTextField="BTRX_REFSRL" />--%>
                                    </Columns>
                                </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
