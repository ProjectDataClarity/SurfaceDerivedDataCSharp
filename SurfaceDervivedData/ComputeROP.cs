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
    class ComputeROPClass
    {
        /// Computation method ROP approach 1 
        string[] RigStateROP = { "Undefined", " ", "Drilling", "Connection", "Reaming", " ", " ", "Circulate", "TripIn", "TripOut" };

        // Create util to use Data access utilities
        DataColumnClass util = new DataColumnClass();

        // Main method - compute bit depth from changes into block height in time, using a rig state (rotary drilling, slide drilling, or
        // oscillate slide drilling) to determine if the ROP needs to be calculated.  Currently this uses a temporary single state
        // (enumeration = 2, which should be drilling)
        public Data ComputeRop(Data Data)
        {
            // Find the Rig State data column
            DataValues rigStates = util.FindDataColumn(Data, "Rig_State");

            // If we have rig state proceed
            if (rigStates != null)
            {
                // Find the block height column, if it exists proceed
                DataValues blockHeights = util.FindDataColumn(Data, "Block_Height");
                if (blockHeights != null)
                {
                    // Computation algorithm
                    // If the rig state is "2" then compute ROP from the current and previous block heights and timestamps
                    // We keep the previous block height and timestamp in startXXX variables and the current sample in endXXX 

                    // Create output space
                    double outRop = 0;

                    // load the first values of block height and its' timestamp into the startXXX varaible for initialization
                    double startBlockHeight = Convert.ToDouble(blockHeights.DataColumn[0].Value);
                    DateTime startTime = blockHeights.DataColumn[0].Timestamp;

                    //

                    // Create an output data column to store results back into Data
                    DataValues DataValues = new DataValues();
                    DataValues.Name = "Computed_ROP Approach 1";
                    DataValues.Units = "ft/hr";
                    DataValues.DataColumn = new List<DataValue>();
                    DataValues.DataColumn.Add(new DataValue(startTime, outRop.ToString()));

                    // Main computation Loop
                    // For each data array index, extract the values of rig state, block height, and the timestamp
                    // If the state is Drilling, then use current and previous value of block height and timestamp to compute ROP
                    for (int i = 1; i < rigStates.DataColumn.Count; i++)
                    {
                        // Extract the values of rig state, block height, and the timestamp
                        string rigState = string.Empty;
                        try
                        {
                            rigState = RigStateROP[Convert.ToInt32( rigStates.DataColumn[i].Value)];
                        }
                        catch
                        {
                            rigState = string.Empty;
                        }
                        double endBlockHeight = Convert.ToDouble(blockHeights.DataColumn[i].Value);
                        DateTime endTime = blockHeights.DataColumn[i].Timestamp;

                        // If we are "drilling, compute ROP, else ROP = 0
                        if (rigState == "Drilling")
                        {
                            /// Trap schlumberger null
                            if (endBlockHeight == -999.25)
                            {
                                outRop = 0;
                            }
                            else
                            {
                                // Compute the difference in time in seconds, and convert the number to a double
                                int diff = (endTime - startTime).Seconds;
                                double div = Convert.ToDouble(Convert.ToDouble(diff));

                                // Error trap for two samples with the same timestamp, compute ROP
                                if (div != 0)
                                {
                                    outRop = 3600 * (endBlockHeight - startBlockHeight) / div;
                                }
                            }

                            // Trap to catch bad rig state values
                            //if (outRop < 0)
                            //{
                            //    outRop = 0;
                            //}
                        }
                        else
                        {
                            // Wrong rig state for ROP calc
                            outRop = 0;
                        }

                        //Final trap throw out ROP < 0
                        if (outRop < 0)
                        {
                            outRop = 0;
                        }

                        // Take the calculated ROP and append the value to the Data column values
                        DataValues.DataColumn.Add(new DataValue(endTime, outRop.ToString()));
                        startTime = endTime;
                        startBlockHeight = endBlockHeight;
                    }

                    // Add the newly created ROP column to Data
                    DataValues.Raw = false;
                    if (util.FindIfDataColumnExists(Data, DataValues.Name))
                    {
                        DataValues.Name = util.GenerateVersonedName(Data, DataValues.Name);
                    }
                    Data.DataList.Add(DataValues);
                }
                else
                {
                    /// no block height found
                }
            }
            else
            {
                /// no rig state found
            }

            return (Data);
        }
    }
}
