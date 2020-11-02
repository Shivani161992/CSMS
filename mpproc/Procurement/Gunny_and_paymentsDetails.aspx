<%@ Page Language="C#" MasterPageFile="~/mpproc/MasterPage/AgencyMaster.master" AutoEventWireup="true" CodeFile="Gunny_and_paymentsDetails.aspx.cs" Inherits="mpproc_Procurement_Gunny_and_paymentsDetails" Title="Gunny and Payments Details" Debug="true"  %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <script type="text/javascript" src="../../js/calendar_eu.js"></script>   
<script type="text/javascript" language="javascript">
function CheckIsNumericInt(tx)
{
var AsciiCode = event.keyCode;
var txt=tx.value;
var txt2 = String.fromCharCode(AsciiCode);
var txt3=txt2*1;
if ((AsciiCode < 46) || (AsciiCode > 57) ||(AsciiCode == 46 ) )
{
alert('Please enter only Numeric');
event.cancelBubble = true;
event.returnValue = false;
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
            if (count > 2 && AsciiCode != 8)  
            {
                alert("Only 2 decimal digits allowed.");
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
    
<table border ="0" cellpadding ="2" cellspacing ="2"   style="font-size :14px; width:700px "> 
              <tr>
                  <td align="left" colspan="2" style="height: 20px">
                      <asp:Label ID="lblmsg" runat="server" ForeColor="Green"></asp:Label></td>
              </tr>
        
            <tr>
 
            
            <td colspan="4" style="background-color: #cccc66; font-size:12px; height: 19px;">
            
            <asp:Label ID="lblmsgWheat_FWise" runat="server" Text="Gunny and payments Details from Agency To PC (Society)" Font-Bold="True" ForeColor="Green" Font-Italic="True" Font-Size="15px"></asp:Label>
           
            </td>
                <td colspan="1" style="font-size: 12px; height: 19px; background-color: #cccc66">
                </td>
            
            </tr>
          <tr>
              <td colspan="1" style="font-size: 12px; width: 147px; height: 19px; background-color: #cccc66">
              </td>
              <td align="right" colspan="5" style="font-size: 12px; height: 19px; background-color: #cccc66">
                  <asp:Label ID="lblMas_Q_IS" runat="server" Text="( Amount in Rs.)"></asp:Label></td>
          </tr>
            <tr>
            <td align="left" style="background-color: gainsboro;">
                <asp:Label ID="lblDistrict" runat="server" ForeColor="Navy" Text="District :-"></asp:Label><asp:Label
                    ID="lblDistrict_Res" runat="server" ForeColor="Navy" Text="lblDistrict_Res"></asp:Label>
                 
            </td>
            
            <td style="height: 26px; background-color: #ffffcc;" align="left">
                <asp:Label ID="lblAgency1" runat="server" ForeColor="Navy" Text="Agency:-"></asp:Label><asp:Label
                    ID="lblAgency_Res" runat="server" ForeColor="Navy" Text="lblAgency_Res" Width="82px"></asp:Label></td>
          
             <td align="left" style="background-color: gainsboro;">
                 <asp:Label ID="lblMar_Seas" runat="server" ForeColor="Navy" Text="Marketing Season :-"></asp:Label><asp:Label
                     ID="lblMar_Seas_Res" runat="server" ForeColor="Navy" Text="lblMar_Seas_Res"></asp:Label>
                 
            </td>
            
            <td  align="left" style="height: 26px; background-color: #ffffcc;">
                &nbsp;<asp:Label ID="lblCropYear1" runat="server" ForeColor="Navy" Text="Crop Year :-"></asp:Label><asp:Label
                    ID="lblCrop_Y_Res" runat="server" ForeColor="Navy" Text="lblCrop_Y_Res" Width="87px"></asp:Label></td>
                <td align="left" style="background-color: gainsboro;">
                </td>
         
         
            </tr>
            
              <tr>
     <td align="left" style="background-color: gainsboro;">
        <asp:Label ID="lblAgency" runat="server" Text="Agency" ForeColor="#004040"></asp:Label>
    
    
    </td>
    <td align="left" style="background-color: #ffffcc;">
    
        <asp:DropDownList ID="DDLAgency" runat="server" Enabled="False" Width="124px">
        </asp:DropDownList>
    
    
    </td>
  
      <td align="left" style="background-color: gainsboro;">
        <asp:Label ID="lblCommodity" runat="server" Text="Commodity" ForeColor="#004040"></asp:Label>
    
    
    </td>
    <td align="left" style="background-color: #ffffcc;">
    
        <asp:DropDownList ID="DDL_Commodity" runat="server" Width="120px">
        </asp:DropDownList>
    
    
    </td>
                  <td align="left" style="background-color: gainsboro;">
                  </td>
  
    </tr>
          <tr>
              <td align="left" style="width: 147px; height: 26px; background-color: gainsboro;">
                  <asp:Label ID="lbl_Transact_Date" runat="server" Text="Transaction Date:" Width="104px"></asp:Label></td>
              <td align="left" style="background-color: #ffffcc;">
                  <asp:TextBox ID="txt_transaction_date" runat="server" Width="116px"></asp:TextBox>
                    <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_txt_transaction_date'
	    });
	     </script>     
                  </td>
              <td align="left" style="width: 223px; height: 26px; background-color: gainsboro;">
                  <asp:Label ID="lbl_PC_Name" runat="server" Text="Purchase Center Name:" Width="207px"></asp:Label></td>
              <td align="left" style="background-color: #ffffcc;">
                  <asp:DropDownList ID="DDL_PC_Name" runat="server" Enabled="False" Width="122px">
                  </asp:DropDownList></td>
              <td align="left" style="height: 26px; width: 147px; background-color: gainsboro;">
                  <asp:Button ID="Button1" runat="server" Text="Fetch" Width="79px" OnClick="Button1_Click" /></td>
          </tr>
    <tr>
        <td align="left" style="width: 147px; height: 26px; background-color: gainsboro">
            <asp:Label ID="lbl_AmPaid" runat="server" Text=" Amount Paid By Agency:(In Rs.)"
                Width="153px"></asp:Label></td>
        <td align="left" style="height: 26px; background-color: #ffffcc;">
            <asp:TextBox ID="txt_AmountPaid" runat="server" Width="116px"></asp:TextBox>
            <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label></td>
        <td align="left" style="width: 223px; height: 26px; background-color: gainsboro">
            <asp:Label ID="lbl_GBISSu" runat="server" Text="Gunny Bags Issued : ( By Agency to Society)"
                Width="203px"></asp:Label></td>
        <td align="left" style="height: 26px; background-color: #ffffcc;">
            <asp:TextBox ID="txt_GunnyIssued" runat="server" Width="116px"></asp:TextBox>
            <asp:Label ID="Label5" runat="server" ForeColor="Red" Text="*"></asp:Label></td>
        <td align="left" style="height: 26px; width: 147px; background-color: gainsboro;">
        </td>
    </tr>
          <tr>
              <td align="left" style="width: 147px; height: 26px; background-color: gainsboro;">
                  <asp:Label ID="lbl_GunRet" runat="server" Text="Gunny Returned  (By Society to Agency)"
                      Width="161px"></asp:Label></td>
              <td align="left" style="height: 26px; background-color: #ffffcc;">
                  <asp:TextBox ID="txt_GunnyReturned" runat="server" Width="116px"></asp:TextBox>
                  <asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*"></asp:Label></td>
              <td align="left" style="width: 223px; height: 26px; background-color: gainsboro;">
                  <asp:Label ID="lbl_Bill_Receiv" runat="server" Text=" Bill Received Amt from Society: (In Rs.) " Width="210px"></asp:Label></td>
              <td align="left" style="height: 26px; background-color: #ffffcc;">
                  <asp:TextBox ID="txtBillAmt" runat="server" Width="116px"></asp:TextBox>
                  <asp:Label ID="Label6" runat="server" ForeColor="Red" Text="*"></asp:Label></td>
              <td align="left" style="height: 26px; width: 147px; background-color: gainsboro;">
              </td>
          </tr>
          <tr>
              <td align="left" style="width: 147px; height: 26px; background-color: gainsboro;">
                  </td>
              <td align="left" style="height: 26px; background-color: #ffffcc;">
                  <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update"
                      Visible="False" Width="137px" />
                  <asp:Label ID="Label7" runat="server" Width="145px"></asp:Label></td>
              <td align="left" style="height: 26px; background-color: gainsboro;">
                  <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" Width="92px" />
                  <asp:Button ID="btn_Reset" runat="server" Text="Reset" Width="107px" OnClick="btn_Reset_Click" /></td>
              <td align="left" style="height: 26px">
                  &nbsp;<asp:Label ID="lbltrans" runat="server" Width="159px"></asp:Label></td>
              <td align="left" style="height: 26px; width: 147px; background-color: gainsboro;">
              </td>
          </tr>
    <tr>
    <td colspan="4" style="background-color: #cccc66;  height: 5px;">
            
          
           
            </td>
        <td colspan="1" style="height: 5px; background-color: #cccc66">
        </td>
    </tr>
     <tr >
         <td align="left" colspan="5">
        &nbsp;<asp:GridView ID="GridView_Wheat_Proc" runat="server" AutoGenerateColumns="False"
            Font-Size="10px" OnSelectedIndexChanged="GridView_Wheat_Proc_SelectedIndexChanged" CellPadding="4" ForeColor="#333333" GridLines="None">
            <Columns>
                <asp:CommandField HeaderText="Action" ShowSelectButton="True" />
                <asp:BoundField DataField="TransactionID" HeaderText="Transaction ID" SortExpression="TransactionID" />
                <asp:BoundField DataField="PCType_ID_Agency" HeaderText="Agency" SortExpression="PCType_ID_Agency" Visible="False" />
                <asp:BoundField DataField="PCID" HeaderText="Purchase Center" SortExpression="PCID" Visible="False" />
                <asp:BoundField DataField="PaymentToPC" HeaderText="Payment To Society" SortExpression="PaymentToPC" />
                <asp:BoundField DataField="GunnyBagsIssuedtoPC" HeaderText="Gunny Bags Issued To Society"
                    SortExpression="GunnyBagsIssuedtoPC" />
                <asp:BoundField DataField="GunnyBagsReturnedbyPC" HeaderText="Gunny Bags Returned by Society"
                    SortExpression="GunnyBagsReturnedbyPC" />
                <asp:BoundField DataField="BilAmtRecdFromPC" HeaderText="Bill Amt Recvd From society"
                    SortExpression="BilAmtRecdFromPC" />
                <asp:BoundField DataField="CommodityId" HeaderText="Commodity" SortExpression="CommodityId" Visible="False" />
            </Columns>
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        &nbsp;
         </td>
    
    </tr> 
    <tr>
    <td colspan="4">
        &nbsp;</td>
        <td colspan="1">
        </td>
    
    
    </tr>
            
      
            </table>
</asp:Content>

