using App.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Interfaces
{
    public interface IPsicologoRepository
    {

        IEnumerable<Psicologo> GetAll();

        int Insert(Psicologo psicologo);

        int Update(Psicologo psicologo);

        Psicologo Select(string cpf);

    }
}