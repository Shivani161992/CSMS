<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true"
    CodeFile="DeliveryOrder_PDS.aspx.cs" Inherits="IssueCenter_DeliveryOrder_PDS"
    Title="Delivery Order PDS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
 function OpenWin() {
    window.showModalDialog('FRMViewAddedFPS_Delievry.aspx');
    return false;
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

    <div style="background-color: #cfdcc8;">
        <asp:Panel ID="panelDO" runat="server">
            <table style="width: 625px; border-color: Silver;" cellpadding="0" cellspacing="0"
                border="1">
                <tr style="height: 25px">
                    <td align="center" style="background-color: Navy;" valign="middle" colspan="4">
                        <asp:Label ID="lbldo" runat="server" Font-Bold="True" ForeColor="whiteSmoke" Text="Delivery Order Only For FPS"
                            Font-Size="10pt"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="left">
                        <asp:Label ID="lbl_District" runat="server" Text="District - " Font-Size="10pt" ForeColor="#C00000"></asp:Label>
                        <asp:Label ID="lblDist" runat="server" Font-Size="10pt" ForeColor="#C00000"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="left">
                        <asp:Label ID="lbl_Msg" runat="server" ForeColor="Blue" Font-Size="10pt"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblpermitno1" runat="server" Font-Size="10pt" Text="Permit No."></asp:Label>
                    </td>
                    <td align="left" >
                        <asp:DropDownList ID="ddlpermit" runat="server" Width="145px" OnSelectedIndexChanged="ddlpermit_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlpermit"
                            ErrorMessage="Select Permit No. Not Indicated (if Not Available)" ValidationGroup="1" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                    </td>
                    <td align="left" >
                        <asp:Label ID="lbltoissue" runat="server" Font-Size="10pt" Text="Issue To"></asp:Label></td>
                    <td align="left">
                        <asp:DropDownList ID="ddl_issueto" runat="server" Width="145px" Enabled="False">
                            <asp:ListItem Value="F" Text="Only FPS"></asp:ListItem>
                            
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblpermitno" runat="server" Font-Size="10pt" Text="Permit No."></asp:Label></td>
                    <td align="left">
                        <asp:TextBox ID="tx_permit_no" runat="server" Width="140px" MaxLength="15" ></asp:TextBox></td>
                    <td align="left">
                        <asp:Label ID="lblpermitdate" runat="server" Font-Size="10pt" Text="PermitDate"></asp:Label></td>
                    <td align="left">
                        <asp:TextBox ID="tx_permit_date" runat="server" Width="100px"></asp:TextBox>

                        <script type="text/javascript">
	                       new tcal ({
				             'formname': '0',
				           'controlname': 'ctl00_ContentPlaceHolder1_tx_permit_date'
	                           });
                        </script>

                        </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblallotmonth" runat="server" Font-Size="10pt" Text="Allot. Month"></asp:Label></td>
                    <td align="left">
                        <asp:DropDownList ID="ddl_allot_month" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_allot_month_SelectedIndexChanged"
                            Width="145px">
                            
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
                    <td align="left">
                        <asp:Label ID="lblallotyear" runat="server" Font-Size="10pt" Text="Allot. Year"></asp:Label></td>
                    <td align="left">
                        <asp:DropDownList ID="ddd_allot_year" Width="145px" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="ddd_allot_year_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblCommodity" runat="server" Font-Size="10pt" Text="Commodity"></asp:Label></td>
                    <td align="left">
                        <asp:DropDownList ID="ddl_commodity" runat="server" ValidationGroup="1" AutoPostBack="True"
                            OnSelectedIndexChanged="ddl_commodity_SelectedIndexChanged" Width="145px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddl_commodity"
                            ErrorMessage="Please Select commodity" ValidationGroup="2" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblScheme" runat="server" Font-Size="10pt" Text="Scheme"></asp:Label></td>
                    <td align="left">
                        <asp:DropDownList ID="ddl_scheme" runat="server" AutoPostBack="True" Width="145px"
                            OnSelectedIndexChanged="ddl_scheme_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddl_scheme"
                            ErrorMessage="Please Select Scheme" ValidationGroup="2" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                        </td>
                </tr>
                 <tr style="height: 25px; background-color: Navy">
                    <td align="center" colspan="4">
                        <asp:Label ID="lbl_Avail_Head" runat="server" Font-Bold="True" Font-Size="10pt" Text="Available Balance Details"
                            ForeColor="whitesmoke"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblbalcomdty" runat="server" Font-Size="10pt" Text="Balance Quantity"></asp:Label></td>
                    <td align="left">
                        <asp:TextBox ID="txtcomdty_bal" runat="server" BackColor="MenuBar" ForeColor="Black"
                            ReadOnly="True" Width="100px"></asp:TextBox>Qtls.</td>
                    <td align="left">
                        <asp:Label ID="lblratetype" runat="server" Font-Size="10pt" Text="Rate Type"></asp:Label></td>
                    <td align="left">
                        <asp:DropDownList  ID="ddl_rate_type" runat="server" AutoPostBack="True" Width="105px"
                            OnSelectedIndexChanged="ddl_rate_type_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value="R">Rural</asp:ListItem>
                            <asp:ListItem Value="U">Urban</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
               
            
                <tr style="height: 25px; background-color: Navy">
                    <td align="center" colspan="4">
                        <asp:Label ID="lbldodetails" runat="server" Font-Bold="True" Font-Size="10pt" Text="Delivery Order Details"
                            ForeColor="whitesmoke"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lbldono" runat="server" Font-Size="10pt" Text="DO No."></asp:Label></td>
                    <td align="left">
                        <asp:TextBox ID="tx_do_no" runat="server" Width="100px" MaxLength="15"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tx_do_no"
                            ErrorMessage="Please enter delivery order no." ValidationGroup="1">*</asp:RequiredFieldValidator>
                    </td>
                    <td align="left">
                        <asp:Label ID="lbldodate" runat="server" Font-Size="10pt" Text="DO Date"></asp:Label></td>
                    <td align="left">
                        <asp:TextBox ID="tx_do_date" runat="server" Width="100px"></asp:TextBox>

                        <script type="text/javascript">
	                       new tcal ({
				             'formname': '0',
				                'controlname': 'ctl00_ContentPlaceHolder1_tx_do_date'
	                                    });
                        </script>

                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lbldovalidity" runat="server" Font-Size="10pt" Text="Validity Date"></asp:Label></td>
                    <td align="left" colspan="3">
                        <asp:TextBox ID="tx_do_validity" runat="server" Width="100px"></asp:TextBox>

                        <script type="text/javascript">
	                               new tcal ({
				                    'formname': '0',
				                      'controlname': 'ctl00_ContentPlaceHolder1_tx_do_validity'
	                                     });
                        </script>

                    </td>
                </tr>
                <tr style="height: 25px; background-color: Navy">
                    <td align="center" colspan="4">
                        <asp:Label ID="lbldodetailsfps" runat="server" Font-Bold="True" Font-Size="10pt"
                            ForeColor="whitesmoke" Text="DO Details FPS - Wise"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lbl_blk" runat="server" ForeColor="Black" Text="FPS Block" Font-Size="10pt">
                        </asp:Label>
                    </td>
                    <td align="left" colspan="3">
                        <asp:DropDownList ID="ddl_block" runat="server" OnSelectedIndexChanged="ddl_block_SelectedIndexChanged"
                            Font-Names="DVBW-TTYogeshEN" Font-Size="Medium" AutoPostBack="True" Width="330px">
                        </asp:DropDownList>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddl_block"
                            ErrorMessage="Please Select FPS Block" ValidationGroup="2" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                        </td>
                </tr>
                <tr>
                    <td align="left" colspan="2">
                        <asp:Label ID="lbl_fps" runat="server" ForeColor="Black" Text="FPS Name" Font-Size="10pt"></asp:Label>
                    </td>
                   
                    <td colspan="2" align="left">
                        <asp:Label ID="lblFPSNAme" runat="server" Font-Bold="True" Font-Names="Arial Unicode MS"
                            ForeColor="Red" Font-Size="10pt"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" valign="top" align="left" rowspan="6">
                        <asp:ListBox ID="Lstbx_fps_name" runat="server" Height="200px" Width="300px" OnSelectedIndexChanged="Lstbx_fps_name_SelectedIndexChanged"
                            AutoPostBack="True"></asp:ListBox>
                    </td>
                    <td align="left">
                        <asp:Label ID="lbl_allotqty" runat="server" ForeColor="Black" Text="Allotment Qty."
                            Font-Size="10pt"></asp:Label></td>
                    <td align="left">
                        <asp:TextBox ID="tx_allot_qty2" runat="server" BackColor="MenuBar" MaxLength="8"
                            ReadOnly="True" Width="100px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lbl_issueqty" runat="server" Text="Already Issued Qty" Font-Size="10pt"></asp:Label></td>
                    <td align="left">
                        <asp:TextBox ID="tx_already_iqty" runat="server" BackColor="MenuBar" MaxLength="8"
                            ReadOnly="True" Width="100px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lbl_balqty" runat="server" ForeColor="Black" Text="Balance Qty" Font-Size="10pt"></asp:Label></td>
                    <td align="left">
                        <asp:TextBox ID="tx_balQty2" runat="server" BackColor="MenuBar" MaxLength="8" ReadOnly="True"
                            Width="100px"></asp:TextBox>Qtls.</td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="totaldoqty" runat="server" Font-Size="12px" Text="Total DO Quantity "></asp:Label></td>
                    <td align="left">
                        <asp:TextBox ID="tx_tot_qty" runat="server" BackColor="MenuBar" MaxLength="8" ReadOnly="True"
                            Width="100px"></asp:TextBox>Qtls.</td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblQuantity" runat="server" Font-Size="12px" ForeColor="Black" Text="Quantity"></asp:Label></td>
                    <td align="left">
                        <asp:TextBox ID="tx_qty" runat="server" MaxLength="13" Width="100px"></asp:TextBox>Qtls.
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tx_qty"
                            ErrorMessage="Please enter quantity" ValidationGroup="2">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btn_Add_FPS" runat="server" Text="Add FPS" OnClick="btn_Add_FPS_Click"
                            ValidationGroup="2" Enabled="False" Font-Bold="true" ForeColor="navy" Width="100px" /></td>
                </tr>
                <tr style="height: 25px; background-color: Navy" id="tr_head" runat="server" visible="false">
                    <td align="center" colspan="4">
                        <asp:Label ID="lbl_Panal_Head" runat="server" ForeColor="whitesmoke" Font-Bold="True"
                            Font-Size="10pt" Text="Added FPS Details"></asp:Label></td>
                </tr>
                <tr>
                    <td align="center" colspan="4">
                        <asp:Panel ID="Pnl_Fps_View" runat="server" ScrollBars="Auto" Width="100%" Visible="False"
                            Height="64px">
                            <asp:GridView ID="gv_FPS_Details" runat="server" AutoGenerateColumns="False" BackColor="White"
                                BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Font-Bold="False"
                                Font-Size="Small" OnSelectedIndexChanged="gv_FPS_Details_SelectedIndexChanged" Width="625px"
                                Height="72px">
                                <RowStyle ForeColor="#000066" />
                                <Columns>
                                    <asp:CommandField SelectText="Delete" ShowSelectButton="True">
                                        <ItemStyle Font-Bold="False" Width="50px" />
                                    </asp:CommandField>
                                    <asp:TemplateField HeaderText="Select to Issue DO" Visible="False">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelecttoIssue" runat="server" AutoPostBack="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="fps_name" HeaderText="FPS Name" SortExpression="fps_name">
                                        <ItemStyle Font-Bold="False" Font-Names="Arial Unicode MS" Font-Size="9pt" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="commodity_name" HeaderText="Commodity" SortExpression="commodity_name">
                                        <ItemStyle Font-Bold="False" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="fps_code" DataField="fps_code" Visible="False" />
                                    <asp:BoundField DataField="scheme_name" HeaderText="Scheme" SortExpression="scheme_name">
                                        <ItemStyle Font-Bold="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="allot_qty" HeaderText="Against Allot." SortExpression="allot_qty" />
                                    <asp:BoundField DataField="qty" HeaderText="Quantity" SortExpression="qty">
                                        <ItemStyle Font-Bold="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="rate_qtls" HeaderText="Rate/Qtls." SortExpression="rate_qtls">
                                        <ItemStyle Font-Bold="False" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="amt" HeaderText="Amount" SortExpression="amt">
                                        <ItemStyle Font-Bold="False" />
                                    </asp:BoundField>
                                </Columns>
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#006699" Font-Bold="False" Font-Size="Small" ForeColor="White" />
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
                <tr style="height: 25px; background-color: Navy">
                    <td align="center" colspan="4">
                        <asp:Label ID="lblpayment" runat="server" ForeColor="whitesmoke" Font-Bold="True"
                            Font-Size="10pt" Text="Payment Details"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblRateQuintal" runat="server" Font-Size="10pt" Text="Rate/Quintal"></asp:Label></td>
                    <td align="left">
                        <asp:TextBox ID="tx_rate_qt" runat="server" MaxLength="8" Width="100px" ReadOnly="True"
                            BackColor="Linen"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tx_rate_qt"
                            ErrorMessage="Please enter Rate" ValidationGroup="2">*</asp:RequiredFieldValidator>
                    </td>
                    <td align="left">
                        <asp:Label ID="lbltotamt" runat="server" Font-Size="10pt" Text="Total Amount"></asp:Label>&nbsp;</td>
                    <td align="left">
                        <asp:TextBox ID="tx_tot_amt" runat="server" BackColor="Linen" MaxLength="8" ReadOnly="True"
                            Width="100px"></asp:TextBox>
                        <asp:Label ID="Label27" runat="server" ForeColor="Black" Text="Rs." Font-Size="10pt"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblPaymentMode" runat="server" Font-Size="10pt" Text="Payment Mode"></asp:Label></td>
                    <td align="left">
                        <asp:DropDownList ID="ddl_pmode" runat="server" AutoPostBack="True" Width="145px"
                            OnSelectedIndexChanged="ddl_pmode_SelectedIndexChanged">
                            <asp:ListItem Value="D">DD / Cheque</asp:ListItem>
                            <asp:ListItem Value="R">Free Scheme / Credit</asp:ListItem>
                            <asp:ListItem Value="A">Cash</asp:ListItem>
                        </asp:DropDownList></td>
                    <td align="left">
                        <asp:Label ID="lblddchekno" runat="server" Font-Size="10pt" Text="DD/Chq. No. "></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="tx_dd_no" runat="server" Width="100px" MaxLength="30"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tx_dd_no"
                            ErrorMessage="Please enter DD No." ValidationGroup="1" >*</asp:RequiredFieldValidator>
                        </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblamount" runat="server" Font-Size="10pt" Text="Amount"></asp:Label></td>
                    <td align="left">
                        <asp:TextBox ID="tx_dd_amount" runat="server" Width="100px" MaxLength="12"></asp:TextBox>Rs.
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tx_dd_amount"
                            ErrorMessage="Please enter Amount"  ValidationGroup="1" >*</asp:RequiredFieldValidator>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblddchekdate" runat="server" Font-Size="10pt" Text="DD/Chq. Date"></asp:Label></td>
                    <td align="left">
                        <asp:TextBox ID="tx_dd_date" runat="server" Width="100px"></asp:TextBox>

                        <script type="text/javascript">
	                         new tcal ({
			                   'formname': '0',
			                  'controlname': 'ctl00_ContentPlaceHolder1_tx_dd_date'
	                            });
                        </script>

                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblBankName" runat="server" Font-Size="10pt" Text="Bank Name"></asp:Label></td>
                    <td align="left" colspan="3">
                        <asp:DropDownList ID="ddl_bank" runat="server" Width="300px">
                        </asp:DropDownList></td>
               </tr>
                <tr style="height:10px">
                    <td align="left" colspan="4">
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="4">
                        <asp:Button ID="save" runat="server" Text="Save" Font-Bold="true" Font-Size="Medium"
                            Width="90px" OnClick="save_Click" ValidationGroup="1" ForeColor="navy" />
                        <asp:Button ID="btnClose" runat="server" Font-Size="Medium" OnClick="btnClose_Click"
                            Font-Bold="true" Text="Close" Width="90px" ForeColor="navy" />
                        <asp:Button ID="btn_new" runat="server" Font-Bold="true" Font-Size="Medium" OnClick="btn_new_Click"
                            Text="New" Width="90px" ForeColor="navy" />&nbsp;
                        <asp:HyperLink ID="hlinkpdo" runat="server" NavigateUrl="#" Visible="False" Font-Size="10pt">Issue Delivery Order</asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" Font-Size="Small" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="2" Width="232px" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Size="Small" ShowMessageBox="True"
                            ShowSummary="False" Style="left: 0px; top: 0px" ValidationGroup="1" Height="1px"
                            Width="216px" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
