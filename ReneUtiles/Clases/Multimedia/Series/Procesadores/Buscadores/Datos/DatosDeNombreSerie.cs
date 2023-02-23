/*
 * Created by SharpDevelop.
 * User: Rene
 * Date: 14/10/2021
 * Time: 17:34
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;

using ReneUtiles.Clases.Multimedia;
using ReneUtiles.Clases.Multimedia.Series;
using ReneUtiles.Clases.Multimedia.Relacionadores.Saltos;
using ReneUtiles.Clases;

namespace ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos
{
	/// <summary>
	/// Description of DatosDeNombreSerie.
	/// </summary>
	public class DatosDeNombreSerie:DatosNumericosDeNombreDeSerie
	{
		//		public DatosDeNombreCapituloDelPrincipio DatosCapituloDelPrincipio;//{get;set;}
		//		public DatosDeNombreCapituloDelFinal DatosCapituloDelFinal;//{get;set;}
		//public int I0{get;set;}
		//public int IFinal{get;set;}
		
		
		public string NombreOriginal;
//{get;set;}
		public string Clave;
		
		//public string NombreRecortado;
		public string NombreAdaptado;
//{get;set;}
		private bool esSoloNumeros;
//{get;set;}
		
		private TipoDeNombreDeSerie? tipoDeNombre;
			
		public DatosDeNombreSerie()
			: base()
		{
			//this.TipoDeNombre=TipoDeNombreDeSerie.DESCONOCIDO;
		}
        public void setEsSoloNumero(bool esSoloNumero) {
            this.esSoloNumeros = esSoloNumero;
        }
		
		public bool EsSoloNumeros {
			//get{ return (DatosCapituloDelPrincipio!=null&&DatosCapituloDelPrincipio.EsSoloNumeros)||(DatosCapituloDelFinal!=null&&DatosCapituloDelFinal.EsSoloNumeros);}
			get{ return (datosDelPrincipio == null&& datosDelFinal == null)?this.esSoloNumeros: ((datosDelPrincipio != null && datosDelPrincipio.EsSoloNumeros) || (datosDelFinal != null && datosDelFinal.EsSoloNumeros)); }
		}
		
		public bool hayClave {
			get{ return Clave != null && !(Clave.Trim().Length == 0); }  //String.IsNullOrWhiteSpace(Clave);}
		}
		
		public  DatosDeNombreCapitulo getDatosDeNombreCapitulo()
		{
			if (datosDelPrincipio != null) {
				return datosDelPrincipio;
			}
			return datosDelFinal;
//			if (DatosCapituloDelPrincipio!=null) {
//				return DatosCapituloDelPrincipio;
//			}
//			return DatosCapituloDelFinal;
			
		}
		public bool EsCapitulo_O_Ova {
            //get{ return (datosDelPrincipio != null && datosDelPrincipio.esCapitulo()) || (datosDelFinal != null && datosDelFinal.esCapitulo()); }
            get { return (datosDelPrincipio != null && (datosDelPrincipio.esCapitulo()|| datosDelPrincipio.esOva())) 
                    || (datosDelFinal != null && (datosDelFinal.esCapitulo()|| datosDelFinal.esOva())); }
        }
 
		public TipoDeNombreDeSerie? getTipoDeNombre()
		{
			return this.tipoDeNombre!=null?this.tipoDeNombre:(this.datosDelFinal!=null&&this.datosDelFinal.TipoDeNombre!=null?this.datosDelFinal.TipoDeNombre:
			                                                  this.datosDelPrincipio!=null&&this.datosDelPrincipio.TipoDeNombre!=null?this.datosDelPrincipio.TipoDeNombre:null);
		}
		public void setTipoDeNombre(TipoDeNombreDeSerie? t){
			this.tipoDeNombre=t;
		}
		
		
	}
}
