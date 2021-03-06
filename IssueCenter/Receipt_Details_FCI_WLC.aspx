<%@ Page Language="C#" MasterPageFile="~/MasterPage/IssueMaster.master" AutoEventWireup="true" CodeFile="Receipt_Details_FCI_WLC.aspx.cs" Inherits="IssueCenter_Receipt_Details_FCI_WLC" Title="Deposit at Issue Centre from FCI" %>
 
<%@ Register Assembly="CustomControlFreak" Namespace="CustomControlFreak" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
        <p align="center" class="MsoNormal" style="margin: 0in 0in 0pt; text-align: center">
            <span>Deposit at Issue Centre from FCI</span><b style="mso-bidi-font-weight: normal"><span
                style="font-size: 14pt; color: red; font-family: Verdana; mso-ansi-language: EN-US"><?xml
                    namespace="" ns="urn:schemas-microsoft-com:office:office" prefix="o" ?><o:p></o:p></span></b></p>
    </div>
    <div id="ronewmargin">
       
                        
                <table cellpadding ="0" cellspacing ="0" border ="0" style="width: 679px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid;">
                    <tr>
                        <td  colspan="2" style="background-color: #e3dba4">
                            <span style="font-size: 10pt; color: #800080">*Pending Truck Challan Entered By MPSCSC
                            </span>
                        </td>
                        <td class="tdmarginro" style="width: 119px; background-color: #e3dba4">
                        </td>
                        <td style="width: 119px; background-color: #e3dba4">
                        </td>
                        <td class="tdmarginro" style="width: 119px; background-color: #e3dba4">
                        </td>
                    </tr>
                    <tr>
                        <td  style="background-color: #e3dba4" colspan="5">
                         <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"  Width="660px" OnSelectedIndexChanged="GridView2_SelectedIndexChanged" OnRowDataBound ="GridView2_RowDataBound" CellPadding="4" ForeColor="#333333" GridLines="None">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="Challan_No" HeaderText="TC No." SortExpression="Challan_No" >
                    <ItemStyle CssClass="griditemlaro" Font-Size="8pt" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="Vehile_no" HeaderText="Truck No." SortExpression="Vehile_no" >
                    <ItemStyle CssClass="griditemlaro" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="RO_No" HeaderText="FCI RO No." SortExpression="RO_No" >
                    <ItemStyle CssClass="griditemlaro" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                 <asp:TemplateField HeaderText="RO Date">
     
                                                            <ItemTemplate>
                                                            <asp:Label ID="lblChallan" runat="server" 
                                                             Text='<%# Eval("Challan_Date").ToString()%>'>
                                                            </asp:Label>
                                                                     </ItemTemplate>
                                                                <HeaderStyle CssClass="gridlarohead" />
                                                        <ItemStyle CssClass="griditemlaro" />
                                                            </asp:TemplateField>
                <asp:BoundField DataField="Recieved_Bags" HeaderText="Bags" SortExpression="Recieved_Bags" >
                    <ItemStyle CssClass="griditemlaro" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="Recd_Qty" HeaderText="Qty" SortExpression="Recd_Qty" >
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" SortExpression="Commodity_Name" >
                    <ItemStyle Font-Size="10pt" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="IsDeposit" HeaderText="Status of Deposit" SortExpression="IsDeposit" >
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
            </Columns>
                             <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                             <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                             <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                             <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                             <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                             <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdmarginro" style="width: 119px; background-color: #e3dba4">
                        </td>
                        <td style="width: 119px; background-color: #e3dba4">
                        </td>
                        <td class="tdmarginro" style="width: 119px; background-color: #e3dba4">
                        </td>
                        <td style="width: 119px; background-color: #e3dba4">
                        </td>
                        <td class="tdmarginro" style="width: 119px; background-color: #e3dba4">
                        </td>
                    </tr>
                    <tr>
                        <td class="tdmarginro" style="width: 119px; background-color: #e3dba4">
                        District Logged In</td>
                        <td style="width: 119px; background-color: #e3dba4">
                            <asp:TextBox ID="txtdist" runat="server" ReadOnly="True" Width="144px"></asp:TextBox></td>
                        <td class="tdmarginro" style="width: 119px; background-color: #e3dba4">
                        SWC Center</td>
                        <td style="width: 119px; background-color: #e3dba4">
                            <asp:TextBox ID="txtissue" runat="server" ReadOnly="True" Width="109px"></asp:TextBox></td>
                        <td class="tdmarginro" style="width: 119px; background-color: #e3dba4">
                        </td>
                    </tr>
                    <tr>
                       
                          
                                
                                
                                        <td class="tdmarginro" style="width: 119px; background-color: #e3dba4;">
                                            Truck Challan No.</td>
                                        <td style="width: 119px; background-color: #e3dba4;">
                                            &nbsp;<asp:TextBox ID="txtchallan" runat="server" Width="144px"></asp:TextBox></td>
                                        <td class="tdmarginro" style="width: 119px; background-color: #e3dba4;">
                                            Truck No.</td>
                                        <td style="background-color: #e3dba4; width: 119px;">
                                            <asp:TextBox ID="txttruckno" runat="server" Width="110px" ></asp:TextBox>
                                        </td>
                                        <td class="tdmarginro" style="width: 119px; background-color: #e3dba4;">
                                            </td>
                                    </tr>
                                    <tr>
                                        <td class="tdmarginddl" style="width: 119px; background-color: #e3dba4; height: 43px;">
                                            Commodity</td>
                                        <td style="width: 119px; background-color: #e3dba4; height: 43px;">
                                            &nbsp;<asp:DropDownList ID="ddlcomdty" runat="server" Width="155px">
                                            </asp:DropDownList></td>
                                        <td class="tdmarginddl" style="width: 119px; background-color: #e3dba4; height: 43px;">
                                            Category</td>
                                        <td style="background-color: #e3dba4; width: 119px; height: 43px;">
                                            &nbsp;<asp:DropDownList ID="ddlcategory" runat="server" Width="111px">
                                            </asp:DropDownList></td> 
                                        <td style="width: 119px; background-color: #e3dba4; height: 43px;"> </td>
                                    </tr>
                    <tr>
                        <td class="tdmarginddl" style="width: 119px; background-color: #e3dba4">
                            Date of Deposit</td>
                        <td style="width: 119px; background-color: #e3dba4">
                            <cc1:DaintyDate ID="DaintyDate2" runat="server" FormatType="DDMMYYYY" Width="162px" />
                        </td>
                        <td class="tdmarginddl" style="width: 119px; background-color: #e3dba4">
                            Quantity Deposit</td>
                        <td class="tdmarginddl" style="width: 119px; background-color: #e3dba4">
                            <asp:TextBox ID="txtqty" runat="server" Width="106px" MaxLength="13"></asp:TextBox></td>
                        <td style="width: 119px; background-color: #e3dba4">
                            </td>
                    </tr>
                    <tr>
                        <td class="tdmarginddl" style="width: 119px; background-color: #e3dba4">
                            Moisture Content</td>
                        <td style="width: 119px; background-color: #e3dba4">
                            <asp:TextBox ID="txtmoisture" runat="server" Width="150px" MaxLength="5"></asp:TextBox></td>
                        <td class="tdmarginddl" style="width: 119px; background-color: #e3dba4">
                            Scheme</td>
                        <td colspan="2" style="background-color: #e3dba4">
                            <asp:DropDownList ID="ddlscheme" runat="server" Width="182px">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="tdmarginddl" style="width: 119px; background-color: #e3dba4">
                            WCM No.</td>
                        <td style="width: 119px; background-color: #e3dba4">
                            <asp:TextBox ID="txtwcm" runat="server" Width="149px" MaxLength="5"></asp:TextBox></td>
                        <td class="tdmarginddl" style="width: 119px; background-color: #e3dba4">
                            Mode of Weigment</td>
                        <td class="tdmarginddl" style="width: 119px; background-color: #e3dba4">
                            <asp:DropDownList ID="txtwmode" runat="server" Width="135px">
                                <asp:ListItem>100%</asp:ListItem>
                                <asp:ListItem>10%</asp:ListItem>
                            </asp:DropDownList></td>
                        <td style="width: 119px; background-color: #e3dba4">
                        </td>
                    </tr>
                                    <tr>
                                        <td style="width: 119px; background-color: #e3dba4;">
                                        </td>
                                        <td style="width: 119px; background-color: #e3dba4;">
                                            </td>
                                        <td style="width: 119px; background-color: #e3dba4;">
                                            </td>
                                        <td style="background-color: #e3dba4; width: 119px;">
                                            </td>
                                        <td style="width: 119px; background-color: #e3dba4;">
                                            </td>
                                    </tr>
                                
        <asp:Label ID="Label1" runat="server"></asp:Label></table> 
        <table cellpadding ="0" cellspacing ="0" border ="0" class="laromargin" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; background-color: #dad08e">
                    <tr>
                       
                          
                                
                                
                                        <td class="tdmarginro" style="width: 81px;">
                                            </td>
                                        <td>
                                            &nbsp;</td>
                                        <td class="tdmarginro" style="width: 92px;">
                                            </td>
                        <td class="tdmarginro" style="width: 89px">
                        </td>
                        <td class="tdmarginro" style="width: 85px">
                        </td>
                                        <td style="width: 98px;">
                                            &nbsp;</td>
                                        <td class="tdmarginro" style="width: 58px">
                                            </td>
                        <td class="tdmarginro">
                        </td>
                                        <td align ="left" >
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdmarginddl" style="width: 81px;">
                                            Godown No.</td>
                                        <td><asp:DropDownList ID="ddlgodown" runat="server" Width="70px" AutoPostBack="True" OnSelectedIndexChanged="ddlgodown_SelectedIndexChanged">
                                        </asp:DropDownList>&nbsp;</td>
                                        <td class="tdmarginddl" style="width: 92px;">
                                            Stack No.</td>
                                        <td class="tdmarginddl" style="width: 89px">
                                            <asp:DropDownList ID="ddlstack" runat="server" Width="87px" AutoPostBack="True" OnSelectedIndexChanged="ddlstack_SelectedIndexChanged">
                                            </asp:DropDownList></td>
                                        <td class="tdmarginddl" style="width: 85px">
                                            No of Bags.</td>
                                        <td style="width: 98px;">
                                            &nbsp;<asp:TextBox ID="txtbags" runat="server" Width="90px" MaxLength="4"></asp:TextBox></td> 
                                        <td style="width: 58px">
                                            Weight</td>
                                        <td>
                                            <asp:TextBox ID="txtweight" runat="server" Width="94px" MaxLength="13"></asp:TextBox></td>
                                        <td align="left"> </td>
                                    </tr>
                    <tr>
                        <td class="tdmarginddl" style="width: 81px;">
                            </td>
                        <td>
                        </td>
                        <td class="tdmarginddl" style="width: 92px;">
                            Max Capacity</td>
                        <td class="tdmarginddl" style="width: 89px">
                            <asp:TextBox ID="txtmaxcap" runat="server" ReadOnly="True" Width="81px"></asp:TextBox></td>
                        <td class="tdmarginddl" style="width: 85px">
                            Current Cap.</td>
                        <td class="tdmarginddl" style="width: 98px;">
                            <asp:TextBox ID="txtcurcap" runat="server" ReadOnly="True" Width="90px"></asp:TextBox></td>
                        <td style="width: 58px">
                            Available</td>
                        <td>
                            <asp:TextBox ID="txtavlcap" runat="server" ReadOnly="True" Width="94px"></asp:TextBox></td>
                        <td align="left">
                        </td>
                    </tr>
                    <tr>
                        <td class="tdmarginddl" style="width: 81px;">
                            </td>
                        <td>
                            </td>
                        <td class="tdmarginddl" style="width: 92px;">
                            </td>
                        <td class="tdmarginddl" style="width: 89px">
                            <asp:Button ID="btnadstack" runat="server" Text="Add Stack" Width="83px" OnClick="btnadstack_Click" /></td>
                        <td class="tdmarginddl" style="width: 85px">
                        </td>
                        <td class="tdmarginddl" style="width: 98px;">
                            </td>
                        <td style="width: 58px">
                        </td>
                        <td>
                        </td>
                        <td align="left">
                        </td>
                    </tr>
            <tr>
                <td  align="center" colspan="8">
                 <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  Width="425px" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound ="GridView1_RowDataBound" CellPadding="4" ForeColor="#333333" GridLines="None">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="G_Name" HeaderText="Godown Name" SortExpression="G_Name" >
                    <ItemStyle CssClass="griditemlaro" Font-Size="8pt" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="Stack" HeaderText="Stack Name" SortExpression="Stack" >
                    <ItemStyle CssClass="griditemlaro" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="Bags" HeaderText="Bags" SortExpression="Bags" >
                    <ItemStyle CssClass="griditemlaro" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
                <asp:BoundField DataField="Weight" HeaderText="Weight" SortExpression="Weight" >
                    <ItemStyle CssClass="griditemlaro" />
                    <HeaderStyle CssClass="gridlarohead" />
                </asp:BoundField>
            </Columns>
                             <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                             <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                             <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                             <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                             <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                             <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
                </td>
                <td align="left">
                </td>
            </tr>
                    <tr>
                        <td colspan="6">
                            <span lang="EN-GB" style="font-size: 7.5pt; color: purple; font-family: Verdana;
                                mso-ansi-language: EN-GB; mso-fareast-font-family: 'Times New Roman'; mso-bidi-font-family: 'Times New Roman';
                                mso-bidi-language: AR-SA; mso-fareast-language: EN-US">(*Click the Add Stack Button
                                to save the Stacking Information) &nbsp; &nbsp; </span>
                            </td>
                        <td style="width: 58px">
                        </td>
                        <td>
                        </td>
                        <td align="left">
                        </td>
                    </tr>
                                    <tr>
                                        <td style="width: 81px;">
                                            Remarks</td>
                                        <td colspan="6">
                                            <asp:TextBox ID="txtremarks" runat="server" TextMode="MultiLine" Width="478px"></asp:TextBox></td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
            <tr>
                <td style="width: 81px">
                </td>
                <td colspan="6">
                    <asp:Button ID="btnSave" runat="server" Text="Submit" Width="83px" OnClick="btnSave_Click" />
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Close" Width="72px" /></td>
                <td>
                </td>
                <td>
                </td>
            </tr>
                                
        <asp:Label ID="Label2" runat="server"></asp:Label></table> 
    </div>
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

    
</asp:Content>

