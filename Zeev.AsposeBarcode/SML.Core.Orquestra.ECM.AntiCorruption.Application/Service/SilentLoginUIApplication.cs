using SML.Core.Orquestra.ECM.AntiCorruption.Application.Interfaces;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Common.Enumerators;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.ExecuteActionRequest;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.DTO.Protocol;
using SML.Core.Orquestra.ECM.AntiCorruption.Domain.Helpers.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Application.Service
{
    public class SilentLoginUIApplication : ISilentLoginUIApplication
    {
        #region Properties

        private string _userToken { get; set; }
        private string _adHocUser { get; set; }
        private string _display { get; set; }
        private string _identification { get; set; }
        private string _appCode { get; set; }
        private ISilentLogin _silentLogin { get; set; }

        #endregion

        #region Constructors

        public SilentLoginUIApplication(string appCode, string userToken, string adHocUser, string display, string identification, string ecmAddress)
        {
            ConstructorProperties(appCode, userToken, adHocUser, display, identification, ecmAddress);
        }

        public SilentLoginUIApplication(string appCode, UserLogin userLogin, string adHocUser, string display, string identification, string ecmAddress)
        {
            _userToken = _silentLogin.GetUserToken(userLogin.UserName, userLogin.Password, userLogin.Domain);
            ConstructorProperties(appCode, string.Empty, adHocUser, display, identification, ecmAddress);

        }

        private void ConstructorProperties(string appCode, string userToken, string adHocUser, string display, string identification, string ecmAddress)
        {
            SetProperties(appCode, userToken, adHocUser, display, identification, ecmAddress);
        }


        private void SetProperties(string appCode, string userToken, string adHocUser, string display, string identification, string ecmAddress)
        {
            _appCode = appCode;
            _userToken = userToken;
            _adHocUser = adHocUser;
            _display = display;
            _identification = identification;
            _silentLogin = new SilentLogin(ecmAddress);
        }
        #endregion

        [Obsolete("Utilizar o overload deste método que recebe: ExecuteActionRequestResult como parâmetro.")]
        public string Search(ExecuteActionRequestOption option, List<ExecuteActionRequestField> fields, List<ExecuteActionRequestColumn> columns)
        {
            var eventObj = new ExecuteActionRequestEvent(EventNameEnum.SmlWebSearch);
            eventObj.Action.Name = "SEARCH";
            eventObj.Option = option;
            eventObj.Content.Fields = fields;
            eventObj.Content.Result.Columns = columns;
            var request = GetExecuteActionRootObject(ModuleNameEnum.Search);
            request.Structure.Events.Add(eventObj);
            return _silentLogin.GetProtocol(request.ToXmlString());
        }

        public string Search(ExecuteActionRequestOption option, List<ExecuteActionRequestField> fields, ExecuteActionRequestResult result)
        {
            var eventObj = new ExecuteActionRequestEvent(EventNameEnum.SmlWebSearch);
            eventObj.Action.Name = "SEARCH";
            eventObj.Option = option;
            eventObj.Content.Fields = fields;
            eventObj.Content.Result = result;
            var request = GetExecuteActionRootObject(ModuleNameEnum.WebSearch);
            request.Structure.Events.Add(eventObj);
            var t = request.ToXmlString();
            return _silentLogin.GetProtocol(request.ToXmlString());
        }

        public string Viewer(ExecuteActionRequestOption option, List<ExecuteActionRequestField> fields)
        {
            var eventObj = new ExecuteActionRequestEvent(EventNameEnum.SmlViewrDocument);
            eventObj.Action.Name = "VIEWER";
            eventObj.Option = option;
            eventObj.Content.Fields = fields;
            var request = GetExecuteActionRootObject(ModuleNameEnum.Viewer);
            request.Structure.Events.Add(eventObj);
            return _silentLogin.GetProtocol(request.ToXmlString());
        }

        public string Import(ExecuteActionRequestOption option, List<ExecuteActionRequestField> fields)
        {
            var request = GetImportRequest(option, fields);

            return _silentLogin.GetProtocol(request.ToXmlString());
        }

        private ExecuteActionRequestRoot GetExecuteActionRootObject(ModuleNameEnum action)
        {
            var request = new ExecuteActionRequestRoot(_identification, action);
            request.Header.Module.Name = action;
            request.Header.Application.Code = _appCode;
            request.Header.UserToken = _userToken;
            request.Header.Identification = _identification;
            request.Header.AdHocUser = _adHocUser;
            request.Header.Access.Limit = 50;
            return request;
        }

        public void Dispose()
        {
            if (_silentLogin != null)
                _silentLogin = null;
        }

        #region Private Methods
        private eContent GetImportRequest(ExecuteActionRequestOption option, List<ExecuteActionRequestField> fields)
        {
            var fieldsObj = fields.Select(field => new eContentStructureEventsEventContentField()
            {
                name = field.Name,
                enabled = field.Enabled == TrueFalseEnum.resultTrue ? (byte)1 : (byte)0,
                visible = field.Visible == TrueFalseEnum.resultTrue ? (byte)1 : (byte)0,
                required = field.Required == TrueFalseEnum.resultTrue ? (byte)1 : (byte)0,
                defaultValue = field.Value
            }).ToArray();

            var request = new eContent()
            {
                header = new eContentHeader()
                {
                    module = new eContentHeaderModule() { name = "smlimported" },
                    application = new eContentHeaderApplication() { code = _appCode },
                    userToken = _userToken,
                    access = new eContentHeaderAccess() { limit = 10 },
                    identification = _identification,
                    adHocUser = _adHocUser
                },
                structure = new eContentStructure()
                {
                    events = new eContentStructureEvents()
                    {
                        @event = new eContentStructureEventsEvent()
                        {
                            name = "smlimported",
                            option = new eContentStructureEventsEventOption()
                            {
                                fullscreen = option.Fullscreen == TrueFalseEnum.resultTrue ? true : false,
                                openFloat = option.OpenFloat == TrueFalseEnum.resultTrue ? true : false,
                                openHeader = option.OpenHeader == TrueFalseEnum.resultTrue ? true : false,
                                showFloatMenu = option.ShowFloatMenu == TrueFalseEnum.resultTrue ? true : false,
                                showHeader = option.ShowHeader == TrueFalseEnum.resultTrue ? true : false,
                                viewerTarget = option.ViewerTarget
                            },
                            content = new eContentStructureEventsEventContent()
                            {
                                type = "basic",
                                fields = fieldsObj
                            }
                        }
                    }
                }
            };
            return request;
        }
        #endregion
    }
}