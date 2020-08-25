using System;

namespace App.Domain.Entity
{
    public class Agenda
    {
        public string CPF_Paciente { get; set; }

        public string CPF_CNPJPsicologo { get; set; }

        public DateTime DataConsulta  { get; set; }

        public DateTime HorarioConsulta { get; set; }

        public string Nome { get; set; }

        
    }
}
