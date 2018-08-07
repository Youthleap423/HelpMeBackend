$(document).ready(function () {
    $(".dateinputset").datepicker({
        showAnim: "fadeIn",
        firstDay: 1,
        showWeek: true,
        dateFormat: "dd/mm/yy",
        showOn: 'both',
        buttonImageOnly: true,
        buttonImage: '../Content/Images/date.gif',
        onSelect: function (dateText, inst) {
            var todate = new Date(parseInt($(this).val().substring(6, 10)), parseInt($(this).val().substring(3, 5)) - 1, parseInt($(this).val().substring(0, 2)));
            todate.setMonth(todate.getMonth() + parseInt($("#agreementdays").val()));
            todate.setDate(todate.getDate() - 1);
            $(".todateset").val(dateFormat(todate, "dd/mm/yyyy"));
        }
    });

    $(".dateinput").datepicker({
        showAnim: "fadeIn",
        firstDay: 1,
        showWeek: true,
        dateFormat: "dd/mm/yy",
        showOn: 'both',
        buttonImageOnly: true,
        buttonImage: '../Content/Images/date.gif'
    });
});

function validSheetName(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    var ev = evt ? evt : event;
    if (charCode == 63 || charCode == 39 || charCode == 42 || charCode == 47 || charCode == 92 || charCode == 58 || charCode == 36) {
        ev.returnValue = false;
        return false;
    }
    else {
        return true;
    }
}

function BindDateEvents() {
    $(".dateinputset").datepicker({
        showAnim: "fadeIn",
        firstDay: 1,
        showWeek: true,
        dateFormat: "dd/mm/yy",
        showOn: 'both',
        buttonImageOnly: true,
        buttonImage: '../Content/Images/date.gif',
        onSelect: function (dateText, inst) {
            var todate = new Date(parseInt($(this).val().substring(6, 10)), parseInt($(this).val().substring(3, 5)) - 1, parseInt($(this).val().substring(0, 2)));
            todate.setMonth(todate.getMonth() + parseInt($("#agreementdays").val()));
            todate.setDate(todate.getDate() - 1);
            $(".todateset").val(dateFormat(todate, "dd/mm/yyyy"));
        }
    });

    $(".dateinput").datepicker({
        showAnim: "fadeIn",
        firstDay: 1,
        showWeek: true,
        dateFormat: "dd/mm/yy",
        showOn: 'both',
        buttonImageOnly: true,
        buttonImage: '../Content/Images/date.gif'
    });
}