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
using System.Linq;
using System.Text;

namespace SurfaceDervivedData
{
    class ComputeDummyClass
    {
        /// Dummy Computation method
        string[] RigStateDummy = { "Undefined", " ", "Drilling", "Connection", "Reaming", " ", " ", "Circulate", "TripIn", "TripOut" };

        // Create util to use Data access utilities
        DataColumnClass util = new DataColumnClass();

        // Main method - compute dummy stub.  
        public Data ComputeDummy(Data Data)
        {
            /// Get your needed input data here

                // Example - get rig state and blovk height
                DataValues rigStates = util.FindDataColumn(Data, "Rig_State");
                DataValues blockHeights = util.FindDataColumn(Data, "Block_Height");

            // Define you data output channels here
                // Create an output data column to store results back into Data
                DataValues Output = new DataValues();
                Output.Name = "Your output data column name";
                Output.Units = "Your output data units";

                // Create data array storage for output
                Output.DataColumn = new List<DataValue>();


            // Put you calculation logic here

                // Example do dummy calculation - remember to cast values to the proper type (string, double, int, ....)
                double dummy = 0;
                foreach (DataValue dv in blockHeights.DataColumn)
                {
                    DateTime t = Convert.ToDateTime(dv.Timestamp);
                    double val = Convert.ToDouble(dv.Value);

                    dummy = dummy + val;

                    /// Store result back into Data
                    Output.DataColumn.Add(new DataValue(t, dummy.ToString()));
                }

                // or

                for (int i = 0; i < blockHeights.DataColumn.Count; i++)
                {
                    DateTime t = Convert.ToDateTime(blockHeights.DataColumn[i].Timestamp);
                    double val = Convert.ToDouble(blockHeights.DataColumn[i].Value);
                    string rigState = RigStateDummy[Convert.ToInt32(rigStates.DataColumn[i].Value)];

                    if (rigState == "Drilling")
                    {
                        dummy = dummy + val;
                    }
                    else
                    {
                        dummy = dummy - val;
                    }

                    /// Put you output back into Data
                    Output.DataColumn.Add(new DataValue(t, dummy.ToString()));
                }

            // Return the updated Data to the master routines
            Data.DataList.Add(Output);
            return (Data);
        }
    }
}
