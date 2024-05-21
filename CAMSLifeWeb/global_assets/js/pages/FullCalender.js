const FullCalendarBasic = function () {
    // Setup module components
    //

    // Basic calendar
    const _componentFullCalendarBasic = function (date) {
        if (typeof FullCalendar == 'undefined') {
            console.warn('Warning - Fullcalendar files are not loaded.');
            return;
        }



        // Define element
        const plannderCalendarAgendaViewElement = document.querySelector('#planned-calendar');
        const actualCalendarAgendaViewElement = document.querySelector('#actual-calendar');

        // Initialize
        if (plannderCalendarAgendaViewElement) {
            const calendarAgendaViewInit = new FullCalendar.Calendar(plannderCalendarAgendaViewElement, {
                initialDate: date,
                initialView: 'timeGridWeek',
                nowIndicator: true,
                firstDay: 1,
                allDaySlot: false,
                scrollTime: "08:00:00",
                slotMinTime: "08:00:00",
                selectable: true,
                selectMirror: true,
                direction: 'ltr',
                events: plannedevents
                ,
                eventClick: function (event, jsEvent, view) {
                    console.log(jsEvent);
                    console.log(event.event._def.extendedProps.dealId);
                    ViewPlannedActivity(event.event._def.extendedProps.dealId);
                    $('#modal_editPlannedActivity').modal();
                }
            });

            // Init
            calendarAgendaViewInit.render();

            // Resize calendar when sidebar toggler is clicked
            $('.sidebar-control').on('click', function () {
                calendarAgendaViewInit.updateSize();
            });
        }

        if (actualCalendarAgendaViewElement) {
            const calendarAgendaViewInit = new FullCalendar.Calendar(actualCalendarAgendaViewElement, {
                initialDate: date,
                initialView: 'timeGridWeek',
                nowIndicator: true,
                firstDay: 1,
                allDaySlot: false,
                scrollTime: "08:00:00",
                slotMinTime: "08:00:00",
                direction: 'ltr',
                events: actualEvents,
                //eventClick: function (event, jsEvent, view) {
                //    console.log(jsEvent);
                //    console.log(event.event._def.extendedProps.dealId);
                //    //ViewActivity(event.event._def.extendedProps.dealId, event.event._def.extendedProps.type);
                //    //$('#modal_editActualActivity').modal();
                //}
            });

            // Init
            calendarAgendaViewInit.render();

            // Resize calendar when sidebar toggler is clicked
            $('.sidebar-control').on('click', function () {
                calendarAgendaViewInit.updateSize();
            });
        }


    };


    //
    // Return objects assigned to module
    //

    return {
        init: function (date) {
            console.log(date);
            _componentFullCalendarBasic(date);
        }
    }
}();


// Initialize module
// ------------------------------

document.addEventListener('DOMContentLoaded', function () {
    var startDate = formatDate(getMonday(new Date()));

    FullCalendarBasic.init(startDate);
});



function getMonday(d) {
    d = new Date(d);
    var day = d.getDay(),
        diff = d.getDate() - day + (day == 0 ? -6 : 1); // adjust when day is sunday
    return new Date(d.setDate(diff));
}
function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    return [year, month, day].join('-');
}