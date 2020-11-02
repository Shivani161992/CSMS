<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="TruckRejection_Option.aspx.cs" Inherits="IssueCenter_TruckRejection_Option" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="height: 76px; width: 605px">
        <tr>
            <td colspan="2" style="font-size: medium; color: #000099">Select Option for Partial Rejection (आंशिक अस्वीकृत )&nbsp; or Full Rejection 
                (पूर्ण अस्वीकृत)</td>
        </tr>
        <tr>
            <td colspan="2" style="font-size: medium; color: #000099">&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:RadioButton ID="rdPartialPdy2016" runat="server" AutoPostBack="True" Text="आंशिक अस्वीकृत खरीफ 2016" ValidationGroup="1" OnCheckedChanged="rdPartialPdy2016_CheckedChanged" Visible="False" />
            </td>
            <td>
                <asp:RadioButton ID="rdFullPdy2016" runat="server" AutoPostBack="True" Text="पूर्ण अस्वीकृत खरीफ 2016" ValidationGroup="1" OnCheckedChanged="rdFullPdy2016_CheckedChanged" Visible="False" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:RadioButton ID="rdpartial" runat="server" AutoPostBack="True"
                    OnCheckedChanged="rdpartial_CheckedChanged" Text="आंशिक अस्वीकृत 2016"
                    ValidationGroup="1" />
            </td>
            <td>
                <asp:RadioButton ID="rdfull" runat="server" AutoPostBack="True"
                    OnCheckedChanged="rdfull_CheckedChanged" Text="पूर्ण अस्वीकृत 2016"
                    ValidationGroup="1" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:RadioButton ID="rdpartial_15" runat="server" AutoPostBack="True"
                    OnCheckedChanged="rdpartial_15_CheckedChanged" Text="आंशिक अस्वीकृत 2015"
                    ValidationGroup="1" />
            </td>
            <td>
                <asp:RadioButton ID="rdfull_15" runat="server" AutoPostBack="True"
                    OnCheckedChanged="rdfull_15_CheckedChanged" Text="पूर्ण अस्वीकृत 2015"
                    ValidationGroup="1" />
            </td>
        </tr>
    </table>
</asp:Content>

