(function (undefined) {
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

    window.observe = function (productId) {
        var mail = $('#mail').val();
        $.ajax({
            url: '/api/Product/Observer?productId=' + productId + '&email=' + mail,
            method: 'POST',
            success: function (data) {
                $('#mailModal' + productId).modal('toggle');
            },
            error: function (xhr) {
                $('#error').html('Please enter a valid mail address');
            }
        });
    };

    function load(page, pageSize) {
        window.location = '/Product?page=' + page + '&pageSize=' + pageSize;
    }
})();