<%@ Page Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="Delete_ZoneRateMaster.aspx.cs" Inherits="State_Delete_ZoneRateMaster" Title="Delete Zone Tender Rate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 526px; background-color: #ccccff;">
        <tr>
            <td colspan="3" style="height: 21px">
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Text="Delete Zone Tender Rate Entry"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="3" style="height: 21px">
                <strong><span style="font-size: 10pt; color: #ff0033">कृपया सावधानी पूर्वक डिलीट करें!
                    डाटा डिलीट होने के बाद रीकवरी संभव नहीं हो सकेगी!</span></strong></td>
        </tr>
        <tr>
            <td style="width: 103px; height: 24px;" align="left">
                <asp:Label ID="Label1" runat="server" Text="Select Supplier Name :" Width="156px" Height="9px"></asp:Label></td>
            <td align="left" colspan="2" style="height: 24px">
                <asp:DropDownList ID="ddlsuppliername" runat="server" Width="284px" AutoPostBack="True" OnSelectedIndexChanged="ddlsuppliername_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlsuppliername"
                    ErrorMessage="Supplier Name Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td style="width: 103px" align="left">
                <asp:Label ID="Label2" runat="server" Text="Select Zone :" Width="105px"></asp:Label></td>
            <td align="left" colspan="2">
                <asp:DropDownList ID="ddlzonename" runat="server" Width="142px" AutoPostBack="True" OnSelectedIndexChanged="ddlzonename_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlzonename"
                    ErrorMessage="Zone Reqired" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td style="width: 103px">
            </td>
            <td style="width: 100px">
            </td>
            <td style="width: 100px">
            </td>
        </tr>
        <tr>
            <td colspan="3" rowspan="3">
               
                <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" Width="656px" Height="92px">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkDelete" runat="server"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Zone Code"  DataField="ZoneCode" SortExpression="ZoneCode"  />
                        <asp:BoundField HeaderText="Zone Name"  DataField="Zone" SortExpression="Zone"/>
                        <asp:BoundField HeaderText="Tender Rate Per MT" DataField="Tender_Rate" SortExpression="Tender_Rate" />
                        <asp:BoundField HeaderText="Financial Year" DataField="Financial_Year" SortExpression="Financial_Year" />
                    </Columns>
                </asp:GridView>
              
            </td>
        </tr>
        <tr>
        </tr>
        <tr>
        </tr>
        <tr>
            <td style="width: 103px">
                <asp:Button ID="Btndelete" runat="server" Text="Delete" Width="120px" OnClick="Btndelete_Click" ValidationGroup="1"/></td>
            <td style="width: 100px">
            </td>
            <td style="width: 100px">
                <asp:Button ID="Btnclose" runat="server" Text="Close" Width="127px" OnClick="Btnclose_Click" /></td>
        </tr>
        <tr>
            <td style="width: 103px; height: 21px;">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ValidationGroup="1" Width="152px" />
            </td>
            <td style="width: 100px; height: 21px;">
            </td>
            <td style="width: 100px; height: 21px;">
            </td>
        </tr>
        <tr>
            <td style="width: 103px">
            </td>
            <td style="width: 100px">
            </td>
            <td style="width: 100px">
            </td>
        </tr>
        <tr>
            <td style="width: 103px">
            </td>
            <td style="width: 100px">
            </td>
            <td style="width: 100px">
            </td>
        </tr>
        <tr>
            <td style="width: 103px">
            </td>
            <td style="width: 100px">
            </td>
            <td style="width: 100px">
            </td>
        </tr>
    </table>

</asp:Content>

