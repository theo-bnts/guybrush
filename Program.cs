using System;

namespace Production
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("█▀ █▀▀ ▄▀█ █▄▄ █▄▄");
            Console.WriteLine("▄█ █▄▄ █▀█ █▄█ █▄█");
            Console.WriteLine();

            DecodedIsland scabb = new DecodedIsland("../../../islands/Scabb.clair");

            scabb.DisplayAndSaveEncodedMap();
            scabb.DisplayAndSaveDecodedMap();
            scabb.DisplayParcels();
            scabb.DisplayParcels(4);
            scabb.DisplayAverageParcelsSize();
            scabb.DisplayParcelSize('a');



            Console.WriteLine("█▀█ █ █ ▄▀█ ▀█▀ ▀█▀");
            Console.WriteLine("█▀▀ █▀█ █▀█  █   █ ");
            Console.WriteLine();

            EncodedIsland phatt = new EncodedIsland("../../../islands/Phatt.chiffre");

            phatt.DisplayAndSaveEncodedMap();
            phatt.DisplayAndSaveDecodedMap();
            phatt.DisplayParcels();
            phatt.DisplayParcels(4);
            phatt.DisplayAverageParcelsSize();
            phatt.DisplayParcelSize('a');
        }
    }
}