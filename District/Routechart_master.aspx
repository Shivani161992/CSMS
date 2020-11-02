<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SCSC_master.master" AutoEventWireup="true" CodeFile="Routechart_master.aspx.cs" Inherits="District_Rootchart_master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript">
     function CheckIsnondecimal(tx) {
         var AsciiCode = event.keyCode;
         var txt = tx.value;
         var txt2 = String.fromCharCode(AsciiCode);
         var txt3 = txt2 * 1;
         if ((AsciiCode < 47) || (AsciiCode > 57)) {
             alert('Please enter only numbers.');
             event.cancelBubble = true;
             event.returnValue = false;
         }
     }

</script>

   <center>
    <table style="width: 698px; height: 400px; border-right: black thin groove; border-top: black thin groove; border-left: black thin groove; border-bottom: black thin groove;" 
           border="1" cellpadding="1">
        <tr>
            <td colspan="2" bgcolor="#FFFF66"><center>
                
                रूट चार्ट मास्टर  </td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 225px; height: 1px; text-align: right; font-size: medium;">
                प्रदाय केंद्र :</td>
            <td style="width: 230px; height: 1px; text-align: left">
                <asp:DropDownList ID="ddlissuecenter" runat="server" Width="194px"  
                    AutoPostBack="True" 
                    onselectedindexchanged="ddlissuecenter_SelectedIndexChanged">
                </asp:DropDownList></td>
        </tr>
         <tr>
            <td style="width: 225px; text-align: right">
                	परिवहनकर्ता का नाम  :</td>
            <td style="width: 230px; text-align: left">
                <asp:DropDownList ID="ddl_transporter" runat="server" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 225px; height: 1px; text-align: right;">
                	रूट क्रमाँक   :</td>
            <td style="width: 230px; height: 1px; text-align: left;">
                <asp:DropDownList ID="ddl_rootno" runat="server" Width="194px">
                    <asp:ListItem>R-1</asp:ListItem>
                    <asp:ListItem>R-2</asp:ListItem>
                    <asp:ListItem>R-3</asp:ListItem>
                    <asp:ListItem>R-4</asp:ListItem>
                    <asp:ListItem>R-5</asp:ListItem>
                    <asp:ListItem>R-6</asp:ListItem>
                    <asp:ListItem>R-7</asp:ListItem>
                    <asp:ListItem>R-8</asp:ListItem>
                    <asp:ListItem>R-9</asp:ListItem>
                    <asp:ListItem>R-10</asp:ListItem>
                    <asp:ListItem>R-11</asp:ListItem>
                    <asp:ListItem>R-12</asp:ListItem>
                    <asp:ListItem>R-13</asp:ListItem>
                    <asp:ListItem>R-14</asp:ListItem>
                    <asp:ListItem>R-15</asp:ListItem>
                    <asp:ListItem>R-16</asp:ListItem>
                    <asp:ListItem>R-17</asp:ListItem>
                    <asp:ListItem>R-18</asp:ListItem>
                    <asp:ListItem>R-19</asp:ListItem>
                    <asp:ListItem>R-20</asp:ListItem>
                    <asp:ListItem>R-21</asp:ListItem>
                    <asp:ListItem>R-22</asp:ListItem>
                    <asp:ListItem>R-23</asp:ListItem>
                    <asp:ListItem>R-24</asp:ListItem>
                    <asp:ListItem>R-25</asp:ListItem>
                    <asp:ListItem>R-26</asp:ListItem>
                    <asp:ListItem>R-27</asp:ListItem>
                    <asp:ListItem>R-28</asp:ListItem>
                    <asp:ListItem>R-29</asp:ListItem>
                    <asp:ListItem>R-30</asp:ListItem>
                    <asp:ListItem>R-31</asp:ListItem>
                    <asp:ListItem>R-32</asp:ListItem>
                    <asp:ListItem>R-33</asp:ListItem>
                    <asp:ListItem>R-34</asp:ListItem>
                    <asp:ListItem>R-35</asp:ListItem>
                    <asp:ListItem>R-36</asp:ListItem>
                    <asp:ListItem>R-37</asp:ListItem>
                    <asp:ListItem>R-38</asp:ListItem>
                    <asp:ListItem>R-39</asp:ListItem>
                    <asp:ListItem>R-40</asp:ListItem>
                    <asp:ListItem>R-41</asp:ListItem>
                    <asp:ListItem>R-42</asp:ListItem>
                    <asp:ListItem>R-43</asp:ListItem>
                    <asp:ListItem>R-44</asp:ListItem>
                    <asp:ListItem>R-45</asp:ListItem>
                    <asp:ListItem>R-46</asp:ListItem>
                    <asp:ListItem>R-47</asp:ListItem>
                    <asp:ListItem>R-48</asp:ListItem>
                    <asp:ListItem>R-49</asp:ListItem>
                    <asp:ListItem>R-50</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 225px; height: 1px; text-align: right;">
                फीड क्रमाँक :</td>
            <td style="width: 230px; height: 1px; text-align: left;">
                <asp:DropDownList ID="ddlfeedno" runat="server" Width="194px">
                           <asp:ListItem>F-1</asp:ListItem>
                    <asp:ListItem>F-2</asp:ListItem>
                    <asp:ListItem>F-3</asp:ListItem>
                    <asp:ListItem>F-4</asp:ListItem>
                    <asp:ListItem>F-5</asp:ListItem>
                    <asp:ListItem>F-6</asp:ListItem>
                    <asp:ListItem>F-7</asp:ListItem>
                    <asp:ListItem>F-8</asp:ListItem>
                    <asp:ListItem>F-9</asp:ListItem>
                    <asp:ListItem>F-10</asp:ListItem>
                    <asp:ListItem>F-11</asp:ListItem>
                    <asp:ListItem>F-12</asp:ListItem>
                    <asp:ListItem>F-13</asp:ListItem>
                    <asp:ListItem>F-14</asp:ListItem>
                    <asp:ListItem>F-15</asp:ListItem>
                    <asp:ListItem>F-16</asp:ListItem>
                    <asp:ListItem>F-17</asp:ListItem>
                    <asp:ListItem>F-18</asp:ListItem>
                    <asp:ListItem>F-19</asp:ListItem>
                    <asp:ListItem>F-20</asp:ListItem>
                    <asp:ListItem>F-21</asp:ListItem>
                    <asp:ListItem>F-22</asp:ListItem>
                    <asp:ListItem>F-23</asp:ListItem>
                    <asp:ListItem>F-24</asp:ListItem>
                    <asp:ListItem>F-25</asp:ListItem>
                    <asp:ListItem>F-26</asp:ListItem>
                    <asp:ListItem>F-27</asp:ListItem>
                    <asp:ListItem>F-28</asp:ListItem>
                    <asp:ListItem>F-29</asp:ListItem>
                    <asp:ListItem>F-30</asp:ListItem>
                    <asp:ListItem>F-31</asp:ListItem>
                    <asp:ListItem>F-32</asp:ListItem>
                    <asp:ListItem>F-33</asp:ListItem>
                    <asp:ListItem>F-34</asp:ListItem>
                    <asp:ListItem>F-35</asp:ListItem>
                    <asp:ListItem>F-36</asp:ListItem>
                    <asp:ListItem>F-37</asp:ListItem>
                    <asp:ListItem>F-38</asp:ListItem>
                    <asp:ListItem>F-39</asp:ListItem>
                    <asp:ListItem>F-40</asp:ListItem>
                    <asp:ListItem>F-41</asp:ListItem>
                    <asp:ListItem>F-42</asp:ListItem>
                    <asp:ListItem>F-43</asp:ListItem>
                    <asp:ListItem>F-44</asp:ListItem>
                    <asp:ListItem>F-45</asp:ListItem>
                    <asp:ListItem>F-46</asp:ListItem>
                    <asp:ListItem>F-47</asp:ListItem>
                    <asp:ListItem>F-48</asp:ListItem>
                    <asp:ListItem>F-49</asp:ListItem>
                    <asp:ListItem>F-50</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 225px; height: 1px; text-align: right;">
                ब्लॉक:</td>
            <td style="width: 230px; height: 1px; text-align: left;">
                <asp:DropDownList ID="ddlBlock" runat="server" Width="194px" 
                   >
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 225px; height: 1px; text-align: right;">
               संभावित प्रदाय का  दिन :</td>
            <td style="width: 230px; height: 1px; text-align: left;">
                <asp:DropDownList ID="ddl_days" runat="server" Width="100px">
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                    <asp:ListItem>6</asp:ListItem>
                    <asp:ListItem>7</asp:ListItem>
                    <asp:ListItem>8</asp:ListItem>
                    <asp:ListItem>9</asp:ListItem>
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>11</asp:ListItem>
                    <asp:ListItem>12</asp:ListItem>
                    <asp:ListItem>13</asp:ListItem>
                    <asp:ListItem>14</asp:ListItem>
                    <asp:ListItem>15</asp:ListItem>
                    <asp:ListItem>16</asp:ListItem>
                    <asp:ListItem>17</asp:ListItem>
                    <asp:ListItem>18</asp:ListItem>
                    <asp:ListItem>19</asp:ListItem>
                    <asp:ListItem>20</asp:ListItem>
                    <asp:ListItem>21</asp:ListItem>
                    <asp:ListItem>22</asp:ListItem>
                    <asp:ListItem>23</asp:ListItem>
                    <asp:ListItem>24</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>26</asp:ListItem>
                    <asp:ListItem>27</asp:ListItem>
                    <asp:ListItem>28</asp:ListItem>
               
                 
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="border-style: hidden; text-align: right;">
                <asp:Label ID="lbl_fps" runat="server" Text="FPS Name:" Visible="False"></asp:Label>
            </td>
            <td style="border-style: hidden; text-align: left;">
                <asp:Label ID="lbl_fps_valuename" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="border-style: hidden; text-align: right;">
                <asp:Label ID="lbl_pay" runat="server" Text="Payment mode:" Visible="False"></asp:Label>
            </td>
            <td style="border-style: hidden; text-align: left;">
                <asp:DropDownList ID="ddl_paymentfps" runat="server" Width="100px" 
                    Visible="False">
                    <asp:ListItem>Credit</asp:ListItem>
                    <asp:ListItem>Cash</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; height: 1px;" colspan="2">
                	&nbsp;</td>
        </tr>
      
       
        <tr>
            <td colspan="2">
                <asp:Panel ID="Panel4" runat="server" Height="300px" HorizontalAlign="Center" 
                    ScrollBars="Vertical">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="None"
                    Width="590px" EnableModelValidation="True" 
                  
    style="font-size: small; font-weight: 700; font-family: Calibri;" >
                        <Columns>
                           <asp:TemplateField HeaderText="क्रमांक">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server"></asp:Label><%# Container.DataItemIndex + 1 %>.
                            </ItemTemplate>
                        </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbSelectAll" runat="server" Width = "30px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="fps_code" HeaderText="एफ. पी. एस. कोड" 
                             SortExpression="fps_code" >
                            <ItemStyle CssClass="griditemlaro" Font-Size="9pt" ForeColor = "Black" 
                                Width="100px" />
                            <HeaderStyle CssClass="gridlarohead" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fps_Uname" HeaderText="एफ. पी. एस. नाम" 
                             SortExpression="fps_Uname" >
                            <ItemStyle CssClass="griditemlaro" Font-Size="9pt" ForeColor = "Black" 
                                Width="450px" />
                            <HeaderStyle CssClass="gridlarohead" HorizontalAlign="Center" 
                             VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Payment mode">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddl_payment" runat="server" Width="100px">
                                        <asp:ListItem>Credit</asp:ListItem>
                                        <asp:ListItem>Cash</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle BackColor="#EFF3FB" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
      
       
        <tr>
            <td colspan="2">
                <asp:Panel ID="Panel3" runat="server" HorizontalAlign="Center" 
                    ScrollBars="Both" Height="400px">
                    <asp:GridView ID="grd_rootchart" runat="server" CellPadding="4" 
                    EnableModelValidation="True" AutoGenerateColumns="False" 
                  
                    
                        
                        
                        
                        DataKeyNames="IssueCenter,block_code,root_no,feed_no,Transporter_id,fps_code,Payment_mode,fps_name,duration_time" style="font-size: x-small" 
    Width="662px" onselectedindexchanged="grd_rootchart_SelectedIndexChanged" Font-Size="X-Small" 
                        ForeColor="#333333" GridLines="None" >
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:CommandField HeaderText="Edit" ShowSelectButton="True" />
                            <asp:TemplateField HeaderText="क्रमांक">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server"></asp:Label><%# Container.DataItemIndex + 1 %>.
                            </ItemTemplate>
                        </asp:TemplateField>
                            <asp:BoundField DataField="DepotName" HeaderText="प्रदाय केंद्र" />
                            <asp:BoundField DataField="Transporter_Name" HeaderText="परिवहनकर्ता  का नाम" />
                            <asp:BoundField DataField="root_no" HeaderText="रूट क्रमाँक  " />
                            <asp:BoundField DataField="feed_no" HeaderText="फीड क्रमाँक " />
                            <asp:BoundField DataField="fps_name" HeaderText="FPS" />
                            <asp:BoundField DataField="block_code" Visible="False" />
                            <asp:BoundField DataField="Transporter_id" HeaderText="transport_code" 
                                Visible="False" />
                            <asp:BoundField DataField="Payment_mode" HeaderText="Payment Mode" />
                            <asp:BoundField DataField="duration_time" HeaderText="संभावित प्रदाय का दिन" />
                        </Columns>
                        <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <PagerStyle ForeColor="#333333" HorizontalAlign="Center" BackColor="#FFCC66" />
                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lbl_error" runat="server"></asp:Label>
                <asp:HiddenField ID="hd_fps" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="width: 225px">
                <asp:Button ID="btnClose" runat="server" Text="New" Width="126px" 
                    OnClick="btnClose_Click" /></td>
            <td style="width: 214px">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="129px" 
                    OnClick="btnSave_Click" Height="26px" />
                <asp:Button ID="btn_update" runat="server" onclick="btn_update_Click" 
                    Text="Update" Visible="False" Width="90px" />
            </td>
        </tr>
        <centre><tr>
            <td colspan="2" align="center">
                <asp:ListBox ID="ddl_fps_name" runat="server" Visible="False" Width="192px">
                </asp:ListBox>
                </td>
        </tr></centre>
        <tr>
            <td colspan="2">
                &nbsp;</td>
        </tr>
    </table>
    </center>
</asp:Content>

