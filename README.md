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
           


