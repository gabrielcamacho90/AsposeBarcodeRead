using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Topshelf;
using Zeev.AsposeBarcode.Library;
using Zeev.AsposeBarcode.Service;

namespace Zeev.AsposeBarcode
{
    class Program
    {
        static void Main(string[] args)
        {
            var rc = HostFactory.Run(x =>
            {
                //Passar a classe de serviço criada anteriormente
                //no lugar de TopShelfServiseTemplate
                x.Service<TopShelfService>(s =>
                {
                    s.ConstructUsing(name => new TopShelfService()); //construtor
                    s.WhenStarted(tc => tc.Init()); //Metodo Init da classe de serviço
                    s.WhenStopped(tc => tc.Stop()); //Metodo Stop da classe de serviço
                });
                x.RunAsLocalSystem();
                //Descrição do serviço que será exibida no gerenciador de serviços do Windows
                //Recomendável manter em arquivo de configuração
                x.SetDescription("DEV - ZeevDocs - Brasilprev - TESTE BCR");
                //Nome amigável exibido no gerenciador de serviços do Windows
                //Recomendável manter em arquivo de configuração
                x.SetDisplayName("DEV - ZeevDocs - Brasilprev - TESTE BCR");
                //Nome único do serviço, por padrão usa-se NameSpace.NomeDoServiço
                //Recomendável manter em arquivo de configuração
                x.SetServiceName("zeev.docs.aspose.bcr");
            });
            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;           
        }
    }
}
