/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 08/01/2022
 * Hora: 14:31
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
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos;

//using System.IO;
using Delimon.Win32.IO;
namespace ReneUtiles.Clases.Multimedia.Series.Recorredores
{
	/// <summary>
	/// Description of RecorredorDeDirectorioTemporadaDeSerie.
	/// </summary>
	public class RecorredorDeDirectorioTemporadaDeSerie:RecorredorDeElementoDeSerie
	{
		Dictionary<KeySerie,HashSet<string>> keysDeSerieYUrls;
		public TemporadaDeSerie T;
		public RecorredorDeDirectorioTemporadaDeSerie(
			ContextoDeConjuntoDeSeries contextoDeConjunto,
			ProcesadorDeSeries procesador,
			DatosDePosicionDeRecorridoDeSeries dpr,
			TemporadaDeSerie T)
			: base(contextoDeConjunto, dpr, procesador)
		{ 
			this.T = T;
			this.keysDeSerieYUrls = this.T.serie.getKeysDeSerieYUrls();
			//contextoDeConjunto.TODOS_PERTENECEN_A_UNA_MISMA_SERIE_SEGURO();
			contextoDeConjunto.add_caracteristicaDeLosCapitulosAnalizados(
				ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.TODOS_PERTENECEN_A_UNA_MISMA_SERIE_SEGURO
				,ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.DEBERIAN_DE_PERTENECER_A_UNA_MISMA_SERIE
			);
//			HashSet<string> nombres = T.serie.getNombres();
//			HashSet<string> claves = T.serie.getClaves();
			
//			contextoDeConjunto.addNombreYClave_List(nombre: nombres, clave: claves);
//			contextoDeConjunto.addClaveALaQueDeberianDePertenecer_List(claves); 
			
			
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
            mandarAUsarAlRecorredor(ldn[0], dps, contextoDeConjuntoContenedor);

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
                                , T: this.T);

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
                                , serie: this.T.serie
                            );

            reco.recorrer();
        }


        private bool obtenerYAgregarDatosNumericosDeArchivo(ContextoDeSerie ctx, FileSystemInfo f, Action<DatosDeArchivoFisico> usarDatosDeArchivoFisico)
		{
			//if(f.ToString()==@"C:\_COSAS\temporal\contenidos\series\Series Solas\American Horror Story\American Horror History - [Temp 9] [Caps.09] [720p] [Dual Audio]\American Horror History S09E01 [720p] [Dual Audio].mkv"){
			//	cwl("aqui");
			//}
			string nombre =f is DirectoryInfo ? f.Name : Archivos.getNombre((FileInfo)f);
			ProcesadorDeNombreDeSerie pr = this.procesador.getProcesadorDeSerie(
				                               contextoDeConjunto: this.contextoDeConjunto
							, contexto: ctx
							, nombre: nombre
			                               );
			List<DatosDeNombreSerie> ldn=new List<DatosDeNombreSerie>();
			DatosDeNombreSerie dn=pr.crearDatosDeNombre(false);
			ldn.Add(dn);
			//DatosNumericosDeNombreDeSerie dn = pr.getDatosNumericosDeNombre();
			if (!dn.isEmpty()) {
				RepresentacionDeCapitulo cr = null;
				TemporadaDeSerie te= this.T;
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
				usarDatosDeArchivoFisico(df);
				//df.datosVideosConSubtitulos = UtilesVideos.getDatosVideosConSubtitulosDe(c);
						
//				cf.addFuente(df);
//				cr.Fuentes = cf;
				cr.Fuentes.addFuente(df);// = cf;
				if (esOva) {
					this.T.addCapituloOva(cr);
				} else {
					this.T.addCapitulo(cr);
				}
				
				return true;
			}
			
			else  if(!ctx.EsVideo){
				if (this.procesador.esNombreNormal(f.Name)) {
							return false;
						}
				usarRecorredor(pr:pr
				               ,ldn:ldn
				              ,ctx:ctx
				              ,mandarAUsarAlRecorredor:usarCorredorSerie);
				
			}
			return false;
		}
		
		
		public void recorrer()
		{
			HashSet<string> nombresSubtitulos=new HashSet<string>();
			Dictionary<string,List<DatosDeArchivoFisico>> nombreVideos=new Dictionary<string,List<DatosDeArchivoFisico>>();
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
					
					obtenerYAgregarDatosNumericosDeArchivo(ctx, c, df => {
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
					obtenerYAgregarDatosNumericosDeArchivo(ctx, f, df => {
					    string nombreDelVideo=Archivos.getNombre(f);
						if(!nombreVideos.ContainsKey(nombreDelVideo)){
					 	    nombreVideos.Add(nombreDelVideo,new List<DatosDeArchivoFisico>());
						    }
						 nombreVideos[nombreDelVideo].Add(df);

                       // df.datosVideosConSubtitulos.videos.Add(f);
							
							                                       	
					});
					
						
					}else{
						if(UtilesVideos.esSubtitulo(f)){
							nombresSubtitulos.Add(Archivos.getNombre(f));
						}
					}
					
			}
			);
			
			foreach (string n in nombresSubtitulos) {
				if(nombreVideos.ContainsKey(n)){
					foreach (DatosDeArchivoFisico df in nombreVideos[n]) {
						if(df.datosVideosConSubtitulos==null){
							df.datosVideosConSubtitulos=new DatosVideosConSubtitulos();
						}
						df.datosVideosConSubtitulos.tieneSubtitulos=true;
                        df.datosVideosConSubtitulos.subtitulos.Add(Archivos.unirUrls_File(carpetaActual,n));
					}
				}
			}
			nombresSubtitulos=null;
			nombreVideos=null;
		}
	}
}

	
//					ProcesadorDeNombreDeSerie pr = this.procesador.getProcesadorDeSerie(
//						                               contextoDeConjunto: this.contextoDeConjunto
//							, contexto: ctx
//							, nombre: c.Name
//					                               );
//					DatosNumericosDeNombreDeSerie dn = pr.getDatosNumericosDeNombre();
//					if (dn.isEmpty()) {
//						RepresentacionDeCapitulo cr = null;
//						if (dn.esConjuntoDeCapitulos) {
//							CapituloDeSerieMultiples cm = new CapituloDeSerieMultiples();
//							cr = cm;
//							cm.NumeroCapituloFinal = dn.getCapituloFinal();
//							cm.NumeroCapituloInicial = dn.getCapituloInicial();
//						} else {
//							CapituloDeSerie cs = new CapituloDeSerie();
//							cs.NumeroDeCapitulo = dn.getCapitulo();
//							cr = cs;
//						}
//						ConjuntoDeFuentes cf = new ConjuntoDeFuentes();
//						DatosDeArchivoFisico df = new DatosDeArchivoFisico();
//						df.Dns = dn;
//						df.Ctx = ctx;
//						df.datosVideosConSubtitulos=UtilesVideos.getDatosVideosConSubtitulosDe(c);
//						//df.esUnaCarpeta=true;
//
//						cf.addFuente(df);
//						cr.Fuentes = cf;
//						this.T.addCapitulo(cr);
//
//					}
