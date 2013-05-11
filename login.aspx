<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="login.aspx.cs" Inherits="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
     <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href='http://fonts.googleapis.com/css?family=Chango&subset=latin,latin-ext' rel='stylesheet' type='text/css'>
    <style type="text/css">
        #form1
        {
            height: 1952px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
     <div id="logo">
    Logo
    </div>
    <div id="nav_login">
        <asp:HyperLink ID="RegLink" Font-Underline="False" runat="server" 
            NavigateUrl="~/register.aspx" CssClass="LogLink"><span>Registracija</span></asp:HyperLink>
        <asp:HyperLink ID="LogInLink" Font-Underline="False" runat="server" CssClass="LogLink" 
            NavigateUrl="~/login.aspx">Prijava</asp:HyperLink>
    </div>
    
    
    
    
    <br />
    <br />
    <br />
    <br />
    <br />
    
<!--testiranje promjene sa comit-->  
    
    
    <div id="navigacija">
        <asp:HyperLink ID="IndexLink" Font-Underline="False" runat="server" 
            NavigateUrl="~/index.aspx" CssClass="NavLink">Početna</asp:HyperLink>
        <asp:HyperLink ID="NoviProjLink" Font-Underline="False" runat="server" 
            CssClass="NavLink" NavigateUrl="~/noviProjekt.aspx">Započni projekt</asp:HyperLink>
        <asp:HyperLink ID="PregledProjLink" Font-Underline="False" runat="server" 
            CssClass="NavLink" NavigateUrl="~/pregledProjekata.aspx">Pregled projekata</asp:HyperLink>
        <asp:HyperLink ID="OnamaLink" Font-Underline="False" runat="server" 
            CssClass="NavLink" NavigateUrl="~/Onama.aspx">O nama</asp:HyperLink>
</div>
       <hr class="prva_lin" />
    <br />
    <br />

    
    <div>
    
    
    
       <!--login -->
    <div id="login">
        <asp:Login ID="Login1" runat="server" DestinationPageUrl="~/index.aspx" 
            onauthenticate="Login1_Authenticate" BorderColor="White" CssClass="pozadina"
            FailureText="Žao nam je, Vaša prijava nije bila uspješna. Molimo pokušajte ponovo." 
            Height="297px" LoginButtonText="PRIJAVA" Width="485px" Font-Size="15px" 
            PasswordLabelText="Lozinka:" 
            PasswordRequiredErrorMessage="Trebate unjeti lozinku." 
            RememberMeText="Zapamti me na ovom računalu." TitleText="Prijava" 
            UserNameLabelText="Korisničko ime:" 
            UserNameRequiredErrorMessage="Trebate unjeti korisničko ime.">
            <CheckBoxStyle Font-Names="Corbel" Font-Size="15px" ForeColor="#000000" 
                CssClass="zapamtiMe" />
            <TextBoxStyle BorderColor="#D02552" CssClass="textbox" />
            <LoginButtonStyle Font-Names="Corbel" CssClass="gumb2" Font-Size="15px" />
            <ValidatorTextStyle ForeColor="#D02552" />
            <LabelStyle Font-Names="Corbel" CssClass="labele" />
            <FailureTextStyle Font-Names="Corbel" Font-Size="15px" ForeColor="#D02552" />
            <TitleTextStyle CssClass="prijava" />
        </asp:Login>
    </div>
    <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" Answer="a" 
        Question="b" ContinueDestinationPageUrl="~/index.aspx" 
        oncreateduser="CreateUserWizard1_CreatedUser">
        <WizardSteps>
            <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td align="center" colspan="2">
                                Sign Up for Your New Account</td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                    ControlToValidate="UserName" ErrorMessage="User Name is required." 
                                    ToolTip="User Name is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                    ControlToValidate="Password" ErrorMessage="Password is required." 
                                    ToolTip="Password is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="ConfirmPasswordLabel" runat="server" 
                                    AssociatedControlID="ConfirmPassword">Confirm Password:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" 
                                    ControlToValidate="ConfirmPassword" 
                                    ErrorMessage="Confirm Password is required." 
                                    ToolTip="Confirm Password is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Email" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" 
                                    ControlToValidate="Email" ErrorMessage="E-mail is required." 
                                    ToolTip="E-mail is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="QuestionLabel" runat="server" AssociatedControlID="Question" 
                                    Visible="False">Security Question:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Question" runat="server" Visible="False">b</asp:TextBox>
                                <asp:RequiredFieldValidator ID="QuestionRequired" runat="server" 
                                    ControlToValidate="Question" ErrorMessage="Security question is required." 
                                    ToolTip="Security question is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="AnswerLabel" runat="server" AssociatedControlID="Answer" 
                                    Visible="False">Security Answer:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Answer" runat="server" Visible="False">a</asp:TextBox>
                                <asp:RequiredFieldValidator ID="AnswerRequired" runat="server" 
                                    ControlToValidate="Answer" ErrorMessage="Security answer is required." 
                                    ToolTip="Security answer is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:CompareValidator ID="PasswordCompare" runat="server" 
                                    ControlToCompare="Password" ControlToValidate="ConfirmPassword" 
                                    Display="Dynamic" 
                                    ErrorMessage="The Password and Confirmation Password must match." 
                                    ValidationGroup="CreateUserWizard1"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" style="color:Red;">
                                <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:CreateUserWizardStep>
            <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
            </asp:CompleteWizardStep>
        </WizardSteps>
    </asp:CreateUserWizard>
    </div>
    </form>
</body>
</html>
