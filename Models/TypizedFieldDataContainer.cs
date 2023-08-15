using System;
using System.Collections.Generic;

namespace Nerdogramm.Models
{
    internal class TypizedFieldDataContainer
    {
        private Dictionary<string, TypizedFieldData> fieldsContainer;
        public TypizedFieldDataContainer(TypizedFieldData[] fields, object[] values)
        {
            fieldsContainer = new Dictionary<string, TypizedFieldData>();
            if (values.Length != fields.Length)
            {
                throw new Exception("Error with data");
            }
            for (int i = 0; i < fields.Length; i++)
            {
                fields[i].Value = values[i];
                fieldsContainer.Add(fields[i].Name, fields[i]);
            }
        }
        public dynamic this[string name]
        {
            get
            {
                if (fieldsContainer.TryGetValue(name, out var res))
                {
                    return res.Cast();
                }
                else
                {
                    return null;
                }
            }
        }
    }

    internal class TypizedFieldData
    {
        public string Name { get; set; }
        public Type TType { get; set; }
        public object Value { get; set; }
        public dynamic Cast()
        {
            return Convert.ChangeType(Value, TType);
        }
    }

}
