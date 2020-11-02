<%@ Page Language="C#" MasterPageFile="~/MasterPage/Procurement_MPSCSC.master" AutoEventWireup="true" CodeFile="ProcurementReports_State.aspx.cs" Inherits="ProcurementReports_State" Title="Report Form" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width:500px; border-top-width: 1pt; border-left-width: 1pt; border-left-color: black; border-bottom-width: 1pt; border-bottom-color: black; border-top-color: black; background-color: lavender; border-right-width: 1pt; border-right-color: black;" >
    <table style="border-top-width: 1pt; border-left-width: 1pt; border-left-color: black; border-bottom-width: 1pt; border-bottom-color: black; border-top-color: black; border-right-width: 1pt; border-right-color: black">
        <tr>
            <td align="center" colspan="2" style="font-size: 10pt; position: static; background-color: #cfdcc8">
                <strong>Reports</strong></td>
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
            <td align="left" style="font-size: 10pt; width: 18px; position: static; background-color: #cfdcc8; height: 17px;">
                5</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 17px;">
                <asp:LinkButton ID="LinkButton38" runat="server" ForeColor="Navy" 
                    PostBackUrl="~/Reports_State/rpt_Pending_Acceptance_Note_Details.aspx">Summary of pending Acceptance Note Detail Division Wise</asp:LinkButton>
                    </td>
        </tr>
        
        <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; background-color: #cfdcc8; height: 17px;">
                6</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 17px;">
                <asp:LinkButton ID="LinkButton41" runat="server" ForeColor="Navy" 
                    PostBackUrl="~/Reports_State/Distwise_AllCommodity_Receipt_Accep.aspx">Summary of Receiving and Acceptance Note District Wise</asp:LinkButton>
                    </td>
        </tr>
        
        
        <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; background-color: #cfdcc8">
            </td>
            <td align="center" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Medium"
                    Text="Receipt Reports"></asp:Label></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; background-color: #cfdcc8">
                1</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="LinkButton26" runat="server" PostBackUrl="~/Reports_State/Summury_Procurement.aspx" Font-Size="14px" ForeColor="Navy">Total Receipt Details(Procurement)</asp:LinkButton></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
                2</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="LinkButton20" runat="server" PostBackUrl="~/Reports_State/DailyReceipt_statement.aspx" Font-Size="14px" ForeColor="Navy">Daily Receipt Statement </asp:LinkButton></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; height: 17px;
                background-color: #cfdcc8">
                4</td>
            <td align="left" style="font-size: 10pt; position: static; height: 17px; background-color: #cfdcdc">
                <asp:LinkButton ID="lnkdelaccept" runat="server" ForeColor="Navy" PostBackUrl="~/Reports_State/Rpt_DeleteAcceptance_Receipt.aspx">Deleted Acceptance and Receipt Report</asp:LinkButton></td>
        </tr>
             <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; height: 17px;
                background-color: #cfdcc8">
                5</td>
            <td align="left" style="font-size: 10pt; position: static; height: 17px; background-color: #cfdcdc">
                <asp:LinkButton ID="Linkmarketing" runat="server" 
                    PostBackUrl="~/Reports_State/Rpt_Marfed_recievingDetail.aspx" Font-Size="14px" 
                    ForeColor="Navy">Receipt from Marketing federation </asp:LinkButton></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; background-color: #cfdcc8">
                7</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="LinkButton40" runat="server" Font-Size="Small" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/Rpt_OperatorsStatus_forAcceptanceNote.aspx">Operators Status (Acceptance Note Details) For Entry Report-District Wise</asp:LinkButton></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; background-color: #cfdcc8">
            </td>
            <td align="center" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Medium"
                    Text="Procurement and Transported Reports"></asp:Label></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
                1</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="LinkButton34" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/frm_rpt_WheatProcured_transport.aspx">Wheat Procured and Transported Rabi 2013-14</asp:LinkButton></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
                2</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                &nbsp;<asp:LinkButton ID="LinkButton35" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/Rpt_PaddyMillingStatus.aspx">Paddy and Coarse grain Procured and Transported-2013-14</asp:LinkButton></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
                3&nbsp;</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                &nbsp;<asp:LinkButton ID="LinkButton39" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/Rpt_PaddyMillingStatus.aspx">Paddy Milling Status Report</asp:LinkButton></td>
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
            <td align="left" style="font-size: 10pt; width: 18px; position: static; background-color: #cfdcc8; height: 18px;">
                4.</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 18px;">
            <asp:LinkButton ID="Linkwheatstatus" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/Rpt_WheatStatus.aspx"> Wheat Status Report</asp:LinkButton>
            
            </td>
        </tr>
        
        <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; background-color: #cfdcc8">
            </td>
            <td align="center" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Medium"
                    Text="Dispatch Details Reports"></asp:Label></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; height: 18px;
                background-color: #cfdcc8">
                1</td>
            <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcdc">
                <asp:LinkButton ID="lnkfrmproc" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/Recdatrack_frmProcurement.aspx">Truck Received at Rack Point send from Procurement</asp:LinkButton></td>
        </tr>
    <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 18px; width: 18px;">
                2</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 18px;">
                <asp:LinkButton ID="LnkRackdetail" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/State_RailRackDetails.aspx">Rack Details</asp:LinkButton></td>
        </tr>
        
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 18px; width: 18px;">
                3</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 18px;">
                <asp:LinkButton ID="lnkrackcommowise" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/Rackreport_Commoditywise.aspx">By Rack Dispatch Details Commodity Wise</asp:LinkButton></td>
        </tr>
        
         <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 18px; width: 18px;">
                4</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 18px;">
                <asp:LinkButton ID="lnkroaddispatch" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/RoadDispatch.aspx">ByRoad Dispatch Details Commodity Wise</asp:LinkButton></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; height: 18px;
                background-color: #cfdcc8">
            </td>
            <td align="center" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcdc">
                &nbsp;</td>
        </tr>
                
     
       
    </table>
    </div>
</asp:Content>

