using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using App.Domain.Entity;
using Microsoft.Extensions.Configuration;

namespace App.Infra.Repository
{
    public class PsicologoRepository
    {
        private readonly IConfiguration _configuration;
        private StringBuilder SQL = new StringBuilder();

        public PsicologoRepository(IConfiguration configuration)
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

        public int Insert(Psicologo usuario)
        {
            throw new NotImplementedException();
        }

        public int Update(Psicologo usuario)
        {
            throw new NotImplementedException();
        }

        public Psicologo Get(string cpf)
        {
            throw new NotImplementedException();
        }
    }
}