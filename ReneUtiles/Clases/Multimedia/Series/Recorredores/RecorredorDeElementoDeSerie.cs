/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 08/01/2022
 * Hora: 14:17
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using ReneUtiles.Clases;
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
namespace ReneUtiles.Clases.Multimedia.Series.Recorredores
{
	/// <summary>
	/// Description of RecorredorDeElementoDeSerie.
	/// </summary>
	public class RecorredorDeElementoDeSerie:ExprecionesRegularesBasico
	{
		public ProcesadorDeSeries procesador;
		public ContextoDeConjuntoDeSeries contextoDeConjunto;
		//public ConfiguracionDeSeries cf;
		
		public DatosDePosicionDeRecorridoDeSeries dpr;
		
		//public DatosDePosicionDeRecorridoDeSeries D_Parent;
		
		public RecorredorDeElementoDeSerie(
			ContextoDeConjuntoDeSeries contextoDeConjunto,
			//ConfiguracionDeSeries cf,
			DatosDePosicionDeRecorridoDeSeries dpr,
			ProcesadorDeSeries procesador
			//DatosDePosicionDeRecorridoDeSeries d_Parent
		)
		{
			this.contextoDeConjunto = contextoDeConjunto;
			//this.cf = cf;
			this.dpr = dpr;
			this.procesador=procesador;
			//this.D_Parent = d_Parent;
		}
	}
}
