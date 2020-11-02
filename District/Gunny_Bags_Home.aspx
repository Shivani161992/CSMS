<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Gunny_Bags_Home.aspx.cs" Inherits="District_Gunny_Bags_Home" %>

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
        .ButtonClass
        {
            cursor: pointer;
        }
    </style>

    <table align="center" style="width: 740px; border-style: solid; border-width: 1px; text-align: left" border="1" cellspacing="0" cellpadding="6">
        <tr>
            <td colspan="3" style="text-align: center; font-size: large; color: #CC6600; text-decoration: underline; font-weight: bold">Welcome To Gunny Bags Home</td>
        </tr>

        <tr>
            <td rowspan="15" style="width: 2px">&nbsp;</td>
            <td style="background-color: #CCCCCC">&nbsp;</td>
            <td rowspan="15" style="width: 2px">&nbsp;</td>
        </tr>
        <tr>
            <td>
                <ul style="color: blue;">
                    <li>
                        <asp:HyperLink ID="HyplMvmtPlan" runat="server" NavigateUrl="~/GunnyBags/Gunny_Bags_Opening_Bal.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Gunny Bags Opening Balance</asp:HyperLink>
                    </li>

                    <li>
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/GunnyBags/Gunny_Bags_R_Receipt.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Gunny Bags Receiving (RR)</asp:HyperLink>
                    </li>
                    
                    <li>
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/GunnyBags/Gunny_Bags_AllocationTransportOrder.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Gunny Bags Transport Order (Godown)</asp:HyperLink>
                    </li>
                    <li>
                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/GunnyBags/GunnyBags_Distribution.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Gunny Bags Delivery Challan from Rail Head (Godown)</asp:HyperLink>
                    </li>
                      <li>
                        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/GunnyBags/Gunny_Bags_GodownToSocietyMapping.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Gunny Bags Godown to Society Mapping</asp:HyperLink>
                    </li>

                     <li>
                        <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/GunnyBags/GunnyBags_TransportOrder_GodownToSociety.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black">Gunny Bags Transport Order (Society)</asp:HyperLink>
                    </li>
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



        <tr>
            <td style="background-color: #CCCCCC"></td>
        </tr>

        <tr>
            <td colspan="3" style="text-align: center; font-size: large; color: #CC6600; text-decoration: underline; font-weight: bold"></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

