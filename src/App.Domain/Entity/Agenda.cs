using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entity
{
   public class Agenda
    {
        string CPF_Paciente { get; set; }

        string CPF_CNPJPsicologo { get; set; }

        DateTime DataConsulta  { get; set; }

        DateTime HorarioConsulta { get; set; }
    }
}
