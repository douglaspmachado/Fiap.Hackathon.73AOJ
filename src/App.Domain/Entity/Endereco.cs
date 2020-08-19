using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace App.Domain.Entity
{
   public class Endereco
    {

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string CEP { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Pais { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Estado { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Cidade { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Logradouro { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Bairro { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Numero { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Complemento { get; set; }

    }
}
