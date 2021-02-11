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

        ClockSimulator simulator;


        #region bus

        public void AddBus(Bus bus)
        {
            DO.Bus doBus = new DO.Bus();
            bus.Clone(doBus);
            try { dal.AddBus(doBus); }
            catch (DO.ItemAlreadyExeistExeption e)
            { throw (ItemAlreadyExeistExeption)e.CloneNew(typeof(ItemAlreadyExeistExeption)); }
        }

        public void DeleteBus(int liscenseNumber)
        {
            try { dal.DeleteBus(liscenseNumber); }
            catch
            { throw new ItemNotExeistExeption(typeof(Bus), liscenseNumber); }
        }

        public void MaintenanceBus(int liscenseNumber)
        {
            try { dal.UpdateBus(liscenseNumber, maintenance); }
            catch (DO.ItemNotExeistExeption)
            {
                throw new ItemNotExeistExeption(typeof(Bus), liscenseNumber);
            }
        }

        private void maintenance(DO.Bus b)
        {
            b.Km_LastMaintenance = 0;
            b.Fuel = 1200;
            b.Date_LastMaintenance = DateTime.Now;
        }

        public void RefuelBus(int liscenseNumber)
        {
            try { dal.UpdateBus(liscenseNumber, b => b.Fuel = 1200); }
            catch (DO.ItemNotExeistExeption)
            {
                throw new ItemNotExeistExeption(typeof(Bus), liscenseNumber);
            }
        }

        public void UpdateBus(Bus bus)
        {
            DO.Bus b = new DO.Bus();
            bus.Clone(b);
            try { dal.UpdateBus(b); }
            catch (DO.ItemNotExeistExeption)
            { throw new ItemNotExeistExeption(typeof(Bus), bus.LicenseNum); }
        }

        public Bus GetBus(int liscenseNumber)
        {
            DO.Bus b;
            try { b = dal.GetBus(liscenseNumber); }
            catch (DO.ItemNotExeistExeption)
            {
                throw new ItemNotExeistExeption(typeof(Bus), liscenseNumber);
            }
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

            ManageDoData.AddLine(line);
        }

        public void AddLine(int lineNumber, List<int> StationsCode, Areas area)
        {
            if (StationsCode.Count < 2)
                throw new Exception();//*********//  not adapt params exeption
            Line line = ManageBoItems.CreateLine(lineNumber, StationsCode, area);
            AddLine(line);
        }

        public void DeleteLine(int id)
        {
            ManageDoData.DeleteLine(id);
        }

        public IEnumerable<Line> GetAllLines()
        {
            return GetBOItems.GetLines(l => true);

        }

        public Line GetLine(int id)
        {
            return GetBOItems.GetLine(l => l.LineID == id);
        }

        public int GetLineId(int lineNumber, int firstStation, int lastStation)
        {
            var l = dal.GetAllLinesBy(L => L.LineNumber == lineNumber && L.FirstStation == firstStation && L.LastStation == lastStation);
            if (!l.Any())
                throw new ItemNotExeistExeption(typeof(Line), lineNumber);
            return l.First().LineID;
        }

        public void UpdateLine(Line line)
        {
            ManageDoData.UpdateLine(line);
        }

        public static int LineStationComparison(DO.LineStation A, DO.LineStation B)
        {
            if (A.LineStationIndex < B.LineStationIndex)
                return -1;
            if (A.LineStationIndex == B.LineStationIndex)
                return 0;
            return 1;
        }

        public IEnumerable<Ferquency> GetFerquencies(int id)
        {
            return from item in dal.GetAllLineTripsBy(lT => lT.LineID == id)
                   select new Ferquency { StartTime = item.StartAt, EndTime = item.FinishAt, Freq = item.Frequency };
        }

        public IEnumerable<Ferquency> GetFerquencies(Line line)
        {
            return from item in dal.GetAllLineTripsBy(lT => lT.LineID == line.LineID)
                   select new Ferquency { StartTime = item.StartAt, EndTime = item.FinishAt, Freq = item.Frequency };
        }

        public void AddLineStation(int id, int station, int index)
        {
            Line line;
            try { line = GetLine(id); }
            catch (ItemNotExeistExeption)
            { throw new LackOfDataExeption(DataType.LineData, id.ToString(), id); } // *************//lack of data exeption
            line.AddStation(station, index);
            ManageDoData.UpdateLine(line);
        }

        public void DeleteLineStation(int lineId, int index)
        {
            Line line = GetLine(lineId);

            line.DeleteStation(index);

            ManageDoData.UpdateLine(line);

        }

        public LineStation GetLineStation(int lineID, int index)
        {

            return GetBOItems.GetLineStation(lS => lS.LineId == lineID && lS.LineStationIndex == index);
            //DO.LineStation dalLineStation = dal.GetLineStation(lineID, index);
            //int nextStation;
            //try { nextStation = dal.GetLineStation(lineID, index + 1).Code; }
            //catch (DO.ItemNotExeistExeption)
            //{ nextStation = -1; }

            //int prevStation;
            //if (index > 0)
            //    prevStation = dal.GetLineStation(lineID, index - 1).Code;
            //else
            //    prevStation = -1;
            //DO.AdjacentStation adjData;
            //try { adjData = dal.GetAdjacentStation(dalLineStation.Code, nextStation); }
            //catch(DO.ItemNotExeistExeption)
            //{ throw new Exception(); } //******************//*************lack of data Exeption 



            //return new LineStation
            //{
            //    Code = dalLineStation.Code,
            //    Distance_ToNext = adjData.Distance,
            //    LineId = lineID,
            //    LineStationIndex = index,
            //    Time_ToNext = adjData.Time,
            //    NextStation = nextStation,
            //    PrevStation = prevStation,
            //    Name = dal.GetStation(dalLineStation.Code).Name
            //};
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
            foreach (LineStation lineStation in GetBOItems.GetLineStations(lS => lS.Code == code))
                ManageDoData.DeleteLineStation(lineStation);
            dal.DeleteStation(code);
        }

        public Station GetStation(int code)
        {
            return Cloning.DoObjectsToStation(code);
        }

        public IEnumerable<Station> GetAllStations()
        {
            return (from item in dal.GetAllStationes()
                    select Cloning.DoObjectsToStation(item.Code)).OrderBy(s => s.Code);
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

        public void AddAdjStation(int station1, int station2, double distance, TimeSpan time)
        {

            try
            {
                dal.AddAdjacentStation(new DO.AdjacentStation
                {
                    Statoin1 = station1,
                    Station2 = station2,
                    Distance = distance,
                    Time = time
                });

            }
            catch (DO.ItemAlreadyExeistExeption)
            { throw new ItemAlreadyExeistExeption(typeof(DO.AdjacentStation), station1, station2); }

        }

        public void UpdateAdjStation(int station1, int station2, double distance, TimeSpan time)
        {
            try
            {
                dal.UpdateAdjacentStation(station1, station2, a => { a.Distance = distance; a.Time = time; });
            }
            catch (DO.ItemNotExeistExeption e)
            {
                throw (ItemNotExeistExeption)e.CloneNew(typeof(ItemNotExeistExeption));
            }
        }

        public IEnumerable<AdjacentStation> GetNextStations(int station_id)
        {
            return (from item in dal.GetAllAdjacentStations()
                    where item.Statoin1 == station_id
                    select ConvertItems.GetAdjacentStation(item.Statoin1, item.Station2)).OrderBy(s => s.Station2);
        }


        public IEnumerable<AdjacentStation> GetprevStations(int station_id)
        {
            return (from item in dal.GetAllAdjacentStations()
                    where item.Station2 == station_id
                    select ConvertItems.GetAdjacentStation(item.Statoin1, item.Station2)).OrderBy(s => s.Statoin1);
        }

        #endregion

        #region User 

        public bool CheckAdmin(string userName, string password)
        {
            if (dal.GetAllUsersBy(u => u.UserName == userName && password == u.Password && u.is_Admin).Any())
                return true;
            return false;
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

        public IEnumerable<LineSchedule> GetLinesSchedule(TimeSpan now, int station)
        {
            return from item in dal.GetAllLineStationsBy(s => s.Code == station)
                   select (LineSchedule) Schedules.GetLineScadual(now, item.LineId, station);

        }


        public void UpdateAdjStations(List<AdjacentStation> adjacentStations)
        {


            List<DO.AdjacentStation> DoAdjStations = (from item in adjacentStations
                                                      where (DataCheck.isExeist<DO.Station>(s => s.Code == item.Statoin1) && DataCheck.isExeist<DO.Station>(s => s.Code == item.Station2))
                                                      select (DO.AdjacentStation)item.CloneNew(typeof(DO.AdjacentStation))).ToList();
            if (adjacentStations.Count != DoAdjStations.Count)
                throw new LackOfDataExeption(DataType.StationData, -1);

            updateAdjStations(DoAdjStations);
        }

        private void updateAdjStations(List<DO.AdjacentStation> adjacentStations)
        {


            foreach (DO.AdjacentStation aS in adjacentStations)
            {
                if (DataCheck.isExeist<DO.AdjacentStation>(a => a.Statoin1 == aS.Statoin1 && a.Station2 == aS.Station2))
                {
                    if (DataCheck.didNeedUpdaete<DO.AdjacentStation>(aS, a => a.Statoin1 == aS.Statoin1 && a.Station2 == aS.Station2))
                    {
                        dal.UpdateAdjacentStation(aS);
                    }
                }
                else
                {
                    dal.AddAdjacentStation(aS);
                }
            }
        }


        IEnumerable<TimeSpan> GetLineDepartureTimes(int lineID)
        {
            Line line = GetLine(lineID);
            Schedules schedules = new Schedules(line);
            return schedules.DepartureTimes;
        }
        IEnumerable<TimeSpan> GetLineStationsTimes(int lineID)
        {
            Line line = GetLine(lineID);
            Schedules schedules = new Schedules(line);
            return schedules.StatinsTimes;
        }

        public void RunSimulator(TimeSpan startTime, int rate, Func<TimeSpan> progressChanged)
        {
            if (simulator != null)
                simulator.stop();
            simulator = new ClockSimulator(rate, startTime);
            //simulator.TimeChanged += 
        }


        //public void StopSimulator()
        //{
        //    if (simulator != null)
        //        simulator.stop();
        //}


    }
}
