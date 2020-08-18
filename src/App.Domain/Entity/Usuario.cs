using System;
using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entity
{
    public class Usuario
    {
        public Usuario()
        {
 
        }

        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Nome { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Sobrenome { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public DateTime DataNascimento { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string CPF_CNPJ { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Celular { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public Endereco Endereco { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido...")]
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>

        [Required(ErrorMessage = "Informe a senha!")]
        public string Senha { get; set; }


    }
}