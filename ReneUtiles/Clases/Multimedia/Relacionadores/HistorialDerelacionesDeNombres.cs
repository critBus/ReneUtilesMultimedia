/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 3/8/2022
 * Hora: 14:23
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ReneUtiles.Clases.Multimedia;
using ReneUtiles.Clases.Multimedia.Relacionadores.Saltos;
using ReneUtiles.Clases;
namespace ReneUtiles.Clases.Multimedia.Relacionadores
{
	/// <summary>
	/// Description of HistorialDerelacionesDeNombres.
	/// </summary>
	public class HistorialDerelacionesDeNombres
	{
		private Dictionary<string,bool> relaciones;
		private RelacionadorDeNombresClaveSeries rel;
		public HistorialDerelacionesDeNombres(RelacionadorDeNombresClaveSeries rel)
		{
			this.rel=rel;
			this.relaciones=new Dictionary<string, bool>();
		}
		
		public bool estanRelacionados(string a,string b){
			string claveDeRelacion=a+"&"+b;
			string claveDeRelacionInversa=b+"&"+a;
			if(this.relaciones.ContainsKey(claveDeRelacion)){
				return relaciones[claveDeRelacion];
			}
			bool seRelacionan=this.rel.estanRelacionados(a,b);
			relaciones.Add(claveDeRelacion,seRelacionan);
			relaciones.Add(claveDeRelacionInversa,seRelacionan);
			return seRelacionan;
		}
	}
}
