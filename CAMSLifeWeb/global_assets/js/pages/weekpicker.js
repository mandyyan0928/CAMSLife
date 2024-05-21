$(function () {
    var sDate;
    var endDate;


    var selectCurrentWeek = function () {
    window.setTimeout(function () {
        $('.week-picker').find('.ui-datepicker-current-day a').addClass('ui-state-active')
    }, 1);
}

$('.week-picker').datepicker({
    showOtherMonths: true,
    selectOtherMonths: true,
    firstDay: 1,
    onSelect: function (dateText, inst) {
        var date = $(this).datepicker('getDate');
        sDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() - date.getDay() + 1);
        endDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() - date.getDay() + 7);
        var dateFormat = inst.settings.dateFormat || $.datepicker._defaults.dateFormat;
        $('#startDate').text($.datepicker.formatDate(dateFormat, sDate, inst.settings));
        $('#endDate').text($.datepicker.formatDate(dateFormat, endDate, inst.settings));
        this.startDate = sDate;


        var ssdate = formatDate(sDate);
        plannedeventReview();
        actualeventReview();
        FullCalendarBasic.init(ssdate);
        selectCurrentWeek();
    },
    beforeShowDay: function (date) {
        var cssClass = '';
        if (date >= sDate && date <= endDate)
            cssClass = 'ui-datepicker-current-day';
        return [true, cssClass];
    },
    onChangeMonthYear: function (year, month, inst) {
        selectCurrentWeek();
    }
});

$('.week-picker .ui-datepicker-calendar tr').on('mousemove', function () { $(this).find('td a').addClass('ui-state-hover'); });
$('.week-picker .ui-datepicker-calendar tr').on('mouseleave', function () { $(this).find('td a').removeClass('ui-state-hover'); });
    });