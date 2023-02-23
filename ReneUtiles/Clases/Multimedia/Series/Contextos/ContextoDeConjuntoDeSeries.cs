/*
 * Created by SharpDevelop.
 * User: Rene
 * Date: 29/9/2021
 * Time: 17:17
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.IO;

using ReneUtiles;
using ReneUtiles.Clases;
using ReneUtiles.Clases.Multimedia;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos;
//using
namespace ReneUtiles.Clases.Multimedia.Series.Contextos
{
	/// <summary>
	/// Description of ContextoDeConjuntoDeSeries.
	/// </summary>
	public class ContextoDeConjuntoDeSeries:ConsolaBasica
	{
		
		
		public enum CaracteristicaCapitulos
		{
			SON_SOLO_NUMEROS,
			//hasta ahora los capitulos solo continen la informacion referente al numero del capitulo (y tal ves la temporada)
			NOMBRES_DE_SERIE_ES_RODEADOS_DE_NUMEROS,
			NOMBRES_DE_SERIE_ES_NUMERO_AL_PRINCIPIO,
			//
			NOMBRES_DE_SERIE_ES_UN_NUMERO_INTERNO,
			NOMBRES_DE_SERIE_ES_NUMEROS_MULTIPLES_AL_FINAL,
			NOMBRES_DE_SERIE_ES_NUMEROS_MULTIPLES_AL_PRINCIPIO,
			NOMBRES_DE_SERIE_ES_NUMERO_AL_FINAL,
			NOMBRES_DE_SERIE_ES_NUMERO_SIMPLE,
			// todos los nombres analizados son de un numero (el nombre de la serie es un numero)
			NOMBRES_DE_SERIE_ES_NUMERO_MULTIPLE,
			// todos los nombres analizados son de multiples numeros (el nombre de la serie es de multiples numeros)
			NOMBRES_DE_SERIE_ES_NORMAL,
			//todos los nombres analizados son normales
			TODOS_PERTENECEN_A_UNA_MISMA_SERIE_SEGURO,
			//solo poner si se save de antemano
			HASTA_AHORA_TODOS_PERTENECEN_A_UNA_MISMA_SERIE,
			DEBERIAN_DE_PERTENECER_A_UNA_MISMA_SERIE,
			//para cuando uno entra a una carpeta (o sel lee un txt con nombres de capitulos(que esta seguro de esto)) y se cree que todos pertenecen a la misma serie, pero bueno no es totalmente seguro
			SE_DESCONOCE,
			HAY_NOMBRES_RODEADOS_DE_NUMEROS,
			
			HAY_NOMBRES_NUMERO_AL_PRINCIPIO,
			//
			HAY_NOMBRES_UN_NUMERO_INTERNO,
			HAY_NOMBRES_NUMEROS_MULTIPLES_AL_FINAL,
			HAY_NOMBRES_NUMEROS_MULTIPLES_AL_PRINCIPIO,
			HAY_NOMBRES_NUMERO_AL_FINAL,
			HAY_NOMBRES_NUMERO_SIMPLE,
			
			HAY_NOMBRES_NUMERICOS_MULTIPLES,
			//HAY_NOMBRES_NUMERICOS_SIMPLES,
			HAY_NOMBRES_NORMALES,
			HAY_SOLO_NUMEROS,
            //HAY_CONTENEDORES_TEMPORADAS,
            HAY_CONTENEDORES_CAPITULOS_MISMA_TEMPORADA,
            HAY_CONTENEDORES_CAPITULOS_MISMA_SERIE,
            SON_SOLO_NOMBRES_DE_SERIES,
			NOMBRES_SIN_MOVER_NUMERO_ADELANTE_SEGURO
		}
		
		public class RelacionTipoCaracteristica_Serie
		{
			
			
			public TipoDeNombreDeSerie tipoDeSerie;
			public CaracteristicaCapitulos caracteristicaEs;
			public CaracteristicaCapitulos caracteristicaHay;
			private Dictionary<KeySerie,HashSet<string>> encontrados;
			private Dictionary<KeySerie,HashSet<string>> alQueDeverianDePertenecer;
			public RelacionTipoCaracteristica_Serie(
				TipoDeNombreDeSerie tipoDeSerie,
				CaracteristicaCapitulos caracteristicaEs,
				CaracteristicaCapitulos caracteristicaHay
				
			)
			{
				this.tipoDeSerie = tipoDeSerie;
				this.caracteristicaEs = caracteristicaEs;
				this.caracteristicaHay = caracteristicaHay;
				this.encontrados = KeySerie.getNewDictionary_KeySerie<HashSet<string>>();
				this.alQueDeverianDePertenecer = KeySerie.getNewDictionary_KeySerie<HashSet<string>>();
			}
			private void addA(Dictionary<KeySerie,HashSet<string>> d, KeySerie k, string url)
			{
				if (d.ContainsKey(k)) {
					HashSet<string> hs = d[k];
					hs.Add(url);
				} else {
					HashSet<string> hs = new HashSet<string>();
					hs.Add(url);
					d.Add(k, hs);
				}
			}
			private bool contieneNombre_En(Dictionary<KeySerie,HashSet<string>> d, string nombre)
			{
				foreach (KeySerie k in d.Keys) {
					if (k.Nombre == nombre) {
						return true;
					}
				}
				return false;
			}
			private bool contieneClave_En(Dictionary<KeySerie,HashSet<string>> d, string clave)
			{
				foreach (KeySerie k in d.Keys) {
					if (k.Clave == clave) {
						return true;
					}
				}
				return false;
			}
			
			private bool algunNombreComienzaCon(Dictionary<KeySerie,HashSet<string>> d, string nombre)
			{
				foreach (KeySerie k in d.Keys) {
					if (k.Nombre.StartsWith(nombre)) {
						return true;
					}
				}
				return false;
			
			}
			public bool algunNombreComienzaCon_EnEncontrados(string nombre)
			{
				return algunNombreComienzaCon(this.encontrados, nombre);
			}
			public bool algunNombreComienzaCon_AlQueDeverianDePertenecer(string nombre)
			{
				return algunNombreComienzaCon(this.alQueDeverianDePertenecer, nombre);
			}
			
			public bool contieneNombre_EnEncontrados(string nombre)
			{
				return contieneNombre_En(this.encontrados, nombre);
			}
			public bool contieneNombre_AlQueDeverianDePertenecer(string clave)
			{
				return contieneNombre_En(this.alQueDeverianDePertenecer, clave);
			}
			public bool contieneClave_EnEncontrados(string nombre)
			{
				return contieneClave_En(this.encontrados, nombre);
			}
			public bool contieneClave_AlQueDeverianDePertenecer(string clave)
			{
				return contieneClave_En(this.alQueDeverianDePertenecer, clave);
			}
			public void addEncontrado(KeySerie k, string url)
			{
				addA(this.encontrados, k, url);
			}
			public void addAlQueDeverianDePertenecer(KeySerie k, string url)
			{
				addA(this.alQueDeverianDePertenecer, k, url);
			}
			
			
			//			public bool contieneNombre(string nombre){
			//				return algunNombreComienzaCon_EnEncontrados
			//			}
		}
		
		
		
		public bool EsConjuntoDeNombres;
		//{ get; set; }
		
		public string[] ConjuntoDeNombres;
		//{ get; set; }
		public bool HayConjuntoDeNombres;
		//{get{return this.ConjuntoDeNombres!=null&&this.ConjuntoDeNombres.Length>0;}}
		
		
		public bool EsElPrimeroEnSerAnalizado{ get; set; }
		
		private HashSet<CaracteristicaCapitulos> caracteristicaDeLosCapitulosAnalizados;
		
		public   RelacionTipoCaracteristica_Serie[] Relaciones;
		
		public ContextoDeConjuntoDeSeries()
		{
			reset();
			
		}
		public void reset()
		{
			add_caracteristicaDeLosCapitulosAnalizados(CaracteristicaCapitulos.SE_DESCONOCE);
			this.EsElPrimeroEnSerAnalizado = true;
			//hayDireccion=false;
			//HayConjuntoDeNombres=false;
			this.EsConjuntoDeNombres = false;
			
			Relaciones = new  RelacionTipoCaracteristica_Serie[] {
				new RelacionTipoCaracteristica_Serie(TipoDeNombreDeSerie.NORMAL
				                                     , CaracteristicaCapitulos.NOMBRES_DE_SERIE_ES_NORMAL
				                                     , CaracteristicaCapitulos.HAY_NOMBRES_NORMALES)
				, new RelacionTipoCaracteristica_Serie(TipoDeNombreDeSerie.SOLO_NUMEROS_MULTIPLES
				                                     , CaracteristicaCapitulos.NOMBRES_DE_SERIE_ES_NUMERO_MULTIPLE
				                                     , CaracteristicaCapitulos.HAY_NOMBRES_NUMERICOS_MULTIPLES)
				, new RelacionTipoCaracteristica_Serie(TipoDeNombreDeSerie.NUMERO_AL_FINAL
				                                     , CaracteristicaCapitulos.NOMBRES_DE_SERIE_ES_NUMERO_AL_FINAL
				                                     , CaracteristicaCapitulos.HAY_NOMBRES_NUMERO_AL_FINAL)
				, new RelacionTipoCaracteristica_Serie(TipoDeNombreDeSerie.NUMERO_AL_PRINCIPIO
				                                     , CaracteristicaCapitulos.NOMBRES_DE_SERIE_ES_NUMERO_AL_PRINCIPIO
				                                     , CaracteristicaCapitulos.HAY_NOMBRES_NUMERO_AL_PRINCIPIO)
				, new RelacionTipoCaracteristica_Serie(TipoDeNombreDeSerie.SOLO_UN_NUMERO
				                                     , CaracteristicaCapitulos.NOMBRES_DE_SERIE_ES_NUMERO_SIMPLE
				                                     , CaracteristicaCapitulos.HAY_NOMBRES_NUMERO_SIMPLE)
				, new RelacionTipoCaracteristica_Serie(TipoDeNombreDeSerie.NUMEROS_MULTIPLES_AL_FINAL
				                                     , CaracteristicaCapitulos.NOMBRES_DE_SERIE_ES_NUMEROS_MULTIPLES_AL_FINAL
				                                     , CaracteristicaCapitulos.HAY_NOMBRES_NUMEROS_MULTIPLES_AL_FINAL)
				, new RelacionTipoCaracteristica_Serie(TipoDeNombreDeSerie.NUMEROS_MULTIPLES_AL_PRINCIPIO
				                                     , CaracteristicaCapitulos.NOMBRES_DE_SERIE_ES_NUMEROS_MULTIPLES_AL_PRINCIPIO
				                                     , CaracteristicaCapitulos.HAY_NOMBRES_NUMEROS_MULTIPLES_AL_PRINCIPIO)
					, new RelacionTipoCaracteristica_Serie(TipoDeNombreDeSerie.RODEADO_DE_NUMEROS
				                                     , CaracteristicaCapitulos.NOMBRES_DE_SERIE_ES_RODEADOS_DE_NUMEROS
				                                     , CaracteristicaCapitulos.HAY_NOMBRES_RODEADOS_DE_NUMEROS)
					, new RelacionTipoCaracteristica_Serie(TipoDeNombreDeSerie.NUMERO_INTERNO
				                                     , CaracteristicaCapitulos.NOMBRES_DE_SERIE_ES_UN_NUMERO_INTERNO
				                                     , CaracteristicaCapitulos.HAY_NOMBRES_UN_NUMERO_INTERNO)
			};
			
//			this.Nombres_NumericosMultiples_Encontrados = new Dictionary<string,HashSet<string>>();
//			this.Nombres_NumericosSimples_Encontrados = new Dictionary<int,HashSet<string>>();
//			this.Nombres_RodeadosDeNumeros_Encontrados = new Dictionary<string,HashSet<string>>();
//			this.Nombres_NumeroAlPrincipio_Encontrados = new Dictionary<string,HashSet<string>>();
//			this.Nombres_NumeroAlFinal_Encontrados = new Dictionary<string,HashSet<string>>();
//			this.Nombres_NumerosMultiplesAlFinal_Encontrados = new Dictionary<string,HashSet<string>>();
//			this.Nombres_NumerosMultiplesAlPrincipio_Encontrados = new Dictionary<string,HashSet<string>>();
//			this.Nombres_UnNumeroInterno_Encontrados = new Dictionary<string,HashSet<string>>();
			
//			KeysEncontrados = KeySerie.getNewHashSet_KeySerie();
//			KeysAlQuedeberianPertenecer = KeySerie.getNewHashSet_KeySerie();
		
			
//			this.NombreDeSerie = new HashSet<string>();
//			this.MarcasDeSerie = new HashSet<string>();
//			this.NombresClaveAlQuedeberianPertenecer = new HashSet<string>();
//			this.NombresAlQuedeberianPertenecer = new HashSet<string>();
//			
			this.caracteristicaDeLosCapitulosAnalizados = new HashSet<CaracteristicaCapitulos>();
			this.ConjuntoDeNombres = new string[0];
//			//--------------
//			this.caracteristicaDeLosCapitulosAnalizados = null;
//			this.ConjuntoDeNombres = null;
//			this.NombreDeSerie = null;
//			this.MarcasDeSerie = null;
		}
		
		private RelacionTipoCaracteristica_Serie getRT(TipoDeNombreDeSerie? t)
		{
			if (t != null) {
				foreach (RelacionTipoCaracteristica_Serie rc in this.Relaciones) {
					if (rc.tipoDeSerie == t) {
						return rc;
					}
				}
			}
			return null;
		}
		
		public void addEsDeTipo(TipoDeNombreDeSerie? t)
		{
			
			RelacionTipoCaracteristica_Serie rc = getRT(t);
			
			if (rc != null) {
				add_caracteristicaDeLosCapitulosAnalizados(rc.caracteristicaEs);
			}

		}
		
		public void addHayDeTipo(TipoDeNombreDeSerie? t)
		{
			RelacionTipoCaracteristica_Serie rc = getRT(t);
			
			if (rc != null) {
				add_caracteristicaDeLosCapitulosAnalizados(rc.caracteristicaHay);
			}
	
		}
		
		
		public CaracteristicaCapitulos[] getCaracteristicasDeTipoEsNombreDeSerie()
		{
			List<CaracteristicaCapitulos> lc = new List<CaracteristicaCapitulos>();
			foreach (RelacionTipoCaracteristica_Serie rc in this.Relaciones) {
				lc.Add(rc.caracteristicaEs);
			}
			return lc.ToArray();

		}
		public void addKeySerieEncotrado(KeySerie k, string url)
		{
			RelacionTipoCaracteristica_Serie rc = getRT(k.TipoDeSerie);
			if (rc != null) {
				rc.addEncontrado(k, url);
			}
			addHayDeTipo(k.TipoDeSerie);
		}
		public void addKeySerieAlQueDeberianDePertencer(KeySerie k, string url)
		{
			RelacionTipoCaracteristica_Serie rc = getRT(k.TipoDeSerie);
			if (rc != null) {
				rc.addAlQueDeverianDePertenecer(k, url);
			}
			addHayDeTipo(k.TipoDeSerie);
		}
		
		
		
		public bool contieneNombre(ContextoDeSerie ctx, string nombre)
		{
			if (contieneNombre_ConjuntoDeNombres(ctx, nombre, () => {
			})) {
				return true;
			}
			foreach (RelacionTipoCaracteristica_Serie rc in this.Relaciones) {
				if (rc.contieneNombre_EnEncontrados(nombre)
				    && rc.contieneNombre_AlQueDeverianDePertenecer(nombre)) {
					return true;
				}
			}
			return false;
		}
		
		public bool contieneNombre_EnEncontrados(string nombre)
		{
			foreach (RelacionTipoCaracteristica_Serie rc in this.Relaciones) {
				if (rc.contieneNombre_EnEncontrados(nombre)) {
					return true;
				}
			}
			return false;

		}
		
		public bool contieneNombre_EnAlQueDeberianDePertencer(string nombre)
		{
			foreach (RelacionTipoCaracteristica_Serie rc in this.Relaciones) {
				if (rc.contieneNombre_AlQueDeverianDePertenecer(nombre)) {
					return true;
				}
			}
			return false;
		}
		
		
		
		
		public bool contieneClave_EnEncontrados(string clave)
		{
			foreach (RelacionTipoCaracteristica_Serie rc in this.Relaciones) {
				if (rc.contieneClave_EnEncontrados(clave)) {
					return true;
				}
			}
			return false;

		}
		
		public bool contieneClave_EnAlQueDeberianDePertencer(string clave)
		{
			foreach (RelacionTipoCaracteristica_Serie rc in this.Relaciones) {
				if (rc.contieneClave_AlQueDeverianDePertenecer(clave)) {
					return true;
				}
			}
			return false;
		}
		
		
		public bool algunNombreComienzaCon_EnEncontrados(string nombre)
		{
			foreach (RelacionTipoCaracteristica_Serie rc in this.Relaciones) {
				if (rc.algunNombreComienzaCon_EnEncontrados(nombre)) {
					return true;
				}
			}
			return false;
		}
		public bool algunNombreComienzaCon_AlQueDeverianDePertenecer(string nombre)
		{
			foreach (RelacionTipoCaracteristica_Serie rc in this.Relaciones) {
				if (rc.algunNombreComienzaCon_AlQueDeverianDePertenecer(nombre)) {
					return true;
				}
			}
			return false;
		}
		
		
		
		public void add_caracteristicaDeLosCapitulosAnalizados(params CaracteristicaCapitulos[] C)
		{
			if (this.caracteristicaDeLosCapitulosAnalizados == null) {
				this.caracteristicaDeLosCapitulosAnalizados = new HashSet<CaracteristicaCapitulos>();
			}
			this.caracteristicaDeLosCapitulosAnalizados.Concat(C);
//			int end = C.Length;
//			for (int i = 0; i < end; i++) {
//				if (!containsAll_caracteristicaDeLosCapitulosAnalizados(C[i])) {
//					caracteristicaDeLosCapitulosAnalizados.Add(C[i]);	
//				}
//				
//			}
		}
		public bool containsAll_caracteristicaDeLosCapitulosAnalizados(params CaracteristicaCapitulos[] C)
		{
			if (caracteristicaDeLosCapitulosAnalizados == null || caracteristicaDeLosCapitulosAnalizados.Count == 0) {//isEmpty(caracteristicaDeLosCapitulosAnalizados)
				return false;
			}
			int end = C.Length;
			for (int i = 0; i < end; i++) {
				if (!caracteristicaDeLosCapitulosAnalizados.Contains(C[i])) {
					return false;
				}
			}
			
			return true;
		}
		//si no es uno de los de C da false
		//si contine a alguien que no sea de los C da false (osea si solo contiene un elemento, pero es de ls C es true)
		//public bool containsORexc_caracteristicaDeLosCapitulosAnalizados(params CaracteristicaCapitulos[] C)
		public bool contieneSoloAEstos_caracteristicaDeLosCapitulosAnalizados(params CaracteristicaCapitulos[] C)
		{
			if (caracteristicaDeLosCapitulosAnalizados == null || caracteristicaDeLosCapitulosAnalizados.Count == 0) {//isEmpty(caracteristicaDeLosCapitulosAnalizados)
				return false;
			}
			//int lengCaracteristicaDeLosCapitulosAnalizados = caracteristicaDeLosCapitulosAnalizados.Count;
			for (int k = 0; k < caracteristicaDeLosCapitulosAnalizados.Count; k++) {
				//int end = C.Length;
				bool saltoConinue = false;
				for (int i = 0; i < C.Length; i++) {
					if (caracteristicaDeLosCapitulosAnalizados.ElementAt(k) == C[i]) {
						saltoConinue = true;
						break;
					}
				}
				if (saltoConinue) {
					continue;
				}
				return false;
			}
			
			
			return true;
		}
		public bool containsOR_caracteristicaDeLosCapitulosAnalizados(params CaracteristicaCapitulos[] C)
		{
			if (caracteristicaDeLosCapitulosAnalizados == null || caracteristicaDeLosCapitulosAnalizados.Count == 0) {//isEmpty(caracteristicaDeLosCapitulosAnalizados)
				return false;
			}
			int end = C.Length;
			for (int i = 0; i < end; i++) {
				if (caracteristicaDeLosCapitulosAnalizados.Contains(C[i])) {
					return true;
				}
			}
			
			return false;
		}
		
		public void remove_caracteristicaDeLosCapitulosAnalizados(params CaracteristicaCapitulos[] C)
		{
			if (caracteristicaDeLosCapitulosAnalizados == null || caracteristicaDeLosCapitulosAnalizados.Count == 0) {//isEmpty(caracteristicaDeLosCapitulosAnalizados)
				return;
			}
			int end = C.Length;
			for (int i = 0; i < end; i++) {
				caracteristicaDeLosCapitulosAnalizados.Remove(C[i]);
			}
			
		}
		public void remove_SE_DESCONOCE()
		{
			remove_caracteristicaDeLosCapitulosAnalizados(CaracteristicaCapitulos.SE_DESCONOCE);
		
		}
		
		public bool SE_DESCONOCE()
		{
			return containsAll_caracteristicaDeLosCapitulosAnalizados(
				CaracteristicaCapitulos.SE_DESCONOCE);
		}
		public bool SON_SOLO_NOMBRES_DE_SERIES()
		{
			return containsAll_caracteristicaDeLosCapitulosAnalizados(
				CaracteristicaCapitulos.SON_SOLO_NOMBRES_DE_SERIES);
		}
		
		public bool NOMBRES_SIN_MOVER_NUMERO_ADELANTE_SEGURO()
		{
			return containsAll_caracteristicaDeLosCapitulosAnalizados(
				CaracteristicaCapitulos.NOMBRES_SIN_MOVER_NUMERO_ADELANTE_SEGURO);
		}
		
		public bool DEBERIAN_DE_PERTENECER_A_UNA_MISMA_SERIE()
		{
			return containsAll_caracteristicaDeLosCapitulosAnalizados(
				CaracteristicaCapitulos.DEBERIAN_DE_PERTENECER_A_UNA_MISMA_SERIE);
		}
		public bool HASTA_AHORA_TODOS_PERTENECEN_A_UNA_MISMA_SERIE()
		{
			return containsAll_caracteristicaDeLosCapitulosAnalizados(
				CaracteristicaCapitulos.HASTA_AHORA_TODOS_PERTENECEN_A_UNA_MISMA_SERIE);
		
		}
		
		
		public bool TODOS_PERTENECEN_A_UNA_MISMA_SERIE_SEGURO()
		{
			return containsAll_caracteristicaDeLosCapitulosAnalizados(
				CaracteristicaCapitulos.TODOS_PERTENECEN_A_UNA_MISMA_SERIE_SEGURO);
		
		}

        //HAY_CONTENEDORES_CAPITULOS_MISMA_SERIE
        public void agregarPropiedadesAContextoHAY_CONTENEDORES_CAPITULOS_MISMA_TEMPORADA()
        {
            add_caracteristicaDeLosCapitulosAnalizados(
                ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.HAY_CONTENEDORES_CAPITULOS_MISMA_TEMPORADA);
        }
        public void agregarPropiedadesAContextoHAY_CONTENEDORES_CAPITULOS_MISMA_SERIE()
        {
            add_caracteristicaDeLosCapitulosAnalizados(
                ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.HAY_CONTENEDORES_CAPITULOS_MISMA_SERIE);
        }
        //public void agregarPropiedadesAContextoHAY_CONTENEDORES_TEMPORADAS()
        //{
        //	add_caracteristicaDeLosCapitulosAnalizados(
        //		ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.HAY_CONTENEDORES_TEMPORADAS);
        //}
        public void agregarPropiedadesAContextoHAY_SOLO_NUMEROS()
		{
			add_caracteristicaDeLosCapitulosAnalizados(
				ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.HAY_SOLO_NUMEROS);
			//add_O_Remove_SON_SOLO_NUMEROS();
		}
		public void agregarPropiedadesAContextoBasicasDeResultado()
		{
			this.EsElPrimeroEnSerAnalizado = false;
			remove_SE_DESCONOCE();
		}
		public void agregarPropiedadesAContexto_Encontrado(KeySerie k, string url)
		{
//			RelacionTipoCaracteristica_Serie rc=getRT(k.TipoDeSerie);
//			if(rc!=null){
			agregarPropiedadesAContextoBasicasDeResultado();
			addKeySerieEncotrado(k, url);
			//addHayDeTipo(k.TipoDeSerie);
//			}
		}
	
		
		public bool contieneNombre_ConjuntoDeNombres(ContextoDeSerie contexto, string nombreDeEstaSerie, metodoRealizar accionHayNombresSimilares)
				//public bool BusquedaEnConjuntoDeNombres(ContextoDeSerie contexto, string nombreDeEstaSerie, metodoRealizar accionHayNombresSimilares)
		{
			int lengConjuntoDeNombres = -1;
			if (this.HayConjuntoDeNombres) {
				int cantidadQueComienzanIgual = 0;
				lengConjuntoDeNombres = this.ConjuntoDeNombres.Length;
				for (int i = 0; i < lengConjuntoDeNombres; i++) {
					if (i != contexto.IndiceConjuntoDeNombres) {
						if (this.ConjuntoDeNombres[i].StartsWith(nombreDeEstaSerie)) {
							cantidadQueComienzanIgual++;
							if (cantidadQueComienzanIgual == 2) {
								break;
							}
						}
					}
				}// fin del for
				if (cantidadQueComienzanIgual >= 2) {
					accionHayNombresSimilares();
					return true;
				}
		
			}
			return false;
		}
		//
		//
		//		public bool busquedaConjuntoDeNombresPorNumeroSimple(ContextoDeSerie contexto, int numeroDeSerie)
		//		{
		//			return BusquedaEnConjuntoDeNombres(contexto, numeroDeSerie.ToString(), () => {
		//				agregarPropiedadesAContextoNumeroSimple(numeroDeSerie, contexto.Url);
		//			});
		//		}
		//
	}
}

//		public void agregarPropiedadesAContextoNumeroSimple(int numeroDeSerie, string url)
//		{
//			agregarPropiedadesAContextoBasicasDeResultado();
//			add_caracteristicaDeLosCapitulosAnalizados(
//				ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.HAY_NOMBRES_NUMERO_SIMPLE
//				//ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.HAY_NOMBRES_NUMERICOS_SIMPLES
//			);
//			addNombres_NumericosSimples_Encontrados(numeroDeSerie, url);
//			//addNombreNumericoSimpleEncontrado(numeroDeSerie);
//		}
//		public void agregarPropiedadesAContextoNumeroMultiple(string nombreDeEstaSerie, string url)
//		{
//			agregarPropiedadesAContextoBasicasDeResultado();
//			add_caracteristicaDeLosCapitulosAnalizados(ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.HAY_NOMBRES_NUMERICOS_MULTIPLES);
//			addNombres_NumericosMultiples_Encontrados(nombreDeEstaSerie, url);
//			//addNombreNumericoMultipleEncontrado(nombreDeEstaSerie);
//		}
//		public void agregarPropiedadesAContextoHAY_NOMBRES_NORMALES()
//		{
//			agregarPropiedadesAContextoBasicasDeResultado();
//			add_caracteristicaDeLosCapitulosAnalizados(
//				ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.HAY_NOMBRES_NORMALES);
//			//add_O_Remove_SON_SOLO_NUMEROS();
//		}
//
//		public void agregarPropiedadesAContextoNombreRodeadoDeNumeros(string nombreDeEstaSerie, string url)
//		{
//			agregarPropiedadesAContextoBasicasDeResultado();
//			add_caracteristicaDeLosCapitulosAnalizados(ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.HAY_NOMBRES_RODEADOS_DE_NUMEROS);
//			//addNombreRodeadoDeNumerosEncontrado(nombreDeEstaSerie);
//			addNombres_RodeadosDeNumeros_Encontrados(nombreDeEstaSerie, url);
//		}
//
//
//		public void agregarPropiedadesAContextoNombreNumeroInicial(string nombreDeEstaSerie, string url)
//		{
//			agregarPropiedadesAContextoBasicasDeResultado();
//			add_caracteristicaDeLosCapitulosAnalizados(
//				ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.HAY_NOMBRES_NUMERO_AL_PRINCIPIO
//			);
//			addNombres_NumeroAlPrincipio_Encontrados(nombreDeEstaSerie, url);
//		}
//		public void agregarPropiedadesAContextoNombreNumeroFinal(string nombreDeEstaSerie, string url)
//		{
//			agregarPropiedadesAContextoBasicasDeResultado();
//			add_caracteristicaDeLosCapitulosAnalizados(
//				ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.HAY_NOMBRES_NUMERO_AL_FINAL
//			);
//			addNombres_NumeroAlFinal_Encontrados(nombreDeEstaSerie, url);
//		}
//		public void agregarPropiedadesAContextoNombreNumerosMultiplesInicial(string nombreDeEstaSerie, string url)
//		{
//			agregarPropiedadesAContextoBasicasDeResultado();
//			add_caracteristicaDeLosCapitulosAnalizados(
//				ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.HAY_NOMBRES_NUMEROS_MULTIPLES_AL_PRINCIPIO
//			);
//			addNombres_NumerosMultiplesAlPrincipio_Encontrados(nombreDeEstaSerie, url);
//		}
//		public void agregarPropiedadesAContextoNombreNumerosMultiplesFinal(string nombreDeEstaSerie, string url)
//		{
//			agregarPropiedadesAContextoBasicasDeResultado();
//			add_caracteristicaDeLosCapitulosAnalizados(
//				ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.HAY_NOMBRES_NUMEROS_MULTIPLES_AL_FINAL
//			);
//			addNombres_NumerosMultiplesAlFinal_Encontrados(nombreDeEstaSerie, url);
//		}
//		public void agregarPropiedadesAContextoNombreUnNumeroInterno(string nombreDeEstaSerie, string url)
//		{
//			agregarPropiedadesAContextoBasicasDeResultado();
//			add_caracteristicaDeLosCapitulosAnalizados(
//				ContextoDeConjuntoDeSeries.CaracteristicaCapitulos.HAY_NOMBRES_UN_NUMERO_INTERNO
//			);
//			addNombres_UnNumeroInterno_Encontrados(nombreDeEstaSerie, url);
//		}
//

//		public bool EsElPrimeroEnSerAnalizado{
//			get{return this.EsElPrimeroEnSerAnalizado;}
//			set{this.EsElPrimeroEnSerAnalizado=value;}
//		}
		
//		public bool seConoceElNombreDeLaSerie()
//		{
//			//return NombreDeSerie != null && NombreDeSerie.Count > 0;
//			return this.NombresAlQuedeberianPertenecer != null && this.NombresAlQuedeberianPertenecer.Count > 0;
//		}
		
		
//		public bool esArchivo()
//		{
//			return Archivos.esArchivo(Url);
//		}
//		public bool esCarpeta()
//		{
//			return Archivos.esCarpeta(Url);
//		}
//
//		public bool hayDireccion {
//			get{ return !isEmptyFull(Url); }
//		}
		
//		public bool algun_nombreDeSerie_StartsWith(string nombre)
//		{
//			foreach (string n in NombreDeSerie) {
//				if (n.StartsWith(nombre)) {
//					return true;
//				}
//			}
//			return false;
//		}


//			if (t != null) {
//				CaracteristicaCapitulos? c = null;
				
//				switch (t) {
//					case TipoDeNombreDeSerie.NUMERO_AL_FINAL:
//						c = CaracteristicaCapitulos.NOMBRES_DE_SERIE_ES_NUMERO_AL_FINAL;
//						break;
//					case TipoDeNombreDeSerie.NUMERO_AL_PRINCIPIO:
//						c = CaracteristicaCapitulos.NOMBRES_DE_SERIE_ES_NUMERO_AL_PRINCIPIO;
//						break;
//					case TipoDeNombreDeSerie.NUMERO_INTERNO:
//						c = CaracteristicaCapitulos.NOMBRES_DE_SERIE_ES_UN_NUMERO_INTERNO;
//						break;
//					case TipoDeNombreDeSerie.NUMEROS_MULTIPLES_AL_FINAL:
//						c = CaracteristicaCapitulos.NOMBRES_DE_SERIE_ES_NUMEROS_MULTIPLES_AL_FINAL;
//						break;
//					case TipoDeNombreDeSerie.NUMEROS_MULTIPLES_AL_PRINCIPIO:
//						c = CaracteristicaCapitulos.NOMBRES_DE_SERIE_ES_NUMEROS_MULTIPLES_AL_PRINCIPIO;
//						break;
//					case TipoDeNombreDeSerie.RODEADO_DE_NUMEROS:
//						c = CaracteristicaCapitulos.NOMBRES_DE_SERIE_ES_RODEADOS_DE_NUMEROS;
//						break;
//					case TipoDeNombreDeSerie.SOLO_NUMEROS_MULTIPLES:
//						c = CaracteristicaCapitulos.NOMBRES_DE_SERIE_ES_NUMERO_MULTIPLE;
//						break;
//					case TipoDeNombreDeSerie.SOLO_UN_NUMERO:
//						c = CaracteristicaCapitulos.NOMBRES_DE_SERIE_ES_NUMERO_SIMPLE;
//						break;
//				}
			
			
//			}
			
		
//			if (t != null) {
//				CaracteristicaCapitulos? c = null;
//				switch (t) {
//					case TipoDeNombreDeSerie.NUMERO_AL_FINAL:
//						c = CaracteristicaCapitulos.HAY_NOMBRES_NUMERO_AL_FINAL;
//						break;
//					case TipoDeNombreDeSerie.NUMERO_AL_PRINCIPIO:
//						c = CaracteristicaCapitulos.HAY_NOMBRES_NUMERO_AL_PRINCIPIO;
//						break;
//					case TipoDeNombreDeSerie.NUMERO_INTERNO:
//						c = CaracteristicaCapitulos.HAY_NOMBRES_UN_NUMERO_INTERNO;
//						break;
//					case TipoDeNombreDeSerie.NUMEROS_MULTIPLES_AL_FINAL:
//						c = CaracteristicaCapitulos.HAY_NOMBRES_NUMEROS_MULTIPLES_AL_FINAL;
//						break;
//					case TipoDeNombreDeSerie.NUMEROS_MULTIPLES_AL_PRINCIPIO:
//						c = CaracteristicaCapitulos.HAY_NOMBRES_NUMEROS_MULTIPLES_AL_PRINCIPIO;
//						break;
//					case TipoDeNombreDeSerie.RODEADO_DE_NUMEROS:
//						c = CaracteristicaCapitulos.HAY_NOMBRES_RODEADOS_DE_NUMEROS;
//						break;
//					case TipoDeNombreDeSerie.SOLO_NUMEROS_MULTIPLES:
//						c = CaracteristicaCapitulos.HAY_NOMBRES_NUMERICOS_MULTIPLES;
//						break;
//					case TipoDeNombreDeSerie.SOLO_UN_NUMERO:
//						c = CaracteristicaCapitulos.HAY_NOMBRES_NUMERO_SIMPLE;
//						break;
//				}
//				if (c != null) {
//					add_caracteristicaDeLosCapitulosAnalizados((CaracteristicaCapitulos)c);
//				}
//			}
//			return new CaracteristicaCapitulos[] {
//				CaracteristicaCapitulos.NOMBRES_DE_SERIE_ES_RODEADOS_DE_NUMEROS,
//				CaracteristicaCapitulos.NOMBRES_DE_SERIE_ES_NUMERO_AL_PRINCIPIO,
//				//
//				CaracteristicaCapitulos.NOMBRES_DE_SERIE_ES_UN_NUMERO_INTERNO,
//				CaracteristicaCapitulos.NOMBRES_DE_SERIE_ES_NUMEROS_MULTIPLES_AL_FINAL,
//				CaracteristicaCapitulos.NOMBRES_DE_SERIE_ES_NUMEROS_MULTIPLES_AL_PRINCIPIO,
//				CaracteristicaCapitulos.NOMBRES_DE_SERIE_ES_NUMERO_AL_FINAL,
//				CaracteristicaCapitulos.NOMBRES_DE_SERIE_ES_NUMERO_SIMPLE,
//				// todos los nombres analizados son de un numero (el nombre de la serie es un numero)
//				CaracteristicaCapitulos.NOMBRES_DE_SERIE_ES_NUMERO_MULTIPLE,
//				// todos los nombres analizados son de multiples numeros (el nombre de la serie es de multiples numeros)
//				CaracteristicaCapitulos.NOMBRES_DE_SERIE_ES_NORMAL
//			};
//		public void add_O_Remove_SON_SOLO_NUMEROS()
//		{
//			add_caracteristicaDeLosCapitulosAnalizados(CaracteristicaCapitulos.HAY_SOLO_NUMEROS);
//			if (containsORexc_caracteristicaDeLosCapitulosAnalizados(CaracteristicaCapitulos.SON_SOLO_NUMEROS
//				                                                     , CaracteristicaCapitulos.TODOS_PERTENECEN_A_UNA_MISMA_SERIE_SEGURO
//				                                                     , CaracteristicaCapitulos.DEBERIAN_DE_PERTENECER_A_UNA_MISMA_SERIE
//				                                                     , CaracteristicaCapitulos.SE_DESCONOCE
//				                                                  	 , CaracteristicaCapitulos.HAY_SOLO_NUMEROS
//				                                                  	 , CaracteristicaCapitulos.SON_SOLO_NOMBRES_DE_SERIES
//				                                                  	 , CaracteristicaCapitulos.NOMBRES_SIN_MOVER_NUMERO_ADELANTE_SEGURO
//			    )) {
//				add_caracteristicaDeLosCapitulosAnalizados(CaracteristicaCapitulos.SON_SOLO_NUMEROS);
//			} else {
//				remove_caracteristicaDeLosCapitulosAnalizados(CaracteristicaCapitulos.SON_SOLO_NUMEROS);
//			}
//		}
//

//		public void addKeySerieEncotrado_List(HashSet<KeySerie> hk){
//			foreach (KeySerie k in hk) {
//				addKeySerieEncotrado(k);
//			}
//		}
		
		
//public string Url{ get; set; }
		
//		private HashSet<KeySerie> KeysEncontrados;
//
//		private HashSet<KeySerie> KeysAlQuedeberianPertenecer;
		
//		private HashSet<string> NombreDeSerie;
//		//{ get; set; }
//		private HashSet<string> MarcasDeSerie;
//{ get; set; }
//		public HashSet<string> NombresClaveAlQuedeberianPertenecer;
//		public HashSet<string> NombresAlQuedeberianPertenecer;
//{ get; set; }
		
		
//		private Dictionary<string,HashSet<string>> Nombres_NumericosMultiples_Encontrados;
//
//		private Dictionary<int,HashSet<string>> Nombres_NumericosSimples_Encontrados;
//
//		private Dictionary<string,HashSet<string>> Nombres_RodeadosDeNumeros_Encontrados;
//
//
//		private Dictionary<string,HashSet<string>> Nombres_NumeroAlPrincipio_Encontrados;
//		private Dictionary<string,HashSet<string>> Nombres_NumeroAlFinal_Encontrados;
//		private Dictionary<string,HashSet<string>> Nombres_NumerosMultiplesAlFinal_Encontrados;
//		private Dictionary<string,HashSet<string>> Nombres_NumerosMultiplesAlPrincipio_Encontrados;
//		private Dictionary<string,HashSet<string>> Nombres_UnNumeroInterno_Encontrados;
		
		
//		public void addKeySerieAlQueDeberianDePertencer_List(HashSet<KeySerie> hk){
//			foreach (KeySerie k in hk) {
//				addKeySerieAlQueDeberianDePertencer(k);
//			}
//		}
//		public void addNombreYClave_List(HashSet<string> nombre, HashSet<string> clave)
//		{
//			this.NombreDeSerie.Concat(nombre);
//			this.MarcasDeSerie.Concat(clave);
//		}
//		public void addNombreYClave(string nombre, string clave)
//		{
//			this.NombreDeSerie.Add(nombre);
//			this.MarcasDeSerie.Add(clave);
//		}
		
//		public void addClaveALaQueDeberianDePertenecer(string clave)
//		{
//			this.NombresClaveAlQuedeberianPertenecer.Add(clave);
//		}
//		public void addClaveALaQueDeberianDePertenecer_List(HashSet<string> clave)
//		{
//			this.NombresClaveAlQuedeberianPertenecer.Concat(clave);
//		}
		
//		public void addNombres_NumericosMultiples_Encontrados(string nombre, string url)
//		{
//			if (this.Nombres_NumericosMultiples_Encontrados.ContainsKey(nombre)) {
//				this.Nombres_NumericosMultiples_Encontrados[nombre].Add(url);
//			} else {
//				HashSet<string> urls = new HashSet<string>();
//				urls.Add(url);
//				this.Nombres_NumericosMultiples_Encontrados.Add(nombre, urls);
//			}
//		}
//
//
//		public void addNombres_NumericosSimples_Encontrados(int nombre, string url)
//		{
//			if (this.Nombres_NumericosSimples_Encontrados.ContainsKey(nombre)) {
//				this.Nombres_NumericosSimples_Encontrados[nombre].Add(url);
//			} else {
//				HashSet<string> urls = new HashSet<string>();
//				urls.Add(url);
//				this.Nombres_NumericosSimples_Encontrados.Add(nombre, urls);
//			}
//		}
//
//
//		public void addNombres_RodeadosDeNumeros_Encontrados(string nombre, string url)
//		{
//			if (this.Nombres_RodeadosDeNumeros_Encontrados.ContainsKey(nombre)) {
//				this.Nombres_RodeadosDeNumeros_Encontrados[nombre].Add(url);
//			} else {
//				HashSet<string> urls = new HashSet<string>();
//				urls.Add(url);
//				this.Nombres_RodeadosDeNumeros_Encontrados.Add(nombre, urls);
//			}
//		}
//
//
//		public void addNombres_NumeroAlPrincipio_Encontrados(string nombre, string url)
//		{
//			if (this.Nombres_NumeroAlPrincipio_Encontrados.ContainsKey(nombre)) {
//				this.Nombres_NumeroAlPrincipio_Encontrados[nombre].Add(url);
//			} else {
//				HashSet<string> urls = new HashSet<string>();
//				urls.Add(url);
//				this.Nombres_NumeroAlPrincipio_Encontrados.Add(nombre, urls);
//			}
//		}
//
//
//		public void addNombres_NumeroAlFinal_Encontrados(string nombre, string url)
//		{
//			if (this.Nombres_NumeroAlFinal_Encontrados.ContainsKey(nombre)) {
//				this.Nombres_NumeroAlFinal_Encontrados[nombre].Add(url);
//			} else {
//				HashSet<string> urls = new HashSet<string>();
//				urls.Add(url);
//				this.Nombres_NumeroAlFinal_Encontrados.Add(nombre, urls);
//			}
//		}
//
//
//		public void addNombres_NumerosMultiplesAlFinal_Encontrados(string nombre, string url)
//		{
//			if (this.Nombres_NumerosMultiplesAlFinal_Encontrados.ContainsKey(nombre)) {
//				this.Nombres_NumerosMultiplesAlFinal_Encontrados[nombre].Add(url);
//			} else {
//				HashSet<string> urls = new HashSet<string>();
//				urls.Add(url);
//				this.Nombres_NumerosMultiplesAlFinal_Encontrados.Add(nombre, urls);
//			}
//		}
//
//
//		public void addNombres_NumerosMultiplesAlPrincipio_Encontrados(string nombre, string url)
//		{
//			if (this.Nombres_NumerosMultiplesAlPrincipio_Encontrados.ContainsKey(nombre)) {
//				this.Nombres_NumerosMultiplesAlPrincipio_Encontrados[nombre].Add(url);
//			} else {
//				HashSet<string> urls = new HashSet<string>();
//				urls.Add(url);
//				this.Nombres_NumerosMultiplesAlPrincipio_Encontrados.Add(nombre, urls);
//			}
//		}
//
//
//		public void addNombres_UnNumeroInterno_Encontrados(string nombre, string url)
//		{
//			if (this.Nombres_UnNumeroInterno_Encontrados.ContainsKey(nombre)) {
//				this.Nombres_UnNumeroInterno_Encontrados[nombre].Add(url);
//			} else {
//				HashSet<string> urls = new HashSet<string>();
//				urls.Add(url);
//				this.Nombres_UnNumeroInterno_Encontrados.Add(nombre, urls);
//			}
//		}
					
		
		
//		public bool contieneNombres_NumericosMultiples_Encontrados(string nombre)
//		{
//			return this.Nombres_NumericosMultiples_Encontrados.ContainsKey(nombre);
//
//		}
//
//
//		public bool contieneNombres_NumericosSimples_Encontrados(int nombre)
//		{
//			return this.Nombres_NumericosSimples_Encontrados.ContainsKey(nombre);
//
//		}
//
//
//		public bool contieneNombres_RodeadosDeNumeros_Encontrados(string nombre)
//		{
//			return this.Nombres_RodeadosDeNumeros_Encontrados.ContainsKey(nombre);
//
//		}
//
//
//		public bool contieneNombres_NumeroAlPrincipio_Encontrados(string nombre)
//		{
//			return this.Nombres_NumeroAlPrincipio_Encontrados.ContainsKey(nombre);
//
//		}
//
//
//		public bool contieneNombres_NumeroAlFinal_Encontrados(string nombre)
//		{
//			return this.Nombres_NumeroAlFinal_Encontrados.ContainsKey(nombre);
//
//		}
//
//
//		public bool contieneNombres_NumerosMultiplesAlFinal_Encontrados(string nombre)
//		{
//			return this.Nombres_NumerosMultiplesAlFinal_Encontrados.ContainsKey(nombre);
//
//		}
//
//
//		public bool contieneNombres_NumerosMultiplesAlPrincipio_Encontrados(string nombre)
//		{
//			return this.Nombres_NumerosMultiplesAlPrincipio_Encontrados.ContainsKey(nombre);
//
//		}
//
//
//		public bool contieneNombres_UnNumeroInterno_Encontrados(string nombre)
//		{
//			return this.Nombres_UnNumeroInterno_Encontrados.ContainsKey(nombre);
//
//		}
		
		
		
//		public bool contieneNombreDeSerie(string nombre)
//		{
//			return this.NombreDeSerie.Contains(nombre);
////			foreach (string n in NombreDeSerie) {
////				if (n == nombre) {
////					return true;
////				}
////			}
////			return false;
//		}
		