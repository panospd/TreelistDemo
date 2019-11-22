namespace DXWebApplication2.Models
{
    public class PostLookup
    {
        public PostLookup(int postId, string postName)
        {
            PostId = postId;
            PostName = postName;
        }

        public int PostId { get; }
        public string PostName { get; }
    }
}