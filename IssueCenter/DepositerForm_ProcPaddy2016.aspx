<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="DepositerForm_ProcPaddy2016.aspx.cs" Inherits="IssueCenter_DepositerForm_ProcPaddy2016" %>

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
            <td colspan="6" style="text-align: center; font-size: large; text-decoration: underline; color: #CC6600"><strong>Depositer Form (Procurement 2016)</strong>
                <input id="hdfGodownCode" type="hidden" runat="server" />
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
            <td>Issue Center</td>
            <td>
                <asp:TextBox ID="txtissue" runat="server" Width="137px" ReadOnly="True" Enabled="False"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="txtissue" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
            <td>Crop Year</td>
            <td>
                <asp:TextBox ID="txtYear" runat="server" Width="137px" ReadOnly="True" Enabled="False">2016-2017</asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="txtYear" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>

        <tr>
            <td>Date of Acceptance</td>
            <td>
                <asp:TextBox ID="DaintyDate3" runat="server" Width="137px"></asp:TextBox>
                <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_DaintyDate3' , 'expiry=true,elapse=-180,restrict=true,close=true')" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="DaintyDate3" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

            </td>
            <td>Commodity</td>
            <td>
                <asp:DropDownList ID="ddlcommodtiy" runat="server" Width="141px" OnSelectedIndexChanged="ddlcommodtiy_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList></td>
        </tr>


        <tr>
            <td>Godown Name</td>
            <td colspan="3">
                <asp:DropDownList ID="ddlgodown" runat="server" Width="270px" AutoPostBack="True" OnSelectedIndexChanged="ddlgodown_SelectedIndexChanged" Style="height: 22px">
                </asp:DropDownList>
            </td>
        </tr>



        <tr>
            <td colspan="4">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="4" BackColor="#DEBA84" BorderColor="#DEBA84" GridLines="None" 
                    OnRowDataBound="GridView2_RowDataBound" ShowFooter="True" Width="100%">
                    <Columns>

                        <asp:BoundField DataField="Society_Id" HeaderText="SocID." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" SortExpression="Society_Id">
                            <ItemStyle CssClass="griditemlaro" Width="50px" />
                            <HeaderStyle CssClass="gridlarohead" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Society" HeaderText="Purchase Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" SortExpression="Society">
                            <ItemStyle CssClass="griditemlaro" Font-Size="9pt" Width="200px" />
                            <HeaderStyle CssClass="gridlarohead" />
                        </asp:BoundField>

                        <%--<asp:BoundField DataField="TC_Number" HeaderText="TC No" 
                            SortExpression="TC_Number">
                            <ItemStyle CssClass="griditemlaro" Font-Size="8pt" Width="30px" />
                            <HeaderStyle CssClass="gridlarohead" />
                        </asp:BoundField>--%>


                        <asp:TemplateField HeaderText="TC Number" SortExpression="TC_Number" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                            <ItemTemplate>
                                <itemstyle />
                                <headerstyle />
                                <asp:Label ID="lbltc" runat="server" Text='<%# Bind("TC_Number") %>' Font-Size="Small"> </asp:Label>
                            </ItemTemplate>

                            <FooterTemplate>
                                <asp:Label ID="lbl_tc" runat="server" ForeColor="Black" Text="Total" Font-Size="Small" Font-Bold="true" Width="50px" ></asp:Label>
                            </FooterTemplate>
                            <FooterStyle HorizontalAlign="Center" />
                            <HeaderStyle Font-Size="9px" />
                        </asp:TemplateField>




                        <%--<asp:BoundField DataField="Accept_Qty" HeaderText="Accept Qty" 
                            SortExpression="Accept_Qty">
                            <ItemStyle CssClass="griditemlaro" HorizontalAlign="Center" Width="30px" />
                            <HeaderStyle CssClass="gridlarohead" />
                        </asp:BoundField>--%>


                        <asp:TemplateField HeaderText="Accept Qty" SortExpression="Accept_Qty" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="70px">
                            <ItemTemplate>
                                <itemstyle />
                                <headerstyle />
                                <asp:Label ID="lblacqt" runat="server" Text='<%# Bind("Accept_Qty") %>' Font-Size="Small" Width="50px"> </asp:Label>
                            </ItemTemplate>

                            <FooterTemplate>
                                <asp:Label ID="lbl_acqt" runat="server" ForeColor="Black" Font-Size="Small" Font-Bold="true"></asp:Label>
                            </FooterTemplate>
                            <FooterStyle HorizontalAlign="Right" />
                            <HeaderStyle Font-Size="9px" />
                        </asp:TemplateField>




                        <%--<asp:BoundField DataField="Bags" HeaderText="Accept Bags" 
                            SortExpression="Bags">
                            <ItemStyle CssClass="griditemlaro" HorizontalAlign="Center" Width="30px" />
                            <HeaderStyle CssClass="gridlarohead" />
                        </asp:BoundField>--%>


                        <asp:TemplateField HeaderText="Accept Bags" SortExpression="Bags" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="70px">
                            <ItemTemplate>
                                <itemstyle />
                                <headerstyle />
                                <asp:Label ID="lblacbag" runat="server" Text='<%# Bind("Bags") %>' Font-Size="Small" Width="50px"> </asp:Label>
                            </ItemTemplate>

                            <FooterTemplate>
                                <asp:Label ID="lbl_acbag" runat="server" ForeColor="Black" Font-Size="Small" Font-Bold="true"></asp:Label>
                            </FooterTemplate>
                            <FooterStyle HorizontalAlign="Right" />
                            <HeaderStyle Font-Size="9px" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="IssueID" HeaderText="Receipt Id" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" SortExpression="IssueID">
                            <ItemStyle CssClass="griditemlaro" HorizontalAlign="Center" Width="70px" />
                            <HeaderStyle CssClass="gridlarohead" />
                        </asp:BoundField>


                    </Columns>
                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" HorizontalAlign="Right" />
                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                    <SelectedRowStyle BackColor="#738A9C" ForeColor="#333333" />
                    <PagerStyle BackColor="#284775" ForeColor="#8C4510" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#A55129" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />

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

