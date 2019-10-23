var config = "";
var mgr = null;

function authorize(config){
    var authConfig = {
        authority: config.authUrl,
        client_id: config.authClientId,
        redirect_uri: config.authCallback,
        response_type: config.authResponseType,
        scope: config.authScope,
        post_logout_redirect_uri: config.homeUrl,
    };

    mgr = new Oidc.UserManager(authConfig);

    mgr.getUser().then(function (user) {
        if (user) {
            console.log("User logged in");
            loadCategories(true);
        }
        else {
            console.log("User not logged in");
            ko.applyBindings(mainViewModel);
            mainViewModel.loading(false);
            loadContent(null, config.loginUrl, null);
        }
    });
}

function login() {
    mgr.signinRedirect();
}

function callback() {
    new Oidc.UserManager({ response_mode: "query" }).signinRedirectCallback().then(function () {
        window.location = config.homeUrl;
    }).catch(function (e) {
        console.error(e);
    });
}

function logout() {
    mgr.signoutRedirect();
}

$(function ()
{
    $("body").on("click", "#login", login);

    var rawFile = new XMLHttpRequest();
    rawFile.open("GET", "appsettings.json", false);
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