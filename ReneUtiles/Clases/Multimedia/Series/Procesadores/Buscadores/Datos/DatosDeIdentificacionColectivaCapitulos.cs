using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos
{
    public class DatosDeIdentificacionColectivaCapitulos:DatosDeIdentificacionColectiva
    {
        public bool esDeMismaTemporada = false;

        public virtual void clonarValores(DatosDeIdentificacionColectivaCapitulos d) {
            this.datosDelContenedor = d.datosDelContenedor;
            this.esDeEsteTipo = d.esDeEsteTipo;
            this.etiqueta = d.etiqueta;

            this.esDeMismaTemporada = d.esDeMismaTemporada;

            
        }
    }
}
