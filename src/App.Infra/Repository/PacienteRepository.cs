using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using App.Application.Interfaces;
using App.Domain.Entity;
using Microsoft.Extensions.Configuration;

namespace App.Infra.Repository
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly IConfiguration _configuration;
        private StringBuilder SQL = new StringBuilder();

        public PacienteRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_configuration.GetConnectionString("aws-db"));
            }
        }

        public int Insert(Usuario usuario)
        {
           throw new NotImplementedException();
        }

        public int Update(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public Usuario Select(int idUsuario)
        {
            throw new NotImplementedException();
        }
    }
}