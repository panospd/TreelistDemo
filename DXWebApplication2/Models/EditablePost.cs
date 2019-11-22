using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DXWebApplication2.Models
{
    public class EditablePost
    {
        private int? _parentId;
        bool? hasAttachment;
        bool? isNew;

        public int PostID { get; set; }

        
        [Display(Name = "Parent")]
        public int? ParentID
        {
            get => _parentId;
            set => _parentId = value !=  null && value != default(int) ? value : null; 
        }

        [Required(ErrorMessage = "Subject is required")]
        [StringLength(50, ErrorMessage = "Must be under 50 characters")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "From is required")]
        [StringLength(50, ErrorMessage = "Must be under 50 characters")]
        public string From { get; set; }

        public string Text { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "Date is required")]
        public DateTime PostDate { get; set; }

        public bool? IsNew
        {
            get { return isNew; }
            set { isNew = value == null ? false : value; }
        }

        public bool? HasAttachment
        {
            get { return hasAttachment; }
            set { hasAttachment = value == null ? false : value; }
        }

        public List<PostLookup> PostLookups { get; } = new List<PostLookup>();
    }
}