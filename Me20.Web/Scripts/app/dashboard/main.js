var Vue = require("vue");
var VueResource = require("vue-resource");

Vue.use(VueResource);

var dashboard = new Vue({
    el: "#dashboard",
    data: {
        CurrentUserName: AppData.currentUserName,
        HasMoreContent: true,
        Content: [],
        Tags: []
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
                    this.Tags.push(this.newTag(tag.tagName, true));
                },
                response => {
                    //TODO: Alert window
                    console.log(response);
                }
                );
        },
        addContent: function (event) {
            this.$http.post("/api/content/", { Url: event.srcElement.value, Tags: [] })
                .then(
                response => {
                    var responseItem = response.body.item;
                    var content = this.newContent(responseItem.uri, responseItem.tags.map(t => this.newTag(t, true)), this.$http);
                    this.Content.push(content);
                    content.GetDetails();
                },
                response => {
                    //TODO: Alert window
                    console.log(response);
                }
                );
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
                }
                );
        },
        loadTags: function () {
            this.$http.get("/api/tags/")
                .then(
                response => {
                    var tags = response.body.item.map(x => this.newTag(x.tagName, true));
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
                    var contents = response.body.item.map(c => this.newContent(c.uri, c.tags.map(t => this.newTag(t, true)), this.$http));
                    this.Content.push.apply(this.Content, contents);
                    this.Content.forEach(c => c.GetDetails());
                },
                response => {
                    //TODO: Alert window
                    console.log(response);
                }
                );
        },
        newTag: function (tagName, taggedByUser) {
            return {
                TagName: tagName,
                TaggedByUser: false
            };
        },
        newContent: function (url, tags, http) {
            return {
                $http: http,
                Url: url,
                Tags: tags,
                Title: url,
                Rating: 0,
                AverageRating: 0,
                DetailsLoaded: false,
                GetDetails: function () {
                    this.$http.get("/api/content/details/?uri=" + this.Url)
                        .then(
                            response => {
                                if (response.body.item[0]) {
                                    console.log("Content details fetched");
                                    console.log(response.body.item[0]);
                                    var contentDetails = response.body.item[0];
                                    this.Tags = contentDetails.tags.map(t => {
                                        return {
                                            TagName: t.tagName,
                                            TaggedByUser: t.taggedByUser
                                        };
                                    });
                                    //TODO: Title
                                    this.Rating = contentDetails.rating;
                                    this.AverageRating = contentDetails.averageRating;
                                    this.DetailsLoaded = true;
                                    this.SortTags();
                                }
                                else {
                                    console.log("No details for content '" + this.Url + "'");
                                }
                            },
                            response => {
                                console.log("Getting details for content '" + this.Url + "' failed.");
                                console.log(response);
                            }
                        );
                },
                AddTag: function () {
                    var tagName = prompt("Tag name");
                    if (tagName) {
                        var newTag = {
                            TagName: tagName,
                            TaggedByUser: true
                        };
                        this.$http.post("/api/content/tag", { Url: this.Url, Tags: [newTag] })
                            .then(
                                response => {
                                    var tags = response.body.item.tags.map(t => {
                                        return {
                                            TagName: t.tagName,
                                            TaggedByUser: t.taggedByUser
                                        };
                                    });
                                    if (tags)
                                    {
                                        for (var i = 0; i < tags.length; i++) {
                                            this.Tags.push(tags[i]);
                                        }
                                    }
                                    this.SortTags();
                                },
                                response => {
                                    console.log(response);
                                }
                            );
                    }
                },
                SortTags: function () {
                    this.Tags.sort(function (x, y) { return x.TaggedByUser === y.TaggedByUser ? 0 : x.TaggedByUser ? -1 : 1; });
                }
            };
        }
    },
    created: function () {
        this.loadTags();
        this.loadContent();
    }
});