<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CommodityRateMaster.aspx.cs" Inherits="mpproc_Admin_CommodityRateMaster"  MasterPageFile="~/mpproc/MasterPage/AdminMasterPage.master"%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript" language="javascript">
function CheckIsNumericInt(e,tx)
{

          var AsciiCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode; 
                 
          if ((AsciiCode < 46 && AsciiCode != 8) || (AsciiCode > 57 ) || (AsciiCode == 47 ) || (AsciiCode == 46 )  )
        {
            alert('Please enter only numbers.');
            return false;
        }   
 }  

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
    
    function CheckIsCapacityt()
    {
   
     var txtCFcap = document.getElementById('ctl00_ContentPlaceHolder1_txt_GFCap');
     if(txtCFcap.value!='')
     {
     if( (txtCFcap.value > 100) )
     {
      alert(" Gunny Filling Capacity Not More then 100 kg");
      event.returnValue = false;
     
     }
     
    if (txtCFcap.value < 50)
    {
      alert(" Gunny Filling Capacity Not Less then 50 kg");
     event.returnValue = false;
    }
    }
    }

    </script>
<table border="0" cellpadding ="2" cellspacing ="2" width="700px"  style="font-size:12px">
<tr>
<td colspan="4"style="text-align: center; background-color:#cccc66; height:30px; ">

    <asp:Label ID="lbl_MasDetailTitle" runat="server" Text="MSP Rate Master" Font-Bold="True" Font-Size="Small"></asp:Label>

</td>
</tr>
<tr>
<td colspan="4" style="height:30px">
<div id="DivMsg" runat="server"  style="color: #990033;"></div>
</td>

</tr>
    <tr>
        <td colspan="4" align="right" >
            <span id="lblRatePQtl" style="font-weight: bold; font-size: 8pt; color: darkblue;
                font-family: Verdana; position: static">(Rate per Quintal)</span></td>
    </tr>
<tr>
<td style="background-color:lightgrey" align="left">
    <asp:Label ID="lbl_MarSeas" runat="server" Text="Marketing Season  "></asp:Label>


</td>
<td align="left">
    &nbsp;<asp:DropDownList ID="DDL_MarSeas" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDL_MarSeas_SelectedIndexChanged" Enabled="False">
    </asp:DropDownList></td>
<td style="background-color: lightgrey" align="left">
    <asp:Label ID="lbl_CropYear" runat="server" Text="Crop Year:"></asp:Label>

</td>
<td align="left">
    <asp:DropDownList ID="DDL_CropYear" runat="server" Enabled="False">
    </asp:DropDownList>

</td>

</tr>
<tr>
<td style="background-color: lightgrey" align="left">
    <asp:Label ID="lbl_Commodity" runat="server" Text="Commodity: "></asp:Label>


</td>
<td align="left">

    <asp:DropDownList ID="DDL_Commodity" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDL_Commodity_SelectedIndexChanged">
    </asp:DropDownList>

</td>
<td style="background-color: lightgrey" align="left">
    <asp:Label ID="lbl_MSPRate" runat="server" Text="MSP Rate (in Rs):"></asp:Label>

</td>
<td align="left">
    <asp:TextBox ID="txt_MSPRate" runat="server"></asp:TextBox>
    <span id="lblrateperQuintal" style="font-weight: bold">Per Qtls.</span></td>

</tr>
<tr>
<td style="background-color: lightgrey" align="left">


    <asp:Label ID="lbl_Commission" runat="server" Text="Commission:"></asp:Label>


</td>
<td align="left">

    <asp:TextBox ID="txt_commission" runat="server"></asp:TextBox>

</td>
<td style="background-color: lightgrey" align="left">
    <asp:Label ID="lbl_Incidental" runat="server" Text="Incidental:"></asp:Label>

</td>
<td align="left">

    <asp:TextBox ID="txt_Incidental" runat="server"></asp:TextBox>
</td>
</tr>
<tr>
<td style="background-color: lightgrey" align="left">
    <asp:Label ID="lbl_Bonus" runat="server" Text="Bonus:"></asp:Label>


</td>
<td align="left">

    <asp:TextBox ID="txt_Bonus" runat="server"></asp:TextBox>

</td>
<td style="background-color: lightgrey" align="left">
    <asp:Label ID="lbl_GunnyFillingCapacity"  Text="GunnyFillingCapacity:(In Kg)" runat="server"></asp:Label>

</td>
<td align="left">

    <asp:TextBox ID="txt_GFCap" runat="server"></asp:TextBox>

</td>


</tr>

<tr>

</tr>

<tr>

</tr>
<tr>
    <td colspan="4" style="background-color: #cccc66;  height: 5px;">
            
          
           
            </td>
    </tr>

<td colspan="4" align="center">

    <asp:Button ID="btn_Save" runat="server" Text="Save" OnClick="btn_Save_Click" />
    <asp:Button ID="btn_update" runat="server" Text="Update" Visible="False" OnClick="btn_update_Click" />

</td>

<tr>
<td colspan="4" style="height:30px">



</td>

</tr>
<tr>
<td colspan="4" style="height: 19px">
    <asp:GridView ID="GridView_MSPRate" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True"
        CellPadding="4" OnSelectedIndexChanged="GridView_MSPRate_SelectedIndexChanged" Font-Names="Calibri" ForeColor="#333333" GridLines="None">
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <Columns>
            <asp:BoundField DataField="CN" HeaderText="Commodity Name" />
            <asp:BoundField DataField="MS" HeaderText="Marketing Season" />
            <asp:BoundField DataField="CY" HeaderText="Crop Year" />
            <asp:BoundField DataField="Rate" HeaderText="MSP Rate" />
            <asp:BoundField DataField="Commission" HeaderText="Commission" />
            <asp:BoundField DataField="incidental" HeaderText="Incidental" />
            <asp:BoundField DataField="bonus" HeaderText="Bonus" />
            <asp:BoundField DataField="GP" HeaderText="Gunny Capacity" />
            <asp:BoundField DataField="MarkID" HeaderText="MakSeasID" ShowHeader="False" >
                <HeaderStyle Font-Size="0px" ForeColor="DarkRed" Width="0px" />
                <ItemStyle Font-Size="0px" Width="0px" />
            </asp:BoundField>
            <asp:BoundField DataField="ComID" HeaderText="CommodityId" >
                <HeaderStyle Font-Size="0px" ForeColor="DarkRed" Width="0px" />
                <ItemStyle Font-Size="0px" Width="0px" />
            </asp:BoundField>
            <asp:BoundField DataField="CommodityRate_ID" HeaderText="CommodityRate_ID" >
                <HeaderStyle Font-Size="0px" ForeColor="Maroon" Width="0px" />
                <ItemStyle Font-Size="0px" Width="0px" />
            </asp:BoundField>
        </Columns>
        <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    


</td>

</tr>
<tr>
<td  colspan="4" style="height:30px">




</td>
</tr>
<tr>
<td colspan="4" style="height: 19px">



</td>
</tr>

</table>
  
</asp:Content>
