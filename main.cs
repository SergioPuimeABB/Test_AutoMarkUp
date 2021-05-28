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
    public class main
    {

        private static ToolWindow tw;

        public static PositionControl positionControlPos = new PositionControl();

        public static ListBox listBoxPointsList = new ListBox();

        public static TextBox tb_prefix = new TextBox();

        public static TextBox tb_suffix = new TextBox();

        public static TextBox tb_startnumber = new TextBox();

        public static RadioButton radioButton_1 = new RadioButton();

        public static RadioButton radioButton_10 = new RadioButton();

        public static RadioButton radioButton_100 = new RadioButton();

        public static ObjectSelectionControl IncrementSteps = new ObjectSelectionControl();

        public static NumericTextBox NumericTextBoxStartWith = new NumericTextBox();

        public static int MarkNumber = 1;


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
                    Text = "Name Prefix",
                    Location = new Point(8, 50),
                    Size = new Size(200, 14)
                };
                //lbl_prefix.GotFocus += new EventHandler(PickTargets);
                tw.Control.Controls.Add(lbl_prefix);


                tb_prefix.Location = new Point(8, 65);
                tb_prefix.Size = new Size(tw_width + 10, 34);
                tb_prefix.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                tb_prefix.Text = "p_";
                tw.Control.Controls.Add(tb_prefix);



                Label lbl_suffix = new Label
                {
                    Text = "Name Suffix",
                    Location = new Point(8, 95),
                    Size = new Size(200, 14)
                };
                tw.Control.Controls.Add(lbl_suffix);

                tb_suffix.Location = new Point(8, 110);
                tb_suffix.Size = new Size(tw_width + 10, 34);
                tb_suffix.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                tb_suffix.GotFocus += new EventHandler(Release_PickTargets);
                tw.Control.Controls.Add(tb_suffix);


                Label labelNumIncrements = new Label
                {
                    Text = "Increment",
                    Location = new Point(8, 140),
                    Size = new Size(80, 14)
                };
                //tw.Control.Controls.Add(labelNumIncrements);


                IncrementSteps.Location = new Point(8, 140);
                IncrementSteps.Size = new Size(70, 35);
                IncrementSteps.AutoSelectNextControl = true;
                IncrementSteps.LabelText = "Increment";
                //IncrementSteps.
                tw.Control.Controls.Add(IncrementSteps);

                //Label lbl_startnumber = new Label
                //{
                //    Text = "Start with:",
                //    Location = new Point(100, 140),
                //    Size = new Size(80, 14)
                //};
                //tw.Control.Controls.Add(lbl_startnumber);

                NumericTextBoxStartWith.Location = new Point(100, 140);
                NumericTextBoxStartWith.Size = new Size(70, 35);
                NumericTextBoxStartWith.LabelText = "Start with:";
                tw.Control.Controls.Add(NumericTextBoxStartWith);








                ////TextBox tb_startnumber = new TextBox();
                //tb_startnumber.Location = new Point(55, 210);
                //tb_startnumber.Size = new Size(40, 34);
                //tb_startnumber.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                //tb_startnumber.TextAlign = HorizontalAlignment.Right;
                //tb_startnumber.RightToLeft = RightToLeft.Yes;
                //tb_startnumber.Text = start_num;
                //tb_startnumber.KeyPress += new KeyPressEventHandler(tb_test_KeyPress);
                ////
                //tb_startnumber.GotFocus += new EventHandler(PickTargets);
                ////
                //tw.Control.Controls.Add(tb_startnumber);



                //RadioButton radioButton_1 = new RadioButton
                //{
                //    Location = new Point(170, 110),
                //    Name = "1",
                //    Size = new Size(37, 34),
                //    Text = "1",
                //    Checked = false
                //};
                //tw.Control.Controls.Add(radioButton_1);

                //RadioButton radioButton_10 = new RadioButton
                //{
                //    Location = new Point(215, 110),
                //    Name = "10",
                //    Size = new Size(37, 34),
                //    Text = "10",
                //    Checked = true
                //};
                //tw.Control.Controls.Add(radioButton_10);


                //RadioButton radioButton_100 = new RadioButton();
                //radioButton_100.Location = new Point(260, 110);
                //radioButton_100.Name = "100";
                //radioButton_100.Size = new Size(45, 34);
                //radioButton_100.Text = "100";
                //radioButton_100.CheckedChanged += new System.EventHandler(radioButton_100_CheckedChanged);
                //tw.Control.Controls.Add(radioButton_100);


                //ListView lb_points_list = new ListView
                //{
                //    Location = new Point(8, 152),
                //    Size = new Size(tw_width + 10, 100),
                //    Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
                //    Name = "Points List"

                //};
                //tw.Control.Controls.Add(lb_points_list);


                //listBoxPointsList = new ListBox
                //{
                //    Location = new Point(8, 152),
                //    Size = new Size(tw_width + 10, 100),
                //    Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
                //    Name = "Points List"

                //};
                //tw.Control.Controls.Add(listBoxPointsList);

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
            //Logger.AddMessage(new LogMessage(position.ToString()));
            positionControlPos.SetFocus();
        }

        private static void AddPos(Vector3 position)
        {
            CreateMarkUp(position);
            listBoxPointsList.SelectedIndices.Clear();
            listMarks.Add(position);
            //listBoxPointsList.Items.Add(position);
            listBoxPointsList.Items.Add("MarkUp " + listMarks.Count.ToString());
            listBoxPointsList.SelectedIndex = listBoxPointsList.Items.Count - 1;
            positionControlPos.Value = position;
            positionControlPos.SetFocus();
            Logger.AddMessage(new LogMessage("ListMarks " + listMarks.Count.ToString()));
        }

        private static int Increments()
        {
            int inc = 1;

            if (radioButton_1.Checked)
            {
                inc = 1;
            }
            if (radioButton_10.Checked)
            {
                inc = 10;
            }
            if (radioButton_100.Checked)
            {
                inc = 100;
            }

            return inc;

        }

        public static void CreateMarkUp(Vector3 position)
        {
            Station station = Project.ActiveProject as Station;
            Markup markupWText = new Markup();
            markupWText.Transform.Translation = position;
            markupWText.Text = GenerateMarkName.GenerateName();
            station.Markups.Add(markupWText);
            MarkNumber = MarkNumber+1 *Increments();

            Logger.AddMessage(new LogMessage("MarkText : " + GenerateMarkName.GenerateName()));
            Logger.AddMessage(new LogMessage("MarkNumber : " + MarkNumber));
            Logger.AddMessage(new LogMessage("Increments : " + Increments()));
        }



    }
}