var Vue = require("vue");
var VeeValidate = require("vee-validate");
var VueResource = require("vue-resource");

var veeValidateConfig = {
    errorBagName: 'errors', // change if property conflicts.
    fieldsBagName: 'fields',
    delay: 0,
    locale: 'en',
    dictionary: null,
    strict: true,
    enableAutoClasses: false,
    classNames: {
        touched: 'touched', // the control has been blurred
        untouched: 'untouched', // the control hasn't been blurred
        valid: 'valid', // model is valid
        invalid: 'invalid', // model is invalid
        pristine: 'pristine', // control has not been interacted with
        dirty: 'dirty' // control has been interacted with
    },
    events: 'input|blur'
};

Vue.use(VueResource);
Vue.use(VeeValidate, veeValidateConfig);

var dashboard = new Vue({
    el: "#dashboard",
    data: {
        CurrentUserName: AppData.currentUserName,
        HasMoreContent: true,
        Content: [],
        Tags: [],
        SuggestedContent: [],
        ToggledSuggestedContent: ""
    },
    computed: {
        PagesLoaded: function () {
            return Math.floor(this.Content.length / AppData.contentPageSize);
        }
    },
    methods: {
        subscribeTag: function (event) {
            var input = event.srcElement.value;
            if (input.length <= 25) {
                this.$http.post("/api/tags/", { TagName: event.srcElement.value })
                    .then(
                    response => {
                        var tag = response.body.item;
                        this.Tags.push(this.newTag(tag.tagName, true));
                        event.srcElement.value = "";
                    },
                    response => {
                        //TODO: Alert window
                        console.log(response);
                        event.srcElement.value = "";
                    });
            }
            else {
                alert("Tag name maximum length is 25 characters");
            }
        },
        addContent: function (event) {
            var addContentCommand =
                {
                    Url: event.srcElement.value,
                    Tags: []
                };

            this.$http.post("/api/content/", addContentCommand)
                .then(response => {
                    //TODO: Loader of some kind?
                    var responseItem = response.body.item;
                    var contentItem = this.newContent(addContentCommand.Url, addContentCommand.Tags.map(t => this.newTag(t, true)), this.$http);
                    this.Content.splice(0, 0, contentItem);
                    contentItem.GetDetails();
                }, response => {
                    //TODO: Alert window
                    console.log(response);
                });
            event.srcElement.value = "";
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
            if (this.HasMoreContent) {
                this.$http.get("/api/content?page=" + (this.PagesLoaded + 1))
                    .then(
                    response => {
                        var contents = response.body.item.map(c => this.newContent(c.uri, c.tags.map(t => this.newTag(t, true)), this.$http));
                        this.Content.push.apply(this.Content, contents);

                        if (contents.length < AppData.contentPageSize)
                            this.HasMoreContent = false;

                        this.Content.forEach(c => c.GetDetails());
                    },
                    response => {
                        console.log(response);
                    }
                    );
            }
        },
        removeContent: function (content) {
            if (confirm("Do you want to remove '" + content.Title + "' from your contents list?")) {
                this.$http.delete("/api/content", { body: content }).then(
                    response => {
                        var index = this.Content.indexOf(content);
                        this.Content.splice(index, 1);
                    },
                    response => {
                        console.log(response);
                    }
                );
            }
        },
        renameContent: function (content) {
            var newTitle = prompt("Enter new title for " + content.Title, content.Title);
            var newContent = { Url: content.Url, Title: newTitle };
            this.$http.put("/api/content", newContent).then(
                response => {
                    content.Title = response.body.item.title;
                },
                response => {
                    console.log(response);
                }
            );
        },
        toggleSuggestedContent: function (tagName) {
            if (this.ToggledSuggestedContent === tagName) {
                this.ToggledSuggestedContent = "";
                return;
            }

            if (!this.SuggestedContent[tagName]) {
                this.SuggestedContent[tagName] = this.newSuggestedContentContainer(tagName);
            }

            //TODO:
            return;
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
                            if (response.body.item) {
                                console.log("Content details fetched");
                                console.log(response.body.item);
                                var contentDetails = response.body.item;
                                this.Tags = contentDetails.tags.map(t => {
                                    return {
                                        TagName: t.tagName,
                                        TaggedByUser: t.taggedByUser
                                    };
                                });
                                this.Title = contentDetails.title;
                                this.Rating = contentDetails.rating;
                                this.AverageRating = contentDetails.averageRating.toFixed(2);
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
                AddTag: function (tagName) {
                    if (!tagName)
                        tagName = prompt("Tag name");
                    if (tagName) {
                        if (tagName.length <= 25)
                        {
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
                                    if (tags) {
                                        var tagNames = this.Tags.map(t => t.TagName.toLowerCase());
                                        for (var i = 0; i < tags.length; i++) {
                                            var tagToAdd = tags[i];
                                            var index = tagNames.indexOf(tagToAdd.TagName.toLowerCase());

                                            if (index >= 0)
                                                this.Tags[index].TaggedByUser = true;
                                            else
                                                this.Tags.push(tagToAdd);

                                        }
                                    }
                                    this.SortTags();
                                },
                                response => {
                                    console.log(response);
                                }
                                );
                        }
                        else {
                            alert("Tag name maximum length is 25 characters");
                        }
                    }
                },
                SortTags: function () {
                    this.Tags.sort(function (x, y) { return x.TaggedByUser === y.TaggedByUser ? 0 : x.TaggedByUser ? -1 : 1; });
                }
            };
        },
        newSuggestedContentContainer: function (tagName) {
            return {
                TagName: tagName,
                CurrentItemIndex: 0,
                Contents: [],
                LoadMore: function (page, count, http) {
                    //TODO:
                    //???
                }
            };
        }
    },
    created: function () {
        this.loadTags();
        this.loadContent();
    }
});