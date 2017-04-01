function movetodate(dateVal,changeFormat)
{
  //  alert(dateVal);
    if (!changeFormat)
        location.href = '/Task/TaskCalendarByDate/?taskDate=' + dateVal;
    else
        location.href = '/Task/TaskCalendarByDateChangeFormat/?taskDate=' + dateVal;
}


//jQuery(document).ready(function ($) {
//    console.log("ready!");
//    alert('1');
//    $("#calendar").datepicker();
//});

//$(document).ready(function () {
//    console.log("ready2!");

    //alert('1');
    //$("#calendar").datepicker();
//});