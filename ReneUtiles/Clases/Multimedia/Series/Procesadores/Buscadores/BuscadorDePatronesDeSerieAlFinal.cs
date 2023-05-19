/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 28/7/2022
 * Hora: 17:05
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

using ReneUtiles.Clases.ExprecionesRegulares.IdentificacionesNumericas;

namespace ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores
{
    /// <summary>
    /// Description of PatronesSeriesAlFinal.
    /// </summary>
    public class BuscadorDePatronesDeSerieAlFinal : BuscadorDeDatosDeSerie
    {
        //		private DatosDeNombreCapituloDelFinal d;
        //private Match mx;
        //		private bool seBusco;

        //public PatronesSeriesAlFinal(RecursosDePatronesDeSeries re):base(re)
        public BuscadorDePatronesDeSerieAlFinal(ProcesadorDeNombreDeSerie pr) : base(pr)
        {
            //this.re = re;
            //this.d = null;
            //			this.seBusco=false;


        }
        protected override void initD()
        {
            if (d == null)
            {
                d = crearDatosDeNombreCapituloDelFinal();
            }
            alIniciarD();
        }

        private DatosDeNombreCapituloDelFinal alEncontrarPatron(Match mm, Action usarDatosFinal)
        {
            if (mm.Success)
            {
                initD();
                usarDatosFinal();//ref d
                if (getD() != null)
                {
                    getD().IndiceDelFinalDeNombre = mm.Index;
                    //getCtxCn().agregarPropiedadesAContextoBasicasDeResultado();

                    Match mSeparaciones = mm; //pq asumo que mm incluye las separaciones al principio y al final

                    //caso que el nombre solo incluye la informacion numerica
                    if (nombre == mSeparaciones.ToString()
                        || (getConf().EsParaAnime && nombreSinOva == mSeparaciones.ToString()))
                    {
                        getD().EsSoloNumeros = true;
                        //getCtxCn().agregarPropiedadesAContextoHAY_SOLO_NUMEROS();
                    }
                    else
                    {
                        //caso en que es normal (info nombre + numerica)
                        //getCtxCn().agregarPropiedadesAContextoHAY_NOMBRES_NORMALES();
                    }
                    return getD();
                }//fin del if d != null

            }//fin del if mm.Succes
            d = null;
            return null;
        }
        public DatosDeNombreCapituloDelFinal getD()
        {
            return (DatosDeNombreCapituloDelFinal)this.d;
        }


        private DatosDeNombreCapituloDelFinal alEncontrarPatronContenedor_DeTemporadas_MismaSerie(Match mm, Action accion)
        {
            return alEncontrarPatron(mm, () =>
            {
                accion();
                if (getD() != null)
                {

                    agregarContenedorDe_Temporadas_MismaSerie();
                       
                }

            });
        }


        private DatosDeNombreCapituloDelFinal alEncontrarPatronContenedor_DeCapitulos_MismaSerie(Match mm, Action accion)
        {
            return alEncontrarPatron(mm, () =>
            {
                accion();
                if (getD() != null)
                {
                    //si no es una carpeta (supongo no siendo un video o siendo solo numero)
                    //le paso los datos adquiridos a la informacion de que es un capitulo
                    //para evitar errores
                    if (getCtx().EsVideo || getCtx().EsSoloNombre)
                    {
                        getD().cambiarDe_ConjuntoDeCapitulos_A_Capitulo();
                        //if (!getD().EsConjuntoDeCapitulos)
                        //{
                        //    getD().Capitulo = getD().CantidadDeCapitulosQueContiene;
                        //    getD().IndiceNumeroCapitulo = getD().IndiceDeNumeroCantidadDeCapitulosQueContiene;

                        //    getD().CantidadDeCapitulosQueContiene = -1;
                        //    getD().IndiceDeNumeroCantidadDeCapitulosQueContiene = -1;
                        //}
                        //else
                        //{

                        //}

                        //getD().IndiceIdentificadorCapitulo = getD().IndiceIdentificadorCapitulos;




                        //getD().IndiceIdentificadorCapitulos = -1;
                    }
                    else
                    {
                        agregarContenedorDe_Capitulos_MismaSerie();
                        //						getD().EsContenedorDeTemporada = true;
                        //						getCtxCn().agregarPropiedadesAContextoHAY_CONTENEDORES_TEMPORADAS();
                    }

                }

            });
        }


        private DatosDeNombreCapituloDelFinal alEncontrarPatronContenedor_DeCapitulos_DeMismaTemporada(Match mm, Action accion)
        {
            return alEncontrarPatron(mm, () =>
            {
                accion();
                if (getD() != null)
                {
                    //si no es una carpeta (supongo no siendo un video o siendo solo numero)
                    //le paso los datos adquiridos a la informacion de que es un capitulo
                    //para evitar errores
                    if (getCtx().EsVideo || getCtx().EsSoloNombre)
                    {
                        getD().cambiarDe_ConjuntoDeCapitulos_A_Capitulo();
                        //if (!getD().esConjuntoDeCapitulos())
                        //{

                        //    //getD().Capitulo = getD().CantidadDeCapitulosQueContiene;
                        //    //getD().IndiceNumeroCapitulo = getD().IndiceDeNumeroCantidadDeCapitulosQueContiene;

                        //    //getD().CantidadDeCapitulosQueContiene = -1;
                        //    //getD().IndiceDeNumeroCantidadDeCapitulosQueContiene = -1;
                        //}
                        //else
                        //{

                        //}

                        //getD().IndiceIdentificadorCapitulo = getD().IndiceIdentificadorCapitulos;




                        //getD().IndiceIdentificadorCapitulos = -1;
                    }
                    else
                    {
                        agregarContenedorDe_CapitulosDe_MismaTemporada();
                        //						getD().EsContenedorDeTemporada = true;
                        //						getCtxCn().agregarPropiedadesAContextoHAY_CONTENEDORES_TEMPORADAS();
                    }

                }

            });
        }

        private bool capturar_NC_CapituloOva_alPrincipio(Match mm)
        {
            Group gNC = this.pr.re.getGrupoNumeroOva(mm);
            if (gNC.Success)
            {
                try
                {


                    int capitulo = inT_Grp(gNC);
                    if ((!esIgnorarNumeroDetrasDe(numero: capitulo
                                                  , indiceInicialDelNumero: gNC.Index))
                        && (!pr.estaDentroDeFecha(gNC)))
                    {
                        capturar_CapituloOVA_NC(mm);

                        return true;
                    }

                }
                catch (Exception ex)
                {
                    cwl("error cadena");

                }
            }
            

            return false;
        }
        private bool capturar_NC_Capitulo_alPrincipio(Match mm)
        {
            Group gNC = this.pr.re.getGrupoNumeroCapitulo(mm);
            if (gNC.Success) {
                try
                {


                    int capitulo = inT_Grp(gNC);
                    if ((!esIgnorarNumeroDetrasDe(numero: capitulo
                                                  , indiceInicialDelNumero: gNC.Index))
                        && (!pr.estaDentroDeFecha(gNC)))
                    {
                        capturar_Capitulo_NC(mm);
                        return true;
                    }

                }
                catch (Exception ex)
                {
                    cwl("error cadena");
                }

            }

            return false;
        }
        private bool capturar_Temporada_NT_alFinal(Match mm)
        {
            Group gNT = this.pr.re.getGrupoNumeroTemporada(mm);
            int temporada = inT_Grp(gNT);
            DatosDeIgnorarNumero ignorar = getEs_IgnorarNumeroDelanteDe(
                 temporada, mm);//mm.Index + mm.Length
            if (ignorar == null
                && (!pr.estaDentroDeFecha(gNT))
               )
            {
                capturar_Temporada_NT(mm);
                return true;
            }
            return false;
        }
        private bool capturar_NT_Temporada_alPrincipio(Match mm)
        {
            Group gNt = this.pr.re.getGrupoNumeroTemporada(mm);


            getD().setIdentificacionTemporada_Numero(
                indiceDeRepresentacionStr: gNt.Index
                , representacionStr: gNt.ToString()
                );

            //getD().IndiceNumeroTemporada = gNt.Index;
            //getD().Temporada = inT_Grp(gNt);
            if (!esIgnorarNumeroDetrasDe(getD().IdenificadorTemporada.identificacionNumerica.Numero//getD().Temporada
                                          , getD().IdenificadorTemporada.identificacionNumerica.IndiceDeRepresentacionStr//getD().IndiceNumeroTemporada
                                          )
               && (!pr.estaDentroDeFecha(gNt))
               )
            {
                capturar_Temporada_NT(mm);
                return true;
            }
            return false;

        }
        

        private void setCapturoOva(Match mx) {
            Group gIO = this.pr.re.getGrupoIdentificadorOva(mx);

            getD().setEsOva();

            getD().setTagOva(
                indiceDeRepresentacionStr_etiquetaOva: gIO.Index
                , representacionStr_etiquetaOva: gIO.ToString()
                );
        }


        public DatosDeNombreCapituloDelFinal buscarPatronesAlFinal(int I0)
        {
            if (this.seBusco && this.I0 <= I0)
            {
                return this.getD();
            }
            //			if(this.seBusco&&this.nombre==nombre&&this.I0 == I0){
            //				return this.getD();
            //			}
            //			this.nombre = nombre;
            this.I0 = I0;
            this.seBusco = true;

            //Match mx = null;

            //en este se va a guardar la parte del nombre a continuacion de la palabra ova
            //pq se sule tener los datos del capitulo despues de esta, no antes
            //y ademas sirve para confirmar sino hay nada util despues (osea pudira ser que 
            //el capitulo solo tuviera "ova" y no diera informacion  numerica)
            //string nombreSinOva = nombre;
            if (getConf().EsParaAnime)
            {//(1320)
             //patron comienza con "ova"
             //se recorta el nombre para que mi captura total solo contenga la parte siguiente del nombre 
             //desde la palabra "ova"
                Match mOva = this.pr.re.Re_Ova.ReInicialSf.Match(nombre.Substring(I0));
                if (mOva.Success)
                {
                    //compruebo si este capitulo solo esta compuesta por la palabra ova
                    // si es asi lo clasifico como solo numero ,Ejm "ova.mp4"
                    if (mOva.ToString() == nombre.ToString())
                    {
                        initD();
                        //getD().Capitulo = 1;
                        //getD().EsOVA = true;
                        getD().EsSoloNumeros = true;
                        Group gIO = this.pr.re.getGrupoIdentificadorOva(mOva);
                        //getD().IdentificadorDeOVAStr = gIO.ToString();
                        //getD().IndiceIdentificadorDeOVA = gIO.Index;
                        getD().setIdentificacionOva_TagOVA(
                            indiceDeRepresentacionStr_tagOVA: gIO.Index
                            , representacionStr_tagOVA: gIO.ToString()
                            );
                        return getD();
                    }
                    try {
                        //Guardo la seccion sin ova pq va a ser usada despues
                        //para comprobar en caso de que se cumpla con algun otro patron al final
                        //que tal ves es solo un ova Ejem "ova 1.mp4"
                        //nombreSinOva = nombreSinOva.Substring(mOva.Length + I0);
                        nombreSinOva = nombre.Substring(mOva.Length + I0);
                    } catch (Exception ex) {
                        cwl("errror aqui 2");
                        throw ex;
                    }
                    
                }

            }//fin del if es para anime

            // [Temp 1][Capi UnionN]
            //[Capi UnionN][Temp 1] 
            //2nd Season Episodio 12 UnionN 
            mx = getMatchCoincidente(arrgR(
                this.pr.re.Re_Temporada_NT_Capitulo_N_Union_N_Repetir.SSfReS
                , this.pr.re.Re_Capitulo_N_Union_N_Repetir_Temporada_NT.SSfReS
                , this.pr.re.Re_NT_IT_Temporada_Capitulo_N_Union_N_Repetir.SSfReS
            ));
            alEncontrarPatron(mx, () =>
            {
                capturar_Temporada_NT(mx);
                if (!buscarDatosUnion(d, mx))
                {
                    capturar_Capitulo_NC(mx);
                }
            });


            if (getD() != null)
            {
                return getD();
            }

            //Westworld [Temporada 1] [Cap.10] [1080p] [Dual Audio]
            //Woo, una abogada extraordinaria [Temporada 1] [Cap.16] [1080p] [Dual Audio] [14,12 Gb]
            mx = getMatchCoincidente(arrgR(
                this.pr.re.Re_Temporada_NT_CapitulosPluP_NC.SSfReS

            ));

            alEncontrarPatronContenedor_DeCapitulos_DeMismaTemporada(mx, () =>
            {
                //cwl(this.pr.re.Re_Temporada_NT_CapitulosPluP_NC.SSfReS);
                Group gICnp = this.pr.re.getGrupoIdentificadorCantidadCapitulo(mx);
                Group gCnp = this.pr.re.getGrupoNumeroCantidadCapitulo(mx);

                getD().setIdentificacion_ConjuntoDeCapitulos_EtiquetaNumero(
                 indiceDeRepresentacionStr_etiqueta: gICnp.Index
                , representacionStr_etiqueta: gICnp.ToString()
                , indiceDeRepresentacionStr_numeroCantidad: gCnp.Index
                , representacionStr_numeroCantidad: gCnp.ToString()
                );



                //getD().IndiceDeNumeroCantidadDeCapitulosQueContiene = gCnp.Index;
                //getD().IndiceIdentificadorCapitulos = gICnp.Index;
                //getD().IdentificadorCapitulosStr = gICnp.ToString();

                //getD().CantidadDeCapitulosQueContieneStr = gCnp.ToString();
                capturar_Temporada_NT(mx);
            });


            if (getD() != null)
            {
                return getD();
            }

            // [Temp 1][Capi 2] lo normal
            // [Capi 2][Temp 1]
            //2nd Season Episodio 12 
            //cwl(this.pr.re.Re_NT_IT_Temporada_Capitulo_NC.SSfReS);
            mx = getMatchCoincidente(arrgR(
                this.pr.re.Re_Temporada_NT_Capitulo_NC.SSfReS
                , this.pr.re.Re_Capitulo_NC_Temporada_NT.SSfReS
                , this.pr.re.Re_NT_IT_Temporada_Capitulo_NC.SSfReS
            ));

            alEncontrarPatron(mx, () =>
            {
                capturar_Capitulo_NC(mx);
                capturar_Temporada_NT(mx);
            });

            if (getD() != null)
            {
                return getD();
            }//1484



            



            if (getConf().EsParaAnime)
            {
                //Seitokai Yakuindomo OVA 8 Union N
                mx = this.pr.re.Re_Ova_N_Union_N_Repetir.SSfReS.Match(nombre, I0);
                alEncontrarPatron(mx, () =>
                {
                    bool capturo = buscarDatosUnion(d, mx) || capturar_Capitulo_AlFinal(mx);
                    if (capturo)
                    {
                        setCapturoOva(mx);
                        //Group gIO = this.pr.re.getGrupoIdentificadorOva(mx);

                        //getD().IndiceIdentificadorDeOVA = gIO.Index;
                        //getD().EsOVA = true;

                        //getD().IdentificadorDeOVAStr = gIO.ToString();
                    }
                    else
                    {
                        d = null;
                    }


                });

                if (getD() != null)
                {
                    return getD();
                }
                //Seitokai Yakuindomo OVA 8
                mx = this.pr.re.Re_Ova_N.SSfReS.Match(nombre, I0);
                alEncontrarPatron(mx, () =>
                {
                    if (capturar_Capitulo_AlFinal(mx))
                    {
                        setCapturoOva(mx);
                        //Group gIO = this.pr.re.getGrupoIdentificadorOva(mx);
                        //getD().IndiceIdentificadorDeOVA = gIO.Index;
                        //getD().EsOVA = true;

                        //getD().IdentificadorDeOVAStr = gIO.ToString();
                    }
                    else
                    {
                        d = null;
                    }
                });

                if (getD() != null)
                {
                    return getD();
                }//1536


                //cwl(this.pr.re.Re_NTR_Capitulo_NC.SSfReS);
                //Overlord IV Episodio 2
                mx = this.pr.re.Re_NTR_Capitulo_NC.SSfReS.Match(nombre, I0);
                alEncontrarPatron(mx, () =>
                {
                    Group Gnrt = this.pr.re.getGrupoNumeroRomanoTemporada(mx);
                    //getD().EsTemporadaNumerosRomanos = true;
                    //getD().IndiceTemporadaNumerosRomanos = Gnrt.Index;
                    //getD().TemporadaNumerosRomanosStr = Gnrt.ToString();
                    getD().setIdentificacionTemporada_NumeroRomanao(
                        indiceDeRepresentacionStr: Gnrt.Index
                        ,representacionStr: Gnrt.ToString()
                        );
                    capturar_Capitulo_NC(mx);


                });

                if (getD() != null)
                {
                    return getD();
                }

                //a jin II 01 Union N
                mx = this.pr.re.Re_NTR_N_Union_N_Repetir.SSfReS.Match(nombre, I0);
                alEncontrarPatron(mx, () =>
                {
                    Group Gnrt = this.pr.re.getGrupoNumeroRomanoTemporada(mx);

                    //getD().EsTemporadaNumerosRomanos = true;
                    //getD().IndiceTemporadaNumerosRomanos = Gnrt.Index;
                    //getD().TemporadaNumerosRomanosStr = Gnrt.ToString();
                    getD().setIdentificacionTemporada_NumeroRomanao(
                        indiceDeRepresentacionStr: Gnrt.Index
                        , representacionStr: Gnrt.ToString()
                        );
                    if (buscarDatosUnion(d, mx)) {
                        agregarContenedorDe_CapitulosDe_MismaTemporada();
                    } else {
                        capturar_Capitulo_AlFinal(mx);
                    }
                    

                    //bool capturo = buscarDatosUnion(d, mx) || capturar_Capitulo_AlFinal(mx);
                    //if (!capturo)
                    //{
                    //    agregarContenedorDe_CapitulosDe_MismaTemporada();

                    //    //if ((getCtx().EsCarpeta || ((!getCtx().EsVideo) && (!getCtx().EsArchivo)))
                    //    //    //&& getD().Temporada > 1
                    //    //    && getD().hayNumeroTemporada_NoCantidad()
                    //    //    )
                    //    //{
                    //    //    //agregarContenedor();
                    //    //    agregarContenedorDe_CapitulosDe_MismaTemporada();
                    //    //}
                    //    //else
                    //    //{
                    //    //    d = null;
                    //    //}
                    //}


                });

                if (getD() != null)
                {
                    return getD();
                }

                //a jin II 01
                mx = this.pr.re.Re_NTR_NC.SSfReS.Match(nombre, I0);
                alEncontrarPatron(mx, () =>
                {
                    Group Gnrt = this.pr.re.getGrupoNumeroRomanoTemporada(mx);

                    //getD().EsTemporadaNumerosRomanos = true;
                    //getD().IndiceTemporadaNumerosRomanos = Gnrt.Index;
                    //getD().TemporadaNumerosRomanosStr = Gnrt.ToString();

                    getD().setIdentificacionTemporada_NumeroRomanao(
                        indiceDeRepresentacionStr: Gnrt.Index
                        , representacionStr: Gnrt.ToString()
                        );

                    if (!capturar_Capitulo_AlFinal(mx))
                    {
                        if ((getCtx().EsCarpeta || ((!getCtx().EsVideo) && (!getCtx().EsArchivo)))
                            && getD().hayNumeroTemporada_NoCantidad() )
                        {
                            //agregarContenedor();
                            agregarContenedorDe_CapitulosDe_MismaTemporada();
                        }
                        else
                        {
                            d = null;
                        }

                    }

                });

                if (getD() != null)
                {
                    return getD();
                }//1562






            }//fin si es para anime

            //Britannia [Temp 1] [Caps.09 UnionN]  generalmente en carpetas que almacenan toda una temporada
            //se ve el plural en el identificador de capitulos
            //(Temporada2) [08 UnionN Cap.] 
            mx = getMatchCoincidente(arrgR(
                this.pr.re.Re_Temporada_NT_CapitulosPlu_N_Union_N_Repetir.SSfReS
                , this.pr.re.Re_Temporada_NT_N_Union_N_Repetir_CapitulosPlu.SSfReS
            ));

            alEncontrarPatronContenedor_DeCapitulos_DeMismaTemporada(mx, () =>
            {
                Group gICnp = this.pr.re.getGrupoIdentificadorCantidadCapitulo(mx);

                getD().setIdentificacion_ConjuntoDeCapitulos_Etiqueta(
                    indiceDeRepresentacionStr_etiqueta: gICnp.Index
                    , representacionStr_etiqueta: gICnp.ToString()
                    );


                //getD().IndiceIdentificadorCapitulos = gICnp.Index;
                //getD().IdentificadorCapitulosStr = gICnp.ToString();

                if (buscarDatosUnion(d, mx))
                {
                    //getD().CantidadDeCapitulosQueContiene = getD().CapituloFinal - getD().CapituloInicial;
                    
                }
                else
                {
                    

                    Group gCnp = this.pr.re.getGrupoNumeroCantidadCapitulo(mx);

                    getD().setIdentificacion_ConjuntoDeCapitulos_Numero(
                    indiceDeRepresentacionStr_numeroCantidad: gCnp.Index
                    , representacionStr_numeroCantidad: gCnp.ToString()
                    );


                    //getD().IndiceDeNumeroCantidadDeCapitulosQueContiene = gCnp.Index;
                    
                    //getD().CantidadDeCapitulosQueContieneStr = gCnp.ToString();
                }

                capturar_Temporada_NT(mx);
            });//1581

            if (getD() != null)
            {
                return getD();
            }




            //Britannia [Temp 1] [Caps.09]  generalmente en carpetas que almacenan toda una temporada
            //se ve el plural en el identificador de capitulos
            //(Temporada2) [08 Cap.] 
            mx = getMatchCoincidente(arrgR(
                this.pr.re.Re_Temporada_NT_CapitulosPlu_NC.SSfReS
                , this.pr.re.Re_Temporada_NT_NC_CapitulosPlu.SSfReS
            ));

            alEncontrarPatronContenedor_DeCapitulos_DeMismaTemporada(mx, () =>
            {
                Group gICnp = this.pr.re.getGrupoIdentificadorCantidadCapitulo(mx);
                Group gCnp = this.pr.re.getGrupoNumeroCantidadCapitulo(mx);

                getD().setIdentificacion_ConjuntoDeCapitulos_EtiquetaNumero(
                    indiceDeRepresentacionStr_etiqueta: gICnp.Index
                    , representacionStr_etiqueta: gICnp.ToString()
                    , indiceDeRepresentacionStr_numeroCantidad: gCnp.Index
                    , representacionStr_numeroCantidad: gCnp.ToString()
                    );

                //getD().IndiceDeNumeroCantidadDeCapitulosQueContiene = gCnp.Index;
                //getD().IndiceIdentificadorCapitulos = gICnp.Index;
                //getD().IdentificadorCapitulosStr = gICnp.ToString();
                
                //getD().CantidadDeCapitulosQueContieneStr = gCnp.ToString();
                capturar_Temporada_NT(mx);
            });


            if (getD() != null)
            {
                return getD();
            }


            //  (Miniserie) [5 UnionN Cap.] FDT   aqui    Miniserie es el identificador de temporada plq es un contenedor
            mx = this.pr.re.Re_Temporada_N_Union_N_Repetir_CapitulosPlu.SSfReS.Match(nombre, I0);
            alEncontrarPatronContenedor_DeCapitulos_DeMismaTemporada(mx, () =>
            {
                Group gICnp = this.pr.re.getGrupoIdentificadorCantidadCapitulo(mx);
                //!!!!

                //getD().IndiceIdentificadorCapitulos = gICnp.Index;
                //getD().IdentificadorCapitulosStr = gICnp.ToString();
                getD().setIdentificacion_ConjuntoDeCapitulos_Etiqueta(
                    indiceDeRepresentacionStr_etiqueta: gICnp.Index
                    , representacionStr_etiqueta: gICnp.ToString()
                    );

                if (buscarDatosUnion(d, mx))
                {
                    //getD().CantidadDeCapitulosQueContiene = getD().CapituloFinal - getD().CapituloInicial;
                }
                else
                {
                    Group gCnp = this.pr.re.getGrupoNumeroCantidadCapitulo(mx);
                    getD().setIdentificacion_ConjuntoDeCapitulos_Numero(
                    indiceDeRepresentacionStr_numeroCantidad: gCnp.Index
                    , representacionStr_numeroCantidad: gCnp.ToString()
                    );
                    //getD().IndiceDeNumeroCantidadDeCapitulosQueContiene = gCnp.Index;

                    //getD().CantidadDeCapitulosQueContieneStr = gCnp.ToString();
                }
                capturar_Temporada_NT(mx);
                //Group gIT = this.pr.re.getGrupoIdentificadorTemporada(mx);

                //getD().IndiceIdentificadorTemporada = gIT.Index;



            });
            if (getD() != null)
            {
                return getD();
            }
            //  (Miniserie) [5 Cap.] FDT   aqui    Miniserie es el identificador de temporada plq es un contenedor
            mx = this.pr.re.Re_Temporada_NC_CapitulosPlu.SSfReS.Match(nombre, I0);
            alEncontrarPatronContenedor_DeCapitulos_DeMismaTemporada(mx, () =>
            {
                Group gICnp = this.pr.re.getGrupoIdentificadorCantidadCapitulo(mx);
                Group gCnp = this.pr.re.getGrupoNumeroCantidadCapitulo(mx);

                //getD().IndiceIdentificadorCapitulos = gICnp.Index;
                //getD().IdentificadorCapitulosStr = gICnp.ToString();
                //getD().IndiceDeNumeroCantidadDeCapitulosQueContiene = gCnp.Index;
                //getD().CantidadDeCapitulosQueContieneStr = gCnp.ToString();
                getD().setIdentificacion_ConjuntoDeCapitulos_EtiquetaNumero(
                    indiceDeRepresentacionStr_etiqueta: gICnp.Index
                    , representacionStr_etiqueta: gICnp.ToString()
                    , indiceDeRepresentacionStr_numeroCantidad: gCnp.Index
                    , representacionStr_numeroCantidad: gCnp.ToString()
                    );


                Group gIT = this.pr.re.getGrupoIdentificadorTemporada(mx);
                capturar_Temporada_NT(mx);
                //getD().IndiceIdentificadorTemporada = gIT.Index;
                //getD().IdentificadorTemporadaStr = gIT.ToString();

            });
            if (getD() != null)
            {
                return getD();
            }//1617





            //Es Carpeta y solo en mangas  
            if (getConf().EsParaAnime 
                && (getCtx().EsCarpeta || ((!getCtx().EsVideo) && (!getCtx().EsArchivo))))
            {

                //Seitokai Yakuindomo 2nd Season 13 - 1 OVA
                mx = this.pr.re.Re_NT_IT_Temporada_NCan_N_Ova.SSfReS.Match(nombre, I0);
                alEncontrarPatronContenedor_DeCapitulos_DeMismaTemporada(mx, () =>
                {
                    capturar_Temporada_NT(mx);

                    Group gCnp = this.pr.re.getGrupoNumeroCantidadCapitulo(mx);
                    try {
                        getD().setIdentificacion_ConjuntoDeCapitulos_Numero(
                        indiceDeRepresentacionStr_numeroCantidad: gCnp.Index
                        , representacionStr_numeroCantidad: gCnp.ToString()
                        );
                    } catch (Exception ex) {
                        cwl("error cade 2");
                    }
                    
                    //getD().IndiceDeNumeroCantidadDeCapitulosQueContiene = gCnp.Index;//dm.IndiceNumeroFueraDeM + mm.Index;
                    //                                                                 //getD().CantidadDeCapitulosQueContiene = inT_Grp(gCnp);//dm.Numero;
                    //getD().CantidadDeCapitulosQueContieneStr = gCnp.ToString();

                    Group GNova = this.pr.re.getGrupoNumeroOva(mx);
                    
                    //getD().IndiceDeNumeroCantidadDeOvasQueContiene = GNova.Index;
                    //getD().CantidadDeOvasQueContieneStr = GNova.ToString();

                    

                    Group GIova = this.pr.re.getGrupoIdentificadorOva(mx);

                    getD().setIdentificacion_ConjuntoDeCapitulos_Ova_EtiquetaOvaNumero(
                        indiceDeRepresentacionStr_etiquetaOva: GIova.Index
                        ,representacionStr_etiquetaOva: GIova.ToString()
                    , indiceDeRepresentacionStr_numeroCantidad: GNova.Index
                    , representacionStr_numeroCantidad: GNova.ToString()
                    );


                    //getD().IndiceIdentificadorDeOVA = GIova.Index;
                    //getD().IdentificadorDeOVAStr = GIova.ToString();
                });
                if (getD() != null)
                {
                    return getD();
                }
                //Hyouka [22+1]
                mx = this.pr.re.Re_Cor_Nc_Mas_Nova_Cor.SSfReS.Match(nombre, I0);
                alEncontrarPatronContenedor_DeCapitulos_DeMismaTemporada(mx, () =>
                {
                    Group gCnp = this.pr.re.getGrupoNumeroCantidadCapitulo(mx);
                    getD().setIdentificacion_ConjuntoDeCapitulos_Numero(
                       indiceDeRepresentacionStr_numeroCantidad: gCnp.Index
                       , representacionStr_numeroCantidad: gCnp.ToString()
                       );
                    //getD().IndiceDeNumeroCantidadDeCapitulosQueContiene = gCnp.Index;//dm.IndiceNumeroFueraDeM + mm.Index;
                    //                                                                 //getD().CantidadDeCapitulosQueContiene = inT_Grp(gCnp);//dm.Numero;
                    //getD().CantidadDeCapitulosQueContieneStr = gCnp.ToString();

                    Group GNova = this.pr.re.getGrupoNumeroOva(mx);
                    //getD().CantidadDeOvasQueContiene = inT_Grp(GNova);
                    //getD().IndiceDeNumeroCantidadDeOvasQueContiene = GNova.Index;
                    //getD().CantidadDeOvasQueContieneStr = GNova.ToString();

                    getD().setIdentificacion_ConjuntoDeCapitulos_Ova_Numero(
                        
                     indiceDeRepresentacionStr_numeroCantidad: GNova.Index
                    , representacionStr_numeroCantidad: GNova.ToString()
                    );
                });
                if (getD() != null)
                {
                    return getD();
                }

            }//fin del if es solo manga y carpeta

            //si no es un archivo o un video, osea talves una carpeta o un nombre en un txt
            if (!(getCtx().EsArchivo || getCtx().EsVideo))
            {

                //Accused [Temp]  [1 -2] 
                mx = this.pr.re.Re_NT_IT_Temporada_NC_N_Ova.SSfReS.Match(nombre, I0);
                alEncontrarPatronContenedor_DeCapitulos_MismaSerie(mx, () =>
                {
                    if (!buscarDatosConjuntoTemporadas(d, mx))
                    {
                        capturar_Temporada_NT(mx);
                    }
                    else {
                        capturar_Temporada_Etiqueta(mx);
                    }
                });
                if (getD() != null)
                {
                    return getD();
                }
                //throw new Exception("hay que crearlo patron: NombreSerie [Temp]  [1 -2] ");
                //usar setIdentificacion_ConjuntoDeTemporadas_Etiqueta_InicialFinal
            }//fin si no es un archivo o un video

            //2nd-season-1
            mx = this.pr.re.Re_NT_IT_Temporada_NC.SSfReS.Match(nombre, I0);
            alEncontrarPatron(mx, () =>
            {
                capturar_Temporada_NT(mx);
                if (!capturar_Capitulo_AlFinal(mx))
                {
                    if (getCtx().EsCarpeta && !getCtx().EsVideo)
                    {
                        agregarContenedorDe_CapitulosDe_MismaTemporada();
                    }
                }
            });
            if (getD() != null)
            {
                return getD();
            }

            if (getCtx().EsCarpeta)
            {
                // [12-Full]
                //[27 cap]
                mx = this.pr.re.Re_Cor_Nc_Full_Cor.SSfReS.Match(nombre, I0);
                alEncontrarPatronContenedor_DeCapitulos_DeMismaTemporada(mx, () =>
                {
                    Group gCnp = this.pr.re.getGrupoNumeroCantidadCapitulo(mx);

                    //getD().IndiceDeNumeroCantidadDeCapitulosQueContiene = gCnp.Index;//dm.IndiceNumeroFueraDeM + mm.Index;
                    //                                                                 //getD().CantidadDeCapitulosQueContiene = inT_Grp(gCnp);//dm.Numero;
                    //getD().CantidadDeCapitulosQueContieneStr = gCnp.ToString();

                    Group gICnp = this.pr.re.getGrupoIdentificadorCantidadCapitulo(mx);

                    //getD().IndiceIdentificadorCapitulos = gICnp.Index;
                    //getD().IdentificadorCapitulosStr = gICnp.ToString();


                    getD().setIdentificacion_ConjuntoDeCapitulos_EtiquetaNumero(
                         indiceDeRepresentacionStr_etiqueta: gICnp.Index
                        , representacionStr_etiqueta: gICnp.ToString()
                        , indiceDeRepresentacionStr_numeroCantidad: gCnp.Index
                        , representacionStr_numeroCantidad: gCnp.ToString()
                        );
                });
                if (getD() != null)
                {
                    return getD();
                }

                // [12]
                mx = this.pr.re.Re_Cor_Nc_Cor.SSfReS.Match(nombre, I0);
                alEncontrarPatronContenedor_DeCapitulos_DeMismaTemporada(mx, () =>
                {
                    Group gCnp = this.pr.re.getGrupoNumeroCantidadCapitulo(mx);

                    //getD().IndiceDeNumeroCantidadDeCapitulosQueContiene = gCnp.Index;//dm.IndiceNumeroFueraDeM + mm.Index;
                    //                                                                 //getD().CantidadDeCapitulosQueContiene = inT_Grp(gCnp);//dm.Numero;
                    //getD().CantidadDeCapitulosQueContieneStr = gCnp.ToString();
                    getD().setIdentificacion_ConjuntoDeCapitulos_Numero(
                    indiceDeRepresentacionStr_numeroCantidad: gCnp.Index
                    , representacionStr_numeroCantidad: gCnp.ToString()
                    );
                });

                if (getD() != null)
                {
                    return getD();
                }

                //[1 TEMP]
                mx = this.pr.re.Re_Cor_NT_Temp_Cor.SSfReS.Match(nombre, I0);
                alEncontrarPatronContenedor_DeCapitulos_DeMismaTemporada(mx, () =>
                {
                    capturar_Temporada_NT(mx);
                });
                if (getD() != null)
                {
                    return getD();
                }

            }//fin del if si es carpeta


            //#T Capitulo #C UnionN
            mx = this.pr.re.Re_NT_Capitulo_N_Union_N_Repetir.SReS.Match(nombre, I0);
            alEncontrarPatron(mx, () =>
            {
                capturar_Temporada_NT(mx);
                if (!buscarDatosUnion(d, mx))
                {
                    capturar_Capitulo_NC(mx);
                }
            });


            if (getD() != null)
            {
                return getD();
            }

            //#T Capitulo #C    Karakai Jouzu no Takagi-san 3 Episodio 1
            mx = this.pr.re.Re_NT_Capitulo_NC.SReS.Match(nombre, I0);
            alEncontrarPatron(mx, () =>
            {


                capturar_Capitulo_NC(mx);

                Group gNt = this.pr.re.getGrupoNumeroTemporada(mx);
                if (!(esIgnorarNumeroDetrasDe(
                numero: inT(gNt)
                , indiceInicialDelNumero: gNt.Index
                                            )
                    || (pr.estaDentroDeFecha(gNt)))
                   )
                {
                    capturar_Temporada_NT(mx);

                }




            });

            if (getD() != null)
            {
                return getD();
            }



            //NxN UnionN
            //NTxE_NC UnionN   1xE1 ConstantesExprecionesRegulares.patronSNxEN
            // S04E UnionN
            // S04xE UnionN
            mx = getMatchCoincidente(arrgR(
                this.pr.re.Re_NxN_Union_N_Repetir_PosiblesEspaciosInternos.SSfReS
                , this.pr.re.Re_NxE_N_Union_N_Repetir.SSfReS
                , this.pr.re.Re_SNxE_N_Union_N_Repetir.SSfReS
                , this.pr.re.Re_SNE_N_Union_N_Repetir.SSfReS
            ));
            alEncontrarPatron(mx, () =>
            {
                //Group gNt = this.pr.re.getGrupoNumeroTemporada(mx);
                //getD().IndiceNumeroTemporada = gNt.Index;
                
                //getD().TemporadaStr = gNt.ToString();

                capturar_Temporada_NT(mx);

                //if (esIgnorarNumeroDetrasDe(getD().Temporada
                //                            , getD().IndiceNumeroTemporada)
                //    || (pr.estaDentroDeFecha(gNt))
                //   )
                if (esIgnorarNumeroDetrasDe(getD().IdenificadorTemporada)
                    || (pr.estaDentroDeFecha(getD().IdenificadorTemporada.identificacionNumerica))
                   )
                {
                    d = null;

                }
                else
                {
                    if (!buscarDatosUnion(d, mx) && !capturar_Capitulo_AlFinal(mx))
                    {
                        getD().cambiarDe_NumeroTemporada_A_NumeroCapitulo();
                        //getD().IndiceNumeroCapitulo = getD().IndiceNumeroTemporada;
                        //getD().CapituloStr = getD().TemporadaStr;


                        //getD().IndiceNumeroTemporada = -1;
                        //getD().Temporada = -1;


                    }
                    else
                    {
                        capturar_Temporada_NT(mx);
                    }

                }
            });
            if (getD() != null)
            {
                return getD();
            }

            //			if(this.nombre=="American Horror History S09E01 [720p] [Dual Audio]"){
            //				cwl("aqui!!");
            //				cwl(this.pr.re.Re_SNEN.SSfReS);
            //			}

            //NxN
            //NTxE_NC   1xE1 ConstantesExprecionesRegulares.patronSNxEN
            // S04E01
            // S04xE01
            Regex[] arreglos = arrgR(
                this.pr.re.Re_NxN_PosiblesEspaciosInternos.SSfReS
                , this.pr.re.Re_NxEN.SSfReS
                , this.pr.re.Re_SNEN.SSfReS
                , this.pr.re.Re_SNxEN.SSfReS
            );
            mx = getMatchCoincidente(arreglos);
            alEncontrarPatron(mx, () =>
            {
                //Group gNt = this.pr.re.getGrupoNumeroTemporada(mx);
                //getD().IndiceNumeroTemporada = gNt.Index;

                //getD().TemporadaStr = gNt.ToString();
                capturar_Temporada_NT(mx);
                //if (esIgnorarNumeroDetrasDe(getD().Temporada
                //                            , getD().IndiceNumeroTemporada)
                //   || (pr.estaDentroDeFecha(gNt))
                   if (esIgnorarNumeroDetrasDe(getD().IdenificadorTemporada)
                   || (pr.estaDentroDeFecha(getD().IdenificadorTemporada.identificacionNumerica))
                   )
                {
                    d = null;

                }
                else
                {
                    if (!capturar_Capitulo_AlFinal(mx))
                    {
                        //getD().IndiceNumeroCapitulo = getD().IndiceNumeroTemporada;
                        //getD().CapituloStr = getD().TemporadaStr;

                        //getD().IndiceNumeroTemporada = -1;
                        //getD().Temporada = -1;
                        getD().cambiarDe_NumeroTemporada_A_NumeroCapitulo();

                    }
                    else
                    {
                        capturar_Temporada_NT(mx);
                    }
                }


            });
            if (getD() != null)
            {
                return getD();
            }
            //usar	patronCapitulo_NC UnionN
            mx = this.pr.re.Re_Capitulo_N_Union_N_Repetir.SSfReS.Match(nombre, I0);
            alEncontrarPatron(mx, () =>
            {
                if (!buscarDatosUnion(d, mx) && !capturar_Capitulo_AlFinal(mx))
                {
                    d = null;


                }
            });
            if (getD() != null)
            {
                return getD();
            }
            //usar	patronCapitulo_NC
            //			cwl(this.pr.re.Re_Capitulo_NC.SSfReS);
            //			cwl("nombre="+nombre);
            mx = this.pr.re.Re_Capitulo_NC.SSfReS.Match(nombre, I0);
            alEncontrarPatron(mx, () =>
            {
                if (!capturar_Capitulo_AlFinal(mx))
                {
                    d = null;
                }
            });
            if (getD() != null)
            {
                return getD();
            }
            //usar patronTemporada_NT

            mx = this.pr.re.Re_Temporada_NT.SSfReS.Match(nombre, I0);
            alEncontrarPatronContenedor_DeCapitulos_DeMismaTemporada(mx, () =>
            {
                if (capturar_Temporada_NT_alFinal(mx))
                {
                    //					if (getCtx().EsCarpeta || ((!getCtx().EsVideo) && (!getCtx().EsArchivo))) {
                    //						//getD().EsContenedorDeTemporada=true;
                    //						agregarContenedor();
                    //					}
                }
                else
                {
                    d = null;
                }
            });
            if (getD() != null)
            {
                return getD();
            }


            //usar patronNT_IT_Temporada  
            mx = this.pr.re.Re_NT_IT_Temporada.SSfReS.Match(nombre, I0);
            alEncontrarPatronContenedor_DeCapitulos_DeMismaTemporada(mx, () =>
            {
                if (capturar_NT_Temporada_alPrincipio(mx))
                {
                    //					if (getCtx().EsCarpeta || ((!getCtx().EsVideo) && (!getCtx().EsArchivo))) {
                    //						//getD().EsContenedorDeTemporada=true;
                    //						agregarContenedor();
                    //					}
                }
                else
                {
                    d = null;
                }
            });
            if (getD() != null)
            {
                return getD();
            }

            if (!getCtx().EsVideo)
            {
                // carpeta solo SE 02
                //carpeta solo T #
                //carpeta  nombre S01
                //carpeta  S01
                mx = getMatchCoincidente(arrgR(
                    this.pr.re.Re_SE_NT_SoloConSeparaciones.InicialSReSFinal
                , this.pr.re.Re_T_NT_SoloConSeparaciones.InicialSReSFinal
                , this.pr.re.Re_Nombre_SN.InicialSReSFinal
                , this.pr.re.Re_SN.InicialSReSFinal

                ));
                alEncontrarPatronContenedor_DeCapitulos_DeMismaTemporada(mx, () => capturar_Temporada_NT(mx));
                if (getD() != null)
                {
                    return getD();
                }//1816

                //2 (01-13 final) (2016)
                //A channel Ａチャンネル (01-12 final) (2011) ★☆
                //patron (01-12 final) para contenedor en carpeta

                mx = getMatchCoincidente(arrgR(
                    this.pr.re.Re_NT_NC0_NCi_final.SSfReS
                , this.pr.re.Re_NC0_NCi_final.SSfReS
                ));
                mx = this.pr.re.Re_NC0_NCi_final.SSfReS.Match(nombre, I0);
                alEncontrarPatronContenedor_DeCapitulos_DeMismaTemporada(mx, () =>
                {
                    if (buscarDatosUnion(d, mx))
                    {
                        capturar_Temporada_NT(mx);
                    }
                    else {
                        d = null;
                    }

                    //Group gNC = this.pr.re.getGrupoNumeroCapitulo(mx);
                    //CaptureCollection lc = gNC.Captures;
                    //getD().EsConjuntoDeCapitulos = true;
                    //getD().CapituloInicialStr = lc[0].ToString();
                    //getD().IndiceNumeroCapituloInicial = lc[0].Index;
                    //getD().CapituloFinalStr = lc[1].ToString();
                    //getD().IndiceNumeroCapituloFinal = lc[1].Index;

                    //if (getD().CapituloInicial < getD().CapituloFinal)
                    //{
                    //    getD().CantidadDeCapitulosQueContiene = getD().CapituloFinal - getD().CapituloInicial;
                    //    Group gIT = this.pr.re.getGrupoIdentificadorTemporada(mx);
                    //    getD().IndiceIdentificadorTemporada = gIT.Index;
                    //    getD().IdentificadorTemporadaStr = gIT.ToString();

                    //    Group gNT = this.pr.re.getGrupoNumeroTemporada(mx);
                    //    if (gNT.Success)
                    //    {
                    //        int temporada = inT_Grp(gNT);
                    //        int indiceNumeroTemporada = gNT.Index;
                    //        if (esIgnorarNumeroDetrasDe(temporada,
                    //                                    indiceNumeroTemporada)
                    //           || (pr.estaDentroDeFecha(gNT))
                    //           )
                    //        {
                    //            getD().Temporada = 1;
                    //        }
                    //        else
                    //        {
                    //            getD().IndiceNumeroTemporada = indiceNumeroTemporada;
                    //            //getD().Temporada = temporada;
                    //            getD().TemporadaStr = gNT.ToString();
                    //        }

                    //    }
                    //    else
                    //    {
                    //        getD().Temporada = 1;
                    //    }

                    //}
                    //else
                    //{
                    //    d = null;
                    //}
                });
                if (getD() != null)
                {
                    return getD();
                }

                //this.pr.re.Re_NT_NC0_NCi_final

            }//fin del if si no es video

            if (getConf().EsParaAnime)
            {
                //shigatsu-wa-kimi-no-uso-23 ova
                mx = this.pr.re.Re_N_Ova.SSfReS.Match(nombre, I0);
                alEncontrarPatron(mx, () =>
                {
                    if (capturar_NC_CapituloOva_alPrincipio(mx)) {

                    }

                    //if (capturar_NC_Capitulo_alPrincipio(mx))
                    //{
                        
                    //    Group gIO = this.pr.re.getGrupoIdentificadorOva(mx);

                    //    getD().setEsOva();

                    //    getD().setIdentificacionOva_TagOVA(
                    //        indiceDeRepresentacionStr_tagOVA: gIO.Index
                    //        ,representacionStr_tagOVA: gIO.ToString()
                    //        );

                    //    //getD().IndiceIdentificadorDeOVA = gIO.Index;
                    //    //getD().EsOVA = true;
                    //    //getD().IdentificadorDeOVAStr = gIO.ToString();

                    //}
                    else
                    {
                        d = null;
                    }
                });
                if (getD() != null)
                {
                    return getD();
                }
            }//fin if es para anime


            //N-N
            //N1,N2  N1&N2
            mx = getMatchCoincidente(arrgR(
                this.pr.re.Re_N_Union_N_Repetir.SSfReS
            ));
            alEncontrarPatronContenedor_DeCapitulos_DeMismaTemporada(mx, () =>
            {
                Group gNC = this.pr.re.getGrupoNumeroCapitulo(mx);
                CaptureCollection lc = gNC.Captures;


                bool capturo = false;
                for (int i = 0; i < lc.Count; i++)
                {
                    Capture cN = lc[i];
                    int capitulo = inT_Cap(cN);
                    if (!esIgnorarNumeroDetrasDe(capitulo
                                                 , cN.Index)
                       && (!pr.estaDentroDeFecha(cN))
                       )
                    {
                        if (!(lc.Count - i > 1 && buscarDatosUnion(d, mx, i)))
                        {
                            getD().setIdentificacion_Capitulo_Numero(
                                indiceDeRepresentacionStr: cN.Index
                                ,representacionStr: cN.ToString()
                                );

                            //getD().IndiceNumeroCapitulo = cN.Index;
                            //getD().CapituloStr = cN.ToString();
                        }
                        capturo = true;
                        break;
                    }
                }
                if (!capturo)
                {
                    d = null;
                }



            });
            return getD();
        }


    }
}

