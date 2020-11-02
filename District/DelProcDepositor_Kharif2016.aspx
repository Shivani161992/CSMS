<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="DelProcDepositor_Kharif2016.aspx.cs" Inherits="District_DelProcDepositor_Kharif2016" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>

    <script type="text/javascript" lang="javascript" src="../js/calendarMvmt.js"></script>

    <%--For Calendar Controls--%>
    <link href="../PaddyMilling/calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
    <script type="text/javascript" lang="" src="../js/calendarMvmt.js"></script>

    <%--Allow Only 2 Digit After Decimal--%>
    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/Number2D.js"></script>

    <script type="text/javascript">
        $(function () {
            $("[id*=dgridchallan] td").hover(function () {
                $("td", $(this).closest("tr")).addClass("hover_row");
            }, function () {
                $("td", $(this).closest("tr")).removeClass("hover_row");
            });
        });
    </script>

    <script type="text/javascript" language="javascript">

        function validate() {
            if (Page_ClientValidate())
                return confirm('क्या आप इस Depositer Form को Delete करना चाहते हैं? यदि हाँ, तो OK पर क्लिक करे अन्यथा Cancel पर क्लिक करे|');
        }
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("input").focus(function () {
                $(this).css("background-color", "#cccccc");
            });
            $("input").blur(function () {
                $(this).css("background-color", "#ffffff");
            });
        });
    </script>

    <style type="text/css">
        .ButtonClass {
            cursor: pointer;
            font-size: small;
        }

        .Qtls {
            font-size: small;
            color: #FF0000;
        }

        .hover_row {
            background-color: #A1DCF2;
        }
    </style>

    <table align="center" style="border-style: solid; border-width: 1px; text-align: left; font-size: medium; width: 850px" border="1" cellspacing="0" cellpadding="3">
        <tr>
            <td colspan="6" style="text-align: center; font-size: large; text-decoration: underline; color: #CC6600"><strong>Delete Depositer Form (Procurement 2016)</strong>
                <input id="hdfWHRNo" type="hidden" runat="server" />
            </td>

        </tr>

        <tr>
            <td align="center" colspan="6"
                style="background-color: #cccccc; font-size: small; color: #CC0000;">
                <span style="color: #000099; font-size: small">
                    <span style="font-family: Verdana; text-align: left"><strong>*सम्बंधित एंट्री को डिलीट करने की पूर्ण जवाबदारी जिला प्रबंधक एवं उपयोगकर्ता की रहेगी, कृपया जांच कर ही डिलीट करें*</strong></span>
                </span>
                <asp:Image
                    ID="Image3" runat="server" Height="18px"
                    ImageUrl="~/IssueCenter/img/blinking_new.gif" Width="39px" />
                &nbsp;
            </td>
        </tr>

        <tr>
            <td rowspan="6">&nbsp;</td>
            <td colspan="4" style="text-align: center; background-color: #CCFF99">
                <asp:Label ID="Label2" runat="server" Style="text-align: center; font-weight: 700; color: #FF0000;" ForeColor="Blue" Visible="False"></asp:Label>
            </td>
            <td rowspan="6"></td>
        </tr>




        <tr>
            <td>Commodity</td>
            <td>
                <asp:DropDownList ID="ddlcommodtiy" runat="server" Width="141px" Height="24px" AutoPostBack="True" OnSelectedIndexChanged="ddlcommodtiy_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
            <td>Depositer Date</td>
            <td>
                <asp:TextBox ID="DaintyDate3" runat="server" Width="137px" Style="text-align: right"></asp:TextBox>
                <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_DaintyDate3' , 'expiry=true,elapse=-150,restrict=true,close=true')" /><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="DaintyDate3" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

            </td>
        </tr>




        <tr>
            <td>Issue Centre</td>
            <td>
                <asp:DropDownList ID="ddlIC" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlIC_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
            <td>Receiving Branch</td>
            <td>
                <asp:DropDownList ID="ddlbranchwlc" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlbranchwlc_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
        </tr>

        <tr>
            <td>Receiving Godown</td>
            <td>
                <asp:DropDownList ID="ddlgodown" runat="server" Width="141px" AutoPostBack="true" Height="24px" OnSelectedIndexChanged="ddlgodown_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
            <td>Depositer Number</td>
            <td>
                <asp:DropDownList ID="ddlAcceptanceNo" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlAcceptanceNo_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
        </tr>




        <tr>
            <td colspan="4">

                <asp:GridView ID="dgridchallan" runat="server" AutoGenerateColumns="False" OnRowDataBound="dgridchallan_RowDataBound" PageSize="15" PagerSettings-Visible="true" ShowFooter="True" CellPadding="4" ForeColor="Black" GridLines="Horizontal"
                    Width="100%" EnableModelValidation="True" BorderColor="#CCCCCC" Style="font-size: small" DataKeyNames="CommodityId,godown" BackColor="White" BorderStyle="None" BorderWidth="1px">
                    <HeaderStyle CssClass="gridheader" BackColor="#333333" Font-Bold="True" ForeColor="White" />
                    <Columns>

                        <asp:TemplateField HeaderText="Delete Depositer No." HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:CheckBox ID="chk_del" runat="server"
                                    OnCheckedChanged="chk_del_CheckedChanged" Checked="True" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Acceptance Note" DataField="Acceptance_No" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" />
                        <asp:BoundField DataField="TC_Number" HeaderText="TC No." ReadOnly="True" SortExpression="TC_Number" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                            <ItemStyle CssClass="griditemlaro" />
                            <HeaderStyle CssClass="gridlarohead" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PurchaseCenterName" HeaderText="Purchase Centre"
                            Visible="False" />
                        <asp:BoundField DataField="Truck_No" HeaderText="Truck No." ReadOnly="True" SortExpression="Truck_Number" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                            <ItemStyle CssClass="griditemlaro" />
                            <HeaderStyle CssClass="gridlarohead" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Acceptance Date" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                            <FooterTemplate>
                                Total Accepted Qty
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblChallan" runat="server"
                                    Text='<%# Eval("Acceptance_Date").ToString()%>'>
                                </asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridlarohead" />
                            <ItemStyle CssClass="griditemlaro" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Accepted Qty" SortExpression="Recd_Qty" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Center" FooterStyle-VerticalAlign="Middle">
                            <EditItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("Accept_Qty") %>'></asp:Label>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lbl_acqt" runat="server" Font-Names="Vani" Font-Size="Small"
                                    ForeColor="Black" Style="font-family: Calibri"></asp:Label>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("Accept_Qty") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="gridlarohead" />
                            <ItemStyle CssClass="griditemlaro" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="IssueID" HeaderText="IssueID" SortExpression="IssueID" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Middle">
                            <HeaderStyle CssClass="gridlarohead" />
                            <ItemStyle CssClass="griditemlaro" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Commodity_" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("CommodityId") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("CommodityId") %>'></asp:Label>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="godown" HeaderText="Godown_id" ReadOnly="True"
                            Visible="False" />
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                    <SelectedRowStyle BackColor="#CC3333" ForeColor="White" Font-Bold="True" />
                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                </asp:GridView>

            </td>
        </tr>




        <tr>
            <td colspan="4" style="background-color: #CCFF99;"></td>
        </tr>

        <tr>
            <td colspan="6" style="text-align: center; font-size: large;">
                <asp:Button ID="btnRecptNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnRecptNew_Click" />

                <asp:Button ID="btnRecptSubmit" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Delete" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" OnClientClick="return validate();" OnClick="btnRecptSubmit_Click" Enabled="False" />

                <asp:Button ID="btnRecptClose" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px" OnClick="btnRecptClose_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

