/*
 * Created by SharpDevelop.
 * User: Rene
 * Date: 3/10/2021
 * Time: 11:51
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
using ReneUtiles.Clases.ExprecionesRegulares.IdentificacionesNumericas;


namespace ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos 
{
	public enum TipoDeNombreDeSerie{
			NORMAL//sin numeros
				,SOLO_UN_NUMERO
				,NUMERO_AL_PRINCIPIO
				,NUMEROS_MULTIPLES_AL_PRINCIPIO
				,NUMERO_AL_FINAL
				,NUMEROS_MULTIPLES_AL_FINAL
				,NUMERO_INTERNO
				,RODEADO_DE_NUMEROS
				,SOLO_NUMEROS_MULTIPLES
				,DESCONOCIDO
				
		}
	/// <summary>
	/// Description of DatosDeNombreCapitulo.
	/// </summary>
	public class DatosDeNombreCapitulo:ConsolaBasica
	{
		
		public TipoDeNombreDeSerie? TipoDeNombre;
        protected bool esSoloNumeros;

        protected DatosDeIdentificacionIndividual identificadorTemporada;
        protected DatosDeIdentificacionIndividual identificadorCapitulo;
        protected DatosDeIdentificacionIndividualCapituloOva identificadorCapituloOva;

        protected DatosDeIdentificacionColectivaCapitulos contendorDeCapitulos;
        protected DatosDeIdentificacionColectivaCapitulosOva contendorDeOvas;

        protected DatosDeIdentificacionColectiva contendorTemporada;
        //cambiarDe_NumeroCapitulo_A_NumeroTemporada

        public DatosDeNombreCapitulo()
		{
			inicializar();
		}

        


        public void iniT(DatosDeNombreCapitulo d)
		{
            inicializar(
                d.identificadorTemporada
         , d.identificadorCapitulo
         , d.identificadorCapituloOva

         , d.contendorDeCapitulos
         , d.contendorDeOvas

         , d.contendorTemporada
               
                , d.esSoloNumeros
           
        , d.TipoDeNombre
        

			); 
		}
		
		protected void inicializar(
          DatosDeIdentificacionIndividual idenificadorTemporada=null,
         DatosDeIdentificacionIndividual identificadorCapitulo = null,
         DatosDeIdentificacionIndividualCapituloOva identificadorCapituloOva = null,

         DatosDeIdentificacionColectivaCapitulos contendorDeCapitulos = null,
         DatosDeIdentificacionColectivaCapitulosOva contendorDeOvas = null,

         DatosDeIdentificacionColectiva contendorTemporada = null,
         
        bool esSoloNumeros = false,
        TipoDeNombreDeSerie? tipoDeNombre=null
            
		)
		{
			
			this.esSoloNumeros = esSoloNumeros;
           
            this.TipoDeNombre=tipoDeNombre;

            
            this.identificadorTemporada = idenificadorTemporada;
            this.identificadorCapitulo = identificadorCapitulo;
            this.identificadorCapituloOva = identificadorCapituloOva;

            this.contendorDeCapitulos = contendorDeCapitulos;
            this.contendorDeOvas = contendorDeOvas;

            this.contendorTemporada = contendorTemporada;

           

    }
		public bool esTipoDeNombre_OR(params TipoDeNombreDeSerie[] T){
			foreach (TipoDeNombreDeSerie t in T) {
				if(this.TipoDeNombre==t){
					return true;
				}
			}
			return true;
		}
        public bool hayOvasEnElContendor()
        {//numeroCantidad
            return this.contendorDeOvas != null
                && this.contendorDeOvas.datosDelContenedor!=null
                //&& this.contendorDeOvas.esDeEsteTipo
                && this.contendorDeOvas.datosDelContenedor.numeroCantidad != null;
        }
        //public bool HayOvasEnElContendor{
        //	get{	return IndiceDeNumeroCantidadDeOvasQueContiene!=-1;}

        //}

        //      public int IndiceNumeroCapituloInicial {
        //	get{ return this.indiceNumeroCapituloInicial; }
        //	set{ this.indiceNumeroCapituloInicial = value; }
        //}
        //public int IndiceNumeroCapituloFinal {
        //	get{ return this.indiceNumeroCapituloFinal; }
        //	set{ this.indiceNumeroCapituloFinal = value; }
        //}
        //public int IndiceNumeroTemporada {
        //	get{ return this.indiceNumeroTemporada; }
        //	set{ this.indiceNumeroTemporada = value; }
        //}
        //public int IndiceNumeroCapitulo {
        //	get{ return this.indiceNumeroCapitulo; }
        //	set{ this.indiceNumeroCapitulo = value; }
        //}

        public bool EsSoloNumeros
        {
            get { return this.esSoloNumeros; }
            set { this.esSoloNumeros = value; }
        }
        public bool esConjuntoDeCapitulos() {
            return this.contendorDeCapitulos != null 
                //&& this.contendorDeCapitulos.datosDelContenedor.esDeEsteTipo
                ;
        }

        //		public bool EsConjuntoDeCapitulos {
        //			get{ return this.esConjuntoDeCapitulos; }
        //			set {
        //				this.esConjuntoDeCapitulos = value;
        ////				if (this.esConjuntoDeCapitulos) {
        ////					this.capitulo=-1;
        ////				}
        //			}
        //		}

        //      public bool EsCapitulo{
        //	get{ return this.Capitulo!=-1||this.capituloInicial!=-1;}
        //}

        public bool esCapitulo()
        {
            return (this.identificadorCapitulo != null 
                && this.identificadorCapitulo.identificacionNumerica!=null)
                || (this.contendorDeCapitulos != null
                && this.contendorDeCapitulos.datosDelContenedor!=null
               // && this.contendorDeCapitulos.esDeEsteTipo
                && !this.contendorDeCapitulos.datosDelContenedor.numerosIndividuales.isEmpty()
                ) || (this.contendorDeOvas != null
                && this.contendorDeOvas.datosDelContenedor!=null
               // && this.contendorDeOvas.esDeEsteTipo
                && !this.contendorDeOvas.datosDelContenedor.numerosIndividuales.isEmpty()
                );
        }


        //      public bool TieneTemporada {
        //	get{ return temporada > -1; }
        //}
        public bool tieneTemporada_Unica() {
            return this.identificadorTemporada != null
                && this.identificadorTemporada.identificacionNumerica!=null;
        }


        //      public int Temporada {
        //	get{ return this.temporada; }
        //	set {
        //		this.temporada = Math.Abs(value);
        //		//this.temporada_LengStr = (this.temporada + "").Length;
        //	}
        //}
        //public int Capitulo {
        //	get{ return this.capitulo; }
        //	set {
        //		this.capitulo = Math.Abs(value);
        //		//this.capitulo_LengStr = (this.capitulo + "").Length;
        //	}
        //}
        //public int CapituloInicial {
        //	get{ return this.capituloInicial; }
        //	set {
        //		this.capituloInicial = Math.Abs(value);
        //		//this.capituloInicial_LengStr = (this.capituloInicial + "").Length;
        //	}
        //}
        //public int CapituloFinal {
        //	get{ return this.capituloFinal; }
        //	set {
        //		this.capituloFinal = Math.Abs(value);
        //		//this.capituloFinal_LengStr = (this.capituloFinal + "").Length;
        //	}
        //}

        //public bool isEmpty()
        //{
        //	if (esConjuntoDeCapitulos) {
        //		return capituloInicial == null || capituloInicial < 0;
        //	}

        //	return capitulo == null || capitulo < 0;
        //}

        public bool isEmpty_IdentificacionNumerica()
        {
            if (!isEmpty_Capitulos()) {
                return false;
            }
            if (this.identificadorTemporada != null
                && this.identificadorTemporada.identificacionNumerica != null)
            {
                return false;
            }

            if (this.contendorTemporada != null
                && this.contendorTemporada.datosDelContenedor != null
                //&& this.contendorTemporada.esDeEsteTipo
                )
            {
                if (this.contendorTemporada.datosDelContenedor.numeroCantidad != null)
                {
                    return false;
                }
                if (this.contendorTemporada.datosDelContenedor.numerosIndividuales != null
                    && !this.contendorTemporada.datosDelContenedor.numerosIndividuales.isEmpty()
                    )
                {
                    return false;
                }
            }
            return true;
        }
        public bool isEmpty_Capitulos()
        {

            if (this.identificadorCapitulo!=null
                &&this.identificadorCapitulo.identificacionNumerica!=null) {
                return false;
            }
            if (this.identificadorCapituloOva != null
                && this.identificadorCapituloOva.identificacionNumerica != null)
            {
                return false;
            }
            
            if (this.contendorDeCapitulos != null
                &&this.contendorDeCapitulos.datosDelContenedor!=null
                //&&this.contendorDeCapitulos.esDeEsteTipo
                ) {
                if (this.contendorDeCapitulos.datosDelContenedor.numeroCantidad!=null) {
                    return false;
                }
                if (this.contendorDeCapitulos.datosDelContenedor.numerosIndividuales != null
                    && !this.contendorDeCapitulos.datosDelContenedor.numerosIndividuales.isEmpty()
                    )
                {
                    return false;
                }
            }

            if (this.contendorDeOvas != null
                && this.contendorDeOvas.datosDelContenedor != null
                //&& this.contendorDeOvas.esDeEsteTipo
                )
            {
                if (this.contendorDeOvas.datosDelContenedor.numeroCantidad != null)
                {
                    return false;
                }
                if (this.contendorDeOvas.datosDelContenedor.numerosIndividuales != null
                    && !this.contendorDeOvas.datosDelContenedor.numerosIndividuales.isEmpty()
                    )
                {
                    return false;
                }
            }
            
            return true;
        }

  //      public string TemporadaStr {
		//	get{ return this.temporadaStr; }
		//	set {
		//		this.temporadaStr=value;
		//		if(value.Trim().Length==0){
		//			this.Temporada=-1;
		//		}else{
		//			this.Temporada=inT(value);
		//		}
				
		//	}
		//}
		//public string CapituloStr {
		//	get{ return this.capituloStr; }
		//	set {
		//		this.capituloStr=value;
		//		if(value.Trim().Length==0){
		//			this.Capitulo=-1;
		//		}else{
		//			this.Capitulo=inT(value);
		//		}
		//		//this.Capitulo=inT(value);
		//		//this.capitulo = Math.Abs(value);
		//		//this.capitulo_LengStr = (this.capitulo + "").Length;
		//	}
		//}
		//public string TemporadaNumerosRomanosStr {
		//	get{ return this.temporadaNumerosRomanosStr; }
		//	set {
		//		this.temporadaNumerosRomanosStr=value;
		//		this.Temporada=Utiles.getNumeroDeNumeroRomano(value);
		//		//this.capitulo = Math.Abs(value);
		//		//this.capitulo_LengStr = (this.capitulo + "").Length;
		//	}
		//}
		
		//public string CapituloInicialStr {
		//	get{ return this.capituloInicialStr; }
		//	set {
		//		this.capituloInicialStr=value;
		//		this.CapituloInicial=inT(value);
		//		//this.capitulo = Math.Abs(value);
		//		//this.capitulo_LengStr = (this.capitulo + "").Length;
		//	}
		//}
		//public string CapituloFinalStr {
		//	get{ return this.capituloFinalStr; }
		//	set {
		//		this.capituloFinalStr=value;
		//		this.CapituloFinal=inT(value);
		//		//this.capitulo = Math.Abs(value);
		//		//this.capitulo_LengStr = (this.capitulo + "").Length;
		//	}
		//}
		
		//public string CantidadDeCapitulosQueContieneStr {
		//	get{ return this.cantidadDeCapitulosQueContieneStr; }
		//	set {
		//		this.cantidadDeCapitulosQueContieneStr=value;
		//		this.CantidadDeCapitulosQueContiene=inT(value);
		//		//this.capitulo = Math.Abs(value);
		//		//this.capitulo_LengStr = (this.capitulo + "").Length;
		//	}
		//}
		//public string CantidadDeOvasQueContieneStr {
		//	get{ return this.cantidadDeOvasQueContieneStr; }
		//	set {
		//		this.cantidadDeOvasQueContieneStr=value;
		//		this.CantidadDeOvasQueContiene=inT(value);
		//		//this.capitulo = Math.Abs(value);
		//		//this.capitulo_LengStr = (this.capitulo + "").Length;
		//	}
		//}
		
		//public int IndiceIdentificadorTemporada {
		//	get{ return this.indiceIdentificadorTemporada; }
		//	set{ this.indiceIdentificadorTemporada = value; }
		//}
		
		//public int IndiceIdentificadorCapitulo {
		//	get{ return this.indiceIdentificadorCapitulo; }
		//	set{ this.indiceIdentificadorCapitulo = value; }
		//}

        public DatosDeIdentificacionIndividual IdenificadorTemporada
        {
            get
            {
                return identificadorTemporada;
            }

            set
            {
                identificadorTemporada = value;
            }
        }

        public DatosDeIdentificacionIndividual IdentificadorCapitulo
        {
            get
            {
                return identificadorCapitulo;
            }

            set
            {
                identificadorCapitulo = value;
            }
        }

        public DatosDeIdentificacionIndividualCapituloOva IdentificadorCapituloOva
        {
            get
            {
                return identificadorCapituloOva;
            }

            set
            {
                identificadorCapituloOva = value;
            }
        }

        public DatosDeIdentificacionColectivaCapitulos ContendorDeCapitulos
        {
            get
            {
                return contendorDeCapitulos;
            }

            set
            {
                contendorDeCapitulos = value;
                this.identificadorCapitulo = null;
                this.identificadorCapituloOva = null;
                
            }
        }

        public DatosDeIdentificacionColectivaCapitulosOva ContendorDeOvas
        {
            get
            {
                return contendorDeOvas;
            }

            set
            {
                contendorDeOvas = value;
                this.identificadorCapitulo = null;
                this.identificadorCapituloOva = null;
            }
        }

        public DatosDeIdentificacionColectiva ContendorTemporada
        {
            get
            {
                return contendorTemporada;
            }

            set
            {
                contendorTemporada = value;
            }
        }

        //public bool EsContenedorDe_ConjuntoDeCapitulos_DeMismaTemporada
        //{
        //    get
        //    {
        //        return esContenedorDe_ConjuntoDeCapitulos_DeMismaTemporada;
        //    }

        //    set
        //    {
        //        esContenedorDe_ConjuntoDeCapitulos_DeMismaTemporada = value;
        //        if (value) {
        //            this.identificadorCapitulo = null;
        //            this.identificadorCapituloOva = null;
        //        }
        //    }
        //}

        public void setEsOva() {

            if (this.identificadorCapitulo!=null) {
                if (this.identificadorCapituloOva!=null) {
                    this.identificadorCapituloOva.clonarValores(this.identificadorCapitulo);
                } else {
                    this.identificadorCapituloOva = DatosDeIdentificacionIndividualCapituloOva.clonar(this.identificadorCapitulo);//= this.identificadorCapitulo;
                }
                
                this.identificadorCapitulo = null;
            }
            if (this.contendorDeCapitulos != null)
            {
                if (this.contendorDeOvas!=null) {
                    this.contendorDeOvas.clonarValores(this.contendorDeCapitulos);
                } else {
                    this.contendorDeOvas = DatosDeIdentificacionColectivaCapitulosOva.clonar(this.contendorDeCapitulos);
                }
                
                this.contendorDeCapitulos = null;
            }
                

        }
        public bool esOva() {
            return this.identificadorCapituloOva != null
                && (this.IdentificadorCapituloOva.etiqueta != null
                || this.IdentificadorCapituloOva.identificacionNumerica != null

                );
        }
        //protected bool EsOVA
        //{
        //    get
        //    {
        //        return this.esOVA
        //            ||();
        //    }

        //    set
        //    {
        //        esOVA = value;
        //        //efecto al set
        //    }
        //}

        //protected int IndiceNumeroTemporadaInicial
        //{
        //    get
        //    {
        //        return indiceNumeroTemporadaInicial;
        //    }

        //    set
        //    {
        //        indiceNumeroTemporadaInicial = value;
        //    }
        //}

        //protected int IndiceNumeroTemporadaFinal
        //{
        //    get
        //    {
        //        return indiceNumeroTemporadaFinal;
        //    }

        //    set
        //    {
        //        indiceNumeroTemporadaFinal = value;
        //    }
        //}

        //public string TemporadaFinalStr
        //{
        //    get { return this.temporadaFinalStr; }
        //    set
        //    {
        //        this.temporadaFinalStr = value;
        //        if (value.Trim().Length == 0)
        //        {
        //            this.temporadaFinal = -1;
        //        }
        //        else
        //        {
        //            this.temporadaFinal = Math.Abs(inT(value));
        //        }

        //    }
        //}
        //public string TemporadaInicialStr
        //{
        //    get { return this.temporadaInicialStr; }
        //    set
        //    {
        //        this.temporadaInicialStr = value;
        //        if (value.Trim().Length == 0)
        //        {
        //            this.temporadaInicial = -1;
        //        }
        //        else
        //        {
        //            this.temporadaInicial = Math.Abs(inT(value));
        //        }

        //    }
        //}

        public bool esContenedorDeTemporada() {
            return this.ContendorTemporada != null
                //&& this.ContendorTemporada.esDeEsteTipo
                ;
                //&& (this.ContendorTemporada.etiqueta != null
                //|| (this.ContendorTemporada.datosDelContenedor != null && this.ContendorTemporada.esDeEsteTipo)
               // );
        }

        public int getCapituloInicial() {
            if (this.contendorDeCapitulos!=null
                && this.contendorDeCapitulos.datosDelContenedor!=null
                //&& this.contendorDeCapitulos.esDeEsteTipo
                && this.contendorDeCapitulos.datosDelContenedor.numerosIndividuales!=null
                &&!this.contendorDeCapitulos.datosDelContenedor.numerosIndividuales.isEmpty()
                ) {
                return this.contendorDeCapitulos.datosDelContenedor.numerosIndividuales.OrdenadosPorNumero.First().Numero;
            }
            if (this.identificadorCapitulo != null
                && this.identificadorCapitulo.identificacionNumerica != null) {
                return this.identificadorCapitulo.identificacionNumerica.Numero;
            }
            if (this.contendorDeOvas != null
                && this.contendorDeOvas.datosDelContenedor != null
                //&& this.contendorDeOvas.esDeEsteTipo
                && this.contendorDeOvas.datosDelContenedor.numerosIndividuales != null
                && !this.contendorDeOvas.datosDelContenedor.numerosIndividuales.isEmpty()
                )
            {
                return this.contendorDeOvas.datosDelContenedor.numerosIndividuales.OrdenadosPorNumero.First().Numero;
            }
            if (this.identificadorCapituloOva != null
                && this.identificadorCapituloOva.identificacionNumerica != null)
            {
                return this.identificadorCapituloOva.identificacionNumerica.Numero;
            }
            return -1;
        }


        public int getCapituloFinal()
        {
            if (this.contendorDeCapitulos != null
                && this.contendorDeCapitulos.datosDelContenedor != null
                //&& this.contendorDeCapitulos.esDeEsteTipo
                && this.contendorDeCapitulos.datosDelContenedor.numerosIndividuales != null
                && !this.contendorDeCapitulos.datosDelContenedor.numerosIndividuales.isEmpty()
                )
            {
                return this.contendorDeCapitulos.datosDelContenedor.numerosIndividuales.OrdenadosPorNumero.Last().Numero;
            }
           
            if (this.contendorDeOvas != null
                && this.contendorDeOvas.datosDelContenedor != null
                //&& this.contendorDeOvas.esDeEsteTipo
                && this.contendorDeOvas.datosDelContenedor.numerosIndividuales != null
                && !this.contendorDeOvas.datosDelContenedor.numerosIndividuales.isEmpty()
                )
            {
                return this.contendorDeOvas.datosDelContenedor.numerosIndividuales.OrdenadosPorNumero.Last().Numero;
            }
            
            return -1;
        }


        public int getCantidadDeCapitulos() {
            int cantidad = 0;
            if (this.contendorDeCapitulos!=null
                &&this.contendorDeCapitulos.datosDelContenedor!=null
                //&& this.contendorDeCapitulos.esDeEsteTipo
                
                ) {
                cantidad+= this.contendorDeCapitulos.datosDelContenedor.getTamannoDelRangoQueContiene();
            }
            if (this.contendorDeOvas != null
                && this.contendorDeOvas.datosDelContenedor != null
                //&& this.contendorDeOvas.esDeEsteTipo
                
                )
            {
                cantidad += this.contendorDeOvas.datosDelContenedor.getTamannoDelRangoQueContiene();
            }
            if (this.identificadorCapitulo!=null
                &&this.identificadorCapitulo.identificacionNumerica!=null) {
                cantidad++;
            }
            if (this.identificadorCapituloOva != null
                && this.identificadorCapituloOva.identificacionNumerica != null)
            {
                cantidad++;
            }
            return cantidad;
        }
        /// <summary>
        /// los cambia de forma paralela osea:
        /// -de ovas a ova
        /// -de capitulos normales a capitulo normal
        /// </summary>
        public void cambiarDe_ConjuntoDeCapitulos_A_Capitulo() {
            if (this.contendorDeCapitulos != null)
            {
                DatosDeIdentificacionIndividual di = new DatosDeIdentificacionIndividual();
                di.identificacionNumerica = this.contendorDeCapitulos.datosDelContenedor.numerosIndividuales.OrdenadosPorNumero.First();
                di.etiqueta = this.contendorDeCapitulos.etiqueta;
                this.IdentificadorCapitulo = di;
                this.contendorDeCapitulos = null;
            }
            else if(this.contendorDeOvas != null) {
                DatosDeIdentificacionIndividual di = new DatosDeIdentificacionIndividual();
                di.identificacionNumerica = this.contendorDeOvas.datosDelContenedor.numerosIndividuales.OrdenadosPorNumero.First();
                di.etiqueta = this.contendorDeOvas.etiqueta;
                this.IdentificadorCapituloOva = new DatosDeIdentificacionIndividualCapituloOva();
                this.IdentificadorCapituloOva.clonarValores(di); //= di;
                this.contendorDeOvas = null;
            }
            
        }

        private IdentificacionNumericaEnStr intentarCambiarDeGrupo(DatosDeIdentificacionColectiva dic)
        {
            IdentificacionNumericaEnStr idn = null;
            if (dic != null
                                    && dic.datosDelContenedor != null
                                    && dic.datosDelContenedor.numerosIndividuales != null
                                    && !dic.datosDelContenedor.numerosIndividuales.isEmpty()
                                    )
            {
                idn = dic.datosDelContenedor.numerosIndividuales.OrdenadosPorIndice[0];
                dic.datosDelContenedor.numerosIndividuales.clear();
                
            }
            return idn;
        }
        public void cambiarDe_NumeroCapitulo_A_NumeroTemporada (){
            IdentificacionNumericaEnStr idn = null;
            if (this.identificadorCapitulo != null)
            {
                idn = this.identificadorCapitulo.identificacionNumerica;
                this.identificadorCapitulo.identificacionNumerica = null;

            }
            else if (this.identificadorCapituloOva != null)
            {
                idn = this.identificadorCapituloOva.identificacionNumerica;
                this.identificadorCapituloOva.identificacionNumerica = null;

            }
            else
            {
                //DatosDeIdentificacionColectivaCapitulos dic = this.contendorDeCapitulos;
                idn = intentarCambiarDeGrupo(this.contendorDeCapitulos);
                if (idn == null)
                {
                    idn = intentarCambiarDeGrupo(this.contendorDeOvas);
                }
            }

            if (idn != null)
            {
                setIdentificacionTemporada_Numero(idn);
            }

        }
        public void cambiarDe_NumeroTemporada_A_NumeroCapitulo() {
            IdentificacionNumericaEnStr idn = null;
            if (this.identificadorTemporada != null)
            {
                idn = this.identificadorTemporada.identificacionNumerica;
                this.identificadorTemporada.identificacionNumerica = null;
                
            }
            
            else
            {
                //DatosDeIdentificacionColectivaCapitulos dic = this.contendorDeCapitulos;
                idn = intentarCambiarDeGrupo(this.contendorTemporada);
                
            }
            
            if (idn!=null) {
                setIdentificacion_Capitulo_Numero(idn);
                
            } 

            }

        public void setIdentificacionTemporada_Numero(IdentificacionNumericaEnStr idnt)
        {
            DatosDeIdentificacionIndividual di = new DatosDeIdentificacionIndividual();
            if (this.IdenificadorTemporada != null)
            {
                di = this.IdenificadorTemporada;
            }


            di.identificacionNumerica = idnt;
            this.IdenificadorTemporada = di;
            //setIdentificacionTemporada_Numero(di, indiceDeRepresentacionStr, representacionStr);
        }
        public void setIdentificacionTemporada_Numero(int indiceDeRepresentacionStr, string representacionStr)
        {
            setIdentificacionTemporada_Numero(new IdentificacionNumericaEnStr(
                indiceDeRepresentacionStr: indiceDeRepresentacionStr
                , representacionStr: representacionStr
                ));
            //DatosDeIdentificacionIndividual di = new DatosDeIdentificacionIndividual();
            //if (this.IdenificadorTemporada != null)
            //{
            //    di = this.IdenificadorTemporada;
            //}


            //di.identificacionNumerica = new IdentificacionNumericaEnStr(
            //    indiceDeRepresentacionStr: indiceDeRepresentacionStr
            //    , representacionStr: representacionStr
            //    );
            //this.IdenificadorTemporada = di;

        }
        //private void setIdentificacionTemporada_Numero(DatosDeIdentificacionIndividual di,int indiceDeRepresentacionStr, string representacionStr) {
            
        //    //if (this.IdenificadorTemporada!=null) {
        //    //    di = this.IdenificadorTemporada;
        //    //}
            

        //    //di.identificacionNumerica = new IdentificacionNumericaEnStr(
        //    //    indiceDeRepresentacionStr: indiceDeRepresentacionStr
        //    //    , representacionStr: representacionStr
        //    //    );
        //    //this.IdenificadorTemporada = di;
        //}

        public void setIdentificacionTemporada_NumeroRomanao(int indiceDeRepresentacionStr, string representacionStr)
        {
            //setIdentificacionTemporada_Numero(new IdentificacionNumeroRomanoEnStr(), indiceDeRepresentacionStr, representacionStr);
            DatosDeIdentificacionIndividual di = new DatosDeIdentificacionIndividual();
            if (this.IdenificadorTemporada != null)
            {
                di = this.IdenificadorTemporada;
            }


            di.identificacionNumerica = new IdentificacionNumeroRomanoEnStr(
                indiceDeRepresentacionStr: indiceDeRepresentacionStr
                , representacionStr: representacionStr
                );
            this.IdenificadorTemporada = di;
        }
        public void setIdentificacionTemporada_Etiqueta(int indiceDeRepresentacionStr, string representacionStr)
        {
            DatosDeIdentificacionIndividual di = new DatosDeIdentificacionIndividual();
            if (this.IdenificadorTemporada != null)
            {
                di = this.IdenificadorTemporada;
            }


            di.etiqueta = new IdentificacionEnStr(
                indiceDeRepresentacionStr: indiceDeRepresentacionStr
                , representacionStr: representacionStr
                );
            this.IdenificadorTemporada = di;
        }

        public void setIdentificacionOva_Numero(
            int indiceDeRepresentacionStr
            , string representacionStr
            ) {
            DatosDeIdentificacionIndividualCapituloOva di = new DatosDeIdentificacionIndividualCapituloOva();
            if (this.IdentificadorCapituloOva != null)
            {
                di = this.IdentificadorCapituloOva;
            }
            di.identificacionNumerica = new IdentificacionNumericaEnStr(
                indiceDeRepresentacionStr: indiceDeRepresentacionStr
                , representacionStr: representacionStr
                );
            this.IdentificadorCapituloOva = di;
        }
        public void setIdentificacionOva_TagOVA(
            int indiceDeRepresentacionStr_tagOVA
            , string representacionStr_tagOVA)
        {
            DatosDeIdentificacionIndividualCapituloOva di = new DatosDeIdentificacionIndividualCapituloOva();
            if (this.IdentificadorCapituloOva != null)
            {
                di = this.IdentificadorCapituloOva;
            }
            di.etiquetaOva = new IdentificacionEnStr(
                indiceDeRepresentacionStr: indiceDeRepresentacionStr_tagOVA
                , representacionStr: representacionStr_tagOVA
                );
            this.IdentificadorCapituloOva = di;
        }

        public void setIdentificacionOva_TagOVANumero(
             int indiceDeRepresentacionStr_tagOVA
            , string representacionStr_tagOVA
            ,int indiceDeRepresentacionStr_numero
            , string representacionStr_numero
            )
        {
            DatosDeIdentificacionIndividualCapituloOva di = new DatosDeIdentificacionIndividualCapituloOva();
            if (this.IdentificadorCapituloOva != null)
            {
                di = this.IdentificadorCapituloOva;
            }
            di.etiquetaOva = new IdentificacionEnStr(
                indiceDeRepresentacionStr: indiceDeRepresentacionStr_tagOVA
                , representacionStr: representacionStr_tagOVA
                );
            di.identificacionNumerica = new IdentificacionNumericaEnStr(
                indiceDeRepresentacionStr: indiceDeRepresentacionStr_numero
                , representacionStr: representacionStr_numero
                );
            this.IdentificadorCapituloOva = di;
        }

        public void setIdentificacion_ConjuntoDeCapitulos_Etiqueta(
            int indiceDeRepresentacionStr_etiqueta
            , string representacionStr_etiqueta
            
            )
        {
            DatosDeIdentificacionColectivaCapitulos dc = new DatosDeIdentificacionColectivaCapitulos();
            if (this.contendorDeCapitulos != null)
            {
                dc = this.contendorDeCapitulos;
            }
            dc.etiqueta = new IdentificacionEnStr(
                indiceDeRepresentacionStr: indiceDeRepresentacionStr_etiqueta
                , representacionStr: representacionStr_etiqueta
                );
            if (dc.datosDelContenedor == null)
            {
                dc.datosDelContenedor = new DatosDeContenedor();
            }

            
            //dc.esDeEsteTipo = true;

            this.contendorDeCapitulos = dc;
        }

        public int getTemporada() {
            if (tieneTemporada_Unica()) {
                return this.identificadorTemporada.identificacionNumerica.Numero;
            }
            return -1;
        }
        public int getIndiceTemporada()
        {
            if (tieneTemporada_Unica())
            {
                return this.identificadorTemporada.identificacionNumerica.IndiceDeRepresentacionStr;
            }
            return -1;
        }

        public int getIndiceEtiquetaTemporada()
        {
            if (this.identificadorTemporada!=null
                && this.identificadorTemporada.etiqueta!=null
                )
            {
                return this.identificadorTemporada.etiqueta.IndiceDeRepresentacionStr;
            }
            return -1;
        }


        public void setIdentificacion_ConjuntoDeCapitulos_Numero(
            
             int indiceDeRepresentacionStr_numeroCantidad
            , string representacionStr_numeroCantidad
            )
        {
            DatosDeIdentificacionColectivaCapitulos dc = new DatosDeIdentificacionColectivaCapitulos();
            if (this.contendorDeCapitulos != null)
            {
                dc = this.contendorDeCapitulos;
            }
            
            if (dc.datosDelContenedor == null)
            {
                dc.datosDelContenedor = new DatosDeContenedor();
            }

            dc.datosDelContenedor.numeroCantidad = new IdentificacionNumericaEnStr(
                indiceDeRepresentacionStr: indiceDeRepresentacionStr_numeroCantidad
                , representacionStr: representacionStr_numeroCantidad
                );
            //dc.esDeEsteTipo = true;

            this.contendorDeCapitulos = dc;
        }

        public void setIdentificacion_ConjuntoDeCapitulos_Ova_Numero(
            
             int indiceDeRepresentacionStr_numeroCantidad
            , string representacionStr_numeroCantidad
            )
        {
            DatosDeIdentificacionColectivaCapitulosOva dc = new DatosDeIdentificacionColectivaCapitulosOva();
            if (this.contendorDeOvas != null)
            {
                dc = this.contendorDeOvas;
            }
            
            if (dc.datosDelContenedor == null)
            {
                dc.datosDelContenedor = new DatosDeContenedor();
            }

            dc.datosDelContenedor.numeroCantidad = new IdentificacionNumericaEnStr(
                indiceDeRepresentacionStr: indiceDeRepresentacionStr_numeroCantidad
                , representacionStr: representacionStr_numeroCantidad
                );
            //dc.esDeEsteTipo = true;

            this.contendorDeOvas = dc;
        }

        public void setIdentificacion_ConjuntoDeCapitulos_Ova_EtiquetaOvaNumero(
            int indiceDeRepresentacionStr_etiquetaOva
            , string representacionStr_etiquetaOva
            , int indiceDeRepresentacionStr_numeroCantidad
            , string representacionStr_numeroCantidad
            )
        {
            DatosDeIdentificacionColectivaCapitulosOva dc = new DatosDeIdentificacionColectivaCapitulosOva();
            if (this.contendorDeOvas != null)
            {
                dc = this.contendorDeOvas;
            }
            dc.etiquetaOva = new IdentificacionEnStr(
                indiceDeRepresentacionStr: indiceDeRepresentacionStr_etiquetaOva
                , representacionStr: representacionStr_etiquetaOva
                );
            if (dc.datosDelContenedor == null)
            {
                dc.datosDelContenedor = new DatosDeContenedor();
            }

            dc.datosDelContenedor.numeroCantidad = new IdentificacionNumericaEnStr(
                indiceDeRepresentacionStr: indiceDeRepresentacionStr_numeroCantidad
                , representacionStr: representacionStr_numeroCantidad
                );
            //dc.esDeEsteTipo = true;

            this.contendorDeOvas = dc;
        }
        public void setIdentificacion_ConjuntoDeCapitulos_EtiquetaNumero(
            int indiceDeRepresentacionStr_etiqueta
            , string representacionStr_etiqueta
            , int indiceDeRepresentacionStr_numeroCantidad
            , string representacionStr_numeroCantidad
            ) {
            DatosDeIdentificacionColectivaCapitulos dc = new DatosDeIdentificacionColectivaCapitulos();
            if (this.contendorDeCapitulos != null)
            {
                dc = this.contendorDeCapitulos;
            }
            dc.etiqueta = new IdentificacionEnStr(
                indiceDeRepresentacionStr: indiceDeRepresentacionStr_etiqueta
                , representacionStr: representacionStr_etiqueta
                );
            if (dc.datosDelContenedor==null) {
                dc.datosDelContenedor = new DatosDeContenedor();
            }
            
            dc.datosDelContenedor.numeroCantidad = new IdentificacionNumericaEnStr(
                indiceDeRepresentacionStr: indiceDeRepresentacionStr_numeroCantidad
                , representacionStr: representacionStr_numeroCantidad
                );
            //dc.esDeEsteTipo = true;

            this.contendorDeCapitulos = dc; 
        }

        public void setIdentificacion_ConjuntoDeCapitulos_InicialFinal(
            int indiceDeRepresentacionStr_inicial
            , string representacionStr_inicial
            , int indiceDeRepresentacionStr_final
            , string representacionStr_final) {
            DatosDeIdentificacionColectivaCapitulos dc = new DatosDeIdentificacionColectivaCapitulos();
            if (this.contendorDeCapitulos!=null) {
                dc = this.contendorDeCapitulos;
            }
            if (dc.datosDelContenedor == null)
            {
                dc.datosDelContenedor = new DatosDeContenedor();
            }
            dc.datosDelContenedor.numerosIndividuales = new ConjuntoDeIdentificacionesNumericas();
            dc.datosDelContenedor.numerosIndividuales.add(
                new IdentificacionNumericaEnStr(
                    indiceDeRepresentacionStr: indiceDeRepresentacionStr_inicial
                , representacionStr: representacionStr_inicial
                )
                );
            dc.datosDelContenedor.numerosIndividuales.add(
                new IdentificacionNumericaEnStr(
                    indiceDeRepresentacionStr: indiceDeRepresentacionStr_final
                , representacionStr: representacionStr_final
                )
                );
            //dc.esDeEsteTipo = true;
            this.contendorDeCapitulos = dc;
        }

        public void setIdentificacion_Capitulo_Etiqueta(
            int indiceDeRepresentacionStr
            , string representacionStr
            ) {
            DatosDeIdentificacionIndividual di=new DatosDeIdentificacionIndividual();
            if (this.identificadorCapitulo!=null) {
                di = this.identificadorCapitulo;
            }
            di.etiqueta = new IdentificacionEnStr(
                indiceDeRepresentacionStr: indiceDeRepresentacionStr
                , representacionStr: representacionStr
                );
            this.identificadorCapitulo = di;
        }
        public void setIdentificacion_Capitulo_Numero(
            int indiceDeRepresentacionStr
            , string representacionStr
            )
        {
            setIdentificacion_Capitulo_Numero(new IdentificacionNumericaEnStr(
                indiceDeRepresentacionStr: indiceDeRepresentacionStr
                , representacionStr: representacionStr
                ));
            //DatosDeIdentificacionIndividual di = new DatosDeIdentificacionIndividual();
            //if (this.identificadorCapitulo != null)
            //{
            //    di = this.identificadorCapitulo;
            //}
            //di.identificacionNumerica = new IdentificacionNumericaEnStr(
            //    indiceDeRepresentacionStr: indiceDeRepresentacionStr
            //    , representacionStr: representacionStr
            //    );
            //this.identificadorCapitulo = di;
        }
        public void setIdentificacion_Capitulo_Numero(
           IdentificacionNumericaEnStr idn
           )
        {
            DatosDeIdentificacionIndividual di = new DatosDeIdentificacionIndividual();
            if (this.identificadorCapitulo != null)
            {
                di = this.identificadorCapitulo;
            }
            di.identificacionNumerica = idn;
            this.identificadorCapitulo = di;
        }
        public void setEsContendedorDe_Capitulos_DeMismaSerie(bool loEs) {
            if (this.contendorDeCapitulos==null) {
                this.ContendorDeCapitulos = new DatosDeIdentificacionColectivaCapitulos();
            }
            //this.contendorDeCapitulos.esDeEsteTipo = loEs;
        }
        public void setEsContendedorDe_Capitulos_DeMismaTemporada(bool loEs)
        {
            if (this.contendorDeCapitulos == null)
            {
                this.ContendorDeCapitulos = new DatosDeIdentificacionColectivaCapitulos();
            }
            //this.contendorDeCapitulos.esDeEsteTipo = loEs;
            this.contendorDeCapitulos.esDeMismaTemporada = loEs;
        }
        public void setEsContendedorDeOvas_Capitulos_DeMismaTemporada(bool loEs)
        {
            if (this.contendorDeOvas == null)
            {
                this.ContendorDeOvas = new DatosDeIdentificacionColectivaCapitulosOva();
            }
           // this.contendorDeOvas.esDeEsteTipo = loEs;
            this.contendorDeOvas.esDeMismaTemporada = loEs;
        }

        public void setEsContendedorDeOvas_Capitulos_DeMismaSerie(bool loEs)
        {
            if (this.contendorDeOvas == null)
            {
                this.ContendorDeOvas = new DatosDeIdentificacionColectivaCapitulosOva();
            }
            //this.contendorDeOvas.esDeEsteTipo = loEs;
        }

        public void setEsContendedorDe_Temporadas(bool loEs)
        {
            if (this.contendorDeCapitulos == null)
            {
                this.ContendorDeCapitulos = new DatosDeIdentificacionColectivaCapitulos();
            }
            //this.contendorDeCapitulos.esDeEsteTipo = loEs;
        }

        public void setTagOva(int indiceDeRepresentacionStr_etiquetaOva
            , string representacionStr_etiquetaOva) {
            IdentificacionEnStr et= new IdentificacionEnStr(
                indiceDeRepresentacionStr: indiceDeRepresentacionStr_etiquetaOva
                , representacionStr: representacionStr_etiquetaOva
                );
            if (this.contendorDeOvas != null) {
                this.contendorDeOvas.etiquetaOva = et;
            } else if (this.identificadorCapituloOva != null) {
                this.contendorDeOvas.etiquetaOva = et;
            }
            
        }

        public bool hayNumeroTemporada_NoCantidad() {
            if (this.identificadorTemporada != null
                && this.identificadorTemporada.identificacionNumerica!=null
                &&this.identificadorTemporada.identificacionNumerica.Numero>-1) {
                return true;
            }
            return this.contendorTemporada != null
                //&& this.contendorTemporada.esDeEsteTipo
                && this.contendorTemporada.datosDelContenedor != null
                && this.contendorTemporada.datosDelContenedor.numerosIndividuales != null
                && !this.contendorTemporada.datosDelContenedor.numerosIndividuales.isEmpty();
        }


        public void setIdentificacion_ConjuntoDeTemporadas_Etiqueta_InicialFinal(
            int indiceDeRepresentacionStr_etiqueta
            , string representacionStr_etiqueta
            ,int indiceDeRepresentacionStr_inicial
            , string representacionStr_inicial
            , int indiceDeRepresentacionStr_final
            , string representacionStr_final)
        {
            DatosDeIdentificacionColectiva dc = new DatosDeIdentificacionColectiva();
            if (this.contendorTemporada != null)
            {
                dc = this.contendorTemporada;
            }
            dc.etiqueta = new IdentificacionEnStr(
                indiceDeRepresentacionStr: indiceDeRepresentacionStr_etiqueta
                , representacionStr: representacionStr_etiqueta
                );
            if (dc.datosDelContenedor == null)
            {
                dc.datosDelContenedor = new DatosDeContenedor();
            }
            
            dc.datosDelContenedor.numerosIndividuales = new ConjuntoDeIdentificacionesNumericas();
            dc.datosDelContenedor.numerosIndividuales.add(
                new IdentificacionNumericaEnStr(
                    indiceDeRepresentacionStr: indiceDeRepresentacionStr_inicial
                , representacionStr: representacionStr_inicial
                )
                );
            dc.datosDelContenedor.numerosIndividuales.add(
                new IdentificacionNumericaEnStr(
                    indiceDeRepresentacionStr: indiceDeRepresentacionStr_final
                , representacionStr: representacionStr_final
                )
                );
            //dc.esDeEsteTipo = true;
            this.contendorTemporada = dc;
        }


        public void setIdentificacion_ConjuntoDeTemporadas_InicialFinal(
                  
                   int indiceDeRepresentacionStr_inicial
                   , string representacionStr_inicial
                   , int indiceDeRepresentacionStr_final
                   , string representacionStr_final)
        {
            DatosDeIdentificacionColectiva dc = new DatosDeIdentificacionColectiva();
            if (this.contendorTemporada != null)
            {
                dc = this.contendorTemporada;
            }
            
            if (dc.datosDelContenedor == null)
            {
                dc.datosDelContenedor = new DatosDeContenedor();
            }

            dc.datosDelContenedor.numerosIndividuales = new ConjuntoDeIdentificacionesNumericas();
            dc.datosDelContenedor.numerosIndividuales.add(
                new IdentificacionNumericaEnStr(
                    indiceDeRepresentacionStr: indiceDeRepresentacionStr_inicial
                , representacionStr: representacionStr_inicial
                )
                );
            dc.datosDelContenedor.numerosIndividuales.add(
                new IdentificacionNumericaEnStr(
                    indiceDeRepresentacionStr: indiceDeRepresentacionStr_final
                , representacionStr: representacionStr_final
                )
                );
            //dc.esDeEsteTipo = true;
            this.contendorTemporada = dc;
        }


        //public int getCantidadDeCapitulos()
        //{
        //    DatosDeIdentificacionIndividual di = this.identificadorCapitulo ?? this.identificadorCapituloOva;
        //    if (
        //        di != null
        //        && di != null

        //        )
        //    {
        //        return 1;
        //    }

        //    DatosDeIdentificacionColectivaCapitulos dc = this.contendorDeCapitulos ?? this.contendorDeOvas;
        //    if (
        //        dc != null
        //        && dc.datosDelContenedor!=null
                
        //        ) {
                
        //        return dc.datosDelContenedor.getTamannoDelRangoQueContiene();
        //    }

        //    return -1;
        //}
       






    }
}
