using System.Linq;
using System.Threading.Channels;
using DolgozokProject;

namespace DolgozokTest
{
    public class Tests
    {
        private string fileName;
        private Dolgozok dolgozok;
        AdatLekerdezesek adatLeker;


        [SetUp]
        public void Setup()
        {
            fileName = "dolgozok.csv";
            dolgozok = new Dolgozok(fileName);
            adatLeker = new AdatLekerdezesek(dolgozok);
        }

        [Test] 
        public void DolgozokListaLetrejonDolgozokkal()
        {
            
            Assert.That(dolgozok.DolgozokLista, Is.Not.Empty);
        }

        [Test]
        public void DolgozokListaTipusaDolgozo()
        {
            Assert.That(dolgozok.DolgozokLista.GetType(), Is.EqualTo(typeof(List<Dolgozo>)));
        }

        [Test]
        public void DolgozokListaElemeinekTulajdonságaikNemUresek()
        {
            foreach (Dolgozo dolgozo in dolgozok.DolgozokLista)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(dolgozo.Id, Is.GreaterThan(0), "A dolgozo ID-ja üres.");
                    Assert.That(!string.IsNullOrEmpty(dolgozo.Nev), "A dolgozo neve üres.");
                    Assert.That(!string.IsNullOrEmpty(dolgozo.Nem), "A dolgozo neme üres.");
                    Assert.That(dolgozo.Kor, Is.GreaterThan(0), "A dolgozo kora üres.");
                    Assert.That(dolgozo.Fizetes, Is.GreaterThan(0), "A dolgozo fizetése üres.");
                });
            }
        }

        [Test]
        public void AdatHozzaadasMetodusTeszt()
        {
            Dolgozok dolgozok2 = new(fileName);

            Dolgozo ujDolgozo = new("Teszt Elek", "férfi", 25, 12000, dolgozok2);
            dolgozok2.AdatHozzaadas(ujDolgozo);

            Assert.That(dolgozok2.DolgozokLista, Does.Contain(ujDolgozo));
        }

        [Test]
        public void EgyDolgozoAdatKiirasHelyesIDNemDobErrort()
        {
            Assert.DoesNotThrow(() =>
            {
                dolgozok.EgyDolgozoAdatKiiras(12);
            });
        }

        [Test]
        public void EgyDolgozoAdatKiirasHelytelenIDDobErrort()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                dolgozok.EgyDolgozoAdatKiiras(-12);
            });
        }

        [Test]
        public void DolgozoToStringHelyesenIrKi()
        {
            Dolgozo dolgozo = dolgozok.DolgozokLista[1];

            string eredetiToString = dolgozo.ToString();

            string aminekLennieKell = $" {dolgozo.Id}. {dolgozo.Nev} vagyok {dolgozo.Kor} éves {dolgozo.Nem} dolgozó vagyok {dolgozo.Fizetes} ft-os fizetéssel.";

            Assert.That(eredetiToString, Is.EqualTo(aminekLennieKell));
        }

        [Test]
        public void AdatModositasErrorRosszID()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                dolgozok.AdatModositas(-12, "Uj Nev", "férfi", 25, 12000);
            });
        }

        [Test]
        public void AdatModositasTenylegesenModosit()
        {
            Dolgozok dolgozok2 = new(fileName);

            Dolgozo eredetiDolgozo = dolgozok2.DolgozokLista[1];

            dolgozok2.AdatModositas(1, "Uj Nev", "férfi", 25, 12000);

            Assert.That(eredetiDolgozo, Is.Not.EqualTo(dolgozok2.DolgozokLista[0]));
        }

        [Test]
        public void AdatModositasHelyesenModosit()
        {
            Dolgozok dolgozok2 = new(fileName);
            dolgozok2.AdatModositas(1, "Uj Nev", "férfi", 25, 12000);

            Dolgozo modositott = dolgozok2.DolgozokLista.First();
            Assert.Multiple(() =>
            {
                Assert.That(modositott.Id, Is.EqualTo(1));
                Assert.That(modositott.Nev, Is.EqualTo("Uj Nev"));
                Assert.That(modositott.Nem, Is.EqualTo("férfi"));
                Assert.That(modositott.Kor, Is.EqualTo(25));
                Assert.That(modositott.Fizetes, Is.EqualTo(12000));
            });
        }

        [Test]
        public void AdatTorlesTenylegTorol()
        {
            Dolgozok dolgozok2 = new(fileName);
            Dolgozo eredetiDolgozo = dolgozok2.DolgozokLista[0];

            dolgozok2.AdatTorles(1);
            Assert.Multiple(() =>
            {
                Assert.That(eredetiDolgozo, Is.Not.EqualTo(dolgozok2.DolgozokLista[0]));

                Assert.That(dolgozok2.DolgozokLista, Does.Not.Contain(eredetiDolgozo));
            });
        }

        [Test]
        public void AdatBeolvasasMegNemLetezoFajllalUjFajltHozLetre()
        {
            _ = new Dolgozok("ujfajl.csv");

            Assert.That(File.Exists("ujfajl.csv"));
        }

        [Test]
        public void AdatTorlesRosszIDErrortDob()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                dolgozok.AdatTorles(-12);
            });
        }

        [Test]
        public void DolgozoEllenorzesErrortDobID()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                dolgozok.DolgozoEllenorzes(new Dolgozo(-12, "Teszt Elek", "nő", 25, 12000));
                dolgozok.DolgozoEllenorzes(new Dolgozo(0, "Teszt Elek", "nő", 25, 12000));
            });
        }

        [Test]
        public void DolgozoEllenorzesErrortDobNev()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                dolgozok.DolgozoEllenorzes(new Dolgozo(1, "", "nő", 25, 12000));
                dolgozok.DolgozoEllenorzes(new Dolgozo(1, null, "nő", 25, 12000));
            });
        }

        [Test]
        public void DolgozoEllenorzesErrortDobNem()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                dolgozok.DolgozoEllenorzes(new Dolgozo(1, "Teszt Elek", "", 25, 12000));
                dolgozok.DolgozoEllenorzes(new Dolgozo(1, "Teszt Elek", null, 25, 12000));
            });
        }

        [Test]
        public void DolgozoEllenorzesErrortDobKor()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                dolgozok.DolgozoEllenorzes(new Dolgozo(1, "Teszt Elek", "nő", 0, 12000));
                dolgozok.DolgozoEllenorzes(new Dolgozo(1, "Teszt Elek", "nő", -12, 12000));
            });
        }

        [Test]
        public void DolgozoEllenorzesErrortDobFizetes()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                dolgozok.DolgozoEllenorzes(new Dolgozo(1, "Teszt Elek", "nő", 25, 0));
                dolgozok.DolgozoEllenorzes(new Dolgozo(1, "Teszt Elek", "nő", 25, -1200));
            });
        }

        [Test]
        public void AdatLekerdezesDolgozokUgyanAzMintAParameterDolgozok()
        {
            Assert.That(dolgozok, Is.EqualTo(adatLeker.Dolgozok));
        }

        [Test]
        public void AdatLekerdezesListaUgyanAzMintAParameterDolgozokLista()
        {
            Assert.That(dolgozok.DolgozokLista, Is.EqualTo(adatLeker.Dolgozok.DolgozokLista));
        }

        [Test]
        public void DolgozokAtlagEletkoraHelyes()
        {
            int osszKor = dolgozok.DolgozokLista.Sum(x => x.Kor);
            int dolgozokSzama = dolgozok.DolgozokLista.Count;

            double aminekLennieKell = osszKor / dolgozokSzama;
            double amitKapok = adatLeker.dolgozokAtlagEletkora();

            Assert.That( amitKapok , Is.EqualTo(aminekLennieKell));
        }

        [Test]
        public void DolgozokAtlagFizeteseHelyes()
        {
            int osszFizetes = dolgozok.DolgozokLista.Sum(x => x.Fizetes);
            int dolgozokSzama = dolgozok.DolgozokLista.Count;

            double aminekLennieKell = osszFizetes / dolgozokSzama;
            double amitKapok = adatLeker.dolgozokAtlagFizetese();

            Assert.That( amitKapok , Is.EqualTo(aminekLennieKell));
        }

        [Test]
        public void FerfiakAtlagFizeteseHelyes()
        {
            int ferfikSzama = dolgozok.DolgozokLista.Count(x => x.Nem == "férfi");
            int ferfiOsszFiezetes = dolgozok.DolgozokLista.Where(x => x.Nem == "férfi").Sum(x => x.Fizetes);

            double aminekLennieKell = ferfiOsszFiezetes / ferfikSzama;
            double amitKapok = adatLeker.ferfiakAtlagFizetese();

            Assert.That( amitKapok , Is.EqualTo(aminekLennieKell));
        }

        [Test]
        public void FerfiDolgozokSzamaHelyes()
        {
            int ferfikSzama = dolgozok.DolgozokLista.Count(x => x.Nem == "férfi");
            int amitKapok = adatLeker.ferfiDolgozokSzama();

            Assert.That(ferfikSzama, Is.EqualTo(amitKapok));
        }

        [Test]
        public void LegfiatalabbDolgozoHelyes()
        {
            Dolgozo legfiatalabb = dolgozok.DolgozokLista.Find(x => x.Kor == dolgozok.DolgozokLista.Min(y => y.Kor))!;
            Dolgozo amitKapok = adatLeker.legfiatalabbDolgozo();

            Assert.That( legfiatalabb, Is.EqualTo(amitKapok));
        }

        [Test]
        public void LegidosebbDolgozoHelyes()
        {
            Dolgozo legidosebb = dolgozok.DolgozokLista.Find(x => x.Kor == dolgozok.DolgozokLista.Max(y => y.Kor))!;
            Dolgozo amitKapok = adatLeker.legIdosebbDolgozo();

            Assert.That(legidosebb, Is.EqualTo(amitKapok));
        }

        [Test]
        public void LegKisebbFizetesuDolgozoHelyes()
        {
            Dolgozo kicsiFizetesu = dolgozok.DolgozokLista.Find(x => x.Fizetes == dolgozok.DolgozokLista.Min(y => y.Fizetes))!;
            Dolgozo amitKapok = adatLeker.legKisebbFizetesuDolgozo();
            
            Assert.That( kicsiFizetesu, Is.EqualTo(amitKapok));
        }

        [Test]
        public void LegnagyobbFizetesuDolgozoHelyes()
        {
            Dolgozo nagyFizetesu = dolgozok.DolgozokLista.Find(x => x.Fizetes == dolgozok.DolgozokLista.Max(y => y.Fizetes))!;
            Dolgozo amitKapok = adatLeker.legnagyobbFizetesuDolgozo();

            Assert.That(nagyFizetesu, Is.EqualTo(amitKapok));
        }

        [Test]
        public void NoiDolgozokSzamaHelyes()
        {
            int noiSzam = dolgozok.DolgozokLista.Count(x => x.Nem == "nő");
            int amitKapok = adatLeker.noiDolgozokSzama();

            Assert.That( noiSzam, Is.EqualTo(amitKapok));
        }

        [Test]
        public void NokAtlagFizeteseHelyes()
        {
            int nokSzama = dolgozok.DolgozokLista.Count(x => x.Nem == "nő");
            int nokOsszFizetes = dolgozok.DolgozokLista.Where(x => x.Nem == "nő").Sum(x => x.Fizetes);

            double aminekLennieKell = nokOsszFizetes / nokSzama;
            double amitKapok = adatLeker.nokAtlagFizetese();

            Assert.That( amitKapok , Is.EqualTo(aminekLennieKell));
        }
    } 
}
