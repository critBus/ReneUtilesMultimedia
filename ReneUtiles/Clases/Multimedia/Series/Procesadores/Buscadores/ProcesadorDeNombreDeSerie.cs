/*
 * Creado por SharpDevelop.
 * Usuario: re.Rene
 * Fecha: 24/7/2022
 * Hora: 18:58
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
#pragma warning disable CS0105 // The using directive for 'ReneUtiles.Clases.Multimedia' appeared previously in this namespace
using ReneUtiles.Clases.Multimedia;
#pragma warning restore CS0105 // The using directive for 'ReneUtiles.Clases.Multimedia' appeared previously in this namespace
#pragma warning disable CS0105 // The using directive for 'ReneUtiles.Clases.Multimedia.Series' appeared previously in this namespace
using ReneUtiles.Clases.Multimedia.Series;
#pragma warning restore CS0105 // The using directive for 'ReneUtiles.Clases.Multimedia.Series' appeared previously in this namespace
//using ReneUtiles.Clases.Multimedia.Series.Procesadores.Ignorar;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Conjuntos;
using ReneUtiles.Clases.Multimedia.Series.Procesadores;
using ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores.Datos;
//using System.IO;
using Delimon.Win32.IO;
using ReneUtiles.Clases.ExprecionesRegulares;
using ReneUtiles.Clases.ExprecionesRegulares.IdentificacionesNumericas;

namespace ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores
{
	public class DatosNombreNumerico
		{
			public DatosDeNombreCapituloDelFinal D{ get; set; } 
			//public int Indice{ get; set; }
			public bool loEs;
			public string respuesta;
		}
	/// <summary>
	/// Description of ProcesadorDeSerie.
	/// </summary>
	public class ProcesadorDeNombreDeSerie:ExprecionesRegularesBasico
	{
			
		//colectivo
		public ContextoDeConjuntoDeSeries contextoDeConjunto;
		//{ get; set; }
		
		//public ConfiguracionDeSeries cf;//{ get; set; }
			
		public RecursosDePatronesDeSeries re;
			
			
		//Individual
		public ContextoDeSerie contexto;
		//{ get; set; }
			
			
		//privado
		public HistoriarDeBusqueda historialDeBusqueda;
			
			
			
		public string nombre;
		public DatosDeNombreSerie dn;
			
			
		
			
		
			
		
		
	
		public ProcesadorDeNombreDeSerie(ContextoDeConjuntoDeSeries contextoDeConjunto
		                        , RecursosDePatronesDeSeries re
		                       , ContextoDeSerie contexto
		                      , string nombre
		                     ,ProcesadorDeSeries pro=null)
		{
			this.re = re;
			this.contextoDeConjunto = contextoDeConjunto;
			this.contexto = contexto;
			//this.cf = cf;
			this.nombre = nombre;
			
			this.historialDeBusqueda = new HistoriarDeBusqueda(this,pro);
			//this.re=new re.RecursosDePatronesDeSeries(this);
			
			
			adaptarNombre();
			
			
		}
		
		private void adaptarNombre()
		{
			
			DatosDeNombreSerie dn = new DatosDeNombreSerie();
			dn.NombreOriginal = this.nombre;
   //         if (this.nombre=="Fuufu Ijou, Koibito Miman. Episodio 11") {
   //             cwl("aqui");
   //         }
			//if (contexto.hayDireccion) {
			//	dn.NombreOriginal = new DirectoryInfo(dn.NombreOriginal).Name;
			//}
			
			string nombreAdaptado = dn.NombreOriginal;
			//if (contexto.EsVideo || contexto.EsArchivo) {
			//	nombreAdaptado = Archivos.getNombre(new FileInfo(nombreAdaptado));
			//}
			nombreAdaptado = Utiles.arreglarPalabra(nombreAdaptado);
			
			
			this.nombre = nombreAdaptado;
//			if (contexto.IndiceExtencion != -1) {
//				//cwl("recorto");
//				this.nombre = subs(nombre, 0, contexto.IndiceExtencion);
//				//
//				//cwl("nombre3="+nombre);
//			}
			this.dn = dn;
		}
		
		
		public  DatosDeNombreSerie crearDatosDeNombre(bool detenerSiEncuentraPatronesAlFinal = true)//,ProcesadorDeSeries pro=null
		{
			//this.historialDeBusqueda.buscarFechas(0);
			realizarBusquedasBasicas(0);
			return this.historialDeBusqueda.crearDatosDeNombre(detenerSiEncuentraPatronesAlFinal);
		}
		
		public string crearClave(string nombreDeSerie){
			return this.historialDeBusqueda.crearClave(nombreDeSerie);
		}
		public DatosDeNombreCapituloDelFinal buscarPatronesAlFinal(int I0)
		{
			//this.historialDeBusqueda.buscarFechas(I0);
			realizarBusquedasBasicas(I0);
			return this.historialDeBusqueda.buscarPatronesAlFinal(I0);
		}
		
		public  DatosDeNombreCapituloDelPrincipio getCapitulosDeNombreDelPrincipio(bool detenerSiEncuentraPatronesAlFinal = true)
		{
			//this.historialDeBusqueda.buscarFechas(0);
			realizarBusquedasBasicas(0);
			return this.historialDeBusqueda.getCapitulosDeNombreDelPrincipio(detenerSiEncuentraPatronesAlFinal);
		}
		public  DatosDeNombreCapituloDelFinal getCapitulosDeNombreDelFinal(int I0)
		{
			//this.historialDeBusqueda.buscarFechas(I0);
			realizarBusquedasBasicas(I0);
			return this.historialDeBusqueda.getCapitulosDeNombreDelFinal(I0);
		}
		public  DatosDeNombreCapitulo getCapitulosDeNombre()
		{
			DatosDeNombreCapituloDelPrincipio dp = getCapitulosDeNombreDelPrincipio();
			if (dp != null) {
				if (dp.DatosDelFinal != null) {
					return dp.DatosDelFinal;
				}
				return dp;
			}
			
			return getCapitulosDeNombreDelFinal(0);
		}
		public  DatosNumericosDeNombreDeSerie getDatosNumericosDeNombre()
		{
			DatosNumericosDeNombreDeSerie dns = new DatosNumericosDeNombreDeSerie();
			//dns.fechasEnNombre = this.historialDeBusqueda.buscarFechas(0);
			realizarBusquedasBasicas(0);
			dns.datosDelPrincipio = getCapitulosDeNombreDelPrincipio(false);
			if (dns.datosDelPrincipio != null) {
				if (dns.datosDelPrincipio.DatosDelFinal != null) {
					dns.datosDelFinal = dns.datosDelPrincipio.DatosDelFinal;
					return dns;
				}
				
			}
			dns.datosDelFinal = getCapitulosDeNombreDelFinal(0);
			return dns;
		}
		
		private void realizarBusquedasBasicas(int I0){
			buscarFechas(I0);
			buscarAleatoriedades(I0);
			buscarEtiquetas(I0);
		}
		public List<DatosDeFechaEnNombre> buscarFechas(int I0)
		{
			return this.historialDeBusqueda.buscarFechas(I0);
		}
		public Dictionary<TipoDeEtiquetaDeSerie,List<DatosDeEtiquetaEnNombre>> buscarEtiquetas(int I0)
		{
			return this.historialDeBusqueda.buscarEtiquetas(I0);
		}
		
		public List<DatosDeAleatoriedadEnNombre> buscarAleatoriedades(int I0)
		{
			return this.historialDeBusqueda.buscarAleatoriedades(I0);
		}
		public TipoDeNombreDeSerie? getTipoDeNombreDe(string nombreDeSerie){
			realizarBusquedasBasicas(0);
			return this.historialDeBusqueda.getTipoDeNombreDe(nombreDeSerie);
		}
		
		public TipoDeNombreDeSerie? getTipoDeNombreDe(ProcesadorDeSeries pro,string nombreDeSerie){
			return pro.getTipoDeNombreDe(this,nombreDeSerie);
		}
		
		public string getNombreRecortadoSinEnvoltorios(int IFinal){
			return this.historialDeBusqueda.getNombreRecortadoSinEnvoltorios(IFinal);
		}
		//esIgnorarNumero
		public bool estaDentroDeFecha(Group cn)
		{
			return estaDentroDeFecha(cn.Index);
		}
		public bool estaDentroDeFecha(Capture cn)
		{
			return estaDentroDeFecha(cn.Index);
		}
        //IdentificacionNumericaEnStr
        public bool estaDentroDeFecha(IdentificacionNumericaEnStr cn)
        {
            return estaDentroDeFecha(cn.IndiceDeRepresentacionStr);
        }
        public bool estaDentroDeFecha(int indice)
		{
			HistoriarDeBusqueda h = this.historialDeBusqueda;
			List<BuscadorDeFechasEnNombre> lb = h.getBusquedasFechasEnNombre();
			foreach (BuscadorDeFechasEnNombre b in lb) {
				if (b.estaDentroDeFecha(indice)) {
					return true;
				}
			}
			return false;
			//return this.fechaEnNombre!=null?this.fechaEnNombre.estaDentroDeLosLimites(indice):false;
		}
		
		
		public bool estaDentroDeAleatoriedad(int indice)
		{
			HistoriarDeBusqueda h = this.historialDeBusqueda;
			List<BuscadorDeAleatoriedadesEnNombre> lb = h.getBusquedasDeAleatoriedadesEnNombre();
			foreach (BuscadorDeAleatoriedadesEnNombre b in lb) {
				if (b.estaDentroDeAleatoriedad(indice)) {
					return true;
				}
			}
			return false;
			//return this.fechaEnNombre!=null?this.fechaEnNombre.estaDentroDeLosLimites(indice):false;
		}
		public bool estaDentroDeEtiquetas(int indice)
		{
			HistoriarDeBusqueda h = this.historialDeBusqueda;
			List<BuscadorDeEtiquetasEnNombre> lb = h.getBusquedasEtiquetasEnNombre();
			foreach (BuscadorDeEtiquetasEnNombre b in lb) {
				if (b.estaDentroDeEtiquetas(indice)) {
					return true;
				}
			}
			return false;
			//return this.fechaEnNombre!=null?this.fechaEnNombre.estaDentroDeLosLimites(indice):false;
		}
		public DatosDeIgnorarNumero estaDentroDeEtiquetas_DatosDeIgnorarNumero(int indice)
		{
			HistoriarDeBusqueda h = this.historialDeBusqueda;
			List<BuscadorDeEtiquetasEnNombre> lb = h.getBusquedasEtiquetasEnNombre();
			foreach (BuscadorDeEtiquetasEnNombre b in lb) {
				DatosDeIgnorarNumero dd=b.estaDentroDeEtiquetas_DatosDeIgnorarNumero(indice);
				if (dd!=null) {
					return dd;
				}
			}
			return null;
			//return this.fechaEnNombre!=null?this.fechaEnNombre.estaDentroDeLosLimites(indice):false;
		}
		public DatosDeIgnorarNumero estaDentroDeAleatoriedad_DatosDeIgnorarNumero(int indice)
		{
			HistoriarDeBusqueda h = this.historialDeBusqueda;
			List<BuscadorDeAleatoriedadesEnNombre> lb = h.getBusquedasDeAleatoriedadesEnNombre();
			foreach (BuscadorDeAleatoriedadesEnNombre b in lb) {
				DatosDeIgnorarNumero dd=b.estaDentroDeAleatoriedad_DatosDeIgnorarNumero(indice);
				if (dd!=null) {
					return dd;
				}
			}
			return null;
			//return this.fechaEnNombre!=null?this.fechaEnNombre.estaDentroDeLosLimites(indice):false;
		}
		public DatosDeIgnorarNumero estaDentroDeFecha_DatosDeIgnorarNumero(int indice)
		{
			HistoriarDeBusqueda h = this.historialDeBusqueda;
			List<BuscadorDeFechasEnNombre> lb = h.getBusquedasFechasEnNombre();
			foreach (BuscadorDeFechasEnNombre b in lb) {
				DatosDeIgnorarNumero dd=b.estaDentroDeFecha_DatosDeIgnorarNumero(indice);
				if (dd!=null) {
					return dd;
				}
			}
			return null;
			//return this.fechaEnNombre!=null?this.fechaEnNombre.estaDentroDeLosLimites(indice):false;
		}
		
//		
		
		
		
		
		
		
		
		
		
		public int esNombreConNumeroAlPrincipio_indiceFinal(string texto, int numero, int indiceInicial)
		{
			
			return this.historialDeBusqueda.esNombreConNumeroAlPrincipio_indiceFinal(texto, numero, indiceInicial);
		}
		
		public int esNombreConNumeroAlFinal_buscarDesdeElPrincipio_indiceFinal(string texto, int numero, int indiceInicial)
		{
			return this.historialDeBusqueda.esNombreConNumeroAlFinal_buscarDesdeElPrincipio_indiceFinal(texto, numero, indiceInicial);
		}
		public int esNombreConNumeroAlFinal_buscarDesdeElFinal_indiceInicial(string texto, int numero, int indiceInicialDelNumero)
		{
			return this.historialDeBusqueda.esNombreConNumeroAlFinal_buscarDesdeElFinal_indiceInicial(texto, numero, indiceInicialDelNumero);
		}
		
		public Match esNombresConUnNumeroInterno_m(string texto, int numero, int indiceInicial)
		{
			return this.historialDeBusqueda.esNombresConUnNumeroInterno_m(texto, numero, indiceInicial);
		}
		
		
		public int esNombreRodeadosDeNumeros_buscarDesdeElPrincipio_indiceFinal(string texto, int numero, int indiceInicial)
		{
			return this.historialDeBusqueda.esNombreRodeadosDeNumeros_buscarDesdeElPrincipio_indiceFinal(texto, numero, indiceInicial);
		}
		public int esNombreRodeadosDeNumeros_buscarDesdeElFinal_indiceInicial(string texto, int numero, int indiceInicialDelNumero)
		{
			return this.historialDeBusqueda.esNombreRodeadosDeNumeros_buscarDesdeElFinal_indiceInicial(texto, numero, indiceInicialDelNumero);
		}
		
		public int esNombreConNumerosAlPrincipio_indiceFinal(string texto, int numero, int indiceInicial)
		{
			return this.historialDeBusqueda.esNombreConNumerosAlPrincipio_indiceFinal(texto, numero, indiceInicial);
		}
		
		
		public int esNombreNumericosMultiples_buscarDesdeElPrincipio_indiceFinal(string texto, int numero, int indiceInicial)
		{
			return this.historialDeBusqueda.esNombreNumericosMultiples_buscarDesdeElPrincipio_indiceFinal(texto, numero, indiceInicial);
		}
		public int esNombreNumericosMultiples_buscarDesdeElFinal_indiceInicial(string texto, int numero, int indiceInicialDelNumero)
		{
			return this.historialDeBusqueda.esNombreNumericosMultiples_buscarDesdeElFinal_indiceInicial(texto, numero, indiceInicialDelNumero);
		}
	}
}






//public DatosDeIgnorarNumero getEs_IgnorarNumeroDelanteDe(string nombre, int numeroFinal, Match m)
//		{
//			return getEs_IgnorarNumeroDelanteDe(nombre, numeroFinal, m.Index + m.Length);
//		}
//		public DatosDeIgnorarNumero getEs_IgnorarNumeroDelanteDe(DatosDeNombreCapitulo dnRespuesta, string nombre, int numeroFinal, int indiceInicialNumero, int indiceAContinuacion)
//		{
//			DatosDeIgnorarNumero d = null;
//			if (esAñoModerno(numeroFinal)) {
//				return null;
//			}
//			
//			//int lengNombre = nombre.Length;
//			//if (indiceAContinuacion != lengNombre && indiceAContinuacion != lengNombre - 1) {
//			//int lengIgnorarNumeroEspecificoDelanteDe = re.cf.ignorarNumeroEspecificoDelanteDe.Length;
//			if (indiceAContinuacion != nombre.Length && indiceAContinuacion != nombre.Length - 1) {
//				int indice = -1;
//				//for (int j = 0; j < lengIgnorarNumeroEspecificoDelanteDe; j++) {
//				for (int j = 0; j < re.cf.ignorarNumeroEspecificoDelanteDe.Length; j++) {
//					CondicionIgnorarNumeroEspecifico c = re.cf.ignorarNumeroEspecificoDelanteDe[j];
//					if (c.Numero == numeroFinal) {
//						//cwl(str(c.Caracteres));
//						indice = Utiles.startsWith_Indice(nombre, indiceAContinuacion, c.Caracteres);
//						if (indice != -1) {
//							//algo negativo
//							return new DatosDeIgnorarNumero(indiceAContinuacion + c.Caracteres[indice].Length);
//						}
//					} else {
//						if (c.Numero > numeroFinal) {
//							break;
//						}
//					}
//				}
//				
//				for (int i = 0; i < 4; i++) {
//					int lengNombre = -1;
//					DatosDeNombreCapitulo.TipoDeNombreDeSerie t = DatosDeNombreCapitulo.TipoDeNombreDeSerie.NORMAL;
//					switch (i) {
//						case 0:
//							lengNombre = esNombreConNumeroAlPrincipio_indiceFinal(this.nombre, numeroFinal, indiceInicialNumero);
//							t=DatosDeNombreCapitulo.TipoDeNombreDeSerie.NUMERO_AL_PRINCIPIO;
//							break;
//						case 1:
//							lengNombre = esNombreRodeadosDeNumeros_buscarDesdeElPrincipio_indiceFinal(this.nombre, numeroFinal, indiceInicialNumero);
//							t=DatosDeNombreCapitulo.TipoDeNombreDeSerie.RODEADO_DE_NUMEROS;
//							break;
//						case 2:
//							lengNombre = esNombreConNumerosAlPrincipio_indiceFinal(this.nombre, numeroFinal, indiceInicialNumero);
//							t=DatosDeNombreCapitulo.TipoDeNombreDeSerie.NUMEROS_MULTIPLES_AL_PRINCIPIO;
//							break;
//						case 3:
//							lengNombre = esNombreNumericosMultiples_buscarDesdeElPrincipio_indiceFinal(this.nombre, numeroFinal, indiceInicialNumero);
//							t=DatosDeNombreCapitulo.TipoDeNombreDeSerie.SOLO_NUMEROS_MULTIPLES;
//							break;
//					}
//					if (lengNombre != -1) {
//						dnRespuesta.TipoDeNombre=t;
//						return new DatosDeIgnorarNumero(lengNombre);
//					}
//				}
//				
//				
//				
//				
//				//comprobrar si este numero es parte de algo que alla que saltar
//				int indiceSiguiente = indiceAContinuacion;
//				if (or(nombre.ElementAt(indiceAContinuacion - 1), ',', '.') && Char.IsNumber(nombre.ElementAt(indiceAContinuacion - 2)) && Char.IsNumber(nombre.ElementAt(indiceAContinuacion))) {
//					Match mConSeparaciones = Matchs.R_N.ReSu.Match(nombre, indiceAContinuacion);
//					indiceSiguiente = indiceAContinuacion + mConSeparaciones.Length;
//				}
//				indice = Utiles.startsWith_Indice(nombre, indiceSiguiente, true, re.cf.SaltarCualquierNumeroAntesDe);
//				if (indice != -1) {
//					indiceSiguiente = indiceAContinuacion + re.cf.SaltarCualquierNumeroAntesDe[indice].Length;
//					if (indiceSiguiente == nombre.Length || !Char.IsLetterOrDigit(nombre.ElementAt(indiceSiguiente))) {
//						return new DatosDeIgnorarNumero(indiceSiguiente);
//					}
//					
//				}
//				
//				
//				//Comprobar si el numero es parte de una Aleaterizacion
//				int indiceInicialDelNumero = indiceAContinuacion - numeroFinal.ToString().Length;
//				string tramo = re.Re_numerosYLetras.Re.Match(nombre.Substring(indiceInicialDelNumero)).ToString();
//				if (Utiles.esAleaterizacion(tramo)) {
//					d = new DatosDeIgnorarNumero(indiceInicialDelNumero + tramo.Length);
//					d.EsAleaterizacion = true;
//					return d;
//				}
//				
//				
//			}//fin del if comparar con el leng nombre
//			
//			return d;
//		}
//		public bool esIgnorarNumeroDetrasDe(string texto, int numero, int indiceInicialDelNumero)
//		{//_SinComprarAño
//			if (esAñoModerno(numero)) {
//				return true;
//			}
//			//Me quedo con el texto hasta el comienzo del numero
//			//para despues poder valorar si hay algo que lo marque como numero a saltar
//			string subTexto = subs(texto, 0, indiceInicialDelNumero);
//			
//			//Se elimina las separaciones finales (desde el comienzo del numero hacia atras
//			//, como el paso anterior recorto el estring estas separaciones de existir
//			//se encuentran al final)
//			subTexto = re.Re_separaciones.ReFinal.Replace(subTexto, "");
//			
//			//Se comprueba la coincidencia (terminado el estring) con algun patron que indique que hay que
//			//ignorar ese numero (recordar que se limpio el final de separaciones)
//			if (re.Re_saltarCualquierNumeroDespuesDe_Patron.ReFinal.Match(subTexto).Success) {
//				return true;
//			}
//			
//			int lengIgnorarNumeroEspecificoDetrasDe = re.cf.ignorarNumeroEspecificoDetrasDe.Length;
//			int indice = -1;
//			for (int j = 0; j < lengIgnorarNumeroEspecificoDetrasDe; j++) {
//				CondicionIgnorarNumeroEspecifico c = re.cf.ignorarNumeroEspecificoDetrasDe[j];
//				if (c.Numero == numero) {
//						
//					indice = Utiles.endsWith_Indice(subTexto, c.Caracteres);
//					if (indice != -1) {
//						//algo negativo
//						return true;
//					}
//				} else {
//					if (c.Numero > numero) {
////						break;
//					}
//				}
//			}//fin for
//			
//			
//			Match m = re.Re_numerosYLetras.ReFinal.Match(subTexto);
//			if (m.Success && Utiles.esAleaterizacion(m.ToString(), true)) {
//				return true;
//			}
//			
//			return false;
//		}
//		public bool esNumeroParteDeNombre(string texto, int numero, int indiceInicialDelNumero, int indiceAContinuacion)
//		{
//			
//			string subTexto = subs(texto, 0, indiceInicialDelNumero);
//			int lengIgnorarNumeroEspecificoDetrasDe = re.cf.ignorarNumeroEspecificoDetrasDe.Length;
//			int indice = -1;
//			for (int j = 0; j < lengIgnorarNumeroEspecificoDetrasDe; j++) {
//				CondicionIgnorarNumeroEspecifico c = re.cf.ignorarNumeroEspecificoDetrasDe[j];
//				if (c.Numero == numero) {
//						
//					indice = Utiles.endsWith_Indice(subTexto, c.Caracteres);
//					if (indice != -1) {
//						//algo negativo
//						return true;
//					}
//				} else {
//					if (c.Numero > numero) {
////						break;
//					}
//				}
//			}//fin for
//			
//			
//			int lengIgnorarNumeroEspecificoDelanteDe = re.cf.ignorarNumeroEspecificoDelanteDe.Length;
//			indice = -1;
//			for (int j = 0; j < lengIgnorarNumeroEspecificoDelanteDe; j++) {
//				CondicionIgnorarNumeroEspecifico c = re.cf.ignorarNumeroEspecificoDelanteDe[j];
//				if (c.Numero == numero) {
//					//cwl(str(c.Caracteres));
//					indice = Utiles.startsWith_Indice(nombre, indiceAContinuacion, c.Caracteres);
//					if (indice != -1) {
//						//algo negativo
//						//return new DatosDeIgnorarNumero(indiceAContinuacion + c.Caracteres[indice].Length);
//						return true;
//					}
//				} else {
//					if (c.Numero > numero) {
//						break;
//					}
//				}
//			}
//				
//			
//			
//			return false;
//		}
//		
//		
//		
//		//union
//		
//		public bool buscarDatosUnion(string nombre, DatosDeNombreCapitulo dd, Match mm, int iNumeroInicial = 0, Action<Capture,int> alDescartarUltimoNumero = null)
//		{//alDescartarUltimoNumero  (Capture #, indiceAContinuacionDespuesDeLosSaltos)=>{}
//			//DatosDeBuscarConjunto
//			//Group gU = re.getGrupoUnion(mm);
//			Group gC = re.getGrupoNumeroCapitulo(mm);
//			CaptureCollection cl = gC.Captures;
////			int numero=inT_Grp(gU);
////			int indiceEnNombreDelPrimerNumero=gC.Captures[0].Index;
//			
//			DatosDeBuscarConjunto d = new DatosDeBuscarConjunto();
//			d.EsConjunto = false;
//			d.EncontroPatron = false;
//			d.NumeroInicial = inT_Cap(cl[iNumeroInicial]);//inT_Grp(gU);
//			
//			
//			d.IndiceNumeroInicial = cl[iNumeroInicial].Index;//2387
//			
//			
//			int indiceEnListaDeUltimo = cl.Count - 1;
//			int ultimoNumero = inT_Cap(cl[indiceEnListaDeUltimo]);
//			string ultimoNumeroStr = cl[indiceEnListaDeUltimo].ToString();
//			int indiceAContinuacion = mm.Index + mm.Length;//cl[indiceEnListaDeUltimo].Index+cl[indiceEnListaDeUltimo].Value.Length;
//			
//			string separador = null;
//			bool esContinuidad = re.getGrupoContinuidad(mm).Success;
//			
//			
//			string numeroAnteriorStr = "";
//			//compruebo que tiene la misma separacion 
//			//y que los numeros esten en orden ascendente
//			for (int i = iNumeroInicial + 1; i <= indiceEnListaDeUltimo; i++) {
//				int indiceNumeroAnterior = i - 1;
//				Capture cAnterio = cl[indiceNumeroAnterior];
//				Capture cActual = cl[i];
//				int indiceFinAnteriorN = cAnterio.Index + cActual.Length;//(cActual.Value.Length)
//				int indiceInicioActualN = cActual.Index;
//				int numeroAnterior = inT_Cap(cAnterio);
//				numeroAnteriorStr = cAnterio.ToString();
//				int numeroActual = inT_Cap(cActual);
//				string separadorActual = subs(nombre, indiceFinAnteriorN, indiceInicioActualN);
//				
//				if (numeroAnterior > numeroActual
//				    || ((separador == null) ? false : separador != separadorActual)
//				    || (esContinuidad && numeroAnterior + 1 != numeroActual)) {
//					indiceEnListaDeUltimo = indiceNumeroAnterior;
//					ultimoNumero = numeroAnterior;
//					ultimoNumeroStr = numeroAnteriorStr;
//					indiceAContinuacion = cActual.Index;
//					if (alDescartarUltimoNumero != null) {
//						alDescartarUltimoNumero(cAnterio, indiceAContinuacion);
//					}
//					break;
//				}
//				if (separador == null) {
//					separador = separadorActual;
//				}
//				
//				
//				
//			}
//			
//			//voy comprobando que el ultimo numero no alla que ignorarlo
//			//siempre que me queden al menos dos numeros para que sea una union
//			for (int i = indiceEnListaDeUltimo; i > iNumeroInicial; i--) {
//				if (i != indiceEnListaDeUltimo) {
//					indiceAContinuacion = cl[i + 1].Index;
//					ultimoNumero = inT_Cap(cl[i]);
//					ultimoNumeroStr = cl[i].ToString();
//					indiceEnListaDeUltimo = i;
//					if (alDescartarUltimoNumero != null) {
//						alDescartarUltimoNumero(cl[i], indiceAContinuacion);
//					}
//				}
//				
//				DatosDeIgnorarNumero ignorar = getEs_IgnorarNumeroDelanteDe(nombre, ultimoNumero, indiceAContinuacion);
//				if (ignorar == null
//				    && (!estaDentroDeFecha(cl[i]))) {
//					d.EncontroPatron = true;
//					d.EsConjunto = true;
//					
//					dd.EsConjuntoDeCapitulos = true;
//					//dd.CapituloInicial = inT_Cap(cl[iNumeroInicial]);
//					dd.CapituloInicialStr = cl[iNumeroInicial].ToString();
//					dd.IndiceNumeroCapituloInicial = cl[iNumeroInicial].Index;
//					//dd.CapituloFinal = ultimoNumero;
//					dd.CapituloFinalStr = ultimoNumeroStr;
//					dd.IndiceNumeroCapituloFinal = cl[indiceEnListaDeUltimo].Index;
//					
//					//return d;
//					return true;
//				}
//			}
//			return false;
//			
//			
//		}
//		
//		
//		
//		//apoyo
//		public bool esAñoModerno(int numero)
//		{
//			return numero > 1950 && numero < 2030;
//		}
//		public DatosDeNombreCapituloDelFinal crearDatosDeNombreCapituloDelFinal()
//		{
//			return new DatosDeNombreCapituloDelFinal();
//		}
//		public string getNumeroMasSeparacion(string texto, int numero, int indiceNumero)
//		{
//			string r = numero.ToString();
//			for (int i = indiceNumero + numero.ToString().Length; i < texto.Length; i++) {
//				char c = texto.ElementAt(i);
//				if (esCharSeparacion(c)) {
//					r += c;
//				} else {
//					break;
//				}
//			}
//			return r;
//		}
//		public string getSinSeparacionAlFinal(string texto)
//		{
//			int indiceFinal = -1;
//			for (int i = texto.Length - 1; i > 0; i--) {
//				char c = texto.ElementAt(i);
//				if (!esCharSeparacion(c)) {
//					indiceFinal = i;
//				}
//			}
//			return (indiceFinal > 0) ? subs(texto, 0, indiceFinal) : (indiceFinal == 0 ? texto.ElementAt(0).ToString() : "");
//			
//		}
//		public int getIndiceAContinuacionDeSeparacionDespuesDeNumero(string texto, int numero, int indiceNumero)
//		{
//			return indiceNumero + getNumeroMasSeparacion(texto, numero, indiceNumero).Length;
//		}
//		public bool esCharSeparacion(char c)
//		{
//			return !(Char.IsLetterOrDigit(c));
//		}
//		//apoyo Contextos
//		
//		public DatosNombreNumerico esNombreNumericoSimpleDesdeELPrincipio_ModificaContexto(int numeroDeSerie, bool detenerSiEncuentraPatronAlfinal = true)
//		{
//			int indice = orIndice(numeroDeSerie, re.cf.nombresNumericosCompletosSimples);
//			if (indice != -1) {
//				//para confirmar que es un nombre numerico simple debe de tener la 
//				//informacion del capitulo al final como patron ()que seguro difiere del numero incial
//				DatosNombreNumerico d = new DatosNombreNumerico();
////				HistoriarDeBusqueda h = pr.historialDeBusqueda;
//				int indiceDeBusqueda = re.cf.nombresNumericosCompletosSimples[indice].ToString().Length;
//			
////				d.D = h.buscarPatronesAlFinal(nombre, indiceDeBusqueda);
//				d.D = buscarPatronesAlFinal(indiceDeBusqueda);
//				if (d.D != null && detenerSiEncuentraPatronAlfinal) {
//					return d;
//				}
//				
//				//d.Indice = esNombreNumericoSimple_ModificaContexto_Indice(numeroDeSerie);
//				d.Indice = _esNombreNumericoSimple_ModificaContexto_Indice(dnRespuesta, numeroDeSerie, indice, df: d.D);
//				if (d.Indice != null) {
//					return d;
//				}
//			}
//			return null;
//		}
//		
//		public DatosNombreNumerico esNombresNumericosCompletosMultiplesDesdeElPrincipio_ModificaContexto(DatosDeNombreCapitulo dnRespuesta, string nombre, int I0, int indiceInicialDelNumero, bool detenerSiEncuentraPatronAlfinal = true)
//		{
//			//int indice = Utiles.startsWith_Indice(nombre, I0, this.re.cf.nombresNumericosCompletosMultiples);
//			int lengDelNombre = esNombreNumericosMultiples_buscarDesdeElPrincipio_indiceFinal(nombre, numero, indiceInicialDelNumero);
//			if (lengDelNombre != -1) {
//				//if (indice != -1) {
//				DatosNombreNumerico d = new DatosNombreNumerico();
//				//HistoriarDeBusqueda h = pr.historialDeBusqueda;
//				//d.D = h.buscarPatronesAlFinal(nombre, I0);
//				d.D = buscarPatronesAlFinal(I0);
//				//d.D = buscarPatronesAlFinal(nombre, I0);
//				if (detenerSiEncuentraPatronAlfinal && d.D != null) {
//					return d;
//				}
//				
//				string nombreDeSerie = subs(nombre, indiceInicialDelNumero, lengDelNombre);
//				d.loEs = _esNombresNumericosCompletosMultiples_ModificaContexto(d.D != null ? d.D : dnRespuesta, nombreDeSerie);
//				if (d.loEs) {
//					return d;
//				}
//				//}
//				
//			}
//			return null;
//		}
//		
//		public int esNombreNumericoSimple_ModificaContexto_Indice(DatosDeNombreCapitulo dnRespuesta, int numeroDeSerie)
//		{
//			int indice = orIndice(numeroDeSerie, re.cf.nombresNumericosCompletosSimples);
//			if (indice != -1) {
//					
//				return _esNombreNumericoSimple_ModificaContexto_Indice(dnRespuesta, numeroDeSerie, indice);
//				
//					
//					
//			}// fin del if coincidencia con nombre numero simple
//			return -1;
//		}
//		
//		public bool esNombreNumericoSimple_ModificaContexto(DatosDeNombreCapitulo dnRespuesta, int numeroDeSerie)
//		{
//			return esNombreNumericoSimple_ModificaContexto_Indice(dnRespuesta, numeroDeSerie) != -1;
//		}
//		
//		public bool esNombresNumericosCompletosMultiples_ModificaContexto(DatosDeNombreCapitulo dnRespuesta, string nombre, int numero, int indiceInicialDelNumero)
//		{
//			//int indice = Utiles.startsWith_Indice(nombre, I0, re.cf.nombresNumericosCompletosMultiples);
//			int lengDelNombre = esNombreNumericosMultiples_buscarDesdeElPrincipio_indiceFinal(nombre, numero, indiceInicialDelNumero);
//			if (lengDelNombre != -1) {
//				string nombreDeSerie = subs(nombre, indiceInicialDelNumero, lengDelNombre);
//				return _esNombresNumericosCompletosMultiples_ModificaContexto(dnRespuesta, nombreDeSerie);
//			}
//			
//			
//			return false;
//		}
//		
//		public bool esNombreRodeadoDeNumeros_AlPrincipio_ModificaContexto(DatosDeNombreCapitulo dnRespuesta, int numero, bool detenerSiEncuentraPatronesAlFinal = true)
//		{
//			DatosNombreNumerico dn = esNombreRodeadoDeNumeros_AlPrincipio_ModificaContexto_Dn(dnRespuesta, numero, detenerSiEncuentraPatronesAlFinal);
//			return dn != null && dn.loEs;
//		}
//		
//		public DatosNombreNumerico esNombreRodeadoDeNumeros_AlPrincipio_ModificaContexto_Dn(DatosDeNombreCapitulo dnRespuesta, int numero, bool detenerSiEncuentraPatronesAlFinal = true)
//		{
//			
//			if (this.re.cf.NombresRodeadosDeNumeros != null) {
//				DatosNombreNumerico dn = new DatosNombreNumerico();
//				//int ind = Utiles.startsWith_Indice(nombre, this.re.cf.NombresRodeadosDeNumeros);
//				int lengNombre = esNombreRodeadosDeNumeros_buscarDesdeElPrincipio_indiceFinal(this.nombre, numero, 0);
//						
//				if (lengNombre != -1) {
//					string nombreDeEstaSerie = subs(this.nombre, 0, lengNombre);//this.re.cf.NombresRodeadosDeNumeros[ind];
//					DatosDeNombreCapituloDelFinal df = null;
//							
////							HistoriarDeBusqueda h = this.pr.historialDeBusqueda;
////							df = h.buscarPatronesAlFinal(nombre, 0);
////							
//					
//					df = buscarPatronesAlFinal(0);
//					dn.D = df;
//					if (detenerSiEncuentraPatronesAlFinal && df != null) {
//						dn.D = df;
//						return dn;
//						//return true;
//					}
//					dn.loEs = esNombreRodeadoDeNumeros_AlPrincipio_ModificaContexto_Indice(dnRespuesta, nombreDeEstaSerie, df);
//					if (dn.loEs) {
//						dn.D = df;
//						return dn;
//					}
////					dn.Indice = ind;
////					if (this.contexto.EsVideo) {
////						//la idea es que si es un video debe de contener al menos
////						//la informacion de los capitulos plq si es igual al NombresRodeadosDeNumeros
////						//seria solo coincidencia pq este numero seria realmente parte
////						//de la informacion del capitulo
////						if (nombre.Trim() != this.re.cf.NombresRodeadosDeNumeros[ind]) {
////							this.contextoDeConjunto.agregarPropiedadesAContextoNombreRodeadoDeNumeros(nombreDeEstaSerie);
////							//agregar al contexto
////							return dn;
////						}	
////					} else {
////						//agregar al contexto
////						this.contextoDeConjunto.agregarPropiedadesAContextoNombreRodeadoDeNumeros(nombreDeEstaSerie);
////						return dn;
////					}
//						
//						
//				}
//			}//fin si hay cf.NombresRodeadosDeNumeros 
////		
//			return null;
//		}
//		
//		