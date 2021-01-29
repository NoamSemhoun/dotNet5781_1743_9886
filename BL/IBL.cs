using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlAPI
{
    public interface IBL
    {
      #region Bus

        IEnumerable<Bus> GetAllBuses();
        Bus GetBus(int liscenseNumber);
        void RefuelBus(int liscenseNumber);
        void MaintenanceBus(int liscenseNumber);      
        void AddBus(Bus bus);
        void UpdateBus(Bus bus);
        void DeleteBus(int liscenseNumber);      
        
        #endregion

      #region Station

        IEnumerable<Station> GetAllStations();
        Station GetStation(int code);
        void UpdateTime(int firstStationCode,int lastStationCode, TimeSpan time);
        void UpdateDistance(int firstStationCode, int lastStationCode, double distance);
        void AddStation(Station station);
        void UpdateStation(Station station);
        void DeleteStation(int code);

        void AddAdjStation(int station1, int station2, double distance, TimeSpan time);
        void UpdateAdjStation(int station1, int station2, double distance, TimeSpan time);

        #endregion
      
      #region Line

        IEnumerable<Line> GetAllLines();
        Line GetLine(int id);
        void AddLine(Line line);
        void UpdateLine(Line line);
        void DeleteLine(int id);
        void AddLine(int lineNumber, List<int> StationsCode, Areas area);

        int GetLineId(int lineNumber, int firstStation, int lastStation);

        IEnumerable<Ferquency> GetFerquencies(int id);
        IEnumerable<Ferquency> GetFerquencies(Line line);

        void AddLineStation(int id, int station, int index);
        void DeleteLineStation(int lineId, int index);

        LineStation GetLineStation(int lineID, int index);

        IEnumerable<AdjacentStation> GetNextStations(int station_id);
        IEnumerable<AdjacentStation> GetprevStations(int station_id);


        #endregion

        #region User


        bool CheckAdmin(string userName, string password);

        void AddUser(User user);

        void DeleteUser(string username);  // Or ID

        void UpdatePassword(string userName, string newPassword);

        void UpdateUser(User user);


        #endregion

        void UpdateAdjStations(List<AdjacentStation> adjacentStations);

        IEnumerable<LineSchedule> GetLinesSchedule(TimeSpan now, int station);

        //IEnumerable<TimeSpan> GetLineDepartureTimes(int lineID);
        //IEnumerable<TimeSpan> GetLineStationsTimes(int lineID);


    }
}
