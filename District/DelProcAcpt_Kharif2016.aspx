<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="DelProcAcpt_Kharif2016.aspx.cs" Inherits="District_DelProcAcpt_Kharif2016" %>

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
                return confirm('क्या आप इस Paddy के Issue ID को Delete करना चाहते हैं? यदि हाँ, तो OK पर क्लिक करे अन्यथा Cancel पर क्लिक करे|');
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
            <td colspan="6" style="text-align: center; font-size: large; text-decoration: underline; color: #CC6600"><strong>Delete Acceptance Note (Procurement 2016)</strong>
                <input id="hdfTruckNo" type="hidden" runat="server" />
                 <input id="hdfIssueID" type="hidden" runat="server" />
                <input id="hdfWHRNo" type="hidden" runat="server" />
            </td>
        </tr>

        <tr>
            <td align="center" colspan="6"
                style="background-color: #cccccc; font-size: small; color: #CC0000;">
                <span style="color: #000099; font-size: small">
                    <span style="font-family: Verdana; text-align: left"><strong>सम्बंधित एंट्री को डिलीट करने की पूर्ण जवाबदारी जिला प्रबंधक एवं उपयोगकर्ता की रहेगी, कृपया जांच कर ही डिलीट करें|</strong></span>
                </span>
                <asp:Image
                    ID="Image3" runat="server" Height="18px"
                    ImageUrl="~/IssueCenter/img/blinking_new.gif" Width="39px" />
                &nbsp;
            </td>
        </tr>

        <tr>
            <td rowspan="7">&nbsp;</td>
            <td colspan="4" style="text-align: center; background-color: #CCFF99">
                <asp:Label ID="Label2" runat="server" Style="text-align: center; font-weight: 700; color: #FF0000;" ForeColor="Blue" Visible="False"></asp:Label>
            </td>
            <td rowspan="7"></td>
        </tr>




        <tr>
            <td>Commodity</td>
            <td>
                <asp:DropDownList ID="ddlcommodtiy" runat="server" Width="141px"  Height="24px" AutoPostBack="True" OnSelectedIndexChanged="ddlcommodtiy_SelectedIndexChanged" >
                </asp:DropDownList>

            </td>
            <td>Acceptance Date</td>
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
            <td>Acceptance Number</td>
            <td>
                <asp:DropDownList ID="ddlAcceptanceNo" runat="server" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlAcceptanceNo_SelectedIndexChanged" >
                </asp:DropDownList>

            </td>
        </tr>




        <tr>
            <td>TC Number</td>
            <td>
                <asp:DropDownList ID="ddlTCNo" runat="server" Width="141px" AutoPostBack="true" Height="24px" OnSelectedIndexChanged="ddlTCNo_SelectedIndexChanged" >
                </asp:DropDownList>

            </td>
            <td>&nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>




        <tr>
            <td colspan="4">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966"
                    BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Bold="False" Font-Size="Small" Width="100%" EnableModelValidation="True" style="text-align: center; font-size: medium;">
                    <RowStyle BackColor="White" ForeColor="#330099" />
                    <Columns>
                        <asp:BoundField DataField="IssueID" HeaderText="Issue ID"
                            SortExpression="IssueID">
                            <ItemStyle Font-Bold="False" />
                        </asp:BoundField>

                            <asp:BoundField DataField="Society" HeaderText="Purchase Centre"
                            SortExpression="Society">
                            <ItemStyle Font-Bold="False" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Truck_No" HeaderText="Truck No"
                            SortExpression="Truck_No">
                            <ItemStyle Font-Bold="False" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Bags" HeaderText="Recd Bags"
                            SortExpression="Bags">
                            <ItemStyle Font-Bold="False" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Accept_Qty" HeaderText="Rec Qty"
                            SortExpression="Accept_Qty">
                            <ItemStyle Font-Bold="False" />
                        </asp:BoundField>

                    </Columns>
                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" Font-Size="Small" ForeColor="#FFFFCC" />

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

