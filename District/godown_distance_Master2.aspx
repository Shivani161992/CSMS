<%@ Page Title="Distance Master" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="godown_distance_Master2.aspx.cs" Inherits="District_PCcenterto_godown_distance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">

    function onlyNumbers(evt) {
        var AsciiCode = event.keyCode;
        var txt = evt.value;
        var txt2 = String.fromCharCode(AsciiCode);
        var txt3 = txt2 * 1;
        if ((AsciiCode < 46) || (AsciiCode > 57)) {
            alert('Please enter only numbers.');
            event.cancelBubble = true;
            event.returnValue = false;
        }

        var num = evt.value;
        var len = num.length;
        var indx = -1;
        indx = num.indexOf('.');
        if (indx != -1) {
            var dgt = num.substr(indx, len);
            var count = dgt.length;
            //alert (count);

            if (AsciiCode == 46) {
                if (num.split(".").length > 1) {
                    alert('दशमलव एक ही बार आ सकता है |');
                    return false;
                }
            }

            if (count > 5) {
                alert("Only 5 decimal digits allowed");
                event.cancelBubble = true;
                event.returnValue = false;
            }



        }

    }
    function onlyNumbersbags(evt) {
        var AsciiCode = event.keyCode;
        var txt = evt.value;
        var txt2 = String.fromCharCode(AsciiCode);
        var txt3 = txt2 * 1;
        if ((AsciiCode <= 46) || (AsciiCode > 57)) {
            alert('Please enter only numbers.');
            event.cancelBubble = true;
            event.returnValue = false;
        }
    }
    //    var e = event || evt; // for trans-browser compatibility
    //    var charCode = e.which || e.keyCode;
    //    if (charCode > 31 && (charCode < 46 || charCode > 57))
    //        return false;
    //    return true;
    //}      

</script>


    <table style="width: 600px; height: 400px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove; border-color: #669999;" 
           border="1" cellpadding="2" cellspacing="0">
        <tr>
            <td colspan="2" 
                
                
                style="font-family: Calibri; font-size: large; color: #000099; background-color: #CCCCFF;"
                background-color: #FF6600">
                
                <strong>Distance Master</strong></center></td>
        </tr>


   <center>
        <tr>
            <td style="width: 341px; height: 1px; text-align: center; font-family: Calibri;">
                District</td>
            <td style="width: 230px; height: 1px; text-align: left">
                <asp:Label ID="lbl_dist" runat="server" 
                    style="font-weight: 700; font-family: Calibri;"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 341px; height: 1px; text-align: center; font-family: Calibri; font-weight: 700;">
                Distance From </td>
            <td style="width: 230px; height: 1px; text-align: left">
                <asp:DropDownList ID="ddl_distancefrom" runat="server" Width="350px" 
                    AutoPostBack="True" 
                    onselectedindexchanged="ddl_distancefrom_SelectedIndexChanged" >
                    
                    
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 341px; height: 1px; text-align: center">
                <asp:Label ID="lbl_purchase" runat="server" Text="Purchase Center" 
                    style="font-family: Calibri"></asp:Label>
            </td>
            <td style="width: 230px; height: 1px; text-align: left">
                <asp:DropDownList ID="ddl_PCeneter" runat="server" Width="350px"  >
                    
                    
                </asp:DropDownList>
                <asp:DropDownList ID="ddl_railheadno" runat="server" Width="270px" 
                    Visible="False" 
              >
                    
                    
                </asp:DropDownList>
            </td>
        </tr>
       
        <tr>
            <td style="width: 341px; height: 1px; text-align: center; font-family: Calibri;">
                To District</td>
            <td style="width: 230px; height: 1px; text-align: left">
                <asp:DropDownList ID="ddl_relatedDistrict" runat="server" Width="194px" 
                    AutoPostBack="True" onselectedindexchanged="ddl_relatedDistrict_SelectedIndexChanged" 
                 >
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 341px; height: 1px; text-align: center; font-family: Calibri;">
                <asp:Label ID="lbl_issue" runat="server" Text="Branch"></asp:Label>
                <asp:Label ID="lbl_mode" runat="server" Text="Transportaion Mode" 
                    Visible="False"></asp:Label>
            </td>
            <td style="width: 230px; height: 1px; text-align: left">
                <asp:DropDownList ID="ddlissuecenter" runat="server" Width="194px" 
                    onselectedindexchanged="ddlissuecenter_SelectedIndexChanged" 
                    AutoPostBack="True" >
                    
                    
                </asp:DropDownList>
                <asp:DropDownList ID="ddl_transportmode" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="ddl_transportmode_SelectedIndexChanged" Visible="False" 
                    Width="194px">
                    <asp:ListItem Value="RD">By Road</asp:ListItem>
                    <asp:ListItem Value="RK">By Rack</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 341px; height: 1px; text-align: center; font-family: Calibri;">
                <asp:Label ID="lbl_godown" runat="server" Text="Godown"></asp:Label>
            </td>
            <td style="width: 230px; height: 1px; text-align: left">
                <asp:DropDownList ID="ddl_Godown" runat="server" Width="350px" 
                    AutoPostBack="True" 
                    onselectedindexchanged="ddl_Godown_SelectedIndexChanged" >
                    
                    
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 341px; height: 1px; text-align: center; font-family: Calibri;">
                Distance(In Km)
            <td style="width: 230px; text-align: left">
                <asp:TextBox ID="txt_distance" runat="server" AutoComplete="off"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td colspan="2" style="font-family: Calibri">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 341px">
                <asp:Button ID="btnClose" runat="server" Text="New" Width="126px" 
                    OnClick="btnClose_Click" CssClass="btn" style="font-family: Calibri" /></td>
            <td style="width: 214px">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="129px" 
                    OnClick="btnSave_Click" CssClass="btn" />
                <asp:HiddenField ID="hd_pc" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="grd_distance" runat="server" BackColor="#CCCCCC" 
                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" 
                    CellSpacing="2" EnableModelValidation="True" AutoGenerateColumns="False" 
                    ForeColor="Black" style="font-family: Calibri" 
                    DataKeyNames="PCCodeOrRailheadcode,Godown_id" 
                    onselectedindexchanged="grd_distance_SelectedIndexChanged" Width="100%" 
                  
                    >
                    <Columns>
                        <asp:CommandField SelectText="Edit" ShowSelectButton="True" />
                        <asp:BoundField HeaderText="PC Center" DataField="Society_Name" />
                        <asp:BoundField DataField="DepotName" HeaderText="IssueCenter" />
                        <asp:BoundField HeaderText="Godown" DataField="Godown_Name" />
                        <asp:BoundField DataField="distance" HeaderText="Distance" />
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                    <PagerStyle ForeColor="Black" HorizontalAlign="Left" BackColor="#CCCCCC" />
                    <RowStyle BackColor="White" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:LinkButton ID="LinkButton23" runat="server" ForeColor="Navy" 
                    PostBackUrl="~/ReportForms_District/Rpt_Distance_Master.aspx">Distance Master Monitoring Report</asp:LinkButton>
                <asp:ListBox ID="ddl_fps_name" runat="server" Visible="False" Width="192px" 
                    style="font-family: Calibri">
                </asp:ListBox>
                <asp:HiddenField ID="hd_fps" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" 
                
                
                style="font-family: Calibri; font-size: large; color: #000099; background-color: #FFCC66;"
                background-color: #FF6600">
                
                &nbsp;</td>
        </tr>
        </table>
    </center>
</asp:Content>


