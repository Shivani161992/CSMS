<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="Insp_Master.aspx.cs" Inherits="State_Insp_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <style>
        .InspecTable
        {
            width: 600px;
        }

        .InspHead
        {
        }

        .InspColumn
        {
            width: 50%;
            color: #10321f;
            letter-spacing: 2px;
            font-family: sans-serif;
            font-size: 15px;
            font-weight: bold;
        }

        .insptxt
        {
            width: 350px;
            height: 20px;
            letter-spacing: 2px;
            font-family: sans-serif;
            font-size: 14px;
            border-color: #03a9f4;
            border: 1px solid #03a9f4;
            border-radius: 8px;
            color: black;
            padding-left: 10px;
        }

        .insfixtext
        {
            width: 350px;
            height: 20px;
            letter-spacing: 2px;
            font-family: sans-serif;
            font-size: 14px;
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
            width: 350px;
            height: 24px;
            letter-spacing: 2px;
            font-family: sans-serif;
            font-size: 14px;
            border-color: #03a9f4;
            border: 1px solid #03a9f4;
            border-radius: 8px;
            padding-left: 10px;
        }

            .inspddl:focus
            {
                border: none;
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
            height: 40px;
            width: 400px;
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
            $("#<%= txtFrmdate.ClientID %> ").datepicker(
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
    <center>
        <table style="width: 100%; font-size: 12px;">
            <tr>
                <td style="text-align: left; width: 500px">
                    <a href="../State/PaddyMillingHome.aspx" class="sign">&#9754 Back
                    </a>
                </td>
                <td style="text-align: right; width: 500px">
                    <a href="Insp_Master.aspx" class="sign">&#8635 New
                    </a>
                </td>
            </tr>
        </table>
        <table class="InspecTable">
            <tr>
                <td colspan="2" style="text-align: center;">
                    <center>
                        <h2 style="color: #e74c3c; font-size: 30px; font-family: sans-serif; letter-spacing: 4px;">HO Level Inspector Master</h2>
                    </center>
                    <input type="hidden" runat="server" id="hdfDist" />
                    <input type="hidden" runat="server" id="hdfInspID" />

                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 10px;"></td>
            </tr>

            <tr>
                <td class="InspColumn" style="padding-left: 20px;">Inspector Name
                    <br />

                    <asp:TextBox ID="txtInspectorname" CssClass="insptxt" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtInspectorname" ErrorMessage="RequiredFieldValidator">Inspector Name is required.</asp:RequiredFieldValidator>
                </td>
                <td class="InspColumn" style="padding-left: 20px;">Designation
                    <br />
                    <asp:TextBox ID="txtDesignation" CssClass="insptxt" runat="server"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtDesignation" ErrorMessage="RequiredFieldValidator">Designation is required.</asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td class="InspColumn" style="padding-left: 20px;">Mobile Number One
                    <br />

                    <asp:TextBox ID="txtMobNum" CssClass="insptxt" runat="server" MaxLength="10" onkeypress="return allowOnlyNumber(event);"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtMobNum" ErrorMessage="RequiredFieldValidator">Mobile Number is required.</asp:RequiredFieldValidator>
                </td>
                <td class="InspColumn" style="padding-left: 20px;">Special Status
                    <br />

                    <asp:TextBox ID="txtMobtwo" CssClass="insptxt" runat="server" ReadOnly="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtMobNum" ErrorMessage="RequiredFieldValidator">This field is required</asp:RequiredFieldValidator>

                </td>

            </tr>


            <tr>
                <td class="InspColumn" style="padding-left: 20px;">From Date
                    <br />

                    <asp:TextBox ID="txtFrmdate" CssClass="insptxt" runat="server" ReadOnly="true"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtFrmdate" ErrorMessage="RequiredFieldValidator">From Date is required.</asp:RequiredFieldValidator>
                </td>
                <td class="InspColumn" style="padding-left: 20px;">To Date
                    <br />
                    <asp:TextBox ID="txtToDate" CssClass="insptxt" runat="server" ReadOnly="true"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtToDate" ErrorMessage="RequiredFieldValidator">To Date is required.</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 10px;"></td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Button ID="bttsub" runat="server" Text="Submit" CssClass="bttsub" OnClick="bttsub_Click" Visible="true" />
                    <asp:Button ID="bttupdate" runat="server" Text="Update" CssClass="bttsub" OnClick="bttupdate_Click" Visible="false" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 10px;"></td>
            </tr>
            <tr id="trgrid" runat="server" visible="true">
                <td colspan="2" style="height: 100px;">
                    <div style="width: 100%; height: 200px; overflow: scroll">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                            OnSelectedIndexChanged="GridView1_SelectedIndexChanged" CellPadding="4"
                            ForeColor="#333333" GridLines="None" Width="100%" CssClass="gridheader"
                            EnableModelValidation="True" border="1">

                            <Columns>
                                <asp:CommandField HeaderText="Select" ShowSelectButton="True" />


                                <asp:BoundField DataField="Inspector_ID" HeaderText="Inspector Code">
                                    <HeaderStyle Font-Names="Arial" Font-Size="12px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Inspector_Name" HeaderText="Inspector Name" SortExpression="Inspector_Name">
                                    <HeaderStyle Font-Size="12px" Font-Names="Arial" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Inspector_desig" HeaderText="Designation">
                                    <HeaderStyle Font-Names="Arial" Font-Size="12px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MobileNum" HeaderText="Mobile Number">
                                    <HeaderStyle Font-Names="Arial" Font-Size="12px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Frmdate" HeaderText="From Date">
                                    <HeaderStyle Font-Names="Arial" Font-Size="12px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ToDate" HeaderText="To Date">
                                    <HeaderStyle Font-Names="Arial" Font-Size="12px" />
                                </asp:BoundField>



                            </Columns>
                            <RowStyle BackColor="#ffffff" ForeColor="#000000" Font-Size="16px" HorizontalAlign="Center" />

                            <HeaderStyle BackColor="#00AAA0" ForeColor="#ffffff" Font-Size="20px" />

                        </asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>

