<%@ Page Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true"
    CodeFile="mpscsc_LARO.aspx.cs" Inherits="mpscsc_LARO" Title="Lifting Against Release Order" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat ="server">
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
<script type="text/javascript" language ="javascript ">

        function SelectAll(id)
        {
            //get reference of GridView control
            var grid = document.getElementById("<%= GridView2.ClientID %>");
            //variable to contain the cell of the grid
            var cell;
            
            if (grid.rows.length > 0)
            {
                //loop starts from 1. rows[0] points to the header.
                for (i=1; i<grid.rows.length; i++)
                {
                    //get the reference of first column
                    cell = grid.rows[i].cells[0];
                    
                    //loop according to the number of childNodes in the cell
                    for (j=0; j<cell.childNodes.length; j++)
                    {           
                        //if childNode type is CheckBox                 
                        if (cell.childNodes[j].type =="checkbox")
                        {
                        //assign the status of the Select All checkbox to the cell checkbox within the grid
                            cell.childNodes[j].checked = document.getElementById(id).checked;
                        }
                    }
                }
            }
        }
    </script>
    <script type="text/javascript">function CheckAndUnCheckCheckBoxes(GridView2, CndChecked) 
    {    var checkIds = getCheckBoxes(document.getElementById(GridView2));   
     for (i = 0; i <= checkIds.length - 1; i++)  
        {       checkIds[i].checked = CndChecked;     }
        }     
        function getCheckBoxes(Id) 
        {    var checkboxesArray = new Array();  
           var inputGrd = Id.getElementsByTagName("input");  
             if (inputGrd.length == 0) null;    
              for (i = 0; i <= inputGrd.length -1; i++)
              
                  { 
                  if(inputGrd[i].type == "checkbox")  
                  
                        {                
                        checkboxesArray.push(inputGrd[i]); 
                             
                         }   
                   }   
                 return checkboxesArray; 
        }    </script>
    <script type="text/javascript" language ="javascript "> 
    
    

function Highlight(chk) {

if (chk.checked) {

 $("#" + chk.id).parent("td").parent("tr").css("background-color", "Red");

}else

{

$("#" + chk.id).parent("td").parent("tr").css("background-color", "white");

}

}

</script>
    <div id="hedl">
        &nbsp;</div>
    <div id="ronewmargin">
       
                        
                <table cellpadding ="0" cellspacing ="0" border ="0" class="laromargin" style="width: 619px; border-right: navy 3px double; padding-right: 1px; border-top: navy 3px double; padding-left: 1px; padding-bottom: 1px; border-left: navy 3px double; padding-top: 1px; border-bottom: navy 3px double; border-collapse: collapse;">
                    <tr>
                        <td  colspan="6" style="border-right: 1px solid; border-top: 1px solid;
                            border-left: 1px solid; border-bottom: 1px solid; background-color: #cccccc" align="center">
                            Lifting Against&nbsp; FCI Release Order
                        </td>
                    </tr>
                    <tr>
                        <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                            &nbsp;
                        </td>
                        <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                            &nbsp;
                        </td>
                        <td  colspan="2" align="center" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                            RO Information&nbsp;
                        </td>
                        <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                            &nbsp;
                        </td>
                        <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                            <asp:Label ID="Label10" runat="server" Text="Allot.Month" Visible="False"></asp:Label></td>
                        <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                            <asp:DropDownList ID="ddlalotmm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlalotmm_SelectedIndexChanged"
                                Visible="False" Width="116px">
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
                        <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                            <asp:Label ID="Label11" runat="server" Text="Allot.Year" Visible="False"></asp:Label></td>
                        <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                            <asp:DropDownList ID="ddlallot_year" runat="server" OnSelectedIndexChanged="ddlallot_year_SelectedIndexChanged"
                                Visible="False" Width="116px">
                            </asp:DropDownList></td>
                        <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                        </td>
                        <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                        </td>
                    </tr>
                    <tr>
                       
                          
                                
                                
                                        <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            RO No.</td>
                                        <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            <asp:DropDownList ID="ddlrono" runat="server" Width="116px" OnSelectedIndexChanged="ddlrono_SelectedIndexChanged"
                                                AutoPostBack="True" BackColor="ActiveCaption">
                                                
                                            </asp:DropDownList>
                                        </td>
                                        <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            RO Date</td>
                                        <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            <asp:TextBox ID="txtrodate" runat="server" Width="110px" BackColor="ActiveCaption" ></asp:TextBox>
                                        </td>
                                        <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            RO Qty</td>
                                        <td align ="left" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" >
                                            <asp:TextBox ID="txtroqty" runat="server" Width="110px" BackColor="ActiveCaption" ></asp:TextBox>(Qtl.)
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdmarginddl" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            Commodity</td>
                                        <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            <asp:TextBox ID="txtcomdty" runat="server" Width="110px" BackColor="ActiveCaption" ></asp:TextBox>
                                        </td>
                                        <td class="tdmarginddl" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            Scheme</td>
                                        <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            <asp:TextBox ID="txtscheme" runat="server" Width="110px" ReadOnly="True" BackColor="ActiveCaption" ></asp:TextBox>&nbsp;</td> 
                                        <td class="tdmarginro" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">Bal Qty </td>
                                        <td align="left" style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;"> 
                                            <asp:TextBox ID="txtbalqty" runat="server" ReadOnly="True" Width="110px" BackColor="ActiveCaption"></asp:TextBox>(Qtl.)</td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                        </td>
                                        <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            <asp:Label ID="lblyear" runat="server" Visible="False"></asp:Label></td>
                                        <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            <asp:Label ID="lblmonth" runat="server" Visible="False"></asp:Label></td>
                                        <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            <asp:Label ID="lblscheme" runat="server" Visible="False"></asp:Label></td>
                                        <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            <asp:Label ID="lblcomdty" runat="server" Visible="False"></asp:Label></td>
                                        <td style="background-color: #cfdcdc; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                        </td>
                                    </tr>
                                
        <asp:Label ID="Label1" runat="server"></asp:Label></table> 
                                   <table border="0" cellpadding="0" cellspacing="0" style="width: 619px; border-right: navy 3px double; padding-right: 1px; border-top: navy 3px double; padding-left: 1px; padding-bottom: 1px; border-left: navy 3px double; padding-top: 1px; border-bottom: navy 3px double; border-collapse: collapse;">
                                       <tr>
                                           <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;
                                               <asp:Label ID="lbltid" runat="server" Visible="False" Width="51px"></asp:Label></td>
                                           <td style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                                           </td>
                                           <td align="left"  colspan="3" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               <strong style="color: purple">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                   &nbsp; &nbsp; ***Transportation&nbsp; Information***</strong></td>
                                           <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               &nbsp;&nbsp; &nbsp;&nbsp;
                                           </td>
                                           <td colspan="4" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                           </td>
                                       </tr>
                                       <tr>
                                           <td colspan="6" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               <asp:Label ID="lbldisply" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="#C00000"
                                                   Visible="False"></asp:Label></td>
                                           <td colspan="4" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                           </td>
                                       </tr>
                                       <tr>
                                           <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               &nbsp;&nbsp;
                                           </td>
                                           <td  colspan="2" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" >                                             
                                               <strong style="color: navy; font-weight: bold; font-size: 14px;">Select
                                               Transport Order Number-&gt;</strong></td>
                                           <td class="tdmarginro" colspan="2" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               <asp:DropDownList ID="ddltono" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddltono_SelectedIndexChanged1" Width="125px">
                                                   <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
                                               </asp:DropDownList></td>
                                           <td colspan="1" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                           </td>
                                           <td colspan="4" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                           </td>
                                       </tr>
                                       <tr>
                                           <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                           </td>
                                           <td  colspan="4" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               <asp:Label ID="lblchk" runat="server" Text="* Please Select Only Single Check Box "
                                                   Visible="False"></asp:Label></td>
                                           <td colspan="1" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                           </td>
                                           <td colspan="4" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                           </td>
                                       </tr>
                                       <tr>
                                           <td class="tdmarginro" colspan="6" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"  Width="511px" OnSelectedIndexChanged="GridView2_SelectedIndexChanged" OnRowDataBound ="GridView2_RowDataBound" CellPadding="4" ForeColor="#333333" GridLines="None">
            <Columns>
                <asp:TemplateField >  
                      
                   <ItemTemplate >        
                     <asp:CheckBox ID="CheckBox1" AutoPostBack="true" runat="server"
                        OnCheckedChanged="CheckBox1_CheckedChanged1" ValidationGroup="2" />
 
                 </ItemTemplate >     
                    <ItemStyle CssClass="griditemlaro" />
                </asp:TemplateField >
                <asp:BoundField DataField="TO_Number" HeaderText="TO No." SortExpression="TO_Number" >
                    <ItemStyle CssClass="griditemlaro" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="District" HeaderText="FCI District" SortExpression="District" >
                    <ItemStyle CssClass="griditemlaro" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="DepoName" HeaderText="Depot" SortExpression="DepoName" >
                    <ItemStyle CssClass="griditemlaro" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="district_name" HeaderText="To District" SortExpression="district_name" >
                    <ItemStyle CssClass="griditemlaro" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="DepotName" HeaderText="IssueCenter" SortExpression="DepotName" >
                    <ItemStyle CssClass="griditemlaro" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="Quantity" HeaderText="Qty." SortExpression="Quantity" >
                    <ItemStyle CssClass="griditemlaro" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="Cumulative_Qty" HeaderText="Lifted Qty." SortExpression="Cumulative_Qty">
                    <ItemStyle CssClass="griditemlaro" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="Pending_Qty" HeaderText="Pending Qty." SortExpression="Pending_Qty">
                    <ItemStyle CssClass="griditemlaro" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="toDistrict" HeaderText="DCode" SortExpression="toDistrict" >
                    <HeaderStyle Font-Size="0px" />
                    <ItemStyle Font-Size="0pt" />
                </asp:BoundField>
                <asp:BoundField DataField="toIssueCenter" HeaderText="ICode" SortExpression="toIssueCenter" >
                    <HeaderStyle Font-Size="0px" />
                    <ItemStyle Font-Size="0px" />
                </asp:BoundField>
                <asp:BoundField DataField="Transporter_Name" HeaderText="Transporter" SortExpression="Transporter_Name" >
                    <ItemStyle Font-Size="0px" />
                    <HeaderStyle Font-Size="0px" />
                </asp:BoundField>
                <asp:BoundField DataField="Trunsuction_Id" HeaderText="ID" SortExpression="Trunsuction_Id">
                    <ItemStyle Font-Size="0px" />
                    <HeaderStyle Font-Size="0px" />
                </asp:BoundField>
            </Columns>
                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <EditRowStyle BackColor="#999999" />
        </asp:GridView></td>
                                           <td colspan="4" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                           </td>
                                       </tr>
                                       <tr>
                                           <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                           </td>
                                           <td class="tdmarginro" colspan="2" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                           </td>
                                           <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               <asp:Button ID="btnGet" runat="server" OnClick="btnGet_Click" Text="GetData" Width="138px" Visible="False" /></td>
                                           <td colspan="2" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                           </td>
                                           <td colspan="4" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                           </td>
                                       </tr>
                                       <tr>
                                           <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               <asp:Label ID="Label5" runat="server" Text="Depot Type" Visible="False"></asp:Label></td>
                                           <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               <asp:DropDownList ID="ddldepottype" runat="server" Width="109px" OnSelectedIndexChanged="ddldepottype_SelectedIndexChanged" Visible="False">
                                                <asp:ListItem Value="01" Selected ="True" >-Select-</asp:ListItem>
                                                <asp:ListItem Value="02"  >OWNED</asp:ListItem>
                                                <asp:ListItem Value="03" >CWC</asp:ListItem>
                                                <asp:ListItem Value="04" >SWC</asp:ListItem>
                                                <asp:ListItem Value="05" >PRIVATE</asp:ListItem>
                                                <asp:ListItem Value="06" >Hired(Private party)</asp:ListItem>
                                               </asp:DropDownList></td>
                                           <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               <asp:Label ID="lblfdist" runat="server" Visible="False" Width="52px"></asp:Label></td>
                                           <td align="left" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               <asp:Label ID="lblfdepo" runat="server" Visible="False" Width="52px"></asp:Label></td>
                                           <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                           </td>
                                           <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                           </td>
                                           <td colspan="4" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                           </td>
                                       </tr>
                                    <tr>
                                        <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            <asp:Label ID="Label6" runat="server" Text="FCI Region" Visible="False"></asp:Label></td>
                                        <td style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
                                            <asp:DropDownList ID="ddlfcidist" runat="server" Width="110px" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" AutoPostBack="True" Font-Bold="True" Font-Italic="True" Font-Size="13px" ForeColor="Navy" Visible="False">
                                            <asp:ListItem Value="01" Selected="True">-Select-</asp:ListItem>
                                            </asp:DropDownList>&nbsp;</td>
                                        <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            <asp:Label ID="Label7" runat="server" Text="Dispatch Depot:" Visible="False"></asp:Label></td>
                                        <td style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" align="left">
                                            <asp:DropDownList ID="ddlfcidepo" runat="server" Width="146px" OnSelectedIndexChanged="ddlfcidepo_SelectedIndexChanged" Font-Bold="True" Font-Italic="True" Font-Size="13px" ForeColor="Navy" Visible="False">
                                            <asp:ListItem Value="01" Selected="True">-Select-</asp:ListItem>
                                            </asp:DropDownList>&nbsp;</td>
                                        <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            <asp:Label ID="Label8" runat="server" Text="Destination District" Visible="False"></asp:Label></td>
                                        <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            <asp:DropDownList ID="ddldistrict" runat="server" Width="126px" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged" AutoPostBack ="true" Font-Bold="True" Font-Italic="True" Font-Size="13px" ForeColor="Navy" Visible="False">
                                              
                                            </asp:DropDownList></td>
                                        <td colspan="4" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                        </td>
                                    </tr>
                                       <tr>
                                           <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                           </td>
                                           <td  colspan="2" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               </td>
                                           <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;"></td>
                                           <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               <asp:Label ID="Label9" runat="server" Text="Destination Depot" Visible="False"></asp:Label></td>
                                           <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            <asp:DropDownList ID="ddlissue" runat="server" Width="127px" Font-Bold="True" Font-Italic="True" Font-Size="13px" ForeColor="Navy" Visible="False" >
                                                                                            </asp:DropDownList></td>
                                           <td colspan="4" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                           </td>
                                       </tr>
                                       <tr>
                                           <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            Transporter Name/Lead</td>
                                           <td class="tdmarginro" colspan="2" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               &nbsp;<asp:DropDownList ID="txttrans" runat="server"  OnSelectedIndexChanged="ddlgtype_SelectedIndexChanged" Font-Size="10px">
                                                <asp:ListItem Value="01" Selected="True">-Select-</asp:ListItem>
                                            </asp:DropDownList>&nbsp;
                                           </td>
                                           <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               Challan Date</td>
                                           <td  style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" colspan="2">
                                          <asp:TextBox ID="challandate" runat="server" Width="111px"></asp:TextBox>
                                          <script type  ="text/javascript">
	new tcal ({
				'formname': '0',
				'controlname': 'ctl00_ContentPlaceHolder1_challandate'
	    });
	     </script>
                                          </td>
                                          
                                           <td colspan="4" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                           </td>
                                       </tr>
                                       <tr>
                                           <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            Challan No</td>
                                           <td colspan="2" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               <asp:DropDownList ID="ddlchallan" runat="server" Width="154px"  OnSelectedIndexChanged="ddlchallan_SelectedIndexChanged" AutoPostBack="True" Visible="False">
                                                   <asp:ListItem Selected="True" Value="01">-Select-</asp:ListItem>
                                               </asp:DropDownList>
                                            <asp:TextBox ID="txtchallan" runat="server" Width="148px" MaxLength="15" ></asp:TextBox>
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                   ControlToValidate="txtchallan" ErrorMessage="Challn No Required" Font-Bold="True"
                                                   ValidationGroup="1">*</asp:RequiredFieldValidator></td>
                                           <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            Truck No:</td>
                                           <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            <asp:TextBox ID="txtvehno" runat="server" Width="110px" MaxLength="12" ></asp:TextBox></td>
                                           <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtvehno"
                                                   ErrorMessage="Truck No Required" Font-Bold="True" ValidationGroup="1">*</asp:RequiredFieldValidator></td>
                                           <td colspan="4" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                           </td>
                                       </tr>
                                       <tr>
                                           <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               No. of Bags</td>
                                           <td colspan="2" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               <asp:TextBox ID="txtnobags" runat="server" Width="147px" MaxLength="4" ></asp:TextBox>
                                               <asp:Label ID="lblpendqty" runat="server" Visible="False"></asp:Label></td>
                                           <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               Dispatch Qty</td>
                                           <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                                <asp:TextBox ID="txtqtysend" runat="server" Width="110px" MaxLength="13"></asp:TextBox></td>
                                           <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               &nbsp; &nbsp;Qtls./Kgms. &nbsp;&nbsp;
                                               </td>
                                           <td colspan="4" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                           </td>
                                       </tr>
                                       <tr>
                                           <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               Dispatch Time</td>
                                           <td  colspan="2" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               
    <asp:DropDownList ID="ddlhour" runat="server">
                            <asp:ListItem Value="01">01</asp:ListItem>
                            <asp:ListItem Value="02">02</asp:ListItem>
                            <asp:ListItem Value="03">03</asp:ListItem>
                            <asp:ListItem Value="04">04</asp:ListItem>
                            <asp:ListItem Value="05">05</asp:ListItem>
                            <asp:ListItem Value="06">06</asp:ListItem>
                            <asp:ListItem Value="07">07</asp:ListItem>
                            <asp:ListItem Value="08">08</asp:ListItem>
                            <asp:ListItem Value="09">09</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="11">11</asp:ListItem>
                            <asp:ListItem Value="12">12</asp:ListItem>
                        </asp:DropDownList>
                        :

  
 
 
 <asp:DropDownList ID="ddlminute" runat="server">
                            <asp:ListItem Value="00">00</asp:ListItem>
                            <asp:ListItem Value="01">01</asp:ListItem>
                            <asp:ListItem Value="02">02</asp:ListItem>
                            <asp:ListItem Value="03">03</asp:ListItem>
                            <asp:ListItem Value="04">04</asp:ListItem>
                            <asp:ListItem Value="05">05</asp:ListItem>
                            <asp:ListItem Value="06">06</asp:ListItem>
                            <asp:ListItem Value="07">07</asp:ListItem>
                            <asp:ListItem Value="08">08</asp:ListItem>
                            <asp:ListItem Value="09">09</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="11">11</asp:ListItem>
                            <asp:ListItem Value="12">12</asp:ListItem>
                            <asp:ListItem Value="13">13</asp:ListItem>
                            <asp:ListItem Value="14">14</asp:ListItem>
                            <asp:ListItem Value="15">15</asp:ListItem>
                            <asp:ListItem Value="16">16</asp:ListItem>
                            <asp:ListItem Value="17">17</asp:ListItem>
                            <asp:ListItem Value="18">18</asp:ListItem>
                            <asp:ListItem Value="19">19</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="21">21</asp:ListItem>
                            <asp:ListItem Value="22">22</asp:ListItem>
                            <asp:ListItem Value="23">23</asp:ListItem>
                            <asp:ListItem Value="24">24</asp:ListItem>
                              <asp:ListItem Value="25">25</asp:ListItem>
                            <asp:ListItem Value="26">26</asp:ListItem>
                            <asp:ListItem Value="27">27</asp:ListItem>
                            <asp:ListItem Value="28">28</asp:ListItem>
                            <asp:ListItem Value="29">29</asp:ListItem>
                            <asp:ListItem Value="30">30</asp:ListItem>
                            <asp:ListItem Value="31">31</asp:ListItem>
                            <asp:ListItem Value="32">32</asp:ListItem>
                            <asp:ListItem Value="33">33</asp:ListItem>
                            <asp:ListItem Value="34">34</asp:ListItem>
                            <asp:ListItem Value="35">35</asp:ListItem>
                            <asp:ListItem Value="36">36</asp:ListItem>
                            <asp:ListItem Value="37">37</asp:ListItem>
                            <asp:ListItem Value="38">38</asp:ListItem>
                            <asp:ListItem Value="39">39</asp:ListItem>
                            <asp:ListItem Value="40">40</asp:ListItem>
                            <asp:ListItem Value="41">41</asp:ListItem>
                            <asp:ListItem Value="42">42</asp:ListItem>
                            <asp:ListItem Value="43">43</asp:ListItem>
                            <asp:ListItem Value="44">44</asp:ListItem>
                            <asp:ListItem Value="45">45</asp:ListItem>
                            <asp:ListItem Value="46">46</asp:ListItem>
                            <asp:ListItem Value="47">47</asp:ListItem>
                            <asp:ListItem Value="48">48</asp:ListItem>
                            <asp:ListItem Value="79">49</asp:ListItem>
                            <asp:ListItem Value="50">50</asp:ListItem>
                            <asp:ListItem Value="51">51</asp:ListItem>
                            <asp:ListItem Value="52">52</asp:ListItem>
                            <asp:ListItem Value="53">53</asp:ListItem>
                            <asp:ListItem Value="54">54</asp:ListItem>
                            <asp:ListItem Value="55">55</asp:ListItem>
                            <asp:ListItem Value="56">56</asp:ListItem>
                            <asp:ListItem Value="57">57</asp:ListItem>
                            <asp:ListItem Value="58">58</asp:ListItem>
                            <asp:ListItem Value="59">59</asp:ListItem>
                        
                        </asp:DropDownList>

:
 
    <asp:DropDownList ID="ddlampm" runat="server" Width="50px">
    <asp:ListItem Value="01">AM</asp:ListItem>
    <asp:ListItem Value="02">PM</asp:ListItem>
    </asp:DropDownList>
                                           </td>
                                           <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               Moisture(%)
                                           </td>
                                           <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                           <asp:TextBox ID="txtmoisture" runat="server" Width="110px" MaxLength="5"></asp:TextBox></td>
                                           <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                           </td>
                                           <td colspan="4" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                           </td>
                                       </tr>
                                       <tr>
                                           <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               <asp:Label ID="Label3" runat="server" Text="Crop Year" Visible="False"></asp:Label></td>
                                           <td class="tdmarginro" colspan="2" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                            <asp:DropDownList ID="ddlcropyear" runat="server" Width="136px"  AutoPostBack ="false" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Visible="False">
                                                <asp:ListItem Value="01" Selected="True">-Not Indicated-</asp:ListItem>
                                                <asp:ListItem Value="02">2010-2009</asp:ListItem>
                                                <asp:ListItem Value="03">2009-2008</asp:ListItem>
                                                <asp:ListItem Value="04">2008-2007</asp:ListItem>
                                                <asp:ListItem Value="05">2007-2006</asp:ListItem>
                                                <asp:ListItem Value="06">2006-2005</asp:ListItem>
                                                <asp:ListItem Value="07">2005-2004</asp:ListItem>
                                                <asp:ListItem Value="08">2004-2003</asp:ListItem>
                                                <asp:ListItem Value="09">2003-2002</asp:ListItem>
                                                <asp:ListItem Value="10">2002-2001</asp:ListItem>
                                                <asp:ListItem Value="11">2001-2000</asp:ListItem>
                                            </asp:DropDownList>
                                            </td>
                                           <td style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               <asp:Label ID="Label4" runat="server" Text="Category" Visible="False"></asp:Label></td>
                                           <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               <asp:DropDownList ID="ddlcategory" runat="server" Width="90px" Visible="False">
                                            </asp:DropDownList></td>
                                           <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               &nbsp; &nbsp; &nbsp;&nbsp;
                                           </td>
                                           <td colspan="4" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                           </td>
                                       </tr>
                                       <tr>
                                           <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               <asp:Label ID="lblstatus" runat="server" Visible="False">N</asp:Label></td>
                                           <td style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;" >
                                               </td>
                                           <td align="right" colspan="2" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               <asp:Button ID="btnaddmore" runat="server" OnClick="btnaddmore_Click" Text="Add More"
                                                   Visible="False" Width="153px" Font-Bold="True" ForeColor="Navy" />
                                               <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClick="btnsubmit_Click" Width="154px" ValidationGroup="1" Font-Bold="True" ForeColor="Navy" /></td>
                                           <td class="tdmarginro" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Close" Width="119px" Font-Bold="True" ForeColor="Maroon" /></td>
                                           <td align="left" colspan="1" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                               <asp:HyperLink ID="HyperLink1" runat="server" Width="129px" NavigateUrl="#" Visible="False">Print Truck Challan</asp:HyperLink></td>
                                           <td colspan="4" style="background-color: #cfdcc8; font-size: 10pt; position: static; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
                                           </td>
                                       </tr>
                                       <tr>
                                           <td class="tdmarginro" style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                               border-bottom: white 1px solid; color: black; background-color: lightblue;">
                                           </td>
                                           <td align="left" colspan="3" style="border-right: white 1px solid; border-top: white 1px solid;
                                               border-left: white 1px solid; border-bottom: white 1px solid; color: black; background-color: lightblue;">
                                           </td>
                                           <td class="tdmarginro" colspan="2" style="border-right: white 1px solid; border-top: white 1px solid;
                                               border-left: white 1px solid; border-bottom: white 1px solid; color: black; background-color: lightblue;">
                                               <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                                   Width="199px" ShowSummary="False" ValidationGroup="1" />
                                           </td>
                                           <td colspan="4" style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; color: black; border-bottom: white 1px solid; background-color: lightblue;">
                                           </td>
                                       </tr>
                                </table>
                      
               
            
      <table style="width: 658px">
      <tr>
      <td align="center">
          <asp:Label ID="Label2" runat="server" Text="Label" Font-Italic="True" ForeColor="#C00000" Visible="False" Width="375px"></asp:Label>
      </td>
      </tr>
      <tr>
      <td> 
       <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound ="GridView1_RowDataBound" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" 
       Width="654px" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="5" PagerSettings-Visible ="true" AllowPaging="True">
                                                   <Columns>
                                                       <asp:CommandField ShowSelectButton="True" HeaderText="Update" >
                                                           <HeaderStyle CssClass="gridlarohead" />
                                                           <ItemStyle CssClass="griditemlaro" />
                                                       </asp:CommandField>
                                                       <asp:BoundField DataField="Challan_No" HeaderText="TC NO." SortExpression="Challan_No">
                                                           <ItemStyle CssClass="griditemlaro" />
                                                           <HeaderStyle CssClass="gridlarohead" />
                                                       </asp:BoundField>
                                                    <asp:TemplateField HeaderText="TC Date">
     
                                                            <ItemTemplate>
                                                            <asp:Label ID="lblChallan" runat="server" 
                                                             Text='<%# Eval("Challan_Date").ToString()%>'>
                                                            </asp:Label>
                                                                     </ItemTemplate>
                                                                <HeaderStyle CssClass="gridlarohead" />
                                                        <ItemStyle CssClass="griditemlaro" />
                                                            </asp:TemplateField>
                                                       <asp:BoundField DataField="RO_No" HeaderText="RO No." ReadOnly="True" SortExpression="RO_No" >
                                                           <HeaderStyle CssClass="gridlarohead" />
                                                           <ItemStyle CssClass="griditemlaro" />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="TO_Number" HeaderText="TO NO." SortExpression="TO_Number">
                                                           <ItemStyle CssClass="griditemlaro" />
                                                           <HeaderStyle CssClass="gridlarohead" />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Depot_Name" HeaderText="IC Name" ReadOnly="True" SortExpression="Depot_Name" >
                                                           <HeaderStyle CssClass="gridlarohead" />
                                                           <ItemStyle CssClass="griditemlaro" />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Vehicle_No" HeaderText="Vehile No." ReadOnly="True" SortExpression="Vehicle_No" >
                                                           <HeaderStyle CssClass="gridlarohead" />
                                                           <ItemStyle CssClass="griditemlaro" />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="No_of_Bags" HeaderText="Bags" ReadOnly="True" SortExpression="No_of_Bags" >
                                                           <HeaderStyle CssClass="gridlarohead" />
                                                           <ItemStyle CssClass="griditemlaro" />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Qty_send" HeaderText="Qty" ReadOnly="True" SortExpression="Qty_send" >
                                                           <HeaderStyle CssClass="gridlarohead" />
                                                           <ItemStyle CssClass="griditemlaro" />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" ReadOnly="True" SortExpression="Commodity_Name" >
                                                           <HeaderStyle CssClass="gridlarohead" />
                                                           <ItemStyle CssClass="griditemlaro" />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="IsRecieved" HeaderText="Status of Deposit" ReadOnly="True"
                                                           SortExpression="IsRecieved" >
                                                           <HeaderStyle CssClass="gridlarohead" />
                                                           <ItemStyle CssClass="griditemlaro" />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Trunsuction_Id" HeaderText="Transuction" SortExpression="Trunsuction_Id">
                                                           <HeaderStyle Font-Size="0px" />
                                                           <ItemStyle Font-Size="0px" />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Issue_center" HeaderText="IC" SortExpression="Issue_center">
                                                           <HeaderStyle Font-Size="0px" />
                                                           <ItemStyle Font-Size="0px" />
                                                       </asp:BoundField>
                                                   </Columns>
                                                   <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                   <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" Font-Bold="True" />
                                                   <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                   <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                   <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
           <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
           <EditRowStyle BackColor="#999999" />
                                               </asp:GridView>
      
      </td>
      </tr>
      </table>
    </div>
     
         
        <div>
        <table border ="0" cellpadding ="0" cellspacing ="0" class="gridfooter">
        <tr>
                        <td>
                            <div >
                                <asp:LinkButton ID="Firstbutton" Text='<img border="0" src="../images/firstpage.gif" align="absmiddle">'
                                    CommandArgument="0" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false"  />&nbsp;
                                <asp:LinkButton ID="Prevbutton" Text='<img border="0" src="../images/prevpage.gif" align="absmiddle">'
                                    CommandArgument="prev" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" />&nbsp;
                                <asp:LinkButton ID="Nextbutton" Text='<img border="0" src="../images/nextpage.gif" align="absmiddle">'
                                    CommandArgument="next" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" />&nbsp;
                                <asp:LinkButton ID="Lastbutton" Text='<img border="0" src="../images/lastpage.gif" align="absmiddle">'
                                    CommandArgument="last" runat="server" OnCommand="FooterPagerClick" CausesValidation ="false" OnClick="Lastbutton_Click" />&nbsp;&nbsp;
                             
                            </div>
                        </td>
                    </tr>
        </table>
        </div>
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
