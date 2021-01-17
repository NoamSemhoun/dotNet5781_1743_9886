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
        string LinePath = @"LineXml.xml"; //XElement



        #endregion


        #region Bus

        public Bus GetBus(int licenseNum)
        {
            XElement BusElement = XMLTools.LoadListFromXMLElement(BusPath);

            Bus bus = (from b in BusElement.Elements()
                       where int.Parse(b.Element("LicenseNum").Value) == licenseNum
                       select new Bus()
                       {
                           LicenseNum = int.Parse(b.Element("LicenseNum").Value),
                           StartDate = DateTime.Parse(b.Element("StartDte").Value),
                           Total_Km = int.Parse(b.Element("Total_Km").Value),
                           Fuel = int.Parse(b.Element("Fuel").Value),
                           Status = (BusStatus)Enum.Parse(typeof(BusStatus), b.Element("Status").Value)
                       }
                        ).FirstOrDefault();

            if (bus == null)
                throw new ItemNotExeistExeption(typeof(Bus), licenseNum);

            return bus;

        }

        public IEnumerable<Bus> GetAllBus()
        {
            XElement BusElement = XMLTools.LoadListFromXMLElement(BusPath);

            return (from b in BusElement.Elements()

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
                   select b1;

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
                throw new ItemNotExeistExeption(typeof(Bus), bus.LicenseNum);
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
                throw new ItemNotExeistExeption(typeof(Bus), licenseNum);

        }

        #endregion


        #region Line


        public Line GetLine(int id)
        {
            XElement LineElement = XMLTools.LoadListFromXMLElement(LinePath);

            Line line = (from line1 in LineElement.Elements()
                         where int.Parse(line1.Element("LineID").Value) == id
                         select new Line()
                         {
                             LineID = int.Parse(line1.Element("LineID").Value),
                             LineNumber = int.Parse(line1.Element("LineNumber").Value),
                             FirstStation = int.Parse(line1.Element("FirstStation").Value),
                             LastStation = int.Parse(line1.Element("LastStation").Value),
                             Area = (Areas)Enum.Parse(typeof(Areas), line1.Element("Area").Value)  //  LIVDOKKKK 
                         }
                        ).FirstOrDefault();

            if (line == null)
                throw new ItemNotExeistExeption(typeof(Line), id);

            return line;

        }

        public IEnumerable<Line> GetAllLines()
        {
            XElement LineElement = XMLTools.LoadListFromXMLElement(LinePath);

            return (from line1 in LineElement.Elements()

                    select new Line()
                    {
                        LineID = int.Parse(line1.Element("LineID").Value),
                        LineNumber = int.Parse(line1.Element("LineNumber").Value),
                        FirstStation = int.Parse(line1.Element("FirstStation").Value),
                        LastStation = int.Parse(line1.Element("LastStation").Value),
                        Area = (Areas)Enum.Parse(typeof(Areas), line1.Element("Area").Value)  //  LIVDOKKKK 
                    }
                        );

        }


        public IEnumerable<Line> GetAllLinesBy(Predicate<Line> predicate)
        {
            XElement LineElement = XMLTools.LoadListFromXMLElement(LinePath);

            return from line1 in LineElement.Elements()
                   let l = new Line()
                   {
                       LineID = int.Parse(line1.Element("LineID").Value),
                       LineNumber = int.Parse(line1.Element("LineNumber").Value),
                       FirstStation = int.Parse(line1.Element("FirstStation").Value),
                       LastStation = int.Parse(line1.Element("LastStation").Value),
                       Area = (Areas)Enum.Parse(typeof(Areas), line1.Element("Area").Value)  //   
                   }
                   where predicate(l)
                   select l;


        }


        public void AddLine(Line line)
        {
            XElement LineElement = XMLTools.LoadListFromXMLElement(LinePath);

            XElement line1 = (from l in LineElement.Elements()
                              where int.Parse(l.Element("LineID").Value) == line.LineID
                              select l).FirstOrDefault();

            if (line1 != null)
                throw new ItemAlreadyExeistExeption(typeof(Line), line.LineID);

            XElement LineElem = new XElement("Line",
                                  new XElement("LineID", line.LineID),
                                  new XElement("LineNumber", line.LineNumber),
                                  new XElement("FirstStation", line.FirstStation),
                                  new XElement("LastStation", line.LastStation),
                                  new XElement("Area", line.Area.ToString()));

            LineElement.Add(LineElem);

            XMLTools.SaveListToXMLElement(LineElement, LinePath);

        }

        public void UpdateLine(Line line)
        {
            XElement LineElement = XMLTools.LoadListFromXMLElement(LinePath);

            XElement line1 = (from p in LineElement.Elements()
                              where int.Parse(p.Element("LineID").Value) == line.LineID  //  LIVDOKKKK with LINE NUMBER 
                              select p).FirstOrDefault();

            if (line1 != null)
            {
                line1.Element("LineID").Value = line.LineID.ToString();
                line1.Element("LineNumber").Value = line.LineNumber.ToString();
                line1.Element("FirstStation").Value = line.FirstStation.ToString();
                line1.Element("LastStation").Value = line.LastStation.ToString();
                line1.Element("Area").Value = line.Area.ToString();

                XMLTools.SaveListToXMLElement(LineElement, LinePath);
            }
            else
                throw new ItemNotExeistExeption(typeof(Line), line.LineID);
        }


        public void UpdateLine(int id, Action<Bus> update) //method that knows to updt specific fields in Person
        {
            throw new NotImplementedException();

        }


        public void DeleteLine(int id)
        {
            XElement LineElement = XMLTools.LoadListFromXMLElement(LinePath);

            XElement line1 = (from p in LineElement.Elements()
                              where int.Parse(p.Element("LineID").Value) == id
                              select p).FirstOrDefault();

            if (line1 != null)
            {
                line1.Remove();

                XMLTools.SaveListToXMLElement(LineElement, LinePath);
            }
            else
                throw new ItemNotExeistExeption(typeof(Line), id);

        }

        #endregion




    }
}