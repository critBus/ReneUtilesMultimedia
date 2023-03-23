/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 3/8/2022
 * Hora: 14:30
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using ReneUtiles.Clases.Multimedia.Series.Representaciones.Series;
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
			this.historial=new HistorialDerelacionesDeNombres(new RelacionadorDeNombresClave());
		}
		public DatosDeRelacionDeSeries estanRelacionados(string a, DatosDeSerieRelacionada A, string b, DatosDeSerieRelacionada B){
            EventosDeRelacionadorDeSerie eventos = new EventosDeRelacionadorDeSerie();
            eventos.creadorDeDatosDeRelacion = (va, vb, r) => {
                DatosDeRelacionDeSeries dr = new DatosDeRelacionDeSeries(r, va, A, vb,  B);
                return dr;
            };
            DatosDeRelacionDeSeries d = this.historial.estanRelacionados(a, b, eventos);

            return d;//(DatosDeRelacionDeSeries)d;
		}
	}
}
