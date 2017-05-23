<%@ Page Title="REFERENCE CODE" Language="C#" MasterPageFile="~/MainPage.Master"
    AutoEventWireup="false" CodeFile="REF_EDIT.aspx.cs" Inherits="Tools_REF_EDIT" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="HeaderContent1" runat="server" ContentPlaceHolderID="ContenHeader">
    <table width="100%">
        <tr>
            <td align="center" style="font-size: large;">
                REFERENCE MAINTENANCE (ADD NEW / EDIT)
                <hr />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <%--   <h2>
        Welcome to ASP.NET!
    </h2>--%>
    <table>
        <tr>
            <td valign="top">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_CODE" runat="server" Text="CODE"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_CODE" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                            <cc1:ComboBox ID="CMB_CODE" runat="server" DataSourceID="SqlDataSource1" DataTextField="RF_CODE"
                                DataValueField="RF_CODE" DropDownStyle="DropDownList" AutoCompleteMode="SuggestAppend"
                                Width="200px" AutoPostBack="true">
                            </cc1:ComboBox>
                        </td>
                        <td>
                            <asp:CheckBox ID="CHBX_EDIT" runat="server" Text="EDIT" AutoPostBack="true" />
                        </td> 
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_CDTYP" runat="server" Text="CODE TYPE"></asp:Label>
                        </td>
                        <td>
                            <cc1:ComboBox ID="CMB_CDTYP" runat="server" DropDownStyle="DropDownList" AutoCompleteMode="SuggestAppend"
                                Width="200px">
                                <%-- <asp:ListItem Selected="true" Text="" Value="#" />--%>
                                <asp:ListItem Text="COMBO BOX COLLECTION" Value="C" />
                                <asp:ListItem Text="KEYWORD COLLECTION" Value="K" />
                            </cc1:ComboBox>
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
                            <cc1:ComboBox ID="CMB_FLDTYP" runat="server" AutoPostBack="true" DropDownStyle="DropDownList"
                                AutoCompleteMode="SuggestAppend" Width="200px">
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
                                <asp:ListItem Text="COMP_CD" Value="COMP_CD" />
                                <asp:ListItem Text="BNK_CURR" Value="BNK_CURR" />
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
                            <asp:Label ID="LBL_RF_CODE" runat="server" Text="CODE"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_RF_CODE" runat="server" MaxLength="10" Width="200px" onchange="rmvqoutes(this)"></asp:TextBox>
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
                            <asp:TextBox ID="TXT_DESC" runat="server" TextMode="MultiLine" Rows="3" MaxLength="50"
                                Width="200px" onchange="rmvqoutes(this)"></asp:TextBox>
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
                            <asp:TextBox ID="TXT_REMX" runat="server" TextMode="MultiLine" MaxLength="50" Rows="3"
                                Width="200px" onchange="rmvqoutes(this)"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="msgbox" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="BTN_ADD" runat="server" Text="SAVE" />
                            <asp:Button ID="BTN_UPDT" runat="server" Text="UPDATE" />
                            <asp:Button ID="BTN_DELETE" runat="server" Text="DELETE" />
                        </td>
                        <td>
                            <asp:Button ID="BTN_CLEAR" runat="server" Text="CLEAR" />
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <asp:TextBox ID="TXT_SRCH" runat="server" Width="200px"></asp:TextBox>
                <asp:Button ID="Button1" runat="server" Text="DOWNLOAD REPORT" />
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="RF_CODE"
                    RowStyle-Wrap="false" HeaderStyle-BackColor="#0071ac" HeaderStyle-ForeColor="WhiteSmoke"
                    RowStyle-ForeColor="Black" RowStyle-Font-Size="Small" RowStyle-Height="25px"
                    RowStyle-Font-Bold="true">
                    <Columns>
                        <asp:CommandField ShowSelectButton="true" />
                        <asp:BoundField DataField="RF_CODE" HeaderText="CODE" SortExpression="RF_CODE" ItemStyle-Width="100Px" />
                        <asp:BoundField DataField="RF_DESCRP" HeaderText="RF_DESCRP" SortExpression="RF_DESCRP"
                            ItemStyle-Width="200Px" ItemStyle-Wrap="true" />
                        <asp:BoundField DataField="RF_FEILDTYPE" HeaderText="FEILDTYPE" SortExpression="RF_FEILDTYPE"
                            ItemStyle-Width="100Px" />
                        <asp:BoundField DataField="RF_CODETYPE" HeaderText="CODETYPE" SortExpression="RF_CODETYPE"
                            ItemStyle-Width="50Px" />
                        <asp:BoundField DataField="RF_UPDBY" HeaderText="UPDBY" SortExpression="RF_UPDBY"
                            ItemStyle-Width="50Px" />
                        <%--
            <asp:BoundField DataField="RF_UPDDATE" HeaderText="RF_UPDDATE" 
                SortExpression="RF_UPDDATE" ItemStyle-Width="100Px" />--%>
                        <%-- <asp:BoundField DataField="RF_UPDTIME" HeaderText="RF_UPDTIME" 
                SortExpression="RF_UPDTIME" ItemStyle-Width="100Px" />--%>
                        <asp:CommandField ShowDeleteButton="true" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FKMCONNSTR %>"
        SelectCommand="SELECT DISTINCT [RF_CODE] FROM [FKM_REFRNC]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:FKMCONNSTR %>"
        SelectCommand="SELECT [RF_CODE], [RF_CODETYPE], [RF_DESCRP], [RF_FEILDTYPE], [RF_UPDBY] FROM [FKM_REFRNC] WHERE (([RF_CODE] LIKE '%' + @RF_CODE + '%') OR ([RF_CODETYPE] LIKE '%' + @RF_CODETYPE + '%') OR ([RF_DESCRP] LIKE '%' + @RF_DESCRP + '%') OR ([RF_FEILDTYPE] LIKE '%' + @RF_FEILDTYPE + '%') OR ([RF_UPDBY] LIKE '%' + @RF_UPDBY + '%') OR ([RF_FEILDTYPE] LIKE '%' + @RF_FEILDTYPE2 + '%')) order by [RF_FEILDTYPE] , [RF_CODE] ">
        <SelectParameters>
            <asp:ControlParameter ControlID="TXT_SRCH" Name="RF_CODE" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="TXT_SRCH" Name="RF_CODETYPE" PropertyName="Text"
                Type="String" />
            <asp:ControlParameter ControlID="TXT_SRCH" Name="RF_DESCRP" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="TXT_SRCH" Name="RF_FEILDTYPE" PropertyName="Text"
                Type="String" />
            <asp:ControlParameter ControlID="TXT_SRCH" Name="RF_UPDBY" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="CMB_FLDTYP" Name="RF_FEILDTYPE2" PropertyName="SelectedValue"
                Type="String" DefaultValue="#" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
