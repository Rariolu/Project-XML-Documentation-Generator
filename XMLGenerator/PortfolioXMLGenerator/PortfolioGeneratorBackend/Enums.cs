using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioGeneratorBackend
{
    /// <summary>
    /// An enum representing the protection level of a member or data type.
    /// </summary>
    public enum PROTECTION
    {
        PRIVATE,
        PROTECTED,
        PUBLIC,
        INTERNAL,
        PROTECTED_INTERNAL,
        PRIVATE_PROTECTED
    }

    /// <summary>
    /// An enumeration of the different kinds of members in a class or struct.
    /// </summary>
    public enum MEMBER_TYPE
    {
        VARIABLE,
        METHOD,
        PROPERTY,
        CONSTRUCTOR
    }
}
