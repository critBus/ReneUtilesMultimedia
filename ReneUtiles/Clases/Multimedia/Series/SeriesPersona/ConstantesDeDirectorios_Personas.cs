/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 08/01/2022
 * Hora: 13:58
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using ReneUtiles.Clases.Multimedia.Series;
namespace ReneUtiles.Clases.Multimedia.Series.SeriesPersona
{
	/// <summary>
	/// Description of ConstantesDeDirectorios_Personas.
	/// </summary>
	public class ConstantesDeDirectorios_Personas
	{
		
		
		
		public static string[] ConjuntosDeSeries={
			"Series"
				,"Miniseries"
				
		};
		public static string[] CapitulosSueltos={
			"Series [TX]"
		,"Series [TX] [Clásicas]"
		,"Series [TX] [Dobladas al Español]"
		,"Series [TX] [HD Dual Audio]"
		,"Series [TX] [Subtituladas]"
		
		};
		
		//Expesificas
		public static string[] Agrupamiento_ConCapitulosSueltos={
			
		};
		public static string[] Agrupamiento={//suele contener mas carpetas
			"Series"
		};
		public static string[] Agrupamiento_Finalizadas={
			"Finalizadas"
		};
		public static string[] Agrupamiento_TX={//suele contener mas carpetas
			"En Transmision"
		};
		
		public static string[] Finalizadas={
			"Series Finalizadas [x Temporadas]"
				,"Series Finalizadas [x Temporadas] [Estrenos]"
				,"Series [Temporadas Finalizadas] [Estrenos]"
		};
		public static string[] Finalizadas_Dobladas={
			"Series Finalizadas [x Temporadas] [ESPAÑOL]"
				,"Series [Temporadas Finalizadas] [Dobladas]"
				,"Series [Temporadas Finalizadas] [Dobladas] [HD]"
				
		};
		public static string[] Finalizadas_HD={
			"Series Finalizadas [x Temporadas] [HD]"
				,"Series [Temporadas Finalizadas] [Estrenos] [HD]"
		};
		public static string[] Finalizadas_HD_Dual_Audio={
			"Series Finalizadas [x Temporadas] [HD Dual Audio]"
				,"Series [Temporadas Finalizadas] [HD Dual Audio]"
		};
		public static string[] Finalizadas_Subtituladas={
			"Series Finalizadas [x Temporadas] [Subtituladas]"
		};
		public static string[] Finalizadas_Clasicas={
			"Series [Temporadas Finalizadas] [Clasicas]"	
		};
		public static string[] TX={
			"Series [TX] [ Estreno]"
				,"Series [TX]"
		};
		public static string[] TX_HD={
			"Series [TX][HD]"
		};
		public static string[] TX_HD_Dual_Audio={
			"Series [TX] [ Estreno] [HD Dual Audio]"
				,"Series [En Transmision] [HD Dual Audio]"
				,"Series [TX] [HD Dual Audio]"
		};
		public static string[] TX_Clasicas={
			"Series [TX] [ Clasicas]"
		};
		public static string[] TX_Españolas={
			"Series [TX] [ Estreno][Españolas]"
		};
		public static string[] TX_Subtituladas={
			"Series [TX] [ Estreno][Subtituladas]"
				,"Series [TX] [Subtituladas]"
		};
		public static string[] TX_Dobladas={
			"Series [TX] [Dobladas al Español]"
				,"Series [En Transmision] [Dobladas]"
				,"Series [TX] [Dobladas]"
				
		};
		
		public static string[] Otros={
			
		};
		
		
		public ConstantesDeDirectorios_Personas()
		{
		}
		
		public static string[][] sm(params string[][] a){
			return a;
		}
		public static string[] ss(params string[] a){
			return a;
		}
	}
}
