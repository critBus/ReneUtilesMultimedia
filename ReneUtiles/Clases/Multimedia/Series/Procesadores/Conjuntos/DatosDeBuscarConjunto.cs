/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 28/7/2022
 * Hora: 17:09
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace ReneUtiles.Clases.Multimedia.Series.Procesadores.Conjuntos
{
	/// <summary>
	/// Description of DatosDeBuscarConjunto.
	/// </summary>
	public class DatosDeBuscarConjunto
	{
		public bool EsConjunto{ get; set; }
			public bool EncontroPatron{ get; set; }
			//public Matchs.DatosDeGetNumeroMatch DatosNumeroFinal{ get; set; }
			public int NumeroInicial{ get; set; }
			public int IndiceNumeroInicial{ get; set; }
			
			public DatosDeBuscarConjunto()
			{
				this.EsConjunto = false;
			}
	}
}
