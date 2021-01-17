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
        string BusOnTripPath = @"BusOnTripXml.xml";
        string stationPath = @"StationXml.xml";

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

        #region BusOnTrip

        BusOnTrip GetBusOnTrip(int id)
        {
            XElement BusOnTripElement = XMLTools.LoadListFromXMLElement(BusOnTripPath);

            BusOnTrip busOnTrip = (from b in BusOnTripElement.Elements()
                       where int.Parse(b.Element("Id").Value) == id
                       select new BusOnTrip()
                       {
                           Id = int.Parse(b.Element("Id").Value),
                           Depart_Planned = TimeSpan.Parse(b.Element("Depart_Planned").Value),
                           LicenceNum = int.Parse(b.Element("LicenceNum").Value),
                           LineID = int.Parse(b.Element("LineID").Value),
                           PrevStation = int.Parse(b.Element("PrevStation").Value),
                           Real_Depart = TimeSpan.Parse(b.Element("Real_Depart").Value),
                           Timeto_NextStation = TimeSpan.Parse(b.Element("Timeto_NextStation").Value),
                           Timeto_PrevStation = TimeSpan.Parse(b.Element("Timeto_PrevStation").Value)
                       }
                        ).FirstOrDefault();

            if (busOnTrip == null)
                throw new ItemNotExeistExeption(typeof(Bus), id);

            return busOnTrip;
        }
        IEnumerable<BusOnTrip> GetAllBusesOnTrip()
        {

            XElement BusOnTripElement = XMLTools.LoadListFromXMLElement(BusOnTripPath);

            return  from b in BusOnTripElement.Elements()
                    select (BusOnTrip)b.xElementToItem(typeof(BusOnTrip));  

        }
        IEnumerable<BusOnTrip> GetAllBusesOnTripBy(Predicate<BusOnTrip> predicate)
        {
            XElement BusOnTripElement = XMLTools.LoadListFromXMLElement(BusOnTripPath);

            return  from b in BusOnTripElement.Elements()
                    let bOT = (BusOnTrip) b.xElementToItem(typeof(BusOnTrip))
                    where(predicate(bOT))
                    select bOT;


        }
        void AddBusOnTrip(BusOnTrip busOnTrip)
        {
            XElement busOnTripElement = XMLTools.LoadListFromXMLElement(BusOnTripPath);

            XElement bOT = (from b in busOnTripElement.Elements()
                           where int.Parse(b.Element("Id").Value) == busOnTrip.Id
                           select b).FirstOrDefault();

            if (bOT != null)
                throw new ItemAlreadyExeistExeption(typeof(BusOnTrip), busOnTrip.Id);

            busOnTripElement.Add(busOnTrip.itemToXElement());

            XMLTools.SaveListToXMLElement(busOnTripElement, BusOnTripPath);
        }
        void UpdateBusOnTrip(BusOnTrip busOnTrip)
        {
            XElement busOnTripElement = XMLTools.LoadListFromXMLElement(BusOnTripPath);

            XElement bOT = (from b1 in busOnTripElement.Elements()
                          where int.Parse(b1.Element("Id").Value) == busOnTrip.Id
                          select b1).FirstOrDefault();

            if (bOT != null)
            {
                bOT.Remove();
                busOnTripElement.Add(busOnTrip.itemToXElement());
                XMLTools.SaveListToXMLElement(busOnTripElement, BusOnTripPath);
            }
            else
                throw new ItemNotExeistExeption(typeof(BusOnTrip), busOnTrip.Id);
        }
        void UpdateBusOnTrip(int id, Action<BusOnTrip> update) //method that knows to updt specific fields
        {
            XElement BusOnTripElement = XMLTools.LoadListFromXMLElement(BusOnTripPath);

            XElement bOPxElement = (from b in BusOnTripElement.Elements()
                                    where int.Parse(b.Element("Id").Value) == id
                                    select b).FirstOrDefault();
            if (bOPxElement == null)
                throw new ItemNotExeistExeption(typeof(BusOnTrip), id);

            var busOnTrip = (BusOnTrip)bOPxElement.xElementToItem(typeof(BusOnTrip));
            update(busOnTrip);

            bOPxElement.Remove();
            BusOnTripElement.Add(busOnTrip.itemToXElement());
            XMLTools.SaveListToXMLElement(BusOnTripElement, BusOnTripPath);
        }
        void DeleteBusOnTrip(int id)
        {
            XElement busOnTripElement = XMLTools.LoadListFromXMLElement(BusOnTripPath);

            XElement b = (from b1 in busOnTripElement.Elements()
                          where int.Parse(b1.Element("Id").Value) == id
                          select b1).FirstOrDefault();

            if (b != null)
            {
                b.Remove();

                XMLTools.SaveListToXMLElement(busOnTripElement, BusOnTripPath);
            }
            else
                throw new ItemNotExeistExeption(typeof(BusOnTrip), id);
        }

        #endregion

        #region Station

        IEnumerable<Station> GetAllStationes()
        {
            XElement stationElement = XMLTools.LoadListFromXMLElement(stationPath);

            return from S in stationElement.Elements()
                   select (Station)S.xElementToItem(typeof(Station));
        }
        IEnumerable<Station> GetAllStationesBy(Predicate<Station> predicate)
        {
            XElement stationElement = XMLTools.LoadListFromXMLElement(stationPath);

            return from S in stationElement.Elements()
                   let st = (Station)S.xElementToItem(typeof(Station))
                   where (predicate(st))
                   select st;
        }
        Station GetStation(int code)
        {
            XElement stationsElement = XMLTools.LoadListFromXMLElement(stationPath);

            Station station = (from s in stationsElement.Elements()
                                   where int.Parse(s.Element("Code").Value) == code
                                   select (Station)s.xElementToItem(typeof(Station))).FirstOrDefault();

            if (station == null)
                throw new ItemNotExeistExeption(typeof(Station), code);

            return station;
        }
        void AddStation(Station station)
        {
            XElement stationElement = XMLTools.LoadListFromXMLElement(stationPath);

            XElement stationXElem = (from S in stationElement.Elements()
                            where int.Parse(S.Element("Code").Value) == station.Code
                            select S).FirstOrDefault();

            if (stationXElem != null)
                throw new ItemAlreadyExeistExeption(typeof(Station), station.Code);

            stationElement.Add(station.itemToXElement());

            XMLTools.SaveListToXMLElement(stationElement, stationPath);
        }
        void UpdateStation(Station station)
        {
            XElement stationElement = XMLTools.LoadListFromXMLElement(stationPath);

            XElement SxElement = (from s in stationElement.Elements()
                            where int.Parse(s.Element("Code").Value) == station.Code
                            select s).FirstOrDefault();

            if (SxElement != null)
            {
                SxElement.Remove();
                stationElement.Add(station.itemToXElement());
                XMLTools.SaveListToXMLElement(stationElement, stationPath);
            }
            else
                throw new ItemNotExeistExeption(typeof(Station), station.Code);
        }
        void UpdateStation(int code, Action<Station> update) //method that knows to updt specific fields
        {
            XElement stationElement = XMLTools.LoadListFromXMLElement(stationPath);

            XElement sxElement = (from s in stationElement.Elements()
                                  where int.Parse(s.Element("Code").Value) == station.Code
                                  select s).FirstOrDefault();

            if (sxElement == null)
                throw new ItemNotExeistExeption(typeof(Station), code);

            var station = (Station)sxElement.xElementToItem(typeof(Station));
            update(station);

            sxElement.Remove();
            stationElement.Add(station.itemToXElement());
            XMLTools.SaveListToXMLElement(stationElement, stationPath);
        }
        void DeleteStation(int code)
        {
            XElement stationElement = XMLTools.LoadListFromXMLElement(stationPath);

            XElement s = (from s1 in stationElement.Elements()
                          where int.Parse(s1.Element("Code").Value) == code
                          select s1).FirstOrDefault();

            if (s != null)
            {
                s.Remove();

                XMLTools.SaveListToXMLElement(stationElement, stationPath);
            }
            else
                throw new ItemNotExeistExeption(typeof(Station), code);
        }

        #endregion

    }
}