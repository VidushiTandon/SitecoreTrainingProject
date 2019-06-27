using events.tac.local.Models;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace events.tac.local.Controllers
{
    public class EventsListController : Controller
    {
        // GET: EventList
        private const int PageSize = 4;
        public ActionResult Index(int page = 1)
        {
            var contextItem = RenderingContext.Current.ContextItem;
            var model = new EventsList();
            var databaseName = contextItem.Database.Name.ToLower();
            var indexName = string.Format("events_{0}_index", databaseName);
            var index = ContentSearchManager.GetIndex(indexName);
            using (var context = index.CreateSearchContext())
            {
                var results = context.GetQueryable<EventDetails>()
                    .Where(i => i.Paths.Contains(contextItem.ID)
                    && i.Language == contextItem.Language.Name)
                    .Page(page - 1, PageSize)
                    .GetResults();
                model.Events = results.Hits.Select(h => h.Document).ToList();
                model.TotalResultCount = results.TotalSearchResults;
                model.PageSize = PageSize;
            }

                return View(model);
        }
    }
}