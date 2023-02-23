/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 31/7/2022
 * Hora: 21:25
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
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos;

using System.IO;
namespace ReneUtiles.Clases.Multimedia.Series.Representaciones
{
	/// <summary>
	/// Description of DatosDeArchivoFisico.
	/// </summary>
	public class DatosDeArchivoFisico:DatosDeFuente
	{
		
		//public bool esUnaCarpeta;
		public DatosVideosConSubtitulos datosVideosConSubtitulos;
//		public bool tieneVideos;
//		public bool tieneSubtitulos;
//		public bool tieneSubtitulosTodos;
//		public bool tieneSubtitulosAlMenosUno;
		
//		public FileSystemInfo F;
//		public DirectoryInfo Parent;
		
		public DatosDeArchivoFisico():base()
		{
		}
	}
}
