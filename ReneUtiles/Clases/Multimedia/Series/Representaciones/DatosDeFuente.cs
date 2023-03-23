/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 31/7/2022
 * Hora: 21:31
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
//using ReneUtiles.Clases.Multimedia.Series.Procesadores.Ignorar;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Conjuntos;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos;

using System.IO;
namespace ReneUtiles.Clases.Multimedia.Series.Representaciones
{
	/// <summary>
	/// Description of DatosDeFuente.
	/// </summary>
	public class DatosDeFuente:ConsolaBasica
	{
		
		class ComparadorDatosDeSerie:IEqualityComparer<DatosDeFuente>
		{
			
			public static readonly Dictionary<string,int> codigosHash = new  Dictionary<string,int>();
			public static int ultimoHash = 0;
			private string getKey(DatosDeFuente obj)
			{
				return obj.Ctx.Url;
			}
			public bool Equals(DatosDeFuente x, DatosDeFuente y)
			{
				return getKey(x) == getKey(y);
			}
			public int GetHashCode(DatosDeFuente obj)
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
		private static readonly ComparadorDatosDeSerie comparadorDeIgualdad_DatosDeSerie=new ComparadorDatosDeSerie();
		
		//public DatosDeNombreCapitulo Dn;
		public List<DatosDeNombreSerie> Ldns;
		//public DatosNumericosDeNombreDeSerie Dns;
		//public List<DatosDeFechaEnNombre> FechasEnNombre;
		
		//		public bool esArchivo;
		//		private bool esCarpeta;
		//		public bool EsSoloNombre;//si salio de un txt (osea aun puede ser un video y por tanto tener extencion)
		//		public bool EsVideo;//{ get; set; }
		//		public int IndiceExtencion;
		public ContextoDeSerie Ctx;
		
		private HashSet<KeySerie> keysDeSerie;
		//		private HashSet<string> NombresRecortados;
		//		private HashSet<string> Claves;
		
		public DatosDeFuente()
		{
			this.keysDeSerie = KeySerie.getNewHashSet_KeySerie();
			this.Ldns=new List<DatosDeNombreSerie>();
//			this.NombresRecortados=new HashSet<string>();
//			this.Claves=new HashSet<string>();
		}
		public void addDatosNumericosDeNombreDeSerie(DatosDeNombreSerie d){
			this.Ldns.Add(d);
		}
		
		public void addKeySerie(KeySerie k)
		{
			this.keysDeSerie.Add(k);
		}
		public HashSet<KeySerie> getKeysDeSerie()
		{
			HashSet<KeySerie> r = KeySerie.getNewHashSet_KeySerie();
			foreach (DatosNumericosDeNombreDeSerie Dns in Ldns) {
				if (Dns is DatosDeNombreSerie) {
					DatosDeNombreSerie d = (DatosDeNombreSerie)Dns;
					TipoDeNombreDeSerie? t = d.getTipoDeNombre();
					KeySerie k = new KeySerie(
						           Nombre: d.NombreAdaptado
					, Clave: d.Clave
					, TipoDeSerie: t != null ? (TipoDeNombreDeSerie)t : TipoDeNombreDeSerie.DESCONOCIDO
					           );
					r.Add(k);
				}
			}
			
			return r;
		}
		
		public static HashSet<DatosDeFuente> getNewHashSet_DatosDeFuente(){
			return new HashSet<DatosDeFuente>(comparadorDeIgualdad_DatosDeSerie);
		}
		public static HashSet<DatosDeFuente> getNewHashSet_DatosDeFuente(HashSet<DatosDeFuente> s){
			return new HashSet<DatosDeFuente>(s,comparadorDeIgualdad_DatosDeSerie);
		}
		
		public int getCantidadDeCapitulos(){
			int cantidad=-1;
			foreach (DatosDeNombreSerie d in this.Ldns) {
                int c = d.getCantidadDeCapitulos();//d.getCantidadDeCapitulosDeContenedorDeTemporada();
				if(c!=-1&&c>cantidad){
					cantidad=c;
				}
			}
			return cantidad;
		}
	}
}
