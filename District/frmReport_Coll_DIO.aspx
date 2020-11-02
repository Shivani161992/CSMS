<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Collector_DIO.master" AutoEventWireup="true" CodeFile="frmReport_Coll_DIO.aspx.cs" Inherits="District_frmReport_Coll_DIO" %>

<%@ MasterType VirtualPath="~/MasterPage/Collector_DIO.master" %>
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
                    <asp:linkbutton id="lnkaccepnote" runat="server" forecolor="Navy"
                        postbackurl="~/ReportForms_District/AcceptNote_Dist.aspx">Acceptance Note Detail</asp:linkbutton>

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
        </tr>--%><asp:linkbutton id="LinkButton4" runat="server" forecolor="Navy"
            postbackurl="~/ReportForms_District/rpt_storagewiseDetail.aspx">Storage wise Deposit Report</asp:linkbutton></td>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td align="left">3</td>
                <td align="left">
                    <asp:linkbutton id="lnkdelaccept" runat="server" forecolor="Navy" postbackurl="~/ReportForms_District/RptDist_DeleteAcceptance_Receipt.aspx">Deleted Acceptance and Receipt Report</asp:linkbutton>
                </td>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td align="left">4</td>
                <td align="left">
                    <asp:linkbutton id="lnktrucrec_drill0" runat="server" forecolor="Navy"
                        postbackurl="~/ReportForms_District/Rpt_PaddyMillingStatus.aspx">Paddy Milling status Report</asp:linkbutton>
                </td>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td align="left">5</td>
                <td align="left">
                    <asp:linkbutton id="lnlrackproc" runat="server" forecolor="Navy" postbackurl="~/ReportForms_District/Rpt_Rack_frmProcurement.aspx">Receive at Rack send from Procurement</asp:linkbutton>
                </td>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td align="left">6</td>
                <td align="left">
                    <asp:linkbutton id="lnktrucrec_drill" runat="server" forecolor="Navy" postbackurl="~/ReportForms_District/TruckRecevingDetails_DistLevel.aspx">Truck Received details from Procurement</asp:linkbutton>
                </td>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td align="left">7</td>
                <td align="left">
                    <asp:linkbutton id="lnktrucrec_drill1" runat="server" forecolor="Navy"
                        postbackurl="~/ReportForms_District/DetailProc.aspx">Procurement Detail between two dates</asp:linkbutton>
                </td>
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
                    <asp:linkbutton id="lnkdoorstep" runat="server" forecolor="Navy" postbackurl="~/ReportForms_District/DoorStep_DO.aspx">Door Step Delivery Order Report</asp:linkbutton>
                </td>
                <td></td>
            </tr>

            <tr>
                <td align="left">2</td>
                <td align="left">
                    <asp:linkbutton id="lnkTO" runat="server" forecolor="Navy" postbackurl="~/ReportForms_District/DPY_TranspotOrder.aspx">Door Step Transport Order Report</asp:linkbutton>
                </td>
                <td></td>
            </tr>



            <tr>
                <td align="left">3</td>
                <td align="left">
                    <asp:linkbutton id="lnkroutechart" runat="server" forecolor="Navy"
                        postbackurl="~/ReportForms_District/rpt_Districtreport_routechart.aspx">RouteChart status Report</asp:linkbutton>
                </td>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td align="left">4</td>
                <td align="left">
                    <asp:linkbutton id="lnkroutechart0" runat="server" forecolor="Navy"
                        postbackurl="~/ReportForms_District/DO_LiftingDetails.aspx">Monthly DO and Lifting Details</asp:linkbutton>
                </td>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td align="left">5</td>
                <td align="left">
                    <asp:linkbutton id="lnkroutechart1" runat="server" forecolor="Navy"
                        postbackurl="~/ReportForms_District/Rpt_Suspened_FPS.aspx">Suspened FPS Reports</asp:linkbutton>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="left">6&nbsp;</td>
                <td align="left">
                    <asp:linkbutton id="lnklinktoFps" runat="server" forecolor="Navy"
                        postbackurl="~/ReportForms_District/Link_to_FPS_Mapping.aspx">Link Society to FPS Mapping Report</asp:linkbutton>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="left">7</td>
                <td align="left">
                    <asp:linkbutton id="lnksocietypayment" runat="server" forecolor="Navy"
                        postbackurl="~/ReportForms_District/Rpt_linksocietywise_bill.aspx">Link societywise bill Report</asp:linkbutton>
                </td>
                <td>&nbsp;</td>
            </tr>



            <tr>
                <td align="left">8</td>
                <td align="left">
                    <asp:linkbutton id="lnklinktoFpsbill" runat="server" forecolor="Navy"
                        postbackurl="~/ReportForms_District/Rpt_fpswise_bill.aspx">FPSwise bill Report</asp:linkbutton>
                </td>
                <td>&nbsp;</td>
            </tr>



            <tr>
                <td align="left">9.</td>
                <td align="left">
                    <asp:linkbutton id="lnktransportorder" runat="server" forecolor="Navy"
                        postbackurl="~/ReportForms_District/frm_transportorderwise_detail.aspx">Transport order Report(Issuecenterwise)</asp:linkbutton>
                </td>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td align="left">10</td>
                <td align="left">

                    <asp:linkbutton id="lnklifting" runat="server"
                        postbackurl="~/ReportForms_District/Rpt_Allotment_lifting_Report.aspx">Allotement,Lifting and Distribution Status Report</asp:linkbutton>

                </td>
                <td>&nbsp;</td>
            </tr>



            <tr>
                <td align="left">11</td>
                <td align="left">

                    <asp:linkbutton id="lnklinktoFpswise" runat="server" forecolor="Navy"
                        postbackurl="~/ReportForms_District/Rpt_Summary_fpswise_bill.aspx">Summary FPSwise bill Detail</asp:linkbutton>

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
                    <asp:linkbutton id="btn_multi_do" runat="server" postbackurl="~/ReportForms_District/No_of_DO_rpt.aspx" forecolor="Navy">Multiple Delivery Order Report District-Wise</asp:linkbutton>
                </td>
                <td style="height: 21px"></td>
            </tr>
            <tr>
                <td align="left" style="height: 20px">2.</td>
                <td align="left" style="height: 20px">
                    <asp:linkbutton id="btn_multi_issue_do" runat="server" postbackurl="~/ReportForms_District/No_of_IssedDO_rpt.aspx" forecolor="Navy">Multiple Issue Against Delivery Order Report District-Wise</asp:linkbutton>
                </td>
                <td style="height: 20px"></td>
            </tr>
            <tr>
                <td align="left">3.</td>
                <td align="left">
                    <asp:linkbutton id="multiDo_issueCentrewise" runat="server" postbackurl="~/ReportForms_District/multiple_DO_issueCentrewise_rpt.aspx" forecolor="Navy">Multiple Delivery Order Report IssueCentre-Wise</asp:linkbutton>
                </td>
                <td></td>
            </tr>
            <tr>
                <td align="left">4.</td>
                <td align="left">
                    <asp:linkbutton id="multiple_issuedDo_issuewise" runat="server" postbackurl="~/ReportForms_District/multiple_IssuedDO_issueCentrewise_rpt.aspx" forecolor="Navy">Multiple Issue Against Delivery Order Report IssueCentre-Wise</asp:linkbutton>
                </td>
                <td></td>
            </tr>
            <tr>
                <td align="left">5.</td>
                <td align="left">
                    <asp:linkbutton id="LinkButton5" runat="server" postbackurl="~/ReportForms_District/liftingDetails_DO_rpt.aspx" forecolor="Navy">Lifted Do Details Against Delivery Order Report District-Wise</asp:linkbutton>
                </td>
                <td></td>
            </tr>

            <tr>
                <td align="left">6.</td>
                <td align="left">
                    <asp:linkbutton id="btn_do" runat="server" postbackurl="~/ReportForms_District/Rack_statement_dispatch.aspx" forecolor="Navy">Sending Rack Statement</asp:linkbutton>
                </td>
                <td></td>
            </tr>

            <tr>
                <td align="left" style="height: 21px">7.</td>
                <td align="left" style="height: 21px">
                    <asp:linkbutton id="LinkButton9" runat="server" postbackurl="~/ReportForms_District/Rack_receipt_statement.aspx" forecolor="Navy">Receiving Rack Statement</asp:linkbutton>
                </td>
                <td style="height: 21px"></td>
            </tr>
            <tr>
                <td align="left">8.</td>
                <td align="left">
                    <asp:linkbutton id="LinkButton10" runat="server" postbackurl="~/ReportForms_District/Rack_Reconciliation_rpt.aspx" forecolor="Navy">Rack Reconsiliation Report</asp:linkbutton>
                </td>
                <td></td>
            </tr>
            <tr>
                <td align="left">9.</td>
                <td align="left">
                    <asp:linkbutton id="LinkButton13" runat="server" postbackurl="~/ReportForms_District/Dispatch_Truck_Details.aspx" forecolor="Navy">Truck Movement Details(Transfer to Other Godown)</asp:linkbutton>
                </td>
                <td></td>
            </tr>
            <tr>
                <td align="left">10.</td>
                <td align="left">
                    <asp:linkbutton id="LinkButton14" runat="server" postbackurl="~/ReportForms_District/DO_Issued_Against_RO.aspx" forecolor="Navy">Delivery Order Report (From FCI)</asp:linkbutton>
                </td>
                <td></td>
            </tr>
            <tr>
                <td align="left">11.</td>
                <td align="left">
                    <asp:linkbutton id="LinkButton15" runat="server" postbackurl="~/ReportForms_District/DO_Lifted_Against_RO.aspx" forecolor="Navy"> Issue Against Delivery Order Report (From FCI)</asp:linkbutton>
                </td>
                <td></td>
            </tr>
            <tr>
                <td align="left">12.</td>
                <td align="left">
                    <asp:linkbutton id="LinkButton17" runat="server" forecolor="Navy"
                        postbackurl="~/ReportForms_District/DailyEntryDistrictWise.aspx">Daily Entry Status DistrictWise</asp:linkbutton>
                </td>
                <td></td>
            </tr>

            &nbsp;<tr>
                <td align="left">13.</td>
                <td align="left">
                    <asp:linkbutton id="LinkButton20" runat="server" forecolor="Navy"
                        postbackurl="~/District/Rpt_getDOdetails.aspx">Delivery Order details</asp:linkbutton>
                </td>
                <td></td>
            </tr>

            <tr>
                <td align="left">14.</td>
                <td align="left">
                    <asp:linkbutton id="LinkButton21" runat="server" forecolor="Navy"
                        postbackurl="~/ReportForms_District/Distcsms_AllCommoditiestransaction.aspx">All Commoditywise Details</asp:linkbutton>
                </td>
                <td></td>
            </tr>

            <tr>
                <td align="left">15.</td>
                <td align="left">
                    <asp:linkbutton id="LinkButton22" runat="server" forecolor="Navy"
                        postbackurl="~/ReportForms_District/OperatorsStatus_DistWise.aspx">Operators Status</asp:linkbutton>
                </td>
                <td></td>
            </tr>

            <tr>
                <td align="left">16.</td>
                <td align="left">
                    <asp:linkbutton id="lnkhnl" runat="server" forecolor="Navy"
                        postbackurl="~/ReportForms_District/District_DispatchbyRack.aspx">Dispatch by Rack</asp:linkbutton>
                </td>
                <td></td>
            </tr>
            <tr>
                <td align="left">17.</td>
                <td align="left">
                    <asp:linkbutton id="lnkrack_commowise" runat="server" forecolor="Navy"
                        postbackurl="~/ReportForms_District/Dist_Rackdetails_Commoditywise.aspx">By Rack Dispatch detail Commoditywise</asp:linkbutton>
                </td>
                <td></td>
            </tr>

            <tr>
                <td align="left">18.</td>
                <td align="left">
                    <asp:linkbutton id="lnkroaddispatch" runat="server" forecolor="Navy"
                        postbackurl="~/ReportForms_District/Dist_Roaddispatch.aspx">By Road Dispatch detail Commoditywise</asp:linkbutton>
                </td>
                <td></td>
            </tr>

            <tr>
                <td align="left">19.&nbsp;</td>
                <td align="left">
                    <asp:linkbutton id="lnkopening" runat="server" forecolor="Navy"
                        postbackurl="~/ReportForms_District/rpt_Opening_balance.aspx">Opening balance status Report</asp:linkbutton>
                </td>
                <td>&nbsp;</td>
            </tr>

                <tr>
                <td align="left" colspan="2"
                    style="text-align: center; font-weight: 700; font-style: italic">Paddy Milling Reports</td>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td align="left">1.&nbsp;</td>
                <td align="left">
                    <asp:linkbutton id="Linkbutton35" runat="server" forecolor="Navy"
                        postbackurl="~/ReportForms_District/PaddyMillingAgreement_Rpt.aspx">Paddy Milling Status</asp:linkbutton>
                </td>
                <td>&nbsp;</td>
            </tr>


        </table>
    </div>



</asp:Content>

