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
    public static class main
    {

        private static ToolWindow tw;

        public static PositionControl positionControlPos = new PositionControl();

        public static ListBox listBoxPointsList = new ListBox();

        public static List<Vector3> listMarks = new List<Vector3>();

        // This is the entry point which will be called when the Add-in is loaded
        public static void AddinMain()
        {
            Logger.AddMessage(new LogMessage("AutoMarkUps Add-in loaded ... 2021.05.03  10:58 ", "AutoMarkUps Add-in"));

            if (tw == null)
            {
                AutoMarkUpsToolWindow();
            }

        }

        private static void AutoMarkUpsToolWindow()
        {
            Project.UndoContext.BeginUndoStep("ToolWindow Creation");
            try
            {
                int tw_width = UIEnvironment.Windows["ObjectBrowser"].Control.Size.Width - 30;

                tw = new ToolWindow("MyToolWindow_4");
                tw.Caption = "Reference ToolWindow.";
                tw.PreferredSize = new Size(tw_width, 330);
                UIEnvironment.Windows.AddDocked(tw, System.Windows.Forms.DockStyle.Top, UIEnvironment.Windows["ObjectBrowser"] as ToolWindow);

                //Form1 form = new Form1();
                //form.Show();

                string start_num = "10";

                //PositionControl pos_control = new PositionControl();
                positionControlPos.ErrorProviderControl = null;
                positionControlPos.ExpressionErrorString = "Bad Expression";
                positionControlPos.LabelQuantity = ABB.Robotics.RobotStudio.BuiltinQuantity.Length;
                positionControlPos.LabelText = "Position";
                positionControlPos.Location = new Point(8, 8);
                positionControlPos.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                positionControlPos.MaxValueErrorString = "Value exceeds maximum";
                positionControlPos.MinValueErrorString = "Value is below minimum";
                positionControlPos.Name = "pos_control";
                positionControlPos.NumTextBoxes = 3;
                positionControlPos.ReadOnly = false;
                positionControlPos.RefCoordSys = null;
                positionControlPos.ShowLabel = true;
                positionControlPos.Size = new Size(tw_width + 10, 34);
                positionControlPos.TabIndex = 1;
                positionControlPos.Text = "positionControl1";
                positionControlPos.VerticalLayout = false;
                //pos_control.GotFocus += new EventHandler(PickTargets);
                //pos_control.MouseEnter += new EventHandler(PickTargets);
                //pos_control.Click += new EventHandler(PickTargets);
                tw.Control.Controls.Add(positionControlPos);

 
                Label lbl_prefix = new Label
                {
                    Text = "Prefix:",
                    Location = new Point(8, 56),
                    Size = new Size(37, 34)
                };
                //lbl_prefix.GotFocus += new EventHandler(PickTargets);
                tw.Control.Controls.Add(lbl_prefix);


                TextBox tb_prefix = new TextBox
                {
                    Location = new Point(45, 50),
                    Size = new Size(tw_width - 27, 34),
                    Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
                    Text = "p_"
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
                tb_suffix.GotFocus += new EventHandler(Release_PickTargets);
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

                Label labelNumIncrements = new Label
                {
                    Text = "Increments:",
                    Location = new Point(105, 120),
                    Size = new Size(65, 30)
                };
                tw.Control.Controls.Add(labelNumIncrements);

                RadioButton radioButton_1 = new RadioButton
                {
                    Location = new Point(170, 110),
                    Name = "1",
                    Size = new Size(37, 34),
                    Text = "1",
                    Checked = false
                };
                tw.Control.Controls.Add(radioButton_1);

                RadioButton radioButton_10 = new RadioButton
                {
                    Location = new Point(215, 110),
                    Name = "10",
                    Size = new Size(37, 34),
                    Text = "10",
                    Checked = true
                };
                tw.Control.Controls.Add(radioButton_10);

                
                RadioButton radioButton_100 = new RadioButton();
                radioButton_100.Location = new Point(260, 110);
                radioButton_100.Name = "100";
                radioButton_100.Size = new Size(45, 34);
                radioButton_100.Text = "100";
                radioButton_100.CheckedChanged += new System.EventHandler(radioButton_100_CheckedChanged);
                tw.Control.Controls.Add(radioButton_100);


                //ListView lb_points_list = new ListView
                //{
                //    Location = new Point(8, 152),
                //    Size = new Size(tw_width + 10, 100),
                //    Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
                //    Name = "Points List"

                //};
                //tw.Control.Controls.Add(lb_points_list);


                listBoxPointsList = new ListBox
                {
                    Location = new Point(8, 152),
                    Size = new Size(tw_width + 10, 100),
                    Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
                    Name = "Points List"

                };
                tw.Control.Controls.Add(listBoxPointsList);

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




        private static void radioButton_100_CheckedChanged(object sender, EventArgs e)
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

        // Don't work
        private static void Release_PickTargets(object sender, EventArgs e)
        {
            //Begin UndoStep
            Project.UndoContext.BeginUndoStep("MultipleTarget");
            try
            {
                Logger.AddMessage(new LogMessage("Released Picked"));
                GraphicPicker.GraphicPick -= new GraphicPickEventHandler(GraphicPicker_GraphicPick);

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
                AddPos(e.PickedPosition);
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

        private static void Release_GraphicPick(object sender, GraphicPickEventArgs e)
        {

            //Station station = Project.ActiveProject as Station;
            //string stepName = station.ActiveTask.GetValidRapidName("Target", "_", 10);

            //Begin UndoStep
            Project.UndoContext.BeginUndoStep("Release Pick Position");
            try
            {
                Logger.AddMessage(new LogMessage("Pick Released"));
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
            positionControlPos.SetFocus();
        }

        private static void AddPos(Vector3 position)
        {
            listBoxPointsList.SelectedIndices.Clear();
            listMarks.Add(position);
            listBoxPointsList.Items.Add(position);
            listBoxPointsList.SelectedIndex = listBoxPointsList.Items.Count - 1;
            positionControlPos.Value = position;
            positionControlPos.SetFocus();
            Logger.AddMessage(new LogMessage("ListMarks " + listMarks.Count.ToString()));
            
        }

    }
}