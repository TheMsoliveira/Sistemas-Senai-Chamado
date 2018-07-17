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
        public SenaiChamadosDbContext() : base ("SenaiConnection")
        {

        }

        // referencia as tabelas do banco
        public DbSet<UsuarioDomain> Usuarios { get; set; }
        public DbSet<ChamadoDomain> Chamados { get; set; }


        /*usando polimosfismo alterando o SaveChanges ()*/
        public override int SaveChanges()
        {
            try
            {
                /*verificando se os dados já existem*/
                foreach (var entry in ChangeTracker.Entries().Where(e => e.Entity.GetType().GetProperty("DataCriacao")!= null))
                {
                    
                    if (new Guid(entry.Property("Id").CurrentValue.ToString()) == Guid.Empty)
                    {
                        entry.Property("Id").CurrentValue = Guid.NewGuid();
                    }

                    if (entry.State == EntityState.Added)
                    {
                        entry.Property("DataCriacao").CurrentValue = DateTime.Now;
                        entry.Property("DataAlteracao").CurrentValue = DateTime.Now;
                    }

                    if(entry.State == EntityState.Modified)
                    {
                        entry.Property("DataCriacao").IsModified = false;
                        entry.Property("DataAlteracao").CurrentValue = DateTime.Now;
                    }

                }
                /* referenciando como base.SaveChanges() é em relação a quem esta herdando no caso DbContext ou seja epanas salvar após modificações*/
                return base.SaveChanges();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
