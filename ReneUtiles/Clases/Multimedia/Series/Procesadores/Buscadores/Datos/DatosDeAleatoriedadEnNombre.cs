/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 7/8/2022
 * Hora: 15:47
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReneUtiles.Clases;
using ReneUtiles.Clases.ExprecionesRegulares;
//using  ReneUtiles.Clases.Multimedia.Series.Procesadores.Ignorar; 
namespace ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos
{
	
	/// <summary>
	/// Description of DatosDeAleatoriedadEnNombre.
	/// </summary>
	public class DatosDeAleatoriedadEnNombre:ExprecionesRegularesBasico
	{

		public int indiceInicial;
		public int indiceFinal;
		public int indiceAContinuacion;
		
		public DatosDeAleatoriedadEnNombre()
		{
		}
		
		public bool estaDentroDeLosLimites(int indice)
		{
			return indice>=this.indiceInicial&&indice<=this.indiceAContinuacion;
		}
		
		public DatosDeIgnorarNumero estaDentroDeLosLimites_DatosDeIgnorarNumero(int indice){
			
			if( indice>=indiceInicial&&indice<indiceAContinuacion){
				DatosDeIgnorarNumero d=new DatosDeIgnorarNumero(indiceAContinuacion);
				d.EsAleaterizacion=true;
				return d;
			}
			return null;
		}
		
	}
}
