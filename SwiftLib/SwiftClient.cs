using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

using RestSharp;

namespace SwiftLib
{
    public class SwiftClient
    {
        private SwiftConfig cfg;
        private RestClient rc;
        private String storageUrl = null;
        private String authToken = null;

        public SwiftClient(SwiftConfig cfg)
        {
            this.cfg = cfg;
            rc = new RestClient(cfg.Url);
            Authenticate();
        }

        public void Authenticate()
        {
            RestClient rc = GetRestClient();
            RestRequest request = new RestRequest("auth/v1.0/", Method.GET);
            request.AddHeader("X-Auth-User", cfg.User);
            request.AddHeader("X-Auth-Key", cfg.Authkey);
            IRestResponse response = rc.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                foreach (Parameter hdr in response.Headers)
                {
                    if (hdr.Name.Equals("X-Storage-Url"))
                        storageUrl = hdr.Value.ToString().Substring(cfg.Url.Length -1);
                    else if (hdr.Name.Equals("X-Auth-Token"))
                        authToken = hdr.Value.ToString();
                }
            }
            else
            {
                throw new Exception("Authentication Failed. Error: " + response.ToString());
            }

            Debug.Print("Storage URL:" + storageUrl + "; " + "Auth Token: " + authToken);
        }

        public Boolean IsAuthenticated()
        {
            if (storageUrl != null)
            {
                RestClient rc = GetRestClient();
                IRestRequest request = GetRequest();
                IRestResponse response = rc.Execute(request);
                return response.StatusCode == System.Net.HttpStatusCode.OK ||
                    response.StatusCode == System.Net.HttpStatusCode.NoContent;
            }

            return false;
        }

        // check if a container exists
        public Boolean ContainerExists(String containerName)
        {
            RestClient rc = GetRestClient();
            containerName = RemoveLeadingSlash(containerName);
            IRestRequest request = GetRequest(storageUrl + "/" + containerName, Method.HEAD);
            IRestResponse response = rc.Execute(request);
            return response.StatusCode == System.Net.HttpStatusCode.OK || 
                response.StatusCode == System.Net.HttpStatusCode.NoContent;
        }

        // create container only if not exists
        public void CreateContainer(String containerName)
        {
            CreateContainer(containerName, false);
        }

        // create a container with overwirte option
        public void CreateContainer(String containerName, Boolean overwrite)
        {
            RestClient rc = GetRestClient();
            if (ContainerExists(containerName))
            {
                if (!overwrite)
                    return;
            }

            containerName = RemoveLeadingSlash(containerName);
            IRestRequest request = GetRequest(storageUrl + "/" + containerName, Method.PUT);
            IRestResponse response = rc.Execute(request);
            if (response.StatusCode != System.Net.HttpStatusCode.Created)
                throw new Exception("Failed to create container. Error: " + response.ToString());
        }

        // create object
        public void CreateObject(String containerName, String objectName, String path)
        {
            RestClient rc = GetRestClient();
            containerName = RemoveLeadingSlash(containerName);
            objectName = RemoveLeadingSlash(objectName);

            String fullObjectName = System.IO.Path.Combine(path, objectName);
            fullObjectName = RemoveLeadingChar(fullObjectName, "\\");

            IRestRequest request = GetRequest(storageUrl + "/" + containerName + "/" + fullObjectName.Replace("\\","/"), Method.PUT);
            request.AddFile(objectName, Path.Combine(cfg.BoxFolder, fullObjectName));
            IRestResponse response = rc.Execute(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK &&
                response.StatusCode != System.Net.HttpStatusCode.Created)
            {
                throw new Exception("Error in creating object. Error:" + response.ToString());
            }
        }

        // list all objects in a container
        public List<SwiftFileInfo> GetObjectList(String containerName)
        {
            RestClient rc = GetRestClient();
            containerName = RemoveLeadingSlash(containerName);
            IRestRequest request = GetRequest(storageUrl + "/" + containerName, Method.GET, "format=json");
            IRestResponse response = rc.Execute(request);
            return FileUtil.GetSwiftFileInfoList(response.Content);
        }

        // get file
        public void GetObject(string objectName)
        {
            RestClient rc = GetRestClient();
            IRestRequest request = GetRequest(storageUrl + "/" + objectName, Method.GET);
            IRestResponse response = rc.Execute(request);
            String fileName = Path.Combine(cfg.DownloadFolder, objectName.Replace("/", "\\"));
            FileInfo fi = new FileInfo(fileName);
            if (!Directory.Exists(fi.DirectoryName))
                Directory.CreateDirectory(fi.DirectoryName);
            File.WriteAllBytes(fileName, response.RawBytes);
        }

        // create a rest client for the given configuration
        private RestClient GetRestClient()
        {
            return new RestClient(cfg.Url);
        }

        // create a rest request for storage URL
        // and auth token in the header
        private IRestRequest GetRequest()
        {
            return GetRequest(storageUrl);
        }

        // create a rest request for resource URL
        // and auth token in the header
        private IRestRequest GetRequest(string resourceUrl)
        {
            return GetRequest(resourceUrl, Method.GET);
        }

        // create a rest request for the resource URL and the method
        // with auth token in the header
        private IRestRequest GetRequest(string resourceUrl, Method method)
        {
            return GetRequest(resourceUrl, method, null);
        }

        // create a request request for the resource URL, method and query strng
        // with auth token in header
        private IRestRequest GetRequest(string resourceUrl, Method method, string queryString)
        {
            RestRequest request = new RestRequest(resourceUrl + 
                (String.IsNullOrEmpty(queryString) ? "" : "?" + queryString), method);
            request.AddHeader("X-Auth-Token", authToken);
            return request;
        }

        private String RemoveLeadingSlash(String token)
        {
            return RemoveLeadingChar(token, "/");
        }

        private String RemoveLeadingChar(String token, string chr)
        {
            while (token != null && token.Length > 0 && token.StartsWith(chr))
                token = token.Substring(1);

            return token;
        }
    }
}
