/*
 * Created by SharpDevelop.
 * User: Rene
 * Date: 25/9/2021
 * Time: 18:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using ReneUtiles.Clases.Multimedia.Relacionadores.Saltos;


namespace ReneUtiles.Clases.Multimedia.Series.Anime
{
	/// <summary>
	/// Description of ConstantesSeriesAnime.
	/// </summary>
	public class ConstantesSeriesAnime
	{
		
		//Manga
		//public static readonly string [] saltarCualquierNumeroDespuesDe_Anime /*saltarAntes*/ = { "Strike Witches_ ", "Magic Kaito "};
		//public static readonly string []saltarCualquierNumeroAntesDe_Anime/*saltarDespues*/ = { " Butai", };
		
		public static readonly CondicionIgnorarNumeroEspecifico[] ignorarNumeroEspecificoDetrasDe=
		{
//			new CondicionIgnorarNumeroEspecifico(0,"Gate ")
//		,new CondicionIgnorarNumeroEspecifico(2,"Life_ Dai-")//Tensei Kenja no Isekai Life_ Dai-2 no Shokugyou wo Ete, Sekai Saikyou ni Narimashita
//		,new CondicionIgnorarNumeroEspecifico(3,"Tenshi no ")
//		,new CondicionIgnorarNumeroEspecifico(8,"man after")
//		,new CondicionIgnorarNumeroEspecifico(9,"Norn")
//		,new CondicionIgnorarNumeroEspecifico(10,"Brave ")
//		,new CondicionIgnorarNumeroEspecifico(17,"Ouji-sama_ U-")//Shin Tennis no Ouji-sama_ U-17 World Cup
//		,new CondicionIgnorarNumeroEspecifico(15,"R-")
//		,new CondicionIgnorarNumeroEspecifico(35,"taimadou-gakuen-")
//		,new CondicionIgnorarNumeroEspecifico(38,"Acacias ")
//		,new CondicionIgnorarNumeroEspecifico(100,"Mob Psycho ")
//		,new CondicionIgnorarNumeroEspecifico(2041,"Night Head ")
		};//, " Butai"
		public static readonly CondicionIgnorarNumeroEspecifico[] ignorarNumeroEspecificoDelanteDe=
		{
//			new CondicionIgnorarNumeroEspecifico(7," Seeds"," Ghost","-Ghost")
//		,new CondicionIgnorarNumeroEspecifico(8," TAGE")
//		,new CondicionIgnorarNumeroEspecifico(11,"eyes")
//		,new CondicionIgnorarNumeroEspecifico(100,"-man","man")
		};
		
		public static readonly string[] detenciones = {"The Animation", "OVA","tv"};
		public static readonly string[] saltarAlPrincipio = {"[Dark Termplar]"};
	
		public static readonly int [] nombresNumericosCompletosSimples={86};
		
		
		
		
		
		
		public static readonly CondicionIgnorarNumeroEspecifico[] nombresConNumeroAlPrincipio=
		{
			new CondicionIgnorarNumeroEspecifico(3,"D","Kanojo","Real","Girl")
				
		,new CondicionIgnorarNumeroEspecifico(7,"Seeds")
		,new CondicionIgnorarNumeroEspecifico(7,"Ghost")
		,new CondicionIgnorarNumeroEspecifico(8,"TAGE")
		,new CondicionIgnorarNumeroEspecifico(11,"eyes")
		,new CondicionIgnorarNumeroEspecifico(100,"-man","man")
				
		};
		public static readonly CondicionIgnorarNumeroEspecifico[] nombresConNumeroAlFinal=
		{
			
		new CondicionIgnorarNumeroEspecifico(0,"Gate")
		//,new CondicionIgnorarNumeroEspecifico(2,"Life""Dai-")//Tensei Kenja no Isekai Life_ Dai-2 no Shokugyou wo Ete, Sekai Saikyou ni Narimashita
		,new CondicionIgnorarNumeroEspecifico(3,"Tenshi","no")
		,new CondicionIgnorarNumeroEspecifico(8,"man","after")
		,new CondicionIgnorarNumeroEspecifico(9,"Norn")
		,new CondicionIgnorarNumeroEspecifico(10,"Brave")
		//,new CondicionIgnorarNumeroEspecifico(17,"Ouji-sama_ U-")//Shin Tennis no Ouji-sama_ U-17 World Cup
		,new CondicionIgnorarNumeroEspecifico(false,15,"R-")
                ,new CondicionIgnorarNumeroEspecifico(false,15,"R")
                ,new CondicionIgnorarNumeroEspecifico(false,15,"R ")
        ,new CondicionIgnorarNumeroEspecifico(35,"taimadou","gakuen")
		,new CondicionIgnorarNumeroEspecifico(100,"Mob","Psycho")
			,new CondicionIgnorarNumeroEspecifico(1412,"Magic","Kaito")
		,new CondicionIgnorarNumeroEspecifico(2041,"Night","Head")
			
		};
		public static readonly CondicionIgnorarNumeroEspecificoRodeadoPor[] nombresConUnNumeroInterno={
			new CondicionIgnorarNumeroEspecificoRodeadoPor(ss("Tensei","Kenja","no","Isekai","Life","Dai"),2,ss("no","Shokugyou","wo","Ete","Sekai","Saikyou","ni","Narimashita"))//Tensei Kenja no Isekai Life_ Dai-2 no Shokugyou wo Ete, Sekai Saikyou ni Narimashita Episodio 4
				,new CondicionIgnorarNumeroEspecificoRodeadoPor(ss("D"),4,ss("DJ"+"First"+"Mix"))//D4DJ First Mix 
				,new CondicionIgnorarNumeroEspecificoRodeadoPor(ss("Shin","Tennis","no","Ouji","sama","U"),17,ss("World","Cup"))
				
		};
		
		public static readonly CondicionIgnorarNumerosEspecificosSeparadosPor[] nombresRodeadosDeNumeros={
			//new CondicionIgnorarNumerosEspecificosSeparadosPor(,false,)//50M2
			
		};
		public static readonly CondicionIgnorarNumerosEspecificos[] nombresConNumerosAlPrincipio={
			new CondicionIgnorarNumerosEspecificos(ints(2,43),"Seiin","Koukou","Danshi","Volley-bu")
		};
		
		public static readonly CondicionIgnorarConjuntoDeNumeros[] nombresNumericosMultiples={
//			new CondicionIgnorarConjuntoDeNumeros()
		};
		
//		public static readonly CondicionIgnorarConjuntoDeNumeros[] nombresNumericosMultiples={
////			new CondicionIgnorarConjuntoDeNumeros(11,22,63)
//		};
		//public static readonly string[] detenciones = {};
		public static readonly string [][] nombresEquivalentes={
			
			
		};
		public ConstantesSeriesAnime()
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
