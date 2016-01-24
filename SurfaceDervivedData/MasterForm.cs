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


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace SurfaceDervivedData
{

    public partial class MasterForm : Form
    {
        // Main form of the Windows application

        // This application is designed to test drilling calculations.  The application is broken into three phases - Read in raw data from a CSV
        // formatted file (with two header lines - one for the trace name, and another for the trace units); A group of buttons which fire off the
        // specific calculation; and a display section for viewing the output.  To create a new calculation, the user must create a calculation class
        // such as computerDummy, to handle the data IO and calculation and add a button to the calculation groupBox, whose callback is the calculation
        // class.  This eventually could be streamlined with button construction code, but not in the first version.

        // Create a stub for Data and initialize the Data utilities
        Data Data;
        DataColumnClass util = new DataColumnClass();

        // Add this delegate to handle any multi-threading issues with spawned plots
        delegate void SetPlotCallback(DataValues dv);

        // Establish the enumerations for plot trace colors and rig states
        string[] PlotColor = { "Blue", "Green", "Red", "Yellow", "Purple", "Orange", "Brown", "Aqua", "Black", "Pink"};
        string[] RigState = { "Undefined", " ", "Drilling", "Connection", "Reaming", " ", " ", "Circulate", "TripIn", "TripOut" };

        // Here's where we start with initialization
        public MasterForm()
        {
            InitializeComponent();
        }

        ///  Input Button Clicks - non calcuation button responses
        #region MainForm button clicks
 
        // Bring up file browser and update input file text box
        private void InputBrowseButtonClick(object sender, EventArgs e)
        {
            DialogResult result = openDataFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filename = openDataFileDialog.FileName;
                inputTextBox.Text = filename;
            }
        }

        // Load the data specified in the input text box into Data
        private void InputStatusButtonClick(object sender, EventArgs e)
        {
            inputStatusButton.Text = "Data Loaded";
            inputStatusButton.ForeColor = Color.White;
            inputStatusButton.BackColor = Color.Green;

            try
            {
                ReadInputCSV(inputTextBox.Text);
                AddButtons("raw");
            }
            catch
            {
                // Load has failed - reset status button
                inputStatusButton.Text = "No Data Loaded - Load Data";
                inputStatusButton.ForeColor = Color.Black;
                inputStatusButton.BackColor = Color.Red;
            }
        }

        private void WriteButtonClick(object sender, EventArgs e)
        {
            if (saveDataFileDialog.ShowDialog() == DialogResult.OK)
            {
                WriteOutputCSV(saveDataFileDialog.FileName);
            }
        }

        private void ExitClick(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        // All computation modules extract their raw data from Data and add their results to Data, where it can be displayed and output
        #region Computation Button Clicks

        //private void DummyClick(object sender, EventArgs e)
        //{
        //    /// This is a dummy stub for new calculations
        //
        //    /// Use try catch method for better error handling
        //    try
        //    {
        //        /// Instanteate your calculation class here
        //        YourCalc calc = new YourCalc();
        //
        //        /// Run your calculation here - Note return Data is the modified Data that includes the output of your calculation
        //        Data = calc.YourCalc(Data);
        //
        //        /// Add the buttons to display your output graphically
        //        AddButtons("calculated");
        //    }
        //    catch
        //    {
        //        /// Place error handling here
        //    }
        //}

        private void ComputeRopClick(object sender, EventArgs e)
        {
            // ROP Approach 1 calculation
            try
            {
                ComputeROPClass calc = new ComputeROPClass();
                Data = calc.ComputeRop(Data);
                AddButtons("calculated");
            }
            catch
            {
            }
        }

        private void ComputeBitDepthClick(object sender, EventArgs e)
        {
            // Bit Depth Approach 1 calculation
            try
            {
                ComputeBitDepthClass calc = new ComputeBitDepthClass();
                Data = calc.ComputeBitDepth(Data);
                AddButtons("calculated");
            }
            catch
            {
            }
        }

        #endregion

        // This region contains the code for reading, writing and manipulating the Data
        #region CSV file IO

        private void ReadInputCSV(string filename)
        {
            // Opens the input CSV data file and stores it in a temporary string, then parses the string into
            // data columns, by first reading the file header, then loads the data into Data the application
            // memory space.  Data contains each trace,  which is data vector, array, or column.  Each
            // sample in a data column consists of a data value, timestamp pair.  Column name and units are
            // stored in the data column header.

            // Read the filename from the DataInputFile textbox, open the file and read it into a string
            string rawData = File.ReadAllText(filename);

            // Remove carriage return issues and break the raw data string into  lines
            rawData = rawData.Replace('\n', '\r');
            string[] lines = rawData.Split(new char[] { '\r' }, StringSplitOptions.RemoveEmptyEntries);

            // Determine the number of rows and columns in the inoput data
            int rows = lines.Length;
            int columns = lines[0].Split(',').Length;

            // Transfer the string based values into Data, the master app database
            // Create Data root and root stub for the data columns (arrays)
            if (columns > 0)
            {
                Data = new Data();
                List<DataValues> DataValues = new List<DataValues>();
                Data.DataList = DataValues;
            }

            // Read Column Names and allocate space for each column of the data array

            // Assumes that there are two header lines - the first for names , the second for units
            string[] line = lines[0].Split(',');
            string[] unitLine = lines[1].Split(',');

            // Read the first two lines, then for each column read the data column name and units, 
            // then create and link each data column in Data 
            for (int i = 0; i < columns; i++)
            {
                DataValues dv = new DataValues();
                dv.Name = line[i].ToString();
                dv.Units = unitLine[i].ToString();

                dv.DataColumn = new List<DataValue>();
                Data.DataList.Add(dv);
            }

            // Now that the columns names are stored in Data, find the DateTime column
            // We need to find this column as each data value has a timestamp associated with it.
            //
            // A more effiecent way of doing this is to find a data samples index in the Data array,
            // then lookup the index in the timestamp array to find the associated timestamp for the sample.
            // However, we intend to have some form of graphic display, so having a value, timestamp pair
            // will be more efficient for plots and charts
            int tsIndex = util.FindDataColumnIndex(Data, "DateTime");
            
            // Now read the remaining lines into the data columns
            // Start at the third line in the data file
            for (int i = 2; i < rows; i++)
            {
                line = lines[i].Split(',');

                // For each data row read the input columns and create a data value
                for (int j = 0; j < columns; j++)
                {
                    DataValue dv = new DataValue();
                    dv.Timestamp = Convert.ToDateTime(line[tsIndex].ToString());
                    dv.Value = line[j].ToString();
                    Data.DataList[j].DataColumn.Add(dv);
                }
            }
        }

        private void WriteOutputCSV(string filename)
        {
            // This method dumps all the data in Data to a CSV file.
            // It constructs row based string lines, first for the column names, then the units, and finally data
            // Plenty of room for improvement - output file selection, column selector can be added later

            // Create the ouput streams for the file writer
            StreamWriter sw = null;
            Stream fs = null;

            // Open the file and attach the stream output to the output file
            try
            {
                fs = File.Open(filename, FileMode.Create);
                sw = new StreamWriter(fs);
            }
            catch
            {
            }

            // Build the name line,the units line, and finally the data
            try
            {
                sw.WriteLine(BuildColumnNames());
                sw.WriteLine(BuildColumnUnits());
                WriteData(sw);
            }
            catch
            {
            }

            // Close the output stream and file
            sw.Close();
            fs.Close();
        }

        private string BuildColumnNames()
        {
            // This method extracts the header name from each column in Data and parses it into a comma separated string
            string output = string.Empty;

            // If there are data columns proceed
            if (Data.DataList.Count > 0)
            {
                // Create the storage space for the output string
                StringBuilder sb = new StringBuilder();

                // Cycle through each column in Data a stuff the column name (from the data column header)
                // into the output string
                // Need to stop 1 sample short so that the last value does not have a "," behind it
                for (int i = 0; i < Data.DataList.Count - 1; i++)
                {
                    sb.Append(Data.DataList[i].Name);
                    sb.Append(", ");
                }
                sb.Append(Data.DataList[Data.DataList.Count -1].Name);

                // Convert the string builder into a string
                output = sb.ToString();
            }
            return (output);
        }

        private string BuildColumnUnits()
        {
            // This method extracts the units from each column in Data and parses it into a comma separated string
            string output = string.Empty;

            // If we have data columns proceed
            if (Data.DataList.Count > 0)
            {
                // Create space for the output string
                StringBuilder sb = new StringBuilder();

                // For each column in Data, pull the units value from the data column header
                // Again stop one sample short for , elimination
                for (int i = 0; i < Data.DataList.Count - 1; i++)
                {
                    sb.Append(Data.DataList[i].Units);
                    sb.Append(", ");
                }
                sb.Append(Data.DataList[Data.DataList.Count -1].Units);

                output = sb.ToString();
            }
            return (output);
        }

        private void WriteData(StreamWriter sw)
        {
            // Thie method extracts a row of values from Data and parses the columns into a comma separated string

            // If we have data proceed
            if (Data.DataList.Count > 0)
            {
                // Create the space for the output string
                StringBuilder sb = new StringBuilder();

                // For each row, clear the output string, parse the value of each column into the string and write the string to the output file
                // Again, we stop the column loop 1 sample short to eliminate the final comma for the string
                // Note that we are assuming that all data arrays have the same number of elements - and that the first data columns is representative
                // of the data vector size
                for (int i = 0; i < Data.DataList[0].DataColumn.Count; i++)
                {
                    sb.Clear();
                    for (int j = 0; j < Data.DataList.Count - 1; j++)
                    {
                        sb.Append(Data.DataList[j].DataColumn[i].Value);
                        sb.Append(", ");
                    }
                    sb.Append(Data.DataList[Data.DataList.Count -1].DataColumn[i].Value);
                    sw.WriteLine(sb.ToString());
                }
            }
        }

        #endregion

        // Graphics methods
        #region Program plot output and graphics

        private void AddButtons(string panel)
        {
            /// This method chechks Data and creates the buttons used to display raw and calculated data 

            // Clear the panels and build buttons
            if (panel == "raw")
            {
                rawPanel.Controls.Clear();
            }
            else
            {
                calculatedPanel.Controls.Clear();
            }

            // For each data column (excepth DateTime), identify the column and add a button to the correct (raw or calculated) panel
            int i = 0;
            Button btn = null;
            foreach (DataValues dv in Data.DataList)
            {
                if (dv.Raw && panel == "raw")
                {
                    if (dv.Name != "DateTime")
                    {
                        btn = new Button();
                        btn.Name = dv.Name;
                        btn.Text = dv.Name;
                        if (dv.Name.Length > 10)
                        {
                           ToolTip ToolTip1 = new ToolTip();
                           ToolTip1.SetToolTip(btn, dv.Name);
                        }
                        btn.Height = 20;
                        btn.Width = 120;
                        btn.Location = new Point(0, 20 * i + 15);
                        btn.MouseDown += new MouseEventHandler(PlotButtonClick);
                        i++;
                        rawPanel.Controls.Add(btn);
                    }
                }
                else if (!dv.Raw && panel != "raw")
                {
                    btn = new Button();
                    btn.Name = dv.Name;
                    btn.Text = dv.Name;
                    if (dv.Name.Length > 10)
                    {
                        ToolTip ToolTip1 = new ToolTip();
                        ToolTip1.SetToolTip(btn, dv.Name);
                    }
                    btn.Height = 20;
                    btn.Width = 120;
                    btn.Location = new Point(0, 20 * i + 15);
                    btn.MouseDown += new MouseEventHandler(PlotButtonClick);
                    i++;
                    calculatedPanel.Controls.Add(btn);
                }
            }
        }

        private void PlotButtonClick(object sender, MouseEventArgs e)
        {
            // This is the callback for a pushed button.  Using MouseButtonClick to indentify left click (add trace to main plot) and right click 
            // (spawn stand-alone plot of trace).
            MouseEventArgs me = (MouseEventArgs)e;
            Button btn = (Button)sender;

            // Left click - add to main plot
            if (me.Button == MouseButtons.Left)
            {
                if (btn.Name == btn.Text)
                {
                    btn.Text = btn.Text + ".";
                    DataValues dv = util.FindDataColumn(Data, btn.Name);
                    int Index = util.FindDataColumnIndex(Data, btn.Name);
                    btn.BackColor = Color.FromName(PlotColor[Index % 10]);
                    SetPlot(dv);
                }
                else
                {
                    btn.Text = btn.Name;
                    btn.BackColor = SystemColors.Control;
                    foreach (Series s in plotChart.Series)
                    {
                        if (s.Name == btn.Name)
                        {
                            plotChart.Series.Remove(s);
                            break;
                        }
                    }
                }
            }
            else
            {
                /// Right click - spawn plot
                SpawnPlot(btn);
            }


        }

        public void SetPlot(DataValues dv )
        {
            /// Ah, the fun begins - lets play with the Microsoft chart options -- Added in case of thread collisons
            if (plotChart.InvokeRequired)
            {
                SetPlotCallback d = new SetPlotCallback(SetPlot);
                this.Invoke(d, new object[] { dv });
            }
            else
            {
                MakePlot(dv);
            }
        }

        public void MakePlot(DataValues dv)
        {
            /// Build main graph

            /// Create series (data) to be input as data to plot and add series to the plot
            Series series = new Series(dv.Name);
            plotChart.Series.Add(series);

            /// Set line properies - type, color, width
            /// Use color table mod 10 to repeat color table
            plotChart.Series[dv.Name].ChartType = SeriesChartType.FastLine;
            plotChart.Series[dv.Name].BorderWidth = 2;
            int index = util.FindDataColumnIndex(Data, dv.Name);
            plotChart.Series[dv.Name].Color = Color.FromName(PlotColor[index%10]);

            /// Convert DateTime stamp to seconds
            /// 
            /// Get timestamp of first data sample and use as Time zero - the value to be subtracted from each timestamp
            DateTime origin = dv.DataColumn[0].Timestamp;
            double od = Convert.ToDouble(origin.Ticks/10000000);

            /// For each value - change the string value to a double and timestamp to seconds
            for (int i = 0; i <  dv.DataColumn.Count; i++)
            {
                double val = Convert.ToDouble(Convert.ToDateTime(dv.DataColumn[i].Timestamp).Ticks/10000000) - od;
                double rc = Convert.ToDouble(dv.DataColumn[i].Value);

                /// Add data to plot series - skip if -999.25 Schlumberger null
                if (rc != -999.25)
                {
                    plotChart.Series[dv.Name].Points.AddXY(val, rc);
                }
            }
        }

        private void SpawnPlot(Button btn)
        {
            // make same plot as the master plot, but in a new window.  Must add all the settings that are hidden in the Visual GUI
            Form spawn = new Form();
            Panel p = new Panel();
            Chart chart = new Chart();
            ChartArea ca = new ChartArea();
            Legend legend = new Legend();
            Title title = new Title();

            /// Set Form values
            spawn.Text = "SPE DSA-TS DQA Surface Derived Data Calculations - Spawned Plot";
            spawn.Height = 600;
            spawn.Width = 800;

            /// Set Panel values
            p.Dock = DockStyle.Fill;

            /// Set Chart Area values
            ca.Name = "chartArea";
            ca.AxisX.Title = "Elapsed Time in Seconds";
            ca.AxisX.Minimum = 0;

            // Legion values
            legend.Name = "Data Trace";
            legend.LegendStyle = LegendStyle.Column;
            legend.TableStyle = LegendTableStyle.Tall;

            // Title values
            title.Text = "Rig Data Display in Seconds";

            // Link chart elements into the main chart
            chart.ChartAreas.Add(ca);
            chart.Legends.Add(legend);
            chart.Titles.Add(title);

            /// Set Chart values
            chart.Dock = DockStyle.Fill;
            System.Windows.Forms.DataVisualization.Charting.Cursor cursorX = null;
            System.Windows.Forms.DataVisualization.Charting.Cursor cursorY = null;
            cursorX = chart.ChartAreas["chartArea"].CursorX;
            cursorX.Interval = 1;
            cursorY = chart.ChartAreas["chartArea"].CursorY;
            cursorX.LineWidth = 2;
            cursorY.LineWidth = 2;
            cursorX.LineDashStyle = ChartDashStyle.DashDot;
            cursorY.LineDashStyle = ChartDashStyle.DashDot;
            cursorX.LineColor = Color.Red;
            cursorY.LineColor = Color.Red;
            cursorX.SelectionColor = Color.Yellow;
            cursorY.SelectionColor = Color.Yellow;

            // Enable end user interactivity     
            chart.ChartAreas["chartArea"].CursorX.IsUserEnabled = true;
            chart.ChartAreas["chartArea"].CursorX.IsUserSelectionEnabled = true;
            chart.ChartAreas["chartArea"].CursorY.IsUserEnabled = true;
            chart.ChartAreas["chartArea"].CursorY.IsUserSelectionEnabled = true;

            /// Get data to display
            DataValues dv = util.FindDataColumn(Data, btn.Name);
            int index = util.FindDataColumnIndex(Data, dv.Name);

            /// Create Series
            Series series = new Series(dv.Name);
            chart.Series.Add(series);
            /// Set line properies - type, color, width
            /// Use color table mod 10 to repeat color table
            chart.Series[dv.Name].ChartType = SeriesChartType.FastLine;
            chart.Series[dv.Name].BorderWidth = 2;
            chart.Series[dv.Name].Color = Color.FromName(PlotColor[index % 10]);

            /// Convert DateTime stamp to seconds
            /// 
            /// Get timestamp of first data sample and use as Time zero - the value to be subtracted from each timestamp
            DateTime origin = dv.DataColumn[0].Timestamp;
            double od = Convert.ToDouble(origin.Ticks / 10000000);

            /// For each value - change the string value to a double and timestamp to seconds
            for (int i = 0; i < dv.DataColumn.Count; i++)
            {
                double val = Convert.ToDouble(Convert.ToDateTime(dv.DataColumn[i].Timestamp).Ticks / 10000000) - od;
                double rc = Convert.ToDouble(dv.DataColumn[i].Value);

                /// Add data to plot series - skip if -999.25 Schlumberger null
                if (rc != -999.25)
                {
                    chart.Series[dv.Name].Points.AddXY(val, rc);
                }
            }

            p.Controls.Add(chart);
            spawn.Controls.Add(p);
            chart.Show();
            spawn.Show();

        }
        #endregion
    }
}
