using PomtoApplication.DTOs.Report.Response;
using PomtoApplication.DTOs.Usuario.Request;
using PomtoApplication.DTOs.Usuario.Response;
using PomtoApplication.Helpers;
using PomtoApplication.IServices;
using PomtoApplication.ShareDto;
using PomtoDomain.Interfaces;
using PomtoDomain.Model;

namespace PomtoApplication.Services.CrudServices
{
    public class UsuarioServices : IUsuarioServices
    {
        private readonly IAutenticacao _autenticacao;
        private readonly IUsuario _usuario;
        private readonly IDocumento _documento;
        private readonly IProfissaoUsuario _profissao;
        private readonly IFimTrabalho _fimTrabalho;
        private readonly ITelefoneUsuario _telefoneUsuario;
        private readonly ICaminhoPasta _caminhoPasta;

        ResponseListDto<GetListProfisaoResponseDTO> responseProfissaoDto = new();
        ResponseDto<GetPerfilUserRequestDto> responsePerfilDto = new();
        ResponseListDto<GetReportResponseDto> responseReportDto = new();
        ResponseDto<GetUserRequestDto> responseUserDto = new();
        ResponseHelper responseHelper = new();
        ResponseNullDto responseNull = new();

        public UsuarioServices(IAutenticacao autenticacao, IUsuario usuario, IDocumento documento, ITelefoneUsuario telefoneUsuario, IProfissaoUsuario profissao, IFimTrabalho fimTrabalho, ICaminhoPasta caminhoPasta)
        {
            _autenticacao=autenticacao;
            _usuario=usuario;
            _documento=documento;
            _telefoneUsuario=telefoneUsuario;
            _profissao=profissao;
            _fimTrabalho=fimTrabalho;
            _caminhoPasta=caminhoPasta;
        }

        public async Task<ResponseListDto<GetReportResponseDto>> GetDocumentosItemAsync()
        {
            var login = await _autenticacao.GetLastLoginAsync();
            var file = await _documento.GetListDocumentByUserAsync(login.UserId);

            if (file != null && file.Count != 0)
            {
                foreach (var itens in file)
                {
                    responseReportDto.Data.Add(new GetReportResponseDto
                    {
                        Id = itens.ID,
                        ReferenciaDocumento = itens.ReferenciaDocumento,
                        NomeDocumento = $"{itens.NomeDocumento}{itens.ExtensaoDocumento}",
                        CaminhoDocumento = itens.CaminhoDocumento,
                        DataEmissao = itens.DataCriacao,
                        TamanhoDocumento = itens.TamanhoDocumento
                    });
                }
            }

            return responseReportDto;
        }

        public async Task<ResponseDto<GetUserRequestDto>> GetItemAsync()
        {
            var login = await _autenticacao.GetLastLoginAsync();
            var user = await _usuario.GetByIdAsync(login.UserId);
            var profissao = await _profissao.GetByIdAsync(user.ProfissaoID);
            var phoneData = await _telefoneUsuario.GetListPhonesByUsuarioIdAsync(login.UserId);

            if (phoneData != null && phoneData.Count != 0)
            {
                foreach (var itens in phoneData)
                {
                    responseUserDto.Data = new GetUserRequestDto
                    {
                        Id = user.ID,
                        NomeCompleto = user.NomeCompleto,
                        UserName = user.UserName,
                        DataNascimento = user.DataNascimento,
                        Password = user.Password,
                        TipoPagamento = user.TipoPagamento,
                        Profissao = profissao.NomeProfissao,
                        ValorHora = user.Pagamento,
                        UserEmail = user.Email,
                        NumeroTelefone = [itens.NumeroTelefone]
                    };
                }

                responsePerfilDto.IsSucess = true;
            }

            return responseUserDto;
        }

        public async Task<ResponseDto<GetPerfilUserRequestDto>> GetPerfilItemAsync()
        {
            try
            {
                var login = await _autenticacao.GetLastLoginAsync();
                var user = await _usuario.GetByIdAsync(login.UserId);
                int days = await _fimTrabalho.GetTotalDaysByPeriodAsync(login.UserId, 1);
                decimal money = await _fimTrabalho.GetTotalRemunerationByPeriodAsync(login.UserId, 1);
                var userWork = await _fimTrabalho.GetWorkByUserAsync(login.UserId);
                var paths = await _caminhoPasta.TakeListPathByUser(login.UserId);

                if (paths != null && paths.Count != 0)
                {
                    foreach (var itens in paths)
                    {
                        responsePerfilDto.Data = new GetPerfilUserRequestDto
                        {
                            NomeCompleto = user.NomeCompleto,
                            UserName = user.UserName,
                            DataNascimento = user.DataNascimento,
                            Pagamento = money,
                            DiasTrabalho = days,
                            HoraTrabalho = userWork.Select(s => s.DataCriacao.Day).Sum(),
                            UserEmail = user.Email,
                            Id = user.ID,
                            PastasDeTrabalho = [itens.NomePath]
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

        public async Task<ResponseListDto<GetListProfisaoResponseDTO>> GetProfissaoItemAsync()
        {
            var profissao = await _profissao.GetListAsync();

            if (profissao != null && profissao.Count != 0)
            {
                foreach (var itens in profissao)
                {
                    responseProfissaoDto.Data.Add(new GetListProfisaoResponseDTO
                    {
                        Id = itens.ID,
                        Profissao = [itens.NomeProfissao]
                    });
                }
            }

            return responseProfissaoDto;

        }

        public async Task<ResponseNullDto> SaveItemAsync(CreateUserRequestDto model)
        {
            try
            {
                var profissao = await _profissao.GetByNameAsync(model.Profissao);

                var userModel = new Pt_Usuario
                {
                    NomeCompleto = model.NomeCompleto.Trim(),
                    Email = responseHelper.EmailConvertToLowerCase(model.UserEmail).Trim(),
                    ProfissaoID = profissao.ID,
                    Pagamento = model.ValorHora,
                    TipoPagamento = model.TipoPagamento.Trim(),
                    DataNascimento = (DateTime)model.DataNascimento,
                    Morada = model.Morada.Trim(),
                    UserName = responseHelper.UserNomeFinal(model.NomeCompleto).Trim(),
                    Password = model.Password.Trim()
                };

                await _usuario.SaveAsync(userModel);

                var user = await _usuario.FindUserAsync(userModel.UserName);

                if (model.NumeroTelefone != null && model.NumeroTelefone.Count != 0)
                {
                    foreach (var itens in model.NumeroTelefone)
                    {
                        var phone = new Pt_TelefoneUsuario
                        {
                            UsuarioID = user.ID,
                            NumeroTelefone = itens
                        };

                        await _telefoneUsuario.SaveAsync(phone);
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

        public async Task<ResponseNullDto> UpdateItemAsync(UpdateUserRequestDto model)
        {
            try
            {
                var login = await _autenticacao.GetLastLoginAsync();

                var profissao = await _profissao.GetByNameAsync(model.Profissao);

                var userModel = new Pt_Usuario
                {
                    ID = login.UserId,
                    NomeCompleto = model.NomeCompleto.Trim(),
                    Email = responseHelper.EmailConvertToLowerCase(model.UserEmail).Trim(),
                    ProfissaoID = profissao.ID,
                    Pagamento = model.ValorHora,
                    TipoPagamento = model.TipoPagamento.Trim(),
                    DataNascimento = (DateTime)model.DataNascimento,
                    Morada = model.Morada.Trim(),
                    UserName = responseHelper.UserNomeFinal(model.NomeCompleto).Trim(),
                    Password = model.Password.Trim()
                };

                await _usuario.SaveAsync(userModel);

                var phoneData = await _telefoneUsuario.GetListPhonesByUsuarioIdAsync(login.UserId);

                if (phoneData != null && phoneData.Count != 0)
                {
                    if (model.NumeroTelefone != null && model.NumeroTelefone.Count != 0)
                    {
                        foreach (var phoneItens in phoneData)
                        {
                            foreach (var itens in model.NumeroTelefone)
                            {
                                var phone = new Pt_TelefoneUsuario
                                {
                                    ID = phoneItens.ID,
                                    UsuarioID = phoneItens.UsuarioID,
                                    NumeroTelefone = itens
                                };

                                await _telefoneUsuario.SaveAsync(phone);
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
