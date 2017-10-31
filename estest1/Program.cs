using PlainElastic.Net;
using PlainElastic.Net.Queries;
using PlainElastic.Net.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace estest1
{
    class Program
    {
        static void Main(string[] args)
        {
            Main2();
        }

        static void Main2() {
            ElasticConnection client = new ElasticConnection("localhost", 9200);
            SearchCommand cmd = new SearchCommand("hlxzsz", "persons");
            var query = new QueryBuilder<Person>()
            .Query(b =>
                    b.Bool(m =>
                    //并且关系
                    m.Must(t =>
                       //分词的最小单位或关系查询
                       t.QueryString(t1 => t1.DefaultField("Name").Query("帅"))
                         )
                      )
                    )
            //分页
            /*
            .From(0)//Skip()
            .Size(10)//Take()*/
            //排序
            //.Sort(c => c.Field("Age", SortDirection.desc))
            //添加高亮
            /*
            .Highlight(h => h
            .PreTags("<b>")
            .PostTags("</b>")
            .Fields(
                 f => f.FieldName("Name").Order(HighlightOrder.score)
                 )
            )*/
            .Build();
            var result = client.Post(cmd, query);
            var serializer = new JsonNetSerializer();
            var searchResult = serializer.ToSearchResult<Person>(result);
            //searchResult.hits.total; //一共有多少匹配结果  10500
            // searchResult.Documents;//当前页的查询结果 
            foreach (var doc in searchResult.Documents)
            {
                Console.WriteLine(doc.Id + "," + doc.Name + "," + doc.Age);
            }
            Console.ReadKey();



        }

        static void Main1()
        {
            Person p1 = new Person();
            p1.Id = 1;
            p1.Age = 8;
            p1.Name = "ll";
            p1.Desc = "最美丽的女孩";

            ElasticConnection client = new ElasticConnection("localhost", 9200);
            var serializer = new JsonNetSerializer();
            //第一个参数相当于“数据库”，第二个参数相当于“表”，第三个参数相当于“主键”
            IndexCommand cmd = new IndexCommand("Hlxzsz", "persons", p1.Id.ToString());
            //Put()第二个参数是要插入的数据
            OperationResult result = client.Put(cmd, serializer.Serialize(p1));
            var indexResult = serializer.ToIndexResult(result.Result);
            if (indexResult.created)
            {
                Console.WriteLine("创建了");
            }
            else
            {
                Console.WriteLine("没创建" + indexResult.error);
            }
            Console.ReadKey();
        }
    }
}
