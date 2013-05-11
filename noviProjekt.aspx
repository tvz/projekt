<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="noviProjekt.aspx.cs" Inherits="noviProjekt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link href="css/ui-lightness/jquery-ui-1.10.3.custom.css" rel="stylesheet" type="text/css" />
     <script type= "text/javascript" src="js/jquery-ui-1.10.3.custom.js"></script>
     
     <script type="text/jscript">
$(function() {
$( "#datepicker" ).datepicker();
});
</script>

    <title></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:Label ID="Label1" runat="server" Text="Ime"></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Label ID="Label2" runat="server" Text="Opis"></asp:Label>
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <asp:Label ID="Label3" runat="server" Text="Vrijednost projekta"></asp:Label>
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <asp:Label ID="Label4" runat="server" Text="Datum isteka projekta"></asp:Label>
        <asp:TextBox ID="datepicker" runat="server"></asp:TextBox>
    
        <asp:Button ID="Button1" runat="server" Text="Spremi" onclick="Button1_Click" />
</asp:Content>

