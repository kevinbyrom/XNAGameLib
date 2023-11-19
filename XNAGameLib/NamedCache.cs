using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace XNAGameLib2D
{
    public class NamedCache<T> 
    {
        private Dictionary<string, T> cache;


        #region Constructors

        public NamedCache()
        {
            this.cache = new Dictionary<string,T>();
        }

        #endregion


        public bool Contains(string name)
        {
            return this.cache.ContainsKey(name.ToUpper());
        }


        public void Add(string name, T item)
        {
            this.cache.Add(name.ToUpper(), item);
        }


        public T Get(string name)
        {
            return this.cache[name.ToUpper()];
        }


        public void Remove(string name)
        {
            this.cache.Remove(name);
        }


        public void RemoveAll()
        {
            this.cache.Clear();
        }
    }
}
