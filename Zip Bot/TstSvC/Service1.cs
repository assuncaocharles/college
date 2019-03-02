using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Threading;

namespace TstSvC
{
    public partial class SVC_Compactar : ServiceBase
    {
        static string arquivo = @"C:\Users\charles\Desktop\Arquivo.txt";
        StreamWriter Escreve;
        Thread T, Y;

        public SVC_Compactar()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Y = new Thread(TGT);
            Y.Start();
           
        }

        protected override void OnStop()
        {
            
        }

        public void TGT()
        {
            if (DateTime.Now.Hour == 10)
            {
                T = new Thread(SvcTh);
                T.Start();
            }
            else 
            {
                Thread.Sleep(TimeSpan.FromHours(1));
            }
            
        }

        public void SvcTh()
        {
            Escreve = new StreamWriter(arquivo, true);
            Escreve.WriteLine("Serviço Iniciado com sucesso: {0}", DateTime.Now);
            Escreve.Flush();
            Compactar.CompactStart();
            Escreve.Close();
            Thread.Sleep(TimeSpan.FromHours(12));
        }
    }
}
