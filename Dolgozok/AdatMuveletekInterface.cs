using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DolgozokProject
{
    //Ebben az interface-ben hoztam létre és találtam ki milyen metódusok lesznek a fő programrészben, ha kell még több metódus itt könnyen lehet hozzáadni.
    public interface AdatMuveletekInterface
    {
        void AdatBeolvasas(string fileName);
        void AdatTorles(int ujId);
        void AdatHozzaadas(Dolgozo dolgozo);
        void AdatModositas(int ujId, string ujNev, string ujNem, int ujKor, int ujFizetes);
        void EgyDolgozoAdatKiiras(int ujId);
    }
}