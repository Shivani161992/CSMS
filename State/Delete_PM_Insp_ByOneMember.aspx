<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/State_MPSCSC.master" AutoEventWireup="true" CodeFile="Delete_PM_Insp_ByOneMember.aspx.cs" Inherits="State_Delete_PM_Insp_ByOneMember" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <html>
    <head>
        <title></title>
        <style>
            .tcolumn
            {
                width: 200px;
                text-align: center;
                border-radius: 32px;
                font-size: 15px;
                font-family: 'Lucida Fax';
            }

                .tcolumn:focus
                {
                    box-shadow: 0 0 25px rgb(107,142,35);
                    padding: 3px 0px 3px 3px;
                    margin: 5px 1px 3px 0px;
                    border: 1px solid rgba(81, 203, 238, 1);
                    width: 0px;
                }

            .tddl
            {
                width: 400px;
                border-radius: 32px;
                height: 35px;
                text-align: center;
                font-family: 'Lucida Fax';
                text-align: center;
            }
            .tddll
            {
                 width: 200px;
                border-radius: 32px;
               
                text-align: center;
                font-family: 'Lucida Fax';
                text-align: center;
            }

            .bttn
            {
                width: 600px;
                height: 40px;
                border-radius: 25px;
                color: #fff!important;
                background-color: #800000!important;
                border-radius: 32px;
            }

            .w3-grey, .w3-hover-grey:hover, .w3-gray, .w3-hover-gray:hover
            {
                color: white!important;
                background-color: #800000!important;
                font-family: 'Lucida Fax';
                font-size: 25px;
            }

            
            .bttn:active
            {
                background-color: yellow;
            }
            .auto-style1
            {
                height: 130px;
            }
        </style>
    </head>
    <body>
        <table style="width: 1025px; border-color: black; background-color: #AEB6BF;" runat="server">
            <tr>
                

                <td colspan="3"></td>
            </tr>
            <tr>
                <td></td>
                <td style="text-align: center;">
                    <table runat="server" border="1" style="width: 1000px; border-color: black; background-color: #1B2631;">
                        <tr>
                            <td colspan="4" style="text-align: center; font-size: 25px; color: #e1bd12; background-color: #1B2631;">
                                <b>Delete  </b>
                                <br />
                                <b>Inspection Acceptance/Rejection Note </b>

                            </td>
                        </tr>
                        <tr>
                            
                            <td style="width: 250px"></td>
                            <td style="width: 250px">
                            </td>
                            <td style="width: 250px">
                            </td>
                            <td style="width: 250px"></td>
                        </tr>



                        <tr>
                            <td style="text-align: left; font-size: 20px; color: #AEB6BF;">
                                <b>District</b></td>
                            <td style="width: 250px">
                                                               <asp:DropDownList ID="ddlDist" runat="server" class="tddll" AutoPostBack="True" OnSelectedIndexChanged="ddlDist_SelectedIndexChanged"></asp:DropDownList>
 
                            </td>
                            <td style="text-align: left; font-size: 20px; color: #AEB6BF;">
                                <b>Issue Center</b></td>
                            <td style="width: 250px">              
     <asp:DropDownList ID="ddlissueC" runat="server" AutoPostBack="True" class="tddll" OnSelectedIndexChanged="ddlissueC_SelectedIndexChanged"></asp:DropDownList>
</td>
                        </tr>

                         <tr>
                            <td style="text-align: left; font-size: 20px; color: #AEB6BF;">
                                <b>Godown</b></td>
                            <td style="width: 250px">
                                                               <asp:DropDownList ID="ddlgd" class="tddll" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlgodown_SelectedIndexChanged"></asp:DropDownList>
 
                            </td>
                            <td style="text-align: left; font-size: 20px; color: #AEB6BF;">
                                <b>Stack</b></td>
                            <td style="width: 250px">              
     <asp:DropDownList ID="ddlSK" runat="server" AutoPostBack="True" class="tddll" OnSelectedIndexChanged="ddlstack_SelectedIndexChanged"></asp:DropDownList>
</td>
                        </tr>





                        <tr id="tractrjt" visible="false">
                            <td style="text-align: left; font-size: 20px; color: #AEB6BF;">
                                <b><asp:Label ID="lblacprjt" runat="server" Text="Label"></asp:Label></b>
                            </td>
                            <td colspan="2">
                                <asp:DropDownList ID="ddlacpt" runat="server" class="tddl" AutoPostBack="True" OnSelectedIndexChanged="ddlacpt_SelectedIndexChanged"></asp:DropDownList>

                            </td>
                            <td style="width: 250px"></td>
                        </tr>
                        
                        <tr>
                            <td></td>
                            <td style="text-align: left; font-size: 20px; color: #AEB6BF; text-align: center;">
                                <b>Crop Year</b>

                            </td>
                            <td>
                                <asp:TextBox ID="txtCropYear" runat="server" Width="250px" class="tcolumn" Style="text-align: center" MaxLength="10" AutoComplete="off" Enabled="false"></asp:TextBox></td>

                            <td></td>

                        </tr>
                        <tr>
                            <td style="text-align: left; font-size: 20px; color: #AEB6BF;">
                                <b>Inspector name</b></td>
                            <td>
                                <asp:TextBox ID="txtInspector" runat="server" Width="250px" class="tcolumn" Style="text-align: center" MaxLength="10" AutoComplete="off" Enabled="false"></asp:TextBox></td>
                            <td style="text-align: left; font-size: 20px; color: #AEB6BF;">
                                <b>Designation</b></td>
                            <td>
                                <asp:TextBox ID="txtDesignation" runat="server" Width="250px" class="tcolumn" Style="text-align: center" MaxLength="10" AutoComplete="off" Enabled="false"></asp:TextBox></td>


                        </tr>
                        <tr>
                            <td style="text-align: left; font-size: 20px; color: #AEB6BF;">
                                <b>District</b></td>
                            <td>
                                <asp:TextBox ID="txtDistrict" runat="server" Width="250px" class="tcolumn" Style="text-align: center" MaxLength="10" AutoComplete="off" Enabled="false"></asp:TextBox></td>
                            <td style="text-align: left; font-size: 20px; color: #AEB6BF;">
                                <b>Issue Center</b></td>
                            <td>
                                <asp:TextBox ID="txtIC" runat="server" Width="250px" class="tcolumn" Style="text-align: center" MaxLength="10" AutoComplete="off" Enabled="false"></asp:TextBox></td>


                        </tr>
                        <tr>
                            <td style="text-align: left; font-size: 20px; color: #AEB6BF;">
                                <b>Godown</b></td>
                            <td>
                                <asp:TextBox ID="txtgodown" runat="server" Width="250px" class="tcolumn" Style="text-align: center" MaxLength="10" AutoComplete="off" Enabled="false"></asp:TextBox></td>
                            <td style="text-align: left; font-size: 20px; color: #AEB6BF;">
                                <b>Stack Name</b></td>
                            <td>
                                <asp:TextBox ID="txtstack" runat="server" Width="250px" class="tcolumn" Style="text-align: center" MaxLength="10" AutoComplete="off" Enabled="false"></asp:TextBox></td>


                        </tr>
                        <tr>
                            <td style="text-align: left; font-size: 20px; color: #AEB6BF;">
                                <b>Date Of Inspection</b></td>
                            <td>
                                <asp:TextBox ID="txtDOI" runat="server" Width="250px" class="tcolumn" Style="text-align: center" MaxLength="10" AutoComplete="off" Enabled="false"></asp:TextBox></td>
                            <td style="text-align: left; font-size: 20px; color: #AEB6BF;">
                                <b>Bags</b></td>
                            <td>
                                <asp:TextBox ID="txtBags" runat="server" Width="250px" class="tcolumn" Style="text-align: center" MaxLength="10" AutoComplete="off" Enabled="false"></asp:TextBox></td>


                        </tr>
                        <tr>
                            <td colspan="4" style="text-align:center; color:#e1bd12; font-size:large;">
                                <b>Miller's Name</b>
                            </td>
                        </tr>
                        <tr id="tronemiller" visible="false">
                            <td style="text-align: left; font-size: 20px; color: #AEB6BF; width:150px">
                                <b>Miller Name</b></td>
                            <td>
                                <asp:TextBox ID="txtmillname" runat="server" Width="250px" class="tcolumn" Style="text-align: center" MaxLength="10" AutoComplete="off" Enabled="false"></asp:TextBox></td>
                            <td style="text-align: left; font-size: 20px; color: #AEB6BF;">
                                <b>Miller District</b></td>
                            <td>
                                <asp:TextBox ID="txtmillDist" runat="server" Width="250px" class="tcolumn" Style="text-align: center" MaxLength="10" AutoComplete="off" Enabled="false"></asp:TextBox></td>


                        </tr>
                        <tr id="trmultiplemiller" visible="false">
                            <td colspan="4" class="auto-style1">
                               <table id="Table1" runat="server" border="1" style="width: 1000px; border-color: black; background-color: #CACFD2  ;">
                                   <tr>
                                       <td style="text-align: left; font-size: 20px; color: #1B2631; width:250px;">
                                <b>1:-Miller Name</b></td>
                                      <td style="width:250px;">
                                <asp:TextBox ID="txtmillnameone" runat="server" Width="250px" class="tcolumn" Style="text-align: center" MaxLength="10" AutoComplete="off" Enabled="false"></asp:TextBox></td>

                                      <td style="text-align: left; font-size: 20px; color: #1B2631; width:250px;" >
                                <b>1:-Miller District</b></td>
                                       <td style="width:250px;">
                                <asp:TextBox ID="txtmilldistone" runat="server" Width="250px" class="tcolumn" Style="text-align: center" MaxLength="10" AutoComplete="off" Enabled="false"></asp:TextBox></td>

                                   </tr>
                                   <tr>
                                       <td style="text-align: left; font-size: 20px; color: #1B2631; width:250px;">
                                <b>2:-Miller Name</b></td>
                                      <td style="width:250px;">
                                <asp:TextBox ID="txtmillnametwo" runat="server" Width="250px" class="tcolumn" Style="text-align: center" MaxLength="10" AutoComplete="off" Enabled="false"></asp:TextBox></td>

                                      <td style="text-align: left; font-size: 20px; color: #1B2631; width:250px;" >
                                <b>2:-Miller District</b></td>
                                       <td style="width:250px;">
                                <asp:TextBox ID="txtmilldisttwo" runat="server" Width="250px" class="tcolumn" Style="text-align: center" MaxLength="10" AutoComplete="off" Enabled="false"></asp:TextBox></td>

                                   </tr>

                                   <tr>
                                       <td style="text-align: left; font-size: 20px; color: #1B2631; width:250px;">
                                <b>3:-Miller Name</b></td>
                                      <td style="width:250px;">
                                <asp:TextBox ID="txtmillnamethree" runat="server" Width="250px" class="tcolumn" Style="text-align: center" MaxLength="10" AutoComplete="off" Enabled="false"></asp:TextBox></td>

                                      <td style="text-align: left; font-size: 20px; color: #1B2631; width:250px;" >
                                <b>3:-Miller District</b></td>
                                       <td style="width:250px;">
                                <asp:TextBox ID="txtmilldistthree" runat="server" Width="250px" class="tcolumn" Style="text-align: center" MaxLength="10" AutoComplete="off" Enabled="false"></asp:TextBox></td>

                                   </tr>

                                   <tr>
                                       <td style="text-align: left; font-size: 20px; color: #1B2631; width:250px;">
                                <b>4:-Miller Name</b></td>
                                      <td style="width:250px;">
                                <asp:TextBox ID="txtmillnamefour" runat="server" Width="250px" class="tcolumn" Style="text-align: center" MaxLength="10" AutoComplete="off" Enabled="false"></asp:TextBox></td>

                                      <td style="text-align: left; font-size: 20px; color: #1B2631; width:250px;" >
                                <b>4:-Miller District</b></td>
                                       <td style="width:250px;">
                                <asp:TextBox ID="txtmilldistfour" runat="server" Width="250px" class="tcolumn" Style="text-align: center" MaxLength="10" AutoComplete="off" Enabled="false"></asp:TextBox></td>

                                   </tr>


                               </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align:center; color:#e1bd12; font-size:large;">
                                <b>Lot Numbers</b>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; font-size: 20px; color: #AEB6BF;">
                                <b>1:-Lot No</b></td>
                            <td>
                                <asp:TextBox ID="txtlotone" runat="server" Width="250px" class="tcolumn" Style="text-align: center" MaxLength="10" AutoComplete="off" Enabled="false"></asp:TextBox></td>
                            <td style="text-align: left; font-size: 20px; color: #AEB6BF;">
                                <b>2:-Lot No</b></td>
                            <td>
                                <asp:TextBox ID="txtlottwo" runat="server" Width="250px" class="tcolumn" Style="text-align: center" MaxLength="10" AutoComplete="off" Enabled="false"></asp:TextBox></td>


                        </tr>
                        <tr>
                            <td style="text-align: left; font-size: 20px; color: #AEB6BF;">
                                <b>3:-Lot No</b></td>
                            <td>
                                <asp:TextBox ID="txtlotthree" runat="server" Width="250px" class="tcolumn" Style="text-align: center" MaxLength="10" AutoComplete="off" Enabled="false"></asp:TextBox></td>
                            <td style="text-align: left; font-size: 20px; color: #AEB6BF;">
                                <b>4:-Lot No</b></td>
                            <td>
                                <asp:TextBox ID="txtlotfour" runat="server" Width="250px" class="tcolumn" Style="text-align: center" MaxLength="10" AutoComplete="off" Enabled="false"></asp:TextBox></td>


                        </tr>
                        <tr>
                            <td style="text-align: left; font-size: 20px; color: #AEB6BF;">
                                <b>5:-Lot No</b></td>
                            <td>
                                <asp:TextBox ID="txtlotfive" runat="server" Width="250px" class="tcolumn" Style="text-align: center" MaxLength="10" AutoComplete="off" Enabled="false"></asp:TextBox></td>
                            <td style="text-align: left; font-size: 20px; color: #AEB6BF;">
                                <b>6:-Lot No</b></td>
                            <td>
                                <asp:TextBox ID="txtlotsix" runat="server" Width="250px" class="tcolumn" Style="text-align: center" MaxLength="10" AutoComplete="off" Enabled="false"></asp:TextBox></td>


                        </tr>
                        <tr>
                            <td style="text-align: left; font-size: 20px; color: #AEB6BF;">
                                <b>7:-Lot No</b></td>
                            <td>
                                <asp:TextBox ID="txtseven" runat="server" Width="250px" class="tcolumn" Style="text-align: center" MaxLength="10" AutoComplete="off" Enabled="false"></asp:TextBox></td>
                            <td style="text-align: left; font-size: 20px; color: #AEB6BF;">
                                <b>8:-Lot No</b></td>
                            <td>
                                <asp:TextBox ID="txteight" runat="server" Width="250px" class="tcolumn" Style="text-align: center" MaxLength="10" AutoComplete="off" Enabled="false"></asp:TextBox></td>


                        </tr>
                         <tr>
                            <td colspan="4" style="text-align:center; color:#e1bd12; font-size:large;">
                                <b>Parameters</b>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                               <table id="Table2" runat="server" border="1" style="width: 1000px; border-color: black; background-color: #CACFD2;">
                                    <tr>
                                       <td style="text-align: left; font-size: 20px; color: #1B2631; width:250px;">
                                <b>TotaS</b></td>
                                      <td style="width:250px;">
                                <asp:TextBox ID="txtTotaS" runat="server" Width="250px" class="tcolumn" Style="text-align: center" MaxLength="10" AutoComplete="off" Enabled="false"></asp:TextBox></td>

                                      <td style="text-align: left; font-size: 20px; color: #1B2631; width:250px;" >
                                <b>Chote ToteS</b></td>
                                       <td style="width:250px;">
                                <asp:TextBox ID="txtChoteToteS" runat="server" Width="250px" class="tcolumn" Style="text-align: center" MaxLength="10" AutoComplete="off" Enabled="false"></asp:TextBox></td>

                                   </tr>
                                   <tr>
                                       <td style="text-align: left; font-size: 20px; color: #1B2631; width:250px;">
                                <b>VijatiyeS</b></td>
                                      <td style="width:250px;">
                                <asp:TextBox ID="txtVijatiyeS" runat="server" Width="250px" class="tcolumn" Style="text-align: center" MaxLength="10" AutoComplete="off" Enabled="false"></asp:TextBox></td>

                                      <td style="text-align: left; font-size: 20px; color: #1B2631; width:250px;" >
                                <b>DamageDaaneS</b></td>
                                       <td style="width:250px;">
                                <asp:TextBox ID="txtDamageDaaneS" runat="server" Width="250px" class="tcolumn" Style="text-align: center" MaxLength="10" AutoComplete="off" Enabled="false"></asp:TextBox></td>

                                   </tr>

                                   <tr>
                                       <td style="text-align: left; font-size: 20px; color: #1B2631; width:250px;">
                                <b>BadrangDaaneS</b></td>
                                      <td style="width:250px;">
                                <asp:TextBox ID="txtBadrangDaaneS" runat="server" Width="250px" class="tcolumn" Style="text-align: center" MaxLength="10" AutoComplete="off" Enabled="false"></asp:TextBox></td>

                                      <td style="text-align: left; font-size: 20px; color: #1B2631; width:250px;" >
                                <b>ChaakiDaaneS</b></td>
                                       <td style="width:250px;">
                                <asp:TextBox ID="txtChaakiDaaneS" runat="server" Width="250px" class="tcolumn" Style="text-align: center" MaxLength="10" AutoComplete="off" Enabled="false"></asp:TextBox></td>

                                   </tr>

                                   <tr>
                                       <td style="text-align: left; font-size: 20px; color: #1B2631; width:250px;">
                                <b>LaalDaaneS</b></td>
                                      <td style="width:250px;">
                                <asp:TextBox ID="txtLaalDaaneS" runat="server" Width="250px" class="tcolumn" Style="text-align: center" MaxLength="10" AutoComplete="off" Enabled="false"></asp:TextBox></td>

                                      <td style="text-align: left; font-size: 20px; color: #1B2631; width:250px;" >
                                <b>NamiS</b></td>
                                       <td style="width:250px;">
                                <asp:TextBox ID="txtnamiS" runat="server" Width="250px" class="tcolumn" Style="text-align: center" MaxLength="10" AutoComplete="off" Enabled="false"></asp:TextBox></td>

                                   </tr>

                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:left;">
                                
                                <asp:Button ID="bttnew" runat="server" BackColor="#1B2631" ForeColor="#e1bd12"   Text="New" OnClick="bttnew_Click" />
                            </td>
                            <td colspan="2" style="height: 50px; text-align: center;">
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="bttn w3-grey" OnClick="bttdelete_Click" />
                            </td>
                            <td style="text-align:right;">
                                <asp:Button ID="bttclose" runat="server" BackColor="#1B2631" ForeColor="#e1bd12"  Text="Close" OnClick="bttclose_Click" />

                            </td>
                        </tr>
                    </table>
                </td>
                <td></td>
            </tr>
            <tr>
                <td colspan="3"></td>
            </tr>
        </table>

    </body>
    </html>




</asp:Content>

