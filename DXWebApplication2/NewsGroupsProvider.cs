using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DXWebApplication2.Models;

namespace DXWebApplication2
{
    public static class NewsGroupsProvider
    {
        const string NewsGroupsDataContextKey = "DXNewsGroupsDataContext";

        public static IEnumerable GetPosts()
        {
            return EditablePostProvider.Posts();
        }
        public static EditablePost GetPostByID(int id)
        {
            return EditablePostProvider.Posts().SingleOrDefault(p => p.PostID == id);
        }
        public static IEnumerable GetChildPosts(int id)
        {
            return EditablePostProvider.Posts().Where(p => p.ParentID == id);
        }

        public static List<EditablePost> GetEditablePosts(bool actualizeDates = false)
        {
            List<EditablePost> posts = (List<EditablePost>)HttpContext.Current.Session["Posts"];
            if (posts == null)
            {
                posts = EditablePostProvider.Posts().ToList();

                var postLookups = posts.Select(p => new PostLookup(p.PostID, p.From));

                foreach (EditablePost post in posts)
                {
                    IEnumerable<PostLookup> filteredPostLookups = postLookups.Where(l => l.PostId != post.PostID);
                    post.PostLookups.AddRange(filteredPostLookups);
                }

                if (actualizeDates)
                    ActualizeDates(posts);
                HttpContext.Current.Session["Posts"] = posts;
            }
            return posts;
        }
        public static void ActualizeDates(List<EditablePost> posts)
        {
            var maxDate = posts.Max(p => p.PostDate);
            var diff = DateTime.Now - maxDate;
            foreach (var post in posts)
                post.PostDate = post.PostDate.Add(diff);
        }
        public static List<EditablePost> GetEditableChildPosts(int parentID)
        {
            return (from post in GetEditablePosts() where post.ParentID == parentID select post).ToList();
        }
        public static EditablePost GetEditablePost(int postID)
        {
            return (from post in GetEditablePosts() where post.PostID == postID select post).FirstOrDefault();
        }
        public static int GetNewEditablePostID()
        {
            IEnumerable<EditablePost> editablePosts = GetEditablePosts();
            return editablePosts.Select(p => p.PostID).Max() + 1;
        }

        public static void InsertPost(EditablePost newPost)
        {
            newPost.PostID = GetNewEditablePostID();
            GetEditablePosts().Add(newPost);
        }
        public static void UpdatePost(EditablePost post)
        {
            EditablePost postToUpdate = GetEditablePost(post.PostID);
            if (postToUpdate != null)
            {
                postToUpdate.Subject = post.Subject;
                postToUpdate.From = post.From;
                postToUpdate.PostDate = post.PostDate;
                postToUpdate.Text = post.Text;
                postToUpdate.IsNew = post.IsNew;
                postToUpdate.HasAttachment = post.HasAttachment;
                postToUpdate.ParentID = post.ParentID;
            }
        }
        public static void DeletePost(int id)
        {
            EditablePost postToDelete = GetEditablePost(id);
            if (postToDelete != null)
                DeleteChildPosts(postToDelete);
        }
        public static void MovePost(int postID, int? newParentPostID)
        {
            int newParentID = Convert.ToInt32(newParentPostID);
            if (GetEditablePost(postID).ParentID == newParentID || IsParentPost(postID, newParentID))
                return;
            GetEditablePost(postID).ParentID = newParentID;
        }

        public static void ReOrderPost(int postID, int? newPostId)
        {
            if(newPostId == null)
                return;

            int targetPostId = Convert.ToInt32(newPostId);
             if(targetPostId == postID)
                 return;

             List<EditablePost> list = GetEditablePosts();
             var postToMove = GetEditablePost(postID);

             var currentIndex = list.IndexOf(postToMove);
             var targetIndex = list.FindIndex(e => e.PostID == newPostId);

            list.RemoveRange(currentIndex, 1);
            list.Insert(targetIndex, postToMove);
        }

        static void DeleteChildPosts(EditablePost post)
        {
            List<EditablePost> childPosts = GetEditableChildPosts(post.PostID);
            if (childPosts != null)
            {
                foreach (EditablePost childPost in childPosts)
                {
                    DeleteChildPosts(childPost);
                }
            }
            GetEditablePosts().Remove(post);
        }
        static bool IsParentPost(int parentID, int childID)
        {
            int? currentPostID = childID;
            do
            {
                if (currentPostID == parentID)
                    return true;
                var post = GetEditablePost(currentPostID.Value);
                currentPostID = post != null ? post.ParentID : null;
            } while (currentPostID.HasValue);
            return false;
        }
    }
}