
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
using RobotStudio.API.Internal.Forms;

namespace Test_AutoMarkUp
{
    public class frmAutoMarkUpBuilder : ToolControlBase
    {
        public bool _firstClick = true;

        public int _markNumber = 0;

        private PositionControl positionControlPos;

        private ComboBox comboBoxPrefix;

        private TextBox textBoxSuffix;

        private ComboBox comboBoxIncrementSteps;

        private NumericUpDown numericUpDownStartWith;

        private Label labelResultname;
        private Label labelPrefix;
        private Label labelSuffix;
        private Label labelNumIncrements;
        private Label labelStartnumber;
        private Label labelFirstlabelname;

        private Button buttonClear;
        private Button buttonCreate;
        private Button buttonClose;

        private GroupBox groupBox;

        public frmAutoMarkUpBuilder()
        {
            InitializeComponent();
            base.Activate += frmAutoMarkUp_Activate;
            base.Deactivate += frmAutoMarkUp_Deactivate;
        }
        private void frmAutoMarkUp_Activate(object sender, EventArgs e)
        {
            if (_firstClick | positionControlPos.Focused)
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

            CreateMarkUp(e.PickedPosition);
            _firstClick = false;
            positionControlPos.SetFocus();
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
            string pref = comboBoxPrefix.Text;
            int name = Int16.Parse(numericUpDownStartWith.Text);
            string suff = textBoxSuffix.Text;
            int resultname = name + _markNumber;
            string generatedName = pref + resultname + suff;
            return generatedName;
        }

        private int Increments()
        {
            int inc = Int16.Parse(comboBoxIncrementSteps.SelectedItem.ToString());
            return inc;
        }


        private void InitializeComponent()
        {
            int tw_width = UIEnvironment.Windows["ObjectBrowser"].Control.Size.Width - 15;

            positionControlPos = new PositionControl();
            comboBoxPrefix = new ComboBox();
            textBoxSuffix = new TextBox();
            labelPrefix = new Label();
            labelSuffix = new Label();
            labelNumIncrements = new Label();
            labelStartnumber = new Label();
            labelStartnumber = new Label();
            labelFirstlabelname = new Label();
            labelResultname = new Label();
            comboBoxIncrementSteps = new ComboBox();
            numericUpDownStartWith = new NumericUpDown();
            groupBox = new GroupBox();
            buttonClear = new Button();
            buttonCreate = new Button();
            buttonClose = new Button();


            positionControlPos.SuspendLayout();
            groupBox.SuspendLayout();
            SuspendLayout();


            positionControlPos.ErrorProviderControl = null;
            positionControlPos.ExpressionErrorString = "Bad Expression";
            positionControlPos.LabelQuantity = ABB.Robotics.RobotStudio.BuiltinQuantity.Length;
            positionControlPos.LabelText = "Position";
            positionControlPos.Location = new Point(8, 18);
            positionControlPos.MaxValueErrorString = "Value exceeds maximum";
            positionControlPos.MinValueErrorString = "Value is below minimum";
            positionControlPos.Name = "pos_control";
            positionControlPos.NumTextBoxes = 3;
            positionControlPos.ReadOnly = false;
            positionControlPos.RefCoordSys = null;
            positionControlPos.ShowLabel = true;
            positionControlPos.Size = new Size(tw_width, 34);
            positionControlPos.TabIndex = 1;
            positionControlPos.Text = "positionControl1";
            positionControlPos.VerticalLayout = false;
            
            labelPrefix.Text = "Name Prefix";
            labelPrefix.Location = new Point(8, 60);
            labelPrefix.Size = new Size(100, 14);

            comboBoxPrefix.Location = new Point(8, 75);
            comboBoxPrefix.Size = new Size((positionControlPos.Width / 2) - 15, 34);
            comboBoxPrefix.TabIndex = 2;
            comboBoxPrefix.DropDownWidth = 70;
            comboBoxPrefix.Items.AddRange(new object[]
                    {"p_",
                     "pname2_",
                     "pname3_",
                     "pname4_"});
            comboBoxPrefix.SelectedItem = "p_";
            comboBoxPrefix.TextChanged += new EventHandler(TextValueChanged);

            labelSuffix.Text = "Name Suffix";
            labelSuffix.Location = new Point((positionControlPos.Width / 2) + 23, 60);
            labelSuffix.Size = new Size(100, 14);

            textBoxSuffix.Location = new Point((positionControlPos.Width / 2) + 23, 75);
            textBoxSuffix.Size = comboBoxPrefix.Size;
            textBoxSuffix.TabIndex = 3;
            textBoxSuffix.TextChanged += new EventHandler(TextValueChanged);

            labelNumIncrements.Text = "Increment";
            labelNumIncrements.Location = new Point(8, 105);
            labelNumIncrements.Size = new Size(80, 14);

            comboBoxIncrementSteps.Location = new Point(8, 120);
            comboBoxIncrementSteps.Size = new Size(70, 45);
            comboBoxIncrementSteps.DropDownWidth = 70;
            comboBoxIncrementSteps.Items.AddRange(new object[]
                    {"1",
                     "10",
                     "100",
                     "1000"});
            comboBoxIncrementSteps.SelectedItem = "10";
            comboBoxIncrementSteps.TabIndex = 4;
            comboBoxIncrementSteps.SelectedIndexChanged += ComboboxIncrementSteps_SelectedIndexChanged;
            comboBoxIncrementSteps.SelectedIndexChanged += new EventHandler(TextValueChanged);

            labelStartnumber.Text = "Start number";
            labelStartnumber.Location = new Point(100, 105);
            labelStartnumber.Size = new Size(80, 14);

            numericUpDownStartWith.Location = new Point(100, 120);
            numericUpDownStartWith.Size = new Size(70, 45);
            numericUpDownStartWith.Minimum = 1;
            numericUpDownStartWith.Maximum = 1000;
            numericUpDownStartWith.Increment = 1;
            numericUpDownStartWith.DecimalPlaces = 0;
            numericUpDownStartWith.Value = 10;
            numericUpDownStartWith.DecimalPlaces = 0;
            numericUpDownStartWith.TabIndex = 5;
            numericUpDownStartWith.ValueChanged += new EventHandler(TextValueChanged);

            labelFirstlabelname.Text = "First label name: ";
            labelFirstlabelname.Location = new Point(8, 155);
            labelFirstlabelname.Size = new Size(90, 14);

            labelResultname.Location = new Point(95, 155);
            labelResultname.Size = new Size(80, 14);
            labelResultname.Text = comboBoxPrefix.Text + numericUpDownStartWith.Value + textBoxSuffix.Text;

            buttonClear.Location = new Point(tw_width - 170, 200);
            buttonClear.Size = new Size(53, 25);
            buttonClear.Text = "Clear";
            buttonClear.FlatStyle = FlatStyle.Flat;
            buttonClear.UseVisualStyleBackColor = true;
            buttonClear.Click += new EventHandler(btn_clear_clicked);

            buttonCreate.Location = new Point(tw_width - 110, 200);
            buttonCreate.Size = new Size(53, 25);
            buttonCreate.Text = "Create";
            buttonCreate.FlatStyle = FlatStyle.Flat;
            buttonCreate.UseVisualStyleBackColor = true;
            buttonCreate.Click += new EventHandler(btn_create_clicked);

            buttonClose.Location = new Point(tw_width - 50, 200);
            buttonClose.Size = new Size(53, 25);
            buttonClose.Text = "Close";
            buttonClose.FlatStyle = FlatStyle.Flat;
            buttonClose.UseVisualStyleBackColor = true;
            buttonClose.Click += new EventHandler(btn_close_clicked);


            base.AdjustableHeight = true;
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoScroll = true;
            base.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            base.Caption = "Auto MarkUp Tool";
            base.Controls.Add(positionControlPos);
            base.Controls.Add(labelPrefix);
            base.Controls.Add(comboBoxPrefix);
            base.Controls.Add(labelSuffix);
            base.Controls.Add(textBoxSuffix);
            base.Controls.Add(labelNumIncrements);
            base.Controls.Add(comboBoxIncrementSteps);
            base.Controls.Add(labelStartnumber);
            base.Controls.Add(numericUpDownStartWith);
            base.Controls.Add(labelFirstlabelname);
            base.Controls.Add(labelResultname);
            base.Controls.Add(buttonClear);
            base.Controls.Add(buttonCreate);
            base.Controls.Add(buttonClose);
            base.Name = "frmAutoMarkUpBuilder";
            base.Size = new System.Drawing.Size(242, 240);
            positionControlPos.ResumeLayout(false);
            groupBox.ResumeLayout(false);
            groupBox.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private void ComboboxIncrementSteps_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxIncrementSteps.SelectedIndex == -1)
            {
                numericUpDownStartWith.Value = decimal.Zero;
            }
            else
            {
                numericUpDownStartWith.Value = Convert.ToDecimal(comboBoxIncrementSteps.SelectedItem.ToString());
            }
        }

        private void TextValueChanged(object sender, EventArgs e)
        {
            labelResultname.Text = comboBoxPrefix.Text + numericUpDownStartWith.Value + textBoxSuffix.Text;
        }


        private void btn_clear_clicked(object sender, EventArgs e)
        {
            positionControlPos.Value = new Vector3(0, 0, 0);
            comboBoxPrefix.SelectedItem = "p_";
            textBoxSuffix.Text = "";
            comboBoxIncrementSteps.SelectedItem = "10";
            numericUpDownStartWith.Value = 10;
        }

        private void btn_create_clicked(object sender, EventArgs e)
        {
            CreateMarkUp(positionControlPos.Value);
            _firstClick = false;
            positionControlPos.SetFocus();
        }

        private void btn_close_clicked(object sender, EventArgs e)
        {
            GraphicPicker.GraphicPick -= GraphicPicker_GraphicPick;
            CloseTool();
        }



    }
}
