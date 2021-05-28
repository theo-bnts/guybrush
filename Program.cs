namespace Production
{
    class Program
    {
        static void Main(string[] args)
        {
            DecryptedIsland i = new DecryptedIsland("../../../islands/Scabb.clair");

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