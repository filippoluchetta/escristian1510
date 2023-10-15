namespace escristianpreverifica
{
    public class Artista
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Cognome { get; set; } = null!;
        public string? Nazionalita { get; set; }

        public override string ToString()
        {
            return string.Format($"[ID = {Id}, Nome = {Nome},  Cognome = {Cognome}, Nazionalità = {Nazionalita}]"); ;
        }
    }

    public class Personaggio
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public int FkOperaId { get; set; }
        public override string ToString()
        {
            return string.Format($"[ID = {Id}, Nome = {Nome}, FkOperaId = {FkOperaId}]"); ;
        }
    }

    public class Opera
    {
        public int Id { get; set; }
        public string Titolo { get; set; } = null!;
        public decimal Quotazione { get; set; }
        public int FkArtista { get; set; }
        public override string ToString()
        {
            return String.Format($"[ID = {Id}, Titolo = {Titolo}, Quotazione = {Quotazione},  FkArtista = {FkArtista}]"); ;
        }
    }

    internal class Program
    {
        static List<Artista> artisti = new List<Artista>()
        {
            new (){Id=1, Cognome="Picasso", Nome="Pablo", Nazionalita="Spagna"},
            new (){Id=2, Cognome="Dalì", Nome="Salvador", Nazionalita="Spagna"},
            new (){Id=3, Cognome="De Chirico", Nome="Giorgio", Nazionalita="Italia"},
            new (){Id=4, Cognome="Guttuso", Nome="Renato", Nazionalita="Italia"}
        };

        //poi le collection che hanno Fk
        static List<Opera> opere = new List<Opera>()
        {
            new (){Id=1, Titolo="Guernica", Quotazione=50000000.00m , FkArtista=1},//opera di Picasso
            new (){Id=2, Titolo="I tre musici", Quotazione=15000000.00m, FkArtista=1},//opera di Picasso
            new (){Id=3, Titolo="Les demoiselles d’Avignon", Quotazione=12000000.00m,  FkArtista=1},//opera di Picasso
            new (){Id=4, Titolo="La persistenza della memoria", Quotazione=16000000.00m,  FkArtista=2},//opera di Dalì
            new (){Id=5, Titolo="Metamorfosi di Narciso", Quotazione=8000000.00m, FkArtista=2},//opera di Dalì
            new (){Id=6, Titolo="Le Muse inquietanti", Quotazione=22000000.00m,  FkArtista=3},//opera di De Chirico
        };

        static List<Personaggio> personaggi = new List<Personaggio>()
        {
            new (){Id=1, Nome="Uomo morente", FkOperaId=1},//un personaggio di Guernica 
            new (){Id=2, Nome="Un musicante", FkOperaId=2},
            new (){Id=3, Nome="una ragazza di Avignone", FkOperaId=3},
            new (){Id=4, Nome="una seconda ragazza di Avignone", FkOperaId=3},
            new (){Id=5, Nome="Narciso", FkOperaId=5},
            new (){Id=6, Nome="Una musa metafisica", FkOperaId=6},
        };

        static void Q1(string cognomeautoredacercare)
        {
            var listaautore = artisti.Where(a => a.Cognome == cognomeautoredacercare).Join(opere,
                a => a.Id,
                o => o.FkArtista,
                (a, o) => o);
            foreach (var item in listaautore)
            {
                Console.WriteLine(item);
            }
        }

        static void Q2()
        {
            var listaartisti = artisti.GroupBy(a => a.Nazionalita);
            foreach (var gruppo in listaartisti)
            {
                Console.WriteLine($"gli artisti della nazionalità {gruppo.Key} sono:");
                foreach (var item in gruppo)
                {
                    Console.WriteLine(item);
                }
            }
        }

        static void Q3()
        {
            var listaartisti = artisti.GroupBy(a => a.Nazionalita);
            foreach (var gruppo in listaartisti)
            {
                Console.WriteLine($"gli artisti della nazionalità {gruppo.Key} sono {gruppo.Count()}");

            }
        }

        static void Q4()
        {
            var listaartisti = artisti.Where(a => a.Cognome == "Picasso").Join(opere,
                a => a.Id,
                o => o.FkArtista,
                (a, o) => new { a, o });
            Console.WriteLine($"la media delle opere di picasso è {listaartisti.Average(o => o.o.Quotazione)}");
            Console.WriteLine($"il max delle opere di picasso è {listaartisti.Max(o => o.o.Quotazione)}");
            Console.WriteLine($"il min delle opere di picasso è {listaartisti.Min(o => o.o.Quotazione)}");

        }

        static void Q5()
        {
            var listaartisti = opere.GroupBy(o => o.FkArtista).Join(artisti,
                g => g.Key,
                a => a.Id,
                (g, a) => new { g, a });
            foreach (var item in listaartisti)
            {
                Console.WriteLine($"la media delle opere di {item.a.Cognome} è {item.g.Average(o => o.Quotazione)}");
                Console.WriteLine($"il max delle opere di {item.a.Cognome} è {item.g.Max(o => o.Quotazione)}");
                Console.WriteLine($"il min delle opere di {item.a.Cognome} è {item.g.Min(o => o.Quotazione)}");
            }
        }

        static void Main(string[] args)
        {
            //Q1("Picasso");
            //Console.WriteLine("-------------------------------------------");
            //Q2();
            //Console.WriteLine("-------------------------------------------");
            //Q3();
            //Console.WriteLine("-------------------------------------------");
            //Q4();
            //Console.WriteLine("-------------------------------------------");
            Q5();
            //Console.WriteLine("-------------------------------------------");
            //Console.WriteLine("-------------------------------------------");
            //Console.WriteLine("-------------------------------------------");
            //Console.WriteLine("-------------------------------------------");

        }
    }
}