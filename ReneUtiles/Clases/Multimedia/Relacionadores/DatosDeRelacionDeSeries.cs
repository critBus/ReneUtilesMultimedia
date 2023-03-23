using ReneUtiles.Clases.Multimedia.Series.Representaciones.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReneUtiles.Clases.Multimedia.Relacionadores
{
    public class DatosDeRelacionDeSeries : DatosDeRelacionDeStr<DatosDeSerieRelacionada>
    {
        //public DatosDeSerieRelacionada serieA;
        //public DatosDeSerieRelacionada serieB;
        public DatosDeRelacionDeSeries(bool estanRelacionados
            , string elementoA, DatosDeSerieRelacionada serieA, string elementoB
            , DatosDeSerieRelacionada serieB) 
            : base(estanRelacionados, elementoA, serieA, elementoB, serieB)
        {
            //this.serieA=serieA;
            //this.serieB = serieB;
        }
        
    }
}
