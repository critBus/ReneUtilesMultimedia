/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 28/7/2022
 * Hora: 18:03
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
    /// Description of BuscadorDeDatosSeries.
    /// </summary>
    public abstract class BuscadorDeDatosDeSerie : BuscadorDeDatosEnNombre
    {
        protected DatosDeNombreCapitulo d;


        //protected string nombre;
        protected string nombreSinOva;
        //protected int? I0;
        protected Match mx;
        //protected ProcesadorDeNombreDeSerie pr;
        //protected bool seBusco;


        public BuscadorDeDatosDeSerie(ProcesadorDeNombreDeSerie pr)
            : base(pr)
        {

            //			this.pr=pr;
            //			this.seBusco=false;
            //			this.nombre=this.pr.nombre;
        }

        protected void alIniciarD()
        {
            d.TipoDeNombre = tipoDeNombreDeSerie;
        }


        protected bool capturar_Capitulo_AlFinal(Match mm, int iNumeroInicial = 0)
        {
            Group gNC = this.pr.re.getGrupoNumeroCapitulo(mm);

            CaptureCollection cl = gNC.Captures;
            //int capitulo = inT_Grp(gNC);
            Capture cN = cl[iNumeroInicial];

            int capitulo = inT_Cap(cN);
            int indiceAcontinuacion = mm.Index + mx.Length;
            if (cl.Count > 1 && iNumeroInicial < cl.Count - 1)
            {
                indiceAcontinuacion = cl[iNumeroInicial + 1].Index;
            }

            DatosDeIgnorarNumero ignorar = getEs_IgnorarNumeroDelanteDe(numeroFinal: capitulo
                                                                        , indiceInicialNumero: cN.Index
                                                                        , indiceAContinuacion: indiceAcontinuacion);
            if (ignorar == null
                && (!pr.estaDentroDeFecha(cN)))
            {
                capturar_Capitulo_NC(mm, iNumeroInicial: iNumeroInicial);
                return true;
            }
            return false;
        }

        protected void capturar_CapituloOVA_NC(Match mm, int iNumeroInicial = 0)
        {
            Group gIC = this.pr.re.getGrupoIdentificadorOva(mm);
            Group gNC = this.pr.re.getGrupoNumeroOva(mm);
            if (gIC.Success)
            {
                d.setIdentificacion_CapituloOva_Etiqueta(
                     indiceDeRepresentacionOvaStr: gIC.Index
                    , representacionOvaStr: gIC.ToString()
                    );


                //            d.IndiceIdentificadorCapitulo = gIC.Index;
                //d.IdentificadorCapituloStr = gIC.ToString(); 
            }

            Group gU = this.pr.re.getGrupoUnion(mm);
            if (gNC.Success)
            {
                if (gU.Success)
                {
                    CaptureCollection cl = gNC.Captures;

                    d.setIdentificacion_CapituloOva_Numero(
                     indiceDeRepresentacionOvaStr: cl[iNumeroInicial].Index
                    , representacionOvaStr: cl[iNumeroInicial].ToString()
                    );

                    //d.CapituloStr = cl[iNumeroInicial].ToString();
                    //d.IndiceNumeroCapitulo = cl[iNumeroInicial].Index;
                }
                else
                {
                    d.setIdentificacion_CapituloOva_Numero(
                     indiceDeRepresentacionOvaStr: gNC.Index
                    , representacionOvaStr: gNC.ToString()
                    );

                    //d.IndiceNumeroCapitulo = gNC.Index;
                    //d.CapituloStr = gNC.ToString();
                }
            }

            

        }

        protected void capturar_Capitulo_NC(Match mm, int iNumeroInicial = 0)
        {
            Group gIC = this.pr.re.getGrupoIdentificadorCapitulo(mm);
            Group gNC = this.pr.re.getGrupoNumeroCapitulo(mm);
            if (gIC.Success)
            {
                d.setIdentificacion_Capitulo_Etiqueta(
                     indiceDeRepresentacionStr: gIC.Index
                    , representacionStr: gIC.ToString()
                    );


                //            d.IndiceIdentificadorCapitulo = gIC.Index;
                //d.IdentificadorCapituloStr = gIC.ToString(); 
            }

            Group gU = this.pr.re.getGrupoUnion(mm);
            if (gNC.Success)
            {
                if (gU.Success)
                {
                    CaptureCollection cl = gNC.Captures;

                    d.setIdentificacion_Capitulo_Numero(
                     indiceDeRepresentacionStr: cl[iNumeroInicial].Index
                    , representacionStr: cl[iNumeroInicial].ToString()
                    );

                    //d.CapituloStr = cl[iNumeroInicial].ToString();
                    //d.IndiceNumeroCapitulo = cl[iNumeroInicial].Index;
                }
                else
                {
                    d.setIdentificacion_Capitulo_Numero(
                     indiceDeRepresentacionStr: gNC.Index
                    , representacionStr: gNC.ToString()
                    );

                    //d.IndiceNumeroCapitulo = gNC.Index;
                    //d.CapituloStr = gNC.ToString();
                }
            }



        }

        protected void capturar_Temporada_Etiqueta(Match mm)
        {
            Group gIT = this.pr.re.getGrupoIdentificadorTemporada(mm);
            if (gIT.Success)
            {
                d.setIdentificacionTemporada_Etiqueta(
                     indiceDeRepresentacionStr: gIT.Index
                    , representacionStr: gIT.ToString()
                    );

                //d.IndiceIdentificadorTemporada = gIT.Index;
                //d.IdentificadorTemporadaStr = gIT.ToString();
            }
        }

        protected void capturar_Temporada_NT(Match mm)
        {
            //Group gIT = this.pr.re.getGrupoIdentificadorTemporada(mm);
            //if (gIT.Success)
            //{
            //    d.setIdentificacionTemporada_Etiqueta(
            //         indiceDeRepresentacionStr: gIT.Index
            //        , representacionStr: gIT.ToString()
            //        );

            //    //d.IndiceIdentificadorTemporada = gIT.Index;
            //    //d.IdentificadorTemporadaStr = gIT.ToString();
            //}
            capturar_Temporada_Etiqueta(mm);

            Group gNT = this.pr.re.getGrupoNumeroTemporada(mm);
            if (gNT.Success)
            {

                d.setIdentificacionTemporada_Numero(
                    indiceDeRepresentacionStr: gNT.Index
                   , representacionStr: gNT.ToString()
                   );
                //d.IndiceNumeroTemporada = gNT.Index;

                
                //d.TemporadaStr = gNT.ToString();
            }
        }
        protected void agregarContenedorDe_CapitulosDe_MismaTemporada()
        {
            d.setEsContendedorDe_Capitulos_DeMismaTemporada(true);
            getCtxCn().agregarPropiedadesAContextoHAY_CONTENEDORES_CAPITULOS_MISMA_TEMPORADA();
        }
        protected void agregarContenedorDe_Capitulos_MismaSerie()
        {
            d.setEsContendedorDe_Capitulos_DeMismaSerie(true);
            getCtxCn().agregarPropiedadesAContextoHAY_CONTENEDORES_CAPITULOS_MISMA_SERIE();
        }

        protected void agregarContenedorDe_Temporadas_MismaSerie()
        {
            d.setEsContendedorDe_Temporadas(true);
            getCtxCn().agregarPropiedadesAContextoHAY_CONTENEDORES_TEMPORADAS_MISMA_SERIE();
        }
        //protected void agregarContenedor()
        //{
        //    d.EsContenedorDeTemporada = true;
        //    getCtxCn().agregarPropiedadesAContextoHAY_CONTENEDORES_TEMPORADAS();
        //}

        protected Match getMatchCoincidente(Regex[] Rs)
        {
            Match sm = null;
            foreach (Regex r in Rs)
            {
                //cwl(r);

                sm = r.Match(nombre, this.getIO());
                if (sm.Success)
                {
                    return sm;
                }
            }
            return sm;
        }

        public bool seEncontroPatron()
        {
            return this.d != null;
        }

        protected abstract void initD();
        protected override void setTipoDeNombre(TipoDeNombreDeSerie? t)
        {
            base.setTipoDeNombre(t);
            //if (d==null) {
            //    d = new DatosDeNombreCapitulo();
            //}
            initD();
            d.TipoDeNombre = this.tipoDeNombreDeSerie;
        }




        //		
    }

}

