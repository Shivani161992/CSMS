<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Markfed_PDY.master" AutoEventWireup="true" CodeFile="FrmPaddyMillingRpt_mfd.aspx.cs" Inherits="State_FrmPaddyMillingRpt_mfd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:Panel ID="Panel3" runat="server" Height="800px" ScrollBars="Both">
            <table style="border: 1pt solid navy; color: black; background-color: #FFFF99; font-family: Calibri;"
                border="1" cellpadding="0" cellspacing="0" width="620px">
                <tr>
                    <td align="center"
                        style="font-weight: bold; font-size: 15pt; color: black; background-color: #cccccc;"
                        colspan="3">Paddy Milling Reports</td>
                </tr>

                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">1</td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton1" runat="server" Font-Size="14px" ForeColor="Navy" PostBackUrl="~/Reports_State/MillerRegistration_rpt.aspx">Miller Registration Report</asp:LinkButton>
                        (CropYear Wise)</td>
                    <td align="left" style="font-weight: bold;">

                        <asp:LinkButton ID="linkbtn1" runat="server" PostBackUrl="~/State/FrmReport_State_mfd.aspx">Back</asp:LinkButton>
                    </td>

                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">2</td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton2" runat="server" Font-Size="14px" ForeColor="Navy" PostBackUrl="~/Reports_State/PMillingStatus_Division.aspx">Paddy Milling Status</asp:LinkButton>
                        (Division Wise)</td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">3</td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton3" runat="server" Font-Size="14px" ForeColor="Navy" PostBackUrl="~/Reports_State/PMillingStatus_District.aspx">Paddy Milling Status</asp:LinkButton>
                        (District Wise)</td>
                </tr>

                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">4</td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton4" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/PMilling_Progressive.aspx">Paddy Milling Progressive Report(%)</asp:LinkButton></td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">5</td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton5" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/PM_Evaluation.aspx">Miller Evaluation Report</asp:LinkButton></td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">6</td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton6" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/CMR_Deposit_Distance_AllMiller.aspx">CMR Deposited In Godown</asp:LinkButton>(Distance Wise)</td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">7</td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton8" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/CMR_Deposit_Dis_AllMiller.aspx">CMR Deposited In Godown</asp:LinkButton>(Miller Wise)</td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">8</td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton9" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/CMR_Deposit_Dis_AllGodown.aspx">CMR Deposited In Godown</asp:LinkButton>(Godown Wise)</td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">9</td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton7" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/PMilling_SelfOtherDist.aspx">Paddy Milling & CMR Receiving Status</asp:LinkButton>(Self + Other Dist)</td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">10</td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton10" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/PM_AllAgreement.aspx">Miller All Agreement Status</asp:LinkButton>(District Wise)</td>
                </tr>
                <%--    <tr>
                    <td align="left" style="font-size: 10pt; position: static;">7</td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton7" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/Paddy_Issued_Distance.aspx">Paddy Issued From Godown</asp:LinkButton>(Distance Wise)</td>
                </tr>--%>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">11</td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton11" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/CMR_Deposit_District.aspx">CMR Deposited In Godown</asp:LinkButton>(District Wise)</td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">&nbsp;</td>
                    <td align="left" style="font-size: 10pt; position: static;">&nbsp;</td>
                </tr>
                <tr>
                    <td align="left" colspan="2" style="font-size: large; position: static; background-color: #FF5050; font-weight: 700; text-align: center;">Exception Reports</td>
                </tr>

                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">1</td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton12" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/PM_Evaluation_RemQty.aspx">Miller Evaluation Report By Rem. Agreement Qty</asp:LinkButton></td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">2</td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton13" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/PM_Progress_RemMilling.aspx">Paddy Milling Progressive Report By Milling(%)</asp:LinkButton></td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">3</td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton14" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/PMDivision_RemRice.aspx">Paddy Milling Status By Rem. Rice Qty.</asp:LinkButton>(Division Wise)</td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>

