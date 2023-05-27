using Finaltouch.QuickGraph.Web.Shared;
using Finaltouch.QuickGraph.Web.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;
using System.Net.Http.Json;

namespace Finaltouch.QuickGraph.Web.Client.Pages
{
    //https://bbbootstrap.com/snippets/payment-form-three-different-payment-options-13285516
    //https://stackoverflow.com/questions/65882646/how-to-define-templated-component-in-renderfragment-with-its-context-available
    public partial class Index
    {
        [Inject]
        private HttpClient Client { get; set; } = default!;
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

        public async Task<NamesResult?> GetNames(GridMetaData metaData)
        {
            var response = await Client.PostAsJsonAsync("api/BabyNames/GetBabyNames", metaData);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<NamesResult>();
            }
            return null;
        }
    }

}
