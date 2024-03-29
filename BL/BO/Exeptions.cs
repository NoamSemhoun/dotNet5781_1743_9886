﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace BO
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

        //    Type type;
        //private object obj;
        //public ItemAlreadyExeistExeption(object o, string[]  props){ obj = o; type = o.GetType(); }

        //public override string ToString()
        //{
        //    string returnString = $"the Item {type} with the data:\n ";
        //    foreach (PropertyInfo prop in type.GetProperties())
        //    {
        //        returnString += $"{prop.Name}: {prop.GetValue(obj)}\n";
        //    }
        //    return returnString + "already exeist";
        //}

    


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


    public class AddLineExeption: Exception
    {
        public class StationAdjMissNumbers
        {
            public int Station1, Station2;
        }


        public List<int> StationMiss { get; }
        public List<StationAdjMissNumbers> StationAdjMisses { get; }

        public AddLineExeption(List<int> SML, List<StationAdjMissNumbers> SAMNL) { StationMiss = SML; StationAdjMisses = SAMNL; }

        public override string ToString()
        {
            string str = "";
            if (StationMiss.Count != 0)
            {
                str += "the stations with the Code numbers:\n";
                foreach (int i in StationMiss)
                    str += $"{i}, ";
                str += "did not exeist\n";
            }
            if(StationAdjMisses.Count != 0)
            {
                str += "the follow Station did not have adj Station data:";
                foreach (StationAdjMissNumbers sA in StationAdjMisses)
                    str += $"station 1: {sA.Station1}, station 2: {sA.Station2}\n";
            }
            return str;
        }

    }

    

    public class LackOfDataExeption : Exception
    {
        public DataType Data{ get; }
        public int[] id { get; }
        public LackOfDataExeption (DataType t, params int[] i) {Data = t; id = i; }
        public LackOfDataExeption (DataType t, string message, params int[] i):base(message) {Data = t; id = i; }

        public override string ToString()
        { 
            return $"the {Data} with the id {id} is missing";
        }

    }


        //public class BadActionExeption : Exception
        //{
        //    public readonly Type ItemType;

    //    public BadActionExeption(Type t, string message) : base(message) => ItemType = t;
    //    public BadActionExeption(Type t) => ItemType = t;

    //    public override string ToString()
    //    {
    //        return $"ERROR! the Action is not adapt to {ItemType}";
    //    }
    //}


}
