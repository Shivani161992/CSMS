<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FarmerDetails.aspx.cs" Inherits="Masters_FarmerDetails" MasterPageFile="~/mpproc/MasterPage/AgencyMaster.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title>Wheat Transfer from Purchase Center to Storage Center</title>
<script type="text/javascript" language="javascript">
function CheckIsChar(tx)
{
var AsciiCode = event.keyCode;
var txt=tx.value;
var txt2 = String.fromCharCode(AsciiCode);
var txt3=txt2*1;
if ((AsciiCode < 65 && AsciiCode !=32) || (AsciiCode > 122))
{
alert('Please enter only character');
event.cancelBubble = true;
event.returnValue = false;
}
}
  </script>
 
  <script type="text/javascript" language="javascript">
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

}

</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    &nbsp;<table>
        <tr>
            <td style="width: 100px; height: 326px;">
<table width="570" cellpadding="2" cellspacing="2" style="margin-left:20px;border-collapse: collapse; border:solid 1px white; background-color:#cccc66" id="TABLE1">
              <tr align="center" class="HeadingBlue">
                  <td colspan="4" style="height: 15px; border-collapse: collapse; border:solid 1px white; background-color: dimgray; width: 604px;">
                    </td>
              </tr>
                <tr align="center" > 
                  <td colspan="4" class="boldTxt" style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                      border-bottom: white 1px solid; border-collapse: collapse; width: 604px;">
                      <span style="font-size: 8pt">
                          <asp:Label ID="lbl_title" runat="server" Text="Farmer Details" Font-Size="Small" ForeColor="Maroon"></asp:Label></span></td>
                </tr>
            
              <tr align="center" >
                  <td  colspan="4" align="right" style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                      border-bottom: white 1px solid; border-collapse: collapse; height: 14px; width: 604px;">
             
                     
                     </td>
              </tr>
            <tr>
                <td colspan="4" style="border-right: white 1px solid; border-top: white 1px solid;
                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                    width: 604px; height: 195px;">
                    <asp:Panel ID="pnlFCI_OTDepot" runat="server" Width="100%">
                        <table cellpadding="0" cellspacing="0" style="width:100%; border-collapse: collapse; border:solid 1px white;" >
                            <tr >
                                <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                    border-bottom: white 1px solid; border-collapse: collapse; height: 21px; width: 142px;" align="left">
                                    <asp:Label ID="lbl_FarName" runat="server" Font-Size="Small">Name Of Farmer:</asp:Label></td>
                                <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                    border-bottom: white 1px solid; border-collapse: collapse; height: 21px; width: 145px;" align="left">

    <asp:TextBox ID="txtFarmerName" runat="server"></asp:TextBox></td>
                                <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                    border-bottom: white 1px solid; border-collapse: collapse; height: 21px; width: 248px;" align="left">
                                    <asp:Label ID="lbl_FatName"  Text="Father's Name:" runat="server" Font-Size="Small"></asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
    <asp:TextBox ID="txt_FatherName" runat="server"></asp:TextBox></td>
                            </tr>
                            
                            
                            <tr>
                                <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                    border-bottom: white 1px solid; border-collapse: collapse; height: 21px; width: 142px;" align="left">
                                    
                                  <asp:Label ID="lbl_KhasaraNo" runat="server" Text="Khasara No. : " Font-Size="Small" ></asp:Label></td>
                                  
                                  <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                    border-bottom: white 1px solid; border-collapse: collapse; height: 21px; width: 145px;" align="left">

                              <asp:TextBox ID="txt_khasrano" runat="server" Font-Size="Small" ></asp:TextBox></td>
                              
                                <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                    border-bottom: white 1px solid; border-collapse: collapse; height: 21px; width: 248px;" align="left">
                                    <asp:Label ID="lbl_B1" runat="server" Font-Size="Small" Text="B-1:"></asp:Label></td>
                                 <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                    border-bottom: white 1px solid; border-collapse: collapse; height: 21px; width: 142px;" align="left">
    <asp:TextBox ID="txt_B1" runat="server" Font-Size="Small"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                    border-bottom: white 1px solid; border-collapse: collapse; height: 21px; width: 142px;" align="left">
    <asp:Label ID="lbl_RationCardNo" runat="server" Text="Ration Card No. :" Font-Size="Small"></asp:Label>
    </td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 145px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">

    <asp:TextBox ID="txt_rctno" runat="server"></asp:TextBox></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 248px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
    <asp:Label ID="lbl_RC_Type" runat="server" Text="Ration Card Type: " Font-Size="Small"></asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:DropDownList ID="DDL_RCardType" runat="server">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 142px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
    <asp:Label ID="lbl_District" runat="server" Text="District:" Font-Size="Small"></asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 145px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:DropDownList ID="DDL_Dist" runat="server" OnSelectedIndexChanged="DDL_Dist_SelectedIndexChanged">
                                    </asp:DropDownList></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 248px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
    <asp:Label ID="lbl_Tehsil" runat="server" Font-Size="Small">Tahsil:</asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:DropDownList ID="DDL_Tah" runat="server" OnSelectedIndexChanged="DDL_Tah_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 142px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:Label ID="lbl_Village" runat="server" Text="Village" Font-Size="Small"></asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 145px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:DropDownList ID="DDL_Village" runat="server" OnSelectedIndexChanged="DDL_Village_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 248px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
    <asp:Label ID="lbl_Halka" runat="server" Font-Size="Small">Halka(Halka No.) :</asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    &nbsp;<asp:TextBox ID="txtHalkaNo" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 142px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                 <asp:Label ID="lbl_RinPustikanumber" runat="server" Text="RinPustika No" Font-Size="Small"></asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 145px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:TextBox ID="txt_RinPustikanumber" runat="server"></asp:TextBox></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 248px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
    <asp:Label ID="lbl_status" runat="server" Font-Size="Small">Status:</asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:DropDownList ID="DDL_status" runat="server">
                                        <asp:ListItem>Active</asp:ListItem>
                                        <asp:ListItem>InActive</asp:ListItem>
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label></td>
                            </tr>
                       
                       
                            
                        </table>
                    </asp:Panel>

    <asp:Button ID="btn_Save" runat="server" Text="Save" OnClick="btn_Save_Click" />
                    &nbsp;&nbsp;<asp:Button ID="btn_update" runat="server" OnClick="btn_update_Click"
                        Text="Update" />&nbsp;
                </td>
            </tr>
   
         
              <tr class="HeadingBlue">
                <td colspan="4" style="border-collapse: collapse; border:solid 1px white;
                    height: 6px; text-align: center; background-color: dimgray; width: 604px;">
                  
                </td>
            </tr>
        </table>
            </td>
        </tr>
        <tr>
            <td style="width: 100px">
        
         <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto">
         <asp:GridView ID="GridView_Farmer" runat="server"
                BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Size="X-Small" AllowPaging="True" OnPageIndexChanging="GridView_Farmer_PageIndexChanging" OnSelectedIndexChanged="GridView_Farmer_SelectedIndexChanged" AutoGenerateSelectButton="True" OnRowDataBound="GridView_Farmer_RowDataBound" AutoGenerateColumns="False" Font-Names="Calibri">
                <RowStyle ForeColor="#330099" BackColor="White" />
                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" Font-Size="X-Small" ForeColor="#FFFFCC" />
            <Columns>
                <asp:BoundField HeaderText="Farmer Name" DataField="FarmerName" SortExpression="FarmerName" />
                <asp:BoundField HeaderText="Farmer Id" DataField="FarmerId" />
                <asp:BoundField HeaderText="Father's Name" DataField="FatherName" SortExpression="FatherName" />
                <asp:BoundField HeaderText="District Name" DataField="DistrictName" />
                <asp:BoundField HeaderText="Tehsil Name" DataField="Tehsilname" />
                <asp:BoundField HeaderText="RinPustikaNO" DataField="RinPustikaNo" />
                <asp:BoundField DataField="PatwariHalkaNo" HeaderText="HalkaNo" />
                <asp:BoundField HeaderText="Village Name" DataField="VillageName" />
                <asp:BoundField HeaderText="Khasara No." DataField="KhasaraNo" SortExpression="KhasaraNo" />
                <asp:BoundField HeaderText="B1 No." DataField="B1_No" />
                <asp:BoundField HeaderText="Ration Card No." DataField="RationCardNo" />
                <asp:BoundField HeaderText="Ration Card Type" DataField="RationCardType" />
                <asp:BoundField HeaderText="Status" DataField="Status" />
            </Columns>
            </asp:GridView>
         </asp:Panel>
            </td>
        </tr>
    </table>
        

</asp:Content>