using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public enum IDnums { one, two };

    public class ItemAlreadyExeistExeption : Exception
    {
        public readonly Type ItemType;
        public readonly int Id;
        public readonly int SecId;
        private IDnums idnums;
        public ItemAlreadyExeistExeption(Type t, int id, string message) : base(message) { ItemType = t; Id = id; idnums = IDnums.one; }
        public ItemAlreadyExeistExeption(Type t, int id) { ItemType = t; Id = id; idnums = IDnums.one; }
        public ItemAlreadyExeistExeption(Type t, int id, int secId) { ItemType = t; Id = id; SecId = secId; idnums = IDnums.two; }
        public override string ToString()
        {
            switch (idnums)
            {
                case (IDnums.one):
                {
                    return $"ERROR: the {Id} {ItemType} number already exeist at the datasource.";
                }
                case IDnums.two:
                {
                        return $"ERROR: the {ItemType} with the numbers {Id} and {SecId} already exeist.";
                }
                default:
                    return "";
            }
            
        }
    }

    public class ItemNotExeistExeption : Exception
    {
        public readonly Type ItemType;
        public readonly int Id;
        public readonly int SecId;
        private IDnums idnums;

        public ItemNotExeistExeption(Type t, int id, string message) : base(message) { ItemType = t; Id = id; idnums = IDnums.one; }
        public ItemNotExeistExeption(Type t, int id) { ItemType = t; Id = id; idnums = IDnums.one; }
        public ItemNotExeistExeption(Type t, int id, int secId) { ItemType = t; Id = id; SecId = secId; idnums = IDnums.two; }

        public override string ToString()
        {
            switch (idnums)
            {
                case (IDnums.one):
                    {
                        return $"ERROR: the {Id} {ItemType} number didn't exeist at the datasource.";

                    }
                case IDnums.two:
                    {
                        return $"ERROR: the {ItemType} with the numbers {Id} and {SecId} didn't exeist.";
                    }
                default:
                    return "";
            }
        }
    }

    public class BadActionExeption : Exception
    {
        public readonly Type ItemType;

        public BadActionExeption(Type t, string message) : base(message) => ItemType = t;
        public BadActionExeption(Type t) => ItemType = t;

        public override string ToString()
        {
            return $"ERROR! the Action is not adapt to {ItemType}";
        }
    }

}
