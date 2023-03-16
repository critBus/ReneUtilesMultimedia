/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 31/7/2022
 * Hora: 21:46
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
using ReneUtiles.Clases.Multimedia.Series.Representaciones;
using ReneUtiles.Clases.Multimedia.Series.Representaciones.Capitulos;
using ReneUtiles.Clases.Multimedia.Series.Representaciones.Series;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos;
//using System.IO;
using Delimon.Win32.IO;
namespace ReneUtiles.Clases.Multimedia.Series.Representaciones.Temporadas
{
	/// <summary>
	/// Description of TemporadaDeSerie.
	/// </summary>
	public class TemporadaDeSerie:ConRepresentacionDeFuentes,IComparable<TemporadaDeSerie>
	{
		public int NumeroTemporada;
		
		
		public Serie serie;
		//public int CantidadDeCapitulos;
		public List<RepresentacionDeCapitulo> Capitulos;
		public SortedSet<int> setNumerosDeCapitulos;
		public List<RepresentacionDeCapitulo> CapitulosOva;
		public SortedSet<int> setNumerosDeCapitulosOva;
		public TemporadaDeSerie(Serie serie, int numero)
			: base()
		{
			this.Capitulos = new List<RepresentacionDeCapitulo>();
			this.CapitulosOva = new List<RepresentacionDeCapitulo>();
			this.setNumerosDeCapitulos = new SortedSet<int>();
			this.setNumerosDeCapitulosOva = new SortedSet<int>();
			this.serie = serie;
			this.NumeroTemporada = numero;
		}
		private void addCapitulo(List<RepresentacionDeCapitulo> lc, SortedSet<int> sc, RepresentacionDeCapitulo cr)
		{
			int c0 = cr.getCapitulo0();//cr is CapituloDeSerie?((CapituloDeSerie)cr).NumeroDeCapitulo:((CapituloDeSerieMultiples)cr).NumeroCapituloInicial;
			foreach (RepresentacionDeCapitulo rc in lc) {
				int rc0 = rc.getCapitulo0();
				if (c0 == rc0) {
					if ((cr is CapituloDeSerieMultiples && rc is CapituloDeSerieMultiples)
					    || ((cr is CapituloDeSerie && rc is CapituloDeSerie))) {
						//rc.Fuentes.addFuente_List(cr.Fuentes);
						rc.Fuentes.unirCon(cr.Fuentes);
						return;
					}
					
				}
			}
			
			lc.Add(cr);
			lc.Sort();
			if (cr is CapituloDeSerie) {
				sc.Add(c0);
			} else {
				int cfi = ((CapituloDeSerieMultiples)cr).NumeroCapituloFinal;
				for (int i = c0; i < cfi + 1; i++) {
					sc.Add(i);
				}
			}
		}
		public void addCapitulo(RepresentacionDeCapitulo cr)
		{
			//cwl("se llamo a agregar capitulo");
			addCapitulo(Capitulos, setNumerosDeCapitulos, cr);
		}
		public void addCapituloOva(RepresentacionDeCapitulo cr)
		{
			addCapitulo(CapitulosOva, setNumerosDeCapitulosOva, cr);
		}
		
		public int CompareTo(TemporadaDeSerie value)
		{
			return this.NumeroTemporada.CompareTo(value.NumeroTemporada);
		}
		
		public void unirCon(TemporadaDeSerie t)
		{
			unirCon_RF(t);
			foreach (RepresentacionDeCapitulo rc in t.Capitulos) {
				addCapitulo(rc);
			}
			foreach (RepresentacionDeCapitulo rc in t.CapitulosOva) {
				addCapituloOva(rc);
			}
		}
		
		public TemporadaDeSerie getCopia(Serie s)
		{
			TemporadaDeSerie t = new TemporadaDeSerie(s, this.NumeroTemporada);
			t.setCopiaDeDatos_RF(this);
			
			List<RepresentacionDeCapitulo> lc = new List<RepresentacionDeCapitulo>();
			foreach (RepresentacionDeCapitulo rc in this.Capitulos) {
				lc.Add(rc.getCopia(t));
			}
			t.Capitulos = lc;
			lc = new List<RepresentacionDeCapitulo>();
			foreach (RepresentacionDeCapitulo rc in this.CapitulosOva) {
				lc.Add(rc.getCopia(t));
			}
			t.CapitulosOva = lc;
			
			
			t.setNumerosDeCapitulos = new SortedSet<int>(setNumerosDeCapitulos);
			
			
			t.setNumerosDeCapitulosOva = new SortedSet<int>(setNumerosDeCapitulosOva);
			return t;
		}
		
		public bool tieneCapitulos_NoOvas()
		{
			return this.setNumerosDeCapitulos.Count != 0;
		}
		
		public RepresentacionDeCapitulo getUltimoCapitulo_NoOva()
		{
			if (this.Capitulos.Count != 0) {
				return this.Capitulos.Last();
			}
			return null;
		}
		
		public bool tieneAlgunCapituloUno()
		{
			return this.setNumerosDeCapitulos.Count != 0 && (this.setNumerosDeCapitulos.ElementAt(0) == 1 || this.setNumerosDeCapitulos.ElementAt(0) == 0);
		}
		
		public TemporadaDeSerie getCapitulosFaltantes()
		{
			//__mostrarFuentes();
			
			List<int> lc = new List<int>();
			int capituloAnterior = 0;
			foreach (int n in setNumerosDeCapitulos) {
				if (n > 1) {
					for (int i = capituloAnterior + 1; i < n; i++) {
						lc.Add(i);
					}
				}
				capituloAnterior = n;
			}
			
			if (lc.Count != 0 || tieneCapitulos_NoOvas()) {
				int cantidadMaxima = getCantidadDeCapitulosDeContenedorDeTemporada();
				if (cantidadMaxima != -1) {
					int ultimoCapitulo = setNumerosDeCapitulos.Last();
					for (int i = ultimoCapitulo + 1; i < cantidadMaxima + 1; i++) {
						lc.Add(i);
					}
				}
			}
			
			if (lc.Count == 0) {
				return null;
			}
			TemporadaDeSerie t = new TemporadaDeSerie(this.serie, this.NumeroTemporada);
			foreach (int n in lc) {
				CapituloDeSerie c = new CapituloDeSerie(t);
				c.NumeroDeCapitulo = n;
				t.addCapitulo(c);
			}
			return t;
		}
		
		
		public TemporadaDeSerie getCapitulosFaltantesSuperiores()
		{
			List<int> lc = new List<int>();
			int capituloAnterior = -1;
			foreach (int n in setNumerosDeCapitulos) {
				if (capituloAnterior != -1) {
					for (int i = capituloAnterior + 1; i < n; i++) {
						lc.Add(i);
					}
				}
				
				capituloAnterior = n;
				
			}
			
			if (lc.Count != 0 || tieneCapitulos_NoOvas()) {
				int cantidadMaxima = getCantidadDeCapitulosDeContenedorDeTemporada();
				if (cantidadMaxima != -1) {
					int ultimoCapitulo = setNumerosDeCapitulos.Last();
					for (int i = ultimoCapitulo + 1; i < cantidadMaxima + 1; i++) {
						lc.Add(i);
					}
				}
			}
			
			if (lc.Count == 0) {
				return null;
			}
			TemporadaDeSerie t = new TemporadaDeSerie(this.serie, this.NumeroTemporada);
			foreach (int n in lc) {
				CapituloDeSerie c = new CapituloDeSerie(t);
				c.NumeroDeCapitulo = n;
				t.addCapitulo(c);
			}
			return t;
		}
		public int getCantidadDeCapitulosDeContenedorDeTemporada()
		{
			
			return this.Fuentes.getCantidadDeCapitulosDeContenedorDeTemporada();
		}
		
		public List<RepresentacionDeCapitulo> getCapitulos(int numero)
		{
			List<RepresentacionDeCapitulo> lc=new List<RepresentacionDeCapitulo>();
			foreach (RepresentacionDeCapitulo rc in this.Capitulos) {
				if(rc is CapituloDeSerie){
					CapituloDeSerie c=(CapituloDeSerie)rc;
					if(c.NumeroDeCapitulo==numero){
						lc.Add(c);
						continue;
					}
				}else{
					CapituloDeSerieMultiples c=(CapituloDeSerieMultiples)rc;
					if(c.NumeroCapituloInicial<=numero&&c.NumeroCapituloFinal>=numero){
						lc.Add(c);
						continue;
					}
				}
				if(rc.getCapitulo0()>numero){
					break;
				}
			}
			return lc;
		}
		public List<RepresentacionDeCapitulo> getCapitulosMayoresIgualQue(int numero)
		{
			List<RepresentacionDeCapitulo> lc=new List<RepresentacionDeCapitulo>();
			foreach (RepresentacionDeCapitulo rc in this.Capitulos) {
				if(rc is CapituloDeSerie){
					CapituloDeSerie c=(CapituloDeSerie)rc;
					if(c.NumeroDeCapitulo>=numero){
						lc.Add(c);
						continue;
					}
				}else{
					CapituloDeSerieMultiples c=(CapituloDeSerieMultiples)rc;
					if(c.NumeroCapituloInicial>=numero||c.NumeroCapituloFinal>=numero){
						lc.Add(c);
						continue;
					}
				}
				if(rc.getCapitulo0()>numero){
					break;
				}
			}
			return lc;
		}
		public List<RepresentacionDeCapitulo> getCapitulosMenoresIgualQue(int numero)
		{
			List<RepresentacionDeCapitulo> lc=new List<RepresentacionDeCapitulo>();
			foreach (RepresentacionDeCapitulo rc in this.Capitulos) {
				if(rc is CapituloDeSerie){
					CapituloDeSerie c=(CapituloDeSerie)rc;
					if(c.NumeroDeCapitulo<=numero){
						lc.Add(c);
						continue;
					}
				}else{
					CapituloDeSerieMultiples c=(CapituloDeSerieMultiples)rc;
					if(c.NumeroCapituloInicial<=numero){
						lc.Add(c);
						continue;
					}
				}
				if(rc.getCapitulo0()>numero){
					break;
				}
			}
			return lc;
		}
		
		
		
		private void __mostrarFuentes()
		{
			foreach (DatosDeFuente df in this.Fuentes.datosDefuentes) {
				foreach (DatosDeNombreSerie d in df.Ldns) {
					__mostrarDatosDeNombreSerie(d, df.Ctx);
				}
			}
		}
		private  void __mostrarDatosDeNombreSerie(DatosDeNombreSerie dn, ContextoDeSerie ctx)
		{
			FileSystemInfo f = ctx.F;
			DatosDeNombreCapitulo d = null;
			if (dn.datosDelFinal == null) {
				d = dn.datosDelPrincipio;
			} else {
				d = dn.datosDelFinal;
			}
			//DatosDeNombreCapitulo d = dn.datosDelFinal ?? dn.datosDelPrincipio;
			if (d != null) {
				string temporada = d.tieneTemporada_Unica() ? " T=" + d.getTemporada() : "";//d.TieneTemporada ? " T=" + d.Temporada : "";
                string esSoloNumero = d.EsSoloNumeros ? " sn" : "";
				//string alFinal = temporada + esSoloNumero + " i=" + d.IndiceDeInicioDespuesDeLosNumeros;
				string alFinal = temporada + esSoloNumero + " " + dn.Clave;
                //if (d.EsConjuntoDeCapitulos) {
                if (d.esConjuntoDeCapitulos())
                {
                    //awl(d.CapituloInicial + " - " + d.CapituloFinal + alFinal);
                    cwl(d.getCapituloInicial() + " - " + d.getCapituloFinal() + alFinal);
				} else {
					if (d.esContenedorDeTemporada()) {
						cwl("c=" + d.getCantidadDeCapitulos() + " " + alFinal);
						//awl("c=" + d.CantidadDeCapitulosQueContiene + " " + alFinal);
					} else {
						//awl(d.Capitulo + alFinal);//+" :"
						cwl(d.getCapituloInicial() + alFinal);
					}
								
				}
				//awStringIndices(5, f.Name);
				UtilesConsola.cwStringIndices(5, f.Name);
				//return !d.EsContenedorDeTemporada;
				return;
							
			} else {
				//awl("null " + f.ToString());
				cwl("null " + f.ToString());
			}
		}
		
	}
}
