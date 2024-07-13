using CommunityToolkit.Maui;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using PomtoApplication.IServices;
using PomtoApplication.Services.CrudServices;
using PomtoApplication.Services.ReportRPL.GenerateReport;
using PomtoApplication.Services.WorkRPL;
using PomtoDomain.Interfaces;
using PomtoInfraData.PomtoContext;
using PomtoInfraData.Repository;

namespace PomtoApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
             .UseMauiApp<App>()
             .UseMauiCommunityToolkit()
             .ConfigureFonts(fonts =>
             {
                 fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                 fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
             });

            var folder = Environment.SpecialFolder.DesktopDirectory;
            var path = Environment.GetFolderPath(folder);
            var dbPath = Path.Join(path, "pomto.db");

            builder.Services.AddDbContext<PomtoDbContext>(options =>
                options.UseSqlite($"Data Source={dbPath}"),
                ServiceLifetime.Transient);

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddMudServices();
            builder.Services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(@"./Keys/"))
                .SetApplicationName("POMTO");

            #region Interface & Services
            builder.Services.AddScoped<IAuthServices,AuthServices>();
            builder.Services.AddScoped<IEntitesServices, EntitesServices>();
            builder.Services.AddScoped<IUsuarioServices, UsuarioServices>();
            #endregion

            #region Services
            builder.Services.AddScoped<GeneratePremiumService>();
            builder.Services.AddScoped<MoveFilesService>();
            builder.Services.AddScoped<ReadPathService>();
            builder.Services.AddScoped<ReportService>();
            builder.Services.AddScoped<SendEmailService>();
            builder.Services.AddScoped<SendFilesService>();
            builder.Services.AddScoped<StartProcessService>();
            #endregion

            #region InterfaceRPL e Repository
            builder.Services.AddScoped<IAutenticacao, AutenticacaoRPL>();
            builder.Services.AddScoped<ICaminhoPasta, CaminhoPastaRPL>();
            builder.Services.AddScoped<IControlePrograma, ControleProgramaRPL>();
            builder.Services.AddScoped<IDocumento, DocumentoRPL>();
            builder.Services.AddScoped<IEmpresa, EmpresaRPL>();
            builder.Services.AddScoped<IFimTrabalho, FimTrabalhoRPL>();
            builder.Services.AddScoped<ILicenca, LicencaRPL>();
            builder.Services.AddScoped<IProfissaoUsuario, ProfissaoUsuarioRPL>();
            builder.Services.AddScoped<ITask, TaskRPL>();
            builder.Services.AddScoped<ITelefoneEmpresa, TelefoneEmpresaRPL>();
            builder.Services.AddScoped<ITelefoneUsuario, TelefoneUsuarioRPL>();
            builder.Services.AddScoped<ITipoLicenca, ITipoLicenca>();
            builder.Services.AddScoped<IUserCompany, UserCompanyRPL>();
            builder.Services.AddScoped<IUsuario, UsuarioRPL>();
            #endregion

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<PomtoDbContext>();
                dbContext.Database.EnsureCreated();
            }

            return app;
        }        
    }
}

