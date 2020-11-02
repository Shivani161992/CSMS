<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true"
    CodeFile="FrmPaddyMillingReports.aspx.cs" Inherits="District_FrmPaddyMillingReports"
    Title="Milling Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:Panel ID="Panel3" runat="server" Height="800px" ScrollBars="Both">
            <table style="border: 1pt solid navy; color: black; background-color: #FFFF99; font-family: Calibri;"
                border="1" cellpadding="0" cellspacing="0" width="800px">
                <tr>
                    <td align="center" style="font-weight: bold; font-size: 15pt; color: black; background-color: #cccccc;"
                        colspan="3">
                        Reports
                    </td>
                </tr>
                <tr>
                    <td align="center" style="font-weight: bold; font-size: 15pt; color: black; background-color: #cccccc;"
                        colspan="3">
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        &nbsp;
                    </td>
                    <td align="left" style="text-align: center">
                        <b>Paddy Milling Reports</b>
                    </td>
                    <td align="left">
                        <asp:LinkButton ID="bakbtn" runat="server" ForeColor="Navy" PostBackUrl="~/District/FrmReports_Districts.aspx">Back</asp:LinkButton>
                    </td>
                </tr>
                <%--                <tr>
                    <td align="left">1</td>
                    <td align="left">
                        <asp:LinkButton ID="lnktrucrec_drill0" runat="server" ForeColor="Navy"
                            PostBackUrl="~/ReportForms_District/Rpt_PaddyMillingStatus.aspx">Paddy Milling status Report</asp:LinkButton></td>
                    <td>&nbsp;</td>
                </tr>--%>
                <tr>
                    <td align="left">
                        1
                    </td>
                    <td align="left">
                        <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Navy" PostBackUrl="~/Reports_State/MillerRegistration_rpt.aspx">Miller Registration Report</asp:LinkButton>&nbsp;(CropYear
                        Wise)
                        <td>
                            &nbsp;
                        </td>
                </tr>
                <tr>
                    <td align="left">
                        2
                    </td>
                    <td align="left">
                        <asp:LinkButton ID="LinkButton2" runat="server" ForeColor="Navy" PostBackUrl="~/Reports_State/PMillingStatus_Division.aspx">Paddy Milling Status</asp:LinkButton>&nbsp;(CropYear
                        & Division Wise)
                        <td>
                            &nbsp;
                        </td>
                </tr>
                <tr>
                    <td align="left">
                        3
                    </td>
                    <td align="left">
                        <asp:LinkButton ID="LinkButton3" runat="server" ForeColor="Navy" PostBackUrl="~/Reports_State/PMillingStatus_District.aspx">Paddy Milling Status</asp:LinkButton>&nbsp;(District
                        Wise)
                        <td>
                            &nbsp;
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
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        5
                    </td>
                    <td align="left">
                        <asp:LinkButton ID="LinkButton5" runat="server" ForeColor="Navy" PostBackUrl="~/Reports_State/PM_MillerStatus.aspx">Paddy Milling Status</asp:LinkButton>&nbsp;(Miller
                        Wise)
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        6
                    </td>
                    <td align="left">
                        <asp:LinkButton ID="LinkButton6" runat="server" ForeColor="Navy" PostBackUrl="~/Reports_State/PaddyLift_Godown.aspx">Godown Wise Paddy Lifting Position</asp:LinkButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        7
                    </td>
                    <td align="left">
                        <asp:LinkButton ID="LinkButton7" runat="server" ForeColor="Navy" PostBackUrl="~/Reports_State/PaddyLift_Miller.aspx">Miller Wise Paddy Lifting Position</asp:LinkButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        8
                    </td>
                    <td align="left">
                        <asp:LinkButton ID="LinkButton8" runat="server" ForeColor="Navy" PostBackUrl="~/ReportForms_District/CMRDeposit_Godown.aspx">Godown Wise CMR Deposit Details</asp:LinkButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        9
                    </td>
                    <td align="left">
                        <asp:LinkButton ID="LinkButton9" runat="server" ForeColor="Navy" PostBackUrl="~/ReportForms_District/CMRDeposit_Miller.aspx">Miller Wise CMR Deposit Details</asp:LinkButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        10
                    </td>
                    <td align="left">
                        <asp:LinkButton ID="LinkButton10" runat="server" ForeColor="Navy" PostBackUrl="~/Reports_State/PM_AllAgreement.aspx">Miller All Agreement Status</asp:LinkButton>(District
                        Wise)
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        11
                    </td>
                    <td align="left">
                        <asp:LinkButton ID="LinkButton11" runat="server" ForeColor="Navy" PostBackUrl="~/Reports_State/CMR_Deposit_District.aspx">CMR Deposited In Godown</asp:LinkButton>(District
                        Wise)
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        12
                    </td>
                    <td align="left">
                        <asp:LinkButton ID="LinkButton12" runat="server" ForeColor="Navy" PostBackUrl="~/Reports_State/CMR_FD_TO_OD.aspx">CMR Deposited In Other District</asp:LinkButton>(District
                        Wise)
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        13
                    </td>
                    <td align="left">
                        <asp:LinkButton ID="LinkButton22" runat="server" ForeColor="Navy" PostBackUrl="~/Reports_State/Balance_In_Godown_rpt.aspx">Balance available in Godown</asp:LinkButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        14
                    </td>
                    <td align="left">
                        <asp:LinkButton ID="LinkButton13" runat="server" ForeColor="Navy" PostBackUrl="~/Reports_State/PM_Rejection_Rpt.aspx">CMR Rejection Report</asp:LinkButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        15
                    </td>
                    <td align="left">
                        <asp:LinkButton ID="LinkButton14" runat="server" ForeColor="Navy" PostBackUrl="~/Reports_State/Report_Insp_By_One_Member_Dist.aspx">Inspection Report(Paddy) by One member</asp:LinkButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        16
                    </td>
                    <td align="left">
                        <asp:LinkButton ID="LinkButton15" runat="server" ForeColor="Navy" PostBackUrl="~/Reports_State/PM_RemainingCMRDetails_Rpt.aspx">Paddy Milling Remaining CMR Details Report</asp:LinkButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        17
                    </td>
                    <td align="left">
                        <asp:LinkButton ID="LinkButton16" runat="server" ForeColor="Navy" PostBackUrl="~/Reports_State/PM_AllAgreement_AllDist_S.aspx">Miller All Agreement Status 2 (From Date To Date)</asp:LinkButton>(District
                        Wise)
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        18
                    </td>
                    <td align="left">
                        <asp:LinkButton ID="LinkButton17" runat="server" ForeColor="Navy" PostBackUrl="~/Reports_State/PM_MillerFDRCheck.aspx">Paddy Milling Security Amount</asp:LinkButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                    <tr>
                    <td align="left">
                        19
                    </td>
                    <td align="left">
                        <asp:LinkButton ID="LinkButton18" runat="server" ForeColor="Navy" PostBackUrl="~/Reports_State/PaddingMilling_DO_Society.aspx">Paddy Milling DO Society</asp:LinkButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                 <tr>
                    <td align="left">
                        20
                    </td>
                    <td align="left">
                        <asp:LinkButton ID="LinkButton19" runat="server" ForeColor="Navy" PostBackUrl="~/Reports_State/ChequeAgainst_FDR.aspx">Cheque Against FDR</asp:LinkButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                           <tr>
                    <td align="left">
                        21
                    </td>
                    <td align="left">
                        <asp:LinkButton ID="LinkButton20" runat="server" ForeColor="Navy" PostBackUrl="~/Reports_State/Total_Security_Amount.aspx">Total Security Amount</asp:LinkButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>

                <tr>
                    <td align="left">
                        22
                    </td>
                    <td align="left">
                        <asp:LinkButton ID="LinkButton21" runat="server" ForeColor="Navy" PostBackUrl="~/Reports_State/MillerWise_SecurityReport.aspx">Miller Security Status Report

                        </asp:LinkButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>

                 <tr>
                    <td align="left">
                        23
                    </td>
                    <td align="left">
                        <asp:LinkButton ID="LinkButton23" runat="server" ForeColor="Navy" PostBackUrl="~/Reports_State/MillerSecurity_FiguresStatus.aspx">Miller Security Figures Status Report

                        </asp:LinkButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                 <tr>
                    <td align="left">
                        24
                    </td>
                    <td align="left">
                        <asp:LinkButton ID="LinkButton24" runat="server" ForeColor="Navy" PostBackUrl="~/Reports_State/Milller_Defaulters.aspx">Same FDR and Cheque Number

                        </asp:LinkButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                 <tr>
                    <td align="left">
                        25
                    </td>
                    <td align="left">
                        <asp:LinkButton ID="LinkButton25" runat="server" ForeColor="Navy" PostBackUrl="~/Reports_State/FDRNumber_NotAvailable.aspx">FDR Number Not Available

                        </asp:LinkButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                 <tr>
                    <td align="left">
                        26
                    </td>
                    <td align="left">
                        <asp:LinkButton ID="LinkButton26" runat="server" ForeColor="Navy" PostBackUrl="~/Reports_State/ChequeNumber_NotAvailable.aspx">Cheque Number Not Available

                        </asp:LinkButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                  <tr>
                    <td align="left">
                        27
                    </td>
                    <td align="left">
                        <asp:LinkButton ID="LinkButton27" runat="server" ForeColor="Navy" PostBackUrl="~/Reports_State/Expired_FDR.aspx">Expired FDR

                        </asp:LinkButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        28
                    </td>
                    <td align="left">
                        <asp:LinkButton ID="LinkButton28" runat="server" ForeColor="Navy" PostBackUrl="~/Reports_State/ExpiredCheques.aspx">Expired Cheques

                        </asp:LinkButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        29
                    </td>
                    <td align="left">
                        <asp:LinkButton ID="LinkButton29" runat="server" ForeColor="Navy" PostBackUrl="~/Reports_State/ExpiredAdditionalCheques.aspx">Expired Additional Cheques

                        </asp:LinkButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
