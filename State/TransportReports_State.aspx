<%@ Page Language="C#" MasterPageFile="~/MasterPage/Transport_MPSCSC.master" AutoEventWireup="true" CodeFile="TransportReports_State.aspx.cs" Inherits="TransportReports_State" Title="Report Form" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width:500px; border-top-width: 1pt; border-left-width: 1pt; border-left-color: black; border-bottom-width: 1pt; border-bottom-color: black; border-top-color: black; background-color: lavender; border-right-width: 1pt; border-right-color: black;" >
    <table style="border-top-width: 1pt; border-left-width: 1pt; border-left-color: black; border-bottom-width: 1pt; border-bottom-color: black; border-top-color: black; border-right-width: 1pt; border-right-color: black">
        <tr>
            <td align="center" colspan="2" style="font-size: 10pt; position: static; background-color: #cfdcc8">
                <strong>Reports</strong></td>
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
                &nbsp;</td>
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
        
        <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; height: 18px;
                background-color: #cfdcc8">
            </td>
            <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcdc;
                text-align: center">
                <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Medium"
                    Text="Door Delivery Order Report" Width="226px"></asp:Label></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; height: 18px;
                background-color: #cfdcc8">
                1</td>
            <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcdc">
                <asp:LinkButton ID="lnkDO" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/DPY_DO_State.aspx">Door Step Delivery Order Report</asp:LinkButton></td>
        </tr>
        
     
       
        <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; height: 18px;
                background-color: #cfdcc8">
                2.</td>
            <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcdc">
                <asp:LinkButton ID="lnkrootchart" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/Rpt_Routechart.aspx"> Routechart Status report Districtwise</asp:LinkButton></td>
        </tr>
        
     
       
    </table>
    </div>
</asp:Content>

