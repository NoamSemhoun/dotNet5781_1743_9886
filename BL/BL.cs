using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlAPI;
using BO;


namespace BL 
{
    public class BLImp : IBL
    {
        DalApi.IDAL dal = DalApi.DalFactory.GetDal();


        #region bus

        public void AddBus(Bus bus)
        {
            DO.Bus doBus = new DO.Bus();
            bus.Clone(doBus);
            dal.AddBus(doBus);            
        }

        public void DeleteBus(int liscenseNumber)
        {
            dal.DeleteBus(liscenseNumber);
        }

        public void MaintenanceBus(int liscenseNumber)
        {
            dal.UpdateBus(liscenseNumber, maintenance); 
        }

        private void maintenance(DO.Bus b)
        {
            b.TripSinceLastService = 0;
            b.FuelRemain = 1200;
            b.LastServiceDate = DateTime.Now;
        }

        public void RefuelBus(int liscenseNumber)
        {
            dal.UpdateBus(liscenseNumber, b => b.FuelRemain = 1200);
        }

        public void UpdateBus(Bus bus)
        {
            DO.Bus b = new DO.Bus();
            bus.Clone(b);
            dal.UpdateBus(b);
        }

        #endregion

        #region Line
        public void AddLine(Line line)
        {
            DO.Line l = new DO.Line();
            line.Clone(l);
            dal.AddLine(l);
            saveLineStationList(line.List_LineStations);
        }

        private void saveLineStationList(List<LineStation> list)
        {
            List<DO.AdjacentStation> AS_List = new List<DO.AdjacentStation>();
            List<DO.LineStation> doLS_List = new List<DO.LineStation>();
            list.LineStationListToDoObjectsLists(doLS_List, AS_List);
            foreach (DO.LineStation lS in doLS_List)
                try { dal.AddLineStation((DO.LineStation)lS.CloneNew(Type.GetType("LineStation"))); }
                catch
                {
                    /////**************************************//////////////**************///////////
                }
            foreach (DO.AdjacentStation aS in AS_List)
                try { dal.AddAdjacentStation((DO.AdjacentStation)aS.CloneNew(Type.GetType("AdjacentStation"))); }
                catch 
                {//**************************/****************/************/****************/**********/
                }
        }

        

        public void DeleteLine(int id)
        {
            throw new NotImplementedException();
        }
        #endregion

        public void AddStation(Station station)
        {
            throw new NotImplementedException();
        }



 

        public void DeleteStation(int code)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bus> GetAllBuses()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Line> GetAllLines()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> GetAllStations()
        {
            throw new NotImplementedException();
        }

        public Bus GetBus(int liscenseNumber)
        {
            throw new NotImplementedException();
        }

        public Line GetLine(int id)
        {
            throw new NotImplementedException();
        }

        public Station GetStation(int code)
        {
            throw new NotImplementedException();
        }


        public void UpdateDistance(int firstStationCode, int lastStationCode)
        {
            throw new NotImplementedException();
        }

        public void UpdateLine(Line line)
        {
            throw new NotImplementedException();
        }

        public void UpdateStation(Station station)
        {
            throw new NotImplementedException();
        }

        public void UpdateTime(int firstStationCode, int lastStationCode)
        {
            throw new NotImplementedException();
        }
    }
}
