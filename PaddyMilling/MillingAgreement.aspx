<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="MillingAgreement.aspx.cs" Inherits="PaddyMilling_MillingAgreement" MaintainScrollPositionOnPostback="true" %>

<%@ MasterType VirtualPath="~/MasterPage/SCSC_master.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        .ButtonClass {
            cursor: pointer;
        }

        .auto-style1 {
            width: 140px;
        }

        .auto-style2 {
        }
    </style>

    <script type="text/javascript" language="javascript">
        function validate() {
            if (Page_ClientValidate())
                return confirm('Are You Sure To Submit Paddy Milling Agreement?');
        }
    </script>

    <script type="text/javascript" lang="javascript" src="Scripts/jquery-2.1.1.js"></script>
    <script type="text/javascript" lang="javascript" src="Scripts/jquery-ui.js"></script>

    <%--Allow Only Alphabets using class="alphaOnly" --%>
    <script type="text/javascript" lang="javascript" src="Scripts/Alphabets.js"></script>

    <link href="Scripts/jquery-ui.css" rel="stylesheet" />


    <%--Don't Allow DropPaste --%>
    <script type="text/javascript" lang="javascript" src="Scripts/DropPaste.js"></script>

    <%--Allow Only Number & One Decimal & 2 digit after decimal using 
        onblur="extractNumber(this,2,true);"
        onkeyup="extractNumber(this,2,true);"
        onkeypress="return blockNonNumbers(this, event, true, true);" --%>
    <script type="text/javascript" lang="javascript" src="Scripts/Number2D.js"></script>

    <%--For Calendar Controls--%>
    <link href="calendar/calendar_orange.css" rel="stylesheet" title="Calendar" />
    <script type="text/javascript" lang="" src="calendar/calendar.js"></script>

    <%--Calculate Two TextBox values and added 3rd textbox--%>
    <script type="text/javascript">
        $(function () {
            var textBox1 = $('input:text[id$=txtCommonDhan]').keyup(foo);
            var textBox2 = $('input:text[id$=txtGradeADhan]').keyup(foo);

            function foo() {
                var value1 = textBox1.val();
                var value2 = textBox2.val();

                var sum = add(value1, value2);
                $('input:text[id$=txtTotalDhan]').val(sum);
                $('input:text[id$=txtDeliverDhan]').val(sum);

            }

            function add() {
                var sum = 0;
                for (var i = 0, j = arguments.length; i < j; i++) {
                    if (IsNumeric(arguments[i])) {
                        sum += parseFloat(arguments[i]);
                    }
                }
                return sum;
            }
            function IsNumeric(input) {
                return (input - 0) == input && input.length > 0;
            }
        });
    </script>

    <%--Script For Help Option on Mouse Over--%>
    <script type="text/javascript">
        $(function () {
            $(document).tooltip({
                track: true
            });
        });
    </script>



    <table align="center" style="width: 820px; text-align: left; border-style: solid; border-width: 1px;" border="1" cellspacing="0" cellpadding="3">
        <tr style="background-color: #FFFFFF">
            <td colspan="4" style="text-align: center; font-size: large; text-decoration: underline; color: #CC6600; border-style: none"><b style="color: #0000FF">*धान कस्टम मिलिंग के लिए अनुबंध*</b>
                <input id="hdfldArva" type="hidden" runat="server" />
                <input id="hdfldDepositMoney" type="hidden" runat="server" />
                <input id="hdfldUshnaF3" type="hidden" runat="server" />
                <input id="hdfArvaChawalRs" type="hidden" runat="server" />
                <input id="hdfldUshnaA3" type="hidden" runat="server" />
                <input id="hdfUshnaChawalRs" type="hidden" runat="server" />
                 <input id="hdfPreviousAgrmt" type="hidden" runat="server" />
                 <input id="hdfSubmittedCMRLot" type="hidden" runat="server" />
                <input id="hdfReturnAgrmtCMR_Percent" type="hidden" runat="server" />
                <br />
                <asp:Panel ID="Panel1" runat="server" Visible="False">
                    <asp:Label ID="lblLotMessage0" runat="server" ForeColor="Red" Text="Your Agreement Number Is : " Style="font-weight: 700"></asp:Label>
                    <asp:Label ID="lblLotNumber0" runat="server" ForeColor="Red" Style="font-weight: 700"></asp:Label>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">जिला</td>
            <td>
                <asp:TextBox ID="txtDistManager" runat="server" Width="137px" Enabled="False"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDistManager" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

            </td>
            <td class="auto-style1">खरीफ विपणन वर्ष</td>
            <td>

                <asp:TextBox ID="txtYear" runat="server" Width="137px" Enabled="False" Height="22px"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td class="auto-style2"><asp:Label ID="lblDM" runat="server" Text=""></asp:Label>
&nbsp;का नाम</td>
            <td colspan="3">
                <asp:TextBox ID="txtDistManagerName" runat="server" class="alphaOnly" Width="555px" Font-Size="Medium" MaxLength="40" Height="25px" Enabled="False"></asp:TextBox>
                &nbsp;<asp:Label ID="Label4" runat="server" CssClass="ButtonClass" Text="??" title="यदि जिला प्रबंधक का नाम उपलब्ध नहीं है तो कृपया जिला प्रबंधक से निवेदन करे की वह अपना नाम रजिस्टर कराए | " Font-Bold="True" Font-Size="Large" ForeColor="Blue"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDistManagerName" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

            </td>

        </tr>
        <tr>
            <td colspan="4" style="background-color: #FFFF99"></td>
        </tr>
        <tr>
            <td class="auto-style2">मिल का राज्य</td>
            <td>

                <asp:RadioButton ID="rbMPState" runat="server" Font-Bold="True" GroupName="State" Text="M.P State" AutoPostBack="True" ForeColor="Blue" OnCheckedChanged="rbMPState_CheckedChanged" />
            </td>
            <td class="auto-style1">
                <asp:RadioButton ID="rbOtherState" runat="server" Font-Bold="True" GroupName="State" Text="Other States" AutoPostBack="True" ForeColor="Blue" OnCheckedChanged="rbOtherState_CheckedChanged" />
            </td>
            <td>
                <asp:DropDownList ID="ddlOtherStates" runat="server" Height="25px" Width="212px" AutoPostBack="True" OnSelectedIndexChanged="ddlOtherStates_SelectedIndexChanged" Enabled="False">
                </asp:DropDownList>
                <asp:Label ID="lblOtherStates" runat="server" Font-Bold="True" ForeColor="Red" Text="**" Visible="False"></asp:Label>

            </td>
        </tr>
        <tr id="FCI" runat="server" visible="false" style="font-size: small; background-color: #CCFF99;">
            <td  colspan="3" style="color: #FFFFFF; background-color: #000000">क्या इस अनुबंध की धान का CMR
                <asp:Label ID="lblDist" runat="server" Text="" style="color: #FFFF00"></asp:Label>
                की FCI में जमा होगा? यदि हाँ तो, हाँ पर क्लिक करें</td>
            <td style="color: #FFFFFF; background-color: #000000">
                <asp:RadioButton ID="rdbNO" runat="server" Text="नहीं" GroupName="FCIYESNO" Checked="true" style="font-weight: 700; color: #FFFF00;" />
                <asp:RadioButton ID="rdbYes" runat="server" Text="हाँ" GroupName="FCIYESNO" style="padding-left:20px; font-weight: 700; color: #FFFF00;"/>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">मिल का जिला </td>
            <td>
                <asp:DropDownList ID="ddlMacersAddDist" runat="server" Height="26px" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlMacersAddDist_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td class="auto-style1">मिल का नाम</td>
            <td>
                <asp:DropDownList ID="ddlMacersName" runat="server" Height="27px" Width="212px" AutoPostBack="True" OnSelectedIndexChanged="ddlMacersName_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;<asp:Label ID="Label3" runat="server" CssClass="ButtonClass" Text="??" title="यदि मिल का नाम उपलब्ध नहीं है तो कृपया मिलर रजिस्ट्रेशन पेज में जाकर मिल का नाम रजिस्टर कराए | " Font-Bold="True" Font-Size="Large" ForeColor="Blue"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlMacersName" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>

            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:RadioButton ID="rbMaalik" runat="server" GroupName="rb" Text="मालिक" />
                <asp:RadioButton ID="rbProp" runat="server" GroupName="rb" Text="प्रोपराइटर" />
                <asp:RadioButton ID="rbOthers" runat="server" GroupName="rb" Text="अधिकृत प्रतिनिधि" />&nbsp;का नाम
            </td>

            <td>
                <asp:TextBox ID="txtOwnerName" runat="server" Width="208px" Font-Size="Medium" MaxLength="40" AutoComplete="off" Height="25px" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtOwnerName" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">मिलिंग क्षमता(मै०टन/घंटा)
                <asp:Label ID="Label8" runat="server" CssClass="ButtonClass" Text="??" title="आपने जो मिलर चुना है, कृपया उनकी अरवा तथा उसना की मिलिंग क्षमता(मै० टन/प्रति घंटा) को देखकर, उन्हें धान का आबंटन करें | " Font-Bold="True" Font-Size="Large" ForeColor="Blue"></asp:Label>

            </td>
            <td>

                <asp:TextBox ID="txtCapacityArva" runat="server" Width="35px" Style="text-align: right" Enabled="False"></asp:TextBox>

                <b>(अरवा)<asp:TextBox ID="txtCapacityUsna" runat="server" Width="35px" Style="margin-left: 5px; text-align: right" Enabled="False"></asp:TextBox>
                    (उसना)</b></td>
            <td class="auto-style1">अमानत राशि
                <asp:Label ID="Label9" runat="server" CssClass="ButtonClass" Text="??" title="मिलर द्वारा निविदा के साथ जमा की गई अमानत राशि | अमानत राशि पर कारपोरेशन द्वारा कोई ब्याज नहीं दिया जाएगा " Font-Bold="True" Font-Size="Large" ForeColor="Blue"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDepositMoney" runat="server" Width="137px" Style="text-align: right" Enabled="False" EnableTheming="True"></asp:TextBox>
                <b>रु०</b></td>
        </tr>

        <tr>
            <td colspan="4" style="background-color: #CCCCCC"></td>
        </tr>
        <tr>
            <td class="auto-style2">मिलर का मोबाइल नंबर</td>
            <td>
                <asp:TextBox ID="txtMobileNo0" runat="server" MaxLength="10" Width="25px" AutoComplete="off" Enabled="False">+91</asp:TextBox>

                <asp:TextBox ID="txtMobileNo" runat="server" class="Number" MaxLength="10" Width="104px" AutoComplete="off" Style="text-align: right;"></asp:TextBox>

                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtMobileNo" ID="RegularExpressionMobileNo" ValidationExpression="^[\s\S]{10,10}$" runat="server" SetFocusOnError="True" ErrorMessage="Invalid Number"></asp:RegularExpressionValidator>

                <b>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtMobileNo" ErrorMessage="RequiredFieldValidator" Font-Bold="False" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
                </b>

            </td>
            <td class="auto-style1">अनुबंध का दिनांक</td>
            <td>
                <asp:TextBox ID="txtDate" runat="server" Width="137px" ReadOnly="True"></asp:TextBox>
                <img src="calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtDate')" /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">अनुबंध दिनांक से
                <asp:Label ID="Label6" runat="server" CssClass="ButtonClass" Text="??" title="मिलर जिस दिनांक से, जिस दिनांक तक अनुबंध करना चाहता हैं, कृपया उस अनुबंध का दिनांक चुनें | " Font-Bold="True" Font-Size="Large" ForeColor="Blue"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFromDate" runat="server" Width="137px" ReadOnly="True"></asp:TextBox>
                <img src="calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtFromDate')" /><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFromDate" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
            <td class="auto-style1">अनुबंध दिनांक तक</td>
            <td>

                <asp:TextBox ID="txtToDate" runat="server" Width="137px" ReadOnly="True"></asp:TextBox>
                <img src="calendar/cal.gif" align='absmiddle' onmouseover="fnInitCalendar(this, 'ctl00_ContentPlaceHolder1_txtToDate', 'restrict=false')" /><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtToDate" ErrorMessage="RequiredFieldValidator" Font-Bold="False" SetFocusOnError="True">**</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style2" colspan="3">मिलर कितने लॉट के धान का अनुबंध करना चाहता है?</td>
            <td>
                <asp:DropDownList ID="ddlAgrmtDhanLot" runat="server" Height="26px" Width="141px" AutoPostBack="True" OnSelectedIndexChanged="ddlAgrmtDhanLot_SelectedIndexChanged">
                </asp:DropDownList>
                <b>(लॉट)</b></td>
        </tr>
        <%--<tr>
            <td class="auto-style2" colspan="3">मिलर कितने लॉट की सिक्यूरिटी राशि जमा करना चाहता है?
                                                        <asp:Label ID="Label1" runat="server" CssClass="ButtonClass" Text="??" title="मिलर को 5 लॉट से अधिक धान एक साथ प्रदाय नहीं की जा सकेगी | " Font-Bold="True" Font-Size="Large" ForeColor="Blue"></asp:Label>
            </td>
            <td>

                <asp:DropDownList ID="ddlDhanLot" runat="server" Height="26px" Width="141px">
                </asp:DropDownList>
                <b>(लॉट)</b></td>
        </tr>
        <tr>
            <td class="auto-style2" colspan="3">सिक्यूरिटी राशि
                                                        का प्रकार
                                                        <asp:Label ID="Label11" runat="server" CssClass="ButtonClass" Text="??" title="मिलर को धान प्राप्त करते समय मात्रा के अनुपात में Bank Guaranty/ FDR/ DD सिक्यूरिटी राशि जो की कम से कम 8 माह की अवधि क लिए प्रभावशील हो, के रूप में प्रस्तुत करना होगा | " Font-Bold="True" Font-Size="Large" ForeColor="Blue"></asp:Label>
            </td>

            <td>
                <asp:RadioButton ID="rbBankGuranty" runat="server" GroupName="Guranty" Text="Bank Guaranty" />
                <asp:RadioButton ID="rbTDR" runat="server" GroupName="Guranty" Style="margin-left: 5px" Text="FDR" />
                <asp:RadioButton ID="rbDD" runat="server" GroupName="Guranty" Style="margin-left: 4px" Text="DD" />
            </td>
        </tr>--%>
        <tr>
            <td colspan="4" style="background-color: #FFFF99"></td>
        </tr>
        <tr style="background-color: #cfdcc8">
            <td class="auto-style2">कामन धान<b>(Qtls)
            </b>&nbsp;<b><asp:Label ID="Label7" runat="server" CssClass="ButtonClass" Text="??" title="मिलर उपरोक्त अनुबंध अवधि में जितना धान(कामन तथा ग्रेड-ए) ले जाना चाहता हैं, कृपया उस धान का आबंटन मिलर की मिलिंग क्षमता को देखकर करे | " Font-Bold="True" Font-Size="Large" ForeColor="Blue"></asp:Label>
            </b>
                <br />
            </td>
            <td>
                <asp:TextBox ID="txtCommonDhan" runat="server" MaxLength="10" Width="137px" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" Style="text-align: right" onfocus="this.select();" onmouseup="return false;" Enabled="False"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Number" ControlToValidate="txtCommonDhan" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>

            </td>
            <td class="auto-style1">ग्रेड-ए धान<b>(Qtls)</b></td>
            <td>
                <asp:TextBox ID="txtGradeADhan" runat="server" MaxLength="10" Width="137px" AutoComplete="off"
                    onblur="extractNumber(this,2,true);"
                    onkeyup="extractNumber(this,2,true);"
                    onkeypress="return blockNonNumbers(this, event, true, true);" Style="text-align: right" onfocus="this.select();" onmouseup="return false;" Enabled="False">0</asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Number" ControlToValidate="txtGradeADhan" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr style="background-color: #cfdcc8">
            <td class="auto-style2">कुल धान<b>(Qtls)</b></td>
            <td>
                <asp:TextBox ID="txtTotalDhan" runat="server" Width="137px" Style="text-align: right" Enabled="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtTotalDhan" ErrorMessage="RequiredFieldValidator" SetFocusOnError="True" Display="Dynamic">**</asp:RequiredFieldValidator>
            </td>
            <td class="auto-style1">मिलिंग का प्रकार</td>
            <td>
                <asp:RadioButton ID="rbR_Arva" runat="server" GroupName="Return" Text="अरवा" />
                <asp:RadioButton ID="rbR_Ushna" runat="server" GroupName="Return" Text="उसना" Style="margin-left: 25px" />
            </td>
        </tr>

        <tr>
            <td colspan="4" style="text-align: center">
                <asp:Panel ID="Panellot" runat="server" Visible="False">
                    <asp:Label ID="lblLotMessage" runat="server" ForeColor="Red" Text="Your Agreement Number Is : " Style="font-weight: 700"></asp:Label>
                    <asp:Label ID="lblLotNumber" runat="server" ForeColor="Red" Style="font-weight: 700"></asp:Label>
                </asp:Panel>
                <asp:Button ID="New" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="115px" CssClass="ButtonClass" CausesValidation="False" OnClick="New_Click" />
                <asp:Button ID="btnSubmit" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Submit" Width="115px" CssClass="ButtonClass" Style="margin-left: 15px; margin-bottom: 0px;" OnClientClick="return validate();"  OnClick="Submit_Click" />
                <asp:Button ID="Print" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Print" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 15px" OnClick="Print_Click" Enabled="False" />
                <asp:Button ID="Close" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="115px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 15px" OnClick="Close_Click" />
            </td>
        </tr>

    </table>

    <table align="center" style="width: 600px">
        <tr>
            <td></td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <%--Allow Only Alphabets using class="alphaOnly" --%>
        <script type="text/javascript" lang="javascript" src="Scripts/Alphabets.js"></script>

        <%--Allow Only Numeric using class="Number" --%>
        <script type="text/javascript" lang="javascript" src="Scripts/Number.js"></script>
    </table>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>

