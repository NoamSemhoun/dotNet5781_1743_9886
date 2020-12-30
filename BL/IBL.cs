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

        #endregion
     
      #region Line

        IEnumerable<Line> GetAllLines();
        Line GetLine(int id);
        void AddLine(Line line);
        void UpdateLine(Line line);
        void DeleteLine(int id);

        #endregion
           
      #region Admin
        bool CheckAdmin(string userName, string password);

        void AddAdmin(string userName, string password);

        void UpdatePassword(string userName, string newPassword);

        void UpdatUser();

        void deleteUser();

        #endregion
    }
}
