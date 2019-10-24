URL = {
    base: window.location.href,
    getAPIUrl: function (action) {
        var baseUrl = $("#api-base-url").text();
        return baseUrl + action;
    },
    getViewUrl: function (action) {
        return this.base + "/" + action;
    },
    getImageUrl: function (filename) {
        return this.base + "/Content/Images/" + filename;
    }
};

URL.categories = function () {
    return this.getAPIUrl("Categories");
}

URL.posts = function (categoryId, offset) {
    return this.getAPIUrl("Posts/" + categoryId + "/10/" + offset);
}

URL.feeds = function () {
    return this.getAPIUrl("Feeds");
}

URL.adminView = function () {
    return this.getViewUrl("Admin");
}

URL.dealsView = function () {
    return this.getViewUrl("Deals");
}

URL.image = function (filename) {
    return this.getImageUrl(filename);
}