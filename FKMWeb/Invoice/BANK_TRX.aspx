<%@ Page Title="BANK TRANSACTION" Language="C#" MasterPageFile="~/MainPage.Master"
    AutoEventWireup="false" CodeFile="BANK_TRX.aspx.cs" Inherits="Invoice_BANK_TRX" %>

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
            <td valign="top">
             <table>
        <tr>
            <td>
                <%--<asp:Label ID="LBL_BTRXTYP" runat="server" Text="TYPE"></asp:Label>--%>
            </td>
            <td>
                <cc1:ComboBox   ID="CMB_BTRXTYP" runat="server" DropDownStyle="Simple"
                        AutoCompleteMode="SuggestAppend"
                    Width="200px" Visible="false">
                    <asp:ListItem Text="HOLDING COMPANY" Value="HOLD" />
                    <asp:ListItem Selected="true" Text="BANK BALANCE" Value="BANK" />
                </cc1:ComboBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_BTRX_REFSRL" runat="server" Text="CODE"></asp:Label>
            </td>
            <td>
                <asp:TextBox   ID="TXT_BTRX_REFSRL" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                <cc1:ComboBox   ID="CMB_BTRX_REFSRL" runat="server"  
                    DataTextField="BTRX_REFSRL" DataValueField="BTRX_REFSRL" DropDownStyle="DropDownList"
                    AutoCompleteMode="SuggestAppend" Width="200px" AutoPostBack="true">
                </cc1:ComboBox>
               
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_FKM_CD" runat="server" Text="FKM CODE"  ></asp:Label>
            </td>
            <td class="style1">
                <cc1:ComboBox   ID="CMB_FKM_CD" runat="server"  
                    DataTextField="FKM_PROJNAME" DataValueField="FKM_SRL" DropDownStyle="DropDownList"
                    AutoCompleteMode="SuggestAppend" Width="200px" AutoPostBack="true"  >
                </cc1:ComboBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="LEDGER TYPE"></asp:Label>
            </td>
            <td  >
                <cc1:ComboBox ID="CMB_LEDGER" runat="server" Width="200px" DataSourceID="FKM_BTRX_LEDGER"
                    DataTextField="BTRX_LEDGER" DataValueField="BTRX_LEDGER" DropDownStyle="DropDown"
                        AutoCompleteMode="SuggestAppend">
                </cc1:ComboBox>
                <asp:SqlDataSource ID="FKM_BTRX_LEDGER" runat="server" ConnectionString="<%$ ConnectionStrings:FKMCONNSTR %>"
                    SelectCommand="SELECT DISTINCT [BTRX_LEDGER] FROM [BANK_TRX] WHERE ([BTRX_TYPE] NOT LIKE 'PETTY') ORDER BY [BTRX_LEDGER]">
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_BTRX_REFDESC" runat="server" Text="REF DESC"></asp:Label>
            </td>
            <td>
                <asp:TextBox   ID="TXT_BTRX_REFDESC" runat="server" TextMode="MultiLine"
                    Rows="3" Width="200px" onchange="rmvqoutes(this)" ></asp:TextBox>
            </td>
        </tr>
        <tr>
        <td>
            <asp:Label ID="LBL_BTRX_ISSDT" runat="server" Text="ISSUE DATE" ></asp:Label>
        </td>
        <td class="style1">
            <asp:TextBox ID="TXT_BTRX_ISSDT" runat="server" Width="200px" ></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TXT_BTRX_ISSDT"
                Format="dd/MM/yyyy" />
        </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_BTRX_VALUEDT" runat="server" Text="ACKNOWLEDGE DATE" Width="220PX"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox   ID="TXT_BTRX_VALUEDT" runat="server" Width="200px"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TXT_BTRX_VALUEDT"
                    Format="dd/MM/yyyy" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_BTRX_BANK_NAME" runat="server" Text="BANK NAME"></asp:Label>
            </td>
            <td>
                <cc1:ComboBox   ID="CMB_BTRX_BANK_NAME" runat="server"  
                    DataTextField="BNK_NAME" DataValueField="BNK_REFSRL" DropDownStyle="DropDownList"
                    AutoCompleteMode="SuggestAppend" Width="300px" AutoPostBack="true">
                </cc1:ComboBox><%--
                <asp:SqlDataSource ID="CMB_BTRX_BANK_NAME1" runat="server" ConnectionString="<%$ ConnectionStrings:FKMCONNSTR %>"
                    SelectCommand="SELECT DISTINCT [BNK_REFSRL], [BNK_NAME]  FROM [BANK_INFO]  WHERE [BNK_TYPE] IN ('B','I','H','P')  ORDER BY [BNK_REFSRL]">
                </asp:SqlDataSource>--%>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_BTRX_BANK_ACC" runat="server" Text="BANK ACC NO" Width="220PX"></asp:Label>
            </td>
            <td>
                <asp:TextBox   ID="TXT_BTRX_BANK_ACC" runat="server" TextMode="MultiLine"
                    Rows="2" MaxLength="50" Width="200px"></asp:TextBox>
            </td>
            </tr>
       <%-- <tr>
            <td>
                <asp:Label ID="LBL_BTRX_BANK_NAME" runat="server" Text="CREDIT ACCOUNT"></asp:Label>
            </td>
            <td>
                <cc1:ComboBox   ID="CMB_BTRX_CREDIT_NAME" runat="server" DataSourceID="CMB_BTRX_DEBIT_NAME2"
                    DataTextField="BNK_NAME" DataValueField="BNK_REFSRL" DropDownStyle="DropDownList"
                    AutoCompleteMode="SuggestAppend" Width="200px" AutoPostBack="true">
                </cc1:ComboBox>
                <asp:SqlDataSource ID="CMB_BTRX_DEBIT_NAME2" runat="server" ConnectionString="<%$ ConnectionStrings:FKMCONNSTR %>"
                    SelectCommand="SELECT DISTINCT [BNK_REFSRL], [BNK_NAME]  FROM [BANK_INFO] WHERE [BNK_TYPE] IN ('P') ORDER BY [BNK_REFSRL]">
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_BTRX_CREDIT_ACC" runat="server" Text="CREDIT ACC NO"></asp:Label>
            </td>
            <td>
                <asp:TextBox   ID="TXT_BTRX_CREDIT_ACC" runat="server" TextMode="MultiLine"
                    Rows="2" MaxLength="50" Width="200px"></asp:TextBox>
            </td>
        </tr>--%>
        
        <tr>
            <td>
                <asp:Label ID="LBL_BTRX_CURRCD" runat="server" Text="CURRENCY"></asp:Label>
            </td>
        <%--    <td>
                <cc1:ComboBox   ID="CMB_BTRX_CURRCD" runat="server" DropDownStyle="Simple"
                        AutoCompleteMode="SuggestAppend"
                    Width="200px" >
                    <asp:ListItem Selected="true" Text="KWD" Value="KWD" />
                    <asp:ListItem Text="US DOLLAR" Value="US DOLLAR" />
                    <asp:ListItem Text="UK POUND" Value="UK POUND" />
                    <asp:ListItem Text="EURO" Value="EURO" />
                </cc1:ComboBox>
            </td>--%>
            <td class="style1">
                <cc1:ComboBox ID="CMB_BTRX_CURRCD" runat="server" Width="200px" DataSourceID="BNK_CURR"
                    DataTextField="RF_DESCRP" DataValueField="RF_DESCRP" DropDownStyle="DropDownList"
                    AutoCompleteMode="SuggestAppend" Enabled="false">
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
                <asp:Label ID="LBL_BTRXCDTYP" runat="server" Text="TRX TYPE"></asp:Label>
            </td>
            <td>
                <cc1:ComboBox   ID="CMB_BTRXCDTYP" runat="server" DropDownStyle="Simple"
                        AutoCompleteMode="SuggestAppend"
                    Width="200px"  >
                    <asp:ListItem Selected="true" Text="DEBIT" Value="DEBIT" />
                    <asp:ListItem Text="CREDIT" Value="CREDIT" />
                </cc1:ComboBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_BTRX_VALUEAMT" runat="server" Text="AMOUNT"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox   ID="TXT_BTRX_VALUEAMT" runat="server" Width="200px"  onchange="numformat(this)" ></asp:TextBox>
                
<%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Numbers, Custom"
    ValidChars=".," TargetControlID="TXT_BTRX_VALUEAMT" />--%>
          <%--      <cc1:MaskedEditExtender ID="MaskedEditExtender7" TargetControlID="TXT_BTRX_VALUEAMT"
                    runat="server" Mask="999,999,999.99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                    OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft"
                    ErrorTooltipEnabled="True" />--%>

                    <div style="display:none;">

                <asp:TextBox   ID="TXT_BTRX_VALUEAMT_OLD" runat="server" Width="200px" ReadOnly="true"  onchange="numformat(this)" ></asp:TextBox>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_BTRX_NARR" runat="server" Text="NARRATION"></asp:Label>
            </td>
            <td>
                <asp:TextBox   ID="TXT_BTRX_NARR" runat="server" TextMode="MultiLine"
                    Rows="5" Width="200px" onchange="rmvqoutes(this)" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_BTRX_STATUS" runat="server" Text="STATUS"></asp:Label>
            </td>
            <td>
                <cc1:ComboBox   ID="CMB_BTRX_STATUS" runat="server" DropDownStyle="Simple"
                        AutoCompleteMode="SuggestAppend"
                    Width="200px" >
                    <asp:ListItem Selected="true" Text="VALID" Value="V" />
                    <asp:ListItem Text="IN-VALID" Value="I" />
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
            <td colspan="10" valign="top" align="right">
                                
            <asp:GridView ID="GridView1" runat="server" 
            AutoGenerateColumns="False"    DataKeyNames="BTRX_REFSRL"
            RowStyle-Wrap="false" HeaderStyle-BackColor="#0071ac" HeaderStyle-ForeColor="WhiteSmoke"
            RowStyle-ForeColor="Black"     HeaderStyle-  RowStyle-Font-Size="Small"
            RowStyle-Height="25px"  RowStyle-Font-Bold="true" >
        <Columns> 
        
                <asp:CommandField ShowSelectButton="true" />
                <asp:TemplateField>
            <ItemTemplate>
                <asp:CheckBox ID="chkRow" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
           <%-- <asp:BoundField DataField="BTRX_REFDESC" HeaderText="DESCRIPTION" 
                SortExpression="BTRX_REFDESC" ItemStyle-Width="100Px" />--%>
            <asp:BoundField DataField="BTRX_REFSRL" HeaderText="REF SRL" 
                SortExpression="BTRX_REFSRL"   />
                 <asp:HyperLinkField DataNavigateUrlFields="BTRX_REFSRL,BTRX_REFDESC" HeaderText="DESCRIPTION" 
             DataNavigateUrlFormatString="BANK_TRX.aspx?REFSRL={0}" Target="_blank" DataTextField="BTRX_REFDESC" />

            <asp:BoundField DataField="BTRX_ISSDT" HeaderText="ISSUE DATE" 
                SortExpression="BTRX_ISSDT" ItemStyle-Width="100Px" />
            <asp:BoundField DataField="BTRX_VALUEDT" HeaderText="ACKNOWLEDGE DATE" 
                SortExpression="BTRX_VALUEDT" ItemStyle-Width="100Px" />
            <%--<asp:BoundField DataField="BTRX_TYPE" HeaderText="BTRX TYPE" 
                SortExpression="BTRX_TYPE" ItemStyle-Width="50Px"/>--%>
            <asp:BoundField DataField="BTRX_CD_TYPE" HeaderText="BTRX CD TYPE" 
                SortExpression="BTRX_CD_TYPE" ItemStyle-Width="50Px"/>
          <%--  <asp:BoundField DataField="BTRX_FKMREF" HeaderText="FKM REF" 
                SortExpression="BTRX_FKMREF" ItemStyle-Width="60Px" />            
            <asp:BoundField DataField="BTRX_NARR" HeaderText="NARRATION" 
                SortExpression="BTRX_NARR" ItemStyle-Width="250Px" />--%>
            <asp:BoundField DataField="BNK_NAME" HeaderText="BANK_NAME" 
                SortExpression="BTRX_DEBIT_NAME" ItemStyle-Width="60Px" />
            <asp:BoundField DataField="BTRX_VALUEAMTC" HeaderText="CREDIT AMT" 
                SortExpression="BTRX_VALUEAMTC" ItemStyle-Width="60Px" />
            <asp:BoundField DataField="BTRX_VALUEAMTD" HeaderText="DEBIT AMT" 
                SortExpression="BTRX_VALUEAMTD" ItemStyle-Width="60Px" />
            <asp:BoundField DataField="BTRX_UPDBY" HeaderText="UPDBY" 
                SortExpression="BTRX_UPDBY" ItemStyle-Width="50Px" />
           <%-- <asp:BoundField DataField="BTRX_UPDDATE" HeaderText="UPD DATE" 
                SortExpression="BTRX_UPDDATE" ItemStyle-Width="100Px" />--%><%--
                 <asp:HyperLinkField DataNavigateUrlFields="BTRX_REFSRL" HeaderText="REF SRL" 
             DataNavigateUrlFormatString="BANK_TRX.aspx?REFSRL={0}" Target="_blank" DataTextField="BTRX_REFSRL" />--%>
             <asp:HyperLinkField DataNavigateUrlFields="BTRX_REFSRL" HeaderText="DOWNLOAD" 
             DataNavigateUrlFormatString="BANK_TRX_LIST.aspx?REFSRL={0}" Target="_blank" DataTextField="BTRX_REFSRL" />
        </Columns>
    </asp:GridView>
            </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
