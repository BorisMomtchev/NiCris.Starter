// Task Dashboard module
var Notifications = {};

// *** Initialize
Notifications.init = function () {
    console.log("Notifications.init");

    // ** Initialize the Save button.
    $("#btnPrimary").click(Notifications.info);
    $("#btnSuccess").click(Notifications.success);
    $("#btnWarning").click(Notifications.warning);
    $("#btnDanger").click(Notifications.error);

    $("#btnInverse").click(Notifications.inverse);
}


Notifications.info = function () {
    newAlert('alert alert-info', 'Some Info!');
}
Notifications.success = function () {
    newAlert('alert alert-success', 'Some Success!');
}
Notifications.warning = function () {
    newAlert('alert alert-block', 'Some Warning!');
}
Notifications.error = function () {
    newAlert('alert alert-error', 'Some Error!');
}

function newAlert(type, message) {
    $("#alert-area").append($("<div class='alert-message " + type + " fade in' data-alert><p> " + message + " </p></div>"));
    $(".alert-message").delay(2000).fadeOut("slow", function () { $(this).remove(); });
}



Notifications.inverse = function () {
    // newNotification('.top-right', 'danger', 'Some Cool Danger!');
    // newNotification('.bottom-right', 'info', 'Some Cool Info!');
    // newNotification('.top-right', 'bangTidy', 'Some Cool bangTidy!');
    newNotification('.top-right', 'blackgloss', 'Some Cool blackgloss!');
}

function newNotification(location, type, message) {
    $(location).notify({
        type: type,
        message: { text: message }
    }).show();
}





// Original Code
function newAlertOriginalCode(type, message) {
    $("#alert-area").append($("<div class='alert-message " + type + " fade in' data-alert><p> " + message + " </p></div>"));
    $(".alert-message").delay(2000).fadeOut("slow", function () { $(this).remove(); });
}

// http://stackoverflow.com/questions/7676356/can-twitter-bootstrap-alerts-fade-in-as-well-as-out
// http://twitter.github.com/bootstrap/base-css.html#buttons
// http://twitter.github.com/bootstrap/javascript.html#alerts

// http://nijikokun.github.com/bootstrap-notify/
// http://www.w3resource.com/twitter-bootstrap/alerts-and-errors-tutorial.php