/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 8/8/2022
 * Hora: 10:14
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using ReneUtiles.Clases.Multimedia;
namespace ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos
{
	/// <summary>
	/// Description of KeySerie.
	/// </summary>
	public class KeySerie
	{
		class ComparadorKeySerie:IEqualityComparer<KeySerie>
		{
			
			public static readonly Dictionary<string,int> codigosHash = new  Dictionary<string,int>();
			public static int ultimoHash = 0;
			private string getKey(KeySerie obj)
			{
				return obj.Clave;
			}
			public bool Equals(KeySerie x, KeySerie y)
			{
				return getKey(x) == getKey(y);
			}
			public int GetHashCode(KeySerie obj)
			{
				string key = getKey(obj);
				if (codigosHash.ContainsKey(key)) {
					return codigosHash[key];
				}
				int hash = ultimoHash++;
				codigosHash.Add(key, hash);
				return hash;
			}
		}
		private static readonly ComparadorKeySerie comparadorDeIgualdadKeySerie=new ComparadorKeySerie();
		
		public string Nombre;
		public string Clave;
		public TipoDeNombreDeSerie TipoDeSerie;
		public KeySerie(string Nombre, string Clave, TipoDeNombreDeSerie TipoDeSerie)
		{
			this.Nombre = Nombre;
			this.Clave = Clave;
			this.TipoDeSerie = TipoDeSerie;
			//comparer;
			
			//HashSet<KeySerie> hass = new HashSet<KeySerie>();
		}
		
		public static HashSet<KeySerie> getNewHashSet_KeySerie(){
			return new HashSet<KeySerie>(comparadorDeIgualdadKeySerie);
		}
		public static HashSet<KeySerie> getNewHashSet_KeySerie(IEnumerable<KeySerie> anterior){
			return new HashSet<KeySerie>(anterior,comparadorDeIgualdadKeySerie);
		}
		
		public static Dictionary<KeySerie,E> getNewDictionary_KeySerie<E>(){
			return new Dictionary<KeySerie, E>(comparadorDeIgualdadKeySerie);
		}//
		
	}
}
