<%@ Page Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="PDSReports_State.aspx.cs" Inherits="frmReports_State" Title="Report Form" %>
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
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Medium"
                    Text="Delivery Order Reports"></asp:Label></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
                1.</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/Reports_State/liftingDetails_DO_STrpt.aspx" Font-Size="14px" ForeColor="Navy">Lifted DO Details Against Delivery Order State Report </asp:LinkButton></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
                2.</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="~/Reports_State/liftingDetails_DO_divisionRpt.aspx" Font-Size="14px" ForeColor="Navy">Lifted DO Details Against Delivery Order Report Division-Wise</asp:LinkButton></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 18px; width: 18px;">
                3.</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 18px;">
                <asp:LinkButton ID="liftedDO_Details" runat="server" PostBackUrl="~/Reports_State/liftingDetails_DO_rpt.aspx" Font-Size="14px" ForeColor="Navy">Lifted DO Details Against Delivery Order Report District-Wise</asp:LinkButton></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
                4.</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="doDetails_comm" runat="server" PostBackUrl="~/Reports_State/Details_DO_commodity.aspx" Font-Size="14px" ForeColor="Navy">Delivery Order Report of District FPS/Commodity/Scheme-Wise</asp:LinkButton></td>
        </tr>
         <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
                5.</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                <asp:LinkButton ID="LinkButton11" runat="server" PostBackUrl="~/Reports_State/graphical_Do_STrpt.aspx" Font-Size="14px" ForeColor="Navy">Graphical Representation of Delivery Order State Report </asp:LinkButton></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
               6.</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                <asp:LinkButton ID="LinkButton12" runat="server" PostBackUrl="~/Reports_State/graphical_Do_Issuedrpt.aspx" Font-Size="14px" ForeColor="Navy">Graphical Representation of Issued DO Against Allotment Report </asp:LinkButton></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
                7.</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                <asp:LinkButton ID="LinkButton13" runat="server" PostBackUrl="~/Reports_State/graphical_Do_liftedrpt.aspx" Font-Size="14px" ForeColor="Navy">Graphical Representation of Lifted DO Against Issued Report </asp:LinkButton></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
                8.</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                <asp:LinkButton ID="LinkButton14" runat="server" PostBackUrl="~/Reports_State/graphical_Do_Divrpt.aspx" Font-Size="14px" ForeColor="Navy">Graphical Representation of Delivery Order  Report Division-Wise </asp:LinkButton></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
               9.</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                <asp:LinkButton ID="LinkButton15" runat="server" PostBackUrl="~/Reports_State/graphical_Do_DivIssuerpt.aspx" Width="521px" Font-Size="14px" ForeColor="Navy">Graphical Representation of Issued DO Against Allotment Report  Division-Wise</asp:LinkButton></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
               10.</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                <asp:LinkButton ID="LinkButton16" runat="server" PostBackUrl="~/Reports_State/graphical_Do_DivLiftrpt.aspx" Font-Size="14px" ForeColor="Navy">Graphical Representation of Lifted DO Against Issued Report  Division-Wise</asp:LinkButton></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; background-color: #cfdcc8">
            </td>
            <td align="center" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Medium"
                    Text="Transport  Reports"></asp:Label></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
                1</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="LinkButton4" runat="server" PostBackUrl="~/Reports_State/TO_rpt_HO.aspx" Font-Size="14px" ForeColor="Navy">Details of Transport Order </asp:LinkButton></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; background-color: #cfdcc8">
                2</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="LinkButton18" runat="server" PostBackUrl="~/Reports_State/Transportation_Statement.aspx" Font-Size="14px" ForeColor="Navy">Statement Of Transportation </asp:LinkButton></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; background-color: #cfdcc8">
                3</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="LinkButton7" runat="server" PostBackUrl="~/Reports_State/Truck_Movement_HO.aspx" Font-Size="14px" ForeColor="Navy">Truck Movement Report  </asp:LinkButton></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; background-color: #cfdcc8">
                4</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="lnktruckrec" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/RecevingType_Distwise.aspx">Truck Receiving Distwise Wise  </asp:LinkButton></td>
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
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
                </td>
            <td align="center" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Medium"
                    Text="State Allocation Reports"></asp:Label></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
                1</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="LinkButton23" runat="server" PostBackUrl="~/Reports_State/StateAllocation_rpt_Ho.aspx" Font-Size="14px" ForeColor="Navy">State Allocation Details</asp:LinkButton></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
                2</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="LinkButton19" runat="server" PostBackUrl="~/Reports_State/State_alloc_rpt.aspx" Font-Size="14px" ForeColor="Navy">State Allocation Details (Graphical Representation)</asp:LinkButton></td>
        </tr>
         <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
                3</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="LinkButton24" runat="server" PostBackUrl="~/Reports_State/StAllot_lift_mpsc.aspx" Font-Size="14px" ForeColor="Navy">Allocation Details Block-Wise (Current & Next Month)</asp:LinkButton></td>
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
                1</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="LinkButton33" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/AllDistrictCommoditiesbetweenDates.aspx">Summary Report(Commodities between two dates)</asp:LinkButton></td>
        </tr>
      
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
                2</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="LinkButton25" runat="server" PostBackUrl="~/Reports_State/State_DO_details.aspx" Font-Size="14px" ForeColor="Navy">No. of Issued DO Details (State Summary Report) IssueCentre-Wise</asp:LinkButton></td>
        </tr>
        
        
        <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; background-color: #cfdcc8">
            </td>
            <td align="center" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Medium"
                    Text="Receipt Reports"></asp:Label></td>
        </tr>
             <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; height: 17px;
                background-color: #cfdcc8">
                1</td>
            <td align="left" style="font-size: 10pt; position: static; height: 17px; background-color: #cfdcdc">
                <asp:LinkButton ID="Linkmarketing" runat="server" 
                    PostBackUrl="~/Reports_State/Rpt_Marfed_recievingDetail.aspx" Font-Size="14px" 
                    ForeColor="Navy">Receipt from Marketing federation </asp:LinkButton></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
                </td>
            <td align="center" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Medium"
                    Text="Scheme Transfer Reports"></asp:Label></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
                1&nbsp;</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="LinkButton27" runat="server" PostBackUrl="~/Reports_State/SchemeTransfer_rpt_Ho.aspx" Font-Size="14px" ForeColor="Navy">Scheme Transfer Report</asp:LinkButton></td>
                
                
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
                <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Medium"
                    Text="Sugar / Salt Report"></asp:Label></td>
        </tr>
       
        <tr>
            <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcc8; width: 18px;">
                1</td>
            <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcdc">
                <asp:LinkButton ID="lnkbtnsupplyorder0" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/Rpt_SugarStatus.aspx">Sugar Status report </asp:LinkButton></td>
        </tr>
        
       
        <tr>
            <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcc8; width: 18px;">
                2</td>
            <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcdc">
                <asp:LinkButton ID="lnkbtnsupplyordersalt" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/Rpt_SaltStatus.aspx">Salt Status report </asp:LinkButton></td>
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

