
function ShowAlert(message, messageType) {

    var alertClass;

    switch (messageType) {

        case "Success":
            alertClass = 'alert-success'
            break;

        case "Danger":
            alertClass = 'alert-danger'
            break;

        case "Error":
            alertClass = 'alert-warning'
            break;

        case "Primary":
            alertClass = 'alert-primary'
            break;

        case "Secondary":
            alertClass = "alert-secondary"
            break;

        case "Info":
            alertClass = "alert-info"
            break;

        case "Light":
            alertClass = "alert-light"
            break;

        case "Dark":
            alertClass = "alert-dark"
            break;

        default:
            alertClass = "alert-primary"
            break;
    }

    $('#alert_container').append(`
        <div id="alert" class="alert fade in ${alertClass}">
            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
            <span>${message}</span>
        </div>`);

    $('#alert').show("fast");
}