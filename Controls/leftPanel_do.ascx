<%@ Control Language="C#" AutoEventWireup="true" CodeFile="leftPanel_do.ascx.cs" Inherits="Controls_leftPanel" %>

 <script type="text/javascript" src="../sdmenu/sdmenu.js">
/***********************************************
* Slashdot Menu script- By DimX
* Submitted to Dynamic Drive DHTML code library: http://www.dynamicdrive.com
* Visit Dynamic Drive at http://www.dynamicdrive.com/ for full source code
***********************************************/
</script>
<script type="text/javascript">
// <![CDATA[
var myMenu;
window.onload = function() {
myMenu = new SDMenu("my_menu");
myMenu.init();
};
// ]]>
</script>
 
<div id="my_menu" class="sdmenu">
<div>
<span>Delivery Order</span>
<a href="<%=Application["aPath"]%>/delivery_order.aspx">New</a>
<a href="http://tools.dynamicdrive.com/favicon/">Update</a>
<a href="http://www.dynamicdrive.com/emailriddler/">Delete</a>
</div>
<div>
<span>Issue</span>
<a href="<%=Application["aPath"]%>/issue_against_do.aspx">Issue Against DO</a>

</div>
<div>
<span>other</span>
<a href="<%=Application["aPath"]%>/issue_against_do.aspx">Issue Against DO</a>

</div>

</div>




