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
        static DalObject() { }// static ctor to ensure instance init is done just before first usage
        DalObject() { } // default => private
        public static DalObject Instance { get => instance; }// The public Instance property to use  
#endregion

        #region Bus

        public void AddBus(Bus bus)
        {
            if (DataSource.List_Buses.FirstOrDefault(b => b.LicenseNum == bus.LicenseNum) != null)
                // adapt to our Exeption
                throw new Exception();// adapt to our Exeption

            DataSource.List_Buses.Add(bus.Clone());
        }

        public void DeleteBus(int licenseNum)
        {
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
                throw new Exception();
            return bus.Clone();
        }

       

        public void UpdateBus(Bus bus)
        {
            if (DataSource.List_Buses.FirstOrDefault(b => b.LicenseNum == bus.LicenseNum) == null)
                throw new Exception(); //////////////
            DataSource.List_Buses.RemoveAll(b => b.LicenseNum == bus.LicenseNum);
            DataSource.List_Buses.Add(bus.Clone());
        }

        public void UpdateBus(int licenseNum, Action<Bus> update)
        {
            throw new NotImplementedException();
        }



        #endregion

        #region BusOnTrip

        public void AddBusOnTrip(BusOnTrip busOnTrip)
        {
            if (DataSource.List_BusesOnTrip.FirstOrDefault(b => b.Id == busOnTrip.Id) != null)
                // adapt to our Exeption
                throw new Exception();// adapt to our Exeption

            DataSource.List_BusesOnTrip.Add(busOnTrip.Clone());
        }

        public void DeleteBusOnTrip(int id)
        {
            DataSource.List_BusesOnTrip.RemoveAll(b => b.Id == id);
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
                throw new Exception();
            return busOnTrip.Clone();
        }

        public void UpdateBusOnTrip(BusOnTrip busOnTrip)
        {
            if (DataSource.List_BusesOnTrip.FirstOrDefault(b => b.Id == busOnTrip.Id) == null)
                throw new Exception(); //////////////
            DataSource.List_BusesOnTrip.RemoveAll(b => b.Id == busOnTrip.Id);
            DataSource.List_BusesOnTrip.Add(busOnTrip.Clone());
        }

        public void UpdateBusOnTrip(int id, Action<BusOnTrip> update)
        {
            throw new NotImplementedException();
        }


        #endregion

        #region Line

        public void AddLine(Line line)
        {
            if (DataSource.List_Lines.FirstOrDefault(l => l.LineID == line.LineID) != null)
                // adapt to our Exeption
                throw new Exception();// adapt to our Exeption

            DataSource.List_Lines.Add(line.Clone());
        }

        public void DeleteLine(int id)
        {
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
                throw new Exception();
            return line.Clone();
        }


        public void UpdateLine(Line line)
        {
            if (DataSource.List_Lines.FirstOrDefault(l => l.LineID == line.LineID) == null)
                throw new Exception(); //////////////
            DataSource.List_Lines.RemoveAll(l => l.LineID == line.LineID);
            DataSource.List_Lines.Add(line.Clone());
        }

        public void UpdateLine(int id, Action<Line> update)
        {
            throw new NotImplementedException();
        }



        #endregion

        #region LineStation

        public void AddLineStation(LineStation lineStation)
        {
            if (DataSource.List_LineStations.FirstOrDefault(l => l.Code == lineStation.Code && l.LineID == lineStation.LineID) != null)
                // adapt to our Exeption
                throw new Exception();// adapt to our Exeption

            DataSource.List_LineStations.Add(lineStation.Clone());
        }

        public void DeleteLineStation(int lineId, int stationId)
        {
            DataSource.List_LineStations.RemoveAll(l => l.Code == stationId && l.LineID == lineId);
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

        public LineStation GetLineStation(int lineId, int stationId)
        {
            LineStation lineStation = DataSource.List_LineStations.FirstOrDefault(l => l.Code == stationId && l.LineID == lineId);
            if (lineStation == null)
                throw new Exception();
            return lineStation.Clone();
        }

        public void UpdateLineStation(LineStation lineStation)
        {
            if (DataSource.List_LineStations.FirstOrDefault(l => l.Code == lineStation.Code && l.LineID == lineStation.LineID) == null)
                throw new Exception(); //////////////
            DataSource.List_LineStations.RemoveAll(l => l.Code == lineStation.Code && l.LineID == lineStation.LineID);
            DataSource.List_LineStations.Add(lineStation.Clone());
        }


        #endregion

        #region LineTrip

        public void AddLineTrip(LineTrip lineTrip)
        {
            if (DataSource.List_LineTrips.FirstOrDefault(l => l.Id == lineTrip.Id) != null)
                // adapt to our Exeption
                throw new Exception();// adapt to our Exeption

            DataSource.List_LineTrips.Add(lineTrip.Clone());
        }

        public void DeleteLineTrip(int id)
        {
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
                throw new Exception();
            return lineTrip.Clone();
        }


        public void UpdateLineTrip(LineTrip lineTrip)
        {
            if (DataSource.List_LineTrips.FirstOrDefault(l => l.Id == lineTrip.Id) == null)
                throw new Exception(); //////////////
            DataSource.List_LineTrips.RemoveAll(l => l.Id == lineTrip.Id);
            DataSource.List_LineTrips.Add(lineTrip.Clone());
        }



        #endregion

        #region Station

        public void AddStation(Station station)
        {
             if (DataSource.List_Stations.FirstOrDefault(l => l.Code == station.Code) != null)
                // adapt to our Exeption
                throw new Exception();// adapt to our Exeption

              DataSource.List_Stations.Add(station.Clone());
        }

        public void DeleteStation(int code)
        {
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
                throw new Exception();
            return station.Clone();
        }


        public void UpdateStation(Station station)
        {
            if (DataSource.List_Stations.FirstOrDefault(l => l.Code == station.Code) == null)
                throw new Exception(); //////////////
            DataSource.List_Stations.RemoveAll(l => l.Code == station.Code);
            DataSource.List_Stations.Add(station.Clone());
        }

        public void UpdateStation(int code, Action<Line> update)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region UserTrip

        public void AddTrip(UserTrip trip)
        {
            if (DataSource.List_Trips.FirstOrDefault(t => t.Id == trip.Id) != null)
                // adapt to our Exeption
                throw new Exception();// adapt to our Exeption

            DataSource.List_Trips.Add(trip.Clone());
        }


        public void DeleteTrip(int id)
        {
            DataSource.List_Trips.RemoveAll(t => t.Id == id);
        }


        public IEnumerable<UserTrip> GetAllTrips()
        {
            return from item in DataSource.List_Trips
                   select item.Clone();
        }

        public IEnumerable<UserTrip> GetAllTripsBy(Predicate<UserTrip> predicate)
        {
            return from item in DataSource.List_Trips
                   where predicate(item)
                   select item.Clone();
        }


        public UserTrip GetTrip(int id)
        {
            UserTrip trip = DataSource.List_Trips.FirstOrDefault(t => t.Id == id);
            if (trip == null)
                throw new Exception();
            return trip.Clone();
        }

        public void UpdateTrip(UserTrip trip)
        {
            if (DataSource.List_Trips.FirstOrDefault(t => t.Id == trip.Id) == null)
                throw new Exception(); //////////////
            DataSource.List_Trips.RemoveAll(t => t.Id == trip.Id);
            DataSource.List_Trips.Add(trip.Clone());
        }


        public void UpdateTrip(int id, Action<Line> update)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region AdjacentStation

        public void AddAdjacentStation(AdjacentStation adjacentStation)
        {

            if (DataSource.List_AdjacentStations.FirstOrDefault(aS => aS.Statoin1 == adjacentStation.Statoin1 && aS.Station2 == adjacentStation.Station2) != null)
                // adapt to our Exeption
                throw new Exception();// adapt to our Exeption

            DataSource.List_AdjacentStations.Add(adjacentStation.Clone());
        }
        
        public void DeleteAdjacentStation(int station1, int station2)
        {
            DataSource.List_AdjacentStations.RemoveAll(aS => aS.Statoin1 == station1 && aS.Station2 == station2);
        }

        public AdjacentStation GetAdjacentStation(int station1, int station2)
        {
            AdjacentStation adjacentStation = DataSource.List_AdjacentStations.FirstOrDefault(aS => aS.Statoin1 == station1 && aS.Station2 == station2);
            if (adjacentStation == null)
                throw new Exception();
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
                throw new Exception(); //////////////
            DataSource.List_AdjacentStations.RemoveAll(aS => aS.Statoin1 == adjacentStation.Statoin1 && aS.Station2 == adjacentStation.Station2);
            DataSource.List_AdjacentStations.Add(adjacentStation.Clone());
        }



        #endregion


    }
}
