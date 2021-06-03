namespace Test_AutoMarkUp
{
    partial class UserControl1
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolBarControl1 = new ABB.Robotics.RobotStudio.Environment.ToolBarControl();
            this.comboBoxEx1 = new RobotStudio.API.Internal.Forms.ComboBoxEx();
            this.numericTextBox1 = new ABB.Robotics.RobotStudio.Stations.Forms.NumericTextBox();
            this.numericTextBoxArray1 = new ABB.Robotics.RobotStudio.Stations.Forms.NumericTextBoxArray();
            this.objectSelectionControl1 = new ABB.Robotics.RobotStudio.Stations.Forms.ObjectSelectionControl();
            this.positionControl1 = new ABB.Robotics.RobotStudio.Stations.Forms.PositionControl();
            this.referenceComboBox1 = new ABB.Robotics.RobotStudio.Stations.Forms.ReferenceComboBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolBarControl1
            // 
            this.toolBarControl1.BackColor = System.Drawing.Color.Black;
            this.toolBarControl1.Location = new System.Drawing.Point(16, 15);
            this.toolBarControl1.Name = "toolBarControl1";
            this.toolBarControl1.Size = new System.Drawing.Size(177, 65);
            this.toolBarControl1.TabIndex = 0;
            this.toolBarControl1.Text = "toolBarControl1";
            // 
            // comboBoxEx1
            // 
            this.comboBoxEx1.AutoAdjustDropDownWidth = true;
            this.comboBoxEx1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBoxEx1.FormattingEnabled = true;
            this.comboBoxEx1.ImageSize = 16;
            this.comboBoxEx1.Location = new System.Drawing.Point(238, 15);
            this.comboBoxEx1.Name = "comboBoxEx1";
            this.comboBoxEx1.SelectedObject = null;
            this.comboBoxEx1.Size = new System.Drawing.Size(121, 21);
            this.comboBoxEx1.TabIndex = 1;
            // 
            // numericTextBox1
            // 
            this.numericTextBox1.ErrorProviderControl = null;
            this.numericTextBox1.ExpressionErrorString = "Bad Expression";
            this.numericTextBox1.LabelText = null;
            this.numericTextBox1.Location = new System.Drawing.Point(238, 45);
            this.numericTextBox1.MaxValue = 1000000000D;
            this.numericTextBox1.MaxValueErrorString = "Value exceeds maximum";
            this.numericTextBox1.MinValue = -1000000000D;
            this.numericTextBox1.MinValueErrorString = "Value is below minimum";
            this.numericTextBox1.Name = "numericTextBox1";
            this.numericTextBox1.Quantity = ABB.Robotics.RobotStudio.BuiltinQuantity.None;
            this.numericTextBox1.ReadOnly = false;
            this.numericTextBox1.ShowLabel = true;
            this.numericTextBox1.Size = new System.Drawing.Size(121, 34);
            this.numericTextBox1.StepSize = 1D;
            this.numericTextBox1.TabIndex = 2;
            this.numericTextBox1.Text = "numericTextBox1";
            this.numericTextBox1.UserEdited = false;
            this.numericTextBox1.Value = 0D;
            // 
            // numericTextBoxArray1
            // 
            this.numericTextBoxArray1.ErrorProviderControl = null;
            this.numericTextBoxArray1.ExpressionErrorString = "Bad Expression";
            this.numericTextBoxArray1.LabelQuantity = ABB.Robotics.RobotStudio.BuiltinQuantity.None;
            this.numericTextBoxArray1.LabelText = null;
            this.numericTextBoxArray1.Location = new System.Drawing.Point(16, 106);
            this.numericTextBoxArray1.MaxValueErrorString = "Value exceeds maximum";
            this.numericTextBoxArray1.MinValueErrorString = "Value is below minimum";
            this.numericTextBoxArray1.Name = "numericTextBoxArray1";
            this.numericTextBoxArray1.NumTextBoxes = 1;
            this.numericTextBoxArray1.ReadOnly = false;
            this.numericTextBoxArray1.ShowLabel = true;
            this.numericTextBoxArray1.Size = new System.Drawing.Size(177, 34);
            this.numericTextBoxArray1.TabIndex = 3;
            this.numericTextBoxArray1.Text = "numericTextBoxArray1";
            this.numericTextBoxArray1.VerticalLayout = false;
            // 
            // objectSelectionControl1
            // 
            this.objectSelectionControl1.AutoSelectNextControl = true;
            this.objectSelectionControl1.Filter = null;
            this.objectSelectionControl1.InitialText = "";
            this.objectSelectionControl1.LabelText = null;
            this.objectSelectionControl1.Location = new System.Drawing.Point(238, 104);
            this.objectSelectionControl1.Name = "objectSelectionControl1";
            this.objectSelectionControl1.ReadOnly = false;
            this.objectSelectionControl1.SelectedObject = null;
            this.objectSelectionControl1.Size = new System.Drawing.Size(121, 35);
            this.objectSelectionControl1.TabIndex = 4;
            this.objectSelectionControl1.Text = "objectSelectionControl1";
            // 
            // positionControl1
            // 
            this.positionControl1.ErrorProviderControl = null;
            this.positionControl1.ExpressionErrorString = "Bad Expression";
            this.positionControl1.LabelQuantity = ABB.Robotics.RobotStudio.BuiltinQuantity.Length;
            this.positionControl1.LabelText = null;
            this.positionControl1.Location = new System.Drawing.Point(16, 167);
            this.positionControl1.MaxValueErrorString = "Value exceeds maximum";
            this.positionControl1.MinValueErrorString = "Value is below minimum";
            this.positionControl1.Name = "positionControl1";
            this.positionControl1.NumTextBoxes = 3;
            this.positionControl1.ReadOnly = false;
            this.positionControl1.RefCoordSys = null;
            this.positionControl1.ShowLabel = true;
            this.positionControl1.Size = new System.Drawing.Size(152, 34);
            this.positionControl1.TabIndex = 5;
            this.positionControl1.Text = "positionControl1";
            this.positionControl1.VerticalLayout = false;
            // 
            // referenceComboBox1
            // 
            this.referenceComboBox1.GraphicalFrameSize = 0.25D;
            this.referenceComboBox1.GraphicalFrameWidth = 5;
            this.referenceComboBox1.Location = new System.Drawing.Point(238, 179);
            this.referenceComboBox1.Name = "referenceComboBox1";
            this.referenceComboBox1.RefCoordSys = null;
            this.referenceComboBox1.ShowGraphicalFrame = true;
            this.referenceComboBox1.Size = new System.Drawing.Size(121, 21);
            this.referenceComboBox1.TabIndex = 6;
            this.referenceComboBox1.Text = "referenceComboBox1";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(238, 226);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 7;
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.referenceComboBox1);
            this.Controls.Add(this.positionControl1);
            this.Controls.Add(this.objectSelectionControl1);
            this.Controls.Add(this.numericTextBoxArray1);
            this.Controls.Add(this.numericTextBox1);
            this.Controls.Add(this.comboBoxEx1);
            this.Controls.Add(this.toolBarControl1);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(381, 281);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ABB.Robotics.RobotStudio.Environment.ToolBarControl toolBarControl1;
        private RobotStudio.API.Internal.Forms.ComboBoxEx comboBoxEx1;
        private ABB.Robotics.RobotStudio.Stations.Forms.NumericTextBox numericTextBox1;
        private ABB.Robotics.RobotStudio.Stations.Forms.NumericTextBoxArray numericTextBoxArray1;
        private ABB.Robotics.RobotStudio.Stations.Forms.ObjectSelectionControl objectSelectionControl1;
        private ABB.Robotics.RobotStudio.Stations.Forms.PositionControl positionControl1;
        private ABB.Robotics.RobotStudio.Stations.Forms.ReferenceComboBox referenceComboBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
    }
}
