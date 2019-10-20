mainViewModel = {
    menuItems: ko.observableArray(),
    menuClick: function (data, event) {
        if (data.id === 999) {
            loadContent(URL.feeds(), URL.adminView(), adminViewModel);
        }
        else {
            loadCategories(false);
            dealsViewModel.categoryId(data.id);
            loadContent(URL.posts(data.id, 0), URL.dealsView(), dealsViewModel);
        }
    },
    loading: ko.observable(true)
};

mainViewModel.onLoaded = function (data) {
    var self = this;
    self.menuItems.removeAll();
    $(data).each(function (index, element) {
        self.menuItems.push({
            id: element.id,
            name: element.value,
            newPosts: element.newPosts
        });
    });
    self.menuItems.push({
        id: 999,
        name: "Admin",
        newPosts: 0
    });
}

var loadContent = function (apiCallUrl, viewUrl, viewModel) {
    if (apiCallUrl) {
        ajax(apiCallUrl, function (data) {
            viewModel.onLoaded(data);
            $("#content").load(viewUrl, function () {
                ko.cleanNode($('#content')[0]);
                ko.applyBindings(viewModel, $('#content')[0]);
                mainViewModel.loading(false);
            });
        });
    }
    else {
        $("#content").load(viewUrl, function () {
            mainViewModel.loading(false);
        });
    }
}

var ajax = function (url, onLoaded) {
    mainViewModel.loading(true);
    mgr.getUser().then(function (user) {
        var xhr = new XMLHttpRequest();
        xhr.open("GET", url);
        xhr.onload = function () {
            onLoaded(JSON.parse(xhr.responseText));
            mainViewModel.loading(false);
        }
        xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
        xhr.send();
    });
}

var loadCategories = function(selectDefault){
    ajax(URL.categories(), function (data) {
        mainViewModel.onLoaded(data);
        if (selectDefault) {
            ko.applyBindings(mainViewModel);
            mainViewModel.menuClick({ id: 2 }, null);
        }
    });
}

$(document).on('click', '.navbar-collapse.in', function (e) {
    $(this).collapse('hide');
});