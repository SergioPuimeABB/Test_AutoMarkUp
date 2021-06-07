
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ABB.Robotics.Math;
using ABB.Robotics.RobotStudio;
using ABB.Robotics.RobotStudio.Environment;
using ABB.Robotics.RobotStudio.Stations;
using ABB.Robotics.RobotStudio.Stations.Forms;

namespace Test_AutoMarkUp
{
    public class frmAutoMarkUpBuilder : ToolControlBase
    {
        public bool _firstClick = true;
        
        public int _markNumber = 0;

        private PositionControl PositionControlPos;

        private ListBox ListBoxPointsList;

        private TextBox TextBoxPrefix;

        private TextBox TextBoxSuffix;

        private TextBox TextBoxStartnumber;

        private ObjectSelectionControl ObjectSelectionControlIncrementSteps;

        private ComboBox ComboboxIncrementSteps;

        private NumericUpDown NumericUpDownStartWith;

        private Label LabelResultname;

        public frmAutoMarkUpBuilder()
        {
            InitializeComponent();
            base.Activate += frmAutoMarkUp_Activate;
            base.Deactivate += frmAutoMarkUp_Deactivate;
        }
        private void frmAutoMarkUp_Activate(object sender, EventArgs e)
        {
            if (_firstClick | PositionControlPos.Focused)
            {
                Logger.AddMessage(new LogMessage("PickTargets"));
                GraphicPicker.GraphicPick += new GraphicPickEventHandler(GraphicPicker_GraphicPick);
            }
        }

        private void frmAutoMarkUp_Deactivate(object sender, EventArgs e)
        {
            GraphicPicker.GraphicPick -= GraphicPicker_GraphicPick;
        }

        private void GraphicPicker_GraphicPick(object sender, GraphicPickEventArgs e)
        {
            Logger.AddMessage(new LogMessage("GraphicPicker_GraphicPick"));

            //e.Cursor = 
            CreateMarkUp(e.PickedPosition);
            _firstClick = false;
            PositionControlPos.SetFocus();
        }

        private void CreateMarkUp(Vector3 position)
        {
            Station station = Project.ActiveProject as Station;
            Markup markupWText = new Markup();
            markupWText.Transform.Translation = position;
            markupWText.Text = GenerateName();
            markupWText.Name = markupWText.Text;
            station.Markups.Add(markupWText);
            _markNumber += 1 * Increments();
        }

        private string GenerateName()
        {
            string pref = TextBoxPrefix.Text;
            int name = Int16.Parse(NumericUpDownStartWith.Text);
            string suff = TextBoxSuffix.Text;
            int resultname = name + _markNumber;

            string generatedName = pref + resultname + suff;
            return generatedName;
        }

        private int Increments()
        {
            int inc = Int16.Parse(ComboboxIncrementSteps.SelectedItem.ToString());
            return inc;
        }


        private void InitializeComponent()
        {
            //int tw_width = UIEnvironment.Windows["ObjectBrowser"].Control.Size.Width - 30;

            //tw = new ToolWindow("MyToolWindow_4");
            //tw.Caption = "Reference ToolWindow.";
            //tw.PreferredSize = new Size(tw_width, 330);
            //tw.Closed += new EventHandler(CloseTW); // ?????????????????????
            //UIEnvironment.Windows.AddDocked(tw, System.Windows.Forms.DockStyle.Top, UIEnvironment.Windows["ObjectBrowser"] as ToolWindow);

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
            //PositionControlPos.Leave += new EventHandler(ReleasePickTargets);
            //PositionControlPos.Click += new GraphicPickEventHandler(GraphicPicker_GraphicPick);
            //PositionControlPos.Enter += new EventHandler(PickTargets);
            //PositionControlPos.Pick += new EventHandler(PositionControlPosFocus);
            //PositionControlPos.Pick += new EventHandler(Release_PickTargets);
            //PositionControlPos.MouseEnter += new EventHandler(PickTargets);
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

        private void ComboboxIncrementSteps_SelectedIndexChanged(object sender, EventArgs e)
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

        private void TextValueChanged(object sender, EventArgs e)
        {
            LabelResultname.Text = TextBoxPrefix.Text + NumericUpDownStartWith.Value + TextBoxSuffix.Text;
        }



    }




    
}