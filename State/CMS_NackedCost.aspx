<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="CMS_NackedCost.aspx.cs" Inherits="State_CMS_NackedCost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="critical_js_files" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            border-bottom-style: groove;
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
            border-bottom-style: groove;
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
            border-bottom-style: groove;
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

        .hover_row
        {
            /*// background-color: #A1DCF2;*/
            background: linear-gradient(silver, #fff);
        }

        .GridHeader
        {
            text-align:center;
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
            $("#<%= txtFrmDate.ClientID %> ").datepicker(
              {
                  dateFormat: 'dd-mm-yy'
              }
            );
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("#<%= txtToDate.ClientID %> ").datepicker(
              {
                  dateFormat: 'dd-mm-yy'
              }
            );
          });
    </script>

    <script type="text/javascript">
        $(function () {
            $("[id*=GridView1] td").hover(function () {
                $("td", $(this).closest("tr")).addClass("hover_row");
            }, function () {
                $("td", $(this).closest("tr")).removeClass("hover_row");
            });
        });
    </script>

    <center>
        <table style="width: 1100px; font-size: 12px;">
            <tr>
                <td style="text-align: left; width: 200px">
                    <a href="../State/ChannaMassorSarson_Home.aspx" class="sign">&#9754 Back
                    </a>
                </td>
                <td style="text-align: center; width: 700px">
                    <h2 style="color: #e74c3c; font-size: 20px; font-family: 'Bellefair'; letter-spacing: 4px; text-align: center;">Nacked Cost Master</h2>
                    <input type="hidden" runat="server" id="hdfID" />
                </td>

                <td style="text-align: right; width: 200px">
                    <a href="CMS_NackedCost.aspx" class="sign">&#8635 New
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



            <tr>
                <td class="InspColumn" style="padding-left: 20px; text-align: left">Commodity
                    <br />

                    <asp:DropDownList ID="ddlcommodities" runat="server" CssClass="inspddl" OnSelectedIndexChanged="ddlcommodities_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" Style="font-size: 12px;"
                        ControlToValidate="ddlcommodities" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                <td class="InspColumn" style="padding-left: 20px; text-align: left"></td>
                <td class="InspColumn" style="padding-left: 20px; text-align: left"></td>
            </tr>

            <tr>
                <td colspan="3" style="height: 5px;"></td>
            </tr>


            <tr>
                <td class="InspColumn" style="padding-left: 20px; text-align: left">Rate (Rs.)
                    <br />

                    <asp:TextBox ID="txtRate" CssClass="insptxt" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtRate" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                <td class="InspColumn" style="padding-left: 20px; text-align: left">From Date
                    <br />

                    <asp:TextBox ID="txtFrmDate" CssClass="insptxt" runat="server" ReadOnly="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtFrmDate" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                <td class="InspColumn" style="padding-left: 20px; text-align: left">To Date
                    <br />

                    <asp:TextBox ID="txtToDate" CssClass="insptxt" runat="server" ReadOnly="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtToDate" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
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
                                    <asp:Button ID="btAdd" runat="server" Text="Submit" CssClass="bttsubother" Visible="true" Enabled="true" OnClick="btAdd_Click" />

                                </td>


                            </tr>
                        </table>
                    </center>
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
                <td colspan="3" style="height: 10px;"></td>
            </tr>
            <tr runat="server" id="trgrid" visible="true">
                <td colspan="3">
                    <center>

                        <div style="width: 100%; height: 150px; overflow: scroll">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                CellPadding="4"
                                ForeColor="#333333" GridLines="None" Width="100%" CssClass="gridheader"
                                EnableModelValidation="True" border="1">

                                <Columns>

                                    <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" HeaderStyle-CssClass="GridHeader">
                                        <HeaderStyle Font-Names="Arial" Font-Size="12px" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Rate_Rs" HeaderText="Rate(Rs.)" HeaderStyle-CssClass="GridHeader">
                                        <HeaderStyle Font-Names="Arial" Font-Size="12px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FromDate" HeaderText="From Date" HeaderStyle-CssClass="GridHeader">
                                        <HeaderStyle Font-Names="Arial" Font-Size="12px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ToDate" HeaderText="To Date" HeaderStyle-CssClass="GridHeader">
                                        <HeaderStyle Font-Size="12px" Font-Names="Arial" />
                                    </asp:BoundField>



                                </Columns>
                                <RowStyle BackColor="#ffffff" ForeColor="#000000" Font-Size="16px" HorizontalAlign="Center" />

                                <HeaderStyle BackColor="#333" ForeColor="#ffffff" Font-Size="14px" />

                            </asp:GridView>
                        </div>
                    </center>

                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 10px;"></td>
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
                <td colspan="3" style="height: 10px;"></td>
            </tr>

        </table>
    </center>
</asp:Content>

