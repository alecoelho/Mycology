using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Micologia.Helper.DTO
{
    public class Singleton
    {
        private Singleton obj;

        private Singleton()
        { 
        }

        public Singleton getInstance()
        {
            if (obj == null)
                obj = new Singleton();

            return obj;
        }
    }

}
