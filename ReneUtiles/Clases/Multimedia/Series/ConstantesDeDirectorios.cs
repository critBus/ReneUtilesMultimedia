/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 1/8/2022
 * Hora: 11:11
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
using ReneUtiles.Clases.Multimedia.Series.Procesadores;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos; 

namespace ReneUtiles.Clases.Multimedia.Series
{
	/// <summary>
	/// Description of ConstantesDeDirectorios.
	/// </summary>
	public class ConstantesDeDirectorios
	{
		public static readonly string[] palabrasNormales={
			"faltaron","que","pendientes","fin","ficcion","drama","terror","susp","suspensos","suspenso","muñes","ciencia","c","aventuras","accion","convertir","1080p","4k","720p","Latinas","latinos","musica","Online","show","transmision","actores","actualizacion","actualizada","actualizados","al","android","animados","anime","antivirus","anuncios","aplicaciones","avi","cap","clasicas","clasicos","clasificados","clip","comics","con","cristiana","cuba","de","del","deporte","discografia","dobladas","documental","documentales","doramas","en","español","española","españolas","estreno","estrenos","ex","exclusiva","festival","filmes","finalizadas","fps","games","gamesplays","genero","graficas","hd","historietas","humor","ingles","interesantes","ios","juegos","juventud","kav","la","latina","linux","mac","mangas","mar","miniseries","mkv","moviles","mp3","mp4","musicales","nod32","novelas","organizadas","pais","para","pc","peliculas","phone","por","premios","programas","reality","revistas","seccion","seg","semana","semanal","series","shows","sitios","softwares","sub","subtituladas","subtitulados","subtitulo","subtitulos","sueltos","temporada","temporadas","trailers","tutoriales","tv","tx","variados","videos","videos2brain","wallpapers","window","windows","x","y"};
		public static string[] ConjuntosDeSeries={
			
		};
		public static string[] CapitulosSueltos={
			
		
		};
		
		//Expesificas
		public static string[] Agrpamiento={};
		
		public static string[] Finalizadas={
		
		};
		
		public static string[] TX={};
		
		
		public ConstantesDeDirectorios()
		{
		}
		
		public static PatronRegex getPatronRegex_SoloPalabrasNormales(){
			string m = "";
			m+="(?:";
			m+=ConstantesExprecionesRegulares.separaciones;
			m+="(?:";
			for (int i = 0; i < palabrasNormales.Length; i++) {
				if (i != 0) {
					m += "|";
				}
				m+=Matchs.getTextoArreglado(palabrasNormales[i]);
			}
			m+=")";
			//m+=ConstantesExprecionesRegulares.ElSiguienteNoEsLetraNiNumero;
			m+=ConstantesExprecionesRegulares.separaciones;
			m+=")+";
			return new PatronRegex(m);
			
			
		}
	}
}
