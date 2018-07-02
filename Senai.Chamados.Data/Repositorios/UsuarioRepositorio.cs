using Senai.Chamados.Data.Contexto;
using Senai.Chamados.Domain.Contratos;
using Senai.Chamados.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senai.Chamados.Data.Repositorios
{

    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly SenaiChamadosDbContext _contexto;
        public UsuarioRepositorio()
        {
            _contexto = new SenaiChamadosDbContext();
        }
        /*4*/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public bool Alterar(UsuarioDomain domain)
        {
            _contexto.Entry<UsuarioDomain>(domain).State = System.Data.Entity.EntityState.Modified;
            int linhasAlteradas = _contexto.SaveChanges();

            if (linhasAlteradas > 0 )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /*5*/

            /// <summary>
            /// Busca Usuario pelo Id
            /// </summary>
            /// <param name="id">Id do usuario</param>
            /// <param name="includes">dominio para fazer Inner</param>
            /// <returns></returns>
        public UsuarioDomain BuscarPorId(Guid id, string[] includes = null)
        {
            var query = _contexto.Usuarios.AsQueryable();
            if(includes != null)
            {
                foreach(var include in includes)
                {
                    query = query.Include(include);
                }               
            }
            return query.FirstOrDefault(x => x.Id == id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public bool Deletar(UsuarioDomain domain)
        {
            var usuario = _contexto.Usuarios.Single(o => o.Id == domain.Id);
            _contexto.Usuarios.Remove(usuario);
            int linhasExcluidas = _contexto.SaveChanges();

            if (linhasExcluidas > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /*2*/
        /*liberação de memoria*/
        public void Dispose()
        {
            _contexto.Dispose();
          
        }
        
        /*1*/

        /// <summary>
        /// Insere um novo usuario no banco
        /// </summary>
        /// <param name="domain">dados usuario</param>
        /// <returns>Retorna para o usuario cadastrado caso o usuario não esteja cadastrado</returns>
        public bool Inserir(UsuarioDomain domain)
        {
            _contexto.Usuarios.Add(domain);
           int linhasInocluidas = _contexto.SaveChanges();

            if (linhasInocluidas > 0 )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Lisata todos os usuario do Banco
        /// </summary>
        /// <param name="includes">Dominio para fazer Inner Join</param>
        /// <returns>Lista de Usuarios Domain</returns>
        public List<UsuarioDomain> Listar(string[] includes = null)
        {
            var query = _contexto.Usuarios.AsQueryable();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

                /*função lambda do foreach*/
                //query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return query.ToList();

        }

        /// <summary>
        /// Valida usuario do banco
        /// </summary>
        /// <param name="email">email do usuario</param>
        /// <param name="senha">senha do usuario</param>
        /// <returns>um uusario caso o mesmo seja valido</returns>
        public UsuarioDomain Login(string email, string senha)
        {
            UsuarioDomain objUsuario = _contexto.Usuarios.FirstOrDefault(x => x.Email == email && x.Senha == senha);
            if (objUsuario == null)
            {
                return null;
            }
            else
            {
                return objUsuario;
            }
        }
            
    }
}
