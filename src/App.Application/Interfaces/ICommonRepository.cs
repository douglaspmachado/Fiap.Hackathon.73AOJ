using App.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Interfaces
{
   public interface ICommonRepository
    {
        IEnumerable<Abordagens> GetAbordagens();

        IEnumerable<Atendimento> GetAtendimento();

        IEnumerable<Genero> GetGenero();
               
    }
}
