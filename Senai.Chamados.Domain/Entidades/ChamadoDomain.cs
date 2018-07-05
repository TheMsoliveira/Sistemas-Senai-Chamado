using Senai.Chamados.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senai.Chamados.Domain.Entidades
{
    /// <summary>
    /// Classe resposável pela entida Chamados
    /// </summary>
    /// 
    [Table("Chamados")]  // Data anotatio que da nome a tabela
    public class ChamadoDomain : BaseDomain
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public EnSetor Setor{ get; set; }
        public EnStatus Status { get; set; }

        [ForeignKey("Usuario")]
        public Guid IdUsuario { get;  set; }


        public virtual  UsuarioDomain Usuario { get; set; }
    }
}
