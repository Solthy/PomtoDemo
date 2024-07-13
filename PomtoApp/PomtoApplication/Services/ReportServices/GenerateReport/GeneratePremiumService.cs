using DotLiquid;
using iText.Html2pdf;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using PomtoApplication.DTOs.Report.Request;
using PomtoApplication.Helpers;

namespace PomtoApplication.Services.ReportRPL.GenerateReport
{
    public class GeneratePremiumService : PathHelper
    {
        public bool GeneratePDF(CreateReportRequestDto data)
        {
            try
            {
                string templateContent = File.ReadAllText(HtmlReportFilePath);
                Template template = Template.Parse(templateContent);

                string renderedHtml = template.Render(Hash.FromAnonymousObject(new
                {
                    WorkData = data.DadosTrabalho,
                    WorkProgram = data.DadosPrograma,
                    CompanyName = data.NomeEmpresa,
                    UserName = data.NomeUsuario,
                    UserEmail = data.EmailUsuario,
                    TimeUser = data.HorasTrabalho,
                    DateEmissao = data.DataEmissao,
                    Money = data.Remuneracao,
                    WorkImagens = data.Imagens,
                    AtivityWork = data.Atividades
                }));

                string tempOutputPath = System.IO.Path.GetTempFileName();

                using (var pdfStream = new FileStream(tempOutputPath, FileMode.Create))
                {
                    HtmlConverter.ConvertToPdf(renderedHtml, pdfStream);
                }

                using (var pdfDoc = new PdfDocument(new PdfReader(tempOutputPath), new PdfWriter(WorkPremiumFilePDF)))
                {
                    int numberOfPages = pdfDoc.GetNumberOfPages();
                    PdfFont font = PdfFontFactory.CreateFont();

                    for (int i = 1; i <= numberOfPages; i++)
                    {
                        PdfPage page = pdfDoc.GetPage(i);
                        Rectangle pageSize = page.GetPageSize();
                        string pageText = $"Página {i} de {numberOfPages}";

                        float textWidth = font.GetWidth(pageText) * 10 / 1000;
                        float textStartX = (pageSize.GetLeft() + pageSize.GetRight()) / 2 - textWidth / 2;

                        new PdfCanvas(page)
                            .BeginText()
                            .SetFontAndSize(font, 10)
                            .MoveText(textStartX, pageSize.GetBottom() + 20)
                            .ShowText(pageText)
                            .EndText();
                    }
                }

                File.Delete(tempOutputPath);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro ao gerar o PDF: " + ex.Message);
                return false;
            }
        }
    }
}
