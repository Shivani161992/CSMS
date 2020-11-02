<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="MovementOrderHome.aspx.cs" Inherits="State_MovementOrderHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <table align="center" style="width: 600px; border-style: solid; border-width: 1px; text-align: left" border="1" cellspacing="0" cellpadding="6">
        <tr>
            <td colspan="3" style="text-align: center; font-size: large; color: #CC6600; text-decoration: underline; font-weight: bold">Welcome To PDS Movement Order</td>
        </tr>

        <tr>
            <td rowspan="6" style="width: 2px">&nbsp;</td>
            <td style="background-color: #CCCCCC">&nbsp;</td>
            <td rowspan="6" style="width: 2px">&nbsp;</td>
        </tr>
        <tr>
            <td>
                <ul style="color: blue;">
                    <li>
                        <asp:HyperLink ID="hyplNew" runat="server" NavigateUrl="~/State/PDSMovementOrder.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> New Movement Order</asp:HyperLink>
                    </li>
                    <li>
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/State/PDS_SubMovementOrder.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Sub Movement Order</asp:HyperLink>&nbsp;(By Rack)
                    </li>
                    <li>
                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/State/PDSMovementOrder_Acpt.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Approval / Reject Movement Order</asp:HyperLink>
                    </li>
                    <li>
                        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/State/CMR_Movement_FromMill.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Inter District CMR Movement from Mill</asp:HyperLink>&nbsp;(By Road)
                    </li>
                    <li>
                        <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/State/UpdateCMR_DepositOrder_PDSMovementOrder.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Update CMR Deposit Order (Movement Order)</asp:HyperLink>&nbsp;(By Road)
                    </li>
                    <%-- <li >
                        <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/State/PDSMO_SDN.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> SCN</asp:HyperLink>
                    </li>--%>
                </ul>
            </td>
        </tr>
        <tr>
            <td style="background-color: #CCCCCC; text-align: center; font-size: large; color: #0000FF; text-decoration: underline; font-weight: bold">Cancellation</td>
        </tr>
        <tr>
            <td>
                <ul style="color: blue;">
                    <li>
                        <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="~/State/PDSMO_Cancel.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Cancellation of Movement Order</asp:HyperLink>
                    </li>
                </ul>
            </td>
        </tr>
        <tr>
            <td style="background-color: #CCCCCC; text-align: center; font-size: large; color: #0000FF; text-decoration: underline; font-weight: bold">Reprint</td>
        </tr>
        <tr>
            <td>
                <ul style="color: blue;">
                    <li>
                        <asp:HyperLink ID="HyplReprint" runat="server" NavigateUrl="~/State/Reprint_PDSMovementOrder.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Reprint Movement Order</asp:HyperLink>
                    </li>
                    <li>
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/State/Reprint_PDSMOCancelled.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Reprint Cancelled Movement Order</asp:HyperLink>
                    </li>
                </ul>
            </td>
        </tr>
   <%--     <tr>
            <td style="background-color: #CCCCCC; text-align: center; font-size: large; color: #0000FF; font-weight: bold"><span style="text-decoration: underline">Reports</span> (PDS Movement)</td>
        </tr>--%>

        <%--        <tr>
            <td>
                <ul style="color: blue;">
                    <li>
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Reports_State/MovmtOrderStatus_Rpt.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">PDS Movement Order</asp:HyperLink>
                        (By Road)
                    </li>
                    <li>
                        <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/Reports_State/PDSMO_Summary_ByRoad.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Total Summary</asp:HyperLink>
                        (By Road)
                    </li>
                    <li>
                        <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/Reports_State/PDS_TruckMovmt.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Truck Movement</asp:HyperLink>
                        (Month Wise)
                    </li>
                    <li>
                        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Reports_State/MovmtOrderRackStatus_Rpt.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">PDS Movement Order</asp:HyperLink>
                        (By Rack)
                    </li>
                    <%-- <li>
                        <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/Reports_State/SMSStatus_Rpt.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">SMS Status</asp:HyperLink>
                    </li>--%>
        <%--           </ul>
            </td>
        </tr>--%>

        <%--        <tr>
            <td style="background-color: #CCCCCC; text-align: center; font-size: large; color: #0000FF; font-weight: bold"><span style="text-decoration: underline">Reports</span> (Gunny Movement)</td>
        </tr>

        <tr>
            <td>
                <ul style="color: blue;">
                    <li>
                        <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/Reports_State/GunnyOrderStatus_Rpt.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Gunny Movement Order</asp:HyperLink>
                    </li>
                </ul>
            </td>
        </tr>--%>

        <%--<tr>
            <td style="background-color: #CCCCCC; text-align: center; font-size: large; color: #0000FF; font-weight: bold"><span style="text-decoration: underline">Employee Contact Details</span></td>
        </tr>

        <tr>
            <td>
                <ul style="color: blue;">
                    <li>
                        <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/State/PDSMO_ContactDetails.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Employee Contact Details of PDS Movement Department</asp:HyperLink>
                    </li>
                </ul>
            </td>
        </tr>--%>

        <tr>
            <td colspan="3" style="text-align: center; font-size: large; color: #CC6600; text-decoration: underline; font-weight: bold"></td>
        </tr>
    </table>





</asp:Content>

