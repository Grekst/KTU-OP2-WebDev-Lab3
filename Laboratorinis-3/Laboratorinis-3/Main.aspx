<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Laboratorinis_3.Main" %>

<!DOCTYPE html>
<html lang="lt">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Kelionių maršrutai</title>
    <link rel="stylesheet" href="StyleSheet1.css" />
</head>
<body>
    <form id="form1" runat="server">


        <!-- Pradiniai duomenys -->
        <div class="card">
            <div class="card-title">Pradiniai duomenys</div>
            <div class="grid-2">

                <div>
                    <div class="field">
                        <label>Keliai — failo kelias (U8a.txt):</label>
                        <asp:TextBox ID="tb_FilePath1" runat="server" Text="~/Data/U8a.txt" />
                    </div>

                    <div class="upload-row">
                        <asp:FileUpload ID="fu_Roads" runat="server" />
                        <asp:Button ID="Btn_UploadRoads" runat="server" Text="Įkelti"
                            CssClass="btn btn-secondary btn-sm" OnClick="Btn_UploadRoads_Click" />
                    </div>

                    <div class="field" style="margin-top: 12px">
                        <label>Kelių duomenys (redaguojami):</label>
                        <asp:TextBox ID="tb_Roads" runat="server" TextMode="MultiLine" Rows="9"
                            placeholder="Miestas1, Miestas2, atstumas&#13;&#10;Vilnius, Kaunas, 102" />
                    </div>
                </div>

                <div>
                    <div class="field">
                        <label>Miestai — failo kelias (U8b.txt):</label>
                        <asp:TextBox ID="tb_FilePath2" runat="server" Text="~/Data/U8b.txt" />
                    </div>

                    <div class="upload-row">
                        <asp:FileUpload ID="fu_Cities" runat="server" />
                        <asp:Button ID="Btn_UploadCities" runat="server" Text="Įkelti"
                            CssClass="btn btn-secondary btn-sm" OnClick="Btn_UploadCities_Click" />
                    </div>

                    <div class="field" style="margin-top: 12px">
                        <label>Miestų duomenys (redaguojami):</label>
                        <asp:TextBox ID="tb_Cities" runat="server" TextMode="MultiLine" Rows="9"
                            placeholder="Miestas, gyventojų_skaičius&#13;&#10;Vilnius, 580000" />
                    </div>
                </div>

            </div>
            <div class="btn-row">
                <asp:Button ID="Btn_LoadByPath" runat="server" Text="Įkelti pagal kelią"
                    CssClass="btn btn-secondary" OnClick="Btn_LoadByPath_Click" />
            </div>
        </div>

        <!-- Parametrai -->
        <div class="card">
            <div class="card-title">Parametrai</div>
            <div class="grid-4">
                <div class="field">
                    <label>Pradinis miestas:</label>
                    <asp:TextBox ID="tb_StartCity" runat="server" placeholder="pvz. Kaunas" />
                </div>

                <div class="field">
                    <label>Maks. populiacija:</label>
                    <asp:TextBox ID="tb_MaxPopulation" runat="server" placeholder="pvz. 500000" />
                </div>

                <div class="field">
                    <label>Min. atstumas (km):</label>
                    <asp:TextBox ID="tb_MinDistance" runat="server" placeholder="pvz. 100" />
                </div>

                <div class="field">
                    <label>Nelankytinas miestas:</label>
                    <asp:TextBox ID="tb_AvoidCity" runat="server" placeholder="pvz. Alytus" />
                </div>

            </div>
            <div class="btn-row">
                <asp:Button ID="Btn_Calculate" runat="server" Text="Ieškoti maršrutų"
                    CssClass="btn btn-primary" OnClick="Btn_Calculate_Click" />

                <asp:Button ID="Btn_SaveInitial" runat="server" Text="Išsaugoti Pradiniai.txt"
                    CssClass="btn btn-success" OnClick="Btn_SaveInitial_Click" />
            </div>
            <asp:Label ID="lbl_Message" runat="server" />
        </div>

        <!-- Rezultatai -->
        <div class="card">
            <div class="card-title">Rezultatai</div>
            <div class="result-wrap">
                <asp:Literal ID="lit_Results" runat="server">
        <div class="result-placeholder">
          Įveskite parametrus ir spauskite „Ieškoti maršrutų"
        </div>
      </asp:Literal>
            </div>
        </div>

    </form>
</body>
</html>
