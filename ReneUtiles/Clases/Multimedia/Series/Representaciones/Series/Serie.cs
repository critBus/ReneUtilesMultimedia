/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 31/7/2022
 * Hora: 21:49
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
//using ReneUtiles.Clases.Multimedia.Series.Procesadores.Ignorar;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Conjuntos;
using ReneUtiles.Clases.Multimedia.Series.Representaciones.Capitulos;
using ReneUtiles.Clases.Multimedia.Series.Representaciones.Temporadas;
using ReneUtiles.Clases.Multimedia.Series.Representaciones.Series;
using ReneUtiles.Clases.Multimedia.Series.Representaciones;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos;
namespace ReneUtiles.Clases.Multimedia.Series.Representaciones.Series
{
	/// <summary>
	/// Description of Serie.
	/// </summary>
	public class Serie:ConRepresentacionDeFuentes
	{
		public List<TemporadaDeSerie> Temporadas;
		public Dictionary<KeySerie,HashSet<string>> keysDeSerieYUrls;
		public Serie()
			: base()
		{
//			this.nombresPropios=new HashSet<string>();
//			this.clavesPropias=new HashSet<string>();
			this.Temporadas = new List<TemporadaDeSerie>();
			//this.keysDeSerie=KeySerie.getNewHashSet_KeySerie();
			this.keysDeSerieYUrls = KeySerie.getNewDictionary_KeySerie<HashSet<string>>();
		}
		public void addKeySerieAll(List<KeySerie> lk, string url)
		{
			foreach (KeySerie k in lk) {
				addKeySerie(k, url);
			}
		}
		public void addKeySerie(KeySerie k, string url)
		{
			if (this.keysDeSerieYUrls.ContainsKey(k)) {
				this.keysDeSerieYUrls[k].Add(url);
				return;
			}
			HashSet<string> hs = new HashSet<string>();
			hs.Add(url);
			this.keysDeSerieYUrls.Add(k, hs);
			//this.keysDeSerie.Add(k);
		}
		public void addKeySerie(KeySerie k)
		{
			if (this.keysDeSerieYUrls.ContainsKey(k)) {
				//this.keysDeSerieYUrls[k].Add(url);
				return;
			}
			HashSet<string> hs = new HashSet<string>();
			//hs.Add(url);
			this.keysDeSerieYUrls.Add(k, hs);
			//this.keysDeSerie.Add(k);
		}
		
		
		public HashSet<KeySerie> getKeysDeSerie()
		{
			HashSet<KeySerie> r = this.Fuentes.getKeysDeSerie();
			//r.Concat(keysDeSerie);
			//HashSet<KeySerie> z=new HashSet<KeySerie>(this.keysDeSerieYUrls.Keys);
			
//			foreach (KeySerie k in this.keysDeSerieYUrls.Keys) {
//				KeySerie k0=r.ElementAt(0);
//				if(k0.Clave==k.Clave){
//					cwl("son iguales");
//				}
//				r.Add(k);
//			}
			r = KeySerie.getNewHashSet_KeySerie(r.Concat(this.keysDeSerieYUrls.Keys));
			return r;
		}
		public Dictionary<KeySerie,HashSet<string>> getKeysDeSerieYUrls()
		{
			Dictionary<KeySerie,HashSet<string>> r = this.Fuentes.getKeysDeSerieYUrls();
			//Dictionary<KeySerie,HashSet<string>> z=this.keysDeSerieYUrls;
			
			;
			//r=new  Dictionary<KeySerie,HashSet<string>>(r.Concat(this.keysDeSerieYUrls));
			foreach (KeyValuePair<KeySerie,HashSet<string>> e in this.keysDeSerieYUrls) {
				if(r.ContainsKey(e.Key)){
					HashSet<string> hs=r[e.Key];
					foreach (string ks in e.Value) {
						hs.Add(ks);
					}
				}else{
					r.Add(e.Key, e.Value);
				}
//				try {
//					r.Add(e.Key, e.Value);
//				} catch (Exception) {
//					cwl("error aqui");
//					throw;
//				}
				
			}
			return r;
		}
		public TemporadaDeSerie getYCrearTemporadaSiNoExiste(int temporada)
		{
			for (int i = 0; i < this.Temporadas.Count; i++) {
				TemporadaDeSerie t = Temporadas[i];
				if (t.NumeroTemporada == temporada) {
					return t;
				}
				if (t.NumeroTemporada > temporada) {
					break;
				}
			}
			TemporadaDeSerie tNueva = new TemporadaDeSerie(this, temporada);
			Temporadas.Add(tNueva);
			Temporadas.Sort();
			return tNueva;
		}
		public void addCapitulo(int temporada, RepresentacionDeCapitulo rc)
		{
			getYCrearTemporadaSiNoExiste(temporada).addCapitulo(rc);
		}
		
		public void unirCon(Serie s)
		{
			unirCon_RF(s);
			foreach (TemporadaDeSerie tc in s.Temporadas) {
				TemporadaDeSerie t = getYCrearTemporadaSiNoExiste(tc.NumeroTemporada);
				t.unirCon(tc);
			}
			foreach (KeyValuePair<KeySerie,HashSet<string>> row in this.keysDeSerieYUrls) {
				if (this.keysDeSerieYUrls.ContainsKey(row.Key)) {
					HashSet<string> hk = this.keysDeSerieYUrls[row.Key];
					foreach (string url in row.Value) {
						hk.Add(url);
					}
				} else {
					this.keysDeSerieYUrls.Add(row.Key, row.Value);
				}
				
			}
		}
		
		public Serie getCopia()
		{
			Serie s = new Serie();
			s.setCopiaDeDatos_RF(this);
			
			Dictionary<KeySerie,HashSet<string>> dk = KeySerie.getNewDictionary_KeySerie<HashSet<string>>();
			foreach (KeyValuePair<KeySerie,HashSet<string>> row in this.keysDeSerieYUrls) {
				dk.Add(row.Key, new HashSet<string>(row.Value));
			}
			s.keysDeSerieYUrls = dk;
			
			List<TemporadaDeSerie> lt = new List<TemporadaDeSerie>();
			foreach (TemporadaDeSerie t in this.Temporadas) {
				lt.Add(t.getCopia(s));
			}
			s.Temporadas = lt;
			
			return s;
			
		}
		
		public bool tieneCapitulos_NoOvas()
		{
			foreach (TemporadaDeSerie t in this.Temporadas) {
				if (t.tieneCapitulos_NoOvas()) {
					return true;
				}
			}
			return false;
		}
		
		public RepresentacionDeCapitulo getUltimoCapitulo_NoOva()
		{
			for (int i = this.Temporadas.Count - 1; i >= 0; i--) {
				TemporadaDeSerie t = this.Temporadas[i];
				if (t.tieneCapitulos_NoOvas()) {
					return t.getUltimoCapitulo_NoOva();
				}
			}
			return null;
		}
		public bool tieneAlgunCapituloUno()
		{
			return this.Temporadas.Count != 0 && (this.Temporadas[0].NumeroTemporada == 1 || this.Temporadas[0].NumeroTemporada == 0) && this.Temporadas[0].tieneAlgunCapituloUno();
		}
		
		public Serie getCapitulosFaltantes()
		{
			bool hayCapitulos = false;
			Serie s = new Serie();
			foreach (TemporadaDeSerie t in this.Temporadas) {
				TemporadaDeSerie tFaltantes = t.getCapitulosFaltantes();
				if (tFaltantes != null) {
					hayCapitulos = true;
					s.Temporadas.Add(tFaltantes);
				}
			}
			if (!hayCapitulos) {
				return null;
			}
			
			return s;
		}
		
		
		public Serie getCapitulosFaltantesSuperiores()
		{
			bool hayCapitulos = false;
			Serie s = new Serie();
			foreach (TemporadaDeSerie t in this.Temporadas) {
				TemporadaDeSerie tFaltantes = t.getCapitulosFaltantesSuperiores();
				if (tFaltantes != null) {
					hayCapitulos = true;
					s.Temporadas.Add(tFaltantes);
				}
			}
			if (!hayCapitulos) {
				return null;
			}
			
			return s;
		}
		
		public List<RepresentacionDeCapitulo> getCapitulos(int numero)
		{
			List<RepresentacionDeCapitulo> lc = new List<RepresentacionDeCapitulo>();
			foreach (TemporadaDeSerie t in this.Temporadas) {
				lc.AddRange(t.getCapitulos(numero));
			}
			return lc;
		}
		
		public List<RepresentacionDeCapitulo> getCapitulosMayoresIgualQue(int numero)
		{
			List<RepresentacionDeCapitulo> lc = new List<RepresentacionDeCapitulo>();
			foreach (TemporadaDeSerie t in this.Temporadas) {
				lc.AddRange(t.getCapitulosMayoresIgualQue(numero));
			}
			return lc;
		}
		public List<RepresentacionDeCapitulo> getCapitulosMenoresIgualQue(int numero)
		{
			List<RepresentacionDeCapitulo> lc = new List<RepresentacionDeCapitulo>();
			foreach (TemporadaDeSerie t in this.Temporadas) {
				lc.AddRange(t.getCapitulosMenoresIgualQue(numero));
			}
			return lc;
		}
	
	}
}


//		private HashSet<string> clavesPropias;
//		private HashSet<string> nombresPropios;
//private HashSet<KeySerie> keysDeSerie;
	
//		public HashSet<string> getClaves(){
//			HashSet<string> r=this.Fuentes.getClaves();
//			r.Concat(this.clavesPropias);
//			return r;
//		}
//		public HashSet<string> getNombres(){
//			HashSet<string> r=this.Fuentes.getNombresRecortados();
//			r.Concat(this.nombresPropios);
//			return r;
//		}
//		public void addNombreYClave(string nombre,string clave){
//			this.nombresPropios.Add(nombre);
//			this.clavesPropias.Add(clave);
//		}
		
//		public bool tieneNombreYClave(){
//			return clavesPropias.Count!=0&&nombresPropios.Count!=0;
//		}
