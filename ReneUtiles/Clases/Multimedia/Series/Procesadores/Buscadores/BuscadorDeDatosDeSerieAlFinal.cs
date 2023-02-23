/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 30/7/2022
 * Hora: 16:44
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
	/// Description of BuscadorDatosDeSerieAlPrincipio.
	/// </summary>
	public class BuscadorDeDatosDeSerieAlFinal:BuscadorDeDatosDeSerie
	{
		public BuscadorDeDatosDeSerieAlFinal(ProcesadorDeNombreDeSerie pr): base(pr)
		{
		}
		
		protected override void initD()
		{
			if (d == null) {
				d =crearDatosDeNombreCapituloDelFinal(); 
				
			}
			alIniciarD();
		}
		public DatosDeNombreCapituloDelFinal getD(){
			return (DatosDeNombreCapituloDelFinal)this.d;
		}
		
		
		
		public  DatosDeNombreCapituloDelFinal getCapitulosDeNombreDelFinal( int I0)
		{
			if(this.seBusco&&this.I0 <= I0){
				return this.getD();
			}
			this.I0 = I0;
			this.seBusco=true;
            //if (nombre == "Virtual_Hero_-_1x04-medium")
            //{
            //    cwl("aqui");
            //}

            //cwl("nombre="+nombre);
            //			if (isEmptyFull(nombre)) {
            //				return null;
            //			}
            if (I0 >= nombre.Length) {
				return null;
			}
			//if(this.nombre=="American Horror History S09E01 [720p] [Dual Audio]"){
			////	cwl("aqui!!");
			//}
			//DatosDeNombreCapituloDelFinal d = null;
			d = this.pr.buscarPatronesAlFinal( I0);
			if (d != null) {
				//cwl("d.EsContenedorDeTemporada="+d.EsContenedorDeTemporada);
				return getD();
			}
			initD();
			if (this.pr.re.cf.EsParaAnime) {//&& (d == null || ((!d.EsOVA) && (!d.HayOvasEnElContendor)))
				if (this.pr.re.Re_Ova.SSfreSfS.Match(nombre, I0).Success) {
					d.EsOVA = true;
				}
			}//1207


            //try {
                //fin de busqueda de patrones  //
                StringNumberTokenizer stk = new StringNumberTokenizer(nombre, I0, false);
                int lengPalabras = stk.Palabra.Length;
                bool esElPrimero = true;
                for (int i = 0; i < 3 && stk.HayNextToken; i++)
                {
                    StringToken tk = stk.next();
                    if (tk.Token.Length > 4)
                    {
                        continue;
                    }
                    //string a = tk.Token;
                    int numero = (int)tk.TokenInt;
                    //cwl("numero="+numero);
                    //cwl("tk.IndiceInicial="+tk.IndiceInicial);
                    int indice = -1;
                    //string numeroMasSeparacion=getNumeroMasSeparacion(nombre,numero,tk.IndiceInicial);
                    int indiceAcontinuacion = getIndiceAContinuacionDeSeparacionDespuesDeNumero(nombre, tk.ToString(), tk.IndiceInicial);
                //if (nombre == "12 Monkeys")
                //{
                //    cwl("aqui");
                //}

                DatosDeIgnorarNumero di = getEs_IgnorarNumeroDelanteDe(numeroFinal: numero
                                                                           , indiceInicialNumero: tk.IndiceInicial
                                                                           , indiceAContinuacion: indiceAcontinuacion);
                    if (di != null)
                    {
                        if (di.EsAleaterizacion)
                        {
                            return null;
                        }
                        //agregado
                        stk.saltaHasta(di.IndiceAContinuacion);
                        continue;
                    }
                    di = pr.estaDentroDeFecha_DatosDeIgnorarNumero(tk.IndiceInicial);
                    if (di != null)
                    {
                        stk.saltaHasta(di.IndiceAContinuacion);
                        continue;
                    }
                    if (esElPrimero)
                    {
                        esElPrimero = false;

                        //					indice = this.pr.esNombreNumericoSimple_ModificaContexto_Indice(numero);
                        //					if (indice != -1) {
                        //						stk.saltaHasta(pr.re.cf.nombresNumericosCompletosSimples[indice].ToString().Length);
                        //						continue;
                        //					}
                        //					indice = this.pr.esNombresNumericosCompletosMultiples_ModificaContexto(nombre, I0);
                        //					if (indice != -1) {
                        //						stk.saltaHasta(pr.re.cf.nombresNumericosCompletosMultiples[indice].Length);
                        //						continue;
                        //					}

                    }//fin del if esELPrimero

                    esElPrimero = false;


                    int lengCondicionesIgnorarNumeroEspecificoRodeadoPor = pr.re.cf.condicionesIgnorarNumeroEspecificoRodeadoPor.Length;
                    if (!tk.TerminaEnElLength)
                    {
                        bool saltar = false;
                        for (int j = 0; j < lengCondicionesIgnorarNumeroEspecificoRodeadoPor; j++)
                        {
                            CondicionIgnorarNumeroEspecificoRodeadoPor c = pr.re.cf.condicionesIgnorarNumeroEspecificoRodeadoPor[j];
                            if (c.Numero == numero)
                            {
                                //	cwl("tk.IndiceAContinuacion="+tk.IndiceAContinuacion);
                                indice = Utiles.startsWith_Indice(nombre, tk.IndiceAContinuacion, c.Despues);
                                if (indice != -1)
                                {
                                    //cwl("tk.IndiceInicial="+tk.IndiceInicial);
                                    //cwl("c.Antes="+c.Antes);
                                    indice = Utiles.endsWith_Indice(nombre, tk.IndiceInicial, c.Antes);
                                    if (indice != -1)
                                    {
                                        saltar = true;
                                        break;
                                    }

                                }
                            }
                            else
                            {
                                if (c.Numero > numero)
                                {
                                    break;
                                }
                            }
                        }
                        if (saltar)
                            continue;

                    }

                    if (esIgnorarNumeroDetrasDe(numero: numero
                                                , indiceInicialDelNumero: tk.IndiceInicial
                                                ))
                    {//, indiceAcontinuacion
                        return null;
                    }
                    //				if (d == null) {
                    //					d = crearDatosDeNombreCapituloDelFinal();
                    //				}				


                    //getCtxCn().agregarPropiedadesAContextoBasicasDeResultado();
                    //getCtxCn().agregarPropiedadesAContextoHAY_NOMBRES_NORMALES();
                    //getCtxCn().agregarPropiedadesAContexto
                    d.Capitulo = numero;
                    d.IndiceNumeroCapitulo = tk.IndiceInicial;
                    return getD();

                }//fin for tokenizer numero


            //} catch (Exception ex) {


            //    //fin de busqueda de patrones  //
            //    StringNumberTokenizer stk = new StringNumberTokenizer(nombre, I0, false);
            //    int lengPalabras = stk.Palabra.Length;
            //    bool esElPrimero = true;
            //    for (int i = 0; i < 3 && stk.HayNextToken; i++)
            //    {
            //        StringToken tk = stk.next();
            //        if (tk.Token.Length > 4)
            //        {
            //            continue;
            //        }
            //        //string a = tk.Token;
            //        int numero = (int)tk.TokenInt;
            //        //cwl("numero="+numero);
            //        //cwl("tk.IndiceInicial="+tk.IndiceInicial);
            //        int indice = -1;
            //        //string numeroMasSeparacion=getNumeroMasSeparacion(nombre,numero,tk.IndiceInicial);
            //        int inicialTK = tk.IndiceInicial;
            //        int indiceAcontinuacion = getIndiceAContinuacionDeSeparacionDespuesDeNumero(nombre, tk.ToString(), inicialTK);
            //        DatosDeIgnorarNumero di = getEs_IgnorarNumeroDelanteDe(numeroFinal: numero
            //                                                               , indiceInicialNumero: tk.IndiceInicial
            //                                                               , indiceAContinuacion: indiceAcontinuacion);
            //        if (di != null)
            //        {
            //            if (di.EsAleaterizacion)
            //            {
            //                return null;
            //            }
            //            //agregado
            //            stk.saltaHasta(di.IndiceAContinuacion);
            //            continue;
            //        }
            //        di = pr.estaDentroDeFecha_DatosDeIgnorarNumero(tk.IndiceInicial);
            //        if (di != null)
            //        {
            //            stk.saltaHasta(di.IndiceAContinuacion);
            //            continue;
            //        }
            //        if (esElPrimero)
            //        {
            //            esElPrimero = false;

            //            //					indice = this.pr.esNombreNumericoSimple_ModificaContexto_Indice(numero);
            //            //					if (indice != -1) {
            //            //						stk.saltaHasta(pr.re.cf.nombresNumericosCompletosSimples[indice].ToString().Length);
            //            //						continue;
            //            //					}
            //            //					indice = this.pr.esNombresNumericosCompletosMultiples_ModificaContexto(nombre, I0);
            //            //					if (indice != -1) {
            //            //						stk.saltaHasta(pr.re.cf.nombresNumericosCompletosMultiples[indice].Length);
            //            //						continue;
            //            //					}

            //        }//fin del if esELPrimero

            //        esElPrimero = false;


            //        int lengCondicionesIgnorarNumeroEspecificoRodeadoPor = pr.re.cf.condicionesIgnorarNumeroEspecificoRodeadoPor.Length;
            //        if (!tk.TerminaEnElLength)
            //        {
            //            bool saltar = false;
            //            for (int j = 0; j < lengCondicionesIgnorarNumeroEspecificoRodeadoPor; j++)
            //            {
            //                CondicionIgnorarNumeroEspecificoRodeadoPor c = pr.re.cf.condicionesIgnorarNumeroEspecificoRodeadoPor[j];
            //                if (c.Numero == numero)
            //                {
            //                    //	cwl("tk.IndiceAContinuacion="+tk.IndiceAContinuacion);
            //                    indice = Utiles.startsWith_Indice(nombre, tk.IndiceAContinuacion, c.Despues);
            //                    if (indice != -1)
            //                    {
            //                        //cwl("tk.IndiceInicial="+tk.IndiceInicial);
            //                        //cwl("c.Antes="+c.Antes);
            //                        indice = Utiles.endsWith_Indice(nombre, tk.IndiceInicial, c.Antes);
            //                        if (indice != -1)
            //                        {
            //                            saltar = true;
            //                            break;
            //                        }

            //                    }
            //                }
            //                else
            //                {
            //                    if (c.Numero > numero)
            //                    {
            //                        break;
            //                    }
            //                }
            //            }
            //            if (saltar)
            //                continue;

            //        }

            //        if (esIgnorarNumeroDetrasDe(numero: numero
            //                                    , indiceInicialDelNumero: tk.IndiceInicial
            //                                    ))
            //        {//, indiceAcontinuacion
            //            return null;
            //        }
            //        //				if (d == null) {
            //        //					d = crearDatosDeNombreCapituloDelFinal();
            //        //				}				


            //        //getCtxCn().agregarPropiedadesAContextoBasicasDeResultado();
            //        //getCtxCn().agregarPropiedadesAContextoHAY_NOMBRES_NORMALES();
            //        //getCtxCn().agregarPropiedadesAContexto
            //        d.Capitulo = numero;
            //        d.IndiceNumeroCapitulo = tk.IndiceInicial;
            //        return getD();

            //    }//fin for tokenizer numero

            //}

            
			
			return null;
		}
		
	}
}
