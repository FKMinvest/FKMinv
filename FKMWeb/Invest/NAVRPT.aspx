<%@ Page Title="NAV REPORT" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="false"
    CodeFile="NAVRPT.aspx.cs" Inherits="SUMMRPT_NAVRPT" %>
    
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style4
        {
            width: 100%;
        }
        
        #panel, #flip
        {
            padding: 5px;
            text-align: center;
        }
    </style>
     <style type="text/css">
<style type="text/css">
.Initial
{
  display: block;
  padding: 4px 18px 4px 18px;
  float: left;
   
  color: Black;
  font-weight: bold;
}
.Initial:hover
{
  color: Red; 
}
.Clicked
{
  float: left;
  display: block; 
  padding: 4px 18px 4px 18px;
  color: Black;
  font-weight: bold;
  color: Blue;
} 
</style> 
</style> 


</asp:Content>
<asp:Content ID="HeaderContent1" runat="server" ContentPlaceHolderID="ContenHeader">
    <script>

    var keyValue 
        function PrintSlipClick(element, key) {
             keyValue = key;
             alert('Printing under process : ' + keyValue + ' ...');
        }

        $(document).ready(function () {
            $("#flip").click(function () {
                $("#panel").slideToggle("slow");
            });

            $("#export").click(function () {
                $("#dwnld").slideDown("slow");
            });

            $("#dwnld").click(function () {
                $("#dwnld").slideUp("slow");
            });
        });
    </script> 
    
   
<table width="100%">
        <tr>
            <td align="center" style="font-size:large;" >                
                   NAV REPORT
               <hr />
            </td>
        </tr>
    </table>
   
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   <%--div style="overflow-x: auto; overflow-y: auto; height: 580px;  width: 100%;">--%>
<table width="75%">
        <tr>
            
            <td>
                <asp:Label ID="LBL_NAV_QTR" runat="server" Text="NAV QUARTER"></asp:Label>
            </td>
            <td class="style1" >
                <%--<asp:TextBox   ID="TXT_FKM_CD" runat="server" Width="200px"></asp:TextBox>--%>
                <cc1:ComboBox    ID="CMB_NAV_QTR" runat="server" AutoPostBack="true"  
                    DataTextField="QUARTER" DataValueField="CD" DropDownStyle="DropDown"      
                    AutoCompleteMode="SuggestAppend">
                </cc1:ComboBox>
            </td>
            <td>
    
            </td>
            </tr>
            </table>

    <table width="100%" align="center">
      <tr>
        <td>
          <asp:Button Text="NAV REPORT" BorderStyle="None" ID="Tab1" CssClass="Initial" runat="server"
              OnClick="Tab1_Click" />
          <asp:Button Text="DISCONTINUED 1" BorderStyle="None" ID="Tab2" CssClass="Initial" runat="server"
              OnClick="Tab2_Click" />
          <asp:Button Text="DISCONTINUED 2" BorderStyle="None" ID="Tab3" CssClass="Initial" runat="server"
              OnClick="Tab3_Click" />
          <asp:MultiView ID="MainView" runat="server">
            <asp:View ID="View1" runat="server">
              <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                <tr>
                  <td>
                    <asp:Button ID="Button1" runat="server" Text="DOWNLOAD REPORT" ToolTip="Download the NAV Report"/>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="FKM_SRL"
            RowStyle-Wrap="false" HeaderStyle-BackColor="#0071ac" HeaderStyle-ForeColor="WhiteSmoke"
            RowStyle-ForeColor="Black"     HeaderStyle-  RowStyle-Font-Size="Small"
            RowStyle-Height="25px"  RowStyle-Font-Bold="true" >
            <Columns>
                <asp:BoundField DataField="FKM_SRL" HeaderText="SRL" SortExpression="FKM_SRL" ItemStyle-Width="70Px"
                    ReadOnly="True"   />
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
                <asp:BoundField DataField="FKM_ROI_R" HeaderText="RATE OF INTEREST" SortExpression="FKM_ROI" ItemStyle-HorizontalAlign="Right" />  
                <asp:BoundField DataField="FKM_ANLINCMCY_R" HeaderText="ANNUAL INTEREST INCOME ON CURRENT YIELD" SortExpression="FKM_ANLINCMCY"
                    ItemStyle-HorizontalAlign="Right" />              
                <asp:BoundField DataField="FKM_SCNINCP_R" HeaderText="ACTUAL RETURNS TILL [ ]" SortExpression="FKM_SCNINCP"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_ANLRLZD_R" HeaderText="ANNUALIZED REALIZED" SortExpression="FKM_ANLRLZD"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_ACTLINCMRU_R" HeaderText="ACTUAL ON INCOME ECEIVED UPTO [ ]" SortExpression="FKM_ACTLINCMRU"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_UNRLYLD_R" HeaderText="UNREALIZED YIELD" SortExpression="FKM_UNRLYLD"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_DVDND_R" HeaderText="DIVIDEND" SortExpression="FKM_DVDND"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_VALVTN_R" HeaderText="VALUATION" SortExpression="FKM_VALVTN"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_CAPGN_R" HeaderText="CAPITAL GAIN (LOSS)" SortExpression="FKM_CAPGN_R"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_UNRLDVD_R" HeaderText="UNREALIZED YIELD / DIVIDEND " SortExpression="FKM_UNRLDVD"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_UNRLDVDCAP_R" HeaderText="UNREALIZED YIELD / DIVIDEND / CAPITAL GAIN (LOSS) " SortExpression="FKM_UNRLDVDCAP"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_FAIRVAL_R" HeaderText="FAIR VALUE" SortExpression="FKM_FAIRVAL"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_NAV_R" HeaderText="NAV" SortExpression="FKM_NAV"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_REMX" HeaderText="REMARKS" SortExpression="FKM_REMX" />
                    <asp:BoundField DataField="FKM_REG" HeaderText="REGISTERED" SortExpression="FKM_REG" />
                
                    <asp:BoundField DataField="FKM_INCGEN" HeaderText="INC GEN" SortExpression="FKM_INCGEN" />
                <asp:BoundField DataField="FKM_INVGRP" HeaderText="CATEGORY" SortExpression="FKM_INVGRP" />
                <asp:BoundField DataField="FKM_CURR" HeaderText="CURRRENCY" SortExpression="FKM_CURR"
                    ItemStyle-Width="100Px" />
          
            </Columns>
        </asp:GridView>
                  </td>
                </tr>
              </table>
            </asp:View>
            <asp:View ID="View2" runat="server">
              <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                <tr>
                  <td>
                    <asp:Button ID="Button2" runat="server" Text="DOWNLOAD REPORT" ToolTip="Download the NAV Report 2"/>
                   <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="FKM_SRL"
            RowStyle-Wrap="false" HeaderStyle-BackColor="#0071ac" HeaderStyle-ForeColor="WhiteSmoke"
            RowStyle-ForeColor="Black"     HeaderStyle-  RowStyle-Font-Size="Small"
            RowStyle-Height="25px"  RowStyle-Font-Bold="true" >
            <Columns>
                     <asp:BoundField DataField="FKM_SRL" HeaderText="SRL" SortExpression="FKM_SRL" ItemStyle-Width="70Px"
                    ReadOnly="True"  />
                <asp:BoundField DataField="FKM_PROJNAME" HeaderText="PROJECT NAME" SortExpression="FKM_PROJNAME"
                    ItemStyle-Width="300Px" />
                <asp:BoundField DataField="FKM_INVAMT_R" HeaderText="INVESTMENT" SortExpression="FKM_INVAMT"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_ROI_R" HeaderText="ROI" SortExpression="FKM_ROI" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_MONYCAL_R" HeaderText="MONTHLY" SortExpression="FKM_MONYCAL"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_QRTYCAL_R" HeaderText="QUARTERLY" SortExpression="FKM_QRTYCAL"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_SMANYCAL_R" HeaderText="SEMI ANNUAL" SortExpression="FKM_SMANYCAL"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_ANLINCMCY_R" HeaderText="ANNUAL INTEREST INCOME ON CURRENT YIELD"
                    SortExpression="FKM_ANLINCMCY" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_ACTLINCMRU_R" HeaderText="ACTUAL ON INCOME ECEIVED UPTO"
                    SortExpression="FKM_ACTLINCMRU" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_SCNINCP_R" HeaderText="ACTUAL RETURNS TILL SEPT 2015"
                    SortExpression="FKM_SCNINCP" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_UNRLDVD_R" HeaderText="UNREALIZED YIELD / DIVIDEND "
                    SortExpression="FKM_UNRLDVD" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_CAPGN_R" HeaderText="CAPITAL GAIN (LOSS)" SortExpression="FKM_CAPGN_R"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_FAIRVAL_R" HeaderText="FAIR VALUE" SortExpression="FKM_FAIRVAL"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_NAV_R" HeaderText="NAV" SortExpression="FKM_NAV" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="FKM_REG" HeaderText="REGISTERED" SortExpression="FKM_REG" />
                <asp:BoundField DataField="FKM_INVGRP" HeaderText="CATEGORY" SortExpression="FKM_INVGRP" />
                <asp:BoundField DataField="FKM_INCGEN" HeaderText="INC GEN" SortExpression="FKM_INCGEN" />
                <asp:BoundField DataField="FKM_CURR" HeaderText="CURRRENCY" SortExpression="FKM_CURR"
                    ItemStyle-Width="100Px" />
                    
            </Columns>
        </asp:GridView>
                  </td>
                </tr>
              </table>
            </asp:View>
            <asp:View ID="View3" runat="server">
              <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                <tr>
                  <td>
                    <asp:Button ID="Button3" runat="server" Text="DOWNLOAD REPORT" ToolTip="Download the NAV Report 3"/>
                  <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataKeyNames="FKM_SRL"
            RowStyle-Wrap="false" HeaderStyle-BackColor="#0071ac" HeaderStyle-ForeColor="WhiteSmoke"
            RowStyle-ForeColor="Black"     HeaderStyle-  RowStyle-Font-Size="Small"
            RowStyle-Height="25px"  RowStyle-Font-Bold="true" >
            <Columns>
                   
                <asp:BoundField DataField="FKM_PROJNAME" HeaderText="PROJECT NAME" SortExpression="FKM_PROJNAME"
                    ItemStyle-Width="300Px" />
                <asp:BoundField DataField="FKM_INVAMT_R" HeaderText="INVESTMENT" SortExpression="FKM_INVAMT"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_ANLINCMCY_R" HeaderText="ANNUAL INTEREST INCOME ON CURRENT YIELD"
                    SortExpression="FKM_ANLINCMCY" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_ROI_R" HeaderText="ROI" SortExpression="FKM_ROI" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_MONYCAL_R" HeaderText="MONTHLY" SortExpression="FKM_MONYCAL"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_QRTYCAL_R" HeaderText="QUARTERLY" SortExpression="FKM_QRTYCAL"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_SMANYCAL_R" HeaderText="SEMI ANNUAL" SortExpression="FKM_SMANYCAL"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_FAIRVAL_R" HeaderText="FAIR VALUE" SortExpression="FKM_FAIRVAL"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FKM_NAV_R" HeaderText="NAV" SortExpression="FKM_NAV" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="FKM_REG" HeaderText="REGISTERED" SortExpression="FKM_REG" />
                <asp:BoundField DataField="FKM_INVGRP" HeaderText="CATEGORY" SortExpression="FKM_INVGRP" />
                <asp:BoundField DataField="FKM_INCGEN" HeaderText="INC GEN" SortExpression="FKM_INCGEN" />
                <asp:BoundField DataField="FKM_CURR" HeaderText="CURRRENCY" SortExpression="FKM_CURR"
                    ItemStyle-Width="100Px" />
                   
            </Columns>
        </asp:GridView>
                  </td>
                </tr>
              </table>
            </asp:View>
          </asp:MultiView>
        </td>
      </tr> 
    </table> 
        
   <%-- </div>--%>
</asp:Content>
