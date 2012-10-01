// Task Dashboard module
var TaskDashboard = {};

var _taskHub = null;

// *** Initialize
TaskDashboard.init = function (client) {

    // Initialize jTable
    $('#TasksTableContainer').jtable({
        title: 'Tasks List',
        actions: {
            listAction: _rootUrl + "Task/TaskList?clientName=" + client,
            deleteAction: _rootUrl + "Task/DeleteTask?clientName=" + client,
            updateAction: _rootUrl + "Task/UpdateTask?clientName=" + client,
            createAction: _rootUrl + "Task/CreateTask?clientName=" + client
        },

        rowUpdated: function (event, data) {
            /* handle */
        },

        fields: {
            Id: {
                title: 'Id',
                width: '10%',
                key: true,
                create: false,
                edit: false
            },
            Name: {
                title: 'Name',
                width: '20%'
            },
            CreatedBy: {
                title: 'Created By',
                width: '15%',
            },
            CreatedDate: {
                title: 'Created Date',
                width: '15%',
                type: 'date',
                displayFormat: 'yy-mm-dd'
            },
            Resolver: {
                title: 'Resolver',
                width: '15%'
            },
            ResolvedDate: {
                title: 'Resolved Date',
                width: '15%',
                type: 'date',
                displayFormat: 'yy-mm-dd',
                create: false,
                edit: false
            },
            Status: {
                title: 'Status',
                width: '10%',
                options: { '0': 'New', '1': 'Active', '2': 'Resolved' }
            }
        }
    });

    // Load the task list from server
    $('#TasksTableContainer').jtable('load');

    // Start our SignalR hub
    TaskDashboard.hubStart(client);
}

// A function to write events to the page
function writeEvent(eventLog, logClass) {
    var now = new Date();
    var nowStr = now.getHours() + ':' + now.getMinutes() + ':' + now.getSeconds();
    $('#EventsList').prepend('<li class="' + logClass + '"><b>' + nowStr + '</b>: ' + eventLog + '.</li>');
}

// Start our SignalR hub
TaskDashboard.hubStart = function (client) {

    // Create SignalR object to get communicate with server
    _taskHub = $.connection.taskHub;

    // Define a function to get 'record created' events
    _taskHub.RecCreated = function (clientName, record) {
        if (clientName != client) {
            $('#TasksTableContainer').jtable('addRecord', {
                record: record,
                clientOnly: true
            });
        }

        writeEvent(clientName + ' has <b>created</b> a new record with id = ' + record.Id, 'event-created');
    };

    // Define a function to get 'record updated' events
    _taskHub.RecUpdated = function (clientName, record) {
        if (clientName != client) {
            $('#TasksTableContainer').jtable('updateRecord', {
                record: record,
                clientOnly: true
            });
        } else {
            setTimeout(function() {
                $('#TasksTableContainer').jtable('reload', function() {
                    // refresh current client with any data generated on server
                });
            }, 5000);
        }

        writeEvent(clientName + ' has <b>updated</b> a new record with id = ' + record.Id, 'event-updated');
    };

    // Define a function to get 'record deleted' events
    _taskHub.RecDeleted = function (clientName, Id) {
        if (clientName != client) {
            $('#TasksTableContainer').jtable('deleteRecord', {
                key: Id,
                clientOnly: true
            });
        }

        writeEvent(clientName + ' has <b>removed</b> a record with id = ' + Id, 'event-deleted');
    };

    // Start the connection with server
    $.connection.hub.start();

        // Define a function to get 'chat messages'
    _taskHub.GetMessage = function (clientName, message) {
        writeEvent('<b>' + clientName + '</b> has sent a message: ' + message, 'event-message');
    };

    $('#Message').keydown(function (e) {
        if (e.which == 13) { //Enter
            e.preventDefault();
            _taskHub.sendMessage(client, $('#Message').val());
            $('#Message').val('');
        }
    });

}
