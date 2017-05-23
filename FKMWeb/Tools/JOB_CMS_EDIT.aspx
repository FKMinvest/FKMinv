<%@ Page Title="TASK CONTROL" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="false"
    CodeFile="JOB_CMS_EDIT.aspx.cs" Inherits="Tools_JOB_CMS_EDIT" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.6.4/jquery.min.js"></script>
    <link rel="stylesheet" type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.21/themes/redmond/jquery-ui.css" />
    <link rel="stylesheet" type="text/css" href="../js/jquery.ptTimeSelect.css" />
    <script type="text/javascript" src="../js/jquery.ptTimeSelect.js"></script>
</asp:Content>
<asp:Content ID="HeaderContent1" runat="server" ContentPlaceHolderID="ContenHeader">
    <table width="100%">
        <tr>
            <td align="center" style="font-size: large;">
                TASK MAINTENANCE
                <hr />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <%--   <h2>
        Welcome to ASP.NET!
    </h2>--%>
    <script type="text/javascript">
        $(document).ready(function () {
            // find the input fields and apply the time select to them.
            $(document.getElementById("<%= TXT_OPNTIME.ClientID %>")).ptTimeSelect();
            $(document.getElementById("<%= TXT_ESTCLSTIME.ClientID %>")).ptTimeSelect();
            $(document.getElementById("<%= TXT_CLSTIME.ClientID %>")).ptTimeSelect();


            //             $(document.getElementById("<%= TXT_OPNTIME.ClientID %>")).focusout( function () { onSuccess(); });
            //                              onClose: function (i) {  TXT_TIME_Load();  
            // //end ptTimeSelect()
        });  // end ready()

        //         function TXT_TIME_Load() {
        //             PageMethods.TXT_OPNTIME_Load(OnSuccess);
        //         }
        //         
        //  function onSuccess() {
        //          alert('ii');
        //      }
    </script>
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
                            <cc1:ComboBox ID="CMB_CODE" runat="server" DataSourceID="SqlDataSource1" DataTextField="JB_SRLNO"
                                DataValueField="JB_SRLNO" DropDownStyle="DropDownList" AutoCompleteMode="SuggestAppend"
                                Width="200px" AutoPostBack="true">
                            </cc1:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_CDTYP" runat="server" Text="CODE TYPE"></asp:Label>
                        </td>
                        <td>
                            <cc1:ComboBox ID="CMB_CDTYP" runat="server" DropDownStyle="DropDownList" AutoCompleteMode="SuggestAppend"
                                Width="200px">
                                <asp:ListItem Text="GENERAL" Value="GENERAL" />
                                <asp:ListItem Text="REVIEW" Value="REVIEW" />
                            </cc1:ComboBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_USR_REFSRL" runat="server" Text="USER NAME"></asp:Label>
                        </td>
                        <td>
                            <cc1:ComboBox ID="CMB_USR_REFSRL" runat="server" DataTextField="USR_NAME" DataValueField="USR_USERID"
                                DropDownStyle="DropDownList" AutoCompleteMode="SuggestAppend" Width="200px" AutoPostBack="false">
                            </cc1:ComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_DESC" runat="server" Text="JOB DESCRIPTION"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_DESC" runat="server" TextMode="MultiLine" Rows="10" Width="200px"
                                onchange="rmvqoutes(this)"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_OPENDATE" runat="server" Text="START DATE"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_OPNDATE" runat="server" Width="200px" MaxLength="10" AutoPostBack="true"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="TXT_OPNDATE"
                                Format="dd/MM/yyyy" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="START TIME"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_OPNTIME" runat="server" Width="200px" MaxLength="10" AutoPostBack="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_ESTCLSDATE" runat="server" Text="EST.&nbsp;CLOSE&nbsp;DATE"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_ESTCLSDATE" runat="server" Width="200px" MaxLength="10" AutoPostBack="true"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TXT_ESTCLSDATE"
                                Format="dd/MM/yyyy" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_ESTCLSTIME" runat="server" Text="EST. CLOSE TIME"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_ESTCLSTIME" runat="server" Width="200px" MaxLength="10" AutoPostBack="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_ESTDURATION" runat="server" Text="EST. DURATION"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_ESTDURATION" runat="server" MaxLength="10" Width="200px" onchange="rmvqoutes(this)"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_CLSDATE" runat="server" Text="CLOSE DATE"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_CLSDATE" runat="server" Width="200px" MaxLength="10" AutoPostBack="true"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TXT_CLSDATE"
                                Format="dd/MM/yyyy" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_CLSTIME" runat="server" Text="CLOSE TIME"></asp:Label>
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TXT_CLSTIME" runat="server" Width="200px" MaxLength="10" AutoPostBack="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_DURATION" runat="server" Text="DURATION"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TXT_DURATION" runat="server" MaxLength="10" Width="200px" onchange="rmvqoutes(this)"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBL_status" runat="server" Text="STATUS"></asp:Label>
                        </td>
                        <td>
                            <cc1:ComboBox ID="CMB_status" runat="server" DropDownStyle="DropDownList" AutoCompleteMode="None"
                                Width="200px">
                                <asp:ListItem Selected="true" Text="COMPLETED" Value="C" />
                                <asp:ListItem Text="OPEN" Value="O" />
                                <asp:ListItem Text="SUBMITTED" Value="S" />
                            </cc1:ComboBox>
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
                        <td>
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
                            <asp:Label ID="msgbox" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FKMCONNSTR %>"
                    SelectCommand="SELECT DISTINCT [JB_SRLNO] FROM [JOB_TKN_INFO]"></asp:SqlDataSource>
                <%--<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:FKMCONNSTR %>"
        SelectCommand="SELECT [JB_SRLNO], [JB_USERID],[JB_CODE], [JB_DESC],[JB_OPN_JOB_DT],[JB_CLS_JOB_DT],[JB_DURATION] FROM [JOB_TKN_INFO] WHERE (([JB_USERID] LIKE '%' + @JB_USERID + '%') OR ([JB_CODE] LIKE '%' + @JB_CODE + '%') OR ([JB_DESC] LIKE '%' + @JB_DESC + '%') OR ([JB_SRLNO] LIKE '%' + @JB_SRLNO + '%')   ) order by [JB_OPN_JOB_DT] , [JB_USERID] ">
        <SelectParameters>
            <asp:ControlParameter ControlID="TXT_SRCH" Name="JB_USERID" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="TXT_SRCH" Name="JB_DESC" PropertyName="Text"
                Type="String" />
            <asp:ControlParameter ControlID="TXT_SRCH" Name="JB_CODE" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="TXT_SRCH" Name="JB_SRLNO" PropertyName="Text"
                Type="String" /> 
        </SelectParameters>
    </asp:SqlDataSource>--%>
            </td>
            <td valign="top">
                <table>
                    <tr>
                        <td>
                            <asp:CheckBox ID="CHBX_EDIT" runat="server" Text="EDIT" AutoPostBack="true" />
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="USER"></asp:Label>
                        </td>
                        <td>
                            <cc1:ComboBox ID="cmb_Rpt_user" runat="server" DataTextField="USR_NAME" DataValueField="USR_USERID"
                                AutoPostBack="true" DropDownStyle="DropDownList" AutoCompleteMode="SuggestAppend"
                                Width="200px">
                            </cc1:ComboBox>
                        </td>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="REPORT STATUS"></asp:Label>
                        </td>
                        <td>
                            <cc1:ComboBox ID="cmb_Rpt_sts" runat="server" DropDownStyle="DropDownList" AutoPostBack="true"
                                AutoCompleteMode="None" Width="200px" OnSelectedIndexChanged="cmb_Rpt_sts_SelectedIndexChanged">
                                <asp:ListItem Selected="true" Text="ALL" Value="%" />
                                <asp:ListItem Text="COMPLETED" Value="C" />
                                <asp:ListItem Text="OPEN" Value="O" />
                                <asp:ListItem Text="SUBMITTED" Value="S" />
                            </cc1:ComboBox>
                        </td>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="DOWNLOAD REPORT" OnClick="Button1_Click" />
                        </td>
                        <td>
                            <asp:Button ID="BTN_mail" runat="server" Text="SEND NOTIFICATION" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" colspan="15">
                            <%--<asp:TextBox ID="TXT_SRCH" runat="server" Width="200px"></asp:TextBox>
                <asp:Button ID="BTNSRCH" runat="server" Text="SEARCH" />--%>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="JB_SRLNO"
                                RowStyle-Wrap="TRUE" HeaderStyle-BackColor="#0071ac" HeaderStyle-ForeColor="WhiteSmoke"
                                RowStyle-ForeColor="Black" RowStyle-Font-Size="Small" RowStyle-Height="25px"
                                RowStyle-Font-Bold="true">
                                <Columns>
                                    <asp:CommandField ShowSelectButton="true" SelectText="CHANGE" ItemStyle-ForeColor="RED" />
                                    <asp:BoundField DataField="JB_SRLNO" HeaderText="SRL" SortExpression="JB_SRLNO" ItemStyle-Width="80Px" />
                                    <asp:BoundField DataField="JB_DESC" HeaderText="DESCRIPTION" SortExpression="JB_DESC"
                                        ItemStyle-Width="600Px" />
                                    <asp:BoundField DataField="JB_USERID" HeaderText="USERID" SortExpression="JB_USERID"
                                        ItemStyle-Width="80Px" />
                                    <asp:BoundField DataField="JB_OPN_JOB_DT" HeaderText="START DATE" SortExpression="JB_OPN_JOB_DT"
                                        ItemStyle-Width="50Px" />
                                    <asp:BoundField DataField="JB_OPN_JOB_TM" HeaderText="START TIME" SortExpression="JB_OPN_JOB_TM"
                                        ItemStyle-Width="65Px" />
                                    <asp:BoundField DataField="JB_EST_CLS_DT" HeaderText="EST. CLOSE DATE" SortExpression="JB_EST_CLS_DT"
                                        ItemStyle-Width="50Px" />
                                    <asp:BoundField DataField="JB_EST_CLS_TM" HeaderText="EST. CLOSE TIME" SortExpression="JB_EST_CLS_TM"
                                        ItemStyle-Width="65Px" />
                                    <asp:BoundField DataField="JB_ESTDURATION" HeaderText="EST. DURATION" SortExpression="JB_ESTDURATION"
                                        ItemStyle-Width="50Px" />
                                    <asp:BoundField DataField="JBT_STATUS" HeaderText="STATUS" SortExpression="JBT_STATUS"
                                        ItemStyle-Width="50Px" />
                                    <asp:BoundField DataField="JB_CLS_JOB_DT" HeaderText="CLOSE DATE" SortExpression="JB_CLS_JOB_DT"
                                        ItemStyle-Width="50Px" />
                                    <asp:BoundField DataField="JB_CLS_JOB_TM" HeaderText="CLOSE TIME" SortExpression="JB_CLS_JOB_TM"
                                        ItemStyle-Width="65Px" />
                                    <asp:BoundField DataField="JB_DURATION" HeaderText="DURATION" SortExpression="JB_DURATION"
                                        ItemStyle-Width="50Px" />
                                    <asp:BoundField DataField="JB_REMX" HeaderText="REMARKS" SortExpression="JB_REMX"
                                        ItemStyle-Width="600Px" />
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
            </td>
        </tr>
    </table>
</asp:Content>
