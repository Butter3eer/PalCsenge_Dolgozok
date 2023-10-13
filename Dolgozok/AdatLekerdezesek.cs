using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DolgozokProject;

namespace DolgozokProject
{
    public class AdatLekerdezesek : AdatLekerdezesekInterface
    {
        //Tulajdonságként vettem fel a lekérdezésekhez a dolgozókat
        private Dolgozok dolgozok;

        //FONTOS! Csak a konstruktorban szabad neki érdéket adni.
        public AdatLekerdezesek(Dolgozok ujDolgozok)
        {
            this.dolgozok = ujDolgozok;
        }

        //A publikussá tétel azért kell, hogy lehessen ellenőrizni bármikor, hogy a konstruktoron keresztül jó változót kapott meg
        public Dolgozok Dolgozok { get => dolgozok; set => dolgozok = value; }

        //Ez a metódus mondja meg a dolgozók átlag életkorát
        public double dolgozokAtlagEletkora()
        {
            //Azért ilyen szellős a programozás, hogy könnyen átlátható legyen bárkinek, kérésre lehet rajtuk egyszerűsíteni
            int dolgozokOsszLetszam = dolgozok.DolgozokLista.Count;

            int osszEletkor = 0;

            foreach (Dolgozo dolgozo in dolgozok.DolgozokLista)
            {
                osszEletkor += dolgozo.Kor;
            }

            return osszEletkor / dolgozokOsszLetszam;
        }

        //Dolgozók átlag fizetése itt található
        public double dolgozokAtlagFizetese()
        {
            int dolgozokOsszLetszam = dolgozok.DolgozokLista.Count;

            int osszFizetes = 0;

            foreach (Dolgozo dolgozo in dolgozok.DolgozokLista)
            {
                osszFizetes += dolgozo.Fizetes;
            }

            return osszFizetes / dolgozokOsszLetszam;
        }

        //Csak a férfiak különszedve átlagfizetése
        public double ferfiakAtlagFizetese()
        {
            int ferfiDolgozok = dolgozok.DolgozokLista.Count(x => x.Nem == "férfi");

            int ferfiOsszFizu = dolgozok.DolgozokLista.Where(x => x.Nem == "férfi").Sum(x => x.Fizetes);

            return ferfiOsszFizu / ferfiDolgozok;
        }

        //Férfiak száma
        public int ferfiDolgozokSzama()
        {
            return dolgozok.DolgozokLista.Count(x => x.Nem == "férfi");
        }

        //Az adatbázisban legelőször előforduló legfiatalabb dolgozót adja vissza, kérésre meg lehet csinálni, hogy az összes legfiatalabb dolgozót adja vissza, ha nem csak egy van
        public Dolgozo legfiatalabbDolgozo()
        {
            Dolgozo legfiatalabb = dolgozok.DolgozokLista.Find(x => x.Kor == dolgozok.DolgozokLista.Min(y => y.Kor));

            return legfiatalabb;
        }

        //Erre a metódusra is ugyan az igaz, mint a legfiatalabbra, csak itt az idősre vonatkozóan
        public Dolgozo legIdosebbDolgozo()
        {
            Dolgozo legidosebb = dolgozok.DolgozokLista.Find(x => x.Kor == dolgozok.DolgozokLista.Max(y => y.Kor));

            return legidosebb;
        }

        //Itt is meg lehet csinálni, hogy az összes ilyen dolgozót adja vissza
        public Dolgozo legKisebbFizetesuDolgozo()
        {
            Dolgozo legkisebbFizetesu = dolgozok.DolgozokLista.Find(x => x.Fizetes == dolgozok.DolgozokLista.Min(y => y.Fizetes));

            return legkisebbFizetesu;
        }

        //Itt is meg lehet csinálni, hogy az összes ilyen dolgozót adja vissza
        public Dolgozo legnagyobbFizetesuDolgozo()
        {
            Dolgozo legNagyobbFizetesu = dolgozok.DolgozokLista.Find(x => x.Fizetes == dolgozok.DolgozokLista.Max(y => y.Fizetes));

            return legNagyobbFizetesu;
        }

        //Női dolgozók száma itt található
        public int noiDolgozokSzama()
        {
            return dolgozok.DolgozokLista.Count(x => x.Nem == "nő");
        }

        //Átlag fizetés csak nőkre külön szedve
        public double nokAtlagFizetese()
        {
            int noiDolgozok = dolgozok.DolgozokLista.Count(x => x.Nem == "nő");

            int noiOsszFizu = dolgozok.DolgozokLista.Where(x => x.Nem == "nő").Sum(x => x.Fizetes);

            return noiOsszFizu / noiDolgozok;
        }
    }
}