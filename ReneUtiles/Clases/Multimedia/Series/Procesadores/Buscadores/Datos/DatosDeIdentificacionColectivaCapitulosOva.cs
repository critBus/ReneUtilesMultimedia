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
    public class DatosDeIdentificacionColectivaCapitulosOva: DatosDeIdentificacionColectivaCapitulos
    {
        public IdentificacionEnStr etiquetaOva;


        public void clonarValores(DatosDeIdentificacionColectivaCapitulosOva d) {
            base.clonarValores(d);
            this.etiquetaOva = d.etiquetaOva;
        }

        public static DatosDeIdentificacionColectivaCapitulosOva clonar(DatosDeIdentificacionColectivaCapitulos d) {
            if (d==null) {
                return null;
            }
            DatosDeIdentificacionColectivaCapitulosOva dov = new DatosDeIdentificacionColectivaCapitulosOva();
            dov.clonarValores(d);
            return dov;
        }

        public override int getIndiceInicial()
        {
            int r = base.getIndiceInicial();
            if (etiquetaOva != null)
            {
                return r < etiquetaOva.IndiceDeRepresentacionStr ? r : etiquetaOva.IndiceDeRepresentacionStr;
            }
            return r;
        }
    }
}
