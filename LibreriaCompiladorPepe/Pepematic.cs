using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace LibreriaCompiladorPepe
{
    public class Pepematic
    {
        public int[] memoria = new int[100];
        public int linea = 0;
        public int acumulador = 0;
        public int instruccionActual, codOperacion, operando;
        const int LEER = 10, ESCRIBIR = 11;
        const int CARGAR = 20, ALMACENAR = 21;
        const int SUMAR = 30, RESTAR = 31, MULTIPLICAR = 32, DIVIDIR = 33;
        const int SALTAR = 40, SALTARNEG = 41, SALTARCERO = 42, ALTO = 43;
        public Pepematic(string rutaArchivo)//carga archivos *.slmp
        {
            using (StreamReader archivo = new StreamReader(rutaArchivo))
            {
                int lineaActual = 0;
                while (!archivo.EndOfStream)
                    memoria[lineaActual++] = int.Parse(archivo.ReadLine());
            }
        }

        public void Ejecutar()
        {
            linea = 0;
            while (linea >= 0)
            {
                instruccionActual = memoria[linea++];
                selectorInstruccion(instruccionActual);
            }
        }

        private void selectorInstruccion(int regActual)
        {
            codOperacion = regActual / 100;// 1012
            operando = regActual % 100;
            switch (codOperacion)
            {
                case LEER:
                    Console.Write("Ingrese un numero entero: ");
                    memoria[operando] = int.Parse(Console.ReadLine());
                    break;
                case ESCRIBIR:
                    Console.WriteLine("> " + memoria[operando]);
                    break;
                case CARGAR:
                    acumulador = memoria[operando];
                    break;
                case ALMACENAR:
                    memoria[operando] = acumulador;
                    break;
                case SUMAR:
                    acumulador += memoria[operando];
                    break;
                case RESTAR:
                    acumulador -= memoria[operando];
                    break;
                case MULTIPLICAR:
                    acumulador *= memoria[operando];
                    break;
                case DIVIDIR:
                    if (memoria[operando] != 0)
                        acumulador /= memoria[operando];
                    else
                    {
                        linea = -1;
                        Console.WriteLine("Error: division por 0");
                    }

                    break;
                case SALTAR:
                    linea = operando;
                    break;
                case SALTARNEG:
                    if (acumulador < 0)
                        linea = operando;
                    break;
                case SALTARCERO:
                    if (acumulador == 0)
                        linea = operando;
                    break;
                case ALTO:
                    linea = -1;
                    Console.WriteLine("Ejecución concluida");
                    break;
            }
        }
    }
}
