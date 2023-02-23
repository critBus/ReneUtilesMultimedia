/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 31/7/2022
 * Hora: 14:20
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
using System.IO;
using ReneUtiles.Clases.Multimedia.Series.Procesadores;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos;
using ReneUtiles.Clases.ExprecionesRegulares;
namespace ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores
{
	/// <summary>
	/// Description of CreadorDeNombreClaveDeSerie.
	/// </summary>
	public class CreadorDeNombreClaveDeSerie:ConsultorDeDatosEnNombre
	{
		//class LimitesDeIndice
		//{
		//	public int inicial;
		//	public int final;
		//}
		//class SeleccionadorDeTramosDeNombre
		//{
		//	public string nombreARecortar;
		//	public List<LimitesDeIndice> tramosAQuitar = new List<LimitesDeIndice>();
		//	public void addLimites(int i0, int i)
		//	{
		//		if (i0 != i && i0 >= 0 && i <= nombreARecortar.Length) {
		//			LimitesDeIndice l = new LimitesDeIndice();
		//			l.inicial = i0;
		//			l.final = i;
		//			tramosAQuitar.Add(l);
		//		}
				
		//	}
		//	public string getNombreRecortado()
		//	{
		//		string r = "";
		//		for (int i = 0; i < nombreARecortar.Length; i++) {
		//			bool saltar = false;
		//			foreach (LimitesDeIndice lim in tramosAQuitar) {
		//				if (i >= lim.inicial && i <= lim.final) {
		//					i = lim.final;
		//					saltar = true;
		//					break;
		//				}
		//			}
		//			if (saltar) {
		//				continue;
		//			}
		//			r += nombreARecortar.ElementAt(i);
		//		}
		//		return r;
		//	}
		
		//}
		
		public CreadorDeNombreClaveDeSerie(ProcesadorDeNombreDeSerie pr, ProcesadorDeSeries pro = null)
			: base(pr, pro)
		{
		}
		
		//public  DatosDeNombreSerie crearNombreClave(bool detenerSiEncuentraPatronesAlFinal = true)
		public  DatosDeNombreSerie crearDatosDeNombre(bool detenerSiEncuentraPatronesAlFinal = true)
		{
			this.seBusco = true;
			string nombreAdaptado = this.nombre;
            //if (nombreAdaptado == "1983")
            //{
            //    cwl("aqui");
            //}

            DatosDeNombreSerie dn = getDn();
			
			dn.fechasEnNombre = getPr().buscarFechas(0);
			//	cwl("nombreAdaptado="+nombreAdaptado);
			DatosDeNombreCapituloDelPrincipio dp = getPr().getCapitulosDeNombreDelPrincipio(detenerSiEncuentraPatronesAlFinal);
			if (dp != null) {
				if (dp.DatosDelFinal != null) {
					//dn.DatosCapituloDelFinal = dp.DatosDelFinal;
					dn.datosDelFinal = dp.DatosDelFinal;
					dp = null;
					if (!detenerSiEncuentraPatronesAlFinal) {
						dn.datosDelPrincipio = dp;
					}
				} else {
					//dn.DatosCapituloDelPrincipio = dp;
					dn.datosDelPrincipio = dp;
				}
                //				cwl("dp.Capitulo="+dp.Capitulo);
                //				cwl("dp.CapituloFinal="+dp.CapituloFinal);
                //				cwl("dp.CapituloInicial="+dp.CapituloInicial);
                //				cwl("dp.Temporada="+dp.Temporada);
                //				cwl("dp.CantidadDeOvasQueContiene="+dp.CantidadDeOvasQueContiene);
                //				cwl("dp.EsOVA="+dp.EsOVA);
                //				cwl("dp.CantidadDeCapitulosQueContiene="+dp.CantidadDeCapitulosQueContiene);
                //cwl("dp.EsOVA="+dp.EsOVA);
            }else
                {
                
                //if (esNumero(nombreAdaptado)) { dn.setEsSoloNumero(true); }
                }
            
			
			if (!dn.EsSoloNumeros) {
				int I0 = (dp != null && dp.IndiceDeInicioDespuesDeLosNumeros != -1) ? dp.IndiceDeInicioDespuesDeLosNumeros : 0;
//				dn.DatosCapituloDelFinal = getPr().getCapitulosDeNombreDelFinal( I0);
//				int IFinal = dn.DatosCapituloDelFinal != null && dn.DatosCapituloDelFinal.IndiceDelFinalDeNombre != -1 ? dn.DatosCapituloDelFinal.IndiceDelFinalDeNombre : nombreAdaptado.Length;
				//if(nombreAdaptado=="American Horror History S09E01 [720p] [Dual Audio]"){
				//	//cwl("aqui!!");
				//}
				ProcesadorDeNombreDeSerie pra=getPr();
				dn.datosDelFinal = pra.getCapitulosDeNombreDelFinal(I0);
                if (dn.datosDelFinal != null) {
                    int i = dn.datosDelFinal.getIndiceDelFinalDeNombre();
                }
				int IFinal = dn.datosDelFinal != null && dn.datosDelFinal.IndiceDelFinalDeNombre != -1 ? dn.datosDelFinal.IndiceDelFinalDeNombre : nombreAdaptado.Length;
				
				SeleccionadorDeTramosDeStr sln = new SeleccionadorDeTramosDeStr();
				sln.nombreARecortar = nombreAdaptado;
                if (I0 > 0)
                {
                    sln.addLimites(0, I0 - 1);
                }
                if (IFinal < nombreAdaptado.Length)
                {
                    sln.addLimites(IFinal, nombreAdaptado.Length);
                }
                //sln.addLimites(0, I0);
                //sln.addLimites(IFinal, nombreAdaptado.Length);

                BuscadorDeFechasEnNombre bFechas=null;
				
				foreach (BuscadorDeFechasEnNombre bf in getPr().historialDeBusqueda.getBusquedasFechasEnNombre()) {
					foreach (DatosDeFechaEnNombre df in bf.getFechasEnNombre()) {
						DatosDeFechaEnNombre.LimitesDeFecha lf = df.getLimitesDeFecha(); 
						sln.addLimites(lf.indiceInicial, lf.indiceFinal);
					}
					if(bFechas==null){
						bFechas=bf;
					}else if(bf.I0<bFechas.I0){
						bFechas=bf;
					}
					
				}
				
				BuscadorDeAleatoriedadesEnNombre bAleatoriedades=null;
				foreach (BuscadorDeAleatoriedadesEnNombre bf in getPr().historialDeBusqueda.getBusquedasDeAleatoriedadesEnNombre()) {
					foreach (DatosDeAleatoriedadEnNombre df in bf.aleatoriedadesEnNombre) {
						//DatosDeFechaEnNombre.LimitesDeFecha lf=df.getLimitesDeFecha();
						sln.addLimites(df.indiceInicial, df.indiceFinal);
					}
					if(bAleatoriedades==null){
						bAleatoriedades=bf;
					}else if(bf.I0<bAleatoriedades.I0){
						bAleatoriedades=bf;
					}
				}
				
				
				BuscadorDeEtiquetasEnNombre bEtiquetas=null;
				foreach (BuscadorDeEtiquetasEnNombre bf in getPr().historialDeBusqueda.getBusquedasEtiquetasEnNombre()) {
					
					foreach (KeyValuePair<TipoDeEtiquetaDeSerie,List<DatosDeEtiquetaEnNombre>> row in bf.etiquetasEnNombre) {
						foreach (DatosDeEtiquetaEnNombre df in row.Value) {
							sln.addLimites(df.indiceInicial, df.indiceFinal);
						}
					}
					
					if(bEtiquetas==null){
						bEtiquetas=bf;
					}else if(bf.I0<bEtiquetas.I0){
						bEtiquetas=bf;
					}
					
				}
				dn.fechasEnNombre=bFechas.getFechasEnNombre();
				dn.aleatoriedadesEnNombre=bAleatoriedades.aleatoriedadesEnNombre;
				dn.etiquetasEnNombre=bEtiquetas.etiquetasEnNombre;
                if (dn.fechasEnNombre.Count()>0
                    || dn.aleatoriedadesEnNombre.Count() > 0
                    || dn.etiquetasEnNombre.Count() > 0) {
                    //if (I0 > 0)
                    //{
                    //    sln.addLimites(0, I0 - 1);
                    //}
                    //if (IFinal < nombreAdaptado.Length)
                    //{
                    //    sln.addLimites(IFinal, nombreAdaptado.Length);
                    //}
                    //nombreAdaptado = sln.getNombreRecortado();
                    I0 = 0;
                    IFinal = nombreAdaptado.Length;

                }
                nombreAdaptado = sln.getStrRecortado();

                nombreAdaptado = adaptarNombreRecortado(
					nombreRecortadoDeSerie: nombreAdaptado
					, I0: I0
					, IFinal: IFinal
				);
				//aqui!!!!!!!!!!!!!!1

				dn.NombreAdaptado = nombreAdaptado;
				if (getPro() != null) {
					dn.setTipoDeNombre(getPr().getTipoDeNombreDe(getPro(), dn.NombreAdaptado));
//					dn.TipoDeNombre=getPr().getTipoDeNombreDe(getPro(),dn.NombreAdaptado);
				} else {
					dn.setTipoDeNombre(getPr().getTipoDeNombreDe(dn.NombreAdaptado));
//					dn.TipoDeNombre=getPr().getTipoDeNombreDe(dn.NombreAdaptado);
				}
				
				
				
				
				
				
				dn.Clave = getPr().crearClave(nombreAdaptado);
				
				
			}//fin del if no es solo numeros
			
//			if (!dn.hayClave) {
//				if (getPr().contextoDeConjunto.DEBERIAN_DE_PERTENECER_A_UNA_MISMA_SERIE()
//				    || getPr().contextoDeConjunto.HASTA_AHORA_TODOS_PERTENECEN_A_UNA_MISMA_SERIE()
//				    || getPr().contextoDeConjunto.TODOS_PERTENECEN_A_UNA_MISMA_SERIE_SEGURO()) {
//					dn.Clave =getPr().contextoDeConjunto.NombresClaveAlQuedeberianPertenecer.ElementAt(0);//[0]; //getPr().contextoDeConjunto.NombreClaveAlQuedeberianPertenecer;
//				}
//			}
			
			//DatosDeNombreCapitulo dc=dn.getDatosDeNombreCapitulo();
			//cwl("dc.EsContenedorDeTemporada="+dn.DatosCapituloDelFinal.EsContenedorDeTemporada);
			
			return dn;
			
			
			//-------------------------
			
//			m = null;
//			string nombreDespuesDeCapitulosDelPrincipio = null;
//			//patron ignorar Palabras al Principio entre [ ] o ( )
//			
//			m = Matchs.getMatch(nombreDespuesDeCapitulosDelPrincipio, Re_patronIgnorarAlPrincipioRodeado.Re);
//			if (m != null) {
//				nombreDespuesDeCapitulosDelPrincipio = nombreDespuesDeCapitulosDelPrincipio.Substring(m.Length);
//			}
			
			//sustituir equivalentes the &
			return null;
		}
		
		private string adaptarNombreRecortado(string nombreRecortadoDeSerie, int I0 = 0, int IFinal = -1)
		{
			//if (IFinal == -1) {
			//	IFinal = nombreRecortadoDeSerie.Length;
			//}
			string nombreAdaptado = nombreRecortadoDeSerie;
			if (getConf().EsParaAnime) {
				nombreAdaptado = getRe().Re_Ova.SSfreSfS.Replace(nombreAdaptado, "");
			}
			int inicio = 0;
			I0 = 0;
			IFinal = nombreAdaptado.Length;
			//cwl("I0="+I0+" IFinal="+IFinal);
			Match m = null;

            //nombreAdaptado=new Regex("((?:[(]|[[]|[{]).*)$").Replace(nombreAdaptado,"");
            nombreAdaptado = Utiles.eliminarContenidoDeEnvolturas(nombreAdaptado);
            nombreAdaptado = getRe().Re_Env_Contenido_.ReFinal.Replace(nombreAdaptado, "");
			
			
			
			//m = Matchs.getMatch(nombreAdaptado, Re_SaltarHastaDespuesDe.Re);
			m = getRe().Re_SaltarHastaDespuesDe.Re.Match(nombreAdaptado);
			if (m.Success) {
				I0 = m.Index + m.Length;
				nombreAdaptado = nombreAdaptado.Substring(I0);
			}
			m = getRe().Re_separaciones.ReInicial.Match(nombreAdaptado); //Matchs.getMatch(nombreAdaptado, Re_separaciones.ReInicial);
			if (m.Success) {
				I0 = m.Index + m.Length;
				nombreAdaptado = nombreAdaptado.Substring(I0);
			}
			m = getRe().Re_SaltarAlPrincipio.ReInicialSf.Match(nombreAdaptado); //Matchs.getMatch(nombreAdaptado, Re_SaltarAlPrincipio.ReInicialSf);
			if (m.Success) {
				bool noSaltar = false;
				string mlower = m.ToString().ToLower();
				int end = getConf().noSaltarAlPrincipio.Length;
				bool breakFor1 = false;
				for (int i = 0; i < end; i++) {
					CondicionNoSaltarAlPrincipio cn = getConf().noSaltarAlPrincipio[i];
					int end2 = cn.CoincidenciasParaSalto.Length;
					for (int j = 0; j < end2; j++) {
						string coincidencia = cn.CoincidenciasParaSalto[j];
						if (mlower == coincidencia) {
							int end3 = cn.ContinuacionesDelNombre.Length;
                            //Match m2 = Matchs.getMatch(nombreAdaptado.Substring(m.Index + m.Length), cn.Re_ContinuacionesDelNombre_Patron.SuReInicial);
                            int mIndex = m.Index;
                            int mLength = m.Length;
                            string sub = nombreAdaptado.Substring(mIndex + mLength);

                            Match m2 = cn.Re_ContinuacionesDelNombre_Patron.SuReInicial.Match(sub);//Matchs.getMatch(nombreAdaptado.Substring(m.Index + m.Length), );
							if (m2.Success) {
								noSaltar = true;
							}
							
							breakFor1 = true;
							break;
						}
					}
					if (breakFor1) {
						break;
					}
				}
				if (!noSaltar) {
					I0 = m.Index + m.Length;
					nombreAdaptado = nombreAdaptado.Substring(I0);	
				}
				
			}
				
				
		

				
				
			//cwl("p="+Re_detenciones.Patron);
			m = getRe().Re_detenciones.SuRe.Match(nombreAdaptado); //Matchs.getMatch(nombreAdaptado, Re_detenciones.SuRe);
			if (m.Success) {
				int indiceAcontinuacion = m.Index + m.Length;
				if (indiceAcontinuacion < nombreAdaptado.Length) {
					if (!Char.IsLetter(nombreAdaptado.ElementAt(indiceAcontinuacion))) {
						nombreAdaptado = subs(nombreAdaptado, 0, m.Index);		
					}
				} else {
					nombreAdaptado = subs(nombreAdaptado, 0, m.Index);
				}
					
			}
				
			nombreAdaptado = nombreAdaptado.Replace("&", "");
            nombreAdaptado = getRe().Re_separaciones_UnoAlMenos.Re.Replace(nombreAdaptado, " ").Trim();
            nombreAdaptado = getRe().Re_Eliminar.SuReSf.Replace(nombreAdaptado, "");
				
			return nombreAdaptado;
				
		}
		
		public string crearClave(string nombreDeSerie)
		{
			nombreDeSerie = adaptarNombreRecortado(nombreDeSerie);
			string clave = "";
			StringTokenizer stk = StringTokenizer.getTokenizerSeparaciones(nombreDeSerie);
			while (stk.HayNextToken) {
				StringToken tk = stk.next();
				if (Utiles.esAleaterizacion(tk.Token)) {
					nombreDeSerie = subs(nombreDeSerie, 0, tk.IndiceInicial);
					break;
				}
				//cwl("tk.Token="+tk.Token);
				clave += tk.Token;
				//cwl("clave="+clave);
			}
			clave = clave.ToLower();
			return clave;
		}
	}
}

//while ((m = Matchs.getMatch(nombreAdaptado, Re_Eliminar.SuReSf)) != null) {
				
//				while ((m = getRe().Re_Eliminar.SuReSf.) != null) {
//					nombreAdaptado = nombreAdaptado.Replace(m.ToString(), "");
//				}
//				m = Matchs.getMatch(nombreAdaptado, Re_Ova.ReInicial);
//				int inicio = 0;
//				if (m != null) {
//					inicio += m.Length;
//				}
//				if (IFinal <= inicio) {
//					IFinal = nombreAdaptado.Length;
//				}
//				nombreAdaptado = subs(nombreAdaptado, I0, IFinal);
//cwl("nombreAdaptado="+nombreAdaptado);
				
				
				
//				if (cf.EsParaAnime) {
//					m = Matchs.getMatch(nombreAdaptado, Re_Ova.ReInicialSu);
//					if (m == null) {
//						m = Matchs.getMatch(nombreAdaptado, Re_Ova.ReInicialSf);
//					}
//					if (m != null) {
//						nombreAdaptado = nombreAdaptado.Substring(m.Length);
//					}
//				}
				
				
//				// (2019) buscar Señales de año
//				m = Matchs.getMatch(nombreAdaptado, Re_cuatroNumerosAñoModerno.Re);
//				if (m != null) {
//					nombreAdaptado = subs(nombreAdaptado, 0, m.Index);
//
//				}
//				//buscar señales de fecha
//				// [24 2 2015] buscar este patron ejem  -La Esquina del Diablo [Cap 32] [25 2 2015]
//				m = Matchs.getMatch(nombreAdaptado, Re_fecha.Re);
//				if (m != null) {
//					nombreAdaptado = subs(nombreAdaptado, 0, m.Index);
//
//				}
				
//				// a jin II
//				m = Matchs.getMatch(nombreAdaptado, Re_NumerosRomanos.SuReSf);
//				if (m != null) {
//
//					Match	mRomanos = Matchs.getMatch(m, Re_NumerosRomanos.Re);
//					if (mRomanos.ToString().ToLower() != "i") {
//						nombreAdaptado = subs(nombreAdaptado, 0, m.Index);
//					}
//
//
//				}
