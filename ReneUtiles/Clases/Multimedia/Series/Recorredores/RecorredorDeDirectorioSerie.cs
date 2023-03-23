/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 7/8/2022
 * Hora: 19:05
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
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos;

//using System.IO;
using Delimon.Win32.IO;

namespace ReneUtiles.Clases.Multimedia.Series.Recorredores
{
	/// <summary>
	/// Description of RecorredorDeDirectorioSerie.
	/// </summary>
	public class RecorredorDeDirectorioSerie:RecorredorDeElementoDeSerie
	{
		Serie serie; 
		//HashSet<KeySerie> keys;
		Dictionary<KeySerie,HashSet<string>> keysDeSerieYUrls;
		//		HashSet<string> nombres;
		//		HashSet<string> claves;
		public RecorredorDeDirectorioSerie(
			ContextoDeConjuntoDeSeries contextoDeConjunto,
			ProcesadorDeSeries procesador,
			DatosDePosicionDeRecorridoDeSeries dpr,
			Serie serie)
			: base(contextoDeConjunto, dpr, procesador)
		{
			this.serie = serie;
			contextoDeConjunto.add_caracteristicaDeLosCapitulosAnalizados(
				ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.TODOS_PERTENECEN_A_UNA_MISMA_SERIE_SEGURO
				, ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.DEBERIAN_DE_PERTENECER_A_UNA_MISMA_SERIE
			);
			//this.keys=serie.getKeysDeSerie();
//			nombres = serie.getNombres();
//			claves = serie.getClaves();
			this.keysDeSerieYUrls = serie.getKeysDeSerieYUrls();
			 
		
		}
        public void usarCorredorTemorada(
                                  //ProcesadorDeNombreDeSerie pr
                                  DatosNumericosDeNombreDeSerie dn
                                  //,Serie s
                                  , DatosDePosicionDeRecorridoDeSeries dps
                                  //,ContextoDeSerie ctx
                                  , ContextoDeConjuntoDeSeries contextoDeConjuntoContenedor
                                 )
        {

            RecorredorDeDirectorioTemporadaDeSerie reco = new RecorredorDeDirectorioTemporadaDeSerie(
                                 contextoDeConjunto: contextoDeConjuntoContenedor
                                , procesador: procesador
                                , dpr: dps
                                , T: this.serie.getYCrearTemporadaSiNoExiste(dn.getTemporada()));

            reco.recorrer();
        }

        public void usarCorredorSerie(
                                  //ProcesadorDeNombreDeSerie pr
                                  DatosNumericosDeNombreDeSerie dn
                                  //,Serie s
                                  , DatosDePosicionDeRecorridoDeSeries dps
                                  //,ContextoDeSerie ctx
                                  , ContextoDeConjuntoDeSeries contextoDeConjuntoContenedor
                                 )
        {

            RecorredorDeDirectorioSerie reco = new RecorredorDeDirectorioSerie(
                                 contextoDeConjunto: contextoDeConjuntoContenedor
                                , procesador: procesador
                                , dpr: dps
                                , serie: this.serie
                            );

            reco.recorrer();
        }

        public void usarRecorredor(//DirectoryInfo c  pr, dn, s
                                    ProcesadorDeNombreDeSerie pr
                                   , List<DatosDeNombreSerie> ldn
                                   //, Serie s
                                   , ContextoDeSerie ctx
                                  , Action<
                                  //ProcesadorDeNombreDeSerie
                                  DatosNumericosDeNombreDeSerie
                                  // ,Serie
                                  , DatosDePosicionDeRecorridoDeSeries
                                  //,ContextoDeSerie
                                  , ContextoDeConjuntoDeSeries
                                  > mandarAUsarAlRecorredor)
        {
            //		
            //			ContextoDeSerie ctx = new ContextoDeSerie();
            //			ctx.F = c;
            //			ctx.Url = c.ToString();
            //			ctx.Parent = c.Parent;
            //			ctx.EsArchivo = false;
            //			ctx.EsCarpeta = true;
            //			ctx.EsVideo = false;
            //			ctx.EsSoloNombre = false;

            ContextoDeConjuntoDeSeries contextoDeConjuntoContenedor = new ContextoDeConjuntoDeSeries();

            contextoDeConjuntoContenedor.add_caracteristicaDeLosCapitulosAnalizados(
                ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.TODOS_PERTENECEN_A_UNA_MISMA_SERIE_SEGURO
                        , ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.DEBERIAN_DE_PERTENECER_A_UNA_MISMA_SERIE
            );
            foreach (KeyValuePair<KeySerie, HashSet<string>> par in this.keysDeSerieYUrls)
            {
                KeySerie k = par.Key;
                foreach (string url in par.Value)
                {
                    contextoDeConjuntoContenedor.addKeySerieAlQueDeberianDePertencer(k, url);
                }
            }


            DatosDePosicionDeRecorridoDeSeries dps = new DatosDePosicionDeRecorridoDeSeries(
                                                            contexto: ctx
                                                            , ldn: ldn
                                                            , D_Parent: this.dpr
                                                        );

            ConRepresentacionDeFuentes crf = null;
            if (mandarAUsarAlRecorredor == usarCorredorTemorada)
            {
                crf = this.serie.getYCrearTemporadaSiNoExiste(ldn[0].getTemporada());
            }
            else
            {
                crf = this.serie;
            }

            DatosDeFuente df = new DatosDeFuente();
            df.Ldns = new List<DatosDeNombreSerie>(ldn);
            df.Ctx = dps.contexto;
            crf.Fuentes.addFuente(df); //= cf;

            mandarAUsarAlRecorredor(ldn[0], dps, contextoDeConjuntoContenedor);

        }


        private void obtenerYUsarDatosDeNombre(
			ContextoDeSerie ctx
			, FileSystemInfo f
			//, Action<ProcesadorDeNombreDeSerie,DatosNumericosDeNombreDeSerie,Serie> siEsContentendorDeTemporada
		, Action<ProcesadorDeNombreDeSerie,DatosNumericosDeNombreDeSerie,Serie,DatosDeArchivoFisico> alTerminarDeUsarSiEsCapitulo)
			//, Action<ProcesadorDeNombreDeSerie,DatosNumericosDeNombreDeSerie,Serie> siEstaVacioYNoEs
		{
			string nombre = f is DirectoryInfo ? f.Name : Archivos.getNombre((FileInfo)f);
			
			ProcesadorDeNombreDeSerie pr = this.procesador.getProcesadorDeSerie(
				                               contextoDeConjunto: this.contextoDeConjunto
							, contexto: ctx
							, nombre: nombre 
			                               );
			List<DatosDeNombreSerie> ldn=new List<DatosDeNombreSerie>();
			DatosDeNombreSerie dn = pr.crearDatosDeNombre(detenerSiEncuentraPatronesAlFinal: false);
			ldn.Add(dn);
			//DatosNumericosDeNombreDeSerie dn = pr.getDatosNumericosDeNombre();
//			if (dn.isEmpty() && ctx.EsVideo) {
//				return;
//			}
			Serie s = this.serie;

			
			if (dn.esContenedorDeTemporada()) {
				usarRecorredor(pr:pr
				               ,ldn:ldn
				              ,ctx:ctx
				              ,mandarAUsarAlRecorredor:usarCorredorTemorada);
				//siEsContentendorDeTemporada(pr, dn, s);
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
					
//					df.addKeySerie(new KeySerie(Nombre:dn.NombreAdaptado
//				                               ,Clave:dn.Clave
//				                               ,TipoDeSerie:tipoDeSerie!=null?tipoDeSerie:TipoDeNombreDeSerie.DESCONOCIDO));
//					df.addNombreYClave(
//						nombre: dn.NombreAdaptado
//								, clave: dn.Clave
//					);
							
							
							
							
					//cf.addFuente(df);
							
							
					//cr.Fuentes = cf;
					cr.Fuentes.addFuente(df);// = cf;
					s.addCapitulo(dn.getTemporada(), cr);
							
					alTerminarDeUsarSiEsCapitulo(pr, dn, s, df);
					//t.addCapitulo(cr);
							
				}//fin del if si esEmpty
				else {
					if(!ctx.EsVideo){
						if (this.procesador.esNombreNormal(f.Name)) {
							return;
						}
						usarRecorredor(pr:pr
				               ,ldn:ldn
				              ,ctx:ctx
				              ,mandarAUsarAlRecorredor:usarCorredorSerie);
					}
					
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
		
		
		
		
	}
}
