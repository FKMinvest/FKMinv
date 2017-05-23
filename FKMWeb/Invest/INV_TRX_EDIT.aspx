<%@ Page Title="INVESTMENT TRANSACTIONS " Language="C#" MasterPageFile="~/MainPage.Master"
    AutoEventWireup="false" CodeFile="INV_TRX_EDIT.aspx.cs" Inherits="INVEST_TRX_EDIT" %>

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
                EDIT INVESTMENT TRANSACTIONS
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
                            <asp:Label ID="LBL_FKM_CD" runat="server" Text="CODE"></asp:Label>
                        </td>
                        <td class="style1">
                            <cc1:ComboBox ID="CMB_FKM_CD" runat="server" DataTextField="FKM_PROJNAME" DataValueField="FKM_SRL"
                                DropDownStyle="DropDownList" AutoCompleteMode="SuggestAppend" Width="100px" AutoPostBack="true">
                            </cc1:ComboBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_REFSRL" runat="server" Text="REF SRL"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_REFSRL" runat="server" Width="200px" ReadOnly="true" ForeColor="Red"
                                Font-Bold="true"></asp:TextBox>
                            <cc1:ComboBox ID="CMB_REFSRL" runat="server" DropDownStyle="DropDownList" AutoCompleteMode="SuggestAppend"
                                Width="200px" AutoPostBack="True" DataSourceID="SqlDataSource2" DataTextField="TRX_REFSRL"
                                DataValueField="TRX_REFSRL" MaxLength="0" Style="display: inline;">
                            </cc1:ComboBox>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:FKMCONNSTR %>"
                                SelectCommand="SELECT DISTINCT [TRX_REFSRL] FROM [INVEST_TRX] WHERE ([TRX_FKM_SRL] = @TRX_FKM_SRL) ORDER BY [TRX_REFSRL] DESC">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="CMB_FKM_CD" DefaultValue="#" Name="TRX_FKM_SRL"
                                        PropertyName="SelectedValue" Type="String" />
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
                            <asp:Label ID="LBL_ISSUEDATE" runat="server" Text="DATE"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_ISSUEDATE" runat="server" Width="200px"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="TXT_ISSUEDATE"
                                Format="dd/MM/yyyy" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <asp:Label ID="LBL_REFMEMO" runat="server" Text="REFERENCE MEMO"></asp:Label>
                        </td>
                        <td class="style1" valign="top">
                            <asp:TextBox ID="TXT_REFMEMO" runat="server" TextMode="MultiLine" Rows="2" MaxLength="20"
                                Width="200px" onchange="rmvqoutes(this)"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <asp:Label ID="LBL_DESCRP" runat="server" Text="DESCRIPTION" Visible="false"></asp:Label>
                        </td>
                        <td class="style1" valign="top">
                            <asp:TextBox ID="TXT_DESCRP" runat="server" TextMode="MultiLine" Rows="3" MaxLength="50"
                                Width="200px" Visible="false" onchange="rmvqoutes(this)"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_TINVAMT" runat="server" Text="INVESTMENT AMOUNT"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_TINVAMT" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
                            <%--<cc1:MaskedEditExtender ID="MaskedEditExtender3" TargetControlID="TXT_TINVAMT" runat="server"
                                Mask="999,999,999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft"
                                ErrorTooltipEnabled="True" />--%>
                        </td>
                        <%-- <td class="style1">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator10" ControlToValidate="TXT_TINVAMT"
                    runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="^[0-9]+"></asp:RegularExpressionValidator>
            </td>--%>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_EXPENSE_T" runat="server" Text="EXPENSE"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_EXPENSE_T" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
                            <%--       <cc1:MaskedEditExtender ID="MaskedEditExtender2" TargetControlID="TXT_EXPENSE_T" runat="server"
                                Mask="999,999,999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft"
                                ErrorTooltipEnabled="True" />--%>
                            <div style="display: none">
                                <asp:TextBox ID="TXT_EXPENSE_Temp" runat="server" Width="200px"></asp:TextBox>
                            </div>
                        </td>
                        <%-- <td class="style1">
             <asp:RegularExpressionValidator ID="RegularExpressionValidator11" ControlToValidate="TXT_INCOME" 
                    runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="^[0-9]+"></asp:RegularExpressionValidator> 
            </td>--%>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_INCOME" runat="server" Text="INCOME"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_INCOME" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
                            <%--<cc1:MaskedEditExtender ID="MaskedEditExtender1" TargetControlID="TXT_INCOME" runat="server"
                                Mask="999,999,999" MessageValidatorTip="true" MaskType="Number" InputDirection="RightToLeft"
                                ErrorTooltipEnabled="True" />--%>
                            <div style="display: none">
                                <asp:TextBox ID="TXT_INCOME_temp" runat="server" Width="200px"></asp:TextBox>
                            </div>
                        </td>
                        <%-- <td class="style1">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator11" ControlToValidate="TXT_INCOME"
                    runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="^[0-9]+"></asp:RegularExpressionValidator>
            </td>--%>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_TROI" runat="server" Text="RATE&nbsp;OF&nbsp;INTEREST"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_TROI" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
                            <%-- <cc1:MaskedEditExtender ID="MaskedEditExtender2" TargetControlID="TXT_TROI"
                    runat="server" Mask="99.99" MessageValidatorTip="true"  
                    OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft"
                    ErrorTooltipEnabled="True" />--%>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="TXT_TROI"
                                runat="server" ErrorMessage="Only 0.00 format allowed" ValidationExpression="^[0-9]+(\.[0-9]{1,2})?$"></asp:RegularExpressionValidator>
                    
                        </td>
                    </tr> 
                    <tr>
                        <td>
                            <asp:Label ID="LBL_NOF_DAYS" runat="server" Text="NO OF DAYS"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_NOF_DAYS" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox> 
                        </td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_STATUS" runat="server" Text="RECEIVED"></asp:Label>
                        </td>
                        <td class="style1">
                            <cc1:ComboBox ID="CMB_STATUS" runat="server" DropDownStyle="DropDown" AutoCompleteMode="SuggestAppend"
                                Width="40px">
                                <asp:ListItem Text="NO" Value="N" Selected="True" />
                                <asp:ListItem Text="YES" Value="Y" />
                            </cc1:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <asp:Label ID="LBL_REMX" runat="server" Text="NARRARTION"></asp:Label>
                        </td>
                        <td class="style1" colspan="2" valign="top">
                            <asp:TextBox ID="TXT_REMX" runat="server" TextMode="MultiLine" Rows="3" MaxLength="50"
                                Width="200px" onchange="rmvqoutes(this)"></asp:TextBox>
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
                            <asp:Label ID="LBL_TOTSNCINCP" runat="server" Text="ACTUAL&nbsp;RETURNS&nbsp;TILL&nbsp;DATE"
                                Visible="false"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_TOTSNCINCP" runat="server" Width="200px" ReadOnly="true" Visible="false"
                                onchange="numformat(this)"></asp:TextBox>
                            <%-- <cc1:MaskedEditExtender ID="MaskedEditExtender4" TargetControlID="TXT_TOTSNCINCP"
                                runat="server" Mask="999,999,999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft"
                                ErrorTooltipEnabled="True" />--%>
                        </td>
                        <%-- <td class="style1">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator13" ControlToValidate="TXT_TOTSNCINCP"
                    runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="^[0-9]+"></asp:RegularExpressionValidator>
            </td>--%>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <table>
                    <tr>
                        <td colspan="2">
                            <asp:Label runat="server" ID="Label1" Text="SPREADSHEET  UPLOAD / DOWNLOAD" />
                        </td>
                        <td colspan="2">
                            <asp:FileUpload ID="FileUploadControl" runat="server" Width="100%" />
                        </td>
                        <td>
                            <asp:Button runat="server" ID="UploadButton" Text="Upload" />
                        </td>
                        <td>
                            <asp:Button ID="Button2" runat="server" Text="DOWNLOAD SPREADSHEET" />
                            <asp:HyperLink runat="server" ID="DownloadButton" Text="Download" NavigateUrl=""
                                Target="_blank" Visible="false" />
                            <%-- <asp:Button runat="server" id="DownloadButton" text="Download"  onclick="DownloadButton_Click" />--%>
                        </td>
                        <td colspan="2">
                            <asp:Label runat="server" ID="StatusLabel" Text="" />
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_IRR_FLNM" runat="server" Width="300px" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="CHBX_EDIT" runat="server" Text="EDIT" AutoPostBack="true" Width="80PX" />
                        </td>
                        <td>
                            <asp:Label ID="LBL_PROJNAME_M" runat="server" Text="PROJECT&nbsp;NAME"></asp:Label>
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="TXT_PROJNAME_M" runat="server" Width="500px" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" rowspan="17" colspan="15">
                            <asp:Button ID="Button1" runat="server" Text="DOWNLOAD REPORT" ToolTip="Download the Summary Report" />
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="TRX_REFSRL"
                                RowStyle-Wrap="false" HeaderStyle-BackColor="#0071ac" HeaderStyle-ForeColor="WhiteSmoke"
                                RowStyle-ForeColor="Black" HeaderStyle- RowStyle-Font-Size="Small" RowStyle-Height="25px"
                                RowStyle-Font-Bold="true">
                                <Columns>
                                    <asp:CommandField ShowSelectButton="true" />
                                    <asp:BoundField DataField="TRX_REFSRL" HeaderText="REFERENCE" SortExpression="TRX_REFSRL" />
                                    <asp:BoundField DataField="TRX_ISSUEDATE" HeaderText="DATE" SortExpression="TRX_ISSUEDATE" />
                                    <asp:BoundField DataField="TRX_INVAMT_R" HeaderText="INVESTMENT AMOUNT" SortExpression="TRX_INVAMT"
                                        ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="TRX_EXPNSAMT_R" HeaderText="ENPENSE" SortExpression="TRX_EXPNSAMT"
                                        ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="TRX_AMT_R" HeaderText="INCOME" SortExpression="TRX_AMT"
                                        ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="TRX_NOF_DAYS" HeaderText="NO OF DAYS" SortExpression="NOF_DAYS"
                                        ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="TRX_ROI_R" HeaderText="RATE OF INTEREST" SortExpression="TRX_ROI"
                                        ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="TRX_REMX" HeaderText="REMARKS" SortExpression="TRX_REMX" />
                                    <asp:BoundField DataField="TRX_STATUS" HeaderText="RCVD" SortExpression="TRX_STATUS" />
                                    <asp:BoundField DataField="TRX_UPDBY" HeaderText="UPDATED BY" SortExpression="TRX_UPDBY" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:Panel ID="Panel1" runat="server" BackColor="LightBlue" Width="500">
        <%--  <
   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <ContentTemplate>--%>
        <table>
            <tr>
                <td align="center" colspan="2" style="font-size: large;">
                    INVESTMENT INFO
                    <hr />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LBL_FKM_REF" runat="server" Text="FKM REF"></asp:Label>
                </td>
                <td class="style1">
                    <asp:TextBox ID="TXT_FKM_REF" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LBL_PROJNAME" runat="server" Text="PROJECT NAME"></asp:Label>
                </td>
                <td class="style1">
                    <%--  <cc1:ComboBox   ID="CMB_PROJNAME" runat="server" Width="200px" DataSourceID="FKM_PROJNAME"
                    DataTextField="RF_DESCRP" DataValueField="RF_DESCRP" DropDownStyle="DropDown"      
                    AutoCompleteMode="SuggestAppend">
                </cc1:ComboBox>
                <asp:SqlDataSource ID="FKM_PROJNAME" runat="server" ConnectionString="<%$ ConnectionStrings:FKMCONNSTR %>"
                    SelectCommand="SELECT DISTINCT [RF_DESCRP] FROM [FKM_REFRNC] WHERE ([RF_FEILDTYPE] = @RF_DESCRP)  ORDER BY [RF_DESCRP]">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="FKM_PROJNAME" Name="RF_DESCRP" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>--%>
                    <asp:TextBox ID="TXT_PROJNAME" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LBL_PRTCPDATE" runat="server" Text="PARTICIPATION&nbsp;DATE"></asp:Label>
                </td>
                <td class="style1">
                    <asp:TextBox ID="TXT_PRTCPDATE" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                    <%-- <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TXT_PRTCPDATE" />--%>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LBL_MTRTDATE" runat="server" Text="MATURITY&nbsp;DATE"></asp:Label>
                </td>
                <td class="style1">
                    <asp:TextBox ID="TXT_MTRTDATE" runat="server" ReadOnly="true" Width="200px"></asp:TextBox>
                    <%--<cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TXT_MTRTDATE" />--%>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LBL_BANK" runat="server" Text="BANK"></asp:Label>
                </td>
                <td class="style1">
                    <%--  <cc1:ComboBox   ID="CMB_BANK" runat="server" DataSourceID="FKM_BANK" DataTextField="RF_DESCRP"
                    DataValueField="RF_DESCRP" DropDownStyle="DropDownList"      AutoCompleteMode="SuggestAppend"
                    Width="200px" >
                </cc1:ComboBox>
                <asp:SqlDataSource ID="FKM_BANK" runat="server" ConnectionString="<%$ ConnectionStrings:FKMCONNSTR %>"
                    SelectCommand="SELECT DISTINCT [RF_DESCRP] FROM [FKM_REFRNC] WHERE ([RF_FEILDTYPE] = @RF_FEILDTYPE) ORDER BY [RF_DESCRP]">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="FKM_BANK" Name="RF_FEILDTYPE" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>--%>
                    <asp:TextBox ID="TXT_BANK" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LBL_YLDPRD" runat="server" Text="YIELD PERIOD"></asp:Label>
                </td>
                <td class="style1">
                    <asp:TextBox ID="TXT_YLDPRD" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LBL_COMMCAP" runat="server" Text="COMMITTED&nbsp;CAPITAL"></asp:Label>
                </td>
                <td class="style1">
                    <asp:TextBox ID="TXT_COMMCAP" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LBL_COMMCAP2" runat="server" Text="COMMITTED&nbsp;CAPITAL2"></asp:Label>
                </td>
                <td class="style1">
                    <asp:TextBox ID="TXT_COMMCAP2" runat="server" ReadOnly="true" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LBL_INVAMT" runat="server" Text="INVEST AMOUNT"></asp:Label>
                </td>
                <td class="style1">
                    <asp:TextBox ID="TXT_INVAMT" runat="server" ReadOnly="true" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LBL_CAPPD" runat="server" Text="CAPITAL PAID"></asp:Label>
                </td>
                <td class="style1">
                    <asp:TextBox ID="TXT_CAPPD" runat="server" ReadOnly="true" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LBL_CAPUNPD" runat="server" Text="CAPITAL UNPAID"></asp:Label>
                </td>
                <td class="style1">
                    <asp:TextBox ID="TXT_CAPUNPD" runat="server" ReadOnly="true" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LBL_CAPRFND" runat="server" Text="CAPITAL REFUND"></asp:Label>
                </td>
                <td class="style1">
                    <asp:TextBox ID="TXT_CAPRFND" runat="server" ReadOnly="true" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LBL_EXPNS" runat="server" Text="EXPENSE"></asp:Label>
                </td>
                <td class="style1">
                    <asp:TextBox ID="TXT_EXPNS" runat="server" ReadOnly="true" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LBL_VALVN" runat="server" Text="VALUATION(COST AS PER))"></asp:Label>
                </td>
                <td class="style1" colspan="2">
                    <asp:TextBox ID="TXT_VALVTN" runat="server" ReadOnly="true" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LBL_ROI" runat="server" Text="RATE OF INTEREST"></asp:Label>
                </td>
                <td class="style1">
                    <asp:TextBox ID="TXT_ROI" runat="server" ReadOnly="true" Width="200px"></asp:TextBox>
                </td>
            </tr>
        </table>
        <%-- </ContentTemplate>
   </asp:UpdatePanel>--%>
    </asp:Panel>
    <cc1:PopupControlExtender ID="PopupControlExtender1" runat="server" TargetControlID="LBL_PROJNAME_M"
        PopupControlID="Panel1" Position="Bottom">
    </cc1:PopupControlExtender>
</asp:Content>
