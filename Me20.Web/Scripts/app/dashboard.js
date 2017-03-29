var dashboard = new Vue({
    el: "#dashboard",
    data: {
        Content: [],
        Tags: []
    },
    methods: {
        subscribeTag: function (event) {
            //TODO: Post to api
            this.Tags.push(event.srcElement.value);
            this.$http.post('/api/tags/', { TagName: event.srcElement.value })
                .then(
                response => {
                    console.log(response);
                },
                response => {
                    console.log(response);
                });
        },
        addContent: function (event) {
            //TODO: Post to api
            this.Content.push(event.srcElement.value);
        },

    }
});