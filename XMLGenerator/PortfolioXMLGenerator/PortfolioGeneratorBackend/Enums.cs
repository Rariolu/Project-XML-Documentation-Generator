using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioGeneratorBackend
{
    public enum PROTECTION
    {
        PRIVATE,
        PROTECTED,
        PUBLIC,
        INTERNAL,
        PROTECTED_INTERNAL,
        PRIVATE_PROTECTED
    }

    public enum MEMBER_TYPE
    {
        VARIABLE,
        METHOD,
        PROPERTY,
        CONSTRUCTOR
    }
}
