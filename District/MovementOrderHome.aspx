<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="MovementOrderHome.aspx.cs" Inherits="District_MovementOrderHome" %>

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
            <td colspan="3" style="text-align: center; font-size: large; color: #CC6600; text-decoration: underline; font-weight: bold">Welcome To PDS Movement Order</td>
        </tr>

        <tr>
            <td rowspan="7" style="width: 2px">&nbsp;</td>
            <td style="background-color: #CCCCCC">&nbsp;</td>
            <td rowspan="7" style="width: 2px">&nbsp;</td>
        </tr>
        <tr>
            <td>
                <ul style="color: blue;">
                    <li>
                        <asp:HyperLink ID="HyplMvmtPlan" runat="server" NavigateUrl="~/District/MvmtPlanAgainst_PDSMovmtOrder.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Movement Plan</asp:HyperLink><span style="font-size: large;"><strong> (By Road)</strong></span> [प्राप्तकर्ता जिले द्वारा भरा जाये]
                       <asp:Image ID="Image3" runat="server" Width="25px" Style="vertical-align: middle" Height="22px" CssClass="ButtonClass" ImageUrl="~/Images/HELP.png" title="मुख्यालय द्वारा जारी किये गए By Road PDS Movement Order के विरुद्ध प्राप्तकर्ता जिलों द्वारा Movement Plan  इस विकल्प से बनाया जाना है | यदि इस स्क्रीन में कोई Movement Order दर्शित नहीं होता है , इसका आशय है कि आपके जिले को कोई Movement Plan जारी नहीं करना है | " />
                    </li>
                           <li>
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/District/TOByRackAgainst_PDSMO.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Transport Order</asp:HyperLink><span style="font-size: large;"><strong> (TO)</strong></span> <span style="margin-left:25px">[प्राप्तकर्ता (By Rack) जिले द्वारा भरा जाये]</span>
                         <asp:Image ID="Image2" runat="server" Style="vertical-align: middle" Width="25px" Height="22px" CssClass="ButtonClass" ImageUrl="~/Images/HELP.png" title="मुख्यालय द्वारा जारी किये गए PDS Movement Order के विरुद्ध प्राप्तकर्ता (By Rack) जिले द्वारा अपने HLRT परिवहनकर्ता को परिवहन आदेश इस विकल्प के माध्यम से जारी किये जावेगा | यदि इस स्क्रीन में कोई Movement Order दर्शित नहीं होता है , इसका आशय है कि आपके जिले द्वारा परिवहन आदेश हेतु कोई Movement Order लंबित नहीं है | " />
                    </li>
                    <li>
                        <asp:HyperLink ID="hyplTO" runat="server" NavigateUrl="~/District/TOAgainst_PDSMovmtOrder.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Transport Order</asp:HyperLink><span style="font-size: large;"><strong> (TO)</strong></span> <span style="margin-left:25px">[प्रेषणकर्ता जिले द्वारा भरा जाये]</span>
                         <asp:Image ID="Image1" runat="server" Style="vertical-align: middle" Width="25px" Height="22px" CssClass="ButtonClass" ImageUrl="~/Images/HELP.png" title="मुख्यालय द्वारा जारी किये गए PDS Movement Order के विरुद्ध प्रेषण कर्ता(By Rack & By Road) जिले द्वारा अपने HLRT/LRT  परिवहनकर्ता को परिवहन आदेश इस विकल्प के माध्यम से जारी किये जावेगा | यदि इस स्क्रीन में कोई Movement Order दर्शित नहीं होता है , इसका आशय है कि आपके जिले द्वारा परिवहन आदेश हेतु  कोई Movement Order लंबित नहीं है | " />
                    </li>
                      <li>
                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/District/DeliveryChallan_ByRailRack.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Delivery Challan</asp:HyperLink> <span style="margin-left:68px">[प्राप्तकर्ता (By Rack) जिले द्वारा भरा जाये]</span>
                    </li>
              <%--  <li>
                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/District/RackReceived.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Rack Point Entry</asp:HyperLink> <span style="margin-left:63px">[प्राप्तकर्ता (By Rack) जिले द्वारा भरा जाये]</span>
                    </li>--%>
                      <li>
                        <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/District/TruckChallan_Book.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Truck Challan (T.C) Book</asp:HyperLink>
                    </li>
                      <li>
                        <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="~/District/Divert_Receipt_Entry_MO.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Divert Receipt Entry To Other Issue Centre</asp:HyperLink>
                    </li>

                     <li>
                        <asp:HyperLink ID="HyperLink10" runat="server" NavigateUrl="~/District/PDS_TransportOrder_WithinDistrict_IC.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Intra District Transport Order (Issue Centre)</asp:HyperLink>
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
                        <asp:HyperLink ID="HyplMvmtPlan0" runat="server" NavigateUrl="~/District/Reprint_MvmtPlan.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Reprint Movement Plan</asp:HyperLink><span style="font-size: large;"><strong>&nbsp;&nbsp;&nbsp;(By Road)</strong></span> [प्राप्तकर्ता जिला]
                    </li>
                      <li>
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/District/Reprint_TO_RecByRack_MO.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Reprint Transport Order</asp:HyperLink><span style="font-size: large;"><strong> &nbsp;(By Rack)</strong></span> [प्राप्तकर्ता जिला]
                    </li>
                    <li>
                        <asp:HyperLink ID="HyplReprintTORoad" runat="server" NavigateUrl="~/District/Reprint_TO_ByRoad_MO.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Reprint Transport Order</asp:HyperLink><span style="font-size: large;"><strong> &nbsp;(By Road)</strong></span> [प्रेषणकर्ता जिला]
                    </li>
                    <li>
                        <asp:HyperLink ID="HyplReprintTORack" runat="server" NavigateUrl="~/District/Reprint_TO_ByRack_MO.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Reprint Transport Order</asp:HyperLink><span style="font-size: large;"><strong> &nbsp;(By Rack)</strong></span> [प्रेषणकर्ता जिला]
                    </li>

                    <li>
                        <asp:HyperLink ID="HyperLink11" runat="server" NavigateUrl="~/District/Reprint_PDS_Transport_Order_WithinDist_IC.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Intra District Transport Order (Issue Centre)</asp:HyperLink><span style="font-size: large;"><strong> &nbsp;(By Road)</strong></span>
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
                    <li>
                        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/District/Delete_PDSMO_DC.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Delete Delivery Challan</asp:HyperLink><span style="font-size: large;"><strong> (By Issue Centre)</strong></span>
                    </li>
                    <li>
                        <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/District/Delete_PDSMODist_DC.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Delete Delivery Challan</asp:HyperLink><span style="font-size: large;"><strong> (By Dist Office)</strong></span>
                    </li>
                     <li>
                        <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/District/Delete_PDSMO_ReceiptEntry.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Delete Receipt Entry</asp:HyperLink>
                    </li>
                    
                    <li>
                        <asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="~/District/PDS_MovPlan_Delete.aspx" Font-Bold="True" Font-Size="Large" ForeColor="Black"> Delete Movement Plan (By Road)</asp:HyperLink>
                    </li>
                </ul>
            </td>
        </tr>
        <tr>
            <td style="background-color: #CCCCCC">&nbsp;</td>
        </tr>

        <tr>
            <td colspan="3" style="text-align: center; font-size: large; color: #CC6600; text-decoration: underline; font-weight: bold"></td>
        </tr>
    </table>


</asp:Content>

