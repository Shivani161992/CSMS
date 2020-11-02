<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="PaymentEntry.aspx.cs" Inherits="District_PaymentEntry" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
    function CheckCalDate(tx) {
        var AsciiCode = event.keyCode;
        var txt = tx.value;
        var txt2 = String.fromCharCode(AsciiCode);
        var txt3 = txt2 * 1;
        if ((AsciiCode > 0)) {
            alert('Please Click on Calander Controll to Enter Date');
            event.cancelBubble = true;
            event.returnValue = false;
        }
    }
</script>
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
            if (count > 5) {
                alert("Only 5 decimal digits allowed");
                event.cancelBubble = true;
                event.returnValue = false;
            }
        }

    }



    </script>

    <table style="border: thin groove #000000; width: 600px; height: 355px">
        <tr>
            <td colspan="2" style="color: #000099; font-size: large">
                <b>Payment to Society Entry (Procurement Season)</b></td>
        </tr>
        <tr>
            <td>
                District</td>
            <td>
                <asp:Label ID="lbldist" runat="server"></asp:Label>
                                            </td>
        </tr>
        <tr>
            <td>
                Select Crop</td>
            <td>
                <asp:DropDownList ID="ddlcrop" runat="server" AutoPostBack="True" Height="16px" 
                    onselectedindexchanged="ddlcrop_SelectedIndexChanged" Width="100px">
                </asp:DropDownList>
                                            </td>
        </tr>
        <tr>
            <td>
                Date of Payment</td>
            <td>
                <asp:TextBox ID="txtPaydate" runat="server" Width="105px"></asp:TextBox>
                <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_txtPaydate'
	    });
	     </script>
                </td>
        </tr>
        <tr>
            <td>
                Name of Procurement Center</td>
            <td>
                <asp:DropDownList ID="ddlsociety" runat="server" Height="33px" Width="250px" 
                    AutoPostBack="True">
                </asp:DropDownList>
                                            </td>
        </tr>
        <tr>
            <td style="height: 18px">
                Deposit Quantity by Procurement Center(MT)</td>
            <td style="height: 18px">
                <asp:Label ID="lbldepositqty" runat="server"></asp:Label>
                                            </td>
        </tr>
        <tr>
            <td>
                Amount Payable</td>
            <td>
                <asp:Label ID="lblamtpayble" runat="server"></asp:Label>
                                            </td>
        </tr>
        <tr>
            <td>
                Amount Paid to Society (In Lakh)</td>
            <td>
                <asp:Label ID="lblamtpaidtosoc" runat="server"></asp:Label>
                                            </td>
        </tr>
        <tr>
            <td>
                Deduction in TDS</td>
            <td>
                <asp:TextBox ID="txtdeduction" runat="server" Width="100px">0</asp:TextBox>
                </td>
        </tr>
        <tr>
            <td>
                Deduction in Poor Stencile</td>
            <td>
                <asp:TextBox ID="txtdeductstencile" runat="server" Width="100px">0</asp:TextBox>
                </td>
        </tr>
        <tr>
            <td>
                Deduction in Poor Stiching</td>
            <td>
                <asp:TextBox ID="txtdeductstich" runat="server" Width="100px">0</asp:TextBox>
                </td>
        </tr>
        <tr>
            <td>
                Deduction in Unserviceable Gunny</td>
            <td>
                <asp:TextBox ID="txtdeductgunny" runat="server" Width="100px">0</asp:TextBox>
                </td>
        </tr>
        <tr>
            <td>
                Deduction in Others</td>
            <td>
                <asp:TextBox ID="txtdeductothers" runat="server" Width="100px">0</asp:TextBox>
                </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnclac" runat="server" Font-Bold="True" Font-Size="Medium" 
                    ForeColor="#000066" Text="Click hear to Calculate Net Amount Payble" 
                    Width="360px" onclick="btnsave_Click" />
            </td>
        </tr>
        <tr>
            <td>
                Net Amount Payable</td>
            <td>
                <asp:Label ID="lblnetpayble" runat="server"></asp:Label>
                                            </td>
        </tr>
        <tr>
            <td>
                Net Amount Paid (in Lakh)</td>
            <td>
                <asp:TextBox ID="txtnetamtpaid" runat="server" Width="105px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td style="height: 39px">
                <asp:Button ID="btnclose" runat="server" Font-Bold="True" ForeColor="#000066" 
                    Text="Close" Width="91px" onclick="btnclose_Click" />
            </td>
            <td style="height: 39px">
                <asp:Button ID="btnsave" runat="server" Font-Bold="True" Font-Size="Medium" 
                    ForeColor="#000066" Text="Save" Width="100px" onclick="btnsave_Click" />
            </td>
        </tr>
    </table>



</asp:Content>

