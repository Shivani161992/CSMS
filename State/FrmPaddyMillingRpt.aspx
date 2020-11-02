<%@ Page Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true"
    CodeFile="FrmPaddyMillingRpt.aspx.cs" Inherits="State_FrmPaddyMillingRpt" Title="Milling Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center>

   <div>
        <asp:Panel ID="Panel3" runat="server" Height="800px" ScrollBars="Both">
            <table style="border: 1pt solid navy; color: black; background-color:white; font-family: Calibri;"
                border="1" cellpadding="0" cellspacing="0" width="620px">
                <tr>
                    <td align="center" style="font-weight: bold; font-size: 15pt; color: black; background-color: #cccccc;"
                        colspan="2">
                        Paddy Milling Reports
                    </td>
                </tr>
                <%--     <tr>
                    <td align="left" style="font-size: 10pt; position: static;">1</td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton39" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/Rpt_PaddyMillingStatus.aspx">Paddy Milling Status Report</asp:LinkButton></td>
                    <td align="left" style="font-weight: bold;">

                        <asp:LinkButton ID="linkbtn1" runat="server" PostBackUrl="~/State/NewFrmReport_State.aspx">Back</asp:LinkButton>
                    </td>

                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">2</td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="Linkmilleragree" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/rpt_miller_info.aspx">Miller Agreement Report</asp:LinkButton></td>
                </tr>--%>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        1
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton1" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/MillerRegistration_rpt.aspx">Miller Registration Report</asp:LinkButton>(CropYear
                        Wise)
                    </td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        2
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton2" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/PMillingStatus_Division.aspx">Paddy Milling Status</asp:LinkButton>(Division
                        Wise)
                    </td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        3
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton3" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/PMillingStatus_District.aspx">Paddy Milling Status</asp:LinkButton>(District
                        Wise)
                    </td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        4
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton4" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/PMilling_Progressive.aspx">Paddy Milling Progressive Report(%)</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        5
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton5" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/PM_Evaluation.aspx">Miller Evaluation Report</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        6
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton6" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/CMR_Deposit_Distance.aspx">CMR Deposited In Godown</asp:LinkButton>(Distance
                        Wise)
                    </td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        7
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton8" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/CMR_Deposit_Dis_AllMiller.aspx">CMR Deposited In Godown</asp:LinkButton>(Miller
                        Wise)
                    </td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        8
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton9" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/CMR_Deposit_Dis_AllGodown.aspx">CMR Deposited In Godown</asp:LinkButton>(Godown
                        Wise)
                    </td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        9
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton7" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/PMilling_SelfOtherDist.aspx">Paddy Milling & CMR Receiving Status</asp:LinkButton>(Self
                        + Other Dist)
                    </td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        10
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton10" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/PM_AllAgreement.aspx">Miller All Agreement Status</asp:LinkButton>(District
                        Wise)
                    </td>
                </tr>
                <%--   <tr>
                    <td align="left" style="font-size: 10pt; position: static;">7</td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton7" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/Paddy_Issued_Distance.aspx">Paddy Issued From Godown</asp:LinkButton>(Distance Wise)</td>
                </tr>--%>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        11
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton11" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/CMR_Deposit_District.aspx">CMR Deposited In Godown</asp:LinkButton>(District
                        Wise)
                    </td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        12
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton15" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/TotalCMR_TotalPaddyIssued.aspx">Total CMR</asp:LinkButton>(District
                        Wise)
                    </td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        13
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton16" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/PaddyMilling_Status_MRFD.aspx">Paddy Milling Status/MRFD</asp:LinkButton>(District
                        Wise)
                    </td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        14
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton17" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/IssueCenter_wise.aspx">IssueCenter_wise</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        15
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton18" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/Miller_Wise.aspx">Miller_Wise</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        16
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton19" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/Quality_Ins_CMR_wise.aspx">Quality_Ins_CMR_wise</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        17
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton20" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/Date_Wise.aspx">Paddy and CMR Days_wise</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        18
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton21" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/PM_Inspection_Team_rpt.aspx">CMR Team Wise</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        19
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton22" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/Balance_In_Godown_rpt.aspx">Balance available in Godown</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        20
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton23" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/CMR_Other_District.aspx">CMR Deposited in Other District (Sending District)</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        21
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton24" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/PM_AllAgreement_AllDist_S.aspx">Miller All Agreement Status (2)</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        22
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton25" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/PM_Inspection_ByOnemember_Paddy.aspx">CMR Inspection Report</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        23
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton26" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/PM_Inspection_ByOnemember_Wheat.aspx">Wheat Inspection Report</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        24
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton27" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/PM_Rejection_Rpt.aspx">CMR Rejection Report</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        25
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton28" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/PM_Recvng_Frm_Sending_OD.aspx">CMR Deposited in other District(Receiving District)</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        26
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton29" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/PM_MillingCapacity_rpt.aspx">PM Milling Capacity</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        27
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton30" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/PM_Month_Wise.aspx">PM Month Wise</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        28
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton31" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/PM_MillerFDRCheck.aspx">Paddy Milling Security Amount</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        29
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton32" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/PaddingMilling_DO_Society.aspx">Paddy Milling DO Society</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        30
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton33" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/ChequeAgainst_FDR.aspx">Cheque Against FDR</asp:LinkButton>
                    </td>
                </tr>
                      <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        31
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton34" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/Total_Security_Amount.aspx">Total Security Amount</asp:LinkButton>
                    </td>
                </tr>

                  <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        32
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton35" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/QualityInspection_HOLevelInspWise.aspx">QC  HO Inspector Details (QC-04)</asp:LinkButton>
                    </td>
                </tr>

                  <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        33
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton36" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/QualityInspection_ICInspectorDetailsWise.aspx">QI IC Inspector Details (QC-03)</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        34
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton38" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/QualityControl_IssueCenterWiseSRDetails.aspx">QI IC Wise Details (QC-02)</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        35
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton39" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/QualityControl_DistWiseSRDetails.aspx">QI District Wise Details (QC-01)</asp:LinkButton>
                    </td>
                </tr>

                 <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        36
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton37" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/Milller_Defaulters.aspx">Same FDR and Cheque Number</asp:LinkButton>
                    </td>
                </tr>





                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        &nbsp;
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2" style="font-size: large; position: static; background-color: #FF5050;
                        font-weight: 700; text-align: center;">
                        Exception Reports
                    </td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        1
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton12" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/PM_Evaluation_RemQty.aspx">Miller Evaluation Report By Rem. Agreement Qty</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        2
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton13" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/PM_Progress_RemMilling.aspx">Paddy Milling Progressive Report By Milling(%)</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">
                        3
                    </td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton14" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/PMDivision_RemRice.aspx">Paddy Milling Status By Rem. Rice Qty.</asp:LinkButton>(Division
                        Wise)
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
