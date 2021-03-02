function jQueryAjaxPost(form)
{
    $.validator.unobtrusive.parse(form);
    if ($(form).valid())
    {
        var ajaxConfig = 
        {

            type: 'POST',
              url: form.action,
              data: new FormData(form),
              sucess: function (response) {
                $("#tablaPer").html(response);

                }


        }
    
    if ($(form).attr('enctype') === "multipart / form - data") {
        ajaxConfig["contentType"] = false;
            ajaxConfig["processData"] = false;
    }
    $.ajax(ajaxConfig);
 }



   
    return false;

}

$("#fechaInf").change(function () {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd
    }
    if (mm < 10) {
        mm = '0' + mm
    }

    today = dd+"/"+mm+"/"+yyyy;
    document.getElementById("fechaInf").setAttribute("max", today);


});


$("#horaInf").blur(function () {

    var dia = $("#fechaInf").val();

    console.log(dia);

    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1;
    var yyyy = today.getFullYear();


    if (dd < 10) {
        dd = '0' + dd
    }
    if (mm < 10) {
        mm = '0' + mm

        today = dd + "/" + mm + "/" + yyyy;

        var currentdate = new Date();
        var hh = currentdate.getHours();
        var min = currentdate.getMinutes();
        var datetime = hh + ":" + min;

        console.log(datetime);
        if (hh > 12) {
            var hh = 12 + hh;
       }
    

        if (dia == today) {
            console.log("mismo dia");
          
            if (min >= 00 && min < 10) {

                var datetime = hh + ":0" + min;
                console.log(dia, today)

            }
            document.getElementById("horaInf").setAttribute("max", datetime);
        } else { document.getElementById("horaInf").setAttribute("max", "");}
        return false;
    }
})

$("#documentosT").blur(function () {
    var string = $("#documentosT").val();
    string = string.trim();
    if (string =="CUIL")
    //  if ($("#documentosT").val() == "CUIL") 
    {
        console.log(string)
            $("#nroDocT").mask("99-99999999-9", { placeholder: "XX-XXXXXXXX-X" });

        }
        else {
            $("#nroDocT").unmask();
        }
    
}
    
);
$('.disabled').click(function (e) {
    e.preventDefault();
})

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode!= 45 && charCode > 31
        && (charCode < 48 || charCode > 57))
       
        return false;
    return true;
}
function noCaracEsp(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode == 63 || charCode > 32 && (charCode < 48 || charCode > 90) &&
        (charCode < 97 || charCode > 122)) {
        return false;
    }
    return true;
}
function noCaracEspConGuion(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode == 63 || charCode > 32 && (charCode < 48 && charCode != 45 || charCode > 90) &&
        (charCode < 97 || charCode > 122)) {
        return false;
    }
    return true;
}
function letras(evt) {
    evt = (evt) ? evt : event;
    var charCode = (evt.charCode) ? evt.charCode : ((evt.keyCode) ? evt.keyCode :
        ((evt.which) ? evt.which : 0));
    if (charCode > 32 && (charCode < 65 || charCode > 90) &&
        (charCode < 97 || charCode > 122)) {
        return false;
    }
    return true;
}

                             


$('#btnLimpiarAuto').click(function () {
  
    $('#colorAuto').val('');
    $('#marcaAuto').val('');
    $('#tipoAuto').val('');
    $('#patenteAutoID').val('');
    $('#modeloAuto').val('');
    
    $("#patenteAutoID").prop('disabled', false);
    $("#tipoAuto").prop('disabled', false);
    $("#btnLimpiarAuto").css('display', 'none');
});