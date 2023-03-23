using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos;
using ReneUtiles.Clases.Multimedia.Series.Representaciones.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReneUtiles.Clases.Multimedia.Relacionadores
{
    public class DatosDeSerieRelacionada
    {
        private Serie serie;
        private KeySerie key;

        private DatosDeNombreSerie dn;

        public void set(KeySerie key) {
            this.key = key;
        }
        public void set(DatosDeNombreSerie dn)
        {
            this.dn = dn;
        }
        public void set(Serie serie)
        {
            this.serie = serie;
        }

    }
}
