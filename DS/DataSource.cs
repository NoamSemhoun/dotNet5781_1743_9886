using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;


namespace DS
{
    public class DataSource
    {
        #region collections
        public static List<Bus> List_Buses;
        public static List<AdjacentStation> List_AdjacentStations;
        public static List<BusOnTrip> List_BusesOnTrip;
        public static List<Line> List_Lines;
        public static List<LineStation> List_LineStations;
        public static List<LineTrip> List_LineTrips;
        public static List<Station> List_Stations;
        public static List<UserTrip> List_Trips;
        public static List<User> List_Users;
        #endregion

        private static Random random = new Random();
      // FOR TEST :
        public static List<string> List_Addresses = new List<string> {"12 Chazar", "30 Havaad Heleumi", "21 Begin",
               "12 Hebron Road","32 Bayt vagan", "51 Herzl" ,"79 Ben Gourion","26 Harav Kook","61 Ben Yeouda"};   // Num between 10-99 only
        static DataSource()
        {
            InitAllLists();
        }
        static void InitAllLists()    // 50 stations ,  10lines with each 10stations,  20Buses  etc... 
       {
            #region create 10 buses Random :
            List_Buses = new List<Bus>();
            for (int i = 0; i < 20; i++)  // Create 20 Buses 
            { 
                List_Buses.Add(
                new Bus
                {
                    LicenseNum = 10000000 + i,   // 100-00-000 to 100-00-020
                    StartDate = new DateTime(2021, 01, 01),
                    Total_Km = random.Next(0, 999),
                    Fuel = random.Next(0, 1200),
                    Status = BusStatus.Available
                });


            }

            #endregion

            #region Boot 50 Real Stations  //איתחול תחנות 

            List_Stations = new List<Station> {
                new Station
                {
                    Code = 73,
                    Name = "שדרות גולדה מאיר/המשורר אצ''ג",
                    Address = "רחוב:שדרות גולדה מאיר  עיר: ירושלים ",
                    Latitude = 31.825302,
                    Longitude = 35.188624
                },
                new Station
                {
                    Code = 76,
                    Name = "בית ספר צור באהר בנות/אלמדינה אלמונוורה",
                    Address = "רחוב:אל מדינה אל מונאוורה  עיר: ירושלים",
                    Latitude = 31.738425,
                    Longitude = 35.228765
                },
                new Station
                {
                    Code = 77,
                    Name = "בית ספר אבן רשד/אלמדינה אלמונוורה",
                    Address = "רחוב:אל מדינה אל מונאוורה  עיר: ירושלים ",
                    Latitude = 31.738676,
                    Longitude = 35.226704
                },
                new Station
                {
                    Code = 78,
                    Name = "שרי ישראל/יפו",
                    Address = "רחוב:שדרות שרי ישראל 15 עיר: ירושלים",
                    Latitude = 31.789128,
                    Longitude = 35.206146
                },
                new Station
                {
                    Code = 83,
                    Name = "בטן אלהווא/חוש אל מרג",
                    Address = "רחוב:בטן אל הווא  עיר: ירושלים",
                    Latitude = 31.766358,
                    Longitude = 35.240417
                },
                new Station
                {
                    Code = 84,
                    Name = "מלכי ישראל/הטורים",
                    Address = " רחוב:מלכי ישראל 77 עיר: ירושלים ",
                    Latitude = 31.790758,
                    Longitude = 35.209791
                },
                new Station
                {
                    Code = 85,
                    Name = "בית ספר לבנים/אלמדארס",
                    Address = "רחוב:אלמדארס  עיר: ירושלים",
                    Latitude = 31.768643,
                    Longitude = 35.238509
                },
                new Station
                {
                    Code = 86,
                    Name = "מגרש כדורגל/אלמדארס",
                    Address = "רחוב:אלמדארס  עיר: ירושלים",
                    Latitude = 31.769899,
                    Longitude = 35.23973
                },
                new Station
                {
                    Code = 88,
                    Name = "בית ספר לבנות/בטן אלהוא",
                    Address = " רחוב:בטן אל הווא  עיר: ירושלים",
                    Latitude = 31.767064,
                    Longitude = 35.238443
                },
                new Station
                {
                    Code = 89,
                    Name = "דרך בית לחם הישה/ואדי קדום",
                    Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים ",
                    Latitude = 31.765863,
                    Longitude = 35.247198
                },
                new Station
                {
                    Code = 90,
                    Name = "גולדה/הרטום",
                    Address = "רחוב:דרך בית לחם הישנה  עיר: ירושלים",
                    Latitude = 31.799804,
                    Longitude = 35.213021
                },
                new Station
                {
                    Code = 91,
                    Name = "דרך בית לחם הישה/ואדי קדום",
                    Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים ",
                    Latitude = 31.765717,
                    Longitude = 35.247102
                },
                new Station
                {
                    Code = 93,
                    Name = "חוש סלימה 1",
                    Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים",
                    Latitude = 31.767265,
                    Longitude = 35.246594
                },
                new Station
                {
                    Code = 94,
                    Name = "דרך בית לחם הישנה ב",
                    Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים",
                    Latitude = 31.767084,
                    Longitude = 35.246655
                },
                new Station
                {
                    Code = 95,
                    Name = "דרך בית לחם הישנה א",
                    Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים",
                    Latitude = 31.768759,
                    Longitude = 31.768759
                },
                new Station
                {
                    Code = 97,
                    Name = "שכונת בזבז 2",
                    Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים",
                    Latitude = 31.77002,
                    Longitude = 35.24348
                },
                new Station
                {
                    Code = 102,
                    Name = "גולדה/שלמה הלוי",
                    Address = " רחוב:שדרות גולדה מאיר  עיר: ירושלים",
                    Latitude = 31.8003,
                    Longitude = 35.208257
                },
                new Station
                {
                    Code = 103,
                    Name = "גולדה/הרטום",
                    Address = " רחוב:שדרות גולדה מאיר  עיר: ירושלים",
                    Latitude = 31.8,
                    Longitude = 35.214106
                },
                new Station
                {
                    Code = 105,
                    Name = "גבעת משה",
                    Address = " רחוב:גבעת משה 2 עיר: ירושלים",
                    Latitude = 31.797708,
                    Longitude = 35.217133
                },
                new Station
                {
                    Code = 106,
                    Name = "גבעת משה",
                    Address = " רחוב:גבעת משה 3 עיר: ירושלים",
                    Latitude = 31.797535,
                    Longitude = 35.217057
                },
                //20
                new Station
                {
                    Code = 108,
                    Name = "עזרת תורה/עלי הכהן",
                    Address = "  רחוב:עזרת תורה 25 עיר: ירושלים",
                    Latitude = 31.797535,
                    Longitude = 35.213728
                },
                new Station
                {
                    Code = 109,
                    Name = "עזרת תורה/דורש טוב",
                    Address = "  רחוב:עזרת תורה 21 עיר: ירושלים ",
                    Latitude = 31.796818,
                    Longitude = 35.212936
                },
                new Station
                {
                    Code = 110,
                    Name = "עזרת תורה/דורש טוב",
                    Address = " רחוב:עזרת תורה 12 עיר: ירושלים",
                    Latitude = 31.796129,
                    Longitude = 35.212698
                },
                new Station
                {
                    Code = 111,
                    Name = "יעקובזון/עזרת תורה",
                    Address = "  רחוב:יעקובזון 1 עיר: ירושלים",
                    Latitude = 31.794631,
                    Longitude = 35.21161
                },
                new Station
                {
                    Code = 112,
                    Name = "יעקובזון/עזרת תורה",
                    Address = " רחוב:יעקובזון  עיר: ירושלים",
                    Latitude = 31.79508,
                    Longitude = 35.211684
                },
                //25
                new Station
                {
                    Code = 113,
                    Name = "זית רענן/אוהל יהושע",
                    Address = "  רחוב:זית רענן 1 עיר: ירושלים",
                    Latitude = 31.796255,
                    Longitude = 35.211065
                },
                new Station
                {
                    Code = 115,
                    Name = "זית רענן/תורת חסד",
                    Address = " רחוב:זית רענן  עיר: ירושלים",
                    Latitude = 31.798423,
                    Longitude = 35.209575
                },
                new Station
                {
                    Code = 116,
                    Name = "זית רענן/תורת חסד",
                    Address = "  רחוב:הרב סורוצקין 48 עיר: ירושלים ",
                    Latitude = 31.798689,
                    Longitude = 35.208878
                },
                new Station
                {
                    Code = 117,
                    Name = "קרית הילד/סורוצקין",
                    Address = "  רחוב:הרב סורוצקין  עיר: ירושלים",
                    Latitude = 31.799165,
                    Longitude = 35.206918
                },
                new Station
                {
                    Code = 119,
                    Name = "סורוצקין/שנירר",
                    Address = "  רחוב:הרב סורוצקין 31 עיר: ירושלים",
                    Latitude = 31.797829,
                    Longitude = 35.205601
                },

                new Station
                {
                    Code = 1485,
                    Name = "שדרות נווה יעקוב/הרב פרדס ",
                    Address = "רחוב: שדרות נווה יעקוב  עיר:ירושלים ",
                    Latitude = 31.840063,
                    Longitude = 35.240062

                },
                new Station
                {
                    Code = 1486,
                    Name = "מרכז קהילתי /שדרות נווה יעקוב",
                    Address = "רחוב:שדרות נווה יעקוב ירושלים עיר:ירושלים ",
                    Latitude = 31.838481,
                    Longitude = 35.23972
                },


                new Station
                {
                    Code = 1487,
                    Name = " מסוף 700 /שדרות נווה יעקוב ",
                    Address = "חוב:שדרות נווה יעקב 7 עיר: ירושלים  ",
                    Latitude = 31.837748,
                    Longitude = 35.231598
                },
                new Station
                {
                    Code = 1488,
                    Name = " הרב פרדס/אסטורהב ",
                    Address = "רחוב:מעגלות הרב פרדס  עיר: ירושלים רציף  ",
                    Latitude = 31.840279,
                    Longitude = 35.246272
                },
                new Station
                {
                    Code = 1490,
                    Name = "הרב פרדס/צוקרמן ",
                    Address = "רחוב:מעגלות הרב פרדס 24 עיר: ירושלים   ",
                    Latitude = 31.843598,
                    Longitude = 35.243639
                },
                new Station
                {
                    Code = 1491,
                    Name = "ברזיל ",
                    Address = "רחוב:ברזיל 14 עיר: ירושלים",
                    Latitude = 31.766256,
                    Longitude = 35.173
                },
                new Station
                {
                    Code = 1492,
                    Name = "בית וגן/הרב שאג ",
                    Address = "רחוב:בית וגן 61 עיר: ירושלים ",
                    Latitude = 31.76736,
                    Longitude = 35.184771
                },
                new Station
                {
                    Code = 1493,
                    Name = "בית וגן/עוזיאל ",
                    Address = "רחוב:בית וגן 21 עיר: ירושלים    ",
                    Latitude = 31.770543,
                    Longitude = 35.183999
                },
                new Station
                {
                    Code = 1494,
                    Name = " קרית יובל/שמריהו לוין ",
                    Address = "רחוב:ארתור הנטקה  עיר: ירושלים    ",
                    Latitude = 31.768465,
                    Longitude = 35.178701
                },
                new Station
                {
                    Code = 1510,
                    Name = " קורצ'אק / רינגלבלום ",
                    Address = "רחוב:יאנוש קורצ'אק 7 עיר: ירושלים",
                    Latitude = 31.759534,
                    Longitude = 35.173688
                },
                new Station
                {
                    Code = 1511,
                    Name = " טהון/גולומב ",
                    Address = "רחוב:יעקב טהון  עיר: ירושלים     ",
                    Latitude = 31.761447,
                    Longitude = 35.175929
                },
                new Station
                {
                    Code = 1512,
                    Name = "הרב הרצוג/שח''ל ",
                    Address = "רחוב:הרב הרצוג  עיר: ירושלים רציף",
                    Latitude = 31.761447,
                    Longitude = 35.199936
                },
                new Station
                {
                    Code = 1514,
                    Name = "פרץ ברנשטיין/נזר דוד ",
                    Address = "רחוב:הרב הרצוג  עיר: ירושלים רציף",
                    Latitude = 31.759186,
                    Longitude = 35.189336
                },


             new Station
             {
                 Code = 1518,
                 Name = "פרץ ברנשטיין/נזר דוד",
                 Address = " רחוב:פרץ ברנשטיין 56 עיר: ירושלים ",
                 Latitude = 31.759121,
                 Longitude = 35.189178
             },
              new Station
              {
                  Code = 1522,
                  Name = "מוזיאון ישראל/רופין",
                  Address = "  רחוב:דרך רופין  עיר: ירושלים ",
                  Latitude = 31.774484,
                  Longitude = 35.204882
              },

             new Station
             {
                 Code = 1523,
                 Name = "הרצוג/טשרניחובסקי",
                 Address = "   רחוב:הרב הרצוג  עיר: ירושלים  ",
                 Latitude = 31.769652,
                 Longitude = 35.208248
             },
              new Station
              {
                  Code = 1524,
                  Name = "רופין/שד' הזז",
                  Address = "    רחוב:הרב הרצוג  עיר: ירושלים   ",
                  Latitude = 31.769652,
                  Longitude = 35.208248,
              },
                new Station
                {
                    Code = 121,
                    Name = "מרכז סולם/סורוצקין ",
                    Address = " רחוב:הרב סורוצקין 13 עיר: ירושלים",
                    Latitude = 31.796033,
                    Longitude = 35.206094
                },
                new Station
                {
                    Code = 123,
                    Name = "אוהל דוד/סורוצקין ",
                    Address = "  רחוב:הרב סורוצקין 9 עיר: ירושלים",
                    Latitude = 31.794958,
                    Longitude = 35.205216
                },
                new Station
                {
                    Code = 122,
                    Name = "מרכז סולם/סורוצקין ",
                    Address = "  רחוב:הרב סורוצקין 28 עיר: ירושלים",
                    Latitude = 31.79617,
                    Longitude = 35.206158
                }};

            #endregion

            #region Create 50 Stations with Random Location :       

            //for (int i = 0; i < 50; i++)  // Create 50 Stations with Random Location
            //{
            //    List_Stations.Add(
            //       new Station
            //       {
            //           Code = 10000 + i,            //  or : random.Next(0,999)    (code existing !)
            //          Longitude = random.NextDouble() * 1.2 + 34.3,       // In ISRAEL :  [34.3, 35.5]
            //          Latitude = random.NextDouble() * 2.3 + 31,          //              [31, 33.3]
            //          Address = List_Addresses[i % List_Addresses.Count()],
            //           Name = List_Addresses[i % List_Addresses.Count()].Substring(3) + "/" + List_Addresses[i % List_Addresses.Count()].Substring(3) //    Begin/Hebron  9=this Adress  (??
            //      });
            //}
            #endregion

            #region Create 10 Lines With 10 Line stations each, with Random Location :       

            List_Lines = new List<Line>(); // Create 10  Line 
            List_LineTrips = new List<LineTrip>();
            List_LineStations = new List<LineStation>();   // With 10 Line stations each ( Masloul)
            for (int i = 0; i < 10; i++)
            {
                int thisStation = List_Stations[random.Next(0, 49)].Code;
                List_Lines.Add(
                new Line {
                    LineID = DO.statics.LineId++,
                    Area = (Areas)random.Next(0, 5),
                    LineNumber =  random.Next(1, 999),      // CodeLine ??
                    FirstStation = thisStation
                });

                int pStation = 0;
                 
                for (int j = 0; j < 10; j++)
                {
                    List_LineStations.Add(    // Create 10 LineStation for THIS Line
                        new LineStation {
                            LineId= 1 + i ,
                            LineStationIndex= 1+j,   // x/10
                            Code = thisStation,
                            NextStation = List_Stations[random.Next(0, 49)].Code,  // The Next of the LastStation = 0
                            PrevStation = pStation,//  The Prev of the FirstStation = 0
                            Num_Stations = 10      
                        } );
                    pStation = thisStation;
                    thisStation = List_LineStations.Last().NextStation;
                }
                List_LineStations.Last().NextStation = 0;

                List_Lines.Last().LastStation = List_LineStations.Last().Code;

                for (int j = 1; j <= 3; j++)
                {
                    List_LineTrips.Add(new LineTrip
                    {
                        Id = DO.statics.LineTripId++,
                        LineID = i + 1,
                        StartAt = new TimeSpan((j * 6) - 1,0,0),
                        FinishAt = new TimeSpan((j+1) * 6 - 1, 0, 0),
                        Frequency = new TimeSpan(0, j * 10, 0)
                    });
                }
            }

            List_AdjacentStations = new List<AdjacentStation>();
            foreach (LineStation l in List_LineStations)
            {
                if(!List_AdjacentStations.Any() || List_AdjacentStations.FirstOrDefault(a => a.Statoin1 == l.Code && a.Station2 == l.NextStation) == null)
                    List_AdjacentStations.Add(new AdjacentStation
                    {
                        Statoin1 = l.Code,
                        Station2 = l.NextStation,
                        Distance = random.NextDouble() * 100,
                        Time = new TimeSpan(0, random.Next(60), random.Next(60))
                    }) ;
            }

            
            

            #endregion
        }
    }
}

