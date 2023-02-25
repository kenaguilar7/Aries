﻿using AriesContador.Core.Models.Accounts;
using CapaEntidad.Entidades.JournalEntries;
using AriesContador.Core.Models.PostingPeriods;
using AriesContador.Core.Repositories;
using AriesContador.Data.Internal.DataAccess;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace AriesContador.Data.Repositories
{
    public class PostingPeriodRepository : IPostingPeriodRepository
    {
        private readonly IConnectionString _connectionString;
        public PostingPeriodRepository(IConnectionString connectionString)
        {
            this._connectionString = connectionString;
        }

        public void Add(PostingPeriod entity)
        {
            MySqlDataAccess dataAccess = new MySqlDataAccess(_connectionString);
            entity.Id = dataAccess.SaveData<PostingPeriod, int>("SP_InsertPostingPeriod", entity);
        }

        public void ClosePostingPeriod(PostingPeriodEndClosing postingPeriod)
        {
            MySqlDataAccess dataAccess = new MySqlDataAccess(_connectionString);
            var saveMyPostingPeriod = postingPeriod.PostingPeriods;
            try
            {
                dataAccess.StartTransaction();
                foreach (var period in saveMyPostingPeriod)
                {
                    dataAccess.SaveDataInTransaction<PostingPeriod>("SP_ClosePeriod", period);
                }

                postingPeriod.PostingPeriods = null;
                postingPeriod.Id = dataAccess.SaveDataInTransaction<PostingPeriodEndClosing, int>(
                    "SP_InsertClosingPostingPeriod",
                    postingPeriod);
                postingPeriod.PostingPeriods = saveMyPostingPeriod;

                dataAccess.CommitTransaction();
            }
            catch (Exception)
            {
                postingPeriod.PostingPeriods = saveMyPostingPeriod;
                dataAccess.RollBackTransaction();
                throw;
            }

        }

        public IEnumerable<PostingPeriod> FindByCompanyId(string companyId)
        {
            MySqlDataAccess dataAccess = new MySqlDataAccess(_connectionString);
            var output = dataAccess.LoadData<PostingPeriod, dynamic>("SP_GetAllPostingPeriod", new { CompanyId = companyId });
            return output;
        }

        public void Remove(PostingPeriod entity)
        {
            throw new NotImplementedException();
        }

        public void Update(PostingPeriod entity)
        {
            throw new NotImplementedException();
        }
    }
}
