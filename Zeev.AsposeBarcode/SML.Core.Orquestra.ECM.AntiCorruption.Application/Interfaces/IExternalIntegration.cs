using System;

namespace SML.Core.Orquestra.ECM.AntiCorruption.Application.Interfaces
{
    /// <summary>
    /// Interface para mapeamento de todos os métodos do WebService: ExternalIntegration
    /// </summary>
    public interface IExternalIntegration : IDisposable
    {
        //void ActiveApplication();
        //void ActiveUser();
        //void AssociateUserGroup();
        //void AssociateUserProfile();
        //void Authentication();
        //void ChangePassword();


        /// <summary>
        /// Gera script SQL para cópia da aplicação
        /// </summary>
        /// <param name="appCode">Código da aplicação</param>
        /// <param name="accessGroupID">ID do Grupo de Acesso</param>
        /// <param name="newAppCode">Código da Nova Aplicação</param>
        /// <param name="destinyCompanyID">ID da Compania de Destino</param>
        /// <param name="ownerID">ID do Proprietário</param>
        /// <param name="executeScript">Boleano de Execução do Script Gerado</param>
        string CopyApplication(string appCode, int accessGroupID, string newAppCode, int destinyCompanyID, int ownerID, bool executeScript);

        //void DeactiveApplication();
        //void DeactiveUser();
        //void DeleteHomePage();
        //void DeleteUserProfile();
        //void GetAccessGroups();
        //void GetAccessGroupsByUser();

        /// <summary>
        /// GetApplication
        /// </summary>
        /// <param name="token">Token do Usuário</param>
        /// <param name="appAlias">Alias da Aplicação</param>
        /// <param name="showInactive">Boleano para exibir as plicações inativas</param>
        string GetApplication(string appAlias, bool showInactive);

        //void GetApplicationFields();
        //void GetApplicationPool();
        //void GetAuthenticationDomain();
        //void GetCompaniesToUser();
        //void GetCountries();
        //void GetHomePageByCompany();
        //void GetOwners();
        //void GetProfile();
        //void GetProfileByUser();
        //void GetUser();
        //void GetUserAccessGroup();
        //void GetUserDetails();
        //void Logout();
        //void RemoveUserGroup();
        //void ResetPassword();
        //void SaveCompany();
        //void SaveHomePage();
        //void SaveOwner();
        //void SaveUser();
    }
}
