using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web.ASPxTreeList;
using DXWebApplication2.Models;

namespace DXWebApplication2.Helper
{
    public class TreeListInlineEditingDemoHelper
    {
        public static EditablePost GetEditedPost(TreeListEditFormTemplateContainer container)
        {
            var post =  new EditablePost
            {
                From = (string)DataBinder.Eval(container.DataItem, "From"),
                Subject = (string)DataBinder.Eval(container.DataItem, "Subject"),
                PostDate = (DateTime)(DataBinder.Eval(container.DataItem, "PostDate") ?? new DateTime()),
                Text = (string)DataBinder.Eval(container.DataItem, "Text"),
                HasAttachment = (bool?)DataBinder.Eval(container.DataItem, "HasAttachment"),
                IsNew = (bool?)DataBinder.Eval(container.DataItem, "IsNew"),
                ParentID = (int?)DataBinder.Eval(container.DataItem, "ParentID")
            };
            var postId = DataBinder.Eval(container.DataItem, "PostID");

            int postIdToExcludeFromParentList = postId != null
                ? (int)DataBinder.Eval(container.DataItem, "PostID")
                : -1;

            post.PostLookups.Add(new PostLookup(0, ""));

            post.PostLookups.AddRange(NewsGroupsProvider.GetEditablePosts()
                .Where(p => p.PostID != postIdToExcludeFromParentList)
                .Select(p => new PostLookup(p.PostID, p.From)));

            return post;
        }
    }
}