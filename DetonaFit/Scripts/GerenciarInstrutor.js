$(document).ready(function () {

    $("#addNovoInstrutor").on("click", function () {
        window.location = "/GerenciarInstrutor/CadastrarInstrutor";
    });

    $("#tb_instrutor tbody").on("click", ".edtInstrutor", function () {
        var id = $(this).attr("id");

        window.location = "/GerenciarInstrutor/AlterarInstrutor?id=" + id;
    });

    $("#tb_instrutor tbody").on("click", ".dltInstrutor", function () {
        debugger;
        var id = $(this).attr("id");

        window.location = "/GerenciarInstrutor/ExcluirInstrutor?id=" + id;
    });

    var Atividade = $("#Atividade").val();

    if (Atividade != "" && Atividade != null && Atividade != undefined) {
        $(".atividadeInstrutor").val(Atividade);
    }

    debugger;
    var EstadoInstrutor = $("#Estado").val();

    if (EstadoInstrutor != "" && EstadoInstrutor != null && EstadoInstrutor != undefined) {
        $(".estadosInstrutor").val(EstadoInstrutor);
    }

    $(".atividadeInstrutor").on("change", function () {
        var valor = $(this).val();

        $("#Atividade").val(valor);
    })

    $(".estadosInstrutor").on("change", function () {
        var valor = $(this).val();

        $("#Estado").val(valor);
    })

    GetInstrutores();
});


function GetInstrutores() {
    debugger;
    $("#tb_instrutor").DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Portuguese-Brasil.json"
        },
        "ajax": '/GerenciarInstrutor/GetInstrutores',
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
            { "data": "Atividade", className: 'dt-body-center', bSortable: false },
            {
                "data": null,
                className: 'dt-body-center',
                mRender: function (data) {
                    var deletar = '<label id="' + data.ID + '" class="dltInstrutor label label-danger" style="margin-left: 5px;">Excluir</label>'

                    var editar = '<label  id="' + data.ID + '" class="edtInstrutor label label-primary">Editar</label>'

                    return editar + deletar;
                },
                bSortable: false

            },
        ],
        "pagingType": "simple_numbers",
        "iDisplayLength": 10,
        "aLengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
        "order": [[1, "asc"]]
    });
}

//function ddd() {
//    $("#tb-instrutor").DataTable({
//        "language": {
//            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Portuguese-Brasil.json"            
//        },
//        "ajax": '/GerenciarInstrutor/GetInstrutores',
//        "dom": 'B<"panel-menu dt-panelmenu"lrf><"clearfix">tipF',
//        "processing": true,
//        "serverSide": true,
//        "bPaginate": true,
//        "bFilter": true,
//        "bSort": true,
//        "autoWidth": false,
//        "aoColumns": [
//            { "data": "ID", className: 'dt-body-center', bSortable: false},
//            { "data": "Nome", className: 'dt-body-center' },
//            { "data": "Identidade", className: 'dt-body-center', bSortable: false},
//            { "data": "CPF", className: 'dt-body-center', bSortable: false },
//            { "data": "Atividade", className: 'dt-body-center', bSortable: false },
//            {
//                "data": null,
//                className: 'dt-body-center',
//                mRender: function (data) {
//                    var deletar = '<label id="' + data.ID +'" class="dltInstrutor label label-danger" style="margin-left: 5px;">Excluir</label>'

//                    var editar = '<label  id="' + data.ID +'" class="edtInstrutor label label-primary">Editar</label>'

//                    return editar + deletar;
//                },
//                bSortable: false
//            },
//        ],
//        "pagingType": "simple_numbers",
//        "iDisplayLength": 10,
//        "aLengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
//        "order": [[0, "desc"]]
//    });

//}
