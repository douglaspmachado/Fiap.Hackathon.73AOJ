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
                                ,'{15}'
                                ,'{16}');
                SELECT CAST(SCOPE_IDENTITY() as int)"
                            ,psicologo.CPF_CNPJ
                            ,psicologo.Perfil
                            ,psicologo.Nome
                            ,psicologo.Sobrenome
                            ,psicologo.DataNascimento
                            ,psicologo.Email
                            ,psicologo.Senha
                            ,psicologo.Celular
                            ,psicologo.Endereco.Pais
                            ,psicologo.Endereco.CEP
                            ,psicologo.Endereco.Estado
                            ,psicologo.Endereco.Cidade
                            ,psicologo.Endereco.Logradouro
                            ,psicologo.Endereco.Bairro
                            ,psicologo.Endereco.Numero
                            ,psicologo.Endereco.Complemento));
                
                SQL.AppendLine(string.Format(@"
                        INSERT INTO [dbo].[TBPROFISSIONAL]
                                ([CPF_CNPJ]
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
                                ,'{8}'
                                ,'{9}');
                SELECT CAST(SCOPE_IDENTITY() as int)"
                            ,psicologo.CPF_CNPJ
                            ,psicologo.CRP
                            ,psicologo.CodGraduacao
                            ,psicologo.InstituicaoEnsino
                            ,psicologo.Curso
                            ,psicologo.AnoInicio
                            ,psicologo.AnoTermino
                            ,psicologo.AreaEstudo
                            ,psicologo.DescricaoAtuacao));


                SCOPE_IDENTITY = conn.QueryFirstOrDefault<int>(SQL.ToString());

            }

            return SCOPE_IDENTITY;
        }

        public int Update(Psicologo psicologo)
        {

            SQL = new StringBuilder();
            int exeCount = 0;

            using (IDbConnection conn = Connection)
            {

                SQL.AppendLine(string.Format(@" INSERIR COMANDO SQL ",
                                    psicologo.Nome,
                                    psicologo.Sobrenome,
                                    psicologo.Email,
                                    psicologo.Endereco,
                                    psicologo.DescricaoAtuacao,
                                    psicologo.InstituicaoEnsino,
                                    psicologo.Abordagens,
                                    psicologo.AnoInicio,
                                    psicologo.AreaEstudo,
                                    psicologo.Atendimento,
                                    psicologo.Celular,
                                    psicologo.CodGraduacao,
                                    psicologo.Curso,
                                    psicologo.DataNascimento,
                                    psicologo.DescricaoAtuacao, psicologo.CPF_CNPJ));
                                    


                exeCount = conn.Execute(SQL.ToString());
            }

            return exeCount;
        }

        public Psicologo Select(string cpf)
        {
            Psicologo psicologo = null;
            SQL = new StringBuilder();

            using (IDbConnection conn = Connection)
            {

                SQL.AppendLine(string.Format(@" ", cpf));


                psicologo = conn.QueryFirstOrDefault<Psicologo>(SQL.ToString());

            }

            return psicologo;
        }
    }
}