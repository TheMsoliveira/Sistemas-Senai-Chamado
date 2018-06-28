using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senai.Chamados.Domain.Contratos
{   /*repositorio generico*/
    public interface IRepositorioBase<TDomain> where TDomain :class
    {
        /*includes semelhante ao iner join */
        /*parametro opcional array de string e includes = null*/
        TDomain BuscarPorId(Guid id, string[] includes = null);
        List<TDomain> Listar(string[] includes = null);
        bool Inserir(TDomain domain);
        bool Alterar(TDomain domain);
        bool Deletar(TDomain domain);

           
    }
}
