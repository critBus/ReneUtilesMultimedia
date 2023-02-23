/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 10/8/2022
 * Hora: 09:14
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
using ReneUtiles.Clases.Multimedia.Series.Representaciones;
//using ReneUtiles.Clases.Multimedia.Series.Procesadores.Ignorar;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Conjuntos;
using ReneUtiles.Clases.Multimedia.Series.Procesadores;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores;
using ReneUtiles.Clases.Multimedia.Series.Representaciones.Temporadas;
using ReneUtiles.Clases.Multimedia.Series.Representaciones.Capitulos;
using ReneUtiles.Clases.Multimedia.Series.Representaciones.Series;
using ReneUtiles.Clases.Multimedia.Relacionadores;
//using System.IO;
using Delimon.Win32.IO;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos;
namespace ReneUtiles.Clases.Multimedia.Series.Recorredores
{
	/// <summary>
	/// Description of RecorredorDeTXT.
	/// </summary>
	public class RecorredorDeTXT:RecorredorDeElementoDeSerie
	{
		public ConjuntoDeSeries series;
		public Predicate<string> usarLinea;
		public RecorredorDeTXT(
			ContextoDeConjuntoDeSeries contextoDeConjunto,
			ProcesadorDeSeries procesador, 
			//ProcesadorDeRelacionesDeNombresClaveSeries proR,
			DatosDePosicionDeRecorridoDeSeries dpr,
			ConjuntoDeSeries series
		)
			: base(contextoDeConjunto, dpr, procesador)
		{
			this.series = series;
			this.usarLinea=ln=>true;
		}
		
		public string eliminarSaltosTrim(string a)
		{
			int indiceInicial = -1;
			for (int i = 0; i < a.Length; i++) {
				char c = a.ElementAt(i);
				if (!esCharSeparacion(c)) {
					indiceInicial = i;
					break;
				}
			}
			string r = "";
			if (indiceInicial != -1) {
				//r=a.Substring(indiceInicial);
				int indiceFinal = -1;
				for (int i = a.Length - 1; i > indiceInicial; i--) {
					char c = a.ElementAt(i);
					if (!esCharSeparacion(c)) {
						indiceFinal = i;
						break;
					}
				}
				if (indiceFinal == -1) {
					r = a.ElementAt(indiceInicial).ToString();
				} else {
					r = subs(a, indiceInicial, indiceFinal+1);
				}
			}
			
			return r;
		}
		public bool esCharSeparacion(char c)
		{
			return !(Char.IsLetterOrDigit(c));
		}
		public void recorrer()
		{
			int indiceError=0;
			FileInfo f = (FileInfo)this.dpr.contexto.F;
			if (Archivos.esTXT(f)) {
				Archivos.recorrerTXT(f, (ls, indice) => {
					if (usarLinea(ls)) {
						string li = ls;
                        
						
						foreach (string de in this.procesador.re.cf.DetencionesAbsolutas) {
							int indiceDeDetencionAbsoluta = li.IndexOf(de);
							if (indiceDeDetencionAbsoluta != -1) {
								li = subs(li, 0, indiceDeDetencionAbsoluta);
							}
						}
						li=Archivos.eliminarCarateresNoPermitidos(li);
						li = eliminarSaltosTrim(li);
						
						if (li.Length > 0) {
							FileInfo fActual = Archivos.renombrarStr(f, li);
					
						
							ContextoDeSerie ctx = new ContextoDeSerie();
							ctx.Url = fActual.ToString();
						
							ctx.EsVideo = UtilesVideos.esVideo(fActual);
							//cxs.IndiceExtencion = fActual.Name.LastIndexOf(".");
						
							ctx.EsCarpeta = false;
							ctx.EsArchivo = false;
							ctx.EsSoloNombre = true;
							
							
							if (ctx.EsVideo) {
								//cwl("fue video y va a recortar");
								//li = Archivos.getNombre(fActual);
								//cwl("esto quedo li="+li);
							}else{
								if(this.procesador.esNombreNormal(f.Name)){
									return;
								}
							}
							
							
							
							
							//--------------------------------
							string separador = procesador.re.cf.separadorDeNombresDeSerieEquivalentes;
							string nombreC = li;
							if (nombreC.Contains(separador)) {
								string[] nombres = split(nombreC, separador);

								List<DatosDeNombreSerie> ldn2 = new List<DatosDeNombreSerie>();
								List<KeySerie> lk = new List<KeySerie>();
								foreach (string n in nombres) {
									string n2 = n.Trim();
									if (n2.Length > 0) {
										ProcesadorDeNombreDeSerie prInt = this.procesador.getProcesadorDeSerie(
											                                  contextoDeConjunto: this.contextoDeConjunto
										, contexto: ctx
										, nombre: n2 
										                                  );
										DatosDeNombreSerie dnI = prInt.crearDatosDeNombre(detenerSiEncuentraPatronesAlFinal: false);
										TipoDeNombreDeSerie? tt = dnI.getTipoDeNombre();
										lk.Add(new KeySerie(
											Nombre: dnI.NombreAdaptado
										, Clave: dnI.Clave
										, TipoDeSerie: tt != null ? (TipoDeNombreDeSerie)tt : TipoDeNombreDeSerie.DESCONOCIDO
										));
										ldn2.Add(dnI);
									}
								}
								Serie s2 = this.series.getSerieYCrearSiNoExiste(lk, ctx.Url);
				
								
								return;
							}
			
							//--------------------------------
							
							
							
							
							
							//+++++++++++++++++++====
							ProcesadorDeNombreDeSerie pr = this.procesador.getProcesadorDeSerie(
								                               contextoDeConjunto: this.contextoDeConjunto
							, contexto: ctx
							, nombre: li 
							                               );
							List<DatosDeNombreSerie> ldn = new List<DatosDeNombreSerie>();

                            //if (li == "86")
                            //{
                            //    cwl("algo");
                            //}

                            DatosDeNombreSerie dn = pr.crearDatosDeNombre(detenerSiEncuentraPatronesAlFinal: false);
                            if (dn!=null) {
                                ldn.Add(dn);
                            }
							
							
							string claveK=dn.Clave;
							string nombreK=dn.NombreAdaptado;
							 
							
							TipoDeNombreDeSerie? tn=dn.getTipoDeNombre();
                            if (claveK==null&& nombreK==null&&esNumero(li.Trim())) {
                                dn.Clave = dn.NombreAdaptado = claveK = nombreK = li.Trim();
                                tn = TipoDeNombreDeSerie.SOLO_UN_NUMERO;

                                dn.datosDelFinal = null;
                                dn.datosDelPrincipio = null;
                                dn.setEsSoloNumero(true);
                                dn.setTipoDeNombre(tn);




                            }
							
//							cwl("fActual="+fActual);
//							cwl("claveK="+claveK);
//							cwl("nombreK="+nombreK);
//							cwl("tn="+tn);
//							cwl("indiceError="+indiceError++);
//							if(indiceError==20){
//								cwl();
//							}
							Serie s = series.getSerieYCrearSiNoExiste(claveK,
							                                          nombreK,
							                                          tn);
							if (dn.esContenedorDeTemporada()) {
								TemporadaDeSerie t= s.getYCrearTemporadaSiNoExiste(dn.getTemporada());
								DatosDeFuente df = new DatosDeFuente();
								df.Ldns = new List<DatosDeNombreSerie>(ldn);
												
								df.Ctx = ctx;
								t.Fuentes.addFuente(df); 
								
								
								
							} else {
								if (!dn.isEmpty()) {
									//TemporadaDeSerie t=new TemporadaDeSerie(s,dn.getTemporada());
							TemporadaDeSerie te= s.getYCrearTemporadaSiNoExiste(dn.getTemporada());
									RepresentacionDeCapitulo cr = null;
									bool esOva = dn.esOva();
									if (dn.esConjuntoDeCapitulos()) {
										CapituloDeSerieMultiples cm = null;
										if (esOva) {
											cm = new CapituloDeSerieMultiplesOva(te);
										} else {
											cm = new CapituloDeSerieMultiples(te);
										}
										cr = cm;
										cm.NumeroCapituloFinal = dn.getCapituloFinal();
										cm.NumeroCapituloInicial = dn.getCapituloInicial();
									} else {
										CapituloDeSerie cs = null;
										if (esOva) {
											cs = new CapituloDeSerieOva(te);
										} else {
											cs = new CapituloDeSerie(te);
										}
										cs.NumeroDeCapitulo = dn.getCapitulo();
										cr = cs;
									}
									//ConjuntoDeFuentes cf = new ConjuntoDeFuentes();
									DatosDeArchivoFisico df = new DatosDeArchivoFisico();
									df.addDatosNumericosDeNombreDeSerie(dn);
									df.Ctx = ctx;
									TipoDeNombreDeSerie? t = dn.getTipoDeNombre();
									df.addKeySerie(new KeySerie(
										Nombre: dn.NombreAdaptado
					, Clave: dn.Clave
					, TipoDeSerie: t != null ? (TipoDeNombreDeSerie)t : TipoDeNombreDeSerie.DESCONOCIDO
									));

							
							
							
							
									
							
							
									cr.Fuentes.addFuente(df);// = cf;
									s.addCapitulo(dn.getTemporada(), cr);
							
									
							
								}//fin del if si esEmpty
								else if (!ctx.EsVideo) {
									DatosDeFuente df = new DatosDeFuente();
									df.Ldns = new List<DatosDeNombreSerie>(ldn);
												
									df.Ctx = ctx;
									s.Fuentes.addFuente(df); 
								}
							}
							
							//+++++++++++++++++++++++++++++
							
							
						}
						
						
						
						
						
						
					}
					
								
				});
			}
		}
	}
}
