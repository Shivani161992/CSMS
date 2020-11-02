<%@ Page Title="Print" Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="frm_Procurment_Print.aspx.cs" Inherits="IssueCenter_frm_printpages" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <table style="width: 647px; height: 163px; border-right: maroon thin groove; border-top: maroon thin groove; border-left: maroon thin groove; border-bottom: maroon thin groove;">

        <tr>
            <td style="height: 19px; border-right: blue thin groove; border-bottom: blue thin groove; font-size: larger; background-color: #99CCFF;"
                valign="top">Wheat Procurement Print </td>
        </tr>

        <tr>
            <td style="height: 19px; border-right: blue thin groove; border-bottom: blue thin groove;"
                valign="top"></td>
        </tr>

        <tr>
            <td style="height: 19px; border-right: blue thin groove; border-bottom: blue thin groove; text-align: left;"
                valign="top">
                <asp:RadioButton ID="rdprintrecp" runat="server" Font-Names="Times New Roman" Font-Size="Small"
                    Text="Reprint Receipt Detail(wheat-2016)"
                    AutoPostBack="True" GroupName="1" ForeColor="#0000CC" Width="227px"
                    Style="font-family: Calibri"
                    OnCheckedChanged="rdprintrecp_CheckedChanged" />
            </td>
        </tr>

        <tr>
            <td style="border-bottom: blue thin groove; height: 19px; text-align: left;"
                valign="top">
                <asp:RadioButton ID="rdprintac" runat="server" Font-Names="Times New Roman" Font-Size="Small"
                    OnCheckedChanged="rdprintac_CheckedChanged"
                    Text="Reprint Acceptance Note No. Detail(wheat-2016)"
                    AutoPostBack="True" GroupName="1" ForeColor="#0000CC" Width="294px"
                    Style="font-family: Calibri" /></td>
        </tr>

        <tr>
            <td style="border-bottom: blue thin groove; height: 19px; text-align: left;"
                valign="top">
                <asp:RadioButton ID="rdprintRej" runat="server" Font-Names="Times New Roman"
                    Font-Size="Small" Text="Reprint Partial Rejection (2016)"
                    AutoPostBack="True" GroupName="1" ForeColor="#0000CC" Width="225px"
                    Style="font-family: Calibri"
                    OnCheckedChanged="rdprintRej_CheckedChanged" /></td>
        </tr>
        <tr>
            <td style="border-bottom: blue thin groove; height: 19px; text-align: left;"
                valign="top">
                <asp:RadioButton ID="rdprintwhr" runat="server"
                    Font-Names="Times New Roman" Font-Size="Small"
                    OnCheckedChanged="rdprintwhr_CheckedChanged"
                    Text="Reprint WHR Request No."
                    AutoPostBack="True" GroupName="1" ForeColor="#C04000" Width="194px"
                    Style="font-family: Calibri; color: #003366;" /></td>
        </tr>
        <tr>
            <td style="border-bottom: blue thin groove; height: 19px; text-align: left;"
                valign="top">&nbsp;</td>
        </tr>
        <tr>
            <td style="border-bottom: blue thin groove; height: 19px; text-align: center; background-color: #FFCC66;"
                valign="top">Paddy Procurement Print</td>
        </tr>
        <tr>
            <td style="border-bottom: blue thin groove; height: 19px; text-align: left;"
                valign="top">&nbsp;</td>
        </tr>
        <tr>
            <td style="border-bottom: blue thin groove; height: 19px; text-align: left;"
                valign="top">
                <asp:RadioButton ID="rdprintpaddy" runat="server" Font-Names="Times New Roman" Font-Size="Small"
                    OnCheckedChanged="rdprintpaddy_CheckedChanged"
                    Text="Reprint Acceptance Note No. Detail(Paddy-2015-16)"
                    AutoPostBack="True" GroupName="1" ForeColor="#0000CC" Width="385px"
                    Style="font-family: Calibri" /></td>
        </tr>
        <tr>
            <td style="border-bottom: blue thin groove; height: 19px; text-align: left;"
                valign="top">
                <asp:RadioButton ID="rdprintpaddyrecipt" runat="server"
                    Font-Names="Times New Roman" Font-Size="Small"
                    OnCheckedChanged="rdprintpaddyrecipt_CheckedChanged"
                    Text="Reprint Receipt Detail"
                    AutoPostBack="True" GroupName="1" ForeColor="#0000CC" Width="385px"
                    Style="font-family: Calibri" /></td>
        </tr>
        <tr>
            <td style="border-bottom: blue thin groove; height: 19px; text-align: left;"
                valign="top">&nbsp;</td>
        </tr>
        <tr>
            <td style="height: 19px; border-right: blue thin groove; border-bottom: blue thin groove; font-size: larger; background-color: #99CCFF;"
                valign="top">Kharif Procurement 2016 Print </td>
        </tr>
        <tr>
            <td style="height: 19px; border-right: blue thin groove; border-bottom: blue thin groove; text-align: left;"
                valign="top">
                <asp:RadioButton ID="rdbReceiptKharif2016" runat="server" Font-Names="Times New Roman" Font-Size="Small"
                    Text="Reprint Receipt Detail (Kharif-2016)"
                    AutoPostBack="True" GroupName="1" ForeColor="#0000CC" Width="227px"
                    Style="font-family: Calibri" OnCheckedChanged="rdbReceiptKharif2016_CheckedChanged" />
            </td>
        </tr>
        <tr>
            <td style="height: 19px; border-right: blue thin groove; border-bottom: blue thin groove; text-align: left;"
                valign="top">
                <asp:RadioButton ID="rdbAcptKharif2016" runat="server" Font-Names="Times New Roman" Font-Size="Small"
                    Text="Reprint Acceptance Note (Kharif-2016)"
                    AutoPostBack="True" GroupName="1" ForeColor="#0000CC" Width="429px"
                    Style="font-family: Calibri" OnCheckedChanged="rdbAcptKharif2016_CheckedChanged" />
            </td>
        </tr>
        <tr>
            <td style="height: 19px; border-right: blue thin groove; border-bottom: blue thin groove; text-align: left;"
                valign="top">
                <asp:RadioButton ID="rdbdepositerKharif2016" runat="server" Font-Names="Times New Roman" Font-Size="Small"
                    Text="Reprint Depositer Form (Kharif-2016)"
                    AutoPostBack="True" GroupName="1" ForeColor="#0000CC" Width="429px"
                    Style="font-family: Calibri" OnCheckedChanged="rdbdepositerKharif2016_CheckedChanged" />
            </td>
        </tr>
        <tr>
            <td style="height: 19px; border-right: blue thin groove; border-bottom: blue thin groove; text-align: left;"
                valign="top">
                <asp:RadioButton ID="rdbPartialRejection" runat="server" Font-Names="Times New Roman" Font-Size="Small"
                    Text="Reprint Partial Rejection (Kharif-2016)"
                    AutoPostBack="True" GroupName="1" ForeColor="#0000CC" Width="429px"
                    Style="font-family: Calibri" OnCheckedChanged="rdbPartialRejection_CheckedChanged" />
            </td>
        </tr>
        <tr>
            <td style="border-bottom: blue thin groove; height: 19px; text-align: left;"
                valign="top">&nbsp;</td>
        </tr>
        <tr>
            <td style="border-bottom: blue thin groove; height: 19px; text-align: left;"
                valign="top">&nbsp;</td>
        </tr>
    </table>
</asp:Content>

