/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 31/7/2022
 * Hora: 11:56
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
//using ReneUtiles.Clases.Multimedia.Series.Procesadores.Ignorar;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Conjuntos;
using ReneUtiles.Clases.Multimedia.Series.Procesadores;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos;
using ReneUtiles.Clases.ExprecionesRegulares;

namespace ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores
{
	/// <summary>
	/// Description of BuscadorDeFechasEnNombre.
	/// </summary>
	public class BuscadorDeFechasEnNombre:BuscadorDeDatosEnNombre
	{
        public List<DatosDeFechaEnNombre> fechasEnNombre;
        public MatchCollection mc;
        public BuscadorDeFechasEnNombre(ProcesadorDeNombreDeSerie pr) : base(pr)
        {
            this.fechasEnNombre = new List<DatosDeFechaEnNombre>();
        }
        private RecursosDePatronesDeSeries getRe()
        {
            return this.pr.re;
        }
        public List<DatosDeFechaEnNombre> getFechasEnNombre()
        {
            return this.fechasEnNombre;
        }
        public List<DatosDeFechaEnNombre> buscarFechas(int i0)
        {
            if (seBuscoCon(i0))
            {
                return fechasEnNombre;
            }
            this.seBusco = true;
            this.I0 = i0;

            Regex exprecion = getRe().reg.refechas.Re_Fecha.SSfreSfS;


            this.mc = exprecion.Matches(this.nombre, i0);

            foreach (Match m in mc)
            {
                Group gF = getRe().reg.refechas.getGrupoFecha(m);
                Group gND = getRe().reg.refechas.getGrupoDia(m);
                Group gNM = getRe().reg.refechas.getGrupoMes(m);
                Group gNA = getRe().reg.refechas.getGrupoAño(m);
                Group gEI = getRe().reg.refechas.getGrupoEnvolturaInicialFecha(m);
                if (gF.Success)
                {
                    if (gND.Success && gNA.Success && gNM.Success)
                    {
                        DatosDeNumeroEnteroEncontrado da = Matchs.getPrimerNumerEntero(this.nombre);
                        if (esNombreNumericosCompletosMultiples_ModificaContexto(nombre: this.nombre
                                                                                , numero: da.numero
                                                                                , indiceInicialDelNumero: da.indiceInicial//gF.Index
                                                                               ))
                        {

                            continue;
                        }
                    }

                    DatosDeFechaEnNombre d = new DatosDeFechaEnNombre();
                    d.AñoStr = gNA.ToString();
                    d.indiceDeteccionDeAño = gNA.Index;

                    if ((!gND.Success) && (!gNM.Success)
                             && gNA.Success && (!gEI.Success))
                    {

                        if (esNumeroParteDeNombre(
                            numero: (int)d.Año,
                            indiceInicialDelNumero: (int)d.indiceDeteccionDeAño
                        //	,m.Index+m.Length
                        ))
                        {
                            continue;
                        }

                        if (esNombreNumericoSimple_ModificaContexto((int)d.Año))
                        {
                            continue;
                        }
                    }

                    if (gND.Success)
                    {
                        d.DiaStr = gND.ToString();
                        d.indiceDeteccionDeDia = gND.Index;
                    }
                    if (gNM.Success)
                    {
                        d.MesStr = gNM.ToString();
                        d.indiceDeteccionDeMes = gNM.Index;
                    }
                    d.fechaStr = gF.ToString();
                    d.indiceDeteccionDeFecha = gF.Index;

                    d.indiceAContinuacion = m.Index + m.Length;
                    this.fechasEnNombre.Add(d);
                }
            }

            return this.fechasEnNombre;
        }

        public bool estaDentroDeFecha(int indice)
        {

            foreach (DatosDeFechaEnNombre d in fechasEnNombre)
            {
                if (d != null && d.estaDentroDeLosLimites(indice))
                {
                    return true;
                }
            }
            return false;
            //return this.fechaEnNombre!=null?this.fechaEnNombre.estaDentroDeLosLimites(indice):false;
        }


        public DatosDeIgnorarNumero estaDentroDeFecha_DatosDeIgnorarNumero(int indice){
			
			foreach (DatosDeFechaEnNombre d in fechasEnNombre) {
				DatosDeIgnorarNumero dd=null;
				if(d!=null){
					dd=d.estaDentroDeLosLimites_DatosDeIgnorarNumero(indice);
					if(dd!=null){
						return dd;
					}
					
				}
			}
			return null;
			//return this.fechaEnNombre!=null?this.fechaEnNombre.estaDentroDeLosLimites(indice):false;
		}
	
	}
}
