// Defines an IFFY to make sure undefined wasn't overridden.
(function (undefined) {
    // Gets an url parameter by its name
    function getParameterByName(name, url) {
        if (!url) {
            url = window.location.href;
        }
        name = name.replace(/[\[\]]/g, "\\$&");
        var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)");
        results = regex.exec(url);
        if (!results) {
            return null;
        }
        if (!results[2]) {
            return '';
        }
        return decodeURIComponent(results[2].replace(/\+/g, " "));
    }

    // loads the previous page on the list view.
    window.previous = function () {
        var page = getParameterByName('page');
        var pageSize = getParameterByName('pageSize');
        if (page === null || pageSize === null) {
            page = 0;
            pageSize = 10;
            load(page, pageSize);
        } else {
            if (page > 0) {
                page--;
            }
            load(page, pageSize || 10);
        }
    };

    // loads the next page on the list view.
    window.next = function () {
        var page = getParameterByName('page');
        var pageSize = getParameterByName('pageSize');
        if (page === null || pageSize === null) {
            page = 1;
            pageSize = 10;
            load(page, pageSize);
        } else {
            page++;
            load(page, pageSize || 10);
        }
    };

    // observes a product with the given id.
    window.observe = function (productId) {
        var mail = $('#mail').val();
        // Execute AJAX requests.
        $.ajax({
            url: '/api/Product/Observer?productId=' + productId + '&email=' + mail,
            method: 'POST',
            success: function (data) {
                // Close the modal dialog, if the action was successful.
                $('#mailModal' + productId).modal('toggle');
            },
            error: function (xhr) {
                // If the mail wasn't valid, show an error message.
                $('#error').html('Please enter a valid mail address');
            }
        });
    };

    // Loads the page with the given page and page size.
    function load(page, pageSize) {
        window.location = '/Product?page=' + page + '&pageSize=' + pageSize;
    }
})();