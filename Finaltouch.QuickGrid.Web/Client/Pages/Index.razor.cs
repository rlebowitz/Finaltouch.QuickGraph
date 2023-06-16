using Finaltouch.QuickGrid.Web.Shared;
using Finaltouch.QuickGrid.Web.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace Finaltouch.QuickGrid.Web.Client.Pages
{
    //https://bbbootstrap.com/snippets/payment-form-three-different-payment-options-13285516
    //https://stackoverflow.com/questions/65882646/how-to-define-templated-component-in-renderfragment-with-its-context-available
    public partial class Index
    {
        [Inject]
        private HttpClient Client { get; set; } = default!;
        private Uri RequestUri { get; set; } = default!;
        private GridItemsProvider<Babyname>? NamesProvider { get; set; }

        private PaginationState Pagination = new PaginationState { ItemsPerPage = 10 };
        private QuickGrid<Babyname>? Grid { get; set; }
        private string? FilterText { get; set; }
        private string? Filter
        {
            get { return FilterText; }
            set
            {
                FilterText = value;
                if (Grid != null)
                {
                    Task.Run(Grid.RefreshDataAsync);
                }
            }
        }

        protected override void OnInitialized()
        {
            NamesProvider = Provider;
            RequestUri = new Uri(Client.BaseAddress!, "api/BabyNames/GetBabyNames");
        }

        private async ValueTask<GridItemsProviderResult<Babyname>> Provider(GridItemsProviderRequest<Babyname> request)
        {
            GridItemsProviderResult<Babyname> result = default;
            var metaData = new GridMetaData
            {
                StartIndex = request.StartIndex,
                Count = request.Count,
                SortProperties = request.GetSortByProperties().ToArray(),
                Filter = new Filter { Field = "state", Operator = Operator.Contains, Value = FilterText },
            };
            var namesResult = await GetNames(metaData);
            if (namesResult != null)
            {
                result = new GridItemsProviderResult<Babyname>()
                {
                    Items = namesResult.BabyNames != null ? namesResult.BabyNames : Array.Empty<Babyname>(),
                    TotalItemCount = namesResult.Count
                };
            }
            return result;
        }

        //https://stackoverflow.com/questions/43421126/how-to-use-httpclient-to-send-content-in-body-of-get-request
        public async Task<NamesResult?> GetNames(GridMetaData metaData)
        {
            var content = JsonSerializer.Serialize(metaData);
            var builder = new UriBuilder(RequestUri);
            builder.Query = "metaData=" + Base64Url.Encode(content);
            var response = await Client.GetAsync(builder.Uri);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<NamesResult>();
            }
            return null;
        }
    }

}
