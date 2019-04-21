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
    var url = "/api/Posts/Get/" + dealsViewModel.categoryId() + "/10/" + offset;
    ajax(url, function (data) {
        updatePosts(data);
    });
}

var updatePosts = function(data) {
    dealsViewModel.totalPosts(data.TotalResults);
    $(data.Results).each(function (index, element) {
        addPost(element);
    });
}

var addPost = function(element)
{
    dealsViewModel.postsCollection.push({
        title: element.Title,
        url: element.Url,
        publishedAt: element.PublishedAt,
        isNew: element.IsNew,
        summary: element.Summary,
        feedTitle: element.Feed.Title,
        feedUrl: element.Feed.Url,
        feedLogoUrl: "Content/Images/" + element.Feed.LogoFileName,
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