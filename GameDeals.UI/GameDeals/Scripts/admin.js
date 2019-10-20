adminViewModel = {
    feedsCollection: ko.observableArray()
};

adminViewModel.onLoaded = function (data) {
    var self = this;
    self.feedsCollection.removeAll();
    $(data).each(function (index, element) {
        self.feedsCollection.push({
            id: element.id,
            title: element.title,
            url: element.url,
            feedUrl: element.feedUrl,
            logoUrl: "Content/Images/" + element.logoFileName,
        });
    });
}