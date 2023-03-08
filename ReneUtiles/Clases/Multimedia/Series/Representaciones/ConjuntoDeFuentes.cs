/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 31/7/2022
 * Hora: 21:35
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
	/// Description of ConjuntoDeFuentes.
	/// </summary>
	public class ConjuntoDeFuentes
	{
		public HashSet<DatosDeFuente> datosDefuentes;
		public ConjuntoDeFuentes()
		{
			this.datosDefuentes=DatosDeFuente.getNewHashSet_DatosDeFuente();//=new HashSet<DatosDeFuente>();
		}
		public HashSet<DatosDeFuente> getDatosDefuentes(){
			return this.datosDefuentes;
		}
		
		
		public void addFuente(DatosDeFuente df){
			this.datosDefuentes.Add(df);
		}
		public HashSet<KeySerie> getKeysDeSerie(){
			HashSet<KeySerie> r=KeySerie.getNewHashSet_KeySerie();
			foreach (DatosDeFuente d in datosDefuentes) {
				
				r=KeySerie.getNewHashSet_KeySerie(r.Concat(d.getKeysDeSerie()));
			}
			return r;
		}
		
		public Dictionary<KeySerie,HashSet<string>> getKeysDeSerieYUrls(){
			Dictionary<KeySerie,HashSet<string>> r=KeySerie.getNewDictionary_KeySerie<HashSet<string>>();
			foreach (DatosDeFuente d in datosDefuentes) {
				HashSet<KeySerie> lk=d.getKeysDeSerie();
				string url=d.Ctx.Url;
				foreach (KeySerie k in lk) {
					if(r.ContainsKey(k)){
						r[k].Add(url);
					}else{
						HashSet<string> lurls=new HashSet<string>();
						lurls.Add(url);
						r.Add(k,lurls);
					}
				}
				
			}
			return r;
		}
//		public void addFuente_List(ConjuntoDeFuentes cf){
//			addFuente_List(cf.datosDefuentes);
//		}
		public void addFuente_List(HashSet<DatosDeFuente> df){
			foreach (DatosDeFuente d in df) {
				addFuente(d);
			}
			//this.datosDefuentes.AddRange(df);
		}
		public void unirCon(ConjuntoDeFuentes cf){
			foreach (DatosDeFuente df in cf.datosDefuentes) {
				addFuente(df);
			}
		}
		public ConjuntoDeFuentes getCopia(){
			ConjuntoDeFuentes c=new ConjuntoDeFuentes();
			c.datosDefuentes=DatosDeFuente.getNewHashSet_DatosDeFuente(datosDefuentes);
			return c;
		}
		
		public int getCantidadDeCapitulosDeContenedorDeTemporada(){
			int cantidad=-1;
			foreach (DatosDeFuente d in this.datosDefuentes) {
				int c=d.getCantidadDeCapitulos();
				if(c!=-1&&c>cantidad){
					cantidad=c;
				}
			}
			return cantidad;
		}
		
//		public HashSet<string> getClaves(){
//			HashSet<string> r=new HashSet<string>();
//			foreach (DatosDeFuente d in datosDefuentes) {
//				r.Concat(d.getClaves());
//			}
//			return r;
//		}
//		
//		public HashSet<string> getNombresRecortados(){
//			HashSet<string> r=new HashSet<string>();
//			foreach (DatosDeFuente d in datosDefuentes) {
//				r.Concat(d.getNombresRecortados());
//			}
//			return r;
//		
//		}
		 
	}
}
