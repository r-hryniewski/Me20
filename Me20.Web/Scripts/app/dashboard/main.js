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
            this.$http.post('/api/tags/', { TagName: event.srcElement.value })
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
            this.$http.post('/api/content/', { Url: event.srcElement.value, Tags: [] })
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
        loadTags: function () {
            this.$http.get('/api/tags/')
                .then(
                    response => {
                        var tags = response.body.item.map(t => this.newTag(t.tagName));
                        this.Tags.push.apply(this.Tags, tags);
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
                Tags: tags
            };
        }
    },
    created: function () {
        this.loadTags();
    }

});