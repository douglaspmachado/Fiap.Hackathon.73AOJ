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

        public int Insert(Usuario usuario)
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
                            ,usuario.CPF_CNPJ
                            ,usuario.Perfil
                            ,usuario.Nome
                            ,usuario.Sobrenome
                            ,usuario.DataNascimento
                            ,usuario.Email
                            ,usuario.Senha
                            ,usuario.Celular
                            ,usuario.Endereco.Pais
                            ,usuario.Endereco.CEP
                            ,usuario.Endereco.Estado
                            ,usuario.Endereco.Cidade
                            ,usuario.Endereco.Logradouro
                            ,usuario.Endereco.Bairro
                            ,usuario.Endereco.Numero
                            ,usuario.Endereco.Complemento));

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

        public Usuario Select(string cpf)
        {
            Usuario usuario = null;
            SQL = new StringBuilder();


            using (IDbConnection conn = Connection)
            {

                SQL.AppendLine(string.Format(@"
                       SELECT  [CPF_CNPJ] AS CPF_CNPJ
                              ,[COD_PERFIL] AS COD_PERFIL
                              ,[NOME] AS NOME
                              ,[SOBRENOME] AS SOBRENOME
                              ,[DT_NASCIMENTO] AS  DT_NASCIMENTO
                              ,[EMAIL] AS EMAIL
                              ,[CELULAR] AS CELULAR
                              ,[PAIS] AS PAIS
                              ,[CEP] AS CEP
                              ,[ESTADO] AS ESTADO
                              ,[CIDADE] AS CIDADE
                              ,[LOGRADOURO] AS LOGRADOURO
                              ,[BAIRRO] AS BAIRRO
                              ,[NUMERO] AS NUMERO
                              ,[COMPLEMENTO] AS COMPLEMENTO
                          FROM [dbo].[TBUSUARIO]
                          WHERE ID = {0} ", cpf));


                usuario = conn.QueryFirstOrDefault<Usuario>(SQL.ToString());

            }

            return usuario;
        }
    }
}