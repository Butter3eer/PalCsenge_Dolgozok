using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DolgozokProject;

namespace DolgozokProject
{
    public interface AdatLekerdezesekInterface
    {
        int ferfiDolgozokSzama();
        int noiDolgozokSzama();
        Dolgozo legnagyobbFizetesuDolgozo();
        Dolgozo legKisebbFizetesuDolgozo();
        double ferfiakAtlagFizetese();
        double nokAtlagFizetese();
        double dolgozokAtlagFizetese();
        Dolgozo legfiatalabbDolgozo();
        Dolgozo legIdosebbDolgozo();
        double dolgozokAtlagEletkora();
    }
}