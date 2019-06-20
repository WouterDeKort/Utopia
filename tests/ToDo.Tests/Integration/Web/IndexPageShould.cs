using AngleSharp.Html.Dom;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Todo.Web;
using ToDo.Tests.Integration.Web.Helpers;
using Xunit;

namespace ToDo.Tests.Integration.Web
{
	public class IndexPageShould : IClassFixture<CustomWebApplicationFactory<Startup>>
	{
		private readonly CustomWebApplicationFactory<Startup> _factory;
		private readonly HttpClient _client;

		public IndexPageShould(CustomWebApplicationFactory<Startup> factory)
		{
			_client = factory.CreateClient();
		}

		[Fact]
		public async Task ReturnViewWithCorrectMessage()
		{
			var defaultPage = await _client.GetAsync("/");
			var content = await HtmlHelpers.GetDocumentAsync(defaultPage);

			var brand = content.QuerySelector(".navbar-brand");
			Assert.Contains("Todo", brand.InnerHtml);

		}

		[Fact(Skip = "Enable authentication from integration tests")]
		public async Task NavigateToDetailsPage()
		{
			var loginPage = await _client.GetAsync("/Identity/Account/Login");

			//_client.PostAsync()

			var defaultPage = await _client.GetAsync("/");
			var content = await HtmlHelpers.GetDocumentAsync(defaultPage);

			var detailsLink = (IHtmlAnchorElement)content.QuerySelector(".item-details-title-1");

			Assert.Equal("/Details", detailsLink.PathName);
			Assert.Equal("?id=1", detailsLink.Search);

		}
	}
}