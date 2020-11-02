<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Markfed_PDY.master" AutoEventWireup="true" CodeFile="PaddyMillingHome_MFD.aspx.cs" Inherits="State_PaddyMillingHome_MFD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:Panel ID="Panel3" runat="server" Height="400px" ScrollBars="Both">
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

            <table align="center" style="width: 630px; border-style: solid; border-width: 1px; text-align: left" border="1" cellspacing="0" cellpadding="6">
                <tr>
                    <td colspan="3" style="text-align: center; font-size: large; color: #CC6600; text-decoration: underline; font-weight: bold">Welcome To Paddy Milling</td>
                </tr>


                <tr>
                    <td rowspan="5" style="width: 2px">&nbsp;</td>
                    <td style="background-color: #CCCCCC"></td>
                    <td rowspan="5" style="width: 2px">&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <ul style="color: blue;">
                            <li>
                                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/PaddyMilling/PM_DO_AddGodown.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Update Paddy Godown</asp:HyperLink>
                                &nbsp;<strong>(Mapping)</strong></li>
                            <li>
                                <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/PaddyMilling/PM_DOUpdate_Shortage.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Update Paddy Milling Delivery Order</asp:HyperLink>
                                &nbsp;<strong>(Shortage)</strong></li>

                             <li>
                                <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/PaddyMilling/Update_MillerRegistration_OtherState.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Update Miller Registration (Other State)</asp:HyperLink>
                                &nbsp;</li>

                              <li>
                                <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/PaddyMilling/AcceptRegistration_OtherState.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Approve Miller Registration (Other State)</asp:HyperLink>
                                &nbsp;</li>
                        </ul>
                    </td>
                </tr>

                <tr>
                    <td style="background-color: #CCCCCC; text-align: center; font-size: large; color: #0000FF; text-decoration: underline; font-weight: bold">Delete</td>
                </tr>

                <tr>
                    <td>
                        <ul style="color: blue;">
                            <li>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/PaddyMilling/Del_RcptCMR_FCI.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Delete Receipt Entry CMR For FCI</asp:HyperLink>
                                &nbsp;</li>

                        </ul>
                    </td>
                </tr>

                <%--<tr>
                    <td style="background-color: #CCCCCC; text-align: center; font-size: large; color: #0000FF; text-decoration: underline; font-weight: bold">Delete</td>
                </tr>
                <tr>
                    <td>
                        <ul style="color: blue;">

                            <li>
                                <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/PaddyMilling/PM_Challan_Delete.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Delete Paddy Challan</asp:HyperLink>
                            </li>
                            <li>
                                <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/PaddyMilling/PM_DO_Delete.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Delete Paddy Milling Delivery Order</asp:HyperLink>
                            </li>--%>

                <%--  <li>
                            <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/PaddyMilling/PM_Agrmt_Delete.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Active/ Deactive/ Delete Miller Agreement</asp:HyperLink>
                        </li>--%>
                <%-- </ul>
                    </td>
                </tr>--%>

                <tr>
                    <td style="background-color: #CCCCCC"></td>
                </tr>

                <tr>
                    <td colspan="3" style="text-align: center; font-size: large; color: #CC6600; text-decoration: underline; font-weight: bold"></td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>

