/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 28/8/2022
 * Hora: 16:59
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
//using ReneUtiles.Clases.Multimedia.Series.Procesadores.Conjuntos;
using ReneUtiles.Clases.Multimedia.Series.Procesadores;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores;
using ReneUtiles.Clases.Multimedia.Series.Representaciones.Temporadas;
using ReneUtiles.Clases.Multimedia.Series.Representaciones.Capitulos;
using ReneUtiles.Clases.Multimedia.Series.Representaciones.Series;
using ReneUtiles.Clases.Multimedia.Relacionadores;
using ReneUtiles.Clases.Multimedia.Series.Recorredores;
//using System.IO;
using ReneUtiles.Clases.Multimedia.Paquetes.Representaciones;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos;
using Delimon.Win32.IO;
namespace ReneUtiles.Clases.Multimedia.Paquetes
{
	/// <summary>
	/// Description of AnalizadorDelPaquete.
	/// </summary>
	public class AnalizadorDelPaquete:ConsolaBasica 
	{
		class DirectoriosPrestablecidos
		{
			public string[] urls;
			public ConjuntoDeEtiquetasDeSerie etiquetas;
			public DirectoriosPrestablecidos(
				string[] urls,
				params TipoDeEtiquetaDeSerie[] etiquetas
			)
			{
				this.urls = urls;
				this.etiquetas = new ConjuntoDeEtiquetasDeSerie(ComparadorTipoDeEtiquetaDeSerie.getNewSortedSet_TipoDeEtiquetaDeSerie(etiquetas));
			}
		}
		
		public DirectoryInfo carpetaPaquete;
		public Paquete paquete;
		
		private HashSet<string> urlsUsadas;
		private HashSet<string> urlsPadres;
		
		private List<DirectoriosPrestablecidos> d_mangas;
		private List<DirectoriosPrestablecidos> d_series;
		//		private List<DirectoriosPrestablecidos> d_mangas_tx;
		//		private List<DirectoriosPrestablecidos> d_mangas_finalizadas;
		//		private List<DirectoriosPrestablecidos> d_series_tx;
		//		private List<DirectoriosPrestablecidos> d_series_finalizadas;
		
		//		private ProcesadorDeRelacionesDeNombresClaveSeries proR;
		//		private ConfiguracionDeSeries cf;
		//		private RecursosDePatronesDeSeries re_series_mangas;
		//		private RecursosDePatronesDeSeries re_series_persona;
		private RecursosDePatronesDeSeriesGenerales reg;
		public AnalizadorDelPaquete(
			Paquete p
			, RecursosDePatronesDeSeriesGenerales reg
			
//			DirectoryInfo carpetaPaquete
//		, ProcesadorDeRelacionesDeNombresClaveSeries proR
//			, ConfiguracionDeSeries cf
		)
		{
			this.reg = reg;
			
//			this.re_series_mangas=re_series_mangas;
//			this.re_series_persona=re_series_persona;
//			this.cf=cf;
//			this.proR=proR;
			
			this.carpetaPaquete = p.carpeta;
			this.paquete = p;//new Paquete(carpetaPaquete, proR, cf);
			
			this.d_mangas = new List<DirectoriosPrestablecidos>();
			this.d_series = new List<DirectoriosPrestablecidos>();
			this.urlsUsadas = new HashSet<string>();
			this.urlsPadres = new HashSet<string>();
		}
		
		private string[] getRutas(params string[] urls)
		{
			List<string> newUrls = new List<string>();
			string keyAño = "####";
			for (int i = 0; i < urls.Length; i++) {
				string u = urls[i];
				if (u.Contains(keyAño)) {
					for (int j = 2016; j < 2024; j++) {
						newUrls.Add(u.Replace(keyAño, j.ToString()));
					}
					continue;
				}
				newUrls.Add(u);
			}
			return newUrls.ToArray();
		}
		private DirectoriosPrestablecidos dp(string[] urls,
			params TipoDeEtiquetaDeSerie[] etiquetas)
		{
			DirectoriosPrestablecidos d = new DirectoriosPrestablecidos(urls, etiquetas);
			d_series.Add(d);
			return d;
		}
		
		private DirectoriosPrestablecidos mdp(string[] urls,
			params TipoDeEtiquetaDeSerie[] etiquetas)
		{
			DirectoriosPrestablecidos d = new DirectoriosPrestablecidos(urls, etiquetas);
			d_mangas.Add(d);
			return d;
		}
		
		private void analizarDirectoriosPrestablecidos(SeriesDelPaquete s, List<DirectoriosPrestablecidos> directorios)
		{
			foreach (DirectoriosPrestablecidos d in directorios) {
				analizarDirectoriosSueltos(d.urls, c => s.addDirectorio(d.etiquetas, c));
//				foreach (string url in d.urls) {
//					DirectoryInfo c = Archivos.unirUrls_Direc(carpetaPaquete, url);
//					if (c.Exists) {
//						s.addDirectorio(d.etiquetas, c);
//						this.urlsUsadas.Add(c.ToString());
//						string[] urlsParents = Archivos.getUrlsParents(url);
//						foreach (string urlP in urlsParents) {
//							this.urlsPadres.Add(urlP);
//						}
//					}
//				}
			}
		}
		private void analizarDirectoriosSueltos(string[] urls, Action<DirectoryInfo> alExistir )
		{
			
			foreach (string _url in urls) {
				string url = _url.Replace("/", @"\");
				DirectoryInfo c = Archivos.unirUrls_Direc(carpetaPaquete, url);
				if (c.Exists) {
					//if (alExistir != null) {
					//	alExistir(c);
					//}



                    alExistir?.Invoke(c);

                    agregarUrl_Usada(c.ToString(),url);
//					this.urlsUsadas.Add(c.ToString());
//					string[] urlsParents = Archivos.getUrlsParents(url);
//					foreach (string urlP in urlsParents) {
//						this.urlsPadres.Add(urlP);
//					}
				}
			}
			
		}
		
		private void agregarUrl_Usada(string urlCompleta,string urlRelativa)
		{


			this.urlsUsadas.Add(urlCompleta);
			string[] urlsParents = Archivos.getUrlsParents(urlRelativa);
			foreach (string urlP in urlsParents) {
				this.urlsPadres.Add(urlP);
			}
		}
		private void buscarUrlsDefaults()
		{
			
			//series:-mangas
			DirectoriosPrestablecidos urls_mangas_tx = mdp(getRutas(
				                                           "Animados Mangas [####]"
                                                           , "Animados Mangas [####]/Series Mangas [TX]"
                , "Series Mangas [TX]/Anime Online [Transmision]"
				, "Series Mangas [TX]/Mangas [Transmision]"
			                                           )
			, TipoDeEtiquetaDeSerie.TX
			                                           );
			
			DirectoriosPrestablecidos urls_mangas_tx_clasicas = mdp(getRutas(
				 "Animados Mangas [####]/!! Series Clasicas [x Capitulos]"
				, "Series Mangas [TX]/Series Anime [Clasicas]"
                , "Animados Mangas [####]/Series Mangas [TX] [Clásicas]"
                                                                )
			                                                    , TipoDeEtiquetaDeSerie.TX
			, TipoDeEtiquetaDeSerie.CLASICAS
			
			                                                    );
			DirectoriosPrestablecidos urls_mangas_finalizadas = mdp(getRutas(
				       "Animados Mangas [####]/Series Mangas Finalizadas [x Temporadas]"
				     , "Animados Mangas [####]/!! Series Mangas Finalizadas [x Temporadas]"
					, "Series Finalizadas [x Temporadas] [Mangas]"
					, "Series Mangas [TX]/Series Anime [Finalizadas] [HD Dual Audio]"
					, "Series Mangas [TX]/Series Anime [Finalizadas]"
                    , "Animados Mangas [####]/Series Mangas x Temporada"
                    , "Animados Mangas [####]/Series Mangas [TX] Temporada [Cap Sueltos]"
                                                                )
			, TipoDeEtiquetaDeSerie.FINALIZADAS
			
			                                                    );
			DirectoriosPrestablecidos urls_mangas_finalizadas_hd_dualAdio = mdp(getRutas(
				                                                                "Series Mangas [TX]/Series Anime [Finalizadas] [HD Dual Audio]"
					
			                                                                )
			, TipoDeEtiquetaDeSerie.FINALIZADAS
			, TipoDeEtiquetaDeSerie.HD
			, TipoDeEtiquetaDeSerie.DUAL_AUDIO
			                                                                );
			
			
			
			//series:-persona
			DirectoriosPrestablecidos urls_tx = dp(getRutas(
				                                    "Series/En Transmision/Series [TX]"
					
			                                    )
			, TipoDeEtiquetaDeSerie.TX
			                                    );
			DirectoriosPrestablecidos urls_tx_clasicas = dp(getRutas(
				                                             "Series [TX] [ Clasicas]"
				, "Series/En Transmision/Series [TX] [Clásicas]"
				, "Series/En Transmision/Series [TX] [Clasicas]"
			                                             )
			                                             , TipoDeEtiquetaDeSerie.TX
			, TipoDeEtiquetaDeSerie.CLASICAS
			                                             );
			DirectoriosPrestablecidos urls_tx_estreno = dp(getRutas(
				                                            "Series [TX] [ Estreno]"
			                                            )
			                                            , TipoDeEtiquetaDeSerie.TX
			, TipoDeEtiquetaDeSerie.ESTRENO
			                                            );
			
			DirectoriosPrestablecidos urls_tx_estreno_hd_dualAudio = dp(getRutas(
				                                                         "Series [TX] [ Estreno] [HD Dual Audio]"
			                                                         )
			                                                         , TipoDeEtiquetaDeSerie.TX
			, TipoDeEtiquetaDeSerie.ESTRENO
			, TipoDeEtiquetaDeSerie.HD
			, TipoDeEtiquetaDeSerie.DUAL_AUDIO
			                                                         );
			DirectoriosPrestablecidos urls_tx_hd_dualAudio = dp(getRutas(
				                                                 "Series/En Transmision/Series [En Transmision] [HD Dual Audio]"
				, "Series/En Transmision/Series [TX] [HD Dual Audio]"
                , "Series [TX] [HD Dual Audio]"
                
                                                             )
			                                                 , TipoDeEtiquetaDeSerie.TX
			, TipoDeEtiquetaDeSerie.HD
			, TipoDeEtiquetaDeSerie.DUAL_AUDIO
			                                                 );
			DirectoriosPrestablecidos urls_tx_hd = dp(getRutas(
				                                       "Series/En Transmision/Series [TX][HD]"
			                                       )
			                                       , TipoDeEtiquetaDeSerie.TX
			, TipoDeEtiquetaDeSerie.HD
			                                       );
			DirectoriosPrestablecidos urls_tx_estreno_españolas = dp(getRutas(
				                                                      "Series [TX] [ Estreno][Españolas]"
			                                                      )
			                                                      , TipoDeEtiquetaDeSerie.TX
			, TipoDeEtiquetaDeSerie.ESTRENO
			, TipoDeEtiquetaDeSerie.ESPAÑOLAS
			                                                      );


            DirectoriosPrestablecidos urls_tx_españolas = dp(getRutas(
                                                                      "Series [TX] [Españolas]"
                                                                  )
                                                                  , TipoDeEtiquetaDeSerie.TX
            
            , TipoDeEtiquetaDeSerie.ESPAÑOLAS
                                                                  );

            DirectoriosPrestablecidos urls_tx_estreno_subtituladas = dp(getRutas(
				                                                         "Series [TX] [ Estreno][Subtituladas]"
			                                                         )
			                                                         , TipoDeEtiquetaDeSerie.TX
			, TipoDeEtiquetaDeSerie.ESTRENO
			, TipoDeEtiquetaDeSerie.SUBTITULADAS
			                                                         );
			DirectoriosPrestablecidos urls_tx_subtituladas = dp(getRutas(
				                                                 "Series/En Transmision/Series [TX] [Subtituladas]"
                                                                 , "Series [TX] [Subtituladas]"
                                                             )
			                                                 , TipoDeEtiquetaDeSerie.TX
			, TipoDeEtiquetaDeSerie.SUBTITULADAS
			                                                 );
			DirectoriosPrestablecidos urls_tx_dobladas = dp(getRutas(
				                                             "Series [TX] [Dobladas al Español]"
					, "Series/En Transmision/Series [En Transmision] [Dobladas]"
					, "Series/En Transmision/Series [TX] [Dobladas]"
					, "Series/En Transmision/Series [TX] [Dobladas al Español]"
			                                             )
			                                             , TipoDeEtiquetaDeSerie.TX
			, TipoDeEtiquetaDeSerie.DOBLADAS
			                                             );
			
			DirectoriosPrestablecidos urls_finalizadas = dp(getRutas(
				                                             "Series Finalizadas [x Temporadas]"
			                                             )
			, TipoDeEtiquetaDeSerie.FINALIZADAS
			                                             );
			DirectoriosPrestablecidos urls_finalizadas_estreno = dp(getRutas(
				                                                     "Series Finalizadas [x Temporadas] [Estrenos]"
				, "Series/Finalizadas/Series [Temporadas Finalizadas] [Estrenos]"
			                                                     )
			, TipoDeEtiquetaDeSerie.FINALIZADAS
			, TipoDeEtiquetaDeSerie.ESTRENO
			                                                     );
			DirectoriosPrestablecidos urls_finalizadas_estreno_hd = dp(getRutas(
				                                                        "Series/Finalizadas/Series [Temporadas Finalizadas] [Estrenos] [HD]"
			                                                        )
			, TipoDeEtiquetaDeSerie.FINALIZADAS
			, TipoDeEtiquetaDeSerie.ESTRENO
			, TipoDeEtiquetaDeSerie.HD
			                                                        );
			DirectoriosPrestablecidos urls_finalizadas_dobladas = dp(getRutas(
				                                                      "Series Finalizadas [x Temporadas] [ESPAÑOL]"
				, "Series/Finalizadas/Series [Temporadas Finalizadas] [Dobladas]"
			                                                      )
			, TipoDeEtiquetaDeSerie.FINALIZADAS
			, TipoDeEtiquetaDeSerie.DOBLADAS
			                                                      );
			DirectoriosPrestablecidos urls_finalizadas_dobladas_hd = dp(getRutas(
				                                                         "Series/Finalizadas/Series [Temporadas Finalizadas] [Dobladas] [HD]"
			                                                         )
			, TipoDeEtiquetaDeSerie.FINALIZADAS
			, TipoDeEtiquetaDeSerie.DOBLADAS
			, TipoDeEtiquetaDeSerie.HD
			                                                         );
			DirectoriosPrestablecidos urls_finalizadas_hd_dualAudio = dp(getRutas(
				 "Series Finalizadas [x Temporadas] [HD Dual Audio]"
				, "Series/Finalizadas/Series [Temporadas Finalizadas] [HD Dual Audio]"
                , "Series [Temporadas Finalizadas] [HD Dual Audio]"
                                                                      )
			, TipoDeEtiquetaDeSerie.FINALIZADAS
			, TipoDeEtiquetaDeSerie.DUAL_AUDIO
			, TipoDeEtiquetaDeSerie.HD
			                                                          );
			DirectoriosPrestablecidos urls_finalizadas_hd = dp(getRutas(
				                                                "Series Finalizadas [x Temporadas] [HD]"
			                                                )
			, TipoDeEtiquetaDeSerie.FINALIZADAS
			, TipoDeEtiquetaDeSerie.HD
			                                                );
            DirectoriosPrestablecidos urls_finalizadas_sagas = dp(getRutas(
                                                                "Series [Temporadas Finalizadas] [Sagas]"
                                                            )
            , TipoDeEtiquetaDeSerie.FINALIZADAS
            , TipoDeEtiquetaDeSerie.SAGAS
                                                            );
            DirectoriosPrestablecidos urls_finalizadas_subtituladas = dp(getRutas(
				                                                          "Series Finalizadas [x Temporadas] [Subtituladas]"
			                                                          )
			, TipoDeEtiquetaDeSerie.FINALIZADAS
			, TipoDeEtiquetaDeSerie.SUBTITULADAS
			                                                          );
			DirectoriosPrestablecidos urls_finalizadas_clasicas = dp(getRutas(
				                                                      "Series/Finalizadas/Series [Temporadas Finalizadas] [Clasicas]"
                                                                      , "Series [Temporadas Finalizadas] [Clasicas]"
                                                                      
                                                                  )
			, TipoDeEtiquetaDeSerie.FINALIZADAS
			, TipoDeEtiquetaDeSerie.CLASICAS
			                                                      );
			
			
			analizarDirectoriosPrestablecidos(this.paquete.seriesMangas, this.d_mangas);
			analizarDirectoriosPrestablecidos(this.paquete.seriesPersona, this.d_series);
			
			
			
			
			//peliculas: -mangas
			string[] urls_peliculas_mangas_clasicas = getRutas(
				                                          "Animados Mangas [####]/!! Peliculas Mangas Clasicas"
				, "Peliculas Anime [Clasicas]"
				, "Series Mangas [TX]/Peliculas Anime [Clasicas]"
			                                          );
			string[] urls_peliculas_mangas = getRutas(
				                                 "Series Mangas [TX]/Peliculas [Mangas]"
			                                 );

            Action<DirectoryInfo> agregarPeliculasMangas = f => { };

			analizarDirectoriosSueltos(urls_peliculas_mangas_clasicas, agregarPeliculasMangas);
			analizarDirectoriosSueltos(urls_peliculas_mangas, agregarPeliculasMangas);
			
//			cwl(Utiles.str_ln(this.urlsPadres));
//			cwl("++++++++++++++++++++++++++++++");
			
			buscarPadresCompletosUsados();
			
//			cwl(Utiles.str_ln(this.urlsPadres));
//			cwl("--------------------------------");
//			cwl(Utiles.str_ln(this.urlsUsadas));
		}
		public string getUrlRelativaDelPaquete(DirectoryInfo c){
			cwl(c.ToString());
			return c.ToString().Replace(this.carpetaPaquete.ToString()+@"\","");
		}
		
		public void agregarDirectorio(DirectoryInfo c, ConjuntoDeEtiquetasDeSerie cte,TipoDeEtiquetaDeSerie tagPrincipalPadre=null)
		{
			if (tagPrincipalPadre==TipoDeEtiquetaDeSerie.PRINCIPAL_MANGA||cte.tieneEtiquetas_OR(TipoDeEtiquetaDeSerie.PRINCIPAL_MANGA, TipoDeEtiquetaDeSerie.MANGAS)) {
              //  cwl("c="+c);
				paquete.seriesMangas.addDirectorio(cte, c);
			} else {
               // cwl("2c=" + c);
                paquete.seriesPersona.addDirectorio(cte, c);
			}
			
			//aqui hay que usar a agregarUrl_Usada pero hay que crear
			//un metodo que cree una url relativa al paquete para el
			agregarUrl_Usada(c.ToString(),getUrlRelativaDelPaquete(c));
		}
		private void buscarUrlsDe(DirectoryInfo carpeta,PatronRegex pr,TipoDeEtiquetaDeSerie tagPrincipalPadre=null){
			TipoDeEtiquetaDeSerie tag=tagPrincipalPadre;
			foreach (DirectoryInfo d in carpeta.GetDirectories()) {
				//if(d.Name=="Series Mangas [TX]"){
				//	cwl("aqui");
				//}
				
				if (!this.urlsUsadas.Contains(d.FullName)) {
					ConjuntoDeEtiquetasDeSerie cte = new ConjuntoDeEtiquetasDeSerie(
						                                 //this.reg.Re_EtiquetasDeSerie_Principal_Secundarias
						                                 pr
						                                 , d.Name
					                                 );
					if (!cte.isEmpty()) {
						if (!cte.tieneSoloAlgunaEtiquetaPrincipal()) {
							agregarDirectorio(d,cte);
						} else {
							buscarUrlsDe(d,this.reg.Re_EtiquetasDeSerie,cte.getEtiquetaPrincipal());
						}
					}

					
				}
			}
			
		}
		public void buscarUrls()
		{
			buscarUrlsDefaults();
            buscarUrlsDe(this.carpetaPaquete, this.reg.Re_EtiquetasDeSerie_Principal_Secundarias);

   //         cwl(Utiles.str_ln(this.urlsPadres));
			//cwl("++++++++++++++++++++++++++++++");
			
			buscarPadresCompletosUsados();


            //cwl(Utiles.str_ln(this.urlsPadres));
            //cwl("--------------------------------");
            //cwl(Utiles.str_ln(this.urlsUsadas));
            //			
        }
		private void buscarPadresCompletosUsados()
		{
			foreach (string url in this.urlsPadres) {
				DirectoryInfo cp = Archivos.unirUrls_Direc(this.carpetaPaquete, url);
				
				
				bool todosLosHijosSeHanEncontrado = true;
				
				bool laDireccionPadreYaSeEncuentraUsada = this.urlsUsadas.Contains(cp.FullName);
				
				if (!laDireccionPadreYaSeEncuentraUsada) {
					foreach (DirectoryInfo dh in cp.GetDirectories()) {
						
						if (!this.urlsUsadas.Contains(dh.FullName)) {
							todosLosHijosSeHanEncontrado = false;
							break;
						}

					}
				
				}
				
				if (todosLosHijosSeHanEncontrado) {
					this.urlsUsadas.Add(cp.ToString());
					this.urlsPadres.Remove(url);
					buscarPadresCompletosUsados();
					break;
				}
			}
		
		}
	
	}
}


//
//
//private void buscarPadresCompletosUsados()
//		{
//			foreach (string url in this.urlsPadres) {
//				DirectoryInfo cp = Archivos.unirUrls_Direc(this.carpetaPaquete, url);
//
//
//				bool todosLosHijosSeHanEncontrado = true;
//
//				bool laDireccionPadreYaSeEncuentraUsada = this.urlsUsadas.Contains(cp.FullName);
//
////				foreach (string urlUsada in this.urlsUsadas) {
////					string dh_str = cp.FullName;
////					if (dh_str == urlUsada) {
////						todosLosHijosSeHanEncontrado = true;
////						break;
////					}
////				}
//
//				if (!laDireccionPadreYaSeEncuentraUsada) {
//					foreach (DirectoryInfo dh in cp.GetDirectories()) {
//
//						if (!this.urlsUsadas.Contains(dh.FullName)) {
//							todosLosHijosSeHanEncontrado = false;
//							break;
//						}
////						string dh_str = dh.FullName;
//
////						bool laEncontro = false;
////						foreach (string urlUsada in this.urlsUsadas) {
////							if (dh_str == urlUsada) {
////								laEncontro = true;
////								break;
////							}
////						}
////
////						if (!laEncontro) {
////							todosLosHijosSeHanEncontrado = false;
////							break;
////						}
//					}
//
//				}
//
//				if (todosLosHijosSeHanEncontrado) {
//					//this.urlsUsadas.Add(url);
//					this.urlsUsadas.Add(cp.ToString());
//					this.urlsPadres.Remove(url);
//					buscarPadresCompletosUsados();
//					break;
//				}
//			}
//
//		}
//
