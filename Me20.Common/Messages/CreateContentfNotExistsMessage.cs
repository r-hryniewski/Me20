
namespace Me20.Common.Messages
{
    public class CreateContentIfNotExistsMessage
    {
        public string Url { get; private set; }

        public CreateContentIfNotExistsMessage(string url)
        {
            Url = url;
        }
    }
}
