/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 28/7/2022
 * Hora: 18:23
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
	/// Description of HistoriarDeBusqueda.
	/// </summary>
	public class HistoriarDeBusqueda:ConsolaBasica
	{
		class SubNombreSinEnvoltorios
		{
			public string nombreSinEnvoltorios;
			public int indiceDeFinalDeReorte;
		}
		private List<SubNombreSinEnvoltorios> nombresRecortadosSinEnvoltorios;
		
		private List<BuscadorDePatronesDeSerieAlFinal> patronesAlFinal;
		private List<BuscadorDeDatosDeSerieAlFinal> datosAlFinal;
		private BuscadorDeDatosDeSerieAlPrincipio datosAlPrincipio;
		private BuscadorDeDatosDeSerieAlPrincipio datosAlPrincipio_NoDetenerPorDatosAlFinal;
		private ProcesadorDeNombreDeSerie pr;
		
		private List<BuscadorDeFechasEnNombre> fechasEnNombre;
		private List<BuscadorDeAleatoriedadesEnNombre> aleatoriedadEnNombre;
		private List<BuscadorDeEtiquetasEnNombre> etiquetasEnNombre;		
		
		private CreadorDeNombreClaveDeSerie creadorDeClaves;
		private DatosDeNombreSerie datosDeNombre_NoDetenerPorDatosAlFinal;
		private DatosDeNombreSerie datosDeNombre;
		
		
		
		private Dictionary<string,int> esNombreConNumeroAlPrincipio_dic;//key:indice inicial value:indice final
		private Dictionary<string, int> esNombreConNumeroAlFinal_buscarDesdeElPrincipio_dic;//key:indice inicial value:indice final
		private Dictionary<string, int> esNombreConNumeroAlFinal_buscarDesdeElFinal_dic;//key:indice inicial delNumero value:indice inicial
		private Dictionary<string, Match> esNombresConUnNumeroInterno_dic;//key:indice inicial value:Match
		private Dictionary<string, int> esNombreRodeadosDeNumeros_buscarDesdeElPrincipio_dic;//key:indice inicial value:indice final
		private Dictionary<string, int> esNombreRodeadosDeNumeros_buscarDesdeElFinal_dic;//key:indice inicial delNumero value:indice inicial
		private Dictionary<string, int> esNombreConNumerosAlPrincipio_dic;//key:indice inicial value:indice final
		private Dictionary<string, int> esNombreNumericosMultiples_buscarDesdeElPrincipio_dic;//key:indice inicial value:indice final
		private Dictionary<string, int> esNombreNumericosMultiples_buscarDesdeElFinal_dic;//key:indice inicial delNumero value:indice inicial
		
		private BuscadorDeTipoDeNombre buscadorDeTipoDeNombre;
		
		private Dictionary<string,TipoDeNombreDeSerie?> clasificacionesDeNombreDeSerie;
		private ProcesadorDeSeries pro;
		private Dictionary<string,string> clavesCreadas;
		public HistoriarDeBusqueda(ProcesadorDeNombreDeSerie pr,ProcesadorDeSeries pro=null)
		{
			this.pr = pr;
			this.pro=pro;
		}
		
		
		public TipoDeNombreDeSerie? getTipoDeNombreDe(string nombreDeSerie){
			if(this.clasificacionesDeNombreDeSerie==null){
				this.clasificacionesDeNombreDeSerie=new Dictionary<string,TipoDeNombreDeSerie?>();
			}
			if(this.clasificacionesDeNombreDeSerie.ContainsKey(nombreDeSerie)){
				return this.clasificacionesDeNombreDeSerie[nombreDeSerie];
			}
			ClasificadorDeNombreDeSerie c=new ClasificadorDeNombreDeSerie(this.pr,nombreDeSerie);
			TipoDeNombreDeSerie? t=c.getTipoDeNombreDe();
			this.clasificacionesDeNombreDeSerie.Add(nombreDeSerie,t);
			return t;
		}
		
		public string getNombreRecortadoSinEnvoltorios(int IFinal)
		{
			if (this.nombresRecortadosSinEnvoltorios != null) {
				foreach (SubNombreSinEnvoltorios sn in this.nombresRecortadosSinEnvoltorios) {
					if (sn.indiceDeFinalDeReorte == IFinal) {
						return sn.nombreSinEnvoltorios;
					}
				}
			} else {
				this.nombresRecortadosSinEnvoltorios = new List<SubNombreSinEnvoltorios>();
			}
		 	
			SubNombreSinEnvoltorios sn2 = new SubNombreSinEnvoltorios();
			sn2.indiceDeFinalDeReorte = IFinal;
			sn2.nombreSinEnvoltorios = getSinSeparacionAlFinal(Utiles.eliminarContenidoDeEnvolturas(subs(pr.nombre, 0, IFinal)));
			this.nombresRecortadosSinEnvoltorios.Add(sn2);
			return sn2.nombreSinEnvoltorios;
		}
		
		public  List<BuscadorDeFechasEnNombre> getBusquedasFechasEnNombre()
		{
			if (this.fechasEnNombre == null) {
				this.fechasEnNombre = new List<BuscadorDeFechasEnNombre>();
			}
			return this.fechasEnNombre;
		}
		public void addBusquedaFechaEnNombre(BuscadorDeFechasEnNombre p)
		{
			this.getBusquedasFechasEnNombre().Add(p);
		}
		
		public BuscadorDeFechasEnNombre getBusquedaFechasEnNombre(int I0)//string nombre,
		{
			List<BuscadorDeFechasEnNombre> l = getBusquedasFechasEnNombre();
			foreach (BuscadorDeFechasEnNombre e in l) {
				if (e.seBuscoCon(I0)) {//nombre, 
					return e;
				}
			}
			return null;
		}
		
		public bool seBuscoFechasEnNombre(int I0)//string nombre,
		{
			return getBusquedaFechasEnNombre(I0) != null;//nombre,
		}
		
		
		
		public List<DatosDeFechaEnNombre> buscarFechas(int I0)
		{
			List<DatosDeFechaEnNombre> d = null;
			if (this.seBuscoFechasEnNombre(I0)) {//nombre,
				d = this.getBusquedaFechasEnNombre(I0).getFechasEnNombre();//nombre,
			} else {
				BuscadorDeFechasEnNombre p = new BuscadorDeFechasEnNombre(this.pr);
				d = p.buscarFechas(I0);//nombre,
				this.addBusquedaFechaEnNombre(p);
							
			}
			return d;
		}
		
		
		private  List<BuscadorDePatronesDeSerieAlFinal> getBusquedasPatronesAlFinal()
		{
			if (this.patronesAlFinal == null) {
				this.patronesAlFinal = new List<BuscadorDePatronesDeSerieAlFinal>();
			}
			return this.patronesAlFinal;
		}
		
		public void addBusquedaPatronAlFinal(BuscadorDePatronesDeSerieAlFinal p)
		{
			this.getBusquedasPatronesAlFinal().Add(p);
		}
		
		public BuscadorDePatronesDeSerieAlFinal getBusquedaPatronAlFinal(int I0)//string nombre,
		{
			List<BuscadorDePatronesDeSerieAlFinal> l = getBusquedasPatronesAlFinal();
			foreach (BuscadorDePatronesDeSerieAlFinal e in l) {
				if (e.seBuscoCon(I0)) {//nombre, 
					return e;
				}
			}
			return null;
		}
		public bool seBuscoPatronAlFinal(int I0)//string nombre,
		{
			return getBusquedaPatronAlFinal(I0) != null;//nombre,
		}
		
		public DatosDeNombreCapituloDelFinal buscarPatronesAlFinal(int I0)
		{
			DatosDeNombreCapituloDelFinal d = null;
			if (this.seBuscoPatronAlFinal(I0)) {//nombre,
				d = this.getBusquedaPatronAlFinal(I0).getD();//nombre, 
			} else {
				BuscadorDePatronesDeSerieAlFinal p = new BuscadorDePatronesDeSerieAlFinal(this.pr);
				d = p.buscarPatronesAlFinal(I0);//nombre, 
				this.addBusquedaPatronAlFinal(p);
							
			}
			return d;
		}
		
		
		
		
		
		private  List<BuscadorDeDatosDeSerieAlFinal> getBusquedasDatosAlFinal()
		{
			if (this.datosAlFinal == null) {
				this.datosAlFinal = new List<BuscadorDeDatosDeSerieAlFinal>();
			}
			return this.datosAlFinal;
		}
		
		public void addBusquedaDatosAlFinal(BuscadorDeDatosDeSerieAlFinal p)
		{
			this.getBusquedasDatosAlFinal().Add(p);
		}
		
		public BuscadorDeDatosDeSerieAlFinal getBusquedaDatosAlFinal(int I0)//string nombre,
		{
			List<BuscadorDeDatosDeSerieAlFinal> l = getBusquedasDatosAlFinal();
			foreach (BuscadorDeDatosDeSerieAlFinal e in l) {
				if (e.seBuscoCon(I0)) {//nombre, 
					return e;
				}
			}
			return null;
		}
		public bool seBuscoDatosAlFinal(int I0)//string nombre,
		{
			return getBusquedaDatosAlFinal(I0) != null;//nombre,
		}
		
		public DatosDeNombreCapituloDelFinal getCapitulosDeNombreDelFinal(int I0)
		{
			DatosDeNombreCapituloDelFinal d = null;
			if (this.seBuscoDatosAlFinal(I0)) {//nombre,
				d = this.getBusquedaDatosAlFinal(I0).getD();//nombre, 
			} else {
				BuscadorDeDatosDeSerieAlFinal p = new BuscadorDeDatosDeSerieAlFinal(this.pr);
				d = p.getCapitulosDeNombreDelFinal(I0);//nombre, 
				this.addBusquedaDatosAlFinal(p);
							
			}
			return d;
		}
		
		
		public  DatosDeNombreCapituloDelPrincipio getCapitulosDeNombreDelPrincipio(bool detenerSiEncuentraPatronesAlFinal = true)
		{
			if (!detenerSiEncuentraPatronesAlFinal) {
				if (this.datosAlPrincipio_NoDetenerPorDatosAlFinal == null) {
					this.datosAlPrincipio_NoDetenerPorDatosAlFinal = new BuscadorDeDatosDeSerieAlPrincipio(this.pr);
				}
				if (!this.datosAlPrincipio_NoDetenerPorDatosAlFinal.seBuscoCon(0)) {
					datosAlPrincipio_NoDetenerPorDatosAlFinal.detenerSiEncuentraPatronesAlFinal = detenerSiEncuentraPatronesAlFinal;
					return this.datosAlPrincipio_NoDetenerPorDatosAlFinal.getCapitulosDeNombreDelPrincipio();//detenerSiEncuentraPatronesAlFinal
				}
				return this.datosAlPrincipio_NoDetenerPorDatosAlFinal.getD();
			} else {
				if (this.datosAlPrincipio == null) {
					this.datosAlPrincipio = new BuscadorDeDatosDeSerieAlPrincipio(this.pr);
				}
				if (!this.datosAlPrincipio.seBuscoCon(0)) {
					return this.datosAlPrincipio.getCapitulosDeNombreDelPrincipio();
				}
				return this.datosAlPrincipio.getD();
			}
			
			
			
			//return this.datosAlPrincipio!=null?this.datosAlPrincipio:().getCapitulosDeNombreDelPrincipio();
		}
		
		public CreadorDeNombreClaveDeSerie getCreadorDeClaves(){
			if(this.creadorDeClaves==null){
				this.creadorDeClaves=new CreadorDeNombreClaveDeSerie(this.pr,this.pro);
			}
			return this.creadorDeClaves;
		}
		
		public  DatosDeNombreSerie crearDatosDeNombre(bool detenerSiEncuentraPatronesAlFinal = true)//,ProcesadorDeSeries pro=null
		{
			if (detenerSiEncuentraPatronesAlFinal) {
				if (this.datosDeNombre == null) {
					CreadorDeNombreClaveDeSerie c = getCreadorDeClaves(); //new CreadorDeNombreClaveDeSerie(this.pr,this.pro);
					this.datosDeNombre = c.crearDatosDeNombre();
				}
				return this.datosDeNombre;
			} else {
				if (this.datosDeNombre_NoDetenerPorDatosAlFinal == null) {
					CreadorDeNombreClaveDeSerie c = getCreadorDeClaves();//new CreadorDeNombreClaveDeSerie(this.pr,this.pro);
					this.datosDeNombre_NoDetenerPorDatosAlFinal = c.crearDatosDeNombre(detenerSiEncuentraPatronesAlFinal);
				}
				return this.datosDeNombre_NoDetenerPorDatosAlFinal;
			}
			
			
			
			
		}
        private string getKey(string nombre,int numero,int indice) {
            return nombre + "&" + numero + "&" + indice;
        }
		
		public int esNombreConNumeroAlPrincipio_indiceFinal(string texto, int numero, int i){
			if(this.esNombreConNumeroAlPrincipio_dic==null){
				this.esNombreConNumeroAlPrincipio_dic=new Dictionary<string, int>();
				if(this.buscadorDeTipoDeNombre==null){
					this.buscadorDeTipoDeNombre=new BuscadorDeTipoDeNombre(this.pr);
				}
			}
			Dictionary<string, int> d=this.esNombreConNumeroAlPrincipio_dic;
            string key = getKey(texto, numero, i);

            if (d.ContainsKey(key)){
				return d[key];
			}
			int r=this.buscadorDeTipoDeNombre.esNombreConNumeroAlPrincipio_indiceFinal(texto,numero,i);
			d.Add(key, r);
			return r;
		}
		public int esNombreConNumeroAlFinal_buscarDesdeElPrincipio_indiceFinal(string texto, int numero, int i){
			if(this.esNombreConNumeroAlFinal_buscarDesdeElPrincipio_dic==null){
				this.esNombreConNumeroAlFinal_buscarDesdeElPrincipio_dic=new Dictionary<string,int>();
				if(this.buscadorDeTipoDeNombre==null){
					this.buscadorDeTipoDeNombre=new BuscadorDeTipoDeNombre(this.pr);
				}
			}
            string key = getKey(texto, numero, i);
			Dictionary<string,int> d=this.esNombreConNumeroAlFinal_buscarDesdeElPrincipio_dic;
			if(d.ContainsKey(key)){
				return d[key];
			}
			int r=this.buscadorDeTipoDeNombre.esNombreConNumeroAlFinal_buscarDesdeElPrincipio_indiceFinal(texto,numero,i);
			d.Add(key,r);
			return r;
		}
		public int esNombreConNumeroAlFinal_buscarDesdeElFinal_indiceInicial(string texto, int numero, int i){
			if(this.esNombreConNumeroAlFinal_buscarDesdeElFinal_dic==null){
				this.esNombreConNumeroAlFinal_buscarDesdeElFinal_dic=new Dictionary<string,int>();
				if(this.buscadorDeTipoDeNombre==null){
					this.buscadorDeTipoDeNombre=new BuscadorDeTipoDeNombre(this.pr);
				}
			}
            string key = getKey(texto, numero, i);
            Dictionary<string,int> d=this.esNombreConNumeroAlFinal_buscarDesdeElFinal_dic;
			if(d.ContainsKey(key)){
				return d[key];
			}
			int r=this.buscadorDeTipoDeNombre.esNombreConNumeroAlFinal_buscarDesdeElFinal_indiceInicial(texto,numero,i);
			d.Add(key,r);
			return r;
		}
		public int esNombreRodeadosDeNumeros_buscarDesdeElPrincipio_indiceFinal(string texto, int numero, int i){
			if(this.esNombreRodeadosDeNumeros_buscarDesdeElPrincipio_dic==null){
				this.esNombreRodeadosDeNumeros_buscarDesdeElPrincipio_dic=new Dictionary<string,int>();
				if(this.buscadorDeTipoDeNombre==null){
					this.buscadorDeTipoDeNombre=new BuscadorDeTipoDeNombre(this.pr);
				}
			}
            string key = getKey(texto, numero, i);
            Dictionary<string,int> d=this.esNombreRodeadosDeNumeros_buscarDesdeElPrincipio_dic;
			if(d.ContainsKey(key)){
				return d[key];
			}
			int r=this.buscadorDeTipoDeNombre.esNombreRodeadosDeNumeros_buscarDesdeElPrincipio_indiceFinal(texto,numero,i);
			d.Add(key,r);
			return r;
		}
		public int esNombreRodeadosDeNumeros_buscarDesdeElFinal_indiceInicial(string texto, int numero, int i){
			if(this.esNombreRodeadosDeNumeros_buscarDesdeElFinal_dic==null){
				this.esNombreRodeadosDeNumeros_buscarDesdeElFinal_dic=new Dictionary<string,int>();
				if(this.buscadorDeTipoDeNombre==null){
					this.buscadorDeTipoDeNombre=new BuscadorDeTipoDeNombre(this.pr);
				}
			}
            string key = getKey(texto, numero, i);
            Dictionary<string,int> d=this.esNombreRodeadosDeNumeros_buscarDesdeElFinal_dic;
			if(d.ContainsKey(key)){
				return d[key];
			}
			int r=this.buscadorDeTipoDeNombre.esNombreRodeadosDeNumeros_buscarDesdeElFinal_indiceInicial(texto,numero,i);
			d.Add(key,r);
			return r;
		}
		public int esNombreConNumerosAlPrincipio_indiceFinal(string texto, int numero, int i){
			if(this.esNombreConNumerosAlPrincipio_dic==null){
				this.esNombreConNumerosAlPrincipio_dic=new Dictionary<string,int>();
				if(this.buscadorDeTipoDeNombre==null){
					this.buscadorDeTipoDeNombre=new BuscadorDeTipoDeNombre(this.pr);
				}
			}
            string key = getKey(texto, numero, i);
            Dictionary<string,int> d=this.esNombreConNumerosAlPrincipio_dic;
			if(d.ContainsKey(key)){
				return d[key];
			}
			int r=this.buscadorDeTipoDeNombre.esNombreConNumerosAlPrincipio_indiceFinal(texto,numero,i);
			d.Add(key,r);
			return r;
		}
		public int esNombreNumericosMultiples_buscarDesdeElPrincipio_indiceFinal(string texto, int numero, int i){
			if(this.esNombreNumericosMultiples_buscarDesdeElPrincipio_dic==null){
				this.esNombreNumericosMultiples_buscarDesdeElPrincipio_dic=new Dictionary<string,int>();
				if(this.buscadorDeTipoDeNombre==null){
					this.buscadorDeTipoDeNombre=new BuscadorDeTipoDeNombre(this.pr);
				}
			}
            string key = getKey(texto, numero, i);
            Dictionary<string,int> d=this.esNombreNumericosMultiples_buscarDesdeElPrincipio_dic;
			if(d.ContainsKey(key)){
				return d[key];
			}
			int r=this.buscadorDeTipoDeNombre.esNombreNumericosMultiples_buscarDesdeElPrincipio_indiceFinal(texto,numero,i);
			d.Add(key,r);
			return r;
		}
		public int esNombreNumericosMultiples_buscarDesdeElFinal_indiceInicial(string texto, int numero, int i){
			if(this.esNombreNumericosMultiples_buscarDesdeElFinal_dic==null){
				this.esNombreNumericosMultiples_buscarDesdeElFinal_dic=new Dictionary<string,int>();
				if(this.buscadorDeTipoDeNombre==null){
					this.buscadorDeTipoDeNombre=new BuscadorDeTipoDeNombre(this.pr);
				}
			}
            string key = getKey(texto, numero, i);
            Dictionary<string,int> d=this.esNombreNumericosMultiples_buscarDesdeElFinal_dic;
			if(d.ContainsKey(key)){
				return d[key];
			}
			int r=this.buscadorDeTipoDeNombre.esNombreNumericosMultiples_buscarDesdeElFinal_indiceInicial(texto,numero,i);
			d.Add(key,r);
			return r;
		}
		
		public Match esNombresConUnNumeroInterno_m(string texto, int numero, int i){
			if(this.esNombresConUnNumeroInterno_dic==null){
				this.esNombresConUnNumeroInterno_dic=new Dictionary<string,Match>();
				if(this.buscadorDeTipoDeNombre==null){
					this.buscadorDeTipoDeNombre=new BuscadorDeTipoDeNombre(this.pr);
				}
			}
            string key = getKey(texto, numero, i);
            Dictionary<string,Match> d=this.esNombresConUnNumeroInterno_dic;
			if(d.ContainsKey(key)){
				return d[key];
			}
			Match r=this.buscadorDeTipoDeNombre.esNombresConUnNumeroInterno_m(texto,numero,i); 
			d.Add(key,r);
			return r;
		}
		
		
		
		
		
		
		
		
		public  List<BuscadorDeAleatoriedadesEnNombre> getBusquedasDeAleatoriedadesEnNombre()
		{
			if (this.aleatoriedadEnNombre == null) {
				this.aleatoriedadEnNombre = new List<BuscadorDeAleatoriedadesEnNombre>();
			}
			return this.aleatoriedadEnNombre;
		}
		public void addBusquedaDeAleatoriedadesEnNombre(BuscadorDeAleatoriedadesEnNombre p)
		{
			this.getBusquedasDeAleatoriedadesEnNombre().Add(p);
		}
		
		public BuscadorDeAleatoriedadesEnNombre getBusquedaDeAleatoriedadesEnNombre(int I0)//string nombre,
		{
			List<BuscadorDeAleatoriedadesEnNombre> l = getBusquedasDeAleatoriedadesEnNombre();
			foreach (BuscadorDeAleatoriedadesEnNombre e in l) {
				if (e.seBuscoCon(I0)) {//nombre, 
					return e;
				}
			}
			return null;
		}
		
		public bool seBuscoAleatoriedadesEnNombre(int I0)//string nombre,
		{
			return getBusquedaDeAleatoriedadesEnNombre(I0) != null;//nombre,
		}
		
		
		
		public List<DatosDeAleatoriedadEnNombre> buscarAleatoriedades(int I0)
		{
			List<DatosDeAleatoriedadEnNombre> d = null;
			if (this.seBuscoAleatoriedadesEnNombre(I0)) {//nombre,
				d = this.getBusquedaDeAleatoriedadesEnNombre(I0).aleatoriedadesEnNombre;//nombre,
			} else {
				BuscadorDeAleatoriedadesEnNombre p = new BuscadorDeAleatoriedadesEnNombre(this.pr);
				d = p.buscarAleatoriedades(I0);//nombre,
				this.addBusquedaDeAleatoriedadesEnNombre(p);
							
			}
			return d;
		}
		
		
		
		
		
		//-----------------------
		
		
		public  List<BuscadorDeEtiquetasEnNombre> getBusquedasEtiquetasEnNombre()
		{
			if (this.etiquetasEnNombre == null) {
				this.etiquetasEnNombre = new List<BuscadorDeEtiquetasEnNombre>();
			}
			return this.etiquetasEnNombre;
		}
		public void addBusquedaDeEtiquetasEnNombre(BuscadorDeEtiquetasEnNombre p)
		{
			this.getBusquedasEtiquetasEnNombre().Add(p);
		}
		
		public BuscadorDeEtiquetasEnNombre getBusquedaDeEtiquetasEnNombre(int I0)//string nombre,
		{
			List<BuscadorDeEtiquetasEnNombre> l = getBusquedasEtiquetasEnNombre();
			foreach (BuscadorDeEtiquetasEnNombre e in l) {
				if (e.seBuscoCon(I0)) {//nombre, 
					return e;
				}
			}
			return null;
		}
		
		public bool seBuscoEtiquetasEnNombre(int I0)//string nombre,
		{
			return getBusquedaDeEtiquetasEnNombre(I0) != null;//nombre,
		}
		
		
		
		public Dictionary<TipoDeEtiquetaDeSerie,List<DatosDeEtiquetaEnNombre>> buscarEtiquetas(int I0)
		{
			Dictionary<TipoDeEtiquetaDeSerie,List<DatosDeEtiquetaEnNombre>> d = null;
			if (this.seBuscoEtiquetasEnNombre(I0)) {//nombre,
				d = this.getBusquedaDeEtiquetasEnNombre(I0).etiquetasEnNombre;//nombre,
			} else {
				BuscadorDeEtiquetasEnNombre p = new BuscadorDeEtiquetasEnNombre(this.pr);
				d = p.buscarEtiquetas(I0);//nombre,
				this.addBusquedaDeEtiquetasEnNombre(p);
							
			}
			return d;
		}
		
		//-----------------------
		
		
		
		
		
		
		public string crearClave(string nombreDeSerie){
			if(this.clavesCreadas==null){
				this.clavesCreadas=new Dictionary<string, string>();
				}
			if(this.clavesCreadas.ContainsKey(nombreDeSerie)){
				return this.clavesCreadas[nombreDeSerie];
			}
			string clave=getCreadorDeClaves().crearClave(nombreDeSerie);
			this.clavesCreadas.Add(nombreDeSerie,clave);
			return clave;
		}
		
		
		
		
		private string getSinSeparacionAlFinal(string texto)
		{
			int indiceFinal = -1;
			for (int i = texto.Length - 1; i > 0; i--) {
				char c = texto.ElementAt(i);
				if (!esCharSeparacion(c)) {
					indiceFinal = i;
				}
			}
			return (indiceFinal > 0) ? subs(texto, 0, indiceFinal) : (indiceFinal == 0 ? texto.ElementAt(0).ToString() : "");
			
		}
		private bool esCharSeparacion(char c)
		{
			return !(Char.IsLetterOrDigit(c));
		}
	}
}
