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
            SQL = new StringBuilder();
            int SCOPE_IDENTITY = 0;


            using (IDbConnection conn = Connection)
            {
                SQL.AppendLine(string.Format(@"
                        INSERT INTO [dbo].[TBAGENDA]
                                ([CPF_CNPJPROF]
                                ,[CPF_PACIENTE]
                                ,[DATA_AGENDAMENTO]
                                ,[HORARIO]) 
                            VALUES
                                ('{0}'
                                ,'{1}'
                                ,'{2}'
                                ,'{3}');
                SELECT CAST(SCOPE_IDENTITY() as int)"
                            ,agenda.CPF_CNPJPsicologo
                            ,agenda.CPF_Paciente
                            ,agenda.DataConsulta
                            ,agenda.HorarioConsulta));

                SCOPE_IDENTITY = conn.QueryFirstOrDefault<int>(SQL.ToString());
                
            }

            return SCOPE_IDENTITY;
        }

        public Agenda Select(string cpf_cnpjPsicologo)
        {
            Agenda agenda = null;
            SQL = new StringBuilder();


            using (IDbConnection conn = Connection)
            {

                SQL.AppendLine(string.Format(@"
                       SELECT  [CPF_CNPJPROF] AS CPF_CNPJPROF
                              ,[CPF_PACIENTE] AS CPF_PACIENTE
                              ,[DATA_AGENDAMENTO] AS DATA_AGENDAMENTO
                              ,[HORARIO] AS HORARIO
                          FROM [dbo].[TBAGENDA]
                          WHERE CPF_CNPJPROF = {0} ", 
                          cpf_cnpjPsicologo));


                agenda = conn.QueryFirstOrDefault<Agenda>(SQL.ToString());

            }

            return agenda;
        }

        public int Update(Agenda agenda)
        {
            throw new NotImplementedException();
        }
    }
}