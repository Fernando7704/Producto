$(document).ready(function () {
    cambio();
});
function cambio() {
    $("#IdArea").change(function (event) {
        $("#DropBIdDepartamento").empty();
        $.ajax({
            url: 'GetId',
            dataType:'json',
            data: { IdArea: $(this).val() },
            type:"POST",
            success: function (departamentos) {
                $.each(departamentos, function (i, departamentos) {
                    $("#DropBIdDepartamento").append('<option value="'
                        + departamentos.Value + '">'
                        + departamentos.Text + '</option>');
                }); 
            }, error: function (ex) {
                alert(ex);
            }

        });
    });
}