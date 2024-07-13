using PomtoApplication.DTOs.Autentication.Request;
using PomtoApplication.DTOs.Autentication.Response;
using PomtoApplication.IServices;
using PomtoApplication.Services.WorkRPL;
using PomtoApplication.ShareDto;
using PomtoDomain.Interfaces;
using PomtoDomain.Model;

namespace PomtoApplication.Services.CrudServices
{
    public class AuthServices : IAuthServices
    {
        private readonly IAutenticacao _autenticacao;
        private readonly ITipoLicenca _tipoLicenca;
        private readonly ILicenca _licenca;
        private readonly IEmpresa _empresa;
        private readonly IUsuario _usuario;

        SendEmailService emailService = new();
        ResponseDto<GetLicenseResponseDto> responseLicense = new();
        ResponseNullDto responseNull = new();

        public AuthServices(IAutenticacao autenticacao, IEmpresa empresa, IUsuario usuario, ITipoLicenca tipoLicenca, ILicenca licenca)
        {
            _autenticacao = autenticacao;
            _empresa = empresa;
            _usuario = usuario;
            _tipoLicenca=tipoLicenca;
            _licenca=licenca;
        }

        public async Task<ResponseNullDto> LogoutAsync(LoginRequestDto model)
        {
            var lastLogin = await _autenticacao.GetLastLoginAsync();

            try
            {
                var modelLogin = new Pt_Login
                {
                    ID = lastLogin.ID,
                    CompanyId = lastLogin.CompanyId,
                    UserId = lastLogin.UserId,
                    Status = false,
                    DataAtualizacao = DateTime.Now
                };

                await _autenticacao.SaveLogoutAsync(modelLogin);

                responseNull.Mensagem = "Até a proxima!";
                responseNull.IsSucess = true;
            }
            catch (Exception ex)
            {
                responseNull.Mensagem = ex.Message;
                responseNull.IsSucess = false;
            }

            return responseNull;
        }

        public async Task<ResponseNullDto> RecoveryCredentialsAsync(RecoveryLoginRequestDto model)
        {
            try
            {
                var credentialUser = await _usuario.FindUserAsync(model.EmailUser);

                if (credentialUser != null)
                {
                    var response = await emailService.RecuperarAcessoAsync(credentialUser.UserName, credentialUser.Email, credentialUser.Password);

                    if (response.IsSucess == false)
                    {
                        responseNull.Mensagem = response.Mensagem;
                        responseNull.IsSucess = response.IsSucess;
                    }
                    else
                    {
                        responseNull.Mensagem = response.Mensagem;
                        responseNull.IsSucess = response.IsSucess;
                    }
                }

                var credentialCompany = await _empresa.FindEmpresaAsync(model.EmailUser);

                if (credentialCompany != null)
                {
                    var response = await emailService.RecuperarAcessoAsync(credentialCompany.UserName, credentialCompany.Email, credentialCompany.Password);

                    if (response.IsSucess == false)
                    {
                        responseNull.Mensagem = response.Mensagem;
                        responseNull.IsSucess = response.IsSucess;
                    }
                    else
                    {
                        responseNull.Mensagem = response.Mensagem;
                        responseNull.IsSucess = response.IsSucess;
                    }
                }
                else
                {
                    responseNull.Mensagem = "Não encontramos Email de Usuário no sistema";
                    responseNull.IsSucess = false;
                }
            }
            catch (Exception ex)
            {
                responseNull.Mensagem = ex.Message;
                responseNull.IsSucess = false;
            }

            return responseNull;
        }

        public async Task<ResponseNullDto> SaveItemAsync(CreateLicenseRequestDto model)
        {
            try
            {
                var lastLogin = await _autenticacao.GetLastLoginAsync();
                var tipoLicensa = await _tipoLicenca.FindTypeLicenseAsync(model.TipoLicensa);

                var modelLicensa = new Pt_Licenca
                {
                    SerialNumber = model.NumberLicensa,
                    UsuarioID = lastLogin.UserId,
                    EmpresaID = lastLogin.CompanyId,
                    TipoLicensaID = tipoLicensa.ID,
                    ExpirateDate = model.DataExpiracao,
                    Status = true,
                    DataAtualizacao = DateTime.Now
                };

                await _licenca.SaveAsync(modelLicensa);

                responseNull.Mensagem = "Licença Criada com Sucesso!";
                responseNull.IsSucess = true;
            }
            catch (Exception ex)
            {
                responseNull.Mensagem = ex.Message;
                responseNull.IsSucess = false;
            }

            return responseNull;
        }

        public async Task<ResponseDto<GetLicenseResponseDto>> TakeDataLicenseAsync(string user)
        {
            var lastLogin = await _autenticacao.GetLastLoginAsync();

            if (lastLogin.UserId != 0)
            {
                var licensa = await _licenca.FindListLicenseByUserAsync(lastLogin.UserId, "Usuário");
                var tipoLicensa = await _tipoLicenca.GetByIdAsync(licensa.TipoLicensaID);
                responseLicense.Data = new GetLicenseResponseDto
                {
                    Id = licensa.ID,
                    NumberLicensa = licensa.SerialNumber,
                    TipoLicensa = tipoLicensa.NomeTipoLicensa,
                    DataExpiracao = licensa.ExpirateDate,
                    DataCriacao = licensa.DataCriacao
                };

                responseLicense.IsSucess = true;
            }

            if(lastLogin.CompanyId != 0)
            {
                var licensa = await _licenca.FindListLicenseByUserAsync(lastLogin.CompanyId, "Empresa");
                var tipoLicensa = await _tipoLicenca.GetByIdAsync(licensa.TipoLicensaID);
                responseLicense.Data = new GetLicenseResponseDto
                {
                    Id = licensa.ID,
                    NumberLicensa = licensa.SerialNumber,
                    TipoLicensa = tipoLicensa.NomeTipoLicensa,
                    DataExpiracao = licensa.ExpirateDate,
                    DataCriacao = licensa.DataCriacao
                };
                responseLicense.IsSucess = true;
            }

            return responseLicense;
        }

        public async Task<ResponseNullDto> UpdateItemAsync(UpdateLicenseRequestDto model)
        {
            try
            {
                var lastLogin = await _autenticacao.GetLastLoginAsync();
                var licensa = await _licenca.FindLicenseAsync(model.NumberLicensa);

                var modelLicensa = new Pt_Licenca
                {
                    ID = licensa.ID,
                    SerialNumber = model.NumberLicensa,
                    UsuarioID = lastLogin.UserId,
                    EmpresaID = lastLogin.CompanyId,
                    TipoLicensaID = licensa.TipoLicensaID,
                    ExpirateDate = model.DataExpiracao,
                    Status = true,
                    DataAtualizacao = DateTime.Now
                };

                await _licenca.SaveAsync(modelLicensa);

                responseNull.Mensagem = "Licença Atualizada com Sucesso!";
                responseNull.IsSucess = true;
            }
            catch (Exception ex)
            {
                responseNull.Mensagem = ex.Message;
                responseNull.IsSucess = false;
            }

            return responseNull;
        }

        public async Task<ResponseNullDto> ValidateLoginAsync(LoginRequestDto model)
        {
            try
            {
                var credentialUser = await _usuario.CredentialUserAsync(model.NomeUsuario, model.Password);

                if (credentialUser != null)
                {
                    if (!string.IsNullOrEmpty(credentialUser.UserName) && !string.IsNullOrEmpty(credentialUser.Password))
                    {
                        var license = await _licenca.FindListLicenseByUserAsync(credentialUser.ID, "Usuário");
                        var tipoLicense = await _tipoLicenca.GetByIdAsync(license.TipoLicensaID);

                        var loginModel = new Pt_Login
                        {
                            UserId = credentialUser.ID
                        };

                        await _autenticacao.SaveAsync(loginModel);

                        responseNull.Mensagem = $"Bem-Vindo {model.NomeUsuario}! Licença {tipoLicense.NomeTipoLicensa}";
                        responseNull.IsSucess = true;
                    }
                    else if (string.IsNullOrEmpty(credentialUser.UserName))
                    {
                        responseNull.Mensagem = "UserName incorreto";
                        responseNull.IsSucess = false;
                    }
                    else if (string.IsNullOrEmpty(credentialUser.Password))
                    {
                        responseNull.Mensagem = "Senha incorreta";
                        responseNull.IsSucess = false;
                    }
                }

                var credentialCompany = await _empresa.CredentialEmpresaAsync(model.NomeUsuario, model.Password);

                if (credentialCompany != null)
                {
                    if (!string.IsNullOrEmpty(credentialCompany.UserName) && !string.IsNullOrEmpty(credentialCompany.Password))
                    {
                        var license = await _licenca.FindListLicenseByUserAsync(credentialCompany.ID, "Usuário");
                        var tipoLicense = await _tipoLicenca.GetByIdAsync(license.TipoLicensaID);

                        var loginModel = new Pt_Login
                        {
                            UserId = credentialCompany.ID
                        };

                        await _autenticacao.SaveAsync(loginModel);

                        responseNull.Mensagem = $"Bem-Vindo {model.NomeUsuario}! Licença {tipoLicense.NomeTipoLicensa}";
                        responseNull.IsSucess = true;
                    }
                    else if (string.IsNullOrEmpty(credentialCompany.UserName))
                    {
                        responseNull.Mensagem = "UserName incorreto";
                        responseNull.IsSucess = false;
                    }
                    else if (string.IsNullOrEmpty(credentialCompany.Password))
                    {
                        responseNull.Mensagem = "Senha incorreta";
                        responseNull.IsSucess = false;
                    }
                }
                else
                {
                    responseNull.Mensagem = "Credenciais Inválidas! Porfavor cadastre-se gratuitamente";
                    responseNull.IsSucess = false;
                }
            }
            catch (Exception ex)
            {
                responseNull.Mensagem = ex.Message;
                responseNull.IsSucess = false;
            }

            return responseNull;
        }
    }
}
