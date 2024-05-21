using ClassLibrary2.Models;
using LogCommon;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try 
            {
                if(args.Length == 0 || args.Length < 2)
                {
                    Console.WriteLine("hiba");
                    return;
                }
                if (!args[0].Equals("-InputFile"))
                {
                    Console.WriteLine("hiba");
                    return;
                }
                StreamReader Name = new StreamReader(args[1]);

                if(Name == null)
                {
                    Console.WriteLine("Ez a fájl nem található!");
                    return;
                }

                int sorokszama = 0;
                int beolvasottsorokszama = 0;
                string sor = "";
                sor = Name.ReadLine();
                while ((sor == Name.ReadLine()) != null)
                {
                    sorokszama++;
                    using(SQL sql = new SQL())
                    {
                        LogEntry elsoosztaly = new LogEntry();
                        string[] tomb = sor.Split(";");
                        elsoosztaly.Id = int.Parse(tomb[0]);
                        elsoosztaly.CorrelationId = tomb[1];
                        elsoosztaly.DateUtc = DateTime.Parse(tomb[2]);
                        elsoosztaly.Thread = int.Parse(tomb[3]);
                        elsoosztaly.level = tomb[4];
                        elsoosztaly.Logger = tomb[5];
                        elsoosztaly.Message = tomb[6];
                        elsoosztaly.Exception = tomb[7];

                        sql.logEntries.Add(elsoosztaly);

                        sql.SaveChanges();
                        beolvasottsorokszama++;

                    }

                    
                }
                Console.WriteLine("Fájl neve: " + args[1]);
                Console.WriteLine("Fájlban lévő sorokszáma: "+ sorokszama);
                Console.WriteLine("Beolvasott sorokszáma: "+ beolvasottsorokszama);


            }catch(
            Exception ex)
            {
                Console.WriteLine("Hiba!");
            }
        }
    }
}