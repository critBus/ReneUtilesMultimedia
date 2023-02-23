/*
 * Created by SharpDevelop.
 * User: Rene
 * Date: 28/9/2021
 * Time: 19:10
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
//using System.IO;
using Delimon.Win32.IO;

using ReneUtiles;
using ReneUtiles.Clases;

namespace ReneUtiles.Clases.Multimedia.Series.Contextos
{
	/// <summary>
	/// Description of ContextoDeSerie.
	/// </summary>
	public class ContextoDeSerie:ConsolaBasica
	{
		public string Url;//{ get; set; }
		
		public FileSystemInfo F;
		public DirectoryInfo Parent;
		
		public bool EsArchivo;
		public bool EsCarpeta;
		public bool EsSoloNombre;//{ get; set; }
		public bool EsVideo;//{ get; set; }
		public int IndiceConjuntoDeNombres;//{ get; set; }
		public int IndiceExtencion;//{ get; set; }
		
		
		
		
		public ContextoDeSerie(){
			this.EsArchivo=false;
			this.EsCarpeta=false;
			this.EsSoloNombre=false;
			this.EsVideo=false;
			IndiceExtencion=-1;
			IndiceConjuntoDeNombres=-1;
		}
		
		
		
		
		public bool hayDireccion{
			get{return Url!=null&&!isEmptyFull(Url);}
		}
//		public bool EsArchivo{
//			get{return hayDireccion?Archivos.esArchivo(Url):esArchivo;}
//			set{this.esArchivo=value;}
//		}
//		public bool EsCarpeta{
//			get{
//				//cwl("Url="+Url);
//				//cwl("hayDireccion="+hayDireccion);
//				//cwl("Archivos.esCarpeta(Url)="+Archivos.esCarpeta(Url));
//				return hayDireccion?Archivos.esCarpeta(Url):esCarpeta;}
//			set{this.esCarpeta=value;}
//		}
		
		
		
	}
}
