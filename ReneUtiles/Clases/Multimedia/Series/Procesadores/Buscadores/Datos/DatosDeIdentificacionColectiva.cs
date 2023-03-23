using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReneUtiles;
using ReneUtiles.Clases;
using ReneUtiles.Clases.ExprecionesRegulares.IdentificacionesNumericas;
using ReneUtiles.Clases.Interfaces;
namespace ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos
{
    public class DatosDeIdentificacionColectiva:ConIndiceInicial, ConPrimerIndiceNumerico
    {
        //public bool esDeEsteTipo=false;
        public IdentificacionEnStr etiqueta;
        public DatosDeContenedor datosDelContenedor;

        public virtual int getIndiceInicial()
        {
            if (datosDelContenedor!=null&& etiqueta!=null) {
                int r = datosDelContenedor.getIndiceInicial();
                
                return r < etiqueta.IndiceDeRepresentacionStr ? r : etiqueta.IndiceDeRepresentacionStr;
                
#pragma warning disable CS0162 // Unreachable code detected
                return r;
#pragma warning restore CS0162 // Unreachable code detected
            }
            if (datosDelContenedor != null) {
                return datosDelContenedor.getIndiceInicial();
            }
            if (etiqueta != null)
            {
                return etiqueta.IndiceDeRepresentacionStr;
            }
            return -1;
        }

        public virtual int getPrimerIndiceDeNumero()
        {
            if (datosDelContenedor != null)
            {
                return datosDelContenedor.getPrimerIndiceDeNumero();
            }
            return -1;
        }

    }
}
