/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 7/8/2022
 * Hora: 15:52
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using ReneUtiles.Clases.Multimedia;
using ReneUtiles.Clases.Multimedia.Relacionadores.Saltos;
using ReneUtiles.Clases.Multimedia.Series.Contextos;
using ReneUtiles.Clases.Multimedia.Series;
using ReneUtiles.Clases;
using System.Text.RegularExpressions;
using ReneUtiles;
using ReneUtiles.Clases.Basicos.String;
#pragma warning disable CS0105 // The using directive for 'ReneUtiles.Clases.Multimedia.Series' appeared previously in this namespace
using ReneUtiles.Clases.Multimedia.Series;
#pragma warning restore CS0105 // The using directive for 'ReneUtiles.Clases.Multimedia.Series' appeared previously in this namespace
//using ReneUtiles.Clases.Multimedia.Series.Procesadores.Ignorar;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Conjuntos;
using ReneUtiles.Clases.Multimedia.Series.Procesadores;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos;
using ReneUtiles.Clases.ExprecionesRegulares;

namespace ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores
{
	/// <summary>
	/// Description of BuscadorDeAleatoiredadesEnNombre.
	/// </summary>
	public class BuscadorDeAleatoriedadesEnNombre:BuscadorDeDatosEnNombre
	{
		public List<DatosDeAleatoriedadEnNombre> aleatoriedadesEnNombre; 
		public MatchCollection mc;
		public BuscadorDeAleatoriedadesEnNombre(ProcesadorDeNombreDeSerie pr)
			: base(pr)
		{
			this.aleatoriedadesEnNombre = new List<DatosDeAleatoriedadEnNombre>();
		}
		
		public List<DatosDeAleatoriedadEnNombre> buscarAleatoriedades(int i0)
		{
			if (seBuscoCon(i0)) {
				return this.aleatoriedadesEnNombre;
			}
			this.seBusco = true;
			this.I0 = i0;
			
			this.mc = getRe().Re_Posible_Aleatoriedad.SSfreSfS.Matches(this.nombre, i0);
			foreach (Match m in mc) {
				Group gA = getRe().getGrupoAleatoriedad(m);
				if (gA.Success) {
					string a=gA.ToString();
					if (Utiles.esAleaterizacion(a)) {
					
						DatosDeAleatoriedadEnNombre d = new DatosDeAleatoriedadEnNombre();
						d.indiceInicial=gA.Index;
						d.indiceFinal=gA.Index+gA.Length;
						d.indiceAContinuacion=m.Index+m.Length;
						this.aleatoriedadesEnNombre.Add(d);
					}
				}
				
				
			}
			
			return this.aleatoriedadesEnNombre;
		}
		
		public bool estaDentroDeAleatoriedad(int indice){
			
			foreach (DatosDeAleatoriedadEnNombre d in this.aleatoriedadesEnNombre) {
				if(d!=null&&d.estaDentroDeLosLimites(indice)){
					return true;
				}
			}
			return false;
			//return this.fechaEnNombre!=null?this.fechaEnNombre.estaDentroDeLosLimites(indice):false;
		}
		
		public DatosDeIgnorarNumero estaDentroDeAleatoriedad_DatosDeIgnorarNumero(int indice){
			
			foreach (DatosDeAleatoriedadEnNombre d in this.aleatoriedadesEnNombre) {
				DatosDeIgnorarNumero dd=null;
				if(d!=null){
					dd=d.estaDentroDeLosLimites_DatosDeIgnorarNumero(indice);
					if(dd!=null){
						return dd;
					}
					
				}
			}
			return null;
			//return this.fechaEnNombre!=null?this.fechaEnNombre.estaDentroDeLosLimites(indice):false;
		}
	
		
	}
}
