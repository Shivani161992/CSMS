<%@ Page Language="C#" MasterPageFile="~/MasterPage/AdminLogin.master" AutoEventWireup="true" CodeFile="PasswordInfo.aspx.cs" Inherits="Admin_PasswordInfo" Title="User Information" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="hedl"> Password Change &nbsp;Information</div>
    
    <div id="rate">
    <table style="border-right: olive thin solid; border-top: olive thin solid; border-left: olive thin solid; border-bottom: olive thin solid"> 
        <tr>
            <td class="tdmarginro" style="width: 66px">
            </td>
            <td align="left" style="width: 138px">
                Login Type
            </td>
            <td class="tdmarginro" colspan="2">
                <asp:DropDownList ID="ddllogintype" runat="server" Width="182px">
                </asp:DropDownList></td>
        </tr>
    </table> 
  </div>
    <table style="width: 519px" >
        <tr>
            <td>
                <asp:Button ID="btnGet" runat="server" Text="Get Data" Width="143px" OnClick="btnGet_Click" />
                <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" Text="Close" Width="137px" /></td>
        </tr>
    <tr>
    <td>
        <asp:Label ID="lblmsg" runat="server" Font-Italic="True" ForeColor="#C00000" Width="304px"></asp:Label></td>
    </tr>
    <tr>
    <td><asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"  PagerSettings-Visible  ="True" OnPageIndexChanging="GridView1_PageIndexChanging" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" Width="618px"  >
         
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True"
                SortExpression="Name" >
                <ItemStyle CssClass="griditem" />
                <HeaderStyle CssClass="gridtotsize" />
            </asp:BoundField>
            <asp:BoundField DataField="Password" HeaderText="Password" SortExpression="Password" />
            <asp:BoundField DataField="updated_By" HeaderText="Updated By" SortExpression="updated_By" >
                <ItemStyle CssClass="griditem" />
                <HeaderStyle CssClass="gridtotsize" />
            </asp:BoundField>
            <asp:BoundField DataField="updated_date" HeaderText="Updated Date" SortExpression="updated_date" />
            <asp:BoundField DataField="HostName" HeaderText="Host Name" SortExpression="HostName" >
                <ItemStyle CssClass="griditem" />
                <HeaderStyle CssClass="gridtotsize" />
            </asp:BoundField>
            <asp:BoundField DataField="DynamicIP" HeaderText="Dynamic IP" SortExpression="DynamicIP" >
                <ItemStyle CssClass="griditem" />
                <HeaderStyle CssClass="gridtotsize" />
            </asp:BoundField>
            <asp:BoundField DataField="UserAgent" HeaderText="User Agent" SortExpression="UserAgent" />
        </Columns>
        <FooterStyle BackColor="Tan" />
        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="PaleGoldenrod" />
    
    </asp:GridView></td>
    </tr>
    </table>
    
    
    <div>
        <table border ="0" cellpadding ="0" cellspacing ="0" class="gridfooter">
        <tr>
                        <td>
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
        </div>
       
</asp:Content>

