fucntion log() {
    document.getElementById('results').innerText = '';

    Array.prototype.forEach.call(arguments, function (msg) {
        if (msg instanceof Error) {
            msg = "Error: " + msg.message;
        }
        else if (typeof msg !== 'string') {
            msg = JSON.stringify(msg, null, 2);
        }
        document.getElementById('results').innerHTML += msg + '\r\n';
    });
}

document.getelementById("login").addEventListener("click", login, false);
document.getElementById("api").addEventListener("click", api, false);
document.getelementById("logout").addEventListener("click", logout, false);

// this manages the OpenId Connect protocol
var config = {
    authority: "https//localhost:5001",
    client_id: "js",
    redirect_url: "https://localhost:5003/callback.html",
    response_type: "code",
    scope: "openid profile api",
    post_logout_redirect_uri: "https://localhost:5003/index.html",
};
var mgr = new Oidc.UserManager(config);

// checks to see if the user is logged into the JS application
mgr.getUser().then(function (user) {
    if (user) {
        log("User is logged in", user.profile);
    }
    else {
        log("User not logged in");
    }
});

// implement login, logout, and api functions
function login() {
    mgr.signinRedirect();
}

function api() {
    mgr.getUser().then(function (user) {
        var url = "https://localhost:6001/identity";

        var xhr = new XMLHttpRequest();
        xhr.open("GET", url);
        xhr.onload = function () {
            log(xhr.status, JSON.parse(xhr.responseText));
        }
        xhr.setRequestHeader("Authorization", "Bearer" + user.access_token);
        xhr.send();
    });
}

function logout() {
    mgr.signoutRedirect();
}