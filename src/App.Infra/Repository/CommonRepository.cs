using System;
using System.Collections.Generic;
using System.Text;
using App.Application.Interfaces;
using App.Domain.Entity;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace App.Infra.Repository
{
    public class CommonRepository : ICommonRepository
    {
        private readonly IConfiguration _configuration;
        private StringBuilder SQL = new StringBuilder();


        public CommonRepository(IConfiguration configuration)
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

        public IEnumerable<Abordagens> GetAbordagens()
        {
            IEnumerable<Abordagens> listaAbordagens;

            using (IDbConnection conn = Connection)
            {
                listaAbordagens = conn.Query<Abordagens>("SELECT CODIGO, DESCRICAO FROM TAB_ABORDAGENS");
            }

            return listaAbordagens;
        }

        public IEnumerable<Atendimento> GetAtendimento()
        {
            IEnumerable<Atendimento> listaAtendimento;

            using (IDbConnection conn = Connection)
            {
                listaAtendimento = conn.Query<Atendimento>("SELECT CODIGO, DESCRICAO FROM TAB_ATENDIMENTO");
            }

            return listaAtendimento;
        }

        public IEnumerable<Genero> GetGenero()
        {
            IEnumerable<Genero> listaGenero;

            using (IDbConnection conn = Connection)
            {
                listaGenero = conn.Query<Genero>("SELECT CODIGO, DESCRICAO FROM TAB_GENERO");
            }

            return listaGenero;
        }
    }
}
