<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="DeliveryOrder_other.aspx.cs" Inherits="IssueCenter_DeliveryOrder_other" Title="Delivery Order" Debug="true"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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
	
    <div style="text-align: right">
    <asp:Panel ID="panelDO" runat ="server"  >
        <table style="border-right: navy 1px solid; border-top: navy 1px solid; border-left: navy 1px solid; width: 610px; border-bottom: navy 1px solid">
        <tr><td align="center" style="background-color: #cccccc">
            <asp:Label ID="lbldo" runat="server" Font-Bold="True" ForeColor="Transparent" Text="Delivery Order"
                Width="108px"></asp:Label></td></tr>
            <tr>
                <td align="left">
                <fieldset style="vertical-align: top; text-align: left; width: 610px;" >
                <legend>
                Permit / Release Order Details
                </legend>
                    <table style="width: 616px; background-color: skyblue;" border="1" cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="4" style="font-size: 10pt; width: 144px; position: static; background-color: #cfdcdc;" align="left">
                                <asp:Label ID="Label1" runat="server" ForeColor="Blue" Font-Size="Medium"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; width: 144px; position: static; background-color: #cfdcdc;">
                                Issue To</td>
                            <td align="left" style="font-size: 10pt; position: static; width: 144px; background-color: #cfdcdc;">
                                <asp:DropDownList ID="ddl_issueto" runat="server" OnSelectedIndexChanged="ddl_issueto_SelectedIndexChanged">
                                <asp:ListItem Value="O">Others</asp:ListItem>
                            </asp:DropDownList></td>
                            <td align="left" style="font-size: 10pt; width: 144px; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="lbl_lead" runat="server" Text="Name" Font-Size="Small" Width="40px"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; width: 144px; position: static; background-color: #cfdcdc;">
                                <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Width="178px" MaxLength="130"></asp:TextBox>*</td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; width: 144px; position: static; background-color: #cfdcdc;">
                                </td>
                            <td align="left" style="font-size: 10pt; width: 144px; position: static; background-color: #cfdcdc;">
                                </td>
                            <td align="left" style="font-size: 10pt; width: 144px; position: static; background-color: #cfdcdc;">
                                </td>
                            <td align="left" style="font-size: 10pt; width: 144px; position: static; background-color: #cfdcdc;">
                                </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; width: 144px; position: static; background-color: #cfdcdc">
                                <asp:Label ID="lblallotyear" runat="server" Font-Size="12px" Text="Allot. Year"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; width: 144px; position: static; background-color: #cfdcdc">
                                <asp:DropDownList ID="ddd_allot_year" runat="server">
                                </asp:DropDownList></td>
                            <td align="left" style="font-size: 10pt; width: 144px; position: static; background-color: #cfdcdc">
                                <asp:Label ID="lblallotmonth" runat="server" Font-Size="12px" Text="Allot. Month"
                                    Width="67px"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; width: 144px; position: static; background-color: #cfdcdc">
                                <asp:DropDownList ID="ddl_allot_month" runat="server">
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
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; width: 144px; position: static; background-color: #cfdcdc">
                                <asp:Label ID="lblCommodity" runat="server" Font-Size="12px" Text="Commodity"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; width: 144px; position: static; background-color: #cfdcdc">
                                <asp:DropDownList ID="ddl_commodity" runat="server" ValidationGroup="1" AutoPostBack="True" OnSelectedIndexChanged="ddl_commodity_SelectedIndexChanged" >
                                </asp:DropDownList></td>
                            <td align="left" style="font-size: 10pt; width: 144px; position: static; background-color: #cfdcdc">
                                <asp:Label ID="lblScheme" runat="server" Font-Size="12px" Text="Scheme"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; width: 144px; position: static; background-color: #cfdcdc">
                                <asp:DropDownList ID="ddl_scheme" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_scheme_SelectedIndexChanged" Width="193px">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; width: 144px; position: static; background-color: #cfdcdc">
                                <asp:Label ID="lblbalcomdty" runat="server" Font-Size="12px" Text="Balance Stock of Commodity"
                                    Width="144px"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; width: 144px; position: static; background-color: #cfdcdc">
                                <asp:TextBox ID="txtcomdty_bal" runat="server" BackColor="Linen" Font-Bold="False"
                                    ForeColor="Black" ReadOnly="True" Width="117px"></asp:TextBox>Qtls.</td>
                            <td align="left" style="font-size: 10pt; width: 144px; position: static; background-color: #cfdcdc">
                            </td>
                            <td align="left" style="font-size: 10pt; width: 144px; position: static; background-color: #cfdcdc">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; width: 144px; position: static; background-color: #cfdcdc">
                                <asp:Label ID="lbl_stock" runat="server" Text="Bal. Stock of commodity/Scheme" Width="176px"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; width: 144px; position: static; background-color: #cfdcdc">
                                <asp:TextBox ID="tx_bal_ic" runat="server" BackColor="Linen" MaxLength="8" ReadOnly="True"
                                    Width="116px"></asp:TextBox>Qtls.</td>
                            <td align="left" style="font-size: 10pt; width: 144px; position: static; background-color: #cfdcdc">
                                <asp:Label ID="lblratetype" runat="server" Font-Size="12px" Text="Rate Type"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; width: 144px; position: static; background-color: #cfdcdc">
                                <asp:DropDownList ID="ddl_rate_type" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_rate_type_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Value="R">Rural</asp:ListItem>
                                    <asp:ListItem Value="U">Urban</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td align="center" style="font-size: 12pt; height: 5px; background-color: lightslategray;" colspan="4">
                                <asp:Label ID="lbldodetails" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                    Text="Delivery Order Details"></asp:Label></td>
                        </tr>
                        <tr>
                       <%--     <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                <asp:Label ID="lbldono" runat="server" Font-Size="12px" Text="DO No."></asp:Label></td>
                            <td align="left" style="font-size: 10pt; color: red; position: static; background-color: #cfdcc8;">
                                <asp:TextBox ID="tx_do_no" runat="server" MaxLength="15" Width="102px"></asp:TextBox>*</td>--%>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                <asp:Label ID="lbldodate" runat="server" Font-Size="12px" Text="DO Date"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                <asp:TextBox ID="tx_do_date" runat="server" Width="119px"></asp:TextBox>
             <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_tx_do_date'
	    });
	     </script>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8">
                                <asp:Label ID="lblQuantity" runat="server" Font-Size="12px" ForeColor="Black" Text="DO Quantity"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8">
                                <asp:TextBox ID="tx_qty" runat="server" MaxLength="13" Width="100px" AutoPostBack="True" OnTextChanged="tx_qty_TextChanged"></asp:TextBox><asp:Label
                                    ID="Label3" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red" Text="*" style="left: 0px"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8">
                                <asp:Label ID="lbldovalidity" runat="server" Font-Size="12px" Text="Validity Date"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8">
                                <asp:TextBox ID="tx_do_validity" runat="server" Width="119px"></asp:TextBox>
             <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_tx_do_validity'
	    });
	     </script>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                
                </td>
            </tr>
            
            <tr>
            <td style="height: 200px" align="left">
                <fieldset style="vertical-align: top; text-align: left; width: 587px;" >
                <legend>
                Payment Details
                </legend>
                    <table style="width: 622px; background-color: lightsteelblue;" border="1" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left" style="font-size: 10pt; width: 97px; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="lblRateQuintal" runat="server" Font-Size="12px" Text="Rate/Quintal"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; width: 108px; position: static; background-color: #cfdcdc;">
                                <asp:TextBox ID="tx_rate_qt" runat="server"
                                    MaxLength="8" Width="90px" ReadOnly="True" BackColor="Linen"></asp:TextBox></td>
                            <td align="left" colspan="2" style="font-size: 10pt; width: 107px; position: static; background-color: #cfdcdc;">
                                &nbsp;
                                <asp:Label ID="lblPaymentMode" runat="server" Font-Size="12px" Text="Payment Mode"
                                    Width="92px"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; width: 144px; position: static; background-color: #cfdcdc;">
                                &nbsp;<asp:DropDownList ID="ddl_pmode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_pmode_SelectedIndexChanged" Width="214px">
                                <asp:ListItem Value="Se">--Select--</asp:ListItem>
                                <asp:ListItem Value="D">DD / Cheque</asp:ListItem>
                                <asp:ListItem Value="R">Free Scheme / Credit </asp:ListItem>
                                <asp:ListItem Value="A">Cash</asp:ListItem>
                                 <asp:ListItem Value="AD">Advance Payment</asp:ListItem>
                                  <asp:ListItem Value="OP">Online Payment</asp:ListItem>
                            </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; width: 97px; position: static; background-color: #cfdcdc;" align="left">
                                <asp:Label ID="lbltotamt" runat="server" Font-Size="12px" Text="Total Amount"></asp:Label></td>
                            <td style="font-size: 10pt; width: 108px; position: static; background-color: #cfdcdc;" align="left">
                                &nbsp;<asp:TextBox ID="tx_tot_amt" runat="server" BackColor="Linen" MaxLength="8" ReadOnly="True"
                                    Width="100px"></asp:TextBox>Rs.</td>
                            <td style="font-size: 10pt; width: 107px; position: static; background-color: #cfdcdc;" colspan="2" align="left">
                                <asp:Label ID="lblddchekno" runat="server" Font-Size="12px" Text="DD/Chq. No. "></asp:Label>
                            </td>
                            <td style="font-size: 10pt; width: 144px; position: static; background-color: #cfdcdc;" align="left">
                                <asp:TextBox ID="tx_dd_no" runat="server" Width="106px" MaxLength="50"></asp:TextBox><asp:Label ID="lbl_ddno" runat="server" Font-Bold="True" Font-Size="Medium" Text="*" ForeColor="Red"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; width: 97px; position: static; background-color: #cfdcdc">
                                <asp:Label ID="lblBankName" runat="server" Font-Size="12px" Text="Bank Name"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; width: 108px; position: static; background-color: #cfdcdc">
                                <asp:DropDownList ID="ddl_bank" runat="server" >
                                </asp:DropDownList></td>
                            <td align="left" colspan="2" style="font-size: 10pt; width: 107px; position: static;
                                background-color: #cfdcdc">
                                <asp:Label ID="lblddchekdate" runat="server" Font-Size="12px" Text="DD/Chq. Date"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; width: 144px; position: static; background-color: #cfdcdc">
                               <asp:TextBox ID="tx_dd_date" runat="server" Width="119px"></asp:TextBox>
             <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_tx_dd_date'
	    });
	     </script>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; width: 97px; position: static; background-color: #cfdcdc;">
                                <asp:Label ID="lblamount" runat="server" Font-Size="12px" Text="Amount"></asp:Label></td>
                            <td align="left" colspan="4" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:TextBox ID="tx_dd_amount" runat="server" Width="112px" MaxLength="12"></asp:TextBox><asp:Label
                                    ID="lbl_amt" runat="server" Text="*" style="z-index: 100" ForeColor="Red"></asp:Label>
                                <asp:Label
                                    ID="Label2" runat="server" ForeColor="Black" Text="Rs."></asp:Label>&nbsp;<asp:Label
                                        ID="LbldisplayDo" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="DO Number is :- "
                                        Visible="False" Width="101px"></asp:Label><asp:Label ID="LblShowDonum" runat="server"
                                            Font-Bold="True" ForeColor="#C00000" Visible="False" Width="132px"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; width: 97px; position: static; background-color: #cfdcdc;">
                            </td>
                            <td align="right" colspan="3" style="font-size: 10pt; position: static; background-color: #cfdcdc;">
                                <asp:Button ID="save" runat="server" Text="Save" Font-Bold="False" Font-Size="Medium" Width="104px" OnClick="save_Click" ValidationGroup="1" />
                                <asp:Button ID="btnClose" runat="server" Font-Size="Medium" OnClick="btnClose_Click"
                                    Text="Close" Width="90px" />
                                <asp:Button ID="btn_new" runat="server" Font-Bold="False" Font-Size="Medium" OnClick="btn_new_Click"
                                    Text="New" Width="75px" /></td>
                            <td align="left" colspan="1" style="font-size: 10pt; width: 144px; position: static; background-color: #cfdcdc;">
                                &nbsp;</td>
                        </tr>
                    </table>
                    </fieldset>
                </td>
            </tr>
        </table>
        </asp:Panel>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextBox1"
            ErrorMessage="Please enter name of IssueTo" Height="0px" ValidationGroup="1"
            Width="0px">*</asp:RequiredFieldValidator>
        <span>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Size="Small" ShowMessageBox="True"
                        ShowSummary="False" Style="left: 0px; top: 0px" ValidationGroup="1" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tx_qty"
                        ErrorMessage="Please enter quantity" Height="0px" ValidationGroup="1" Width="0px">*</asp:RequiredFieldValidator>
                     <%--   <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="tx_do_no" ErrorMessage="Please enter delivery order no."
                                    Height="0px" ValidationGroup="1" Width="0px">*</asp:RequiredFieldValidator>--%>
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