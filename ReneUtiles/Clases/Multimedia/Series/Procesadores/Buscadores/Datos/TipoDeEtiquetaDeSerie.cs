/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 12/8/2022
 * Hora: 07:54
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
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores;

namespace ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos
{
	/// <summary>
	/// Description of TipoDeEtiqueta.
	/// </summary>
	public class TipoDeEtiquetaDeSerie:ConsolaBasica,IComparable<TipoDeEtiquetaDeSerie>
	{
		//Etiquetas principales
		public static readonly TipoDeEtiquetaDeSerie PRINCIPAL_MANGA = new  TipoDeEtiquetaDeSerie("MANGA", "g_prin_manga",
			                                                               sm(ss("Series", "Anime"), ss("Anime", "Online")
			   , ss("Animados", "Mangas"), ss("Series", "Mangas"), ss("Mangas"))
		                                                               );
		public static readonly TipoDeEtiquetaDeSerie PRINCIPAL_SERIES_PERSONA = new  TipoDeEtiquetaDeSerie("SERIES", "g_prin_ser_per",
			                                                                        sm(ss("Series"))
		                                                                        );
		
		//Tipo
		public static readonly TipoDeEtiquetaDeSerie MANGAS = new  TipoDeEtiquetaDeSerie("MANGAS", "g_mangas",
			                                                      sm(ss("Mangas"))
		                                                      );
		
		//-------
		public static readonly TipoDeEtiquetaDeSerie TX = new  TipoDeEtiquetaDeSerie("TX", "g_tx",
			                                                  sm(ss("tx"), ss("En", "Transmision"), ss("Transmision"), ss("x", "Capitulos"))
		                                                  );
		public static readonly TipoDeEtiquetaDeSerie FINALIZADAS = new  TipoDeEtiquetaDeSerie("FINALIZADAS", "g_finalizadas",
			                                                           sm(ss("Finalizadas")
			   , ss("x", "Temporadas"), ss("Temporadas", "Finalizadas"))
		                                                           );
		public static readonly TipoDeEtiquetaDeSerie HD = new  TipoDeEtiquetaDeSerie("HD", "g_hd",
			                                                  sm(ss("hd"))
		                                                  );
		public static readonly TipoDeEtiquetaDeSerie CLASICAS = new  TipoDeEtiquetaDeSerie("CLASICAS", "g_clasicas",
			                                                        sm(ss("clasicas"))
		                                                        );
		public static readonly TipoDeEtiquetaDeSerie DUAL_AUDIO = new  TipoDeEtiquetaDeSerie("DUAL_AUDIO", "g_dual_audio",
			                                                          sm(ss("Dual", "Audio"))
		                                                          );
		public static readonly TipoDeEtiquetaDeSerie SUBTITULADAS = new  TipoDeEtiquetaDeSerie("SUBTITULADAS", "g_subtituladas",
			                                                            sm(ss("Subtituladas"))
		                                                            );
		public static readonly TipoDeEtiquetaDeSerie ESTRENO = new  TipoDeEtiquetaDeSerie("ESTRENO", "g_extreno",
			                                                       sm(ss("Estreno"), ss("Estrenos"))
		                                                       );
		//idioma
		public static readonly TipoDeEtiquetaDeSerie DOBLADAS = new  TipoDeEtiquetaDeSerie("DOBLADAS", "g_dobladas",
			                                                        sm(ss("Dobladas"), ss("Dobladas", "al", "Español"))
		                                                        );
		public static readonly TipoDeEtiquetaDeSerie ESPAÑOL = new  TipoDeEtiquetaDeSerie("ESPAÑOL", "g_espanol",
			                                                       sm(ss("ESPAÑOL"),ss("ESP"))
		                                                       );
		//nacionalidad
		public static readonly TipoDeEtiquetaDeSerie ESPAÑOLAS = new  TipoDeEtiquetaDeSerie("ESPAÑOLAS", "g_espanolas",
			                                                         sm(ss("Españolas"))
		                                                         );
        public static readonly TipoDeEtiquetaDeSerie SAGAS = new TipoDeEtiquetaDeSerie(
            "SAGAS", "g_sagas",
                                                                     sm(ss("Sagas"))
                                                                 );

        public static  TipoDeEtiquetaDeSerie[] ETIQUETAS_PRINCIPALES = {
			PRINCIPAL_MANGA,
			PRINCIPAL_SERIES_PERSONA
		};
		public static	 TipoDeEtiquetaDeSerie[] ETIQUETAS_DE_CLASIFICACION = {MANGAS, TX, FINALIZADAS, HD, CLASICAS
					, DUAL_AUDIO, SUBTITULADAS, ESTRENO, DOBLADAS, ESPAÑOL, ESPAÑOLAS,SAGAS
        };
        public static TipoDeEtiquetaDeSerie[] ETIQUETAS_DE_CLASIFICACION_EXTRA = {MANGAS, HD, CLASICAS
                    , DUAL_AUDIO, SUBTITULADAS, ESTRENO, DOBLADAS, ESPAÑOL, ESPAÑOLAS,SAGAS
        };
        public static TipoDeEtiquetaDeSerie[] VALUES = Arreglos.unir(ETIQUETAS_PRINCIPALES, ETIQUETAS_DE_CLASIFICACION);

        public static string KEY_GRUPO_PRINCIPAL = "gru_tags_prin";
		public static string KEY_GRUPO_SECUNDARIO = "gru_tags_prin";
		
		public string[][] Textos;
		public string keyGrup;
		public string nombreTag;
		public  TipoDeEtiquetaDeSerie(
			string nombreTag,
			string keyGrup,
			string[][] Textos
			
		)
		{
			this.Textos = Textos;
			this.keyGrup = keyGrup;
			this.nombreTag = nombreTag;
		}
		
		public bool tieneEsteGrupo(Match m)
		{
			return m.Groups[this.keyGrup].Success;
		}
		public Group getGroup(Match m)
		{
			return m.Groups[this.keyGrup];
		}
		public override string ToString()
		{
			return this.nombreTag;
		}
		
		
		//static
		
		public static string[][] sm(params string[][] a)
		{
			return a;
		}
		public static string[] ss(params string[] a)
		{
			return a;
		}
		
		public static string getPatronTags(params TipoDeEtiquetaDeSerie[] tags)
		{
			string m = "(?:";
			for (int i = 0; i < tags.Length; i++) {
				if (i != 0) {
					m += "|";
				}
				TipoDeEtiquetaDeSerie t = tags[i];
				string mp = "";
				for (int j = 0; j < t.Textos.Length; j++) {
					if (j != 0) {
						mp += "|";
					}
					mp += "(?:";
					for (int k = 0; k < t.Textos[j].Length; k++) {
                        if (k != 0)
                        {
                            //mp += ConstantesExprecionesRegulares.ElAnteriorNoEsLetraNiNumero;
                            mp += ConstantesExprecionesRegulares.separaciones_UnoAlMenos;
                        }
                        mp += Matchs.getTextoArreglado(t.Textos[j][k]);
					}
					mp += ")";
					
				}
				m += Matchs.grupoNombrado(t.keyGrup, mp);
			}
			m += ")";
			return m;
		}
		public static PatronRegex getPatronRegex_PrincipalesYDespues_Tags()
		{
			
			string m = Matchs.grupoNombrado(KEY_GRUPO_PRINCIPAL, getPatronTags(ETIQUETAS_PRINCIPALES))
                + ConstantesExprecionesRegulares.separaciones_UnoAlMenos
                       + Matchs.grupoNombrado(KEY_GRUPO_SECUNDARIO, getPatronTags(ETIQUETAS_DE_CLASIFICACION)) + "*";
			
			
			return new PatronRegex(m);
		}
		public static PatronRegex getPatronRegex_Etiquetas()
		{
			
			string m = "(?:" + Matchs.grupoNombrado(KEY_GRUPO_PRINCIPAL, getPatronTags(ETIQUETAS_PRINCIPALES)) + "|"
			           + Matchs.grupoNombrado(KEY_GRUPO_SECUNDARIO, getPatronTags(ETIQUETAS_DE_CLASIFICACION)) + ")+";
			
			
			return new PatronRegex(m);
		}
		public static SortedSet<TipoDeEtiquetaDeSerie> getTiposDeEtiqueta_SiSoloEstaCompuestoPorEstas(PatronRegex re, string texto)
		{
			List<TipoDeEtiquetaDeSerie> l = new List<TipoDeEtiquetaDeSerie>();
//			cwl("re.InicialSReSFinal="+re.InicialSReSFinal);
//			cwl("texto="+texto);
			Match m = re.InicialSReSFinal.Match(texto);
			if (m.Success) {
				foreach (TipoDeEtiquetaDeSerie t in ETIQUETAS_PRINCIPALES) {
					if (t.tieneEsteGrupo(m)) {
						l.Add(t);
						break;
					}
				}	
				foreach (TipoDeEtiquetaDeSerie t in ETIQUETAS_DE_CLASIFICACION) {
					if (t.tieneEsteGrupo(m)) {
						l.Add(t);
					
					}
				}
			}	
			
			return ComparadorTipoDeEtiquetaDeSerie.getNewSortedSet_TipoDeEtiquetaDeSerie(l);
		}
		
		//		public static List<TipoDeEtiquetaDeSerie> getTiposDeEtiqueta_SiSoloEstaCompuestoPorEstas(PatronRegex re, string texto)
		//		{
		//			List<TipoDeEtiquetaDeSerie> l = new List<TipoDeEtiquetaDeSerie>();
		//			Match m = re.InicialSReSFinal.Match(texto);
		//			foreach ( TipoDeEtiquetaDeSerie t in ETIQUETAS_PRINCIPALES) {
		//				if (t.tieneEsteGrupo(m)) {
		//					l.Add(t);
		//					break;
		//				}
		//			}
		//			foreach ( TipoDeEtiquetaDeSerie t in ETIQUETAS_DE_CLASIFICACION) {
		//				if (t.tieneEsteGrupo(m)) {
		//					l.Add(t);
		//
		//				}
		//			}
		//			return l;
		//		}
		//
		public int CompareTo(TipoDeEtiquetaDeSerie value)
		{
			return  this.keyGrup.CompareTo(value.keyGrup);
		}

        public static TipoDeEtiquetaDeSerie get(string nombre) {
            foreach (TipoDeEtiquetaDeSerie t in VALUES)
            {
                if (t.nombreTag==nombre) {
                    return t;
                }
            }
            return null;
        }


        public static HashSet<TipoDeEtiquetaDeSerie> getNewHashSet() {
            return ComparadorTipoDeEtiquetaDeSerie.getNewHashSet_TipoDeEtiquetaDeSerie();
        }
        public static HashSet<TipoDeEtiquetaDeSerie> getNewHashSet(IEnumerable<TipoDeEtiquetaDeSerie> anterior)
        {
            return ComparadorTipoDeEtiquetaDeSerie.getNewHashSet_TipoDeEtiquetaDeSerie(anterior);
        }
        public static SortedSet<TipoDeEtiquetaDeSerie> getNewSortedSet() {
            return ComparadorTipoDeEtiquetaDeSerie.getNewSortedSet_TipoDeEtiquetaDeSerie();
        }
        public static SortedSet<TipoDeEtiquetaDeSerie> getNewSortedSet(IEnumerable<TipoDeEtiquetaDeSerie> anterior)
        {
            return ComparadorTipoDeEtiquetaDeSerie.getNewSortedSet_TipoDeEtiquetaDeSerie(anterior);
        }


    }
	
	//-------------------------
		
	public class ComparadorTipoDeEtiquetaDeSerie:IEqualityComparer<TipoDeEtiquetaDeSerie>,IComparer<TipoDeEtiquetaDeSerie>
	{
		private static readonly ComparadorTipoDeEtiquetaDeSerie comparadorDeIgualdad_TipoDeEtiquetaDeSerie = new ComparadorTipoDeEtiquetaDeSerie();
		
		public static readonly Dictionary<string,int> codigosHash = new  Dictionary<string,int>();
		public static int ultimoHash = 0;
		private string getKey(TipoDeEtiquetaDeSerie obj)
		{
			return obj.keyGrup;
		}
		public bool Equals(TipoDeEtiquetaDeSerie x, TipoDeEtiquetaDeSerie y)
		{
			return getKey(x) == getKey(y);
		}
		public int GetHashCode(TipoDeEtiquetaDeSerie obj)
		{
			string key = getKey(obj);
			if (codigosHash.ContainsKey(key)) {
				return codigosHash[key];
			}
			int hash = ultimoHash++;
			codigosHash.Add(key, hash);
			return hash;
		}
		public int Compare(TipoDeEtiquetaDeSerie x, TipoDeEtiquetaDeSerie y)
		{
			return x.CompareTo(y);
		}
			
			
		public static HashSet<TipoDeEtiquetaDeSerie> getNewHashSet_TipoDeEtiquetaDeSerie()
		{
			return new HashSet<TipoDeEtiquetaDeSerie>(comparadorDeIgualdad_TipoDeEtiquetaDeSerie);
		}
		public static HashSet<TipoDeEtiquetaDeSerie> getNewHashSet_TipoDeEtiquetaDeSerie(IEnumerable<TipoDeEtiquetaDeSerie> anterior)
		{
			return new HashSet<TipoDeEtiquetaDeSerie>(anterior, comparadorDeIgualdad_TipoDeEtiquetaDeSerie);
		}
		
		public static SortedSet<TipoDeEtiquetaDeSerie> getNewSortedSet_TipoDeEtiquetaDeSerie(IEnumerable<TipoDeEtiquetaDeSerie> anterior)
		{
			return new SortedSet<TipoDeEtiquetaDeSerie>(anterior, comparadorDeIgualdad_TipoDeEtiquetaDeSerie);
		}
        public static SortedSet<TipoDeEtiquetaDeSerie> getNewSortedSet_TipoDeEtiquetaDeSerie()
        {
            return new SortedSet<TipoDeEtiquetaDeSerie>( comparadorDeIgualdad_TipoDeEtiquetaDeSerie);
        }

        public static Dictionary<TipoDeEtiquetaDeSerie,E> getNewDictionary_TipoDeEtiquetaDeSerie<E>()
		{
			return new Dictionary<TipoDeEtiquetaDeSerie, E>(comparadorDeIgualdad_TipoDeEtiquetaDeSerie);
		}
	}
	
		
	//--------------------------
}
