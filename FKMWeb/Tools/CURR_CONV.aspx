<%@ Page Title=" CURRENCY CONVERTER"  Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="false"
    CodeFile="CURR_CONV.aspx.cs" Inherits="Tools_CURR_CONV" %>
    

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

</asp:Content>
<asp:Content ID="HeaderContent1" runat="server" ContentPlaceHolderID="ContenHeader">
    <table width="100%">
        <tr>
            <td align="center" style="font-size:large;" >
                
                    CURRENCY CONVERTER
               <hr />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<iframe id="tmccc" src="http://themoneyconverter.com/CurrencyConverter.aspx?tab=0&amp;from=USD&amp;to=GBP&amp;amount=1&amp;bg=ffffff" style="width:251px; height:348px; border: none;" scrolling="no" marginwidth="0" marginheight="0"></iframe>
    <%--<table>
        <tr>
            <td>
                <asp:Label ID="LBL_CDTYP" runat="server" Text="CODE TYPE"></asp:Label>
            </td>
            <td>
                
                   
                <cc1:ComboBox    ID="CMB_CDTYP" runat="server" 
                DropDownStyle="DropDownList"        AutoCompleteMode="SuggestAppend" Width="200px"  >
               
           
                    <asp:ListItem Text="COMBO BOX COLLECTION" Value="C" />
                    <asp:ListItem Text="KEYWORD COLLECTION" Value="K" />
                    </cc1:ComboBox>
            </td>
            <td rowspan="10" valign="top"  align="right" >
    <asp:TextBox   ID="TXT_SRCH" runat="server"   Width="200px" ></asp:TextBox>
                <asp:Button ID="BTNSRCH" runat="server" Text="SEARCH"  />
             <div style="overflow-x:auto; overflow-y:auto; height:350px; width:750px;">
            <asp:GridView ID="GridView1" runat="server" 
    AutoGenerateColumns="False" DataSourceID="SqlDataSource2"  DataKeyNames="RF_CODE"
            RowStyle-Wrap="false" HeaderStyle-BackColor="#0071ac" HeaderStyle-ForeColor="WhiteSmoke"
            RowStyle-ForeColor="Black"     HeaderStyle-  RowStyle-Font-Size="Small"
            RowStyle-Height="25px"  RowStyle-Font-Bold="true" >
        <Columns>
            <asp:BoundField DataField="RF_CODE" HeaderText="CODE" 
                SortExpression="RF_CODE" ItemStyle-Width="100Px" />
            <asp:BoundField DataField="RF_CODETYPE" HeaderText="CODETYPE" 
                SortExpression="RF_CODETYPE" ItemStyle-Width="50Px"/>
            <asp:BoundField DataField="RF_DESCRP" HeaderText="RF_DESCRP" 
                SortExpression="RF_DESCRP" ItemStyle-Width="200Px" />
            <asp:BoundField DataField="RF_FEILDTYPE" HeaderText="FEILDTYPE" 
                SortExpression="RF_FEILDTYPE" ItemStyle-Width="100Px" />
            <asp:BoundField DataField="RF_UPDBY" HeaderText="UPDBY" 
                SortExpression="RF_UPDBY" ItemStyle-Width="50Px" />
            
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
                <asp:Label ID="LBL_FLDTYP" runat="server" Text="FEILD"></asp:Label>
            </td>
            <td>
                <cc1:ComboBox    ID="CMB_FLDTYP" runat="server" 
                DropDownStyle="DropDownList"        AutoCompleteMode="SuggestAppend" Width="200px"  >
                    <asp:ListItem Selected="true" Text="" Value="#" />
                    <asp:ListItem Text="FKM_CURR" Value="FKM_CURR" />
                    <asp:ListItem Text="FKM_PROJNAME" Value="FKM_PROJNAME" />
                    <asp:ListItem Text="FKM_HOLDNAME" Value="FKM_HOLDNAME" />
                    <asp:ListItem Text="FKM_LOCATION" Value="FKM_LOCATION" />
                    <asp:ListItem Text="FKM_OPPRBY" Value="FKM_OPPRBY" />
                    <asp:ListItem Text="FKM_INVCOMP" Value="FKM_INVCOMP" />
                    <asp:ListItem Text="FKM_INVGRP" Value="FKM_INVGRP" />
                    <asp:ListItem Text="FKM_BANK" Value="FKM_BANK" />
                    <asp:ListItem Text="FKM_YEILDPRD" Value="FKM_YEILDPRD" />
                    <asp:ListItem Text="FKM_INCGEN" Value="FKM_INCGEN" />
                    <asp:ListItem Text="FKM_STATUS" Value="FKM_STATUS" />
                    <asp:ListItem Text="FKM_PRVTEQT" Value="FKM_PRVTEQT" />
                    <asp:ListItem Text="DOCUMENT" Value="DOCUMENT" />
                    <asp:ListItem Text="QUOTE" Value="QUOTE" />
                     </cc1:ComboBox>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_CODE" runat="server" Text="CODE"></asp:Label>
            </td>
            <td>
                <asp:TextBox   ID="TXT_CODE" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_DESC" runat="server" Text="DESCRIPTION"></asp:Label>
            </td>
            <td>
                <asp:TextBox   ID="TXT_DESC" runat="server" TextMode="MultiLine" Rows="3" MaxLength="50" Width="200px" onchange="rmvqoutes(this)" ></asp:TextBox>
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
            <td>
                <asp:TextBox   ID="TXT_REMX" runat="server" TextMode="MultiLine" Rows="3" MaxLength="50" Width="200px" onchange="rmvqoutes(this)" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="msgbox" runat="server"  ></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="BTN_ADD" runat="server" Text="SAVE" />
            </td>
            <td>
                <asp:Button ID="BTN_CLEAR" runat="server" Text="CLEAR" />
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FKMCONNSTR %>"
        SelectCommand="SELECT DISTINCT [RF_CODE] FROM [FKM_REFRNC]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
    ConnectionString="<%$ ConnectionStrings:FKMCONNSTR %>" 
    
        
        SelectCommand="SELECT [RF_CODE], [RF_CODETYPE], [RF_DESCRP], [RF_FEILDTYPE], [RF_UPDBY] FROM [FKM_REFRNC] WHERE (([RF_CODE] LIKE '%' + @RF_CODE + '%') OR ([RF_CODETYPE] LIKE '%' + @RF_CODETYPE + '%') OR ([RF_DESCRP] LIKE '%' + @RF_DESCRP + '%') OR ([RF_FEILDTYPE] LIKE '%' + @RF_FEILDTYPE + '%') OR ([RF_UPDBY] LIKE '%' + @RF_UPDBY + '%') OR ([RF_FEILDTYPE] LIKE '%' + @RF_FEILDTYPE2 + '%')) ORDER BY [RF_SRL] DESC">
        <SelectParameters>
            <asp:ControlParameter ControlID="TXT_SRCH" Name="RF_CODE" PropertyName="Text" 
                Type="String" />
            <asp:ControlParameter ControlID="TXT_SRCH" Name="RF_CODETYPE" PropertyName="Text" 
                Type="String" />
            <asp:ControlParameter ControlID="TXT_SRCH" Name="RF_DESCRP" 
                PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="TXT_SRCH" Name="RF_FEILDTYPE" PropertyName="Text" 
                Type="String" />
            <asp:ControlParameter ControlID="TXT_SRCH" Name="RF_UPDBY" PropertyName="Text" 
                Type="String" />
            <asp:ControlParameter ControlID="CMB_FLDTYP" Name="RF_FEILDTYPE2" 
                PropertyName="SelectedValue" Type="String" DefaultValue="#"  />
        </SelectParameters>
</asp:SqlDataSource>--%>
</asp:Content>
