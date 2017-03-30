//TODO: TypeScript?
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
                    this.Tags.push({
                        TagName: response.body.tagName
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
                    this.Content.push({
                        Url: response.body.url,
                        Tags: response.body.tags
                        });
                },
                response => {
                    //TODO: Alert window
                    console.log(response);
                });
        },
    }
});