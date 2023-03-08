/*
 * Created by SharpDevelop.
 * User: Rene
 * Date: 3/10/2021
 * Time: 11:50
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;

using ReneUtiles.Clases.Multimedia;
using ReneUtiles.Clases.Multimedia.Relacionadores.Saltos;
using ReneUtiles.Clases;

namespace ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos
{
    /// <summary>
    /// Description of DatosDeNombreCapitulosDelFinal.
    /// </summary>
    public class DatosDeNombreCapituloDelFinal : DatosDeNombreCapitulo
    {
        private int indiceDelFinalDeNombre;
        private int indiceDelPrimerNumero;



        public DatosDeNombreCapituloDelFinal()
        {
            inicializar();
            this.indiceDelFinalDeNombre = -1;
            this.indiceDelPrimerNumero = -1;

        }
        public void iniT(DatosDeNombreCapituloDelFinal d)
        {
            base.iniT(d);
            this.indiceDelFinalDeNombre = d.indiceDelFinalDeNombre;
            this.indiceDelPrimerNumero = d.indiceDelPrimerNumero;


        }

        public int getIndiceDelFinalDeNombre()
        {
            int menor = -1;
            List<int> li = new List<int>();
            DatosDeIdentificacionIndividual[] D = {
                identificadorTemporada,identificadorCapitulo,identificadorCapituloOva
            };
            foreach (DatosDeIdentificacionIndividual d in D)
            {
                if (d != null)
                {
                    if (d.identificacionNumerica != null)
                    {
                        li.Add(d.identificacionNumerica.IndiceDeRepresentacionStr);
                    }
                    if (d.etiqueta != null)
                    {
                        li.Add(d.etiqueta.IndiceDeRepresentacionStr);
                    }
                }
            }
            DatosDeIdentificacionColectiva[] DI = {
                contendorDeCapitulos,contendorDeOvas,contendorTemporada
            };
            foreach (DatosDeIdentificacionColectiva d in DI)
            {
                if (d != null

                    )
                {
                    if (d.datosDelContenedor != null
                    && d.esDeEsteTipo//d.datosDelContenedor.esDeEsteTipo
                   )
                    {
                        if (!d.datosDelContenedor.numerosIndividuales.isEmpty())
                        {
                            li.Add(d.datosDelContenedor.numerosIndividuales.OrdenadosPorIndice.ElementAt(0).IndiceDeRepresentacionStr);
                        }
                        if (d.datosDelContenedor.numeroCantidad != null)
                        {
                            li.Add(d.datosDelContenedor.numeroCantidad.IndiceDeRepresentacionStr);
                        }
                    }


                    if (d.etiqueta != null)
                    {
                        li.Add(d.etiqueta.IndiceDeRepresentacionStr);
                    }

                }
            }
            li.Add(this.indiceDelFinalDeNombre);

            int[] indices = li.ToArray();
            //= {
            //    IndiceIdentificadorTemporada,
            //    IndiceIdentificadorCapitulos,
            //    IndiceIdentificadorCapitulo,
            //     this.indiceDelFinalDeNombre,
            //     IndiceTemporadaNumerosRomanos,
            //     IndiceDelPrimerNumero,

            //     IndiceNumeroTemporadaInicial

            //};
            //cwl(str(indices));   
            int end = indices.Length;
            for (int i = 0; i < end; i++)
            {
                int indice = indices[i];
                if (indice != -1 && (menor == -1 || indice < menor))
                {
                    menor = indice;
                }
            }
            return menor;
        }


        public int IndiceDelFinalDeNombre
        {
            get
            {
                return getIndiceDelFinalDeNombre();
                //int menor = -1;
                //int[] indices = {
                //	IndiceIdentificadorTemporada,
                //	IndiceIdentificadorCapitulos,
                //	IndiceIdentificadorCapitulo,
                //	 this.indiceDelFinalDeNombre,
                //	 IndiceTemporadaNumerosRomanos,
                //	 IndiceDelPrimerNumero,
                //                 IndiceNumeroTemporadaInicial

                //            };
                ////cwl(str(indices));   
                //int end = indices.Length;
                //for (int i = 0; i < end; i++) {
                //	int indice = indices[i];
                //	if (indice != -1 && (menor == -1 || indice < menor)) {
                //		menor = indice;
                //	}
                //}
                //return menor;
                ////return this.indiceDelFinalDeNombre;

            }
            set { this.indiceDelFinalDeNombre = value; }
        }
        public int IndiceDelPrimerNumero
        {
            get
            {
                List<int> li = new List<int>();
                DatosDeIdentificacionIndividual[] D = {
                identificadorTemporada,identificadorCapitulo,identificadorCapituloOva
            };
                foreach (DatosDeIdentificacionIndividual d in D)
                {
                    if (d != null
                        && d.identificacionNumerica != null
                        )
                    {
                        li.Add(d.identificacionNumerica.IndiceDeRepresentacionStr);
                    }
                }
                DatosDeIdentificacionColectiva[] DI = {
                contendorDeCapitulos,contendorDeOvas,contendorTemporada
            };
                foreach (DatosDeIdentificacionColectiva d in DI)
                {
                    if (d != null

                    )
                    {
                        if (d.datosDelContenedor != null
                        && d.esDeEsteTipo//d.datosDelContenedor.esDeEsteTipo
                       )
                        {
                            if (!d.datosDelContenedor.numerosIndividuales.isEmpty())
                            {
                                li.Add(d.datosDelContenedor.numerosIndividuales.OrdenadosPorIndice.ElementAt(0).IndiceDeRepresentacionStr);
                            }
                            if (d.datosDelContenedor.numeroCantidad != null)
                            {
                                li.Add(d.datosDelContenedor.numeroCantidad.IndiceDeRepresentacionStr);
                            }
                        }
                    }
                }
                li.Add(this.indiceDelFinalDeNombre);

                int[] indices = li.ToArray();



                int menor = -1;
     //           int[] indices = {
     //               IndiceNumeroCapitulo,
     //               IndiceNumeroTemporada,
     //               IndiceNumeroCapituloInicial,
     //               IndiceNumeroCapituloFinal,
     //               IndiceDeNumeroCantidadDeCapitulosQueContiene,
					////this.indiceDelPrimerNumero
     //               IndiceDelPrimerNumero,
     //               IndiceNumeroTemporadaInicial

     //           };
                //cwl(str(indices));   
                int end = indices.Length;
                for (int i = 0; i < end; i++)
                {
                    int indice = indices[i];
                    if (indice != -1 && (menor == -1 || indice < menor))
                    {
                        menor = indice;
                    }
                }
                return menor;



                //	return this.indiceDelPrimerNumero;
            }
            set { this.indiceDelPrimerNumero = value; }
        }
        //		public int IndiceIdentificadorTemporada {
        //			get{ return this.indiceIdentificadorTemporada; }
        //			set{ this.indiceIdentificadorTemporada = value; }
        //		}
        //		
        //		public int IndiceIdentificadorCapitulo {
        //			get{ return this.indiceIdentificadorCapitulo; }
        //			set{ this.indiceIdentificadorCapitulo = value; }
        //		}






    }
}
