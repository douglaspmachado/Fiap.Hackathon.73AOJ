using App.Domain.Entity;

namespace App.Application.Interfaces
{
    public interface IPacienteRepository
    {
        Usuario Select(int idPaciente);

        int Insert(Usuario usuario);

        int Update(Usuario usuario);

    }
}
