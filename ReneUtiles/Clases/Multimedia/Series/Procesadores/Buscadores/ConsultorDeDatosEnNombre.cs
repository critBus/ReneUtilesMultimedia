/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 31/7/2022
 * Hora: 14:22
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
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Conjuntos;
//using ReneUtiles.Clases.Multimedia.Series.Procesadores.Ignorar;
using ReneUtiles.Clases.Multimedia.Series.Procesadores;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos;
namespace ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores
{
	/// <summary>
	/// Description of ConsultorDeDatosEnNombre.
	/// </summary>
	public class ConsultorDeDatosEnNombre:ExprecionesRegularesBasico
	{
		protected bool seBusco;
		protected string nombre;
		protected ProcesadorDeNombreDeSerie pr;
		protected ProcesadorDeSeries pro;
		public ConsultorDeDatosEnNombre(ProcesadorDeNombreDeSerie pr,ProcesadorDeSeries pro=null)
		{
			this.pr=pr;
			this.seBusco=false;
			this.nombre=this.pr.nombre;
			this.pro=pro;
		}
		protected DatosDeNombreSerie getDn()
		{
			return this.pr.dn; 
		}
		
		protected ProcesadorDeNombreDeSerie getPr()
		{
			return this.pr; 
		}
		protected ProcesadorDeSeries getPro()
		{
			return this.pro; 
		}
		protected ConfiguracionDeSeries getConf()
		{
			return this.pr.re.cf; 
		}
		protected ContextoDeSerie getCtx()
		{
			return this.pr.contexto;
		}
		protected ContextoDeConjuntoDeSeries getCtxCn()
		{
			return this.pr.contextoDeConjunto;
		}
		
		protected RecursosDePatronesDeSeries getRe()
		{
			return this.pr.re; 
		}
	}
}
