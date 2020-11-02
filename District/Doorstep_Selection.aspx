<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Doorstep_Selection.aspx.cs" Inherits="District_Doorstep_Selection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 647px; height: 163px; border-right: maroon thin groove; border-top: maroon thin groove; border-left: maroon thin groove; border-bottom: maroon thin groove;">
        
        <tr>
            <td style="height: 19px; border-right: blue thin groove; border-bottom: blue thin groove; font-size: larger; background-color: #99CCFF;" 
                valign = "top">
                <strong>द्वारप्रदाय योजना </strong></td>
        </tr>
        
        <tr>
            <td style="height: 19px; border-right: blue thin groove; border-bottom: blue thin groove;" 
                valign = "top">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="border-bottom: blue thin groove; height: 19px" valign="top">
                <asp:RadioButton ID="rddistmstr" runat="server" Font-Names="Times New Roman" Font-Size="Small"
                     Text="बेस डिपो से प्रदाय केंद्र की दूरी की प्रविष्टि हेतू यह चुनें "
                    Width="401px" AutoPostBack="True" GroupName="1" ForeColor="#C04000" OnCheckedChanged="rddistmstr_CheckedChanged" /></td>
        </tr>
        <tr>
            <td style="border-bottom: blue thin groove; height: 19px" valign="top">
                <asp:RadioButton ID="drroutechart" runat="server" Font-Names="Times New Roman" Font-Size="Small"
                    Text="द्वारप्रदाय के परिवहनकर्ता की रूट चार्ट की मेपिंग की प्रविष्टि हेतू यह चुनें "
                    Width="459px" AutoPostBack="True" GroupName="1" ForeColor="#0000CC" OnCheckedChanged="drroutechart_CheckedChanged" /></td>
        </tr>
        
        <tr>
            <td style="border-bottom: blue thin groove; height: 19px" valign="top">
                <asp:RadioButton ID="rdtrans" runat="server" Font-Names="Times New Roman" Font-Size="Small"
                    OnCheckedChanged="rdtrans_CheckedChanged" Text="द्वारप्रदाय के परिवहनकर्ता की प्रविष्टि हेतू यह चुनें "
                    Width="409px" AutoPostBack="True" GroupName="1" ForeColor="#C04000" /></td>
        </tr>
        <tr>
            <td style="border-bottom: blue thin groove; height: 19px" valign="top">
                <asp:RadioButton ID="rdlink" runat="server" Font-Names="Times New Roman" Font-Size="Small"
                    OnCheckedChanged="rdlink_CheckedChanged" 
                    Text="लिंक सोसाइटी की उचित मूल्य दुकानो से मैपिंग हेतू यह चुनें " 
                    AutoPostBack="True" GroupName="1" ForeColor="#0000CC" Width="385px" /></td>
        </tr>
        <tr>
            <td style="border-bottom: blue thin groove; height: 19px" valign="top">
                <asp:RadioButton ID="rdprabhari" runat="server" Font-Names="Times New Roman" Font-Size="Small"
                    OnCheckedChanged="rdprabhari_CheckedChanged" 
                    Text="द्वारप्रदाय के प्रभारी की जानकारी की प्रविष्टि हेतू यह चुनें " 
                    AutoPostBack="True" GroupName="1" ForeColor="#C04000" Width="378px" /></td>
        </tr>
        <tr>
            <td style="border-bottom: blue thin groove; height: 19px" valign="top">
                
                <asp:RadioButton ID="rd_TO" runat="server" Font-Names="Times New Roman" Font-Size="Small" Text="परिवहन आदेश(Transport Order) की जारी करने हेतू यह चुनें "
                    Width="391px" AutoPostBack="True" GroupName="1" ForeColor="#0000CC" OnCheckedChanged="rd_TO_CheckedChanged" /></td>
        </tr>
        <tr>
            <td style="border-bottom: blue thin groove; height: 19px" valign="top">
                <asp:RadioButton ID="RadioButton1" runat="server" Font-Names="Times New Roman" Font-Size="Small" Text="परिवहन आदेश(Transport Order) को प्रिंट करने हेतू यह चुनें "
                    Width="391px" AutoPostBack="True" GroupName="1" ForeColor="#C04000" OnCheckedChanged="RadioButton1_CheckedChanged" /></td>
        </tr>
        <tr>
            <td style="border-bottom: blue thin groove; height: 19px" valign="top">
                <asp:RadioButton ID="rdfpsSuspend" runat="server" Font-Names="Times New Roman" 
                    Font-Size="Small" Text="उ मु दु को निलंबित करने हेतु यहाँ चुने "
                    Width="391px" AutoPostBack="True" GroupName="1" ForeColor="#C04000" OnCheckedChanged="rdfpsSuspend_CheckedChanged" /></td>
        </tr>
    </table>
</asp:Content>

