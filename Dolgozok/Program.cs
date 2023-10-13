using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DolgozokProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dolgozok dolgozok = new Dolgozok("dolgozok.csv");
            Dolgozo dolgozo = new Dolgozo("Teszt Elek", "férfi", 25, 12000, dolgozok);
            AdatLekerdezesek adatLeker = new AdatLekerdezesek(dolgozok);

            dolgozok.AdatHozzaadas(dolgozo);
            Console.WriteLine(dolgozok.DolgozokLista.Last().ToString());
            Console.WriteLine("---------------------------------------");

            dolgozok.EgyDolgozoAdatKiiras(1);
            dolgozok.AdatModositas(1, "Uj Teszt", "nő", 30, 15000);
            dolgozok.EgyDolgozoAdatKiiras(1);
            Console.WriteLine("----------------------------------------");

            dolgozok.AdatTorles(1);
            Console.WriteLine(dolgozok.DolgozokLista.First().ToString()); ;
            Console.WriteLine("----------------------------------------");

            Console.WriteLine($"Dolgozók átlag életkora: {adatLeker.dolgozokAtlagEletkora()}");
            Console.WriteLine($"Dolgozók átlag fizetése: {adatLeker.dolgozokAtlagFizetese()}");
            Console.WriteLine($"Férfiak átlag fizetése: {adatLeker.ferfiakAtlagFizetese()}");
            Console.WriteLine($"Férfi dolgozók száma: {adatLeker.ferfiDolgozokSzama()}");
            Console.WriteLine($"Legfiatalabb dolgozó: {adatLeker.legfiatalabbDolgozo()}");
            Console.WriteLine($"Legidősebb dolgozó: {adatLeker.legIdosebbDolgozo()}");
            Console.WriteLine($"Legkisebb fizetésű dolgozó: {adatLeker.legKisebbFizetesuDolgozo()}");
            Console.WriteLine($"Legnagyobb fizetésű dolgozó: {adatLeker.legnagyobbFizetesuDolgozo()}");
            Console.WriteLine($"Női dolgozók száma: {adatLeker.noiDolgozokSzama()}");
            Console.WriteLine($"Nők átlag fizetése: {adatLeker.nokAtlagFizetese()}");
        }
    }
}
