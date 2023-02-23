/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 3/8/2022
 * Hora: 14:30
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace ReneUtiles.Clases.Multimedia.Relacionadores
{
	/// <summary>
	/// Description of ProcesadorDeRelacionesDeNombresClaveSeries.
	/// </summary>
	public class ProcesadorDeRelacionesDeNombresClaveSeries
	{	private HistorialDerelacionesDeNombres historial;
		public ProcesadorDeRelacionesDeNombresClaveSeries()
		{
			this.historial=new HistorialDerelacionesDeNombres(new RelacionadorDeNombresClaveSeries());
		}
		public bool estanRelacionados(string a,string b){
			return this.historial.estanRelacionados(a,b);
		}
	}
}
