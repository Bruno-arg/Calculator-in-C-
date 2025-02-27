﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Threading.Channels;
using System.Timers;

namespace Calculadora
{
    class Program
    {
        static void Main(string[] args)
        {
            ColorText("####### CALCULADORA #######", ConsoleColor.DarkYellow);
            bool repeat=true;
            do
            {
                int eleccion = 0;
                eleccion = PedirOperacion();
                Confirmacion(eleccion);
                var valores = PedirValores(eleccion);
                Operar(eleccion, valores.Item1, valores.Item2);
                repeat = Repeat();
            } while (repeat == true);
        }   
        static bool Repeat()
        {
            ClearText();
            ColorText("¿Desea realizar otra operación?", ConsoleColor.Yellow);
            ColorText("1. SI", ConsoleColor.Green);
            ColorText("2. NO", ConsoleColor.Red);
            bool error = false;
            int repetir = 0;
            bool repeat= true;
            do
            {
                try
                {
                    int[] lista12 = { 1, 2 };
                    repetir = int.Parse(Console.ReadLine());
                    if (lista12.Contains(repetir)) error = false;
                    else error = true;
                    if (error == true) ColorText("Opción invalida", ConsoleColor.DarkRed);
                }
                catch
                {
                    error = true;
                    if (error == true) ColorText("Opción invalida", ConsoleColor.DarkRed);
                }
            } while (error == true);
            if (repetir == 1) repeat = true;
            if (repetir == 2)
            {
                ColorText("¡Gracias por usar el programa!", ConsoleColor.Yellow);
                repeat = false;
            }
            ClearText();
            return repeat;
        }
        static void ClearText()
        {
            Console.ReadLine();
            Console.Clear();
        }
        static void Operar(int eleccion, double a, double b)
        {
            double resultado=0;
            switch (eleccion)
            {
                case 1:
                    resultado = Suma(a, b);
                    MayorMenor(resultado);
                    break;
                case 2:
                    resultado = Resta(a, b);
                    MayorMenor(resultado);
                    break;
                case 3:
                    resultado = Division(a, b);
                    MayorMenor(resultado);
                    break;
                case 4:
                    resultado = Multiplicacion(a, b);
                    MayorMenor(resultado);
                    break;
            }
        }
        static void MayorMenor(double resultado)
        {
            if (resultado>0)   
            {
                ColorText("Tu resultado es ", ConsoleColor.Yellow);
                ColorNumber(resultado, ConsoleColor.Green);
            }
            if (resultado < 0)
            {
                ColorText("Tu resultado es ", ConsoleColor.Yellow);
                ColorNumber(resultado, ConsoleColor.Red);
            }
            if (resultado == 0)
            {
                ColorText("Tu resultado es ", ConsoleColor.Yellow);
                ColorNumber(resultado, ConsoleColor.White);
            }
        }
        static void Confirmacion(int eleccion)
        {
            switch (eleccion)
            {
                case 1:
                    ColorText("Suma elegida", ConsoleColor.Yellow);
                    break;
                case 2:
                    ColorText("Resta elegida", ConsoleColor.Yellow);
                    break;
                case 3:
                    ColorText("División elegida", ConsoleColor.Yellow);
                    break;
                case 4:
                    ColorText("Multiplicación elegida", ConsoleColor.Yellow);
                    break;
            }
            Console.WriteLine("");
        }
        static int PedirOperacion()
        {
            int[] listaOperaciones = {1, 2, 3, 4};
            bool error = true;
            int eleccion = 0;
            ColorText("¿Que operación deseas realizar?", ConsoleColor.Yellow);
            Console.WriteLine("1. Suma\n2. Resta\n3. División\n4. Multiplicación\n");
            while(error==true)
            {
                try
                {
                    eleccion = int.Parse(Console.ReadLine());
                    if (listaOperaciones.Contains(eleccion)) error = false;
                    else error = true;
                }
                catch
                {
                    error = true;
                    Console.WriteLine("");
                }
                if (error==true) ColorText("Opción invalida", ConsoleColor.DarkRed);
            }
            return eleccion;
        }
        static (double, double) PedirValores(int eleccion)
        {
            double valor1=0, valor2=0;
            
            bool error = true;
            while (error == true)
            {
                ColorText("Introduce el primer valor:", ConsoleColor.Yellow);
                try
                {
                    error = false;
                    valor1 = double.Parse(Console.ReadLine());
                }
                catch
                {
                    ColorText("Número invalido", ConsoleColor.DarkRed);
                    error = true;
                }
            }
            Console.WriteLine("");

            do
            {
                ColorText("Introduce el segundo valor:", ConsoleColor.Yellow);
                try
                {
                    error = false;
                    valor2 = double.Parse(Console.ReadLine());
                    if (eleccion == 3 && valor2 == 0) error = true;
                    else error = false;
                }
                catch
                {
                    error = true;
                }
                if (error == true) ColorText("Número invalido", ConsoleColor.DarkRed);
            } while (error == true);
            Console.WriteLine("");
            return (valor1, valor2);
        }
        static double Suma(double a, double b) => a + b;
        static double Resta(double a, double b) => a - b;
        static double Division(double a, double b) => a / b;
        static double Multiplicacion(double a, double b) => a * b;
        static void ColorNumber(double text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        static void ColorText(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
