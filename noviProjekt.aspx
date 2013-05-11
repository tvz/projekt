<%@ Page Language="C#" AutoEventWireup="true" CodeFile="noviProjekt.aspx.cs" Inherits="noviProjekt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="css/ui-lightness/jquery-ui-1.10.3.custom.css" rel="stylesheet" type="text/css" />
     <script type= "text/javascript" src="js/jquery-ui-1.10.3.custom.js"></script>
     
     <script type="text/jscript">
$(function() {
$( "#datepicker" ).datepicker();
});
</script>

    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Ime"></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Label ID="Label2" runat="server" Text="Opis"></asp:Label>
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <asp:Label ID="Label3" runat="server" Text="Vrijednost projekta"></asp:Label>
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <asp:Label ID="Label4" runat="server" Text="Datum isteka projekta"></asp:Label>
        <asp:TextBox ID="datepicker" runat="server"></asp:TextBox>
    
    </div>
    </form>
</body>
</html>
