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
    ///  This represents the data structure for the display program.
    ///  
    /// Data (the class) is the root of the data structure.  Data contains a list of all the traces that have been read from the outside of the program
    /// (raw), and all the computed results (calculated).  This is a list of DataValues which is an individual data trace.  Each DataValues column or trace
    /// consists of a header and a list or array of DataValue.  DataValue is the data structure for an individual value, timestamp pair or a signle data point.
    /// The List of DataValue therfore represents all the data points for an individual trace.  The Header information for DataValues is the name of the trace
    /// or column, the units for the trace (which is currently vestigial) and a boolean (raw) which helps distinguish the read in values (raw) from the
    /// calculated traces (calculated).
    /// 
    /// The strenght of this is a single data entity that can be passed to the calculation routine or method.  All the input data and previously calculated data will
    /// available to the calculation method.  Data also represents a repository of all the results, so that calculations can be chained together.
    /// 

    public class DataValue
    {
        /// <summary>
        ///  This class is the basic data value, timestamp pair.  It is the single data value entity.
        ///  DataValue currently has just a data value, timestamp pair but can be extended to include item like value status, bookkeeping for an audit trail, etc.
        /// </summary>
        private DateTime _dt;
        private string _value;

        /// Creators
        public DataValue()
        {
            this._dt = DateTime.MinValue;
            this._value = string.Empty;
        }

        /// API
        public DataValue(DateTime dt, string value)
        {
            this._dt = dt;
            this._value = value;
        }

        /// Return elements
        public DateTime Timestamp
        {
            get { return (this._dt); }
            set { this._dt = value; }
        }

        public string Value
        {
            get { return (this._value); }
            set { this._value = value; }
        }
    }

    public class DataValues
    {
        /// <summary>
        /// The DataValues Class is the "trace" based entity.  It represents a full column of data, with some metadata attached.  The metadata consists of a
        /// trace name, the traces units (which are not used at this stage, but needs to be added for general use), and a application flag which keeps track
        /// of the source of the data column - raw for original, read-in data, and calculated for computed data.
        /// </summary>
        private string _name;
        private string _units;
        private bool _raw;
        private List<DataValue> _values;

        // Creators
        public DataValues()
        {
            this._name = string.Empty;
            this._units = string.Empty;
            this._raw = true;
            this._values = null;
        }

        // API
        public DataValues(string name, string units, List<DataValue> values)
        {
            this._name = name;
            this._units = units;
            this._raw = true;
            this._values = values;
        }

        // Return Values
        public string Name
        {
            get { return (this._name); }
            set { this._name = value; }
        }

        public string Units
        {
            get { return (this._units); }
            set { this._units = value; }
        }

        public bool Raw
        {
            get { return (this._raw); }
            set { this._raw = value; }
        }

        public List<DataValue> DataColumn
        {
            get { return (this._values); }
            set { this._values = value; }
        }
    }

    public class Data
    {
        /// <summary>
        /// This is the root of the data structure.  It is simply a list of available traces.
        /// </summary>
        private List<DataValues> _data;

        //Creators
        public Data()
        {
            this._data = null;
        }

        // API
        public Data(List<DataValues> data)
        {
            this._data = data;
        }

        //Return Values
        public List<DataValues> DataList
        {
            get { return (this._data); }
            set { this._data = value; }
        }
    }
}
