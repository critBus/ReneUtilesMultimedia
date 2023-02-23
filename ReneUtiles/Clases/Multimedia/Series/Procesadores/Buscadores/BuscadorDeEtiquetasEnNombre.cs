/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 08/15/2022
 * Hora: 14:09
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
using ReneUtiles.Clases.Multimedia.Series.Procesadores;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos;
using System.IO;
using ReneUtiles.Clases.ExprecionesRegulares;

namespace ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores
{
	/// <summary>
	/// Description of BuscadorDeEtiquetasEnNombre.
	/// </summary>
	public class BuscadorDeEtiquetasEnNombre:BuscadorDeDatosEnNombre
	{
		public Dictionary<TipoDeEtiquetaDeSerie,List<DatosDeEtiquetaEnNombre>> etiquetasEnNombre;
		//		public int indiceAContinuacion;
		//		public int indiceInicial;
		public MatchCollection mc;
		public BuscadorDeEtiquetasEnNombre(ProcesadorDeNombreDeSerie pr)
			: base(pr)
		{
			
			this.etiquetasEnNombre = new Dictionary<TipoDeEtiquetaDeSerie,List<DatosDeEtiquetaEnNombre>>();
		}
		private void agregarLasEtiquetasQueContengan(TipoDeEtiquetaDeSerie[] tags, Match m)
		{
			foreach (TipoDeEtiquetaDeSerie t in tags) {
				Group gt = t.getGroup(m);
				if (gt.Success) {
					List<DatosDeEtiquetaEnNombre> l = null;
					if (!this.etiquetasEnNombre.ContainsKey(t)) {
						l = new List<DatosDeEtiquetaEnNombre>();
						this.etiquetasEnNombre.Add(t, l);
					} else {
						l = this.etiquetasEnNombre[t];
					}
					
					foreach (Capture c in gt.Captures) {
						DatosDeEtiquetaEnNombre d = new DatosDeEtiquetaEnNombre();
						d.indiceInicial = c.Index;
						d.indiceFinal = c.Index + c.Length-1;
						d.indiceAContinuacion = c.Index + c.Length;
						
						d.etiqueta = c.ToString();
						d.tipoDeEtiqueta = t;
						l.Add(d);
					}
				}
			}
		}
		
		public Dictionary<TipoDeEtiquetaDeSerie,List<DatosDeEtiquetaEnNombre>> buscarEtiquetas(int i0)
		{
			if (seBuscoCon(i0)) {
				return this.etiquetasEnNombre;
			}
			this.seBusco = true;
			this.I0 = i0;
			this.mc = getRe().reg.Re_EtiquetasDeSerie.SSfreSfS.Matches(this.nombre, i0);
			foreach (Match m in mc) {
				if (m.Success) {
					agregarLasEtiquetasQueContengan(TipoDeEtiquetaDeSerie.ETIQUETAS_PRINCIPALES, m);
					agregarLasEtiquetasQueContengan(TipoDeEtiquetaDeSerie.ETIQUETAS_DE_CLASIFICACION, m);
//					this.indiceAContinuacion=m.Index + m.Length;
//					this.indiceInicial=m.Index;
					
				}
			}
			return this.etiquetasEnNombre;
		}
		
		
		public bool estaDentroDeEtiquetas(int indice)
		{
			foreach (KeyValuePair<TipoDeEtiquetaDeSerie,List<DatosDeEtiquetaEnNombre>> row in this.etiquetasEnNombre) {
				foreach (DatosDeEtiquetaEnNombre d in row.Value) {
					if (d != null && d.estaDentroDeLosLimites(indice)) {
						return true;
					}
				}
			}
//			foreach (DatosDeEtiquetaEnNombre d in this.etiquetasEnNombre) {
//				if (d != null && d.estaDentroDeLosLimites(indice)) {
//					return true;
//				}
//			}
			return false;
			//return this.fechaEnNombre!=null?this.fechaEnNombre.estaDentroDeLosLimites(indice):false;
		}
		
		public DatosDeIgnorarNumero estaDentroDeEtiquetas_DatosDeIgnorarNumero(int indice)
		{
			foreach (KeyValuePair<TipoDeEtiquetaDeSerie,List<DatosDeEtiquetaEnNombre>> row in this.etiquetasEnNombre) {
				foreach (DatosDeEtiquetaEnNombre d in row.Value) {
					DatosDeIgnorarNumero dd = null;
					if (d != null && (dd = d.estaDentroDeLosLimites_DatosDeIgnorarNumero(indice))!=null) {
						return dd;
					}
				}
			}
			
//			foreach (DatosDeEtiquetaEnNombre d in this.etiquetasEnNombre) {
//				DatosDeIgnorarNumero dd = null;
//				if (d != null) {
//					dd = d.estaDentroDeLosLimites_DatosDeIgnorarNumero(indice);
//					if (dd != null) {
//						return dd;
//					}
//					
//				}
//			}
			return null;
			//return this.fechaEnNombre!=null?this.fechaEnNombre.estaDentroDeLosLimites(indice):false;
		}
	
		
	}
}
