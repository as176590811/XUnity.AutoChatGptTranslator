//using SimpleJSON;
//using System.Net;
//using XUnity.AutoTranslator.Plugin.Core.Endpoints;
//using XUnity.AutoTranslator.Plugin.Core.Endpoints.Http;
//using XUnity.AutoTranslator.Plugin.Core.Utilities;
//using XUnity.AutoTranslator.Plugin.Core.Web;

//internal class ChatGptTranslatorEndpoint : HttpEndpoint
//{
//    private string? _apiKey;
//    private string? _model;
//    private string? _prompt;
//    private const string _url = "https://api.openai.com/v1/chat/completions";
//    private const string _specialCharacters = "---";

//    public override string Id => "ChatGPTTranslate";

//    public override string FriendlyName => "ChatGPT Translate";

//    public override int MaxTranslationsPerRequest => 1;

//    public override int MaxConcurrency => 10;

//    public override void Initialize(IInitializationContext context)
//    {
//        _apiKey = context.GetOrCreateSetting("ChatGPT", "APIKey", "");
//        _model = context.GetOrCreateSetting("ChatGPT", "Model", "gpt-4-turbo");
//        _prompt = context.GetOrCreateSetting("ChatGPT", "Prompt", "");

//        // Add Specific behaviour
//        //_prompt = $"{_prompt} Only separate the translated dialogues with '{_specialCharacters}', without adding additional line breaks.";

//        // if the plugin cannot be enabled, simply throw so the user cannot select the plugin
//        if (string.IsNullOrEmpty(_apiKey)) throw new Exception("The ChatGPT endpoint requires an API key which has not been provided.");
//    }

//    public override void OnCreateRequest(IHttpRequestCreationContext context)
//    {
//        var requestData = GetRequestData(context);

//        var request = new XUnityWebRequest("POST", _url, requestData);

//        request.Headers[HttpRequestHeader.Authorization] = $"Bearer {_apiKey}";
//        request.Headers[HttpRequestHeader.ContentType] = "application/json";

//        //WriteLogFile(requestData, "request");

//        context.Complete(request);
//    }

//    private string GetRequestData(IHttpRequestCreationContext context)
//    {        
//        // Have to use SimpleJSON because of compatability
//        // Create the messages array
//        JSONArray messagesArray = new JSONArray();
//        messagesArray.Add(new JSONObject { ["role"] = "system", ["content"] = _prompt });

//        // Support Batch mode
//        foreach (var text in context.UntranslatedTexts)
//            messagesArray.Add(new JSONObject { ["role"] = "user", ["content"] = text });

//        var requestBody = new JSONObject
//        {
//            ["model"] = _model,
//            ["temperature"] = 0.1,
//            ["max_tokens"] = 1000,
//            ["top_p"] = 1,
//            ["frequency_penalty"] = 0,
//            ["presence_penalty"] = 0,
//            ["messages"] = messagesArray,
//        };

//        // Serialize to string
//        return requestBody.ToString();
//    }

//    public override void OnExtractTranslation(IHttpTranslationExtractionContext context)
//    {
//        var data = context.Response.Data;

//        //WriteLogFile(data, "response");

//        // Parse the response JSON
//        var jsonResponse = JSON.Parse(data);

//        if (MaxTranslationsPerRequest == 1)
//        {
//            context.Complete(GetTranslatedText(jsonResponse, 0));
//        }
//        else
//        {
//            //var translations = new List<string>();

//            //// Get first response
//            //var responses = GetTranslatedText(jsonResponse, 0).Split(new string[] { _specialCharacters }, StringSplitOptions.None);

//            //foreach (var repsonse in responses)
//            //    translations.Add(repsonse);

//            //context.Complete(translations.ToArray());
//        }
//    }

//    private static string GetTranslatedText(JSONNode jsonResponse, int index)
//    {
//        var rawString =  jsonResponse["choices"][index]["message"]["content"].ToString();

//        //Trim the quotes and Unescape
//        rawString = JsonHelper.Unescape(rawString.Substring(1, rawString.Length - 2));
//        return rawString;
//    }

//    private void WriteLogFile(string content, string prefix)
//    {
//        string dateTimeString = DateTime.Now.ToString("yyyyMMdd_HHmmss");
//        File.WriteAllText($"C:\\test\\{prefix}_{dateTimeString}.txt", content);
//    }
//}