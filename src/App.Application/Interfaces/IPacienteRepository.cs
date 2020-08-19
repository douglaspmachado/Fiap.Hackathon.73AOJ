using App.Domain.Entity;

namespace App.Application.Interfaces
{
    public interface IPacienteRepository
    {
         Usuario Get(string cpf);

        int Insert(Usuario usuario);

        int Update(Usuario usuario);

    }
}
