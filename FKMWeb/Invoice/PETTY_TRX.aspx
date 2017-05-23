<%@ Page Title="PETTY TRANSACTION" Language="C#" MasterPageFile="~/MainPage.Master"
    AutoEventWireup="false" CodeFile="PETTY_TRX.aspx.cs" Inherits="Invoice_PETTY_TRX" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
<script type="text/javascript">
                                                                                      function GridSelectAllColumn(spanChk) {
                                                                                          var oItem = spanChk.children;
                                                                                          var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0]; xState = theBox.checked;
                                                                                          elm = theBox.form.elements;

                                                                                          for (i = 0; i < elm.length; i++) {
                                                                                              if (elm[i].type === 'checkbox' && elm[i].checked != xState)
                                                                                                  elm[i].click();
                                                                                          }
                                                                                      }
</script>
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
                <table cellpadding="1px" cellspacing="2px">
                    <tr>
                        <td>
                            <%--<asp:Label ID="LBL_BTRXTYP" runat="server" Text="TYPE"></asp:Label>--%>
                        </td>
                        <td>
                            <cc1:ComboBox ID="CMB_BTRXTYP" runat="server" DropDownStyle="Simple"  
                                  AutoCompleteMode="SuggestAppend" Width="200px" Visible="false">
                                <asp:ListItem Selected="true" Text="PETTY CASH" Value="PETTY" />
                                <%--<asp:ListItem Text="CAPITAL CALL" Value="CAP" />
                    <asp:ListItem Text="EXPENSE" Value="EXP" />
                    <asp:ListItem Text="INCOME" Value="INC" />
                    <asp:ListItem Text="REDEMPTION" Value="RDM" />
                    <asp:ListItem Text="HOLDING COMPANY" Value="HOLD" />
                    <asp:ListItem Text="BANK BALANCE" Value="BANK" />--%>
                            </cc1:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_BTRX_REFSRL" runat="server" Text="CODE"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_BTRX_REFSRL" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                            <cc1:ComboBox ID="CMB_BTRX_REFSRL" runat="server" DataTextField="BTRX_REFSRL" DataValueField="BTRX_REFSRL"
                                DropDownStyle="DropDownList" AutoCompleteMode="SuggestAppend" Width="200px" AutoPostBack="true"
                                >
                            </cc1:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_BTRX_BANK_NAME" runat="server" Text="NAME"></asp:Label>
                        </td>
                        <td>
                            <cc1:ComboBox ID="CMB_BTRX_BANK_NAME" runat="server" DataTextField="BNK_NAME" DataValueField="BNK_REFSRL"
                                DropDownStyle="DropDownList" AutoCompleteMode="SuggestAppend" Width="200px" AutoPostBack="true">
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
                    SelectCommand="SELECT DISTINCT [BTRX_LEDGER] FROM [BANK_TRX] WHERE ([BTRX_TYPE] LIKE 'PETTY') ORDER BY [BTRX_LEDGER]">
                </asp:SqlDataSource>
            </td>
        </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_BTRX_REFDESC" runat="server" Text="REF DESC"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_BTRX_REFDESC" runat="server" TextMode="MultiLine" Rows="3" Width="200px"
                                onchange="rmvqoutes(this)"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_BTRXCDTYP" runat="server" Text="TRX TYPE"></asp:Label>
                        </td>
                        <td>
                            <cc1:ComboBox ID="CMB_BTRXCDTYP" runat="server" DropDownStyle="Simple"  
                                  AutoCompleteMode="SuggestAppend" Width="200px">
                                <asp:ListItem Selected="true" Text="DEBIT" Value="DEBIT" />
                                <asp:ListItem Text="CREDIT" Value="CREDIT" />
                            </cc1:ComboBox>
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
                            <asp:Label ID="LBL_BTRX_VALUEDT" runat="server" Text="VALUE DATE"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_BTRX_VALUEDT" runat="server" Width="200px"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TXT_BTRX_VALUEDT"
                                Format="dd/MM/yyyy" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_BTRX_VALUEAMT" runat="server" Text="AMOUNT"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_BTRX_VALUEAMT" runat="server" Width="200px" onchange="numformat(this)"></asp:TextBox>
                            <div style="display: none;">
                                <asp:TextBox ID="TXT_BTRX_VALUEAMT_OLD" runat="server" Width="200px" ReadOnly="true"
                                    onchange="numformat(this)"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_BTRX_NARR" runat="server" Text="NARRATION"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_BTRX_NARR" runat="server" TextMode="MultiLine" Rows="5" Width="200px"
                                onchange="rmvqoutes(this)"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_BTRX_STATUS" runat="server" Text="STATUS"></asp:Label>
                        </td>
                        <td>
                            <cc1:ComboBox ID="CMB_BTRX_STATUS" runat="server" DropDownStyle="Simple"  
                                  AutoCompleteMode="SuggestAppend" Width="200px">
                                <asp:ListItem Selected="true" Text="VALID" Value="V" />
                                <asp:ListItem Text="IN-VALID" Value="I" />
                            </cc1:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="BTN_ADD" runat="server" Text="SAVE"   />
                            <asp:Button ID="BTN_UPDT" runat="server" Text="UPDATE"  />
                            <asp:Button ID="BTN_DELETE" runat="server" Text="DELETE"  />
                        </td>
                        <td>
                            <asp:Button ID="BTN_CLEAR" runat="server" Text="CLEAR"  />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_BTRX_ISSDT" runat="server" Text="ISSUE DATE" Visible="false"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_BTRX_ISSDT" runat="server" Width="200px" Visible="false"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TXT_BTRX_ISSDT"
                                Format="dd/MM/yyyy" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_BTRX_CURRCD" runat="server" Text="CURRENCY" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <cc1:ComboBox ID="CMB_BTRX_CURRCD" runat="server" DropDownStyle="Simple"  
                                  AutoCompleteMode="SuggestAppend" Width="200px" Visible="false">
                                <asp:ListItem Selected="true" Text="KWD" Value="KWD" />
                            </cc1:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_BTRX_BANK_ACC" runat="server" Text="BANK ACC NO" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_BTRX_BANK_ACC" runat="server" TextMode="MultiLine" Rows="2"
                                MaxLength="50" Width="200px" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <table>
                    <tr>
                        <td>
                            <asp:CheckBox ID="CHBX_EDIT" runat="server" Text="EDIT" AutoPostBack="true"  />
                        </td>
                        <%--  <td>
                <asp:Label ID="LBL_MONTH" runat="server" Text="MONTH"></asp:Label>
            </td>--%>
                        <td class="style1" align="center" colspan="2">
                            <cc1:ComboBox ID="CMB_MONTH" runat="server" AutoPostBack="true" DataTextField="MONTH"
                                DataValueField="CD" DropDownStyle="DropDown"    
                                AutoCompleteMode="SuggestAppend" Width="200px"  >
                            </cc1:ComboBox>
                        </td>
                        <td>
                        
                    <asp:TextBox ID="TXT_SRCH" runat="server" Width="400px" Height="20PX" Font-Bold="true"
                        Font-Size="Large" OnTextChanged="TXTSRCH_TextChanged" ToolTip="Search Bar"  AutoPostBack="true"  ></asp:TextBox> </td>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="DOWNLOAD REPORT"  />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="10">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="BTRX_REFSRL"
                                RowStyle-Wrap="false" HeaderStyle-BackColor="#0071ac" HeaderStyle-ForeColor="WhiteSmoke"
                                RowStyle-ForeColor="Black"        RowStyle-Font-Size="Small" RowStyle-Height="25px" RowStyle-Font-Bold="true">
                                <Columns> 
                                
                <asp:CommandField ShowSelectButton="true" />
                                <asp:TemplateField HeaderText="Select">
         <ItemTemplate>
            <asp:CheckBox ID="chkRow" runat="server" />
         </ItemTemplate>
         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
         <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
         <HeaderTemplate>
             <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="GridSelectAllColumn(this);" />
         </HeaderTemplate>
      </asp:TemplateField> 
                                    <asp:BoundField DataField="BTRX_REFSRL" HeaderText="SRL" SortExpression="BTRX_REFSRL" /> 
                                    <asp:BoundField DataField="BTRX_VALUEDT" HeaderText=" DATE" SortExpression="BTRX_VALUEDT"
                                        ItemStyle-Width="80Px" />
                                     <asp:BoundField DataField="BTRX_REFDESC" HeaderText="DESCRIPTION" 
                SortExpression="BTRX_REFDESC" ItemStyle-Width="100Px" />
                                    <asp:BoundField DataField="BTRX_LEDGER" HeaderText="LEDGER TYPE" 
                SortExpression="BTRX_LEDGER" ItemStyle-Width="100Px"/>
                                   <%-- <asp:HyperLinkField DataNavigateUrlFields="BTRX_REFSRL,BTRX_REFDESC" HeaderText="DESCRIPTION"
                                        DataNavigateUrlFormatString="PETTY_TRX.aspx?REFSRL={0}" Target="_blank" DataTextField="BTRX_REFDESC" />--%>
                                   
                                    <%--  <asp:BoundField DataField="BTRX_FKMREF" HeaderText="FKM REF" 
                SortExpression="BTRX_FKMREF" ItemStyle-Width="60Px" />            
            <asp:BoundField DataField="BTRX_NARR" HeaderText="NARRATION" 
                SortExpression="BTRX_NARR" ItemStyle-Width="250Px" />--%>
                                    <%--<asp:BoundField DataField="BTRX_BANK_NAME" HeaderText="BANK_NAME" 
                SortExpression="BTRX_DEBIT_NAME" ItemStyle-Width="60Px" />--%>
                                    <asp:BoundField DataField="BTRX_VALUEAMTC" HeaderText="CREDIT" SortExpression="BTRX_VALUEAMTC"
                                        ItemStyle-Width="60Px"   ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="BTRX_VALUEAMTD" HeaderText="DEBIT" SortExpression="BTRX_VALUEAMTD"
                                        ItemStyle-Width="60Px" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="BTRX_UPDBY" HeaderText="UPDBY" SortExpression="BTRX_UPDBY"
                                        ItemStyle-Width="50Px" />
                                    <%-- <asp:BoundField DataField="BTRX_UPDDATE" HeaderText="UPD DATE" 
                SortExpression="BTRX_UPDDATE" ItemStyle-Width="100Px" />--%>
                                    <%--    <asp:HyperLinkField DataNavigateUrlFields="BTRX_REFSRL" HeaderText="REF SRL" 
             DataNavigateUrlFormatString="PETTY_TRX.aspx?REFSRL={0}" Target="_blank" DataTextField="BTRX_REFSRL" />--%>
                                    <%--<asp:HyperLinkField  DataNavigateUrlFields="BTRX_REFSRL" HeaderText="DOWNLOAD" 
             DataNavigateUrlFormatString="BANK_TRX_LIST.aspx?REFSRL={0}" Target="_blank" DataTextField="BTRX_REFSRL" />
            
            <asp:BoundField DataField="BTRX_VALUEAMT" HeaderText="BTRX_VALUEAMT" 
                SortExpression="BTRX_VALUEAMT" Visible="false" />
            <asp:BoundField DataField="BTRX_CURRCD" HeaderText="BTRX_CURRCD" 
                SortExpression="BTRX_CURRCD"  Visible="false" />--%>
                                    <asp:BoundField DataField="STATUS" HeaderText="STATUS" SortExpression="STATUS"
                                        ItemStyle-Width="50Px" />
                <asp:CommandField  ShowDeleteButton="true" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
