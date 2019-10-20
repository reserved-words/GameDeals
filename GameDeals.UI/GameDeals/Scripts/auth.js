//function log() {
//    document.getElementById('results').innerText = '';

//    Array.prototype.forEach.call(arguments, function (msg) {
//        if (msg instanceof Error) {
//            msg = "Error: " + msg.message;
//        }
//        else if (typeof msg !== 'string') {
//            msg = JSON.stringify(msg, null, 2);
//        }
//        document.getElementById('results').innerHTML += msg + '\r\n';
//    });
//}

$("body").on("click", "#login", login);

//document.getElementById("login").addEventListener("click", login, false);
//document.getElementById("api").addEventListener("click", api, false);
//document.getElementById("logout").addEventListener("click", logout, false);

var config = {
    authority: "http://localhost:5000",
    client_id: "GameDeals",
    redirect_uri: "http://localhost:50606/Callback",
    response_type: "code",
    scope: "GameDealsApi",
    post_logout_redirect_uri: "http://localhost:50606/",
};

var mgr = new Oidc.UserManager(config);

mgr.getUser().then(function (user) {
    if (user) {
        console.log("User logged in");
        loadCategories(true);
    }
    else {
        console.log("User not logged in");
        ko.applyBindings(mainViewModel);
        mainViewModel.loading(false);
        loadContent(null, "http://localhost:50606/Login", null);
    }
});

function login() {
    mgr.signinRedirect();
}

function callback() {
    new Oidc.UserManager({ response_mode: "query" }).signinRedirectCallback().then(function () {
        window.location = "http://localhost:50606/";
    }).catch(function (e) {
        console.error(e);
    });
}

//function api() {
//    mgr.getUser().then(function (user) {
//        var url = "http://localhost:5001/identity";

//        var xhr = new XMLHttpRequest();
//        xhr.open("GET", url);
//        xhr.onload = function () {
//            log(xhr.status, JSON.parse(xhr.responseText));
//        }
//        xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
//        xhr.send();
//    });
//}

function logout() {
    mgr.signoutRedirect();
}