/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 28/7/2022
 * Hora: 17:18
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
#pragma warning disable CS0105 // The using directive for 'ReneUtiles.Clases.Multimedia.Series' appeared previously in this namespace
using ReneUtiles.Clases.Multimedia.Series;
#pragma warning restore CS0105 // The using directive for 'ReneUtiles.Clases.Multimedia.Series' appeared previously in this namespace
//using ReneUtiles.Clases.Multimedia.Series.Procesadores.Ignorar;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Conjuntos;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos;

namespace ReneUtiles.Clases.Multimedia.Series.Procesadores
{
	/// <summary>
	/// Description of RecursosDePatronesDeSeries.
	/// </summary>
	public class RecursosDePatronesDeSeries:ExprecionesRegularesBasico
	{
		//public ProcesadorDeSerie pr;
		
		
		
		public static readonly string KEY_IDENTIFICADOR_TEMPORADA = "idn_temporada";
		public static readonly string KEY_IDENTIFICADOR_CAPITULO = "idn_capitulo";
		public static readonly string KEY_IDENTIFICADOR_CANTIDAD_CAPITULOS = "idn_cantidad_capitulo";
		public static readonly string KEY_IDENTIFICADOR_CONJUNTO = "idn_conjunto_capitulos";
		public static readonly string KEY_IDENTIFICADOR_CONTINUIDAD = "idn_contidnuidad_capitulos";
		public static readonly string KEY_IDENTIFICADOR_OVA = "idn_capitulo";
		public static readonly string KEY_NUMERO_TEMPORADA = "num_temporada";
		public static readonly string KEY_NUMERO_ROMANO_TEMPORADA = "num_romano_temporada";
		public static readonly string KEY_NUMERO_CAPITULO = "num_capitulo";
		public static readonly string KEY_NUMERO_OVA = "num_ova";
		public static readonly string KEY_NUMERO_CANTIDAD_CAPITULOS = "num_cantidad_capitulo";
		public static readonly string KEY_UNION = "grup_union";
		public static readonly string KEY_CONTENIDO_CAPITULO = "contenido_capitulo";
        public static readonly string KEY_UNION_TEMPORADA = "grup_union_temporada";
        public static readonly string KEY_IDENTIFICADOR_CONJUNTO_TEMPORADA = "idn_conjunto_capitulos_temporada";
        public static readonly string KEY_IDENTIFICADOR_CONTINUIDAD_TEMPORADA = "idn_contidnuidad_capitulos_temporada";


        //public static readonly string KEY_NUMERO_DIA = "dia";
        //public static readonly string KEY_NUMERO_AÑO = "anno";
        //public static readonly string KEY_NUMERO_MES = "mes";
        //public static readonly string KEY_FECHA = "fecha";
        //public static readonly string KEY_SEPARACION_FECHA = "separacion_fecha";
        //public static readonly string KEY_ENVOLTURA_INICIAL_FECHA = "envol_ini_fecha";
        //public static readonly string KEY_ENVOLTURA_FINAL_FECHA = "envol_ini_fecha";

        string patron_OVA;
		string patron_Temporada_NT;
		string patron_Capitulo_NC;
		string patron_Capitulo_N_Union_N_Repetir;
		string patron_Temporada_NT_Capitulo_NC;
		string patron_Capitulo_NC_Temporada_NT;
		string patron_N_Union_N_Repetir;
#pragma warning disable CS0169 // The field 'RecursosDePatronesDeSeries.patron_N_Continuacion_N_Repetir' is never used
		string patron_N_Continuacion_N_Repetir;
#pragma warning restore CS0169 // The field 'RecursosDePatronesDeSeries.patron_N_Continuacion_N_Repetir' is never used
		string patron_Temporada_NT_Capitulo_N_Union_N_Repetir;
		string patron_Capitulo_N_Union_N_Repetir_Temporada_NT;
		string patron_Ova_N;
		string patron_Ova_N_Union_N_Repetir;
		string patron_NTR_NC;
		string patron_NTR_N_Union_N_Repetir;
		string patron_NTR_Capitulo_NC;
		string patron_CapitulosPlu_NC;
		string patron_CapitulosPlu_N_Union_N_Repetir;
		string patron_Temporada_NT_CapitulosPlu_NC;
		string patron_Temporada_NT_CapitulosPlu_N_Union_N_Repetir;
		string patron_Temporada_NT_NC_CapitulosPlu;
		string patron_Temporada_NT_N_Union_N_Repetir_CapitulosPlu;
		string patron_Temporada_NC_CapitulosPlu;
		string patron_Temporada_N_Union_N_Repetir_CapitulosPlu;
        string patron_NT_IT_Temporada;
		string patron_NT_IT_Temporada_Capitulo_NC;
		string patron_NT_IT_Temporada_Capitulo_N_Union_N_Repetir;
		string patron_NT_IT_Temporada_NC_N_Ova;
        string patron_NT_IT_Temporada_NCan_N_Ova;
        string patron_N_Ova;
		string patron_NT_IT_Temporada_NC;
		string patron_Cor_Nc_Mas_Nova_Cor;
		string patron_Cor_Nc_Cor;
		string patron_Cor_Nc_Full_Cor;
		string patron_Cor_NT_Temp_Cor;
		string patron_NxN_PosiblesEspaciosInternos;
		string patron_NxEN;
		string patron_Nx_N_Union_N_Repetir_PosiblesEspaciosInternos;
		string patron_NxE_N_Union_N_Repetir;
		string patron_SNEN;
		string patron_SNxEN;
		string patron_SNE_N_Union_N_Repetir;
		string patron_SNxE_N_Union_N_Repetir;
		//		string patron_SE_NT_SoloConSeparaciones;
		//		string patron_T_NT_SoloConSeparaciones;
		string patron_T_NT;
		string patron_SE_NT;
		string patron_SN;
		string patron_Nombre_SN;
		string patron_NC0_NCi_final;
		string patron_NT_NC0_NCi_final;
		string patron_NT_N_Union_N_Repetir;
		string patron_N_aaaaaaaa_Temporada_NT;
		string patron_N_aaaaaaaa_NT_IT_Temporada;
		string patron_N_Union_N_Repetir_aaaaaaaa_Temporada_NT;
		string patron_N_Union_N_Repetir_aaaaaaaa_NT_IT_Temporada;
		string patron_NT_Temporada_NC;
		string patron_NT_Temporada_N_Union_N_Repetir;
		string patron_NC;
		//string patron_Fecha;
		string patron_NT_Capitulo_NC;
		string patron_NT_Capitulo_N_Union_N_Repetir;
		
		
		string patron_N_Nombre;
		string patron_N_Multiples_Nombre;
		string patron_Nombre_N;
		string patron_Nombre_N_Multiples;
		string patron_N_Multiples_N;
		string patron_Nombre_N_Nombre;
        string patron_NombresCarpetaSubtitulos;


        string patron_CapitulosPluP_NC;
        string patron_Temporada_NT_CapitulosPluP_NC;

        string patron_NT_Union_NT_Repetir;
        string patron_Temporada_Cor_NT_Union_NT_Cor;

        public PatronRegex Re_Temporada_Cor_NT_Union_NT_CorC;
        //		string patron_N_Union_N;
        public PatronRegex Re_CapitulosPluP_NC;
        public PatronRegex Re_Temporada_NT_CapitulosPluP_NC;
        public PatronRegex Re_NombresCarpetaSubtitulos;
        public PatronRegex Re_numerosYLetras;
		public PatronRegex Re_Ova;
		public PatronRegex Re_Temporada_NT_Capitulo_NC;
		public PatronRegex Re_Capitulo_NC_Temporada_NT;
		public PatronRegex Re_N_Union_N_Repetir;
		//public PatronRegex Re_N_Continuacion_N_Repetir;
		public PatronRegex Re_Temporada_NT_Capitulo_N_Union_N_Repetir;
		public PatronRegex Re_Capitulo_N_Union_N_Repetir_Temporada_NT;
		public PatronRegex Re_Ova_N;
		public PatronRegex Re_Ova_N_Union_N_Repetir;
		public PatronRegex Re_NTR_NC;
		public PatronRegex Re_NTR_N_Union_N_Repetir;
		public PatronRegex Re_NTR_Capitulo_NC;
		public PatronRegex Re_CapitulosPlu_NC;
		public PatronRegex Re_CapitulosPlu_N_Union_N_Repetir;
		public PatronRegex Re_Temporada_NT_CapitulosPlu_NC;
		public PatronRegex Re_Temporada_NT_CapitulosPlu_N_Union_N_Repetir;
		public PatronRegex Re_Temporada_NT_NC_CapitulosPlu;
		public PatronRegex Re_Temporada_NT_N_Union_N_Repetir_CapitulosPlu;
		public PatronRegex Re_Temporada_NC_CapitulosPlu;
		public PatronRegex Re_Temporada_N_Union_N_Repetir_CapitulosPlu;
		public PatronRegex Re_NT_IT_Temporada;
		public PatronRegex Re_NT_IT_Temporada_Capitulo_NC;
		public PatronRegex Re_NT_IT_Temporada_Capitulo_N_Union_N_Repetir;
		public PatronRegex Re_NT_IT_Temporada_NC_N_Ova;
        public PatronRegex Re_NT_IT_Temporada_NCan_N_Ova;
        public PatronRegex Re_N_Ova;
		public PatronRegex Re_NT_IT_Temporada_NC;
		public PatronRegex Re_Cor_Nc_Mas_Nova_Cor;
		public PatronRegex Re_Cor_Nc_Cor;
		public PatronRegex Re_Cor_Nc_Full_Cor;
		public PatronRegex Re_Cor_NT_Temp_Cor;
		public PatronRegex Re_NxN_PosiblesEspaciosInternos;
		public PatronRegex Re_NxEN;
		public PatronRegex Re_NxN_Union_N_Repetir_PosiblesEspaciosInternos;
		public PatronRegex Re_NxE_N_Union_N_Repetir;
		public PatronRegex Re_separaciones;
        public PatronRegex Re_separaciones_UnoAlMenos;
        public PatronRegex Re_saltarCualquierNumeroDespuesDe_Patron;
		public PatronRegex Re_SNEN;
		public PatronRegex Re_SNxEN;
		public PatronRegex Re_SNE_N_Union_N_Repetir;
		public PatronRegex Re_SNxE_N_Union_N_Repetir;
		public PatronRegex Re_Temporada_NT;
		public PatronRegex Re_Capitulo_NC;
		public PatronRegex Re_NT_Capitulo_NC;
		public PatronRegex Re_NT_Capitulo_N_Union_N_Repetir;
		public PatronRegex Re_Capitulo_N_Union_N_Repetir;
		public PatronRegex Re_SE_NT_SoloConSeparaciones;
		public PatronRegex Re_T_NT_SoloConSeparaciones;
		public PatronRegex Re_SN;
		public PatronRegex Re_Nombre_SN;
		public PatronRegex Re_NC0_NCi_final;
		public PatronRegex Re_NT_NC0_NCi_final;
		public PatronRegex Re_NT_N_Union_N_Repetir;
		public PatronRegex Re_N_aaaaaaaa_Temporada_NT;
		public PatronRegex Re_N_aaaaaaaa_NT_IT_Temporada;
		public PatronRegex Re_N_Union_N_Repetir_aaaaaaaa_Temporada_NT;
		public PatronRegex Re_N_Union_N_Repetir_aaaaaaaa_NT_IT_Temporada;
		public PatronRegex Re_NT_Temporada_NC;
		public PatronRegex Re_NT_Temporada_N_Union_N_Repetir;
		public PatronRegex Re_NC;
//		public PatronRegex Re_Fecha;
		public PatronRegex Re_SaltarHastaDespuesDe;
		public PatronRegex Re_SaltarAlPrincipio;
		public PatronRegex Re_detenciones;
		public PatronRegex Re_Eliminar;
		public PatronRegex Re_Env_Contenido_;
		
		public PatronRegex Re_nombresConNumeroAlPrincipio;
		public PatronRegex Re_nombresConNumeroAlFinal;
		public PatronRegex Re_nombresConUnNumeroInterno;
		public PatronRegex Re_nombresRodeadosDeNumeros;
		public PatronRegex Re_nombresConNumerosAlPrincipio;
		public PatronRegex Re_nombresNumericosMultiples;
		
		public PatronRegex Re_N_Nombre;
		public PatronRegex Re_N_Multiples_Nombre;
		public PatronRegex Re_Nombre_N;
		public PatronRegex Re_Nombre_N_Multiples;
		public PatronRegex Re_N_Multiples_N;
		public PatronRegex Re_Nombre_N_Nombre;
		
		public PatronRegex Re_Posible_Aleatoriedad;
		public static readonly string KEY_ALEATORIEDAD = "grup_aleatoriedad";
		
		
		//		public PatronRegex Re_N_Union_N;
		
		public ConfiguracionDeSeries cf;
		public RecursosDePatronesDeSeriesGenerales reg;

		
		
		public RecursosDePatronesDeSeries(ConfiguracionDeSeries cf,RecursosDePatronesDeSeriesGenerales reg)//ProcesadorDeSerie pr
		{
			//this.pr = pr;
			this.cf = cf;
			this.reg=reg;
			Re_Posible_Aleatoriedad=new PatronRegex(
				grupoNombrado(KEY_ALEATORIEDAD,@"[a-zA-Z0-9]{5,}")
				);
			
			
			
			patron_N_Nombre = ConstantesExprecionesRegulares.numeros
			+ ConstantesExprecionesRegulares.separaciones
			+ ConstantesExprecionesRegulares.letras;
			patron_N_Multiples_Nombre = "(?:"
			+ ConstantesExprecionesRegulares.numeros
			+ ConstantesExprecionesRegulares.separaciones
			+ ")+"
			+ ConstantesExprecionesRegulares.letras;
			patron_Nombre_N = ConstantesExprecionesRegulares.letras
			+ ConstantesExprecionesRegulares.separaciones
			+ ConstantesExprecionesRegulares.numeros;
			patron_Nombre_N_Multiples = ConstantesExprecionesRegulares.letras
			+ "(?:"
			+ ConstantesExprecionesRegulares.separaciones
			+ ConstantesExprecionesRegulares.numeros
			+ ")+";
			patron_N_Multiples_N = ConstantesExprecionesRegulares.numeros
			+ "(?:"
			+ ConstantesExprecionesRegulares.separaciones
			+ ConstantesExprecionesRegulares.numeros
			+ ")+";
			patron_Nombre_N_Nombre=ConstantesExprecionesRegulares.letras
				+ ConstantesExprecionesRegulares.separaciones
			+ ConstantesExprecionesRegulares.numeros
				+ ConstantesExprecionesRegulares.separaciones
				+ConstantesExprecionesRegulares.letras;
			
			Re_N_Nombre = new PatronRegex(patron_N_Nombre);
			Re_N_Multiples_Nombre = new PatronRegex(patron_N_Multiples_Nombre);
			Re_Nombre_N = new PatronRegex(patron_Nombre_N);
			Re_Nombre_N_Multiples = new PatronRegex(patron_Nombre_N_Multiples);
			Re_N_Multiples_N = new PatronRegex(patron_N_Multiples_N);
			Re_Nombre_N_Nombre=new PatronRegex(patron_Nombre_N_Nombre);

            string nombresConNumeroAlPrincipio_patron = cf.GetNombresConNumeroAlPrincipio_patron();

            Re_nombresConNumeroAlPrincipio = new PatronRegex(nombresConNumeroAlPrincipio_patron);
            string nombresConNumeroAlFinal_patron = cf.GetNombresConNumeroAlFinal_patron();
            Re_nombresConNumeroAlFinal = new PatronRegex(nombresConNumeroAlFinal_patron);
            string nombresConUnNumeroInterno_patron = cf.GetNombresConUnNumeroInterno_patron();
            Re_nombresConUnNumeroInterno = new PatronRegex(nombresConUnNumeroInterno_patron);
            string nombresRodeadosDeNumeros_patron = cf.GetNombresRodeadosDeNumeros_patron();
            Re_nombresRodeadosDeNumeros = new PatronRegex(nombresRodeadosDeNumeros_patron);
            string nombresConNumerosAlPrincipio_patron = cf.GetNombresConNumerosAlPrincipio_patron();
            Re_nombresConNumerosAlPrincipio = new PatronRegex(nombresConNumerosAlPrincipio_patron);
            string nombresNumericosMultiples_patron = cf.GetNombresNumericosMultiples_patron();
            Re_nombresNumericosMultiples = new PatronRegex(nombresNumericosMultiples_patron);
			
				
			Re_Env_Contenido_ = new PatronRegex("((?:[(]|[[]|[{]).*)");
			Re_Eliminar = new PatronRegex(@"(and|y|[&])");
			Re_numerosYLetras = new PatronRegex(ConstantesExprecionesRegulares.numerosYLetras);
			patron_OVA = grupoNombrado(KEY_IDENTIFICADOR_OVA, "ova");
			Re_Ova = new PatronRegex(patron_OVA);
			
			patron_Temporada_NT = 
				grupoNombrado(KEY_IDENTIFICADOR_TEMPORADA, cf.identificadoresTemporadas_Patron)
			+
			ConstantesExprecionesRegulares.separaciones +	//-
			grupoNombrado(KEY_NUMERO_TEMPORADA, ConstantesExprecionesRegulares.numeros_4max);
			
			patron_Capitulo_NC = 
				grupoNombrado(KEY_IDENTIFICADOR_CAPITULO, cf.identificadoresCapitulo_Patron)
			+
			ConstantesExprecionesRegulares.separaciones +	//-	
			grupoNombrado(KEY_NUMERO_CAPITULO, ConstantesExprecionesRegulares.numeros_4max);	
			
			patron_Temporada_NT_Capitulo_NC =
				patron_Temporada_NT +
			ConstantesExprecionesRegulares.separaciones +
			patron_Capitulo_NC;
			
			patron_Capitulo_NC_Temporada_NT =
				patron_Capitulo_NC +
			ConstantesExprecionesRegulares.separaciones +
			patron_Temporada_NT;
			
			Re_Temporada_NT_Capitulo_NC = new PatronRegex(patron_Temporada_NT_Capitulo_NC);
			Re_Capitulo_NC_Temporada_NT = new PatronRegex(patron_Capitulo_NC_Temporada_NT);
			
			patron_N_Union_N_Repetir =
				
				 grupoNombrado(KEY_UNION,
				orExpr(
					grupoNombrado(KEY_NUMERO_CAPITULO, ConstantesExprecionesRegulares.numeros_4max) +
					unoAlMenos(ConstantesExprecionesRegulares.espacios +
					grupoNombrado(KEY_IDENTIFICADOR_CONJUNTO, cf.union_Patron) +
					ConstantesExprecionesRegulares.espacios +
					grupoNombrado(KEY_NUMERO_CAPITULO, ConstantesExprecionesRegulares.numeros_4max))
				,// ----------------------------------
					grupoNombrado(KEY_NUMERO_CAPITULO, ConstantesExprecionesRegulares.numeros_4max) +
					unoAlMenos(ConstantesExprecionesRegulares.espacios +
				           
					grupoNombrado(KEY_IDENTIFICADOR_CONTINUIDAD, cf.continuacion_Patron)
					+ ConstantesExprecionesRegulares.espacios +
					grupoNombrado(KEY_NUMERO_CAPITULO, ConstantesExprecionesRegulares.numeros_4max))
				)
				              
				
			);
			Re_N_Union_N_Repetir = new PatronRegex(patron_Capitulo_NC_Temporada_NT);
			//Re_N_Continuacion_N_Repetir
			
			patron_Temporada_NT_Capitulo_N_Union_N_Repetir = patron_Temporada_NT +
			ConstantesExprecionesRegulares.separaciones +
			grupoNombrado(KEY_IDENTIFICADOR_CAPITULO, cf.identificadoresCapitulo_Patron)
			+ ConstantesExprecionesRegulares.separaciones
			+ patron_N_Union_N_Repetir;
			
			Re_Temporada_NT_Capitulo_N_Union_N_Repetir = new PatronRegex(patron_Temporada_NT_Capitulo_N_Union_N_Repetir);
			
			patron_Capitulo_N_Union_N_Repetir_Temporada_NT =
				grupoNombrado(KEY_IDENTIFICADOR_CAPITULO, cf.identificadoresCapitulo_Patron)
			+
			ConstantesExprecionesRegulares.separaciones +	//-	
			patron_N_Union_N_Repetir +
			ConstantesExprecionesRegulares.separaciones +
			patron_Temporada_NT;
			Re_Capitulo_N_Union_N_Repetir_Temporada_NT = new PatronRegex(patron_Capitulo_N_Union_N_Repetir_Temporada_NT);
			
			patron_Ova_N = patron_OVA +
			ConstantesExprecionesRegulares.separaciones +
			grupoNombrado(KEY_NUMERO_CAPITULO, ConstantesExprecionesRegulares.numeros_4max);
			Re_Ova_N = new PatronRegex(patron_Ova_N);
			
			patron_Ova_N_Union_N_Repetir = patron_OVA +
			ConstantesExprecionesRegulares.separaciones + patron_N_Union_N_Repetir;
			Re_Ova_N_Union_N_Repetir = new PatronRegex(patron_Ova_N_Union_N_Repetir);
			
			//ConstantesExprecionesRegulares.separaciones_UnoAlMenos +
			patron_NTR_NC = grupoNombrado(KEY_NUMERO_ROMANO_TEMPORADA, ConstantesExprecionesRegulares.NumerosRomanos0_9)
			+ ConstantesExprecionesRegulares.separaciones_UnoAlMenos
			+ grupoNombrado(KEY_NUMERO_CAPITULO, ConstantesExprecionesRegulares.numeros_4max);
			Re_NTR_NC = new PatronRegex(patron_NTR_NC);
			
			patron_NTR_Capitulo_NC = grupoNombrado(KEY_NUMERO_ROMANO_TEMPORADA, ConstantesExprecionesRegulares.NumerosRomanos0_9)
			+ ConstantesExprecionesRegulares.separaciones_UnoAlMenos
			+ grupoNombrado(KEY_IDENTIFICADOR_CAPITULO, cf.identificadoresCapitulo_Patron)
			+ ConstantesExprecionesRegulares.separaciones
			+ grupoNombrado(KEY_NUMERO_CAPITULO, ConstantesExprecionesRegulares.numeros_4max);
			Re_NTR_Capitulo_NC = new PatronRegex(patron_NTR_Capitulo_NC);
			
			patron_NTR_N_Union_N_Repetir = grupoNombrado(KEY_NUMERO_ROMANO_TEMPORADA, ConstantesExprecionesRegulares.NumerosRomanos0_9)
			+ ConstantesExprecionesRegulares.separaciones_UnoAlMenos
			+ patron_N_Union_N_Repetir;
			Re_NTR_N_Union_N_Repetir = new PatronRegex(patron_NTR_N_Union_N_Repetir);
			
			patron_CapitulosPlu_NC =
				grupoNombrado(KEY_IDENTIFICADOR_CANTIDAD_CAPITULOS, cf.identificadoresCantidadCapituloTemporada_Patron) +
			ConstantesExprecionesRegulares.separaciones +
			grupoNombrado(KEY_NUMERO_CANTIDAD_CAPITULOS, ConstantesExprecionesRegulares.numeros_4max);
			Re_CapitulosPlu_NC = new PatronRegex(patron_CapitulosPlu_NC);
			
			patron_Temporada_NT_CapitulosPlu_NC =
				patron_Temporada_NT +
			ConstantesExprecionesRegulares.separaciones +
			patron_CapitulosPlu_NC;
			Re_Temporada_NT_CapitulosPlu_NC = new PatronRegex(patron_Temporada_NT_CapitulosPlu_NC);
			
			
			patron_CapitulosPlu_N_Union_N_Repetir = grupoNombrado(KEY_IDENTIFICADOR_CANTIDAD_CAPITULOS, cf.identificadoresCantidadCapituloTemporada_Patron) +
			ConstantesExprecionesRegulares.separaciones + patron_N_Union_N_Repetir;
			Re_CapitulosPlu_N_Union_N_Repetir = new PatronRegex(patron_CapitulosPlu_N_Union_N_Repetir);
			
			patron_Temporada_NT_CapitulosPlu_N_Union_N_Repetir =
				patron_Temporada_NT +
			ConstantesExprecionesRegulares.separaciones +
			patron_CapitulosPlu_N_Union_N_Repetir;
			Re_Temporada_NT_CapitulosPlu_N_Union_N_Repetir = new PatronRegex(patron_Temporada_NT_CapitulosPlu_N_Union_N_Repetir);
			
			patron_Temporada_NT_NC_CapitulosPlu =
				patron_Temporada_NT +
			ConstantesExprecionesRegulares.separaciones_UnoAlMenos +
			grupoNombrado(KEY_NUMERO_CANTIDAD_CAPITULOS, ConstantesExprecionesRegulares.numeros_4max) +
			ConstantesExprecionesRegulares.separaciones +
			grupoNombrado(KEY_IDENTIFICADOR_CANTIDAD_CAPITULOS, cf.identificadoresCantidadCapituloTemporada_Patron);
			Re_Temporada_NT_NC_CapitulosPlu = new PatronRegex(patron_Temporada_NT_NC_CapitulosPlu);
			
			
			patron_Temporada_NT_N_Union_N_Repetir_CapitulosPlu =
				patron_Temporada_NT +
			ConstantesExprecionesRegulares.separaciones_UnoAlMenos +
			patron_N_Union_N_Repetir +
			ConstantesExprecionesRegulares.separaciones +
			grupoNombrado(KEY_IDENTIFICADOR_CANTIDAD_CAPITULOS, cf.identificadoresCantidadCapituloTemporada_Patron);
			Re_Temporada_NT_N_Union_N_Repetir_CapitulosPlu = new PatronRegex(patron_Temporada_NT_N_Union_N_Repetir_CapitulosPlu);
			
			patron_Temporada_NC_CapitulosPlu =
				grupoNombrado(KEY_IDENTIFICADOR_TEMPORADA, cf.identificadoresTemporadas_Patron) +
		  	
			ConstantesExprecionesRegulares.separaciones_UnoAlMenos +
			grupoNombrado(KEY_NUMERO_CANTIDAD_CAPITULOS, ConstantesExprecionesRegulares.numeros_4max) +
			ConstantesExprecionesRegulares.separaciones +
			grupoNombrado(KEY_IDENTIFICADOR_CANTIDAD_CAPITULOS, cf.identificadoresCantidadCapituloTemporada_Patron);
			Re_Temporada_NC_CapitulosPlu = new PatronRegex(patron_Temporada_NC_CapitulosPlu);
			
			
			patron_Temporada_N_Union_N_Repetir_CapitulosPlu =
				grupoNombrado(KEY_IDENTIFICADOR_TEMPORADA, cf.identificadoresTemporadas_Patron) +
		  	
			ConstantesExprecionesRegulares.separaciones_UnoAlMenos +
			patron_N_Union_N_Repetir +
			ConstantesExprecionesRegulares.separaciones +
			grupoNombrado(KEY_IDENTIFICADOR_CANTIDAD_CAPITULOS, cf.identificadoresCantidadCapituloTemporada_Patron);
			Re_Temporada_N_Union_N_Repetir_CapitulosPlu = new PatronRegex(patron_Temporada_N_Union_N_Repetir_CapitulosPlu);
			
			
			patron_NT_IT_Temporada = 
			//grupoNombrado(KEY_NUMERO_TEMPORADA, ConstantesExprecionesRegulares.numeros_4max) +
				this.cf.terminacionesNumericas_Patron.Replace(ConfiguracionDeSeries.KEY_GRUPO_NUMERO_TERMINACION_NUMERICA, KEY_NUMERO_TEMPORADA) + //-
			ConstantesExprecionesRegulares.separaciones +	//-
			grupoNombrado(KEY_IDENTIFICADOR_TEMPORADA, cf.identificadoresTemporadas_Patron);
			Re_NT_IT_Temporada = new PatronRegex(patron_NT_IT_Temporada);
			
			patron_NT_IT_Temporada_Capitulo_NC =
				patron_NT_IT_Temporada +
			ConstantesExprecionesRegulares.separaciones +
			patron_Capitulo_NC;
			
			
			Re_NT_IT_Temporada_Capitulo_NC = new PatronRegex(patron_NT_IT_Temporada_Capitulo_NC);
			
			patron_NT_IT_Temporada_Capitulo_N_Union_N_Repetir = patron_NT_IT_Temporada +
			ConstantesExprecionesRegulares.separaciones +
			grupoNombrado(KEY_IDENTIFICADOR_CAPITULO, cf.identificadoresCapitulo_Patron)
			+
			ConstantesExprecionesRegulares.separaciones +	//-	
			patron_N_Union_N_Repetir;
			Re_NT_IT_Temporada_Capitulo_N_Union_N_Repetir = new PatronRegex(patron_NT_IT_Temporada_Capitulo_N_Union_N_Repetir);
			
			
			
			patron_N_Ova =
				grupoNombrado(KEY_NUMERO_OVA, ConstantesExprecionesRegulares.numeros_4max) +
			ConstantesExprecionesRegulares.separaciones +
			patron_OVA;
			
			Re_N_Ova = new PatronRegex(patron_N_Ova);
			
			patron_NT_IT_Temporada_NC =
				patron_NT_IT_Temporada +
			ConstantesExprecionesRegulares.separaciones_UnoAlMenos +
			grupoNombrado(KEY_NUMERO_CAPITULO, ConstantesExprecionesRegulares.numeros_4max);

			Re_NT_IT_Temporada_NC = new PatronRegex(patron_NT_IT_Temporada_NC);
			
			patron_NT_IT_Temporada_NC_N_Ova = patron_NT_IT_Temporada_NC +
			ConstantesExprecionesRegulares.separaciones_UnoAlMenos +
			patron_N_Ova;
			Re_NT_IT_Temporada_NC_N_Ova = new PatronRegex(patron_NT_IT_Temporada_NC_N_Ova);


            patron_NT_IT_Temporada_NCan_N_Ova =
                patron_NT_IT_Temporada +
            ConstantesExprecionesRegulares.separaciones_UnoAlMenos +
            grupoNombrado(KEY_NUMERO_CANTIDAD_CAPITULOS, ConstantesExprecionesRegulares.numeros_4max)
            + ConstantesExprecionesRegulares.separaciones_UnoAlMenos +
            patron_N_Ova;
            ;
            Re_NT_IT_Temporada_NCan_N_Ova = new PatronRegex(patron_NT_IT_Temporada_NCan_N_Ova);
            

            patron_Cor_Nc_Mas_Nova_Cor =
				ConstantesExprecionesRegulares.envolturaInicial +
			ConstantesExprecionesRegulares.separaciones +
			grupoNombrado(KEY_NUMERO_CANTIDAD_CAPITULOS, ConstantesExprecionesRegulares.numeros_4max) +
			ConstantesExprecionesRegulares.separaciones +
			"[+]" +
			ConstantesExprecionesRegulares.separaciones +
			grupoNombrado(KEY_NUMERO_OVA, ConstantesExprecionesRegulares.numeros_4max) +
			ConstantesExprecionesRegulares.separaciones +
			ConstantesExprecionesRegulares.envolturaFinal;
			
			Re_Cor_Nc_Mas_Nova_Cor = new PatronRegex(patron_Cor_Nc_Mas_Nova_Cor);
			
			
			
			patron_Cor_Nc_Cor = ConstantesExprecionesRegulares.envolturaInicial +
			ConstantesExprecionesRegulares.separaciones +
			grupoNombrado(KEY_NUMERO_CANTIDAD_CAPITULOS, ConstantesExprecionesRegulares.numeros_4max) +
			ConstantesExprecionesRegulares.separaciones +
			ConstantesExprecionesRegulares.envolturaFinal;
			
			Re_Cor_Nc_Cor = new PatronRegex(patron_Cor_Nc_Cor);
			
			patron_Cor_Nc_Full_Cor = ConstantesExprecionesRegulares.envolturaInicial +
			ConstantesExprecionesRegulares.separaciones +
			grupoNombrado(KEY_NUMERO_CANTIDAD_CAPITULOS, ConstantesExprecionesRegulares.numeros_4max) +
			ConstantesExprecionesRegulares.separaciones +
			grupoNombrado(KEY_IDENTIFICADOR_CANTIDAD_CAPITULOS, "(?:Full|(Complet[ao])|" + cf.identificadoresCantidadCapituloTemporada_Patron + ")") + //+cf.identificadoresTemporadas_Patron+//|Cap(?:(?:s)|(?:itulos))?
				
				
			ConstantesExprecionesRegulares.separaciones +
			ConstantesExprecionesRegulares.envolturaFinal;
			Re_Cor_Nc_Full_Cor = new PatronRegex(patron_Cor_Nc_Full_Cor);
			
			
			patron_Cor_NT_Temp_Cor =
				ConstantesExprecionesRegulares.envolturaInicial +
			ConstantesExprecionesRegulares.separaciones +
			grupoNombrado(KEY_NUMERO_TEMPORADA, ConstantesExprecionesRegulares.numeros_4max) +
			ConstantesExprecionesRegulares.separaciones +
			grupoNombrado(KEY_IDENTIFICADOR_TEMPORADA, cf.identificadoresTemporadas_Patron) +
			ConstantesExprecionesRegulares.separaciones +
			ConstantesExprecionesRegulares.envolturaFinal;
			Re_Cor_NT_Temp_Cor = new PatronRegex(patron_Cor_NT_Temp_Cor);
			
			patron_NxN_PosiblesEspaciosInternos = 
				grupoNombrado(KEY_NUMERO_TEMPORADA, ConstantesExprecionesRegulares.numeros_4max)
			+ ConstantesExprecionesRegulares.espacios
			+ grupoNombrado(KEY_IDENTIFICADOR_CAPITULO, "(?:x|X)")
			+ ConstantesExprecionesRegulares.espacios
			+ grupoNombrado(KEY_NUMERO_CAPITULO, ConstantesExprecionesRegulares.numeros_4max);
			Re_NxN_PosiblesEspaciosInternos = new PatronRegex(patron_NxN_PosiblesEspaciosInternos);
			
			patron_NxEN = //@"(?:\d{1,3})(?:x|X)[Ee](?:\d{1,3})";
				grupoNombrado(KEY_NUMERO_TEMPORADA, ConstantesExprecionesRegulares.numeros_4max)
			+ grupoNombrado(KEY_IDENTIFICADOR_CAPITULO, @"(?:x|X)[Ee]")
			+ grupoNombrado(KEY_NUMERO_CAPITULO, ConstantesExprecionesRegulares.numeros_4max);
			;
			Re_NxEN = new PatronRegex(patron_NxEN);
			
			patron_Nx_N_Union_N_Repetir_PosiblesEspaciosInternos =
				grupoNombrado(KEY_NUMERO_TEMPORADA, ConstantesExprecionesRegulares.numeros_4max)
			+ ConstantesExprecionesRegulares.espacios
			+ grupoNombrado(KEY_IDENTIFICADOR_CAPITULO, "(?:x|X)")
			+ ConstantesExprecionesRegulares.espacios
			+ patron_N_Union_N_Repetir;
			
			Re_NxN_Union_N_Repetir_PosiblesEspaciosInternos = new PatronRegex(patron_Nx_N_Union_N_Repetir_PosiblesEspaciosInternos);
			patron_NxE_N_Union_N_Repetir = //@"(?:\d{1,3})(?:x|X)[Ee](?:\d{1,3})";
				grupoNombrado(KEY_NUMERO_TEMPORADA, ConstantesExprecionesRegulares.numeros_4max)
			+ grupoNombrado(KEY_IDENTIFICADOR_CAPITULO, @"(?:x|X)[Ee]")
			+ patron_N_Union_N_Repetir;
			;
			Re_NxE_N_Union_N_Repetir = new PatronRegex(patron_NxE_N_Union_N_Repetir);
			
			
			Re_separaciones = new PatronRegex(ConstantesExprecionesRegulares.separaciones);
            Re_separaciones_UnoAlMenos = new PatronRegex(ConstantesExprecionesRegulares.separaciones_UnoAlMenos);

            Re_saltarCualquierNumeroDespuesDe_Patron = new PatronRegex(this.cf.saltarCualquierNumeroDespuesDe_Patron);
			
			patron_SNEN = grupoNombrado(KEY_IDENTIFICADOR_TEMPORADA, @"(?:[Ss])")
			+ grupoNombrado(KEY_NUMERO_TEMPORADA, ConstantesExprecionesRegulares.numeros_4max)
			+ ConstantesExprecionesRegulares.separaciones
			+ grupoNombrado(KEY_IDENTIFICADOR_CAPITULO, @"(?:[Ee])")
			+ grupoNombrado(KEY_NUMERO_CAPITULO, ConstantesExprecionesRegulares.numeros_4max);
		
			
			Re_SNEN = new PatronRegex(patron_SNEN);
			
			patron_SNxEN = //@"(?:[Ss]\d{1,3})(?:x|X)[Ee](?:\d{1,3})"
				grupoNombrado(KEY_IDENTIFICADOR_TEMPORADA, @"(?:[Ss])")
			+ grupoNombrado(KEY_NUMERO_TEMPORADA, ConstantesExprecionesRegulares.numeros_4max)
			+ grupoNombrado(KEY_IDENTIFICADOR_CAPITULO, @"(?:x|X|[Ee])")
			+ grupoNombrado(KEY_NUMERO_CAPITULO, ConstantesExprecionesRegulares.numeros_4max);
			Re_SNxEN = new PatronRegex(patron_SNxEN);
			
			
			
			patron_SNE_N_Union_N_Repetir = grupoNombrado(KEY_IDENTIFICADOR_TEMPORADA, @"(?:[Ss])")
			+ grupoNombrado(KEY_NUMERO_TEMPORADA, ConstantesExprecionesRegulares.numeros_4max)
			+ ConstantesExprecionesRegulares.separaciones
			+ grupoNombrado(KEY_IDENTIFICADOR_CAPITULO, @"(?:[Ee])")
			+ patron_N_Union_N_Repetir;
		
			
			Re_SNE_N_Union_N_Repetir = new PatronRegex(patron_SNE_N_Union_N_Repetir);
			
			patron_SNxE_N_Union_N_Repetir = //@"(?:[Ss]\d{1,3})(?:x|X)[Ee](?:\d{1,3})"
				grupoNombrado(KEY_IDENTIFICADOR_TEMPORADA, @"(?:[Ss])")
			+ grupoNombrado(KEY_NUMERO_TEMPORADA, ConstantesExprecionesRegulares.numeros_4max)
			+ grupoNombrado(KEY_IDENTIFICADOR_CAPITULO, @"(?:x|X|[Ee])")
			+ patron_N_Union_N_Repetir;
			Re_SNxE_N_Union_N_Repetir = new PatronRegex(patron_SNxE_N_Union_N_Repetir);
			
			
			Re_Temporada_NT = new PatronRegex(patron_Temporada_NT);
			Re_Capitulo_NC = new PatronRegex(patron_Capitulo_NC);
			
			patron_Capitulo_N_Union_N_Repetir = 
				grupoNombrado(KEY_IDENTIFICADOR_CAPITULO, cf.identificadoresCapitulo_Patron)
			+
			ConstantesExprecionesRegulares.separaciones +	//-	
			patron_N_Union_N_Repetir;
			Re_Capitulo_N_Union_N_Repetir = new PatronRegex(patron_Capitulo_N_Union_N_Repetir);
			
			
			patron_SE_NT =
				grupoNombrado(KEY_IDENTIFICADOR_TEMPORADA, @"[Ss][Ee]") +
			ConstantesExprecionesRegulares.separaciones +
			grupoNombrado(KEY_NUMERO_TEMPORADA, ConstantesExprecionesRegulares.numeros_4max);
			
//			patron_SE_NT_SoloConSeparaciones=//"^" + 
//				//ConstantesExprecionesRegulares.separaciones +
//				 patron_SE_NT 
//				//+ ConstantesExprecionesRegulares.separaciones + "$"
//				;
			Re_SE_NT_SoloConSeparaciones = new PatronRegex(patron_SE_NT);
			
			patron_T_NT =
				grupoNombrado(KEY_IDENTIFICADOR_TEMPORADA, @"[Tt]") +
			ConstantesExprecionesRegulares.separaciones +
			grupoNombrado(KEY_NUMERO_TEMPORADA, ConstantesExprecionesRegulares.numeros_4max);
			Re_T_NT_SoloConSeparaciones = new PatronRegex(patron_T_NT);
			
			patron_SN =
				grupoNombrado(KEY_IDENTIFICADOR_TEMPORADA, @"[Ss]") +
			grupoNombrado(KEY_NUMERO_TEMPORADA, ConstantesExprecionesRegulares.numeros_4max);
			Re_SN = new PatronRegex(patron_Nombre_SN);
			patron_Nombre_SN =
					"(.{3,})" +
			ConstantesExprecionesRegulares.separaciones_UnoAlMenos +
			patron_SN + "$";
			Re_Nombre_SN = new PatronRegex(patron_Nombre_SN);
			
			
			patron_NC0_NCi_final =
					grupoNombrado(KEY_NUMERO_CAPITULO, ConstantesExprecionesRegulares.numeros_4max) +
			ConstantesExprecionesRegulares.separaciones_UnoAlMenos +
			grupoNombrado(KEY_NUMERO_CAPITULO, ConstantesExprecionesRegulares.numeros_4max) +
			ConstantesExprecionesRegulares.separaciones_UnoAlMenos +
			grupoNombrado(KEY_IDENTIFICADOR_TEMPORADA, "[Ff][Ii][Nn][Aa][Ll]");
			Re_NC0_NCi_final = new PatronRegex(patron_NC0_NCi_final);
			
			
			patron_NT_NC0_NCi_final =
				grupoNombrado(KEY_NUMERO_TEMPORADA, ConstantesExprecionesRegulares.numeros_4max) +
			ConstantesExprecionesRegulares.separaciones_UnoAlMenos
			+ patron_NC0_NCi_final;
			Re_NT_NC0_NCi_final = new PatronRegex(patron_NC0_NCi_final);
			
			patron_NT_N_Union_N_Repetir =
				grupoNombrado(KEY_NUMERO_TEMPORADA, ConstantesExprecionesRegulares.numeros_4max) +
			ConstantesExprecionesRegulares.espacios_UnoAlMenos +
			patron_N_Union_N_Repetir;
			Re_NT_N_Union_N_Repetir = new PatronRegex(patron_NT_N_Union_N_Repetir);
			
			
			patron_N_aaaaaaaa_Temporada_NT =
					grupoNombrado(KEY_NUMERO_CAPITULO, ConstantesExprecionesRegulares.numeros_4max) +
			ConstantesExprecionesRegulares.separaciones_UnoAlMenos +
			grupoNombrado(KEY_CONTENIDO_CAPITULO, ConstantesExprecionesRegulares.cualquieras) +
			ConstantesExprecionesRegulares.separaciones_UnoAlMenos +
			patron_Temporada_NT;
			Re_N_aaaaaaaa_Temporada_NT = new PatronRegex(patron_N_aaaaaaaa_Temporada_NT);
			
			patron_N_aaaaaaaa_NT_IT_Temporada =
					grupoNombrado(KEY_NUMERO_CAPITULO, ConstantesExprecionesRegulares.numeros_4max) +
			ConstantesExprecionesRegulares.separaciones_UnoAlMenos +
			grupoNombrado(KEY_CONTENIDO_CAPITULO, ConstantesExprecionesRegulares.cualquieras) +
			ConstantesExprecionesRegulares.separaciones_UnoAlMenos +
			patron_NT_IT_Temporada;
			Re_N_aaaaaaaa_NT_IT_Temporada = new PatronRegex(patron_N_aaaaaaaa_NT_IT_Temporada);
				
				
			patron_N_Union_N_Repetir_aaaaaaaa_Temporada_NT =
					patron_N_Union_N_Repetir +
			ConstantesExprecionesRegulares.separaciones_UnoAlMenos +
			grupoNombrado(KEY_CONTENIDO_CAPITULO, ConstantesExprecionesRegulares.cualquieras) +
			ConstantesExprecionesRegulares.separaciones_UnoAlMenos +
			patron_Temporada_NT;
			Re_N_Union_N_Repetir_aaaaaaaa_Temporada_NT = new PatronRegex(patron_N_Union_N_Repetir_aaaaaaaa_Temporada_NT);
			
			patron_N_Union_N_Repetir_aaaaaaaa_NT_IT_Temporada =
				patron_N_Union_N_Repetir_aaaaaaaa_Temporada_NT =
					patron_N_Union_N_Repetir +
			ConstantesExprecionesRegulares.separaciones_UnoAlMenos +
			grupoNombrado(KEY_CONTENIDO_CAPITULO, ConstantesExprecionesRegulares.cualquieras) +
			ConstantesExprecionesRegulares.separaciones_UnoAlMenos +
			patron_NT_IT_Temporada;
			Re_N_Union_N_Repetir_aaaaaaaa_NT_IT_Temporada = new PatronRegex(patron_N_Union_N_Repetir_aaaaaaaa_NT_IT_Temporada);
			
			patron_NT_Temporada_NC =
				grupoNombrado(KEY_NUMERO_TEMPORADA, ConstantesExprecionesRegulares.numeros_4max) +
			ConstantesExprecionesRegulares.separaciones +
			grupoNombrado(KEY_IDENTIFICADOR_TEMPORADA, this.cf.identificadoresTemporadas_Patron) +
			ConstantesExprecionesRegulares.separaciones +
			grupoNombrado(KEY_NUMERO_CAPITULO, ConstantesExprecionesRegulares.numeros_4max);
			Re_NT_Temporada_NC = new PatronRegex(patron_NT_Temporada_NC);
			
			patron_NT_Temporada_N_Union_N_Repetir =
				grupoNombrado(KEY_NUMERO_TEMPORADA, ConstantesExprecionesRegulares.numeros_4max) +
			ConstantesExprecionesRegulares.separaciones +
			grupoNombrado(KEY_IDENTIFICADOR_TEMPORADA, this.cf.identificadoresTemporadas_Patron) +
			ConstantesExprecionesRegulares.separaciones +
			patron_N_Union_N_Repetir;
			
			Re_NT_Temporada_N_Union_N_Repetir = new PatronRegex(patron_NT_Temporada_N_Union_N_Repetir);
			
			patron_NC = grupoNombrado(KEY_NUMERO_CAPITULO, ConstantesExprecionesRegulares.numeros_4max);
			Re_NC = new PatronRegex(patron_NC);
			
			
			
			//string dia = @"(?<" + KEY_NUMERO_DIA + @">(?:[0-2]?[0-9])|(?:3[0-1]))";
			//string diaAlto = @"(?<" + KEY_NUMERO_DIA + @">(?:1[3-9])|(?:2[0-9])|(?:3[0-1]))";
			//string nNormal = @"(?:0?[0-9])|(?:1[0-2])";
			//string mes = @"(?<" + KEY_NUMERO_MES + @">" + nNormal + ")";
			//string gSeparacion = @"(?<" + KEY_SEPARACION_FECHA + @">[-_. ])";
			//string año = @"(?<" + KEY_NUMERO_AÑO + @">(?:19[5-9][0-9])|(?:20[0-9][0-9]))";
			//string kSeparacion = @"\k<" + KEY_SEPARACION_FECHA + @">";
			//string envolturaInicial = @"(?<" + KEY_ENVOLTURA_INICIAL_FECHA + @">[[]|[(]|[{])";
			//string envolturaFinal = @"(?<" + KEY_ENVOLTURA_FINAL_FECHA + @">[]]|[)]|[}])";
			//patron_Fecha = @"(?<" + KEY_FECHA + @">(?:" + diaAlto + gSeparacion + mes + kSeparacion + año + @")|(?:" + mes + gSeparacion + diaAlto + kSeparacion + año + @")|(?:" + dia + gSeparacion + mes + kSeparacion + año + @")|(?:" + envolturaInicial + año + envolturaFinal + @")|(?:" + año + @"))";
			//Re_Fecha = new PatronRegex(patron_Fecha);
			
			Re_SaltarHastaDespuesDe = new PatronRegex("(" + cf.saltarHastaDespuesDe_Patron + ")");
			Re_SaltarAlPrincipio = new PatronRegex("(" + cf.saltarAlPrincipio_Patron + ")");
			Re_detenciones = new PatronRegex(cf.detenciones_Patron);
			
			
			patron_NT_Capitulo_NC=
				grupoNombrado(KEY_NUMERO_TEMPORADA, ConstantesExprecionesRegulares.numeros_4max) +
			ConstantesExprecionesRegulares.separaciones_UnoAlMenos +
				patron_Capitulo_NC;
			Re_NT_Capitulo_NC = new PatronRegex(patron_NT_Capitulo_NC);
			
			patron_NT_Capitulo_N_Union_N_Repetir=
				grupoNombrado(KEY_NUMERO_TEMPORADA, ConstantesExprecionesRegulares.numeros_4max) +
			ConstantesExprecionesRegulares.separaciones_UnoAlMenos +
				patron_Capitulo_N_Union_N_Repetir;
			Re_NT_Capitulo_N_Union_N_Repetir = new PatronRegex(patron_NT_Capitulo_N_Union_N_Repetir);

            patron_NombresCarpetaSubtitulos = cf.nombresCarpetaSubtitulos_patron;
            Re_NombresCarpetaSubtitulos = new PatronRegex(patron_NombresCarpetaSubtitulos);



            patron_CapitulosPluP_NC =
                grupoNombrado(KEY_IDENTIFICADOR_CANTIDAD_CAPITULOS, cf.identificadoresCantidadCapituloTemporada_Prioridad_Patron) +
            ConstantesExprecionesRegulares.separaciones +
            grupoNombrado(KEY_NUMERO_CANTIDAD_CAPITULOS, ConstantesExprecionesRegulares.numeros_4max);
            Re_CapitulosPluP_NC = new PatronRegex(patron_CapitulosPluP_NC);

            patron_Temporada_NT_CapitulosPluP_NC =
                patron_Temporada_NT +
            ConstantesExprecionesRegulares.separaciones +
            patron_CapitulosPluP_NC;
            Re_Temporada_NT_CapitulosPluP_NC = new PatronRegex(patron_Temporada_NT_CapitulosPluP_NC);



            


            patron_NT_Union_NT_Repetir =

                 grupoNombrado(KEY_UNION_TEMPORADA,
                orExpr(
                    grupoNombrado(KEY_NUMERO_TEMPORADA, ConstantesExprecionesRegulares.numeros_2max) +
                    unoAlMenos(ConstantesExprecionesRegulares.espacios +
                    grupoNombrado(KEY_IDENTIFICADOR_CONJUNTO_TEMPORADA, cf.union_Patron) +
                    ConstantesExprecionesRegulares.espacios +
                    grupoNombrado(KEY_NUMERO_TEMPORADA, ConstantesExprecionesRegulares.numeros_2max))
                ,// ----------------------------------
                    grupoNombrado(KEY_NUMERO_TEMPORADA, ConstantesExprecionesRegulares.numeros_2max) +
                    unoAlMenos(ConstantesExprecionesRegulares.espacios +

                    grupoNombrado(KEY_IDENTIFICADOR_CONTINUIDAD_TEMPORADA, cf.continuacion_Patron)
                    + ConstantesExprecionesRegulares.espacios +
                    grupoNombrado(KEY_NUMERO_TEMPORADA, ConstantesExprecionesRegulares.numeros_2max))
                )


            );


            patron_Temporada_Cor_NT_Union_NT_Cor =
                grupoNombrado(KEY_IDENTIFICADOR_TEMPORADA, cf.identificadoresTemporadas_Patron) +

            ConstantesExprecionesRegulares.separaciones +
            ConstantesExprecionesRegulares.envolturaInicial +
            ConstantesExprecionesRegulares.separaciones +
            patron_NT_Union_NT_Repetir +
            ConstantesExprecionesRegulares.separaciones +
            ConstantesExprecionesRegulares.envolturaFinal
            ;

            Re_Temporada_Cor_NT_Union_NT_CorC =new PatronRegex(patron_Temporada_Cor_NT_Union_NT_Cor);

        }




        //acceso a grupos
        public Group getGrupoAleatoriedad(Match m)
		{
			return m.Groups[KEY_ALEATORIEDAD];
		}
		//public Group getGrupoAño(Match m)
		//{
		//	return m.Groups[KEY_NUMERO_AÑO];
		//}
		//public Group getGrupoMes(Match m)
		//{
		//	return m.Groups[KEY_NUMERO_MES];
		//}
		//public Group getGrupoDia(Match m)
		//{
		//	return m.Groups[KEY_NUMERO_DIA];
		//}
		//public Group getGrupoFecha(Match m)
		//{
		//	return m.Groups[KEY_FECHA];
		//}
		//public Group getGrupoSeparacionFecha(Match m)
		//{
		//	return m.Groups[KEY_SEPARACION_FECHA];
		//}
		//public Group getGrupoEnvolturaInicialFecha(Match m)
		//{
		//	return m.Groups[KEY_ENVOLTURA_INICIAL_FECHA];
		//}
		//public Group getGrupoEnvolturaFinalFecha(Match m)
		//{
		//	return m.Groups[KEY_ENVOLTURA_FINAL_FECHA];
		//}
		
		
		
		public Group getGrupoContenidoCapitulo(Match m)
		{
			return m.Groups[KEY_CONTENIDO_CAPITULO];
		}
		public Group getGrupoNumeroCapitulo(Match m)
		{
			return m.Groups[KEY_NUMERO_CAPITULO];
		}
		public Group getGrupoUnion(Match m)
		{
			return m.Groups[KEY_UNION];
		}
		public Group getGrupoConjunto(Match m)
		{
			return m.Groups[KEY_IDENTIFICADOR_CONJUNTO];
		}
		public Group getGrupoContinuidad(Match m)
		{
			return m.Groups[KEY_IDENTIFICADOR_CONTINUIDAD];
		}


        
        public Group getGrupoUnionTemporada(Match m)
        {
            return m.Groups[KEY_UNION_TEMPORADA];
        }
        public Group getGrupoConjuntoTemporada(Match m)
        {
            return m.Groups[KEY_IDENTIFICADOR_CONJUNTO_TEMPORADA];
        }
        public Group getGrupoContinuidadTemporada(Match m)
        {
            return m.Groups[KEY_IDENTIFICADOR_CONTINUIDAD_TEMPORADA];
        }


        public Group getGrupoIdentificadorCapitulo(Match m)
		{
			return m.Groups[KEY_IDENTIFICADOR_CAPITULO];
		}
		public Group getGrupoIdentificadorTemporada(Match m)
		{
			return m.Groups[KEY_IDENTIFICADOR_TEMPORADA];
		}
		public Group getGrupoNumeroTemporada(Match m)
		{
			return m.Groups[KEY_NUMERO_TEMPORADA];
		}
		public Group getGrupoNumeroRomanoTemporada(Match m)
		{
			return m.Groups[KEY_NUMERO_ROMANO_TEMPORADA];
		}
		public Group getGrupoIdentificadorOva(Match m)
		{
			return m.Groups[KEY_IDENTIFICADOR_OVA];
		}
		public Group getGrupoNumeroOva(Match m)
		{
			return m.Groups[KEY_NUMERO_OVA];
		}
		public Group getGrupoIdentificadorCantidadCapitulo(Match m)
		{
			return m.Groups[KEY_IDENTIFICADOR_CANTIDAD_CAPITULOS];
		}
		public Group getGrupoNumeroCantidadCapitulo(Match m)
		{
			return m.Groups[KEY_NUMERO_CANTIDAD_CAPITULOS];
		}
		
		
		
		
		
		
	}
}
