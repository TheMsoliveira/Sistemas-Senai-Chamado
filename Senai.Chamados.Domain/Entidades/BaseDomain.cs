using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senai.Chamados.Domain.Entidades
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class  BaseDomain
    {
        /*guid - código unico usado para aplições web*/
        public Guid Id { get; set; }
        public DateTime DataCriacao{ get; set; }
        public DateTime DataAlteracao{ get; set; }
    }
}
