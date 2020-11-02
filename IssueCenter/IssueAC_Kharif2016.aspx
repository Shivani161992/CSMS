<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="IssueAC_Kharif2016.aspx.cs" Inherits="IssueCenter_IssueAC_Kharif2016" %>

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
                return confirm('क्या आपने सही जानकारी भरी हैं? यदि हाँ, तो OK पर क्लिक करे अन्यथा Cancel पर क्लिक करे तथा सही जानकारी भरें|');
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
            <td colspan="6" style="text-align: center; font-size: large; text-decoration: underline; color: #CC6600"><strong>Issue Acceptance Note Number (Procurement 2016)</strong>
                <input id="hdfGodownCode" type="hidden" runat="server" />
                <input id="hdfCSMS_Comid" type="hidden" runat="server" />
            </td>
        </tr>

        <tr>
            <td align="center" colspan="6"
                style="background-color: #cccccc; font-size: small; color: #CC0000;">
                <span style="color: #000099; font-size: small">
                    <span style="font-family: Verdana; text-align: left">स्वीकृति पत्रक बनाने के पूर्व चेक लिस्ट से प्राप्त की गयी जानकारी जांच ले , 
             स्वीकृति पत्रक बन जाने के बाद डाटा निरस्त नहीं किया जायेगा |</span>
                </span>
                <asp:Image
                    ID="Image3" runat="server" Height="18px"
                    ImageUrl="~/IssueCenter/img/blinking_new.gif" Width="39px" />
                &nbsp;</td>
        </tr>

        <tr>
            <td rowspan="7">&nbsp;</td>
            <td colspan="4" style="text-align: center; background-color: #CCFF99">
                <asp:Label ID="Label2" runat="server" Style="text-align: center; font-weight: 700; color: #FF0000;" ForeColor="Blue" Visible="False"></asp:Label>
            </td>
            <td rowspan="7"></td>
        </tr>




        <tr>
            <td>Crop Year</td>
            <td>
                <asp:TextBox ID="txtYear" runat="server" Width="137px" ReadOnly="True" Enabled="False">2016-2017</asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="txtYear" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
            <td>Date of Deposit</td>
            <td>
                <asp:TextBox ID="DaintyDate3P" runat="server" Width="137px"></asp:TextBox>
                <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_DaintyDate3P' , 'expiry=true,elapse=-180,restrict=true,close=true')" /><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="DaintyDate3P" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
        </tr>



        
        <tr>
            <td>Commodity</td>
            <td>
                <asp:DropDownList ID="ddlmarksesn" runat="server" Width="141px" AutoPostBack="true" OnSelectedIndexChanged="ddlmarksesn_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>Sending District</td>
            <td>
                <asp:DropDownList ID="ddldistproment" runat="server" Width="141px" AutoPostBack="true" OnSelectedIndexChanged="ddldistproment_SelectedIndexChanged">
                </asp:DropDownList></td>
        </tr>




        <tr>
            <td>Purchase Center</td>
            <td colspan="3">
                <asp:DropDownList ID="ddlpurchcenterP" runat="server" Width="557px" AutoPostBack="true" OnSelectedIndexChanged="ddlpurchcenterP_SelectedIndexChanged">
                </asp:DropDownList></td>
        </tr>

        <tr>
            <td>Godown Name</td>
            <td>
                <asp:DropDownList ID="ddlgodown" runat="server" Width="141px" AutoPostBack="true" OnSelectedIndexChanged="ddlgodown_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>Date of Acceptance</td>
            <td>
                <asp:TextBox ID="txtAccDate" runat="server" Width="137px" ReadOnly="True" Enabled="False"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="txtAccDate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>

        <tr>
            <td colspan="4">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="3" OnRowDataBound="GridView2_RowDataBound"
                    ShowFooter="True" HorizontalAlign="Center" RowStyle-HorizontalAlign="Center" BackColor="#DEBA84" BorderColor="#DEBA84"
                    BorderStyle="None" BorderWidth="1px" CellSpacing="2">
                    <Columns>

                        <asp:BoundField DataField="IssueID" HeaderText="IssueID" SortExpression="IssueID"></asp:BoundField>
                        <asp:BoundField DataField="TC_Number" HeaderText="TC No" SortExpression="TC_Number"></asp:BoundField>
                        <asp:BoundField DataField="Truck_Number" HeaderText="Truck No." SortExpression="Truck_Number"></asp:BoundField>

                        <asp:TemplateField HeaderText="Commodity_Name" SortExpression="Commodity_Name">
                            <ItemTemplate>
                                <itemstyle />
                                <headerstyle />
                                <asp:Label ID="lblcomm" runat="server" Text='<%# Bind("Commodity_Name") %>'> </asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lbl_comm" runat="server" ForeColor="Black" Text="Grand Total"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="RecdQty_Faq" SortExpression="RecdQty_Faq">
                            <ItemTemplate>
                                <itemstyle />
                                <headerstyle />
                                <asp:Label ID="lblrcfaq" runat="server" Font-Size="14px" Text='<%# Bind("RecdQty_Faq") %>'> </asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lbl_totfaq" runat="server" ForeColor="Black"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>


                        <%--                        <asp:TemplateField HeaderText="RecdQty_Urs" SortExpression="RecdQty_Urs">
                            <ItemTemplate>
                                <itemstyle />
                                <headerstyle />
                                <asp:Label ID="lblrcurs" runat="server" Font-Size="14px" Text='<%# Bind("RecdQty_Urs") %>'> </asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lbl_toturs" runat="server" ForeColor="Black" Font-Names="Vani"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>--%>

                        <asp:TemplateField HeaderText="RecdBags_JuteNew" SortExpression="RecdBags_JuteNew">
                            <ItemTemplate>
                                <itemstyle />
                                <headerstyle />
                                <asp:Label ID="lblrcjutnew" runat="server" Font-Size="14px" Text='<%# Bind("RecdBags_JuteNew") %>'> </asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lbl_totjutnew" runat="server" ForeColor="Black"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="RecdBags_JuteOld" SortExpression="RecdBags_JuteOld">
                            <ItemTemplate>
                                <itemstyle />
                                <headerstyle />
                                <asp:Label ID="lblrcjutold" runat="server" Font-Size="14px" Text='<%# Bind("RecdBags_JuteOld") %>'> </asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lbl_totjutold" runat="server" ForeColor="Black"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="RecdBags_PP" SortExpression="RecdBags_PP">
                            <ItemTemplate>
                                <itemstyle />
                                <headerstyle />
                                <asp:Label ID="lblrcpp" runat="server" Font-Size="14px" Text='<%# Bind("RecdBags_PP") %>'> </asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lbl_totpp" runat="server" ForeColor="Black"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>



                        <asp:BoundField DataField="CropYear" HeaderText="Crop Year" SortExpression="CropYear" ></asp:BoundField>

                    </Columns>
                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" Font-Bold="true" Font-Size="13px" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" Font-Size="12px" />
                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" Font-Size="12px" HorizontalAlign="Center" />
                </asp:GridView>
            </td>
        </tr>




        <tr>
            <td colspan="4" style="background-color: #CCFF99;"></td>
        </tr>

        <tr>
            <td colspan="6" style="text-align: center; font-size: large;">
                <asp:Button ID="btnRecptNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnRecptNew_Click" />

                <asp:Button ID="btnRecptSubmit" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Submit" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" OnClientClick="return validate();" OnClick="btnRecptSubmit_Click" />

                <asp:Button ID="btnPrint" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Print" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" Enabled="False" CausesValidation="False" OnClick="btnPrint_Click1" />

                <asp:Button ID="btnRecptClose" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px" OnClick="btnRecptClose_Click" />
            </td>
        </tr>
    </table>
</asp:Content>

