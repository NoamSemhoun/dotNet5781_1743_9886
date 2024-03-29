﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DL
{
    public static class ConvertTo
    {
        public static object xElementToItem(this XElement xElement, Type type)
        {
            object item = Activator.CreateInstance(type);
            foreach (PropertyInfo prop in type.GetProperties())
            {
                if (!xElement.Elements(prop.Name).Any())
                    continue;

                if (prop.PropertyType.IsEnum)
                {
                    prop.SetValue(item, Enum.Parse(prop.PropertyType, xElement.Element(prop.Name).Value));
                  //*********************//**********************//**
                }
                else if(prop.PropertyType.Name ==  "TimeSpan")
                {
                    prop.SetValue(item, TimeSpan.Parse(xElement.Element(prop.Name).Value));
                }
                else if (prop.PropertyType.Name == typeof(double).Name) 
                {
                    prop.SetValue(item, double.Parse(xElement.Element(prop.Name).Value));
                }
                else
                {
                    try { prop.SetValue(item, Convert.ChangeType(xElement.Element(prop.Name).Value, prop.PropertyType).ToString() ); }    //  CultureInfo.InvariantCulture ?
                    catch  { throw new Exception($"ERROR! could not convert the property type {prop.PropertyType.Name}"); }
                }
            }
            return item;
        }

        public static XElement itemToXElement(this object item)
        {
            Type type = item.GetType();
            XElement root = new XElement(type.Name);

            foreach (PropertyInfo prop in type.GetProperties())
            {
                root.Add(new XElement(prop.Name, prop.GetValue(item).ToString()));
            }

            return root;
        }
    }
}
