using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace XNAGameLib2D
{
    public class SingleInstance<T> where T : new()
    {
        private T current;

        public T Current
        {
            get
            {
                if (current == null)
                    current = new T();

                return current;
            }
        }
    }
}
