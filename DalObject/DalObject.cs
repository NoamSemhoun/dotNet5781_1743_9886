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
                                                                                                                                   


    public class DalObject : IDAL
    {

        //public int check()
        //{
        //    return DataSource.List_BusesOnTrip.Count();
        //}
        #region singlton
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
            if (DataSource.List_Lines.FirstOrDefault(l => l.Id == line.Id) != null)
                // adapt to our Exeption
                throw new Exception();// adapt to our Exeption

            DataSource.List_Lines.Add(line.Clone());
        }

        public void DeleteLine(int id)
        {
            DataSource.List_Lines.RemoveAll(l => l.Id == id);
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
            Line line = DataSource.List_Lines.FirstOrDefault(l => l.Id == id);
            if (line == null)
                throw new Exception();
            return line.Clone();
        }


        public void UpdateLine(Line line)
        {
            if (DataSource.List_Lines.FirstOrDefault(l => l.Id == line.Id) == null)
                throw new Exception(); //////////////
            DataSource.List_Lines.RemoveAll(l => l.Id == line.Id);
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
            if (DataSource.List_LineStations.FirstOrDefault(l => l.Code == lineStation.Code && l.LineId == lineStation.LineId) != null)
                // adapt to our Exeption
                throw new Exception();// adapt to our Exeption

            DataSource.List_LineStations.Add(lineStation.Clone());
        }

        public void DeleteLineStation(int lineId, int stationId)
        {
            DataSource.List_LineStations.RemoveAll(l => l.Code == stationId && l.LineId == lineId);
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
            LineStation lineStation = DataSource.List_LineStations.FirstOrDefault(l => l.Code == stationId && l.LineId == lineId);
            if (lineStation == null)
                throw new Exception();
            return lineStation.Clone();
        }

        public void UpdateLineStation(LineStation lineStation)
        {
            if (DataSource.List_LineStations.FirstOrDefault(l => l.Code == lineStation.Code && l.LineId == lineStation.LineId) == null)
                throw new Exception(); //////////////
            DataSource.List_LineStations.RemoveAll(l => l.Code == lineStation.Code && l.LineId == lineStation.LineId);
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

        #region Trip

        public void AddTrip(Trip trip)
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


        public IEnumerable<Trip> GetAllTrips()
        {
            return from item in DataSource.List_Trips
                   select item.Clone();
        }

        public IEnumerable<Trip> GetAllTripsBy(Predicate<Trip> predicate)
        {
            return from item in DataSource.List_Trips
                   where predicate(item)
                   select item.Clone();
        }


        public Trip GetTrip(int id)
        {
            Trip trip = DataSource.List_Trips.FirstOrDefault(t => t.Id == id);
            if (trip == null)
                throw new Exception();
            return trip.Clone();
        }

        public void UpdateTrip(Trip trip)
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
            throw new NotImplementedException();
        }

        public void DeleteAdjacentStation(int id)
        {
            throw new NotImplementedException();
        }

        public AdjacentStation GetAdjacentStation(int station1, int station2)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AdjacentStation> GetAllAdjacentStations()
        {
            throw new NotImplementedException();
        }


        public IEnumerable<AdjacentStation> GetAllAdjacentStationsBy(Predicate<AdjacentStation> predicate)
        {
            throw new NotImplementedException();
        }


        public void UpdateAdjacentStation(AdjacentStation adjacentStation)
        {
            throw new NotImplementedException();
        }



        #endregion


    }
}
