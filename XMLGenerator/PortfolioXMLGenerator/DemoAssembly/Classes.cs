using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoAssembly
{
    public class ClassA
    {
        private int priv = 4;
        public bool pub = true;
        private protected int privProt = 42;
        protected internal string protIntern = "24";

        static int privStatic = 4;
        public static bool pubStatic = true;
        private protected static int privProtStatic = 42;
        protected internal static string protInternStatic = "24";
    }

    public class ClassB<T>
    {
        IEnumerator Enumerator()
        {
            throw new NotImplementedException();
        }

        public void ListTest(List<int> list)
        {

        }

        public void DictTest(Dictionary<int, string> dict)
        {

        }
    }

    public class ClassC<T, U>
    {
        public T DemoTFunction()
        {
            throw new NotImplementedException();
        }
        public U DemoUFunction()
        {
            throw new NotImplementedException();
        }
    }

    public class ClassD<T, U, V>
    {
        public T Bloopy
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }

    public class ClassE<T, U, V, W, X, Y, Z, A, B, C>
    {

    }
}
