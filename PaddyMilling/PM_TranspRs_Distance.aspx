<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="PM_TranspRs_Distance.aspx.cs" Inherits="PM_TranspRs_Distance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>

    <script type="text/javascript" lang="javascript" src="../js/calendarMvmt.js"></script>

    <script type="text/javascript" language="javascript">
        function validate() {
                return confirm('क्या आप Data Save करना चाहते हो? यदि हाँ, तो OK पर क्लिक करे अन्यथा Cancel पर क्लिक करे|');
        }
    </script>

    <%--Allow Only 2 Digit After Decimal--%>
    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/Number2D.js"></script>

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

    <table align="center" style="width: 740px; border-style: solid; border-width: 1px; text-align: left; font-size: small" border="1" cellspacing="0" cellpadding="5">
        <tr>
            <td colspan="6" style="text-align: center; font-size: large; color: #CC6600"><strong><span class="auto-style2">Paddy/Rice Transportation Lead Distance Master</span></strong>

            </td>

            <input id="hdfMappingNo" type="hidden" runat="server"/>
            <input id="hdfMillingRs" type="hidden" runat="server"/>
        </tr>

        <tr>
            <td rowspan="7">&nbsp;</td>
            <td colspan="4" style="text-align: center; background-color: #CCFF99">
                <asp:Label ID="lblmsg" runat="server" Style="text-align: center; font-weight: 700; color: #FF0000;" ForeColor="Blue" Visible="False"></asp:Label>
            </td>
            <td rowspan="7"></td>
        </tr>

        <tr>
            <td>District</td>
            <td>
                <asp:TextBox ID="txtDistManager" runat="server" Width="137px" Style="margin-left: 0px" ReadOnly="True" Enabled="False"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDistManager" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

            </td>
            <td>Crop Year</td>
            <td>

                <asp:TextBox ID="txtYear" runat="server" Width="137px" ReadOnly="True" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtYear" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td>Sector Name</td>
            <td>
                <asp:DropDownList ID="ddlSector" runat="server" Width="141px" AutoPostBack="true" OnSelectedIndexChanged="ddlSector_SelectedIndexChanged" >
                </asp:DropDownList></td>
            <td>Lead / Distance</td>
            <td>
                <asp:DropDownList ID="ddlLead" runat="server" Width="141px" Enabled="False" >
                </asp:DropDownList></td>
        </tr>



        <tr>
            <td>Milling Charges<strong><br />
                (P.QTL)</strong></td>
            <td>
                <asp:TextBox ID="txtMillCharges" runat="server" Width="137px" Style="text-align: right" MaxLength="6" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" onfocus="this.select();" onmouseup="return false;" Enabled="False"></asp:TextBox>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="NAN" ControlToValidate="txtMillCharges" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMillCharges" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
            <td>Transportation Rs.<br />
                <strong>(P.MT K.M)</strong></td>
            <td>
                <asp:TextBox ID="txtTranpRs" runat="server" Width="137px" Style="text-align: right" MaxLength="6" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" onfocus="this.select();" onmouseup="return false;" Enabled="False"></asp:TextBox>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="NAN" ControlToValidate="txtTranpRs" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTranpRs" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
                <asp:Button ID="btnAdd" runat="server" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="22px" Text="Add" Width="50px" CssClass="ButtonClass" BackColor="Silver" ForeColor="Red" Style="margin-left: 13px" OnClick="btnAdd_Click" />
            </td>
        </tr>



        <tr>
            <td colspan="4" style="text-align:center">
                <asp:Label ID="lblRs" runat="server" Text="क्या आप सिर्फ Milling Charges Update करना चाहते हो?" Visible="false" style="color: #0000FF; font-weight: 700;"></asp:Label>
                <asp:CheckBox ID="chkRs" runat="server" AutoPostBack="True" Visible="false" OnCheckedChanged="chkRs_CheckedChanged" />
                </td>
        </tr>


        <tr>

            <td colspan="4" style="text-align:center">
                <asp:Label ID="lblMaster" runat="server" Text="Already Added Lead/Distance Master Data" Visible="false" style="color: #FF0000; font-weight: 700;"></asp:Label>
                <asp:GridView ID="GridView2" Width="100%" runat="server" AutoGenerateColumns="False" CellPadding="3" EnableModelValidation="True" ForeColor="Black" GridLines="Vertical"
                    OnRowDataBound="GridView2_RowDataBound" OnRowDeleting="GridView2_RowDeleting" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    <Columns>
                        <asp:BoundField ReadOnly="true" HeaderText="S. No." />
                        <asp:BoundField DataField="SectorName" HeaderText="Sector Name" />
                        <asp:BoundField DataField="Lead_Name" HeaderText="Lead/Distance" />
                        <asp:BoundField DataField="Transp_Rs" HeaderText="Transportation Rs." HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                        </asp:BoundField>

                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:LinkButton ID="iddelete" runat="server" CausesValidation="false" CommandName="Delete" OnClientClick="return confirm('Are You Sure You Want To Delete?');" Text="Delete"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Mapping_No" HeaderText="MappingNo" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" FooterStyle-CssClass="hiddencol"></asp:BoundField>
                        <asp:BoundField DataField="Lead_ID" HeaderText="LeadID" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Right" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" FooterStyle-CssClass="hiddencol"></asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
                <br />
                <asp:Label ID="lblNewAdd" runat="server" Text="Newly Added Lead/Distance Master Data" Visible="false" style="color: #FF0000; font-weight: 700;"></asp:Label>
                <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False" CellPadding="4" EnableModelValidation="True"
                    OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px">
                    <Columns>
                        <asp:BoundField ReadOnly="true" HeaderText="S. No." />
                        <asp:BoundField DataField="SectorName" HeaderText="Sector Name" />
                        <asp:BoundField DataField="LeadName" HeaderText="Lead/Distance" />
                        <asp:BoundField DataField="TranspRs" HeaderText="Transportation Rs." HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Remove">
                            <ItemTemplate>
                                <asp:LinkButton ID="iddelete" runat="server" CausesValidation="false" CommandName="Delete" OnClientClick="return confirm('Are You Sure You Want To Remove?');" Text="Remove"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" ForeColor="#330099" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                </asp:GridView>
            </td>
        </tr>



        <tr>
            <td colspan="4" style="background-color: #CCFF99; color: #0000FF;"></td>
        </tr>

        <tr>
            <td colspan="6" style="text-align: center; font-size: large;">
                <asp:Button ID="btnRecptNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnRecptNew_Click" />

                <asp:Button ID="btnRecptSubmit" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Submit" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" OnClientClick="return validate();" Enabled="false" OnClick="btnRecptSubmit_Click1" CausesValidation="False" />

                <asp:Button ID="btnRecptClose" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px" OnClick="btnRecptClose_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>


