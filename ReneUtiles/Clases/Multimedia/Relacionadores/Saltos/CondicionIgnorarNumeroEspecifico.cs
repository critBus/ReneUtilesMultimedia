/*
 * Created by SharpDevelop.
 * User: Rene
 * Date: 25/9/2021
 * Time: 17:07
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace ReneUtiles.Clases.Multimedia.Relacionadores.Saltos
{
	/// <summary>
	/// Description of CondicionIgnorarNumeroEspecifico.
	/// </summary>
	public class CondicionIgnorarNumeroEspecifico:CondicionIgnorarNumero
	{
		
		
		private int numero;
		
//		public CondicionIgnorarNumeroEspecifico(bool numeroDelanteDe ,int numero,params string[] caracteres):base(numeroDelanteDe ,caracteres)
//		{
//			this.numero=numero;
//		}
		public bool aceptarSeparacionesEntreLosElementos;
		public CondicionIgnorarNumeroEspecifico(bool aceptarSeparacionesEntreLosElementos,int numero,params string[] caracteres):base(caracteres)
		{
			this.numero=numero;
			this.aceptarSeparacionesEntreLosElementos=aceptarSeparacionesEntreLosElementos;
		}
		public CondicionIgnorarNumeroEspecifico(int numero,params string[] caracteres)
			:this(true,numero,caracteres)
		{
			//this.numero=numero;
		}
		public int Numero{
			get{ return this.numero;}
		}
	}
}
