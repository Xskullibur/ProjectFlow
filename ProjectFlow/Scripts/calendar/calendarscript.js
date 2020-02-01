var globalAllDay;

function checkForSpecialChars(stringToCheck) {
    var pattern = /[^A-Za-z0-9 ]/;
    return pattern.test(stringToCheck); 
}

function isAllDay(startDate, endDate) {
    var allDay;

    if (startDate.format("HH:mm:ss") == "00:00:00" && endDate.format("HH:mm:ss") == "00:00:00") {
        allDay = true;
        globalAllDay = true;
    }
    else {
        allDay = false;
        globalAllDay = false;
    }
    
    return allDay;
}

function qTipText(start, end, description) {
    var text;

    if (end !== null)
        text = description + "<br/><br/>" + "<strong>Start:</strong> " + start.format("DD/MM/YYYY") + "<br/><strong>End:</strong> " + end.format("DD/MM/YYYY");
    else
        text = description + "<br/><br/>" + "<strong>Start:</strong> " + start.format("DD/MM/YYYY") + "<br/><strong>End:</strong>" + start.format("DD/MM/YYYY");

    return text;
}

$(document).ready(function() {

    var date = new Date();
    var d = date.getDate();
    var m = date.getMonth();
    var y = date.getFullYear();
    var options = {
        weekday: "long", year: "numeric", month: "short",
        day: "numeric", hour: "2-digit", minute: "2-digit"
    };

    $('#calendar').fullCalendar({
        header: {
            left: 'today',
            center: 'title',
            right: 'prev,next'
        },
        defaultView: 'month',
        selectable: true,
        selectHelper: true,
        editable: false,
        events: "JsonResponse.ashx",
        eventRender: function(event, element) {
            element.qtip({
                content: {
                    title: '<strong>' + event.title + '</strong>',
                    text: qTipText(event.start, event.end, event.description)
                },
                position: {
                    my: 'bottom left',
                    at: 'bottom center'
                },
                style: { classes: 'qtip-shadow qtip-rounded qtip-bootstrap' }
            });
        }
    });
});
