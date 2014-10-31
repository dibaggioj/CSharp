using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

/* Needed to add additional assemblies:
 *     System.Web.Extensions (for System.Web.Script namespace, which contains the JavaScriptSerializer)
 *     System.Runtime.Serialization (for System.Runtime.Serialization and System.Runtime.Serialization.JSON)
 */
// Needed to install Microsoft.BCL.Async from Nuget Package Manager (double check the packages.config file)


// to debug the 3 different scenarios in this application, startup the SimpleRestService/MyService.svc first (by setting as startup project and running / by opening in the browser)

namespace WorkingWithRest
{
    class Program
    {
        // System.Web.Extensions.dll
        // http://localhost:1234/MyService.svc/json/4
        // http://localhost:1234/MyService.svc/xml/4

        static void Main(string[] args)
        {
            Action action = async () =>
            {
                var stopwatch = new System.Diagnostics.Stopwatch();

                // Scenario 1: using JavaScriptSerializer, from the System.Web.Script namespace (which you might not want to include in your project)
                Console.WriteLine("JSON (JavaScriptSerializer)");
                {
                    // basic implementation of JSON using a web client, which is a wrapper around the HTTP client and it exists because it handles common operations

                    // fetch data (as JSON string)
                    var url = new Uri("http://localhost:1234/MyService.svc/json/4"); // .svc is a very simple implementation of a servie (Web API oculd also be used, which is more in-line with REST-based services)
                    var client = new System.Net.WebClient();
                    var json = await client.DownloadStringTaskAsync(url); // returns just a string

                    // deserialize JSON into objects
                    var serializer = new JavaScriptSerializer(); // JavaScriptSerializer is part of the framework, for interacting with JSON. The serializer serializes and deserializes
                    var data = serializer.Deserialize<JSONSAMPLE.Data>(json); // we call for it to deserialize, and when it does, instead of a string, it truly becomes objects at that time. (now we're working with objects in a type-safe way and we're no longer dealing with strings which would have to be parsed etc). When this is running data becomes strongly-type after this line (you can see in debugger)
                    // this data is a strong type, not an anonymous type
                    // note: there is no guarantee the JSON can be deserialized (there could be a typo in the JSON), so you'd need to add some guards around it

                    // use the objects (demonstration)
                    Console.WriteLine(data.Number);
                    foreach (var item in data.Multiples)
                        Console.Write("{0}, ", item);
                }

                // Scenario 2: using DataContractJsonSerializer, from a namespace that you might want to include in your project
                Console.WriteLine();
                Console.WriteLine("JSON (DataContractJsonSerializer)");
                {
                    var url = new Uri("http://localhost:1234/MyService.svc/json/4");
                    var client = new System.Net.WebClient();
                    var json = await client.OpenReadTaskAsync(url);
                    var serializer = new DataContractJsonSerializer(typeof(DATACONTRACT.Data)); // make object
                    var data = serializer.ReadObject(json) as DATACONTRACT.Data; // object isn't enough, so make it exactly how you want it. Note: this is pretty safe, but you'd want to add some guards around this to deal with possible exceptions (eg., make sure ReadObject doesn't return an exception and doesn't return a null). When this is running data becomes strongly-type after this line (you can see in debugger)
                    Console.WriteLine(data.Number);
                    foreach (var item in data.Multiples)
                        Console.Write("{0}, ", item);
                }

                // Scenario 3: returning XML. The WCF environment now know how to start changing how this is being dealt with
                Console.WriteLine();
                Console.WriteLine("XML");
                {
                    var url = new Uri("http://localhost:1234/MyService.svc/xml/4");
                    var client = new System.Net.WebClient();
                    var xml = await client.DownloadStringTaskAsync(url);
                    var bytes = Encoding.UTF8.GetBytes(xml);
                    using (MemoryStream stream = new MemoryStream(bytes))
                    {
                        var serializer = new XmlSerializer(typeof(XMLSAMPLE.Data));
                        var data = serializer.Deserialize(stream) as XMLSAMPLE.Data;
                        Console.WriteLine(data.Number);
                        foreach (var item in data.Multiples)
                            Console.Write("{0}, ", item);
                    }
                }
            };

            action.Invoke();
            Console.Read();
        }
    }

    namespace JSONSAMPLE // this implementation is simple, but it can also break
    {
        public class Data // this is the type we want to cast the JSON object into
        {
            public int Number { get; set; }
            public int[] Multiples { get; set; }
        }
    }

    namespace DATACONTRACT // this implementation is safer than the above
    {
        [DataContract]
        public class Data // this is the receiving type
        {
            [DataMember] // this line can be decorated with parameters in order to make the data look how you need it to in case it does not arrive how you expect it. The DataContractSerializer uses reflections to find the attributes placed on its members and then it can look up and those those attribute properties such as Name and then it can map a different incoming name to that property. It wouldn't know about these properties if it didn't reflect into it
            public int Number { get; set; }
            [DataMember]
            public int[] Multiples { get; set; }
        }
    }

    namespace XMLSAMPLE
    {
        [XmlRoot(Namespace = "http://schemas.datacontract.org/2004/07/SimpleRestService")]
        public class Data
        {
            public int Number { get; set; }
            [XmlArrayItem(Namespace = "http://schemas.microsoft.com/2003/10/Serialization/Arrays")]
            public int[] Multiples { get; set; }
        }
    }

}
