<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="RejProc_Pdy2016.aspx.cs" Inherits="IssueCenter_RejProc_Pdy2016" %>

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

    <%--Code For Check and Uncheck--%>
    <script type="text/javascript">
        $(function () {
            $("#ctl00_ContentPlaceHolder1_chk_faq").click(function () {
                if ($(this).is(":checked")) {
                    $("#ctl00_ContentPlaceHolder1_txt_faq_per").removeAttr("disabled");
                    $("#ctl00_ContentPlaceHolder1_txt_faq_per").focus();
                } else {
                    $("#ctl00_ContentPlaceHolder1_txt_faq_per").attr("disabled", "disabled");
                    $("#ctl00_ContentPlaceHolder1_txt_faq_per").val(0);
                }
            });
        });

        $(function () {
            $("#ctl00_ContentPlaceHolder1_chk_extra").click(function () {
                if ($(this).is(":checked")) {
                    $("#ctl00_ContentPlaceHolder1_txt_extra_per").removeAttr("disabled");
                    $("#ctl00_ContentPlaceHolder1_txt_extra_per").focus();
                } else {
                    $("#ctl00_ContentPlaceHolder1_txt_extra_per").attr("disabled", "disabled");
                    $("#ctl00_ContentPlaceHolder1_txt_extra_per").val(0);
                }
            });
        });

        $(function () {
            $("#ctl00_ContentPlaceHolder1_chk_damaged").click(function () {
                if ($(this).is(":checked")) {
                    $("#ctl00_ContentPlaceHolder1_txt_damage_per").removeAttr("disabled");
                    $("#ctl00_ContentPlaceHolder1_txt_damage_per").focus();
                } else {
                    $("#ctl00_ContentPlaceHolder1_txt_damage_per").attr("disabled", "disabled");
                    $("#ctl00_ContentPlaceHolder1_txt_damage_per").val(0);
                }
            });
        });

        $(function () {
            $("#ctl00_ContentPlaceHolder1_chk_brightness").click(function () {
                if ($(this).is(":checked")) {
                    $("#ctl00_ContentPlaceHolder1_txt_bright_per").removeAttr("disabled");
                    $("#ctl00_ContentPlaceHolder1_txt_bright_per").focus();
                } else {
                    $("#ctl00_ContentPlaceHolder1_txt_bright_per").attr("disabled", "disabled");
                    $("#ctl00_ContentPlaceHolder1_txt_bright_per").val(0);
                }
            });
        });

        $(function () {
            $("#ctl00_ContentPlaceHolder1_chk_partially").click(function () {
                if ($(this).is(":checked")) {
                    $("#ctl00_ContentPlaceHolder1_txt_partial_per").removeAttr("disabled");
                    $("#ctl00_ContentPlaceHolder1_txt_partial_per").focus();
                } else {
                    $("#ctl00_ContentPlaceHolder1_txt_partial_per").attr("disabled", "disabled");
                    $("#ctl00_ContentPlaceHolder1_txt_partial_per").val(0);
                }
            });
        });

        $(function () {
            $("#ctl00_ContentPlaceHolder1_chk_splited").click(function () {
                if ($(this).is(":checked")) {
                    $("#ctl00_ContentPlaceHolder1_txt_split_per").removeAttr("disabled");
                    $("#ctl00_ContentPlaceHolder1_txt_split_per").focus();
                } else {
                    $("#ctl00_ContentPlaceHolder1_txt_split_per").attr("disabled", "disabled");
                    $("#ctl00_ContentPlaceHolder1_txt_split_per").val(0);
                }
            });
        });

        $(function () {
            $("#ctl00_ContentPlaceHolder1_chk_moist").click(function () {
                if ($(this).is(":checked")) {
                    $("#ctl00_ContentPlaceHolder1_txt_moist_per").removeAttr("disabled");
                    $("#ctl00_ContentPlaceHolder1_txt_moist_per").focus();
                } else {
                    $("#ctl00_ContentPlaceHolder1_txt_moist_per").attr("disabled", "disabled");
                    $("#ctl00_ContentPlaceHolder1_txt_moist_per").val(0);
                }
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

    <table align="center" style="border-style: solid; border-width: 1px; text-align: left; font-size: medium; width: 850px" border="1" cellspacing="0" cellpadding="2">
        <tr>
            <td colspan="6" style="text-align: center; font-size: large; text-decoration: underline; color: #CC6600"><strong>Reject Truck, Dispatch From Purchase Center (Procurement 2016)</strong>
                <input id="hdfCSMS_Comid" type="hidden" runat="server" />
                <input id="hdfgdntype" type="hidden" runat="server" />
            </td>
        </tr>

        <tr>
            <td rowspan="24">&nbsp;</td>
            <td colspan="4" style="text-align: center; background-color: #CCFF99">
                <asp:Label ID="Label2" runat="server" Style="text-align: center; font-weight: 700;" ForeColor="Blue" Visible="False"></asp:Label>
            </td>
            <td rowspan="24"></td>
        </tr>

        <tr>
            <td>Issue Center</td>
            <td>
                <asp:TextBox ID="txtissue" runat="server" Width="110px" ReadOnly="True" Enabled="False"></asp:TextBox>

            </td>
            <td>Crop Year</td>
            <td>
                <asp:TextBox ID="txtYear" runat="server" Width="110px" ReadOnly="True" Enabled="False">2016-2017</asp:TextBox>

            </td>
        </tr>




        <tr>
            <td>Commodity</td>
            <td>
                <asp:DropDownList ID="ddlcomdty" runat="server" Width="114px" AutoPostBack="true" OnSelectedIndexChanged="ddlcomdty_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
            <td>Sending District</td>
            <td>
                <asp:DropDownList ID="ddldistpdy" runat="server" Width="114px" AutoPostBack="True" OnSelectedIndexChanged="ddldistpdy_SelectedIndexChanged">
                </asp:DropDownList>

            </td>
        </tr>

        <tr>
            <td>Purchase Center</td>
            <td colspan="3">
                <asp:DropDownList ID="ddluparjan" runat="server" Width="613px" AutoPostBack="True" OnSelectedIndexChanged="ddluparjan_SelectedIndexChanged">
                </asp:DropDownList></td>
        </tr>


        <tr>
            <td colspan="4">
                <center>
                    <asp:Panel ID="pnlgrd" runat="server" ScrollBars="Vertical" Visible="false" Width="850px" Height="200px">

                        <asp:GridView ID="dgridchallan" runat="server" AutoGenerateColumns="False"
                            OnSelectedIndexChanged="dgridchallan_SelectedIndexChanged" PageSize="1" PagerSettings-Visible="true" ShowFooter="True" CellPadding="1"
                            ForeColor="#333333" GridLines="None" Width="830px" CssClass="ButtonClass">
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Height="15px" />
                            <Columns>
                                <asp:CommandField HeaderText="Action" ShowSelectButton="True" SelectText="Click to Receive" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle ForeColor="Red" />
                                </asp:CommandField>
                                <asp:BoundField DataField="DateOfIssue" HeaderText="Date Of Dispatch" SortExpression="DateOfIssue" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="IssueID" HeaderText="IssueID" SortExpression="IssueID">
                                    <ItemStyle HorizontalAlign="Left" ForeColor="Maroon" />
                                    <HeaderStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TruckChalanNo" HeaderText="TC No." ReadOnly="True" SortExpression="TruckChalanNo" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="TruckNo" HeaderText="Truck No." ReadOnly="True" SortExpression="TruckNo" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>

                                <asp:BoundField DataField="Bags" HeaderText="Bags" ReadOnly="True" SortExpression="Bags" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="QtyTransffer" HeaderText="Qty" ReadOnly="True" SortExpression="QtyTransffer" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="Transporter_Name" HeaderText="Transport Name" ReadOnly="True" SortExpression="Transporter_Name" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="TransporterId" HeaderText="TransporterId" ReadOnly="True" SortExpression="TransporterId" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="JutBag" HeaderText="New Jute" ReadOnly="True" SortExpression="JutBag" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="Jut_OldBag" HeaderText="Jute Old" ReadOnly="True" SortExpression="Jut_OldBag" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="HDPEBag" HeaderText="PP Bag" ReadOnly="True" SortExpression="HDPEBag" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="GodownTypeId" HeaderText="GdnType" ReadOnly="True" SortExpression="GodownTypeId" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>


                            </Columns>
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <SelectedRowStyle BackColor="#E2DED6" ForeColor="Black" Font-Bold="True" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <AlternatingRowStyle BackColor="#cccccc" ForeColor="#284775" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <EditRowStyle BackColor="#999999" />
                        </asp:GridView>


                    </asp:Panel>
                </center>
            </td>
        </tr>


        <tr>
            <td colspan="4" style="text-align: center; background-color: #99FF66; font-size: small; font-weight: 700;">Sending Details</td>
        </tr>

        <tr>
            <td>Issue_ID</td>
            <td>
                <asp:TextBox ID="txtissueId" runat="server" Width="180px" ReadOnly="True" Enabled="False" Style="text-align: right"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="txtissueId" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
            <td>Date of Dispatch</td>
            <td>
                <asp:TextBox ID="DaintyDate1P" runat="server" Width="110px" ReadOnly="True" Enabled="False" Style="text-align: right"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="DaintyDate1P" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>

        <tr>
            <td>Send Truck Challan No.</td>
            <td>
                <asp:TextBox ID="txtchlnno" runat="server" Width="110px" ReadOnly="True" Enabled="False" Style="text-align: right"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="txtchlnno" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
            <td>Sending Truck No.</td>
            <td>
                <asp:TextBox ID="txttrucknopady" runat="server" Width="110px" ReadOnly="True" Enabled="False" Style="text-align: right"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="txttrucknopady" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>

        <tr>
            <td>Transporter Name</td>
            <td colspan="3">
                <asp:DropDownList ID="ddlpdyTransporter" runat="server" Width="613px" Enabled="false">
                </asp:DropDownList></td>
        </tr>

        <tr>
            <td>NO.of Bags Dispatched</td>
            <td>
                <asp:TextBox ID="txtissubag" runat="server" Width="110px" ReadOnly="True" Enabled="False" Style="text-align: right"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="txtissubag" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
            <td>Net Qty. Dispatched<b><span class="Qtls"> (Qtls)</span></b></td>
            <td>
                <asp:TextBox ID="txtissueqty" runat="server" Width="110px" ReadOnly="True" Enabled="False" Style="text-align: right"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="txtissueqty" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>

        <tr>
            <td colspan="4" style="text-align: center; background-color: #CC9900; font-size: small; font-weight: 700;">Receipt Details
            </td>
        </tr>

        <tr>
            <td><strong>Branch</strong></td>
            <td>
                <asp:DropDownList ID="ddlbranchwlc" runat="server" Width="114px" AutoPostBack="True" OnSelectedIndexChanged="ddlbranchwlc_SelectedIndexChanged">
                </asp:DropDownList></td>
            <td><strong>Godown</strong></td>
            <td>
                <asp:DropDownList ID="ddlgodown" runat="server" Width="114px" AutoPostBack="false">
                </asp:DropDownList>
            </td>
        </tr>



        <tr>
            <td>Rejected Date</td>
            <td>
                <asp:TextBox ID="DaintyDate3" runat="server" Width="110px" ReadOnly="True" Style="text-align: right"></asp:TextBox>
                <img src="../PaddyMilling/calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_DaintyDate3' , 'expiry=true,elapse=-150,restrict=true,close=true')" /><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="DaintyDate3" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
            <td colspan="2">&nbsp;</td>
        </tr>



        <tr>
            <td>Rejected Challan No.</td>
            <td>
                <asp:TextBox ID="txtrec_tcnumber" runat="server" Width="110px" AutoComplete="off" class="Number" MaxLength="13" Style="text-align: right" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="txtrec_tcnumber" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
            <td>Rejected Truck No.</td>
            <td>
                <asp:TextBox ID="txtRec_TruckNumber" runat="server" AutoComplete="off" Class="TruckNumber" MaxLength="14" onfocus="this.select();" onmouseup="return false;" Width="110px" Style="text-align: right"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="txtRec_TruckNumber" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td colspan="4" style="text-align: center; background-color: #CC9900; font-size: small; font-weight: 700;"></td>
        </tr>


        <tr style="font-size: small">
            <td>स्कंध एफ०ए०क्यू० नहीं है?</td>
            <td>
                <asp:CheckBox ID="chk_faq" runat="server" />

            </td>
            <td>कितने प्रतिशत एफ०ए०क्यू० नहीं है?</td>
            <td>
                <asp:TextBox ID="txt_faq_per" runat="server" MaxLength="5" Enabled="False" Width="110px" Style="text-align: right" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" onfocus="this.select();" onmouseup="return false;">0</asp:TextBox>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="NAN" ControlToValidate="txt_faq_per" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,5})?))$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" ControlToValidate="txt_faq_per" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>



        <tr style="font-size: small">
            <td>बाह्य पदार्थ अधिक है?</td>
            <td>
                <asp:CheckBox ID="chk_extra" runat="server" />
            </td>
            <td>कितने प्रतिशत बाह्य पदार्थ अधिक है?</td>
            <td>
                <asp:TextBox ID="txt_extra_per" runat="server" MaxLength="5" Enabled="False" Width="110px" Style="text-align: right" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" onfocus="this.select();" onmouseup="return false;">0</asp:TextBox>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="NAN" ControlToValidate="txt_extra_per" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,5})?))$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" ControlToValidate="txt_extra_per" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>



        <tr style="font-size: small">
            <td>दाने क्षतिग्रस्त है?</td>
            <td>
                <asp:CheckBox ID="chk_damaged" runat="server" />
            </td>
            <td>कितने प्रतिशत दाने क्षतिग्रस्त है?</td>
            <td>
                <asp:TextBox ID="txt_damage_per" runat="server" MaxLength="5" Enabled="False" Width="110px" Style="text-align: right" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" onfocus="this.select();" onmouseup="return false;">0</asp:TextBox>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="NAN" ControlToValidate="txt_damage_per" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,5})?))$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" ControlToValidate="txt_damage_per" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>



        <tr style="font-size: small">
            <td>स्कंध चमक विहीन है?</td>
            <td>
                <asp:CheckBox ID="chk_brightness" runat="server" />
            </td>
            <td>कितने प्रतिशत स्कंध चमक विहीन है?</td>
            <td>
                <asp:TextBox ID="txt_bright_per" runat="server" MaxLength="5" Enabled="False" Width="110px" Style="text-align: right" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" onfocus="this.select();" onmouseup="return false;">0</asp:TextBox>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="NAN" ControlToValidate="txt_bright_per" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,5})?))$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server" ControlToValidate="txt_bright_per" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>



        <tr style="font-size: small">
            <td>आंशिक क्षतिग्रस्त है?</td>
            <td>
                <asp:CheckBox ID="chk_partially" runat="server" />
            </td>
            <td>कितने प्रतिशत आंशिक क्षतिग्रस्त है?</td>
            <td>
                <asp:TextBox ID="txt_partial_per" runat="server" MaxLength="5" Enabled="False" Width="110px" Style="text-align: right" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" onfocus="this.select();" onmouseup="return false;">0</asp:TextBox>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="NAN" ControlToValidate="txt_partial_per" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,5})?))$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator42" runat="server" ControlToValidate="txt_partial_per" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>



        <tr style="font-size: small">
            <td>टूटन व् सिकुड़े दाने है?</td>
            <td>
                <asp:CheckBox ID="chk_splited" runat="server" />
            </td>
            <td>कितने प्रतिशत टूटन व् सिकुड़े दाने है?</td>
            <td>
                <asp:TextBox ID="txt_split_per" runat="server" MaxLength="5" Enabled="False" Width="110px" Style="text-align: right" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" onfocus="this.select();" onmouseup="return false;">0</asp:TextBox>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="NAN" ControlToValidate="txt_split_per" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,5})?))$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator43" runat="server" ControlToValidate="txt_split_per" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>



        <tr style="font-size: small">
            <td>नमी का प्रतिशत अधिक है?</td>
            <td>
                <asp:CheckBox ID="chk_moist" runat="server" />
            </td>
            <td>कितने प्रतिशत नमी अधिक है?</td>
            <td>
                <asp:TextBox ID="txt_moist_per" runat="server" MaxLength="5" Enabled="False" Width="110px" Style="text-align: right" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" onfocus="this.select();" onmouseup="return false;">0</asp:TextBox>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="NAN" ControlToValidate="txt_moist_per" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,5})?))$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator44" runat="server" ControlToValidate="txt_moist_per" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>

            </td>
        </tr>



        <tr style="font-size: small">
            <td>अन्य कारण</td>
            <td colspan="3">
                <asp:TextBox ID="txtreason" runat="server" TextMode="MultiLine" Width="607px" MaxLength="90" Class="alphaOnly"></asp:TextBox>
            </td>
        </tr>



        <tr>
            <td colspan="4" style="background-color: #CCFF99;"></td>
        </tr>



        <tr>
            <td colspan="6" style="text-align: center; font-size: large;">
                <asp:Button ID="btnRecptNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnRecptNew_Click" />

                <asp:Button ID="btnRecptSubmit" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Reject" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" OnClientClick="return validate();" OnClick="btnRecptSubmit_Click" />

                <asp:Button ID="btnPrint" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Print" Width="115px" CssClass="ButtonClass" Style="margin-left: 10px" Enabled="False" CausesValidation="False" OnClick="btnPrint_Click1" />

                <asp:Button ID="btnRecptClose" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px" OnClick="btnRecptClose_Click" />
            </td>
        </tr>



        <%--Allow Only Alphabets using class="alphaNumericWithSpecial" --%>
        <script lang="javascript" src="../PaddyMilling/Scripts/TruckNo.js" type="text/javascript">      </script>
        <script lang="javascript" src="../PaddyMilling/Scripts/Number.js" type="text/javascript">       </script>
        <script lang="javascript" src="../PaddyMilling/Scripts/Alphabets.js" type="text/javascript">      </script>

    </table>
</asp:Content>

