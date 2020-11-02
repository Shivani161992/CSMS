<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="ExpiredFDR_Updation.aspx.cs" Inherits="State_ExpiredFDR_Updation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="critical_js_files" runat="Server">
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
    </style>


      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $("#<%= txtMaturityDate.ClientID %> ").datepicker(
              {
                  dateFormat: 'dd-mm-yy'
              }
            );
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <center>
        <table style="width: 1100px; font-size: 12px;">
            <tr>
                <td style="text-align: left; width: 200px">
                    <a href="../State/PaddyMillingHome.aspx" class="sign">&#9754 Back
                    </a>
                </td>
                <td style="text-align: center; width: 700px">
                    <center>                    <h2 style="color: #e74c3c; font-size: 20px; font-family: 'Bellefair'; letter-spacing: 4px; text-align:center;">Updating Expired FDR</h2></center>


                </td>

                <td style="text-align: right; width: 200px">
                    <a href="ExpiredFDR_Updation.aspx" class="sign">&#8635 New
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
                <td class="InspColumn" style="padding-left: 20px; text-align: left">District
                     <%--<asp:RadioButton ID="rbMPState" runat="server" AutoPostBack="True" Font-Bold="True" ForeColor="#10321f" GroupName="State" OnCheckedChanged="rbMPState_CheckedChanged" Text="M.P State" />
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     <asp:RadioButton ID="rbOtherState" runat="server" Font-Bold="True" GroupName="State" Text="Other States" AutoPostBack="True" ForeColor="#10321f" OnCheckedChanged="rbOtherState_CheckedChanged" />
                     --%>
                    <br />

                    <asp:DropDownList ID="ddlMPDist" runat="server" CssClass="inspddl" AutoPostBack="True" OnSelectedIndexChanged="ddlMPDist_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td class="InspColumn" style="padding-left: 20px; text-align: left">&nbsp;&nbsp;
                    Miller Name
                    <br />
                    <asp:DropDownList ID="ddlMillName" runat="server" Width="317px" CssClass="inspddl" AutoPostBack="true" Style="margin-left: 10px" OnSelectedIndexChanged="ddlMillName_SelectedIndexChanged">
                    </asp:DropDownList>
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
            <tr runat="server" id="trgrid" visible="false">
                <td colspan="3">

                    <div style="width: 100%; height: 150px; overflow: scroll">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                            OnSelectedIndexChanged="GridView1_SelectedIndexChanged" CellPadding="4"
                            ForeColor="#333333" GridLines="None" Width="100%" CssClass="gridheader"
                            EnableModelValidation="True" border="1">

                            <Columns>
                                <asp:CommandField HeaderText="Select" ShowSelectButton="True" />

                                <asp:BoundField DataField="ID" HeaderText="ID">
                                    <HeaderStyle Font-Names="Arial" Font-Size="12px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Bank_Name" HeaderText="Bank Name">
                                    <HeaderStyle Font-Names="Arial" Font-Size="12px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FDR_IFSC" HeaderText="FDR IFSC Code" SortExpression="Inspector_Name">
                                    <HeaderStyle Font-Size="12px" Font-Names="Arial" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FDR_Number" HeaderText="FDR Number">
                                    <HeaderStyle Font-Names="Arial" Font-Size="12px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FDR_Value" HeaderText="FDR Amount">
                                    <HeaderStyle Font-Names="Arial" Font-Size="12px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FDR_Maturity" HeaderText="FDR Maturity Date">
                                    <HeaderStyle Font-Names="Arial" Font-Size="12px" />
                                </asp:BoundField>


                            </Columns>
                            <RowStyle BackColor="#ffffff" ForeColor="#000000" Font-Size="16px" HorizontalAlign="Center" />

                            <HeaderStyle BackColor="#333" ForeColor="#ffffff" Font-Size="14px" />

                        </asp:GridView>
                    </div>

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
            <tr>
                <td class="InspColumn" style="padding-left: 20px; text-align: left">ID
                    <br />
                    <asp:TextBox ID="txtID" CssClass="insptxt" runat="server" ReadOnly="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtID" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                 <td class="InspColumn" style="padding-left: 20px; text-align: left">Bank Name
                    <br />
                    <asp:TextBox ID="txtBankName" CssClass="insptxt" runat="server" ReadOnly="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtBankName" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                 <td class="InspColumn" style="padding-left: 20px; text-align: left">FDR IFSC Code
                    <br />
                    <asp:TextBox ID="txtFDRIFSC" CssClass="insptxt" runat="server" ReadOnly="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtFDRIFSC" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
            </tr>

                 <tr>
                <td class="InspColumn" style="padding-left: 20px; text-align: left">FDR Number
                    <br />
                    <asp:TextBox ID="txtFDRNumber" CssClass="insptxt" runat="server" ReadOnly="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtFDRIFSC" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                 <td class="InspColumn" style="padding-left: 20px; text-align: left">FDR Amount
                    <br />
                    <asp:TextBox ID="txtFDRAmount" CssClass="insptxt" runat="server" ReadOnly="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtFDRAmount" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                 <td class="InspColumn" style="padding-left: 20px; text-align: left">FDR Maturity Date
                    <br />
                    <asp:TextBox ID="txtMaturityDate" CssClass="insptxt" runat="server" ReadOnly="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtMaturityDate" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
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
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="bttsubother" Visible="true" Enabled="false" OnClick="btnUpdate_Click" />
                                </td>
                               

                            </tr>
                        </table>
                    </center>
                </td>
            </tr>

        </table>
    </center>
</asp:Content>

