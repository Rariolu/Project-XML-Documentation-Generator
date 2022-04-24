using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioGeneratorBackend
{
    public class ParsedEnum : ParseMemberParent
    {
        //TypeStruct typeDef;
        //public TypeStruct Type
        //{
        //    get
        //    {
        //        return typeDef;
        //    }
        //}

        Dictionary<string, EnumValue> values = new Dictionary<string, EnumValue>();
        public KeyValuePair<string, EnumValue>[] Values
        {
            get
            {
                return values.ToArray();
            }
        }

        public ParsedEnum(string name, string _namespace)
            : base(name, name, _namespace)
        {
            //typeDef = new TypeStruct();
            //typeDef.TypeName = name;
            //typeDef.Namespace = _namespace;
        }

        public void AddValue(string name, int val)
        {
            values.Add(name, new EnumValue(name, val));
        }

        public bool HasValue(string name, out EnumValue enumValue)
        {
            if (values.ContainsKey(name))
            {
                enumValue = values[name];
                return true;
            }
            enumValue = null;
            return false;
        }

        //public bool HasValue(string name, out int val)
        //{
        //    if (values.ContainsKey(name))
        //    {
        //        val = values[name];
        //        return true;
        //    }
        //    val = -1;
        //    return false;
        //}

        public static bool operator==(ParsedEnum enum1, ParsedEnum enum2)
        {
            bool typesUnequal = enum1.Type != enum2.Type;
            //TODO: Add protection levels
            if (typesUnequal)
            {
                return false;
            }

            if (enum1.values.Count != enum2.values.Count)
            {
                return false;
            }

            foreach(string enum1Key in enum1.values.Keys)
            {
                if (!enum2.values.ContainsKey(enum1Key))
                {
                    return false;
                }

                if (enum1.values[enum1Key] != enum2.values[enum1Key])
                {
                    return false;
                }
            }

            return true;
        }

        public static bool operator!=(ParsedEnum enum1, ParsedEnum enum2)
        {
            return !(enum1 == enum2);
        }
    }

    public class EnumValue
    {
        public string Name { get; private set; }
        public int Value { get; private set; }
        public string Description { get; set; }

        public EnumValue(string name, int value, string description="")
        {
            Name = name;
            Value = value;
            Description = description;
        }
    }
}