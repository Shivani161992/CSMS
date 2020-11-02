<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="Bank_Master.aspx.cs" Inherits="IssueCenter_Bank_Master" Title="Bank Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="transCP">
<center >
<div id="transCPm"> 
<table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse; border-right: green 3px double; border-top: green 3px double; border-left: green 3px double; border-bottom: green 3px double; width: 332px;" id="">
        <tr>
                <td colspan="3" style="height: 24px; color: white; background-color: transparent; background-image: url(../Images/imgg2.jpg);" align="center">
                    <asp:Label ID="lblBankMaster" runat="server" Text="Bank Master"></asp:Label></td>
            </tr>
        </table>
<table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: green 3px double; border-top: green 3px double; border-left: green 3px double; border-bottom: green 3px double; width: 332px;" >
    <tr>
        <td colspan="2" style="height: 21px">
            <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label></td>
    </tr>
    <tr>
        <td align="left" style="width: 148px">
            <asp:Label ID="lblBankName" runat="server" Text="Bank Name" Font-Size="10pt"></asp:Label></td>
        <td align="left" style="width: 178px">
    <asp:TextBox ID="txtbankname" runat="server" Width="151px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtbankname"
                ErrorMessage="Bank Name Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td align="left" style="width: 148px" >
            <asp:Label ID="lblAddress" runat="server" Text="Address" Font-Size="10pt"></asp:Label></td>
        <td align="left" style="width: 178px" >
            <asp:TextBox ID="txtbadds" runat="server" Width="152px"></asp:TextBox>
            </td>
    </tr>
    <tr>
        <td align="right" style="width: 148px">
    <asp:Button ID="btnsubmit" runat="server" Text="Save" OnClick="btnsubmit_Click" Width="82px" ValidationGroup="1" />
            <asp:Button ID="btnupdate" runat="server" Text="Update" Width="57px" OnClick="btnUpdate_Click" Visible="False" /></td>
        <td align="left" style="width: 178px">
            <asp:Button ID="btnclose" runat="server" Text="Close" Width="65px" OnClick="btnclose_Click" /></td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                ValidationGroup="1" Width="254px" Height="1px" />
        </td>
    </tr>
</table>
</div> 
<table>
<tr>
<td>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound="GridView1_RowDataBound"
           PageSize ="3"  AllowPaging="True" PagerSettings-Visible  ="True" OnPageIndexChanging="GridView1_PageIndexChanging" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" Width="400px"  >
        <FooterStyle BackColor="Tan" />
        <Columns>
            <asp:CommandField HeaderText="Action" ShowSelectButton="True">
                <HeaderStyle Font-Size="12px" />
                <ItemStyle Font-Size="11px" />
            </asp:CommandField>
            <asp:BoundField DataField="Bank_ID" HeaderText="Bank ID" SortExpression="Bank_ID">
                <ItemStyle Font-Size="11px" />
                <HeaderStyle Font-Size="12px" />
            </asp:BoundField>
            <asp:BoundField DataField="Bank_Name" HeaderText="Bank Name" SortExpression="Bank_Name">
                <ItemStyle Font-Size="11px" />
                <HeaderStyle Font-Size="12px" />
            </asp:BoundField>
        </Columns>
        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
        <HeaderStyle BackColor="Tan" Font-Bold="True" />
        <AlternatingRowStyle BackColor="PaleGoldenrod" />
    </asp:GridView>
</td>
</tr>
</table>
<table border ="0" cellpadding ="0" cellspacing ="0" class="gridfooter">
        <tr>
                        <td style="width: 117px">
                            <div >
                                <asp:LinkButton ID="Firstbutton" Text='<img border="0" src="../images/firstpage.gif" align="absmiddle">'
                                    CommandArgument="0" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false"  />&nbsp;
                                <asp:LinkButton ID="Prevbutton" Text='<img border="0" src="../images/prevpage.gif" align="absmiddle">'
                                    CommandArgument="prev" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" />&nbsp;
                                <asp:LinkButton ID="Nextbutton" Text='<img border="0" src="../images/nextpage.gif" align="absmiddle">'
                                    CommandArgument="next" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" />&nbsp;
                                <asp:LinkButton ID="Lastbutton" Text='<img border="0" src="../images/lastpage.gif" align="absmiddle">'
                                    CommandArgument="last" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" />&nbsp;&nbsp;
                             
                            </div>
                        </td>
                    </tr>
        </table>
</center>
</div> 
</asp:Content>



