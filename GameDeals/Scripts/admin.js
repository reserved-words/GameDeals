adminViewModel = {
    feedsCollection: ko.observableArray()
};

adminViewModel.onLoaded = function (data) {
    var self = this;
    self.feedsCollection.removeAll();
    $(data).each(function (index, element) {
        self.feedsCollection.push({
            id: element.Id,
            title: element.Title,
            url: element.Url,
            feedUrl: element.FeedUrl,
            logoUrl: "Content/Images/" + element.LogoFileName,
        });
    });
}