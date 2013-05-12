<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="noviProjekt.aspx.cs" Inherits="noviProjekt" %>

<<<<<<< HEAD
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.10/themes/flick/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.0.js"></script>  
    <script type="text/javascript" src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.10/jquery-ui.min.js"></script>
=======
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link href="css/ui-lightness/jquery-ui-1.10.3.custom.css" rel="stylesheet" type="text/css" />
     <script type= "text/javascript" src="js/jquery-ui-1.10.3.custom.js"></script>
>>>>>>> a8edca6853aacacb7bab364d9f34f2029d29c98e
     
     <script type="text/javascript">
$(function() {
$( "#TextBox_expiration_date" ).datepicker({ dateFormat: "dd.mm.yy" });
});
</script>

    <title></title>
<<<<<<< HEAD
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Ime"></asp:Label>
        <asp:TextBox ID="TextBox_name" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
            ControlToValidate="TextBox_name" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
            <br />
=======
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:Label ID="Label1" runat="server" Text="Ime"></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
>>>>>>> a8edca6853aacacb7bab364d9f34f2029d29c98e
        <asp:Label ID="Label2" runat="server" Text="Opis"></asp:Label>
        <asp:TextBox ID="TextBox_description" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ControlToValidate="TextBox_description" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
            <br />
        <asp:Label ID="Label3" runat="server" Text="Vrijednost projekta"></asp:Label>
        <asp:TextBox ID="TextBox_goal" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ControlToValidate="TextBox_goal" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator
                ID="RegularExpressionValidator1" runat="server" 
            ErrorMessage="RegularExpressionValidator" ValidationExpression="{0-9}*" 
            ControlToValidate="TextBox_goal"></asp:RegularExpressionValidator>
            <br />
        <asp:Label ID="Label4" runat="server" Text="Datum isteka projekta"></asp:Label>
        <asp:TextBox ID="TextBox_expiration_date" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="TextBox_expiration_date" 
            ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
            <br />
        <asp:Label ID="Label5" runat="server" Text="URL slike"></asp:Label>
        <asp:TextBox ID="TextBox_image_path" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label6" runat="server" Text="URL videa"></asp:Label>
        <asp:TextBox ID="TextBox_video_path" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" Text="Spremi" onclick="Button1_Click" />
<<<<<<< HEAD
        <br />
    
    &nbsp;tu jos treba ubaciti validacije na sva polja i prilagoditi ih. Unos projekta trenutno 
        radi. Linkove za css ovog datepickera imate na
        <a href="http://www.asp.net/ajaxlibrary/CDNjQueryUI1910.ashx">
        http://www.asp.net/ajaxlibrary/CDNjQueryUI1910.ashx</a> pa stavite koji najbolje 
        pase za web. Trenutno se sve dodaje na ID korisnika 1 jer jos nemamo userpage. 
        Kreirajte bar jednog korisnika sa ID1 da mu se mogu pridjeliti projekti ili 
        promijenite id u funkciji na korisnika kojeg vec imate.</div>
    </form>
</body>
</html>
=======
</asp:Content>

>>>>>>> a8edca6853aacacb7bab364d9f34f2029d29c98e
