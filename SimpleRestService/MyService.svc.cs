using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Services;

// so when this application is running in the browser, you can type in a url like http://localhost:1234/MyService.svc/json/5 and this should then return a JSON message like {"Multiples":[5,10,15,20,25,30,35,40,45,50,55,60,65,70,75,80,85,90,95,100],"Number":0} where that Multiples array contains multiples of 5 (the number we typed in)

namespace SimpleRestService
{
    [ServiceContract]
    public class MyService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/json/{number}", ResponseFormat = WebMessageFormat.Json)]  // once we use WebGet, then we know that we'll be using REST to access the data. That's another attribute that's being applied to it, so that the WCF environment now knows to start changing how this is being dealt with. This changes behavior but keeps the same syntax. Here, if URL says "/json/" then we know we'll be returning JSON, and we say what repsonse format we'll have
        public Data GetMultipleJson(string number)
        {
            var x = new Data(int.Parse(number));
            return new Data(int.Parse(number));
        }

        [OperationContract]
        [WebGet(UriTemplate = "/xml/{number}", ResponseFormat = WebMessageFormat.Xml)] // here if the URL says "/xml/" then we know we'll be returning XML, and we say what repsonse format we'll have
        public Data GetMultipleXml(string number)
        {
            return new Data(int.Parse(number));
        }
    }

    [DataContract]
    public class Data
    {
        public Data(int number)
        {
            var list = Enumerable.Range(1, 100);
            this.Multiples = list.Where(x => x % number == 0).ToArray();
        }
        [DataMember]
        public int Number { get; set; }
        [DataMember]
        public int[] Multiples { get; set; }
    }
}
