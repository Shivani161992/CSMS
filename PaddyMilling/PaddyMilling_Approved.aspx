<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="PaddyMilling_Approved.aspx.cs" Inherits="PaddyMilling_PaddyMilling_Approved" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
        .ButtonClass {
            cursor: pointer;
        }
    </style>


        <table align="center" style="width: 680px; border-style: solid; border-width: 1px; text-align: left" border="1" cellspacing="0" cellpadding="6">
        <tr>
            <td colspan="6" style="text-align: center; font-size: large; text-decoration: underline; color: #CC6600"><strong>Approval / Reject Paddy Milling Agreement</strong></td>
        </tr>
        <tr>
            <td rowspan="8">&nbsp;</td>
            <td>अनुबंध का नंबर</td>
            <td>
                <asp:DropDownList ID="ddlAgrmtNo" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlAgrmtNo_SelectedIndexChanged" >
                </asp:DropDownList>
            </td>
            <td>खरीफ विपणन वर्ष</td>
            <td>
                <asp:TextBox ID="txtCropYear" runat="server" Enabled="False" Width="146px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCropYear" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
            <td rowspan="8">&nbsp;</td>
        </tr>
        <tr>
            <td>अनुबंध का जिला</td>
            <td>
                <asp:TextBox ID="TxtDist" runat="server" Enabled="False" Width="146px"></asp:TextBox>
            </td>
            <td>मोबाइल नंबर</td>
            <td>
                 <asp:TextBox ID="txtMobileNo" runat="server" Enabled="False" Width="146px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>जिला प्रबंधक</td>
            <td colspan="3">
                <asp:TextBox ID="txtDMName" runat="server" Enabled="False" Width="450px" Height="20px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>मिल का नाम</td>
            <td colspan="3">
                <asp:TextBox ID="txtMillName" runat="server" Enabled="False" Width="450px"></asp:TextBox></td>
        </tr>
        <tr>
            <td>अनुबंध दिनांक से</td>
            <td>
                <asp:TextBox ID="txtFrmDate" runat="server" Enabled="False" Width="100px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFrmDate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
            <td>अनुबंध दिनांक तक</td>
            <td>
                 <asp:TextBox ID="txtToDate" runat="server" Enabled="False" Width="100px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtToDate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>धान का अनुबंध</td>
            <td>
               <asp:TextBox ID="txtAgrmtLot" runat="server" Enabled="False" Width="100px"></asp:TextBox><b>(लॉट)</b>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtAgrmtLot" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
            <td>सिक्यूरिटी राशि</td>
            <td>
                 <asp:TextBox ID="txtSecurityLot" runat="server" Enabled="False" Width="100px"></asp:TextBox><b>(लॉट)</b>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtSecurityLot" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>कामन धान</td>
            <td>
                <asp:TextBox ID="txtCDahn" runat="server" Enabled="False" Width="100px"></asp:TextBox><b>(Qtls)</b>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtCDahn" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
            <td>ग्रेड-ए धान</td>
            <td>
                 <asp:TextBox ID="txtGDhan" runat="server" Enabled="False" Width="100px"></asp:TextBox><b>(Qtls)</b>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtGDhan" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>कुल धान</td>
            <td>
                <asp:TextBox ID="txtTotalDhan" runat="server" Enabled="False" Width="100px">
                </asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtTotalDhan" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
            <td>मिलिंग का प्रकार</td>
            <td>
                 <asp:TextBox ID="txtMillingType" runat="server" Enabled="False" Width="100px" Height="20px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtMillingType" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr id="FCI" runat="server" visible="false" style="background-color: #000000; color: #FFFFFF; text-align:center">
            <td colspan="6" >इस अनुबंध की धान का CMR सिर्फ
                <asp:Label ID="lblDist" runat="server" Text="" style="color: #FFFF00"></asp:Label>
                की FCI में ही जमा होगा|
            </td>
        </tr>
        <tr>
           
            <td colspan="6" style="text-align: center; font-size: large;">

                <asp:Button ID="btnRecptNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="100px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnRecptNew_Click" />

                <asp:Button ID="btnAccept" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Approval" Width="100px" CssClass="ButtonClass" Style="margin-left: 10px" OnClick="btnAccept_Click" OnClientClick="return confirm('Are You Sure To Approved Milling Agreement?');" Enabled="False" />

                <asp:Button ID="btnReject" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Reject" Width="100px" CssClass="ButtonClass" Style="margin-left: 10px" OnClick="btnReject_Click" OnClientClick="return confirm('Are You Sure To Reject Milling Agreement?');" Enabled="False" />

                <asp:Button ID="btnPrint" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Print" Width="100px" CssClass="ButtonClass" Style="margin-left: 10px" Enabled="False" CausesValidation="False" OnClick="btnPrint_Click" />

                <asp:Button ID="btnRecptClose" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="100px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px" OnClick="btnRecptClose_Click" />
            </td>
            
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

