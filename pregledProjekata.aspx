<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="pregledProjekata.aspx.cs" Inherits="pregledProjekata" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form id="form1" runat="server">
    <asp:Label ID="LabelName" runat="server" Text="Naziv projekta"></asp:Label>
    <asp:TextBox ID="TextBoxName" runat="server"></asp:TextBox><br />
    <asp:Label ID="LabelGoal" runat="server" Text="Vrijednost projekta"></asp:Label>
    <asp:TextBox ID="TextBoxGoal" runat="server"></asp:TextBox>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Moguće je unijeti samo brojeve."
             ControlToValidate="TextBoxGoal" ValidationExpression="[0,9]+"></asp:RegularExpressionValidator><br />
    <asp:Label ID="LabelExpirationDate" runat="server" Text="Datum isteka projekta"></asp:Label>
    <asp:TextBox ID="TextBoxExpirationDate" runat="server"></asp:TextBox><br />
    <asp:Button ID="ButtonSearch" runat="server" onclick="ButtonSearch_Click" 
        Text="Pretraži" /> <br />
    <asp:Label ID="Label1" runat="server" Text="Sortiraj"></asp:Label>
    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True">
        <asp:ListItem Value="name">Naziv</asp:ListItem>
        <asp:ListItem Value="goal">Vrijednost</asp:ListItem>
        <asp:ListItem Value="expirationDate">Datum isteka</asp:ListItem>
    </asp:DropDownList>
    <asp:RadioButton ID="RadioButtonDESC" runat="server" AutoPostBack="True" 
        GroupName="sorting" Text="Silazno" />
    <asp:RadioButton ID="RadioButtonASC" runat="server" AutoPostBack="True" 
        GroupName="sorting" Text="Uzlazno" />
    <div runat="server" id="projekti_search">
    </div>
    </form>
</asp:Content>

