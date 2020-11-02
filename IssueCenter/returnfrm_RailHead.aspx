<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="returnfrm_RailHead.aspx.cs" Inherits="IssueCenter_returnfrm_RailHead" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" lang="javascript" src="../PaddyMilling/Scripts/jquery-2.1.1.js"></script>

    <script type="text/javascript">
        function CheckIsNumeric(tx) {
            var AsciiCode = event.keyCode;
            var txt = tx.value;
            var txt2 = String.fromCharCode(AsciiCode);
            var txt3 = txt2 * 1;
            if ((AsciiCode < 46) || (AsciiCode > 57)) {
                alert('Please enter only numbers.');
                event.cancelBubble = true;
                event.returnValue = false;
            }

            var num = tx.value;
            var len = num.length;
            var indx = -1;
            indx = num.indexOf('.');
            if (indx != -1) {
                var coda = event.keyCode;
                if (coda == 46) {
                    alert('Decimal cannot come twice');
                    event.cancelBubble = true;
                    event.returnValue = false;
                }
                var dgt = num.substr(indx, len);
                var count = dgt.length;
                //alert (count);
                if (count > 2) {
                    alert("Only 2 decimal digits allowed");
                    event.cancelBubble = true;
                    event.returnValue = false;
                }
            }

        }
    </script>



    <script type="text/javascript">
        function Check(textBox, maxLength) {
            if (textBox.value.length > maxLength) {
                alert("Maximum " + maxLength + " Characters Allowed");
                textBox.value = textBox.value.substr(0, maxLength);
            }
        }
    </script>


    <table style="width: 665px; height: 425px; border-right: #000000 thin groove; border-top: #000000 thin groove; border-left: #000000 thin groove; border-bottom: #000000 thin groove; background-color: #ffffcc;">
        <tr>
            <td colspan="4" style="color: #990033; height: 1px; background-color: #ffff99; border-right: blue thin groove; border-left: blue thin groove; border-bottom: blue thin groove;">रैक से वापस आई खाद्यान्न की प्रविष्टि करें |</td>
        </tr>
        <tr>
            <td colspan="2" style="border-right: blue thin groove; border-left: blue thin groove; width: 100px; border-bottom: blue thin groove; text-align: center;">
                <asp:Label ID="Label1" runat="server" Text="Select Rack Number" BackColor="#FFFFCC" ForeColor="#0000CC" Width="169px"></asp:Label></td>
            <td colspan="2" style="border-right: blue thin groove; border-left: blue thin groove; width: 100px; border-bottom: blue thin groove;">
                <asp:DropDownList ID="ddlrackno" runat="server" Width="271px" AutoPostBack="True" OnSelectedIndexChanged="ddlrackno_SelectedIndexChanged">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 100px; border-right: blue thin groove; border-left: blue thin groove; border-bottom: blue thin groove;">
                <asp:Label ID="Label2" runat="server" Text="Select Commodity" Width="129px" BackColor="#FFFFCC" ForeColor="#0000CC"></asp:Label></td>
            <td style="width: 100px; border-right: blue thin groove; border-left: blue thin groove; border-bottom: blue thin groove;">
                <asp:DropDownList ID="ddlcommodity" runat="server" Width="150px" Enabled="False">
                </asp:DropDownList></td>
            <td style="width: 100px; border-right: blue thin groove; border-left: blue thin groove; border-bottom: blue thin groove;">
                <asp:Label ID="lblscheme" runat="server" Text="Select Scheme" Width="125px" BackColor="#FFFFCC" ForeColor="#0000CC"></asp:Label></td>
            <td style="width: 100px; border-right: blue thin groove; border-left: blue thin groove; border-bottom: blue thin groove;">
                <asp:DropDownList ID="ddlscheme" runat="server" Width="166px" Enabled="False">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 122px; border-right: blue thin groove; border-left: blue thin groove; border-bottom: blue thin groove;">
                <asp:Label ID="Label3" runat="server" Text="No of Bags" Width="121px" BackColor="#FFFFCC" ForeColor="#0000CC"></asp:Label></td>
            <td style="width: 122px; border-right: blue thin groove; border-left: blue thin groove; border-bottom: blue thin groove;">
                <asp:TextBox ID="txtbagno" runat="server" Width="146px" AutoComplete="off" class="Number" MaxLength="3" Style="text-align: right" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
            </td>
            <td style="width: 122px; border-right: blue thin groove; border-left: blue thin groove; border-bottom: blue thin groove;">
                <asp:Label ID="Label4" runat="server" Text="Quantity Received" Width="127px" BackColor="#FFFFCC" ForeColor="#0000CC"></asp:Label></td>
            <td style="width: 122px; border-right: blue thin groove; border-left: blue thin groove; border-bottom: blue thin groove;">
                <asp:TextBox ID="txtquant" runat="server" Width="155px" AutoComplete="off" MaxLength="10" Style="text-align: right"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td style="width: 100px; border-right: blue thin groove; border-left: blue thin groove; border-bottom: blue thin groove;">
                <asp:Label ID="Label5" runat="server" Text="Challan Number" BackColor="#FFFFCC" ForeColor="#0000CC"></asp:Label></td>
            <td style="width: 100px; border-right: blue thin groove; border-left: blue thin groove; border-bottom: blue thin groove;">
                <asp:TextBox ID="txttrukchlnno" runat="server" Width="121px" Height="16px" AutoComplete="off"></asp:TextBox>
                <br />
                <asp:DropDownList ID="ddlChallanNo" runat="server" Width="150px" Visible="False" OnSelectedIndexChanged="ddlChallanNo_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList></td>
            <td style="width: 100px; border-right: blue thin groove; border-left: blue thin groove; border-bottom: blue thin groove;">
                <asp:Label ID="Label6" runat="server" Text="Truck Number" BackColor="#FFFFCC" ForeColor="#0000CC"></asp:Label></td>
            <td style="width: 100px; border-right: blue thin groove; border-left: blue thin groove; border-bottom: blue thin groove;">
                <asp:TextBox ID="txttruckno" runat="server" Width="146px" AutoComplete="off" Class="TruckNumber" MaxLength="14" onfocus="this.select();" onmouseup="return false;"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 100px; border-right: blue thin groove; border-left: blue thin groove; border-bottom: blue thin groove;">
                <asp:Label ID="Label7" runat="server" Text="Receiving Date" Width="121px" BackColor="#FFFFCC" ForeColor="#0000CC"></asp:Label></td>
            <td style="width: 100px; border-right: blue thin groove; border-left: blue thin groove; border-bottom: blue thin groove;">

                <asp:TextBox ID="DaintyDate1" runat="server" Width="99px" AutoComplete="off"></asp:TextBox>
                <script type="text/javascript">
                    new tcal({
                        'formname': '0',
                        'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate1'
                    });
                </script>
            </td>
            <td style="width: 100px; border-right: blue thin groove; border-left: blue thin groove; border-bottom: blue thin groove;">
                <asp:Label ID="Label8" runat="server" Text="Receving Godown" Width="127px" BackColor="#FFFFCC" ForeColor="#0000CC"></asp:Label></td>
            <td style="width: 100px; border-right: blue thin groove; border-left: blue thin groove; border-bottom: blue thin groove;">
                <asp:DropDownList ID="ddlgodown" runat="server" Width="176px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 100px; border-right: blue thin groove; border-left: blue thin groove; border-bottom: blue thin groove;">
                <asp:Label ID="Label9" runat="server" Text="Crop Year" BackColor="#FFFFCC" ForeColor="#0000CC"></asp:Label></td>
            <td style="width: 100px; border-right: blue thin groove; border-left: blue thin groove; border-bottom: blue thin groove;">
                <asp:DropDownList ID="ddlcropyear" runat="server" Width="143px">
                </asp:DropDownList></td>
            <td style="width: 100px; border-right: blue thin groove; border-left: blue thin groove; border-bottom: blue thin groove;"></td>
            <td style="width: 100px; border-right: blue thin groove; border-left: blue thin groove; border-bottom: blue thin groove;"></td>
        </tr>
        <tr>
            <td style="border-right: blue thin groove; border-left: blue thin groove; border-bottom: blue thin groove; height: 1px; background-color: white;">
                <asp:Label ID="Label10" runat="server" Text="Remarks (If any)" BackColor="#FFFFCC" ForeColor="#0000CC" Width="129px"></asp:Label></td>
            <td colspan="3" style="border-right: blue thin groove; border-left: blue thin groove; border-bottom: blue thin groove; height: 1px; background-color: white;">
                <asp:TextBox ID="txtremark" runat="server" Height="20px" TextMode="MultiLine" Width="471px" CssClass="alphaNumericWithSpecial" onKeyUp="javascript:Check(this, 50);" onChange="javascript:Check(this, 50);"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="height: 1px; background-color: teal; border-right: blue thin groove; border-left: blue thin groove;" colspan="4"></td>
        </tr>
        <tr>
            <td style="width: 100px; border-right: blue thin groove; border-top: blue thin groove; border-bottom: blue thin groove;">
                <asp:HiddenField ID="hdfSTO_No" runat="server" />
            </td>
            <td style="width: 100px; border-right: blue thin groove; border-top: blue thin groove; border-bottom: blue thin groove;">
                <asp:Button ID="btnNew" runat="server" Text="New" Width="91px" OnClick="btnNew_Click" /></td>
            <td style="width: 100px; border-right: blue thin groove; border-top: blue thin groove; border-bottom: blue thin groove;">
                <asp:Button ID="btnsubmit" runat="server" Text="Submit" Width="98px" OnClick="btnsubmit_Click" /></td>
            <td style="width: 100px; border-right: blue thin groove; border-top: blue thin groove; border-bottom: blue thin groove;">
                <asp:Button ID="btnClose" runat="server" Text="Close" Width="107px" OnClick="btnClose_Click" /></td>
        </tr>
        <tr>
            <td colspan="4" style="border-right: blue thin groove; border-top: blue thin groove; border-bottom: blue thin groove">
                <asp:Label ID="Label11" runat="server" ForeColor="Red" Width="631px"></asp:Label></td>
        </tr>

        <script lang="javascript" src="../PaddyMilling/Scripts/Number.js" type="text/javascript">       </script>
        <script lang="javascript" src="../PaddyMilling/Scripts/TruckNo.js" type="text/javascript">      </script>

        <script lang="javascript" src="../PaddyMilling/Scripts/Alphabets.js" type="text/javascript">    </script>
    </table>
</asp:Content>

