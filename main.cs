using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ABB.Robotics.Math;
using ABB.Robotics.RobotStudio;
using ABB.Robotics.RobotStudio.Environment;
using ABB.Robotics.RobotStudio.Stations;
using ABB.Robotics.RobotStudio.Stations.Forms;

using Test_AutoMarkUp;


namespace Test_AutoMarkUp
{
    public class AutoMarkUp
    {
        private static RibbonGroup rgAMU;

        private static CommandBarGalleryPopup galleryAMU;

        private static CommandBarButton btnAMU;

        public static PositionControl PositionControlPos = new PositionControl();

        public static ListBox ListBoxPointsList = new ListBox();

        public static TextBox TextBoxPrefix = new TextBox();

        public static TextBox TextBoxSuffix = new TextBox();

        public static TextBox TextBoxStartnumber = new TextBox();

        public static ObjectSelectionControl ObjectSelectionControlIncrementSteps = new ObjectSelectionControl();

        public static ComboBox ComboboxIncrementSteps = new ComboBox();

        public static NumericUpDown NumericUpDownStartWith = new NumericUpDown();

        public static Label LabelResultname = new Label();

        public static int MarkNumber = 0;

        public static bool FirstClick = true;

        public static List<Vector3> listMarks = new List<Vector3>();

        // This is the entry point which will be called when the Add-in is loaded
        public static void AddinMain()
        {
            Logger.AddMessage(new LogMessage("AutoMarkUps Add-in loaded ... 2021.05.03  10:58 ", "AutoMarkUps Add-in"));

            if (rgAMU == null)
            {
                AddRibbonGroup();
            }

        }

        public static void AddRibbonGroup()
        {
            rgAMU = new RibbonGroup("rgAMU", "AMU");
            galleryAMU = new CommandBarGalleryPopup("AutoMarkUp Tool");
            galleryAMU.NumberOfColumns = 6;
            galleryAMU.GalleryTextPosition = GalleryTextPosition.Below;
            galleryAMU.GalleryItemSize = new Size(96, 96);
            //galleryEB.Image = Resources.EquipmentBuilder;
            galleryAMU.HelpText = "For creating station equipment.";
            CommandBarHeader control = new CommandBarHeader("Marks & Walls");
            galleryAMU.GalleryControls.Add(control);
            btnAMU = new CommandBarButton("AutoMarkUp", "Net");
            //btnFB.Image = Resources.NetFence_96x96;
            galleryAMU.GalleryControls.Add(btnAMU);

            UIEnvironment.RibbonTabs["Modeling"].Groups[0].Controls.Insert(8, galleryAMU);

            btnAMU.UpdateCommandUI += btnAMU_UpdateCommandUI;
            btnAMU.ExecuteCommand += btnAMU_ExecuteCommand;
            ToolControlManager.RegisterToolCommand("AutoMarkUp", ToolControlManager.FindToolHost("ElementBrowser"));
            
        }

        private static void btnAMU_UpdateCommandUI(object sender, UpdateCommandUIEventArgs e)
        {
            e.Enabled = Project.ActiveProject is Station;
        }

        private static void btnAMU_ExecuteCommand(object sender, ExecuteCommandEventArgs e)
        {
            ToolControlManager.ShowTool(typeof(frmAutoMarkUpBuilder), e.Id);
        }

        //private static void CloseTW(object sender, EventArgs e)
        //{
        //    Logger.AddMessage(new LogMessage("Closed Window"));
        //    GraphicPicker.GraphicPick -= new GraphicPickEventHandler(GraphicPicker_GraphicPick);
        //}


        //public static void CreateMarkUp(Vector3 position)
        //{
        //    Station station = Project.ActiveProject as Station;
        //    Markup markupWText = new Markup();
        //    markupWText.Transform.Translation = position;
        //    markupWText.Text = GenerateName();
        //    markupWText.Name = markupWText.Text;
        //    station.Markups.Add(markupWText);
        //    MarkNumber += 1 *Increments();
        //    //return;
        //    //Logger.AddMessage(new LogMessage("MarkNumber : " + MarkNumber));
        //    //Logger.AddMessage(new LogMessage("Increments : " + Increments()));
        //}

        //public static string GenerateName()
        //{
        //    string pref = TextBoxPrefix.Text;
        //    int name = Int16.Parse(NumericUpDownStartWith.Text);
        //    string suff = TextBoxSuffix.Text;

        //    int resultname = name + MarkNumber;

        //    string generatedName = pref + resultname + suff;


        //    return generatedName;
        //}


    }
}