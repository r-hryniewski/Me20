using Me20.Common.Interfaces;

namespace Me20.Content.Events.Contents
{
    public class ContentRatedByEvent : IHaveUserName
    {
        public string UserName { get; private set; }
        public byte Rating { get; private set; }

        public ContentRatedByEvent(string userName, byte rating)
        {
            UserName = userName;
            Rating = rating;
        }
    }
}
