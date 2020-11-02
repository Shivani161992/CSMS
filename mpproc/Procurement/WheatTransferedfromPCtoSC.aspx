<%@ Page Language="C#" MasterPageFile="~/mpproc/MasterPage/AgencyMaster.master" AutoEventWireup="true" CodeFile="WheatTransferedfromPCtoSC.aspx.cs" Inherits="mpproc_Procurement_WheatTransferedfromPCtoSC" Title="MP Procurement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <script type="text/javascript" src="../../js/calendar_eu.js"></script>  
 <script type="text/javascript" src="../js/DecimalValid.js"></script>
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
  
<script type="text/javascript" language="javascript">


function chkDate() {

   
    var control3 = document.getElementById('ctl00_ContentPlaceHolder1_DaintyDate1');       
   var t=document.getElementById('ctl00_ContentPlaceHolder1_hydTodayDate');
  
   //var tdate = coverttommddyy(t);
   var trdate=new Date(t.value);
   
   var prdate=coverttommddyy(control3.value);
   var presentdate=new Date(prdate);
  
   if(trdate < presentdate)
   {
    alert('Your Date is invalid Date cannot be greater than todays date ');
    control3.value='';
    
   }   
}
function coverttommddyy(pdate)
{
    var dd=pdate.split('/');
    var date=dd[1]+'/'+ dd[0] + '/' +dd[2];
    return date;
}

function checkQuantity()
{
    
    var cntr_progressive=document.getElementById('ctl00_ContentPlaceHolder1_lblProgressiveProc');
    var cntr_qtyTrans=document.getElementById('ctl00_ContentPlaceHolder1_txtQtyTransfromPC');
    var cntr_qtyLifted=document.getElementById('ctl00_ContentPlaceHolder1_lblQtyLifted');
       
    var val1 = parseFloat(cntr_progressive.value);
    var val2= parseFloat(cntr_qtyTrans.value) + parseFloat(cntr_qtyLifted.value);
    
    if(parseFloat(val1) < parseFloat(val2))
    {
    
        alert('Quantity lifted cannot be greater than left progressive procured Qty!');
        cntr_qtyTrans.value="";
    }
    
}

    </script>
    
    <table width="800" cellpadding="0" cellspacing="0" style="margin-left:0px;border-collapse: collapse; border:solid 1px white; background-color:#cccc66" id="TABLE1" onclick="return TABLE1_onclick()">
              <tr align="center" class="HeadingBlue">
                  <td colspan="4" style="height: 15px; border-collapse: collapse; border:solid 1px white; background-color: dimgray; width: 772px;">
                      <asp:Label ID="lbl_tit_wheattans" runat="server" Font-Bold="True" ForeColor="White" Text="Wheat transfer from PC To SC"></asp:Label></td>
              </tr>
                <tr align="center" > 
                  <td colspan="4" class="boldTxt" style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                      border-bottom: white 1px solid; border-collapse: collapse; height: 15px; width: 772px;">
                      <span style="font-size: 8pt"></span></td>
                </tr>
            
              <tr align="center" >
                  <td  colspan="4" align="right" style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                      border-bottom: white 1px solid; border-collapse: collapse; height: 14px; width: 772px;">
                     
                      <asp:Label id="lbl_Mas_Q_IS" runat="server" Font-Size="Small" Text="(Qty. is in Qtl.Kgs, Amount in Rs.)" Font-Bold="True"></asp:Label>
                     
                     
                     </td>
              </tr>
            <tr>
                <td colspan="4" style="border-right: white 1px solid; border-top: white 1px solid;
                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                    height: 137px; width: 772px;">
                    <asp:Panel ID="pnlFCI_OTDepot" runat="server" Width="100%">
                        <table cellpadding="0" cellspacing="0" style="width:100%; border-collapse: collapse; border:solid 1px white;" >
                            <tr id="trFCIa_dist_depo">
                                <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                    border-bottom: white 1px solid; border-collapse: collapse; height: 21px; width: 295px;" align="left">
                                    <asp:Label ID="lbl_DistRes" runat="server" Font-Size="Small" Font-Bold="True"></asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 295px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:Label ID="lbl_AgencyRes" runat="server" Font-Size="Small" Font-Bold="True" Width="163px"></asp:Label></td>
                                <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                    border-bottom: white 1px solid; border-collapse: collapse; height: 21px;" align="left" >
                                    
                                    <asp:Label ID="lbl_MarSeasRes" runat="server" Font-Size="Small" Font-Bold="True"></asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px" >
                                    <asp:Label id="lbl_CropYearRes" runat="server" Width="122px" Font-Size="Small" Font-Bold="True"></asp:Label>
                                    </td>
                            </tr>
                            <tr>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 295px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:Label ID="lbl_PProcTill" runat="server" Font-Size="Small" Text="Progressive Procured Till Date:"
                                        Width="194px"></asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 295px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">                                    
                                    <asp:TextBox ID="lblProgressiveProc" runat="server" Width="103px" BackColor="#CCCC99" ForeColor="Black" Height="15px" BorderStyle="Groove" BorderWidth="0px" Font-Bold="True" Font-Size="Small" ReadOnly="True"></asp:TextBox></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 181px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:Label ID="lbl_Qlift" runat="server" Font-Size="Small" Text="&#13;&#10;Qty Lifted Till Date:"></asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 272px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    &nbsp;<asp:TextBox ID="lblQtyLifted" runat="server" Width="95px" BackColor="#CCCC99" BorderStyle="Groove" BorderWidth="0px" Font-Bold="True" Font-Size="Small" ReadOnly="True"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 295px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:Label ID="lbl_TransDate" runat="server" Font-Size="Small">Transaction Date:(DD/MM/YYYY):</asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 295px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                       <asp:TextBox ID="DaintyDate1" runat="server" Width="137px"></asp:TextBox>
                                        <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate1'
	    });
	     </script>     
                                       </td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 181px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                                                <asp:Label ID="lbl_pc" runat="server" Font-Size="Small">Purchase Center:</asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 272px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    
                                    <asp:DropDownList ID="DDL_PC" runat="server" Enabled="False" Width="136px">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 295px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                </td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 295px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:Label ID="lbl_commodity" runat="server" Font-Size="Small">Commodity:</asp:Label>
                                    <asp:DropDownList ID="DDL_Commodity" runat="server">
                                    </asp:DropDownList></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 181px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    </td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 272px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                <asp:Button ID="btn_fecth" runat="server" Text="Fetch" Width="75px" OnClick="btn_fecth_Click" /></td>
                            </tr>
                            <tr>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 181px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:Label ID="lbl_Transfer" runat="server" Font-Size="14px" Font-Bold="True" ForeColor="Maroon" Width="133px">Transfer:</asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 295px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                </td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 181px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                </td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 181px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 295px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:Label ID="lbl_ToDist" runat="server" Font-Size="Small">To District:</asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 295px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:DropDownList ID="DDL_Dist" runat="server" OnSelectedIndexChanged="DDL_Dist_SelectedIndexChanged" AutoPostBack="True" Width="130px">
                                    </asp:DropDownList></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 181px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:Label ID="lbl_ToStorage" runat="server" Font-Size="Small" Text="To Storage Center:"></asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 272px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:DropDownList ID="ddl_StorageCenter" runat="server" Width="120px">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:Label ID="lbl_QtTransfred" runat="server" Font-Size="Small" Text="Qty Transfered from Purchase Center:" Width="210px"></asp:Label>
                                    </td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 295px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:TextBox ID="txtQtyTransfromPC" runat="server" Width="107px"></asp:TextBox></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 272px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:Label ID="lbl_QtDep" runat="server" Font-Size="Small" Text="Qty Deposited in Godown:" Width="156px"></asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 328px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:TextBox ID="txtQtyDepositedGodown" runat="server" Width="107px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 295px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 17px">
                                    </td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 295px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 17px">
                                </td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 181px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 17px">
                                    </td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 272px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 17px">
                                    </td>
                            </tr>
                            <tr>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 295px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 11px">
                                    <asp:Label ID="Label8" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 295px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 11px">
                                    <asp:Button ID="btn_Reset" runat="server" Text="Reset" OnClick="btn_Reset_Click" Width="95px" /></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 181px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 11px">
                                    <asp:Button ID="btn_AddNew" runat="server" Text="AddNew" OnClick="btn_AddNew_Click" Width="108px" /></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 272px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 11px">
                                    </td>
                            </tr>                     
                       
                            
                        </table>
                        </asp:Panel>
                    <asp:HiddenField ID="hydTodayDate" runat="server" />
                   
                </td>
            </tr>
    <tr>
        <td colspan="4" style="border-right: white 1px solid; border-top: white 1px solid;
            border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse; width: 772px;" align="center">
            <asp:Label ID="lblNoRecord" runat="server" Font-Bold="True" Font-Names="Calibri" ForeColor="Maroon"
                Text="No Records Found for the given Date" Visible="False"></asp:Label>
            <asp:GridView ID="GridView_Transfered" runat="server" BackColor="#FFFF80" BorderColor="#CC9966"
                BorderStyle="None" BorderWidth="1px" CellPadding="4" AutoGenerateColumns="False" Font-Names="Calibri" Font-Size="9pt">
                <RowStyle BackColor="White" ForeColor="#330099" />
                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                <Columns>
                    <asp:BoundField DataField="TransactionID" HeaderText="TransactionID" />
                    <asp:BoundField DataField="TransferDate" HeaderText="Transfer Date" />
                    <asp:BoundField DataField="PurchaseCenter" HeaderText="From Purchase Center" />
                    <asp:BoundField DataField="Commodity" HeaderText="Commodity" />
                    <asp:BoundField DataField="StorageCenter" HeaderText="From Storage Center" />
                    <asp:BoundField DataField="QtyTransferred" HeaderText="QtyTransferred" />
                    <asp:BoundField DataField="QtyDeposited" HeaderText="Qty Procured" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
         
              <tr class="HeadingBlue">
                <td colspan="4" style="border-collapse: collapse; border:solid 1px white;
                    height: 15px; text-align: center; background-color: dimgray; width: 772px;">
                </td>
            </tr>
        </table>
</asp:Content>

