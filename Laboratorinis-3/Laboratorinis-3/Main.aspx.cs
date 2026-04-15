using Laboratorinis_3;
using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Laboratorinis_3
{
    public partial class Main : Page
    {
        protected void Page_Load(object sender, EventArgs e) { }

        /// <summary>
        /// File upload to textbox from path
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Btn_LoadByPath_Click(object sender, EventArgs e)
        {
            InOut.ClearMessage(lbl_Message);
            try
            {
                string path1 = Server.MapPath(tb_FilePath1.Text.Trim());
                string path2 = Server.MapPath(tb_FilePath2.Text.Trim());
                tb_Roads.Text = InOut.ReadFileData(path1);
                tb_Cities.Text = InOut.ReadFileData(path2);
            }
            catch (Exception ex)
            {
                InOut.ShowError("Klaida skaitant failą: " + ex.Message, lbl_Message);
            }
        }

        // ── failo įkėlimas per FileUpload kontrolį ────────────────────
        protected void Btn_UploadRoads_Click(object sender, EventArgs e)
        {
            UploadToTextBox(fu_Roads, tb_Roads);
        }

        protected void Btn_UploadCities_Click(object sender, EventArgs e)
        {
            UploadToTextBox(fu_Cities, tb_Cities);
        }

        /// <summary>
        /// File upload to textbox from FileUpload component
        /// </summary>
        /// <param name="file"></param>
        /// <param name="textBox"></param>
        private void UploadToTextBox(FileUpload file, TextBox textBox)
        {
            InOut.ClearMessage(lbl_Message);
            if (!file.HasFile)
            {
                InOut.ShowError("Failas nepasirinktas.", lbl_Message);
                return;
            }
            using (StreamReader sr = new StreamReader(file.FileContent))
                textBox.Text = sr.ReadToEnd();
        }

        /// <summary>
        /// Calculation method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Btn_Calculate_Click(object sender, EventArgs e)
        {
            InOut.ClearMessage(lbl_Message);

            LList<City> cities = InOut.ReadCities(tb_Cities);
            LList<Road> roads = InOut.ReadRoads(tb_Roads);
            string startCity = InOut.ReadText(tb_StartCity);
            int maxPop = InOut.ReadInt(tb_MaxPopulation);
            int minDist = InOut.ReadInt(tb_MinDistance);
            string avoid = InOut.ReadText(tb_AvoidCity);

            if (string.IsNullOrWhiteSpace(startCity) || maxPop == 0)
            {
                InOut.ShowError("Užpildykite pradinio miesto ir maks. populiacijos laukus.", lbl_Message);
                return;
            }

            LList<Route> routes = TaskUtils.FindAllRoutes(cities, roads, startCity, maxPop, minDist, avoid);
            TaskUtils.SortRoutes(routes);

            InOut.DisplayRoutes(routes, lit_Results);
        }

        /// <summary>
        /// Initial data upload button logic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Btn_SaveInitial_Click(object sender, EventArgs e)
        {
            InOut.ClearMessage(lbl_Message);
            try
            {
                LList<City> cities = InOut.ReadCities(tb_Cities);
                LList<Road> roads = InOut.ReadRoads(tb_Roads);

                string filePath = Server.MapPath("~/Data/Pradiniai.txt");
                InOut.SaveInitialData(
                    roads, cities,
                    InOut.ReadText(tb_StartCity),
                    InOut.ReadInt(tb_MaxPopulation),
                    InOut.ReadInt(tb_MinDistance),
                    InOut.ReadText(tb_AvoidCity),
                    filePath);

                InOut.ShowSuccess("Išsaugota: " + filePath, lbl_Message);
            }
            catch (Exception ex)
            {
                InOut.ShowError("Klaida: " + ex.Message, lbl_Message);
            }
        }
    }
}