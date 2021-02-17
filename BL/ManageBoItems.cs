using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BL
{
    static class ManageBoItems
    {
        #region Line

        private static DalApi.IDAL dal = DalApi.DalFactory.GetDal();

        internal static Line CreateLine(int lineNumber, List<int> StationsCode, Areas area)
        {
            Line line = new Line
            {
                Area = area,
                LineID = 0,
                LineNumber = lineNumber
            };

            setLineStationList(line, StationsCode);

            return line;
        }

        private static void setLineStationList(Line line, List<int> StationsCode)
        {
            line.List_LineStations = createLineStatationList(StationsCode).ToList();
            setFirstAndLast(line);
        }

        private static void setFirstAndLast(Line line)
        {
            line.LastStation = line.List_LineStations.Last().Code;
            line.FirstStation = line.List_LineStations[0].Code;

            line.LastStationName = line.List_LineStations.Last().Name;
            line.FirstStationName = line.List_LineStations[0].Name;
        }
        
        private static IEnumerable<LineStation> createLineStatationList(List<int> StationCode)
        {
            List<LineStation> lineStationList = new List<LineStation>();
            List<int> missStations = new List<int>();
            List<AddLineExeption.StationAdjMissNumbers> missAdjStation = new List<AddLineExeption.StationAdjMissNumbers>();
            
            if (StationCode.Count() < 2)
                throw new Exception("bad input Exeption");
            try { lineStationList.Add(CreateLineStation(0, 1, StationCode[0], StationCode[1], 0)); }
            catch (LackOfDataExeption e)
            {
                if (e.Data == DataType.StationData)
                    missStations.Add(StationCode[0]);
                else
                    missAdjStation.Add(new AddLineExeption.StationAdjMissNumbers { Station1 = StationCode[0], Station2 = StationCode[1] });
            }
            int i = 1;
            for (; i < StationCode.Count - 1; i++)
                try { lineStationList.Add(CreateLineStation(0, i + 1, StationCode[i], StationCode[i + 1], StationCode[i - 1])); }
                catch (LackOfDataExeption e)
                {
                    if (e.Data == DataType.StationData)
                        missStations.Add(StationCode[i]);
                    else
                        missAdjStation.Add(new AddLineExeption.StationAdjMissNumbers { Station1 = StationCode[i], Station2 = StationCode[i + 1] });
                }
            try { lineStationList.Add(CreateLineStation(0, i + 1, StationCode[i], 0, StationCode[i - 1])); }
            catch (LackOfDataExeption e)
            {
                if (e.Data == DataType.StationData)
                    missStations.Add(StationCode[0]);
            }

            if (missAdjStation.Any() || missStations.Any())
                throw new AddLineExeption(missStations, missAdjStation);

            return lineStationList;
        }

        internal static void AddStation(this Line line, int stationCode, int index)
        {
            line.List_LineStations = line.List_LineStations.Select(lS =>
                                                                        {
                                                                            if (lS.LineStationIndex >= index)
                                                                            {
                                                                                lS.LineStationIndex++;
                                                                                return lS;
                                                                            }
                                                                            return lS;
                                                                        }).ToList();
            LineStation prev = line.List_LineStations.FirstOrDefault(lS => lS.LineStationIndex == index - 1);
            LineStation next = line.List_LineStations.FirstOrDefault(lS => lS.LineStationIndex == index + 1);

            int prevCode = 0, nextCode = 0; 

            if (prev != null)
            {
                prev.NextStation = stationCode;
                setAdjData(prev);
                prevCode = prev.Code;
            }
            if (next != null)
            {
                next.PrevStation = stationCode;
                nextCode = next.Code;
            }

            line.List_LineStations.Add(CreateLineStation(line.LineID, index, stationCode, nextCode, prevCode));
        }

        internal static void DeleteStation(this Line line,  int index)
        {
            LineStation lineStation = line.List_LineStations.FirstOrDefault(lS => lS.LineStationIndex == index);
            if (line.List_LineStations.Count == 2 || lineStation == null)
                throw new Exception("bad input exeption");
            line.List_LineStations.Remove(lineStation);

            if (index == line.List_LineStations.Count() + 1)
            {
                LineStation newLast = line.GetLineStation(index - 1);
                newLast.setLast();
                line.LastStation = newLast.Code;
            }
            else if (index != 1)
            {
                line.GetLineStation(index - 1).setNextStation(line.GetLineStation(index + 1));
            }
            
            foreach (LineStation lS in line.List_LineStations)
                if (lS.LineStationIndex > index)
                    lS.LineStationIndex--;
            if (index == 1)
            {
                        line.FirstStation = line.GetLineStation(1).Code;
            }
        }
        
        internal static void DeleteStation(this Line line, LineStation lineStation)
        {
            line.DeleteStation(lineStation.LineStationIndex);
        }

        internal static LineStation GetLineStation(this Line line, int index)
        {
            return line.List_LineStations.FirstOrDefault(lS => lS.LineStationIndex == index);
        }
        #endregion

        #region LineStation
        internal static LineStation CreateLineStation(int lineId, int index, int code, int next, int prev)
        {   
            LineStation lineStation = new LineStation
            {
                Code = code,
                LineId = lineId,
                LineStationIndex = index,
                NextStation = next,
                PrevStation = prev,
            };
            if(lineStation.NextStation > 0)
                setAdjData(lineStation);
            setStationName(lineStation);
            return lineStation;
        }

        

        private static void setAdjData(LineStation lineStation)
        {
            DO.AdjacentStation adj;
            try
            {
                adj = dal.GetAdjacentStation(lineStation.Code, lineStation.NextStation);
            }
            catch (DO.ItemNotExeistExeption)
            {
                throw new LackOfDataExeption(DataType.AdjacentStation, lineStation.Code, lineStation.NextStation);
            }
        }

        private static void setStationName(LineStation lineStation )
        {
            try
            {
                lineStation.Name = dal.GetStation(lineStation.Code).Name;
            }
            catch (DO.ItemNotExeistExeption)
            {
                throw new LackOfDataExeption(DataType.StationData, lineStation.Code);
            }
        }
    
        private static LineStation setNextStation (this LineStation first, LineStation second)
        {
            DO.AdjacentStation adj;
            try { adj = dal.GetAdjacentStation(first.Code, second.Code); }
            catch (DO.ItemNotExeistExeption)
            { throw new LackOfDataExeption(DataType.AdjacentStation, first.Code, second.Code); }

            first.NextStation = second.Code;
            first.Distance_ToNext = adj.Distance;
            first.Time_ToNext = adj.Time;

            second.LineStationIndex = first.LineStationIndex + 1;
            second.PrevStation = first.Code;

            return second;
        }
    
        private static void setLast(this LineStation lineStation)
        {
            lineStation.NextStation = 0;
            lineStation.Time_ToNext = new TimeSpan(0);
            lineStation.Distance_ToNext = 0;
        }
        #endregion

     

    }
}
