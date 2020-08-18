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


    }
}