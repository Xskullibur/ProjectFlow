$(document).ready(function () {

    let unique_id = $(nav_id).attr('name');

    $(document).on('click', nav_id + ' a.mdc-list-item', function () {
        let href = $(this).data('href');
        __doPostBack(unique_id, href);
    });
});