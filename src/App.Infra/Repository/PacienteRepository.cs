using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using App.Application.Interfaces;
using App.Domain.Entity;
using Dapper;
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
            SQL = new StringBuilder();
            int SCOPE_IDENTITY = 0;


            using (IDbConnection conn = Connection)
            {
                SQL.AppendLine(string.Format(@"SELECT CAST(SCOPE_IDENTITY() as int)"));


                SCOPE_IDENTITY = conn.QueryFirstOrDefault<int>(SQL.ToString());
                
            }

            return SCOPE_IDENTITY;

        }

        public int Update(Usuario usuario)
        {
            SQL = new StringBuilder();
            int exeCount = 0;

            using (IDbConnection conn = Connection)
            {

                SQL.AppendLine(string.Format(@" INCLUIR COMANDO SQL ", usuario.Nome,
                                    usuario.Sobrenome,
                                    usuario.Email,
                                    usuario.DataNascimento,
                                    usuario.Celular,
                                    usuario.Endereco, usuario.CPF_CNPJ));


                exeCount = conn.Execute(SQL.ToString());
            }

            return exeCount;
        }

        public Usuario Select(int cpf)
        {
            Usuario usuario = null;
            SQL = new StringBuilder();


            using (IDbConnection conn = Connection)
            {

                SQL.AppendLine(string.Format(@"  ", cpf));


                usuario = conn.QueryFirstOrDefault<Usuario>(SQL.ToString());

            }

            return usuario;
        }
    }
}