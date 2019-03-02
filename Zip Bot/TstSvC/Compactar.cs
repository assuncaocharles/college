using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Compression;
using System.IO;
using System.Globalization;
using System.Threading;

namespace TstSvC
{
    public static class Compactar
    {
        #region Local Variables

        private static CultureInfo ci = new CultureInfo("de-DE");
        private static DateTime dt = DateTime.Now;
        private static StreamWriter log = new StreamWriter(@"C:\Compac\Log_" + dt.ToString("d", ci) + ".txt", true, Encoding.Unicode);
        private static string Origem = @"C:\Compac";
        private static DirectoryInfo diretorio = new DirectoryInfo(Origem);

        #endregion

        //Método que procura todos arquivos dentro do diretório passado e os envia ao Compress()
        private static void SearchFiles(DirectoryInfo dt)
        {
            bool exist = false;

            foreach (DirectoryInfo dir in dt.GetDirectories())
            {
                if (dir.GetFiles().Count() == 0)
                {
                    log.WriteLine("Pasta \"{0}\" vazia!", dir.FullName);
                    log.Flush();
                }
                else
                {
                    foreach (FileInfo ftc in dir.GetFiles())
                    {
                        if (ftc.Extension != ".gz")
                        {
                            exist = true;
                        }
                    }

                    if (exist == true)
                    {
                        foreach (FileInfo ftc in dir.GetFiles())
                        {
                            if (ftc.Extension == ".gz")
                            {
                                ftc.Delete();
                            }
                        }
                        foreach (FileInfo ftc in dir.GetFiles())
                        {
                            Compress(ftc);
                            ftc.Delete();

                        }
                    }
                    else
                    {
                        log.WriteLine("Todos arquivos em \"{0}\" estão compactados", dir.FullName);
                        log.Flush();
                    }
                    continue;

                }
            }
        }

        //Método que entra dentro dos diretórios até seu ultimo nível e retorna enviando os diretorios à SearchFiles
        private static void SearchSubDirectorys(DirectoryInfo diretorio)
        {
            foreach (DirectoryInfo x in diretorio.GetDirectories())
            {
                if (x.GetDirectories().Count() > 0)
                {
                    if (x.GetFiles().Count() > 0)
                    {
                        SearchFiles(x);
                    }
                    SearchSubDirectorys(x);
                }
                else
                {
                    SearchFiles(x.Parent);


                }
            }
        }

        /// <summary>
        /// Método para iniciar a compactação dos arquivos no diretório padrão.
        /// </summary>
        public static void CompactStart()
        {
            log.WriteLine("\r\n\n" + DateTime.Now.ToString() + "\r\n\n");

            try
            {
                SearchSubDirectorys(diretorio);
            }
            catch (Exception ex)
            {
                log.WriteLine("{0}", ex.Message);
                log.Flush();
            }

        }

        //Método que compacta cada arquivo passado como parâmetro
        private static void Compress(FileInfo fileToCompress)
        {
            try
            {
                using (FileStream originalFileStream = fileToCompress.OpenRead())
                {
                    if ((File.GetAttributes(fileToCompress.FullName) & FileAttributes.Hidden) != FileAttributes.Hidden & fileToCompress.Extension != ".gz")
                    {
                        using (FileStream compressedFileStream = File.Create(fileToCompress.FullName + ".gz"))
                        {
                            using (GZipStream compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                            {
                                originalFileStream.CopyTo(compressionStream);
                                log.WriteLine("Arquivo {0} Zipado de {1} bytes para {2} bytes.", fileToCompress.Name, fileToCompress.Length.ToString(), compressedFileStream.Length.ToString());
                                log.Flush();
                            }
                            compressedFileStream.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.WriteLine("{0}", ex.Message);
                log.Flush();
            }

        }
    }
}
