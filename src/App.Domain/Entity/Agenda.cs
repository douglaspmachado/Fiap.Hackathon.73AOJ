using System;

namespace App.Domain.Entity
{
    public class Agenda
    {
        public string CPF_Paciente { get; set; }

        public string CPF_CNPJPsicologo { get; set; }

        public string DataConsulta  { get; set; }

        public string HorarioConsulta { get; set; }
    }
}
