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
                listaAbordagens = conn.Query<Abordagens>("SELECT COD_ABORDAGEM AS Codigo, DESCRICAO As Descricao FROM TBTIPOABORD");
            }

            return listaAbordagens;
        }

        public IEnumerable<Atendimento> GetAtendimento()
        {
            IEnumerable<Atendimento> listaAtendimento;

            using (IDbConnection conn = Connection)
            {
                listaAtendimento = conn.Query<Atendimento>("SELECT COD_ATENDIMENTO as Codigo, DESCRICAO as Descricao FROM TBTIPOATEND");
            }

            return listaAtendimento;
        }

        public IEnumerable<Genero> GetGenero()
        {
            IEnumerable<Genero> listaGenero;

            using (IDbConnection conn = Connection)
            {
                listaGenero = conn.Query<Genero>("SELECT COD_GENERO as Codigo, DESCRICAO as Descricao FROM TBTIPOGENERO");
            }

            return listaGenero;
        }

        public IEnumerable<Perfil> GetPerfil()
        {
            IEnumerable<Perfil> listaPerfil;

            using (IDbConnection conn = Connection)
            {
                listaPerfil = conn.Query<Perfil>("SELECT COD_PERFIL as Codigo, DESCRICAO as Descricao FROM TBTIPOPERFIL");
            }

            return listaPerfil;
        }

    }
}
