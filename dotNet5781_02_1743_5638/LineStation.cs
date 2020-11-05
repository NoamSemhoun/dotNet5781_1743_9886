using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_1743_5638
{
    enum Area { General,North,South,Center,Jerusalem }
    class LineStation       // Heritage et interface   ou interclasse dans l'autre
    {
        // meme qu'avant avec en plus,
        
        int distance; // with the precedent station 
        int time;     // avec la station précedente

    };
    
    class Line  // simple  List  BODED
    {
        List<int> trajet = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };   // masloul liste des stations
        /*
                int Kav ; 
                int FirstStation, LastStation ;

                // 1 et 2 
                bool SearchStation(int Kav,int code); // code de la station sur une ligne
                int Distance(int NumStation1, int NumStation2); // distance entre les 2 station
                int Time(int NumStation1, int NumStation2); // temp  entre les 2 station
                // 6 ET 7 iterface etc...  icomparable

            }

            class NewLine    // que 2 fois la mm ligne : dans les 2 sens 
            {*/

    }            
}
