using System;
using System.Collections.Generic;
using System.Linq;

namespace api.dane.gov.pl
{
    // klasa reprezentuje typ zwracany przez api
    // zgodny ze wzorcem https://api.dane.gov.pl/doc#model-ResourceTableRow
    // slozy do zmapowania odpowiedzi a nastepnie wyciagniecie interesujacej wartosci
    //
    // wykorzystano specjalną funkcję VisualStudio2019 do wygenerowania klasy
    // z odpowiedzi pod zapytaniem https://api.dane.gov.pl/1.4/resources/28087/data?page=1&per_page=100

    internal class LastnameResponse : ResponseType
    {
        public Jsonapi jsonapi { get; set; }
        public Datum[] data { get; set; }
        public Links links { get; set; }
        public Meta meta { get; set; }

        public override List<string> GetData()
        {
            return data.Select(item => item.attributes.col1.val).ToList();
        }

        internal class Jsonapi
        {
            public string version { get; set; }
        }

        internal class Links
        {
            public string self { get; set; }
            public string last { get; set; }
            public string next { get; set; }
        }

        internal class Meta
        {
            public string relative_uri { get; set; }
            public Params _params { get; set; }
            public string language { get; set; }
            public DateTime server_time { get; set; }
            public Data_Schema data_schema { get; set; }
            public int count { get; set; }
            public string path { get; set; }
            public Headers_Map headers_map { get; set; }
            public Aggregations aggregations { get; set; }
        }

        internal class Params
        {
            public string page { get; set; }
            public string per_page { get; set; }
        }

        internal class Data_Schema
        {
            public Field[] fields { get; set; }
            public string[] missingValues { get; set; }
        }

        internal class Field
        {
            public string name { get; set; }
            public string type { get; set; }
            public string format { get; set; }
        }

        internal class Headers_Map
        {
            public string col1 { get; set; }
            public string col2 { get; set; }
        }

        internal class Aggregations
        {
        }

        internal class Datum
        {
            public string type { get; set; }
            public Attributes attributes { get; set; }
            public Relationships relationships { get; set; }
            public Links2 links { get; set; }
            public string id { get; set; }
            public Meta1 meta { get; set; }
        }

        internal class Attributes
        {
            public Col2 col2 { get; set; }
            public Col1 col1 { get; set; }
        }

        internal class Col2
        {
            public int repr { get; set; }
            public float val { get; set; }
        }

        internal class Col1
        {
            public string repr { get; set; }
            public string val { get; set; }
        }

        internal class Relationships
        {
            public Resource resource { get; set; }
        }

        internal class Resource
        {
            public Links1 links { get; set; }
            public Data data { get; set; }
        }

        internal class Links1
        {
            public string related { get; set; }
        }

        internal class Data
        {
            public string id { get; set; }
            public string type { get; set; }
        }

        internal class Links2
        {
            public string self { get; set; }
        }

        internal class Meta1
        {
            public int row_no { get; set; }
            public DateTime updated_at { get; set; }
        }
    }


    
}
