using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DolgozokProject
{
    public class Dolgozo
    {
        private int id;
        private string nev;
        private string nem;
        private int kor;
        private int fizetes;
        private Dolgozok dolgozok;

        public Dolgozo(int id, string nev, string nem, int kor, int fizetes)
        {
            this.id = id;
            this.nev = nev;
            this.nem = nem;
            this.kor = kor;
            this.fizetes = fizetes;
        }

        public Dolgozo(string nev, string nem, int kor, int fizetes, Dolgozok dolgozok)
        {
            this.dolgozok = dolgozok;
            this.nev = nev;
            this.nem = nem;
            this.kor = kor;
            this.fizetes = fizetes;

            try
            {
                this.id = dolgozok.DolgozokLista.Max(d => d.id);
            }
            catch (Exception)
            {
                throw new ArgumentException("Új dolgozó hozzáadása sikertelen, új ID kiosztása hibába ütközött.");
            }
        }

        public int Id { get => id; set => id = value; }
        public string Nev { get => nev; set => nev = value; }
        public string Nem { get => nem; set => nem = value; }
        public int Kor { get => kor; set => kor = value; }
        public int Fizetes { get => fizetes; set => fizetes = value; }

        public override string ToString()
        {
            return $" {this.id}. {this.nev} vagyok {this.kor} éves {this.nem} dolgozó vagyok {this.fizetes} ft-os fizetéssel.";
        }
    }
}