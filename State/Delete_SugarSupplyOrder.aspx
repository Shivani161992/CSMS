<%@ Page Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="Delete_SugarSupplyOrder.aspx.cs" Inherits="State_Delete_SugarSupplyOrder" Title="Delete Sugar SO" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: center">
        <div style="text-align: left">
            <table style="width: 599px; background-color: #ccccff;">
                <tr>
                    <td colspan="4" style="height: 19px">
                        <asp:Label ID="Label3" runat="server" Font-Bold="True" Text="Delete Sugar Supply Order Entry"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="4" style="height: 19px">
                        <strong><span style="font-size: 10pt; color: #ff0033">कृपया सावधानी पूर्वक डिलीट करें!
                            डाटा डिलीट होने के बाद रीकवरी संभव नहीं हो सकेगी!</span></strong></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        <asp:Label ID="Label1" runat="server" Text="Supplier Name" Width="136px"></asp:Label></td>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlsuppliername" runat="server" AutoPostBack="True" Width="250px" OnSelectedIndexChanged="ddlsuppliername_SelectedIndexChanged">
                        </asp:DropDownList></td>
                    <td style="width: 100px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        <asp:Label ID="Label2" runat="server" Text="Order No"></asp:Label></td>
                    <td style="width: 100px">
                        <asp:DropDownList ID="ddlorderno" runat="server" AutoPostBack="True" Width="128px" OnSelectedIndexChanged="ddlorderno_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlorderno"
                            ErrorMessage="Orderno Required" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
                    <td style="width: 100px">
                    </td>
                    <td style="width: 100px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px">
                    </td>
                    <td style="width: 100px">
                    </td>
                    <td style="width: 100px">
                    </td>
                    <td style="width: 100px">
                    </td>
                </tr>
                <tr>
                    <td colspan="4" rowspan="3">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" GridLines="None" Width="669px">
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <Columns>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkDelete" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Order No" DataField="Orderno" SortExpression="Orderno" />
                                <asp:BoundField HeaderText="Order Date" DataField="Dispatch_Date" SortExpression="Dispatch_Date" />
                                <asp:BoundField HeaderText="Zone Name"  DataField="Zone" SortExpression="Zone"/>
                                <asp:BoundField HeaderText="District Name" DataField="district_name" SortExpression="district_name" />
                                <asp:BoundField HeaderText="Depot Name" DataField="DepotName" SortExpression="DepotName" />
                                <asp:BoundField HeaderText="Qty"  DataField="Qty" SortExpression="Qty"/>
                            </Columns>
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#999999" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                </tr>
                <tr>
                </tr>
                <tr>
                    <td style="width: 100px">
                        <asp:Button ID="Btndelete" runat="server" Text="Delete" Width="125px" OnClick="Btndelete_Click" ValidationGroup="1" /></td>
                    <td style="width: 100px">
                    </td>
                    <td style="width: 100px">
                        <asp:Button ID="Btnclose" runat="server" Text="Close" Width="118px" OnClick="Btnclose_Click" /></td>
                    <td style="width: 100px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ValidationGroup="1" Width="151px" />
                    </td>
                    <td style="width: 100px">
                    </td>
                    <td style="width: 100px">
                    </td>
                    <td style="width: 100px">
                    </td>
                </tr>
            </table>
        </div>
    </div>

</asp:Content>

