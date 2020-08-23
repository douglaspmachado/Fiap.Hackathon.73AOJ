using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entity
{
    public class Psicologo : Usuario
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string CRP { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string CodGraduacao { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string InstituicaoEnsino { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Curso { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string AnoInicio { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string AnoTermino { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string AreaEstudo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string DescricaoAtuacao { get; set; }

        
        public IEnumerable<Abordagens> Abordagens { get; set; }


        public IEnumerable<Atendimento> Atendimentos { get; set; }

        /// <summary>
        /// 
        /// </summary>
       
        public Graduacao Graduacao { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public List<Agenda> Agenda { get; set; }



    }
}
