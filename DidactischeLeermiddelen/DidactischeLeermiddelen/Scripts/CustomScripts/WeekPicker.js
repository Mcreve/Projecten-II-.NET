var startDate,
        endDate;



$('#weekpicker').datepicker({
    autoclose: true,
    format: 'dd/mm/yyyy',
    forceParse: false,
    startDate: new Date(),
    weekStart: 1
}).on("changeDate", function (e) {
    var date = e.date;
    startDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() - date.getDay()+1);
    endDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() - date.getDay() + 5);
    $('#weekpicker').datepicker('update', startDate);
    $('#weekpicker').val(startDate.getDate() + '/' + (startDate.getMonth() + 1) + '/' + startDate.getFullYear() + ' - ' + endDate.getDate() + '/' + (endDate.getMonth() + 1) + '/' + endDate.getFullYear());
    $('#date').val($('#weekpicker').datepicker('getFormattedDate'));
});