using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_1743_5638
{
    public class Station            
    {

        protected int shelterNumber;
        protected double latitude;          
        protected double longitude;     
        protected string address;
        private static Random r = new Random();

        public int ShelterNumber { get => shelterNumber; set => shelterNumber = value; }


        private static List<string> listAddresses = new List<string> { "12 Chazar, Jerusalem", "30 Havaad Heleumi,Jerusalem", "21 Begin, Jerusalem", "12 Hebron Road, Jerusalem","32 Bayt vagan" ,"5 Herzl, Jerusalem" };

        protected Station(int StationNumber) 
        {
            ShelterNumber = StationNumber;
            latitude = r.NextDouble() * 2.3 + 31;    
            longitude = r.NextDouble() * 1.2 + 34.3;
            int Address = r.Next(0, listAddresses.Count());
            address = listAddresses[Address];
        }

        public override string ToString()  
        {
            return $"Bus Station Code: {shelterNumber},{latitude}\u00b0N  {longitude}°E"; //Faut il afficher l'addresse ?
        }
    }

}

