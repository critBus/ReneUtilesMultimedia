/*
 * Created by SharpDevelop.
 * User: Rene
 * Date: 15/10/2021
 * Time: 18:39
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Text.RegularExpressions;

using ReneUtiles.Clases;
namespace ReneUtiles.Clases.Multimedia.Relacionadores.Saltos
{
	/// <summary>
	/// Description of CondicionNoSaltarAlPrincipio.
	/// </summary>
	public class CondicionNoSaltarAlPrincipio
	{
		public string[] CoincidenciasParaSalto;//{get;set;}
		public string[] ContinuacionesDelNombre;//{get;set;}
		public string ContinuacionesDelNombre_Patron;//{get;set;}
		public PatronRegex Re_ContinuacionesDelNombre_Patron;
		
		public CondicionNoSaltarAlPrincipio(string[] coincidenciasParaSalto,string[] continuacionesDelNombre)
		{
			int end=coincidenciasParaSalto.Length;
			for (int i = 0; i < end; i++) {
				coincidenciasParaSalto[i]=coincidenciasParaSalto[i].ToLower();
			}
			end=continuacionesDelNombre.Length;
			for (int i = 0; i < end; i++) {
				continuacionesDelNombre[i]=continuacionesDelNombre[i].ToLower();
			}
			this.CoincidenciasParaSalto=coincidenciasParaSalto;
			this.ContinuacionesDelNombre=continuacionesDelNombre;
			ContinuacionesDelNombre_Patron=ConstantesExprecionesRegulares.getPatronPalabrasOR(true, ContinuacionesDelNombre);
			Re_ContinuacionesDelNombre_Patron=new PatronRegex(ContinuacionesDelNombre_Patron);
		}
	}
}
