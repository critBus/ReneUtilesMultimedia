/*
 * Creado por SharpDevelop.
 * Usuario: Rene
 * Fecha: 4/8/2022
 * Hora: 16:05
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
namespace ReneUtiles.Clases.Multimedia.Series.Procesadores.Buscadores
{
	/// <summary>
	/// Description of BuscadorDeTipoDeNombre.
	/// </summary>
	public class BuscadorDeTipoDeNombre:ConsultorDeDatosEnNombre
	{
		public BuscadorDeTipoDeNombre(ProcesadorDeNombreDeSerie pr):base(pr)
		{
		}
		
		
		
		
		public int esNombreConNumeroAlPrincipio_indiceFinal(string texto, int numero, int indiceInicial){
			CondicionIgnorarNumeroEspecifico[] C=getConf().NombresConNumeroAlPrincipio;
			string s=subs(texto,indiceInicial);
			for (int i = 0; i < C.Length; i++) {
				CondicionIgnorarNumeroEspecifico c=C[i];
				if(c.Numero>numero){
					break;
				}else if(c.Numero==numero){
					Match m=getRe().Re_nombresConNumeroAlPrincipio.ReInicial.Match(s);
					if(m.Success){
						//return (m.Length-1)+indiceInicial;
						return (m.Length)+indiceInicial;
					}
				}
			}
			return -1;
		}
		
		public int esNombreConNumeroAlFinal_buscarDesdeElPrincipio_indiceFinal(string texto, int numero, int indiceInicial){
			CondicionIgnorarNumeroEspecifico[] C=getConf().NombresConNumeroAlFinal;
			string s=subs(texto,indiceInicial);
			for (int i = 0; i < C.Length; i++) {
				CondicionIgnorarNumeroEspecifico c=C[i];
				if(c.Numero>numero){
					break;
				}else if(c.Numero==numero){
					Match m=getRe().Re_nombresConNumeroAlFinal.ReInicial.Match(s);
					if(m.Success){
						//return (m.Length-1)+indiceInicial;
						return (m.Length)+indiceInicial;
					}
				}
			}
			return -1;
		}
		public int esNombreConNumeroAlFinal_buscarDesdeElFinal_indiceInicial(string texto, int numero, int indiceInicialDelNumero){
			CondicionIgnorarNumeroEspecifico[] C=getConf().NombresConNumeroAlFinal;
			int indiceFinal=indiceInicialDelNumero+numero.ToString().Length;
			string s=subs(texto,0,indiceFinal);
			
			for (int i = 0; i < C.Length; i++) {
				CondicionIgnorarNumeroEspecifico c=C[i];
				int numeroC=c.Numero;
				if(numeroC>numero){
					break;
				}else
					if(c.Numero==numero){
//					if(s=="American Horror History S0"){
//							cwl("aqui");
//							cwl(getRe().Re_nombresConNumeroAlFinal.ReFinal);
//						}
					Match m=getRe().Re_nombresConNumeroAlFinal.ReFinal.Match(s);
					if(m.Success){
						return m.Index;//(m.Length-1)+indiceInicial;
					}
				}
			}
			return -1;
		}
		
		public Match esNombresConUnNumeroInterno_m(string texto, int numero, int indiceInicial){
			CondicionIgnorarNumeroEspecificoRodeadoPor[] C=getConf().NombresConUnNumeroInterno; 
			//string s=subs(texto,indiceInicial);
			for (int i = 0; i < C.Length; i++) {
				CondicionIgnorarNumeroEspecificoRodeadoPor c=C[i];
				if(c.Numero>numero){
					break;
				}else if(c.Numero==numero){
                    //if (texto == "Virtual_Hero_-_1x04-medium")
                    //{
                    //    //r da "" vacia plq hay que buscar donde se inicializa pq no lo esta iniciando bien
                    //    string r = getRe().Re_nombresConUnNumeroInterno.Re.ToString();
                    //    cwl(getRe().Re_nombresConUnNumeroInterno.Re);
                    //    cwl("aqui");
                    //}

                    Match m=getRe().Re_nombresConUnNumeroInterno.Re.Match(texto);
					if(m.Success){
                        
						return m;
					}
				}
			}
			return null;
		}
		
		
		public int esNombreRodeadosDeNumeros_buscarDesdeElPrincipio_indiceFinal(string texto, int numero, int indiceInicial){
			CondicionIgnorarNumerosEspecificosSeparadosPor[] C=getConf().NombresRodeadosDeNumeros;
			string s=subs(texto,indiceInicial);
			for (int i = 0; i < C.Length; i++) {
				CondicionIgnorarNumerosEspecificosSeparadosPor c=C[i];
				if(c.NumeroInicial>numero){
					break;
				}else if(c.NumeroInicial==numero){
					Match m=getRe().Re_nombresRodeadosDeNumeros.ReInicial.Match(s);
					if(m.Success){
						//return (m.Length-1)+indiceInicial;
						return (m.Length)+indiceInicial;
					}
				}
			}
			return -1;
		}
		public int esNombreRodeadosDeNumeros_buscarDesdeElFinal_indiceInicial(string texto, int numero, int indiceInicialDelNumero){
			CondicionIgnorarNumerosEspecificosSeparadosPor[] C=getConf().NombresRodeadosDeNumeros;
			int indiceFinal=indiceInicialDelNumero+numero.ToString().Length;
			string s=subs(texto,0,indiceFinal);
			for (int i = 0; i < C.Length; i++) {
				CondicionIgnorarNumerosEspecificosSeparadosPor c=C[i];
				 if(c.NumeroFinal==numero){
					Match m=getRe().Re_nombresRodeadosDeNumeros.ReFinal.Match(s);
					if(m.Success){
						return m.Index;//(m.Length-1)+indiceInicial;
					}
				}
			}
			return -1;
		}
		
		public int esNombreConNumerosAlPrincipio_indiceFinal(string texto, int numero, int indiceInicial){
            //if (texto == "12 Monkeys")
            //{
            //    cwl("aqui");
            //}
            CondicionIgnorarNumerosEspecificos[] C=getConf().NombresConNumerosAlPrincipio;
			string s=subs(texto,indiceInicial);
			for (int i = 0; i < C.Length; i++) {
				CondicionIgnorarNumerosEspecificos c=C[i];
				if(c.Numeros[0]>numero){
					break;
				}else if(c.Numeros[0]==numero){
					Match m=getRe().Re_nombresConNumerosAlPrincipio.ReInicial.Match(s);
					if(m.Success){
						//return (m.Length-1)+indiceInicial;
						return (m.Length)+indiceInicial;
					}
				}
			}
			return -1;
		}
		
		
		public int esNombreNumericosMultiples_buscarDesdeElPrincipio_indiceFinal(string texto, int numero, int indiceInicial){
			CondicionIgnorarConjuntoDeNumeros[] C=getConf().NombresNumericosMultiples;
			string s=subs(texto,indiceInicial);
			for (int i = 0; i < C.Length; i++) {
				CondicionIgnorarConjuntoDeNumeros c=C[i];
				if(c.Numeros[0]>numero){
					break;
				}else if(c.Numeros[0]==numero){
					Match m=getRe().Re_nombresNumericosMultiples.ReInicial.Match(s);
					if(m.Success){
						//return (m.Length-1)+indiceInicial;
						return (m.Length)+indiceInicial;
					}
				}
			}
			return -1;
		}
		public int esNombreNumericosMultiples_buscarDesdeElFinal_indiceInicial(string texto, int numero, int indiceInicialDelNumero){
			CondicionIgnorarConjuntoDeNumeros[] C=getConf().NombresNumericosMultiples;
			int indiceFinal=indiceInicialDelNumero+numero.ToString().Length;
			string s=subs(texto,0,indiceFinal);
			for (int i = 0; i < C.Length; i++) {
				CondicionIgnorarConjuntoDeNumeros c=C[i];
				if(c.Numeros[c.Numeros.Length-1]==numero){
					Match m=getRe().Re_nombresNumericosMultiples.ReFinal.Match(s);
					if(m.Success){
						return m.Index;//(m.Length-1)+indiceInicial;
					}
				}
			}
			return -1;
		}
		
	}
}
