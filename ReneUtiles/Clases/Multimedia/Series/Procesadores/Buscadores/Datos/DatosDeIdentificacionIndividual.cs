using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReneUtiles;
using ReneUtiles.Clases;
using ReneUtiles.Clases.ExprecionesRegulares.IdentificacionesNumericas;

namespace ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos
{
    public class DatosDeIdentificacionIndividual
    {
        public IdentificacionEnStr etiqueta;
        public IdentificacionNumericaEnStr identificacionNumerica;

        public virtual void clonarValores(DatosDeIdentificacionIndividual d)
        {
            
            this.etiqueta = d.etiqueta;
            this.identificacionNumerica = d.identificacionNumerica;
            
        }
        }
}
