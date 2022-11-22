// using System;
// using System.Collections.Concurrent;
// using System.Collections.Generic;
// using System.Collections.Specialized;
// using System.Configuration;
// using System.Data.Common;
// using System.Globalization;
// using System.IO;
// using System.Linq;
// using System.Net;
// using System.Net.Http;
// using System.Net.Http.Headers;
// using System.Security.Cryptography;
// using System.Text;
// using System.Threading;
// using System.Threading.Tasks;

// namespace Events.Services
// {
//     public class AzureBlobFactory
//     {
//         /*private readonly ILogger<AzureBlobFactory> _logger;

//         public AzureBlobFactory(ILogger<AzureBlobFactory> logger)
//         {
//             _logger = logger;
//         }*/

//         public BlobStorage GetBlobStorageFor(string containerName, string connectionString)
//         {
//             if (!string.IsNullOrWhiteSpace(connectionString))
//             {
//                 return new BlobStorage( containerName, connectionString);
//             }
//             return null;
//         }
//     }


//     /// <summary>
//     /// BlobStorage is container dependent.... So on initilize give it a container, and that instance of BlobStorage will only work on that container
//     /// </summary>
//     public class BlobStorage
//     {
//         private readonly string _storageAccountName;
//         private readonly string _storageAccountKey;
//         private readonly string _containerName;
//         private readonly string _baseUrl;

//         private readonly HttpClient _client;

//         private PrivateBlobHashSet _filesInContainer = new PrivateBlobHashSet();

//         public BlobStorage(string containerName, string connectionString, bool useLocalEmulator = false)
//         {
//             //_logger = logger;
//             _client = new HttpClient();
//             _containerName = containerName.ToLower();

//             DbConnectionStringBuilder connectionStringBuilder = new DbConnectionStringBuilder();
//             connectionStringBuilder.ConnectionString = connectionString;

//             _storageAccountName = connectionStringBuilder["AccountName"].ToString();
//             _storageAccountKey = connectionStringBuilder["AccountKey"].ToString();
//             _baseUrl = string.Format("https://{0}.blob.core.windows.net", _storageAccountName);
//         }

//         /// <summary>
//         /// Use the ref keyword if you want fileName to be case-corrected
//         /// </summary>
//         /// <param name="fileName"></param>
//         /// <returns></returns>
//         public bool BlobFileExists(string fileName)
//         {
//             string x = fileName;
//             return BlobFileExists(ref x);
//         }

//         public bool BlobFileExists(ref string fileName)
//         {
//             if (_filesInContainer.CheckForBlob(ref fileName))
//             {
//                 return true;
//             }

//             //Fire this incase file isn't in the list.
//             var task = BlobDirectCheckAsync(fileName);
//             if (task.GetAwaiter().GetResult() != null)
//             {
//                 fileName = task.Result;
//                 return true;
//             }

//             return false;
//         }

//         public string DownloadFileFromBlobStorage(string fileName)
//         {
//             bool exists = BlobFileExists(ref fileName);
//             if (exists)
//             {
//                 string uri = string.Join("/", _baseUrl, _containerName, fileName);
//                 return BlobAPIRequest<String>(uri: uri, method: HttpMethod.Get,
//                     resultFunc: x =>
//                     {
//                         if (x.StatusCode == HttpStatusCode.OK)
//                         {
//                             return x.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
//                         }
//                         return default;
//                     },
//                     timeoutPeriodForRequestSeconds: 60);
//             }
//             return default;
//         }

//         public string UploadHttpPostedFile(string fileName, Stream file, string contentType, bool deleteOld = false)
//         {
//             return UploadFileToBlobStorage(fileName, file, deleteOld, contentType);
//         }

//         public string UploadFile(string fileName, Stream file, bool deleteOld = false)
//         {
//             return UploadFileToBlobStorage(fileName, file, deleteOld);
//         }

//         public string AppendBlob(string fileName, Stream content)
//         {
//             if (content == null || content.Length == 0) { return fileName; }

//             bool exists = BlobFileExists(ref fileName);
//             if (!exists)
//             {
//                 CreateAppendBlob(fileName);
//             }


//             AppendBlockToBlob(fileName, content);


//             return fileName;
//         }

//         private string AppendBlockToBlob(string fileName, Stream content)
//         {
//             string uri = string.Join("/", _baseUrl, _containerName, fileName) + "?comp=appendblock";

//             Dictionary<string, string> addHeaders = new Dictionary<string, string>()
//                 {
//                     {"x-ms-blob-type","AppendBlob" }
//                 };

//             return BlobAPIRequest<string>(uri: uri, method: HttpMethod.Put,
//                 resultFunc: x =>
//                 {
//                     if (x.StatusCode == HttpStatusCode.Created)
//                     {
//                         return x.RequestMessage.RequestUri.ToString();
//                     }
//                     return default;
//                 },
//                 requestStream: content,
//                 additionalHeaders: addHeaders,
//                 timeoutPeriodForRequestSeconds: 60);
//         }


//         private string CreateAppendBlob(string fileName)
//         {
//             string uri = string.Join("/", _baseUrl, _containerName, fileName);

//             Dictionary<string, string> addHeaders = new Dictionary<string, string>()
//                 {
//                     {"x-ms-blob-type","AppendBlob" }
//                 };

//             return BlobAPIRequest<string>(uri: uri, method: HttpMethod.Put,
//                 resultFunc: x =>
//                 {
//                     if (x.StatusCode == HttpStatusCode.Created)
//                     {
//                         _filesInContainer.Add(fileName);
//                         return x.RequestMessage.RequestUri.ToString();
//                     }
//                     return default;
//                 },
//                 additionalHeaders: addHeaders,
//                 timeoutPeriodForRequestSeconds: 60);

//         }

//         private string UploadFileToBlobStorage(string fileName, Stream file, bool deleteOld, string optionalContentType = null)
//         {
//             bool exists = BlobFileExists(ref fileName);
//             if (!exists || deleteOld)
//             {
//                 string uri = string.Join("/", _baseUrl, _containerName, fileName);
//                 file.Position = 0;

//                 Dictionary<string, string> addHeaders = new Dictionary<string, string>()
//                 {
//                     {"x-ms-blob-type","BlockBlob" }
//                 };

//                 if (!string.IsNullOrWhiteSpace(optionalContentType))
//                 {
//                     addHeaders.Add("x-ms-blob-content-type", optionalContentType);
//                 }

//                 return BlobAPIRequest<string>(uri: uri, method: HttpMethod.Put,
//                     resultFunc: x =>
//                     {
//                         if (x.StatusCode == HttpStatusCode.Created)
//                         {
//                             _filesInContainer.Add(fileName);
//                             return x.RequestMessage.RequestUri.ToString();
//                         }
//                         return default;
//                     },
//                     requestStream: file,
//                     additionalHeaders: addHeaders,
//                     timeoutPeriodForRequestSeconds: 60);
//             }
//             return default;
//         }

//         #region Helper Class
//         //This class protects the underlying data structure and keeps the Dictionary as key's of format .ToLower()        

//         private class PrivateBlobHashSet
//         {
//             private ConcurrentDictionary<string, string> _filesInContainer = new ConcurrentDictionary<string, string>();

//             public bool CheckForBlob(ref string fileName)
//             {
//                 if (_filesInContainer.TryGetValue(fileName.ToLower(), out string x))
//                 {
//                     fileName = x;
//                     return true;
//                 }
//                 return false;
//             }

//             /// <summary>
//             /// Will add the fileName to the list if it doesn't exist.
//             /// </summary>
//             /// <param name="fileName"></param>
//             public void Add(string fileName)
//             {
//                 _filesInContainer.TryAdd(fileName.ToLower(), fileName);
//             }

//             public void Clear()
//             {
//                 _filesInContainer.Clear();
//             }
//         }

//         #endregion

//         #region Async Functions
//         private Task<string> BlobDirectCheckAsync(string fileName)
//         {
//             return BlobAPIRequestAsync<string>(
//                 uri: string.Join("/", _baseUrl, _containerName, fileName),
//                 method: HttpMethod.Head,
//                 resultFunc: x =>
//                 {
//                     if (x.StatusCode == HttpStatusCode.OK)
//                     {
//                         _filesInContainer.Add(fileName);
//                         return fileName;
//                     }
//                     return default;
//                 });
//         }



//         /// <summary>
//         /// A synchronous call to the Blob API, essentially this will wait til a response is had.
//         /// </summary>
//         /// <typeparam name="T"></typeparam>
//         /// <param name="uri">The endpoint to hit</param>
//         /// <param name="method">Method type, Get,Post,Put etc...</param>
//         /// <param name="resultFunc">A Function to call against the resulting HttpMessageResponse</param>
//         /// <param name="requestStream">This will be used over requestPayload</param>
//         /// <param name="requestPayload">If you want to use byte[] instead of a stream</param>
//         /// <param name="additionalHeaders">Additional http headers needed for a specific call other than the default</param>
//         /// <returns></returns>
//         private T BlobAPIRequest<T>(string uri,
//             HttpMethod method,
//             Func<HttpResponseMessage, T> resultFunc,
//             Stream requestStream = null,
//             byte[] requestPayload = null,
//             int timeoutPeriodForRequestSeconds = 20,
//             Dictionary<string, string> additionalHeaders = null)
//         {
//             return BlobAPIRequestAsync<T>(uri: uri,
//                 method: method, resultFunc: resultFunc, timeoutPeriodForRequestSeconds: timeoutPeriodForRequestSeconds,
//                 requestStream: requestStream, requestPayload: requestPayload, additionalHeaders: additionalHeaders).GetAwaiter().GetResult();
//         }

//         /// <summary>
//         /// The async implementation of a Blob API request
//         /// </summary>
//         /// <typeparam name="T"></typeparam>
//         /// <param name="uri">The endpoint to hit</param>
//         /// <param name="method">Method type, Get,Post,Put etc...</param>
//         /// <param name="resultFunc">A Function to call against the resulting HttpMessageResponse</param>
//         /// <param name="requestStream">This will be used over requestPayload</param>
//         /// <param name="requestPayload">If you want to use byte[] instead of a stream</param>
//         /// /// <param name="cancelToken">If you call this async you can optionally submit a cancel token</param>
//         /// <param name="additionalHeaders">Additional http headers needed for a specific call other than the default</param>
//         /// <returns></returns>
//         private async Task<T> BlobAPIRequestAsync<T>(string uri,
//             HttpMethod method,
//             Func<HttpResponseMessage, T> resultFunc,
//             Stream requestStream = null,
//             byte[] requestPayload = null,
//             int timeoutPeriodForRequestSeconds = 20,
//             Dictionary<string, string> additionalHeaders = null)
//         {
//             using (var httpRequestMessage = new HttpRequestMessage(method, uri))
//             {
//                 if (requestStream != null)
//                 {
//                     httpRequestMessage.Content = new StreamContent(requestStream);
//                 }
//                 else
//                 {
//                     httpRequestMessage.Content = (requestPayload == null) ? null : new ByteArrayContent(requestPayload);
//                 }
//                 // Add the request headers for x-ms-date and x-ms-version.
//                 DateTime now = DateTime.UtcNow;
//                 httpRequestMessage.Headers.Add("x-ms-date", now.ToString("R", CultureInfo.InvariantCulture));
//                 httpRequestMessage.Headers.Add("x-ms-version", "2017-04-17");
//                 if (additionalHeaders != null && additionalHeaders.Any())
//                 {
//                     foreach (var i in additionalHeaders)
//                     {
//                         httpRequestMessage.Headers.Add(i.Key, i.Value);
//                     }
//                 }

//                 // If you need any additional headers, add them here before creating
//                 //   the authorization header.

//                 // Get the authorization header and add it.
//                 httpRequestMessage.Headers.Authorization = AzureStorageAuthenticationHelper.GetAuthorizationHeader(
//                     _storageAccountName, _storageAccountKey, now, httpRequestMessage);

//                 var cancelTokenSource = new CancellationTokenSource(timeoutPeriodForRequestSeconds * 1000);
//                 var cancelToken = cancelTokenSource.Token;//use this to kill if it takes a while because it is probably hung up
//                 try
//                 {
//                     using (HttpResponseMessage httpResponseMessage = await _client.SendAsync(httpRequestMessage, cancelToken).ConfigureAwait(false))
//                     {
//                         return resultFunc(httpResponseMessage);
//                     }
//                 }
//                 catch (OperationCanceledException ex)
//                 {
//                     //_logger.LogError(ex, "Blob Storage: BlobAPIRequestAsync Timed out\n" + httpRequestMessage.Headers.ToString());
//                 }
//                 catch (Exception e)
//                 {
//                     //_logger.LogError(e, "Blob Storage: " + e.Message);
//                 }
//                 finally
//                 {
//                     cancelTokenSource.Dispose();
//                 }

//                 return default;
//             }
//         }

//         #endregion
//     }

//     /// <summary>
//     /// You can take this class and drop it into another project and use this code
//     /// to create the headers you need to make a REST API call to Azure Storage.
//     /// </summary>
//     internal static class AzureStorageAuthenticationHelper
//     {
//         /// <summary>
//         /// This creates the authorization header. This is required, and must be built 
//         ///   exactly following the instructions. This will return the authorization header
//         ///   for most storage service calls.
//         /// Create a string of the message signature and then encrypt it.
//         /// </summary>
//         /// <param name="storageAccountName">The name of the storage account to use.</param>
//         /// <param name="storageAccountKey">The access key for the storage account to be used.</param>
//         /// <param name="now">Date/Time stamp for now.</param>
//         /// <param name="httpRequestMessage">The HttpWebRequest that needs an auth header.</param>
//         /// <param name="ifMatch">Provide an eTag, and it will only make changes
//         /// to a blob if the current eTag matches, to ensure you don't overwrite someone else's changes.</param>
//         /// <param name="md5">Provide the md5 and it will check and make sure it matches the blob's md5.
//         /// If it doesn't match, it won't return a value.</param>
//         /// <returns></returns>
//         internal static AuthenticationHeaderValue GetAuthorizationHeader(
//            string storageAccountName, string storageAccountKey, DateTime now,
//            HttpRequestMessage httpRequestMessage, string ifMatch = "", string md5 = "")
//         {
//             // This is the raw representation of the message signature.
//             HttpMethod method = httpRequestMessage.Method;
//             String MessageSignature = String.Format("{0}\n\n\n{1}\n{5}\n\n\n\n{2}\n\n\n\n{3}{4}",
//                       method.ToString(),
//                       (method == HttpMethod.Get || method == HttpMethod.Head || httpRequestMessage.Content == null) ? String.Empty
//                         : httpRequestMessage.Content.Headers.ContentLength.ToString(),
//                       ifMatch,
//                       GetCanonicalizedHeaders(httpRequestMessage),
//                       GetCanonicalizedResource(httpRequestMessage.RequestUri, storageAccountName),
//                       md5);

//             // Now turn it into a byte array.
//             byte[] SignatureBytes = Encoding.UTF8.GetBytes(MessageSignature);

//             // Create the HMACSHA256 version of the storage key.
//             HMACSHA256 SHA256 = new HMACSHA256(Convert.FromBase64String(storageAccountKey));

//             // Compute the hash of the SignatureBytes and convert it to a base64 string.
//             string signature = Convert.ToBase64String(SHA256.ComputeHash(SignatureBytes));

//             // This is the actual header that will be added to the list of request headers.
//             // You can stop the code here and look at the value of 'authHV' before it is returned.
//             AuthenticationHeaderValue authHV = new AuthenticationHeaderValue("SharedKey",
//                 storageAccountName + ":" + Convert.ToBase64String(SHA256.ComputeHash(SignatureBytes)));
//             return authHV;
//         }

//         /// <summary>
//         /// Put the headers that start with x-ms in a list and sort them.
//         /// Then format them into a string of [key:value\n] values concatenated into one string.
//         /// (Canonicalized Headers = headers where the format is standardized).
//         /// </summary>
//         /// <param name="httpRequestMessage">The request that will be made to the storage service.</param>
//         /// <returns>Error message; blank if okay.</returns>
//         private static string GetCanonicalizedHeaders(HttpRequestMessage httpRequestMessage)
//         {
//             var headers = from kvp in httpRequestMessage.Headers
//                           where kvp.Key.StartsWith("x-ms-", StringComparison.OrdinalIgnoreCase)
//                           orderby kvp.Key
//                           select new { Key = kvp.Key.ToLowerInvariant(), kvp.Value };

//             StringBuilder sb = new StringBuilder();

//             // Create the string in the right format; this is what makes the headers "canonicalized" --
//             //   it means put in a standard format. http://en.wikipedia.org/wiki/Canonicalization
//             foreach (var kvp in headers)
//             {
//                 StringBuilder headerBuilder = new StringBuilder(kvp.Key);
//                 char separator = ':';

//                 // Get the value for each header, strip out \r\n if found, then append it with the key.
//                 foreach (string headerValues in kvp.Value)
//                 {
//                     string trimmedValue = headerValues.TrimStart().Replace("\r\n", String.Empty);
//                     headerBuilder.Append(separator).Append(trimmedValue);

//                     // Set this to a comma; this will only be used 
//                     //   if there are multiple values for one of the headers.
//                     separator = ',';
//                 }
//                 sb.Append(headerBuilder.ToString()).Append("\n");
//             }
//             return sb.ToString();
//         }

//         /// <summary>
//         /// This part of the signature string represents the storage account 
//         ///   targeted by the request. Will also include any additional query parameters/values.
//         /// For ListContainers, this will return something like this:
//         ///   /storageaccountname/\ncomp:list
//         /// </summary>
//         /// <param name="address">The URI of the storage service.</param>
//         /// <param name="accountName">The storage account name.</param>
//         /// <returns>String representing the canonicalized resource.</returns>
//         private static string GetCanonicalizedResource(Uri address, string storageAccountName)
//         {
//             // The absolute path is "/" because for we're getting a list of containers.
//             StringBuilder sb = new StringBuilder("/").Append(storageAccountName).Append(address.AbsolutePath);

//             // Address.Query is the resource, such as "?comp=list".
//             // This ends up with a NameValueCollection with 1 entry having key=comp, value=list.
//             // It will have more entries if you have more query parameters.
//             NameValueCollection values = HttpUtilityHelper.ParseQueryString(address.Query.ToLower());

//             foreach (var item in values.AllKeys.OrderBy(k => k))
//             {
//                 sb.Append('\n').Append(item).Append(':').Append(values[item]);
//             }


//             return sb.ToString();

//         }
//     }


//     internal sealed class HttpUtilityHelper // https://github.com/mono/mono/tree/master/mcs/class/System.Web
//     {

//         public static void HtmlAttributeEncode(string s, TextWriter output)
//         {
//             if (output == null)
//             {
//                 throw new ArgumentNullException("output");
//             }
//             HttpEncoder.Current.HtmlAttributeEncode(s, output);
//         }

//         public static string HtmlAttributeEncode(string s)
//         {
//             if (s == null)
//                 return null;

//             using (var sw = new StringWriter())
//             {
//                 HttpEncoder.Current.HtmlAttributeEncode(s, sw);
//                 return sw.ToString();
//             }
//         }

//         public static string UrlDecode(string str)
//         {
//             return UrlDecode(str, Encoding.UTF8);
//         }

//         static char[] GetChars(MemoryStream b, Encoding e)
//         {
//             return e.GetChars(b.GetBuffer(), 0, (int)b.Length);
//         }

//         static void WriteCharBytes(IList<byte> buf, char ch, Encoding e)
//         {
//             if (ch > 255)
//             {
//                 foreach (byte b in e.GetBytes(new char[] { ch }))
//                     buf.Add(b);
//             }
//             else
//                 buf.Add((byte)ch);
//         }

//         public static string UrlDecode(string s, Encoding e)
//         {
//             if (null == s)
//                 return null;

//             if (s.IndexOf('%') == -1 && s.IndexOf('+') == -1)
//                 return s;

//             if (e == null)
//                 e = Encoding.UTF8;

//             long len = s.Length;
//             var bytes = new List<byte>();
//             int xchar;
//             char ch;

//             for (int i = 0; i < len; i++)
//             {
//                 ch = s[i];
//                 if (ch == '%' && i + 2 < len && s[i + 1] != '%')
//                 {
//                     if (s[i + 1] == 'u' && i + 5 < len)
//                     {
//                         // unicode hex sequence
//                         xchar = GetChar(s, i + 2, 4);
//                         if (xchar != -1)
//                         {
//                             WriteCharBytes(bytes, (char)xchar, e);
//                             i += 5;
//                         }
//                         else
//                             WriteCharBytes(bytes, '%', e);
//                     }
//                     else if ((xchar = GetChar(s, i + 1, 2)) != -1)
//                     {
//                         WriteCharBytes(bytes, (char)xchar, e);
//                         i += 2;
//                     }
//                     else
//                     {
//                         WriteCharBytes(bytes, '%', e);
//                     }
//                     continue;
//                 }

//                 if (ch == '+')
//                     WriteCharBytes(bytes, ' ', e);
//                 else
//                     WriteCharBytes(bytes, ch, e);
//             }

//             byte[] buf = bytes.ToArray();
//             bytes = null;
//             return e.GetString(buf);

//         }

//         public static string UrlDecode(byte[] bytes, Encoding e)
//         {
//             if (bytes == null)
//                 return null;

//             return UrlDecode(bytes, 0, bytes.Length, e);
//         }

//         static int GetInt(byte b)
//         {
//             char c = (char)b;
//             if (c >= '0' && c <= '9')
//                 return c - '0';

//             if (c >= 'a' && c <= 'f')
//                 return c - 'a' + 10;

//             if (c >= 'A' && c <= 'F')
//                 return c - 'A' + 10;

//             return -1;
//         }

//         static int GetChar(byte[] bytes, int offset, int length)
//         {
//             int value = 0;
//             int end = length + offset;
//             for (int i = offset; i < end; i++)
//             {
//                 int current = GetInt(bytes[i]);
//                 if (current == -1)
//                     return -1;
//                 value = (value << 4) + current;
//             }

//             return value;
//         }

//         static int GetChar(string str, int offset, int length)
//         {
//             int val = 0;
//             int end = length + offset;
//             for (int i = offset; i < end; i++)
//             {
//                 char c = str[i];
//                 if (c > 127)
//                     return -1;

//                 int current = GetInt((byte)c);
//                 if (current == -1)
//                     return -1;
//                 val = (val << 4) + current;
//             }

//             return val;
//         }

//         public static string UrlDecode(byte[] bytes, int offset, int count, Encoding e)
//         {
//             if (bytes == null)
//                 return null;
//             if (count == 0)
//                 return String.Empty;

//             if (bytes == null)
//                 throw new ArgumentNullException("bytes");

//             if (offset < 0 || offset > bytes.Length)
//                 throw new ArgumentOutOfRangeException("offset");

//             if (count < 0 || offset + count > bytes.Length)
//                 throw new ArgumentOutOfRangeException("count");

//             StringBuilder output = new StringBuilder();
//             MemoryStream acc = new MemoryStream();

//             int end = count + offset;
//             int xchar;
//             for (int i = offset; i < end; i++)
//             {
//                 if (bytes[i] == '%' && i + 2 < count && bytes[i + 1] != '%')
//                 {
//                     if (bytes[i + 1] == (byte)'u' && i + 5 < end)
//                     {
//                         if (acc.Length > 0)
//                         {
//                             output.Append(GetChars(acc, e));
//                             acc.SetLength(0);
//                         }
//                         xchar = GetChar(bytes, i + 2, 4);
//                         if (xchar != -1)
//                         {
//                             output.Append((char)xchar);
//                             i += 5;
//                             continue;
//                         }
//                     }
//                     else if ((xchar = GetChar(bytes, i + 1, 2)) != -1)
//                     {
//                         acc.WriteByte((byte)xchar);
//                         i += 2;
//                         continue;
//                     }
//                 }

//                 if (acc.Length > 0)
//                 {
//                     output.Append(GetChars(acc, e));
//                     acc.SetLength(0);
//                 }

//                 if (bytes[i] == '+')
//                 {
//                     output.Append(' ');
//                 }
//                 else
//                 {
//                     output.Append((char)bytes[i]);
//                 }
//             }

//             if (acc.Length > 0)
//             {
//                 output.Append(GetChars(acc, e));
//             }

//             acc = null;
//             return output.ToString();
//         }

//         public static byte[] UrlDecodeToBytes(byte[] bytes)
//         {
//             if (bytes == null)
//                 return null;

//             return UrlDecodeToBytes(bytes, 0, bytes.Length);
//         }

//         public static byte[] UrlDecodeToBytes(string str)
//         {
//             return UrlDecodeToBytes(str, Encoding.UTF8);
//         }

//         public static byte[] UrlDecodeToBytes(string str, Encoding e)
//         {
//             if (str == null)
//                 return null;

//             if (e == null)
//                 throw new ArgumentNullException("e");

//             return UrlDecodeToBytes(e.GetBytes(str));
//         }

//         public static byte[] UrlDecodeToBytes(byte[] bytes, int offset, int count)
//         {
//             if (bytes == null)
//                 return null;
//             if (count == 0)
//                 return new byte[0];

//             int len = bytes.Length;
//             if (offset < 0 || offset >= len)
//                 throw new ArgumentOutOfRangeException("offset");

//             if (count < 0 || offset > len - count)
//                 throw new ArgumentOutOfRangeException("count");

//             MemoryStream result = new MemoryStream();
//             int end = offset + count;
//             for (int i = offset; i < end; i++)
//             {
//                 char c = (char)bytes[i];
//                 if (c == '+')
//                 {
//                     c = ' ';
//                 }
//                 else if (c == '%' && i < end - 2)
//                 {
//                     int xchar = GetChar(bytes, i + 1, 2);
//                     if (xchar != -1)
//                     {
//                         c = (char)xchar;
//                         i += 2;
//                     }
//                 }
//                 result.WriteByte((byte)c);
//             }

//             return result.ToArray();
//         }

//         public static string UrlEncode(string str)
//         {
//             return UrlEncode(str, Encoding.UTF8);
//         }

//         public static string UrlEncode(string s, Encoding Enc)
//         {
//             if (s == null)
//                 return null;

//             if (s == String.Empty)
//                 return String.Empty;

//             bool needEncode = false;
//             int len = s.Length;
//             for (int i = 0; i < len; i++)
//             {
//                 char c = s[i];
//                 if ((c < '0') || (c < 'A' && c > '9') || (c > 'Z' && c < 'a') || (c > 'z'))
//                 {
//                     if (HttpEncoder.NotEncoded(c))
//                         continue;

//                     needEncode = true;
//                     break;
//                 }
//             }

//             if (!needEncode)
//                 return s;

//             // avoided GetByteCount call
//             byte[] bytes = new byte[Enc.GetMaxByteCount(s.Length)];
//             int realLen = Enc.GetBytes(s, 0, s.Length, bytes, 0);
//             return Encoding.ASCII.GetString(UrlEncodeToBytes(bytes, 0, realLen));
//         }

//         public static string UrlEncode(byte[] bytes)
//         {
//             if (bytes == null)
//                 return null;

//             if (bytes.Length == 0)
//                 return String.Empty;

//             return Encoding.ASCII.GetString(UrlEncodeToBytes(bytes, 0, bytes.Length));
//         }

//         public static string UrlEncode(byte[] bytes, int offset, int count)
//         {
//             if (bytes == null)
//                 return null;

//             if (bytes.Length == 0)
//                 return String.Empty;

//             return Encoding.ASCII.GetString(UrlEncodeToBytes(bytes, offset, count));
//         }

//         public static byte[] UrlEncodeToBytes(string str)
//         {
//             return UrlEncodeToBytes(str, Encoding.UTF8);
//         }

//         public static byte[] UrlEncodeToBytes(string str, Encoding e)
//         {
//             if (str == null)
//                 return null;

//             if (str.Length == 0)
//                 return new byte[0];

//             byte[] bytes = e.GetBytes(str);
//             return UrlEncodeToBytes(bytes, 0, bytes.Length);
//         }

//         public static byte[] UrlEncodeToBytes(byte[] bytes)
//         {
//             if (bytes == null)
//                 return null;

//             if (bytes.Length == 0)
//                 return new byte[0];

//             return UrlEncodeToBytes(bytes, 0, bytes.Length);
//         }

//         public static byte[] UrlEncodeToBytes(byte[] bytes, int offset, int count)
//         {
//             if (bytes == null)
//                 return null;
//             return HttpEncoder.Current.UrlEncode(bytes, offset, count);
//         }

//         public static string UrlEncodeUnicode(string str)
//         {
//             if (str == null)
//                 return null;

//             return Encoding.ASCII.GetString(UrlEncodeUnicodeToBytes(str));
//         }

//         public static byte[] UrlEncodeUnicodeToBytes(string str)
//         {
//             if (str == null)
//                 return null;

//             if (str.Length == 0)
//                 return new byte[0];

//             MemoryStream result = new MemoryStream(str.Length);
//             foreach (char c in str)
//             {
//                 HttpEncoder.UrlEncodeChar(c, result, true);
//             }
//             return result.ToArray();
//         }

//         /// <summary>
//         /// Decodes an HTML-encoded string and returns the decoded string.
//         /// </summary>
//         /// <param name="s">The HTML string to decode. </param>
//         /// <returns>The decoded text.</returns>
//         public static string HtmlDecode(string s)
//         {
//             if (s == null)
//                 return null;

//             using (var sw = new StringWriter())
//             {
//                 HttpEncoder.Current.HtmlDecode(s, sw);
//                 return sw.ToString();
//             }
//         }

//         /// <summary>
//         /// Decodes an HTML-encoded string and sends the resulting output to a TextWriter output stream.
//         /// </summary>
//         /// <param name="s">The HTML string to decode</param>
//         /// <param name="output">The TextWriter output stream containing the decoded string. </param>
//         public static void HtmlDecode(string s, TextWriter output)
//         {
//             if (output == null)
//             {
//                 throw new ArgumentNullException("output");
//             }

//             if (!String.IsNullOrEmpty(s))
//             {
//                 HttpEncoder.Current.HtmlDecode(s, output);
//             }
//         }

//         public static string HtmlEncode(string s)
//         {
//             if (s == null)
//                 return null;

//             using (var sw = new StringWriter())
//             {
//                 HttpEncoder.Current.HtmlEncode(s, sw);
//                 return sw.ToString();
//             }
//         }

//         /// <summary>
//         /// HTML-encodes a string and sends the resulting output to a TextWriter output stream.
//         /// </summary>
//         /// <param name="s">The string to encode. </param>
//         /// <param name="output">The TextWriter output stream containing the encoded string. </param>
//         public static void HtmlEncode(string s, TextWriter output)
//         {
//             if (output == null)
//             {
//                 throw new ArgumentNullException("output");
//             }

//             if (!String.IsNullOrEmpty(s))
//             {
//                 HttpEncoder.Current.HtmlEncode(s, output);
//             }
//         }
//         public static string HtmlEncode(object value)
//         {
//             if (value == null)
//                 return null;

// #if !(MOBILE || NO_SYSTEM_WEB_DEPENDENCY)
//             IHtmlString htmlString = value as IHtmlString;
//             if (htmlString != null)
//                 return htmlString.ToHtmlString();
// #endif

//             return HtmlEncode(value.ToString());
//         }

//         public static string JavaScriptStringEncode(string value)
//         {
//             return JavaScriptStringEncode(value, false);
//         }

//         public static string JavaScriptStringEncode(string value, bool addDoubleQuotes)
//         {
//             if (String.IsNullOrEmpty(value))
//                 return addDoubleQuotes ? "\"\"" : String.Empty;

//             int len = value.Length;
//             bool needEncode = false;
//             char c;
//             for (int i = 0; i < len; i++)
//             {
//                 c = value[i];

//                 if (c >= 0 && c <= 31 || c == 34 || c == 39 || c == 60 || c == 62 || c == 92)
//                 {
//                     needEncode = true;
//                     break;
//                 }
//             }

//             if (!needEncode)
//                 return addDoubleQuotes ? "\"" + value + "\"" : value;

//             var sb = new StringBuilder();
//             if (addDoubleQuotes)
//                 sb.Append('"');

//             for (int i = 0; i < len; i++)
//             {
//                 c = value[i];
//                 if (c >= 0 && c <= 7 || c == 11 || c >= 14 && c <= 31 || c == 39 || c == 60 || c == 62)
//                     sb.AppendFormat("\\u{0:x4}", (int)c);
//                 else switch ((int)c)
//                     {
//                         case 8:
//                             sb.Append("\\b");
//                             break;

//                         case 9:
//                             sb.Append("\\t");
//                             break;

//                         case 10:
//                             sb.Append("\\n");
//                             break;

//                         case 12:
//                             sb.Append("\\f");
//                             break;

//                         case 13:
//                             sb.Append("\\r");
//                             break;

//                         case 34:
//                             sb.Append("\\\"");
//                             break;

//                         case 92:
//                             sb.Append("\\\\");
//                             break;

//                         default:
//                             sb.Append(c);
//                             break;
//                     }
//             }

//             if (addDoubleQuotes)
//                 sb.Append('"');

//             return sb.ToString();
//         }
//         public static string UrlPathEncode(string s)
//         {
//             return HttpEncoder.Current.UrlPathEncode(s);
//         }

//         public static NameValueCollection ParseQueryString(string query)
//         {
//             return ParseQueryString(query, Encoding.UTF8);
//         }

//         public static NameValueCollection ParseQueryString(string query, Encoding encoding)
//         {
//             if (query == null)
//                 throw new ArgumentNullException("query");
//             if (encoding == null)
//                 throw new ArgumentNullException("encoding");
//             if (query.Length == 0 || (query.Length == 1 && query[0] == '?'))
//                 return new HttpQSCollection();
//             if (query[0] == '?')
//                 query = query.Substring(1);

//             NameValueCollection result = new HttpQSCollection();
//             ParseQueryString(query, encoding, result);
//             return result;
//         }

//         internal static void ParseQueryString(string query, Encoding encoding, NameValueCollection result)
//         {
//             if (query.Length == 0)
//                 return;

//             string decoded = HtmlDecode(query);
//             int decodedLength = decoded.Length;
//             int namePos = 0;
//             bool first = true;
//             while (namePos <= decodedLength)
//             {
//                 int valuePos = -1, valueEnd = -1;
//                 for (int q = namePos; q < decodedLength; q++)
//                 {
//                     if (valuePos == -1 && decoded[q] == '=')
//                     {
//                         valuePos = q + 1;
//                     }
//                     else if (decoded[q] == '&')
//                     {
//                         valueEnd = q;
//                         break;
//                     }
//                 }

//                 if (first)
//                 {
//                     first = false;
//                     if (decoded[namePos] == '?')
//                         namePos++;
//                 }

//                 string name, value;
//                 if (valuePos == -1)
//                 {
//                     name = null;
//                     valuePos = namePos;
//                 }
//                 else
//                 {
//                     name = UrlDecode(decoded.Substring(namePos, valuePos - namePos - 1), encoding);
//                 }
//                 if (valueEnd < 0)
//                 {
//                     namePos = -1;
//                     valueEnd = decoded.Length;
//                 }
//                 else
//                 {
//                     namePos = valueEnd + 1;
//                 }
//                 value = UrlDecode(decoded.Substring(valuePos, valueEnd - valuePos), encoding);

//                 result.Add(name, value);
//                 if (namePos == -1)
//                     break;
//             }
//         }

//         sealed class HttpQSCollection : NameValueCollection
//         {
//             public override string ToString()
//             {
//                 int count = Count;
//                 if (count == 0)
//                     return "";
//                 StringBuilder sb = new StringBuilder();
//                 string[] keys = AllKeys;
//                 for (int i = 0; i < count; i++)
//                 {
//                     sb.AppendFormat("{0}={1}&", keys[i], UrlEncode(this[keys[i]]));
//                 }
//                 if (sb.Length > 0)
//                     sb.Length--;
//                 return sb.ToString();
//             }
//         }
//         public
//     class HttpEncoder
//         {
//             static char[] hexChars = "0123456789abcdef".ToCharArray();
//             static object entitiesLock = new object();
//             static SortedDictionary<string, char> entities;
//             static Lazy<HttpEncoder> defaultEncoder;
//             static Lazy<HttpEncoder> currentEncoderLazy;
//             static HttpEncoder currentEncoder;

//             static IDictionary<string, char> Entities
//             {
//                 get
//                 {
//                     lock (entitiesLock)
//                     {
//                         if (entities == null)
//                             InitEntities();

//                         return entities;
//                     }
//                 }
//             }

//             public static HttpEncoder Current
//             {
//                 get
//                 {
//                     if (currentEncoder == null)
//                         currentEncoder = currentEncoderLazy.Value;
//                     return currentEncoder;
//                 }
//                 set
//                 {
//                     if (value == null)
//                         throw new ArgumentNullException("value");
//                     currentEncoder = value;
//                 }
//             }

//             public static HttpEncoder Default
//             {
//                 get
//                 {
//                     return defaultEncoder.Value;
//                 }
//             }

//             static HttpEncoder()
//             {
//                 defaultEncoder = new Lazy<HttpEncoder>(() => new HttpEncoder());
//                 currentEncoderLazy = new Lazy<HttpEncoder>(new Func<HttpEncoder>(GetCustomEncoderFromConfig));
//             }

//             public HttpEncoder()
//             {
//             }
//             protected internal virtual
//             void HeaderNameValueEncode(string headerName, string headerValue, out string encodedHeaderName, out string encodedHeaderValue)
//             {
//                 if (String.IsNullOrEmpty(headerName))
//                     encodedHeaderName = headerName;
//                 else
//                     encodedHeaderName = EncodeHeaderString(headerName);

//                 if (String.IsNullOrEmpty(headerValue))
//                     encodedHeaderValue = headerValue;
//                 else
//                     encodedHeaderValue = EncodeHeaderString(headerValue);
//             }

//             static void StringBuilderAppend(string s, ref StringBuilder sb)
//             {
//                 if (sb == null)
//                     sb = new StringBuilder(s);
//                 else
//                     sb.Append(s);
//             }

//             static string EncodeHeaderString(string input)
//             {
//                 StringBuilder sb = null;

//                 for (int i = 0; i < input.Length; i++)
//                 {
//                     char ch = input[i];

//                     if ((ch < 32 && ch != 9) || ch == 127)
//                         StringBuilderAppend(String.Format("%{0:x2}", (int)ch), ref sb);
//                 }

//                 if (sb != null)
//                     return sb.ToString();

//                 return input;
//             }
//             protected internal virtual void HtmlAttributeEncode(string value, TextWriter output)
//             {

//                 if (output == null)
//                     throw new ArgumentNullException("output");

//                 if (String.IsNullOrEmpty(value))
//                     return;

//                 output.Write(HtmlAttributeEncode(value));
//             }

//             protected internal virtual void HtmlDecode(string value, TextWriter output)
//             {
//                 if (output == null)
//                     throw new ArgumentNullException("output");

//                 output.Write(HtmlDecode(value));
//             }

//             protected internal virtual void HtmlEncode(string value, TextWriter output)
//             {
//                 if (output == null)
//                     throw new ArgumentNullException("output");

//                 output.Write(HtmlEncode(value));
//             }

//             protected internal virtual byte[] UrlEncode(byte[] bytes, int offset, int count)
//             {
//                 return UrlEncodeToBytes(bytes, offset, count);
//             }

//             static HttpEncoder GetCustomEncoderFromConfig()
//             {
//                 return defaultEncoder.Value;
//             }
//             protected internal virtual
//             string UrlPathEncode(string value)
//             {
//                 if (String.IsNullOrEmpty(value))
//                     return value;

//                 MemoryStream result = new MemoryStream();
//                 int length = value.Length;
//                 for (int i = 0; i < length; i++)
//                     UrlPathEncodeChar(value[i], result);

//                 return Encoding.ASCII.GetString(result.ToArray());
//             }

//             internal static byte[] UrlEncodeToBytes(byte[] bytes, int offset, int count)
//             {
//                 if (bytes == null)
//                     throw new ArgumentNullException("bytes");

//                 int blen = bytes.Length;
//                 if (blen == 0)
//                     return new byte[0];

//                 if (offset < 0 || offset >= blen)
//                     throw new ArgumentOutOfRangeException("offset");

//                 if (count < 0 || count > blen - offset)
//                     throw new ArgumentOutOfRangeException("count");

//                 MemoryStream result = new MemoryStream(count);
//                 int end = offset + count;
//                 for (int i = offset; i < end; i++)
//                     UrlEncodeChar((char)bytes[i], result, false);

//                 return result.ToArray();
//             }

//             internal static string HtmlEncode(string s)
//             {
//                 if (s == null)
//                     return null;

//                 if (s.Length == 0)
//                     return String.Empty;

//                 bool needEncode = false;
//                 for (int i = 0; i < s.Length; i++)
//                 {
//                     char c = s[i];
//                     if (c == '&' || c == '"' || c == '<' || c == '>' || c > 159
//                         || c == '\''
//                     )
//                     {
//                         needEncode = true;
//                         break;
//                     }
//                 }

//                 if (!needEncode)
//                     return s;

//                 StringBuilder output = new StringBuilder();
//                 int len = s.Length;

//                 for (int i = 0; i < len; i++)
//                 {
//                     char ch = s[i];
//                     switch (ch)
//                     {
//                         case '&':
//                             output.Append("&amp;");
//                             break;
//                         case '>':
//                             output.Append("&gt;");
//                             break;
//                         case '<':
//                             output.Append("&lt;");
//                             break;
//                         case '"':
//                             output.Append("&quot;");
//                             break;
//                         case '\'':
//                             output.Append("&#39;");
//                             break;
//                         case '\uff1c':
//                             output.Append("&#65308;");
//                             break;

//                         case '\uff1e':
//                             output.Append("&#65310;");
//                             break;

//                         default:
//                             if (ch > 159 && ch < 256)
//                             {
//                                 output.Append("&#");
//                                 output.Append(((int)ch).ToString(Helpers.InvariantCulture));
//                                 output.Append(";");
//                             }
//                             else
//                                 output.Append(ch);
//                             break;
//                     }
//                 }

//                 return output.ToString();
//             }

//             internal static string HtmlAttributeEncode(string s)
//             {
//                 if (String.IsNullOrEmpty(s))
//                     return String.Empty;
//                 bool needEncode = false;
//                 for (int i = 0; i < s.Length; i++)
//                 {
//                     char c = s[i];
//                     if (c == '&' || c == '"' || c == '<'
//                         || c == '\''
//                     )
//                     {
//                         needEncode = true;
//                         break;
//                     }
//                 }

//                 if (!needEncode)
//                     return s;

//                 StringBuilder output = new StringBuilder();
//                 int len = s.Length;

//                 for (int i = 0; i < len; i++)
//                 {
//                     char ch = s[i];
//                     switch (ch)
//                     {
//                         case '&':
//                             output.Append("&amp;");
//                             break;
//                         case '"':
//                             output.Append("&quot;");
//                             break;
//                         case '<':
//                             output.Append("&lt;");
//                             break;
//                         case '\'':
//                             output.Append("&#39;");
//                             break;
//                         default:
//                             output.Append(ch);
//                             break;
//                     }
//                 }

//                 return output.ToString();
//             }

//             internal static string HtmlDecode(string s)
//             {
//                 if (s == null)
//                     return null;

//                 if (s.Length == 0)
//                     return String.Empty;

//                 if (s.IndexOf('&') == -1)
//                     return s;
//                 StringBuilder rawEntity = new StringBuilder();
//                 StringBuilder entity = new StringBuilder();
//                 StringBuilder output = new StringBuilder();
//                 int len = s.Length;
//                 // 0 -> nothing,
//                 // 1 -> right after '&'
//                 // 2 -> between '&' and ';' but no '#'
//                 // 3 -> '#' found after '&' and getting numbers
//                 int state = 0;
//                 int number = 0;
//                 bool is_hex_value = false;
//                 bool have_trailing_digits = false;

//                 for (int i = 0; i < len; i++)
//                 {
//                     char c = s[i];
//                     if (state == 0)
//                     {
//                         if (c == '&')
//                         {
//                             entity.Append(c);
//                             rawEntity.Append(c);
//                             state = 1;
//                         }
//                         else
//                         {
//                             output.Append(c);
//                         }
//                         continue;
//                     }

//                     if (c == '&')
//                     {
//                         state = 1;
//                         if (have_trailing_digits)
//                         {
//                             entity.Append(number.ToString(Helpers.InvariantCulture));
//                             have_trailing_digits = false;
//                         }

//                         output.Append(entity.ToString());
//                         entity.Length = 0;
//                         entity.Append('&');
//                         continue;
//                     }

//                     if (state == 1)
//                     {
//                         if (c == ';')
//                         {
//                             state = 0;
//                             output.Append(entity.ToString());
//                             output.Append(c);
//                             entity.Length = 0;
//                         }
//                         else
//                         {
//                             number = 0;
//                             is_hex_value = false;
//                             if (c != '#')
//                             {
//                                 state = 2;
//                             }
//                             else
//                             {
//                                 state = 3;
//                             }
//                             entity.Append(c);
//                             rawEntity.Append(c);
//                         }
//                     }
//                     else if (state == 2)
//                     {
//                         entity.Append(c);
//                         if (c == ';')
//                         {
//                             string key = entity.ToString();
//                             if (key.Length > 1 && Entities.ContainsKey(key.Substring(1, key.Length - 2)))
//                                 key = Entities[key.Substring(1, key.Length - 2)].ToString();

//                             output.Append(key);
//                             state = 0;
//                             entity.Length = 0;
//                             rawEntity.Length = 0;
//                         }
//                     }
//                     else if (state == 3)
//                     {
//                         if (c == ';')
//                         {
//                             if (number == 0)
//                                 output.Append(rawEntity.ToString() + ";");
//                             else
//                             if (number > 65535)
//                             {
//                                 output.Append("&#");
//                                 output.Append(number.ToString(Helpers.InvariantCulture));
//                                 output.Append(";");
//                             }
//                             else
//                             {
//                                 output.Append((char)number);
//                             }
//                             state = 0;
//                             entity.Length = 0;
//                             rawEntity.Length = 0;
//                             have_trailing_digits = false;
//                         }
//                         else if (is_hex_value && Uri.IsHexDigit(c))
//                         {
//                             number = number * 16 + Uri.FromHex(c);
//                             have_trailing_digits = true;
//                             rawEntity.Append(c);
//                         }
//                         else if (Char.IsDigit(c))
//                         {
//                             number = number * 10 + ((int)c - '0');
//                             have_trailing_digits = true;
//                             rawEntity.Append(c);
//                         }
//                         else if (number == 0 && (c == 'x' || c == 'X'))
//                         {
//                             is_hex_value = true;
//                             rawEntity.Append(c);
//                         }
//                         else
//                         {
//                             state = 2;
//                             if (have_trailing_digits)
//                             {
//                                 entity.Append(number.ToString(Helpers.InvariantCulture));
//                                 have_trailing_digits = false;
//                             }
//                             entity.Append(c);
//                         }
//                     }
//                 }

//                 if (entity.Length > 0)
//                 {
//                     output.Append(entity.ToString());
//                 }
//                 else if (have_trailing_digits)
//                 {
//                     output.Append(number.ToString(Helpers.InvariantCulture));
//                 }
//                 return output.ToString();
//             }

//             internal static bool NotEncoded(char c)
//             {
//                 return (c == '!' || c == '(' || c == ')' || c == '*' || c == '-' || c == '.' || c == '_'
//                 );
//             }

//             internal static void UrlEncodeChar(char c, Stream result, bool isUnicode)
//             {
//                 if (c > 255)
//                 {
//                     //FIXME: what happens when there is an internal error?
//                     //if (!isUnicode)
//                     //	throw new ArgumentOutOfRangeException ("c", c, "c must be less than 256");
//                     int idx;
//                     int i = (int)c;

//                     result.WriteByte((byte)'%');
//                     result.WriteByte((byte)'u');
//                     idx = i >> 12;
//                     result.WriteByte((byte)hexChars[idx]);
//                     idx = (i >> 8) & 0x0F;
//                     result.WriteByte((byte)hexChars[idx]);
//                     idx = (i >> 4) & 0x0F;
//                     result.WriteByte((byte)hexChars[idx]);
//                     idx = i & 0x0F;
//                     result.WriteByte((byte)hexChars[idx]);
//                     return;
//                 }

//                 if (c > ' ' && NotEncoded(c))
//                 {
//                     result.WriteByte((byte)c);
//                     return;
//                 }
//                 if (c == ' ')
//                 {
//                     result.WriteByte((byte)'+');
//                     return;
//                 }
//                 if ((c < '0') ||
//                     (c < 'A' && c > '9') ||
//                     (c > 'Z' && c < 'a') ||
//                     (c > 'z'))
//                 {
//                     if (isUnicode && c > 127)
//                     {
//                         result.WriteByte((byte)'%');
//                         result.WriteByte((byte)'u');
//                         result.WriteByte((byte)'0');
//                         result.WriteByte((byte)'0');
//                     }
//                     else
//                         result.WriteByte((byte)'%');

//                     int idx = ((int)c) >> 4;
//                     result.WriteByte((byte)hexChars[idx]);
//                     idx = ((int)c) & 0x0F;
//                     result.WriteByte((byte)hexChars[idx]);
//                 }
//                 else
//                     result.WriteByte((byte)c);
//             }

//             internal static void UrlPathEncodeChar(char c, Stream result)
//             {
//                 if (c < 33 || c > 126)
//                 {
//                     byte[] bIn = Encoding.UTF8.GetBytes(c.ToString());
//                     for (int i = 0; i < bIn.Length; i++)
//                     {
//                         result.WriteByte((byte)'%');
//                         int idx = ((int)bIn[i]) >> 4;
//                         result.WriteByte((byte)hexChars[idx]);
//                         idx = ((int)bIn[i]) & 0x0F;
//                         result.WriteByte((byte)hexChars[idx]);
//                     }
//                 }
//                 else if (c == ' ')
//                 {
//                     result.WriteByte((byte)'%');
//                     result.WriteByte((byte)'2');
//                     result.WriteByte((byte)'0');
//                 }
//                 else
//                     result.WriteByte((byte)c);
//             }

//             static void InitEntities()
//             {
//                 // Build the hash table of HTML entity references.  This list comes
//                 // from the HTML 4.01 W3C recommendation.
//                 entities = new SortedDictionary<string, char>(StringComparer.Ordinal);

//                 entities.Add("nbsp", '\u00A0');
//                 entities.Add("iexcl", '\u00A1');
//                 entities.Add("cent", '\u00A2');
//                 entities.Add("pound", '\u00A3');
//                 entities.Add("curren", '\u00A4');
//                 entities.Add("yen", '\u00A5');
//                 entities.Add("brvbar", '\u00A6');
//                 entities.Add("sect", '\u00A7');
//                 entities.Add("uml", '\u00A8');
//                 entities.Add("copy", '\u00A9');
//                 entities.Add("ordf", '\u00AA');
//                 entities.Add("laquo", '\u00AB');
//                 entities.Add("not", '\u00AC');
//                 entities.Add("shy", '\u00AD');
//                 entities.Add("reg", '\u00AE');
//                 entities.Add("macr", '\u00AF');
//                 entities.Add("deg", '\u00B0');
//                 entities.Add("plusmn", '\u00B1');
//                 entities.Add("sup2", '\u00B2');
//                 entities.Add("sup3", '\u00B3');
//                 entities.Add("acute", '\u00B4');
//                 entities.Add("micro", '\u00B5');
//                 entities.Add("para", '\u00B6');
//                 entities.Add("middot", '\u00B7');
//                 entities.Add("cedil", '\u00B8');
//                 entities.Add("sup1", '\u00B9');
//                 entities.Add("ordm", '\u00BA');
//                 entities.Add("raquo", '\u00BB');
//                 entities.Add("frac14", '\u00BC');
//                 entities.Add("frac12", '\u00BD');
//                 entities.Add("frac34", '\u00BE');
//                 entities.Add("iquest", '\u00BF');
//                 entities.Add("Agrave", '\u00C0');
//                 entities.Add("Aacute", '\u00C1');
//                 entities.Add("Acirc", '\u00C2');
//                 entities.Add("Atilde", '\u00C3');
//                 entities.Add("Auml", '\u00C4');
//                 entities.Add("Aring", '\u00C5');
//                 entities.Add("AElig", '\u00C6');
//                 entities.Add("Ccedil", '\u00C7');
//                 entities.Add("Egrave", '\u00C8');
//                 entities.Add("Eacute", '\u00C9');
//                 entities.Add("Ecirc", '\u00CA');
//                 entities.Add("Euml", '\u00CB');
//                 entities.Add("Igrave", '\u00CC');
//                 entities.Add("Iacute", '\u00CD');
//                 entities.Add("Icirc", '\u00CE');
//                 entities.Add("Iuml", '\u00CF');
//                 entities.Add("ETH", '\u00D0');
//                 entities.Add("Ntilde", '\u00D1');
//                 entities.Add("Ograve", '\u00D2');
//                 entities.Add("Oacute", '\u00D3');
//                 entities.Add("Ocirc", '\u00D4');
//                 entities.Add("Otilde", '\u00D5');
//                 entities.Add("Ouml", '\u00D6');
//                 entities.Add("times", '\u00D7');
//                 entities.Add("Oslash", '\u00D8');
//                 entities.Add("Ugrave", '\u00D9');
//                 entities.Add("Uacute", '\u00DA');
//                 entities.Add("Ucirc", '\u00DB');
//                 entities.Add("Uuml", '\u00DC');
//                 entities.Add("Yacute", '\u00DD');
//                 entities.Add("THORN", '\u00DE');
//                 entities.Add("szlig", '\u00DF');
//                 entities.Add("agrave", '\u00E0');
//                 entities.Add("aacute", '\u00E1');
//                 entities.Add("acirc", '\u00E2');
//                 entities.Add("atilde", '\u00E3');
//                 entities.Add("auml", '\u00E4');
//                 entities.Add("aring", '\u00E5');
//                 entities.Add("aelig", '\u00E6');
//                 entities.Add("ccedil", '\u00E7');
//                 entities.Add("egrave", '\u00E8');
//                 entities.Add("eacute", '\u00E9');
//                 entities.Add("ecirc", '\u00EA');
//                 entities.Add("euml", '\u00EB');
//                 entities.Add("igrave", '\u00EC');
//                 entities.Add("iacute", '\u00ED');
//                 entities.Add("icirc", '\u00EE');
//                 entities.Add("iuml", '\u00EF');
//                 entities.Add("eth", '\u00F0');
//                 entities.Add("ntilde", '\u00F1');
//                 entities.Add("ograve", '\u00F2');
//                 entities.Add("oacute", '\u00F3');
//                 entities.Add("ocirc", '\u00F4');
//                 entities.Add("otilde", '\u00F5');
//                 entities.Add("ouml", '\u00F6');
//                 entities.Add("divide", '\u00F7');
//                 entities.Add("oslash", '\u00F8');
//                 entities.Add("ugrave", '\u00F9');
//                 entities.Add("uacute", '\u00FA');
//                 entities.Add("ucirc", '\u00FB');
//                 entities.Add("uuml", '\u00FC');
//                 entities.Add("yacute", '\u00FD');
//                 entities.Add("thorn", '\u00FE');
//                 entities.Add("yuml", '\u00FF');
//                 entities.Add("fnof", '\u0192');
//                 entities.Add("Alpha", '\u0391');
//                 entities.Add("Beta", '\u0392');
//                 entities.Add("Gamma", '\u0393');
//                 entities.Add("Delta", '\u0394');
//                 entities.Add("Epsilon", '\u0395');
//                 entities.Add("Zeta", '\u0396');
//                 entities.Add("Eta", '\u0397');
//                 entities.Add("Theta", '\u0398');
//                 entities.Add("Iota", '\u0399');
//                 entities.Add("Kappa", '\u039A');
//                 entities.Add("Lambda", '\u039B');
//                 entities.Add("Mu", '\u039C');
//                 entities.Add("Nu", '\u039D');
//                 entities.Add("Xi", '\u039E');
//                 entities.Add("Omicron", '\u039F');
//                 entities.Add("Pi", '\u03A0');
//                 entities.Add("Rho", '\u03A1');
//                 entities.Add("Sigma", '\u03A3');
//                 entities.Add("Tau", '\u03A4');
//                 entities.Add("Upsilon", '\u03A5');
//                 entities.Add("Phi", '\u03A6');
//                 entities.Add("Chi", '\u03A7');
//                 entities.Add("Psi", '\u03A8');
//                 entities.Add("Omega", '\u03A9');
//                 entities.Add("alpha", '\u03B1');
//                 entities.Add("beta", '\u03B2');
//                 entities.Add("gamma", '\u03B3');
//                 entities.Add("delta", '\u03B4');
//                 entities.Add("epsilon", '\u03B5');
//                 entities.Add("zeta", '\u03B6');
//                 entities.Add("eta", '\u03B7');
//                 entities.Add("theta", '\u03B8');
//                 entities.Add("iota", '\u03B9');
//                 entities.Add("kappa", '\u03BA');
//                 entities.Add("lambda", '\u03BB');
//                 entities.Add("mu", '\u03BC');
//                 entities.Add("nu", '\u03BD');
//                 entities.Add("xi", '\u03BE');
//                 entities.Add("omicron", '\u03BF');
//                 entities.Add("pi", '\u03C0');
//                 entities.Add("rho", '\u03C1');
//                 entities.Add("sigmaf", '\u03C2');
//                 entities.Add("sigma", '\u03C3');
//                 entities.Add("tau", '\u03C4');
//                 entities.Add("upsilon", '\u03C5');
//                 entities.Add("phi", '\u03C6');
//                 entities.Add("chi", '\u03C7');
//                 entities.Add("psi", '\u03C8');
//                 entities.Add("omega", '\u03C9');
//                 entities.Add("thetasym", '\u03D1');
//                 entities.Add("upsih", '\u03D2');
//                 entities.Add("piv", '\u03D6');
//                 entities.Add("bull", '\u2022');
//                 entities.Add("hellip", '\u2026');
//                 entities.Add("prime", '\u2032');
//                 entities.Add("Prime", '\u2033');
//                 entities.Add("oline", '\u203E');
//                 entities.Add("frasl", '\u2044');
//                 entities.Add("weierp", '\u2118');
//                 entities.Add("image", '\u2111');
//                 entities.Add("real", '\u211C');
//                 entities.Add("trade", '\u2122');
//                 entities.Add("alefsym", '\u2135');
//                 entities.Add("larr", '\u2190');
//                 entities.Add("uarr", '\u2191');
//                 entities.Add("rarr", '\u2192');
//                 entities.Add("darr", '\u2193');
//                 entities.Add("harr", '\u2194');
//                 entities.Add("crarr", '\u21B5');
//                 entities.Add("lArr", '\u21D0');
//                 entities.Add("uArr", '\u21D1');
//                 entities.Add("rArr", '\u21D2');
//                 entities.Add("dArr", '\u21D3');
//                 entities.Add("hArr", '\u21D4');
//                 entities.Add("forall", '\u2200');
//                 entities.Add("part", '\u2202');
//                 entities.Add("exist", '\u2203');
//                 entities.Add("empty", '\u2205');
//                 entities.Add("nabla", '\u2207');
//                 entities.Add("isin", '\u2208');
//                 entities.Add("notin", '\u2209');
//                 entities.Add("ni", '\u220B');
//                 entities.Add("prod", '\u220F');
//                 entities.Add("sum", '\u2211');
//                 entities.Add("minus", '\u2212');
//                 entities.Add("lowast", '\u2217');
//                 entities.Add("radic", '\u221A');
//                 entities.Add("prop", '\u221D');
//                 entities.Add("infin", '\u221E');
//                 entities.Add("ang", '\u2220');
//                 entities.Add("and", '\u2227');
//                 entities.Add("or", '\u2228');
//                 entities.Add("cap", '\u2229');
//                 entities.Add("cup", '\u222A');
//                 entities.Add("int", '\u222B');
//                 entities.Add("there4", '\u2234');
//                 entities.Add("sim", '\u223C');
//                 entities.Add("cong", '\u2245');
//                 entities.Add("asymp", '\u2248');
//                 entities.Add("ne", '\u2260');
//                 entities.Add("equiv", '\u2261');
//                 entities.Add("le", '\u2264');
//                 entities.Add("ge", '\u2265');
//                 entities.Add("sub", '\u2282');
//                 entities.Add("sup", '\u2283');
//                 entities.Add("nsub", '\u2284');
//                 entities.Add("sube", '\u2286');
//                 entities.Add("supe", '\u2287');
//                 entities.Add("oplus", '\u2295');
//                 entities.Add("otimes", '\u2297');
//                 entities.Add("perp", '\u22A5');
//                 entities.Add("sdot", '\u22C5');
//                 entities.Add("lceil", '\u2308');
//                 entities.Add("rceil", '\u2309');
//                 entities.Add("lfloor", '\u230A');
//                 entities.Add("rfloor", '\u230B');
//                 entities.Add("lang", '\u2329');
//                 entities.Add("rang", '\u232A');
//                 entities.Add("loz", '\u25CA');
//                 entities.Add("spades", '\u2660');
//                 entities.Add("clubs", '\u2663');
//                 entities.Add("hearts", '\u2665');
//                 entities.Add("diams", '\u2666');
//                 entities.Add("quot", '\u0022');
//                 entities.Add("amp", '\u0026');
//                 entities.Add("lt", '\u003C');
//                 entities.Add("gt", '\u003E');
//                 entities.Add("OElig", '\u0152');
//                 entities.Add("oelig", '\u0153');
//                 entities.Add("Scaron", '\u0160');
//                 entities.Add("scaron", '\u0161');
//                 entities.Add("Yuml", '\u0178');
//                 entities.Add("circ", '\u02C6');
//                 entities.Add("tilde", '\u02DC');
//                 entities.Add("ensp", '\u2002');
//                 entities.Add("emsp", '\u2003');
//                 entities.Add("thinsp", '\u2009');
//                 entities.Add("zwnj", '\u200C');
//                 entities.Add("zwj", '\u200D');
//                 entities.Add("lrm", '\u200E');
//                 entities.Add("rlm", '\u200F');
//                 entities.Add("ndash", '\u2013');
//                 entities.Add("mdash", '\u2014');
//                 entities.Add("lsquo", '\u2018');
//                 entities.Add("rsquo", '\u2019');
//                 entities.Add("sbquo", '\u201A');
//                 entities.Add("ldquo", '\u201C');
//                 entities.Add("rdquo", '\u201D');
//                 entities.Add("bdquo", '\u201E');
//                 entities.Add("dagger", '\u2020');
//                 entities.Add("Dagger", '\u2021');
//                 entities.Add("permil", '\u2030');
//                 entities.Add("lsaquo", '\u2039');
//                 entities.Add("rsaquo", '\u203A');
//                 entities.Add("euro", '\u20AC');
//             }
//         }

//         class Helpers
//         {
//             public static readonly CultureInfo InvariantCulture = CultureInfo.InvariantCulture;
//         }

//         // Marker interface implemented by objects that should NOT be HTML encoded when <%: o %> is used
//         public interface IHtmlString
//         {
//             string ToHtmlString();
//         }
//     }
// }
