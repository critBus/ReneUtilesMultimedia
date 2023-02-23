/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 08/01/2022
 * Hora: 14:01
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
//using System.IO;
using Delimon.Win32.IO;

namespace ReneUtiles.Clases.Multimedia.Series.Recorredores
{
	/// <summary>
	/// Description of ContextoPosicionDeRecorrido.
	/// </summary>
	public class DatosDePosicionDeRecorridoDeSeries
	{
		public ContextoDeSerie contexto;
		public List<DatosDeNombreSerie> ldn;
		//public DatosDeNombreSerie dn;
		
		public DatosDePosicionDeRecorridoDeSeries D_Parent;
		
		public DatosDePosicionDeRecorridoDeSeries(ContextoDeSerie contexto
		                                          ,DatosDeNombreSerie dn=null
		                                          ,DatosDePosicionDeRecorridoDeSeries D_Parent=null)
		{
			this.contexto=contexto;
			this.ldn=new List<DatosDeNombreSerie>();
			if(dn!=null){
				this.ldn.Add(dn);
			}
			//this.dn=dn;
			this.D_Parent=D_Parent;
		}
		public DatosDePosicionDeRecorridoDeSeries(ContextoDeSerie contexto
		                                          ,List<DatosDeNombreSerie> ldn
		                                          ,DatosDePosicionDeRecorridoDeSeries D_Parent=null)
		{
			this.contexto=contexto;
			this.ldn=new List<DatosDeNombreSerie>(ldn);
			
			
			//this.dn=dn;
			this.D_Parent=D_Parent;
		}
	}
}
