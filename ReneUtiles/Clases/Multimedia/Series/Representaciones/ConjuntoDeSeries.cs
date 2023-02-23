/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 31/7/2022
 * Hora: 21:52
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
using ReneUtiles.Clases.Multimedia.Series.Representaciones.Series;
using ReneUtiles.Clases.Multimedia.Series.Representaciones.Capitulos;
using ReneUtiles.Clases.Multimedia.Series.Representaciones.Temporadas;
using ReneUtiles.Clases.Multimedia.Relacionadores;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos;
namespace ReneUtiles.Clases.Multimedia.Series.Representaciones
{
	/// <summary>
	/// Description of ConjuntoDeSeries.
	/// </summary>
	public class ConjuntoDeSeries:ConRepresentacionDeFuentes
	{
		class Row_KeyEquivalentes
		{
			public bool usado = false;
			public KeySerie[] keys;
		}
		class ConjuntoDeCF_KeyEquivalentes
		{
			
			public Row_KeyEquivalentes[] conjuntos;
			public ConjuntoDeCF_KeyEquivalentes(ConfiguracionDeSeries cf)
			{//ConjuntoDeSeries cg,
				this.conjuntos = new Row_KeyEquivalentes[cf.keysEquivalentes.Length];
				for (int i = 0; i < cf.keysEquivalentes.Length; i++) {
					this.conjuntos[i] = new Row_KeyEquivalentes();
					this.conjuntos[i].keys = cf.keysEquivalentes[i];
				}
			
			}
			public void unirCon(ConjuntoDeCF_KeyEquivalentes c)
			{
				for (int i = 0; i < c.conjuntos.Length; i++) {
					if (c.conjuntos[i].usado) {
						this.conjuntos[i].usado = c.conjuntos[i].usado;
					}
				}
			}
			public void setCopia(ConjuntoDeCF_KeyEquivalentes c)
			{
				this.conjuntos = new Row_KeyEquivalentes[c.conjuntos.Length];
				for (int i = 0; i < c.conjuntos.Length; i++) {
					this.conjuntos[i] = new Row_KeyEquivalentes();
					this.conjuntos[i].usado = c.conjuntos[i].usado;
					this.conjuntos[i].keys = c.conjuntos[i].keys;
				}
			}
			
		}
		
		public List<Serie> series;
		public ProcesadorDeRelacionesDeNombresClaveSeries proR;
		private Dictionary<string,Serie> seriesPorClave;
		
		private ConfiguracionDeSeries cf;
		private ConjuntoDeCF_KeyEquivalentes ckequi;
		public ConjuntoDeSeries(ProcesadorDeRelacionesDeNombresClaveSeries proR, ConfiguracionDeSeries cf)
			: base()
		{
			this.seriesPorClave = new Dictionary<string,Serie>();
			this.series = new List<Serie>();
			
			this.proR = proR;
			this.cf = cf;
			this.ckequi = new ConjuntoDeCF_KeyEquivalentes(cf);
			this.proR = proR;
		}
		public Serie getSerieYCrearSiNoExiste(List<KeySerie> lk, string url)
		{
			Serie s = null;
			foreach (KeySerie k in lk) {
				s = getSerie(k.Clave);
				if (s != null) {
					break;
					//s.addKeySerieAll(lk,url);
				}
			}
			if (s == null) {
				s = new Serie();
				this.series.Add(s);
			}
			s.addKeySerieAll(lk, url);
			foreach (KeySerie k in lk) {
				if (!this.seriesPorClave.ContainsKey(k.Clave)) {
					seriesPorClave.Add(k.Clave, s);
				}
				
			}
			return s;
		}
		public Serie getSerieYCrearSiNoExiste(string clave, string nombre, TipoDeNombreDeSerie? tipoDeSerie)
		{
			Serie s = getSerie(clave);
			if (s == null) {
				s = new Serie();
				
				s.addKeySerie(new KeySerie(Nombre: nombre
				                               , Clave: clave
				                               , TipoDeSerie: tipoDeSerie != null ? (TipoDeNombreDeSerie)tipoDeSerie : TipoDeNombreDeSerie.DESCONOCIDO));
//				s.addNombreYClave(nombre: nombre
//								, clave: clave);
				series.Add(s);
				seriesPorClave.Add(clave, s);
				
				
				foreach (Row_KeyEquivalentes row in this.ckequi.conjuntos) {
					if (!row.usado) {
						foreach (KeySerie k in row.keys) {
							if (proR.estanRelacionados(k.Clave, clave)) {
								foreach (KeySerie k2 in row.keys) {
									s.addKeySerie(k2);
									if (!seriesPorClave.ContainsKey(k2.Clave)) {
										seriesPorClave.Add(k2.Clave, s);
									}
								}
								row.usado = true;
								break;
							}
						}
						if (row.usado) {
							break;
						}
					}
					//this.
				}
				
			}
			return s;
		}
		public Serie getSerie(string clave)
		{
			
			
			if (this.seriesPorClave.ContainsKey(clave)) {
				return this.seriesPorClave[clave];
			}
			foreach (Serie s in this.series) {
				//foreach (string c in s.getClaves()) {
				HashSet<KeySerie> keys = s.getKeysDeSerie();
				foreach (KeySerie k in keys) {
					//cwl("k.Clave="+k.Clave);//aratakangatari  //magickaito
//					if(k.Clave=="aratakangatari"){
//						cwl();
//					}
					if (proR.estanRelacionados(k.Clave, clave)) {
						seriesPorClave.Add(clave, s);
						return s;
					}
				}
			}
			return null;
		}
		public void addSerie_SinRevisar(Serie s)
		{
			this.series.Add(s);
            HashSet<KeySerie> hasSetKey = s.getKeysDeSerie();

            foreach (KeySerie k in hasSetKey) {
				if (!this.seriesPorClave.ContainsKey(k.Clave)) {
					seriesPorClave.Add(k.Clave, s);
				}
			}
			
		}
		public Serie getSerie(HashSet<KeySerie> hk)
		{
			Serie encontrada = null;
			foreach (KeySerie k in hk) {
				encontrada = getSerie(k.Clave);
				if (encontrada != null) {
					return encontrada;
				}
				
				 
			}
			return null;
			//if(encontrada==null){}
		}
		public bool tieneEstaSerie(Serie s)
		{
			return getSerie(s.getKeysDeSerie()) != null;
		}
		
		public void unirCon(ConjuntoDeSeries cs)
		{
			foreach (Serie s in cs.series) {
				Serie encontrada = null;
				//List<string> clavesNoEncontradas = new List<string>();
				Dictionary<KeySerie,HashSet<string>> keysNoEncontradas = KeySerie.getNewDictionary_KeySerie<HashSet<string>>();
				foreach (KeyValuePair<KeySerie,HashSet<string>> row in s.keysDeSerieYUrls) {
					KeySerie k = row.Key;
					if (encontrada == null) {
						encontrada = getSerie(k.Clave);
						if (encontrada == null) {
							keysNoEncontradas.Add(k, row.Value);
						}
					} else {
						if (!seriesPorClave.ContainsKey(k.Clave)) {
							keysNoEncontradas.Add(k, row.Value);
						}
					}
					
					
					
				}
				
				if (encontrada != null) {
					encontrada.unirCon(s);
					foreach (KeyValuePair<KeySerie,HashSet<string>> row in keysNoEncontradas) {
						KeySerie k = row.Key;
						if (!this.seriesPorClave.ContainsKey(k.Clave)) {
							seriesPorClave.Add(k.Clave, encontrada);
						}
					}
				} else {
					addSerie_SinRevisar(s);
				}
			}
			
			this.ckequi.unirCon(cs.ckequi);
		}
		public ConjuntoDeSeries getCopia()
		{
			ConjuntoDeSeries c = new ConjuntoDeSeries(this.proR, this.cf);
			c.setCopiaDeDatos_RF(this);
			List<Serie> ls = new List<Serie>();
			Dictionary<string,Serie> ds = new Dictionary<string, Serie>();
			foreach (Serie s in this.series) {
				Serie sc = s.getCopia();
				foreach (KeyValuePair<string,Serie> row in this.seriesPorClave) {
					if (row.Value == s) {
						ds.Add(row.Key, sc);
					}
				}
				ls.Add(sc);
			}
			c.series = ls;
			c.ckequi.setCopia(this.ckequi);
			return c;
			
		}
		public ConjuntoDeSeries getSeriesPropiasQueCoincidenConLasDe(ConjuntoDeSeries cs)
		{
			//ConjuntoDeSeries copiaActual=getCopia();
			ConjuntoDeSeries respuesta = new ConjuntoDeSeries(this.proR, this.cf);
			foreach (Serie s in this.series) {
				if (cs.tieneEstaSerie(s)) {
					respuesta.addSerie_SinRevisar(s);
				}
			}
			return respuesta;
		}
		public List<RepresentacionDeCapitulo> getUltimosCapitulos()
		{
			List<RepresentacionDeCapitulo> r = new List<RepresentacionDeCapitulo>();
			foreach (Serie s in this.series) {
				if (s.tieneCapitulos_NoOvas()) {
					r.Add(s.getUltimoCapitulo_NoOva());
				}
			}
			return r;
		}
		public ConjuntoDeSeries getSeriesConCapitulosUno()
		{
			ConjuntoDeSeries respuesta = new ConjuntoDeSeries(this.proR, this.cf);
			foreach (Serie s in this.series) {
				if (s.tieneAlgunCapituloUno()) {
					respuesta.addSerie_SinRevisar(s);
				}
			}
			return respuesta;
		}
		public ConjuntoDeSeries getCapitulosFaltantes()
		{
			ConjuntoDeSeries respuesta = new ConjuntoDeSeries(this.proR, this.cf);
			foreach (Serie s in this.series) {
				Serie sFaltantes = s.getCapitulosFaltantes();
				if (sFaltantes != null) {
					respuesta.addSerie_SinRevisar(sFaltantes);
				}
			}
			return respuesta;
		}
		public ConjuntoDeSeries getCapitulosFaltantesSuperiores()
		{
			ConjuntoDeSeries respuesta = new ConjuntoDeSeries(this.proR, this.cf);
			foreach (Serie s in this.series) {
				Serie sFaltantes = s.getCapitulosFaltantesSuperiores();
				if (sFaltantes != null) {
					respuesta.addSerie_SinRevisar(sFaltantes);
				}
			}
			return respuesta;
		}
		
		public List<RepresentacionDeCapitulo> getCapitulos(int numero)
		{
			List<RepresentacionDeCapitulo> lc = new List<RepresentacionDeCapitulo>();
			foreach (Serie s in this.series) {
				lc.AddRange(s.getCapitulos(numero));
			}
			return lc;
		}
		public List<RepresentacionDeCapitulo> getCapitulosMayoresIgualQue(int numero)
		{
			List<RepresentacionDeCapitulo> lc = new List<RepresentacionDeCapitulo>();
			foreach (Serie s in this.series) {
				lc.AddRange(s.getCapitulosMayoresIgualQue(numero));
			}
			return lc;
		}
		public List<RepresentacionDeCapitulo> getCapitulosMenoresIgualQue(int numero)
		{
			List<RepresentacionDeCapitulo> lc = new List<RepresentacionDeCapitulo>();
			foreach (Serie s in this.series) {
				lc.AddRange(s.getCapitulosMenoresIgualQue(numero));
			}
			return lc;
		}


        public bool isEmpty() {
            return this.series.Count == 0;
        }
	}
}



//		public Serie getSerieYCrearSiNoExiste(KeySerie,HashSet<string>)
//		{
//			Serie encontrada = null;
//			List<string> clavesNoEncontradas = new List<string>();
//			foreach (KeySerie k in hk) {
//				if (encontrada == null) {
//					encontrada = getSerie(k.Clave);
//					if (encontrada == null) {
//						clavesNoEncontradas.Add(k.Clave);
//					}
//				}else{
//					if(!seriesPorClave.ContainsKey(k.Clave)){
//						clavesNoEncontradas.Add(k.Clave);
//					}
//				}
//
//			}
//
//			if(encontrada==null){
//			//Aqui!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
//			}
//		}
//		