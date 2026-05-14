using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.BLL.Services
{
    using DMS.BLL.DTOs;
    using DMS.BLL.Interfaces;
    using DMS.DAL.Interfaces;
    using DMS.Domain.Entities;
    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Configuration;

    public class FullTextSearchService
        : IFullTextSearchService
    {
        private readonly IConfiguration
            _configuration;

        private readonly IVaultRepository
            _vaultRepository;

        private readonly IOCRRepository
            _ocrRepository;

        private readonly IFullTextSearchRepository
            _repository;

        public FullTextSearchService(
            IConfiguration configuration,

            IVaultRepository vaultRepository,

            IOCRRepository ocrRepository,

            IFullTextSearchRepository repository)
        {
            _configuration =
                configuration;

            _vaultRepository =
                vaultRepository;

            _ocrRepository =
                ocrRepository;

            _repository =
                repository;
        }

        private async Task<string>
            BuildVaultConnection(
                int vaultId)
        {
            var vault =
                await _vaultRepository
                    .GetByIdAsync(vaultId);

            var masterConnection =
                _configuration
                    .GetConnectionString(
                        "MasterConnection");

            var builder =
                new SqlConnectionStringBuilder(
                    masterConnection);

            builder.InitialCatalog =
                vault.DatabaseName;

            return builder.ConnectionString;
        }

        public async Task BuildIndexAsync(
            int vaultId,
            int documentId)
        {
            var connectionString =
                await BuildVaultConnection(
                    vaultId);

            var ocrText =
                await _ocrRepository
                    .GetOCRTextAsync(
                        connectionString,
                        documentId);

            if (string.IsNullOrWhiteSpace(
                    ocrText))
            {
                return;
            }

            await _repository
                .CreateIndexAsync(
                    connectionString,

                    new SearchIndex
                    {
                        DocumentID =
                            documentId,

                        IndexedText =
                            ocrText
                    });

            var words =
                ocrText
                    .Split(' ',
                        StringSplitOptions
                            .RemoveEmptyEntries)

                    .Distinct();

            foreach (var word in words)
            {
                await _repository
                    .CreateKeywordAsync(
                        connectionString,

                        new SearchKeyword
                        {
                            DocumentID =
                                documentId,

                            KeywordText =
                                word
                        });
            }
        }

        public async Task<List<SearchResponse>>SearchAsync(SearchRequestDto dto)
        {
            var connectionString = await BuildVaultConnection(dto.VaultID);

            return await _repository.SearchAsync(connectionString,dto.Keyword);
        }
    }
}
