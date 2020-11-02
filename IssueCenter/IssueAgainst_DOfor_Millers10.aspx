<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="IssueAgainst_DOfor_Millers10.aspx.cs" Inherits="IssueCenter_IssueAgainst_OpenSales_DO" Title="DO Page" %>
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
<asp:Panel ID="panel_do" runat ="server" Width="641px" >
        <table>
        <tr><td align="center" 
                style="background-color: #808000; height: 21px; width: 624px;">
            <asp:Label ID="Label27" runat="server" Font-Bold="True" 
                Text="Issue Against DO for Miller" 
                
                style="font-family: Georgia; color: #333300; text-decoration: underline; font-size: large; background-color: #FFFFFF;"></asp:Label></td></tr>
            <tr>
                <td style="vertical-align: top; text-align: left; height: 130px; font-family: Georgia; font-size: small; width: 624px;">
                <fieldset style="height: 100px; width: 600px;" >
                    <table style="width: 629px; background-color: lightblue;" border="1" 
                        cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="4" 
                                style="font-size: 10pt; position: static; background-color: #ffffcc; text-align: center;" 
                                align="left">
                                <asp:Label ID="Label1" runat="server" ForeColor="#0000C0" Font-Size="Medium" Font-Italic="True"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; position: static; height: 20px; background-color: #ffffcc">
                                Crop Year</td>
                            <td style="font-size: 10pt; position: static; height: 20px; background-color: #ffffcc">
                                <asp:TextBox ID="txtYear" runat="server" Enabled="False" ReadOnly="True" Width="112px"></asp:TextBox>
                            </td>
                            <td align="left" style="font-size: 10pt; position: static; height: 20px; background-color: #ffffcc">
                                <asp:Label ID="lbltoissue" runat="server" Text="Issued To"></asp:Label>
                            </td>
                            <td align="left" 
                                style="font-size: 10pt; position: static; height: 20px; background-color: #ffffcc; width: 221px;">
                                <asp:DropDownList ID="ddlissueto" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlissueto_SelectedIndexChanged" Width="162px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; position: static; height: 20px; background-color: #ffffcc">
                                <asp:Label ID="lbldono" runat="server" ForeColor="Red" Text="Delivery Order No." Width="99px"></asp:Label>
                            </td>
                            <td style="font-size: 10pt; position: static; height: 20px; background-color: #ffffcc">
                                <asp:DropDownList ID="ddl_do_no" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_do_no_SelectedIndexChanged" Width="182px">
                                </asp:DropDownList>
                            </td>
                            <td align="left" style="font-size: 10pt; position: static; height: 20px; background-color: #ffffcc">
                                Lot Number</td>
                            <td align="left" style="font-size: 10pt; position: static; height: 20px; background-color: #ffffcc; width: 221px;">
                                <asp:TextBox ID="txtLotNumber" runat="server" Enabled="False" ReadOnly="True" Width="112px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; position: static; height: 20px; background-color: #ffffcc">
                                <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="Party/Miller Name" 
                                    Width="99px"></asp:Label>
                            </td>
                            <td style="font-size: 10pt; position: static; height: 20px; background-color: #ffffcc">
                                <asp:DropDownList ID="ddlpartyname" runat="server" Enabled="False" 
                                    OnSelectedIndexChanged="ddlissueto_SelectedIndexChanged" Width="182px">
                                </asp:DropDownList>
                            </td>
                            <td align="left" 
                                style="font-size: 10pt; position: static; height: 20px; background-color: #ffffcc">
                                <asp:Label ID="lbldodate" runat="server" Text="DO Date" Width="70px"></asp:Label>
                            </td>
                            <td align="left" 
                                style="font-size: 10pt; position: static; height: 20px; background-color: #ffffcc; width: 221px;">
                                <asp:TextBox ID="tx_do_date" runat="server" BackColor="#E0E0E0" 
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" 
                                    Width="112px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; position: static; background-color: #ffffcc; height: 18px;" align="left">
                                <asp:Label ID="lblQuantity" runat="server" Text="DO Quantity" Width="84px"></asp:Label>
                            </td>
                            <td style="font-size: 10pt; position: static; background-color: #ffffcc; height: 18px;">
                                <asp:TextBox ID="tx_do_qty" runat="server" BackColor="#E0E0E0" 
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" 
                                    Width="112px"></asp:TextBox>
                                <asp:Label ID="Label28" runat="server" ForeColor="#CC0000" 
                                    style="text-align: center; font-size: small" Text="in Qtls."></asp:Label>
                            </td>
                            <td style="font-size: 10pt; position: static; background-color: #ffffcc; height: 18px;" align="left">
                                <asp:Label ID="lbl_balqty" runat="server" Text="Bal Quantity"></asp:Label>
                            </td>
                            <td style="font-size: 10pt; position: static; background-color: #ffffcc; height: 18px; width: 221px;" 
                                align="left">
                                <asp:TextBox ID="tx_balance_qty" runat="server" BackColor="#E0E0E0" 
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" 
                                    Width="112px"></asp:TextBox>
                                Qtls.</td>
                        </tr>
                        <tr>
                            <td align="left" 
                                style="font-size: 10pt; position: static; background-color: #ffffcc; height: 18px;">
                                <asp:Label ID="lblCommodity" runat="server" Text="Commodity"></asp:Label>
                            </td>
                            <td style="font-size: 10pt; position: static; background-color: #ffffcc; height: 18px;">
                                <asp:DropDownList ID="ddl_commodity" runat="server" Enabled="False" 
                                    ValidationGroup="1" Width="182px">
                                </asp:DropDownList>
                            </td>
                            <td align="left" 
                                style="font-size: 10pt; position: static; background-color: #ffffcc; height: 18px;">
                                <asp:Label ID="lblScheme" runat="server" Text="Scheme" Width="63px"></asp:Label>
                            </td>
                            <td align="left" 
                                style="font-size: 10pt; position: static; background-color: #ffffcc; height: 18px; width: 221px;">
                                <asp:DropDownList ID="ddl_scheme" runat="server" Enabled="False" Width="162px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="4" 
                                style="font-size: 10pt; position: static; background-color: #ffffcc; height: 20px;">
                                &nbsp;</td>
                        </tr>
                       
                    </table>
                    <br />
                </fieldset>
                 <br />
                     <br />
                     <br />
                </td>
            </tr>
            
            <tr>
            <td style="vertical-align: top; text-align: left; height: 252px; background-color: #FFFFFF; width: 624px;">
                <fieldset style="height: 177px; font-family: Georgia; font-size: small;" >
                <legend>Issued Details
                </legend>
                    <table style="background-color: lightsteelblue; width: 630px;" border="1" 
                        cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="font-size: 10pt; position: static; background-color: #ffffcc; height: 20px; width: 123px;" align="left">
                                <asp:Label ID="lbl_issueqty" runat="server" Text="Issued Quantity"></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #ffffcc; height: 20px;" 
                                colspan="2">
                                <asp:TextBox ID="tx_issued_qty" runat="server" ReadOnly="True" Width="112px" 
                                    BackColor="#E0E0E0" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                <asp:Label ID="Label23" runat="server" Text="Qtls."></asp:Label>
                            </td>
                            <td style="font-size: 10pt; position: static; background-color: #ffffcc; height: 20px; width: 77px;" 
                                align="left">
                                <asp:Label ID="lblbalqty" runat="server" Text="Balance Qty."></asp:Label></td>
                            <td style="font-size: 10pt; position: static; background-color: #ffffcc; height: 20px;" 
                                colspan="2">
                                <asp:TextBox ID="tx_issue_balqty" runat="server" ReadOnly="True" Width="112px" 
                                    BackColor="#E0E0E0" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                <asp:Label ID="Label25" runat="server" Text="Qtls."></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="background-color: #FFCCFF; ">
                                <asp:Label ID="lbldispsource" runat="server" Text="Stock Issued From"></asp:Label></td>
                            <td style="background-color: #FFCCFF; " colspan="2">
                                <asp:DropDownList ID="ddlsarrival" runat="server" 
                                    OnSelectedIndexChanged="ddlsarrival_SelectedIndexChanged" Width="182px">
                                </asp:DropDownList></td>
                            <td align="left" 
                                style="background-color: #FFCCFF; ">
                                <asp:Label ID="lblGodownNo" runat="server" Text="Godown"></asp:Label></td>
                            <td colspan="2" 
                                style="background-color: #FFCCFF; ">
                                <asp:DropDownList ID="ddl_godown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_godown_SelectedIndexChanged" Width="208px">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="background-color: #FFCCFF; ">
                                <asp:Label ID="lblbalcomdty" runat="server" Text="Current Balance of Commodity at IssueCentre"></asp:Label></td>
                            <td align="left" colspan="4" style="background-color: #FFCCFF; ">
                                <asp:TextBox ID="tx_cur_bal" runat="server" BackColor="#E0E0E0" ReadOnly="True" 
                                    Width="112px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>Qtls.</td>
                        </tr>
                        <tr>
                            <td style="background-color: #FFCCFF; " align="left">
                                <asp:Label ID="lblDispatchQty" runat="server" Text="Quantity To Issue"></asp:Label></td>
                            <td style="background-color: #FFCCFF; ">
                                <asp:TextBox ID="tx_qty_to_issue" runat="server" Width="100px" MaxLength="13" 
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>*&nbsp;<asp:Label 
                                    ID="Label29" runat="server" ForeColor="#CC0000" 
                                    style="text-align: center; font-size: small" Text="in Qtls."></asp:Label>
                            </td>
                            <td style="background-color: #FFCCFF; " align="left">
                                Qtls.</td>
                            <td style="background-color: #FFCCFF; " 
                                align="left">
                                <asp:Label ID="lblBagNumber" runat="server" Text="No of Bags"></asp:Label></td>
                            <td style="background-color: #FFCCFF; " align="left">
                                <asp:TextBox ID="tx_bags" runat="server" Width="100px" MaxLength="4" 
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>*
                                            </td>
                            <td style="background-color: #FFCCFF; " 
                                align="left">
                                &nbsp;</td>
                        </tr>
                    </table>
                    <table border="1" cellpadding="0" cellspacing="0" 
                        style="background-color: lightsteelblue; width: 629px;">
                        <tr>
                            <td align="left" 
                                
                                style="font-size: 10pt; position: static; background-color: #FFCCFF; height: 25px;">
                                <asp:Label ID="lblTruckNumber" runat="server" Text="Truck No."></asp:Label>
                            </td>
                            <td style="font-size: 10pt; position: static; background-color: #FFCCFF; height: 25px;">
                                <asp:TextBox ID="tx_gatepass" runat="server" BorderColor="Black" 
                                    BorderStyle="Solid" BorderWidth="1px" MaxLength="70" Width="180px"></asp:TextBox>
                            </td>
                            <td style="font-size: 10pt; position: static; background-color: #FFCCFF; height: 25px;">
                                <asp:Label ID="lblissuedate" runat="server" Text="Issued Date"></asp:Label>
                            </td>
                            <td colspan="2" 
                                
                                style="font-size: 10pt; position: static; background-color: #FFCCFF; height: 25px;">
                                <asp:TextBox ID="tx_issued_date" runat="server" BorderColor="Black" 
                                    BorderStyle="Solid" BorderWidth="1px" Width="100px"></asp:TextBox>
                                    
                                    <script type  ="text/javascript">
                                        new tcal({
                                            'formname': '0',
                                            'controlname': 'ctl00_ContentPlaceHolder1_tx_issued_date'
                                        });
	     </script>
                                    
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" 
                                style="background-color: #808000; text-align: center;">
                                <asp:Button ID="btnsave" runat="server" BorderColor="Black" BorderStyle="Solid" 
                                    BorderWidth="1px" Font-Bold="False" Font-Size="Medium" OnClick="btnsave_Click" 
                                    Text="Save" ValidationGroup="1" Width="88px" 
                                    style="font-family: Calibri; font-size: large" />
                                <asp:Button ID="btnclose" runat="server" BorderColor="Black" 
                                    BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" 
                                    OnClick="btnclose_Click" Text="Close" Width="95px" 
                                    style="font-family: Calibri; font-size: large" />
                            </td>
                            <td style="background-color: #808000; text-align: center;">
                                <asp:Button ID="btn_new" runat="server" BorderColor="Black" BorderStyle="Solid" 
                                    BorderWidth="1px" Font-Bold="False" Font-Size="Medium" OnClick="btn_new_Click" 
                                    Text="New" Width="70px" style="font-family: Calibri; height: 27px;" />
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

