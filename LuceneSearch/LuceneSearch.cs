using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using SocialNetwork.Models;

namespace SocialNetwork
{
    public class LuceneSearch
    {
        public LuceneSearch()
        {
            BuildIndex();
        }

        public static string path = Path.Combine(System.Web.HttpContext.Current.Request.PhysicalApplicationPath, "LuceneIndex");
        //Take data from database and copied in documents
        public Document GetDocument(UserTaskModel userTask)
        {
            var document = new Document();
            document.Add(new Field("User", userTask.User.UserName, Field.Store.NO, Field.Index.ANALYZED));
            document.Add(new Field("Title", userTask.UserTaskTitle, Field.Store.NO, Field.Index.ANALYZED));
            document.Add(new Field("Content", userTask.UserTaskContent, Field.Store.NO, Field.Index.ANALYZED));
            var array = String.Join(" ", userTask.Comments.Select(x => x.CommentContent).ToArray());
            document.Add(new Field("Comments", array, Field.Store.NO, Field.Index.ANALYZED));
            document.Add(new Field("Id", value: userTask.Id.ToString(CultureInfo.InvariantCulture), store: Field.Store.YES, index: Field.Index.NO));
            return document;
        }

        public void BuildIndex()
        {
            var ac = new ApplicationDbContext();
            List<UserTaskModel> userTasks = ac.UserTasks.ToList();
            var directory = FSDirectory.Open(new DirectoryInfo(path));
            var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            var indexWriter = new IndexWriter(directory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED);
            foreach (var userTask in userTasks)
            {
                indexWriter.AddDocument(GetDocument(userTask));
            }
            indexWriter.Optimize();
            indexWriter.Dispose();
        }

        public List<int> GetSearchByField(string searchString, string field)
        {
            var directory = FSDirectory.Open(new DirectoryInfo(path));
            var reader = IndexReader.Open(directory, true);
            var searcher = new IndexSearcher(reader);
            Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            //        var parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "Title", analyzer);
            var query = new FuzzyQuery(new Term(field, searchString), 0.45f);
            var collector = TopScoreDocCollector.Create(100, true);
            searcher.Search(query, collector);
            var hits = collector.TopDocs().ScoreDocs;
            var searchResult = new List<int>();
            foreach (var scoreDoc in hits)
            {
                //Get the document that represents the search result.
                var document = searcher.Doc(scoreDoc.Doc);

                int elementId = int.Parse(document.Get("Id"));

                //The same document can be returned multiple times within the4 search results.
                if (!searchResult.Contains(elementId))
                {
                    searchResult.Add(elementId);
                }
            }
            //Now that we have the product Ids representing our search results, retrieve the products from the database.
            reader.Dispose();
            searcher.Dispose();
            analyzer.Close();
            return searchResult;
        }

        //public List<int> SearchResult(string searchString)
        //{

        //    var searchResult = new List<int>();
        //    var search = new Dictionary<string, List<int>>();
        //    searchResult = GetSearchByField(searchString, "User", searchResult);
        //    search.Add("User", searchResult);
        //    searchResult = GetSearchByField(searchString, "Title", searchResult);
        //    searchResult = GetSearchByField(searchString, "Content", searchResult);
        //    searchResult = GetSearchByField(searchString, "Comments", searchResult);
        //    return searchResult;
        //}

        public Dictionary<string, List<int>> SearchResult(string searchString)
        {
            var searchResult = new Dictionary<string, List<int>>
            {
                {"User", GetSearchByField(searchString, "User")},
                {"Title", GetSearchByField(searchString, "Title")},
                {"Content", GetSearchByField(searchString, "Content")},
                {"Comments", GetSearchByField(searchString, "Comments")}
            };
            return searchResult;
        }


    }
}