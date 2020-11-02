<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="PM_DO_AddGodown.aspx.cs" Inherits="PaddyMilling_PM_DO_AddGodown" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function validate() {
            if (Page_ClientValidate())
                return confirm('क्या आपने सही गोदाम की Mapping की है? यदि हाँ, तो OK पर क्लिक करे अन्यथा Cancel पर क्लिक करे|');
        }
    </script>

    <style type="text/css">
        .ButtonClass {
            cursor: pointer;
        }

        .auto-style1 {
            color: #0000FF;
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
            <td colspan="6" style="text-align: center; font-size: large; color: #CC6600"><strong><span class="auto-style2">Update Paddy Godown</span> <span class="auto-style1">(Mapping)</span></strong>

            </td>
            <input id="hdfIndex" type="hidden" runat="server" />
            <input id="hdfSelectedGodam" type="hidden" runat="server" />
            <input id="hdfGodown" type="hidden" runat="server" />
        </tr>

        <tr>
            <td rowspan="13">&nbsp;</td>
            <td colspan="4" style="text-align: center; background-color: #999999">
                <asp:Label ID="Label2" runat="server" Style="text-align: center; font-weight: 700;" ForeColor="Blue" Visible="False"></asp:Label>
            </td>
            <td rowspan="13"></td>
        </tr>

        <tr>
            <td>District</td>
            <td>
                <asp:TextBox ID="txt_FrmDist" runat="server" Width="137px" ReadOnly="True" Enabled="False"></asp:TextBox>

            </td>
            <td>Crop Year</td>
            <td>

                <asp:TextBox ID="txtYear" runat="server" Width="137px" ReadOnly="True" Enabled="False"></asp:TextBox>

            </td>
        </tr>

        <tr>
            <td>मिल का नाम</td>
            <td colspan="3">
                <asp:DropDownList ID="ddlMillName" runat="server" Width="426px" AutoPostBack="True" OnSelectedIndexChanged="ddlMillName_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>


        <tr>
            <td>अनुबंध नंबर</td>
            <td>
                <asp:DropDownList ID="ddlAgtmtNumber" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlAgtmtNumber_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
            <td>Agreement Qty<strong>(Qtls)</strong></td>
            <td>

                <asp:TextBox ID="txtAgrmtQty" runat="server" Width="137px" ReadOnly="True" Enabled="False"></asp:TextBox>

            </td>
        </tr>

        <tr>
            <td colspan="4">
                <asp:GridView ID="GridView2" Width="100%" runat="server" HeaderStyle-CssClass="FixedHeader" AutoGenerateColumns="False" CellPadding="4" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                    OnRowDataBound="GridView2_RowDataBound" Style="font-size: small" OnRowDeleting="GridView2_RowDeleting1" OnSelectedIndexChanged="GridView2_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>

                        <asp:BoundField ReadOnly="true" HeaderText="S.No" HeaderStyle-Width="40px" ItemStyle-Width="40px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right"></asp:BoundField>

                        <asp:BoundField DataField="IssueCenter" HeaderText="Issue Centre" HeaderStyle-Width="108px" ItemStyle-Width="108px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right"></asp:BoundField>


                        <asp:BoundField DataField="Godown_id" HeaderText="Godown" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right"></asp:BoundField>

                        <asp:BoundField DataField="Alloted_CommonPaddy" HeaderText="Mapped Qty(Qtls)" HeaderStyle-Width="90px" ItemStyle-Width="90px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right"></asp:BoundField>

                        <asp:BoundField DataField="Rem_CommonPaddy" HeaderText="Rem. Qty(Qtls)" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right"></asp:BoundField>

                        <asp:BoundField DataField="Godown_id" HeaderText="GodownNumber" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" FooterStyle-CssClass="hiddencol"></asp:BoundField>

                        <asp:BoundField DataField="Mapping_No" HeaderText="MappingNo" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" FooterStyle-CssClass="hiddencol"></asp:BoundField>

                        <asp:ButtonField Text="Select" CommandName="Select" HeaderText="Select" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Blue" />

                    </Columns>
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                </asp:GridView>

            </td>
        </tr>

        <tr>
            <td colspan="4" style="text-align: center; font-weight: 700;">क्या आप उसी गोदाम में Update करना चाहते हो, जिसकी आपने पहले से मैपिंग की है?<asp:CheckBox ID="chkGodam" runat="server" AutoPostBack="True" OnCheckedChanged="chkGodam_CheckedChanged" Visible="False" />
            </td>
        </tr>

        <tr>

            <td colspan="4" style="text-align: center; background-color: #99FF66; font-size: small; font-weight: 700;">क्या आपके जिले की धान किसी अन्य जिले में रखी हुई है?
                <asp:CheckBox ID="chkChange" runat="server" AutoPostBack="True" OnCheckedChanged="chkChange_CheckedChanged" Visible="False" />
            </td>
        </tr>

        <tr id="trMsg" visible="false" runat="server" style="background-color: #99FF66; text-align: center">
            <td style="font-weight: 700" colspan="4">District<asp:DropDownList ID="ddlFrmDist1" runat="server" Width="141px" AutoPostBack="True" Style="margin-left: 10px" OnSelectedIndexChanged="ddlFrmDist1_SelectedIndexChanged">
            </asp:DropDownList>

            </td>
        </tr>

        <tr>
            <td>प्रदाय केंद्र</td>
            <td>
                <asp:DropDownList ID="ddlIssueCentre" runat="server" Width="141px" OnSelectedIndexChanged="ddlIssueCentre_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>ब्रांच</td>
            <td>

                <asp:DropDownList ID="ddlBranch" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>


        <tr>
            <td>गोदाम</td>
            <td colspan="3">
                <asp:DropDownList ID="ddlGodown" runat="server" Width="426px" AutoPostBack="True" OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td>गोदाम में बची हुई मात्रा<strong> (Qtls)</strong></td>
            <td>
                <asp:TextBox ID="txtGodownRemQty" runat="server" Width="137px" ReadOnly="True" Style="text-align: right" Enabled="False"></asp:TextBox>
            </td>
            <td>Mapping के अनुसार 
                <br />
                बची हुई मात्रा <strong>(Qtls)</strong></td>
            <td>
                <asp:TextBox ID="txtDORemQty" runat="server" Width="137px" ReadOnly="True" Style="text-align: right" Enabled="False"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td colspan="4" style="text-align: center; background-color: #CCFF99; font-size: medium">Add Qty<strong> (Qtls)</strong>
                <asp:TextBox ID="txtQty" runat="server" Width="91px" Style="text-align: right; margin-left: 5px" MaxLength="10" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" onfocus="this.select();" onmouseup="return false;" Enabled="False"></asp:TextBox>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="NAN" ControlToValidate="txtQty" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtQty" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
                <asp:Button ID="btnAdd" runat="server" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="22px" Text="Add" Width="60px" CssClass="ButtonClass" BackColor="Silver" ForeColor="Red" Style="margin-left: 3px" OnClick="btnAdd_Click" />


            </td>
        </tr>

        <tr>
            <td colspan="4" style="text-align: center; background-color: #CCFF99; font-size: medium">
                <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False" CellPadding="4" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                    OnRowDataBound="GridView1_RowDataBound">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField ReadOnly="true" HeaderText="S. No.">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ICName" HeaderText="Issue Center">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="GodownName" HeaderText="Godown" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                            <FooterStyle HorizontalAlign="Center"></FooterStyle>

                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity (Qtls)" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">

                            <FooterStyle HorizontalAlign="Right"></FooterStyle>

                            <HeaderStyle HorizontalAlign="Right"></HeaderStyle>

                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>

                    </Columns>
                    <HeaderStyle BackColor="YellowGreen" Font-Bold="True" ForeColor="Black" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                </asp:GridView>


            </td>
        </tr>
        <tr>
            <td colspan="6" style="background-color: #999999; color: #0000FF;"></td>
        </tr>

        <tr>
            <td colspan="6" style="text-align: center; font-size: large;">
                <asp:Button ID="btnRecptNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnRecptNew_Click" />

                <asp:Button ID="btnRecptSubmit" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Update" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" OnClientClick="return validate();" Enabled="False" OnClick="btnRecptSubmit_Click1" />

                <asp:Button ID="btnRecptClose" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px" OnClick="btnRecptClose_Click" />
            </td>
        </tr>
    </table>
</asp:Content>

