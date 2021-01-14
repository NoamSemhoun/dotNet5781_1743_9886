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
                        LineStationIndex = 1,
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
                    LineStationIndex = i + 1,
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
                    LineStationIndex = i + 1,
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
        //            DO.LineStation linSt = dal.GetLineStation(lS.LineId, lS.Code);
        //            if (!lS.Equals(linSt))
        //                throw new ItemAlreadyExeistExeption(typeof(DO.LineStation), lS.LineId, lS.Code);
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
            List<DO.LineStation> tmp = dal.GetAllLineStationsBy(lS => lS.LineId == id).ToList();
            foreach (DO.LineStation lS in tmp)
                dal.DeleteLineStation(lS.LineId, lS.LineStationIndex);
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
            List<DO.LineStation> DoLs_list = dal.GetAllLineStationsBy(ls => ls.LineId == id).ToList();
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
                if (lS.NextStation != -1 && lS.NextStation != 0)
                    aS = dal.GetAdjacentStation(lS.Code, lS.NextStation);
                else
                    aS = new DO.AdjacentStation { Distance = 0, Time = new TimeSpan(0) };
                list.Add(new LineStation
                {
                    Code = lS.Code,
                    LineId = lS.LineId,
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

            dataCheck<DO.AdjacentStation> adjDtChecker = new dataCheck<DO.AdjacentStation>();

            List<DO.AdjacentStation> adj = (from item in line.List_LineStations
                                                  where item.NextStation != -1
                                                  select new DO.AdjacentStation { Statoin1 = item.Code, Station2 = item.NextStation, Distance = item.Distance_ToNext, Time = item.Time_ToNext }).ToList();

            updateAdjStations(adj);

            updateLineStations(line.List_LineStations);

            //var lineStList = from item in line.List_LineStations
            //                 select new DO.LineStation { Code = item.Code, LineId = item.LineId, LineStationIndex = item.LineStationIndex, NextStation = item.NextStation, PrevStation = item.PrevStation };

            //dataCheck<DO.LineStation> lSCheck = new dataCheck<DO.LineStation>();

            //foreach (DO.LineStation lS in lineStList)
            //{
            //    if (lSCheck.isExeist(l => l.Code == lS.Code && l.LineId == lS.LineId))
            //    {
            //        if (lSCheck.didNeedUpdaete(lS, l => l.Code == lS.Code && l.LineId == lS.LineId))
            //        {
            //            dal.UpdateLineStation(lS);
            //        }
            //    }
            //    else
            //    {
            //        dal.AddLineStation(lS);
            //    }
            //}

            //List<DO.LineStation> lis = dal.GetAllLineStationsBy(l => l.LineId == line.LineId).ToList(); ;
            //foreach (DO.LineStation lineStation in lis)
            //{
            //    if ((lineStList.FirstOrDefault(l => l.LineId == lineStation.LineId && l.Code == lineStation.Code) == null))
            //        dal.DeleteLineStation(lineStation.LineId, lineStation.Code);
            //}

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

            string name;
            try { name = dal.GetStation(station).Name; }
            catch (DO.ItemNotExeistExeption)
            { throw new LackOfDataExeption(DataType.StationData, station); } // ***********//data not exeist

            if (index < 1 || index > line.List_LineStations.Count())
                throw new Exception(); // *******************// incorrect input exeption;


            LineStation stationToAdd = createNewLineStation(line, index, station);

            LineStation prev = line.List_LineStations.FirstOrDefault(l => l.LineStationIndex == index - 1);

            if (prev != null)
                getUpdaedPrev(prev, stationToAdd);

            LineStation next = line.List_LineStations.FirstOrDefault(l => l.LineStationIndex == index);

            if (next != null)
                next.PrevStation = station;

            foreach (LineStation l in line.List_LineStations)
                if (l.LineStationIndex >= index)
                    l.LineStationIndex++;

            line.List_LineStations.Add(stationToAdd);

            updateLineStations(line.List_LineStations);
        }

        public void DeleteLineStation(int lineId, int index)
        {
            Line line = GetLine(lineId);

            LineStation lineStation = line.List_LineStations.FirstOrDefault(l => l.LineStationIndex == index);

            if (lineStation == null)
                throw new LackOfDataExeption(DataType.LineStation, lineId, index);

            
            LineStation prev = line.List_LineStations.FirstOrDefault(l => l.LineStationIndex == lineStation.LineStationIndex - 1);

            LineStation next = line.List_LineStations.FirstOrDefault(l => l.LineStationIndex == lineStation.LineStationIndex + 1);

            if (prev != null)
            {
                getUpdaedPrev(prev, next);
                if (next != null)
                    next.PrevStation = prev.Code;                
            }
            else if(next != null)
            {
                next.PrevStation = - 1;
            }

            foreach (LineStation l in line.List_LineStations)
            {
                if (l.LineStationIndex > lineStation.LineStationIndex)
                    l.LineStationIndex--;
            }

            updateLineStations(line.List_LineStations);

            dal.DeleteLineStation(line.LineID ,line.List_LineStations.Count());
        }

        public LineStation GetLineStation(int lineID, int index)
        {
            DO.LineStation dalLineStation = dal.GetLineStation(lineID, index);
            int nextStation;
            try { nextStation = dal.GetLineStation(lineID, index + 1).Code; }
            catch (DO.ItemNotExeistExeption)
            { nextStation = -1; }

            int prevStation;
            if (index > 0)
                prevStation = dal.GetLineStation(lineID, index - 1).Code;
            else
                prevStation = -1;
            DO.AdjacentStation adjData;
            try { adjData = dal.GetAdjacentStation(dalLineStation.Code, nextStation); }
            catch(DO.ItemNotExeistExeption)
            { throw new Exception(); } //******************//*************lack of data Exeption 

            

            return new LineStation
            {
                Code = dalLineStation.Code,
                Distance_ToNext = adjData.Distance,
                LineId = lineID,
                LineStationIndex = index,
                Time_ToNext = adjData.Time,
                NextStation = nextStation,
                PrevStation = prevStation,
                Name = dal.GetStation(dalLineStation.Code).Name
            };
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
                throw (ItemNotExeistExeption) e.CloneNew(typeof(ItemNotExeistExeption));
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

        #region LineStation

        private void updateLineStation(DO.LineStation lineStation)
        {
            try { dal.UpdateLineStation(lineStation); }
            catch { throw new ItemNotExeistExeption(typeof(LineStation), lineStation.LineId, lineStation.LineStationIndex); }
        }

        private void updateLineStations(List<LineStation> lineStations_list)
        {
            Predicate<DO.LineStation> predicate;
            dataCheck <DO.LineStation> check = new dataCheck<DO.LineStation>();
            foreach (LineStation lineStation in lineStations_list)
            {
                predicate = l => l.LineId == lineStation.LineId && l.LineStationIndex == lineStation.LineStationIndex;
                if (check.isExeist(predicate))
                {
                    updateLineStation((DO.LineStation)lineStation.CloneNew(typeof(DO.LineStation))) ;
                }
                else
                    dal.AddLineStation((DO.LineStation)lineStation.CloneNew(typeof(DO.LineStation)));
            }
        }

        private LineStation createNewLineStation(Line line, int index, int station)
        {
            LineStation stationToAdd = new LineStation { Code = station, LineId = line.LineID, LineStationIndex = index };
                        
            try { stationToAdd.Name = dal.GetStation(station).Name; }
            catch (DO.ItemNotExeistExeption)
            { throw new LackOfDataExeption(DataType.StationData, station); }
                        
            LineStation prev = line.List_LineStations.FirstOrDefault(lS => lS.LineStationIndex == index - 1);
            if (prev != null)
                stationToAdd.PrevStation = prev.Code;
            else
                stationToAdd.PrevStation =  - 1;

            LineStation next = line.List_LineStations.FirstOrDefault(lS => lS.LineStationIndex == index);

            DO.AdjacentStation adjData;
            try { adjData = dal.GetAdjacentStation(station, next.Code); }
            catch (DO.ItemNotExeistExeption)
            { throw new LackOfDataExeption(DataType.AdjacentStation, station, next.Code); }
            
            if(next != null)
            {
                stationToAdd.NextStation = next.Code;
                stationToAdd.Time_ToNext = adjData.Time;
                stationToAdd.Distance_ToNext = adjData.Distance;
            }

            return stationToAdd;

        }

        private void getUpdaedPrev(LineStation prev, LineStation station)
        {
            if (station != null)
            {
                try
                {
                    DO.AdjacentStation adjData = dal.GetAdjacentStation(prev.Code, station.Code);
                    prev.NextStation = station.Code;
                    prev.Distance_ToNext = adjData.Distance;
                    prev.Time_ToNext = adjData.Time;
                }
                catch (DO.ItemNotExeistExeption)
                { throw new LackOfDataExeption(DataType.AdjacentStation, prev.Code, station.Code); }
            }
            else if(station == null)
            {
                prev.Time_ToNext = new TimeSpan(0);
                prev.Distance_ToNext = 0;
                prev.NextStation = -1;
            }
        
        }

        #endregion




        public void UpdateAdjStations(List<AdjacentStation> adjacentStations)
        {
            dataCheck<DO.Station> dataCheck = new dataCheck<DO.Station>();

            List<DO.AdjacentStation> DoAdjStations = (from item in adjacentStations
                                                      where (dataCheck.isExeist(s => s.Code == item.Statoin1) && dataCheck.isExeist(s => s.Code == item.Station2))
                                                      select (DO.AdjacentStation)item.CloneNew(typeof(DO.AdjacentStation))).ToList();
            if (adjacentStations.Count != DoAdjStations.Count)
                throw new LackOfDataExeption(DataType.StationData, -1);

            updateAdjStations(DoAdjStations);
        }

        private void updateAdjStations(List<DO.AdjacentStation> adjacentStations)
        {
            dataCheck<DO.AdjacentStation> adjDtChecker = new dataCheck<DO.AdjacentStation>();

            foreach (DO.AdjacentStation aS in adjacentStations)
            {
                if (adjDtChecker.isExeist(a => a.Statoin1 == aS.Statoin1 && a.Station2 == aS.Station2))
                {
                    if (adjDtChecker.didNeedUpdaete(aS, a => a.Statoin1 == aS.Statoin1 && a.Station2 == aS.Station2))
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

    }
}
