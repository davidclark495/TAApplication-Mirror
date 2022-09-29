$(document).ready(function () {
    $('#roleManager').DataTable();
});

function setRole(unid,role) {
    $.post(
        {
            url: "/Admin/SetRole",
            data: {
                "unid": unid,
                "role": role
            }
        })
        .done(function (data){
        console.log("Sample of Data:", data);
    });
}
