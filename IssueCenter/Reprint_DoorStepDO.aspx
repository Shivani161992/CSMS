<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="Reprint_DoorStepDO.aspx.cs" Inherits="IssueCenter_Reprint_DoorStepDO" Title="Untitled Page" %>

    

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table style="width: 602px; height: 27px">

<tr>
<td style="width: 297px">


</td>

<td>


</td>
</tr>

<tr>
<td style="width: 297px">
    Select Delivery Order Number</td>

<td>
    <asp:DropDownList ID="ddlDOnum" runat="server" Width="233px" OnSelectedIndexChanged="ddlDOnum_SelectedIndexChanged" AutoPostBack="True">
    </asp:DropDownList></td>
</tr>

<tr>
<td >
 

</td>

<td>
</td>
</tr>
    <tr>
        <td style="height: 21px">
        </td>
        <td style="height: 21px">
            <asp:LinkButton ID="hlinkpdo" runat="server" Font-Size="10pt" NavigateUrl="#" Visible="False">Print Delivery Order</asp:LinkButton></td>
    </tr>

</table>

</asp:Content>

