using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Senai.Chamados.Web.Util
{
    public class Hash
    {
        /// <summary>
        /// retorna um text encriptografado
        /// </summary>
        /// <param name="Texto">texto que ira ser incriptografado</param>
        /// <returns>retorna o texto encriptografado</returns>
        public static string GerarHash(String Texto)
        {   
            // declara uma variavel do tipo builder
            StringBuilder result = new StringBuilder();
            // declara uma variavel do tipo SHA256 para encriptografar
            SHA256 sha256 = SHA256Managed.Create();
            // converte o texto recebido como parametro em bytes
            byte[] bytes = Encoding.UTF8.GetBytes(Texto);
            //gera uma hash de acordo com a variavel  bytes 
            byte[] hash = sha256.ComputeHash(bytes);
            //percorre a hash e cai concatenando o texto
            for (int i = 0; i <hash.Length; i++)
            {

                result.Append(hash[i].ToString("X"));
            }
            //retorna o texto encriptografado
            return result.ToString();
        }
    }
}