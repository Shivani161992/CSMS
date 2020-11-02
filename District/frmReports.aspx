<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="frmReports.aspx.cs" Inherits="frmReports" Title="Report Form" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="border: 1pt solid blue; width: 606px; background-color: #cfdcdc;">
        <table style="border: 1pt solid navy; color: black; background-color: #cfdcc8; width: 573px;">
            <tr>
                <td align="center" style="font-weight: bold; font-size: 15pt; color: black; background-color: #cccccc;"></td>
                <td align="center" style="font-weight: bold; font-size: 15pt; color: black; background-color: #cccccc;">Reports</td>
                <td style="font-weight: bold; font-size: 15pt; color: black; background-color: #cccccc"></td>
            </tr>
            <tr>
                <td align="left">&nbsp;</td>
                <td align="left" style="text-align: center">
                    <b>Procurement Reports</b></td>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td align="left">1</td>
                <td align="left">
                    <asp:LinkButton ID="lnkaccepnote" runat="server" ForeColor="Navy"
                        PostBackUrl="~/ReportForms_District/AcceptNote_Dist.aspx">Acceptance Note Detail</asp:LinkButton>

                </td>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td align="left">2</td>
                <td align="left">

                    <%--<tr>
            <td align="left">
                24.</td>
            <td align="left">
                <asp:LinkButton ID="LinkButton18" runat="server" ForeColor="Navy" 
                    PostBackUrl="~/ReportForms_District/frm_rpt_CntrwisePrcTrns.aspx">Wheat Procured and Transported Rabi 2013-14</asp:LinkButton></td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="left">
                25.</td>
            <td align="left">
                <asp:LinkButton ID="LinkButton19" runat="server" ForeColor="Navy" 
                    PostBackUrl="~/ReportForms_District/frm_rpt_Procurment_Commodity_CntrWis.aspx">Paddy and Coarse grain Procured and Transported-2013-14</asp:LinkButton></td>
            <td>
            </td>
        </tr>--%><asp:LinkButton ID="LinkButton4" runat="server" ForeColor="Navy"
            PostBackUrl="~/ReportForms_District/rpt_storagewiseDetail.aspx">Storage wise Deposit Report</asp:LinkButton></td>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td align="left">3</td>
                <td align="left">
                    <asp:LinkButton ID="lnkdelaccept" runat="server" ForeColor="Navy" PostBackUrl="~/ReportForms_District/RptDist_DeleteAcceptance_Receipt.aspx">Deleted Acceptance and Receipt Report</asp:LinkButton></td>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td align="left">4</td>
                <td align="left">
                    <asp:LinkButton ID="lnktrucrec_drill0" runat="server" ForeColor="Navy"
                        PostBackUrl="~/ReportForms_District/Rpt_PaddyMillingStatus.aspx">Paddy Milling status Report</asp:LinkButton></td>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td align="left">5</td>
                <td align="left">
                    <asp:LinkButton ID="lnlrackproc" runat="server" ForeColor="Navy" PostBackUrl="~/ReportForms_District/Rpt_Rack_frmProcurement.aspx">Receive at Rack send from Procurement</asp:LinkButton></td>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td align="left">6</td>
                <td align="left">
                    <asp:LinkButton ID="lnktrucrec_drill" runat="server" ForeColor="Navy" PostBackUrl="~/ReportForms_District/TruckRecevingDetails_DistLevel.aspx">Truck Received details from Procurement</asp:LinkButton></td>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td align="left">7</td>
                <td align="left">
                    <asp:LinkButton ID="lnktrucrec_drill1" runat="server" ForeColor="Navy"
                        PostBackUrl="~/ReportForms_District/DetailProc.aspx">Procurement Detail between two dates</asp:LinkButton></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="left" colspan="2"
                    style="font-weight: 700; text-align: center; font-style: italic">F.C.I Claims Reports</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="left">1</td>
                <td align="left">
                    <asp:LinkButton ID="LinkButton10" runat="server" ForeColor="Navy"
                        PostBackUrl="~/ReportForms_District/rpt_AcceptanceNoteDetails_FCI.aspx">Summary Detail of Acceptance Note Details for FCI</asp:LinkButton></td>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td align="left">2</td>
                <td align="left">
                    <asp:LinkButton ID="LinkButton11" runat="server" ForeColor="Navy"
                        PostBackUrl="~/ReportForms_District/rpt_IncidentalChargesCostBill.aspx">Report for Incidental Charges Against Supply to FCI For Central Pool- Cost Bill</asp:LinkButton></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="left">3</td>
                <td align="left">
                    <asp:LinkButton ID="LinkButton12" runat="server" ForeColor="Navy"
                        PostBackUrl="~/ReportForms_District/rpt_IncidentalCharges_Mandi_N_TaxBills.aspx">Report for Incidental Charges Against Supply to FCI For Central Pool-Mandi & N Tax Bill</asp:LinkButton></td>
                <td>&nbsp;</td>
            </tr>


            <tr>
                <td align="left" colspan="2"
                    style="font-weight: 700; text-align: center; font-style: italic">Door Step 
                Delivery and Transport Reports</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="left">1</td>
                <td align="left">
                    <asp:LinkButton ID="lnkdoorstep" runat="server" ForeColor="Navy" PostBackUrl="~/ReportForms_District/DoorStep_DO.aspx">Door Step Delivery Order Report</asp:LinkButton></td>
                <td></td>
            </tr>

            <tr>
                <td align="left">2</td>
                <td align="left">
                    <asp:LinkButton ID="lnkTO" runat="server" ForeColor="Navy" PostBackUrl="~/ReportForms_District/DPY_TranspotOrder.aspx">Door Step Transport Order Report</asp:LinkButton></td>
                <td></td>
            </tr>



            <tr>
                <td align="left">3</td>
                <td align="left">
                    <asp:LinkButton ID="lnkroutechart" runat="server" ForeColor="Navy"
                        PostBackUrl="~/ReportForms_District/rpt_Districtreport_routechart.aspx">RouteChart status Report</asp:LinkButton></td>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td align="left">4</td>
                <td align="left">
                    <asp:LinkButton ID="lnkroutechart0" runat="server" ForeColor="Navy"
                        PostBackUrl="~/ReportForms_District/DO_LiftingDetails.aspx">Monthly DO and Lifting Details</asp:LinkButton></td>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td align="left">5</td>
                <td align="left">
                    <asp:LinkButton ID="lnkroutechart1" runat="server" ForeColor="Navy"
                        PostBackUrl="~/ReportForms_District/Rpt_Suspened_FPS.aspx">Suspened FPS Reports</asp:LinkButton></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="left">6&nbsp;</td>
                <td align="left">
                    <asp:LinkButton ID="lnklinktoFps" runat="server" ForeColor="Navy"
                        PostBackUrl="~/ReportForms_District/Link_to_FPS_Mapping.aspx">Link Society to FPS Mapping Report</asp:LinkButton></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="left">7</td>
                <td align="left">
                    <asp:LinkButton ID="lnksocietypayment" runat="server" ForeColor="Navy"
                        PostBackUrl="~/ReportForms_District/Rpt_linksocietywise_bill.aspx">Link societywise bill Report</asp:LinkButton></td>
                <td>&nbsp;</td>
            </tr>



            <tr>
                <td align="left">8</td>
                <td align="left">
                    <asp:LinkButton ID="lnklinktoFpsbill" runat="server" ForeColor="Navy"
                        PostBackUrl="~/ReportForms_District/Rpt_fpswise_bill.aspx">FPSwise bill Report</asp:LinkButton></td>
                <td>&nbsp;</td>
            </tr>



            <tr>
                <td align="left">9.</td>
                <td align="left">
                    <asp:LinkButton ID="lnktransportorder" runat="server" ForeColor="Navy"
                        PostBackUrl="~/ReportForms_District/frm_transportorderwise_detail.aspx">Transport order Report(Issuecenterwise)</asp:LinkButton></td>
                <td>&nbsp;</td>
            </tr>



            <tr>
                <td align="left">10</td>
                <td align="left">

                    <asp:LinkButton ID="lnklifting" runat="server"
                        PostBackUrl="~/ReportForms_District/Rpt_Allotment_lifting_Report.aspx">Allotement,Lifting and Distribution Status Report</asp:LinkButton>

                </td>
                <td>&nbsp;</td>
            </tr>



            <tr>
                <td align="left">11</td>
                <td align="left">

                    <asp:LinkButton ID="lnklinktoFpswise" runat="server" ForeColor="Navy"
                        PostBackUrl="~/ReportForms_District/Rpt_Summary_fpswise_bill.aspx">Summary FPSwise bill Detail</asp:LinkButton>

                </td>
                <td>&nbsp;</td>
            </tr>



            <tr>
                <td align="left" colspan="2"
                    style="text-align: center; font-weight: 700; font-style: italic">Delivery Order Reports</td>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td align="left" style="height: 21px">1.</td>
                <td align="left" style="height: 21px">
                    <asp:LinkButton ID="btn_multi_do" runat="server" PostBackUrl="~/ReportForms_District/No_of_DO_rpt.aspx" ForeColor="Navy">Multiple Delivery Order Report District-Wise</asp:LinkButton></td>
                <td style="height: 21px"></td>
            </tr>
            <tr>
                <td align="left" style="height: 20px">2.</td>
                <td align="left" style="height: 20px">
                    <asp:LinkButton ID="btn_multi_issue_do" runat="server" PostBackUrl="~/ReportForms_District/No_of_IssedDO_rpt.aspx" ForeColor="Navy">Multiple Issue Against Delivery Order Report District-Wise</asp:LinkButton></td>
                <td style="height: 20px"></td>
            </tr>
            <tr>
                <td align="left">3.</td>
                <td align="left">
                    <asp:LinkButton ID="multiDo_issueCentrewise" runat="server" PostBackUrl="~/ReportForms_District/multiple_DO_issueCentrewise_rpt.aspx" ForeColor="Navy">Multiple Delivery Order Report IssueCentre-Wise</asp:LinkButton></td>
                <td></td>
            </tr>
            <tr>
                <td align="left">4.</td>
                <td align="left">
                    <asp:LinkButton ID="multiple_issuedDo_issuewise" runat="server" PostBackUrl="~/ReportForms_District/multiple_IssuedDO_issueCentrewise_rpt.aspx" ForeColor="Navy">Multiple Issue Against Delivery Order Report IssueCentre-Wise</asp:LinkButton></td>
                <td></td>
            </tr>
            <tr>
                <td align="left">5.</td>
                <td align="left">
                    <asp:LinkButton ID="LinkButton5" runat="server" PostBackUrl="~/ReportForms_District/liftingDetails_DO_rpt.aspx" ForeColor="Navy">Lifted Do Details Against Delivery Order Report District-Wise</asp:LinkButton></td>
                <td></td>
            </tr>
            <tr>
                <td align="left">6.</td>
                <td align="left">
                    <asp:LinkButton ID="LinkButton7" runat="server" PostBackUrl="~/ReportForms_District/Details_DO_comm_rpt.aspx" ForeColor="Navy">Delivery Order Report FPS/Commodity/Scheme-Wise</asp:LinkButton></td>
                <td></td>
            </tr>
            <tr>
                <td align="left">7.</td>
                <td align="left">
                    <asp:LinkButton ID="LinkButton16" runat="server" PostBackUrl="~/ReportForms_District/Details_DO_comm_rpt_IC.aspx" ForeColor="Navy">Delivery Order Report FPS/Commodity/Scheme/IssueCenter-Wise</asp:LinkButton></td>
                <td></td>
            </tr>

            <%--  <tr>
            <td align="left">
                8.</td>
            <td align="left">
                <asp:LinkButton ID="n_2_alloc" runat="server" PostBackUrl="~/allocation_N-2_leadwise.aspx" ForeColor="Navy">N-2 Allocation of Lead Society Report</asp:LinkButton></td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="left">
                9.</td>
            <td align="left">
                <asp:LinkButton ID="form11" runat="server" PostBackUrl="~/District/form11_rpt.aspx" ForeColor="Navy">Form-11(A)</asp:LinkButton></td>
            <td>
            </td>
        </tr>--%>
            <tr>
                <td align="left">8.</td>
                <td align="left">
                    <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/ReportForms_District/TOrpt.aspx" ForeColor="Navy">Details of Transport Order</asp:LinkButton></td>
                <td></td>
            </tr>
            <tr>
                <td align="left">9.</td>
                <td align="left">
                    <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="~/ReportForms_District/Truck_Move_rpt.aspx" ForeColor="Navy">Truck Movement Report</asp:LinkButton></td>
                <td></td>
            </tr>
            <tr>
                <td align="left">10.</td>
                <td align="left">
                    <asp:LinkButton ID="LinkButton3" runat="server" PostBackUrl="~/ReportForms_District/Release_Order_rpt.aspx" ForeColor="Navy">Release Order Register</asp:LinkButton></td>
                <td></td>
            </tr>
            <%-- <tr>
            <td align="left">
                13.</td>
            <td align="left">
                <asp:LinkButton ID="LinkButton4" runat="server" PostBackUrl="~/ReportForms_District/Stock_Register_rpt.aspx" ForeColor="Navy">Stock Register</asp:LinkButton></td>
            <td>
            </td>
        </tr>--%>
            <tr>
                <td align="left">11.</td>
                <td align="left">
                    <asp:LinkButton ID="LinkButton6" runat="server" PostBackUrl="~/ReportForms_District/Current_Balance_rpt.aspx" ForeColor="Navy">Current Balance Of Stock</asp:LinkButton></td>
                <td></td>
            </tr>
            <tr>
                <td align="left">12.</td>
                <td align="left">
                    <asp:LinkButton ID="LinkButton8" runat="server" PostBackUrl="~/ReportForms_District/Statement_Transport.aspx" ForeColor="Navy">Statement Of Transportation </asp:LinkButton></td>
                <td></td>
            </tr>
            <tr>
                <td align="left">13.</td>
                <td align="left">
                    <asp:LinkButton ID="btn_do" runat="server" PostBackUrl="~/ReportForms_District/Rack_statement_dispatch.aspx" ForeColor="Navy">Sending Rack Statement</asp:LinkButton></td>
                <td></td>
            </tr>

            <tr>
                <td align="left" style="height: 21px">14.</td>
                <td align="left" style="height: 21px">
                    <asp:LinkButton ID="LinkButton9" runat="server" PostBackUrl="~/ReportForms_District/Rack_receipt_statement.aspx" ForeColor="Navy">Receiving Rack Statement</asp:LinkButton></td>
                <td style="height: 21px"></td>
            </tr>
            <%-- <tr>
            <td align="left">
                15.</td>
            <td align="left">
                <asp:LinkButton ID="LinkButton10" runat="server" PostBackUrl="~/ReportForms_District/Rack_Reconciliation_rpt.aspx" ForeColor="Navy">Rack Reconsiliation Report</asp:LinkButton></td>
            <td>
            </td>
        </tr>--%>
            <%--<tr>
            <td align="left">
                16.</td>
            <td align="left">
                <asp:LinkButton ID="LinkButton11" runat="server" PostBackUrl="~/ReportForms_District/scheme_transfer_rpt.aspx" ForeColor="Navy">Scheme Transfer Report</asp:LinkButton></td>
            <td>
            </td>
        </tr>--%>
            <%-- <tr>
            <td align="left">
                17.</td>
            <td align="left">
                <asp:LinkButton ID="LinkButton12" runat="server" PostBackUrl="~/ReportForms_District/Form13A.aspx" ForeColor="Navy">Daily Stock Transfer Information(Form-13(A))</asp:LinkButton></td>
            <td>
            </td>
        </tr>--%>
            <tr>
                <td align="left">15.</td>
                <td align="left">
                    <asp:LinkButton ID="LinkButton13" runat="server" PostBackUrl="~/ReportForms_District/Dispatch_Truck_Details.aspx" ForeColor="Navy">Truck Movement Details(Transfer to Other Godown)</asp:LinkButton></td>
                <td></td>
            </tr>
            <tr>
                <td align="left">16.</td>
                <td align="left">
                    <asp:LinkButton ID="LinkButton14" runat="server" PostBackUrl="~/ReportForms_District/DO_Issued_Against_RO.aspx" ForeColor="Navy">Delivery Order Report (From FCI)</asp:LinkButton></td>
                <td></td>
            </tr>
            <tr>
                <td align="left">17</td>
                <td align="left">
                    <asp:LinkButton ID="LinkButton15" runat="server" PostBackUrl="~/ReportForms_District/DO_Lifted_Against_RO.aspx" ForeColor="Navy"> Issue Against Delivery Order Report (From FCI)</asp:LinkButton></td>
                <td></td>
            </tr>
            <tr>
                <td align="left">18.</td>
                <td align="left">
                    <asp:LinkButton ID="LinkButton17" runat="server" ForeColor="Navy"
                        PostBackUrl="~/ReportForms_District/DailyEntryDistrictWise.aspx">Daily Entry Status DistrictWise</asp:LinkButton></td>
                <td></td>
            </tr>

            &nbsp;<tr>
                <td align="left">19.</td>
                <td align="left">
                    <asp:LinkButton ID="LinkButton20" runat="server" ForeColor="Navy"
                        PostBackUrl="~/District/Rpt_getDOdetails.aspx">Delivery Order details</asp:LinkButton></td>
                <td></td>
            </tr>

            <tr>
                <td align="left">20.</td>
                <td align="left">
                    <asp:LinkButton ID="LinkButton21" runat="server" ForeColor="Navy"
                        PostBackUrl="~/ReportForms_District/Distcsms_AllCommoditiestransaction.aspx">All Commoditywise Details</asp:LinkButton></td>
                <td></td>
            </tr>

            <tr>
                <td align="left">21.</td>
                <td align="left">
                    <asp:LinkButton ID="LinkButton22" runat="server" ForeColor="Navy"
                        PostBackUrl="~/ReportForms_District/OperatorsStatus_DistWise.aspx">Operators Status</asp:LinkButton></td>
                <td></td>
            </tr>

            <tr>
                <td align="left">22.</td>
                <td align="left">
                    <asp:LinkButton ID="lnkhnl" runat="server" ForeColor="Navy"
                        PostBackUrl="~/ReportForms_District/District_DispatchbyRack.aspx">Dispatch by Rack</asp:LinkButton>
                </td>
                <td></td>
            </tr>
            <tr>
                <td align="left">23.</td>
                <td align="left">
                    <asp:LinkButton ID="lnkrack_commowise" runat="server" ForeColor="Navy"
                        PostBackUrl="~/ReportForms_District/Dist_Rackdetails_Commoditywise.aspx">By Rack Dispatch detail Commoditywise</asp:LinkButton>
                </td>
                <td></td>
            </tr>

            <tr>
                <td align="left">24.</td>
                <td align="left">
                    <asp:LinkButton ID="lnkroaddispatch" runat="server" ForeColor="Navy"
                        PostBackUrl="~/ReportForms_District/Dist_Roaddispatch.aspx">By Road Dispatch detail Commoditywise</asp:LinkButton>
                </td>
                <td></td>
            </tr>

            <tr>
                <td align="left">25</td>
                <td align="left">
                    <asp:LinkButton ID="lnkopening" runat="server" ForeColor="Navy"
                        PostBackUrl="~/ReportForms_District/rpt_Opening_balance.aspx">Opening balance status Report</asp:LinkButton></td>
                <td>&nbsp;</td>
            </tr>


            <tr>
                <td align="left">&nbsp;</td>
                <td align="left" style="text-align: center">
                    <b>Paddy Milling Reports</b></td>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td align="left">1</td>
                <td align="left">
                    <asp:LinkButton ID="LinkButton18" runat="server" ForeColor="Navy"
                        PostBackUrl="~/ReportForms_District/PaddyMillingAgreement_Rpt.aspx">Paddy Milling Status</asp:LinkButton>

                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>
