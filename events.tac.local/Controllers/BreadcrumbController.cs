using events.tac.local.Models;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace events.tac.local.Controllers
{
    public class BreadcrumbController : Controller
    {
        public IEnumerable<NavigationItem> CreateModel()
        {
            var currentItem = RenderingContext.Current.ContextItem;
            var homeItem = Sitecore.Context.Database.GetItem(Sitecore.Context.Site.StartPath);
            var breadcrumb = RenderingContext.Current.ContextItem.Axes.GetAncestors()
                .Where(i => i.Axes.IsDescendantOf(homeItem))
                .Concat(new Item[] { currentItem })
                .ToList();

            IEnumerable<NavigationItem> NavigationList = breadcrumb.Select(s => new NavigationItem
            {
                Title = s.DisplayName,
                URL = LinkManager.GetItemUrl(s),
                Active = (s.ID == currentItem.ID)
            });
            return NavigationList;
        }
        // GET: Breadcrumb
        public ActionResult Index()
        {
            return View(CreateModel());
        }
    }
}