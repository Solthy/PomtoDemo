using PomtoApplication.Helpers;
using PomtoApplication.ShareDto;
using System.Net;
using System.Net.Mail;

namespace PomtoApplication.Services.WorkRPL
{
    public class SendFilesService : PathHelper
    {
        ResponseNullDto responseNullDto = new();

        public async Task ShareMultipleFiles()
        {
            if (File.Exists(WorkFile) && File.Exists(WorkProgramExecuteTimeFile))
            {
                await Share.Default.RequestAsync(new ShareMultipleFilesRequest
                {
                    Title = "Olá! Estou enviando os arquivos de hoje. Desejo uma boa continuação.",
                    Files = new List<ShareFile> { new ShareFile(WorkFile), new ShareFile(WorkProgramExecuteTimeFile) }
                });
            }
            else
            {
                responseNullDto.IsSucess = false;
                responseNullDto.Mensagem = $"Não é possível enviar os arquivos.\nOs seguintes arquivos são necessários para o envio:\n- {Path.GetFileName(WorkFile)}\n- {Path.GetFileName(WorkProgramExecuteTimeFile)}";
            }
        }

        public async Task EnviarEmailDefaul(string nome, string email, string userName, string password)
        {
            try
            {
                using (var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    EnableSsl = true,
                    Timeout = 50000,
                    Credentials = new NetworkCredential("earespomto@gmail.com", "icerzhvnvlzuuvpi")
                })
                {
                    var message = new MailMessage
                    {
                        From = new MailAddress("earespomto@gmail.com", "Equipa POMTO"),
                        Subject = "Pedido de Recuperação de Conta",
                        IsBodyHtml = true,
                        Body = $@"
                <!DOCTYPE html>
                <html lang='pt-pt'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            display: flex;
                            justify-content: center;
                            align-items: center;
                            height: 100vh;
                            margin: 0;
                            background-color: #f0f0f0;
                        }}
                        .card {{
                            width: 300px;
                            padding: 20px;
                            background-color: #fff;
                            border-radius: 8px;
                            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
                        }}
                        .footer {{
                            margin-top: 20px;
                            text-align: center;
                            color: #888;
                            font-size: 14px;
                        }}
                    </style>
                </head>
                <body>
                    <div class='card'>
                        <h2>Credenciais de Acesso</h2>
                        <div class='message'>
                            <h3>Nome do Usuário:</h3>
                            <p>{userName}</p>
                            <h3>Senha:</h3>
                            <p>{password}</p>
                        </div>
                        <div class='footer'>
                            Equipa do sistema Ponto
                        </div>
                    </div>
                </body>
                </html>"
                    };
                    message.To.Add(new MailAddress(email, nome));

                    await client.SendMailAsync(message);
                    responseNullDto.IsSucess = true;
                    responseNullDto.Mensagem = "E-mail enviado! Por favor verifique o seu e-mail.";
                }
            }
            catch (Exception ex)
            {
                responseNullDto.Mensagem = $"Ocorreu um erro ao enviar o e-mail: {ex.Message}";
            }
        }

    }
}
