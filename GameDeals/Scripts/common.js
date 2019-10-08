﻿URL = {
    base: window.location.href,
    getAPIUrl: function (action) {
        return "http://localhost:50284/api/" + action;
    },
    getViewUrl: function (action) {
        return this.base + "/Home/" + action;
    },
    getImageUrl: function (filename) {
        return this.base + "/Content/Images/" + filename;
    }
};

URL.categories = function () {
    return this.getAPIUrl("Categories/Get");
}

URL.posts = function (categoryId, offset) {
    return this.getAPIUrl("Posts/Get/" + categoryId + "/10/" + offset);
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