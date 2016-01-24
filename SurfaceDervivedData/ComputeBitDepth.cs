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
    class ComputeBitDepthClass
    {
        /// Computation method ROP approach 1 
        string[] RigStateBitDepth = { "Undefined", " ", "Drilling", "Connection", "Reaming", " ", " ", "Circulate", "TripIn", "TripOut" }; 

        // Create util to use Data access utilities
        DataColumnClass utility = new DataColumnClass();

        // Main method - compute ROP from changes into block height in time, using a rig state (rotary drilling, slide drilling, or
        // oscillate slide drilling) to determine if the bit depth needs to be calculated.  Currently this uses a temporary single state
        // (enumeration = 2, which should be drilling)
        public Data ComputeBitDepth(Data Data)
        {
            // Find the Rig State data column
            DataValues blockHeight = utility.FindDataColumn( Data, "Block_Height");
            DataValues rigStates = utility.FindDataColumn(Data, "Rig_State");

            // Create output 
            DataValues bitDepth = new DataValues();
            bitDepth.Name = "Bit_Depth";
            bitDepth.Units = blockHeight.Units;
            bitDepth.Raw = false;
            double oldBitDepth = 0;
            double oldBlockHeight = 0;

            // Create data array storage for output
            bitDepth.DataColumn = new List<DataValue>();

            /// do the algorithm
            for (int i = 0; i < blockHeight.DataColumn.Count; i++)
            {
                // Get calculation values
                DateTime t = Convert.ToDateTime(blockHeight.DataColumn[i].Timestamp);
                double val = Convert.ToDouble(blockHeight.DataColumn[i].Value);
                string rigState = RigStateBitDepth[Convert.ToInt32(rigStates.DataColumn[i].Value)];
                double bd = 0;

                if (i == 0)
                {
                    // Trap first time through algorithm - need at least two values to compute ROP
                    bd = 0;
                    oldBitDepth = 0;
                    oldBlockHeight = val;
                }
                else if (val == -999.25)
                {
                    // Trap schlumberger null value
                    bd = val;
                }
                else
                {
                    // Compute ROP
                    if (rigState == "Drilling" || rigState == "Reaming" || rigState == "TripIn" || rigState == "TripOut")
                    {
                        bd = oldBitDepth + (oldBlockHeight - val);
                        oldBitDepth = bd;
                    }
                    else
                    {
                        bd = oldBitDepth;
                    }
                    oldBlockHeight = val;
                }

                /// Put you output back into Data
                bitDepth.DataColumn.Add(new DataValue(t, bd.ToString()));
            }
            Data.DataList.Add(bitDepth);
            return (Data);
        }
    }
}
