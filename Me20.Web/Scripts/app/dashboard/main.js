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
                    this.Tags.push({
                        TagName: tag.tagName
                    });
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
                    this.Content.push({
                        Url: content.uri,
                        Tags: content.tags
                        });
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
                        var tags = response.body.item;
                        console.log(tags);
                    },
                    response => {
                        //TODO: Alert window
                        console.log(response);
                    }
                );
        }
    },
    created: function () {
        this.loadTags();
    }

});