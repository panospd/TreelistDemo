using System;
using System.Collections.Generic;
using DXWebApplication2.Models;

namespace DXWebApplication2
{
    public static class EditablePostProvider
    {
        public static IEnumerable<EditablePost> Posts()
        {
            return new List<EditablePost>
            {
                new EditablePost
                {
                    From = "Panos",
                    HasAttachment = true,
                    IsNew = true,
                    ParentID = null,
                    PostDate = new DateTime(2019,11,1),
                    PostID = 1,
                    Subject = "Football",
                    Text = "Text example"
                },
                new EditablePost
                {
                    From = "Amalia",
                    HasAttachment = false,
                    IsNew = true,
                    ParentID = null,
                    PostDate = new DateTime(2019,11,1),
                    PostID = 2,
                    Subject = "Football",
                    Text = "Text example",
                },
                new EditablePost
                {
                    From = "Kalliopi",
                    HasAttachment = false,
                    IsNew = true,
                    ParentID = null,
                    PostDate = new DateTime(2019,11,1),
                    PostID = 3,
                    Subject = "Football",
                    Text = "Text example",
                },
                new EditablePost
                {
                    From = "Dionisis",
                    HasAttachment = false,
                    IsNew = true,
                    ParentID = null,
                    PostDate = new DateTime(2019,11,1),
                    PostID = 4,
                    Subject = "Tennis",
                    Text = "Text example",
                },
                new EditablePost
                {
                    From = "Anastasia",
                    HasAttachment = false,
                    IsNew = true,
                    ParentID = null,
                    PostDate = new DateTime(2019,11,1),
                    PostID = 5,
                    Subject = "Coding",
                    Text = "Text example",
                },
                new EditablePost
                {
                    From = "Chris",
                    HasAttachment = false,
                    IsNew = true,
                    ParentID = null,
                    PostDate = new DateTime(2019,11,1),
                    PostID = 6,
                    Subject = "Drinks",
                    Text = "Text example",
                },
                new EditablePost
                {
                    From = "Matt",
                    HasAttachment = false,
                    IsNew = true,
                    ParentID = null,
                    PostDate = new DateTime(2019,11,1),
                    PostID = 7,
                    Subject = "Football",
                    Text = "Text example",
                }
            };
        }
    }
}