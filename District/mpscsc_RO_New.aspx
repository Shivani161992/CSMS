<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="mpscsc_RO_New.aspx.cs" Inherits="mpscsc_RO_New" Title="Release Order Of FCI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">

function calcAmount() {


    var control1 = document.getElementById('ctl00_ContentPlaceHolder1_txtrate');
    var control2 = document.getElementById('ctl00_ContentPlaceHolder1_txtroqty');
     var control3 = document.getElementById('ctl00_ContentPlaceHolder1_txtamount');
   
     var amt=(parseFloat(control1.value) * parseFloat(control2.value))
    
         control3.value =Math.round (amt*100)/100;  
       
      
   

}
</script>
<center>


<table cellpadding ="0" cellspacing ="0" class ="tablelayout" style="width: 617px">

<tr>
 <td style="width: 830px">
 <table border="1" cellpadding="0" cellspacing="0" style="padding:1; BORDER-COLLAPSE: collapse;  border-right: navy 3px double; border-top: navy 3px double; border-left: navy 3px double; border-bottom: navy 3px double; width: 619px;">
     <tr>
         <td align="center" colspan="4" style="border-right: white 1px solid; border-top: white 1px solid;
             font-size: 10pt; border-left: white 1px solid; color: white; border-bottom: white 1px solid;
             background-color: lightslategray">
             <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="Release Order of FCI"></asp:Label></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc">
         </td>
         <td style="border-right: white 1px solid; border-top: white 1px solid; font-size: 10pt;
             border-left: white 1px solid; color: white; border-bottom: white 1px solid; background-color: lightslategray">
             <asp:Label ID="Label3" runat="server" Text="District (Logged in)"></asp:Label></td>
         <td class="tdmarginro" style="border-right: white 1px solid; border-top: white 1px solid;
             font-size: 10pt; border-left: white 1px solid; color: white; border-bottom: white 1px solid;
             background-color: lightslategray">
             <asp:Label ID="lbldistrict" runat="server" BackColor="LightSlateGray" Font-Bold="True" Font-Size="11px" Width="106px" Font-Italic="True"></asp:Label></td>
         <td style="font-size: 10pt; position: static; background-color: #cfdcdc">
         <asp:TextBox ID="TextBox1" runat="server" Width="130px" Visible="False"></asp:TextBox></td>
     </tr>
     <tr>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc">
      <asp:Label ID="lblforDist" runat="server" Text="For District"></asp:Label></td>
         <td style="font-size: 10pt; position: static; background-color: #cfdcdc">
     <asp:DropDownList ID="ddldistrict" runat="server"  Width ="154px" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged" AutoPostBack ="false" >
     <asp:ListItem Value="01" Selected ="True">-Select-</asp:ListItem>
                          
     </asp:DropDownList></td>
         <td class="tdmarginro" style="font-size: 10pt; position: static; background-color: #cfdcdc">
         </td>
         <td style="font-size: 10pt; position: static; background-color: #cfdcdc">
         </td>
     </tr>
 <tr>
 <td class ="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
     <asp:Label ID="lblReleaseOrder" runat="server" Text="RO No." Width="74px"></asp:Label></td>
 <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
     <asp:TextBox ID="txtrono" runat="server" Width ="146px" OnTextChanged="txtrono_TextChanged" MaxLength="13"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtrono"
         ErrorMessage="RO Number Required" ValidationGroup="1">*</asp:RequiredFieldValidator>
 </td>
     <td class ="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         <asp:Label ID="lblReleaseOrderDate" runat="server" Text="RO Date"></asp:Label></td>
  <td style="background-color: #cfdcdc; font-size: 10pt; position: static;"> <asp:TextBox ID="txtrodate" runat="server" Width="130px"></asp:TextBox>
   <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_txtrodate'
	    });
	     </script>
  </td>
 </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         </td>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         </td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lbldovalidity" runat="server" Font-Size="12px" Text="Validity :" Width="68px"></asp:Label></td>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
            <asp:TextBox ID="txtrovalidity" runat="server" Width="130px"></asp:TextBox>
             <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_txtrovalidity'
	    });
	     </script>
             </td>
     </tr>
     <tr>
         <td class="tdmarginddl" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblmonth" runat="server" Text="Allotment Month" Width="109px"></asp:Label></td>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:DropDownList ID="ddlalotmm" runat="server"  Width ="153px" OnSelectedIndexChanged="ddlalotmm_SelectedIndexChanged">
                            <asp:ListItem Value="01">January</asp:ListItem>
                            <asp:ListItem Value="02">February</asp:ListItem>
                            <asp:ListItem Value="03">March</asp:ListItem>
                            <asp:ListItem Value="04">April</asp:ListItem>
                            <asp:ListItem Value="05">May</asp:ListItem>
                            <asp:ListItem Value="06">June</asp:ListItem>
                            <asp:ListItem Value="07">July</asp:ListItem>
                            <asp:ListItem Value="08">August</asp:ListItem>
                            <asp:ListItem Value="09">September</asp:ListItem>
                            <asp:ListItem Value="10">October</asp:ListItem>
                            <asp:ListItem Value="11">November</asp:ListItem>
                            <asp:ListItem Value="12">December</asp:ListItem>
                        </asp:DropDownList></td>
         <td class="tdmarginddl" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             &nbsp;<asp:Label ID="lblyear" runat="server" Text="Allotment Year"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
               <asp:DropDownList ID="ddlallot_year" runat="server" Width="153px" OnSelectedIndexChanged="ddlallot_year_SelectedIndexChanged">
                                </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginddl" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblCommodity" runat="server" Font-Size="12px" Text="Commodity"></asp:Label></td>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
      <asp:DropDownList ID="ddlcomdty" runat="server"  Width ="153px" OnSelectedIndexChanged="ddlcomdty_SelectedIndexChanged" AutoPostBack ="false" >
       <asp:ListItem Value="01" Selected ="True">-Select-</asp:ListItem>
      </asp:DropDownList></td>
         <td class="tdmarginddl" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblScheme" runat="server" Font-Size="12px" Text="Scheme"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
     <asp:DropDownList ID="ddlscheme" runat="server"  Width ="153px" AutoPostBack="True" OnSelectedIndexChanged="ddlscheme_SelectedIndexChanged">
      <asp:ListItem Value="01" Selected ="True">-Select-</asp:ListItem>
     </asp:DropDownList></td>
     </tr>
     <tr>
         <td class="tdmarginddl" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblqty" runat="server" Text="Alloted Quantity" Width="113px"></asp:Label></td>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:TextBox ID="txtalotqty" runat="server" Width="130px" ReadOnly="True" BackColor="#FFFF80"></asp:TextBox>
             (Qtls.)</td>
         <td class="tdmarginddl" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lbl_balqty" runat="server" Text="Balance Qty."></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:TextBox ID="txtbalance" runat="server" Width="130px" ReadOnly="True" BackColor="#FFFF80"></asp:TextBox>(Qtls.)</td>
     </tr>
  <tr>
  <td class ="tdmarginddl" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
      </td>
 <td style="background-color: #cfdcdc; font-size: 10pt; position: static;" align="left">
     &nbsp;</td>
     <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;" colspan="2">
         &nbsp;<asp:Label ID="lblmssg" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="#C00000" Visible="False" Width="55px"></asp:Label></td>
  
  </tr>
  <tr>
  <td class ="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
      <asp:Label ID="lblRateQuintal" runat="server" Text="Rate"></asp:Label></td>
 <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">
     <asp:TextBox ID="txtrate" runat="server"  Width ="130px" OnTextChanged="txtrate_TextChanged" BackColor="#FFFF80"></asp:TextBox>(Rs.)</td>
     <td class ="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         <asp:Label ID="lblQuantity" runat="server" Text="RO Quantity"></asp:Label></td>
  <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;"> <asp:TextBox ID="txtroqty" runat="server"  Width ="130px" OnTextChanged="txtroqty_TextChanged" MaxLength="13"></asp:TextBox>(Qtls.)</td>
  </tr>
  <tr>
 <td class ="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">  
     <asp:Label ID="lblamount" runat="server" Text="Amount"></asp:Label></td>
 <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">  <asp:TextBox ID="txtamount" runat="server"  Width ="130px" ReadOnly="True"></asp:TextBox>(Rs.)</td>
 <td class ="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">  </td>
 <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;"> &nbsp;</td>
 </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         </td>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static;" align="right">
             <asp:Label ID="lblPaymentMode" runat="server" Text="Payment Type"></asp:Label></td>
         <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:DropDownList ID="ddlpaymenttype" runat="server" Width="103px" OnSelectedIndexChanged="ddlpaymenttype_SelectedIndexChanged" AutoPostBack="True">
                 <asp:ListItem Value="P">Paid</asp:ListItem>
                 <asp:ListItem Value="F">Free Scheme</asp:ListItem>
             </asp:DropDownList></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         </td>
     </tr>
     <tr>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblddchekno" runat="server" Font-Size="12px" Text="DD/Chq. No. "></asp:Label></td>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         <asp:TextBox ID="txtckeckno" runat="server" Width="130px"></asp:TextBox>
     </td>
         <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="lblddchekdate" runat="server" Font-Size="12px" Text="DD/Chq. Date"></asp:Label></td>
         <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
            <asp:TextBox ID="txtddate" runat="server" Width="130px"></asp:TextBox>
              <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_txtddate'
	    });
	     </script>
             </td>
     </tr>
  <tr>
  <td class ="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;"> 
      <asp:Label ID="lblamountdd" runat="server" Font-Size="12px" Text="Amount"></asp:Label></td>
 <td  style="background-color: #cfdcdc; font-size: 10pt; position: static;">
     <asp:TextBox ID="txtchqamt" runat="server" Width="130px" OnTextChanged="txtchqamt_TextChanged"></asp:TextBox>
     <asp:Label ID="lblchqrs" runat="server" Text="(Rs.)"></asp:Label></td>
     <td class ="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         <asp:Label ID="lblBankName" runat="server" Font-Size="12px" Text="Bank Name"></asp:Label></td>
  <td  align ="left " style="background-color: #cfdcdc; font-size: 10pt; position: static;"> &nbsp;<asp:DropDownList ID="ddlbankname" runat="server" Width="153px">
             </asp:DropDownList></td>
  </tr>
  <tr>
  <td class ="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static;" >
      <asp:Label ID="lblRemark" runat="server" Text="Remarks"></asp:Label></td>
      <td align="left" colspan="3" rowspan="2" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
          <asp:TextBox ID="txtremark" runat="server"  Width ="404px" TextMode ="MultiLine" OnTextChanged="txtremark_TextChanged" ></asp:TextBox></td>
  </tr>
  <tr>
  <td style="background-color: #cfdcdc; font-size: 10pt; position: static;"> </td>
  <td style="background-color: #cfdcdc; font-size: 10pt; position: static;"> </td>
  
  </tr>
  <tr>
  <td style="background-color: #cfdcdc; font-size: 10pt; position: static;"></td>
  <td colspan="3" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
      <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
          ValidationGroup="1" Width="412px" ShowSummary="False" />
  </td>
  </tr>
  <tr>
   
 <td style="background-color: #cfdcdc; font-size: 10pt; position: static;"></td>
      <td align="left" colspan="3" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
      <asp:Button ID="btnsave" runat="server" Text="Submit" OnClick="btnsave_Click" ValidationGroup="1" Width="146px" Font-Bold="True" ForeColor="Navy"/>&nbsp;
          <asp:Button ID="btnclose" runat="server" Text="Close" OnClick="btnclose_Click" Width="114px" Font-Bold="True" ForeColor="Navy" />
      </td>
     
      

  
  </tr>
     <tr>
         <td style="background-color: #cfdcdc; font-size: 10pt; position: static;">
         </td>
         <td align="left" colspan="3" style="background-color: #cfdcdc; font-size: 10pt; position: static;">
             <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label></td>
     </tr>
 </table>
 
 </td>
</tr>
</table>

</center>

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
</asp:Content>

