<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="PDS_SubMovementOrder.aspx.cs" Inherits="State_PDS_SubMovementOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" lang="javascript" src="Scripts/jquery-2.1.1.js"></script>

    <%--Allow Only 2 Digit After Decimal--%>
    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/Number2D.js"></script>

    <style type="text/css">
        .ButtonClass {
            cursor: pointer;
        }
    </style>

    <table align="center" style="width: 625px; border-style: solid; font-size: 15px; border-width: 1px; text-align: left;" border="1" cellspacing="0" cellpadding="4">
        <tr>
            <td colspan="6" style="text-align: center; font-size: large; color: #CC6600"><span style="text-decoration: underline; font-weight: 700;">PDS Sub Movement Order</span> (By Rack)

                <input id="hdfDistMode" type="hidden" runat="server"/>
                <input id="hdfFooterQtyTotal" type="hidden" runat="server"/>
                <input id="hdfReachDate" type="hidden" runat="server"/>
                <input id="hdfHideFrmDist" type="hidden" runat="server"/>
                <input id="hdfComdty" type="hidden" runat="server"/>
                <input id="hdfMOCreatedDate" type="hidden" runat="server"/>
                <input id="hdfToDist" type="hidden" runat="server"/>
                <input id="hdfSMO" type="hidden" runat="server"/>
                <input id="hdfFrmDist" type="hidden" runat="server"/>
                <input id="hdfTotalQuantity" type="hidden" runat="server"/>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>MO Number</td>
            <td>
                <asp:DropDownList ID="ddlMvmtNo" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlMvmtNo_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>To District</td>
            <td>
                <asp:DropDownList ID="ddlToDist" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlToDist_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>MO Date</td>
            <td>

                <asp:TextBox ID="txtMODate" runat="server" Enabled="False" Width="146px"></asp:TextBox>

            </td>
            <td>स्कंध का वितरण</td>
            <td>
                <asp:TextBox ID="txtDispDist" runat="server" Enabled="False" Width="146px" Height="26px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtDispDist" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>Crop Year</td>
            <td>
                <asp:TextBox ID="txtCropYear" runat="server" Enabled="False" Width="146px"></asp:TextBox>
            </td>
            <td>Commodity</td>
            <td>

                <asp:TextBox ID="txtComdty" runat="server" Enabled="False" Width="146px"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtComdty" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>From District</td>
            <td>
                <asp:TextBox ID="txtFrmDist" runat="server" Enabled="False" Width="146px"></asp:TextBox>
            </td>
            <td>To District</td>
            <td>
                <asp:TextBox ID="txtToDist" runat="server" Enabled="False" Width="146px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtToDist" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>Total MO Qty<strong><asp:Label ID="lblMT" runat="server" style="font-weight: 700; color: #FF0000;" Visible="False"></asp:Label></strong></td>
            <td>
                <asp:TextBox ID="txtTotalQty" runat="server" Enabled="False" Width="146px"></asp:TextBox>
            </td>
            <td>Rem Dist Qty<strong><asp:Label ID="lblMT1" runat="server" style="font-weight: 700; color: #FF0000;" Visible="False"></asp:Label></strong></td>
            <td>
                <asp:TextBox ID="txtRemQty" runat="server" Enabled="False" Width="146px"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:Label ID="LblChkDist" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlToSendDist" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlToSendDist_SelectedIndexChanged">
                </asp:DropDownList></td>
            <td>Quantity <strong><asp:Label ID="lblMT2" runat="server" style="font-weight: 700; color: #FF0000;" Visible="False"></asp:Label></strong></td>
            <td>
                <asp:TextBox ID="txtAddQty" runat="server" Width="90px" Style="text-align: right" MaxLength="10" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="NAN" ControlToValidate="txtAddQty" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtAddQty" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
                <asp:Button ID="btnAdd" runat="server" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="22px" Text="Add" Width="50px" CssClass="ButtonClass" BackColor="Silver" ForeColor="Red" Style="margin-left: 2px" OnClick="btnAdd_Click" />


            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td colspan="4">
                <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False" ShowFooter="True" CellPadding="4" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                    OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField ReadOnly="true" HeaderText="S. No." ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="fromdisttext" HeaderText="From Dist." ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="todisttext" HeaderText="To Dist." ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="toOtherdisttext" HeaderText="Other Dist" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="quantity" HeaderText="Quantity" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
<FooterStyle HorizontalAlign="Right"></FooterStyle>

<HeaderStyle HorizontalAlign="Right"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Remove" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="iddelete" runat="server" CausesValidation="false" CommandName="Delete" OnClientClick="return confirm('Are You Sure You Want To Remove?');" Text="Remove"></asp:LinkButton>
                            </ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                </asp:GridView>
            </td>
            <td>&nbsp;</td>
        </tr>

        <%--<tr  style="visibility:hidden">--%>


        <tr>
            <td colspan="6" style="text-align: center; font-size: large;">
                <asp:Button ID="btnRecptNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnRecptNew_Click" />

                <asp:Button ID="btnRecptSubmit" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Submit" Width="115px" CssClass="ButtonClass" Enabled="False" Style="margin-left: 10px" OnClick="btnRecptSubmit_Click" CausesValidation="False" />

                <asp:Button ID="btnPrint" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Print" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" Enabled="False" CausesValidation="False" OnClick="btnPrint_Click" />

                <asp:Button ID="btnRecptClose" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px" OnClick="btnRecptClose_Click" />
            </td>
        </tr>
    </table>
</asp:Content>

