<%@ Page Title="INVESTMENT DETAIL (ADD/EDIT)" Language="C#" MasterPageFile="~/MainPage.Master"
    AutoEventWireup="false" CodeFile="INVEST_EDIT.aspx.cs" Inherits="INVEST_EDIT" %>

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
                VEIW/EDIT INVESTMENT DETAILS
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
                <asp:TextBox  ID="TXT_FKM_CD" runat="server" Width="200px"></asp:TextBox>
                <cc1:ComboBox ID="CMB_FKM_CD" runat="server" AutoPostBack="true" DataTextField="FKM_PROJNAME"
                    DataValueField="FKM_SRL" DropDownStyle="DropDownList"  
                      AutoCompleteMode="SuggestAppend"  >
                </cc1:ComboBox>
            </td>
        </tr>
        <%-- <tr>
            <td>
                <asp:Label ID="LBL_PROJNAME" runat="server" Text="PROJECT NAME"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox ID="TXT_PROJNAME" runat="server" Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_HOLDNAME" runat="server" Text="HOLDING NAME"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox ID="TXT_HOLDNAME" runat="server" Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_OPPRBY" runat="server" Text="OPPR OFFERED BY"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox ID="TXT_OPPRBY" runat="server" Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_INVCOMP" runat="server" Text="INVEST CATEGORY"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox ID="TXT_INVCOMP" runat="server" Width="400px"></asp:TextBox>
            </td>
        </tr>--%>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="PROJECT NAME"></asp:Label>
            </td>
            <td class="style1">
                <cc1:ComboBox ID="CMB_PROJNAME" runat="server" Width="400px" DataSourceID="FKM_PROJNAME"
                    DataTextField="RF_DESCRP" DataValueField="RF_DESCRP" DropDownStyle="DropDown"
                        AutoCompleteMode="SuggestAppend">
                </cc1:ComboBox>
                <asp:SqlDataSource ID="FKM_PROJNAME" runat="server" ConnectionString="<%$ ConnectionStrings:FKMCONNSTR %>"
                    SelectCommand="SELECT DISTINCT [RF_DESCRP] FROM [FKM_REFRNC] WHERE ([RF_FEILDTYPE] = @RF_DESCRP)  ORDER BY [RF_DESCRP]">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="FKM_PROJNAME" Name="RF_DESCRP" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="HOLDING COMPANY NAME"></asp:Label>
            </td>
            <td class="style1">
                <cc1:ComboBox ID="CMB_HOLDNAME" runat="server" Width="400px" DataSourceID="FKM_HOLDNAME"
                    DataTextField="RF_DESCRP" DataValueField="RF_DESCRP" DropDownStyle="DropDown"
                        AutoCompleteMode="SuggestAppend">
                </cc1:ComboBox>
                <asp:SqlDataSource ID="FKM_HOLDNAME" runat="server" ConnectionString="<%$ ConnectionStrings:FKMCONNSTR %>"
                    SelectCommand="SELECT DISTINCT [RF_DESCRP] FROM [FKM_REFRNC] WHERE ([RF_FEILDTYPE] = @RF_FEILDTYPE) ORDER BY [RF_DESCRP]">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="FKM_HOLDNAME" Name="RF_FEILDTYPE" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="OPPORTUNITY&nbsp;OFFERED&nbsp;BY"></asp:Label>
            </td>
            <td class="style1">
                <cc1:ComboBox ID="CMB_OPPRBY" runat="server" Width="400px" DataSourceID="FKM_OPPRBY"
                    DataTextField="RF_DESCRP" DataValueField="RF_DESCRP" DropDownStyle="DropDown"
                        AutoCompleteMode="SuggestAppend">
                </cc1:ComboBox>
                <asp:SqlDataSource ID="FKM_OPPRBY" runat="server" ConnectionString="<%$ ConnectionStrings:FKMCONNSTR %>"
                    SelectCommand="SELECT DISTINCT [RF_DESCRP] FROM [FKM_REFRNC] WHERE ([RF_FEILDTYPE] = @RF_FEILDTYPE) ORDER BY [RF_DESCRP]">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="FKM_OPPRBY" Name="RF_FEILDTYPE" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="INVEST CATEGORY"></asp:Label>
            </td>
            <td class="style1">
                <cc1:ComboBox ID="CMB_INVCOMP" runat="server" Width="400px" DataSourceID="FKM_INVCOMP"
                    DataTextField="RF_DESCRP" DataValueField="RF_DESCRP" DropDownStyle="DropDown"
                        AutoCompleteMode="SuggestAppend">
                </cc1:ComboBox>
                <asp:SqlDataSource ID="FKM_INVCOMP" runat="server" ConnectionString="<%$ ConnectionStrings:FKMCONNSTR %>"
                    SelectCommand="SELECT DISTINCT [RF_DESCRP] FROM [FKM_REFRNC] WHERE ([RF_FEILDTYPE] = @RF_FEILDTYPE) ORDER BY [RF_DESCRP]">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="FKM_INVCOMP" Name="RF_FEILDTYPE" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_INVGRP" runat="server" Text="INVEST GROUP"></asp:Label>
            </td>
            <td class="style1">
                <cc1:ComboBox ID="CMB_INVGRP" runat="server" Width="400px" DataSourceID="FKM_INVGRP"
                    DataTextField="RF_DESCRP" DataValueField="RF_DESCRP" DropDownStyle="DropDownList"
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
        <%-- <tr>
            <td>
                <asp:Label ID="LBL_LOCATION" runat="server" Text="LOCATION"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox ID="TXT_LOCATION" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_CURR" runat="server" Text="CURRENCY"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox ID="TXT_CURR" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_BANK" runat="server" Text="BANK"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox ID="TXT_BANK" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>--%>
        <tr>
            <td>
                <asp:Label ID="LBL_LOCATION" runat="server" Text="LOCATION"></asp:Label>
            </td>
            <td class="style1">
                <cc1:ComboBox ID="CMB_LOCATION" runat="server" Width="200px" DataSourceID="FKM_LOCATION"
                    DataTextField="RF_DESCRP" DataValueField="RF_DESCRP" DropDownStyle="DropDownList"
                        AutoCompleteMode="SuggestAppend">
                </cc1:ComboBox>
                <asp:SqlDataSource ID="FKM_LOCATION" runat="server" ConnectionString="<%$ ConnectionStrings:FKMCONNSTR %>"
                    SelectCommand="SELECT DISTINCT RTRIM([RF_DESCRP]) AS [RF_DESCRP] FROM [FKM_REFRNC] WHERE ([RF_FEILDTYPE] = @RF_FEILDTYPE) ORDER BY [RF_DESCRP]">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="FKM_LOCATION" Name="RF_FEILDTYPE" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
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
                <asp:Label ID="LBL_BANK" runat="server" Text="BANK"></asp:Label>
            </td>
            <td class="style1">
                <cc1:ComboBox ID="CMB_BANK" runat="server" DataSourceID="FKM_BANK" DataTextField="RF_DESCRP"
                    DataValueField="RF_DESCRP" DropDownStyle="DropDownList"  
                      AutoCompleteMode="SuggestAppend" Width="200px">
                </cc1:ComboBox>
                <asp:SqlDataSource ID="FKM_BANK" runat="server" ConnectionString="<%$ ConnectionStrings:FKMCONNSTR %>"
                    SelectCommand="SELECT DISTINCT RTRIM([RF_DESCRP]) AS [RF_DESCRP] FROM [FKM_REFRNC] WHERE ([RF_FEILDTYPE] = @RF_FEILDTYPE) ORDER BY [RF_DESCRP]">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="FKM_BANK" Name="RF_FEILDTYPE" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_COMP_CD" runat="server" Text="REGISTERED"></asp:Label>
            </td>
            <td class="style1">
                <cc1:ComboBox ID="CMB_COMP_CD" runat="server" DataSourceID="COMP_CD" DataTextField="RF_DESCRP"
                    DataValueField="RF_CODE" DropDownStyle="DropDownList"  
                      AutoCompleteMode="SuggestAppend" Width="200px" ForeColor="Red">
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
                <asp:Label ID="TXT_PRVTEQT" runat="server" Text="PRIVATE EQUITY"></asp:Label>
            </td>
            <td class="style1">
                <cc1:ComboBox ID="CMB_PRVTEQT" runat="server" DropDownStyle="DropDown"  
                      AutoCompleteMode="SuggestAppend" Width="200px">
                    <asp:ListItem Text="PRIVATE EQUITY" Value="Y" Selected="True" />
                    <asp:ListItem Text="NON-PRIVATE EQUITY" Value="N" />
                </cc1:ComboBox>
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
                    <asp:ListItem Text="OFF THE RECORD" Value="O" />
                </cc1:ComboBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_FKM_LNK" runat="server" Text="LINK FKM"></asp:Label>
            </td>
            <td class="style1">
                <cc1:ComboBox ID="CMB_FKM_LNK" runat="server" DataTextField="FKM_SRL" DataValueField="FKM_SRL"
                    DropDownStyle="DropDown"     AutoCompleteMode="SuggestAppend"
                    Width="200px">
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
            <td class="style1">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_COMMCAP2" runat="server" Text="COMMITTED CAPITAL2"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox ID="TXT_COMMCAP2" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
            </td>
            <td class="style1">
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
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_CAPPD" runat="server" Text="CAPITAL PAID" Visible="false"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox ID="TXT_CAPPD" runat="server" Width="200px" Visible="false" onchange="numformat(this)"></asp:TextBox>
                <td class="style1">
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
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_CAPRFND" runat="server" Text="CAPITAL REFUND" Visible="true"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox ID="TXT_CAPRFND" runat="server" Width="200px" Visible="true" onchange="numformat(this)"></asp:TextBox>
            </td>
            <td class="style1">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_EXPNS" runat="server" Text="EXPENSE" Visible="true"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox ID="TXT_EXPNS" runat="server" Width="200px" Visible="true" onchange="numformat(this)"></asp:TextBox>
            </td>
            <td class="style1">
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
                <asp:Label ID="LBL_BOOKVAL" runat="server" Text="BOOK VALUE" Visible="false"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox ID="TXT_BOOKVAL" runat="server" Width="200px" Visible="false" onchange="numformat(this)"></asp:TextBox>
            </td>
            <td class="style1">
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
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_ANLYCAL" runat="server" Text="ANNUAL YEILD"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox ID="TXT_ANLYCAL" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
            </td>
            <td class="style1">
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
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="BTN_ADD" runat="server" Text="SAVE"   />
                <asp:Button ID="BTN_UPDATE" runat="server" Text="UPDATE"   />
            </td>
            <td class="style1">
                <asp:Button ID="BTN_CLEAR" runat="server" Text="CLEAR" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <%-- <tr>
            <td>
                <asp:Label ID="LBL_ANLINCMCY" runat="server" Text="ACTUAL INTEREST INCOME ON CURRENT YIELD "  Visible="false" ></asp:Label>
            </td>
            <td class="style1">
               <asp:TextBox  ID="TXT_ANLINCMCY" runat="server" Width="200px"  Visible="false"  onchange="numformat(this)"  ></asp:TextBox>
                 
            </td> 
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_ANLRLZD" runat="server" Text="ANNUALIZED REALIZED"  Visible="false" ></asp:Label>
            </td>
            <td class="style1">
               <asp:TextBox  ID="TXT_ANLRLZD" runat="server" Width="200px"  Visible="false"  onchange="numformat(this)" ></asp:TextBox>
                
            </td>
             
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_ACTLINCMRU" runat="server" Text="ACTUAL INCOME RECEIVED UPTO"  Visible="false" ></asp:Label>
            </td>
            <td class="style1">
               <asp:TextBox  ID="TXT_ACTLINCMRU" runat="server" Width="200px"  Visible="false"  onchange="numformat(this)" ></asp:TextBox>
               
            </td>
            <td class="style1">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator13" ControlToValidate="TXT_ACTLINCMRU"
                    runat="server" ErrorMessage="Only  0.00 format allowed" ValidationExpression="^[0-9]+(\.[0-9]{1,2})?$"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_UNRLYLD" runat="server" Text="UNREALIZED YIELD"  Visible="false" ></asp:Label>
            </td>
            <td class="style1">
               <asp:TextBox  ID="TXT_UNRLYLD" runat="server" Width="200px"  Visible="false"  onchange="numformat(this)" ></asp:TextBox>
                
            </td>
            <td class="style1">
                
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_DVDND" runat="server" Text="DIVIDEND"  Visible="false" ></asp:Label>
            </td>
            <td class="style1">
               <asp:TextBox  ID="TXT_DVDND" runat="server" Width="200px"  Visible="false"  onchange="numformat(this)"  ></asp:TextBox>
                 
            </td>
            <td class="style1">
              
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_VALVN" runat="server" Text="VALUATION"  Visible="false" ></asp:Label>
            </td>
            <td class="style1">
               <asp:TextBox  ID="TXT_VALVTN" runat="server" Width="200px"  Visible="false" onchange="numformat(this)"  ></asp:TextBox>
                
            </td>
            <td class="style1">
               
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_CAPGN" runat="server" Text="CAPITAL GAIN (LOSS)"  Visible="false" ></asp:Label>
            </td>
            <td class="style1">
               <asp:TextBox  ID="TXT_CAPGN" runat="server" Width="200px"  Visible="false"  onchange="numformat(this)" ></asp:TextBox>
                
            </td>
            <td class="style1">
               
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_UNRLDVD" runat="server" Text="UNREALIZED YIELD / DIVIDEND"  Visible="false" ></asp:Label>
            </td>
            <td class="style1">
               <asp:TextBox  ID="TXT_UNRLDVD" runat="server" Width="200px"  Visible="false"  onchange="numformat(this)"  ></asp:TextBox>
                
            </td>
            <td class="style1">
               
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_UNRLDVDCAP" runat="server" Text="UNREALIZED YIELD / DIVIDEND / CAPITAL GAIN(LOSS)"  Visible="false" ></asp:Label>
            </td>
            <td class="style1">
               <asp:TextBox  ID="TXT_UNRLDVDCAP" runat="server" Width="200px"  Visible="false"  onchange="numformat(this)"  ></asp:TextBox>
                 
            </td>
            <td class="style1">
               
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_FAIRVAL" runat="server" Text="FAIR VALUE"  Visible="false" ></asp:Label>
            </td>
            <td class="style1">
               <asp:TextBox  ID="TXT_FAIRVAL" runat="server" Width="200px"  Visible="false"  onchange="numformat(this)" ></asp:TextBox>
                
            </td>
            <td class="style1">
                
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_NAV" runat="server" Text="NAV"  Visible="false" ></asp:Label>
            </td>
            <td class="style1">
               <asp:TextBox  ID="TXT_NAV" runat="server" Width="200px"  Visible="false"  onchange="numformat(this)" ></asp:TextBox>
                
            </td>
            <td class="style1">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator15" ControlToValidate="TXT_NAV"
                    runat="server" ErrorMessage="Only 0.00 format allowed" ValidationExpression="^[0-9]+(\.[0-9]{1,2})?$"></asp:RegularExpressionValidator>
            </td>
        </tr>--%>
    </table>
            </td>
            <td valign="top">
             <table>
        <tr>
                        <td>
                            <asp:CheckBox ID="CHBX_EDIT" runat="server" Text="EDIT" AutoPostBack="true" OnCheckedChanged="CHBX_EDIT_CheckedChanged" />
                      
            </td>
           <%-- <td valign="top">
                 <asp:TextBox ID="TXT_SRCH" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td valign="top">
                <asp:Button ID="BTNSRCH" runat="server" Text="SEARCH"  OnClick="BTNSRCH_Click"/> 
            </td>--%>
        </tr>
        <tr>
                        <td colspan="10">
              <%--   OnSelectedIndexChanged="GridView1_SelectedIndexChanged">--%>
                   <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="FKM_SRL"
                RowStyle-Wrap="false" HeaderStyle-BackColor="#0071ac" HeaderStyle-ForeColor="WhiteSmoke"
                RowStyle-ForeColor="Black"   HeaderStyle- 
                RowStyle-Font-Size="Small" RowStyle-Height="28px" RowStyle-Font-Bold="true"
                 OnRowDataBound="GridView1_RowDataBound"  >
                <Columns>
                <asp:CommandField ShowSelectButton="true" />
                    <asp:HyperLinkField DataNavigateUrlFields="FKM_SRL" HeaderText="CODE" DataNavigateUrlFormatString="~/INV_TRX.aspx?FKMCODE={0}"
                        Target="_blank" DataTextField="FKM_SRL" />
                    <%--     <asp:BoundField DataField="FKM_SRL" HeaderText="SRL" SortExpression="FKM_SRL" ItemStyle-Width="70Px"
                    ReadOnly="True" />--%>                   
                    <asp:BoundField DataField="FKM_ISSDATE" HeaderText="ISSUE DATE" SortExpression="FKM_ISSDATE"
                        ItemStyle-Width="100Px" />
                    <asp:BoundField DataField="FKM_CURR" HeaderText="CURRRENCY" SortExpression="FKM_CURR"
                        ControlStyle-Width="200px" ItemStyle-Width="100Px" />
                    <asp:BoundField DataField="FKM_PROJNAME" HeaderText="PROJECT NAME" SortExpression="FKM_PROJNAME"
                        ItemStyle-Width="300Px" />
                    <%--<asp:HyperLinkField DataNavigateUrlFields="FKM_SRL,FKM_PROJNAME" HeaderText="PROJECT NAME"
             DataNavigateUrlFormatString="INVEST/INVEST_EDIT.aspx?REFSRL={0}" Target="_blank" DataTextField="FKM_PROJNAME" ItemStyle-Width="300Px" />
                <asp:BoundField DataField="FKM_HOLDNAME" HeaderText="HOLDING NAME" SortExpression="FKM_HOLDNAME"
                    ItemStyle-Width="300Px" />--%>
                    <asp:BoundField DataField="FKM_LOCATION" HeaderText="LOCATION" SortExpression="FKM_LOCATION"
                        ControlStyle-Width="400px" ItemStyle-Width="100Px" ItemStyle-Wrap="false" />
                    <asp:BoundField DataField="FKM_INVCOMP" HeaderText="INVESTMENT CATEGORY" SortExpression="FKM_INVCOMP"
                        ControlStyle-Width="300px" />
                    <asp:BoundField DataField="FKM_OPPRBY" HeaderText="OPPORTUNITY OFFERED BY" SortExpression="FKM_OPPRBY"
                        ControlStyle-Width="300px" />
                    <asp:BoundField DataField="FKM_BANK" HeaderText="BANK" SortExpression="FKM_BANK"
                        ControlStyle-Width="300px" />
                    <asp:BoundField DataField="FKM_PRVTEQT" HeaderText="PRIVATE EQUITY" SortExpression="FKM_PRVTEQT"
                        ControlStyle-Width="300px" />
                    <asp:BoundField DataField="FKM_YEILDPRD" HeaderText="YEILD PERIOD" SortExpression="FKM_YEILDPRD"
                        ControlStyle-Width="300px" />
                    <asp:BoundField DataField="FKM_PRTCPDATE" HeaderText="PARTICIPATION DATE" SortExpression="FKM_PRTCPDATE"
                        ControlStyle-Width="150px" />
                    <asp:BoundField DataField="FKM_MTRTDATE" HeaderText="MATURITY DATE" SortExpression="FKM_MTRTDATE"
                        ControlStyle-Width="150px" />
                    <asp:BoundField DataField="FKM_COMMCAP_R" HeaderText="COMMITED CAPITAL" SortExpression="FKM_COMMCAP"
                        ControlStyle-Width="120px" ItemStyle-HorizontalAlign="Right" />
                    <%-- <asp:BoundField DataField="FKM_COMMCAP2_R" HeaderText="COMMITTED CAP2" SortExpression="FKM_COMMCAP2"
                    ItemStyle-HorizontalAlign="Right" />--%>
                    <asp:BoundField DataField="FKM_INVAMT_R" HeaderText="INVESTMENT" SortExpression="FKM_INVAMT"
                        ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="FKM_ROI_R" HeaderText="ROI" SortExpression="FKM_ROI" ItemStyle-HorizontalAlign="Right"
                        ControlStyle-Width="70px" />
                    <asp:BoundField DataField="FKM_CAPUNPD_R" HeaderText="CAPITAL UNPAID" SortExpression="FKM_CAPUNPD"
                        ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="FKM_CAPRFND_R" HeaderText="CAPITAL REFUND" SortExpression="FKM_CAPRFND"
                        ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="FKM_EXPNS_R" HeaderText="EXPENSE" SortExpression="FKM_EXPNS"
                        ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="FKM_MONYCAL_R" HeaderText="MONTHLY YEILD" SortExpression="FKM_MONYCAL"
                        ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="FKM_QRTYCAL_R" HeaderText="QUARTERLY YEILD" SortExpression="FKM_QRTYCAL"
                        ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="FKM_SMANYCAL_R" HeaderText="SEMI ANNUAL YEILD" SortExpression="FKM_SMANYCAL"
                        ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="FKM_ANLYCAL_R" HeaderText="ANNUAL YEILD" SortExpression="FKM_ANLYCAL"
                        ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="FKM_SCNINCP_R" HeaderText="SINCE INCEPTIION" SortExpression="FKM_SCNINCP"
                        ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="FKM_SALEPRCD_R" HeaderText="SALE PROCEED" SortExpression="FKM_SALEPRCD"
                        ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="FKM_REMX" HeaderText="REMARKS" SortExpression="FKM_REMX" />
                    <%-- 
                <asp:BoundField DataField="FKM_INCGEN" HeaderText="INC GEN" SortExpression="FKM_INCGEN" />
                <asp:BoundField DataField="FKM_STATUS" HeaderText="STATUS" SortExpression="FKM_STATUS" />
                <asp:BoundField DataField="FKM_LNKSRL" HeaderText="LNK SRL" SortExpression="FKM_LNKSRL" />
                <asp:BoundField DataField="FKM_CAPPD_R" HeaderText="CAPITAL PAID" SortExpression="FKM_CAPPD"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_VALVTN_R" HeaderText="VALUATION" SortExpression="FKM_VALVTN"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_DVDND_R" HeaderText="DIVIDEND" SortExpression="FKM_DVDND"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_BOOKVAL_R" HeaderText="BOOK VALUE" SortExpression="FKM_BOOKVAL"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_CAPGN_R" HeaderText="CAPITAL GAIN" SortExpression="FKM_CAPGN"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_UNRLCAP_R" HeaderText="UNRL CAPITAL" SortExpression="FKM_UNRLCAP"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_FAIRVAL_R" HeaderText="FAIR VALUE" SortExpression="FKM_FAIRVAL"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_NAV_R" HeaderText="NAV" SortExpression="FKM_NAV" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_REMX" HeaderText="REMARKS" SortExpression="FKM_REMX" />
                <asp:BoundField DataField="FKM_UPDBY" HeaderText="UPD BY" SortExpression="FKM_UPDBY" />--%>
                </Columns>
            </asp:GridView>
              
         
            </td>
        </tr>
    </table>
            </td>
        </tr>
    </table>
</asp:Content>
