import Vue from "vue";
var VueResource = require("vue-resource");

Vue.use(VueResource);

var dashboard = new Vue({
    el: "#dashboard",
    data: {
        Content: [],
        Tags: []
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
        }
    }
});