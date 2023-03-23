/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 2/8/2022
 * Hora: 16:45
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using ReneUtiles.Clases.Multimedia.Series.Representaciones.Temporadas;
//using System.Collections;
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
using ReneUtiles.Clases.Multimedia.Series.Representaciones;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos;
namespace ReneUtiles.Clases.Multimedia.Series.Representaciones.Capitulos
{
	/// <summary>
	/// Description of CapituloCarpeta.
	/// </summary>
	public abstract class RepresentacionDeCapitulo:ConRepresentacionDeFuentes,IComparer<RepresentacionDeCapitulo>,IComparable<RepresentacionDeCapitulo>
	{
		public TemporadaDeSerie temporada;
		
		public RepresentacionDeCapitulo(TemporadaDeSerie t):base(){
			this.temporada=t;
		}
		
		public int getCapitulo0(){
			return this is CapituloDeSerie?((CapituloDeSerie)this).NumeroDeCapitulo:((CapituloDeSerieMultiples)this).NumeroCapituloInicial;
		}
		
		public int Compare(RepresentacionDeCapitulo x, RepresentacionDeCapitulo y){
			if(x is CapituloDeSerie || y is CapituloDeSerie){
				
				int cIx=x.getCapitulo0();//x is CapituloDeSerie?((CapituloDeSerie)x).NumeroDeCapitulo:((CapituloDeSerieMultiples)x).NumeroCapituloInicial;
				int cIy=y.getCapitulo0();//x is CapituloDeSerie?((CapituloDeSerie)y).NumeroDeCapitulo:((CapituloDeSerieMultiples)y).NumeroCapituloInicial;
				return cIx.CompareTo(cIy);
			}else {
				CapituloDeSerieMultiples cmx=((CapituloDeSerieMultiples)x);
				CapituloDeSerieMultiples cmy=((CapituloDeSerieMultiples)y);
				int c0=cmx.NumeroCapituloInicial.CompareTo(cmy.NumeroCapituloFinal);
				return c0==0?cmx.NumeroCapituloFinal.CompareTo(cmy.NumeroCapituloFinal):c0;
				
			}
			
		}
		public int CompareTo(RepresentacionDeCapitulo value){
			return Compare(this,value);
		}
		public abstract  RepresentacionDeCapitulo getCopia(TemporadaDeSerie t);
//		public bool esUnaCarpeta;
//		public bool tieneVideos;
//		public bool tieneSubtitulos;
		
	}
}
