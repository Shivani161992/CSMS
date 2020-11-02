<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="IssueAgainst_OpenSales_DO.aspx.cs" Inherits="IssueCenter_IssueAgainst_OpenSales_DO" Title="DO Page" %>
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
    

<div>
<asp:Panel ID="panel_do" runat ="server" >
        <table>
        <tr><td align="center" style="background-color: #cccccc; height: 21px;">
            <asp:Label ID="Label27" runat="server" Font-Bold="True" 
                Text="Issue Against DO for  Open,Damage and Sweepage Commodity"></asp:Label></td></tr>
            <tr>
                <td style="vertical-align: top; text-align: left; height: 130px;">
                <fieldset style="height: 95px" >
                <legend>Delivery Order of Sales Open Commodity</legend>
                    <table style="width: 736px; background-color: lightblue;" border="1" cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="7" style="font-size: 10pt; position: static; background-color: #ffffcc;" align="left">
                                <asp:Label ID="Label1" runat="server" ForeColor="#0000C0" Font-Size="Medium" Font-Italic="True"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; position: static; height: 20px; background-color: #ffffcc">
                                <asp:Label ID="lbltoissue" runat="server" Text="Issued To"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; height: 20px; background-color: #ffffcc">
                              <asp:DropDownList ID = "ddlissueto" runat = "server" AutoPostBack="True" OnSelectedIndexChanged="ddlissueto_SelectedIndexChanged" Width="162px"></asp:DropDownList></td>
                            <td align="left" style="font-size: 10pt; position: static; height: 20px; background-color: #ffffcc">
                                <asp:Label ID="lbldono" runat="server" Text="Delivery Order No" Width="99px" ForeColor="Red"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; height: 20px; background-color: #ffffcc">
                                <asp:DropDownList ID="ddl_do_no" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_do_no_SelectedIndexChanged" Width="162px">
                                </asp:DropDownList></td>
                            <td align="left" style="font-size: 10pt; position: static; height: 20px; background-color: #ffffcc">
                                <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="Party/Miller Name" Width="99px"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; height: 20px; background-color: #ffffcc"><asp:DropDownList ID = "ddlpartyname" runat = "server" OnSelectedIndexChanged="ddlissueto_SelectedIndexChanged" Width="162px" Enabled="False">
                            </asp:DropDownList></td>
                            <td style="font-size: 10pt; position: static; height: 20px; background-color: #ffffcc">
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; position: static; background-color: #ffffcc; height: 18px;" align="left">
                                <asp:Label ID="lbldodate" runat="server" Text="DO Date" Width="70px"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #ffffcc; height: 18px;">
                                <asp:TextBox ID="tx_do_date" runat="server" Width="112px" ReadOnly="True" BackColor="#E0E0E0" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
                            <td style="font-size: 10pt; position: static; background-color: #ffffcc; height: 18px;" align="left">
                                <asp:Label ID="lblQuantity" runat="server" Text="DO Quantity" Width="84px"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #ffffcc; height: 18px;" align="left">
                                <asp:TextBox ID="tx_do_qty" runat="server" Width="134px" ReadOnly="True" BackColor="#E0E0E0" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
                            <td style="font-size: 10pt; position: static; background-color: #ffffcc; height: 18px;" align="left">
                                <asp:Label ID="lbl_balqty" runat="server" Text="Bal Quantity"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #ffffcc; height: 18px;" align="left">
                                <asp:TextBox ID="tx_balance_qty" runat="server" Width="88px" ReadOnly="True" BackColor="#E0E0E0" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
                            <td style="font-size: 10pt; position: static; background-color: #ffffcc; height: 18px;">
                                Qtls.</td>
                        </tr>
                        <tr>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #ffffcc; height: 20px;">
                                <asp:Label ID="lblCommodity" runat="server" Text="Commodity"></asp:Label></td>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #ffffcc; height: 20px;">
                                <asp:DropDownList ID="ddl_commodity" runat="server" Enabled="False"
                                     ValidationGroup="1"
                                    Width="191px">
                                </asp:DropDownList></td>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #ffffcc; height: 20px;">
                                <asp:Label ID="lblScheme" runat="server" Text="Scheme" Width="63px"></asp:Label></td>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #ffffcc; height: 20px;">
                                <asp:DropDownList ID="ddl_scheme" runat="server" Enabled="False" Width="158px" >
                                </asp:DropDownList></td>
                            <td align="left" colspan="1" style="font-size: 10pt; position: static; background-color: #ffffcc; height: 20px;">
                                </td>
                            <td align="left" colspan="6" style="font-size: 10pt; position: static; background-color: #ffffcc; height: 20px;">
                                </td>
                        </tr>
                    </table>
                </fieldset>
                
                </td>
            </tr>
            
            <tr>
            <td style="vertical-align: top; text-align: left; height: 252px;">
                <fieldset style="height: 177px" >
                <legend>Issued Details
                </legend>
                    <table style="background-color: lightsteelblue; width: 736px;" border="1" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="font-size: 10pt; position: static; background-color: #ffffcc; height: 20px; width: 123px;" align="left">
                                <asp:Label ID="lbl_issueqty" runat="server" Text="Issued Quantity"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #ffffcc; height: 20px;">
                                <asp:TextBox ID="tx_issued_qty" runat="server" ReadOnly="True" Width="152px" BackColor="#E0E0E0" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
                            <td style="font-size: 10pt; position: static; background-color: #ffffcc; height: 20px;" align="left">
                                <asp:Label ID="Label23" runat="server" Text="Qtls."></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #ffffcc; height: 20px;" align="left">
                                <asp:Label ID="lblbalqty" runat="server" Text="Balance Qty."></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #ffffcc; height: 20px;">
                                <asp:TextBox ID="tx_issue_balqty" runat="server" ReadOnly="True" Width="90px" BackColor="#E0E0E0" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
                            <td style="font-size: 10pt; position: static; background-color: #ffffcc; height: 20px;" align="left">
                                <asp:Label ID="Label25" runat="server" Text="Qtls."></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #ffffcc; height: 20px;">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cccc66; height: 25px; width: 123px;">
                                <asp:Label ID="lbldispsource" runat="server" Text="Stock Issued From"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cccc66; height: 25px;">
                                <asp:DropDownList ID="ddlsarrival" runat="server" OnSelectedIndexChanged="ddlsarrival_SelectedIndexChanged" Width="174px">
                                </asp:DropDownList></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cccc66; height: 25px;">
                            </td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cccc66; height: 25px;">
                                <asp:Label ID="lblGodownNo" runat="server" Text="Godown"></asp:Label></td>
                            <td colspan="3" style="font-size: 10pt; position: static; background-color: #cccc66; height: 25px;">
                                <asp:DropDownList ID="ddl_godown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_godown_SelectedIndexChanged" Width="208px">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="font-size: 10pt; position: static; background-color: #cccc66; height: 20px;">
                                <asp:Label ID="lblbalcomdty" runat="server" Text="Current Balance of Commodity at IssueCentre"></asp:Label></td>
                            <td align="left" colspan="2" style="font-size: 10pt; position: static; background-color: #cccc66; height: 20px;">
                                <asp:TextBox ID="tx_cur_bal" runat="server" BackColor="#E0E0E0" ReadOnly="True" Width="80px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>Qtls.</td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cccc66; height: 20px;">
                                </td>
                            <td align="left" colspan="2" style="font-size: 10pt; position: static; background-color: #cccc66; height: 20px;">
                                </td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; position: static; background-color: #cccc66; height: 24px; width: 123px;" align="left">
                                <asp:Label ID="lblDispatchQty" runat="server" Text="Quantity To Issue"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cccc66; width: 73px; height: 24px;">
                                <asp:TextBox ID="tx_qty_to_issue" runat="server" Width="88px" MaxLength="13" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>*&nbsp;</td>
                            <td style="font-size: 10pt; position: static; background-color: #cccc66; height: 24px; width: 73px;" align="left">
                                Qtls.</td>
                            <td style="font-size: 10pt; position: static; background-color: #cccc66; height: 24px; width: 73px;" align="left">
                                <asp:Label ID="lblBagNumber" runat="server" Text="No of Bags"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cccc66; height: 24px; width: 73px;" align="left">
                                <asp:TextBox ID="tx_bags" runat="server" Width="64px" MaxLength="4" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>*</td>
                            <td style="font-size: 10pt; position: static; background-color: #cccc66; height: 24px; width: 73px;" align="left">
                                <asp:Label ID="lblissuedate" runat="server" Text="Issued Date"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cccc66; height: 24px; width: 73px;">
                           <asp:TextBox ID="tx_issued_date" runat="server" Width="65px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
             <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_tx_issued_date'
	    });
	     </script>
                                </td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; position: static; background-color: #cccc66; height: 25px;" align="left">
                                <asp:Label ID="lblTruckNumber" runat="server" Text="Truck No."></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #cccc66; height: 25px;" colspan="5">
                                <asp:TextBox ID="tx_gatepass" runat="server" Width="180px" MaxLength="70" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
                            <td style="font-size: 10pt; position: static; background-color: #cccc66; height: 25px;">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; position: static; background-color: #cc99ff;">
                                &nbsp;</td>
                            <td style="font-size: 10pt; position: static; background-color: #cc99ff;">
                                &nbsp;</td>
                            <td style="font-size: 10pt; position: static; background-color: #cc99ff;">
                                &nbsp;</td>
                            <td style="font-size: 10pt; position: static; background-color: #cc99ff;">
                                &nbsp;<asp:Button ID="btnsave" runat="server"  Font-Bold="False" Font-Size="Medium"
                                    OnClick="btnsave_Click" Text="Save" ValidationGroup="1" Width="88px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" /></td>
                            <td style="font-size: 10pt; position: static; background-color: #cc99ff;">
                                <asp:Button ID="btnclose" runat="server" Font-Size="Medium" OnClick="btnclose_Click"
                                    Text="Close" Width="95px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" /></td>
                            <td style="font-size: 10pt; position: static; background-color: #cc99ff;">
                                <asp:Button ID="btn_new" runat="server" Font-Bold="False" Font-Size="Medium" OnClick="btn_new_Click"
                                    Text="New" Width="70px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" /></td>
                            <td style="font-size: 10pt; position: static; background-color: #cc99ff;">
                                </td>
                        </tr>
                    </table>
                </fieldset> 
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tx_qty_to_issue"
                    ErrorMessage="Please enter quantity to issue" Height="0px" ValidationGroup="1"
                    Width="1px">*</asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tx_bags"
                    ErrorMessage="Please enter no of bags" Height="0px" ValidationGroup="1" Width="0px">*</asp:RequiredFieldValidator>
                &nbsp; &nbsp; &nbsp;&nbsp;
                <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                    Font-Size="Small" ShowMessageBox="True" ShowSummary="False" Style="z-index: 101"
                    ValidationGroup="1" Width="365px" />
                </td>
            </tr>
        </table>
        </asp:Panel> 
    </div>

</asp:Content>

