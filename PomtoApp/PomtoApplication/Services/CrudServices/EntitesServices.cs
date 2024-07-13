using PomtoApplication.DTOs.Entites.Request;
using PomtoApplication.DTOs.Entites.Response;
using PomtoApplication.Helpers;
using PomtoApplication.IServices;
using PomtoApplication.ShareDto;
using PomtoDomain.Interfaces;
using PomtoDomain.Model;

namespace PomtoApplication.Services.CrudServices
{
    public class EntitesServices : IEntitesServices
    {
        private readonly IAutenticacao _autenticacao;
        private readonly IEmpresa _empresa;
        private readonly ITelefoneEmpresa _telefoneEmpresa;

        ResponseDto<GetPerfilEntitesResponseDto> responsePerfilDto = new();
        ResponseDto<GetEntitesResponseDto> responseEntitesDto = new();
        ResponseHelper responseHelper = new();
        ResponseNullDto responseNull = new();

        public EntitesServices(IAutenticacao autenticacao, IEmpresa empresa, ITelefoneEmpresa telefoneEmpresa)
        {
            _autenticacao=autenticacao;
            _empresa=empresa;
            _telefoneEmpresa=telefoneEmpresa;
        }

        public async Task<ResponseDto<GetEntitesResponseDto>> GetItemAsync()
        {
            try
            {
                var login = await _autenticacao.GetLastLoginAsync();
                var company = await _empresa.GetByIdAsync(login.CompanyId);
                var phoneData = await _telefoneEmpresa.GetListPhonesByCompanyIdAsync(login.CompanyId);

                if (phoneData != null && phoneData.Count != 0)
                {
                    foreach (var itens in phoneData)
                    {
                        responseEntitesDto.Data = new GetEntitesResponseDto
                        {
                            NomeEmpresa = company.NomeEmpresa,
                            EmailEmpresa = company.Email,
                            TipoEmpresa = company.TipoEmpresa,
                            NIF = company.NIF,
                            ContaResponsavel = company.ContaResponsavel,
                            Id = company.ID,
                            NumeroTelefone = [itens.NumeroTelefone]
                        };
                    }

                    responseEntitesDto.IsSucess = true;
                }
            }
            catch (Exception ex)
            {
                responseEntitesDto.Mensagem = $"Falha na Operação: {ex.Message}";
                responseEntitesDto.IsSucess = false;
            }

            return responseEntitesDto;
        }

        public async Task<ResponseDto<GetPerfilEntitesResponseDto>> GetPerfilItemAsync()
        {
            try
            {
                var login = await _autenticacao.GetLastLoginAsync();
                var company = await _empresa.GetByIdAsync(login.CompanyId);
                var phoneData = await _telefoneEmpresa.GetListPhonesByCompanyIdAsync(login.CompanyId);

                if(phoneData != null && phoneData.Count != 0)
                {
                    foreach(var itens in phoneData)
                    {
                        responsePerfilDto.Data = new GetPerfilEntitesResponseDto
                        {
                            NomeEmpresa = company.NomeEmpresa,
                            EmailEmpresa = company.Email,
                            TipoEmpresa = company.TipoEmpresa,
                            NIF = company.NIF,
                            ContaResponsavel = company.ContaResponsavel,
                            Id = company.ID,
                            NumeroTelefone = [itens.NumeroTelefone]
                        };
                    }

                    responsePerfilDto.IsSucess = true;
                }
            }
            catch (Exception ex)
            {
                responsePerfilDto.Mensagem = $"Falha na Operação: {ex.Message}";
                responsePerfilDto.IsSucess = false;
            }

            return responsePerfilDto;
        }

        public async Task<ResponseNullDto> SaveItemAsync(CreateEntitesRequestDto model)
        {
            try
            {
                var companyModel = new Pt_Empresa
                {
                    NomeEmpresa = model.NomeEmpresa.Trim(),
                    Email = responseHelper.EmailConvertToLowerCase(model.EmailEmpresa).Trim(),
                    NIF = model.NIF.Trim(),
                    TipoEmpresa = model.TipoEmpresa.Trim(),
                    UserName = responseHelper.UserNomeFinal(model.NomeUsuario).Trim(),
                    Password = model.Password.Trim(),
                    DescricaoEmpresa = model.DetalhesEmpresa.Trim(),
                    Morada = model.LocalizacaoEmpresa.Trim(),
                    ContaResponsavel = model.ContaResponsavel.Trim()
                };

                await _empresa.SaveAsync(companyModel);

                var company = await _empresa.FindEmpresaAsync(companyModel.UserName);

                if (model.NumeroTelefone != null && model.NumeroTelefone.Count != 0)
                {
                    foreach (var itens in model.NumeroTelefone)
                    {
                        var phone = new Pt_TelefoneEmpresa
                        {
                            EmpresaID = company.ID,
                            NumeroTelefone = itens
                        };

                        await _telefoneEmpresa.SaveAsync(phone);
                    }
                }

                responseNull.Mensagem = "Empresa criada com sucesso!";
                responseNull.IsSucess = true;
            }
            catch (Exception ex)
            {
                responseNull.Mensagem = $"Falha na Operação: {ex.Message}";
                responseNull.IsSucess = false;
            }

            return responseNull;
        }

        public async Task<ResponseNullDto> UpdateItemAsync(UpdateEntitesRequestDto model)
        {
            try
            {
                var login = await _autenticacao.GetLastLoginAsync();

                var companyModel = new Pt_Empresa
                {
                    ID = login.CompanyId,
                    NomeEmpresa = model.NomeEmpresa.Trim(),
                    Email = responseHelper.EmailConvertToLowerCase(model.EmailEmpresa).Trim(),
                    NIF = model.NIF.Trim(),
                    TipoEmpresa = model.TipoEmpresa.Trim(),
                    UserName = responseHelper.UserNomeFinal(model.NomeUsuario).Trim(),
                    Password = model.Password.Trim(),
                    DescricaoEmpresa = model.DetalhesEmpresa.Trim(),
                    Morada = model.LocalizacaoEmpresa.Trim(),
                    ContaResponsavel = model.ContaResponsavel.Trim()
                };

                await _empresa.SaveAsync(companyModel);

                var phoneData = await _telefoneEmpresa.GetListPhonesByCompanyIdAsync(login.CompanyId);

                if (phoneData != null && phoneData.Count != 0)
                {
                    if (model.NumeroTelefone != null && model.NumeroTelefone.Count != 0)
                    {
                        foreach (var phoneItens in phoneData)
                        {
                            foreach (var itens in model.NumeroTelefone)
                            {
                                var phone = new Pt_TelefoneEmpresa
                                {
                                    ID = phoneItens.ID,
                                    EmpresaID = phoneItens.EmpresaID,
                                    NumeroTelefone = itens
                                };

                                await _telefoneEmpresa.SaveAsync(phone);
                            }
                        }
                    }
                }

                responseNull.Mensagem = "Empresa criada com sucesso!";
                responseNull.IsSucess = true;
            }
            catch (Exception ex)
            {
                responseNull.Mensagem = $"Falha na Operação: {ex.Message}";
                responseNull.IsSucess = false;
            }

            return responseNull;
        }
    }
}
