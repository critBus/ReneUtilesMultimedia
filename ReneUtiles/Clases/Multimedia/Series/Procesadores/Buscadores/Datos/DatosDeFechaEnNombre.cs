/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 31/7/2022
 * Hora: 08:42
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReneUtiles.Clases;
//using  ReneUtiles.Clases.Multimedia.Series.Procesadores.Ignorar;
using ReneUtiles.Clases.ExprecionesRegulares;
using ReneUtiles.Clases.ExprecionesRegulares.Fechas;
namespace ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos
{
	/// <summary>
	/// Description of DatosDeFechaEnNombre.
	/// </summary>
	public class DatosDeFechaEnNombre:DatosDeFechaEnStr
	{
		//public class LimitesDeFecha
		//{
		//	public int indiceInicial;
		//	public int indiceFinal;
		//	public int indiceAContinuacion;
		//}
		//private int? año;
		//private int? dia;
		//private int? mes;
		//public int? indiceDeteccionDeAño;
		//public int? indiceDeteccionDeDia;
		//public int? indiceDeteccionDeMes;
		//public string fechaStr;
		//public int? indiceDeteccionDeFecha;
		//private string añoStr;
		//private string diaStr;
		//private string mesStr;
		//public int? indiceAContinuacion;
		
		
		
		//public DatosDeFechaEnNombre()
		//{
		//}
		
		//private int? getIndiceFinal(string a, int? i0)
		//{
		//	return a != null && i0 != null ? i0 + a.Length : null;
		//}
		
		//public LimitesDeFecha getLimitesDeFecha()
		//{
		//	LimitesDeFecha lf = new LimitesDeFecha();
		//	lf.indiceInicial = Utiles.minimo(this.indiceDeteccionDeAño
		//	                              , this.indiceDeteccionDeDia
		//	                             , this.indiceDeteccionDeMes
		//	                            , this.indiceDeteccionDeFecha);
		//	lf.indiceFinal = Utiles.maximo(getIndiceFinal(this.añoStr,this.indiceDeteccionDeAño)
		//	                              , getIndiceFinal(this.diaStr,this.indiceDeteccionDeDia)
		//	                               , getIndiceFinal(this.mesStr,this.indiceDeteccionDeMes)
		//	                               , getIndiceFinal(this.fechaStr,this.indiceDeteccionDeFecha));
		//	lf.indiceAContinuacion=this.indiceAContinuacion??lf.indiceFinal+1;
		//	return lf;
		//}
		//public bool isEmpty()
		//{
		//	return this.año == null && this.dia == null && this.mes == null && fechaStr == null;
		//}
		//public bool estaDentroDeLosLimites(int indice)
		//{
		//	if (isEmpty()) {
		//		return false;
		//	}
		//	LimitesDeFecha lf = getLimitesDeFecha();
		//	return indice>=lf.indiceInicial&&indice<lf.indiceAContinuacion;
			
		//} 
		
		public DatosDeIgnorarNumero estaDentroDeLosLimites_DatosDeIgnorarNumero(int indice){
			if (isEmpty()) {
				return null;
			}
			LimitesDeFecha lf = getLimitesDeFecha();
			if( indice>=lf.indiceInicial&&indice<lf.indiceAContinuacion){
				DatosDeIgnorarNumero d=new DatosDeIgnorarNumero(lf.indiceAContinuacion);
				d.DentroDeFecha=true;
				return d;
			}
			return null;
		}
		
		//public int?  Año {
		//	get{ return this.año; }
		//	set{ this.año = value; }
		//}
		//public int?  Mes {
		//	get{ return this.mes; }
		//	set{ this.mes = value; }
		//}
		//public int?  Dia {
		//	get{ return this.dia; }
		//	set{ this.dia = value; }
		//}
		
		//public string AñoStr {
		//	get{ return this.añoStr; }
		//	set {
		//		this.añoStr = value;
		//		this.año = inT(value); 
		//		//this.capitulo = Math.Abs(value);
		//		//this.capitulo_LengStr = (this.capitulo + "").Length;
		//	}
		//}
		//public string DiaStr {
		//	get{ return this.diaStr; }
		//	set {
		//		this.diaStr = value;
		//		this.dia = inT(value);
		//		//this.capitulo = Math.Abs(value);
		//		//this.capitulo_LengStr = (this.capitulo + "").Length;
		//	}
		//}
		//public string MesStr {
		//	get{ return this.mesStr; }
		//	set {
		//		this.mesStr = value;
		//		this.mes = inT(value);
		//		//this.capitulo = Math.Abs(value);
		//		//this.capitulo_LengStr = (this.capitulo + "").Length;
		//	}
		//}
	}
}
