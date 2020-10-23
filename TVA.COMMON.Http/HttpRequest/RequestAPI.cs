using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TVA.COMMON.Http.Type;
using TVA.COMMON.Library.Convert;

namespace TVA.COMMON.Http.HttpRequest
{
    public static class RequestAPI
    {
        public static async Task<ResponseData> ConnectRestAPI(RequestInfo requestInfor, MethodType type)
        {
            ResponseData responseData = new ResponseData();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    if (requestInfor.HeaderValue != null && requestInfor.HeaderValue.AuthorizationType != null && requestInfor.HeaderValue.AuthorizationValue != null)
                    {
                        client.DefaultRequestHeaders.Authorization =
                           new AuthenticationHeaderValue(requestInfor.HeaderValue.AuthorizationType, requestInfor.HeaderValue.AuthorizationValue);
                    }

                    if (requestInfor.HeaderValue != null && requestInfor.HeaderValue.ListHeader != null && requestInfor.HeaderValue.ListHeader.Any())
                    {
                        foreach (var item in requestInfor.HeaderValue.ListHeader)
                        {
                            client.DefaultRequestHeaders.Add(item.Key, item.Value);
                        }
                    }

                    var request = new HttpResponseMessage();
                    switch (type)
                    {
                        case MethodType.GET:
                            request = await client.GetAsync(requestInfor.UrlBase);
                            break;
                        case MethodType.POST:
                            request = await client.PostAsync(requestInfor.UrlBase, new FormUrlEncodedContent(requestInfor.FormValue));
                            break;
                        case MethodType.PUT:
                            request = await client.PutAsync(requestInfor.UrlBase, new FormUrlEncodedContent(requestInfor.FormValue));
                            break;
                        case MethodType.DELETE:
                            request = await client.DeleteAsync(requestInfor.UrlBase);
                            break;
                        default:
                            break;
                    }

                    if (request.StatusCode == HttpStatusCode.OK)
                    {
                        string resultData = request.Content.ReadAsStringAsync().Result;

                        responseData = new ResponseData()
                        {
                            Code = (int)HttpStatusCode.OK,
                            Data = resultData
                        };
                    }
                    else if (request.StatusCode == HttpStatusCode.NoContent)
                    {
                        responseData = new ResponseData()
                        {
                            Code = (int)HttpStatusCode.NoContent,
                            Message = "NoContent",
                        };
                    }
                    else
                    {
                        var errorData = ConvertJson.Deserialize<ErrorData>(request.Content.ReadAsStringAsync().Result);
                        if (errorData != null)
                        {
                            responseData = new ResponseData()
                            {
                                Code = (int)request.StatusCode,
                                Message = errorData.error + " - " + errorData.error_description
                            };
                        }
                        else
                        {
                            responseData = new ResponseData()
                            {
                                Code = (int)request.StatusCode,
                                Message = "Unknown Error: " + request.Content.ReadAsStringAsync().Result
                            };
                        }

                    }
                }
            }
            catch (Exception ex)
            {
            }

            return responseData;
        }
    }
}
