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
namespace ReneUtiles.Clases.Multimedia.Series.SeriesPersona
{
	/// <summary>
	/// Description of ConstantesSeriesPersona.
	/// </summary>
	public class ConstantesSeriesPersona
	{
		public static readonly CondicionIgnorarNumeroEspecifico[] ignorarNumeroEspecificoDetrasDe=
		{
//			new CondicionIgnorarNumeroEspecifico(38,"Acacias")
//		
		};
		
		public static readonly CondicionIgnorarNumeroEspecifico[] ignorarNumeroEspecificoDelanteDe = {
//			new CondicionIgnorarNumeroEspecifico(2, "años y un día","años un dia","años y un dia")//
//				,new CondicionIgnorarNumeroEspecifico(3, "Caminos","%")//
//		, new CondicionIgnorarNumeroEspecifico(7, "Vidas")//
//		, new CondicionIgnorarNumeroEspecifico(8, "TAGE")
//		, new CondicionIgnorarNumeroEspecifico(9, "1 1 Lone Star")
//		, new CondicionIgnorarNumeroEspecifico(12, "Monkeys")
//		, new CondicionIgnorarNumeroEspecifico(22, "De Julio")
//		, new CondicionIgnorarNumeroEspecifico(30, "Monedas")
//		, new CondicionIgnorarNumeroEspecifico(91, " Alerta Policía")
//		, new CondicionIgnorarNumeroEspecifico(911, "Lone Star")
//		, new CondicionIgnorarNumeroEspecifico(1000, "Maneras")//"maneras"
		};
		
		public static readonly CondicionNoSaltarAlPrincipio[] noSaltarAlPrincipio={
			new CondicionNoSaltarAlPrincipio(new string[]{"the","los"},new string[]{"100","4040"})
		};//poner en minusculas preferiblemente
		
		
		//public static readonly string[] nombresNumericosCompletosMultiples = { "11 22 63","11.22.63" };
		public static readonly int [] nombresNumericosCompletosSimples={1983};
		
		//public static readonly string[] nombresRodeadosDeNumeros = { "50M2","19-2" };
		
		
		
		
		public static readonly CondicionIgnorarNumeroEspecifico[] nombresConNumeroAlPrincipio=
		{
		new CondicionIgnorarNumeroEspecifico(2, "años","y","un","día")//
				,new CondicionIgnorarNumeroEspecifico(3, "Caminos","%")//
			,new CondicionIgnorarNumeroEspecifico(3,"%")//
		, new CondicionIgnorarNumeroEspecifico(7, "Vidas")//
		, new CondicionIgnorarNumeroEspecifico(8, "TAGE")
		
		, new CondicionIgnorarNumeroEspecifico(12, "Monkeys")
                , new CondicionIgnorarNumeroEspecifico(13, "Reasons","Why")
        , new CondicionIgnorarNumeroEspecifico(22, "De","Julio")
		, new CondicionIgnorarNumeroEspecifico(30, "Monedas")
		, new CondicionIgnorarNumeroEspecifico(91, "Alerta","Policía")
		, new CondicionIgnorarNumeroEspecifico(911, "Lone","Star")
		, new CondicionIgnorarNumeroEspecifico(1000, "Maneras","de","morir")
		};
		public static readonly CondicionIgnorarNumeroEspecifico[] nombresConNumeroAlFinal=
		{
			new CondicionIgnorarNumeroEspecifico(2,"Code")
			,new CondicionIgnorarNumeroEspecifico(38,"Acacias")
		
			,new CondicionIgnorarNumeroEspecifico(100,"Los")
			,new CondicionIgnorarNumeroEspecifico(100,"The")
				,new CondicionIgnorarNumeroEspecifico(404,"Code")
			,new CondicionIgnorarNumeroEspecifico(4400,"the")
//			new CondicionIgnorarNumeroEspecifico(100,"Los ","The ","the.","The.")
//			,new CondicionIgnorarNumeroEspecifico(4400,"the.","the ","The ")
		};
		public static readonly CondicionIgnorarNumeroEspecificoRodeadoPor[] nombresConUnNumeroInterno={
			new CondicionIgnorarNumeroEspecificoRodeadoPor(false,ss("DI"),4,ss("RIES"))//DI4RIES - 1x01
		};
		
		public static readonly CondicionIgnorarNumerosEspecificosSeparadosPor[] nombresRodeadosDeNumeros={
			new CondicionIgnorarNumerosEspecificosSeparadosPor(50,2,false,"M")//50M2
			,new CondicionIgnorarNumerosEspecificosSeparadosPor(19,2,false,"-")//19-2
		};
		public static readonly CondicionIgnorarNumerosEspecificos[] nombresConNumerosAlPrincipio={
			new CondicionIgnorarNumerosEspecificos(ints(9,1,1),"Lone","Star")
		};
		
		public static readonly CondicionIgnorarConjuntoDeNumeros[] nombresNumericosMultiples={
			new CondicionIgnorarConjuntoDeNumeros(11,22,63)
		};
		public static readonly string [][] nombresEquivalentes={
			new string[]{"Arcane","Arcane League of Legends"}
			,new string[]{"Caballero Luna","Moon Knight"}
			
		};
		public ConstantesSeriesPersona()
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
