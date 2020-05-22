let listaProdutos = [];
let tabelaProdutos = {};
var $row = {};

function apiBuscarProdutos() {
    var urlBuscarProdutos = $('#urlBuscarProdutos').val();
    return $.ajax({
        type: "GET",
        url: urlBuscarProdutos,
        data: {},
        contentType: "application/json; charset=utf-8",
        processData: true,
        traditional: true
    });
}

function apiBuscarPorCodigo(codigo) {
    var urlBuscarPorCodigo = $('#urlBuscarPorCodigo').val();
    return $.ajax({
        type: "GET",
        url: urlBuscarPorCodigo + "?codigoProduto=" + codigo,
        data: {},
        contentType: "application/json; charset=utf-8",
        processData: true,
        traditional: true
    });
}

function apiImageToBase64(formData) {
    var urlImageToBase64 = $('#urlImageToBase64').val();
    return $.ajax({
        url: urlImageToBase64,
        data: formData,
        processData: false,
        contentType: false,
        type: "POST"
    });
}

function apiRemoverProduto(codigo) {
    var urlRemoverProduto = $('#urlRemoverProduto').val();
    return $.ajax({
        url: urlRemoverProduto + "?codigo=" + codigo,
        data: {},
        processData: false,
        contentType: false,
        type: "POST"
    });
}

function apiSalvarProduto(formData) {
    var urlSalvarProduto = $('#urlSalvarProduto').val();
    return $.ajax({
        type: "POST",
        url: urlSalvarProduto,
        data: formData,
        processData: false,
        contentType: false
    });
}

function aplicandoMascaras() {
    $('.quantidadeProduto').mask("000.000.000", { reverse: true });
    $('.precoCustoProduto').mask("000.000.000,00", { reverse: true });
    $('.precoVendaProduto').mask("000.000.000,00", { reverse: true });
}

function BuscarPorCodigo() {

    let codigo = $('.codigoProdutoCadastro').val();

    apiBuscarPorCodigo(codigo).done(function (retorno) {
        if (retorno.sucesso) {
            $('.nomeProduto').val(retorno.dados.nome);
            $('.descricaoProduto').val(retorno.dados.descricao);
            $('.precoCustoProduto').val(retorno.dados.precoCustoExibir);
            $('.precoVendaProduto').val(retorno.dados.precoVendaExibir);
            $('.quantidadeProduto').val(retorno.dados.quantidadeExibir);
            $('.observacaoProduto').val(retorno.dados.observacao);
            $('#files').val('');

            $('.previewImage').removeAttr('src');
            $('.previewImage').attr('src', 'data:image/jpeg;base64, ' + retorno.dados.base64);

            $('.salvarProduto').text('Alterar Cadastro');
        }
        else {
            $('.nomeProduto').val('');
            $('.nomeProduto').val('');
            $('.descricaoProduto').val('');
            $('.precoCustoProduto').val('');
            $('.precoVendaProduto').val('');
            $('.quantidadeProduto').val('');
            $('.observacaoProduto').val('');
            $('#files').val('');
            $('.previewImage').removeAttr('src');

            $('.salvarProduto').text('Confirmar Cadastro');
        }
    });
}

function BuscarProdutos() {
    bloqueioDeTela(true);
    apiBuscarProdutos().done(function (retorno) {
        if (retorno.sucesso) {

            listaProdutos = retorno.dados;

            var table = $('#tbListaProdutos').DataTable();
            table.destroy();

            tabelaProdutos = configuracaoTabelaProdutos();
        }
        else {
            if (retorno.tipo === 'erro') {

            }
            else {

            }
        }
    }).fail(function () { bloqueioDeTela(false); }).always(function () { bloqueioDeTela(false); });
}

function configuracaoTabelaProdutos() {
    let conf = $('#tbListaProdutos').DataTable({
        "language": dataTableLanguage(),
        "aaData": listaProdutos,
        "columns": configuracaoDasColunasDeProdutos()
    });

    return conf;
}

function configuracaoDasColunasDeProdutos() {
    return [
        {
            "data": "produtoId",
            className: "custom-center col-produtoId-produto"
        }, {
            "data": "codigo",
            className: "custom-left col-codigo-produto"
        }, {
            "data": "nome",
            className: "custom-left col-nome-produto"
        }, {
            "data": "precoVendaExibir",
            className: "custom-right col-valor-venda-produto"
        }, {
            "data": "quantidadeExibir",
            className: "custom-right col-quantidade-produto"
        }, {
            "data": null,
            className: "custom-center col-acoes-produto",
            "orderable": false,
            render: configuracaoDasColunasProdutosRenderBotoes
        }];
}

function configuracaoDasColunasProdutosRenderBotoes(data, type, row) {
    var retorno = '';

    retorno += ' <button type="button" class="btn btn-primary btn-xs detalhesProduto" data-codigo="' + row.codigo + '" title="Detalhes Produto"' +
        ' data-toggle="modal" data-target="#modalDetalhesProduto"><span class="glyphicon glyphicon-search"></span>' +
        '</button >';

    retorno += ' <button type="button" class="btn btn-danger btn-xs removerProduto" data-codigo="' + row.codigo + '" title="Remover Produto"' +
        ' data-toggle="modal" data-target="#modalRemoverProduto"><span class="glyphicon glyphicon-remove"></span>' +
        '</button >';

    return retorno;
}

function LimparCamposCadastro() {
    $('.codigoProdutoCadastro').val('');
    $('.nomeProduto').val('');
    $('.descricaoProduto').val('');
    $('.precoCustoProduto').val('');
    $('.precoVendaProduto').val('');
    $('.quantidadeProduto').val('');
    $('.observacaoProduto').val('');
    $('.previewImage').removeAttr('src');
    $('#files').val('');
}

function PreviewFotoAnexada_OnChange() {

    var files = $('#files')[0].files[0];

    var formData = new FormData();
    formData.append("files", files);

    apiImageToBase64(formData).done(function (retorno) {
        if (retorno.sucesso) {

            $('.previewImage').removeAttr('src');
            $('.previewImage').attr('src', 'data:image/jpeg;base64, ' + retorno.dados);

        }
        else {
            if (retorno.tipo === 'erro') {

            }
            else {

            }
        }
    });
}

function SalvarProduto_OnClick() {
    bloqueioDeTela(true);
    var files = $('#files')[0].files[0];
    var existeFoto = false;

    if ($('.previewImage').attr('src') != '')
        existeFoto = true;

    var formData = new FormData();
    formData.append("files", files);
    formData.append("codigo", $('.codigoProdutoCadastro').val());
    formData.append("nome", $('.nomeProduto').val());
    formData.append("descricao", $('.descricaoProduto').val());
    formData.append("precoCusto", $('.precoCustoProduto').val());
    formData.append("precoVenda", $('.precoVendaProduto').val());
    formData.append("quantidade", $('.quantidadeProduto').val());
    formData.append("observacao", $('.observacaoProduto').val());
    formData.append("existeFoto", existeFoto);

    apiSalvarProduto(formData).done(function (retorno) {
        if (retorno.sucesso) {
            abrirDialogSucessoMensagem('O produto foi cadastrado com sucesso!');
            LimparCamposCadastro();
        }
        else {
            if (retorno.tipo === 'erro') {
                abrirDialogErroMensagem(retorno.mensagens);
            }
            else {
                abrirDialogAlertaListaMensagem(retorno.mensagens);
            }
        }
    }).fail(function () { bloqueioDeTela(false); }).always(function () { bloqueioDeTela(false); });
}

function removerProduto() {
    $row = $(this).closest('tr');
    $('.confirmarRemoverProduto').val($(this).data('codigo'));
}

function confirnmarRemoverProduto_OnClick() {
    bloqueioDeTela(true);
    let codigo = $('.confirmarRemoverProduto').val();

    apiRemoverProduto(codigo).done(function (retorno) {
        if (retorno.sucesso) {
            tabelaProdutos.row($row).remove().draw();
            abrirDialogSucessoMensagem(retorno.mensagens);
        }
        else {
            if (retorno.tipo === 'erro') {
                abrirDialogErroMensagem(retorno.mensagens);
            }
            else {
                abrirDialogAlertaListaMensagem(retorno.mensagens);
            }
        }
    }).fail(function () { bloqueioDeTela(false); }).always(function () { bloqueioDeTela(false); });
}

function BuscarDetalhesProduto() {
    bloqueioDeTela(true);
    let codigo = $(this).data('codigo');

    apiBuscarPorCodigo(codigo).done(function (retorno) {
        if (retorno.sucesso) {
            $('.nomeDetalhes').text('Informações do ' + retorno.dados.nome);
            $('.codigoDetalhes').val(retorno.dados.codigo);
            $('.descricaoDetalhes').val(retorno.dados.descricao);
            $('.quantidadeDetalhes').val(retorno.dados.quantidadeExibir);
            $('.precoVendaDetalhes').val(retorno.dados.precoVendaExibir);
            $('.valorEstoqueDetalhes').val(retorno.dados.valorEstoqueExibir);
            $('.observacaoDetalhes').val(retorno.dados.observacao);
            $('.fotoDetalhes').attr('src', 'data:image/jpeg;base64, ' + retorno.dados.base64);
        }
        else {
            if (retorno.tipo === 'erro') {
                abrirDialogErroMensagem(retorno.mensagens);
            }
            else {
                abrirDialogAlertaListaMensagem(retorno.mensagens);
            }
        }
    }).fail(function () { bloqueioDeTela(false); }).always(function () { bloqueioDeTela(false); });
}

function documentoLoginReady() {

    tabelaProdutos = configuracaoDasColunasDeProdutos();

    $("body").delegate(".salvarProduto", "click", SalvarProduto_OnClick);
    $("body").delegate(".btnConsultaProduto", "click", BuscarProdutos);
    $("body").delegate(".codigoProdutoCadastro", "change", BuscarPorCodigo);
    $("body").delegate(".anexarFoto", "change", PreviewFotoAnexada_OnChange);
    $("body").delegate(".removerProduto", "click", removerProduto);
    $("body").delegate(".confirmarRemoverProduto", "click", confirnmarRemoverProduto_OnClick);
    $("body").delegate(".detalhesProduto", "click", BuscarDetalhesProduto);

    BuscarProdutos();
    aplicandoMascaras();
}


$(document).ready(documentoLoginReady);