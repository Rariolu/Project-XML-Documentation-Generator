using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioGeneratorBackend
{
    public abstract class ParseMemberParent
    {
        string name = "";
        public string Name
        {
            get
            {
                return name;
            }
        }

        TypeStruct type;
        public TypeStruct Type
        {
            get
            {
                return type;
            }
        }

        public string Description { get; set; }

        bool isStatic;
        public bool IsStatic
        {
            get
            {
                return isStatic;
            }
        }

        public ParseMemberParent(string _name, string _type, string _namespace, bool _isStatic = false)
        {
            name = _name;
            type = new TypeStruct();
            type.TypeName = _type;
            type.Namespace = _namespace;
            isStatic = _isStatic;
        }
    }
}
