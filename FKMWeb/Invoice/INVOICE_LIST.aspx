<%@ Page Title="INVOICE LIST" Language="C#" MasterPageFile="~/MainPage.Master"
    AutoEventWireup="false" CodeFile="INVOICE_LIST.aspx.cs" Inherits="Invoice_INVOICE_LIST" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="HeaderContent1" runat="server" ContentPlaceHolderID="ContenHeader">
    <table width="100%">
        <tr>
            <td align="center" style="font-size: large;">
                <asp:Label ID="LBL_CAPTION" runat="server"></asp:Label>
                <hr />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table>
        <tr>
            <td>
                <asp:Label ID="LBL_INVCTYP" runat="server" Text="INVOICE TYPE"></asp:Label>
            </td>
            <td>
                <cc1:ComboBox   ID="CMB_INVCTYP" runat="server" DropDownStyle="Simple"
                        AutoCompleteMode="SuggestAppend"
                    Width="200px" AutoPostBack="true" >
                    <asp:ListItem Selected="true" Text="ALL" Value="%" />
                    <asp:ListItem   Text="CAPITAL CALL" Value="CAP" />
                    <asp:ListItem Text="EXPENSE" Value="EXP" />
                    <asp:ListItem Text="INCOME" Value="INC" />
                    <asp:ListItem Text="REDEMPTION" Value="RDM" />
                    <asp:ListItem Text="DISTRIBUTION" Value="DST" />
                <%--    <asp:ListItem Text="HOLDING COMPANY" Value="HOLD" />
                    <asp:ListItem Text="BANK BALANCE" Value="BANK" />
                    <asp:ListItem Text="PETTY CASH" Value="PETTY" />--%>
                </cc1:ComboBox>
            </td>
           
            <td>
                <asp:Label ID="LBL_INVC_DEBIT_NAME" runat="server" Text="BANK NAME"></asp:Label>
            </td>
            <td>
                <cc1:ComboBox   ID="CMB_INVC_DEBIT_NAME" runat="server" DataSourceID="CMB_INVC_DEBIT_NAME1"
                    DataTextField="BNK_NAME" DataValueField="BNK_REFSRL" DropDownStyle="DropDownList"
                    AutoCompleteMode="SuggestAppend" Width="200px" AutoPostBack="true">
                </cc1:ComboBox>
                <asp:SqlDataSource ID="CMB_INVC_DEBIT_NAME1" runat="server" ConnectionString="<%$ ConnectionStrings:FKMCONNSTR %>"
                    SelectCommand="SELECT DISTINCT [BNK_REFSRL], RTRIM([BNK_NAME])+ ' | ' + RTRIM([BNK_ACCOUNT_NO]) AS [BNK_NAME]  FROM [BANK_INFO]  WHERE [BNK_TYPE] IN ('B','I' )  ORDER BY [BNK_REFSRL]">
                </asp:SqlDataSource>
            </td>
            
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_FKM_CD" runat="server" Text="FKM CODE"></asp:Label>
            </td>
            <td class="style1">
                <cc1:ComboBox   ID="CMB_FKM_CD" runat="server"
                    DataTextField="FKM_PROJNAME" DataValueField="FKM_SRL" DropDownStyle="DropDownList"
                    AutoCompleteMode="SuggestAppend" Width="200px" AutoPostBack="true">
                </cc1:ComboBox><%--
                <asp:SqlDataSource ID="FKM_SRL" runat="server" ConnectionString="<%$ ConnectionStrings:FKMCONNSTR %>"
                    SelectCommand="SELECT DISTINCT [FKM_SRL] ,[FKM_SRL] + '-' + [FKM_PROJNAME] AS [FKM_PROJNAME]  FROM [INVEST_INFO] ORDER BY [FKM_SRL]">
                </asp:SqlDataSource>--%>
            </td>
            <td>
                <asp:Label ID="LBL_INVC_CURRCD" runat="server" Text="CURRENCY"></asp:Label>
            </td>
            <td>
                <cc1:ComboBox   ID="CMB_INVC_CURRCD" runat="server" DropDownStyle="Simple"
                        AutoCompleteMode="SuggestAppend"
                    Width="200px"  AutoPostBack="true">
                    <asp:ListItem Selected="true" Text="ALL" Value="%" />
                    <asp:ListItem Text="US DOLLAR" Value="US DOLLAR" />
                    <asp:ListItem Text="UK POUND" Value="UK POUND" />
                    <asp:ListItem Text="EURO" Value="EURO" />
                    <asp:ListItem  Text="KWD" Value="KWD" />
                </cc1:ComboBox>
            </td>
        </tr>
        <tr>
        <td>
            <asp:Label ID="LBL_INVC_ISSDT" runat="server" Text="ISSUE DATE"></asp:Label>
        </td>
        <td class="style1">
            <asp:TextBox ID="TXT_INVC_ISSDT" runat="server" Width="200px"  AutoPostBack="true"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TXT_INVC_ISSDT"
                Format="dd/MM/yyyy" />
        </td><td>
                <asp:Label ID="LBL_INVC_VALUEDT" runat="server" Text="VALUE DATE"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox   ID="TXT_INVC_VALUEDT" runat="server" Width="200px"  AutoPostBack="true"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TXT_INVC_VALUEDT"
                    Format="dd/MM/yyyy" />
            </td>
            
            <td   colspan="1" align="right">
                <asp:Button ID="BTN_CLEAR" runat="server" Text="CLEAR" />
            </td>
        </tr>
        <tr>
            <td colspan="10" valign="top" align="right"> 
            <asp:GridView ID="GridView1" runat="server" 
            AutoGenerateColumns="False"    DataKeyNames="INVC_REFSRL"  HeaderStyle-BackColor="#0071ac" HeaderStyle-ForeColor="WhiteSmoke"
            RowStyle-ForeColor="Black" RowStyle-Font-Size="Small"
            RowStyle-Height="25px"  RowStyle-Font-Bold="true" >
        <Columns>
             <asp:HyperLinkField DataNavigateUrlFields="INVC_REFSRL" HeaderText="REF SRL" 
             DataNavigateUrlFormatString="INVOICE_MTNC.aspx?REFSRL={0}" Target="_blank" DataTextField="INVC_REFSRL" />
            <asp:BoundField DataField="INVC_ISSDT" HeaderText="ISSUE DATE" 
                SortExpression="INVC_ISSDT" ItemStyle-Width="100Px" />
            <asp:BoundField DataField="INVC_TYPE" HeaderText="INVC TYPE" 
                SortExpression="INVC_TYPE" ItemStyle-Width="50Px"/>
            <asp:BoundField DataField="INVC_FKMREF" HeaderText="FKM REF" 
                SortExpression="INVC_FKMREF" ItemStyle-Width="60Px" />            
            <asp:BoundField DataField="INVC_NARR" HeaderText="NARRATION" 
                SortExpression="INVC_NARR" ItemStyle-Width="250Px" ItemStyle-Wrap="true" />
            <asp:BoundField DataField="INVC_REFDESC" HeaderText="DESCRIPTION" 
                SortExpression="INVC_REFDESC" ItemStyle-Width="100Px"  ItemStyle-Wrap="true"  />
            <asp:BoundField DataField="INVC_DEBIT_NAME" HeaderText="DEBIT_NAME" 
                SortExpression="INVC_DEBIT_NAME" ItemStyle-Width="150Px" />
            <asp:BoundField DataField="INVC_CREDIT_NAME" HeaderText="INVC_CREDIT_NAME" 
                SortExpression="INVC_CREDIT_NAME" ItemStyle-Width="150Px" /> 
            <asp:BoundField DataField="INVC_VALUEAMT" HeaderText="VALUE AMT" 
                SortExpression="INVC_VALUEAMT" ItemStyle-Width="60Px" />
            <asp:BoundField DataField="INVC_UPDBY" HeaderText="UPDBY" 
                SortExpression="INVC_UPDBY" ItemStyle-Width="50Px" />
            <asp:BoundField DataField="INVC_UPDDATE" HeaderText="UPD DATE" 
                SortExpression="INVC_UPDDATE" ItemStyle-Width="100Px" />
                
             <asp:HyperLinkField DataNavigateUrlFields="INVC_REFSRL" HeaderText="DOWNLOAD" 
             DataNavigateUrlFormatString="INVOICE_LIST.aspx?REFSRL={0}" Target="_blank" DataTextField="INVC_REFSRL" />
        </Columns>
    </asp:GridView> 
            </td>
        </tr>
    </table>
 
</asp:Content>
