function visibilityChange()
{
    var field = $("#TaskType");
    var sd = field.val();

    //  alert(sd);
    if (sd == 1) {
        document.getElementById('TaskPeriodDiv').style.display = "none";
        document.getElementById('TaskDataStart').style.display = "";
        document.getElementById('TaskDataEnd').style.display = "";
        document.getElementById('TaskPeriod').style.display = "none";
        document.getElementById('periodLabel').style.display = "none";
        document.getElementById('dateStartLabel').style.display = "";
        document.getElementById('dateEndLabel').style.display = "";
    }
    if (sd == 2) {
        // alert('3');
        // document.getElementById('TaskPeriod').value = "0 0 * * 0";
        document.getElementById('TaskPeriodDiv').style.display = "";
        document.getElementById('TaskPeriodDiv').value = "";
        // document.getElementById('TaskPeriod').style.display = document.getElementById('TaskPeriod').value;
        //document.getElementById('TaskPeriodDiv').style.display = document.getElementById('TaskPeriodDiv').value;
        document.getElementById('TaskDataStart').style.display = "none";
        document.getElementById('TaskDataEnd').style.display = "none";
        document.getElementById('dateStartLabel').style.display = "none";
        document.getElementById('dateEndLabel').style.display = "none";
        document.getElementById('periodLabel').innerHTML = "";

    }
}

$(document).ready(function () {
    console.log("ready!");
    visibilityChange();

    $("#TaskType").change(function (event, ui) {
      // alert('1');
        event.preventDefault();
        visibilityChange();
    })

});