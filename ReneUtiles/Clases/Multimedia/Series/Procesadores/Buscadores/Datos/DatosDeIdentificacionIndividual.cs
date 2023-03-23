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
    public class DatosDeIdentificacionIndividual:ConIndiceInicial, ConPrimerIndiceNumerico
    {
        public IdentificacionEnStr etiqueta;
        public IdentificacionNumericaEnStr identificacionNumerica;

        public virtual void clonarValores(DatosDeIdentificacionIndividual d)
        {
            
            this.etiqueta = d.etiqueta;
            this.identificacionNumerica = d.identificacionNumerica;
            
        }

        public virtual int getIndiceInicial() {
            if (etiqueta != null&&identificacionNumerica!=null) {
                return etiqueta.IndiceDeRepresentacionStr < identificacionNumerica.IndiceDeRepresentacionStr
                    ? etiqueta.IndiceDeRepresentacionStr : identificacionNumerica.IndiceDeRepresentacionStr;
            }
            if (etiqueta != null) {
                return etiqueta.IndiceDeRepresentacionStr;
            }
            if (identificacionNumerica != null)
            {
                return identificacionNumerica.IndiceDeRepresentacionStr;
            }
            return -1;
        }
        public virtual int getPrimerIndiceDeNumero() {
            if (identificacionNumerica != null)
            {
                return identificacionNumerica.IndiceDeRepresentacionStr;
            }
            return -1;
        }
    }
}
