<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<br />
    <br />

    
    <div>
    
    
    
       <!--login -->
    <div class="log-reg">
    
        <asp:Login ID="Login1" runat="server" DestinationPageUrl="~/index.aspx" 
            onauthenticate="Login1_Authenticate" BorderColor="White" 
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
    
    <!--registracija -->
    <div class="log-reg">
    <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" Answer="a" 
        Question="b" 
        oncreateduser="CreateUserWizard1_CreatedUser" ContinueButtonText="" 
            CancelButtonText="Odustani" 
            CompleteSuccessText="Vaš korisnički račun je uspješno napravljen" 
            CreateUserButtonText="KREIRAJ" Height="455px" Width="423px">
        <CompleteSuccessTextStyle CssClass="zapamtiMe" Font-Size="15px" 
            ForeColor="#D02552" />
        <ContinueButtonStyle CssClass="gumb2" />
        <CreateUserButtonStyle CssClass="gumb2" />
        <CancelButtonStyle CssClass="gumb2" />
        <WizardSteps>
            <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td align="center" colspan="2" class="prijava">
                                Registriraj se:</td>
                        </tr>
                        
                        <tr>
                            <td align="right">
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" 
                                    CssClass="labele">Korisničko ime:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="UserName" runat="server" CssClass="textbox"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                    ControlToValidate="UserName" ErrorMessage="User Name is required." 
                                    ToolTip="User Name is required." ValidationGroup="CreateUserWizard1" 
                                    ForeColor="#D02552">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" 
                                    CssClass="labele">Lozinka:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Password" runat="server" TextMode="Password" 
                                    CssClass="textbox"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                    ControlToValidate="Password" ErrorMessage="Password is required." 
                                    ToolTip="Password is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="ConfirmPasswordLabel" runat="server" 
                                    AssociatedControlID="ConfirmPassword" CssClass="labele">Potvrdi lozinku:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password" 
                                    CssClass="textbox"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" 
                                    ControlToValidate="ConfirmPassword" 
                                    ErrorMessage="Confirm Password is required." 
                                    ToolTip="Confirm Password is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email" 
                                    CssClass="labele">E-mail:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Email" runat="server" CssClass="textbox"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" 
                                    ControlToValidate="Email" ErrorMessage="E-mail is required." 
                                    ToolTip="E-mail is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="QuestionLabel" runat="server" AssociatedControlID="Question" 
                                    Visible="False" CssClass="labele">Sigurnosno pitanje:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Question" runat="server" Visible="False" CssClass="textbox">b</asp:TextBox>
                                <asp:RequiredFieldValidator ID="QuestionRequired" runat="server" 
                                    ControlToValidate="Question" ErrorMessage="Security question is required." 
                                    ToolTip="Security question is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="AnswerLabel" runat="server" AssociatedControlID="Answer" 
                                    Visible="False" CssClass="labele">Sigurnosni odgovor:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Answer" runat="server" Visible="False" CssClass="textbox">a</asp:TextBox>
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
</asp:Content>

