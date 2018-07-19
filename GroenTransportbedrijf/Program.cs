using System;
using System.Collections.Generic;


namespace GroenTransportbedrijf
{
    class Program
    {
        
        static Double BerekenPrijs(Double volume, Double gewicht, int afstandNL, (Double volume, Double gewicht) typelad, Double waarde, int afstandBTNL)
        {
            Double prijsNL = 0;
            Double prijsBTNL = 0;
            Double douane = 0;
            // prijs van vervoer in NL:
            // prijsNL en prijsBTNL berekenen: afsplitsen in functie
            prijsNL = (afstandNL * typelad.volume * volume) + (afstandNL * typelad.gewicht * gewicht);
            //Console.WriteLine("PrijsNL {0}", prijsNL);
            // prijs van vervoer buiten NL:
            prijsBTNL = (afstandBTNL * typelad.volume * volume) + (afstandBTNL * typelad.gewicht * gewicht);
            //Console.WriteLine("PrijsBTNL {0}", prijsBTNL);
            // prijs vervoer buiten NL + toeslag van 45%
            prijsBTNL = prijsBTNL + prijsBTNL * 0.45;
            //Console.WriteLine("PrijsBTNL + buitenland toeslag 45% {0}", prijsBTNL);
            // douane kosten over waarde
            douane = waarde * 0.035;
            //Console.WriteLine("3.5% van de waarde is {0}", douane);
            if (douane < 45 && waarde > 0)
            {
                douane = 45;
                //Console.WriteLine("3.5% van de waarde is lager dan 45 euro.");
            }
            return prijsNL + prijsBTNL + douane;
        }

        static void Main(string[] args)
        {
            //tuples voor vloeibare en niet-vloeibare stoffen prijzen per volume en gewicht
            (Double volume, Double gewicht) vloeibaar = (volume: 1.25, gewicht: 0.45);
            (Double volume, Double gewicht) nietvloeibaar = (volume: 0.80, gewicht: 0.55);
            Double volume = 0;
            Double gewicht = 0;
            Double waarde = 0;
            int afstandNL = 0;
            int afstandBTNL = 0;
            String typelading = "";
            Double prijs = 0.0;
            // tuples vloeibaar en nietvloeibaar stop ik in deze dictionary:
            Dictionary<String, (Double volume, Double gewicht)> tlading = new Dictionary<string, (double volume, double gewicht)>();
            // Dit scheelt wat regels code:
            tlading.Add("vloeibaar", vloeibaar);
            tlading.Add("niet-vloeibaar", nietvloeibaar);
                        
            // vraag naar volume van de lading
            // vraag naar gewicht van de lading
            // vraag naar type van de lading
            // vraag naar afstand binnen NL
            // vraag naar afstand buiten NL (is nul indien de rit in NL plaatsvindt)
            // vraag naar waarde van de lading (hoeven we niet te vragen indien afstandBTNL == 0

            Console.WriteLine("Transportkosten berekenen.");
            Console.WriteLine("Geef het volume van de lading in kubieke meters: ");
            volume = Double.Parse(Console.ReadLine());
            Console.WriteLine("Geef het gewicht van de lading in kilogrammen: ");
            gewicht = Double.Parse(Console.ReadLine());
            Console.WriteLine("Geef het type lading (vloeibaar / niet-vloeibaar)");
            typelading = Console.ReadLine();
            Console.WriteLine("Geef de afstand (in gehele km) binnen Nederland van het transport: ");
            afstandNL = int.Parse(Console.ReadLine());
            Console.WriteLine("Geef de afstand (in gehele km) buiten Nederland. Dit is 0 indien het transport alleen in Nederland plaatsvindt.");
            afstandBTNL = int.Parse(Console.ReadLine());
            if (afstandBTNL == 0)
            {
                prijs = BerekenPrijs(volume, gewicht, afstandNL, tlading[typelading], waarde, afstandBTNL);
                Console.WriteLine("De prijs van het transport in Nederland bedraagt: {0}", prijs);
                Environment.Exit(0);
            }
            Console.WriteLine("Geef de waarde van de lading: ");
            waarde = Double.Parse(Console.ReadLine());
            prijs = BerekenPrijs(volume, gewicht, afstandNL, tlading[typelading], waarde, afstandBTNL);
            Console.WriteLine("De prijs van het transport bedraagt: {0} euro", prijs);
        }
    }
}
