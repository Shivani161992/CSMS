<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="CMRRejection_Challan.aspx.cs" Inherits="PaddyMilling_CMRRejection_Challan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>

    <script type="text/javascript" lang="javascript" src="../js/calendarMvmt.js"></script>

    <%--For Calendar Controls--%>
    <link href="../PaddyMilling/calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
    <script type="text/javascript" lang="" src="../js/calendarMvmt.js"></script>


    <%--Allow Only 2 Digit After Decimal--%>
    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/Number2D.js"></script>

    <script type="text/javascript" language="javascript">
        function validate() {
            if (Page_ClientValidate())
                return confirm('क्या आपने सही जानकारी भरी हैं? यदि हाँ, तो OK पर क्लिक करे अन्यथा Cancel पर क्लिक करे|');
        }
    </script>

    <style type="text/css">
        .auto-styleNSC
        {
            width: 622px;
        }

        .InspecTable
        {
            width: 1100px;
        }

        .InspHead
        {
        }

        .InspColumn
        {
            width: 33%;
            color: #10321f;
            letter-spacing: 2px;
            font-family: Almendra;
            font-size: 13px;
            font-weight: bold;
        }

        .insptxt
        {
            width: 300px;
            height: 20px;
            letter-spacing: 2px;
            font-family: sans-serif;
            font-size: 12px;
            border-color: #03a9f4;
            border: 1px solid #03a9f4;
            border-radius: 8px;
            color: black;
            padding-left: 10px;
        }

        .insfixtext
        {
            width: 310px;
            height: 20px;
            letter-spacing: 2px;
            font-family: sans-serif;
            font-size: 12px;
            border-color: #03a9f4;
            border: 1px solid #03a9f4;
            border-radius: 8px;
            color: black;
            padding-left: 10px;
        }

        .insptxt:focus
        {
            border: none;
        }

        .inspddl
        {
            width: 310px;
            height: 24px;
            letter-spacing: 2px;
            font-family: sans-serif;
            font-size: 12px;
            border-color: #03a9f4;
            border: 1px solid #03a9f4;
            border-radius: 8px;
            padding-left: 10px;
        }

            .inspddl:focus
            {
                border-radius: 8px;
            }

        .bttsub
        {
            background: transparent;
            border: none;
            outline: none;
            color: #fff;
            background: #3498db;
            padding: 10px 20px;
            cursor: pointer;
            border-radius: 5px;
            letter-spacing: 4px;
            font-family: sans-serif;
            height: 30px;
            width: 400px;
        }

            .bttsub:enabled, button[enabled]
            {
                background: #e74c3c;
            }

        .bttsubother
        {
            background: transparent;
            border: none;
            outline: none;
            color: #fff;
            background: #00AAA0;
            padding: 10px 20px;
            cursor: pointer;
            border-radius: 5px;
            letter-spacing: 4px;
            font-family: sans-serif;
            height: 30px;
            width: 150px;
        }

            .bttsubother:enabled, button[enabled]
            {
                background: #e74c3c;
            }

        .sign
        {
            color: #062946;
            font-size: 15px;
            text-decoration: none;
            letter-spacing: 2px;
        }
    </style>

    <script>
        function allowOnlyNumber(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>




    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $("#<%= txtIssuedDate.ClientID %> ").datepicker(
              {
                  dateFormat: 'dd-mm-yy'
              }
            );
        });
    </script>
    <center>
        <table style="width: 1100px; font-size: 12px;">
            <tr>
                <td style="text-align: left; width: 200px">
                    <a href="../IssueCenter/PaddyMillingHome.aspx" class="sign">&#9754 Back
                    </a>
                </td>
                <td style="text-align: center; width: 700px">
                    <h2 style="color: #e74c3c; font-size: 20px; font-family: 'Bellefair'; letter-spacing: 4px;">CMR Rejection Challan</h2>
                    <input type="hidden" runat="server" id="hdfDist" />
                    <input type="hidden" runat="server" id="hdfInspID" />
                    <input id="hdfMillId" type="hidden" runat="server" />
                    <input id="hdfbagsType" type="hidden" runat="server" />
                </td>

                <td style="text-align: right; width: 200px">
                    <a href="CMRRejection_Challan.aspx" class="sign">&#8635 New
                    </a>
                </td>
            </tr>
        </table>
    </center>
    <center>
        <table class="InspecTable">

            <tr>
                <td colspan="3" style="height: 10px;"></td>
            </tr>

            <tr runat="server" id="trNumber" visible="false">
                <td colspan="3" style="height: 10px;">
                    <center>
                        <asp:Label ID="Label2" runat="server" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size: 20px;" ForeColor="Blue" Visible="False"></asp:Label>
                    </center>
                </td>
            </tr>

            <tr>
                <td class="InspColumn" style="padding-left: 20px; text-align: left">Crop Year
                    <br />

                    <asp:DropDownList ID="ddlCropYear" runat="server" CssClass="inspddl" OnSelectedIndexChanged="ddlCropYear_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>


                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" Style="font-size: 12px;"
                        ControlToValidate="ddlCropYear" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                <td class="InspColumn" style="padding-left: 20px; text-align: left">Stock Issued
                    <br />


                    <asp:TextBox ID="SourceArival" runat="server" CssClass="insptxt" ReadOnly="True">CMR Return To Miller From Stack</asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" Style="font-size: 12px;"
                        ControlToValidate="SourceArival" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                <td class="InspColumn" style="padding-left: 20px; text-align: left">Branch Name
                    <br />

                    <asp:DropDownList ID="ddlBranch" runat="server" CssClass="inspddl" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" Style="font-size: 12px;"
                        ControlToValidate="ddlBranch" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
            </tr>







            <tr>

                <td class="InspColumn" style="padding-left: 20px; text-align: left">Godown Name
                    <br />

                    <asp:DropDownList ID="ddlGodown" runat="server" CssClass="inspddl" AutoPostBack="True" OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" Style="font-size: 12px;"
                        ControlToValidate="ddlGodown" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                <td class="InspColumn" style="padding-left: 20px; text-align: left">Stack Number
                    <br />
                    <asp:DropDownList ID="ddlstackNumber" runat="server" CssClass="inspddl" AutoPostBack="True" OnSelectedIndexChanged="ddlStackNumber_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" Style="font-size: 12px;"
                        ControlToValidate="ddlstackNumber" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>

                <td class="InspColumn" style="padding-left: 20px; text-align: left">Rejection No.
                    <br />
                    <asp:DropDownList ID="ddlRejectionNo" runat="server" CssClass="inspddl" AutoPostBack="True" OnSelectedIndexChanged="ddlRejectionNo_SelectedIndexChanged">
                    </asp:DropDownList>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" Style="font-size: 12px;"
                        ControlToValidate="ddlRejectionNo" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>

                </td>

            </tr>

            <tr>

                <td class="InspColumn" style="padding-left: 20px; text-align: left">Miller Name

                <asp:DropDownList ID="ddlmillname" runat="server" CssClass="inspddl" AutoPostBack="True" OnSelectedIndexChanged="ddlmillname_SelectedIndexChanged">
                </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" Style="font-size: 12px;"
                        ControlToValidate="ddlmillname" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                <td class="InspColumn" style="padding-left: 20px; text-align: left">Agreement Number
                    <br />
                    <asp:DropDownList ID="ddlAgree" runat="server" CssClass="inspddl" AutoPostBack="True" OnSelectedIndexChanged="ddlAgree_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" Style="font-size: 12px;"
                        ControlToValidate="ddlAgree" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>

                <td class="InspColumn" style="padding-left: 20px; text-align: left">Lot Number
                    <br />
                    <asp:DropDownList ID="ddlLot" runat="server" CssClass="inspddl" AutoPostBack="True" OnSelectedIndexChanged="ddlLot_SelectedIndexChanged">
                    </asp:DropDownList>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" Style="font-size: 12px;"
                        ControlToValidate="ddlLot" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>

                </td>

            </tr>
            <tr>
                <td colspan="3" style="height: 5px;"></td>
            </tr>
            <tr>
                <td colspan="3" style="background-color: lightgray; height: 5px; border-radius: 32px;">
                    <center>
                        <table style="width: 700px">
                            <tr>
                                <td style="background-color: darkgrey; height: 5px; border-radius: 32px;"></td>
                            </tr>
                        </table>
                    </center>

                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 5px;"></td>
            </tr>
            <tr>

                <td class="InspColumn" style="padding-left: 20px; text-align: left">Paddy DO
                    <br />
                    <asp:TextBox ID="txtpaddyDO" CssClass="insptxt" runat="server" ReadOnly="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtpaddyDO" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                <td class="InspColumn" style="padding-left: 20px; text-align: left">CMR DO
                    <br />
                    <asp:TextBox ID="txtCMRDO" CssClass="insptxt" runat="server" ReadOnly="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtCMRDO" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>

                <td class="InspColumn" style="padding-left: 20px; text-align: left">CMR Acceptance
                    <br />
                    <asp:TextBox ID="txtCMRAccep" CssClass="insptxt" runat="server" ReadOnly="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtCMRAccep" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>

                </td>

            </tr>


            <tr>

                <td class="InspColumn" style="padding-left: 20px; text-align: left">Rejected Quantity (Qtls)
                    <br />
                    <asp:TextBox ID="txtRejQuant" CssClass="insptxt" runat="server" ReadOnly="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtRejQuant" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                <td class="InspColumn" style="padding-left: 20px; text-align: left">Rejected Bags
                    <br />
                    <asp:TextBox ID="txtRejeBags" CssClass="insptxt" runat="server" ReadOnly="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtRejeBags" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>

                <td class="InspColumn" style="padding-left: 20px; text-align: left">Rejected Bags Type
                    <br />
                    <asp:TextBox ID="txtrejecBagsType" CssClass="insptxt" runat="server" ReadOnly="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtrejecBagsType" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>

                </td>

            </tr>

            <tr>

                <td class="InspColumn" style="padding-left: 20px; text-align: left">HO Level Inspector
                    <br />
                    <asp:TextBox ID="txtInspecHo" CssClass="insptxt" runat="server" ReadOnly="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtInspecHo" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                <td class="InspColumn" style="padding-left: 20px; text-align: left">Date of Inspection
                    <br />
                    <asp:TextBox ID="txtDOR" CssClass="insptxt" runat="server" ReadOnly="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtDOR" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>

                <td class="InspColumn" style="padding-left: 20px; text-align: left">Mobile Number
                    <br />
                    <asp:TextBox ID="txtMobNum" CssClass="insptxt" runat="server" ReadOnly="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtMobNum" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>

                </td>

            </tr>
            <tr>
                <td colspan="3" style="height: 5px;"></td>
            </tr>
            <tr>
                <td colspan="3" style="background-color: lightgray; height: 5px; border-radius: 32px;">
                    <center>
                        <table style="width: 700px">
                            <tr>
                                <td style="background-color: darkgrey; height: 5px; border-radius: 32px;"></td>
                            </tr>
                        </table>
                    </center>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 5px;"></td>
            </tr>

            <tr>

                <td class="InspColumn" style="padding-left: 20px; text-align: left">Remaining Qty (Qtls)
                    <br />
                    <asp:TextBox ID="txtRemQty" CssClass="insptxt" runat="server" ReadOnly="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtRemQty" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                <td class="InspColumn" style="padding-left: 20px; text-align: left">Remaining Bags
                    <br />
                    <asp:TextBox ID="txtRemBags" CssClass="insptxt" runat="server" ReadOnly="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtRemBags" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                <td class="InspColumn" style="padding-left: 20px; text-align: left">Type of CMR
                    <br />


                    <asp:DropDownList ID="ddlCommodity" runat="server" CssClass="inspddl">
                    </asp:DropDownList>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlCommodity" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">*</asp:RequiredFieldValidator>

                </td>
            </tr>






            <tr>

                <td class="InspColumn" style="padding-left: 20px; text-align: left">Issued Qty (Qtls)
                    <br />


                    <asp:TextBox ID="txtIssuedQty" CssClass="insptxt" runat="server" MaxLength="8" AutoComplete="off" AutoPostBack="true" OnTextChanged="txtIssuedQty_TextChanged"
                        onblur="extractNumber(this,2,true);"
                        onkeyup="extractNumber(this,2,true);"
                        onkeypress="return blockNonNumbers(this, event, true, true);" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtIssuedQty" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">*</asp:RequiredFieldValidator>

                </td>

                <td class="InspColumn" style="padding-left: 20px; text-align: left">Issued Bags
                    <br />


                    <asp:TextBox ID="txtIssuedBags" CssClass="insptxt" runat="server" AutoComplete="off" class="Number" MaxLength="4" onfocus="this.select();" onmouseup="return false;" AutoPostBack="true" OnTextChanged="txtIssuedBags_TextChanged"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtIssuedBags" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">*</asp:RequiredFieldValidator>

                </td>
                <td class="InspColumn" style="padding-left: 20px; text-align: left">Issued Bags Type
                    <br />


                    <asp:TextBox ID="txtbagsType" CssClass="insptxt" runat="server" AutoComplete="off" ReadOnly="true"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtbagsType" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">*</asp:RequiredFieldValidator>

                </td>




            </tr>

            <tr>

                <td class="InspColumn" style="padding-left: 20px; text-align: left">Truck Number
                    <br />

                    <asp:TextBox ID="txtTCNo" CssClass="insptxt" runat="server" AutoComplete="off"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTCNo" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">*</asp:RequiredFieldValidator>

                </td>

                <td class="InspColumn" style="padding-left: 20px; text-align: left">Date of Issue
                    <br />


                    <asp:TextBox ID="txtIssuedDate" ReadOnly="true" CssClass="insptxt" runat="server" AutoComplete="off"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtIssuedDate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">*</asp:RequiredFieldValidator>

                </td>





            </tr>
            <tr>
                <td colspan="3" style="height: 5px;"></td>
            </tr>


            <tr>
                <td colspan="3">
                    <center>
                        <table style="width: 200px;">
                            <tr>
                                <td>
                                    <asp:Button ID="btnRecptSubmit" runat="server" Text="Submit" CssClass="bttsubother" Visible="true" Enabled="false" OnClick="btnRecptSubmit_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="bttsubother" Visible="true" Enabled="false" OnClick="btnPrint_Click" />
                                </td>


                            </tr>
                        </table>
                    </center>
                </td>
            </tr>



            <tr>
                <td colspan="3" style="height: 5px;"></td>
            </tr>













            <script lang="javascript" src="../PaddyMilling/Scripts/TruckNo.js" type="text/javascript">      </script>

            <script lang="javascript" src="../PaddyMilling/Scripts/Number.js" type="text/javascript">       </script>

        </table>
    </center>
</asp:Content>


