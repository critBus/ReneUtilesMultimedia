/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 31/7/2022
 * Hora: 21:44
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
using ReneUtiles.Clases.Multimedia.Series.Representaciones;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos;
using ReneUtiles.Clases.Multimedia.Series.Representaciones.Temporadas;
namespace ReneUtiles.Clases.Multimedia.Series.Representaciones.Capitulos
{
	/// <summary>
	/// Description of CapituloDeSerieMultiple.
	/// </summary>
	public class CapituloDeSerieMultiples:RepresentacionDeCapitulo
	{
		public int NumeroCapituloInicial;
		public int NumeroCapituloFinal;
		public CapituloDeSerieMultiples(TemporadaDeSerie t):base(t)
		{
		}
		protected void setCopiaDeDatos_CM(CapituloDeSerieMultiples c){
			setCopiaDeDatos_RF(c);
			this.NumeroCapituloInicial=c.NumeroCapituloInicial;
			this.NumeroCapituloFinal=c.NumeroCapituloFinal;
		}
		public override  RepresentacionDeCapitulo getCopia(TemporadaDeSerie t){
			CapituloDeSerieMultiples c=new CapituloDeSerieMultiples(t);
			c.setCopiaDeDatos_CM(this);
			return c;
		}
//		public  RepresentacionDeCapitulo getCopia(){
//			CapituloDeSerieMultiples c=new CapituloDeSerieMultiples();
//			c.setCopiaDeDatos(this);
//			c.NumeroCapituloInicial=this.NumeroCapituloInicial;
//			c.NumeroCapituloFinal=this.NumeroCapituloFinal;
//			return c;
//		}
	}
}
