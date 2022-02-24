﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioGeneratorBackend
{
    public class ParsedAssembly
    {
        string name;
        public string Name
        {
            get
            {
                return name;
            }
        }

        Dictionary<string, ParsedType> parsedTypes;
        public ParsedType[] ParsedTypes
        {
            get
            {
                return parsedTypes.Values.ToArray();
            }
        }

        public ParsedAssembly(string _name)
        {
            name = _name;
            parsedTypes = new Dictionary<string, ParsedType>();
        }

        public void AddType(ParsedType type)
        {
            parsedTypes.Add(type.FullName, type);
        }

        public static bool operator ==(ParsedAssembly parsedAssembly1, ParsedAssembly parsedAssembly2)
        {
            if (parsedAssembly1.Name != parsedAssembly2.Name)
            {
                return false;
            }

            if (parsedAssembly1.ParsedTypes.Length != parsedAssembly2.ParsedTypes.Length)
            {
                return false;
            }

            foreach (string typeKey1 in parsedAssembly1.parsedTypes.Keys)
            {
                if (!parsedAssembly2.parsedTypes.ContainsKey(typeKey1))
                {
                    return false;
                }

                ParsedType type1 = parsedAssembly1.parsedTypes[typeKey1];
                ParsedType type2 = parsedAssembly2.parsedTypes[typeKey1];

                if (type1 != type2)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool operator !=(ParsedAssembly parsedAssembly1, ParsedAssembly parsedAssembly2)
        {
            return !(parsedAssembly1 == parsedAssembly2);
        }
    }
}