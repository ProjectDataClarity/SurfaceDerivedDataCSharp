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
    class DataColumnClass
    {
        // These methods interact with Data to return information about data traces or columns
        // These are effectively the memory management for Data

        // This method returns an data column (header and values) based on the input column name
        public DataValues FindDataColumn(Data Data, string name)
        {
            DataValues Column = null;

            // For each data column find a match of the of column name and requested column name
            // if there is a match, break out of the search and return the data column
            foreach (DataValues dv in Data.DataList)
            {
                if (dv.Name.ToLower() == name.ToLower())
                {
                    Column = dv;
                    break;
                }
            }
            return (Column);
        }

        // This method find the integer index associated with a requested column name
        public int FindDataColumnIndex(Data Data, string name)
        {
            // index counter to be output
            int i = 0;

            // Loop through the columns in Data, and if there is a match of column name and reqwuested column name,
            // then return the index of the column array.  This is quite useful for locating the DateTime column.
            // It also eliminates a fixed position for the DateTime column in the input file
            foreach (DataValues dv in Data.DataList)
            {
                if (dv.Name.ToLower() == name.ToLower())
                {
                    break;
                }
                i++;
            }
            return (i);
        }

        // This method checks for the existence of a column in Data.  It is used primarily in checking/versioning the trace name, as multiple traces with the same name
        // will cause identity issues betwee traces of the same name
        public bool FindIfDataColumnExists(Data Data, string name)
        {
            bool retVal = false;

            foreach (DataValues dv in Data.DataList)
            {
                if (dv.Name.ToLower() == name.ToLower())
                {
                    retVal = true;
                    break;
                }
            }

            return (retVal);
        }

        // This method checks to see if a trace of the same name already exisits in Data (case: when the same caluculation is run twice, the output will already
        // exist in Data, so we change the name so that the traces are distinguishable.
        public string GenerateVersonedName(Data Data, string name)
        {
            string retVal = string.Empty;
            string newName = string.Empty;
            int i = 1;

            bool tryAgain = true;
            while(tryAgain) 
            {
                newName = name + " version " + i.ToString();
                tryAgain = FindIfDataColumnExists(Data, newName);
                i++;
            }
            retVal = newName;
            return (retVal);
        }
    }
}
