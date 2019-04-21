﻿mainViewModel = {
    menuItems: ko.observableArray(),
    menuClick: function (data, event) {
        if (data.id === 999) {
            loadContent("/API/Feeds", "/Home/Admin", adminViewModel);
        }
        else {
            loadCategories(false);
            dealsViewModel.categoryId(data.id);
            loadContent("/API/Posts/Get/" + data.id + "/10/0", "/Home/Deals", dealsViewModel);
        }
    },
    loading: ko.observable(true)
};

mainViewModel.onLoaded = function (data) {
    var self = this;
    self.menuItems.removeAll();
    $(data).each(function (index, element) {
        self.menuItems.push({
            id: element.Id,
            name: element.Value,
            newPosts: element.NewPosts
        });
    });
    self.menuItems.push({
        id: 999,
        name: "Admin",
        newPosts: 0
    });
}

var loadContent = function (apiCallUrl, viewUrl, viewModel) {
    ajax(apiCallUrl, function (data) {
        viewModel.onLoaded(data);
        $("#content").load(viewUrl, function () {
            ko.cleanNode($('#content')[0]);
            ko.applyBindings(viewModel, $('#content')[0]);
            mainViewModel.loading(false);
        });
    });
}

var ajax = function (url, onLoaded) {
    mainViewModel.loading(true);
    $.ajax({
        type: "GET",
        url: url,
        success: function(data) {
            onLoaded(data);
            mainViewModel.loading(false);
        },
        error: function(ex) {
            alert("Error");
        }
    });
}

var loadCategories = function(selectDefault){
    ajax("/API/Categories/Get", function (data) {
        mainViewModel.onLoaded(data);
        if (selectDefault) {
            ko.applyBindings(mainViewModel);
            mainViewModel.menuClick({ id: 2 }, null);
        }
    });
}

$(function () {
    loadCategories(true);
});

$(document).on('click', '.navbar-collapse.in', function (e) {
    $(this).collapse('hide');
});