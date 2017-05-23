<%@ Page Title="SAFE VAULT" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="false"
    CodeFile="SAFEV_EDIT.aspx.cs" Inherits="Tools_SAFEV_EDIT" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="HeaderContent1" runat="server" ContentPlaceHolderID="ContenHeader">
    <table width="100%">
        <tr>
            <td align="center" style="font-size: large;">
                SAFE VAULT MAINTENANCE (ADD NEW / EDIT)
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
                <asp:TextBox ID="TXT_CODE" runat="server" Width="180px" ReadOnly="true"></asp:TextBox>
                <cc1:ComboBox ID="CMB_CODE" runat="server" DataSourceID="SqlDataSource1" DataTextField="SV_SRL"
                    DataValueField="SV_SRL" DropDownStyle="DropDownList"  
                      AutoCompleteMode="SuggestAppend" Width="180px" AutoPostBack="true">
                </cc1:ComboBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_FKM_CD" runat="server" Text="FKM CODE"></asp:Label>
            </td>
            <td class="style1">
                <cc1:ComboBox ID="CMB_FKM_CD" runat="server" DataTextField="FKM_PROJNAME" DataValueField="FKM_SRL"
                    DropDownStyle="DropDownList" AutoCompleteMode="SuggestAppend" Width="200px">
                </cc1:ComboBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_DESC" runat="server" Text="DESCRIPTION"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TXT_DESC" runat="server" TextMode="MultiLine" Rows="3" MaxLength="100"
                    Width="200px" onchange="rmvqoutes(this)"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_ISSUEDATE" runat="server" Text="SENT DATE"></asp:Label>
            </td>
            <td class="style1">
                <asp:TextBox ID="TXT_ISSUEDATE" runat="server" Width="200px" MaxLength="10"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="TXT_ISSUEDATE"
                    Format="dd/MM/yyyy" />
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
                <asp:Button ID="BTN_ADD" runat="server" Text="SAVE" />
                <asp:Button ID="BTN_UPDT" runat="server" Text="UPDATE" />
            </td>
            <td>
                <asp:Button ID="BTN_CLEAR" runat="server" Text="CLEAR" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_CABNT" runat="server" Text="CABINET NO" Visible="FALSE"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TXT_CABNT" runat="server" MaxLength="10" Width="200px" onchange="rmvqoutes(this)"
                    Visible="FALSE"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LBL_SHELF" runat="server" Text="SHELF" Visible="FALSE"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TXT_SHELF" runat="server" MaxLength="10" Width="200px" onchange="rmvqoutes(this)"
                    Visible="FALSE"></asp:TextBox>
                <%--<cc1:ComboBox    ID="CMB_SHELF" runat="server"
                DropDownStyle="DropDownList"        AutoCompleteMode="SuggestAppend" Width="200px"  >
               
                    <asp:ListItem Text="COMBO BOX COLLECTION" Value="C" />
                    <asp:ListItem Text="KEYWORD COLLECTION" Value="K" />
                    </cc1:ComboBox>--%>
            </td>
        </tr>
    </table>
    
            </td>
            <td valign="top">
            
    <table>
        <tr>
          
            <td>
                <asp:CheckBox ID="CHBX_EDIT" runat="server" Text="EDIT" AutoPostBack="true" />
            </td>
            <td>   <asp:TextBox ID="TXT_SRCH" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>   <asp:Button ID="Button1" runat="server" Text="DOWNLOAD REPORT" />
            </td>
            
        </tr>
        
        <tr>
            <td colspan="5" valign="top">
               
                    
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  
                        DataKeyNames="SV_SRL" RowStyle-Wrap="false" HeaderStyle-BackColor="#0071ac" HeaderStyle-ForeColor="WhiteSmoke"
                        RowStyle-ForeColor="Black"  
                        RowStyle-Font-Size="Small" RowStyle-Height="25px" RowStyle-Font-Bold="true">
                        <Columns>
                            <asp:CommandField ShowSelectButton="true" />
                            <%--<asp:BoundField DataField="SV_SRL" HeaderText="CODE" SortExpression="SV_SRL" ItemStyle-Width="50Px"
                                ItemStyle-HorizontalAlign="Center" />--%>
                            <asp:HyperLinkField DataNavigateUrlFields="DOC_PATH" HeaderText="DOWNLOAD" DataNavigateUrlFormatString="{0}"
                                Target="_blank" DataTextField="SV_SRL" />
                            <asp:BoundField DataField="SV_FKM_LNK" HeaderText="FKM CODE" SortExpression="SV_FKM_LNK" ItemStyle-Width="50Px"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="SV_DESC" HeaderText="DESC" SortExpression="SV_DESC" ItemStyle-Width="200Px"
                                ItemStyle-Wrap="true" />
                            <asp:BoundField DataField="SV_DATE" HeaderText="DATE" SortExpression="SV_DATE" ItemStyle-Width="100Px" />
                            <asp:BoundField DataField="SV_UPDBY" HeaderText="UPDBY" SortExpression="SV_UPDBY"
                                ItemStyle-Width="50Px" />
                        </Columns>
                    </asp:GridView>
                 
            </td>
        </tr>
    </table>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FKMCONNSTR %>"
        SelectCommand="SELECT DISTINCT [SV_SRL] FROM [SAFE_VAULT] ORDER BY 1"></asp:SqlDataSource>
  <%--  <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:FKMCONNSTR %>"
        SelectCommand="SELECT [SV_SRL],[SV_FKM_LNK], [SV_DESC], [SV_DATE], [SV_UPDBY],[DOC_PATH] FROM [SAFE_VAULT],[FKM_DOC] WHERE (([SV_SRL] LIKE '%' + @SV_SRL + '%') OR ([SV_DESC] LIKE '%' + @SV_DESC + '%') OR ([SV_DATE] LIKE '%' + @SV_DATE + '%') OR ([SV_UPDBY] LIKE '%' + @SV_UPDBY + '%')) AND [SV_SRL] = [DOC_SRL] order by [SV_SRL] ">
        <SelectParameters>
            <asp:ControlParameter ControlID="TXT_SRCH" Name="SV_SRL" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="TXT_SRCH" Name="SV_DESC" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="TXT_SRCH" Name="SV_DATE" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="TXT_SRCH" Name="SV_UPDBY" PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>--%>
</asp:Content>
