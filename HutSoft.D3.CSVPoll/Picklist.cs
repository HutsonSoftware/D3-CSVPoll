using System;
using System.Collections.Generic;

namespace HutSoft.D3.CSVPoll
{
    class Picklist
    {
        private string _value;
        private string _name;

        public Picklist(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value.Replace("\"", "");
            }
        }

        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value.Replace("\"", "");
            }
        }

        public string NameValue
        {
            get
            {
                return string.Format("{0}{1}", Name, Value);
            }
        }
    }

    class PicklistComparer : IEqualityComparer<Picklist>
    {
        public bool Equals(Picklist x, Picklist y)
        {
            if (Object.ReferenceEquals(x, y))
                return true;

            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            return x.Name == y.Name && x.Value == y.Value;
        }

        public int GetHashCode(Picklist picklist)
        {
            if (Object.ReferenceEquals(picklist, null))
                return 0;

            int hashPicklistName = picklist.Name == null ? 0 : picklist.Name.GetHashCode();
            int hashPicklistValue = picklist.Value == null ? 0 : picklist.Value.GetHashCode();

            return hashPicklistName ^ hashPicklistValue;
        }
    }
}
