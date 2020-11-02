<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="Reprint_PartialRejection.aspx.cs" Inherits="IssueCenter_Reprint_PartialRejection" Title="Partial Reprint Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table style="border: thin dotted #008080; width: 602px; height: 27px" border="1" 
        cellpadding="1" cellspacing="1">

<tr>
<td style="font-weight: 700; font-size: large; font-family: Calibri; text-decoration: underline;" 
        colspan="2">


    Reprint Acceptance Note</td>

</tr>

<tr>
<td colspan="2">


    &nbsp;</td>

</tr>

<tr>
<td style="font-weight: 700; text-align: right;">


    Crop


    Year</td>

<td style="font-weight: 700">


    Wheat-2015</td>

</tr>

<tr>
<td style="width: 297px; text-align: right; font-family: Georgia; font-size: small; background-color: #FF6600; color: #FFFFCC;">
    Issue Id Number:</td>

<td>
    <asp:DropDownList ID="ddl_Prej" runat="server" AutoPostBack="True" Height="21px" 
                                    onselectedindexchanged="ddl_Prej_SelectedIndexChanged" Width="300px">
    </asp:DropDownList>
                            </td>
</tr>

<tr>
<td colspan="2">
    &nbsp;</td>

</tr>

    <tr>
        <td style="height: 21px" colspan="2">
            <asp:LinkButton ID="hlinkpdo" runat="server" Font-Size="10pt" NavigateUrl="#" 
                Visible="False" style="font-size: medium" >Print Acceptance Note No.</asp:LinkButton>
        </td>
    </tr>

</table>
</asp:Content>

