<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="PaddyProcHome2016_RptIC.aspx.cs" Inherits="Report_IssueCenter_PaddyProcHome2016_RptIC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:Panel ID="Panel3" runat="server" Height="800px" ScrollBars="Both">
            <table style="border: 1pt solid navy; color: black; background-color: #FFFF99; font-family: Calibri;"
                border="1" cellpadding="0" cellspacing="0" width="620px">
                <tr>
                    <td align="center"
                        style="font-weight: bold; font-size: 15pt; color: black; background-color: #cccccc;"
                        colspan="2">Procurement Reports 2016</td>
                </tr>

                <tr>
                    <td align="left" style="font-size: 10pt; position: static;" colspan="2">&nbsp;</td>
                </tr>

                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">1.</td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton1" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Report_IssueCenter/CheckList_Paddy2016.aspx">Check List On Date</asp:LinkButton></td>
                </tr>

                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">2.</td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton2" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Report_IssueCenter/CheckList2Dates_Paddy2016.aspx">Check List Between Two Dates</asp:LinkButton></td>
                </tr>

                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">3.</td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton3" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Report_IssueCenter/Rcpt_PaddyProc2016.aspx">Daily Receipt Register (Procurement 2016)</asp:LinkButton></td>
                </tr>

                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">4.</td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton4" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Report_IssueCenter/AcptNote_PdyProc2016.aspx">Acceptence Note Details</asp:LinkButton></td>
                </tr>

                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">5.</td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton5" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Report_IssueCenter/StorageWiseDeposit_PdyProc2016.aspx">Storage Wise Deposit Report</asp:LinkButton></td>
                </tr>


                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">6.</td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton6" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Report_IssueCenter/DeleteAcptRcpt_PdyProc2016.aspx">Deleted Acceptance and Receipt Report</asp:LinkButton></td>
                </tr>

                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">7.</td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton7" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Report_IssueCenter/RejTruck_PdyProc2016.aspx">Rejected Truck Report</asp:LinkButton></td>
                </tr>

                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">8.</td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton8" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Report_IssueCenter/GdnWiseRcpt_PdyProc2016.aspx">Godown Wise Receipt</asp:LinkButton></td>
                </tr>

                <tr>
                    <td align="left" style="font-size: 10pt; position: static;" colspan="2">&nbsp;</td>
                </tr>

                <%--           <tr>
                    <td align="left" colspan="2" style="font-size: large; position: static; background-color: #FF5050; font-weight: 700; text-align: center;">By Rail Rack</td>
                </tr>
                
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;" colspan="2">&nbsp;</td>
                </tr>--%>
            </table>
        </asp:Panel>
    </div>
</asp:Content>

