var startDate,
        endDate;

$.fn.datepicker.dates['nl'] = {
    days: ["Zondag", "Maandag", "Dinsdag", "Woensdag", "Donderdag", "Vrijdag", "Zaterdag"],
    daysShort: ["Zo", "Ma", "Di", "Wo", "Do", "Vr", "Za"],
    daysMin: ["Zo", "Ma", "Di", "Wo", "Do", "Vr", "Za"],
    months: ["Januari", "Februari", "Maart", "April", "Mei", "Juni", "July", "Augustus", "September", "October", "November", "December"],
    monthsShort: ["Jan", "Feb", "Mar", "Apr", "Mei", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
    today: "Vandaag",
    clear: "Reset",
    format: "dd/mm/yyyy",
    titleFormat: "MM yyyy", /* Leverages same syntax as 'format' */
    weekStart: 1
};

$('#weekpicker').datepicker({
    autoclose: true,
    format: 'dd/mm/yyyy',
    forceParse: false,
    startDate: new Date(),
    weekStart: 1,
    language: "nl"
}).on("changeDate", function (e) {
    var date = e.date;
    startDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() - date.getDay()+1);
    endDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() - date.getDay() + 5);
    $('#weekpicker').datepicker('update', startDate);
    $('#weekpicker').val(startDate.getDate() + '/' + (startDate.getMonth() + 1) + '/' + startDate.getFullYear() + ' - ' + endDate.getDate() + '/' + (endDate.getMonth() + 1) + '/' + endDate.getFullYear());
    $('#date').val($('#weekpicker').datepicker('getFormattedDate'));
    $("#dateForm").submit();
});

var dateformView = {
    init: function() {
        $("#dateForm").submit(function() {
            $.post(this.action, $(this).serialize(), function(data) {
                $("#ajax-result").html(data);
            });
            return false;
        });
    }
}

$(function () {
    dateformView.init();
});