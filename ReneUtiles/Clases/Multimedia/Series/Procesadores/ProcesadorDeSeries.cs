/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 2/8/2022
 * Hora: 15:41
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
	/// Description of ProcesadorDeSeries.
	/// </summary>
	public class ProcesadorDeSeries
	{
		private HistorialDeProcesadoresDeSerie historial;  
		public  RecursosDePatronesDeSeries re;
		public ProcesadorDeSeries(RecursosDePatronesDeSeries re)
		{
			this.re=re; 
			//this.re=new RecursosDePatronesDeSeries();
			this.historial=new HistorialDeProcesadoresDeSerie(this);
		}
		
		public ProcesadorDeNombreDeSerie getProcesadorDeSerie(
			//string url
		      ContextoDeConjuntoDeSeries contextoDeConjunto
		                        , ContextoDeSerie contexto
		                      , string nombre)
		{
			return historial.getProcesadorDeSerie(
				contextoDeConjunto:contextoDeConjunto
			,contexto:contexto
			,nombre:nombre);
		}
		
		public TipoDeNombreDeSerie? getTipoDeNombreDe(ProcesadorDeNombreDeSerie pr,string nombreDeSerie){
			return this.historial.getTipoDeNombreDe(pr,nombreDeSerie);
		}
		
		public TipoDeRecorredorDeSeries? getTipoDeRecorredor(string texto)
		{
			 return this.historial.getTipoDeRecorredor(texto);
		}
		public bool esNombreNormal(string texto){
			return this.historial.esNombreNormal(texto);
		}
		public bool __esNombreNormal(string texto){
			texto=Utiles.arreglarPalabra(texto.ToLower());
			return this.re.reg.Re_SoloPalabrasNormales.ReInicialFinal.Match(texto).Success;
			
		}
	}
}
