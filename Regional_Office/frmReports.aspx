<%@ Page Language="C#" MasterPageFile="~/MasterPage/Regional_Office.master" AutoEventWireup="true" CodeFile="frmReports.aspx.cs" Inherits="frmReports" Title="Report Form" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="border-color: black; border-width: 1pt; width:766px; background-color: lavender; " >
    <table style="border-top-width: 1pt; border-left-width: 1pt; border-left-color: black; border-bottom-width: 1pt; border-bottom-color: black; border-top-color: black; border-right-width: 1pt; border-right-color: black">
        <tr>
            <td align="center">
            </td>
            <td align="center">
                Reports</td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="left">
                1.</td>
            <td align="left">
                <asp:LinkButton ID="btn_do" runat="server" PostBackUrl="~/Division_Report/Release_Order_rpt.aspx">Release Order Register - District Wise </asp:LinkButton></td>
            <td>
            </td>
        </tr>
        
        <tr>
            <td align="left">
                2.</td>
            <td align="left">
                <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/Division_Report/Stock_Region_rpt.aspx">Stock Register - District Wise </asp:LinkButton></td>
            <td>
            </td>
        </tr>
         <tr>
            <td align="left">
                3.</td>
            <td align="left">
                <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="~/Division_Report/TO_Diviosonal.aspx">Transport Order - District Wise</asp:LinkButton></td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="left">
                4.</td>
            <td align="left">
                <asp:LinkButton ID="LinkButton3" runat="server" PostBackUrl="~/Division_Report/Current_Balance_rpt.aspx">Current Balance Of Stock - District Wise</asp:LinkButton></td>
            <td>
            </td>
        </tr>
         <tr>
            <td align="left">
                5.</td>
            <td align="left">
                <asp:LinkButton ID="LinkButton8" runat="server" PostBackUrl="~/Division_Report/Truck_Movement_Region.aspx">Truck Movement Detail</asp:LinkButton></td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="left">
               6.</td>
            <td align="left">
                <asp:LinkButton ID="LinkButton4" runat="server" PostBackUrl="~/Division_Report/liftingDetails_DO_divisionRpt.aspx">Lifted DO Details Report </asp:LinkButton></td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="left">
               7.</td>
            <td align="left">
                <asp:LinkButton ID="LinkButton6" runat="server" PostBackUrl="~/Division_Report/Details_DO_commodity.aspx">Delivery Order  Report Of District FPS/Commodity/Scheme- Wise</asp:LinkButton></td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="left">
                8.</td>
            <td align="left">
                <asp:LinkButton ID="LinkButton5" runat="server" PostBackUrl="~/Division_Report/liftingDetails_DO_rpt.aspx">Lifted DO Details Against Delivery Order  Report District- Wise</asp:LinkButton></td>
            <td>
            </td>
        </tr>
         <tr>
            <td align="left" style="width: 23px">
                9.</td>
            <td align="left" style="width: 419px">
                <asp:LinkButton ID="LinkButton10" runat="server" PostBackUrl="~/Division_Report/graphical_DO_divisionRpt.aspx">Graphical Representation of Delivery Order Divisional Report </asp:LinkButton></td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 23px">
                10.</td>
            <td align="left" style="width: 419px">
                <asp:LinkButton ID="LinkButton11" runat="server" PostBackUrl="~/Division_Report/graphical_DO_issueRpt.aspx">Graphical Representation of Issued DO Against Allotment Report </asp:LinkButton></td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 23px">
                11.</td>
            <td align="left" style="width: 419px">
                <asp:LinkButton ID="LinkButton12" runat="server" PostBackUrl="~/Division_Report/graphical_DO_liftRpt.aspx">Graphical Representation of Lifted DO Against Issued Report </asp:LinkButton></td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="left">
                12.</td>
            <td align="left">
                <asp:LinkButton ID="LinkButton7" runat="server" PostBackUrl="~/Division_Report/Statement_Transp_Region.aspx" >Statement Of Transportation</asp:LinkButton></td>
            <td>
            </td>
        </tr>
         <tr>
            <td align="left">
                13.</td>
            <td align="left">
                <asp:LinkButton ID="LinkButton9" runat="server" PostBackUrl="~/Division_Report/District_Alloc_Lift.aspx" >Total Allotmnet And Lifting</asp:LinkButton></td>
            <td>
            </td>
        </tr>
         <tr>
            <td align="left">
                14.</td>
            <td align="left">
                <asp:LinkButton ID="LinkButton13" runat="server" PostBackUrl="~/Division_Report/state_alloc_rpt.aspx">State Allocation Details</asp:LinkButton></td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="left">
                15.</td>
            <td align="left">
                <asp:LinkButton ID="LinkButton14" runat="server" PostBackUrl="~/Division_Report/RM_TotalSummary.aspx">Summary Report (District Wise)</asp:LinkButton></td>
          
          
            <td>
            </td>
        </tr>
        <tr>
            <td align="left">
                16</td>
            <td align="left">
            
             <asp:LinkButton ID="lnkacceptance" runat="server" PostBackUrl="~/Division_Report/Accep_division.aspx">Acceptance Note Details</asp:LinkButton>
            
            </td>
            <td>
            </td>
        </tr>
        
        <tr>
            <td align="left">
                17</td>
            <td align="left">
            
             <asp:LinkButton ID="lnkopstatus" runat="server" PostBackUrl="~/Division_Report/OperatorStatus.aspx">Operator Status</asp:LinkButton>
            
            </td>
            <td>
            </td>
        </tr>
        
        <tr>
            <td align="left">
                18</td>
           <td align="left">
            
             <asp:LinkButton ID="lnkpaddy" runat="server" 
                    PostBackUrl="~/Division_Report/Rpt_PaddyMillingStatus.aspx">Paddy Milling Staus Report</asp:LinkButton>
            
            </td>
            <td>
                &nbsp;</td>
        </tr>
        
        <tr>
            <td align="left">
                19</td>
            <td align="left">
            
             <asp:LinkButton ID="lnkpaddyOpenionig" runat="server" 
                    PostBackUrl="~/Division_Report/rpt_Opening_balance_Region.aspx">Opening balance Staus Report</asp:LinkButton>
            
            </td>
            <td>
                &nbsp;</td>
        </tr>
        
        
        <tr>
            <td align="left">
                20</td>
            <td align="left">
            
             <asp:LinkButton ID="LinkButton15" runat="server" 
                    PostBackUrl="~/Division_Report/Proc_Details.aspx">Procurement Detail</asp:LinkButton>
            
            </td>
            <td>
                &nbsp;</td>
        </tr>
        
        
        <tr>
            <td align="left">
                21</td>
            <td align="left">
            
             <asp:LinkButton ID="LinkButton16" runat="server" 
                    PostBackUrl="~/Division_Report/Rpt_District_DistributionAgainst_Lifting.aspx">	Allotement,Lifting and Distribution Status Report</asp:LinkButton>
            
            </td>
            <td>
                &nbsp;</td>
        </tr>
        
        
        <tr>
            <td align="left">
                22</td>
            <td align="left">
            
             <asp:LinkButton ID="LinkButton17" runat="server" 
                    PostBackUrl="~/Division_Report/Rpt_District_DistributionAgainst_Lifting.aspx">Monthly Distribution against Allotment</asp:LinkButton>
            
            </td>
            <td>
                &nbsp;</td>
        </tr>
        
        
        <tr>
            <td align="left">
                23</td>
            <td align="left">
            
        <%--     <asp:LinkButton ID="LinkButton18" runat="server" 
                    PostBackUrl="~/Division_Report/Rpt_SurrenderTO_FCI.aspx">Surrender to FCI</asp:LinkButton>--%>
            
            </td>
            <td>
                &nbsp;</td>
        </tr>
        
        
        <tr>
            <td align="left">
                24</td>
            <td align="left">
            
             <asp:LinkButton ID="LinkButton19" runat="server" 
                    PostBackUrl="~/Division_Report/Rpt_Distribution_Monitoring.aspx">Distribution Monitoring Report (Updated On18/11/2015)</asp:LinkButton>
            
            </td>
            <td>
                &nbsp;</td>
        </tr>
        
        
        <tr>
            <td align="left">
                25</td>
            <td align="left">
            
             <asp:LinkButton ID="LinkButton20" runat="server" 
                    PostBackUrl="~/Division_Report/MovmtOrderStatus_Rpt.aspx">PDS Movement Order (By Road)</asp:LinkButton>
            
            </td>
            <td>
                &nbsp;</td>
        </tr>
        
        
        <tr>
            <td align="left">
                26</td>
            <td align="left">
            
             <asp:LinkButton ID="LinkButton22" runat="server" 
                    PostBackUrl="~/Division_Report/MovmtOrderRackStatus_Rpt.aspx">PDS Movement Order (By Rack)</asp:LinkButton>
            
            </td>
            <td>
                &nbsp;</td>
        </tr>
        
        
        <tr>
            <td align="left">
                27</td>
            <td align="left">
            
             <asp:LinkButton ID="LinkButton23" runat="server" 
                    PostBackUrl="~/Reports_State/MillerRegistration_rpt.aspx">Miller Registration Report (CropYear Wise)</asp:LinkButton>
            
            </td>
            <td>
                &nbsp;</td>
        </tr>
        
        
        <tr>
            <td align="left">
                28</td>
            <td align="left">
            
             <asp:LinkButton ID="LinkButton24" runat="server" 
                    PostBackUrl="~/Reports_State/PMillingStatus_Division.aspx">Paddy Milling Status (CropYear & Division Wise)</asp:LinkButton>
            
            </td>
            <td>
                &nbsp;</td>
        </tr>
        
        
        <tr>
            <td align="left">
                29</td>
            <td align="left">
            
             <asp:LinkButton ID="LinkButton25" runat="server" 
                    PostBackUrl="~/Reports_State/PMillingStatus_District.aspx">Paddy Milling Status (District Wise)</asp:LinkButton>
            
            </td>
            <td>
                &nbsp;</td>
        </tr>
        
        
        <tr>
            <td align="left">
                30</td>
            <td align="left">
            
             <asp:LinkButton ID="LinkButton26" runat="server" 
                    PostBackUrl="~/Reports_State/PMilling_Progressive.aspx">Paddy Milling Progressive Report(%)</asp:LinkButton>
            
            </td>
            <td>
                &nbsp;</td>
        </tr>
        
         <tr>
            <td align="left">
                31</td>
            <td align="left">
            
             <asp:LinkButton ID="LinkButton21" runat="server" 
                    PostBackUrl="~/Division_Report/FCIBillRegister.aspx">FCI Bill Register</asp:LinkButton>
            
            &nbsp;27-05-2017</td>
            <td>
                &nbsp;</td>
        </tr>
        
         <tr>
            <td align="left">
                32</td>
            <td align="left">
            
             <asp:LinkButton ID="LinkButton28" runat="server" 
                    PostBackUrl="~/Division_Report/FCI_transPay.aspx">FCI को परिदान के विरुद्ध प्रस्तुत किये गए दावों का विवरण 2017-18 (प्राप्त स्वीकृति मात्रा के अनुसार)</asp:LinkButton>
            
             </td>
            <td>
                &nbsp;</td>
        </tr>
        
         <tr>
            <td align="left">
                33</td>
            <td align="left">
            
             <asp:LinkButton ID="LinkButton27" runat="server" 
                    PostBackUrl="~/Division_Report/FCI_Transp2017.aspx">FCI को परिदान की जानकारी 2017-18 (CSMS के अनुसार)</asp:LinkButton>
            
             </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    </div>
</asp:Content>

