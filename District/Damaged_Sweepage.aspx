<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Damaged_Sweepage.aspx.cs" Inherits="District_Damaged_Sweepage" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <table style="width: 694px; height: 453px; border-right: #000000 thin groove; border-top: #000000 thin groove; border-left: #000000 thin groove; border-bottom: #000000 thin groove;">
        <tr>
          <center>
            <td colspan="2" style="border-top: #000099 thin groove">
                <span style="color: #9900ff; font-family: Verdana"><strong>Damage &amp; Sweepage Entry Form</strong></span></td></center> 
        </tr>
    <center>
    
        <tr>
            <td style="border-top: #000099 thin groove; height: 1px; background-color: #ffff99;" colspan="2" >
            </td>
        </tr>
        <tr>
            <td style="border-top: #000099 thin groove; height: 22px; border-right: #000000 thin groove; width: 374px; background-color: lavender;" >
            <center> 
             <asp:Label ID="lbldate" runat="server" Text="Select Date of Entry" Font-Bold="True" ForeColor="#0000CC" Width="150px"></asp:Label>&nbsp;</center>
            </td>
            <td style="border-top: #000099 thin groove; border-right: #000000 thin groove; width: 265px; height: 22px; background-color: lavender; text-align: left;" >
            
             <asp:TextBox ID="effective_from" runat="server" Width="193px" ></asp:TextBox>
       <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_effective_from'
	    });
	     </script>
            
            </td>
        </tr>
        
        <tr>
            <td style="border-top: #000099 thin groove; background-color: lavender; width: 374px; height: 20px;" >
             <center>  
              <asp:Label ID="lblarrival" runat="server" Text="Source of Arrival" Width="140px" Font-Bold="True" ForeColor="Blue"></asp:Label>&nbsp;</center>
             </td>
            <td style="border-top: #000099 thin groove; background-color: lavender; width: 265px; text-align: left; height: 20px;" >
                <asp:DropDownList ID="ddlsarrival" runat="server" Width="193px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="border-top: #000099 thin groove; height: 20px; border-right: #000000 thin groove; width: 374px; background-color: lavender;" >
             <center>   
             <asp:Label ID="lblcommodity" runat="server" Text="Commodity" Font-Bold="True" ForeColor="#3300FF" Width="140px"></asp:Label>&nbsp;</center>
             </td>
            <td style="border-top: #000099 thin groove; border-right: #000000 thin groove; width: 265px; height: 20px; background-color: lavender; text-align: left;" >
                <asp:DropDownList ID="ddlcomdty" runat="server" Width="193px">
                    <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="border-top: #000099 thin groove; height: 19px; border-right: #000000 thin groove; width: 374px; background-color: lavender;" >
             <center> 
               <asp:Label ID="lblscheme" runat="server" Text="Scheme" Font-Bold="True" ForeColor="#3300FF" Width="140px"></asp:Label>&nbsp;</center>
             </td>
            <td style="border-top: #000099 thin groove; border-right: #000000 thin groove; width: 265px; height: 19px; background-color: lavender; text-align: left;" >
                <asp:DropDownList ID="ddlscheme" runat="server" Width="193px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="border-top: #000099 thin groove; border-right: #000000 thin groove; width: 374px; background-color: lavender; height: 22px;" >
             <center>  
              <asp:Label ID="lblIC" runat="server" Text="Issue Center" Font-Bold="True" ForeColor="#3300FF" Width="140px"></asp:Label>&nbsp;</center>
              </td>
            <td style="border-top: #000099 thin groove; border-right: #000000 thin groove; width: 265px; background-color: lavender; text-align: left; height: 22px;" >
                <asp:DropDownList ID="ddlissuecenter" runat="server" Width="193px" 
                    OnSelectedIndexChanged="ddlissuecenter_SelectedIndexChanged" 
                    AutoPostBack="True">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="border-top: #000099 thin groove; background-color: lavender; width: 374px; height: 21px;" >
              <center> 
               <asp:Label ID="lblgodown" runat="server" Text="Godown" Font-Bold="True" ForeColor="#3300FF" Width="140px"></asp:Label>&nbsp;</center>
               </td>
            <td style="border-top: #000099 thin groove; background-color: lavender; width: 265px; text-align: left; height: 21px;" >
                <asp:DropDownList ID="ddlgodown" runat="server" Width="193px" OnSelectedIndexChanged="ddlgodown_SelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem Selected="True" Value="01">--Select--</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="border-top: #000099 thin groove; height: 18px; background-color: lavender; width: 374px;" >
           <center>    
            <asp:Label ID="lblcropyear" runat="server" Text="Crop Year" Font-Bold="True" ForeColor="#3300FF" Width="140px"></asp:Label>&nbsp;</center>
            </td>
            <td style="border-top: #000099 thin groove; height: 18px; background-color: lavender; width: 265px; text-align: left;" >
                <asp:DropDownList ID="ddlcropyear" runat="server" Visible="true" Width="193px" 
                    OnSelectedIndexChanged="ddlcropyear_SelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem Selected="True" Value="00">--Select--</asp:ListItem>
                    <asp:ListItem Value="01">2014-2015</asp:ListItem>
                    <asp:ListItem Value="02">2013-2014</asp:ListItem>
                    <asp:ListItem Value="03">2012-2013</asp:ListItem>
                    <asp:ListItem Value="04">2011-2012</asp:ListItem>
                    <asp:ListItem Value="05">2010-2011</asp:ListItem>
                    <asp:ListItem Value="06">2009-2010</asp:ListItem>
                    <asp:ListItem Value="07">2008-2009</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        
        <tr>
            <td style="border-top: #000099 thin groove; height: 24px; border-right: #000000 thin groove; width: 374px; background-color: floralwhite;" >
            <center>   
             <asp:Label ID="lblqty" runat="server" Text="Available Quantity of Commodity" Font-Bold="True" ForeColor="#3300FF" Width="257px"></asp:Label>&nbsp;</center>
            </td>
            <td style="border-top: #000099 thin groove; border-right: #000000 thin groove; width: 265px; height: 24px; background-color: floralwhite; text-align: left;" >
                <asp:TextBox ID="txtqty" runat="server" MaxLength="13" Width="193px" 
                    Enabled="False"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="border-top: #000099 thin groove; height: 16px; border-right: #000000 thin groove; width: 374px; background-color: oldlace;" >
              <center>  
              <asp:Label ID="lbldamagebags" runat="server" Text="Damage Bags" Font-Bold="True" ForeColor="#3300FF" Width="140px"></asp:Label>&nbsp;</center>
              </td>
            <td style="border-top: #000099 thin groove; border-right: #000000 thin groove; width: 265px; height: 16px; background-color: oldlace; text-align: left;" >
                <asp:TextBox ID="txtdamagebag" runat="server" Width="193px" MaxLength="5"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="border-top: #000099 thin groove; background-color: oldlace; width: 374px; height: 18px;" >
              <center>  
              <asp:Label ID="lbldamqty" runat="server" Text="Damaged Quantity" Font-Bold="True" ForeColor="#3300FF" Width="140px"></asp:Label>&nbsp;</center>
              </td>
            <td style="border-top: #000099 thin groove; background-color: oldlace; width: 265px; text-align: left; height: 18px;" >
                <asp:TextBox ID="txtdamquantity" runat="server" Width="193px" MaxLength="5" 
                   
                   ></asp:TextBox></td>
        </tr>
        <tr>
            <td style="border-top: #000099 thin groove; background-color: oldlace; width: 374px; height: 17px;" >
             <center>  
              <asp:Label ID="lblsweepqty" runat="server" Text="Sweepage Quantity" Font-Bold="True" ForeColor="#3300FF" Width="140px"></asp:Label>&nbsp;</center>
              </td>
            <td style="border-top: #000099 thin groove; background-color: oldlace; width: 265px; text-align: left; height: 17px;" >
                <asp:TextBox ID="txtsweepqty" runat="server" Width="193px" Rows="5"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="border-top: #000099 thin groove; background-color: oldlace; width: 374px; height: 17px; color: #0033CC;" >
                
                <center>
                <asp:Label ID="lblsweepqty0" runat="server" 
                    Text="Damaged Category" Font-Bold="True" ForeColor="#3300FF" Width="140px"></asp:Label></center></td>
            <td style="border-top: #000099 thin groove; background-color: oldlace; width: 265px; text-align: left; height: 17px;" >
                <asp:DropDownList ID="ddlcataegory" runat="server" Width="193px" 
                  >
                    <asp:ListItem Selected="True" Value="01">--Select--</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="border-top: #000099 thin groove; background-color: #ccff99;" colspan="2">
            </td>
        </tr>
        <tr>
           <td style="border-top: #000099 thin groove; height: 2px; border-right: #000000 thin groove; width: 374px; text-align: center;" >
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="105px" style="font-family: Verdana; background-color: #ccccff" OnClick="btnSave_Click" /></td>
            <td style="border-top: #000099 thin groove; height: 2px; width: 265px; text-align: center;" >
                <asp:Button ID="btnClose" runat="server" Text="Close" Width="111px" style="font-family: Verdana; background-color: #ccccff" OnClick="btnClose_Click" /></td>
        </tr>
        <tr>
            <td colspan="2" style="border-top: #000099 thin groove; text-align: center;">
                <asp:Button ID="btnNew" runat="server" Text="New" Width="114px" 
                    style="font-family: Verdana; background-color: #ccccff" OnClick="btnNew_Click" 
                    Height="26px" /></td>
        </tr>
        <tr>
            <td colspan="2" style="border-top: #000099 thin groove; text-align: left;">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Red" Width="113px">गोदाम का नाम</asp:Label>
                &nbsp; &nbsp;<asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                &nbsp;
                <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="Red" Width="222px">की जानकारी नीचे संशोधित करें</asp:Label></td>
        </tr>
    </table>
    
    </center>

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



    </script>
 
 <script type="text/javascript">
function CheckIsnondecimal(tx)
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

<%-- <style type="text/css">
.Gridview
{
font-family:Verdana;
font-size:10pt;
font-weight:normal;
color:black;

}
</style>--%>
<script type="text/javascript">
function ConfirmationBox(DepotID) {

var result = confirm('क्या आप गोदाम '+DepotID+' की सम्बंधित जानकारी निरस्त करना चाहते है?' );
if (result)
 {

return true;
}
else {
return false;
}
}
</script>

 <table >
        <tr>
            <td >
            <asp:GridView ID="gvDetails" DataKeyNames="TransId,Depot_ID" runat="server" 
        AutoGenerateColumns="False" HeaderStyle-BackColor="#61A6F8" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White" 
        onrowcancelingedit="gvDetails_RowCancelingEdit" 
        onrowdeleting="gvDetails_RowDeleting" onrowediting="gvDetails_RowEditing" 
        onrowupdating="gvDetails_RowUpdating" OnRowDataBound = "gvDetails_RowDataBound" Width="688px">
     
<Columns>
<asp:TemplateField>
<EditItemTemplate>
    <asp:LinkButton ID="LinkButton1" CommandName ="Update"  runat="server" ToolTip="Update" Height="20px"  >Update</asp:LinkButton>

<asp:LinkButton ID="LinkButton2" CommandName ="Cancel"  runat="server" ToolTip="Cancel" Height="20px"  >Cancel</asp:LinkButton>


</EditItemTemplate>
<ItemTemplate>

<asp:LinkButton ID="LinkButton2" CommandName ="Edit"  runat="server" ToolTip="Cancel" Height="20px"  >Edit</asp:LinkButton>

<asp:LinkButton ID="LinkButton3" CommandName ="Delete"  runat="server" ToolTip="Cancel" Height="20px"  >Delete</asp:LinkButton>


</ItemTemplate>
 </asp:TemplateField>
 
 <asp:TemplateField HeaderText="Commodity">
<EditItemTemplate>
<asp:Label ID="lbleditcom" runat="server" Text='<%#Eval("Commodity_Name") %>'/>
</EditItemTemplate>
<ItemTemplate>
<asp:Label ID="lblitemcom" runat="server" Text='<%#Eval("Commodity_Name") %>'/>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="Scheme">
<EditItemTemplate>
<asp:Label ID="lbleditsch" runat="server" Text='<%#Eval("Scheme_Name") %>'/>
</EditItemTemplate>
<ItemTemplate>
<asp:Label ID="lblitemsch" runat="server" Text='<%#Eval("Scheme_Name") %>'/>
</ItemTemplate>
</asp:TemplateField>
 
 
 <asp:TemplateField HeaderText="Date">
<EditItemTemplate>
<asp:Label ID="lbleditdate" runat="server" Text='<%#Eval("Date") %>'/>
</EditItemTemplate>
<ItemTemplate>
<asp:Label ID="lblitemdate" runat="server" Text='<%#Eval("Date") %>'/>
</ItemTemplate>
</asp:TemplateField>
 
 <asp:TemplateField HeaderText="Damage Bags">
 <EditItemTemplate>
 <asp:TextBox ID="txtdamBag" runat="server" Text='<%#Eval("Damage_Bags") %>' />
 </EditItemTemplate>
 <ItemTemplate>
 <asp:Label ID="lblDamBag" runat="server" Text='<%#Eval("Damage_Bags") %>' />
 </ItemTemplate>
 </asp:TemplateField>
 
 <asp:TemplateField HeaderText="Damage Quantity">
 <EditItemTemplate>
 <asp:TextBox ID="txtdamQty" runat="server" Text='<%#Eval("Damage_Quantity") %>' />
 </EditItemTemplate>
 <ItemTemplate>
 <asp:Label ID="lblDamQty" runat="server" Text='<%#Eval("Damage_Quantity") %>' />
 </ItemTemplate>
 </asp:TemplateField>
 
 
 <asp:TemplateField HeaderText="Sweepage Quantity">
 <EditItemTemplate>
 <asp:TextBox ID="txtqty" runat="server" Text='<%#Eval("SweepageQty") %>' />
 </EditItemTemplate>
 <ItemTemplate>
 <asp:Label ID="lblqty" runat="server" Text='<%#Eval("SweepageQty") %>' />
 </ItemTemplate>
 </asp:TemplateField>
 </Columns>
    <HeaderStyle BackColor="#61A6F8" Font-Bold="True" ForeColor="White" />
</asp:GridView>
            
            </td>
        </tr>
    </table>

</asp:Content>

