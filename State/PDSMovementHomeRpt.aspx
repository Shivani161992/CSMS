<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="PDSMovementHomeRpt.aspx.cs" Inherits="State_PDSMovementHomeRpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:Panel ID="Panel3" runat="server" Height="800px" ScrollBars="Both">
            <table style="border: 1pt solid navy; color: black; background-color: #FFFF99; font-family: Calibri;"
                border="1" cellpadding="0" cellspacing="0" width="620px">
                <tr>
                    <td align="center"
                        style="font-weight: bold; font-size: 15pt; color: black; background-color: #cccccc;"
                        colspan="2">PDS Movement Order Reports</td>
                </tr>

                <tr>
                    <td align="left" style="font-size: 10pt; position: static;" colspan="2">&nbsp;</td>
                </tr>

                <tr>
                    <td align="left" colspan="2" style="font-size: large; position: static; background-color: #FF5050; font-weight: 700; text-align: center;">By Road</td>
                </tr>

                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">1.</td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton1" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/MovmtOrderStatus_Rpt.aspx">PDS Movement Order Status</asp:LinkButton></td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">2.</td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton2" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/PDSMO_Summary_ByRoad.aspx">PDS Movement Order Total Summary</asp:LinkButton></td>
                </tr>

                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">3.</td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton3" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/PDS_TruckMovmt.aspx">PDS Truck Movement</asp:LinkButton>
                        <strong>&nbsp;(Month Wise)</strong></td>
                </tr>

                 <tr>
                    <td align="left" style="font-size: 10pt; position: static;">4.</td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton7" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/PDS_Diverted_District.aspx">PDS Diverted District</asp:LinkButton>
                        <strong>&nbsp;(Month Wise)</strong></td>
                </tr>

                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">5.</td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton8" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/MovmtOrderStatus_Rpt_Percentage.aspx">PDS Movement Order Status (Percentange)</asp:LinkButton>
                        <strong>&nbsp;</strong></td>
                </tr>
                  <tr>
                    <td align="left" style="font-size: 10pt; position: static;">6.</td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton9" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/MovntOrderStatus_OnChallan_Perc_Rpt.aspx">PDS Movement Order Status (Percentange) On Issued Quantity</asp:LinkButton>
                        <strong>&nbsp;</strong></td>
                </tr>




                <tr>
                    <td align="left" style="font-size: 10pt; position: static;" colspan="2">&nbsp;</td>
                </tr>

                <tr>
                    <td align="left" colspan="2" style="font-size: large; position: static; background-color: #FF5050; font-weight: 700; text-align: center;">By Rail Rack</td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">1</td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton4" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/MovmtOrderRackStatus_Rpt.aspx">PDS Movement Order Status</asp:LinkButton></td>
                </tr>

                <tr>
                    <td align="left" style="font-size: 10pt; position: static;" colspan="2">&nbsp;</td>
                </tr>
               <tr>
                    <td align="left" colspan="2" style="font-size: large; position: static; background-color: #FF5050; font-weight: 700; text-align: center;">Cancelled</td>
                </tr>
                    <tr>
                    <td align="left" style="font-size: 10pt; position: static;">1.</td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton5" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/Cancel_PDSMO_RoadRpt.aspx">Cancelled PDS Movement Order Status</asp:LinkButton><strong>&nbsp;(By Road)</strong></td>
                </tr>
                <tr>
                    <td align="left" style="font-size: 10pt; position: static;">2</td>
                    <td align="left" style="font-size: 10pt; position: static;">
                        <asp:LinkButton ID="LinkButton6" runat="server" Font-Size="14px" ForeColor="Navy"
                            PostBackUrl="~/Reports_State/Cancel_PDSMO_RackRpt.aspx">Cancelled PDS Movement Order Status</asp:LinkButton><strong>&nbsp;(By Rail Rack)</strong></td>
                </tr>

                <tr>
                    <td align="left" style="font-size: 10pt; position: static;" colspan="2">&nbsp;</td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>

