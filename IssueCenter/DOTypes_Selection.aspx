<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="DOTypes_Selection.aspx.cs" Inherits="IssueCenter_DOTypes_Selection" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 744px; height: 163px; border-right: maroon thin groove; border-top: maroon thin groove; border-left: maroon thin groove; border-bottom: maroon thin groove;">
        <tr>
            <td colspan="2" style="height: 1px; text-align: center; border-bottom: blue thin groove;">
                <span style="color: #cc0033"><span style="font-size: 11pt">
                जिला कार्यालय द्वारा मिलर
                    को धान देना ,ओपन सेल , डेमेज सेल की बिक्री हेतु DO बनाये जाते है</span> </span>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 1px; text-align: center; border-bottom: blue thin groove;">
                <span style="font-size: 11pt; color: #6600ff">उपरोक्त के अलावा DO केन्द्र पर बनाये जायेंगे
                    , कृपया जानकारी भरने से पूर्व सुनिश्चित कर ले |</span></td>
        </tr>
        <tr>
            <td colspan="2" style="border-right: blue thin groove; border-bottom: blue thin groove;
                height: 19px" valign="top">
            </td>
        </tr>
        <tr>
            <td colspan="2" style="border-right: blue thin groove; border-bottom: blue thin groove;
                height: 19px" valign="top">
                <asp:RadioButton ID="rdDPY" runat="server" Font-Names="Times New Roman" Font-Size="Small"
                    OnCheckedChanged="rdDPY_CheckedChanged" Text="द्वार प्रदाय योजना के अंतर्गत जारी  लिए यहाँ क्लिक करें" AutoPostBack="True" GroupName="1" ForeColor="red" /></td>
        </tr>
        <tr>
            <td style="width: 266px; height: 19px; border-right: blue thin groove; border-bottom: blue thin groove;" valign = "top">
                <br />
                <asp:RadioButton ID="rdIssue" runat="server" Font-Names="Times New Roman" Font-Size="Small"
                    OnCheckedChanged="rdIssue_CheckedChanged" Text="केन्द्र द्वारा बनाये गए DO की जानकरी भरने हेतु यह चुने" AutoPostBack="True" GroupName="1" ForeColor="#0000CC" Width="343px" /></td>
            <td style="width: 100px; height: 19px; border-bottom: blue thin groove;" valign = "top">
                <br />
                <asp:RadioButton ID="rdDist" runat="server" Font-Names="Times New Roman" Font-Size="Small"
                    OnCheckedChanged="rdDist_CheckedChanged" Text="जिला कार्यालय द्वारा बनाये गए DO की जानकरी भरने हेतु यह चुने"
                    Width="400px" AutoPostBack="True" GroupName="1" ForeColor="#0000CC" /></td>
        </tr>
    </table>
</asp:Content>

