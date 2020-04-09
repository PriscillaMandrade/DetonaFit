
$(document).ready(function () {

    $("#addNovoAluno").on("click", function () {
        window.location = "/GerenciarAluno/CadastrarAluno";
    });

    GetAlunos();

    Mascaras();

    $("#tb_alunos tbody").on("click", ".edtAluno", function () {
        var id = $(this).attr("id");

        window.location = "/GerenciarAluno/AlterarAluno?id=" + id;
    });

    $("#tb_alunos tbody").on("click", ".dltAluno", function () {
        debugger;
        var id = $(this).attr("id");

        window.location = "/GerenciarAluno/ExcluirAluno?id=" + id;
    });

    var Plano = $("#Plano").val();

    if (Plano != "" && Plano != null && Plano != undefined) {
        $(".planoAluno").val(Plano);
    }

    debugger;
    var EstadoAluno = $("#Estado").val();

    if (EstadoAluno != "" && EstadoAluno != null && EstadoAluno != undefined) {
        $(".estadosAluno").val(EstadoAluno);
    }


    $(".planoAluno").on("change", function () {
        var valor = $(this).val();

        $("#Plano").val(valor);
    })

    $(".estadosAluno").on("change", function () {
        var valor = $(this).val();

        $("#Estado").val(valor);
    })
});

function GetAlunos() {
    $("#tb_alunos").DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Portuguese-Brasil.json"
        },
        "ajax": '/GerenciarAluno/GetAlunos',
        "dom": 'B<"panel-menu dt-panelmenu"lrf><"clearfix">tipF',
        "processing": true,
        "serverSide": true,
        "bPaginate": true,
        "bFilter": true,
        "bSort": true,
        "autoWidth": false,
        "aoColumns": [
            { "data": "ID", className: 'dt-body-center', bSortable: false},
            { "data": "Nome", className: 'dt-body-center'},
            { "data": "Identidade", className: 'dt-body-center', bSortable: false},
            { "data": "CPF", className: 'dt-body-center', bSortable: false},
            { "data": "Situacao", className: 'dt-body-center', bSortable: false},
            {
                "data": null,  
                className: 'dt-body-center',
                mRender: function (data) {
                    var deletar = '<label id="' + data.ID +'" class="dltAluno label label-danger" style="margin-left: 5px;">Excluir</label>'

                    var editar = '<label  id="' + data.ID +'" class="edtAluno label label-primary">Editar</label>'

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

function Mascaras() {
    $("#CPF").mask("000.000.000-00");

    $("#CEP").mask("00000-000");
}