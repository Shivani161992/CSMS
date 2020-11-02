<%@ Page Language="C#" AutoEventWireup="true" CodeFile="allocationEstimatedToPurchaseCenter.aspx.cs" Inherits="Procurement_allocationEstimatedToPurchaseCenter" MasterPageFile="~/mpproc/MasterPage/AgencyMaster.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <script type="text/javascript" src="../js/DecimalValid.js" ></script>
<table width="600" cellpadding="0" cellspacing="0" style="margin-left:20px;border-collapse: collapse; border:solid 1px white; background-color:#cccc66" id="TABLE1">
              <tr align="center" class="HeadingBlue">
                  <td colspan="4" style="height: 15px; border-collapse: collapse; border:solid 1px white; background-color: #696969;">
                    </td>
              </tr>
                <tr align="center" > 
                  <td colspan="4" class="boldTxt" style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                      border-bottom: white 1px solid; border-collapse: collapse; height: 17px;">
                      <span style="font-size: 8pt">
                          <asp:Label ID="lbl_titleEstAll" runat="server" Text="Estimated Allocation to Purchase Centres " Font-Size="Small" ForeColor="Maroon"></asp:Label></span></td>
                </tr>
            
              <tr align="center" >
                  <td  colspan="4" align="right" style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                      border-bottom: white 1px solid; border-collapse: collapse; height: 14px;">
                     
                   
                     
                     
                     </td>
              </tr>
            <tr>
                <td colspan="4" style="border-right: white 1px solid; border-top: white 1px solid;
                    border-left: white 1px solid; border-bottom: white 1px solid; border-collapse: collapse;
                    ">
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
    
        <asp:DropDownList ID="DDL_Commodity" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDL_Commodity_SelectedIndexChanged">
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
                                </td>
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
                                   <asp:Label ID="lbl_quantmt" runat="server" Text="Quantity(in MT)" Font-Size="Small" ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 276px; height: 19px; background-color: #990000">
                                        <asp:Label ID="lbl_PCenterName" runat="server" Text="Purchase Center Name" Width="161px" Font-Bold="True" ForeColor="White"></asp:Label></td>
                                <td align="left" style="width: 276px; height: 19px; background-color: #990000">
                                </td>
                                <td align="left" style="width: 276px; height: 19px; background-color: #990000">
                                        <asp:Label ID="lbl_Quant_tit" runat="server" Text="Quantity" Font-Bold="True" ForeColor="White"></asp:Label></td>
                                <td align="left" style="width: 276px; height: 19px; background-color: #990000">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 276px; height: 28px; background-color: palegoldenrod">
                                        <asp:Label ID="lbl_PurGrid" runat="server"></asp:Label></td>
                                <td align="left" style="width: 276px; height: 28px; background-color: palegoldenrod">
                                </td>
                                <td align="left" style="width: 276px; height: 28px; background-color: palegoldenrod">
                                        <asp:TextBox ID="txt_pan_quant" runat="server"></asp:TextBox></td>
                                <td align="left" style="width: 276px; height: 28px; background-color: palegoldenrod">
                                </td>
                            </tr>
                                               
                        </table>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Font-Size="Small" ShowFooter="True" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowDataBound="GridView1_RowDataBound" Visible="False">
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
                 
                    
                    <asp:TemplateField HeaderText="Purchase Center Name" >
                            <ItemTemplate>
                               <%#Eval("PurchaseCenterName")%>
                            </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" Width="350px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                     <asp:TemplateField >
                        <ItemTemplate>
                       <asp:TextBox ID="txt_ALLId" runat="server" Enabled="false" Visible="false"  Text='<%#Eval("AllocationTargetId")%>'></asp:TextBox>
                                   
                       </ItemTemplate>
                        
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderText="Quantity"   >
                      
                     
                      <EditItemTemplate>
                                   <asp:TextBox ID="txt_EditQuantity"  runat="server" Text='<%#Eval("Quantity")%>'></asp:TextBox>
                       </EditItemTemplate>
                        <ItemTemplate>
                                    <asp:Label ID="lbl_EditQuantity"  runat="server" Text='<%#Eval("Quantity")%>'></asp:Label>
                         </ItemTemplate>
                         
                         <HeaderStyle HorizontalAlign="Left" Width="100px" />
                    </asp:TemplateField>
                  <asp:TemplateField HeaderText="Edit" ShowHeader="False"> 
                <EditItemTemplate> 
                    <asp:LinkButton ID="lbkUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton> 
                    <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton> 
                </EditItemTemplate> 
              
                <ItemTemplate> 
                    <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton> 
                </ItemTemplate> 
                      <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                 </asp:TemplateField>
                    </Columns>
                            
                            
                        </asp:GridView>
                        &nbsp;
                        <center>
                       <div id="divPur" runat="server">
                       </div>
                       </center>
                        </asp:Panel>
                    
                </td>
            </tr>
            
              <tr>
                                <td align="left" style="border-right: white 1px solid; border-top: white 1px solid;
                                    border-left: white 1px solid; width: 142px; border-bottom: white 1px solid; border-collapse: collapse;
                                    height: 21px">
                                    <asp:Label ID="lbl_u" runat="server" Visible="False" Width="27px"></asp:Label></td>
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
                                    
                                    <asp:Button ID="btn_save" runat="server" Text="Save" OnClick="btn_save_Click" />
                                    <asp:Button ID="Reset" runat="server" OnClick="Reset_Click" Text="Reset" /></td>
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
 