using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    sealed class DLXml : IDAL    //internal
    {
        #region singelton
        static readonly DLXml instance = new DLXml();
        static DLXml() { }// static ctor to ensure instance init is done just before first usage
        DLXml() { } // default => private
        public static DLXml Instance { get => instance; }// The public Instance property to use
        #endregion

        #region DS XML Files

        string BusPath = @"BusXml.xml"; //XElement

     


        #endregion

        #region Bus

        public Bus GetBus(int licenseNum)
        {
            XElement BusElement = XMLTools.LoadListFromXMLElement(BusPath);

            Bus bus = (from b in BusElement.Elements()
                       where int.Parse(b.Element("LicenseNum").Value) == licenseNum
                       select  new Bus()
                       {
                            LicenseNum = int.Parse(b.Element("LicenseNum").Value) ,
                            StartDate = DateTime.Parse(b.Element("StartDte").Value),
                            Total_Km = int.Parse(b.Element("Total_Km").Value),
                            Fuel = int.Parse(b.Element("Fuel").Value),
                            Status = (BusStatus)Enum.Parse(typeof(BusStatus), b.Element("Status").Value)
                       }
                        ).FirstOrDefault();

            if (bus == null)
                throw new ItemNotExeistExeption(typeof(Bus) , licenseNum);

            return bus;

        }

        public IEnumerable<Bus> GetAllBus()
          {
            XElement BusElement = XMLTools.LoadListFromXMLElement(BusPath);

           return ( from b in BusElement.Elements()
                 
                   select new Bus()
                   {
                       LicenseNum = int.Parse(b.Element("LicenseNum").Value),
                       StartDate = DateTime.Parse(b.Element("StartDte").Value),
                       Total_Km = int.Parse(b.Element("Total_Km").Value),
                       Fuel = int.Parse(b.Element("Fuel").Value),
                       Status = (BusStatus)Enum.Parse(typeof(BusStatus), b.Element("Status").Value)
                   }
                   );
 
          }
        public IEnumerable<Bus> GetAllBusBy(Predicate<Bus> predicate)
        {
            XElement BusElement = XMLTools.LoadListFromXMLElement(BusPath);

            return from b in BusElement.Elements()
                    let b1 = new Bus()
                    {
                        LicenseNum = int.Parse(b.Element("LicenseNum").Value),
                        StartDate = DateTime.Parse(b.Element("StartDte").Value),
                        Total_Km = int.Parse(b.Element("Total_Km").Value),
                        Fuel = int.Parse(b.Element("Fuel").Value),
                        Status = (BusStatus)Enum.Parse(typeof(BusStatus), b.Element("Status").Value)
                    }
                    where predicate(b1)
                    select b1 ;

        }


        public void AddBus(Bus bus)
        {
            XElement busElement = XMLTools.LoadListFromXMLElement(BusPath);

            XElement b1 = (from b in busElement.Elements()
                           where int.Parse(b.Element("LicenseNum").Value) == bus.LicenseNum
                           select b).FirstOrDefault();

            if (b1 != null)
                throw new ItemAlreadyExeistExeption(typeof(Bus), bus.LicenseNum);

            XElement BusElem = new XElement("Bus",
                                  new XElement("LicenseNum", bus.LicenseNum),
                                  new XElement("Total_Km", bus.Total_Km),
                                  new XElement("Fuel", bus.Fuel),
                                  new XElement("StartDate", bus.StartDate),
                                  new XElement("Status", bus.Status.ToString()));

            busElement.Add(BusElem);

            XMLTools.SaveListToXMLElement(busElement, BusPath);

        }

          public void UpdateBus(Bus bus)
        {
            XElement busElement = XMLTools.LoadListFromXMLElement(BusPath);

            XElement b = (from p in busElement.Elements()
                            where int.Parse(p.Element("LicenseNum").Value) == bus.LicenseNum
                            select p).FirstOrDefault();

            if (b != null)
            {
                b.Element("LicenseNum").Value = bus.LicenseNum.ToString();

                b.Element("Total_Km").Value = bus.Total_Km.ToString();
                b.Element("Fuel").Value = bus.Fuel.ToString();
                b.Element("StartDate").Value = bus.StartDate.ToString();
                b.Element("Status").Value = bus.Status.ToString();

                XMLTools.SaveListToXMLElement(busElement, BusPath);
            }
            else
                throw new ItemNotExeistExeption(typeof(Bus), bus.LicenseNum) ;
        }


          public void UpdateBus(int licenseNum, Action<Bus> update) //method that knows to updt specific fields in Person
        {
            throw new NotImplementedException();

        }


        public void DeleteBus(int licenseNum)
        {
            XElement busElement = XMLTools.LoadListFromXMLElement(BusPath);

            XElement b = (from p in busElement.Elements()
                            where int.Parse(p.Element("LicenseNum").Value) == licenseNum
                            select p).FirstOrDefault();

            if (b != null)
            {
                b.Remove();

                XMLTools.SaveListToXMLElement(busElement, BusPath);
            }
            else
                throw new ItemNotExeistExeption(typeof(Bus), licenseNum) ;

        }

      #endregion





    }
}
