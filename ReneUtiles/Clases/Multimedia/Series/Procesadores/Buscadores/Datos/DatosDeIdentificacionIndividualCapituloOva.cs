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
    public class DatosDeIdentificacionIndividualCapituloOva: DatosDeIdentificacionIndividual
    {
        public IdentificacionEnStr etiquetaOva;



        public static DatosDeIdentificacionIndividualCapituloOva clonar(DatosDeIdentificacionIndividual d) {
            if (d==null) {
                return null;
            }
            DatosDeIdentificacionIndividualCapituloOva dov= new DatosDeIdentificacionIndividualCapituloOva();
            dov.clonarValores(d);
            return dov;

        }

        public  void clonarValores(DatosDeIdentificacionIndividualCapituloOva d) {
            base.clonarValores(d);
            d.etiqueta = etiqueta;
        }

        public override int getIndiceInicial()
        {
            int r = base.getIndiceInicial();
            if (etiquetaOva!=null) {
                return r < etiquetaOva.IndiceDeRepresentacionStr ? r : etiquetaOva.IndiceDeRepresentacionStr;
            }
            return r;
        }

    }
}
