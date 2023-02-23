/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 4/8/2022
 * Hora: 12:11
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace ReneUtiles.Clases.Multimedia.Relacionadores.Saltos
{
	/// <summary>
	/// Description of CondicionIgnorarNumerosEspecificosSeparadosPor.
	/// </summary>
	public class CondicionIgnorarNumerosEspecificosSeparadosPor
	{
		public int NumeroInicial;
		public int NumeroFinal;
		public string []Separaciones;
		public bool aceptarSeparacionesEntreLosElementos;
		public CondicionIgnorarNumerosEspecificosSeparadosPor(int numeroInicial
		                                                     ,int numeroFinal
		                                                    ,bool aceptarSeparacionesEntreLosElementos
		                                                    ,params string []separaciones)
		{
			this.NumeroInicial=numeroInicial;
			this.NumeroFinal=numeroFinal;
			this.Separaciones=separaciones;
			this.aceptarSeparacionesEntreLosElementos=aceptarSeparacionesEntreLosElementos;
		}
		public CondicionIgnorarNumerosEspecificosSeparadosPor(int NumeroInicial
		                                                     ,int NumeroFinal
		                                                    ,params string []Separaciones)
			:this(NumeroInicial,NumeroFinal,true,Separaciones)
		{
		}
	}
}
