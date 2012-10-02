$(function () {
    var messenger = $.connection.messenger  // generate the client-side hub proxy { Initialized to Exposed Hub }

    function init() {
        messenger.addToGroup("sourcing");
        return messenger.getAllMessages().done(function (message) {
            // Process Message Indivudally if Necessary
            
        });
    }

    messenger.begin = function () {
        $.jGrowl.defaults.animateClose = { width: 'hide' };
        $.jGrowl("Messenging System Started", { life: 500 });
    };

    messenger.add = function (message) {
        $.jGrowl(message.Content, { header: message.Title, sticky: true });
    };


    // Start the Connection
    $.connection.hub.start(function () {
        init().done(function () {
            messenger.begin();

        });
    });

});