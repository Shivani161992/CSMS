<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="RackPointEntry_frmProcurement.aspx.cs" Inherits="District_RackPointEntry_frmProcurement" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript">
function CheckIsnonDecimal(tx)
{
var AsciiCode = event.keyCode;
var txt=tx.value;
var txt2 = String.fromCharCode(AsciiCode);
var txt3=txt2*1;
if ((AsciiCode < 47) || (AsciiCode > 57))
{
alert('Please enter only numbers.');
event.cancelBubble = true;
event.returnValue = false;
}
}
   </script>
   
    <script type="text/javascript">
function CheckCalDate(tx)
{
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

<script type="text/javascript">
function CheckIsNumeric(tx)
{
var AsciiCode = event.keyCode;
var txt=tx.value;
var txt2 = String.fromCharCode(AsciiCode);
var txt3=txt2*1;
if ((AsciiCode < 46) || (AsciiCode > 57))
{
alert('Please enter only numbers.');
event.cancelBubble = true;
event.returnValue = false;
}

var num=tx.value;
var len=num.length;
var indx=-1;
indx=num.indexOf('.');
if (indx != -1)
{
var coda = event.keyCode;
 if(coda == 46)
 {
    alert('Decimal cannot come twice');
    event.cancelBubble = true;
    event.returnValue = false;
 }
var dgt=num.substr(indx,len);
var count= dgt.length;
//alert (count);
if (count > 5)  
{
 alert("Only 5 decimal digits allowed");
 event.cancelBubble = true;
 event.returnValue = false;
}
}

}



    </script>
 
 
 <script type="text/javascript">
function CheckIsNumeric(tx)
{
var AsciiCode = event.keyCode;
var txt=tx.value;
var txt2 = String.fromCharCode(AsciiCode);
var txt3=txt2*1;
if ((AsciiCode < 46) || (AsciiCode > 57))
{
alert('Please enter only numbers.');
event.cancelBubble = true;
event.returnValue = false;
}

var num=tx.value;
var len=num.length;
var indx=-1;
indx=num.indexOf('.');
if (indx != -1)
{
var coda = event.keyCode
 if(coda == 46)
 {
    alert('Decimal cannot come twice');
    event.cancelBubble = true;
    event.returnValue = false;
 }
var dgt=num.substr(indx,len);
var count= dgt.length;
//alert (count);
if (count > 5)  
{
 alert("Only 5 decimal digits allowed");
 event.cancelBubble = true;
 event.returnValue = false;
}
}

}
function IsNumericProcQty(key,txt)
{
  var keycode = (key.which) ? key.which : key.keyCode;
    var num=txt.value;
    var len=num.length;
    var indx=-1;
    indx=num.indexOf('.');
    
            var dgt=num.substr(indx,len);
            var count= dgt.length;
      
    if(keycode==08)
    {
        return true;
    }
     else if (keycode == 59 || keycode  == 32)
     {            
            alert('Semi Colon (;) & Blank Space Not Allowed ...');
            return false;
     } 
     else if (keycode =="36" || keycode =="37" || keycode =="38" || keycode =="40" || keycode =="41" || keycode =="43" || keycode =="92" || keycode =="124" || keycode =="34" || keycode =="39" || keycode =="60" || keycode =="62" || keycode =="44" || keycode =="64" || keycode =="61" || keycode=="63")
     {
            alert('Do not use SQL Key-Words, Semi Colon(;) and Special Characters(&,%,$)..etc');
	        return false;
     }
    
    else if (keycode==09)
    {
             if(num>4999)
            {
                 alert(' मात्रा 5000 से अधिक प्रविष्ट नहीं कर सकते हें|');
                 txt.value="0";
                 return false;
            }
    } 
    
    else if (num>4999)
    {
            alert('मात्रा 5000 से अधिक प्रविष्ट नहीं कर सकते हें|');
            txt.value="0";
            return false;
    }      
    else  if (count > 5)  
    {
                alert("दशमलव के बाद 5 अंक ही आ सकते है");
                return false;
    }
    
    else if(keycode==46)
    {
         if (num.split(".").length>1)
         {    
            alert('दशमलव एक ही बार आ सकता है |');
            return false;
         }
    }
    else if (keycode >= 48 && keycode <= 58 )
    {
        return true;
    }  
    else 
    {
        alert('कृपया संख्या ही प्रविष्ट करें |');
        return false;
    }}


function IsNumericProcQtyBag(key,txt)
{
  var keycode = (key.which) ? key.which : key.keyCode;
    var num=txt.value;
    var len=num.length;
    var indx=-1;
    indx=num.indexOf('.');
    
            var dgt=num.substr(indx,len);
            var count= dgt.length;
      
    if(keycode==08)
    {
        return true;
    }
    
     else if (keycode == 59 || keycode  == 32 )
     {            
            alert('Semi Colon (;) & Blank Space Not Allowed ...');
            return false;
     } 
     else if (keycode =="36" || keycode =="37" || keycode =="38" || keycode =="40" || keycode =="41" || keycode =="43" || keycode =="92" || keycode =="124" || keycode =="34" || keycode =="39" || keycode =="60" || keycode =="62" || keycode =="44" || keycode =="64" || keycode =="61" || keycode=="63")
     {
            alert('Do not use SQL Key-Words, Semi Colon(;) and Special Characters(&,%,$)..etc');
	        return false;
     }
    
    else if (keycode==09)
    {
             if(num>10000)
            {
                 alert(' मात्रा 10000 से अधिक प्रविष्ट नहीं कर सकते हें|');
                 txt.value="0";
                 return false;
            }
    } 
    
    else if (num>10000)
    {
            alert('मात्रा 10000 से अधिक प्रविष्ट नहीं कर सकते हें|');
            txt.value="0";
            return false;
    }      
    else if(keycode==46)
    {
         if (num.split(".").length>0)
         {    
            alert('दशमलव नहीं आ सकता है |');
            return false;
         }
    }
    
    else if (keycode >= 48 && keycode <= 58 )
    {
        return true;
    }  
    else 
    {
        alert('कृपया संख्या ही प्रविष्ट करें |');
        return false;
    }}






function chkSqlKey(key,txt)
{
 
    var keycode = (key.which) ? key.which : key.keyCode;
    
    if (keycode == 59  || keycode == 32 )
     {            
            alert('Semi Colon (;) & Blank Space Not Allowed ...');
            return false;
     } 
   
     else if (keycode =="36" || keycode =="37" || keycode =="38" || keycode =="40" || keycode =="41" || keycode =="43" || keycode =="92" || keycode =="124" || keycode =="34" || keycode =="39" || keycode =="60" || keycode =="62" || keycode =="44" || keycode =="64" || keycode =="61" || keycode=="63")
     {
            alert('Do not use SQL Key-Words, Semi Colon(;) and Special Characters(&,%,$)..etc');
	        return false;
     }
}
function ChkNotgrate()
{
var ctrlisqty = document.getElementById('ctl00_ContentPlaceHolder1_txtissueqty');
var ctrlrvqty = document.getElementById('ctl00_ContentPlaceHolder1_txtrecdqty');
var ctxisqty = ctrlisqty.value 
var ctxtrcvqty = ctrlrvqty.value 

if(ctxtrcvqty == txisqty * 100  )
{
alert("NOT grdn 10 times");
return false
}
return true
}
    </script>   
    
    <div id="hedl"><asp:Label ID="lber" runat="server" Text=""  Visible="false" ForeColor="red" Font-Size="9px"></asp:Label> </div>
    

    <table style="width: 840px; height: 378px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;">
        <tr>
            <td colspan="4" style="border-right: black thin groove; border-bottom: black thin groove;
                height: 1px">
                <span style="color: #3300ff"><strong><span style="color: #ff0066">NOTE:</span> जो ट्रक
                    वास्तविक रूप से प्राप्त हुआ है उसे ध्यान से चुन कर ही एंट्री करें , वर्तमान में
                    एंट्री डिलीट नहीं हो सकेगी</strong></span></td>
        </tr>
        <tr>
            <td colspan="4" style="border-right: black thin groove; border-bottom: black thin groove;
                height: 1px">
                <span style="color: #cc0033">
                रैक से भेजने हेतु , खरीदी केन्द्र द्वारा भेजे गए खाद्यान्न की प्राप्ति हेतु प्रविष्टि
                करें</span></td>
        </tr>
        <tr>
            <td style="width: 211px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <span style="color: #990000"><span style="color: #000000"></span>
                    <asp:Label ID="lblCommodity" runat="server" Font-Size="12px" Text="Commodity" Font-Bold="True" ForeColor="#3300FF" Width="73px"></asp:Label></span></td>
            <td colspan="3" style="height: 1px; width: 128px; border-bottom: black thin groove; border-right: black thin groove; text-align: left;">
                <asp:DropDownList ID="ddlcomdty" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlcomdty_SelectedIndexChanged"
                    Width="140px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 211px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <span style="color: #6600ff">Sending District</span></td>
            <td style="width: 159px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <asp:DropDownList ID="ddldistrict" runat="server" Width="156px" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList></td>
            <td style="width: 146px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <span style="color: #3300ff">Select Purchase Center</span></td>
            <td style="width: 144px; height: 1px; border-bottom: black thin groove;">
            <asp:DropDownList ID="ddluparjan" runat="server" Width="247px" OnSelectedIndexChanged="ddlIssuecenter_SelectedIndexChanged" AutoPostBack="True">
            </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="height: 1px; border-bottom: black thin groove; border-right: black thin groove;" colspan="4">
               <asp:Panel ID="pnlgrd" runat="server" ScrollBars="Vertical" Height="160px" Visible="false">
                
            <asp:GridView ID="dgridchallan" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="dgridchallan_SelectedIndexChanged" OnRowDataBound="dgridchallan_RowDataBound" 
            PageSize="1" PagerSettings-Visible ="true" ShowFooter = "True" CellPadding="1" ForeColor="#333333" GridLines="None" 
            OnPageIndexChanging="dgridchallan_PageIndexChanging" OnPageIndexChanged="dgridchallan_PageIndexChanged"  >
        <HeaderStyle   BackColor="#5D7B9D" ForeColor="White" />
        <Columns>
           <asp:CommandField HeaderText="Action" ShowSelectButton="True" SelectText="Click to Receive" >
                <ItemStyle  ForeColor="Blue" />
            </asp:CommandField>
                  <asp:BoundField DataField="DateOfIssue" HeaderText="Date Of Dispatch" SortExpression="DateOfIssue">
            </asp:BoundField>   
             <asp:BoundField DataField="IssueID" HeaderText="IssueID" SortExpression="IssueID">
                <ItemStyle   HorizontalAlign="Left" ForeColor="Maroon" />
                 <HeaderStyle ForeColor="White" />
            </asp:BoundField>          
            <asp:BoundField DataField="TruckChalanNo" HeaderText="TC No." ReadOnly="True" SortExpression="TruckChalanNo" >
                <ItemStyle  HorizontalAlign="Right"/>
                <HeaderStyle  HorizontalAlign="Right"/>
            </asp:BoundField>
            <asp:BoundField DataField="TruckNo" HeaderText="Truck No." ReadOnly="True"
                SortExpression="TruckNo" >
                <ItemStyle  HorizontalAlign="Right"/>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:BoundField>
            
           
            <asp:BoundField DataField="Bags" HeaderText="Bags" ReadOnly="True" SortExpression="Bags" >
                <ItemStyle  HorizontalAlign="Right"/>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="QtyTransffer" HeaderText="Qty" ReadOnly="True"
                SortExpression="QtyTransffer" >
                <ItemStyle HorizontalAlign="Right" />
                <HeaderStyle  HorizontalAlign="Right"/>
            </asp:BoundField>
            <asp:BoundField DataField="Transporter_Name" HeaderText="Transport Name" ReadOnly="True" SortExpression="Transporter_Name" >
                <ItemStyle HorizontalAlign="Right"/>
                <HeaderStyle HorizontalAlign="Right" />
            </asp:BoundField>
             <asp:BoundField DataField="TransporterId" HeaderText="TransporterId" ReadOnly="True" SortExpression="TransporterId">
                <ItemStyle HorizontalAlign="Right" Width="0px" Font-Size="0px"/>
                <HeaderStyle HorizontalAlign="Right" Width="0px" Font-Size="0px" />
            </asp:BoundField>                     
           
        </Columns>
             <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
             <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" />
             <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
             <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <EditRowStyle BackColor="#999999" />
    </asp:GridView>
    </asp:Panel>
               </td>
        </tr>
        <tr>
            <td style="width: 211px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <span style="color: #990000">Dispatch Date</span></td>
             <td style="width: 159px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <asp:Label ID="lblchallanDate" runat="server" Width="153px" Font-Bold="True" ForeColor="#0000C0"></asp:Label></td>
           <td style="width: 146px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <span style="color: #990000">Crop Year</span></td>
            <td style="width: 144px; height: 1px; border-bottom: black thin groove; text-align: left;">
                <asp:DropDownList ID="ddlcropyear" runat="server" Width="140px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="border-right: black thin groove; width: 211px; border-bottom: black thin groove;
                height: 1px">
                <span style="color: #cc0033">
                Issue Id</span></td>
            <td style="border-right: black thin groove; width: 159px; border-bottom: black thin groove;
                height: 1px">
                <asp:TextBox ID="txtissueId" runat="server" Enabled="False" Font-Size="12px"
                    Width="146px"></asp:TextBox></td>
            <td style="border-right: black thin groove; width: 146px; border-bottom: black thin groove;
                height: 1px">
                <span style="color: #cc0033">
                TC Number</span></td>
            <td style="width: 144px; border-bottom: black thin groove; height: 1px; text-align: left;">
                <asp:TextBox ID="txtchlnno" runat="server" Enabled="False"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="border-right: black thin groove; width: 211px; border-bottom: black thin groove;
                height: 1px">
                Truck Number</td>
            <td style="border-right: black thin groove; width: 159px; border-bottom: black thin groove;
                height: 1px">
                <asp:Label ID="txttrucknopady" runat="server" Font-Bold="True" ForeColor="#0000C0"
                    Width="153px"></asp:Label></td>
            <td style="border-right: black thin groove; width: 146px; border-bottom: black thin groove;
                height: 1px">
                <asp:TextBox ID="txtbookno" runat="server" Visible="False" Width="146px"></asp:TextBox></td>
            <td style="width: 144px; border-bottom: black thin groove; height: 1px; text-align: left">
                <asp:TextBox ID="txtaccptno" runat="server" MaxLength="13" Visible="False" Width="146px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 211px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <span style="color: #990000">Send Bags in Number</span></td>
            <td style="width: 159px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <asp:TextBox ID="txtissubag" runat="server" Enabled="False" Font-Bold="True" ForeColor="#0000C0"></asp:TextBox></td>
             <td style="width: 146px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <span style="color: #990000">Send Qty in Qts</span></td>
             <td style="width: 144px; height: 1px; border-bottom: black thin groove; text-align: left;">
                <asp:TextBox ID="txtissueqty" runat="server" Enabled="False" Font-Bold="True" ForeColor="#0000C0"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="border-right: black thin groove; width: 211px; border-bottom: black thin groove;
                height: 1px">
                <asp:Label ID="Label3" runat="server" BackColor="White" Font-Size="12px" Text="Receving TC Number"
                    Width="131px"></asp:Label></td>
            <td style="border-right: black thin groove; width: 159px; border-bottom: black thin groove;
                height: 1px">
                <asp:TextBox ID="txtrec_tcnumber" runat="server" Height="14px" MaxLength="13" Width="135px"></asp:TextBox></td>
            <td style="border-right: black thin groove; width: 146px; border-bottom: black thin groove;
                height: 1px">
                <asp:Label ID="Label4" runat="server" BackColor="White" Font-Bold="False" Font-Size="12px"
                    Text="Receving Truck Number" Width="141px"></asp:Label></td>
            <td style="width: 144px; border-bottom: black thin groove; height: 1px; text-align: left">
                <asp:TextBox ID="txtRec_TruckNumber" runat="server" Height="15px" MaxLength="13"
                    Width="147px"></asp:TextBox></td>
        </tr>
        <tr>
             <td style="width: 211px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <span style="color: #990000">Recd Bags in Number</span></td>
             <td style="width: 159px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <asp:TextBox ID="txtrecdbags" runat="server"></asp:TextBox></td>
            <td style="width: 146px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <span style="color: #990000">Recd Qty in Qts</span></td>
            <td style="width: 144px; height: 1px; border-bottom: black thin groove; text-align: left;">
                <asp:TextBox ID="txtrecdqty" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 211px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <span style="color: #990000">Received Date</span></td>
            <td style="width: 159px; height: 1px; border-bottom: black thin groove; border-right: black thin groove; text-align: left;">
            
             <asp:TextBox ID="DaintyDate1" runat="server" Width="105px"></asp:TextBox>
             <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_DaintyDate1'
	    });
	     </script>
            
            </td>
             <td style="width: 146px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <span style="color: #990000">Select Rake Number</span></td>
           <td style="width: 144px; height: 1px; border-bottom: black thin groove; text-align: left;">
                <asp:DropDownList ID="ddlracknumber" runat="server" Width="245px" >
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="height: 1px; border-bottom: black thin groove; background-color: #ffff99; text-align: left;" colspan="4">
                <asp:Label ID="Label9" runat="server" Visible="False"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 211px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <asp:Label ID="lbltranporterID" runat="server" Visible="False"></asp:Label></td>
            <td style="width: 159px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="54px" OnClick="btnSave_Click" /></td>
             <td style="width: 146px; height: 1px; border-bottom: black thin groove; border-right: black thin groove;">
                <asp:Button ID="btnnew" runat="server" Text="New" Width="67px" OnClick="btnnew_Click" /></td>
            <td style="width: 144px; height: 1px; border-bottom: black thin groove; text-align: left;">
                <asp:Button ID="btnclose" runat="server" Text="Close" Width="65px" OnClick="btnclose_Click" /></td>
        </tr>
        <tr>
            <td style="border-right: black thin groove; width: 211px; border-bottom: black thin groove;
                height: 1px">
            </td>
            <td style="border-right: black thin groove; width: 159px; border-bottom: black thin groove;
                height: 1px">
            </td>
            <td style="border-right: black thin groove; width: 146px; border-bottom: black thin groove;
                height: 1px">
            </td>
            <td style="width: 144px; border-bottom: black thin groove; height: 1px; text-align: left">
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="#" Visible="False" >Print Receipt Detail</asp:HyperLink></td>
        </tr>
    </table>



</asp:Content>

