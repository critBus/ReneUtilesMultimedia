/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 31/7/2022
 * Hora: 21:36
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
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos;
using ReneUtiles.Clases.Multimedia.Series.Representaciones.Temporadas;
namespace ReneUtiles.Clases.Multimedia.Series.Representaciones.Capitulos 
{
	/// <summary>
	/// Description of CapituloDeSerie.
	/// </summary>
	public class CapituloDeSerie:RepresentacionDeCapitulo 
	{
		public int NumeroDeCapitulo;
		
		
		
		public CapituloDeSerie(TemporadaDeSerie t):base(t)
		{
		}
		protected void setCopiaDeDatos_CS(CapituloDeSerie c){
			setCopiaDeDatos_RF(c);
			this.NumeroDeCapitulo=c.NumeroDeCapitulo;
		}
		public override  RepresentacionDeCapitulo getCopia(TemporadaDeSerie t){
			CapituloDeSerie c=new CapituloDeSerie(t);
			c.setCopiaDeDatos_CS(this);
//			c.setCopiaDeDatos_RF(this);
//			c.NumeroDeCapitulo=this.NumeroDeCapitulo;
			return c;
		}
	}
}
