var Vue = require("vue");
var VueResource = require("vue-resource");

Vue.use(VueResource);

var dashboard = new Vue({
    el: "#dashboard",
    data: {
        CurrentUserName: AppData.currentUserName,
        HasMoreContent: true,
        Content: [],
        Tags: [],
    },
    computed: {
        PagesLoaded: function () {
            return this.Content.length / AppData.contentPageSize;
        }
    },
    methods: {
        subscribeTag: function (event) {
            this.$http.post("/api/tags/", { TagName: event.srcElement.value })
                .then(
                response => {
                    var tag = response.body.item;
                    this.Tags.push(this.newTag(tag.tagName));
                },
                response => {
                    //TODO: Alert window
                    console.log(response);
                });
        },
        addContent: function (event) {
            this.$http.post("/api/content/", { Url: event.srcElement.value, Tags: [] })
                .then(
                response => {
                    var content = response.body.item;
                    this.Content.push(this.newContent(content.uri, content.tags.map(this.newTag)));
                },
                response => {
                    //TODO: Alert window
                    console.log(response);
                });
        },
        rateContent: function (content, rating) {

            this.$http.post("/api/content/rate", { Url: content.Url, Rating: rating })
                .then(
                response => {
                    content.Rating = response.body.item.rating;
                },
                response => {
                    //TODO: Alert window
                    console.log(response);
                });
        },
        loadTags: function () {
            this.$http.get("/api/tags/")
                .then(
                    response => {
                        var tags = response.body.item.map(x => this.newTag(x.tagName));
                        this.Tags.push.apply(this.Tags, tags);
                    },
                    response => {
                        //TODO: Alert window
                        console.log(response);
                    }
                );
        },
        loadContent: function () {
            this.$http.get("/api/content/")
                .then(
                    response => {
                        var contents = response.body.item.map(t => this.newContent(t.uri, t.tags.map(this.newTag)));
                        this.Content.push.apply(this.Content, contents);
                    },
                    response => {
                        //TODO: Alert window
                        console.log(response);
                    }
                );
        },
        newTag: function (tagName) {
            return {
                TagName: tagName
            };
        },
        newContent: function (url, tags) {
            return {
                Url: url,
                Tags: tags,
                Title: url,
                Rating: 0
            };
        }
    },
    created: function () {
        this.loadTags();
        this.loadContent();
    }
});