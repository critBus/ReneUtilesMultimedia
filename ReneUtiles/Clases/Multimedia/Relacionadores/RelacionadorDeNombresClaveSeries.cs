/*
 * Created by SharpDevelop.
 * User: Rene
 * Date: 25/9/2021
 * Time: 19:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ReneUtiles.Clases.Multimedia;
using ReneUtiles.Clases.Multimedia.Relacionadores.Saltos;
using ReneUtiles.Clases;


namespace ReneUtiles.Clases.Multimedia.Relacionadores
{
	/// <summary>
	/// Description of RelacionadorDeNombresClaveSeries.
	/// </summary>
	public class RelacionadorDeNombresClaveSeries:ConsolaBasica
	{

		
		
		public RelacionadorDeNombresClaveSeries()
		{
		}
		
		
		public bool estanRelacionados(string a,string b){
			a=a.ToLower();
			b=b.ToLower();
			return estanRelacionados_YaAmbosEnMinusculas(a,b);
		}
		public bool estanRelacionados_YaAmbosEnMinusculas(string a,string b){
//			if(a=="aratakangatari"&&b=="magickaito"){
//				cwl();
//			}
			
			
			int MIN = 7;//"1234567"
			
			if (a==b) {
				return true;
			}
			int lengA=a.Length;
			int lengB=b.Length;
			if(lengA<=MIN||lengB<=MIN){
				return false;//pq la comparacion de que si son == ya se iso arriba 
			}
			int MIN_IGUALES=4;
			if(subs(a,0,MIN_IGUALES)!=subs(b,0,MIN_IGUALES)){
				return false;//pq tienen que ser iguales los primeros caracteres
			}
			
			int cantIguales = 0, cantDiferentes = 0, cantDiferentesSegidos = 0, indiceRuptura = MIN
				, recorrido = 0;
        	bool diferenciaGrave = false, ultimoPasoDelFor = false;//terminoFor
        	//int distanciaRestanteMarcador = 0, distanciaRestanteNombre = 0;
        	int distanciaRestanteA = lengA-MIN_IGUALES, distanciaRestanteB = lengB-MIN_IGUALES;
        	int distanciaRestanteMenor = distanciaRestanteA<distanciaRestanteB?distanciaRestanteA:distanciaRestanteB, 
        	distanciaRestanteMayor = distanciaRestanteA>distanciaRestanteB?distanciaRestanteA:distanciaRestanteB;
        	bool terminoForEnUltimosCaracteresIguales=false;
        	
        	
        	
        	int endDelFor=(lengA <= lengB ? lengA : lengB);
        	for (int i = MIN_IGUALES; i <endDelFor ; i++) {
        		recorrido++;
        		//actualizar distancias restantes
        		distanciaRestanteA--;
        		distanciaRestanteB--;
        		distanciaRestanteMenor--;
        		distanciaRestanteMayor--;
        		
        		
        		if (i == endDelFor - 1) {
		                ultimoPasoDelFor = true;
		        }
        		
        		char charA_Actual=a.ElementAt(i);
        		char charB_Actual=b.ElementAt(i);
        		
        		//cwl("i="+i+" A="+charA_Actual+" B="+charB_Actual);
        		
        		if (charA_Actual==charB_Actual||Utiles.esCharDesconocidoOR(charA_Actual,charB_Actual)) {
        		    cantIguales++;
                    cantDiferentesSegidos = 0;
                    
                    if (ultimoPasoDelFor) {
                    	terminoForEnUltimosCaracteresIguales=true;
                    }
                    continue;
        		}
        		
        		
        		
        		if(!ultimoPasoDelFor){
        			char charA_Anterior=a.ElementAt(i-1);
        			char charB_Anterior=b.ElementAt(i-1);
        			
        			
	        		//The InBeetwen === The InBetween
	        		//caso error humano, cuando existen 2''== seguidos y en uno de los dos se omite uno de estos ''
	        		// en general abbc--abc siendo b->i 
	        		//1: se comprueba la existencia de  2''== seguidos tomando como referencia el i actual y el anterior en ambos
	        		bool charA_Actual_Igual_charA_Anterior=charA_Actual==charA_Anterior;
	        		bool charB_Actual_Igual_charB_Anterior=charB_Actual==charB_Anterior;
	        		if (charA_Actual_Igual_charA_Anterior||charB_Actual_Igual_charB_Anterior) {
	        			
	        			// se decide cual es el de los 2''== y cual no
	        			// y se inicializan las v necesarias
	        			char charDeDiferentesSeguidos_Anterior='-';
	        			char charDeIgualesSeguidos_Actual='-';
	        			if (charA_Actual_Igual_charA_Anterior) {
	        				charDeDiferentesSeguidos_Anterior=charB_Anterior;
	        				charDeIgualesSeguidos_Actual=charA_Actual;
	        			}else{
	        				charDeDiferentesSeguidos_Anterior=charA_Anterior;
	        				charDeIgualesSeguidos_Actual=charB_Actual;
	        			}
	        			//2:se comprueba que el ''anterior del que es diferente, si es == al
	        			// que tiene los 2''== coparandolo con su ''actual
	        			if (charDeDiferentesSeguidos_Anterior==charDeIgualesSeguidos_Actual) {
	        				char charA_Siguiente=a.ElementAt(i+1);
        					char charB_Siguiente=b.ElementAt(i+1);
        					
        					char charDeDiferentesSeguidos_Actual='-';
		        			char charDeIgualesSeguidos_Siguiente='-';
		        			if (charA_Actual_Igual_charA_Anterior) {
		        				charDeDiferentesSeguidos_Actual=charB_Actual;
		        				charDeIgualesSeguidos_Siguiente=charA_Siguiente;
		        			}else{
		        				charDeDiferentesSeguidos_Actual=charA_Actual;
		        				charDeIgualesSeguidos_Siguiente=charB_Siguiente;
		        			}
        					//3: se comprueba si el '' a continuacion de los 2''== es == al '' actual del
	        				// los tiene diferente
	        				if (charDeDiferentesSeguidos_Actual==charDeIgualesSeguidos_Siguiente) {
	        					char charA_Anterior2=a.ElementAt(i-2);
        						char charB_Anterior2=b.ElementAt(i-2);
        						
        						char charDeIgualesSeguidos_Anterior2='-';
			        			if (charA_Actual_Igual_charA_Anterior) {
			        				charDeDiferentesSeguidos_Anterior=charB_Anterior;
			        				charDeIgualesSeguidos_Anterior2=charA_Anterior2;
			        			}else{
			        				charDeDiferentesSeguidos_Anterior=charA_Anterior;
			        				charDeIgualesSeguidos_Anterior2=charB_Anterior2;
			        			}
	        					
	        					//4:se comprueba si el ''-2 (abbc seria a,con 2b->i) es == al
								// '' anterior al que los tiene diferente (abc seria a ,con b->i)
								if (charDeDiferentesSeguidos_Anterior==charDeIgualesSeguidos_Anterior2) {
									
									if (charA_Actual_Igual_charA_Anterior) {
			        					a = a.Substring(1);
			        					lengA--;
			        					distanciaRestanteA--;
				        			}else{
				        				b = b.Substring(1);
				        				lengB--;
				        				distanciaRestanteB--;
				        			}
			                        cantIguales++;
			                        cantDiferentesSegidos = 0;
			                        
			                        //actualizar valores fijos
			                        endDelFor=(lengA <= lengB ? lengA : lengB);
			                        distanciaRestanteMenor = distanciaRestanteA<distanciaRestanteB?distanciaRestanteA:distanciaRestanteB;
        							distanciaRestanteMayor = distanciaRestanteA>distanciaRestanteB?distanciaRestanteA:distanciaRestanteB;
        	
			                        
			                        continue;
								}
																
	        				}
	        				
	        			}
	        			
	        			
	        			
	        		}//fin caso abbc abc
        		//aun dentro de if(!ultimoPasoDelFor)
        		
        		}
        		//ya no hay casos que eviten que sean los actuales diferentes
        		cantDiferentes++;
                cantDiferentesSegidos++;
                
               
                
                
                
                if (cantDiferentesSegidos == 3) {
                	// como el informe final se basa en los que fueron iguales hasta el 
                	//indice de ruptura (~~ el ultimo ''== )
                	// y a este lo voy a desplasar 3'' atras pq ya son muchos '' diferences seguidos (3'')
                	cantDiferentes -= 3;
                    indiceRuptura = i - 3;
                    if (recorrido < 5) {
                        return false;//pq el recorrido fue muy corto con muchas diferencias
                    }
                    
                    if (recorrido < 10) {
                        if (distanciaRestanteMenor > 0) {//pendiente pq aun no se si usar marcador
                            return false;//la idea seria que si el marcador aun le quedan caracteres 
                            //pero todavia despues de este largo recorrido(10) no se ha dado una respuesta positiva
                            // no vale la pena seguir buscando, recordar que aqui ya existieron almenos 3''!= seguidos
                        }
                    }
                    
                    if (recorrido < 14) {
                    	
                        if (((recorrido + MIN_IGUALES) < distanciaRestanteA / 2) || ((recorrido + MIN_IGUALES) < distanciaRestanteB / 2)) {
                            return false;
                        }
                    }
                    break;
                    
                }//fin del if (cantDiferentesSegidos == 3) 
        		
                if (cantDiferentes == 5) {//ver que son diferentes no seguidos
                    return false; //resultado negativo principal
                }
        		
                
                 if (cantDiferentesSegidos == 2) {
                	
                	//pq no se admiten dos diferencias graves seguidas (diferenciaGrave = 2''!=)
                	if (diferenciaGrave) {
                        cantDiferentes -= 2;//pq para la conclusión  final solo se toman en cuenta los resultados hasta el indice de ruptura, y este se va a acortar
                        indiceRuptura = i - 2;//indice de ruptura (~~ el ultimo ''== )
                        break;
                    }
                	//caso error humano: donde por omitir o gregar un caracter hay diferencia
                	//principalmente donde los 4'' actuales(comensando por i) de uno son
                	// == a los 4'' desde i-1 del otro
                	//la solucion: eliminar un '' del primero (el ''0,que oviamente no es ninguno de estos)
                	//hay que comprobar si tienen caracteres a continuacion suficientes para comparar,para
                	//que salte uno error
                	// se comprueba para evitar marcar un error grave innecesario 
                	bool esA4Seguidos=false;
                	if(i>0
                		 &&(
                		 (distanciaRestanteA>2&&distanciaRestanteB>1&&(esA4Seguidos=subs(b,i - 1, i + 2)==subs(a,i, i + 3)))
                		||(distanciaRestanteA>1&&distanciaRestanteB>2&&subs(a,i - 1, i + 2)==subs(b,i, i + 3))
                	)
                		  ){
                		
                		if (esA4Seguidos) {
                			a=a.Substring(1);
                			lengA--;
                			distanciaRestanteA--;
                		}else{
                			b=b.Substring(1);
                			lengB--;
                			distanciaRestanteB--;
                		}
                		cantDiferentesSegidos = 0;
                		i++;//pq lo deja donde esta el ultimo ''== dando la oportunidad a un final positivo y despues  es donde podrian ser diferentes 
                        cantDiferentes--;
                        
                        //actualizar valores fijos
			            endDelFor=(lengA <= lengB ? lengA : lengB);
			            distanciaRestanteMenor = distanciaRestanteA<distanciaRestanteB?distanciaRestanteA:distanciaRestanteB;
        				distanciaRestanteMayor = distanciaRestanteA>distanciaRestanteB?distanciaRestanteA:distanciaRestanteB;
                	}else {
                		//no se pudo evitar marcar como diferencia grave
                        diferenciaGrave = true;
                    }//fin del if caso 4''
                }else{
                //hay cualquier cantidad de diferentes seguidos,exepto 2,3,5 (creo que solo 1 y 4)
                
                if (cantDiferentesSegidos==1) {
                	//caso error humano: donde por omitir o gregar un caracter hay diferencia
                	//principalmente donde los 4'' actuales(comensando por i) de uno son
                	// == a los 4'' desde i+1 del otro
                	//la solucion: eliminar un '' del segundo (el ''0,que oviamente no es ninguno de estos)
                	//hay que comprobar si tienen caracteres a continuacion suficientes para comparar,para
                	//que salte uno error
                	bool esA4Seguidos=false;
                	if(
                		  (distanciaRestanteA>2&&distanciaRestanteB>3&&(esA4Seguidos=subs(b,i + 1, i + 4)==subs(a,i, i + 3)))
                		||(distanciaRestanteA>3&&distanciaRestanteB>2&&subs(a,i + 1, i + 4)==subs(b,i, i + 3))
                		  ){
                		
                		if (esA4Seguidos) {//b se recorta aqui, pq la solucion es recortar el que no tiene los 4'' a partir del actual
                			b=b.Substring(1);
                			lengB--;
                			distanciaRestanteB--;
                		}else{
                			a=a.Substring(1);
                			lengA--;
                			distanciaRestanteA--;
                		}
                		cantDiferentesSegidos = 0;
                		i+=2;//pq lo deja donde esta el ultimo ''== dando la oportunidad a un final positivo y despues  es donde podrian ser diferentes
                        cantDiferentes--;
                        
                        //actualizar valores fijos
			            endDelFor=(lengA <= lengB ? lengA : lengB);
			            distanciaRestanteMenor = distanciaRestanteA<distanciaRestanteB?distanciaRestanteA:distanciaRestanteB;
        				distanciaRestanteMayor = distanciaRestanteA>distanciaRestanteB?distanciaRestanteA:distanciaRestanteB;
                	}//fin del if caso 4'' 2da vez
                }//fin del if (cantDiferentesSegidos==1) 
                	
                	
                }//fin del if (cantDiferentesSegidos == 2) -- arriba esta dentro del else
                
                
                
        		
        	
        	}//fin del for
			
//        	cwl("cantIguales="+cantIguales);
//        	cwl("cantDiferentes="+cantDiferentes);
//        	cwl("cantDiferentesSegidos="+cantDiferentesSegidos);
//        	cwl("indiceRuptura="+indiceRuptura);
//        	cwl("recorrido="+recorrido);
//        	cwl("indiceRuptura="+indiceRuptura);
        	
        	//Conclusión 
        	
        	if (lengA!= lengB) {
        		
        		if ( cantDiferentes >= 2) {
	        		//si no fue un recorrido demasiado extenso pero
	        		// mas de la mitad del reccorido fueron ''diferentes -> no hay relacion
		            if (recorrido < 10 || !(cantDiferentes < recorrido / 2)) {
		                return false;
		            }
	        	}
        		
        		if (terminoForEnUltimosCaracteresIguales) {
        			if (recorrido < 3 && distanciaRestanteMayor > 1) {
		                return false;
		            }
		            if (recorrido < 5 && cantDiferentes > 0 && distanciaRestanteMayor > 4) {
		                return false;
		            }
        			int lengMayor=lengA>lengB?lengA:lengB;
		            if (distanciaRestanteMayor > lengMayor * 2) {
		                return false;
		            }
        		}
        	}else{// los leng son iguales
	        	if (diferenciaGrave||cantDiferentes>=3) {
	        			return false;
	        	}
        		
        	}
        	
        	
        	
        	
			
        	
        	
				return true;
			}
	}
}
