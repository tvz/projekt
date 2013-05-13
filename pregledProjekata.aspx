<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="pregledProjekata.aspx.cs" Inherits="pregledProjekata" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form id="form1" runat="server">
    <asp:Label ID="LabelName" runat="server" Text="Naziv projekta"></asp:Label>
    <asp:TextBox ID="TextBoxName" runat="server"></asp:TextBox><br />
    <asp:Label ID="LabelGoal" runat="server" Text="Vrijednost projekta"></asp:Label>
    <asp:TextBox ID="TextBoxGoal" runat="server"></asp:TextBox>
    <asp:Label ID="LabelExpirationDate" runat="server" Text="Datum isteka projekta"></asp:Label>
    <asp:TextBox ID="TextBoxExpirationDate" runat="server"></asp:TextBox><br />
    <asp:Button ID="ButtonSearch" runat="server" onclick="ButtonSearch_Click" 
        Text="Pretraži" /> <br />
    <asp:Label ID="Label1" runat="server" Text="Sortiraj"></asp:Label>
    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
        onselectedindexchanged="DropDownList1_SelectedIndexChanged">
        <asp:ListItem Value="name">Naziv</asp:ListItem>
        <asp:ListItem Value="goal">Vrijednost</asp:ListItem>
        <asp:ListItem Value="expirationDate">Datum isteka</asp:ListItem>
    </asp:DropDownList>
    <asp:RadioButton ID="RadioButtonDESC" runat="server" AutoPostBack="True" 
        GroupName="sorting" Text="Silazno" 
        oncheckedchanged="DropDownList1_SelectedIndexChanged" />
    <asp:RadioButton ID="RadioButtonASC" runat="server" AutoPostBack="True" 
        GroupName="sorting" Text="Uzlazno" 
        oncheckedchanged="DropDownList1_SelectedIndexChanged" />
    <div runat="server" id="projekti_search">
    </div>
    
    <div id="noviProj">
    <asp:Label ID="LabelName" runat="server" Text="Ime projekta" CssClass="labele"></asp:Label>
    <asp:TextBox ID="TextBoxName" runat="server" CssClass="textbox"></asp:TextBox><br />
    <br />
    <asp:Label ID="LabelGoal" runat="server" Text="Vrijednost projekta" 
            CssClass="labele"></asp:Label>
    <asp:TextBox ID="TextBoxGoal" runat="server" CssClass="textbox"></asp:TextBox>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Moguće je unijeti samo brojeve."
            ValidationExpression="[0-9]+" ControlToValidate="TextBoxGoal" 
            CssClass="poruka" Font-Names="Corbel" Font-Size="15px" ForeColor="#D02552" 
            Height="15px"></asp:RegularExpressionValidator><br />
            <br />
    <asp:Label ID="LabelCreatedAt" runat="server" Text="Datum početka projekta" 
            CssClass="labele"></asp:Label>
    <asp:TextBox ID="TextBoxCreatedAt" runat="server" CssClass="textbox"></asp:TextBox><br /> <br />
    <asp:Label ID="LabelExpirationDate" runat="server" Text="Datum isteka projekta" 
            CssClass="labele"></asp:Label>
    <asp:TextBox ID="TextBoxExpirationDate" runat="server" CssClass="textbox"></asp:TextBox><br /><br />
    <asp:Button ID="ButtonSearch" runat="server" onclick="ButtonSearch_Click" 
        Text="Pretraži" CssClass="gumb2" />
   </div>
    </form>
</asp:Content>

