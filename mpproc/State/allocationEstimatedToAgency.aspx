 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="allocationEstimatedToAgency.aspx.cs" Inherits="mpproc_State_allocationEstimatedToAgency" MasterPageFile="~/mpproc/MasterPage/StateMasterPage.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <script type="text/javascript" src="../js/DecimalValid.js" ></script>
 <script type="text/javascript" language="javascript">

function CheckIsNumeric(e,tx)
    {         
        var AsciiCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;  
        
       // alert(AsciiCode);                      
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
  function fnChkCont()
    {
  
   var DDLCom=document.getElementById("ctl00_ContentPlaceHolder1_DDL_Commodity");
       
        if(DDLCom.value=="--Select--")
        {
          alert("Please Select commodity");
          document.getElementById("ctl00_ContentPlaceHolder1_DDL_Commodity").focus();
          event.returnValue = false;
        }
        
      
    }


  </script>
<table width="600" cellpadding="0" cellspacing="0" style="margin-left:20px;border-collapse: collapse; border:solid 1px white; background-color:#cccc66" id="TABLE1">
              <tr align="center" class="HeadingBlue">
                  <td colspan="4" style="height: 15px; border-collapse: collapse; border:solid 1px white; background-color: #696969;">
                    </td>
              </tr>
                <tr align="center" > 
                  <td colspan="4" class="boldTxt" style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                      border-bottom: white 1px solid; border-collapse: collapse; height: 15px;">
                      <span style="font-size: 8pt">
                          <asp:Label ID="lbl_title" runat="server" Text="Estimated Allocation to Agency" Font-Size="Small" ForeColor="Maroon"></asp:Label></span></td>
                </tr>
            
           
            <tr>
                <td colspan="4" style="border-right: white 1px solid; border-top: white 1px solid;
                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;">
                    
                    <asp:Panel ID="pnlFCI_OTDepot" runat="server" Width="100%">
                        <table cellpadding="0" cellspacing="0" style="width:100%; border-collapse: collapse; border:solid 1px white;" >
                            <tr id="trFCIa_dist_depo">
                                <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                    border-bottom: white 1px solid; border-collapse: collapse; height: 21px; width: 142px;" align="left">
                                    <asp:Label ID="lbl_MarkSeas" runat="server" Font-Size="Small">Marketing Season</asp:Label></td>
                                <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                    border-bottom: white 1px solid; border-collapse: collapse; height: 21px; width: 145px;" align="left">
                                    <asp:DropDownList ID="DDL_MarkSeas" runat="server">
        </asp:DropDownList></td>
                                <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                    border-bottom: white 1px solid; border-collapse: collapse; height: 21px; width: 154px;" align="left">
                                    <asp:Label ID="lbl_CropYear"  Text="Crop Year" runat="server" Font-Size="Small"></asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
            
            
           <asp:DropDownList ID="DDL_CropYear" runat="server">
        </asp:DropDownList></td>
                            </tr>
                            
                            
                            <tr>
                                <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                    border-bottom: white 1px solid; border-collapse: collapse; height: 21px; width: 142px;" align="left">
                                  <asp:Label ID="lbl_Agency" runat="server" Text="Agency" Font-Size="Small" ></asp:Label></td>
                                <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                    border-bottom: white 1px solid; border-collapse: collapse; height: 21px; width: 145px;" align="left">
    
        <asp:DropDownList ID="DDL_Agency" runat="server">
        </asp:DropDownList></td>
                                <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                    border-bottom: white 1px solid; border-collapse: collapse; height: 21px; width: 154px;" align="left">
                                    <asp:Label ID="lbl_Commodity" runat="server" Font-Size="Small" Text="Commodity"></asp:Label></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
    
        <asp:DropDownList ID="DDL_Commodity" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDL_Commodity_SelectedIndexChanged" >
        </asp:DropDownList></td>
                            </tr>
                          
                            <tr>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 142px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                </td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 145px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                </td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 154px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                </td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                   <asp:Label ID="lbl_quant" runat="server" Text="Quantity(in MT)" Font-Size="Small" ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4" style="color: #ccccff; height: 10px; background-color: #ccccff">
                                </td>
                            </tr>
                          
                                               
                        </table>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" GridLines="None"  Font-Size="Small" ShowFooter="True" >
                            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" BorderStyle="Inset" BorderWidth="1px" />
                            <FooterStyle BackColor="#000040" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                            <HeaderStyle BackColor="#000040" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                      
                        <asp:TemplateField HeaderText="S.N." >
                            <ItemTemplate>
                               <%#Container.DataItemIndex+1%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" Width="20px" />
                    </asp:TemplateField>
                 
                    
                    <asp:TemplateField  HeaderText="DistrictName">
                            <ItemTemplate>
                            
                                <asp:Label ID="lbl_Grid_Dist" runat="server" Text='<%#Eval("DistrictName")%>'></asp:Label>
                               
                            </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" Width="200px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                   
                    
                     <asp:TemplateField HeaderText="Quantity"  >
                       <ItemTemplate>
                       
                    
                          <input type="Text" id="txt_Quantity" name="txt_Quantity"    onkeypress ="return CheckIsNumeric(event,this);"   style="text-align:right" />
                       
                          </ItemTemplate>
                   
                         <HeaderStyle HorizontalAlign="Left" Width="100px" />
                    </asp:TemplateField>
                    
                     <asp:TemplateField  >
                       <ItemTemplate>
                       
                     
                          <input type="hidden" id="txt_GDid" name="txt_GDid"  value='<%#Eval("Autho_Dist")%>' />
                       
                          </ItemTemplate>
                   
                         <HeaderStyle HorizontalAlign="Left" Width="70px" />
                    </asp:TemplateField>
                              
              
                    </Columns>
                            
                            
                        </asp:GridView>
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" GridLines="None"  Font-Size="Small" ShowFooter="True" OnRowCancelingEdit="GridView2_RowCancelingEdit" OnRowEditing="GridView2_RowEditing"   OnRowDataBound="GridView2_RowDataBound"   >
                            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" BorderStyle="Inset" BorderWidth="1px" />
                            <FooterStyle BackColor="#000040" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                            <HeaderStyle BackColor="#000040" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                      
                        <asp:TemplateField HeaderText="S.N." >
                            <ItemTemplate>
                               <%#Container.DataItemIndex+1%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" Width="20px" />
                    </asp:TemplateField>
                 
                    
                    <asp:TemplateField  HeaderText="DistrictName">
                            <ItemTemplate>
                             
                                <asp:Label ID="lbl_Grid_Dist" runat="server" Text='<%#Eval("DistrictName")%>'></asp:Label>
                               
                            </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" Width="200px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                   
                    
                     
                         <asp:TemplateField HeaderText="Quantity" >
                      
                     
                        <ItemTemplate>
                      
                                      <input type="Text" id="txt_EditQuantity" name="txt_Quantity" value='<%#Eval("Quantity")%>' onkeypress ="return CheckIsNumeric(event,this);"   style="text-align:right" />
                                 
                         </ItemTemplate>
                         
                         <HeaderStyle HorizontalAlign="Left" Width="100px" />
                    </asp:TemplateField>
                 
                    
                    
                     <asp:TemplateField  >
                       <ItemTemplate>
                       
                       
                          <asp:TextBox ID="txt_ALLId" runat="server" Enabled="false" Visible="false"  Text='<%#Eval("AllocationTargetId")%>'></asp:TextBox>
                       
                          <input type="hidden" id="txt_GDid" name="txt_GDid"  value='<%#Eval("Autho_Dist")%>' />
                       
                          </ItemTemplate>
                   
                         <HeaderStyle HorizontalAlign="Left" Width="70px" />
                    </asp:TemplateField>
                    
                    <asp:TemplateField  >
                     
              
                    </asp:TemplateField>
                              
              
                    </Columns>
                            
                            
                        </asp:GridView>
                        <asp:Label ID="lbl_total" runat="server" Text="Total :" Visible="False"></asp:Label>
                        &nbsp;&nbsp;<asp:TextBox ID="txt_total" runat="server" Visible="False" ReadOnly="True" style="text-align:right" ></asp:TextBox>
                        <center>
                       <div id="divPur" runat="server">
                           &nbsp;</div>
                       </center>
                        </asp:Panel>
                    
                </td>
            </tr>
            
              <tr>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 142px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    </td>
                                <td align="center" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 145px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    
                                    <asp:Button ID="btn_save" runat="server" Text="Save" OnClick="btn_save_Click" ToolTip="Click For Record Save"  />
                                    <asp:Button ID="btn_Update" runat="server" OnClick="btn_Update_Click" Text="Save "
                                        Visible="False" />
                                    <asp:Button ID="btn_Reset" runat="server" OnClick="btn_Reset_Click" Text="Reset"
                                        Visible="False" /></td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 154px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    </td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                </td>
                            </tr>
                            
                               <tr>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 142px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                </td>
                                <td align="center" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 145px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    &nbsp;</td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 154px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                </td>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                </td>
                            </tr>
    <tr>
        <td colspan="4" style="border-right: white 1px solid; border-top: white 1px solid;
            border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse">
        </td>
    </tr>
         
              <tr class="HeadingBlue">
                <td colspan="4" style="border-collapse: collapse; border:solid 1px white;
                    height: 15px; text-align: center; background-color: dimgray;">
                </td>
            </tr>
        </table>
        
    


</asp:Content>
