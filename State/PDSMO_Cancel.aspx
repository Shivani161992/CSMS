<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="PDSMO_Cancel.aspx.cs" Inherits="State_PDSMO_Cancel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>

    <style type="text/css">
        .ButtonClass {
            cursor: pointer;
        }

        .hiddencol {
            display: none;
        }

        input[type="button"] {
            cursor: pointer;
        }
        .auto-style1 {
            color: #CC6600;
            font-size: medium;
        }
    </style>

    <script type="text/javascript">

        function TimerFunc() {
            var c = 300;
            var t;
            timedCount();

            function timedCount() {

                var hours = parseInt(c / 3600) % 24;
                var minutes = parseInt(c / 60) % 60;
                var seconds = c % 60;

                //var result = (hours < 10 ? "0" + hours : hours) + ":" + (minutes < 10 ? "0" + minutes : minutes) + ":" + (seconds < 10 ? "0" + seconds : seconds);
                var result = (minutes < 10 ? "0" + minutes : minutes) + ":" + (seconds < 10 ? "0" + seconds : seconds);

                $('#timer').html(result);
                if (c == 0) {
                    //setConfirmUnload(false);
                    //$("#quiz_form").submit();
                    window.location = "PDSMovementOrder_Acpt.aspx";
                }
                c = c - 1;
                t = setTimeout(function () {
                    timedCount()
                },
                1000);

                var GetLabelValue = document.getElementById('ctl00_ContentPlaceHolder1_lblmsg').innerText;
                if (GetLabelValue == 'Success') {
                    c = -500;
                }
            }
        }

        $(document).ready(function () {
            $('#ctl00_ContentPlaceHolder1_ChkOTP').click(function () {

                var GenerateOTP = "<%=this.GenerateOTP%>";
                var EnteredOTP = document.getElementById('ctl00_ContentPlaceHolder1_txtOTP').value;

                //alert(GenerateOTP);

                if (EnteredOTP == GenerateOTP) {
                    //alert('Success');
                    document.getElementById("ctl00_ContentPlaceHolder1_lblmsg").innerHTML = 'Success';
                    document.getElementById("ctl00_ContentPlaceHolder1_lblmsg").style.color = "#00CC00";
                    document.getElementById("ctl00_ContentPlaceHolder1_txtOTP").disabled = true;
                    document.getElementById("ctl00_ContentPlaceHolder1_btnCancel").disabled = false;
                    document.getElementById("ctl00_ContentPlaceHolder1_ChkOTP").disabled = true;

                    $('#timer').hide()
                }
                else {
                    //alert('Failed');
                    document.getElementById("ctl00_ContentPlaceHolder1_lblmsg").innerHTML = 'Failed';
                    document.getElementById("ctl00_ContentPlaceHolder1_lblmsg").style.color = "Red";
                    document.getElementById("ctl00_ContentPlaceHolder1_btnCancel").disabled = true;
                }

            });
        });
    </script>

    <table align="center" style="width: 640px; border-style: solid; border-width: 1px; text-align: left" border="1" cellspacing="0" cellpadding="6">
        <tr>
            <td colspan="6" style="text-align: center; font-size: large; text-decoration: underline; color: #CC6600"><strong>Cancellation of PDS Movement Order</strong></td>
            <input id="hdfMobileNo" type="hidden" runat="server" />
            <input id="hdfEmpID" type="hidden" runat="server" />
            <input id="hdfOTP" type="hidden" runat="server" />
        </tr>
        <tr>
            <td rowspan="4">&nbsp;</td>
            <td>Crop Year</td>
            <td>
                <asp:DropDownList ID="ddlCropYear" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlCropYear_SelectedIndexChanged">
                </asp:DropDownList></td>
            <td>Commodity</td>
            <td>
               <asp:DropDownList ID="ddlCommodity" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlCommodity_SelectedIndexChanged" >
                </asp:DropDownList></td>
            <td rowspan="4">&nbsp;</td>
        </tr>
        <tr>
            <td>Mode of Dispatch</td>
            <td>
                <asp:DropDownList ID="ddlComdtyMode" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlComdtyMode_SelectedIndexChanged">
                </asp:DropDownList></td>
            <td>Sending District</td>
            <td>
                <asp:DropDownList ID="ddlFrmDist" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlFrmDist_SelectedIndexChanged"  >
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td>MO Number</td>
            <td>
                <asp:DropDownList ID="ddlMvmtNo" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlMvmtNo_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>MO Date</td>
            <td>
                <asp:TextBox ID="txtMODate" runat="server" Enabled="False" Width="146px"></asp:TextBox>
            </td>
        </tr>
        <tr id="rowGunnyType" runat="server" visible="false">
            <td>Types of Gunny</td>
            <td>
                <asp:TextBox ID="txtGunnyType" runat="server" Enabled="False" Width="146px"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td colspan="4" style="background-color: #CC6600"></td>
        </tr>
        <tr>
            <td colspan="6">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" CellPadding="4" EnableModelValidation="True" ShowFooter="True" OnRowDataBound="GridView1_RowDataBound" Width="100%">
                    <Columns>
                        <asp:BoundField HeaderText="क्र." ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="प्रेषण कर्ता जिला" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="प्राप्तकर्ता जिला" DataField="ReceiveDistName" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="परिवहन अवधि" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="उपार्जन वर्ष" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="परिवहन की मात्रा (मै० टन)" DataField="PDSMOQty" DataFormatString="{0:0.00}">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="ToDistCode" DataField="ToDist" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" FooterStyle-CssClass="hiddencol" />
                    </Columns>
                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="Black" HorizontalAlign="Right" VerticalAlign="Middle" />
                    <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Black" />
                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" ForeColor="#330099" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                </asp:GridView>
            </td>
        </tr>

        <tr>
            <td colspan="6" style="background-color: #CC6600"></td>
        </tr>
        <tr>
            <td colspan="6">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" CellPadding="4" EnableModelValidation="True" ShowFooter="True" OnRowDataBound="GridView2_RowDataBound" Width="100%">
                    <Columns>
                        <asp:BoundField HeaderText="क्रमांक" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="रैक प्राप्तकर्ता जिला" DataField="RackRecdDist" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="स्कंध प्राप्तकर्ता जिला" DataField="RecdDist" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="परिवहन की मात्रा (मै० टन)" DataField="SubQtyByDist" DataFormatString="{0:0.00}">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="ToDistCode" DataField="ToOtherDist" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" FooterStyle-CssClass="hiddencol" />
                    </Columns>
                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="Black" HorizontalAlign="Right" VerticalAlign="Middle" />
                    <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Black" />
                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" ForeColor="#330099" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                </asp:GridView>

            </td>
        </tr>
        <tr>

            <td colspan="6" style="text-align: center; font-size: large;"><span class="auto-style1">Select Your Mobile Number For OTP</span>
                <asp:DropDownList ID="ddlMobileNo" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlMobileNo_SelectedIndexChanged">
                </asp:DropDownList>

                <asp:TextBox ID="txtEmpName" runat="server" Enabled="False" Width="176px"></asp:TextBox>

            </td>

        </tr>
        <tr>

            <td colspan="6" style="text-align: center; font-size: large;">&nbsp;

                 <input type="button" id="btnOTP" runat="server" onserverclick="btnOTP_Click" value="Send OTP" style="background-color: Silver; border-color: red; border-style: solid; font-weight: bold; height: 23px; width: 100px;" causesvalidation="false" disabled="disabled" />
                <asp:TextBox ID="txtOTP" runat="server" AutoComplete="off" placeholder="Enter OTP Number" Style="border-color: black;" Enabled="False"></asp:TextBox>

                <span id='timer' style="color: #FF0000; font-weight: bold"></span>

                <input type="button" id="ChkOTP" runat="server" value="Check OTP" style="background-color: Silver; border-color: red; border-style: solid; font-weight: bold; height: 23px; width: 100px;" causesvalidation="false" disabled="disabled" />
                <asp:Label ID="lblmsg" runat="server" Text="" Style="font-weight: 700;"></asp:Label>

            </td>

        </tr>
        <tr>
            <td>&nbsp;</td>
            <td colspan="4" style="text-align: center; font-size: large;">

                <asp:Button ID="btnRecptNew" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="New" Width="100px" CssClass="ButtonClass" CausesValidation="False" OnClick="btnRecptNew_Click" />

                <asp:Button ID="btnCancel" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Cancel" Width="100px" CssClass="ButtonClass" Style="margin-left: 10px" CausesValidation="False" OnClick="btnCancel_Click" OnClientClick="return confirm('Are You Sure To Cancel Movement Order?');" disabled="disabled"/>

                <asp:Button ID="btnPrint" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Print" Width="100px" CssClass="ButtonClass" Style="margin-left: 10px" Enabled="False" CausesValidation="False" OnClick="btnPrint_Click" />

                <asp:Button ID="btnRecptClose" runat="server" BackColor="Silver" BorderColor="#333300" BorderStyle="Solid" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Height="30px" Text="Close" Width="100px" CssClass="ButtonClass" CausesValidation="False" Style="margin-left: 10px" OnClick="btnRecptClose_Click" />
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>

