/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 13/8/2022
 * Hora: 18:59
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
using ReneUtiles.Clases.Multimedia.Series.Representaciones;
//using ReneUtiles.Clases.Multimedia.Series.Procesadores.Ignorar;
//using ReneUtiles.Clases.Multimedia.Series.Procesadores.Conjuntos;
using ReneUtiles.Clases.Multimedia.Series.Procesadores;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores;
using ReneUtiles.Clases.Multimedia.Series.Representaciones.Temporadas;
using ReneUtiles.Clases.Multimedia.Series.Representaciones.Capitulos;
using ReneUtiles.Clases.Multimedia.Series.Representaciones.Series;
using ReneUtiles.Clases.Multimedia.Relacionadores;
using ReneUtiles.Clases.Multimedia.Series.Recorredores;
//using System.IO;
using Delimon.Win32.IO;
namespace ReneUtiles.Clases.Multimedia.Paquetes.Representaciones
{
	/// <summary>
	/// Description of Paquete.
	/// </summary>
	public class Paquete
	{
		public string nombre;
		public DirectoryInfo carpeta;
		public SeriesDelPaquete seriesPersona;
		public SeriesDelPaquete seriesMangas;
		
		
		private ProcesadorDeRelacionesDeNombresClaveSeries proR;
		private ConfiguracionDeSeries cf_series_anime;
		private ConfiguracionDeSeries cf_series_persona;
		
		public Paquete(
			DirectoryInfo carpeta
			,ProcesadorDeRelacionesDeNombresClaveSeries proR
			,ConfiguracionDeSeries cf_series_anime
			,ConfiguracionDeSeries cf_series_persona
		)
		{
			this.carpeta=carpeta;
            if (carpeta!=null) {
                this.nombre = carpeta.Name;
            }
			
			
			//this.cf=cf;
			this.proR=proR;
			
			this.seriesMangas=new SeriesDelPaquete(proR,cf_series_anime);
			this.seriesPersona=new SeriesDelPaquete(proR,cf_series_persona);
		}
	}
}
