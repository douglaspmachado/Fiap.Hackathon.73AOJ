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
    public class PsicologoRepository : IPsicologoRepository
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
        public IEnumerable<Psicologo> GetAll()
        {
            IEnumerable<Psicologo> psicologos;
            SQL = new StringBuilder();

            using (IDbConnection conn = Connection)
            {
                SQL.AppendLine(@"

                             SELECT  U.CPFCNPJ AS CPF_CNPJ
                              ,U.COD_PERFIL AS CodigoPerfil
							  ,PERF.DESCRICAO as DescricaoPerfil
                              ,U.NOME AS Nome
                              ,U.SOBRENOME AS Sobrenome
                              ,U.DT_NASCIMENTO AS  DataNascimento
                              ,U.EMAIL AS EMAIL
                              ,U.CELULAR AS Celular
                              ,U.PAIS AS Pais
                              ,U.CEP AS CEP
                              ,U.ESTADO AS Estado
                              ,U.CIDADE AS Cidade
                              ,U.LOGRADOURO AS Logradouro
                              ,U.BAIRRO AS Bairro
                              ,U.NUMERO AS Numero
                              ,U.COMPLEMENTO AS Complemento
                              ,P.CRP AS CRP
                              ,P.COD_GRADUACAO AS CodigoGraduacao
							  ,GRAD.DESCRICAO AS DescricaoGraduacao
                              ,P.INSTITUICAO_ENSINO AS InstituicaoEnsino
                              ,P.CURSO AS Curso
                              ,P.ANO_INICIO AS AnoInicio
                              ,P.ANO_FIM AS AnoTermino
                              ,P.AREA_ESTUDO AS AreaEstudo
                              ,P.DESCRICAO AS DescricaoAtuacao
							  

                          FROM dbo.TBUSUARIO AS U
						  INNER JOIN dbo.TBPROFISSIONAL AS P
                          ON U.CPFCNPJ = P.CPFCNPJ
						  INNER JOIN DBO.TBTIPOPERFIL AS PERF
						  ON U.COD_PERFIL = PERF.COD_PERFIL
						  INNER JOIN DBO.TBTIPOGRADUACAO AS GRAD
						  ON P.COD_GRADUACAO = GRAD.COD_GRADUACAO

                              ");

                psicologos = conn.Query<Psicologo>(SQL.ToString());
            }

            return psicologos;
        }

        public bool Insert(Psicologo psicologo)
        {
            SQL = new StringBuilder();
            int SCOPE_IDENTITY = 0;



            try
            {



                using (IDbConnection conn = Connection)
                {
                    SQL.AppendLine(string.Format(@"
                        INSERT INTO [dbo].[TBUSUARIO]
                                ([CPFCNPJ]
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
                                ,'{15}');"

                                , psicologo.CPF_CNPJ
                                , psicologo.Perfil.CodigoPerfil
                                , psicologo.Nome
                                , psicologo.Sobrenome
                                , psicologo.DataNascimento.Date.Date
                                , psicologo.Email
                                , psicologo.Senha
                                , psicologo.Celular
                                , psicologo.Endereco.Pais
                                , psicologo.Endereco.CEP
                                , psicologo.Endereco.Estado
                                , psicologo.Endereco.Cidade
                                , psicologo.Endereco.Logradouro
                                , psicologo.Endereco.Bairro
                                , psicologo.Endereco.Numero
                                , psicologo.Endereco.Complemento));


                    conn.Execute(SQL.ToString());

                    SQL = new StringBuilder();

                    SQL.AppendLine(string.Format(@"
                        INSERT INTO [dbo].[TBPROFISSIONAL]
                                ([CPFCNPJ]
                                ,[CRP]
                                ,[COD_GRADUACAO]
                                ,[INSTITUICAO_ENSINO]
                                ,[CURSO]
                                ,[ANO_INICIO]
                                ,[ANO_FIM]
                                ,[AREA_ESTUDO]
                                ,[DESCRICAO]) 
                            VALUES
                                ('{0}'
                                ,'{1}'
                                ,'{2}'
                                ,'{3}'
                                ,'{4}'
                                ,'{5}'
                                ,'{6}'
                                ,'{7}'
                                ,'{8}');

                SELECT CAST(SCOPE_IDENTITY() as int)"

                                , psicologo.CPF_CNPJ
                                , psicologo.CRP
                                , psicologo.CodGraduacao
                                , psicologo.InstituicaoEnsino
                                , psicologo.Curso
                                , psicologo.AnoInicio
                                , psicologo.AnoTermino
                                , psicologo.AreaEstudo
                                , psicologo.DescricaoAtuacao));



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

        public int Update(Psicologo psicologo)
        {

            SQL = new StringBuilder();
            int exeCount = 0;

            //using (IDbConnection conn = Connection)
            //{

            //    SQL.AppendLine(string.Format(@" INSERIR COMANDO SQL ",
            //                        psicologo.Nome,
            //                        psicologo.Sobrenome,
            //                        psicologo.Email,
            //                        psicologo.Endereco,
            //                        psicologo.DescricaoAtuacao,
            //                        psicologo.InstituicaoEnsino,
            //                        psicologo.Abordagens,
            //                        psicologo.AnoInicio,
            //                        psicologo.AreaEstudo,
            //                        psicologo.Atendimento,
            //                        psicologo.Celular,
            //                        psicologo.CodGraduacao,
            //                        psicologo.Curso,
            //                        psicologo.DataNascimento,
            //                        psicologo.DescricaoAtuacao, psicologo.CPF_CNPJ));



            //    exeCount = conn.Execute(SQL.ToString());
            //}

            return exeCount;
        }

        public Psicologo Select(string cpf)
        {
            Psicologo psicologo = null;
            IEnumerable<Abordagens> listaAbordagem = null;
            IEnumerable<Atendimento> listaAtendimento = null;
            IEnumerable<Agenda> listaAgenda = null;

            try
            {


                SQL = new StringBuilder();

                using (IDbConnection conn = Connection)
                {

                    SQL.AppendLine(string.Format(@"

                       SELECT  U.CPFCNPJ AS CPF_CNPJ
                              ,U.COD_PERFIL AS CodigoPerfil
							  ,PERF.DESCRICAO as DescricaoPerfil
                              ,U.NOME AS Nome
                              ,U.SOBRENOME AS Sobrenome
                              ,U.DT_NASCIMENTO AS  DataNascimento
                              ,U.EMAIL AS EMAIL
                              ,U.CELULAR AS Celular
                              ,U.PAIS AS Pais
                              ,U.CEP AS CEP
                              ,U.ESTADO AS Estado
                              ,U.CIDADE AS Cidade
                              ,U.LOGRADOURO AS Logradouro
                              ,U.BAIRRO AS Bairro
                              ,U.NUMERO AS Numero
                              ,U.COMPLEMENTO AS Complemento
                              ,P.CRP AS CRP
                              ,P.COD_GRADUACAO AS CodigoGraduacao
							  ,GRAD.DESCRICAO AS DescricaoGraduacao
                              ,P.INSTITUICAO_ENSINO AS InstituicaoEnsino
                              ,P.CURSO AS Curso
                              ,P.ANO_INICIO AS AnoInicio
                              ,P.ANO_FIM AS AnoTermino
                              ,P.AREA_ESTUDO AS AreaEstudo
                              ,P.DESCRICAO AS DescricaoAtuacao
							  

                          FROM dbo.TBUSUARIO AS U
						  INNER JOIN dbo.TBPROFISSIONAL AS P
                          ON U.CPFCNPJ = P.CPFCNPJ
						  INNER JOIN DBO.TBTIPOPERFIL AS PERF
						  ON U.COD_PERFIL = PERF.COD_PERFIL
						  INNER JOIN DBO.TBTIPOGRADUACAO AS GRAD
						  ON P.COD_GRADUACAO = GRAD.COD_GRADUACAO
                          WHERE U.CPFCNPJ = '{0}' ", cpf));


                    psicologo = conn.QueryFirstOrDefault<Psicologo>(SQL.ToString());

                    //Se retornou psicologo, busco as informações de atendimento, abordagem e agenda
                    if (psicologo != null)
                    {

                        SQL = new StringBuilder();

                        SQL.AppendLine(string.Format(@"

                                   SELECT TIPO_ABORD.COD_ABORDAGEM AS CodigoAbordagem
	                                      ,TIPO_ABORD.DESCRICAO AS DescricaoAbordagem
                                    FROM TBTIPOABORD AS TIPO_ABORD
                                    INNER JOIN TBPROF_TIPOABORD AS PROF_TIPO_ABORD
                                    ON TIPO_ABORD.COD_ABORDAGEM = PROF_TIPO_ABORD.COD_ABORDAGEM
                                    WHERE PROF_TIPO_ABORD.CRP = '{0}'", psicologo.CRP));

                        listaAbordagem = conn.Query<Abordagens>(SQL.ToString());


                        SQL = new StringBuilder();

                        SQL.AppendLine(string.Format(@"

                                   SELECT TIPO_ATEND.COD_ATENDIMENTO AS CodigoAtendimento
	                                      ,TIPO_ATEND.DESCRICAO AS DescricaoAtendimento
                                    FROM TBTIPOATEND AS TIPO_ATEND
                                    INNER JOIN TBPROF_TIPOATEND AS PROF_TIPO_ATEND
                                    ON PROF_TIPO_ATEND.COD_ATENDIMENTO = PROF_TIPO_ATEND.COD_ATENDIMENTO
                                    WHERE PROF_TIPO_ATEND.CRP =  '{0}'", psicologo.CRP));

                        listaAtendimento = conn.Query<Atendimento>(SQL.ToString());


                        SQL = new StringBuilder();

                        SQL.AppendLine(string.Format(@"

                                   SELECT USU.NOME + ' ' + USU.SOBRENOME AS Nome
	                                      ,AGENDA.[CPF_PACIENTE] AS CPF_Paciente
	                                      ,AGENDA.[DATA_AGENDAMENTO] AS DataConsulta
	                                      ,CONVERT(DATETIME,AGENDA.[HORARIO]) AS HorarioConsulta
                                    FROM TBAGENDA AS AGENDA
                                    INNER JOIN TBUSUARIO USU
                                    ON AGENDA.[CPF_PACIENTE] = USU.[CPFCNPJ]
                                    WHERE CPF_CNPJPROF =  '{0}'", psicologo.CPF_CNPJ));

                        listaAgenda = conn.Query<Agenda>(SQL.ToString());

                        psicologo.Agenda = new List<Agenda>();

                        if (listaAbordagem.AsList().Count > 0)
                        {
                            //psicologo.Abordagens.AsList().AddRange(listaAbordagem);
                        }

                        if (listaAtendimento.AsList().Count > 0)
                        {
                            
                            //psicologo.Atendimentos.AsList().AddRange(listaAtendimento);
                        }

                        if (listaAgenda.AsList().Count > 0)
                        {
                            psicologo.Agenda = new List<Agenda>();
                            psicologo.Agenda.AddRange(listaAgenda);
                        }

                    }

                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
                psicologo = null;
            }



            return psicologo;


        }

        public bool Autenticar(string cpf, string senha)
        {
            Usuario usuario = null;
            SQL = new StringBuilder();


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
                          WHERE CPFCNPJ = {0} AND SENHA = {1} ", cpf, senha));


                usuario = conn.QueryFirstOrDefault<Usuario>(SQL.ToString());



            }

            return usuario == null ? false : true;
        }

    }
}