/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 08/15/2022
 * Hora: 14:07
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReneUtiles.Clases;
//using ReneUtiles.Clases.Multimedia.Series.Procesadores.Ignorar;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos;
using ReneUtiles.Clases.ExprecionesRegulares;

namespace ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos
{
	/// <summary>
	/// Description of DatosDeEtiquetaEnNombre.
	/// </summary>
	public class DatosDeEtiquetaEnNombre:ExprecionesRegularesBasico
	{

		public int indiceInicial;
		public int indiceFinal;
		public int indiceAContinuacion;
		public string etiqueta;
		public TipoDeEtiquetaDeSerie tipoDeEtiqueta;
		public DatosDeEtiquetaEnNombre()
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
