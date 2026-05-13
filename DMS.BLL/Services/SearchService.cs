
using DMS.BLL.DTOs;
using DMS.BLL.Interfaces;
using DMS.DAL.Interfaces;
using DMS.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace DMS.BLL.Services
{
    public class SearchService : ISearchService
    {
        private readonly IVaultRepository  _vaultRepository;

        private readonly ISearchRepository _repository;

        private readonly IConfiguration _configuration;

        public SearchService(IVaultRepository vaultRepository,ISearchRepository repository,IConfiguration configuration)
        {
            _vaultRepository = vaultRepository;
            _repository = repository;
            _configuration = configuration;
        }

        private async Task<string>BuildVaultConnection(int vaultId)
        {
            var vault =  await _vaultRepository.GetByIdAsync(vaultId);
            var masterConnection = _configuration.GetConnectionString("MasterConnection");
            var builder =  new SqlConnectionStringBuilder(masterConnection);
            builder.InitialCatalog =  vault.DatabaseName;
            return builder.ConnectionString;
        }

        public async Task<List<SearchResult>> SearchAsync(int vaultId,string keyword)
        {
            var connectionString = await BuildVaultConnection(vaultId);

            return await _repository.SearchAsync(connectionString,keyword);
        }
    }
}
