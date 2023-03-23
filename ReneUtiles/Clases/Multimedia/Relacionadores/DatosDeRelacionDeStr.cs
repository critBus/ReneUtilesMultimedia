using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReneUtiles.Clases.Multimedia.Relacionadores
{
    public class DatosDeRelacionDeStr<E>
    {
        public bool estanRelacionados;

        //public string elementoA;
        //public string elementoB;
        Dictionary<string, HashSet<E>> elementosRelacionados;
        Func<HashSet<E>> crearHashSet;
        public DatosDeRelacionDeStr(
            bool estanRelacionados
            , string claveA
            , E A
            , string claveB
            , E B
            )
        {
            this.estanRelacionados = estanRelacionados;
            //this.elementoA = elementoA;
            //this.elementoB = elementoB;
            elementosRelacionados = new Dictionary<string, HashSet<E>>();
            this.crearHashSet = () => new HashSet<E>();
            add(claveA,A);
            add(claveB, B);
        }
        public void add( string clave
            , E e) {
            if (elementosRelacionados.ContainsKey(clave))
            {
                elementosRelacionados[clave].Add(e);
            }
            else {
                HashSet<E> hs = this.crearHashSet();
                hs.Add(e);
                elementosRelacionados.Add(clave,hs);
            }
        }
    }
}
