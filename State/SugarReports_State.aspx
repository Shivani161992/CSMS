<%@ Page Language="C#" MasterPageFile="~/MasterPage/Sugar_MPSCSC.master" AutoEventWireup="true" CodeFile="SugarReports_State.aspx.cs" Inherits="SugarReports_State" Title="Report Form" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width:500px; border-top-width: 1pt; border-left-width: 1pt; border-left-color: black; border-bottom-width: 1pt; border-bottom-color: black; border-top-color: black; background-color: lavender; border-right-width: 1pt; border-right-color: black;" >
    <table style="border-top-width: 1pt; border-left-width: 1pt; border-left-color: black; border-bottom-width: 1pt; border-bottom-color: black; border-top-color: black; border-right-width: 1pt; border-right-color: black">
        <tr>
            <td align="center" colspan="2" style="font-size: 10pt; position: static; background-color: #cfdcc8">
                <strong>Reports</strong></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; height: 17px;
                background-color: #cfdcc8">
            </td>
            <td align="center" style="font-size: 10pt; position: static; height: 17px; background-color: #cfdcdc">
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Medium"
                    Text="Stock Reports"></asp:Label></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; height: 17px; background-color: #cfdcc8; width: 18px;">
                1</td>
            <td align="left" style="font-size: 10pt; position: static; height: 17px; background-color: #cfdcdc">
                <asp:LinkButton ID="LinkButton5" runat="server" PostBackUrl="~/Reports_State/Stock_rpt_HO.aspx" Font-Size="14px" ForeColor="Navy">Stock Register </asp:LinkButton></td>
        </tr>
         <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
                2</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="LinkButton10" runat="server" PostBackUrl="~/Reports_State/Stock_Register_Graph.aspx" Font-Size="14px" ForeColor="Navy">Stock Register(Graphical Representation) </asp:LinkButton></td>
        </tr>
         <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
                3</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="LinkButton6" runat="server" PostBackUrl="~/Reports_State/Current_Balance_rpt.aspx" Font-Size="14px" ForeColor="Navy">Current Balance Of Stock  </asp:LinkButton></td>
        </tr>
          <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
                4</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="LinkButton9" runat="server" PostBackUrl="~/Reports_State/Current_Balance_Graph.aspx" Font-Size="14px" ForeColor="Navy">Current Balance Of Stock (Graphical Representation)</asp:LinkButton></td>
        </tr>
          <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
                5.</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="LinkButton43" runat="server" 
                    PostBackUrl="~/Reports_State/rpt_Opening_balance.aspx" Font-Size="14px" 
                    ForeColor="Navy">Crop yearwise Opening Balance Report</asp:LinkButton></td>
        </tr>
          <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
                6</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="Linkdamage" runat="server" 
                    PostBackUrl="~/Reports_State/frm_damage_sweepage.aspx" Font-Size="14px" 
                    ForeColor="Navy">Damage and sweepage cropyear wise Report</asp:LinkButton></td>
        </tr>
     
        <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; height: 18px;
                background-color: #cfdcc8">
            </td>
            <td align="center" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcdc">
                <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Medium"
                    Text="Summary Reports"></asp:Label></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
                3</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="LinkButton33" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/AllDistrictCommoditiesbetweenDates.aspx">Summary Report(Commodities between two dates)</asp:LinkButton></td>
        </tr>
      
        
        <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; background-color: #cfdcc8">
            </td>
            <td align="center" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Medium"
                    Text="Receipt Reports"></asp:Label></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
                2</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="LinkButton20" runat="server" PostBackUrl="~/Reports_State/DailyReceipt_statement.aspx" Font-Size="14px" ForeColor="Navy">Daily Receipt Statement </asp:LinkButton></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px; height: 17px;">
                3</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 17px;">
                <asp:LinkButton ID="LinkButton37" runat="server" ForeColor="Navy" 
                    PostBackUrl="~/Reports_State/Rpt_TenderPurchase_Supplyorder.aspx">Total Receipt Details for Tender Purchase by Road(Sugar/Salt)-District Wise</asp:LinkButton></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
                &nbsp;</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
                </td>
            <td align="center" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                &nbsp;<asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Italic="True"
                    Font-Size="Medium" Text="Commodity Statement Report"></asp:Label></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
                1</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                &nbsp;<asp:LinkButton ID="lnkdistwise_compliedrpt" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/Rpt_csms_distwise_compliedreport.aspx">District Wise Commodity Statement</asp:LinkButton></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; background-color: #cfdcc8; height: 18px;">
                2</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 18px;">
            <asp:LinkButton ID="lnkICwise" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/IC_AllComodity_Summry.aspx">Issue Center Wise Commodity Statement</asp:LinkButton>
            
            </td>
        </tr>
        
         <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; background-color: #cfdcc8; height: 18px;">
                3</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 18px;">
            <asp:LinkButton ID="LinkButton42" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/Ho_CropYear_IC.aspx">Issue Center Wise CropYear Statement</asp:LinkButton>
            
            </td>
        </tr>
        
        <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; height: 18px;
                background-color: #cfdcc8">
            </td>
            <td align="center" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcdc">
                <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Medium"
                    Text="Sugar/Salt Statement  Reports"></asp:Label></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcc8; width: 18px;">
                1</td>
            <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcdc">
                <asp:LinkButton ID="lnkbtnsupplyorder" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/SupplyOrder_rpt.aspx">Statement of Sugar Supply Order Details Zone Wise</asp:LinkButton></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcc8">
                2.</td>
            <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcdc">
                <asp:LinkButton ID="Linktender" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/Rpt_TenderPurchase_Supplyorder.aspx">Total Receipt Details for Tender Purchase by Road(Sugar)-District Wise</asp:LinkButton></td>
        </tr>
       
        <tr>
            <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcc8; width: 18px;">
                3</td>
            <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcdc">
                <asp:LinkButton ID="lnkbtnsupplyorder0" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/Rpt_SugarStatus.aspx">Sugar Status report </asp:LinkButton></td>
        </tr>
        
       
        <tr>
            <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcc8; width: 18px;">
                4.</td>
            <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcdc">
                <asp:LinkButton ID="lnkbtnsupplyordersalt" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/Rpt_SaltStatus.aspx">Salt Status report </asp:LinkButton></td>
        </tr>
        
             
       
    </table>
    </div>
</asp:Content>

