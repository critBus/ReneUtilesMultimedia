/*
 * Created by SharpDevelop.
 * User: Rene
 * Date: 26/9/2021
 * Time: 16:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;

using ReneUtiles.Clases.Multimedia;
using ReneUtiles.Clases.Multimedia.Relacionadores.Saltos;
using ReneUtiles.Clases;

namespace ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos   
{
	/// <summary>
	/// Description of DatosDeNombreCapitulosDelPrincipio.
	/// </summary>
	public class DatosDeNombreCapituloDelPrincipio:DatosDeNombreCapitulo  
	{
		
		int indiceDeInicioDespuesDeLosNumeros;
		
		public DatosDeNombreCapituloDelFinal DatosDelFinal{get;set;}
		
		
		public DatosDeNombreCapituloDelPrincipio()
		{
			inicializar();
			this.indiceDeInicioDespuesDeLosNumeros=-1;
		}
		
		public void iniT(DatosDeNombreCapituloDelPrincipio d){
			base.iniT(d);
//			inicializar(
//				 d.temporada
//				,d.capitulo
//				
//				,d.esSoloNumeros
//				,d.esConjuntoDeCapitulos
//				,d.capituloInicial
//				,d.capituloFinal
//				,d.capituloInicial_LengStr
//				,d.capituloFinal_LengStr
//				,d.temporada_LengStr
//				,d.capitulo_LengStr);
			this.indiceDeInicioDespuesDeLosNumeros=d.indiceDeInicioDespuesDeLosNumeros;
		}
		
		
		
		
		public int IndiceDeInicioDespuesDeLosNumeros{
			get{return this.indiceDeInicioDespuesDeLosNumeros;}
			set{this.indiceDeInicioDespuesDeLosNumeros=value;}
		}
		
	}
}
