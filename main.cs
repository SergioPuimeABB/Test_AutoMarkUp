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

        public static PositionControl PositionControlPos = new PositionControl();

        public static ListBox ListBoxPointsList = new ListBox();

        public static TextBox TextBoxPrefix = new TextBox();

        public static TextBox TextBoxSuffix = new TextBox();

        public static TextBox TextBoxStartnumber = new TextBox();

        public static RadioButton RadioButton_1 = new RadioButton();

        public static RadioButton RadioButton_10 = new RadioButton();

        public static RadioButton RadioButton_100 = new RadioButton();

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
                tw.Closed += new EventHandler(CloseTW); // ?????????????????????
                UIEnvironment.Windows.AddDocked(tw, System.Windows.Forms.DockStyle.Top, UIEnvironment.Windows["ObjectBrowser"] as ToolWindow);

                //string start_num = "10";

                PositionControlPos.ErrorProviderControl = null;
                PositionControlPos.ExpressionErrorString = "Bad Expression";
                PositionControlPos.LabelQuantity = ABB.Robotics.RobotStudio.BuiltinQuantity.Length;
                PositionControlPos.LabelText = "Position";
                PositionControlPos.Location = new Point(8, 8);
                PositionControlPos.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                PositionControlPos.MaxValueErrorString = "Value exceeds maximum";
                PositionControlPos.MinValueErrorString = "Value is below minimum";
                PositionControlPos.Name = "pos_control";
                PositionControlPos.NumTextBoxes = 3;
                PositionControlPos.ReadOnly = false;
                PositionControlPos.RefCoordSys = null;
                PositionControlPos.ShowLabel = true;
                PositionControlPos.Size = new Size(tw_width + 10, 34);
                PositionControlPos.TabIndex = 1;
                PositionControlPos.Text = "positionControl1";
                PositionControlPos.VerticalLayout = false;
                //PositionControlPos.GotFocus += new EventHandler(PickTargets);
                //PositionControlPos.Leave += new EventHandler(Release_PickTargets);
                //PositionControlPos.Click += new GraphicPickEventHandler(GraphicPicker_GraphicPick);
                //PositionControlPos.Enter += new EventHandler(PickTargets);
                PositionControlPos.Pick += new EventHandler(PickTargets);
                //PositionControlPos.Pick += new EventHandler(Release_PickTargets);
                //PositionControlPos.MouseEnter += new EventHandler(PickTargets);
                //pos_control.Click += new EventHandler(PickTargets);
                tw.Control.Controls.Add(PositionControlPos);


                Label lbl_prefix = new Label
                {
                    Text = "Name Prefix",
                    Location = new Point(8, 50),
                    Size = new Size(200, 14)
                };
                tw.Control.Controls.Add(lbl_prefix);


                TextBoxPrefix.Location = new Point(8, 65);
                TextBoxPrefix.Size = new Size(tw_width + 10, 34);
                TextBoxPrefix.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                TextBoxPrefix.Text = "p_";
                TextBoxPrefix.TextChanged += new EventHandler(TextValueChanged);
                tw.Control.Controls.Add(TextBoxPrefix);


                Label lbl_suffix = new Label
                {
                    Text = "Name Suffix",
                    Location = new Point(8, 95),
                    Size = new Size(200, 14)
                };
                tw.Control.Controls.Add(lbl_suffix);


                TextBoxSuffix.Location = new Point(8, 110);
                TextBoxSuffix.Size = new Size(tw_width + 10, 34);
                TextBoxSuffix.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                //tb_suffix.GotFocus += new EventHandler(Release_PickTargets);
                TextBoxSuffix.TextChanged += new EventHandler(TextValueChanged);
                tw.Control.Controls.Add(TextBoxSuffix);


                Label labelNumIncrements = new Label
                {
                    Text = "Increment",
                    Location = new Point(8, 140),
                    Size = new Size(80, 14)
                };
                tw.Control.Controls.Add(labelNumIncrements);

                ComboboxIncrementSteps.Location = new Point(8, 155);
                ComboboxIncrementSteps.Size = new Size(70, 45);
                ComboboxIncrementSteps.DropDownWidth = 70;
                ComboboxIncrementSteps.Items.AddRange(new object[] 
                        {"1",
                        "10",
                        "100",
                        "1000"});
                ComboboxIncrementSteps.SelectedItem = "10";
                ComboboxIncrementSteps.SelectedIndexChanged += ComboboxIncrementSteps_SelectedIndexChanged;
                ComboboxIncrementSteps.SelectedIndexChanged += new EventHandler(TextValueChanged);
                tw.Control.Controls.Add(ComboboxIncrementSteps);

                Label lbl_startnumber = new Label
                {
                    Text = "Start number",
                    Location = new Point(100, 140),
                    Size = new Size(80, 14)
                };
                tw.Control.Controls.Add(lbl_startnumber);

                NumericUpDownStartWith.Location = new Point(100, 155);
                NumericUpDownStartWith.Size = new Size(70, 55);
                NumericUpDownStartWith.Minimum = 1;
                NumericUpDownStartWith.Maximum = 1000;
                NumericUpDownStartWith.Increment = 1;
                NumericUpDownStartWith.DecimalPlaces = 0;
                NumericUpDownStartWith.Value = 10;
                NumericUpDownStartWith.DecimalPlaces = 0;
                NumericUpDownStartWith.ValueChanged += new EventHandler(TextValueChanged);
                tw.Control.Controls.Add(NumericUpDownStartWith);

                Label lbl_firstlabelname = new Label
                {
                    Text = "First label name: ",
                    Location = new Point(8, 200),
                    Size = new Size(85, 14)
                };
                tw.Control.Controls.Add(lbl_firstlabelname);

                tw.Control.Controls.Add(lbl_startnumber);
                LabelResultname.Location = new Point(90, 200);
                LabelResultname.Size = new Size(80, 14);
                LabelResultname.Text = TextBoxPrefix.Text + NumericUpDownStartWith.Value + TextBoxSuffix.Text;
                tw.Control.Controls.Add(LabelResultname);

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


        //private static void tb_test_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        //}

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

        private static void PickTargets(object sender, EventArgs e)
        {

            Logger.AddMessage(new LogMessage("PickTargets"));
            if (FirstClick)
            {
                GraphicPicker.GraphicPick += new GraphicPickEventHandler(GraphicPicker_GraphicPick);
            }
            PositionControlPos.SetFocus();

            ////Begin UndoStep
            //Project.UndoContext.BeginUndoStep("MultipleTarget");
            //try
            //{

            //    Logger.AddMessage(new LogMessage("PickTargets"));
            //    if (FirstClick)
            //    {
            //        GraphicPicker.GraphicPick += new GraphicPickEventHandler(GraphicPicker_GraphicPick);
            //    }
            //    PositionControlPos.SetFocus();
            //}
            //catch (Exception ex)
            //{
            //    Project.UndoContext.CancelUndoStep(CancelUndoStepType.Rollback);
            //    Logger.AddMessage(new LogMessage(ex.Message.ToString()));
            //}
            //finally
            //{
            //    //End UndoStep
            //    Project.UndoContext.EndUndoStep();

            //}
        }

        private static void CloseTW(object sender, EventArgs e)
        {
                Logger.AddMessage(new LogMessage("Closed Window"));
                GraphicPicker.GraphicPick -= new GraphicPickEventHandler(GraphicPicker_GraphicPick);
        }


        //private static void Release_PickTargets(object sender, EventArgs e)
        //{
        //    //Begin UndoStep
        //    Project.UndoContext.BeginUndoStep("MultipleTarget");
        //    try
        //    {
        //        Logger.AddMessage(new LogMessage("Released Picked"));
        //        GraphicPicker.GraphicPick -= new GraphicPickEventHandler(GraphicPicker_GraphicPick);

        //    }
        //    catch (Exception ex)
        //    {
        //        Project.UndoContext.CancelUndoStep(CancelUndoStepType.Rollback);
        //        Logger.AddMessage(new LogMessage(ex.Message.ToString()));
        //    }
        //    finally
        //    {
        //        //End UndoStep
        //        Project.UndoContext.EndUndoStep();

        //    }
        //}


        private static void GraphicPicker_GraphicPick(object sender, GraphicPickEventArgs e)
        {

            Logger.AddMessage(new LogMessage("GraphicPicker_GraphicPick"));

            CreateMarkUp(e.PickedPosition);
            FirstClick = false;

            return;

            //Station station = Project.ActiveProject as Station;
            //string stepName = station.ActiveTask.GetValidRapidName("Target", "_", 10);

            ////Begin UndoStep
            //Project.UndoContext.BeginUndoStep("Pick Position");
            //try
            //{
            //    Logger.AddMessage(new LogMessage("GraphicPicker_GraphicPick"));

            //    CreateMarkUp(e.PickedPosition);
            //    FirstClick = false;
            //}
            //catch (Exception exception)
            //{
            //    Project.UndoContext.CancelUndoStep(CancelUndoStepType.Rollback);
            //    Logger.AddMessage(new LogMessage(exception.Message.ToString()));
            //}
            //finally
            //{
            //    //End UndoStep
            //    Project.UndoContext.EndUndoStep();
            //}
        }



        //private static void Release_GraphicPick(object sender, GraphicPickEventArgs e)
        //{

        //    //Station station = Project.ActiveProject as Station;
        //    //string stepName = station.ActiveTask.GetValidRapidName("Target", "_", 10);

        //    //Begin UndoStep
        //    Project.UndoContext.BeginUndoStep("Release Pick Position");
        //    try
        //    {
        //        Logger.AddMessage(new LogMessage("Pick Released"));
        //    }
        //    catch (Exception exception)
        //    {
        //        Project.UndoContext.CancelUndoStep(CancelUndoStepType.Rollback);
        //        Logger.AddMessage(new LogMessage(exception.Message.ToString()));
        //    }
        //    finally
        //    {
        //        //End UndoStep
        //        Project.UndoContext.EndUndoStep();
        //    }
        //}


        //public static void GetPos(Vector3 position)
        //{
        //    Logger.AddMessage(new LogMessage(position.ToString()));
        //    PositionControlPos.SetFocus();
        //}

        //private static void AddPos(Vector3 position)
        //{
        //    CreateMarkUp(position);
        //    //ListBoxPointsList.SelectedIndices.Clear();
        //    //listMarks.Add(position);
        //    //listBoxPointsList.Items.Add(position);
        //    //ListBoxPointsList.Items.Add("MarkUp " + listMarks.Count.ToString());
        //    //ListBoxPointsList.SelectedIndex = ListBoxPointsList.Items.Count - 1;
            
        //    //PositionControlPos.Value = position;
        //    //PositionControlPos.SetFocus();
        //    Logger.AddMessage(new LogMessage("MarkNumber " + MarkNumber));
        //}

        private static int Increments()
        {
            int inc = Int16.Parse(ComboboxIncrementSteps.SelectedItem.ToString());

            return inc;
        }

        private static void ComboboxIncrementSteps_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboboxIncrementSteps.SelectedIndex == -1)
            {
                NumericUpDownStartWith.Value = decimal.Zero;
            }
            else
            {
                NumericUpDownStartWith.Value = Convert.ToDecimal(ComboboxIncrementSteps.SelectedItem.ToString());
            }
        }

        private static void TextValueChanged(object sender, EventArgs e)
        {
            LabelResultname.Text = TextBoxPrefix.Text + NumericUpDownStartWith.Value + TextBoxSuffix.Text;
        }

        public static void CreateMarkUp(Vector3 position)
        {
            Station station = Project.ActiveProject as Station;
            Markup markupWText = new Markup();
            markupWText.Transform.Translation = position;
            markupWText.Text = GenerateName();
            markupWText.Name = markupWText.Text;
            station.Markups.Add(markupWText);
            MarkNumber += 1 *Increments();
            return;
            //Logger.AddMessage(new LogMessage("MarkNumber : " + MarkNumber));
            //Logger.AddMessage(new LogMessage("Increments : " + Increments()));
        }

        public static string GenerateName()
        {
            string pref = TextBoxPrefix.Text;
            int name = Int16.Parse(NumericUpDownStartWith.Text);
            string suff = TextBoxSuffix.Text;

            int resultname = name + MarkNumber;

            string generatedName = pref + resultname + suff;


            return generatedName;
        }


    }
}