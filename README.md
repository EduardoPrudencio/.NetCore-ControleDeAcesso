# .NetCore-ControleDeAcesso

Este exemplo foi criado com a composição de 3 projetos.

Security.Api: camada de apresentação que recebe as requicições e devolves as respostas de acordo com as regras de negócio.
Security.Business: cama que gerencia o processamento baseada nas regras de negócio existentes nesta mesma camada.
Security.DataAccess: camada responsável pela obtenção de informação persistidas em bancos de dados, arquivos XMl, arquivos Excel etc...
É nessa camada que serão feitas as configurações do EntityFramework.

Criando Connectiosns Strings
    No arquivo AppSettings.json de ser inserido o seguinte bloco:
   
 ...
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost; Database=nomeDoBancoDeDados; Trusted_Connection=True; MultipleActiveResultSets=true"
  }
  ...

Vamos referenciar, através do NuGet, no projeto Security.DataAccess, as aplicações:

Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Design

Em seguida, neste mesmo projeto, vamos criar uma pasta chamada Context e nela o arquivo ApiContext.cs com o seguinte conteúdo:

 public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {

        }
    }

    Vamos inserir o seguinte bloco no arquivo AppSettings.json

     services.AddDbContext<ApiContext>(options => options
                                                  .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                                                   b => b.MigrationsAssembly("Security.Api")));


O trecho  b => b.MigrationsAssembly("Security.Api") deve ser informado sempre que o arquivo que herda de DbContext, no nosso caso
o ApiContext, estiver em um projeto diferente do que contem o AppSettings.

Depois basta executar o comanfo Add-Migration Initial para cria todo o conteúdo do Migraion.

Ao executar o comando update-database as tabelas serão criadas no banco de dados cujo o nome foi informado no AppSettings.json

Implementando Identity

Para saber mais sobre o <a href="https://docs.microsoft.com/pt-br/aspnet/core/security/authentication/identity?view=aspnetcore-2.2&tabs=visual-studio" target="_blank">Identity</a>

Com a intenção de facilitar a compreenção no código eu trabalhei com extention Method para a configuração do identiy, Isso 
porque nesse exemplo eu trabalhei com um Context separado para o Identity.

No projeto da API foi criada a pasta configuration e dentro dela a classe IdentityConfiguration.

 public static class IdentityConfiguration
    {

        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<AplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Security.Api")));

            services.AddDefaultIdentity<IdentityUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<AplicationDbContext>()
                    .AddDefaultTokenProviders();

            return services;
        }
    }

    Na classe Startup foi adcionada a segiunte linha:

    ...
    services.AddIdentityConfiguration(Configuration);
    ...

Depois basta gerarmos o migration e atualizar o banco de dados

Erro por ter mais de um context

Se você excutar o comando Add-Migration Identity vai se deparar com o seguinte erro:

More than one DbContext was found. Specify which one to use. Use the '-Context' parameter for PowerShell commands and the '--context' parameter for dotnet commands.


Para que esse erro não ocorra é necessário definir o contexto usado no projeto. Isso faz com que nosso comando fique da seguinte forma:
Add-Migration Identity -Context AplicationDbContext.

O comando para gerar as tabelas também precisam da definição do contexto como no exemplo abaixo.
update-database -Context AplicationDbContext

Depois é necessário colocar a seguinte linha na classe Startup:

 app.UseAuthentication();
 
 Vale lembrar que deve ser antes da linha de configuração do MVC.
           


