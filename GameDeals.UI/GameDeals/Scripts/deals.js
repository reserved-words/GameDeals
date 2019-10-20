dealsViewModel = {
    postsCollection: ko.observableArray(),
    categoryId: ko.observable(),
    totalPosts: ko.observable()
};

dealsViewModel.onLoaded = function (data) {
    var self = this;
    self.postsCollection.removeAll();
    updatePosts(data);
}

dealsViewModel.getMorePosts = function () {
    var offset = dealsViewModel.postsCollection().length;
    var url = URL.posts(dealsViewModel.categoryId(), offset);
    ajax(url, function (data) {
        updatePosts(data);
    });
}

var updatePosts = function(data) {
    dealsViewModel.totalPosts(data.totalResults);
    $(data.results).each(function (index, element) {
        addPost(element);
    });
}

var addPost = function(element)
{
    dealsViewModel.postsCollection.push({
        title: element.title,
        url: element.url,
        publishedAt: element.publishedAt,
        isNew: element.isNew,
        summary: element.summary,
        feedTitle: element.feed.title,
        feedUrl: element.feed.url,
        feedLogoUrl: URL.image(element.feed.logoFileName),
        showSummary: ko.observable(false),
        hideSummary: ko.observable(true),
        showHideSummary: function () {
            if (this.showSummary()) {
                this.showSummary(false);
                this.hideSummary(true);
            } else {
                this.showSummary(true);
                this.hideSummary(false);
            }
        }
    });
}