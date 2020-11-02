<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="DO_SaleOthers.aspx.cs" Inherits="District_DO_SaleOthers" Title="Untitled Page" %>
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
 
 
  <table style="width: 717px">
        <tr><td align="center" style="background-color: #ccffff; height: 21px;">
            <asp:Label ID="lbldo" runat="server" Font-Bold="True" ForeColor="#FF3366" Text="Delivery Order Paddy to Miller and Open Sales"
                Width="384px"></asp:Label></td></tr>
            <tr>
                <td align="left" style="height: 200px">
                
                
                    <asp:Label ID="Label8" runat="server" Text="Create DO for sale of Damage and Sweepage Commodity and Paddy to Miller Only" Width="686px" Font-Bold="True" ForeColor="#663366"></asp:Label>
                    <table style="width: 725px; background-color: skyblue; height: 187px;" border="1" cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="6" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 1px;" align="left">
                                <asp:Label ID="Label1" runat="server" ForeColor="Blue" Font-Size="Medium"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="6" style="font-size: 10pt; position: static; height: 1px;
                                background-color: #ffffff">
                                <strong><span style="color: #ff0033">कृपया ध्यान रखे की अब Deivery Order का नंबर कंप्यूटर
                                    से जनरेट होगा, अतः D.O. Number &nbsp;प्रविष्ठी का विकल्प हटा दिया गया है</span></strong></td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #ffff99; height: 16px;">
                                <asp:Label ID="lbltoissue" runat="server" Font-Size="12px" Text="Issue To" Width="86px"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #ffff99; height: 16px;">
                                <asp:DropDownList ID="ddl_issueto" runat="server"  Width="178px" OnSelectedIndexChanged="ddl_issueto_SelectedIndexChanged" AutoPostBack="True">
                               
                            </asp:DropDownList></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #ffff99; height: 16px;">
                                Crop Year</td>
                            <td align="left" colspan="3" style="font-size: 10pt; position: static; background-color: #ffff99; height: 16px;">
                                <asp:DropDownList ID="ddlfinancialyear" runat="server" Width="154px" 
                                    AutoPostBack="True" 
                                    onselectedindexchanged="ddlfinancialyear_SelectedIndexChanged">
                                    <asp:ListItem>--select---</asp:ListItem>
                                    <asp:ListItem Value="10">2015-2016</asp:ListItem>
                                <asp:ListItem Value="01" >2014-2015</asp:ListItem>
                                                <asp:ListItem Value="02">2013-2014</asp:ListItem>
                                                <asp:ListItem Value="03">2012-2013</asp:ListItem>
                                                <asp:ListItem Value="04">2011-2012</asp:ListItem>
                                                <asp:ListItem Value="05">2010-2011</asp:ListItem>
                                                <asp:ListItem Value="06">2009-2010</asp:ListItem>
                                                <asp:ListItem Value="07">2008-2009</asp:ListItem>
                                                <asp:ListItem Value="08">2007-2008</asp:ListItem>
                                                <asp:ListItem Value="09">2006-2007</asp:ListItem>
                                
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #ffff99; height: 16px;">
                                <asp:Label ID="lbl_lead" runat="server" Font-Size="12px" Text="Party Name" Width="108px"></asp:Label></td>
                            <td align="left" 
                                style="font-size: 10pt; position: static; background-color: #ffff99; height: 16px;" 
                                colspan="2">
                                <asp:DropDownList ID="ddl_party" runat="server" Font-Size="Medium"  
                                    Width="300px" style="font-size: small">
                                </asp:DropDownList></td>
                            <td align="left" colspan="3" style="font-size: 10pt; position: static; background-color: #ffff99; height: 16px;">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #ffff99; height: 13px;">
                                <asp:Label ID="lblCommodity" runat="server" Font-Size="12px" Text="Commodity"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #ffff99; height: 13px;">
                                <asp:DropDownList ID="ddl_commodity" runat="server" ValidationGroup="1" AutoPostBack="True"  Width="170px">
                                </asp:DropDownList></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #ffff99; height: 13px;">
                                <asp:Label ID="lblScheme" runat="server" Font-Size="12px" Text="Scheme"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #ffff99; height: 13px;" colspan="2">
                                <asp:DropDownList ID="ddl_scheme" runat="server" AutoPostBack="True"  Width="215px" OnSelectedIndexChanged="ddl_scheme_SelectedIndexChanged">
                                </asp:DropDownList></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #ffff99; height: 13px;">
                                </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; width: 144px; position: static; background-color: #cfdcdc; height: 2px;">
                                Available QTY</td>
                            <td align="left" style="font-size: 10pt; width: 168px; position: static; background-color: #cfdcdc; height: 2px;">
                                <asp:TextBox ID="txtdamqty" runat="server" Enabled="False" Width="175px"></asp:TextBox></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 2px;" colspan="2">
                                </td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 2px; width: 109px;">
                                </td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; width: 4px; height: 2px;">
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="font-size: 10pt; background-color: lightslategray; color: white; position: static; height: 17px;" colspan="4">
                                <asp:Label ID="lbldodetails" runat="server" Font-Bold="True" Font-Size="12px" Text="Delivery Order Details"></asp:Label></td>
                            <td style="font-size: 10pt; background-color: #cfdcdc; position: static; height: 17px; width: 109px;">
                            </td>
                            <td align="left" style="font-size: 10pt; background-color: #cfdcdc; position: static; width: 4px; height: 17px;">
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; position: static; background-color: #ffff99; height: 5px;" align="left">
                                <asp:Label ID="lbldono" runat="server" Font-Size="12px" Text="DO No." Visible = "false"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #ffff99; height: 5px;" align="left">
                                <asp:TextBox ID="tx_do_no" runat="server" Width="154px" MaxLength="15" Visible = "false"></asp:TextBox>*</td>
                            <td style="font-size: 10pt; position: static; background-color: #ffff99; height: 5px;" align="left">
                                <asp:Label ID="lbldodate" runat="server" Font-Size="12px" Text="DO Date"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #ffff99; height: 5px;" align="left" colspan="3">
                                <asp:TextBox ID="tx_do_date" runat="server" Width="138px"></asp:TextBox>
             <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_tx_do_date'
	    });
	     </script>

                                </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; width: 144px; position: static; background-color: #ffff99; height: 1px;">
                                <asp:Label ID="lblQuantity" runat="server" Font-Size="12px" ForeColor="Black" Text="DO Quantity"></asp:Label>
                                </td>
                            <td align="left" style="font-size: 10pt; width: 144px; position: static; background-color: #ffff99; height: 1px;">
                                <asp:TextBox ID="tx_qty" runat="server" MaxLength="13" Width="92px" ></asp:TextBox><asp:Label
                                    ID="Label3" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red" Text="*" style="left: 0px"></asp:Label>
                                <asp:Label ID="Label6" runat="server" Text="Qtls."></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #ffff99; height: 1px; width: 144px;">
                                <asp:Label ID="lbldovalidity" runat="server" Font-Size="12px" Text="Validity Date"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #ffff99; height: 1px;" colspan="3">
                                <asp:TextBox ID="tx_do_validity" runat="server" Width="140px"></asp:TextBox>
             <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_tx_do_validity'
	    });
	     </script>
                            </td>
                        </tr>
                    </table>
               
                
                </td>
            </tr>
            
            <tr>
            <td>
                <center>
                    <asp:Label ID="Label9" runat="server" Text="Payment Details"></asp:Label>
                    <table style="width: 714px; background-color: lightsteelblue; height: 182px;" border="1" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #99cc99; width: 120px; height: 1px;">
                                <asp:Label ID="lblRateQuintal" runat="server" Font-Size="12px" Text="Rate/Quintal"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #99cc99; width: 221px; height: 1px;">
                                <asp:TextBox ID="tx_rate_qt" runat="server"
                                    MaxLength="8" Width="151px" BackColor="Linen" OnTextChanged="tx_rate_qt_TextChanged" AutoPostBack="True"></asp:TextBox></td>
                            <td align="left" colspan="2" style="font-size: 10pt; position: static; background-color: #99cc99; width: 115px; height: 1px;">
                                <asp:Label ID="lblPaymentMode" runat="server" Font-Size="12px" Text="Payment Mode" Width="92px"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #99cc99; height: 1px;" colspan="2">
                                &nbsp;&nbsp;<asp:DropDownList ID="ddl_pmode" runat="server" AutoPostBack="True"  Width="203px" OnSelectedIndexChanged="ddl_pmode_SelectedIndexChanged">
                               
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                               
                                <asp:ListItem Value="D">DD/Cheque</asp:ListItem>
                                <asp:ListItem Value="R">Free Scheme / Credit </asp:ListItem>
                                <asp:ListItem Value="A">Cash</asp:ListItem>
                                <asp:ListItem Value="AD">Advance Payment</asp:ListItem>
                                <asp:ListItem Value="OP">Online Payment</asp:ListItem>
                                
                            </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; width: 120px; position: static; background-color: #99cc99; height: 1px;">
                                <asp:Label ID="lbltotamt" runat="server" Font-Size="12px" Text="Total Amount" Width="88px"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; width: 221px; position: static; background-color: #99cc99; height: 1px;">
                              
                            <asp:TextBox ID="tx_tot_amt" runat="server" BackColor="Linen" MaxLength="8" ReadOnly="True"
                                    Width="150px"></asp:TextBox></td>
                            <td align="left" colspan="2" style="font-size: 10pt; width: 115px; position: static;
                                background-color: #99cc99; height: 1px;">
                                <asp:Label ID="lblddchekno" runat="server" Font-Size="12px" Text="DD/Chq.No. " Width="95px"></asp:Label></td>
                            <td align="left" colspan="2" style="font-size: 10pt; position: static; background-color: #99cc99; height: 1px;">
                                <asp:TextBox ID="tx_dd_no" runat="server" Width="120px" MaxLength="50"></asp:TextBox>
                                <asp:Label ID="lbl_ddno" runat="server" Font-Bold="True" Font-Size="Medium" Text="*" ForeColor="Red"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; position: static; background-color: #99cc99; height: 1px; width: 120px;" align="left">
                                <asp:Label ID="lblBankName" runat="server" Font-Size="12px" Text="Bank Name"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #99cc99; height: 1px; width: 221px;" align="left">
                                <asp:DropDownList ID="ddl_bank" runat="server" Width="205px" >
                                </asp:DropDownList></td>
                            <td style="font-size: 10pt; position: static; background-color: #99cc99; height: 1px; width: 115px;" colspan="2" align="left">
                                <asp:Label ID="lblddchekdate" runat="server" Font-Size="12px" Text="Payment Date" Width="94px"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #99cc99; height: 1px;" align="left" colspan="2">
                                 <asp:TextBox ID="tx_dd_date" runat="server" Width="108px"></asp:TextBox>
             <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_tx_dd_date'
	    });
	     </script>

                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cc9999; width: 120px;">
                                <asp:Label ID="lblamount" runat="server" Font-Size="12px" Text="Amount"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cc9999; width: 221px;">
                                <asp:TextBox ID="tx_dd_amount" runat="server" Width="125px" MaxLength="12"></asp:TextBox><asp:Label
                                    ID="lbl_amt" runat="server" Text="*" style="z-index: 100" ForeColor="Red"></asp:Label>
                                <asp:Label
                                    ID="Label2" runat="server" ForeColor="Black" Text="Rs." Width="27px"></asp:Label></td>
                            <td align="left" colspan="4" style="font-size: 10pt; position: static; background-color: #cc9999;">
                                <asp:Label ID="LbldisplayDo" runat="server" Font-Bold="True" ForeColor="Black" Text="DO Number is :- "
                                    Visible="False" Width="101px"></asp:Label><asp:Label ID="LblShowDonum" runat="server"
                                        Font-Bold="True" ForeColor="Black" Visible="False" Width="241px"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8; width: 120px;">
                                </td>
                            <td align="left" colspan="3" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                &nbsp;</td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;" colspan="2">
                                &nbsp;&nbsp;<asp:HyperLink ID="hyp_printdo" runat="server">Print do</asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cc99ff; width: 120px;">
                            </td>
                            <td align="right" colspan="3" style="font-size: 10pt; position: static; background-color: #cc99ff;">
                                <asp:Button ID="save" runat="server" Text="Save" Font-Bold="False" Font-Size="Medium" Width="104px"  ValidationGroup="1" OnClick="save_Click" />
                                <asp:Button ID="btnClose" runat="server" Font-Size="Medium" 
                                    Text="Close" Width="90px" OnClick="btnClose_Click" />
                                <asp:Button ID="btn_new" runat="server" Font-Bold="False" Font-Size="Medium" 
                                    Text="New" Width="75px" OnClick="btn_new_Click" /></td>
                            <td align="left" colspan="2" style="font-size: 10pt; position: static; background-color: #cc99ff;">
                                &nbsp;</td>
                        </tr>
                    </table>
               </center>    
                </td>
            </tr>
        </table>

</asp:Content>

