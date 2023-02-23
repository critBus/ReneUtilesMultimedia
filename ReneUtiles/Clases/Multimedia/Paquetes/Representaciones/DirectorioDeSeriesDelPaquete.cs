/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 13/8/2022
 * Hora: 18:54
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
using ReneUtiles.Clases.Multimedia.Series;
using ReneUtiles.Clases.Multimedia.Series.Representaciones;
//using ReneUtiles.Clases.Multimedia.Series.Procesadores.Ignorar;
//using ReneUtiles.Clases.Multimedia.Series.Procesadores.Conjuntos;
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
	/// Description of DirectorioSeriesDePaquete.
	/// </summary>
	public class DirectorioDeSeriesDelPaquete
	{
		public FileSystemInfo carpeta;
		//public List<TipoDeEtiquetaDeSerie> etiquetas;
		public ConjuntoDeEtiquetasDeSerie etiquetas;
		public ConjuntoDeSeries series;
		public DirectorioDeSeriesDelPaquete(
            FileSystemInfo carpeta,
			ConjuntoDeEtiquetasDeSerie etiquetas,
			ConjuntoDeSeries series
		)
		{
			this.carpeta = carpeta;
			this.etiquetas = etiquetas;
			this.series = series;
		}

        public static HashSet<DirectorioDeSeriesDelPaquete> getNewHashSet()
        {
            return ComparadorDirectorioDeSeriesDelPaquete.getNewHashSet_DirectorioDeSeriesDelPaquete();
        }
        public static HashSet<DirectorioDeSeriesDelPaquete> getNewHashSet(IEnumerable<DirectorioDeSeriesDelPaquete> anterior)
        {
            return ComparadorDirectorioDeSeriesDelPaquete.getNewHashSet_DirectorioDeSeriesDelPaquete(anterior);
        }

        public static Dictionary<DirectorioDeSeriesDelPaquete, E> getNewDictionary<E>()
        {
            return ComparadorDirectorioDeSeriesDelPaquete.getNewDictionary_DirectorioDeSeriesDelPaquete<E>();
        }
    }
	
	
	
	public class ComparadorDirectorioDeSeriesDelPaquete:IEqualityComparer<DirectorioDeSeriesDelPaquete>
	{
		private static readonly ComparadorDirectorioDeSeriesDelPaquete comparadorDeIgualdad_DirectorioDeSeriesDelPaquete = new ComparadorDirectorioDeSeriesDelPaquete();
		
		public static readonly Dictionary<string,int> codigosHash = new  Dictionary<string,int>();
		public static int ultimoHash = 0;
		private string getKey(DirectorioDeSeriesDelPaquete obj)
		{
			
			return obj.carpeta.ToString();
			//return obj.getValor();
		}
		public bool Equals(DirectorioDeSeriesDelPaquete x, DirectorioDeSeriesDelPaquete y)
		{
			return getKey(x) == getKey(y);
		}
		public int GetHashCode(DirectorioDeSeriesDelPaquete obj)
		{
			string key = getKey(obj);
			if (codigosHash.ContainsKey(key)) {
				return codigosHash[key];
			}
			int hash = ultimoHash++;
			codigosHash.Add(key, hash);
			return hash;
		}
			
			
			
		public static HashSet<DirectorioDeSeriesDelPaquete> getNewHashSet_DirectorioDeSeriesDelPaquete()
		{
			return new HashSet<DirectorioDeSeriesDelPaquete>(comparadorDeIgualdad_DirectorioDeSeriesDelPaquete);
		}
		public static HashSet<DirectorioDeSeriesDelPaquete> getNewHashSet_DirectorioDeSeriesDelPaquete(IEnumerable<DirectorioDeSeriesDelPaquete> anterior)
		{
			return new HashSet<DirectorioDeSeriesDelPaquete>(anterior, comparadorDeIgualdad_DirectorioDeSeriesDelPaquete);
		}
		
		public static Dictionary<DirectorioDeSeriesDelPaquete,E> getNewDictionary_DirectorioDeSeriesDelPaquete<E>()
		{
			return new Dictionary<DirectorioDeSeriesDelPaquete, E>(comparadorDeIgualdad_DirectorioDeSeriesDelPaquete);
		}
	}
	
}
