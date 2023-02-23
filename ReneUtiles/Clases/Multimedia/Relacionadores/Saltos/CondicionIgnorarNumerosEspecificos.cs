/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 4/8/2022
 * Hora: 12:24
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace ReneUtiles.Clases.Multimedia.Relacionadores.Saltos
{
	/// <summary>
	/// Description of CondicionIgnorarNumerosEspecificos.
	/// </summary>
	public class CondicionIgnorarNumerosEspecificos
	{
		public int []Numeros;
		public string []Caracteres;
		public bool aceptarSeparacionesEntreLosElementos;
		public CondicionIgnorarNumerosEspecificos(bool aceptarSeparacionesEntreLosElementos,int []Numeros,params string []Caracteres)
		{
			this.Numeros=Numeros;
			this.Caracteres=Caracteres;
			this.aceptarSeparacionesEntreLosElementos=aceptarSeparacionesEntreLosElementos;
		}
		public CondicionIgnorarNumerosEspecificos(int []Numeros,params string []Caracteres)
			:this(true,Numeros,Caracteres)
		{
		
		}
		
	}
}
