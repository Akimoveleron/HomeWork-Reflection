using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkReflection
{
    [Serializable]
    public  class F
    {
       
       private int i1, i2, i3, i4, i5;

        public int Property1 => i1;
        public int Property2 =>i2;
        public int Property3 =>i3;
        public int Property4 =>i4;
        public int Property5 =>i5;

        public static F Get() => new F() { i1=6, i2=7, i3=8, i4=9, i5=10};
    }
}
