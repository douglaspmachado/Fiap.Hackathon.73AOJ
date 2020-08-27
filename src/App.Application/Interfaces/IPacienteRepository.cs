using App.Domain.Entity;
using System;

namespace App.Application.Interfaces
{
    public interface IPacienteRepository
    {
        Usuario Select(string cpf);

        bool Insert(Usuario usuario);

        int Update(Usuario usuario);

        Usuario Autenticar(string cpf, string senha);

        bool InsertAgenda(string cpf, string cpf_prof, DateTime dtConsulta, string time);
    }
}
