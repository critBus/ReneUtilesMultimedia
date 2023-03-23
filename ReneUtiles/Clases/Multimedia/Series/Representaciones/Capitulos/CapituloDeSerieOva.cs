/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 2/8/2022
 * Hora: 18:52
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using ReneUtiles.Clases.Multimedia.Series.Representaciones.Temporadas;
namespace ReneUtiles.Clases.Multimedia.Series.Representaciones.Capitulos
{
	/// <summary>
	/// Description of CapituloDeSerieOva.
	/// </summary>
	public class CapituloDeSerieOva:CapituloDeSerie
	{
		public CapituloDeSerieOva(TemporadaDeSerie t):base(t)
		{
		}
		
#pragma warning disable CS0114 // 'CapituloDeSerieOva.getCopia(TemporadaDeSerie)' hides inherited member 'CapituloDeSerie.getCopia(TemporadaDeSerie)'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword.
		public  RepresentacionDeCapitulo getCopia(TemporadaDeSerie t){
#pragma warning restore CS0114 // 'CapituloDeSerieOva.getCopia(TemporadaDeSerie)' hides inherited member 'CapituloDeSerie.getCopia(TemporadaDeSerie)'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword.
			CapituloDeSerieOva c=new CapituloDeSerieOva(t);
			c.setCopiaDeDatos_CS(this);
			return c;
		}
	}
}
