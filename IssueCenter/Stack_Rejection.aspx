<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master"
    AutoEventWireup="true" CodeFile="Stack_Rejection.aspx.cs" Inherits="IssueCenter_Stack_Rejection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            font-size: 14px;
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
            width: 310px;
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

<%--    <link href="../PaddyMilling/calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
    <script type="text/javascript" lang="" src="../PaddyMilling/calendar/calendarLot.js"></script>--%>


    <script type="text/javascript" lang="javascript" src="Scripts/jquery-2.1.1.js"></script>

    <script>
        function allowOnlyNumber(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>

    <script type="text/javascript">
        var TotalChkBx;
        var Counter;

        window.onload = function () {
            //Get total no. of CheckBoxes in side the GridView.
            TotalChkBx = parseInt('<%= this.GridView1.Rows.Count %>');

            //Get total no. of checked CheckBoxes in side the GridView.
            Counter = 0;
        }

        function HeaderClick(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl =
                document.getElementById('<%= this.GridView1.ClientID %>');
                        var TargetChildControl = "chk_select";

                        //Get all the control of the type INPUT in the base control.
                        var Inputs = TargetBaseControl.getElementsByTagName("input");

                        //Checked/Unchecked all the checkBoxes in side the GridView.
                        for (var n = 0; n < Inputs.length; ++n)
                            if (Inputs[n].type == 'checkbox' &&
                                      Inputs[n].id.indexOf(TargetChildControl, 0) >= 0)
                                Inputs[n].checked = CheckBox.checked;

                        //Reset Counter
                        Counter = CheckBox.checked ? TotalChkBx : 0;
                    }

                    function ChildClick(CheckBox, HCheckBox) {
                        //get target control.
                        var HeaderCheckBox = document.getElementById(HCheckBox);

                        //Modifiy Counter; 
                        if (CheckBox.checked && Counter < TotalChkBx)
                            Counter++;
                        else if (Counter > 0)
                            Counter--;

                        //Change state of the header CheckBox.
                        if (Counter < TotalChkBx)
                            HeaderCheckBox.checked = false;
                        else if (Counter == TotalChkBx)
                            HeaderCheckBox.checked = true;
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


     <link href="../PaddyMilling/calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
    <script type="text/javascript" lang="" src="../PaddyMilling/calendar/calendarLot.js"></script>

    <center>
        <table style="width: 1100px; font-size: 12px;">
            <tr>
                <td style="text-align: left; width: 200px">
                    <a href="../IssueCenter/PaddyMillingHome.aspx" class="sign">&#9754 Back
                    </a>
                </td>
                  <td style="text-align: center; width:700px">
                       <h2 style="color: #e74c3c; font-size: 30px; font-family: sans-serif; letter-spacing: 4px;">Stack Acceptance/Rejection</h2>
                        <input type="hidden" runat="server" id="hdfDist" />
                    <input type="hidden" runat="server" id="hdfInspID" />
                      </td>

                <td style="text-align: right; width:200px">
                    <a href="Stack_Rejection.aspx" class="sign">&#8635 New
                    </a>
                </td>
            </tr>
        </table>

        <table class="InspecTable">
           
            <tr>
                <td colspan="3" style="height: 10px;"> </td>
            </tr>

              <tr runat="server" id="trNumber" visible="false">
                <td colspan="3" style="height: 10px;">
                    <center>
                    <asp:Label ID="Label2" runat="server" Style="text-align: center; font-weight: 700; color: #FF0000; Font-Size:20px;"  ForeColor="Blue" Visible="False"></asp:Label>
                        </center>
                </td>
            </tr>


            <tr>
                <td class="InspColumn" style="padding-left: 20px;">CropYear
                    <br />

                    <asp:DropDownList ID="ddlCropyear" runat="server" CssClass="inspddl" OnSelectedIndexChanged="ddlCropyear_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>

                     <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" Style="font-size: 12px;"
                        ControlToValidate="ddlCropyear" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                <td class="InspColumn" style="padding-left: 20px;">Commodity
                    <br />
                    <asp:DropDownList ID="ddlCommodity" runat="server" CssClass="inspddl" AutoPostBack="true" OnSelectedIndexChanged="ddlCommodity_SelectedIndexChanged">
                    </asp:DropDownList>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Style="font-size: 12px;"
                        ControlToValidate="ddlCommodity" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                <td class="InspColumn" style="padding-left: 20px;">District
                    <br />
                    <asp:DropDownList ID="ddlDist" runat="server" CssClass="inspddl" OnSelectedIndexChanged="ddlDist_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Style="font-size: 12px;"
                        ControlToValidate="ddlDist" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="InspColumn" style="padding-left: 20px;">Issue Center
                    <br />

                    <asp:DropDownList ID="ddlIssueCenter" runat="server" CssClass="inspddl" AutoPostBack="true" OnSelectedIndexChanged="ddlIssueCenter_SelectedIndexChanged">
                    </asp:DropDownList>

                     <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Style="font-size: 12px;"
                        ControlToValidate="ddlIssueCenter" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                <td class="InspColumn" style="padding-left: 20px;">Godown
                    <br />
                    <asp:DropDownList ID="ddlGodown" runat="server" CssClass="inspddl" AutoPostBack="true" OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged">
                    </asp:DropDownList>

                       <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Style="font-size: 12px;"
                        ControlToValidate="ddlGodown" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                <td class="InspColumn" style="padding-left: 20px;">Stack
                    <br />
                    <asp:DropDownList ID="ddlStack" runat="server" CssClass="inspddl" AutoPostBack="true" OnSelectedIndexChanged="ddlStack_SelectedIndexChanged">
                    </asp:DropDownList>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Style="font-size: 12px;"
                        ControlToValidate="ddlStack" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr id="trgrid" runat="server" visible="false">
                <td colspan="3" style="height: 100px;">
                    <div style="width: 100%; height: 200px; overflow: scroll">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                            CellPadding="4"
                            ForeColor="#333333" GridLines="None" Width="100%" CssClass="gridheader"
                            EnableModelValidation="True" border="1"
                            OnRowCreated="GridView1_RowCreated">

                            <Columns>
                                <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_select" runat="server" Enabled="false" Checked="true" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkBxHeader"
                                            onclick="javascript:HeaderClick(this);" runat="server" Checked="true" Enabled="false" />
                                    </HeaderTemplate>
                                </asp:TemplateField>


                                <asp:BoundField DataField="Inspector_Name" HeaderText="Inspector Name">
                                    <HeaderStyle Font-Names="Arial" Font-Size="12px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Mill_Name" HeaderText="Miller Name">
                                    <HeaderStyle Font-Names="Arial" Font-Size="12px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Agreement_ID" HeaderText="Agreement">
                                    <HeaderStyle Font-Size="12px" Font-Names="Arial" />
                                </asp:BoundField>
                                <asp:BoundField DataField="LotNumber" HeaderText="Lot Number">
                                    <HeaderStyle Font-Names="Arial" Font-Size="12px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DO_No" HeaderText="Paddy DO">
                                    <HeaderStyle Font-Names="Arial" Font-Size="12px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CMR_DO" HeaderText="CMR DO">
                                    <HeaderStyle Font-Names="Arial" Font-Size="12px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Acceptance_No" HeaderText="CMR Acceptance">
                                    <HeaderStyle Font-Names="Arial" Font-Size="12px" />
                                </asp:BoundField>

                                <asp:BoundField DataField="Accept_CommonRice" HeaderText="Qty (Qtls)">
                                    <HeaderStyle Font-Names="Arial" Font-Size="12px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Bags" HeaderText="Bags">
                                    <HeaderStyle Font-Names="Arial" Font-Size="12px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BagType" HeaderText="BagType">
                                    <HeaderStyle Font-Names="Arial" Font-Size="12px" />
                                </asp:BoundField>



                            </Columns>
                            <RowStyle BackColor="#ffffff" ForeColor="#000000" Font-Size="16px" HorizontalAlign="Center" />

                            <HeaderStyle BackColor="#00AAA0" ForeColor="#ffffff" Font-Size="20px" />

                        </asp:GridView>
                    </div>
                </td>
            </tr>

            <tr>
                <td colspan="3" style="height: 10px;"></td>
            </tr>

            <tr>
                <td class="InspColumn" style="padding-left: 20px;"> HO Level Inspector Name
                    <br />

                    <asp:DropDownList ID="ddlInsp" runat="server" CssClass="inspddl" OnTextChanged="ddlInsp_TextChanged" AutoPostBack="true">
                    </asp:DropDownList>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" Style="font-size: 12px;"
                        ControlToValidate="ddlInsp" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                    
                </td>
                <td class="InspColumn" style="padding-left: 20px;">Designation
                    <br />
                    <asp:TextBox ID="txtDesig" CssClass="insptxt" runat="server" ReadOnly="true"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtDesig" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                <td class="InspColumn" style="padding-left: 20px;">Mobile Number
                    <br />
                    <asp:TextBox ID="txtMobNum" CssClass="insptxt" runat="server" ReadOnly="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtMobNum" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td class="InspColumn" style="padding-left: 20px;">Other Mobile Number
                    <br />
                    <asp:TextBox ID="txtOthMobNum" CssClass="insptxt" runat="server" MaxLength="10" onkeypress="return allowOnlyNumber(event);"></asp:TextBox>

                      <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtMobNum" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                </td>
                <td class="InspColumn" style="padding-left: 20px;">Date of Inspection
                    <br />

                   <asp:TextBox ID="txtIssuedDate" CssClass="insptxt" ReadOnly="true"  runat="server" AutoComplete="off" ></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtIssuedDate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                

                </td>
               <%-- <td class="InspColumn" style="padding-left: 20px;">Bags
                    <br />

                    <asp:TextBox ID="txtbags" CssClass="insptxt" runat="server" onkeypress="return allowOnlyNumber(event);"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" Style="font-size: 12px;"
                        ControlToValidate="txtbags" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>

                </td>--%>
            </tr>

            <tr>
                <td colspan="3" style="height: 10px;"></td>
            </tr>
            <tr runat="server" id="trparameters" visible="false">
                <td colspan="3">



                    <table align="center" border="1" style="border-spacing: 1px; text-align: center; width: 894px; font-size: small; border-radius: 8px;" id="table2">

                        <tr>

                            <td colspan="4" style="color: white; width: 1050px; background-color: #00AAA0; text-align: center; font-size: large; font-weight: bolder; letter-spacing: 2px; font-family: sans-serif;"><strong>Quality Inspection By Team (Rice)</strong></td>
                        </tr>
                        <tr>


                            <td style="width: 31px;">क्रम सं.</td>
                            <td style="width: 31px; text-align: center;">अपवर्तन</td>
                            <td style="width: 114px;">अधिकतम सीमा (प्रतिशत)<br />
                                <b>कामन</b></td>
                            <td style="width: 114px;">गुणवत्ता परिक्षण उपरांत परिणाम<br />
                                <b>कामन</b>
                            </td>
                        </tr>
                        <tr>


                            <td rowspan="2" style="width: 31px">1.</td>
                            <td style="text-align: left; height: 51px;">टोटा<br />
                            </td>
                            <td style="width: 114px; height: 51px;">
                                <asp:Label ID="LblTotaS" runat="server"></asp:Label>
                            </td>
                            <td style="width: 114px; height: 51px;">
                                <asp:TextBox ID="TxtTotaS" runat="server" Style="text-align: right" class="NumberDecimal" MaxLength="5" Width="104px" AutoComplete="off"
                                    onblur="extractNumber(this,2,true);"
                                    onkeyup="extractNumber(this,2,true);"
                                    onkeypress="return blockNonNumbers(this, event, true, true);" ValidationGroup="Common" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtTotaS" ErrorMessage="Invalid Number" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                                <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtTotaS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>

                            <td style="text-align: left; height: 51px;">छोटे टोटे</td>
                            <td style="height: 51px">
                                <asp:Label ID="LblChoteToteS" runat="server"></asp:Label>
                            </td>
                            <td style="height: 51px">
                                <asp:TextBox ID="TxtChoteToteS" runat="server" Style="text-align: right" class="NumberDecimal" MaxLength="5" Width="104px" AutoComplete="off"
                                    onblur="extractNumber(this,2,true);"
                                    onkeyup="extractNumber(this,2,true);"
                                    onkeypress="return blockNonNumbers(this, event, true, true);" ValidationGroup="Common" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TxtChoteToteS" ErrorMessage="Invalid Number" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                                <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxtChoteToteS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                            </td>
                        </tr>



                        <tr>
                            <td style="width: 31px; height: 51px;">2.</td>
                            <td style="text-align: left; height: 51px;">विजातीय तत्व **</td>
                            <td style="width: 114px; height: 51px;">
                                <asp:Label ID="LblVijatiyeS" runat="server"></asp:Label>
                            </td>
                            <td style="width: 114px; height: 51px;">
                                <asp:TextBox ID="txtVijatiyeS" runat="server" Style="text-align: right" class="NumberDecimal" MaxLength="5" Width="104px" AutoComplete="off"
                                    onblur="extractNumber(this,2,true);"
                                    onkeyup="extractNumber(this,2,true);"
                                    onkeypress="return blockNonNumbers(this, event, true, true);" ValidationGroup="Common" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtVijatiyeS" ErrorMessage="Invalid Number" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                                <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtVijatiyeS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr>
                            <td style="width: 31px">3.</td>
                            <td style="text-align: left; height: 51px;">क्षतिग्रस्त दाने</td>
                            <td style="width: 114px">
                                <asp:Label ID="LblDamageDaaneS" runat="server"></asp:Label>
                            </td>
                            <td style="width: 114px">
                                <asp:TextBox ID="txtDamageDaaneS" runat="server" Style="text-align: right" class="NumberDecimal" MaxLength="5" Width="104px" AutoComplete="off"
                                    onblur="extractNumber(this,2,true);"
                                    onkeyup="extractNumber(this,2,true);"
                                    onkeypress="return blockNonNumbers(this, event, true, true);" ValidationGroup="Common" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtDamageDaaneS" ErrorMessage="Invalid Number" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                                <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDamageDaaneS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr>
                            <td style="width: 31px; height: 51px;">4.</td>
                            <td style="text-align: left; height: 51px;">बदरंग दाने</td>
                            <td style="width: 114px; height: 51px;">
                                <asp:Label ID="LblBadrangDaaneS" runat="server"></asp:Label>
                            </td>
                            <td style="width: 114px; height: 51px;">
                                <asp:TextBox ID="txtBadrangDaaneS" runat="server" Style="text-align: right" class="NumberDecimal" MaxLength="5" Width="104px" AutoComplete="off"
                                    onblur="extractNumber(this,2,true);"
                                    onkeyup="extractNumber(this,2,true);"
                                    onkeypress="return blockNonNumbers(this, event, true, true);" ValidationGroup="Common" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtBadrangDaaneS" ErrorMessage="Invalid Number" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                                <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtBadrangDaaneS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                            </td>
                        </tr>



                        <tr>
                            <td style="width: 31px; height: 51px;">5.</td>
                            <td style="text-align: left; height: 51px;">चाकी दाने</td>
                            <td style="width: 114px; height: 51px;">
                                <asp:Label ID="LblChaakiDaaneS" runat="server"></asp:Label>
                            </td>
                            <td style="width: 114px; height: 51px;">
                                <asp:TextBox ID="txtChaakiDaaneS" runat="server" Style="text-align: right" class="NumberDecimal" MaxLength="5" Width="104px" AutoComplete="off"
                                    onblur="extractNumber(this,2,true);"
                                    onkeyup="extractNumber(this,2,true);"
                                    onkeypress="return blockNonNumbers(this, event, true, true);" ValidationGroup="Common" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="txtChaakiDaaneS" ErrorMessage="Invalid Number" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                                <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtChaakiDaaneS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr>
                            <td style="width: 31px; height: 51px;">6.</td>
                            <td style="text-align: left; height: 51px;">लाल दाने</td>
                            <td style="width: 114px; height: 51px;">
                                <asp:Label ID="LblLaalDaaneS" runat="server"></asp:Label>
                            </td>
                            <td style="width: 114px; height: 51px;">
                                <asp:TextBox ID="txtLaalDaaneS" runat="server" Style="text-align: right" class="NumberDecimal" MaxLength="5" Width="104px" AutoComplete="off"
                                    onblur="extractNumber(this,2,true);"
                                    onkeyup="extractNumber(this,2,true);"
                                    onkeypress="return blockNonNumbers(this, event, true, true);" ValidationGroup="Common" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ControlToValidate="txtLaalDaaneS" ErrorMessage="Invalid Number" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                                <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtLaalDaaneS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                            </td>
                        </tr>


                        <tr>

                            <td style="width: 31px; height: 51px;">7.</td>
                            <td style="text-align: left; height: 51px;">नमी तत्व (R)</td>
                            <td style="width: 114px; height: 51px;">
                                <asp:Label ID="LblNamiS" runat="server"></asp:Label>
                            </td>
                            <td style="width: 114px; height: 51px;">
                                <asp:TextBox ID="txtNamiS" runat="server" AutoComplete="off" class="NumberDecimal" MaxLength="5" onblur="extractNumber(this,2,true);" onfocus="this.select();" onkeypress="return blockNonNumbers(this, event, true, true);" onkeyup="extractNumber(this,2,true);" onmouseup="return false;" Style="text-align: right" ValidationGroup="Common" Width="104px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator20" runat="server" ControlToValidate="txtNamiS" Display="Dynamic" ErrorMessage="Invalid Number" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
                                <b>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtNamiS" Display="Dynamic" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
                                </b></td>
                        </tr>
                        <tr>
                            <td colspan="4" style="height: 40px;">

                                <%-- <asp:Button ID="btnQuilityTested" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Submit For Quility Inspection (Paddy)" Width="254px" CssClass="ButtonClass"  OnClientClick="return validate1();" Enabled="False" />--%>
                                <asp:Button ID="btnQuilityTested" runat="server" Text="Quality Inspection" CssClass="bttsub" Visible="true" OnClick="btnQuilityTested_Click" Enabled="false" />

                            </td>
                        </tr>


                    </table>


                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 10px;"></td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: center;">
                    <center>
                        <table style="width: 500px;">
                            <tr>
                                <td>
                                    <asp:Button ID="bttaccept" runat="server" Text="Accept" CssClass="bttsubother" Visible="true" Enabled="false" OnClick="bttaccept_Click" />
                                </td>
                                 <td style="width:300px;">
                                   

                                </td>
                                <td>
                                    <asp:Button ID="bttreject" runat="server" Text="Reject" CssClass="bttsubother" Visible="true" Enabled="false" OnClick="bttreject_Click" />
                                </td>
                                
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
