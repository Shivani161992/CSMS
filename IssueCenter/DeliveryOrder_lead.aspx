<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="DeliveryOrder_lead.aspx.cs" Inherits="IssueCenter_DeliveryOrder_lead" Title="Delivery Order" Debug="true"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script language="javascript" type="text/javascript">
        var submit = 0;
        function CheckIsRepeat() {
            if (++submit > 1) {
                alert('An attempt was made to submit this form more than once; this extra attempt will be ignored.');
                return false;
            }
        }
    </script>

<script type="text/javascript">
    function CheckIsNumeric(e,tx)
    {         
        var AsciiCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;                        
        if ((AsciiCode < 46 && AsciiCode != 8) || (AsciiCode > 57 ) || (AsciiCode == 47 ))
        {
            alert('Please enter only numbers.');
            return false;
        }                
        var num=tx.value;        
        var len=num.length;
        var indx=-1;
        indx=num.indexOf('.');
        if (indx != -1)
        {
            if ((AsciiCode == 46 ))
            {
                alert('Point must be apear only one time.');
                return false;
            }
            var dgt=num.substr(indx,len);
            var count= dgt.length;
            //alert (count);
            if (count > 5 && AsciiCode != 8)  
            {
                alert("Only 5 decimal digits allowed.");
                return false;
            }
        }
    }
    </script>
 
	<script type="text/javascript">
function CheckCalDate(tx){
var AsciiCode = event.keyCode;
var txt=tx.value;
var txt2 = String.fromCharCode(AsciiCode);
var txt3=txt2*1;
if ((AsciiCode > 0))
{
alert('Please Click on Calander Controll to Enter Date');
event.cancelBubble = true;
event.returnValue = false;
}
}
</script>
    <div style="text-align: right">
    <asp:Panel ID="panelDO" runat ="server"  >
        <table style="width: 717px">
        <tr><td align="center" style="background-color: maroon; height: 20px;">
            <asp:Label ID="lbldo" runat="server" Font-Bold="True" ForeColor="Transparent" Text="Delivery Order"
                Width="117px"></asp:Label></td></tr>
            <tr>
                <td align="left" style="height: 341px">
                <fieldset style="vertical-align: top; text-align: left; width: 623px; height: 313px;" >
                <legend>
                    <asp:Label ID="Label8" runat="server" Text="Permit / Release Order Details"></asp:Label>&nbsp;</legend>
                    <table style="width: 703px; background-color: skyblue;" border="1" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left" colspan="6" style="font-size: 10pt; position: static; height: 21px;
                                background-color: #ffffff">
                                <span style="color: #ff0033"><strong>कृपया ध्यान रखे की अब Deivery Order का नंबर कंप्यूटर
                                    से जनरेट होगा, अतः D.O. Number &nbsp;प्रविष्ठी का विकल्प हटा दिया गया है </strong>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 21px;" align="left">
                                <asp:Label ID="Label1" runat="server" ForeColor="Blue" Font-Size="Medium"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; width: 144px; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="lblpermitno" runat="server" Font-Size="12px" Text="Permit No."></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; width: 168px; background-color: #cfdcdc;">
                                <asp:TextBox ID="tx_permit_no" runat="server" Width="150px" MaxLength="15">0</asp:TextBox></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 207px;">
                                <asp:Label ID="lblpermitdate" runat="server" Font-Size="12px" Text="PermitDate"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 275px;">
                                <asp:TextBox ID="tx_permit_date" runat="server" Width="119px"></asp:TextBox>
             <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_tx_permit_date'
	    });
	     </script>
                            </td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                </td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 4px;">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; width: 144px; position: static; background-color: #cfdcdc; height: 25px;">
                                <asp:Label ID="lbltoissue" runat="server" Font-Size="12px" Text="Issue To"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; width: 168px; background-color: #cfdcdc; height: 25px;">
                                <asp:DropDownList ID="ddl_issueto" runat="server" OnSelectedIndexChanged="ddl_issueto_SelectedIndexChanged" Width="165px">
                                <asp:ListItem Value="L">Lead Society</asp:ListItem>
                            </asp:DropDownList></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 25px; width: 207px;">
                                <asp:Label ID="lbl_lead" runat="server" Font-Size="12px" Text="Name" Width="40px"></asp:Label></td>
                            <td align="left" colspan="3" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 25px;">
                                <asp:DropDownList ID="ddl_lead" runat="server" Font-Names="Kruti Dev 010" Font-Size="Medium" OnSelectedIndexChanged="ddl_lead_SelectedIndexChanged" Width="259px">
                                </asp:DropDownList>&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; width: 144px; position: static; background-color: #cfdcdc; height: 24px;">
                                <asp:Label ID="lblallotmonth" runat="server" Font-Size="12px" Text="Allot. Month" Width="67px" Visible="true"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; width: 168px; position: static; background-color: #cfdcdc; height: 24px;">
                                <asp:DropDownList ID="ddl_allot_month" runat="server" Width="164px" Visible="true">
                                <asp:ListItem Value="1">January</asp:ListItem>
                                <asp:ListItem Value="2">February</asp:ListItem>
                                <asp:ListItem Value="3">March</asp:ListItem>
                                <asp:ListItem Value="4">April</asp:ListItem>
                                <asp:ListItem Value="5">May</asp:ListItem>
                                <asp:ListItem Value="6">June</asp:ListItem>
                                <asp:ListItem Value="7">July</asp:ListItem>
                                <asp:ListItem Value="8">August</asp:ListItem>
                                <asp:ListItem Value="9">September</asp:ListItem>
                                <asp:ListItem Value="10">October</asp:ListItem>
                                <asp:ListItem Value="11">November</asp:ListItem>
                                <asp:ListItem Value="12">December</asp:ListItem>
                            </asp:DropDownList></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 207px; height: 24px;">
                                <asp:Label ID="lblallotyear" runat="server" Font-Size="12px" Text="Allot. Year" Visible="true"></asp:Label></td>
                            <td align="left" colspan="2" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 24px;">
                                <asp:DropDownList ID="ddd_allot_year" runat="server" Width="158px" Visible="true">
                                </asp:DropDownList></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 4px; height: 24px;">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; width: 144px; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="lblCommodity" runat="server" Font-Size="12px" Text="Commodity"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; width: 168px; position: static; background-color: #cfdcdc;">
                                <asp:DropDownList ID="ddl_commodity" runat="server" ValidationGroup="1" AutoPostBack="True" OnSelectedIndexChanged="ddl_commodity_SelectedIndexChanged" Width="166px">
                                </asp:DropDownList></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 207px;">
                                <asp:Label ID="lblScheme" runat="server" Font-Size="12px" Text="Scheme"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 275px;">
                                <asp:DropDownList ID="ddl_scheme" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_scheme_SelectedIndexChanged" Width="159px">
                                </asp:DropDownList></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                </td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 4px;">
                                </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; width: 144px; position: static; background-color: #cfdcdc">
                                <asp:Label ID="lblbalcomdty" runat="server" Font-Size="12px" Text="Balance Stock of Commodity"
                                    Width="144px"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; width: 168px; position: static; background-color: #cfdcdc">
                                <asp:TextBox ID="txtcomdty_bal" runat="server" BackColor="Linen" Font-Bold="False"
                                    ForeColor="Black" ReadOnly="True" Width="127px"></asp:TextBox>
                                <asp:Label ID="Label7" runat="server" Text="Qtls."></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 207px;">
                                <asp:Label ID="lblratetype" runat="server" Font-Size="12px" Text="Rate Type"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 275px;">
                                <asp:DropDownList ID="ddl_rate_type" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_rate_type_SelectedIndexChanged" Width="158px">
                                    <asp:ListItem Selected="True" Value="R">Rural</asp:ListItem>
                                    <asp:ListItem Value="U">Urban</asp:ListItem>
                                </asp:DropDownList></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                            </td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 4px;">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; width: 144px; position: static; background-color: #cfdcdc">
                                <asp:Label ID="lbl_alloc" runat="server" Text="Allocation for the Month"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; width: 168px; position: static; background-color: #cfdcdc">
                                <asp:TextBox ID="tx_allot_qty" runat="server" BackColor="Linen" MaxLength="8" ReadOnly="True"
                                    Width="123px"></asp:TextBox><asp:Label ID="lbl_qtalloc" runat="server" Text="Qtls."></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 207px;">
                                <asp:Label ID="lbl_issue" runat="server" Text="DO already Issued Qty." Width="128px"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 275px;">
                                <asp:TextBox ID="tx_already_iqty" runat="server" BackColor="Linen" MaxLength="8"
                                    ReadOnly="True" Width="90px"></asp:TextBox><asp:Label ID="lbl_qtissue" runat="server"
                                        Text="Qtls."></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                            </td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 4px;">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; width: 144px; position: static; background-color: #cfdcdc; height: 5px;">
                                <asp:Label ID="lbl_curbal" runat="server" Text="Current Balance Eligibility"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; width: 168px; position: static; background-color: #cfdcdc; height: 5px;">
                                <asp:TextBox ID="tx_balQty" runat="server" BackColor="Linen" MaxLength="8" ReadOnly="True"
                                    Width="122px"></asp:TextBox><asp:Label ID="lbl_qtcur" runat="server" Text="Qtls."></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 207px; height: 5px;">
                                <asp:Label ID="lbl_stock" runat="server" Text="Balance Stock of Commodity/Scheme" Width="128px"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 275px; height: 5px;">
                                <asp:TextBox ID="tx_bal_ic" runat="server" BackColor="Linen" MaxLength="8" ReadOnly="True"
                                    Width="90px"></asp:TextBox><asp:Label ID="lbl_qtstk" runat="server" Text="Qtls."></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 5px;">
                            </td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 4px; height: 5px;">
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="font-size: 10pt; background-color: maroon; color: white; position: static; height: 17px;" colspan="4">
                                <asp:Label ID="lbldodetails" runat="server" Font-Bold="True" Font-Size="12px" Text="Delivery Order Details"></asp:Label></td>
                            <td style="font-size: 10pt; background-color: #cfdcdc; position: static; height: 17px;">
                            </td>
                            <td align="left" style="font-size: 10pt; background-color: #cfdcdc; position: static; width: 4px; height: 17px;">
                            </td>
                        </tr>
                        <tr>
                     <%--       <td style="font-size: 10pt; width: 144px; position: static; background-color: #cfdcdc;" align="left">
                                <asp:Label ID="lbldono" runat="server" Font-Size="12px" Text="DO No."></asp:Label></td>
                            <td style="width: 168px; font-size: 10pt; position: static; background-color: #cfdcdc; color: red;" align="left">
                                <asp:TextBox ID="tx_do_no" runat="server" Width="93px" MaxLength="15"></asp:TextBox>*</td>--%>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 1px;" align="left">
                                <asp:Label ID="lbldodate" runat="server" Font-Size="12px" Text="DO Date"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 1px;" align="left">
                                <asp:TextBox ID="tx_do_date" runat="server" Width="119px"></asp:TextBox>
             <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_tx_do_date'
	    });
	     </script>
                                </td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 207px; height: 1px;" align="left">
                                <asp:Label ID="lblQuantity" runat="server" Font-Size="12px" ForeColor="Black" Text="DO Quantity"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 275px; height: 1px;" align="left">
                                &nbsp;<asp:TextBox ID="tx_qty" runat="server" MaxLength="13" Width="92px" OnTextChanged="tx_qty_TextChanged" AutoPostBack="True"></asp:TextBox><asp:Label
                                    ID="Label3" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red" Text="*" style="left: 0px"></asp:Label>
                                <asp:Label ID="Label6" runat="server" Text="Qtls."></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; width: 144px; position: static; background-color: #cfdcdc">
                                &nbsp;<asp:Label ID="lbldovalidity" runat="server" Font-Size="12px" Text="Validity Date"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; width: 168px; position: static; background-color: #cfdcdc">
                                &nbsp;<asp:TextBox ID="tx_do_validity" runat="server" Width="119px"></asp:TextBox>
                                <script type  ="text/javascript">
	                              new tcal ({ 'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_tx_do_validity'
	    });
	     </script>
                                </td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 207px;">
                                </td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 275px;">
             
                            </td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc">
                            </td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 4px;">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; width: 144px; position: static; height: 17px;
                                background-color: #cfdcdc">
                            </td>
                            <td align="left" style="font-size: 10pt; width: 168px; position: static; height: 17px;
                                background-color: #cfdcdc">
                            </td>
                            <td align="left" style="font-size: 10pt; width: 207px; position: static; height: 17px;
                                background-color: #cfdcdc">
                            </td>
                            <td align="left" style="font-size: 10pt; width: 275px; position: static; height: 17px;
                                background-color: #cfdcdc">
                            </td>
                            <td align="left" style="font-size: 10pt; position: static; height: 17px; background-color: #cfdcdc">
                            </td>
                            <td align="left" style="font-size: 10pt; width: 4px; position: static; height: 17px;
                                background-color: #cfdcdc">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; width: 144px; position: static; height: 17px;
                                background-color: #cfdcdc">
                            </td>
                            <td align="left" style="font-size: 10pt; width: 168px; position: static; height: 17px;
                                background-color: #cfdcdc">
                            </td>
                            <td align="left" style="font-size: 10pt; width: 207px; position: static; height: 17px;
                                background-color: #cfdcdc">
                            </td>
                            <td align="left" style="font-size: 10pt; width: 275px; position: static; height: 17px;
                                background-color: #cfdcdc">
                            </td>
                            <td align="left" style="font-size: 10pt; position: static; height: 17px; background-color: #cfdcdc">
                            </td>
                            <td align="left" style="font-size: 10pt; width: 4px; position: static; height: 17px;
                                background-color: #cfdcdc">
                            </td>
                        </tr>
                    </table>
                </fieldset>
                
                </td>
            </tr>
            
            <tr>
            <td>
                <fieldset style="vertical-align: top; text-align: left; height: 180px;" >
                <legend>
                    <asp:Label ID="Label9" runat="server" Text="Payment Details" BackColor="#8080FF" Width="109px"></asp:Label>&nbsp;</legend>
                    <table style="width: 701px; background-color: lightsteelblue;" border="1" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                </td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                </td>
                            <td align="left" colspan="2" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 36px;">
                                </td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 249px;">
                                &nbsp;&nbsp;
                            </td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8">
                                <asp:Label ID="lblRateQuintal" runat="server" Font-Size="12px" Text="Rate/Quintal"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8">
                                <asp:TextBox ID="tx_rate_qt" runat="server"
                                    MaxLength="8" Width="100px" BackColor="Linen"></asp:TextBox></td>
                            <td align="left" colspan="2" style="font-size: 10pt; width: 36px; position: static;
                                background-color: #cfdcc8">
                                <asp:Label ID="lblPaymentMode" runat="server" Font-Size="12px" Text="Payment Mode"
                                    Width="92px"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; width: 249px; position: static; background-color: #cfdcc8">
                                <asp:DropDownList ID="ddl_pmode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_pmode_SelectedIndexChanged" Width="224px">
                                <asp:ListItem Value="Se">--Select--</asp:ListItem>
                                <asp:ListItem Value="D">DD / Cheque</asp:ListItem>
                                <asp:ListItem Value="R">Free Scheme / Credit </asp:ListItem>
                                <asp:ListItem Value="A">Cash</asp:ListItem>
                                 <asp:ListItem Value="AD">Advance Payment</asp:ListItem>
                                  <asp:ListItem Value="OP">Online Payment</asp:ListItem>
                            </asp:DropDownList></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8">
                                <asp:Label ID="lbltotamt" runat="server" Font-Size="12px" Text="Total Amount" Width="77px"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8">
                                <asp:TextBox ID="tx_tot_amt" runat="server" BackColor="Linen" MaxLength="8" ReadOnly="True"
                                    Width="100px"></asp:TextBox></td>
                            <td align="left" colspan="2" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 36px;">
                                <asp:Label ID="lblddchekno" runat="server" Font-Size="12px" Text="DD/Chq.No. " Width="111px"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 249px;">
                                <asp:TextBox ID="tx_dd_no" runat="server" Width="120px" MaxLength="50"></asp:TextBox><asp:Label ID="lbl_ddno" runat="server" Font-Bold="True" Font-Size="Medium" Text="*" ForeColor="Red"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8">
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 26px;" align="left">
                                <asp:Label ID="lblBankName" runat="server" Font-Size="12px" Text="Bank Name"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 26px;" align="left">
                                <asp:DropDownList ID="ddl_bank" runat="server" Width="225px" >
                                </asp:DropDownList></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 26px; width: 36px;" colspan="2" align="left">
                                <asp:Label ID="lblddchekdate" runat="server" Font-Size="12px" Text="Payment Date" Width="104px"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 26px; width: 249px;" align="left">
                                 <asp:TextBox ID="tx_dd_date" runat="server" Width="95px"></asp:TextBox>
             <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_tx_dd_date'
	    });
	     </script>
                                </td>
                            <td style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 26px;" align="left">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 26px;">
                                <asp:Label ID="lblamount" runat="server" Font-Size="12px" Text="Amount"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 26px;">
                                <asp:TextBox ID="tx_dd_amount" runat="server" Width="125px" MaxLength="12"></asp:TextBox>
                                <asp:Label ID="lbl_amt" runat="server" Text="*" style="z-index: 100" ForeColor="Red"></asp:Label>
                                <asp:Label ID="Label2" runat="server" ForeColor="Black" Text="Rs."></asp:Label></td>
                            <td align="left" colspan="3" style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 26px;">
                             <asp:Label ID="LbldisplayDo" runat="server" Text = "DO Number is :- " Font-Bold="True" ForeColor="#0000C0" Width="101px" Visible="False"></asp:Label>
                                <asp:Label ID="LblShowDonum" runat="server" Font-Bold="True" ForeColor="#C00000" Width="241px" Visible="False"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 26px;">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #ffffcc;">
                                </td>
                            <td align="left" colspan="3" style="font-size: 10pt; position: static; background-color: #ffffcc;">
                                </td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #ffffcc;">
                                </td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #ffffcc;">
                                &nbsp;&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                            </td>
                            <td align="right" colspan="3" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                <asp:Button ID="save" runat="server" Text="Save" Font-Bold="False" Font-Size="Medium" Width="104px" OnClick="save_Click" ValidationGroup="1" OnClientClick="return CheckIsRepeat();" />
                                <asp:Button ID="btnClose" runat="server" Font-Size="Medium" OnClick="btnClose_Click"
                                    Text="Close" Width="90px" />
                                <asp:Button ID="btn_new" runat="server" Font-Bold="False" Font-Size="Medium" OnClick="btn_new_Click"
                                    Text="New" Width="75px" /></td>
                            <td align="left" colspan="2" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                &nbsp;</td>
                        </tr>
                    </table>
                    </fieldset>
                </td>
            </tr>
        </table>
        </asp:Panel>
        <span>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Size="Small" ShowMessageBox="True"
                        ShowSummary="False" Style="left: 0px; top: 0px" ValidationGroup="1" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tx_qty"
                        ErrorMessage="Please enter quantity" Height="0px" ValidationGroup="1" Width="0px">*</asp:RequiredFieldValidator>
                        <%--<asp:RequiredFieldValidator  ID="RequiredFieldValidator1" runat="server" ControlToValidate="tx_do_no" ErrorMessage="Please enter delivery order no."  Height="0px" ValidationGroup="1" Width="0px">*</asp:RequiredFieldValidator>--%>
                                    <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator3" runat="server" ControlToValidate="tx_dd_no" ErrorMessage="Please enter DD No."
                                    Height="0px" ValidationGroup="1" Width="0px">*</asp:RequiredFieldValidator><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator4" runat="server" ControlToValidate="tx_dd_amount"
                                    ErrorMessage="Please enter DD Amount" Height="0px" ValidationGroup="1" Width="0px">*</asp:RequiredFieldValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddl_commodity"
                        ErrorMessage="Please enter commodity" Height="0px" ValidationGroup="1" Width="0px">*</asp:RequiredFieldValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tx_rate_qt"
                        ErrorMessage="Please enter Rate" Height="0px" ValidationGroup="1"
                        Width="0px">*</asp:RequiredFieldValidator>
        <asp:HyperLink id="hlinkpdo" runat="server" NavigateUrl="#" Visible="False">Issue Delivery Order</asp:HyperLink></span></div>

</asp:Content>