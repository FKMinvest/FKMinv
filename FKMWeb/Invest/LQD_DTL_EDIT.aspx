<%@ Page Title="LIQUIDATED DETAIL" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="false"
    CodeFile="LQD_DTL_EDIT.aspx.cs" Inherits="INVEST_LQD_DTL_EDIT" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style1
        {
            width: 269px;
        }
    </style>
</asp:Content>
<asp:Content ID="HeaderContent1" runat="server" ContentPlaceHolderID="ContenHeader">
    <table width="100%">
        <tr>
            <td align="center" style="font-size: large;">
                VEIW/EDIT LIQUIDATED DETAIL
                <hr />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<table>
        <tr>
            <td valign="top">
    <table   >
        <tr>
            <td>
                <asp:Label ID="LBL_FKM_CD" runat="server" Text="FKM CODE"></asp:Label>
            </td>
            <td class="style1">
                <%--<asp:TextBox  ID="TXT_FKM_CD" runat="server" Width="200px"></asp:TextBox>--%>
                <cc1:ComboBox ID="CMB_FKM_CD" runat="server" AutoPostBack="true" DataTextField="FKM_PROJNAME"
                    DataValueField="FKM_SRL" DropDownStyle="DropDown"  
                      AutoCompleteMode="SuggestAppend">
                </cc1:ComboBox>
            </td> 
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_PROJNAME" runat="server" Text="PROJECT NAME"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox ID="TXT_PROJNAME" runat="server" Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_PRTCPDATE" runat="server" Text="PARTICIPATION DATE"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox ID="TXT_PRTCPDATE" runat="server" Width="100px"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TXT_PRTCPDATE"
                    Format="dd/MM/yyyy" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_LQDDATE" runat="server" Text="LIQUIDATED DATE"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox ID="TXT_LQDDATE" runat="server" Width="100px"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TXT_LQDDATE"
                    Format="dd/MM/yyyy" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_CURR" runat="server" Text="CURRENCY"></asp:Label>
            </td>
            <td class="style1">
                <cc1:ComboBox ID="CMB_CURR" runat="server" Width="200px" DataSourceID="FKM_CURR"
                    DataTextField="RF_DESCRP" DataValueField="RF_DESCRP" DropDownStyle="DropDownList"
                    AutoCompleteMode="SuggestAppend">
                </cc1:ComboBox>
                <asp:SqlDataSource ID="FKM_CURR" runat="server" ConnectionString="<%$ ConnectionStrings:FKMCONNSTR %>"
                    SelectCommand="SELECT DISTINCT RTRIM([RF_DESCRP]) AS [RF_DESCRP] FROM [FKM_REFRNC] WHERE ([RF_FEILDTYPE] = @RF_FEILDTYPE) ORDER BY [RF_DESCRP]">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="FKM_CURR" Name="RF_FEILDTYPE" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_ROI" runat="server" Text="RATE OF INTEREST"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox ID="TXT_ROI" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
            </td>
            <td class="style1">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ControlToValidate="TXT_ROI"
                    runat="server" ErrorMessage="Only  0.00 format allowed" ValidationExpression="^[0-9]+(\.[0-9]{1,2})?$"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_PRINCIPAL" runat="server" Text="PRINCIPAL"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox ID="TXT_PRINCIPAL" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_EXPENSE" runat="server" Text="EXPENSE"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox ID="TXT_EXPENSE" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_TOTCOST" runat="server" Text="TOTAL COST"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox ID="TXT_TOTCOST" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_INTEREST" runat="server" Text="INTEREST" Visible="true"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox ID="TXT_INTEREST" runat="server" Width="200px" Visible="true" onchange="numformat(this)"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_DVDND" runat="server" Text="DIVIDEND" Visible="true"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox ID="TXT_DVDND" runat="server" Width="200px" Visible="true" onchange="numformat(this)"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_CAPGN" runat="server" Text="CAPITAL GAIN"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox ID="TXT_CAPGN" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_LQDAMT" runat="server" Text="LIQUIDATE&nbsp;AMOUNT"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox ID="TXT_LQDAMT" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_AMOUNT" runat="server" Text="AMOUNT" Visible="false"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox ID="TXT_AMOUNT" runat="server" Width="200px" Visible="false" onchange="numformat(this)"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_REMX" runat="server" Text="REMARKS"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox ID="TXT_REMX" runat="server" TextMode="MultiLine" Rows="5" MaxLength="150"
                    Width="200px" onchange="rmvqoutes(this)"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_STATUS" runat="server" Text="STATUS"></asp:Label>
            </td>
            <td class="style1">
                <cc1:ComboBox ID="CMB_STATUS" runat="server" DropDownStyle="DropDown"  
                      AutoCompleteMode="SuggestAppend" Width="200px">
                    <asp:ListItem Text="ACTIVE" Value="Y" Selected="True" />
                    <asp:ListItem Text="INACTIVE" Value="N" />
                </cc1:ComboBox>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="BTN_ADD" runat="server" Text="SAVE" />
                <asp:Button ID="BTN_UPDT" runat="server" Text="UPDATE" />
            </td>
            <td class="style1">
                <asp:Button ID="BTN_CLEAR" runat="server" Text="CLEAR" />
            </td>
        </tr>
    </table>
            </td>
            <td rowspan="35" valign="top">
               <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="FKM_SRL"
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
                <asp:BoundField DataField="FKM_REG" HeaderText="REGISTERED" SortExpression="FKM_REG" />
                <asp:BoundField DataField="FKM_CURR" HeaderText="CURRRENCY" SortExpression="FKM_CURR"
                    ItemStyle-Width="100Px" />
                <%--   <asp:HyperLinkField DataNavigateUrlFields="INVC_REFSRL" HeaderText="DOWNLOAD" 
             DataNavigateUrlFormatString="INVOICE_LIST.aspx?REFSRL={0}" Target="_blank" DataTextField="INVC_REFSRL" />--%>
            </Columns>
        </asp:GridView>
                </td>
                </tr>
</table>
</asp:Content>
