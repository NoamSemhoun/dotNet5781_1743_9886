using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BL
{
    class ManageDoData
    {

        static DalApi.IDAL dal = DalApi.DalFactory.GetDal();

        public static void AddLine(Line line)
        {
            LineDoData lineData = new LineDoData(line);

            if (!lineData.didStationsExeist())
                throw new LackOfDataExeption(DataType.StationData, 000);

            try
            {
                dal.AddLine(lineData.DOLine);
                int lineId = dal.GetAllLinesBy(l => l.LineNumber == line.LineNumber && l.FirstStation == line.FirstStation && l.LastStation == line.LastStation).FirstOrDefault().LineID;
                foreach (DO.LineStation lineStation in lineData.DOLineStationsList)
                {
                    lineStation.LineId = lineId;
                    dal.AddLineStation(lineStation);
                }
                foreach (DO.AdjacentStation adj in lineData.DOAdjacentStationsList)
                {
                    try { dal.AddAdjacentStation(adj); }
                    catch (DO.ItemAlreadyExeistExeption)
                    { }
                }
            }
            catch (DO.ItemAlreadyExeistExeption)
            {
                throw new ItemAlreadyExeistExeption(typeof(Line), line.LineID);
            }
        }
        
        public static void DeleteLine(int id)
        {
            int index;
            try
            {
                index = dal.GetAllLineStationsBy(lS => lS.LineId == id).OrderBy(lS => lS.LineStationIndex).Last().LineStationIndex;
            }
            catch
            { index = 0; }
            DeleteLine(id, index);
        }

        public static void DeleteLine(Line line)
        {
            DeleteLine(line.LineID, line.List_LineStations.Count());
        }

        private static void DeleteLine(int lineId, int lastIndex)
        {
            try
            {
                dal.DeleteLine(lineId);
                for (int i = 1; i <= lastIndex; i++)
                    dal.DeleteLineStation(lineId, i);
            }
            catch (DO.ItemNotExeistExeption e)
            { throw new ItemNotExeistExeption(e.ItemType, e.Id); }
        }

        public static void UpdateLine(Line line)
        {
            LineDoData lineData = new LineDoData(line);

            if (!lineData.didAdjacentStationsExeist(out int first, out int second))
                throw new LackOfDataExeption(DataType.AdjacentStation, first, second);

            try { dal.UpdateLine(lineData.DOLine); }
            catch (DO.ItemNotExeistExeption)
            { throw new ItemNotExeistExeption(typeof(Line), line.LineID); }

            deleteUnnecessaryStations(line);
            UpdateLineStations(line);            
        }

        private static void deleteUnnecessaryStations(Line line)
        {
            var stationsForDelete = from item in dal.GetAllLineStations()
                                    where item.LineId == line.LineID && item.LineStationIndex > line.List_LineStations.Count
                                    select new { lineID = item.LineId, index = item.LineStationIndex };

            foreach (var v in stationsForDelete)
                dal.DeleteLineStation(v.lineID, v.index);
        }

        private static void UpdateLineStations(Line line)
        {
            foreach (LineStation lineStation in line.List_LineStations)
                try { UpdateLineStation(lineStation); }
                catch (ItemNotExeistExeption)
                {
                    AddLineStation(lineStation);
                }
        }

        public static void AddLineStation(LineStation lineStation)
        {
            if (lineStation.NextStation != 0 && !DataCheck.isExeist<DO.AdjacentStation>(aS => aS.Statoin1 == lineStation.Code && aS.Station2 == lineStation.NextStation) )
                throw new LackOfDataExeption(DataType.AdjacentStation, lineStation.Code, lineStation.NextStation);
            dal.AddLineStation((DO.LineStation)lineStation.CloneNew(typeof(DO.LineStation)));
        }

        public static void UpdateLineStation(LineStation lineStation)
        {
            if (!DataCheck.isExeist<DO.AdjacentStation>(aS => aS.Statoin1 == lineStation.Code && aS.Station2 == lineStation.NextStation) && lineStation.NextStation > 0)
                throw new LackOfDataExeption(DataType.AdjacentStation, lineStation.PrevStation, lineStation.Code);
            try { dal.UpdateLineStation((DO.LineStation)lineStation.CloneNew(typeof(DO.LineStation))); }
            catch (DO.ItemNotExeistExeption)
            { throw new ItemNotExeistExeption(typeof(LineStation), lineStation.LineId, lineStation.LineStationIndex); }
        }

        internal static void DeleteLineStation(LineStation lineStation)
        {
            Line line = GetBOItems.GetLine(l => l.LineID == lineStation.LineId);
            line.DeleteStation(lineStation);
            UpdateLine(line);
        }
    }
}
