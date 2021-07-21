using SML.Core.Orquestra.ECM.AntiCorruption.Application.Interfaces;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.UploadFileRequest.Enumerators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Application.Service
{
    public class DocumentControl : IDocumentControl
    {
        private ISilentLoginApplication _silentLoginApplication;
        private IUploadApplication _uploadApplication;
        public DocumentControl(ISilentLoginApplication silentLoginApplication, IUploadApplication uploadApplication)
        {
            _uploadApplication = uploadApplication;
            _silentLoginApplication = silentLoginApplication;
        }
        public long CreateDocumentWithUpload(List<FileInfo> files, DocumentWorkflow preDocumentWorkflow, DocumentWorkflow posDocumentWorkflow, Dictionary<string, string> fields)
        {
            var indId = Preindex(preDocumentWorkflow, fields);

            var listFile = files.Where(x => x != null).Select(x => new KeyValuePair<string, string>(x.Name, x.FullName)).ToList();

            try
            {
                Upload(indId, listFile);

                fields.Clear(); //Não precisa enviar os campos já enviados na Pre_Index
                _silentLoginApplication.Save(indId, posDocumentWorkflow, fields);

                return indId;
            }
            catch (Exception ex)
            {
                try
                {
                    if (indId > 0)
                        _silentLoginApplication.Delete(indId);
                }
                catch (Exception exDel)
                {

                    throw new Exception($"Aconteceu um erro após a préindexação do documento e não foi possível excluir o documento criado. | Erro Raiz: { ex.Message }", exDel);
                }
                throw new Exception("Não foi possível concluir a criação de documento", ex);
            }
        }

        public long CreateDocumentWithUpload(Dictionary<string, string> files, DocumentWorkflow preDocumentWorkflow, DocumentWorkflow posDocumentWorkflow, Dictionary<string, string> fields)
        {
            var indId = Preindex(preDocumentWorkflow, fields);

            try
            {
                Upload(indId, files.ToList());

                fields.Clear(); //Não precisa enviar os campos já enviados na Pre_Index
                _silentLoginApplication.Save(indId, posDocumentWorkflow, fields);

                return indId;
            }
            catch (Exception ex)
            {
                try
                {
                    if (indId > 0)
                        _silentLoginApplication.Delete(indId);
                }
                catch (Exception exDel)
                {
                    throw new Exception($"Aconteceu um erro após a préindexação do documento e não foi possível excluir o documento criado. | Erro Raiz: { ex.Message }", exDel);
                }

                throw new Exception("Não foi possível concluir a criação de documento", ex);
            }
        }
        private void Upload(long indId, List<KeyValuePair<string, string>> listFieldUpdate)
        {
            try
            {
                _uploadApplication.UploadFiles(indId, FileOperationEnum.AddBefore, listFieldUpdate);
            }
            catch (Exception ex)
            {
                throw new Exception("Aconteceu um erro no envio do arquivo.", ex);
            }
        }

        private long Preindex(DocumentWorkflow preDocumentWorkflow, Dictionary<string, string> fields)
        {
            try
            {
                return _silentLoginApplication.PreIndex(preDocumentWorkflow, fields);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar documento", ex);
            }
        }
    }
}
