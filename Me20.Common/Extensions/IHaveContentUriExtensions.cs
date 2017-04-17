using Me20.Common.Interfaces;

namespace Me20.Common.Extensions
{
    public static class IHaveContentUriExtensions
    {
        public static string SchemalessUriAsBase64(this IHaveContentUri item) => item?.Uri?.ToSchemalessUriAsBase64();
    }
}
