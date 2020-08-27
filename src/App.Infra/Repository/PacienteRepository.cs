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

        public bool Insert(Usuario usuario)
        {
            SQL = new StringBuilder();
            int SCOPE_IDENTITY = 0;



            try
            {


                using (IDbConnection conn = Connection)
                {
                    SQL.AppendLine(string.Format(@"
                        INSERT INTO [dbo].[TBUSUARIO]
                                ([CPF_CNPJ]
                                ,[COD_PERFIL]
                                ,[NOME]
                                ,[SOBRENOME]
                                ,[DT_NASCIMENTO]
                                ,[EMAIL]
                                ,[SENHA]
                                ,[CELULAR]
                                ,[PAIS]
                                ,[CEP]
                                ,[ESTADO]
                                ,[CIDADE]
                                ,[LOGRADOURO]
                                ,[BAIRRO]
                                ,[NUMERO]
                                ,[COMPLEMENTO]) 
                            VALUES
                                ('{0}'
                                ,'{1}'
                                ,'{2}'
                                ,'{3}'
                                ,'{4}'
                                ,'{5}'
                                ,'{6}'
                                ,'{7}'
                                ,'{8}'
                                ,'{9}'
                                ,'{10}'
                                ,'{11}'
                                ,'{12}'
                                ,'{13}'
                                ,'{14}'
                                ,'{15}');

                SELECT CAST(SCOPE_IDENTITY() as int)"


                                , usuario.CPF_CNPJ
                                , usuario.Perfil
                                , usuario.Nome
                                , usuario.Sobrenome
                                , usuario.DataNascimento
                                , usuario.Email
                                , usuario.Senha
                                , usuario.Celular
                                , usuario.Endereco.Pais
                                , usuario.Endereco.CEP
                                , usuario.Endereco.Estado
                                , usuario.Endereco.Cidade
                                , usuario.Endereco.Logradouro
                                , usuario.Endereco.Bairro
                                , usuario.Endereco.Numero
                                , usuario.Endereco.Complemento));

                    SCOPE_IDENTITY = conn.QueryFirstOrDefault<int>(SQL.ToString());

                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                return false;
            }

        }

        public int Update(Usuario usuario)
        {
            SQL = new StringBuilder();
            int exeCount = 0;

            //using (IDbConnection conn = Connection)
            //{

            //    SQL.AppendLine(string.Format(@" INCLUIR COMANDO SQL ", usuario.Nome,
            //                        usuario.Sobrenome,
            //                        usuario.Email,
            //                        usuario.DataNascimento,
            //                        usuario.Celular,
            //                        usuario.Endereco, usuario.CPF_CNPJ));


            //    exeCount = conn.Execute(SQL.ToString());
            //}

            return exeCount;
        }

        public Usuario Select(string cpf)
        {
            Usuario usuario = null;
            SQL = new StringBuilder();
            IEnumerable<Agenda> listaAgenda = null;

            using (IDbConnection conn = Connection)
            {

                SQL.AppendLine(string.Format(@"
                       SELECT  [CPFCNPJ] AS CPF_CNPJ
                              ,[COD_PERFIL] AS COD_PERFIL
                              ,[NOME] AS Nome
                              ,[SOBRENOME] AS Sobrenome
                              ,[DT_NASCIMENTO] AS  DataNascimento
                              ,[EMAIL] AS Email
                              ,[CELULAR] AS Celular
                              ,[PAIS] AS Pais
                              ,[CEP] AS CEP
                              ,[ESTADO] AS Estado
                              ,[CIDADE] AS Cidade
                              ,[LOGRADOURO] AS Logradouro
                              ,[BAIRRO] AS Bairro
                              ,[NUMERO] AS Numero
                              ,[COMPLEMENTO] AS Complemento
                          FROM [dbo].[TBUSUARIO]
                          WHERE CPFCNPJ = '{0}' ", cpf));


                usuario = conn.QueryFirstOrDefault<Usuario>(SQL.ToString());

                if (usuario != null)
                {

                    SQL = new StringBuilder();

                    SQL.AppendLine(string.Format(@"

                                     SELECT USU.NOME + ' ' + USU.SOBRENOME AS Nome
	                                      ,PROF.CRP AS CPF_Paciente
	                                      ,AGENDA.[DATA_AGENDAMENTO] AS DataConsulta
	                                      ,CONVERT(DATETIME,AGENDA.[HORARIO]) AS HorarioConsulta
                                    FROM TBAGENDA AS AGENDA
                                    INNER JOIN TBUSUARIO AS  USU
                                    ON AGENDA.CPF_CNPJPROF = USU.[CPFCNPJ]
									INNER JOIN TBPROFISSIONAL AS PROF
									ON USU.CPFCNPJ = PROF.CPFCNPJ
                                    WHERE AGENDA.[CPF_PACIENTE] =   '{0}'", usuario.CPF_CNPJ));

                    listaAgenda = conn.Query<Agenda>(SQL.ToString());

                    usuario.Agenda = new List<Agenda>();

                    if (listaAgenda.AsList().Count > 0)
                    {
                        usuario.Agenda.AddRange(listaAgenda);
                    }
                }

            }

            return usuario;
        }

        public Usuario Autenticar(string cpf, string senha)
        {
            Usuario usuario = null;
            SQL = new StringBuilder();


            using (IDbConnection conn = Connection)
            {

                SQL.AppendLine(string.Format(@"
                       SELECT  [CPFCNPJ] AS CPF_CNPJ
                              ,[COD_PERFIL] AS CodPerfil
                              ,[NOME] AS Nome
                              ,[SOBRENOME] AS Sobrenome
                              ,[DT_NASCIMENTO] AS  DataNascimento
                              ,[EMAIL] AS Email
                              ,[CELULAR] AS Celular
                              ,[PAIS] AS Pais
                              ,[CEP] AS CEP
                              ,[ESTADO] AS Estado
                              ,[CIDADE] AS Cidade
                              ,[LOGRADOURO] AS Logradouro
                              ,[BAIRRO] AS Bairro
                              ,[NUMERO] AS Numero
                              ,[COMPLEMENTO] AS Complemento
                          FROM [dbo].[TBUSUARIO]
                          WHERE CPFCNPJ = '{0}' AND SENHA = '{1}' ", cpf, senha));


                usuario = conn.QueryFirstOrDefault<Usuario>(SQL.ToString());

            }

            return usuario;

        }

        public bool InsertAgenda(string cpf, string cpf_prof, DateTime dtConsulta, string horario_consulta)
        {
            SQL = new StringBuilder();

            try
            {
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
                                ,CONVERT(DATE,'{2}',103)
                                ,'{3}');"

                                , cpf_prof
                                , cpf
                                , dtConsulta
                                , horario_consulta));

                    conn.Execute(SQL.ToString());

                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                return false;
            }

        }

    }
}