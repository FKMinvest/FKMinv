<%@ Page Title="DOCUMENT UPLOADS" Language="C#" MasterPageFile="~/MainPage.Master"
    AutoEventWireup="false" CodeFile="DOC_MTNC.aspx.cs" Inherits="Tools_DOC_MTNC" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="HeaderContent1" runat="server" ContentPlaceHolderID="ContenHeader">
    <table width="100%">
        <tr>
            <td align="center" style="font-size: large;">
                DOCUMENT MAINTENANCE
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
                            <asp:Label ID="LBL_SRL" runat="server" Text="CODE"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_SRL" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                            <cc1:ComboBox ID="CMB_DOC_SRL" runat="server" DataTextField="DOC_SRL" DataValueField="DOC_SRL"
                                DropDownStyle="DropDownList" AutoCompleteMode="SuggestAppend" Width="200px" AutoPostBack="true">
                            </cc1:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_DOCTYP" runat="server" Text="DOCUMENT TYPE"></asp:Label>
                        </td>
                        <td>
                            <cc1:ComboBox ID="CMB_DOCTYP" runat="server" DataSourceID="DOC_TYPE" DataTextField="RF_CODE"
                                DataValueField="RF_CODE" DropDownStyle="DropDownList" AutoCompleteMode="SuggestAppend"
                                Width="200px" AutoPostBack="true">
                            </cc1:ComboBox>
                            <asp:SqlDataSource ID="DOC_TYPE" runat="server" ConnectionString="<%$ ConnectionStrings:FKMCONNSTR %>"
                                SelectCommand="SELECT DISTINCT [RF_CODE] FROM [FKM_REFRNC] WHERE [RF_FEILDTYPE] LIKE 'DOCUMENT' ORDER BY [RF_CODE]">
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_FKM_CD" runat="server" Text="FKM CODE"></asp:Label>
                        </td>
                        <td class="style1">
                            <cc1:ComboBox ID="CMB_FKM_CD" runat="server" DataTextField="FKM_PROJNAME" DataValueField="FKM_SRL"
                                DropDownStyle="DropDownList" AutoCompleteMode="SuggestAppend" Width="200px" AutoPostBack="true">
                            </cc1:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:FileUpload ID="FileUploadControl" runat="server" Width="100%" />
                            <%--<asp:Button runat="server" id="UploadButton" text="Upload" onclick="UploadButton_Click" />--%>
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
                            <asp:TextBox ID="TXT_REMX" runat="server" TextMode="MultiLine" Rows="3" MaxLength="50"
                                Width="200px" onchange="rmvqoutes(this)"></asp:TextBox>
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
                    <tr>
                        <td colspan="2">
                            <asp:Label runat="server" ID="StatusLabel" Text="Upload status: " />
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <table>
                    <tr>
                        <td>
                            <asp:CheckBox ID="CHBX_EDIT" runat="server" Text="EDIT" AutoPostBack="true" Width="70px" />
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_SRCH" runat="server" Width="200px"></asp:TextBox>
                        </td>
                        <td>
                             
                        </td>
                    </tr>
                    <tr>
                        <td colspan="10" valign="top">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="DOC_SRL"
                                RowStyle-Wrap="false" HeaderStyle-BackColor="#0071ac" HeaderStyle-ForeColor="WhiteSmoke"
                                RowStyle-ForeColor="Black"  RowStyle-Font-Size="Small" RowStyle-Height="25px"
                                RowStyle-Font-Bold="true">
                                <Columns>
                                    <asp:CommandField ShowSelectButton="true" />
                                    <asp:HyperLinkField DataNavigateUrlFields="DOC_PATH" HeaderText="DOWNLOAD" DataNavigateUrlFormatString="{0}"
                                        Target="_blank" DataTextField="DOC_SRL" />
                                    <asp:BoundField DataField="DOC_TYPE" HeaderText="DOC TYPE" SortExpression="DOC_TYPE"
                                        ItemStyle-Width="50Px" />
                                    <asp:BoundField DataField="DOC_DESC" HeaderText="DESCRIPTION" SortExpression="DOC_DESC"
                                        ItemStyle-Width="200Px" />
                                    <asp:BoundField DataField="DOC_FKM_LNK" HeaderText="FKM  CODE" SortExpression="DOC_FKM_LNK"
                                        ItemStyle-Width="60Px" />
                                        <asp:CommandField ShowDeleteButton="true" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:FKMCONNSTR %>"
        SelectCommand="SELECT [DOC_PATH],[DOC_SRL], [DOC_TYPE], [DOC_DESC], [DOC_FKM_LNK] FROM [FKM_DOC] WHERE (([DOC_SRL] LIKE '%' + @RF_CODE + '%') OR ([DOC_TYPE] LIKE '%' + @RF_CODETYPE + '%') OR ([DOC_DESC] LIKE '%' + @RF_DESCRP + '%') OR ([DOC_FKM_LNK] LIKE '%' + @RF_FEILDTYPE + '%') ) ORDER BY [DOC_SRL] DESC">
        <SelectParameters>
            <asp:ControlParameter ControlID="TXT_SRCH" Name="RF_CODE" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="CMB_DOCTYP" Name="RF_CODETYPE" PropertyName="SelectedValue"
                Type="String" DefaultValue="#" />
            <asp:ControlParameter ControlID="TXT_SRCH" Name="RF_DESCRP" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="CMB_FKM_CD" Name="RF_FEILDTYPE" PropertyName="SelectedValue"
                Type="String" DefaultValue="#" />
            <asp:ControlParameter ControlID="TXT_SRCH" Name="RF_UPDBY" PropertyName="Text" Type="String" />
            <%--
            <asp:ControlParameter ControlID="TXT_SRCH" Name="RF_FEILDTYPE2" 
                PropertyName="SelectedValue" Type="String" DefaultValue="#"  />--%>
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
