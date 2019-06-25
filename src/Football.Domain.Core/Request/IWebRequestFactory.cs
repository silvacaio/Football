using System;
using System.IO;
using System.Net;

namespace Football.Domain.Core.Request
{
    public interface IHttpWebRequest
    {
        string Method { get; set; }
        WebHeaderCollection Headers { get; set; }

        IHttpWebResponse GetResponse();
    }

    public interface IHttpWebResponse : IDisposable
    {
        Stream GetResponseStream();
    }

    public interface IHttpWebRequestFactory
    {
        IHttpWebRequest Create(string uri);
    }

    public class HttpWebRequestFactory : IHttpWebRequestFactory
    {
        public IHttpWebRequest Create(string uri)
        {
            return new WrapHttpWebRequest((HttpWebRequest)WebRequest.Create(uri));
        }
    }

    public class WrapHttpWebRequest : IHttpWebRequest
    {
        private readonly HttpWebRequest _request;

        public WrapHttpWebRequest(HttpWebRequest request)
        {
            _request = request;
        }

        public string Method
        {
            get { return _request.Method; }
            set { _request.Method = value; }
        }

        public WebHeaderCollection Headers
        {
            get { return _request.Headers; }
            set { _request.Headers = value; }
        }

        public IHttpWebResponse GetResponse()
        {
            return new WrapHttpWebResponse((HttpWebResponse)_request.GetResponse());
        }
    }

    public class WrapHttpWebResponse : IHttpWebResponse
    {
        private WebResponse _response;

        public WrapHttpWebResponse(HttpWebResponse response)
        {
            _response = response;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_response != null)
                {
                    ((IDisposable)_response).Dispose();
                    _response = null;
                }
            }
        }

        public Stream GetResponseStream()
        {
            return _response.GetResponseStream();
        }
    }

}
