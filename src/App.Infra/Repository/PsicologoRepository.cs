using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using App.Domain.Entity;
using Dapper;
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

        public int Insert(Psicologo psicologo)
        {
            SQL = new StringBuilder();
            int SCOPE_IDENTITY = 0;


            using (IDbConnection conn = Connection)
            {
                SQL.AppendLine(string.Format(@"
                                                //INSERT AQUI

                                                SELECT CAST(SCOPE_IDENTITY() as int)"));


                SCOPE_IDENTITY = conn.QueryFirstOrDefault<int>(SQL.ToString());

            }

            return SCOPE_IDENTITY;
        }

        public int Update(Psicologo usuario)
        {
            throw new NotImplementedException();
        }

        public Psicologo Get(string cpf)
        {
            Psicologo psicologo = null;
            SQL = new StringBuilder();


            using (IDbConnection conn = Connection)
            {

                SQL.AppendLine(string.Format(@"  ", cpf));


                psicologo = conn.QueryFirstOrDefault<Psicologo>(SQL.ToString());

            }

            return psicologo;
        }
    }
}