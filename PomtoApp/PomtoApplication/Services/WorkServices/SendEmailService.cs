using PomtoApplication.ShareDto;
using System.Net;
using System.Net.Mail;

namespace PomtoApplication.Services.WorkRPL
{
    public class SendEmailService
    {
        ResponseNullDto responseNull = new();

        public async Task NewRequestLicense()
        {
            if (Email.Default.IsComposeSupported)
            {
                try
                {
                    var message = new EmailMessage
                    {
                        Subject = "Pedido de Licença",
                        Body = "Olá! Pretendo adquirir a Licença Esmeralda.",
                        BodyFormat = EmailBodyFormat.PlainText,
                        To = new List<string> { "eares.software@gmail.com" }
                    };

                    await Email.Default.ComposeAsync(message);

                    responseNull.Mensagem = "E-mail enviado com sucesso! Vamos enviar a sua licença mediante o pronto pagamento. Obrigado!";
                }
                catch (Exception ex)
                {
                    responseNull.Mensagem = $"Erro ao enviar o e-mail: {ex.Message}";
                }
            }
            else
            {
                responseNull.Mensagem = "Composição de e-mail não suportada neste dispositivo.";
            }
        }

        public async Task NewRequestFastLicenseEmailDefault(string nome, string email, string telefone, string license)
        {
            try
            {
                using (var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    EnableSsl = true,
                    Credentials = new NetworkCredential("earespomto@gmail.com", "icerzhvnvizuuvpi")
                })
                using (var message = new MailMessage
                {
                    From = new MailAddress("earespomto@gmail.com", "Equipa POMTO"),
                    Subject = $"Pedido de Licença para {nome}",
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
                        <h2>Pedido de Licença</h2>
                        <div class='message'>
                            <p>Olá! Pretendo adquirir a Licença Esmeralda.</p>
                            <p>Obrigado!</p>
                            <h3>Contacto</h3>
                            <p>{telefone}</p>
                            <h3>Série da Licença</h3>
                            <p>{license}</p>
                        </div>
                        <div class='footer'>
                            Equipa do sistema Ponto
                        </div>
                    </div>
                </body>
                </html>"
                })
                {
                    message.To.Add(new MailAddress(email, nome));
                    await client.SendMailAsync(message);
                    responseNull.IsSucess = true;
                    responseNull.Mensagem = "E-mail enviado! Vamos enviar a sua licença mediante o pronto pagamento. Obrigado!";
                }
            }
            catch (Exception ex)
            {
                responseNull.IsSucess = false;
                responseNull.Mensagem = $"Ocorreu um erro ao enviar o e-mail: {ex.Message}";
            }
        }

        public async Task SendUserFeedbackEmailAsync(string nome, string email, int classificacao, string userMessage)
        {
            try
            {
                using (var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    EnableSsl = true,
                    Credentials = new NetworkCredential("earespomto@gmail.com", "icerzhvnvlzuuvpi")
                })
                using (var message = new MailMessage
                {
                    From = new MailAddress("earespomto@gmail.com", "Equipa POMTO"),
                    Subject = $"Avaliação do POMTO de {nome}",
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
                        <h2>Avaliação do POMTO</h2>
                        <div class='message'>
                            <h3>Classificação</h3>
                            <p>Número de estrelas: {classificacao}</p>
                            <p>{userMessage}</p>
                        </div>
                        <div class='footer'>
                            Equipa do sistema Ponto
                        </div>
                    </div>
                </body>
                </html>"
                })
                {
                    message.To.Add(new MailAddress(email, nome));
                    await client.SendMailAsync(message);
                    responseNull.IsSucess = true;
                    responseNull.Mensagem = "E-mail enviado com sucesso! Obrigado por ajudar-nos a melhorar.";
                }
            }
            catch (Exception ex)
            {
                responseNull.IsSucess = false;
                responseNull.Mensagem = $"Ocorreu um erro ao enviar o e-mail: {ex.Message}";
            }
        }

        public async Task<ResponseNullDto> RecuperarAcessoAsync(string nome, string email, string senha)
        {
            var response = new ResponseNullDto();
            var smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential("earespomto@gmail.com", "icerzhvnvlzuuvpi")
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("earespomto@gmail.com", "Equipa POMTO"),
                Subject = $"Recuperação de Conta {nome}",
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
                        <p>Nome de Usuário: {nome}</p>
                        <p>Senha: {senha}</p>
                    </div>
                    <div class='footer'>
                        Equipa do sistema Ponto
                    </div>
                </div>
            </body>
            </html>"
            };

            mailMessage.To.Add(new MailAddress(email, nome));

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
                response.IsSucess = true;
                response.Mensagem = "E-mail enviado! Por favor, confirme o seu e-mail.";
            }
            catch (Exception ex)
            {
                response.IsSucess = false;
                response.Mensagem = $"Ocorreu um erro ao enviar o e-mail: {ex.Message}";
            }

            return response;
        }

    }
}
