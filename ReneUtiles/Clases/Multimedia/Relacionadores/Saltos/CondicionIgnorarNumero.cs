/*
 * Created by SharpDevelop.
 * User: Rene
 * Date: 25/9/2021
 * Time: 17:02
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace ReneUtiles.Clases.Multimedia.Relacionadores.Saltos
{
	/// <summary>
	/// Description of CondicionIgnorarNumero.
	/// </summary>
	public class CondicionIgnorarNumero
	{
		//protected bool numeroDelanteDe;
		protected string[] caracteres;
		
//		public CondicionIgnorarNumero(bool numeroDelanteDe ,params string[] caracteres)
//		{
//			this.numeroDelanteDe=numeroDelanteDe;
//			this.caracteres=caracteres;
//		}
		public CondicionIgnorarNumero(params string[] caracteres)
		{
			//this.numeroDelanteDe=numeroDelanteDe;
			for (int i = 0; i < caracteres.Length; i++) {
				caracteres[i]=Utiles.arreglarPalabra(caracteres[i]);
			}
			this.caracteres=caracteres;
		}
		
		public string[] Caracteres{
			get{return this.caracteres;}
		}
	}
}
