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
	public class HistorialDerelacionesDeNombres:ConsolaBasica
	{
		private Dictionary<string, DatosDeRelacionDeSeries> relaciones;
		private RelacionadorDeNombresClave rel;
		public HistorialDerelacionesDeNombres(RelacionadorDeNombresClave rel)
		{
			this.rel=rel;
			this.relaciones=new Dictionary<string, DatosDeRelacionDeSeries>();
		}
		
		public DatosDeRelacionDeSeries estanRelacionados(string a,string b, EventosDeRelacionadorDeSerie eventos)
        {
			string claveDeRelacion=a+"&"+b;
			string claveDeRelacionInversa=b+"&"+a;
			if(this.relaciones.ContainsKey(claveDeRelacion)){
                DatosDeRelacionDeSeries r = relaciones[claveDeRelacion];
                return eventos.alYaExistirRelacionPrevia(r);
                 
			}
            if (this.relaciones.ContainsKey(claveDeRelacionInversa))
            {
                DatosDeRelacionDeSeries r = relaciones[claveDeRelacionInversa];
                return eventos.alYaExistirRelacionPrevia(r);
            }
            this.rel.eventos = eventos;
            DatosDeRelacionDeSeries respuestaARelacion =this.rel.estanRelacionados(a,b);
            //try {
                relaciones.Add(claveDeRelacion, respuestaARelacion);
                relaciones.Add(claveDeRelacionInversa, respuestaARelacion);
            //} catch (Exception ex) {
            //    cwl("dio error al intentar agregar "+a+" "+b);
            //    cwl("en historial de relaciones de nombres de series");
            //}
			
			return respuestaARelacion;
		}
	}
}
