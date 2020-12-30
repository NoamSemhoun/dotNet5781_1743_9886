using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlAPI;
using BO;


namespace BL 
{
    class BLImp : IBL
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
            b.Km_LastMaintenance = 0;
            b.Fuel = 1200;
            b.Date_LastMaintenance = DateTime.Now;
        }

        public void RefuelBus(int liscenseNumber)
        {
            dal.UpdateBus(liscenseNumber, b => b.Fuel = 1200);
        }

        public void UpdateBus(Bus bus)
        {
            DO.Bus b = new DO.Bus();
            bus.Clone(b);
            dal.UpdateBus(b);
        }


        public Bus GetBus(int liscenseNumber)
        {
            DO.Bus b = dal.GetBus(liscenseNumber);
            Bus bus = new Bus();
            b.Clone(bus);
            return bus;
        }

        public IEnumerable<Bus> GetAllBuses()
        {
            return from item in dal.GetAllBus()
                   select (Bus)item.CloneNew(typeof(Bus));
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
                {
                    //**************************/****************/************/****************/**********/
                }
        }



        public void DeleteLine(int id)
        {
            dal.DeleteLine(id);
            IEnumerable<DO.LineStation> tmp = dal.GetAllLineStationsBy(lS => lS.LineID == id);
            foreach (DO.LineStation lS in tmp)
                dal.DeleteLineStation(lS.LineID, lS.Code);
        }

        public IEnumerable<Line> GetAllLines()
        {
            return from item in dal.GetAllLines()
                   select GetLine(item.LineID);
        }

        public Line GetLine(int id)
        {
            Line line = new Line();

            DO.Line lineDo = dal.GetLine(id);
            lineDo.Clone(line);
            List<DO.LineStation> DoLs_list = (List<DO.LineStation>)dal.GetAllLineStationsBy(ls => ls.LineID == id);
            var lineStationList = createStationListFromDoObjects(DoLs_list);
            line.List_LineStations = lineStationList;

            return (Line)line.CloneNew(typeof(Line));
        }

        public List<LineStation> createStationListFromDoObjects(List<DO.LineStation> lS_List)
        {
            lS_List.Sort(LineStationComparison);
            List<LineStation> list = new List<LineStation>();
            foreach (DO.LineStation lS in lS_List)
            {
                DO.AdjacentStation aS;
                if (lS.NextStation != -1)
                    aS = dal.GetAdjacentStation(lS.Code, lS.NextStation);
                else
                    aS = new DO.AdjacentStation { Distance = 0, Time = new TimeSpan(0) };
                list.Add(new LineStation
                {
                    Code = lS.Code,
                    LineId = lS.LineID,
                    LineStationIndex = lS.LineStationIndex,
                    NextStation = lS.NextStation,
                    PrevStation = lS.PrevStation,
                    Distance_ToNext = aS.Distance,
                    Time_ToNext = aS.Time,
                    Name = dal.GetStation(lS.Code).Name
                });

            }

            return list;
        }

        public void UpdateLine(Line line)
        {
            dal.UpdateLine((DO.Line)line.CloneNew(typeof(DO.Line)));
            foreach (LineStation l in line.List_LineStations)
                dal.UpdateLineStation((DO.LineStation)l.CloneNew(typeof(DO.LineStation)));
        }

        public static int LineStationComparison(DO.LineStation A, DO.LineStation B)
        {
            if (A.LineStationIndex < B.LineStationIndex)
                return -1;
            if (A.LineStationIndex == B.LineStationIndex)
                return 0;
            return 1;
        }


        #endregion

        #region station

        public void AddStation(Station station)
        {
            DO.Station station1 = new DO.Station();
            station.Clone(station1);
            dal.AddStation(station1); // Is existing ?
        }


        public void DeleteStation(int code)
        {
            dal.DeleteStation(code);
        }

        public Station GetStation(int code)
        {
            return Cloning.DoObjectsToStation(code);
        }



        public IEnumerable<Station> GetAllStations()
        {
            return from item in dal.GetAllStationes()
                   select Cloning.DoObjectsToStation(item.Code);
        }


        public void UpdateDistance(int firstStationCode, int lastStationCode, double distance)
        {
            dal.UpdateAdjacentStation(firstStationCode, lastStationCode, aS => aS.Distance = distance);
        }

        public void UpdateTime(int firstStationCode, int lastStationCode, TimeSpan time)
        {
            dal.UpdateAdjacentStation(firstStationCode, lastStationCode, aS => aS.Time = time);
        }

        public void UpdateStation(Station station)
        {
            dal.UpdateStation((DO.Station)station.CloneNew(typeof(DO.Station)));
            foreach (Line l in station.List_Lines)
                dal.UpdateLine((DO.Line)l.CloneNew(typeof(DO.Line)));
        }


        #endregion

        #region User 
        
        public bool CheckAdmin(string userName, string password)
        {
            throw new NotImplementedException();
            // Bool isAdmin    User.isAdmin=true;
        }

        public void AddUser(User myuser)
        {
            DO.User User1 = new DO.User();
            myuser.Clone(User1);
            dal.AddUser(User1); 
        }

        public void DeleteUser(string username)  // Or ID           
        {
            dal.DeleteUser(username);
        }


        public void UpdatePassword(string userName, string newPassword)
        {
            throw new NotImplementedException();

        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();

        }


        #endregion





    }
}
