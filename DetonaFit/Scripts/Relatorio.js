$(document).ready(function () {
    GetAlunosMatriculados();
});

function GetAlunosMatriculados() {
    $("#tb_alunosmatriculados").DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Portuguese-Brasil.json"
        },
        "ajax": '/Relatorios/GetAlunosMatriculados',
        "dom": 'B<"panel-menu dt-panelmenu"lrf><"clearfix">tipF',
        "processing": true,
        "serverSide": true,
        "bPaginate": true,
        "bFilter": true,
        "bSort": true,
        "autoWidth": false,
        "aoColumns": [
            { "data": "ID", className: 'dt-body-center', bSortable: false },
            { "data": "Nome", className: 'dt-body-center' },
            { "data": "Identidade", className: 'dt-body-center', bSortable: false },
            { "data": "CPF", className: 'dt-body-center', bSortable: false },
            { "data": "Situacao", className: 'dt-body-center', bSortable: false },
            { "data": "ProximoPagamento", className: 'dt-body-center' },
        ],
        "pagingType": "simple_numbers",
        "iDisplayLength": 10,
        "aLengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
        "order": [[1, "asc"]]
    });

}