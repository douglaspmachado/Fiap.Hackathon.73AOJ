using App.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Interfaces
{
    public interface IAgendaRepository
    {

        IEnumerable<Agenda> GetAll();

        int Insert(Agenda agenda);

        int Update(Agenda agenda);

        Agenda Select(string cpf_cnpjPsicologo);

    }
}