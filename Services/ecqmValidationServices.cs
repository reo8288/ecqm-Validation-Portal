using ecqmValidation.Library.Helpers;
using ecqmValidation.Library.Models;

namespace ecqmValidation.UI.Services
{
	public class ecqmValidationService
	{
		private readonly HttpClient _httpClient;
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IConfiguration _configuration;
		public ecqmValidationService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
		{
			_configuration = configuration;
			_httpClientFactory = httpClientFactory;
			_httpClient = _httpClientFactory.CreateClient();
			_httpClient.BaseAddress = new Uri(_configuration.GetSection("Services").GetValue<string>("ecqmValidation"));
		}

		public async Task<IEnumerable<MeasureModel>> GetMeasures()
		{
			var result = await _httpClient.GetFromJsonAsync<IEnumerable<MeasureModel>>("ecqmValidation/getmeasures").ConfigureAwait(false);
			return result.ToList();
		}
		public async Task<IEnumerable<ElementModel>> GetElements()
		{
			var result = await _httpClient.GetFromJsonAsync<IEnumerable<ElementModel>>("ecqmValidation/getelements");
			return result.ToList();
		}
		public async Task<IEnumerable<MismatchQuestionModel>> GetUnansweredQuestionsForReview(bool showApproved = false, bool showCancelled = false)
		{
			var result = await _httpClient.GetFromJsonAsync<IEnumerable<MismatchQuestionModel>>($"ecqmValidation/getquestionsforreview?showApproved={showApproved}&&showCancelled={showCancelled}");
			return result.ToList();
		}
		public async Task<IEnumerable<MismatchQuestionModel>> GetUnansweredQuestionsForUser(int userID)
		{
			var result = await _httpClient.GetFromJsonAsync<IEnumerable<MismatchQuestionModel>>($"ecqmValidation/getquestionsforuser?userID={userID}");
			return result.ToList();
		}
		public async Task<MismatchQuestionModel> GetUnansweredQuestionByID(int questionID)
		{
			return await _httpClient.GetFromJsonAsync<MismatchQuestionModel>($"ecqmValidation/getquestionbyid?questionID={questionID}");
		}
		public async Task<int> UpsertMismatchQuestion(MismatchQuestionModel mismatchQuestion)
		{
			Throw.Exception.IfNull(mismatchQuestion);
			var result = await _httpClient.PostAsJsonAsync<MismatchQuestionModel>("ecqmValidation/upsertmismatchquestion", mismatchQuestion).ConfigureAwait(false);
			return result.Content.ReadFromJsonAsync<int>().Result;
		}
		public async Task<int> UpdateQuestionStatus(MismatchQuestionBaseModel mismatchQuestion)
		{
			var result = await _httpClient.PostAsJsonAsync<MismatchQuestionBaseModel>("ecqmValidation/updatequestionstatus", mismatchQuestion).ConfigureAwait(false);
			return result.Content.ReadFromJsonAsync<int>().Result;
		}
		public async Task<int> SubmitQuestion(MismatchQuestionBaseModel mismatchQuestion)
		{
			var result = await _httpClient.PostAsJsonAsync<MismatchQuestionBaseModel>("ecqmValidation/submitquestion", mismatchQuestion).ConfigureAwait(false);
			return result.Content.ReadFromJsonAsync<int>().Result;
		}
	}
}
