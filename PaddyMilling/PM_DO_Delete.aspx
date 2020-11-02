<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="PM_DO_Delete.aspx.cs" Inherits="PaddyMilling_PM_DO_Delete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>

    <script type="text/javascript" lang="javascript" src="../js/calendarMvmt.js"></script>

    <script type="text/javascript" language="javascript">
        function validate() {
            return confirm('क्या आप Delete करना चाहते हो? यदि हाँ, तो OK पर क्लिक करे अन्यथा Cancel पर क्लिक करे|');
        }
    </script>

    <style type="text/css">
        .ButtonClass {
            cursor: pointer;
        }

        .auto-style2 {
            text-decoration: underline;
        }

        .hiddencol {
            display: none;
        }
    </style>

    <table align="center" style="width: 630px; border-style: solid; border-width: 1px; text-align: left; font-size: small" border="1" cellspacing="0" cellpadding="5">
        <tr>
            <td colspan="6" style="text-align: center; font-size: large; color: #CC6600"><strong><span class="auto-style2">Delete Paddy Milling Delivery Order </span></strong>

            </td>
            <input id="hdfIndex" type="hidden" runat="server" />
            <input id="hdfMillingType" type="hidden" runat="server" />
            <input id="hdfDhanLot" type="hidden" runat="server" />
            <input id="hdfRem_Alloted_CommonDhan" type="hidden" runat="server" />
        </tr>

        <tr>
            <td rowspan="6">&nbsp;</td>
            <td colspan="4" style="text-align: center; background-color: #999999">
                <asp:Label ID="Label2" runat="server" Style="text-align: center; font-weight: 700;" ForeColor="Blue" Visible="False"></asp:Label>
            </td>
            <td rowspan="6"></td>
        </tr>

        <tr>
            <td>District</td>
            <td>
                <asp:DropDownList ID="ddlFrmDist" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlFrmDist_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
            <td>Crop Year</td>
            <td>

                <asp:TextBox ID="txtYear" runat="server" Width="137px" ReadOnly="True" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtYear" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td>Mill Name</td>
            <td colspan="3">
                <asp:DropDownList ID="ddlMillName" runat="server" Width="437px" AutoPostBack="True" OnSelectedIndexChanged="ddlMillName_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
        </tr>

        <tr>
            <td>Agreement No.</td>
            <td>
                <asp:DropDownList ID="ddlAgtmtNumber" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlAgtmtNumber_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>DO Number</td>
            <td>
                <asp:DropDownList ID="ddlDONo" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlDONo_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td>DO Date</td>
            <td>

                <asp:TextBox ID="txtDODate" runat="server" Width="137px" ReadOnly="True"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDODate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
            <td>DO Last Date</td>
            <td>
                <asp:TextBox ID="txtDOLastDate" runat="server" Width="137px" ReadOnly="True"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDOLastDate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td colspan="4" style="text-align: center; background-color: #CCFF99; font-size: medium">
                <asp:GridView ID="GridView2" Width="100%" runat="server" HeaderStyle-CssClass="FixedHeader" AutoGenerateColumns="False" CellPadding="4" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                    OnRowDataBound="GridView2_RowDataBound" Style="font-size: small">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>

                        <asp:BoundField ReadOnly="true" HeaderText="S.No" HeaderStyle-Width="40px" ItemStyle-Width="40px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right"></asp:BoundField>

                        <asp:BoundField DataField="LotNo" HeaderText="Lot No" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right"></asp:BoundField>

                        <asp:BoundField DataField="Issue_Centre" HeaderText="Issue Centre" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right"></asp:BoundField>

                        <asp:BoundField DataField="Godown_Code" HeaderText="Godown" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right"></asp:BoundField>

                        <asp:BoundField DataField="Alloted_CommonDhan" HeaderText="DO Qty(Qtls)" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right"></asp:BoundField>

                        <asp:BoundField DataField="RemDOQty" HeaderText="DO Rem. Qty(Qtls)" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right"></asp:BoundField>

                        <asp:BoundField DataField="Issue_Centre" HeaderText="IssueCentre" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" FooterStyle-CssClass="hiddencol"></asp:BoundField>

                        <asp:BoundField DataField="Godown_Code" HeaderText="GodownNumber" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" FooterStyle-CssClass="hiddencol"></asp:BoundField>


                    </Columns>

                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                </asp:GridView>

            </td>
        </tr>



        <tr>
            <td colspan="6" style="text-align: center; font-size: large;">
                <asp:Button ID="btnRecptNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnRecptNew_Click" />

                <asp:Button ID="btnDelete" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Delete" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" OnClientClick="return validate();" OnClick="btnDelete_Click" Enabled="False" />

                <asp:Button ID="btnRecptClose" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px" OnClick="btnRecptClose_Click" />
            </td>
        </tr>
    </table>
</asp:Content>

