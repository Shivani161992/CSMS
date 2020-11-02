<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateFarmerRegistration.aspx.cs" Inherits="mpproc_Masters_UpdateFarmerRegistration" MasterPageFile="~/mpproc/MasterPage/AgencyMaster.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title>Update Farmer Registration Form</title>
 <script type="text/javascript" src="../../js/calendar_eu.js"></script>   
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

function CheckIsNum(e,tx)
    {         
        var AsciiCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;  
             
        if ((AsciiCode < 46 && AsciiCode != 47) || (AsciiCode > 57 ))
        {
//        alert(AsciiCode);
          alert('Please enter only numbers.');
          
            return false;
        }                
      
        
    }
  </script>
 
  
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="margin-left:0px; margin-top:0px" width="100%">
        <tr>
            <td style="width: 103px; height: 324px;">
             <table width="100%" cellpadding="2" cellspacing="2" style=" border-collapse: collapse; border:solid 1px white; background-color:#cccc66" id="TABLE1">
              <tr align="center" class="HeadingBlue">
                  <td colspan="4" style="height: 15px; border-collapse: collapse; border:solid 1px white; background-color: dimgray; width: 668px;">
                    </td>
              </tr>
                <tr align="center" > 
                  <td colspan="4" class="boldTxt" style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                      border-bottom: white 1px solid; border-collapse: collapse; width: 668px; height: 20px;">
                      <span style="font-size: 8pt">
                          <asp:Label ID="lbl_titleFar" runat="server" Text="किसान का पंजीयन संशोधित करें " Font-Size="Small" ForeColor="Maroon" Width="744px"></asp:Label></span></td>
                </tr>
            
              <tr align="center" >
                  <td  colspan="4" align="right" style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                      border-bottom: white 1px solid; border-collapse: collapse; height: 14px; width: 668px;">
             
                     </td>
              </tr>
            <tr>
                <td colspan="4" style="border-right: white 1px solid; border-top: white 1px solid;
                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                    width: 668px; height: 195px;">
                    <asp:Panel ID="pnlFCI_OTDepot" runat="server" Width="100%">
                        <table cellpadding="0" cellspacing="0" style="width:100%; border-collapse: collapse; border:solid 1px white;" >
                            <tr>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 103px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
    <asp:Label ID="lbl_District" runat="server" Text="जिला :" Font-Size="Small"></asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 145px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:DropDownList ID="DDL_Dist" runat="server" OnSelectedIndexChanged="DDL_Dist_SelectedIndexChanged">
                                    </asp:DropDownList></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 335px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
    <asp:Label ID="lbl_Tehsil" runat="server" Font-Size="Small">तहसील :</asp:Label>
                                    <asp:DropDownList ID="DDL_Tah" runat="server" OnSelectedIndexChanged="DDL_Tah_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px;">
                                    </td>
                            </tr>
                            <tr>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 103px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:Label ID="lbl_Village" runat="server" Font-Size="Small" Text="निवासी ग्राम का नाम :" Width="154px"></asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 145px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px"><asp:DropDownList ID="DDL_Village" runat="server" OnSelectedIndexChanged="DDL_Tah_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 335px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    &nbsp;
                                    </td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px;">
                                    </td>
                            </tr>
                           
                    
                        
                            <tr>
                                <td align="left" colspan="2" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    </td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 335px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    </td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px;">
                                </td>
                            </tr>
                         
                          
                            <tr>
                                <td align="left" colspan="2" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    </td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 335px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    </td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px;">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    </td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 335px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    </td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px;">
                                </td>
                            </tr>
                            
                            
                      
                            <tr>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 103px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    </td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 145px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    </td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 335px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
    </td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px;">
                                    </td>
                            </tr>
                           
                            <tr>
                                <td align="left" colspan="4" style="border-right: black 2px solid; border-top: black 2px solid;
                                    border-left: black 2px solid; border-bottom: black 2px solid; border-collapse: collapse;
                                    height: 253px" valign="top">
                                  <asp:GridView ID="GridLandRecord" runat="server"
                BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Size="10pt" AllowPaging="True" OnSelectedIndexChanged="GridLandRecord_SelectedIndexChanged" AutoGenerateSelectButton="True" AutoGenerateColumns="False" Font-Names="Calibri" AutoGenerateDeleteButton="True" Width="816px" OnPageIndexChanging="GridLandRecord_PageIndexChanging" OnRowDeleting="GridLandRecord_RowDeleting" DataKeyNames="FarmerID" OnRowDataBound="GridLandRecord_RowDataBound">
                <RowStyle ForeColor="#330099" BackColor="White" />
            <Columns>
                <asp:BoundField HeaderText="पंजीयन क्र " DataField="FarmerId" />
                                            <asp:BoundField DataField="FarmerName" HeaderText="किसान का नाम " />
                                            <asp:BoundField DataField="FarmerFatHUS" HeaderText="पति/पिता का नाम" />
                <asp:BoundField HeaderText="निवासी ग्राम का नाम" DataField="village" />
                                            <asp:BoundField DataField="Tehsil" HeaderText="तहसील " />
                                            <asp:BoundField DataField="Distname" HeaderText="जिला" />
                <asp:BoundField HeaderText="मोबाइल नं" DataField="Mobileno" />
                                            <asp:BoundField DataField="Category" HeaderText="वर्ग" >
                                                <HeaderStyle Font-Size="0px" />
                                                <ItemStyle Font-Size="0px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CropDepDate" HeaderText="किस तिथि को उपज दिया जाना सम्भावित है " />
                <asp:BoundField DataField="FarmerId" HeaderText="FarmerID">
                    <HeaderStyle Font-Size="0px" />
                    <ItemStyle Font-Size="0px" />
                </asp:BoundField>
                <asp:BoundField DataField="DistID_LRMP" HeaderText="DistID_LRMP">
                    <HeaderStyle Font-Size="0px" />
                    <ItemStyle Font-Size="0px" />
                </asp:BoundField>
                <asp:BoundField DataField="TehsileID" HeaderText="TehsileID">
                    <HeaderStyle Font-Size="0px" Width="0px" />
                    <ItemStyle Font-Size="0px" />
                </asp:BoundField>
                <asp:BoundField DataField="Vcode" HeaderText="Village_Code">
                    <HeaderStyle Font-Size="0px" />
                    <ItemStyle Font-Size="0px" />
                </asp:BoundField>
              
              <asp:TemplateField  >
                       <ItemTemplate>
                       
                       
                        <%--  <asp:TextBox ID="txt_FamerID" runat="server" Enabled="false" Visible="false"  Text='<%#Eval("FarmerID")%>'></asp:TextBox>--%>
                       
                      <%--    <input type="Text" id="txt_FarmerID" name="txt_FarmerID"  value='<%#Eval("FarmerID")%>' />
                           <input type="hidden" id="txt_Did" name="txt_Did"  value='<%#Eval("DistID_LRMP")%>' />
                            <input type="hidden" id="txt_tehid" name="txt_tehid"  value='<%#Eval("TehsileID")%>' />
                             <input type="hidden" id="txt_vid" name="txt_vid"  value='<%#Eval("Vcode")%>' />
                       --%>
                          </ItemTemplate>
                   
                         <HeaderStyle HorizontalAlign="Left" Width="70px" />
                    </asp:TemplateField>
                                        
                                        
                                        
                                        </Columns>
                                       
                                       
                                        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                        <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" Font-Size="X-Small" />
                                    </asp:GridView>
                                </td>
                            </tr>
                          
                            <tr>
                                <td align="left" colspan="3" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 7px">
                                    </td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 7px">
                                    </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 12px">
                                    &nbsp;
                                </td>
                            </tr>
                        <tr>
                                <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                    border-bottom: white 1px solid; border-collapse: collapse; height: 10px; width: 103px;" align="left">
                                    
                                  </td>
                            <td align="left" colspan="2" style="border-right: white 1px solid; border-top: white 1px solid;
                                border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                height: 10px">
                                &nbsp;</td>
                                 <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                    border-bottom: white 1px solid; border-collapse: collapse; height: 10px;" align="left">
    </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    &nbsp;</td>
                            </tr>
                       
                            
                        </table>
                    </asp:Panel>
                    &nbsp; &nbsp; &nbsp;&nbsp;
                </td>
            </tr>
   
         
              <tr class="HeadingBlue">
                <td colspan="4" style="border-collapse: collapse; border:solid 1px white;
                    height: 3px; text-align: center; background-color: dimgray; width: 668px;">
                  
                </td>
            </tr>
        </table>
            </td>
        </tr>
        <tr>
            <td style="width: 103px">
      
       
            </td>
        </tr>
    </table>
  
        <script type="text/javascript" language="javascript">
        function ConfirmOnDelete(item)
        {
          if (confirm("Are you sure to delete: " + item + "?")==true)
            return true;
          else
            return false;
        }
    </script>

</asp:Content>


