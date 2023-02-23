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
		public  RepresentacionDeCapitulo getCopia(TemporadaDeSerie t){
			CapituloDeSerieMultiplesOva c=new CapituloDeSerieMultiplesOva(t);
			c.setCopiaDeDatos_CM(this);
			return c;
		}
	}
}
