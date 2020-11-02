<%@ Page Title="Reprint Delivery Challan" Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="Reprint_DeliveryChallan.aspx.cs" Inherits="IssueCenter_Reprint_DeliveryChallan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table style="border: thin dotted #008080; width: 602px; height: 27px" border="1" 
        cellpadding="1" cellspacing="1">

<tr>
<td style="font-weight: 700; font-size: large; font-family: Calibri; text-decoration: underline;" 
        colspan="4">


    Reprint Delivery Challan</td>

</tr>

<tr>
<td colspan="4">


    &nbsp;</td>

</tr>

<tr>
<td>


    Allotment Month</td>

<td>


                                <asp:DropDownList ID="ddlmonth" runat="server" 
                                    
        onselectedindexchanged="ddlmonth_SelectedIndexChanged" Width="138px" 
                                    AutoPostBack="True">
                                    <asp:ListItem Value="1">January</asp:ListItem>
                                    <asp:ListItem Value="2">February</asp:ListItem>
                                    <asp:ListItem Value="3">March</asp:ListItem>
                                    <asp:ListItem Value="4">April</asp:ListItem>
                                    <asp:ListItem Value="5">May</asp:ListItem>
                                    <asp:ListItem Value="6">June</asp:ListItem>
                                    <asp:ListItem Value="7">July</asp:ListItem>
                                    <asp:ListItem Value="8">August</asp:ListItem>
                                    <asp:ListItem Value="9">September</asp:ListItem>
                                    <asp:ListItem Value="10">October</asp:ListItem>
                                    <asp:ListItem Value="11">November</asp:ListItem>
                                    <asp:ListItem Value="12">December</asp:ListItem>
                                </asp:DropDownList>


    </td>

<td>


    Allotment Year</td>

<td>


                                <asp:DropDownList ID="ddlyear" runat="server" AutoPostBack="True" 
                                    
        OnSelectedIndexChanged="ddlyear_SelectedIndexChanged" Width="133px">
                                </asp:DropDownList>


    </td>

</tr>

<tr>
<td style="width: 297px; text-align: right; font-family: Georgia; font-size: small; background-color: #FF6600; color: #FFFFCC;" 
        colspan="2">
    Select Transport Order Number:</td>

<td colspan="2">
                                <asp:DropDownList ID="ddl_TO_no" runat="server" AutoPostBack="True" 
                                    
        OnSelectedIndexChanged="ddl_do_no_SelectedIndexChanged" Width="233px">
                                </asp:DropDownList>
                            </td>
</tr>

<tr>
<td colspan="4">
    &nbsp;</td>

</tr>

<tr>
<td style="width: 297px; text-align: right; color: #000000; font-family: Georgia; font-size: small; background-color: #99CCFF;" 
        colspan="2">
    &nbsp;Delivery Challan Number:</td>

<td colspan="2">
    <asp:DropDownList ID="ddlDChallan" runat="server" Width="233px" 
        OnSelectedIndexChanged="ddlDOnum_SelectedIndexChanged" AutoPostBack="True"  ToolTip="DC No.(FPS Code)">
    </asp:DropDownList>
</td>
</tr>

<tr>
<td colspan="4" >
 

</td>

</tr>
    <tr>
        <td style="height: 21px" colspan="4">
            <asp:HiddenField ID="hd_date" runat="server" />
            <asp:LinkButton ID="hlinkpdo" runat="server" Font-Size="10pt" NavigateUrl="#" 
                Visible="False" style="font-size: medium">Print Delivery Challan</asp:LinkButton>
        </td>
    </tr>

</table>

</asp:Content>

