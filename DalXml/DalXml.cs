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
    sealed class DalXml : IDAL    //internal
    {
        #region singelton
        static readonly DalXml instance = new DalXml();
        static DalXml() { }// static ctor to ensure instance init is done just before first usage
        DalXml() { } // default => private
        public static DalXml Instance { get => instance; }// The public Instance property to use
        #endregion

        #region DS XML Files

        string BusPath = @"BusXml.xml"; //XElement
        string LinePath = @"LineXml.xml"; //XElement
        string BusOnTripPath = @"BusOnTripXml.xml";
        string stationPath = @"StationXml.xml";
        string adjacentStationsPath = @"AdjacentStationXml.xml";
        string lineStationsPath = @"LineStationXml.xml";
        string lineTripPath = @"LineTripXml.xml";
        string userPath = @"UserXml.xml";

        #endregion


        #region Bus

        public Bus GetBus(int licenseNum)
        {
            return XmlCRUD.Get<Bus>(BusPath, new int[] { licenseNum }, "LicenseNum");
        }

        public IEnumerable<Bus> GetAllBus()
        {
            return XmlCRUD.GetAll<Bus>(BusPath);

        }
        public IEnumerable<Bus> GetAllBusBy(Predicate<Bus> predicate)
        {
            return XmlCRUD.GetAllBy(BusPath, predicate);
        }


        public void AddBus(Bus bus)
        {
            XmlCRUD.Add(BusPath, bus, "LicenseNum");
        }

        public void UpdateBus(Bus bus)
        {
            XmlCRUD.Update<Bus>(BusPath, bus, "LicenseNum");
        }


        public void UpdateBus(int licenseNum, Action<Bus> update) //method that knows to updt specific fields in Person
        {
            XmlCRUD.Update(BusPath, update, new int[] { licenseNum }, "LicenseNum");
        }


        public void DeleteBus(int licenseNum)
        {
            XmlCRUD.Remove<Bus>(BusPath, new int[] { licenseNum }, "LicenseNum");
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
            if (line.LineID != 0)
                throw new Exception("bad inpue exeption");//******************************//*****************************//***/*

            line.LineID = XmlStatics.GetNextId("Line");

            XmlCRUD.Add(LinePath, line, "LineID");    
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

        public void UpdateLine(int id, Action<Line> update) //method that knows to updt specific fields in Person
        {
            XmlCRUD.Update<Line>(LinePath, update, new int[] { id }, "LineID");
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

        public BusOnTrip GetBusOnTrip(int id)
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
        public IEnumerable<BusOnTrip> GetAllBusesOnTrip()
        {

            XElement BusOnTripElement = XMLTools.LoadListFromXMLElement(BusOnTripPath);

            return from b in BusOnTripElement.Elements()
                   select (BusOnTrip)b.xElementToItem(typeof(BusOnTrip));

        }
        public IEnumerable<BusOnTrip> GetAllBusesOnTripBy(Predicate<BusOnTrip> predicate)
        {
            XElement BusOnTripElement = XMLTools.LoadListFromXMLElement(BusOnTripPath);

            return from b in BusOnTripElement.Elements()
                   let bOT = (BusOnTrip)b.xElementToItem(typeof(BusOnTrip))
                   where (predicate(bOT))
                   select bOT;


        }
        public void AddBusOnTrip(BusOnTrip busOnTrip)
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
        public void UpdateBusOnTrip(BusOnTrip busOnTrip)
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
        public void UpdateBusOnTrip(int id, Action<BusOnTrip> update) //method that knows to updt specific fields
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
        public void DeleteBusOnTrip(int id)
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

        public IEnumerable<Station> GetAllStationes()
        {
            XElement stationElement = XMLTools.LoadListFromXMLElement(stationPath);

            return from S in stationElement.Elements()
                   select (Station)S.xElementToItem(typeof(Station));
        }
        public IEnumerable<Station> GetAllStationesBy(Predicate<Station> predicate)
        {
            XElement stationElement = XMLTools.LoadListFromXMLElement(stationPath);

            return from S in stationElement.Elements()
                   let st = (Station)S.xElementToItem(typeof(Station))
                   where (predicate(st))
                   select st;
        }
        public Station GetStation(int code)
        {
            XElement stationsElement = XMLTools.LoadListFromXMLElement(stationPath);

            Station station = (from s in stationsElement.Elements()
                               where int.Parse(s.Element("Code").Value) == code
                               select (Station)s.xElementToItem(typeof(Station))).FirstOrDefault();

            if (station == null)
                throw new ItemNotExeistExeption(typeof(Station), code);

            return station;
        }
        public void AddStation(Station station)
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
        public void UpdateStation(Station station)
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
        public void UpdateStation(int code, Action<Station> update) //method that knows to updt specific fields
        {
            XElement stationElement = XMLTools.LoadListFromXMLElement(stationPath);

            XElement sxElement = (from s in stationElement.Elements()
                                  where int.Parse(s.Element("Code").Value) == code
                                  select s).FirstOrDefault();

            if (sxElement == null)
                throw new ItemNotExeistExeption(typeof(Station), code);

            var station = (Station)sxElement.xElementToItem(typeof(Station));
            update(station);

            sxElement.Remove();
            stationElement.Add(station.itemToXElement());
            XMLTools.SaveListToXMLElement(stationElement, stationPath);
        }
        public void DeleteStation(int code)
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

        #region AdjacentStation

        public IEnumerable<AdjacentStation> GetAllAdjacentStations()
        {
            return XmlCRUD.GetAll<AdjacentStation>(adjacentStationsPath);
        }
        public IEnumerable<AdjacentStation> GetAllAdjacentStationsBy(Predicate<AdjacentStation> predicate)
        {
            return XmlCRUD.GetAllBy(adjacentStationsPath, predicate);
        }
        public AdjacentStation GetAdjacentStation(int station1, int station2)
        {
            return XmlCRUD.Get<AdjacentStation>(adjacentStationsPath, new int[] { station1, station2 }, "Statoin1", "Station2");
        }
        public void AddAdjacentStation(AdjacentStation adjacentStation)
        {
            XmlCRUD.Add<AdjacentStation>(adjacentStationsPath, adjacentStation, "Statoin1", "Station2");
        }
        public void UpdateAdjacentStation(AdjacentStation adjacentStation)
        {
            XmlCRUD.Update<AdjacentStation>(adjacentStationsPath, adjacentStation, "Statoin1", "Station2");
        }
        public void UpdateAdjacentStation(int station1, int station2, Action<AdjacentStation> update) //method that knows to updt specific fields
        {
            XmlCRUD.Update<AdjacentStation>(adjacentStationsPath, update, new int[] { station1, station2 }, "Statoin1", "Station2");
        }
        public void DeleteAdjacentStation(int station1, int station2)
        {
            XmlCRUD.Remove<AdjacentStation>(adjacentStationsPath, new int[] { station1, station2 }, "Statoin1", "Station2");
        }

        #endregion


        #region LineStation

        public IEnumerable<LineStation> GetAllLineStations()
        {
            return XmlCRUD.GetAll<LineStation>(lineStationsPath);
        }
        public IEnumerable<LineStation> GetAllLineStationsBy(Predicate<LineStation> predicate)
        {
            return XmlCRUD.GetAllBy(lineStationsPath, predicate);
        }
        public LineStation GetLineStation(int lineId, int stationId)
        {
            return XmlCRUD.Get<LineStation>(lineStationsPath, new int[] { lineId, stationId }, "LineId", "LineStationIndex");
        }
        public void AddLineStation(LineStation lineStation)
        {
            XmlCRUD.Add<LineStation>(lineStationsPath, lineStation, "LineId", "LineStationIndex");
        }
        public void UpdateLineStation(LineStation lineStation)
        {
            XmlCRUD.Update<LineStation>(lineStationsPath, lineStation, "LineId", "LineStationIndex");
        }
        //void UpdateLineStation(int id, Action<Line> update); //method that knows to updt specific fields
        public void DeleteLineStation(int lineId, int stationId)
        {
            XmlCRUD.Remove<LineStation>(lineStationsPath, new int[] { lineId, stationId }, "LineId", "LineStationIndex");
        }

        #endregion

        #region LineTrip

        public IEnumerable<LineTrip> GetAllLineTrips()
        {
            return XmlCRUD.GetAll<LineTrip>(lineTripPath);
        }
        public IEnumerable<LineTrip> GetAllLineTripsBy(Predicate<LineTrip> predicate)
        {
            return XmlCRUD.GetAllBy<LineTrip>(lineTripPath, predicate);
        }
        public LineTrip GetLineTrip(int id)
        {
            return XmlCRUD.Get<LineTrip>(lineTripPath, new int[] { id }, "Id");
        }
        public void AddLineTrip(LineTrip lineTrip)
        {
            XmlCRUD.Add<LineTrip>(lineTripPath, lineTrip, "Id");
        }
        public void UpdateLineTrip(LineTrip lineTrip)
        {
            XmlCRUD.Update<LineTrip>(lineTripPath, lineTrip, "Id");
        }
        //void UpdateLineStation(int id, Action<Line> update); //method that knows to updt specific fields
        public void DeleteLineTrip(int id)
        {
            XmlCRUD.Remove<LineTrip>(lineTripPath, new int[] { id }, "Id");
        }
        #endregion

        #region User

        public void AddUser(User user)
        {
            XElement UserElement = XMLTools.LoadListFromXMLElement(userPath);

            XElement us = (from elem in UserElement.Elements()
                           where elem.Element("UserName").Value == user.UserName
                           select elem
                        ).FirstOrDefault();

            if (us != null)
                throw new ItemAlreadyExeistExeption(typeof(User), 00);

            UserElement.Add(user.itemToXElement());
            XMLTools.SaveListToXMLElement(UserElement, userPath);
        }

        public void DeleteUser(string username)
        {
            XElement UserElement = XMLTools.LoadListFromXMLElement(userPath);

            XElement us = (from elem in UserElement.Elements()
                           where elem.Element("UserName").Value == username
                           select elem
                        ).FirstOrDefault();

            if (us != null)
            {
                us.Remove();

                XMLTools.SaveListToXMLElement(UserElement, userPath);
            }
            else
                throw new ItemNotExeistExeption(typeof(User), 00);
        }// Or ID

        public User GetUser(string name)
        {
            XElement UserElement = XMLTools.LoadListFromXMLElement(userPath);

            User us = (from elem in UserElement.Elements()
                       where elem.Element("UserName").Value == name
                       select (User)elem.xElementToItem(typeof(User))
                        ).FirstOrDefault();

            if (us == null)
                throw new ItemNotExeistExeption(typeof(User), 00);

            return us;
        }

        public IEnumerable<User> GetAllUsers()
        {
            XElement UserElement = XMLTools.LoadListFromXMLElement(userPath);

            return from u in UserElement.Elements()
                   select (User)u.xElementToItem(typeof(User));
        }

        public IEnumerable<User> GetAllUsersBy(Predicate<User> predicate)
        {
            XElement UserElement = XMLTools.LoadListFromXMLElement(userPath);

            return from u in UserElement.Elements()
                   let us = (User)u.xElementToItem(typeof(User))
                   where predicate(us)
                   select (User)u.xElementToItem(typeof(User));
        }

        public void UpdateUser(User user)
        {
            XElement UserElement = XMLTools.LoadListFromXMLElement(userPath);

            XElement us = (from elem in UserElement.Elements()
                           where elem.Element("UserName").Value == user.UserName
                           select elem).FirstOrDefault();

            if (us != null)
            {
                us.Remove();
                UserElement.Add(user.itemToXElement());
                XMLTools.SaveListToXMLElement(UserElement, userPath);
            }
            else
                throw new ItemNotExeistExeption(typeof(User), 00);
        }

        public void UpdateUaer(string name, Action<User> update)
        {
            XElement UserElement = XMLTools.LoadListFromXMLElement(userPath);


            XElement us = (from elem in UserElement.Elements()
                           where elem.Element("UserName").Value == name
                           select elem).FirstOrDefault();
            if (us == null)
                throw new ItemNotExeistExeption(typeof(User), 00);

            var user = (User)us.xElementToItem(typeof(User));
            update(user);

            us.Remove();
            UserElement.Add(user.itemToXElement());
            XMLTools.SaveListToXMLElement(UserElement, userPath);
        }
        #endregion

    }
}