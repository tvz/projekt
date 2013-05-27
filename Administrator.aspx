<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Administrator.aspx.cs" Inherits="Administrator"
    EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href='http://fonts.googleapis.com/css?family=Chango&subset=latin,latin-ext'
        rel='stylesheet' type='text/css' />

    <script type="text/javascript" src="http://code.jquery.com/jquery-1.9.1.js"></script>

    <link rel="icon" type="image/png" href="img/zicalica_favicon.png" />
    <link href="css/custom-theme/jquery-ui-1.10.3.custom.css" rel="stylesheet" />

    <script type="text/javascript" src="js/jquery-1.9.1.js"></script>

    <script type="text/javascript" src="js/jquery-ui-1.10.3.custom.js"></script>

    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
            Projekti</p>
        Status projekta
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
            <asp:ListItem Value="Neodobren">Neodobren</asp:ListItem>
            <asp:ListItem Value="Odobren">Odobren</asp:ListItem>
            <asp:ListItem Value="Svi">Svi</asp:ListItem>
        </asp:DropDownList>
        <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
            AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="607px" RowStyle-HorizontalAlign="Center"
            Font-Names="Arial" Font-Size="Small" AllowPaging="True" PagerSettings-PageButtonCount="10"
            DataKeyNames="ID,name,goal,description,expiration_date,image_path,video_path,enabled"
            OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowDeleting="GridView1_RowDeleting"
            OnRowCancelingEdit="GridView1_RowCancelingEdit">
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" SortExpression="ID"
                    Visible="False" ReadOnly="True" />
                <asp:BoundField DataField="name" HeaderText="Ime Projekta" ConvertEmptyStringToNull="False" />
                <asp:BoundField DataField="description" HeaderText="Opis" ConvertEmptyStringToNull="False" />
                <asp:BoundField DataField="goal" HeaderText="Cilj" ConvertEmptyStringToNull="False" />
                <asp:BoundField DataField="expiration_date" HeaderText="Datum isteka projekta" ConvertEmptyStringToNull="False" />
                <asp:BoundField DataField="created_at" HeaderText="Datum kreiranja" ReadOnly="True"
                    ConvertEmptyStringToNull="False" />
                <asp:BoundField DataField="updated_at" HeaderText="Datum promjene" ReadOnly="True"
                    ConvertEmptyStringToNull="False" />
                <asp:BoundField DataField="image_path" HeaderText="Putanja slike" ConvertEmptyStringToNull="False" />
                <asp:BoundField DataField="video_path" HeaderText="Putanja videa" ConvertEmptyStringToNull="False" />
                <asp:CheckBoxField DataField="enabled" HeaderText="Status" Text="Odobren" />
                <asp:CommandField ButtonType="Button" CausesValidation="False" ShowEditButton="True" />
                <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
            </Columns>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
        <p>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server"></asp:SqlDataSource>
            Korisnici</p>
        Status korisnika
        <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
            <asp:ListItem Value="Svi">Svi</asp:ListItem>
            <asp:ListItem Value="Blokiran">Blokiran</asp:ListItem>
            <asp:ListItem Value="Omogucen">Omogucen</asp:ListItem>
        </asp:DropDownList>
        <asp:GridView ID="GridView2" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
            AutoGenerateColumns="False" DataSourceID="SqlDataSource2" Width="607px" RowStyle-HorizontalAlign="Center"
            Font-Names="Arial" Font-Size="Small" AllowPaging="True" PagerSettings-PageButtonCount="10"
            DataKeyNames="ID,username,enabled"
            OnRowEditing="GridView2_RowEditing" OnRowUpdating="GridView2_RowUpdating" OnRowDeleting="GridView2_RowDeleting"
            OnRowCancelingEdit="GridView2_RowCancelingEdit">
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" SortExpression="ID"
                    Visible="False" ReadOnly="True" />
                <asp:BoundField DataField="username" HeaderText="Korisnicko ime" ConvertEmptyStringToNull="False" />
                <asp:BoundField DataField="created_at" HeaderText="Datum kreiranja" ReadOnly="True"
                    ConvertEmptyStringToNull="False" />
                <asp:BoundField DataField="updated_at" HeaderText="Datum promjene" ReadOnly="True"
                    ConvertEmptyStringToNull="False" />
                <asp:BoundField ConvertEmptyStringToNull="False" DataField="email" 
                    HeaderText="Email" SortExpression="email" />
                <asp:CheckBoxField DataField="enabled" HeaderText="Status" Text="Odobren" />
                <asp:CommandField ButtonType="Button" CausesValidation="False" ShowEditButton="True" />
                <asp:CommandField ButtonType="Button" ShowDeleteButton="True" 
                    CausesValidation="False" />
            </Columns>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
