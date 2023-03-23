using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReneUtiles.Clases.Multimedia.Relacionadores
{
    public class EventosDeRelacionadorDeSerie
    {
        public Func<string, string, bool, DatosDeRelacionDeSeries> creadorDeDatosDeRelacion;

        public EventosDeRelacionadorDeSerie() {
            //this.creadorDeDatosDeRelacion = (a, b, estanRelacionados) => new DatosDeRelacionDeSeries(estanRelacionados, a,a, b,b);
        }

        public DatosDeRelacionDeSeries alYaExistirRelacionPrevia(DatosDeRelacionDeSeries anterior) {
            return anterior;
        }
    }
}
