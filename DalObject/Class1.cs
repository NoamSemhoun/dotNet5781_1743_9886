using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;

namespace DalObject
{
    class DalObject : IDAL
    {

        #region Bus

        public void AddBus(Bus bus)
        {
            throw new NotImplementedException();
        }

        public void DeleteBus(int licenseNum)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bus> GetAllBus()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bus> GetAllBusBy(Predicate<Bus> predicate)
        {
            throw new NotImplementedException();
        }

        public Bus GetBus(int licenseNum)
        {
            throw new NotImplementedException();
        }

        public void UpdateBus(Bus bus)
        {
            throw new NotImplementedException();
        }

        public void UpdateBus(int licenseNum, Action<Bus> update)
        {
            throw new NotImplementedException();
        }



        #endregion

        #region BusOnTrip

        public void AddBusOnTrip(BusOnTrip busOnTrip)
        {
            throw new NotImplementedException();
        }

        public void DeleteBusOnTrip(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BusOnTrip> GetAllBusesOnTrip()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BusOnTrip> GetAllBusesOnTripBy(Predicate<BusOnTrip> predicate)
        {
            throw new NotImplementedException();
        }

        public BusOnTrip GetBusOnTrip(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateBusOnTrip(BusOnTrip busOnTrip)
        {
            throw new NotImplementedException();
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
