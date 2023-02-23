/*
 * Created by SharpDevelop.
 * User: Rene
 * Date: 13/10/2021
 * Time: 15:15
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace ReneUtiles.Clases.Multimedia.Relacionadores.Saltos
{
	/// <summary>
	/// Description of CondicionIgnorarNumeroEspecificoRodeadoPor.
	/// </summary>
	public class CondicionIgnorarNumeroEspecificoRodeadoPor
	{
		public int Numero{get;set;}
		public string []Antes;//{get;set;}
		public string []Despues;//{get;set;}
		public bool aceptarSeparacionesEntreLosElementos;
		public CondicionIgnorarNumeroEspecificoRodeadoPor(bool aceptarSeparacionesEntreLosElementos, string[] antes,int numero,string []despues)
		{
			this.Numero=numero;
			this.Antes=antes;
			this.Despues=despues; 
			this.aceptarSeparacionesEntreLosElementos=aceptarSeparacionesEntreLosElementos;
		}
		public CondicionIgnorarNumeroEspecificoRodeadoPor( string[] antes,int numero,string []despues)
			:this(true,antes,numero,despues)
		{
//			this.Numero=numero;
//			this.Antes=antes;
//			this.Despues=despues; 
		}
	}
}
