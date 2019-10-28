loginViewModel = {
    loginClick: function (data, event) {
        login();
    }
};

loginViewModel.onLoaded = function (data) {
}

var config = "";
var mgr = null;

function authorize(config) {

    var authConfig = {
        authority: config.authUrl,
        client_id: config.authClientId,
        redirect_uri: window.applicationBaseUrl + "Callback",
        response_type: config.authResponseType,
        scope: config.authScope,
        post_logout_redirect_uri: window.applicationBaseUrl,
    };

    mgr = new Oidc.UserManager(authConfig);

    mgr.getUser().then(function (user) {
        ko.applyBindings(mainViewModel);
        if (user) {
            mainViewModel.loggedIn(true);
            loadCategories(true);
        }
        else {
            mainViewModel.loggedIn(false);
            mainViewModel.loading(false);
            loadContent(null, window.applicationBaseUrl + "Login", loginViewModel);
        }
    });
}

function login() {
    mgr.signinRedirect();
}

function callback() {
    new Oidc.UserManager({ response_mode: "query" }).signinRedirectCallback().then(function () {
        window.location = window.applicationBaseUrl;
    }).catch(function (e) {
        console.error(e);
    });
}

function logout() {
    mgr.signoutRedirect();

    mgr.getUser().then(function (user) {
        if (!user) {
        }
        else {
            mainViewModel.loggedIn(false);
        }
    });
}

$(function ()
{
    var rawFile = new XMLHttpRequest();
    rawFile.open("GET", window.applicationBaseUrl + "appsettings.json", false);
    rawFile.onreadystatechange = function () {
        if (rawFile.readyState === 4) {
            if (rawFile.status === 200 || rawFile.status == 0) {
                config = JSON.parse(rawFile.responseText);
                authorize(config);
            }
        }
    }
    rawFile.send(null);
});