<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master"  AutoEventWireup="true" CodeFile="DeliveryOrder_mpscsc.aspx.cs" Inherits="delivery" Title="Delivery Order" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
    
	<div style="text-align: right; height: 1025px;" >
    <asp:Panel ID="panelDO" runat ="server" Height="808px"  >
        <table style="width: 608px; height: 700px">
        <tr><td align="center" style="background-color: #cccccc; width: 713px;" valign="top">
            <asp:Label ID="lbldo" runat="server" Font-Bold="True" ForeColor="Black" Text="Delivery Order"
                Width="108px"></asp:Label></td></tr>
            <tr>
                <td rowspan="2" style="width: 713px; height: 610px" valign="top">
               <%-- <fieldset style="vertical-align: top; text-align: left; height: 648px; width: 632px;" >
                <legend style="color: white; background-color: maroon">
                Permit / Release Order Details
                </legend>--%>
                    <table border="1" cellpadding="0" cellspacing="0" style="width: 680px">
                        <tr>
                            <td align="left" colspan="6" style="font-size: 10pt; position: static; background-color: #cfdcc8; border-right: gray 2px solid; border-top: gray 2px solid; border-left: gray 2px solid; border-bottom: gray 2px solid;">
                            <table style="width: 688px">
                                <tr>
                                    <td colspan="6" style="border-right: silver 1px solid; border-top: silver 1px solid;
                                        border-left: silver 1px solid; border-bottom: silver 1px solid">
                                        <asp:Label ID="Label5" runat="server" Text="District" Font-Size="14pt" ForeColor="#C00000" Width="96px"></asp:Label>
                                        <asp:Label ID="lblDist" runat="server" Font-Size="14pt" ForeColor="#C00000" Width="168px"></asp:Label>
                                <asp:Label ID="Label1" runat="server" ForeColor="Blue" Font-Size="Medium"></asp:Label></td>
                                </tr>
                            <tr> 
                            <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; background-color: #ffcc66; height: 1px;">
                                <asp:Label ID="lblpermitno1" runat="server" Font-Size="12px" Text="Permit No."></asp:Label></td>
                             <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; background-color: #ffcc66; height: 1px;" colspan="5"> 
                                <asp:DropDownList ID="ddlpermit" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpermit_SelectedIndexChanged" Width="256px">
                                </asp:DropDownList>&nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp;
                             </td>
                            </tr>
                                <tr>
                                    <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; background-color: #ffcc66;">
                                <asp:Label ID="lblpermitno" runat="server" Font-Size="12px" Text="Permit No." Width="60px"></asp:Label></td>
                                    <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; background-color: #ffcc66;">
                                <asp:TextBox ID="tx_permit_no" runat="server" Width="143px" MaxLength="15" OnTextChanged="tx_permit_no_TextChanged" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">0</asp:TextBox></td>
                                    <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; background-color: #ffcc66;">
                                <asp:Label ID="lblpermitdate" runat="server" Font-Size="12px" Text="PermitDate"></asp:Label></td>
                                    <td colspan="3" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; background-color: #ffcc66;">
                                   <asp:TextBox ID="tx_permit_date" runat="server" Width="119px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
             <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_tx_permit_date'
	    });
	     </script>
                                 <asp:TextBox ID="tx_permit_date1" runat="server" BackColor="MenuBar" Font-Bold="False"
                                    ForeColor="Black" ReadOnly="True" Width="16px" Visible="False"></asp:TextBox>&nbsp;
                                        <asp:CustomValidator ID="CustomValidator4" runat="server" ControlToValidate="tx_permit_date" Display="Dynamic" ErrorMessage="दिनांक dd/MM/yyyy फोर्मेट मे ही चुने" OnServerValidate="CustomValidator4_ServerValidate" ValidateEmptyText="True" ValidationGroup="1">*</asp:CustomValidator></td>
                                </tr>
                                <tr>
                                    <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; background-color: #ffcc66;">
                                <asp:Label ID="lbltoissue" runat="server" Font-Size="12px" Text="Issue To"></asp:Label></td>
                                    <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; background-color: #ffcc66;">
                                        <asp:DropDownList ID="ddl_issueto" runat="server" Width="184px">
                                            
                                            <asp:ListItem Value="MPSCSC">MPSCSC</asp:ListItem>
                                           
                                        </asp:DropDownList></td>
                                    <td colspan="4" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; background-color: #ffcc66;">
                                        &nbsp;
                                <asp:Label ID="lbl_transporter" runat="server" Text="Transporter" Font-Size="Small" Width="40px" Visible="False"></asp:Label>
                                <asp:DropDownList ID="ddl_lead" runat="server" Font-Names="Arial Unicode MS" Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="ddl_lead_SelectedIndexChanged" Width="216px">
                                </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid">
                                <asp:Label ID="lblallotmonth" runat="server" Font-Size="12px" Text="Allot. Month"
                                    Width="67px"></asp:Label></td>
                                    <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid">
                                <asp:DropDownList ID="ddl_allot_month" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_allot_month_SelectedIndexChanged" Width="153px">
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
                                    <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid">
                                        <asp:Label ID="lblallotyear" runat="server" Font-Size="12px" Text="Allot. Year" Width="101px"></asp:Label></td>
                                    <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                        <asp:DropDownList ID="ddd_allot_year" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddd_allot_year_SelectedIndexChanged">
                                </asp:DropDownList>&nbsp;
                                    </td>
                                    <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid" colspan="2">
                                        &nbsp; &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                <asp:Label ID="lblCommodity" runat="server" Font-Size="12px" Text="Commodity"></asp:Label></td>
                                    <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                <asp:DropDownList ID="ddl_commodity" runat="server" ValidationGroup="1" AutoPostBack="True" OnSelectedIndexChanged="ddl_commodity_SelectedIndexChanged" Width="153px">
                                </asp:DropDownList></td>
                                    <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                <asp:Label ID="lblScheme" runat="server" Font-Size="12px" Text="Scheme"></asp:Label></td>
                                    <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
                                &nbsp;
                                <asp:DropDownList ID="ddl_scheme" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_scheme_SelectedIndexChanged">
                                </asp:DropDownList></td>
                                    <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" colspan="2">
                                        &nbsp; &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid">
                                <asp:Label ID="lblbalcomdty" runat="server" Font-Size="12px" Text="Balance Stock of Commodity"></asp:Label></td>
                                    <td colspan="2" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid">
                                <asp:TextBox ID="txtcomdty_bal" runat="server" BackColor="MenuBar" Font-Bold="False"
                                    ForeColor="Black" ReadOnly="True" Width="93px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>Qtls.</td>
                                    <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid">
                                <asp:Label ID="lblratetype" runat="server" Font-Size="12px" Text="Rate Type" Width="53px"></asp:Label></td>
                                    <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; width: 66px;">
                                <asp:DropDownList ID="ddl_rate_type" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_rate_type_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Value="R">Rural</asp:ListItem>
                                    <asp:ListItem Value="U">Urban</asp:ListItem>
                                </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; background-color: #ffffcc;">
                                <asp:Label ID="lbl_alloc" runat="server" Text="Allocation for the Month"></asp:Label>&nbsp;</td>
                                    <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; background-color: #ffffcc;">
                                        <asp:TextBox ID="tx_allot_qty" runat="server" MaxLength="8" Width="58px" BackColor="MenuBar" ReadOnly="True" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox><asp:Label
                                    ID="lbl_qtalloc" runat="server" Text="Qtls."></asp:Label></td>
                                    <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; background-color: #ffffcc;">
                                <asp:Label ID="lbl_issue" runat="server" Text="DO already Issued Qty."></asp:Label></td>
                                    <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; background-color: #ffffcc;">
                                <asp:TextBox ID="tx_already_iqty" runat="server" BackColor="MenuBar" MaxLength="8"
                                    ReadOnly="True" Width="90px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
                                    <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; background-color: #ffffcc;">
                                        <asp:Label ID="lbl_qtissue" runat="server"
                                        Text="Qtls."></asp:Label></td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; width: 66px; background-color: #ffffcc; height: 1px;">
                                <asp:Label ID="lbl_curbal" runat="server" Text="Current Balance Eligibility" Width="168px" Height="20px"></asp:Label>&nbsp;</td>
                                    <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; width: 66px; background-color: #ffffcc; height: 1px;">
                                <asp:TextBox ID="tx_balQty" runat="server" BackColor="MenuBar" MaxLength="8" ReadOnly="True"
                                    Width="58px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox><asp:Label ID="lbl_qtcur" runat="server" Text="Qtls."></asp:Label></td>
                                    <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; width: 66px; background-color: #ffffcc; height: 1px;">
                                <asp:Label ID="lbl_stock" runat="server" Text="Balance Stock of Commodity/Scheme" Width="176px" Height="13px"></asp:Label></td>
                                    <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; width: 66px; background-color: #ffffcc; height: 1px;">
                                <asp:TextBox ID="tx_bal_ic" runat="server" BackColor="MenuBar" MaxLength="8" ReadOnly="True"
                                    Width="90px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
                                    <td style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; width: 66px; background-color: #ffffcc; height: 1px;">
                                        <asp:Label ID="lbl_qtstk" runat="server" Text="Qtls."></asp:Label></td>
                                </tr>
                            </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; background-color: lightslategray; color: white; position: static; height: 17px;">
                            </td>
                            <td align="left" style="font-size: 10pt;   background-color: lightslategray; color: white; position: static; height: 17px;">
                            </td>
                            <td align="center" style="font-size: 10pt; background-color: lightslategray; color: white; position: static; height: 17px;" colspan="2">
                                <asp:Label ID="lbldodetails" runat="server" Font-Bold="True" Font-Size="12px" Text="Delivery Order Details" Width="152px"></asp:Label></td>
                            <td style="font-size: 10pt;   background-color: lightslategray; color: white; position: static; height: 17px;">
                            </td>
                            <td align="left" style="font-size: 10pt; background-color: lightslategray; color: white; position: static; height: 17px;">
                            </td>
                        </tr>
                        <tr>
                            <%--<td style="  font-size: 10pt; position: static; background-color: #cfdcc8; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
                                <asp:Label ID="lbldono" runat="server" Font-Size="12px" Text="DO No." Width="48px"></asp:Label></td>
                            <td style="  font-size: 10pt; position: static; background-color: #cfdcc8; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
                                <asp:TextBox ID="tx_do_no" runat="server" Width="89px" MaxLength="15" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>*
                                </td>--%>
                            <td style="font-size: 10pt; position: static; background-color: #ffffcc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
                                <asp:Label ID="lbldodate" runat="server" Font-Size="12px" Text="DO Date" Width="46px"></asp:Label></td>
                            <td style="  font-size: 10pt; position: static; background-color: #ffffcc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
                            <asp:TextBox ID="tx_do_date" runat="server" Width="119px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
             <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_tx_do_date'
	    });
	     </script>

                                <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="tx_do_date" Display="Dynamic" ErrorMessage="दिनांक dd/MM/yyyy फोर्मेट मे ही चुने" OnServerValidate="CustomValidator2_ServerValidate" ValidateEmptyText="True" ValidationGroup="1">*</asp:CustomValidator></td>
                            <td style="font-size: 10pt; position: static; background-color: #ffffcc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
                                <asp:Label ID="lbldovalidity" runat="server" Font-Size="12px" Text="Validity Date"
                                    Width="68px"></asp:Label></td>
                            <td style="  font-size: 10pt; position: static; background-color: #ffffcc; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
                           <asp:TextBox ID="tx_do_validity" runat="server" Width="119px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
             <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_tx_do_validity'
	    });
	     </script>

                                <asp:CustomValidator ID="CustomValidator3" runat="server" ControlToValidate="tx_do_validity" Display="Dynamic" ErrorMessage="दिनांक dd/MM/yyyy फोर्मेट मे ही चुने" OnServerValidate="CustomValidator3_ServerValidate" ValidateEmptyText="True" ValidationGroup="1">*</asp:CustomValidator></td>
                        </tr>
                        <tr>
                            <td style="font-size: 10pt; background-color: lightslategray;  color: white; position: static;">
                            </td>
                            <td style="font-size: 10pt; background-color: lightslategray;  color: white; position: static;">
                            </td>
                            <td align="center" colspan="2" style="font-size: 10pt; background-color: lightslategray; color: white; position: static;">
                                <asp:Label ID="lbldodetailsfps" runat="server" Font-Bold="True" Font-Size="12px"
                                    Text="DO Details FPS - Wise"></asp:Label></td>
                            <td style="font-size: 10pt; background-color: lightslategray;  color: white; position: static;">
                            </td>
                            <td style="font-size: 10pt; background-color: lightslategray;  color: white; position: static;">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" style="font-size: 10pt; position: static; background-color: #cfdcc8; height: 439px;">
                            <table style="width: 100%; height: 296px;">
                            <tr>
                                <td align="left" colspan="7" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid; height: 1px;">
                                <asp:Label ID="lbl_blk" runat="server" ForeColor="Black" Text="FPS Block" Width="72px"></asp:Label>
                                <asp:DropDownList ID="ddl_block" runat="server" OnSelectedIndexChanged="ddl_block_SelectedIndexChanged" Font-Names="Arial Unicode MS" Font-Size="Small" AutoPostBack="True" Width="280px">
                                </asp:DropDownList></td>
                            </tr>
                                <tr>
                                    <td align="left" colspan="7" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid">
                                                <table style="width: 624px; height: 256px;" border="1">
                                                    <tr>
                                                        <td colspan="2" valign="top" align="left" style="height: 200px">
                                                            <asp:Label ID="lbl_fps" runat="server" ForeColor="Black" Text="FPS"></asp:Label>
                                                            <asp:Label ID="Label4" runat="server" Text="Name"></asp:Label>
                                                            <asp:ListBox ID="ddl_fps_name" runat="server" Height="200px" Width="328px" OnSelectedIndexChanged="ddl_fps_name_SelectedIndexChanged" AutoPostBack="True"></asp:ListBox></td>
                                                        <td colspan="4" valign="top" style="width: 344px; height: 200px;">
                                                            <div style="text-align: left">
                                                                <table style="width: 63%; height: 104px" border="1">
                                                                    <tr>
                                                                        <td colspan="3" style="height: 26px" valign="top">
                                                                            <asp:Label ID="lblFPSNAme" runat="server" Font-Bold="True" Font-Names="Arial Unicode MS"
                                                                                Font-Size="Medium" ForeColor="Red" Width="328px" Height="14px"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 118px; height: 1px;">
                                <asp:Label ID="lbl_allotqty" runat="server" ForeColor="Black" Text="Allotment Qty."></asp:Label></td>
                                                                        <td colspan="2" style="height: 1px; width: 138px;">
                                <asp:TextBox ID="tx_allot_qty2" runat="server" BackColor="MenuBar" MaxLength="8"
                                    Width="90px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 118px; height: 1px;">
                                <asp:Label ID="lbl_issueqty" runat="server" ForeColor="Black" Style="z-index: 100"
                                    Text="Already Issued Qty"></asp:Label></td>
                                                                        <td colspan="2" style="height: 1px; width: 138px;">
                                <asp:TextBox ID="tx_already_iqty2" runat="server" BackColor="MenuBar" MaxLength="8" Width="90px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 118px; height: 1px;" contenteditable="">
                                <asp:Label ID="lbl_balqty" runat="server" ForeColor="Black" Style="z-index: 100;
                                    left: -85px; top: 0px" Text="Balance Qty"></asp:Label></td>
                                                                        <td colspan="2" style="height: 1px; width: 138px;">
                                <asp:TextBox ID="tx_balQty2" runat="server" BackColor="MenuBar" MaxLength="8"
                                    Width="90px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True"></asp:TextBox>
                                <asp:Label ID="lbl_qtls" runat="server" ForeColor="Black" Text="Qtls."></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 118px; height: 1px;">
                                <asp:Label ID="totaldoqty" runat="server" Font-Size="12px" Text="Total DO Quantity "></asp:Label></td>
                                                                        <td colspan="2" style="height: 1px; width: 138px;">
                                        <asp:TextBox ID="tx_tot_qty" runat="server" BackColor="MenuBar" MaxLength="8"
                                    Width="90px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ReadOnly="True"></asp:TextBox>
                                <asp:Label ID="lbl_qtls2" runat="server" ForeColor="Black" Text="Qtls."></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 118px; height: 1px;">
                                <asp:Label ID="lblQuantity" runat="server" Font-Size="12px" ForeColor="Black" Text="Quantity"></asp:Label></td>
                                                                        <td colspan="2" style="height: 1px; width: 138px;">
                                <asp:TextBox ID="tx_qty" runat="server" MaxLength="13" Width="89px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ></asp:TextBox><asp:Label
                                    ID="Label3" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red" Text="*" style="left: 0px"></asp:Label>
                                <asp:Label ID="lbl_qtls1" runat="server" ForeColor="Black" Text="Qtls."></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 118px">
                                                                            <asp:Button ID="BTNReview" runat="server" Text="View Added FPS" Width="128px" Enabled="False" OnClick ="BTNReview_Click" Visible="False" /></td>
                                                                        <td colspan="2" style="width: 138px">
                                <asp:Button ID="Button1" runat="server" Text="Add FPS" OnClick="Button1_Click" ValidationGroup="2" Width="120px" /></td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="7" style="border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid">
               
                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Width="100%" Visible="False" Height="64px" >
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Font-Bold="False"
                            Font-Size="Small" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Width="100%" Height="72px">
                            <RowStyle ForeColor="#000066" />
                            <Columns>
                            
                                <asp:CommandField SelectText="Delete" ShowSelectButton="True">
                                    <ItemStyle Font-Bold="False" />
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
                                <tr>
                                    <td colspan="7" style="border-right: silver 1px solid; border-top: silver 1px solid;
                                        border-left: silver 1px solid; border-bottom: silver 1px solid">
                    <table style="width: 100%; height: 160px;" border="1" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                <asp:Label ID="lblRateQuintal" runat="server" Font-Size="12px" Text="Rate/Quintal"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8;">
                                <asp:TextBox ID="tx_rate_qt" runat="server"
                                    MaxLength="8" Width="90px" ReadOnly="True" BackColor="Linen" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
                            <td align="left" colspan="2" style="background-color: #cfdcc8;">
                                &nbsp;<asp:Label ID="lblPaymentMode" runat="server" Font-Size="12px" Text="Payment Mode"
                                    Width="92px"></asp:Label></td>
                            <td align="left" colspan="2" style="background-color: #cfdcc8">
                                &nbsp;
                            <asp:DropDownList ID="ddl_pmode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_pmode_SelectedIndexChanged" Width="224px">
                                <asp:ListItem Value="Se">--Select--</asp:ListItem>
                                <asp:ListItem Value="D">DD / Cheque</asp:ListItem>
                                <asp:ListItem Value="R">Free Scheme / Credit </asp:ListItem>
                                <asp:ListItem Value="A">Cash</asp:ListItem>
                                 <asp:ListItem Value="AD">Advance Payment</asp:ListItem>
                                  <asp:ListItem Value="OP">Online Payment</asp:ListItem>
                            </asp:DropDownList>&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8">
                                <asp:Label ID="lbltotamt" runat="server" Font-Size="12px" Text="Total Amount" Width="78px"></asp:Label></td>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcc8">
                                <asp:TextBox ID="tx_tot_amt" runat="server" BackColor="Linen" MaxLength="8" ReadOnly="True"
                                    Width="100px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                <asp:Label ID="Label27" runat="server" ForeColor="Black" Text="Rs."></asp:Label></td>
                            <td align="left" colspan="2" style="background-color: #cfdcc8">
                                <asp:Label ID="lblddchekno" runat="server" Font-Size="12px" Text="DD/Chq. No. "></asp:Label></td>
                            <td align="left" style="background-color: #cfdcc8; width: 126px;">
                                <asp:TextBox ID="tx_dd_no" runat="server" Width="120px" MaxLength="50" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox><asp:Label ID="lbl_ddno" runat="server" Font-Bold="True" Font-Size="Medium" Text="*" ForeColor="Red"></asp:Label></td>
                            <td align="left" style="background-color: #cfdcc8">
                            <asp:TextBox ID="tx_dd_date1" runat="server" BackColor="Linen" Font-Bold="False"
                                    ForeColor="Black" ReadOnly="True" Width="70px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Visible="False"></asp:TextBox></td>
                        </tr>
                        
                        <tr>
                            <td align="left" style="background-color: #cfdcc8">
                                <asp:Label ID="lblamount" runat="server" Font-Size="12px" Text="Amount"></asp:Label></td>
                            <td align="left" style="background-color: #cfdcc8">
                                <asp:TextBox ID="tx_dd_amount" runat="server" Width="112px" MaxLength="12" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                <asp:Label
                                    ID="Label2" runat="server" ForeColor="Black" Text="Rs."></asp:Label></td>
                            <td align="left" colspan="2" style="background-color: #cfdcc8">
                                <asp:Label ID="lblddchekdate" runat="server" Font-Size="12px" Text="DD/Chq. Date"></asp:Label></td>
                            <td align="left" style="background-color: #cfdcc8; width: 126px;">
                           <asp:TextBox ID="tx_dd_date" runat="server" Width="119px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
             <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_tx_dd_date'
	    });
	     </script>

                                <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="tx_dd_date" Display="Dynamic" ErrorMessage="दिनांक dd/MM/yyyy फोर्मेट मे ही चुने" OnServerValidate="CustomValidator1_ServerValidate" ValidateEmptyText="True" ValidationGroup="1">*</asp:CustomValidator></td>
                            <td align="left" style="background-color: #cfdcc8;">
                                <asp:Label
                                    ID="lbl_amt" runat="server" Text="*" style="z-index: 100" ForeColor="Red"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" style="background-color: #cfdcc8;">
                                <asp:Label ID="lblBankName" runat="server" Font-Size="12px" Text="Bank Name"></asp:Label></td>
                            <td align="left" colspan="3" style="background-color: #cfdcc8;">
                                <asp:DropDownList ID="ddl_bank" runat="server" >
                                </asp:DropDownList></td>
                            <td align="left" style="background-color: #cfdcc8; width: 126px;">
                                </td>
                            <td align="left" style="background-color: #cfdcc8;">
                                &nbsp;
                                </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt;  position: static; background-color: #cfdcdc;" colspan="6">
                                &nbsp;<%--       <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="tx_do_no" ErrorMessage="Please enter delivery order no."
                                    Height="0px" ValidationGroup="1" Width="0px">*</asp:RequiredFieldValidator>--%>
                                <asp:Label ID="LbldisplayDo" runat="server" Font-Bold="True" ForeColor="#0000C0"
                                    Text="DO Number is :- " Visible="False" Width="99px"></asp:Label><asp:Label ID="LblShowDonum"
                                        runat="server" Font-Bold="True" ForeColor="#C00000" Visible="False" Width="122px"></asp:Label>
                                &nbsp;<asp:Button ID="save" runat="server" Text="Save" Font-Bold="False" Font-Size="Medium" Width="104px" OnClick="save_Click" ValidationGroup="1" Enabled="False" BackColor="Silver" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" /><asp:Button ID="btnClose" runat="server" Font-Size="Medium" OnClick="btnClose_Click"
                                    Text="Close" Width="90px" BackColor="Silver" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                <asp:Button ID="btn_new" runat="server" Font-Bold="False" Font-Size="Medium" OnClick="btn_new_Click"
                                    Text="New" Width="75px" BackColor="Silver" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />&nbsp;
        <asp:HyperLink id="hlinkpdo" runat="server" NavigateUrl="#" Visible="False" Font-Size="10pt">Issue Delivery Order</asp:HyperLink></td>
                        </tr>
                        <tr>
                            <td align="left" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 20px;">
                                <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator3" runat="server" ControlToValidate="tx_dd_no" ErrorMessage="Please enter DD No."
                                    Height="0px" ValidationGroup="1" Width="0px">*</asp:RequiredFieldValidator><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator4" runat="server" ControlToValidate="tx_dd_amount"
                                    ErrorMessage="Please enter DD Amount" Height="0px" ValidationGroup="1" Width="0px">*</asp:RequiredFieldValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddl_commodity"
                        ErrorMessage="Please enter commodity" Height="0px" ValidationGroup="2" Width="0px">*</asp:RequiredFieldValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tx_rate_qt"
                        ErrorMessage="Please enter Rate" Height="0px" ValidationGroup="2"
                        Width="0px">*</asp:RequiredFieldValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tx_qty"
                        ErrorMessage="Please enter quantity" Height="0px" ValidationGroup="2" Width="0px">*</asp:RequiredFieldValidator></td>
                            <td align="right" colspan="3" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 20px;">
                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" Font-Size="Small" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="2" Width="232px" Height="23px" />
                            </td>
                            <td align="left" colspan="2" style="font-size: 10pt; position: static; background-color: #cfdcdc; height: 20px;">
       
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Size="Small" ShowMessageBox="True"
                        ShowSummary="False" Style="left: 0px; top: 0px" ValidationGroup="1" Height="1px" Width="221px" />
                            </td>
                        </tr>
                    </table>
                                       
                                    </td>
                                </tr>
                            </table>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <%-- </fieldset>--%>
                                    <%--<asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Width="100%" Height="48px" Visible="False" >
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Font-Bold="False"
                            Font-Size="Small" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Width="689px">
                            <RowStyle ForeColor="#000066" />
                            <Columns>
                            
                                <asp:CommandField SelectText="Delete" ShowSelectButton="True">
                                    <ItemStyle Font-Bold="False" />
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
                    </asp:Panel>--%>
                <%--<fieldset style="vertical-align: top; text-align: left; height: 160px;" >
                <legend style="color: white; background-color: maroon">
                Payment Details
                </legend>--%>
                    <%--</fieldset>--%>
                </td>
            </tr>
            
           
        </table>
        
        </asp:Panel>
         </div>
</asp:Content>

