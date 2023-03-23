/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 13/8/2022
 * Hora: 18:58
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using ReneUtiles.Clases.Multimedia;
using ReneUtiles.Clases.Multimedia.Relacionadores.Saltos;
using ReneUtiles.Clases.Multimedia.Series.Contextos;
using ReneUtiles.Clases.Multimedia.Series;
using ReneUtiles.Clases;
using System.Text.RegularExpressions;
using ReneUtiles;
using ReneUtiles.Clases.Basicos.String;
#pragma warning disable CS0105 // The using directive for 'ReneUtiles.Clases.Multimedia.Series' appeared previously in this namespace
using ReneUtiles.Clases.Multimedia.Series;
#pragma warning restore CS0105 // The using directive for 'ReneUtiles.Clases.Multimedia.Series' appeared previously in this namespace
using ReneUtiles.Clases.Multimedia.Series.Representaciones;
//using ReneUtiles.Clases.Multimedia.Series.Procesadores.Ignorar;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Conjuntos;
using ReneUtiles.Clases.Multimedia.Series.Procesadores;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores;
using ReneUtiles.Clases.Multimedia.Series.Representaciones.Temporadas;
using ReneUtiles.Clases.Multimedia.Series.Representaciones.Capitulos;
using ReneUtiles.Clases.Multimedia.Series.Representaciones.Series;
using ReneUtiles.Clases.Multimedia.Relacionadores;
using ReneUtiles.Clases.Multimedia.Series.Recorredores;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos;
//using System.IO;
using Delimon.Win32.IO;
namespace ReneUtiles.Clases.Multimedia.Paquetes.Representaciones
{
	/// <summary>
	/// Description of SeriesDelPaquete.
	/// </summary>
	public class SeriesDelPaquete
	{
		public Dictionary<ConjuntoDeEtiquetasDeSerie,HashSet<DirectorioDeSeriesDelPaquete>> directoriosDeSeries; 
		private ProcesadorDeRelacionesDeNombresClaveSeries proR;
		private ConfiguracionDeSeries cf;
		public SeriesDelPaquete(
			ProcesadorDeRelacionesDeNombresClaveSeries proR
			, ConfiguracionDeSeries cf
		)
		{
			this.directoriosDeSeries=ComparadorConjuntoDeEtiquetasDeSerie.getNewDictionary_ConjuntoDeEtiquetasDeSerie<HashSet<DirectorioDeSeriesDelPaquete>>();
			this.cf=cf;
			this.proR=proR;
		}
		
		public void addDirectorio(ConjuntoDeEtiquetasDeSerie c,DirectoryInfo carpeta){
			DirectorioDeSeriesDelPaquete d=new DirectorioDeSeriesDelPaquete(carpeta,c,new ConjuntoDeSeries(this.proR,this.cf));
			if(directoriosDeSeries.ContainsKey(c)){
				
				directoriosDeSeries[c].Add(d);
				return;
			}
			HashSet<DirectorioDeSeriesDelPaquete> hs=ComparadorDirectorioDeSeriesDelPaquete.getNewHashSet_DirectorioDeSeriesDelPaquete();
			hs.Add(d);
			directoriosDeSeries.Add(c,hs);
		}
	}
}
