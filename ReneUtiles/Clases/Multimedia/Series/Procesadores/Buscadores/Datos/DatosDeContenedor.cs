using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReneUtiles;
using ReneUtiles.Clases;
using ReneUtiles.Clases.ExprecionesRegulares;
using ReneUtiles.Clases.ExprecionesRegulares.IdentificacionesNumericas;
using System.Collections.ObjectModel;

namespace ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos
{
    public class DatosDeContenedor
    {
        //public bool esDeEsteTipo;

        public ConjuntoDeIdentificacionesNumericas numerosIndividuales;
        public IdentificacionNumericaEnStr numeroCantidad;

        public DatosDeContenedor() {
            //this.esDeEsteTipo = false;
            this.numerosIndividuales = new ConjuntoDeIdentificacionesNumericas();
            this.numeroCantidad = null;
        }
        /// <summary>
        /// Se refiere al rango interno entre el numero inicial y el final
        /// Se le da prioridad a al numero que representa la cantidad si existe
        /// </summary>
        /// <returns></returns>
        public int getTamannoDelRangoQueContiene() {
            if (this.numeroCantidad!=null) {
                return this.numeroCantidad.Numero;
            }
            if (this.numerosIndividuales!=null) {
                ReadOnlyCollection<IdentificacionNumericaEnStr> sn = this.numerosIndividuales.OrdenadosPorNumero;
                if (sn.Count>0) {
                    int menor = sn.First().Numero;
                    int mayor = sn.Last().Numero;
                    return mayor - menor;
                }
                
                
            }
            return 0;

        }

    }
}
