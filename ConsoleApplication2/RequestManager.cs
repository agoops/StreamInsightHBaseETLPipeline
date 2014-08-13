using System;
using System.ServiceModel;
using Microsoft.ComplexEventProcessing;
using Microsoft.ComplexEventProcessing.Linq;
using Microsoft.ComplexEventProcessing.ManagementService;
using System.Reactive;
using System.Reactive.Linq;
using System.Data.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;

using System.Management.Automation;
using System.Collections.ObjectModel;
using System.Security;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System.Net;

namespace ConsoleApplication2
{
    class RequestManager
    {
        public class RequestHolder
        {
            private string tableName { get; set; }
            public int CAPACITY = 1000;
            private int RowsPut = 0;

            public RequestHolder(string tName, int capacity)
            {
                this.tableName = tName;
                this.CAPACITY = capacity;
            }

            public RowBody rowBody { get; set; }
            public void addRow(RowElement r)
            {
                this.rowBody.Row.Add(r);

                if (this.rowBody.Row.Count >= CAPACITY)
                {
                    putRowBody(this.tableName, "falsekey", this.rowBody, CAPACITY);
                    RowsPut += CAPACITY;
                    Console.WriteLine(RowsPut + " rows put in " + tableName);

                    this.rowBody.Row.Clear();
                }
                
            }
        }


        private static string CLUSTER_URL = "https://crmhbasecluster2.azurehdinsight.net/hbaserest/";

        public static void putRowBody(string tableName, string key, RequestManager.RowBody rowBody, int numRows)
        {
            
            //Pass tablename, key, rowbody to be submitted via PUT request
            var start = DateTime.Now;
            putRow(CLUSTER_URL+tableName, key, GetJsonRowBody(rowBody));
            var end = DateTime.Now;
            var timeTaken = (end - start).TotalMilliseconds;
            double rate = ((double)numRows) / ( timeTaken / 1000.0);

            Console.WriteLine("\nTime Taken: " + timeTaken + " ms");
            Console.WriteLine("Rate: " + rate + " rows/s");
        }


        static void putRow(string tableUrl, string rowKey, string rowBody)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            string jsonBody = rowBody;
            string url = tableUrl + "/" + rowKey;
            Uri uri = new Uri(url);
           
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "PUT";
            request.ContentType = "application/json";
            request.Credentials = new NetworkCredential("admin", "Turtledive11)");

            byte[] byte1 = encoding.GetBytes(jsonBody);
            request.ContentLength = byte1.Length;
            Stream dataStream = request.GetRequestStream();
            var start = DateTime.Now;
            dataStream.Write(byte1, 0, byte1.Length);
            dataStream.Close();
            var end = start;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                end = DateTime.Now;
                Console.WriteLine("\tInternet Time: " + (end - start).TotalMilliseconds);
                Console.WriteLine(response.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }



        private static string GetJsonRowBody(RowBody input) {
            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(RowBody));

            ser.WriteObject(stream1, input);
            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            string result = sr.ReadToEnd();
            return result;
        }
        public static string ToBase64(string input){
            var bytes = Encoding.UTF8.GetBytes(input);
            var base64 = Convert.ToBase64String(bytes);
            return base64;
        }




        [DataContract]
        internal class RowBody
        {
            [DataMember]
            internal List<RowElement> Row;
        }

        [DataContract]
        internal class RowElement
        {
            [DataMember]
            internal string key;

            [DataMember]
            internal List<CellElement> Cell;
        }

        [DataContract]
        internal class CellElement
        {
            [DataMember]
            internal string column;
            [DataMember(Name="$")]
            internal string value;
        }
    }
}
