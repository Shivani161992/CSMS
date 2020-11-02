<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="PaddyMillingHome.aspx.cs" Inherits="District_PaddyMillingHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>
    <script type="text/javascript" src="../PaddyMilling/Scripts/jquery-ui.js"></script>

    <%--Script For Help Option Only Set Background--%>
    <link href="../PaddyMilling/Scripts/jquery-ui.css" rel="stylesheet" />

    <script>
        $(function () {
            $(document).tooltip({
                track: true
            });
        });
    </script>



    <style type="text/css">
        .ButtonClass {
            cursor: pointer;
        }
    </style>

    <table align="center" style="width: 740px; border-style: solid; border-width: 1px; text-align: left" border="1" cellspacing="0" cellpadding="6">
        <tr>
            <td colspan="3" style="text-align: center; font-size: large; color: #CC6600; text-decoration: underline; font-weight: bold">Welcome To Paddy Milling</td>
            <input  runat="server" type="hidden" id="hdfCheckStatus"/>
        </tr>

        <tr>
          
            <td style="background-color: #CCCCCC">&nbsp;</td>
            <td rowspan="15" style="width: 2px">&nbsp;</td>
        </tr>
        <tr>
            <td>
                <ul style="color: blue;">
                     <li>
                        <asp:HyperLink ID="HyperLink23" runat="server" NavigateUrl="~/PaddyMilling/PM_Update_MillerRegistration.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Update Miller Registration</asp:HyperLink>
                    </li>
                    <li>
                        <asp:HyperLink ID="HyplMvmtPlan" runat="server" NavigateUrl="~/PaddyMilling/AcceptRegistration.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Approval Miller Registration</asp:HyperLink>
                    </li>
                    <li>
                        <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/District/godown_distance_Master.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Distance From Paddy Mill To Godown</asp:HyperLink>&nbsp;<strong>(Mapping)</strong>
                    </li>

                     <%--<li>
                        <asp:HyperLink ID="HyperLink40" runat="server" NavigateUrl="~/PaddyMilling/PaddyStock_Position.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Paddy Available In Godown</asp:HyperLink>
                    </li>
--%>

                    <li>
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/PaddyMilling/MillingAgreement.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Paddy Milling Agreement</asp:HyperLink>
                    </li>
                    <li>
                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/PaddyMilling/PaddyMilling_Approved.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Approval / Reject Milling Agreement</asp:HyperLink>
                    </li>
                    <li>
                        <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/PaddyMilling/Paddy_Issued_Godown.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Paddy Issued To Miller From Godown</asp:HyperLink>&nbsp;<strong>(Mapping)</strong>
                    </li>

                    <li>
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/PaddyMilling/PaddyMilling_DO.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Paddy Milling Delivery Order</asp:HyperLink>
                        &nbsp;<strong>(DO)</strong></li>
                    <li runat="server" id="CMRDO">
                        <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="~/PaddyMilling/CMR_DepositOrder.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">CMR Deposit Order</asp:HyperLink>&nbsp;<strong>(Mapping)</strong>
                    </li>
                    <li>
                        <asp:HyperLink ID="HyperLink15" runat="server" NavigateUrl="~/PaddyMilling/Paddy_Adjust_DO.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Adjustment Delivery Order of Paddy</asp:HyperLink>
                    </li>
                </ul>
            </td>
        </tr>
        <tr>
            <td style="background-color: #CCCCCC; text-align: center; font-size: large; color: #0000FF; text-decoration: underline; font-weight: bold">Paddy To FCI</td>
        </tr>
        <tr>
            <td>
                <ul style="color: blue;">
                    <li>
                        <asp:HyperLink ID="HyperLink16" runat="server" NavigateUrl="~/PaddyMilling/Receipt_CMR_FCI.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Receipt Entry CMR For FCI</asp:HyperLink><span style="font-size: large;"></span>
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
                    <%--  <li>
                        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/PaddyMilling/Reprint_PMAgrmt.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Reprint Milling Agreement</asp:HyperLink><span style="font-size: large;"></span>
                    </li>--%>

                    <li>
                        <asp:HyperLink ID="HyperLink10" runat="server" NavigateUrl="~/PaddyMilling/MillerReg_RePrint.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Reprint Miller Registration</asp:HyperLink><span style="font-size: large;"></span>
                    </li>

                    <li>
                        <asp:HyperLink ID="HyperLink19" runat="server" NavigateUrl="~/PaddyMilling/Reprint_MillingAgreement.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Reprint Miller's Agreement</asp:HyperLink><span style="font-size: large;"></span>
                    </li>
                    <li>
                        <asp:HyperLink ID="HyplMvmtPlan0" runat="server" NavigateUrl="~/PaddyMilling/Reprint_PMDO.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Reprint Delivery Order</asp:HyperLink><span style="font-size: large;"><strong>&nbsp;(DO)</strong></span>
                    </li>
                    <li runat="server" id="RCMRDO">
                        <asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="~/PaddyMilling/Reprint_CMRDO.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Reprint CMR Deposit Order</asp:HyperLink><span style="font-size: large;"><strong>&nbsp;(Mapping)</strong></span>
                    </li>
                    <li>
                        <asp:HyperLink ID="HyperLink14" runat="server" NavigateUrl="~/PaddyMilling/Reprint_CMRFCI.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Reprint Receipt Entry CMR For FCI</asp:HyperLink><span style="font-size: large;"></span>
                    </li>

                    <li>
                        <asp:HyperLink ID="HyperLink18" runat="server" NavigateUrl="~/PaddyMilling/Reprint_Ins_CMR_Rej.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Reprint Inspection Of CMR Rejection Entry</asp:HyperLink><span style="font-size: large;"></span>
                    </li>
                </ul>
            </td>
        </tr>

        <tr>
            <td style="background-color: #CCCCCC; text-align: center; font-size: large; color: #0000FF; text-decoration: underline; font-weight: bold">Update</td>
        </tr>
        <tr>
            <td>
                <ul style="color: blue;">
                    <li>
                        <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/PaddyMilling/PMilling_SecurityLot.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Update Miller Agreement Security Lot</asp:HyperLink><span style="font-size: large;"></span>
                    </li>
                    <li>
                        <asp:HyperLink ID="HyperLink20" runat="server" NavigateUrl="~/District/PM_DO_AddGodown_Dist.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Update Godown Mapping </asp:HyperLink><span style="font-size: large;"></span>
                    </li>
               
                    <li>
                        <asp:HyperLink ID="HyperLink21" runat="server" NavigateUrl="~/District/PM_DOUpdate_Shortage.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Update Paddy Milling Delivery Order</asp:HyperLink>
                         &nbsp;<strong>(Shortage)</strong>
                        <span style="font-size: large;"></span>
                    </li>
                    
                     </ul>

            </td>
        </tr>

        <tr>
            <td style="background-color: #CCCCCC; text-align: center; font-size: large; color: #0000FF; text-decoration: underline; font-weight: bold">Delete</td>
        </tr>
        <tr>
            <td>
                <ul style="color: blue;">
                    <%--  <li>
                        <asp:HyperLink ID="HyperLink14" runat="server" NavigateUrl="~/PaddyMilling/PM_Agrmt_Delete.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Delete Paddy Milling Agreement</asp:HyperLink><span style="font-size: large;"></span>
                    </li>--%>
                    <li>
                        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/PaddyMilling/PM_DO_Delete.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Delete Paddy Milling Delivery Order</asp:HyperLink><span style="font-size: large;"></span>
                    </li>
                    <li>
                        <asp:HyperLink ID="HyperLink11" runat="server" NavigateUrl="~/PaddyMilling/PM_Challan_Delete.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Delete Paddy Challan</asp:HyperLink><span style="font-size: large;"></span>
                    </li>
                    <li runat="server" id="DelCMRDO">
                        <asp:HyperLink ID="HyperLink12" runat="server" NavigateUrl="~/PaddyMilling/CMR_DO_Delete.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Delete CMR Deposit Order</asp:HyperLink><span style="font-size: large;"></span>
                    </li>
                    <li runat="server" id="DelCMRRecpt">
                        <asp:HyperLink ID="HyperLink13" runat="server" NavigateUrl="~/PaddyMilling/ReceiptCMR_Delete.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Delete Receipt Entry CMR</asp:HyperLink><span style="font-size: large;"></span>
                    </li>
                </ul>

            </td>
        </tr>

        <tr runat="server" id="CMRInspection">
            <td style="background-color: #CCCCCC; text-align: center; font-size: large; color: #0000FF; text-decoration: underline; font-weight: bold">CMR Inspection</td>
        </tr>
        <tr runat="server" id="CMRInspectionEntry">
            <td>
                <ul style="color: blue;">
                     <li>
                        <asp:HyperLink ID="HyperLink27" runat="server" NavigateUrl="~/PaddyMilling/Inspector_MasterDist.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Inspector Master</asp:HyperLink><span style="font-size: large;"></span>
                    </li>

                   <%-- <li>
                        <asp:HyperLink ID="HyperLink17" runat="server" NavigateUrl="~/PaddyMilling/CMRInspection_Rejection.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> CMR Inspection Rejection Entry</asp:HyperLink><span style="font-size: large;"></span>
                    </li>--%>

               <%--     <li>
                        <asp:HyperLink ID="HyperLink18" runat="server" NavigateUrl="~/PaddyMilling/RejectedCMR_DepositOrder.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Rejected CMR Deposit Order</asp:HyperLink><span style="font-size: large;"></span>
                    </li>--%>
                </ul>

            </td>
        </tr>

        <%--
        <tr runat="server" id="DRCMRDO">
            <td style="background-color: #CCCCCC; text-align: center; font-size: large; color: #0000FF; text-decoration: underline; font-weight: bold">Delete Request</td>
        </tr>
        <tr runat="server" id="DCMRDO">
            <td>
                <ul style="color: blue;">
                    <li>
                        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/PaddyMilling/DelReq_CMR_DO.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Delete Request For CMR Deposit Order</asp:HyperLink><span style="font-size: large;"></span>
                    </li>
                </ul>
            </td>
        </tr>--%>

        <%--        <tr>
            <td style="background-color: #CCCCCC; text-align: center; font-size: large; color: #0000FF; text-decoration: underline; font-weight: bold">Milling Bill</td>
        </tr>
        <tr>
            <td>
                <ul style="color: blue;">
                    <li>
                        <asp:HyperLink ID="HyperLink14" runat="server" NavigateUrl="~/PaddyMilling/PM_TranspRs_Distance.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Paddy Transportation Lead Distance</asp:HyperLink><span style="font-size: large;"></span>
                    </li>
                </ul>

            </td>
        </tr>--%>
         <tr runat="server" id="Tr1">
            <td style="background-color: #CCCCCC; text-align: center; font-size: large; color: #0000FF; text-decoration: underline; font-weight: bold">Bill Generate</td>
        </tr>
        <tr>         <td>
                <ul style="color: blue;">

                    <li>
                        <asp:HyperLink ID="HyperLink22" runat="server" NavigateUrl="~/PaddyMilling/PM_Transporter_Rates_District.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Transporter Rates</asp:HyperLink><span style="font-size: large;"></span>
                    </li>
                     <li>
                        <asp:HyperLink ID="HyperLink26" runat="server" NavigateUrl="~/PaddyMilling/PM_Miller_BankDetails.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Miller Bank Details</asp:HyperLink><span style="font-size: large;"></span>
                    </li>


               <%--     <li>
                        <asp:HyperLink ID="HyperLink18" runat="server" NavigateUrl="~/PaddyMilling/RejectedCMR_DepositOrder.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Rejected CMR Deposit Order</asp:HyperLink><span style="font-size: large;"></span>
                    </li>--%>
                </ul>

            </td>
            </tr>

      

          <tr runat="server" id="Tr2">
            <td style="background-color: #CCCCCC; text-align: center; font-size: large; color: #0000FF; text-decoration: underline; font-weight: bold">FDR and Cheque</td>
        </tr>
        <tr>         <td>
                <ul style="color: blue;">
                       <li>
                        <asp:HyperLink ID="HyperLink25" runat="server" NavigateUrl="~/PaddyMilling/PM_MillerMaster_FDRCheck.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Paddy Milling FDR & Cheque Miller Master</asp:HyperLink><span style="font-size: large;"></span>
                    </li>
                     <li>
                        <asp:HyperLink ID="HyperLink24" runat="server" NavigateUrl="~/PaddyMilling/PM_FDR_Cheque_Update.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Paddy Milling FDR & Cheque Update</asp:HyperLink><span style="font-size: large;"></span>
                    </li>
                   <%-- <li>
                        <asp:HyperLink ID="HyperLink24" runat="server" NavigateUrl="~/Miller_Registeration/MillerMappingWithSociety.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Miller To Society Mapping</asp:HyperLink><span style="font-size: large;"></span>
                    </li>--%>

               <%--     <li>
                        <asp:HyperLink ID="HyperLink18" runat="server" NavigateUrl="~/PaddyMilling/RejectedCMR_DepositOrder.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Rejected CMR Deposit Order</asp:HyperLink><span style="font-size: large;"></span>
                    </li>--%>
                </ul>

            </td>
            </tr>

        <tr>
            <td style="background-color: #CCCCCC"></td>
        </tr>
        <tr>
            <td colspan="3" style="text-align: center; font-size: large; color: #CC6600; text-decoration: underline; font-weight: bold"></td>
        </tr>
    </table>


</asp:Content>



