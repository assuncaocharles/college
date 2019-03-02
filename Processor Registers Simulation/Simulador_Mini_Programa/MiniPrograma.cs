using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace Simulador_Mini_Programa
{
    public static class MiniPrograma
    {
        #region Attributes

        private static int[] Memoria = new int[100];
        private static int[] Registradores = new int[11];
        private static int MDR, MAR, H;
        public static int Cont = 0;
        private static StreamWriter ArquivoT = new StreamWriter("Temp.txt", true);
        private static StreamWriter Log;

        #endregion

        #region ProgramFunctions

        private static void WriteSteps()
        {
            ArquivoT.Close();
            StreamReader ArquivoTemp = new StreamReader("Temp.txt", System.Text.Encoding.UTF8);
            Log.WriteLine("\r\nPassos:");
            while (!ArquivoTemp.EndOfStream)
            {
                string LN = ArquivoTemp.ReadLine();
                Log.WriteLine("{0}", LN);
            }
            ArquivoTemp.Close();
            File.Delete("Temp.txt");
        }

        public static void WriteLog()
        {

            if (File.Exists("Log.txt"))
            {
                File.Delete("Log.txt");
            }

            Log = new StreamWriter("Log.txt", true);

            Log.WriteLine("");
            Log.WriteLine("===Memória===\n\r");
            for (int i = 0; i < Memoria.Length; i++)
            {
                Log.WriteLine("\r\n Endereço: {0} - Conteúdo: {1}", i, Memoria[i]);
            }
            Log.WriteLine("");
            Log.WriteLine("\r\n===Registradores=== \n\r");
            for (int i = 1; i < Registradores.Length; i++)
            {
                Log.WriteLine("\n\rR{0} - Conteúdo: {1}", i, Registradores[i]);
            }
            Log.WriteLine("");
            Log.WriteLine("\r\nValor de H: {0}", H);

            WriteSteps();

            Log.Close();
        }

        #endregion

        #region MethodsToManipulation

        /// <summary>
        /// Método para mover valor de da memória para registrador
        /// </summary>
        /// <param name="Endereco">Endereço da memória do qual será movido o valor</param>
        /// <param name="Registrador">Registrador destino</param>
        public static void MOV(string Endereco, string Registrador)
        {
            ArquivoT.WriteLine("\r\n");    
            string EnderecoSub = Endereco.Replace("[", "").Replace("]", "");
            MAR = int.Parse(EnderecoSub);
            int R = int.Parse(Registrador.Replace("R",""));
            MDR = Memoria[MAR];
            Registradores[R] = 0;
            Registradores[R] = MDR;
            ArquivoT.WriteLine("Move o valor {0} para MAR \r\nMove o valor {1} da memória[{2}] para MDR \r\nMove o Valor de MDR para o registrador[{3}]\r\n\r\n ", EnderecoSub, Memoria[MAR], MAR, R);
        }

        /// <summary>
        /// Move um valor para o registrador H
        /// </summary>
        /// <param name="Operando">Registrador ou memória com  valor a ser movido</param>
        public static void MOV(string Operando)
        {
            ArquivoT.WriteLine("\r\n");
            if (Operando.Substring(0, 1) == "R")
            {
                int R = int.Parse(Operando.Replace("R", ""));
                H += Registradores[R];
                ArquivoT.WriteLine("Move o valor {0} do registrador[{1}] para H\r\n\r\n", Registradores[R], R);
            }
            else
            {
                string EnderecoSub = Operando.Replace("[", "").Replace("]", "");
                MAR = int.Parse(EnderecoSub);
                MDR = Memoria[MAR];
                H += MDR ;
                ArquivoT.WriteLine("Move {0} para MAR \r\n Move o valor {1} da memória[{2}] para MDR \r\n Move o valor de MDR para H\r\n\r\n", EnderecoSub, Memoria[MAR], MAR);
            }

        }

        /// <summary>
        /// Carrega um valor à um espaço de memória
        /// </summary>
        /// <param name="Operando">Valor constante a ser carregado</param>
        /// <param name="Endereco">Endereço da memória para receber o valor</param>
        public static void LOAD(int Operando, string Endereco)
        {
            ArquivoT.WriteLine("\r\n");
            string EnderecoSub = Endereco.Replace("[", "").Replace("]", "");
            MAR = int.Parse(EnderecoSub);
            MDR = Operando;
            Memoria[MAR] = 0;
            Memoria[MAR] = MDR;
            ArquivoT.WriteLine("Carrega {0} para MAR \r\nCarrega o valor {1} para MDR \r\nCarrega o valor {2} de MDR para a memória[{3}]\r\n\r\n", EnderecoSub, Operando, MDR, MAR);

        }

        /// <summary>
        /// Carrega um valor da memória à um registrador destino
        /// </summary>
        /// <param name="Endereco">Endereço da memória do qual o valor será carregado</param>
        /// <param name="Registrador">Registrador destino</param>
        public static void LOAD(string Endereco, string Registrador)
        {
            ArquivoT.WriteLine("\r\n");
            string EnderecoSub = Endereco.Replace("[", "").Replace("]", "");
            MAR = int.Parse(EnderecoSub);
            MDR = Memoria[MAR];
            int R = int.Parse(Registrador.Replace("R", ""));
            Registradores[R] = 0;
            Registradores[R] = MDR;
            ArquivoT.WriteLine("Carrega {0} para MAR \r\nCarrega o valor {1} da memória[{2}] para MDR \r\nCarrega o valor de MDR para o registrador[{3}]\r\n\r\n", EnderecoSub, Memoria[MAR], MAR, R);
        }

        /// <summary>
        /// Adiciona um determinado valor  à H
        /// </summary>
        /// <param name="Operando">Endereço da memória, registrador ou valor inteiro à ser adicionado</param>
        public static void ADD(string Operando)
        {
            ArquivoT.WriteLine("\r\n");
            if (Operando.Substring(0,1) == "R")
            {
                int R = int.Parse(Operando.Replace("R",""));
                H += Registradores[R];
                ArquivoT.WriteLine("Adiciona o valor {0} do registrador[{1}] para H\r\n\r\n", Registradores[R], R);
            }
            else if (Operando.Substring(0, 1) == "[")
            {
                string EnderecoSub = Operando.Replace("[", "").Replace("]", "");
                MAR = int.Parse(EnderecoSub);
                MDR = Memoria[MAR];
                H += Memoria[MAR];
                ArquivoT.WriteLine("Adiciona {0} em MAR \r\nAdciona o valor {1} em MDR\r\nAdiciona o valor de MDR em H\r\n\r\n", MAR, MDR);
            }
            else 
            {
                int EnderecoSub = int.Parse(Operando);
                H += EnderecoSub;
                ArquivoT.WriteLine("Adiciona o valor {0} para H\r\n\r\n", EnderecoSub);

            }
        }

        /// <summary>
        /// Adiciona um valor à um Registrador destino
        /// </summary>
        /// <param name="Registrador">Registrador Destino</param>
        /// <param name="Operando">Endereço de memória, registrador ou valor constante à ser adicionado no registrador</param>
        public static void ADD(string Registrador, string Operando)
        {
            ArquivoT.WriteLine("\r\n");
            int R = int.Parse(Registrador.Replace("R",""));

            if (Operando.Substring(0,1) == "R")
            {
                int Res = int.Parse(Operando.Replace("R",""));
                Registradores[R] += Registradores[Res];
                ArquivoT.WriteLine("Adiciona o valor {0} do registrador[{1}] para o registrador[{2}]\r\n\r\n", Registradores[Res], Res, R);
            }
            else if (Operando.Substring(0, 1) == "[")
            {
                string EnderecoSub = Operando.Replace("[", "").Replace("]", "");
                MAR = int.Parse(EnderecoSub);
                MDR = Memoria[MAR];
                Registradores[R] += MDR;
                ArquivoT.WriteLine("Adiciona o valor {0} em MAR \r\nAdiciona o valor {1} em MDR \r\nAdiciona o valor de MDR no Registrador {2}\r\n\r\n", MAR, MDR, R);
            }
            else
            {

                Registradores[R] += int.Parse(Operando);
                ArquivoT.WriteLine("Adiciona o valor {0} para o Registrador {1}\r\n\r\n", Operando, R);

            }
        }

        #endregion
    }
}
