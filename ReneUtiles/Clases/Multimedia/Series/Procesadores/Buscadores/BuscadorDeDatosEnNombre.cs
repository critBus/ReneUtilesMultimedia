/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 31/7/2022
 * Hora: 11:58
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
    /// Description of BuscadorDeDatosEnNombre.
    /// </summary>
    public class BuscadorDeDatosEnNombre : ConsultorDeDatosEnNombre
    {
        public int? I0;

        protected TipoDeNombreDeSerie? tipoDeNombreDeSerie;
        public BuscadorDeDatosEnNombre(ProcesadorDeNombreDeSerie pr)
            : base(pr)
        {
            //tipoDeNombreDeSerie = TipoDeNombreDeSerie.DESCONOCIDO;

        }
        private RecursosDePatronesDeSeries getRe()
        {
            return this.pr.re;
        }


        protected int getIO()
        {
            return this.I0 == null ? -1 : (int)this.I0;
        }
        public bool seBuscoCon(int I0)
        {
            return this.seBusco && this.I0 < I0;//&&this.nombre==nombre
        }


        protected virtual void setTipoDeNombre(TipoDeNombreDeSerie? t)
        {
            this.tipoDeNombreDeSerie = t;
        }


        //otros


        public DatosDeIgnorarNumero getEs_IgnorarNumeroDelanteDe(int numeroFinal, Match m)
        {
            return getEs_IgnorarNumeroDelanteDe(numeroFinal, m.Index, m.Index + m.Length);
        }
        public DatosDeIgnorarNumero getEs_IgnorarNumeroDelanteDe(
            int numeroFinal
            , int indiceInicialNumero
            , int indiceAContinuacion)
        {
            Func<DatosDeIgnorarNumero, DatosDeIgnorarNumero> rectificarD = v =>
            {
                if (v != null && v.IndiceAContinuacion < indiceAContinuacion)
                {
                    v.IndiceAContinuacion = indiceAContinuacion;
                }
                return v;
            };
            DatosDeIgnorarNumero d = null;
            if (esAñoModerno(numeroFinal))
            {
                return null;
            }

            if (indiceAContinuacion != nombre.Length && indiceAContinuacion != nombre.Length - 1)
            {
                for (int i = 0; i < 5; i++)
                {
                    int lengNombre = -1;
                    string nombreDeSerie = null;
                    bool loEs = false;
                    switch (i)
                    {
                        case 0:
                            Match mn = getPr().esNombresConUnNumeroInterno_m(this.nombre, numeroFinal, indiceInicialNumero);
                            if (mn != null && mn.Success)
                            {
                                nombreDeSerie = mn.ToString();//subs(nombre,0,lengNombre);
                                lengNombre = mn.Length;
                                loEs = esNombreUnNumeroInterno_ModificaContexto(nombreDeSerie, null, true);
                            }
                            break;
                        case 1:
                            lengNombre = getPr().esNombreRodeadosDeNumeros_buscarDesdeElPrincipio_indiceFinal(this.nombre, numeroFinal, indiceInicialNumero);
                            if (lengNombre != -1)
                            {
                                nombreDeSerie = subs(nombre, 0, lengNombre);
                                loEs = esNombreRodeadoDeNumeros_ModificaContexto(nombreDeSerie, null, true);
                            }

                            break;
                        case 2:
                            //if (this.nombre == "12 Monkeys")
                            //{
                            //    cwl("aqui");
                            //}
                            ProcesadorDeNombreDeSerie pr = getPr();
                            lengNombre = pr.esNombreConNumerosAlPrincipio_indiceFinal(this.nombre, numeroFinal, indiceInicialNumero);
                            if (lengNombre != -1)
                            {
                                nombreDeSerie = subs(nombre, 0, lengNombre);
                                loEs = esNombreNumerosMultiplesInicial_ModificaContexto(nombreDeSerie, null, true);
                            }
                            break;
                        case 3:
                            lengNombre = getPr().esNombreConNumeroAlPrincipio_indiceFinal(this.nombre, numeroFinal, indiceInicialNumero);
                            if (lengNombre != -1)
                            {
                                nombreDeSerie = subs(nombre, 0, lengNombre);
                                loEs = esNombreNumeroInicial_ModificaContexto(nombreDeSerie, null, true);
                            }
                            break;


                        case 4:
                            lengNombre = getPr().esNombreNumericosMultiples_buscarDesdeElPrincipio_indiceFinal(this.nombre, numeroFinal, indiceInicialNumero);
                            if (lengNombre != -1)
                            {
                                nombreDeSerie = subs(nombre, 0, lengNombre);
                                loEs = esNombreNumericosCompletosMultiples_ModificaContexto(nombreDeSerie, null, true);
                            }
                            break;

                    }
                    if (loEs)
                    {
                        //setTipoDeNombre(t);
                        return rectificarD(new DatosDeIgnorarNumero(lengNombre));
                    }
                }



                int indice = -1;
                //for (int j = 0; j < lengIgnorarNumeroEspecificoDelanteDe; j++) {
                for (int j = 0; j < getConf().ignorarNumeroEspecificoDelanteDe.Length; j++)
                {
                    CondicionIgnorarNumeroEspecifico c = getConf().ignorarNumeroEspecificoDelanteDe[j];
                    if (c.Numero == numeroFinal)
                    {
                        //cwl(str(c.Caracteres));
                        indice = Utiles.startsWith_Indice(nombre, indiceAContinuacion, c.Caracteres);
                        if (indice != -1)
                        {
                            //algo negativo
                            return rectificarD(new DatosDeIgnorarNumero(indiceAContinuacion + c.Caracteres[indice].Length));
                        }
                    }
                    else
                    {
                        if (c.Numero > numeroFinal)
                        {
                            break;
                        }
                    }
                }





                //comprobrar si este numero es parte de algo que alla que saltar
                int indiceSiguiente = indiceAContinuacion;
                if (or(nombre.ElementAt(indiceAContinuacion - 1), ',', '.') && Char.IsNumber(nombre.ElementAt(indiceAContinuacion - 2)) && Char.IsNumber(nombre.ElementAt(indiceAContinuacion)))
                {
                    Match mConSeparaciones = Matchs.R_N.ReSu.Match(nombre, indiceAContinuacion);
                    indiceSiguiente = indiceAContinuacion + mConSeparaciones.Length;
                }
                indice = Utiles.startsWith_Indice(nombre, indiceSiguiente, true, getConf().SaltarCualquierNumeroAntesDe);
                if (indice != -1)
                {
                    indiceSiguiente = indiceAContinuacion + getConf().SaltarCualquierNumeroAntesDe[indice].Length;
                    if (indiceSiguiente == nombre.Length || !Char.IsLetterOrDigit(nombre.ElementAt(indiceSiguiente)))
                    {
                        return rectificarD(new DatosDeIgnorarNumero(indiceSiguiente));
                    }

                }

                //Comprobar si el numero es parte de una Aleaterizacion
                //int indiceInicialDelNumero = indiceAContinuacion - numeroFinal.ToString().Length;
                //				string tramo = getRe().Re_numerosYLetras.Re.Match(nombre.Substring(indiceInicialNumero)).ToString();
                //				if (Utiles.esAleaterizacion(tramo)) {
                //					d = new DatosDeIgnorarNumero(indiceInicialNumero + tramo.Length);
                //					d.EsAleaterizacion = true;
                //					return d;
                //				}
                d = getPr().estaDentroDeAleatoriedad_DatosDeIgnorarNumero(indiceInicialNumero);
                if (d != null)
                {
                    return rectificarD(d);
                }
                d = getPr().estaDentroDeFecha_DatosDeIgnorarNumero(indiceInicialNumero);
                if (d != null)
                {
                    return rectificarD(d);
                }


            }//fin del if comparar con el leng nombre

            return rectificarD(d);
        }
        public bool esIgnorarNumeroDetrasDe(DatosDeIdentificacionIndividual n) {
            return esIgnorarNumeroDetrasDe(n.identificacionNumerica.Numero
                ,n.identificacionNumerica.IndiceDeRepresentacionStr);
        }
        public bool esIgnorarNumeroDetrasDe(int numero, int indiceInicialDelNumero)
        {//_SinComprarAño
            if (esAñoModerno(numero))
            {
                return true;
            }
            //Me quedo con el texto hasta el comienzo del numero
            //para despues poder valorar si hay algo que lo marque como numero a saltar
            string subTexto = subs(this.nombre, 0, indiceInicialDelNumero);

            //Se elimina las separaciones finales (desde el comienzo del numero hacia atras
            //, como el paso anterior recorto el estring estas separaciones de existir
            //se encuentran al final)
            subTexto = getRe().Re_separaciones.ReFinal.Replace(subTexto, "");


            int indiceSiguienteAlNumero = indiceInicialDelNumero + numero.ToString().Length;
            ProcesadorDeNombreDeSerie pre = getPr();
            for (int i = 0; i < 4; i++)
            {
                int indiceInicialNombre = -1;
                string nombreDeSerie = null;
                bool loEs = false;
                switch (i)
                {
                    case 0:
                        Match mn = pre.esNombresConUnNumeroInterno_m(this.nombre, numero, indiceInicialDelNumero);
                        if (mn != null && mn.Success)
                        {
                            nombreDeSerie = mn.ToString();//subs(nombre,0,lengNombre);
                            indiceInicialNombre = mn.Index;
                            loEs = esNombreUnNumeroInterno_ModificaContexto(nombreDeSerie, null, true);
                        }

                        break;
                    case 1:
                        indiceInicialNombre = pre.esNombreRodeadosDeNumeros_buscarDesdeElFinal_indiceInicial(this.nombre, numero, indiceInicialDelNumero);
                        if (indiceInicialNombre != -1)
                        {
                            nombreDeSerie = subs(nombre, 0, indiceSiguienteAlNumero);
                            loEs = esNombreRodeadoDeNumeros_ModificaContexto(nombreDeSerie, null, true);
                        }

                        break;
                    case 2:
                        //						if(subTexto=="American Horror History S"){
                        //							cwl("aqui");
                        //						}
                        indiceInicialNombre = pre.esNombreConNumeroAlFinal_buscarDesdeElFinal_indiceInicial(this.nombre, numero, indiceInicialDelNumero);
                        if (indiceInicialNombre != -1)
                        {
                            nombreDeSerie = subs(nombre, indiceInicialNombre, indiceSiguienteAlNumero);
                            loEs = esNombreNumeroFinal_ModificaContexto(nombreDeSerie, null, true);
                        }
                        break;

                    case 3:
                        indiceInicialNombre = getPr().esNombreNumericosMultiples_buscarDesdeElFinal_indiceInicial(this.nombre, numero, indiceInicialDelNumero);
                        if (indiceInicialNombre != -1)
                        {
                            nombreDeSerie = subs(nombre, 0, indiceSiguienteAlNumero);
                            loEs = esNombreNumericosCompletosMultiples_ModificaContexto(nombreDeSerie, null, true);
                        }
                        break;

                }
                if (loEs)
                {
                    //setTipoDeNombre(t);
                    //return new DatosDeIgnorarNumero(lengNombre);
                    return true;
                }
            }


            //Se comprueba la coincidencia (terminado el estring) con algun patron que indique que hay que
            //ignorar ese numero (recordar que se limpio el final de separaciones)
            if (getRe().Re_saltarCualquierNumeroDespuesDe_Patron.ReSfFinal.Match(subTexto).Success)
            {
                return true;
            }

            int lengIgnorarNumeroEspecificoDetrasDe = getConf().ignorarNumeroEspecificoDetrasDe.Length;
            int indice = -1;
            for (int j = 0; j < lengIgnorarNumeroEspecificoDetrasDe; j++)
            {
                CondicionIgnorarNumeroEspecifico c = getConf().ignorarNumeroEspecificoDetrasDe[j];
                if (c.Numero == numero)
                {

                    indice = Utiles.endsWith_Indice(subTexto, c.Caracteres);
                    if (indice != -1)
                    {
                        //algo negativo
                        return true;
                    }
                }
                else
                {
                    if (c.Numero > numero)
                    {
                        //						break;
                    }
                }
            }//fin for


            //			Match m = getRe().Re_numerosYLetras.ReFinal.Match(subTexto);
            //			if (m.Success && Utiles.esAleaterizacion(m.ToString(), true)) {
            //				return true;
            //			}


            DatosDeIgnorarNumero di = getPr().estaDentroDeAleatoriedad_DatosDeIgnorarNumero(indiceInicialDelNumero);
            if (di != null)
            {
                return true;
            }
            di = getPr().estaDentroDeFecha_DatosDeIgnorarNumero(indiceInicialDelNumero);
            if (di != null)
            {
                return true;
            }

            return false;
        }
        public bool esNumeroParteDeNombre(int numero, int indiceInicialDelNumero)
        {

            for (int i = 0; i < 5; i++)
            {
                int lengNombre = -1;
                string nombreDeSerie = null;
                bool loEs = false;
                switch (i)
                {
                    case 0:
                        Match mn = getPr().esNombresConUnNumeroInterno_m(this.nombre, numero, indiceInicialDelNumero);
                        if (mn != null && mn.Success)
                        {
                            nombreDeSerie = mn.ToString();//subs(nombre,0,lengNombre);
                            lengNombre = mn.Length;
                            loEs = esNombreUnNumeroInterno_ModificaContexto(nombreDeSerie, null, true);
                        }
                        break;
                    case 1:
                        lengNombre = getPr().esNombreRodeadosDeNumeros_buscarDesdeElPrincipio_indiceFinal(this.nombre, numero, indiceInicialDelNumero);
                        if (lengNombre != -1)
                        {
                            nombreDeSerie = subs(nombre, 0, lengNombre);
                            loEs = esNombreRodeadoDeNumeros_ModificaContexto(nombreDeSerie, null, true);
                        }
                        break;
                    case 2:
                        //if (this.nombre=="2 anos un dia") {
                        //    cwl("aqui");
                        //}
                        ProcesadorDeNombreDeSerie pros = getPr();

                        lengNombre = pros.esNombreConNumeroAlPrincipio_indiceFinal(this.nombre, numero, indiceInicialDelNumero);
                        if (lengNombre != -1)
                        {
                            nombreDeSerie = subs(nombre, 0, lengNombre);
                            loEs = esNombreNumeroInicial_ModificaContexto(nombreDeSerie, null, true);
                        }
                        break;

                    case 3:
                        lengNombre = getPr().esNombreConNumerosAlPrincipio_indiceFinal(this.nombre, numero, indiceInicialDelNumero);
                        if (lengNombre != -1)
                        {
                            nombreDeSerie = subs(nombre, 0, lengNombre);
                            loEs = esNombreNumerosMultiplesInicial_ModificaContexto(nombreDeSerie, null, true);
                        }
                        break;


                        //						case 4:
                        //							lengNombre = getPr().esNombreNumericosMultiples_buscarDesdeElPrincipio_indiceFinal(this.nombre, numero, indiceInicialDelNumero);
                        //							nombreDeSerie=subs(nombre,0,lengNombre);
                        //							loEs=esNombreNumericosCompletosMultiples_ModificaContexto(nombreDeSerie);
                        //							break;
                }
                if (loEs)
                {
                    //setTipoDeNombre(t);
                    //return new DatosDeIgnorarNumero(lengNombre);
                    return true;
                }
            }

            int indiceSiguienteAlNumero = indiceInicialDelNumero + numero.ToString().Length;
            for (int i = 0; i < 4; i++)
            {
                int indiceInicialNombre = -1;
                string nombreDeSerie = null;
                bool loEs = false;
                switch (i)
                {
                    case 0:
                        Match mn = getPr().esNombresConUnNumeroInterno_m(this.nombre, numero, indiceInicialDelNumero);
                        if (mn != null && mn.Success)
                        {
                            nombreDeSerie = mn.ToString();//subs(nombre,0,lengNombre);
                            indiceInicialNombre = mn.Index;
                            loEs = esNombreUnNumeroInterno_ModificaContexto(nombreDeSerie, null, true);
                        }
                        break;
                    case 1:
                        indiceInicialNombre = getPr().esNombreRodeadosDeNumeros_buscarDesdeElFinal_indiceInicial(this.nombre, numero, indiceInicialDelNumero);
                        if (indiceInicialNombre != -1)
                        {
                            nombreDeSerie = subs(nombre, indiceInicialNombre, indiceSiguienteAlNumero);
                            loEs = esNombreRodeadoDeNumeros_ModificaContexto(nombreDeSerie, null, true);
                        }
                        break;
                    case 2:
                        indiceInicialNombre = getPr().esNombreConNumeroAlFinal_buscarDesdeElFinal_indiceInicial(this.nombre, numero, indiceInicialDelNumero);
                        if (indiceInicialNombre != -1)
                        {
                            nombreDeSerie = subs(nombre, indiceInicialNombre, indiceSiguienteAlNumero);
                            loEs = esNombreNumeroFinal_ModificaContexto(nombreDeSerie, null, true);
                        }
                        break;

                        //						case 3:
                        //							indiceInicialNombre = getPr().esNombreNumericosMultiples_buscarDesdeElFinal_indiceInicial(this.nombre, numero, indiceInicialDelNumero);
                        //							nombreDeSerie=subs(nombre,0,indiceSiguienteAlNumero);
                        //							loEs=esNombreNumericosCompletosMultiples_ModificaContexto(nombreDeSerie);
                        //							break;

                }
                if (loEs)
                {
                    //setTipoDeNombre(t);
                    //return new DatosDeIgnorarNumero(lengNombre);
                    return true;
                }
            }

            string nombreDeSerie2 = null;
            bool loEs2 = false;

            int lengNombre2 = getPr().esNombreNumericosMultiples_buscarDesdeElPrincipio_indiceFinal(this.nombre, numero, indiceInicialDelNumero);
            if (lengNombre2 != -1)
            {
                nombreDeSerie2 = subs(nombre, 0, lengNombre2);
                loEs2 = esNombreNumericosCompletosMultiples_ModificaContexto(nombreDeSerie2, null, true);
                if (loEs2)
                {
                    return true;
                }
            }

            int indiceInicialNombre2 = getPr().esNombreNumericosMultiples_buscarDesdeElFinal_indiceInicial(this.nombre, numero, indiceInicialDelNumero);
            if (indiceInicialNombre2 != -1)
            {
                nombreDeSerie2 = subs(nombre, indiceInicialNombre2, indiceSiguienteAlNumero);
                loEs2 = esNombreNumericosCompletosMultiples_ModificaContexto(nombreDeSerie2, null, true);
                if (loEs2)
                {
                    return true;
                }
            }

            return false;
        }



        private bool buscarUnion(DatosDeNombreCapitulo dd, Match mm, int iNumeroInicial , Action<Capture, int> alDescartarUltimoNumero 
            ,Func<Match, Group> getGC
            ,Action<int,string,int,string> alCapturar
            ) {
            Group gC = getGC(mm);
            CaptureCollection cl = gC.Captures;



            int indiceEnListaDeUltimo = cl.Count - 1;
            int ultimoNumero = inT_Cap(cl[indiceEnListaDeUltimo]);
            string ultimoNumeroStr = cl[indiceEnListaDeUltimo].ToString();
            int indiceAContinuacion = mm.Index + mm.Length;//cl[indiceEnListaDeUltimo].Index+cl[indiceEnListaDeUltimo].Value.Length;

            string separador = null;
            bool esContinuidad = getRe().getGrupoContinuidad(mm).Success;


            string numeroAnteriorStr = "";
            //compruebo que tiene la misma separacion 
            //y que los numeros esten en orden ascendente
            for (int i = iNumeroInicial + 1; i <= indiceEnListaDeUltimo; i++)
            {
                int indiceNumeroAnterior = i - 1;
                Capture cAnterio = cl[indiceNumeroAnterior];
                Capture cActual = cl[i];
                int indiceFinAnteriorN = cAnterio.Index + cActual.Length;//(cActual.Value.Length)
                int indiceInicioActualN = cActual.Index;
                int numeroAnterior = inT_Cap(cAnterio);
                numeroAnteriorStr = cAnterio.ToString();
                int numeroActual = inT_Cap(cActual);
                string separadorActual = subs(nombre, indiceFinAnteriorN, indiceInicioActualN);

                if (numeroAnterior > numeroActual
                    || ((separador == null) ? false : separador != separadorActual)
                    || (esContinuidad && numeroAnterior + 1 != numeroActual))
                {
                    indiceEnListaDeUltimo = indiceNumeroAnterior;
                    ultimoNumero = numeroAnterior;
                    ultimoNumeroStr = numeroAnteriorStr;
                    indiceAContinuacion = cActual.Index;
                    if (alDescartarUltimoNumero != null)
                    {
                        alDescartarUltimoNumero(cAnterio, indiceAContinuacion);
                    }
                    break;
                }
                if (separador == null)
                {
                    separador = separadorActual;
                }



            }

            //voy comprobando que el ultimo numero no alla que ignorarlo
            //siempre que me queden al menos dos numeros para que sea una union
            for (int i = indiceEnListaDeUltimo; i > iNumeroInicial; i--)
            {
                if (i != indiceEnListaDeUltimo)
                {
                    indiceAContinuacion = cl[i + 1].Index;
                    ultimoNumero = inT_Cap(cl[i]);
                    ultimoNumeroStr = cl[i].ToString();
                    indiceEnListaDeUltimo = i;
                    if (alDescartarUltimoNumero != null)
                    {
                        alDescartarUltimoNumero(cl[i], indiceAContinuacion);
                    }
                }

                DatosDeIgnorarNumero ignorar = getEs_IgnorarNumeroDelanteDe(
                                                   numeroFinal: ultimoNumero
                    , indiceInicialNumero: cl[indiceEnListaDeUltimo].Index
                    , indiceAContinuacion: indiceAContinuacion);
                if (ignorar == null
                    && (!getPr().estaDentroDeFecha(cl[i]))
                    && (!getPr().estaDentroDeAleatoriedad(cl[i].Index)))
                {

                    alCapturar(cl[iNumeroInicial].Index, cl[iNumeroInicial].ToString(), cl[indiceEnListaDeUltimo].Index, ultimoNumeroStr);
                    //dd.setIdentificacion_ConjuntoDeCapitulos_InicialFinal(
                    //    indiceDeRepresentacionStr_inicial: cl[iNumeroInicial].Index
                    //    , representacionStr_inicial: cl[iNumeroInicial].ToString()
                    //    , indiceDeRepresentacionStr_final: cl[indiceEnListaDeUltimo].Index
                    //    , representacionStr_final: ultimoNumeroStr
                    //    );


                    return true;
                }
            }
            return false;

        }


        public bool buscarDatosConjuntoTemporadas(DatosDeNombreCapitulo dd, Match mm, int iNumeroInicial = 0, Action<Capture, int> alDescartarUltimoNumero = null)
        {//alDescartarUltimoNumero  (Capture #, indiceAContinuacionDespuesDeLosSaltos)=>{}

            return buscarUnion(
                dd,mm, iNumeroInicial, alDescartarUltimoNumero
                ,getGC:getRe().getGrupoNumeroTemporada 
                ,alCapturar:dd.setIdentificacion_ConjuntoDeTemporadas_InicialFinal
                );


        }

        public bool buscarDatosUnion(DatosDeNombreCapitulo dd, Match mm, int iNumeroInicial = 0, Action<Capture, int> alDescartarUltimoNumero = null)
        {
            return buscarUnion(
                dd, mm, iNumeroInicial, alDescartarUltimoNumero
                , getGC: getRe().getGrupoNumeroCapitulo
                , alCapturar: dd.setIdentificacion_ConjuntoDeCapitulos_InicialFinal
                );


            
            //Group gC = getRe().getGrupoNumeroCapitulo(mm);
            //CaptureCollection cl = gC.Captures;
            

            //int indiceEnListaDeUltimo = cl.Count - 1;
            //int ultimoNumero = inT_Cap(cl[indiceEnListaDeUltimo]);
            //string ultimoNumeroStr = cl[indiceEnListaDeUltimo].ToString();
            //int indiceAContinuacion = mm.Index + mm.Length;//cl[indiceEnListaDeUltimo].Index+cl[indiceEnListaDeUltimo].Value.Length;

            //string separador = null;
            //bool esContinuidad = getRe().getGrupoContinuidad(mm).Success;


            //string numeroAnteriorStr = "";
            ////compruebo que tiene la misma separacion 
            ////y que los numeros esten en orden ascendente
            //for (int i = iNumeroInicial + 1; i <= indiceEnListaDeUltimo; i++)
            //{
            //    int indiceNumeroAnterior = i - 1;
            //    Capture cAnterio = cl[indiceNumeroAnterior];
            //    Capture cActual = cl[i];
            //    int indiceFinAnteriorN = cAnterio.Index + cActual.Length;//(cActual.Value.Length)
            //    int indiceInicioActualN = cActual.Index;
            //    int numeroAnterior = inT_Cap(cAnterio);
            //    numeroAnteriorStr = cAnterio.ToString();
            //    int numeroActual = inT_Cap(cActual);
            //    string separadorActual = subs(nombre, indiceFinAnteriorN, indiceInicioActualN);

            //    if (numeroAnterior > numeroActual
            //        || ((separador == null) ? false : separador != separadorActual)
            //        || (esContinuidad && numeroAnterior + 1 != numeroActual))
            //    {
            //        indiceEnListaDeUltimo = indiceNumeroAnterior;
            //        ultimoNumero = numeroAnterior;
            //        ultimoNumeroStr = numeroAnteriorStr;
            //        indiceAContinuacion = cActual.Index;
            //        if (alDescartarUltimoNumero != null)
            //        {
            //            alDescartarUltimoNumero(cAnterio, indiceAContinuacion);
            //        }
            //        break;
            //    }
            //    if (separador == null)
            //    {
            //        separador = separadorActual;
            //    }



            //}

            ////voy comprobando que el ultimo numero no alla que ignorarlo
            ////siempre que me queden al menos dos numeros para que sea una union
            //for (int i = indiceEnListaDeUltimo; i > iNumeroInicial; i--)
            //{
            //    if (i != indiceEnListaDeUltimo)
            //    {
            //        indiceAContinuacion = cl[i + 1].Index;
            //        ultimoNumero = inT_Cap(cl[i]);
            //        ultimoNumeroStr = cl[i].ToString();
            //        indiceEnListaDeUltimo = i;
            //        if (alDescartarUltimoNumero != null)
            //        {
            //            alDescartarUltimoNumero(cl[i], indiceAContinuacion);
            //        }
            //    }

            //    DatosDeIgnorarNumero ignorar = getEs_IgnorarNumeroDelanteDe(
            //                                       numeroFinal: ultimoNumero
            //        , indiceInicialNumero: cl[indiceEnListaDeUltimo].Index
            //        , indiceAContinuacion: indiceAContinuacion);
            //    if (ignorar == null
            //        && (!getPr().estaDentroDeFecha(cl[i]))
            //        && (!getPr().estaDentroDeAleatoriedad(cl[i].Index)))
            //    {
                   
            //        dd.setIdentificacion_ConjuntoDeCapitulos_InicialFinal(
            //            indiceDeRepresentacionStr_inicial: cl[iNumeroInicial].Index
            //            , representacionStr_inicial: cl[iNumeroInicial].ToString()
            //            , indiceDeRepresentacionStr_final: cl[indiceEnListaDeUltimo].Index
            //            , representacionStr_final: ultimoNumeroStr
            //            );

                    
            //        return true;
            //    }
            //}
            //return false;


        }



        //apoyo
        public bool tieneAlgunNumero(string n)
        {
            for (int i = 0; i < n.Length; i++)
            {
                char c = n.ElementAt(i);
                if (Char.IsNumber(c))
                {
                    return true;
                }
            }
            return false;
        }
        public bool esAñoModerno(int numero)
        {
            return numero > 1950 && numero < 2030;
        }
        public DatosDeNombreCapituloDelFinal crearDatosDeNombreCapituloDelFinal()
        {
            return new DatosDeNombreCapituloDelFinal();
        }
        public string getNumeroMasSeparacion(string texto, string numero, int indiceNumero)
        {
            string r = numero.ToString();
            for (int i = indiceNumero + numero.ToString().Length; i < texto.Length; i++)
            {
                char c = texto.ElementAt(i);
                if (esCharSeparacion(c))
                {
                    r += c;
                }
                else
                {
                    break;
                }
            }
            return r;
        }
        public string getSinSeparacionAlFinal(string texto)
        {
            int indiceFinal = -1;
            for (int i = texto.Length - 1; i > 0; i--)
            {
                char c = texto.ElementAt(i);
                if (!esCharSeparacion(c))
                {
                    indiceFinal = i;
                }
            }
            return (indiceFinal > 0) ? subs(texto, 0, indiceFinal) : (indiceFinal == 0 ? texto.ElementAt(0).ToString() : "");

        }
        public int getIndiceAContinuacionDeSeparacionDespuesDeNumero(string texto, string numero, int indiceNumero)
        {
            return indiceNumero + getNumeroMasSeparacion(texto, numero, indiceNumero).Length;
        }
        public bool esCharSeparacion(char c)
        {
            return !(Char.IsLetterOrDigit(c));
        }
        //apoyo Contextos

        public DatosNombreNumerico esNombreNumericoSimpleDesdeELPrincipio_ModificaContexto(int numeroDeSerie, bool detenerSiEncuentraPatronAlfinal = true)
        {
            int indice = orIndice(numeroDeSerie, getConf().nombresNumericosCompletosSimples);
            if (indice != -1)
            {
                //para confirmar que es un nombre numerico simple debe de tener la 
                //informacion del capitulo al final como patron ()que seguro difiere del numero incial
                DatosNombreNumerico d = new DatosNombreNumerico();
                int indiceDeBusqueda = getConf().nombresNumericosCompletosSimples[indice].ToString().Length;
                if (!(this is BuscadorDePatronesDeSerieAlFinal))
                {
                    d.D = getPr().buscarPatronesAlFinal(indiceDeBusqueda);
                    if (d.D != null && detenerSiEncuentraPatronAlfinal)
                    {
                        return d;
                    }
                }


                d.loEs = _esNombreNumericoSimple_ModificaContexto(numeroDeSerie, d.D, true);//indice,
                if (d.loEs != null)
                {
                    return d;
                }
            }
            return null;
        }

        public DatosNombreNumerico esNombreNumericosCompletosMultiplesDesdeElPrincipio_ModificaContexto(string nombre, int I0, int numero, int indiceInicialDelNumero, bool detenerSiEncuentraPatronAlfinal = true)
        {

            int lengDelNombre = getPr().esNombreNumericosMultiples_buscarDesdeElPrincipio_indiceFinal(nombre, numero, indiceInicialDelNumero);
            if (lengDelNombre != -1)
            {
                //if (indice != -1) {
                DatosNombreNumerico d = new DatosNombreNumerico();
                //HistoriarDeBusqueda h = pr.historialDeBusqueda;
                //d.D = h.buscarPatronesAlFinal(nombre, I0);
                if (!(this is BuscadorDePatronesDeSerieAlFinal))
                {
                    d.D = getPr().buscarPatronesAlFinal(I0);
                    //d.D = buscarPatronesAlFinal(nombre, I0);
                    if (detenerSiEncuentraPatronAlfinal && d.D != null)
                    {
                        return d;
                    }
                }
                string nombreDeSerie = subs(nombre, indiceInicialDelNumero, lengDelNombre);
                d.loEs = esNombreNumericosCompletosMultiples_ModificaContexto(nombreDeSerie, d.D, true);
                if (d.loEs)
                {
                    return d;
                }
                //}

            }
            return null;
        }

        public bool esNombreNumericoSimple_ModificaContexto(int numeroDeSerie, DatosDeNombreCapituloDelFinal df = null)
        {
            DatosNombreNumerico d = esNombreNumericoSimpleDesdeELPrincipio_ModificaContexto(numeroDeSerie, false);
            return d != null && d.loEs;
            //			int indice = orIndice(numeroDeSerie, getConf().nombresNumericosCompletosSimples);
            //			if (indice != -1) {
            //					
            //				return _esNombreNumericoSimple_ModificaContexto(numeroDeSerie, df);
            //				
            //					
            //					
            //			}// fin del if coincidencia con nombre numero simple
            //			return false;
        }



        public bool esNombreNumericosCompletosMultiples_ModificaContexto(string nombre, int numero, int indiceInicialDelNumero)
        {
            DatosNombreNumerico d = esNombreNumericosCompletosMultiplesDesdeElPrincipio_ModificaContexto(nombre, 0, numero, indiceInicialDelNumero, false);
            return d != null && d.loEs;
            //			int lengDelNombre = getPr().esNombreNumericosMultiples_buscarDesdeElPrincipio_indiceFinal(nombre, numero, indiceInicialDelNumero);
            //			if (lengDelNombre != -1) {
            //				//string nombreDeSerie = subs(nombre, indiceInicialDelNumero, lengDelNombre);
            //				
            //				
            //				//return esNombreNumericosCompletosMultiples_ModificaContexto(nombreDeSerie,null,true);
            //			}


            //return false;
        }

        public bool esNombreRodeadoDeNumeros_AlPrincipio_ModificaContexto(int numero, bool detenerSiEncuentraPatronesAlFinal = true)
        {
            DatosNombreNumerico dn = esNombreRodeadoDeNumeros_AlPrincipio_ModificaContexto_Dn(numero, detenerSiEncuentraPatronesAlFinal);
            return dn != null && dn.loEs;
        }

        public DatosNombreNumerico esNombreRodeadoDeNumeros_AlPrincipio_ModificaContexto_Dn(int numero, bool detenerSiEncuentraPatronesAlFinal = true)
        {

            if (getConf().NombresRodeadosDeNumeros != null)
            {
                DatosNombreNumerico dn = new DatosNombreNumerico();
                //int ind = Utiles.startsWith_Indice(nombre, getConf().NombresRodeadosDeNumeros);
                int lengNombre = getPr().esNombreRodeadosDeNumeros_buscarDesdeElPrincipio_indiceFinal(this.nombre, numero, 0);

                if (lengNombre != -1)
                {
                    string nombreDeEstaSerie = subs(this.nombre, 0, lengNombre);//getConf().NombresRodeadosDeNumeros[ind];
                    DatosDeNombreCapituloDelFinal df = null;

                    if (!(this is BuscadorDePatronesDeSerieAlFinal))
                    {
                        df = getPr().buscarPatronesAlFinal(0);
                        dn.D = df;
                        if (detenerSiEncuentraPatronesAlFinal && df != null)
                        {
                            dn.D = df;
                            return dn;
                            //return true;
                        }
                    }
                    dn.loEs = esNombreRodeadoDeNumeros_ModificaContexto(nombreDeEstaSerie, dn.D, true);
                    if (dn.loEs)
                    {
                        dn.D = df;
                        return dn;
                    }


                }
            }//fin si hay cf.NombresRodeadosDeNumeros 
             //		
            return null;
        }






        //para contexto y tipos de nombre
        private ContextoDeConjuntoDeSeries.CaracteristicaCapitulos[] getCaracteristicasEsDeNombre_Menos(ContextoDeConjuntoDeSeries.CaracteristicaCapitulos e)
        {
            ContextoDeConjuntoDeSeries.CaracteristicaCapitulos[] T = getCtxCn().getCaracteristicasDeTipoEsNombreDeSerie();
            ContextoDeConjuntoDeSeries.CaracteristicaCapitulos[] R = new ContextoDeConjuntoDeSeries.CaracteristicaCapitulos[T.Length - 1];
            bool fueEncontrado = false;
            for (int i = 0; i < T.Length; i++)
            {
                ContextoDeConjuntoDeSeries.CaracteristicaCapitulos a = T[i];
                if (a != e)
                {
                    R[fueEncontrado ? i - 1 : i] = a;

                }
                else
                {
                    fueEncontrado = true;
                }
            }
            return R;
        }


        protected void agregarTipoDeNombreDeSerieAlContexto(TipoDeNombreDeSerie? t, object nombreDeSerie)
        {
            KeySerie k = new KeySerie(Nombre: nombreDeSerie.ToString()

                                    , Clave: getPr().crearClave(nombreDeSerie.ToString())

                                    , TipoDeSerie: t != null ? (TipoDeNombreDeSerie)t : TipoDeNombreDeSerie.DESCONOCIDO);

            getCtxCn().agregarPropiedadesAContexto_Encontrado(k, getCtx().Url);
            //			if (t != null) {
            //				string url = getCtx().Url;
            //				switch (t) {
            //					case TipoDeNombreDeSerie.NUMERO_AL_FINAL:
            //						getCtxCn().agregarPropiedadesAContextoNombreNumeroFinal((string)nombreDeSerie, url);
            //						break;
            //					case TipoDeNombreDeSerie.NUMERO_AL_PRINCIPIO:
            //						getCtxCn().agregarPropiedadesAContextoNombreNumeroInicial((string)nombreDeSerie, url);
            //						break;
            //					case TipoDeNombreDeSerie.NUMERO_INTERNO:
            //						getCtxCn().agregarPropiedadesAContextoNombreUnNumeroInterno((string)nombreDeSerie, url);
            //						break;
            //					case TipoDeNombreDeSerie.NUMEROS_MULTIPLES_AL_FINAL:
            //						getCtxCn().agregarPropiedadesAContextoNombreNumerosMultiplesFinal((string)nombreDeSerie, url);
            //						break;
            //					case TipoDeNombreDeSerie.NUMEROS_MULTIPLES_AL_PRINCIPIO:
            //						getCtxCn().agregarPropiedadesAContextoNombreNumerosMultiplesInicial((string)nombreDeSerie, url);
            //						break;
            //					case TipoDeNombreDeSerie.RODEADO_DE_NUMEROS:
            //						getCtxCn().agregarPropiedadesAContextoNombreRodeadoDeNumeros((string)nombreDeSerie, url);
            //						break;
            //					case TipoDeNombreDeSerie.SOLO_NUMEROS_MULTIPLES:
            //						getCtxCn().agregarPropiedadesAContextoNumeroMultiple((string)nombreDeSerie, url);
            //						break;
            //					case TipoDeNombreDeSerie.SOLO_UN_NUMERO:
            //						int numero = nombreDeSerie is string ? inT(((string)nombreDeSerie).Trim()) : (int)nombreDeSerie;
            //						getCtxCn().agregarPropiedadesAContextoNumeroSimple(numero, url);
            //						break;
            //				}
            //			}
            //			
        }

        private bool intentarModificarContextoPorTipoDeNombre<E>(E nombreDeSerie
                                                                 , DatosDeNombreCapituloDelFinal df
                                                                 , ContextoDeConjuntoDeSeries.CaracteristicaCapitulos hay_de_este
                                                                 , ContextoDeConjuntoDeSeries.CaracteristicaCapitulos es_de_este
                                                                 , Action accionAlConfirmar

        //		                                                          , Func<bool> contieneEsteNombre


        //		                                                        , Func<bool> busquedaConjuntoDeNombresPor
        //		                                                       , Func<bool> contieneNombreDeEsteTipo

        )
        {
            if (getCtxCn().NOMBRES_SIN_MOVER_NUMERO_ADELANTE_SEGURO())
            {
                accionAlConfirmar();
                return true;
            }
            if (getCtxCn().SON_SOLO_NOMBRES_DE_SERIES())
            {
                accionAlConfirmar();
                return true;
            }

            if (getCtxCn().contieneNombre(getCtx(), nombreDeSerie.ToString()))
            {
                //if (contieneEsteNombre()) {
                accionAlConfirmar();
                return true;
            }

            if (!getCtxCn().containsOR_caracteristicaDeLosCapitulosAnalizados(
                    getCaracteristicasEsDeNombre_Menos(es_de_este)
                ))
            {

                if (!getCtxCn().EsElPrimeroEnSerAnalizado)
                {
                    if (getCtxCn().containsAll_caracteristicaDeLosCapitulosAnalizados(
                            hay_de_este
                        ))
                    {
                        if (getCtxCn().containsAll_caracteristicaDeLosCapitulosAnalizados(
                                es_de_este
                            ))
                        {
                            return false;//pq si llego aqui es que no encontro el nombre numerico
                                         //simple en la lista, lo que significa que (como se sabe el nombre de la serie
                                         //es un numerico simple) al menos no es el conocido que debe ser
                        }
                    }
                }

                if (df != null)
                {
                    if (getPr().getNombreRecortadoSinEnvoltorios(df.IndiceDelFinalDeNombre) == nombreDeSerie.ToString())
                    {
                        accionAlConfirmar();
                        return true;
                    }

                }

            }

            //			if (getCtxCn().EsElPrimeroEnSerAnalizado) {
            //				if (getCtxCn().SE_DESCONOCE()) { 
            //					
            ////					if (busquedaConjuntoDeNombresPor()) {
            ////						accionAlConfirmar();
            ////						return true;
            ////					}
            ////					
            //					if (df != null) {
            //						if (getPr().getNombreRecortadoSinEnvoltorios(df.IndiceDelFinalDeNombre) == nombreDeSerie.ToString()) {
            //							accionAlConfirmar();
            //							return true;
            //						}
            //						
            //					}
            ////					
            //				}
            //			} else {
            //				
            //				if (!getCtxCn().containsOR_caracteristicaDeLosCapitulosAnalizados(
            //					    getCaracteristicasEsDeNombre_Menos(es_de_este)
            //				    )) {
            //					
            //					
            //					if (getCtxCn().containsAll_caracteristicaDeLosCapitulosAnalizados(
            //						    hay_de_este
            //					    )) {
            //
            ////						if (contieneNombreDeEsteTipo()) {
            ////							accionAlConfirmar();
            ////							return true;
            ////						}
            //						if (getCtxCn().containsAll_caracteristicaDeLosCapitulosAnalizados(
            //							    es_de_este
            //						    )) {
            //							return false;//pq si llego aqui es que no encontro el nombre numerico
            //							//simple en la lista, lo que significa que (como se sabe el nombre de la serie
            //							//es un numerico simple) al menos no es el conocido que debe ser
            //						}
            //					}//fin del if HAY_NOMBRES_NUMERICOS_SIMPLES
            //								
            ////					if (busquedaConjuntoDeNombresPor()) {
            ////						accionAlConfirmar();
            ////						return true;
            ////					}
            //					if (df != null) {
            //						if (getPr().getNombreRecortadoSinEnvoltorios(df.IndiceDelFinalDeNombre) == nombreDeSerie.ToString()) {
            //							accionAlConfirmar();
            //							return true;
            //						}
            //						
            //					}
            //				}//fin del if NOMBRES_DE_SERIE_ES_NORMAL||NOMBRES_DE_SERIE_ES_NUMERO_MULTIPLE
            //				
            //							
            //			}//fin del else


            return false;
            //-----------------------
        }
        //fin funcion


        public bool _esNombreNumericoSimple_ModificaContexto(int numeroDeSerie, DatosDeNombreCapituloDelFinal df = null, bool fueEncontradoElNombreEnCF = false)
        {
            Action accionAlConfirmar = () =>
            {

                //getCtxCn().agregarPropiedadesAContextoNumeroSimple(numeroDeSerie,getCtx().Url);
                setTipoDeNombre(TipoDeNombreDeSerie.SOLO_UN_NUMERO);
                agregarTipoDeNombreDeSerieAlContexto(this.tipoDeNombreDeSerie, numeroDeSerie);

            };
            if (fueEncontradoElNombreEnCF)
            {
                if (getCtx().EsSoloNombre
                    || getCtx().EsCarpeta
                    || (getCtx().EsVideo && (df != null && (df.isEmpty_Capitulos()))))
                {
                    accionAlConfirmar();
                    return true;
                }

            }
            return intentarModificarContextoPorTipoDeNombre<int>(
                nombreDeSerie: numeroDeSerie
            , df: df
            , hay_de_este: ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.HAY_NOMBRES_NUMERO_SIMPLE
            , es_de_este: ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.NOMBRES_DE_SERIE_ES_NUMERO_SIMPLE
            , accionAlConfirmar: accionAlConfirmar
            //			, busquedaConjuntoDeNombresPor: () => getCtxCn().busquedaConjuntoDeNombresPorNumeroSimple(getCtx(), numeroDeSerie)
            //			, contieneNombreDeEsteTipo: () => getCtxCn().contieneNombres_NumericosSimples_Encontrados(numeroDeSerie)
            );
            //			
        }
        public bool esNombreNumericosCompletosMultiples_ModificaContexto(string nombreDeEstaSerie, DatosDeNombreCapituloDelFinal df = null, bool fueEncontradoElNombreEnCF = false)//int indice
        {
            Action accionAlConfirmar = () =>
            {
                //getCtxCn().agregarPropiedadesAContextoNumeroMultiple(nombreDeEstaSerie,getCtx().Url);
                setTipoDeNombre(TipoDeNombreDeSerie.SOLO_NUMEROS_MULTIPLES);
                agregarTipoDeNombreDeSerieAlContexto(this.tipoDeNombreDeSerie, nombreDeEstaSerie);

            };
            if (fueEncontradoElNombreEnCF)
            {
                if (getCtx().EsSoloNombre
                    || getCtx().EsCarpeta
                    || (getCtx().EsVideo && (df != null && (df.isEmpty_Capitulos()))))
                {
                    accionAlConfirmar();
                    return true;
                }

            }
            return intentarModificarContextoPorTipoDeNombre<string>(
                nombreDeSerie: nombreDeEstaSerie
            , df: df
            , hay_de_este: ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.HAY_NOMBRES_NUMERICOS_MULTIPLES
            , es_de_este: ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.NOMBRES_DE_SERIE_ES_NUMERO_MULTIPLE
            , accionAlConfirmar: accionAlConfirmar
            //			, busquedaConjuntoDeNombresPor: () => getCtxCn().BusquedaEnConjuntoDeNombres(getCtx(), nombreDeEstaSerie, () => getCtxCn().agregarPropiedadesAContextoNumeroMultiple(nombreDeEstaSerie, getCtx().Url))
            //			, contieneNombreDeEsteTipo: () => getCtxCn().contieneNombres_NumericosMultiples_Encontrados(nombreDeEstaSerie)
            );
            //			
        }
        public bool esNombreRodeadoDeNumeros_ModificaContexto(string nombreDeEstaSerie, DatosDeNombreCapituloDelFinal df = null, bool fueEncontradoElNombreEnCF = false)
        {
            Action accionAlConfirmar = () =>
            {
                //getCtxCn().agregarPropiedadesAContextoNombreRodeadoDeNumeros(nombreDeEstaSerie,getCtx().Url);
                setTipoDeNombre(TipoDeNombreDeSerie.RODEADO_DE_NUMEROS);
                agregarTipoDeNombreDeSerieAlContexto(this.tipoDeNombreDeSerie, nombreDeEstaSerie);

            };
            if (fueEncontradoElNombreEnCF)
            {
                accionAlConfirmar();
                return true;
            }
            return intentarModificarContextoPorTipoDeNombre<string>(
                nombreDeSerie: nombreDeEstaSerie
            , df: df
            , hay_de_este: ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.HAY_NOMBRES_RODEADOS_DE_NUMEROS
            , es_de_este: ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.NOMBRES_DE_SERIE_ES_RODEADOS_DE_NUMEROS
            , accionAlConfirmar: accionAlConfirmar
            //			, busquedaConjuntoDeNombresPor: () => getCtxCn().BusquedaEnConjuntoDeNombres(getCtx(), nombreDeEstaSerie, () => getCtxCn().agregarPropiedadesAContextoNombreRodeadoDeNumeros(nombreDeEstaSerie, getCtx().Url))
            //			, contieneNombreDeEsteTipo: () => getCtxCn().contieneNombres_RodeadosDeNumeros_Encontrados(nombreDeEstaSerie)
            );

            //			
        }


        public bool esNombreNumeroInicial_ModificaContexto(string nombreDeEstaSerie, DatosDeNombreCapituloDelFinal df = null, bool fueEncontradoElNombreEnCF = false)
        {
            Action accionAlConfirmar = () =>
            {
                //getCtxCn().agregarPropiedadesAContextoNombreNumeroInicial(nombreDeEstaSerie,getCtx().Url);
                setTipoDeNombre(TipoDeNombreDeSerie.NUMERO_AL_PRINCIPIO);
                agregarTipoDeNombreDeSerieAlContexto(this.tipoDeNombreDeSerie, nombreDeEstaSerie);

            };
            if (fueEncontradoElNombreEnCF)
            {
                accionAlConfirmar();
                return true;
            }
            return intentarModificarContextoPorTipoDeNombre<string>(
                nombreDeSerie: nombreDeEstaSerie
            , df: df
            , hay_de_este: ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.HAY_NOMBRES_NUMERO_AL_PRINCIPIO
            , es_de_este: ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.NOMBRES_DE_SERIE_ES_NUMERO_AL_PRINCIPIO
            , accionAlConfirmar: accionAlConfirmar
            //			, busquedaConjuntoDeNombresPor: () => getCtxCn().BusquedaEnConjuntoDeNombres(getCtx(), nombreDeEstaSerie, () => getCtxCn().agregarPropiedadesAContextoNombreNumeroInicial(nombreDeEstaSerie, getCtx().Url))
            //			, contieneNombreDeEsteTipo: () => getCtxCn().contieneNombres_NumeroAlPrincipio_Encontrados(nombreDeEstaSerie)
            );

            //			
        }
        public bool esNombreNumeroFinal_ModificaContexto(string nombreDeEstaSerie, DatosDeNombreCapituloDelFinal df = null, bool fueEncontradoElNombreEnCF = false)
        {
            Action accionAlConfirmar = () =>
            {
                //getCtxCn().agregarPropiedadesAContextoNombreNumeroFinal(nombreDeEstaSerie,getCtx().Url);
                setTipoDeNombre(TipoDeNombreDeSerie.NUMERO_AL_FINAL);
                agregarTipoDeNombreDeSerieAlContexto(this.tipoDeNombreDeSerie, nombreDeEstaSerie);

            };
            if (fueEncontradoElNombreEnCF)
            {
                accionAlConfirmar();
                return true;
            }
            return intentarModificarContextoPorTipoDeNombre<string>(
                nombreDeSerie: nombreDeEstaSerie
            , df: df
            , hay_de_este: ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.HAY_NOMBRES_NUMERO_AL_FINAL
            , es_de_este: ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.NOMBRES_DE_SERIE_ES_NUMERO_AL_FINAL
            , accionAlConfirmar: accionAlConfirmar
            //			, busquedaConjuntoDeNombresPor: () => getCtxCn().BusquedaEnConjuntoDeNombres(getCtx(), nombreDeEstaSerie, () => getCtxCn().agregarPropiedadesAContextoNombreNumeroFinal(nombreDeEstaSerie, getCtx().Url))
            //			, contieneNombreDeEsteTipo: () => getCtxCn().contieneNombres_NumeroAlFinal_Encontrados(nombreDeEstaSerie)
            );

            //			
        }
        public bool esNombreNumerosMultiplesInicial_ModificaContexto(string nombreDeEstaSerie, DatosDeNombreCapituloDelFinal df = null, bool fueEncontradoElNombreEnCF = false)
        {
            Action accionAlConfirmar = () =>
            {
                //getCtxCn().agregarPropiedadesAContextoNombreNumerosMultiplesInicial(nombreDeEstaSerie,getCtx().Url);
                setTipoDeNombre(TipoDeNombreDeSerie.NUMEROS_MULTIPLES_AL_PRINCIPIO);
                agregarTipoDeNombreDeSerieAlContexto(this.tipoDeNombreDeSerie, nombreDeEstaSerie);

            };
            if (fueEncontradoElNombreEnCF)
            {
                accionAlConfirmar();
                return true;
            }
            return intentarModificarContextoPorTipoDeNombre<string>(
                nombreDeSerie: nombreDeEstaSerie
            , df: df
            , hay_de_este: ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.HAY_NOMBRES_NUMEROS_MULTIPLES_AL_PRINCIPIO
            , es_de_este: ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.NOMBRES_DE_SERIE_ES_NUMEROS_MULTIPLES_AL_PRINCIPIO
            , accionAlConfirmar: accionAlConfirmar
            //			, busquedaConjuntoDeNombresPor: () => getCtxCn().BusquedaEnConjuntoDeNombres(getCtx(), nombreDeEstaSerie, () => getCtxCn().agregarPropiedadesAContextoNombreNumerosMultiplesInicial(nombreDeEstaSerie, getCtx().Url))
            //			, contieneNombreDeEsteTipo: () => getCtxCn().contieneNombres_NumerosMultiplesAlPrincipio_Encontrados(nombreDeEstaSerie)
            );

            //			
        }
        public bool esNombreNumerosMultiplesFinal_ModificaContexto(string nombreDeEstaSerie, DatosDeNombreCapituloDelFinal df = null, bool fueEncontradoElNombreEnCF = false)
        {
            Action accionAlConfirmar = () =>
            {
                //getCtxCn().agregarPropiedadesAContextoNombreNumerosMultiplesFinal(nombreDeEstaSerie,getCtx().Url);

                setTipoDeNombre(TipoDeNombreDeSerie.NUMEROS_MULTIPLES_AL_FINAL);
                agregarTipoDeNombreDeSerieAlContexto(this.tipoDeNombreDeSerie, nombreDeEstaSerie);
            };
            if (fueEncontradoElNombreEnCF)
            {
                accionAlConfirmar();
                return true;
            }
            return intentarModificarContextoPorTipoDeNombre<string>(
                nombreDeSerie: nombreDeEstaSerie
            , df: df
            , hay_de_este: ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.HAY_NOMBRES_NUMEROS_MULTIPLES_AL_FINAL
            , es_de_este: ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.NOMBRES_DE_SERIE_ES_NUMEROS_MULTIPLES_AL_FINAL
            , accionAlConfirmar: accionAlConfirmar
            //			, busquedaConjuntoDeNombresPor: () => getCtxCn().BusquedaEnConjuntoDeNombres(getCtx(), nombreDeEstaSerie, () => getCtxCn().agregarPropiedadesAContextoNombreNumerosMultiplesFinal(nombreDeEstaSerie, getCtx().Url))
            //			, contieneNombreDeEsteTipo: () => getCtxCn().contieneNombres_NumerosMultiplesAlFinal_Encontrados(nombreDeEstaSerie)
            );

            //			
        }
        public bool esNombreUnNumeroInterno_ModificaContexto(string nombreDeEstaSerie, DatosDeNombreCapituloDelFinal df = null, bool fueEncontradoElNombreEnCF = false)
        {
            Action accionAlConfirmar = () =>
            {
                //getCtxCn().agregarPropiedadesAContextoNombreUnNumeroInterno(nombreDeEstaSerie,getCtx().Url);
                setTipoDeNombre(TipoDeNombreDeSerie.NUMERO_INTERNO);
                agregarTipoDeNombreDeSerieAlContexto(this.tipoDeNombreDeSerie, nombreDeEstaSerie);

            };
            if (fueEncontradoElNombreEnCF)
            {
                accionAlConfirmar();
                return true;
            }
            return intentarModificarContextoPorTipoDeNombre<string>(
                nombreDeSerie: nombreDeEstaSerie
            , df: df
            , hay_de_este: ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.HAY_NOMBRES_UN_NUMERO_INTERNO
            , es_de_este: ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.NOMBRES_DE_SERIE_ES_UN_NUMERO_INTERNO
            , accionAlConfirmar: accionAlConfirmar
            //			, busquedaConjuntoDeNombresPor: () => getCtxCn().BusquedaEnConjuntoDeNombres(getCtx(), nombreDeEstaSerie, () => getCtxCn().agregarPropiedadesAContextoNombreUnNumeroInterno(nombreDeEstaSerie, getCtx().Url))
            //			, contieneNombreDeEsteTipo: () => getCtxCn().contieneNombres_UnNumeroInterno_Encontrados(nombreDeEstaSerie)
            );

            //			
        }

    }
}



//
//
//public bool esNumeroParteDeNombre(string texto, int numero, int indiceInicialDelNumero, int indiceAContinuacion)
//		{
//
//			string subTexto = subs(texto, 0, indiceInicialDelNumero);
//			int lengIgnorarNumeroEspecificoDetrasDe =getConf().ignorarNumeroEspecificoDetrasDe.Length;
//			int indice = -1;
//			for (int j = 0; j < lengIgnorarNumeroEspecificoDetrasDe; j++) {
//				CondicionIgnorarNumeroEspecifico c =getConf().ignorarNumeroEspecificoDetrasDe[j];
//				if (c.Numero == numero) {
//
//					indice = Utiles.endsWith_Indice(subTexto, c.Caracteres);
//					if (indice != -1) {
//						//algo negativo
//						return true;
//					}
//				} else {
//					if (c.Numero > numero) {
////						break;
//					}
//				}
//			}//fin for
//
//
//			int lengIgnorarNumeroEspecificoDelanteDe =getConf().ignorarNumeroEspecificoDelanteDe.Length;
//			indice = -1;
//			for (int j = 0; j < lengIgnorarNumeroEspecificoDelanteDe; j++) {
//				CondicionIgnorarNumeroEspecifico c =getConf().ignorarNumeroEspecificoDelanteDe[j];
//				if (c.Numero == numero) {
//					//cwl(str(c.Caracteres));
//					indice = Utiles.startsWith_Indice(nombre, indiceAContinuacion, c.Caracteres);
//					if (indice != -1) {
//						//algo negativo
//						//return new DatosDeIgnorarNumero(indiceAContinuacion + c.Caracteres[indice].Length);
//						return true;
//					}
//				} else {
//					if (c.Numero > numero) {
//						break;
//					}
//				}
//			}
//
//
//
//			return false;
//		}
//
