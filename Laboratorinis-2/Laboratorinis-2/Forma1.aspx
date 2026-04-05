<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Forma1.aspx.cs" Inherits="Laboratorinis_2.Forma1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="StyleSheet1.css" rel="stylesheet" type="text/css" />
    <title></title>
    <style type="text/css">
        .auto-style1 {
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="MinDistance_Label" runat="server" Text="Minimalus atstumas"></asp:Label>
            <br />
            <asp:TextBox ID="MinDistance_DataTextBox" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="MinDistance_DataTextBox" ErrorMessage="Minimalus atstumas yra privalomas" ForeColor="Red">*</asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Label ID="MaxPopulation_Label" runat="server" Text="Maksimali populiacija"></asp:Label>
            <br />
            <asp:TextBox ID="MaxPopulation_DataTextBox" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="MaxPopulation_DataTextBox" ErrorMessage="Maksimali populiacija yra privaloma" ForeColor="Red">*</asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Label ID="StartCity_Label" runat="server" Text="Pradinis miestas"></asp:Label>
            <br />
            <asp:TextBox ID="StartCity_TextBox" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="StartCity_TextBox" ErrorMessage="Praidinis miestas yra privalomas" ForeColor="Red">*</asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Label ID="DestinationCity_Label" runat="server" Text="Nelankytinas meistas"></asp:Label>
            <br />
            <asp:TextBox ID="AvoidCity_TextBox" runat="server"></asp:TextBox>
            <br />
            <br />
        </div>
        <div>
            <br />
            <asp:Label ID="Data_Header" runat="server" Text="Pradiniai duomenys"></asp:Label>
            <br />
            <br />
            <asp:Button ID="Data_UploadFromInternal" runat="server" Text="Įkelti iš App_Data" OnClick="Data_UploadFromInternal_Click" class="button" />
            <br />
            <br />
            <asp:Label ID="Data_Label1" runat="server" Text="Keliai tarp miestų (U8b.txt)"></asp:Label>
            <br />
            <asp:TextBox ID="Data_TextBox1" runat="server" TextMode="MultiLine" CssClass="auto-style1" Width="346px" Height="111px"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Data_Label2" runat="server" Text="Miestai (U8a.txt)"></asp:Label>
            <br />
            <asp:TextBox ID="Data_TextBox2" runat="server" CssClass="auto-style1" TextMode="MultiLine" Width="346px" Height="111px"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Data_CalculateButton" runat="server" Text="Vykdyti" OnClick="Data_CalculateButton_Click" class="button" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="UploadStartingData_Button" runat="server" Text="Rašyti į App_Data" class="button2" OnClick="UploadStartingData_Button_Click" />
            <br />
            <br />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
            <br />
            <br />
        </div>
        <div>
            <asp:Label ID="Result_Label" runat="server" Text="Rezultatai"></asp:Label>
            <br />
            <asp:TextBox ID="Result_TextBox" runat="server" Width="346px" Height="111px" ReadOnly="True" TextMode="MultiLine"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="UploadResults_Button" runat="server" CssClass="button2" Text="Rašyti į App_Data" OnClick="UploadResults_Button_Click" />
        </div>
    </form>
</body>
</html>
