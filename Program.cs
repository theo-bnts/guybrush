using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Production
{
    class Program
    {
        static void Main(string[] args)
        {
            DecryptedIsland i = new DecryptedIsland("../../../islands/Scabb.chiffre");

            i.GetFileLines();
            i.Encrypt();
            i.Display();

            EncryptedIsland j = new EncryptedIsland ("../../../islands/Scabb.chiffre");

            j.GetFileLine();
            j.Decrypt();
            j.Display();
        }
    }
}