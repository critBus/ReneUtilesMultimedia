/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 2/8/2022
 * Hora: 15:26
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos
{
	/// <summary>
	/// Description of DatosNumericosDeNombreDeSerie.
	/// </summary>
	public class DatosNumericosDeNombreDeSerie 
	{
		public DatosDeNombreCapituloDelPrincipio datosDelPrincipio;
		public DatosDeNombreCapituloDelFinal datosDelFinal;
		
		public List<DatosDeFechaEnNombre> fechasEnNombre;
		public List<DatosDeAleatoriedadEnNombre> aleatoriedadesEnNombre;
		public Dictionary<TipoDeEtiquetaDeSerie,List<DatosDeEtiquetaEnNombre>> etiquetasEnNombre;
		
		public DatosNumericosDeNombreDeSerie()
		{
            this.fechasEnNombre = new List<DatosDeFechaEnNombre>();
            this.aleatoriedadesEnNombre = new List<DatosDeAleatoriedadEnNombre>();
            this.etiquetasEnNombre = ComparadorTipoDeEtiquetaDeSerie.getNewDictionary_TipoDeEtiquetaDeSerie<List<DatosDeEtiquetaEnNombre>>();

        }
		public bool isEmpty(){
			return datosDelPrincipio==null&&datosDelFinal==null;
		}
        public bool esOva()
        {
            //return(datosDelFinal!=null?datosDelFinal.EsOVA:(datosDelPrincipio!=null?datosDelPrincipio.EsOVA:false));

            ;
            return (datosDelFinal != null ? datosDelFinal.esOva() : false) ||
                (datosDelPrincipio != null ? datosDelPrincipio.esOva() : false)
                ;
        }
        public bool esConjuntoDeCapitulos(){
//			return(datosDelFinal!=null?datosDelFinal.EsConjuntoDeCapitulos:(datosDelPrincipio!=null?datosDelPrincipio.EsConjuntoDeCapitulos:false));
			return (datosDelPrincipio!=null?datosDelPrincipio.esConjuntoDeCapitulos():false)
				||(datosDelFinal!=null?datosDelFinal.esConjuntoDeCapitulos(): false);
		}
		
		public int getCapituloInicial(){//CapituloFinal
			return (datosDelFinal!=null&&datosDelFinal.esConjuntoDeCapitulos()?datosDelFinal.getCapituloInicial():
			        (datosDelPrincipio!=null?datosDelPrincipio.getCapituloInicial (): -1));
			//return (datosDelPrincipio!=null&&datosDelPrincipio.EsConjuntoDeCapitulos?datosDelPrincipio.CapituloInicial:(datosDelFinal!=null?datosDelFinal.CapituloInicial:-1));
		}
		public int getCapituloFinal(){//CapituloFinal
			return (datosDelFinal!=null&&datosDelFinal.esConjuntoDeCapitulos()? datosDelFinal.getCapituloFinal():
			        (datosDelPrincipio!=null&& datosDelPrincipio.esConjuntoDeCapitulos()? datosDelPrincipio.getCapituloFinal():-1));
			//return (datosDelPrincipio!=null&&datosDelPrincipio.EsConjuntoDeCapitulos?datosDelPrincipio.CapituloFinal:(datosDelFinal!=null?datosDelFinal.CapituloFinal:-1));
		}
		
		//public int getCapitulo(){
		//	return (datosDelFinal!=null&&!datosDelFinal.EsConjuntoDeCapitulos?datosDelFinal.Capitulo:
		//	        (datosDelPrincipio!=null?datosDelPrincipio.Capitulo:-1));
		//	//return (datosDelPrincipio!=null&&!datosDelPrincipio.EsConjuntoDeCapitulos?datosDelPrincipio.Capitulo:(datosDelFinal!=null?datosDelFinal.Capitulo:-1));
		//}
		
		public bool esContenedorDeTemporada(){
            //EsContenedorDeTemporada
            //return (datosDelPrincipio!=null?datosDelPrincipio.EsContenedorDeTemporada:false)
            //		||(datosDelFinal!=null?datosDelFinal.EsContenedorDeTemporada:false);
            //return(datosDelFinal!=null?datosDelFinal.EsContenedorDeTemporada:(datosDelPrincipio!=null?datosDelPrincipio.EsContenedorDeTemporada:false));
            return datosDelFinal != null ? datosDelFinal.esContenedorDeTemporada() : datosDelPrincipio != null && datosDelPrincipio.esContenedorDeTemporada();

        }
		
		public int getTemporada(){
			
		return (datosDelFinal!=null&&datosDelFinal.tieneTemporada()
                && datosDelFinal.IdenificadorTemporada.identificacionNumerica != null
                ? datosDelFinal.IdenificadorTemporada.identificacionNumerica.Numero
                :
			        (datosDelPrincipio!=null&& datosDelPrincipio.tieneTemporada()
                    && datosDelPrincipio.IdenificadorTemporada.identificacionNumerica!=null
                    ? datosDelPrincipio.IdenificadorTemporada.identificacionNumerica.Numero: 1));
		}

        //public bool esConjuntoDeCapitulos() {
        //    return (datosDelFinal!=null&&datosDelFinal.esConjuntoDeCapitulos())
        //        || (datosDelFinal != null && datosDelFinal.esConjuntoDeCapitulos())
        //}

		public bool hayCantidadDeCapitulos()
        {//DeContenedorDeTemporada
            if (datosDelFinal!=null) {
                int cantidad = datosDelFinal.getCantidadDeCapitulos();
                if (cantidad > 0) {
                    return true;
                }
            }
            if (datosDelPrincipio != null)
            {
                int cantidad = datosDelPrincipio.getCantidadDeCapitulos();
                if (cantidad > 0)
                {
                    return true;
                }
            }
            return false;
        }
		
		public int getCantidadDeCapitulos(){
            if (datosDelFinal != null)
            {
                int cantidad = datosDelFinal.getCantidadDeCapitulos();
                if (cantidad > 0)
                {
                    return cantidad;
                }
            }
            if (datosDelPrincipio != null)
            {
                int cantidad = datosDelPrincipio.getCantidadDeCapitulos();
                if (cantidad > 0)
                {
                    return cantidad;
                }
            }
            return 0;

            //return (datosDelFinal!=null&&datosDelFinal.EsContenedorDeTemporada&&datosDelFinal.CantidadDeCapitulosQueContiene!=-1?datosDelFinal.CantidadDeCapitulosQueContiene:
            //        datosDelPrincipio!=null&&datosDelPrincipio.EsContenedorDeTemporada?datosDelPrincipio.CantidadDeCapitulosQueContiene:-1
            //       );
        }
	}
}
