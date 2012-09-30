// *** Main layout
var Layout = {}

Layout._height = null;

/// <summary>Initialize the layout view.</summary>
Layout.init = function () {
    // Initialize the main splitter.
    $("#mainSplitter").kendoSplitter({
        orientation: "vertical",
        panes: [
            { size: "36px", resizable: false },
        ]
    });

    // Initialize the content splitter.
    $("#contentSplitter").kendoSplitter({
        panes: [
            { contentUrl: _rootUrl + "Home/Navigation", size: "195px", collapsible: true }
        ]
    });

    // Status bar.
    $("#bottom-pane").kendoSplitter({
        orientation: "vertical",
        panes: [
            { collapsible: false, resizable: false }
        ]
    });

    // Hide th e scrollbar in the main splitter.
    $('#mainSplitter').children('div.k-pane').css('overflow-y', 'hidden');

    // Call the Layout.resize function every 100 miliseconds.
    setInterval("Layout.resize()", 100);
}

/// <summary>Check the size of the window, and if it has changed, then resize the splitters to fit the window.</summary>
Layout.resize = function () {
    var height = $(window).height();

    // Has the window height changed?
    if (height != Layout._height) {
        Layout._height = height;

        // Resize the main splitter.
        var mainSplitter = $('#mainSplitter');
        mainSplitter.height(height - 24);
        mainSplitter.resize();

        // Resize the content splitter.
        var contentSplitter = $('#contentSplitter');
        contentSplitter.height(height - 64);
        contentSplitter.resize();
    }
}

// *** Navigation section
Layout.Navigation = {}

Layout.Navigation.init = function () {
    $.ajax({
        url: _rootUrl + "Home/PanelData",
        dataType: 'json',
        success: function (data) {
            $("#navPanelBar").kendoPanelBar(
                {
                    dataSource: data,
                    expandMode: "multiple",     // "single",
                    select: Layout.Navigation.navPanelBar_onSelect
                }).data("kendoPanelBar");
        }
    });
}

Layout.Navigation.navPanelBar_onSelect = function (e) {

    var link = $(e.item).children('a.k-link').first().attr('href');

    if (link) {
        var splitter = $("#contentSplitter").data("kendoSplitter");
        splitter.ajaxRequest("#contentPane", link);
        e.preventDefault();
    }
}

Layout.Navigation.syncNavPanelBar = function (funcptr) {
    var splitter = $("#contentSplitter").data("kendoSplitter");

    if (funcptr != undefined) {
        splitter.bind("contentLoad", funcptr);
    }
    splitter.ajaxRequest("#navigationPane", _rootUrl + "Home/Navigation");
}
