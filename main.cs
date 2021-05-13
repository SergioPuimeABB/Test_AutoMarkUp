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
    public static class main
    {

        public static PositionControl pos_control = new PositionControl();

        // This is the entry point which will be called when the Add-in is loaded
        public static void AddinMain()
        {
            Logger.AddMessage(new LogMessage("AutoMarkUps Add-in loaded ... 2021.05.03  10:58 ", "AutoMarkUps Add-in"));

            AutoMarkUpsToolWindow();

        }

        private static void AutoMarkUpsToolWindow()
        {
            Project.UndoContext.BeginUndoStep("ToolWindow Creation");
            try
            {
                int tw_width = UIEnvironment.Windows["ObjectBrowser"].Control.Size.Width - 30;

                ToolWindow tw = new ToolWindow("MyToolWindow_4");
                tw.Caption = "Reference ToolWindow.";
                tw.PreferredSize = new Size(tw_width, 330);
                UIEnvironment.Windows.AddDocked(tw, System.Windows.Forms.DockStyle.Top, UIEnvironment.Windows["ObjectBrowser"] as ToolWindow);

                //Form1 form = new Form1();
                //form.Show();

                string start_num = "10";

                //PositionControl pos_control = new PositionControl();
                pos_control.ErrorProviderControl = null;
                pos_control.ExpressionErrorString = "Bad Expression";
                pos_control.LabelQuantity = ABB.Robotics.RobotStudio.BuiltinQuantity.Length;
                pos_control.LabelText = "Position";
                pos_control.Location = new Point(8, 8);
                pos_control.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                pos_control.MaxValueErrorString = "Value exceeds maximum";
                pos_control.MinValueErrorString = "Value is below minimum";
                pos_control.Name = "pos_control";
                pos_control.NumTextBoxes = 3;
                pos_control.ReadOnly = false;
                pos_control.RefCoordSys = null;
                pos_control.ShowLabel = true;
                pos_control.Size = new Size(tw_width + 10, 34);
                pos_control.TabIndex = 1;
                pos_control.Text = "positionControl1";
                pos_control.VerticalLayout = false;
                //pos_control.GotFocus += new EventHandler(PickTargets);
                //pos_control.MouseEnter += new EventHandler(PickTargets);
                //pos_control.Click += new EventHandler(PickTargets);
                tw.Control.Controls.Add(pos_control);

 
                Label lbl_prefix = new Label
                {
                    Text = "Prefix:",
                    Location = new Point(8, 56),
                    Size = new Size(37, 34)
                };
                lbl_prefix.GotFocus += new EventHandler(PickTargets);
                tw.Control.Controls.Add(lbl_prefix);


                TextBox tb_prefix = new TextBox
                {
                    Location = new Point(45, 50),
                    Size = new Size(tw_width - 27, 34),
                    Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
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
                    Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
                    Size = new Size(tw_width - 27, 34)
                };
                tw.Control.Controls.Add(tb_suffix);


                Label lbl_startnumber = new Label
                {
                    Text = "Start n.:",
                    Location = new Point(8, 120),
                    Size = new Size(45, 30)
                };
                tw.Control.Controls.Add(lbl_startnumber);

                
                TextBox tb_startnumber = new TextBox();
                tb_startnumber.Location = new Point(55, 114);
                tb_startnumber.Size = new Size(40, 34);
                tb_startnumber.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                tb_startnumber.TextAlign = HorizontalAlignment.Right;
                tb_startnumber.RightToLeft = RightToLeft.Yes;
                tb_startnumber.Text = start_num;
                tb_startnumber.KeyPress += new KeyPressEventHandler(tb_test_KeyPress);
                //
                tb_startnumber.GotFocus += new EventHandler(PickTargets);
                //
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

                
                RadioButton rb_100 = new RadioButton();
                rb_100.Location = new Point(150, 110);
                rb_100.Name = "100";
                rb_100.Size = new Size(45, 34);
                rb_100.Text = "100";
                rb_100.CheckedChanged += new System.EventHandler(rb_100_CheckedChanged);
                tw.Control.Controls.Add(rb_100);

                ListView lb_points_list = new ListView
                {
                    Location = new Point(8, 152),
                    Size = new Size(tw_width + 10, 100),
                    Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
                    Name = "Points List"

                };
                tw.Control.Controls.Add(lb_points_list);

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

        public static void est()
        {
            string tmp1 = "tmp1";
        }




        private static void rb_100_CheckedChanged(object sender, EventArgs e)
        {
        }

        private static void pos_control_TextChanged(object sender, EventArgs e)
        {
        }


        private static void PickTargets(object sender, EventArgs e)
        {
            //Begin UndoStep
            Project.UndoContext.BeginUndoStep("MultipleTarget");
            try
            {
                Logger.AddMessage(new LogMessage("Picked"));
                //Initialize GraphicPicker
                GraphicPicker.GraphicPick += new GraphicPickEventHandler(GraphicPicker_GraphicPick);
            }
            catch (Exception ex)
            {
                Project.UndoContext.CancelUndoStep(CancelUndoStepType.Rollback);
                Logger.AddMessage(new LogMessage(ex.Message.ToString()));
            }
            finally
            {
                //End UndoStep
                Project.UndoContext.EndUndoStep();

            }
        }

        private static void GraphicPicker_GraphicPick(object sender, GraphicPickEventArgs e)
        {

            //Station station = Project.ActiveProject as Station;
            //string stepName = station.ActiveTask.GetValidRapidName("Target", "_", 10);

            //Begin UndoStep
            Project.UndoContext.BeginUndoStep("Pick Position");
            try
            {
                GetPos(e.PickedPosition);
            }
            catch (Exception exception)
            {
                Project.UndoContext.CancelUndoStep(CancelUndoStepType.Rollback);
                Logger.AddMessage(new LogMessage(exception.Message.ToString()));
            }
            finally
            {
                //End UndoStep
                Project.UndoContext.EndUndoStep();
            }
        }


        public static void GetPos(Vector3 position)
        {
            Logger.AddMessage(new LogMessage(position.ToString()));
            pos_control.SetFocus();

        }

    }
}