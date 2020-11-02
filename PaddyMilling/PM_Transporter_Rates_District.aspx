<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="PM_Transporter_Rates_District.aspx.cs" Inherits="PaddyMilling_PM_Transporter_Rates_District" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <html>
        <head>
            <style>
                .column
        {
            height: 15px;
            color: #6B8E23;
            font-family: 'Lucida Fax';
            text-align: center;
            font-size: 35px;
        }

        .tcolumn
        {
            width: 400px;
            text-align: center;
            border-radius: 32px;
            height: 25px;
            font-size: 15px;
            font-family: 'Lucida Fax';
        }

            .tcolumn:focus
            {
                box-shadow: 0 0 25px rgb(107,142,35);
                padding: 3px 0px 3px 3px;
                margin: 5px 1px 3px 0px;
                border: 1px solid rgba(81, 203, 238, 1);
            }

        .tddl
        {
            width: 400px;
            border-radius: 32px;
            height:25px;
            text-align: center;
            font-family: 'Lucida Fax';
        }

        .bttn
        {
            width: 400px;
            height: 30px;
            border-radius: 25px;
            color: #fff!important;
            background-color: #000!important;
            border-radius: 32px;
        }

        .w3-grey, .w3-hover-grey:hover, .w3-gray, .w3-hover-gray:hover
        {
            color: white!important;
            background-color: #8B0000!important;
            font-family: 'Lucida Fax';
            font-size: 15px;
        }

        .bttn:active
        {
            background-color: yellow;
        }

        .txt
        {
        }

            .txt:focus
            {
            }

        .table
        {
            background-color: #1B2631;
            width: 500px;
            border-radius: 32px;
            margin-right: 35px;
            text-align: center;
            
        }

                .label
                {
                    color:white;
                    font-family: 'Lucida Fax';
            font-size: 15px;
                }

            </style>

        </head>
        <body>
            <form>
                 <table style="width:800px; background-color:#FFFFF0;" border="1">
        
        <tr>
            <td  colspan="4" style="width:800px; flex-align:center; font-family:Cambria; font-size:large; color:#8B0000;">
                <strong>

              
                Transporter Rates (Master)
                      </strong>
            </td>

        </tr>
        <tr>
            
            <td colspan="4" style="width:700px; background-color:#8B0000; height:20px; flex-align:center">
                <asp:Label ID="label" CssClass="label"  runat="server"></asp:Label>
            </td>
           
        </tr>
                     <tr>
            <td colspan="4" style="width:800px;height:20px;  flex-align:center">

            </td>
        </tr>

         <tr>
             
            <td style="width:400px; flex-align:center; font-family:Cambria; color:#8B0000; ">
                <b>Crop Year</b>
            </td>
             
             <td style="width:400px; flex-align:center">
                 <asp:DropDownList ID="ddlCropYear" runat="server" style="width:300px;" class="tddl"  AutoPostBack="True"></asp:DropDownList>
            </td>
            
        </tr>
         <tr>
            <td colspan="4" style="width:800px; height:20px; flex-align:center">

            </td>
        </tr>
        <tr>
            <td style="width:400px; flex-align:center; font-family:Cambria; color:#8B0000;">
               <b> Lead</b>
            </td>
             
             <td style="width:400px; flex-align:center">
                 <asp:DropDownList ID="ddllead" runat="server" style="width:300px;" class="tddl"  AutoPostBack="True" OnSelectedIndexChanged="ddllead_SelectedIndexChanged"></asp:DropDownList>
            </td>
            
        </tr>
         <tr>
            <td colspan="4" style="width:800px; height:20px; flex-align:center">

            </td>
        </tr>
        <tr>
            <td style="width:400px; flex-align:center; font-family:Cambria; color:#8B0000;">
              <b> LRT Rate (per Ton/Km) (Rs.)</b>
            </td>
             
             <td style="width:400px; flex-align:center">
                  <asp:TextBox ID="txtRate" runat="server" CssClass="tcolumn"  style="width:300px;" ></asp:TextBox>
            </td>
            
        </tr>
         <tr>
            <td colspan="4" style="width:800px; height:20px;  flex-align:center">

            </td>
        </tr>
                     <tr>
            <td style="width:400px; flex-align:center; font-family:Cambria; color:#8B0000;">
              <b> FCI Rate (per Ton/Km) (Rs.)</b>
            </td>
             
             <td style="width:400px; flex-align:center">
                  <asp:TextBox ID="txtFCIRate" runat="server" CssClass="tcolumn"  style="width:300px;" ></asp:TextBox>
            </td>
            
        </tr>
         <tr>
            <td colspan="4" style="width:800px; height:20px;  flex-align:center">

            </td>
        </tr>
         <tr>
            <td colspan="4" style="width:800px; flex-align:center">
                <asp:Button ID="bttSubmit" runat="server" Text="Submit" CssClass="bttn w3-grey" Enabled="false" Visible="false" OnClick="bttSubmit_Click" />
                <br />
                                <asp:Button ID="bttUpdate" runat="server" Text="Update" CssClass="bttn w3-grey" Enabled="false" Visible="false" OnClick="bttUpdate_Click" />

            </td>
        </tr>
         <tr>
            <td colspan="4" style="width:800px; height:20px;  flex-align:center">

            </td>
        </tr>
                      <tr>
            
            <td colspan="4" style="width:700px; background-color:#8B0000; height:20px; flex-align:center">
                
            </td>
           
        </tr>
                     <tr>
                         <td colspan="4" style="width:700px; height:20px; flex-align:center">

                         </td>
                     </tr>
                     <tr>
                         <td colspan="4" style="width:700px;  flex-align:center">
                             <div style ="height:200px; width:800px; overflow:auto;">

                             <asp:GridView ID="GridView1" Width="800px"  runat="server" AutoGenerateColumns="False" ShowFooter="True"  CellPadding="4" EnableModelValidation="True" ForeColor="#004E4E" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="district_name" HeaderText="District" />
                        <asp:BoundField DataField="CropYear" HeaderText="Crop Year" />
                        <asp:BoundField DataField="Lead_Name" HeaderText="Lead" />
                         <asp:BoundField DataField="RatesTrans" HeaderText="LRT Rate (per Ton/Km) (Rs.)" />
                      <asp:BoundField DataField="FCI_RatesTrans" HeaderText="FCI Rate (per Ton/Km) (Rs.) " >
                          


                      </asp:BoundField>
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:LinkButton ID="iddelete" runat="server" CausesValidation="false" CommandName="Delete" OnClientClick="return confirm('Are You Sure You Want To Delete?');"  Text="Delete"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#8B0000" Font-Bold="True" ForeColor="black" />
                    <HeaderStyle BackColor="#8B0000" Font-Bold="True" ForeColor="white" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="black" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFFBD6" ForeColor="black" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />

                </asp:GridView>
                                 </div>
                         </td>
                     </tr>

    </table>
            </form>
        </body>
    </html>
     
       
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

