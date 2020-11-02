<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="MvmtPlanAgainst_PDSMovmtOrder.aspx.cs" Inherits="District_MvmtPlanAgainst_PDSMovmtOrder_HO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>
    <script type="text/javascript" src="../PaddyMilling/Scripts/jquery-ui.js"></script>

    <%--For Calendar Controls--%>
    <link href="../PaddyMilling/calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
    <script type="text/javascript" lang="" src="../PaddyMilling/calendar/calendar.js"></script>

    <%--Allow Only 2 Digit After Decimal--%>
    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/Number2D.js"></script>

    <%--Script For Help Option Only Set Background--%>
    <link href="../PaddyMilling/Scripts/jquery-ui.css" rel="stylesheet" />

    <%--Script For Help Option on Mouse Over--%>
    <script>
        $(function () {
            $(document).tooltip({
                track: true
            });
        });
    </script>



    <style type="text/css">
        .ButtonClass {
            cursor: pointer;
        }
    </style>


    <table align="center" style="width: 740px; border-style: solid; border-width: 1px; text-align: left" border="1" cellspacing="0" cellpadding="6">
        <tr  >
            <td colspan="6" style="text-align: center; font-size: large; color: #CC6600" ><span style="vertical-align:top;font-weight:bold;text-decoration:underline">Movement Plan Against HO PDS Movement Order</span><span style="vertical-align:top;font-weight:bold;"> (By Road)</span>&nbsp;&nbsp;<asp:Image ID="Image3" runat="server" Width="22px" Height="20px" CssClass="ButtonClass" ImageUrl="~/Images/HELP.png" title="इस स्क्रीन में मुख्यालय द्वारा जारी किये गए मूवमेंट आर्डर के विरुद्ध,प्राप्तकर्ता जिला कार्यालय के द्वारा किन किन गोदामों पर कितनी मात्रा प्राप्त की जाना है,यह एंट्री की जाना है|इस हेतु निम्नानुसार एंट्री करें:- 
                    (1)मुख्यालय मूवमेंट आर्डर का चयन करें|
(2)प्रदाय केन्द्र,ब्रांच के नाम एवं गोदाम का चयन करें जहाँ स्कंध प्राप्त किया जाना है|
(3)प्राप्त की जाने वाली मात्रा की एंट्री करें|
(4)Add पर क्लिक करें| अन्य प्रदाय केन्द्रों की भी एंट्री कर Save करे| " />
            </td>

        </tr>

        <tr>
            <td rowspan="9"></td>
            <td colspan="4" style="text-align: center; background-color: #CCCCCC">
                <asp:Label ID="Label2" runat="server" Style="text-align: center; color: #FF3300; font-weight: 700;" Text="Label" Visible="False"></asp:Label>
            </td>
            <td rowspan="9"></td>
        </tr>

        <tr>
            <td>MO Number</td>
            <td>
                <asp:DropDownList ID="ddlMONumber" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlMONumber_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
            <td>Commodity</td>
            <td>
                <asp:TextBox ID="txtComdty" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtComdty" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td>Crop-Year</td>
            <td>
                <asp:TextBox ID="txtCropYear" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtCropYear" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
            <td>Mode of Dispatch</td>
            <td>
                <asp:TextBox ID="txtDispatch" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtDispatch" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
        </tr>


        <tr>
            <td>Date of MO</td>
            <td>
                <asp:TextBox ID="txtDateMo" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtDateMo" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
            <td>End Date of&nbsp; Transportation</td>
            <td>
                <asp:TextBox ID="txtEndDate" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtEndDate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
        </tr>


        <tr>
            <td>From District</td>
            <td>
                <asp:TextBox ID="txtFrmDist" runat="server" Width="146px" ReadOnly="True" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtFrmDist" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
            <td>Quantity <asp:Label ID="lblMT" runat="server" Text="" style="font-weight: 700"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtQuantity" runat="server" Width="146px" ReadOnly="True" Style="text-align: right" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtQuantity" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
        </tr>


        <tr>
            <td colspan="4" style="background-color: #CCCCCC">

                <input id="hdfComdtyValue" type="hidden" runat="server" />
                 <input id="hdfDispatchModeValue" type="hidden" runat="server" />
                 <input id="hdfFrmDistValue" type="hidden" runat="server" />
                 <input id="hdfQuantityValue" type="hidden" runat="server" />
                 <input id="hdfModeofDist" type="hidden" runat="server" />
                 <input id="hdfSMOSMO" type="hidden" runat="server" />
                 <input id="hdfCommodity25" type="hidden" runat="server" />
            </td>

        </tr>


        <tr>
            <td>Issue Center</td>
            <td>
                <asp:DropDownList ID="ddlIssueCentre" runat="server" Width="150px" OnSelectedIndexChanged="ddlIssueCentre_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>Branch Name</td>
            <td>
                <asp:DropDownList ID="ddlBranch" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>


        <tr>
            <td>Godown</td>
            <td>
                <asp:DropDownList ID="ddlGodown" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>Qty To Be Deposited <asp:Label ID="lblMT0" runat="server" Text="" style="font-weight: 700"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtQty" runat="server" Width="91px" Style="text-align: right" MaxLength="10" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" onfocus="this.select();" onmouseup="return false;" Enabled="False"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="NAN" ControlToValidate="txtQty" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtQty" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
                <asp:Button ID="btnAdd" runat="server" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="22px" Text="Add" Width="50px" CssClass="ButtonClass" BackColor="Silver" ForeColor="Red" Style="margin-left: 1px" OnClick="btnAdd_Click" />

            </td>
        </tr>


        <tr style="background-color: #CCCCCC">
            <td colspan="4" align="center">
                <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False" ShowFooter="True" CellPadding="4" EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                    OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting">
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
                        <asp:BoundField DataField="BranchName" HeaderText="Branch">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="GodownName" HeaderText="Godown" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                            <FooterStyle HorizontalAlign="Right"></FooterStyle>

                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">

                            <FooterStyle HorizontalAlign="Right"></FooterStyle>

                            <HeaderStyle HorizontalAlign="Right"></HeaderStyle>

                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>

                        <asp:TemplateField HeaderText="Remove">
                            <ItemTemplate>
                                <asp:LinkButton ID="iddelete" runat="server" CausesValidation="false" CommandName="Delete" OnClientClick="return confirm('Are You Sure You Want To Remove?');" Text="Remove"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
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

                <asp:Button ID="btnRecptSubmit" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Submit" Width="115px" CssClass="ButtonClass" Enabled="False" Style="margin-left: 10px" OnClick="btnRecptSubmit_Click" CausesValidation="False" />

                <asp:Button ID="btnPrint" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Print" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" Enabled="False" CausesValidation="False" OnClick="btnPrint_Click" />

                <asp:Button ID="btnRecptClose" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px" OnClick="btnRecptClose_Click" />
            </td>
        </tr>
    </table>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

