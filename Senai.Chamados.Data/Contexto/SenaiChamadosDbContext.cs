using Senai.Chamados.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senai.Chamados.Data.Contexto
{   /*DbContext necessário baixar o pacote pelo NuGet - add referencias com botão direito gerenciar pacotes do NuGeT*/
    public class SenaiChamadosDbContext: DbContext
    {   
        // criação de um construtor para a conexão
        // usandmo connection string criada no app.config
        public SenaiChamadosDbContext() : base (@"Data Source =.\SqlExpress; Initial Catalog = SenaiChamadosDb; user id = sa; password=senai@123")
        {

        }

        // referencia as tabelas do banco
        public DbSet<UsuarioDomain> Usuarios { get; set; }
    }
}
