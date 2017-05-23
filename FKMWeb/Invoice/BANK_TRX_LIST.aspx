<%@ Page Title="BANK TRANSACTION LIST" Language="C#" MasterPageFile="~/MainPage.Master"
    AutoEventWireup="false" CodeFile="BANK_TRX_LIST.aspx.cs" Inherits="Invoice_BANK_TRX_LIST" %>

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
                <asp:Label ID="LBL_BTRXTYP" runat="server" Text="INVOICE TYPE"></asp:Label>
            </td>
            <td>
                <cc1:ComboBox   ID="CMB_BTRXTYP" runat="server" DropDownStyle="Simple"
                        AutoCompleteMode="SuggestAppend"
                    Width="200px" AutoPostBack="true" >
                    <asp:ListItem Selected="true" Text="ALL" Value="%" />
                  <%--  <asp:ListItem   Text="CAPITAL CALL" Value="CAP" />
                    <asp:ListItem Text="EXPENSE" Value="EXP" />
                    <asp:ListItem Text="INCOME" Value="INC" />
                    <asp:ListItem Text="REDEMPTION" Value="RDM" />--%>
                    <asp:ListItem Text="HOLDING COMPANY" Value="HOLD" />
                    <asp:ListItem Text="BANK BALANCE" Value="BANK" />
                    <asp:ListItem Text="PETTY CASH" Value="PETTY" />
                </cc1:ComboBox>
            </td>
           
            <td>
                <asp:Label ID="LBL_BTRX_DEBIT_NAME" runat="server" Text="BANK NAME"></asp:Label>
            </td>
            <td>
                <cc1:ComboBox   ID="CMB_BTRX_DEBIT_NAME" runat="server" DataSourceID="CMB_BTRX_DEBIT_NAME1"
                    DataTextField="BNK_NAME" DataValueField="BNK_REFSRL" DropDownStyle="DropDownList"
                    AutoCompleteMode="SuggestAppend" Width="200px" AutoPostBack="true">
                </cc1:ComboBox>
                <asp:SqlDataSource ID="CMB_BTRX_DEBIT_NAME1" runat="server" ConnectionString="<%$ ConnectionStrings:FKMCONNSTR %>"
                    SelectCommand="SELECT DISTINCT [BNK_REFSRL], [BNK_NAME]  FROM [BANK_INFO]  WHERE [BNK_TYPE] IN ('B','I','H','P')  ORDER BY [BNK_REFSRL]">
                </asp:SqlDataSource>
            </td>
            
        </tr>
        <tr>
          
            <td>
                <asp:Label ID="LBL_BTRXCDTYP" runat="server" Text="TRX TYPE"></asp:Label>
            </td>
            <td>
                <cc1:ComboBox   ID="CMB_BTRXCDTYP" runat="server" DropDownStyle="Simple"
                        AutoCompleteMode="SuggestAppend"
                    Width="200px"  AutoPostBack="true">
                    <asp:ListItem Selected="true" Text="BOTH" Value="%" />
                    <asp:ListItem Text="DEBIT" Value="DEBIT" />
                    <asp:ListItem Text="CREDIT" Value="CREDIT" />
                </cc1:ComboBox>
            </td>
            <td>
                <asp:Label ID="LBL_BTRX_CURRCD" runat="server" Text="CURRENCY"></asp:Label>
            </td>
            <td>
                <cc1:ComboBox   ID="CMB_BTRX_CURRCD" runat="server" DropDownStyle="Simple"
                        AutoCompleteMode="SuggestAppend"
                    Width="200px"  AutoPostBack="true">
                    <asp:ListItem Selected="true" Text="ALL" Value="%" />
                    <asp:ListItem Text="KWD" Value="KWD" />
                    <asp:ListItem Text="US DOLLAR" Value="US DOLLAR" />
                    <asp:ListItem Text="UK POUND" Value="UK POUND" />
                    <asp:ListItem Text="EURO" Value="EURO" />
                </cc1:ComboBox>
            </td>
        </tr>
         <tr>
        <td>
            <asp:Label ID="LBL_BTRX_ISSDT" runat="server" Text="ISSUE DATE"></asp:Label>
        </td>
        <td class="style1">
            <asp:TextBox ID="TXT_BTRX_ISSDT" runat="server" Width="200px"  AutoPostBack="true"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TXT_BTRX_ISSDT"
                Format="dd/MM/yyyy" />
        </td><td>
                <asp:Label ID="LBL_BTRX_VALUEDT" runat="server" Text="VALUE DATE"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox   ID="TXT_BTRX_VALUEDT" runat="server" Width="200px"  AutoPostBack="true"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TXT_BTRX_VALUEDT"
                    Format="dd/MM/yyyy" />
            </td>
            
            <td   colspan="5" align="right">
                <asp:Button ID="BTN_CLEAR" runat="server" Text="CLEAR" />
            </td>
        </tr>
        <tr>
            <td colspan="10" valign="top" align="right">  
             <asp:Button ID="Button1" runat="server" Text="DOWNLOAD REPORT" />
            <asp:GridView ID="GridView1" runat="server" 
            AutoGenerateColumns="False"    DataKeyNames="BTRX_REFSRL"
            RowStyle-Wrap="false" HeaderStyle-BackColor="#0071ac" HeaderStyle-ForeColor="WhiteSmoke"
            RowStyle-ForeColor="Black"     HeaderStyle-  RowStyle-Font-Size="Small"
            RowStyle-Height="25px"  RowStyle-Font-Bold="true" >
        <Columns> 
     <%--   <asp:TemplateField>
            <ItemTemplate>
                <asp:CheckBox ID="chkRow" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>--%>
             <asp:HyperLinkField DataNavigateUrlFields="BTRX_REFSRL" HeaderText="REF SRL" 
             DataNavigateUrlFormatString="BANK_TRX.aspx?REFSRL={0}" Target="_blank" DataTextField="BTRX_REFSRL" />
            <asp:BoundField DataField="BTRX_ISSDT" HeaderText="ISSUE DATE" 
                SortExpression="BTRX_ISSDT" ItemStyle-Width="100Px" />
            <asp:BoundField DataField="BTRX_TYPE" HeaderText="TRX TYPE" 
                SortExpression="BTRX_TYPE" ItemStyle-Width="50Px"/>
            <asp:BoundField DataField="BTRX_CD_TYPE" HeaderText="Cr./Dr." 
                SortExpression="BTRX_CD_TYPE" ItemStyle-Width="50Px"/>        
                
            <asp:BoundField DataField="BTRX_REFDESC" HeaderText="DESCRIPTION" 
                SortExpression="BTRX_REFDESC" ItemStyle-Width="100Px" />
            <asp:BoundField DataField="BTRX_BANK_NAME" HeaderText="BANK_NAME" 
                SortExpression="BTRX_DEBIT_NAME" ItemStyle-Width="60Px" />
            <asp:BoundField DataField="BTRX_BANK_ACC" HeaderText="ACCOUNT" 
                SortExpression="BTRX_BANK_ACC" ItemStyle-Width="60Px" />    
           <%-- <asp:BoundField DataField="BTRX_VALUEAMT" HeaderText="VALUE AMT" 
                SortExpression="BTRX_VALUEAMT" ItemStyle-Width="60Px" />--%>
            <asp:BoundField DataField="BTRX_VALUEAMTC" HeaderText="CREDIT AMT" 
                SortExpression="BTRX_VALUEAMTC" ItemStyle-Width="60Px" />
            <asp:BoundField DataField="BTRX_VALUEAMTD" HeaderText="DEBIT AMT" 
                SortExpression="BTRX_VALUEAMTD" ItemStyle-Width="60Px" />
            <asp:BoundField DataField="BTRX_UPDBY" HeaderText="UPDBY" 
                SortExpression="BTRX_UPDBY" ItemStyle-Width="50Px" />
            <asp:BoundField DataField="BTRX_UPDDATE" HeaderText="UPD DATE" 
                SortExpression="BTRX_UPDDATE" ItemStyle-Width="100Px" />
                
             <asp:HyperLinkField DataNavigateUrlFields="BTRX_REFSRL" HeaderText="DOWNLOAD" 
             DataNavigateUrlFormatString="BANK_TRX_LIST.aspx?REFSRL={0}" Target="_blank" DataTextField="BTRX_REFSRL" />
        </Columns>
    </asp:GridView> 
            </td>
        </tr>
    </table>
 
</asp:Content>
