var config = {
    authority: "http://localhost:5000",
    client_id: "GameDeals",
    redirect_uri: "http://localhost:50606/Callback",
    response_type: "code",
    scope: "openid profile GameDealsApi",
    post_logout_redirect_uri: "http://localhost:50606/",
};

var mgr = new Oidc.UserManager(config);

function callApi(url, load) {

    mgr.getUser().then(function (user) {
        if (!user) {
            mgr.signinRedirect();
            return;
        }

        var xhr = new XMLHttpRequest();
        xhr.open("GET", url);
        xhr.onload = function () {
            console.log(xhr.status);
            load(JSON.parse(xhr.responseText));
        }
        xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
        xhr.send();
    });
}

function logout() {
    mgr.signoutRedirect();
}