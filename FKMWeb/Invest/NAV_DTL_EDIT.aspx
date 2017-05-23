<%@ Page Title="NAV DETAIL " Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="false"
    CodeFile="NAV_DTL_EDIT.aspx.cs" Inherits="INVEST_NAV_DTL_EDIT" %>

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
                VEIW/EDIT NAV DETAIL
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
                            <asp:Label ID="LBL_NAV_QTR" runat="server" Text="NAV QUARTER"></asp:Label>
                        </td>
                        <td class="style1">
                          
                            <cc1:ComboBox ID="CMB_NAV_QTR" runat="server" AutoPostBack="true" DataTextField="QUARTER"
                                DataValueField="CD" DropDownStyle="DropDown"   AutoCompleteMode="SuggestAppend">
                            </cc1:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_FKM_CD" runat="server" Text="FKM CODE"></asp:Label>
                        </td>
                        <td class="style1">
                             
                            <cc1:ComboBox ID="CMB_FKM_CD" runat="server" AutoPostBack="true" DataTextField="FKM_PROJNAME"
                                DataValueField="FKM_SRL" DropDownStyle="DropDown"   AutoCompleteMode="SuggestAppend">
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
                            <asp:Label ID="LBL_INVCOMP" runat="server" Text="INVEST CATEGORY"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_INVCOMP" runat="server" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_INVGRP" runat="server" Text="INVEST GROUP"></asp:Label>
                        </td>
                        <td class="style1">
                            <cc1:ComboBox ID="CMB_INVGRP" runat="server" Width="400px" DataSourceID="FKM_INVGRP"
                                DataTextField="RF_DESCRP" DataValueField="RF_DESCRP" DropDownStyle="DropDown"
                                  AutoCompleteMode="SuggestAppend">
                            </cc1:ComboBox>
                            <asp:SqlDataSource ID="FKM_INVGRP" runat="server" ConnectionString="<%$ ConnectionStrings:FKMCONNSTR %>"
                                SelectCommand="SELECT DISTINCT [RF_DESCRP] FROM [FKM_REFRNC] WHERE ([RF_FEILDTYPE] = @RF_FEILDTYPE) ORDER BY [RF_DESCRP]">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="FKM_INVGRP" Name="RF_FEILDTYPE" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
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
                            <asp:Label ID="LBL_MTRTDATE" runat="server" Text="MATURITY DATE"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_MTRTDATE" runat="server" Width="100px"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TXT_MTRTDATE"
                                Format="dd/MM/yyyy" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_CURR" runat="server" Text="CURRENCY"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_CURR" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_COMP_CD" runat="server" Text="REGISTERED"></asp:Label>
                        </td>
                        <td class="style1">
                            <cc1:ComboBox ID="CMB_COMP_CD" runat="server" DataSourceID="COMP_CD" DataTextField="RF_DESCRP"
                                DataValueField="RF_CODE" DropDownStyle="DropDownList"   Enabled="false"
                                Width="200px" ForeColor="Red">
                            </cc1:ComboBox>
                            <asp:SqlDataSource ID="COMP_CD" runat="server" ConnectionString="<%$ ConnectionStrings:FKMCONNSTR %>"
                                SelectCommand="SELECT DISTINCT RTRIM([RF_DESCRP]) AS [RF_DESCRP], RTRIM([RF_CODE]) AS [RF_CODE] FROM [FKM_REFRNC] WHERE ([RF_FEILDTYPE] = @RF_FEILDTYPE) ORDER BY [RF_DESCRP]">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="COMP_CD" Name="RF_FEILDTYPE" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_INCGEN" runat="server" Text="INCOME GENRATOR"></asp:Label>
                        </td>
                        <td class="style1">
                            <cc1:ComboBox ID="CMB_INCGEN" runat="server" DropDownStyle="DropDown"  
                                AutoCompleteMode="SuggestAppend" Width="200px">
                                <asp:ListItem Text="INCOME GENRATING" Value="Y" Selected="True" />
                                <asp:ListItem Text="NON-INCOME GENRATING" Value="N" />
                            </cc1:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_COMMCAP" runat="server" Text="COMMITTED CAPITAL"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_COMMCAP" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
                        </td>
                        <%--" ^ [0-9] +(\.[0-9]{1,3})?$"--%>
                        <td class="style1">
                            <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="TXT_COMMCAP"
                 runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="^[0-9]+"></asp:RegularExpressionValidator>
                            --%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_COMMCAP2" runat="server" Text="COMMITTED CAPITAL(2)"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_COMMCAP2" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="TXT_COMMCAP2"
                 runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="^[0-9]+"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_INVAMT" runat="server" Text="INVESTMENT AMOUNT"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_INVAMT" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="TXT_INVAMT"
                    runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="^[0-9]+"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_CAPPD" runat="server" Text="CAPITAL PAID" Visible="true"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_CAPPD" runat="server" Width="200px" Visible="true" onchange="numformat(this)"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="TXT_CAPPD"
                    runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="^[0-9]+"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_CAPUNPD" runat="server" Text="OUTSTANDING" Visible="true"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_CAPUNPD" runat="server" Width="200px" Visible="true" onchange="numformat(this)"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="TXT_CAPUNPD"
                    runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="^[0-9]+"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_ROI" runat="server" Text="RATE OF INTEREST (%)"></asp:Label>
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
                            <asp:Label ID="LBL_YEILDPRD" runat="server" Text="YEILD PREIOD"></asp:Label>
                        </td>
                        <td class="style1">
                            <cc1:ComboBox ID="CMB_YEILDPRD" runat="server" DropDownStyle="DropDown"  
                                AutoCompleteMode="SuggestAppend" Width="200px">
                                <asp:ListItem Text=" " Value=" " Selected="True" />
                                <asp:ListItem Text="MONTHLY" Value="M" />
                                <asp:ListItem Text="QUARTERLY" Value="Q" />
                                <asp:ListItem Text="SEMI ANNUALY" Value="S" />
                                <asp:ListItem Text="ANNUALY" Value="A" />
                            </cc1:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_MONYCAL" runat="server" Text="MONTHLY YEILD"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_MONYCAL" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator16" ControlToValidate="TXT_MONYCAL"
                    runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="^[0-9]+"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_QRTYCAL" runat="server" Text="QUARTERLY YEILD"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_QRTYCAL" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator17" ControlToValidate="TXT_QRTYCAL"
                    runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="^[0-9]+"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_SMANYCAL" runat="server" Text="SEMI ANNUAL YEILD"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_SMANYCAL" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <%--    <asp:RegularExpressionValidator ID="RegularExpressionValidator18" ControlToValidate="TXT_SMANYCAL"
                    runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="^[0-9]+"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_COMP_PRD" runat="server" Text="COMPUTING PERIOD"></asp:Label>
                        </td>
                        <td class="style1">
                            <cc1:ComboBox ID="CMB_COMP_PRD" runat="server" DropDownStyle="DropDown"  
                                AutoCompleteMode="SuggestAppend" Width="200px"> 
                                <asp:ListItem Text="MONTH" Value="M" />
                                <asp:ListItem Text="DAY" Value="D" /> 
                            </cc1:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_CONSIDER_CI" runat="server" Text="CONSIDER AMOUNT"></asp:Label>
                        </td>
                        <td class="style1">
                            <cc1:ComboBox ID="CMB_CONSIDER_CI" runat="server" DropDownStyle="DropDown"  
                                AutoCompleteMode="SuggestAppend" Width="200px"> 
                                <asp:ListItem Text="COMMITED AMOUNT" Value="C" />
                                <asp:ListItem Text="INVESTED AMOUNT" Value="I" /> 
                            </cc1:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_VALUATION_NEW" runat="server" Text="CONSIDER VALUATION"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_VALUATION_NEW" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ControlToValidate="TXT_VALVTN"
                    runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="^[0-9]+"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_LIBORRATE" runat="server" Text="LIBOR RATE"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_LIBORRATE" runat="server" Width="200px"  onchange="onlynumberKey(this)" ></asp:TextBox>
                            </td>
                        <td class="style1">
                           <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="TXT_LIBORRATE"
                                runat="server" ErrorMessage="Only  0.00 format allowed" ValidationExpression="^[0-9]+(\.[0-9]{1,4})?$"></asp:RegularExpressionValidato--%>
                        <%--</td>
                        </td>
                        <td class="style1" align="right" >--%>
                                                      <asp:Button ID="BTN_PROCESS" runat="server" Text="PROCESS" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_SNCINCP" runat="server" Text="ACTUAL&nbsp;RETURNS&nbsp;TILL&nbsp;DATE"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_SCNINCP" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator20" ControlToValidate="TXT_SCNINCP"
                    runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="^[0-9]+"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_ANLINCMCY" runat="server" Text="ANNUAL&nbsp;INTEREST&nbsp;INCOME ON&nbsp;CURRENT&nbsp;YIELD "></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_ANLINCMCY" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator23" ControlToValidate="TXT_ANLINCMCY"
                    runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="^[0-9]+"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_ANLRLZD" runat="server" Text="ANNUALIZED REALIZED"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_ANLRLZD" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator22" ControlToValidate="TXT_ANLRLZD"
                    runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="^[0-9]+"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_ACTLINCMRU" runat="server" Text="ACTUAL INCOME RECEIVED UPTO (%)"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_ACTLINCMRU" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator13" ControlToValidate="TXT_ACTLINCMRU"
                                runat="server" ErrorMessage="Only  0.00 format allowed" ValidationExpression="^[0-9]+(\.[0-9]{1,2})?$"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_UNRLYLD" runat="server" Text="UNREALIZED YIELD"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_UNRLYLD" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator24" ControlToValidate="TXT_UNRLYLD"
                    runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="^[0-9]+"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_DVDND" runat="server" Text="DIVIDEND"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_DVDND" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator10" ControlToValidate="TXT_DVDND"
                    runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="^[0-9]+"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_VALVTN" runat="server" Text="VALUATION"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_VALVTN" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator21" ControlToValidate="TXT_SALEPRCD"
                    runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="^[0-9]+"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_CAPGN" runat="server" Text="CAPITAL GAIN (LOSS)"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_CAPGN" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator12" ControlToValidate="TXT_CAPGN"
                    runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="^[0-9]+"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_UNRLDVD" runat="server" Text="UNREALIZED YIELD / DIVIDEND"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_UNRLDVD" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator26" ControlToValidate="TXT_UNRLDVD"
                    runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="^[0-9]+"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_UNRLDVDCAP" runat="server" Text="UNREALIZED YIELD / DIVIDEND / CAPITAL GAIN(LOSS)"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_UNRLDVDCAP" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator25" ControlToValidate="TXT_UNRLDVDCAP"
                    runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="^[0-9]+"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_FAIRVAL" runat="server" Text="FAIR VALUE"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_FAIRVAL" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator14" ControlToValidate="TXT_FAIRVAL"
                    runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="^[0-9]+"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_NAV" runat="server" Text="NAV"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_NAV" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
                        </td>
                        <td class="style1">
                          <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator15" ControlToValidate="TXT_NAV"
                                runat="server" ErrorMessage="Only 0.00 format allowed" ValidationExpression="^[0-9]+(\.[0-9]{1,2})?$"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_SALEPRCD" runat="server" Text="SALE PROCEED"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_SALEPRCD" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator21" ControlToValidate="TXT_SALEPRCD"
                    runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="^[0-9]+"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_TOT_INCM" runat="server" Text="TOTAL INCOME"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_TOT_INCM" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator21" ControlToValidate="TXT_SALEPRCD"
                    runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="^[0-9]+"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
<%--                    <tr>
                        <td>
                            <asp:Label ID="LBL_EXIT_CPGN" runat="server" Text="EXIT CAPITAL GAIN"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_EXIT_CPGN" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
                        </td> 
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_ACTRL_INCM" runat="server" Text="ACTUAL REALIZED INCOME"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_ACTRL_INCM" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
                        </td>
                        <td class="style1">
                             
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_ANNRL_PERC" runat="server" Text="ANNUALIZED REALIZED PERCENTAGE"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_ANNRL_PERC" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
                        </td>
                        <td class="style1">
                           
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_ANNRL_INCM" runat="server" Text="ANNUAL REALIZED INCOME"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_ANNRL_INCM" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
                        </td>
                        <td class="style1">
                          
                        </td>
                    </tr>--%>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_UNRL_INCM" runat="server" Text="UNREALIZED INCOME"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_UNRL_INCM" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
                        </td>
                        <td class="style1">
                             
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_UNRL_PERC" runat="server" Text="UNREALIZED PERCENTAGE"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_UNRL_PERC" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
                        </td>
                        
                    </tr>
    <%--                <tr>
                        <td>
                            <asp:Label ID="LBL_UNRL_DISC_INCM" runat="server" Text="UNREALIZED DISCOUNTED INCOME"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_UNRL_DISC_INCM" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
                        </td>
                        
                    </tr>--%>
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
                    <tr>
                        <td>
                            <asp:Label ID="LBL_LOCATION" runat="server" Text="LOCATION" Visible="false"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_LOCATION" runat="server" Width="200px" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_HOLDNAME" runat="server" Text="HOLDING COMPANY NAME" Visible="false"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_HOLDNAME" runat="server" Width="400px" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_OPPRBY" runat="server" Text="OPPORTUNITY OFFERED BY" Visible="false"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_OPPRBY" runat="server" Width="400px" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_BANK" runat="server" Text="BANK" Visible="false"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_BANK" runat="server" Width="200px" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="TXT_PRVTEQT" runat="server" Text="PRIVATE EQUITY" Visible="false"></asp:Label>
                        </td>
                        <td class="style1">
                            <cc1:ComboBox ID="CMB_PRVTEQT" runat="server" DropDownStyle="DropDown"  
                                AutoCompleteMode="SuggestAppend" Width="200px" Visible="false">
                                <asp:ListItem Text="PRIVATE EQUITY" Value="Y" Selected="True" />
                                <asp:ListItem Text="NON-PRIVATE EQUITY" Value="N" />
                            </cc1:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_FKM_LNK" runat="server" Text="LINK FKM" Visible="false"></asp:Label>
                        </td>
                        <td class="style1">
                            <cc1:ComboBox ID="CMB_FKM_LNK" runat="server" DataSourceID="FKM_SRL" DataTextField="FKM_SRL"
                                DataValueField="FKM_SRL" DropDownStyle="DropDown"   AutoCompleteMode="SuggestAppend"
                                Width="200px" Visible="false">
                            </cc1:ComboBox>
                            <asp:SqlDataSource ID="FKM_SRL" runat="server" ConnectionString="<%$ ConnectionStrings:FKMCONNSTR %>"
                                SelectCommand="SELECT DISTINCT [FKM_SRL],[FKM_SRL] + '-' + [FKM_PROJNAME] AS [FKM_PROJNAME]  FROM [INVEST_INFO] ORDER BY [FKM_SRL]">
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_CAPRFND" runat="server" Text="CAPITAL REFUND" Visible="false"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_CAPRFND" runat="server" Width="200px" Visible="false" onchange="numformat(this)"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ControlToValidate="TXT_CAPRFND"
                    runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="^[0-9]+"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_EXPNS" runat="server" Text="EXPENSE" Visible="false"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_EXPNS" runat="server" Width="200px" Visible="false" onchange="numformat(this)"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ControlToValidate="TXT_EXPNS"
                    runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="^[0-9]+"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_BOOKVAL" runat="server" Text="BOOK VALUE" Visible="false"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_BOOKVAL" runat="server" Width="200px" Visible="false" onchange="numformat(this)"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <%--    <asp:RegularExpressionValidator ID="RegularExpressionValidator11" ControlToValidate="TXT_BOOKVAL"
                    runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="^[0-9]+"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_ANLYCAL" runat="server" Text="ANNUAL YEILD" Visible="false"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_ANLYCAL" runat="server" Width="200px" Visible="false" onchange="numformat(this)"></asp:TextBox>
                        </td>
                        <td class="style1">
                            <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator19" ControlToValidate="TXT_ANLYCAL"
                    runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="^[0-9]+"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                </table>
            </td>
            <td rowspan="35" valign="top">
                
                 <table  >
            <tr>
                <td>
                    <asp:Label ID="LBL_NAV_QTR_QRY" runat="server" Text="NAV QUARTER"></asp:Label>
                </td>
                <td class="style1"> 
                    <cc1:ComboBox ID="CMB_NAV_QTR_QRY" runat="server" AutoPostBack="true" DataTextField="QUARTER"
                        DataValueField="CD" DropDownStyle="DropDown"    
                        AutoCompleteMode="SuggestAppend">
                    </cc1:ComboBox>
                </td>
                <td>
                    <asp:Label ID="LBL_FKM_CD_QRY" runat="server" Text="FKM CODE"></asp:Label>
                </td>
                <td class="style1"> 
                    <cc1:ComboBox ID="CMB_FKM_CD_QRY" runat="server" AutoPostBack="true" DataTextField="FKM_PROJNAME"
                        DataValueField="FKM_SRL" DropDownStyle="DropDown"  
                          AutoCompleteMode="SuggestAppend" Width="400px">
                    </cc1:ComboBox>
                </td> 
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
            
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="FKM_SRL,FKM_ISSDATE"
            RowStyle-Wrap="false" HeaderStyle-BackColor="#0071ac" HeaderStyle-ForeColor="WhiteSmoke"
            RowStyle-ForeColor="Black"  RowStyle-Font-Size="Small" RowStyle-Height="25px" RowStyle-Font-Bold="true">
            <Columns> <asp:CommandField ShowSelectButton="true" />
                <%--<asp:HyperLinkField DataNavigateUrlFields="FKM_SRL,FKM_ISSDATE" HeaderText="REF SRL"
                    DataNavigateUrlFormatString="NAV_DTL_EDIT.aspx?REFSRL={0}&QTR={1}" Target="_blank"
                    DataTextField="FKM_SRL" />--%>
                    <asp:BoundField DataField="FKM_SRL" HeaderText="REF SRL" SortExpression="FKM_SRL"
                                ReadOnly="True" /> 
                <asp:BoundField DataField="FKM_HD1" HeaderText="QUARTER" SortExpression="FKM_HD1"
                    ReadOnly="True" />
                <asp:BoundField DataField="FKM_PROJNAME" HeaderText="PROJECT NAME" SortExpression="FKM_PROJNAME"
                    ItemStyle-Width="300Px" />
                <asp:BoundField DataField="FKM_PRTCPDATE" HeaderText="PARTICIPATION DATE" SortExpression="FKM_PRTCPDATE" />
                <asp:BoundField DataField="FKM_MTRTDATE" HeaderText="LIQUIDATION DATE" SortExpression="FKM_MTRTDATE" />
                <asp:BoundField DataField="FKM_COMMCAP_R" HeaderText="COMMITED CAPITAL" SortExpression="FKM_COMMCAP"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_CAPPD_R" HeaderText="PAID" SortExpression="FKM_CAPPD"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_CAPUNPD_R" HeaderText="OUTSTANDING" SortExpression="FKM_CAPUNPD"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_INVAMT_R" HeaderText="INVESTMENT AMOUNT (COST)" SortExpression="FKM_INVAMT"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_ROI_R" HeaderText="RATE OF INTEREST" SortExpression="FKM_ROI"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_ANLINCMCY_R" HeaderText="ANNUAL INTEREST INCOME ON CURRENT YIELD"
                    SortExpression="FKM_ANLINCMCY" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_SCNINCP_R" HeaderText="ACTUAL RETURNS TILL [ ]" SortExpression="FKM_SCNINCP"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_ANLRLZD_R" HeaderText="ANNUALIZED REALIZED" SortExpression="FKM_ANLRLZD"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_ACTLINCMRU_R" HeaderText="ACTUAL ON INCOME ECEIVED UPTO [ ]"
                    SortExpression="FKM_ACTLINCMRU" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_UNRLYLD_R" HeaderText="UNREALIZED YIELD" SortExpression="FKM_UNRLYLD"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_DVDND_R" HeaderText="DIVIDEND" SortExpression="FKM_DVDND"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_VALVTN_R" HeaderText="VALUATION" SortExpression="FKM_VALVTN"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_CAPGN_R" HeaderText="CAPITAL GAIN (LOSS)" SortExpression="FKM_CAPGN_R"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_UNRLDVD_R" HeaderText="UNREALIZED YIELD / DIVIDEND "
                    SortExpression="FKM_UNRLDVD" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_UNRLDVDCAP_R" HeaderText="UNREALIZED YIELD / DIVIDEND / CAPITAL GAIN (LOSS) "
                    SortExpression="FKM_UNRLDVDCAP" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_FAIRVAL_R" HeaderText="FAIR VALUE" SortExpression="FKM_FAIRVAL"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_NAV_R" HeaderText="NAV" SortExpression="FKM_NAV" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_REMX" HeaderText="REMARKS" SortExpression="FKM_REMX" />
                <asp:BoundField DataField="FKM_REG" HeaderText="REGISTERED" SortExpression="FKM_REG" />
                <asp:BoundField DataField="FKM_INCGEN" HeaderText="INC GEN" SortExpression="FKM_INCGEN" />
                <asp:BoundField DataField="FKM_INVGRP" HeaderText="CATEGORY" SortExpression="FKM_INVGRP" />
                <asp:BoundField DataField="FKM_CURR" HeaderText="CURRRENCY" SortExpression="FKM_CURR"
                    ItemStyle-Width="100Px" />
                <%--   <asp:HyperLinkField DataNavigateUrlFields="INVC_REFSRL" HeaderText="DOWNLOAD" 
             DataNavigateUrlFormatString="INVOICE_LIST.aspx?REFSRL={0}" Target="_blank" DataTextField="INVC_REFSRL" />--%>
            </Columns>
        </asp:GridView>
        </tr>
        </table>
            </td>
        </tr>
    </table>
</asp:Content>
