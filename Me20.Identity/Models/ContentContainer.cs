using Me20.Common.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Me20.Identity.Models
{
    internal class ContentContainer : IReadOnlyDictionary<Uri, UsersContent>, IEnumerable<UsersContent>, IEnumerable 
    {
        private Dictionary<string, UsersContent> items;

        internal ContentContainer()
        {
            items = new Dictionary<string, UsersContent>(StringComparer.OrdinalIgnoreCase);
        }

        public UsersContent this[Uri key] => items[key.ToSchemalessUriAsMD5()];

        public IEnumerable<Uri> Keys => items.Values.Select(c => c.Uri);

        public IEnumerable<UsersContent> Values => items.Values;

        public int Count => items.Count;

        public bool IsReadOnly => false;

        public bool ContainsKey(Uri key) => items.ContainsKey(key.ToSchemalessUriAsMD5());

        public bool TryGetValue(Uri key, out UsersContent value) => items.TryGetValue(key.ToSchemalessUriAsMD5(), out value);

        public IEnumerator<KeyValuePair<Uri, UsersContent>> GetEnumerator() => items.Values.Select(c => new KeyValuePair<Uri, UsersContent>(c.Uri, c)).GetEnumerator();

        public void Add(UsersContent item) => items.Add(item.Uri.ToSchemalessUriAsMD5(), item);

        public void Clear() => items.Clear();

        public bool Contains(UsersContent item) => items.ContainsKey(item.Uri.ToSchemalessUriAsMD5());

        public bool Remove(UsersContent item) => Remove(item.Uri);

        public bool Remove(Uri uri) => items.Remove(uri.ToSchemalessUriAsMD5());

        IEnumerator<UsersContent> IEnumerable<UsersContent>.GetEnumerator() => items.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => (this as IEnumerable<UsersContent>)?.GetEnumerator();
    }
}
