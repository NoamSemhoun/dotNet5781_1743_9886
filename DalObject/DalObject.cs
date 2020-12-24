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
            throw new NotImplementedException();
        }

        public void DeleteLine(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Line> GetAllLines()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Line> GetAllLinesBy(Predicate<Line> predicate)
        {
            throw new NotImplementedException();
        }

        public Line GetLine(int id)
        {
            throw new NotImplementedException();
        }


        public void UpdateLine(Line line)
        {
            throw new NotImplementedException();
        }

        public void UpdateLine(int id, Action<Line> update)
        {
            throw new NotImplementedException();
        }



        #endregion

        #region LineStation

        public void AddLineStation(LineStation lineStation)
        {
            throw new NotImplementedException();
        }

        public void DeleteLineStation(int lineId, int stationId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LineStation> GetAllLineStations()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LineStation> GetAllLineStationsBy(Predicate<LineStation> predicate)
        {
            throw new NotImplementedException();
        }

        public LineStation GetLineStation(int lineId, int stationId)
        {
            throw new NotImplementedException();
        }

        public void UpdateLineStation(LineStation lineStation)
        {
            throw new NotImplementedException();
        }


        #endregion

        #region LineTrip

        public void AddLineTrip(LineTrip lineTrip)
        {
            throw new NotImplementedException();
        }

        public void DeleteLineTrip(int id)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<LineTrip> GetAllLineTrips()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LineTrip> GetAllLineTripsBy(Predicate<LineTrip> predicate)
        {
            throw new NotImplementedException();
        }


        public LineTrip GetLineTrip(int id)
        {
            throw new NotImplementedException();
        }


        public void UpdateLineTrip(LineTrip lineTrip)
        {
            throw new NotImplementedException();
        }



        #endregion

        #region Station

        public void AddStation(Station station)
        {
            throw new NotImplementedException();
        }

        public void DeleteStation(int code)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Station> GetAllStationes()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> GetAllStationesBy(Predicate<Station> predicate)
        {
            throw new NotImplementedException();
        }

        public Station GetStation(int code)
        {
            throw new NotImplementedException();
        }


        public void UpdateStation(Station station)
        {
            throw new NotImplementedException();
        }

        public void UpdateStation(int code, Action<Line> update)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Trip

        public void AddTrip(Trip trip)
        {
            throw new NotImplementedException();
        }


        public void DeleteTrip(int id)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Trip> GetAllTrips()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Trip> GetAllTripsBy(Predicate<Trip> predicate)
        {
            throw new NotImplementedException();
        }


        public Trip GetTrip(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateTrip(Trip trip)
        {
            throw new NotImplementedException();
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
