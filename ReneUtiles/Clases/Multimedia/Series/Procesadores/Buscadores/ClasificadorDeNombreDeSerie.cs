/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 6/8/2022
 * Hora: 11:16
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
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Conjuntos;
//using ReneUtiles.Clases.Multimedia.Series.Procesadores.Ignorar;
using ReneUtiles.Clases.Multimedia.Series.Procesadores;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos;
namespace ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores
{
	/// <summary>
	/// Description of ClasificadorDeNombreDeSerie.
	/// </summary>
	public class ClasificadorDeNombreDeSerie:BuscadorDeDatosEnNombre
	{
		Match mTipoDeNombre; 
		public ClasificadorDeNombreDeSerie(ProcesadorDeNombreDeSerie pr, string nombre)
			: base(pr)
		{
			this.nombre = nombre;
		}
		
		public TipoDeNombreDeSerie? getTipoDeNombreDe()
		{
			String nombreAUsar = this.nombre;
			if (!tieneAlgunNumero(nombreAUsar)) {
				setTipoDeNombre(TipoDeNombreDeSerie.NORMAL);
				return this.tipoDeNombreDeSerie;
			}
			if (esNumero(nombreAUsar)) {
				setTipoDeNombre(TipoDeNombreDeSerie.SOLO_UN_NUMERO);
				
			} else {
				for (int i = 0; i < 5; i++) {
					Match sm = null;
					TipoDeNombreDeSerie? t =null;// TipoDeNombreDeSerie.DESCONOCIDO;
					switch (i) {
						case 0:
							sm = getRe().Re_N_Nombre.ReInicialFinal.Match(nombreAUsar);
							t = TipoDeNombreDeSerie.NUMERO_AL_PRINCIPIO;
							break;
						case 1:
							sm = getRe().Re_N_Multiples_Nombre.ReInicialFinal.Match(nombreAUsar);
							t = TipoDeNombreDeSerie.NUMEROS_MULTIPLES_AL_PRINCIPIO;
							break;
						case 2:
							sm = getRe().Re_Nombre_N.ReInicialFinal.Match(nombreAUsar);
							t = TipoDeNombreDeSerie.NUMERO_AL_FINAL;
							break;
						case 3:
							sm = getRe().Re_Nombre_N_Multiples.ReInicialFinal.Match(nombreAUsar);
							t = TipoDeNombreDeSerie.NUMEROS_MULTIPLES_AL_FINAL;
							break;
						case 4:
							sm = getRe().Re_N_Multiples_N.ReInicialFinal.Match(nombreAUsar);
							t = TipoDeNombreDeSerie.SOLO_NUMEROS_MULTIPLES;
							break;
						case 5:
							sm = getRe().Re_Nombre_N_Nombre.ReInicialFinal.Match(nombreAUsar);
							t = TipoDeNombreDeSerie.NUMERO_INTERNO;
							break;
						
					}
				
					if (sm != null && sm.Success) {
						mTipoDeNombre = sm;
						setTipoDeNombre(t); 
					}
				}
				
				
				MatchCollection mc = Matchs.R_N.Re.Matches(nombreAUsar);
				foreach (Match m in mc) {
					int numero = inT_Cap(m);
					int indiceInicial = m.Index;
					if (esNumeroParteDeNombre(numero: numero
				                         , indiceInicialDelNumero: indiceInicial)) {
						//if (this.tipoDeNombreDeSerie != TipoDeNombreDeSerie.DESCONOCIDO) {
						return this.tipoDeNombreDeSerie;
						//}
					}
				}
				
				
			}//else de no es numero solo
			
			
			
			
			
			
			agregarTipoDeNombreDeSerieAlContexto(this.tipoDeNombreDeSerie, nombreAUsar);
			return this.tipoDeNombreDeSerie;
		}
	}
}
