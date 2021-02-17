using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DL
{
    class XmlCRUD
    {
        public static bool isExists<T>(string path, T obj, params string[] idsProps)
        {
            XElement rootElement = XMLTools.LoadListFromXMLElement(path);

            
            
            foreach (XElement e in rootElement.Elements())
            {
                bool exists = true;
                for (int i = 0; i < idsProps.Count(); i++)
                {
                    PropertyInfo prop = obj.GetType().GetProperty(idsProps[i]);
                    if (e.Element(idsProps[i]).Value != (string)prop.GetValue(obj))
                        exists =  false;
                }
                if (exists)
                    return true;
            }
            return false;
        }

        public static T Get<T>(string path ,int[] ids , params string[] idsProps)
        {
            XElement rootElement = XMLTools.LoadListFromXMLElement(path);

            T obj = (from item in rootElement.Elements()
                     where isEqual(item, ids, idsProps)
                     select (T)item.xElementToItem(typeof(T))).FirstOrDefault();

            if (obj == null)
                throw new DO.ItemNotExeistExeption(typeof(T), ids[0]);

            return obj;
        }

        public static bool isEqual(XElement element, int[] ids, params string[] idsProps)
        {
            for(int i = 0; i < idsProps.Count(); i++)
            {
                if (ids[i].ToString() != element.Element(idsProps[i]).Value)
                    return false;
            }
            return true;
        }

        public static void Add<T>(string path, T obj, params string[] idsProps)
        {
            XElement rootElement = XMLTools.LoadListFromXMLElement(path);

            int[] ids = new int[idsProps.Count()];

            for (int i = 0; i < idsProps.Count(); i++)
            {
                ids[i] = (int)obj.GetType().GetProperty(idsProps[i]).GetValue(obj);
            }

            try
            {
                Get<T>(path, ids, idsProps);
                throw new DO.ItemAlreadyExeistExeption(typeof(T), ids[0]);
            }
            catch {}

            rootElement.Add(obj.itemToXElement());
            XMLTools.SaveListToXMLElement(rootElement, path);
        }

        public static IEnumerable<T> GetAll<T>(string path)
        {
            XElement rootElement = XMLTools.LoadListFromXMLElement(path);

            return from item in rootElement.Elements()
                   select (T)item.xElementToItem(typeof(T));
        }
    
        public static IEnumerable<T> GetAllBy<T>(string path, Predicate<T> predicate)
        {
            XElement rootElement = XMLTools.LoadListFromXMLElement(path);

            return from elem in rootElement.Elements()
                   let obj = (T)elem.xElementToItem(typeof(T))
                   where predicate(obj)
                   select obj;
        }

        public static void Remove<T> (string path, int[] ids, params string[] idsProps)
        {
            XElement rootElement = XMLTools.LoadListFromXMLElement(path);
            
            XElement element = (from item in rootElement.Elements()
                                where isEqual(item, ids, idsProps)
                                select item).FirstOrDefault();

            if (element == null)
                throw new DO.ItemNotExeistExeption(typeof(T), ids[0]);

            element.Remove();

            XMLTools.SaveListToXMLElement(rootElement, path);
        }

        public static void Update<T>(string path, T obj, params string[] idsProps)
        {
            XElement rootElement = XMLTools.LoadListFromXMLElement(path);

            int[] ids = new int[idsProps.Count()];

            for (int i = 0; i < idsProps.Count(); i++)
            {
                ids[i] = (int)obj.GetType().GetProperty(idsProps[i]).GetValue(obj);
            }

            XElement element = (from item in rootElement.Elements()
                                where isEqual(item, ids, idsProps)
                                select item).FirstOrDefault();

            if (element == null)
                throw new DO.ItemNotExeistExeption(typeof(T), ids[0]);

            element.Remove();

            rootElement.Add(obj.itemToXElement());
            XMLTools.SaveListToXMLElement(rootElement, path);
        }

        public static void Update<T>(string path, Action<T> action, int[] ids, params string[] idsProps)
        {
            T obj = Get<T>(path, ids, idsProps);

            action(obj);

            Update<T>(path, obj, idsProps);
        }


    }
}
