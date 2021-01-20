using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DL
{
    class XmlStatics
    {
        static private string path = @"StaticsXml.xml";

        static public int GetNextId(string typeName)
        {
            XElement root = XMLTools.LoadListFromXMLElement(path);

            if(root.Elements(typeName).Any())
            {
                XElement _static = root.Element(typeName);
                int returnValue = int.Parse(_static.Value);
                _static.Value = (returnValue + 1).ToString();
                XMLTools.SaveListToXMLElement(root, path);
                return returnValue;
            }
            else
            {
                root.Add(new XElement(typeName, 2));
                XMLTools.SaveListToXMLElement(root, path);
                return 1;
            }
        }    
    }

}
