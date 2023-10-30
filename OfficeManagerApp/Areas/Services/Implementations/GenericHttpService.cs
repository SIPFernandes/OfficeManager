using System.Net.Http.Json;
using System.Net;
using OfficeManager.Shared.Response_Model;
using OfficeManagerApp.Areas.Services.Interfaces;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Authorization;

namespace OfficeManagerApp.Areas.Services.Implementations
{
    public class GenericHttpService<TEntity, TRequestModel, TResponseModel> : IGenericHttpService<TEntity, TRequestModel, TResponseModel> 
        where TEntity : class where TResponseModel : class where TRequestModel : class
    {
        protected readonly HttpClient _httpClient;

        public virtual string Url { get; set; } = string.Empty;

        public GenericHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;            
        }

        public GenericHttpService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;

            var accessToken = ((ExternalAuthStateProvider)authenticationStateProvider)
                .AccessToken;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        public async Task Delete(int Id)
        {
            await _httpClient.DeleteAsync(Url + "/" + Id);
        }

        public async Task<IList<TEntity>> GetAll()
        {
            var entities = await _httpClient.GetFromJsonAsync<List<TEntity>>(Url);

            return entities;
        }

        public virtual async Task Insert(TRequestModel entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var response = await _httpClient.PostAsJsonAsync(Url, entity);

            if (response.StatusCode.Equals(HttpStatusCode.BadRequest))
            {
                var content = await response.Content.ReadFromJsonAsync<BadResponseModel>();

                throw new Exception(string.Join(", ", content.Errors));
            }
        }

        public async Task Update(TRequestModel entity, int Id)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var response = await _httpClient.PutAsJsonAsync(Url + "/" + Id, entity);

            if (response.StatusCode.Equals(HttpStatusCode.BadRequest))
            {
                var content = await response.Content.ReadFromJsonAsync<BadResponseModel>();

                throw new Exception(string.Join(",", content.Errors));
            }
        }

        public async Task<TResponseModel> Get(int Id)
        {
            if (Id <= 0)
            {
                throw new ArgumentNullException("entity");
            }

            var entity = await _httpClient.GetFromJsonAsync<TResponseModel>(Url + "/" + Id);

            return entity;
        }

    }
}
