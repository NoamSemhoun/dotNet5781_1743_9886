using System;
using System.Collections.Generic;
using System.Text;
using DO;

namespace DalApi
{
    public interface IDAL
    {
        //object this[Type type, object obj];



        #region Bus
        /// <summary>
        /// return IEnumerable collection of all buses  
        /// </summary>
        /// <returns IEnumerable<Bus>></returns>
        IEnumerable<Bus> GetAllBus();
        /// <summary>
        /// return IEnumerable collection of buses adapt to predicat
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<Bus> GetAllBusBy(Predicate<Bus> predicate);
        /// <summary>
        /// get licence number of bus
        /// return the bus object
        /// </summary>
        /// <param name="licenseNum"></param>
        /// <returns></returns>
        Bus GetBus(int licenseNum);
        /// <summary>
        /// get bus
        /// add it to the collection
        /// </summary>
        /// <param name="bus"></param>
        void AddBus(Bus bus);
        /// <summary>
        /// Update Bus at the collection
        /// </summary>
        /// <param name="bus"></param>
        void UpdateBus(Bus bus);
        void UpdateBus(int licenseNum, Action<Bus> update); //method that knows to updt specific fields in Person
        /// <summary>
        /// get int licence number
        /// delete the bus from the collection
        /// </summary>
        /// <param name="licenseNum"></param>
        void DeleteBus(int licenseNum);
        #endregion

        #region BusOnTrip

        IEnumerable<BusOnTrip> GetAllBusesOnTrip();
        IEnumerable<BusOnTrip> GetAllBusesOnTripBy(Predicate<BusOnTrip> predicate);
        BusOnTrip GetBusOnTrip(int id);
        void AddBusOnTrip(BusOnTrip busOnTrip);
        void UpdateBusOnTrip(BusOnTrip busOnTrip);
        void UpdateBusOnTrip(int id, Action<BusOnTrip> update); //method that knows to updt specific fields
        void DeleteBusOnTrip(int id);

        #endregion

        #region Line

        IEnumerable<Line> GetAllLines();
        IEnumerable<Line> GetAllLinesBy(Predicate<Line> predicate);
        Line GetLine(int id);
        void AddLine(Line line);
        void UpdateLine(Line line);
        void UpdateLine(int id, Action<Line> update); //method that knows to updt specific fields
        void DeleteLine(int id);

        #endregion

        #region Station

        IEnumerable<Station> GetAllStationes();
        IEnumerable<Station> GetAllStationesBy(Predicate<Station> predicate);
        Station GetStation(int code);
        void AddStation(Station station);
        void UpdateStation(Station station);
        void UpdateStation(int code, Action<Line> update); //method that knows to updt specific fields
        void DeleteStation(int code);

        #endregion

        #region Trip

        IEnumerable<Trip> GetAllTrips();
        IEnumerable<Trip> GetAllTripsBy(Predicate<Trip> predicate);
        Trip GetTrip(int id);
        void AddTrip(Trip trip);
        void UpdateTrip(Trip trip);
        void UpdateTrip(int id, Action<Line> update); //method that knows to updt specific fields
        void DeleteTrip(int id);

        #endregion

        #region AdjacentStation

        IEnumerable<AdjacentStation> GetAllAdjacentStations();
        IEnumerable<AdjacentStation> GetAllAdjacentStationsBy(Predicate<AdjacentStation> predicate);
        AdjacentStation GetAdjacentStation(int station1, int station2);
        void AddAdjacentStation(AdjacentStation adjacentStation);
        void UpdateAdjacentStation(AdjacentStation adjacentStation);
        //void UpdateAdjacentStation(int id, Action<AdjacentStation> update); //method that knows to updt specific fields
        void DeleteAdjacentStation(int id);

        #endregion

        #region LineStation


        IEnumerable<LineStation> GetAllLineStations();
        IEnumerable<LineStation> GetAllLineStationsBy(Predicate<LineStation> predicate);
        LineStation GetLineStation(int lineId, int stationId);
        void AddLineStation(LineStation lineStation);
        void UpdateLineStation(LineStation lineStation);
        //void UpdateLineStation(int id, Action<Line> update); //method that knows to updt specific fields
        void DeleteLineStation(int lineId, int stationId);

        #endregion

        #region LineTrip

        IEnumerable<LineTrip> GetAllLineTrips();
        IEnumerable<LineTrip> GetAllLineTripsBy(Predicate<LineTrip> predicate);
        LineTrip GetLineTrip(int id);
        void AddLineTrip(LineTrip lineTrip);
        void UpdateLineTrip(LineTrip lineTrip);
        //void UpdateLineStation(int id, Action<Line> update); //method that knows to updt specific fields
        void DeleteLineTrip(int id);

        #endregion

    }
}
