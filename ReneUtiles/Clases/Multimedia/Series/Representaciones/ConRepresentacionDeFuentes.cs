/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 31/7/2022
 * Hora: 21:41
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
using ReneUtiles.Clases.Multimedia.Series;
//using ReneUtiles.Clases.Multimedia.Series.Procesadores.Ignorar;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Conjuntos;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos;
namespace ReneUtiles.Clases.Multimedia.Series.Representaciones 
{
	/// <summary>
	/// Description of ConRepresentacionDeFuente.
	/// </summary>
	public class ConRepresentacionDeFuentes:ConsolaBasica
	{
		public ConjuntoDeFuentes Fuentes;
		public HashSet<string> Nombres;
		public ConRepresentacionDeFuentes()
		{
			this.Fuentes=new ConjuntoDeFuentes();
			this.Nombres=new HashSet<string>();
		}
		protected virtual void setCopiaDeDatos_RF(ConRepresentacionDeFuentes c){
			this.Fuentes=c.Fuentes.getCopia();
			this.Nombres=new HashSet<string>(c.Nombres);
		}
		
		protected void unirCon_RF(ConRepresentacionDeFuentes c){
			this.Fuentes.unirCon(c.Fuentes);
			foreach (string n in c.Nombres) {
				this.Nombres.Add(n);
			}
		}
		
		public bool tieneFuentes(){
			return this.Fuentes!=null&&this.Fuentes.getDatosDefuentes().Count>0;
		}
	}
}
