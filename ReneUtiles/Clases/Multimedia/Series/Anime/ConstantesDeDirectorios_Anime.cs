/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 08/01/2022
 * Hora: 13:57
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace ReneUtiles.Clases.Multimedia.Series.Anime
{
	/// <summary>
	/// Description of ConstantesDeDirectorios_Anime.
	/// </summary>
	public class ConstantesDeDirectorios_Anime
	{
		
		
		//los animes puede aparecer dentro de la carpeta de animados
		//(generalmente cuando no estan afuera)
		public static string[] ConjuntosDeSeries={
			
				"Series Anime [Finalizadas]"
		};
		public static string[] CapitulosSueltos={
			
		
		"Anime Online [Transmision]"};
		
		
		//Expesificas
		public static string[] Agrupamiento_ConCapitulosSueltos={
			"Animados Mangas [2021]"
				,"Animados Mangas [2020]"
		};
		public static string[] Agrupamiento={//suele contener mas carpetas
			
		};
		public static string[] Agrupamiento_Finalizadas={
			
		};
		public static string[] Agrupamiento_TX={//suele contener mas carpetas
			
		};
		
		public static string[] Finalizadas={//puede estar en elexterior del paquete
			"Series Finalizadas [x Temporadas] [Mangas]"
				,"Series Anime [Finalizadas]"
				,"!! Series Mangas Finalizadas [x Temporadas]"
				,"Series Mangas Finalizadas [x Temporadas]"
		};
		
		
		public static string[] Finalizadas_Dobladas={
			
		};
		public static string[] Finalizadas_HD={
			
		};
		public static string[] Finalizadas_HD_Dual_Audio={
			"Series Anime [Finalizadas] [HD Dual Audio]"
				
		};
		public static string[] Finalizadas_Subtituladas={
			
		};
		public static string[] Finalizadas_Clasicas={
			
		};
		
		public static string[] TX={
			"Anime Online [Transmision]"
				,"Mangas [Transmision]"
		};
		public static string[] TX_HD={
			
		};
		public static string[] TX_HD_Dual_Audio={
			
		};
		public static string[] TX_Clasicas={
			"!! Series Clasicas [x Capitulos]"
				,"Series Anime [Clasicas]"
		};
		public static string[] TX_Españolas={
			
		};
		public static string[] TX_Subtituladas={
			
		};
		public static string[] TX_Dobladas={
			
		};
		
		
		
		
		
		public static string[] Otros={
			"!! Peliculas Mangas Clasicas"
				,"Peliculas Anime [Clasicas]"
				,"Peliculas [Mangas]"
				,"! ! Mejores Cuartos [Otaku]"
				,"!!  Calendario de Mangas Noticias ESTRENOS-ANIMEPRIMAVERA 2019"
				,"!! Canciones de Series Mangas"
				,"!! Creaciones Otakus Cubanos [By Pedro-animes]"
				,"!! Manga y Ovas"
				,"!! Troleo Otaku"
				,"!! Wallpaper Mangas"
				,"Curiosidades de Filmes"
				,"Expo [Cosplay][Anime]"
				,"Historia de los Comic"
				,"Trailers (2020)"
		};
		public ConstantesDeDirectorios_Anime()
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
