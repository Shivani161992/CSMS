<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="PDYDist_OtherDist_MAP.aspx.cs" Inherits="PaddyMilling_PDYDist_OtherDist_MAP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>

    <script type="text/javascript" lang="javascript" src="../js/calendarMvmt.js"></script>

    <script type="text/javascript" language="javascript">
        function validate() {
            return confirm('क्या आपने सही जानकारी भरी है? यदि हाँ, तो OK पर क्लिक करे अन्यथा Cancel पर क्लिक करे|');
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

    <input id="hdfYear" type="hidden" runat="server" />

    <table align="center" style="width: 620px; border-style: solid; border-width: 1px; text-align: left; font-size: small" border="1" cellspacing="0" cellpadding="5">
        <tr>
            <td colspan="6" style="text-align: center; font-size: large; color: #CC6600"><strong><span class="auto-style2">Paddy District To Paddy Other District</span><span style="color: blue"> (Godown Mapping)</span></strong>

            </td>

        </tr>

        <tr>
            <td rowspan="8">&nbsp;</td>
            <td colspan="4" style="text-align: center; background-color: #999999"></td>
            <td rowspan="8"></td>
        </tr>

        <tr>
            <td>Paddy District</td>
            <td>
                <asp:DropDownList ID="ddlFrmDist" runat="server" Width="141px" AutoPostBack="true" OnSelectedIndexChanged="ddlFrmDist_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
            <td>Godown District</td>
            <td>

                <asp:DropDownList ID="ddlToDist" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlToDist_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
        </tr>

        <tr>
            <td colspan="4" style="text-align: center; background-color: #FF9933; font-size: medium"></td>
        </tr>

        <tr>
            <td colspan="4">

                <asp:GridView ID="GridView2" Width="100%" runat="server" HeaderStyle-CssClass="FixedHeader" AutoGenerateColumns="False" CellPadding="4" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                    OnRowDataBound="GridView2_RowDataBound" Style="font-size: small">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>

                        <asp:BoundField ReadOnly="true" HeaderText="S.No" HeaderStyle-Width="40px" ItemStyle-Width="40px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right"></asp:BoundField>

                        <asp:BoundField DataField="DepotName" HeaderText="Issue Centre" HeaderStyle-Width="108px" ItemStyle-Width="108px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right"></asp:BoundField>

                        <asp:BoundField DataField="Godown_id" HeaderText="Godown" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right"></asp:BoundField>

                        <asp:BoundField DataField="Godown_id" HeaderText="GodownNumber" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" FooterStyle-CssClass="hiddencol"></asp:BoundField>

                        <asp:BoundField DataField="Mapping_No" HeaderText="MappingNo" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" FooterStyle-CssClass="hiddencol"></asp:BoundField>

                    </Columns>
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                </asp:GridView>
            </td>
        </tr>

        <tr>
            <td>Issue Centre</td>
            <td>
                <asp:DropDownList ID="ddlIssueCentre" runat="server" Width="141px" OnSelectedIndexChanged="ddlIssueCentre_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>Branch</td>
            <td>

                <asp:DropDownList ID="ddlBranch" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>



        <tr>
            <td>Godown</td>
            <td colspan="3">
                <asp:DropDownList ID="ddlGodown" runat="server" Width="379px" AutoPostBack="true" OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:Button ID="btnAdd" runat="server" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="22px" Text="Add" Width="60px" CssClass="ButtonClass" BackColor="Silver" ForeColor="Red" Style="margin-left: 3px" OnClick="btnAdd_Click" />


            </td>
        </tr>




        <tr>
            <td colspan="4" style="text-align: center; background-color: #CCFF99; font-size: medium">
                <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False" CellPadding="4" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                    OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField ReadOnly="true" HeaderText="S. No."></asp:BoundField>

                        <asp:BoundField DataField="ICName" HeaderText="Issue Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center"></asp:BoundField>

                        <asp:BoundField DataField="BranchName" HeaderText="Branch" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center"></asp:BoundField>

                        <asp:BoundField DataField="GodownName" HeaderText="Godown" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center"></asp:BoundField>

                        <asp:TemplateField HeaderText="Remove">
                            <ItemTemplate>
                                <asp:LinkButton ID="iddelete" runat="server" CausesValidation="false" CommandName="Delete" OnClientClick="return confirm('Are You Sure You Want To Remove?');" Text="Remove"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="YellowGreen" Font-Bold="True" ForeColor="Black" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                </asp:GridView>


            </td>
        </tr>

        <tr>
            <td colspan="4" style="text-align: center; background-color: #999999"></td>
        </tr>



        <tr>
            <td colspan="6" style="text-align: center; font-size: large;">
                <asp:Button ID="btnRecptNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnRecptNew_Click" />

                <asp:Button ID="btnSubmit" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Submit" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" OnClientClick="return validate();" Enabled="False" OnClick="btnSubmit_Click" />

                <asp:Button ID="btnRecptClose" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px" OnClick="btnRecptClose_Click" />
            </td>
        </tr>
    </table>
</asp:Content>

