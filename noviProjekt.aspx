<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="noviProjekt.aspx.cs" Inherits="noviProjekt" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
    <form id="form1" runat="server">
    
        <asp:Label ID="Label_ime" runat="server" Text="Ime"></asp:Label>
        <asp:TextBox ID="TextBox_name" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBox_name"
            ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="Label_opis" runat="server" Text="Opis"></asp:Label>
        <asp:TextBox ID="TextBox_description" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox_description"
            ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="Label_vrijednost" runat="server" Text="Vrijednost projekta"></asp:Label>
        <asp:TextBox ID="TextBox_goal" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox_goal"
            ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="RegularExpressionValidator"
            ValidationExpression="[0-9]+" ControlToValidate="TextBox_goal"></asp:RegularExpressionValidator>
        <br />
        <asp:Label ID="Label4" runat="server" Text="Datum isteka projekta"></asp:Label>
        <asp:TextBox ID="TextBox_expiration_date" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox_expiration_date"
            ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="Label5" runat="server" Text="URL slike"></asp:Label>
        <asp:TextBox ID="TextBox_image_path" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label6" runat="server" Text="URL videa"></asp:Label>
        <asp:TextBox ID="TextBox_video_path" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" Text="Spremi" OnClick="Button1_Click" />
        <br />
        &nbsp;
        
        <!--tu jos treba ubaciti validacije na sva polja i prilagoditi ih. Unos projekta
        trenutno radi. Linkove za css ovog datepickera imate na <a href="http://www.asp.net/ajaxlibrary/CDNjQueryUI1910.ashx">
            http://www.asp.net/ajaxlibrary/CDNjQueryUI1910.ashx</a> pa stavite koji najbolje
        pase za web. Oni su trenutno uljuceni kao vanjski resurs. Ako ih zelite editirat, a mislim da ce trebat malo smanjit velicinu, preuzmite ih sa ovog gore linka.
        Trenutno se sve dodaje na ID korisnika 1 jer jos nemamo userpage. Kreirajte
        bar jednog korisnika sa ID1 da mu se mogu pridjeliti projekti ili promijenite id
        u funkciji na korisnika kojeg vec imate.-->
    </form>
    </div>
</asp:Content>
