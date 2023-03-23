/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 2/8/2022
 * Hora: 18:53
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using ReneUtiles.Clases.Multimedia.Series.Representaciones.Temporadas;
namespace ReneUtiles.Clases.Multimedia.Series.Representaciones.Capitulos
{
	/// <summary>
	/// Description of CapituloDeSerieMultiplesOva.
	/// </summary>
	public class CapituloDeSerieMultiplesOva:CapituloDeSerieMultiples
	{
		public CapituloDeSerieMultiplesOva(TemporadaDeSerie t):base(t)
		{
		}
#pragma warning disable CS0114 // 'CapituloDeSerieMultiplesOva.getCopia(TemporadaDeSerie)' hides inherited member 'CapituloDeSerieMultiples.getCopia(TemporadaDeSerie)'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword.
		public  RepresentacionDeCapitulo getCopia(TemporadaDeSerie t){
#pragma warning restore CS0114 // 'CapituloDeSerieMultiplesOva.getCopia(TemporadaDeSerie)' hides inherited member 'CapituloDeSerieMultiples.getCopia(TemporadaDeSerie)'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword.
			CapituloDeSerieMultiplesOva c=new CapituloDeSerieMultiplesOva(t);
			c.setCopiaDeDatos_CM(this);
			return c;
		}
	}
}
