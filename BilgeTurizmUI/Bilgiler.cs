using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BilgeTurizm.DATA;

namespace BilgeTurizmUI
{
    public static class Bilgiler
    {
        // Burada yolcuların ve program içerisindeki sabit değerler tutulacak

        public static DateTime gidisTarihi { get; set; }

        public static DateTime donusTarihi { get; set; }

        public static string gidisSaati { get; set; }

        public static string donusSaati { get; set; }

        public static int neredenSehir { get; set; }

        public static int nereyeSehir { get; set; }

        public static OtobusTipi gidisOtobusTipi { get; set; }

        public static OtobusTipi donusOtobusTipi { get; set; }

        public static Dictionary<int, string> gidisSecilenKoltuklar { get; set; }

        public static Dictionary<int, string>  donusSecilenKoltuklar { get; set; }

        public static int gidisSeferID { get; set; }

        public static int  donusSeferID { get; set; }

        public static List<Yolcu> gidisMusteriler;

        public static List<Yolcu> donusMusteriler;

        public static bool rezerveMi { get; set; }

        public static SeyehatTipi seyahatTipi { get; set; }

        public static decimal ToplamFiyat { get; set; }

        public static bool SigortaVarMi { get; set; }

        public static string PNRKodu { get; set; }

    }

    public enum OtobusTipi
    {
        Standart,
        Suit
    }


    public enum SeyehatTipi
    {
        GidisDonus,
        TekYon
    }
}


