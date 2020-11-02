<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintStackNumber.aspx.cs"
    Inherits="IssueCenter_PrintStackNumber" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .A4
        {
            width: 210mm; /* You willmay need to reduce this to handle printer margins */
            margin: auto; /* This means it will be centred */
            height: 250mm;
            text-align: justify;
            font-size: medium;
        }

        .LineBreak
        {
            /*page-break-before: always;*/
            height: 0mm;
            overflow: hidden;
        }

        .auto-style1
        {
            width: 100%;
            border-style: solid;
            border-width: 2px;
            border-color: black;
        }

        p
        {
            margin-right: 0in;
            margin-left: 0in;
            font-size: 12.0pt;
            font-family: "Times New Roman", "serif";
        }

        @page
        {
            size: auto; /* auto is the current printer page size */
            margin: 25px 25px 0px 30px; /* this affects the margin in the printer settings */
        }

        .auto-style3
        {
            width: 100%;
        }

        .auto-style4
        {
            width: 100%;
            font-size: large;
        }
    </style>
</head>
<body onload="window.print()">
    <form id="form1" runat="server">
        <br />
        <div class="A4">
            <table align="center" class="auto-style1" style="width: 925px;">
                <tr>
                    <td rowspan="3" style="vertical-align: top; text-align: right" cellspacing="0" cellpadding="0">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/mpscsc_logo.jpg" Height="120px"
                            Width="120px" />
                    </td>
                    <td style="text-align: center" class="auto-style4">
                        <strong style="color: #003399">MP State Civil Supplies Corporation Limited</strong>
                    </td>

                </tr>
                <tr>
                    <td style="text-align: center" class="auto-style3">District ....<asp:Label ID="lblInspDist" runat="server" Font-Bold="True"></asp:Label>....<asp:Label
                        ID="lblYear" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center" class="auto-style3">
                        <strong>&lt;&lt;
                        <asp:Label ID="lblInspActRjt" runat="server" Text="Stack Acceptance/Rejection Note" Style="text-decoration: underline"></asp:Label>
                            &nbsp;&gt;&gt;</strong>
                    </td>
                </tr>
            </table>
            <table id="Table1" align="center" class="auto-style1" runat="server" style="width: 925px">
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblAcptNo" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                        <asp:Label ID="lblrejection" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                        <asp:Label ID="lblinspectionid" runat="server" Style="text-align: center; font-weight: 700;"
                            ForeColor="white" Visible="false"></asp:Label>

                    </td>
                    <td>&nbsp;
                    </td>
                    <td style="text-align: right">
                        <asp:Label ID="lblAcptRjctDate" runat="server" Text=" Date -"></asp:Label>
                        &nbsp;....<asp:Label ID="lblAcptDate" runat="server" Font-Bold="True"></asp:Label>....
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>To,
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; District
                    Manager
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; M.P.
                    State Civil Supplies Corporation
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    ....<asp:Label ID="lbldist" runat="server" Font-Bold="True"></asp:Label>....
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="2">Sub:-&nbsp; Inspection Of your District Godown Comodity:-....<asp:Label ID="lblcommo"
                        runat="server" Font-Bold="True"></asp:Label>....
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>महोदय,
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="4">आपके जिले के गोदामो में संगृहीत स्कंध की गुणवक्ता की जांच निम्म विवरण अनुसार की
                    गईएंव जांच में निम्म घटक पाये गये जिसके आधार पर स्टैक की मात्रा को  ....<asp:Label ID="Label17" runat="server" Font-Bold="True"></asp:Label>.... किया
                    जाता है|
                    </td>
                </tr>
                <tr>
                    <td colspan="4"></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <div style="width: 100%;">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                CellPadding="4"
                                ForeColor="#333333" GridLines="None" Width="100%" CssClass="gridheader"
                                EnableModelValidation="True" border="1">

                                <Columns>




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
                                    <asp:BoundField DataField="PaddyDO" HeaderText="Paddy DO">
                                        <HeaderStyle Font-Names="Arial" Font-Size="12px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CMRDO" HeaderText="CMR DO">
                                        <HeaderStyle Font-Names="Arial" Font-Size="12px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CMR_Acceptance_No" HeaderText="CMR Acceptance">
                                        <HeaderStyle Font-Names="Arial" Font-Size="12px" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Accept_CommonRice" HeaderText="Qty (Qtls)">
                                        <HeaderStyle Font-Names="Arial" Font-Size="12px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="firstBags" HeaderText="Bags">
                                        <HeaderStyle Font-Names="Arial" Font-Size="12px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BagType" HeaderText="BagType">
                                        <HeaderStyle Font-Names="Arial" Font-Size="12px" />
                                    </asp:BoundField>



                                </Columns>
                                <RowStyle BackColor="#ffffff" ForeColor="#000000" Font-Size="16px" HorizontalAlign="Center" />

                                <HeaderStyle BackColor="white" ForeColor="Black" Font-Bold="true" HorizontalAlign="Center" Font-Size="20px" />

                            </asp:GridView>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>



                <tr>
                    <td colspan="4">
                        <table align="center" border="1" style="border-spacing: 1px; text-align: center; width: 894px; font-size: small"
                            id="table2">
                            <tr>
                                <td colspan="4" style="color: black; width: 1050px; background-color: white; text-align: center; font-size: large; text-decoration: underline;">
                                    <strong>Quality Inspection (Rice)</strong>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 31px;">क्रम सं.
                                </td>
                                <td style="width: 31px; text-align: center;">अपवर्तन
                                </td>
                                <td style="width: 114px;">अधिकतम सीमा (प्रतिशत)<br />
                                    <b>कामन</b>
                                </td>
                                <td style="width: 114px;">गुणवत्ता परिक्षण उपरांत परिणाम<br />
                                    <b>कामन</b>
                                </td>
                            </tr>
                            <tr>
                                <td rowspan="2" style="width: 31px">1.
                                </td>
                                <td style="text-align: left; height: 51px;">टोटा<br />
                                </td>
                                <td style="width: 114px; height: 51px;">
                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                </td>
                                <td style="width: 114px; height: 51px;">
                                    <asp:Label ID="Label9" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; height: 51px;">छोटे टोटे
                                </td>
                                <td style="height: 51px">
                                    <asp:Label ID="Label2" runat="server"></asp:Label>
                                </td>
                                <td style="height: 51px">
                                    <asp:Label ID="Label10" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 31px; height: 51px;">2.
                                </td>
                                <td style="text-align: left; height: 51px;">विजातीय तत्व **
                                </td>
                                <td style="width: 114px; height: 51px;">
                                    <asp:Label ID="Label3" runat="server"></asp:Label>
                                </td>
                                <td style="width: 114px; height: 51px;">
                                    <asp:Label ID="Label11" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 31px">3.
                                </td>
                                <td style="text-align: left; height: 51px;">क्षतिग्रस्त दाने
                                </td>
                                <td style="width: 114px">
                                    <asp:Label ID="Label4" runat="server"></asp:Label>
                                </td>
                                <td style="width: 114px">
                                    <asp:Label ID="Label12" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 31px; height: 51px;">4.
                                </td>
                                <td style="text-align: left; height: 51px;">बदरंग दाने
                                </td>
                                <td style="width: 114px; height: 51px;">
                                    <asp:Label ID="Label5" runat="server"></asp:Label>
                                </td>
                                <td style="width: 114px; height: 51px;">
                                    <asp:Label ID="Label13" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 31px; height: 51px;">5.
                                </td>
                                <td style="text-align: left; height: 51px;">चाकी दाने
                                </td>
                                <td style="width: 114px; height: 51px;">
                                    <asp:Label ID="Label6" runat="server"></asp:Label>
                                </td>
                                <td style="width: 114px; height: 51px;">
                                    <asp:Label ID="Label14" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 31px; height: 51px;">6.
                                </td>
                                <td style="text-align: left; height: 51px;">लाल दाने
                                </td>
                                <td style="width: 114px; height: 51px;">
                                    <asp:Label ID="Label7" runat="server"></asp:Label>
                                </td>
                                <td style="width: 114px; height: 51px;">
                                    <asp:Label ID="Label15" runat="server"></asp:Label>
                                </td>
                            </tr>
                    </td>
                </tr>
                <tr>
                    <td style="width: 31px; height: 51px;">7.
                    </td>
                    <td style="text-align: left; height: 51px;">नमी तत्व (R)
                    </td>
                    <td style="width: 114px; height: 51px;">
                        <asp:Label ID="Label8" runat="server"></asp:Label>
                    </td>
                    <td style="width: 114px; height: 51px;">
                        <asp:Label ID="Label16" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <table id="Table3" align="center" class="auto-style1" style="width: 925px" runat="server">
                <tr>
                    <td colspan="4"></td>
                </tr>
                <tr>
                    <td>निरक्षण का दिनाँक
                    </td>
                    <td>&nbsp;....<asp:Label ID="lblinspdate" runat="server" Font-Bold="True"></asp:Label>....
                    </td>
                    <td>जिले का नाम
                    </td>
                    <td>....<asp:Label ID="lbldistinspone" runat="server" Font-Bold="True"></asp:Label>....
                    </td>
                </tr>
                <tr>
                    <td>प्रदाय केंद्र का नाम
                    </td>
                    <td>....<asp:Label ID="lblIC" runat="server" Font-Bold="True"></asp:Label>....
                    </td>
                    <td>गोदाम का नाम
                    </td>
                    <td>....<asp:Label ID="lblgodown" runat="server" Font-Bold="True"></asp:Label>....
                    </td>
                </tr>
                <tr>
                    <td>स्टैक क्रिमंक
                    </td>
                    <td>....<asp:Label ID="lblstack" runat="server" Font-Bold="True"></asp:Label>....
                    </td>
                    <td>गोदाम इंस्पेक्टर का नाम
                    </td>
                    <td>....<asp:Label ID="lblinspname" runat="server" Font-Bold="True"></asp:Label>....
                    </td>
                </tr>
                <tr>
                    <td>गोदाम इंस्पेक्टर का पद
                    </td>
                    <td>....<asp:Label ID="lblinspdesg" runat="server" Font-Bold="True"></asp:Label>....
                    </td>
                    <td>बैग
                    </td>
                    <td>....<asp:Label ID="lblbag" runat="server" Font-Bold="True"></asp:Label>....
                    </td>




                </tr>
                <tr>
                </tr>
                <tr>
                    <td colspan="4">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="4">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right" colspan="4">
                        <b>हस्ताक्षर गोदाम निरक्षणकर्ता</b>
                    </td>
                </tr>
                <tr>
                    <td colspan="4"></td>
                </tr>
                <tr>
                    <td colspan="4"></td>
                </tr>
                <tr>
                    <td colspan="4"></td>
                </tr>
                <tr>
                    <td colspan="4"></td>
                </tr>
                <tr>
                    <td colspan="4"></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
