using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SML.Core.Orquestra.ECM.AntiCorruption.Application.Interfaces;
using SML.Core.Orquestra.ECM.AntiCorruption.Application.Service;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common;

namespace Zeev.AsposeBarcode.Library
{
    public static class ZeevDocsIntegration
    {
        static string appCode = "TSS8A57ED632303";
        static string ecmAddress = "https://devprojects.orquestraecm.com.br/stable";
        static string token = "5DWmqZ3ZFCr6yJJgxvIkGu06Eoq4EyT4HORR1mMQzGiACkZsFVKlGeewdjtmuoPQ3MFI92i45eFnCUvVtoMkcFY4F0CVCLa6f6Tn9IG/9n/U5Gs5vqYloKZKt4fCqsISMFw/N3Dt8ki7mcpYMldalrYEWvm+YLkQHxiKyZX6fl2RxskBB54uSClm3LKRnNbGOvRzFXbRbJqPeK3+Le0gLq0Fq0p1ZA5/n2+ed8354QVhZc9RUMRRqpkP2FuOIlzpdbjUSn9r7akJqAJIjS4y8ijETnbx583D69HlrfTKY53Y9ONM/vGoj4wza8TfrQfzl+RRQcFyDyxfM3W75oYLLYUFXftqCCxLLFqrkhDun9pOfjwrjIRAIXnM/DOUHFz+dt+f3rCvp4XBXq5nrSrxWQeo7fzuepNzurPQk2b8T3uIVIB9cqNULDvAOsRNqOTVB0r1z3QA/0Mq3UQ6hcFHR4DWYSs5o+5GXeccaP2p5/QBKU12t0FNuf8xhrIIrkuwi5cvwlcY76zr/E0oX0ZzI6XDfA8V/o0Y";
        static string identification= $"Teste Processamento BCR - {DateTime.Now.ToString("dd-MM-yyyy")}";

        public static void SendDocument(Dictionary<string,string> files,DocumentWorkflow preIndexFlow, DocumentWorkflow posIndexFlow, Dictionary<string,string>fields)
        {
            ISilentLoginApplication _silentLogin = new SilentLoginApplication(ecmAddress,appCode,token,identification);
            IUploadApplication _silentLoginUI = new UploadApplication(appCode, token, identification, ecmAddress);
            DocumentControl docCtrl = new DocumentControl(_silentLogin, _silentLoginUI);
            docCtrl.CreateDocumentWithUpload(files, preIndexFlow, posIndexFlow,fields);

           
        }
           
        
    }               
}
