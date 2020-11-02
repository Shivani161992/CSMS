<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="Bundle_Master.aspx.cs" Inherits="GunnyBags_Bundle_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p>
       <html>
           <head>
               <title>

               </title>
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
            background-color: #004E4E!important;
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
               </style>
           </head>
           <body>

                  <table style="width:800px; background-color:#F8F8F8;" border="1">
        
        <tr>
            <td  colspan="2" style="width:800px; flex-align:center; font-family:Cambria; font-size:large; color:#004E4E;">
                <strong>

              
                Gunny Bags Bundle Type (Master)
                      </strong>
            </td>

        </tr>
                      <tr>
            
            <td colspan="2" style="width:700px; background-color:#004E4E; height:20px; flex-align:center">
                
            </td>
           
        </tr>
                      <tr>
            <td colspan="2" style="width:800px;height:20px;  flex-align:center">

            </td>
        </tr>
                      <tr>
                         <td style="width:400px; flex-align:center; font-family:Cambria; color:#004E4E; ">
                <b>Bundle Type</b>
                          </td>
                           <td style="width:400px; flex-align:center">
                  <asp:TextBox ID="txtBundleType" runat="server" CssClass="tcolumn"  style="width:300px;" ></asp:TextBox>
            </td>
                      </tr>
                    
                     
                       <tr>
            <td colspan="4" style="width:800px; height:20px;  flex-align:center">

            </td>
        </tr>
                      <tr>
            <td colspan="4" style="width:800px; flex-align:center">
                <asp:Button ID="bttSubmit" runat="server" Text="Submit" CssClass="bttn w3-grey" OnClick="bttSubmit_Click" />
                </td>
                        </tr>
                       <tr>
            <td colspan="4" style="width:800px; height:20px;  flex-align:center">

            </td>
        </tr>
                   
                       <tr>
                         <td  colspan="2" style="width:800px; flex-align:center; font-family:Cambria; font-size:large; color:#004E4E;">
                <strong>

              
              Bundle Types
                      </strong>
            </td>
                          
                     </tr>
                     <tr>
                         <td colspan="4" style="width:700px;  flex-align:center">
                             <div style ="height:200px; width:800px; overflow:auto;">

                             <asp:GridView ID="GridView1" Width="900px"  runat="server" AutoGenerateColumns="False" ShowFooter="True"  CellPadding="4" EnableModelValidation="True" ForeColor="#004E4E" GridLines="None"
                  DataKeyNames="BundleID">
                    <AlternatingRowStyle BackColor="#F8F8F8" />
                    <Columns>
                        
                       
                        <asp:BoundField DataField="BundleType" HeaderText="Bundle Type" />
                       
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:LinkButton ID="iddelete" runat="server"  OnClick="lnkdelete_Click" Text="Delete"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#004E4E" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#004E4E" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />

                </asp:GridView>
                                 </div>
                         </td>
                     </tr>

                   </table>
              
           </body>
       </html></p>
</asp:Content>
