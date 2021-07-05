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
        
        // This is the entry point which will be called when the Add-in is loaded
        public static void AddinMain()
        {
            Logger.AddMessage(new LogMessage("AutoMarkUps Add-in loaded ... 2021.07.05  14:00 ", "AutoMarkUps Add-in"));

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
            //galleryAMU.HelpText = "For creating station equipment.";
            CommandBarHeader control = new CommandBarHeader("Marks");
            galleryAMU.GalleryControls.Add(control);
            btnAMU = new CommandBarButton("AutoMarkUp", "AutoMarkUp");
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

    }
}