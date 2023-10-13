using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DolgozokProject
{
    public class Dolgozok : AdatMuveletekInterface
    {
        //Ebben a listában van eltárolva az összes dolgozó a csv fájlból
        private List<Dolgozo> dolgozokLista;

        public Dolgozok(string fileName)
        {
            dolgozokLista = new List<Dolgozo>();
            //Már amikor létrejön a Dolgozok objektum lezajlik a beolvasás
            AdatBeolvasas(fileName);
        }

        //A dolgozók listájából publikus element lett készítve későbbi kezelések érdekében
        public List<Dolgozo> DolgozokLista { get => dolgozokLista; set => dolgozokLista = value; }

        //Ez a beolvasás metódusa
        public void AdatBeolvasas(string fileName)
        {
            //Annak érdekében, hogy véletlenül se legyen a listában többszörös beolvasás minden beolvasás előtt ki van űrítve a lista
            this.dolgozokLista.Clear();

            //Először ellenőrzöm, hogy a fájl egyáltalán létezik-e lehetséges crash-elés elkerülése érdekében
            if (File.Exists(fileName) || String.IsNullOrEmpty(fileName))
            {
                StreamReader file = new StreamReader(fileName);
                file.ReadLine();

                while (!file.EndOfStream)
                {
                    string[] adatok = file.ReadLine().Split(',');
                    //Itt adom hozzá a listához az új dolgozókat
                    dolgozokLista.Add(new Dolgozo(Convert.ToInt32(adatok[0]), adatok[1], adatok[2], Convert.ToInt32(adatok[3]), Convert.ToInt32(adatok[4])));
                }
                file.Close();
            }
            else
            {
                File.Create(fileName).Close();
                StreamWriter file = new StreamWriter(fileName);
                file.WriteLine("id,nev,nem,kor,fizetes");
                file.Close();
            }
            
        }

        //A listához újonan ezen metódus meghívásával lehet dolgozót hozzáadni, amelynél hozzáadás előtt egy ellenőrző metódussal, amit a kódban a 110. sorban hoztam létre nézem meg, hogy a dolgozó, akit hozzá akarnak adni a listához nem tartalmaz üres tulajdonságot, egyébként ArgumentExeption-t dob az ellenőrzés.
        public void AdatHozzaadas(Dolgozo dolgozo)
        {
            //Bool metódus, ezért lehet könnyen használni If függvénnyel
            if (DolgozoEllenorzes(dolgozo))
            {
                this.dolgozokLista.Add(dolgozo);   
            }
        }

        //Ez a metódus szolgál arra, ha egyetlen dolgozót szeretnénk kiíratni a listából az ID segítségével
        public void EgyDolgozoAdatKiiras(int ujId)
        {
            //Itt ellenőrzöm le, hogy létezik-e ez az ID a listában
            if (dolgozokLista.Any(item => item.Id == ujId))
            {
                Console.WriteLine(dolgozokLista.Find(x => x.Id == ujId).ToString());
            }
            else
            {
                throw new ArgumentException("A megadott ID nem szerepel a listában.");
            }
        }

        //Ebben a metódusban lehet módosítani egy dolgozó adatait, egyelőre csak minden adat beírásával újra az új tulajdonsággal együtt, lehet fejleszteni rajta, ha van rá igény
        public void AdatModositas(int ujId, string ujNev, string ujNem, int ujKor, int ujFizetes)
        {
            if (dolgozokLista.Any(item => item.Id == ujId))
            {
                Dolgozo modositandoDolgozo = dolgozokLista.Find(x => x.Id == ujId);

                int modositandoDolgozoIndex = dolgozokLista.IndexOf(modositandoDolgozo);

                dolgozokLista[modositandoDolgozoIndex] = new Dolgozo(ujId, ujNev, ujNem, ujKor, ujFizetes);
            }
            else
            {
                throw new ArgumentException("A megadott ID nem szerepel a listában.");
            }
        }

        //Ebben a metódusban lehet kitörölni a megadott ID alapján az adott dolgozót és itt is van ellenőrzés és ArgumentExeption arra, ha esetleg a megadott ID nem létezne a listában
        public void AdatTorles(int ujId)
        {
            if (dolgozokLista.Any(item => item.Id == ujId))
            {
                dolgozokLista.Remove(dolgozokLista.Find(x => x.Id == ujId));
            }
            else
            {
                throw new ArgumentException("A megadott ID nem szerepel a listában.");
            }
        }

        //Itt az ellenőrzés arra, hogy egy dolgozó rendelkezik-e üres tulajdonsággal
        public bool DolgozoEllenorzes(Dolgozo dolgozo)
        {
            if (dolgozo.Id <= 0)
            {
                throw new ArgumentException("A dolgozó ID-ja nem érvényes.");  
            }
            else if (string.IsNullOrEmpty(dolgozo.Nev))
            {
                throw new ArgumentException("A dolgozó neve nem lehet üres.");
            }
            else if (string.IsNullOrEmpty(dolgozo.Nem))
            {
                throw new ArgumentException("A dolgozó neme nem lehet üres.");
            }
            else if (dolgozo.Kor <= 0)
            {
                throw new ArgumentException("A dolgozó kora nem lehet kisebb mint 0 vagy üres.");
            }
            else if (dolgozo.Fizetes <= 0)
            {
                throw new ArgumentException("A dolgozó fizetése nem lehet kisebb mint 0 vagy üres.");
            }
            else
            {
                return true;
            }
        }
    }
}