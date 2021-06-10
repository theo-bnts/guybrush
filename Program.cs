using System;

namespace Production
{
    class Program
    {
        static void Main(string[] args)
        {
            DecodedIsland i1 = new DecodedIsland("../../../islands/Scabb.clair");

            i1.DisplayAndSaveEncodedMap();
            i1.DisplayAndSaveDecodedMap();
            i1.DisplayParcels();
            i1.DisplayParcels(4);
            i1.DisplayAverageParcelsSize();
            i1.DisplayParcelSize('a');

            EncodedIsland i2 = new EncodedIsland("../../../islands/Scabb.chiffre");

            i2.DisplayAndSaveEncodedMap();
            i2.DisplayAndSaveDecodedMap();
            i2.DisplayParcels();
            i2.DisplayParcels(4);
            i2.DisplayAverageParcelsSize();
            i2.DisplayParcelSize('a');
        }
    }
}