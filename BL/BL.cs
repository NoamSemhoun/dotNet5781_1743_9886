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
            //create a DO.line adapt to the new BO.line to add and check if a line with that data already exeist:  
            DO.Line l = new DO.Line();
            line.Clone(l);
            var lineL = dal.GetAllLinesBy(L => L.LineNumber == line.LineNumber && L.FirstStation == line.FirstStation && L.LastStation == line.LastStation);
            if (lineL.Any())
                throw new ItemAlreadyExeistExeption(typeof(Line), line.LineNumber);
            line.LineID = 0;

            //chack if all the station exeist at the dal layer
            foreach (LineStation lS in line.List_LineStations)
                try { dal.GetStation(lS.Code); }
                catch { throw new Exception(); } // ***************** // ********* // not fauond data exeption

            List<DO.AdjacentStation> adjStationsToAdd = ConvertItems.GetAdjStationToAdd(line);

            dal.AddLine(l);
            foreach (DO.AdjacentStation aS in adjStationsToAdd)
                dal.AddAdjacentStation(aS);

            lineL = dal.GetAllLinesBy(L => L.LineNumber == l.LineNumber && L.FirstStation == l.FirstStation && L.LastStation == l.LastStation);
            l = lineL.First();

            List<DO.LineStation> lineStationsToAdd = ConvertItems.GetLineStationToAdd(l.LineID, line);

            foreach (DO.LineStation lS in lineStationsToAdd)
                dal.AddLineStation(lS);

        }

        public void AddLine(int lineNumber, List<int> StationsCode, Areas area)
        {
            if (StationsCode.Count < 2)
                throw new Exception();//*********//  not adapt params exeption
            Line line = createLine(lineNumber, StationsCode, area);
            AddLine(line);
        }

        private Line createLine(int lineNumber, List<int> StationsCode, Areas area)
        {
            Line line = new Line
            {
                Area = area,
                FirstStation = StationsCode[0],
                LastStation = StationsCode.Last(),
                FirstStationName = dal.GetStation(StationsCode[0]).Name,
                LastStationName = dal.GetStation(StationsCode.Last()).Name,
                LineID = 0,
                LineNumber = lineNumber,
                List_LineStations = new List<LineStation>()
            };

            List<int> exeptionStations = new List<int>();
            List<AddLineExeption.StationAdjMissNumbers> exeptioMissAdjData = new List<AddLineExeption.StationAdjMissNumbers>();

            DO.Station station;
            DO.AdjacentStation adjacent;

            try 
            {
                station = dal.GetStation(StationsCode[0]);
                try 
                {
                    adjacent = dal.GetAdjacentStation(StationsCode[0], StationsCode[1]);
                    line.List_LineStations.Add(new LineStation
                    {
                        Code = StationsCode[0],
                        Distance_ToNext = adjacent.Distance,
                        LineId = line.LineID,
                        LineStationIndex = 0,
                        Name = station.Name,
                        NextStation = StationsCode[1],
                        PrevStation = -1,
                        Time_ToNext = adjacent.Time
                    });
                }
                catch (DO.ItemNotExeistExeption)
                {
                    exeptioMissAdjData.Add(new AddLineExeption.StationAdjMissNumbers { Station1 = StationsCode[0], Station2 = StationsCode[1] });
                }
            }
            catch (DO.ItemNotExeistExeption)
            {
                exeptionStations.Add(StationsCode[0]);
            }
            int i;
            for ( i = 1; i < StationsCode.Count() - 1; i++)
            {
                
                try { station = dal.GetStation(StationsCode[i]); }
                catch (DO.ItemNotExeistExeption) 
                {
                    exeptionStations.Add(StationsCode[i]);
                    continue;
                } 
                try { adjacent = dal.GetAdjacentStation(StationsCode[i], StationsCode[i + 1]); }
                catch (DO.ItemNotExeistExeption)
                {
                    exeptioMissAdjData.Add(new AddLineExeption.StationAdjMissNumbers { Station1 = StationsCode[i], Station2 = StationsCode[i + 1] });
                    continue;
                }

                line.List_LineStations.Add(new LineStation
                {
                    Code = StationsCode[i],
                    Distance_ToNext = adjacent.Distance,
                    LineId = line.LineID,
                    LineStationIndex = i,
                    Name = station.Name,
                    NextStation = StationsCode[i + 1],
                    PrevStation = StationsCode[i - 1],
                    Time_ToNext = adjacent.Time                  
                }
                );
            }

            try
            {
                station = dal.GetStation(StationsCode.Last());
                line.List_LineStations.Add(new LineStation
                {
                    Code = StationsCode[i],
                    Distance_ToNext = -1,
                    LineId = line.LineID,
                    LineStationIndex = i,
                    Name = station.Name,
                    NextStation = -1,
                    PrevStation = StationsCode[i - 1]
                });
            }
            catch (DO.ItemNotExeistExeption)
            {
                exeptionStations.Add(StationsCode[0]);
            }

            if (exeptionStations.Any() || exeptioMissAdjData.Any())
                throw new AddLineExeption(exeptionStations, exeptioMissAdjData);

            return line;
        }


        //private void saveLineStationList(List<LineStation> list)
        //{
        //    List<DO.AdjacentStation> AS_List = new List<DO.AdjacentStation>();
        //    List<DO.LineStation> doLS_List = new List<DO.LineStation>();
        //    list.LineStationListToDoObjectsLists(doLS_List, AS_List);
        //    foreach (DO.LineStation lS in doLS_List)
        //        try { dal.AddLineStation((DO.LineStation)lS.CloneNew(typeof(DO.LineStation))); }
        //        catch (DO.ItemAlreadyExeistExeption) 
        //        {
        //            DO.LineStation linSt = dal.GetLineStation(lS.LineID, lS.Code);
        //            if (!lS.Equals(linSt))
        //                throw new ItemAlreadyExeistExeption(typeof(DO.LineStation), lS.LineID, lS.Code);
        //        }
        //    foreach (DO.AdjacentStation aS in AS_List)
        //        try { dal.AddAdjacentStation((DO.AdjacentStation)aS.CloneNew(typeof(DO.AdjacentStation))); }
        //        catch (DO.ItemAlreadyExeistExeption)
        //        {
        //            //DO.AdjacentStation adSt = dal.GetAdjacentStation(aS.Statoin1, aS.Station2);
        //            //if (!aS.Equals(adSt))
        //            //    throw new ItemAlreadyExeistExeption(typeof(DO.AdjacentStation), aS.Statoin1, aS.Station2);
        //        }
        //}



        public void DeleteLine(int id)
        {
            try { dal.DeleteLine(id); }
            catch (DO.ItemNotExeistExeption)
            { throw new ItemNotExeistExeption(typeof(Line), id); }
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
            DO.Line lineDo;
            try { lineDo = dal.GetLine(id); }
            catch (DO.ItemNotExeistExeption e)
            { throw new ItemNotExeistExeption(typeof(Line), id); }

            lineDo.Clone(line);
            List<DO.LineStation> DoLs_list = dal.GetAllLineStationsBy(ls => ls.LineID == id).ToList();
            var lineStationList = createStationListFromDoObjects(DoLs_list);
            line.List_LineStations = lineStationList;
            line.FirstStationName = lineStationList[0].Name;
            line.LastStationName = lineStationList.Last().Name;

            
            return line;
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

        public int GetLineId(int lineNumber, int firstStation, int lastStation)
        {
            var l = dal.GetAllLinesBy(L => L.LineNumber == lineNumber && L.FirstStation == firstStation && L.LastStation == lastStation);
            if (!l.Any())
                throw new ItemNotExeistExeption(typeof(Line), lineNumber);
            return l.First().LineID;
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
                throw (ItemNotExeistExeption) e.CloneNew(typeof(ItemNotExeistExeption));
            }
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
