/*
 * Created by SharpDevelop.
 * User: Rene
 * Date: 25/9/2021
 * Time: 18:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using ReneUtiles.Clases.Multimedia.Relacionadores.Saltos;
namespace ReneUtiles.Clases.Multimedia.Series
{
	/// <summary>
	/// Description of ConstantesSeries.
	/// </summary>
	public class ConstantesSeries
	{
		//General
		public static readonly string  separadorDeNombresDeSerieEquivalentes="===";
		public static readonly string [] ignorarEstaPalabraAlPrincipio={"New", "Parte","version", "Extreno ", "Emision", "copy",  "fin de semana ", "Part"};
		
		public static readonly string [] saltarCualquierNumeroDespuesDe /*saltarAntes*/= {"parte ",   "mp", "New", "H","Parte", "version",   "Extreno", "Emision", "copy", "v", "Part","....","Xvid","falta","falta el","pend","pendiente"};
		public static readonly string []saltarCualquierNumeroAntesDe/*saltarDespues*/= {"p", "Mb",  "fps", "kbit", "bit", "gb", ",", "kb"};
		//public static readonly string []    union = {" - ", " -", "- ", "-", " y ", " y", "y ", "y", ","};
		public static readonly string []    union = {"-","y"};
		public static readonly string []    continuacion = { ",","&"};
		public static readonly string UNION_PREDETERMINADA = "-";
		
		//public static readonly string[] identificadoresCantidadCapituloTemporada= {"capitulos", "caps", "cap", "episodios", "chapters"}
		
		
		
		public static readonly CondicionIgnorarNumeroEspecifico[] ignorarNumeroEspecificoDetrasDe=
		{
		 new CondicionIgnorarNumeroEspecifico(1,".amzn.web-d")
		,new CondicionIgnorarNumeroEspecifico(2,".dd+")
		,new CondicionIgnorarNumeroEspecifico(69,"-ajp")
//		,new CondicionIgnorarNumeroEspecifico(100,"Los ","The ","the.","The.")
		,new CondicionIgnorarNumeroEspecifico(264,".x",".h.")
//		,new CondicionIgnorarNumeroEspecifico(4400,"the.","the ","The ")
		};
		public static readonly CondicionIgnorarNumeroEspecifico[] ignorarNumeroEspecificoDelanteDe=
		{}; 
		public static readonly CondicionIgnorarNumeroEspecificoRodeadoPor[] condicionesIgnorarNumeroEspecificoRodeadoPor={
			new CondicionIgnorarNumeroEspecificoRodeadoPor(false,ss("vi"),2,ss("eo"))
		};
		
		//"Extreno ", "Emision ",
		public static readonly string[] detenciones = {"episodio", "capitulo", "temp", "fin de semana", "(", "[", "{", "copy",  "Extreno", "Emision", "parte", "version", "The Animation",  "www.", " FDT", " ESP", " US", ".US", " tv", "(TV)", "-tv", " SUB","1080p","720p","Dual Audio","Miniserie","Spanish","....",".hdtv.xvid-notv.[VTV]","vi2eo","SUPER MARIO","Miniserie","FDT"};
		public static readonly string[] identificadoresTemporadas = {"temporada", "temp", "tem", "season", "period","Miniserie"};
		public static readonly string[] identificadoresCapitulo = {"capitulo", "cap", "capi", "chapter", "episodio", "epi", "epis", "episo", "episod","CODEap"};//CODE 2 - [Temp 1] [CODEap-04] [MP4] [349,49 Mb]
        public static readonly string[] identificadoresCantidadCapituloTemporada_Prioridad = { "capitulos", "caps", "cap.", "episodios", "chapters" };
        public static readonly string[] identificadoresCantidadCapituloTemporada = {"capitulos", "caps", "cap.", "cap", "episodios", "chapters"};
		public static readonly string[] detencionesAbsolutas = {">!="};
		 
		public static readonly string[] saltarAlPrincipio = {"The", "DC's ", "DCs", "Marvels ", "M", "Marvel ", "Marvels", "Marvel's ", "Marvel's","El","Los"};
		public static readonly CondicionNoSaltarAlPrincipio[] noSaltarAlPrincipio={};
		
		public static readonly string separadores_SinRodear=" \t\n\r\f-☆!?;+\'~.*,:/";
		
		public static readonly string[] saltarHastaDespuesDe = {".com)"};//".com"
//		public static readonly string[] patrones_Saltar_Hasta_Despues_De={
//			Cons
//		};
		
		
		
		
		
		public static readonly int [] nombresNumericosCompletosSimples={};//911
		public static readonly string [] nombresNumericosCompletosMultiples={};//"9 1 1"
		
		//public static readonly string[] nombresRodeadosDeNumeros = {  };//los mas fijos(pequeños) sin separaciones Internas//"50M2","19-2"
		
		
		public static readonly CondicionIgnorarNumeroEspecifico[] nombresConNumeroAlPrincipio=
		{};
		public static readonly CondicionIgnorarNumeroEspecifico[] nombresConNumeroAlFinal=
		{
//			new CondicionIgnorarNumeroEspecifico(100,"Los ","The ","the.","The.")
//			,new CondicionIgnorarNumeroEspecifico(4400,"the.","the ","The ")
		};
		public static readonly CondicionIgnorarNumeroEspecificoRodeadoPor[] nombresConUnNumeroInterno={
			
		};
		public static readonly CondicionIgnorarNumerosEspecificosSeparadosPor[] nombresRodeadosDeNumeros={
			
		};
		public static readonly CondicionIgnorarNumerosEspecificos[] nombresConNumerosAlPrincipio={
			
		};
		public static readonly CondicionIgnorarConjuntoDeNumeros[] nombresNumericosMultiples={
//			new CondicionIgnorarConjuntoDeNumeros(11,22,63)
		};
		
		public static readonly string [][] nombresEquivalentes={
			
			
		};


        public static string[] nombresCarpetaSubtitulos = { "Subtitulos", "sub", "Subtitle" };

        public ConstantesSeries()
		{
		}
		
		private static int[] ints(params int[] N){
			return N;
		}
		private static string[] ss(params string[] N){
			return N;
		}
	}
}
