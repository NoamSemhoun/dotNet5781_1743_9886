using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;
using DS;


namespace DL
{



    public class DalObject : IDAL             // CRUD Function
    {

        //public int check()
        //{
        //    return DataSource.List_BusesOnTrip.Count();
        //}
        #region singleton
        static readonly DalObject instance = new DalObject();
        static DalObject() { }// static ctor to ensure instance init is done just before first usage
        DalObject() { } // default => private
        public static DalObject Instance { get => instance; }// The public Instance property to use  
        #endregion

        #region Bus

        public void AddBus(Bus bus)
        {
            if (DataSource.List_Buses.FirstOrDefault(b => b.LicenseNum == bus.LicenseNum) != null)

                throw new ItemAlreadyExeistExeption(typeof(Bus), bus.LicenseNum);
            DataSource.List_Buses.Add(bus.Clone());
        }

        public void DeleteBus(int licenseNum)
        {
            if (DataSource.List_Buses.FirstOrDefault(b => b.LicenseNum == licenseNum) == null)
                throw new ItemNotExeistExeption(typeof(Bus), licenseNum);

            DataSource.List_Buses.RemoveAll(b => b.LicenseNum == licenseNum);
        }

        public IEnumerable<Bus> GetAllBus()
        {
            return from item in DataSource.List_Buses
                   select item.Clone();
        }

        public IEnumerable<Bus> GetAllBusBy(Predicate<Bus> predicate)
        {
            return from item in DataSource.List_Buses
                   where predicate(item)
                   select item.Clone();
        }

        public Bus GetBus(int licenseNum)
        {
            Bus bus = DataSource.List_Buses.FirstOrDefault(b => b.LicenseNum == licenseNum);
            if (bus == null)
                throw new ItemNotExeistExeption(typeof(Bus), licenseNum);
            return bus.Clone();
        }



        public void UpdateBus(Bus bus)
        {
            if (DataSource.List_Buses.FirstOrDefault(b => b.LicenseNum == bus.LicenseNum) == null)
                throw new ItemNotExeistExeption(typeof(Bus), bus.LicenseNum);
            DataSource.List_Buses.RemoveAll(b => b.LicenseNum == bus.LicenseNum);
            DataSource.List_Buses.Add(bus.Clone());
        }
        
        public void UpdateBus(int licenseNum, Action<Bus> update)
        {
            Bus bus = DataSource.List_Buses.FirstOrDefault(b => b.LicenseNum == licenseNum);
            if (bus == null)
                throw new ItemNotExeistExeption(typeof(Bus), bus.LicenseNum);
            try { update(DataSource.List_Buses.FirstOrDefault(b => b.LicenseNum == licenseNum)); }
            catch
            { throw new BadActionExeption(typeof(Bus)); }
        }



        #endregion

        #region BusOnTrip

        public void AddBusOnTrip(BusOnTrip busOnTrip)
        {
            if (DataSource.List_BusesOnTrip.FirstOrDefault(b => b.Id == busOnTrip.Id) != null)
                throw new ItemAlreadyExeistExeption(typeof(BusOnTrip), busOnTrip.Id);

            DataSource.List_BusesOnTrip.Add(busOnTrip.Clone());
        }

        public void DeleteBusOnTrip(int id)
        {
            BusOnTrip busOnTrip = DataSource.List_BusesOnTrip.FirstOrDefault(b => b.Id == id);
            if (busOnTrip == null)
                throw new ItemNotExeistExeption(typeof(BusOnTrip), id);
            DataSource.List_BusesOnTrip.Remove(busOnTrip);
        }

        public IEnumerable<BusOnTrip> GetAllBusesOnTrip()
        {
            return from item in DataSource.List_BusesOnTrip
                   select item.Clone();
        }

        public IEnumerable<BusOnTrip> GetAllBusesOnTripBy(Predicate<BusOnTrip> predicate)
        {
            return from item in DataSource.List_BusesOnTrip
                   where predicate(item)
                   select item.Clone();
        }

        public BusOnTrip GetBusOnTrip(int id)
        {
            BusOnTrip busOnTrip = DataSource.List_BusesOnTrip.FirstOrDefault(b => b.Id == id);
            if (busOnTrip == null)
                throw new ItemNotExeistExeption(typeof(BusOnTrip), id);
            return busOnTrip.Clone();
        }

        public void UpdateBusOnTrip(BusOnTrip busOnTrip)
        {
            if (DataSource.List_BusesOnTrip.FirstOrDefault(b => b.Id == busOnTrip.Id) == null)
                throw new ItemNotExeistExeption(typeof(BusOnTrip), busOnTrip.Id);
            DataSource.List_BusesOnTrip.RemoveAll(b => b.Id == busOnTrip.Id);
            DataSource.List_BusesOnTrip.Add(busOnTrip.Clone());
        }

        public void UpdateBusOnTrip(int id, Action<BusOnTrip> update)
        {
            BusOnTrip busOnTrip = DataSource.List_BusesOnTrip.FirstOrDefault(b => b.Id == id);
            if (busOnTrip == null)
                throw new ItemNotExeistExeption(typeof(BusOnTrip), id);
            update(busOnTrip);
        }


        #endregion

        #region Line

        public void AddLine(Line line)
        {
            if (DataSource.List_Lines.FirstOrDefault(l => l.LineID == line.LineID ) != null)

                throw new ItemNotExeistExeption(typeof(Line), line.LineID);
            line.LineID = statics.LineId++;
            DataSource.List_Lines.Add(line.Clone());
        }

        public void DeleteLine(int id)
        {
            Line line = DataSource.List_Lines.FirstOrDefault(l => l.LineID == id);
            if (line == null)
                throw new ItemNotExeistExeption(typeof(Line), id);

            DataSource.List_Lines.RemoveAll(l => l.LineID == id);
        }

        public IEnumerable<Line> GetAllLines()
        {
            return from item in DataSource.List_Lines
                   select item.Clone();
        }

        public IEnumerable<Line> GetAllLinesBy(Predicate<Line> predicate)
        {
            return from item in DataSource.List_Lines
                   where predicate(item)
                   select item.Clone();
        }

        public Line GetLine(int id)
        {
            Line line = DataSource.List_Lines.FirstOrDefault(l => l.LineID == id);
            if (line == null)
                throw new ItemNotExeistExeption(typeof(Line), id);
            return line.Clone();
        }


        public void UpdateLine(Line line)
        {
            if (DataSource.List_Lines.FirstOrDefault(l => l.LineID == line.LineID) == null)
                throw new ItemNotExeistExeption(typeof(Line), line.LineID);
            DataSource.List_Lines.RemoveAll(l => l.LineID == line.LineID);
            DataSource.List_Lines.Add(line.Clone());
        }

        public void UpdateLine(int id, Action<Line> update)
        {
            Line line = DataSource.List_Lines.FirstOrDefault(l => l.LineID == id);
            if (line == null)
                throw new ItemNotExeistExeption(typeof(Line), id);
            try { update(line); }
            catch { throw new BadActionExeption(typeof(Line)); }
        }



        #endregion

        #region LineStation

        public void AddLineStation(LineStation lineStation)
        {
            if (DataSource.List_LineStations.FirstOrDefault(l => l.LineStationIndex == lineStation.LineStationIndex && l.LineId == lineStation.LineId) != null)
                throw new ItemAlreadyExeistExeption(typeof(LineStation), lineStation.LineStationIndex, lineStation.LineId);

            DataSource.List_LineStations.Add(lineStation.Clone());
        }

        public void DeleteLineStation(int lineId, int index)
        {
            if (DataSource.List_LineStations.FirstOrDefault(l => l.LineStationIndex == index && l.LineId == lineId) == null)
                throw new ItemNotExeistExeption(typeof(LineStation), lineId, index);

            DataSource.List_LineStations.RemoveAll(l => l.LineStationIndex == index && l.LineId == lineId);
        }

        public IEnumerable<LineStation> GetAllLineStations()
        {
            return from item in DataSource.List_LineStations
                   select item.Clone();
        }

        public IEnumerable<LineStation> GetAllLineStationsBy(Predicate<LineStation> predicate)
        {
            return from item in DataSource.List_LineStations
                   where predicate(item)
                   select item.Clone();
        }

        public LineStation GetLineStation(int lineId, int index)
        {
            LineStation lineStation = DataSource.List_LineStations.FirstOrDefault(l => l.LineStationIndex == index && l.LineId == lineId);
            if (lineStation == null)
                throw new ItemNotExeistExeption(typeof(LineStation), lineId, index);
            return lineStation.Clone();
        }

        public void UpdateLineStation(LineStation lineStation)
        {
            if (DataSource.List_LineStations.FirstOrDefault(l => l.LineStationIndex == lineStation.LineStationIndex && l.LineId == lineStation.LineId) == null)
                throw new ItemNotExeistExeption(typeof(LineStation), lineStation.LineId, lineStation.LineStationIndex);
            DataSource.List_LineStations.RemoveAll(l => l.LineStationIndex == lineStation.LineStationIndex && l.LineId == lineStation.LineId);
            DataSource.List_LineStations.Add(lineStation.Clone());
        }


        #endregion

        #region LineTrip

        public void AddLineTrip(LineTrip lineTrip)
        {
            if (DataSource.List_LineTrips.FirstOrDefault(l => l.Id == lineTrip.Id) != null)
                throw new ItemAlreadyExeistExeption(typeof(LineTrip), lineTrip.Id);

            DataSource.List_LineTrips.Add(lineTrip.Clone());
        }

        public void DeleteLineTrip(int id)
        {
            if (DataSource.List_LineTrips.FirstOrDefault(l => l.Id == id) == null)
                throw new ItemNotExeistExeption(typeof(LineTrip), id);
            DataSource.List_LineTrips.RemoveAll(l => l.Id == id);
        }

        public IEnumerable<LineTrip> GetAllLineTrips()
        {
            return from item in DataSource.List_LineTrips
                   select item.Clone();
        }

        public IEnumerable<LineTrip> GetAllLineTripsBy(Predicate<LineTrip> predicate)
        {
            return from item in DataSource.List_LineTrips
                   where predicate(item)
                   select item.Clone();
        }


        public LineTrip GetLineTrip(int id)
        {
            LineTrip lineTrip = DataSource.List_LineTrips.FirstOrDefault(l => l.Id == id);
            if (lineTrip == null)
                throw new ItemNotExeistExeption(typeof(LineTrip), id);
            return lineTrip.Clone();
        }


        public void UpdateLineTrip(LineTrip lineTrip)
        {
            if (DataSource.List_LineTrips.FirstOrDefault(l => l.Id == lineTrip.Id) == null)
                throw new ItemNotExeistExeption(typeof(LineTrip), lineTrip.Id);
            DataSource.List_LineTrips.RemoveAll(l => l.Id == lineTrip.Id);
            DataSource.List_LineTrips.Add(lineTrip.Clone());
        }



        #endregion

        #region Station

        public void AddStation(Station station)
        {
            if (DataSource.List_Stations.FirstOrDefault(l => l.Code == station.Code) != null)
                throw new ItemAlreadyExeistExeption(typeof(Station), station.Code); //   TO do
            DataSource.List_Stations.Add(station.Clone());
        }

        public void DeleteStation(int code)
        {
            if (DataSource.List_Stations.FirstOrDefault(l => l.Code == code) == null)
                throw new ItemNotExeistExeption(typeof(Station), code);
            DataSource.List_Stations.RemoveAll(l => l.Code == code);
        }


        public IEnumerable<Station> GetAllStationes()
        {
            return from item in DataSource.List_Stations
                   select item.Clone();
        }

        public IEnumerable<Station> GetAllStationesBy(Predicate<Station> predicate)
        {
            return from item in DataSource.List_Stations
                   where predicate(item)
                   select item.Clone();
        }

        public Station GetStation(int code)
        {
            Station station = DataSource.List_Stations.FirstOrDefault(l => l.Code == code);
            if (station == null)
                throw new ItemNotExeistExeption(typeof(Station), code);
            return station.Clone();
        }


        public void UpdateStation(Station station)
        {
            if (DataSource.List_Stations.FirstOrDefault(l => l.Code == station.Code) == null)
                throw new ItemNotExeistExeption(typeof(Station), station.Code);
            DataSource.List_Stations.RemoveAll(l => l.Code == station.Code);
            DataSource.List_Stations.Add(station.Clone());
        }

        public void UpdateStation(int code, Action<Station> update)
        {
            Station station = DataSource.List_Stations.FirstOrDefault(s => s.Code == code);
            if (station == null)
                throw new ItemNotExeistExeption(typeof(Station), code);
            update(station);
        }

        #endregion

        #region UserTrip

        //public void AddTrip(UserTrip trip)
        //{
        //    if (DataSource.List_Trips.FirstOrDefault(t => t.Id == trip.Id) != null)
        //        throw new ItemAlreadyExeistExeption(typeof(UserTrip), trip.Id);

        //    DataSource.List_Trips.Add(trip.Clone());
        //}


        //public void DeleteTrip(int id)
        //{
        //    if (DataSource.List_Trips.FirstOrDefault(t => t.Id == id) == null)
        //        throw new ItemNotExeistExeption(typeof(UserTrip), id);
        //    DataSource.List_Trips.RemoveAll(t => t.Id == id);
        //}


        //public IEnumerable<UserTrip> GetAllTrips()
        //{
        //    return from item in DataSource.List_Trips
        //           select item.Clone();
        //}

        //public IEnumerable<UserTrip> GetAllTripsBy(Predicate<UserTrip> predicate)
        //{
        //    return from item in DataSource.List_Trips
        //           where predicate(item)
        //           select item.Clone();
        //}


        //public UserTrip GetTrip(int id)
        //{
        //    UserTrip trip = DataSource.List_Trips.FirstOrDefault(t => t.Id == id);
        //    if (trip == null)
        //        throw new ItemNotExeistExeption(typeof(UserTrip), id); 
        //    return trip.Clone();
        //}

        //public void UpdateTrip(UserTrip trip)
        //{
        //    if (DataSource.List_Trips.FirstOrDefault(t => t.Id == trip.Id) == null)
        //        throw new ItemNotExeistExeption(typeof(UserTrip), trip.Id);
        //    DataSource.List_Trips.RemoveAll(t => t.Id == trip.Id);
        //    DataSource.List_Trips.Add(trip.Clone());
        //}


        //public void UpdateTrip(int id, Action<Line> update)
        //{
        //    if (DataSource.List_Trips.FirstOrDefault(t => t.Id == id) 
        //        throw new ItemNotExeistExeption(typeof(UserTrip), trip.Id);
        //}
        #endregion

        #region AdjacentStation

        public void AddAdjacentStation(AdjacentStation adjacentStation)
        {

            if (DataSource.List_AdjacentStations.FirstOrDefault(aS => aS.Statoin1 == adjacentStation.Statoin1 && aS.Station2 == adjacentStation.Station2) != null)
                throw new ItemAlreadyExeistExeption(typeof(AdjacentStation), adjacentStation.Statoin1, adjacentStation.Station2);

            DataSource.List_AdjacentStations.Add(adjacentStation.Clone());
        }

        public void DeleteAdjacentStation(int station1, int station2)
        {
            if (DataSource.List_AdjacentStations.FirstOrDefault(aS => aS.Statoin1 == station1 && aS.Station2 == station2) == null)
                throw new ItemNotExeistExeption(typeof(AdjacentStation), station1, station2);
            DataSource.List_AdjacentStations.RemoveAll(aS => aS.Statoin1 == station1 && aS.Station2 == station2);
        }

        public AdjacentStation GetAdjacentStation(int station1, int station2)
        {
            if (!DataSource.List_AdjacentStations.Any())
                return null;
            AdjacentStation adjacentStation  = DataSource.List_AdjacentStations.FirstOrDefault(aS => aS.Statoin1 == station1 && aS.Station2 == station2);
            if (adjacentStation == null)
                throw new ItemNotExeistExeption(typeof(AdjacentStation), station1, station2);
            return adjacentStation.Clone();
        }

        public IEnumerable<AdjacentStation> GetAllAdjacentStations()
        {
            return from item in DataSource.List_AdjacentStations
                   select item.Clone();
        }


        public IEnumerable<AdjacentStation> GetAllAdjacentStationsBy(Predicate<AdjacentStation> predicate)
        {
            return from item in DataSource.List_AdjacentStations
                   where predicate(item)
                   select item.Clone();
        }


        public void UpdateAdjacentStation(AdjacentStation adjacentStation)
        {
            if (DataSource.List_AdjacentStations.FirstOrDefault(aS => aS.Statoin1 == adjacentStation.Statoin1 && aS.Station2 == adjacentStation.Station2) == null)
                throw new ItemNotExeistExeption(typeof(AdjacentStation), adjacentStation.Statoin1, adjacentStation.Station2);
            DataSource.List_AdjacentStations.RemoveAll(aS => aS.Statoin1 == adjacentStation.Statoin1 && aS.Station2 == adjacentStation.Station2);
            DataSource.List_AdjacentStations.Add(adjacentStation.Clone());
        }

        public void UpdateAdjacentStation(int station1, int station2, Action<AdjacentStation> update)
        {
            AdjacentStation adstaion = (DataSource.List_AdjacentStations.FirstOrDefault(aS => aS.Statoin1 == station1 && aS.Station2 == station2));
            if (adstaion == null)
                throw new ItemNotExeistExeption(typeof(AdjacentStation), station1, station2);
            try { update(adstaion); }
            catch
            { throw new BadActionExeption(typeof(AdjacentStation)); }
        }



        #endregion


        #region User

        public void DeleteUser(string username)
        {
            if (DataSource.List_Users.FirstOrDefault(b => b.UserName == username) == null)
                //throw new ItemNotExeistExeption(typeof(Bus), id);  // id or username with string

            DataSource.List_Users.RemoveAll(b => b.UserName == username);
        }

        public void AddUser(User user)
        {
            if (DataSource.List_Users.FirstOrDefault(l => l.UserName == user.UserName) != null)
                throw new ItemAlreadyExeistExeption(typeof(User), 1111);  // Or  Username with string

            DataSource.List_Users.Add(user.Clone());
        }

        public User GetUser(string name)
        {
            User user = DataSource.List_Users.FirstOrDefault(u => u.UserName == name);
            if (user == null)
                throw new ItemNotExeistExeption(typeof(User), 1111);
            return user.Clone();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return from item in DataSource.List_Users
                   select item.Clone();
        }


        public IEnumerable<User> GetAllUsersBy(Predicate<User> predicate)
        {
            return from item in DataSource.List_Users
                   where predicate(item)
                   select item.Clone();
        }


        public void UpdateUser(User user)
        {
            if (DataSource.List_Users.FirstOrDefault(u => u.UserName == user.UserName) == null)
                throw new ItemNotExeistExeption(typeof(User), 1111);
            DataSource.List_Users.RemoveAll(u => u.UserName == user.UserName);
            DataSource.List_Users.Add(user.Clone());
        }

        public void UpdateUaer(string name, Action<User> update)
        {
            User user = (DataSource.List_Users.FirstOrDefault(u => u.UserName == name));
            if (user == null)
                throw new ItemNotExeistExeption(typeof(User), 1111);
            try { update(user); }
            catch
            { throw new BadActionExeption(typeof(AdjacentStation)); }
        }


        #endregion


    }
}