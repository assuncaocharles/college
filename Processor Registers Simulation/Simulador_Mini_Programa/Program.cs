using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace Simulador_Mini_Programa
{ 

    class Program
    {       
        static void Main(string[] args)
        {
            if (File.Exists("Relatorio_de_Erros.txt"))            
                 File.Delete("Relatorio_de_Erros.txt");
            
            StreamReader Arquivo = new StreamReader("Comandos.txt", System.Text.Encoding.UTF8);
            StreamWriter Erros = new StreamWriter("Relatorio_de_Erros.txt", true);

            try
            {

                string Linha = "";

                while (!Arquivo.EndOfStream)
                {
                    Linha = Arquivo.ReadLine();
                    

                    if (Linha != null)
                    {
                        Linha = Linha.ToUpper();
                        Linha = Linha.Replace(",","").Replace(";","");
                        string[] CMD = Linha.Split(' ');


                        if (CMD.Count() == 3)
                        {
                            if (CMD[0] == "LOAD" && CMD[1].Substring(0, 1) != "[" && CMD[1].Substring(0, 1) != "R")
                            {
                                try
                                {
                                    Type Y = typeof(MiniPrograma);
                                    MethodInfo X = Y.GetMethod(CMD[0], BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(int), typeof(string) }, null);
                                    X.Invoke(null, new object[] { int.Parse(CMD[1]), CMD[2] });
                                }
                                catch (Exception ex)
                                {
                                    Erros.WriteLine("Comando errado na linha {0}", MiniPrograma.Cont++);
                                }
                            }
                            else
                            {
                                try
                                {
                                    Type Y = typeof(MiniPrograma);
                                    MethodInfo X = Y.GetMethod(CMD[0], BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(string), typeof(string) }, null);
                                    X.Invoke(null, new object[] { CMD[1], CMD[2] });
                                }
                                catch (Exception ex)
                                {
                                    Erros.WriteLine("Comando errado na linha {0}", MiniPrograma.Cont++);
                                }
                            }
                        
                        }
                        else if (CMD.Count() == 2)
                        {
                            try
                            {
                                Type Y = typeof(MiniPrograma);
                                MethodInfo X = Y.GetMethod(CMD[0], BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(string) }, null);
                                X.Invoke(null, new object[] { CMD[1]});
                            }
                            catch (Exception ex)
                            {
                                Erros.WriteLine("Comando errado na linha {0}", MiniPrograma.Cont++);
                            }

                          
                        }
                        else
                        {
                            Console.WriteLine("Comando Inválido");
                            Erros.WriteLine("Comando errado na linha {0}", MiniPrograma.Cont++);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Erros.WriteLine("Comando errado na linha {0}", MiniPrograma.Cont++);
            }
            finally
            {
                MiniPrograma.WriteLog();
                Erros.Close();
                Console.WriteLine("Concluído");
                Console.ReadKey();
            }
        }
    }
}
