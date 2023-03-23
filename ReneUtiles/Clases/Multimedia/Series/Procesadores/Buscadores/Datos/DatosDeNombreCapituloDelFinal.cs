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
using ReneUtiles.Clases.Interfaces;

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

            ConIndiceInicial []CI = {
                identificadorTemporada
                ,identificadorCapitulo
                ,identificadorCapituloOva
                ,contendorDeCapitulos
                ,contendorDeOvas
                ,contendorTemporada
            };
            foreach (ConIndiceInicial ci in CI)
            {
                if (ci!=null) {
                    int r = ci.getIndiceInicial();
                    if (r!=-1) {
                        li.Add(r);
                    }
                }
            }
            

            int[] indices = li.ToArray();
            
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
                
            }
            set { this.indiceDelFinalDeNombre = value; }
        }
        public int IndiceDelPrimerNumero
        {
            get
            {
                List<int> li = new List<int>();

                ConPrimerIndiceNumerico[] CI = {
                identificadorTemporada
                ,identificadorCapitulo
                ,identificadorCapituloOva
                ,contendorDeCapitulos
                ,contendorDeOvas
                ,contendorTemporada
            };
                foreach (ConPrimerIndiceNumerico ci in CI)
                {
                    if (ci != null)
                    {
                        int r = ci.getPrimerIndiceDeNumero();
                        if (r != -1)
                        {
                            li.Add(r);
                        }
                    }
                }
                
                int[] indices = li.ToArray();



                int menor = -1;
     
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
