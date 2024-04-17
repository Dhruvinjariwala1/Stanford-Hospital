$(document).ready(function () {
    $('#userlist').DataTable({
        columnDefs: [{
            "defaultContent": "-",
            "targets": "_all"
        }]
    });
});