<%@ Page Title="INVOICE MAINTENANCE" Language="C#" MasterPageFile="~/MainPage.Master"
    AutoEventWireup="false" CodeFile="INVOICE_MTNC.aspx.cs" Inherits="Invoice_INVOICE_MTNC" %>

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
                <%--<asp:Label ID="LBL_INVCTYP" runat="server" Text="TYPE"></asp:Label>--%>
            </td>
            <td>
                <cc1:ComboBox   ID="CMB_INVCTYP" runat="server" DropDownStyle="Simple"
                        AutoCompleteMode="SuggestAppend"
                    Width="200px" Visible="false">
                    <asp:ListItem Selected="true" Text="CAPITAL CALL" Value="CAP" />
                    <asp:ListItem Text="EXPENSE" Value="EXP" />
                    <asp:ListItem Text="INCOME" Value="INC" />
                    <asp:ListItem Text="REDEMPTION" Value="RDM" />
                    <asp:ListItem Text="DISTRIBUTION" Value="DST" />
                    <%--<asp:ListItem Text="HOLDING COMPANY" Value="HOLD" />
                    <asp:ListItem Text="BANK BALANCE" Value="BANK" />
                    <asp:ListItem Text="PETTY CASH" Value="PETTY" />--%>
                </cc1:ComboBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_INVC_REFSRL" runat="server" Text="CODE"></asp:Label>
            </td>
            <td>
                <asp:TextBox   ID="TXT_INVC_REFSRL" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                <cc1:ComboBox   ID="CMB_INVC_REFSRL" runat="server" 
                    DataTextField="INVC_REFSRL" DataValueField="INVC_REFSRL" DropDownStyle="DropDownList"
                    AutoCompleteMode="SuggestAppend" Width="200px" AutoPostBack="true">
                </cc1:ComboBox> 
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
                </cc1:ComboBox> 
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_INVC_REFDESC" runat="server" Text="REF DESC"></asp:Label>
            </td>
            <td>
                <asp:TextBox   ID="TXT_INVC_REFDESC" runat="server" TextMode="MultiLine"
                    Rows="3" Width="200px"  onchange="rmvqoutes(this)"></asp:TextBox>
            </td>
        </tr>
        <tr>
        <td>
            <asp:Label ID="LBL_INVC_ISSDT" runat="server" Text="ISSUE DATE"></asp:Label>
        </td>
        <td class="style1">
            <asp:TextBox ID="TXT_INVC_ISSDT" runat="server" Width="200px"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TXT_INVC_ISSDT"
                Format="dd/MM/yyyy" />
        </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_INVC_DEBIT_NAME" runat="server" Text="DEBIT ACCOUNT"></asp:Label>
            </td>
            <td>
                <cc1:ComboBox   ID="CMB_INVC_DEBIT_NAME" runat="server"  
                    DataTextField="BNK_NAME" DataValueField="BNK_REFSRL" DropDownStyle="DropDownList"
                    AutoCompleteMode="SuggestAppend" Width="200px" AutoPostBack="true">
                </cc1:ComboBox> 
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_INVC_DEBIT_ACC" runat="server" Text="DEBIT ACC NO"></asp:Label>
            </td>
            <td>
                <asp:TextBox   ID="TXT_INVC_DEBIT_ACC" runat="server" TextMode="MultiLine"
                    Rows="2" MaxLength="50" Width="200px"></asp:TextBox>
            </td>
            </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_INVC_CREDIT_NAME" runat="server" Text="CREDIT ACCOUNT"></asp:Label>
            </td>
            <td>
                <cc1:ComboBox   ID="CMB_INVC_CREDIT_NAME" runat="server"  
                    DataTextField="BNK_NAME" DataValueField="BNK_REFSRL" DropDownStyle="DropDownList"
                    AutoCompleteMode="SuggestAppend" Width="200px" AutoPostBack="true">
                </cc1:ComboBox> 
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_INVC_CREDIT_ACC" runat="server" Text="CREDIT ACC NO" Width="150px"></asp:Label>
            </td>
            <td>
                <asp:TextBox   ID="TXT_INVC_CREDIT_ACC" runat="server" TextMode="MultiLine"
                    Rows="2" MaxLength="50" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_INVC_CURRCD" runat="server" Text="CURRENCY"></asp:Label>
            </td>
            <td>
              <%--  <cc1:ComboBox   ID="CMB_INVC_CURRCD" runat="server" DropDownStyle="Simple"
                        AutoCompleteMode="SuggestAppend"
                    Width="200px" >
                    <asp:ListItem Selected="true" Text="US DOLLAR" Value="US DOLLAR" />
                    <asp:ListItem Text="UK POUND" Value="UK POUND" />
                    <asp:ListItem Text="EURO" Value="EURO" />
                    <asp:ListItem  Text="KWD" Value="KWD" />
                </cc1:ComboBox>
            </td>
            <td class="style1">--%>
                <cc1:ComboBox ID="CMB_INVC_CURRCD" runat="server" Width="200px" DataSourceID="BNK_CURR"
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
                <asp:Label ID="LBL_INVC_VALUEDT" runat="server" Text="VALUE DATE"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox   ID="TXT_INVC_VALUEDT" runat="server" Width="200px" ></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TXT_INVC_VALUEDT"
                    Format="dd/MM/yyyy" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_INVC_VALUEAMT" runat="server" Text="AMOUNT"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox   ID="TXT_INVC_VALUEAMT" runat="server" Width="200px"  onchange="numformat(this)" ></asp:TextBox>
                
<%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Numbers, Custom"
    ValidChars=".," TargetControlID="TXT_INVC_VALUEAMT" />--%>
           <%--     <cc1:MaskedEditExtender ID="MaskedEditExtender7" TargetControlID="TXT_INVC_VALUEAMT"
                    runat="server" Mask="999,999,999.99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                    OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft"
                    ErrorTooltipEnabled="True" />--%>

                    <div style="display:none;">

                <asp:TextBox   ID="TXT_INVC_VALUEAMT_OLD" runat="server" Width="200px" ReadOnly="true"  onchange="numformat(this)" ></asp:TextBox>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_INVC_NARR" runat="server" Text="NARRATION"></asp:Label>
            </td>
            <td>
                <asp:TextBox   ID="TXT_INVC_NARR" runat="server" TextMode="MultiLine"
                    Rows="5" Width="200px"  onchange="rmvqoutes(this)"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_INVC_STATUS" runat="server" Text="STATUS"></asp:Label>
            </td>
            <td>
                <cc1:ComboBox   ID="CMB_INVC_STATUS" runat="server" DropDownStyle="Simple"
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
                <asp:CheckBox ID="CHBX_EDIT" runat="server" Text="EDIT" AutoPostBack="true" Width="100px" />
            </td>
            
                    </tr>
                    <tr>
            <td  valign="top" align="right">
            <asp:GridView ID="GridView1" runat="server" 
            AutoGenerateColumns="False"    DataKeyNames="INVC_REFSRL"
            RowStyle-Wrap="false" HeaderStyle-BackColor="#0071ac" HeaderStyle-ForeColor="WhiteSmoke"
            RowStyle-ForeColor="Black"   RowStyle-Font-Size="Small"
            RowStyle-Height="25px"  RowStyle-Font-Bold="true" >
        <Columns>
        
                <asp:CommandField ShowSelectButton="true"   />
            <asp:BoundField DataField="INVC_REFSRL" HeaderText="REF SRL" 
                SortExpression="INVC_REFSRL" ItemStyle-Width="100Px" />
             <%--<asp:HyperLinkField DataNavigateUrlFields="INVC_REFSRL" HeaderText="REF SRL" 
             DataNavigateUrlFormatString="INVOICE_MTNC.aspx?REFSRL={0}" Target="_blank" DataTextField="INVC_REFSRL" />--%>
            <asp:BoundField DataField="INVC_ISSDT" HeaderText="ISSUE DATE" 
                SortExpression="INVC_ISSDT" ItemStyle-Width="100Px" />
            <asp:BoundField DataField="INVC_TYPE" HeaderText="INVC TYPE" 
                SortExpression="INVC_TYPE" ItemStyle-Width="50Px"/>
            <asp:BoundField DataField="INVC_FKMREF" HeaderText="FKM REF" 
                SortExpression="INVC_FKMREF" ItemStyle-Width="60Px" />            
            <asp:BoundField DataField="INVC_NARR" HeaderText="NARRATION" 
                SortExpression="INVC_NARR" ItemStyle-Width="250Px" />
            <asp:BoundField DataField="INVC_REFDESC" HeaderText="DESCRIPTION" 
                SortExpression="INVC_REFDESC" ItemStyle-Width="100Px" />
          <%--  <asp:BoundField DataField="INVC_DEBIT_NAME" HeaderText="DEBIT_NAME" 
                SortExpression="INVC_DEBIT_NAME" ItemStyle-Width="60Px" />
            <asp:BoundField DataField="INVC_CREDIT_NAME" HeaderText="INVC_CREDIT_NAME" 
                SortExpression="INVC_CREDIT_NAME" ItemStyle-Width="60Px" /> --%>
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
            </td>
        </tr>
    </table>
</asp:Content>
