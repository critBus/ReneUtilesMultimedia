/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 28/7/2022
 * Hora: 18:00
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
using ReneUtiles.Clases.ExprecionesRegulares;

namespace ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores
{
	/// <summary>
	/// Description of BuscadorDatosSeriesAlPrincipio.
	/// </summary>
	public class BuscadorDeDatosDeSerieAlPrincipio:BuscadorDeDatosDeSerie
	{
		//private PatronesSeriesAlFinal patronesAlFinal;
		//private DatosDeNombreCapituloDelPrincipio d;
		//public BuscadorDatosSeriesAlPrincipio(RecursosDePatronesDeSeries re): base(re)
		
		public bool detenerSiEncuentraPatronesAlFinal = true;
		
		public BuscadorDeDatosDeSerieAlPrincipio(ProcesadorDeNombreDeSerie pr,bool detenerSiEncuentraPatronesAlFinal = true)
			: base(pr)
		{
			this.I0 = 0;
			this.detenerSiEncuentraPatronesAlFinal=detenerSiEncuentraPatronesAlFinal;
			
			//this.patronesAlFinal=new PatronesSeriesAlFinal(re);
		}
        protected override void initD()
		{
			if (d == null) {
				d = new DatosDeNombreCapituloDelPrincipio();
			}
			alIniciarD();
		}
		public DatosDeNombreCapituloDelPrincipio getD()
		{
			return (DatosDeNombreCapituloDelPrincipio)this.d;
		}
		
		public  DatosDeNombreCapituloDelPrincipio getCapitulosDeNombreDelPrincipio()
		{
			if (this.seBusco) {
				return this.getD();
			}
//			this.nombre = nombre;
			//this.I0 = I0;
			this.seBusco = true;
			
			DatosDeNombreCapituloDelPrincipio dd = null;

            //if (nombre == "86")
            //{
            //    cwl("aqui");
            //}


            //Patron Solo Numero
            if (esNumero(nombre)) {
				//cwl("nombre="+nombre);
				//algo de que es solo un numero
				int numeroDeSerie = inT(nombre);
				
				//if (esNombreNumericoSimple_ModificaContexto(numeroDeSerie)) {
				
				if(esNombreNumericoSimple_ModificaContexto(numeroDeSerie)){
					return null;
				}
				getCtxCn().agregarPropiedadesAContextoBasicasDeResultado();
				getCtxCn().agregarPropiedadesAContextoHAY_SOLO_NUMEROS();
				
				
				if (dd == null) {
					dd = new DatosDeNombreCapituloDelPrincipio();
				}
				dd.CapituloStr = nombre;
				//dd.Capitulo = numeroDeSerie;
				dd.IndiceNumeroCapitulo = 0;
				dd.IndiceDeInicioDespuesDeLosNumeros = numeroDeSerie.ToString().Length;
				dd.EsSoloNumeros = true;
				
				return dd;
			}
			
			//Patrones que comienzan con un numero
			if (nombre.Length > 0 && Char.IsNumber(nombre.ElementAt(0))) {
				//cwl("nombre="+nombre);
				int numeroInicial = Matchs.getNumeroInicialEntero(nombre);
				//Seccion comprobar que el numero se puede ignorar, para arrora tiempo   (804)
				{
                    //Caso el primer numero hay que ignorarlo
                    //Match mConSeparacion = Matchs.R_N.ReInicialS.Match(nombre);
                    Match mConSeparacion = this.pr.re.Re_NC.ReInicialS.Match(nombre);

                    //------------------
                    //Group gNC = this.pr.re.getGrupoNumeroCapitulo(mConSeparacion);
                    //CaptureCollection cl = gNC.Captures;
                    //Capture cN = cl[0];
                    //int capitulo = inT_Cap(cN);
                    //------------------------

                    if (esNombreNumericoSimple_ModificaContexto(numeroInicial))
                    {
                        //if (nombre == "86-2nd-season-1")
                        //{
                        //    cwl("aqui");
                        //}

                        DatosDeNombreCapituloDelFinal df = pr.buscarPatronesAlFinal(mConSeparacion.Length);//(mConSeparacion.Length);
                        if (df!=null) {
                            int indiceDelFinalDelNombre = df.IndiceDelFinalDeNombre;
                            int cantidadMaximaDeCaracteresIntermedios = 3;

                            //if (nombre == "86-2nd-season-1")
                            //{
                            //    bool a = indiceDelFinalDelNombre >= mConSeparacion.Length;
                            //    bool b = indiceDelFinalDelNombre <= mConSeparacion.Length + cantidadMaximaDeCaracteresIntermedios;
                            //    cwl("aqui");
                            //}
                            // mConSeparacion.Length
                            if (indiceDelFinalDelNombre>=numeroInicial.ToString().Length&& indiceDelFinalDelNombre<= mConSeparacion.Length+ cantidadMaximaDeCaracteresIntermedios) {
                                if (dd == null)
                                {
                                    dd = new DatosDeNombreCapituloDelPrincipio();
                                }
                                dd.DatosDelFinal = df;
                                return dd;
                            }
                            
                        }
                    }


                    initD();
					DatosDeIgnorarNumero di = getEs_IgnorarNumeroDelanteDe( numeroInicial, mConSeparacion);
					if (di != null) {
						return null;
					}
					this.d=null;
					
					//TN N-N 
					mx = this.pr.re.Re_NT_N_Union_N_Repetir.ReInicialS.Match(nombre, getIO());
					alEncontrarPatron(mx, () => {
						capturar_Temporada_NT(mx); 
						bool capturo = buscarUnionAlPrincipio() || capturar_Capitulo_AlFinal(mx);
						if (!capturo) {
							Group gNC = this.pr.re.getGrupoNumeroCapitulo(mx);
							CaptureCollection cl = gNC.Captures;
							Capture cN = cl[0];
							int capitulo = d.Temporada;
							int indiceInicialCapitulo=d.IndiceNumeroTemporada;
							int indiceAcontinuacion = cN.Index;
							DatosDeIgnorarNumero ignorar = getEs_IgnorarNumeroDelanteDe(
								numeroFinal: capitulo
								,indiceInicialNumero:indiceInicialCapitulo
								,indiceAContinuacion: indiceAcontinuacion);
							if (ignorar == null
							    && (!pr.estaDentroDeFecha(cN))) {
								getD().IndiceNumeroCapitulo = getD().IndiceNumeroTemporada;
								getD().CapituloStr = getD().TemporadaStr;
						
							
								getD().IndiceNumeroTemporada = -1;
								getD().Temporada = -1;
								getD().IndiceDeInicioDespuesDeLosNumeros = indiceAcontinuacion;
							} else {
								d = null;
							}
							
							
						}
					});
					if (d != null) {
						return getD();
					}
					
					//NxN UnionN
					//NTxE_NC UnionN   1xE1 ConstantesExprecionesRegulares.patronSNxEN
					// S04E UnionN
					// S04xE UnionN
					mx = getMatchCoincidente(arrgR(
						this.pr.re.Re_NxN_Union_N_Repetir_PosiblesEspaciosInternos.ReInicialS
				, this.pr.re.Re_NxE_N_Union_N_Repetir.ReInicialS
				, this.pr.re.Re_SNxE_N_Union_N_Repetir.ReInicialS
				, this.pr.re.Re_SNE_N_Union_N_Repetir.ReInicialS
					));
					alEncontrarPatron(mx, () => {
						Group gNt = this.pr.re.getGrupoNumeroTemporada(mx);
						getD().IndiceNumeroTemporada = gNt.Index;
						//getD().Temporada = inT_Grp(gNt);
						getD().TemporadaStr = gNt.ToString();
						if (esIgnorarNumeroDetrasDe(
							getD().Temporada
							, getD().IndiceNumeroTemporada)) {
							d = null;
						
						} else {
							if (!buscarUnionAlPrincipio()
							    && !capturar_Capitulo_AlFinal(mx)) {
								getD().IndiceNumeroCapitulo = getD().IndiceNumeroTemporada;
								getD().CapituloStr = getD().TemporadaStr;
						
							
								getD().IndiceNumeroTemporada = -1;
								getD().Temporada = -1;
//								Group gNC =this.pr.re.getGrupoNumeroCapitulo();
//								Capture cN=gNC.Captures[0];
								Group gIC = this.pr.re.getGrupoIdentificadorCapitulo(mx);
								
								getD().IndiceDeInicioDespuesDeLosNumeros = gIC.Index;
						
							} else {
								capturar_Temporada_NT(mx);
							}
					
						}
					});
					if (d != null) {
						return getD();
					}
					
					//NxN
					//NTxE_NC   1xE1 ConstantesExprecionesRegulares.patronSNxEN
					// S04E01
					// S04xE01
					mx = getMatchCoincidente(arrgR(
						this.pr.re.Re_NxN_PosiblesEspaciosInternos.ReInicialS
				, this.pr.re.Re_NxEN.ReInicialS
				, this.pr.re.Re_SNEN.ReInicialS
				, this.pr.re.Re_SNxEN.ReInicialS
					));
					alEncontrarPatron(mx, () => {
						Group gNt = this.pr.re.getGrupoNumeroTemporada(mx);
						getD().IndiceNumeroTemporada = gNt.Index;
						//getD().Temporada = inT_Grp(gNt);
						getD().TemporadaStr = gNt.ToString();
						if (esIgnorarNumeroDetrasDe(
							getD().Temporada, getD().IndiceNumeroTemporada)) {
							d = null;
						
						} else {
							if (!capturar_Capitulo_AlFinal(mx)) {
								getD().IndiceNumeroCapitulo = getD().IndiceNumeroTemporada;
								//getD().Capitulo = getD().Temporada;
								getD().CapituloStr = getD().TemporadaStr;
							
								getD().IndiceNumeroTemporada = -1;
								getD().Temporada = -1;
						
								Group gIC = this.pr.re.getGrupoIdentificadorCapitulo(mx);
								
								getD().IndiceDeInicioDespuesDeLosNumeros = gIC.Index;
							} else {
								capturar_Temporada_NT(mx);
							}
						}
				
				
					});
					if (d != null) {
						return getD();
					}
					
					//NT_Temporada_N UnionN
					mx = this.pr.re.Re_NT_Temporada_N_Union_N_Repetir.ReInicialS.Match(nombre, getIO()); 
					alEncontrarPatron(mx, () => {
						capturar_Temporada_NT(mx);
						if (!buscarUnionAlPrincipio()) {
							if (!capturar_Capitulo_AlFinal(mx)) {
							
								int capitulo = d.Temporada;
								int indiceInicialCapitulo=d.IndiceNumeroTemporada;
								int indiceAcontinuacion = d.IndiceIdentificadorTemporada;
								DatosDeIgnorarNumero ignorar = getEs_IgnorarNumeroDelanteDe(
									numeroFinal:capitulo
									,indiceInicialNumero:indiceInicialCapitulo
									,indiceAContinuacion: indiceAcontinuacion);
								if (ignorar == null
								    && (!pr.estaDentroDeFecha(d.IndiceNumeroTemporada))) {
									getD().IndiceNumeroCapitulo = getD().IndiceNumeroTemporada;
									getD().CapituloStr = getD().TemporadaStr;
						
							
									getD().IndiceNumeroTemporada = -1;
									getD().Temporada = -1;
									getD().IndiceIdentificadorTemporada = -1;
									getD().IdentificadorTemporadaStr = "";
									getD().IndiceDeInicioDespuesDeLosNumeros = indiceAcontinuacion;
								} else {
									d = null;
								}   	
							}
						}
						
						
					});
					if (d != null) {
						return getD();
					}
					
					//NT_Temporada_NC
					mx = this.pr.re.Re_NT_Temporada_NC.ReInicialS.Match(nombre, getIO());
					alEncontrarPatron(mx, () => {
						capturar_Temporada_NT(mx);
						if (!capturar_Capitulo_AlFinal(mx)) {
							
							int capitulo = d.Temporada;
							int indiceInicialCapitulo=d.IndiceNumeroTemporada;
							int indiceAcontinuacion = d.IndiceIdentificadorTemporada;
							DatosDeIgnorarNumero ignorar = getEs_IgnorarNumeroDelanteDe(
								numeroFinal:capitulo
								,indiceInicialNumero:indiceInicialCapitulo
								,indiceAContinuacion: indiceAcontinuacion);
							if (ignorar == null
							    && (!pr.estaDentroDeFecha(d.IndiceNumeroTemporada))) {
								getD().IndiceNumeroCapitulo = getD().IndiceNumeroTemporada;
								getD().CapituloStr = getD().TemporadaStr;
						
							
								getD().IndiceNumeroTemporada = -1;
								getD().Temporada = -1;
								getD().IndiceIdentificadorTemporada = -1;
								getD().IdentificadorTemporadaStr = "";
								getD().IndiceDeInicioDespuesDeLosNumeros = indiceAcontinuacion;
							} else {
								d = null;
							}   	
						}
						
					});
					if (d != null) {
						return getD();
					}
					
					
					
					//N_UnionN_......_Temporada_NT_
					//N_UnionN_......_2da Temporada
					mx = getMatchCoincidente(arrgR(
						this.pr.re.Re_N_Union_N_Repetir_aaaaaaaa_Temporada_NT.ReInicialS
						, this.pr.re.Re_N_Union_N_Repetir_aaaaaaaa_NT_IT_Temporada.ReInicialS
					));
					alEncontrarPatron(mx, () => {
						capturar_Temporada_NT(mx); 
						if (buscarUnionAlPrincipio()) {
							if (getD().IndiceDeInicioDespuesDeLosNumeros == -1) {
								Group Gcc = this.pr.re.getGrupoContenidoCapitulo(mx);
								getD().IndiceDeInicioDespuesDeLosNumeros = Gcc.Index;
							}
						} else {
							if (!capturar_Capitulo_AlFinal(mx)) {
								d = null;
							}
						}
						
					});
					if (d != null) {
						return getD();
					}
					
					//N_......_Temporada_NT_
					//N_......_2da Temporada
					mx = getMatchCoincidente(arrgR(
						this.pr.re.Re_N_aaaaaaaa_Temporada_NT.ReInicialS
						, this.pr.re.Re_N_aaaaaaaa_NT_IT_Temporada.ReInicialS
					));
					alEncontrarPatron(mx, () => {
						capturar_Temporada_NT(mx);
						if (!capturar_Capitulo_AlFinal(mx)) {
							d = null;   	
						}
						
					});
					if (d != null) {
						return getD();
					}
					
					//N_
					mx = this.pr.re.Re_NC.ReInicialS.Match(nombre, getIO());
					alEncontrarPatron(mx, () => {
						if (!capturar_Capitulo_AlFinal(mx)) {
							d = null;   	
						}
					});
					if (d != null) {
						return getD();
					}






				}//end if si comienza con un caracter numero
				
				
			}
		
			
			return null;
		}
		private bool buscarUnionAlPrincipio(int iNumeroInicial = 0)
		{
			return buscarDatosUnion(
			                                dd: this.d
			                                , mm: this.mx
			                                , iNumeroInicial: iNumeroInicial
			                               , alDescartarUltimoNumero: (c, indiceAcontinuacion) => {
				getD().IndiceDeInicioDespuesDeLosNumeros = indiceAcontinuacion;
			});
		}
		private DatosDeNombreCapituloDelPrincipio alEncontrarPatron(Match mm, Action usarDatosFinal)
		{
			if (mm.Success) {
				initD();
				usarDatosFinal();
				if (d != null) {
					//si no le di este datos hantes es para ponerlo ahora
					if (getD().IndiceDeInicioDespuesDeLosNumeros == -1) {
						//asumo que la mm ya tiene las separaciones
						getD().IndiceDeInicioDespuesDeLosNumeros = mm.Length;
					}
					getCtxCn().agregarPropiedadesAContextoBasicasDeResultado();
					if (nombre.Trim() == mm.ToString()) {
						d.EsSoloNumeros = true;
						getCtxCn().agregarPropiedadesAContextoHAY_SOLO_NUMEROS();
					}
					return getD();
				}
			}
			return null;
		}
		
	}
	
	
}

//					for (int i = 0; i < 3; i++) {
//						DatosNombreNumerico dn = null;
//						switch (i) {
//							case 0:
//								//Se busca por si es un nombre numerico simple 
//								dn = esNombreNumericoSimpleDesdeELPrincipio_ModificaContexto( numeroInicial);
//								break;
//							case 1:
//								dn = esNombreRodeadoDeNumeros_AlPrincipio_ModificaContexto_Dn(numeroInicial,detenerSiEncuentraPatronesAlFinal);
//								break;
//							case 2:
//								//Se busca por si es un nombre numerico multiple
//								dn = esNombresNumericosCompletosMultiplesDesdeElPrincipio_ModificaContexto(0, detenerSiEncuentraPatronesAlFinal);
//								break;
//						}
//						
//						if (dn != null) {
//							//Se compreba si tiene los datos del capitulo al final
//							//pq esto confirmaria que el numero inicial es parte del nombre
//							// y no es informacion del capitulo
//							// Y si se encuentra la informacion al final, se aprobecha y se informa
//							//para ahorrar tiempo
//							if (detenerSiEncuentraPatronesAlFinal && dn.D != null) {
//								dd = new DatosDeNombreCapituloDelPrincipio();
//								dd.DatosDelFinal = dn.D;
//								return dd;
//							}
//							//Se comprobo que si era un nombre numerico simple plq se puede ignorar
//							//este numero
//							if (dn.Indice != -1) {
//								return null;
//							}
//					
//						}//fin del if si es un nombre numerico simple
//					
//					}
//					
//					//Se busca por si es un nombre numerico simple 
//					ProcesadorDeNombreDeSerie.DatosNombreNumerico dn = this.pr.esNombreNumericoSimpleDesdeELPrincipio_ModificaContexto(nombre, numeroInicial);
//					
//					if (dn == null) {
//						dn = this.pr.esNombreRodeadoDeNumeros_AlPrincipio_ModificaContexto_Dn(detenerSiEncuentraPatronesAlFinal);
//					}
					
					
					
					
					
//					if (getConf().NombresRodeadosDeNumeros != null) {
//						int ind = Utiles.startsWith_Indice(nombre, getConf().NombresRodeadosDeNumeros);
//						
//						
//						if (ind != -1) {
//							DatosDeNombreCapituloDelFinal df = null;
//							
////							HistoriarDeBusqueda h = this.pr.historialDeBusqueda;
////							df = h.buscarPatronesAlFinal(nombre, 0);
////							
//							df = this.pr.buscarPatronesAlFinal( 0);
//							if (detenerSiEncuentraPatronesAlFinal&&df != null) {
//								dd = new DatosDeNombreCapituloDelPrincipio();
//								dd.DatosDelFinal = df;
//								return dd;
//							}
//						
//							if (getCtx().EsVideo) {
//								//la idea es que si es un video debe de contener al menos
//								//la informacion de los capitulos plq si es igual al NombresRodeadosDeNumeros
//								//seria solo coincidencia pq este numero seria realmente parte
//								//de la informacion del capitulo
//								if (nombre.Trim() != getConf().NombresRodeadosDeNumeros[ind]) {
//									//agregar al contexto
//									return null;
//								}	
//							} else {
//								//agregar al contexto
//								return null;
//							}
//						
//						
//						}
//					}//fin si hay cf.NombresRodeadosDeNumeros 
//					
//					//Se busca por si es un nombre numerico multiple
//					ProcesadorDeNombreDeSerie.DatosNombreNumerico dn2 = this.pr.esNombresNumericosCompletosMultiplesDesdeElPrincipio_ModificaContexto(nombre, 0);
//					if (dn2 != null) {
//						if (dn2.D != null) {
//							dd = new DatosDeNombreCapituloDelPrincipio();
//							dd.DatosDelFinal = dn2.D;
//							return dd;
//						}
//						if (dn2.Indice != -1) {
//							return null;
//						}
//				
//					}
					