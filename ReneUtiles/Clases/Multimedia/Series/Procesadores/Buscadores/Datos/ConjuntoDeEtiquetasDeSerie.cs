/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 13/8/2022
 * Hora: 16:52
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.IO;
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
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Conjuntos;
//using ReneUtiles.Clases.Multimedia.Series.Procesadores.Ignorar;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores;
using ReneUtiles.Clases.Multimedia.Series.Recorredores;

namespace ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos
{
	public enum TipoDeRecorredorDeSeries{
		ConjuntoDeSeries
			,CapitulosSueltos
			,Serie
			,TemporadaDeSerie
	}
	/// <summary>
	/// Description of ConjuntoDeEtiquetasDeSerie.
	/// </summary>
	public class ConjuntoDeEtiquetasDeSerie
	{
		
		
		public SortedSet<TipoDeEtiquetaDeSerie> etiquetas;

        public ConjuntoDeEtiquetasDeSerie(params TipoDeEtiquetaDeSerie[] etiquetas) : this(etiquetas.ToList())
        {

        }
        public ConjuntoDeEtiquetasDeSerie():this(TipoDeEtiquetaDeSerie.getNewSortedSet()) {

        }
        public ConjuntoDeEtiquetasDeSerie(
            IEnumerable<TipoDeEtiquetaDeSerie> anterior
        ) :this(TipoDeEtiquetaDeSerie.getNewSortedSet(anterior))
        {
            
        }

        public ConjuntoDeEtiquetasDeSerie(
			SortedSet<TipoDeEtiquetaDeSerie> etiquetas
		)
		{
			this.etiquetas = etiquetas;
		}
		public ConjuntoDeEtiquetasDeSerie(
			PatronRegex re, string texto
		):this(TipoDeEtiquetaDeSerie.getTiposDeEtiqueta_SiSoloEstaCompuestoPorEstas(re,texto))
		{
			
		}
		public bool isEmpty(){
			return this.etiquetas.Count==0;
		}
		public bool tieneEtiquetas_All(params TipoDeEtiquetaDeSerie[] E){
			if(E.Length==0){
				return false;
			}
			foreach (TipoDeEtiquetaDeSerie te in E) {
				bool tieneEsta=false;
				foreach (TipoDeEtiquetaDeSerie e in this.etiquetas) {
					if(e==te){
						tieneEsta=true;
						break;
					}
				}
				if(!tieneEsta){
					return false;
				}
			}
			return true;
		}
		public bool tieneEtiquetas_OR(params TipoDeEtiquetaDeSerie[] E){
			if(E.Length==0){
				return false;
			}
			foreach (TipoDeEtiquetaDeSerie te in E) {
				foreach (TipoDeEtiquetaDeSerie e in this.etiquetas) {
					if(e==te){
						return true;
					}
				}
				
			}
			return false;
		}
		public bool tieneSoloEsta(TipoDeEtiquetaDeSerie e){
			return etiquetas.Count==1&&this.etiquetas.ElementAt(0)==e;
		}
		public TipoDeEtiquetaDeSerie getEtiquetaPrincipal(){
			if(tieneEtiquetas_OR(TipoDeEtiquetaDeSerie.PRINCIPAL_MANGA)){
				return TipoDeEtiquetaDeSerie.PRINCIPAL_MANGA;
			}else if(tieneEtiquetas_OR(TipoDeEtiquetaDeSerie.PRINCIPAL_SERIES_PERSONA)){
				return TipoDeEtiquetaDeSerie.PRINCIPAL_SERIES_PERSONA;
			}
			
			return null;
		}
		public bool tieneSoloAlgunaEtiquetaPrincipal(){
			foreach (TipoDeEtiquetaDeSerie e in TipoDeEtiquetaDeSerie.ETIQUETAS_PRINCIPALES) {
				if(tieneSoloEsta(e)){
					return true;
				}
			}
			return false;
		}
		public TipoDeRecorredorDeSeries? getTipoDeRecorredor(){
			if(isEmpty()){
				return null;
			}
			if(tieneSoloAlgunaEtiquetaPrincipal()){
				return TipoDeRecorredorDeSeries.ConjuntoDeSeries;
			}
			if(tieneEtiquetas_OR(TipoDeEtiquetaDeSerie.FINALIZADAS)){
				return TipoDeRecorredorDeSeries.ConjuntoDeSeries;
			}
			if(tieneEtiquetas_OR(TipoDeEtiquetaDeSerie.TX)){
				return TipoDeRecorredorDeSeries.CapitulosSueltos;
			}
			return TipoDeRecorredorDeSeries.ConjuntoDeSeries;
		}


        public bool esFinalizadas() {
            return tieneEtiquetas_All(TipoDeEtiquetaDeSerie.FINALIZADAS);
        }

        public static ConjuntoDeEtiquetasDeSerie getNewConjunto(IEnumerable<TipoDeEtiquetaDeSerie> le) {
            return new ConjuntoDeEtiquetasDeSerie(ComparadorTipoDeEtiquetaDeSerie.getNewSortedSet_TipoDeEtiquetaDeSerie(le));
        }


        public static Dictionary<ConjuntoDeEtiquetasDeSerie, T> getNewDictionary<T>()
        {
            return ComparadorConjuntoDeEtiquetasDeSerie.getNewDictionary_ConjuntoDeEtiquetasDeSerie<T>();
        }

        public static HashSet<ConjuntoDeEtiquetasDeSerie> getNewHashSet()
        {
            return ComparadorConjuntoDeEtiquetasDeSerie.getNewHashSet_ConjuntoDeEtiquetasDeSerie();
        }
        public static HashSet<ConjuntoDeEtiquetasDeSerie> getNewHashSet(IEnumerable<ConjuntoDeEtiquetasDeSerie> anterior)
        {
            return ComparadorConjuntoDeEtiquetasDeSerie.getNewHashSet_ConjuntoDeEtiquetasDeSerie(anterior);
        }

    }
	
	
	
	public class ComparadorConjuntoDeEtiquetasDeSerie:IEqualityComparer<ConjuntoDeEtiquetasDeSerie>
	{
		private static readonly ComparadorConjuntoDeEtiquetasDeSerie comparadorDeIgualdad_ConjuntoDeEtiquetasDeSerie = new ComparadorConjuntoDeEtiquetasDeSerie();
		
		public static readonly Dictionary<string,int> codigosHash = new  Dictionary<string,int>();
		public static int ultimoHash = 0;
		private string getKey(ConjuntoDeEtiquetasDeSerie obj)
		{
			string k="";
			foreach (TipoDeEtiquetaDeSerie tk in obj.etiquetas) {
				k+=tk.keyGrup+"&";
			}
			return k;
			//return obj.getValor();
		}
		public bool Equals(ConjuntoDeEtiquetasDeSerie x, ConjuntoDeEtiquetasDeSerie y)
		{
			return getKey(x) == getKey(y);
		}
		public int GetHashCode(ConjuntoDeEtiquetasDeSerie obj)
		{
			string key = getKey(obj);
			if (codigosHash.ContainsKey(key)) {
				return codigosHash[key];
			}
			int hash = ultimoHash++;
			codigosHash.Add(key, hash);
			return hash;
		}
			
			
			
		public static HashSet<ConjuntoDeEtiquetasDeSerie> getNewHashSet_ConjuntoDeEtiquetasDeSerie()
		{
			return new HashSet<ConjuntoDeEtiquetasDeSerie>(comparadorDeIgualdad_ConjuntoDeEtiquetasDeSerie);
		}
		public static HashSet<ConjuntoDeEtiquetasDeSerie> getNewHashSet_ConjuntoDeEtiquetasDeSerie(IEnumerable<ConjuntoDeEtiquetasDeSerie> anterior)
		{
			return new HashSet<ConjuntoDeEtiquetasDeSerie>(anterior, comparadorDeIgualdad_ConjuntoDeEtiquetasDeSerie);
		}
		
		public static Dictionary<ConjuntoDeEtiquetasDeSerie,E> getNewDictionary_ConjuntoDeEtiquetasDeSerie<E>()
		{
			return new Dictionary<ConjuntoDeEtiquetasDeSerie, E>(comparadorDeIgualdad_ConjuntoDeEtiquetasDeSerie);
		}
	}
	
}
