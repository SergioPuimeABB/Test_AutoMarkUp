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

                int tw_width = UIEnvironment.Windows["ObjectBrowser"].Control.Size.Width - 30;

                ToolWindow tw = new ToolWindow("MyToolWindow_4");
                tw.Caption = "Reference ToolWindow.";
                tw.PreferredSize = new Size(tw_width, 330);
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
                    Size = new Size(tw_width+10, 34),
                    TabIndex = 1,
                    Text = "positionControl1",
                    VerticalLayout = false
                };
                tw.Control.Controls.Add(pos_control);

                Label lbl_prefix = new Label
                {
                    Text = "Prefix:",
                    Location = new Point(8, 56),
                    Size = new Size(37,34)
                };
                tw.Control.Controls.Add(lbl_prefix);


                TextBox tb_prefix = new TextBox
                {
                    Location = new Point(45,50),
                    Size = new Size (185,34),
                    Text = "pt_"
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
                    Size = new Size(185, 34)
                };
                tw.Control.Controls.Add(tb_suffix);

                
                Label lbl_startnumber = new Label
                {
                    Text = "Start n.:",
                    Location = new Point(8, 120),
                    Size = new Size(45, 34)
                };
                tw.Control.Controls.Add(lbl_startnumber);

                TextBox tb_startnumber = new TextBox();
                tb_startnumber.Location = new Point(55, 114);
                tb_startnumber.Size = new Size(40, 34);
                tb_startnumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
                tb_startnumber.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
                tb_startnumber.KeyPress += new KeyPressEventHandler(tb_test_KeyPress);
                tb_startnumber.Text = "10";
                tw.Control.Controls.Add(tb_startnumber);

                RadioButton rb_10 = new RadioButton
                {
                    Location = new Point(105, 110),
                    Name = "10",
                    Size = new Size(37, 34),
                    Text = "10",
                    Checked = true
                };
                tw.Control.Controls.Add(rb_10);

                RadioButton rb_100 = new RadioButton
                {
                    Location = new Point(150, 110),
                    Name = "100",
                    Size = new Size(45, 34),
                    Text = "100"
                };
                tw.Control.Controls.Add(rb_100);

                TextBox tb_points_list = new TextBox
                {
                    Location = new Point(8, 150),
                    Multiline = true,
                    Name = "Points List",
                    Size = new Size(tw_width, 150)
                };
                tw.Control.Controls.Add(tb_points_list);


                //RadioButton rb_1000 = new RadioButton
                //{
                //    Location = new Point(200, 110),
                //    Name = "1000",
                //    Size = new Size(55, 34),
                //    Text = "1000"
                //};
                //tw.Control.Controls.Add(rb_1000);

                //Label lbl_lbl10 = new Label
                //{
                //    Text = "10",
                //    Location = new Point(150, 114),
                //    Size = new Size(37, 34)
                //};
                //tw.Control.Controls.Add(lbl_lbl10);

                //TextBox tb_test = new TextBox();
                ////tb_test.KeyPress += new KeyPressEventHandler(tb_test_TextChanged);
                //tb_test.KeyPress += new System.Windows.Forms.KeyPressEventHandler(tb_test_KeyPress);
                //tb_test.Location = new Point(78, 214);
                //tb_test.Size = new Size(40, 34);
                //tw.Control.Controls.Add(tb_test);



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


        private static void tb_test_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        //private void tb_test_TextChanged(object sender, KeyPressEventArgs e)
        // {
        //    Logger.AddMessage(new LogMessage(sender.ToString() , "AutoMarkUps Add-in"));

        //    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        //(e.KeyChar != '.'))
        //    {
        //        e.Handled = true;
        //    }

        //    //// only allow one decimal point
        //    //if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
        //    //{
        //    //    e.Handled = true;
        //    //}
        //}
        

    }
}