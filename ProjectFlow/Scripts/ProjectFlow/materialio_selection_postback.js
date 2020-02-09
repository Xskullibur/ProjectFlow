let id;

$(document).ready(function () {
    id = '#ContentPlaceHolderBase_ContentPlaceHolder_materialTable';

    let unique_id = $(id).attr('name');

    $(document).on('change', id + ' input[type=checkbox].mdc-checkbox__native-control.autoselect', function () {
        selectAll(this.checked);
        __doPostBack(unique_id, JSON.stringify(getSelectedArray()));
    });

    $(document).on('change', id + ' input[type=checkbox].mdc-checkbox__native-control:not(.autoselect)', function () {
        __doPostBack(unique_id, JSON.stringify(getSelectedArray()));
    });

});

/**
 * Returns an array of boolean value consisting of each element selection sorted by its index
 * */
function getSelectedArray() {
    return $.map($(id + ' input[type=checkbox].mdc-checkbox__native-control:not(.autoselect)'), function (input, i) {
        return input.checked;
    });
}

function selectAll(value) {
    $.each($(id + ' input[type=checkbox].mdc-checkbox__native-control:not(.autoselect)'), function (i, input) {
        $(input).prop('checked', value);
    });
}

