<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="Transporter_Order_Payment_detail.aspx.cs" Inherits="IssueCenter_Transporter_Order_Payment_detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script language="javascript" type="text/javascript">
    function OpenWin() {
        window.showModalDialog('FRMViewAddedFPS_Delievry.aspx');
        return false;
    }
</script>

<script type="text/javascript">
    function CheckIsNumeric(e, tx) {
        var AsciiCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
        if ((AsciiCode < 46 && AsciiCode != 8) || (AsciiCode > 57) || (AsciiCode == 47)) {
            alert('Please enter only numbers.');
            return false;
        }

        var num = tx.value;
        var len = num.length;
        var indx = -1;
        indx = num.indexOf('.');
        if (indx != -1) {
            if ((AsciiCode == 46)) {
                alert('Point must be apear only one time.');
                return false;
            }
            var dgt = num.substr(indx, len);
            var count = dgt.length;
            //alert (count);
            if (count > 5 && AsciiCode != 8) {
                alert("Only 5 decimal digits allowed.");
                return false;
            }
        }
    }
    </script>
    
    <center>
    
	<div style="text-align: right; height: 548px; width: 729px;" >
    <asp:Panel ID="panelDO" runat ="server" Height="600px" Width="730px"  >
        <table style="width: 608px; height: 700px">
        <tr>
        <td align="center" style="background-color: #cccccc; width: 713px; height: 21px;" valign="top">
            <asp:Label ID="lbldo" runat="server" Font-Bold="True" ForeColor="Black" Text="Payment Detail-Door Step TransporterOrder"
                Width="385px"></asp:Label></td>
        </tr>
            <tr>
                <td rowspan="2" style="width: 713px; height: 600px" valign="top">
               
                    <table border="1" cellpadding="0" cellspacing="0" style="width: 624px; height: 666px;">
                        <tr>
                            <td align="left" 
                                
                                
                                
                                style="border: 2px solid gray; font-size: 10pt; position: static; background-color: #cfdcc8; height: 170px; width: 717px;">
                            <table style="width: 712px; height: 245px;">
                                <tr>
                                    <td colspan="5" style="border: 1px solid silver; height: 3px;" valign = "top">
                                        <span style="color: #cc0033"><strong>केवल नगद विक्रय की प्रविष्टि हेतु (प्रत्येक 
                                        उ मू दू की बैंक सम्बंधित जानकारी की प्रविष्टि प्रथक प्रथक की जायेगी )</strong></span></td>
                                </tr>
                                <tr>
                                    <td style="border: 1px solid silver; width: 120px; height: 16px;">
                                        <asp:Label ID="lblallotmonth" runat="server" Font-Size="12px" 
                                            Text="Allot. Month" Width="67px"></asp:Label>
                                    </td>
                                    <td style="border: 1px solid silver; width: 140px; height: 16px;">
                                        <asp:DropDownList ID="ddl_allot_month" runat="server" AutoPostBack="True" 
                                            OnSelectedIndexChanged="ddl_allot_month_SelectedIndexChanged" Width="153px">
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
                                        </asp:DropDownList>
                                    </td>
                                    <td style="border: 1px solid silver; width: 27px; height: 16px;">
                                        <asp:Label ID="lblallotyear" runat="server" Font-Size="12px" Text="Allot. Year" 
                                            Width="101px"></asp:Label>
                                    </td>
                                    <td style="border: 1px solid silver; width: 89px; height: 16px;">
                                        <asp:DropDownList ID="ddd_allot_year" runat="server">
                                        </asp:DropDownList>
                                        &nbsp;
                                    </td>
                                    <td style="border: 1px solid silver; width: 277px; height: 16px;">
                                        &nbsp; &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border: 1px solid silver; height: 29px;" 
                                        colspan="5">
                                <asp:Label ID="Label6" runat="server" Text="जिसका भुगतान प्राप्त हुआ है वो दूकान चुने |"
                                    Width="241px" ForeColor="#C000C0" Height="18px"></asp:Label>&nbsp;
                                        <asp:DropDownList ID="ddlfpsname" runat="server" AutoPostBack="True"  
                                            OnSelectedIndexChanged="ddlfpsname_SelectedIndexChanged" Width="250px" 
                                            Height="26px">
                                        </asp:DropDownList>
                                        &nbsp; &nbsp;&nbsp;
                                        <asp:Label ID="lblratetype0" runat="server" Font-Bold="True" Font-Size="12px" 
                                            ForeColor="Navy" Text="Rate Type" Width="58px"></asp:Label>
                                        &nbsp;
                                        <asp:DropDownList ID="ddl_rate_type" runat="server" AutoPostBack="True" 
                                            OnSelectedIndexChanged="ddl_rate_type_SelectedIndexChanged" Width="96px">
                                            <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="R">Rural</asp:ListItem>
                                            <asp:ListItem Value="U">Urban</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                   <tr>
                                       <td 
                                           style="border: 1px solid silver; width: 120px; height: 20px;">
                                        <asp:Label ID="Label7" runat="server" Text="Wheat Allotment" Font-Bold="True"></asp:Label></td>
                                       <td style="border: 1px solid silver; width: 140px; height: 20px;">
                                           <asp:TextBox ID="txtwheat" runat="server" BackColor="MenuBar" 
                                               BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" 
                                               style="text-align: right" Width="90px"></asp:TextBox>
                                           <asp:Label ID="Label11" runat="server" Text="Qtls."></asp:Label>
                                       </td>
                                       <td style="border: 1px solid silver; width: 27px; height: 20px;">
                                           <asp:Label ID="Label15" runat="server" Font-Bold="True" Text="Rate per Qtl" 
                                               Width="75px"></asp:Label>
                                       </td>
                                       <td style="border: 1px solid silver; width: 89px; height: 20px;">
                                           <asp:TextBox ID="txtwRate" runat="server" BackColor="MenuBar" 
                                               BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" 
                                               style="text-align: right" Width="90px"></asp:TextBox>
                                       </td>
                                       <td style="border: 1px solid silver; width: 621px; height: 20px;">
                                           <strong>TotalAmount</strong> &nbsp;<asp:TextBox ID="txtWAmount" runat="server" 
                                               BackColor="MenuBar" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" 
                                               ReadOnly="True" style="text-align: right" Width="90px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="border: 1px solid silver; width: 120px; height: 19px;">
                                        <asp:Label ID="Label8" runat="server" Text="Rice Allotment" Font-Bold="True"></asp:Label></td>
                                    <td style="border: 1px solid silver; width: 140px; height: 19px;">
                                        <asp:TextBox ID="txtrice" runat="server" BackColor="MenuBar" BorderColor="Black"
                                            BorderStyle="Solid" BorderWidth="1px"  ReadOnly="True" 
                                            Width="90px" style="text-align: right"></asp:TextBox>
                                        <asp:Label ID="Label12" runat="server" Text="Qtls."></asp:Label></td>
                                    <td style="border: 1px solid silver; width: 27px; height: 19px;">
                                        <asp:Label ID="Label16" runat="server" Text="Rate per Qtl" Width="75px" 
                                            Font-Bold="True"></asp:Label></td>
                                    <td style="border: 1px solid silver; width: 89px; height: 19px;">
                                        <asp:TextBox ID="txtrRate" runat="server" BackColor="MenuBar" BorderColor="Black"
                                            BorderStyle="Solid" BorderWidth="1px"  ReadOnly="True" Width="90px" 
                                            style="text-align: right"></asp:TextBox></td>
                                    <td 
                                        style="border: 1px solid silver; width: 277px; height: 19px;">
                                        <strong>TotalAmount</strong> <asp:TextBox ID="txtRAmount" runat="server" 
                                            BackColor="MenuBar" BorderColor="Black"
                                            BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Width="90px" 
                                            style="text-align: right"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="border: 1px solid silver; width: 120px; height: 20px;">
                                        <asp:Label ID="Label9" runat="server" Text="Sugar Allotment" Font-Bold="True" 
                                            Width="102px"></asp:Label></td>
                                    <td style="border: 1px solid silver; width: 140px; height: 20px;">
                                        <asp:TextBox ID="txtsugar" runat="server" BackColor="MenuBar" BorderColor="Black"
                                            BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Width="90px" 
                                            style="text-align: right"></asp:TextBox>
                                        <asp:Label ID="Label13" runat="server" Text="Qtls."></asp:Label></td>
                                    <td style="border: 1px solid silver; width: 27px; height: 20px;">
                                        <asp:Label ID="Label17" runat="server" Text="Rate per Qtl" Width="75px" 
                                            Font-Bold="True"></asp:Label></td>
                                    <td style="border: 1px solid silver; width: 89px; height: 20px;">
                                        <asp:TextBox ID="txtsugRate" runat="server" BackColor="MenuBar" BorderColor="Black"
                                            BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Width="90px" 
                                            style="text-align: right"></asp:TextBox></td>
                                    <td 
                                        style="border: 1px solid silver; width: 277px; height: 20px;">
                                        <strong>TotalAmount</strong>
                                        <asp:TextBox ID="txtsugAmount" runat="server" BackColor="MenuBar" BorderColor="Black"
                                            BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Width="90px" 
                                            style="text-align: right"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="border: 1px solid silver; width: 120px; height: 18px;">
                                        <asp:Label ID="Label10" runat="server" Text="Salt Allotment" Font-Bold="True"></asp:Label></td>
                                    <td style="border: 1px solid silver; width: 140px; height: 18px;">
                                        <asp:TextBox ID="txtsalt" runat="server" BackColor="MenuBar" BorderColor="Black"
                                            BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Width="90px" 
                                            style="text-align: right"></asp:TextBox>
                                        <asp:Label ID="Label14" runat="server" Text="Qtls."></asp:Label></td>
                                    <td style="border: 1px solid silver; width: 27px; height: 18px;">
                                        <asp:Label ID="Label18" runat="server" Text="Rate per Qtl" Width="75px" 
                                            Font-Bold="True"></asp:Label></td>
                                    <td style="border: 1px solid silver; width: 89px; height: 18px;">
                                        <asp:TextBox ID="txtsaltRate" runat="server" BackColor="MenuBar" BorderColor="Black"
                                            BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Width="90px" 
                                            style="text-align: right"></asp:TextBox></td>
                                    <td 
                                        style="border: 1px solid silver; width: 277px; height: 18px;">
                                        <strong>TotalAmount</strong>
                                        <asp:TextBox ID="txtsaltAmount" runat="server" BackColor="MenuBar" BorderColor="Black"
                                            BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Width="90px" 
                                            style="text-align: right"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="border: 1px solid silver; width: 120px; height: 22px;">
                                        <asp:Label ID="Label29" runat="server" Font-Bold="True" Text="Maize Allotment"></asp:Label>
                                    </td>
                                    <td style="border: 1px solid silver; width: 140px; height: 22px;">
                                        <asp:TextBox ID="txtmaize" runat="server" BackColor="MenuBar" 
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" 
                                            style="text-align: right" Width="90px"></asp:TextBox>
                                        <asp:Label ID="Label30" runat="server" Text="Qtls."></asp:Label>
                                    </td>
                                    <td style="border: 1px solid silver; width: 27px; height: 22px;">
                                        <asp:Label ID="Label31" runat="server" Font-Bold="True" Text="Rate per Qtl" 
                                            Width="75px"></asp:Label>
                                    </td>
                                    <td style="border: 1px solid silver; width: 89px; height: 22px;">
                                        <asp:TextBox ID="txtMaizeRate" runat="server" BackColor="MenuBar" 
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" 
                                            style="text-align: right" Width="90px"></asp:TextBox>
                                    </td>
                                    <td style="border: 1px solid silver; width: 277px; height: 22px;">
                                        <strong>TotalAmount</strong>&nbsp;<asp:TextBox ID="txtMaizeAmount" runat="server" 
                                            BackColor="MenuBar" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" 
                                            ReadOnly="True" style="text-align: right" Width="90px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        width: 120px; border-bottom: silver 1px solid">
                                        </td>
                                    <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        width: 140px; border-bottom: silver 1px solid">
                                        </td>
                                    <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        border-bottom: silver 1px solid" colspan="2">
                                        <span style="color: #3300ff"><strong>Total Payable ( शुद्ध भुगतान )</strong></span></td>
                                    <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid;
                                        width: 277px; border-bottom: silver 1px solid; text-align: left;">
                                        <asp:TextBox ID="txttotalamt" runat="server" BackColor="MenuBar" BorderColor="Black"
                                            BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Width="90px" 
                                            style="text-align: right" ForeColor="#C00000"></asp:TextBox></td>
                                </tr>
                            </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; background-color: #cfdcc8;  position: static; height: 250px; width: 717px;"   
                                valign = "top">
                               
                                            <table border="1" cellpadding="0" cellspacing="0"  style="width: 96%; height: 160px;">
                                                <tr>
                                                    <td align="left" 
                                                        style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 10px; ">
                                                        <asp:Label ID="Label28" runat="server" ForeColor="Black" 
                                                            style="font-size: small" Text="Link Cooperative Society" Width="142px"></asp:Label>
                                                    </td>
                                                    <td align="left" style="height: 10px; background-color: #cfdcc8">
                                                        <asp:DropDownList ID="ddlCooperative" runat="server" 
                                                             Font-Size="Small" Width="299px">
                                                        </asp:DropDownList>
                                                        &nbsp;</td>
                                                    <td align="left" style="background-color: #cfdcc8; width: 120px; height: 10px;">
                                                        &nbsp;</td>
                                                    <td align="left" style="background-color: #cfdcc8; height: 10px; width: 7px;">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td align="left" colspan="3" style="background-color: #cfdcc8">
                                                        <asp:Panel ID="pnlbankdetails" runat="server" Width="96%" Height="84px">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblPaymentMode" runat="server" Font-Size="12px" 
                                                                            Text="Payment Mode" Width="92px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddl_pmode" runat="server" Width="224px">
                                                                            <asp:ListItem Value="Se">--Select--</asp:ListItem>
                                                                            <asp:ListItem Value="D">DD / Cheque</asp:ListItem>
                                                                            
                                                                            <asp:ListItem Value="AD">Advance Payment</asp:ListItem>
                                                                            <asp:ListItem Value="OP">Online Payment</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblddchekno" runat="server" Font-Size="12px" Text="DD/Chq.No. /Bank Trans" 
                                                                            Width="149px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tx_dd_no" runat="server" MaxLength="50" Width="120px" 
                                                                            style="text-align: right"></asp:TextBox>
                                                                        <asp:Label ID="lbl_ddno" runat="server" Font-Bold="True" Font-Size="Medium" 
                                                                            ForeColor="Red" Text="*"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblBankName" runat="server" Font-Size="12px" Text="Bank Name"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddl_bank" runat="server" Width="225px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblddchekdate" runat="server" Font-Size="12px" 
                                                                            Text="Payment Date" Width="104px"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tx_dd_date" runat="server" Width="95px"></asp:TextBox>
                                                                        <script type="text/javascript">


                                                new tcal({
                                                    'formname': '0',
                                                    'controlname': 'ctl00_ContentPlaceHolder1_tx_dd_date'
                                                });
	     </script>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblamount" runat="server" Font-Size="12px" Text="Amount"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tx_dd_amount" runat="server" MaxLength="12" Width="125px" 
                                                                            style="text-align: right"></asp:TextBox>
                                                                        <asp:Label ID="Label2" runat="server" ForeColor="Black" Text="Rs."></asp:Label>
                                                                        <asp:Label ID="lbl_amt" runat="server" ForeColor="Red" style="z-index: 100" 
                                                                            Text="*"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td align="left" style="background-color: #cfdcc8; width: 7px;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" colspan="4" 
                                                        
                                                        style="font-size: 10pt;  position: static; background-color: #cfdcdc; height: 27px;">
                                                        &nbsp; &nbsp;<asp:Button ID="save" runat="server" BackColor="Silver" BorderColor="Black" 
                                                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="False" 
                                                            Font-Size="Medium" OnClick="save_Click" Text="Save" ValidationGroup="1" 
                                                            Width="104px" />
                                                        <asp:Button ID="btnClose" runat="server" BackColor="Silver" BorderColor="Black" 
                                                            BorderStyle="Solid" BorderWidth="1px" Font-Size="Medium" 
                                                            OnClick="btnClose_Click" Text="Close" Width="90px" />
                                                        <asp:Button ID="btn_new" runat="server" BackColor="Silver" BorderColor="Black" 
                                                            BorderStyle="Solid" BorderWidth="1px" Font-Bold="False" Font-Size="Medium" 
                                                            OnClick="btn_new_Click" Text="New" Width="75px" />
                                                        &nbsp;
                                                      <%--  <asp:HyperLink ID="hlinkpdo" runat="server" Font-Size="10pt" NavigateUrl="#" 
                                                            Visible="False">Print this Payment Details</asp:HyperLink>--%>
                                                    </td>
                                                </tr>
                                                
                                            </table>
                                        
                            </td>
                        </tr>
                    </table>
                   
                </td>
            </tr>
            
           
        </table>
        
        </asp:Panel>
         </div>
  
  </center>
</asp:Content>
