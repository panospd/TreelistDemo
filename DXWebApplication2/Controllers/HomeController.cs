using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DXWebApplication2.Models;

namespace DXWebApplication2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(EditablePostProvider.Posts());
        }

        public ActionResult InlineEditingWithTemplatePartial()
        {
            var posts = NewsGroupsProvider.GetEditablePosts();

            return PartialView("../Partial/InlineEditingWithTemplatePartial", posts);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult InlineEditingWithTemplateAddNewPostPartial(EditablePost post)
        {
            if (ModelState.IsValid)
                NewsGroupsProvider.InsertPost(post);
            else
                ViewData["EditNodeError"] = "Please, correct all errors.";
            return InlineEditingWithTemplatePartial();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult InlineEditingWithTemplateUpdatePostPartial(EditablePost post)
        {
            if (ModelState.IsValid)
            {
                NewsGroupsProvider.UpdatePost(post);
                if(post.ParentID != null)
                    NewsGroupsProvider.MovePost(post.PostID, post.ParentID);
            }
            else
                ViewData["EditNodeError"] = "Please, correct all errors.";
            return InlineEditingWithTemplatePartial();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult InlineEditingWithTemplateMovePostPartial(int postID, int? parentID, bool hierarchy)
        {
            if(hierarchy)
                NewsGroupsProvider.MovePost(postID, parentID);
            else
                NewsGroupsProvider.ReOrderPost(postID, parentID);

            return InlineEditingWithTemplatePartial();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult InlineEditingWithTemplateDeletePostPartial(int postID)
        {
             NewsGroupsProvider.DeletePost(postID);
            return InlineEditingWithTemplatePartial();
        }
    }
}