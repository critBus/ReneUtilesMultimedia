/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 24/7/2022
 * Hora: 18:59
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using ReneUtiles.Clases;
using ReneUtiles.Clases.Multimedia.Relacionadores.Saltos;

using ReneUtiles.Clases.Multimedia.Series;
using ReneUtiles.Clases.Multimedia.Series.Anime;
using ReneUtiles.Clases.Multimedia.Series.SeriesPersona;

//using ReneUtiles.Clases.Multimedia.Series.Procesadores.Ignorar;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Conjuntos;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores;
using ReneUtiles.Clases.Multimedia.Series.Procesadores;
using ReneUtiles.Clases.Multimedia.Series.Contextos;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos;

namespace ReneUtiles.Clases.Multimedia.Series
{
	/// <summary>
	/// Description of ConfiguracionDeSeries.
	/// </summary>
	public class ConfiguracionDeSeries:ConsolaBasica
	{
		 
		public  string separadorDeNombresDeSerieEquivalentes = "===";
		
		private string[] saltarCualquierNumeroDespuesDe;
		//-
		
		/*saltarAntes*/
		public string saltarCualquierNumeroDespuesDe_Patron{ get; set; }
		/*saltarAntes*/
		private string[] saltarCualquierNumeroAntesDe;
		//-
		
		//saltarDespues
		public string saltarCualquierNumeroAntesDe_Patron{ get; set; }
		//saltarDespues
		private string[] union;
		//-
		
		public string union_Patron{ get; set; }
		private string union_predeterminada;
		//-
		
		public string union_predeterminada_Patron{ get; set; }
		private string[] detenciones;
		public string detenciones_Patron{ get; set; }
		private string[] identificadoresTemporadas;
		//-
		
		public string identificadoresTemporadas_Patron{ get; set; }
		private string[] identificadoresCapitulo;
		public string identificadoresCapitulo_Patron{ get; set; }
		
		private string[] identificadoresCantidadCapituloTemporada;
		public string identificadoresCantidadCapituloTemporada_Patron{ get; set; }
		private string[] detencionesAbsolutas;
		public string detencionesAbsolutas_Patron{ get; set; }
		private string[] saltarAlPrincipio;
		public string saltarAlPrincipio_Patron;
		public int[] nombresNumericosCompletosSimples{ get; set; }
		//		public string[] nombresNumericosCompletosMultiples{ get; set; }
		
		private string[] ignorarEstaPalabraAlPrincipio;
		public string  ignorarEstaPalabraAlPrincipio_Patron{ get; set; }
		
		public  TerminacionNumerica[] terminacionesNumericas;
		public  string terminacionesNumericas_Patron{ get; set; }

		//		private string[] nombresRodeadosDeNumeros;
		//		public string nombresRodeadosDeNumeros_Patron{ get; set; }
		public string separadores_SinRodear{ get; set; }
		
		private string[] continuacion;
		public string     continuacion_Patron{ get; set; }
		
		private string[] saltarHastaDespuesDe{ get; set; }
		public string     saltarHastaDespuesDe_Patron{ get; set; }
		public CondicionIgnorarNumeroEspecifico[] ignorarNumeroEspecificoDetrasDe{ get; set; }
		public CondicionIgnorarNumeroEspecifico[] ignorarNumeroEspecificoDelanteDe{ get; set; }
		public CondicionIgnorarNumeroEspecificoRodeadoPor[] condicionesIgnorarNumeroEspecificoRodeadoPor;
		public CondicionNoSaltarAlPrincipio[] noSaltarAlPrincipio;
		
		public static readonly string KEY_GRUPO_NUMERO_TERMINACION_NUMERICA = "key_numero_terminacion_numerica";
		
		public bool EsParaAnime;
		
		
		
		private   CondicionIgnorarNumeroEspecifico[] nombresConNumeroAlPrincipio;
		private   CondicionIgnorarNumeroEspecifico[] nombresConNumeroAlFinal;

		private   CondicionIgnorarNumeroEspecificoRodeadoPor[] nombresConUnNumeroInterno;
		private   CondicionIgnorarNumerosEspecificosSeparadosPor[] nombresRodeadosDeNumeros;
		private   CondicionIgnorarNumerosEspecificos[] nombresConNumerosAlPrincipio;
		private   CondicionIgnorarConjuntoDeNumeros[] nombresNumericosMultiples;


        private string nombresConNumeroAlPrincipio_patron;
        private string nombresConNumeroAlFinal_patron;
        private string nombresConUnNumeroInterno_patron;
        private string nombresRodeadosDeNumeros_patron;
        private string nombresConNumerosAlPrincipio_patron;
        private string nombresNumericosMultiples_patron;

		public   KeySerie[][] keysEquivalentes;

        private string[] nombresCarpetaSubtitulos;
        public string nombresCarpetaSubtitulos_patron;


        private string[] identificadoresCantidadCapituloTemporada_Prioridad;
        public string identificadoresCantidadCapituloTemporada_Prioridad_Patron { get; set; }

        public RecursosDePatronesDeSeries re;
		
		private ConfiguracionDeSeries()
		{
            //cwl("constructor configuracion de series");
		}


		
		private static ConfiguracionDeSeries getConfiguracionComun(RecursosDePatronesDeSeriesGenerales reg)
		{
			ConfiguracionDeSeries cf = new ConfiguracionDeSeries();
			
			
			cf.SaltarCualquierNumeroDespuesDe = ConstantesSeries.saltarCualquierNumeroDespuesDe;
			cf.SaltarCualquierNumeroAntesDe = ConstantesSeries.saltarCualquierNumeroAntesDe;
			cf.Union = ConstantesSeries.union;
			cf.IdentificadoresTemporadas = ConstantesSeries.identificadoresTemporadas;
			cf.IdentificadoresCapitulo = ConstantesSeries.identificadoresCapitulo;
			cf.TerminacionesNumericas = TerminacionNumerica.TerminacionesNumericasBasicas;
			cf.IdentificadoresCantidadCapituloTemporada = ConstantesSeries.identificadoresCantidadCapituloTemporada;
            cf.IdentificadoresCantidadCapituloTemporadaPrioridad = ConstantesSeries.identificadoresCantidadCapituloTemporada_Prioridad;

            cf.IgnorarEstaPalabraAlPrincipio = ConstantesSeries.ignorarEstaPalabraAlPrincipio;
			
			cf.separadores_SinRodear = ConstantesSeries.separadores_SinRodear;
			cf.Continuacion = ConstantesSeries.continuacion;
			cf.condicionesIgnorarNumeroEspecificoRodeadoPor = ConstantesSeries.condicionesIgnorarNumeroEspecificoRodeadoPor;
			
			cf.SaltarHastaDespuesDe = ConstantesSeries.saltarHastaDespuesDe;
			cf.SaltarAlPrincipio = ConstantesSeries.saltarAlPrincipio;
			
			
			cf.NombresConNumeroAlPrincipio = ConstantesSeries.nombresConNumeroAlPrincipio;
			cf.NombresConNumeroAlFinal = ConstantesSeries.nombresConNumeroAlFinal;

            //cf.NombresConUnNumeroInterno = ConstantesSeries.nombresConUnNumeroInterno;
            cf.nombresConUnNumeroInterno= ConstantesSeries.nombresConUnNumeroInterno;

            cf.NombresRodeadosDeNumeros = ConstantesSeries.nombresRodeadosDeNumeros;
			cf.NombresConNumerosAlPrincipio = ConstantesSeries.nombresConNumerosAlPrincipio;
			cf.NombresNumericosMultiples = ConstantesSeries.nombresNumericosMultiples; 
			
			cf.DetencionesAbsolutas=ConstantesSeries.detencionesAbsolutas;
			
			cf.keysEquivalentes = new KeySerie[][]{ };
			
			cf.separadorDeNombresDeSerieEquivalentes = ConstantesSeries.separadorDeNombresDeSerieEquivalentes;
			cf.re=new RecursosDePatronesDeSeries(cf,reg);

            cf.NombresCarpetaSubtitulos = ConstantesSeries.nombresCarpetaSubtitulos;

            //cf.union=ConstantesSeries.union;
            return cf;
		}
		
		public static ConfiguracionDeSeries getConfiguracionParaAnime(RecursosDePatronesDeSeriesGenerales reg)
		{
			ConfiguracionDeSeries cf = getConfiguracionComun(reg);
			
			
			//nada
			//cf.nombresNumericosCompletosMultiples = new string[0];
			cf.noSaltarAlPrincipio = new CondicionNoSaltarAlPrincipio[0];
			
			//anime
			cf.nombresNumericosCompletosSimples = ConstantesSeriesAnime.nombresNumericosCompletosSimples;
			cf.ignorarNumeroEspecificoDetrasDe = Arreglos.fusionar(ConstantesSeries.ignorarNumeroEspecificoDetrasDe,
				ConstantesSeriesAnime.ignorarNumeroEspecificoDetrasDe);
			cf.ignorarNumeroEspecificoDelanteDe = ConstantesSeriesAnime.ignorarNumeroEspecificoDelanteDe;
			
			cf.Detenciones = Arreglos.fusionar(ConstantesSeries.detenciones, ConstantesSeriesAnime.detenciones);
			cf.EsParaAnime = true; 
			
			
			cf.NombresConNumeroAlPrincipio = Arreglos.fusionar(ConstantesSeries.nombresConNumeroAlPrincipio, ConstantesSeriesAnime.nombresConNumeroAlPrincipio);
			cf.NombresConNumeroAlFinal = Arreglos.fusionar(ConstantesSeries.nombresConNumeroAlFinal, ConstantesSeriesAnime.nombresConNumeroAlFinal);
			cf.NombresConUnNumeroInterno = Arreglos.fusionar(ConstantesSeries.nombresConUnNumeroInterno, ConstantesSeriesAnime.nombresConUnNumeroInterno);
			cf.NombresRodeadosDeNumeros = Arreglos.fusionar(ConstantesSeries.nombresRodeadosDeNumeros, ConstantesSeriesAnime.nombresRodeadosDeNumeros);
			cf.NombresConNumerosAlPrincipio = Arreglos.fusionar(ConstantesSeries.nombresConNumerosAlPrincipio, ConstantesSeriesAnime.nombresConNumerosAlPrincipio);
			cf.NombresNumericosMultiples = Arreglos.fusionar(ConstantesSeries.nombresNumericosMultiples, ConstantesSeriesAnime.nombresNumericosMultiples);
			cf.keysEquivalentes = crearMatrisKey(ConstantesSeriesAnime.nombresEquivalentes, cf);

            cf.re = new RecursosDePatronesDeSeries(cf, reg);
            return cf;
		}
		
		public static ConfiguracionDeSeries getConfiguracionParaSeriesPersona(RecursosDePatronesDeSeriesGenerales reg)
		{
			ConfiguracionDeSeries cf = getConfiguracionComun(reg);
			
			//cf.ignorarNumeroEspecificoDetrasDe=ConstantesSeries.ignorarNumeroEspecificoDetrasDe;
			
			//nada
			cf.nombresNumericosCompletosSimples = ConstantesSeriesPersona.nombresNumericosCompletosSimples;
			
			
			//series en persona
			cf.ignorarNumeroEspecificoDetrasDe = Arreglos.fusionar(ConstantesSeries.ignorarNumeroEspecificoDetrasDe,
				ConstantesSeriesPersona.ignorarNumeroEspecificoDetrasDe);
			cf.noSaltarAlPrincipio = Arreglos.fusionar(ConstantesSeries.noSaltarAlPrincipio, ConstantesSeriesPersona.noSaltarAlPrincipio);
			
//			cf.nombresNumericosCompletosMultiples = ConstantesSeriesPersona.nombresNumericosCompletosMultiples;
			
			cf.ignorarNumeroEspecificoDelanteDe = ConstantesSeriesPersona.ignorarNumeroEspecificoDelanteDe;
//			cf.NombresRodeadosDeNumeros = ConstantesSeriesPersona.nombresRodeadosDeNumeros;
			cf.Detenciones = ConstantesSeries.detenciones;
			
			
			cf.NombresConNumeroAlPrincipio = Arreglos.fusionar(ConstantesSeries.nombresConNumeroAlPrincipio, ConstantesSeriesPersona.nombresConNumeroAlPrincipio);
			cf.NombresConNumeroAlFinal = Arreglos.fusionar(ConstantesSeries.nombresConNumeroAlFinal, ConstantesSeriesPersona.nombresConNumeroAlFinal);

            CondicionIgnorarNumeroEspecificoRodeadoPor[]  numerointerno= Arreglos.fusionar(ConstantesSeries.nombresConUnNumeroInterno, ConstantesSeriesPersona.nombresConUnNumeroInterno);

            cf.NombresConUnNumeroInterno = numerointerno;
			cf.NombresRodeadosDeNumeros = Arreglos.fusionar(ConstantesSeries.nombresRodeadosDeNumeros, ConstantesSeriesPersona.nombresRodeadosDeNumeros);
			cf.NombresConNumerosAlPrincipio = Arreglos.fusionar(ConstantesSeries.nombresConNumerosAlPrincipio, ConstantesSeriesPersona.nombresConNumerosAlPrincipio);
			cf.NombresNumericosMultiples = Arreglos.fusionar(ConstantesSeries.nombresNumericosMultiples, ConstantesSeriesPersona.nombresNumericosMultiples);
			
			cf.EsParaAnime = false;
			cf.keysEquivalentes = crearMatrisKey(ConstantesSeriesPersona.nombresEquivalentes, cf);

            cf.re = new RecursosDePatronesDeSeries(cf, reg);
            return cf;
		}

        public string GetNombresConNumeroAlPrincipio_patron()
        {
            return this.nombresConNumeroAlPrincipio_patron;
        }
        public string GetNombresConNumeroAlFinal_patron()
        {
            return this.nombresConNumeroAlFinal_patron;
        }
        public string GetNombresConUnNumeroInterno_patron()
        {
            return this.nombresConUnNumeroInterno_patron;
        }
        public string GetNombresRodeadosDeNumeros_patron()
        {
            return this.nombresRodeadosDeNumeros_patron;
        }
        public string GetNombresConNumerosAlPrincipio_patron()
        {
            return this.nombresConNumerosAlPrincipio_patron;
        }
        public string GetNombresNumericosMultiples_patron()
        {
            return this.nombresNumericosMultiples_patron;
        }

        

        private static KeySerie [][] crearMatrisKey(string[][]A, ConfiguracionDeSeries cf)
		{
			KeySerie[][] K = new KeySerie[A.Length][];
			for (int i = 0; i < A.Length; i++) {
				K[i] = new KeySerie[A[i].Length];
				for (int j = 0; j < A[i].Length; j++) {
					K[i][j] = crearKey(A[i][j], cf);
				}
			}
			return K;
		}
		private static KeySerie crearKey(string a, ConfiguracionDeSeries cf)
		{
			ProcesadorDeNombreDeSerie pr = new ProcesadorDeNombreDeSerie(
				                              contextoDeConjunto: new ContextoDeConjuntoDeSeries()
				, re: cf.re//new RecursosDePatronesDeSeries(cf)
				, contexto: new ContextoDeSerie()
				, nombre: a
				
			                              );
			DatosDeNombreSerie dn = pr.crearDatosDeNombre(false);
			TipoDeNombreDeSerie? t = dn.getTipoDeNombre();
			return new KeySerie(
				Nombre: dn.NombreAdaptado
			, Clave: dn.Clave
			, TipoDeSerie: t != null ? (TipoDeNombreDeSerie)t : TipoDeNombreDeSerie.DESCONOCIDO
			);
		}

        public string[] IdentificadoresCantidadCapituloTemporadaPrioridad
        {
            get { return this.identificadoresCantidadCapituloTemporada_Prioridad; }
            set
            {
                this.identificadoresCantidadCapituloTemporada_Prioridad = value;
                identificadoresCantidadCapituloTemporada_Prioridad_Patron = ConstantesExprecionesRegulares.getPatronPalabrasOR(true, value);
            }
        }

        public string[] NombresCarpetaSubtitulos
        {
            get { return this.nombresCarpetaSubtitulos; }
            set {
                this.nombresCarpetaSubtitulos = value;
                this.nombresCarpetaSubtitulos_patron = crearPatron_NombresCarpetaSubtitulos(value);
            }
        }
        private string crearPatron_NombresCarpetaSubtitulos(string[] N)
        {
            string r = "(?:";
            for (int i = 0; i < N.Length; i++)
            {
                
               
                if (i != 0)
                {
                    r += "|";
                }
                r += "(?:";

                r +=  N[i].ToLower()
                    + ConstantesExprecionesRegulares.separaciones
                    + "(?:"
                    + ConstantesExprecionesRegulares.numeros_conComa
                    + ConstantesExprecionesRegulares.separaciones
                    + "(?:mb|kb)?"
                    + ")?"
                    ;

                



                r += ")";

            }
            r += ")";

            return getPatronSiEstaVacio(r); ;
        }


        public string[] SaltarCualquierNumeroDespuesDe {
			get{ return this.saltarCualquierNumeroDespuesDe; }
			set {
				this.saltarCualquierNumeroDespuesDe = value;
				saltarCualquierNumeroDespuesDe_Patron = ConstantesExprecionesRegulares.getPatronPalabrasOR(true, value);
			}
		}
		public string[] SaltarCualquierNumeroAntesDe {
			get{ return this.saltarCualquierNumeroAntesDe; }
			set {
				this.saltarCualquierNumeroAntesDe = value;
				saltarCualquierNumeroAntesDe_Patron = ConstantesExprecionesRegulares.getPatronPalabrasOR(true, value);
			}
		}
		public string[] Union {
			get{ return this.union; }
			set {
				this.union = value;
				union_Patron = ConstantesExprecionesRegulares.getPatronPalabrasOR(true, value);
			}
		}
		public string Union_predeterminada {
			get{ return this.union_predeterminada; }
			set {
				this.union_predeterminada = value;
				union_predeterminada_Patron = ConstantesExprecionesRegulares.getPatronPalabrasOR(true, value); 
			}
		}
		public string[] IdentificadoresTemporadas {
			get{ return this.identificadoresTemporadas; }
			set {
				this.identificadoresTemporadas = value;
				identificadoresTemporadas_Patron = ConstantesExprecionesRegulares.getPatronPalabrasOR(true, value);
			}
		}
		public string[] IdentificadoresCapitulo {
			get{ return this.identificadoresCapitulo; }
			set {
				this.identificadoresCapitulo = value;
				identificadoresCapitulo_Patron = ConstantesExprecionesRegulares.getPatronPalabrasOR(true, value);
			}
		}
		public  TerminacionNumerica[] TerminacionesNumericas {
			get{ return this.terminacionesNumericas; }
			set {
				this.terminacionesNumericas = value;
				terminacionesNumericas_Patron = ConstantesExprecionesRegulares.getPatronTerminacionNumerica(value, KEY_GRUPO_NUMERO_TERMINACION_NUMERICA);
			}
		}
		
		public string [] IgnorarEstaPalabraAlPrincipio {
			get{ return this.ignorarEstaPalabraAlPrincipio; }
			set {
				this.ignorarEstaPalabraAlPrincipio = value;
				ignorarEstaPalabraAlPrincipio_Patron = ConstantesExprecionesRegulares.getPatronPalabrasOR(true, value);
			}
		}
		public string[] IdentificadoresCantidadCapituloTemporada {
			get{ return this.identificadoresCantidadCapituloTemporada; }
			set {
				this.identificadoresCantidadCapituloTemporada = value;
				identificadoresCantidadCapituloTemporada_Patron = ConstantesExprecionesRegulares.getPatronPalabrasOR(true, value);
			}
		}
		public string[] Detenciones {
			get{ return this.detenciones; }
			set {
				this.detenciones = value;
				//	cwl("this.detenciones="+this.detenciones );
				detenciones_Patron = ConstantesExprecionesRegulares.getPatronPalabrasOR(true, value);
				//	cwl("detenciones_Patron="+detenciones_Patron);
			}
		}
		public string[] DetencionesAbsolutas {
			get{ return this.detencionesAbsolutas; }
			set {
				this.detencionesAbsolutas = value;
				detencionesAbsolutas_Patron = ConstantesExprecionesRegulares.getPatronPalabrasOR(true, value);
			}
		}
		//		public string[] NombresRodeadosDeNumeros {
		//			get{ return this.detenciones; }
		//			set {
		//				this.nombresRodeadosDeNumeros = value;
		//				nombresRodeadosDeNumeros_Patron = ConstantesExprecionesRegulares.getPatronPalabrasOR(true, value);
		//			}
		//		}
		public string[] Continuacion {
			get{ return this.continuacion; }
			set {
				this.continuacion = value;
				continuacion_Patron = ConstantesExprecionesRegulares.getPatronPalabrasOR(true, value);
			}
		}
		public string[] SaltarHastaDespuesDe {
			get{ return this.saltarHastaDespuesDe; }
			set {
				this.saltarHastaDespuesDe = value;
				saltarHastaDespuesDe_Patron = ConstantesExprecionesRegulares.getPatronPalabrasOR(true, value);
			}
		}
		public string[] SaltarAlPrincipio {
			get{ return this.saltarAlPrincipio; }
			set {
				this.saltarAlPrincipio = value;
				saltarAlPrincipio_Patron = ConstantesExprecionesRegulares.getPatronPalabrasOR(true, value);
			}
		}
		
		
		
		
		
		
		public CondicionIgnorarNumeroEspecifico[] NombresConNumeroAlPrincipio {
			get{ return nombresConNumeroAlPrincipio; }
			set {
				nombresConNumeroAlPrincipio = value;
				nombresConNumeroAlPrincipio_patron = crearPatron_NombresConNumeroAlPrincipio(value);
			}
		}
		
		private string crearPatron_NombresConNumeroAlPrincipio(CondicionIgnorarNumeroEspecifico[]N)
		{
			string r = "(?:";
			for (int i = 0; i < N.Length; i++) {
				CondicionIgnorarNumeroEspecifico c = N[i];
				bool s = c.aceptarSeparacionesEntreLosElementos;
				if (i != 0) {
					r += "|";
				}
				r += "(?:";
				
				r += "0?" + c.Numero;
			
				foreach (string se in c.Caracteres) {
					
//					if(s){
//						r+=ConstantesExprecionesRegulares.separaciones;
//					}
					r += s ? ConstantesExprecionesRegulares.separaciones : c.Caracteres.Length > 1 ? " " : "";
					r += se.ToLower();
				}
				
				
				
				r += ")";
				
			}
			r += ")";
			
			return getPatronSiEstaVacio(r);;
		}

		public CondicionIgnorarNumeroEspecifico[] NombresConNumeroAlFinal {
			get{ return nombresConNumeroAlFinal; }
			set {
				nombresConNumeroAlFinal = value;
				nombresConNumeroAlFinal_patron = crearPatron_NombresConNumeroAlFinal(value);
			}
		}
		private string getPatronSiEstaVacio(string p){
			return p=="(?:)"?"":p;
		}
		
		private string crearPatron_NombresConNumeroAlFinal(CondicionIgnorarNumeroEspecifico[]N)
		{
			string r = "(?:";
			for (int i = 0; i < N.Length; i++) {
				CondicionIgnorarNumeroEspecifico c = N[i];
				bool s = c.aceptarSeparacionesEntreLosElementos;
				if (i != 0) {
					r += "|";
				}
				r += "(?:";
				
				
			
				foreach (string se in c.Caracteres) {
					r += se.ToLower();
//					if(s){
//						r+=ConstantesExprecionesRegulares.separaciones;
//					}
					r += s ? ConstantesExprecionesRegulares.separaciones : c.Caracteres.Length > 1 ? " " : "";
				}
				
				
				r += "0?" + c.Numero;
				r += ")";
				
			}
			r += ")";
			
			return getPatronSiEstaVacio(r);
		}


		public CondicionIgnorarNumeroEspecificoRodeadoPor[] NombresConUnNumeroInterno {
			get{ return nombresConUnNumeroInterno; }
			private set {
				nombresConUnNumeroInterno = value;
				nombresConUnNumeroInterno_patron = crearPatron_NombresConUnNumeroInterno(value);
                //if (nombresConUnNumeroInterno_patron=="") {
                //    cwl("vacio");
                //}
                //cwl("----------------");
                //cwl(nombresConUnNumeroInterno_patron);
                //cwl("****************");
            }
		}
			
		private string crearPatron_NombresConUnNumeroInterno(CondicionIgnorarNumeroEspecificoRodeadoPor[]N)
		{
			string r = "(?:";
			for (int i = 0; i < N.Length; i++) {
				CondicionIgnorarNumeroEspecificoRodeadoPor c = N[i];
				bool s = c.aceptarSeparacionesEntreLosElementos;
				if (i != 0) {
					r += "|";
				}
				r += "(?:";
				
				
			
				foreach (string se in c.Antes) {
					r += se.ToLower();
					r += s ? ConstantesExprecionesRegulares.separaciones : c.Antes.Length > 1 ? " " : "";
					
				}
				
				
				r += "0?" + c.Numero;
				
				foreach (string se in c.Despues) {
					r += s ? ConstantesExprecionesRegulares.separaciones : c.Despues.Length > 1 ? " " : "";
					r += se.ToLower();
					
					
				}
				
				r += ")";
				
			}
			r += ")";
			
			return getPatronSiEstaVacio(r);
		}


		public CondicionIgnorarNumerosEspecificosSeparadosPor[] NombresRodeadosDeNumeros {
			get{ return nombresRodeadosDeNumeros; }
			set {
				nombresRodeadosDeNumeros = value;
				nombresRodeadosDeNumeros_patron = crearPatron_NombresRodeadosDeNumeros(value);
			}
		}
			
		private string crearPatron_NombresRodeadosDeNumeros(CondicionIgnorarNumerosEspecificosSeparadosPor[]N)
		{
			string r = "(?:";
			for (int i = 0; i < N.Length; i++) {
				CondicionIgnorarNumerosEspecificosSeparadosPor c = N[i];
				bool s = c.aceptarSeparacionesEntreLosElementos;
				if (i != 0) {
					r += "|";
				}
				r += "(?:";
				
				r += "0?" + c.NumeroFinal;
				r += s ? ConstantesExprecionesRegulares.separaciones : c.Separaciones.Length > 1 ? " " : "";
				
				foreach (string se in c.Separaciones) {
					r += se.ToLower();
//					if(s){
//						r+=ConstantesExprecionesRegulares.separaciones;
//					}
					r += s ? ConstantesExprecionesRegulares.separaciones : c.Separaciones.Length > 1 ? " " : "";
				}
				
				
				r += "0?" + c.NumeroFinal;
				r += ")";
				
			}
			r += ")";
			
			return getPatronSiEstaVacio(r);
		}

		public CondicionIgnorarNumerosEspecificos[] NombresConNumerosAlPrincipio {
			get{ return nombresConNumerosAlPrincipio; }
			set {
				nombresConNumerosAlPrincipio = value;
				nombresConNumerosAlPrincipio_patron = crearPatron_NombresConNumerosAlPrincipio(value);
			}
		}
			
		private string crearPatron_NombresConNumerosAlPrincipio(CondicionIgnorarNumerosEspecificos[]N)
		{
			string r = "(?:";
			for (int i = 0; i < N.Length; i++) {
				CondicionIgnorarNumerosEspecificos c = N[i];
				bool s = c.aceptarSeparacionesEntreLosElementos;
				if (i != 0) {
					r += "|";
				}
				r += "(?:";
				
				
				for (int j = 0; j < c.Numeros.Length; j++) {
					r += "0?" + c.Numeros[j];
					if (j != c.Numeros.Length - 1) {
						r += s ? ConstantesExprecionesRegulares.separaciones : c.Caracteres.Length > 1 ? " " : "";
					}
				}
				
				
				foreach (string se in c.Caracteres) {
					r += s ? ConstantesExprecionesRegulares.separaciones : c.Caracteres.Length > 1 ? " " : "";
					r += se.ToLower();
				}
				
				
				
				r += ")";
				
			}
			r += ")";
			
			return getPatronSiEstaVacio(r);
		}

		public CondicionIgnorarConjuntoDeNumeros[] NombresNumericosMultiples {
			get{ return nombresNumericosMultiples; }
			set {
				nombresNumericosMultiples = value;
				nombresNumericosMultiples_patron = crearPatron_NombresNumericosMultiples(value);
			}
		}
		
		private string crearPatron_NombresNumericosMultiples(CondicionIgnorarConjuntoDeNumeros[]N)
		{
			string r = "(?:";
			for (int i = 0; i < N.Length; i++) {
				CondicionIgnorarConjuntoDeNumeros c = N[i];
				bool s = true;
				if (i != 0) {
					r += "|";
				}
				r += "(?:";
				
				
				for (int j = 0; j < c.Numeros.Length; j++) {
					r += "0?" + c.Numeros[j];
					if (j != c.Numeros.Length - 1) {
						//r+=s?ConstantesExprecionesRegulares.separaciones:c.Caracteres.Length>1?" ":"";
						r += s ? ConstantesExprecionesRegulares.separaciones : "";
					}
				}
				
				
				
				
				
				
				r += ")";
				
			}
			r += ")";
			
			return getPatronSiEstaVacio(r);
		}

	}
}
