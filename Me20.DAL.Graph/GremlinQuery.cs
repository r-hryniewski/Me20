using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.DAL.Graph
{
    //TODO: Abstract some things, validate them, expand it into bigger object model
    //Case and method names convention matching gremlin queries as close as possible
    public class GremlinQuery
    {
        StringBuilder sb;

        private GremlinQuery(string startingInput = "")
        {
            sb = new StringBuilder(startingInput);
        }

        public static GremlinQuery g => new GremlinQuery("g");

        public static GremlinQuery V() => new GremlinQuery("V()");

        public static GremlinQuery V(string vertexId) => new GremlinQuery($"V('{vertexId}')");

        public static GremlinQuery gV() => new GremlinQuery("g.V()");

        public static GremlinQuery gV(string vertexId) => new GremlinQuery($"g.V('{vertexId}')");

        public GremlinQuery hasLabel(string label) => this.Append(".hasLabel('").Append(label).Append("')");

        public GremlinQuery has(string propertyName, string propertyValue) => this.Append(".has('").Append(propertyName).Append("', '").Append(propertyValue).Append("')");

        public GremlinQuery has(string propertyName, int propertyValue) => this.Append(".has('").Append(propertyName).Append("', ").Append(propertyValue.ToString()).Append(")");

        public GremlinQuery has(string propertyName, bool propertyValue) => this.Append(".has('").Append(propertyName).Append("', ").Append(propertyValue.ToString().ToLower()).Append(")");

        public GremlinQuery addV(string label) => this.Append(".addV('").Append(label).Append("')");

        public GremlinQuery addV(string label, string id) => this.addV(label).property("id", id);

        public GremlinQuery addE(string label) => this.Append(".addE('").Append(label).Append("')");

        public GremlinQuery to(string vertexQuery) => this.Append(".to(").Append(vertexQuery).Append(")");

        public GremlinQuery to(GremlinQuery vertexQuery) => this.Append(".to(").Append(vertexQuery).Append(")");

        public GremlinQuery property(string propertyName, string propertyValue) => this.Append(".property('").Append(propertyName).Append("', '").Append(propertyValue).Append("')");

        public GremlinQuery property(string propertyName, int propertyValue) => this.Append(".property('").Append(propertyName).Append("', ").Append(propertyValue.ToString()).Append(")");

        public GremlinQuery property(string propertyName, long propertyValue) => this.Append(".property('").Append(propertyName).Append("', ").Append(propertyValue.ToString()).Append(")");

        public GremlinQuery property(string propertyName, bool propertyValue) => this.Append(".property('").Append(propertyName).Append("', ").Append(propertyValue.ToString().ToLower()).Append(")");

        public GremlinQuery drop() => this.Append(".drop()");

        public override string ToString() => sb.ToString();

        private GremlinQuery Append(string query)
        {
            sb.Append(query);
            return this;
        }

        public static implicit operator string(GremlinQuery query) => query.sb.ToString();
    }
}
