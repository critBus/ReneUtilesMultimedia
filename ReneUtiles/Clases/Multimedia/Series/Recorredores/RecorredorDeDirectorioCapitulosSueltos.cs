/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 3/8/2022
 * Hora: 14:13
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
using ReneUtiles.Clases.Multimedia.Series.Representaciones;
//using ReneUtiles.Clases.Multimedia.Series.Procesadores.Ignorar;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Conjuntos;
using ReneUtiles.Clases.Multimedia.Series.Procesadores;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores;
using ReneUtiles.Clases.Multimedia.Series.Representaciones.Temporadas;
using ReneUtiles.Clases.Multimedia.Series.Representaciones.Capitulos;
using ReneUtiles.Clases.Multimedia.Series.Representaciones.Series;
using ReneUtiles.Clases.Multimedia.Relacionadores;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos;
//using System.IO;
using Delimon.Win32.IO;
namespace ReneUtiles.Clases.Multimedia.Series.Recorredores
{
	/// <summary>
	/// Description of RecorredorDeDirectorioCapitulosSueltos.
	/// </summary>
	public class RecorredorDeDirectorioCapitulosSueltos:RecorredorDeElementoDeSerie
	{
		public ConjuntoDeSeries series; 
		public Predicate<DirectoryInfo> usarCarpeta;
		public RecorredorDeDirectorioCapitulosSueltos(
			ContextoDeConjuntoDeSeries contextoDeConjunto,
			ProcesadorDeSeries procesador,
			//ProcesadorDeRelacionesDeNombresClaveSeries proR,
			DatosDePosicionDeRecorridoDeSeries dpr,
			ConjuntoDeSeries series
		)
			: base(contextoDeConjunto, dpr, procesador)
		{
			this.series = series;
			//this.proR = proR;
			
		}
		
		
		
		public void usarCorredorTemorada(
									//ProcesadorDeNombreDeSerie pr
			DatosDeNombreSerie dn
		                          , Serie s
		                          , DatosDePosicionDeRecorridoDeSeries dps
		                          //,ContextoDeSerie ctx
		                          , ContextoDeConjuntoDeSeries contextoDeConjuntoContenedor
		)
		{
		
			RecorredorDeDirectorioTemporadaDeSerie reco = new RecorredorDeDirectorioTemporadaDeSerie(
				                                              contextoDeConjunto: contextoDeConjuntoContenedor
							, procesador: procesador
							, dpr: dps
							, T: s.getYCrearTemporadaSiNoExiste(dn.getTemporada()));
						
			reco.recorrer();
		}
		
		public void usarCorredorSerie(
									//ProcesadorDeNombreDeSerie pr
			DatosDeNombreSerie dn
		                          , Serie s
		                          , DatosDePosicionDeRecorridoDeSeries dps
		                          //,ContextoDeSerie ctx
		                          , ContextoDeConjuntoDeSeries contextoDeConjuntoContenedor
		)
		{
		
			
			
			RecorredorDeDirectorioSerie reco = new RecorredorDeDirectorioSerie(
				                                   contextoDeConjunto: contextoDeConjuntoContenedor
							, procesador: procesador
							, dpr: dps
							, serie: s
			                                   );
						
			reco.recorrer();
		}
		
		public void usarRecorredor(//DirectoryInfo c  pr, dn, s
			ProcesadorDeNombreDeSerie pr
		                           , List<DatosDeNombreSerie> ldn
		                           , Serie s
		                           , ContextoDeSerie ctx
		                          , Action<
		//ProcesadorDeNombreDeSerie
		                          DatosDeNombreSerie
		                          ,Serie
		                          ,DatosDePosicionDeRecorridoDeSeries
		//,ContextoDeSerie
		                          ,ContextoDeConjuntoDeSeries
		                          > mandarAUsarAlRecorredor)
		{

			
			ContextoDeConjuntoDeSeries contextoDeConjuntoContenedor = new ContextoDeConjuntoDeSeries();

			contextoDeConjuntoContenedor.add_caracteristicaDeLosCapitulosAnalizados(
				ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.TODOS_PERTENECEN_A_UNA_MISMA_SERIE_SEGURO
						, ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.DEBERIAN_DE_PERTENECER_A_UNA_MISMA_SERIE
			);
			Dictionary<KeySerie,HashSet<string>> keysDeSerieYUrls = s.getKeysDeSerieYUrls();
			foreach (KeyValuePair<KeySerie,HashSet<string>> par in keysDeSerieYUrls) {
				KeySerie k = par.Key;
				foreach (string  url in par.Value) {
					contextoDeConjuntoContenedor.addKeySerieAlQueDeberianDePertencer(k, url);
				}
			}
						
						
			DatosDePosicionDeRecorridoDeSeries dps = new DatosDePosicionDeRecorridoDeSeries(
				                                         contexto: ctx
															, ldn: ldn
															, D_Parent: this.dpr
			                                         );
			
			
//			DatosDeFuente df = new DatosDeFuente();
//			df.Ldns = new List<DatosDeNombreSerie>(ldn);
//			df.Ctx = dps.contexto;
//				
//							
//							
//			s.Fuentes.addFuente(df); //= cf;
			ConRepresentacionDeFuentes crf=null;
			if(mandarAUsarAlRecorredor==usarCorredorTemorada){
				crf=s.getYCrearTemporadaSiNoExiste(ldn[0].getTemporada());
			}else{
				crf=s;
			}
			
			mandarAUsarAlRecorredor(ldn[0], s, dps, contextoDeConjuntoContenedor);
			
		}
		//			DatosDeNombreSerie dn
		//		                          , Serie s
		//		                          , DatosDePosicionDeRecorridoDeSeries dps
		//		                          , ContextoDeConjuntoDeSeries contextoDeConjuntoContenedor
		
		
		public void usarRecorredorCapitulosSueltos(
			DatosDeNombreSerie dn
          , Serie s
          , DatosDePosicionDeRecorridoDeSeries dps
          , ContextoDeConjuntoDeSeries contextoDeConjuntoContenedor)
		{
			RecorredorDeDirectorioCapitulosSueltos r = new RecorredorDeDirectorioCapitulosSueltos(
				                                           contextoDeConjunto: contextoDeConjuntoContenedor
			, procesador: this.procesador
			, dpr: dps
			, series: this.series
			                                           );
			r.usarCarpeta = this.usarCarpeta;
			r.recorrer();
		}
		public void usarRecorredorConjuntosDeSerie(
			DatosDeNombreSerie dn
          , Serie s
          , DatosDePosicionDeRecorridoDeSeries dps
          , ContextoDeConjuntoDeSeries contextoDeConjuntoContenedor)
		{
			RecorredorConjuntoDeSeries r = new RecorredorConjuntoDeSeries(
				                               contextoDeConjunto: contextoDeConjuntoContenedor
			, procesador: this.procesador
			, dpr: dps
			, series: this.series
			                               );
			r.usarCarpeta = this.usarCarpeta;
			r.recorrer();
		}
		private bool usarRecorredorDeSerNecesario(
			ProcesadorDeNombreDeSerie pr
		                           , List<DatosDeNombreSerie> ldn
		                           , Serie s
		                           , ContextoDeSerie ctx)
		{
//			ConjuntoDeEtiquetasDeSerie c = new ConjuntoDeEtiquetasDeSerie(
//				                              re: procesador.re.Re_EtiquetasDeSerie_Principal_Secundarias
//			, texto: pr.nombre
//			                              );
//			TipoDeRecorredorDeSeries? t = c.getTipoDeRecorredor();
			TipoDeRecorredorDeSeries? t = this.procesador.getTipoDeRecorredor(pr.nombre);
			if (t != null) {
				switch (t) {
					case TipoDeRecorredorDeSeries.ConjuntoDeSeries:
						usarRecorredor(
							pr: pr
						, ldn: ldn
						, s: s
						, ctx: ctx
						, mandarAUsarAlRecorredor: usarRecorredorConjuntosDeSerie
						);
						return true;
					case TipoDeRecorredorDeSeries.CapitulosSueltos:
						usarRecorredor(
							pr: pr
						, ldn: ldn
						, s: s
						, ctx: ctx
						, mandarAUsarAlRecorredor: usarRecorredorCapitulosSueltos
						);
						return true;
				}
			}
			
			
			return false;
		}
		
		
		
		
		
		private void obtenerYUsarDatosDeNombre(
			ContextoDeSerie ctx
			, FileSystemInfo f
			//, Action<ProcesadorDeNombreDeSerie,DatosDeNombreSerie,Serie> siEsContentendorDeTemporada
		, Action<
		ProcesadorDeNombreDeSerie
		,DatosDeNombreSerie
		,Serie
		,DatosDeArchivoFisico
		> alTerminarDeUsarSiEsCapitulo)
		{

			string nombre = f is DirectoryInfo ? f.Name : Archivos.getNombre((FileInfo)f);
			if (ctx.EsCarpeta) {
				if (this.usarCarpeta!=null&&!this.usarCarpeta((DirectoryInfo)f)) {
					return;
				}
			}

            //----------------------------


            string separador = procesador.re.cf.separadorDeNombresDeSerieEquivalentes;
            string nombreC = nombre;
            if (nombreC.Contains(separador))
            {
                string[] nombres = split(nombreC, separador);
                //				for (int i = 0; i < nombres.Length; i++) {
                //					nombres[i] = nombres[i].Trim();
                //				}
                List<DatosDeNombreSerie> ldn2 = new List<DatosDeNombreSerie>();
                List<KeySerie> lk = new List<KeySerie>();
                foreach (string n in nombres)
                {
                    string n2 = n.Trim();
                    if (n2.Length > 0)
                    {
                        ProcesadorDeNombreDeSerie prInt = this.procesador.getProcesadorDeSerie(
                                                              contextoDeConjunto: this.contextoDeConjunto
                            , contexto: ctx
                            , nombre: n2
                                                          );
                        DatosDeNombreSerie dnI = prInt.crearDatosDeNombre(detenerSiEncuentraPatronesAlFinal: false);
                        string nombreAdaptadoT = dnI.NombreAdaptado;

                        TipoDeNombreDeSerie? tt = dnI.getTipoDeNombre();
                        lk.Add(new KeySerie(
                            Nombre: nombreAdaptadoT
                        , Clave: dnI.Clave
                        , TipoDeSerie: tt != null ? (TipoDeNombreDeSerie)tt : TipoDeNombreDeSerie.DESCONOCIDO
                        ));
                        ldn2.Add(dnI);
                    }
                }
                Serie s2 = this.series.getSerieYCrearSiNoExiste(lk, ctx.Url);

                usarRecorredor(pr: null
                               , ldn: ldn2
                               , s: s2
                              , ctx: ctx
                              //,D_Parent:
                              , mandarAUsarAlRecorredor: usarCorredorSerie);
                return;
            }


            //---------------------
            ProcesadorDeNombreDeSerie pr = this.procesador.getProcesadorDeSerie(
				                               contextoDeConjunto: this.contextoDeConjunto
							, contexto: ctx
							, nombre: nombre 
			                               );
			List<DatosDeNombreSerie> ldn = new List<DatosDeNombreSerie>();
			DatosDeNombreSerie dn = pr.crearDatosDeNombre(detenerSiEncuentraPatronesAlFinal: false);
			ldn.Add(dn);
            //			if (dn.isEmpty() && ctx.EsVideo) {
            //				return;
            //			}
            DatosDeSerieRelacionada dr = new DatosDeSerieRelacionada();
            dr.set(dn);
            Serie s = series.getSerieYCrearSiNoExiste(dn.Clave, dn.NombreAdaptado, dn.getTipoDeNombre(),dr);
//			Serie s = series.getSerie(dn.Clave);
//			if (s == null) {
//				s = new Serie();
//			}
//			if (!dn.isEmpty() && !s.tieneNombreYClave()) {
//				s.addNombreYClave(nombre: dn.NombreAdaptado
//								, clave: dn.Clave);
//			}	
			if (dn.esContenedorDeTemporada()) {
				//siEsContentendorDeTemporada(pr, dn, s);
				usarRecorredor(pr: pr
				               
				               , ldn: ldn
				               , s: s
				              , ctx: ctx
				              , mandarAUsarAlRecorredor: usarCorredorTemorada);
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
						cs.NumeroDeCapitulo = dn.getCapituloInicial();
						cr = cs;
					}
					//ConjuntoDeFuentes cf = new ConjuntoDeFuentes();
					DatosDeArchivoFisico df = new DatosDeArchivoFisico();
					//df.Dns = dn;
					df.addDatosNumericosDeNombreDeSerie(dn);
					df.Ctx = ctx;
					TipoDeNombreDeSerie? t = dn.getTipoDeNombre();
					df.addKeySerie(new KeySerie(
						Nombre: dn.NombreAdaptado
					, Clave: dn.Clave
					, TipoDeSerie: t != null ? (TipoDeNombreDeSerie)t : TipoDeNombreDeSerie.DESCONOCIDO
					));
//					df.addNombreYClave(
//						nombre: dn.NombreAdaptado
//								, clave: dn.Clave
//					);
							
							
							
							
					//cf.addFuente(df);
							
							
					cr.Fuentes.addFuente(df);// = cf;
					s.addCapitulo(dn.getTemporada(), cr);
							
					alTerminarDeUsarSiEsCapitulo(pr, dn, s, df);
					//t.addCapitulo(cr);
							
				}//fin del if si esEmpty
						
				else if (!ctx.EsVideo) {
//					if(ctx.EsCarpeta&& usarRecorredorDeSerNecesario(pr: pr
//				               , ldn: ldn
//				               , s: s
//				              , ctx: ctx)){
//						return;
//					}
					if (ctx.EsCarpeta) {
						if (usarRecorredorDeSerNecesario(pr: pr
				               , ldn: ldn
				               , s: s
				              , ctx: ctx)) {
							return;
						}
						if (this.procesador.esNombreNormal(f.Name)) {
							return;
						}
					}					
					usarRecorredor(pr: pr
				               , ldn: ldn
				               , s: s
				              , ctx: ctx
				              , mandarAUsarAlRecorredor: usarCorredorSerie);
				}
				//si no fuese detectada carpeta con # de capitulos
				//es que puede que sea el nombre de una serie con su contenido
				//aqui posiblemente baya
				//el recorredor mas externo 
				//por ahora nada
			}
		}
		
		public void recorrer()
		{
			
			HashSet<string> nombresSubtitulos = new HashSet<string>();
			Dictionary<string,List<DatosDeArchivoFisico>> nombreVideos = new Dictionary<string,List<DatosDeArchivoFisico>>();
            DirectoryInfo carpetaActual = new DirectoryInfo(dpr.contexto.Url);
            Archivos.recorrerCarpeta_BoolEntrarSubCarpeta(
				d: carpetaActual
                , metodoUtilizarCarpeta: (c, profundidad, indice) => {
					
					
				if (profundidad != 0) {
					ContextoDeSerie ctx = new ContextoDeSerie();
					ctx.F = c;
					ctx.Url = c.ToString();
					ctx.Parent = c.Parent;
					ctx.EsArchivo = false;
					ctx.EsCarpeta = true;
					ctx.EsVideo = false;
					ctx.EsSoloNombre = false;
					
					obtenerYUsarDatosDeNombre(ctx: ctx
					                          , f: c
           
					                          , alTerminarDeUsarSiEsCapitulo: (pr, dn, s, df) => {
                                                  df.datosVideosConSubtitulos = UtilesVideos.getDatosVideosConSubtitulosDe(c);
                                              });
					
		
					
					return false;
				}
				return true;
			}
				, metodoUtilizarArchivo: (f, profundidad, indice) => {
				
				if (UtilesVideos.esVideo(f)) {
					ContextoDeSerie ctx = new ContextoDeSerie();
					ctx.F = f;
					ctx.Url = f.ToString();
					ctx.Parent = f.Directory;
					ctx.EsArchivo = true;
					ctx.EsCarpeta = false;
					ctx.EsVideo = true;
					ctx.EsSoloNombre = false;
					
					
					obtenerYUsarDatosDeNombre(ctx: ctx
					                          , f: f
//					                          , siEsContentendorDeTemporada:
//					                          (pr, dn, s) => {
//					}
					                          , alTerminarDeUsarSiEsCapitulo: 
					                          (pr, dn, s, df) => {
						string nombreDelVideo = Archivos.getNombre(f);
						if (!nombreVideos.ContainsKey(nombreDelVideo)) {
							nombreVideos.Add(nombreDelVideo, new List<DatosDeArchivoFisico>());
						}
						nombreVideos[nombreDelVideo].Add(df);
					});
					
				} else {
					if (UtilesVideos.esSubtitulo(f)) {
						nombresSubtitulos.Add(Archivos.getNombre(f));
					}
				}
					
			}
			);

            foreach (string n in nombresSubtitulos)
            {
                if (nombreVideos.ContainsKey(n))
                {
                    foreach (DatosDeArchivoFisico df in nombreVideos[n])
                    {
                        if (df.datosVideosConSubtitulos == null)
                        {
                            df.datosVideosConSubtitulos = new DatosVideosConSubtitulos();
                        }
                        df.datosVideosConSubtitulos.tieneSubtitulos = true;
                        df.datosVideosConSubtitulos.subtitulos.Add(Archivos.unirUrls_File(carpetaActual, n));
                    }
                }
            }
            nombresSubtitulos = null;
            nombreVideos = null;
        }
		//
	}
}



//					ProcesadorDeNombreDeSerie pr = this.procesador.getProcesadorDeSerie(
//						                               contextoDeConjunto: this.contextoDeConjunto
//							, contexto: ctx
//							, nombre: c.Name
//					                               );
//
//					DatosDeNombreSerie dn = pr.crearNombreClave(detenerSiEncuentraPatronesAlFinal: false);
//					Serie s = series.getSerie(dn.Clave);
//					if (s == null) {
//						s = new Serie();
//					}
//
//					if (dn.esContenedorDeTemporada()) {
//
//						ContextoDeConjuntoDeSeries contextoDeConjuntoContenedor = new ContextoDeConjuntoDeSeries();
//						contextoDeConjuntoContenedor.addNombreYClave(
//							nombre: dn.NombreAdaptado
//								, clave: dn.Clave);
//						contextoDeConjuntoContenedor.addClaveALaQueDeberianDePertenecer(dn.Clave);
//						contextoDeConjuntoContenedor.add_caracteristicaDeLosCapitulosAnalizados(
//						ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.TODOS_PERTENECEN_A_UNA_MISMA_SERIE_SEGURO
//						,ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.DEBERIAN_DE_PERTENECER_A_UNA_MISMA_SERIE
//						);
//						TipoDeNombreDeSerie? t=dn.getTipoDeNombre();
//						contextoDeConjuntoContenedor.addEsDeTipo(t);
//						contextoDeConjuntoContenedor.addHayDeTipo(t);
//
//						DatosDePosicionDeRecorridoDeSeries dps = new DatosDePosicionDeRecorridoDeSeries(
//				                                        contexto: ctx
//															, dn: dn
//			                                        );
//						RecorredorDeDirectorioTemporadaDeSerie reco = new RecorredorDeDirectorioTemporadaDeSerie(
//                              contextoDeConjunto: contextoDeConjuntoContenedor
//							, procesador: procesador
//							, dpr: dps
//							, T: s.getYCrearTemporadaSiNoExiste(dn.getTemporada()));
//
//						reco.recorrer();
//					} else {
//
//						if (!dn.isEmpty()) {
//							//TemporadaDeSerie t=new TemporadaDeSerie(s,dn.getTemporada());
//
//							RepresentacionDeCapitulo cr = null;
//							bool esOva = dn.esOva();
//							if (dn.esConjuntoDeCapitulos()) {
//								CapituloDeSerieMultiples cm = null;
//								if (esOva) {
//									cm = new CapituloDeSerieMultiplesOva();
//								} else {
//									cm = new CapituloDeSerieMultiples();
//								}
//								cr = cm;
//								cm.NumeroCapituloFinal = dn.getCapituloFinal();
//								cm.NumeroCapituloInicial = dn.getCapituloInicial();
//							} else {
//								CapituloDeSerie cs = null;
//								if (esOva) {
//									cs = new CapituloDeSerieOva();
//								} else {
//									cs = new CapituloDeSerie();
//								}
//								cs.NumeroDeCapitulo = dn.getCapitulo();
//								cr = cs;
//							}
//							ConjuntoDeFuentes cf = new ConjuntoDeFuentes();
//							DatosDeArchivoFisico df = new DatosDeArchivoFisico();
//							df.Dns = dn;
//							df.Ctx = ctx;
//
//							df.addNombreYClave(
//								nombre: dn.NombreAdaptado
//								, clave: dn.Clave
//							);
//
//							//usarDatosDeArchivoFisico(df);
//
//
//							cf.addFuente(df);
//
//
//							cr.Fuentes = cf;
//							s.addCapitulo(dn.getTemporada(), cr);
//							//t.addCapitulo(cr);
//
//						}//fin del if si esEmpty
//
//
//						//si no fuese detectada carpeta con # de capitulos
//						//es que puede que sea el nombre de una serie con su contenido
//						//aqui posiblemente baya
//						//el recorredor mas externo
//						//por ahora nada
//					}
