<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<br />
    <br />

    
    <form id="form1" runat="server">
    
    
       <!--login -->
    <div class="log-reg">
        
        <asp:Login ID="Login1" runat="server" DestinationPageUrl="~/index.aspx" 
            onauthenticate="Login1_Authenticate" BorderColor="White" 
            FailureText="Žao nam je, Vaša prijava nije bila uspješna. Molimo pokušajte ponovo." 
            Height="297px" LoginButtonText="PRIJAVA" Width="485px" Font-Size="15px" 
            PasswordLabelText="Lozinka:" 
            PasswordRequiredErrorMessage="Trebate unijeti lozinku." 
            RememberMeText="Zapamti me na ovom računalu." TitleText="Prijava" 
            UserNameLabelText="Korisničko ime:" 
            UserNameRequiredErrorMessage="Trebate unijeti korisničko ime.">
            <CheckBoxStyle Font-Names="Corbel" Font-Size="15px" ForeColor="#000000" 
                CssClass="zapamtiMe" />
            <TextBoxStyle BorderColor="#D02552" CssClass="textbox" />
            <LoginButtonStyle Font-Names="Corbel" CssClass="gumb2" Font-Size="15px" />
            <ValidatorTextStyle ForeColor="#D02552" />
            <LabelStyle Font-Names="Corbel" CssClass="labele" />
            <FailureTextStyle Font-Names="Corbel" Font-Size="15px" ForeColor="#D02552" 
                CssClass="poruka" />
            <TitleTextStyle CssClass="prijava" />
        </asp:Login>
    
    </div>
    
    <!--registracija -->
    <div class="log-reg">
    <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" 
        oncreateduser="CreateUserWizard1_CreatedUser" ContinueButtonText="" 
            CancelButtonText="Odustani" 
            CompleteSuccessText="Vaš korisnički račun je uspješno napravljen" 
            CreateUserButtonText="KREIRAJ" Height="455px" Width="423px"       
            DuplicateEmailErrorMessage="Već postoji korisnički račun koji koristi unesenu e-mail adresu." 
            DuplicateUserNameErrorMessage="Molimo unesite drugo korisničko ime." 
            InvalidEmailErrorMessage="Molimo unesite ispravnu e-mail adresu." 
            InvalidAnswerErrorMessage="Molimo unesite drugo sigurnosno pitanje." 
            InvalidPasswordErrorMessage="Minimalna duljina lozinke je: {0}.Ne-alfanumeričkih znakova potrebno: {1}." 
            InvalidQuestionErrorMessage="Unesite drugo sigurnosno pitanje" 
            
            UnknownErrorMessage="Korisnički račun nije kreiran. Molimo pokušajte ponovo." 
            CssClass="poruka" FinishCompleteButtonText="Završi" 
            FinishPreviousButtonText="Natrag" StartNextButtonText="Dalje">
        <FinishCompleteButtonStyle CssClass="gumb" />
        <CompleteSuccessTextStyle CssClass="zapamtiMe" Font-Size="15px" 
            ForeColor="#D02552" />
        <ContinueButtonStyle CssClass="gumb2" />
        <HeaderStyle CssClass="poruka" />
        <CreateUserButtonStyle CssClass="gumb2" />
        <TitleTextStyle CssClass="labele" />
        <FinishPreviousButtonStyle CssClass="gumb2" />
        <StartNextButtonStyle CssClass="gumb2" />
        <CancelButtonStyle CssClass="gumb2" />
        <WizardSteps>
            <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
                <ContentTemplate>
                    <table class="tablica">
                        <tr>
                            <td align="center" colspan="2" class="reg" height="5px">
                                Registriraj se:<br />
                            </td>
                        </tr>
                        
                        <tr>
                            <td align="right" class="tablica" height="5px">
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" 
                                    CssClass="labele">Korisničko ime:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="UserName" runat="server" CssClass="textbox"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                    ControlToValidate="UserName" ErrorMessage="Korisničko ime je obavezno." 
                                    ToolTip="User Name is required." ValidationGroup="CreateUserWizard1" 
                                    ForeColor="#D02552">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="tablica" height="5px">
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" 
                                    CssClass="labele">Lozinka:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Password" runat="server" TextMode="Password" 
                                    CssClass="textbox"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                    ControlToValidate="Password" ErrorMessage="Lozinka je obavezna." 
                                    ToolTip="Password is required." ValidationGroup="CreateUserWizard1" 
                                    ForeColor="#D02552">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="tablica" height="5px">
                                <asp:Label ID="ConfirmPasswordLabel" runat="server" 
                                    AssociatedControlID="ConfirmPassword" CssClass="labele">Potvrdi lozinku:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password" 
                                    CssClass="textbox"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" 
                                    ControlToValidate="ConfirmPassword" 
                                    ErrorMessage="Potvrda lozinke je obavezna." 
                                    ToolTip="Confirm Password is required." 
                                    ValidationGroup="CreateUserWizard1" ForeColor="#D02552">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="tablica" height="5px">
                                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email" 
                                    CssClass="labele">E-mail:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Email" runat="server" CssClass="textbox"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" 
                                    ControlToValidate="Email" ErrorMessage="E-mail je obavezan." 
                                    ToolTip="E-mail is required." ValidationGroup="CreateUserWizard1" 
                                    ForeColor="#D02552">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="tablica" height="5px">
                                <asp:Label ID="QuestionLabel" runat="server" AssociatedControlID="Question" 
                                    CssClass="labele">Sigurnosno pitanje:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Question" runat="server" CssClass="textbox"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="QuestionRequired" runat="server" 
                                    ControlToValidate="Question" ErrorMessage="Sigurnosno pitanje je obavezno." 
                                    ToolTip="Security question is required." 
                                    ValidationGroup="CreateUserWizard1" ForeColor="#D02552">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="tablica" height="5px">
                                <asp:Label ID="AnswerLabel" runat="server" AssociatedControlID="Answer" 
                                    CssClass="labele">Sigurnosni odgovor:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Answer" runat="server" CssClass="textbox"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="AnswerRequired" runat="server" 
                                    ControlToValidate="Answer" ErrorMessage="Sigurnosni odgovor je obavezan." 
                                    ToolTip="Security answer is required." ValidationGroup="CreateUserWizard1" 
                                    ForeColor="#D02552">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" class="tablica" height="5px">
                                <asp:CompareValidator ID="PasswordCompare" runat="server" 
                                    ControlToCompare="Password" ControlToValidate="ConfirmPassword" 
                                    Display="Dynamic" 
                                    ErrorMessage="Lozinka i potvrda lozinke se moraju poklapati" 
                                    ValidationGroup="CreateUserWizard1" CssClass="poruka" ForeColor=""></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" style="color:#D02552;" class="tablica" height="5px">
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
    </form>
    </div>
</asp:Content>

