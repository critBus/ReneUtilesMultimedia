/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 1/8/2022
 * Hora: 20:05
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
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Conjuntos;
//using ReneUtiles.Clases.Multimedia.Series.Procesadores.Ignorar;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos; 
namespace ReneUtiles.Clases.Multimedia.Series.Procesadores
{
	/// <summary>
	/// Description of HistorialDeProcesadoresDeSerie.
	/// </summary>
	public class HistorialDeProcesadoresDeSerie
	{
		//private RecursosDePatronesDeSeries re;
		Dictionary<string,ProcesadorDeNombreDeSerie> procesadores;  
		private Dictionary<string,TipoDeNombreDeSerie?> clasificacionesDeNombreDeSerie;
		//HashSet<string,>;
		
		private ProcesadorDeSeries pro;
		
		private Dictionary<string,TipoDeRecorredorDeSeries?> tiposDeRecorredoresDeSerie;
		private Dictionary<string,bool> nombresQueSonNormales;
		public HistorialDeProcesadoresDeSerie(ProcesadorDeSeries pro)
		{
			//this.re = re;
			this.pro = pro;
			this.procesadores = new Dictionary<string,ProcesadorDeNombreDeSerie>();
		}
		
		public TipoDeRecorredorDeSeries? getTipoDeRecorredor(string texto)
		{
			if (this.tiposDeRecorredoresDeSerie == null) {
				this.tiposDeRecorredoresDeSerie = new Dictionary<string,TipoDeRecorredorDeSeries?>();
			}
			if (this.tiposDeRecorredoresDeSerie.ContainsKey(texto)) {
				return this.tiposDeRecorredoresDeSerie[texto];
			}
			ConjuntoDeEtiquetasDeSerie c = new ConjuntoDeEtiquetasDeSerie(
				                              re: pro.re.reg.Re_EtiquetasDeSerie_Principal_Secundarias
			, texto: texto
			                              );
			TipoDeRecorredorDeSeries? t = c.getTipoDeRecorredor();
			this.tiposDeRecorredoresDeSerie.Add(texto, t);
			return t;
		}
		
		public TipoDeNombreDeSerie? getTipoDeNombreDe(ProcesadorDeNombreDeSerie pr, string nombreDeSerie)
		{
			if (this.clasificacionesDeNombreDeSerie == null) {
				this.clasificacionesDeNombreDeSerie = new Dictionary<string,TipoDeNombreDeSerie?>();
			}
			if (this.clasificacionesDeNombreDeSerie.ContainsKey(nombreDeSerie)) {
				return this.clasificacionesDeNombreDeSerie[nombreDeSerie];
			}
			TipoDeNombreDeSerie? t = pr.getTipoDeNombreDe(nombreDeSerie);
			
			this.clasificacionesDeNombreDeSerie.Add(nombreDeSerie, t);
			return t;
		}
		
		public ProcesadorDeNombreDeSerie getProcesadorDeSerie(
			//string url
			ContextoDeConjuntoDeSeries contextoDeConjunto
		                        , ContextoDeSerie contexto
		                      , string nombre)
		{
            //string url = contexto.Url;
            string url = contexto.Url+"&"+ nombre;
            if (procesadores.ContainsKey(url)) {
				return procesadores[url];
			}
			ProcesadorDeNombreDeSerie pr = new ProcesadorDeNombreDeSerie(
				                               contextoDeConjunto: contextoDeConjunto
				, re: this.pro.re
				, contexto: contexto
				, nombre: nombre
				, pro: this.pro);
			
			procesadores.Add(url, pr);
			return pr;
		}
		
		
		
		public bool esNombreNormal(string texto)
		{
			if (this.nombresQueSonNormales == null) {
				this.nombresQueSonNormales = new Dictionary<string,bool>();
			}
			if (this.nombresQueSonNormales.ContainsKey(texto)) {
				return this.nombresQueSonNormales[texto];
			}
			bool esNombreNormal=this.pro.__esNombreNormal(texto);
			this.nombresQueSonNormales.Add(texto,esNombreNormal);
			return esNombreNormal;
		}
	}
}
