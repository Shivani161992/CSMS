<%@ Page Language="C#" MasterPageFile="~/MasterPage/MD_MPSCSC.master" AutoEventWireup="true" CodeFile="mdReports_State.aspx.cs" Inherits="State_mdreports" Title="Untitled Page" %>
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
                    Text="Procurement Reports"></asp:Label></td>
        </tr>
       <%-- <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
                1</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="LinkButton34" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/frm_rpt_WheatProcured_transport.aspx">Wheat Procured and Transported Rabi 2013-14</asp:LinkButton></td>
        </tr>--%>
        <tr>
            <td align="left" 
                style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px; height: 20px;">
                1</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 20px;">
                <asp:LinkButton ID="LinkButton39" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/Rpt_PaddyMillingStatus.aspx">Paddy Milling Status Report</asp:LinkButton></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
                2</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="lnkprodetail" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/Procurement_Details.aspx">Procurement Details Between  two Dates</asp:LinkButton></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
                3</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="Linkmilleragree" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/rpt_miller_info.aspx">Miller Agreement Report</asp:LinkButton></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
                4</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="LinkButton38" runat="server" ForeColor="Navy" 
                    PostBackUrl="~/Reports_State/rpt_Pending_Acceptance_Note_Details.aspx">Summary of pending Acceptance Note Detail Division Wise</asp:LinkButton>
                    </td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
                5</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="LinkButton41" runat="server" ForeColor="Navy" 
                    PostBackUrl="~/Reports_State/Distwise_AllCommodity_Receipt_Accep.aspx">Summary of Receiving and Acceptance Note District Wise</asp:LinkButton>
                    </td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
                6</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="lnkdelaccept" runat="server" ForeColor="Navy" PostBackUrl="~/Reports_State/Rpt_DeleteAcceptance_Receipt.aspx">Deleted Acceptance and Receipt Report</asp:LinkButton>
                
                </td>
        </tr>
        
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
                7&nbsp;</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="lnkprodetail0" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/CategoryWise_WheatDeposit.aspx">Category Wise Wheat Deposit Report(2015)</asp:LinkButton></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
                8</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="lnkfci" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/FCIDeatil_Procurement.aspx">Wheat Surrender to FCI During Procurement</asp:LinkButton></td>
        </tr>
        
         <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
                9</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="lnkgdnwise" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/WheatDeposit_GodownWise.aspx">Wheat Deposited GodownWise from Procurement</asp:LinkButton></td>
                    
                    
        </tr>
         <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
               10</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="lnkindist" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/Rpt_WheatDispatch_OtherDist.aspx">Wheat Deposited Other District</asp:LinkButton></td>
        </tr>
        
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
               11</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="lnkpayment" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/SocietyPayment.aspx">Wheat Deposited and Payment Details</asp:LinkButton></td>
        </tr>
        
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
               12</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="lnksilo" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/Wheat_SiloDeposit.aspx">Wheat Deposited at SILO</asp:LinkButton></td>
        </tr>

<tr>
 <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
               13</td>
 <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="Lnkwrnwhr" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/WrongWHR.aspx">Wrong WHR Entered</asp:LinkButton></td>
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
       <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; height: 18px;
                background-color: #cfdcc8">
                3</td>
            <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcdc">
                <asp:LinkButton ID="LinkButton44" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/HO_Issuewise_FPSAllotment.aspx"> Issue Center Wise FPS Allotment</asp:LinkButton></td>
        </tr>
        
     
       
       <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; height: 18px;
                background-color: #cfdcc8">
                4</td>
            <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcdc">
                <asp:LinkButton ID="Linkdistribute" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/Rpt_DistributionMonitoring.aspx">Districtwise Distribution Monitoring Report</asp:LinkButton></td>
        </tr>
        
     
       
       <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; height: 18px;
                background-color: #cfdcc8">
                5</td>
            <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcdc">
                <asp:LinkButton ID="lnksector" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/SectorMaster_Report.aspx">Districtwise Sector Name Report</asp:LinkButton></td>
        </tr>
           <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; height: 18px;
                background-color: #cfdcc8">
                6</td>
            <td align="left" style="font-size: 10pt; position: static; height: 18px; background-color: #cfdcdc">
                <asp:LinkButton ID="lnklifting" runat="server" Font-Size="14px" ForeColor="Navy"
                     PostBackUrl="~/Reports_State/Rpt_Allotment_lifting_Report.aspx">Allotment,Lifting and Distribution Issuecenterwise Detail Report</asp:LinkButton></td>
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
          1</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="lnkdistwise_compliedrpt" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/Rpt_csms_distwise_compliedreport.aspx">District Wise Commodity Statement</asp:LinkButton></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; background-color: #cfdcc8; height: 18px;">
                2 <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 18px;">
            <asp:LinkButton ID="lnkICwise" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/IC_AllComodity_Summry.aspx">Issue Center Wise Commodity Statement</asp:LinkButton>
            
            </td>
        </tr>
        
         <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; background-color: #cfdcc8; height: 18px;">
                3</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 18px;">
            <asp:LinkButton ID="Linkwheatstatus" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/Rpt_WheatStatus.aspx"> Wheat Status Report</asp:LinkButton>
            
            </td>
        </tr>
        
         <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; background-color: #cfdcc8; height: 18px;">
                4</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 18px;">
            <asp:LinkButton ID="Linkwheatstatus1" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/State_NewCompiled.aspx"> Commodity Statement After New Opening (01/01/2015)</asp:LinkButton>
            
            </td>
        </tr>
        
         <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; background-color: #cfdcc8; height: 18px;">
                &nbsp;</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 18px;">
            <asp:LinkButton ID="Linkwheatstatus0" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/State_NewCompiled.aspx"> Total Summary(New Opening) from 01/01/2015</asp:LinkButton>
            
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
        
        <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; background-color: #cfdcc8">
            </td>
            <td align="center" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Medium"
                    Text="Operators Status Reports"></asp:Label></td>
        </tr>
                 <tr>
        <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
            1</td>
                <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="LinkButton30" runat="server" PostBackUrl="~/Reports_State/RegisteredOperator_rpt.aspx" Font-Size="14px" ForeColor="Navy">Registered Operator Details(Issue Centre)</asp:LinkButton></td>
                </tr>
        <tr>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 18px;">
                2</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="LinkButton31" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/RegisteredOperator_rpt_DM.aspx">Registered Operator Details(DMMPSCSC)</asp:LinkButton></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; background-color: #cfdcc8">
                3</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
            <asp:LinkButton ID="LinkButton36" runat="server" Font-Size="14px" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/OperatorStatus_Distwise.aspx">Distwise Operators Status</asp:LinkButton></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; background-color: #cfdcc8">
                4</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="LinkButton40" runat="server" Font-Size="Small" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/Rpt_OperatorsStatus_forAcceptanceNote.aspx">Operators Status (Acceptance Note Details) For Entry Report-District Wise</asp:LinkButton></td>
        </tr>
        <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; background-color: #cfdcc8">
                5</td>
            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                <asp:LinkButton ID="LinkButton45" runat="server" Font-Size="Small" ForeColor="Navy"
                    PostBackUrl="~/Reports_State/HO_DataEntryStatus.aspx">Data Entry Status Report-Issue Center Wise</asp:LinkButton></td>
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
                3</td>
            <td align="left" style="font-size: 10pt; position: static; height: 17px; background-color: #cfdcdc">
                <asp:LinkButton ID="Linkmarketing" runat="server" 
                    PostBackUrl="~/Reports_State/Rpt_Marfed_recievingDetail.aspx" Font-Size="14px" 
                    ForeColor="Navy">Receipt from Marketing federation </asp:LinkButton></td>
        </tr>
        
        <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; height: 17px;
                background-color: #cfdcc8">
                &nbsp;</td>
            <td align="left" style="font-size: 10pt; position: static; height: 17px; background-color: #cfdcdc; text-align: center;">
                <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Medium"
                    Text="Paddy Milling Reports"></asp:Label></td>
        </tr>
        
             <tr>
            <td align="left" style="font-size: 10pt; width: 18px; position: static; height: 17px;
                background-color: #cfdcc8">
                1.</td>
            <td align="left" style="font-size: 10pt; position: static; height: 17px; background-color: #cfdcdc; text-align: left;">
                    <asp:LinkButton ID="Linkbutton10" runat="server" ForeColor="Navy"
                        PostBackUrl="~/ReportForms_District/PaddyMillingAgreement_Rpt.aspx">Paddy Milling Status</asp:LinkButton>
                 </td>
        </tr>
        
        
        
        
        
     
       
    </table>
    </div>

</asp:Content>

