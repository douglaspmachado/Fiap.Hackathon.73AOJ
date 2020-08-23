using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using App.Application.Interfaces;
using App.Domain.Entity;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace App.Infra.Repository
{
    public class AgendaRepository : IAgendaRepository
    {
        private readonly IConfiguration _configuration;
        private StringBuilder SQL = new StringBuilder();

        public AgendaRepository(IConfiguration configuration)
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

        public IEnumerable<Agenda> GetAll()
        {
            throw new NotImplementedException();
        }

        public int Insert(Agenda agenda)
        {
            throw new NotImplementedException();
        }

        public Agenda Select(string cpf_cnpjPsicologo)
        {
            throw new NotImplementedException();
        }

        public int Update(Agenda agenda)
        {
            throw new NotImplementedException();
        }
    }
}