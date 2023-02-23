/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 29/8/2022
 * Hora: 17:57
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
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
using ReneUtiles.Clases.Multimedia.Series;
//using ReneUtiles.Clases.Multimedia.Series.Procesadores.Ignorar;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Conjuntos;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos;
using ReneUtiles.Clases.ExprecionesRegulares;
using ReneUtiles.Clases.ExprecionesRegulares.Fechas;
namespace ReneUtiles.Clases.Multimedia.Series.Procesadores
{
	/// <summary>
	/// Description of RecursosDePatronesDeSeriesGenerales.
	/// </summary>
	public class RecursosDePatronesDeSeriesGenerales:ExprecionesRegularesBasico
	{

        public RecursosDePatronesDeFecha refechas;

		public PatronRegex Re_EtiquetasDeSerie_Principal_Secundarias;
		public PatronRegex Re_EtiquetasDeSerie;
		public PatronRegex Re_SoloPalabrasNormales;
		public RecursosDePatronesDeSeriesGenerales()
		{
            refechas = new RecursosDePatronesDeFecha();
            Re_SoloPalabrasNormales =ConstantesDeDirectorios.getPatronRegex_SoloPalabrasNormales();
			Re_EtiquetasDeSerie_Principal_Secundarias=TipoDeEtiquetaDeSerie.getPatronRegex_PrincipalesYDespues_Tags();
			Re_EtiquetasDeSerie=TipoDeEtiquetaDeSerie.getPatronRegex_Etiquetas();
		}
	}
}
