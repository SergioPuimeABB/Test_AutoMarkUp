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


namespace Test_AutoMarkUp
{
    class main
    {
        // This is the entry point which will be called when the Add-in is loaded
        public static void AddinMain()
        {
            Logger.AddMessage(new LogMessage("AutoMarkUps Add-in loaded ... 2020.10.08  12:20 ", "AutoMarkUps Add-in"));

            AutoMarkUpsToolWindow();
        }

        public static void AutoMarkUpsToolWindow()
        {
            Project.UndoContext.BeginUndoStep("ToolWindow Creation");
            try
            {
                //((System.ComponentModel.ISupportInitialize)(pb_createBox)).BeginInit();

                ToolWindow tw = new ToolWindow("MyToolWindow_4");
                tw.Caption = "Reference ToolWindow.";
                UIEnvironment.Windows.AddDocked(tw, System.Windows.Forms.DockStyle.Top, UIEnvironment.Windows["ObjectBrowser"] as ToolWindow);

                Form1 form = new Form1();
                form.Show();

                PositionControl pos_control = new PositionControl
                {
                    ErrorProviderControl = null,
                    ExpressionErrorString = "Bad Expression",
                    LabelQuantity = ABB.Robotics.RobotStudio.BuiltinQuantity.Length,
                    LabelText = "Corner Point",
                    Location = new Point(8, 8),
                    Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
                    MaxValueErrorString = "Value exceeds maximum",
                    MinValueErrorString = "Value is below minimum",
                    Name = "pos_control",
                    NumTextBoxes = 3,
                    ReadOnly = false,
                    RefCoordSys = null,
                    ShowLabel = true,
                    Size = new Size(250, 34),
                    TabIndex = 1,
                    Text = "positionControl1",
                    VerticalLayout = false
                };
                tw.Control.Controls.Add(pos_control);

                Label lbl_prefix = new Label
                {
                    Text = "Prefix:",
                    Location = new Point(8, 56),
                    //Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
                    Size = new Size(37,34)
                };
                tw.Control.Controls.Add(lbl_prefix);


                TextBox tb_prefix = new TextBox
                {
                    Location = new Point(45,50),
                    Size = new Size (213,34)
                };
                tw.Control.Controls.Add(tb_prefix);


                Label lbl_suffix = new Label
                {
                    Text = "Suffix:",
                    Location = new Point(8, 88),
                    Size = new Size(37, 34)
                };
                tw.Control.Controls.Add(lbl_suffix);


                TextBox tb_suffix = new TextBox
                {
                    Location = new Point(45, 82),
                    Size = new Size(213, 34)
                };
                tw.Control.Controls.Add(tb_suffix);

                
                Label lbl_startnumber = new Label
                {
                    Text = "Start number:",
                    Location = new Point(8, 120),
                    Size = new Size(70, 34)
                };
                tw.Control.Controls.Add(lbl_startnumber);

                TextBox tb_startnumber = new TextBox
                {
                    Location = new Point(78, 114),
                    Size = new Size(40, 34),
                    TextAlign = System.Windows.Forms.HorizontalAlignment.Right,
                    RightToLeft = System.Windows.Forms.RightToLeft.Yes,
                  //  TextChanged += new EventHandler(textBox1_TextChanged);
            };
                tw.Control.Controls.Add(tb_startnumber);

                TextBox tb_test = new TextBox();
                tb_test.KeyPress += new KeyPressEventHandler(tb_test_TextChanged);
                tb_test.Location = new Point(78, 214);
                tb_test.Size = new Size(40, 34);
                tw.Control.Controls.Add(tb_test);



            }

            catch (Exception execption)
            {
                Project.UndoContext.CancelUndoStep(CancelUndoStepType.Rollback);
                Logger.AddMessage(new LogMessage(execption.Message.ToString()));
                throw;
            }

            finally
            {
                Project.UndoContext.EndUndoStep();
            }
        }

        private void tb_test_TextChanged(object sender, KeyPressEventArgs e)
         {
            Logger.AddMessage(new LogMessage(sender.ToString() , "AutoMarkUps Add-in"));

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        
        //private PositionControl pos_control;

    }
}