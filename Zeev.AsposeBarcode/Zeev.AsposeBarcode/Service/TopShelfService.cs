using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Linq;
using Zeev.AsposeBarcode.Library;

namespace Zeev.AsposeBarcode.Service
{
    public class TopShelfService
    {
        //readonly Timer timer;
        //readonly int Interval;


        //Construtor da classe, aqui setamos o intervalo de cada ciclo e iniciamos o timer
        public TopShelfService()
        {
            //Interval = 1000; //De preferência obter o valor do config
            //timer = new Timer(Interval) { AutoReset = true };
            //timer.Elapsed += TimerElapsed;
        }

        public void Start() {  }
        public void Stop() {  }

        //Metodo chamado pela classe program, espera o carregamento do serviço.
        //executa e inicia o timer para aguardar próximo ciclo
        public void Init()
        {
            System.Threading.Thread.Sleep(10000);
            //Execute();
            //Start();
            Task.Run(Execute);
        }

        //Quando o timer atinge o intervalo definido o evento TimerElapsed é acionado
        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            //para o timer para impedir que o evento TimerElapsed seja chamado
            //antes que a execução do método execute esteja concluída
            //Após a execução o timer é iniciado novamente.
            //timer.Stop();
            
            //timer.Start();
        }

        private async Task Execute()
        {
            try
            {
                IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appSettings.json").Build();
                ReadBarcode readBarcode = new ReadBarcode(configuration);
                //await readBarcode.ReadFilesByAspose();
                await readBarcode.ReadBarcodeManager();

            }
            catch(Exception ex)
            {
                Console.WriteLine($"Erro ao processar arquivos {ex.Message}");
            }
            
            
            
        }
    

    }
}
