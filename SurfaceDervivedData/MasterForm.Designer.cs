/*

Copyright[2016][Martin Cavanaugh][Project Data Clarity]

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

*/



namespace SurfaceDervivedData
{
    partial class MasterForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.masterPanel = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RopButton = new System.Windows.Forms.Button();
            this.inputGroupBox = new System.Windows.Forms.GroupBox();
            this.inputStatusButton = new System.Windows.Forms.Button();
            this.inputBrowseButton = new System.Windows.Forms.Button();
            this.inputTextBox = new System.Windows.Forms.TextBox();
            this.inputLabel = new System.Windows.Forms.Label();
            this.openDataFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.InputDataGroupBox = new System.Windows.Forms.GroupBox();
            this.CalculatedDataGroupBox = new System.Windows.Forms.GroupBox();
            this.plotChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.rawPanel = new System.Windows.Forms.Panel();
            this.calculatedPanel = new System.Windows.Forms.Panel();
            this.writeButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.saveDataFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.BitDepthButton = new System.Windows.Forms.Button();
            this.masterPanel.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.inputGroupBox.SuspendLayout();
            this.InputDataGroupBox.SuspendLayout();
            this.CalculatedDataGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.plotChart)).BeginInit();
            this.SuspendLayout();
            // 
            // masterPanel
            // 
            this.masterPanel.Controls.Add(this.exitButton);
            this.masterPanel.Controls.Add(this.writeButton);
            this.masterPanel.Controls.Add(this.groupBox2);
            this.masterPanel.Controls.Add(this.groupBox1);
            this.masterPanel.Controls.Add(this.inputGroupBox);
            this.masterPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.masterPanel.Location = new System.Drawing.Point(0, 0);
            this.masterPanel.Name = "masterPanel";
            this.masterPanel.Size = new System.Drawing.Size(784, 562);
            this.masterPanel.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.plotChart);
            this.groupBox2.Controls.Add(this.CalculatedDataGroupBox);
            this.groupBox2.Controls.Add(this.InputDataGroupBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 167);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(760, 351);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Output";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.BitDepthButton);
            this.groupBox1.Controls.Add(this.RopButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 82);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(760, 79);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Calculations";
            // 
            // RopButton
            // 
            this.RopButton.Location = new System.Drawing.Point(9, 19);
            this.RopButton.Name = "RopButton";
            this.RopButton.Size = new System.Drawing.Size(125, 23);
            this.RopButton.TabIndex = 0;
            this.RopButton.Text = "Compute ROP";
            this.RopButton.UseVisualStyleBackColor = true;
            this.RopButton.Click += new System.EventHandler(this.ComputeRopClick);
            // 
            // inputGroupBox
            // 
            this.inputGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputGroupBox.Controls.Add(this.inputStatusButton);
            this.inputGroupBox.Controls.Add(this.inputBrowseButton);
            this.inputGroupBox.Controls.Add(this.inputTextBox);
            this.inputGroupBox.Controls.Add(this.inputLabel);
            this.inputGroupBox.Location = new System.Drawing.Point(12, 12);
            this.inputGroupBox.Name = "inputGroupBox";
            this.inputGroupBox.Size = new System.Drawing.Size(760, 64);
            this.inputGroupBox.TabIndex = 0;
            this.inputGroupBox.TabStop = false;
            this.inputGroupBox.Text = "Data Input";
            // 
            // inputStatusButton
            // 
            this.inputStatusButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.inputStatusButton.Location = new System.Drawing.Point(586, 26);
            this.inputStatusButton.Name = "inputStatusButton";
            this.inputStatusButton.Size = new System.Drawing.Size(168, 23);
            this.inputStatusButton.TabIndex = 3;
            this.inputStatusButton.Text = "No Data Loaded - Load Data";
            this.inputStatusButton.UseVisualStyleBackColor = true;
            this.inputStatusButton.Click += new System.EventHandler(this.InputStatusButtonClick);
            // 
            // inputBrowseButton
            // 
            this.inputBrowseButton.Location = new System.Drawing.Point(377, 26);
            this.inputBrowseButton.Name = "inputBrowseButton";
            this.inputBrowseButton.Size = new System.Drawing.Size(98, 23);
            this.inputBrowseButton.TabIndex = 2;
            this.inputBrowseButton.Text = "Browse";
            this.inputBrowseButton.UseVisualStyleBackColor = true;
            this.inputBrowseButton.Click += new System.EventHandler(this.InputBrowseButtonClick);
            // 
            // inputTextBox
            // 
            this.inputTextBox.Location = new System.Drawing.Point(121, 28);
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.Size = new System.Drawing.Size(250, 20);
            this.inputTextBox.TabIndex = 1;
            // 
            // inputLabel
            // 
            this.inputLabel.AutoSize = true;
            this.inputLabel.Location = new System.Drawing.Point(6, 31);
            this.inputLabel.Name = "inputLabel";
            this.inputLabel.Size = new System.Drawing.Size(109, 13);
            this.inputLabel.TabIndex = 0;
            this.inputLabel.Text = "Input Data File (.CSV)";
            // 
            // openDataFileDialog
            // 
            this.openDataFileDialog.FileName = "SampleData.csv";
            this.openDataFileDialog.Filter = "\"CSV Files|*.csv|All Files|*.*";
            // 
            // InputDataGroupBox
            // 
            this.InputDataGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.InputDataGroupBox.Controls.Add(this.rawPanel);
            this.InputDataGroupBox.Location = new System.Drawing.Point(9, 20);
            this.InputDataGroupBox.Name = "InputDataGroupBox";
            this.InputDataGroupBox.Size = new System.Drawing.Size(125, 325);
            this.InputDataGroupBox.TabIndex = 0;
            this.InputDataGroupBox.TabStop = false;
            this.InputDataGroupBox.Text = "Input Data";
            // 
            // CalculatedDataGroupBox
            // 
            this.CalculatedDataGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CalculatedDataGroupBox.Controls.Add(this.calculatedPanel);
            this.CalculatedDataGroupBox.Location = new System.Drawing.Point(629, 17);
            this.CalculatedDataGroupBox.Name = "CalculatedDataGroupBox";
            this.CalculatedDataGroupBox.Size = new System.Drawing.Size(125, 325);
            this.CalculatedDataGroupBox.TabIndex = 1;
            this.CalculatedDataGroupBox.TabStop = false;
            this.CalculatedDataGroupBox.Text = "Calculated Data";
            // 
            // plotChart
            // 
            this.plotChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.AxisX.Minimum = 0D;
            chartArea1.AxisX.Title = "Elapsed Time in Seconds";
            chartArea1.CursorX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Seconds;
            chartArea1.CursorX.IsUserEnabled = true;
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.CursorX.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDot;
            chartArea1.CursorX.LineWidth = 2;
            chartArea1.CursorX.SelectionColor = System.Drawing.Color.Gold;
            chartArea1.CursorY.IsUserEnabled = true;
            chartArea1.CursorY.IsUserSelectionEnabled = true;
            chartArea1.CursorY.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDot;
            chartArea1.CursorY.LineWidth = 2;
            chartArea1.CursorY.SelectionColor = System.Drawing.Color.Gold;
            chartArea1.Name = "ChartArea1";
            this.plotChart.ChartAreas.Add(chartArea1);
            legend1.LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Column;
            legend1.Name = "Data Trace";
            legend1.TableStyle = System.Windows.Forms.DataVisualization.Charting.LegendTableStyle.Tall;
            this.plotChart.Legends.Add(legend1);
            this.plotChart.Location = new System.Drawing.Point(140, 33);
            this.plotChart.Name = "plotChart";
            this.plotChart.Size = new System.Drawing.Size(486, 309);
            this.plotChart.TabIndex = 2;
            this.plotChart.Text = "Data Visualization";
            title1.Name = "Title1";
            title1.Text = "Rig Data Display in Seconds";
            this.plotChart.Titles.Add(title1);
            // 
            // rawPanel
            // 
            this.rawPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rawPanel.Location = new System.Drawing.Point(3, 16);
            this.rawPanel.Name = "rawPanel";
            this.rawPanel.Size = new System.Drawing.Size(119, 306);
            this.rawPanel.TabIndex = 0;
            // 
            // calculatedPanel
            // 
            this.calculatedPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.calculatedPanel.Location = new System.Drawing.Point(3, 16);
            this.calculatedPanel.Name = "calculatedPanel";
            this.calculatedPanel.Size = new System.Drawing.Size(119, 306);
            this.calculatedPanel.TabIndex = 0;
            // 
            // writeButton
            // 
            this.writeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.writeButton.Location = new System.Drawing.Point(24, 527);
            this.writeButton.Name = "writeButton";
            this.writeButton.Size = new System.Drawing.Size(119, 23);
            this.writeButton.TabIndex = 3;
            this.writeButton.Text = "Write CSV output";
            this.writeButton.UseVisualStyleBackColor = true;
            this.writeButton.Click += new System.EventHandler(this.WriteButtonClick);
            // 
            // exitButton
            // 
            this.exitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.exitButton.Location = new System.Drawing.Point(644, 527);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(119, 23);
            this.exitButton.TabIndex = 4;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.ExitClick);
            // 
            // saveDataFileDialog
            // 
            this.saveDataFileDialog.FileName = "DQAOutput.csv";
            this.saveDataFileDialog.Filter = "\"CSV Files|*.csv|All Files|*.*";
            // 
            // BitDepthButton
            // 
            this.BitDepthButton.Location = new System.Drawing.Point(140, 19);
            this.BitDepthButton.Name = "BitDepthButton";
            this.BitDepthButton.Size = new System.Drawing.Size(125, 23);
            this.BitDepthButton.TabIndex = 1;
            this.BitDepthButton.Text = "Compute Bit Depth";
            this.BitDepthButton.UseVisualStyleBackColor = true;
            this.BitDepthButton.Click += new System.EventHandler(this.ComputeBitDepthClick);
            // 
            // MasterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.masterPanel);
            this.Name = "MasterForm";
            this.Text = "SPE DSA-TS DQA Surface Derived Data Calculations";
            this.masterPanel.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.inputGroupBox.ResumeLayout(false);
            this.inputGroupBox.PerformLayout();
            this.InputDataGroupBox.ResumeLayout(false);
            this.CalculatedDataGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.plotChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel masterPanel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox inputGroupBox;
        private System.Windows.Forms.Button inputStatusButton;
        private System.Windows.Forms.Button inputBrowseButton;
        private System.Windows.Forms.TextBox inputTextBox;
        private System.Windows.Forms.Label inputLabel;
        private System.Windows.Forms.OpenFileDialog openDataFileDialog;
        private System.Windows.Forms.Button RopButton;
        private System.Windows.Forms.GroupBox CalculatedDataGroupBox;
        private System.Windows.Forms.GroupBox InputDataGroupBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart plotChart;
        private System.Windows.Forms.Panel calculatedPanel;
        private System.Windows.Forms.Panel rawPanel;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button writeButton;
        private System.Windows.Forms.SaveFileDialog saveDataFileDialog;
        private System.Windows.Forms.Button BitDepthButton;
    }
}

