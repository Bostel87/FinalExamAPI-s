using FinalExam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;


namespace FinalExam.Handlers
{
    public class KeyHandler : DelegatingHandler
    {
        //Api Key desde la BD
        private readonly TestEntities db = new TestEntities();

        //Metodo para obtener apikey desde base de datos
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            var validapi = false;



            IEnumerable<string> requestHeader;
            var checkApi = request.Headers.TryGetValues("Key", out requestHeader);
            if (checkApi)
            {
                var HttpApiKey = requestHeader.FirstOrDefault().ToString();


                if (db.USUARIOS.Where(w => w.APIKEY == HttpApiKey).Any())
                {
                    validapi = true;
                }
                else
                {
                    return request.CreateResponse(
                        HttpStatusCode.Forbidden,
                        new
                        {
                            error = "ApiKey Distinta",
                            HttpStatusCode.Forbidden
                        });
                }

            }

            if (!checkApi)
            {
                return request.CreateResponse(
                    HttpStatusCode.Forbidden,
                    new
                    {
                        error = "ApiKey No Detectada",
                        HttpStatusCode.Forbidden
                    });
            }
            var response = await base.SendAsync(request, cancellationToken);

            return response;
        }

        //variable quedamada para simular el apikey
        //private const string ApiKey = "bostel2901*-/";
        //protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        //{
        //    bool validapi = false;
        //    IEnumerable<string> requestHeader;
        //    var checkApi = request.Headers.TryGetValues("Key", out requestHeader);
        //    if (checkApi)
        //    {
        //        if (requestHeader.FirstOrDefault().Equals(ApiKey))
        //        {
        //            validapi = true;
        //        }
        //        else
        //        {
        //            return request.CreateResponse(
        //                HttpStatusCode.Forbidden,
        //                new
        //                {
        //                    error = "ApiKey Distinta",
        //                    HttpStatusCode.Forbidden
        //                });
        //        }

        //    }

        //    if (!checkApi)
        //    {
        //        return request.CreateResponse(
        //            HttpStatusCode.Forbidden,
        //            new
        //            {
        //                error = "ApiKey No Detectada",
        //                HttpStatusCode.Forbidden
        //            });
        //    }
        //    var response = await base.SendAsync(request, cancellationToken);

        //    return response;
        //}
    }

}