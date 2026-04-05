using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Laboratorinis_2
{
    public partial class Forma1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Main button for calculating path
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Data_CalculateButton_Click(object sender, EventArgs e)
        {
            LListRoad roadList = InOut.ReadRoad(Data_TextBox1.Text);
            LListCity cityList = InOut.ReadCity(Data_TextBox2.Text);

            string start = StartCity_TextBox.Text.Trim(); // Example: Kaunas
            int maxP = int.Parse(MaxPopulation_DataTextBox.Text); //  Example: 500000
            int minD = int.Parse(MinDistance_DataTextBox.Text); // Example: 50
            string avoid = AvoidCity_TextBox.Text.Trim(); // Example: Alytus

            LListRoute foundRoutes = TaskUtils.FindAllRoutes(cityList, roadList, start, maxP, minD, avoid);
            foundRoutes.Sort();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Rasti maršrutai:");

            foundRoutes.Begin();
            if (!foundRoutes.Exist())
            {
                sb.AppendLine("Maršrutų nerasta. Patikrinkite pradinį miestą ir filtrus.");
            }
            else
            {
                for (; foundRoutes.Exist(); foundRoutes.Next())
                {
                    sb.AppendLine(foundRoutes.Get().ToString());
                }
            }
            Result_TextBox.Text = sb.ToString();
        }

        /// <summary>
        /// Pastes data from filepath to textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Data_UploadFromInternal_Click(object sender, EventArgs e)
        {
            string fileName1 = Server.MapPath("~/Data/U8a.txt");
            string fileName2 = Server.MapPath("~/Data/U8b.txt");

            Data_TextBox1.Text = InOut.ReadFileData(fileName1);
            Data_TextBox2.Text = InOut.ReadFileData(fileName2);
        }

        /// <summary>
        /// Writes starting data to filepath
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UploadStartingData_Button_Click(object sender, EventArgs e)
        {
            string path = Server.MapPath("~/Data/PradiniaiDuomenys.txt");
            string contents_A = Data_TextBox1.Text;
            string contents_B = Data_TextBox2.Text;

            InOut.WriteContentsToFile(path, contents_A + '\n' + new string('=', 32) + '\n' + contents_B);
        }

        /// <summary>
        /// Writes results to filepath
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UploadResults_Button_Click(object sender, EventArgs e)
        {
            string path = Server.MapPath("~/Data/Rezultatai.txt");
            string contents = Result_TextBox.Text;

            InOut.WriteContentsToFile(path, contents);
        }
    }
}